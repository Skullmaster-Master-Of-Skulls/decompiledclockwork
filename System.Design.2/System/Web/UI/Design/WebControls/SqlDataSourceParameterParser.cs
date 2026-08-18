using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web.UI.WebControls;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x02000112 RID: 274
	internal static class SqlDataSourceParameterParser
	{
		// Token: 0x06000A10 RID: 2576 RVA: 0x0003EFFC File Offset: 0x0003D1FC
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
			string a = providerName.ToLowerInvariant();
			if (!(a == "system.data.sqlclient") && !(a == "system.data.sqlserverce.4.0"))
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
			if (parameterParser == null)
			{
				return new Parameter[0];
			}
			return parameterParser.ParseCommandText(commandText);
		}

		// Token: 0x02000444 RID: 1092
		private abstract class ParameterParser
		{
			// Token: 0x06002905 RID: 10501
			public abstract Parameter[] ParseCommandText(string commandText);
		}

		// Token: 0x02000445 RID: 1093
		private sealed class SqlClientParameterParser : SqlDataSourceParameterParser.ParameterParser
		{
			// Token: 0x06002907 RID: 10503 RVA: 0x000F94FC File Offset: 0x000F76FC
			private static bool IsValidParamNameChar(char c)
			{
				return char.IsLetterOrDigit(c) || c == '@' || c == '$' || c == '#' || c == '_';
			}

			// Token: 0x06002908 RID: 10504 RVA: 0x000F951C File Offset: 0x000F771C
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
							text += commandText[i].ToString();
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

			// Token: 0x020005C5 RID: 1477
			private enum State
			{
				// Token: 0x040022C9 RID: 8905
				InText,
				// Token: 0x040022CA RID: 8906
				InQuote,
				// Token: 0x040022CB RID: 8907
				InDoubleQuote,
				// Token: 0x040022CC RID: 8908
				InBracket,
				// Token: 0x040022CD RID: 8909
				InParameter
			}
		}

		// Token: 0x02000446 RID: 1094
		private sealed class MiscParameterParser : SqlDataSourceParameterParser.ParameterParser
		{
			// Token: 0x0600290A RID: 10506 RVA: 0x000F96A8 File Offset: 0x000F78A8
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

			// Token: 0x020005C6 RID: 1478
			private enum State
			{
				// Token: 0x040022CF RID: 8911
				InText,
				// Token: 0x040022D0 RID: 8912
				InQuote,
				// Token: 0x040022D1 RID: 8913
				InDoubleQuote,
				// Token: 0x040022D2 RID: 8914
				InBracket,
				// Token: 0x040022D3 RID: 8915
				InQuestion
			}
		}

		// Token: 0x02000447 RID: 1095
		private sealed class OracleClientParameterParser : SqlDataSourceParameterParser.ParameterParser
		{
			// Token: 0x0600290C RID: 10508 RVA: 0x000F97BB File Offset: 0x000F79BB
			private static bool IsValidParamNameChar(char c)
			{
				return char.IsLetterOrDigit(c) || c == '_';
			}

			// Token: 0x0600290D RID: 10509 RVA: 0x000F97CC File Offset: 0x000F79CC
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
							text += commandText[i].ToString();
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

			// Token: 0x020005C7 RID: 1479
			private enum State
			{
				// Token: 0x040022D5 RID: 8917
				InText,
				// Token: 0x040022D6 RID: 8918
				InQuote,
				// Token: 0x040022D7 RID: 8919
				InDoubleQuote,
				// Token: 0x040022D8 RID: 8920
				InBracket,
				// Token: 0x040022D9 RID: 8921
				InParameter
			}
		}
	}
}
