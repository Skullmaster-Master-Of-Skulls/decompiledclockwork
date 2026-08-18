using System;
using System.Collections;
using System.Text;

namespace System.Web.Security.AntiXss
{
	// Token: 0x02000618 RID: 1560
	internal static class CssEncoder
	{
		// Token: 0x06004DEA RID: 19946 RVA: 0x0010E91C File Offset: 0x0010CB1C
		internal static string Encode(string input)
		{
			if (string.IsNullOrEmpty(input))
			{
				return input;
			}
			char[][] value = CssEncoder.characterValuesLazy.Value;
			StringBuilder outputStringBuilder = EncoderUtil.GetOutputStringBuilder(input.Length, 7);
			Utf16StringReader utf16StringReader = new Utf16StringReader(input);
			for (;;)
			{
				int num = utf16StringReader.ReadNextScalarValue();
				if (num < 0)
				{
					break;
				}
				if (num >= value.Length)
				{
					char[] value2 = SafeList.SlashThenSixDigitHexValueGenerator(num);
					outputStringBuilder.Append(value2);
				}
				else if (value[num] != null)
				{
					char[] value3 = value[num];
					outputStringBuilder.Append(value3);
				}
				else
				{
					outputStringBuilder.Append((char)num);
				}
			}
			return outputStringBuilder.ToString();
		}

		// Token: 0x06004DEB RID: 19947 RVA: 0x0010E9A0 File Offset: 0x0010CBA0
		private static char[][] InitialiseSafeList()
		{
			char[][] result = SafeList.Generate(255, new SafeList.GenerateSafeValue(SafeList.SlashThenSixDigitHexValueGenerator));
			SafeList.PunchSafeList(ref result, CssEncoder.CssSafeList());
			return result;
		}

		// Token: 0x06004DEC RID: 19948 RVA: 0x0010E9D1 File Offset: 0x0010CBD1
		private static IEnumerable CssSafeList()
		{
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
			for (int i = 97; i <= 122; i = num + 1)
			{
				yield return i;
				num = i;
			}
			yield break;
		}

		// Token: 0x04002A3A RID: 10810
		private static Lazy<char[][]> characterValuesLazy = new Lazy<char[][]>(new Func<char[][]>(CssEncoder.InitialiseSafeList));
	}
}
