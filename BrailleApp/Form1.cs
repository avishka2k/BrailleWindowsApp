using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace BrailleApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //ComboBox comboBox = new ComboBox();
            ShapesItem();       
        }

        private void ShapesItem()
        {
            string[] shapes = {"Square", "Rectangle", "Pyramid", "Right Triangle", "Left Triangle",
                   "Diamond", "Circle", "Left arrow", "Right arrow", "Hexagon", "Heart",
                   "Crown", "Christmas tree", "Wave", "X mark", "Star"};
            Array.Sort(shapes);
            comboBox1.Items.AddRange(shapes);
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShapesItem();
            string selectedItem = comboBox1.SelectedItem.ToString();

            switch (selectedItem)
            {
                case "Square":
                    string textValue = numericUpDown1.Text;
                    Rectangle_Shape(textValue, textValue, false);
                    break;
                case "Rectangle":
                    string textValue1 = numericUpDown1.Text;
                    string textValue2 = numericUpDown2.Text;
                    Rectangle_Shape(textValue1, textValue2, true);
                    break;
                case "Pyramid":
                    Pyramid_Shape();
                    break;
                case "Circle":
                    Circle_Shape();
                    break;
                case "Right Triangle":
                    RightTriangle_Shape();
                    break;
                case "Left Triangle":
                    LeftTriangle_Shape();
                    break;
                case "Diamond":
                    Diamond_Shape();
                    break;
                case "Heart":
                    Heart_Shape();
                    break;
                default:           
                    break;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            errorLabel.ForeColor = Color.Red;
            if (comboBox1.SelectedItem == null)
            {
                errorLabel.Show();
                errorLabel.Text = "Select Item";
                return;
            }
            errorLabel.Hide();
            errorLabel.Text = "";
            comboBox1_SelectedIndexChanged(sender,e);
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        //Rectangle, Square
        private void Rectangle_Shape(string textValue1, string textValue2, bool second)
        {
            label1.Show();
            numericUpDown1.Show();
            label1.Text = "Side length";
            label2.Hide();
            numericUpDown2.Hide();

            Second_Value(second);
            string url = $"http://localhost:8089/DotPattern/api/rectangle/{textValue1}/{textValue2}";
            GetApi(url);
        }

        private void Second_Value(bool second)
        {
            if (second)
            {
                label2.Show();
                numericUpDown2.Show();
                label1.Text = "Width";
                label2.Text = "Height";
            }
        }

        private void Parms(string labelText1, string labelText2, bool second)
        {
            label1.Show();
            numericUpDown1.Show();
            label1.Text = labelText1;
            label2.Hide();
            numericUpDown2.Hide();

            if (second)
            {
                label2.Show();
                numericUpDown2.Show();
                label2.Text = labelText2;
            }
        }
        //Circle
        private void Circle_Shape()
        {
            Parms("Radius", "", false);
            string textValue1 = numericUpDown1.Text;
            string textValue2 = "3";

            string url = $"http://localhost:8089/DotPattern/api/circle/{textValue1}/{textValue2}";
            GetApi(url);
        }

        //Pyramid
        private void Pyramid_Shape()
        {
            Parms("Width", "Height", true);
            string textValue1 = numericUpDown1.Text;
            string textValue2 = numericUpDown2.Text;
            string url = $"";
            GetApi(url);
        }

        //RightTriangle
        private void RightTriangle_Shape()
        {
            Parms("Size", "", false);
            string textValue1 = numericUpDown1.Text;
            string url = $"";
            GetApi(url);

        }

        //LeftTriangle
        private void LeftTriangle_Shape()
        {
            Parms("Size", "", false);
            string textValue1 = numericUpDown1.Text;
            string url = $"";
            GetApi(url);
        }

        //Diamond
        private void Diamond_Shape()
        {
            Parms("Width", "Height", true);
        }

        //Heart
        private void Heart_Shape()
        {
            Parms("Size", "", false);

        }

        //Get Api data
        private async void GetApi(string url)
        {
            try
            {
                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync(url);
                string dotPattern = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    richTextBox1.Text = dotPattern;
                }
                else
                {
                    richTextBox1.Text = "Server Error";
                }
            }catch(Exception ex)
            {
                richTextBox1.Text = ex.Message;
            }
            
        }

        
    }
}
