using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.Json;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Threading;


namespace ELEA_BOARD
{
    public partial class MainForm : Form
    {
        //(250429조민석)
        private Image penDefaultImage, penClickedImage;
        private Image highlightDefaultImage, highlightClickedImage;
        private Image eraserDefaultImage, eraserClickedImage;
        private Image boardDefaultImage, boardClickedImage;
        private Image eleaDefaultImage, eleaClickedImage;
        private Image goClass;
        //(250430조민석)
        private Image hambergerDefaultImage, hambergerClickImage;
        //(250430조민석)펜 이미지
        private Image penBlackDefaultImage, penBlackClickImage;
        private Image penRedDefaultImage, penRedClickImage;
        private Image penBlueDefaultImage, penBlueClickImage;
        //(250430조민석) 형광펜 이미지
        private Image highlightYellowDefaultImage, highlightYellowClickImage;
        private Image highlightGreenDefaultImage, highlightGreenClickImage;
        private Image highlightPinkDefultImage, highlightPinkClickImage;
        //펜, 형광펜 슬라이더 이미지(250502 조민석)
        private Image penTrackBackImage, penTrackFrontImage, penTrackPointImagge;
        //지우개 이미지(250507조민석)
        private Image EraserPrevDefaultImage, EraserPrevClickImage;
        private Image EraserStrockDefaultImage, EraserStrockClickImage;
        private Image Eraser1DefaultImage, Eraser1ClickImage;
        private Image ErserAllDefaultImage, ErserAllCilckImage;
        //펜색 버튼 기본값(250507)
        private Button selectedPenButton = null;
        //private string project_path = Directory.GetParent(Directory.GetParent(Application.StartupPath).FullName).FullName;
        // 현재 클릭된 툴바 버튼 저장
        private Button activeButton = null;

        // 전역 변수 선언
        bool isDragging = false;
        int dragOffsetX = 0;

        //250430(혜진 추가)
        private DrawingForm drawForm = null;

        private static readonly HttpClient client = new HttpClient();

        //ppt viewer 추가(250521)
        private Process pptProcess = null;
        private System.Windows.Forms.Timer checkWindowTimer; // Timer 클래스 명확히 지정
        private string openedFilePath = null;
        private int attempts = 0; // static 키워드 제거하고 인스턴스 변수로 변경\
        public Panel panel1;

        public MainForm()
        {
            InitializeComponent();
            pptList();

            // panel1 생성 및 설정 – MainForm 전체에 꽉 차게
            panel1 = new Panel();
            panel1.Dock = DockStyle.Fill;
            this.Controls.Add(panel1);


            // 타이머 초기화
            checkWindowTimer = new System.Windows.Forms.Timer();
            checkWindowTimer.Interval = 200;
            checkWindowTimer.Tick += CheckWindowTimer_Tick;

            this.FormClosing += Form1_FormClosing;

            Process[] processes = Process.GetProcessesByName("PPTService");
            foreach (Process proc in processes)
            {
                try
                {
                    if (!proc.HasExited)
                    {
                        proc.Kill();
                    }
                }
                catch { }
            }

            // 타이머 설정
            checkWindowTimer = new System.Windows.Forms.Timer();
            checkWindowTimer.Interval = 500;
            checkWindowTimer.Tick += CheckWindowTimer_Tick;

            this.FormClosing += Form1_FormClosing;
        }

