using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x0200011E RID: 286
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class AuthenticodeSignatureInformation
	{
		// Token: 0x06000932 RID: 2354 RVA: 0x00020018 File Offset: 0x0001E218
		[SecurityCritical]
		internal AuthenticodeSignatureInformation(X509Native.AXL_AUTHENTICODE_SIGNER_INFO signer, X509Chain signatureChain, TimestampInformation timestamp)
		{
			this.m_verificationResult = (SignatureVerificationResult)signer.dwError;
			this.m_hashAlgorithmId = signer.algHash;
			if (signer.pwszDescription != IntPtr.Zero)
			{
				this.m_description = Marshal.PtrToStringUni(signer.pwszDescription);
			}
			if (signer.pwszDescriptionUrl != IntPtr.Zero)
			{
				string uriString = Marshal.PtrToStringUni(signer.pwszDescriptionUrl);
				Uri.TryCreate(uriString, UriKind.RelativeOrAbsolute, out this.m_descriptionUrl);
			}
			this.m_signatureChain = signatureChain;
			if (timestamp == null || timestamp.VerificationResult == SignatureVerificationResult.MissingSignature)
			{
				this.m_timestamp = null;
				return;
			}
			if (timestamp.IsValid)
			{
				this.m_timestamp = timestamp;
				return;
			}
			this.m_verificationResult = SignatureVerificationResult.InvalidTimestamp;
		}

		// Token: 0x06000933 RID: 2355 RVA: 0x000200CC File Offset: 0x0001E2CC
		internal AuthenticodeSignatureInformation(SignatureVerificationResult error)
		{
			this.m_verificationResult = error;
		}

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x06000934 RID: 2356 RVA: 0x000200DB File Offset: 0x0001E2DB
		public string Description
		{
			get
			{
				return this.m_description;
			}
		}

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x06000935 RID: 2357 RVA: 0x000200E3 File Offset: 0x0001E2E3
		public Uri DescriptionUrl
		{
			get
			{
				return this.m_descriptionUrl;
			}
		}

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x06000936 RID: 2358 RVA: 0x000200EB File Offset: 0x0001E2EB
		public string HashAlgorithm
		{
			get
			{
				return CapiNative.GetAlgorithmName(this.m_hashAlgorithmId);
			}
		}

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x06000937 RID: 2359 RVA: 0x000200F8 File Offset: 0x0001E2F8
		public int HResult
		{
			get
			{
				return CapiNative.HResultForVerificationResult(this.m_verificationResult);
			}
		}

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x06000938 RID: 2360 RVA: 0x00020105 File Offset: 0x0001E305
		public X509Chain SignatureChain
		{
			[SecuritySafeCritical]
			[StorePermission(SecurityAction.Demand, OpenStore = true, EnumerateCertificates = true)]
			get
			{
				return this.m_signatureChain;
			}
		}

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x06000939 RID: 2361 RVA: 0x0002010D File Offset: 0x0001E30D
		public X509Certificate2 SigningCertificate
		{
			[SecuritySafeCritical]
			[StorePermission(SecurityAction.Demand, OpenStore = true, EnumerateCertificates = true)]
			get
			{
				if (this.m_signingCertificate == null && this.SignatureChain != null)
				{
					this.m_signingCertificate = this.SignatureChain.ChainElements[0].Certificate;
				}
				return this.m_signingCertificate;
			}
		}

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x0600093A RID: 2362 RVA: 0x00020141 File Offset: 0x0001E341
		public TimestampInformation Timestamp
		{
			get
			{
				return this.m_timestamp;
			}
		}

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x0600093B RID: 2363 RVA: 0x0002014C File Offset: 0x0001E34C
		public TrustStatus TrustStatus
		{
			get
			{
				SignatureVerificationResult verificationResult = this.VerificationResult;
				if (verificationResult == SignatureVerificationResult.CertificateNotExplicitlyTrusted)
				{
					return TrustStatus.KnownIdentity;
				}
				if (verificationResult == SignatureVerificationResult.CertificateExplicitlyDistrusted)
				{
					return TrustStatus.Untrusted;
				}
				if (verificationResult == SignatureVerificationResult.Valid)
				{
					return TrustStatus.Trusted;
				}
				return TrustStatus.UnknownIdentity;
			}
		}

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x0600093C RID: 2364 RVA: 0x0002017A File Offset: 0x0001E37A
		public SignatureVerificationResult VerificationResult
		{
			get
			{
				return this.m_verificationResult;
			}
		}

		// Token: 0x040006EF RID: 1775
		private string m_description;

		// Token: 0x040006F0 RID: 1776
		private Uri m_descriptionUrl;

		// Token: 0x040006F1 RID: 1777
		private CapiNative.AlgorithmId m_hashAlgorithmId;

		// Token: 0x040006F2 RID: 1778
		private X509Chain m_signatureChain;

		// Token: 0x040006F3 RID: 1779
		private TimestampInformation m_timestamp;

		// Token: 0x040006F4 RID: 1780
		private SignatureVerificationResult m_verificationResult;

		// Token: 0x040006F5 RID: 1781
		private X509Certificate2 m_signingCertificate;
	}
}
