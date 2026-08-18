using System;

namespace System.Web.Util
{
	// Token: 0x020001CE RID: 462
	internal static class Utf16StringValidator
	{
		// Token: 0x0600176F RID: 5999 RVA: 0x000498DF File Offset: 0x00047ADF
		public static string ValidateString(string input)
		{
			return Utf16StringValidator.ValidateString(input, Utf16StringValidator._skipUtf16Validation);
		}

		// Token: 0x06001770 RID: 6000 RVA: 0x000498EC File Offset: 0x00047AEC
		internal static string ValidateString(string input, bool skipUtf16Validation)
		{
			if (skipUtf16Validation || string.IsNullOrEmpty(input))
			{
				return input;
			}
			int num = -1;
			for (int i = 0; i < input.Length; i++)
			{
				if (char.IsSurrogate(input[i]))
				{
					num = i;
					break;
				}
			}
			if (num < 0)
			{
				return input;
			}
			char[] array = input.ToCharArray();
			for (int j = num; j < array.Length; j++)
			{
				char c = array[j];
				if (char.IsLowSurrogate(c))
				{
					array[j] = '�';
				}
				else if (char.IsHighSurrogate(c))
				{
					if (j + 1 < array.Length && char.IsLowSurrogate(array[j + 1]))
					{
						j++;
					}
					else
					{
						array[j] = '�';
					}
				}
			}
			return new string(array);
		}

		// Token: 0x0400170E RID: 5902
		private const char UNICODE_NULL_CHAR = '\0';

		// Token: 0x0400170F RID: 5903
		private const char UNICODE_REPLACEMENT_CHAR = '�';

		// Token: 0x04001710 RID: 5904
		private static readonly bool _skipUtf16Validation = AppSettings.AllowRelaxedUnicodeDecoding;
	}
}
