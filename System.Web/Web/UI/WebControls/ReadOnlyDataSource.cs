using System;
using System.Collections;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000624 RID: 1572
	internal sealed class ReadOnlyDataSource : IDataSource
	{
		// Token: 0x06004E08 RID: 19976 RVA: 0x0013C661 File Offset: 0x0013B661
		public ReadOnlyDataSource(object dataSource, string dataMember)
		{
			this._dataSource = dataSource;
			this._dataMember = dataMember;
		}

		// Token: 0x140000F9 RID: 249
		// (add) Token: 0x06004E09 RID: 19977 RVA: 0x0013C677 File Offset: 0x0013B677
		// (remove) Token: 0x06004E0A RID: 19978 RVA: 0x0013C679 File Offset: 0x0013B679
		event EventHandler IDataSource.DataSourceChanged
		{
			add
			{
			}
			remove
			{
			}
		}

		// Token: 0x06004E0B RID: 19979 RVA: 0x0013C67C File Offset: 0x0013B67C
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

		// Token: 0x06004E0C RID: 19980 RVA: 0x0013C6BF File Offset: 0x0013B6BF
		ICollection IDataSource.GetViewNames()
		{
			return ReadOnlyDataSource.ViewNames;
		}

		// Token: 0x04002C76 RID: 11382
		private static string[] ViewNames = new string[0];

		// Token: 0x04002C77 RID: 11383
		private string _dataMember;

		// Token: 0x04002C78 RID: 11384
		private object _dataSource;
	}
}
