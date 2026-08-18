using System;
using System.Net.Mime;

namespace System.Net.Mail
{
	// Token: 0x0200025A RID: 602
	internal static class DomainLiteralReader
	{
		// Token: 0x06001702 RID: 5890 RVA: 0x00076444 File Offset: 0x00074644
		internal static int ReadReverse(string data, int index)
		{
			index--;
			for (;;)
			{
				index = WhitespaceReader.ReadFwsReverse(data, index);
				if (index < 0)
				{
					goto IL_83;
				}
				int num = QuotedPairReader.CountQuotedChars(data, index, false);
				if (num > 0)
				{
					index -= num;
				}
				else
				{
					if (data[index] == MailBnfHelper.StartSquareBracket)
					{
						break;
					}
					if ((int)data[index] > MailBnfHelper.Ascii7bitMaxValue || !MailBnfHelper.Dtext[(int)data[index]])
					{
						goto IL_55;
					}
					index--;
				}
				if (index < 0)
				{
					goto IL_83;
				}
			}
			return index - 1;
			IL_55:
			throw new FormatException(SR.GetString("MailHeaderFieldInvalidCharacter", new object[]
			{
				data[index]
			}));
			IL_83:
			throw new FormatException(SR.GetString("MailHeaderFieldInvalidCharacter", new object[]
			{
				MailBnfHelper.EndSquareBracket
			}));
		}
	}
}
