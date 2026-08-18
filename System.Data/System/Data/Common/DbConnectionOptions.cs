using System;
using System.Collections;
using System.Globalization;
using System.Security;
using System.Text;
using System.Text.RegularExpressions;

namespace System.Data.Common
{
	// Token: 0x0200012A RID: 298
	internal class DbConnectionOptions
	{
		// Token: 0x06001373 RID: 4979 RVA: 0x0023AE68 File Offset: 0x0023A268
		public DbConnectionOptions(string connectionString) : this(connectionString, null, false)
		{
		}

		// Token: 0x06001374 RID: 4980 RVA: 0x0023AE88 File Offset: 0x0023A288
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

		// Token: 0x06001375 RID: 4981 RVA: 0x0023AF18 File Offset: 0x0023A318
		protected DbConnectionOptions(DbConnectionOptions connectionOptions)
		{
			this._usersConnectionString = connectionOptions._usersConnectionString;
			this.HasPasswordKeyword = connectionOptions.HasPasswordKeyword;
			this.UseOdbcRules = connectionOptions.UseOdbcRules;
			this._parsetable = connectionOptions._parsetable;
			this.KeyChain = connectionOptions.KeyChain;
		}

		// Token: 0x06001376 RID: 4982 RVA: 0x0023AF68 File Offset: 0x0023A368
		public string UsersConnectionString(bool hidePassword)
		{
			return this.UsersConnectionString(hidePassword, false);
		}

		// Token: 0x06001377 RID: 4983 RVA: 0x0023AF88 File Offset: 0x0023A388
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

		// Token: 0x06001378 RID: 4984 RVA: 0x0023AFC8 File Offset: 0x0023A3C8
		internal string UsersConnectionStringForTrace()
		{
			return this.UsersConnectionString(true, true);
		}

		// Token: 0x1700029C RID: 668
		// (get) Token: 0x06001379 RID: 4985 RVA: 0x0023AFE8 File Offset: 0x0023A3E8
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

		// Token: 0x1700029D RID: 669
		// (get) Token: 0x0600137A RID: 4986 RVA: 0x0023B0C8 File Offset: 0x0023A4C8
		internal bool HasPersistablePassword
		{
			get
			{
				return !this.HasPasswordKeyword || this.ConvertValueToBoolean("persist security info", false);
			}
		}

		// Token: 0x1700029E RID: 670
		// (get) Token: 0x0600137B RID: 4987 RVA: 0x0023B0F8 File Offset: 0x0023A4F8
		public bool IsEmpty
		{
			get
			{
				return null == this.KeyChain;
			}
		}

		// Token: 0x1700029F RID: 671
		// (get) Token: 0x0600137C RID: 4988 RVA: 0x0023B118 File Offset: 0x0023A518
		internal Hashtable Parsetable
		{
			get
			{
				return this._parsetable;
			}
		}

		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x0600137D RID: 4989 RVA: 0x0023B138 File Offset: 0x0023A538
		public ICollection Keys
		{
			get
			{
				return this._parsetable.Keys;
			}
		}

		// Token: 0x170002A1 RID: 673
		public string this[string keyword]
		{
			get
			{
				return (string)this._parsetable[keyword];
			}
		}

		// Token: 0x0600137F RID: 4991 RVA: 0x0023B178 File Offset: 0x0023A578
		internal static void AppendKeyValuePairBuilder(StringBuilder builder, string keyName, string keyValue, bool useOdbcRules)
		{
			ADP.CheckArgumentNull(builder, "builder");
			ADP.CheckArgumentLength(keyName, "keyName");
			if (keyName == null || !DbConnectionOptions.ConnectionStringValidKeyRegex.IsMatch(keyName))
			{
				throw ADP.InvalidKeyname(keyName);
			}
			if (keyValue != null && !DbConnectionOptions.IsValueValidInternal(keyValue))
			{
				throw ADP.InvalidValue(keyName);
			}
			if (0 < builder.Length && ';' != builder[builder.Length - 1])
			{
				builder.Append(";");
			}
			if (useOdbcRules)
			{
				builder.Append(keyName);
			}
			else
			{
				builder.Append(keyName.Replace("=", "=="));
			}
			builder.Append("=");
			if (keyValue != null)
			{
				if (useOdbcRules)
				{
					if (0 < keyValue.Length && ('{' == keyValue[0] || 0 <= keyValue.IndexOf(';') || string.Compare("Driver", keyName, StringComparison.OrdinalIgnoreCase) == 0) && !DbConnectionOptions.ConnectionStringQuoteOdbcValueRegex.IsMatch(keyValue))
					{
						builder.Append('{').Append(keyValue.Replace("}", "}}")).Append('}');
						return;
					}
					builder.Append(keyValue);
					return;
				}
				else
				{
					if (DbConnectionOptions.ConnectionStringQuoteValueRegex.IsMatch(keyValue))
					{
						builder.Append(keyValue);
						return;
					}
					if (-1 != keyValue.IndexOf('"') && -1 == keyValue.IndexOf('\''))
					{
						builder.Append('\'');
						builder.Append(keyValue);
						builder.Append('\'');
						return;
					}
					builder.Append('"');
					builder.Append(keyValue.Replace("\"", "\"\""));
					builder.Append('"');
				}
			}
		}

