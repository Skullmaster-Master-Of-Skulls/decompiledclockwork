using System;
using System.Collections;

namespace System.Web.UI.WebControls
{
	// Token: 0x020004AC RID: 1196
	internal sealed class ReadOnlyDataSourceView : DataSourceView
	{
		// Token: 0x06003BE2 RID: 15330 RVA: 0x000C2853 File Offset: 0x000C0A53
		public ReadOnlyDataSourceView(ReadOnlyDataSource owner, string name, IEnumerable dataSource) : base(owner, name)
		{
			this._dataSource = dataSource;
		}

		// Token: 0x06003BE3 RID: 15331 RVA: 0x000C2864 File Offset: 0x000C0A64
		protected internal override IEnumerable ExecuteSelect(DataSourceSelectArguments arguments)
		{
			arguments.RaiseUnsupportedCapabilitiesError(this);
			return this._dataSource;
		}

		// Token: 0x04002352 RID: 9042
		private IEnumerable _dataSource;
	}
}
