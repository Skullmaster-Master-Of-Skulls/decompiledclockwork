using System;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI.Calendar
{
	// Token: 0x02000FFE RID: 4094
	public sealed class HeaderCellRenderEventArgs : EventArgs
	{
		// Token: 0x0600A00F RID: 40975 RVA: 0x0023A35B File Offset: 0x0023855B
		public HeaderCellRenderEventArgs(TableCell cell, HeaderType type)
		{
			this._cell = cell;
			this._headerType = type;
		}

		// Token: 0x17003294 RID: 12948
		// (get) Token: 0x0600A010 RID: 40976 RVA: 0x0023A371 File Offset: 0x00238571
		public TableCell Cell
		{
			get
			{
				return this._cell;
			}
		}

		// Token: 0x17003295 RID: 12949
		// (get) Token: 0x0600A011 RID: 40977 RVA: 0x0023A379 File Offset: 0x00238579
		public HeaderType HeaderType
		{
			get
			{
				return this._headerType;
			}
		}

		// Token: 0x04002CD4 RID: 11476
		private TableCell _cell;

		// Token: 0x04002CD5 RID: 11477
		private HeaderType _headerType;
	}
}
