using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Oracle.ManagedDataAccess.Client;
using Oracle.SqlAndPlsqlParser.LocalParsing;
using OracleInternal.Common;

namespace OracleInternal.ServiceObjects
{
	// Token: 0x020001C8 RID: 456
	internal class SQLParser
	{
		// Token: 0x0600116B RID: 4459 RVA: 0x000C0314 File Offset: 0x000BE514
		private static bool SqlPreLocalBuild(string commandText, bool addRowid, out List<SqlData> sqlTokList, out List<SqlData> tableList, out uint numberOfTables, out bool distinct, out bool wildcard, out bool onlyWildcard, out bool rowIdCol, out bool parseFailed)
		{
			sqlTokList = null;
			tableList = null;
			numberOfTables = 0U;
			wildcard = false;
			onlyWildcard = false;
			distinct = false;
			rowIdCol = false;
			parseFailed = false;
			if (!SQLParser.SqlLocalParse(commandText, out rowIdCol, out wildcard, out onlyWildcard, out distinct, out numberOfTables, out tableList, out sqlTokList))
			{
				parseFailed = true;
				return false;
			}
			return (numberOfTables <= 1U || !addRowid) && !distinct;
		}

		// Token: 0x0600116C RID: 4460 RVA: 0x000C036C File Offset: 0x000BE56C
		private static bool SqlLocalParse(string commandText, out bool bFoundRowidInSql, out bool wildcard, out bool onlyWildcard, out bool distinct, out uint numberOfTables, out List<SqlData> tableList, out List<SqlData> sqlTokList)
		{
			numberOfTables = 0U;
			sqlTokList = null;
			tableList = new List<SqlData>();
			bFoundRowidInSql = (wildcard = (onlyWildcard = (distinct = false)));
			if (!SQLParser.SqlParse(commandText, out sqlTokList))
			{
				return false;
			}
			SQLParser.SqlReadSQLTokenList(sqlTokList, ref tableList, out numberOfTables, out wildcard, out onlyWildcard, out distinct, out bFoundRowidInSql);
			return true;
		}

