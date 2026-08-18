using System;
using System.Configuration;
using System.Net.Security;
using System.Net.Sockets;
using System.Threading;

namespace System.Net.Configuration
{
	// Token: 0x02000340 RID: 832
	internal sealed class SettingsSectionInternal
	{
		// Token: 0x06001DC2 RID: 7618 RVA: 0x0008CB38 File Offset: 0x0008AD38
		internal SettingsSectionInternal(SettingsSection section)
		{
			if (section == null)
			{
				section = new SettingsSection();
			}
			this.alwaysUseCompletionPortsForConnect = section.Socket.AlwaysUseCompletionPortsForConnect;
			this.alwaysUseCompletionPortsForAccept = section.Socket.AlwaysUseCompletionPortsForAccept;
			this.checkCertificateName = section.ServicePointManager.CheckCertificateName;
			this.checkCertificateRevocationList = section.ServicePointManager.CheckCertificateRevocationList;
			this.dnsRefreshTimeout = section.ServicePointManager.DnsRefreshTimeout;
			this.ipProtectionLevel = section.Socket.IPProtectionLevel;
			this.ipv6Enabled = section.Ipv6.Enabled;
			this.enableDnsRoundRobin = section.ServicePointManager.EnableDnsRoundRobin;
			this.encryptionPolicy = section.ServicePointManager.EncryptionPolicy;
			this.expect100Continue = section.ServicePointManager.Expect100Continue;
			this.maximumUnauthorizedUploadLength = section.HttpWebRequest.MaximumUnauthorizedUploadLength;
			this.maximumResponseHeadersLength = section.HttpWebRequest.MaximumResponseHeadersLength;
			this.maximumErrorResponseLength = section.HttpWebRequest.MaximumErrorResponseLength;
			this.useUnsafeHeaderParsing = section.HttpWebRequest.UseUnsafeHeaderParsing;
			this.useNagleAlgorithm = section.ServicePointManager.UseNagleAlgorithm;
			this.autoConfigUrlRetryInterval = section.WebProxyScript.AutoConfigUrlRetryInterval;
			TimeSpan t = section.WebProxyScript.DownloadTimeout;
			this.downloadTimeout = ((t == TimeSpan.MaxValue || t == TimeSpan.Zero) ? -1 : ((int)t.TotalMilliseconds));
			this.performanceCountersEnabled = section.PerformanceCounters.Enabled;
			this.httpListenerUnescapeRequestUrl = section.HttpListener.UnescapeRequestUrl;
			this.httpListenerTimeouts = section.HttpListener.Timeouts.GetTimeouts();
			this.defaultCredentialsHandleCacheSize = section.WindowsAuthentication.DefaultCredentialsHandleCacheSize;
			WebUtilityElement webUtility = section.WebUtility;
			this.WebUtilityUnicodeDecodingConformance = webUtility.UnicodeDecodingConformance;
			this.WebUtilityUnicodeEncodingConformance = webUtility.UnicodeEncodingConformance;
		}

		// Token: 0x1700078F RID: 1935
		// (get) Token: 0x06001DC3 RID: 7619 RVA: 0x0008CD04 File Offset: 0x0008AF04
		internal static SettingsSectionInternal Section
		{
			get
			{
				if (SettingsSectionInternal.s_settings == null)
				{
					object internalSyncObject = SettingsSectionInternal.InternalSyncObject;
					lock (internalSyncObject)
					{
						if (SettingsSectionInternal.s_settings == null)
						{
							SettingsSectionInternal.s_settings = new SettingsSectionInternal((SettingsSection)PrivilegedConfigurationManager.GetSection(ConfigurationStrings.SettingsSectionPath));
						}
					}
				}
				return SettingsSectionInternal.s_settings;
			}
		}

