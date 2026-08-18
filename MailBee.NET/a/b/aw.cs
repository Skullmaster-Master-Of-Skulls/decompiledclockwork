using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace a.b
{
	// Token: 0x02000302 RID: 770
	internal class aw : af, bn
	{
		// Token: 0x06001B16 RID: 6934 RVA: 0x000766F0 File Offset: 0x000756F0
		public aw(y A_0, byte[] A_1, int A_2)
		{
			this.g = A_0;
			aw.f = aw.b(A_0);
			this.b = new byte[64];
			Array.Copy(A_1, A_2 * 64, this.b, 0, 64);
		}

		// Token: 0x06001B17 RID: 6935 RVA: 0x0007672A File Offset: 0x0007572A
		public aw(y A_0)
		{
			this.g = A_0;
			aw.f = aw.b(A_0);
			this.b = new byte[64];
		}

		// Token: 0x06001B18 RID: 6936 RVA: 0x00076751 File Offset: 0x00075751
		private static int b(y A_0)
		{
			return A_0.f() / 64;
		}

		// Token: 0x06001B19 RID: 6937 RVA: 0x0007675C File Offset: 0x0007575C
		public static aw[] a(y A_0, byte[] A_1, int A_2)
		{
			aw[] array = new aw[(A_2 + 64 - 1) / 64];
			int num = 0;
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = new aw(A_0);
				if (num < A_1.Length)
				{
					int num2 = Math.Min(64, A_1.Length - num);
					Array.Copy(A_1, num, array[i].b, 0, num2);
					if (num2 != 64)
					{
						for (int j = num2; j < 64; j++)
						{
							array[i].b[j] = byte.MaxValue;
						}
					}
				}
				else
				{
					for (int k = 0; k < array[i].b.Length; k++)
					{
						array[i].b[k] = byte.MaxValue;
					}
				}
				num += 64;
			}
			return array;
		}

		// Token: 0x06001B1A RID: 6938 RVA: 0x00076814 File Offset: 0x00075814
		public static int a(y A_0, IList A_1)
		{
			int num = aw.b(A_0);
			int i = A_1.Count;
			int num2 = (i + num - 1) / num;
			int num3 = num2 * num;
			while (i < num3)
			{
				A_1.Add(aw.a(A_0));
				i++;
			}
			return num2;
		}

		// Token: 0x06001B1B RID: 6939 RVA: 0x00076854 File Offset: 0x00075854
		public static aw[] a(y A_0, af[] A_1, int A_2)
		{
			aw[] result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				for (int i = 0; i < A_1.Length; i++)
				{
					A_1[i].a3(memoryStream);
				}
				byte[] a_ = memoryStream.ToArray();
				aw[] array = new aw[aw.a(A_2)];
				for (int j = 0; j < array.Length; j++)
				{
					array[j] = new aw(A_0, a_, j);
				}
				result = array;
			}
			return result;
		}

		// Token: 0x06001B1C RID: 6940 RVA: 0x000768D4 File Offset: 0x000758D4
		public static List<aw> a(y A_0, bn[] A_1)
		{
			int num = aw.b(A_0);
			List<aw> list = new List<aw>();
			for (int i = 0; i < A_1.Length; i++)
			{
				byte[] a_ = A_1[i].bv();
				for (int j = 0; j < num; j++)
				{
					list.Add(new aw(A_0, a_, j));
				}
			}
			return list;
		}

		// Token: 0x06001B1D RID: 6941 RVA: 0x00076928 File Offset: 0x00075928
		public static void a(af[] A_0, byte[] A_1, int A_2)
		{
			int num = A_2 / 64;
			int num2 = A_2 % 64;
			int num3 = (A_2 + A_1.Length - 1) / 64;
			if (num == num3)
			{
				Array.Copy(((aw)A_0[num]).b, num2, A_1, 0, A_1.Length);
				return;
			}
			int num4 = 0;
			Array.Copy(((aw)A_0[num]).b, num2, A_1, num4, 64 - num2);
			num4 += 64 - num2;
			for (int i = num + 1; i < num3; i++)
			{
				Array.Copy(((aw)A_0[i]).b, 0, A_1, num4, 64);
				num4 += 64;
			}
			Array.Copy(((aw)A_0[num3]).b, 0, A_1, num4, A_1.Length - num4);
		}

		// Token: 0x06001B1E RID: 6942 RVA: 0x000769D4 File Offset: 0x000759D4
		public static fd a(aw[] A_0, int A_1)
		{
			int num = A_1 >> 6;
			int a_ = A_1 & 63;
			if (A_0.Length == 0)
			{
				return null;
			}
			return new fd(A_0[num].b, a_);
		}

		// Token: 0x06001B1F RID: 6943 RVA: 0x000769FE File Offset: 0x000759FE
		public static int b(int A_0)
		{
			return A_0 * 64;
		}

		// Token: 0x06001B20 RID: 6944 RVA: 0x00076A04 File Offset: 0x00075A04
		private static aw a(y A_0)
		{
			aw aw = new aw(A_0);
			for (int i = 0; i < aw.b.Length; i++)
			{
				aw.b[i] = byte.MaxValue;
			}
			return aw;
		}

		// Token: 0x06001B21 RID: 6945 RVA: 0x00076A39 File Offset: 0x00075A39
		private static int a(int A_0)
		{
			return (A_0 + 64 - 1) / 64;
		}

		// Token: 0x06001B22 RID: 6946 RVA: 0x00076A44 File Offset: 0x00075A44
		public void a3(Stream A_0)
		{
			A_0.Write(this.b, 0, this.b.Length);
		}

		// Token: 0x06001B23 RID: 6947 RVA: 0x00076A5B File Offset: 0x00075A5B
		public byte[] bv()
		{
			return this.b;
		}

		// Token: 0x06001B24 RID: 6948 RVA: 0x00076A63 File Offset: 0x00075A63
		public y a()
		{
			return this.g;
		}

		// Token: 0x0400131D RID: 4893
		private const int a = 6;

		// Token: 0x0400131E RID: 4894
		private byte[] b;

		// Token: 0x0400131F RID: 4895
		private const byte c = 255;

		// Token: 0x04001320 RID: 4896
		private const int d = 64;

		// Token: 0x04001321 RID: 4897
		private const int e = 63;

		// Token: 0x04001322 RID: 4898
		private static int f = 8;

		// Token: 0x04001323 RID: 4899
		private y g;
	}
}
