using System;
using System.Net;

namespace System.ServiceModel.Security.Tokens
{
	// Token: 0x02000385 RID: 901
	public sealed class InitiatorServiceModelSecurityTokenRequirement : ServiceModelSecurityTokenRequirement
	{
		// Token: 0x06002155 RID: 8533 RVA: 0x0007B868 File Offset: 0x00079A68
		public InitiatorServiceModelSecurityTokenRequirement()
		{
			base.Properties.Add(ServiceModelSecurityTokenRequirement.IsInitiatorProperty, true);
		}

		// Token: 0x17000800 RID: 2048
		// (get) Token: 0x06002156 RID: 8534 RVA: 0x0007B886 File Offset: 0x00079A86
		// (set) Token: 0x06002157 RID: 8535 RVA: 0x0007B894 File Offset: 0x00079A94
		public EndpointAddress TargetAddress
		{
			get
			{
				return base.GetPropertyOrDefault<EndpointAddress>(ServiceModelSecurityTokenRequirement.TargetAddressProperty, null);
			}
			set
			{
				base.Properties[ServiceModelSecurityTokenRequirement.TargetAddressProperty] = value;
			}
		}

		// Token: 0x17000801 RID: 2049
		// (get) Token: 0x06002158 RID: 8536 RVA: 0x0007B8A7 File Offset: 0x00079AA7
		// (set) Token: 0x06002159 RID: 8537 RVA: 0x0007B8B5 File Offset: 0x00079AB5
		public Uri Via
		{
			get
			{
				return base.GetPropertyOrDefault<Uri>(ServiceModelSecurityTokenRequirement.ViaProperty, null);
			}
			set
			{
				base.Properties[ServiceModelSecurityTokenRequirement.ViaProperty] = value;
			}
		}

		// Token: 0x17000802 RID: 2050
		// (get) Token: 0x0600215A RID: 8538 RVA: 0x0007B8C8 File Offset: 0x00079AC8
		// (set) Token: 0x0600215B RID: 8539 RVA: 0x0007B8D6 File Offset: 0x00079AD6
		internal bool IsOutOfBandToken
		{
			get
			{
				return base.GetPropertyOrDefault<bool>(ServiceModelSecurityTokenRequirement.IsOutOfBandTokenProperty, false);
			}
			set
			{
				base.Properties[ServiceModelSecurityTokenRequirement.IsOutOfBandTokenProperty] = value;
			}
		}

		// Token: 0x17000803 RID: 2051
		// (get) Token: 0x0600215C RID: 8540 RVA: 0x0007B8EE File Offset: 0x00079AEE
		// (set) Token: 0x0600215D RID: 8541 RVA: 0x0007B8FC File Offset: 0x00079AFC
		internal bool PreferSslCertificateAuthenticator
		{
			get
			{
				return base.GetPropertyOrDefault<bool>(ServiceModelSecurityTokenRequirement.PreferSslCertificateAuthenticatorProperty, false);
			}
			set
			{
				base.Properties[ServiceModelSecurityTokenRequirement.PreferSslCertificateAuthenticatorProperty] = value;
			}
		}

		// Token: 0x17000804 RID: 2052
		// (get) Token: 0x0600215E RID: 8542 RVA: 0x0007B914 File Offset: 0x00079B14
		// (set) Token: 0x0600215F RID: 8543 RVA: 0x0007B91C File Offset: 0x00079B1C
		internal WebHeaderCollection WebHeaders
		{
			get
			{
				return this.webHeaderCollection;
			}
			set
			{
				this.webHeaderCollection = value;
			}
		}

		// Token: 0x06002160 RID: 8544 RVA: 0x0007B925 File Offset: 0x00079B25
		public override string ToString()
		{
			return base.InternalToString();
		}

		// Token: 0x04001F52 RID: 8018
		private WebHeaderCollection webHeaderCollection;
	}
}
