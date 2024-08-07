﻿namespace MapEditor
{
    partial class Editor
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            menuStrip1 = new MenuStrip();
            mapaToolStripMenuItem = new ToolStripMenuItem();
            otvorMapuToolStripMenuItem = new ToolStripMenuItem();
            otvorOverlayToolStripMenuItem = new ToolStripMenuItem();
            ulozMapuToolStripMenuItem = new ToolStripMenuItem();
            ulozOverlayToolStripMenuItem = new ToolStripMenuItem();
            OpenMapDialog = new OpenFileDialog();
            SaveMapDialog = new SaveFileDialog();
            OpenOverlayDialog = new OpenFileDialog();
            saveOverlayDialog = new SaveFileDialog();
            descriptionBox = new TextBox();
            CreateObjectContextMenu = new ContextMenuStrip(components);
            deleteToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            toolStripCreateDoor = new ToolStripMenuItem();
            toolStripCreateTrap = new ToolStripMenuItem();
            teleportingToolStripMenuItem = new ToolStripMenuItem();
            toolStripCreateItem = new ToolStripMenuItem();
            createItToolStripMenuItem = new ToolStripMenuItem();
            equipmentToolStripMenuItem = new ToolStripMenuItem();
            createStartToolStripMenuItem = new ToolStripMenuItem();
            toolStripCreateFinish = new ToolStripMenuItem();
            menuStrip1.SuspendLayout();
            CreateObjectContextMenu.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { mapaToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(800, 24);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // mapaToolStripMenuItem
            // 
            mapaToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { otvorMapuToolStripMenuItem, otvorOverlayToolStripMenuItem, ulozMapuToolStripMenuItem, ulozOverlayToolStripMenuItem });
            mapaToolStripMenuItem.Name = "mapaToolStripMenuItem";
            mapaToolStripMenuItem.Size = new Size(49, 20);
            mapaToolStripMenuItem.Text = "Mapa";
            // 
            // otvorMapuToolStripMenuItem
            // 
            otvorMapuToolStripMenuItem.Name = "otvorMapuToolStripMenuItem";
            otvorMapuToolStripMenuItem.Size = new Size(145, 22);
            otvorMapuToolStripMenuItem.Text = "Otvor mapu";
            otvorMapuToolStripMenuItem.Click += otvorMapuToolStripMenuItem_Click;
            // 
            // otvorOverlayToolStripMenuItem
            // 
            otvorOverlayToolStripMenuItem.Name = "otvorOverlayToolStripMenuItem";
            otvorOverlayToolStripMenuItem.Size = new Size(145, 22);
            otvorOverlayToolStripMenuItem.Text = "Otvor overlay";
            otvorOverlayToolStripMenuItem.Click += otvorOverlayToolStripMenuItem_Click;
            // 
            // ulozMapuToolStripMenuItem
            // 
            ulozMapuToolStripMenuItem.Name = "ulozMapuToolStripMenuItem";
            ulozMapuToolStripMenuItem.Size = new Size(145, 22);
            ulozMapuToolStripMenuItem.Text = "Uloz mapu";
            ulozMapuToolStripMenuItem.Click += ulozMapuToolStripMenuItem_Click;
            // 
            // ulozOverlayToolStripMenuItem
            // 
            ulozOverlayToolStripMenuItem.Name = "ulozOverlayToolStripMenuItem";
            ulozOverlayToolStripMenuItem.Size = new Size(145, 22);
            ulozOverlayToolStripMenuItem.Text = "Uloz overlay";
            ulozOverlayToolStripMenuItem.Click += ulozOverlayToolStripMenuItem_Click;
            // 
            // OpenMapDialog
            // 
            OpenMapDialog.DefaultExt = "MAP";
            OpenMapDialog.Filter = "Maze Map|*.map";
            // 
            // SaveMapDialog
            // 
            SaveMapDialog.DefaultExt = "MAP";
            SaveMapDialog.Filter = "Maze Map|*.map";
            // 
            // OpenOverlayDialog
            // 
            OpenOverlayDialog.DefaultExt = "state";
            OpenOverlayDialog.FileName = "openFileDialog1";
            OpenOverlayDialog.Filter = "Overlay|*.state";
            // 
            // saveOverlayDialog
            // 
            saveOverlayDialog.DefaultExt = "state";
            saveOverlayDialog.Filter = "Overlay|*.state";
            // 
            // descriptionBox
            // 
            descriptionBox.Dock = DockStyle.Right;
            descriptionBox.Location = new Point(590, 24);
            descriptionBox.Multiline = true;
            descriptionBox.Name = "descriptionBox";
            descriptionBox.Size = new Size(210, 871);
            descriptionBox.TabIndex = 1;
            // 
            // CreateObjectContextMenu
            // 
            CreateObjectContextMenu.Items.AddRange(new ToolStripItem[] { deleteToolStripMenuItem, toolStripSeparator1, toolStripCreateDoor, toolStripCreateTrap, toolStripCreateItem, createStartToolStripMenuItem, toolStripCreateFinish });
            CreateObjectContextMenu.Name = "CreateObjectContextMenu";
            CreateObjectContextMenu.Size = new Size(181, 164);
            // 
            // deleteToolStripMenuItem
            // 
            deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            deleteToolStripMenuItem.Size = new Size(180, 22);
            deleteToolStripMenuItem.Text = "Delete";
            deleteToolStripMenuItem.Click += deleteToolStripMenuItem_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(177, 6);
            // 
            // toolStripCreateDoor
            // 
            toolStripCreateDoor.Name = "toolStripCreateDoor";
            toolStripCreateDoor.Size = new Size(180, 22);
            toolStripCreateDoor.Text = "Create Doors";
            toolStripCreateDoor.Click += toolStripCreateDoor_Click;
            // 
            // toolStripCreateTrap
            // 
            toolStripCreateTrap.DropDownItems.AddRange(new ToolStripItem[] { teleportingToolStripMenuItem });
            toolStripCreateTrap.Name = "toolStripCreateTrap";
            toolStripCreateTrap.Size = new Size(180, 22);
            toolStripCreateTrap.Text = "Create Trap";
            // 
            // teleportingToolStripMenuItem
            // 
            teleportingToolStripMenuItem.Name = "teleportingToolStripMenuItem";
            teleportingToolStripMenuItem.Size = new Size(116, 22);
            teleportingToolStripMenuItem.Text = "Teleport";
            teleportingToolStripMenuItem.Click += teleportingToolStripMenuItem_Click;
            // 
            // toolStripCreateItem
            // 
            toolStripCreateItem.DropDownItems.AddRange(new ToolStripItem[] { createItToolStripMenuItem, equipmentToolStripMenuItem });
            toolStripCreateItem.Name = "toolStripCreateItem";
            toolStripCreateItem.Size = new Size(180, 22);
            toolStripCreateItem.Text = "Create Item";
            // 
            // createItToolStripMenuItem
            // 
            createItToolStripMenuItem.Name = "createItToolStripMenuItem";
            createItToolStripMenuItem.Size = new Size(180, 22);
            createItToolStripMenuItem.Text = "Item";
            createItToolStripMenuItem.Click += toolStripCreateItem_Click;
            // 
            // equipmentToolStripMenuItem
            // 
            equipmentToolStripMenuItem.Name = "equipmentToolStripMenuItem";
            equipmentToolStripMenuItem.Size = new Size(180, 22);
            equipmentToolStripMenuItem.Text = "Equipment";
            equipmentToolStripMenuItem.Click += equipmentToolStripMenuItem_Click;
            // 
            // createStartToolStripMenuItem
            // 
            createStartToolStripMenuItem.Name = "createStartToolStripMenuItem";
            createStartToolStripMenuItem.Size = new Size(180, 22);
            createStartToolStripMenuItem.Text = "Create Start";
            createStartToolStripMenuItem.Click += createStartToolStripMenuItem_Click;
            // 
            // toolStripCreateFinish
            // 
            toolStripCreateFinish.Name = "toolStripCreateFinish";
            toolStripCreateFinish.Size = new Size(180, 22);
            toolStripCreateFinish.Text = "Create Finish";
            toolStripCreateFinish.Click += toolStripCreateFinish_Click;
            // 
            // Editor
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 895);
            Controls.Add(descriptionBox);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "Editor";
            Text = "Form1";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            CreateObjectContextMenu.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem mapaToolStripMenuItem;
        private ToolStripMenuItem otvorMapuToolStripMenuItem;
        private ToolStripMenuItem otvorOverlayToolStripMenuItem;
        private ToolStripMenuItem ulozOverlayToolStripMenuItem;
        private OpenFileDialog OpenMapDialog;
        private ToolStripMenuItem ulozMapuToolStripMenuItem;
        private SaveFileDialog SaveMapDialog;
        private OpenFileDialog OpenOverlayDialog;
        private SaveFileDialog saveOverlayDialog;
        private TextBox descriptionBox;
        private ContextMenuStrip CreateObjectContextMenu;
        private ToolStripMenuItem toolStripCreateDoor;
        private ToolStripMenuItem toolStripCreateTrap;
        private ToolStripMenuItem toolStripCreateItem;
        private ToolStripMenuItem toolStripCreateFinish;
        private ToolStripMenuItem teleportingToolStripMenuItem;
        private ToolStripMenuItem deleteToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem createStartToolStripMenuItem;
        private ToolStripMenuItem createItToolStripMenuItem;
        private ToolStripMenuItem equipmentToolStripMenuItem;
    }
}