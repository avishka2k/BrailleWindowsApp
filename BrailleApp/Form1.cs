using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection.Emit;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Google.Cloud.Speech.V1;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using static Google.Api.ResourceDescriptor.Types;

namespace BrailleApp
{
    public partial class Form1 : Form
    {
        string baseUrl = "http://localhost:8089/DotPattern/api";
        SpeechSynthesizer speech = new SpeechSynthesizer();
        private List<History> history;
        private int nextId;
        private string textapiresult;
        private SettingsCheckBox settingsCheck = new SettingsCheckBox();
        private GenderSettings genderSettings = new GenderSettings();
        public Form1()
        {
            InitializeComponent();        
            comboBoxItems();
            timer1.Tick += Timer1_Tick;
            timer1.Enabled = false;
            LoadData();
            LoadToggle1();
            LoadGender();
            nextId = history.Count > 0 ? history.Max(p => p.Id) + 1 : 1;
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
        string[] genders = { "Male", "FeMale" };
        private void comboBoxItems()
        {     
            Array.Sort(shapes);
            comboBox1.Items.AddRange(shapes);
            comboBox3.Items.AddRange(genders);
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedItem = comboBox1.SelectedItem.ToString();
            if(selectedItem == "Circle")
            {
                pictureBox1.Visible = true;
            }
            else
            {
                pictureBox1.Visible = false;
            }
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
            ConvertBtn(sender, e);
            SaveHistory();


        }

        private void ConvertBtn(object sender, EventArgs e)
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

            comboBox1_SelectedIndexChanged(sender, e);
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
        private async void Circle_Shape()
        {
            Parms("Radius", "", false);
            string textValue1 = numericUpDown1.Text;
            int x = 100;
            int y = 100;
            string url = $"{baseUrl}/circle/{textValue1}/{x}/{y}";
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(url);
                byte[] imageData = await response.Content.ReadAsByteArrayAsync();
                MemoryStream ms = new MemoryStream(imageData);
                Image image = Image.FromStream(ms);
                pictureBox1.Image = image;
            }
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
        public string braillePattern = "";
        //Get Api data
        private async void GetApi(string url)
        {
            try
            {
                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync(url);
                string dotPattern = await response.Content.ReadAsStringAsync();
                patternView.Show();
                if (response.IsSuccessStatusCode)
                {
                    patternView.Text = dotPattern;
                    braillePattern = dotPattern;
                }
                else
                {
                    patternView.Text = "Server Error";
                }
            }catch(Exception ex)
            {
                patternView.Text = ex.Message;
            }
            
        }


        private void button4_Click(object sender, EventArgs e)
        {
            string text = richTextBox2.Text;
            string url = $"http://localhost:8089/Text/api/text";
            GetApiText(url, text);
            SaveHistory();
        }
        
        private async void GetApiText(string url, string inputText)
        {
            FetchApi fetch = new FetchApi();
            string result = await fetch.GetApiText(url, inputText);

            switch (result)
            {
                case "Error":
                    label4.Visible = true;
                    label4.Text = "Error";
                    break;

                case "No_Special_characters":
                    label4.Visible = true;
                    label4.Text = "Special characters are not allowed.";
                    break;

                case "Empty":
                    label4.Visible = true;
                    label4.Text = "Input cannot be empty";
                    break;

                default:
                    label4.Visible = false;
                    richTextBox3.Text = result;
                    textapiresult = result;
                    break;
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
            PrintDocument pd = new PrintDocument();
            if (comboBox1.SelectedItem != null)
            {
                string selectedItem = comboBox1.SelectedItem.ToString();

                if (selectedItem == "Circle")
                {
                    pd.PrintPage += new PrintPageEventHandler(PrintImage);
                    PrintDialog printDialog = new PrintDialog();
                    printDialog.Document = pd;
                    if (printDialog.ShowDialog() == DialogResult.OK)
                    {
                        pd.Print();
                    }
                }
                else if (patternView.Text != null)
                {
                    pd.PrintPage += new PrintPageEventHandler(this.printDocument1_PrintPage);
                    PrintDialog printDialog = new PrintDialog();
                    printDialog.Document = pd;

                    if (printDialog.ShowDialog() == DialogResult.OK)
                    {
                        pd.Print();
                    }
                }
            }
        }

        private void VoiceAssistant_Status(string text) {
            bool item = toggle1.Check;

            if (item)
            {
                click_to_speech(text);
            }
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
        }
        private void recognitionEngine_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            string result = e.Result.Text;

            Dictionary<string, Action> commandActions = new Dictionary<string, Action>
            {
                { "Shaps", () => this.tabControl1.SelectedIndex = 0 },
                { "Text", () => this.tabControl1.SelectedIndex = 1 },
                { "Settings", () => this.tabControl1.SelectedIndex = 2 },
                { "Convert", button1.PerformClick },
                { "Print", button5.PerformClick }
            };

            if (commandActions.TryGetValue(result, out Action action))
            {
                action();
                speech.SpeakAsync(result);
                return;
            }

            if (this.tabControl1.SelectedIndex == 0)
            {
                if (TrySetComboBoxIndex(result))
                {
                    speech.SpeakAsync(result);
                    return;
                }
            }
        }

        private bool TrySetComboBoxIndex(string result)
        {
            string[] modifiedShapes = shapes.Select(s => s.Replace(" ", "")).ToArray();
            int index = Array.IndexOf(modifiedShapes, result);
            if (index != -1)
            {
                comboBox1.SelectedIndex = index;
                return true;
            }
            return false;
        }

        /*
        private void recognitionEngine_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            string result = e.Result.Text;
            
            switch (result)
            {
                case "Shaptobraille":
                    this.tabControl1.SelectedIndex = 0;
                    speech.SpeakAsync(result);
                    break;
                case "Texttobraille":
                    this.tabControl1.SelectedIndex = 1;
                    speech.SpeakAsync(result);
                    break;
                case "Settings":
                    this.tabControl1.SelectedIndex = 2;
                    speech.SpeakAsync(result);
                    break;
            }
            if (this.tabControl1.SelectedIndex == 0)
            {
                if(result == "Convert")
                {
                    button1.PerformClick();
                    speech.SpeakAsync(result);
                }
                foreach (string shape in shapes)
                {
                    string modifiedShape = shape.Replace(" ", "");
                    if (result == modifiedShape)
                    {
                        int index = Array.IndexOf(shapes, shape);
                        comboBox1.SelectedIndex = index;
                        result = shape;
                        speech.SpeakAsync(result);
                        return;
                    }
                }                
            }            
        }
        */
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

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;
            g.DrawString(patternView.Text, new Font("Arial", 20), Brushes.Black, new PointF(100, 100));
        }
        private void PrintImage(object sender, PrintPageEventArgs e)
        {
            Image image = pictureBox1.Image;
            float aspectRatio = (float)image.Width / (float)image.Height;
            float newWidth = e.MarginBounds.Width;
            float newHeight = newWidth / aspectRatio;
            if (newHeight > e.MarginBounds.Height)
            {
                newHeight = e.MarginBounds.Height;
                newWidth = newHeight * aspectRatio;
            }
            RectangleF destRect = new RectangleF(e.MarginBounds.Left, e.MarginBounds.Top, newWidth, newHeight);
            e.Graphics.DrawImage(image, destRect);
        }

        
     

