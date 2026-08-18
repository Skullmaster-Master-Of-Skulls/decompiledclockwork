using System;

namespace System.Web.Http.Routing
{
	// Token: 0x0200010A RID: 266
	public class HttpVirtualPathData : IHttpVirtualPathData
	{
		// Token: 0x06000676 RID: 1654 RVA: 0x00015CA4 File Offset: 0x00013EA4
		public HttpVirtualPathData(IHttpRoute route, string virtualPath)
		{
			if (route == null)
			{
				throw Error.ArgumentNull("route");
			}
			if (virtualPath == null)
			{
				throw Error.ArgumentNull("virtualPath");
			}
			this.Route = route;
			this.VirtualPath = virtualPath;
		}

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x06000677 RID: 1655 RVA: 0x00015CD6 File Offset: 0x00013ED6
		// (set) Token: 0x06000678 RID: 1656 RVA: 0x00015CDE File Offset: 0x00013EDE
		public IHttpRoute Route { get; private set; }

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x06000679 RID: 1657 RVA: 0x00015CE7 File Offset: 0x00013EE7
		// (set) Token: 0x0600067A RID: 1658 RVA: 0x00015CEF File Offset: 0x00013EEF
		public string VirtualPath
		{
			get
			{
				return this._virtualPath;
			}
			set
			{
				if (value == null)
				{
					throw Error.PropertyNull();
				}
				this._virtualPath = value;
			}
		}

		// Token: 0x040001CE RID: 462
		private string _virtualPath;
	}
}
