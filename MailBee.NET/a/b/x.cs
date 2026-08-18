using System;
using System.Collections;
using System.Reflection;
using MailBee;

namespace a.b
{
	// Token: 0x0200026A RID: 618
	[DefaultMember("Item")]
	internal class x : CollectionBase
	{
		// Token: 0x06001636 RID: 5686 RVA: 0x000646D7 File Offset: 0x000636D7
		public bj a(int A_0)
		{
			return (bj)base.List[A_0];
		}

		// Token: 0x06001637 RID: 5687 RVA: 0x000646EA File Offset: 0x000636EA
		public void a(int A_0, bj A_1)
		{
			if (A_1 == null)
			{
				throw new MailBeeInvalidArgumentException(21);
			}
			base.List[A_0] = A_1;
		}

		// Token: 0x06001638 RID: 5688 RVA: 0x00064704 File Offset: 0x00063704
		public void a(bj A_0)
		{
			if (A_0 == null)
			{
				throw new MailBeeInvalidArgumentException(21);
			}
			base.List.Add(A_0);
		}
	}
}