        private void RunPowerPointSlideShow(string filePath)
        {
            try
            {
                openedFilePath = filePath;
                string fileExtension = Path.GetExtension(filePath).ToLower();
                bool isPpsx = fileExtension == ".ppsx";
                Console.WriteLine("isPpsx" + isPpsx);

                // PPTVIEW.EXE를 사용하여 슬라이드쇼 모드로 직접 실행
                // 이 방법은 슬라이드 쇼를 바로 시작합니다
                ProcessStartInfo startInfo = new ProcessStartInfo();

                // 슬라이드쇼 모드로 직접 실행하는 방법
                if (isPpsx)
                {
                    // PPSX는 이미 슬라이드쇼 파일이므로 직접 실행
                    startInfo.FileName = filePath;
                    startInfo.UseShellExecute = true;
                }
                else
                {
                    // PPTX를 슬라이드쇼로 실행하기 위한 특별 처리
                    // PowerPoint가 있는 폴더의 PPTVIEW.EXE 찾기 시도
                    string pptViewPath = FindPowerPointViewerPath();

                    if (!String.IsNullOrEmpty(pptViewPath) && File.Exists(pptViewPath))
                    {
                        startInfo.FileName = pptViewPath;
                        startInfo.Arguments = "\"" + filePath + "\"";
                        startInfo.UseShellExecute = false;
                    }
                    else
                    {
                        // PPTVIEW.EXE를 찾지 못한 경우 대체 방법
                        // PowerPoint 실행 후 F5 키 전송하는 방식으로 대체
                        startInfo.FileName = "POWERPNT.EXE";
                        startInfo.Arguments = "/S \"" + filePath + "\"";
                        startInfo.UseShellExecute = true;
                    }
                }

                startInfo.WorkingDirectory = Path.GetDirectoryName(filePath) ?? Environment.CurrentDirectory;

                // PowerPoint 프로세스 시작
                pptProcess = Process.Start(startInfo);

                // 검색 시도 횟수 초기화
                attempts = 0;

                // 타이머 시작 (창을 찾아서 임베드하기 위함)
                checkWindowTimer.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show("슬라이드쇼 실행 중 오류가 발생했습니다: " + ex.Message, "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CheckWindowTimer_Tick(object sender, EventArgs e)
        {
            // 10초간 20번 시도 (0.5초 간격으로 업데이트)
            if (attempts > 20)
            {
                checkWindowTimer.Stop();
                attempts = 0;
                return;
            }
            attempts++;

            // 슬라이드쇼 창 찾기
            IntPtr slideShowHwnd = FindPowerPointSlideShowWindow();
            if (slideShowHwnd != IntPtr.Zero)
            {
                checkWindowTimer.Stop();
                attempts = 0;

                // 창 스타일 변경 및 패널에 임베드
                EmbedWindowInPanel(slideShowHwnd);
            }
            else if (attempts >= 1) // 2초 이상 지나면 F5 키 한번 더 시도
            {
                // PowerPoint 메인 창 찾기
                IntPtr ppHwnd = FindPowerPointWindow();
                if (ppHwnd != IntPtr.Zero)
                {
                    // F5 키 전송 시도
                    SetForegroundWindow(ppHwnd);
                    Thread.Sleep(500);
                    SendKeys.SendWait("{F5}");
                }
            }
        }

        private void EmbedWindowInPanel(IntPtr hwnd)
        {
            try
            {
                // 창 스타일 변경 (테두리 제거)
                int style = GetWindowLong(hwnd, GWL_STYLE);
                style &= ~(WS_CAPTION | WS_THICKFRAME);
                SetWindowLong(hwnd, GWL_STYLE, style);

                // 창 위치 조정
                System.Drawing.Point panelPoint = panel1.PointToScreen(new System.Drawing.Point(0, 0));
                MoveWindow(hwnd, panelPoint.X, panelPoint.Y, panel1.Width, panel1.Height, true);

                // 창을 패널에 임베드
                SetParent(hwnd, panel1.Handle);

                // 창 크기 조정
                SetWindowPos(hwnd, IntPtr.Zero, 0, 0, panel1.Width, panel1.Height,
                    SWP_NOZORDER | SWP_SHOWWINDOW);
            }
            catch (Exception ex)
            {
                MessageBox.Show("창 임베딩 중 오류 발생: " + ex.Message);
            }
        }

        private string FindPowerPointViewerPath()
        {
            // PowerPoint 설치 폴더에서 PPTVIEW.EXE 찾기
            string[] possiblePaths = {
                @"C:\Program Files\Microsoft Office\root\Office16\PPTVIEW.EXE",
                @"C:\Program Files (x86)\Microsoft Office\root\Office16\PPTVIEW.EXE",
                @"C:\Program Files\Microsoft Office\Office16\PPTVIEW.EXE",
                @"C:\Program Files (x86)\Microsoft Office\Office16\PPTVIEW.EXE",
                @"C:\Program Files\Microsoft Office\Office15\PPTVIEW.EXE",
                @"C:\Program Files (x86)\Microsoft Office\Office15\PPTVIEW.EXE"
            };

            foreach (string path in possiblePaths)
            {
                if (File.Exists(path))
                {
                    return path;
                }
            }

            return string.Empty;
        }

        private IntPtr FindPowerPointWindow()
        {
            return FindWindow("PPTFrameClass", null);
        }

        private IntPtr FindPowerPointSlideShowWindow()
        {
            // 슬라이드쇼 창 클래스 이름들
            string[] classNames = {
                "PPTSlideShowWndClass",  // 최신 PowerPoint 버전
                "paneClassDC",           // 일부 버전
                "screenClass",           // 일부 버전
                "FullpageScreenClass"    // 일부 버전
            };

            foreach (string className in classNames)
            {
                IntPtr hwnd = FindWindow(className, null);
                if (hwnd != IntPtr.Zero)
                {
                    return hwnd;
                }
            }

            return IntPtr.Zero;
        }

        private void CloseCurrentPresentation()
        {
            checkWindowTimer.Stop();

            try
            {
                // 모든 PowerPoint 프로세스 종료를 시도
                Process[] processes = Process.GetProcessesByName("POWERPNT");
                foreach (Process proc in processes)
                {
                    try
                    {
                        proc.CloseMainWindow();
                        Thread.Sleep(500);
                        if (!proc.HasExited)
                        {
                            proc.Kill();
                        }
                    }
                    catch { }
                }

                // PowerPoint Viewer 프로세스도 종료
                Process[] viewerProcesses = Process.GetProcessesByName("PPTVIEW");
                foreach (Process proc in viewerProcesses)
                {
                    try
                    {
                        proc.Kill();
                    }
                    catch { }
                }

                pptProcess = null;
                openedFilePath = null;
            }
            catch (Exception ex)
            {
                Console.WriteLine("프로세스 종료 중 오류: " + ex.Message);
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            checkWindowTimer.Stop();
            CloseCurrentPresentation();
        }

        #region Windows API
        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        [DllImport("user32.dll")]
        private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        // 상수 정의
        private const int GWL_STYLE = -16;
        private const int WS_CAPTION = 0x00C00000;
        private const int WS_THICKFRAME = 0x00040000;
        private const int WS_MINIMIZE = 0x20000000;
        private const int WS_MAXIMIZE = 0x01000000;
        private const int WS_SYSMENU = 0x00080000;
        private const uint SWP_NOZORDER = 0x0004;
        private const uint SWP_SHOWWINDOW = 0x0040;
        #endregion


    private void ApplyTransparentStyleToAll(Control.ControlCollection controls)
        {
            foreach (Control ctrl in controls)
            {
                if (ctrl is Button btn)
                {
                    ApplyImageOnlyButtonStyle(btn);
                }
                else if (ctrl is Panel pnl)
                {
                    pnl.BackColor = Color.Transparent;
                }

                // 재귀적으로 자식 컨트롤도 처리
                if (ctrl.HasChildren)
                {
                    ApplyTransparentStyleToAll(ctrl.Controls);
                }
            }
        }

        private void ApplyImageOnlyButtonStyle(Button btn)
        {
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btn.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btn.BackColor = Color.Transparent;
            btn.ForeColor = Color.Transparent;
            btn.Text = "";
            btn.TabStop = false;
            btn.BackgroundImageLayout = ImageLayout.Stretch;

            // 포커스 테두리 제거
            btn.GotFocus += (s, e) => btn.Parent?.Focus();
        }


        public async Task pptList()
        {
            string url = "http://192.168.0.238:7777/en/front/index";

            var request = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = new StringContent("{}", Encoding.UTF8, "application/json")
            };

            try
            {
                HttpResponseMessage response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    string jsonString = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("응답 JSON (원본):");
                    Console.WriteLine(jsonString);

                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    FileListResponse dataList = JsonSerializer.Deserialize<FileListResponse>(jsonString, options);
                    if (dataList?.fileList != null)
                    {
                        foreach (var item in dataList.fileList)
                        {
                            string urlPost = "http://192.168.0.238:7777/en/front/getppt/";
                            DownloadPpt(item.index, urlPost);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("에러 발생: " + ex.Message);
            }
        }

        public void DownloadPpt(int index, string serverUrl)
        {
            string url = serverUrl + index;
            Console.WriteLine(url);
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    // 서버에 GET 요청
                    HttpResponseMessage response = client.GetAsync(url).Result;
                    Console.WriteLine(response.IsSuccessStatusCode);
                    byte[] fileData = response.Content.ReadAsByteArrayAsync().Result;

                    // 기본 파일명 설정
                    string fileName = "downloadedPpt.ppsx";

                    // Content-Disposition에서 파일명 추출
                    if (response.Content.Headers.ContentDisposition != null)
                    {
                        fileName = response.Content.Headers.ContentDisposition.FileName?.Trim('"') ?? fileName;
                    }
                    Console.WriteLine(fileName);

                    // 저장 경로 설정
                    string saveDirectory = Path.Combine(Application.StartupPath, "PPT");
                    Console.WriteLine(saveDirectory);
                    if (!Directory.Exists(saveDirectory))
                        Directory.CreateDirectory(saveDirectory);

                    string saveFilePath = Path.Combine(saveDirectory, fileName);

                    // 파일 저장 (동기)
                    File.WriteAllBytes(saveFilePath, fileData);
                    Console.WriteLine("파일 저장 완료: " + saveFilePath);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("예외 발생: " + ex.Message);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // 화면 해상도를 가져옵니다.
            Rectangle screenBounds = Screen.PrimaryScreen.Bounds;

            // 폼의 크기를 화면 해상도에 맞춥니다.
            this.Width = screenBounds.Width;
            this.Height = screenBounds.Height;
            // 전체화면 설정
            this.FormBorderStyle = FormBorderStyle.None;         // 창 테두리 제거 (버튼 포함)
            this.WindowState = FormWindowState.Maximized;        // 전체화면

            // 작업 표시줄까지 완전히 덮기
            this.Bounds = Screen.PrimaryScreen.Bounds;

            //툴바 이미지 맵핑
            try
            {
                //툴바 펜 버튼 이미지(250429조민석)
                penDefaultImage = Image.FromFile(Application.StartupPath + @"\image\\menu_pen_off.png");
                penClickedImage = Image.FromFile(Application.StartupPath + @"\image\\menu_pen_on.png");
                highlightDefaultImage = Image.FromFile(Application.StartupPath + @"\image\\menu_highlight_off.png");
                highlightClickedImage = Image.FromFile(Application.StartupPath + @"\image\\menu_highlight_on.png");
                eraserDefaultImage = Image.FromFile(Application.StartupPath + @"\image\\menu_eraser_off.png");
                eraserClickedImage = Image.FromFile(Application.StartupPath + @"\image\\menu_eraser_on.png");
                boardDefaultImage = Image.FromFile(Application.StartupPath + @"\image\\menu_board_off.png");
                boardClickedImage = Image.FromFile(Application.StartupPath + @"\image\\menu_board_on.png");
                eleaDefaultImage = Image.FromFile(Application.StartupPath + @"\image\\menu_elea_off.png");
                eleaClickedImage = Image.FromFile(Application.StartupPath + @"\image\\menu_elea_on.png");
                //햄버거 버튼 이미지 (250430조민석)
                hambergerDefaultImage = Image.FromFile(Application.StartupPath + @"\image\menu.png");
                hambergerClickImage = Image.FromFile(Application.StartupPath + @"\image\menu_close.png");
                //펜 버튼 이미지 (250430조민석)
                penBlackDefaultImage = Image.FromFile(Application.StartupPath + @"\image\pen_color4_off.png");
                penBlackClickImage = Image.FromFile(Application.StartupPath + @"\image\pen_color4_on.png");
                penRedDefaultImage = Image.FromFile(Application.StartupPath + @"\image\pen_color5_off.png");
                penRedClickImage = Image.FromFile(Application.StartupPath + @"\image\pen_color5_on.png");
                penBlueDefaultImage = Image.FromFile(Application.StartupPath + @"\image\pen_color6_off.png");
                penBlueClickImage = Image.FromFile(Application.StartupPath + @"\image\pen_color6_on.png");
                //형광펜 버튼 이미지 (250430조민석)
                highlightYellowDefaultImage = Image.FromFile(Application.StartupPath + @"\image\highlight_color1_off.png");
                highlightYellowClickImage = Image.FromFile(Application.StartupPath + @"\image\highlight_color1_on.png");
                highlightGreenDefaultImage = Image.FromFile(Application.StartupPath + @"\image\highlight_color2_off.png");
                highlightGreenClickImage = Image.FromFile(Application.StartupPath + @"\image\highlight_color2_on.png");
                highlightPinkDefultImage = Image.FromFile(Application.StartupPath + @"\image\highlight_color3_off.png");
                highlightPinkClickImage = Image.FromFile(Application.StartupPath + @"\image\highlight_color3_on.png");
                //슬라이더 이미지(250502 조민석)
                penTrackBackImage = Image.FromFile(Application.StartupPath + @"\image\bar_black.png");
                penTrackFrontImage = Image.FromFile(Application.StartupPath + @"\image\bar_grey.png");
                penTrackPointImagge = Image.FromFile(Application.StartupPath + @"\image\btn_lever.png");
                //지우개 버튼 이미지(250507)
                EraserPrevDefaultImage = Image.FromFile(Application.StartupPath + @"\image\eraser1_off.png");
                EraserPrevClickImage = Image.FromFile(Application.StartupPath + @"\image\eraser1_press.png");
                EraserStrockDefaultImage = Image.FromFile(Application.StartupPath + @"\image\eraser2_off.png");
                EraserStrockClickImage = Image.FromFile(Application.StartupPath + @"\image\eraser2_on.png");
                Eraser1DefaultImage = Image.FromFile(Application.StartupPath + @"\image\eraser3_off.png");
                Eraser1ClickImage = Image.FromFile(Application.StartupPath + @"\image\eraser3_on.png");
                ErserAllDefaultImage = Image.FromFile(Application.StartupPath + @"\image\eraser4.png");
                ErserAllCilckImage = Image.FromFile(Application.StartupPath + @"\image\eraser4_press.png");
                goClass = Image.FromFile(Application.StartupPath + @"\image\go_class.png");
                drawForm = new DrawingForm(this);
                //툴바 리셋
                reset_toolbar();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"이미지 로딩 실패: {ex.Message}");
            }

            // 버튼 클릭 이벤트 연결(250429조민석)
            panButton.Click += Button_Click;
            highlightButton.Click += Button_Click;
            EraserButton.Click += Button_Click;
            BoardButton.Click += Button_Click;
            EleaButton.Click += Button_Click;

            // 초기 툴바 버튼들 이미지 세팅(250429조민석)
            panButton.BackgroundImage = penDefaultImage;
            highlightButton.BackgroundImage = highlightDefaultImage;
            EraserButton.BackgroundImage = eraserDefaultImage;
            BoardButton.BackgroundImage = boardDefaultImage;
            EleaButton.BackgroundImage = eleaDefaultImage;
            //이벤트 등록
            //펜굵기 조절
            penBoxBarFill.Width = penBoxHandle.Left + penBoxHandle.Width / 2;
            penBoxHandle.MouseDown += pictureBoxHandle_MouseDown;
            penBoxHandle.MouseMove += penBoxHandle_MouseMove;
            penBoxHandle.MouseUp += pictureBoxHandle_MouseUp;
            penBarBackground.MouseDown += new System.Windows.Forms.MouseEventHandler(penBarBackground_MouseDown);
            penBoxBarFill.MouseDown += penBarBackground_MouseDown;
            //형광펜 굵기 조절
            highlightBoxBarFill.Width = highlightBoxHandle.Left + highlightBoxHandle.Width / 2;
            highlightBoxHandle.MouseDown += pictureBoxHandle_MouseDown;
            highlightBoxHandle.MouseMove += highlightBoxHandle_MouseMove;
            highlightBoxHandle.MouseUp += pictureBoxHandle_MouseUp;
            highlightBarBackground.MouseDown += new System.Windows.Forms.MouseEventHandler(this.highlightBarBackground_MouseDown);
            highlightBoxBarFill.MouseDown += highlightBarBackground_MouseDown;
            EraserPrevButton.MouseDown += EraserPrevButton_MouseDown;
            EraserPrevButton.MouseUp += EraserPrevButton_MouseUp;
            EraserAllButton.MouseDown += EraserAllButton_MouseDown;
            EraserAllButton.MouseUp += EraserAllButton_MouseUp;
            //컬러박스 초기화
            PenColorBoxActive(false);
            HighlightColorBoxActive(false);
            EraserColorBoxActive(false);
            
        }

        private void reset_toolbar() 
        {
            if (drawForm != null)
            {
                //툴바 이미지 초기화
                HambergerButton.BackgroundImage = hambergerDefaultImage;
                panButton.BackgroundImage = penDefaultImage;
                highlightButton.BackgroundImage = highlightDefaultImage;
                EraserButton.BackgroundImage = eraserDefaultImage;
                if (drawForm.Visible == false || drawForm.IsDisposed || drawForm == null)
                {
                    BoardButton.BackgroundImage = boardDefaultImage;
                }
                EleaButton.BackgroundImage = eleaDefaultImage;
                ToolBarImage.Visible = false;
                panButton.Visible = false;
                highlightButton.Visible = false;
                EraserButton.Visible = false;
                BoardButton.Visible = false;
                EleaButton.Visible = false;
                //펜 색상 상자 초기화
                PenColorBoxActive(false);
                //형광펜 상자 초기화
                HighlightColorBoxActive(false);
                //지우개 상자 박스
                EraserColorBoxActive(false);
                activeButton = null;
            }
        }

        //250429조민석
        private void Button_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;

            // 버튼과 이미지 매핑 리스트
            var buttonList = new List<(Button button, Image defaultImage, Image clickedImage)>
            {
                (panButton, penDefaultImage, penClickedImage),
                (highlightButton, highlightDefaultImage, highlightClickedImage),
                (EraserButton, eraserDefaultImage, eraserClickedImage)
            };

            // 같은 버튼 다시 클릭했을 경우
            if (activeButton == clickedButton)
            {
                // 이미지 초기화
                foreach (var (button, defaultImage, _) in buttonList)
                {
                    if (button == clickedButton)
                        button.BackgroundImage = defaultImage;
                }

                // 해당 UI 비활성화
                if (clickedButton == panButton) PenColorBoxActive(false);
                if (clickedButton == highlightButton) HighlightColorBoxActive(false);
                if (clickedButton == EraserButton) EraserColorBoxActive(false);

                activeButton = null;
            }
            else
            {
                // 모든 버튼 이미지 초기화
                foreach (var (button, defaultImage, _) in buttonList)
                {
                    button.BackgroundImage = defaultImage;
                }

                // 클릭된 버튼만 이미지 변경
                foreach (var (button, _, clickedImage) in buttonList)
                {
                    if (button == clickedButton)
                        button.BackgroundImage = clickedImage;
                }

                // 모든 ColorBox 비활성화
                PenColorBoxActive(false);
                HighlightColorBoxActive(false);
                EraserColorBoxActive(false);

                // 클릭된 버튼에 해당하는 ColorBox만 활성화
                if (clickedButton == panButton) PenColorBoxActive(true);
                if (clickedButton == highlightButton) HighlightColorBoxActive(true);
                if (clickedButton == EraserButton) EraserColorBoxActive(true);

                activeButton = clickedButton;
            }
        }
        private void pictureBoxHandle_MouseDown(object sender, MouseEventArgs e)
        {
            isDragging = true;
            dragOffsetX = e.X;
        }
        //펜 슬라이더 움직이는 바 이미지 조절(250430조민석)
        private void penBoxHandle_MouseMove(object sender, MouseEventArgs e)
        {
            bool isMousePressed = Control.MouseButtons == MouseButtons.Left;
            if (!isDragging && !isMousePressed) return;

            Point cursorPosition = penBarBackground.PointToClient(Cursor.Position);
            int newLeft = cursorPosition.X - penBoxHandle.Width / 2;

            newLeft = Math.Max(0, Math.Min(newLeft, penBarBackground.Width - penBoxHandle.Width));

            // 비율 계산
            double percentage = (newLeft / (double)(penBarBackground.Width - penBoxHandle.Width)) * 100;
            int panbold = (int)Math.Round(percentage);
            Console.WriteLine(panbold);

            penBoxHandle.Left = newLeft;
            penBoxBarFill.Width = newLeft + penBoxHandle.Width / 2;

            if (drawForm != null && !drawForm.IsDisposed)
            {
                float newWidth = 1f + (19f * ((float)percentage / 100f));
                drawForm.SetPenWidth(newWidth);
            }
        }

        // 펜 슬라이더 배경 클릭으로 이동 기능 추가 (250507 조민석)
        private void penBarBackground_MouseDown(object sender, MouseEventArgs e)
        {
            MovePenSliderTo(e.X);
        }

        private void MovePenSliderTo(int mouseX)
        {
            int newLeft = mouseX - penBoxHandle.Width / 2;

            // 범위 제한
            newLeft = Math.Max(0, Math.Min(newLeft, penBarBackground.Width - penBoxHandle.Width));
            double percentage = (newLeft / (double)(penBarBackground.Width - penBoxHandle.Width)) * 100;
            int panbold = (int)Math.Round(percentage);
            Console.WriteLine(panbold);

            penBoxHandle.Left = newLeft;
            penBoxBarFill.Width = newLeft + penBoxHandle.Width / 2;

            if (drawForm != null && !drawForm.IsDisposed)
            {
                float newWidth = 1f + (19f * ((float)percentage / 100f));
                drawForm.SetPenWidth(newWidth);
            }
        }
        //형광펜 슬라이더 움직이는 바 이미지 조절(250430조민석)
        private void highlightBoxHandle_MouseMove(object sender, MouseEventArgs e)
        {
            // 마우스 왼쪽 버튼이 눌려 있거나 드래그 중일 때만 동작
            if (!isDragging && e.Button != MouseButtons.Left) return;

            int newLeft = highlightBoxHandle.Left + e.X - dragOffsetX;

            // 패널 내부로 제한
            newLeft = Math.Max(0, Math.Min(newLeft, highlightBarBackground.Width - highlightBoxHandle.Width));
            double percentage = (newLeft / (double)(highlightBarBackground.Width - highlightBoxHandle.Width)) * 100;
            int panbold = (int)Math.Round(percentage);
            Console.WriteLine(panbold);

            highlightBoxHandle.Left = newLeft;
            highlightBoxBarFill.Width = newLeft + highlightBoxHandle.Width / 2;

            if (drawForm != null && !drawForm.IsDisposed)
            {
                float newWidth = 30f + (30f * ((float)percentage / 100f)); // 30~60
                drawForm.SetPenWidth(newWidth);
            }
        }
        // 형광펜 슬라이더 배경 클릭으로 이동 기능 추가 (250507 조민석)
        private void highlightBarBackground_MouseDown(object sender, MouseEventArgs e)
        {
            MoveHighlightSliderTo(e.X);
        }
        private void MoveHighlightSliderTo(int mouseX)
        {
            int newLeft = mouseX - highlightBoxHandle.Width / 2;

            // 범위 제한
            newLeft = Math.Max(0, Math.Min(newLeft, highlightBarBackground.Width - highlightBoxHandle.Width));
            double percentage = (newLeft / (double)(highlightBarBackground.Width - highlightBoxHandle.Width)) * 100;
            int panbold = (int)Math.Round(percentage);

            highlightBoxHandle.Left = newLeft;
            highlightBoxBarFill.Width = newLeft + highlightBoxHandle.Width / 2;

            if (drawForm != null && !drawForm.IsDisposed)
            {
                float newWidth = 30f + (30f * ((float)percentage / 100f));
                drawForm.SetPenWidth(newWidth);
            }
        }
        //250430조민석
        private void pictureBoxHandle_MouseUp(object sender, MouseEventArgs e)
        {
            isDragging = false;
        }

        private void PenBoxImage_Click(object sender, EventArgs e)
        {

        }

        private void HambergerButton_Click(object sender, EventArgs e)
        {
            if(HambergerButton.BackgroundImage == hambergerClickImage)
            {
                reset_toolbar();
            }
            else
            {
                HambergerButton.BackgroundImage = hambergerClickImage;
                ToolBarImage.Visible = true;
                panButton.Visible = true;
                highlightButton.Visible = true;
                EraserButton.Visible = true;
                BoardButton.Visible = true;
                EleaButton.Visible = true;
            }

        }
        private void AppCloseButton_Click(object sender, EventArgs e)
        {
            ExitPop popup = new ExitPop();
            popup.TopLevel = true;
            popup.StartPosition = FormStartPosition.CenterScreen; // 메인폼 중앙에 위치
            popup.Show(this); // 모달 창으로 띄움: 메인폼 비활성화
            popup.Focus();
        }

        // 펜 컬러 박스 활성화/비활성화 함수
        private void PenColorBoxActive(bool isVisible)
        {
            if (isVisible)
            {
                Button openButton = new Button()
                {
                    Width = 80,
                    Height = 80,
                    BackgroundImage = goClass, // goClass는 Image 타입
                    BackgroundImageLayout = ImageLayout.Stretch, // 또는 Zoom
                    FlatStyle = FlatStyle.Flat,
                    BackColor = Color.Transparent, // 또는 필요한 배경색
                    TabStop = false,
                    Cursor = Cursors.Hand
                };

                // 테두리 제거
                openButton.FlatAppearance.BorderSize = 0;
                // hover 시 테두리 방지
                openButton.FlatAppearance.MouseOverBackColor = Color.Transparent;
                openButton.FlatAppearance.MouseDownBackColor = Color.Transparent;
                openButton.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255); // 완전 투명은 불가능하지만 최대한 티 안 나게
                PenBoxImage.Visible = true;
                PenColorBarPanel.Visible = true;
                PenBlackButton.Visible = true;
                PenRedButton.Visible = true;
                PenBlueButton.Visible = true;
                penBarBackground.Visible = true;
                penBoxBarFill.Visible = true;
                penBoxHandle.Visible = true;
            }
            else
            {
                PenBoxImage.Visible = false;
                PenColorBarPanel.Visible = false;
                PenBlackButton.Visible = false;
                PenRedButton.Visible = false;
                PenBlueButton.Visible = false;
                penBarBackground.Visible = false;
                penBoxBarFill.Visible = false;
                penBoxHandle.Visible = false;
            }
        }

        // 형광펜 컬러 박스 활성화/비활성화 함수
        private void HighlightColorBoxActive(bool isVisible)
        {
            if (isVisible)
            {
                highlightBoxImage.Visible = true;
                highlightColorBarImage.Visible = true;
                highlightYellowButton.Visible = true;
                highlighGreenButton.Visible = true;
                highlightPinkButton.Visible = true;
                highlightBarBackground.Visible = true;
                highlightBoxBarFill.Visible = true;
                highlightBoxHandle.Visible = true;
            }
            else
            {
                highlightBoxImage.Visible = false;
                highlightColorBarImage.Visible = false;
                highlightYellowButton.Visible = false;
                highlighGreenButton.Visible = false;
                highlightPinkButton.Visible = false;
                highlightBarBackground.Visible = false;
                highlightBoxBarFill.Visible = false;
                highlightBoxHandle.Visible = false;
            }
        }
        //지우개 박스 활성화/비활성화 함수
        private void EraserColorBoxActive(bool isVisible)
        {
            if (isVisible)
            {
                EraserBox.Visible = true;
                EraserButtonBox.Visible = true;
                EraserPrevButton.Visible = true;
                EraserStrockButton.Visible = true;
                EraserPixelButton.Visible = true;
                EraserAllButton.Visible = true;
            }
            else
            {
                EraserBox.Visible = false;
                EraserButtonBox.Visible = false;
                EraserPrevButton.Visible = false;
                EraserStrockButton.Visible = false;
                EraserPixelButton.Visible = false;
                EraserAllButton.Visible = false;
                EraserStrockButton.BackgroundImage = EraserStrockDefaultImage;
                EraserPixelButton.BackgroundImage = Eraser1DefaultImage;
            }
        }

        private void BoardButton_Click(object sender, EventArgs e)
        {
          /*if (drawForm == null || drawForm.IsDisposed)
            {
                drawForm = new DrawingForm();
                drawForm.PenColor = Color.Black;    // 초기값 지정
                drawForm.PenWidth = 3f;
                drawForm.Show();
            }
            else
            {
                drawForm.BringToFront();
            }*/
            if (BoardButton.BackgroundImage == boardDefaultImage)
            {
                if (drawForm == null || drawForm.IsDisposed)
                {
                    drawForm = new DrawingForm(this);
                }

                drawForm.CurrentMode = DrawMode.Pen;
                drawForm.PenColor = Color.Black;    // 초기값 지정
                drawForm.PenWidth = 3f;
                drawForm.Show();
                BoardButton.BackgroundImage = boardClickedImage;
            }
            else
            {
                BoardButton.BackgroundImage = boardDefaultImage;
                drawForm.PenColor = Color.Black;
                drawForm.Visible = false;
            }
        }
        private void DrawingForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            BoardButton.BackgroundImage = boardDefaultImage;
        }

        private void penBoxBarFill_Click(object sender, EventArgs e)
        {

        }

        private void highlightBoxHandle_Click(object sender, EventArgs e)
        {

        }

        //private void BGColorChange_Click(object sender, EventArgs e)
        //{
        //    if (drawForm != null && !drawForm.IsDisposed)
        //    {
        //        drawForm.ToggleBackgroundColor();
        //    }
        //}

        private void EraserButton_Click(object sender, EventArgs e)
        {
            if(drawForm != null)
            {

                drawForm.CurrentMode = DrawMode.EraserPixel;
                drawForm.PenWidth = 20f;
            }
        }
        // PPT 파일목록 열기 ( 250519_김혜진 )
        private void openFileList_Click(object sender, EventArgs e)
        {
            string targetFolder = Path.Combine(Application.StartupPath, "PPT");
            Directory.CreateDirectory(targetFolder);

            if (!Directory.Exists(targetFolder))
            {
                MessageBox.Show("PPT 폴더가 존재하지 않습니다:\n" + targetFolder, "오류");
                return;
            }

            string[] pptFiles = Directory.GetFiles(targetFolder, "*.ppsx");

            if (pptFiles.Length == 0)
            {
                MessageBox.Show("해당 폴더에 PPSX 파일이 없습니다.", "알림");
                return;
            }

            using (Form selectForm = new Form())
            {
                selectForm.Text = "ELEA BOARD";
                selectForm.Width = 600;
                selectForm.Height = 500;
                selectForm.StartPosition = FormStartPosition.CenterParent;
                selectForm.FormBorderStyle = FormBorderStyle.FixedDialog;
                selectForm.MaximizeBox = false;
                selectForm.BackColor = Color.White;

                Label titleLabel = new Label()
                {
                    Text = "수업용 파일을 선택하세요.",
                    Font = new Font("Segoe UI", 14, FontStyle.Bold),
                    Dock = DockStyle.Top,
                    Height = 50,
                    TextAlign = ContentAlignment.MiddleCenter,
                    ForeColor = Color.FromArgb(45, 45, 48),
                    BackColor = Color.FromArgb(240, 240, 240)
                };

                // 파일명 / 클릭(버튼) 제목 패널
                Panel headerPanel = new Panel()
                {
                    Height = 30,
                    Dock = DockStyle.Top,
                    BackColor = Color.FromArgb(240, 240, 240),
                    Padding = new Padding(10, 5, 10, 5),
                };

                Label fileNameHeader = new Label()
                {
                    Text = "수업명",
                    Font = new Font("굴림", 10, FontStyle.Bold),
                    ForeColor = Color.FromArgb(45, 45, 48),
                    TextAlign = ContentAlignment.MiddleCenter,
                    AutoSize = false,
                    Width = 400,
                    Dock = DockStyle.Left,
                };

                Label openHeader = new Label()
                {
                    Text = "클릭",
                    Font = new Font("굴림", 10, FontStyle.Bold),
                    ForeColor = Color.FromArgb(45, 45, 48),
                    TextAlign = ContentAlignment.MiddleLeft,
                    Anchor = AnchorStyles.Left,
                    AutoSize = false,
                    Width = 110,
                    Dock = DockStyle.Right,
                };

                headerPanel.Controls.Add(openHeader);
                headerPanel.Controls.Add(fileNameHeader);

                FlowLayoutPanel flowPanel = new FlowLayoutPanel()
                {
                    Dock = DockStyle.Fill,
                    AutoScroll = true,
                    Padding = new Padding(15),
                    FlowDirection = FlowDirection.TopDown,
                    WrapContents = false,
                    BackColor = Color.White
                };

                foreach (string file in pptFiles)
                {
                    string fileName = Path.GetFileName(file);

                    Panel rowPanel = new Panel()
                    {
                        Width = 530,
                        Height = 51, // 구분선 포함 높이
                        BackColor = Color.White,
                        Margin = new Padding(0, 0, 0, 0),
                    };

                    Label fileLabel = new Label()
                    {
                        Text = fileName,
                        Font = new Font("Segoe UI", 10, FontStyle.Regular),
                        AutoSize = false,
                        Width = 400,
                        Height = 50,
                        TextAlign = ContentAlignment.MiddleCenter,
                        ForeColor = Color.FromArgb(45, 45, 48),
                        Dock = DockStyle.Left,
                        Padding = new Padding(10, 0, 0, 0)
                    };

                    Button openButton = new Button()
                    {
                        Width = 80,
                        Height = 30,
                        FlatStyle = FlatStyle.Flat,
                        ForeColor = Color.FromArgb(45, 45, 48),
                        BackgroundImage = goClass,
                        Cursor = Cursors.Hand,
                        Tag = file,
                        Margin = new Padding(0),
                        Anchor = AnchorStyles.Bottom
                    };
                    openButton.FlatAppearance.BorderSize = 0;
                    openButton.FlatAppearance.BorderColor = Color.Silver;

                    // 둥근 모서리 적용
                    openButton.Paint += (s, pe) =>
                    {
                        Rectangle bounds = openButton.ClientRectangle;
                        int radius = 10;
                        using (GraphicsPath path = new GraphicsPath())
                        {
                            path.AddArc(bounds.X, bounds.Y, radius, radius, 180, 90);
                            path.AddArc(bounds.Right - radius, bounds.Y, radius, radius, 270, 90);
                            path.AddArc(bounds.Right - radius, bounds.Bottom - radius, radius, radius, 0, 90);
                            path.AddArc(bounds.X, bounds.Bottom - radius, radius, radius, 90, 90);
                            path.CloseAllFigures();
                            openButton.Region = new Region(path);
                        }
                    };

                    openButton.MouseEnter += (s, e2) =>
                    {
                        openButton.BackColor = Color.FromArgb(220, 220, 220);
                    };
                    openButton.MouseLeave += (s, e2) =>
                    {
                        openButton.BackColor = Color.FromArgb(240, 240, 240);
                    };

                    openButton.Click += (s, args) =>
                    {
                        string selectedPath = (string)((Button)s).Tag;
                        selectForm.Tag = selectedPath;
                        selectForm.DialogResult = DialogResult.OK;
                        selectForm.Close();
                    };

                    // 컨트롤 추가 및 위치 지정
                    rowPanel.Controls.Add(openButton);
                    rowPanel.Controls.Add(fileLabel);

                    openButton.Location = new Point(rowPanel.Width - openButton.Width - 20, 10);

                    // 행 하단 구분선 추가
                    Panel separator = new Panel()
                    {
                        Height = 1,
                        Width = rowPanel.Width,
                        BackColor = Color.LightGray,
                        Dock = DockStyle.Bottom,
                    };
                    rowPanel.Controls.Add(separator);

                    flowPanel.Controls.Add(rowPanel);
                }

                // 컨트롤 순서대로 폼에 추가
                selectForm.Controls.Add(flowPanel);
                selectForm.Controls.Add(headerPanel);
                selectForm.Controls.Add(titleLabel);

                if (selectForm.ShowDialog() == DialogResult.OK)
                {
                    string selectedPath = selectForm.Tag as string;

                    if (!string.IsNullOrEmpty(selectedPath))
                    {
                        MessageBox.Show("선택된 파일:\n" + selectedPath, "파일 선택됨");
                        CloseCurrentPresentation();
                        RunPowerPointSlideShow(selectedPath);

                        // PPSX 파일 열기 로직
                    }
                }
            }
        }

        private void OpenButton_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void EleaButton_Click(object sender, EventArgs e)
        {
            if(EleaButton.BackgroundImage == eleaDefaultImage)
            {
                EleaButton.BackgroundImage = eleaClickedImage;
            }
            else
            {
                EleaButton.BackgroundImage = eleaDefaultImage;
            }
        }

        private void highlightButton_Click(object sender, EventArgs e)
        {
            if (drawForm != null)
            {
                EraserColorBoxActive(false);
                PenColorBoxActive(false);
                highlightYellowButton.BackgroundImage = highlightYellowClickImage;
                highlighGreenButton.BackgroundImage = highlightGreenDefaultImage;
                highlightPinkButton.BackgroundImage = highlightPinkDefultImage;
                drawForm.CurrentMode = DrawMode.Highlighter;
                drawForm.PenColor = Color.Yellow;
                drawForm.PenWidth = 30f;
            }
        }

        private void panButton_Click(object sender, EventArgs e)
        {
            if (drawForm != null)
            {
                EraserColorBoxActive(false);
                PenBlackButton.BackgroundImage = penBlackClickImage;
                PenRedButton.BackgroundImage = penRedDefaultImage;
                PenBlueButton.BackgroundImage = penBlueDefaultImage;
                HighlightColorBoxActive(false);
                drawForm.CurrentMode = DrawMode.Pen;
                drawForm.PenColor = Color.Black;
                drawForm.PenWidth = 3f;
            }
        }

        private void btnPenBlack_Click(object sender, EventArgs e)
        {
            if(drawForm != null)
            {
                PenBlackButton.BackgroundImage = penBlackClickImage;
                PenRedButton.BackgroundImage = penRedDefaultImage;
                PenBlueButton.BackgroundImage = penBlueDefaultImage;
                drawForm.SetPenMode(Color.Black);
            }
           
        }

        private void btnPenRed_Click(object sender, EventArgs e)
        {
            if (drawForm != null)
            {
                PenRedButton.BackgroundImage = penRedClickImage;
                PenBlackButton.BackgroundImage = penBlackDefaultImage;
                PenBlueButton.BackgroundImage = penBlueDefaultImage;
                drawForm.SetPenMode(Color.Red);
            }
            
        }

        private void btnPenBlue_Click(object sender, EventArgs e)
        {
            if (drawForm != null)
            {
                PenBlueButton.BackgroundImage = penBlueClickImage;
                PenBlackButton.BackgroundImage = penBlackDefaultImage;
                PenRedButton.BackgroundImage = penRedDefaultImage;
                drawForm.SetPenMode(Color.Blue);
            }
            
        }

        private void btnHighlighterPenYellow_Click(object sender, EventArgs e)
        {
            if (drawForm != null)
            {
                highlightYellowButton.BackgroundImage = highlightYellowClickImage;
                highlighGreenButton.BackgroundImage = highlightGreenDefaultImage;
                highlightPinkButton.BackgroundImage = highlightPinkDefultImage;
                drawForm.SetHighlighterMode(Color.Yellow);
            }
            
        }

        private void btnHighlighterPenGreen_Click(object sender, EventArgs e)
        {
            if (drawForm != null)
            {
                highlighGreenButton.BackgroundImage = highlightGreenClickImage;
                highlightPinkButton.BackgroundImage = highlightPinkDefultImage;
                highlightYellowButton.BackgroundImage = highlightYellowDefaultImage;
                drawForm.SetHighlighterMode(Color.Green);
            }
        }

        private void btnHighlighterPenPink_Click(object sender, EventArgs e)
        {
            if (drawForm != null)
            {
                highlightPinkButton.BackgroundImage = highlightPinkClickImage;
                highlighGreenButton.BackgroundImage = highlightGreenDefaultImage;
                highlightYellowButton.BackgroundImage = highlightYellowDefaultImage;
                drawForm.SetHighlighterMode(Color.Pink);
            }
            
        }

        private void EraserPrevButton_Click(object sender, EventArgs e)
        {
            EraserStrockButton.BackgroundImage = EraserStrockDefaultImage;
            EraserPixelButton.BackgroundImage = Eraser1DefaultImage;
        }

        private void EraserPrevButton_MouseDown(object sender, MouseEventArgs e)
        {
            EraserPrevButton.BackgroundImage = EraserPrevClickImage; // 클릭 상태 이미지
        }

        private void EraserPrevButton_MouseUp(object sender, MouseEventArgs e)
        {
            EraserPrevButton.BackgroundImage = EraserPrevDefaultImage; // 기본 이미지로 복귀

            if (drawForm != null && !drawForm.IsDisposed)
            {
                drawForm.Undo(); // Undo 실행
            }
        }


        private void EraserStrockButton_Click(object sender, EventArgs e)
        {
            if (drawForm != null)
            {
                if(EraserStrockButton.BackgroundImage == EraserStrockDefaultImage)
                {
                    drawForm.SetEraserMode(DrawMode.EraserStroke);
                    EraserPixelButton.BackgroundImage = Eraser1DefaultImage;
                    EraserStrockButton.BackgroundImage = EraserStrockClickImage;
                }
                else
                {
                    EraserStrockButton.BackgroundImage = EraserStrockDefaultImage;
                }
            }
            
        }

        private void EraserPixelButton_Click(object sender, EventArgs e)
        {
            if (drawForm != null)
            {
                if (EraserPixelButton.BackgroundImage == Eraser1DefaultImage)
                {
                    drawForm.SetEraserMode(DrawMode.EraserPixel);
                    EraserPixelButton.BackgroundImage = Eraser1ClickImage;
                    EraserStrockButton.BackgroundImage = EraserStrockDefaultImage;
                }
                else
                {
                    EraserPixelButton.BackgroundImage = Eraser1DefaultImage;
                }
            }
        }

        private void EraserAllButton_Click(object sender, EventArgs e)
        {
            if (drawForm != null)
            {
                EraserStrockButton.BackgroundImage = EraserStrockDefaultImage;
                EraserPixelButton.BackgroundImage = Eraser1DefaultImage;
                drawForm.EraserAll();
            }
        }
        private void EraserAllButton_MouseDown(object sender, MouseEventArgs e)
        {
            EraserAllButton.BackgroundImage = ErserAllCilckImage;
        }

        private void EraserAllButton_MouseUp(object sender, MouseEventArgs e)
        {
            EraserAllButton.BackgroundImage = ErserAllDefaultImage; 

            if (drawForm != null && !drawForm.IsDisposed)
            {
                drawForm.Undo(); // Undo 실행
            }
        }
       
        public void BoardReset()
        {
            BoardButton.BackgroundImage = boardDefaultImage;
        }

        public class FileListResponse
        {
            public List<FileItem> fileList { get; set; }
        }

        public class FileItem
        {
            public int index { get; set; }
            public string filename { get; set; }
        }
    }
}
