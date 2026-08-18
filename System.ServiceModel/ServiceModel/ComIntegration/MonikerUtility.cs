using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace System.ServiceModel.ComIntegration
{
	// Token: 0x0200023F RID: 575
	internal static class MonikerUtility
	{
		// Token: 0x06001110 RID: 4368 RVA: 0x0003E898 File Offset: 0x0003CA98
		internal static string Getkeyword(string moniker, out MonikerHelper.MonikerAttribute keyword)
		{
			moniker = moniker.TrimStart(new char[0]);
			int num = moniker.IndexOf("=", StringComparison.Ordinal);
			if (num == -1)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MonikerSyntaxException(SR.GetString("NoEqualSignFound", new object[]
				{
					moniker
				})));
			}
			int num2 = moniker.IndexOf(",", StringComparison.Ordinal);
			if (num2 != -1 && num2 < num)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MonikerSyntaxException(SR.GetString("NoEqualSignFound", new object[]
				{
					moniker
				})));
			}
			string text = moniker.Substring(0, num).Trim();
			text = text.ToLower(CultureInfo.InvariantCulture);
			foreach (MonikerHelper.KeywordInfo keywordInfo in MonikerHelper.KeywordInfo.KeywordCollection)
			{
				if (text == keywordInfo.Name)
				{
					keyword = keywordInfo.Attrib;
					moniker = moniker.Substring(num + 1).TrimStart(new char[0]);
					return moniker;
				}
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MonikerSyntaxException(SR.GetString("UnknownMonikerKeyword", new object[]
			{
				text
			})));
		}

		// Token: 0x06001111 RID: 4369 RVA: 0x0003E9B0 File Offset: 0x0003CBB0
		internal static string GetValue(string moniker, out string val)
		{
			StringBuilder stringBuilder = new StringBuilder();
			int i = 0;
			moniker = moniker.Trim();
			if (string.IsNullOrEmpty(moniker))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MonikerSyntaxException(SR.GetString("KewordMissingValue")));
			}
			char c = moniker[i];
			if (c == '"' || c == '\'')
			{
				char c2 = moniker[i];
				for (i++; i < moniker.Length; i++)
				{
					if (moniker[i] == c2)
					{
						if (i >= moniker.Length - 1 || moniker[i + 1] != c2)
						{
							break;
						}
						stringBuilder.Append(c2);
						i++;
					}
					else
					{
						stringBuilder.Append(moniker[i]);
					}
				}
				if (i >= moniker.Length)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MonikerSyntaxException(SR.GetString("MissingQuote", new object[]
					{
						stringBuilder.ToString()
					})));
				}
				i++;
				if (i < moniker.Length)
				{
					moniker = moniker.Substring(i);
					moniker = moniker.Trim();
					if (!string.IsNullOrEmpty(moniker))
					{
						if (moniker[0] != ',')
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MonikerSyntaxException(SR.GetString("BadlyTerminatedValue", new object[]
							{
								stringBuilder.ToString()
							})));
						}
						moniker = moniker.Substring(1);
						moniker = moniker.Trim();
					}
				}
				else
				{
					moniker = "";
				}
			}
			else
			{
				while (i < moniker.Length && moniker[i] != ',')
				{
					stringBuilder.Append(moniker[i]);
					i++;
				}
				if (i < moniker.Length)
				{
					i++;
					if (i < moniker.Length)
					{
						moniker = moniker.Substring(i);
						moniker = moniker.Trim();
					}
				}
				else
				{
					moniker = "";
				}
			}
			val = stringBuilder.ToString().Trim();
			return moniker;
		}

		// Token: 0x06001112 RID: 4370 RVA: 0x0003EB70 File Offset: 0x0003CD70
		internal static void Parse(string displayName, ref Dictionary<MonikerHelper.MonikerAttribute, string> propertyTable)
		{
			int num = displayName.IndexOf(":", StringComparison.Ordinal);
			if (num == -1)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MonikerSyntaxException(SR.GetString("MonikerMissingColon")));
			}
			string text = displayName.Substring(num + 1).Trim();
			while (!string.IsNullOrEmpty(text))
			{
				MonikerHelper.MonikerAttribute key;
				text = MonikerUtility.Getkeyword(text, out key);
				string value;
				propertyTable.TryGetValue(key, out value);
				if (!string.IsNullOrEmpty(value))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MonikerSyntaxException(SR.GetString("RepeatedKeyword")));
				}
				text = MonikerUtility.GetValue(text, out value);
				propertyTable[key] = value;
			}
		}
	}
}
