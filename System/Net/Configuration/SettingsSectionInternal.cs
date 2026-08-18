using System;
using System.Configuration;
using System.Threading;

namespace System.Net.Configuration
{
	// Token: 0x0200065F RID: 1631
	internal sealed class SettingsSectionInternal
	{
		// Token: 0x06003269 RID: 12905 RVA: 0x000D6784 File Offset: 0x000D5784
		internal SettingsSectionInternal(SettingsSection section)
		{
			if (section == null)
			{
				section = new SettingsSection();
			}
			this.alwaysUseCompletionPortsForConnect = section.Socket.AlwaysUseCompletionPortsForConnect;
			this.alwaysUseCompletionPortsForAccept = section.Socket.AlwaysUseCompletionPortsForAccept;
			this.checkCertificateName = section.ServicePointManager.CheckCertificateName;
			this.CheckCertificateRevocationList = section.ServicePointManager.CheckCertificateRevocationList;
			this.DnsRefreshTimeout = section.ServicePointManager.DnsRefreshTimeout;
			this.ipv6Enabled = section.Ipv6.Enabled;
			this.EnableDnsRoundRobin = section.ServicePointManager.EnableDnsRoundRobin;
			this.Expect100Continue = section.ServicePointManager.Expect100Continue;
			this.maximumUnauthorizedUploadLength = section.HttpWebRequest.MaximumUnauthorizedUploadLength;
			this.maximumResponseHeadersLength = section.HttpWebRequest.MaximumResponseHeadersLength;
			this.maximumErrorResponseLength = section.HttpWebRequest.MaximumErrorResponseLength;
			this.useUnsafeHeaderParsing = section.HttpWebRequest.UseUnsafeHeaderParsing;
			this.UseNagleAlgorithm = section.ServicePointManager.UseNagleAlgorithm;
			TimeSpan t = section.WebProxyScript.DownloadTimeout;
			this.downloadTimeout = ((t == TimeSpan.MaxValue || t == TimeSpan.Zero) ? -1 : ((int)t.TotalMilliseconds));
			this.performanceCountersEnabled = section.PerformanceCounters.Enabled;
			NetworkingPerfCounters.Initialize();
		}

