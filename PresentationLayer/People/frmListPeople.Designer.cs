namespace DVLD
{
    partial class frmListPeople
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmListPeople));
            this.lblPeopleManagement = new System.Windows.Forms.Label();
            this.dgvPeople = new Guna.UI2.WinForms.Guna2DataGridView();
            this.cmsPeopleGrid = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tmsShowDetails = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.tmsAddNewPerson = new System.Windows.Forms.ToolStripMenuItem();
            this.tmsEditPerson = new System.Windows.Forms.ToolStripMenuItem();
            this.tmsDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.tmsSendEmail = new System.Windows.Forms.ToolStripMenuItem();
            this.tmsPhoneCall = new System.Windows.Forms.ToolStripMenuItem();
            this.btnAddPerson = new Guna.UI2.WinForms.Guna2Button();
            this.lblFilterBy = new System.Windows.Forms.Label();
            this.cbFilterBy = new Guna.UI2.WinForms.Guna2ComboBox();
            this.txtFilterValue = new Guna.UI2.WinForms.Guna2TextBox();
            this.guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            this.lblRecordsCount = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.guna2Panel2 = new Guna.UI2.WinForms.Guna2Panel();
            this.lblFilteredRecordsCount = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.guna2PictureBox1 = new Guna.UI2.WinForms.Guna2PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPeople)).BeginInit();
            this.cmsPeopleGrid.SuspendLayout();
            this.guna2Panel1.SuspendLayout();
            this.guna2Panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblPeopleManagement
            // 
            this.lblPeopleManagement.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(103)))), ((int)(((byte)(178)))));
            this.lblPeopleManagement.Font = new System.Drawing.Font("Segoe UI Semibold", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPeopleManagement.ForeColor = System.Drawing.Color.White;
            this.lblPeopleManagement.Location = new System.Drawing.Point(70, 22);
            this.lblPeopleManagement.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblPeopleManagement.Name = "lblPeopleManagement";
            this.lblPeopleManagement.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.lblPeopleManagement.Size = new System.Drawing.Size(1310, 42);
            this.lblPeopleManagement.TabIndex = 0;
            this.lblPeopleManagement.Text = "People Management";
            // 
            // dgvPeople
            // 
            this.dgvPeople.AllowUserToAddRows = false;
            this.dgvPeople.AllowUserToDeleteRows = false;
            this.dgvPeople.AllowUserToOrderColumns = true;
            this.dgvPeople.AllowUserToResizeColumns = false;
            this.dgvPeople.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            this.dgvPeople.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 18F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvPeople.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvPeople.ColumnHeadersHeight = 4;
            this.dgvPeople.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.dgvPeople.ContextMenuStrip = this.cmsPeopleGrid;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 18F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvPeople.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvPeople.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvPeople.Location = new System.Drawing.Point(9, 183);
            this.dgvPeople.Name = "dgvPeople";
            this.dgvPeople.ReadOnly = true;
            this.dgvPeople.RowHeadersVisible = false;
            this.dgvPeople.Size = new System.Drawing.Size(1500, 388);
            this.dgvPeople.TabIndex = 6;
            this.dgvPeople.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White;
            this.dgvPeople.ThemeStyle.AlternatingRowsStyle.Font = null;
            this.dgvPeople.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty;
            this.dgvPeople.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty;
            this.dgvPeople.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty;
            this.dgvPeople.ThemeStyle.BackColor = System.Drawing.Color.White;
            this.dgvPeople.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvPeople.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.dgvPeople.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvPeople.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("Segoe UI", 18F);
            this.dgvPeople.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White;
            this.dgvPeople.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.dgvPeople.ThemeStyle.HeaderStyle.Height = 4;
            this.dgvPeople.ThemeStyle.ReadOnly = true;
            this.dgvPeople.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White;
            this.dgvPeople.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvPeople.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("Segoe UI", 18F);
            this.dgvPeople.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.Black;
            this.dgvPeople.ThemeStyle.RowsStyle.Height = 22;
            this.dgvPeople.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvPeople.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            // 
            // cmsPeopleGrid
            // 
            this.cmsPeopleGrid.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tmsShowDetails,
            this.toolStripMenuItem1,
            this.tmsAddNewPerson,
            this.tmsEditPerson,
            this.tmsDelete,
            this.toolStripMenuItem2,
            this.tmsSendEmail,
            this.tmsPhoneCall});
            this.cmsPeopleGrid.Name = "cmsPeopleGrid";
            this.cmsPeopleGrid.Size = new System.Drawing.Size(163, 148);
            // 
            // tmsShowDetails
            // 
            this.tmsShowDetails.Image = global::DVLD.Properties.Resources.ShowDetails;
            this.tmsShowDetails.Name = "tmsShowDetails";
            this.tmsShowDetails.Size = new System.Drawing.Size(162, 22);
            this.tmsShowDetails.Text = "Show details";
            this.tmsShowDetails.Click += new System.EventHandler(this.tmsShowDetails_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(159, 6);
            // 
            // tmsAddNewPerson
            // 
            this.tmsAddNewPerson.Image = global::DVLD.Properties.Resources.AddNewPerson;
            this.tmsAddNewPerson.Name = "tmsAddNewPerson";
            this.tmsAddNewPerson.Size = new System.Drawing.Size(162, 22);
            this.tmsAddNewPerson.Text = "Add New Person";
            this.tmsAddNewPerson.Click += new System.EventHandler(this.tmsAddNewPerson_Click);
            // 
            // tmsEditPerson
            // 
            this.tmsEditPerson.Image = global::DVLD.Properties.Resources.Edit;
            this.tmsEditPerson.Name = "tmsEditPerson";
            this.tmsEditPerson.Size = new System.Drawing.Size(162, 22);
            this.tmsEditPerson.Text = "Edit";
            this.tmsEditPerson.Click += new System.EventHandler(this.tmsEditPerson_Click);
            // 
            // tmsDelete
            // 
            this.tmsDelete.Image = global::DVLD.Properties.Resources.Delete;
            this.tmsDelete.Name = "tmsDelete";
            this.tmsDelete.Size = new System.Drawing.Size(162, 22);
            this.tmsDelete.Text = "Delete";
            this.tmsDelete.Click += new System.EventHandler(this.tmsDelete_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(159, 6);
            // 
            // tmsSendEmail
            // 
            this.tmsSendEmail.Image = global::DVLD.Properties.Resources.SendEmail;
            this.tmsSendEmail.Name = "tmsSendEmail";
            this.tmsSendEmail.Size = new System.Drawing.Size(162, 22);
            this.tmsSendEmail.Text = "Send Email";
            this.tmsSendEmail.Click += new System.EventHandler(this.tmsSendEmail_Click);
            // 
            // tmsPhoneCall
            // 
            this.tmsPhoneCall.Image = global::DVLD.Properties.Resources.PhoneCall;
            this.tmsPhoneCall.Name = "tmsPhoneCall";
            this.tmsPhoneCall.Size = new System.Drawing.Size(162, 22);
            this.tmsPhoneCall.Text = "Phone Call";
            this.tmsPhoneCall.Click += new System.EventHandler(this.tmsPhoneCall_Click);
            // 
            // btnAddPerson
            // 
            this.btnAddPerson.BorderRadius = 5;
            this.btnAddPerson.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnAddPerson.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnAddPerson.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnAddPerson.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnAddPerson.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(103)))), ((int)(((byte)(178)))));
            this.btnAddPerson.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddPerson.ForeColor = System.Drawing.Color.White;
            this.btnAddPerson.Location = new System.Drawing.Point(1329, 119);
            this.btnAddPerson.Name = "btnAddPerson";
            this.btnAddPerson.Size = new System.Drawing.Size(180, 45);
            this.btnAddPerson.TabIndex = 7;
            this.btnAddPerson.Text = "+ Add Person";
            this.btnAddPerson.Click += new System.EventHandler(this.btnAddPerson_Click);
            // 
            // lblFilterBy
            // 
            this.lblFilterBy.AutoSize = true;
            this.lblFilterBy.Font = new System.Drawing.Font("Segoe UI Semibold", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFilterBy.ForeColor = System.Drawing.Color.Black;
            this.lblFilterBy.Location = new System.Drawing.Point(12, 132);
            this.lblFilterBy.Name = "lblFilterBy";
            this.lblFilterBy.Size = new System.Drawing.Size(109, 32);
            this.lblFilterBy.TabIndex = 8;
            this.lblFilterBy.Text = "Filter By:";
            // 
            // cbFilterBy
            // 
            this.cbFilterBy.BackColor = System.Drawing.Color.Transparent;
            this.cbFilterBy.BorderRadius = 5;
            this.cbFilterBy.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbFilterBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFilterBy.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cbFilterBy.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cbFilterBy.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cbFilterBy.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cbFilterBy.ItemHeight = 30;
            this.cbFilterBy.Location = new System.Drawing.Point(127, 128);
            this.cbFilterBy.Name = "cbFilterBy";
            this.cbFilterBy.Size = new System.Drawing.Size(140, 36);
            this.cbFilterBy.TabIndex = 9;
            this.cbFilterBy.SelectedIndexChanged += new System.EventHandler(this.cbFiltrerBy_SelectedIndexChanged);
            // 
            // txtFilterValue
            // 
            this.txtFilterValue.BorderRadius = 5;
            this.txtFilterValue.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtFilterValue.DefaultText = "";
            this.txtFilterValue.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtFilterValue.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtFilterValue.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtFilterValue.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtFilterValue.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtFilterValue.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtFilterValue.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtFilterValue.Location = new System.Drawing.Point(295, 128);
            this.txtFilterValue.Name = "txtFilterValue";
            this.txtFilterValue.PlaceholderText = "";
            this.txtFilterValue.SelectedText = "";
            this.txtFilterValue.Size = new System.Drawing.Size(184, 36);
            this.txtFilterValue.TabIndex = 10;
            this.txtFilterValue.Visible = false;
            this.txtFilterValue.TextChanged += new System.EventHandler(this.txtFilterByValue_TextChanged);
            this.txtFilterValue.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFilterValue_KeyPress);
            // 
            // guna2Panel1
            // 
            this.guna2Panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(75)))), ((int)(((byte)(120)))));
            this.guna2Panel1.BorderRadius = 5;
            this.guna2Panel1.Controls.Add(this.lblRecordsCount);
            this.guna2Panel1.Controls.Add(this.label1);
            this.guna2Panel1.Location = new System.Drawing.Point(9, 577);
            this.guna2Panel1.Name = "guna2Panel1";
            this.guna2Panel1.Size = new System.Drawing.Size(324, 58);
            this.guna2Panel1.TabIndex = 11;
            // 
            // lblRecordsCount
            // 
            this.lblRecordsCount.AutoSize = true;
            this.lblRecordsCount.Font = new System.Drawing.Font("Segoe UI Semibold", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRecordsCount.ForeColor = System.Drawing.Color.White;
            this.lblRecordsCount.Location = new System.Drawing.Point(160, 15);
            this.lblRecordsCount.Name = "lblRecordsCount";
            this.lblRecordsCount.Size = new System.Drawing.Size(58, 32);
            this.lblRecordsCount.TabIndex = 10;
            this.lblRecordsCount.Text = "N/A";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(3, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(165, 32);
            this.label1.TabIndex = 9;
            this.label1.Text = "Total Records:";
            // 
            // guna2Panel2
            // 
            this.guna2Panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(75)))), ((int)(((byte)(120)))));
            this.guna2Panel2.BorderRadius = 5;
            this.guna2Panel2.Controls.Add(this.lblFilteredRecordsCount);
            this.guna2Panel2.Controls.Add(this.label3);
            this.guna2Panel2.Location = new System.Drawing.Point(363, 577);
            this.guna2Panel2.Name = "guna2Panel2";
            this.guna2Panel2.Size = new System.Drawing.Size(324, 58);
            this.guna2Panel2.TabIndex = 12;
            // 
            // lblFilteredRecordsCount
            // 
            this.lblFilteredRecordsCount.AutoSize = true;
            this.lblFilteredRecordsCount.Font = new System.Drawing.Font("Segoe UI Semibold", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFilteredRecordsCount.ForeColor = System.Drawing.Color.White;
            this.lblFilteredRecordsCount.Location = new System.Drawing.Point(190, 15);
            this.lblFilteredRecordsCount.Name = "lblFilteredRecordsCount";
            this.lblFilteredRecordsCount.Size = new System.Drawing.Size(135, 32);
            this.lblFilteredRecordsCount.TabIndex = 10;
            this.lblFilteredRecordsCount.Text = "0 Record(s)";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI Semibold", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(3, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(194, 32);
            this.label3.TabIndex = 9;
            this.label3.Text = "Filtered Records:";
            // 
            // guna2PictureBox1
            // 
            this.guna2PictureBox1.BackColor = System.Drawing.SystemColors.Control;
            this.guna2PictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("guna2PictureBox1.Image")));
            this.guna2PictureBox1.ImageRotate = 0F;
            this.guna2PictureBox1.Location = new System.Drawing.Point(9, 22);
            this.guna2PictureBox1.Margin = new System.Windows.Forms.Padding(2);
            this.guna2PictureBox1.Name = "guna2PictureBox1";
            this.guna2PictureBox1.Size = new System.Drawing.Size(57, 42);
            this.guna2PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.guna2PictureBox1.TabIndex = 1;
            this.guna2PictureBox1.TabStop = false;
            // 
            // frmListPeople
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 32F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1521, 647);
            this.Controls.Add(this.guna2Panel2);
            this.Controls.Add(this.guna2Panel1);
            this.Controls.Add(this.txtFilterValue);
            this.Controls.Add(this.cbFilterBy);
            this.Controls.Add(this.lblFilterBy);
            this.Controls.Add(this.btnAddPerson);
            this.Controls.Add(this.dgvPeople);
            this.Controls.Add(this.guna2PictureBox1);
            this.Controls.Add(this.lblPeopleManagement);
            this.Font = new System.Drawing.Font("Segoe UI", 18F);
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(8);
            this.Name = "frmListPeople";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "People Management";
            this.Load += new System.EventHandler(this.frmPeople_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPeople)).EndInit();
            this.cmsPeopleGrid.ResumeLayout(false);
            this.guna2Panel1.ResumeLayout(false);
            this.guna2Panel1.PerformLayout();
            this.guna2Panel2.ResumeLayout(false);
            this.guna2Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblPeopleManagement;
        private Guna.UI2.WinForms.Guna2PictureBox guna2PictureBox1;
        private Guna.UI2.WinForms.Guna2DataGridView dgvPeople;
        private Guna.UI2.WinForms.Guna2Button btnAddPerson;
        private System.Windows.Forms.Label lblFilterBy;
        private Guna.UI2.WinForms.Guna2ComboBox cbFilterBy;
        private Guna.UI2.WinForms.Guna2TextBox txtFilterValue;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private System.Windows.Forms.Label lblRecordsCount;
        private System.Windows.Forms.Label label1;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel2;
        private System.Windows.Forms.Label lblFilteredRecordsCount;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ContextMenuStrip cmsPeopleGrid;
        private System.Windows.Forms.ToolStripMenuItem tmsShowDetails;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem tmsAddNewPerson;
        private System.Windows.Forms.ToolStripMenuItem tmsEditPerson;
        private System.Windows.Forms.ToolStripMenuItem tmsDelete;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem tmsSendEmail;
        private System.Windows.Forms.ToolStripMenuItem tmsPhoneCall;
    }
}