		// Token: 0x06001380 RID: 4992 RVA: 0x0023B308 File Offset: 0x0023A708
		public bool ConvertValueToBoolean(string keyName, bool defaultValue)
		{
			object obj = this._parsetable[keyName];
			if (obj == null)
			{
				return defaultValue;
			}
			return DbConnectionOptions.ConvertValueToBooleanInternal(keyName, (string)obj);
		}

		// Token: 0x06001381 RID: 4993 RVA: 0x0023B338 File Offset: 0x0023A738
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

		// Token: 0x06001382 RID: 4994 RVA: 0x0023B3C8 File Offset: 0x0023A7C8
		public bool ConvertValueToIntegratedSecurity()
		{
			object obj = this._parsetable["integrated security"];
			return obj != null && this.ConvertValueToIntegratedSecurityInternal((string)obj);
		}

		// Token: 0x06001383 RID: 4995 RVA: 0x0023B3F8 File Offset: 0x0023A7F8
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

		// Token: 0x06001384 RID: 4996 RVA: 0x0023B4A8 File Offset: 0x0023A8A8
		public int ConvertValueToInt32(string keyName, int defaultValue)
		{
			object obj = this._parsetable[keyName];
			if (obj == null)
			{
				return defaultValue;
			}
			return DbConnectionOptions.ConvertToInt32Internal(keyName, (string)obj);
		}

		// Token: 0x06001385 RID: 4997 RVA: 0x0023B4D8 File Offset: 0x0023A8D8
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

		// Token: 0x06001386 RID: 4998 RVA: 0x0023B548 File Offset: 0x0023A948
		public string ConvertValueToString(string keyName, string defaultValue)
		{
			string text = (string)this._parsetable[keyName];
			if (text == null)
			{
				return defaultValue;
			}
			return text;
		}

		// Token: 0x06001387 RID: 4999 RVA: 0x0023B578 File Offset: 0x0023A978
		private static bool CompareInsensitiveInvariant(string strvalue, string strconst)
		{
			return 0 == StringComparer.OrdinalIgnoreCase.Compare(strvalue, strconst);
		}

		// Token: 0x06001388 RID: 5000 RVA: 0x0023B598 File Offset: 0x0023A998
		public bool ContainsKey(string keyword)
		{
			return this._parsetable.ContainsKey(keyword);
		}

		// Token: 0x06001389 RID: 5001 RVA: 0x0023B5B8 File Offset: 0x0023A9B8
		protected internal virtual PermissionSet CreatePermissionSet()
		{
			return null;
		}

		// Token: 0x0600138A RID: 5002 RVA: 0x0023B5C8 File Offset: 0x0023A9C8
		internal void DemandPermission()
		{
			if (this._permissionset == null)
			{
				this._permissionset = this.CreatePermissionSet();
			}
			this._permissionset.Demand();
		}

		// Token: 0x0600138B RID: 5003 RVA: 0x0023B5F8 File Offset: 0x0023A9F8
		protected internal virtual string Expand()
		{
			return this._usersConnectionString;
		}

		// Token: 0x0600138C RID: 5004 RVA: 0x0023B618 File Offset: 0x0023AA18
		internal static string ExpandDataDirectory(string keyword, string value, ref string datadir)
		{
			string text = null;
			if (value != null && value.StartsWith("|datadirectory|", StringComparison.OrdinalIgnoreCase))
			{
				string text2 = datadir;
				if (text2 == null)
				{
					object data = AppDomain.CurrentDomain.GetData("DataDirectory");
					text2 = (data as string);
					if (data != null && text2 == null)
					{
						throw ADP.InvalidDataDirectory();
					}
					if (ADP.IsEmpty(text2))
					{
						text2 = AppDomain.CurrentDomain.BaseDirectory;
					}
					if (text2 == null)
					{
						text2 = "";
					}
					datadir = text2;
				}
				int length = "|datadirectory|".Length;
				bool flag = 0 < text2.Length && text2[text2.Length - 1] == '\\';
				bool flag2 = length < value.Length && value[length] == '\\';
				if (!flag && !flag2)
				{
					text = text2 + '\\' + value.Substring(length);
				}
				else if (flag && flag2)
				{
					text = text2 + value.Substring(length + 1);
				}
				else
				{
					text = text2 + value.Substring(length);
				}
				if (!ADP.GetFullPath(text).StartsWith(text2, StringComparison.Ordinal))
				{
					throw ADP.InvalidConnectionOptionValue(keyword);
				}
			}
			return text;
		}

