using System;
using System.Net.Mime;

namespace System.Net.Mail
{
	// Token: 0x02000274 RID: 628
	internal static class QuotedPairReader
	{
		// Token: 0x060017B4 RID: 6068 RVA: 0x00078D28 File Offset: 0x00076F28
		internal static int CountQuotedChars(string data, int index, bool permitUnicodeEscaping)
		{
			if (index <= 0 || data[index - 1] != MailBnfHelper.Backslash)
			{
				return 0;
			}
			int num = QuotedPairReader.CountBackslashes(data, index - 1);
			if (num % 2 == 0)
			{
				return 0;
			}
			if (!permitUnicodeEscaping && (int)data[index] > MailBnfHelper.Ascii7bitMaxValue)
			{
				throw new FormatException(SR.GetString("MailHeaderFieldInvalidCharacter", new object[]
				{
					data[index]
				}));
			}
			return num + 1;
		}

		// Token: 0x060017B5 RID: 6069 RVA: 0x00078D98 File Offset: 0x00076F98
		private static int CountBackslashes(string data, int index)
		{
			int num = 0;
			do
			{
				num++;
				index--;
			}
			while (index >= 0 && data[index] == MailBnfHelper.Backslash);
			return num;
		}
	}
}
