using System;
using System.Collections;
using System.Reflection;

namespace a.b
{
	// Token: 0x02000256 RID: 598
	[DefaultMember("Item")]
	internal class ec : DictionaryBase
	{
		// Token: 0x06001482 RID: 5250 RVA: 0x0005FAFD File Offset: 0x0005EAFD
		public int b(int A_0)
		{
			return (int)base.Dictionary[A_0];
		}

		// Token: 0x06001483 RID: 5251 RVA: 0x0005FB15 File Offset: 0x0005EB15
		public void b(int A_0, int A_1)
		{
			base.Dictionary[A_0] = A_1;
		}

		// Token: 0x06001484 RID: 5252 RVA: 0x0005FB2E File Offset: 0x0005EB2E
		public void a(int A_0, int A_1)
		{
			if (!base.Dictionary.Contains(A_0))
			{
				base.Dictionary.Add(A_0, A_1);
			}
		}

		// Token: 0x06001485 RID: 5253 RVA: 0x0005FB5A File Offset: 0x0005EB5A
		public bool a(int A_0)
		{
			return base.Dictionary.Contains(A_0);
		}
	}
}
