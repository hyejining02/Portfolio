using System;

namespace ELEA_BOARD
{
    partial class MainForm
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.openFileList = new System.Windows.Forms.Button();
            this.highlightBarBackground = new System.Windows.Forms.Panel();
            this.highlightBoxHandle = new System.Windows.Forms.PictureBox();
            this.highlightBoxBarFill = new System.Windows.Forms.PictureBox();
            this.penBarBackground = new System.Windows.Forms.Panel();
            this.penBoxHandle = new System.Windows.Forms.PictureBox();
            this.penBoxBarFill = new System.Windows.Forms.PictureBox();
            this.EraserAllButton = new System.Windows.Forms.Button();
            this.EraserButtonBox = new System.Windows.Forms.Panel();
            this.EraserPixelButton = new System.Windows.Forms.Button();
            this.EraserStrockButton = new System.Windows.Forms.Button();
            this.EraserPrevButton = new System.Windows.Forms.Button();
            this.EraserBox = new System.Windows.Forms.PictureBox();
            this.HambergerButton = new System.Windows.Forms.Button();
            this.highlightColorBarImage = new System.Windows.Forms.Panel();
            this.highlightPinkButton = new System.Windows.Forms.Button();
            this.highlighGreenButton = new System.Windows.Forms.Button();
            this.highlightYellowButton = new System.Windows.Forms.Button();
            this.AppCloseButton = new System.Windows.Forms.Button();
            this.PenColorBarPanel = new System.Windows.Forms.Panel();
            this.PenBlueButton = new System.Windows.Forms.Button();
            this.PenRedButton = new System.Windows.Forms.Button();
            this.PenBlackButton = new System.Windows.Forms.Button();
            this.PenBoxImage = new System.Windows.Forms.PictureBox();
            this.EleaButton = new System.Windows.Forms.Button();
            this.BoardButton = new System.Windows.Forms.Button();
            this.EraserButton = new System.Windows.Forms.Button();
            this.highlightButton = new System.Windows.Forms.Button();
            this.panButton = new System.Windows.Forms.Button();
            this.ToolBarImage = new System.Windows.Forms.PictureBox();
            this.highlightBoxImage = new System.Windows.Forms.PictureBox();
            this.highlightBarBackground.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.highlightBoxHandle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.highlightBoxBarFill)).BeginInit();
            this.penBarBackground.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.penBoxHandle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.penBoxBarFill)).BeginInit();
            this.EraserButtonBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.EraserBox)).BeginInit();
            this.highlightColorBarImage.SuspendLayout();
            this.PenColorBarPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PenBoxImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ToolBarImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.highlightBoxImage)).BeginInit();
            this.SuspendLayout();
            // 
            // openFileList
            // 
            this.openFileList.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.openFileList.BackColor = System.Drawing.Color.Transparent;
            this.openFileList.BackgroundImage = global::ELEA_BOARD.Properties.Resources.search_file;
            this.openFileList.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.openFileList.Cursor = System.Windows.Forms.Cursors.Hand;
            this.openFileList.FlatAppearance.BorderSize = 0;
            this.openFileList.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.openFileList.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.openFileList.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.openFileList.Location = new System.Drawing.Point(-9, -6);
            this.openFileList.Name = "openFileList";
            this.openFileList.Size = new System.Drawing.Size(82, 82);
            this.openFileList.TabIndex = 26;
            this.openFileList.UseVisualStyleBackColor = false;
            this.openFileList.Click += new System.EventHandler(this.openFileList_Click);
            // 
            // highlightBarBackground
            // 
            this.highlightBarBackground.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.highlightBarBackground.BackgroundImage = global::ELEA_BOARD.Properties.Resources.bar_grey;
            this.highlightBarBackground.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.highlightBarBackground.Controls.Add(this.highlightBoxHandle);
            this.highlightBarBackground.Controls.Add(this.highlightBoxBarFill);
            this.highlightBarBackground.Location = new System.Drawing.Point(1599, 405);
            this.highlightBarBackground.Name = "highlightBarBackground";
            this.highlightBarBackground.Size = new System.Drawing.Size(162, 27);
            this.highlightBarBackground.TabIndex = 25;
            // 
            // highlightBoxHandle
            // 
            this.highlightBoxHandle.BackgroundImage = global::ELEA_BOARD.Properties.Resources.btn_lever;
            this.highlightBoxHandle.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.highlightBoxHandle.Location = new System.Drawing.Point(0, -4);
            this.highlightBoxHandle.Name = "highlightBoxHandle";
            this.highlightBoxHandle.Size = new System.Drawing.Size(30, 38);
            this.highlightBoxHandle.TabIndex = 23;
            this.highlightBoxHandle.TabStop = false;
            this.highlightBoxHandle.Click += new System.EventHandler(this.highlightBoxHandle_Click);
            // 
            // highlightBoxBarFill
            // 
            this.highlightBoxBarFill.BackgroundImage = global::ELEA_BOARD.Properties.Resources.bar_black;
            this.highlightBoxBarFill.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.highlightBoxBarFill.Location = new System.Drawing.Point(0, 0);
            this.highlightBoxBarFill.Name = "highlightBoxBarFill";
            this.highlightBoxBarFill.Size = new System.Drawing.Size(162, 27);
            this.highlightBoxBarFill.TabIndex = 24;
            this.highlightBoxBarFill.TabStop = false;
            // 
            // penBarBackground
            // 
            this.penBarBackground.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.penBarBackground.BackColor = System.Drawing.Color.Transparent;
            this.penBarBackground.BackgroundImage = global::ELEA_BOARD.Properties.Resources.bar_grey;
            this.penBarBackground.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.penBarBackground.Controls.Add(this.penBoxHandle);
            this.penBarBackground.Controls.Add(this.penBoxBarFill);
            this.penBarBackground.Location = new System.Drawing.Point(1598, 327);
            this.penBarBackground.Name = "penBarBackground";
            this.penBarBackground.Size = new System.Drawing.Size(162, 26);
            this.penBarBackground.TabIndex = 22;
            // 
            // penBoxHandle
            // 
            this.penBoxHandle.BackColor = System.Drawing.Color.Transparent;
            this.penBoxHandle.BackgroundImage = global::ELEA_BOARD.Properties.Resources.btn_lever;
            this.penBoxHandle.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.penBoxHandle.Location = new System.Drawing.Point(0, -9);
            this.penBoxHandle.Name = "penBoxHandle";
            this.penBoxHandle.Size = new System.Drawing.Size(30, 46);
            this.penBoxHandle.TabIndex = 23;
            this.penBoxHandle.TabStop = false;
            // 
            // penBoxBarFill
            // 
            this.penBoxBarFill.BackColor = System.Drawing.Color.Transparent;
            this.penBoxBarFill.BackgroundImage = global::ELEA_BOARD.Properties.Resources.bar_black;
            this.penBoxBarFill.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.penBoxBarFill.Location = new System.Drawing.Point(0, -1);
            this.penBoxBarFill.Name = "penBoxBarFill";
            this.penBoxBarFill.Size = new System.Drawing.Size(162, 27);
            this.penBoxBarFill.TabIndex = 0;
            this.penBoxBarFill.TabStop = false;
            this.penBoxBarFill.Click += new System.EventHandler(this.penBoxBarFill_Click);
            // 
            // EraserAllButton
            // 
            this.EraserAllButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.EraserAllButton.BackgroundImage = global::ELEA_BOARD.Properties.Resources.eraser4;
            this.EraserAllButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.EraserAllButton.FlatAppearance.BorderSize = 0;
            this.EraserAllButton.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.EraserAllButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.EraserAllButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.EraserAllButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.EraserAllButton.Location = new System.Drawing.Point(1591, 482);
            this.EraserAllButton.Name = "EraserAllButton";
            this.EraserAllButton.Size = new System.Drawing.Size(182, 39);
            this.EraserAllButton.TabIndex = 17;
            this.EraserAllButton.UseVisualStyleBackColor = true;
            this.EraserAllButton.Click += new System.EventHandler(this.EraserAllButton_Click);
            // 
            // EraserButtonBox
            // 
            this.EraserButtonBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.EraserButtonBox.BackColor = System.Drawing.Color.Transparent;
            this.EraserButtonBox.BackgroundImage = global::ELEA_BOARD.Properties.Resources.color_bar;
            this.EraserButtonBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.EraserButtonBox.Controls.Add(this.EraserPixelButton);
            this.EraserButtonBox.Controls.Add(this.EraserStrockButton);
            this.EraserButtonBox.Controls.Add(this.EraserPrevButton);
            this.EraserButtonBox.ForeColor = System.Drawing.Color.Transparent;
            this.EraserButtonBox.Location = new System.Drawing.Point(1600, 417);
            this.EraserButtonBox.Name = "EraserButtonBox";
            this.EraserButtonBox.Size = new System.Drawing.Size(164, 56);
            this.EraserButtonBox.TabIndex = 21;
            // 
            // EraserPixelButton
            // 
            this.EraserPixelButton.BackColor = System.Drawing.Color.Transparent;
            this.EraserPixelButton.BackgroundImage = global::ELEA_BOARD.Properties.Resources.eraser3_off;
            this.EraserPixelButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.EraserPixelButton.FlatAppearance.BorderSize = 0;
            this.EraserPixelButton.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.EraserPixelButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.EraserPixelButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.EraserPixelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.EraserPixelButton.Location = new System.Drawing.Point(111, 9);
            this.EraserPixelButton.Name = "EraserPixelButton";
            this.EraserPixelButton.Size = new System.Drawing.Size(42, 41);
            this.EraserPixelButton.TabIndex = 2;
            this.EraserPixelButton.UseVisualStyleBackColor = false;
            this.EraserPixelButton.Click += new System.EventHandler(this.EraserPixelButton_Click);
            // 
            // EraserStrockButton
            // 
            this.EraserStrockButton.BackColor = System.Drawing.Color.Transparent;
            this.EraserStrockButton.BackgroundImage = global::ELEA_BOARD.Properties.Resources.eraser2_off;
            this.EraserStrockButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.EraserStrockButton.FlatAppearance.BorderSize = 0;
            this.EraserStrockButton.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.EraserStrockButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.EraserStrockButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.EraserStrockButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.EraserStrockButton.Location = new System.Drawing.Point(60, 9);
            this.EraserStrockButton.Name = "EraserStrockButton";
            this.EraserStrockButton.Size = new System.Drawing.Size(42, 41);
            this.EraserStrockButton.TabIndex = 1;
            this.EraserStrockButton.UseVisualStyleBackColor = false;
            this.EraserStrockButton.Click += new System.EventHandler(this.EraserStrockButton_Click);
            // 
            // EraserPrevButton
            // 
            this.EraserPrevButton.BackColor = System.Drawing.Color.Transparent;
            this.EraserPrevButton.BackgroundImage = global::ELEA_BOARD.Properties.Resources.eraser1_off;
            this.EraserPrevButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.EraserPrevButton.FlatAppearance.BorderSize = 0;
            this.EraserPrevButton.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.EraserPrevButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.EraserPrevButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.EraserPrevButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.EraserPrevButton.Location = new System.Drawing.Point(10, 9);
            this.EraserPrevButton.Name = "EraserPrevButton";
            this.EraserPrevButton.Size = new System.Drawing.Size(42, 41);
            this.EraserPrevButton.TabIndex = 0;
            this.EraserPrevButton.UseVisualStyleBackColor = false;
            this.EraserPrevButton.Click += new System.EventHandler(this.EraserPrevButton_Click);
            // 
            // EraserBox
            // 
            this.EraserBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.EraserBox.BackColor = System.Drawing.Color.Transparent;
            this.EraserBox.BackgroundImage = global::ELEA_BOARD.Properties.Resources.color_menu_bar;
            this.EraserBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.EraserBox.Location = new System.Drawing.Point(1575, 405);
            this.EraserBox.Name = "EraserBox";
            this.EraserBox.Size = new System.Drawing.Size(215, 135);
            this.EraserBox.TabIndex = 20;
            this.EraserBox.TabStop = false;
            // 
            // HambergerButton
            // 
            this.HambergerButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.HambergerButton.BackColor = System.Drawing.Color.Transparent;
            this.HambergerButton.BackgroundImage = global::ELEA_BOARD.Properties.Resources.menu;
            this.HambergerButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.HambergerButton.FlatAppearance.BorderSize = 0;
            this.HambergerButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.HambergerButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.HambergerButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.HambergerButton.ForeColor = System.Drawing.Color.Transparent;
            this.HambergerButton.Location = new System.Drawing.Point(1814, 116);
            this.HambergerButton.Name = "HambergerButton";
            this.HambergerButton.Size = new System.Drawing.Size(85, 80);
            this.HambergerButton.TabIndex = 18;
            this.HambergerButton.UseVisualStyleBackColor = false;
            this.HambergerButton.Click += new System.EventHandler(this.HambergerButton_Click);
            // 
            // highlightColorBarImage
            // 
            this.highlightColorBarImage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.highlightColorBarImage.BackColor = System.Drawing.Color.Transparent;
            this.highlightColorBarImage.BackgroundImage = global::ELEA_BOARD.Properties.Resources.color_bar;
            this.highlightColorBarImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.highlightColorBarImage.Controls.Add(this.highlightPinkButton);
            this.highlightColorBarImage.Controls.Add(this.highlighGreenButton);
            this.highlightColorBarImage.Controls.Add(this.highlightYellowButton);
            this.highlightColorBarImage.ForeColor = System.Drawing.Color.Transparent;
            this.highlightColorBarImage.Location = new System.Drawing.Point(1599, 339);
            this.highlightColorBarImage.Name = "highlightColorBarImage";
            this.highlightColorBarImage.Size = new System.Drawing.Size(164, 56);
            this.highlightColorBarImage.TabIndex = 18;
            // 
            // highlightPinkButton
            // 
            this.highlightPinkButton.BackColor = System.Drawing.Color.Transparent;
            this.highlightPinkButton.BackgroundImage = global::ELEA_BOARD.Properties.Resources.highlight_color3_off;
            this.highlightPinkButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.highlightPinkButton.FlatAppearance.BorderSize = 0;
            this.highlightPinkButton.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.highlightPinkButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.highlightPinkButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.highlightPinkButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.highlightPinkButton.Location = new System.Drawing.Point(111, 9);
            this.highlightPinkButton.Name = "highlightPinkButton";
            this.highlightPinkButton.Size = new System.Drawing.Size(42, 41);
            this.highlightPinkButton.TabIndex = 2;
            this.highlightPinkButton.UseVisualStyleBackColor = false;
            this.highlightPinkButton.Click += new System.EventHandler(this.btnHighlighterPenPink_Click);
            // 
            // highlighGreenButton
            // 
            this.highlighGreenButton.BackColor = System.Drawing.Color.Transparent;
            this.highlighGreenButton.BackgroundImage = global::ELEA_BOARD.Properties.Resources.highlight_color2_off;
            this.highlighGreenButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.highlighGreenButton.FlatAppearance.BorderSize = 0;
            this.highlighGreenButton.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.highlighGreenButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.highlighGreenButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.highlighGreenButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.highlighGreenButton.Location = new System.Drawing.Point(60, 9);
            this.highlighGreenButton.Name = "highlighGreenButton";
            this.highlighGreenButton.Size = new System.Drawing.Size(42, 41);
            this.highlighGreenButton.TabIndex = 1;
            this.highlighGreenButton.UseVisualStyleBackColor = false;
            this.highlighGreenButton.Click += new System.EventHandler(this.btnHighlighterPenGreen_Click);
            // 
            // highlightYellowButton
            // 
            this.highlightYellowButton.BackColor = System.Drawing.Color.Transparent;
            this.highlightYellowButton.BackgroundImage = global::ELEA_BOARD.Properties.Resources.highlight_color1_off;
            this.highlightYellowButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.highlightYellowButton.FlatAppearance.BorderSize = 0;
            this.highlightYellowButton.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.highlightYellowButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.highlightYellowButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.highlightYellowButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.highlightYellowButton.Location = new System.Drawing.Point(10, 9);
            this.highlightYellowButton.Name = "highlightYellowButton";
            this.highlightYellowButton.Size = new System.Drawing.Size(42, 41);
            this.highlightYellowButton.TabIndex = 0;
            this.highlightYellowButton.UseVisualStyleBackColor = false;
            this.highlightYellowButton.Click += new System.EventHandler(this.btnHighlighterPenYellow_Click);
            // 
            // AppCloseButton
            // 
            this.AppCloseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.AppCloseButton.BackColor = System.Drawing.Color.Transparent;
            this.AppCloseButton.BackgroundImage = global::ELEA_BOARD.Properties.Resources.exit_ver2;
            this.AppCloseButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.AppCloseButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.AppCloseButton.FlatAppearance.BorderSize = 0;
            this.AppCloseButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.AppCloseButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.AppCloseButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AppCloseButton.Location = new System.Drawing.Point(1843, -3);
            this.AppCloseButton.Name = "AppCloseButton";
            this.AppCloseButton.Size = new System.Drawing.Size(63, 68);
            this.AppCloseButton.TabIndex = 17;
            this.AppCloseButton.UseVisualStyleBackColor = false;
            this.AppCloseButton.Click += new System.EventHandler(this.AppCloseButton_Click);
            // 
            // PenColorBarPanel
            // 
            this.PenColorBarPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.PenColorBarPanel.BackColor = System.Drawing.Color.Transparent;
            this.PenColorBarPanel.BackgroundImage = global::ELEA_BOARD.Properties.Resources.color_bar;
            this.PenColorBarPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.PenColorBarPanel.Controls.Add(this.PenBlueButton);
            this.PenColorBarPanel.Controls.Add(this.PenRedButton);
            this.PenColorBarPanel.Controls.Add(this.PenBlackButton);
            this.PenColorBarPanel.ForeColor = System.Drawing.Color.Transparent;
            this.PenColorBarPanel.Location = new System.Drawing.Point(1598, 256);
            this.PenColorBarPanel.Name = "PenColorBarPanel";
            this.PenColorBarPanel.Size = new System.Drawing.Size(164, 56);
            this.PenColorBarPanel.TabIndex = 13;
            // 
            // PenBlueButton
            // 
            this.PenBlueButton.BackColor = System.Drawing.Color.Transparent;
            this.PenBlueButton.BackgroundImage = global::ELEA_BOARD.Properties.Resources.pen_color6_off;
            this.PenBlueButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.PenBlueButton.FlatAppearance.BorderSize = 0;
            this.PenBlueButton.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.PenBlueButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.PenBlueButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.PenBlueButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PenBlueButton.Location = new System.Drawing.Point(111, 9);
            this.PenBlueButton.Name = "PenBlueButton";
            this.PenBlueButton.Size = new System.Drawing.Size(42, 41);
            this.PenBlueButton.TabIndex = 2;
            this.PenBlueButton.UseVisualStyleBackColor = false;
            this.PenBlueButton.Click += new System.EventHandler(this.btnPenBlue_Click);
            // 
            // PenRedButton
            // 
            this.PenRedButton.BackColor = System.Drawing.Color.Transparent;
            this.PenRedButton.BackgroundImage = global::ELEA_BOARD.Properties.Resources.pen_color5_off;
            this.PenRedButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.PenRedButton.FlatAppearance.BorderSize = 0;
            this.PenRedButton.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.PenRedButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.PenRedButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.PenRedButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PenRedButton.Location = new System.Drawing.Point(60, 9);
            this.PenRedButton.Name = "PenRedButton";
            this.PenRedButton.Size = new System.Drawing.Size(42, 41);
            this.PenRedButton.TabIndex = 1;
            this.PenRedButton.UseVisualStyleBackColor = false;
            this.PenRedButton.Click += new System.EventHandler(this.btnPenRed_Click);
            // 
            // PenBlackButton
            // 
            this.PenBlackButton.BackColor = System.Drawing.Color.Transparent;
            this.PenBlackButton.BackgroundImage = global::ELEA_BOARD.Properties.Resources.pen_color4_off;
            this.PenBlackButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.PenBlackButton.FlatAppearance.BorderSize = 0;
            this.PenBlackButton.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.PenBlackButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.PenBlackButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.PenBlackButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PenBlackButton.Location = new System.Drawing.Point(10, 9);
            this.PenBlackButton.Name = "PenBlackButton";
            this.PenBlackButton.Size = new System.Drawing.Size(42, 41);
            this.PenBlackButton.TabIndex = 0;
            this.PenBlackButton.UseVisualStyleBackColor = false;
            this.PenBlackButton.Click += new System.EventHandler(this.btnPenBlack_Click);
            // 
            // PenBoxImage
            // 
            this.PenBoxImage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.PenBoxImage.BackColor = System.Drawing.Color.Transparent;
            this.PenBoxImage.BackgroundImage = global::ELEA_BOARD.Properties.Resources.color_menu_bar;
            this.PenBoxImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.PenBoxImage.Location = new System.Drawing.Point(1573, 244);
            this.PenBoxImage.Name = "PenBoxImage";
            this.PenBoxImage.Size = new System.Drawing.Size(215, 135);
            this.PenBoxImage.TabIndex = 12;
            this.PenBoxImage.TabStop = false;
            this.PenBoxImage.Click += new System.EventHandler(this.PenBoxImage_Click);
            // 
            // EleaButton
            // 
            this.EleaButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.EleaButton.BackColor = System.Drawing.Color.Transparent;
            this.EleaButton.BackgroundImage = global::ELEA_BOARD.Properties.Resources.menu_elea_off;
            this.EleaButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.EleaButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.EleaButton.FlatAppearance.BorderSize = 0;
            this.EleaButton.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.EleaButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.EleaButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.EleaButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.EleaButton.Location = new System.Drawing.Point(1825, 565);
            this.EleaButton.Name = "EleaButton";
            this.EleaButton.Size = new System.Drawing.Size(76, 76);
            this.EleaButton.TabIndex = 11;
            this.EleaButton.UseVisualStyleBackColor = false;
            this.EleaButton.Click += new System.EventHandler(this.EleaButton_Click);
            // 
            // BoardButton
            // 
            this.BoardButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.BoardButton.BackColor = System.Drawing.Color.Transparent;
            this.BoardButton.BackgroundImage = global::ELEA_BOARD.Properties.Resources.menu_board_off;
            this.BoardButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.BoardButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BoardButton.FlatAppearance.BorderSize = 0;
            this.BoardButton.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.BoardButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.BoardButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.BoardButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BoardButton.Location = new System.Drawing.Point(1825, 483);
            this.BoardButton.Name = "BoardButton";
            this.BoardButton.Size = new System.Drawing.Size(76, 76);
            this.BoardButton.TabIndex = 10;
            this.BoardButton.UseVisualStyleBackColor = false;
            this.BoardButton.Click += new System.EventHandler(this.BoardButton_Click);
            // 
            // EraserButton
            // 
            this.EraserButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.EraserButton.BackColor = System.Drawing.Color.Transparent;
            this.EraserButton.BackgroundImage = global::ELEA_BOARD.Properties.Resources.menu_eraser_off;
            this.EraserButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.EraserButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.EraserButton.FlatAppearance.BorderSize = 0;
            this.EraserButton.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.EraserButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.EraserButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.EraserButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.EraserButton.Location = new System.Drawing.Point(1825, 401);
            this.EraserButton.Name = "EraserButton";
            this.EraserButton.Size = new System.Drawing.Size(76, 76);
            this.EraserButton.TabIndex = 9;
            this.EraserButton.UseVisualStyleBackColor = false;
            this.EraserButton.Click += new System.EventHandler(this.EraserButton_Click);
            // 
            // highlightButton
            // 
            this.highlightButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.highlightButton.BackColor = System.Drawing.Color.Transparent;
            this.highlightButton.BackgroundImage = global::ELEA_BOARD.Properties.Resources.menu_highlight_off;
            this.highlightButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.highlightButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.highlightButton.FlatAppearance.BorderSize = 0;
            this.highlightButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.highlightButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.highlightButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.highlightButton.Location = new System.Drawing.Point(1825, 316);
            this.highlightButton.Name = "highlightButton";
            this.highlightButton.Size = new System.Drawing.Size(76, 76);
            this.highlightButton.TabIndex = 8;
            this.highlightButton.UseVisualStyleBackColor = false;
            this.highlightButton.Click += new System.EventHandler(this.highlightButton_Click);
            // 
            // panButton
            // 
            this.panButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panButton.BackColor = System.Drawing.Color.Transparent;
            this.panButton.BackgroundImage = global::ELEA_BOARD.Properties.Resources.menu_pen_off;
            this.panButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.panButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panButton.FlatAppearance.BorderSize = 0;
            this.panButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.panButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.panButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.panButton.Location = new System.Drawing.Point(1825, 227);
            this.panButton.Name = "panButton";
            this.panButton.Size = new System.Drawing.Size(76, 76);
            this.panButton.TabIndex = 7;
            this.panButton.UseVisualStyleBackColor = false;
            this.panButton.Click += new System.EventHandler(this.panButton_Click);
            // 
            // ToolBarImage
            // 
            this.ToolBarImage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ToolBarImage.BackColor = System.Drawing.Color.Transparent;
            this.ToolBarImage.BackgroundImage = global::ELEA_BOARD.Properties.Resources.menubar;
            this.ToolBarImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ToolBarImage.Location = new System.Drawing.Point(1797, 201);
            this.ToolBarImage.Name = "ToolBarImage";
            this.ToolBarImage.Size = new System.Drawing.Size(107, 490);
            this.ToolBarImage.TabIndex = 0;
            this.ToolBarImage.TabStop = false;
            // 
            // highlightBoxImage
            // 
            this.highlightBoxImage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.highlightBoxImage.BackColor = System.Drawing.Color.Transparent;
            this.highlightBoxImage.BackgroundImage = global::ELEA_BOARD.Properties.Resources.color_menu_bar;
            this.highlightBoxImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.highlightBoxImage.Location = new System.Drawing.Point(1574, 327);
            this.highlightBoxImage.Name = "highlightBoxImage";
            this.highlightBoxImage.Size = new System.Drawing.Size(215, 135);
            this.highlightBoxImage.TabIndex = 17;
            this.highlightBoxImage.TabStop = false;
            // 
            // MainForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(1904, 1041);
            this.Controls.Add(this.openFileList);
            this.Controls.Add(this.highlightBarBackground);
            this.Controls.Add(this.penBarBackground);
            this.Controls.Add(this.EraserAllButton);
            this.Controls.Add(this.EraserButtonBox);
            this.Controls.Add(this.EraserBox);
            this.Controls.Add(this.HambergerButton);
            this.Controls.Add(this.highlightColorBarImage);
            this.Controls.Add(this.AppCloseButton);
            this.Controls.Add(this.PenColorBarPanel);
            this.Controls.Add(this.PenBoxImage);
            this.Controls.Add(this.EleaButton);
            this.Controls.Add(this.BoardButton);
            this.Controls.Add(this.EraserButton);
            this.Controls.Add(this.highlightButton);
            this.Controls.Add(this.panButton);
            this.Controls.Add(this.ToolBarImage);
            this.Controls.Add(this.highlightBoxImage);
            this.DoubleBuffered = true;
            this.Name = "MainForm";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.highlightBarBackground.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.highlightBoxHandle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.highlightBoxBarFill)).EndInit();
            this.penBarBackground.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.penBoxHandle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.penBoxBarFill)).EndInit();
            this.EraserButtonBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.EraserBox)).EndInit();
            this.highlightColorBarImage.ResumeLayout(false);
            this.PenColorBarPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PenBoxImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ToolBarImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.highlightBoxImage)).EndInit();
            this.ResumeLayout(false);

        }


        #endregion

        private System.Windows.Forms.PictureBox ToolBarImage;
        private System.Windows.Forms.Button panButton;
        private System.Windows.Forms.Button highlightButton;
        private System.Windows.Forms.Button EraserButton;
        private System.Windows.Forms.Button BoardButton;
        private System.Windows.Forms.Button EleaButton;
        private System.Windows.Forms.PictureBox PenBoxImage;
        private System.Windows.Forms.Panel PenColorBarPanel;
        private System.Windows.Forms.Button PenBlueButton;
        private System.Windows.Forms.Button PenRedButton;
        private System.Windows.Forms.Button PenBlackButton;
        private System.Windows.Forms.Button AppCloseButton;
        private System.Windows.Forms.PictureBox highlightBoxImage;
        private System.Windows.Forms.Button highlightYellowButton;
        private System.Windows.Forms.Button highlighGreenButton;
        private System.Windows.Forms.Button highlightPinkButton;
        private System.Windows.Forms.Panel highlightColorBarImage;
        private System.Windows.Forms.Panel EraserButtonBox;
        private System.Windows.Forms.Button EraserPixelButton;
        private System.Windows.Forms.Button EraserStrockButton;
        private System.Windows.Forms.Button EraserPrevButton;
        private System.Windows.Forms.PictureBox EraserBox;
        private System.Windows.Forms.Button EraserAllButton;
        private System.Windows.Forms.Panel penBarBackground;
        private System.Windows.Forms.PictureBox penBoxBarFill;
        private System.Windows.Forms.PictureBox penBoxHandle;
        private System.Windows.Forms.PictureBox highlightBoxHandle;
        private System.Windows.Forms.Panel highlightBarBackground;
        private System.Windows.Forms.PictureBox highlightBoxBarFill;
        private System.Windows.Forms.Button openFileList;
        private System.Windows.Forms.Button HambergerButton;
    }
}

