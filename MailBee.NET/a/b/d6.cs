using System;
using System.Collections;
using System.Reflection;
using MailBee;

namespace a.b
{
	// Token: 0x0200026D RID: 621
	[DefaultMember("Item")]
	internal class d6 : CollectionBase
	{
		// Token: 0x06001659 RID: 5721 RVA: 0x0006504A File Offset: 0x0006404A
		public ii a(int A_0)
		{
			return (ii)base.List[A_0];
		}

		// Token: 0x0600165A RID: 5722 RVA: 0x0006505D File Offset: 0x0006405D
		public void a(int A_0, ii A_1)
		{
			if (A_1 == null)
			{
				throw new MailBeeInvalidArgumentException(21);
			}
			base.List[A_0] = A_1;
		}

		// Token: 0x0600165B RID: 5723 RVA: 0x00065077 File Offset: 0x00064077
		public void a(ii A_0)
		{
			if (A_0 == null)
			{
				throw new MailBeeInvalidArgumentException(21);
			}
			base.List.Add(A_0);
		}
	}
}
