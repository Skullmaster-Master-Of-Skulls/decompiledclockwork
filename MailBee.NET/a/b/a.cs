using System;
using System.Text;

namespace a.b
{
	// Token: 0x0200032E RID: 814
	internal class a
	{
		// Token: 0x06001D73 RID: 7539 RVA: 0x0007F0EB File Offset: 0x0007E0EB
		private a()
		{
		}

		// Token: 0x06001D74 RID: 7540 RVA: 0x0007F0F3 File Offset: 0x0007E0F3
		public static string c(byte[] A_0, int A_1, int A_2)
		{
			if (A_1 < 0 || A_1 >= A_0.Length)
			{
				throw new IndexOutOfRangeException("Illegal offset");
			}
			if (A_2 < 0 || (A_0.Length - A_1) / 2 < A_2)
			{
				throw new ArgumentException("Illegal Length");
			}
			return Encoding.Unicode.GetString(A_0, A_1, A_2 * 2);
		}

		// Token: 0x06001D75 RID: 7541 RVA: 0x0007F132 File Offset: 0x0007E132
		public static string b(byte[] A_0)
		{
			if (A_0.Length == 0)
			{
				return "";
			}
			return global::a.b.a.c(A_0, 0, A_0.Length / 2);
		}

		// Token: 0x06001D76 RID: 7542 RVA: 0x0007F14C File Offset: 0x0007E14C
		public static string b(byte[] A_0, int A_1, int A_2)
		{
			if (A_1 < 0 || A_1 >= A_0.Length)
			{
				throw new IndexOutOfRangeException("Illegal offset");
			}
			if (A_2 < 0 || (A_0.Length - A_1) / 2 < A_2)
			{
				throw new ArgumentException("Illegal Length");
			}
			string @string;
			try
			{
				@string = Encoding.GetEncoding("UTF-16BE").GetString(A_0, A_1, A_2 * 2);
			}
			catch
			{
				throw new InvalidOperationException();
			}
			return @string;
		}

		// Token: 0x06001D77 RID: 7543 RVA: 0x0007F1B8 File Offset: 0x0007E1B8
		public static string a(byte[] A_0)
		{
			if (A_0.Length == 0)
			{
				return "";
			}
			return global::a.b.a.b(A_0, 0, A_0.Length / 2);
		}

		// Token: 0x06001D78 RID: 7544 RVA: 0x0007F1D0 File Offset: 0x0007E1D0
		public static string a(byte[] A_0, int A_1, int A_2)
		{
			string @string;
			try
			{
				int count = Math.Min(A_2, A_0.Length - A_1);
				@string = Encoding.GetEncoding("ISO-8859-1").GetString(A_0, A_1, count);
			}
			catch
			{
				throw new InvalidOperationException();
			}
			return @string;
		}

		// Token: 0x06001D79 RID: 7545 RVA: 0x0007F218 File Offset: 0x0007E218
		public static void c(string A_0, byte[] A_1, int A_2)
		{
			try
			{
				byte[] bytes = Encoding.GetEncoding("ISO-8859-1").GetBytes(A_0);
				Array.Copy(bytes, 0, A_1, A_2, bytes.Length);
			}
			catch
			{
				throw;
			}
		}

		// Token: 0x06001D7A RID: 7546 RVA: 0x0007F258 File Offset: 0x0007E258
		public static void b(string A_0, c2 A_1)
		{
			byte[] bytes;
			try
			{
				bytes = Encoding.GetEncoding("ISO-8859-1").GetBytes(A_0);
			}
			catch (EncoderFallbackException)
			{
				throw;
			}
			A_1.po(bytes);
		}

		// Token: 0x06001D7B RID: 7547 RVA: 0x0007F294 File Offset: 0x0007E294
		public static void b(string A_0, byte[] A_1, int A_2)
		{
			byte[] bytes = Encoding.GetEncoding("UTF-16LE").GetBytes(A_0);
			Array.Copy(bytes, 0, A_1, A_2, bytes.Length);
		}

		// Token: 0x06001D7C RID: 7548 RVA: 0x0007F2C0 File Offset: 0x0007E2C0
		public static void a(string A_0, c2 A_1)
		{
			byte[] bytes;
			try
			{
				bytes = Encoding.GetEncoding("UTF-16LE").GetBytes(A_0);
			}
			catch (EncoderFallbackException)
			{
				throw;
			}
			A_1.po(bytes);
		}

