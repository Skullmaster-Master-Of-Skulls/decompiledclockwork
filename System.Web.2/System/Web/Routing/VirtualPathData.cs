using System;
using System.Runtime.CompilerServices;

namespace System.Web.Routing
{
	// Token: 0x02000155 RID: 341
	[TypeForwardedFrom("System.Web.Routing, Version=3.5.0.0, Culture=Neutral, PublicKeyToken=31bf3856ad364e35")]
	public class VirtualPathData
	{
		// Token: 0x060013A1 RID: 5025 RVA: 0x00038C9B File Offset: 0x00036E9B
		public VirtualPathData(RouteBase route, string virtualPath)
		{
			this.Route = route;
			this.VirtualPath = virtualPath;
		}

		// Token: 0x170005ED RID: 1517
		// (get) Token: 0x060013A2 RID: 5026 RVA: 0x00038CBC File Offset: 0x00036EBC
		public RouteValueDictionary DataTokens
		{
			get
			{
				return this._dataTokens;
			}
		}

		// Token: 0x170005EE RID: 1518
		// (get) Token: 0x060013A3 RID: 5027 RVA: 0x00038CC4 File Offset: 0x00036EC4
		// (set) Token: 0x060013A4 RID: 5028 RVA: 0x00038CCC File Offset: 0x00036ECC
		public RouteBase Route { get; set; }

		// Token: 0x170005EF RID: 1519
		// (get) Token: 0x060013A5 RID: 5029 RVA: 0x00038CD5 File Offset: 0x00036ED5
		// (set) Token: 0x060013A6 RID: 5030 RVA: 0x00038CE6 File Offset: 0x00036EE6
		public string VirtualPath
		{
			get
			{
				return this._virtualPath ?? string.Empty;
			}
			set
			{
				this._virtualPath = value;
			}
		}

		// Token: 0x040014E4 RID: 5348
		private string _virtualPath;

		// Token: 0x040014E5 RID: 5349
		private RouteValueDictionary _dataTokens = new RouteValueDictionary();
	}
}
