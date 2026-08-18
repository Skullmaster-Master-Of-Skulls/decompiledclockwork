using System;
using System.Text;

namespace a.h
{
	// Token: 0x02000205 RID: 517
	internal class j : d
	{
		// Token: 0x060010EF RID: 4335 RVA: 0x000475CC File Offset: 0x000465CC
		public j(n A_0)
		{
			int num = (int)A_0.f();
			A_0.f();
			int a_ = (int)A_0.f();
			int a_2 = (int)A_0.f();
			this.k = num;
			this.a = A_0.a(a_);
			this.b = A_0.a(a_2);
			int num2 = this.b.IndexOf(':');
			this.c = this.b.Substring(0, num2);
			this.b = this.b.Substring(num2 + 1);
		}

		// Token: 0x060010F0 RID: 4336 RVA: 0x00047651 File Offset: 0x00046651
		public j(int A_0, string A_1, string A_2, string A_3)
		{
			this.k = A_0;
			this.a = A_1;
			this.c = A_2;
			this.b = A_3;
		}

		// Token: 0x060010F1 RID: 4337 RVA: 0x00047676 File Offset: 0x00046676
		public new int a()
		{
			return this.k;
		}

		// Token: 0x060010F2 RID: 4338 RVA: 0x0004767E File Offset: 0x0004667E
		public override string ToString()
		{
			return new StringBuilder().Append(global::a.h.f.a((long)this.a())).Append(": ").Append(base.ToString()).ToString();
		}

		// Token: 0x04000E54 RID: 3668
		public new const int a = 0;

		// Token: 0x04000E55 RID: 3669
		public new const int b = 1;

		// Token: 0x04000E56 RID: 3670
		public new const int c = 2;

		// Token: 0x04000E57 RID: 3671
		public const int d = 3;

		// Token: 0x04000E58 RID: 3672
		public const int e = 4;

		// Token: 0x04000E59 RID: 3673
		public const int f = 5;

		// Token: 0x04000E5A RID: 3674
		public const int g = 6;

		// Token: 0x04000E5B RID: 3675
		public const int h = 7;

		// Token: 0x04000E5C RID: 3676
		public const int i = 8;

		// Token: 0x04000E5D RID: 3677
		public const int j = 9;

		// Token: 0x04000E5E RID: 3678
		private int k;
	}
}
