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
    public partial class AddJG : Form
    {
        int JGID = -1;
        public AddJG()
        {
            InitializeComponent();
        }

        public AddJG(int id)
        {
            InitializeComponent();
            JGID = id;
            string sql = string.Format("select * from t_jg where id={0}", id);
            DataTable dtTemp = OLEHelper.Instance.GetDataTable(sql);
            if (dtTemp != null && dtTemp.Rows.Count > 0)
            {
                tbName.Text = dtTemp.Rows[0]["name"].ToString();
                tbX.Text = dtTemp.Rows[0]["x"].ToString();
                tbY.Text = dtTemp.Rows[0]["y"].ToString();
                tbZ.Text = dtTemp.Rows[0]["z"].ToString();
                tbType.Text = dtTemp.Rows[0]["type"].ToString();
                tbOwner.Text = dtTemp.Rows[0]["owner"].ToString();
                tbCaizhi.Text = dtTemp.Rows[0]["caizhi"].ToString();
                tbGuige.Text = dtTemp.Rows[0]["guige"].ToString();
                tbXinghao.Text = dtTemp.Rows[0]["xinghao"].ToString();
                tbXingZhuang.Text= dtTemp.Rows[0]["xingzhuang"].ToString();
                tbRemarks.Text = dtTemp.Rows[0]["remarks"].ToString();
            }
        }

        private void btSave_Click(object sender, EventArgs e)
        {

            if (Check())
            {
                try
                {
                    if (Save())
                    {
                        MessageBox.Show("保存成功！");
                        this.DialogResult = DialogResult.OK;
                    }
                    else
                    {
                        MessageBox.Show("保存失败！");
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show("保存失败：" + ex.Message);
                }
            }
        }
        bool Save()
        {
            string sql = string.Empty;
            if (JGID > 0)
            {
                sql = string.Format("update t_jg set x={0},y={1},z={2},name='{3}',type='{4}',owner='{5}',caizhi='{6}',guige='{7}',xinghao='{8}',xingzhuang='{9}',remarks='{10}' where id={11}"

                    ,Convert.ToDouble(tbX.Text)
                    ,Convert.ToDouble(tbY.Text)
                    ,Convert.ToDouble(tbZ.Text)
                    ,tbName.Text,tbType.Text,tbOwner.Text,tbCaizhi.Text,tbGuige.Text,tbXinghao.Text,tbXingZhuang.Text,tbRemarks.Text,JGID);
            }
            else
            {
                //新增
                sql = string.Format("insert into t_jg (x,y,z,name,type,owner,caizhi,guige,xinghao,xingzhuang,remarks)values({0},{1},{2},'{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}')"

                    , Convert.ToDouble(tbX.Text)
                    , Convert.ToDouble(tbY.Text)
                    , Convert.ToDouble(tbZ.Text)
                    , tbName.Text, tbType.Text, tbOwner.Text, tbCaizhi.Text, tbGuige.Text, tbXinghao.Text, tbXingZhuang.Text, tbRemarks.Text);
            }
            if (OLEHelper.Instance.ExecuteSql(sql))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        bool Check()
        {
            if (string.IsNullOrEmpty(tbName.Text.Trim()))
            {
                MessageBox.Show("名称不能为空！");
                tbName.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(tbX.Text.Trim()))
            {
                MessageBox.Show("X不能为空！");
                tbX.Focus();
                return false;
            }
            else
            {
                try
                {
                    Convert.ToDouble(tbX.Text);
                }
                catch
                {
                    MessageBox.Show("X必须为数字类型！");
                    tbX.Focus();
                    return false;
                }
            }

            if (string.IsNullOrEmpty(tbY.Text.Trim()))
            {
                MessageBox.Show("Y不能为空！");
                tbY.Focus();
                return false;
            }
            else
            {
                try
                {
                    Convert.ToDouble(tbY.Text);
                }
                catch
                {
                    MessageBox.Show("Y必须为数字类型！");
                    tbY.Focus();
                    return false;
                }
            }

            if (string.IsNullOrEmpty(tbZ.Text.Trim()))
            {
                MessageBox.Show("Z不能为空！");
                tbZ.Focus();
                return false;
            }
            else
            {
                try
                {
                    Convert.ToDouble(tbZ.Text);
                }
                catch
                {
                    MessageBox.Show("Z必须为数字类型！");
                    tbZ.Focus();
                    return false;
                }
            }
            return true;
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
