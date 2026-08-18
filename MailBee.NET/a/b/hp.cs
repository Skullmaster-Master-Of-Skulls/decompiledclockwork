using System;

namespace a.b
{
	// Token: 0x0200025C RID: 604
	internal class hp
	{
		// Token: 0x0600149C RID: 5276 RVA: 0x0005FF40 File Offset: 0x0005EF40
		internal hp(byte[] A_0, int A_1)
		{
			if (A_1 == 14)
			{
				this.a = ii.b(A_0, 0, 4);
				this.b = ii.b(A_0, 4, 8);
				this.c = (int)ii.b(A_0, 8, 10);
				this.d = (long)((int)ii.b(A_0, 10, 12));
				return;
			}
			this.a = ii.b(A_0, 0, 8);
			this.b = ii.b(A_0, 8, 16);
			this.c = (int)ii.b(A_0, 16, 18);
			this.d = (long)((int)ii.b(A_0, 16, 18));
		}

		// Token: 0x0600149D RID: 5277 RVA: 0x0005FFD8 File Offset: 0x0005EFD8
		public override string ToString()
		{
			return string.Format("OffsetIndexItem\nIndex Identifier: {0} (0x{1})\nFile Offset: {2} (0x{3})\ncRef: {4} (0x{5} bin:{6})\nSize: {7} (0x{8})", new object[]
			{
				this.a,
				Convert.ToString(this.a, 16),
				this.b,
				Convert.ToString(this.b, 16),
				this.d,
				Convert.ToString(this.d, 16),
				Convert.ToString(this.d, 2),
				this.c,
				Convert.ToString(this.c, 16)
			});
		}

		// Token: 0x04001045 RID: 4165
		internal long a;

		// Token: 0x04001046 RID: 4166
		internal long b;

		// Token: 0x04001047 RID: 4167
		internal int c;

		// Token: 0x04001048 RID: 4168
		internal long d;
	}
}
