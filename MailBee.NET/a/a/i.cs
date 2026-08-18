using System;
using System.Text;

namespace a.a
{
	// Token: 0x020003EC RID: 1004
	internal class i : v
	{
		// Token: 0x060023B0 RID: 9136 RVA: 0x00095F86 File Offset: 0x00094F86
		protected i()
		{
		}

		// Token: 0x060023B1 RID: 9137 RVA: 0x00095F90 File Offset: 0x00094F90
		public new static int a(byte[] A_0, int A_1, int A_2, int A_3, bool A_4)
		{
			int num = A_2;
			for (;;)
			{
				int num2 = Array.IndexOf<byte>(A_0, 10, num, A_2 + A_3 - num);
				if (num2 < 0)
				{
					break;
				}
				num = num2 + 1;
				if (!A_4 || A_0[A_1] == 45)
				{
					return num;
				}
				if (num - 3 < A_1)
				{
					goto Block_3;
				}
				if (num >= A_1 + 5)
				{
					if (A_0[num - 5] == 13 && A_0[num - 4] == 10 && A_0[num - 3] == 46 && A_0[num - 2] == 13 && A_0[num - 1] == 10)
					{
						if (num >= A_2 + A_3)
						{
							return num;
						}
						byte b = A_0[num];
						if (b == 43 || b == 45)
						{
							return num;
						}
					}
				}
				else if (num == A_1 + 3 && A_0[num - 3] == 46 && A_0[num - 2] == 13 && A_0[num - 1] == 10)
				{
					return num;
				}
			}
			return -num;
			Block_3:
			return -num;
		}

		// Token: 0x060023B2 RID: 9138 RVA: 0x00096044 File Offset: 0x00095044
		public new static af a(string A_0, Encoding A_1)
		{
			int num = A_0.IndexOfAny(v.a);
			if (num == 0)
			{
				throw new l(122, A_0);
			}
			string a_;
			if (num < 0)
			{
				a_ = A_0.ToUpper();
			}
			else
			{
				a_ = A_0.Substring(0, num).ToUpper();
			}
			if (a_ == "+OK")
			{
				return af.a;
			}
			if (a_ == "-ERR")
			{
				return af.c;
			}
			if (!(a_ == "+"))
			{
				throw new l(125, a_);
			}
			return af.b;
		}

		// Token: 0x060023B3 RID: 9139 RVA: 0x000960BC File Offset: 0x000950BC
		public new static string a(string A_0)
		{
			int num = A_0.IndexOfAny(v.a);
			if (num < 0)
			{
				return string.Empty;
			}
			return A_0.Substring(num + 1).Trim();
		}

		// Token: 0x060023B4 RID: 9140 RVA: 0x000960F0 File Offset: 0x000950F0
		public new static ao a(ao A_0, Encoding A_1)
		{
			int num = A_0.b();
			int num2 = A_0.e();
			byte[] array = A_0.d();
			int num3 = w.b(array, num, num2);
			if (num3 < 0)
			{
				throw new l(121, A_1.GetString(array, num, num2));
			}
			num3 += 2;
			int num4 = num + num2 - 3;
			if (num4 < num3 || array[num4] != 46)
			{
				throw new l(121, A_1.GetString(array, num, num2));
			}
			return w.a(A_0, num3, num4 - num3);
		}

		// Token: 0x0400179C RID: 6044
		private new const string a = "+OK";

		// Token: 0x0400179D RID: 6045
		private const string b = "-ERR";

		// Token: 0x0400179E RID: 6046
		private const string c = "+";
	}
}
