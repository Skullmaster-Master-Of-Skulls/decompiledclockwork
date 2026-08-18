using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000A37 RID: 2615
	[MessageContract(IsWrapped = false)]
	internal class DisconnectInfo
	{
		// Token: 0x060067D1 RID: 26577 RVA: 0x00183EF5 File Offset: 0x001820F5
		public DisconnectInfo()
		{
			this.body = new DisconnectInfo.DisconnectInfoDC();
		}

		// Token: 0x060067D2 RID: 26578 RVA: 0x00183F08 File Offset: 0x00182108
		public DisconnectInfo(DisconnectReason reason, Referral[] referrals)
		{
			this.body = new DisconnectInfo.DisconnectInfoDC(reason, referrals);
		}

		// Token: 0x170018D8 RID: 6360
		// (get) Token: 0x060067D3 RID: 26579 RVA: 0x00183F1D File Offset: 0x0018211D
		public DisconnectReason Reason
		{
			get
			{
				return this.body.reason;
			}
		}

		// Token: 0x170018D9 RID: 6361
		// (get) Token: 0x060067D4 RID: 26580 RVA: 0x00183F2A File Offset: 0x0018212A
		public IList<Referral> Referrals
		{
			get
			{
				if (this.body.referrals == null)
				{
					return null;
				}
				return Array.AsReadOnly<Referral>(this.body.referrals);
			}
		}

		// Token: 0x060067D5 RID: 26581 RVA: 0x00183F4B File Offset: 0x0018214B
		public bool HasBody()
		{
			return this.body != null;
		}

		// Token: 0x04003B8F RID: 15247
		[MessageBodyMember(Name = "Disconnect", Namespace = "http://schemas.microsoft.com/net/2006/05/peer")]
		private DisconnectInfo.DisconnectInfoDC body;

		// Token: 0x02000E75 RID: 3701
		[DataContract(Name = "DisconnectInfo", Namespace = "http://schemas.microsoft.com/net/2006/05/peer")]
		private class DisconnectInfoDC
		{
			// Token: 0x060083EC RID: 33772 RVA: 0x001E803D File Offset: 0x001E623D
			public DisconnectInfoDC()
			{
			}

			// Token: 0x060083ED RID: 33773 RVA: 0x001E8045 File Offset: 0x001E6245
			public DisconnectInfoDC(DisconnectReason reason, Referral[] referrals)
			{
				this.reason = reason;
				this.referrals = referrals;
			}

			// Token: 0x04004B1D RID: 19229
			[DataMember(Name = "Reason")]
			public DisconnectReason reason;

			// Token: 0x04004B1E RID: 19230
			[DataMember(Name = "Referrals")]
			public Referral[] referrals;
		}
	}
}
