using System;
using System.IO;

namespace a.b
{
	// Token: 0x0200031B RID: 795
	internal class g9
	{
		// Token: 0x06001C70 RID: 7280 RVA: 0x0007C8FC File Offset: 0x0007B8FC
		public static byte[] a(Stream A_0)
		{
			byte[] array = new byte[A_0.Length];
			A_0.Read(array, 0, (int)A_0.Length);
			return array;
		}

		// Token: 0x06001C71 RID: 7281 RVA: 0x0007C928 File Offset: 0x0007B928
		public static byte[] a(he A_0, int A_1)
		{
			if (A_0.k() && A_0.e() == 0)
			{
				return A_0.a();
			}
			byte[] array = new byte[A_1];
			A_0.c(array);
			return array;
		}

		// Token: 0x06001C72 RID: 7282 RVA: 0x0007C95C File Offset: 0x0007B95C
		public static int a(Stream A_0, byte[] A_1)
		{
			return g9.a(A_0, A_1, 0, A_1.Length);
		}

		// Token: 0x06001C73 RID: 7283 RVA: 0x0007C96C File Offset: 0x0007B96C
		public static int a(Stream A_0, byte[] A_1, int A_2, int A_3)
		{
			int num = 0;
			for (;;)
			{
				int num2 = A_0.Read(A_1, A_2 + num, A_3 - num - A_2);
				num += num2;
				if (A_0.Position == A_0.Length)
				{
					break;
				}
				if (num == A_3)
				{
					return num;
				}
			}
			return num;
		}

		// Token: 0x06001C74 RID: 7284 RVA: 0x0007C9A4 File Offset: 0x0007B9A4
		public static void a(Stream A_0, Stream A_1)
		{
			byte[] array = new byte[4096];
			A_0.Position = 0L;
			int num;
			while ((num = A_0.Read(array, 0, array.Length)) != -1)
			{
				if (num > 0)
				{
					A_1.Write(array, 0, num);
				}
			}
		}

		// Token: 0x06001C75 RID: 7285 RVA: 0x0007C9E3 File Offset: 0x0007B9E3
		public static long a(byte[] A_0)
		{
			return (long)new bx().a(ref A_0);
		}
	}
}
