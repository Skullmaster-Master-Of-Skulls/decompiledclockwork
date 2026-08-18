using System;
using System.Security.Cryptography.X509Certificates;

namespace System.Security.Policy
{
	// Token: 0x02000103 RID: 259
	internal class ParsedData
	{
		// Token: 0x17000162 RID: 354
		// (get) Token: 0x06000435 RID: 1077 RVA: 0x0000DE74 File Offset: 0x0000C074
		// (set) Token: 0x06000436 RID: 1078 RVA: 0x0000DE7C File Offset: 0x0000C07C
		public bool RequestsShellIntegration
		{
			get
			{
				return this.requestsShellIntegration;
			}
			set
			{
				this.requestsShellIntegration = value;
			}
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x06000437 RID: 1079 RVA: 0x0000DE85 File Offset: 0x0000C085
		// (set) Token: 0x06000438 RID: 1080 RVA: 0x0000DE8D File Offset: 0x0000C08D
		public X509Certificate2 Certificate
		{
			get
			{
				return this.certificate;
			}
			set
			{
				this.certificate = value;
			}
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x06000439 RID: 1081 RVA: 0x0000DE96 File Offset: 0x0000C096
		// (set) Token: 0x0600043A RID: 1082 RVA: 0x0000DE9E File Offset: 0x0000C09E
		public string AppName
		{
			get
			{
				return this.appName;
			}
			set
			{
				this.appName = value;
			}
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x0600043B RID: 1083 RVA: 0x0000DEA7 File Offset: 0x0000C0A7
		// (set) Token: 0x0600043C RID: 1084 RVA: 0x0000DEAF File Offset: 0x0000C0AF
		public string AppPublisher
		{
			get
			{
				return this.appPublisher;
			}
			set
			{
				this.appPublisher = value;
			}
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x0600043D RID: 1085 RVA: 0x0000DEB8 File Offset: 0x0000C0B8
		// (set) Token: 0x0600043E RID: 1086 RVA: 0x0000DEC0 File Offset: 0x0000C0C0
		public string AuthenticodedPublisher
		{
			get
			{
				return this.authenticodedPublisher;
			}
			set
			{
				this.authenticodedPublisher = value;
			}
		}

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x0600043F RID: 1087 RVA: 0x0000DEC9 File Offset: 0x0000C0C9
		// (set) Token: 0x06000440 RID: 1088 RVA: 0x0000DED1 File Offset: 0x0000C0D1
		public bool UseManifestForTrust
		{
			get
			{
				return this.disallowTrustOverride;
			}
			set
			{
				this.disallowTrustOverride = value;
			}
		}

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x06000441 RID: 1089 RVA: 0x0000DEDA File Offset: 0x0000C0DA
		// (set) Token: 0x06000442 RID: 1090 RVA: 0x0000DEE2 File Offset: 0x0000C0E2
		public string SupportUrl
		{
			get
			{
				return this.supportUrl;
			}
			set
			{
				this.supportUrl = value;
			}
		}

		// Token: 0x0400044C RID: 1100
		private bool requestsShellIntegration;

		// Token: 0x0400044D RID: 1101
		private string appName;

		// Token: 0x0400044E RID: 1102
		private string appPublisher;

		// Token: 0x0400044F RID: 1103
		private string supportUrl;

		// Token: 0x04000450 RID: 1104
		private string authenticodedPublisher;

		// Token: 0x04000451 RID: 1105
		private bool disallowTrustOverride;

		// Token: 0x04000452 RID: 1106
		private X509Certificate2 certificate;
	}
}
