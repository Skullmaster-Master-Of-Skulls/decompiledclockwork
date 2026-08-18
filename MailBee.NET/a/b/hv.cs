using System;
using System.Collections.Generic;
using System.IO;

namespace a.b
{
	// Token: 0x020002FC RID: 764
	internal class hv : o
	{
		// Token: 0x06001AFF RID: 6911 RVA: 0x000762F8 File Offset: 0x000752F8
		protected hv(y A_0, ed[] A_1, int A_2) : base(A_0)
		{
			this.a = new ed[A_0.c()];
			for (int i = 0; i < this.a.Length; i++)
			{
				this.a[i] = A_1[i + A_2];
			}
		}

		// Token: 0x06001B00 RID: 6912 RVA: 0x00076340 File Offset: 0x00075340
		public new static af[] a(y A_0, List<ed> A_1)
		{
			int num = A_0.c();
			int num2 = (A_1.Count + num - 1) / num;
			ed[] array = new ed[num2 * num];
			Array.Copy(A_1.ToArray(), 0, array, 0, A_1.Count);
			for (int i = A_1.Count; i < array.Length; i++)
			{
				array[i] = new hv.a();
			}
			af[] array2 = new af[num2];
			for (int j = 0; j < num2; j++)
			{
				array2[j] = new hv(A_0, array, j * num);
			}
			return array2;
		}

		// Token: 0x06001B01 RID: 6913 RVA: 0x000763C8 File Offset: 0x000753C8
		public override void bc(Stream A_0)
		{
			int num = this.a.c();
			for (int i = 0; i < num; i++)
			{
				this.a[i].a(A_0);
			}
		}

		// Token: 0x04001314 RID: 4884
		private new ed[] a;

		// Token: 0x020002FD RID: 765
		private new class a : ed
		{
			// Token: 0x06001B02 RID: 6914 RVA: 0x000763FB File Offset: 0x000753FB
			public override void lk()
			{
			}

			// Token: 0x06001B03 RID: 6915 RVA: 0x000763FD File Offset: 0x000753FD
			public override bool lj()
			{
				return false;
			}
		}
	}
}
