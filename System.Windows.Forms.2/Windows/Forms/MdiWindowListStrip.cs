using System;
using System.Globalization;
using System.Security;

namespace System.Windows.Forms
{
	// Token: 0x020002ED RID: 749
	internal class MdiWindowListStrip : MenuStrip
	{
		// Token: 0x06002F8B RID: 12171 RVA: 0x000D673B File Offset: 0x000D493B
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.mdiParent = null;
			}
			base.Dispose(disposing);
		}

		// Token: 0x17000B2F RID: 2863
		// (get) Token: 0x06002F8C RID: 12172 RVA: 0x000D6750 File Offset: 0x000D4950
		internal ToolStripMenuItem MergeItem
		{
			get
			{
				if (this.mergeItem == null)
				{
					this.mergeItem = new ToolStripMenuItem();
					this.mergeItem.MergeAction = MergeAction.MatchOnly;
				}
				if (this.mergeItem.Owner == null)
				{
					this.Items.Add(this.mergeItem);
				}
				return this.mergeItem;
			}
		}

		// Token: 0x17000B30 RID: 2864
		// (get) Token: 0x06002F8D RID: 12173 RVA: 0x000D67A1 File Offset: 0x000D49A1
		// (set) Token: 0x06002F8E RID: 12174 RVA: 0x000D67A9 File Offset: 0x000D49A9
		internal MenuStrip MergedMenu
		{
			get
			{
				return this.mergedMenu;
			}
			set
			{
				this.mergedMenu = value;
			}
		}

		// Token: 0x06002F8F RID: 12175 RVA: 0x000D67B4 File Offset: 0x000D49B4
		public void PopulateItems(Form mdiParent, ToolStripMenuItem mdiMergeItem, bool includeSeparator)
		{
			this.mdiParent = mdiParent;
			base.SuspendLayout();
			this.MergeItem.DropDown.SuspendLayout();
			try
			{
				ToolStripMenuItem toolStripMenuItem = this.MergeItem;
				toolStripMenuItem.DropDownItems.Clear();
				toolStripMenuItem.Text = mdiMergeItem.Text;
				Form[] mdiChildren = mdiParent.MdiChildren;
				if (mdiChildren != null && mdiChildren.Length != 0)
				{
					if (includeSeparator)
					{
						ToolStripSeparator toolStripSeparator = new ToolStripSeparator();
						toolStripSeparator.MergeAction = MergeAction.Append;
						toolStripSeparator.MergeIndex = -1;
						toolStripMenuItem.DropDownItems.Add(toolStripSeparator);
					}
					Form activeMdiChild = mdiParent.ActiveMdiChild;
					int num = 0;
					int num2 = 1;
					int num3 = 0;
					bool flag = false;
					for (int i = 0; i < mdiChildren.Length; i++)
					{
						if (mdiChildren[i].Visible && mdiChildren[i].CloseReason == CloseReason.None)
						{
							num++;
							if ((flag && num3 < 9) || (!flag && num3 < 8) || mdiChildren[i].Equals(activeMdiChild))
							{
								string text = WindowsFormsUtils.EscapeTextWithAmpersands(mdiParent.MdiChildren[i].Text);
								text = ((text == null) ? string.Empty : text);
								ToolStripMenuItem toolStripMenuItem2 = new ToolStripMenuItem(mdiParent.MdiChildren[i]);
								toolStripMenuItem2.Text = string.Format(CultureInfo.CurrentCulture, "&{0} {1}", new object[]
								{
									num2,
									text
								});
								toolStripMenuItem2.MergeAction = MergeAction.Append;
								toolStripMenuItem2.MergeIndex = num2;
								toolStripMenuItem2.Click += this.OnWindowListItemClick;
								if (mdiChildren[i].Equals(activeMdiChild))
								{
									toolStripMenuItem2.Checked = true;
									flag = true;
								}
								num2++;
								num3++;
								toolStripMenuItem.DropDownItems.Add(toolStripMenuItem2);
							}
						}
					}
					if (num > 9)
					{
						ToolStripMenuItem toolStripMenuItem3 = new ToolStripMenuItem();
						toolStripMenuItem3.Text = SR.GetString("MDIMenuMoreWindows");
						toolStripMenuItem3.Click += this.OnMoreWindowsMenuItemClick;
						toolStripMenuItem3.MergeAction = MergeAction.Append;
						toolStripMenuItem.DropDownItems.Add(toolStripMenuItem3);
					}
				}
			}
			finally
			{
				base.ResumeLayout(false);
				this.MergeItem.DropDown.ResumeLayout(false);
			}
		}

		// Token: 0x06002F90 RID: 12176 RVA: 0x000D69D8 File Offset: 0x000D4BD8
		private void OnMoreWindowsMenuItemClick(object sender, EventArgs e)
		{
			Form[] mdiChildren = this.mdiParent.MdiChildren;
			if (mdiChildren != null)
			{
				IntSecurity.AllWindows.Assert();
				try
				{
					using (MdiWindowDialog mdiWindowDialog = new MdiWindowDialog())
					{
						mdiWindowDialog.SetItems(this.mdiParent.ActiveMdiChild, mdiChildren);
						DialogResult dialogResult = mdiWindowDialog.ShowDialog();
						if (dialogResult == DialogResult.OK)
						{
							mdiWindowDialog.ActiveChildForm.Activate();
							if (mdiWindowDialog.ActiveChildForm.ActiveControl != null && !mdiWindowDialog.ActiveChildForm.ActiveControl.Focused)
							{
								mdiWindowDialog.ActiveChildForm.ActiveControl.Focus();
							}
						}
					}
				}
				finally
				{
					CodeAccessPermission.RevertAssert();
				}
			}
		}

		// Token: 0x06002F91 RID: 12177 RVA: 0x000D6A8C File Offset: 0x000D4C8C
		private void OnWindowListItemClick(object sender, EventArgs e)
		{
			ToolStripMenuItem toolStripMenuItem = sender as ToolStripMenuItem;
			if (toolStripMenuItem != null)
			{
				Form mdiForm = toolStripMenuItem.MdiForm;
				if (mdiForm != null)
				{
					IntSecurity.ModifyFocus.Assert();
					try
					{
						mdiForm.Activate();
						if (mdiForm.ActiveControl != null && !mdiForm.ActiveControl.Focused)
						{
							mdiForm.ActiveControl.Focus();
						}
					}
					finally
					{
						CodeAccessPermission.RevertAssert();
					}
				}
			}
		}

		// Token: 0x040013A0 RID: 5024
		private Form mdiParent;

		// Token: 0x040013A1 RID: 5025
		private ToolStripMenuItem mergeItem;

		// Token: 0x040013A2 RID: 5026
		private MenuStrip mergedMenu;
	}
}
