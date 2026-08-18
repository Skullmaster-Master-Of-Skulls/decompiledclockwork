using System;
using System.Collections;
using System.Data.Entity;
using System.Text;

namespace System.Data.EntityClient
{
	// Token: 0x02000119 RID: 281
	internal class DbConnectionOptions
	{
		// Token: 0x06000E6D RID: 3693 RVA: 0x0003DB70 File Offset: 0x0003BD70
		internal DbConnectionOptions(string connectionString, Hashtable synonyms)
		{
			this._parsetable = new Hashtable();
			this._usersConnectionString = ((connectionString != null) ? connectionString : "");
			if (0 < this._usersConnectionString.Length)
			{
				this.KeyChain = DbConnectionOptions.ParseInternal(this._parsetable, this._usersConnectionString, synonyms);
			}
		}

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x06000E6E RID: 3694 RVA: 0x0003DBC5 File Offset: 0x0003BDC5
		internal string UsersConnectionString
		{
			get
			{
				return this._usersConnectionString ?? string.Empty;
			}
		}

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x06000E6F RID: 3695 RVA: 0x0003DBD6 File Offset: 0x0003BDD6
		internal bool IsEmpty
		{
			get
			{
				return this.KeyChain == null;
			}
		}

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x06000E70 RID: 3696 RVA: 0x0003DBE1 File Offset: 0x0003BDE1
		internal Hashtable Parsetable
		{
			get
			{
				return this._parsetable;
			}
		}

		// Token: 0x170001AD RID: 429
		internal string this[string keyword]
		{
			get
			{
				return (string)this._parsetable[keyword];
			}
		}

		// Token: 0x06000E72 RID: 3698 RVA: 0x0003DBFC File Offset: 0x0003BDFC
		internal static string ExpandDataDirectory(string keyword, string value)
		{
			string text = null;
			if (value != null && value.StartsWith("|datadirectory|", StringComparison.OrdinalIgnoreCase))
			{
				object data = AppDomain.CurrentDomain.GetData("DataDirectory");
				string text2 = data as string;
				if (data != null && text2 == null)
				{
					throw EntityUtil.InvalidOperation(Strings.ADP_InvalidDataDirectory);
				}
				if (text2 == string.Empty)
				{
					text2 = AppDomain.CurrentDomain.BaseDirectory;
				}
				if (text2 == null)
				{
					text2 = "";
				}
				int length = "|datadirectory|".Length;
				bool flag = 0 < text2.Length && text2[text2.Length - 1] == '\\';
				bool flag2 = length < value.Length && value[length] == '\\';
				if (!flag && !flag2)
				{
					text = text2 + "\\" + value.Substring(length);
				}
				else if (flag && flag2)
				{
					text = text2 + value.Substring(length + 1);
				}
				else
				{
					text = text2 + value.Substring(length);
				}
				if (!EntityUtil.GetFullPath(text).StartsWith(text2, StringComparison.Ordinal))
				{
					throw EntityUtil.InvalidConnectionOptionValue(keyword);
				}
			}
			return text;
		}

		// Token: 0x06000E73 RID: 3699 RVA: 0x0003DD0C File Offset: 0x0003BF0C
		private static string GetKeyName(StringBuilder buffer)
		{
			int num = buffer.Length;
			while (0 < num && char.IsWhiteSpace(buffer[num - 1]))
			{
				num--;
			}
			return buffer.ToString(0, num).ToLowerInvariant();
		}

		// Token: 0x06000E74 RID: 3700 RVA: 0x0003DD48 File Offset: 0x0003BF48
		private static string GetKeyValue(StringBuilder buffer, bool trimWhitespace)
		{
			int num = buffer.Length;
			int i = 0;
			if (trimWhitespace)
			{
				while (i < num)
				{
					if (!char.IsWhiteSpace(buffer[i]))
					{
						break;
					}
					i++;
				}
				while (0 < num && char.IsWhiteSpace(buffer[num - 1]))
				{
					num--;
				}
			}
			return buffer.ToString(i, num - i);
		}

