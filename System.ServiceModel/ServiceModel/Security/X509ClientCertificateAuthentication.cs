using System;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace System.ServiceModel.Security
{
	// Token: 0x02000347 RID: 839
	public class X509ClientCertificateAuthentication
	{
		// Token: 0x06001E6A RID: 7786 RVA: 0x000707A1 File Offset: 0x0006E9A1
		internal X509ClientCertificateAuthentication()
		{
		}

		// Token: 0x06001E6B RID: 7787 RVA: 0x000707C8 File Offset: 0x0006E9C8
		internal X509ClientCertificateAuthentication(X509ClientCertificateAuthentication other)
		{
			this.certificateValidationMode = other.certificateValidationMode;
			this.customCertificateValidator = other.customCertificateValidator;
			this.includeWindowsGroups = other.includeWindowsGroups;
			this.mapClientCertificateToWindowsAccount = other.mapClientCertificateToWindowsAccount;
			this.trustedStoreLocation = other.trustedStoreLocation;
			this.revocationMode = other.revocationMode;
			this.isReadOnly = other.isReadOnly;
		}

		// Token: 0x170007A8 RID: 1960
		// (get) Token: 0x06001E6C RID: 7788 RVA: 0x0007084C File Offset: 0x0006EA4C
		internal static X509CertificateValidator DefaultCertificateValidator
		{
			get
			{
				if (X509ClientCertificateAuthentication.defaultCertificateValidator == null)
				{
					bool useMachineContext = true;
					X509ChainPolicy x509ChainPolicy = new X509ChainPolicy();
					x509ChainPolicy.RevocationMode = X509RevocationMode.Online;
					if (!ServiceModelAppSettings.UseLegacyCertificateUsagePolicy)
					{
						X509ClientCertificateAuthentication.defaultCertificateValidator = new X509ClientCertificateAuthentication.ClientChainTrustValidator(useMachineContext, x509ChainPolicy);
					}
					else
					{
						X509ClientCertificateAuthentication.defaultCertificateValidator = X509CertificateValidator.CreateChainTrustValidator(useMachineContext, x509ChainPolicy);
					}
				}
				return X509ClientCertificateAuthentication.defaultCertificateValidator;
			}
		}

		// Token: 0x170007A9 RID: 1961
		// (get) Token: 0x06001E6D RID: 7789 RVA: 0x00070895 File Offset: 0x0006EA95
		// (set) Token: 0x06001E6E RID: 7790 RVA: 0x0007089D File Offset: 0x0006EA9D
		public X509CertificateValidationMode CertificateValidationMode
		{
			get
			{
				return this.certificateValidationMode;
			}
			set
			{
				X509CertificateValidationModeHelper.Validate(value);
				this.ThrowIfImmutable();
				this.certificateValidationMode = value;
			}
		}

		// Token: 0x170007AA RID: 1962
		// (get) Token: 0x06001E6F RID: 7791 RVA: 0x000708B2 File Offset: 0x0006EAB2
		// (set) Token: 0x06001E70 RID: 7792 RVA: 0x000708BA File Offset: 0x0006EABA
		public X509RevocationMode RevocationMode
		{
			get
			{
				return this.revocationMode;
			}
			set
			{
				this.ThrowIfImmutable();
				this.revocationMode = value;
			}
		}

		// Token: 0x170007AB RID: 1963
		// (get) Token: 0x06001E71 RID: 7793 RVA: 0x000708C9 File Offset: 0x0006EAC9
		// (set) Token: 0x06001E72 RID: 7794 RVA: 0x000708D1 File Offset: 0x0006EAD1
		public StoreLocation TrustedStoreLocation
		{
			get
			{
				return this.trustedStoreLocation;
			}
			set
			{
				this.ThrowIfImmutable();
				this.trustedStoreLocation = value;
			}
		}

		// Token: 0x170007AC RID: 1964
		// (get) Token: 0x06001E73 RID: 7795 RVA: 0x000708E0 File Offset: 0x0006EAE0
		// (set) Token: 0x06001E74 RID: 7796 RVA: 0x000708E8 File Offset: 0x0006EAE8
		public X509CertificateValidator CustomCertificateValidator
		{
			get
			{
				return this.customCertificateValidator;
			}
			set
			{
				this.ThrowIfImmutable();
				this.customCertificateValidator = value;
			}
		}

		// Token: 0x170007AD RID: 1965
		// (get) Token: 0x06001E75 RID: 7797 RVA: 0x000708F7 File Offset: 0x0006EAF7
		// (set) Token: 0x06001E76 RID: 7798 RVA: 0x000708FF File Offset: 0x0006EAFF
		public bool MapClientCertificateToWindowsAccount
		{
			get
			{
				return this.mapClientCertificateToWindowsAccount;
			}
			set
			{
				this.ThrowIfImmutable();
				this.mapClientCertificateToWindowsAccount = value;
			}
		}

		// Token: 0x170007AE RID: 1966
		// (get) Token: 0x06001E77 RID: 7799 RVA: 0x0007090E File Offset: 0x0006EB0E
		// (set) Token: 0x06001E78 RID: 7800 RVA: 0x00070916 File Offset: 0x0006EB16
		public bool IncludeWindowsGroups
		{
			get
			{
				return this.includeWindowsGroups;
			}
			set
			{
				this.ThrowIfImmutable();
				this.includeWindowsGroups = value;
			}
		}

		// Token: 0x06001E79 RID: 7801 RVA: 0x00070928 File Offset: 0x0006EB28
		internal X509CertificateValidator GetCertificateValidator()
		{
			if (this.certificateValidationMode == X509CertificateValidationMode.None)
			{
				return X509CertificateValidator.None;
			}
			if (this.certificateValidationMode == X509CertificateValidationMode.PeerTrust)
			{
				return X509CertificateValidator.PeerTrust;
			}
			if (this.certificateValidationMode == X509CertificateValidationMode.Custom)
			{
				if (this.customCertificateValidator == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("MissingCustomCertificateValidator")));
				}
				return this.customCertificateValidator;
			}
			else
			{
				bool useMachineContext = this.trustedStoreLocation == StoreLocation.LocalMachine;
				X509ChainPolicy x509ChainPolicy = new X509ChainPolicy();
				x509ChainPolicy.RevocationMode = this.revocationMode;
				if (!ServiceModelAppSettings.UseLegacyCertificateUsagePolicy)
				{
					if (this.certificateValidationMode == X509CertificateValidationMode.ChainTrust)
					{
						return new X509ClientCertificateAuthentication.ClientChainTrustValidator(useMachineContext, x509ChainPolicy);
					}
					return new X509ClientCertificateAuthentication.ClientPeerOrChainTrustValidator(useMachineContext, x509ChainPolicy);
				}
				else
				{
					if (this.certificateValidationMode == X509CertificateValidationMode.ChainTrust)
					{
						return X509CertificateValidator.CreateChainTrustValidator(useMachineContext, x509ChainPolicy);
					}
					return X509CertificateValidator.CreatePeerOrChainTrustValidator(useMachineContext, x509ChainPolicy);
				}
			}
		}

		// Token: 0x06001E7A RID: 7802 RVA: 0x000709D8 File Offset: 0x0006EBD8
		internal void MakeReadOnly()
		{
			this.isReadOnly = true;
		}

		// Token: 0x06001E7B RID: 7803 RVA: 0x000709E1 File Offset: 0x0006EBE1
		private void ThrowIfImmutable()
		{
			if (this.isReadOnly)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ObjectIsReadOnly")));
			}
		}

		// Token: 0x04001E8D RID: 7821
		internal const X509CertificateValidationMode DefaultCertificateValidationMode = X509CertificateValidationMode.ChainTrust;

		// Token: 0x04001E8E RID: 7822
		internal const X509RevocationMode DefaultRevocationMode = X509RevocationMode.Online;

		// Token: 0x04001E8F RID: 7823
		internal const StoreLocation DefaultTrustedStoreLocation = StoreLocation.LocalMachine;

		// Token: 0x04001E90 RID: 7824
		internal const bool DefaultMapCertificateToWindowsAccount = false;

		// Token: 0x04001E91 RID: 7825
		private static X509CertificateValidator defaultCertificateValidator;

		// Token: 0x04001E92 RID: 7826
		private X509CertificateValidationMode certificateValidationMode = X509CertificateValidationMode.ChainTrust;

		// Token: 0x04001E93 RID: 7827
		private X509RevocationMode revocationMode = X509RevocationMode.Online;

		// Token: 0x04001E94 RID: 7828
		private StoreLocation trustedStoreLocation = StoreLocation.LocalMachine;

		// Token: 0x04001E95 RID: 7829
		private X509CertificateValidator customCertificateValidator;

		// Token: 0x04001E96 RID: 7830
		private bool mapClientCertificateToWindowsAccount;

		// Token: 0x04001E97 RID: 7831
		private bool includeWindowsGroups = true;

		// Token: 0x04001E98 RID: 7832
		private bool isReadOnly;

		// Token: 0x02000B7D RID: 2941
		private class ClientChainTrustValidator : X509CertificateValidator
		{
			// Token: 0x060072D3 RID: 29395 RVA: 0x001ACC04 File Offset: 0x001AAE04
			static ClientChainTrustValidator()
			{
				Oid oid = new Oid("1.3.6.1.5.5.7.3.2", "1.3.6.1.5.5.7.3.2");
				X509ClientCertificateAuthentication.ClientChainTrustValidator.OidChainPolicy = new X509ChainPolicy
				{
					ApplicationPolicy = 
					{
						oid
					},
					RevocationMode = X509RevocationMode.NoCheck
				};
			}

			// Token: 0x060072D4 RID: 29396 RVA: 0x001ACC41 File Offset: 0x001AAE41
			public ClientChainTrustValidator(bool useMachineContext, X509ChainPolicy chainPolicy)
			{
				if (chainPolicy == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("chainPolicy");
				}
				this.useMachineContext = useMachineContext;
				this.chainPolicy = chainPolicy;
			}

			// Token: 0x060072D5 RID: 29397 RVA: 0x001ACC6C File Offset: 0x001AAE6C
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

			// Token: 0x060072D6 RID: 29398 RVA: 0x001ACCA4 File Offset: 0x001AAEA4
			internal bool TryValidate(X509Certificate2 certificate, out Exception exception)
			{
				bool result;
				using (X509Chain x509Chain = new X509Chain(this.useMachineContext))
				{
					x509Chain.ChainPolicy = this.chainPolicy;
					x509Chain.ChainPolicy.VerificationTime = DateTime.Now;
					if (!x509Chain.Build(certificate))
					{
						exception = new SecurityTokenValidationException(SR.GetString("X509ChainBuildFail", new object[]
						{
							SecurityUtils.GetCertificateId(certificate),
							X509ClientCertificateAuthentication.ClientChainTrustValidator.GetChainStatusInformation(x509Chain.ChainStatus)
						}));
						result = false;
					}
					else
					{
						if (x509Chain.ChainElements.Count > 1)
						{
							x509Chain.ChainPolicy = X509ClientCertificateAuthentication.ClientChainTrustValidator.OidChainPolicy;
							x509Chain.ChainPolicy.VerificationTime = DateTime.Now;
							X509Certificate2 certificate2 = x509Chain.ChainElements[1].Certificate;
							if (!x509Chain.Build(certificate2))
							{
								exception = new SecurityTokenValidationException(SR.GetString("X509ChainBuildFail", new object[]
								{
									SecurityUtils.GetCertificateId(certificate),
									X509ClientCertificateAuthentication.ClientChainTrustValidator.GetChainStatusInformation(x509Chain.ChainStatus)
								}));
								return false;
							}
						}
						exception = null;
						result = true;
					}
				}
				return result;
			}

			// Token: 0x060072D7 RID: 29399 RVA: 0x001ACDB0 File Offset: 0x001AAFB0
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

			// Token: 0x040040FD RID: 16637
			private bool useMachineContext;

			// Token: 0x040040FE RID: 16638
			private X509ChainPolicy chainPolicy;

			// Token: 0x040040FF RID: 16639
			private static readonly X509ChainPolicy OidChainPolicy;
		}

		// Token: 0x02000B7E RID: 2942
		private class ClientPeerOrChainTrustValidator : X509CertificateValidator
		{
			// Token: 0x060072D8 RID: 29400 RVA: 0x001ACE04 File Offset: 0x001AB004
			public ClientPeerOrChainTrustValidator(bool useMachineContext, X509ChainPolicy chainPolicy)
			{
				this.chain = new X509ClientCertificateAuthentication.ClientChainTrustValidator(useMachineContext, chainPolicy);
				this.peer = X509CertificateValidator.PeerTrust;
			}

			// Token: 0x060072D9 RID: 29401 RVA: 0x001ACE24 File Offset: 0x001AB024
			public override void Validate(X509Certificate2 certificate)
			{
				if (certificate == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("certificate");
				}
				Exception ex;
				if (this.chain.TryValidate(certificate, out ex))
				{
					return;
				}
				try
				{
					this.peer.Validate(certificate);
				}
				catch (SecurityTokenValidationException ex2)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenValidationException(ex2.Message + " " + ex.Message));
				}
			}

			// Token: 0x04004100 RID: 16640
			private X509ClientCertificateAuthentication.ClientChainTrustValidator chain;

			// Token: 0x04004101 RID: 16641
			private X509CertificateValidator peer;
		}
	}
}
