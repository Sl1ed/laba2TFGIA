using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            tbFSource.AppendText("000010011" + "\r\n");
            tbFSource.AppendText("abcdcdaa");
            int n = tbFSource.Lines.Length;
        }

        private void btnFStart_Click(object sender, EventArgs e)
        {
            CLex Lex = new CLex();
            Lex.strPSource = tbFSource.Lines;
            Lex.strPMessage = tbFMessage.Lines;
            Lex.intPSourceColSelection = -1;
            Lex.intPSourceRowSelection = 0;
            int x = tbFSource.TextLength;
            int y = tbFSource.Lines.Length;
            tbFMessage.Text = "";

            try
            {
                Lex.GetSymbol(); // Выводятся литеры и классификация
                while (Lex.enumPState != TState.Finish)
                {
                    Lex.NextToken();
                    String s = "";
                    String s1 = "";
                    switch (Lex.enumPToken)
                    {
                        case TToken.lxmNumber: { s = "LxmNumber"; s1 = Lex.strPLexicalUnit; break; }
                        case TToken.lxmIdentifier: { s = "lxmId"; s1 = Lex.strPLexicalUnit; break; }
                    }
                    String m = "(" + s + "," + s1 + ")"; //литера и ее тип
                    tbFMessage.Text += m; //добавляется в строку сообщение
                }
            }
            catch (Exception exc)
            {
                tbFMessage.Text += exc.Message;
                tbFSource.Select();
                tbFSource.SelectionStart = 0;
                int n = 0;
                for (int i = 0; i < Lex.intPSourceRowSelection; i++) n += tbFSource.Lines[i].Length + 2;
                n += Lex.intPSourceColSelection;
                tbFSource.SelectionLength = n;


            }
        }

     

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

