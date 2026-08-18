using System;

namespace System.Web.UI
{
	// Token: 0x020002A6 RID: 678
	public interface IHierarchicalDataSource
	{
		// Token: 0x14000032 RID: 50
		// (add) Token: 0x06001F98 RID: 8088
		// (remove) Token: 0x06001F99 RID: 8089
		event EventHandler DataSourceChanged;

		// Token: 0x06001F9A RID: 8090
		HierarchicalDataSourceView GetHierarchicalView(string viewPath);
	}
}
