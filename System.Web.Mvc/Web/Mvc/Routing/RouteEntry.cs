using System;
using System.Web.Routing;

namespace System.Web.Mvc.Routing
{
	// Token: 0x0200000C RID: 12
	public class RouteEntry
	{
		// Token: 0x06000056 RID: 86 RVA: 0x00002F2E File Offset: 0x0000112E
		public RouteEntry(string name, Route route)
		{
			if (route == null)
			{
				throw new ArgumentNullException("route");
			}
			this._name = name;
			this._route = route;
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000057 RID: 87 RVA: 0x00002F52 File Offset: 0x00001152
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000058 RID: 88 RVA: 0x00002F5A File Offset: 0x0000115A
		public Route Route
		{
			get
			{
				return this._route;
			}
		}

		// Token: 0x04000017 RID: 23
		private readonly string _name;

		// Token: 0x04000018 RID: 24
		private readonly Route _route;
	}
}
