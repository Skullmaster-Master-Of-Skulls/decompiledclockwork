using System;
using System.Collections;
using System.Configuration;
using System.Net.Cache;

namespace System.Net.Configuration
{
	// Token: 0x0200065E RID: 1630
	public sealed class SettingsSection : ConfigurationSection
	{
		// Token: 0x06003260 RID: 12896 RVA: 0x000D6590 File Offset: 0x000D5590
		internal static void EnsureConfigLoaded()
		{
			try
			{
				AuthenticationManager.EnsureConfigLoaded();
				bool isCachingEnabled = RequestCacheManager.IsCachingEnabled;
				int defaultConnectionLimit = System.Net.ServicePointManager.DefaultConnectionLimit;
				bool expect100Continue = System.Net.ServicePointManager.Expect100Continue;
				ArrayList prefixList = WebRequest.PrefixList;
				IWebProxy internalDefaultWebProxy = WebRequest.InternalDefaultWebProxy;
				NetworkingPerfCounters.Initialize();
			}
			catch
			{
			}
		}

		// Token: 0x06003261 RID: 12897 RVA: 0x000D65DC File Offset: 0x000D55DC
		public SettingsSection()
		{
			this.properties.Add(this.httpWebRequest);
			this.properties.Add(this.ipv6);
			this.properties.Add(this.servicePointManager);
			this.properties.Add(this.socket);
			this.properties.Add(this.webProxyScript);
			this.properties.Add(this.performanceCounters);
		}

		// Token: 0x17000BB2 RID: 2994
		// (get) Token: 0x06003262 RID: 12898 RVA: 0x000D6708 File Offset: 0x000D5708
		[ConfigurationProperty("httpWebRequest")]
		public HttpWebRequestElement HttpWebRequest
		{
			get
			{
				return (HttpWebRequestElement)base[this.httpWebRequest];
			}
		}

		// Token: 0x17000BB3 RID: 2995
		// (get) Token: 0x06003263 RID: 12899 RVA: 0x000D671B File Offset: 0x000D571B
		[ConfigurationProperty("ipv6")]
		public Ipv6Element Ipv6
		{
			get
			{
				return (Ipv6Element)base[this.ipv6];
			}
		}

		// Token: 0x17000BB4 RID: 2996
		// (get) Token: 0x06003264 RID: 12900 RVA: 0x000D672E File Offset: 0x000D572E
		[ConfigurationProperty("servicePointManager")]
		public ServicePointManagerElement ServicePointManager
		{
			get
			{
				return (ServicePointManagerElement)base[this.servicePointManager];
			}
		}

		// Token: 0x17000BB5 RID: 2997
		// (get) Token: 0x06003265 RID: 12901 RVA: 0x000D6741 File Offset: 0x000D5741
		[ConfigurationProperty("socket")]
		public SocketElement Socket
		{
			get
			{
				return (SocketElement)base[this.socket];
			}
		}

		// Token: 0x17000BB6 RID: 2998
		// (get) Token: 0x06003266 RID: 12902 RVA: 0x000D6754 File Offset: 0x000D5754
		[ConfigurationProperty("webProxyScript")]
		public WebProxyScriptElement WebProxyScript
		{
			get
			{
				return (WebProxyScriptElement)base[this.webProxyScript];
			}
		}

		// Token: 0x17000BB7 RID: 2999
		// (get) Token: 0x06003267 RID: 12903 RVA: 0x000D6767 File Offset: 0x000D5767
		[ConfigurationProperty("performanceCounters")]
		public PerformanceCountersElement PerformanceCounters
		{
			get
			{
				return (PerformanceCountersElement)base[this.performanceCounters];
			}
		}

		// Token: 0x17000BB8 RID: 3000
		// (get) Token: 0x06003268 RID: 12904 RVA: 0x000D677A File Offset: 0x000D577A
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return this.properties;
			}
		}

		// Token: 0x04002F38 RID: 12088
		private ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x04002F39 RID: 12089
		private readonly ConfigurationProperty httpWebRequest = new ConfigurationProperty("httpWebRequest", typeof(HttpWebRequestElement), null, ConfigurationPropertyOptions.None);

		// Token: 0x04002F3A RID: 12090
		private readonly ConfigurationProperty ipv6 = new ConfigurationProperty("ipv6", typeof(Ipv6Element), null, ConfigurationPropertyOptions.None);

		// Token: 0x04002F3B RID: 12091
		private readonly ConfigurationProperty servicePointManager = new ConfigurationProperty("servicePointManager", typeof(ServicePointManagerElement), null, ConfigurationPropertyOptions.None);

		// Token: 0x04002F3C RID: 12092
		private readonly ConfigurationProperty socket = new ConfigurationProperty("socket", typeof(SocketElement), null, ConfigurationPropertyOptions.None);

		// Token: 0x04002F3D RID: 12093
		private readonly ConfigurationProperty webProxyScript = new ConfigurationProperty("webProxyScript", typeof(WebProxyScriptElement), null, ConfigurationPropertyOptions.None);

		// Token: 0x04002F3E RID: 12094
		private readonly ConfigurationProperty performanceCounters = new ConfigurationProperty("performanceCounters", typeof(PerformanceCountersElement), null, ConfigurationPropertyOptions.None);
	}
}
