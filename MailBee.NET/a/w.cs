using System;
using System.Collections;
using System.Text;

namespace a
{
	// Token: 0x020004F4 RID: 1268
	internal class w
	{
		// Token: 0x06002A32 RID: 10802 RVA: 0x000C5EEE File Offset: 0x000C4EEE
		private w()
		{
		}

		// Token: 0x06002A33 RID: 10803 RVA: 0x000C5EF8 File Offset: 0x000C4EF8
		public static string b(byte[] A_0)
		{
			for (int i = A_0.Length - 1; i > -1; i--)
			{
				if (A_0[i] != 0)
				{
					return Convert.ToBase64String(A_0, 0, i + 1);
				}
			}
			return string.Empty;
		}

		// Token: 0x06002A34 RID: 10804 RVA: 0x000C5F2A File Offset: 0x000C4F2A
		public static byte[] c(byte[] A_0, int A_1, int A_2)
		{
			return w.b(A_0, A_1, A_2, w.a, w.b);
		}

		// Token: 0x06002A35 RID: 10805 RVA: 0x000C5F40 File Offset: 0x000C4F40
		public static ao a(ao A_0, int A_1, int A_2)
		{
			byte[] array = A_0.d();
			int num = A_1;
			int num2 = 0;
			int num3 = A_1;
			while (num2 < A_1 + A_2 && (num2 = w.b(array, num, A_2 - num, w.b)) > -1)
			{
				Buffer.BlockCopy(array, num, array, num3, num2 - num + 3);
				num3 += num2 - num + 3;
				num = num2 + 4;
			}
			if (num < A_1 + A_2)
			{
				Buffer.BlockCopy(array, num, array, num3, A_1 + A_2 - num);
				num3 += A_1 + A_2 - num;
			}
			return new ao(A_0, A_1, num3 - A_1);
		}

		// Token: 0x06002A36 RID: 10806 RVA: 0x000C5FB8 File Offset: 0x000C4FB8
		public static byte[] b(byte[] A_0, int A_1, int A_2, bool A_3)
		{
			if (A_0 == null)
			{
				throw new ArgumentNullException();
			}
			int num = A_2;
			if (num > A_0.Length)
			{
				if (num < A_0.Length * 2)
				{
					num = A_0.Length * 2;
				}
				byte[] array = new byte[num];
				if (A_3 && A_1 > 0)
				{
					Buffer.BlockCopy(A_0, 0, array, 0, A_1);
				}
				return array;
			}
			return A_0;
		}

		// Token: 0x06002A37 RID: 10807 RVA: 0x000C6000 File Offset: 0x000C5000
		public static byte[] a(byte[] A_0, int A_1, int A_2, bool A_3)
		{
			if (A_0 == null)
			{
				throw new ArgumentNullException();
			}
			if (A_0.Length == 0)
			{
				throw new ArgumentException();
			}
			if (A_2 < A_0.Length / 2 && (!A_3 || A_2 >= A_1 * 2))
			{
				byte[] array = new byte[A_2];
				if (A_3 && A_1 > 0)
				{
					Buffer.BlockCopy(A_0, 0, array, 0, A_1);
				}
				return array;
			}
			return A_0;
		}

		// Token: 0x06002A38 RID: 10808 RVA: 0x000C604C File Offset: 0x000C504C
		public static int b(byte[] A_0, int A_1, int A_2, byte[] A_3)
		{
			if (A_3.Length < 1)
			{
				return A_1;
			}
			int num = A_1 + A_2 - A_3.Length + 1;
			int num2 = A_1;
			IL_34:
			while (num - num2 >= 0 && (num2 = Array.IndexOf<byte>(A_0, A_3[0], num2, num - num2)) > -1)
			{
				for (int i = 1; i < A_3.Length; i++)
				{
					if (A_0[num2 + i] != A_3[i])
					{
						num2++;
						goto IL_34;
					}
				}
				return num2;
			}
			return -1;
		}

