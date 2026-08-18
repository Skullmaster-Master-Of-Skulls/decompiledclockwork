using System;
using System.Collections;
using System.Reflection;

namespace a.b
{
	// Token: 0x02000259 RID: 601
	[DefaultMember("Item")]
	internal class hq : DictionaryBase
	{
		// Token: 0x06001490 RID: 5264 RVA: 0x0005FC30 File Offset: 0x0005EC30
		public int b(long A_0)
		{
			return (int)base.Dictionary[A_0];
		}

		// Token: 0x06001491 RID: 5265 RVA: 0x0005FC48 File Offset: 0x0005EC48
		public void b(long A_0, int A_1)
		{
			base.Dictionary[A_0] = A_1;
		}

		// Token: 0x06001492 RID: 5266 RVA: 0x0005FC61 File Offset: 0x0005EC61
		public void a(long A_0, int A_1)
		{
			if (!base.Dictionary.Contains(A_0))
			{
				base.Dictionary.Add(A_0, A_1);
			}
		}

		// Token: 0x06001493 RID: 5267 RVA: 0x0005FC8D File Offset: 0x0005EC8D
		public bool a(long A_0)
		{
			return base.Dictionary.Contains(A_0);
		}
	}
}
