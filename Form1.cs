using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace calculator_backup_new
{
    public partial class Form1 : Form
    {
        List<string> history = new List<string>();
        bool status = false;
        double num1, num2;
        string process = "";
        int factorial = 1;
        public Form1()
        {
            InitializeComponent();
        }
        private void dataSaver(string process) // history saver
        {
            history.Add(process);
            label3.Text = string.Join("\n", history.ToArray().Reverse().Take(10).Reverse().ToList().Select(x => x));
        }
        private void numbers(object sender, EventArgs e) //numbers
        {
            if (textBox1.Text == "0" || status)
                textBox1.Clear();
            status = false;
            Button button = (Button)sender;
            if (textBox1.Text == "."){
                if (!textBox1.Text.Contains(".")){
                    textBox1.Text += button.Text;
                }
            }
            else textBox1.Text += button.Text;
        }
        private void button11_Click(object sender, EventArgs e) // = 
        {  
            switch (process){
                case "+": num2 = num1 + double.Parse(textBox1.Text); break;
                case "-": num2 = num1 - double.Parse(textBox1.Text); break;
                case "*": num2 = num1 * double.Parse(textBox1.Text); break;
                case "÷": num2 = num1 / double.Parse(textBox1.Text); break;
                default: num2 = 0; break;
            }
            label5.Text = "num2";
            label4.Text = textBox1.Text;
            dataSaver($"{num1} {process} {textBox1.Text} = {num2}");
            textBox1.Text = num2.ToString();
            num1 = 0;
        }
        private void button19_Click(object sender, EventArgs e) // delete all
        {
            textBox1.Clear();
            DeleteLabel();
            textBox1.Text = "0";
        }
        void DeleteLabel() 
        {
            label5.Text = "";
            label2.Text = "";
            label4.Text = "";
            label1.Text = "";
        }
        private void button33_Click(object sender, EventArgs e) // delete single
        {
            DeleteLabel();
            int index = textBox1.Text.Length;
            index--;
            textBox1.Text = textBox1.Text.Remove(index);
            if (textBox1.Text.Length == 0)
            {
                textBox1.Text = "0";
            }
        }
        private void button15_Click(object sender, EventArgs e) //square root
        {
            if (textBox1.Text.Length > 0)
            {
                num2 = Math.Pow(double.Parse(textBox1.Text), 0.5);
                num2 = Math.Round(num2, 3);
                dataSaver($"√{textBox1.Text} = {num2}");
                textBox1.Text = num2.ToString();
                num1 = 0;
            }
        }
        private void operators(object sender, EventArgs e) // + - * ÷
        {
            label2.Text = "num1";
            label1.Text = textBox1.Text;
            Button button = (Button)sender;
            process= button.Text;
            num1 += double.Parse(textBox1.Text);
            textBox1.Clear();
        }
        private void Form1_Load(object sender, EventArgs e) //button size set
        {
            Yuvarlakbuton ybuton = new Yuvarlakbuton();
            ybuton.Size = new Size(80, 80);
            ybuton.Location = new System.Drawing.Point(27,12);
            ybuton.BackColor = Color.Red;
            ybuton.FlatAppearance.BorderSize = 0;
            ybuton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            ybuton.FlatAppearance.BorderColor = Color.Red;
            ybuton.Text = "OFF";
            ybuton.ForeColor = Color.Black;
            ybuton.Font = new Font("Calibri", 23);
            this.Controls.Add(ybuton);
            ybuton.Click += new EventHandler(ybuton_click);

            Yuvarlakbuton ybut2 = new Yuvarlakbuton();
            ybut2.Size = new Size(80, 80);
            ybut2.Location = new System.Drawing.Point(27,100);
            ybut2.BackColor = Color.Aqua;
            ybut2.FlatAppearance.BorderSize = 0;
            ybut2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            ybut2.Text = "HİSTORY\nDEL";
            ybut2.ForeColor = Color.Black;
            ybut2.Font = new Font("Calibri", 11);
            this.Controls.Add(ybut2);
            ybut2.Click += new EventHandler(ybut2_click);
        }
        private void ybut2_click(object sender,EventArgs e) //history delete
        {
            label3.Text = "";
            history.Clear();
            DeleteLabel();
        }
        private void ybuton_click(object sender, EventArgs e) //off button
        {
            this.Close();
            Application.Exit();
        }
        private void trigoClick(object sender, EventArgs e) //trigonometric operations
        {
            if (textBox1.Text.Length > 0){
                Button button = (Button)sender;
                switch (button.Text){
                    case "sin": num2 = Math.Sin(double.Parse(textBox1.Text) * (Math.PI / 180.0)); break;
                    case "cos": num2 = Math.Cos(double.Parse(textBox1.Text) * (Math.PI / 180.0)); break;
                    case "tan": num2 = Math.Tan(double.Parse(textBox1.Text) * (Math.PI / 180.0)); break;
                    case "cot": num2 = 1 / Math.Tan(double.Parse(textBox1.Text) * (Math.PI / 180.0)); break;
                    case "sinh": num2 = Math.Sinh(double.Parse(textBox1.Text));break;
                    case "cosh": num2 = Math.Cosh(double.Parse(textBox1.Text));break;
                    case "tanh": num2 = Math.Tanh(double.Parse(textBox1.Text));break;
                    case "coth": num2 = 1/Math.Tanh(double.Parse(textBox1.Text));break;
                    default: num2 = 0; break;
                }
                num2 = Math.Round(num2, 3);
                dataSaver($"{button.Text}({textBox1.Text}) = {num2}");
                textBox1.Text = num2.ToString();
                num1 = 0;
            }
        }
        private void logaritms(object sender, EventArgs e) //log and ln
        {
            if (textBox1.Text.Length > 0){
                Button button = (Button)sender;
                num2 = button.Text == "ln" ? Math.Log(double.Parse(textBox1.Text)) : Math.Log10(double.Parse(textBox1.Text));
                num2 = Math.Round(num2, 3);
                dataSaver($"{button.Text}({textBox1.Text}) = {num2}");
                textBox1.Text = num2.ToString();
                num1 = 0;
            }
        }
        private void button12_Click(object sender, EventArgs e) //PI number 
        {
            textBox1.Text = Math.PI.ToString();
            dataSaver($"{"π ="}{textBox1.Text}");      
        }
        private void button21_Click(object sender, EventArgs e) // 1/x
        {
            if (textBox1.Text.Length > 0){
                num2 = 1/double.Parse(textBox1.Text);
                dataSaver($"1/{textBox1.Text} = {num2}");
                textBox1.Text = num2.ToString();
                num1 = 0;
            }
        }
        private void button26_Click(object sender, EventArgs e) // factorial
        {
            for (int i = 1; i <= double.Parse(textBox1.Text); i++)
                factorial *= i;
            dataSaver($"{textBox1.Text}! = {factorial}");
            textBox1.Text = factorial.ToString();
            factorial = 1;
            num1 = 0;
        }
        private void button27_Click(object sender, EventArgs e) //square
        {
            if (textBox1.Text.Length > 0){
                num2 = double.Parse(textBox1.Text) * double.Parse(textBox1.Text);
                num2 = Math.Round(num2, 3);
                dataSaver($"{textBox1.Text}² = {num2}");
                textBox1.Text = num2.ToString();
                num1 = 0;
            }
        }
        private void button29_Click(object sender, EventArgs e) //cube
        {
            if (textBox1.Text.Length > 0){
                num2 = Math.Pow(double.Parse(textBox1.Text), 3);
                num2 = Math.Round(num2, 3);
                dataSaver($"{textBox1.Text}³ = {num2}");
                textBox1.Text = num2.ToString();
                num1 = 0;
            }
        }
        public class Yuvarlakbuton : Button   //creating circle button classes
        {
            protected override void OnResize(EventArgs e){
                base.OnResize(e);
                GraphicsPath graphicsPath = new GraphicsPath();
                graphicsPath.AddEllipse(new Rectangle(Point.Empty, this.Size));
                this.Region = new Region(graphicsPath);
            }
        }
    }
}
