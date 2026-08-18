using System;

namespace System.Web.Http.Routing
{
	// Token: 0x02000079 RID: 121
	public interface IHttpRouteInfoProvider
	{
		// Token: 0x17000176 RID: 374
		// (get) Token: 0x06000328 RID: 808
		string Name { get; }

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x06000329 RID: 809
		string Template { get; }

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x0600032A RID: 810
		int Order { get; }
	}
}
