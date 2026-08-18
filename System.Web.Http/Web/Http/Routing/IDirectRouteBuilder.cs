using System;
using System.Collections.Generic;
using System.Web.Http.Controllers;

namespace System.Web.Http.Routing
{
	// Token: 0x0200000C RID: 12
	public interface IDirectRouteBuilder
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000049 RID: 73
		// (set) Token: 0x0600004A RID: 74
		string Name { get; set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600004B RID: 75
		// (set) Token: 0x0600004C RID: 76
		string Template { get; set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600004D RID: 77
		// (set) Token: 0x0600004E RID: 78
		IDictionary<string, object> Defaults { get; set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600004F RID: 79
		// (set) Token: 0x06000050 RID: 80
		IDictionary<string, object> Constraints { get; set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000051 RID: 81
		// (set) Token: 0x06000052 RID: 82
		IDictionary<string, object> DataTokens { get; set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000053 RID: 83
		// (set) Token: 0x06000054 RID: 84
		int Order { get; set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000055 RID: 85
		// (set) Token: 0x06000056 RID: 86
		decimal Precedence { get; set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000057 RID: 87
		IReadOnlyCollection<HttpActionDescriptor> Actions { get; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000058 RID: 88
		bool TargetIsAction { get; }

		// Token: 0x06000059 RID: 89
		RouteEntry Build();
	}
}
