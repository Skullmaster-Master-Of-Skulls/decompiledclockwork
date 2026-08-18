using System;

namespace System.Web.UI.WebControls
{
	// Token: 0x020003D7 RID: 983
	public class DetailsViewCommandEventArgs : CommandEventArgs
	{
		// Token: 0x0600301D RID: 12317 RVA: 0x0009E3D1 File Offset: 0x0009C5D1
		public DetailsViewCommandEventArgs(object commandSource, CommandEventArgs originalArgs) : base(originalArgs)
		{
			this._commandSource = commandSource;
		}

		// Token: 0x17000DDA RID: 3546
		// (get) Token: 0x0600301E RID: 12318 RVA: 0x0009E3E1 File Offset: 0x0009C5E1
		public object CommandSource
		{
			get
			{
				return this._commandSource;
			}
		}

		// Token: 0x17000DDB RID: 3547
		// (get) Token: 0x0600301F RID: 12319 RVA: 0x0009E3E9 File Offset: 0x0009C5E9
		// (set) Token: 0x06003020 RID: 12320 RVA: 0x0009E3F1 File Offset: 0x0009C5F1
		public bool Handled { get; set; }

		// Token: 0x0400206E RID: 8302
		private object _commandSource;
	}
}
