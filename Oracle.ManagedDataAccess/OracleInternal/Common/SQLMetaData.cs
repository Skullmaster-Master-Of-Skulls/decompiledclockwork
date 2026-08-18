using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using Oracle.SqlAndPlsqlParser.LocalParsing;
using OracleInternal.ServiceObjects;

namespace OracleInternal.Common
{
	// Token: 0x020000B9 RID: 185
	internal class SQLMetaData
	{
		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x06000733 RID: 1843 RVA: 0x0004267C File Offset: 0x0004087C
		internal bool IsInitialLongFetchSizeInChars
		{
			get
			{
				return this.HasLOBOrLongColumn && this.m_bIsInitialLongFetchSizeInChars;
			}
		}

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x06000734 RID: 1844 RVA: 0x00042690 File Offset: 0x00040890
		internal bool HasLOBColumns
		{
			get
			{
				if (!this.m_bHasLOBOrLongColumnChecked)
				{
					bool hasLOBOrLongColumn = this.HasLOBOrLongColumn;
				}
				return this.m_numOfCLOBCols + this.m_numOfBLOBCols > 0;
			}
		}

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x06000735 RID: 1845 RVA: 0x000426B4 File Offset: 0x000408B4
		internal bool HasBFILEColumn
		{
			get
			{
				if (!this.m_bHasBFILEColumnChecked)
				{
					if (this.m_columnDescribeInfo != null && this.m_columnDescribeInfo.Length > 0)
					{
						for (int i = 0; i < this.m_columnDescribeInfo.Length; i++)
						{
							if (114 == this.m_columnDescribeInfo[i].m_dataType)
							{
								this.m_numOfBFileColumns++;
								this.m_bHasBFILEColumn = true;
							}
						}
					}
					this.m_bHasBFILEColumnChecked = true;
				}
				return this.m_bHasBFILEColumn;
			}
		}

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x06000736 RID: 1846 RVA: 0x00042724 File Offset: 0x00040924
		internal bool HasLOBOrLongColumn
		{
			get
			{
				if (!this.m_bHasLOBOrLongColumnChecked)
				{
					if (this.m_columnDescribeInfo != null && this.m_columnDescribeInfo.Length > 0)
					{
						for (int i = 0; i < this.m_columnDescribeInfo.Length; i++)
						{
							if (112 == this.m_columnDescribeInfo[i].m_dataType)
							{
								this.m_numOfLOBColumns++;
								this.m_numOfCLOBCols++;
								this.m_bHasLOBOrLongColumn = true;
							}
							else if (113 == this.m_columnDescribeInfo[i].m_dataType)
							{
								this.m_numOfLOBColumns++;
								this.m_numOfBLOBCols++;
								this.m_bHasLOBOrLongColumn = true;
							}
							else if (8 == this.m_columnDescribeInfo[i].m_dataType || 24 == this.m_columnDescribeInfo[i].m_dataType)
							{
								this.m_numOfLONGColumns++;
								this.m_bHasLOBOrLongColumn = true;
								if (8 == this.m_columnDescribeInfo[i].m_dataType)
								{
									this.m_bIsInitialLongFetchSizeInChars = true;
								}
							}
						}
					}
					this.m_bHasLOBOrLongColumnChecked = true;
				}
				return this.m_bHasLOBOrLongColumn;
			}
		}

