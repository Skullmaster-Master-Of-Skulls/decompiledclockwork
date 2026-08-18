using System;
using System.Collections;
using System.Reflection;
using MailBee;

namespace a.b
{
	// Token: 0x0200027A RID: 634
	[DefaultMember("Item")]
	internal class gs : DictionaryBase
	{
		// Token: 0x06001699 RID: 5785 RVA: 0x00067B2B File Offset: 0x00066B2B
		public e2 b(int A_0)
		{
			return (e2)base.Dictionary[A_0];
		}

		// Token: 0x0600169A RID: 5786 RVA: 0x00067B43 File Offset: 0x00066B43
		public void b(int A_0, e2 A_1)
		{
			if (A_1 == null)
			{
				throw new MailBeeInvalidArgumentException(21);
			}
			base.Dictionary[A_0] = A_1;
		}

		// Token: 0x0600169B RID: 5787 RVA: 0x00067B62 File Offset: 0x00066B62
		public void a(int A_0, e2 A_1)
		{
			base.Dictionary.Add(A_0, A_1);
		}

		// Token: 0x0600169C RID: 5788 RVA: 0x00067B76 File Offset: 0x00066B76
		public bool a(int A_0)
		{
			return base.Dictionary.Contains(A_0);
		}
	}
}
