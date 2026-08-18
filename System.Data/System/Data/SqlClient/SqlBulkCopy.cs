using System;
using System.Collections;
using System.Data.Common;
using System.Data.SqlTypes;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Xml;

namespace System.Data.SqlClient
{
	// Token: 0x020002B4 RID: 692
	public sealed class SqlBulkCopy : IDisposable
	{
		// Token: 0x06002305 RID: 8965 RVA: 0x0028E308 File Offset: 0x0028D708
		public SqlBulkCopy(SqlConnection connection)
		{
			if (connection == null)
			{
				throw ADP.ArgumentNull("connection");
			}
			this._connection = connection;
			this._columnMappings = new SqlBulkCopyColumnMappingCollection();
		}

		// Token: 0x06002306 RID: 8966 RVA: 0x0028E358 File Offset: 0x0028D758
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

		// Token: 0x06002307 RID: 8967 RVA: 0x0028E398 File Offset: 0x0028D798
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

		// Token: 0x06002308 RID: 8968 RVA: 0x0028E3E8 File Offset: 0x0028D7E8
		public SqlBulkCopy(string connectionString, SqlBulkCopyOptions copyOptions) : this(connectionString)
		{
			this._copyOptions = copyOptions;
		}

		// Token: 0x17000533 RID: 1331
		// (get) Token: 0x06002309 RID: 8969 RVA: 0x0028E408 File Offset: 0x0028D808
		// (set) Token: 0x0600230A RID: 8970 RVA: 0x0028E428 File Offset: 0x0028D828
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

		// Token: 0x17000534 RID: 1332
		// (get) Token: 0x0600230B RID: 8971 RVA: 0x0028E458 File Offset: 0x0028D858
		// (set) Token: 0x0600230C RID: 8972 RVA: 0x0028E478 File Offset: 0x0028D878
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

		// Token: 0x17000535 RID: 1333
		// (get) Token: 0x0600230D RID: 8973 RVA: 0x0028E498 File Offset: 0x0028D898
		public SqlBulkCopyColumnMappingCollection ColumnMappings
		{
			get
			{
				return this._columnMappings;
			}
		}

		// Token: 0x17000536 RID: 1334
		// (get) Token: 0x0600230E RID: 8974 RVA: 0x0028E4B8 File Offset: 0x0028D8B8
		// (set) Token: 0x0600230F RID: 8975 RVA: 0x0028E4D8 File Offset: 0x0028D8D8
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

		// Token: 0x17000537 RID: 1335
		// (get) Token: 0x06002310 RID: 8976 RVA: 0x0028E518 File Offset: 0x0028D918
		// (set) Token: 0x06002311 RID: 8977 RVA: 0x0028E538 File Offset: 0x0028D938
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

		// Token: 0x17000538 RID: 1336
		// (get) Token: 0x06002312 RID: 8978 RVA: 0x0028E568 File Offset: 0x0028D968
		internal int ObjectID
		{
			get
			{
				return this._objectID;
			}
		}

		// Token: 0x14000029 RID: 41
		// (add) Token: 0x06002313 RID: 8979 RVA: 0x0028E588 File Offset: 0x0028D988
		// (remove) Token: 0x06002314 RID: 8980 RVA: 0x0028E5B8 File Offset: 0x0028D9B8
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

		// Token: 0x17000539 RID: 1337
		// (get) Token: 0x06002315 RID: 8981 RVA: 0x0028E5E8 File Offset: 0x0028D9E8
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

		// Token: 0x06002316 RID: 8982 RVA: 0x0028E618 File Offset: 0x0028DA18
		void IDisposable.Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06002317 RID: 8983 RVA: 0x0028E638 File Offset: 0x0028DA38
		private bool IsCopyOption(SqlBulkCopyOptions copyOption)
		{
			return (this._copyOptions & copyOption) == copyOption;
		}

