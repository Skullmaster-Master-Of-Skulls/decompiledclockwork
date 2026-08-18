using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000A3E RID: 2622
	[MessageContract(IsWrapped = false)]
	internal class RefuseInfo
	{
		// Token: 0x060067E7 RID: 26599 RVA: 0x00183FBB File Offset: 0x001821BB
		public RefuseInfo()
		{
			this.body = new RefuseInfo.RefuseInfoDC();
		}

		// Token: 0x060067E8 RID: 26600 RVA: 0x00183FCE File Offset: 0x001821CE
		public RefuseInfo(RefuseReason reason, Referral[] referrals)
		{
			this.body = new RefuseInfo.RefuseInfoDC(reason, referrals);
		}

		// Token: 0x170018DC RID: 6364
		// (get) Token: 0x060067E9 RID: 26601 RVA: 0x00183FE3 File Offset: 0x001821E3
		public RefuseReason Reason
		{
			get
			{
				return this.body.reason;
			}
		}

		// Token: 0x170018DD RID: 6365
		// (get) Token: 0x060067EA RID: 26602 RVA: 0x00183FF0 File Offset: 0x001821F0
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

		// Token: 0x060067EB RID: 26603 RVA: 0x00184011 File Offset: 0x00182211
		public bool HasBody()
		{
			return this.body != null;
		}

		// Token: 0x04003B99 RID: 15257
		[MessageBodyMember(Name = "Refuse", Namespace = "http://schemas.microsoft.com/net/2006/05/peer")]
		private RefuseInfo.RefuseInfoDC body;

		// Token: 0x02000E76 RID: 3702
		[DataContract(Name = "RefuseInfo", Namespace = "http://schemas.microsoft.com/net/2006/05/peer")]
		private class RefuseInfoDC
		{
			// Token: 0x060083EE RID: 33774 RVA: 0x001E805B File Offset: 0x001E625B
			public RefuseInfoDC()
			{
			}

			// Token: 0x060083EF RID: 33775 RVA: 0x001E8063 File Offset: 0x001E6263
			public RefuseInfoDC(RefuseReason reason, Referral[] referrals)
			{
				this.reason = reason;
				this.referrals = referrals;
			}

			// Token: 0x04004B1F RID: 19231
			[DataMember(Name = "Reason")]
			public RefuseReason reason;

			// Token: 0x04004B20 RID: 19232
			[DataMember(Name = "Referrals")]
			public Referral[] referrals;
		}
	}
}
