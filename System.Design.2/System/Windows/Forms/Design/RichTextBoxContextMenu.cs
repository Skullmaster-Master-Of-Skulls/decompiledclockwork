using System;
using System.Design;

namespace System.Windows.Forms.Design
{
	// Token: 0x02000324 RID: 804
	internal class RichTextBoxContextMenu : ContextMenu
	{
		// Token: 0x06001FD0 RID: 8144 RVA: 0x000C0E54 File Offset: 0x000BF054
		public RichTextBoxContextMenu(RichTextBox parent)
		{
			this.undoMenu = new MenuItem(SR.GetString("StandardMenuUndo"), new EventHandler(this.undoMenu_Clicked));
			this.cutMenu = new MenuItem(SR.GetString("StandardMenuCut"), new EventHandler(this.cutMenu_Clicked));
			this.copyMenu = new MenuItem(SR.GetString("StandardMenuCopy"), new EventHandler(this.copyMenu_Clicked));
			this.pasteMenu = new MenuItem(SR.GetString("StandardMenuPaste"), new EventHandler(this.pasteMenu_Clicked));
			this.deleteMenu = new MenuItem(SR.GetString("StandardMenuDelete"), new EventHandler(this.deleteMenu_Clicked));
			this.selectAllMenu = new MenuItem(SR.GetString("StandardMenuSelectAll"), new EventHandler(this.selectAllMenu_Clicked));
			MenuItem item = new MenuItem("-");
			MenuItem item2 = new MenuItem("-");
			base.MenuItems.Add(this.undoMenu);
			base.MenuItems.Add(item);
			base.MenuItems.Add(this.cutMenu);
			base.MenuItems.Add(this.copyMenu);
			base.MenuItems.Add(this.pasteMenu);
			base.MenuItems.Add(this.deleteMenu);
			base.MenuItems.Add(item2);
			base.MenuItems.Add(this.selectAllMenu);
			this.parent = parent;
		}

		// Token: 0x06001FD1 RID: 8145 RVA: 0x000C0FD0 File Offset: 0x000BF1D0
		protected override void OnPopup(EventArgs e)
		{
			if (this.parent.SelectionLength > 0)
			{
				this.cutMenu.Enabled = true;
				this.copyMenu.Enabled = true;
				this.deleteMenu.Enabled = true;
			}
			else
			{
				this.cutMenu.Enabled = false;
				this.copyMenu.Enabled = false;
				this.deleteMenu.Enabled = false;
			}
			if (Clipboard.GetText() != null)
			{
				this.pasteMenu.Enabled = true;
			}
			else
			{
				this.pasteMenu.Enabled = false;
			}
			if (this.parent.CanUndo)
			{
				this.undoMenu.Enabled = true;
				return;
			}
			this.undoMenu.Enabled = false;
		}

		// Token: 0x06001FD2 RID: 8146 RVA: 0x000C107C File Offset: 0x000BF27C
		private void cutMenu_Clicked(object sender, EventArgs e)
		{
			Clipboard.SetText(this.parent.SelectedText);
			this.parent.SelectedText = "";
		}

		// Token: 0x06001FD3 RID: 8147 RVA: 0x000C109E File Offset: 0x000BF29E
		private void copyMenu_Clicked(object sender, EventArgs e)
		{
			Clipboard.SetText(this.parent.SelectedText);
		}

		// Token: 0x06001FD4 RID: 8148 RVA: 0x000C10B0 File Offset: 0x000BF2B0
		private void deleteMenu_Clicked(object sender, EventArgs e)
		{
			this.parent.SelectedText = "";
		}

		// Token: 0x06001FD5 RID: 8149 RVA: 0x000C10C2 File Offset: 0x000BF2C2
		private void pasteMenu_Clicked(object sender, EventArgs e)
		{
			this.parent.SelectedText = Clipboard.GetText();
		}

		// Token: 0x06001FD6 RID: 8150 RVA: 0x000C10D4 File Offset: 0x000BF2D4
		private void selectAllMenu_Clicked(object sender, EventArgs e)
		{
			this.parent.SelectAll();
		}

		// Token: 0x06001FD7 RID: 8151 RVA: 0x000C10E1 File Offset: 0x000BF2E1
		private void undoMenu_Clicked(object sender, EventArgs e)
		{
			this.parent.Undo();
		}

		// Token: 0x04001892 RID: 6290
		private MenuItem undoMenu;

		// Token: 0x04001893 RID: 6291
		private MenuItem cutMenu;

		// Token: 0x04001894 RID: 6292
		private MenuItem copyMenu;

		// Token: 0x04001895 RID: 6293
		private MenuItem pasteMenu;

		// Token: 0x04001896 RID: 6294
		private MenuItem deleteMenu;

		// Token: 0x04001897 RID: 6295
		private MenuItem selectAllMenu;

		// Token: 0x04001898 RID: 6296
		private RichTextBox parent;
	}
}
