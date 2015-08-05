namespace HistoryBlocker
{
    partial class frmIncognito
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
            this.btnLock = new System.Windows.Forms.Button();
            this.applicationsListBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.btnUnlock = new System.Windows.Forms.Button();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.dataGridAppList = new System.Windows.Forms.DataGridView();
            this.appNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.msNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.statusDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.applicationsListBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridAppList)).BeginInit();
            this.SuspendLayout();
            // 
            // btnLock
            // 
            this.btnLock.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLock.Location = new System.Drawing.Point(393, 387);
            this.btnLock.Name = "btnLock";
            this.btnLock.Size = new System.Drawing.Size(75, 23);
            this.btnLock.TabIndex = 1;
            this.btnLock.Text = "&Lock";
            this.btnLock.UseVisualStyleBackColor = true;
            this.btnLock.Click += new System.EventHandler(this.btnLock_Click);
            // 
            // applicationsListBindingSource
            // 
            this.applicationsListBindingSource.DataSource = typeof(HistoryBlocker.applicationsList);
            // 
            // btnUnlock
            // 
            this.btnUnlock.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUnlock.Location = new System.Drawing.Point(474, 387);
            this.btnUnlock.Name = "btnUnlock";
            this.btnUnlock.Size = new System.Drawing.Size(75, 23);
            this.btnUnlock.TabIndex = 2;
            this.btnUnlock.Text = "&Unlock";
            this.btnUnlock.UseVisualStyleBackColor = true;
            this.btnUnlock.Click += new System.EventHandler(this.btnUnlock_Click);
            // 
            // btnBrowse
            // 
            this.btnBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnBrowse.Location = new System.Drawing.Point(13, 387);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(75, 23);
            this.btnBrowse.TabIndex = 3;
            this.btnBrowse.Text = "&Browse";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // dataGridAppList
            // 
            this.dataGridAppList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridAppList.AutoGenerateColumns = false;
            this.dataGridAppList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridAppList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.appNameDataGridViewTextBoxColumn,
            this.msNameDataGridViewTextBoxColumn,
            this.statusDataGridViewTextBoxColumn});
            this.dataGridAppList.DataSource = this.applicationsListBindingSource;
            this.dataGridAppList.Location = new System.Drawing.Point(13, 12);
            this.dataGridAppList.Name = "dataGridAppList";
            this.dataGridAppList.Size = new System.Drawing.Size(536, 354);
            this.dataGridAppList.TabIndex = 4;
            // 
            // appNameDataGridViewTextBoxColumn
            // 
            this.appNameDataGridViewTextBoxColumn.DataPropertyName = "AppName";
            this.appNameDataGridViewTextBoxColumn.HeaderText = "AppName";
            this.appNameDataGridViewTextBoxColumn.Name = "appNameDataGridViewTextBoxColumn";
            this.appNameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // msNameDataGridViewTextBoxColumn
            // 
            this.msNameDataGridViewTextBoxColumn.DataPropertyName = "msName";
            this.msNameDataGridViewTextBoxColumn.HeaderText = "msName";
            this.msNameDataGridViewTextBoxColumn.Name = "msNameDataGridViewTextBoxColumn";
            this.msNameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // statusDataGridViewTextBoxColumn
            // 
            this.statusDataGridViewTextBoxColumn.DataPropertyName = "Status";
            this.statusDataGridViewTextBoxColumn.HeaderText = "Status";
            this.statusDataGridViewTextBoxColumn.Name = "statusDataGridViewTextBoxColumn";
            this.statusDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // frmIncognito
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(561, 422);
            this.Controls.Add(this.dataGridAppList);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.btnUnlock);
            this.Controls.Add(this.btnLock);
            this.MinimumSize = new System.Drawing.Size(400, 300);
            this.Name = "frmIncognito";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.applicationsListBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridAppList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnLock;
        private System.Windows.Forms.BindingSource applicationsListBindingSource;
        private System.Windows.Forms.Button btnUnlock;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.DataGridView dataGridAppList;
        private System.Windows.Forms.DataGridViewTextBoxColumn appNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn msNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn statusDataGridViewTextBoxColumn;
    }
}