		// Token: 0x17000BB9 RID: 3001
		// (get) Token: 0x0600326A RID: 12906 RVA: 0x000D68CC File Offset: 0x000D58CC
		internal static SettingsSectionInternal Section
		{
			get
			{
				if (SettingsSectionInternal.s_settings == null)
				{
					lock (SettingsSectionInternal.InternalSyncObject)
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

		// Token: 0x17000BBA RID: 3002
		// (get) Token: 0x0600326B RID: 12907 RVA: 0x000D692C File Offset: 0x000D592C
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

		// Token: 0x0600326C RID: 12908 RVA: 0x000D6958 File Offset: 0x000D5958
		internal static SettingsSectionInternal GetSection()
		{
			return new SettingsSectionInternal((SettingsSection)PrivilegedConfigurationManager.GetSection(ConfigurationStrings.SettingsSectionPath));
		}

		// Token: 0x17000BBB RID: 3003
		// (get) Token: 0x0600326D RID: 12909 RVA: 0x000D696E File Offset: 0x000D596E
		internal bool AlwaysUseCompletionPortsForAccept
		{
			get
			{
				return this.alwaysUseCompletionPortsForAccept;
			}
		}

		// Token: 0x17000BBC RID: 3004
		// (get) Token: 0x0600326E RID: 12910 RVA: 0x000D6976 File Offset: 0x000D5976
		internal bool AlwaysUseCompletionPortsForConnect
		{
			get
			{
				return this.alwaysUseCompletionPortsForConnect;
			}
		}

		// Token: 0x17000BBD RID: 3005
		// (get) Token: 0x0600326F RID: 12911 RVA: 0x000D697E File Offset: 0x000D597E
		internal bool CheckCertificateName
		{
			get
			{
				return this.checkCertificateName;
			}
		}

		// Token: 0x17000BBE RID: 3006
		// (get) Token: 0x06003270 RID: 12912 RVA: 0x000D6986 File Offset: 0x000D5986
		// (set) Token: 0x06003271 RID: 12913 RVA: 0x000D698E File Offset: 0x000D598E
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

		// Token: 0x17000BBF RID: 3007
		// (get) Token: 0x06003272 RID: 12914 RVA: 0x000D6997 File Offset: 0x000D5997
		// (set) Token: 0x06003273 RID: 12915 RVA: 0x000D699F File Offset: 0x000D599F
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

		// Token: 0x17000BC0 RID: 3008
		// (get) Token: 0x06003274 RID: 12916 RVA: 0x000D69A8 File Offset: 0x000D59A8
		internal int DownloadTimeout
		{
			get
			{
				return this.downloadTimeout;
			}
		}

		// Token: 0x17000BC1 RID: 3009
		// (get) Token: 0x06003275 RID: 12917 RVA: 0x000D69B0 File Offset: 0x000D59B0
		// (set) Token: 0x06003276 RID: 12918 RVA: 0x000D69B8 File Offset: 0x000D59B8
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

		// Token: 0x17000BC2 RID: 3010
		// (get) Token: 0x06003277 RID: 12919 RVA: 0x000D69C1 File Offset: 0x000D59C1
		// (set) Token: 0x06003278 RID: 12920 RVA: 0x000D69C9 File Offset: 0x000D59C9
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

		// Token: 0x17000BC3 RID: 3011
		// (get) Token: 0x06003279 RID: 12921 RVA: 0x000D69D2 File Offset: 0x000D59D2
		internal bool Ipv6Enabled
		{
			get
			{
				return this.ipv6Enabled;
			}
		}

		// Token: 0x17000BC4 RID: 3012
		// (get) Token: 0x0600327A RID: 12922 RVA: 0x000D69DA File Offset: 0x000D59DA
		// (set) Token: 0x0600327B RID: 12923 RVA: 0x000D69E2 File Offset: 0x000D59E2
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

		// Token: 0x17000BC5 RID: 3013
		// (get) Token: 0x0600327C RID: 12924 RVA: 0x000D69EB File Offset: 0x000D59EB
		internal int MaximumUnauthorizedUploadLength
		{
			get
			{
				return this.maximumUnauthorizedUploadLength;
			}
		}

		// Token: 0x17000BC6 RID: 3014
		// (get) Token: 0x0600327D RID: 12925 RVA: 0x000D69F3 File Offset: 0x000D59F3
		// (set) Token: 0x0600327E RID: 12926 RVA: 0x000D69FB File Offset: 0x000D59FB
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

		// Token: 0x17000BC7 RID: 3015
		// (get) Token: 0x0600327F RID: 12927 RVA: 0x000D6A04 File Offset: 0x000D5A04
		internal bool UseUnsafeHeaderParsing
		{
			get
			{
				return this.useUnsafeHeaderParsing;
			}
		}

		// Token: 0x17000BC8 RID: 3016
		// (get) Token: 0x06003280 RID: 12928 RVA: 0x000D6A0C File Offset: 0x000D5A0C
		// (set) Token: 0x06003281 RID: 12929 RVA: 0x000D6A14 File Offset: 0x000D5A14
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

		// Token: 0x17000BC9 RID: 3017
		// (get) Token: 0x06003282 RID: 12930 RVA: 0x000D6A1D File Offset: 0x000D5A1D
		internal bool PerformanceCountersEnabled
		{
			get
			{
				return this.performanceCountersEnabled;
			}
		}

		// Token: 0x04002F3F RID: 12095
		private static object s_InternalSyncObject;

		// Token: 0x04002F40 RID: 12096
		private static SettingsSectionInternal s_settings;

		// Token: 0x04002F41 RID: 12097
		private bool alwaysUseCompletionPortsForAccept;

		// Token: 0x04002F42 RID: 12098
		private bool alwaysUseCompletionPortsForConnect;

		// Token: 0x04002F43 RID: 12099
		private bool checkCertificateName;

		// Token: 0x04002F44 RID: 12100
		private bool checkCertificateRevocationList;

		// Token: 0x04002F45 RID: 12101
		private int downloadTimeout;

		// Token: 0x04002F46 RID: 12102
		private int dnsRefreshTimeout;

		// Token: 0x04002F47 RID: 12103
		private bool enableDnsRoundRobin;

		// Token: 0x04002F48 RID: 12104
		private bool expect100Continue;

		// Token: 0x04002F49 RID: 12105
		private bool ipv6Enabled;

		// Token: 0x04002F4A RID: 12106
		private int maximumResponseHeadersLength;

		// Token: 0x04002F4B RID: 12107
		private int maximumErrorResponseLength;

		// Token: 0x04002F4C RID: 12108
		private int maximumUnauthorizedUploadLength;

		// Token: 0x04002F4D RID: 12109
		private bool useUnsafeHeaderParsing;

		// Token: 0x04002F4E RID: 12110
		private bool useNagleAlgorithm;

		// Token: 0x04002F4F RID: 12111
		private bool performanceCountersEnabled;
	}
}
