using System;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02000190 RID: 400
	public class AutoCompleteBoxDataSourceSelectEventArgs : EventArgs
	{
		// Token: 0x06000DAB RID: 3499 RVA: 0x00034072 File Offset: 0x00032272
		public AutoCompleteBoxDataSourceSelectEventArgs(IDataSource dataSource, string filterString)
		{
			this._dataSource = dataSource;
			this._filterString = filterString;
		}

		// Token: 0x170004A0 RID: 1184
		// (get) Token: 0x06000DAC RID: 3500 RVA: 0x00034088 File Offset: 0x00032288
		public IDataSource DataSource
		{
			get
			{
				return this._dataSource;
			}
		}

		// Token: 0x170004A1 RID: 1185
		// (get) Token: 0x06000DAD RID: 3501 RVA: 0x00034090 File Offset: 0x00032290
		public string FilterString
		{
			get
			{
				return this._filterString;
			}
		}

		// Token: 0x040003EE RID: 1006
		private IDataSource _dataSource;

		// Token: 0x040003EF RID: 1007
		private string _filterString;
	}
}
