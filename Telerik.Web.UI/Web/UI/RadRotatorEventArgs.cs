using System;

namespace Telerik.Web.UI
{
	// Token: 0x020019FE RID: 6654
	public class RadRotatorEventArgs : EventArgs
	{
		// Token: 0x060101A5 RID: 65957 RVA: 0x0039EB9E File Offset: 0x0039CD9E
		public RadRotatorEventArgs(RadRotatorItem item)
		{
			this.Item = item;
		}

		// Token: 0x17004DBA RID: 19898
		// (get) Token: 0x060101A6 RID: 65958 RVA: 0x0039EBAD File Offset: 0x0039CDAD
		// (set) Token: 0x060101A7 RID: 65959 RVA: 0x0039EBB5 File Offset: 0x0039CDB5
		public RadRotatorItem Item
		{
			get
			{
				return this._item;
			}
			private set
			{
				this._item = value;
			}
		}

		// Token: 0x040048ED RID: 18669
		private RadRotatorItem _item;
	}
}