		// Token: 0x0600116D RID: 4461 RVA: 0x000C03BC File Offset: 0x000BE5BC
		private static bool SqlParse(string sqlStmt, out List<SqlData> sqlDataList)
		{
			sqlDataList = new List<SqlData>();
			SqlMicTokTyp sqlMicTokTyp = SqlMicTokTyp.I_UNKNOWN;
			SqlMacTokTyp sqlMacTokTyp = SqlMacTokTyp.A_UNKNOWN;
			SqlMacTokTyp sqlMacTokTyp2 = SqlMacTokTyp.A_UNKNOWN;
			SqlState sqlState = SqlState.S_BEGSQL;
			bool flag = false;
			StringBuilder stringBuilder = new StringBuilder();
			int num = 0;
			int num2 = 0;
			bool flag2 = false;
			bool flag3 = false;
			string text = sqlStmt;
			int num3 = 0;
			int length = text.Length;
			while (sqlState != SqlState.S_UNKNOWN)
			{
				string empty;
				SqlMicTokTyp sqlMicTokTyp2 = SQLParser.SqlGetNextToken(text, length, out empty, ref num3);
				switch (sqlMicTokTyp2)
				{
				case SqlMicTokTyp.I_COMMA:
					if (sqlState == SqlState.S_INCOLUMN)
					{
						sqlState = SqlState.S_NEWCOLUMN;
						if (stringBuilder.Length != 0)
						{
							uint id = (uint)(flag2 ? (sqlMacTokTyp2 | (SqlMacTokTyp)536870912) : sqlMacTokTyp2);
							sqlDataList.Add(new SqlData(stringBuilder.ToString(), id, 0U));
							stringBuilder.Length = 0;
						}
						flag2 = false;
						sqlMacTokTyp = SqlMacTokTyp.A_COMMA;
						sqlDataList.Add(new SqlData(empty, (uint)sqlMacTokTyp, 0U));
					}
					else if (sqlState == SqlState.S_INTABLE)
					{
						sqlState = SqlState.S_NEWTABLE;
						if (stringBuilder.Length != 0)
						{
							sqlDataList.Add(new SqlData(stringBuilder.ToString(), (uint)sqlMacTokTyp2, 0U));
							stringBuilder.Length = 0;
						}
						sqlMacTokTyp = SqlMacTokTyp.A_COMMA;
						sqlDataList.Add(new SqlData(empty, (uint)sqlMacTokTyp, 0U));
					}
					else
					{
						sqlState = SqlState.S_UNKNOWN;
					}
					break;
				case SqlMicTokTyp.I_DOT:
				case SqlMicTokTyp.I_AT:
					if (sqlState == SqlState.S_INCOLUMN)
					{
						sqlMacTokTyp = SqlMacTokTyp.A_COLUMN;
						stringBuilder.Append(empty);
					}
					else if (sqlState == SqlState.S_INTABLE)
					{
						sqlMacTokTyp = SqlMacTokTyp.A_TABLE;
						stringBuilder.Append(empty);
					}
					else
					{
						sqlState = SqlState.S_UNKNOWN;
					}
					break;
				case SqlMicTokTyp.I_BPAREN:
					if (sqlState == SqlState.S_SELECT || sqlState == SqlState.S_NEWCOLUMN || sqlState == SqlState.S_INCOLUMN)
					{
						sqlMacTokTyp = SqlMacTokTyp.A_COLUMN;
						sqlState = SqlState.S_INCOLUMN;
						num++;
						flag2 = true;
						flag3 = false;
						stringBuilder.Append(empty);
						while (num != 0)
						{
							empty = string.Empty;
							while (num3 < length && char.IsWhiteSpace(text[num3]))
							{
								num3++;
								stringBuilder.Append(" ");
							}
							if (num3 >= length)
							{
								sqlState = SqlState.S_UNKNOWN;
								break;
							}
							SqlMicTokTyp sqlMicTokTyp3 = SQLParser.SqlGetNextToken(text, length, out empty, ref num3);
							if (sqlMicTokTyp3 == SqlMicTokTyp.I_FINITO || sqlMicTokTyp3 == SqlMicTokTyp.I_UNKNOWN || sqlMicTokTyp3 == SqlMicTokTyp.I_ERROR)
							{
								sqlState = SqlState.S_UNKNOWN;
								break;
							}
							stringBuilder.Append(empty);
							if (sqlMicTokTyp3 == SqlMicTokTyp.I_EPAREN)
							{
								num--;
							}
							else if (sqlMicTokTyp3 == SqlMicTokTyp.I_BPAREN)
							{
								num++;
							}
						}
						sqlMicTokTyp2 = SqlMicTokTyp.I_EPAREN;
						empty = string.Empty;
					}
					else
					{
						sqlState = SqlState.S_UNKNOWN;
					}
					break;
				case SqlMicTokTyp.I_COMMENT:
				case SqlMicTokTyp.I_HINT:
					if (stringBuilder.Length != 0)
					{
						sqlMacTokTyp = sqlMacTokTyp2;
						sqlDataList.Add(new SqlData(stringBuilder.ToString(), (uint)sqlMacTokTyp, 0U));
						stringBuilder.Length = 0;
					}
					sqlDataList.Add(new SqlData(empty, 13U, 0U));
					break;
				case SqlMicTokTyp.I_ASTERISK:
					if (sqlState == SqlState.S_NEWCOLUMN || sqlState == SqlState.S_SELECT)
					{
						sqlState = SqlState.S_INCOLUMN;
						sqlMacTokTyp = SqlMacTokTyp.A_COLUMN;
						sqlDataList.Add(new SqlData(empty, (uint)(sqlMacTokTyp | (SqlMacTokTyp)268435456), 0U));
					}
					else if (sqlState == SqlState.S_INCOLUMN)
					{
						if (sqlMicTokTyp == SqlMicTokTyp.I_DOT)
						{
							sqlMacTokTyp = SqlMacTokTyp.A_COLUMN;
							stringBuilder.Append(empty);
							sqlDataList.Add(new SqlData(stringBuilder.ToString(), (uint)(sqlMacTokTyp | (SqlMacTokTyp)268435456), 0U));
							stringBuilder.Length = 0;
						}
						else
						{
							sqlMacTokTyp = SqlMacTokTyp.A_COLUMN;
							sqlMicTokTyp2 = SqlMicTokTyp.I_OPER;
							flag3 = true;
							flag2 = true;
							stringBuilder.Append(empty);
						}
					}
					else
					{
						sqlState = SqlState.S_UNKNOWN;
					}
					break;
				case SqlMicTokTyp.I_NUMLIT:
				case SqlMicTokTyp.I_NCHARLIT:
				case SqlMicTokTyp.I_CHARLIT:
					if (sqlState == SqlState.S_SELECT || sqlState == SqlState.S_NEWCOLUMN)
					{
						sqlState = SqlState.S_INCOLUMN;
					}
					if (sqlState == SqlState.S_INCOLUMN)
					{
						if (stringBuilder.Length != 0)
						{
							sqlMacTokTyp = SqlMacTokTyp.A_COLUMN;
							uint id = (uint)(flag2 ? (sqlMacTokTyp | (SqlMacTokTyp)536870912) : sqlMacTokTyp);
							if (sqlMicTokTyp != SqlMicTokTyp.I_OPER && sqlMicTokTyp != SqlMicTokTyp.I_DOT)
							{
								sqlDataList.Add(new SqlData(stringBuilder.ToString(), id, 0U));
								stringBuilder.Length = 0;
							}
						}
						flag3 = false;
						sqlMacTokTyp = SqlMacTokTyp.A_COLUMN;
						flag2 = true;
						stringBuilder.Append(empty);
					}
					else
					{
						sqlState = SqlState.S_UNKNOWN;
					}
					break;
				case SqlMicTokTyp.I_VARCHAR:
				{
					SqlMacTokTyp sqlMacTokTyp3 = SQLParser.SqlGetMacroTokenType(empty);
					switch (sqlMacTokTyp3)
					{
					case SqlMacTokTyp.A_SELECT:
						if (sqlState == SqlState.S_BEGSQL)
						{
							sqlState = SqlState.S_SELECT;
							sqlMacTokTyp = SqlMacTokTyp.A_SELECT;
							sqlDataList.Add(new SqlData(empty, (uint)sqlMacTokTyp, 0U));
						}
						else
						{
							sqlState = SqlState.S_UNKNOWN;
						}
						break;
					case SqlMacTokTyp.A_ALL:
						if (stringBuilder.Length != 0)
						{
							sqlMacTokTyp = sqlMacTokTyp2;
							sqlDataList.Add(new SqlData(stringBuilder.ToString(), (uint)sqlMacTokTyp2, 0U));
							stringBuilder.Length = 0;
						}
						sqlDataList.Add(new SqlData(empty, 1U, 0U));
						break;
					case SqlMacTokTyp.A_UNIQUE:
						if (stringBuilder.Length != 0)
						{
							sqlMacTokTyp = sqlMacTokTyp2;
							sqlDataList.Add(new SqlData(stringBuilder.ToString(), (uint)sqlMacTokTyp2, 0U));
							stringBuilder.Length = 0;
						}
						sqlDataList.Add(new SqlData(empty, 2U, 0U));
						break;
					case SqlMacTokTyp.A_DISTINCT:
						if (stringBuilder.Length != 0)
						{
							sqlMacTokTyp = sqlMacTokTyp2;
							sqlDataList.Add(new SqlData(stringBuilder.ToString(), (uint)sqlMacTokTyp2, 0U));
							stringBuilder.Length = 0;
						}
						sqlDataList.Add(new SqlData(empty, 3U, 0U));
						break;
					case SqlMacTokTyp.A_AS:
						if (sqlState == SqlState.S_INCOLUMN || sqlState == SqlState.S_INTABLE)
						{
							if (stringBuilder.Length != 0)
							{
								uint id;
								if (flag2 && sqlState == SqlState.S_INCOLUMN)
								{
									id = (uint)(sqlMacTokTyp2 | (SqlMacTokTyp)536870912);
									flag2 = false;
								}
								else
								{
									id = (uint)sqlMacTokTyp2;
								}
								sqlDataList.Add(new SqlData(stringBuilder.ToString(), id, 0U));
								stringBuilder.Length = 0;
								sqlMacTokTyp = SqlMacTokTyp.A_AS;
								sqlDataList.Add(new SqlData(empty, (uint)sqlMacTokTyp, 0U));
							}
							else
							{
								sqlState = SqlState.S_UNKNOWN;
							}
						}
						else
						{
							sqlState = SqlState.S_UNKNOWN;
						}
						break;
					case SqlMacTokTyp.A_FROM:
						if (sqlState == SqlState.S_INCOLUMN)
						{
							sqlState = SqlState.S_FROM;
							if (stringBuilder.Length != 0)
							{
								uint id = (uint)(flag2 ? (sqlMacTokTyp2 | (SqlMacTokTyp)536870912) : sqlMacTokTyp2);
								sqlDataList.Add(new SqlData(stringBuilder.ToString(), id, 0U));
								stringBuilder.Length = 0;
								flag2 = false;
							}
							sqlMacTokTyp = SqlMacTokTyp.A_FROM;
							sqlDataList.Add(new SqlData(empty, (uint)sqlMacTokTyp, 0U));
						}
						else
						{
							sqlState = SqlState.S_UNKNOWN;
						}
						break;
					case SqlMacTokTyp.A_AFTERFROM:
						if (sqlState == SqlState.S_INTABLE)
						{
							sqlState = SqlState.S_ENDSQL;
							if (stringBuilder.Length != 0)
							{
								sqlDataList.Add(new SqlData(stringBuilder.ToString(), (uint)sqlMacTokTyp2, 0U));
								stringBuilder.Length = 0;
							}
							sqlMacTokTyp = SqlMacTokTyp.A_AFTERFROM;
							sqlDataList.Add(new SqlData(empty, (uint)sqlMacTokTyp, 0U));
						}
						else
						{
							sqlState = SqlState.S_UNKNOWN;
						}
						break;
					case SqlMacTokTyp.A_PSEUDOCOL:
					case SqlMacTokTyp.A_UNKNOWN:
						if (sqlMacTokTyp3 == SqlMacTokTyp.A_PSEUDOCOL)
						{
							flag2 = true;
						}
						if (sqlState == SqlState.S_SELECT || sqlState == SqlState.S_NEWCOLUMN)
						{
							sqlState = SqlState.S_INCOLUMN;
							sqlMacTokTyp = SqlMacTokTyp.A_COLUMN;
							stringBuilder.Append(empty);
						}
						else if (sqlState == SqlState.S_INCOLUMN)
						{
							if (sqlMacTokTyp2 == SqlMacTokTyp.A_AS)
							{
								sqlMacTokTyp = SqlMacTokTyp.A_COLALIAS;
								stringBuilder.Append(empty);
							}
							else if (flag3)
							{
								sqlMacTokTyp = SqlMacTokTyp.A_COLUMN;
								flag3 = false;
								stringBuilder.Append(empty);
							}
							else if (sqlMacTokTyp2 == SqlMacTokTyp.A_CASE)
							{
								sqlMacTokTyp = SqlMacTokTyp.A_COLUMN;
								sqlDataList.Add(new SqlData(empty, 6U, 0U));
								empty = string.Empty;
							}
							else if (sqlMicTokTyp != SqlMicTokTyp.I_OPER && sqlMicTokTyp != SqlMicTokTyp.I_DOT && sqlMicTokTyp != SqlMicTokTyp.I_AT)
							{
								if (stringBuilder.Length != 0)
								{
									sqlMacTokTyp = SqlMacTokTyp.A_COLUMN;
									uint id = (uint)(flag2 ? (sqlMacTokTyp | (SqlMacTokTyp)536870912) : sqlMacTokTyp);
									sqlDataList.Add(new SqlData(stringBuilder.ToString(), id, 0U));
									stringBuilder.Length = 0;
									flag2 = false;
								}
								sqlMacTokTyp = SqlMacTokTyp.A_COLALIAS;
								stringBuilder.Append(empty);
							}
							else
							{
								sqlState = SqlState.S_INCOLUMN;
								sqlMacTokTyp = sqlMacTokTyp2;
								stringBuilder.Append(empty);
							}
						}
						else if (sqlState == SqlState.S_FROM || sqlState == SqlState.S_NEWTABLE)
						{
							sqlState = SqlState.S_INTABLE;
							sqlMacTokTyp = SqlMacTokTyp.A_TABLE;
							stringBuilder.Append(empty);
						}
						else if (sqlState == SqlState.S_INTABLE)
						{
							if (sqlMacTokTyp2 == SqlMacTokTyp.A_AS)
							{
								sqlMacTokTyp = SqlMacTokTyp.A_TABALIAS;
								stringBuilder.Append(empty);
							}
							else if (sqlMicTokTyp == SqlMicTokTyp.I_VARCHAR)
							{
								if (stringBuilder.Length != 0)
								{
									sqlMacTokTyp = SqlMacTokTyp.A_TABLE;
									sqlDataList.Add(new SqlData(stringBuilder.ToString(), (uint)sqlMacTokTyp, 0U));
									stringBuilder.Length = 0;
								}
								sqlMacTokTyp = SqlMacTokTyp.A_TABALIAS;
								stringBuilder.Append(empty);
							}
							else
							{
								sqlState = SqlState.S_INTABLE;
								sqlMacTokTyp = sqlMacTokTyp2;
								stringBuilder.Append(empty);
							}
						}
						break;
					case SqlMacTokTyp.A_CASE:
						if (sqlState == SqlState.S_SELECT || sqlState == SqlState.S_NEWCOLUMN || sqlState == SqlState.S_INCOLUMN)
						{
							stringBuilder.Append(empty);
							string text2 = text;
							int num4 = num3;
							int length2 = text2.Length;
							while (num4 < length2 && char.IsWhiteSpace(text2[num4]))
							{
								num4++;
								stringBuilder.Append(" ");
							}
							string empty2 = string.Empty;
							SqlMicTokTyp sqlMicTokTyp4 = SQLParser.SqlGetNextToken(text2, length2, out empty2, ref num4);
							if (sqlMicTokTyp4 != SqlMicTokTyp.I_EPAREN && sqlMicTokTyp4 != SqlMicTokTyp.I_CHARLIT && sqlMicTokTyp4 != SqlMicTokTyp.I_BPAREN)
							{
								while (string.Compare(empty2, "when", true) != 0)
								{
									stringBuilder.Append(empty2);
									while (num4 < length2 && char.IsWhiteSpace(text2[num4]))
									{
										num4++;
										stringBuilder.Append(" ");
									}
									sqlMicTokTyp4 = SQLParser.SqlGetNextToken(text2, length2, out empty2, ref num4);
									if (sqlMicTokTyp4 == SqlMicTokTyp.I_FINITO || sqlMicTokTyp4 == SqlMicTokTyp.I_UNKNOWN || sqlMicTokTyp4 == SqlMicTokTyp.I_ERROR)
									{
										sqlState = SqlState.S_UNKNOWN;
										break;
									}
								}
							}
							if (string.Compare(empty2, "when", true) == 0 && sqlMicTokTyp4 != SqlMicTokTyp.I_EPAREN && sqlMicTokTyp4 != SqlMicTokTyp.I_CHARLIT && sqlMicTokTyp4 != SqlMicTokTyp.I_BPAREN)
							{
								num2++;
								stringBuilder.Append(empty2);
								while (num2 != 0)
								{
									while (num4 < length2 && char.IsWhiteSpace(text2[num4]))
									{
										num4++;
										stringBuilder.Append(" ");
									}
									empty2 = string.Empty;
									sqlMicTokTyp4 = SQLParser.SqlGetNextToken(text2, length2, out empty2, ref num4);
									if (sqlMicTokTyp4 == SqlMicTokTyp.I_FINITO || sqlMicTokTyp4 == SqlMicTokTyp.I_UNKNOWN || sqlMicTokTyp4 == SqlMicTokTyp.I_ERROR)
									{
										break;
									}
									stringBuilder.Append(empty2);
									if (string.Compare(empty2, "end", true) == 0)
									{
										num2--;
									}
									else if (string.Compare(empty2, "case", true) == 0)
									{
										num2++;
									}
								}
								sqlDataList.Add(new SqlData(stringBuilder.ToString(), 15U, 0U));
								sqlMacTokTyp = SqlMacTokTyp.A_CASE;
								sqlState = SqlState.S_INCOLUMN;
								flag2 = true;
								flag3 = false;
								stringBuilder.Length = 0;
								empty = string.Empty;
								empty2 = string.Empty;
								text = text2;
								flag = true;
							}
							else
							{
								stringBuilder.Length = 0;
								flag = false;
							}
							num3 = num4;
						}
						if (flag)
						{
							flag = false;
						}
						break;
					}
					break;
				}
				case SqlMicTokTyp.I_OPER:
					if (sqlState == SqlState.S_INCOLUMN)
					{
						flag3 = true;
						sqlMacTokTyp = SqlMacTokTyp.A_COLUMN;
						flag2 = true;
						stringBuilder.Append(empty);
					}
					else
					{
						sqlState = SqlState.S_UNKNOWN;
					}
					break;
				case SqlMicTokTyp.I_FINITO:
					if (sqlState == SqlState.S_INTABLE)
					{
						if (stringBuilder.Length != 0)
						{
							sqlDataList.Add(new SqlData(stringBuilder.ToString(), (uint)sqlMacTokTyp2, 0U));
							stringBuilder.Length = 0;
						}
					}
					else
					{
						sqlState = SqlState.S_UNKNOWN;
					}
					break;
				}
				sqlMicTokTyp = sqlMicTokTyp2;
				sqlMacTokTyp2 = sqlMacTokTyp;
				empty = string.Empty;
				if (sqlState == SqlState.S_ENDSQL || sqlMicTokTyp2 == SqlMicTokTyp.I_FINITO)
				{
					if (stringBuilder.Length != 0)
					{
						sqlDataList.Add(new SqlData(stringBuilder.ToString(), 13U, 0U));
						stringBuilder.Length = 0;
					}
					if (num3 < length)
					{
						sqlDataList.Add(new SqlData(text.Substring(num3), 13U, 0U));
						break;
					}
					break;
				}
			}
			return sqlState != SqlState.S_UNKNOWN;
		}