		// Token: 0x0600138D RID: 5005 RVA: 0x0023B728 File Offset: 0x0023AB28
		internal string ExpandDataDirectories(ref string filename, ref int position)
		{
			StringBuilder stringBuilder = new StringBuilder(this._usersConnectionString.Length);
			string text = null;
			int num = 0;
			bool flag = false;
			string text2;
			for (NameValuePair nameValuePair = this.KeyChain; nameValuePair != null; nameValuePair = nameValuePair.Next)
			{
				text2 = nameValuePair.Value;
				if (this.UseOdbcRules)
				{
					string name;
					if ((name = nameValuePair.Name) == null || (!(name == "driver") && !(name == "pwd") && !(name == "uid")))
					{
						text2 = DbConnectionOptions.ExpandDataDirectory(nameValuePair.Name, text2, ref text);
					}
				}
				else
				{
					string name2;
					switch (name2 = nameValuePair.Name)
					{
					case "provider":
					case "data provider":
					case "remote provider":
					case "extended properties":
					case "user id":
					case "password":
					case "uid":
					case "pwd":
						goto IL_151;
					}
					text2 = DbConnectionOptions.ExpandDataDirectory(nameValuePair.Name, text2, ref text);
				}
				IL_151:
				if (text2 == null)
				{
					text2 = nameValuePair.Value;
				}
				if (this.UseOdbcRules || "file name" != nameValuePair.Name)
				{
					if (text2 != nameValuePair.Value)
					{
						flag = true;
						DbConnectionOptions.AppendKeyValuePairBuilder(stringBuilder, nameValuePair.Name, text2, this.UseOdbcRules);
						stringBuilder.Append(';');
					}
					else
					{
						stringBuilder.Append(this._usersConnectionString, num, nameValuePair.Length);
					}
				}
				else
				{
					flag = true;
					filename = text2;
					position = stringBuilder.Length;
				}
				num += nameValuePair.Length;
			}
			if (flag)
			{
				text2 = stringBuilder.ToString();
			}
			else
			{
				text2 = null;
			}
			return text2;
		}

