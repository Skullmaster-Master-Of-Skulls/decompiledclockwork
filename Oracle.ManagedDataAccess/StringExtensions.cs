using System;
using System.Text;

// Token: 0x0200002F RID: 47
internal static class StringExtensions
{
	// Token: 0x06000286 RID: 646 RVA: 0x0000DC60 File Offset: 0x0000BE60
	internal static bool Contains(this string str, string subString, StringComparison compareType)
	{
		return !string.IsNullOrEmpty(str) && str.IndexOf(subString, compareType) >= 0;
	}

	// Token: 0x06000287 RID: 647 RVA: 0x0000DC7C File Offset: 0x0000BE7C
	internal static string ReplaceXmlChars(this string str)
	{
		return StringExtensions.Replace(str, StringExtensions.sCharsToMatch, StringExtensions.sStringsToReplaceWith);
	}

	// Token: 0x06000288 RID: 648 RVA: 0x0000DC90 File Offset: 0x0000BE90
	internal static int CompareWithAmpersand(char[] strArray, int index, int length, int ampArrayIndex)
	{
		int length2 = StringExtensions.sStringsToReplaceWith[ampArrayIndex].Length;
		if (length - index < length2)
		{
			return 0;
		}
		for (int i = index; i < index + length2; i++)
		{
			if (StringExtensions.sStringsToReplaceWith[ampArrayIndex][i - index] != strArray[i])
			{
				return 0;
			}
		}
		return length2;
	}

	// Token: 0x06000289 RID: 649 RVA: 0x0000DCD8 File Offset: 0x0000BED8
	internal static int CompareWithAmpersand(char[] strArray, int index, int length, out int charsToSkip)
	{
		charsToSkip = 1;
		for (int i = 0; i < StringExtensions.sStringsToReplaceWith.Length; i++)
		{
			int num;
			if ((num = StringExtensions.CompareWithAmpersand(strArray, index, length, i)) > 0)
			{
				charsToSkip = num;
				return i;
			}
		}
		return 0;
	}

	// Token: 0x0600028A RID: 650 RVA: 0x0000DD10 File Offset: 0x0000BF10
	private static string Replace(string str, char[] charsToMatch, string[] stringsToReplaceWith)
	{
		if (string.IsNullOrEmpty(str))
		{
			return string.Empty;
		}
		int i;
		if ((i = str.IndexOfAny(charsToMatch)) == -1)
		{
			return str;
		}
		StringBuilder stringBuilder = new StringBuilder(str.Length + 100);
		char[] array = str.ToCharArray();
		if (i != 0)
		{
			stringBuilder.Append(array, 0, i);
		}
		while (i < array.Length)
		{
			bool flag = false;
			int j = 0;
			while (j < charsToMatch.Length)
			{
				if (array[i] == charsToMatch[j])
				{
					if (array[i] == '&')
					{
						int num = 1;
						int num2;
						if ((num2 = StringExtensions.CompareWithAmpersand(array, i, str.Length, out num)) == 0)
						{
							stringBuilder.Append(stringsToReplaceWith[j]);
						}
						else
						{
							stringBuilder.Append(stringsToReplaceWith[num2]);
						}
						i += num;
						flag = true;
						break;
					}
					stringBuilder.Append(stringsToReplaceWith[j]);
					flag = true;
					i++;
					break;
				}
				else
				{
					j++;
				}
			}
			if (!flag)
			{
				stringBuilder.Append(array[i]);
				i++;
			}
		}
		return stringBuilder.ToString();
	}

	// Token: 0x040002FE RID: 766
	private static readonly char[] sCharsToMatch = new char[]
	{
		'<',
		'>',
		'&',
		'\'',
		'"'
	};

	// Token: 0x040002FF RID: 767
	private static readonly string[] sStringsToReplaceWith = new string[]
	{
		"&lt;",
		"&gt;",
		"&amp;",
		"&apos;",
		"&quot;"
	};
}
