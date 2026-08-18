using System;
using System.Collections.Generic;

namespace System.Web.Http.Routing
{
	// Token: 0x02000081 RID: 129
	public interface IHttpRouteData
	{
		// Token: 0x1700018B RID: 395
		// (get) Token: 0x06000360 RID: 864
		IHttpRoute Route { get; }

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x06000361 RID: 865
		IDictionary<string, object> Values { get; }
	}
}