		// Token: 0x06001D7D RID: 7549 RVA: 0x0007F2FC File Offset: 0x0007E2FC
		public static void a(string A_0, byte[] A_1, int A_2)
		{
			try
			{
				byte[] bytes = Encoding.GetEncoding("UTF-16BE").GetBytes(A_0);
				Array.Copy(bytes, 0, A_1, A_2, bytes.Length);
			}
			catch
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x06001D7E RID: 7550 RVA: 0x0007F340 File Offset: 0x0007E340
		public static string a()
		{
			return "ISO-8859-1";
		}

		// Token: 0x06001D7F RID: 7551 RVA: 0x0007F348 File Offset: 0x0007E348
		public static bool d(string A_0)
		{
			if (A_0 == null)
			{
				return false;
			}
			for (int i = 0; i < A_0.Length; i++)
			{
				if (A_0[i] > 'ÿ')
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001D80 RID: 7552 RVA: 0x0007F37C File Offset: 0x0007E37C
		public static string c(gc A_0, int A_1)
		{
			char[] array = new char[A_1];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = (char)A_0.a2();
			}
			return new string(array);
		}

		// Token: 0x06001D81 RID: 7553 RVA: 0x0007F3B0 File Offset: 0x0007E3B0
		public static string b(gc A_0, int A_1)
		{
			char[] array = new char[A_1];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = (char)A_0.a1();
			}
			return new string(array);
		}

		// Token: 0x06001D82 RID: 7554 RVA: 0x0007F3E4 File Offset: 0x0007E3E4
		public static string a(gc A_0)
		{
			int a_ = A_0.a1();
			if (((byte)A_0.ReadByte() & 1) == 0)
			{
				return global::a.b.a.c(A_0, a_);
			}
			return global::a.b.a.b(A_0, a_);
		}

		// Token: 0x06001D83 RID: 7555 RVA: 0x0007F412 File Offset: 0x0007E412
		public static string a(gc A_0, int A_1)
		{
			if (((byte)A_0.ReadByte() & 1) == 0)
			{
				return global::a.b.a.c(A_0, A_1);
			}
			return global::a.b.a.b(A_0, A_1);
		}

		// Token: 0x06001D84 RID: 7556 RVA: 0x0007F430 File Offset: 0x0007E430
		public static void b(c2 A_0, string A_1)
		{
			int length = A_1.Length;
			A_0.pn(length);
			bool flag = global::a.b.a.d(A_1);
			A_0.pj(flag ? 1 : 0);
			if (flag)
			{
				global::a.b.a.a(A_1, A_0);
				return;
			}
			global::a.b.a.b(A_1, A_0);
		}

		// Token: 0x06001D85 RID: 7557 RVA: 0x0007F474 File Offset: 0x0007E474
		public static void a(c2 A_0, string A_1)
		{
			bool flag = global::a.b.a.d(A_1);
			A_0.pj(flag ? 1 : 0);
			if (flag)
			{
				global::a.b.a.a(A_1, A_0);
				return;
			}
			global::a.b.a.b(A_1, A_0);
		}

		// Token: 0x06001D86 RID: 7558 RVA: 0x0007F4A7 File Offset: 0x0007E4A7
		public static int c(string A_0)
		{
			return 3 + A_0.Length * (global::a.b.a.d(A_0) ? 2 : 1);
		}

		// Token: 0x06001D87 RID: 7559 RVA: 0x0007F4C0 File Offset: 0x0007E4C0
		public static bool b(string A_0)
		{
			bool result;
			try
			{
				result = !A_0.Equals(Encoding.GetEncoding("ISO-8859-1").GetString(Encoding.GetEncoding("ISO-8859-1").GetBytes(A_0)));
			}
			catch
			{
				result = true;
			}
			return result;
		}

		// Token: 0x06001D88 RID: 7560 RVA: 0x0007F510 File Offset: 0x0007E510
		public static string a(string A_0)
		{
			char[] array = A_0.ToCharArray();
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < array.Length; i++)
			{
				if (global::a.b.a.a(array[i]))
				{
					string value = global::a.b.a.b(array[i]);
					stringBuilder.Append(value);
				}
				else
				{
					stringBuilder.Append(array[i]);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001D89 RID: 7561 RVA: 0x0007F565 File Offset: 0x0007E565
		public static string b(char A_0)
		{
			return Convert.ToString((int)A_0, 16);
		}

		// Token: 0x06001D8A RID: 7562 RVA: 0x0007F56F File Offset: 0x0007E56F
		public static string a(short A_0)
		{
			return global::a.b.a.b((char)A_0);
		}

		// Token: 0x06001D8B RID: 7563 RVA: 0x0007F578 File Offset: 0x0007E578
		public static string a(int A_0)
		{
			return global::a.b.a.b((char)A_0);
		}

		// Token: 0x06001D8C RID: 7564 RVA: 0x0007F581 File Offset: 0x0007E581
		public static string a(long A_0)
		{
			return global::a.b.a.b((char)A_0);
		}

		// Token: 0x06001D8D RID: 7565 RVA: 0x0007F58C File Offset: 0x0007E58C
		private static bool a(char A_0)
		{
			string text = "$-_.+!*'(),@=&";
			return A_0 > '\u007f' || (!char.IsLetterOrDigit(A_0) && text.IndexOf(A_0) < 0);
		}

		// Token: 0x04001387 RID: 4999
		private const string a = "ISO-8859-1";
	}
}
