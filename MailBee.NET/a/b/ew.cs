using System;
using System.Collections;
using System.Reflection;
using MailBee;

namespace a.b
{
	// Token: 0x02000275 RID: 629
	[DefaultMember("Item")]
	internal class ew : DictionaryBase
	{
		// Token: 0x06001685 RID: 5765 RVA: 0x00067424 File Offset: 0x00066424
		public bh b(int A_0)
		{
			return (bh)base.Dictionary[A_0];
		}

		// Token: 0x06001686 RID: 5766 RVA: 0x0006743C File Offset: 0x0006643C
		public void b(int A_0, bh A_1)
		{
			if (A_1 == null)
			{
				throw new MailBeeInvalidArgumentException(21);
			}
			base.Dictionary[A_0] = A_1;
		}

		// Token: 0x06001687 RID: 5767 RVA: 0x0006745B File Offset: 0x0006645B
		public void a(int A_0, bh A_1)
		{
			if (!base.Dictionary.Contains(A_0))
			{
				base.Dictionary.Add(A_0, A_1);
			}
		}

		// Token: 0x06001688 RID: 5768 RVA: 0x00067482 File Offset: 0x00066482
		public bool a(int A_0)
		{
			return base.Dictionary.Contains(A_0);
		}
	}
}
