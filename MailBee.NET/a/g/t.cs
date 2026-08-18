using System;
using MailBee;

namespace a.g
{
	// Token: 0x020003F0 RID: 1008
	internal class t : MailBeeException
	{
		// Token: 0x060023C2 RID: 9154 RVA: 0x00096554 File Offset: 0x00095554
		public t(int A_0, short A_1, short A_2) : base(A_0)
		{
			this.a = A_2;
			this.b = A_1;
		}

		// Token: 0x060023C3 RID: 9155 RVA: 0x0009656B File Offset: 0x0009556B
		public short b()
		{
			return this.a;
		}

		// Token: 0x060023C4 RID: 9156 RVA: 0x00096573 File Offset: 0x00095573
		public short a()
		{
			return this.b;
		}

		// Token: 0x040017A2 RID: 6050
		private short a;

		// Token: 0x040017A3 RID: 6051
		private short b;
	}
}
