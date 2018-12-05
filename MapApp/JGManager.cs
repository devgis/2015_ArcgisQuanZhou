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
    public partial class JGManager : Form
    {
        public bool Changed = false;
        public JGManager()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 加载数据
        /// </summary>
        public void LoadData()
        {
            DataTable dttemp = OLEHelper.Instance.GetDataTable("select * from t_jg");
            dgvJGList.DataSource = dttemp;
        }

        private void JGManager_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void dgvJGList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                if (e.ColumnIndex == 0)
                {
                    DataGridViewCheckBoxCell cell = (dgvJGList.Rows[e.RowIndex].Cells[e.ColumnIndex] as DataGridViewCheckBoxCell);
                    if (cell.Value!=null&&cell.Value.ToString().ToUpper()=="TRUE")
                    {
                        cell.Value = false;
                    }
                    else
                        cell.Value = true;
                    }
                }
            }

        private void tsbtnAdd_Click(object sender, EventArgs e)
        {
            AddJG frmAdd = new AddJG();
            if (frmAdd.ShowDialog() == DialogResult.OK)
            {
                LoadData();//刷新
                Changed = true;
            }
        }

        private void tsbtnEdit_Click(object sender, EventArgs e)
        {
            //判定只选择一项并且选择了
            List<int> ids = getSelect();
            if (ids == null)
            {
                MessageBox.Show("请选择！");
                return;
            }
            else if(ids.Count!=1)
            {
                MessageBox.Show("请选择并且只能选择一项进行编");
                return;
            }

            AddJG frmAdd = new AddJG(ids[0]);
            if (frmAdd.ShowDialog() == DialogResult.OK)
            {
                LoadData();//刷新
                Changed = true;
            }
        }

        private void tsbtnDel_Click(object sender, EventArgs e)
        {
            //判定选择了
            List<int> ids = getSelect();
            if (ids == null||ids.Count<=0)
            {
                MessageBox.Show("请选择！");
                return;
            }
            //删除
            
            string sql = string.Empty;
            if (ids.Count > 1)
            {
                string sids = "(";
                for (int i = 0; i < ids.Count; i++)
                {
                    if (i == 0)
                    {
                        sids += ids[i];
                    }
                    else
                    {
                        sids += ("," + ids[i]);
                    }
                }
                sids += ")";
                sql = string.Format("delete * from t_jg where id in {0}", sids);
            }
            else
            {
                sql = string.Format("delete * from t_jg where id={0}", ids[0]);
            }

            try
            {
                if (OLEHelper.Instance.ExecuteSql(sql))
                {
                    LoadData();
                    Changed = true;
                    MessageBox.Show("删除成功！");
                }
                else
                {
                    MessageBox.Show("删除失败！");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("删除错误:" + ex.Message);
            }
        }

        private List<int> getSelect()
        {
            List<int> listRs = new List<int>();
            foreach (DataGridViewRow row in dgvJGList.Rows)
            {
                DataGridViewCheckBoxCell cell = (row.Cells["CSelect"] as DataGridViewCheckBoxCell);
                if (cell.Value != null && cell.Value.ToString().ToUpper() == "TRUE")
                {
                    try
                    {
                        int id = Convert.ToInt32(row.Cells["CID"].Value);
                        listRs.Add(id);
                    }
                    catch
                    { }
                }
            }
            return listRs;
        }

        private void JGManager_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Changed)
            {
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                this.DialogResult = DialogResult.Cancel;
            }
        }
    }
}
