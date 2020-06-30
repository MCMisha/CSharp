using System;
using System.Drawing;
using System.Windows.Forms;

namespace Client
{
    public partial class Form1 : Form
    {
        readonly Client client = new Client();
        bool enableSending = false;
        public Form1()
        {
            InitializeComponent();
        }
        private void Button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Введите логин", "Сообщение");
                return;
            }
            if (textBox1.Text.Contains(" "))
            {
                MessageBox.Show("Логин не должен содержать пробелы", "Сообщение");
                return;
            }
            if (textBox2.Text.Contains(" "))
            {
                MessageBox.Show("Пароль не должен содержать пробелы", "Сообщение");
                return;
            }
            if (textBox2.Text != textBox3.Text)
            {
                MessageBox.Show("Пароли должны совпадать", "Сообщение");
                return;
            }
            string answer = client.requestForRegistration(textBox1.Text, textBox2.Text);
            if (answer == "Регистрация прошла успешна")
            {
                enableSending = true;
                Update();
            }
            MessageBox.Show(answer, "Ответ сервера");
        }
        private void Button2_Click(object sender, EventArgs e)
        {
            if (client.requestForChecking() == "OK")
            {
                textBox4.BackColor = Color.Green;
            }
            else
            {
                textBox4.BackColor = Color.Red;
            }
        }
        private void Button3_Click(object sender, EventArgs e)
        {
            if (textBox6.Text == "")
            {
                MessageBox.Show("Введите логин", "Сообщение");
                return;
            }
            if (textBox6.Text.Contains(" "))
            {
                MessageBox.Show("Логин не может содержать пробелы", "Сообщение");
                return;
            }
            if (textBox5.Text == "")
            {
                MessageBox.Show("Введите пароль", "Сообщение");
                return;
            }
            if (textBox5.Text.Contains(" "))
            {
                MessageBox.Show("Пароль не должен содержать пробелы", "Сообщение");
                return;
            }
            string answer = client.requestForAutorization(textBox6.Text, textBox5.Text);
            if (answer == "Авторизация прошла успешна")
            {
                enableSending = true;
                Update();
            }
            MessageBox.Show(answer, "Ответ сервера");
        }
        private void Button4_Click(object sender, EventArgs e)
        {
            if (textBox7.Text == "")
            {
                MessageBox.Show("Введите сообщение", "Сообщение");
                return;
            }
            textBox8.Text = client.requestForSendingMessage(textBox7.Text);
        }
        private new void Update()
        {
            if (enableSending == true)
            {
                textBox1.Text = "";
                textBox1.Enabled = false;
                textBox2.Text = "";
                textBox2.Enabled = false;
                textBox3.Text = "";
                textBox3.Enabled = false;
                button1.Enabled = false;
                textBox6.Text = "";
                textBox6.Enabled = false;
                textBox5.Text = "";
                textBox5.Enabled = false;
                button3.Enabled = false;
                textBox7.Enabled = true;
                button4.Enabled = true;
            }
            else
            {
                textBox1.Enabled = true;
                textBox2.Enabled = true;
                textBox3.Enabled = true;
                button1.Enabled = true;
                textBox6.Enabled = true;
                textBox5.Enabled = true;
                button3.Enabled = true;
                textBox7.Text = "";
                textBox7.Enabled = false;
                button4.Enabled = false;
            }
        }
    }
}