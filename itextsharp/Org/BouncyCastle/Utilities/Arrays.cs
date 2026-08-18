using System;
using System.Text;

namespace Org.BouncyCastle.Utilities
{
	// Token: 0x020001D7 RID: 471
	public sealed class Arrays
	{
		// Token: 0x06001282 RID: 4738 RVA: 0x0006A899 File Offset: 0x00069899
		private Arrays()
		{
		}

		// Token: 0x06001283 RID: 4739 RVA: 0x0006A8A4 File Offset: 0x000698A4
		public static bool AreEqual(bool[] a, bool[] b)
		{
			if (a == b)
			{
				return true;
			}
			if (a == null || b == null)
			{
				return false;
			}
			int num = a.Length;
			if (num != b.Length)
			{
				return false;
			}
			while (num != 0)
			{
				num--;
				if (a[num] != b[num])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001284 RID: 4740 RVA: 0x0006A8DD File Offset: 0x000698DD
		public static bool AreEqual(byte[] a, byte[] b)
		{
			return a == b || (a != null && b != null && Arrays.HaveSameContents(a, b));
		}

		// Token: 0x06001285 RID: 4741 RVA: 0x0006A8F4 File Offset: 0x000698F4
		[Obsolete("Use 'AreEqual' method instead")]
		public static bool AreSame(byte[] a, byte[] b)
		{
			return Arrays.AreEqual(a, b);
		}

		// Token: 0x06001286 RID: 4742 RVA: 0x0006A900 File Offset: 0x00069900
		public static bool ConstantTimeAreEqual(byte[] a, byte[] b)
		{
			int num = a.Length;
			if (num != b.Length)
			{
				return false;
			}
			int num2 = 0;
			while (num != 0)
			{
				num--;
				num2 |= (int)(a[num] ^ b[num]);
			}
			return num2 == 0;
		}

		// Token: 0x06001287 RID: 4743 RVA: 0x0006A932 File Offset: 0x00069932
		public static bool AreEqual(int[] a, int[] b)
		{
			return a == b || (a != null && b != null && Arrays.HaveSameContents(a, b));
		}

		// Token: 0x06001288 RID: 4744 RVA: 0x0006A94C File Offset: 0x0006994C
		private static bool HaveSameContents(byte[] a, byte[] b)
		{
			int num = a.Length;
			if (num != b.Length)
			{
				return false;
			}
			while (num != 0)
			{
				num--;
				if (a[num] != b[num])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001289 RID: 4745 RVA: 0x0006A978 File Offset: 0x00069978
		private static bool HaveSameContents(int[] a, int[] b)
		{
			int num = a.Length;
			if (num != b.Length)
			{
				return false;
			}
			while (num != 0)
			{
				num--;
				if (a[num] != b[num])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600128A RID: 4746 RVA: 0x0006A9A4 File Offset: 0x000699A4
		public static string ToString(object[] a)
		{
			StringBuilder stringBuilder = new StringBuilder(91);
			if (a.Length > 0)
			{
				stringBuilder.Append(a[0]);
				for (int i = 1; i < a.Length; i++)
				{
					stringBuilder.Append(", ").Append(a[i]);
				}
			}
			stringBuilder.Append(']');
			return stringBuilder.ToString();
		}

		// Token: 0x0600128B RID: 4747 RVA: 0x0006A9FC File Offset: 0x000699FC
		public static int GetHashCode(byte[] data)
		{
			if (data == null)
			{
				return 0;
			}
			int num = data.Length;
			int num2 = num + 1;
			while (--num >= 0)
			{
				num2 *= 257;
				num2 ^= (int)data[num];
			}
			return num2;
		}

		// Token: 0x0600128C RID: 4748 RVA: 0x0006AA2F File Offset: 0x00069A2F
		public static byte[] Clone(byte[] data)
		{
			if (data != null)
			{
				return (byte[])data.Clone();
			}
			return null;
		}

		// Token: 0x0600128D RID: 4749 RVA: 0x0006AA41 File Offset: 0x00069A41
		public static int[] Clone(int[] data)
		{
			if (data != null)
			{
				return (int[])data.Clone();
			}
			return null;
		}
	}
}
