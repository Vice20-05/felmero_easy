namespace felmero
{
    partial class Form1
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
            this.tabControlMain = new System.Windows.Forms.TabControl();
            this.tabPageEmployees = new System.Windows.Forms.TabPage();
            this.labelEmployeeCount = new System.Windows.Forms.Label();
            this.buttonGenerateEmployees = new System.Windows.Forms.Button();
            this.dataGridViewEmployees = new System.Windows.Forms.DataGridView();
            this.tabPageProjects = new System.Windows.Forms.TabPage();
            this.dataGridViewProjects = new System.Windows.Forms.DataGridView();
            this.labelAcceptedProjects = new System.Windows.Forms.Label();
            this.buttonAcceptProject = new System.Windows.Forms.Button();
            this.labelResult = new System.Windows.Forms.Label();
            this.labelCost = new System.Windows.Forms.Label();
            this.buttonRemoveMember = new System.Windows.Forms.Button();
            this.listBoxTeam = new System.Windows.Forms.ListBox();
            this.buttonAddMember = new System.Windows.Forms.Button();
            this.numericUpDownHours = new System.Windows.Forms.NumericUpDown();
            this.labelHours = new System.Windows.Forms.Label();
            this.comboBoxProgrammer = new System.Windows.Forms.ComboBox();
            this.labelProgrammer = new System.Windows.Forms.Label();
            this.comboBoxProjectManager = new System.Windows.Forms.ComboBox();
            this.labelProjectManager = new System.Windows.Forms.Label();
            this.numericUpDownBudget = new System.Windows.Forms.NumericUpDown();
            this.labelBudget = new System.Windows.Forms.Label();
            this.textBoxProjectName = new System.Windows.Forms.TextBox();
            this.labelProjectName = new System.Windows.Forms.Label();
            this.tabControlMain.SuspendLayout();
            this.tabPageEmployees.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEmployees)).BeginInit();
            this.tabPageProjects.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewProjects)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHours)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownBudget)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControlMain
            // 
            this.tabControlMain.Controls.Add(this.tabPageEmployees);
            this.tabControlMain.Controls.Add(this.tabPageProjects);
            this.tabControlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlMain.Location = new System.Drawing.Point(0, 0);
            this.tabControlMain.Name = "tabControlMain";
            this.tabControlMain.SelectedIndex = 0;
            this.tabControlMain.Size = new System.Drawing.Size(884, 561);
            this.tabControlMain.TabIndex = 0;
            // 
            // tabPageEmployees
            // 
            this.tabPageEmployees.Controls.Add(this.labelEmployeeCount);
            this.tabPageEmployees.Controls.Add(this.buttonGenerateEmployees);
            this.tabPageEmployees.Controls.Add(this.dataGridViewEmployees);
            this.tabPageEmployees.Location = new System.Drawing.Point(4, 22);
            this.tabPageEmployees.Name = "tabPageEmployees";
            this.tabPageEmployees.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageEmployees.Size = new System.Drawing.Size(876, 535);
            this.tabPageEmployees.TabIndex = 0;
            this.tabPageEmployees.Text = "Munkatársak";
            this.tabPageEmployees.UseVisualStyleBackColor = true;
            // 
            // labelEmployeeCount
            // 
            this.labelEmployeeCount.AutoSize = true;
            this.labelEmployeeCount.Location = new System.Drawing.Point(190, 21);
            this.labelEmployeeCount.Name = "labelEmployeeCount";
            this.labelEmployeeCount.Size = new System.Drawing.Size(81, 13);
            this.labelEmployeeCount.TabIndex = 2;
            this.labelEmployeeCount.Text = "Munkatársak: 0";
            // 
            // buttonGenerateEmployees
            // 
            this.buttonGenerateEmployees.Location = new System.Drawing.Point(12, 12);
            this.buttonGenerateEmployees.Name = "buttonGenerateEmployees";
            this.buttonGenerateEmployees.Size = new System.Drawing.Size(160, 30);
            this.buttonGenerateEmployees.TabIndex = 0;
            this.buttonGenerateEmployees.Text = "Generate employees";
            this.buttonGenerateEmployees.UseVisualStyleBackColor = true;
            this.buttonGenerateEmployees.Click += new System.EventHandler(this.buttonGenerateEmployees_Click);
            // 
            // dataGridViewEmployees
            // 
            this.dataGridViewEmployees.AllowUserToAddRows = false;
            this.dataGridViewEmployees.AllowUserToDeleteRows = false;
            this.dataGridViewEmployees.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewEmployees.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewEmployees.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewEmployees.Location = new System.Drawing.Point(18, 64);
            this.dataGridViewEmployees.Name = "dataGridViewEmployees";
            this.dataGridViewEmployees.ReadOnly = true;
            this.dataGridViewEmployees.RowHeadersVisible = false;
            this.dataGridViewEmployees.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewEmployees.Size = new System.Drawing.Size(852, 471);
            this.dataGridViewEmployees.TabIndex = 1;
            // 
            // tabPageProjects
            // 
            this.tabPageProjects.Controls.Add(this.dataGridViewProjects);
            this.tabPageProjects.Controls.Add(this.labelAcceptedProjects);
            this.tabPageProjects.Controls.Add(this.buttonAcceptProject);
            this.tabPageProjects.Controls.Add(this.labelResult);
            this.tabPageProjects.Controls.Add(this.labelCost);
            this.tabPageProjects.Controls.Add(this.buttonRemoveMember);
            this.tabPageProjects.Controls.Add(this.listBoxTeam);
            this.tabPageProjects.Controls.Add(this.buttonAddMember);
            this.tabPageProjects.Controls.Add(this.numericUpDownHours);
            this.tabPageProjects.Controls.Add(this.labelHours);
            this.tabPageProjects.Controls.Add(this.comboBoxProgrammer);
            this.tabPageProjects.Controls.Add(this.labelProgrammer);
            this.tabPageProjects.Controls.Add(this.comboBoxProjectManager);
            this.tabPageProjects.Controls.Add(this.labelProjectManager);
            this.tabPageProjects.Controls.Add(this.numericUpDownBudget);
            this.tabPageProjects.Controls.Add(this.labelBudget);
            this.tabPageProjects.Controls.Add(this.textBoxProjectName);
            this.tabPageProjects.Controls.Add(this.labelProjectName);
            this.tabPageProjects.Location = new System.Drawing.Point(4, 22);
            this.tabPageProjects.Name = "tabPageProjects";
            this.tabPageProjects.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageProjects.Size = new System.Drawing.Size(876, 535);
            this.tabPageProjects.TabIndex = 1;
            this.tabPageProjects.Text = "Projektek";
            this.tabPageProjects.UseVisualStyleBackColor = true;
            // 
            // dataGridViewProjects
            // 
            this.dataGridViewProjects.AllowUserToAddRows = false;
            this.dataGridViewProjects.AllowUserToDeleteRows = false;
            this.dataGridViewProjects.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewProjects.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridViewProjects.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewProjects.Location = new System.Drawing.Point(400, 35);
            this.dataGridViewProjects.Name = "dataGridViewProjects";
            this.dataGridViewProjects.ReadOnly = true;
            this.dataGridViewProjects.RowHeadersVisible = false;
            this.dataGridViewProjects.Size = new System.Drawing.Size(464, 488);
            this.dataGridViewProjects.TabIndex = 17;
            // 
            // labelAcceptedProjects
            // 
            this.labelAcceptedProjects.AutoSize = true;
            this.labelAcceptedProjects.Location = new System.Drawing.Point(397, 15);
            this.labelAcceptedProjects.Name = "labelAcceptedProjects";
            this.labelAcceptedProjects.Size = new System.Drawing.Size(105, 13);
            this.labelAcceptedProjects.TabIndex = 16;
            this.labelAcceptedProjects.Text = "Elfogadott projektek:";
            // 
            // buttonAcceptProject
            // 
            this.buttonAcceptProject.Enabled = false;
            this.buttonAcceptProject.Location = new System.Drawing.Point(12, 400);
            this.buttonAcceptProject.Name = "buttonAcceptProject";
            this.buttonAcceptProject.Size = new System.Drawing.Size(160, 30);
            this.buttonAcceptProject.TabIndex = 15;
            this.buttonAcceptProject.Text = "Accept project";
            this.buttonAcceptProject.UseVisualStyleBackColor = true;
            this.buttonAcceptProject.Click += new System.EventHandler(this.buttonAcceptProject_Click);
            // 
            // labelResult
            // 
            this.labelResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelResult.Location = new System.Drawing.Point(12, 355);
            this.labelResult.Name = "labelResult";
            this.labelResult.Size = new System.Drawing.Size(375, 40);
            this.labelResult.TabIndex = 14;
            // 
            // labelCost
            // 
            this.labelCost.Location = new System.Drawing.Point(12, 318);
            this.labelCost.Name = "labelCost";
            this.labelCost.Size = new System.Drawing.Size(375, 35);
            this.labelCost.TabIndex = 13;
            this.labelCost.Text = "Számított költség: 0 RON";
            // 
            // buttonRemoveMember
            // 
            this.buttonRemoveMember.Location = new System.Drawing.Point(12, 285);
            this.buttonRemoveMember.Name = "buttonRemoveMember";
            this.buttonRemoveMember.Size = new System.Drawing.Size(110, 25);
            this.buttonRemoveMember.TabIndex = 12;
            this.buttonRemoveMember.Text = "Eltávolítás";
            this.buttonRemoveMember.UseVisualStyleBackColor = true;
            this.buttonRemoveMember.Click += new System.EventHandler(this.buttonRemoveMember_Click);
            // 
            // listBoxTeam
            // 
            this.listBoxTeam.FormattingEnabled = true;
            this.listBoxTeam.Location = new System.Drawing.Point(12, 165);
            this.listBoxTeam.Name = "listBoxTeam";
            this.listBoxTeam.Size = new System.Drawing.Size(375, 108);
            this.listBoxTeam.TabIndex = 11;
            // 
            // buttonAddMember
            // 
            this.buttonAddMember.Location = new System.Drawing.Point(237, 130);
            this.buttonAddMember.Name = "buttonAddMember";
            this.buttonAddMember.Size = new System.Drawing.Size(150, 25);
            this.buttonAddMember.TabIndex = 10;
            this.buttonAddMember.Text = "Hozzáadás a csapathoz";
            this.buttonAddMember.UseVisualStyleBackColor = true;
            this.buttonAddMember.Click += new System.EventHandler(this.buttonAddMember_Click);
            // 
            // numericUpDownHours
            // 
            this.numericUpDownHours.Location = new System.Drawing.Point(140, 133);
            this.numericUpDownHours.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.numericUpDownHours.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownHours.Name = "numericUpDownHours";
            this.numericUpDownHours.Size = new System.Drawing.Size(80, 20);
            this.numericUpDownHours.TabIndex = 9;
            this.numericUpDownHours.Value = new decimal(new int[] {
            40,
            0,
            0,
            0});
            // 
            // labelHours
            // 
            this.labelHours.AutoSize = true;
            this.labelHours.Location = new System.Drawing.Point(12, 135);
            this.labelHours.Name = "labelHours";
            this.labelHours.Size = new System.Drawing.Size(79, 13);
            this.labelHours.TabIndex = 8;
            this.labelHours.Text = "Tervezett órák:";
            // 
            // comboBoxProgrammer
            // 
            this.comboBoxProgrammer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxProgrammer.FormattingEnabled = true;
            this.comboBoxProgrammer.Location = new System.Drawing.Point(140, 102);
            this.comboBoxProgrammer.Name = "comboBoxProgrammer";
            this.comboBoxProgrammer.Size = new System.Drawing.Size(247, 21);
            this.comboBoxProgrammer.TabIndex = 7;
            // 
            // labelProgrammer
            // 
            this.labelProgrammer.AutoSize = true;
            this.labelProgrammer.Location = new System.Drawing.Point(12, 105);
            this.labelProgrammer.Name = "labelProgrammer";
            this.labelProgrammer.Size = new System.Drawing.Size(66, 13);
            this.labelProgrammer.TabIndex = 6;
            this.labelProgrammer.Text = "Programozó:";
            // 
            // comboBoxProjectManager
            // 
            this.comboBoxProjectManager.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxProjectManager.FormattingEnabled = true;
            this.comboBoxProjectManager.Location = new System.Drawing.Point(140, 72);
            this.comboBoxProjectManager.Name = "comboBoxProjectManager";
            this.comboBoxProjectManager.Size = new System.Drawing.Size(247, 21);
            this.comboBoxProjectManager.TabIndex = 5;
            this.comboBoxProjectManager.SelectedIndexChanged += new System.EventHandler(this.comboBoxProjectManager_SelectedIndexChanged);
            // 
            // labelProjectManager
            // 
            this.labelProjectManager.AutoSize = true;
            this.labelProjectManager.Location = new System.Drawing.Point(12, 75);
            this.labelProjectManager.Name = "labelProjectManager";
            this.labelProjectManager.Size = new System.Drawing.Size(94, 13);
            this.labelProjectManager.TabIndex = 4;
            this.labelProjectManager.Text = "Projektmenedzser:";
            // 
            // numericUpDownBudget
            // 
            this.numericUpDownBudget.DecimalPlaces = 2;
            this.numericUpDownBudget.Increment = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDownBudget.Location = new System.Drawing.Point(140, 42);
            this.numericUpDownBudget.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.numericUpDownBudget.Name = "numericUpDownBudget";
            this.numericUpDownBudget.Size = new System.Drawing.Size(150, 20);
            this.numericUpDownBudget.TabIndex = 3;
            this.numericUpDownBudget.ThousandsSeparator = true;
            this.numericUpDownBudget.ValueChanged += new System.EventHandler(this.numericUpDownBudget_ValueChanged);
            // 
            // labelBudget
            // 
            this.labelBudget.AutoSize = true;
            this.labelBudget.Location = new System.Drawing.Point(12, 45);
            this.labelBudget.Name = "labelBudget";
            this.labelBudget.Size = new System.Drawing.Size(105, 13);
            this.labelBudget.TabIndex = 2;
            this.labelBudget.Text = "Becsült keret (RON):";
            // 
            // textBoxProjectName
            // 
            this.textBoxProjectName.Location = new System.Drawing.Point(140, 12);
            this.textBoxProjectName.Name = "textBoxProjectName";
            this.textBoxProjectName.Size = new System.Drawing.Size(247, 20);
            this.textBoxProjectName.TabIndex = 1;
            // 
            // labelProjectName
            // 
            this.labelProjectName.AutoSize = true;
            this.labelProjectName.Location = new System.Drawing.Point(12, 15);
            this.labelProjectName.Name = "labelProjectName";
            this.labelProjectName.Size = new System.Drawing.Size(70, 13);
            this.labelProjectName.TabIndex = 0;
            this.labelProjectName.Text = "Projekt neve:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 561);
            this.Controls.Add(this.tabControlMain);
            this.MinimumSize = new System.Drawing.Size(800, 500);
            this.Name = "Form1";
            this.Text = "Projektcsapat-tervező";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControlMain.ResumeLayout(false);
            this.tabPageEmployees.ResumeLayout(false);
            this.tabPageEmployees.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEmployees)).EndInit();
            this.tabPageProjects.ResumeLayout(false);
            this.tabPageProjects.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewProjects)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHours)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownBudget)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControlMain;
        private System.Windows.Forms.TabPage tabPageEmployees;
        private System.Windows.Forms.Label labelEmployeeCount;
        private System.Windows.Forms.Button buttonGenerateEmployees;
        private System.Windows.Forms.DataGridView dataGridViewEmployees;
        private System.Windows.Forms.TabPage tabPageProjects;
        private System.Windows.Forms.DataGridView dataGridViewProjects;
        private System.Windows.Forms.Label labelAcceptedProjects;
        private System.Windows.Forms.Button buttonAcceptProject;
        private System.Windows.Forms.Label labelResult;
        private System.Windows.Forms.Label labelCost;
        private System.Windows.Forms.Button buttonRemoveMember;
        private System.Windows.Forms.ListBox listBoxTeam;
        private System.Windows.Forms.Button buttonAddMember;
        private System.Windows.Forms.NumericUpDown numericUpDownHours;
        private System.Windows.Forms.Label labelHours;
        private System.Windows.Forms.ComboBox comboBoxProgrammer;
        private System.Windows.Forms.Label labelProgrammer;
        private System.Windows.Forms.ComboBox comboBoxProjectManager;
        private System.Windows.Forms.Label labelProjectManager;
        private System.Windows.Forms.NumericUpDown numericUpDownBudget;
        private System.Windows.Forms.Label labelBudget;
        private System.Windows.Forms.TextBox textBoxProjectName;
        private System.Windows.Forms.Label labelProjectName;
    }
}
