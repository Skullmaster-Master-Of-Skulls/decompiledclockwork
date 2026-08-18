using System;
using System.Collections.Generic;

namespace System.Web.Http.Routing
{
	// Token: 0x02000107 RID: 263
	public class HttpRouteData : IHttpRouteData
	{
		// Token: 0x0600066F RID: 1647 RVA: 0x00015B3F File Offset: 0x00013D3F
		public HttpRouteData(IHttpRoute route) : this(route, new HttpRouteValueDictionary())
		{
		}

		// Token: 0x06000670 RID: 1648 RVA: 0x00015B4D File Offset: 0x00013D4D
		public HttpRouteData(IHttpRoute route, HttpRouteValueDictionary values)
		{
			if (route == null)
			{
				throw Error.ArgumentNull("route");
			}
			if (values == null)
			{
				throw Error.ArgumentNull("values");
			}
			this._route = route;
			this._values = values;
		}

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x06000671 RID: 1649 RVA: 0x00015B7F File Offset: 0x00013D7F
		public IHttpRoute Route
		{
			get
			{
				return this._route;
			}
		}

		// Token: 0x17000217 RID: 535
		// (get) Token: 0x06000672 RID: 1650 RVA: 0x00015B87 File Offset: 0x00013D87
		public IDictionary<string, object> Values
		{
			get
			{
				return this._values;
			}
		}

		// Token: 0x040001C9 RID: 457
		private IHttpRoute _route;

		// Token: 0x040001CA RID: 458
		private IDictionary<string, object> _values;
	}
}
