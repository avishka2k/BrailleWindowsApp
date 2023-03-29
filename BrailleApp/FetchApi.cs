using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace BrailleApp
{
    public class FetchApi
    {
        public async Task<string> GetApiText(string url, string inputText)
        {
            string[] skipped = new string[]
            {
            "!","@","#","$","%","^","&","*","(",")"
            };

            try
            {
                if (skipped.Any(c => inputText.Contains(c)))
                {
                    return "No_Special_characters";
                }
                if (string.IsNullOrWhiteSpace(inputText))
                {
                    return "Empty";
                }
                string newurl = $"{url}/{inputText}";
                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync(newurl);
                string textpattern = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    return textpattern;
                }
                else
                {
                    return "Error";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }
        /*
        public async Task<string> GetApiShapes(string url)
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
                    return "Error";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }
        */

    }
}
