using System;
using System.Collections;

namespace System.Web.UI.Design
{
	// Token: 0x0200004D RID: 77
	public interface IDataSourceProvider
	{
		// Token: 0x06000299 RID: 665
		object GetSelectedDataSource();

		// Token: 0x0600029A RID: 666
		IEnumerable GetResolvedSelectedDataSource();
	}
}
