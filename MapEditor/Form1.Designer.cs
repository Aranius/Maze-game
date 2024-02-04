namespace MapEditor
{
    partial class Form1
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
            components=new System.ComponentModel.Container();
            menuStrip1=new MenuStrip();
            mapaToolStripMenuItem=new ToolStripMenuItem();
            otvorMapuToolStripMenuItem=new ToolStripMenuItem();
            otvorOverlayToolStripMenuItem=new ToolStripMenuItem();
            ulozMapuToolStripMenuItem=new ToolStripMenuItem();
            ulozOverlayToolStripMenuItem=new ToolStripMenuItem();
            OpenMapDialog=new OpenFileDialog();
            SaveMapDialog=new SaveFileDialog();
            OpenOverlayDialog=new OpenFileDialog();
            saveOverlayDialog=new SaveFileDialog();
            descriptionBox=new TextBox();
            CreateObjectContextMenu=new ContextMenuStrip(components);
            toolStripCreateDoor=new ToolStripMenuItem();
            toolStripCreateTrap=new ToolStripMenuItem();
            toolStripCreateItem=new ToolStripMenuItem();
            toolStripCreateFinish=new ToolStripMenuItem();
            menuStrip1.SuspendLayout();
            CreateObjectContextMenu.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { mapaToolStripMenuItem });
            menuStrip1.Location=new Point(0, 0);
            menuStrip1.Name="menuStrip1";
            menuStrip1.Size=new Size(800, 24);
            menuStrip1.TabIndex=0;
            menuStrip1.Text="menuStrip1";
            // 
            // mapaToolStripMenuItem
            // 
            mapaToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { otvorMapuToolStripMenuItem, otvorOverlayToolStripMenuItem, ulozMapuToolStripMenuItem, ulozOverlayToolStripMenuItem });
            mapaToolStripMenuItem.Name="mapaToolStripMenuItem";
            mapaToolStripMenuItem.Size=new Size(49, 20);
            mapaToolStripMenuItem.Text="Mapa";
            // 
            // otvorMapuToolStripMenuItem
            // 
            otvorMapuToolStripMenuItem.Name="otvorMapuToolStripMenuItem";
            otvorMapuToolStripMenuItem.Size=new Size(145, 22);
            otvorMapuToolStripMenuItem.Text="Otvor mapu";
            otvorMapuToolStripMenuItem.Click+=otvorMapuToolStripMenuItem_Click;
            // 
            // otvorOverlayToolStripMenuItem
            // 
            otvorOverlayToolStripMenuItem.Name="otvorOverlayToolStripMenuItem";
            otvorOverlayToolStripMenuItem.Size=new Size(145, 22);
            otvorOverlayToolStripMenuItem.Text="Otvor overlay";
            otvorOverlayToolStripMenuItem.Click+=otvorOverlayToolStripMenuItem_Click;
            // 
            // ulozMapuToolStripMenuItem
            // 
            ulozMapuToolStripMenuItem.Name="ulozMapuToolStripMenuItem";
            ulozMapuToolStripMenuItem.Size=new Size(145, 22);
            ulozMapuToolStripMenuItem.Text="Uloz mapu";
            ulozMapuToolStripMenuItem.Click+=ulozMapuToolStripMenuItem_Click;
            // 
            // ulozOverlayToolStripMenuItem
            // 
            ulozOverlayToolStripMenuItem.Name="ulozOverlayToolStripMenuItem";
            ulozOverlayToolStripMenuItem.Size=new Size(145, 22);
            ulozOverlayToolStripMenuItem.Text="Uloz overlay";
            ulozOverlayToolStripMenuItem.Click+=ulozOverlayToolStripMenuItem_Click;
            // 
            // OpenMapDialog
            // 
            OpenMapDialog.DefaultExt="MAP";
            OpenMapDialog.Filter="Maze Map|*.map";
            // 
            // SaveMapDialog
            // 
            SaveMapDialog.DefaultExt="MAP";
            SaveMapDialog.Filter="Maze Map|*.map";
            // 
            // OpenOverlayDialog
            // 
            OpenOverlayDialog.DefaultExt="state";
            OpenOverlayDialog.FileName="openFileDialog1";
            OpenOverlayDialog.Filter="Overlay|*.state";
            // 
            // saveOverlayDialog
            // 
            saveOverlayDialog.DefaultExt="state";
            saveOverlayDialog.Filter="Overlay|*.state";
            // 
            // descriptionBox
            // 
            descriptionBox.Dock=DockStyle.Right;
            descriptionBox.Location=new Point(700, 24);
            descriptionBox.Multiline=true;
            descriptionBox.Name="descriptionBox";
            descriptionBox.Size=new Size(100, 426);
            descriptionBox.TabIndex=1;
            // 
            // CreateObjectContextMenu
            // 
            CreateObjectContextMenu.Items.AddRange(new ToolStripItem[] { toolStripCreateDoor, toolStripCreateTrap, toolStripCreateItem, toolStripCreateFinish });
            CreateObjectContextMenu.Name="CreateObjectContextMenu";
            CreateObjectContextMenu.Size=new Size(143, 92);
            // 
            // toolStripCreateDoor
            // 
            toolStripCreateDoor.Name="toolStripCreateDoor";
            toolStripCreateDoor.Size=new Size(142, 22);
            toolStripCreateDoor.Text="Create Doors";
            toolStripCreateDoor.Click+=toolStripCreateDoor_Click;
            // 
            // toolStripCreateTrap
            // 
            toolStripCreateTrap.Name="toolStripCreateTrap";
            toolStripCreateTrap.Size=new Size(142, 22);
            toolStripCreateTrap.Text="Create Trap";
            // 
            // toolStripCreateItem
            // 
            toolStripCreateItem.Name="toolStripCreateItem";
            toolStripCreateItem.Size=new Size(142, 22);
            toolStripCreateItem.Text="CreateItem";
            // 
            // toolStripCreateFinish
            // 
            toolStripCreateFinish.Name="toolStripCreateFinish";
            toolStripCreateFinish.Size=new Size(142, 22);
            toolStripCreateFinish.Text="CreateFinish";
            // 
            // Form1
            // 
            AutoScaleDimensions=new SizeF(7F, 15F);
            AutoScaleMode=AutoScaleMode.Font;
            ClientSize=new Size(800, 450);
            Controls.Add(descriptionBox);
            Controls.Add(menuStrip1);
            MainMenuStrip=menuStrip1;
            Name="Form1";
            Text="Form1";
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
    }
}