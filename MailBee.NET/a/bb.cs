using System;
using System.Collections.Specialized;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using MailBee;

namespace a
{
	// Token: 0x02000505 RID: 1285
	internal class bb
	{
		// Token: 0x06002AAD RID: 10925 RVA: 0x000CB42E File Offset: 0x000CA42E
		private bb()
		{
		}

		// Token: 0x06002AAE RID: 10926 RVA: 0x000CB436 File Offset: 0x000CA436
		public static string[] e(string A_0)
		{
			return bb.b(A_0, -1, false);
		}

		// Token: 0x06002AAF RID: 10927 RVA: 0x000CB440 File Offset: 0x000CA440
		public static string[] b(string A_0, int A_1, bool A_2)
		{
			StringCollection stringCollection = new StringCollection();
			int num = 0;
			int num2 = 0;
			while (num2 < A_0.Length && (A_1 > 0 || A_1 < 0))
			{
				num2 = A_0.IndexOf("\r\n", num);
				if (num2 < 0)
				{
					num2 = A_0.Length;
				}
				if (num2 == num)
				{
					if (A_2)
					{
						stringCollection.Add(string.Empty);
					}
				}
				else
				{
					stringCollection.Add(A_0.Substring(num, num2 - num));
				}
				num = num2 + 2;
				A_1--;
			}
			string[] array = new string[stringCollection.Count];
			stringCollection.CopyTo(array, 0);
			return array;
		}

		// Token: 0x06002AB0 RID: 10928 RVA: 0x000CB4C8 File Offset: 0x000CA4C8
		public static string a(string A_0, char A_1, char A_2, bool A_3)
		{
			if (A_0 == null || A_0 == string.Empty)
			{
				return A_0;
			}
			int num = A_0.IndexOf(A_1);
			if (num < 0 || num == A_0.Length - 1)
			{
				return string.Empty;
			}
			int num2 = A_0.IndexOf(A_2, num + 1);
			if (num2 < 0)
			{
				return string.Empty;
			}
			if (A_3)
			{
				return A_0.Substring(num, num2 - num + 1);
			}
			return A_0.Substring(num + 1, num2 - num - 1);
		}

