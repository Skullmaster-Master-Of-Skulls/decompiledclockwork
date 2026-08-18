using System;
using System.Collections;
using System.Globalization;
using System.Security;
using System.Text;
using System.Text.RegularExpressions;

namespace System.Data.Common
{
	// Token: 0x020002E4 RID: 740
	internal class DbConnectionOptions
	{
		// Token: 0x06002EAB RID: 11947 RVA: 0x00127E08 File Offset: 0x00127208
		public DbConnectionOptions(string connectionString) : this(connectionString, null, false)
		{
		}

		// Token: 0x06002EAC RID: 11948 RVA: 0x00127E20 File Offset: 0x00127220
		public DbConnectionOptions(string connectionString, Hashtable synonyms, bool useOdbcRules)
		{
			this.UseOdbcRules = useOdbcRules;
			this._parsetable = new Hashtable();
			this._usersConnectionString = ((connectionString != null) ? connectionString : "");
			if (0 < this._usersConnectionString.Length)
			{
				this.KeyChain = DbConnectionOptions.ParseInternal(this._parsetable, this._usersConnectionString, true, synonyms, this.UseOdbcRules);
				this.HasPasswordKeyword = (this._parsetable.ContainsKey("password") || this._parsetable.ContainsKey("pwd"));
				this.HasUserIdKeyword = (this._parsetable.ContainsKey("user id") || this._parsetable.ContainsKey("uid"));
			}
		}

		// Token: 0x06002EAD RID: 11949 RVA: 0x00127EDC File Offset: 0x001272DC
		protected DbConnectionOptions(DbConnectionOptions connectionOptions)
		{
			this._usersConnectionString = connectionOptions._usersConnectionString;
			this.HasPasswordKeyword = connectionOptions.HasPasswordKeyword;
			this.HasUserIdKeyword = connectionOptions.HasUserIdKeyword;
			this.UseOdbcRules = connectionOptions.UseOdbcRules;
			this._parsetable = connectionOptions._parsetable;
			this.KeyChain = connectionOptions.KeyChain;
		}

		// Token: 0x06002EAE RID: 11950 RVA: 0x00127F38 File Offset: 0x00127338
		public string UsersConnectionString(bool hidePassword)
		{
			return this.UsersConnectionString(hidePassword, false);
		}

		// Token: 0x06002EAF RID: 11951 RVA: 0x00127F50 File Offset: 0x00127350
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

		// Token: 0x06002EB0 RID: 11952 RVA: 0x00127F90 File Offset: 0x00127390
		internal string UsersConnectionStringForTrace()
		{
			return this.UsersConnectionString(true, true);
		}

		// Token: 0x1700078F RID: 1935
		// (get) Token: 0x06002EB1 RID: 11953 RVA: 0x00127FA8 File Offset: 0x001273A8
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

		// Token: 0x17000790 RID: 1936
		// (get) Token: 0x06002EB2 RID: 11954 RVA: 0x00128080 File Offset: 0x00127480
		internal bool HasPersistablePassword
		{
			get
			{
				return !this.HasPasswordKeyword || this.ConvertValueToBoolean("persist security info", false);
			}
		}

		// Token: 0x17000791 RID: 1937
		// (get) Token: 0x06002EB3 RID: 11955 RVA: 0x001280A4 File Offset: 0x001274A4
		public bool IsEmpty
		{
			get
			{
				return this.KeyChain == null;
			}
		}

		// Token: 0x17000792 RID: 1938
		// (get) Token: 0x06002EB4 RID: 11956 RVA: 0x001280BC File Offset: 0x001274BC
		internal Hashtable Parsetable
		{
			get
			{
				return this._parsetable;
			}
		}

		// Token: 0x17000793 RID: 1939
		// (get) Token: 0x06002EB5 RID: 11957 RVA: 0x001280D0 File Offset: 0x001274D0
		public ICollection Keys
		{
			get
			{
				return this._parsetable.Keys;
			}
		}

		// Token: 0x17000794 RID: 1940
		public string this[string keyword]
		{
			get
			{
				return (string)this._parsetable[keyword];
			}
		}

		// Token: 0x06002EB7 RID: 11959 RVA: 0x00128108 File Offset: 0x00127508
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

		// Token: 0x06002EB8 RID: 11960 RVA: 0x00128290 File Offset: 0x00127690
		public bool ConvertValueToBoolean(string keyName, bool defaultValue)
		{
			object obj = this._parsetable[keyName];
			if (obj == null)
			{
				return defaultValue;
			}
			return DbConnectionOptions.ConvertValueToBooleanInternal(keyName, (string)obj);
		}

