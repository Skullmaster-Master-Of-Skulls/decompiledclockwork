using System;
using System.Collections;
using System.Reflection;

namespace a.b
{
	// Token: 0x0200025A RID: 602
	[DefaultMember("Item")]
	internal class dh : DictionaryBase
	{
		// Token: 0x06001495 RID: 5269 RVA: 0x0005FCA8 File Offset: 0x0005ECA8
		public int b(long A_0)
		{
			return (int)base.Dictionary[A_0];
		}

		// Token: 0x06001496 RID: 5270 RVA: 0x0005FCC0 File Offset: 0x0005ECC0
		public void b(long A_0, int A_1)
		{
			base.Dictionary[A_0] = A_1;
		}

		// Token: 0x06001497 RID: 5271 RVA: 0x0005FCD9 File Offset: 0x0005ECD9
		public void a(long A_0, int A_1)
		{
			if (!base.Dictionary.Contains(A_0))
			{
				base.Dictionary.Add(A_0, A_1);
			}
		}

		// Token: 0x06001498 RID: 5272 RVA: 0x0005FD05 File Offset: 0x0005ED05
		public bool a(long A_0)
		{
			return base.Dictionary.Contains(A_0);
		}
	}
}
