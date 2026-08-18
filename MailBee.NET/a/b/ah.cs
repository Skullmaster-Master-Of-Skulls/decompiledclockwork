using System;
using System.IO;

namespace a.b
{
	// Token: 0x020002F6 RID: 758
	internal class ah : o
	{
		// Token: 0x06001ABC RID: 6844 RVA: 0x000754A5 File Offset: 0x000744A5
		public ah(i4 A_0) : base((A_0.a() == 512) ? c5.b : c5.d)
		{
			this.b = A_0.bv();
			this.c = this.b.Length;
		}

		// Token: 0x06001ABD RID: 6845 RVA: 0x000754E0 File Offset: 0x000744E0
		public ah(Stream A_0, y A_1) : this(A_1)
		{
			int num = g9.a(A_0, this.b);
			this.c = ((num == -1) ? 0 : num);
		}

		// Token: 0x06001ABE RID: 6846 RVA: 0x0007550F File Offset: 0x0007450F
		public ah(y A_0) : base(A_0)
		{
			this.b = new byte[512];
			d4.a(this.b, ah.a);
		}

		// Token: 0x06001ABF RID: 6847 RVA: 0x00075538 File Offset: 0x00074538
		public int b()
		{
			return this.c;
		}

		// Token: 0x06001AC0 RID: 6848 RVA: 0x00075540 File Offset: 0x00074540
		public bool c()
		{
			return this.c != 512;
		}

		// Token: 0x06001AC1 RID: 6849 RVA: 0x00075552 File Offset: 0x00074552
		public new static byte a()
		{
			return ah.a;
		}

		// Token: 0x06001AC2 RID: 6850 RVA: 0x0007555C File Offset: 0x0007455C
		public new static ah[] a(y A_0, byte[] A_1, int A_2)
		{
			ah[] array = new ah[(A_2 + 512 - 1) / 512];
			int num = 0;
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = new ah(A_0);
				if (num < A_1.Length)
				{
					int num2 = Math.Min(512, A_1.Length - num);
					Array.Copy(A_1, num, array[i].b, 0, num2);
					if (num2 != 512)
					{
						for (int j = (num2 > 0) ? (num2 - 1) : num2; j < 512; j++)
						{
							array[i].b[j] = ah.a;
						}
					}
				}
				else
				{
					for (int k = 0; k < array[i].b.Length; k++)
					{
						array[i].b[k] = ah.a;
					}
				}
				num += 512;
			}
			return array;
		}

		// Token: 0x06001AC3 RID: 6851 RVA: 0x00075630 File Offset: 0x00074630
		public new static void a(ah[] A_0, byte[] A_1, int A_2)
		{
			int num = A_2 / 512;
			int num2 = A_2 % 512;
			int num3 = (A_2 + A_1.Length - 1) / 512;
			if (num == num3)
			{
				Array.Copy(A_0[num].b, num2, A_1, 0, A_1.Length);
				return;
			}
			int num4 = 0;
			Array.Copy(A_0[num].b, num2, A_1, num4, 512 - num2);
			num4 += 512 - num2;
			for (int i = num + 1; i < num3; i++)
			{
				Array.Copy(A_0[i].b, 0, A_1, num4, 512);
				num4 += 512;
			}
			Array.Copy(A_0[num3].b, 0, A_1, num4, A_1.Length - num4);
		}

		// Token: 0x06001AC4 RID: 6852 RVA: 0x000756DC File Offset: 0x000746DC
		public new static fd a(ah[] A_0, int A_1)
		{
			if (A_0 == null || A_0.Length == 0)
			{
				return null;
			}
			y y = A_0[0].a;
			int num = (int)y.d();
			int num2 = y.f() - 1;
			int num3 = A_1 >> num;
			int a_ = A_1 & num2;
			return new fd(A_0[num3].b, a_);
		}

		// Token: 0x06001AC5 RID: 6853 RVA: 0x00075722 File Offset: 0x00074722
		public override void bc(Stream A_0)
		{
			base.a(A_0, this.b);
		}

		// Token: 0x040012F4 RID: 4852
		private new static byte a = byte.MaxValue;

		// Token: 0x040012F5 RID: 4853
		private byte[] b;

		// Token: 0x040012F6 RID: 4854
		private int c;
	}
}
