using System;
using System.IO;

namespace a.b
{
	// Token: 0x02000329 RID: 809
	internal class eo
	{
		// Token: 0x06001D2D RID: 7469 RVA: 0x0007E6CC File Offset: 0x0007D6CC
		public static byte[] a(Stream A_0)
		{
			MemoryStream memoryStream = new MemoryStream();
			byte[] array = new byte[4096];
			int num = 0;
			while (num != -1)
			{
				num = A_0.Read(array, 0, array.Length);
				if (num > 0)
				{
					memoryStream.Write(array, 0, num);
				}
			}
			return memoryStream.ToArray();
		}

		// Token: 0x06001D2E RID: 7470 RVA: 0x0007E711 File Offset: 0x0007D711
		public static int a(Stream A_0, byte[] A_1)
		{
			return eo.a(A_0, A_1, 0, A_1.Length);
		}

		// Token: 0x06001D2F RID: 7471 RVA: 0x0007E720 File Offset: 0x0007D720
		public static int a(Stream A_0, byte[] A_1, int A_2, int A_3)
		{
			int num = 0;
			for (;;)
			{
				int num2 = A_0.Read(A_1, A_2 + num, A_3 - num);
				if (num2 == 0)
				{
					break;
				}
				num += num2;
				if (num == A_3)
				{
					return num;
				}
			}
			if (num != 0)
			{
				return num;
			}
			return -1;
		}

		// Token: 0x06001D30 RID: 7472 RVA: 0x0007E750 File Offset: 0x0007D750
		public static void a(Array A_0, int A_1, int A_2, byte A_3)
		{
			if (A_0.Length == 0)
			{
				throw new NullReferenceException();
			}
			if (A_1 > A_2)
			{
				throw new ArgumentException();
			}
			if (A_1 < 0 || A_0.Length < A_2)
			{
				throw new IndexOutOfRangeException();
			}
			for (int i = A_1; i < A_2; i++)
			{
				A_0.SetValue(A_3, i);
			}
		}

		// Token: 0x06001D31 RID: 7473 RVA: 0x0007E7A1 File Offset: 0x0007D7A1
		public static void a(Array A_0, byte A_1)
		{
			eo.a(A_0, 0, A_0.Length, A_1);
		}

		// Token: 0x06001D32 RID: 7474 RVA: 0x0007E7B1 File Offset: 0x0007D7B1
		public static int a(int A_0, int A_1)
		{
			if (A_0 >= 0)
			{
				return A_0 >> A_1;
			}
			return (A_0 >> A_1) + (2 << ~A_1);
		}

		// Token: 0x06001D33 RID: 7475 RVA: 0x0007E7CC File Offset: 0x0007D7CC
		public static int a(int A_0, long A_1)
		{
			return eo.a(A_0, (int)A_1);
		}

		// Token: 0x06001D34 RID: 7476 RVA: 0x0007E7D6 File Offset: 0x0007D7D6
		public static long a(long A_0, int A_1)
		{
			if (A_0 >= 0L)
			{
				return A_0 >> A_1;
			}
			return (A_0 >> A_1) + (2L << ~A_1);
		}

		// Token: 0x06001D35 RID: 7477 RVA: 0x0007E7F3 File Offset: 0x0007D7F3
		public static long a(long A_0, long A_1)
		{
			return eo.a(A_0, (int)A_1);
		}
	}
}
