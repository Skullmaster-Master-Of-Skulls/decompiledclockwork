using System;
using System.Collections.Generic;
using System.IO;

namespace a.b
{
	// Token: 0x020002F2 RID: 754
	internal class ib : af, cr
	{
		// Token: 0x06001A99 RID: 6809 RVA: 0x00074E43 File Offset: 0x00073E43
		public ib(y A_0)
		{
			this.c = -2;
			this.a = new List<int>(ib.e);
			this.b = new gx[0];
			this.d = A_0;
		}

		// Token: 0x06001A9A RID: 6810 RVA: 0x00074E78 File Offset: 0x00073E78
		public int a()
		{
			int num = 0;
			int num2 = 0;
			for (;;)
			{
				int num3 = gx.d(this.d, num2 + num + this.a.Count);
				int num4 = ik.a(this.d, num3);
				if (num2 == num3 && num == num4)
				{
					break;
				}
				num2 = num3;
				num = num4;
			}
			int result = this.a(num2);
			this.a(num);
			this.c();
			return result;
		}

		// Token: 0x06001A9B RID: 6811 RVA: 0x00074ED4 File Offset: 0x00073ED4
		public int a(int A_0)
		{
			int count = this.a.Count;
			if (A_0 > 0)
			{
				int num = A_0 - 1;
				int num2 = count + 1;
				for (int i = 0; i < num; i++)
				{
					this.a.Add(num2++);
				}
				this.a.Add(-2);
			}
			return count;
		}

		// Token: 0x06001A9C RID: 6812 RVA: 0x00074F23 File Offset: 0x00073F23
		public int b()
		{
			return this.c;
		}

		// Token: 0x06001A9D RID: 6813 RVA: 0x00074F2B File Offset: 0x00073F2B
		public void jm(int A_0)
		{
			this.c = A_0;
		}

		// Token: 0x06001A9E RID: 6814 RVA: 0x00074F34 File Offset: 0x00073F34
		internal void c()
		{
			this.b = gx.a(this.d, this.a.ToArray());
		}

		// Token: 0x06001A9F RID: 6815 RVA: 0x00074F54 File Offset: 0x00073F54
		public void a3(Stream A_0)
		{
			for (int i = 0; i < this.b.Length; i++)
			{
				this.b[i].a3(A_0);
			}
		}

		// Token: 0x06001AA0 RID: 6816 RVA: 0x00074F82 File Offset: 0x00073F82
		public static void a(gx A_0, he A_1)
		{
			A_0.a(A_1);
		}

		// Token: 0x06001AA1 RID: 6817 RVA: 0x00074F8B File Offset: 0x00073F8B
		public int ap()
		{
			return this.b.Length;
		}

		// Token: 0x040012EA RID: 4842
		private List<int> a;

		// Token: 0x040012EB RID: 4843
		private gx[] b;

		// Token: 0x040012EC RID: 4844
		private int c;

		// Token: 0x040012ED RID: 4845
		private y d;

		// Token: 0x040012EE RID: 4846
		private static int e = 128;
	}
}
