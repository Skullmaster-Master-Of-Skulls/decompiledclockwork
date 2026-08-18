using System;
using System.Configuration;

namespace System.Web.Configuration
{
	// Token: 0x020006F8 RID: 1784
	public sealed class HttpCookiesSection : ConfigurationSection
	{
		// Token: 0x0600561B RID: 22043 RVA: 0x0012DFE8 File Offset: 0x0012C1E8
		static HttpCookiesSection()
		{
			HttpCookiesSection._properties = new ConfigurationPropertyCollection();
			HttpCookiesSection._properties.Add(HttpCookiesSection._propHttpOnlyCookies);
			HttpCookiesSection._properties.Add(HttpCookiesSection._propRequireSSL);
			HttpCookiesSection._properties.Add(HttpCookiesSection._propDomain);
			HttpCookiesSection._properties.Add(HttpCookiesSection._propSameSite);
		}

		// Token: 0x170018DA RID: 6362
		// (get) Token: 0x0600561D RID: 22045 RVA: 0x0012E0C0 File Offset: 0x0012C2C0
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return HttpCookiesSection._properties;
			}
		}

		// Token: 0x170018DB RID: 6363
		// (get) Token: 0x0600561E RID: 22046 RVA: 0x0012E0C7 File Offset: 0x0012C2C7
		// (set) Token: 0x0600561F RID: 22047 RVA: 0x0012E0D9 File Offset: 0x0012C2D9
		[ConfigurationProperty("httpOnlyCookies", DefaultValue = false)]
		public bool HttpOnlyCookies
		{
			get
			{
				return (bool)base[HttpCookiesSection._propHttpOnlyCookies];
			}
			set
			{
				base[HttpCookiesSection._propHttpOnlyCookies] = value;
			}
		}

		// Token: 0x170018DC RID: 6364
		// (get) Token: 0x06005620 RID: 22048 RVA: 0x0012E0EC File Offset: 0x0012C2EC
		// (set) Token: 0x06005621 RID: 22049 RVA: 0x0012E0FE File Offset: 0x0012C2FE
		[ConfigurationProperty("requireSSL", DefaultValue = false)]
		public bool RequireSSL
		{
			get
			{
				return (bool)base[HttpCookiesSection._propRequireSSL];
			}
			set
			{
				base[HttpCookiesSection._propRequireSSL] = value;
			}
		}

		// Token: 0x170018DD RID: 6365
		// (get) Token: 0x06005622 RID: 22050 RVA: 0x0012E111 File Offset: 0x0012C311
		// (set) Token: 0x06005623 RID: 22051 RVA: 0x0012E123 File Offset: 0x0012C323
		[ConfigurationProperty("domain", DefaultValue = "")]
		public string Domain
		{
			get
			{
				return (string)base[HttpCookiesSection._propDomain];
			}
			set
			{
				base[HttpCookiesSection._propDomain] = value;
			}
		}

		// Token: 0x170018DE RID: 6366
		// (get) Token: 0x06005624 RID: 22052 RVA: 0x0012E131 File Offset: 0x0012C331
		// (set) Token: 0x06005625 RID: 22053 RVA: 0x0012E143 File Offset: 0x0012C343
		[ConfigurationProperty("sameSite", DefaultValue = (SameSiteMode)(-1))]
		public SameSiteMode SameSite
		{
			get
			{
				return (SameSiteMode)base[HttpCookiesSection._propSameSite];
			}
			set
			{
				base[HttpCookiesSection._propSameSite] = value;
			}
		}

		// Token: 0x04002DC4 RID: 11716
		private static ConfigurationPropertyCollection _properties;

		// Token: 0x04002DC5 RID: 11717
		private static readonly ConfigurationProperty _propHttpOnlyCookies = new ConfigurationProperty("httpOnlyCookies", typeof(bool), false, ConfigurationPropertyOptions.None);

		// Token: 0x04002DC6 RID: 11718
		private static readonly ConfigurationProperty _propRequireSSL = new ConfigurationProperty("requireSSL", typeof(bool), false, ConfigurationPropertyOptions.None);

		// Token: 0x04002DC7 RID: 11719
		private static readonly ConfigurationProperty _propDomain = new ConfigurationProperty("domain", typeof(string), string.Empty, ConfigurationPropertyOptions.None);

		// Token: 0x04002DC8 RID: 11720
		private static readonly ConfigurationProperty _propSameSite = new ConfigurationProperty("sameSite", typeof(SameSiteMode), (SameSiteMode)(-1), new SameSiteConverter(), null, ConfigurationPropertyOptions.None);
	}
}
