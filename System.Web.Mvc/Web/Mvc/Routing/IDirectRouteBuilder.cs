using System;
using System.Collections.Generic;
using System.Web.Routing;

namespace System.Web.Mvc.Routing
{
	// Token: 0x02000007 RID: 7
	public interface IDirectRouteBuilder
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000020 RID: 32
		// (set) Token: 0x06000021 RID: 33
		string Name { get; set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000022 RID: 34
		// (set) Token: 0x06000023 RID: 35
		string Template { get; set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000024 RID: 36
		// (set) Token: 0x06000025 RID: 37
		RouteValueDictionary Defaults { get; set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000026 RID: 38
		// (set) Token: 0x06000027 RID: 39
		RouteValueDictionary Constraints { get; set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000028 RID: 40
		// (set) Token: 0x06000029 RID: 41
		RouteValueDictionary DataTokens { get; set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600002A RID: 42
		// (set) Token: 0x0600002B RID: 43
		int Order { get; set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600002C RID: 44
		// (set) Token: 0x0600002D RID: 45
		decimal Precedence { get; set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600002E RID: 46
		IReadOnlyCollection<ActionDescriptor> Actions { get; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600002F RID: 47
		bool TargetIsAction { get; }

		// Token: 0x06000030 RID: 48
		RouteEntry Build();
	}
}
