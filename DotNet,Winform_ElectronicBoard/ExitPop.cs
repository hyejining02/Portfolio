using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ELEA_BOARD
{
    public partial class ExitPop : Form
    {
        public ExitPop()
        {
            InitializeComponent();
        }

        private void ExitPop_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.FormBorderStyle = FormBorderStyle.FixedDialog; // 크기 변경 불가
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ControlBox = false; // 닫기 버튼 숨기려면
            this.StartPosition = FormStartPosition.CenterParent;
            this.ShowInTaskbar = false;
            this.BackColor = Color.White;
            this.TopMost = true;
            this.Text = "종료";

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
