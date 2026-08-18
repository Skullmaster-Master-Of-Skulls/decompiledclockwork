using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;

namespace OracleInternal.Common
{
	// Token: 0x020000BD RID: 189
	public static class OracleMetadata
	{
		// Token: 0x0600073E RID: 1854 RVA: 0x000434B8 File Offset: 0x000416B8
		public static IEnumerable<OracleLpTableColumns> GetColumnInformation(OracleConnection con, IEnumerable<OracleLpTable> tables)
		{
			List<string> list = new List<string>();
			bool flag = false;
			foreach (OracleLpTable oracleLpTable in tables)
			{
				if (!string.IsNullOrEmpty(oracleLpTable.m_dbLinkName))
				{
					if (!list.Contains(oracleLpTable.m_dbLinkName))
					{
						list.Add(oracleLpTable.m_dbLinkName);
					}
				}
				else
				{
					flag = true;
				}
			}
			if ((list.Count == 1 && !flag) || list.Count == 0)
			{
				string dbLinkName = string.Empty;
				if (list.Count == 1)
				{
					dbLinkName = list[0];
				}
				return OracleMetadata.GetColumnInfoWithSameDbLink(con, tables, dbLinkName);
			}
			return OracleMetadata.GetColumnInfoWithDiffDbLinks(con, tables);
		}

