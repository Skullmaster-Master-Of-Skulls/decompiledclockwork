using System;
using System.IdentityModel.Configuration;
using System.IdentityModel.Tokens;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Xml;

namespace System.IdentityModel.Selectors
{
	// Token: 0x020001B3 RID: 435
	public abstract class X509CertificateValidator : ICustomIdentityConfiguration
	{
		// Token: 0x1700036E RID: 878
		// (get) Token: 0x06000E2A RID: 3626 RVA: 0x00040F05 File Offset: 0x0003F105
		public static X509CertificateValidator None
		{
			get
			{
				if (X509CertificateValidator.none == null)
				{
					X509CertificateValidator.none = new X509CertificateValidator.NoneX509CertificateValidator();
				}
				return X509CertificateValidator.none;
			}
		}

		// Token: 0x1700036F RID: 879
		// (get) Token: 0x06000E2B RID: 3627 RVA: 0x00040F1D File Offset: 0x0003F11D
		public static X509CertificateValidator PeerTrust
		{
			get
			{
				if (X509CertificateValidator.peerTrust == null)
				{
					X509CertificateValidator.peerTrust = new X509CertificateValidator.PeerTrustValidator();
				}
				return X509CertificateValidator.peerTrust;
			}
		}

		// Token: 0x17000370 RID: 880
		// (get) Token: 0x06000E2C RID: 3628 RVA: 0x00040F35 File Offset: 0x0003F135
		public static X509CertificateValidator ChainTrust
		{
			get
			{
				if (X509CertificateValidator.chainTrust == null)
				{
					X509CertificateValidator.chainTrust = new X509CertificateValidator.ChainTrustValidator();
				}
				return X509CertificateValidator.chainTrust;
			}
		}

		// Token: 0x17000371 RID: 881
		// (get) Token: 0x06000E2D RID: 3629 RVA: 0x00040F4D File Offset: 0x0003F14D
		internal static X509CertificateValidator NTAuthChainTrust
		{
			get
			{
				if (X509CertificateValidator.ntAuthChainTrust == null)
				{
					X509CertificateValidator.ntAuthChainTrust = new X509CertificateValidator.ChainTrustValidator(false, null, 6U);
				}
				return X509CertificateValidator.ntAuthChainTrust;
			}
		}

		// Token: 0x17000372 RID: 882
		// (get) Token: 0x06000E2E RID: 3630 RVA: 0x00040F68 File Offset: 0x0003F168
		public static X509CertificateValidator PeerOrChainTrust
		{
			get
			{
				if (X509CertificateValidator.peerOrChainTrust == null)
				{
					X509CertificateValidator.peerOrChainTrust = new X509CertificateValidator.PeerOrChainTrustValidator();
				}
				return X509CertificateValidator.peerOrChainTrust;
			}
		}

		// Token: 0x06000E2F RID: 3631 RVA: 0x00040F80 File Offset: 0x0003F180
		public static X509CertificateValidator CreateChainTrustValidator(bool useMachineContext, X509ChainPolicy chainPolicy)
		{
			if (chainPolicy == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("chainPolicy");
			}
			return new X509CertificateValidator.ChainTrustValidator(useMachineContext, chainPolicy, 1U);
		}

		// Token: 0x06000E30 RID: 3632 RVA: 0x00040F9D File Offset: 0x0003F19D
		public static X509CertificateValidator CreatePeerOrChainTrustValidator(bool useMachineContext, X509ChainPolicy chainPolicy)
		{
			if (chainPolicy == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("chainPolicy");
			}
			return new X509CertificateValidator.PeerOrChainTrustValidator(useMachineContext, chainPolicy);
		}

		// Token: 0x06000E31 RID: 3633
		public abstract void Validate(X509Certificate2 certificate);