        private void ConvertRichTextBoxToJpg(RichTextBox richTextBox)
        {
            Bitmap bitmap = new Bitmap(richTextBox.Width, richTextBox.Height);

            Graphics graphics = Graphics.FromImage(bitmap);

            graphics.DrawString(richTextBox.Text, richTextBox.Font, Brushes.White, new PointF(0, 0));

            string fileName = Path.GetRandomFileName();
            fileName = Path.ChangeExtension(fileName, ".jpg");

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = fileName;
            saveFileDialog.Filter = "JPG files (*.jpg)|*.jpg";
            saveFileDialog.Title = "Save file as...";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {             
                bitmap.Save(saveFileDialog.FileName, ImageFormat.Jpeg);
            }
        }

        private void toggleButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        private void toggle1_CheckChange(object sender, EventArgs e)
        {
            bool toggle = toggle1.Check;
            if (toggle)
            {
                SpeechAi();
            }
            settingsCheck.IsChecked = toggle1.Check;
            string json = JsonConvert.SerializeObject(settingsCheck);
            File.WriteAllText("jsondata/checkboxData.json", json);
        }

        private void LoadToggle1()
        {
            try
            {
                string json = File.ReadAllText("jsondata/checkboxData.json");
                settingsCheck = JsonConvert.DeserializeObject<SettingsCheckBox>(json);
                toggle1.Check = settingsCheck.IsChecked;
            }
            catch (FileNotFoundException)
            {
                toggle1.Check = false;
            }
        }
        private void LoadGender()
        {
            try
            {

                string json = File.ReadAllText("jsondata/gender.json");
                genderSettings = JsonConvert.DeserializeObject<GenderSettings>(json);
                comboBox3.SelectedIndex = genderSettings.Gender;
            }
            catch (FileNotFoundException)
            {
                comboBox3.SelectedIndex = 0;
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

            genderSettings.Gender = comboBox3.SelectedIndex;
            string json = JsonConvert.SerializeObject(genderSettings);
            File.WriteAllText("jsondata/gender.json", json);
            string comboBox3selectedItem = comboBox3.SelectedItem.ToString();

            if (comboBox3selectedItem == "Male")
                speech.SelectVoiceByHints(VoiceGender.Male);
            else
                speech.SelectVoiceByHints(VoiceGender.Female);

            

        }


        private void LoadData()
        {
            try
            {
                string json = File.ReadAllText("jsondata/history_data.json");
                JObject jObject = JObject.Parse(json);
                JArray jArray = (JArray)jObject["history"];
                history = jArray.ToObject<List<History>>();
            }
            catch (FileNotFoundException)
            {
                history = new List<History>();
            }

            dataGridView1.DataSource = history;
        }

        private void SaveData()
        {
            JObject jObject = new JObject();
            JArray jArray = new JArray();
            jObject.Add("history", jArray);
            foreach (History employee in history)
            {
                JObject jEmployee = new JObject
                {
                    { "id", employee.Id },
                    { "type", employee.Type },
                    { "parameter1", employee.Parameter1 },
                    { "parameter2", employee.Parameter2 },
                    { "braille", employee.Braille },
                    { "createdDateTime", employee.CreatedDateTime }
                };
                jArray.Add(jEmployee);
            }

            string json = jObject.ToString();
            File.WriteAllText("jsondata/history_data.json", json);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            history.Clear();
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = history;
            SaveData();
        }


        private void SaveHistory()
        {
            int parameter1 = (int)numericUpDown1.Value;
            string parameter2 = numericUpDown2.Value.ToString();
            if (numericUpDown1.Value == 0)
                parameter2 = "N/A";
            if (numericUpDown2.Value == 0)
                parameter2 = "N/A";

            int currentTab = this.tabControl1.SelectedIndex;
            string type = "";
            string braille = "";
            if (currentTab == 0)
            {
                type = "Shapes";
                braille = patternView.Text;
            }
            else if (currentTab == 1) 
            {
                type = "Text";
                braille = textapiresult;
            }

            History employee = new History { Id = nextId++, Type = type, Braille = braille, Parameter1 = parameter1, Parameter2 = parameter2, CreatedDateTime = DateTime.Now };
            history.Add(employee);

            dataGridView1.DataSource = null;
            dataGridView1.DataSource = history;

            SaveData();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;

            // Check if a row is selected
            if (rowIndex >= 0)
            {
                History emp = dataGridView1.Rows[e.RowIndex].DataBoundItem as History;

                // Get the data from the selected row
                HistoryForm form3 = new HistoryForm(emp);
                form3.ShowDialog();
            }
        }
    }
}
