using System;
using System.Globalization;

namespace System.Net.Configuration
{
	// Token: 0x0200032A RID: 810
	internal static class ConfigurationStrings
	{
		// Token: 0x06001D12 RID: 7442 RVA: 0x0008AD95 File Offset: 0x00088F95
		private static string GetSectionPath(string sectionName)
		{
			return string.Format(CultureInfo.InvariantCulture, "{0}/{1}", new object[]
			{
				"system.net",
				sectionName
			});
		}

		// Token: 0x06001D13 RID: 7443 RVA: 0x0008ADB8 File Offset: 0x00088FB8
		private static string GetSectionPath(string sectionName, string subSectionName)
		{
			return string.Format(CultureInfo.InvariantCulture, "{0}/{1}/{2}", new object[]
			{
				"system.net",
				sectionName,
				subSectionName
			});
		}

		// Token: 0x1700072E RID: 1838
		// (get) Token: 0x06001D14 RID: 7444 RVA: 0x0008ADDF File Offset: 0x00088FDF
		internal static string AuthenticationModulesSectionPath
		{
			get
			{
				return ConfigurationStrings.GetSectionPath("authenticationModules");
			}
		}

		// Token: 0x1700072F RID: 1839
		// (get) Token: 0x06001D15 RID: 7445 RVA: 0x0008ADEB File Offset: 0x00088FEB
		internal static string ConnectionManagementSectionPath
		{
			get
			{
				return ConfigurationStrings.GetSectionPath("connectionManagement");
			}
		}

		// Token: 0x17000730 RID: 1840
		// (get) Token: 0x06001D16 RID: 7446 RVA: 0x0008ADF7 File Offset: 0x00088FF7
		internal static string DefaultProxySectionPath
		{
			get
			{
				return ConfigurationStrings.GetSectionPath("defaultProxy");
			}
		}

		// Token: 0x17000731 RID: 1841
		// (get) Token: 0x06001D17 RID: 7447 RVA: 0x0008AE03 File Offset: 0x00089003
		internal static string SmtpSectionPath
		{
			get
			{
				return ConfigurationStrings.GetSectionPath("mailSettings", "smtp");
			}
		}

		// Token: 0x17000732 RID: 1842
		// (get) Token: 0x06001D18 RID: 7448 RVA: 0x0008AE14 File Offset: 0x00089014
		internal static string RequestCachingSectionPath
		{
			get
			{
				return ConfigurationStrings.GetSectionPath("requestCaching");
			}
		}

		// Token: 0x17000733 RID: 1843
		// (get) Token: 0x06001D19 RID: 7449 RVA: 0x0008AE20 File Offset: 0x00089020
		internal static string SettingsSectionPath
		{
			get
			{
				return ConfigurationStrings.GetSectionPath("settings");
			}
		}

		// Token: 0x17000734 RID: 1844
		// (get) Token: 0x06001D1A RID: 7450 RVA: 0x0008AE2C File Offset: 0x0008902C
		internal static string WebRequestModulesSectionPath
		{
			get
			{
				return ConfigurationStrings.GetSectionPath("webRequestModules");
			}
		}

		// Token: 0x04001BCE RID: 7118
		internal const string Address = "address";

		// Token: 0x04001BCF RID: 7119
		internal const string AutoConfigUrlRetryInterval = "autoConfigUrlRetryInterval";

		// Token: 0x04001BD0 RID: 7120
		internal const string AutoDetect = "autoDetect";

		// Token: 0x04001BD1 RID: 7121
		internal const string AlwaysUseCompletionPortsForAccept = "alwaysUseCompletionPortsForAccept";

		// Token: 0x04001BD2 RID: 7122
		internal const string AlwaysUseCompletionPortsForConnect = "alwaysUseCompletionPortsForConnect";

		// Token: 0x04001BD3 RID: 7123
		internal const string AuthenticationModulesSectionName = "authenticationModules";

		// Token: 0x04001BD4 RID: 7124
		internal const string BypassList = "bypasslist";

		// Token: 0x04001BD5 RID: 7125
		internal const string BypassOnLocal = "bypassonlocal";

		// Token: 0x04001BD6 RID: 7126
		internal const string CheckCertificateName = "checkCertificateName";

		// Token: 0x04001BD7 RID: 7127
		internal const string CheckCertificateRevocationList = "checkCertificateRevocationList";