		// Token: 0x06000E32 RID: 3634 RVA: 0x0000443A File Offset: 0x0000263A
		public virtual void LoadCustomConfiguration(XmlNodeList nodelist)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotImplementedException(SR.GetString("ID0023", new object[]
			{
				base.GetType().AssemblyQualifiedName
			})));
		}

		// Token: 0x04000CF3 RID: 3315
		private static X509CertificateValidator peerTrust;

		// Token: 0x04000CF4 RID: 3316
		private static X509CertificateValidator chainTrust;

		// Token: 0x04000CF5 RID: 3317
		private static X509CertificateValidator ntAuthChainTrust;

		// Token: 0x04000CF6 RID: 3318
		private static X509CertificateValidator peerOrChainTrust;

		// Token: 0x04000CF7 RID: 3319
		private static X509CertificateValidator none;

		// Token: 0x02000299 RID: 665
		private class NoneX509CertificateValidator : X509CertificateValidator
		{
			// Token: 0x0600137B RID: 4987 RVA: 0x00052AF7 File Offset: 0x00050CF7
			public override void Validate(X509Certificate2 certificate)
			{
				if (certificate == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("certificate");
				}
			}
		}

		// Token: 0x0200029A RID: 666
		private class PeerTrustValidator : X509CertificateValidator
		{
			// Token: 0x0600137D RID: 4989 RVA: 0x00052B14 File Offset: 0x00050D14
			public override void Validate(X509Certificate2 certificate)
			{
				if (certificate == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("certificate");
				}
				Exception exception;
				if (!this.TryValidate(certificate, out exception))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(exception);
				}
			}

			// Token: 0x0600137E RID: 4990 RVA: 0x00052B4C File Offset: 0x00050D4C
			private static bool StoreContainsCertificate(StoreName storeName, X509Certificate2 certificate)
			{
				X509CertificateStore x509CertificateStore = new X509CertificateStore(storeName, StoreLocation.CurrentUser);
				X509Certificate2Collection x509Certificate2Collection = null;
				bool result;
				try
				{
					x509CertificateStore.Open(OpenFlags.ReadOnly);
					x509Certificate2Collection = x509CertificateStore.Find(X509FindType.FindByThumbprint, certificate.GetCertHash(), false);
					result = SecurityUtils.CollectionContainsCertificate(x509Certificate2Collection, certificate);
				}
				finally
				{
					SecurityUtils.ResetAllCertificates(x509Certificate2Collection);
					x509CertificateStore.Close();
				}
				return result;
			}

			// Token: 0x0600137F RID: 4991 RVA: 0x00052BA4 File Offset: 0x00050DA4
			internal bool TryValidate(X509Certificate2 certificate, out Exception exception)
			{
				DateTime now = DateTime.Now;
				if (now > certificate.NotAfter || now < certificate.NotBefore)
				{
					exception = new SecurityTokenValidationException(SR.GetString("X509InvalidUsageTime", new object[]
					{
						SecurityUtils.GetCertificateId(certificate),
						now,
						certificate.NotBefore,
						certificate.NotAfter
					}));
					return false;
				}
				if (!X509CertificateValidator.PeerTrustValidator.StoreContainsCertificate(StoreName.TrustedPeople, certificate))
				{
					exception = new SecurityTokenValidationException(SR.GetString("X509IsNotInTrustedStore", new object[]
					{
						SecurityUtils.GetCertificateId(certificate)
					}));
					return false;
				}
				if (X509CertificateValidator.PeerTrustValidator.StoreContainsCertificate(StoreName.Disallowed, certificate))
				{
					exception = new SecurityTokenValidationException(SR.GetString("X509IsInUntrustedStore", new object[]
					{
						SecurityUtils.GetCertificateId(certificate)
					}));
					return false;
				}
				exception = null;
				return true;
			}
		}

		// Token: 0x0200029B RID: 667
		private class ChainTrustValidator : X509CertificateValidator
		{
			// Token: 0x06001381 RID: 4993 RVA: 0x00052C74 File Offset: 0x00050E74
			public ChainTrustValidator()
			{
				this.chainPolicy = null;
			}

			// Token: 0x06001382 RID: 4994 RVA: 0x00052C8A File Offset: 0x00050E8A
			public ChainTrustValidator(bool useMachineContext, X509ChainPolicy chainPolicy, uint chainPolicyOID)
			{
				this.useMachineContext = useMachineContext;
				this.chainPolicy = chainPolicy;
				this.chainPolicyOID = chainPolicyOID;
			}

			// Token: 0x06001383 RID: 4995 RVA: 0x00052CB0 File Offset: 0x00050EB0
			public override void Validate(X509Certificate2 certificate)
			{
				if (certificate == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("certificate");
				}
				X509CertificateChain x509CertificateChain = new X509CertificateChain(this.useMachineContext, this.chainPolicyOID);
				if (this.chainPolicy != null)
				{
					x509CertificateChain.ChainPolicy = this.chainPolicy;
				}
				if (!x509CertificateChain.Build(certificate))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenValidationException(SR.GetString("X509ChainBuildFail", new object[]
					{
						SecurityUtils.GetCertificateId(certificate),
						X509CertificateValidator.ChainTrustValidator.GetChainStatusInformation(x509CertificateChain.ChainStatus)
					})));
				}
			}

			// Token: 0x06001384 RID: 4996 RVA: 0x00052D38 File Offset: 0x00050F38
			private static string GetChainStatusInformation(X509ChainStatus[] chainStatus)
			{
				if (chainStatus != null)
				{
					StringBuilder stringBuilder = new StringBuilder(128);
					for (int i = 0; i < chainStatus.Length; i++)
					{
						stringBuilder.Append(chainStatus[i].StatusInformation);
						stringBuilder.Append(" ");
					}
					return stringBuilder.ToString();
				}
				return string.Empty;
			}

			// Token: 0x04001136 RID: 4406
			private bool useMachineContext;

			// Token: 0x04001137 RID: 4407
			private X509ChainPolicy chainPolicy;

			// Token: 0x04001138 RID: 4408
			private uint chainPolicyOID = 1U;
		}

		// Token: 0x0200029C RID: 668
		private class PeerOrChainTrustValidator : X509CertificateValidator
		{
			// Token: 0x06001385 RID: 4997 RVA: 0x00052D8C File Offset: 0x00050F8C
			public PeerOrChainTrustValidator()
			{
				this.chain = X509CertificateValidator.ChainTrust;
				this.peer = (X509CertificateValidator.PeerTrustValidator)X509CertificateValidator.PeerTrust;
			}

			// Token: 0x06001386 RID: 4998 RVA: 0x00052DAF File Offset: 0x00050FAF
			public PeerOrChainTrustValidator(bool useMachineContext, X509ChainPolicy chainPolicy)
			{
				this.chain = X509CertificateValidator.CreateChainTrustValidator(useMachineContext, chainPolicy);
				this.peer = (X509CertificateValidator.PeerTrustValidator)X509CertificateValidator.PeerTrust;
			}

			// Token: 0x06001387 RID: 4999 RVA: 0x00052DD4 File Offset: 0x00050FD4
			public override void Validate(X509Certificate2 certificate)
			{
				if (certificate == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("certificate");
				}
				Exception ex;
				if (this.peer.TryValidate(certificate, out ex))
				{
					return;
				}
				try
				{
					this.chain.Validate(certificate);
				}
				catch (SecurityTokenValidationException ex2)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenValidationException(ex.Message + " " + ex2.Message));
				}
			}

			// Token: 0x04001139 RID: 4409
			private X509CertificateValidator chain;

			// Token: 0x0400113A RID: 4410
			private X509CertificateValidator.PeerTrustValidator peer;
		}
	}
}
