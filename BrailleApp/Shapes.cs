using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BrailleApp
{
    public class Shapes
    {
        string baseUrl = "http://localhost:8089/DotPattern/api";
        Form1 fm = new Form1();
        private async Task<string> GetApi(string url)
        {
            try
            {
                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync(url);
                string dotPattern = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    return dotPattern;
                }
                else
                {
                    return "Server Error";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        //Rectangle
        public async void Rectangle_Shape(string text1, string text2,string txt)
        {
            fm.Parms("Width", "Height", true);
            string url = $"{baseUrl}/rectangle/{text1}/{text2}";
            txt = await GetApi(url);
        }
        //Square
        public async void Square_Shape(string text1,  string richTextBox1)
        {
            fm.Parms("Side length", "", false);
            string url = $"{baseUrl}/rectangle/{text1}/{text1}";
            string result = await GetApi(url);
            result = richTextBox1;
        }
        //Circle
        public async void Circle_Shape(string text1, string richTextBox1)
        {
            fm.Parms("Radius", "", false);
            string url = $"{baseUrl}/circle/{text1}/{text1}";
            string result = await GetApi(url);
            result = richTextBox1;
        }

        //Triangle
        public async void Triangle_Shape(string text1, string richTextBox1)
        {
            fm.Parms("Height", "", false);
            string url = $"{baseUrl}/traingle/{text1}";
            string result = await GetApi(url);
            result = richTextBox1;
        }
        //Pyramid
        public async void Pyramid_Shape(string text1, string richTextBox1)
        {
            fm.Parms("Height", "", false);
            string url = $"{baseUrl}/pyramid/{text1}";
            string result = await GetApi(url);
            result = richTextBox1;
        }
        //X mark
        public async void X_mark_Shape(string text1, string richTextBox1)
        {
            fm.Parms("Size", "", false);
            string url = $"{baseUrl}/X_mark/{text1}";
            string result = await GetApi(url);
            result = richTextBox1;
        }
        //Hexagon
        public async void Hexagon_Shape(string text1, string richTextBox1)
        {
            fm.Parms("Size", "", false);
            string url = $"{baseUrl}/hexagon/{text1}";
            string result = await GetApi(url);
            result = richTextBox1;
        }
        //Diamond
        public async void Diamond_Shape(string text1, string richTextBox1)
        {
            fm.Parms("Size", "", false);
            string url = $"{baseUrl}/diamond/{text1}";
            string result = await GetApi(url);
            result = richTextBox1;
        }

        //RightTriangle
        public async void RightTriangle_Shape(string text1, string richTextBox1)
        {
            fm.Parms("Size", "", false);
            string url = $"";
            string result = await GetApi(url);
            result = richTextBox1;
        }

        //LeftTriangle
        public async void LeftTriangle_Shape(string text1, string richTextBox1)
        {
            fm.Parms("Size", "", false);
            string url = $"";
            string result = await GetApi(url);
            result = richTextBox1;
        }
        //Heart
        public async void Heart_Shape(string text1, string richTextBox1)
        {
            fm.Parms("Size", "", false);
            string url = $"{baseUrl}/heart/{text1}";
            string result = await GetApi(url);
            result = richTextBox1;
        }
        //Christmas tree
        public async void Christmas_tree_Shape(string text1, string richTextBox1)
        {
            fm.Parms("Heigth", "", false);
            string url = $"{baseUrl}/Christmas_tree/{text1}";
            string result = await GetApi(url);
            result = richTextBox1;
        }

        //LeftArrow
        public void LeftArrow_Shape()
        {
            fm.Parms("Size", "", false);
        }

        //RightArrow
        public void RightArrow_Shape()
        {
            fm.Parms("Size", "", false);
        }
    }
}