		// Token: 0x0600116E RID: 4462 RVA: 0x000C0DF4 File Offset: 0x000BEFF4
		private static void SqlReadSQLTokenList(List<SqlData> tokenList, ref List<SqlData> tableList, out uint numberOfTables, out bool wildCard, out bool onlyWildCard, out bool distinct, out bool bFoundRowidInSql)
		{
			tableList = new List<SqlData>();
			numberOfTables = 0U;
			wildCard = false;
			onlyWildCard = true;
			distinct = false;
			bFoundRowidInSql = false;
			foreach (SqlData sqlData in tokenList)
			{
				if (!bFoundRowidInSql)
				{
					if (string.Compare(sqlData.m_data, "rowid", true) == 0)
					{
						bFoundRowidInSql = true;
					}
					else
					{
						int num = sqlData.m_data.LastIndexOf(".rowid", StringComparison.InvariantCultureIgnoreCase);
						if (num != -1 && num == sqlData.m_data.Length - 6)
						{
							bFoundRowidInSql = true;
						}
					}
				}
				uint num2 = sqlData.m_id & 268435455U;
				uint num3 = sqlData.m_id & 4026531840U;
				if (num2 == 8U)
				{
					tableList.Add(new SqlData(sqlData.m_data, numberOfTables += 1U, 8U));
				}
				else if (num2 == 9U)
				{
					tableList.Add(new SqlData(sqlData.m_data, numberOfTables, 9U));
				}
				else if (num2 == 6U)
				{
					if (num3 == 268435456U)
					{
						wildCard = true;
					}
					else
					{
						onlyWildCard = false;
					}
				}
				else if (num2 == 3U || num2 == 2U)
				{
					distinct = true;
				}
			}
		}

		// Token: 0x0600116F RID: 4463 RVA: 0x000C0F24 File Offset: 0x000BF124
		private static SqlMicTokTyp SqlGetNextToken(string sqlString, int sqlStringLen, out string tokenString, ref int pos)
		{
			if (pos >= sqlStringLen)
			{
				tokenString = string.Empty;
				return SqlMicTokTyp.I_FINITO;
			}
			while (char.IsWhiteSpace(sqlString[pos]))
			{
				pos++;
				if (pos >= sqlStringLen)
				{
					tokenString = string.Empty;
					return SqlMicTokTyp.I_FINITO;
				}
			}
			StringBuilder stringBuilder = new StringBuilder();
			char c = sqlString[pos];
			SqlMicTokTyp sqlMicTokTyp;
			if (c <= '@')
			{
				switch (c)
				{
				case '!':
					if (pos + 1 < sqlStringLen && sqlString[pos + 1] == '=')
					{
						sqlMicTokTyp = SqlMicTokTyp.I_OPER;
						stringBuilder.Append(sqlString[pos++]);
						stringBuilder.Append(sqlString[pos++]);
						goto IL_9E1;
					}
					sqlMicTokTyp = SqlMicTokTyp.I_UNKNOWN;
					stringBuilder.Append(sqlString[pos++]);
					goto IL_9E1;
				case '"':
					sqlMicTokTyp = SqlMicTokTyp.I_VARCHAR;
					stringBuilder.Append(sqlString[pos++]);
					while (pos < sqlStringLen)
					{
						if (sqlString[pos] == '"')
						{
							stringBuilder.Append(sqlString[pos++]);
							goto IL_9E1;
						}
						stringBuilder.Append(sqlString[pos++]);
					}
					sqlMicTokTyp = SqlMicTokTyp.I_UNKNOWN;
					goto IL_9E1;
				case '#':
				case '$':
				case '%':
				case '&':
					goto IL_804;
				case '\'':
					sqlMicTokTyp = SqlMicTokTyp.I_CHARLIT;
					stringBuilder.Append(sqlString[pos++]);
					while (pos < sqlStringLen)
					{
						if (sqlString[pos] == '\'')
						{
							if (pos + 1 >= sqlStringLen || sqlString[pos + 1] != '\'')
							{
								stringBuilder.Append(sqlString[pos++]);
								goto IL_9E1;
							}
							stringBuilder.Append(sqlString[pos++]);
							stringBuilder.Append(sqlString[pos++]);
						}
						else
						{
							stringBuilder.Append(sqlString[pos++]);
						}
					}
					sqlMicTokTyp = SqlMicTokTyp.I_UNKNOWN;
					goto IL_9E1;
				case '(':
					sqlMicTokTyp = SqlMicTokTyp.I_BPAREN;
					stringBuilder.Append(sqlString[pos++]);
					goto IL_9E1;
				case ')':
					sqlMicTokTyp = SqlMicTokTyp.I_EPAREN;
					stringBuilder.Append(sqlString[pos++]);
					goto IL_9E1;
				case '*':
					sqlMicTokTyp = SqlMicTokTyp.I_ASTERISK;
					stringBuilder.Append(sqlString[pos++]);
					goto IL_9E1;
				case '+':
					break;
				case ',':
					sqlMicTokTyp = SqlMicTokTyp.I_COMMA;
					stringBuilder.Append(sqlString[pos++]);
					goto IL_9E1;
				case '-':
					if (pos + 1 < sqlStringLen && sqlString[pos + 1] == '-')
					{
						if (pos + 2 < sqlStringLen && sqlString[pos + 2] == '+')
						{
							sqlMicTokTyp = SqlMicTokTyp.I_HINT;
							stringBuilder.Append(sqlString[pos++]);
							stringBuilder.Append(sqlString[pos++]);
							stringBuilder.Append(sqlString[pos++]);
						}
						else
						{
							sqlMicTokTyp = SqlMicTokTyp.I_COMMENT;
							stringBuilder.Append(sqlString[pos++]);
							stringBuilder.Append(sqlString[pos++]);
						}
					}
					else
					{
						sqlMicTokTyp = SqlMicTokTyp.I_OPER;
						stringBuilder.Append(sqlString[pos++]);
					}
					if (sqlMicTokTyp == SqlMicTokTyp.I_HINT || sqlMicTokTyp == SqlMicTokTyp.I_COMMENT)
					{
						while (pos < sqlStringLen)
						{
							if (sqlString[pos] == '\n')
							{
								stringBuilder.Append(sqlString[pos]);
								goto IL_9E1;
							}
							stringBuilder.Append(sqlString[pos++]);
						}
						sqlMicTokTyp = SqlMicTokTyp.I_UNKNOWN;
						goto IL_9E1;
					}
					goto IL_9E1;
				case '.':
					sqlMicTokTyp = SqlMicTokTyp.I_DOT;
					stringBuilder.Append(sqlString[pos++]);
					goto IL_9E1;
				case '/':
					if (pos + 1 < sqlStringLen && sqlString[pos + 1] == '*')
					{
						if (pos + 2 < sqlStringLen && sqlString[pos + 2] == '+')
						{
							sqlMicTokTyp = SqlMicTokTyp.I_HINT;
							stringBuilder.Append(sqlString[pos++]);
							stringBuilder.Append(sqlString[pos++]);
							stringBuilder.Append(sqlString[pos++]);
						}
						else
						{
							sqlMicTokTyp = SqlMicTokTyp.I_COMMENT;
							stringBuilder.Append(sqlString[pos++]);
							stringBuilder.Append(sqlString[pos++]);
						}
					}
					else
					{
						sqlMicTokTyp = SqlMicTokTyp.I_OPER;
						stringBuilder.Append(sqlString[pos++]);
					}
					if (sqlMicTokTyp == SqlMicTokTyp.I_HINT || sqlMicTokTyp == SqlMicTokTyp.I_COMMENT)
					{
						while (pos < sqlStringLen)
						{
							if (pos + 1 < sqlStringLen && sqlString[pos] == '*' && sqlString[pos + 1] == '/')
							{
								stringBuilder.Append(sqlString[pos++]);
								stringBuilder.Append(sqlString[pos++]);
								goto IL_9E1;
							}
							stringBuilder.Append(sqlString[pos++]);
						}
						sqlMicTokTyp = SqlMicTokTyp.I_UNKNOWN;
						goto IL_9E1;
					}
					goto IL_9E1;
				default:
					switch (c)
					{
					case '<':
					case '>':
						if (pos + 1 < sqlStringLen && sqlString[pos + 1] == '=')
						{
							sqlMicTokTyp = SqlMicTokTyp.I_OPER;
							stringBuilder.Append(sqlString[pos++]);
							stringBuilder.Append(sqlString[pos++]);
							goto IL_9E1;
						}
						sqlMicTokTyp = SqlMicTokTyp.I_OPER;
						stringBuilder.Append(sqlString[pos++]);
						goto IL_9E1;
					case '=':
						break;
					case '?':
						goto IL_804;
					case '@':
						sqlMicTokTyp = SqlMicTokTyp.I_AT;
						stringBuilder.Append(sqlString[pos++]);
						goto IL_9E1;
					default:
						goto IL_804;
					}
					break;
				}
				sqlMicTokTyp = SqlMicTokTyp.I_OPER;
				stringBuilder.Append(sqlString[pos++]);
				goto IL_9E1;
			}
			if (c != 'N')
			{
				if (c == '|')
				{
					if (pos + 1 < sqlStringLen && sqlString[pos + 1] == '|')
					{
						sqlMicTokTyp = SqlMicTokTyp.I_OPER;
						stringBuilder.Append(sqlString[pos++]);
						stringBuilder.Append(sqlString[pos++]);
						goto IL_9E1;
					}
					sqlMicTokTyp = SqlMicTokTyp.I_UNKNOWN;
					stringBuilder.Append(sqlString[pos++]);
					goto IL_9E1;
				}
			}
			else
			{
				if (pos + 1 < sqlStringLen && sqlString[pos + 1] == '\'')
				{
					sqlMicTokTyp = SqlMicTokTyp.I_NCHARLIT;
					stringBuilder.Append(sqlString[pos++]);
					stringBuilder.Append(sqlString[pos++]);
					while (pos < sqlStringLen)
					{
						if (sqlString[pos] == '\'')
						{
							if (pos + 1 >= sqlStringLen || sqlString[pos + 1] != '\'')
							{
								stringBuilder.Append(sqlString[pos++]);
								goto IL_9E1;
							}
							stringBuilder.Append(sqlString[pos++]);
							stringBuilder.Append(sqlString[pos++]);
						}
						else
						{
							stringBuilder.Append(sqlString[pos++]);
						}
					}
					sqlMicTokTyp = SqlMicTokTyp.I_UNKNOWN;
					goto IL_9E1;
				}
				sqlMicTokTyp = SqlMicTokTyp.I_VARCHAR;
				stringBuilder.Append(sqlString[pos++]);
				while (pos < sqlStringLen)
				{
					if (!char.IsLetterOrDigit(sqlString[pos]) && sqlString[pos] != '_' && sqlString[pos] != '＿' && sqlString[pos] != '$' && sqlString[pos] != '＄' && sqlString[pos] != '#' && sqlString[pos] != '＃')
					{
						break;
					}
					stringBuilder.Append(sqlString[pos++]);
				}
				goto IL_9E1;
			}
			IL_804:
			if (char.IsDigit(sqlString[pos]))
			{
				sqlMicTokTyp = SqlMicTokTyp.I_NUMLIT;
				stringBuilder.Append(sqlString[pos++]);
				while (pos < sqlStringLen && (char.IsDigit(sqlString[pos]) || sqlString[pos] == '.'))
				{
					stringBuilder.Append(sqlString[pos++]);
				}
				if (pos < sqlStringLen && (sqlString[pos] == 'e' || sqlString[pos] == 'E'))
				{
					stringBuilder.Append(sqlString[pos++]);
					if (pos < sqlStringLen && (sqlString[pos] == '+' || sqlString[pos] == '-'))
					{
						stringBuilder.Append(sqlString[pos++]);
					}
					while (pos < sqlStringLen)
					{
						if (!char.IsDigit(sqlString[pos]))
						{
							break;
						}
						stringBuilder.Append(sqlString[pos++]);
					}
				}
			}
			else if (char.IsLetter(sqlString[pos]))
			{
				sqlMicTokTyp = SqlMicTokTyp.I_VARCHAR;
				stringBuilder.Append(sqlString[pos++]);
				while (pos < sqlStringLen)
				{
					if (!char.IsLetterOrDigit(sqlString[pos]) && sqlString[pos] != '_' && sqlString[pos] != '＿' && sqlString[pos] != '$' && sqlString[pos] != '＄' && sqlString[pos] != '#' && sqlString[pos] != '＃')
					{
						break;
					}
					stringBuilder.Append(sqlString[pos++]);
				}
			}
			else
			{
				sqlMicTokTyp = SqlMicTokTyp.I_UNKNOWN;
				stringBuilder.Append(sqlString[pos++]);
			}
			IL_9E1:
			tokenString = stringBuilder.ToString();
			return sqlMicTokTyp;
		}

