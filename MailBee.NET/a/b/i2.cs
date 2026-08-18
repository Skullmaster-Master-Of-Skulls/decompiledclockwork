using System;
using System.Collections;
using System.Text;

namespace a.b
{
	// Token: 0x020003B0 RID: 944
	internal class i2
	{
		// Token: 0x0600221C RID: 8732 RVA: 0x0008BBAC File Offset: 0x0008ABAC
		public static bool a(IEnumerable A_0, IEnumerable A_1)
		{
			bool flag = A_0 == A_1;
			if (!flag && A_0 != null && A_1 != null)
			{
				IEnumerator enumerator = A_1.GetEnumerator();
				flag = true;
				foreach (object obj in A_0)
				{
					if (!enumerator.MoveNext())
					{
						flag = false;
						break;
					}
					object obj2 = enumerator.Current;
					if (obj != obj2 && (obj == null || !obj.Equals(obj2)))
					{
						flag = false;
						break;
					}
				}
				if (flag && enumerator.MoveNext())
				{
					flag = false;
				}
			}
			return flag;
		}

		// Token: 0x0600221D RID: 8733 RVA: 0x0008BC48 File Offset: 0x0008AC48
		public static bool a(IEnumerable A_0, object A_1)
		{
			bool flag = A_0 == A_1;
			if (!flag && A_0 != null && A_1 != null && A_0.GetType() == A_1.GetType())
			{
				flag = i2.a(A_0, A_1 as IEnumerable);
			}
			return flag;
		}

		// Token: 0x0600221E RID: 8734 RVA: 0x0008BC84 File Offset: 0x0008AC84
		public static int a(int A_0, object A_1)
		{
			int num = (A_1 != null) ? A_1.GetHashCode() : 0;
			if (A_0 != 0)
			{
				num += A_0 * 31;
			}
			return num;
		}

		// Token: 0x0600221F RID: 8735 RVA: 0x0008BCAC File Offset: 0x0008ACAC
		public static int a(int A_0, int A_1)
		{
			int num = A_1;
			if (A_0 != 0)
			{
				num += A_0 * 31;
			}
			return num;
		}

		// Token: 0x06002220 RID: 8736 RVA: 0x0008BCC8 File Offset: 0x0008ACC8
		public static int b(IEnumerable A_0)
		{
			int num = 1;
			if (A_0 == null)
			{
				throw new ArgumentNullException("enumerable");
			}
			foreach (object obj in A_0)
			{
				num = num * 31 + ((obj != null) ? obj.GetHashCode() : 0);
			}
			return num;
		}

		// Token: 0x06002221 RID: 8737 RVA: 0x0008BD34 File Offset: 0x0008AD34
		public static string a(IEnumerable A_0)
		{
			return i2.a(A_0, "[", "]", ",", "null");
		}

		// Token: 0x06002222 RID: 8738 RVA: 0x0008BD50 File Offset: 0x0008AD50
		public static string a(IEnumerable A_0, string A_1)
		{
			return i2.a(A_0, string.Empty, string.Empty, A_1, string.Empty);
		}

		// Token: 0x06002223 RID: 8739 RVA: 0x0008BD68 File Offset: 0x0008AD68
		public static string a(IEnumerable A_0, string A_1, string A_2, string A_3, string A_4)
		{
			if (A_0 == null)
			{
				throw new ArgumentNullException("enumerable");
			}
			StringBuilder stringBuilder = new StringBuilder(A_1);
			bool flag = true;
			foreach (object obj in A_0)
			{
				if (obj != null || !string.IsNullOrEmpty(A_4))
				{
					if (flag)
					{
						flag = false;
					}
					else
					{
						stringBuilder.Append(A_3);
					}
					if (obj == null)
					{
						stringBuilder.Append(A_4);
					}
					else if (obj is DictionaryEntry)
					{
						DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
						stringBuilder.Append(dictionaryEntry.Key.ToString());
						stringBuilder.Append("=");
						stringBuilder.Append((dictionaryEntry.Value == null) ? A_4 : dictionaryEntry.Value.ToString());
					}
					else
					{
						stringBuilder.Append(obj.ToString());
					}
				}
			}
			stringBuilder.Append(A_2);
			return stringBuilder.ToString();
		}
	}
}
