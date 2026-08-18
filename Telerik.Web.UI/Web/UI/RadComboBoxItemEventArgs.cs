using System;

namespace Telerik.Web.UI
{
	// Token: 0x0200101D RID: 4125
	public class RadComboBoxItemEventArgs : EventArgs
	{
		// Token: 0x0600A2EF RID: 41711 RVA: 0x0024438C File Offset: 0x0024258C
		public RadComboBoxItemEventArgs(RadComboBoxItem item)
		{
			this._item = item;
		}

		// Token: 0x1700337C RID: 13180
		// (get) Token: 0x0600A2F0 RID: 41712 RVA: 0x0024439B File Offset: 0x0024259B
		// (set) Token: 0x0600A2F1 RID: 41713 RVA: 0x002443A3 File Offset: 0x002425A3
		public RadComboBoxItem Item
		{
			get
			{
				return this._item;
			}
			set
			{
				this._item = value;
			}
		}

		// Token: 0x04002D4B RID: 11595
		private RadComboBoxItem _item;
	}
}
