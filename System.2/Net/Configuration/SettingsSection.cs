using System;
using System.Configuration;
using System.Net.Cache;

namespace System.Net.Configuration
{
	// Token: 0x0200033F RID: 831
	public sealed class SettingsSection : ConfigurationSection
	{
		// Token: 0x06001DB6 RID: 7606 RVA: 0x0008C87C File Offset: 0x0008AA7C
		internal static void EnsureConfigLoaded()
		{
			try
			{
				AuthenticationManager.EnsureConfigLoaded();
				object obj = RequestCacheManager.IsCachingEnabled;
				obj = System.Net.ServicePointManager.DefaultConnectionLimit;
				obj = System.Net.ServicePointManager.Expect100Continue;
				obj = WebRequest.PrefixList;
				obj = WebRequest.InternalDefaultWebProxy;
			}
			catch
			{
			}
		}

		// Token: 0x06001DB7 RID: 7607 RVA: 0x0008C8D0 File Offset: 0x0008AAD0
		public SettingsSection()
		{
			this.properties.Add(this.httpWebRequest);
			this.properties.Add(this.ipv6);
			this.properties.Add(this.servicePointManager);
			this.properties.Add(this.socket);
			this.properties.Add(this.webProxyScript);
			this.properties.Add(this.performanceCounters);
			this.properties.Add(this.httpListener);
			this.properties.Add(this.webUtility);
			this.properties.Add(this.windowsAuthentication);
		}

		// Token: 0x17000785 RID: 1925
		// (get) Token: 0x06001DB8 RID: 7608 RVA: 0x0008CA83 File Offset: 0x0008AC83
		[ConfigurationProperty("httpWebRequest")]
		public HttpWebRequestElement HttpWebRequest
		{
			get
			{
				return (HttpWebRequestElement)base[this.httpWebRequest];
			}
		}

		// Token: 0x17000786 RID: 1926
		// (get) Token: 0x06001DB9 RID: 7609 RVA: 0x0008CA96 File Offset: 0x0008AC96
		[ConfigurationProperty("ipv6")]
		public Ipv6Element Ipv6
		{
			get
			{
				return (Ipv6Element)base[this.ipv6];
			}
		}

		// Token: 0x17000787 RID: 1927
		// (get) Token: 0x06001DBA RID: 7610 RVA: 0x0008CAA9 File Offset: 0x0008ACA9
		[ConfigurationProperty("servicePointManager")]
		public ServicePointManagerElement ServicePointManager
		{
			get
			{
				return (ServicePointManagerElement)base[this.servicePointManager];
			}
		}

		// Token: 0x17000788 RID: 1928
		// (get) Token: 0x06001DBB RID: 7611 RVA: 0x0008CABC File Offset: 0x0008ACBC
		[ConfigurationProperty("socket")]
		public SocketElement Socket
		{
			get
			{
				return (SocketElement)base[this.socket];
			}
		}

		// Token: 0x17000789 RID: 1929
		// (get) Token: 0x06001DBC RID: 7612 RVA: 0x0008CACF File Offset: 0x0008ACCF
		[ConfigurationProperty("webProxyScript")]
		public WebProxyScriptElement WebProxyScript
		{
			get
			{
				return (WebProxyScriptElement)base[this.webProxyScript];
			}
		}

		// Token: 0x1700078A RID: 1930
		// (get) Token: 0x06001DBD RID: 7613 RVA: 0x0008CAE2 File Offset: 0x0008ACE2
		[ConfigurationProperty("performanceCounters")]
		public PerformanceCountersElement PerformanceCounters
		{
			get
			{
				return (PerformanceCountersElement)base[this.performanceCounters];
			}
		}

		// Token: 0x1700078B RID: 1931
		// (get) Token: 0x06001DBE RID: 7614 RVA: 0x0008CAF5 File Offset: 0x0008ACF5
		[ConfigurationProperty("httpListener")]
		public HttpListenerElement HttpListener
		{
			get
			{
				return (HttpListenerElement)base[this.httpListener];
			}
		}

		// Token: 0x1700078C RID: 1932
		// (get) Token: 0x06001DBF RID: 7615 RVA: 0x0008CB08 File Offset: 0x0008AD08
		[ConfigurationProperty("webUtility")]
		public WebUtilityElement WebUtility
		{
			get
			{
				return (WebUtilityElement)base[this.webUtility];
			}
		}

		// Token: 0x1700078D RID: 1933
		// (get) Token: 0x06001DC0 RID: 7616 RVA: 0x0008CB1B File Offset: 0x0008AD1B
		[ConfigurationProperty("windowsAuthentication")]
		public WindowsAuthenticationElement WindowsAuthentication
		{
			get
			{
				return (WindowsAuthenticationElement)base[this.windowsAuthentication];
			}
		}

		// Token: 0x1700078E RID: 1934
		// (get) Token: 0x06001DC1 RID: 7617 RVA: 0x0008CB2E File Offset: 0x0008AD2E
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return this.properties;
			}
		}

		// Token: 0x04001C6D RID: 7277
		private ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x04001C6E RID: 7278
		private readonly ConfigurationProperty httpWebRequest = new ConfigurationProperty("httpWebRequest", typeof(HttpWebRequestElement), null, ConfigurationPropertyOptions.None);

		// Token: 0x04001C6F RID: 7279
		private readonly ConfigurationProperty ipv6 = new ConfigurationProperty("ipv6", typeof(Ipv6Element), null, ConfigurationPropertyOptions.None);

		// Token: 0x04001C70 RID: 7280
		private readonly ConfigurationProperty servicePointManager = new ConfigurationProperty("servicePointManager", typeof(ServicePointManagerElement), null, ConfigurationPropertyOptions.None);

		// Token: 0x04001C71 RID: 7281
		private readonly ConfigurationProperty socket = new ConfigurationProperty("socket", typeof(SocketElement), null, ConfigurationPropertyOptions.None);

		// Token: 0x04001C72 RID: 7282
		private readonly ConfigurationProperty webProxyScript = new ConfigurationProperty("webProxyScript", typeof(WebProxyScriptElement), null, ConfigurationPropertyOptions.None);

		// Token: 0x04001C73 RID: 7283
		private readonly ConfigurationProperty performanceCounters = new ConfigurationProperty("performanceCounters", typeof(PerformanceCountersElement), null, ConfigurationPropertyOptions.None);

		// Token: 0x04001C74 RID: 7284
		private readonly ConfigurationProperty httpListener = new ConfigurationProperty("httpListener", typeof(HttpListenerElement), null, ConfigurationPropertyOptions.None);

		// Token: 0x04001C75 RID: 7285
		private readonly ConfigurationProperty webUtility = new ConfigurationProperty("webUtility", typeof(WebUtilityElement), null, ConfigurationPropertyOptions.None);

		// Token: 0x04001C76 RID: 7286
		private readonly ConfigurationProperty windowsAuthentication = new ConfigurationProperty("windowsAuthentication", typeof(WindowsAuthenticationElement), null, ConfigurationPropertyOptions.None);
	}
}
