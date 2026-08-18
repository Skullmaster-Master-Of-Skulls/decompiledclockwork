using System;
using System.Collections;
using System.Reflection;
using MailBee;

namespace a.b
{
	// Token: 0x02000254 RID: 596
	[DefaultMember("Item")]
	internal class h8 : CollectionBase
	{
		// Token: 0x06001479 RID: 5241 RVA: 0x0005FA3D File Offset: 0x0005EA3D
		public dx a(int A_0)
		{
			return (dx)base.List[A_0];
		}

		// Token: 0x0600147A RID: 5242 RVA: 0x0005FA50 File Offset: 0x0005EA50
		public void a(int A_0, dx A_1)
		{
			if (A_1 == null)
			{
				throw new MailBeeInvalidArgumentException(21);
			}
			base.List[A_0] = A_1;
		}

		// Token: 0x0600147B RID: 5243 RVA: 0x0005FA6A File Offset: 0x0005EA6A
		public void a(dx A_0)
		{
			if (A_0 == null)
			{
				throw new MailBeeInvalidArgumentException(21);
			}
			base.List.Add(A_0);
		}
	}
}
