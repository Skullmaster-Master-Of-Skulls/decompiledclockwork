using System;
using System.Collections;
using System.Reflection;
using MailBee;

namespace a.b
{
	// Token: 0x02000255 RID: 597
	[DefaultMember("Item")]
	internal class dl : DictionaryBase
	{
		// Token: 0x0600147D RID: 5245 RVA: 0x0005FA8C File Offset: 0x0005EA8C
		public h8 b(int A_0)
		{
			return (h8)base.Dictionary[A_0];
		}

		// Token: 0x0600147E RID: 5246 RVA: 0x0005FAA4 File Offset: 0x0005EAA4
		public void b(int A_0, h8 A_1)
		{
			if (A_1 == null)
			{
				throw new MailBeeInvalidArgumentException(21);
			}
			base.Dictionary[A_0] = A_1;
		}

		// Token: 0x0600147F RID: 5247 RVA: 0x0005FAC3 File Offset: 0x0005EAC3
		public void a(int A_0, h8 A_1)
		{
			if (A_1 == null)
			{
				throw new MailBeeInvalidArgumentException(21);
			}
			base.Dictionary.Add(A_0, A_1);
		}

		// Token: 0x06001480 RID: 5248 RVA: 0x0005FAE2 File Offset: 0x0005EAE2
		public bool a(int A_0)
		{
			return base.Dictionary.Contains(A_0);
		}
	}
}
