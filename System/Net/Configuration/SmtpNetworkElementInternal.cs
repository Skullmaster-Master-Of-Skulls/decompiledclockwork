using System;

namespace System.Net.Configuration
{
	// Token: 0x02000665 RID: 1637
	internal sealed class SmtpNetworkElementInternal
	{
		// Token: 0x060032B5 RID: 12981 RVA: 0x000D72FC File Offset: 0x000D62FC
		internal SmtpNetworkElementInternal(SmtpNetworkElement element)
		{
			this.host = element.Host;
			this.port = element.Port;
			this.clientDomain = element.ClientDomain;
			this.targetname = element.TargetName;
			if (element.DefaultCredentials)
			{
				this.credential = (NetworkCredential)CredentialCache.DefaultCredentials;
				return;
			}
			if (element.UserName != null && element.UserName.Length > 0)
			{
				this.credential = new NetworkCredential(element.UserName, element.Password);
			}
		}

		// Token: 0x17000BE3 RID: 3043
		// (get) Token: 0x060032B6 RID: 12982 RVA: 0x000D7385 File Offset: 0x000D6385
		internal NetworkCredential Credential
		{
			get
			{
				return this.credential;
			}
		}

		// Token: 0x17000BE4 RID: 3044
		// (get) Token: 0x060032B7 RID: 12983 RVA: 0x000D738D File Offset: 0x000D638D
		internal string Host
		{
			get
			{
				return this.host;
			}
		}

		// Token: 0x17000BE5 RID: 3045
		// (get) Token: 0x060032B8 RID: 12984 RVA: 0x000D7395 File Offset: 0x000D6395
		internal string ClientDomain
		{
			get
			{
				return this.clientDomain;
			}
		}

		// Token: 0x17000BE6 RID: 3046
		// (get) Token: 0x060032B9 RID: 12985 RVA: 0x000D739D File Offset: 0x000D639D
		internal int Port
		{
			get
			{
				return this.port;
			}
		}

		// Token: 0x17000BE7 RID: 3047
		// (get) Token: 0x060032BA RID: 12986 RVA: 0x000D73A5 File Offset: 0x000D63A5
		internal string TargetName
		{
			get
			{
				return this.targetname;
			}
		}

		// Token: 0x04002F69 RID: 12137
		private string targetname;

		// Token: 0x04002F6A RID: 12138
		private string host;

		// Token: 0x04002F6B RID: 12139
		private string clientDomain;

		// Token: 0x04002F6C RID: 12140
		private int port;

		// Token: 0x04002F6D RID: 12141
		private NetworkCredential credential;
	}
}
