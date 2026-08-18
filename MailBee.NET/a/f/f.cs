using System;
using System.Text;

namespace a.f
{
	// Token: 0x020000F4 RID: 244
	internal class f
	{
		// Token: 0x06000821 RID: 2081 RVA: 0x00025630 File Offset: 0x00024630
		private f()
		{
		}

		// Token: 0x06000822 RID: 2082 RVA: 0x00025638 File Offset: 0x00024638
		public static string b(string A_0)
		{
			StringBuilder stringBuilder = new StringBuilder();
			int num = 0;
			int i = 0;
			while (i < A_0.Length)
			{
				if (A_0[i] == '&')
				{
					stringBuilder.Append(A_0, num, i - num);
					stringBuilder.Append("&-");
					i++;
					num = i;
				}
				else if (A_0[i] >= '\u007f')
				{
					stringBuilder.Append(A_0, num, i - num);
					int num2 = i;
					while (i <= A_0.Length)
					{
						if (i == A_0.Length)
						{
							stringBuilder.Append(f.b(A_0, num2, i - num2));
							num = A_0.Length;
							break;
						}
						if (A_0[i] == '&')
						{
							stringBuilder.Append(f.b(A_0, num2, i - num2));
							stringBuilder.Append("&-");
							i++;
							num = i;
							break;
						}
						if (A_0[i] < '\u007f')
						{
							stringBuilder.Append(f.b(A_0, num2, i - num2));
							num = i;
							break;
						}
						i++;
					}
				}
				else
				{
					i++;
				}
			}
			if (num > 0)
			{
				return stringBuilder.Append(A_0, num, A_0.Length - num).ToString();
			}
			return A_0;
		}

		// Token: 0x06000823 RID: 2083 RVA: 0x00025750 File Offset: 0x00024750
		public static string a(string A_0)
		{
			StringBuilder stringBuilder = new StringBuilder();
			int num = 0;
			int num2;
			while (num < A_0.Length && (num2 = A_0.IndexOf('&', num)) > -1)
			{
				if (num2 == A_0.Length - 1)
				{
					return A_0;
				}
				stringBuilder.Append(A_0, num, num2 - num);
				int num3 = A_0.IndexOf('-', num2 + 1);
				if (num3 == num2 + 1)
				{
					stringBuilder.Append('&');
				}
				else
				{
					if (num3 <= -1)
					{
						return A_0;
					}
					stringBuilder.Append(f.a(A_0, num2, num3 + 1 - num2));
				}
				num = num3 + 1;
			}
			if (stringBuilder.Length == 0)
			{
				return A_0;
			}
			return stringBuilder.Append(A_0, num, A_0.Length - num).ToString();
		}

		// Token: 0x06000824 RID: 2084 RVA: 0x000257F4 File Offset: 0x000247F4
		private static string b(string A_0, int A_1, int A_2)
		{
			byte[] bytes = Encoding.UTF7.GetBytes(A_0.Substring(A_1, A_2));
			string text = Encoding.ASCII.GetString(bytes, 0, bytes.Length).Replace('/', ',');
			if (text.Length > 0 && text[0] == '+')
			{
				return "&" + text.Substring(1);
			}
			return text;
		}

		// Token: 0x06000825 RID: 2085 RVA: 0x00025854 File Offset: 0x00024854
		private static string a(string A_0, int A_1, int A_2)
		{
			string text;
			if (A_2 > 0 && A_0[A_1] == '&')
			{
				text = "+" + A_0.Substring(A_1 + 1, A_2 - 1);
			}
			else
			{
				text = A_0.Substring(A_1, A_2);
			}
			byte[] bytes = Encoding.ASCII.GetBytes(text.Replace(',', '/'));
			return Encoding.UTF7.GetString(bytes, 0, bytes.Length);
		}
	}
}
