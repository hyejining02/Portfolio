using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace ELEA_BOARD
{
    public partial class DrawingForm : Form
    {
        public Color PenColor { get; set; } = Color.Black;
        public float PenWidth { get; set; } = 3f;
        public DrawMode CurrentMode { get; set; } = DrawMode.Pen;

        // 그리기 ( 비트맵 -> 스트로크 방식으로 변경 )
        private List<Stroke> strokes = new List<Stroke>();
        private Stroke currentStroke;

        private Color currentBackgroundColor = Color.White;
        private Color pixelEraserColor = Color.White;
        private MainForm mainForm;

        public DrawingForm(MainForm mainForm)
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            this.Text = "판서";   // 제목표시줄에 나오는 이름
            this.FormBorderStyle = FormBorderStyle.Sizable;     // 윈도우 기본 형태로 설정
            this.WindowState = FormWindowState.Normal;          // FixedDialog
            this.StartPosition = FormStartPosition.CenterScreen;    // 전체화면이 아닌 일반크기
            this.BackColor = Color.White;   // 배경색 및 초기캔버스 설정
            this.TopMost = true;
            this.mainForm = mainForm; 
            this.FormClosed += DrawingForm_FormClosed;
            this.MinimizeBox = false;
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            
            if (CurrentMode == DrawMode.EraserStroke)
            {
                EraseStrokeAt(e.Location);
                currentStroke = null;
                return;
            }

            currentStroke = new Stroke
            {
                Points = new List<Point> { e.Location },
                Color = (CurrentMode == DrawMode.Highlighter) ? Color.FromArgb(70, PenColor) :
                       (CurrentMode == DrawMode.EraserPixel ? currentBackgroundColor : PenColor),
                Width = PenWidth,
                Mode = CurrentMode
            };
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if ( e.Button == MouseButtons.Left && currentStroke != null )
            {
                if (CurrentMode == DrawMode.EraserStroke)
                    return;

                currentStroke.Points.Add(e.Location);
                Invalidate();
            }
        }

        private void DrawingForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            mainForm.BoardReset();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            if ( e.Button == MouseButtons.Left && currentStroke != null )
            {
                if (CurrentMode == DrawMode.EraserStroke)
                    return;

                strokes.Add(currentStroke);
                currentStroke = null;
                Invalidate();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            foreach ( var stroke in strokes )
            {
                using (Pen pen = new Pen ( stroke.Color, stroke.Width)
                {
                    StartCap = LineCap.Round,
                    EndCap = LineCap.Round
                })
                {
                    if (stroke.Points.Count > 1)
                        e.Graphics.DrawLines(pen, stroke.Points.ToArray());
                }
            }

            if (currentStroke != null && currentStroke.Points.Count > 1)
            {
                using (Pen pen = new Pen(currentStroke.Color, currentStroke.Width)
                {
                    StartCap = LineCap.Round,
                    EndCap = LineCap.Round
                })
                {
                    e.Graphics.DrawLines(pen, currentStroke.Points.ToArray());
                }
            }
        }

        public void SetPenWidth ( float width )
        {
            PenWidth = width;
        }

        public void SetPenMode(Color color)
        {
            CurrentMode = DrawMode.Pen;
            PenColor = color;
        }

        public void SetHighlighterMode(Color color)
        {
            CurrentMode = DrawMode.Highlighter;
            PenColor = color;
        }

        public void SetEraserMode(DrawMode eraserMode)
        {
            CurrentMode = eraserMode;
        }

        public void Undo()
        {
            if (strokes.Count > 0)
            {
                strokes.RemoveAt(strokes.Count - 1);
                Invalidate();
            }
        }

        public void SetEraserPixelMode()
        {
            CurrentMode = DrawMode.EraserPixel;
            PenColor = pixelEraserColor;
        }

        public void EraseStrokeAt(Point location)
        {
            for ( int i = strokes.Count - 1; i >= 0; i-- )
            {
                foreach ( var pt in strokes[i].Points )
                {
                    if ( Distance(pt, location) <= 10f )
                    {
                        strokes.RemoveAt(i);
                        Invalidate();
                        return;
                    }
                }
            }
        }

        public void EraserAll()
        {
            strokes.Clear();
            Invalidate();
        }

        private float Distance ( Point a, Point b )
        {
            float dx = a.X - b.X;
            float dy = a.Y - b.Y;
            return ( float ) Math.Sqrt( dx * dx + dy * dy );
        }

        private void DrawingForm_Load(object sender, EventArgs e){}

        public void ToggleBackgroundColor()
        {
            currentBackgroundColor = (currentBackgroundColor == Color.White) ? Color.Green : Color.White;
            pixelEraserColor = currentBackgroundColor;
            this.BackColor = currentBackgroundColor;

            // 픽셀지우개로 그린 stroke들도 새 배경색으로 적용
            foreach ( var stroke in strokes )
            {
                if (stroke.Mode == DrawMode.EraserPixel)
                    stroke.Color = pixelEraserColor;
            }

            Invalidate(); // 다시 그리기
        }

        private void BGColorChange_Click(object sender, EventArgs e)
        {
            ToggleBackgroundColor();
        }
    }

    public enum DrawMode
    {
        Pen,
        Highlighter,
        EraserPixel,
        EraserStroke,
        EraserAll
    }

    public class Stroke
    {
        public List<Point> Points { get; set; }
        public Color Color { get; set; }
        public float Width { get; set; }
        public DrawMode Mode { get; set; }
    }
}
