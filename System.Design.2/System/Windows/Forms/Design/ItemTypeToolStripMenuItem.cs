using System;
using System.Drawing;
using System.Drawing.Design;

namespace System.Windows.Forms.Design
{
	// Token: 0x02000302 RID: 770
	internal class ItemTypeToolStripMenuItem : ToolStripMenuItem
	{
		// Token: 0x06001E8F RID: 7823 RVA: 0x000B7353 File Offset: 0x000B5553
		public ItemTypeToolStripMenuItem(Type t)
		{
			this._itemType = t;
		}

		// Token: 0x1700068A RID: 1674
		// (get) Token: 0x06001E90 RID: 7824 RVA: 0x000B736D File Offset: 0x000B556D
		public Type ItemType
		{
			get
			{
				return this._itemType;
			}
		}

		// Token: 0x1700068B RID: 1675
		// (get) Token: 0x06001E91 RID: 7825 RVA: 0x000B7375 File Offset: 0x000B5575
		// (set) Token: 0x06001E92 RID: 7826 RVA: 0x000B737D File Offset: 0x000B557D
		public bool ConvertTo
		{
			get
			{
				return this.convertTo;
			}
			set
			{
				this.convertTo = value;
			}
		}

		// Token: 0x1700068C RID: 1676
		// (get) Token: 0x06001E93 RID: 7827 RVA: 0x000B7386 File Offset: 0x000B5586
		// (set) Token: 0x06001E94 RID: 7828 RVA: 0x00003937 File Offset: 0x00001B37
		public override Image Image
		{
			get
			{
				if (this._image == null)
				{
					this._image = ToolStripDesignerUtils.GetToolboxBitmap(this.ItemType);
				}
				return this._image;
			}
			set
			{
			}
		}

		// Token: 0x1700068D RID: 1677
		// (get) Token: 0x06001E95 RID: 7829 RVA: 0x000B73A7 File Offset: 0x000B55A7
		// (set) Token: 0x06001E96 RID: 7830 RVA: 0x00003937 File Offset: 0x00001B37
		public override string Text
		{
			get
			{
				return ToolStripDesignerUtils.GetToolboxDescription(this.ItemType);
			}
			set
			{
			}
		}

		// Token: 0x06001E97 RID: 7831 RVA: 0x000B73B4 File Offset: 0x000B55B4
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.tbxItem = null;
			}
			base.Dispose(disposing);
		}

		// Token: 0x040017CD RID: 6093
		private static string systemWindowsFormsNamespace = typeof(ToolStripItem).Namespace;

		// Token: 0x040017CE RID: 6094
		private static ToolboxItem invalidToolboxItem = new ToolboxItem();

		// Token: 0x040017CF RID: 6095
		private Type _itemType;

		// Token: 0x040017D0 RID: 6096
		private bool convertTo;

		// Token: 0x040017D1 RID: 6097
		private ToolboxItem tbxItem = ItemTypeToolStripMenuItem.invalidToolboxItem;

		// Token: 0x040017D2 RID: 6098
		private Image _image;
	}
}