		// Token: 0x06001170 RID: 4464 RVA: 0x000C191C File Offset: 0x000BFB1C
		private static SqlMacTokTyp SqlGetMacroTokenType(string token)
		{
			SqlMacTokTyp result;
			if (string.Compare(token, "select", true) == 0)
			{
				result = SqlMacTokTyp.A_SELECT;
			}
			else if (string.Compare(token, "as", true) == 0)
			{
				result = SqlMacTokTyp.A_AS;
			}
			else if (string.Compare(token, "all", true) == 0)
			{
				result = SqlMacTokTyp.A_ALL;
			}
			else if (string.Compare(token, "distinct", true) == 0)
			{
				result = SqlMacTokTyp.A_DISTINCT;
			}
			else if (string.Compare(token, "unique", true) == 0)
			{
				result = SqlMacTokTyp.A_UNIQUE;
			}
			else if (string.Compare(token, "from", true) == 0)
			{
				result = SqlMacTokTyp.A_FROM;
			}
			else if (string.Compare(token, "where", true) == 0)
			{
				result = SqlMacTokTyp.A_AFTERFROM;
			}
			else if (string.Compare(token, "order", true) == 0)
			{
				result = SqlMacTokTyp.A_AFTERFROM;
			}
			else if (string.Compare(token, "start", true) == 0)
			{
				result = SqlMacTokTyp.A_AFTERFROM;
			}
			else if (string.Compare(token, "connect", true) == 0)
			{
				result = SqlMacTokTyp.A_AFTERFROM;
			}
			else if (string.Compare(token, "group", true) == 0)
			{
				result = SqlMacTokTyp.A_AFTERFROM;
			}
			else if (string.Compare(token, "with", true) == 0)
			{
				result = SqlMacTokTyp.A_AFTERFROM;
			}
			else if (string.Compare(token, "union", true) == 0)
			{
				result = SqlMacTokTyp.A_AFTERFROM;
			}
			else if (string.Compare(token, "intersect", true) == 0)
			{
				result = SqlMacTokTyp.A_AFTERFROM;
			}
			else if (string.Compare(token, "minus", true) == 0)
			{
				result = SqlMacTokTyp.A_AFTERFROM;
			}
			else if (string.Compare(token, "table", true) == 0)
			{
				result = SqlMacTokTyp.A_AFTERFROM;
			}
			else if (string.Compare(token, "for", true) == 0)
			{
				result = SqlMacTokTyp.A_AFTERFROM;
			}
			else if (string.Compare(token, "user", true) == 0)
			{
				result = SqlMacTokTyp.A_PSEUDOCOL;
			}
			else if (string.Compare(token, "uid", true) == 0)
			{
				result = SqlMacTokTyp.A_PSEUDOCOL;
			}
			else if (string.Compare(token, "sysdate", true) == 0)
			{
				result = SqlMacTokTyp.A_PSEUDOCOL;
			}
			else if (string.Compare(token, "rowid", true) == 0)
			{
				result = SqlMacTokTyp.A_PSEUDOCOL;
			}
			else if (string.Compare(token, "rownum", true) == 0)
			{
				result = SqlMacTokTyp.A_PSEUDOCOL;
			}
			else if (string.Compare(token, "level", true) == 0)
			{
				result = SqlMacTokTyp.A_PSEUDOCOL;
			}
			else if (string.Compare(token, "currval", true) == 0)
			{
				result = SqlMacTokTyp.A_PSEUDOCOL;
			}
			else if (string.Compare(token, "nextval", true) == 0)
			{
				result = SqlMacTokTyp.A_PSEUDOCOL;
			}
			else if (string.Compare(token, "case", true) == 0)
			{
				result = SqlMacTokTyp.A_CASE;
			}
			else
			{
				result = SqlMacTokTyp.A_UNKNOWN;
			}
			return result;
		}

