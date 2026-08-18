using System;
using System.Collections;

namespace a.b
{
	// Token: 0x020002A1 RID: 673
	internal class a8
	{
		// Token: 0x0600179E RID: 6046 RVA: 0x0006BADC File Offset: 0x0006AADC
		public static void a(byte[] A_0, int A_1, int A_2, byte[] A_3, int A_4)
		{
			for (int i = 0; i < A_2; i++)
			{
				A_3[A_4 + i] = A_0[A_1 + i];
			}
		}

		// Token: 0x0600179F RID: 6047 RVA: 0x0006BB00 File Offset: 0x0006AB00
		public static byte[] a(byte[][] A_0)
		{
			int num = 0;
			for (int i = 0; i < A_0.Length; i++)
			{
				num += A_0[i].Length;
			}
			byte[] array = new byte[num];
			int num2 = 0;
			for (int j = 0; j < A_0.Length; j++)
			{
				for (int k = 0; k < A_0[j].Length; k++)
				{
					array[num2++] = A_0[j][k];
				}
			}
			return array;
		}

		// Token: 0x060017A0 RID: 6048 RVA: 0x0006BB64 File Offset: 0x0006AB64
		public static byte[] a(byte[] A_0, int A_1, int A_2)
		{
			byte[] array = new byte[A_2];
			a8.a(A_0, A_1, A_2, array, 0);
			return array;
		}

		// Token: 0x060017A1 RID: 6049 RVA: 0x0006BB83 File Offset: 0x0006AB83
		public static DateTime a(int A_0, int A_1)
		{
			return a8.a((long)A_0 << 32 | ((long)A_1 & (long)((ulong)-1)));
		}

		// Token: 0x060017A2 RID: 6050 RVA: 0x0006BB95 File Offset: 0x0006AB95
		public static DateTime a(long A_0)
		{
			return DateTime.FromFileTime(A_0);
		}

		// Token: 0x060017A3 RID: 6051 RVA: 0x0006BB9D File Offset: 0x0006AB9D
		public static long a(DateTime A_0)
		{
			return A_0.ToFileTime();
		}

		// Token: 0x060017A4 RID: 6052 RVA: 0x0006BBA6 File Offset: 0x0006ABA6
		public static bool b(IList A_0, IList A_1)
		{
			return a8.a(A_0, A_1);
		}

		// Token: 0x060017A5 RID: 6053 RVA: 0x0006BBB0 File Offset: 0x0006ABB0
		private static bool a(IList A_0, IList A_1)
		{
			foreach (object obj in A_0)
			{
				bool flag = false;
				IEnumerator enumerator2 = A_1.GetEnumerator();
				while (!flag && enumerator2.MoveNext())
				{
					object obj2 = enumerator2.Current;
					if (obj.Equals(obj2))
					{
						flag = true;
					}
				}
				if (!flag)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060017A6 RID: 6054 RVA: 0x0006BC08 File Offset: 0x0006AC08
		public static byte[] a(byte[] A_0)
		{
			int num = 4;
			int num2 = A_0.Length % num;
			byte[] array;
			if (num2 == 0)
			{
				array = A_0;
			}
			else
			{
				num2 = num - num2;
				array = new byte[A_0.Length + num2];
				Array.Copy(A_0, array, A_0.Length);
			}
			return array;
		}

		// Token: 0x060017A7 RID: 6055 RVA: 0x0006BC40 File Offset: 0x0006AC40
		public static char[] a(char[] A_0)
		{
			int num = 4;
			int num2 = A_0.Length % num;
			char[] array;
			if (num2 == 0)
			{
				array = A_0;
			}
			else
			{
				num2 = num - num2;
				array = new char[A_0.Length + num2];
				Array.Copy(A_0, array, A_0.Length);
			}
			return array;
		}

		// Token: 0x060017A8 RID: 6056 RVA: 0x0006BC76 File Offset: 0x0006AC76
		public static char[] a(string A_0)
		{
			return a8.a(A_0.ToCharArray());
		}

		// Token: 0x04001172 RID: 4466
		public static readonly long a = new DateTime(1970, 1, 1).Ticks;
	}
}