		// Token: 0x06002318 RID: 8984 RVA: 0x0028E658 File Offset: 0x0028DA58
		private BulkCopySimpleResultSet CreateAndExecuteInitialQuery()
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
			BulkCopySimpleResultSet bulkCopySimpleResultSet = new BulkCopySimpleResultSet();
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
				string text3 = array[3].Replace("'", "''");
				string text4 = array[2];
				if (text4 != null)
				{
					text4 = text4.Replace("'", "''");
				}
				string text5 = array[1];
				if (text3.Length > 0 && '#' == text3[0] && ADP.IsEmpty(text5))
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
					text += string.Format(null, "exec {0}..{1} N'{2}.{3}'", new object[]
					{
						text5,
						text2,
						text4,
						text3
					});
				}
			}
			Bid.Trace("<sc.SqlBulkCopy.CreateAndExecuteInitialQuery|INFO> Initial Query: '%ls' \n", text);
			this._parser.TdsExecuteSQLBatch(text, this.BulkCopyTimeout, null, this._stateObj);
			this._parser.Run(RunBehavior.UntilDone, null, null, bulkCopySimpleResultSet, this._stateObj);
			return bulkCopySimpleResultSet;
		}

		// Token: 0x06002319 RID: 8985 RVA: 0x0028E818 File Offset: 0x0028DC18
		private string AnalyzeTargetAndCreateUpdateBulkCommand(BulkCopySimpleResultSet internalResults)
		{
			this._sortedColumnMappings = new ArrayList();
			StringBuilder stringBuilder = new StringBuilder();
			if (this._connection.IsShiloh && internalResults[2].Count == 0)
			{
				throw SQL.BulkLoadNoCollation();
			}
			stringBuilder.Append("insert bulk " + this.DestinationTableName + " (");
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
			for (int i = 0; i < internalResults[1].MetaData.Length; i++)
			{
				_SqlMetaData sqlMetaData = internalResults[1].MetaData[i];
				bool flag2 = false;
				if (sqlMetaData.type == SqlDbType.Timestamp || (sqlMetaData.isIdentity && !this.IsCopyOption(SqlBulkCopyOptions.KeepIdentity)))
				{
					internalResults[1].MetaData[i] = null;
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
							this.AppendColumnNameAndTypeName(stringBuilder, sqlMetaData.column, sqlMetaData.type.ToString());
						}
						byte nullableType = sqlMetaData.metaType.NullableType;
						switch (nullableType)
						{
						case 41:
						case 42:
						case 43:
							stringBuilder.Append("(" + sqlMetaData.scale.ToString(null) + ")");
							break;
						default:
							switch (nullableType)
							{
							case 106:
							case 108:
								stringBuilder.Append(string.Concat(new string[]
								{
									"(",
									sqlMetaData.precision.ToString(null),
									",",
									sqlMetaData.scale.ToString(null),
									")"
								}));
								goto IL_3BE;
							case 107:
								break;
							default:
								if (nullableType == 240)
								{
									if (sqlMetaData.IsLargeUdt)
									{
										stringBuilder.Append("(max)");
										goto IL_3BE;
									}
									int length = sqlMetaData.length;
									stringBuilder.Append("(" + length.ToString(null) + ")");
									goto IL_3BE;
								}
								break;
							}
							if (!sqlMetaData.metaType.IsFixed && !sqlMetaData.metaType.IsLong)
							{
								int num3 = sqlMetaData.length;
								byte nullableType2 = sqlMetaData.metaType.NullableType;
								if (nullableType2 == 99 || nullableType2 == 231 || nullableType2 == 239)
								{
									num3 /= 2;
								}
								stringBuilder.Append("(" + num3.ToString(null) + ")");
							}
							else if (sqlMetaData.metaType.IsPlp && sqlMetaData.metaType.SqlDbType != SqlDbType.Xml)
							{
								stringBuilder.Append("(max)");
							}
							break;
						}
						IL_3BE:
						if (!this._connection.IsShiloh)
						{
							break;
						}
						Result result = internalResults[2];
						object obj = result[i][3];
						if (obj == null)
						{
							break;
						}
						SqlString sqlString = (SqlString)obj;
						if (sqlString.IsNull)
						{
							break;
						}
						stringBuilder.Append(" COLLATE " + sqlString.ToString());
						if (this._SqlDataReaderRowSource == null)
						{
							break;
						}
						int internalSourceColumnOrdinal = this._localColumnMappings[j]._internalSourceColumnOrdinal;
						int lcid = internalResults[1].MetaData[i].collation.LCID;
						int localeId = this._SqlDataReaderRowSource.GetLocaleId(internalSourceColumnOrdinal);
						if (localeId != lcid)
						{
							throw SQL.BulkLoadLcidMismatch(localeId, this._SqlDataReaderRowSource.GetName(internalSourceColumnOrdinal), lcid, sqlMetaData.column);
						}
						break;
					}
					else
					{
						j++;
					}
				}
				if (j == this._localColumnMappings.Count)
				{
					internalResults[1].MetaData[i] = null;
				}
			}
			if (num + num2 != this._localColumnMappings.Count)
			{
				throw SQL.BulkLoadNonMatchingColumnMapping();
			}
			stringBuilder.Append(")");
			if ((this._copyOptions & (SqlBulkCopyOptions.CheckConstraints | SqlBulkCopyOptions.TableLock | SqlBulkCopyOptions.KeepNulls | SqlBulkCopyOptions.FireTriggers)) != SqlBulkCopyOptions.Default)
			{
				bool flag3 = false;
				stringBuilder.Append(" with (");
				if (this.IsCopyOption(SqlBulkCopyOptions.KeepNulls))
				{
					stringBuilder.Append("KEEP_NULLS");
					flag3 = true;
				}
				if (this.IsCopyOption(SqlBulkCopyOptions.TableLock))
				{
					stringBuilder.Append((flag3 ? ", " : "") + "TABLOCK");
					flag3 = true;
				}
				if (this.IsCopyOption(SqlBulkCopyOptions.CheckConstraints))
				{
					stringBuilder.Append((flag3 ? ", " : "") + "CHECK_CONSTRAINTS");
					flag3 = true;
				}
				if (this.IsCopyOption(SqlBulkCopyOptions.FireTriggers))
				{
					stringBuilder.Append((flag3 ? ", " : "") + "FIRE_TRIGGERS");
				}
				stringBuilder.Append(")");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600231A RID: 8986 RVA: 0x0028EDF8 File Offset: 0x0028E1F8
		private void SubmitUpdateBulkCommand(BulkCopySimpleResultSet internalResults, string TDSCommand)
		{
			this._parser.TdsExecuteSQLBatch(TDSCommand, this.BulkCopyTimeout, null, this._stateObj);
			this._parser.Run(RunBehavior.UntilDone, null, null, null, this._stateObj);
		}

		// Token: 0x0600231B RID: 8987 RVA: 0x0028EE38 File Offset: 0x0028E238
		private void WriteMetaData(BulkCopySimpleResultSet internalResults)
		{
			this._stateObj.SetTimeoutSeconds(this.BulkCopyTimeout);
			_SqlMetaDataSet metaData = internalResults[1].MetaData;
			this._stateObj._outputMessageType = 7;
			this._parser.WriteBulkCopyMetaData(metaData, this._sortedColumnMappings.Count, this._stateObj);
		}

		// Token: 0x0600231C RID: 8988 RVA: 0x0028EE98 File Offset: 0x0028E298
		public void Close()
		{
			if (this._insideRowsCopiedEvent)
			{
				throw SQL.InvalidOperationInsideEvent();
			}
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600231D RID: 8989 RVA: 0x0028EEC8 File Offset: 0x0028E2C8
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

		// Token: 0x0600231E RID: 8990 RVA: 0x0028EF88 File Offset: 0x0028E388
		private object GetValueFromSourceRow(int columnOrdinal, _SqlMetaData metadata, int[] UseSqlValue, int destRowIndex)
		{
			if (UseSqlValue[destRowIndex] == 0)
			{
				UseSqlValue[destRowIndex] = -1;
				if (metadata.metaType.NullableType == 106 || metadata.metaType.NullableType == 108)
				{
					Type type = null;
					switch (this._rowSourceType)
					{
					case SqlBulkCopy.ValueSourceType.IDataReader:
						if (this._SqlDataReaderRowSource != null)
						{
							type = this._SqlDataReaderRowSource.GetFieldType(columnOrdinal);
						}
						break;
					case SqlBulkCopy.ValueSourceType.DataTable:
					case SqlBulkCopy.ValueSourceType.RowArray:
						type = this._currentRow.Table.Columns[columnOrdinal].DataType;
						break;
					}
					if (typeof(SqlDecimal) == type || typeof(decimal) == type)
					{
						UseSqlValue[destRowIndex] = 4;
					}
					else if (typeof(SqlDouble) == type || typeof(double) == type)
					{
						UseSqlValue[destRowIndex] = 5;
					}
					else if (typeof(SqlSingle) == type || typeof(float) == type)
					{
						UseSqlValue[destRowIndex] = 10;
					}
				}
			}
			switch (this._rowSourceType)
			{
			case SqlBulkCopy.ValueSourceType.IDataReader:
			{
				if (this._SqlDataReaderRowSource == null)
				{
					return ((IDataReader)this._rowSource).GetValue(columnOrdinal);
				}
				int num = UseSqlValue[destRowIndex];
				switch (num)
				{
				case 4:
					return this._SqlDataReaderRowSource.GetSqlDecimal(columnOrdinal);
				case 5:
					return new SqlDecimal(this._SqlDataReaderRowSource.GetSqlDouble(columnOrdinal).Value);
				default:
					if (num != 10)
					{
						return this._SqlDataReaderRowSource.GetValue(columnOrdinal);
					}
					return new SqlDecimal((double)this._SqlDataReaderRowSource.GetSqlSingle(columnOrdinal).Value);
				}
				break;
			}
			case SqlBulkCopy.ValueSourceType.DataTable:
			case SqlBulkCopy.ValueSourceType.RowArray:
			{
				object obj = this._currentRow[columnOrdinal];
				if (obj != null && DBNull.Value != obj && (10 == UseSqlValue[destRowIndex] || 5 == UseSqlValue[destRowIndex] || 4 == UseSqlValue[destRowIndex]))
				{
					INullable nullable = obj as INullable;
					if (nullable == null || !nullable.IsNull)
					{
						SqlBuffer.StorageType storageType = (SqlBuffer.StorageType)UseSqlValue[destRowIndex];
						switch (storageType)
						{
						case SqlBuffer.StorageType.Decimal:
							if (nullable != null)
							{
								return (SqlDecimal)obj;
							}
							return new SqlDecimal((decimal)obj);
						case SqlBuffer.StorageType.Double:
						{
							if (nullable != null)
							{
								return new SqlDecimal(((SqlDouble)obj).Value);
							}
							double num2 = (double)obj;
							if (!double.IsNaN(num2))
							{
								return new SqlDecimal(num2);
							}
							break;
						}
						default:
							if (storageType == SqlBuffer.StorageType.Single)
							{
								if (nullable != null)
								{
									return new SqlDecimal((double)((SqlSingle)obj).Value);
								}
								float num3 = (float)obj;
								if (!float.IsNaN(num3))
								{
									return new SqlDecimal((double)num3);
								}
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

		// Token: 0x0600231F RID: 8991 RVA: 0x0028F248 File Offset: 0x0028E648
		private bool ReadFromRowSource()
		{
			switch (this._rowSourceType)
			{
			case SqlBulkCopy.ValueSourceType.IDataReader:
				return ((IDataReader)this._rowSource).Read();
			case SqlBulkCopy.ValueSourceType.DataTable:
			case SqlBulkCopy.ValueSourceType.RowArray:
				while (this._rowEnumerator.MoveNext())
				{
					this._currentRow = (DataRow)this._rowEnumerator.Current;
					if ((this._currentRow.RowState & DataRowState.Deleted) == (DataRowState)0 && (this._rowState == (DataRowState)0 || (this._currentRow.RowState & this._rowState) != (DataRowState)0))
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

		// Token: 0x06002320 RID: 8992 RVA: 0x0028F2F8 File Offset: 0x0028E6F8
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

		// Token: 0x06002321 RID: 8993 RVA: 0x0028F378 File Offset: 0x0028E778
		private void AppendColumnNameAndTypeName(StringBuilder query, string columnName, string typeName)
		{
			query.Append('[');
			query.Append(columnName.Replace("]", "]]"));
			query.Append("] ");
			query.Append(typeName);
		}

		// Token: 0x06002322 RID: 8994 RVA: 0x0028F3C8 File Offset: 0x0028E7C8
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

		// Token: 0x06002323 RID: 8995 RVA: 0x0028F408 File Offset: 0x0028E808
		private object ValidateBulkCopyVariant(object value)
		{
			MetaType metaTypeFromValue = MetaType.GetMetaTypeFromValue(value);
			byte tdstype = metaTypeFromValue.TDSType;
			if (tdstype <= 108)
			{
				switch (tdstype)
				{
				case 36:
				case 40:
				case 41:
				case 42:
				case 43:
					break;
				case 37:
				case 38:
				case 39:
					goto IL_C1;
				default:
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
						goto IL_C1;
					default:
						if (tdstype != 108)
						{
							goto IL_C1;
						}
						break;
					}
					break;
				}
			}
			else if (tdstype != 127)
			{
				switch (tdstype)
				{
				case 165:
				case 167:
					break;
				case 166:
					goto IL_C1;
				default:
					if (tdstype != 231)
					{
						goto IL_C1;
					}
					break;
				}
			}
			if (value is INullable)
			{
				return MetaType.GetComValueFromSqlVariant(value);
			}
			return value;
			IL_C1:
			throw SQL.BulkLoadInvalidVariantValue();
		}

		// Token: 0x06002324 RID: 8996 RVA: 0x0028F4E8 File Offset: 0x0028E8E8
		private object ConvertValue(object value, _SqlMetaData metadata)
		{
			if (!ADP.IsNull(value))
			{
				MetaType metaType = metadata.metaType;
				object result;
				try
				{
					byte nullableType = metaType.NullableType;
					MetaType metaTypeFromSqlDbType;
					if (nullableType <= 111)
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
							goto IL_278;
						default:
							switch (nullableType)
							{
							case 58:
							case 59:
							case 61:
							case 62:
								break;
							case 60:
								goto IL_278;
							default:
								switch (nullableType)
								{
								case 98:
									value = this.ValidateBulkCopyVariant(value);
									goto IL_28B;
								case 99:
									goto IL_1E1;
								case 100:
								case 101:
								case 102:
								case 103:
								case 105:
								case 107:
									goto IL_278;
								case 104:
								case 109:
								case 110:
								case 111:
									break;
								case 106:
								case 108:
								{
									metaTypeFromSqlDbType = MetaType.GetMetaTypeFromSqlDbType(metaType.SqlDbType, false);
									value = SqlParameter.CoerceValue(value, metaTypeFromSqlDbType);
									SqlDecimal sqlDecimal;
									if (value is SqlDecimal)
									{
										sqlDecimal = (SqlDecimal)value;
									}
									else
									{
										sqlDecimal = new SqlDecimal((decimal)value);
									}
									if (sqlDecimal.Scale != metadata.scale)
									{
										sqlDecimal = TdsParser.AdjustSqlDecimalScale(sqlDecimal, (int)metadata.scale);
										value = sqlDecimal;
									}
									if (sqlDecimal.Precision > metadata.precision)
									{
										throw SQL.BulkLoadCannotConvertValue(value.GetType(), metaTypeFromSqlDbType, ADP.ParameterValueOutOfRange(sqlDecimal));
									}
									goto IL_28B;
								}
								default:
									goto IL_278;
								}
								break;
							}
							break;
						}
					}
					else if (nullableType <= 175)
					{
						switch (nullableType)
						{
						case 165:
						case 167:
							break;
						case 166:
							goto IL_278;
						default:
							switch (nullableType)
							{
							case 173:
							case 175:
								break;
							case 174:
								goto IL_278;
							default:
								goto IL_278;
							}
							break;
						}
					}
					else
					{
						if (nullableType == 231)
						{
							goto IL_1E1;
						}
						switch (nullableType)
						{
						case 239:
							goto IL_1E1;
						case 240:
							if (value.GetType() != typeof(byte[]))
							{
								value = this._connection.GetBytes(value);
								goto IL_28B;
							}
							goto IL_28B;
						case 241:
							if (value is XmlReader)
							{
								value = MetaType.GetStringFromXml((XmlReader)value);
								goto IL_28B;
							}
							goto IL_28B;
						default:
							goto IL_278;
						}
					}
					metaTypeFromSqlDbType = MetaType.GetMetaTypeFromSqlDbType(metaType.SqlDbType, false);
					value = SqlParameter.CoerceValue(value, metaTypeFromSqlDbType);
					goto IL_28B;
					IL_1E1:
					metaTypeFromSqlDbType = MetaType.GetMetaTypeFromSqlDbType(metaType.SqlDbType, false);
					value = SqlParameter.CoerceValue(value, metaTypeFromSqlDbType);
					int num = (value is string) ? ((string)value).Length : ((SqlString)value).Value.Length;
					if (num > metadata.length / 2)
					{
						throw SQL.BulkLoadStringTooLong();
					}
					goto IL_28B;
					IL_278:
					throw SQL.BulkLoadCannotConvertValue(value.GetType(), metadata.metaType, null);
					IL_28B:
					result = value;
				}
				catch (Exception e)
				{
					if (!ADP.IsCatchableExceptionType(e))
					{
						throw;
					}
					throw SQL.BulkLoadCannotConvertValue(value.GetType(), metadata.metaType, e);
				}
				return result;
			}
			if (!metadata.isNullable)
			{
				throw SQL.BulkLoadBulkLoadNotAllowDBNull(metadata.column);
			}
			return value;
		}

		// Token: 0x06002325 RID: 8997 RVA: 0x0028F7C8 File Offset: 0x0028EBC8
		public void WriteToServer(IDataReader reader)
		{
			SqlConnection.ExecutePermission.Demand();
			SqlStatistics statistics = this.Statistics;
			try
			{
				statistics = SqlStatistics.StartTimer(this.Statistics);
				if (reader == null)
				{
					throw new ArgumentNullException("reader");
				}
				this._rowSource = reader;
				this._SqlDataReaderRowSource = (this._rowSource as SqlDataReader);
				this._rowSourceType = SqlBulkCopy.ValueSourceType.IDataReader;
				this.WriteRowSourceToServer(reader.FieldCount);
			}
			finally
			{
				SqlStatistics.StopTimer(statistics);
			}
		}

		// Token: 0x06002326 RID: 8998 RVA: 0x0028F858 File Offset: 0x0028EC58
		public void WriteToServer(DataTable table)
		{
			this.WriteToServer(table, (DataRowState)0);
		}

		// Token: 0x06002327 RID: 8999 RVA: 0x0028F878 File Offset: 0x0028EC78
		public void WriteToServer(DataTable table, DataRowState rowState)
		{
			SqlConnection.ExecutePermission.Demand();
			SqlStatistics statistics = this.Statistics;
			try
			{
				statistics = SqlStatistics.StartTimer(this.Statistics);
				if (table == null)
				{
					throw new ArgumentNullException("table");
				}
				this._rowState = (rowState & ~DataRowState.Deleted);
				this._rowSource = table;
				this._SqlDataReaderRowSource = null;
				this._rowSourceType = SqlBulkCopy.ValueSourceType.DataTable;
				this._rowEnumerator = table.Rows.GetEnumerator();
				this.WriteRowSourceToServer(table.Columns.Count);
			}
			finally
			{
				SqlStatistics.StopTimer(statistics);
			}
		}

		// Token: 0x06002328 RID: 9000 RVA: 0x0028F918 File Offset: 0x0028ED18
		public void WriteToServer(DataRow[] rows)
		{
			SqlConnection.ExecutePermission.Demand();
			SqlStatistics statistics = this.Statistics;
			try
			{
				statistics = SqlStatistics.StartTimer(this.Statistics);
				if (rows == null)
				{
					throw new ArgumentNullException("rows");
				}
				if (rows.Length != 0)
				{
					DataTable table = rows[0].Table;
					this._rowState = (DataRowState)0;
					this._rowSource = rows;
					this._SqlDataReaderRowSource = null;
					this._rowSourceType = SqlBulkCopy.ValueSourceType.RowArray;
					this._rowEnumerator = rows.GetEnumerator();
					this.WriteRowSourceToServer(table.Columns.Count);
				}
			}
			finally
			{
				SqlStatistics.StopTimer(statistics);
			}
		}

		// Token: 0x06002329 RID: 9001 RVA: 0x0028F9C8 File Offset: 0x0028EDC8
		private void WriteRowSourceToServer(int columnCount)
		{
			this.CreateOrValidateConnection("WriteToServer");
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			SNIHandle target = null;
			try
			{
				target = SqlInternalConnection.GetBestEffortCleanupTarget(this._connection);
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
						goto IL_B7;
					}
				}
				this._localColumnMappings = new SqlBulkCopyColumnMappingCollection();
				this._localColumnMappings.CreateDefaultMapping(columnCount);
				IL_B7:
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
				this.WriteToServerInternal();
			}
			catch (OutOfMemoryException e2)
			{
				this._connection.Abort(e2);
				throw;
			}
			catch (StackOverflowException e3)
			{
				this._connection.Abort(e3);
				throw;
			}
			catch (ThreadAbortException e4)
			{
				this._connection.Abort(e4);
				SqlInternalConnection.BestEffortCleanup(target);
				throw;
			}
			finally
			{
				this._columnMappings.ReadOnly = false;
			}
		}

		// Token: 0x0600232A RID: 9002 RVA: 0x0028FC98 File Offset: 0x0028F098
		private void WriteToServerInternal()
		{
			string tdscommand = null;
			bool flag = false;
			bool flag2 = false;
			int[] array = null;
			int batchSize = this._batchSize;
			bool flag3 = false;
			if (this._batchSize > 0)
			{
				flag3 = true;
			}
			Exception ex = null;
			this._rowsCopied = 0;
			if (this._destinationTableName == null)
			{
				throw SQL.BulkLoadMissingDestinationTable();
			}
			if (!this.ReadFromRowSource())
			{
				return;
			}
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				bool flag4 = true;
				this._parser = this._connection.Parser;
				this._stateObj = this._parser.GetSession(this);
				this._stateObj._bulkCopyOpperationInProgress = true;
				try
				{
					this._stateObj.StartSession(this.ObjectID);
					BulkCopySimpleResultSet internalResults;
					try
					{
						internalResults = this.CreateAndExecuteInitialQuery();
					}
					catch (SqlException inner)
					{
						throw SQL.BulkLoadInvalidDestinationTable(this._destinationTableName, inner);
					}
					this._rowsUntilNotification = this._notifyAfter;
					tdscommand = this.AnalyzeTargetAndCreateUpdateBulkCommand(internalResults);
					if (this._sortedColumnMappings.Count != 0)
					{
						this._stateObj.SniContext = SniContext.Snix_SendRows;
						for (;;)
						{
							if (this.IsCopyOption(SqlBulkCopyOptions.UseInternalTransaction))
							{
								this._internalTransaction = this._connection.BeginTransaction();
							}
							this.SubmitUpdateBulkCommand(internalResults, tdscommand);
							try
							{
								this.WriteMetaData(internalResults);
								object[] array2 = new object[this._sortedColumnMappings.Count];
								if (array == null)
								{
									array = new int[array2.Length];
								}
								int num = batchSize;
								do
								{
									for (int i = 0; i < array2.Length; i++)
									{
										_ColumnMapping columnMapping = (_ColumnMapping)this._sortedColumnMappings[i];
										_SqlMetaData metadata = columnMapping._metadata;
										object valueFromSourceRow = this.GetValueFromSourceRow(columnMapping._sourceColumnOrdinal, metadata, array, i);
										array2[i] = this.ConvertValue(valueFromSourceRow, metadata);
									}
									this._parser.WriteByte(209, this._stateObj);
									for (int j = 0; j < array2.Length; j++)
									{
										_ColumnMapping columnMapping2 = (_ColumnMapping)this._sortedColumnMappings[j];
										_SqlMetaData metadata2 = columnMapping2._metadata;
										if (metadata2.type != SqlDbType.Variant)
										{
											this._parser.WriteBulkCopyValue(array2[j], metadata2, this._stateObj);
										}
										else
										{
											this._parser.WriteSqlVariantDataRowValue(array2[j], this._stateObj);
										}
									}
									this._rowsCopied++;
									if (this._notifyAfter > 0 && this._rowsUntilNotification > 0 && --this._rowsUntilNotification == 0)
									{
										try
										{
											this._stateObj.BcpLock = true;
											flag2 = this.FireRowsCopiedEvent((long)this._rowsCopied);
											Bid.Trace("<sc.SqlBulkCopy.WriteToServerInternal|INFO> \n");
											if (ConnectionState.Open != this._connection.State)
											{
												break;
											}
										}
										catch (Exception ex2)
										{
											if (!ADP.IsCatchableExceptionType(ex2))
											{
												throw;
											}
											ex = OperationAbortedException.Aborted(ex2);
											break;
										}
										finally
										{
											this._stateObj.BcpLock = false;
										}
										if (flag2)
										{
											break;
										}
										this._rowsUntilNotification = this._notifyAfter;
									}
									if (this._rowsUntilNotification > this._notifyAfter)
									{
										this._rowsUntilNotification = this._notifyAfter;
									}
									flag = this.ReadFromRowSource();
									if (flag3)
									{
										num--;
										if (num == 0)
										{
											break;
										}
									}
								}
								while (flag);
							}
							catch (Exception e)
							{
								if (ADP.IsCatchableExceptionType(e))
								{
									this._stateObj.CancelRequest();
								}
								throw;
							}
							if (ConnectionState.Open != this._connection.State)
							{
								break;
							}
							this._parser.WriteBulkCopyDone(this._stateObj);
							this._parser.Run(RunBehavior.UntilDone, null, null, null, this._stateObj);
							if (flag2 || ex != null)
							{
								goto IL_33A;
							}
							if (this._internalTransaction != null)
							{
								this._internalTransaction.Commit();
								this._internalTransaction = null;
							}
							if (!flag)
							{
								goto Block_16;
							}
						}
						throw ADP.OpenConnectionRequired("WriteToServer", this._connection.State);
						IL_33A:
						throw OperationAbortedException.Aborted(ex);
						Block_16:
						this._localColumnMappings = null;
					}
				}
				catch (Exception e2)
				{
					flag4 = ADP.IsCatchableExceptionType(e2);
					if (flag4)
					{
						this._stateObj._internalTimeout = false;
						if (this._internalTransaction != null)
						{
							if (!this._internalTransaction.IsZombied)
							{
								this._internalTransaction.Rollback();
							}
							this._internalTransaction = null;
						}
					}
					throw;
				}
				finally
				{
					if (flag4 && this._stateObj != null)
					{
						this._stateObj.CloseSession();
					}
				}
			}
			finally
			{
				if (this._stateObj != null)
				{
					this._stateObj._bulkCopyOpperationInProgress = false;
					this._stateObj = null;
				}
			}
		}

		// Token: 0x0600232B RID: 9003 RVA: 0x00290148 File Offset: 0x0028F548
		private void OnRowsCopied(SqlRowsCopiedEventArgs value)
		{
			SqlRowsCopiedEventHandler rowsCopiedEventHandler = this._rowsCopiedEventHandler;
			if (rowsCopiedEventHandler != null)
			{
				rowsCopiedEventHandler(this, value);
			}
		}

		// Token: 0x0600232C RID: 9004 RVA: 0x00290168 File Offset: 0x0028F568
		private bool FireRowsCopiedEvent(long rowsCopied)
		{
			SqlRowsCopiedEventArgs sqlRowsCopiedEventArgs = new SqlRowsCopiedEventArgs(rowsCopied);
			try
			{
				this._insideRowsCopiedEvent = true;
				this.OnRowsCopied(sqlRowsCopiedEventArgs);
			}
			finally
			{
				this._insideRowsCopiedEvent = false;
			}
			return sqlRowsCopiedEventArgs.Abort;
		}

		// Token: 0x040016CA RID: 5834
		private const int TranCountResultId = 0;

		// Token: 0x040016CB RID: 5835
		private const int TranCountRowId = 0;

		// Token: 0x040016CC RID: 5836
		private const int TranCountValueId = 0;

		// Token: 0x040016CD RID: 5837
		private const int MetaDataResultId = 1;

		// Token: 0x040016CE RID: 5838
		private const int CollationResultId = 2;

		// Token: 0x040016CF RID: 5839
		private const int ColIdId = 0;

		// Token: 0x040016D0 RID: 5840
		private const int NameId = 1;

		// Token: 0x040016D1 RID: 5841
		private const int Tds_CollationId = 2;

		// Token: 0x040016D2 RID: 5842
		private const int CollationId = 3;

		// Token: 0x040016D3 RID: 5843
		private const int DefaultCommandTimeout = 30;

		// Token: 0x040016D4 RID: 5844
		private int _batchSize;

		// Token: 0x040016D5 RID: 5845
		private bool _ownConnection;

		// Token: 0x040016D6 RID: 5846
		private SqlBulkCopyOptions _copyOptions;

		// Token: 0x040016D7 RID: 5847
		private int _timeout = 30;

		// Token: 0x040016D8 RID: 5848
		private string _destinationTableName;

		// Token: 0x040016D9 RID: 5849
		private int _rowsCopied;

		// Token: 0x040016DA RID: 5850
		private int _notifyAfter;

		// Token: 0x040016DB RID: 5851
		private int _rowsUntilNotification;

		// Token: 0x040016DC RID: 5852
		private bool _insideRowsCopiedEvent;

		// Token: 0x040016DD RID: 5853
		private object _rowSource;

		// Token: 0x040016DE RID: 5854
		private SqlDataReader _SqlDataReaderRowSource;

		// Token: 0x040016DF RID: 5855
		private SqlBulkCopyColumnMappingCollection _columnMappings;

		// Token: 0x040016E0 RID: 5856
		private SqlBulkCopyColumnMappingCollection _localColumnMappings;

		// Token: 0x040016E1 RID: 5857
		private SqlConnection _connection;

		// Token: 0x040016E2 RID: 5858
		private SqlTransaction _internalTransaction;

		// Token: 0x040016E3 RID: 5859
		private SqlTransaction _externalTransaction;

		// Token: 0x040016E4 RID: 5860
		private SqlBulkCopy.ValueSourceType _rowSourceType;

		// Token: 0x040016E5 RID: 5861
		private DataRow _currentRow;

		// Token: 0x040016E6 RID: 5862
		private int _currentRowLength;

		// Token: 0x040016E7 RID: 5863
		private DataRowState _rowState;

		// Token: 0x040016E8 RID: 5864
		private IEnumerator _rowEnumerator;

		// Token: 0x040016E9 RID: 5865
		private TdsParser _parser;

		// Token: 0x040016EA RID: 5866
		private TdsParserStateObject _stateObj;

		// Token: 0x040016EB RID: 5867
		private ArrayList _sortedColumnMappings;

		// Token: 0x040016EC RID: 5868
		private SqlRowsCopiedEventHandler _rowsCopiedEventHandler;

		// Token: 0x040016ED RID: 5869
		private static int _objectTypeCount;

		// Token: 0x040016EE RID: 5870
		internal readonly int _objectID = Interlocked.Increment(ref SqlBulkCopy._objectTypeCount);

		// Token: 0x020002B5 RID: 693
		private enum ValueSourceType
		{
			// Token: 0x040016F0 RID: 5872
			Unspecified,
			// Token: 0x040016F1 RID: 5873
			IDataReader,
			// Token: 0x040016F2 RID: 5874
			DataTable,
			// Token: 0x040016F3 RID: 5875
			RowArray
		}
	}
}
