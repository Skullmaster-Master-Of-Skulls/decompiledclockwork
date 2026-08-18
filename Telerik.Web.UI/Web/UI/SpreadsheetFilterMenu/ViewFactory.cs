using System;

namespace Telerik.Web.UI.SpreadsheetFilterMenu
{
	// Token: 0x020008B8 RID: 2232
	internal class ViewFactory : IViewFactory
	{
		// Token: 0x17001B27 RID: 6951
		// (get) Token: 0x060052EB RID: 21227 RVA: 0x001016B6 File Offset: 0x000FF8B6
		public FilterMenuTemplate Owner
		{
			get
			{
				return this._owner;
			}
		}

		// Token: 0x060052EC RID: 21228 RVA: 0x001016BE File Offset: 0x000FF8BE
		public ViewFactory(FilterMenuTemplate owner)
		{
			this._owner = owner;
		}

		// Token: 0x060052ED RID: 21229 RVA: 0x001016CD File Offset: 0x000FF8CD
		public IFilterMenuView CreateView()
		{
			return new View(this.Owner);
		}

		// Token: 0x0400145A RID: 5210
		private readonly FilterMenuTemplate _owner;
	}
}