		// Token: 0x06002EB9 RID: 11961 RVA: 0x001282BC File Offset: 0x001276BC
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

		// Token: 0x06002EBA RID: 11962 RVA: 0x00128348 File Offset: 0x00127748
		public bool ConvertValueToIntegratedSecurity()
		{
			object obj = this._parsetable["integrated security"];
			return obj != null && this.ConvertValueToIntegratedSecurityInternal((string)obj);
		}

		// Token: 0x06002EBB RID: 11963 RVA: 0x00128378 File Offset: 0x00127778
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

		// Token: 0x06002EBC RID: 11964 RVA: 0x00128420 File Offset: 0x00127820
		public int ConvertValueToInt32(string keyName, int defaultValue)
		{
			object obj = this._parsetable[keyName];
			if (obj == null)
			{
				return defaultValue;
			}
			return DbConnectionOptions.ConvertToInt32Internal(keyName, (string)obj);
		}

		// Token: 0x06002EBD RID: 11965 RVA: 0x0012844C File Offset: 0x0012784C
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

		// Token: 0x06002EBE RID: 11966 RVA: 0x001284B4 File Offset: 0x001278B4
		public string ConvertValueToString(string keyName, string defaultValue)
		{
			string text = (string)this._parsetable[keyName];
			if (text == null)
			{
				return defaultValue;
			}
			return text;
		}

		// Token: 0x06002EBF RID: 11967 RVA: 0x001284DC File Offset: 0x001278DC
		private static bool CompareInsensitiveInvariant(string strvalue, string strconst)
		{
			return StringComparer.OrdinalIgnoreCase.Compare(strvalue, strconst) == 0;
		}

		// Token: 0x06002EC0 RID: 11968 RVA: 0x001284F8 File Offset: 0x001278F8
		public bool ContainsKey(string keyword)
		{
			return this._parsetable.ContainsKey(keyword);
		}

		// Token: 0x06002EC1 RID: 11969 RVA: 0x00128514 File Offset: 0x00127914
		protected internal virtual PermissionSet CreatePermissionSet()
		{
			return null;
		}

		// Token: 0x06002EC2 RID: 11970 RVA: 0x00128524 File Offset: 0x00127924
		internal void DemandPermission()
		{
			if (this._permissionset == null)
			{
				this._permissionset = this.CreatePermissionSet();
			}
			this._permissionset.Demand();
		}

		// Token: 0x06002EC3 RID: 11971 RVA: 0x00128550 File Offset: 0x00127950
		protected internal virtual string Expand()
		{
			return this._usersConnectionString;
		}

