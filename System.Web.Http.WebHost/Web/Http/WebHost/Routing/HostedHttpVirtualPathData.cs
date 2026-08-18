using System;
using System.Web.Http.Routing;
using System.Web.Routing;

namespace System.Web.Http.WebHost.Routing
{
	// Token: 0x0200001C RID: 28
	internal class HostedHttpVirtualPathData : IHttpVirtualPathData
	{
		// Token: 0x060000C4 RID: 196 RVA: 0x00004741 File Offset: 0x00002941
		public HostedHttpVirtualPathData(VirtualPathData virtualPath, IHttpRoute httpRoute)
		{
			if (virtualPath == null)
			{
				throw Error.ArgumentNull("route");
			}
			this._virtualPath = virtualPath;
			this.Route = httpRoute;
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000C5 RID: 197 RVA: 0x00004765 File Offset: 0x00002965
		// (set) Token: 0x060000C6 RID: 198 RVA: 0x0000476D File Offset: 0x0000296D
		public IHttpRoute Route { get; private set; }

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x00004776 File Offset: 0x00002976
		// (set) Token: 0x060000C8 RID: 200 RVA: 0x00004783 File Offset: 0x00002983
		public string VirtualPath
		{
			get
			{
				return this._virtualPath.VirtualPath;
			}
			set
			{
				if (value == null)
				{
					throw Error.PropertyNull();
				}
				this._virtualPath.VirtualPath = value;
			}
		}

		// Token: 0x0400002E RID: 46
		private readonly VirtualPathData _virtualPath;
	}
}
