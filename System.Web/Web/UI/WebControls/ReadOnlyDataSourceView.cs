using System;
using System.Collections;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000625 RID: 1573
	internal sealed class ReadOnlyDataSourceView : DataSourceView
	{
		// Token: 0x06004E0E RID: 19982 RVA: 0x0013C6D3 File Offset: 0x0013B6D3
		public ReadOnlyDataSourceView(ReadOnlyDataSource owner, string name, IEnumerable dataSource) : base(owner, name)
		{
			this._dataSource = dataSource;
		}

		// Token: 0x06004E0F RID: 19983 RVA: 0x0013C6E4 File Offset: 0x0013B6E4
		protected internal override IEnumerable ExecuteSelect(DataSourceSelectArguments arguments)
		{
			arguments.RaiseUnsupportedCapabilitiesError(this);
			return this._dataSource;
		}

		// Token: 0x04002C79 RID: 11385
		private IEnumerable _dataSource;
	}
}
