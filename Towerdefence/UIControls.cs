using Microsoft.Xna.Framework;
using MonoGame.UI.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Input;



namespace Towerdefence
{
    public enum BUTTON_CLICK { NONE, PLAY, QUIT};
    internal class UIControls : ControlManager
    {
        string m_whitemonsterskilled = "";
        string m_darkmonsterskilled ="";
        string m_currentincome = "";
        string m_surviveddays = "";
        string m_moneySpent = "";
        Button m_playBtn;
        Button m_quitBtn;
        TextArea m_textArea1;
        Form m_form1;
        Form m_form2;
        List<Control> m_controls;
        List<Control> m_controls2;
        public bool m_isInTxt;
        BUTTON_CLICK m_buttonclick = BUTTON_CLICK.NONE;
        public string WhiteMonstersKilled
        {
            set { m_whitemonsterskilled = value; }
        }
        public string DarkMonstersKilled
        {
            set { m_darkmonsterskilled = value; }
        }
        public string CurrentIncome
        {
            set { m_currentincome = value; }
        }
        public string survivedDays
        {
            set { m_surviveddays = value; }
        }
        public string moneyspent
        {
            set { m_moneySpent = value; }
        }
        public UIControls(Game game) : base(game)
        {
        }
        public void AddMenu() { Controls.Add(m_form1); }
        public void AddPauseMenu() { Controls.Add(m_form2); }
        public void RemoveMenu() { Controls.Remove(m_form1); }
        public void RemovePauseMenu() { Controls.Remove(m_form2); }
        public override void InitializeComponent()
        {
            m_controls = new List<Control>();
            m_form1 = GetForm("", new Vector2(300, 100));

            m_controls2 = new List<Control>();

            m_textArea1 = GetTextArea(new Vector2(125, 80));    
          

            m_playBtn = GetButton("PLAY", new Vector2(125, 0));
            m_controls.Add(m_playBtn);
          
            m_form1.Controls.AddRange(m_controls.ToArray<Control>());

            m_form2 = GetForm("", new Vector2(600, 600));
            Label label = new Label() { Location = new Vector2(0, 0), Text = "White monsters slayn: "+m_whitemonsterskilled, TextColor = Color.Black};
       
            m_quitBtn = GetButton("QUIT", new Vector2(0, 300));
            m_controls2.Add(m_quitBtn);
            m_controls2.Add(label);
            label = new Label() { Location = new Vector2(0, 50), Text = "Dark monsters slayn: " + m_darkmonsterskilled, TextColor = Color.Black };
            m_controls2.Add(label);
            label = new Label() { Location = new Vector2(0, 100), Text = "Money Spent: " + m_moneySpent, TextColor = Color.Black };
            m_controls2.Add(label);
            label = new Label() { Location = new Vector2(0, 150), Text = "Days survived: " + m_surviveddays, TextColor = Color.Black };
            m_controls2.Add(label);
            label = new Label() { Location = new Vector2(0, 200), Text = "Current Income: " + m_currentincome, TextColor = Color.Black };
            m_controls2.Add(label);
            m_form2.Controls.AddRange(m_controls2.ToArray<Control>());
        }
        public BUTTON_CLICK GetButton() { return m_buttonclick; }
        public void SetButton(BUTTON_CLICK click) { m_buttonclick = click; }
        //Skapar en knapp med lyssnare
        private Button GetButton(string buttonText,Vector2 location)
        {
            Button button = new Button()
            {
                Text = buttonText,
                Size = new Vector2(95, 40),
                BackgroundColor = Color.Green,
                Location = location
            };
            button.Clicked += Button_Clicked;
            return button;
        }

        //Metod som går igång när knappen klickas på
        private void Button_Clicked(object sender, EventArgs e)
        {
            Button b=sender as Button;
            if(b.Text=="PLAY")
            {
                m_buttonclick = BUTTON_CLICK.PLAY;
            }
            else if(b.Text =="QUIT")
            {
                m_buttonclick = BUTTON_CLICK.QUIT;
            }
        }

        //skapar ett textfält med lyssnare för när musen klickas på i fältet
        private TextArea GetTextArea(Vector2 location) {
            string defaultText = @"TextArea ";
            TextArea txtArea = new TextArea()
            {
                Location = location,
                Text = defaultText,
                BackgroundColor = Color.White,
                Size = new Vector2(30, 100),
                Enabled = true
            };
            
            txtArea.MouseDown += TextArea_Clicked;
            return txtArea;
        }

        private void TextArea_Clicked(object sender, EventArgs e)
        {
            //Inmatningen av text sker i Game1. isInText signalerar att användaren har klickat på textfältet
            m_isInTxt = true;
            //Tom sträng för att sätta storleken på textfältet (Size verkar inte funka för ändamålet)
            m_textArea1.Text = "                   ";
        }

        //Sätter ny test från Game1
        public void SetText(string s) {
            m_textArea1.Text = s;
        }
        public string GetTextInput() { return m_textArea1.Text; }
        private Form GetForm(string name, Vector2 size)
        {
            Form form = new Form()
            {
                Title = name,
                IsMovable = false,
                Location = new Vector2(Game1.resolutionX/2, Game1.resolutionY/2),
                Size = size
            };
            return form;
        }

    }
}
