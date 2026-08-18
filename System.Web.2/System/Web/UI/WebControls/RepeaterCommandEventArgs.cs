using System;

namespace System.Web.UI.WebControls
{
	// Token: 0x020004B3 RID: 1203
	public class RepeaterCommandEventArgs : CommandEventArgs
	{
		// Token: 0x06003C46 RID: 15430 RVA: 0x000C38C2 File Offset: 0x000C1AC2
		public RepeaterCommandEventArgs(RepeaterItem item, object commandSource, CommandEventArgs originalArgs) : base(originalArgs)
		{
			this.item = item;
			this.commandSource = commandSource;
		}

		// Token: 0x17001199 RID: 4505
		// (get) Token: 0x06003C47 RID: 15431 RVA: 0x000C38D9 File Offset: 0x000C1AD9
		public RepeaterItem Item
		{
			get
			{
				return this.item;
			}
		}

		// Token: 0x1700119A RID: 4506
		// (get) Token: 0x06003C48 RID: 15432 RVA: 0x000C38E1 File Offset: 0x000C1AE1
		public object CommandSource
		{
			get
			{
				return this.commandSource;
			}
		}

		// Token: 0x04002373 RID: 9075
		private RepeaterItem item;

		// Token: 0x04002374 RID: 9076
		private object commandSource;
	}
}
