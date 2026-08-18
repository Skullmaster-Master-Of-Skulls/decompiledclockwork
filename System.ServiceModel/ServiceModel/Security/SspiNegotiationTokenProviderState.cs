using System;
using System.Security.Cryptography;

namespace System.ServiceModel.Security
{
	// Token: 0x0200030B RID: 779
	internal class SspiNegotiationTokenProviderState : IssuanceTokenProviderState
	{
		// Token: 0x06001ABB RID: 6843 RVA: 0x00064256 File Offset: 0x00062456
		public SspiNegotiationTokenProviderState(ISspiNegotiation sspiNegotiation)
		{
			if (sspiNegotiation == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("sspiNegotiation");
			}
			this.sspiNegotiation = sspiNegotiation;
			this.negotiationDigest = CryptoHelper.NewSha1HashAlgorithm();
		}

		// Token: 0x170006A3 RID: 1699
		// (get) Token: 0x06001ABC RID: 6844 RVA: 0x00064283 File Offset: 0x00062483
		public ISspiNegotiation SspiNegotiation
		{
			get
			{
				return this.sspiNegotiation;
			}
		}

		// Token: 0x170006A4 RID: 1700
		// (get) Token: 0x06001ABD RID: 6845 RVA: 0x0006428B File Offset: 0x0006248B
		internal HashAlgorithm NegotiationDigest
		{
			get
			{
				return this.negotiationDigest;
			}
		}

		// Token: 0x06001ABE RID: 6846 RVA: 0x00064294 File Offset: 0x00062494
		public override void Dispose()
		{
			try
			{
				if (this.sspiNegotiation != null)
				{
					this.sspiNegotiation.Dispose();
					this.sspiNegotiation = null;
					((IDisposable)this.negotiationDigest).Dispose();
					this.negotiationDigest = null;
				}
			}
			finally
			{
				base.Dispose();
			}
		}

		// Token: 0x04001D33 RID: 7475
		private ISspiNegotiation sspiNegotiation;

		// Token: 0x04001D34 RID: 7476
		private HashAlgorithm negotiationDigest;
	}
}
