using System;
using System.Collections;
using System.Data.OracleClient;
using System.Globalization;
using System.Security;
using System.Text;

namespace System.Data.Common
{
	// Token: 0x0200005B RID: 91
	internal class DbConnectionOptions
	{
		// Token: 0x0600039D RID: 925 RVA: 0x00064594 File Offset: 0x00063994
		public DbConnectionOptions(string connectionString) : this(connectionString, null, false)
		{
		}

		// Token: 0x0600039E RID: 926 RVA: 0x000645B4 File Offset: 0x000639B4
		public DbConnectionOptions(string connectionString, Hashtable synonyms, bool useOdbcRules)
		{
			this.UseOdbcRules = useOdbcRules;
			this._parsetable = new Hashtable();
			this._usersConnectionString = ((connectionString != null) ? connectionString : "");
			if (0 < this._usersConnectionString.Length)
			{
				this.KeyChain = DbConnectionOptions.ParseInternal(this._parsetable, this._usersConnectionString, true, synonyms, this.UseOdbcRules);
				this.HasPasswordKeyword = (this._parsetable.ContainsKey("password") || this._parsetable.ContainsKey("pwd"));
			}
		}

		// Token: 0x0600039F RID: 927 RVA: 0x00064644 File Offset: 0x00063A44
		public string UsersConnectionString(bool hidePassword)
		{
			return this.UsersConnectionString(hidePassword, false);
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x00064664 File Offset: 0x00063A64
		private string UsersConnectionString(bool hidePassword, bool forceHidePassword)
		{
			string usersConnectionString = this._usersConnectionString;
			if (this.HasPasswordKeyword && (forceHidePassword || (hidePassword && !this.HasPersistablePassword)))
			{
				this.ReplacePasswordPwd(out usersConnectionString, false);
			}
			if (usersConnectionString == null)
			{
				return "";
			}
			return usersConnectionString;
		}

		// Token: 0x060003A1 RID: 929 RVA: 0x000646A4 File Offset: 0x00063AA4
		internal string UsersConnectionStringForTrace()
		{
			return this.UsersConnectionString(true, true);
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060003A2 RID: 930 RVA: 0x000646C4 File Offset: 0x00063AC4
		internal bool HasBlankPassword
		{
			get
			{
				if (this.ConvertValueToIntegratedSecurity())
				{
					return false;
				}
				if (this._parsetable.ContainsKey("password"))
				{
					return ADP.IsEmpty((string)this._parsetable["password"]);
				}
				if (this._parsetable.ContainsKey("pwd"))
				{
					return ADP.IsEmpty((string)this._parsetable["pwd"]);
				}
				return (this._parsetable.ContainsKey("user id") && !ADP.IsEmpty((string)this._parsetable["user id"])) || (this._parsetable.ContainsKey("uid") && !ADP.IsEmpty((string)this._parsetable["uid"]));
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060003A3 RID: 931 RVA: 0x000647A4 File Offset: 0x00063BA4
		internal bool HasPersistablePassword
		{
			get
			{
				return !this.HasPasswordKeyword || this.ConvertValueToBoolean("persist security info", false);
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060003A4 RID: 932 RVA: 0x000647D4 File Offset: 0x00063BD4
		public bool IsEmpty
		{
			get
			{
				return null == this.KeyChain;
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060003A5 RID: 933 RVA: 0x000647F4 File Offset: 0x00063BF4
		internal Hashtable Parsetable
		{
			get
			{
				return this._parsetable;
			}
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x00064814 File Offset: 0x00063C14
		public bool ConvertValueToBoolean(string keyName, bool defaultValue)
		{
			object obj = this._parsetable[keyName];
			if (obj == null)
			{
				return defaultValue;
			}
			return DbConnectionOptions.ConvertValueToBooleanInternal(keyName, (string)obj);
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x00064844 File Offset: 0x00063C44
		internal static bool ConvertValueToBooleanInternal(string keyName, string stringValue)
		{
			if (DbConnectionOptions.CompareInsensitiveInvariant(stringValue, "true") || DbConnectionOptions.CompareInsensitiveInvariant(stringValue, "yes"))
			{
				return true;
			}
			if (DbConnectionOptions.CompareInsensitiveInvariant(stringValue, "false") || DbConnectionOptions.CompareInsensitiveInvariant(stringValue, "no"))
			{
				return false;
			}
			string strvalue = stringValue.Trim();
			if (DbConnectionOptions.CompareInsensitiveInvariant(strvalue, "true") || DbConnectionOptions.CompareInsensitiveInvariant(strvalue, "yes"))
			{
				return true;
			}
			if (DbConnectionOptions.CompareInsensitiveInvariant(strvalue, "false") || DbConnectionOptions.CompareInsensitiveInvariant(strvalue, "no"))
			{
				return false;
			}
			throw ADP.InvalidConnectionOptionValue(keyName);
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x000648D4 File Offset: 0x00063CD4
		public bool ConvertValueToIntegratedSecurity()
		{
			object obj = this._parsetable["integrated security"];
			return obj != null && this.ConvertValueToIntegratedSecurityInternal((string)obj);
		}

		// Token: 0x060003A9 RID: 937 RVA: 0x00064904 File Offset: 0x00063D04
		internal bool ConvertValueToIntegratedSecurityInternal(string stringValue)
		{
			if (DbConnectionOptions.CompareInsensitiveInvariant(stringValue, "sspi") || DbConnectionOptions.CompareInsensitiveInvariant(stringValue, "true") || DbConnectionOptions.CompareInsensitiveInvariant(stringValue, "yes"))
			{
				return true;
			}
			if (DbConnectionOptions.CompareInsensitiveInvariant(stringValue, "false") || DbConnectionOptions.CompareInsensitiveInvariant(stringValue, "no"))
			{
				return false;
			}
			string strvalue = stringValue.Trim();
			if (DbConnectionOptions.CompareInsensitiveInvariant(strvalue, "sspi") || DbConnectionOptions.CompareInsensitiveInvariant(strvalue, "true") || DbConnectionOptions.CompareInsensitiveInvariant(strvalue, "yes"))
			{
				return true;
			}
			if (DbConnectionOptions.CompareInsensitiveInvariant(strvalue, "false") || DbConnectionOptions.CompareInsensitiveInvariant(strvalue, "no"))
			{
				return false;
			}
			throw ADP.InvalidConnectionOptionValue("integrated security");
		}

		// Token: 0x060003AA RID: 938 RVA: 0x000649B4 File Offset: 0x00063DB4
		public int ConvertValueToInt32(string keyName, int defaultValue)
		{
			object obj = this._parsetable[keyName];
			if (obj == null)
			{
				return defaultValue;
			}
			return DbConnectionOptions.ConvertToInt32Internal(keyName, (string)obj);
		}

		// Token: 0x060003AB RID: 939 RVA: 0x000649E4 File Offset: 0x00063DE4
		internal static int ConvertToInt32Internal(string keyname, string stringValue)
		{
			int result;
			try
			{
				result = int.Parse(stringValue, NumberStyles.Integer, CultureInfo.InvariantCulture);
			}
			catch (FormatException inner)
			{
				throw ADP.InvalidConnectionOptionValue(keyname, inner);
			}
			catch (OverflowException inner2)
			{
				throw ADP.InvalidConnectionOptionValue(keyname, inner2);
			}
			return result;
		}

		// Token: 0x060003AC RID: 940 RVA: 0x00064A54 File Offset: 0x00063E54
		public string ConvertValueToString(string keyName, string defaultValue)
		{
			string text = (string)this._parsetable[keyName];
			if (text == null)
			{
				return defaultValue;
			}
			return text;
		}

		// Token: 0x060003AD RID: 941 RVA: 0x00064A84 File Offset: 0x00063E84
		private static bool CompareInsensitiveInvariant(string strvalue, string strconst)
		{
			return 0 == StringComparer.OrdinalIgnoreCase.Compare(strvalue, strconst);
		}

		// Token: 0x060003AE RID: 942 RVA: 0x00064AA4 File Offset: 0x00063EA4
		protected internal virtual PermissionSet CreatePermissionSet()
		{
			return null;
		}

		// Token: 0x060003AF RID: 943 RVA: 0x00064AB4 File Offset: 0x00063EB4
		internal void DemandPermission()
		{
			if (this._permissionset == null)
			{
				this._permissionset = this.CreatePermissionSet();
			}
			this._permissionset.Demand();
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x00064AE4 File Offset: 0x00063EE4
		protected internal virtual string Expand()
		{
			return this._usersConnectionString;
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x00064B04 File Offset: 0x00063F04
		private static string GetKeyName(StringBuilder buffer)
		{
			int num = buffer.Length;
			while (0 < num && char.IsWhiteSpace(buffer[num - 1]))
			{
				num--;
			}
			return buffer.ToString(0, num).ToLower(CultureInfo.InvariantCulture);
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x00064B44 File Offset: 0x00063F44
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

		// Token: 0x060003B3 RID: 947 RVA: 0x00064BA4 File Offset: 0x00063FA4
		internal static int GetKeyValuePair(string connectionString, int currentPosition, StringBuilder buffer, bool useOdbcRules, out string keyname, out string keyvalue)
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
								throw ADP.ConnectionStringSyntax(index);
							}
							index = currentPosition;
							if ('=' != c)
							{
								parserState = DbConnectionOptions.ParserState.Key;
								goto IL_24D;
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
							throw ADP.ConnectionStringSyntax(index);
						}
						goto IL_24D;
					}
					break;
				case DbConnectionOptions.ParserState.KeyEqual:
					if (!useOdbcRules && '=' == c)
					{
						parserState = DbConnectionOptions.ParserState.Key;
						goto IL_24D;
					}
					keyname = DbConnectionOptions.GetKeyName(buffer);
					if (ADP.IsEmpty(keyname))
					{
						throw ADP.ConnectionStringSyntax(index);
					}
					buffer.Length = 0;
					parserState = DbConnectionOptions.ParserState.KeyEnd;
					goto IL_10C;
				case DbConnectionOptions.ParserState.KeyEnd:
					goto IL_10C;
				case DbConnectionOptions.ParserState.UnquotedValue:
					if (char.IsWhiteSpace(c))
					{
						goto IL_24D;
					}
					if (char.IsControl(c))
					{
						goto IL_262;
					}
					if (';' == c)
					{
						goto IL_262;
					}
					goto IL_24D;
				case DbConnectionOptions.ParserState.DoubleQuoteValue:
					if ('"' == c)
					{
						parserState = DbConnectionOptions.ParserState.DoubleQuoteValueQuote;
					}
					else
					{
						if (c == '\0')
						{
							throw ADP.ConnectionStringSyntax(index);
						}
						goto IL_24D;
					}
					break;
				case DbConnectionOptions.ParserState.DoubleQuoteValueQuote:
					if ('"' == c)
					{
						parserState = DbConnectionOptions.ParserState.DoubleQuoteValue;
						goto IL_24D;
					}
					keyvalue = DbConnectionOptions.GetKeyValue(buffer, false);
					parserState = DbConnectionOptions.ParserState.QuotedValueEnd;
					goto IL_217;
				case DbConnectionOptions.ParserState.SingleQuoteValue:
					if ('\'' == c)
					{
						parserState = DbConnectionOptions.ParserState.SingleQuoteValueQuote;
					}
					else
					{
						if (c == '\0')
						{
							throw ADP.ConnectionStringSyntax(index);
						}
						goto IL_24D;
					}
					break;
				case DbConnectionOptions.ParserState.SingleQuoteValueQuote:
					if ('\'' == c)
					{
						parserState = DbConnectionOptions.ParserState.SingleQuoteValue;
						goto IL_24D;
					}
					keyvalue = DbConnectionOptions.GetKeyValue(buffer, false);
					parserState = DbConnectionOptions.ParserState.QuotedValueEnd;
					goto IL_217;
				case DbConnectionOptions.ParserState.BraceQuoteValue:
					if ('}' == c)
					{
						parserState = DbConnectionOptions.ParserState.BraceQuoteValueQuote;
						goto IL_24D;
					}
					if (c == '\0')
					{
						throw ADP.ConnectionStringSyntax(index);
					}
					goto IL_24D;
				case DbConnectionOptions.ParserState.BraceQuoteValueQuote:
					if ('}' == c)
					{
						parserState = DbConnectionOptions.ParserState.BraceQuoteValue;
						goto IL_24D;
					}
					keyvalue = DbConnectionOptions.GetKeyValue(buffer, false);
					parserState = DbConnectionOptions.ParserState.QuotedValueEnd;
					goto IL_217;
				case DbConnectionOptions.ParserState.QuotedValueEnd:
					goto IL_217;
				case DbConnectionOptions.ParserState.NullTermination:
					if (c != '\0' && !char.IsWhiteSpace(c))
					{
						throw ADP.ConnectionStringSyntax(currentPosition);
					}
					break;
				default:
					throw ADP.InternalError(ADP.InternalErrorCode.InvalidParserState1);
				}
				IL_255:
				currentPosition++;
				continue;
				IL_10C:
				if (char.IsWhiteSpace(c))
				{
					goto IL_255;
				}
				if (useOdbcRules)
				{
					if ('{' == c)
					{
						parserState = DbConnectionOptions.ParserState.BraceQuoteValue;
						goto IL_24D;
					}
				}
				else
				{
					if ('\'' == c)
					{
						parserState = DbConnectionOptions.ParserState.SingleQuoteValue;
						goto IL_255;
					}
					if ('"' == c)
					{
						parserState = DbConnectionOptions.ParserState.DoubleQuoteValue;
						goto IL_255;
					}
				}
				if (';' == c || c == '\0')
				{
					break;
				}
				if (char.IsControl(c))
				{
					throw ADP.ConnectionStringSyntax(index);
				}
				parserState = DbConnectionOptions.ParserState.UnquotedValue;
				goto IL_24D;
				IL_217:
				if (char.IsWhiteSpace(c))
				{
					goto IL_255;
				}
				if (';' == c)
				{
					break;
				}
				if (c == '\0')
				{
					parserState = DbConnectionOptions.ParserState.NullTermination;
					goto IL_255;
				}
				throw ADP.ConnectionStringSyntax(index);
				IL_24D:
				buffer.Append(c);
				goto IL_255;
			}
			IL_262:
			switch (parserState)
			{
			case DbConnectionOptions.ParserState.NothingYet:
			case DbConnectionOptions.ParserState.KeyEnd:
			case DbConnectionOptions.ParserState.NullTermination:
				break;
			case DbConnectionOptions.ParserState.Key:
			case DbConnectionOptions.ParserState.DoubleQuoteValue:
			case DbConnectionOptions.ParserState.SingleQuoteValue:
			case DbConnectionOptions.ParserState.BraceQuoteValue:
				throw ADP.ConnectionStringSyntax(index);
			case DbConnectionOptions.ParserState.KeyEqual:
				keyname = DbConnectionOptions.GetKeyName(buffer);
				if (ADP.IsEmpty(keyname))
				{
					throw ADP.ConnectionStringSyntax(index);
				}
				break;
			case DbConnectionOptions.ParserState.UnquotedValue:
			{
				keyvalue = DbConnectionOptions.GetKeyValue(buffer, true);
				char c2 = keyvalue[keyvalue.Length - 1];
				if (!useOdbcRules && ('\'' == c2 || '"' == c2))
				{
					throw ADP.ConnectionStringSyntax(index);
				}
				break;
			}
			case DbConnectionOptions.ParserState.DoubleQuoteValueQuote:
			case DbConnectionOptions.ParserState.SingleQuoteValueQuote:
			case DbConnectionOptions.ParserState.BraceQuoteValueQuote:
			case DbConnectionOptions.ParserState.QuotedValueEnd:
				keyvalue = DbConnectionOptions.GetKeyValue(buffer, false);
				break;
			default:
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidParserState2);
			}
			if (';' == c && currentPosition < connectionString.Length)
			{
				currentPosition++;
			}
			return currentPosition;
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x00064ED4 File Offset: 0x000642D4
		private static bool IsValueValidInternal(string keyvalue)
		{
			return keyvalue == null || -1 == keyvalue.IndexOf('\0');
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x00064EF4 File Offset: 0x000642F4
		private static bool IsKeyNameValid(string keyname)
		{
			return keyname != null && (0 < keyname.Length && ';' != keyname[0] && !char.IsWhiteSpace(keyname[0])) && -1 == keyname.IndexOf('\0');
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x00064F34 File Offset: 0x00064334
		private static NameValuePair ParseInternal(Hashtable parsetable, string connectionString, bool buildChain, Hashtable synonyms, bool firstKey)
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
				i = DbConnectionOptions.GetKeyValuePair(connectionString, num, buffer, firstKey, out text, out value);
				if (ADP.IsEmpty(text))
				{
					break;
				}
				string text2 = (synonyms != null) ? ((string)synonyms[text]) : text;
				if (!DbConnectionOptions.IsKeyNameValid(text2))
				{
					throw ADP.KeywordNotSupported(text);
				}
				if (!firstKey || !parsetable.Contains(text2))
				{
					parsetable[text2] = value;
				}
				if (nameValuePair != null)
				{
					nameValuePair = (nameValuePair.Next = new NameValuePair(text2, value, i - num));
				}
				else if (buildChain)
				{
					nameValuePair = (result = new NameValuePair(text2, value, i - num));
				}
			}
			return result;
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x00064FF4 File Offset: 0x000643F4
		internal NameValuePair ReplacePasswordPwd(out string constr, bool fakePassword)
		{
			int num = 0;
			NameValuePair result = null;
			NameValuePair nameValuePair = null;
			NameValuePair nameValuePair2 = null;
			StringBuilder stringBuilder = new StringBuilder(this._usersConnectionString.Length);
			for (NameValuePair nameValuePair3 = this.KeyChain; nameValuePair3 != null; nameValuePair3 = nameValuePair3.Next)
			{
				if ("password" != nameValuePair3.Name && "pwd" != nameValuePair3.Name)
				{
					stringBuilder.Append(this._usersConnectionString, num, nameValuePair3.Length);
					if (fakePassword)
					{
						nameValuePair2 = new NameValuePair(nameValuePair3.Name, nameValuePair3.Value, nameValuePair3.Length);
					}
				}
				else if (fakePassword)
				{
					stringBuilder.Append(nameValuePair3.Name).Append("=*;");
					nameValuePair2 = new NameValuePair(nameValuePair3.Name, "*", nameValuePair3.Name.Length + "=*;".Length);
				}
				if (fakePassword)
				{
					if (nameValuePair != null)
					{
						nameValuePair = (nameValuePair.Next = nameValuePair2);
					}
					else
					{
						result = (nameValuePair = nameValuePair2);
					}
				}
				num += nameValuePair3.Length;
			}
			constr = stringBuilder.ToString();
			return result;
		}

		// Token: 0x040003BE RID: 958
		private const string ConnectionStringValidKeyPattern = "^(?![;\\s])[^\\p{Cc}]+(?<!\\s)$";

		// Token: 0x040003BF RID: 959
		private const string ConnectionStringValidValuePattern = "^[^\0]*$";

		// Token: 0x040003C0 RID: 960
		private const string ConnectionStringQuoteValuePattern = "^[^\"'=;\\s\\p{Cc}]*$";

		// Token: 0x040003C1 RID: 961
		private const string ConnectionStringQuoteOdbcValuePattern = "^\\{([^\\}\0]|\\}\\})*\\}$";

		// Token: 0x040003C2 RID: 962
		internal const string DataDirectory = "|datadirectory|";

		// Token: 0x040003C3 RID: 963
		private readonly string _usersConnectionString;

		// Token: 0x040003C4 RID: 964
		private readonly Hashtable _parsetable;

		// Token: 0x040003C5 RID: 965
		internal readonly NameValuePair KeyChain;

		// Token: 0x040003C6 RID: 966
		internal readonly bool HasPasswordKeyword;

		// Token: 0x040003C7 RID: 967
		internal readonly bool UseOdbcRules;

		// Token: 0x040003C8 RID: 968
		private PermissionSet _permissionset;

		// Token: 0x0200005C RID: 92
		private enum ParserState
		{
			// Token: 0x040003CA RID: 970
			NothingYet = 1,
			// Token: 0x040003CB RID: 971
			Key,
			// Token: 0x040003CC RID: 972
			KeyEqual,
			// Token: 0x040003CD RID: 973
			KeyEnd,
			// Token: 0x040003CE RID: 974
			UnquotedValue,
			// Token: 0x040003CF RID: 975
			DoubleQuoteValue,
			// Token: 0x040003D0 RID: 976
			DoubleQuoteValueQuote,
			// Token: 0x040003D1 RID: 977
			SingleQuoteValue,
			// Token: 0x040003D2 RID: 978
			SingleQuoteValueQuote,
			// Token: 0x040003D3 RID: 979
			BraceQuoteValue,
			// Token: 0x040003D4 RID: 980
			BraceQuoteValueQuote,
			// Token: 0x040003D5 RID: 981
			QuotedValueEnd,
			// Token: 0x040003D6 RID: 982
			NullTermination
		}
	}
}