		// Token: 0x06001171 RID: 4465 RVA: 0x000C1B54 File Offset: 0x000BFD54
		internal static bool DoSqlLocalProcessing(ref string commandText, bool addRowid, out bool bFoundRowidInSql, OracleConnectionImpl connImpl, OracleConnection conn)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			bool result2;
			try
			{
				bFoundRowidInSql = false;
				string cmdText = commandText;
				if (connImpl != null && connImpl.m_pm != null)
				{
					bool result = false;
					if (connImpl.m_pm.TryGetSqlWithRowId(ref commandText, out bFoundRowidInSql, out result))
					{
						return result;
					}
				}
				OracleCommandImpl.TrimCommentsFromSQL(ref cmdText);
				bool? flag = null;
				if (OracleCommandImpl.GetSqlStatementType(cmdText, ref flag) != SqlStatementType.SELECT)
				{
					result2 = false;
				}
				else
				{
					List<SqlData> sqlTokList = null;
					List<SqlData> tabList = null;
					uint num = 0U;
					bool flag2 = false;
					bool bOnlyWildcard = false;
					bool flag3 = false;
					string cmdText2 = commandText;
					bool flag4 = false;
					try
					{
						if (!SQLParser.SqlPreLocalBuild(commandText, addRowid, out sqlTokList, out tabList, out num, out flag3, out flag2, out bOnlyWildcard, out bFoundRowidInSql, out flag4))
						{
							result2 = false;
						}
						else if (addRowid)
						{
							if (bFoundRowidInSql || num > 1U)
							{
								result2 = false;
							}
							else
							{
								Dictionary<string, List<SqlData>> dictionary = null;
								if (!flag4)
								{
									string text;
									if (!SQLParser.SqlRebuildSQL(sqlTokList, tabList, out text, num, bOnlyWildcard, addRowid, connImpl, conn, ref dictionary))
									{
										return false;
									}
									commandText = text;
								}
								result2 = true;
							}
						}
						else
						{
							result2 = false;
						}
					}
					finally
					{
						if (connImpl != null && connImpl.m_pm != null)
						{
							connImpl.m_pm.CacheSqlWithRowIdInfo(cmdText2, commandText, bFoundRowidInSql);
						}
					}
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Exit, new string[0]);
				}
			}
			return result2;
		}

		// Token: 0x06001172 RID: 4466 RVA: 0x000C1CB4 File Offset: 0x000BFEB4
		internal static bool GetSchemaMetaData(SQLMetaData sqlMetInfo, OracleConnection conn, OracleConnectionImpl connImpl, bool metadataHasImplicitROWIDColumn)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			bool result;
			try
			{
				if (sqlMetInfo == null)
				{
					result = false;
				}
				else if (sqlMetInfo.bStmtParsed)
				{
					result = true;
				}
				else if (string.IsNullOrWhiteSpace(sqlMetInfo.pCommandText))
				{
					if (sqlMetInfo.m_sqlMetaInfo == SQLLocalParsePrimaryKeyInfo.Null || sqlMetInfo.m_sqlMetaInfo == null)
					{
						if (sqlMetInfo.m_noOfColumns > 0)
						{
							sqlMetInfo.m_sqlMetaInfo = new SQLLocalParsePrimaryKeyInfo((int)sqlMetInfo.m_noOfColumns);
						}
						else
						{
							sqlMetInfo.m_sqlMetaInfo = SQLLocalParsePrimaryKeyInfo.Null;
						}
					}
					result = false;
				}
				else
				{
					lock (sqlMetInfo.m_syncLP)
					{
						if (sqlMetInfo.bStmtParsed)
						{
							return true;
						}
						if (connImpl.m_cs.m_metadataPooling && connImpl.m_pm.TryRetrieveLocalParseInfoFromCache(connImpl.ServiceName, sqlMetInfo.pCommandText, ref sqlMetInfo))
						{
							return true;
						}
						if (sqlMetInfo.m_sqlMetaInfo == SQLLocalParsePrimaryKeyInfo.Null || sqlMetInfo.m_sqlMetaInfo == null)
						{
							if (sqlMetInfo.m_noOfColumns > 0)
							{
								sqlMetInfo.m_sqlMetaInfo = new SQLLocalParsePrimaryKeyInfo((int)sqlMetInfo.m_noOfColumns);
							}
							else
							{
								sqlMetInfo.m_sqlMetaInfo = SQLLocalParsePrimaryKeyInfo.Null;
							}
						}
						bool flag2 = false;
						if (!ConfigBaseClass.m_bUseLegacyLocalParser && !flag2)
						{
							try
							{
								if (sqlMetInfo.parsedStmt == null)
								{
									string text = sqlMetInfo.pCommandText;
									text = text.TrimEnd(new char[0]);
									if (!text.EndsWith(";"))
									{
										text += ";";
									}
									try
									{
										sqlMetInfo.parsedStmt = OracleConnection.OracleLpParser.ParseStatements(conn, text);
									}
									catch (Exception ex)
									{
										if (ProviderConfig.m_bTraceLevelPublic)
										{
											string text2 = text.Replace(SQLParser.s_replaceString, string.Empty);
											string text3 = ex.ToString().Replace(SQLParser.s_replaceString, string.Empty);
											Trace.Write(OracleTraceLevel.Public, OracleTraceTag.SQL, new string[]
											{
												string.Concat(new string[]
												{
													"(LOCALPARSER) (ERROR:",
													text3,
													") (SQL:",
													text2,
													")"
												})
											});
										}
									}
								}
								if (sqlMetInfo.parsedStmt == null)
								{
									if (ProviderConfig.m_bTraceLevelPrivate)
									{
										Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Error, new string[]
										{
											"OracleLpParser.ParseStatements() returned null for " + sqlMetInfo.pCommandText
										});
									}
									throw new NotSupportedException("OracleLpParser.ParseStatements() returned null for " + sqlMetInfo.pCommandText);
								}
								int num = 0;
								foreach (OracleLpStatement oracleLpStatement in sqlMetInfo.parsedStmt)
								{
									num++;
									if (oracleLpStatement.m_vODPContext == null)
									{
										oracleLpStatement.m_vODPContext = conn;
									}
									int num2 = sqlMetInfo.m_columnDescribeInfo.Length;
									List<OracleLpColumnDescriptor> list = new List<OracleLpColumnDescriptor>();
									foreach (OracleLpColumnDescriptor item in ((OracleLpSelectStatement)oracleLpStatement).ColumnDescriptors)
									{
										list.Add(item);
									}
									if (list.Count != num2)
									{
										if (ProviderConfig.m_bTraceLevelPrivate)
										{
											Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Error, new string[]
											{
												"Number of Column Descriptors returned by New Parser is less than number of column obtained from execution at DB do not match for " + sqlMetInfo.pCommandText
											});
											Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Error, new string[]
											{
												string.Concat(new object[]
												{
													"colDescriptors.Count: ",
													list.Count,
													", descInfoLength: ",
													num2
												})
											});
										}
										throw new Exception("Number of Column Descriptors returned by New Parser is less than number of column obtained from execution at DB do not match for " + sqlMetInfo.pCommandText);
									}
									uint num3 = 0U;
									while ((ulong)num3 < (ulong)((long)num2))
									{
										ColumnDescribeInfo columnDescribeInfo = sqlMetInfo.m_columnDescribeInfo[(int)((UIntPtr)num3)];
										int count = list.Count;
										int i = 0;
										OracleLpColumnDescriptor oracleLpColumnDescriptor = null;
										while (i < count)
										{
											if (list[i].ColumnName.DbName == columnDescribeInfo.pColAlias)
											{
												oracleLpColumnDescriptor = list[i];
												list.RemoveAt(i);
												break;
											}
											i++;
										}
										if (oracleLpColumnDescriptor == null)
										{
											if (ProviderConfig.m_bTraceLevelPrivate)
											{
												Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Error, new string[]
												{
													"ColumnName's returned by New Parser and column name obtained from execution at DB do not match for " + sqlMetInfo.pCommandText
												});
												Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Error, new string[]
												{
													"Column name from server: " + columnDescribeInfo.pColAlias
												});
											}
											throw new Exception("ColumnName's returned by New Parser and column name obtained from execution at DB do not match for " + sqlMetInfo.pCommandText);
										}
										if ((oracleLpColumnDescriptor.BaseTableName == null || string.IsNullOrEmpty(oracleLpColumnDescriptor.BaseTableName.DbName)) && oracleLpColumnDescriptor.ColumnName != null && !string.IsNullOrEmpty(oracleLpColumnDescriptor.ColumnName.DbName))
										{
											oracleLpColumnDescriptor.ColumnName.DbName = oracleLpColumnDescriptor.ColumnName.DbName.Replace(" ", string.Empty);
										}
										if (oracleLpColumnDescriptor.ColumnName != null && oracleLpColumnDescriptor.ColumnName.DbName == columnDescribeInfo.pColAlias)
										{
											ColumnLocalParsePrimaryKeyInfo columnLocalParsePrimaryKeyInfo = sqlMetInfo.m_sqlMetaInfo.m_columnMetaInfo[(int)((UIntPtr)num3)];
											columnLocalParsePrimaryKeyInfo.Updatable = true;
											columnLocalParsePrimaryKeyInfo.bIsExpression = false;
											if (oracleLpColumnDescriptor.BaseColumnName != null)
											{
												columnLocalParsePrimaryKeyInfo.m_columnName = oracleLpColumnDescriptor.BaseColumnName.DbName;
											}
											if (oracleLpColumnDescriptor.BaseSchemaName != null)
											{
												columnLocalParsePrimaryKeyInfo.m_schemaName = oracleLpColumnDescriptor.BaseSchemaName.DbName;
											}
											if (oracleLpColumnDescriptor.BaseTableName != null)
											{
												columnLocalParsePrimaryKeyInfo.pTabName = oracleLpColumnDescriptor.BaseTableName.DbName;
												columnLocalParsePrimaryKeyInfo.Updatable = true;
												columnLocalParsePrimaryKeyInfo.bIsExpression = false;
											}
											else if (oracleLpColumnDescriptor.BaseTableName == null)
											{
												columnLocalParsePrimaryKeyInfo.Updatable = false;
												columnLocalParsePrimaryKeyInfo.bIsExpression = true;
												columnLocalParsePrimaryKeyInfo.m_columnName = null;
											}
											if (oracleLpColumnDescriptor.IsRowID)
											{
												columnLocalParsePrimaryKeyInfo.m_columnName = "ROWID";
												columnLocalParsePrimaryKeyInfo.Updatable = false;
												columnLocalParsePrimaryKeyInfo.bIsExpression = true;
											}
											columnLocalParsePrimaryKeyInfo.bIsHidden = oracleLpColumnDescriptor.IsHidden;
										}
										num3 += 1U;
									}
								}
								using (IEnumerator<OracleLpStatement> enumerator3 = sqlMetInfo.parsedStmt.GetEnumerator())
								{
									if (enumerator3.MoveNext())
									{
										OracleLpStatement oracleLpStatement2 = enumerator3.Current;
										if (oracleLpStatement2.NamedObjectsReferences.Count == 1)
										{
											if (oracleLpStatement2.NamedObjectsReferences[0].ObjectName != null)
											{
												sqlMetInfo.m_sqlMetaInfo.m_tableName = oracleLpStatement2.NamedObjectsReferences[0].ObjectName.DbName;
											}
											if (oracleLpStatement2.NamedObjectsReferences[0].SchemaName != null)
											{
												sqlMetInfo.m_sqlMetaInfo.m_schemaName = oracleLpStatement2.NamedObjectsReferences[0].SchemaName.DbName;
											}
											else if (oracleLpStatement2.NamedObjectsReferences[0].ColumnDescriptors != null && oracleLpStatement2.NamedObjectsReferences[0].ColumnDescriptors.Count != 0)
											{
												foreach (OracleLpColumnDescriptor oracleLpColumnDescriptor2 in oracleLpStatement2.NamedObjectsReferences[0].ColumnDescriptors)
												{
													if (sqlMetInfo.m_sqlMetaInfo.m_tableName == oracleLpColumnDescriptor2.BaseTableName.DbName && oracleLpColumnDescriptor2.BaseSchemaName != null && !string.IsNullOrEmpty(oracleLpColumnDescriptor2.BaseSchemaName.DbName))
													{
														sqlMetInfo.m_sqlMetaInfo.m_schemaName = oracleLpColumnDescriptor2.BaseSchemaName.DbName;
														break;
													}
												}
											}
										}
										oracleLpStatement2.m_vODPContext = null;
									}
								}
							}
							catch (Exception ex2)
							{
								flag2 = true;
								if (ProviderConfig.m_bTraceLevelPrivate)
								{
									Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Error, new string[]
									{
										"Error while parsing using Adrian Parser: " + ex2.ToString()
									});
									if (sqlMetInfo != null)
									{
										if (sqlMetInfo.m_columnDescribeInfo != null)
										{
											int num4 = sqlMetInfo.m_columnDescribeInfo.Length;
											Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Error, new string[]
											{
												"Dumping all column names received from server.. "
											});
											for (int j = 0; j < num4; j++)
											{
												Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Error, new string[]
												{
													"Column name from server: " + sqlMetInfo.m_columnDescribeInfo[j].pColAlias
												});
											}
										}
										if (sqlMetInfo.parsedStmt != null)
										{
											foreach (OracleLpStatement oracleLpStatement3 in sqlMetInfo.parsedStmt)
											{
												Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Error, new string[]
												{
													"stmtement Text: " + oracleLpStatement3.Text
												});
												Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Error, new string[]
												{
													"Dumping all column names received from parser.. "
												});
												foreach (OracleLpColumnDescriptor oracleLpColumnDescriptor3 in ((OracleLpSelectStatement)oracleLpStatement3).ColumnDescriptors)
												{
													if (oracleLpColumnDescriptor3.ColumnName != null)
													{
														Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Error, new string[]
														{
															"Column name from parser: " + oracleLpColumnDescriptor3.ColumnName.DbName
														});
													}
												}
											}
										}
									}
								}
							}
						}
						if (ConfigBaseClass.m_bUseLegacyLocalParser || flag2)
						{
							List<SqlData> sqlTokList = null;
							List<SqlData> tableList = null;
							uint numberOfTables = 0U;
							bool flag3 = false;
							bool onlyWildcard = false;
							bool flag4 = false;
							bool flag5 = false;
							bool parseFailed = false;
							SQLParser.SqlPreLocalBuild(sqlMetInfo.pCommandText, false, out sqlTokList, out tableList, out numberOfTables, out flag4, out flag3, out onlyWildcard, out flag5, out parseFailed);
							SQLParser.SqlLocalBuildEx(connImpl, sqlMetInfo, tableList, numberOfTables, sqlTokList, onlyWildcard, (uint)sqlMetInfo.m_noOfColumns, metadataHasImplicitROWIDColumn, parseFailed);
						}
						sqlMetInfo.bStmtParsed = (sqlMetInfo.m_sqlMetaInfo.bStmtParsed = true);
						if (connImpl.m_cs.m_metadataPooling)
						{
							connImpl.m_pm.TryCacheLocalParsePrimaryKeyInfo(connImpl.ServiceName, sqlMetInfo.pCommandText, sqlMetInfo);
						}
					}
					result = true;
				}
			}
			catch (Exception ex3)
			{
				OracleException.HandleError(OracleTraceLevel.Private, OracleTraceTag.Error, ex3, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Exit, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x06001173 RID: 4467 RVA: 0x000C27B0 File Offset: 0x000C09B0
		private static bool SqlLocalBuildEx(OracleConnectionImpl connImpl, SQLMetaData sqlMetaData, List<SqlData> tableList, uint numberOfTables, List<SqlData> sqlTokList, bool onlyWildcard, uint noOfpMeta, bool metadataHasImplicitROWIDColumn, bool parseFailed)
		{
			if (sqlMetaData.bGotDescribeInfoFromDB)
			{
				bool flag = false;
				Dictionary<string, List<SqlData>> dictionary = null;
				string pCommandText = sqlMetaData.pCommandText;
				if (!parseFailed && !SQLParser.SqlRebuildSQL(sqlTokList, tableList, out pCommandText, numberOfTables, onlyWildcard, false, connImpl, null, ref dictionary))
				{
					return false;
				}
				if (numberOfTables == 1U && !SQLParser.SqlPopulateTableSchemaInfo(ref sqlMetaData.m_sqlMetaInfo, tableList))
				{
					return false;
				}
				if (!SQLParser.SqlFillMetaAttr(pCommandText, ref sqlMetaData, tableList, numberOfTables, onlyWildcard, noOfpMeta, metadataHasImplicitROWIDColumn))
				{
					return false;
				}
				if (!SQLParser.SqlFixColTabSch(ref sqlMetaData, numberOfTables, tableList, noOfpMeta, out flag))
				{
					return false;
				}
				if (flag)
				{
					if (dictionary == null && !SQLParser.SqlGetColumnsForAllTables(connImpl, null, tableList, out dictionary, false))
					{
						return false;
					}
					if (!SQLParser.SqlGetResolveAllCols(ref sqlMetaData.m_sqlMetaInfo, tableList, dictionary, numberOfTables, noOfpMeta))
					{
						return false;
					}
					if (!SQLParser.SqlFixColTabSch(ref sqlMetaData, numberOfTables, tableList, noOfpMeta, out flag))
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06001174 RID: 4468 RVA: 0x000C2864 File Offset: 0x000C0A64
		private static bool SqlPopulateTableSchemaInfo(ref SQLLocalParsePrimaryKeyInfo sqlParseInfo, List<SqlData> tableList)
		{
			string text;
			uint num;
			SQLParser.SqlGetTableName(tableList, 1U, SqlMacTokTyp.A_TABLE, out text, out num);
			if (text != null)
			{
				string text2;
				if (!SQLParser.SqlSplitStrings(text, SqlMicTokTyp.I_DOT, out sqlParseInfo.m_tableName, out sqlParseInfo.m_schemaName, out text2))
				{
					return false;
				}
				if (sqlParseInfo.m_tableName != null)
				{
					if (sqlParseInfo.m_tableName.Length != 0 && sqlParseInfo.m_tableName[0] != '"')
					{
						sqlParseInfo.m_tableName = sqlParseInfo.m_tableName.ToUpperInvariant();
					}
					sqlParseInfo.m_tableName = sqlParseInfo.m_tableName.Trim(new char[]
					{
						'"'
					});
				}
				if (sqlParseInfo.m_schemaName != null)
				{
					if (sqlParseInfo.m_schemaName.Length != 0 && sqlParseInfo.m_schemaName[0] != '"')
					{
						sqlParseInfo.m_schemaName = sqlParseInfo.m_schemaName.ToUpperInvariant();
					}
					sqlParseInfo.m_schemaName = sqlParseInfo.m_schemaName.Trim(new char[]
					{
						'"'
					});
				}
			}
			return true;
		}

		// Token: 0x06001175 RID: 4469 RVA: 0x000C2958 File Offset: 0x000C0B58
		private static void SqlGetTableName(List<SqlData> tabList, uint TableNum, SqlMacTokTyp AliasOrActual, out string tabName, out uint idAliasOrActual)
		{
			tabName = null;
			idAliasOrActual = 8U;
			int i = 0;
			while (i < tabList.Count)
			{
				SqlData sqlData = tabList[i];
				SqlData sqlData2 = null;
				if (i + 1 < tabList.Count)
				{
					sqlData2 = tabList[i + 1];
				}
				if (sqlData.m_tag == 8U && sqlData.m_id == TableNum)
				{
					if (AliasOrActual == SqlMacTokTyp.A_TABALIAS && sqlData2 != null && sqlData2.m_tag == 9U && sqlData2.m_id == TableNum)
					{
						tabName = sqlData2.m_data;
						idAliasOrActual = sqlData2.m_tag;
						return;
					}
					tabName = sqlData.m_data;
					idAliasOrActual = sqlData.m_tag;
					return;
				}
				else
				{
					i++;
				}
			}
		}

		// Token: 0x06001176 RID: 4470 RVA: 0x000C29F0 File Offset: 0x000C0BF0
		private static bool SqlSplitStrings(string inStr, SqlMicTokTyp Flag, out string outStr1, out string outStr2, out string outStr3)
		{
			char c = '"';
			char c2 = '.';
			char c3 = '@';
			char c4 = '"';
			string text;
			outStr3 = (text = null);
			string text2;
			outStr2 = (text2 = text);
			outStr1 = text2;
			string[] array = new string[3];
			bool flag = false;
			int num = 0;
			if (Flag == SqlMicTokTyp.I_DOT || Flag == SqlMicTokTyp.I_AT)
			{
				int length = inStr.Length;
				int num2 = 0;
				for (int i = 0; i < length; i++)
				{
					char c5 = inStr[i];
					if (!flag || c5 == c4)
					{
						if (c5 == c)
						{
							flag = !flag;
						}
						else if (Flag == SqlMicTokTyp.I_DOT && c5 == c2)
						{
							array[num++] = inStr.Substring(num2, i - num2);
							num2 = i + 1;
						}
						else if (c5 == c3)
						{
							array[num++] = inStr.Substring(num2, i - num2);
							num2 = i + 1;
						}
					}
				}
				if (num2 < length)
				{
					array[num++] = inStr.Substring(num2, length - num2);
				}
				if (num > 2)
				{
					outStr1 = array[2];
					outStr2 = array[1];
					outStr3 = array[0];
				}
				else if (num > 1)
				{
					outStr1 = array[1];
					outStr2 = array[0];
				}
				else if (num > 0)
				{
					outStr1 = array[0];
				}
				return true;
			}
			return false;
		}

		// Token: 0x06001177 RID: 4471 RVA: 0x000C2B20 File Offset: 0x000C0D20
		private static bool SqlFillMetaAttr(string commandText, ref SQLMetaData sqlMetadata, List<SqlData> tabList, uint NoOfTables, bool bOnlyWildcard, uint NoOfpMeta, bool metadataHasImplicitROWIDColumn)
		{
			List<SqlData> list = null;
			List<SqlData> list2 = null;
			string text = null;
			bool flag = false;
			if (!bOnlyWildcard || NoOfTables != 1U)
			{
				SQLParser.SqlParse(commandText, out list);
				bool flag2 = false;
				bool flag3 = false;
				SQLParser.SqlReadSQLTokenList(list, ref list2, out NoOfTables, out flag, out bOnlyWildcard, out flag2, out flag3);
				int num = 0;
				int num2 = sqlMetadata.m_columnDescribeInfo.Length;
				foreach (SqlData sqlData in list)
				{
					uint num3 = sqlData.m_id & 268435455U;
					uint num4 = sqlData.m_id & 4026531840U;
					if (num3 == 6U)
					{
						if (num >= num2)
						{
							if (ProviderConfig.m_bTraceLevelPrivate)
							{
								Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Error, new string[]
								{
									"SqlParser::SqlFillMetaAttr() - column count mismatch"
								});
							}
							return false;
						}
						ColumnDescribeInfo columnDescribeInfo = sqlMetadata.m_columnDescribeInfo[num];
						ColumnLocalParsePrimaryKeyInfo columnLocalParsePrimaryKeyInfo = sqlMetadata.m_sqlMetaInfo.m_columnMetaInfo[num];
						if (num4 == 0U || columnDescribeInfo.m_dataType == 11 || columnDescribeInfo.m_dataType == 208)
						{
							columnLocalParsePrimaryKeyInfo.Updatable = true;
							columnLocalParsePrimaryKeyInfo.bIsExpression = false;
							if (!SQLParser.SqlSplitStrings(sqlData.m_data, SqlMicTokTyp.I_DOT, out columnLocalParsePrimaryKeyInfo.m_columnName, out columnLocalParsePrimaryKeyInfo.pTabName, out columnLocalParsePrimaryKeyInfo.m_schemaName))
							{
								return false;
							}
							if (columnLocalParsePrimaryKeyInfo.m_columnName != null)
							{
								if (columnLocalParsePrimaryKeyInfo.m_columnName.Length != 0 && columnLocalParsePrimaryKeyInfo.m_columnName[0] != '"')
								{
									columnLocalParsePrimaryKeyInfo.m_columnName = columnLocalParsePrimaryKeyInfo.m_columnName.ToUpperInvariant();
								}
								columnLocalParsePrimaryKeyInfo.m_columnName = columnLocalParsePrimaryKeyInfo.m_columnName.Trim(new char[]
								{
									'"'
								});
								if (columnLocalParsePrimaryKeyInfo.m_columnName == "ROWID")
								{
									columnLocalParsePrimaryKeyInfo.Updatable = false;
									columnLocalParsePrimaryKeyInfo.bIsExpression = true;
								}
							}
							if (columnLocalParsePrimaryKeyInfo.pTabName != null)
							{
								if (columnLocalParsePrimaryKeyInfo.pTabName.Length != 0 && columnLocalParsePrimaryKeyInfo.pTabName[0] != '"')
								{
									columnLocalParsePrimaryKeyInfo.pTabName = columnLocalParsePrimaryKeyInfo.pTabName.ToUpperInvariant();
								}
								columnLocalParsePrimaryKeyInfo.pTabName = columnLocalParsePrimaryKeyInfo.pTabName.Trim(new char[]
								{
									'"'
								});
							}
							if (columnLocalParsePrimaryKeyInfo.m_schemaName != null)
							{
								if (columnLocalParsePrimaryKeyInfo.m_schemaName.Length != 0 && columnLocalParsePrimaryKeyInfo.m_schemaName[0] != '"')
								{
									columnLocalParsePrimaryKeyInfo.m_schemaName = columnLocalParsePrimaryKeyInfo.m_schemaName.ToUpperInvariant();
								}
								columnLocalParsePrimaryKeyInfo.m_schemaName = columnLocalParsePrimaryKeyInfo.m_schemaName.Trim(new char[]
								{
									'"'
								});
							}
						}
						else
						{
							columnLocalParsePrimaryKeyInfo.Updatable = false;
							columnLocalParsePrimaryKeyInfo.bIsExpression = true;
						}
						num++;
					}
				}
				return true;
			}
			for (uint num5 = 0U; num5 < NoOfpMeta; num5 += 1U)
			{
				ColumnDescribeInfo columnDescribeInfo2 = sqlMetadata.m_columnDescribeInfo[(int)((UIntPtr)num5)];
				ColumnLocalParsePrimaryKeyInfo columnLocalParsePrimaryKeyInfo2 = sqlMetadata.m_sqlMetaInfo.m_columnMetaInfo[(int)((UIntPtr)num5)];
				columnLocalParsePrimaryKeyInfo2.m_columnName = columnDescribeInfo2.pColAlias;
				columnLocalParsePrimaryKeyInfo2.Updatable = true;
				columnLocalParsePrimaryKeyInfo2.bIsExpression = false;
				if (num5 == NoOfpMeta - 1U && metadataHasImplicitROWIDColumn && (columnDescribeInfo2.m_dataType == 11 || columnDescribeInfo2.m_dataType == 208))
				{
					columnLocalParsePrimaryKeyInfo2.m_columnName = "ROWID";
					columnLocalParsePrimaryKeyInfo2.Updatable = false;
					columnLocalParsePrimaryKeyInfo2.bIsExpression = true;
				}
				uint num6;
				SQLParser.SqlGetTableName(tabList, 1U, SqlMacTokTyp.A_TABALIAS, out text, out num6);
				if (text != null && text.Length != 0 && num6 == 9U)
				{
					columnLocalParsePrimaryKeyInfo2.pTabAlias = text;
					if (columnLocalParsePrimaryKeyInfo2.pTabAlias != null && columnLocalParsePrimaryKeyInfo2.pTabAlias.Length != 0 && columnLocalParsePrimaryKeyInfo2.pTabAlias[0] != '"')
					{
						columnLocalParsePrimaryKeyInfo2.pTabAlias = columnLocalParsePrimaryKeyInfo2.pTabAlias.ToUpperInvariant();
					}
					columnLocalParsePrimaryKeyInfo2.pTabAlias = columnLocalParsePrimaryKeyInfo2.pTabAlias.Trim(new char[]
					{
						'"'
					});
				}
				SQLParser.SqlGetTableName(tabList, 1U, SqlMacTokTyp.A_TABLE, out text, out num6);
				string text2;
				if (!SQLParser.SqlSplitStrings(text, SqlMicTokTyp.I_DOT, out columnLocalParsePrimaryKeyInfo2.pTabName, out columnLocalParsePrimaryKeyInfo2.m_schemaName, out text2))
				{
					return false;
				}
				if (columnLocalParsePrimaryKeyInfo2.pTabName != null)
				{
					if (columnLocalParsePrimaryKeyInfo2.pTabName.Length != 0 && columnLocalParsePrimaryKeyInfo2.pTabName[0] != '"')
					{
						columnLocalParsePrimaryKeyInfo2.pTabName = columnLocalParsePrimaryKeyInfo2.pTabName.ToUpperInvariant();
					}
					columnLocalParsePrimaryKeyInfo2.pTabName = columnLocalParsePrimaryKeyInfo2.pTabName.Trim(new char[]
					{
						'"'
					});
				}
				if (columnLocalParsePrimaryKeyInfo2.m_schemaName != null)
				{
					if (columnLocalParsePrimaryKeyInfo2.m_schemaName.Length != 0 && columnLocalParsePrimaryKeyInfo2.m_schemaName[0] != '"')
					{
						columnLocalParsePrimaryKeyInfo2.m_schemaName = columnLocalParsePrimaryKeyInfo2.m_schemaName.ToUpperInvariant();
					}
					columnLocalParsePrimaryKeyInfo2.m_schemaName = columnLocalParsePrimaryKeyInfo2.m_schemaName.Trim(new char[]
					{
						'"'
					});
				}
			}
			return true;
		}

		// Token: 0x06001178 RID: 4472 RVA: 0x000C2FF4 File Offset: 0x000C11F4
		private static bool SqlRebuildSQL(List<SqlData> sqlTokList, List<SqlData> tabList, out string newSQL, uint NoOfTables, bool bOnlyWildcard, bool bAddRowid, OracleConnectionImpl connImpl, OracleConnection conn, ref Dictionary<string, List<SqlData>> tableColumnsMap)
		{
			newSQL = string.Empty;
			StringBuilder stringBuilder = new StringBuilder();
			using (List<SqlData>.Enumerator enumerator = sqlTokList.GetEnumerator())
			{
				IL_23B:
				while (enumerator.MoveNext())
				{
					SqlData sqlData = enumerator.Current;
					uint num = sqlData.m_id & 268435455U;
					uint num2 = sqlData.m_id & 4026531840U;
					if (num2 == 268435456U)
					{
						if (bOnlyWildcard && NoOfTables == 1U)
						{
							string text = sqlData.m_data;
							string[] array = text.Split(new char[]
							{
								'.',
								'@'
							});
							int num3 = array.Length;
							if (num3 > 1)
							{
								stringBuilder.Append(sqlData.m_data);
							}
							else
							{
								uint num4;
								SQLParser.SqlGetTableName(tabList, 1U, SqlMacTokTyp.A_TABALIAS, out text, out num4);
								string text2;
								string text3;
								string text4;
								if (!SQLParser.SqlSplitStrings(text, SqlMicTokTyp.I_AT, out text2, out text3, out text4))
								{
									return false;
								}
								if (text3 != null && text3.Length > 0)
								{
									text = text3;
								}
								else
								{
									text = text2;
								}
								stringBuilder.Append(text);
								stringBuilder.Append(".");
								stringBuilder.Append(sqlData.m_data);
							}
						}
						else
						{
							string text3;
							string text5;
							string schemaName;
							if (!SQLParser.SqlSplitStrings(sqlData.m_data, SqlMicTokTyp.I_DOT, out text5, out text3, out schemaName))
							{
								return false;
							}
							if (tableColumnsMap == null && !SQLParser.SqlGetColumnsForAllTables(connImpl, conn, tabList, out tableColumnsMap, true))
							{
								return false;
							}
							if (text3 == null || text3.Length <= 0)
							{
								uint num5 = 1U;
								for (;;)
								{
									string text;
									uint num4;
									SQLParser.SqlGetTableName(tabList, num5, SqlMacTokTyp.A_TABALIAS, out text, out num4);
									if (text == null || text.Length <= 0)
									{
										goto IL_23B;
									}
									if (!SQLParser.SqlSplitStrings(text, SqlMicTokTyp.I_DOT, out text3, out schemaName, out text5))
									{
										break;
									}
									if (num5 > 1U)
									{
										stringBuilder.Append(", ");
									}
									if (text3 != null && text3.Length > 0)
									{
										SQLParser.SqlAddColNames(ref stringBuilder, tableColumnsMap, tabList, text3, schemaName);
									}
									num5 += 1U;
								}
								return false;
							}
							SQLParser.SqlAddColNames(ref stringBuilder, tableColumnsMap, tabList, text3, schemaName);
						}
					}
					else if (num == 5U)
					{
						if (bAddRowid && !SQLParser.SqlAppendRowid(ref stringBuilder, tabList))
						{
							return false;
						}
						stringBuilder.Append(" ");
						stringBuilder.Append(sqlData.m_data);
						stringBuilder.Append(" ");
					}
					else
					{
						stringBuilder.Append(sqlData.m_data);
						stringBuilder.Append(" ");
					}
				}
			}
			newSQL = stringBuilder.ToString();
			return true;
		}

		// Token: 0x06001179 RID: 4473 RVA: 0x000C3284 File Offset: 0x000C1484
		private static void SqlAddColNames(ref StringBuilder newSQLBuilder, Dictionary<string, List<SqlData>> tableColumnsMap, List<SqlData> tabList, string tabName, string schemaName)
		{
			uint tableNum = 0U;
			uint num = 0U;
			string value = null;
			string text = null;
			string text2 = null;
			if (tabName != null && tabName.Length > 0)
			{
				if (!SQLParser.SqlGetTableIndex(tabList, tabName, out tableNum, out num, true))
				{
					return;
				}
				string text3;
				if (!SQLParser.SqlSplitStrings(tabName, SqlMicTokTyp.I_AT, out text, out text2, out text3))
				{
					return;
				}
				if (text2 != null && text2.Length > 0)
				{
					value = text2;
				}
				else
				{
					value = text;
				}
			}
			string key;
			SQLParser.SqlGetTableName(tabList, tableNum, SqlMacTokTyp.A_TABLE, out key, out num);
			List<SqlData> list = tableColumnsMap[key];
			int count = list.Count;
			for (int i = 0; i < count; i++)
			{
				if (schemaName != null && schemaName.Length > 0)
				{
					newSQLBuilder.Append(schemaName);
					newSQLBuilder.Append(".");
				}
				if (tabName != null && tabName.Length > 0)
				{
					newSQLBuilder.Append(value);
					newSQLBuilder.Append(".");
				}
				SqlData sqlData = list[i];
				newSQLBuilder.Append(sqlData.m_data);
				if (i < count - 1)
				{
					newSQLBuilder.Append(", ");
				}
				else
				{
					newSQLBuilder.Append(" ");
				}
			}
		}

		// Token: 0x0600117A RID: 4474 RVA: 0x000C3398 File Offset: 0x000C1598
		private static bool SqlGetTableIndex(List<SqlData> tabList, string tabName, out uint index, out uint idAliasOrActual, bool tokenizeTableName = true)
		{
			index = 0U;
			idAliasOrActual = 0U;
			foreach (SqlData sqlData in tabList)
			{
				string strA;
				if (tokenizeTableName)
				{
					string text;
					string text2;
					string text3;
					if (!SQLParser.SqlSplitStrings(sqlData.m_data, SqlMicTokTyp.I_DOT, out text, out text2, out text3))
					{
						return false;
					}
					if (text == null)
					{
						return false;
					}
					string[] array = text.Split(new char[]
					{
						'@'
					});
					if (array.Length > 1)
					{
						strA = array[1];
					}
					else
					{
						strA = array[0];
					}
				}
				else
				{
					if (sqlData.m_data == null)
					{
						return false;
					}
					strA = sqlData.m_data;
				}
				if (string.Compare(strA, tabName, true) == 0)
				{
					index = sqlData.m_id;
					idAliasOrActual = sqlData.m_tag;
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600117B RID: 4475 RVA: 0x000C347C File Offset: 0x000C167C
		private static bool SqlFixColTabSch(ref SQLMetaData sqlMetadata, uint NoOfTables, List<SqlData> tabList, uint NoOfpMeta, out bool bUnresolvedColumn)
		{
			SQLLocalParsePrimaryKeyInfo sqlMetaInfo = sqlMetadata.m_sqlMetaInfo;
			bUnresolvedColumn = false;
			for (uint num = 0U; num < NoOfpMeta; num += 1U)
			{
				ColumnLocalParsePrimaryKeyInfo columnLocalParsePrimaryKeyInfo = sqlMetaInfo.m_columnMetaInfo[(int)((UIntPtr)num)];
				uint tableNum;
				uint num2;
				if (columnLocalParsePrimaryKeyInfo.pTabName != null && columnLocalParsePrimaryKeyInfo.pTabName.Length > 0 && SQLParser.SqlGetTableIndex(tabList, columnLocalParsePrimaryKeyInfo.pTabName, out tableNum, out num2, true) && num2 == 9U)
				{
					uint num3;
					if (columnLocalParsePrimaryKeyInfo.pTabAlias == null || columnLocalParsePrimaryKeyInfo.pTabAlias.Length == 0)
					{
						string text;
						SQLParser.SqlGetTableName(tabList, tableNum, SqlMacTokTyp.A_TABALIAS, out text, out num3);
						if (text != null && text.Length > 0 && num3 == 9U)
						{
							columnLocalParsePrimaryKeyInfo.pTabAlias = text;
						}
					}
					string text2;
					SQLParser.SqlGetTableName(tabList, tableNum, SqlMacTokTyp.A_TABLE, out text2, out num3);
					if (text2 != null && text2.Length > 0)
					{
						string text3;
						if (!SQLParser.SqlSplitStrings(text2, SqlMicTokTyp.I_DOT, out columnLocalParsePrimaryKeyInfo.pTabName, out columnLocalParsePrimaryKeyInfo.m_schemaName, out text3))
						{
							return false;
						}
						if (columnLocalParsePrimaryKeyInfo.pTabName != null && columnLocalParsePrimaryKeyInfo.pTabName.Length != 0)
						{
							if (columnLocalParsePrimaryKeyInfo.pTabName[0] != '"')
							{
								columnLocalParsePrimaryKeyInfo.pTabName = columnLocalParsePrimaryKeyInfo.pTabName.ToUpperInvariant();
							}
							columnLocalParsePrimaryKeyInfo.pTabName = columnLocalParsePrimaryKeyInfo.pTabName.Trim(new char[]
							{
								'"'
							});
						}
						if (columnLocalParsePrimaryKeyInfo.m_schemaName != null && columnLocalParsePrimaryKeyInfo.m_schemaName.Length != 0)
						{
							if (columnLocalParsePrimaryKeyInfo.m_schemaName[0] != '"')
							{
								columnLocalParsePrimaryKeyInfo.m_schemaName = columnLocalParsePrimaryKeyInfo.m_schemaName.ToUpperInvariant();
							}
							columnLocalParsePrimaryKeyInfo.m_schemaName = columnLocalParsePrimaryKeyInfo.m_schemaName.Trim(new char[]
							{
								'"'
							});
						}
					}
				}
				if ((columnLocalParsePrimaryKeyInfo.pTabName == null || columnLocalParsePrimaryKeyInfo.pTabName.Length == 0) && (columnLocalParsePrimaryKeyInfo.Updatable || columnLocalParsePrimaryKeyInfo.m_columnName == "ROWID"))
				{
					if (sqlMetaInfo.m_tableName != null && sqlMetaInfo.m_tableName.Length > 0 && NoOfTables == 1U)
					{
						columnLocalParsePrimaryKeyInfo.pTabName = sqlMetaInfo.m_tableName;
					}
					else
					{
						bUnresolvedColumn = true;
					}
				}
				if ((columnLocalParsePrimaryKeyInfo.m_schemaName == null || columnLocalParsePrimaryKeyInfo.m_schemaName.Length == 0) && (columnLocalParsePrimaryKeyInfo.Updatable || columnLocalParsePrimaryKeyInfo.m_columnName == "ROWID") && sqlMetaInfo.m_schemaName != null && sqlMetaInfo.m_schemaName.Length > 0 && NoOfTables == 1U)
				{
					columnLocalParsePrimaryKeyInfo.m_schemaName = sqlMetaInfo.m_schemaName;
				}
			}
			return true;
		}

		// Token: 0x0600117C RID: 4476 RVA: 0x000C36EC File Offset: 0x000C18EC
		private static bool SqlGetResolveAllCols(ref SQLLocalParsePrimaryKeyInfo sqlParseInfo, List<SqlData> tabList, Dictionary<string, List<SqlData>> tableColumnMap, uint NoOfTables, uint NoOfpMeta)
		{
			for (uint num = 0U; num < NoOfpMeta; num += 1U)
			{
				ColumnLocalParsePrimaryKeyInfo columnLocalParsePrimaryKeyInfo = sqlParseInfo.m_columnMetaInfo[(int)((UIntPtr)num)];
				bool flag = columnLocalParsePrimaryKeyInfo.pTabName != null && columnLocalParsePrimaryKeyInfo.pTabName.Length > 0;
				bool flag2 = columnLocalParsePrimaryKeyInfo.m_schemaName != null && columnLocalParsePrimaryKeyInfo.m_schemaName.Length > 0;
				if ((!flag || !flag2) && columnLocalParsePrimaryKeyInfo.Updatable)
				{
					string colName;
					if (columnLocalParsePrimaryKeyInfo.m_columnName[0] == '"')
					{
						colName = columnLocalParsePrimaryKeyInfo.m_columnName.Trim(new char[]
						{
							'"'
						});
					}
					else
					{
						colName = columnLocalParsePrimaryKeyInfo.m_columnName;
					}
					if (!flag)
					{
						columnLocalParsePrimaryKeyInfo.Updatable = false;
						columnLocalParsePrimaryKeyInfo.bIsExpression = true;
					}
					foreach (string text in tableColumnMap.Keys)
					{
						if (SQLParser.SqlIsColumnInList(tableColumnMap[text], colName))
						{
							uint tableNum;
							uint num2;
							if (!SQLParser.SqlGetTableIndex(tabList, text, out tableNum, out num2, false))
							{
								return false;
							}
							uint num3;
							if (columnLocalParsePrimaryKeyInfo.pTabAlias == null || columnLocalParsePrimaryKeyInfo.pTabAlias.Length <= 0)
							{
								string text2;
								SQLParser.SqlGetTableName(tabList, tableNum, SqlMacTokTyp.A_TABALIAS, out text2, out num3);
								if (text2 != null && text2.Length > 0 && num3 == 9U)
								{
									columnLocalParsePrimaryKeyInfo.pTabAlias = text2;
								}
							}
							string text3;
							SQLParser.SqlGetTableName(tabList, tableNum, SqlMacTokTyp.A_TABLE, out text3, out num3);
							if (text3 == null || text3.Length <= 0)
							{
								break;
							}
							string text4;
							string text5;
							string text6;
							if (!SQLParser.SqlSplitStrings(text3, SqlMicTokTyp.I_DOT, out text4, out text5, out text6))
							{
								return false;
							}
							columnLocalParsePrimaryKeyInfo.Updatable = true;
							columnLocalParsePrimaryKeyInfo.bIsExpression = false;
							if (!flag && text4 != null)
							{
								columnLocalParsePrimaryKeyInfo.pTabName = text4;
								if (columnLocalParsePrimaryKeyInfo.pTabName.Length != 0 && columnLocalParsePrimaryKeyInfo.pTabName[0] != '"')
								{
									columnLocalParsePrimaryKeyInfo.pTabName = columnLocalParsePrimaryKeyInfo.pTabName.ToUpperInvariant();
								}
								columnLocalParsePrimaryKeyInfo.pTabName = columnLocalParsePrimaryKeyInfo.pTabName.Trim(new char[]
								{
									'"'
								});
							}
							if (!flag2 && text5 != null)
							{
								columnLocalParsePrimaryKeyInfo.m_schemaName = text5;
								if (columnLocalParsePrimaryKeyInfo.m_schemaName.Length != 0 && columnLocalParsePrimaryKeyInfo.m_schemaName[0] != '"')
								{
									columnLocalParsePrimaryKeyInfo.m_schemaName = columnLocalParsePrimaryKeyInfo.m_schemaName.ToUpperInvariant();
								}
								columnLocalParsePrimaryKeyInfo.m_schemaName = columnLocalParsePrimaryKeyInfo.m_schemaName.Trim(new char[]
								{
									'"'
								});
								break;
							}
							break;
						}
					}
				}
			}
			return true;
		}

		// Token: 0x0600117D RID: 4477 RVA: 0x000C398C File Offset: 0x000C1B8C
		private static bool SqlIsColumnInList(List<SqlData> columnList, string colName)
		{
			foreach (SqlData sqlData in columnList)
			{
				if (sqlData.m_data == colName)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600117E RID: 4478 RVA: 0x000C39E8 File Offset: 0x000C1BE8
		private static bool SqlGetColumnsForAllTables(OracleConnectionImpl connImpl, OracleConnection conn, List<SqlData> tableList, out Dictionary<string, List<SqlData>> tableColumnMap, bool addQuotes)
		{
			tableColumnMap = new Dictionary<string, List<SqlData>>();
			foreach (SqlData sqlData in tableList)
			{
				if (sqlData.m_tag == 8U)
				{
					List<SqlData> value = null;
					if (!SQLParser.SqlGetColumns(connImpl, conn, sqlData.m_data, out value, addQuotes))
					{
						return false;
					}
					tableColumnMap[sqlData.m_data] = value;
				}
			}
			return true;
		}

		// Token: 0x0600117F RID: 4479 RVA: 0x000C3A6C File Offset: 0x000C1C6C
		private static bool SqlGetColumns(OracleConnectionImpl connImpl, OracleConnection conn, string tableName, out List<SqlData> columnList, bool addQuotes)
		{
			columnList = new List<SqlData>();
			SQLMetaData sqlmetaData = null;
			int num = 0;
			new OracleCommandImpl().RetrieveMetadata("SELECT * FROM " + tableName, CommandType.Text, null, connImpl, conn, out sqlmetaData, out num);
			if (sqlmetaData == null || !sqlmetaData.bGotDescribeInfoFromDB)
			{
				return false;
			}
			foreach (ColumnDescribeInfo columnDescribeInfo2 in sqlmetaData.m_columnDescribeInfo)
			{
				if (addQuotes)
				{
					columnList.Add(new SqlData("\"" + columnDescribeInfo2.pColAlias + "\"", 6U, 0U));
				}
				else
				{
					columnList.Add(new SqlData(columnDescribeInfo2.pColAlias, 6U, 0U));
				}
			}
			return true;
		}

		// Token: 0x06001180 RID: 4480 RVA: 0x000C3B0C File Offset: 0x000C1D0C
		private static bool SqlAppendRowid(ref StringBuilder newSQLBuilder, List<SqlData> tabList)
		{
			uint num = 1U;
			for (;;)
			{
				string text;
				uint num2;
				SQLParser.SqlGetTableName(tabList, num, SqlMacTokTyp.A_TABALIAS, out text, out num2);
				if (text == null || text.Length <= 0)
				{
					return true;
				}
				string text2;
				string text3;
				string text4;
				if (!SQLParser.SqlSplitStrings(text, SqlMicTokTyp.I_AT, out text2, out text3, out text4))
				{
					break;
				}
				if (text3 != null && text3.Length > 0)
				{
					text = text3;
				}
				else
				{
					text = text2;
				}
				newSQLBuilder.Append(string.Format(", {0}.{1} ", text, "ROWID"));
				num += 1U;
			}
			return false;
		}

		// Token: 0x040013E2 RID: 5090
		private const uint SQL_TOKTYP_EXTRACTOR = 268435455U;

		// Token: 0x040013E3 RID: 5091
		private const uint SQL_TOKATR_EXTRACTOR = 4026531840U;

		// Token: 0x040013E4 RID: 5092
		private const uint SQL_TOKATR_WILDCARD = 268435456U;

		// Token: 0x040013E5 RID: 5093
		private const uint SQL_TOKATR_PSEUDOCOL = 536870912U;

		// Token: 0x040013E6 RID: 5094
		private const uint SQL_TOKATR_NORMALCOL = 0U;

		// Token: 0x040013E7 RID: 5095
		private const int dotRowIdLen = 6;

		// Token: 0x040013E8 RID: 5096
		private const int MAX_LITERAL_SIZE = 4100;

		// Token: 0x040013E9 RID: 5097
		internal const string SQL_COLUMN_ROWID = "ROWID";

		// Token: 0x040013EA RID: 5098
		private const ushort UCS2_HASH = 65283;

		// Token: 0x040013EB RID: 5099
		private const ushort UCS2_DOLLAR = 65284;

		// Token: 0x040013EC RID: 5100
		private const ushort UCS2_UNDERSCORE = 65343;

		// Token: 0x040013ED RID: 5101
		private static string s_replaceString = "\r\n";
	}
}
