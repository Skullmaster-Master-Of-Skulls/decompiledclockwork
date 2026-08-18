using System;
using System.Collections;
using System.Text;

namespace a.f
{
	// Token: 0x020000EE RID: 238
	internal class r : v
	{
		// Token: 0x060007E6 RID: 2022 RVA: 0x00024C70 File Offset: 0x00023C70
		protected r()
		{
		}

		// Token: 0x060007E7 RID: 2023 RVA: 0x00024C78 File Offset: 0x00023C78
		public new static int a(byte[] A_0, int A_1, int A_2, int A_3, ref int A_4)
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
				if (A_4 < 0)
				{
					A_4 = num - A_1;
				}
				if (num < A_1 + 5 || A_0[num - 3] != 125)
				{
					return num;
				}
				int count = (num > A_1 + 23) ? 20 : (num - 3 - A_1);
				int num3 = Array.LastIndexOf<byte>(A_0, 123, num - 4, count);
				if (num3 > -1)
				{
					try
					{
						int num4 = int.Parse(Encoding.ASCII.GetString(A_0, num3 + 1, num - 4 - num3));
						if (num + num4 >= A_2 + A_3)
						{
							return -(num + num4);
						}
						num += num4;
						continue;
					}
					catch
					{
					}
					return num;
				}
				return num;
			}
			return -num;
		}

		// Token: 0x060007E8 RID: 2024 RVA: 0x00024D2C File Offset: 0x00023D2C
		public new static m a(byte[] A_0, int A_1, int A_2, Encoding A_3)
		{
			if (A_2 < 1)
			{
				throw new l(122, A_3.GetString(A_0, A_1, A_2));
			}
			if (A_0[A_1] == 42)
			{
				return m.c;
			}
			if (A_0[A_1] == 43)
			{
				return m.d;
			}
			return m.b;
		}

		// Token: 0x060007E9 RID: 2025 RVA: 0x00024D58 File Offset: 0x00023D58
		public new static string a(byte[] A_0, int A_1, int A_2, Encoding A_3, out int A_4, out int A_5)
		{
			int num = Array.IndexOf<byte>(A_0, 32, A_1, A_2);
			if (num < 0 || num >= A_1 + A_2 - 1)
			{
				throw new l(125, A_3.GetString(A_0, A_1, A_2));
			}
			int num2 = Array.IndexOf<byte>(A_0, 32, num + 1, A_1 + A_2 - (num + 1));
			if (num2 < 0)
			{
				A_4 = 0;
				A_5 = A_1 + A_2;
				return A_3.GetString(A_0, num + 1, A_1 + A_2 - (num + 1)).ToUpper();
			}
			string @string = A_3.GetString(A_0, num + 1, num2 - (num + 1));
			if (@string.Length > 0 && char.IsDigit(@string[0]))
			{
				try
				{
					A_4 = int.Parse(@string);
				}
				catch
				{
					A_4 = 0;
					A_5 = num2 + 1;
					return @string.ToUpper();
				}
				if (num2 >= A_1 + A_2 - 1)
				{
					throw new l(125, A_3.GetString(A_0, A_1, A_2));
				}
				int num3 = Array.IndexOf<byte>(A_0, 32, num2 + 1, A_1 + A_2 - (num2 + 1));
				if (num3 < 0)
				{
					A_5 = A_1 + A_2;
					return A_3.GetString(A_0, num2 + 1, A_1 + A_2 - (num2 + 1)).ToUpper();
				}
				A_5 = num3 + 1;
				return A_3.GetString(A_0, num2 + 1, num3 - num2 - 1).ToUpper();
			}
			A_4 = 0;
			A_5 = num2 + 1;
			return @string.ToUpper();
		}

		// Token: 0x060007EA RID: 2026 RVA: 0x00024EA0 File Offset: 0x00023EA0
		public new static string a(string A_0)
		{
			int num = A_0.IndexOf(' ');
			if (num < 0)
			{
				throw new l(125, A_0);
			}
			return A_0.Substring(0, num);
		}

		// Token: 0x060007EB RID: 2027 RVA: 0x00024ECB File Offset: 0x00023ECB
		public new static string a(byte[] A_0, int A_1, int A_2, Encoding A_3, int A_4)
		{
			if (A_4 < A_1 + A_2)
			{
				return A_3.GetString(A_0, A_4, A_1 + A_2 - A_4).Trim();
			}
			return string.Empty;
		}

		// Token: 0x060007EC RID: 2028 RVA: 0x00024EF0 File Offset: 0x00023EF0
		private new static object a(ao A_0, int A_1, int A_2, Encoding A_3, ArrayList A_4, out int A_5, bool A_6, bool A_7, byte A_8)
		{
			if (A_2 < 1)
			{
				throw new l(125, string.Empty);
			}
			byte b = 41;
			byte b2 = A_0.d()[A_1];
			if (b2 <= 40)
			{
				if (b2 == 34)
				{
					goto IL_1C7;
				}
				if (b2 != 40)
				{
					goto IL_271;
				}
			}
			else if (b2 != 91)
			{
				if (b2 != 123)
				{
					goto IL_271;
				}
				if (A_2 < 5)
				{
					throw new l(125, A_3.GetString(A_0.d(), A_1, A_2));
				}
				int num = Array.IndexOf<byte>(A_0.d(), 125, A_1 + 2, (A_2 < 22) ? (A_2 - 2) : 20);
				if (num < 0 || num > A_1 + A_2 - 2 || A_0.d()[num + 2] != 10)
				{
					throw new l(125, A_3.GetString(A_0.d(), A_1, A_2));
				}
				try
				{
					int num2 = int.Parse(Encoding.ASCII.GetString(A_0.d(), A_1 + 1, num - (A_1 + 1)));
					A_5 = num + 3 + num2;
					if (A_6)
					{
						return null;
					}
					ao ao = new ao(A_0, num + 3, num2);
					if (A_4 != null)
					{
						A_4.Add(ao);
					}
					return ao;
				}
				catch
				{
					throw new l(125, A_3.GetString(A_0.d(), A_1, A_2));
				}
				goto IL_1C7;
			}
			else
			{
				if (!A_7)
				{
					goto IL_271;
				}
				b = 93;
			}
			A_5 = A_1 + 1;
			ArrayList arrayList = null;
			if (!A_6)
			{
				arrayList = new ArrayList();
			}
			while (A_5 <= A_1 + A_2 - 1)
			{
				if (A_0.d()[A_5] == b)
				{
					A_5++;
					if (A_6)
					{
						return null;
					}
					return arrayList;
				}
				else
				{
					object value = r.a(A_0, A_5, A_1 + A_2 - A_5, A_3, A_4, out A_5, A_6, false, b);
					if (!A_6)
					{
						arrayList.Add(value);
					}
					while (A_5 < A_1 + A_2)
					{
						if (A_0.d()[A_5] != 32)
						{
							break;
						}
						A_5++;
					}
				}
			}
			throw new l(125, A_3.GetString(A_0.d(), A_1, A_2));
			IL_1C7:
			int i = A_1 + 1;
			while (i < A_1 + A_2)
			{
				i = Array.IndexOf<byte>(A_0.d(), 34, i, A_1 + A_2 - i);
				if (i < 0)
				{
					throw new l(125, A_3.GetString(A_0.d(), A_1, A_2));
				}
				int num3 = 1;
				while (num3 < i && A_0.d()[i - num3] == 92)
				{
					num3++;
				}
				num3--;
				if (num3 % 2 == 1)
				{
					i++;
				}
				else
				{
					A_5 = i + 1;
					if (A_6)
					{
						return null;
					}
					return ao.a(A_0, A_1 + 1, i - (A_1 + 1));
				}
			}
			throw new l(125, A_3.GetString(A_0.d(), A_1, A_2));
			IL_271:
			int num4 = -1;
			ao ao2 = null;
			A_5 = A_1 + 1;
			while (A_5 < A_1 + A_2)
			{
				int num5 = (A_1 + A_2 - A_5 > 20) ? 20 : (A_1 + A_2 - A_5);
				int num6 = Array.IndexOf<byte>(A_0.d(), 32, A_5, num5);
				if (A_8 > 0)
				{
					num4 = Array.IndexOf<byte>(A_0.d(), A_8, A_5, num5);
				}
				int num7 = Array.IndexOf<byte>(A_0.d(), 91, A_5, num5);
				if (num7 > -1 && (num7 < num6 || num6 < 0) && (num7 < num4 || num4 < 0) && Encoding.ASCII.GetString(A_0.d(), A_1, num7 - A_1).ToUpper() == "BODY")
				{
					A_5 = num7;
					r.a(A_0, A_5, A_1 + A_2 - A_5, A_3, A_4, out A_5, true, true, 93);
				}
				else
				{
					if (num6 > -1 && (num6 < num4 || num4 < 0))
					{
						A_5 = num6;
						ao2 = new ao(A_0, A_1, num6 - A_1);
						break;
					}
					if (num4 > -1 && (num4 < num6 || num6 < 0))
					{
						A_5 = num4;
						ao2 = new ao(A_0, A_1, num4 - A_1);
						break;
					}
					A_5 += num5;
				}
			}
			if (ao2 == null)
			{
				ao2 = new ao(A_0, A_1, A_5 - A_1);
			}
			if (!A_6 && ao2.e() == 3 && ao2.a(Encoding.ASCII).ToUpper() == "NIL")
			{
				return null;
			}
			return ao2;
		}

		// Token: 0x060007ED RID: 2029 RVA: 0x000252F0 File Offset: 0x000242F0
		public new static ArrayList a(ao A_0, int A_1, int A_2, Encoding A_3, ArrayList A_4, int A_5, bool A_6, out int A_7)
		{
			A_7 = A_1;
			if (A_2 == 0)
			{
				return null;
			}
			ArrayList arrayList = new ArrayList();
			while (A_7 < A_1 + A_2 && (A_5 < 0 || arrayList.Count < A_5))
			{
				if (arrayList.Count > 0)
				{
					while (A_7 < A_1 + A_2 && A_0.d()[A_7] == 32)
					{
						A_7++;
					}
				}
				if (A_7 < A_1 + A_2)
				{
					object value = r.a(A_0, A_7, A_1 + A_2 - A_7, A_3, A_4, out A_7, false, A_6, 0);
					arrayList.Add(value);
				}
			}
			return arrayList;
		}
	}
}