		// Token: 0x06002A39 RID: 10809 RVA: 0x000C60A8 File Offset: 0x000C50A8
		public static int b(byte[] A_0, int A_1, int A_2)
		{
			int num = A_1 + A_2 - 1;
			int num2 = A_1;
			while (num - num2 >= 0 && (num2 = Array.IndexOf<byte>(A_0, 13, num2, num - num2)) > -1)
			{
				if (A_0[num2 + 1] == 10)
				{
					return num2;
				}
				num2++;
			}
			return -1;
		}

		// Token: 0x06002A3A RID: 10810 RVA: 0x000C60E8 File Offset: 0x000C50E8
		public static bool a(byte[] A_0, int A_1, int A_2)
		{
			for (int i = A_1; i < A_1 + A_2; i++)
			{
				if (A_0[i] > 127)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002A3B RID: 10811 RVA: 0x000C610D File Offset: 0x000C510D
		public static bool a(byte[] A_0)
		{
			return w.a(A_0, 0, A_0.Length);
		}

		// Token: 0x06002A3C RID: 10812 RVA: 0x000C611C File Offset: 0x000C511C
		public static ArrayList a(byte[] A_0, int A_1, int A_2, byte[] A_3)
		{
			ArrayList arrayList = new ArrayList(A_2 / 10000);
			int num = A_1;
			while (num < A_1 + A_2 && (num = w.b(A_0, num, A_2 - (num - A_1), A_3)) > -1)
			{
				arrayList.Add(num);
				num += A_3.Length;
			}
			return arrayList;
		}

		// Token: 0x06002A3D RID: 10813 RVA: 0x000C6168 File Offset: 0x000C5168
		public static byte[] b(byte[] A_0, int A_1, int A_2, byte[] A_3, byte[] A_4)
		{
			if (A_3.Length == 0)
			{
				if (A_1 == 0 && A_2 == A_0.Length)
				{
					return A_0;
				}
				byte[] array = new byte[A_2];
				Buffer.BlockCopy(A_0, A_1, array, 0, A_2);
				return array;
			}
			else
			{
				ArrayList arrayList = new ArrayList(A_2 / 10000);
				int num = A_1;
				while (num < A_1 + A_2 && (num = w.b(A_0, num, A_2 - (num - A_1), A_3)) > -1)
				{
					arrayList.Add(num);
					num += A_3.Length;
				}
				if (arrayList.Count != 0)
				{
					byte[] array2 = new byte[A_2 + arrayList.Count * (A_4.Length - A_3.Length)];
					num = A_1;
					int num2 = 0;
					for (int i = 0; i < arrayList.Count; i++)
					{
						Buffer.BlockCopy(A_0, num, array2, num2, (int)arrayList[i] - num);
						num2 += (int)arrayList[i] - num;
						Buffer.BlockCopy(A_4, 0, array2, num2, A_4.Length);
						num2 += A_4.Length;
						num = (int)arrayList[i] + A_3.Length;
					}
					Buffer.BlockCopy(A_0, num, array2, num2, A_2 + A_1 - num);
					return array2;
				}
				if (A_1 == 0 && A_2 == A_0.Length)
				{
					return A_0;
				}
				byte[] array3 = new byte[A_2];
				Buffer.BlockCopy(A_0, A_1, array3, 0, A_2);
				return array3;
			}
		}

		// Token: 0x06002A3E RID: 10814 RVA: 0x000C6298 File Offset: 0x000C5298
		public static int a(byte[] A_0, int A_1, int A_2, byte[] A_3, byte[] A_4)
		{
			if (A_3.Length != A_4.Length)
			{
				throw new ArgumentException();
			}
			int num = A_1;
			int num2 = 0;
			while (num < A_1 + A_2 && (num = w.b(A_0, num, A_2 - (num - A_1), A_3)) > -1)
			{
				Buffer.BlockCopy(A_4, 0, A_0, num, A_4.Length);
				num2++;
			}
			return num2;
		}

		// Token: 0x06002A3F RID: 10815 RVA: 0x000C62E8 File Offset: 0x000C52E8
		public static byte[] b(byte[] A_0, byte[] A_1)
		{
			if (A_0 == null)
			{
				return A_1;
			}
			if (A_1 == null)
			{
				return A_0;
			}
			byte[] array = new byte[A_0.Length + A_1.Length];
			Buffer.BlockCopy(A_0, 0, array, 0, A_0.Length);
			Buffer.BlockCopy(A_1, 0, array, A_0.Length, A_1.Length);
			return array;
		}

		// Token: 0x06002A40 RID: 10816 RVA: 0x000C6328 File Offset: 0x000C5328
		public static byte[] a(byte[] A_0, byte[] A_1, int A_2)
		{
			if (A_0 == null)
			{
				return A_1;
			}
			if (A_1 == null)
			{
				return A_0;
			}
			byte[] array = new byte[A_0.Length + A_2];
			Buffer.BlockCopy(A_0, 0, array, 0, A_0.Length);
			Buffer.BlockCopy(A_1, 0, array, A_0.Length, A_2);
			return array;
		}

		// Token: 0x06002A41 RID: 10817 RVA: 0x000C6364 File Offset: 0x000C5364
		public static byte[] a(ref byte[] A_0, int A_1, int A_2)
		{
			if (A_1 + A_2 > A_0.Length)
			{
				return null;
			}
			byte[] array = new byte[A_2];
			Buffer.BlockCopy(A_0, A_1, array, 0, array.Length);
			byte[] array2 = new byte[A_0.Length - A_2];
			Buffer.BlockCopy(A_0, 0, array2, 0, A_1);
			Buffer.BlockCopy(A_0, A_1 + A_2, array2, A_1, A_0.Length - (A_1 + A_2));
			A_0 = array2;
			return array;
		}

		// Token: 0x06002A42 RID: 10818 RVA: 0x000C63C0 File Offset: 0x000C53C0
		public static int a(byte[] A_0, byte[] A_1)
		{
			if (A_0.Length < A_1.Length)
			{
				return -1;
			}
			if (A_0.Length > A_1.Length)
			{
				return 1;
			}
			for (int i = 0; i < A_0.Length; i++)
			{
				if (A_0[i] < A_1[i])
				{
					return -1;
				}
				if (A_0[i] > A_1[i])
				{
					return 1;
				}
			}
			return 0;
		}

		// Token: 0x06002A43 RID: 10819 RVA: 0x000C6404 File Offset: 0x000C5404
		public static int a(byte[] A_0, byte A_1, int A_2)
		{
			int result = -1;
			for (int i = A_2; i < A_0.Length; i++)
			{
				if (A_0[i] == A_1)
				{
					result = i;
					break;
				}
			}
			return result;
		}

		// Token: 0x06002A44 RID: 10820 RVA: 0x000C642C File Offset: 0x000C542C
		public static byte[] a(byte[] A_0, char[] A_1)
		{
			byte[] bytes = Encoding.ASCII.GetBytes(A_1);
			int num = 0;
			int num2 = A_0.Length;
			for (int i = num; i < num2; i++)
			{
				bool flag = false;
				foreach (byte b in bytes)
				{
					if (A_0[i] == b)
					{
						num++;
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					break;
				}
			}
			for (int k = num2 - 1; k >= num; k--)
			{
				bool flag2 = false;
				foreach (byte b2 in bytes)
				{
					if (A_0[k] == b2)
					{
						num2--;
						flag2 = true;
						break;
					}
				}
				if (!flag2)
				{
					break;
				}
			}
			byte[] array2 = new byte[num2 - num];
			Buffer.BlockCopy(A_0, num, array2, 0, num2 - num);
			return array2;
		}

		// Token: 0x04001D28 RID: 7464
		public static readonly byte[] a = Encoding.ASCII.GetBytes("\r\n.");

		// Token: 0x04001D29 RID: 7465
		private static readonly byte[] b = Encoding.ASCII.GetBytes("\r\n..");

		// Token: 0x04001D2A RID: 7466
		public static readonly byte[] c = Encoding.ASCII.GetBytes("\r\n");
	}
}
