using System;

namespace System.Web.Security.AntiXss
{
	// Token: 0x02000612 RID: 1554
	internal struct Utf16StringReader
	{
		// Token: 0x06004DE6 RID: 19942 RVA: 0x0010E82B File Offset: 0x0010CA2B
		public Utf16StringReader(string input)
		{
			this._input = input;
			this._currentOffset = 0;
		}

		// Token: 0x06004DE7 RID: 19943 RVA: 0x0010E83B File Offset: 0x0010CA3B
		private static int ConvertToUtf32(char leadingSurrogate, char trailingSurrogate)
		{
			return (int)((leadingSurrogate - '\ud800') * 'Ѐ' + (trailingSurrogate - '\udc00')) + 65536;
		}

		// Token: 0x06004DE8 RID: 19944 RVA: 0x0010E858 File Offset: 0x0010CA58
		private static bool IsValidUnicodeScalarValue(int codePoint)
		{
			return (0 <= codePoint && codePoint <= 55295) || (57344 <= codePoint && codePoint <= 1114111);
		}

		// Token: 0x06004DE9 RID: 19945 RVA: 0x0010E880 File Offset: 0x0010CA80
		public int ReadNextScalarValue()
		{
			if (this._currentOffset >= this._input.Length)
			{
				return -1;
			}
			string input = this._input;
			int currentOffset = this._currentOffset;
			this._currentOffset = currentOffset + 1;
			char c = input[currentOffset];
			int num = (int)c;
			if (char.IsHighSurrogate(c) && this._currentOffset < this._input.Length)
			{
				char c2 = this._input[this._currentOffset];
				if (char.IsLowSurrogate(c2))
				{
					this._currentOffset++;
					num = Utf16StringReader.ConvertToUtf32(c, c2);
				}
			}
			if (Utf16StringReader.IsValidUnicodeScalarValue(num))
			{
				return num;
			}
			return 65533;
		}

		// Token: 0x04002998 RID: 10648
		private const char LeadingSurrogateStart = '\ud800';

		// Token: 0x04002999 RID: 10649
		private const char TrailingSurrogateStart = '\udc00';

		// Token: 0x0400299A RID: 10650
		private const int UnicodeReplacementCharacterCodePoint = 65533;

		// Token: 0x0400299B RID: 10651
		private int _currentOffset;

		// Token: 0x0400299C RID: 10652
		private readonly string _input;
	}
}
