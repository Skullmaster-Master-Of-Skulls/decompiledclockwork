using System;

namespace System.Net.Configuration
{
	// Token: 0x02000345 RID: 837
	internal sealed class SmtpNetworkElementInternal
	{
		// Token: 0x06001E1D RID: 7709 RVA: 0x0008D824 File Offset: 0x0008BA24
		internal SmtpNetworkElementInternal(SmtpNetworkElement element)
		{
			this.host = element.Host;
			this.port = element.Port;
			this.targetname = element.TargetName;
			this.clientDomain = element.ClientDomain;
			this.enableSsl = element.EnableSsl;
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

		// Token: 0x170007C5 RID: 1989
		// (get) Token: 0x06001E1E RID: 7710 RVA: 0x0008D8B9 File Offset: 0x0008BAB9
		internal NetworkCredential Credential
		{
			get
			{
				return this.credential;
			}
		}

		// Token: 0x170007C6 RID: 1990
		// (get) Token: 0x06001E1F RID: 7711 RVA: 0x0008D8C1 File Offset: 0x0008BAC1
		internal string Host
		{
			get
			{
				return this.host;
			}
		}

		// Token: 0x170007C7 RID: 1991
		// (get) Token: 0x06001E20 RID: 7712 RVA: 0x0008D8C9 File Offset: 0x0008BAC9
		internal string ClientDomain
		{
			get
			{
				return this.clientDomain;
			}
		}

		// Token: 0x170007C8 RID: 1992
		// (get) Token: 0x06001E21 RID: 7713 RVA: 0x0008D8D1 File Offset: 0x0008BAD1
		internal int Port
		{
			get
			{
				return this.port;
			}
		}

		// Token: 0x170007C9 RID: 1993
		// (get) Token: 0x06001E22 RID: 7714 RVA: 0x0008D8D9 File Offset: 0x0008BAD9
		internal string TargetName
		{
			get
			{
				return this.targetname;
			}
		}

		// Token: 0x170007CA RID: 1994
		// (get) Token: 0x06001E23 RID: 7715 RVA: 0x0008D8E1 File Offset: 0x0008BAE1
		internal bool EnableSsl
		{
			get
			{
				return this.enableSsl;
			}
		}

		// Token: 0x04001CAD RID: 7341
		private string targetname;

		// Token: 0x04001CAE RID: 7342
		private string host;

		// Token: 0x04001CAF RID: 7343
		private string clientDomain;

		// Token: 0x04001CB0 RID: 7344
		private int port;

		// Token: 0x04001CB1 RID: 7345
		private NetworkCredential credential;

		// Token: 0x04001CB2 RID: 7346
		private bool enableSsl;
	}
}
