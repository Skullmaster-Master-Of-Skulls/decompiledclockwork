using System;
using System.Collections;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02001021 RID: 4129
	internal class EmptyDataSourceView : DataSourceView
	{
		// Token: 0x0600A30D RID: 41741 RVA: 0x00244AB8 File Offset: 0x00242CB8
		protected override IEnumerable ExecuteSelect(DataSourceSelectArguments arguments)
		{
			return new object[0];
		}

		// Token: 0x0600A30E RID: 41742 RVA: 0x00244AC0 File Offset: 0x00242CC0
		public EmptyDataSourceView(IDataSource owner) : base(owner, "")
		{
		}
	}
}