		// Token: 0x06000E75 RID: 3701 RVA: 0x0003DDA0 File Offset: 0x0003BFA0
		private static int GetKeyValuePair(string connectionString, int currentPosition, StringBuilder buffer, out string keyname, out string keyvalue)
		{
			int index = currentPosition;
			buffer.Length = 0;
			keyname = null;
			keyvalue = null;
			char c = '\0';
			DbConnectionOptions.ParserState parserState = DbConnectionOptions.ParserState.NothingYet;
			int length = connectionString.Length;
			while (currentPosition < length)
			{
				c = connectionString[currentPosition];
				switch (parserState)
				{
				case DbConnectionOptions.ParserState.NothingYet:
					if (';' != c && !char.IsWhiteSpace(c))
					{
						if (c == '\0')
						{
							parserState = DbConnectionOptions.ParserState.NullTermination;
						}
						else
						{
							if (char.IsControl(c))
							{
								throw EntityUtil.ConnectionStringSyntax(index);
							}
							index = currentPosition;
							if ('=' != c)
							{
								parserState = DbConnectionOptions.ParserState.Key;
								goto IL_1F7;
							}
							parserState = DbConnectionOptions.ParserState.KeyEqual;
						}
					}
					break;
				case DbConnectionOptions.ParserState.Key:
					if ('=' == c)
					{
						parserState = DbConnectionOptions.ParserState.KeyEqual;
					}
					else
					{
						if (!char.IsWhiteSpace(c) && char.IsControl(c))
						{
							throw EntityUtil.ConnectionStringSyntax(index);
						}
						goto IL_1F7;
					}
					break;
				case DbConnectionOptions.ParserState.KeyEqual:
					if ('=' == c)
					{
						parserState = DbConnectionOptions.ParserState.Key;
						goto IL_1F7;
					}
					keyname = DbConnectionOptions.GetKeyName(buffer);
					if (string.IsNullOrEmpty(keyname))
					{
						throw EntityUtil.ConnectionStringSyntax(index);
					}
					buffer.Length = 0;
					parserState = DbConnectionOptions.ParserState.KeyEnd;
					goto IL_F9;
				case DbConnectionOptions.ParserState.KeyEnd:
					goto IL_F9;
				case DbConnectionOptions.ParserState.UnquotedValue:
					if (char.IsWhiteSpace(c))
					{
						goto IL_1F7;
					}
					if (char.IsControl(c))
					{
						goto IL_20B;
					}
					if (';' == c)
					{
						goto IL_20B;
					}
					goto IL_1F7;
				case DbConnectionOptions.ParserState.DoubleQuoteValue:
					if ('"' == c)
					{
						parserState = DbConnectionOptions.ParserState.DoubleQuoteValueQuote;
					}
					else
					{
						if (c == '\0')
						{
							throw EntityUtil.ConnectionStringSyntax(index);
						}
						goto IL_1F7;
					}
					break;
				case DbConnectionOptions.ParserState.DoubleQuoteValueQuote:
					if ('"' == c)
					{
						parserState = DbConnectionOptions.ParserState.DoubleQuoteValue;
						goto IL_1F7;
					}
					keyvalue = DbConnectionOptions.GetKeyValue(buffer, false);
					parserState = DbConnectionOptions.ParserState.QuotedValueEnd;
					goto IL_1BE;
				case DbConnectionOptions.ParserState.SingleQuoteValue:
					if ('\'' == c)
					{
						parserState = DbConnectionOptions.ParserState.SingleQuoteValueQuote;
					}
					else
					{
						if (c == '\0')
						{
							throw EntityUtil.ConnectionStringSyntax(index);
						}
						goto IL_1F7;
					}
					break;
				case DbConnectionOptions.ParserState.SingleQuoteValueQuote:
					if ('\'' == c)
					{
						parserState = DbConnectionOptions.ParserState.SingleQuoteValue;
						goto IL_1F7;
					}
					keyvalue = DbConnectionOptions.GetKeyValue(buffer, false);
					parserState = DbConnectionOptions.ParserState.QuotedValueEnd;
					goto IL_1BE;
				case DbConnectionOptions.ParserState.QuotedValueEnd:
					goto IL_1BE;
				case DbConnectionOptions.ParserState.NullTermination:
					if (c != '\0' && !char.IsWhiteSpace(c))
					{
						throw EntityUtil.ConnectionStringSyntax(currentPosition);
					}
					break;
				default:
					throw EntityUtil.InternalError(EntityUtil.InternalErrorCode.InvalidParserState1);
				}
				IL_1FF:
				currentPosition++;
				continue;
				IL_F9:
				if (char.IsWhiteSpace(c))
				{
					goto IL_1FF;
				}
				if ('\'' == c)
				{
					parserState = DbConnectionOptions.ParserState.SingleQuoteValue;
					goto IL_1FF;
				}
				if ('"' == c)
				{
					parserState = DbConnectionOptions.ParserState.DoubleQuoteValue;
					goto IL_1FF;
				}
				if (';' == c || c == '\0')
				{
					break;
				}
				if (char.IsControl(c))
				{
					throw EntityUtil.ConnectionStringSyntax(index);
				}
				parserState = DbConnectionOptions.ParserState.UnquotedValue;
				goto IL_1F7;
				IL_1BE:
				if (char.IsWhiteSpace(c))
				{
					goto IL_1FF;
				}
				if (';' == c)
				{
					break;
				}
				if (c == '\0')
				{
					parserState = DbConnectionOptions.ParserState.NullTermination;
					goto IL_1FF;
				}
				throw EntityUtil.ConnectionStringSyntax(index);
				IL_1F7:
				buffer.Append(c);
				goto IL_1FF;
			}
			IL_20B:
			switch (parserState)
			{
			case DbConnectionOptions.ParserState.NothingYet:
			case DbConnectionOptions.ParserState.KeyEnd:
			case DbConnectionOptions.ParserState.NullTermination:
				break;
			case DbConnectionOptions.ParserState.Key:
			case DbConnectionOptions.ParserState.DoubleQuoteValue:
			case DbConnectionOptions.ParserState.SingleQuoteValue:
				throw EntityUtil.ConnectionStringSyntax(index);
			case DbConnectionOptions.ParserState.KeyEqual:
				keyname = DbConnectionOptions.GetKeyName(buffer);
				if (string.IsNullOrEmpty(keyname))
				{
					throw EntityUtil.ConnectionStringSyntax(index);
				}
				break;
			case DbConnectionOptions.ParserState.UnquotedValue:
			{
				keyvalue = DbConnectionOptions.GetKeyValue(buffer, true);
				char c2 = keyvalue[keyvalue.Length - 1];
				if ('\'' == c2 || '"' == c2)
				{
					throw EntityUtil.ConnectionStringSyntax(index);
				}
				break;
			}
			case DbConnectionOptions.ParserState.DoubleQuoteValueQuote:
			case DbConnectionOptions.ParserState.SingleQuoteValueQuote:
			case DbConnectionOptions.ParserState.QuotedValueEnd:
				keyvalue = DbConnectionOptions.GetKeyValue(buffer, false);
				break;
			default:
				throw EntityUtil.InternalError(EntityUtil.InternalErrorCode.InvalidParserState2);
			}
			if (';' == c && currentPosition < connectionString.Length)
			{
				currentPosition++;
			}
			return currentPosition;
		}

