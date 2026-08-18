using System;
using MailBee.Outlook;

namespace a.b
{
	// Token: 0x02000290 RID: 656
	internal abstract class df
	{
		// Token: 0x06001717 RID: 5911 RVA: 0x000697A7 File Offset: 0x000687A7
		public df(POIDocument A_0)
		{
			this.a = A_0;
		}

		// Token: 0x06001718 RID: 5912 RVA: 0x000697B6 File Offset: 0x000687B6
		protected df(df A_0)
		{
			this.a = A_0.a;
		}

		// Token: 0x06001719 RID: 5913
		public abstract string k5();

		// Token: 0x0600171A RID: 5914
		public abstract df im();

		// Token: 0x04001145 RID: 4421
		protected POIDocument a;
	}
}
