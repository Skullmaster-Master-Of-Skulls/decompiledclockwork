using System;
using System.Collections.Generic;
using System.Text;
using TechnoPro.Common.Win32;

namespace TechnoPro.ClockWorkServer.Client.Configuration
{
	// Token: 0x02000003 RID: 3
	public class WCFClientConfig
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000007 RID: 7 RVA: 0x0000218C File Offset: 0x0000038C
		// (set) Token: 0x06000008 RID: 8 RVA: 0x00002194 File Offset: 0x00000394
		public DateTime ClientConfigModifiedDatetime { get; set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000009 RID: 9 RVA: 0x0000219D File Offset: 0x0000039D
		// (set) Token: 0x0600000A RID: 10 RVA: 0x000021A5 File Offset: 0x000003A5
		public InternetInformationServicesVersion HostType { get; set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000B RID: 11 RVA: 0x000021AE File Offset: 0x000003AE
		// (set) Token: 0x0600000C RID: 12 RVA: 0x000021B6 File Offset: 0x000003B6
		public string Version { get; set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000D RID: 13 RVA: 0x000021BF File Offset: 0x000003BF
		// (set) Token: 0x0600000E RID: 14 RVA: 0x000021C7 File Offset: 0x000003C7
		public IDictionary<string, string> Settings { get; set; }

		// Token: 0x0600000F RID: 15 RVA: 0x000021D0 File Offset: 0x000003D0
		public WCFClientConfig()
		{
			this.Settings = new Dictionary<string, string>();
		}

		// Token: 0x17000007 RID: 7
		public string this[string name]
		{
			get
			{
				return this.Settings[name];
			}
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002208 File Offset: 0x00000408
		public string Endpoint(string servicetype)
		{
			InternetInformationServicesVersion hostType = this.HostType;
			string result;
			if (hostType != InternetInformationServicesVersion.IIS6)
			{
				if (hostType != InternetInformationServicesVersion.IIS7)
				{
					result = (servicetype.ToUpper().Equals("IMESSAGING") ? string.Format("WSDualHttp_{0}Service", servicetype.Substring(1)) : string.Format("WSHttpBinding_{0}Service", servicetype.Substring(1)));
				}
				else
				{
					result = string.Format("NetTcpBinding_{0}Service", servicetype.Substring(1));
				}
			}
			else
			{
				result = (servicetype.ToUpper().Equals("IMESSAGING") ? string.Format("WSDualHttpBinding_{0}Service", servicetype.Substring(1)) : string.Format("WSHttpBinding_{0}Service", servicetype.Substring(1)));
			}
			return result;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000022B0 File Offset: 0x000004B0
		[Obsolete("Use TechnoPro.Common.Text.MergeCodePatternAdapter instead")]
		public string ReplacePattern(string pattern)
		{
			StringBuilder stringBuilder = new StringBuilder(pattern);
			stringBuilder = stringBuilder.Replace("{hosttype}", this.HostType.ToString());
			foreach (KeyValuePair<string, string> keyValuePair in this.Settings)
			{
				stringBuilder = stringBuilder.Replace(string.Format("{{{0}}}", keyValuePair.Key), keyValuePair.Value);
			}
			return stringBuilder.ToString();
		}
	}
}
