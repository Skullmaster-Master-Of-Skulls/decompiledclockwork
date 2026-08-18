using System;
using System.Collections;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x0200088A RID: 2186
	public class SearchBoxDataSourceSelectEventArgs : EventArgs
	{
		// Token: 0x060050DE RID: 20702 RVA: 0x000FC10B File Offset: 0x000FA30B
		public SearchBoxDataSourceSelectEventArgs(IDataSource dataSource, string filterString, bool showAllResults, IDictionary userContext)
		{
			this._dataSource = dataSource;
			this._filterString = filterString;
			this._showAllResults = showAllResults;
			this._userContext = userContext;
		}

		// Token: 0x060050DF RID: 20703 RVA: 0x000FC130 File Offset: 0x000FA330
		public SearchBoxDataSourceSelectEventArgs(IDataSource dataSource, string filterString, bool showAllResults, IDictionary userContext, SearchContextItemData contextItem)
		{
			this._dataSource = dataSource;
			this._filterString = filterString;
			this._showAllResults = showAllResults;
			this._selectedContextItem = contextItem;
			this._userContext = userContext;
		}

		// Token: 0x17001A7D RID: 6781
		// (get) Token: 0x060050E0 RID: 20704 RVA: 0x000FC15D File Offset: 0x000FA35D
		public IDataSource DataSource
		{
			get
			{
				return this._dataSource;
			}
		}

		// Token: 0x17001A7E RID: 6782
		// (get) Token: 0x060050E1 RID: 20705 RVA: 0x000FC165 File Offset: 0x000FA365
		public string FilterString
		{
			get
			{
				return this._filterString;
			}
		}

		// Token: 0x17001A7F RID: 6783
		// (get) Token: 0x060050E2 RID: 20706 RVA: 0x000FC16D File Offset: 0x000FA36D
		public bool ShowAllResults
		{
			get
			{
				return this._showAllResults;
			}
		}

		// Token: 0x17001A80 RID: 6784
		// (get) Token: 0x060050E3 RID: 20707 RVA: 0x000FC175 File Offset: 0x000FA375
		public SearchContextItemData SelectedContextItem
		{
			get
			{
				return this._selectedContextItem;
			}
		}

		// Token: 0x17001A81 RID: 6785
		// (get) Token: 0x060050E4 RID: 20708 RVA: 0x000FC17D File Offset: 0x000FA37D
		public IDictionary UserContext
		{
			get
			{
				return this._userContext;
			}
		}

		// Token: 0x040013EF RID: 5103
		private IDataSource _dataSource;

		// Token: 0x040013F0 RID: 5104
		private SearchContextItemData _selectedContextItem;

		// Token: 0x040013F1 RID: 5105
		private IDictionary _userContext;

		// Token: 0x040013F2 RID: 5106
		private string _filterString;

		// Token: 0x040013F3 RID: 5107
		private bool _showAllResults;
	}
}
