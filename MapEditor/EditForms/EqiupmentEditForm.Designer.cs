namespace MapEditor.EditForms
{
    partial class EqiupmentEditForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DescriptionTextBox = new TextBox();
            NameTextBox = new TextBox();
            label2 = new Label();
            label1 = new Label();
            label3 = new Label();
            label4 = new Label();
            EquipmentComboBox = new ComboBox();
            listBox1 = new ListBox();
            label5 = new Label();
            StatComboBox = new ComboBox();
            label6 = new Label();
            ValueUpDown = new NumericUpDown();
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            button4 = new Button();
            ((System.ComponentModel.ISupportInitialize)ValueUpDown).BeginInit();
            SuspendLayout();
            // 
            // textBox2
            // 
            DescriptionTextBox.Location = new Point(82, 39);
            DescriptionTextBox.Name = "textBox2";
            DescriptionTextBox.Size = new Size(304, 23);
            DescriptionTextBox.TabIndex = 7;
            // 
            // textBox1
            // 
            NameTextBox.Location = new Point(82, 12);
            NameTextBox.Name = "textBox1";
            NameTextBox.Size = new Size(304, 23);
            NameTextBox.TabIndex = 6;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(6, 42);
            label2.Name = "label2";
            label2.Size = new Size(70, 15);
            label2.TabIndex = 5;
            label2.Text = "Description:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(6, 12);
            label1.Name = "label1";
            label1.Size = new Size(42, 15);
            label1.TabIndex = 4;
            label1.Text = "Name:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(6, 65);
            label3.Name = "label3";
            label3.Size = new Size(127, 15);
            label3.TabIndex = 8;
            label3.Text = "Item type:   Equipment";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(6, 94);
            label4.Name = "label4";
            label4.Size = new Size(95, 15);
            label4.TabIndex = 9;
            label4.Text = "Equipment Type:";
            // 
            // comboBox1
            // 
            EquipmentComboBox.FormattingEnabled = true;
            EquipmentComboBox.Items.AddRange(new object[] { "Weapon", "Shield", "Armor ", "Helmet", "Boots", "Gloves", "Ring", "Amulet" });
            EquipmentComboBox.Location = new Point(107, 91);
            EquipmentComboBox.Name = "comboBox1";
            EquipmentComboBox.Size = new Size(279, 23);
            EquipmentComboBox.TabIndex = 10;
            // 
            // listBox1
            // 
            listBox1.FormattingEnabled = true;
            listBox1.ItemHeight = 15;
            listBox1.Location = new Point(266, 136);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(120, 94);
            listBox1.TabIndex = 11;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(11, 140);
            label5.Name = "label5";
            label5.Size = new Size(54, 15);
            label5.TabIndex = 12;
            label5.Text = "StatType:";
            // 
            // comboBox2
            // 
            StatComboBox.FormattingEnabled = true;
            StatComboBox.Items.AddRange(new object[] { "Attack", "Defense", "Speed", "Helath" });
            StatComboBox.Location = new Point(71, 137);
            StatComboBox.Name = "comboBox2";
            StatComboBox.Size = new Size(189, 23);
            StatComboBox.TabIndex = 13;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(12, 176);
            label6.Name = "label6";
            label6.Size = new Size(38, 15);
            label6.TabIndex = 14;
            label6.Text = "Value:";
            // 
            // numericUpDown1
            // 
            ValueUpDown.Location = new Point(71, 168);
            ValueUpDown.Name = "numericUpDown1";
            ValueUpDown.Size = new Size(120, 23);
            ValueUpDown.TabIndex = 15;
            // 
            // button1
            // 
            button1.Location = new Point(12, 207);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 16;
            button1.Text = "Add";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(93, 207);
            button2.Name = "button2";
            button2.Size = new Size(75, 23);
            button2.TabIndex = 17;
            button2.Text = "Remove";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.Location = new Point(107, 276);
            button3.Name = "button3";
            button3.Size = new Size(75, 23);
            button3.TabIndex = 19;
            button3.Text = "Cancel";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // button4
            // 
            button4.Location = new Point(15, 276);
            button4.Name = "button4";
            button4.Size = new Size(75, 23);
            button4.TabIndex = 18;
            button4.Text = "Save";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // EqiupmentEditForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(403, 320);
            Controls.Add(button3);
            Controls.Add(button4);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(ValueUpDown);
            Controls.Add(label6);
            Controls.Add(StatComboBox);
            Controls.Add(label5);
            Controls.Add(listBox1);
            Controls.Add(EquipmentComboBox);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(DescriptionTextBox);
            Controls.Add(NameTextBox);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "EqiupmentEditForm";
            Text = "EqiupmentEditForm";
            ((System.ComponentModel.ISupportInitialize)ValueUpDown).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox DescriptionTextBox;
        private TextBox NameTextBox;
        private Label label2;
        private Label label1;
        private Label label3;
        private Label label4;
        private ComboBox EquipmentComboBox;
        private ListBox listBox1;
        private Label label5;
        private ComboBox StatComboBox;
        private Label label6;
        private NumericUpDown ValueUpDown;
        private Button button1;
        private Button button2;
        private Button button3;
        private Button button4;
    }
}