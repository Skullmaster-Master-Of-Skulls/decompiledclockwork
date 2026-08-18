using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x02000107 RID: 263
	internal static class SqlDataSourceCommandParser
	{
		// Token: 0x0600094A RID: 2378 RVA: 0x000358AC File Offset: 0x00033AAC
		private static bool ConsumeField(string s, int startIndex, List<string> parts)
		{
			while (startIndex < s.Length && char.IsWhiteSpace(s, startIndex))
			{
				startIndex++;
			}
			string item;
			startIndex = SqlDataSourceCommandParser.ConsumeIdentifier(s, startIndex, out item);
			parts.Add(item);
			return SqlDataSourceCommandParser.ExpectField(s, startIndex, parts);
		}

		// Token: 0x0600094B RID: 2379 RVA: 0x000358EC File Offset: 0x00033AEC
		private static bool ConsumeFrom(string s, int startIndex, List<string> parts)
		{
			while (startIndex < s.Length && char.IsWhiteSpace(s, startIndex))
			{
				startIndex++;
			}
			return startIndex + 5 < s.Length && (string.Compare(s, startIndex, "from", 0, 4, StringComparison.OrdinalIgnoreCase) == 0 && char.IsWhiteSpace(s, startIndex + 4)) && SqlDataSourceCommandParser.ConsumeTable(s, startIndex + 5, parts);
		}

		// Token: 0x0600094C RID: 2380 RVA: 0x00035948 File Offset: 0x00033B48
		private static int ConsumeIdentifier(string s, int startIndex, out string identifier)
		{
			bool flag = false;
			identifier = string.Empty;
			while (startIndex < s.Length)
			{
				if (!flag && s[startIndex] == '[')
				{
					flag = true;
					identifier += s[startIndex].ToString();
					startIndex++;
				}
				else if (flag && s[startIndex] == ']')
				{
					flag = false;
					identifier += s[startIndex].ToString();
					startIndex++;
				}
				else if (flag)
				{
					identifier += s[startIndex].ToString();
					startIndex++;
				}
				else
				{
					if (char.IsWhiteSpace(s, startIndex) || s[startIndex] == ',')
					{
						break;
					}
					identifier += s[startIndex].ToString();
					startIndex++;
				}
			}
			return startIndex;
		}

		// Token: 0x0600094D RID: 2381 RVA: 0x00035A24 File Offset: 0x00033C24
		private static bool ConsumeSelect(string s, int startIndex, List<string> parts)
		{
			return s.Length >= 7 && s.ToLowerInvariant().StartsWith("select", StringComparison.Ordinal) && char.IsWhiteSpace(s, 6) && SqlDataSourceCommandParser.ConsumeField(s, startIndex + 7, parts);
		}

		// Token: 0x0600094E RID: 2382 RVA: 0x00035A5C File Offset: 0x00033C5C
		private static bool ConsumeTable(string s, int startIndex, List<string> parts)
		{
			while (startIndex < s.Length && char.IsWhiteSpace(s, startIndex))
			{
				startIndex++;
			}
			string item;
			startIndex = SqlDataSourceCommandParser.ConsumeIdentifier(s, startIndex, out item);
			parts.Add(item);
			return startIndex == s.Length;
		}

		// Token: 0x0600094F RID: 2383 RVA: 0x00035AA0 File Offset: 0x00033CA0
		private static bool ExpectField(string s, int startIndex, List<string> parts)
		{
			while (startIndex < s.Length && char.IsWhiteSpace(s, startIndex))
			{
				startIndex++;
			}
			if (startIndex >= s.Length - 1)
			{
				return false;
			}
			if (s[startIndex] == ',')
			{
				return SqlDataSourceCommandParser.ConsumeField(s, startIndex + 1, parts);
			}
			return SqlDataSourceCommandParser.ConsumeFrom(s, startIndex, parts);
		}

		// Token: 0x06000950 RID: 2384 RVA: 0x00035AF4 File Offset: 0x00033CF4
		private static string[] GetIdentifierParts(string identifier)
		{
			bool flag = false;
			StringBuilder stringBuilder = new StringBuilder();
			ArrayList arrayList = new ArrayList();
			for (int i = 0; i < identifier.Length; i++)
			{
				char c = identifier[i];
				if (c != '.')
				{
					if (c != '[')
					{
						if (c != ']')
						{
							if (!flag)
							{
								if (c <= '*')
								{
									if (c == '#' || c == '*')
									{
										goto IL_C9;
									}
								}
								else if (c == '@' || c == '_')
								{
									goto IL_C9;
								}
								if (!char.IsLetter(c) && (stringBuilder.Length <= 0 || (c != '$' && !char.IsDigit(c))))
								{
									return null;
								}
							}
							IL_C9:
							stringBuilder.Append(c);
						}
						else
						{
							if (!flag || (identifier.Length > i + 2 && identifier[i + 1] != '.'))
							{
								return null;
							}
							flag = false;
						}
					}
					else
					{
						if (flag)
						{
							return null;
						}
						flag = true;
					}
				}
				else if (flag)
				{
					stringBuilder.Append('.');
				}
				else
				{
					arrayList.Add(stringBuilder.ToString());
					stringBuilder.Length = 0;
				}
			}
			arrayList.Add(stringBuilder.ToString());
			return (string[])arrayList.ToArray(typeof(string));
		}

		// Token: 0x06000951 RID: 2385 RVA: 0x00035C08 File Offset: 0x00033E08
		public static string GetLastIdentifierPart(string identifier)
		{
			string[] identifierParts = SqlDataSourceCommandParser.GetIdentifierParts(identifier);
			if (identifierParts == null || identifierParts.Length == 0)
			{
				return null;
			}
			return identifierParts[identifierParts.Length - 1];
		}

		// Token: 0x06000952 RID: 2386 RVA: 0x00035C2C File Offset: 0x00033E2C
		public static string[] ParseSqlString(string sqlString)
		{
			if (string.IsNullOrEmpty(sqlString))
			{
				return null;
			}
			string[] result;
			try
			{
				sqlString = sqlString.Trim();
				List<string> list = new List<string>();
				result = (SqlDataSourceCommandParser.ConsumeSelect(sqlString, 0, list) ? list.ToArray() : null);
			}
			catch (Exception ex)
			{
				result = null;
			}
			return result;
		}
	}
}
