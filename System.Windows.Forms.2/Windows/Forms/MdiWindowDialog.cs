using System;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Windows.Forms
{
	// Token: 0x020002EF RID: 751
	[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	internal sealed partial class MdiWindowDialog : Form
	{
		// Token: 0x06002F92 RID: 12178 RVA: 0x000D6AF8 File Offset: 0x000D4CF8
		public MdiWindowDialog()
		{
			this.InitializeComponent();
		}

		// Token: 0x17000B31 RID: 2865
		// (get) Token: 0x06002F93 RID: 12179 RVA: 0x000D6B06 File Offset: 0x000D4D06
		public Form ActiveChildForm
		{
			get
			{
				return this.active;
			}
		}

		// Token: 0x06002F94 RID: 12180 RVA: 0x000D6B10 File Offset: 0x000D4D10
		public void SetItems(Form active, Form[] all)
		{
			int selectedIndex = 0;
			for (int i = 0; i < all.Length; i++)
			{
				if (all[i].Visible)
				{
					int num = this.itemList.Items.Add(new MdiWindowDialog.ListItem(all[i]));
					if (all[i].Equals(active))
					{
						selectedIndex = num;
					}
				}
			}
			this.active = active;
			this.itemList.SelectedIndex = selectedIndex;
		}

		// Token: 0x06002F95 RID: 12181 RVA: 0x000D6B70 File Offset: 0x000D4D70
		private void ItemList_doubleClick(object source, EventArgs e)
		{
			this.okButton.PerformClick();
		}

		// Token: 0x06002F96 RID: 12182 RVA: 0x000D6B80 File Offset: 0x000D4D80
		private void ItemList_selectedIndexChanged(object source, EventArgs e)
		{
			MdiWindowDialog.ListItem listItem = (MdiWindowDialog.ListItem)this.itemList.SelectedItem;
			if (listItem != null)
			{
				this.active = listItem.form;
			}
		}

		// Token: 0x040013AC RID: 5036
		private Form active;

		// Token: 0x020006D5 RID: 1749
		private class ListItem
		{
			// Token: 0x06006AE0 RID: 27360 RVA: 0x0018C1BE File Offset: 0x0018A3BE
			public ListItem(Form f)
			{
				this.form = f;
			}

			// Token: 0x06006AE1 RID: 27361 RVA: 0x0018C1CD File Offset: 0x0018A3CD
			public override string ToString()
			{
				return this.form.Text;
			}

			// Token: 0x04003B50 RID: 15184
			public Form form;
		}
	}
}
