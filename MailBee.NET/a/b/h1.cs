using System;

namespace a.b
{
	// Token: 0x02000265 RID: 613
	internal class h1
	{
		// Token: 0x060015F9 RID: 5625 RVA: 0x00062AE0 File Offset: 0x00061AE0
		public virtual byte[] b()
		{
			if (this.d != null)
			{
				return this.d;
			}
			di di = this.f.c((long)this.b);
			byte[] a_ = new byte[(int)di.Length];
			di.b(a_);
			this.d = a_;
			return this.d;
		}

		// Token: 0x060015FA RID: 5626 RVA: 0x00062B30 File Offset: 0x00061B30
		public virtual int[] a()
		{
			if (this.e != null)
			{
				return this.e;
			}
			long[] array = this.f.c((long)this.b).a();
			int[] array2 = new int[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array2[i] = (int)array[i];
			}
			return array2;
		}

		// Token: 0x060015FB RID: 5627 RVA: 0x00062B83 File Offset: 0x00061B83
		public virtual int c()
		{
			return this.f.b((long)this.b);
		}

		// Token: 0x060015FC RID: 5628 RVA: 0x00062B98 File Offset: 0x00061B98
		internal h1(byte[] A_0, int A_1, bs A_2)
		{
			this.f = A_2;
			if (A_2.f() == 14)
			{
				this.a = (int)ii.b(A_0, A_1, A_1 + 4);
				this.b = ((int)ii.b(A_0, A_1 + 4, A_1 + 8) & -2);
				this.c = ((int)ii.b(A_0, A_1 + 8, A_1 + 12) & -2);
				return;
			}
			this.a = (int)ii.b(A_0, A_1, A_1 + 4);
			this.b = ((int)ii.b(A_0, A_1 + 8, A_1 + 16) & -2);
			this.c = ((int)ii.b(A_0, A_1 + 16, A_1 + 24) & -2);
		}

		// Token: 0x060015FD RID: 5629 RVA: 0x00062C3B File Offset: 0x00061C3B
		public override string ToString()
		{
			return string.Format("PSTDescriptorItem\n   descriptorIdentifier: {0}\n   offsetIndexIdentifier: {1}\n   subNodeOffsetIndexIdentifier: {2}\n", this.a, this.b, this.c);
		}

		// Token: 0x04001074 RID: 4212
		internal int a;

		// Token: 0x04001075 RID: 4213
		internal int b;

		// Token: 0x04001076 RID: 4214
		internal int c;

		// Token: 0x04001077 RID: 4215
		internal byte[] d;

		// Token: 0x04001078 RID: 4216
		internal int[] e;

		// Token: 0x04001079 RID: 4217
		private bs f;
	}
}
