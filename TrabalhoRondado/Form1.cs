using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TrabalhoRondado
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void addClienteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            addCrud add = new addCrud();
            add.ShowDialog(); //opens the window for adding new customer
        }

        private void sAIRToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void oPÇÕESToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