		// Token: 0x04001BD8 RID: 7128
		internal const string ClientDomain = "clientDomain";

		// Token: 0x04001BD9 RID: 7129
		internal const string ConnectionManagementSectionName = "connectionManagement";

		// Token: 0x04001BDA RID: 7130
		internal const string DefaultCredentials = "defaultCredentials";

		// Token: 0x04001BDB RID: 7131
		internal const string DefaultCredentialsHandleCacheSize = "defaultCredentialsHandleCacheSize";

		// Token: 0x04001BDC RID: 7132
		internal const string DefaultHttpCachePolicy = "defaultHttpCachePolicy";

		// Token: 0x04001BDD RID: 7133
		internal const string DefaultFtpCachePolicy = "defaultFtpCachePolicy";

		// Token: 0x04001BDE RID: 7134
		internal const string DefaultPolicyLevel = "defaultPolicyLevel";

		// Token: 0x04001BDF RID: 7135
		internal const string DefaultProxySectionName = "defaultProxy";

		// Token: 0x04001BE0 RID: 7136
		internal const string DeliveryMethod = "deliveryMethod";

		// Token: 0x04001BE1 RID: 7137
		internal const string DeliveryFormat = "deliveryFormat";

		// Token: 0x04001BE2 RID: 7138
		internal const string DisableAllCaching = "disableAllCaching";

		// Token: 0x04001BE3 RID: 7139
		internal const string DnsRefreshTimeout = "dnsRefreshTimeout";

		// Token: 0x04001BE4 RID: 7140
		internal const string DownloadTimeout = "downloadTimeout";

		// Token: 0x04001BE5 RID: 7141
		internal const string Enabled = "enabled";

		// Token: 0x04001BE6 RID: 7142
		internal const string EnableDnsRoundRobin = "enableDnsRoundRobin";

		// Token: 0x04001BE7 RID: 7143
		internal const string EnableSsl = "enableSsl";

		// Token: 0x04001BE8 RID: 7144
		internal const string EncryptionPolicy = "encryptionPolicy";

		// Token: 0x04001BE9 RID: 7145
		internal const string Expect100Continue = "expect100Continue";

		// Token: 0x04001BEA RID: 7146
		internal const string File = "file:";

		// Token: 0x04001BEB RID: 7147
		internal const string From = "from";

		// Token: 0x04001BEC RID: 7148
		internal const string Ftp = "ftp:";

		// Token: 0x04001BED RID: 7149
		internal const string Host = "host";

		// Token: 0x04001BEE RID: 7150
		internal const string HttpWebRequest = "httpWebRequest";

		// Token: 0x04001BEF RID: 7151
		internal const string HttpListener = "httpListener";

		// Token: 0x04001BF0 RID: 7152
		internal const string Http = "http:";

		// Token: 0x04001BF1 RID: 7153
		internal const string Https = "https:";

		// Token: 0x04001BF2 RID: 7154
		internal const string Ipv6 = "ipv6";

		// Token: 0x04001BF3 RID: 7155
		internal const string IsPrivateCache = "isPrivateCache";

		// Token: 0x04001BF4 RID: 7156
		internal const string IPProtectionLevel = "ipProtectionLevel";

		// Token: 0x04001BF5 RID: 7157
		internal const string MailSettingsSectionName = "mailSettings";

		// Token: 0x04001BF6 RID: 7158
		internal const string MaxConnection = "maxconnection";

		// Token: 0x04001BF7 RID: 7159
		internal const string MaximumAge = "maximumAge";

		// Token: 0x04001BF8 RID: 7160
		internal const string MaximumStale = "maximumStale";

		// Token: 0x04001BF9 RID: 7161
		internal const string MaximumResponseHeadersLength = "maximumResponseHeadersLength";

		// Token: 0x04001BFA RID: 7162
		internal const string MaximumErrorResponseLength = "maximumErrorResponseLength";

		// Token: 0x04001BFB RID: 7163
		internal const string MinimumFresh = "minimumFresh";

		// Token: 0x04001BFC RID: 7164
		internal const string Module = "module";

		// Token: 0x04001BFD RID: 7165
		internal const string Name = "name";

		// Token: 0x04001BFE RID: 7166
		internal const string Network = "network";

		// Token: 0x04001BFF RID: 7167
		internal const string Password = "password";

