using System;
using System.Runtime.Serialization;
using System.Security.Cryptography;

namespace System.ServiceModel.Security
{
	// Token: 0x02000308 RID: 776
	internal class SspiNegotiationTokenAuthenticatorState : NegotiationTokenAuthenticatorState
	{
		// Token: 0x06001A99 RID: 6809 RVA: 0x00063976 File Offset: 0x00061B76
		public SspiNegotiationTokenAuthenticatorState(ISspiNegotiation sspiNegotiation)
		{
			if (sspiNegotiation == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("sspiNegotiation");
			}
			this.sspiNegotiation = sspiNegotiation;
			this.negotiationDigest = CryptoHelper.NewSha1HashAlgorithm();
		}

		// Token: 0x17000699 RID: 1689
		// (get) Token: 0x06001A9A RID: 6810 RVA: 0x000639A3 File Offset: 0x00061BA3
		public ISspiNegotiation SspiNegotiation
		{
			get
			{
				return this.sspiNegotiation;
			}
		}

		// Token: 0x1700069A RID: 1690
		// (get) Token: 0x06001A9B RID: 6811 RVA: 0x000639AB File Offset: 0x00061BAB
		// (set) Token: 0x06001A9C RID: 6812 RVA: 0x000639B3 File Offset: 0x00061BB3
		internal int RequestedKeySize
		{
			get
			{
				return this.requestedKeySize;
			}
			set
			{
				this.requestedKeySize = value;
			}
		}

		// Token: 0x1700069B RID: 1691
		// (get) Token: 0x06001A9D RID: 6813 RVA: 0x000639BC File Offset: 0x00061BBC
		internal HashAlgorithm NegotiationDigest
		{
			get
			{
				return this.negotiationDigest;
			}
		}

		// Token: 0x1700069C RID: 1692
		// (get) Token: 0x06001A9E RID: 6814 RVA: 0x000639C4 File Offset: 0x00061BC4
		// (set) Token: 0x06001A9F RID: 6815 RVA: 0x000639CC File Offset: 0x00061BCC
		internal string Context
		{
			get
			{
				return this.context;
			}
			set
			{
				this.context = value;
			}
		}

		// Token: 0x1700069D RID: 1693
		// (get) Token: 0x06001AA0 RID: 6816 RVA: 0x000639D5 File Offset: 0x00061BD5
		// (set) Token: 0x06001AA1 RID: 6817 RVA: 0x000639DD File Offset: 0x00061BDD
		internal EndpointAddress AppliesTo
		{
			get
			{
				return this.appliesTo;
			}
			set
			{
				this.appliesTo = value;
			}
		}

		// Token: 0x1700069E RID: 1694
		// (get) Token: 0x06001AA2 RID: 6818 RVA: 0x000639E6 File Offset: 0x00061BE6
		// (set) Token: 0x06001AA3 RID: 6819 RVA: 0x000639EE File Offset: 0x00061BEE
		internal DataContractSerializer AppliesToSerializer
		{
			get
			{
				return this.appliesToSerializer;
			}
			set
			{
				this.appliesToSerializer = value;
			}
		}

		// Token: 0x06001AA4 RID: 6820 RVA: 0x000639F7 File Offset: 0x00061BF7
		public override string GetRemoteIdentityName()
		{
			if (this.sspiNegotiation != null && !base.IsNegotiationCompleted)
			{
				return this.sspiNegotiation.GetRemoteIdentityName();
			}
			return base.GetRemoteIdentityName();
		}

		// Token: 0x06001AA5 RID: 6821 RVA: 0x00063A1C File Offset: 0x00061C1C
		public override void Dispose()
		{
			try
			{
				object thisLock = base.ThisLock;
				lock (thisLock)
				{
					if (this.sspiNegotiation != null)
					{
						this.sspiNegotiation.Dispose();
					}
					if (this.negotiationDigest != null)
					{
						((IDisposable)this.negotiationDigest).Dispose();
					}
				}
			}
			finally
			{
				base.Dispose();
			}
		}

		// Token: 0x04001D29 RID: 7465
		private ISspiNegotiation sspiNegotiation;

		// Token: 0x04001D2A RID: 7466
		private HashAlgorithm negotiationDigest;

		// Token: 0x04001D2B RID: 7467
		private string context;

		// Token: 0x04001D2C RID: 7468
		private int requestedKeySize;

		// Token: 0x04001D2D RID: 7469
		private EndpointAddress appliesTo;

		// Token: 0x04001D2E RID: 7470
		private DataContractSerializer appliesToSerializer;
	}
}