		// Token: 0x06002AB1 RID: 10929 RVA: 0x000CB538 File Offset: 0x000CA538
		public static int a(StringCollection A_0, string A_1, int A_2)
		{
			for (int i = A_2; i < A_0.Count; i++)
			{
				if (A_0[i] == A_1)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06002AB2 RID: 10930 RVA: 0x000CB568 File Offset: 0x000CA568
		public static void a(StringCollection A_0)
		{
			for (int i = 0; i < A_0.Count - 1; i++)
			{
				int num = i + 1;
				while (num < A_0.Count && (num = bb.a(A_0, A_0[i], num)) > -1)
				{
					A_0.RemoveAt(num);
				}
			}
		}

		// Token: 0x06002AB3 RID: 10931 RVA: 0x000CB5B4 File Offset: 0x000CA5B4
		public static string a(string A_0, Encoding A_1)
		{
			for (int i = 0; i < A_0.Length; i++)
			{
				int num = (int)A_0[i];
				if (num < 33 || num > 126 || num == 61 || num == 43)
				{
					int num2 = A_0.Length - i;
					byte[] array = new byte[A_1.GetMaxByteCount(num2)];
					int bytes = A_1.GetBytes(A_0, i, A_0.Length - i, array, 0);
					StringBuilder stringBuilder = new StringBuilder(i + num2 * 3);
					stringBuilder.Append(A_0, 0, i);
					for (int j = 0; j < bytes; j++)
					{
						byte b = array[j];
						if (b < 33 || b > 126 || b == 61 || b == 43)
						{
							stringBuilder.Append('+');
							stringBuilder.Append(b.ToString("X2"));
						}
						else
						{
							stringBuilder.Append((char)b);
						}
					}
					return stringBuilder.ToString();
				}
			}
			return A_0;
		}

		// Token: 0x06002AB4 RID: 10932 RVA: 0x000CB69C File Offset: 0x000CA69C
		public static string d(string A_0)
		{
			StringBuilder stringBuilder = new StringBuilder(A_0.Length);
			int i = 0;
			while (i < A_0.Length)
			{
				bool flag = false;
				if (A_0[i] == '+' && i < A_0.Length - 2)
				{
					string text = A_0.Substring(i, 3);
					char c = '\0';
					try
					{
						c = Convert.ToChar(byte.Parse(text.Substring(1, 2), NumberStyles.HexNumber));
						i += 3;
						flag = true;
					}
					catch (Exception)
					{
					}
					if (c != '\0')
					{
						stringBuilder.Append(c);
					}
				}
				if (!flag)
				{
					stringBuilder.Append(A_0[i++]);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06002AB5 RID: 10933 RVA: 0x000CB744 File Offset: 0x000CA744
		public static string[] c(string A_0)
		{
			return bb.a(A_0, -1, false);
		}

		// Token: 0x06002AB6 RID: 10934 RVA: 0x000CB750 File Offset: 0x000CA750
		public static string[] a(string A_0, int A_1, bool A_2)
		{
			StringCollection stringCollection = new StringCollection();
			int num = 0;
			int num2 = 0;
			while (num2 < A_0.Length && (A_1 > 0 || A_1 < 0))
			{
				num2 = A_0.IndexOf('\n', num);
				if (num2 < 0)
				{
					num2 = A_0.Length;
				}
				if (num2 == num)
				{
					if (A_2)
					{
						stringCollection.Add(string.Empty);
					}
				}
				else
				{
					int num3 = 0;
					if (num2 > 0 && A_0[num2 - 1] == '\r')
					{
						num3 = 1;
					}
					string text = A_0.Substring(num, num2 - num - num3);
					if (text.Length == 0)
					{
						if (A_2)
						{
							stringCollection.Add(text);
						}
					}
					else
					{
						stringCollection.Add(text);
					}
				}
				num = num2 + 1;
				A_1--;
			}
			string[] array = new string[stringCollection.Count];
			stringCollection.CopyTo(array, 0);
			return array;
		}

		// Token: 0x06002AB7 RID: 10935 RVA: 0x000CB810 File Offset: 0x000CA810
		public static Encoding b(string A_0)
		{
			Encoding result = null;
			foreach (string input in bb.a("meta", A_0, true))
			{
				Match match = new Regex("charset=(?<paramCharset>[^\"'>\\s]+)", RegexOptions.IgnoreCase | RegexOptions.Compiled).Match(input);
				if (!match.Success)
				{
					match = new Regex("charset\\s*=\\s*\"(?<paramCharset>[^\"]+)\"", RegexOptions.IgnoreCase | RegexOptions.Compiled).Match(input);
				}
				if (match.Success)
				{
					try
					{
						result = bb.a(match.Groups["paramCharset"].Value);
						break;
					}
					catch (NotSupportedException)
					{
						break;
					}
				}
			}
			return result;
		}

		// Token: 0x06002AB8 RID: 10936 RVA: 0x000CB8CC File Offset: 0x000CA8CC
		public static StringCollection a(string A_0, string A_1, bool A_2)
		{
			StringCollection stringCollection = new StringCollection();
			string pattern = string.Empty;
			if (A_2)
			{
				pattern = string.Format(CultureInfo.InvariantCulture, "<{0}(\\s+[^= >]+([\\s]*=[\\s]*(?(\")([\"][^\"]*[\"])|(?(')(['][^']*['])|([^>]+))))?)*\\s*>", new object[]
				{
					Regex.Escape(A_0)
				});
			}
			else
			{
				pattern = string.Format(CultureInfo.InvariantCulture, "<(/?){0}(\\s+[^= >]+([\\s]*=[\\s]*(?(\")([\"][^\"]*[\"])|(?(')(['][^']*['])|([^>]+))))?)*\\s*>", new object[]
				{
					Regex.Escape(A_0)
				});
			}
			foreach (object obj in new Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.Singleline).Matches(A_1))
			{
				Match match = (Match)obj;
				stringCollection.Add(match.Value);
			}
			return stringCollection;
		}

		// Token: 0x06002AB9 RID: 10937 RVA: 0x000CB988 File Offset: 0x000CA988
		public static Encoding a(string A_0)
		{
			Encoding encoding = Global.DefaultEncoding;
			if (A_0 != null && A_0.Length > 0)
			{
				try
				{
					encoding = Encoding.GetEncoding(A_0);
				}
				catch
				{
					if (A_0.ToLower() == "utf8")
					{
						encoding = Encoding.UTF8;
					}
				}
				if (encoding == Encoding.ASCII)
				{
					encoding = Global.DefaultEncoding;
				}
				else if (encoding.CodePage == 28591)
				{
					return Encoding.GetEncoding(1252);
				}
			}
			return encoding;
		}

		// Token: 0x04001D83 RID: 7555
		private const string a = "\r\n";

		// Token: 0x04001D84 RID: 7556
		private const int b = 2;
	}
}
