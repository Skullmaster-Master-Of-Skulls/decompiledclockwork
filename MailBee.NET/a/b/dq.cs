using System;
using MailBee.Outlook;

namespace a.b
{
	// Token: 0x02000304 RID: 772
	internal abstract class dq : df
	{
		// Token: 0x06001B27 RID: 6951 RVA: 0x00076A87 File Offset: 0x00075A87
		public dq(POIDocument A_0) : base(A_0)
		{
		}

		// Token: 0x06001B28 RID: 6952 RVA: 0x00076A90 File Offset: 0x00075A90
		public new virtual DocumentSummaryInformation a()
		{
			return this.a.DocumentSummaryInformation;
		}

		// Token: 0x06001B29 RID: 6953 RVA: 0x00076A9D File Offset: 0x00075A9D
		public virtual SummaryInformation b()
		{
			return this.a.SummaryInformation;
		}

		// Token: 0x06001B2A RID: 6954 RVA: 0x00076AAA File Offset: 0x00075AAA
		public override df im()
		{
			return new f9(this);
		}
	}
}
