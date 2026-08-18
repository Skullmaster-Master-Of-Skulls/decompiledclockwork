using System;

namespace Telerik.Web.UI.SpreadsheetCustomFormat
{
	// Token: 0x020008A9 RID: 2217
	internal class ViewFactory : IViewFactory
	{
		// Token: 0x17001AFD RID: 6909
		// (get) Token: 0x06005267 RID: 21095 RVA: 0x001005CB File Offset: 0x000FE7CB
		public CustomFormatTemplate Owner
		{
			get
			{
				return this._owner;
			}
		}

		// Token: 0x06005268 RID: 21096 RVA: 0x001005D3 File Offset: 0x000FE7D3
		public ViewFactory(CustomFormatTemplate owner)
		{
			this._owner = owner;
		}

		// Token: 0x06005269 RID: 21097 RVA: 0x001005E2 File Offset: 0x000FE7E2
		public ICustomFormatView CreateView()
		{
			return new View(this.Owner);
		}

		// Token: 0x0400141E RID: 5150
		private readonly CustomFormatTemplate _owner;
	}
}
