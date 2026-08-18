using System;
using System.Collections;

namespace System.Web.UI
{
	// Token: 0x020002A1 RID: 673
	public interface IDataSource
	{
		// Token: 0x14000031 RID: 49
		// (add) Token: 0x06001F89 RID: 8073
		// (remove) Token: 0x06001F8A RID: 8074
		event EventHandler DataSourceChanged;

		// Token: 0x06001F8B RID: 8075
		DataSourceView GetView(string viewName);

		// Token: 0x06001F8C RID: 8076
		ICollection GetViewNames();
	}
}
