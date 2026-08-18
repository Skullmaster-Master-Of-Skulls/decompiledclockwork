using System;
using System.Security.Permissions;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x02000130 RID: 304
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class TimestampInformation
	{
		// Token: 0x060009F7 RID: 2551 RVA: 0x000243A0 File Offset: 0x000225A0
		[SecurityCritical]
		internal TimestampInformation(X509Native.AXL_AUTHENTICODE_TIMESTAMPER_INFO timestamper)
		{
			this.m_hashAlgorithmId = timestamper.algHash;
			this.m_verificationResult = (SignatureVerificationResult)timestamper.dwError;
			ulong fileTime = (ulong)timestamper.ftTimestamp.dwHighDateTime << 32 | (ulong)timestamper.ftTimestamp.dwLowDateTime;
			this.m_timestamp = DateTime.FromFileTimeUtc((long)fileTime);
			if (timestamper.pChainContext != IntPtr.Zero)
			{
				this.m_timestampChain = new X509Chain(timestamper.pChainContext);
			}
		}

		// Token: 0x060009F8 RID: 2552 RVA: 0x00024417 File Offset: 0x00022617
		internal TimestampInformation(SignatureVerificationResult error)
		{
			this.m_verificationResult = error;
		}

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x060009F9 RID: 2553 RVA: 0x00024426 File Offset: 0x00022626
		public string HashAlgorithm
		{
			get
			{
				return CapiNative.GetAlgorithmName(this.m_hashAlgorithmId);
			}
		}

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x060009FA RID: 2554 RVA: 0x00024433 File Offset: 0x00022633
		public int HResult
		{
			get
			{
				return CapiNative.HResultForVerificationResult(this.m_verificationResult);
			}
		}

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x060009FB RID: 2555 RVA: 0x00024440 File Offset: 0x00022640
		public bool IsValid
		{
			get
			{
				return this.VerificationResult == SignatureVerificationResult.Valid || this.VerificationResult == SignatureVerificationResult.CertificateNotExplicitlyTrusted;
			}
		}

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x060009FC RID: 2556 RVA: 0x00024459 File Offset: 0x00022659
		public X509Chain SignatureChain
		{
			[SecuritySafeCritical]
			[StorePermission(SecurityAction.Demand, OpenStore = true, EnumerateCertificates = true)]
			get
			{
				return this.m_timestampChain;
			}
		}

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x060009FD RID: 2557 RVA: 0x00024461 File Offset: 0x00022661
		public X509Certificate2 SigningCertificate
		{
			[SecuritySafeCritical]
			[StorePermission(SecurityAction.Demand, OpenStore = true, EnumerateCertificates = true)]
			get
			{
				if (this.m_timestamper == null && this.SignatureChain != null)
				{
					this.m_timestamper = this.SignatureChain.ChainElements[0].Certificate;
				}
				return this.m_timestamper;
			}
		}

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x060009FE RID: 2558 RVA: 0x00024495 File Offset: 0x00022695
		public DateTime Timestamp
		{
			get
			{
				return this.m_timestamp.ToLocalTime();
			}
		}

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x060009FF RID: 2559 RVA: 0x000244A2 File Offset: 0x000226A2
		public SignatureVerificationResult VerificationResult
		{
			get
			{
				return this.m_verificationResult;
			}
		}

		// Token: 0x0400074D RID: 1869
		private CapiNative.AlgorithmId m_hashAlgorithmId;

		// Token: 0x0400074E RID: 1870
		private DateTime m_timestamp;

		// Token: 0x0400074F RID: 1871
		private X509Chain m_timestampChain;

		// Token: 0x04000750 RID: 1872
		private SignatureVerificationResult m_verificationResult;

		// Token: 0x04000751 RID: 1873
		private X509Certificate2 m_timestamper;
	}
}
