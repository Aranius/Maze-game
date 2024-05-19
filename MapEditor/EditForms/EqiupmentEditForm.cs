using MapModel.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MapEditor.EditForms
{
    public partial class EqiupmentEditForm : Form
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Equipment.EquipmentTypeEnum EquipmentType { get; set; }
        public List<Stat> Staty { get; set; }


        public EqiupmentEditForm()
        {
            InitializeComponent();
            Staty = new List<Stat>();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Staty.Add(new Stat((Stat.StatEnum)StatComboBox.SelectedIndex, (int)ValueUpDown.Value));
            listBox1.Items.Add($"{StatComboBox.SelectedItem} {ValueUpDown.Value}");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a stat to remove");
                return;
            }

            Staty.RemoveAt(listBox1.SelectedIndex);
            listBox1.Items.RemoveAt(listBox1.SelectedIndex);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Name = NameTextBox.Text;
            Description = DescriptionTextBox.Text;

            if (string.IsNullOrWhiteSpace(Name) || string.IsNullOrWhiteSpace(Description))
            {
                MessageBox.Show("Name and Description cannot be empty");
                return;
            }

            if (StatComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("Please select an equipement type");
                return;
            }

            EquipmentType = (Equipment.EquipmentTypeEnum)EquipmentComboBox.SelectedIndex;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
