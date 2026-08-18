using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web.UI.WebControls;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x020004CC RID: 1228
	internal static class SqlDataSourceParameterParser
	{
		// Token: 0x06002C68 RID: 11368 RVA: 0x000F9DCC File Offset: 0x000F8DCC
		public static Parameter[] ParseCommandText(string providerName, string commandText)
		{
			if (string.IsNullOrEmpty(providerName))
			{
				providerName = "System.Data.SqlClient";
			}
			if (string.IsNullOrEmpty(commandText))
			{
				commandText = string.Empty;
			}
			SqlDataSourceParameterParser.ParameterParser parameterParser = null;
			string a;
			if ((a = providerName.ToLowerInvariant()) != null)
			{
				if (!(a == "system.data.sqlclient"))
				{
					if (!(a == "system.data.odbc") && !(a == "system.data.oledb"))
					{
						if (a == "system.data.oracleclient")
						{
							parameterParser = new SqlDataSourceParameterParser.OracleClientParameterParser();
						}
					}
					else
					{
						parameterParser = new SqlDataSourceParameterParser.MiscParameterParser();
					}
				}
				else
				{
					parameterParser = new SqlDataSourceParameterParser.SqlClientParameterParser();
				}
			}
			if (parameterParser == null)
			{
				return new Parameter[0];
			}
			return parameterParser.ParseCommandText(commandText);
		}

		// Token: 0x020004CD RID: 1229
		private abstract class ParameterParser
		{
			// Token: 0x06002C69 RID: 11369
			public abstract Parameter[] ParseCommandText(string commandText);
		}

		// Token: 0x020004CE RID: 1230
		private sealed class SqlClientParameterParser : SqlDataSourceParameterParser.ParameterParser
		{
			// Token: 0x06002C6B RID: 11371 RVA: 0x000F9E68 File Offset: 0x000F8E68
			private static bool IsValidParamNameChar(char c)
			{
				return char.IsLetterOrDigit(c) || c == '@' || c == '$' || c == '#' || c == '_';
			}

			// Token: 0x06002C6C RID: 11372 RVA: 0x000F9E88 File Offset: 0x000F8E88
			public override Parameter[] ParseCommandText(string commandText)
			{
				int i = 0;
				int length = commandText.Length;
				SqlDataSourceParameterParser.SqlClientParameterParser.State state = SqlDataSourceParameterParser.SqlClientParameterParser.State.InText;
				List<Parameter> list = new List<Parameter>();
				StringCollection stringCollection = new StringCollection();
				while (i < length)
				{
					switch (state)
					{
					case SqlDataSourceParameterParser.SqlClientParameterParser.State.InText:
						if (commandText[i] == '\'')
						{
							state = SqlDataSourceParameterParser.SqlClientParameterParser.State.InQuote;
						}
						else if (commandText[i] == '"')
						{
							state = SqlDataSourceParameterParser.SqlClientParameterParser.State.InDoubleQuote;
						}
						else if (commandText[i] == '[')
						{
							state = SqlDataSourceParameterParser.SqlClientParameterParser.State.InBracket;
						}
						else if (commandText[i] == '@')
						{
							state = SqlDataSourceParameterParser.SqlClientParameterParser.State.InParameter;
						}
						else
						{
							i++;
						}
						break;
					case SqlDataSourceParameterParser.SqlClientParameterParser.State.InQuote:
						i++;
						while (i < length && commandText[i] != '\'')
						{
							i++;
						}
						i++;
						state = SqlDataSourceParameterParser.SqlClientParameterParser.State.InText;
						break;
					case SqlDataSourceParameterParser.SqlClientParameterParser.State.InDoubleQuote:
						i++;
						while (i < length && commandText[i] != '"')
						{
							i++;
						}
						i++;
						state = SqlDataSourceParameterParser.SqlClientParameterParser.State.InText;
						break;
					case SqlDataSourceParameterParser.SqlClientParameterParser.State.InBracket:
						i++;
						while (i < length && commandText[i] != ']')
						{
							i++;
						}
						i++;
						state = SqlDataSourceParameterParser.SqlClientParameterParser.State.InText;
						break;
					case SqlDataSourceParameterParser.SqlClientParameterParser.State.InParameter:
					{
						i++;
						string text = string.Empty;
						while (i < length && SqlDataSourceParameterParser.SqlClientParameterParser.IsValidParamNameChar(commandText[i]))
						{
							text += commandText[i];
							i++;
						}
						if (!text.StartsWith("@", StringComparison.Ordinal))
						{
							Parameter item = new Parameter(text);
							if (!stringCollection.Contains(text))
							{
								list.Add(item);
								stringCollection.Add(text);
							}
						}
						state = SqlDataSourceParameterParser.SqlClientParameterParser.State.InText;
						break;
					}
					}
				}
				return list.ToArray();
			}

			// Token: 0x020004CF RID: 1231
			private enum State
			{
				// Token: 0x04001E47 RID: 7751
				InText,
				// Token: 0x04001E48 RID: 7752
				InQuote,
				// Token: 0x04001E49 RID: 7753
				InDoubleQuote,
				// Token: 0x04001E4A RID: 7754
				InBracket,
				// Token: 0x04001E4B RID: 7755
				InParameter
			}
		}

		// Token: 0x020004D0 RID: 1232
		private sealed class MiscParameterParser : SqlDataSourceParameterParser.ParameterParser
		{
			// Token: 0x06002C6E RID: 11374 RVA: 0x000FA014 File Offset: 0x000F9014
			public override Parameter[] ParseCommandText(string commandText)
			{
				int i = 0;
				int length = commandText.Length;
				SqlDataSourceParameterParser.MiscParameterParser.State state = SqlDataSourceParameterParser.MiscParameterParser.State.InText;
				List<Parameter> list = new List<Parameter>();
				while (i < length)
				{
					switch (state)
					{
					case SqlDataSourceParameterParser.MiscParameterParser.State.InText:
						if (commandText[i] == '\'')
						{
							state = SqlDataSourceParameterParser.MiscParameterParser.State.InQuote;
						}
						else if (commandText[i] == '"')
						{
							state = SqlDataSourceParameterParser.MiscParameterParser.State.InDoubleQuote;
						}
						else if (commandText[i] == '[')
						{
							state = SqlDataSourceParameterParser.MiscParameterParser.State.InBracket;
						}
						else if (commandText[i] == '?')
						{
							state = SqlDataSourceParameterParser.MiscParameterParser.State.InQuestion;
						}
						else
						{
							i++;
						}
						break;
					case SqlDataSourceParameterParser.MiscParameterParser.State.InQuote:
						i++;
						while (i < length && commandText[i] != '\'')
						{
							i++;
						}
						i++;
						state = SqlDataSourceParameterParser.MiscParameterParser.State.InText;
						break;
					case SqlDataSourceParameterParser.MiscParameterParser.State.InDoubleQuote:
						i++;
						while (i < length && commandText[i] != '"')
						{
							i++;
						}
						i++;
						state = SqlDataSourceParameterParser.MiscParameterParser.State.InText;
						break;
					case SqlDataSourceParameterParser.MiscParameterParser.State.InBracket:
						i++;
						while (i < length && commandText[i] != ']')
						{
							i++;
						}
						i++;
						state = SqlDataSourceParameterParser.MiscParameterParser.State.InText;
						break;
					case SqlDataSourceParameterParser.MiscParameterParser.State.InQuestion:
						i++;
						list.Add(new Parameter("?"));
						state = SqlDataSourceParameterParser.MiscParameterParser.State.InText;
						break;
					}
				}
				return list.ToArray();
			}

			// Token: 0x020004D1 RID: 1233
			private enum State
			{
				// Token: 0x04001E4D RID: 7757
				InText,
				// Token: 0x04001E4E RID: 7758
				InQuote,
				// Token: 0x04001E4F RID: 7759
				InDoubleQuote,
				// Token: 0x04001E50 RID: 7760
				InBracket,
				// Token: 0x04001E51 RID: 7761
				InQuestion
			}
		}

		// Token: 0x020004D2 RID: 1234
		private sealed class OracleClientParameterParser : SqlDataSourceParameterParser.ParameterParser
		{
			// Token: 0x06002C70 RID: 11376 RVA: 0x000FA133 File Offset: 0x000F9133
			private static bool IsValidParamNameChar(char c)
			{
				return char.IsLetterOrDigit(c) || c == '_';
			}

			// Token: 0x06002C71 RID: 11377 RVA: 0x000FA144 File Offset: 0x000F9144
			public override Parameter[] ParseCommandText(string commandText)
			{
				int i = 0;
				int length = commandText.Length;
				SqlDataSourceParameterParser.OracleClientParameterParser.State state = SqlDataSourceParameterParser.OracleClientParameterParser.State.InText;
				List<Parameter> list = new List<Parameter>();
				StringCollection stringCollection = new StringCollection();
				while (i < length)
				{
					switch (state)
					{
					case SqlDataSourceParameterParser.OracleClientParameterParser.State.InText:
						if (commandText[i] == '\'')
						{
							state = SqlDataSourceParameterParser.OracleClientParameterParser.State.InQuote;
						}
						else if (commandText[i] == '"')
						{
							state = SqlDataSourceParameterParser.OracleClientParameterParser.State.InDoubleQuote;
						}
						else if (commandText[i] == '[')
						{
							state = SqlDataSourceParameterParser.OracleClientParameterParser.State.InBracket;
						}
						else if (commandText[i] == ':')
						{
							state = SqlDataSourceParameterParser.OracleClientParameterParser.State.InParameter;
						}
						else
						{
							i++;
						}
						break;
					case SqlDataSourceParameterParser.OracleClientParameterParser.State.InQuote:
						i++;
						while (i < length && commandText[i] != '\'')
						{
							i++;
						}
						i++;
						state = SqlDataSourceParameterParser.OracleClientParameterParser.State.InText;
						break;
					case SqlDataSourceParameterParser.OracleClientParameterParser.State.InDoubleQuote:
						i++;
						while (i < length && commandText[i] != '"')
						{
							i++;
						}
						i++;
						state = SqlDataSourceParameterParser.OracleClientParameterParser.State.InText;
						break;
					case SqlDataSourceParameterParser.OracleClientParameterParser.State.InBracket:
						i++;
						while (i < length && commandText[i] != ']')
						{
							i++;
						}
						i++;
						state = SqlDataSourceParameterParser.OracleClientParameterParser.State.InText;
						break;
					case SqlDataSourceParameterParser.OracleClientParameterParser.State.InParameter:
					{
						i++;
						string text = string.Empty;
						while (i < length && SqlDataSourceParameterParser.OracleClientParameterParser.IsValidParamNameChar(commandText[i]))
						{
							text += commandText[i];
							i++;
						}
						Parameter item = new Parameter(text);
						if (!stringCollection.Contains(text))
						{
							list.Add(item);
							stringCollection.Add(text);
						}
						state = SqlDataSourceParameterParser.OracleClientParameterParser.State.InText;
						break;
					}
					}
				}
				return list.ToArray();
			}

			// Token: 0x020004D3 RID: 1235
			private enum State
			{
				// Token: 0x04001E53 RID: 7763
				InText,
				// Token: 0x04001E54 RID: 7764
				InQuote,
				// Token: 0x04001E55 RID: 7765
				InDoubleQuote,
				// Token: 0x04001E56 RID: 7766
				InBracket,
				// Token: 0x04001E57 RID: 7767
				InParameter
			}
		}
	}
}