		// Token: 0x06002EC4 RID: 11972 RVA: 0x00128564 File Offset: 0x00127964
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
				if (!ADP.GetFullPath(text).StartsWith(text2, StringComparison.Ordinal))
				{
					throw ADP.InvalidConnectionOptionValue(keyword);
				}
			}
			return text;
		}

		// Token: 0x06002EC5 RID: 11973 RVA: 0x00128674 File Offset: 0x00127A74
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
					string name = nameValuePair.Name;
					if (!(name == "driver") && !(name == "pwd") && !(name == "uid"))
					{
						text2 = DbConnectionOptions.ExpandDataDirectory(nameValuePair.Name, text2, ref text);
					}
				}
				else
				{
					string name2 = nameValuePair.Name;
					uint num2 = <PrivateImplementationDetails><System_Data_netmodule>.ComputeStringHash(name2);
					if (num2 <= 2781420622U)
					{
						if (num2 <= 1433271620U)
						{
							if (num2 != 910909208U)
							{
								if (num2 == 1433271620U)
								{
									if (name2 == "pwd")
									{
										goto IL_192;
									}
								}
							}
							else if (name2 == "password")
							{
								goto IL_192;
							}
						}
						else if (num2 != 1556604621U)
						{
							if (num2 == 2781420622U)
							{
								if (name2 == "data provider")
								{
									goto IL_192;
								}
							}
						}
						else if (name2 == "uid")
						{
							goto IL_192;
						}
					}
					else if (num2 <= 3082861500U)
					{
						if (num2 != 2906666283U)
						{
							if (num2 == 3082861500U)
							{
								if (name2 == "provider")
								{
									goto IL_192;
								}
							}
						}
						else if (name2 == "user id")
						{
							goto IL_192;
						}
					}
					else if (num2 != 4008387664U)
					{
						if (num2 == 4015305829U)
						{
							if (name2 == "extended properties")
							{
								goto IL_192;
							}
						}
					}
					else if (name2 == "remote provider")
					{
						goto IL_192;
					}
					text2 = DbConnectionOptions.ExpandDataDirectory(nameValuePair.Name, text2, ref text);
				}
				IL_192:
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

		// Token: 0x06002EC6 RID: 11974 RVA: 0x001288B8 File Offset: 0x00127CB8
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

		// Token: 0x06002EC7 RID: 11975 RVA: 0x00128964 File Offset: 0x00127D64
		private static string GetKeyName(StringBuilder buffer)
		{
			int num = buffer.Length;
			while (0 < num && char.IsWhiteSpace(buffer[num - 1]))
			{
				num--;
			}
			return buffer.ToString(0, num).ToLower(CultureInfo.InvariantCulture);
		}

		// Token: 0x06002EC8 RID: 11976 RVA: 0x001289A4 File Offset: 0x00127DA4
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

		// Token: 0x06002EC9 RID: 11977 RVA: 0x001289FC File Offset: 0x00127DFC
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
								goto IL_249;
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
						goto IL_249;
					}
					break;
				case DbConnectionOptions.ParserState.KeyEqual:
					if (!useOdbcRules && '=' == c)
					{
						parserState = DbConnectionOptions.ParserState.Key;
						goto IL_249;
					}
					keyname = DbConnectionOptions.GetKeyName(buffer);
					if (ADP.IsEmpty(keyname))
					{
						throw ADP.ConnectionStringSyntax(index);
					}
					buffer.Length = 0;
					parserState = DbConnectionOptions.ParserState.KeyEnd;
					goto IL_108;
				case DbConnectionOptions.ParserState.KeyEnd:
					goto IL_108;
				case DbConnectionOptions.ParserState.UnquotedValue:
					if (char.IsWhiteSpace(c))
					{
						goto IL_249;
					}
					if (char.IsControl(c))
					{
						goto IL_25E;
					}
					if (';' == c)
					{
						goto IL_25E;
					}
					goto IL_249;
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
						goto IL_249;
					}
					break;
				case DbConnectionOptions.ParserState.DoubleQuoteValueQuote:
					if ('"' == c)
					{
						parserState = DbConnectionOptions.ParserState.DoubleQuoteValue;
						goto IL_249;
					}
					keyvalue = DbConnectionOptions.GetKeyValue(buffer, false);
					parserState = DbConnectionOptions.ParserState.QuotedValueEnd;
					goto IL_213;
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
						goto IL_249;
					}
					break;
				case DbConnectionOptions.ParserState.SingleQuoteValueQuote:
					if ('\'' == c)
					{
						parserState = DbConnectionOptions.ParserState.SingleQuoteValue;
						goto IL_249;
					}
					keyvalue = DbConnectionOptions.GetKeyValue(buffer, false);
					parserState = DbConnectionOptions.ParserState.QuotedValueEnd;
					goto IL_213;
				case DbConnectionOptions.ParserState.BraceQuoteValue:
					if ('}' == c)
					{
						parserState = DbConnectionOptions.ParserState.BraceQuoteValueQuote;
						goto IL_249;
					}
					if (c == '\0')
					{
						throw ADP.ConnectionStringSyntax(index);
					}
					goto IL_249;
				case DbConnectionOptions.ParserState.BraceQuoteValueQuote:
					if ('}' == c)
					{
						parserState = DbConnectionOptions.ParserState.BraceQuoteValue;
						goto IL_249;
					}
					keyvalue = DbConnectionOptions.GetKeyValue(buffer, false);
					parserState = DbConnectionOptions.ParserState.QuotedValueEnd;
					goto IL_213;
				case DbConnectionOptions.ParserState.QuotedValueEnd:
					goto IL_213;
				case DbConnectionOptions.ParserState.NullTermination:
					if (c != '\0' && !char.IsWhiteSpace(c))
					{
						throw ADP.ConnectionStringSyntax(currentPosition);
					}
					break;
				default:
					throw ADP.InternalError(ADP.InternalErrorCode.InvalidParserState1);
				}
				IL_251:
				currentPosition++;
				continue;
				IL_108:
				if (char.IsWhiteSpace(c))
				{
					goto IL_251;
				}
				if (useOdbcRules)
				{
					if ('{' == c)
					{
						parserState = DbConnectionOptions.ParserState.BraceQuoteValue;
						goto IL_249;
					}
				}
				else
				{
					if ('\'' == c)
					{
						parserState = DbConnectionOptions.ParserState.SingleQuoteValue;
						goto IL_251;
					}
					if ('"' == c)
					{
						parserState = DbConnectionOptions.ParserState.DoubleQuoteValue;
						goto IL_251;
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
				goto IL_249;
				IL_213:
				if (char.IsWhiteSpace(c))
				{
					goto IL_251;
				}
				if (';' == c)
				{
					break;
				}
				if (c == '\0')
				{
					parserState = DbConnectionOptions.ParserState.NullTermination;
					goto IL_251;
				}
				throw ADP.ConnectionStringSyntax(index);
				IL_249:
				buffer.Append(c);
				goto IL_251;
			}
			IL_25E:
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

		// Token: 0x06002ECA RID: 11978 RVA: 0x00128D20 File Offset: 0x00128120
		private static bool IsValueValidInternal(string keyvalue)
		{
			return keyvalue == null || -1 == keyvalue.IndexOf('\0');
		}

		// Token: 0x06002ECB RID: 11979 RVA: 0x00128D3C File Offset: 0x0012813C
		private static bool IsKeyNameValid(string keyname)
		{
			return keyname != null && (0 < keyname.Length && ';' != keyname[0] && !char.IsWhiteSpace(keyname[0])) && -1 == keyname.IndexOf('\0');
		}

		// Token: 0x06002ECC RID: 11980 RVA: 0x00128D7C File Offset: 0x0012817C
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

		// Token: 0x06002ECD RID: 11981 RVA: 0x00128E34 File Offset: 0x00128234
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

		// Token: 0x06002ECE RID: 11982 RVA: 0x00128F48 File Offset: 0x00128348
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

		// Token: 0x04001CC0 RID: 7360
		private const string ConnectionStringValidKeyPattern = "^(?![;\\s])[^\\p{Cc}]+(?<!\\s)$";

		// Token: 0x04001CC1 RID: 7361
		private const string ConnectionStringValidValuePattern = "^[^\0]*$";

		// Token: 0x04001CC2 RID: 7362
		private const string ConnectionStringQuoteValuePattern = "^[^\"'=;\\s\\p{Cc}]*$";

		// Token: 0x04001CC3 RID: 7363
		private const string ConnectionStringQuoteOdbcValuePattern = "^\\{([^\\}\0]|\\}\\})*\\}$";

		// Token: 0x04001CC4 RID: 7364
		internal const string DataDirectory = "|datadirectory|";

		// Token: 0x04001CC5 RID: 7365
		private static readonly Regex ConnectionStringValidKeyRegex = new Regex("^(?![;\\s])[^\\p{Cc}]+(?<!\\s)$", RegexOptions.Compiled);

		// Token: 0x04001CC6 RID: 7366
		private static readonly Regex ConnectionStringValidValueRegex = new Regex("^[^\0]*$", RegexOptions.Compiled);

		// Token: 0x04001CC7 RID: 7367
		private static readonly Regex ConnectionStringQuoteValueRegex = new Regex("^[^\"'=;\\s\\p{Cc}]*$", RegexOptions.Compiled);

		// Token: 0x04001CC8 RID: 7368
		private static readonly Regex ConnectionStringQuoteOdbcValueRegex = new Regex("^\\{([^\\}\0]|\\}\\})*\\}$", RegexOptions.ExplicitCapture | RegexOptions.Compiled);

		// Token: 0x04001CC9 RID: 7369
		private readonly string _usersConnectionString;

		// Token: 0x04001CCA RID: 7370
		private readonly Hashtable _parsetable;

		// Token: 0x04001CCB RID: 7371
		internal readonly NameValuePair KeyChain;

		// Token: 0x04001CCC RID: 7372
		internal readonly bool HasPasswordKeyword;

		// Token: 0x04001CCD RID: 7373
		internal readonly bool HasUserIdKeyword;

		// Token: 0x04001CCE RID: 7374
		internal readonly bool UseOdbcRules;

		// Token: 0x04001CCF RID: 7375
		private PermissionSet _permissionset;

		// Token: 0x02000436 RID: 1078
		private enum ParserState
		{
			// Token: 0x0400233E RID: 9022
			NothingYet = 1,
			// Token: 0x0400233F RID: 9023
			Key,
			// Token: 0x04002340 RID: 9024
			KeyEqual,
			// Token: 0x04002341 RID: 9025
			KeyEnd,
			// Token: 0x04002342 RID: 9026
			UnquotedValue,
			// Token: 0x04002343 RID: 9027
			DoubleQuoteValue,
			// Token: 0x04002344 RID: 9028
			DoubleQuoteValueQuote,
			// Token: 0x04002345 RID: 9029
			SingleQuoteValue,
			// Token: 0x04002346 RID: 9030
			SingleQuoteValueQuote,
			// Token: 0x04002347 RID: 9031
			BraceQuoteValue,
			// Token: 0x04002348 RID: 9032
			BraceQuoteValueQuote,
			// Token: 0x04002349 RID: 9033
			QuotedValueEnd,
			// Token: 0x0400234A RID: 9034
			NullTermination
		}
	}
}
