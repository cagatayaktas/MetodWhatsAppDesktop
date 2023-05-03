namespace MetodWhatsAppDesktop
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnGetProducts = new System.Windows.Forms.ToolStripButton();
            this.btnSendProducts = new System.Windows.Forms.ToolStripButton();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.gridProducts = new System.Windows.Forms.DataGridView();
            this.colSelect = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.modelDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.renkDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Beden = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bakiyeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.resimDataGridViewImageColumn = new System.Windows.Forms.DataGridViewImageColumn();
            this.bsProduct = new System.Windows.Forms.BindingSource(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.gridPhones = new System.Windows.Forms.DataGridView();
            this.colPhoneSec = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.nameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gsmDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bsPhone = new System.Windows.Forms.BindingSource(this.components);
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnNew = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.tbGsm = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridProducts)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsProduct)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridPhones)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsPhone)).BeginInit();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnGetProducts,
            this.btnSendProducts});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1101, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnGetProducts
            // 
            this.btnGetProducts.Image = ((System.Drawing.Image)(resources.GetObject("btnGetProducts.Image")));
            this.btnGetProducts.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnGetProducts.Name = "btnGetProducts";
            this.btnGetProducts.Size = new System.Drawing.Size(156, 22);
            this.btnGetProducts.Text = "Güncel Ürün Listesi Getir";
            this.btnGetProducts.Click += new System.EventHandler(this.btnGetProducts_Click);
            // 
            // btnSendProducts
            // 
            this.btnSendProducts.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.btnSendProducts.Image = ((System.Drawing.Image)(resources.GetObject("btnSendProducts.Image")));
            this.btnSendProducts.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSendProducts.Name = "btnSendProducts";
            this.btnSendProducts.Size = new System.Drawing.Size(130, 22);
            this.btnSendProducts.Text = "WhatsApp a Bağlan";
            this.btnSendProducts.Click += new System.EventHandler(this.btnSendProducts_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 25);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.gridProducts);
            this.splitContainer1.Panel1.Controls.Add(this.panel1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.gridPhones);
            this.splitContainer1.Panel2.Controls.Add(this.panel3);
            this.splitContainer1.Panel2.Controls.Add(this.panel2);
            this.splitContainer1.Size = new System.Drawing.Size(1101, 513);
            this.splitContainer1.SplitterDistance = 738;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 1;
            // 
            // gridProducts
            // 
            this.gridProducts.AllowUserToAddRows = false;
            this.gridProducts.AllowUserToDeleteRows = false;
            this.gridProducts.AllowUserToOrderColumns = true;
            this.gridProducts.AutoGenerateColumns = false;
            this.gridProducts.BackgroundColor = System.Drawing.Color.White;
            this.gridProducts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridProducts.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colSelect,
            this.modelDataGridViewTextBoxColumn,
            this.renkDataGridViewTextBoxColumn,
            this.Beden,
            this.bakiyeDataGridViewTextBoxColumn,
            this.resimDataGridViewImageColumn});
            this.gridProducts.DataSource = this.bsProduct;
            this.gridProducts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridProducts.Location = new System.Drawing.Point(0, 33);
            this.gridProducts.Name = "gridProducts";
            this.gridProducts.RowHeadersVisible = false;
            this.gridProducts.RowTemplate.Height = 200;
            this.gridProducts.ShowCellErrors = false;
            this.gridProducts.ShowCellToolTips = false;
            this.gridProducts.ShowEditingIcon = false;
            this.gridProducts.ShowRowErrors = false;
            this.gridProducts.Size = new System.Drawing.Size(738, 480);
            this.gridProducts.TabIndex = 1;
            // 
            // colSelect
            // 
            this.colSelect.HeaderText = "Sec";
            this.colSelect.Name = "colSelect";
            this.colSelect.Width = 50;
            // 
            // modelDataGridViewTextBoxColumn
            // 
            this.modelDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.modelDataGridViewTextBoxColumn.DataPropertyName = "Model";
            this.modelDataGridViewTextBoxColumn.HeaderText = "Model";
            this.modelDataGridViewTextBoxColumn.Name = "modelDataGridViewTextBoxColumn";
            this.modelDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // renkDataGridViewTextBoxColumn
            // 
            this.renkDataGridViewTextBoxColumn.DataPropertyName = "Renk";
            this.renkDataGridViewTextBoxColumn.HeaderText = "Renk";
            this.renkDataGridViewTextBoxColumn.Name = "renkDataGridViewTextBoxColumn";
            this.renkDataGridViewTextBoxColumn.ReadOnly = true;
            this.renkDataGridViewTextBoxColumn.Width = 200;
            // 
            // Beden
            // 
            this.Beden.DataPropertyName = "Beden";
            this.Beden.HeaderText = "Beden";
            this.Beden.Name = "Beden";
            this.Beden.ReadOnly = true;
            // 
            // bakiyeDataGridViewTextBoxColumn
            // 
            this.bakiyeDataGridViewTextBoxColumn.DataPropertyName = "Bakiye";
            this.bakiyeDataGridViewTextBoxColumn.HeaderText = "Bakiye";
            this.bakiyeDataGridViewTextBoxColumn.Name = "bakiyeDataGridViewTextBoxColumn";
            this.bakiyeDataGridViewTextBoxColumn.ReadOnly = true;
            this.bakiyeDataGridViewTextBoxColumn.Width = 75;
            // 
            // resimDataGridViewImageColumn
            // 
            this.resimDataGridViewImageColumn.DataPropertyName = "Resim";
            this.resimDataGridViewImageColumn.HeaderText = "Resim";
            this.resimDataGridViewImageColumn.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.resimDataGridViewImageColumn.Name = "resimDataGridViewImageColumn";
            this.resimDataGridViewImageColumn.ReadOnly = true;
            this.resimDataGridViewImageColumn.Width = 200;
            // 
            // bsProduct
            // 
            this.bsProduct.DataSource = typeof(MetodWhatsAppDesktop.Models.ProductModel);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(738, 33);
            this.panel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(736, 31);
            this.label1.TabIndex = 0;
            this.label1.Text = "Ürün Listesi";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // gridPhones
            // 
            this.gridPhones.AllowUserToAddRows = false;
            this.gridPhones.AllowUserToDeleteRows = false;
            this.gridPhones.AutoGenerateColumns = false;
            this.gridPhones.BackgroundColor = System.Drawing.Color.White;
            this.gridPhones.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridPhones.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colPhoneSec,
            this.nameDataGridViewTextBoxColumn,
            this.gsmDataGridViewTextBoxColumn});
            this.gridPhones.DataSource = this.bsPhone;
            this.gridPhones.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridPhones.Location = new System.Drawing.Point(0, 33);
            this.gridPhones.Name = "gridPhones";
            this.gridPhones.RowHeadersVisible = false;
            this.gridPhones.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridPhones.Size = new System.Drawing.Size(358, 382);
            this.gridPhones.TabIndex = 2;
            this.gridPhones.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridPhones_CellDoubleClick);
            // 
            // colPhoneSec
            // 
            this.colPhoneSec.HeaderText = "Sec";
            this.colPhoneSec.Name = "colPhoneSec";
            this.colPhoneSec.Width = 50;
            // 
            // nameDataGridViewTextBoxColumn
            // 
            this.nameDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.nameDataGridViewTextBoxColumn.DataPropertyName = "Name";
            this.nameDataGridViewTextBoxColumn.HeaderText = "İsim / Ünvan";
            this.nameDataGridViewTextBoxColumn.Name = "nameDataGridViewTextBoxColumn";
            this.nameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // gsmDataGridViewTextBoxColumn
            // 
            this.gsmDataGridViewTextBoxColumn.DataPropertyName = "Gsm";
            this.gsmDataGridViewTextBoxColumn.HeaderText = "Gsm";
            this.gsmDataGridViewTextBoxColumn.Name = "gsmDataGridViewTextBoxColumn";
            this.gsmDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // bsPhone
            // 
            this.bsPhone.DataSource = typeof(MetodWhatsAppDesktop.Models.PhoneBookModel);
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.btnDelete);
            this.panel3.Controls.Add(this.btnNew);
            this.panel3.Controls.Add(this.btnSave);
            this.panel3.Controls.Add(this.tbGsm);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(this.tbName);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 415);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(358, 98);
            this.panel3.TabIndex = 1;
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelete.Location = new System.Drawing.Point(289, 67);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(56, 23);
            this.btnDelete.TabIndex = 4;
            this.btnDelete.Text = "Sil";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnNew
            // 
            this.btnNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNew.Location = new System.Drawing.Point(140, 67);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(56, 23);
            this.btnNew.TabIndex = 2;
            this.btnNew.Text = "Yeni";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(202, 67);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(81, 23);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "Kaydet";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // tbGsm
            // 
            this.tbGsm.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbGsm.Location = new System.Drawing.Point(100, 37);
            this.tbGsm.Name = "tbGsm";
            this.tbGsm.Size = new System.Drawing.Size(245, 23);
            this.tbGsm.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(17, 40);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(31, 15);
            this.label4.TabIndex = 0;
            this.label4.Text = "Gsm";
            // 
            // tbName
            // 
            this.tbName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbName.Location = new System.Drawing.Point(100, 8);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(245, 23);
            this.tbName.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 11);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 15);
            this.label3.TabIndex = 0;
            this.label3.Text = "İsim / Ünvan";
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.label2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(358, 33);
            this.panel2.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(356, 31);
            this.label2.TabIndex = 1;
            this.label2.Text = "Rehber";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1101, 538);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip1);
            this.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Metod Ürün Paylaş";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridProducts)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsProduct)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridPhones)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsPhone)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnGetProducts;
        private System.Windows.Forms.ToolStripButton btnSendProducts;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView gridProducts;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.BindingSource bsProduct;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colSelect;
        private System.Windows.Forms.DataGridViewTextBoxColumn modelDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn renkDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn Beden;
        private System.Windows.Forms.DataGridViewTextBoxColumn bakiyeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewImageColumn resimDataGridViewImageColumn;
        private System.Windows.Forms.DataGridView gridPhones;
        private System.Windows.Forms.BindingSource bsPhone;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox tbGsm;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colPhoneSec;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn gsmDataGridViewTextBoxColumn;
    }
}

