using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BrailleApp
{
    public partial class Toggle : UserControl
    {
        public Toggle()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.Selectable, true);

        }
        enum eApparence { on, off, onSelect, offSelect }

        eApparence apparence = eApparence.off;
        eApparence Apparence
        {
            get { return apparence; }
            set
            {
                apparence = value;
                onChangeApparece();
            }
        }

        bool check;
        [Category("Behavior")]
        public bool Check
        {
            get { return check; }
            set
            {
                check = value;
                setApparence();
                onCheckChange();
            }
        }

        public void ChangeValue()
        {
            Check = !check;
        }
        void onChangeApparece()
        {
            switch (apparence)
            {
                case eApparence.on:
                    this.BackgroundImage = Properties.Resources.toggle_on;
                    this.BorderStyle = BorderStyle.None;
                    break;
                case eApparence.off:
                    this.BackgroundImage = Properties.Resources.toggle_off;
                    this.BorderStyle = BorderStyle.None;
                    break;
                case eApparence.onSelect:
                    this.BackgroundImage = Properties.Resources.toggle_on;
                    this.BorderStyle = BorderStyle.None;
                    break;
                case eApparence.offSelect:
                    this.BackgroundImage = Properties.Resources.toggle_off;
                    this.BorderStyle = BorderStyle.None;
                    break;
                default:
                    break;
            }
        }
        void setApparence()
        {
            if (this.Focused)
            {
                if (check)
                    Apparence = eApparence.onSelect;
                else
                    Apparence = eApparence.offSelect;
            }
            else
            {
                if (check)
                    Apparence = eApparence.on;
                else
                    Apparence = eApparence.off;
            }
        }




        public event EventHandler CheckChange;
        protected void onCheckChange()
        {
            CheckChange?.Invoke(this, new EventArgs());
        }



        protected override void OnGotFocus(EventArgs e)
        {
            Invalidate();
            base.OnGotFocus(e);
            setApparence();
        }
        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);
            Invalidate();
            setApparence();
        }

        private void Toggle_Click(object sender, EventArgs e)
        {
            ChangeValue();
        }

        private void Toggle_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Space)
                ChangeValue();
        }

        private void Toggle_MouseEnter(object sender, EventArgs e)
        {
            this.BorderStyle = BorderStyle.FixedSingle;
        }

        private void Toggle_MouseLeave(object sender, EventArgs e)
        {
            this.BorderStyle = BorderStyle.None;
        }

        private void Toggle_Load(object sender, EventArgs e)
        {

        }

    }
}
