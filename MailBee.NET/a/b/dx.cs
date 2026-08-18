using System;

namespace a.b
{
	// Token: 0x02000253 RID: 595
	internal class dx
	{
		// Token: 0x06001476 RID: 5238 RVA: 0x0005F8C4 File Offset: 0x0005E8C4
		internal dx(byte[] A_0, int A_1)
		{
			if (A_1 == 14)
			{
				this.a = (int)ii.b(A_0, 0, 4);
				this.b = (long)((int)ii.b(A_0, 4, 8));
				this.c = (long)((int)ii.b(A_0, 8, 12));
				this.d = (int)ii.b(A_0, 12, 16);
				return;
			}
			this.a = (int)ii.b(A_0, 0, 4);
			this.b = (long)((int)ii.b(A_0, 8, 16));
			this.c = (long)((int)ii.b(A_0, 16, 24));
			this.d = (int)ii.b(A_0, 24, 28);
			this.e = (int)ii.b(A_0, 28, 32);
		}

		// Token: 0x06001477 RID: 5239 RVA: 0x0005F972 File Offset: 0x0005E972
		internal virtual di f(bs A_0)
		{
			return new di(A_0, A_0.e(this.b));
		}

		// Token: 0x06001478 RID: 5240 RVA: 0x0005F988 File Offset: 0x0005E988
		public override string ToString()
		{
			return string.Format("DescriptorIndexNode\nDescriptor Identifier: {0} (0x{1})\nData offset identifier: {2} (0x{3})\nLocal descriptors offset index identifier: {4} (0x{5})\nParent Descriptor Index Identifier: {6} (0x{7})\nItem Type: {8} (0x{9})", new object[]
			{
				this.a,
				Convert.ToString(this.a, 16),
				this.b,
				Convert.ToString(this.b, 16),
				this.c,
				Convert.ToString(this.c, 16),
				this.d,
				Convert.ToString(this.d, 16),
				this.e,
				Convert.ToString(this.e, 16)
			});
		}

		// Token: 0x0400103F RID: 4159
		public int a;

		// Token: 0x04001040 RID: 4160
		public long b;

		// Token: 0x04001041 RID: 4161
		public long c;

		// Token: 0x04001042 RID: 4162
		public int d;

		// Token: 0x04001043 RID: 4163
		public int e;
	}
}
