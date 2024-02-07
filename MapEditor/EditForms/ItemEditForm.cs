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
    public partial class ItemEditForm : Form
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ItemTypeEnum ItemType { get; set; }

        public ItemEditForm()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Name = textBox1.Text;
            Description = textBox2.Text;

            if (string.IsNullOrWhiteSpace(Name) || string.IsNullOrWhiteSpace(Description))
            {
                MessageBox.Show("Name and Description cannot be empty");
                return;
            }

            if (comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("Please select an item type");
                return;
            }

            ItemType = (ItemTypeEnum)comboBox1.SelectedIndex;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
