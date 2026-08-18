using System;
using System.IdentityModel.Selectors;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel.Security;

namespace System.IdentityModel
{
	// Token: 0x020000C1 RID: 193
	internal class X509CertificateValidatorEx : X509CertificateValidator
	{
		// Token: 0x060005E1 RID: 1505 RVA: 0x000154FC File Offset: 0x000136FC
		public X509CertificateValidatorEx(X509CertificateValidationMode certificateValidationMode, X509RevocationMode revocationMode, StoreLocation trustedStoreLocation)
		{
			this.certificateValidationMode = certificateValidationMode;
			switch (this.certificateValidationMode)
			{
			case X509CertificateValidationMode.None:
				this.validator = X509CertificateValidator.None;
				return;
			case X509CertificateValidationMode.PeerTrust:
				this.validator = X509CertificateValidator.PeerTrust;
				return;
			case X509CertificateValidationMode.ChainTrust:
			{
				bool useMachineContext = trustedStoreLocation == StoreLocation.LocalMachine;
				this.chainPolicy = new X509ChainPolicy();
				this.chainPolicy.RevocationMode = revocationMode;
				this.validator = X509CertificateValidator.CreateChainTrustValidator(useMachineContext, this.chainPolicy);
				return;
			}
			case X509CertificateValidationMode.PeerOrChainTrust:
			{
				bool useMachineContext2 = trustedStoreLocation == StoreLocation.LocalMachine;
				this.chainPolicy = new X509ChainPolicy();
				this.chainPolicy.RevocationMode = revocationMode;
				this.validator = X509CertificateValidator.CreatePeerOrChainTrustValidator(useMachineContext2, this.chainPolicy);
				return;
			}
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ID4256")));
		}

		// Token: 0x060005E2 RID: 1506 RVA: 0x000155C8 File Offset: 0x000137C8
		public override void Validate(X509Certificate2 certificate)
		{
			if (this.certificateValidationMode == X509CertificateValidationMode.ChainTrust || this.certificateValidationMode == X509CertificateValidationMode.PeerOrChainTrust)
			{
				this.chainPolicy.VerificationTime = DateTime.Now;
			}
			this.validator.Validate(certificate);
		}

		// Token: 0x040004FD RID: 1277
		private X509CertificateValidationMode certificateValidationMode;

		// Token: 0x040004FE RID: 1278
		private X509ChainPolicy chainPolicy;

		// Token: 0x040004FF RID: 1279
		private X509CertificateValidator validator;
	}
}
