using System;
using System.Security.Permissions;

namespace System.Web.Configuration
{
	// Token: 0x0200026B RID: 619
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class WebContext
	{
		// Token: 0x06002096 RID: 8342 RVA: 0x0008E023 File Offset: 0x0008D023
		public WebContext(WebApplicationLevel pathLevel, string site, string applicationPath, string path, string locationSubPath, string appConfigPath)
		{
			this._pathLevel = pathLevel;
			this._site = site;
			this._applicationPath = applicationPath;
			this._path = path;
			this._locationSubPath = locationSubPath;
			this._appConfigPath = appConfigPath;
		}

		// Token: 0x17000700 RID: 1792
		// (get) Token: 0x06002097 RID: 8343 RVA: 0x0008E058 File Offset: 0x0008D058
		public WebApplicationLevel ApplicationLevel
		{
			get
			{
				return this._pathLevel;
			}
		}

		// Token: 0x17000701 RID: 1793
		// (get) Token: 0x06002098 RID: 8344 RVA: 0x0008E060 File Offset: 0x0008D060
		public string Site
		{
			get
			{
				return this._site;
			}
		}

		// Token: 0x17000702 RID: 1794
		// (get) Token: 0x06002099 RID: 8345 RVA: 0x0008E068 File Offset: 0x0008D068
		public string ApplicationPath
		{
			get
			{
				return this._applicationPath;
			}
		}

		// Token: 0x17000703 RID: 1795
		// (get) Token: 0x0600209A RID: 8346 RVA: 0x0008E070 File Offset: 0x0008D070
		public string Path
		{
			get
			{
				return this._path;
			}
		}

		// Token: 0x17000704 RID: 1796
		// (get) Token: 0x0600209B RID: 8347 RVA: 0x0008E078 File Offset: 0x0008D078
		public string LocationSubPath
		{
			get
			{
				return this._locationSubPath;
			}
		}

		// Token: 0x0600209C RID: 8348 RVA: 0x0008E080 File Offset: 0x0008D080
		public override string ToString()
		{
			return this._appConfigPath;
		}

		// Token: 0x04001AA7 RID: 6823
		private WebApplicationLevel _pathLevel;

		// Token: 0x04001AA8 RID: 6824
		private string _site;

		// Token: 0x04001AA9 RID: 6825
		private string _applicationPath;

		// Token: 0x04001AAA RID: 6826
		private string _path;

		// Token: 0x04001AAB RID: 6827
		private string _locationSubPath;

		// Token: 0x04001AAC RID: 6828
		private string _appConfigPath;
	}
}
