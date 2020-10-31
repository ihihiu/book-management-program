using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BookManager
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();

            dataGridView1.DataSource = DataManager.Users;
            dataGridView1.CurrentCellChanged += DataGridView1_CurrentCellChanged;

            button1.Click += (sender, e) =>
            {
                try
                {
                    if (DataManager.Users.Exists((x) => x.Id == int.Parse(textBox1.Text)))
                        MessageBox.Show("사용자 ID가 이미 존재합니다.");
                    else
                    {
                        User user = new User()
                        {
                            Id = int.Parse(textBox1.Text),
                            Name = textBox2.Text
                        };
                        DataManager.Users.Add(user);

                        dataGridView1.DataSource = null;
                        dataGridView1.DataSource = DataManager.Users;
                    }
                }
                catch (Exception ex) { }
            };
            button2.Click += (sender, e) =>
            {
                try
                {
                    User user = DataManager.Users.Single((x) => x.Id == int.Parse(textBox1.Text));
                    user.Name = textBox2.Text;

                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = DataManager.Users;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("존재하지 않는 사용자입니다.");
                }           
            };
            button3.Click += (sender, e) =>
            {
                try
                {
                    User user = DataManager.Users.Single((x) => x.Id == int.Parse(textBox1.Text));
                    DataManager.Users.Remove(user);

                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = DataManager.Users;
                }catch(Exception ex)
                {
                    MessageBox.Show("존재하지 않는 사용자입니다.");
                }
            };
        }

        private void DataGridView1_CurrentCellChanged(object sender, EventArgs e)
        {
            try
            {
                User user = dataGridView1.CurrentRow.DataBoundItem as User;
                textBox1.Text = user.Id.ToString();
                textBox2.Text = user.Name;
            }catch(Exception ex)
            {

            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
