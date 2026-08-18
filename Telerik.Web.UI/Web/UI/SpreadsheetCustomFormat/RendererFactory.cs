using System;

namespace Telerik.Web.UI.SpreadsheetCustomFormat
{
	// Token: 0x020008A4 RID: 2212
	internal class RendererFactory
	{
		// Token: 0x17001AED RID: 6893
		// (get) Token: 0x0600523B RID: 21051 RVA: 0x000FFF8C File Offset: 0x000FE18C
		public CustomFormatTemplate Owner
		{
			get
			{
				return this._owner;
			}
		}

		// Token: 0x0600523C RID: 21052 RVA: 0x000FFF94 File Offset: 0x000FE194
		public RendererFactory(CustomFormatTemplate owner)
		{
			this._owner = owner;
		}

		// Token: 0x0600523D RID: 21053 RVA: 0x000FFFA3 File Offset: 0x000FE1A3
		public ICustomFormatRenderer CreateRenderer()
		{
			return new Renderer(this.Owner.View);
		}

		// Token: 0x04001410 RID: 5136
		private readonly CustomFormatTemplate _owner;
	}
}
