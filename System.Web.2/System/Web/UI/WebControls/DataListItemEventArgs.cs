using System;

namespace System.Web.UI.WebControls
{
	// Token: 0x020003D1 RID: 977
	public class DataListItemEventArgs : EventArgs
	{
		// Token: 0x06002F34 RID: 12084 RVA: 0x0009A37D File Offset: 0x0009857D
		public DataListItemEventArgs(DataListItem item)
		{
			this.item = item;
		}

		// Token: 0x17000D90 RID: 3472
		// (get) Token: 0x06002F35 RID: 12085 RVA: 0x0009A38C File Offset: 0x0009858C
		public DataListItem Item
		{
			get
			{
				return this.item;
			}
		}

		// Token: 0x0400202A RID: 8234
		private DataListItem item;
	}
}
