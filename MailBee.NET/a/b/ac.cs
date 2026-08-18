using System;
using System.Globalization;
using System.Text;

namespace a.b
{
	// Token: 0x020003B9 RID: 953
	internal class ac
	{
		// Token: 0x06002264 RID: 8804 RVA: 0x0008C510 File Offset: 0x0008B510
		public static string a(string A_0, params object[] A_1)
		{
			string result;
			try
			{
				result = string.Format(CultureInfo.InvariantCulture, A_0, A_1);
			}
			catch (FormatException)
			{
				result = A_0;
			}
			return result;
		}

		// Token: 0x06002265 RID: 8805 RVA: 0x0008C544 File Offset: 0x0008B544
		public static string[] a(string A_0, char A_1, char A_2, bool A_3, params char[] A_4)
		{
			if (A_0 == null)
			{
				throw new ArgumentNullException("toSplit");
			}
			if (A_4 == null || A_4.Length == 0)
			{
				throw new ArgumentNullException("separator");
			}
			string text = new string(A_4);
			if (text.IndexOf(A_1) >= 0 || text.IndexOf(A_2) >= 0)
			{
				throw new ArgumentException(cg.g(), "separator");
			}
			hx hx = new hx();
			StringBuilder stringBuilder = null;
			int length = A_0.Length;
			bool flag = false;
			for (int i = 0; i < length; i++)
			{
				char c = A_0[i];
				if (c == A_2)
				{
					if (i >= length - 1)
					{
						throw new ArgumentException(cg.e(), "toSplit");
					}
					if (stringBuilder == null)
					{
						stringBuilder = new StringBuilder();
					}
					i++;
					c = A_0[i];
					if (c <= 'r')
					{
						if (c == 'n')
						{
							stringBuilder.Append('\n');
							goto IL_201;
						}
						if (c == 'r')
						{
							stringBuilder.Append('\r');
							goto IL_201;
						}
					}
					else
					{
						if (c == 't')
						{
							stringBuilder.Append('\t');
							goto IL_201;
						}
						if (c == 'x')
						{
							if (i < length - 2)
							{
								int num = ac.a(A_0[i + 1]) * 16;
								int num2 = ac.a(A_0[i + 2]);
								char value = (char)(num + num2);
								stringBuilder.Append(value);
								i += 2;
								goto IL_201;
							}
							throw new ArgumentException(cg.f(), "toSplit");
						}
					}
					stringBuilder.Append(c);
				}
				else if (c == A_1)
				{
					if (stringBuilder != null)
					{
						hx.c(stringBuilder.ToString());
						stringBuilder = null;
					}
					else if (flag)
					{
						hx.c(string.Empty);
					}
					flag = !flag;
				}
				else if (text.IndexOf(c) >= 0)
				{
					if (flag)
					{
						if (stringBuilder == null)
						{
							stringBuilder = new StringBuilder();
						}
						stringBuilder.Append(c);
					}
					else if (stringBuilder != null)
					{
						hx.c(stringBuilder.ToString());
						stringBuilder = null;
					}
					else if (A_3 && (i == 0 || text.IndexOf(A_0[i - 1]) >= 0))
					{
						hx.c(string.Empty);
					}
				}
				else
				{
					if (stringBuilder == null)
					{
						stringBuilder = new StringBuilder();
					}
					stringBuilder.Append(c);
				}
				IL_201:;
			}
			if (flag)
			{
				throw new ArgumentException(cg.d(), "toSplit");
			}
			if (stringBuilder != null)
			{
				hx.c(stringBuilder.ToString());
			}
			string[] array = new string[hx.Count];
			hx.ov(array, 0);
			return array;
		}

		// Token: 0x06002266 RID: 8806 RVA: 0x0008C79C File Offset: 0x0008B79C
		private static int a(char A_0)
		{
			if (A_0 >= 'a' && A_0 <= 'f')
			{
				return (int)(A_0 - 'a' + '\n');
			}
			if (A_0 >= 'A' && A_0 <= 'F')
			{
				return (int)(A_0 - 'A' + '\n');
			}
			if (A_0 >= '0' && A_0 <= '9')
			{
				return (int)(A_0 - '0');
			}
			throw new ArgumentException(cg.c(), "c");
		}
	}
}