		// Token: 0x06000E76 RID: 3702 RVA: 0x0003E069 File Offset: 0x0003C269
		private static bool IsKeyNameValid(string keyname)
		{
			return keyname != null && (0 < keyname.Length && ';' != keyname[0] && !char.IsWhiteSpace(keyname[0])) && -1 == keyname.IndexOf('\0');
		}

		// Token: 0x06000E77 RID: 3703 RVA: 0x0003E0A0 File Offset: 0x0003C2A0
		private static NameValuePair ParseInternal(Hashtable parsetable, string connectionString, Hashtable synonyms)
		{
			StringBuilder buffer = new StringBuilder();
			NameValuePair nameValuePair = null;
			NameValuePair result = null;
			int i = 0;
			int length = connectionString.Length;
			while (i < length)
			{
				int num = i;
				string text;
				string value;
				i = DbConnectionOptions.GetKeyValuePair(connectionString, num, buffer, out text, out value);
				if (string.IsNullOrEmpty(text))
				{
					break;
				}
				string text2 = (synonyms != null) ? ((string)synonyms[text]) : text;
				if (!DbConnectionOptions.IsKeyNameValid(text2))
				{
					throw EntityUtil.ADP_KeywordNotSupported(text);
				}
				parsetable[text2] = value;
				if (nameValuePair != null)
				{
					nameValuePair = (nameValuePair.Next = new NameValuePair(text2, value, i - num));
				}
				else
				{
					nameValuePair = (result = new NameValuePair(text2, value, i - num));
				}
			}
			return result;
		}

		// Token: 0x040009DA RID: 2522
		internal const string DataDirectory = "|datadirectory|";

		// Token: 0x040009DB RID: 2523
		private readonly string _usersConnectionString;

		// Token: 0x040009DC RID: 2524
		private readonly Hashtable _parsetable;

		// Token: 0x040009DD RID: 2525
		internal readonly NameValuePair KeyChain;

		// Token: 0x02000498 RID: 1176
		private enum ParserState
		{
			// Token: 0x04001A09 RID: 6665
			NothingYet = 1,
			// Token: 0x04001A0A RID: 6666
			Key,
			// Token: 0x04001A0B RID: 6667
			KeyEqual,
			// Token: 0x04001A0C RID: 6668
			KeyEnd,
			// Token: 0x04001A0D RID: 6669
			UnquotedValue,
			// Token: 0x04001A0E RID: 6670
			DoubleQuoteValue,
			// Token: 0x04001A0F RID: 6671
			DoubleQuoteValueQuote,
			// Token: 0x04001A10 RID: 6672
			SingleQuoteValue,
			// Token: 0x04001A11 RID: 6673
			SingleQuoteValueQuote,
			// Token: 0x04001A12 RID: 6674
			QuotedValueEnd,
			// Token: 0x04001A13 RID: 6675
			NullTermination
		}
	}
}
