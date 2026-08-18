using System;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel.Security;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000A2A RID: 2602
	internal class PeerCertificateClientCredentials : SecurityCredentialsManager
	{
		// Token: 0x06006748 RID: 26440 RVA: 0x00181B60 File Offset: 0x0017FD60
		public PeerCertificateClientCredentials(X509Certificate2 selfCertificate, X509CertificateValidator validator)
		{
			this.selfCertificate = selfCertificate;
			this.certificateValidator = validator;
		}

		// Token: 0x06006749 RID: 26441 RVA: 0x00181B76 File Offset: 0x0017FD76
		public override SecurityTokenManager CreateSecurityTokenManager()
		{
			return new PeerCertificateClientCredentials.PeerCertificateClientCredentialsSecurityTokenManager(this);
		}

		// Token: 0x04003B47 RID: 15175
		private X509Certificate2 selfCertificate;

		// Token: 0x04003B48 RID: 15176
		private X509CertificateValidator certificateValidator;

		// Token: 0x02000E6F RID: 3695
		private class PeerCertificateClientCredentialsSecurityTokenManager : SecurityTokenManager
		{
			// Token: 0x060083D0 RID: 33744 RVA: 0x001E79F0 File Offset: 0x001E5BF0
			public PeerCertificateClientCredentialsSecurityTokenManager(PeerCertificateClientCredentials creds)
			{
				this.creds = creds;
			}

			// Token: 0x060083D1 RID: 33745 RVA: 0x001E7A00 File Offset: 0x001E5C00
			public override SecurityTokenSerializer CreateSecurityTokenSerializer(SecurityTokenVersion version)
			{
				MessageSecurityTokenVersion messageSecurityTokenVersion = (MessageSecurityTokenVersion)version;
				return new WSSecurityTokenSerializer(messageSecurityTokenVersion.SecurityVersion, messageSecurityTokenVersion.TrustVersion, messageSecurityTokenVersion.SecureConversationVersion, messageSecurityTokenVersion.EmitBspRequiredAttributes, null, null, null);
			}

			// Token: 0x060083D2 RID: 33746 RVA: 0x001E7A34 File Offset: 0x001E5C34
			public override SecurityTokenAuthenticator CreateSecurityTokenAuthenticator(SecurityTokenRequirement tokenRequirement, out SecurityTokenResolver outOfBandTokenResolver)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException());
			}

			// Token: 0x060083D3 RID: 33747 RVA: 0x001E7A48 File Offset: 0x001E5C48
			public override SecurityTokenProvider CreateSecurityTokenProvider(SecurityTokenRequirement requirement)
			{
				if (requirement == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("requirement");
				}
				if (requirement.TokenType == SecurityTokenTypes.X509Certificate && requirement.KeyUsage == SecurityKeyUsage.Signature)
				{
					return new PeerX509TokenProvider(this.creds.certificateValidator, this.creds.selfCertificate);
				}
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException());
			}

			// Token: 0x04004B10 RID: 19216
			private PeerCertificateClientCredentials creds;
		}
	}
}
