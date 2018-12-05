using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MapApp
{
    public partial class MapSearchJG : Form
    {
        public string Words
        {
            get;
            set;
        }
        public MapSearchJG()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text.Trim()))
            {
                MessageBox.Show("请输入需要查询的关键字!");
            }
            else
            {
                Words = textBox1.Text;
                this.DialogResult = DialogResult.OK;
            }
        }
    }
}
