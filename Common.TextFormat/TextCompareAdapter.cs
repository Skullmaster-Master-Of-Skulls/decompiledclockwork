using System;

namespace TechnoPro.Common.TextFormat.Adapters
{
	// Token: 0x02000003 RID: 3
	public static class TextCompareAdapter
	{
		// Token: 0x06000007 RID: 7 RVA: 0x0000216C File Offset: 0x0000036C
		public static int GetDistance(this string s1, string s2)
		{
			int maxOffset = 5;
			float num = TextCompareAdapter.Distance(s1, s2, maxOffset);
			num *= 100f;
			return Convert.ToInt32(num);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002198 File Offset: 0x00000398
		public static int GetDistanceSlowerButMoreAccurate(this string s1, string s2)
		{
			return TextCompareAdapter.Compute(s1, s2);
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000021B4 File Offset: 0x000003B4
		private static float Distance(string s1, string s2, int maxOffset)
		{
			bool flag = string.IsNullOrEmpty(s1);
			float result;
			if (flag)
			{
				result = (float)(string.IsNullOrEmpty(s2) ? 0 : s2.Length);
			}
			else
			{
				bool flag2 = string.IsNullOrEmpty(s2);
				if (flag2)
				{
					result = (float)s1.Length;
				}
				else
				{
					int num = 0;
					int num2 = 0;
					int num3 = 0;
					int num4 = 0;
					while (num + num2 < s1.Length && num + num3 < s2.Length)
					{
						bool flag3 = s1[num + num2] == s2[num + num3];
						if (flag3)
						{
							num4++;
						}
						else
						{
							num2 = 0;
							num3 = 0;
							for (int i = 0; i < maxOffset; i++)
							{
								bool flag4 = num + i < s1.Length && s1[num + i] == s2[num];
								if (flag4)
								{
									num2 = i;
									break;
								}
								bool flag5 = num + i < s2.Length && s1[num] == s2[num + i];
								if (flag5)
								{
									num3 = i;
									break;
								}
							}
						}
						num++;
					}
					result = (float)((s1.Length + s2.Length) / 2 - num4);
				}
			}
			return result;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000022E8 File Offset: 0x000004E8
		public static int Compute(string s, string t)
		{
			int length = s.Length;
			int length2 = t.Length;
			int[,] array = new int[length + 1, length2 + 1];
			bool flag = length == 0;
			int result;
			if (flag)
			{
				result = length2;
			}
			else
			{
				bool flag2 = length2 == 0;
				if (flag2)
				{
					result = length;
				}
				else
				{
					int i = 0;
					while (i <= length)
					{
						array[i, 0] = i++;
					}
					int j = 0;
					while (j <= length2)
					{
						array[0, j] = j++;
					}
					for (int k = 1; k <= length; k++)
					{
						for (int l = 1; l <= length2; l++)
						{
							int num = (t[l - 1] == s[k - 1]) ? 0 : 1;
							array[k, l] = Math.Min(Math.Min(array[k - 1, l] + 1, array[k, l - 1] + 1), array[k - 1, l - 1] + num);
						}
					}
					result = array[length, length2];
				}
			}
			return result;
		}
	}
}
