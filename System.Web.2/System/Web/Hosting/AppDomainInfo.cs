using System;

namespace System.Web.Hosting
{
	// Token: 0x020007DF RID: 2015
	public class AppDomainInfo : IAppDomainInfo
	{
		// Token: 0x0600604F RID: 24655 RVA: 0x0014C7A7 File Offset: 0x0014A9A7
		internal AppDomainInfo(string id, string vpath, string physPath, int siteId, bool isIdle)
		{
			this._id = id;
			this._virtualPath = vpath;
			this._physicalPath = physPath;
			this._siteId = siteId;
			this._isIdle = isIdle;
		}

		// Token: 0x06006050 RID: 24656 RVA: 0x0014C7D4 File Offset: 0x0014A9D4
		public string GetId()
		{
			return this._id;
		}

		// Token: 0x06006051 RID: 24657 RVA: 0x0014C7DC File Offset: 0x0014A9DC
		public string GetVirtualPath()
		{
			return this._virtualPath;
		}

		// Token: 0x06006052 RID: 24658 RVA: 0x0014C7E4 File Offset: 0x0014A9E4
		public string GetPhysicalPath()
		{
			return this._physicalPath;
		}

		// Token: 0x06006053 RID: 24659 RVA: 0x0014C7EC File Offset: 0x0014A9EC
		public int GetSiteId()
		{
			return this._siteId;
		}

		// Token: 0x06006054 RID: 24660 RVA: 0x0014C7F4 File Offset: 0x0014A9F4
		public bool IsIdle()
		{
			return this._isIdle;
		}

		// Token: 0x0400324B RID: 12875
		private string _id;

		// Token: 0x0400324C RID: 12876
		private string _virtualPath;

		// Token: 0x0400324D RID: 12877
		private string _physicalPath;

		// Token: 0x0400324E RID: 12878
		private int _siteId;

		// Token: 0x0400324F RID: 12879
		private bool _isIdle;
	}
}
