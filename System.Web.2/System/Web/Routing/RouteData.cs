using System;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace System.Web.Routing
{
	// Token: 0x0200014B RID: 331
	[TypeForwardedFrom("System.Web.Routing, Version=3.5.0.0, Culture=Neutral, PublicKeyToken=31bf3856ad364e35")]
	public class RouteData
	{
		// Token: 0x06001356 RID: 4950 RVA: 0x000380FC File Offset: 0x000362FC
		public RouteData()
		{
		}

		// Token: 0x06001357 RID: 4951 RVA: 0x0003811A File Offset: 0x0003631A
		public RouteData(RouteBase route, IRouteHandler routeHandler)
		{
			this.Route = route;
			this.RouteHandler = routeHandler;
		}

		// Token: 0x170005DC RID: 1500
		// (get) Token: 0x06001358 RID: 4952 RVA: 0x00038146 File Offset: 0x00036346
		public RouteValueDictionary DataTokens
		{
			get
			{
				return this._dataTokens;
			}
		}

		// Token: 0x170005DD RID: 1501
		// (get) Token: 0x06001359 RID: 4953 RVA: 0x0003814E File Offset: 0x0003634E
		// (set) Token: 0x0600135A RID: 4954 RVA: 0x00038156 File Offset: 0x00036356
		public RouteBase Route { get; set; }

		// Token: 0x170005DE RID: 1502
		// (get) Token: 0x0600135B RID: 4955 RVA: 0x0003815F File Offset: 0x0003635F
		// (set) Token: 0x0600135C RID: 4956 RVA: 0x00038167 File Offset: 0x00036367
		public IRouteHandler RouteHandler
		{
			get
			{
				return this._routeHandler;
			}
			set
			{
				this._routeHandler = value;
			}
		}

		// Token: 0x170005DF RID: 1503
		// (get) Token: 0x0600135D RID: 4957 RVA: 0x00038170 File Offset: 0x00036370
		public RouteValueDictionary Values
		{
			get
			{
				return this._values;
			}
		}

		// Token: 0x0600135E RID: 4958 RVA: 0x00038178 File Offset: 0x00036378
		public string GetRequiredString(string valueName)
		{
			object obj;
			if (this.Values.TryGetValue(valueName, out obj))
			{
				string text = obj as string;
				if (!string.IsNullOrEmpty(text))
				{
					return text;
				}
			}
			throw new InvalidOperationException(string.Format(CultureInfo.CurrentUICulture, SR.GetString("RouteData_RequiredValue"), new object[]
			{
				valueName
			}));
		}

		// Token: 0x040014D7 RID: 5335
		private IRouteHandler _routeHandler;

		// Token: 0x040014D8 RID: 5336
		private RouteValueDictionary _values = new RouteValueDictionary();

		// Token: 0x040014D9 RID: 5337
		private RouteValueDictionary _dataTokens = new RouteValueDictionary();
	}
}
