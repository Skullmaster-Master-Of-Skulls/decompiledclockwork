using System;

namespace System.Web.Configuration
{
	// Token: 0x02000777 RID: 1911
	public sealed class WebContext
	{
		// Token: 0x06005C14 RID: 23572 RVA: 0x0013EF1B File Offset: 0x0013D11B
		public WebContext(WebApplicationLevel pathLevel, string site, string applicationPath, string path, string locationSubPath, string appConfigPath)
		{
			this._pathLevel = pathLevel;
			this._site = site;
			this._applicationPath = applicationPath;
			this._path = path;
			this._locationSubPath = locationSubPath;
			this._appConfigPath = appConfigPath;
		}

		// Token: 0x17001AF0 RID: 6896
		// (get) Token: 0x06005C15 RID: 23573 RVA: 0x0013EF50 File Offset: 0x0013D150
		public WebApplicationLevel ApplicationLevel
		{
			get
			{
				return this._pathLevel;
			}
		}

		// Token: 0x17001AF1 RID: 6897
		// (get) Token: 0x06005C16 RID: 23574 RVA: 0x0013EF58 File Offset: 0x0013D158
		public string Site
		{
			get
			{
				return this._site;
			}
		}

		// Token: 0x17001AF2 RID: 6898
		// (get) Token: 0x06005C17 RID: 23575 RVA: 0x0013EF60 File Offset: 0x0013D160
		public string ApplicationPath
		{
			get
			{
				return this._applicationPath;
			}
		}

		// Token: 0x17001AF3 RID: 6899
		// (get) Token: 0x06005C18 RID: 23576 RVA: 0x0013EF68 File Offset: 0x0013D168
		public string Path
		{
			get
			{
				return this._path;
			}
		}

		// Token: 0x17001AF4 RID: 6900
		// (get) Token: 0x06005C19 RID: 23577 RVA: 0x0013EF70 File Offset: 0x0013D170
		public string LocationSubPath
		{
			get
			{
				return this._locationSubPath;
			}
		}

		// Token: 0x06005C1A RID: 23578 RVA: 0x0013EF78 File Offset: 0x0013D178
		public override string ToString()
		{
			return this._appConfigPath;
		}

		// Token: 0x0400306A RID: 12394
		private WebApplicationLevel _pathLevel;

		// Token: 0x0400306B RID: 12395
		private string _site;

		// Token: 0x0400306C RID: 12396
		private string _applicationPath;

		// Token: 0x0400306D RID: 12397
		private string _path;

		// Token: 0x0400306E RID: 12398
		private string _locationSubPath;

		// Token: 0x0400306F RID: 12399
		private string _appConfigPath;
	}
}
