using System;
using System.Runtime.CompilerServices;

namespace System.Web.Routing
{
	// Token: 0x0200014E RID: 334
	[TypeForwardedFrom("System.Web.Routing, Version=3.5.0.0, Culture=Neutral, PublicKeyToken=31bf3856ad364e35")]
	public class RouteTable
	{
		// Token: 0x170005E0 RID: 1504
		// (get) Token: 0x0600136A RID: 4970 RVA: 0x00038840 File Offset: 0x00036A40
		public static RouteCollection Routes
		{
			get
			{
				return RouteTable._instance;
			}
		}

		// Token: 0x040014DE RID: 5342
		private static RouteCollection _instance = new RouteCollection();
	}
}
