using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using TechnoPro.Common.DataStructure;

namespace TechnoPro.Common.Public.Adapters
{
	// Token: 0x020005F3 RID: 1523
	public static class StringAdapter
	{
		// Token: 0x060030DF RID: 12511 RVA: 0x000431D0 File Offset: 0x000413D0
		public static int GetIntFromString(this string s, int defaultValue = 0)
		{
			bool flag = string.IsNullOrEmpty(s);
			int result;
			if (flag)
			{
				result = defaultValue;
			}
			else
			{
				int num;
				result = ((!int.TryParse(s, out num)) ? defaultValue : num);
			}
			return result;
		}

		// Token: 0x060030E0 RID: 12512 RVA: 0x00043200 File Offset: 0x00041400
		public static T GetEnumFromStringHoldingEnumIntValue<T>(this string s, T defaultValue) where T : struct
		{
			bool flag = string.IsNullOrEmpty(s);
			T result;
			if (flag)
			{
				result = defaultValue;
			}
			else
			{
				int intFromString = s.GetIntFromString(0);
				bool flag2 = Enum.IsDefined(typeof(T), intFromString);
				if (flag2)
				{
					result = (T)((object)intFromString);
				}
				else
				{
					result = defaultValue;
				}
			}
			return result;
		}

		// Token: 0x060030E1 RID: 12513 RVA: 0x00043250 File Offset: 0x00041450
		public static IList<string> Guess(this string item, IList<string> lookup)
		{
			List<Pair<string, double>> list = (from g in lookup
			select new Pair<string, double>(g, g.GetDamerauLevenshteinDistanceScore(item)) into g
			where g.Item2 >= 0.6
			select g).ToList<Pair<string, double>>();
			list.Sort((Pair<string, double> g1, Pair<string, double> g2) => g2.Item2.CompareTo(g1.Item2));
			return (from g in list
			select g.Item1).ToList<string>();
		}

		// Token: 0x060030E2 RID: 12514 RVA: 0x000432FC File Offset: 0x000414FC
		public static bool Like(this string arg, string comparison)
		{
			double damerauLevenshteinDistanceScore = arg.GetDamerauLevenshteinDistanceScore(comparison);
			return damerauLevenshteinDistanceScore >= 0.6;
		}

		// Token: 0x060030E3 RID: 12515 RVA: 0x00043328 File Offset: 0x00041528
		public static double GetDamerauLevenshteinDistanceScore(this string str1, string str2)
		{
			int num = str1.DamerauLevenshteinDistance(str2);
			int num2 = (str1.Length > str2.Length) ? str1.Length : str2.Length;
			return 1.0 - (double)num / (double)num2;
		}

		// Token: 0x060030E4 RID: 12516 RVA: 0x00043370 File Offset: 0x00041570
		public static int DamerauLevenshteinDistance(this string str1, string str2)
		{
			bool flag = str1 == str2;
			int result;
			if (flag)
			{
				result = 0;
			}
			else
			{
				bool flag2 = string.IsNullOrEmpty(str1) || string.IsNullOrEmpty(str2);
				if (flag2)
				{
					result = (str1 ?? string.Empty).Length + (str2 ?? string.Empty).Length;
				}
				else
				{
					bool flag3 = str1.Length > str2.Length;
					if (flag3)
					{
						string text = str1;
						str1 = str2;
						str2 = text;
					}
					bool flag4 = str2.Contains(str1);
					if (flag4)
					{
						result = str2.Length - str1.Length;
					}
					else
					{
						int length = str1.Length;
						int length2 = str2.Length;
						int[,] array = new int[length + 2, length2 + 2];
						int num = length + length2;
						array[0, 0] = num;
						for (int i = 0; i <= length; i++)
						{
							array[i + 1, 1] = i;
							array[i + 1, 0] = num;
						}
						for (int j = 0; j <= length2; j++)
						{
							array[1, j + 1] = j;
							array[0, j + 1] = num;
						}
						SortedDictionary<char, int> sortedDictionary = new SortedDictionary<char, int>();
						foreach (char key in str1 + str2)
						{
							bool flag5 = !sortedDictionary.ContainsKey(key);
							if (flag5)
							{
								sortedDictionary.Add(key, 0);
							}
						}
						for (int l = 1; l <= length; l++)
						{
							int num2 = 0;
							for (int m = 1; m <= length2; m++)
							{
								int num3 = sortedDictionary[str2[m - 1]];
								int num4 = num2;
								bool flag6 = str1[l - 1] == str2[m - 1];
								if (flag6)
								{
									array[l + 1, m + 1] = array[l, m];
									num2 = m;
								}
								else
								{
									array[l + 1, m + 1] = Math.Min(array[l, m], Math.Min(array[l + 1, m], array[l, m + 1])) + 1;
								}
								array[l + 1, m + 1] = Math.Min(array[l + 1, m + 1], array[num3, num4] + (l - num3 - 1) + 1 + (m - num4 - 1));
							}
							sortedDictionary[str1[l - 1]] = l;
						}
						result = array[length + 1, length2 + 1];
					}
				}
			}
			return result;
		}

		// Token: 0x060030E5 RID: 12517 RVA: 0x00043628 File Offset: 0x00041828
		public static SecureString ToSecureString(this string s)
		{
			SecureString secureString = new SecureString();
			foreach (char c in s)
			{
				secureString.AppendChar(c);
			}
			return secureString;
		}

		// Token: 0x060030E6 RID: 12518 RVA: 0x00043668 File Offset: 0x00041868
		public static string EscapeXml(this string s)
		{
			bool flag = string.IsNullOrEmpty(s);
			string result;
			if (flag)
			{
				result = s;
			}
			else
			{
				result = ((!SecurityElement.IsValidText(s)) ? SecurityElement.Escape(s) : s);
			}
			return result;
		}

		// Token: 0x060030E7 RID: 12519 RVA: 0x0004369C File Offset: 0x0004189C
		public static string UnEscapeXml(this string s)
		{
			bool flag = string.IsNullOrEmpty(s) || !s.Contains('&');
			string result;
			if (flag)
			{
				result = s;
			}
			else
			{
				string text = s.Replace("&apos;", "'");
				text = text.Replace("&quot;", "\"");
				text = text.Replace("&gt;", ">");
				text = text.Replace("&lt;", "<");
				text = text.Replace("&amp;", "&");
				result = text;
			}
			return result;
		}
	}
}
