using System;
using System.Collections.Generic;
using System.Data.Entity.Resources;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace System.Data.Entity.Core.EntityClient.Internal
{
	// Token: 0x02000340 RID: 832
	internal class DbConnectionOptions
	{
		// Token: 0x06001DB3 RID: 7603 RVA: 0x0008F1B4 File Offset: 0x0008D3B4
		internal DbConnectionOptions()
		{
		}

		// Token: 0x06001DB4 RID: 7604 RVA: 0x0008F1C8 File Offset: 0x0008D3C8
		internal DbConnectionOptions(string connectionString, IList<string> validKeywords)
		{
			this._usersConnectionString = (connectionString ?? "");
			if (0 < this._usersConnectionString.Length)
			{
				this.KeyChain = DbConnectionOptions.ParseInternal(this._parsetable, this._usersConnectionString, validKeywords);
			}
		}

		// Token: 0x17000367 RID: 871
		// (get) Token: 0x06001DB5 RID: 7605 RVA: 0x0008F21C File Offset: 0x0008D41C
		internal string UsersConnectionString
		{
			get
			{
				return this._usersConnectionString ?? string.Empty;
			}
		}

		// Token: 0x17000368 RID: 872
		// (get) Token: 0x06001DB6 RID: 7606 RVA: 0x0008F22D File Offset: 0x0008D42D
		internal bool IsEmpty
		{
			get
			{
				return null == this.KeyChain;
			}
		}

		// Token: 0x17000369 RID: 873
		// (get) Token: 0x06001DB7 RID: 7607 RVA: 0x0008F238 File Offset: 0x0008D438
		internal Dictionary<string, string> Parsetable
		{
			get
			{
				return this._parsetable;
			}
		}

		// Token: 0x1700036A RID: 874
		internal virtual string this[string keyword]
		{
			get
			{
				string result;
				this._parsetable.TryGetValue(keyword, out result);
				return result;
			}
		}

		// Token: 0x06001DB9 RID: 7609 RVA: 0x0008F260 File Offset: 0x0008D460
		[SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase")]
		private static string GetKeyName(StringBuilder buffer)
		{
			int num = buffer.Length;
			while (0 < num && char.IsWhiteSpace(buffer[num - 1]))
			{
				num--;
			}
			return buffer.ToString(0, num).ToLowerInvariant();
		}

		// Token: 0x06001DBA RID: 7610 RVA: 0x0008F29C File Offset: 0x0008D49C
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

		// Token: 0x06001DBB RID: 7611 RVA: 0x0008F2F4 File Offset: 0x0008D4F4
		[SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
		private static int GetKeyValuePair(string connectionString, int currentPosition, StringBuilder buffer, out string keyname, out string keyvalue)
		{
			int num = currentPosition;
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
								throw new ArgumentException(Strings.ADP_ConnectionStringSyntax(num));
							}
							num = currentPosition;
							if ('=' != c)
							{
								parserState = DbConnectionOptions.ParserState.Key;
								goto IL_25B;
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
							throw new ArgumentException(Strings.ADP_ConnectionStringSyntax(num));
						}
						goto IL_25B;
					}
					break;
				case DbConnectionOptions.ParserState.KeyEqual:
					if ('=' == c)
					{
						parserState = DbConnectionOptions.ParserState.Key;
						goto IL_25B;
					}
					keyname = DbConnectionOptions.GetKeyName(buffer);
					if (string.IsNullOrEmpty(keyname))
					{
						throw new ArgumentException(Strings.ADP_ConnectionStringSyntax(num));
					}
					buffer.Length = 0;
					parserState = DbConnectionOptions.ParserState.KeyEnd;
					goto IL_11B;
				case DbConnectionOptions.ParserState.KeyEnd:
					goto IL_11B;
				case DbConnectionOptions.ParserState.UnquotedValue:
					if (char.IsWhiteSpace(c))
					{
						goto IL_25B;
					}
					if (char.IsControl(c))
					{
						goto IL_26F;
					}
					if (';' == c)
					{
						goto IL_26F;
					}
					goto IL_25B;
				case DbConnectionOptions.ParserState.DoubleQuoteValue:
					if ('"' == c)
					{
						parserState = DbConnectionOptions.ParserState.DoubleQuoteValueQuote;
					}
					else
					{
						if (c == '\0')
						{
							throw new ArgumentException(Strings.ADP_ConnectionStringSyntax(num));
						}
						goto IL_25B;
					}
					break;
				case DbConnectionOptions.ParserState.DoubleQuoteValueQuote:
					if ('"' == c)
					{
						parserState = DbConnectionOptions.ParserState.DoubleQuoteValue;
						goto IL_25B;
					}
					keyvalue = DbConnectionOptions.GetKeyValue(buffer, false);
					parserState = DbConnectionOptions.ParserState.QuotedValueEnd;
					goto IL_204;
				case DbConnectionOptions.ParserState.SingleQuoteValue:
					if ('\'' == c)
					{
						parserState = DbConnectionOptions.ParserState.SingleQuoteValueQuote;
					}
					else
					{
						if (c == '\0')
						{
							throw new ArgumentException(Strings.ADP_ConnectionStringSyntax(num));
						}
						goto IL_25B;
					}
					break;
				case DbConnectionOptions.ParserState.SingleQuoteValueQuote:
					if ('\'' == c)
					{
						parserState = DbConnectionOptions.ParserState.SingleQuoteValue;
						goto IL_25B;
					}
					keyvalue = DbConnectionOptions.GetKeyValue(buffer, false);
					parserState = DbConnectionOptions.ParserState.QuotedValueEnd;
					goto IL_204;
				case DbConnectionOptions.ParserState.QuotedValueEnd:
					goto IL_204;
				case DbConnectionOptions.ParserState.NullTermination:
					if (c != '\0' && !char.IsWhiteSpace(c))
					{
						throw new ArgumentException(Strings.ADP_ConnectionStringSyntax(currentPosition));
					}
					break;
				default:
					throw new InvalidOperationException(Strings.ADP_InternalProviderError(1015));
				}
				IL_263:
				currentPosition++;
				continue;
				IL_11B:
				if (char.IsWhiteSpace(c))
				{
					goto IL_263;
				}
				if ('\'' == c)
				{
					parserState = DbConnectionOptions.ParserState.SingleQuoteValue;
					goto IL_263;
				}
				if ('"' == c)
				{
					parserState = DbConnectionOptions.ParserState.DoubleQuoteValue;
					goto IL_263;
				}
				if (';' == c || c == '\0')
				{
					break;
				}
				if (char.IsControl(c))
				{
					throw new ArgumentException(Strings.ADP_ConnectionStringSyntax(num));
				}
				parserState = DbConnectionOptions.ParserState.UnquotedValue;
				goto IL_25B;
				IL_204:
				if (char.IsWhiteSpace(c))
				{
					goto IL_263;
				}
				if (';' == c)
				{
					break;
				}
				if (c == '\0')
				{
					parserState = DbConnectionOptions.ParserState.NullTermination;
					goto IL_263;
				}
				throw new ArgumentException(Strings.ADP_ConnectionStringSyntax(num));
				IL_25B:
				buffer.Append(c);
				goto IL_263;
			}
			IL_26F:
			switch (parserState)
			{
			case DbConnectionOptions.ParserState.NothingYet:
			case DbConnectionOptions.ParserState.KeyEnd:
			case DbConnectionOptions.ParserState.NullTermination:
				break;
			case DbConnectionOptions.ParserState.Key:
			case DbConnectionOptions.ParserState.DoubleQuoteValue:
			case DbConnectionOptions.ParserState.SingleQuoteValue:
				throw new ArgumentException(Strings.ADP_ConnectionStringSyntax(num));
			case DbConnectionOptions.ParserState.KeyEqual:
				keyname = DbConnectionOptions.GetKeyName(buffer);
				if (string.IsNullOrEmpty(keyname))
				{
					throw new ArgumentException(Strings.ADP_ConnectionStringSyntax(num));
				}
				break;
			case DbConnectionOptions.ParserState.UnquotedValue:
			{
				keyvalue = DbConnectionOptions.GetKeyValue(buffer, true);
				char c2 = keyvalue[keyvalue.Length - 1];
				if ('\'' == c2 || '"' == c2)
				{
					throw new ArgumentException(Strings.ADP_ConnectionStringSyntax(num));
				}
				break;
			}
			case DbConnectionOptions.ParserState.DoubleQuoteValueQuote:
			case DbConnectionOptions.ParserState.SingleQuoteValueQuote:
			case DbConnectionOptions.ParserState.QuotedValueEnd:
				keyvalue = DbConnectionOptions.GetKeyValue(buffer, false);
				break;
			default:
				throw new InvalidOperationException(Strings.ADP_InternalProviderError(1016));
			}
			if (';' == c && currentPosition < connectionString.Length)
			{
				currentPosition++;
			}
			return currentPosition;
		}

		// Token: 0x06001DBC RID: 7612 RVA: 0x0008F650 File Offset: 0x0008D850
		private static NameValuePair ParseInternal(IDictionary<string, string> parsetable, string connectionString, IList<string> validKeywords)
		{
			StringBuilder buffer = new StringBuilder();
			NameValuePair nameValuePair = null;
			NameValuePair result = null;
			int i = 0;
			int length = connectionString.Length;
			while (i < length)
			{
				int currentPosition = i;
				string text;
				string value;
				i = DbConnectionOptions.GetKeyValuePair(connectionString, currentPosition, buffer, out text, out value);
				if (string.IsNullOrEmpty(text))
				{
					break;
				}
				if (!validKeywords.Contains(text))
				{
					throw new ArgumentException(Strings.ADP_KeywordNotSupported(text));
				}
				parsetable[text] = value;
				if (nameValuePair != null)
				{
					nameValuePair = (nameValuePair.Next = new NameValuePair());
				}
				else
				{
					nameValuePair = (result = new NameValuePair());
				}
			}
			return result;
		}

		// Token: 0x04000A1D RID: 2589
		internal const string DataDirectory = "|datadirectory|";

		// Token: 0x04000A1E RID: 2590
		private readonly string _usersConnectionString;

		// Token: 0x04000A1F RID: 2591
		private readonly Dictionary<string, string> _parsetable = new Dictionary<string, string>();

		// Token: 0x04000A20 RID: 2592
		internal readonly NameValuePair KeyChain;

		// Token: 0x02000341 RID: 833
		private enum ParserState
		{
			// Token: 0x04000A22 RID: 2594
			NothingYet = 1,
			// Token: 0x04000A23 RID: 2595
			Key,
			// Token: 0x04000A24 RID: 2596
			KeyEqual,
			// Token: 0x04000A25 RID: 2597
			KeyEnd,
			// Token: 0x04000A26 RID: 2598
			UnquotedValue,
			// Token: 0x04000A27 RID: 2599
			DoubleQuoteValue,
			// Token: 0x04000A28 RID: 2600
			DoubleQuoteValueQuote,
			// Token: 0x04000A29 RID: 2601
			SingleQuoteValue,
			// Token: 0x04000A2A RID: 2602
			SingleQuoteValueQuote,
			// Token: 0x04000A2B RID: 2603
			QuotedValueEnd,
			// Token: 0x04000A2C RID: 2604
			NullTermination
		}
	}
}
