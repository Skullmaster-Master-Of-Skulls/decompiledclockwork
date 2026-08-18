using System;

namespace System.Web.UI.WebControls
{
	// Token: 0x020003CD RID: 973
	public class DataListCommandEventArgs : CommandEventArgs
	{
		// Token: 0x06002F17 RID: 12055 RVA: 0x0009A135 File Offset: 0x00098335
		public DataListCommandEventArgs(DataListItem item, object commandSource, CommandEventArgs originalArgs) : base(originalArgs)
		{
			this.item = item;
			this.commandSource = commandSource;
		}

		// Token: 0x17000D82 RID: 3458
		// (get) Token: 0x06002F18 RID: 12056 RVA: 0x0009A14C File Offset: 0x0009834C
		public DataListItem Item
		{
			get
			{
				return this.item;
			}
		}

		// Token: 0x17000D83 RID: 3459
		// (get) Token: 0x06002F19 RID: 12057 RVA: 0x0009A154 File Offset: 0x00098354
		public object CommandSource
		{
			get
			{
				return this.commandSource;
			}
		}

		// Token: 0x04002024 RID: 8228
		private DataListItem item;

		// Token: 0x04002025 RID: 8229
		private object commandSource;
	}
}
