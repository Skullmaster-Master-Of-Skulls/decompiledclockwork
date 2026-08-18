using System;
using System.Security.Permissions;

namespace System.Web.Hosting
{
	// Token: 0x020002B9 RID: 697
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class AppDomainInfo : IAppDomainInfo
	{
		// Token: 0x0600240F RID: 9231 RVA: 0x0009A486 File Offset: 0x00099486
		internal AppDomainInfo(string id, string vpath, string physPath, int siteId, bool isIdle)
		{
			this._id = id;
			this._virtualPath = vpath;
			this._physicalPath = physPath;
			this._siteId = siteId;
			this._isIdle = isIdle;
		}

		// Token: 0x06002410 RID: 9232 RVA: 0x0009A4B3 File Offset: 0x000994B3
		public string GetId()
		{
			return this._id;
		}

		// Token: 0x06002411 RID: 9233 RVA: 0x0009A4BB File Offset: 0x000994BB
		public string GetVirtualPath()
		{
			return this._virtualPath;
		}

		// Token: 0x06002412 RID: 9234 RVA: 0x0009A4C3 File Offset: 0x000994C3
		public string GetPhysicalPath()
		{
			return this._physicalPath;
		}

		// Token: 0x06002413 RID: 9235 RVA: 0x0009A4CB File Offset: 0x000994CB
		public int GetSiteId()
		{
			return this._siteId;
		}

		// Token: 0x06002414 RID: 9236 RVA: 0x0009A4D3 File Offset: 0x000994D3
		public bool IsIdle()
		{
			return this._isIdle;
		}

		// Token: 0x04001C2F RID: 7215
		private string _id;

		// Token: 0x04001C30 RID: 7216
		private string _virtualPath;

		// Token: 0x04001C31 RID: 7217
		private string _physicalPath;

		// Token: 0x04001C32 RID: 7218
		private int _siteId;

		// Token: 0x04001C33 RID: 7219
		private bool _isIdle;
	}
}
