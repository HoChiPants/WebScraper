namespace WebScraper
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.Semester = new System.Windows.Forms.ComboBox();
            this.Year = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.GetData = new System.Windows.Forms.Button();
            this.DepNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.CourseNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CourseCredits = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CourseTitle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CourseEnrolment = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CourseSemester = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CourseYear = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CourseDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.save = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DepNum,
            this.CourseNum,
            this.CourseCredits,
            this.CourseTitle,
            this.CourseEnrolment,
            this.CourseSemester,
            this.CourseYear,
            this.CourseDescription});
            this.dataGridView1.Location = new System.Drawing.Point(12, 161);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 62;
            this.dataGridView1.RowTemplate.Height = 28;
            this.dataGridView1.Size = new System.Drawing.Size(621, 537);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 81);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(328, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Find all classes taught by Semester and Year";
            // 
            // Semester
            // 
            this.Semester.FormattingEnabled = true;
            this.Semester.Location = new System.Drawing.Point(33, 110);
            this.Semester.Name = "Semester";
            this.Semester.Size = new System.Drawing.Size(121, 28);
            this.Semester.TabIndex = 2;
            this.Semester.Text = "Semester";
            this.Semester.SelectedIndexChanged += new System.EventHandler(this.Semester_SelectedIndexChanged);
            // 
            // Year
            // 
            this.Year.FormattingEnabled = true;
            this.Year.Location = new System.Drawing.Point(219, 110);
            this.Year.Name = "Year";
            this.Year.Size = new System.Drawing.Size(121, 28);
            this.Year.TabIndex = 3;
            this.Year.Text = "Year";
            this.Year.SelectedIndexChanged += new System.EventHandler(this.Year_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(807, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(376, 20);
            this.label2.TabIndex = 4;
            this.label2.Text = "Find course descriptions by department and number";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(915, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(136, 20);
            this.label3.TabIndex = 5;
            this.label3.Text = "Example: CS3100";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(919, 112);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(122, 26);
            this.textBox1.TabIndex = 6;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // GetData
            // 
            this.GetData.Location = new System.Drawing.Point(706, 433);
            this.GetData.Name = "GetData";
            this.GetData.Size = new System.Drawing.Size(225, 66);
            this.GetData.TabIndex = 7;
            this.GetData.Text = "Get Data";
            this.GetData.UseVisualStyleBackColor = true;
            this.GetData.Click += new System.EventHandler(this.GetData_Click);
            // 
            // DepNum
            // 
            this.DepNum.HeaderText = "Department";
            this.DepNum.MinimumWidth = 8;
            this.DepNum.Name = "DepNum";
            this.DepNum.Width = 150;
            // 
            // textBox2
            // 
            this.textBox2.AllowDrop = true;
            this.textBox2.Location = new System.Drawing.Point(706, 161);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(506, 234);
            this.textBox2.TabIndex = 8;
            this.textBox2.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // CourseNum
            // 
            this.CourseNum.HeaderText = "Course Number";
            this.CourseNum.MinimumWidth = 8;
            this.CourseNum.Name = "CourseNum";
            this.CourseNum.Width = 150;
            // 
            // CourseCredits
            // 
            this.CourseCredits.HeaderText = "Course Credits";
            this.CourseCredits.MinimumWidth = 8;
            this.CourseCredits.Name = "CourseCredits";
            this.CourseCredits.Width = 150;
            // 
            // CourseTitle
            // 
            this.CourseTitle.HeaderText = "CourseTitle";
            this.CourseTitle.MinimumWidth = 8;
            this.CourseTitle.Name = "CourseTitle";
            this.CourseTitle.Width = 150;
            // 
            // CourseEnrolment
            // 
            this.CourseEnrolment.HeaderText = "Course Enrolment";
            this.CourseEnrolment.MinimumWidth = 8;
            this.CourseEnrolment.Name = "CourseEnrolment";
            this.CourseEnrolment.Width = 150;
            // 
            // CourseSemester
            // 
            this.CourseSemester.HeaderText = "Course Semester";
            this.CourseSemester.MinimumWidth = 8;
            this.CourseSemester.Name = "CourseSemester";
            this.CourseSemester.Width = 150;
            // 
            // CourseYear
            // 
            this.CourseYear.HeaderText = "Course Year";
            this.CourseYear.MinimumWidth = 8;
            this.CourseYear.Name = "CourseYear";
            this.CourseYear.Width = 150;
            // 
            // CourseDescription
            // 
            this.CourseDescription.HeaderText = "Course Descritpion";
            this.CourseDescription.MinimumWidth = 8;
            this.CourseDescription.Name = "CourseDescription";
            this.CourseDescription.Width = 150;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(426, 110);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(100, 26);
            this.textBox3.TabIndex = 9;
            this.textBox3.TextChanged += new System.EventHandler(this.textBox3_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(363, 81);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(229, 20);
            this.label4.TabIndex = 10;
            this.label4.Text = "How many do you want to see?";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // save
            // 
            this.save.Location = new System.Drawing.Point(706, 543);
            this.save.Name = "save";
            this.save.Size = new System.Drawing.Size(225, 68);
            this.save.TabIndex = 11;
            this.save.Text = "Save course information";
            this.save.UseVisualStyleBackColor = true;
            this.save.Click += new System.EventHandler(this.save_Click);
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(807, 66);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(310, 43);
            this.label5.TabIndex = 12;
            this.label5.Text = "If you would like multiple, please search them individually. They will be added b" +
    "elow";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1244, 736);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.save);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.GetData);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Year);
            this.Controls.Add(this.Semester);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridView1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox Semester;
        private System.Windows.Forms.ComboBox Year;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button GetData;
        private System.Windows.Forms.DataGridViewTextBoxColumn DepNum;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.DataGridViewTextBoxColumn CourseNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn CourseCredits;
        private System.Windows.Forms.DataGridViewTextBoxColumn CourseTitle;
        private System.Windows.Forms.DataGridViewTextBoxColumn CourseEnrolment;
        private System.Windows.Forms.DataGridViewTextBoxColumn CourseSemester;
        private System.Windows.Forms.DataGridViewTextBoxColumn CourseYear;
        private System.Windows.Forms.DataGridViewTextBoxColumn CourseDescription;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button save;
        private System.Windows.Forms.Label label5;
    }
}