		// Token: 0x06000737 RID: 1847 RVA: 0x00042838 File Offset: 0x00040A38
		internal static bool GetPrimaryKey(OracleConnection conn, SQLMetaData sqlMetaData, int hiddenColumnCount, bool bSchemaTable)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			bool result;
			try
			{
				if (sqlMetaData == null)
				{
					result = false;
				}
				else if (sqlMetaData.bPkFetched)
				{
					result = true;
				}
				else if (sqlMetaData.m_noOfColumns == 0)
				{
					if (sqlMetaData.m_sqlMetaInfo == null)
					{
						sqlMetaData.m_sqlMetaInfo = SQLLocalParsePrimaryKeyInfo.Null;
					}
					result = false;
				}
				else
				{
					lock (sqlMetaData.m_syncPK)
					{
						try
						{
							if (sqlMetaData.bPkFetched)
							{
								return true;
							}
							if (!string.IsNullOrWhiteSpace(sqlMetaData.pCommandText) && conn.m_oracleConnectionImpl.m_cs.m_metadataPooling && conn.m_oracleConnectionImpl.m_pm.TryRetrievePrimaryKeyInfoFromCache(conn.m_oracleConnectionImpl.ServiceName, sqlMetaData.pCommandText, ref sqlMetaData))
							{
								return true;
							}
							if (!sqlMetaData.bStmtParsed)
							{
								SQLParser.GetSchemaMetaData(sqlMetaData, conn, conn.m_oracleConnectionImpl, hiddenColumnCount > 0);
							}
							short noOfColumns = sqlMetaData.m_noOfColumns;
							short num = (short)((int)sqlMetaData.m_noOfColumns - hiddenColumnCount);
							if (sqlMetaData.m_sqlMetaInfo == SQLLocalParsePrimaryKeyInfo.Null || sqlMetaData.m_sqlMetaInfo == null)
							{
								sqlMetaData.m_sqlMetaInfo = new SQLLocalParsePrimaryKeyInfo((int)noOfColumns);
							}
							SQLLocalParsePrimaryKeyInfo sqlMetaInfo = sqlMetaData.m_sqlMetaInfo;
							bool[] array = new bool[(int)noOfColumns];
							bool[] array2 = new bool[(int)noOfColumns];
							bool flag2 = false;
							bool flag3 = false;
							for (short num2 = 0; num2 < noOfColumns; num2 += 1)
							{
								ColumnLocalParsePrimaryKeyInfo columnLocalParsePrimaryKeyInfo = sqlMetaInfo.m_columnMetaInfo[(int)num2];
								if (columnLocalParsePrimaryKeyInfo.m_columnName != null && (columnLocalParsePrimaryKeyInfo.m_columnName == "ROWID" || columnLocalParsePrimaryKeyInfo.m_columnName == "\"ROWID\""))
								{
									sqlMetaInfo.m_columnMetaInfo[(int)num2].bIsUnique = true;
									array[(int)num2] = true;
									flag3 = true;
									if (num2 < num)
									{
										flag2 = true;
									}
								}
							}
							if (flag3 && !bSchemaTable)
							{
								for (int i = 0; i < (int)noOfColumns; i++)
								{
									sqlMetaInfo.m_columnMetaInfo[i].bIsKeyColumn = array[i];
								}
								sqlMetaInfo.bRowidPresent = true;
								return true;
							}
							if (sqlMetaInfo.m_tableName == null || sqlMetaInfo.m_tableName.Length == 0)
							{
								sqlMetaData.bPkFetched = (sqlMetaData.m_sqlMetaInfo.bPkFetched = true);
								return true;
							}
							StringBuilder stringBuilder = new StringBuilder(SQLMetaData.pSqlBlock1);
							stringBuilder.Append(":1");
							stringBuilder.Append(SQLMetaData.pSqlBlock2);
							if (sqlMetaInfo.m_schemaName != null && sqlMetaInfo.m_schemaName.Length != 0)
							{
								stringBuilder.Append(":2");
							}
							else
							{
								stringBuilder.Append("user");
							}
							stringBuilder.Append(SQLMetaData.pSqlBlock3a);
							stringBuilder.Append(SQLMetaData.pSqlBlock3b);
							stringBuilder.Append(SQLMetaData.pSqlBlock3c);
							stringBuilder.Append(SQLMetaData.pSqlBlock4);
							OracleCommand oracleCommand = conn.CreateCommand();
							oracleCommand.CommandText = stringBuilder.ToString();
							OracleParameter oracleParameter = new OracleParameter(":1", OracleDbType.Varchar2, ParameterDirection.Input);
							oracleParameter.Value = sqlMetaInfo.m_tableName;
							oracleCommand.Parameters.Add(oracleParameter);
							OracleParameter oracleParameter2 = null;
							if (sqlMetaInfo.m_schemaName != null && sqlMetaInfo.m_schemaName.Length != 0)
							{
								oracleParameter2 = new OracleParameter(":2", OracleDbType.Varchar2, ParameterDirection.Input);
								oracleParameter2.Value = sqlMetaInfo.m_schemaName;
								oracleCommand.Parameters.Add(oracleParameter2);
							}
							OracleParameter oracleParameter3 = new OracleParameter(":3", OracleDbType.RefCursor, ParameterDirection.Output);
							oracleCommand.Parameters.Add(oracleParameter3);
							try
							{
								oracleCommand.ExecuteNonQuery();
							}
							catch (Exception ex)
							{
								sqlMetaData.bPkFetched = (sqlMetaData.m_sqlMetaInfo.bPkFetched = true);
								oracleParameter.Dispose();
								if (oracleParameter2 != null)
								{
									oracleParameter2.Dispose();
								}
								oracleParameter3.Dispose();
								oracleCommand.Dispose();
								if (ProviderConfig.m_bTraceLevelPrivate)
								{
									Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Error, new string[]
									{
										"SQLMetaData::GetPrimaryKey() failed. -" + ex.Message
									});
								}
								return false;
							}
							string text = null;
							string text2 = null;
							string text3 = null;
							bool flag4 = false;
							OracleDataReader dataReader = (oracleParameter3.Value as OracleRefCursor).GetDataReader();
							if (dataReader.Read())
							{
								text = (dataReader[0] as string);
								text2 = (dataReader[1] as string);
								text3 = (dataReader[2] as string);
							}
							else
							{
								flag4 = true;
							}
							bool flag5 = false;
							bool flag6 = false;
							while (!flag4)
							{
								string text4 = text;
								string text5 = text2;
								string text6 = text3;
								bool flag7 = true;
								bool flag8 = true;
								bool flag9 = false;
								bool flag10 = false;
								int j;
								for (j = 0; j < (int)noOfColumns; j++)
								{
									ColumnLocalParsePrimaryKeyInfo columnLocalParsePrimaryKeyInfo2 = sqlMetaInfo.m_columnMetaInfo[j];
									int num3 = (columnLocalParsePrimaryKeyInfo2.m_columnName != null) ? columnLocalParsePrimaryKeyInfo2.m_columnName.Length : 0;
									if (num3 != 0)
									{
										bool flag11 = columnLocalParsePrimaryKeyInfo2.m_columnName[0] == '"';
										if (columnLocalParsePrimaryKeyInfo2.m_columnName.Length - (flag11 ? 2 : 0) == text4.Length && string.Compare(columnLocalParsePrimaryKeyInfo2.m_columnName, flag11 ? 1 : 0, text4, 0, text4.Length) == 0)
										{
											if (!sqlMetaData.m_columnDescribeInfo[j].m_isNullAllowed)
											{
												flag8 = false;
											}
											else
											{
												flag7 = false;
											}
											flag9 = true;
											break;
										}
									}
								}
								if (!flag9)
								{
									while (!flag4 && text2 == text5)
									{
										if (!(text3 == text6))
										{
											break;
										}
										if (dataReader.Read())
										{
											text = (dataReader[0] as string);
											text2 = (dataReader[1] as string);
											text3 = (dataReader[2] as string);
										}
										else
										{
											flag4 = true;
										}
									}
								}
								else
								{
									if (dataReader.Read())
									{
										text = (dataReader[0] as string);
										text2 = (dataReader[1] as string);
										text3 = (dataReader[2] as string);
									}
									else
									{
										flag4 = true;
									}
									if (flag4 || text5 != text2 || text6 != text3)
									{
										if (flag7)
										{
											if (!flag5)
											{
												array2[j] = true;
												flag10 = true;
											}
											sqlMetaInfo.m_columnMetaInfo[j].bIsUnique = true;
											for (j++; j < (int)noOfColumns; j++)
											{
												ColumnLocalParsePrimaryKeyInfo columnLocalParsePrimaryKeyInfo3 = sqlMetaInfo.m_columnMetaInfo[j];
												int num4 = (columnLocalParsePrimaryKeyInfo3.m_columnName != null) ? columnLocalParsePrimaryKeyInfo3.m_columnName.Length : 0;
												if (num4 != 0)
												{
													bool flag11 = columnLocalParsePrimaryKeyInfo3.m_columnName[0] == '"';
													if (columnLocalParsePrimaryKeyInfo3.m_columnName.Length - (flag11 ? 2 : 0) == text4.Length && string.Compare(columnLocalParsePrimaryKeyInfo3.m_columnName, flag11 ? 1 : 0, text4, 0, text4.Length) == 0 && !sqlMetaData.m_columnDescribeInfo[j].m_isNullAllowed)
													{
														if (!flag5)
														{
															array2[j] = true;
															flag10 = true;
														}
														sqlMetaInfo.m_columnMetaInfo[j].bIsUnique = true;
													}
												}
											}
											flag5 = true;
										}
									}
									else
									{
										array2[j] = true;
										for (j++; j < (int)noOfColumns; j++)
										{
											ColumnLocalParsePrimaryKeyInfo columnLocalParsePrimaryKeyInfo4 = sqlMetaInfo.m_columnMetaInfo[j];
											int num5 = (columnLocalParsePrimaryKeyInfo4.m_columnName != null) ? columnLocalParsePrimaryKeyInfo4.m_columnName.Length : 0;
											if (num5 != 0)
											{
												bool flag11 = columnLocalParsePrimaryKeyInfo4.m_columnName[0] == '"';
												if (columnLocalParsePrimaryKeyInfo4.m_columnName.Length - (flag11 ? 2 : 0) == text4.Length && string.Compare(columnLocalParsePrimaryKeyInfo4.m_columnName, flag11 ? 1 : 0, text4, 0, text4.Length) == 0)
												{
													if (!sqlMetaData.m_columnDescribeInfo[j].m_isNullAllowed)
													{
														flag8 = false;
													}
													else
													{
														flag7 = false;
													}
													array2[j] = true;
												}
											}
										}
										while (!flag4 && text2 == text5 && text3 == text6)
										{
											if (flag9)
											{
												flag9 = false;
												for (j = 0; j < (int)noOfColumns; j++)
												{
													ColumnLocalParsePrimaryKeyInfo columnLocalParsePrimaryKeyInfo5 = sqlMetaInfo.m_columnMetaInfo[j];
													int num6 = (columnLocalParsePrimaryKeyInfo5.m_columnName != null) ? columnLocalParsePrimaryKeyInfo5.m_columnName.Length : 0;
													if (num6 != 0)
													{
														bool flag11 = columnLocalParsePrimaryKeyInfo5.m_columnName[0] == '"';
														if (columnLocalParsePrimaryKeyInfo5.m_columnName.Length - (flag11 ? 2 : 0) == text.Length && string.Compare(columnLocalParsePrimaryKeyInfo5.m_columnName, flag11 ? 1 : 0, text, 0, text.Length) == 0)
														{
															if (!sqlMetaData.m_columnDescribeInfo[j].m_isNullAllowed)
															{
																flag8 = false;
															}
															else
															{
																flag7 = false;
															}
															array2[j] = true;
															flag9 = true;
														}
													}
												}
											}
											if (dataReader.Read())
											{
												text = (dataReader[0] as string);
												text2 = (dataReader[1] as string);
												text3 = (dataReader[2] as string);
											}
											else
											{
												flag4 = true;
											}
										}
										if (flag9)
										{
											if (!flag5 && flag7)
											{
												flag10 = true;
												flag5 = true;
											}
											if (!flag5 && !flag6 && !flag8)
											{
												flag10 = true;
												flag6 = true;
											}
										}
									}
									if (flag10)
									{
										Array.Copy(array2, array, (int)noOfColumns);
									}
									Array.Clear(array2, 0, (int)noOfColumns);
								}
							}
							oracleParameter.Dispose();
							if (oracleParameter2 != null)
							{
								oracleParameter2.Dispose();
							}
							oracleParameter3.Dispose();
							dataReader.Dispose();
							oracleCommand.Dispose();
							if (flag4)
							{
								for (int k = 0; k < (int)noOfColumns; k++)
								{
									sqlMetaInfo.m_columnMetaInfo[k].bIsKeyColumn = array[k];
								}
								if (flag5 || flag6 || flag2)
								{
									sqlMetaInfo.bPkPresent = true;
								}
								if (!flag5 && !flag6 && flag3)
								{
									sqlMetaInfo.bRowidPresent = true;
								}
								sqlMetaData.bPkFetched = (sqlMetaData.m_sqlMetaInfo.bPkFetched = true);
								return true;
							}
						}
						finally
						{
							if (sqlMetaData.bPkFetched && conn.m_oracleConnectionImpl.m_cs.m_metadataPooling)
							{
								conn.m_oracleConnectionImpl.m_pm.TryCacheLocalParsePrimaryKeyInfo(conn.m_oracleConnectionImpl.ServiceName, sqlMetaData.pCommandText, sqlMetaData);
							}
						}
					}
					result = false;
				}
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x06000738 RID: 1848 RVA: 0x00043240 File Offset: 0x00041440
		internal void CalculateRowSize()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				bool hasBFILEColumn = this.HasBFILEColumn;
				bool hasLOBOrLongColumn = this.HasLOBOrLongColumn;
				if ((hasBFILEColumn || hasLOBOrLongColumn) && this.m_maxRowSize == 0)
				{
					for (int i = 0; i < (int)this.m_noOfColumns; i++)
					{
						if (this.m_columnDescribeInfo[i].m_dataType != 112 && this.m_columnDescribeInfo[i].m_dataType != 113 && this.m_columnDescribeInfo[i].m_dataType != 8 && this.m_columnDescribeInfo[i].m_dataType != 24 && this.m_columnDescribeInfo[i].m_dataType != 114)
						{
							this.m_maxRowSize += this.m_columnDescribeInfo[i].m_maxLength;
						}
					}
				}
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
				}
			}
		}

		// Token: 0x0400098F RID: 2447
		internal const string IDENTIFIER_MAXSIZE = "128";

		// Token: 0x04000990 RID: 2448
		internal const string OBJ_TYPE_MAXSIZE = "23";

		// Token: 0x04000991 RID: 2449
		internal short m_noOfColumns;

		// Token: 0x04000992 RID: 2450
		internal int m_maxRowSize;

		// Token: 0x04000993 RID: 2451
		internal int m_numOfLOBColumns;

		// Token: 0x04000994 RID: 2452
		internal int m_numOfLONGColumns;

		// Token: 0x04000995 RID: 2453
		internal int m_numOfBFileColumns;

		// Token: 0x04000996 RID: 2454
		internal int m_numOfCLOBCols;

		// Token: 0x04000997 RID: 2455
		internal int m_numOfBLOBCols;

		// Token: 0x04000998 RID: 2456
		internal ColumnDescribeInfo[] m_columnDescribeInfo;

		// Token: 0x04000999 RID: 2457
		internal Type[] m_fieldTypes;

		// Token: 0x0400099A RID: 2458
		internal SQLLocalParsePrimaryKeyInfo m_sqlMetaInfo = SQLLocalParsePrimaryKeyInfo.Null;

		// Token: 0x0400099B RID: 2459
		internal bool bGotDescribeInfoFromDB;

		// Token: 0x0400099C RID: 2460
		internal bool bPkFetched;

		// Token: 0x0400099D RID: 2461
		internal bool bStmtParsed;

		// Token: 0x0400099E RID: 2462
		internal object m_syncPK = new object();

		// Token: 0x0400099F RID: 2463
		internal object m_syncLP = new object();

		// Token: 0x040009A0 RID: 2464
		internal string pCommandText;

		// Token: 0x040009A1 RID: 2465
		internal bool m_bHasLOBOrLongColumn;

		// Token: 0x040009A2 RID: 2466
		internal bool m_bHasLOBOrLongColumnChecked;

		// Token: 0x040009A3 RID: 2467
		internal bool m_bIsInitialLongFetchSizeInChars;

		// Token: 0x040009A4 RID: 2468
		internal IEnumerable<OracleLpStatement> parsedStmt;

		// Token: 0x040009A5 RID: 2469
		internal bool m_bHasBFILEColumn;

		// Token: 0x040009A6 RID: 2470
		internal bool m_bHasBFILEColumnChecked;

		// Token: 0x040009A7 RID: 2471
		internal static string pSqlBlock1 = "DECLARE tab_name\t  VARCHAR2(128); tab_owner  VARCHAR2(128); syn_name\t  VARCHAR2(128); syn_owner  VARCHAR2(128); db_link_name  VARCHAR2(128); col_name_qry VARCHAR2(512); obj_t      VARCHAR2(23); depth      NUMBER; max_depth  NUMBER; BEGIN depth     := 0; max_depth := 10; tab_name  := ";

		// Token: 0x040009A8 RID: 2472
		internal static string pSqlBlock2 = "; tab_owner := ";

		// Token: 0x040009A9 RID: 2473
		internal static string pSqlBlock3a = "; LOOP BEGIN select ot into obj_t FROM (select object_type ot from all_objects";

		// Token: 0x040009AA RID: 2474
		internal static string pSqlBlock3b = " where object_name = tab_name and owner = tab_owner) WHERE ROWNUM <= 1; IF (obj_t != 'SYNONYM') THEN EXIT; END IF; EXCEPTION WHEN NO_DATA_FOUND THEN obj_t := null; END; syn_name := tab_name; syn_owner:= tab_owner; select c1, c2, c3 into tab_name, tab_owner, db_link_name FROM (select table_name c1, COALESCE(table_owner, ' ') c2, COALESCE(db_link, ' ') c3 from all_synonyms";

		// Token: 0x040009AB RID: 2475
		internal static string pSqlBlock3c = " where synonym_name = syn_name and owner in (syn_owner, 'PUBLIC') order by decode(owner, 'PUBLIC', 2, 1)) WHERE ROWNUM <= 1; IF (tab_owner = ' ' AND db_link_name != ' ') THEN select username into tab_owner from all_db_links where DB_LINK = db_link_name; EXIT; END IF; IF (depth = max_depth) THEN RAISE NO_DATA_FOUND; END IF; depth := depth + 1; END LOOP; ";

		// Token: 0x040009AC RID: 2476
		internal static string pSqlBlock4 = "open :3 for SELECT c1, c2, c3 FROM (SELECT acc.column_name c1, acc.constraint_name c2, ac.constraint_type c3 FROM all_cons_columns acc, all_constraints ac WHERE (ac.constraint_type = 'P' OR ac.constraint_type = 'U') AND ac.table_name = tab_name AND ac.owner = tab_owner AND ac.table_name = acc.table_name AND ac.owner = acc.owner AND ac.constraint_name = acc.constraint_name UNION SELECT aic.column_name c1, ai.index_name c2, 'U' c3 FROM all_indexes ai, all_ind_columns aic WHERE ai.uniqueness = 'UNIQUE' AND ai.table_name = tab_name AND ai.table_owner= tab_owner AND ai.table_name = aic.table_name AND ai.table_owner = aic.table_owner AND ai.index_name = aic.index_name AND ai.owner = aic.index_owner) ORDER BY 3, 2, 1; END;";

		// Token: 0x040009AD RID: 2477
		internal static string pSqlBlock5 = "; select username into tab_owner from all_db_links where DB_LINK=";

		// Token: 0x040009AE RID: 2478
		internal static string pSqlBlock6 = " for select column_name, owner, table_name, hidden_column from all_tab_cols";

		// Token: 0x040009AF RID: 2479
		internal static string pSqlBlock7 = "open ";

		// Token: 0x040009B0 RID: 2480
		internal static string pSqlBlock8 = " where owner=tab_owner and table_name=tab_name;";

		// Token: 0x040009B1 RID: 2481
		internal static string pSqlBlock9 = "\nEND;";

		// Token: 0x040009B2 RID: 2482
		internal static string pSqlBlock10 = " syn_name := NULL; syn_owner := NULL; obj_t := NULL; depth := 0; max_depth := 10;";

		// Token: 0x040009B3 RID: 2483
		internal static string pSqlBlock11 = ":{0}";

		// Token: 0x040009B4 RID: 2484
		internal static string pSqlBlock12 = " tab_name  := ";

		// Token: 0x040009B5 RID: 2485
		internal static string pSqlBlock13 = "; tab_owner := ";

		// Token: 0x040009B6 RID: 2486
		internal static string pSqlBlock14 = " if (db_link_name != ' ') THEN ";

		// Token: 0x040009B7 RID: 2487
		internal static string pSqlBlock18 = " col_name_qry := 'select column_name, owner, table_name, hidden_column from all_tab_cols' || '@' || db_link_name || ' where table_name = ''' || tab_name || ''' AND owner = ''' || tab_owner || ''''; ";

		// Token: 0x040009B8 RID: 2488
		internal static string pSqlBlock15 = " for col_name_qry; ";

		// Token: 0x040009B9 RID: 2489
		internal static string pSqlBlock16 = " ELSE ";

		// Token: 0x040009BA RID: 2490
		internal static string pSqlBlock17 = " END IF; ";
	}
}
