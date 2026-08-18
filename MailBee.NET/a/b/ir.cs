using System;
using System.Collections;
using System.Reflection;

namespace a.b
{
	// Token: 0x02000257 RID: 599
	[DefaultMember("Item")]
	internal class ir : DictionaryBase
	{
		// Token: 0x06001487 RID: 5255 RVA: 0x0005FB75 File Offset: 0x0005EB75
		public long b(int A_0)
		{
			return (long)base.Dictionary[A_0];
		}

		// Token: 0x06001488 RID: 5256 RVA: 0x0005FB8D File Offset: 0x0005EB8D
		public void b(int A_0, long A_1)
		{
			base.Dictionary[A_0] = A_1;
		}

		// Token: 0x06001489 RID: 5257 RVA: 0x0005FBA6 File Offset: 0x0005EBA6
		public void a(int A_0, long A_1)
		{
			if (!base.Dictionary.Contains(A_0))
			{
				base.Dictionary.Add(A_0, A_1);
			}
		}

		// Token: 0x0600148A RID: 5258 RVA: 0x0005FBD2 File Offset: 0x0005EBD2
		public bool a(int A_0)
		{
			return base.Dictionary.Contains(A_0);
		}
	}
}
