using System;
using System.Collections;
using System.Reflection;
using MailBee;

namespace a.b
{
	// Token: 0x0200025D RID: 605
	[DefaultMember("Item")]
	internal class i : CollectionBase
	{
		// Token: 0x0600149E RID: 5278 RVA: 0x0006007D File Offset: 0x0005F07D
		public hp a(int A_0)
		{
			return (hp)base.List[A_0];
		}

		// Token: 0x0600149F RID: 5279 RVA: 0x00060090 File Offset: 0x0005F090
		public void a(int A_0, hp A_1)
		{
			if (A_1 == null)
			{
				throw new MailBeeInvalidArgumentException(21);
			}
			base.List[A_0] = A_1;
		}

		// Token: 0x060014A0 RID: 5280 RVA: 0x000600AA File Offset: 0x0005F0AA
		public void a(hp A_0)
		{
			if (A_0 == null)
			{
				throw new MailBeeInvalidArgumentException(21);
			}
			base.List.Add(A_0);
		}
	}
}
