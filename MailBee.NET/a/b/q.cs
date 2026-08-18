using System;
using System.Collections;
using System.Reflection;

namespace a.b
{
	// Token: 0x02000281 RID: 641
	[DefaultMember("Item")]
	internal class q : DictionaryBase
	{
		// Token: 0x060016D0 RID: 5840 RVA: 0x00068613 File Offset: 0x00067613
		public int b(a4 A_0)
		{
			return (int)base.Dictionary[A_0.ToString()];
		}

		// Token: 0x060016D1 RID: 5841 RVA: 0x0006862B File Offset: 0x0006762B
		public void b(a4 A_0, int A_1)
		{
			base.Dictionary[A_0] = A_1.ToString();
		}

		// Token: 0x060016D2 RID: 5842 RVA: 0x00068640 File Offset: 0x00067640
		public void a(a4 A_0, int A_1)
		{
			if (!base.Dictionary.Contains(A_0.ToString()))
			{
				base.Dictionary.Add(A_0.ToString(), A_1);
			}
		}

		// Token: 0x060016D3 RID: 5843 RVA: 0x0006866C File Offset: 0x0006766C
		public bool a(a4 A_0)
		{
			return base.Dictionary.Contains(A_0.ToString());
		}
	}
}
