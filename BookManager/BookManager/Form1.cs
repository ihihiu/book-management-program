using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BookManager
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            label5.Text = DataManager.Books.Count.ToString();
            label6.Text = DataManager.Users.Count.ToString();
            label7.Text = DataManager.Books.Where((x) => x.isBorrowed).Count().ToString();
            label8.Text = DataManager.Books.
                Where((x) => { return x.isBorrowed 
                    && x.BorrowedAt.AddDays(7) < DateTime.Now; })
                .Count().ToString();

            dataGridView1.DataSource = DataManager.Books;
            dataGridView2.DataSource = DataManager.Users;

            dataGridView1.CurrentCellChanged += DataGridView1_CurrentCellChanged;
            dataGridView2.CurrentCellChanged += DataGridView2_CurrentCellChanged;

            button1.Click += Button1_Click;
            button2.Click += Button2_Click;
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim() == "")
            {
                MessageBox.Show("도서를 먼저 입력하세요.");
            }
            else
            {
                try
                {
                    Book book = DataManager.Books.Single((x) => x.ISbn == textBox1.Text);
                    if (book.isBorrowed)
                    {
                        book.UserId = 0;
                        book.UserName = "";
                        book.isBorrowed = false;
                        //book.BorrowedAt = new DateTime();

                        dataGridView1.DataSource = null;
                        dataGridView1.DataSource = DataManager.Books;

                        if (book.BorrowedAt.AddDays(7) < DateTime.Now)
                            MessageBox.Show(book.Name + "이/가 연체 상태로 반납되었습니다.");
                        else
                            MessageBox.Show(book.Name + "이 반납되었습니다.");
                    }
                    else
                        MessageBox.Show("대여 상태가 아닙니다.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("존재하지 않는 도서입니다.");
                }
            }

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim() == "")
            {
                MessageBox.Show("도서를 먼저 선택해 주십시오.");
            }
            else if (textBox3.Text.Trim() == "")
            {
                MessageBox.Show("사용자를 선택해 주십시오.");
            }
            else
            {
                try
                {
                    Book book = DataManager.Books.Single((x) => x.ISbn == textBox1.Text);
                    if (book.isBorrowed)
                        MessageBox.Show("이미 대여 중인 도서입니다.");
                    else
                    {
                        User user = DataManager.Users.Single((x) => x.Id.ToString() == textBox3.Text);
                        book.UserId = user.Id;
                        book.UserName = user.Name;
                        book.isBorrowed = true;
                        book.BorrowedAt = DateTime.Now;

                        dataGridView1.DataSource = null;
                        dataGridView1.DataSource = DataManager.Books;

                        MessageBox.Show(book.Name + "이/가 " + user.Name + " 님께 대여되었습니다.");

                    }
                }catch(Exception exception)
                {
                    MessageBox.Show("존재하지 않는 도서 또는 사용자입니다.");
                }
            }
        }

        private void DataGridView2_CurrentCellChanged(object sender, EventArgs e)   //도서 현황   //사용자
        {
            try
            {
                User user=dataGridView2.CurrentRow.DataBoundItem as User;
                textBox3.Text = user.Id.ToString();
            }
            catch (Exception exception)
            {

            }
        }

        private void DataGridView1_CurrentCellChanged(object sender, EventArgs e)
        {
            try
            {
                Book book = dataGridView1.CurrentRow.DataBoundItem as Book;
                textBox1.Text = book.ISbn;
                textBox2.Text = book.Name;
            }
            catch (Exception exception)
            {

            }
            
        }
        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem1_Click_3(object sender, EventArgs e)
        {
            new Form2().ShowDialog();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            new Form3().ShowDialog();
        }
    }
}
