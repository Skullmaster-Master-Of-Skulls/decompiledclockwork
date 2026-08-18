using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ClockWorkAPI.AT
{
	// Token: 0x0200001A RID: 26
	public class AtItemChooserTextBox : UserControl, IDisposable
	{
		// Token: 0x060000DF RID: 223 RVA: 0x000068F0 File Offset: 0x000058F0
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00006928 File Offset: 0x00005928
		private void InitializeComponent()
		{
			this.txt = new TextBox();
			this.link_createNewItem = new LinkLabel();
			base.SuspendLayout();
			this.txt.Dock = DockStyle.Fill;
			this.txt.Location = new Point(2, 2);
			this.txt.Margin = new Padding(4);
			this.txt.Name = "txt";
			this.txt.Size = new Size(281, 26);
			this.txt.TabIndex = 0;
			this.txt.KeyDown += this.txt_KeyDown;
			this.txt.Leave += this.txt_Leave;
			this.txt.KeyUp += this.txt_KeyUp;
			this.txt.KeyPress += this.txt_KeyPress;
			this.link_createNewItem.Dock = DockStyle.Right;
			this.link_createNewItem.Enabled = false;
			this.link_createNewItem.Location = new Point(283, 2);
			this.link_createNewItem.Name = "link_createNewItem";
			this.link_createNewItem.Size = new Size(133, 27);
			this.link_createNewItem.TabIndex = 1;
			this.link_createNewItem.TabStop = true;
			this.link_createNewItem.Text = "&Create new item";
			this.link_createNewItem.TextAlign = ContentAlignment.MiddleRight;
			base.AutoScaleDimensions = new SizeF(9f, 18f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.Controls.Add(this.txt);
			base.Controls.Add(this.link_createNewItem);
			this.Font = new Font("Arial", 12f, FontStyle.Regular, GraphicsUnit.Point, 0);
			base.Margin = new Padding(4);
			base.Name = "AtItemChooserTextBox";
			base.Padding = new Padding(2);
			base.Size = new Size(418, 31);
			base.KeyPress += this.AtItemChooserTextBox_KeyPress;
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060000E1 RID: 225 RVA: 0x00006B64 File Offset: 0x00005B64
		// (set) Token: 0x060000E2 RID: 226 RVA: 0x00006B7C File Offset: 0x00005B7C
		public ItemCollection Items
		{
			get
			{
				return this.items;
			}
			set
			{
				this.items = value;
			}
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00006B88 File Offset: 0x00005B88
		public AtItemChooserTextBox(ItemCollection items)
		{
			this.popup = new ATItemChooserTextBox_PopupList();
			this.lb = this.popup.LB;
			this.items = items;
			this.InitializeComponent();
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00006BE4 File Offset: 0x00005BE4
		public AtItemChooserTextBox()
		{
			this.popup = new ATItemChooserTextBox_PopupList();
			this.lb = this.popup.LB;
			this.items = new ItemCollection();
			this.InitializeComponent();
			this.popup.OnUserSelectedItem += this.popup_OnUserSelectedItem;
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00006C5C File Offset: 0x00005C5C
		~AtItemChooserTextBox()
		{
			if (this.popup != null)
			{
				this.popup.Close();
				this.popup.OnUserSelectedItem -= this.popup_OnUserSelectedItem;
				this.lb = null;
				this.popup.Dispose();
				this.popup = null;
			}
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00006CD4 File Offset: 0x00005CD4
		private void popup_OnUserSelectedItem(object sender, string selectedText)
		{
			this.txt_KeyPress(this.txt, new KeyPressEventArgs('.'));
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00006CEC File Offset: 0x00005CEC
		private void txt_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Up)
			{
				this.popup.SimulateUpDown(-1);
				e.Handled = true;
			}
			else if (e.KeyCode == Keys.Down)
			{
				this.popup.SimulateUpDown(1);
				e.Handled = true;
			}
			else if (e.KeyCode == Keys.Return || e.KeyCode == Keys.Tab)
			{
				this.popup.SimulateEnter();
			}
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00006D7A File Offset: 0x00005D7A
		private void AtItemChooserTextBox_KeyPress(object sender, KeyPressEventArgs e)
		{
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00006D7D File Offset: 0x00005D7D
		private void RefreshCreateNewLink()
		{
			this.link_createNewItem.Enabled = (this.selectedUnknownItem != null && this.selectedUnknownItem.Length > 0);
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00006DA8 File Offset: 0x00005DA8
		private void txt_KeyPress(object sender, KeyPressEventArgs e)
		{
			char keyChar = e.KeyChar;
			this.items.Parse(this.txt.Text, out this.selectedItemCategory, out this.selectedItem, out this.selectedUnknownItem);
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00006DE6 File Offset: 0x00005DE6
		private void txt_Leave(object sender, EventArgs e)
		{
			this.popup.Hide();
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00006DF8 File Offset: 0x00005DF8
		private void txt_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
			{
				e.Handled = true;
			}
		}

		// Token: 0x0400007D RID: 125
		private IContainer components = null;

		// Token: 0x0400007E RID: 126
		private TextBox txt;

		// Token: 0x0400007F RID: 127
		private LinkLabel link_createNewItem;

		// Token: 0x04000080 RID: 128
		private ATItemChooserTextBox_PopupList popup;

		// Token: 0x04000081 RID: 129
		private ListBox lb;

		// Token: 0x04000082 RID: 130
		private ItemCollection items;

		// Token: 0x04000083 RID: 131
		private ItemCategory selectedItemCategory = null;

		// Token: 0x04000084 RID: 132
		private Item selectedItem = null;

		// Token: 0x04000085 RID: 133
		private string selectedUnknownItem = null;
	}
}
