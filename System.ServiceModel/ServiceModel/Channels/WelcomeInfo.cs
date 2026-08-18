using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000A41 RID: 2625
	[MessageContract(IsWrapped = false)]
	internal class WelcomeInfo
	{
		// Token: 0x060067F1 RID: 26609 RVA: 0x00184069 File Offset: 0x00182269
		public WelcomeInfo()
		{
			this.body = new WelcomeInfo.WelcomeInfoDC();
		}

		// Token: 0x060067F2 RID: 26610 RVA: 0x0018407C File Offset: 0x0018227C
		public WelcomeInfo(ulong nodeId, Referral[] referrals)
		{
			this.body = new WelcomeInfo.WelcomeInfoDC(nodeId, referrals);
		}

		// Token: 0x170018E0 RID: 6368
		// (get) Token: 0x060067F3 RID: 26611 RVA: 0x00184091 File Offset: 0x00182291
		public ulong NodeId
		{
			get
			{
				return this.body.nodeId;
			}
		}

		// Token: 0x170018E1 RID: 6369
		// (get) Token: 0x060067F4 RID: 26612 RVA: 0x0018409E File Offset: 0x0018229E
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

		// Token: 0x060067F5 RID: 26613 RVA: 0x001840BF File Offset: 0x001822BF
		public bool HasBody()
		{
			return this.body != null;
		}

		// Token: 0x04003B9F RID: 15263
		[MessageBodyMember(Name = "Welcome", Namespace = "http://schemas.microsoft.com/net/2006/05/peer")]
		private WelcomeInfo.WelcomeInfoDC body;

		// Token: 0x02000E78 RID: 3704
		[DataContract(Name = "WelcomeInfo", Namespace = "http://schemas.microsoft.com/net/2006/05/peer")]
		private class WelcomeInfoDC
		{
			// Token: 0x060083F2 RID: 33778 RVA: 0x001E8097 File Offset: 0x001E6297
			public WelcomeInfoDC()
			{
			}

			// Token: 0x060083F3 RID: 33779 RVA: 0x001E809F File Offset: 0x001E629F
			public WelcomeInfoDC(ulong nodeId, Referral[] referrals)
			{
				this.nodeId = nodeId;
				this.referrals = referrals;
			}

			// Token: 0x04004B23 RID: 19235
			[DataMember(Name = "NodeId")]
			public ulong nodeId;

			// Token: 0x04004B24 RID: 19236
			[DataMember(Name = "Referrals")]
			public Referral[] referrals;
		}
	}
}
