using System;

namespace System.Configuration
{
	// Token: 0x0200005D RID: 93
	public sealed class ExeContext
	{
		// Token: 0x0600039B RID: 923 RVA: 0x0001389E File Offset: 0x00011A9E
		internal ExeContext(ConfigurationUserLevel userContext, string exePath)
		{
			this._userContext = userContext;
			this._exePath = exePath;
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x0600039C RID: 924 RVA: 0x000138B4 File Offset: 0x00011AB4
		public ConfigurationUserLevel UserLevel
		{
			get
			{
				return this._userContext;
			}
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x0600039D RID: 925 RVA: 0x000138BC File Offset: 0x00011ABC
		public string ExePath
		{
			get
			{
				return this._exePath;
			}
		}

		// Token: 0x04000267 RID: 615
		private ConfigurationUserLevel _userContext;

		// Token: 0x04000268 RID: 616
		private string _exePath;
	}
}
