using System;

namespace System.Web.Http.Routing
{
	// Token: 0x02000012 RID: 18
	public class RouteEntry
	{
		// Token: 0x0600007E RID: 126 RVA: 0x000035C5 File Offset: 0x000017C5
		public RouteEntry(string name, IHttpRoute route)
		{
			if (route == null)
			{
				throw new ArgumentNullException("route");
			}
			this._name = name;
			this._route = route;
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600007F RID: 127 RVA: 0x000035E9 File Offset: 0x000017E9
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000080 RID: 128 RVA: 0x000035F1 File Offset: 0x000017F1
		public IHttpRoute Route
		{
			get
			{
				return this._route;
			}
		}

		// Token: 0x0400001E RID: 30
		private readonly string _name;

		// Token: 0x0400001F RID: 31
		private readonly IHttpRoute _route;
	}
}
