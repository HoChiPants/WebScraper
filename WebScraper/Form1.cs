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
        private bool _unlimited;
        

        public Form1()
        {
            //pupulates the drop down for chosing semester and year
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
            //gives numerical value for spring, summer, or fall and sets the global variable to keep track
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
            //sets the year and makes sure its two digits
            string temp = Year.SelectedItem.ToString();
            if (temp.Length == 1)
            {
                temp = 0 + Year.SelectedItem.ToString();
            }
            _year = temp;
        }

        private void GetData_Click(object sender, EventArgs e)
        {
            //There are three options for when get data is selected. First, you put in the year and semester. Second, you put in the class. Third, you havent put in anything
            if (_year != null && _semester != 0)
            {
                //calls the get enrolments for the webpage to get all the data it wants
                salenium = new Salenium();
                setDataGrid(salenium.getEnrollments("https://student.apps.utah.edu/uofu/stu/ClassSchedules/main/1" + _year + _semester + "/class_list.html?subject=CS", _year,_semester, textBox3.Text, _unlimited));
                //clear year and semster global variables and clears the text box
                makeAllNull();
            }
            else if (_dep_num != null)
            {
                //calls get description with the class
                salenium = new Salenium();
                string des = salenium.getCreditsAndDesc(_dep_num,true)[1];
                string newLine = Environment.NewLine;
                //if a class could not be found
                if (des == null)
                {
                    MessageBox.Show("Class not found." + newLine + "Check syntax and try again");
                }
                else
                {
                    if (des.Length > 15)
                    {
                        //formats the textbox to loook better
                        textBox2.Text += _dep_num + ":" + newLine;
                        textBox2.Text += des;
                        textBox2.Text += newLine;
                        textBox2.Text += newLine;
                    }
                    else
                    {
                        textBox2.Text += "Class Not Found" + newLine + newLine;
                    }
                }
                //clear the text box and the global variable
                makeAllNull();
            }
            else
            {
                //an error message to have them put in data
                MessageBox.Show("Please choose either a year and semester or a course");
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //sets global variable
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
                //pupulates the datagridview with the dictionary of data you just retrieved
                if (dict[col].Count > 4)
                {
                    dataGridView1.Rows.Add(dict[col][0], dict[col][1], dict[col][5], dict[col][7], dict[col][2], getsemester(dict[col][3]), dict[col][4], dict[col][6]);
                }
                else
                {
                    dataGridView1.Rows.Add("Data Not Found", "", "", "", "", "", "", "");
                }
            }
    
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            //makes sure the amount of courses you want is a number and not letters
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
                //saves the datagridview as a csv file format
                var sb = new StringBuilder();

                

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
            //sets the semester from numerical to string
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

        private void makeAllNull()
        {
            _semester = 0;
            _year = null;
            _dep_num = null;
            Year.Text = "Year";
            Semester.Text = "Semester";
            textBox1.Text = string.Empty;
            textBox3.Text = string.Empty;
            unlimited.Checked = false;
            
        }

        private void unlimited_CheckedChanged(object sender, EventArgs e)
        {
            if(unlimited.Checked)
                {
                _unlimited = true;
            }
        }
    }
}
