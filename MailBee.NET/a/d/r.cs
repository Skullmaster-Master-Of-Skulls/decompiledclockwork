using System;

namespace a.d
{
	// Token: 0x0200047B RID: 1147
	internal class r : v
	{
		// Token: 0x06002794 RID: 10132 RVA: 0x000B7E92 File Offset: 0x000B6E92
		protected r()
		{
		}

		// Token: 0x06002795 RID: 10133 RVA: 0x000B7E9C File Offset: 0x000B6E9C
		public new static int a(byte[] A_0, int A_1, int A_2)
		{
			bool flag = true;
			int num = A_1;
			for (;;)
			{
				if (flag)
				{
					flag = false;
				}
				else
				{
					num = Array.IndexOf<byte>(A_0, 10, num, A_1 + A_2 - num);
					if (num < 0)
					{
						break;
					}
					num++;
				}
				if (num > A_1 + A_2 - 6)
				{
					return -1;
				}
				num += 3;
				if (A_0[num] == 32)
				{
					goto Block_4;
				}
			}
			return num;
			Block_4:
			int num2 = Array.IndexOf<byte>(A_0, 10, num, A_1 + A_2 - num);
			if (num2 > -1)
			{
				num2++;
			}
			return num2;
		}

		// Token: 0x06002796 RID: 10134 RVA: 0x000B7EFC File Offset: 0x000B6EFC
		public static int b(string A_0)
		{
			int num = A_0.IndexOfAny(r.a);
			if (num == 0)
			{
				throw new l(122, A_0);
			}
			string s;
			if (num < 0)
			{
				s = A_0;
			}
			else
			{
				s = A_0.Substring(0, num);
			}
			int result;
			try
			{
				result = int.Parse(s);
			}
			catch (Exception)
			{
				throw new l(125, A_0);
			}
			return result;
		}

		// Token: 0x06002797 RID: 10135 RVA: 0x000B7F58 File Offset: 0x000B6F58
		public new static string a(string A_0)
		{
			int num = A_0.IndexOfAny(r.a);
			if (num < 0)
			{
				return string.Empty;
			}
			return A_0.Substring(num + 1).Trim();
		}

		// Token: 0x04001B1D RID: 6941
		private new static readonly char[] a = new char[]
		{
			' ',
			'-'
		};
	}
}