		// Token: 0x17000790 RID: 1936
		// (get) Token: 0x06001DC4 RID: 7620 RVA: 0x0008CD74 File Offset: 0x0008AF74
		private static object InternalSyncObject
		{
			get
			{
				if (SettingsSectionInternal.s_InternalSyncObject == null)
				{
					object value = new object();
					Interlocked.CompareExchange(ref SettingsSectionInternal.s_InternalSyncObject, value, null);
				}
				return SettingsSectionInternal.s_InternalSyncObject;
			}
		}

		// Token: 0x06001DC5 RID: 7621 RVA: 0x0008CDA0 File Offset: 0x0008AFA0
		internal static SettingsSectionInternal GetSection()
		{
			return new SettingsSectionInternal((SettingsSection)PrivilegedConfigurationManager.GetSection(ConfigurationStrings.SettingsSectionPath));
		}

		// Token: 0x17000791 RID: 1937
		// (get) Token: 0x06001DC6 RID: 7622 RVA: 0x0008CDB6 File Offset: 0x0008AFB6
		internal bool AlwaysUseCompletionPortsForAccept
		{
			get
			{
				return this.alwaysUseCompletionPortsForAccept;
			}
		}

		// Token: 0x17000792 RID: 1938
		// (get) Token: 0x06001DC7 RID: 7623 RVA: 0x0008CDBE File Offset: 0x0008AFBE
		internal bool AlwaysUseCompletionPortsForConnect
		{
			get
			{
				return this.alwaysUseCompletionPortsForConnect;
			}
		}

		// Token: 0x17000793 RID: 1939
		// (get) Token: 0x06001DC8 RID: 7624 RVA: 0x0008CDC6 File Offset: 0x0008AFC6
		internal int AutoConfigUrlRetryInterval
		{
			get
			{
				return this.autoConfigUrlRetryInterval;
			}
		}

		// Token: 0x17000794 RID: 1940
		// (get) Token: 0x06001DC9 RID: 7625 RVA: 0x0008CDCE File Offset: 0x0008AFCE
		internal bool CheckCertificateName
		{
			get
			{
				return this.checkCertificateName;
			}
		}

		// Token: 0x17000795 RID: 1941
		// (get) Token: 0x06001DCA RID: 7626 RVA: 0x0008CDD6 File Offset: 0x0008AFD6
		// (set) Token: 0x06001DCB RID: 7627 RVA: 0x0008CDDE File Offset: 0x0008AFDE
		internal bool CheckCertificateRevocationList
		{
			get
			{
				return this.checkCertificateRevocationList;
			}
			set
			{
				this.checkCertificateRevocationList = value;
			}
		}

		// Token: 0x17000796 RID: 1942
		// (get) Token: 0x06001DCC RID: 7628 RVA: 0x0008CDE7 File Offset: 0x0008AFE7
		// (set) Token: 0x06001DCD RID: 7629 RVA: 0x0008CDEF File Offset: 0x0008AFEF
		internal int DefaultCredentialsHandleCacheSize
		{
			get
			{
				return this.defaultCredentialsHandleCacheSize;
			}
			set
			{
				this.defaultCredentialsHandleCacheSize = value;
			}
		}

		// Token: 0x17000797 RID: 1943
		// (get) Token: 0x06001DCE RID: 7630 RVA: 0x0008CDF8 File Offset: 0x0008AFF8
		// (set) Token: 0x06001DCF RID: 7631 RVA: 0x0008CE00 File Offset: 0x0008B000
		internal int DnsRefreshTimeout
		{
			get
			{
				return this.dnsRefreshTimeout;
			}
			set
			{
				this.dnsRefreshTimeout = value;
			}
		}

		// Token: 0x17000798 RID: 1944
		// (get) Token: 0x06001DD0 RID: 7632 RVA: 0x0008CE09 File Offset: 0x0008B009
		internal int DownloadTimeout
		{
			get
			{
				return this.downloadTimeout;
			}
		}

