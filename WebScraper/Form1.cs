using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WebScraper
{
    public partial class Form1 : Form
    {
        private string _dep_num;
        private string _year;
        private int _semester = 0;
        private static Salenium salenium;
        

        public Form1()
        {
            InitializeComponent();
            Semester.Items.Add("Spring");
            Semester.Items.Add("Summer");
            Semester.Items.Add("Fall");
            for (int i = 01; i < 21; i++)
            {
                Year.Items.Add(i);
            }
        }

        private void Semester_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Semester.SelectedItem.ToString() == "Spring")
            {
                _semester = 4;
            }
            else if (Semester.SelectedItem.ToString() == "Summer")
            {
                _semester = 6;
            }
            else
            {
                _semester = 8;
            }
        }

        private void Year_SelectedIndexChanged(object sender, EventArgs e)
        {
            string temp = Year.SelectedItem.ToString();
            if (temp.Length == 1)
            {
                temp = 0 + Year.SelectedItem.ToString();
            }
            _year = temp;
        }

        private void GetData_Click(object sender, EventArgs e)
        {
            if (_year != null && _semester != 0)
            {
                salenium = new Salenium();
                setDataGrid(salenium.getEnrollments("https://student.apps.utah.edu/uofu/stu/ClassSchedules/main/1" + _year + _semester + "/class_list.html?subject=CS", _year,_semester, textBox3.Text));
                Year.Text = string.Empty;
                Semester.Text = string.Empty;
                _year = null;
                _semester = 0;
                textBox3.Text = string.Empty;
            }
            else if (_dep_num != null)
            {
                salenium = new Salenium();
                string des = salenium.getDescription(_dep_num);
                string newLine = Environment.NewLine;
                if (des == null)
                {
                    MessageBox.Show("Class not found." + newLine + "Check syntax and try again");
                }
                else
                {
                    textBox2.Text += _dep_num + ":" + newLine;
                    textBox2.Text += des;
                    textBox2.Text += newLine;
                    textBox2.Text += newLine;
                }
                textBox1.Clear();
                _dep_num = null;
            }
            else
            {
                MessageBox.Show("Please choose either a year and semester or a course");
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            _dep_num = textBox1.Text;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void setDataGrid(Dictionary<int,List<string>> dict)
        {
            if(dict == null)
            {
                MessageBox.Show("There was an error with the connection" + Environment.NewLine + "Please try again");
                return;
            }
            foreach (int col in dict.Keys)
            {
                dataGridView1.Rows.Add(dict[col][0],dict[col][1], dict[col][2], dict[col][3], dict[col][6], getsemester(dict[col][4]), dict[col][5], dict[col][7]);
            }
    
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if(System.Text.RegularExpressions.Regex.IsMatch(textBox3.Text, "[^0-9]"))
            {
                MessageBox.Show("please use only numbers" + Environment.NewLine + "Thanks");
                textBox3.Text = string.Empty;
            }
            else{
                
            }
            
        }

        private void save_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 1)
            {
                MessageBox.Show("please search for a classes information before saving");
            }
            else
            {
                var sb = new StringBuilder();

                var headers = dataGridView1.Columns.Cast<DataGridViewColumn>();
                sb.AppendLine(string.Join(",", headers.Select(column => "\"" + column.HeaderText + "\"").ToArray()));

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    var cells = row.Cells.Cast<DataGridViewCell>();
                    sb.AppendLine(string.Join(",", cells.Select(cell => "\"" + cell.Value + "\"").ToArray()));
                }
                sb.Remove(sb.Length - 25, 25);
                using (SaveFileDialog save = new SaveFileDialog() { Filter = "CSV|*.csv", ValidateNames = true })
                {
                    if (save.ShowDialog() == DialogResult.OK)
                    {
                        File.WriteAllText(save.FileName, sb.ToString());
                    }
                }
            }
        }

        private string getsemester (string sem)
        {
            if (sem == "4" )
            {
                return "Spring";
            }
            else if (sem == "6" )
            {
                return "Summer";
            }
            else
            {
                return "Fall";
            }
        }

    }
}
