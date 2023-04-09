using System;
using System.Windows.Forms;

namespace BrailleApp
{
    public partial class HistoryForm : Form
    {
        private History history;
        public HistoryForm(History his)
        {
            InitializeComponent();
            history = his;
        }

        private void HistoryForm_Load(object sender, EventArgs e)
        {
            label4.Text = history.Id.ToString();
            label5.Text = history.Type.ToString();
            label6.Text = history.CreatedDateTime.ToString();
            label7.Text = history.Braille.ToString();
        }
    }
}
