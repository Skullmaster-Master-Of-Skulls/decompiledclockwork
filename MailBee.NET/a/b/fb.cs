using System;
using System.Collections;
using System.Reflection;
using MailBee;

namespace a.b
{
	// Token: 0x02000264 RID: 612
	[DefaultMember("Item")]
	internal class fb : DictionaryBase
	{
		// Token: 0x060015F2 RID: 5618 RVA: 0x000629D7 File Offset: 0x000619D7
		public h1 b(int A_0)
		{
			return (h1)base.Dictionary[A_0];
		}

		// Token: 0x060015F3 RID: 5619 RVA: 0x000629EF File Offset: 0x000619EF
		public void b(int A_0, h1 A_1)
		{
			if (A_1 == null)
			{
				throw new MailBeeInvalidArgumentException(21);
			}
			base.Dictionary[A_0] = A_1;
		}

		// Token: 0x060015F4 RID: 5620 RVA: 0x00062A0E File Offset: 0x00061A0E
		public void a(int A_0, h1 A_1)
		{
			if (A_1 == null)
			{
				throw new MailBeeInvalidArgumentException(21);
			}
			base.Dictionary.Add(A_0, A_1);
		}

		// Token: 0x060015F5 RID: 5621 RVA: 0x00062A30 File Offset: 0x00061A30
		public void a(fb A_0)
		{
			if (A_0 == null)
			{
				throw new MailBeeInvalidArgumentException(21);
			}
			foreach (object obj in A_0.a())
			{
				int num = (int)obj;
				if (!base.Dictionary.Contains(num))
				{
					base.Dictionary.Add(num, A_0.b(num));
				}
			}
		}

		// Token: 0x060015F6 RID: 5622 RVA: 0x00062AB8 File Offset: 0x00061AB8
		public bool a(int A_0)
		{
			return base.Dictionary.Contains(A_0);
		}

		// Token: 0x060015F7 RID: 5623 RVA: 0x00062ACB File Offset: 0x00061ACB
		public ICollection a()
		{
			return base.Dictionary.Keys;
		}
	}
}
