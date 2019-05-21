using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DUZP_Testing
{
    
    public partial class Form1 : Form
    {
        int testIndex = 0, currentScore = 0;
        
        public TestSuite a = new TestSuite();
        String filepath;
        FileStream fs, fs2;
        public Form1()
        {
            InitializeComponent();
        }

        private void ВыходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            a = new TestSuite();
            saveFileDialog1.Filter = "Test Data|*.test";
            saveFileDialog1.Title = "Save a test file";
            saveFileDialog1.ShowDialog();
            if (saveFileDialog1.FileName != "")
            {
                filepath = saveFileDialog1.FileName;
                fs = new FileStream(saveFileDialog1.FileName, FileMode.Create);
                fs2 = new FileStream(saveFileDialog1.FileName + ".resultdata", FileMode.Create);
                
                comboBox1.Items.Clear();
                comboBox1.Items.Add("0");
            }
           
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            if( textBox3.Text != "" && textBox7.Text != "" && textBox4.Text != "" && textBox5.Text != "" && textBox6.Text != "" && comboBox1.SelectedIndex >=0)
            {
                Question b = new Question();
                b.quest = textBox3.Text;
                b.ans[0] = textBox4.Text;
                b.ans[1] = textBox5.Text;
                b.ans[2] = textBox6.Text;
                b.ans[3] = textBox7.Text;
                if (radioButton5.Checked)
                {
                    b.rans = 0;
                }
                if (radioButton6.Checked)
                {
                    b.rans = 1;
                }
                if (radioButton7.Checked)
                {
                    b.rans = 2;
                }
                if (radioButton8.Checked)
                {
                    b.rans = 3;
                }
                a.tests.Insert( comboBox1.SelectedIndex, b);
                if(comboBox1.SelectedIndex == comboBox1.Items.Count-1)
                {
                    comboBox1.Items.Add((comboBox1.Items.Count ).ToString());
                    comboBox1.SelectedItem = (comboBox1.Items.Count-1);
                }
              
            }
            else
            {
                MessageBox.Show("Нет открытого файла, или информация о вопросе не полна.");
            }
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            if(textBox9.Text == "PASSWORD")
            {
                if(a.tests.Count >0 && textBox1.Text != "" && textBox2.Text != "")
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    try
                    {
                        formatter.Serialize(fs, a);
                    }
                    catch (SerializationException exp)
                    {
                        Console.WriteLine("Failed to serialize. Reason: " + exp.Message);
                        throw;
                    }
                    finally
                    {
                        fs.Close();
                        fs2.Close();
                        MessageBox.Show("Тест сохранён.");
                    }
                }
                else
                {
                    MessageBox.Show("Информация о тесте не заполнена, или тест не содержит ни одного вопроса." +
                        " \n Заполните необходимую информацию и попробуйте ещё раз.");
                }
            }
            else
            {
                MessageBox.Show("Неправильный пароль, попробуйте ещё раз.");
            }
        }

        private void ToolStripTextBox1_Click(object sender, EventArgs e)
        {

        }

        private void ВойтиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(toolStripTextBox1.Text == "PASSWORD")
            {
                ((Control)tabControl1.TabPages[1]).Enabled = true;
                ((Control)tabControl1.TabPages[2]).Enabled = true;
                toolStripTextBox1.Text = "";
            }
        }

        private void MenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ((Control)tabControl1.TabPages[1]).Enabled = false;
            ((Control)tabControl1.TabPages[2]).Enabled = false;
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            label7.Text = "ID: " + comboBox1.SelectedIndex;
            if (a.tests.Count > comboBox1.SelectedIndex)
            {
              
                textBox3.Text = a.tests[comboBox1.SelectedIndex].quest;
                textBox7.Text = a.tests[comboBox1.SelectedIndex].ans[0];
                textBox4.Text = a.tests[comboBox1.SelectedIndex].ans[1];
                textBox5.Text = a.tests[comboBox1.SelectedIndex].ans[2];
                textBox6.Text = a.tests[comboBox1.SelectedIndex].ans[3];

                switch (a.tests[comboBox1.SelectedIndex].rans)
                {
                    case 1:
                        radioButton5.Checked = false;
                        radioButton6.Checked = true;
                        radioButton7.Checked = false;
                        radioButton8.Checked = false;
                        break;
                    case 2:
                        radioButton5.Checked = false;
                        radioButton6.Checked = false;
                        radioButton7.Checked = true;
                        radioButton8.Checked = false;
                        break;
                    case 3:
                        radioButton5.Checked = false;
                        radioButton6.Checked = false;
                        radioButton7.Checked = false;
                        radioButton8.Checked = true;
                        break;
                    case 0:
                        radioButton5.Checked = true;
                        radioButton6.Checked = false;
                        radioButton7.Checked = false;
                        radioButton8.Checked = false;
                        break;
                }

            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked && a.tests[testIndex].rans == 0)
            {
                currentScore++;
            }
            if (radioButton2.Checked && a.tests[testIndex].rans == 1)
            {
                currentScore++;
            }
            if (radioButton3.Checked && a.tests[testIndex].rans == 2)
            {
                currentScore++;
            }
            if (radioButton4.Checked && a.tests[testIndex].rans == 3)
            {
                currentScore++;
            }
            toolStripProgressBar1.Value ++;
            testIndex++;
            if (testIndex < a.tests.Count)
            {
                label4.Text = "ВОПРОС:" + a.tests[testIndex].quest;
                radioButton1.Text = "1)" + a.tests[testIndex].ans[0];
                radioButton2.Text = "2)" + a.tests[testIndex].ans[1];
                radioButton3.Text = "3)" + a.tests[testIndex].ans[2];
                radioButton4.Text = "4)" + a.tests[testIndex].ans[3];
            }
            else
            {
                MessageBox.Show("Тест завершён, ваш счёт: " + currentScore);
                BinaryFormatter formatter = new BinaryFormatter();
                Result res = new Result();
                res.name = textBox10.Text;
                res.sur = textBox11.Text;
                res.group = textBox12.Text;
                res.result = currentScore.ToString();

                try
                {
                    formatter.Serialize(fs2, res);
                }
                catch (SerializationException exp)
                {
                    Console.WriteLine("Failed to serialize. Reason: " + exp.Message);
                    throw;
                }
                finally
                {
                    fs.Close();fs2.Close();
                    MessageBox.Show("Результат сохранён.");
                    button1.Enabled = false;
                    currentScore = 0;
                }
            }
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Test Data|*.test";
            openFileDialog1.Title = "Open a test file";
            openFileDialog1.ShowDialog();
            if (openFileDialog1.FileName != "")
            {
                fs = new FileStream(openFileDialog1.FileName, FileMode.Open);
                fs2 = new FileStream(openFileDialog1.FileName + ".resultdata", FileMode.Open);

                var reader = new BinaryFormatter();
                try
                {
                    a = ((TestSuite)reader.Deserialize(fs));
                }
                catch (System.Runtime.Serialization.SerializationException)
                {
                    Console.WriteLine("File is empty or corrupted");
                }
                finally
                {
                    comboBox1.Items.Clear();
                    foreach(Question q in a.tests)
                    {
                        comboBox1.Items.Add(a.tests.IndexOf(q));
                    }
                }
            }
            }

        private void Button5_Click(object sender, EventArgs e)
        {
            if (textBox8.Text == "PASSWORD")
            {
                openFileDialog2.Filter = "Test Results|*.resultdata";
                openFileDialog2.Title = "Open a result file";
                openFileDialog2.ShowDialog();
                if (openFileDialog2.FileName != "")
                {

                    fs2 = new FileStream(openFileDialog2.FileName, FileMode.Open);

                    var reader = new BinaryFormatter();
                    List<Result> res = new List<Result>();
                    try
                    {
                        while (fs2.Position < fs2.Length)
                        {
                            res.Add((Result)reader.Deserialize(fs2));

                        }

                    }
                    catch (System.Runtime.Serialization.SerializationException)
                    {
                        Console.WriteLine("File is empty or corrupted");
                    }
                    finally
                    {
                        for (int i = 0; i < res.Count; i++)
                        {
                            ListViewItem temp = listView1.Items.Add(i.ToString());
                            temp.SubItems.Add(res[i].name);
                            temp.SubItems.Add(res[i].sur);
                            temp.SubItems.Add(res[i].group);
                            temp.SubItems.Add(res[i].result);
                        }
                    }
                }
            }
        }

        private void OpenТестToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Test Data|*.test";
            openFileDialog1.Title = "Open a test file";
            openFileDialog1.ShowDialog();
            if (openFileDialog1.FileName != "")
            {
                fs = new FileStream(openFileDialog1.FileName, FileMode.Open);
                fs2 = new FileStream(openFileDialog1.FileName + ".resultdata", FileMode.Append);

                var reader = new BinaryFormatter();
                try
                {
                   a = ((TestSuite)reader.Deserialize(fs)); 
                }
                catch (System.Runtime.Serialization.SerializationException)
                {
                    Console.WriteLine("File is empty or corrupted");
                }
                finally
                {
                    
                    label1.Text = "Название теста: " + a.testName;
                    label3.Text = "Всего вопросов: " + a.tests.Count;
                    toolStripProgressBar1.Maximum = a.tests.Count;
                    toolStripProgressBar1.Step = 1;
                    toolStripProgressBar1.Value = 0;

                    testIndex = 0;
                    label4.Text = "ВОПРОС:" + a.tests[0].quest;
                    radioButton1.Text = "1)" + a.tests[0].ans[0];
                    radioButton2.Text = "2)" + a.tests[0].ans[1];
                    radioButton3.Text = "3)" + a.tests[0].ans[2];
                    radioButton4.Text = "4)" + a.tests[0].ans[3];
                    button1.Enabled = true;
                }
            }
        }

        private void ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