		// Token: 0x04001C00 RID: 7168
		internal const string PerformanceCounters = "performanceCounters";

		// Token: 0x04001C01 RID: 7169
		internal const string PickupDirectoryFromIis = "pickupDirectoryFromIis";

		// Token: 0x04001C02 RID: 7170
		internal const string PickupDirectoryLocation = "pickupDirectoryLocation";

		// Token: 0x04001C03 RID: 7171
		internal const string PolicyLevel = "policyLevel";

		// Token: 0x04001C04 RID: 7172
		internal const string Port = "port";

		// Token: 0x04001C05 RID: 7173
		internal const string Prefix = "prefix";

		// Token: 0x04001C06 RID: 7174
		internal const string Proxy = "proxy";

		// Token: 0x04001C07 RID: 7175
		internal const string ProxyAddress = "proxyaddress";

		// Token: 0x04001C08 RID: 7176
		internal const string RequestCachingSectionName = "requestCaching";

		// Token: 0x04001C09 RID: 7177
		internal const string ScriptLocation = "scriptLocation";

		// Token: 0x04001C0A RID: 7178
		internal const string SectionGroupName = "system.net";

		// Token: 0x04001C0B RID: 7179
		internal const string ServicePointManager = "servicePointManager";

		// Token: 0x04001C0C RID: 7180
		internal const string SettingsSectionName = "settings";

		// Token: 0x04001C0D RID: 7181
		internal const string SmtpSectionName = "smtp";

		// Token: 0x04001C0E RID: 7182
		internal const string Socket = "socket";

		// Token: 0x04001C0F RID: 7183
		internal const string SpecifiedPickupDirectory = "specifiedPickupDirectory";

		// Token: 0x04001C10 RID: 7184
		internal const string TargetName = "targetName";

		// Token: 0x04001C11 RID: 7185
		internal const string Type = "type";

		// Token: 0x04001C12 RID: 7186
		internal const string UnicodeDecodingConformance = "unicodeDecodingConformance";

		// Token: 0x04001C13 RID: 7187
		internal const string UnicodeEncodingConformance = "unicodeEncodingConformance";

		// Token: 0x04001C14 RID: 7188
		internal const string UnspecifiedMaximumAge = "unspecifiedMaximumAge";

		// Token: 0x04001C15 RID: 7189
		internal const string UseDefaultCredentials = "useDefaultCredentials";

		// Token: 0x04001C16 RID: 7190
		internal const string UseNagleAlgorithm = "useNagleAlgorithm";

		// Token: 0x04001C17 RID: 7191
		internal const string UseSystemDefault = "usesystemdefault";

		// Token: 0x04001C18 RID: 7192
		internal const string UseUnsafeHeaderParsing = "useUnsafeHeaderParsing";

		// Token: 0x04001C19 RID: 7193
		internal const string UserName = "userName";

		// Token: 0x04001C1A RID: 7194
		internal const string WebProxyScript = "webProxyScript";

		// Token: 0x04001C1B RID: 7195
		internal const string WebRequestModulesSectionName = "webRequestModules";

		// Token: 0x04001C1C RID: 7196
		internal const string WebUtility = "webUtility";

		// Token: 0x04001C1D RID: 7197
		internal const string WindowsAuthentication = "windowsAuthentication";

		// Token: 0x04001C1E RID: 7198
		internal const string maximumUnauthorizedUploadLength = "maximumUnauthorizedUploadLength";

		// Token: 0x04001C1F RID: 7199
		internal const string UnescapeRequestUrl = "unescapeRequestUrl";

		// Token: 0x04001C20 RID: 7200
		internal const string Timeouts = "timeouts";

		// Token: 0x04001C21 RID: 7201
		internal const string EntityBody = "entityBody";

		// Token: 0x04001C22 RID: 7202
		internal const string DrainEntityBody = "drainEntityBody";

		// Token: 0x04001C23 RID: 7203
		internal const string RequestQueue = "requestQueue";

		// Token: 0x04001C24 RID: 7204
		internal const string IdleConnection = "idleConnection";

		// Token: 0x04001C25 RID: 7205
		internal const string HeaderWait = "headerWait";

		// Token: 0x04001C26 RID: 7206
		internal const string MinSendBytesPerSecond = "minSendBytesPerSecond";
	}
}
