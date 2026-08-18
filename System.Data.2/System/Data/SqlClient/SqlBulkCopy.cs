using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlTypes;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace System.Data.SqlClient
{
	// Token: 0x020001A9 RID: 425
	public sealed class SqlBulkCopy : IDisposable
	{
		// Token: 0x060018AC RID: 6316 RVA: 0x000ADCDC File Offset: 0x000AD0DC
		public SqlBulkCopy(SqlConnection connection)
		{
			if (connection == null)
			{
				throw ADP.ArgumentNull("connection");
			}
			this._connection = connection;
			this._columnMappings = new SqlBulkCopyColumnMappingCollection();
		}

		// Token: 0x060018AD RID: 6317 RVA: 0x000ADD28 File Offset: 0x000AD128
		public SqlBulkCopy(SqlConnection connection, SqlBulkCopyOptions copyOptions, SqlTransaction externalTransaction) : this(connection)
		{
			this._copyOptions = copyOptions;
			if (externalTransaction != null && this.IsCopyOption(SqlBulkCopyOptions.UseInternalTransaction))
			{
				throw SQL.BulkLoadConflictingTransactionOption();
			}
			if (!this.IsCopyOption(SqlBulkCopyOptions.UseInternalTransaction))
			{
				this._externalTransaction = externalTransaction;
			}
		}

		// Token: 0x060018AE RID: 6318 RVA: 0x000ADD68 File Offset: 0x000AD168
		public SqlBulkCopy(string connectionString) : this(new SqlConnection(connectionString))
		{
			if (connectionString == null)
			{
				throw ADP.ArgumentNull("connectionString");
			}
			this._connection = new SqlConnection(connectionString);
			this._columnMappings = new SqlBulkCopyColumnMappingCollection();
			this._ownConnection = true;
		}

		// Token: 0x060018AF RID: 6319 RVA: 0x000ADDB0 File Offset: 0x000AD1B0
		public SqlBulkCopy(string connectionString, SqlBulkCopyOptions copyOptions) : this(connectionString)
		{
			this._copyOptions = copyOptions;
		}

		// Token: 0x1700037A RID: 890
		// (get) Token: 0x060018B0 RID: 6320 RVA: 0x000ADDCC File Offset: 0x000AD1CC
		// (set) Token: 0x060018B1 RID: 6321 RVA: 0x000ADDE0 File Offset: 0x000AD1E0
		public int BatchSize
		{
			get
			{
				return this._batchSize;
			}
			set
			{
				if (value >= 0)
				{
					this._batchSize = value;
					return;
				}
				throw ADP.ArgumentOutOfRange("BatchSize");
			}
		}

		// Token: 0x1700037B RID: 891
		// (get) Token: 0x060018B2 RID: 6322 RVA: 0x000ADE04 File Offset: 0x000AD204
		// (set) Token: 0x060018B3 RID: 6323 RVA: 0x000ADE18 File Offset: 0x000AD218
		public int BulkCopyTimeout
		{
			get
			{
				return this._timeout;
			}
			set
			{
				if (value < 0)
				{
					throw SQL.BulkLoadInvalidTimeout(value);
				}
				this._timeout = value;
			}
		}

		// Token: 0x1700037C RID: 892
		// (get) Token: 0x060018B4 RID: 6324 RVA: 0x000ADE38 File Offset: 0x000AD238
		// (set) Token: 0x060018B5 RID: 6325 RVA: 0x000ADE4C File Offset: 0x000AD24C
		public bool EnableStreaming
		{
			get
			{
				return this._enableStreaming;
			}
			set
			{
				this._enableStreaming = value;
			}
		}

		// Token: 0x1700037D RID: 893
		// (get) Token: 0x060018B6 RID: 6326 RVA: 0x000ADE60 File Offset: 0x000AD260
		public SqlBulkCopyColumnMappingCollection ColumnMappings
		{
			get
			{
				return this._columnMappings;
			}
		}

		// Token: 0x1700037E RID: 894
		// (get) Token: 0x060018B7 RID: 6327 RVA: 0x000ADE74 File Offset: 0x000AD274
		// (set) Token: 0x060018B8 RID: 6328 RVA: 0x000ADE88 File Offset: 0x000AD288
		public string DestinationTableName
		{
			get
			{
				return this._destinationTableName;
			}
			set
			{
				if (value == null)
				{
					throw ADP.ArgumentNull("DestinationTableName");
				}
				if (value.Length == 0)
				{
					throw ADP.ArgumentOutOfRange("DestinationTableName");
				}
				this._destinationTableName = value;
			}
		}

		// Token: 0x1700037F RID: 895
		// (get) Token: 0x060018B9 RID: 6329 RVA: 0x000ADEC0 File Offset: 0x000AD2C0
		// (set) Token: 0x060018BA RID: 6330 RVA: 0x000ADED4 File Offset: 0x000AD2D4
		public int NotifyAfter
		{
			get
			{
				return this._notifyAfter;
			}
			set
			{
				if (value >= 0)
				{
					this._notifyAfter = value;
					return;
				}
				throw ADP.ArgumentOutOfRange("NotifyAfter");
			}
		}

		// Token: 0x17000380 RID: 896
		// (get) Token: 0x060018BB RID: 6331 RVA: 0x000ADEF8 File Offset: 0x000AD2F8
		internal int ObjectID
		{
			get
			{
				return this._objectID;
			}
		}

		// Token: 0x14000020 RID: 32
		// (add) Token: 0x060018BC RID: 6332 RVA: 0x000ADF0C File Offset: 0x000AD30C
		// (remove) Token: 0x060018BD RID: 6333 RVA: 0x000ADF30 File Offset: 0x000AD330
		public event SqlRowsCopiedEventHandler SqlRowsCopied
		{
			add
			{
				this._rowsCopiedEventHandler = (SqlRowsCopiedEventHandler)Delegate.Combine(this._rowsCopiedEventHandler, value);
			}
			remove
			{
				this._rowsCopiedEventHandler = (SqlRowsCopiedEventHandler)Delegate.Remove(this._rowsCopiedEventHandler, value);
			}
		}

		// Token: 0x17000381 RID: 897
		// (get) Token: 0x060018BE RID: 6334 RVA: 0x000ADF54 File Offset: 0x000AD354
		internal SqlStatistics Statistics
		{
			get
			{
				if (this._connection != null && this._connection.StatisticsEnabled)
				{
					return this._connection.Statistics;
				}
				return null;
			}
		}

		// Token: 0x060018BF RID: 6335 RVA: 0x000ADF84 File Offset: 0x000AD384
		void IDisposable.Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060018C0 RID: 6336 RVA: 0x000ADFA0 File Offset: 0x000AD3A0
		private bool IsCopyOption(SqlBulkCopyOptions copyOption)
		{
			return (this._copyOptions & copyOption) == copyOption;
		}

		// Token: 0x060018C1 RID: 6337 RVA: 0x000ADFB8 File Offset: 0x000AD3B8
		private string CreateInitialQuery()
		{
			string[] array;
			try
			{
				array = MultipartIdentifier.ParseMultipartIdentifier(this.DestinationTableName, "[\"", "]\"", "SQL_BulkCopyDestinationTableName", true);
			}
			catch (Exception inner)
			{
				throw SQL.BulkLoadInvalidDestinationTable(this.DestinationTableName, inner);
			}
			if (ADP.IsEmpty(array[3]))
			{
				throw SQL.BulkLoadInvalidDestinationTable(this.DestinationTableName, null);
			}
			string text = "select @@trancount; SET FMTONLY ON select * from " + this.DestinationTableName + " SET FMTONLY OFF ";
			if (this._connection.IsShiloh)
			{
				string text2;
				if (this._connection.IsKatmaiOrNewer)
				{
					text2 = "sp_tablecollations_100";
				}
				else if (this._connection.IsYukonOrNewer)
				{
					text2 = "sp_tablecollations_90";
				}
				else
				{
					text2 = "sp_tablecollations";
				}
				string text3 = array[3];
				bool flag = text3.Length > 0 && '#' == text3[0];
				if (!ADP.IsEmpty(text3))
				{
					text3 = SqlServerEscapeHelper.EscapeStringAsLiteral(text3);
					text3 = SqlServerEscapeHelper.EscapeIdentifier(text3);
				}
				string text4 = array[2];
				if (!ADP.IsEmpty(text4))
				{
					text4 = SqlServerEscapeHelper.EscapeStringAsLiteral(text4);
					text4 = SqlServerEscapeHelper.EscapeIdentifier(text4);
				}
				string text5 = array[1];
				if (flag && ADP.IsEmpty(text5))
				{
					text += string.Format(null, "exec tempdb..{0} N'{1}.{2}'", new object[]
					{
						text2,
						text4,
						text3
					});
				}
				else
				{
					if (!ADP.IsEmpty(text5))
					{
						text5 = SqlServerEscapeHelper.EscapeIdentifier(text5);
					}
					text += string.Format(null, "exec {0}..{1} N'{2}.{3}'", new object[]
					{
						text5,
						text2,
						text4,
						text3
					});
				}
			}
			return text;
		}

		// Token: 0x060018C2 RID: 6338 RVA: 0x000AE144 File Offset: 0x000AD544
		private Task<BulkCopySimpleResultSet> CreateAndExecuteInitialQueryAsync(out BulkCopySimpleResultSet result)
		{
			string text = this.CreateInitialQuery();
			Bid.Trace("<sc.SqlBulkCopy.CreateAndExecuteInitialQueryAsync|INFO> Initial Query: '%ls' \n", text);
			Bid.CorrelationTrace("<sc.SqlBulkCopy.CreateAndExecuteInitialQueryAsync|Info|Correlation> ObjectID%d#, ActivityID %ls\n", this.ObjectID);
			Task task = this._parser.TdsExecuteSQLBatch(text, this.BulkCopyTimeout, null, this._stateObj, !this._isAsyncBulkCopy, true, null);
			if (task == null)
			{
				result = new BulkCopySimpleResultSet();
				this.RunParser(result);
				return null;
			}
			result = null;
			return task.ContinueWith<BulkCopySimpleResultSet>(delegate(Task t)
			{
				if (t.IsFaulted)
				{
					throw t.Exception.InnerException;
				}
				BulkCopySimpleResultSet bulkCopySimpleResultSet = new BulkCopySimpleResultSet();
				this.RunParserReliably(bulkCopySimpleResultSet);
				return bulkCopySimpleResultSet;
			}, TaskScheduler.Default);
		}

		// Token: 0x060018C3 RID: 6339 RVA: 0x000AE1C8 File Offset: 0x000AD5C8
		private string AnalyzeTargetAndCreateUpdateBulkCommand(BulkCopySimpleResultSet internalResults)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (this._connection.IsShiloh && internalResults[2].Count == 0)
			{
				throw SQL.BulkLoadNoCollation();
			}
			stringBuilder.AppendFormat("insert bulk {0} (", this.DestinationTableName);
			int num = 0;
			int num2 = 0;
			bool flag;
			if (this._parser.IsYukonOrNewer)
			{
				flag = this._connection.HasLocalTransaction;
			}
			else
			{
				flag = (bool)(0 < (SqlInt32)internalResults[0][0][0]);
			}
			if (flag && this._externalTransaction == null && this._internalTransaction == null && this._connection.Parser != null && this._connection.Parser.CurrentTransaction != null && this._connection.Parser.CurrentTransaction.IsLocal)
			{
				throw SQL.BulkLoadExistingTransaction();
			}
			_SqlMetaDataSet metaData = internalResults[1].MetaData;
			this._sortedColumnMappings = new List<_ColumnMapping>(metaData.Length);
			for (int i = 0; i < metaData.Length; i++)
			{
				_SqlMetaData sqlMetaData = metaData[i];
				bool flag2 = false;
				if (sqlMetaData.type == SqlDbType.Timestamp || (sqlMetaData.isIdentity && !this.IsCopyOption(SqlBulkCopyOptions.KeepIdentity)))
				{
					metaData[i] = null;
					flag2 = true;
				}
				int j = 0;
				while (j < this._localColumnMappings.Count)
				{
					if (this._localColumnMappings[j]._destinationColumnOrdinal == sqlMetaData.ordinal || this.UnquotedName(this._localColumnMappings[j]._destinationColumnName) == sqlMetaData.column)
					{
						if (flag2)
						{
							num2++;
							break;
						}
						this._sortedColumnMappings.Add(new _ColumnMapping(this._localColumnMappings[j]._internalSourceColumnOrdinal, sqlMetaData));
						num++;
						if (num > 1)
						{
							stringBuilder.Append(", ");
						}
						if (sqlMetaData.type == SqlDbType.Variant)
						{
							this.AppendColumnNameAndTypeName(stringBuilder, sqlMetaData.column, "sql_variant");
						}
						else if (sqlMetaData.type == SqlDbType.Udt)
						{
							this.AppendColumnNameAndTypeName(stringBuilder, sqlMetaData.column, "varbinary");
						}
						else
						{
							this.AppendColumnNameAndTypeName(stringBuilder, sqlMetaData.column, typeof(SqlDbType).GetEnumName(sqlMetaData.type));
						}
						byte nullableType = sqlMetaData.metaType.NullableType;
						if (nullableType <= 106)
						{
							if (nullableType - 41 > 2)
							{
								if (nullableType != 106)
								{
									goto IL_2FE;
								}
								goto IL_261;
							}
							else
							{
								stringBuilder.AppendFormat(null, "({0})", new object[]
								{
									sqlMetaData.scale
								});
							}
						}
						else
						{
							if (nullableType == 108)
							{
								goto IL_261;
							}
							if (nullableType != 240)
							{
								goto IL_2FE;
							}
							if (sqlMetaData.IsLargeUdt)
							{
								stringBuilder.Append("(max)");
							}
							else
							{
								int length = sqlMetaData.length;
								stringBuilder.AppendFormat(null, "({0})", new object[]
								{
									length
								});
							}
						}
						IL_392:
						if (this._connection.IsShiloh)
						{
							Result result = internalResults[2];
							object obj = result[i][3];
							SqlDbType type = sqlMetaData.type;
							if (type <= SqlDbType.NVarChar)
							{
								if (type != SqlDbType.Char && type - SqlDbType.NChar > 2)
								{
									goto IL_3EA;
								}
								goto IL_3E5;
							}
							else
							{
								if (type == SqlDbType.Text || type == SqlDbType.VarChar)
								{
									goto IL_3E5;
								}
								goto IL_3EA;
							}
							IL_3ED:
							bool flag3;
							if (obj == null || !flag3)
							{
								break;
							}
							SqlString sqlString = (SqlString)obj;
							if (sqlString.IsNull)
							{
								break;
							}
							stringBuilder.Append(" COLLATE " + sqlString.Value);
							if (this._SqlDataReaderRowSource == null || sqlMetaData.collation == null)
							{
								break;
							}
							int internalSourceColumnOrdinal = this._localColumnMappings[j]._internalSourceColumnOrdinal;
							int lcid = sqlMetaData.collation.LCID;
							int localeId = this._SqlDataReaderRowSource.GetLocaleId(internalSourceColumnOrdinal);
							if (localeId != lcid)
							{
								throw SQL.BulkLoadLcidMismatch(localeId, this._SqlDataReaderRowSource.GetName(internalSourceColumnOrdinal), lcid, sqlMetaData.column);
							}
							break;
							IL_3EA:
							flag3 = false;
							goto IL_3ED;
							IL_3E5:
							flag3 = true;
							goto IL_3ED;
						}
						break;
						IL_261:
						stringBuilder.AppendFormat(null, "({0},{1})", new object[]
						{
							sqlMetaData.precision,
							sqlMetaData.scale
						});
						goto IL_392;
						IL_2FE:
						if (!sqlMetaData.metaType.IsFixed && !sqlMetaData.metaType.IsLong)
						{
							int num3 = sqlMetaData.length;
							byte nullableType2 = sqlMetaData.metaType.NullableType;
							if (nullableType2 == 99 || nullableType2 == 231 || nullableType2 == 239)
							{
								num3 /= 2;
							}
							stringBuilder.AppendFormat(null, "({0})", new object[]
							{
								num3
							});
							goto IL_392;
						}
						if (sqlMetaData.metaType.IsPlp && sqlMetaData.metaType.SqlDbType != SqlDbType.Xml)
						{
							stringBuilder.Append("(max)");
							goto IL_392;
						}
						goto IL_392;
					}
					else
					{
						j++;
					}
				}
				if (j == this._localColumnMappings.Count)
				{
					metaData[i] = null;
				}
			}
			if (num + num2 != this._localColumnMappings.Count)
			{
				throw SQL.BulkLoadNonMatchingColumnMapping();
			}
			stringBuilder.Append(")");
			if ((this._copyOptions & (SqlBulkCopyOptions.CheckConstraints | SqlBulkCopyOptions.TableLock | SqlBulkCopyOptions.KeepNulls | SqlBulkCopyOptions.FireTriggers | SqlBulkCopyOptions.AllowEncryptedValueModifications)) != SqlBulkCopyOptions.Default)
			{
				bool flag4 = false;
				stringBuilder.Append(" with (");
				if (this.IsCopyOption(SqlBulkCopyOptions.KeepNulls))
				{
					stringBuilder.Append("KEEP_NULLS");
					flag4 = true;
				}
				if (this.IsCopyOption(SqlBulkCopyOptions.TableLock))
				{
					stringBuilder.Append((flag4 ? ", " : "") + "TABLOCK");
					flag4 = true;
				}
				if (this.IsCopyOption(SqlBulkCopyOptions.CheckConstraints))
				{
					stringBuilder.Append((flag4 ? ", " : "") + "CHECK_CONSTRAINTS");
					flag4 = true;
				}
				if (this.IsCopyOption(SqlBulkCopyOptions.FireTriggers))
				{
					stringBuilder.Append((flag4 ? ", " : "") + "FIRE_TRIGGERS");
					flag4 = true;
				}
				if (this.IsCopyOption(SqlBulkCopyOptions.AllowEncryptedValueModifications))
				{
					stringBuilder.Append((flag4 ? ", " : "") + "ALLOW_ENCRYPTED_VALUE_MODIFICATIONS");
				}
				stringBuilder.Append(")");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060018C4 RID: 6340 RVA: 0x000AE7B8 File Offset: 0x000ADBB8
		private Task SubmitUpdateBulkCommand(string TDSCommand)
		{
			Bid.CorrelationTrace("<sc.SqlBulkCopy.SubmitUpdateBulkCommand|Info|Correlation> ObjectID%d#, ActivityID %ls\n", this.ObjectID);
			Task task = this._parser.TdsExecuteSQLBatch(TDSCommand, this.BulkCopyTimeout, null, this._stateObj, !this._isAsyncBulkCopy, true, null);
			if (task == null)
			{
				this.RunParser(null);
				return null;
			}
			return task.ContinueWith(delegate(Task t)
			{
				if (t.IsFaulted)
				{
					throw t.Exception.InnerException;
				}
				this.RunParserReliably(null);
			}, TaskScheduler.Default);
		}

		// Token: 0x060018C5 RID: 6341 RVA: 0x000AE820 File Offset: 0x000ADC20
		private void WriteMetaData(BulkCopySimpleResultSet internalResults)
		{
			this._stateObj.SetTimeoutSeconds(this.BulkCopyTimeout);
			_SqlMetaDataSet metaData = internalResults[1].MetaData;
			this._stateObj._outputMessageType = 7;
			this._parser.WriteBulkCopyMetaData(metaData, this._sortedColumnMappings.Count, this._stateObj);
		}

		// Token: 0x060018C6 RID: 6342 RVA: 0x000AE874 File Offset: 0x000ADC74
		public void Close()
		{
			if (this._insideRowsCopiedEvent)
			{
				throw SQL.InvalidOperationInsideEvent();
			}
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060018C7 RID: 6343 RVA: 0x000AE89C File Offset: 0x000ADC9C
		private void Dispose(bool disposing)
		{
			if (disposing)
			{
				this._columnMappings = null;
				this._parser = null;
				try
				{
					if (this._internalTransaction != null)
					{
						this._internalTransaction.Rollback();
						this._internalTransaction.Dispose();
						this._internalTransaction = null;
					}
				}
				catch (Exception e)
				{
					if (!ADP.IsCatchableExceptionType(e))
					{
						throw;
					}
					ADP.TraceExceptionWithoutRethrow(e);
				}
				finally
				{
					if (this._connection != null)
					{
						if (this._ownConnection)
						{
							this._connection.Dispose();
						}
						this._connection = null;
					}
				}
			}
		}

		// Token: 0x060018C8 RID: 6344 RVA: 0x000AE950 File Offset: 0x000ADD50
		private object GetValueFromSourceRow(int destRowIndex, out bool isSqlType, out bool isDataFeed, out bool isNull)
		{
			_SqlMetaData metadata = this._sortedColumnMappings[destRowIndex]._metadata;
			int sourceColumnOrdinal = this._sortedColumnMappings[destRowIndex]._sourceColumnOrdinal;
			switch (this._rowSourceType)
			{
			case SqlBulkCopy.ValueSourceType.IDataReader:
			case SqlBulkCopy.ValueSourceType.DbDataReader:
				if (this._currentRowMetadata[destRowIndex].IsDataFeed)
				{
					if (this._DbDataReaderRowSource.IsDBNull(sourceColumnOrdinal))
					{
						isSqlType = false;
						isDataFeed = false;
						isNull = true;
						return DBNull.Value;
					}
					isSqlType = false;
					isDataFeed = true;
					isNull = false;
					switch (this._currentRowMetadata[destRowIndex].Method)
					{
					case SqlBulkCopy.ValueMethod.DataFeedStream:
						return new StreamDataFeed(this._DbDataReaderRowSource.GetStream(sourceColumnOrdinal));
					case SqlBulkCopy.ValueMethod.DataFeedText:
						return new TextDataFeed(this._DbDataReaderRowSource.GetTextReader(sourceColumnOrdinal));
					case SqlBulkCopy.ValueMethod.DataFeedXml:
						return new XmlDataFeed(this._SqlDataReaderRowSource.GetXmlReader(sourceColumnOrdinal));
					default:
					{
						isDataFeed = false;
						object value = this._DbDataReaderRowSource.GetValue(sourceColumnOrdinal);
						ADP.IsNullOrSqlType(value, out isNull, out isSqlType);
						return value;
					}
					}
				}
				else if (this._SqlDataReaderRowSource != null)
				{
					if (this._currentRowMetadata[destRowIndex].IsSqlType)
					{
						isSqlType = true;
						isDataFeed = false;
						INullable nullable;
						switch (this._currentRowMetadata[destRowIndex].Method)
						{
						case SqlBulkCopy.ValueMethod.SqlTypeSqlDecimal:
							nullable = this._SqlDataReaderRowSource.GetSqlDecimal(sourceColumnOrdinal);
							break;
						case SqlBulkCopy.ValueMethod.SqlTypeSqlDouble:
							nullable = new SqlDecimal(this._SqlDataReaderRowSource.GetSqlDouble(sourceColumnOrdinal).Value);
							break;
						case SqlBulkCopy.ValueMethod.SqlTypeSqlSingle:
							nullable = new SqlDecimal((double)this._SqlDataReaderRowSource.GetSqlSingle(sourceColumnOrdinal).Value);
							break;
						default:
							nullable = (INullable)this._SqlDataReaderRowSource.GetSqlValue(sourceColumnOrdinal);
							break;
						}
						isNull = nullable.IsNull;
						return nullable;
					}
					isSqlType = false;
					isDataFeed = false;
					object value2 = this._SqlDataReaderRowSource.GetValue(sourceColumnOrdinal);
					isNull = (value2 == null || value2 == DBNull.Value);
					if (!isNull && metadata.type == SqlDbType.Udt)
					{
						INullable nullable2 = value2 as INullable;
						isNull = (nullable2 != null && nullable2.IsNull);
					}
					return value2;
				}
				else
				{
					isDataFeed = false;
					IDataReader dataReader = (IDataReader)this._rowSource;
					if (this._enableStreaming && this._SqlDataReaderRowSource == null && dataReader.IsDBNull(sourceColumnOrdinal))
					{
						isSqlType = false;
						isNull = true;
						return DBNull.Value;
					}
					object value3 = dataReader.GetValue(sourceColumnOrdinal);
					ADP.IsNullOrSqlType(value3, out isNull, out isSqlType);
					return value3;
				}
				break;
			case SqlBulkCopy.ValueSourceType.DataTable:
			case SqlBulkCopy.ValueSourceType.RowArray:
			{
				isDataFeed = false;
				object obj = this._currentRow[sourceColumnOrdinal];
				ADP.IsNullOrSqlType(obj, out isNull, out isSqlType);
				if (!isNull && this._currentRowMetadata[destRowIndex].IsSqlType)
				{
					switch (this._currentRowMetadata[destRowIndex].Method)
					{
					case SqlBulkCopy.ValueMethod.SqlTypeSqlDecimal:
						if (isSqlType)
						{
							return (SqlDecimal)obj;
						}
						isSqlType = true;
						return new SqlDecimal((decimal)obj);
					case SqlBulkCopy.ValueMethod.SqlTypeSqlDouble:
					{
						if (isSqlType)
						{
							return new SqlDecimal(((SqlDouble)obj).Value);
						}
						double num = (double)obj;
						if (!double.IsNaN(num))
						{
							isSqlType = true;
							return new SqlDecimal(num);
						}
						break;
					}
					case SqlBulkCopy.ValueMethod.SqlTypeSqlSingle:
					{
						if (isSqlType)
						{
							return new SqlDecimal((double)((SqlSingle)obj).Value);
						}
						float num2 = (float)obj;
						if (!float.IsNaN(num2))
						{
							isSqlType = true;
							return new SqlDecimal((double)num2);
						}
						break;
					}
					}
				}
				return obj;
			}
			default:
				throw ADP.NotSupported();
			}
		}

		// Token: 0x060018C9 RID: 6345 RVA: 0x000AECDC File Offset: 0x000AE0DC
		private Task ReadFromRowSourceAsync(CancellationToken cts)
		{
			if (this._isAsyncBulkCopy && this._DbDataReaderRowSource != null)
			{
				return this._DbDataReaderRowSource.ReadAsync(cts).ContinueWith<Task<bool>>(delegate(Task<bool> t)
				{
					if (t.Status == TaskStatus.RanToCompletion)
					{
						this._hasMoreRowToCopy = t.Result;
					}
					return t;
				}, TaskScheduler.Default).Unwrap<bool>();
			}
			this._hasMoreRowToCopy = false;
			try
			{
				this._hasMoreRowToCopy = this.ReadFromRowSource();
			}
			catch (Exception exception)
			{
				if (this._isAsyncBulkCopy)
				{
					TaskCompletionSource<bool> taskCompletionSource = new TaskCompletionSource<bool>();
					taskCompletionSource.SetException(exception);
					return taskCompletionSource.Task;
				}
				throw;
			}
			return null;
		}

		// Token: 0x060018CA RID: 6346 RVA: 0x000AED78 File Offset: 0x000AE178
		private bool ReadFromRowSource()
		{
			switch (this._rowSourceType)
			{
			case SqlBulkCopy.ValueSourceType.IDataReader:
			case SqlBulkCopy.ValueSourceType.DbDataReader:
				return ((IDataReader)this._rowSource).Read();
			case SqlBulkCopy.ValueSourceType.DataTable:
			case SqlBulkCopy.ValueSourceType.RowArray:
				while (this._rowEnumerator.MoveNext())
				{
					this._currentRow = (DataRow)this._rowEnumerator.Current;
					if ((this._currentRow.RowState & this._rowStateToSkip) == (DataRowState)0)
					{
						this._currentRowLength = this._currentRow.ItemArray.Length;
						return true;
					}
				}
				return false;
			default:
				throw ADP.NotSupported();
			}
		}

		// Token: 0x060018CB RID: 6347 RVA: 0x000AEE0C File Offset: 0x000AE20C
		private SqlBulkCopy.SourceColumnMetadata GetColumnMetadata(int ordinal)
		{
			int sourceColumnOrdinal = this._sortedColumnMappings[ordinal]._sourceColumnOrdinal;
			_SqlMetaData metadata = this._sortedColumnMappings[ordinal]._metadata;
			bool isDataFeed;
			bool isSqlType;
			SqlBulkCopy.ValueMethod method;
			if ((this._SqlDataReaderRowSource != null || this._dataTableSource != null) && (metadata.metaType.NullableType == 106 || metadata.metaType.NullableType == 108))
			{
				isDataFeed = false;
				Type right;
				switch (this._rowSourceType)
				{
				case SqlBulkCopy.ValueSourceType.IDataReader:
				case SqlBulkCopy.ValueSourceType.DbDataReader:
					right = this._SqlDataReaderRowSource.GetFieldType(sourceColumnOrdinal);
					break;
				case SqlBulkCopy.ValueSourceType.DataTable:
				case SqlBulkCopy.ValueSourceType.RowArray:
					right = this._dataTableSource.Columns[sourceColumnOrdinal].DataType;
					break;
				default:
					right = null;
					break;
				}
				if (typeof(SqlDecimal) == right || typeof(decimal) == right)
				{
					isSqlType = true;
					method = SqlBulkCopy.ValueMethod.SqlTypeSqlDecimal;
				}
				else if (typeof(SqlDouble) == right || typeof(double) == right)
				{
					isSqlType = true;
					method = SqlBulkCopy.ValueMethod.SqlTypeSqlDouble;
				}
				else if (typeof(SqlSingle) == right || typeof(float) == right)
				{
					isSqlType = true;
					method = SqlBulkCopy.ValueMethod.SqlTypeSqlSingle;
				}
				else
				{
					isSqlType = false;
					method = SqlBulkCopy.ValueMethod.GetValue;
				}
			}
			else if (this._enableStreaming && metadata.length == 2147483647 && !this._rowSourceIsSqlDataReaderSmi)
			{
				isSqlType = false;
				if (this._SqlDataReaderRowSource != null)
				{
					MetaType metaType = this._SqlDataReaderRowSource.MetaData[sourceColumnOrdinal].metaType;
					if (metadata.type == SqlDbType.VarBinary && metaType.IsBinType && metaType.SqlDbType != SqlDbType.Timestamp && this._SqlDataReaderRowSource.IsCommandBehavior(CommandBehavior.SequentialAccess))
					{
						isDataFeed = true;
						method = SqlBulkCopy.ValueMethod.DataFeedStream;
					}
					else if ((metadata.type == SqlDbType.VarChar || metadata.type == SqlDbType.NVarChar) && metaType.IsCharType && metaType.SqlDbType != SqlDbType.Xml)
					{
						isDataFeed = true;
						method = SqlBulkCopy.ValueMethod.DataFeedText;
					}
					else if (metadata.type == SqlDbType.Xml && metaType.SqlDbType == SqlDbType.Xml)
					{
						isDataFeed = true;
						method = SqlBulkCopy.ValueMethod.DataFeedXml;
					}
					else
					{
						isDataFeed = false;
						method = SqlBulkCopy.ValueMethod.GetValue;
					}
				}
				else if (this._DbDataReaderRowSource != null)
				{
					if (metadata.type == SqlDbType.VarBinary)
					{
						isDataFeed = true;
						method = SqlBulkCopy.ValueMethod.DataFeedStream;
					}
					else if (metadata.type == SqlDbType.VarChar || metadata.type == SqlDbType.NVarChar)
					{
						isDataFeed = true;
						method = SqlBulkCopy.ValueMethod.DataFeedText;
					}
					else
					{
						isDataFeed = false;
						method = SqlBulkCopy.ValueMethod.GetValue;
					}
				}
				else
				{
					isDataFeed = false;
					method = SqlBulkCopy.ValueMethod.GetValue;
				}
			}
			else
			{
				isSqlType = false;
				isDataFeed = false;
				method = SqlBulkCopy.ValueMethod.GetValue;
			}
			return new SqlBulkCopy.SourceColumnMetadata(method, isSqlType, isDataFeed);
		}

		// Token: 0x060018CC RID: 6348 RVA: 0x000AF078 File Offset: 0x000AE478
		private void CreateOrValidateConnection(string method)
		{
			if (this._connection == null)
			{
				throw ADP.ConnectionRequired(method);
			}
			if (this._connection.IsContextConnection)
			{
				throw SQL.NotAvailableOnContextConnection();
			}
			if (this._ownConnection && this._connection.State != ConnectionState.Open)
			{
				this._connection.Open();
			}
			this._connection.ValidateConnectionForExecute(method, null);
			if (this._externalTransaction != null && this._connection != this._externalTransaction.Connection)
			{
				throw ADP.TransactionConnectionMismatch();
			}
		}

		// Token: 0x060018CD RID: 6349 RVA: 0x000AF0F8 File Offset: 0x000AE4F8
		private void RunParser(BulkCopySimpleResultSet bulkCopyHandler = null)
		{
			SqlInternalConnectionTds openTdsConnection = this._connection.GetOpenTdsConnection();
			openTdsConnection.ThreadHasParserLockForClose = true;
			try
			{
				this._parser.Run(RunBehavior.UntilDone, null, null, bulkCopyHandler, this._stateObj);
			}
			finally
			{
				openTdsConnection.ThreadHasParserLockForClose = false;
			}
		}

		// Token: 0x060018CE RID: 6350 RVA: 0x000AF154 File Offset: 0x000AE554
		private void RunParserReliably(BulkCopySimpleResultSet bulkCopyHandler = null)
		{
			SqlInternalConnectionTds openTdsConnection = this._connection.GetOpenTdsConnection();
			openTdsConnection.ThreadHasParserLockForClose = true;
			try
			{
				this._parser.RunReliably(RunBehavior.UntilDone, null, null, bulkCopyHandler, this._stateObj);
			}
			finally
			{
				openTdsConnection.ThreadHasParserLockForClose = false;
			}
		}

		// Token: 0x060018CF RID: 6351 RVA: 0x000AF1B0 File Offset: 0x000AE5B0
		private void CommitTransaction()
		{
			if (this._internalTransaction != null)
			{
				SqlInternalConnectionTds openTdsConnection = this._connection.GetOpenTdsConnection();
				openTdsConnection.ThreadHasParserLockForClose = true;
				try
				{
					this._internalTransaction.Commit();
					this._internalTransaction.Dispose();
					this._internalTransaction = null;
				}
				finally
				{
					openTdsConnection.ThreadHasParserLockForClose = false;
				}
			}
		}

		// Token: 0x060018D0 RID: 6352 RVA: 0x000AF21C File Offset: 0x000AE61C
		private void AbortTransaction()
		{
			if (this._internalTransaction != null)
			{
				if (!this._internalTransaction.IsZombied)
				{
					SqlInternalConnectionTds openTdsConnection = this._connection.GetOpenTdsConnection();
					openTdsConnection.ThreadHasParserLockForClose = true;
					try
					{
						this._internalTransaction.Rollback();
					}
					finally
					{
						openTdsConnection.ThreadHasParserLockForClose = false;
					}
				}
				this._internalTransaction.Dispose();
				this._internalTransaction = null;
			}
		}

		// Token: 0x060018D1 RID: 6353 RVA: 0x000AF294 File Offset: 0x000AE694
		private void AppendColumnNameAndTypeName(StringBuilder query, string columnName, string typeName)
		{
			SqlServerEscapeHelper.EscapeIdentifier(query, columnName);
			query.Append(" ");
			query.Append(typeName);
		}

		// Token: 0x060018D2 RID: 6354 RVA: 0x000AF2BC File Offset: 0x000AE6BC
		private string UnquotedName(string name)
		{
			if (ADP.IsEmpty(name))
			{
				return null;
			}
			if (name[0] == '[')
			{
				int length = name.Length;
				name = name.Substring(1, length - 2);
			}
			return name;
		}

		// Token: 0x060018D3 RID: 6355 RVA: 0x000AF2F4 File Offset: 0x000AE6F4
		private object ValidateBulkCopyVariant(object value)
		{
			MetaType metaTypeFromValue = MetaType.GetMetaTypeFromValue(value, true);
			byte tdstype = metaTypeFromValue.TDSType;
			if (tdstype <= 108)
			{
				if (tdstype <= 43)
				{
					if (tdstype != 36 && tdstype - 40 > 3)
					{
						goto IL_B4;
					}
				}
				else
				{
					switch (tdstype)
					{
					case 48:
					case 50:
					case 52:
					case 56:
					case 59:
					case 60:
					case 61:
					case 62:
						break;
					case 49:
					case 51:
					case 53:
					case 54:
					case 55:
					case 57:
					case 58:
						goto IL_B4;
					default:
						if (tdstype != 108)
						{
							goto IL_B4;
						}
						break;
					}
				}
			}
			else if (tdstype <= 165)
			{
				if (tdstype != 127 && tdstype != 165)
				{
					goto IL_B4;
				}
			}
			else if (tdstype != 167 && tdstype != 231)
			{
				goto IL_B4;
			}
			if (value is INullable)
			{
				return MetaType.GetComValueFromSqlVariant(value);
			}
			return value;
			IL_B4:
			throw SQL.BulkLoadInvalidVariantValue();
		}

		// Token: 0x060018D4 RID: 6356 RVA: 0x000AF3BC File Offset: 0x000AE7BC
		private object ConvertValue(object value, _SqlMetaData metadata, bool isNull, ref bool isSqlType, out bool coercedToDataFeed)
		{
			coercedToDataFeed = false;
			if (!isNull)
			{
				MetaType metaType = metadata.metaType;
				bool flag = false;
				byte scale = metadata.scale;
				byte precision = metadata.precision;
				int length = metadata.length;
				if (metadata.isEncrypted)
				{
					metaType = metadata.baseTI.metaType;
					scale = metadata.baseTI.scale;
					precision = metadata.baseTI.precision;
					length = metadata.baseTI.length;
				}
				object result;
				try
				{
					byte nullableType = metaType.NullableType;
					MetaType metaTypeFromSqlDbType;
					if (nullableType <= 165)
					{
						if (nullableType <= 59)
						{
							switch (nullableType)
							{
							case 34:
							case 35:
							case 36:
							case 38:
							case 40:
							case 41:
							case 42:
							case 43:
							case 50:
								break;
							case 37:
							case 39:
							case 44:
							case 45:
							case 46:
							case 47:
							case 48:
							case 49:
								goto IL_30B;
							default:
								if (nullableType - 58 > 1)
								{
									goto IL_30B;
								}
								break;
							}
						}
						else if (nullableType - 61 > 1)
						{
							switch (nullableType)
							{
							case 98:
								value = this.ValidateBulkCopyVariant(value);
								flag = true;
								goto IL_319;
							case 99:
								goto IL_26B;
							case 100:
							case 101:
							case 102:
							case 103:
							case 105:
							case 107:
								goto IL_30B;
							case 104:
							case 109:
							case 110:
							case 111:
								break;
							case 106:
							case 108:
							{
								metaTypeFromSqlDbType = MetaType.GetMetaTypeFromSqlDbType(metaType.SqlDbType, false);
								value = SqlParameter.CoerceValue(value, metaTypeFromSqlDbType, out coercedToDataFeed, out flag, false);
								SqlDecimal sqlDecimal;
								if (isSqlType && !flag)
								{
									sqlDecimal = (SqlDecimal)value;
								}
								else
								{
									sqlDecimal = new SqlDecimal((decimal)value);
								}
								if (sqlDecimal.Scale != scale)
								{
									sqlDecimal = TdsParser.AdjustSqlDecimalScale(sqlDecimal, (int)scale);
								}
								if (sqlDecimal.Precision > precision)
								{
									try
									{
										sqlDecimal = SqlDecimal.ConvertToPrecScale(sqlDecimal, (int)precision, (int)sqlDecimal.Scale);
									}
									catch (SqlTruncateException)
									{
										throw SQL.BulkLoadCannotConvertValue(value.GetType(), metaTypeFromSqlDbType, ADP.ParameterValueOutOfRange(sqlDecimal));
									}
									catch (Exception e)
									{
										throw SQL.BulkLoadCannotConvertValue(value.GetType(), metaTypeFromSqlDbType, e);
									}
								}
								value = sqlDecimal;
								isSqlType = true;
								flag = false;
								goto IL_319;
							}
							default:
								if (nullableType != 165)
								{
									goto IL_30B;
								}
								break;
							}
						}
					}
					else if (nullableType <= 173)
					{
						if (nullableType != 167 && nullableType != 173)
						{
							goto IL_30B;
						}
					}
					else if (nullableType != 175)
					{
						if (nullableType == 231)
						{
							goto IL_26B;
						}
						switch (nullableType)
						{
						case 239:
							goto IL_26B;
						case 240:
							if (!(value is byte[]))
							{
								value = this._connection.GetBytes(value);
								flag = true;
								goto IL_319;
							}
							goto IL_319;
						case 241:
							if (value is XmlReader)
							{
								value = new XmlDataFeed((XmlReader)value);
								flag = true;
								coercedToDataFeed = true;
								goto IL_319;
							}
							goto IL_319;
						default:
							goto IL_30B;
						}
					}
					metaTypeFromSqlDbType = MetaType.GetMetaTypeFromSqlDbType(metaType.SqlDbType, false);
					value = SqlParameter.CoerceValue(value, metaTypeFromSqlDbType, out coercedToDataFeed, out flag, false);
					goto IL_319;
					IL_26B:
					metaTypeFromSqlDbType = MetaType.GetMetaTypeFromSqlDbType(metaType.SqlDbType, false);
					value = SqlParameter.CoerceValue(value, metaTypeFromSqlDbType, out coercedToDataFeed, out flag, false);
					if (coercedToDataFeed)
					{
						goto IL_319;
					}
					int num = (isSqlType && !flag) ? ((SqlString)value).Value.Length : ((string)value).Length;
					if (num > length / 2)
					{
						throw SQL.BulkLoadStringTooLong();
					}
					goto IL_319;
					IL_30B:
					throw SQL.BulkLoadCannotConvertValue(value.GetType(), metaType, null);
					IL_319:
					if (flag)
					{
						isSqlType = false;
					}
					result = value;
				}
				catch (Exception e2)
				{
					if (!ADP.IsCatchableExceptionType(e2))
					{
						throw;
					}
					throw SQL.BulkLoadCannotConvertValue(value.GetType(), metaType, e2);
				}
				return result;
			}
			if (!metadata.isNullable)
			{
				throw SQL.BulkLoadBulkLoadNotAllowDBNull(metadata.column);
			}
			return value;
		}

		// Token: 0x060018D5 RID: 6357 RVA: 0x000AF75C File Offset: 0x000AEB5C
		public void WriteToServer(DbDataReader reader)
		{
			SqlConnection.ExecutePermission.Demand();
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			if (this._isBulkCopyingInProgress)
			{
				throw SQL.BulkLoadPendingOperation();
			}
			SqlStatistics statistics = this.Statistics;
			try
			{
				statistics = SqlStatistics.StartTimer(this.Statistics);
				this._rowSource = reader;
				this._DbDataReaderRowSource = reader;
				this._SqlDataReaderRowSource = (reader as SqlDataReader);
				if (this._SqlDataReaderRowSource != null)
				{
					this._rowSourceIsSqlDataReaderSmi = (this._SqlDataReaderRowSource is SqlDataReaderSmi);
				}
				this._dataTableSource = null;
				this._rowSourceType = SqlBulkCopy.ValueSourceType.DbDataReader;
				this._isAsyncBulkCopy = false;
				this.WriteRowSourceToServerAsync(reader.FieldCount, CancellationToken.None);
			}
			finally
			{
				SqlStatistics.StopTimer(statistics);
			}
		}

		// Token: 0x060018D6 RID: 6358 RVA: 0x000AF824 File Offset: 0x000AEC24
		public void WriteToServer(IDataReader reader)
		{
			SqlConnection.ExecutePermission.Demand();
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			if (this._isBulkCopyingInProgress)
			{
				throw SQL.BulkLoadPendingOperation();
			}
			SqlStatistics statistics = this.Statistics;
			try
			{
				statistics = SqlStatistics.StartTimer(this.Statistics);
				this._rowSource = reader;
				this._SqlDataReaderRowSource = (this._rowSource as SqlDataReader);
				if (this._SqlDataReaderRowSource != null)
				{
					this._rowSourceIsSqlDataReaderSmi = (this._SqlDataReaderRowSource is SqlDataReaderSmi);
				}
				this._DbDataReaderRowSource = (this._rowSource as DbDataReader);
				this._dataTableSource = null;
				this._rowSourceType = SqlBulkCopy.ValueSourceType.IDataReader;
				this._isAsyncBulkCopy = false;
				this.WriteRowSourceToServerAsync(reader.FieldCount, CancellationToken.None);
			}
			finally
			{
				SqlStatistics.StopTimer(statistics);
			}
		}

		// Token: 0x060018D7 RID: 6359 RVA: 0x000AF8FC File Offset: 0x000AECFC
		public void WriteToServer(DataTable table)
		{
			this.WriteToServer(table, (DataRowState)0);
		}

		// Token: 0x060018D8 RID: 6360 RVA: 0x000AF914 File Offset: 0x000AED14
		public void WriteToServer(DataTable table, DataRowState rowState)
		{
			SqlConnection.ExecutePermission.Demand();
			if (table == null)
			{
				throw new ArgumentNullException("table");
			}
			if (this._isBulkCopyingInProgress)
			{
				throw SQL.BulkLoadPendingOperation();
			}
			SqlStatistics statistics = this.Statistics;
			try
			{
				statistics = SqlStatistics.StartTimer(this.Statistics);
				this._rowStateToSkip = ((rowState == (DataRowState)0 || rowState == DataRowState.Deleted) ? DataRowState.Deleted : (~rowState | DataRowState.Deleted));
				this._rowSource = table;
				this._dataTableSource = table;
				this._SqlDataReaderRowSource = null;
				this._rowSourceType = SqlBulkCopy.ValueSourceType.DataTable;
				this._rowEnumerator = table.Rows.GetEnumerator();
				this._isAsyncBulkCopy = false;
				this.WriteRowSourceToServerAsync(table.Columns.Count, CancellationToken.None);
			}
			finally
			{
				SqlStatistics.StopTimer(statistics);
			}
		}

		// Token: 0x060018D9 RID: 6361 RVA: 0x000AF9E0 File Offset: 0x000AEDE0
		public void WriteToServer(DataRow[] rows)
		{
			SqlConnection.ExecutePermission.Demand();
			SqlStatistics statistics = this.Statistics;
			if (rows == null)
			{
				throw new ArgumentNullException("rows");
			}
			if (this._isBulkCopyingInProgress)
			{
				throw SQL.BulkLoadPendingOperation();
			}
			if (rows.Length == 0)
			{
				return;
			}
			try
			{
				statistics = SqlStatistics.StartTimer(this.Statistics);
				DataTable table = rows[0].Table;
				this._rowStateToSkip = DataRowState.Deleted;
				this._rowSource = rows;
				this._dataTableSource = table;
				this._SqlDataReaderRowSource = null;
				this._rowSourceType = SqlBulkCopy.ValueSourceType.RowArray;
				this._rowEnumerator = rows.GetEnumerator();
				this._isAsyncBulkCopy = false;
				this.WriteRowSourceToServerAsync(table.Columns.Count, CancellationToken.None);
			}
			finally
			{
				SqlStatistics.StopTimer(statistics);
			}
		}

		// Token: 0x060018DA RID: 6362 RVA: 0x000AFAA8 File Offset: 0x000AEEA8
		public Task WriteToServerAsync(DataRow[] rows)
		{
			return this.WriteToServerAsync(rows, CancellationToken.None);
		}

		// Token: 0x060018DB RID: 6363 RVA: 0x000AFAC4 File Offset: 0x000AEEC4
		public Task WriteToServerAsync(DataRow[] rows, CancellationToken cancellationToken)
		{
			Task result = null;
			SqlConnection.ExecutePermission.Demand();
			if (rows == null)
			{
				throw new ArgumentNullException("rows");
			}
			if (this._isBulkCopyingInProgress)
			{
				throw SQL.BulkLoadPendingOperation();
			}
			SqlStatistics statistics = this.Statistics;
			try
			{
				statistics = SqlStatistics.StartTimer(this.Statistics);
				if (rows.Length == 0)
				{
					TaskCompletionSource<object> taskCompletionSource = new TaskCompletionSource<object>();
					if (cancellationToken.IsCancellationRequested)
					{
						taskCompletionSource.SetCanceled();
					}
					else
					{
						taskCompletionSource.SetResult(null);
					}
					result = taskCompletionSource.Task;
					return result;
				}
				DataTable table = rows[0].Table;
				this._rowStateToSkip = DataRowState.Deleted;
				this._rowSource = rows;
				this._dataTableSource = table;
				this._SqlDataReaderRowSource = null;
				this._rowSourceType = SqlBulkCopy.ValueSourceType.RowArray;
				this._rowEnumerator = rows.GetEnumerator();
				this._isAsyncBulkCopy = true;
				result = this.WriteRowSourceToServerAsync(table.Columns.Count, cancellationToken);
			}
			finally
			{
				SqlStatistics.StopTimer(statistics);
			}
			return result;
		}

		// Token: 0x060018DC RID: 6364 RVA: 0x000AFBB8 File Offset: 0x000AEFB8
		public Task WriteToServerAsync(DbDataReader reader)
		{
			return this.WriteToServerAsync(reader, CancellationToken.None);
		}

		// Token: 0x060018DD RID: 6365 RVA: 0x000AFBD4 File Offset: 0x000AEFD4
		public Task WriteToServerAsync(DbDataReader reader, CancellationToken cancellationToken)
		{
			Task result = null;
			SqlConnection.ExecutePermission.Demand();
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			if (this._isBulkCopyingInProgress)
			{
				throw SQL.BulkLoadPendingOperation();
			}
			SqlStatistics statistics = this.Statistics;
			try
			{
				statistics = SqlStatistics.StartTimer(this.Statistics);
				this._rowSource = reader;
				this._SqlDataReaderRowSource = (reader as SqlDataReader);
				this._DbDataReaderRowSource = reader;
				this._dataTableSource = null;
				this._rowSourceType = SqlBulkCopy.ValueSourceType.DbDataReader;
				this._isAsyncBulkCopy = true;
				result = this.WriteRowSourceToServerAsync(reader.FieldCount, cancellationToken);
			}
			finally
			{
				SqlStatistics.StopTimer(statistics);
			}
			return result;
		}

		// Token: 0x060018DE RID: 6366 RVA: 0x000AFC80 File Offset: 0x000AF080
		public Task WriteToServerAsync(IDataReader reader)
		{
			return this.WriteToServerAsync(reader, CancellationToken.None);
		}

		// Token: 0x060018DF RID: 6367 RVA: 0x000AFC9C File Offset: 0x000AF09C
		public Task WriteToServerAsync(IDataReader reader, CancellationToken cancellationToken)
		{
			Task result = null;
			SqlConnection.ExecutePermission.Demand();
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			if (this._isBulkCopyingInProgress)
			{
				throw SQL.BulkLoadPendingOperation();
			}
			SqlStatistics statistics = this.Statistics;
			try
			{
				statistics = SqlStatistics.StartTimer(this.Statistics);
				this._rowSource = reader;
				this._SqlDataReaderRowSource = (this._rowSource as SqlDataReader);
				this._DbDataReaderRowSource = (this._rowSource as DbDataReader);
				this._dataTableSource = null;
				this._rowSourceType = SqlBulkCopy.ValueSourceType.IDataReader;
				this._isAsyncBulkCopy = true;
				result = this.WriteRowSourceToServerAsync(reader.FieldCount, cancellationToken);
			}
			finally
			{
				SqlStatistics.StopTimer(statistics);
			}
			return result;
		}

		// Token: 0x060018E0 RID: 6368 RVA: 0x000AFD58 File Offset: 0x000AF158
		public Task WriteToServerAsync(DataTable table)
		{
			return this.WriteToServerAsync(table, (DataRowState)0, CancellationToken.None);
		}

		// Token: 0x060018E1 RID: 6369 RVA: 0x000AFD74 File Offset: 0x000AF174
		public Task WriteToServerAsync(DataTable table, CancellationToken cancellationToken)
		{
			return this.WriteToServerAsync(table, (DataRowState)0, cancellationToken);
		}

		// Token: 0x060018E2 RID: 6370 RVA: 0x000AFD8C File Offset: 0x000AF18C
		public Task WriteToServerAsync(DataTable table, DataRowState rowState)
		{
			return this.WriteToServerAsync(table, rowState, CancellationToken.None);
		}

		// Token: 0x060018E3 RID: 6371 RVA: 0x000AFDA8 File Offset: 0x000AF1A8
		public Task WriteToServerAsync(DataTable table, DataRowState rowState, CancellationToken cancellationToken)
		{
			Task result = null;
			SqlConnection.ExecutePermission.Demand();
			if (table == null)
			{
				throw new ArgumentNullException("table");
			}
			if (this._isBulkCopyingInProgress)
			{
				throw SQL.BulkLoadPendingOperation();
			}
			SqlStatistics statistics = this.Statistics;
			try
			{
				statistics = SqlStatistics.StartTimer(this.Statistics);
				this._rowStateToSkip = ((rowState == (DataRowState)0 || rowState == DataRowState.Deleted) ? DataRowState.Deleted : (~rowState | DataRowState.Deleted));
				this._rowSource = table;
				this._SqlDataReaderRowSource = null;
				this._dataTableSource = table;
				this._rowSourceType = SqlBulkCopy.ValueSourceType.DataTable;
				this._rowEnumerator = table.Rows.GetEnumerator();
				this._isAsyncBulkCopy = true;
				result = this.WriteRowSourceToServerAsync(table.Columns.Count, cancellationToken);
			}
			finally
			{
				SqlStatistics.StopTimer(statistics);
			}
			return result;
		}

		// Token: 0x060018E4 RID: 6372 RVA: 0x000AFE74 File Offset: 0x000AF274
		private Task WriteRowSourceToServerAsync(int columnCount, CancellationToken ctoken)
		{
			Task currentReconnectionTask = this._connection._currentReconnectionTask;
			if (currentReconnectionTask != null && !currentReconnectionTask.IsCompleted)
			{
				if (this._isAsyncBulkCopy)
				{
					TaskCompletionSource<object> tcs = new TaskCompletionSource<object>();
					Action <>9__2;
					currentReconnectionTask.ContinueWith(delegate(Task t)
					{
						Task task2 = this.WriteRowSourceToServerAsync(columnCount, ctoken);
						TaskCompletionSource<object> tcs;
						if (task2 == null)
						{
							tcs.SetResult(null);
							return;
						}
						Task task3 = task2;
						tcs = tcs;
						Action onSuccess;
						if ((onSuccess = <>9__2) == null)
						{
							onSuccess = (<>9__2 = delegate()
							{
								tcs.SetResult(null);
							});
						}
						AsyncHelper.ContinueTask(task3, tcs, onSuccess, null, null, null, null, null);
					}, ctoken);
					return tcs.Task;
				}
				AsyncHelper.WaitForCompletion(currentReconnectionTask, this.BulkCopyTimeout, delegate
				{
					throw SQL.CR_ReconnectTimeout();
				}, false);
			}
			bool flag = true;
			this._isBulkCopyingInProgress = true;
			this.CreateOrValidateConnection("WriteToServer");
			SqlInternalConnectionTds openTdsConnection = this._connection.GetOpenTdsConnection();
			this._parserLock = openTdsConnection._parserLock;
			this._parserLock.Wait(this._isAsyncBulkCopy);
			TdsParser target = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			Task result;
			try
			{
				target = SqlInternalConnection.GetBestEffortCleanupTarget(this._connection);
				this.WriteRowSourceToServerCommon(columnCount);
				Task task = this.WriteToServerInternalAsync(ctoken);
				if (task != null)
				{
					flag = false;
					result = task.ContinueWith<Task>(delegate(Task t)
					{
						try
						{
							this.AbortTransaction();
						}
						finally
						{
							this._isBulkCopyingInProgress = false;
							if (this._parser != null)
							{
								this._parser._asyncWrite = false;
							}
							if (this._parserLock != null)
							{
								this._parserLock.Release();
								this._parserLock = null;
							}
						}
						return t;
					}, TaskScheduler.Default).Unwrap();
				}
				else
				{
					result = null;
				}
			}
			catch (OutOfMemoryException e)
			{
				this._connection.Abort(e);
				throw;
			}
			catch (StackOverflowException e2)
			{
				this._connection.Abort(e2);
				throw;
			}
			catch (ThreadAbortException e3)
			{
				this._connection.Abort(e3);
				SqlInternalConnection.BestEffortCleanup(target);
				throw;
			}
			finally
			{
				this._columnMappings.ReadOnly = false;
				if (flag)
				{
					try
					{
						this.AbortTransaction();
					}
					finally
					{
						this._isBulkCopyingInProgress = false;
						if (this._parser != null)
						{
							this._parser._asyncWrite = false;
						}
						if (this._parserLock != null)
						{
							this._parserLock.Release();
							this._parserLock = null;
						}
					}
				}
			}
			return result;
		}

		// Token: 0x060018E5 RID: 6373 RVA: 0x000B00E0 File Offset: 0x000AF4E0
		private void WriteRowSourceToServerCommon(int columnCount)
		{
			bool flag = false;
			this._columnMappings.ReadOnly = true;
			this._localColumnMappings = this._columnMappings;
			if (this._localColumnMappings.Count > 0)
			{
				this._localColumnMappings.ValidateCollection();
				using (IEnumerator enumerator = this._localColumnMappings.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						object obj = enumerator.Current;
						SqlBulkCopyColumnMapping sqlBulkCopyColumnMapping = (SqlBulkCopyColumnMapping)obj;
						if (sqlBulkCopyColumnMapping._internalSourceColumnOrdinal == -1)
						{
							flag = true;
							break;
						}
					}
					goto IL_94;
				}
			}
			this._localColumnMappings = new SqlBulkCopyColumnMappingCollection();
			this._localColumnMappings.CreateDefaultMapping(columnCount);
			IL_94:
			if (flag)
			{
				int num = -1;
				flag = false;
				if (this._localColumnMappings.Count > 0)
				{
					foreach (object obj2 in this._localColumnMappings)
					{
						SqlBulkCopyColumnMapping sqlBulkCopyColumnMapping2 = (SqlBulkCopyColumnMapping)obj2;
						if (sqlBulkCopyColumnMapping2._internalSourceColumnOrdinal == -1)
						{
							string text = this.UnquotedName(sqlBulkCopyColumnMapping2.SourceColumn);
							switch (this._rowSourceType)
							{
							case SqlBulkCopy.ValueSourceType.IDataReader:
							case SqlBulkCopy.ValueSourceType.DbDataReader:
								try
								{
									num = ((IDataRecord)this._rowSource).GetOrdinal(text);
								}
								catch (IndexOutOfRangeException e)
								{
									throw SQL.BulkLoadNonMatchingColumnName(text, e);
								}
								break;
							case SqlBulkCopy.ValueSourceType.DataTable:
								num = ((DataTable)this._rowSource).Columns.IndexOf(text);
								break;
							case SqlBulkCopy.ValueSourceType.RowArray:
								num = ((DataRow[])this._rowSource)[0].Table.Columns.IndexOf(text);
								break;
							}
							if (num == -1)
							{
								throw SQL.BulkLoadNonMatchingColumnName(text);
							}
							sqlBulkCopyColumnMapping2._internalSourceColumnOrdinal = num;
						}
					}
				}
			}
		}

		// Token: 0x060018E6 RID: 6374 RVA: 0x000B02D4 File Offset: 0x000AF6D4
		internal void OnConnectionClosed()
		{
			TdsParserStateObject stateObj = this._stateObj;
			if (stateObj != null)
			{
				stateObj.OnConnectionClosed();
			}
		}

		// Token: 0x060018E7 RID: 6375 RVA: 0x000B02F4 File Offset: 0x000AF6F4
		private void OnRowsCopied(SqlRowsCopiedEventArgs value)
		{
			SqlRowsCopiedEventHandler rowsCopiedEventHandler = this._rowsCopiedEventHandler;
			if (rowsCopiedEventHandler != null)
			{
				rowsCopiedEventHandler(this, value);
			}
		}

		// Token: 0x060018E8 RID: 6376 RVA: 0x000B0314 File Offset: 0x000AF714
		private bool FireRowsCopiedEvent(long rowsCopied)
		{
			SqlInternalConnectionTds openTdsConnection = this._connection.GetOpenTdsConnection();
			bool canBeReleasedFromAnyThread = openTdsConnection._parserLock.CanBeReleasedFromAnyThread;
			openTdsConnection._parserLock.Release();
			SqlRowsCopiedEventArgs sqlRowsCopiedEventArgs = new SqlRowsCopiedEventArgs(rowsCopied);
			try
			{
				this._insideRowsCopiedEvent = true;
				this.OnRowsCopied(sqlRowsCopiedEventArgs);
			}
			finally
			{
				this._insideRowsCopiedEvent = false;
				openTdsConnection._parserLock.Wait(canBeReleasedFromAnyThread);
			}
			return sqlRowsCopiedEventArgs.Abort;
		}

		// Token: 0x060018E9 RID: 6377 RVA: 0x000B0394 File Offset: 0x000AF794
		private Task ReadWriteColumnValueAsync(int col)
		{
			bool isSqlType;
			bool flag;
			bool flag2;
			object obj = this.GetValueFromSourceRow(col, out isSqlType, out flag, out flag2);
			_SqlMetaData metadata = this._sortedColumnMappings[col]._metadata;
			if (!flag)
			{
				obj = this.ConvertValue(obj, metadata, flag2, ref isSqlType, out flag);
				if (!flag2 && metadata.isEncrypted)
				{
					obj = this._parser.EncryptColumnValue(obj, metadata, metadata.column, this._stateObj, flag, isSqlType);
					isSqlType = false;
				}
			}
			Task result = null;
			if (metadata.type != SqlDbType.Variant)
			{
				result = this._parser.WriteBulkCopyValue(obj, metadata, this._stateObj, isSqlType, flag, flag2);
			}
			else
			{
				SqlBuffer.StorageType storageType = SqlBuffer.StorageType.Empty;
				if (this._SqlDataReaderRowSource != null && this._connection.IsKatmaiOrNewer)
				{
					storageType = this._SqlDataReaderRowSource.GetVariantInternalStorageType(this._sortedColumnMappings[col]._sourceColumnOrdinal);
				}
				if (storageType == SqlBuffer.StorageType.DateTime2)
				{
					this._parser.WriteSqlVariantDateTime2((DateTime)obj, this._stateObj);
				}
				else if (storageType == SqlBuffer.StorageType.Date)
				{
					this._parser.WriteSqlVariantDate((DateTime)obj, this._stateObj);
				}
				else
				{
					result = this._parser.WriteSqlVariantDataRowValue(obj, this._stateObj, true);
				}
			}
			return result;
		}

		// Token: 0x060018EA RID: 6378 RVA: 0x000B04B4 File Offset: 0x000AF8B4
		private void RegisterForConnectionCloseNotification<T>(ref Task<T> outterTask)
		{
			SqlConnection connection = this._connection;
			if (connection == null)
			{
				throw ADP.ClosedConnectionError();
			}
			connection.RegisterForConnectionCloseNotification<T>(ref outterTask, this, 3);
		}

		// Token: 0x060018EB RID: 6379 RVA: 0x000B04DC File Offset: 0x000AF8DC
		private Task CopyColumnsAsync(int col, TaskCompletionSource<object> source = null)
		{
			Task result = null;
			Task task = null;
			try
			{
				int i;
				for (i = col; i < this._sortedColumnMappings.Count; i++)
				{
					task = this.ReadWriteColumnValueAsync(i);
					if (task != null)
					{
						break;
					}
				}
				if (task != null)
				{
					if (source == null)
					{
						source = new TaskCompletionSource<object>();
						result = source.Task;
					}
					this.CopyColumnsAsyncSetupContinuation(source, task, i);
					return result;
				}
				if (source != null)
				{
					source.SetResult(null);
				}
			}
			catch (Exception exception)
			{
				if (source == null)
				{
					throw;
				}
				source.TrySetException(exception);
			}
			return result;
		}

		// Token: 0x060018EC RID: 6380 RVA: 0x000B0570 File Offset: 0x000AF970
		private void CopyColumnsAsyncSetupContinuation(TaskCompletionSource<object> source, Task task, int i)
		{
			AsyncHelper.ContinueTask(task, source, delegate
			{
				if (i + 1 < this._sortedColumnMappings.Count)
				{
					this.CopyColumnsAsync(i + 1, source);
					return;
				}
				source.SetResult(null);
			}, this._connection.GetOpenTdsConnection(), null, null, null, null);
		}

		// Token: 0x060018ED RID: 6381 RVA: 0x000B05C0 File Offset: 0x000AF9C0
		private void CheckAndRaiseNotification()
		{
			bool flag = false;
			Exception ex = null;
			this._rowsCopied++;
			if (this._notifyAfter > 0 && this._rowsUntilNotification > 0)
			{
				int num = this._rowsUntilNotification - 1;
				this._rowsUntilNotification = num;
				if (num == 0)
				{
					try
					{
						this._stateObj.BcpLock = true;
						flag = this.FireRowsCopiedEvent((long)this._rowsCopied);
						Bid.Trace("<sc.SqlBulkCopy.WriteToServerInternal|INFO> \n");
						if (ConnectionState.Open != this._connection.State)
						{
							ex = ADP.OpenConnectionRequired("CheckAndRaiseNotification", this._connection.State);
						}
					}
					catch (Exception ex2)
					{
						if (!ADP.IsCatchableExceptionType(ex2))
						{
							ex = ex2;
						}
						else
						{
							ex = OperationAbortedException.Aborted(ex2);
						}
					}
					finally
					{
						this._stateObj.BcpLock = false;
					}
					if (!flag)
					{
						this._rowsUntilNotification = this._notifyAfter;
					}
				}
			}
			if (!flag && this._rowsUntilNotification > this._notifyAfter)
			{
				this._rowsUntilNotification = this._notifyAfter;
			}
			if (ex == null && flag)
			{
				ex = OperationAbortedException.Aborted(null);
			}
			if (this._connection.State != ConnectionState.Open)
			{
				throw ADP.OpenConnectionRequired("WriteToServer", this._connection.State);
			}
			if (ex != null)
			{
				this._parser._asyncWrite = false;
				Task task = this._parser.WriteBulkCopyDone(this._stateObj);
				this.RunParser(null);
				this.AbortTransaction();
				throw ex;
			}
		}

		// Token: 0x060018EE RID: 6382 RVA: 0x000B0740 File Offset: 0x000AFB40
		private Task CheckForCancellation(CancellationToken cts, TaskCompletionSource<object> tcs)
		{
			if (cts.IsCancellationRequested)
			{
				if (tcs == null)
				{
					tcs = new TaskCompletionSource<object>();
				}
				tcs.SetCanceled();
				return tcs.Task;
			}
			return null;
		}

		// Token: 0x060018EF RID: 6383 RVA: 0x000B0770 File Offset: 0x000AFB70
		private TaskCompletionSource<object> ContinueTaskPend(Task task, TaskCompletionSource<object> source, Func<TaskCompletionSource<object>> action)
		{
			if (task == null)
			{
				return action();
			}
			AsyncHelper.ContinueTask(task, source, delegate
			{
				TaskCompletionSource<object> taskCompletionSource = action();
			}, null, null, null, null, null);
			return null;
		}

		// Token: 0x060018F0 RID: 6384 RVA: 0x000B07B4 File Offset: 0x000AFBB4
		private Task CopyRowsAsync(int rowsSoFar, int totalRows, CancellationToken cts, TaskCompletionSource<object> source = null)
		{
			Task task = null;
			try
			{
				int i = rowsSoFar;
				Action <>9__1;
				Action <>9__2;
				while ((totalRows <= 0 || i < totalRows) && this._hasMoreRowToCopy)
				{
					if (this._isAsyncBulkCopy)
					{
						task = this.CheckForCancellation(cts, source);
						if (task != null)
						{
							return task;
						}
					}
					this._stateObj.WriteByte(209);
					Task task2 = this.CopyColumnsAsync(0, null);
					if (task2 != null)
					{
						source = (source ?? new TaskCompletionSource<object>());
						task = source.Task;
						AsyncHelper.ContinueTask(task2, source, delegate
						{
							this.CheckAndRaiseNotification();
							Task task5 = this.ReadFromRowSourceAsync(cts);
							if (task5 == null)
							{
								this.CopyRowsAsync(i + 1, totalRows, cts, source);
								return;
							}
							Task task6 = task5;
							TaskCompletionSource<object> source3 = source;
							Action onSuccess2;
							if ((onSuccess2 = <>9__2) == null)
							{
								onSuccess2 = (<>9__2 = delegate()
								{
									this.CopyRowsAsync(i + 1, totalRows, cts, source);
								});
							}
							AsyncHelper.ContinueTask(task6, source3, onSuccess2, this._connection.GetOpenTdsConnection(), null, null, null, null);
						}, this._connection.GetOpenTdsConnection(), null, null, null, null);
						return task;
					}
					this.CheckAndRaiseNotification();
					Task task3 = this.ReadFromRowSourceAsync(cts);
					if (task3 != null)
					{
						if (source == null)
						{
							source = new TaskCompletionSource<object>();
						}
						task = source.Task;
						Task task4 = task3;
						TaskCompletionSource<object> source2 = source;
						Action onSuccess;
						if ((onSuccess = <>9__1) == null)
						{
							onSuccess = (<>9__1 = delegate()
							{
								this.CopyRowsAsync(i + 1, totalRows, cts, source);
							});
						}
						AsyncHelper.ContinueTask(task4, source2, onSuccess, this._connection.GetOpenTdsConnection(), null, null, null, null);
						return task;
					}
					int j = i;
					i = j + 1;
				}
				if (source != null)
				{
					source.TrySetResult(null);
				}
			}
			catch (Exception exception)
			{
				if (source == null)
				{
					throw;
				}
				source.TrySetException(exception);
			}
			return task;
		}

		// Token: 0x060018F1 RID: 6385 RVA: 0x000B098C File Offset: 0x000AFD8C
		private Task CopyBatchesAsync(BulkCopySimpleResultSet internalResults, string updateBulkCommandText, CancellationToken cts, TaskCompletionSource<object> source = null)
		{
			try
			{
				Action <>9__0;
				while (this._hasMoreRowToCopy)
				{
					SqlInternalConnectionTds openTdsConnection = this._connection.GetOpenTdsConnection();
					if (this.IsCopyOption(SqlBulkCopyOptions.UseInternalTransaction))
					{
						openTdsConnection.ThreadHasParserLockForClose = true;
						try
						{
							this._internalTransaction = this._connection.BeginTransaction();
						}
						finally
						{
							openTdsConnection.ThreadHasParserLockForClose = false;
						}
					}
					Task task = this.SubmitUpdateBulkCommand(updateBulkCommandText);
					if (task != null)
					{
						if (source == null)
						{
							source = new TaskCompletionSource<object>();
						}
						Task task2 = task;
						TaskCompletionSource<object> source2 = source;
						Action onSuccess;
						if ((onSuccess = <>9__0) == null)
						{
							onSuccess = (<>9__0 = delegate()
							{
								if (this.CopyBatchesAsyncContinued(internalResults, updateBulkCommandText, cts, source) == null)
								{
									this.CopyBatchesAsync(internalResults, updateBulkCommandText, cts, source);
								}
							});
						}
						AsyncHelper.ContinueTask(task2, source2, onSuccess, this._connection.GetOpenTdsConnection(), null, null, null, null);
						return source.Task;
					}
					Task task3 = this.CopyBatchesAsyncContinued(internalResults, updateBulkCommandText, cts, source);
					if (task3 != null)
					{
						return task3;
					}
				}
			}
			catch (Exception exception)
			{
				if (source != null)
				{
					source.TrySetException(exception);
					return source.Task;
				}
				throw;
			}
			if (source != null)
			{
				source.SetResult(null);
				return source.Task;
			}
			return null;
		}

		// Token: 0x060018F2 RID: 6386 RVA: 0x000B0B28 File Offset: 0x000AFF28
		private Task CopyBatchesAsyncContinued(BulkCopySimpleResultSet internalResults, string updateBulkCommandText, CancellationToken cts, TaskCompletionSource<object> source)
		{
			Task result;
			try
			{
				this.WriteMetaData(internalResults);
				this._parser.LoadColumnEncryptionKeys(internalResults[1].MetaData, this._connection.DataSource);
				Task task = this.CopyRowsAsync(0, this._savedBatchSize, cts, null);
				if (task != null)
				{
					if (source == null)
					{
						source = new TaskCompletionSource<object>();
					}
					AsyncHelper.ContinueTask(task, source, delegate
					{
						if (this.CopyBatchesAsyncContinuedOnSuccess(internalResults, updateBulkCommandText, cts, source) == null)
						{
							this.CopyBatchesAsync(internalResults, updateBulkCommandText, cts, source);
						}
					}, this._connection.GetOpenTdsConnection(), delegate(Exception _)
					{
						this.CopyBatchesAsyncContinuedOnError(false);
					}, delegate
					{
						this.CopyBatchesAsyncContinuedOnError(true);
					}, null, null);
					result = source.Task;
				}
				else
				{
					result = this.CopyBatchesAsyncContinuedOnSuccess(internalResults, updateBulkCommandText, cts, source);
				}
			}
			catch (Exception exception)
			{
				if (source == null)
				{
					throw;
				}
				source.TrySetException(exception);
				result = source.Task;
			}
			return result;
		}

		// Token: 0x060018F3 RID: 6387 RVA: 0x000B0C70 File Offset: 0x000B0070
		private Task CopyBatchesAsyncContinuedOnSuccess(BulkCopySimpleResultSet internalResults, string updateBulkCommandText, CancellationToken cts, TaskCompletionSource<object> source)
		{
			Task result;
			try
			{
				Task task = this._parser.WriteBulkCopyDone(this._stateObj);
				if (task == null)
				{
					this.RunParser(null);
					this.CommitTransaction();
					result = null;
				}
				else
				{
					if (source == null)
					{
						source = new TaskCompletionSource<object>();
					}
					AsyncHelper.ContinueTask(task, source, delegate
					{
						try
						{
							this.RunParser(null);
							this.CommitTransaction();
						}
						catch (Exception)
						{
							this.CopyBatchesAsyncContinuedOnError(false);
							throw;
						}
						this.CopyBatchesAsync(internalResults, updateBulkCommandText, cts, source);
					}, this._connection.GetOpenTdsConnection(), delegate(Exception _)
					{
						this.CopyBatchesAsyncContinuedOnError(false);
					}, null, null, null);
					result = source.Task;
				}
			}
			catch (Exception exception)
			{
				if (source == null)
				{
					throw;
				}
				source.TrySetException(exception);
				result = source.Task;
			}
			return result;
		}

		// Token: 0x060018F4 RID: 6388 RVA: 0x000B0D68 File Offset: 0x000B0168
		private void CopyBatchesAsyncContinuedOnError(bool cleanupParser)
		{
			SqlInternalConnectionTds openTdsConnection = this._connection.GetOpenTdsConnection();
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				if (cleanupParser && this._parser != null && this._stateObj != null)
				{
					this._parser._asyncWrite = false;
					Task task = this._parser.WriteBulkCopyDone(this._stateObj);
					this.RunParser(null);
				}
				if (this._stateObj != null)
				{
					this.CleanUpStateObject(true);
				}
			}
			catch (OutOfMemoryException)
			{
				openTdsConnection.DoomThisConnection();
				throw;
			}
			catch (StackOverflowException)
			{
				openTdsConnection.DoomThisConnection();
				throw;
			}
			catch (ThreadAbortException)
			{
				openTdsConnection.DoomThisConnection();
				throw;
			}
			this.AbortTransaction();
		}

		// Token: 0x060018F5 RID: 6389 RVA: 0x000B0E44 File Offset: 0x000B0244
		private void CleanUpStateObject(bool isCancelRequested = true)
		{
			if (this._stateObj != null)
			{
				this._parser.Connection.ThreadHasParserLockForClose = true;
				try
				{
					this._stateObj.ResetBuffer();
					this._stateObj._outputPacketNumber = 1;
					bool flag = isCancelRequested || LocalAppContextSwitches.SendCancellationAfterBulkCopySuccess;
					if (flag && (this._parser.State == TdsParserState.OpenNotLoggedIn || this._parser.State == TdsParserState.OpenLoggedIn))
					{
						this._stateObj.CancelRequest();
					}
					if (LocalAppContextSwitches.DisableHardenedQueryTimeouts)
					{
						this._stateObj._internalTimeout = false;
					}
					else
					{
						this._stateObj.SetTimeoutStateStopped();
					}
					this._stateObj.CloseSession();
					this._stateObj._bulkCopyOpperationInProgress = false;
					this._stateObj._bulkCopyWriteTimeout = false;
					this._stateObj = null;
				}
				finally
				{
					this._parser.Connection.ThreadHasParserLockForClose = false;
				}
			}
		}

		// Token: 0x060018F6 RID: 6390 RVA: 0x000B0F34 File Offset: 0x000B0334
		private void WriteToServerInternalRestContinuedAsync(BulkCopySimpleResultSet internalResults, CancellationToken cts, TaskCompletionSource<object> source)
		{
			Task task = null;
			try
			{
				string updateBulkCommandText = this.AnalyzeTargetAndCreateUpdateBulkCommand(internalResults);
				if (this._sortedColumnMappings.Count != 0)
				{
					this._stateObj.SniContext = SniContext.Snix_SendRows;
					this._savedBatchSize = this._batchSize;
					this._rowsUntilNotification = this._notifyAfter;
					this._rowsCopied = 0;
					this._currentRowMetadata = new SqlBulkCopy.SourceColumnMetadata[this._sortedColumnMappings.Count];
					for (int i = 0; i < this._currentRowMetadata.Length; i++)
					{
						this._currentRowMetadata[i] = this.GetColumnMetadata(i);
					}
					task = this.CopyBatchesAsync(internalResults, updateBulkCommandText, cts, null);
				}
				if (task != null)
				{
					if (source == null)
					{
						source = new TaskCompletionSource<object>();
					}
					AsyncHelper.ContinueTask(task, source, delegate
					{
						if (task.IsCanceled)
						{
							this._localColumnMappings = null;
							try
							{
								this.CleanUpStateObject(true);
								return;
							}
							finally
							{
								source.SetCanceled();
							}
						}
						if (task.Exception != null)
						{
							source.SetException(task.Exception.InnerException);
							return;
						}
						this._localColumnMappings = null;
						try
						{
							this.CleanUpStateObject(false);
						}
						finally
						{
							if (source != null)
							{
								if (cts.IsCancellationRequested)
								{
									source.SetCanceled();
								}
								else
								{
									source.SetResult(null);
								}
							}
						}
					}, this._connection.GetOpenTdsConnection(), null, null, null, null);
				}
				else
				{
					this._localColumnMappings = null;
					try
					{
						this.CleanUpStateObject(false);
					}
					catch (Exception ex)
					{
					}
					if (source != null)
					{
						source.SetResult(null);
					}
				}
			}
			catch (Exception exception)
			{
				this._localColumnMappings = null;
				try
				{
					this.CleanUpStateObject(true);
				}
				catch (Exception ex2)
				{
				}
				if (source == null)
				{
					throw;
				}
				source.TrySetException(exception);
			}
		}

		// Token: 0x060018F7 RID: 6391 RVA: 0x000B10EC File Offset: 0x000B04EC
		private void WriteToServerInternalRestAsync(CancellationToken cts, TaskCompletionSource<object> source)
		{
			this._hasMoreRowToCopy = true;
			Task<BulkCopySimpleResultSet> internalResultsTask = null;
			BulkCopySimpleResultSet internalResults = new BulkCopySimpleResultSet();
			SqlInternalConnectionTds openTdsConnection = this._connection.GetOpenTdsConnection();
			try
			{
				this._parser = this._connection.Parser;
				this._parser._asyncWrite = this._isAsyncBulkCopy;
				Task task;
				try
				{
					task = this._connection.ValidateAndReconnect(delegate
					{
						if (this._parserLock != null)
						{
							this._parserLock.Release();
							this._parserLock = null;
						}
					}, this.BulkCopyTimeout);
				}
				catch (SqlException inner)
				{
					throw SQL.BulkLoadInvalidDestinationTable(this._destinationTableName, inner);
				}
				if (task != null)
				{
					if (this._isAsyncBulkCopy)
					{
						CancellationTokenRegistration regReconnectCancel = default(CancellationTokenRegistration);
						TaskCompletionSource<object> cancellableReconnectTS = new TaskCompletionSource<object>();
						if (cts.CanBeCanceled)
						{
							regReconnectCancel = cts.Register(delegate()
							{
								cancellableReconnectTS.TrySetCanceled();
							});
						}
						AsyncHelper.ContinueTask(task, cancellableReconnectTS, delegate
						{
							cancellableReconnectTS.SetResult(null);
						}, null, null, null, null, null);
						AsyncHelper.SetTimeoutException(cancellableReconnectTS, this.BulkCopyTimeout, () => SQL.BulkLoadInvalidDestinationTable(this._destinationTableName, SQL.CR_ReconnectTimeout()), CancellationToken.None);
						AsyncHelper.ContinueTask(cancellableReconnectTS.Task, source, delegate
						{
							regReconnectCancel.Dispose();
							if (this._parserLock != null)
							{
								this._parserLock.Release();
								this._parserLock = null;
							}
							this._parserLock = this._connection.GetOpenTdsConnection()._parserLock;
							this._parserLock.Wait(true);
							this.WriteToServerInternalRestAsync(cts, source);
						}, null, delegate(Exception e)
						{
							regReconnectCancel.Dispose();
						}, delegate
						{
							regReconnectCancel.Dispose();
						}, (Exception ex) => SQL.BulkLoadInvalidDestinationTable(this._destinationTableName, ex), this._connection);
					}
					else
					{
						try
						{
							AsyncHelper.WaitForCompletion(task, this.BulkCopyTimeout, delegate
							{
								throw SQL.CR_ReconnectTimeout();
							}, true);
						}
						catch (SqlException inner2)
						{
							throw SQL.BulkLoadInvalidDestinationTable(this._destinationTableName, inner2);
						}
						this._parserLock = this._connection.GetOpenTdsConnection()._parserLock;
						this._parserLock.Wait(false);
						this.WriteToServerInternalRestAsync(cts, source);
					}
				}
				else
				{
					if (this._isAsyncBulkCopy)
					{
						this._connection.AddWeakReference(this, 3);
					}
					openTdsConnection.ThreadHasParserLockForClose = true;
					try
					{
						this._stateObj = this._parser.GetSession(this);
						this._stateObj._bulkCopyOpperationInProgress = true;
						this._stateObj.StartSession(this.ObjectID);
					}
					finally
					{
						openTdsConnection.ThreadHasParserLockForClose = false;
					}
					try
					{
						internalResultsTask = this.CreateAndExecuteInitialQueryAsync(out internalResults);
					}
					catch (SqlException inner3)
					{
						throw SQL.BulkLoadInvalidDestinationTable(this._destinationTableName, inner3);
					}
					if (internalResultsTask != null)
					{
						AsyncHelper.ContinueTask(internalResultsTask, source, delegate
						{
							this.WriteToServerInternalRestContinuedAsync(internalResultsTask.Result, cts, source);
						}, this._connection.GetOpenTdsConnection(), null, null, null, null);
					}
					else
					{
						this.WriteToServerInternalRestContinuedAsync(internalResults, cts, source);
					}
				}
			}
			catch (Exception exception)
			{
				if (source == null)
				{
					throw;
				}
				source.TrySetException(exception);
			}
		}

		// Token: 0x060018F8 RID: 6392 RVA: 0x000B146C File Offset: 0x000B086C
		private Task WriteToServerInternalAsync(CancellationToken ctoken)
		{
			TaskCompletionSource<object> source = null;
			Task<object> result = null;
			if (this._isAsyncBulkCopy)
			{
				source = new TaskCompletionSource<object>();
				result = source.Task;
				this.RegisterForConnectionCloseNotification<object>(ref result);
			}
			if (this._destinationTableName != null)
			{
				try
				{
					Task task = this.ReadFromRowSourceAsync(ctoken);
					if (task != null)
					{
						AsyncHelper.ContinueTask(task, source, delegate
						{
							if (!this._hasMoreRowToCopy)
							{
								source.SetResult(null);
								return;
							}
							this.WriteToServerInternalRestAsync(ctoken, source);
						}, this._connection.GetOpenTdsConnection(), null, null, null, null);
						return result;
					}
					if (!this._hasMoreRowToCopy)
					{
						if (source != null)
						{
							source.SetResult(null);
						}
						return result;
					}
					this.WriteToServerInternalRestAsync(ctoken, source);
					return result;
				}
				catch (Exception exception)
				{
					if (source == null)
					{
						throw;
					}
					source.TrySetException(exception);
				}
				return result;
			}
			if (source != null)
			{
				source.SetException(SQL.BulkLoadMissingDestinationTable());
				return result;
			}
			throw SQL.BulkLoadMissingDestinationTable();
		}

		// Token: 0x04000EB8 RID: 3768
		private const int TranCountResultId = 0;

		// Token: 0x04000EB9 RID: 3769
		private const int TranCountRowId = 0;

		// Token: 0x04000EBA RID: 3770
		private const int TranCountValueId = 0;

		// Token: 0x04000EBB RID: 3771
		private const int MetaDataResultId = 1;

		// Token: 0x04000EBC RID: 3772
		private const int CollationResultId = 2;

		// Token: 0x04000EBD RID: 3773
		private const int ColIdId = 0;

		// Token: 0x04000EBE RID: 3774
		private const int NameId = 1;

		// Token: 0x04000EBF RID: 3775
		private const int Tds_CollationId = 2;

		// Token: 0x04000EC0 RID: 3776
		private const int CollationId = 3;

		// Token: 0x04000EC1 RID: 3777
		private const int MAX_LENGTH = 2147483647;

		// Token: 0x04000EC2 RID: 3778
		private const int DefaultCommandTimeout = 30;

		// Token: 0x04000EC3 RID: 3779
		private bool _enableStreaming;

		// Token: 0x04000EC4 RID: 3780
		private int _batchSize;

		// Token: 0x04000EC5 RID: 3781
		private bool _ownConnection;

		// Token: 0x04000EC6 RID: 3782
		private SqlBulkCopyOptions _copyOptions;

		// Token: 0x04000EC7 RID: 3783
		private int _timeout = 30;

		// Token: 0x04000EC8 RID: 3784
		private string _destinationTableName;

		// Token: 0x04000EC9 RID: 3785
		private int _rowsCopied;

		// Token: 0x04000ECA RID: 3786
		private int _notifyAfter;

		// Token: 0x04000ECB RID: 3787
		private int _rowsUntilNotification;

		// Token: 0x04000ECC RID: 3788
		private bool _insideRowsCopiedEvent;

		// Token: 0x04000ECD RID: 3789
		private object _rowSource;

		// Token: 0x04000ECE RID: 3790
		private SqlDataReader _SqlDataReaderRowSource;

		// Token: 0x04000ECF RID: 3791
		private bool _rowSourceIsSqlDataReaderSmi;

		// Token: 0x04000ED0 RID: 3792
		private DbDataReader _DbDataReaderRowSource;

		// Token: 0x04000ED1 RID: 3793
		private DataTable _dataTableSource;

		// Token: 0x04000ED2 RID: 3794
		private SqlBulkCopyColumnMappingCollection _columnMappings;

		// Token: 0x04000ED3 RID: 3795
		private SqlBulkCopyColumnMappingCollection _localColumnMappings;

		// Token: 0x04000ED4 RID: 3796
		private SqlConnection _connection;

		// Token: 0x04000ED5 RID: 3797
		private SqlTransaction _internalTransaction;

		// Token: 0x04000ED6 RID: 3798
		private SqlTransaction _externalTransaction;

		// Token: 0x04000ED7 RID: 3799
		private SqlBulkCopy.ValueSourceType _rowSourceType;

		// Token: 0x04000ED8 RID: 3800
		private DataRow _currentRow;

		// Token: 0x04000ED9 RID: 3801
		private int _currentRowLength;

		// Token: 0x04000EDA RID: 3802
		private DataRowState _rowStateToSkip;

		// Token: 0x04000EDB RID: 3803
		private IEnumerator _rowEnumerator;

		// Token: 0x04000EDC RID: 3804
		private TdsParser _parser;

		// Token: 0x04000EDD RID: 3805
		private TdsParserStateObject _stateObj;

		// Token: 0x04000EDE RID: 3806
		private List<_ColumnMapping> _sortedColumnMappings;

		// Token: 0x04000EDF RID: 3807
		private SqlRowsCopiedEventHandler _rowsCopiedEventHandler;

		// Token: 0x04000EE0 RID: 3808
		private static int _objectTypeCount;

		// Token: 0x04000EE1 RID: 3809
		internal readonly int _objectID = Interlocked.Increment(ref SqlBulkCopy._objectTypeCount);

		// Token: 0x04000EE2 RID: 3810
		private int _savedBatchSize;

		// Token: 0x04000EE3 RID: 3811
		private bool _hasMoreRowToCopy;

		// Token: 0x04000EE4 RID: 3812
		private bool _isAsyncBulkCopy;

		// Token: 0x04000EE5 RID: 3813
		private bool _isBulkCopyingInProgress;

		// Token: 0x04000EE6 RID: 3814
		private SqlInternalConnectionTds.SyncAsyncLock _parserLock;

		// Token: 0x04000EE7 RID: 3815
		private SqlBulkCopy.SourceColumnMetadata[] _currentRowMetadata;

		// Token: 0x0200037C RID: 892
		private enum ValueSourceType
		{
			// Token: 0x04001F6B RID: 8043
			Unspecified,
			// Token: 0x04001F6C RID: 8044
			IDataReader,
			// Token: 0x04001F6D RID: 8045
			DataTable,
			// Token: 0x04001F6E RID: 8046
			RowArray,
			// Token: 0x04001F6F RID: 8047
			DbDataReader
		}

		// Token: 0x0200037D RID: 893
		private enum ValueMethod : byte
		{
			// Token: 0x04001F71 RID: 8049
			GetValue,
			// Token: 0x04001F72 RID: 8050
			SqlTypeSqlDecimal,
			// Token: 0x04001F73 RID: 8051
			SqlTypeSqlDouble,
			// Token: 0x04001F74 RID: 8052
			SqlTypeSqlSingle,
			// Token: 0x04001F75 RID: 8053
			DataFeedStream,
			// Token: 0x04001F76 RID: 8054
			DataFeedText,
			// Token: 0x04001F77 RID: 8055
			DataFeedXml
		}

		// Token: 0x0200037E RID: 894
		private struct SourceColumnMetadata
		{
			// Token: 0x06003468 RID: 13416 RVA: 0x00140DE0 File Offset: 0x001401E0
			public SourceColumnMetadata(SqlBulkCopy.ValueMethod method, bool isSqlType, bool isDataFeed)
			{
				this.Method = method;
				this.IsSqlType = isSqlType;
				this.IsDataFeed = isDataFeed;
			}

			// Token: 0x04001F78 RID: 8056
			public readonly SqlBulkCopy.ValueMethod Method;

			// Token: 0x04001F79 RID: 8057
			public readonly bool IsSqlType;

			// Token: 0x04001F7A RID: 8058
			public readonly bool IsDataFeed;
		}
	}
}
