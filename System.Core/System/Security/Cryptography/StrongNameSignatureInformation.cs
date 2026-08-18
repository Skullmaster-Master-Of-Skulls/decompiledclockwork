using System;
using System.Security.Permissions;

namespace System.Security.Cryptography
{
	// Token: 0x0200011B RID: 283
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class StrongNameSignatureInformation
	{
		// Token: 0x06000900 RID: 2304 RVA: 0x0001F1F6 File Offset: 0x0001D3F6
		internal StrongNameSignatureInformation(AsymmetricAlgorithm publicKey)
		{
			this.m_verificationResult = SignatureVerificationResult.Valid;
			this.m_publicKey = publicKey;
		}

		// Token: 0x06000901 RID: 2305 RVA: 0x0001F20C File Offset: 0x0001D40C
		internal StrongNameSignatureInformation(SignatureVerificationResult error)
		{
			this.m_verificationResult = error;
		}

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x06000902 RID: 2306 RVA: 0x0001F21B File Offset: 0x0001D41B
		public string HashAlgorithm
		{
			get
			{
				return StrongNameSignatureInformation.StrongNameHashAlgorithm;
			}
		}

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x06000903 RID: 2307 RVA: 0x0001F222 File Offset: 0x0001D422
		public int HResult
		{
			get
			{
				return CapiNative.HResultForVerificationResult(this.m_verificationResult);
			}
		}

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x06000904 RID: 2308 RVA: 0x0001F22F File Offset: 0x0001D42F
		public bool IsValid
		{
			get
			{
				return this.m_verificationResult == SignatureVerificationResult.Valid;
			}
		}

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x06000905 RID: 2309 RVA: 0x0001F23A File Offset: 0x0001D43A
		public AsymmetricAlgorithm PublicKey
		{
			get
			{
				return this.m_publicKey;
			}
		}

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x06000906 RID: 2310 RVA: 0x0001F242 File Offset: 0x0001D442
		public SignatureVerificationResult VerificationResult
		{
			get
			{
				return this.m_verificationResult;
			}
		}

		// Token: 0x040006E8 RID: 1768
		private SignatureVerificationResult m_verificationResult;

		// Token: 0x040006E9 RID: 1769
		private AsymmetricAlgorithm m_publicKey;

		// Token: 0x040006EA RID: 1770
		private static readonly string StrongNameHashAlgorithm = CapiNative.GetAlgorithmName(CapiNative.AlgorithmId.Sha1);
	}
}