		// Token: 0x0600138E RID: 5006 RVA: 0x0023B928 File Offset: 0x0023AD28
		internal string ExpandKeyword(string keyword, string replacementValue)
		{
			bool flag = false;
			int num = 0;
			StringBuilder stringBuilder = new StringBuilder(this._usersConnectionString.Length);
			for (NameValuePair nameValuePair = this.KeyChain; nameValuePair != null; nameValuePair = nameValuePair.Next)
			{
				if (nameValuePair.Name == keyword && nameValuePair.Value == this[keyword])
				{
					DbConnectionOptions.AppendKeyValuePairBuilder(stringBuilder, nameValuePair.Name, replacementValue, this.UseOdbcRules);
					stringBuilder.Append(';');
					flag = true;
				}
				else
				{
					stringBuilder.Append(this._usersConnectionString, num, nameValuePair.Length);
				}
				num += nameValuePair.Length;
			}
			if (!flag)
			{
				DbConnectionOptions.AppendKeyValuePairBuilder(stringBuilder, keyword, replacementValue, this.UseOdbcRules);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600138F RID: 5007 RVA: 0x0023B9D8 File Offset: 0x0023ADD8
		private static string GetKeyName(StringBuilder buffer)
		{
			int num = buffer.Length;
			while (0 < num && char.IsWhiteSpace(buffer[num - 1]))
			{
				num--;
			}
			return buffer.ToString(0, num).ToLower(CultureInfo.InvariantCulture);
		}

		// Token: 0x06001390 RID: 5008 RVA: 0x0023BA18 File Offset: 0x0023AE18
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

		// Token: 0x06001391 RID: 5009 RVA: 0x0023BA78 File Offset: 0x0023AE78
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

		// Token: 0x06001392 RID: 5010 RVA: 0x0023BDA8 File Offset: 0x0023B1A8
		private static bool IsValueValidInternal(string keyvalue)
		{
			return keyvalue == null || -1 == keyvalue.IndexOf('\0');
		}

		// Token: 0x06001393 RID: 5011 RVA: 0x0023BDC8 File Offset: 0x0023B1C8
		private static bool IsKeyNameValid(string keyname)
		{
			return keyname != null && (0 < keyname.Length && ';' != keyname[0] && !char.IsWhiteSpace(keyname[0])) && -1 == keyname.IndexOf('\0');
		}

		// Token: 0x06001394 RID: 5012 RVA: 0x0023BE08 File Offset: 0x0023B208
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

		// Token: 0x06001395 RID: 5013 RVA: 0x0023BEC8 File Offset: 0x0023B2C8
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

		// Token: 0x06001396 RID: 5014 RVA: 0x0023BFD8 File Offset: 0x0023B3D8
		internal static void ValidateKeyValuePair(string keyword, string value)
		{
			if (keyword == null || !DbConnectionOptions.ConnectionStringValidKeyRegex.IsMatch(keyword))
			{
				throw ADP.InvalidKeyname(keyword);
			}
			if (value != null && !DbConnectionOptions.ConnectionStringValidValueRegex.IsMatch(value))
			{
				throw ADP.InvalidValue(keyword);
			}
		}

		// Token: 0x04000C08 RID: 3080
		private const string ConnectionStringValidKeyPattern = "^(?![;\\s])[^\\p{Cc}]+(?<!\\s)$";

		// Token: 0x04000C09 RID: 3081
		private const string ConnectionStringValidValuePattern = "^[^\0]*$";

		// Token: 0x04000C0A RID: 3082
		private const string ConnectionStringQuoteValuePattern = "^[^\"'=;\\s\\p{Cc}]*$";

		// Token: 0x04000C0B RID: 3083
		private const string ConnectionStringQuoteOdbcValuePattern = "^\\{([^\\}\0]|\\}\\})*\\}$";

		// Token: 0x04000C0C RID: 3084
		internal const string DataDirectory = "|datadirectory|";

		// Token: 0x04000C0D RID: 3085
		private static readonly Regex ConnectionStringValidKeyRegex = new Regex("^(?![;\\s])[^\\p{Cc}]+(?<!\\s)$", RegexOptions.Compiled);

		// Token: 0x04000C0E RID: 3086
		private static readonly Regex ConnectionStringValidValueRegex = new Regex("^[^\0]*$", RegexOptions.Compiled);

		// Token: 0x04000C0F RID: 3087
		private static readonly Regex ConnectionStringQuoteValueRegex = new Regex("^[^\"'=;\\s\\p{Cc}]*$", RegexOptions.Compiled);

		// Token: 0x04000C10 RID: 3088
		private static readonly Regex ConnectionStringQuoteOdbcValueRegex = new Regex("^\\{([^\\}\0]|\\}\\})*\\}$", RegexOptions.ExplicitCapture | RegexOptions.Compiled);

		// Token: 0x04000C11 RID: 3089
		private readonly string _usersConnectionString;

		// Token: 0x04000C12 RID: 3090
		private readonly Hashtable _parsetable;

		// Token: 0x04000C13 RID: 3091
		internal readonly NameValuePair KeyChain;

		// Token: 0x04000C14 RID: 3092
		internal readonly bool HasPasswordKeyword;

		// Token: 0x04000C15 RID: 3093
		internal readonly bool UseOdbcRules;

		// Token: 0x04000C16 RID: 3094
		private PermissionSet _permissionset;

		// Token: 0x0200012B RID: 299
		private enum ParserState
		{
			// Token: 0x04000C18 RID: 3096
			NothingYet = 1,
			// Token: 0x04000C19 RID: 3097
			Key,
			// Token: 0x04000C1A RID: 3098
			KeyEqual,
			// Token: 0x04000C1B RID: 3099
			KeyEnd,
			// Token: 0x04000C1C RID: 3100
			UnquotedValue,
			// Token: 0x04000C1D RID: 3101
			DoubleQuoteValue,
			// Token: 0x04000C1E RID: 3102
			DoubleQuoteValueQuote,
			// Token: 0x04000C1F RID: 3103
			SingleQuoteValue,
			// Token: 0x04000C20 RID: 3104
			SingleQuoteValueQuote,
			// Token: 0x04000C21 RID: 3105
			BraceQuoteValue,
			// Token: 0x04000C22 RID: 3106
			BraceQuoteValueQuote,
			// Token: 0x04000C23 RID: 3107
			QuotedValueEnd,
			// Token: 0x04000C24 RID: 3108
			NullTermination
		}
	}
}
