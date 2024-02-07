using Maze.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MapEditor
{
    public partial class FinishEditForm : Form
    {
        public List<Item> Treasures { get; set; }
        public List<string> SelectedTreasures { get; set; }

        public FinishEditForm()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void FinishEditForm_Shown(object sender, EventArgs e)
        {
            foreach (var item in Treasures)
            {
                checkedListBox1.Items.Add(item.Name);
            }            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SelectedTreasures = checkedListBox1.CheckedItems.OfType<string>().ToList();

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
