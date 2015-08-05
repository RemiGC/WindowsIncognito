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
            this.listApp = new System.Windows.Forms.ListBox();
            this.applicationsListBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.btnUnlock = new System.Windows.Forms.Button();
            this.btnBrowse = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.applicationsListBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // btnLock
            // 
            this.btnLock.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLock.Location = new System.Drawing.Point(253, 300);
            this.btnLock.Name = "btnLock";
            this.btnLock.Size = new System.Drawing.Size(75, 23);
            this.btnLock.TabIndex = 1;
            this.btnLock.Text = "&Lock";
            this.btnLock.UseVisualStyleBackColor = true;
            this.btnLock.Click += new System.EventHandler(this.btnLock_Click);
            // 
            // listApp
            // 
            this.listApp.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listApp.DataSource = this.applicationsListBindingSource;
            this.listApp.DisplayMember = "Status";
            this.listApp.FormattingEnabled = true;
            this.listApp.Location = new System.Drawing.Point(12, 12);
            this.listApp.Name = "listApp";
            this.listApp.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listApp.Size = new System.Drawing.Size(397, 264);
            this.listApp.TabIndex = 0;
            this.listApp.ValueMember = "AppName";
            // 
            // applicationsListBindingSource
            // 
            this.applicationsListBindingSource.DataSource = typeof(HistoryBlocker.applicationsList);
            // 
            // btnUnlock
            // 
            this.btnUnlock.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUnlock.Location = new System.Drawing.Point(334, 300);
            this.btnUnlock.Name = "btnUnlock";
            this.btnUnlock.Size = new System.Drawing.Size(75, 23);
            this.btnUnlock.TabIndex = 2;
            this.btnUnlock.Text = "&Unlock";
            this.btnUnlock.UseVisualStyleBackColor = true;
            this.btnUnlock.Click += new System.EventHandler(this.btnUnlock_Click);
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(13, 300);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(75, 23);
            this.btnBrowse.TabIndex = 3;
            this.btnBrowse.Text = "&Browse";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.button1_Click);
            // 
            // frmIncognito
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(421, 335);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.btnUnlock);
            this.Controls.Add(this.listApp);
            this.Controls.Add(this.btnLock);
            this.MinimumSize = new System.Drawing.Size(400, 300);
            this.Name = "frmIncognito";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.applicationsListBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnLock;
        private System.Windows.Forms.ListBox listApp;
        private System.Windows.Forms.BindingSource applicationsListBindingSource;
        private System.Windows.Forms.Button btnUnlock;
        private System.Windows.Forms.Button btnBrowse;
    }
}