		// Token: 0x17000799 RID: 1945
		// (get) Token: 0x06001DD1 RID: 7633 RVA: 0x0008CE11 File Offset: 0x0008B011
		// (set) Token: 0x06001DD2 RID: 7634 RVA: 0x0008CE19 File Offset: 0x0008B019
		internal bool EnableDnsRoundRobin
		{
			get
			{
				return this.enableDnsRoundRobin;
			}
			set
			{
				this.enableDnsRoundRobin = value;
			}
		}

		// Token: 0x1700079A RID: 1946
		// (get) Token: 0x06001DD3 RID: 7635 RVA: 0x0008CE22 File Offset: 0x0008B022
		internal EncryptionPolicy EncryptionPolicy
		{
			get
			{
				return this.encryptionPolicy;
			}
		}

		// Token: 0x1700079B RID: 1947
		// (get) Token: 0x06001DD4 RID: 7636 RVA: 0x0008CE2A File Offset: 0x0008B02A
		// (set) Token: 0x06001DD5 RID: 7637 RVA: 0x0008CE32 File Offset: 0x0008B032
		internal bool Expect100Continue
		{
			get
			{
				return this.expect100Continue;
			}
			set
			{
				this.expect100Continue = value;
			}
		}

		// Token: 0x1700079C RID: 1948
		// (get) Token: 0x06001DD6 RID: 7638 RVA: 0x0008CE3B File Offset: 0x0008B03B
		internal IPProtectionLevel IPProtectionLevel
		{
			get
			{
				return this.ipProtectionLevel;
			}
		}

		// Token: 0x1700079D RID: 1949
		// (get) Token: 0x06001DD7 RID: 7639 RVA: 0x0008CE43 File Offset: 0x0008B043
		internal bool Ipv6Enabled
		{
			get
			{
				return this.ipv6Enabled;
			}
		}

		// Token: 0x1700079E RID: 1950
		// (get) Token: 0x06001DD8 RID: 7640 RVA: 0x0008CE4B File Offset: 0x0008B04B
		// (set) Token: 0x06001DD9 RID: 7641 RVA: 0x0008CE53 File Offset: 0x0008B053
		internal int MaximumResponseHeadersLength
		{
			get
			{
				return this.maximumResponseHeadersLength;
			}
			set
			{
				this.maximumResponseHeadersLength = value;
			}
		}

		// Token: 0x1700079F RID: 1951
		// (get) Token: 0x06001DDA RID: 7642 RVA: 0x0008CE5C File Offset: 0x0008B05C
		internal int MaximumUnauthorizedUploadLength
		{
			get
			{
				return this.maximumUnauthorizedUploadLength;
			}
		}

		// Token: 0x170007A0 RID: 1952
		// (get) Token: 0x06001DDB RID: 7643 RVA: 0x0008CE64 File Offset: 0x0008B064
		// (set) Token: 0x06001DDC RID: 7644 RVA: 0x0008CE6C File Offset: 0x0008B06C
		internal int MaximumErrorResponseLength
		{
			get
			{
				return this.maximumErrorResponseLength;
			}
			set
			{
				this.maximumErrorResponseLength = value;
			}
		}

		// Token: 0x170007A1 RID: 1953
		// (get) Token: 0x06001DDD RID: 7645 RVA: 0x0008CE75 File Offset: 0x0008B075
		internal bool UseUnsafeHeaderParsing
		{
			get
			{
				return this.useUnsafeHeaderParsing;
			}
		}

		// Token: 0x170007A2 RID: 1954
		// (get) Token: 0x06001DDE RID: 7646 RVA: 0x0008CE7D File Offset: 0x0008B07D
		// (set) Token: 0x06001DDF RID: 7647 RVA: 0x0008CE85 File Offset: 0x0008B085
		internal bool UseNagleAlgorithm
		{
			get
			{
				return this.useNagleAlgorithm;
			}
			set
			{
				this.useNagleAlgorithm = value;
			}
		}

