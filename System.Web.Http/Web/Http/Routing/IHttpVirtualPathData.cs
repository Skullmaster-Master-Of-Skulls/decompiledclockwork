using System;

namespace System.Web.Http.Routing
{
	// Token: 0x02000104 RID: 260
	public interface IHttpVirtualPathData
	{
		// Token: 0x17000212 RID: 530
		// (get) Token: 0x06000658 RID: 1624
		IHttpRoute Route { get; }

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x06000659 RID: 1625
		// (set) Token: 0x0600065A RID: 1626
		string VirtualPath { get; set; }
	}
}
