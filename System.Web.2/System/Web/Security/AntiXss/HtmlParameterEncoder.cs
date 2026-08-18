using System;
using System.Collections;
using System.Text;

namespace System.Web.Security.AntiXss
{
	// Token: 0x0200061A RID: 1562
	internal static class HtmlParameterEncoder
	{
		// Token: 0x06004DEE RID: 19950 RVA: 0x0010E9F2 File Offset: 0x0010CBF2
		internal static string QueryStringParameterEncode(string s, Encoding encoding)
		{
			return HtmlParameterEncoder.FormQueryEncode(s, encoding, EncodingType.QueryString);
		}

		// Token: 0x06004DEF RID: 19951 RVA: 0x0010E9FC File Offset: 0x0010CBFC
		internal static string FormStringParameterEncode(string s, Encoding encoding)
		{
			return HtmlParameterEncoder.FormQueryEncode(s, encoding, EncodingType.HtmlForm);
		}

		// Token: 0x06004DF0 RID: 19952 RVA: 0x0010EA06 File Offset: 0x0010CC06
		private static string FormQueryEncode(string s, Encoding encoding, EncodingType encodingType)
		{
			return HtmlParameterEncoder.FormQueryEncode(s, encoding, encodingType, HtmlParameterEncoder.characterValuesLazy);
		}

		// Token: 0x06004DF1 RID: 19953 RVA: 0x0010EA18 File Offset: 0x0010CC18
		private static string FormQueryEncode(string s, Encoding encoding, EncodingType encodingType, Lazy<char[][]> characterValuesLazy)
		{
			if (string.IsNullOrEmpty(s))
			{
				return s;
			}
			if (encoding == null)
			{
				throw new ArgumentNullException("encoding");
			}
			char[][] value = characterValuesLazy.Value;
			byte[] bytes = encoding.GetBytes(s.ToCharArray());
			char[] array = new char[bytes.Length * 3];
			int length = 0;
			foreach (byte b in bytes)
			{
				if (b == 0 || b == 32 || (int)b > value.Length || value[(int)b] != null)
				{
					char[] array2;
					if (b == 32)
					{
						if (encodingType != EncodingType.QueryString)
						{
							if (encodingType != EncodingType.HtmlForm)
							{
								throw new ArgumentOutOfRangeException("encodingType");
							}
							array2 = HtmlParameterEncoder.FormStringSpace;
						}
						else
						{
							array2 = HtmlParameterEncoder.QueryStringSpace;
						}
					}
					else
					{
						array2 = value[(int)b];
					}
					for (int j = 0; j < array2.Length; j++)
					{
						array[length++] = array2[j];
					}
				}
				else
				{
					array[length++] = (char)b;
				}
			}
			return new string(array, 0, length);
		}

		// Token: 0x06004DF2 RID: 19954 RVA: 0x0010EAF8 File Offset: 0x0010CCF8
		private static char[][] InitialiseSafeList()
		{
			char[][] result = SafeList.Generate(255, new SafeList.GenerateSafeValue(SafeList.PercentThenHexValueGenerator));
			SafeList.PunchSafeList(ref result, HtmlParameterEncoder.UrlParameterSafeList());
			return result;
		}

		// Token: 0x06004DF3 RID: 19955 RVA: 0x0010EB29 File Offset: 0x0010CD29
		private static IEnumerable UrlParameterSafeList()
		{
			yield return 45;
			yield return 46;
			int num;
			for (int i = 48; i <= 57; i = num + 1)
			{
				yield return i;
				num = i;
			}
			for (int i = 65; i <= 90; i = num + 1)
			{
				yield return i;
				num = i;
			}
			yield return 95;
			for (int i = 97; i <= 122; i = num + 1)
			{
				yield return i;
				num = i;
			}
			yield return 126;
			yield break;
		}

		// Token: 0x06004DF4 RID: 19956 RVA: 0x0010EB32 File Offset: 0x0010CD32
		internal static string UrlPathEncode(string s, Encoding encoding)
		{
			return HtmlParameterEncoder.FormQueryEncode(s, encoding, EncodingType.QueryString, HtmlParameterEncoder.pathCharacterValuesLazy);
		}

		// Token: 0x06004DF5 RID: 19957 RVA: 0x0010EB44 File Offset: 0x0010CD44
		private static char[][] InitialisePathSafeList()
		{
			char[][] result = SafeList.Generate(255, new SafeList.GenerateSafeValue(SafeList.PercentThenHexValueGenerator));
			SafeList.PunchSafeList(ref result, HtmlParameterEncoder.UrlPathSafeList());
			return result;
		}

		// Token: 0x06004DF6 RID: 19958 RVA: 0x0010EB75 File Offset: 0x0010CD75
		private static IEnumerable UrlPathSafeList()
		{
			foreach (object obj in HtmlParameterEncoder.UrlParameterSafeList())
			{
				yield return obj;
			}
			IEnumerator enumerator = null;
			yield return 35;
			yield return 37;
			yield return 47;
			yield return 92;
			yield return 40;
			yield return 41;
			yield break;
			yield break;
		}

		// Token: 0x04002A3E RID: 10814
		private static readonly char[] QueryStringSpace = "%20".ToCharArray();

		// Token: 0x04002A3F RID: 10815
		private static readonly char[] FormStringSpace = "+".ToCharArray();

		// Token: 0x04002A40 RID: 10816
		private static Lazy<char[][]> characterValuesLazy = new Lazy<char[][]>(new Func<char[][]>(HtmlParameterEncoder.InitialiseSafeList));

		// Token: 0x04002A41 RID: 10817
		private static Lazy<char[][]> pathCharacterValuesLazy = new Lazy<char[][]>(new Func<char[][]>(HtmlParameterEncoder.InitialisePathSafeList));
	}
}
