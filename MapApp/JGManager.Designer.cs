namespace MapApp
{
    partial class JGManager
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(JGManager));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbtnAdd = new System.Windows.Forms.ToolStripButton();
            this.tsbtnEdit = new System.Windows.Forms.ToolStripButton();
            this.tsbtnDel = new System.Windows.Forms.ToolStripButton();
            this.dgvJGList = new System.Windows.Forms.DataGridView();
            this.CSelect = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.CID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CX = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CZ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.COwner = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CCaizhi = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CGuige = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CXinghao = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CXingzhuang = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CRemarks = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvJGList)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtnAdd,
            this.tsbtnEdit,
            this.tsbtnDel});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(757, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbtnAdd
            // 
            this.tsbtnAdd.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnAdd.Image")));
            this.tsbtnAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnAdd.Name = "tsbtnAdd";
            this.tsbtnAdd.Size = new System.Drawing.Size(52, 22);
            this.tsbtnAdd.Text = "添加";
            this.tsbtnAdd.Click += new System.EventHandler(this.tsbtnAdd_Click);
            // 
            // tsbtnEdit
            // 
            this.tsbtnEdit.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnEdit.Image")));
            this.tsbtnEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnEdit.Name = "tsbtnEdit";
            this.tsbtnEdit.Size = new System.Drawing.Size(52, 22);
            this.tsbtnEdit.Text = "编辑";
            this.tsbtnEdit.Click += new System.EventHandler(this.tsbtnEdit_Click);
            // 
            // tsbtnDel
            // 
            this.tsbtnDel.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnDel.Image")));
            this.tsbtnDel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnDel.Name = "tsbtnDel";
            this.tsbtnDel.Size = new System.Drawing.Size(52, 22);
            this.tsbtnDel.Text = "删除";
            this.tsbtnDel.Click += new System.EventHandler(this.tsbtnDel_Click);
            // 
            // dgvJGList
            // 
            this.dgvJGList.AllowUserToAddRows = false;
            this.dgvJGList.AllowUserToDeleteRows = false;
            this.dgvJGList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvJGList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CSelect,
            this.CID,
            this.CName,
            this.CX,
            this.CY,
            this.CZ,
            this.CType,
            this.COwner,
            this.CCaizhi,
            this.CGuige,
            this.CXinghao,
            this.CXingzhuang,
            this.CRemarks});
            this.dgvJGList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvJGList.Location = new System.Drawing.Point(0, 25);
            this.dgvJGList.Name = "dgvJGList";
            this.dgvJGList.ReadOnly = true;
            this.dgvJGList.RowTemplate.Height = 23;
            this.dgvJGList.Size = new System.Drawing.Size(757, 342);
            this.dgvJGList.TabIndex = 1;
            this.dgvJGList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvJGList_CellClick);
            // 
            // CSelect
            // 
            this.CSelect.HeaderText = "选择";
            this.CSelect.Name = "CSelect";
            this.CSelect.ReadOnly = true;
            // 
            // CID
            // 
            this.CID.DataPropertyName = "id";
            this.CID.HeaderText = "ID";
            this.CID.Name = "CID";
            this.CID.ReadOnly = true;
            this.CID.Visible = false;
            // 
            // CName
            // 
            this.CName.DataPropertyName = "name";
            this.CName.HeaderText = "名称";
            this.CName.Name = "CName";
            this.CName.ReadOnly = true;
            // 
            // CX
            // 
            this.CX.DataPropertyName = "x";
            this.CX.HeaderText = "X";
            this.CX.Name = "CX";
            this.CX.ReadOnly = true;
            // 
            // CY
            // 
            this.CY.DataPropertyName = "y";
            this.CY.HeaderText = "Y";
            this.CY.Name = "CY";
            this.CY.ReadOnly = true;
            // 
            // CZ
            // 
            this.CZ.DataPropertyName = "z";
            this.CZ.HeaderText = "Z";
            this.CZ.Name = "CZ";
            this.CZ.ReadOnly = true;
            // 
            // CType
            // 
            this.CType.DataPropertyName = "type";
            this.CType.HeaderText = "类型";
            this.CType.Name = "CType";
            this.CType.ReadOnly = true;
            // 
            // COwner
            // 
            this.COwner.DataPropertyName = "owner";
            this.COwner.HeaderText = "所有者";
            this.COwner.Name = "COwner";
            this.COwner.ReadOnly = true;
            this.COwner.Width = 200;
            // 
            // CCaizhi
            // 
            this.CCaizhi.DataPropertyName = "caizhi";
            this.CCaizhi.HeaderText = "材质";
            this.CCaizhi.Name = "CCaizhi";
            this.CCaizhi.ReadOnly = true;
            // 
            // CGuige
            // 
            this.CGuige.DataPropertyName = "guige";
            this.CGuige.HeaderText = "规格";
            this.CGuige.Name = "CGuige";
            this.CGuige.ReadOnly = true;
            // 
            // CXinghao
            // 
            this.CXinghao.DataPropertyName = "xinghao";
            this.CXinghao.HeaderText = "型号";
            this.CXinghao.Name = "CXinghao";
            this.CXinghao.ReadOnly = true;
            // 
            // CXingzhuang
            // 
            this.CXingzhuang.DataPropertyName = "xingzhuang";
            this.CXingzhuang.HeaderText = "形状";
            this.CXingzhuang.Name = "CXingzhuang";
            this.CXingzhuang.ReadOnly = true;
            // 
            // CRemarks
            // 
            this.CRemarks.DataPropertyName = "remarks";
            this.CRemarks.HeaderText = "备注";
            this.CRemarks.Name = "CRemarks";
            this.CRemarks.ReadOnly = true;
            this.CRemarks.Width = 300;
            // 
            // JGManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(757, 367);
            this.Controls.Add(this.dgvJGList);
            this.Controls.Add(this.toolStrip1);
            this.Name = "JGManager";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "井盖管理";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.JGManager_FormClosing);
            this.Load += new System.EventHandler(this.JGManager_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvJGList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbtnAdd;
        private System.Windows.Forms.ToolStripButton tsbtnEdit;
        private System.Windows.Forms.ToolStripButton tsbtnDel;
        private System.Windows.Forms.DataGridView dgvJGList;
        private System.Windows.Forms.DataGridViewCheckBoxColumn CSelect;
        private System.Windows.Forms.DataGridViewTextBoxColumn CID;
        private System.Windows.Forms.DataGridViewTextBoxColumn CName;
        private System.Windows.Forms.DataGridViewTextBoxColumn CX;
        private System.Windows.Forms.DataGridViewTextBoxColumn CY;
        private System.Windows.Forms.DataGridViewTextBoxColumn CZ;
        private System.Windows.Forms.DataGridViewTextBoxColumn CType;
        private System.Windows.Forms.DataGridViewTextBoxColumn COwner;
        private System.Windows.Forms.DataGridViewTextBoxColumn CCaizhi;
        private System.Windows.Forms.DataGridViewTextBoxColumn CGuige;
        private System.Windows.Forms.DataGridViewTextBoxColumn CXinghao;
        private System.Windows.Forms.DataGridViewTextBoxColumn CXingzhuang;
        private System.Windows.Forms.DataGridViewTextBoxColumn CRemarks;
    }
}