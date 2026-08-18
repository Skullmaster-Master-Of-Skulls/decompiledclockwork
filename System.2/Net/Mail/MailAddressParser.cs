using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Text;

namespace System.Net.Mail
{
	// Token: 0x0200026C RID: 620
	internal static class MailAddressParser
	{
		// Token: 0x06001754 RID: 5972 RVA: 0x00077044 File Offset: 0x00075244
		internal static MailAddress ParseAddress(string data)
		{
			int num = data.Length - 1;
			return MailAddressParser.ParseAddress(data, false, ref num);
		}

		// Token: 0x06001755 RID: 5973 RVA: 0x00077068 File Offset: 0x00075268
		internal static IList<MailAddress> ParseMultipleAddresses(string data)
		{
			IList<MailAddress> list = new List<MailAddress>();
			for (int i = data.Length - 1; i >= 0; i--)
			{
				list.Insert(0, MailAddressParser.ParseAddress(data, true, ref i));
			}
			return list;
		}

		// Token: 0x06001756 RID: 5974 RVA: 0x000770A0 File Offset: 0x000752A0
		private static MailAddress ParseAddress(string data, bool expectMultipleAddresses, ref int index)
		{
			if (!ServicePointManager.AllowNewLineInMailAddress && MailBnfHelper.HasCROrLF(data))
			{
				throw new FormatException(SR.GetString("MailAddressInvalidFormat"));
			}
			index = MailAddressParser.ReadCfwsAndThrowIfIncomplete(data, index);
			bool flag = false;
			if (data[index] == MailBnfHelper.EndAngleBracket)
			{
				flag = true;
				index--;
			}
			string domain = MailAddressParser.ParseDomain(data, ref index);
			if (data[index] != MailBnfHelper.At)
			{
				throw new FormatException(SR.GetString("MailAddressInvalidFormat"));
			}
			index--;
			string userName = MailAddressParser.ParseLocalPart(data, ref index, flag, expectMultipleAddresses);
			if (flag)
			{
				if (index < 0 || data[index] != MailBnfHelper.StartAngleBracket)
				{
					throw new FormatException(SR.GetString("MailHeaderFieldInvalidCharacter", new object[]
					{
						(index >= 0) ? data[index] : MailBnfHelper.EndAngleBracket
					}));
				}
				index--;
				index = WhitespaceReader.ReadFwsReverse(data, index);
			}
			string displayName;
			if (index >= 0 && (!expectMultipleAddresses || data[index] != MailBnfHelper.Comma))
			{
				displayName = MailAddressParser.ParseDisplayName(data, ref index, expectMultipleAddresses);
			}
			else
			{
				displayName = string.Empty;
			}
			return new MailAddress(displayName, userName, domain);
		}

		// Token: 0x06001757 RID: 5975 RVA: 0x000771B7 File Offset: 0x000753B7
		private static int ReadCfwsAndThrowIfIncomplete(string data, int index)
		{
			index = WhitespaceReader.ReadCfwsReverse(data, index);
			if (index < 0)
			{
				throw new FormatException(SR.GetString("MailAddressInvalidFormat"));
			}
			return index;
		}

		// Token: 0x06001758 RID: 5976 RVA: 0x000771D8 File Offset: 0x000753D8
		private static string ParseDomain(string data, ref int index)
		{
			index = MailAddressParser.ReadCfwsAndThrowIfIncomplete(data, index);
			int num = index;
			if (data[index] == MailBnfHelper.EndSquareBracket)
			{
				index = DomainLiteralReader.ReadReverse(data, index);
			}
			else
			{
				index = DotAtomReader.ReadReverse(data, index);
			}
			string input = data.Substring(index + 1, num - index);
			index = MailAddressParser.ReadCfwsAndThrowIfIncomplete(data, index);
			return MailAddressParser.NormalizeOrThrow(input);
		}

		// Token: 0x06001759 RID: 5977 RVA: 0x00077238 File Offset: 0x00075438
		private static string ParseLocalPart(string data, ref int index, bool expectAngleBracket, bool expectMultipleAddresses)
		{
			index = MailAddressParser.ReadCfwsAndThrowIfIncomplete(data, index);
			int num = index;
			if (data[index] == MailBnfHelper.Quote)
			{
				index = QuotedStringFormatReader.ReadReverseQuoted(data, index, true);
			}
			else
			{
				index = DotAtomReader.ReadReverse(data, index);
				if (index >= 0 && !MailBnfHelper.Whitespace.Contains(data[index]) && data[index] != MailBnfHelper.EndComment && (!expectAngleBracket || data[index] != MailBnfHelper.StartAngleBracket) && (!expectMultipleAddresses || data[index] != MailBnfHelper.Comma) && data[index] != MailBnfHelper.Quote)
				{
					throw new FormatException(SR.GetString("MailHeaderFieldInvalidCharacter", new object[]
					{
						data[index]
					}));
				}
			}
			string input = data.Substring(index + 1, num - index);
			index = WhitespaceReader.ReadCfwsReverse(data, index);
			return MailAddressParser.NormalizeOrThrow(input);
		}

		// Token: 0x0600175A RID: 5978 RVA: 0x0007731C File Offset: 0x0007551C
		private static string ParseDisplayName(string data, ref int index, bool expectMultipleAddresses)
		{
			int num = WhitespaceReader.ReadCfwsReverse(data, index);
			string text;
			if (num >= 0 && data[num] == MailBnfHelper.Quote)
			{
				index = QuotedStringFormatReader.ReadReverseQuoted(data, num, true);
				int num2 = index + 2;
				text = data.Substring(num2, num - num2);
				index = WhitespaceReader.ReadCfwsReverse(data, index);
				if (index >= 0 && (!expectMultipleAddresses || data[index] != MailBnfHelper.Comma))
				{
					throw new FormatException(SR.GetString("MailHeaderFieldInvalidCharacter", new object[]
					{
						data[index]
					}));
				}
			}
			else
			{
				int num3 = index;
				index = QuotedStringFormatReader.ReadReverseUnQuoted(data, index, true, expectMultipleAddresses);
				text = data.Substring(index + 1, num3 - index);
				text = text.Trim();
			}
			return MailAddressParser.NormalizeOrThrow(text);
		}

		// Token: 0x0600175B RID: 5979 RVA: 0x000773D0 File Offset: 0x000755D0
		internal static string NormalizeOrThrow(string input)
		{
			string result;
			try
			{
				result = input.Normalize(NormalizationForm.FormC);
			}
			catch (ArgumentException innerException)
			{
				throw new FormatException(SR.GetString("MailAddressInvalidFormat"), innerException);
			}
			return result;
		}
	}
}