		// Token: 0x0600073F RID: 1855 RVA: 0x0004356C File Offset: 0x0004176C
		public static IEnumerable<OracleLpTableColumns> GetColumnInfoWithSameDbLink(OracleConnection con, IEnumerable<OracleLpTable> tables, string dbLinkName)
		{
			int num = 0;
			bool flag = false;
			List<OracleLpTableColumns> list = new List<OracleLpTableColumns>();
			bool flag2 = false;
			List<string> list2 = new List<string>();
			List<string> list3 = new List<string>();
			List<string> list4 = new List<string>();
			new List<OracleRefCursor>();
			string text = string.Empty;
			string value = string.Empty;
			int count = ((List<OracleLpTable>)tables).Count;
			if (!string.IsNullOrEmpty(dbLinkName))
			{
				flag = true;
				value = OracleMetadata.atSign + dbLinkName;
			}
			else
			{
				dbLinkName = string.Empty;
			}
			foreach (OracleLpTable oracleLpTable in tables)
			{
				if (string.IsNullOrEmpty(oracleLpTable.m_schemaName))
				{
					text = ((con.m_cs.m_userId[0] != '"') ? con.m_cs.m_userId.ToUpperInvariant() : con.m_cs.m_userId.Trim(OracleMetadata.double_quotes));
					oracleLpTable.m_schemaName = text;
				}
				else
				{
					text = oracleLpTable.m_schemaName;
				}
				if (flag)
				{
					string text2 = string.Empty;
					if (oracleLpTable.m_dbLinkName.Contains("@"))
					{
						string[] array = oracleLpTable.m_dbLinkName.Split(new char[]
						{
							'@'
						});
						dbLinkName = array[0];
						text2 = OracleMetadata.atSign + array[1].ToUpperInvariant();
					}
					else
					{
						dbLinkName = oracleLpTable.m_dbLinkName;
					}
					dbLinkName = ((dbLinkName[0] != '"') ? dbLinkName.ToUpperInvariant() : dbLinkName.Trim(OracleMetadata.double_quotes).ToUpperInvariant());
					if (!dbLinkName.Contains("."))
					{
						dbLinkName = dbLinkName + "." + con.DatabaseDomainName.ToUpperInvariant() + text2;
					}
					else
					{
						dbLinkName += text2;
					}
				}
				OracleLpTableColumns oracleLpTableColumns = flag ? con.m_oracleConnectionImpl.m_pm.m_tableColumnsCache.Get(con.m_oracleConnectionImpl.ServiceName, text, oracleLpTable.m_tableName + '@' + dbLinkName) : con.m_oracleConnectionImpl.m_pm.m_tableColumnsCache.Get(con.m_oracleConnectionImpl.ServiceName, text, oracleLpTable.m_tableName);
				if (oracleLpTableColumns == null)
				{
					if (!flag)
					{
						list3.Add(text);
					}
					else
					{
						list4.Add(dbLinkName);
					}
					list2.Add(oracleLpTable.m_tableName);
					num++;
					flag2 = true;
					list.Add(null);
				}
				else
				{
					list.Add(oracleLpTableColumns);
				}
			}
			if (flag2)
			{
				StringBuilder stringBuilder = new StringBuilder(SQLMetaData.pSqlBlock1);
				stringBuilder.Append(":1");
				if (!flag)
				{
					stringBuilder.Append(SQLMetaData.pSqlBlock2);
				}
				else
				{
					stringBuilder.Append(SQLMetaData.pSqlBlock5);
				}
				stringBuilder.Append(":2");
				stringBuilder.Append(SQLMetaData.pSqlBlock3a);
				if (flag)
				{
					stringBuilder.Append(value);
				}
				stringBuilder.Append(SQLMetaData.pSqlBlock3b);
				if (flag)
				{
					stringBuilder.Append(value);
				}
				stringBuilder.Append(SQLMetaData.pSqlBlock3c);
				stringBuilder.Append(SQLMetaData.pSqlBlock14);
				stringBuilder.Append(SQLMetaData.pSqlBlock18);
				stringBuilder.Append(SQLMetaData.pSqlBlock7);
				stringBuilder.Append(":3");
				stringBuilder.Append(SQLMetaData.pSqlBlock15);
				stringBuilder.Append(SQLMetaData.pSqlBlock16);
				stringBuilder.Append(SQLMetaData.pSqlBlock7);
				stringBuilder.Append(":3");
				stringBuilder.Append(SQLMetaData.pSqlBlock6);
				if (flag)
				{
					stringBuilder.Append(value);
				}
				stringBuilder.Append(SQLMetaData.pSqlBlock8);
				stringBuilder.Append(SQLMetaData.pSqlBlock17);
				stringBuilder.Append(SQLMetaData.pSqlBlock9);
				OracleCommand oracleCommand = new OracleCommand(stringBuilder.ToString(), con);
				OracleParameter oracleParameter = new OracleParameter(":1", OracleDbType.Varchar2, ParameterDirection.Input);
				oracleParameter.Value = list2.ToArray();
				oracleCommand.Parameters.Add(oracleParameter);
				if (!flag)
				{
					OracleParameter oracleParameter2 = new OracleParameter(":2", OracleDbType.Varchar2, ParameterDirection.Input);
					oracleParameter2.Value = list3.ToArray();
					oracleCommand.Parameters.Add(oracleParameter2);
				}
				else
				{
					OracleParameter oracleParameter3 = new OracleParameter(":2", OracleDbType.Varchar2, ParameterDirection.Input);
					oracleParameter3.Value = list4.ToArray();
					oracleCommand.Parameters.Add(oracleParameter3);
				}
				OracleParameter param = new OracleParameter(":3", OracleDbType.RefCursor, ParameterDirection.Output);
				oracleCommand.Parameters.Add(param);
				oracleCommand.ArrayBindCount = num;
				OracleDataReader oracleDataReader = null;
				try
				{
					oracleDataReader = oracleCommand.ExecuteReader();
					int num2 = 0;
					foreach (OracleLpTable oracleLpTable2 in tables)
					{
						num2++;
						if (list[num2 - 1] == null)
						{
							List<OracleLpColumn> list5 = new List<OracleLpColumn>();
							while (oracleDataReader.Read())
							{
								string @string = oracleDataReader.GetString(0);
								OracleLpColumn oracleLpColumn = new OracleLpColumn(@string);
								oracleLpColumn.m_schemaName = oracleDataReader.GetString(1);
								oracleLpColumn.m_tableName = oracleDataReader.GetString(2);
								string string2 = oracleDataReader.GetString(3);
								if (string2 != null)
								{
									oracleLpColumn.m_isHidden = string2.Equals("yes", StringComparison.InvariantCultureIgnoreCase);
								}
								if (ProviderConfig.m_bTraceLevelPrivate)
								{
									Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Error, new string[]
									{
										"Retrieved Column from data dictionary for {0}.{1} : {2} , Hidden: {3}",
										oracleLpColumn.m_schemaName,
										oracleLpColumn.m_tableName,
										@string,
										oracleLpColumn.m_isHidden.ToString()
									});
								}
								list5.Add(oracleLpColumn);
							}
							OracleLpTableColumns oracleLpTableColumns2 = new OracleLpTableColumns(oracleLpTable2, list5);
							list[num2 - 1] = oracleLpTableColumns2;
							if (flag)
							{
								string text3 = string.Empty;
								if (oracleLpTable2.m_dbLinkName.Contains("@"))
								{
									string[] array2 = oracleLpTable2.m_dbLinkName.Split(new char[]
									{
										'@'
									});
									dbLinkName = array2[0];
									text3 = OracleMetadata.atSign + array2[1].ToUpperInvariant();
								}
								else
								{
									dbLinkName = oracleLpTable2.m_dbLinkName;
								}
								dbLinkName = ((dbLinkName[0] != '"') ? dbLinkName.ToUpperInvariant() : dbLinkName.Trim(OracleMetadata.double_quotes).ToUpperInvariant());
								if (!dbLinkName.Contains("."))
								{
									dbLinkName = dbLinkName + "." + con.DatabaseDomainName.ToUpperInvariant() + text3;
								}
								else
								{
									dbLinkName += text3;
								}
							}
							if (flag)
							{
								con.m_oracleConnectionImpl.m_pm.m_tableColumnsCache.Put(con.m_oracleConnectionImpl.ServiceName, oracleLpTable2.m_schemaName, oracleLpTable2.m_tableName + '@' + dbLinkName, oracleLpTableColumns2);
							}
							else
							{
								con.m_oracleConnectionImpl.m_pm.m_tableColumnsCache.Put(con.m_oracleConnectionImpl.ServiceName, oracleLpTable2.m_schemaName, oracleLpTable2.m_tableName, oracleLpTableColumns2);
							}
							oracleDataReader.NextResult();
						}
					}
				}
				finally
				{
					if (oracleDataReader != null)
					{
						oracleDataReader.Close();
					}
				}
			}
			return list;
		}

