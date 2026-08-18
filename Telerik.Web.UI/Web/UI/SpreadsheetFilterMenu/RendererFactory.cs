using System;

namespace Telerik.Web.UI.SpreadsheetFilterMenu
{
	// Token: 0x020008B3 RID: 2227
	internal class RendererFactory
	{
		// Token: 0x17001B0E RID: 6926
		// (get) Token: 0x060052A6 RID: 21158 RVA: 0x00100D38 File Offset: 0x000FEF38
		public FilterMenuTemplate Owner
		{
			get
			{
				return this._owner;
			}
		}

		// Token: 0x060052A7 RID: 21159 RVA: 0x00100D40 File Offset: 0x000FEF40
		public RendererFactory(FilterMenuTemplate owner)
		{
			this._owner = owner;
		}

		// Token: 0x060052A8 RID: 21160 RVA: 0x00100D4F File Offset: 0x000FEF4F
		public IFilterMenuRenderer CreateRenderer()
		{
			return new Renderer(this.Owner.View);
		}

		// Token: 0x0400144E RID: 5198
		private readonly FilterMenuTemplate _owner;
	}
}
