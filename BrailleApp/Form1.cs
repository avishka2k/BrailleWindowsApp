using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection.Emit;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace BrailleApp
{
    public partial class Form1 : Form
    {
        string baseUrl = "http://localhost:8089/DotPattern/api";
        SpeechSynthesizer speech = new SpeechSynthesizer();
        public Form1()
        {
            InitializeComponent();

            SpeechAi();
            ShapesItem();
            comboBox2Item();
            comboBox2.SelectedItem = comboBox2.Items[1];
            timer1.Tick += Timer1_Tick;
            timer1.Enabled = false;
        }
        private void Timer1_Tick(object sender, EventArgs e)
        {
            label3.Visible = false;
            timer1.Enabled = false;
        }
        private void click_to_speech(string text)
        {
            speech.Speak(text);
        }
        string[] shapes = {"Square", "Rectangle", "Pyramid", "Right Triangle", "Left Triangle",
                   "Diamond", "Circle", "Left arrow", "Right arrow", "Hexagon", "Heart","Triangle",
                   "Crown", "Christmas tree", "Wave", "X mark", "Star"};
        private void ShapesItem()
        {
          
            Array.Sort(shapes);
            comboBox1.Items.AddRange(shapes);
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedItem = comboBox1.SelectedItem.ToString();
            Shapes sp = new Shapes();
            string text1 = numericUpDown1.Text;
            string text2 = numericUpDown2.Text;
            switch (selectedItem)
            {
                case "Square":
                    Square_Shape();
                    break;
                case "Rectangle":
                    Rectangle_Shape();
                    break;
                case "Pyramid":
                    Pyramid_Shape();
                    break;
                case "Triangle":
                    Triangle_Shape();
                    break;
                case "Circle":
                    Circle_Shape();
                    break;
                case "X mark":
                    X_mark_Shape();
                    break;
                case "Hexagon":
                    Hexagon_Shape();
                    break;
                case "Christmas tree":
                    Christmas_tree_Shape();
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
                case "Left arrow":
                    LeftArrow_Shape();
                    break;
                case "Right arrow":
                    RightArrow_Shape();
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
            VoiceAssistant_Status("Convert");
            errorLabel.ForeColor = Color.Red;
            if (comboBox1.SelectedItem == null)
            {
                errorLabel.Show();
                errorLabel.Text = "Select Item";
                return;
            }
            errorLabel.Hide();
            errorLabel.Text = "";
            richTextBox1.SelectionAlignment = HorizontalAlignment.Center;

            comboBox1_SelectedIndexChanged(sender,e);
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        public void Parms(string labelText1, string labelText2, bool second)
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
        //Rectangle
        private void Rectangle_Shape()
        {
            string textValue1 = numericUpDown1.Text;
            string textValue2 = numericUpDown2.Text;
            Parms("Width", "Height", true);
            string url = $"{baseUrl}/rectangle/{textValue1}/{textValue2}";
            GetApi(url);
        }
        //Square
        private void Square_Shape()
        {
            string textValue1 = numericUpDown1.Text;
            Parms("Side length", "", false);
            string url = $"{baseUrl}/rectangle/{textValue1}/{textValue1}";
            GetApi(url);
        }
        //Circle
        private void Circle_Shape()
        {
            Parms("Radius", "", false);
            string textValue1 = numericUpDown1.Text;
            string textValue2 = "3";

            string url = $"{baseUrl}/circle/{textValue1}/{textValue2}";
            GetApi(url);
        }

        //Triangle
        private void Triangle_Shape()
        {
            Parms("Height", "", false);
            string textValue1 = numericUpDown1.Text;
            string url = $"{baseUrl}/traingle/{textValue1}";
            GetApi(url);
        }
        //Pyramid
        private void Pyramid_Shape()
        {
            Parms("Height", "", false);
            string textValue1 = numericUpDown1.Text;
            string url = $"{baseUrl}/pyramid/{textValue1}";
            GetApi(url);
        }
        //X mark
        private void X_mark_Shape()
        {
            Parms("Size", "", false);
            string textValue1 = numericUpDown1.Text;
            string url = $"{baseUrl}/X_mark/{textValue1}";
            GetApi(url);
        }
        //Hexagon
        private void Hexagon_Shape()
        {
            Parms("Size", "", false);
            string textValue1 = numericUpDown1.Text;
            string url = $"{baseUrl}/hexagon/{textValue1}";
            GetApi(url);
        }
        //Diamond
        private void Diamond_Shape()
        {
            Parms("Size", "", false);
            string textValue1 = numericUpDown1.Text;
            string url = $"{baseUrl}/diamond/{textValue1}";
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
        //Heart
        private void Heart_Shape()
        {
            Parms("Size", "", false);
            string textValue1 = numericUpDown1.Text;
            string url = $"{baseUrl}/heart/{textValue1}";
            GetApi(url);
        }
        //Christmas tree
        private void Christmas_tree_Shape()
        {
            Parms("Heigth", "", false);
            string textValue1 = numericUpDown1.Text;
            string url = $"{baseUrl}/Christmas_tree/{textValue1}";
            GetApi(url);
        }

        //LeftArrow
        private void LeftArrow_Shape()
        {
            Parms("Size", "", false);
        }

        //RightArrow
        private void RightArrow_Shape()
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

        private void richTextBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            string text = richTextBox2.Text;
            string url = $"http://localhost:8089/Text/api/text";
            GetApiText(url, text);
        }

        private async void GetApiText(string url, string inputText)
        {
            FetchApi fetch = new FetchApi();
            string result = await fetch.GetApiText(url, inputText);

            if (result == "Error")
            {
                label4.Visible = true;
                label4.Text = "Error";
            }
            else if (result == "No_Special_characters")
            {
                label4.Visible = true;
                label4.Text = "Special characters are not allowed.";
            }
            else if(result == "Empty")
            {
                label4.Visible = true;
                label4.Text = "Input cannot be empty";
            }
            else
            {
                label4.Visible = false;
                richTextBox3.Text = result;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
                string textcopy = richTextBox3.Text;
                if (textcopy == null || textcopy == "")
                    return;

                Clipboard.SetText(textcopy);
                label3.Text = "Copied";
                label3.Visible = true;
                timer1.Enabled = true;       
        }

        private void richTextBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            richTextBox2.Text = "";
           // VoiceAssistant_Status("Clear");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //VoiceAssistant_Status("This is test");
        }

        private void VoiceAssistant_Status(string text) {
            string item = comboBox2.SelectedItem.ToString();

            if (item == "On")
            {
                click_to_speech(text);
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            


        }
        private void comboBox2Item()
        {
            comboBox2.Items.Add("On");
            comboBox2.Items.Add("Off");
        }

        private void SpeechAi()
        {
            SpeechRecognitionEngine recognitionEngine = new SpeechRecognitionEngine();
            Choices choices = new Choices();
            string[] lines = File.ReadAllLines(Environment.CurrentDirectory + "//grammar.txt");
            foreach (string line in lines)
            {
                string[] words = line.Split(' ');
                choices.Add(new GrammarBuilder(new Choices(words)));
            }
            Grammar grammar = new Grammar(new GrammarBuilder(choices));
            recognitionEngine.LoadGrammar(grammar);
            recognitionEngine.SetInputToDefaultAudioDevice();
            recognitionEngine.RecognizeAsync(RecognizeMode.Multiple);
            recognitionEngine.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(recognitionEngine_SpeechRecognized);
            speech.SelectVoiceByHints(VoiceGender.Male);
        }
        private void recognitionEngine_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            string result = e.Result.Text;
            if(result == "Convert")
            {
                button1.PerformClick();
                result= button1.Text;
            }
            foreach (string shape in shapes)
            {
                string modifiedShape = shape.Replace(" ", "?");
                if (result == modifiedShape)
                {
                    int index = Array.IndexOf(shapes, shape);
                    comboBox1.SelectedIndex = index;
                    result = shape;
                    break;
                }
            }
            speech.SpeakAsync(result);
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            string value = numericUpDown1.Value.ToString();
            VoiceAssistant_Status(value);
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            string value = numericUpDown2.Value.ToString();
            VoiceAssistant_Status(value);
        }
    }
}