		// Token: 0x170007A3 RID: 1955
		// (get) Token: 0x06001DE0 RID: 7648 RVA: 0x0008CE8E File Offset: 0x0008B08E
		internal bool PerformanceCountersEnabled
		{
			get
			{
				return this.performanceCountersEnabled;
			}
		}

		// Token: 0x170007A4 RID: 1956
		// (get) Token: 0x06001DE1 RID: 7649 RVA: 0x0008CE96 File Offset: 0x0008B096
		internal bool HttpListenerUnescapeRequestUrl
		{
			get
			{
				return this.httpListenerUnescapeRequestUrl;
			}
		}

		// Token: 0x170007A5 RID: 1957
		// (get) Token: 0x06001DE2 RID: 7650 RVA: 0x0008CE9E File Offset: 0x0008B09E
		internal long[] HttpListenerTimeouts
		{
			get
			{
				return this.httpListenerTimeouts;
			}
		}

		// Token: 0x170007A6 RID: 1958
		// (get) Token: 0x06001DE3 RID: 7651 RVA: 0x0008CEA6 File Offset: 0x0008B0A6
		// (set) Token: 0x06001DE4 RID: 7652 RVA: 0x0008CEAE File Offset: 0x0008B0AE
		internal UnicodeDecodingConformance WebUtilityUnicodeDecodingConformance { get; private set; }

		// Token: 0x170007A7 RID: 1959
		// (get) Token: 0x06001DE5 RID: 7653 RVA: 0x0008CEB7 File Offset: 0x0008B0B7
		// (set) Token: 0x06001DE6 RID: 7654 RVA: 0x0008CEBF File Offset: 0x0008B0BF
		internal UnicodeEncodingConformance WebUtilityUnicodeEncodingConformance { get; private set; }

		// Token: 0x04001C77 RID: 7287
		private static object s_InternalSyncObject;

		// Token: 0x04001C7A RID: 7290
		private static volatile SettingsSectionInternal s_settings;

		// Token: 0x04001C7B RID: 7291
		private bool alwaysUseCompletionPortsForAccept;

		// Token: 0x04001C7C RID: 7292
		private bool alwaysUseCompletionPortsForConnect;

		// Token: 0x04001C7D RID: 7293
		private bool checkCertificateName;

		// Token: 0x04001C7E RID: 7294
		private bool checkCertificateRevocationList;

		// Token: 0x04001C7F RID: 7295
		private int defaultCredentialsHandleCacheSize;

		// Token: 0x04001C80 RID: 7296
		private int autoConfigUrlRetryInterval;

		// Token: 0x04001C81 RID: 7297
		private int downloadTimeout;

		// Token: 0x04001C82 RID: 7298
		private int dnsRefreshTimeout;

		// Token: 0x04001C83 RID: 7299
		private bool enableDnsRoundRobin;

		// Token: 0x04001C84 RID: 7300
		private EncryptionPolicy encryptionPolicy;

		// Token: 0x04001C85 RID: 7301
		private bool expect100Continue;

		// Token: 0x04001C86 RID: 7302
		private IPProtectionLevel ipProtectionLevel;

		// Token: 0x04001C87 RID: 7303
		private bool ipv6Enabled;

		// Token: 0x04001C88 RID: 7304
		private int maximumResponseHeadersLength;

		// Token: 0x04001C89 RID: 7305
		private int maximumErrorResponseLength;

		// Token: 0x04001C8A RID: 7306
		private int maximumUnauthorizedUploadLength;

		// Token: 0x04001C8B RID: 7307
		private bool useUnsafeHeaderParsing;

		// Token: 0x04001C8C RID: 7308
		private bool useNagleAlgorithm;

		// Token: 0x04001C8D RID: 7309
		private bool performanceCountersEnabled;

		// Token: 0x04001C8E RID: 7310
		private bool httpListenerUnescapeRequestUrl;

		// Token: 0x04001C8F RID: 7311
		private long[] httpListenerTimeouts;
	}
}
