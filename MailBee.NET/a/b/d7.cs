using System;

namespace a.b
{
	// Token: 0x020002C2 RID: 706
	internal class d7
	{
		// Token: 0x0600186A RID: 6250 RVA: 0x0006ECE0 File Offset: 0x0006DCE0
		public d7(long A_0, e9 A_1)
		{
			this.b = A_1;
			int num = (int)Math.Ceiling(1.0 * (double)(A_0 / (long)A_1.il()));
			this.a = new bool[num];
		}

		// Token: 0x0600186B RID: 6251 RVA: 0x0006ED21 File Offset: 0x0006DD21
		public void a(int A_0)
		{
			if (A_0 >= this.a.Length)
			{
				return;
			}
			if (this.a[A_0])
			{
				throw new InvalidOperationException("Potential loop detected - Block " + A_0 + " was already claimed but was just requested again");
			}
			this.a[A_0] = true;
		}

		// Token: 0x04001235 RID: 4661
		private bool[] a;

		// Token: 0x04001236 RID: 4662
		private e9 b;
	}
}