		// Token: 0x06000740 RID: 1856 RVA: 0x00043C80 File Offset: 0x00041E80
		public static IEnumerable<OracleLpTableColumns> GetColumnInfoWithDiffDbLinks(OracleConnection con, IEnumerable<OracleLpTable> tables)
		{
			int num = 0;
			int num2 = 1;
			string text = string.Empty;
			string text2 = string.Empty;
			string value = string.Empty;
			List<OracleLpTableColumns> list = new List<OracleLpTableColumns>();
			StringBuilder stringBuilder = new StringBuilder(SQLMetaData.pSqlBlock1);
			OracleCommand oracleCommand = new OracleCommand();
			bool flag = false;
			foreach (OracleLpTable oracleLpTable in tables)
			{
				bool flag2;
				if (string.IsNullOrEmpty(oracleLpTable.m_dbLinkName))
				{
					if (string.IsNullOrEmpty(oracleLpTable.m_schemaName))
					{
						text = ((con.m_cs.m_userId[0] != '"') ? con.m_cs.m_userId.ToUpperInvariant() : con.m_cs.m_userId.Trim(OracleMetadata.double_quotes));
						oracleLpTable.m_schemaName = text;
					}
					else
					{
						text = oracleLpTable.m_schemaName;
					}
					flag2 = false;
				}
				else
				{
					string text3 = string.Empty;
					value = OracleMetadata.atSign + oracleLpTable.m_dbLinkName;
					if (oracleLpTable.m_dbLinkName.Contains("@"))
					{
						string[] array = oracleLpTable.m_dbLinkName.Split(new char[]
						{
							'@'
						});
						text2 = array[0];
						text3 = OracleMetadata.atSign + array[1].Trim(OracleMetadata.double_quotes).ToUpperInvariant();
					}
					else
					{
						text2 = oracleLpTable.m_dbLinkName;
					}
					text2 = ((text2[0] != '"') ? text2.ToUpperInvariant() : text2.Trim(OracleMetadata.double_quotes).ToUpperInvariant());
					if (!text2.Contains("."))
					{
						text = text2 + "." + con.DatabaseDomainName.ToUpperInvariant() + text3;
					}
					else
					{
						text = text2 + text3;
					}
					flag2 = true;
				}
				OracleLpTableColumns oracleLpTableColumns = flag2 ? con.m_oracleConnectionImpl.m_pm.m_tableColumnsCache.Get(con.m_oracleConnectionImpl.ServiceName, oracleLpTable.m_schemaName, oracleLpTable.m_tableName + '@' + text) : con.m_oracleConnectionImpl.m_pm.m_tableColumnsCache.Get(con.m_oracleConnectionImpl.ServiceName, oracleLpTable.m_schemaName, oracleLpTable.m_tableName);
				if (oracleLpTableColumns == null)
				{
					string text4;
					string text5;
					if (num == 0)
					{
						text4 = string.Format(SQLMetaData.pSqlBlock11, num2++);
						stringBuilder.Append(text4);
						if (!flag2)
						{
							stringBuilder.Append(SQLMetaData.pSqlBlock2);
							text5 = string.Format(SQLMetaData.pSqlBlock11, num2++);
							stringBuilder.Append(text5);
						}
						else
						{
							text5 = string.Format(SQLMetaData.pSqlBlock11, num2++);
							stringBuilder.Append(SQLMetaData.pSqlBlock5);
							stringBuilder.Append(text5);
						}
					}
					else
					{
						stringBuilder.Append(SQLMetaData.pSqlBlock10);
						text4 = string.Format(SQLMetaData.pSqlBlock11, num2++);
						stringBuilder.Append(SQLMetaData.pSqlBlock12);
						stringBuilder.Append(text4);
						if (!flag2)
						{
							text5 = string.Format(SQLMetaData.pSqlBlock11, num2++);
							stringBuilder.Append(SQLMetaData.pSqlBlock13);
							stringBuilder.Append(text5);
						}
						else
						{
							text5 = string.Format(SQLMetaData.pSqlBlock11, num2++);
							stringBuilder.Append(SQLMetaData.pSqlBlock5);
							stringBuilder.Append(text5);
						}
					}
					stringBuilder.Append(SQLMetaData.pSqlBlock3a);
					if (flag2)
					{
						stringBuilder.Append(value);
					}
					stringBuilder.Append(SQLMetaData.pSqlBlock3b);
					if (flag2)
					{
						stringBuilder.Append(value);
					}
					stringBuilder.Append(SQLMetaData.pSqlBlock3c);
					string text6 = string.Format(SQLMetaData.pSqlBlock11, num2++);
					stringBuilder.Append(SQLMetaData.pSqlBlock14);
					stringBuilder.Append(SQLMetaData.pSqlBlock18);
					stringBuilder.Append(SQLMetaData.pSqlBlock7);
					stringBuilder.Append(text6);
					stringBuilder.Append(SQLMetaData.pSqlBlock15);
					stringBuilder.Append(SQLMetaData.pSqlBlock16);
					stringBuilder.Append(SQLMetaData.pSqlBlock7);
					stringBuilder.Append(text6);
					stringBuilder.Append(SQLMetaData.pSqlBlock6);
					if (flag2)
					{
						stringBuilder.Append(value);
					}
					stringBuilder.Append(SQLMetaData.pSqlBlock8);
					stringBuilder.Append(SQLMetaData.pSqlBlock17);
					OracleParameter oracleParameter = new OracleParameter(text4, OracleDbType.Varchar2, ParameterDirection.Input);
					oracleParameter.Value = oracleLpTable.m_tableName;
					oracleCommand.Parameters.Add(oracleParameter);
					OracleParameter oracleParameter2 = new OracleParameter(text5, OracleDbType.Varchar2, ParameterDirection.Input);
					oracleParameter2.Value = text;
					oracleCommand.Parameters.Add(oracleParameter2);
					OracleParameter param = new OracleParameter(text6, OracleDbType.RefCursor, ParameterDirection.Output);
					oracleCommand.Parameters.Add(param);
					flag = true;
					list.Add(null);
				}
				else
				{
					list.Add(oracleLpTableColumns);
				}
				num++;
			}
			if (flag)
			{
				stringBuilder.Append(SQLMetaData.pSqlBlock9);
				oracleCommand.Connection = con;
				oracleCommand.CommandText = stringBuilder.ToString();
				OracleDataReader oracleDataReader = null;
				try
				{
					oracleDataReader = oracleCommand.ExecuteReader();
					num = 0;
					foreach (OracleLpTable oracleLpTable2 in tables)
					{
						num++;
						if (list[num - 1] == null)
						{
							List<OracleLpColumn> list2 = new List<OracleLpColumn>();
							while (oracleDataReader.Read())
							{
								string @string = oracleDataReader.GetString(0);
								OracleLpColumn oracleLpColumn = new OracleLpColumn(@string);
								oracleLpColumn.m_schemaName = oracleDataReader.GetString(1);
								oracleLpColumn.m_tableName = oracleDataReader.GetString(2);
								string string2 = oracleDataReader.GetString(3);
								if (string2 != null)
								{
									oracleLpColumn.m_isHidden = string2.Equals("yes", StringComparison.InvariantCultureIgnoreCase);
								}
								if (ProviderConfig.m_bTraceLevelPrivate)
								{
									Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Error, new string[]
									{
										"Retrieved Column from data dictionary for {0}.{1} : {2} , Hidden: {3}",
										oracleLpColumn.m_schemaName,
										oracleLpColumn.m_tableName,
										@string,
										oracleLpColumn.m_isHidden.ToString()
									});
								}
								list2.Add(oracleLpColumn);
							}
							OracleLpTableColumns oracleLpTableColumns2 = new OracleLpTableColumns(oracleLpTable2, list2);
							list[num - 1] = oracleLpTableColumns2;
							bool flag2;
							if (string.IsNullOrEmpty(oracleLpTable2.m_dbLinkName))
							{
								flag2 = false;
							}
							else
							{
								string text7 = string.Empty;
								value = OracleMetadata.atSign + oracleLpTable2.m_dbLinkName;
								if (oracleLpTable2.m_dbLinkName.Contains("@"))
								{
									string[] array2 = oracleLpTable2.m_dbLinkName.Split(new char[]
									{
										'@'
									});
									text2 = array2[0];
									text7 = OracleMetadata.atSign + array2[1].Trim(OracleMetadata.double_quotes).ToUpperInvariant();
								}
								else
								{
									text2 = oracleLpTable2.m_dbLinkName;
								}
								text2 = ((text2[0] != '"') ? text2.ToUpperInvariant() : text2.Trim(OracleMetadata.double_quotes).ToUpperInvariant());
								if (!text2.Contains("."))
								{
									text = text2 + "." + con.DatabaseDomainName.ToUpperInvariant() + text7;
								}
								else
								{
									text = text2 + text7;
								}
								flag2 = true;
							}
							if (flag2)
							{
								con.m_oracleConnectionImpl.m_pm.m_tableColumnsCache.Put(con.m_oracleConnectionImpl.ServiceName, oracleLpTable2.m_schemaName, oracleLpTable2.m_tableName + '@' + text, oracleLpTableColumns2);
							}
							else
							{
								con.m_oracleConnectionImpl.m_pm.m_tableColumnsCache.Put(con.m_oracleConnectionImpl.ServiceName, oracleLpTable2.m_schemaName, oracleLpTable2.m_tableName, oracleLpTableColumns2);
							}
							oracleDataReader.NextResult();
						}
					}
				}
				finally
				{
					if (oracleDataReader != null)
					{
						oracleDataReader.Close();
					}
				}
			}
			return list;
		}

		// Token: 0x040009C4 RID: 2500
		private static char[] double_quotes = new char[]
		{
			'"'
		};

		// Token: 0x040009C5 RID: 2501
		private static string atSign = "@";
	}
}
