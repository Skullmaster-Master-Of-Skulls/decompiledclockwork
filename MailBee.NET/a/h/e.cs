using System;
using System.Text;

namespace a.h
{
	// Token: 0x02000202 RID: 514
	internal class e
	{
		// Token: 0x060010BB RID: 4283 RVA: 0x00046A7B File Offset: 0x00045A7B
		public int d()
		{
			return this.g;
		}

		// Token: 0x060010BC RID: 4284 RVA: 0x00046A83 File Offset: 0x00045A83
		public long a()
		{
			return this.h;
		}

		// Token: 0x060010BD RID: 4285 RVA: 0x00046A8B File Offset: 0x00045A8B
		public int b()
		{
			return this.i;
		}

		// Token: 0x060010BE RID: 4286 RVA: 0x00046A93 File Offset: 0x00045A93
		public int e()
		{
			return this.j;
		}

		// Token: 0x060010BF RID: 4287 RVA: 0x00046A9B File Offset: 0x00045A9B
		public long c()
		{
			return this.k;
		}

		// Token: 0x060010C0 RID: 4288 RVA: 0x00046AA4 File Offset: 0x00045AA4
		public e(n A_0)
		{
			this.g = (int)A_0.f();
			this.h = (long)A_0.e();
			this.i = (int)A_0.f();
			this.j = (int)A_0.f();
			this.k = (long)((ulong)A_0.e());
		}

		// Token: 0x060010C1 RID: 4289 RVA: 0x00046AF5 File Offset: 0x00045AF5
		public e(int A_0, long A_1, int A_2, int A_3, long A_4)
		{
			this.g = A_0;
			this.h = A_1;
			this.i = A_2;
			this.j = A_3;
			this.k = A_4;
		}

		// Token: 0x060010C2 RID: 4290 RVA: 0x00046B24 File Offset: 0x00045B24
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("RendData:").Append(" getType=").Append(this.d()).Append(" getPosition=").Append(this.a()).Append(" getWidth=").Append(this.b()).Append(" getHeight=").Append(this.e()).Append(" getFlags=").Append(this.c());
			return stringBuilder.ToString();
		}

		// Token: 0x04000E43 RID: 3651
		public const int a = 1;

		// Token: 0x04000E44 RID: 3652
		public const int b = 0;

		// Token: 0x04000E45 RID: 3653
		public const int c = 1;

		// Token: 0x04000E46 RID: 3654
		public const int d = 2;

		// Token: 0x04000E47 RID: 3655
		public const int e = 3;

		// Token: 0x04000E48 RID: 3656
		public const int f = 4;

		// Token: 0x04000E49 RID: 3657
		internal int g;

		// Token: 0x04000E4A RID: 3658
		internal long h;

		// Token: 0x04000E4B RID: 3659
		internal int i;

		// Token: 0x04000E4C RID: 3660
		internal int j;

		// Token: 0x04000E4D RID: 3661
		internal long k;
	}
}
