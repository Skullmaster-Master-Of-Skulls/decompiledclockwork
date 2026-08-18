using System;
using System.Collections;

namespace System.Web.UI.WebControls
{
	// Token: 0x020004AB RID: 1195
	internal sealed class ReadOnlyDataSource : IDataSource
	{
		// Token: 0x06003BDC RID: 15324 RVA: 0x000C27E4 File Offset: 0x000C09E4
		public ReadOnlyDataSource(object dataSource, string dataMember)
		{
			this._dataSource = dataSource;
			this._dataMember = dataMember;
		}

		// Token: 0x140000E1 RID: 225
		// (add) Token: 0x06003BDD RID: 15325 RVA: 0x00006164 File Offset: 0x00004364
		// (remove) Token: 0x06003BDE RID: 15326 RVA: 0x00006164 File Offset: 0x00004364
		event EventHandler IDataSource.DataSourceChanged
		{
			add
			{
			}
			remove
			{
			}
		}

		// Token: 0x06003BDF RID: 15327 RVA: 0x000C27FC File Offset: 0x000C09FC
		DataSourceView IDataSource.GetView(string viewName)
		{
			IDataSource dataSource = this._dataSource as IDataSource;
			if (dataSource != null)
			{
				return dataSource.GetView(viewName);
			}
			IEnumerable resolvedDataSource = DataSourceHelper.GetResolvedDataSource(this._dataSource, this._dataMember);
			return new ReadOnlyDataSourceView(this, this._dataMember, resolvedDataSource);
		}

		// Token: 0x06003BE0 RID: 15328 RVA: 0x000C283F File Offset: 0x000C0A3F
		ICollection IDataSource.GetViewNames()
		{
			return ReadOnlyDataSource.ViewNames;
		}

		// Token: 0x0400234F RID: 9039
		private static string[] ViewNames = new string[0];

		// Token: 0x04002350 RID: 9040
		private string _dataMember;

		// Token: 0x04002351 RID: 9041
		private object _dataSource;
	}
}
