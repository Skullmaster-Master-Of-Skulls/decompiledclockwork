using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Globalization;
using System.Text;
using System.Xml;
using Oracle.ManagedDataAccess.Types;
using Oracle.SqlAndPlsqlParser.LocalParsing;
using OracleInternal.Common;
using OracleInternal.ServiceObjects;
using OracleInternal.TTC.Accessors;

namespace Oracle.ManagedDataAccess.Client
{
	// Token: 0x02000063 RID: 99
	public sealed class OracleDataReader : DbDataReader
	{
		// Token: 0x060004AD RID: 1197 RVA: 0x00025638 File Offset: 0x00023838
		internal OracleDataReader(OracleDataReaderImpl readerImpl, OracleConnection connection, long fetchSize, long clientInitialLOBFS, long internalInitialLOBFS, int initialLongFetchSize, int recordsAffected, string commandText, SqlStatementType sqlStatementType, CommandBehavior behavior = CommandBehavior.Default)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				if (sqlStatementType == SqlStatementType.DML)
				{
					this.m_recordsAffected = recordsAffected;
				}
				else
				{
					this.m_recordsAffected = -1;
				}
				this.m_connection = connection;
				this.m_sqlStatementType = sqlStatementType;
				this.m_commandBehavior = behavior;
				this.m_commandText = commandText;
				this.m_bclosed = false;
				if (readerImpl == null)
				{
					this.m_bEndOfFile = true;
				}
				else
				{
					this.m_readerImpl = readerImpl;
					this.m_fetchSize = fetchSize;
					readerImpl.m_clientInitialLOBFS = clientInitialLOBFS;
					readerImpl.m_internalInitialLOBFS = internalInitialLOBFS;
					this.m_initialLongFetchSize = initialLongFetchSize;
					if (this.m_readerImpl.m_sqlMetaData != null)
					{
						this.m_maxRowSize = this.m_readerImpl.m_sqlMetaData.m_maxRowSize + this.m_readerImpl.m_sqlMetaData.m_numOfLOBColumns * Math.Max(86, 86 + (int)clientInitialLOBFS) + this.m_readerImpl.m_sqlMetaData.m_numOfLONGColumns * Math.Max(2, this.m_initialLongFetchSize) + this.m_readerImpl.m_sqlMetaData.m_numOfBFileColumns * 86;
						this.m_fieldCount = (int)this.m_readerImpl.m_sqlMetaData.m_noOfColumns - this.m_readerImpl.m_numberOfHiddenColumns;
						if (this.m_readerImpl.m_sqlMetaData.HasLOBOrLongColumn)
						{
							this.m_LobImplCache = new object[this.m_fieldCount];
							this.m_LastCachedRowNumber = new long[this.m_fieldCount];
						}
					}
					this.m_readerImpl.OnClose = delegate()
					{
						if (!this.m_bclosed)
						{
							this.m_readerImpl = null;
							this.Close();
						}
					};
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
		}

		// Token: 0x060004AE RID: 1198 RVA: 0x00025878 File Offset: 0x00023A78
		protected override void Finalize()
		{
			try
			{
				this.Dispose(false);
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
			}
			finally
			{
				base.Finalize();
			}
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x060004AF RID: 1199 RVA: 0x000258C4 File Offset: 0x00023AC4
		public override int Depth
		{
			get
			{
				if (this.m_bclosed)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.OBJECT_CLOSED, new string[0]));
				}
				return 0;
			}
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x060004B0 RID: 1200 RVA: 0x000258E8 File Offset: 0x00023AE8
		// (set) Token: 0x060004B1 RID: 1201 RVA: 0x00025910 File Offset: 0x00023B10
		public long FetchSize
		{
			get
			{
				if (this.m_bclosed)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.OBJECT_CLOSED, new string[0]));
				}
				return this.m_fetchSize;
			}
			set
			{
				if (this.m_bclosed)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.OBJECT_CLOSED, new string[0]));
				}
				if (value <= 0L)
				{
					throw new ArgumentException();
				}
				this.m_fetchSize = value;
			}
		}

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x060004B2 RID: 1202 RVA: 0x00025944 File Offset: 0x00023B44
		public override int FieldCount
		{
			get
			{
				if (this.m_bclosed)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.OBJECT_CLOSED, new string[0]));
				}
				if (this.m_fieldCount <= 0 && this.m_readerImpl != null && this.m_readerImpl.m_accessors != null)
				{
					this.m_fieldCount = this.m_readerImpl.m_accessors.Length - this.m_readerImpl.m_numberOfHiddenColumns;
				}
				return this.m_fieldCount;
			}
		}

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x060004B3 RID: 1203 RVA: 0x000259B4 File Offset: 0x00023BB4
		public override bool HasRows
		{
			get
			{
				if (this.m_bclosed)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.OBJECT_CLOSED, new string[0]));
				}
				if (!this.m_bHasRows && !this.m_bDoneReadOne && this.Read())
				{
					this.m_internalRowCounter = -1;
				}
				return this.m_bHasRows;
			}
		}

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x060004B4 RID: 1204 RVA: 0x00025A04 File Offset: 0x00023C04
		public override int VisibleFieldCount
		{
			get
			{
				if (this.m_bclosed)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.OBJECT_CLOSED, new string[0]));
				}
				if (this.m_fieldCount <= 0 && this.m_readerImpl != null && this.m_readerImpl.m_accessors != null)
				{
					this.m_fieldCount = this.m_readerImpl.m_accessors.Length - this.m_readerImpl.m_numberOfHiddenColumns;
				}
				return this.m_fieldCount;
			}
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x060004B5 RID: 1205 RVA: 0x00025A74 File Offset: 0x00023C74
		public int HiddenFieldCount
		{
			get
			{
				if (this.m_bclosed)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.OBJECT_CLOSED, new string[0]));
				}
				if (this.m_readerImpl == null)
				{
					return 0;
				}
				return this.m_readerImpl.m_numberOfHiddenColumns;
			}
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x060004B6 RID: 1206 RVA: 0x00025AAC File Offset: 0x00023CAC
		public int InitialLONGFetchSize
		{
			get
			{
				if (this.m_bclosed)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.OBJECT_CLOSED, new string[0]));
				}
				if (this.m_readerImpl != null)
				{
					return (int)this.m_readerImpl.m_initialLongFS;
				}
				return 0;
			}
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x060004B7 RID: 1207 RVA: 0x00025AE4 File Offset: 0x00023CE4
		public int InitialLOBFetchSize
		{
			get
			{
				if (this.m_bclosed)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.OBJECT_CLOSED, new string[0]));
				}
				if (this.m_readerImpl != null)
				{
					return (int)this.m_readerImpl.m_clientInitialLOBFS;
				}
				return 0;
			}
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x060004B8 RID: 1208 RVA: 0x00025B1C File Offset: 0x00023D1C
		public override bool IsClosed
		{
			get
			{
				return this.m_bclosed;
			}
		}

		// Token: 0x17000133 RID: 307
		public override object this[string columnName]
		{
			get
			{
				return this[this.GetOrdinal(columnName)];
			}
		}

		// Token: 0x17000134 RID: 308
		public override object this[int i]
		{
			get
			{
				return this.GetValue(i);
			}
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x060004BB RID: 1211 RVA: 0x00025B40 File Offset: 0x00023D40
		public override int RecordsAffected
		{
			get
			{
				return this.m_recordsAffected;
			}
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x060004BC RID: 1212 RVA: 0x00025B48 File Offset: 0x00023D48
		internal int CurrentRow
		{
			get
			{
				return this.m_RowNumber;
			}
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x060004BD RID: 1213 RVA: 0x00025B50 File Offset: 0x00023D50
		public long RowSize
		{
			get
			{
				return (long)this.m_maxRowSize;
			}
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x060004BE RID: 1214 RVA: 0x00025B5C File Offset: 0x00023D5C
		// (set) Token: 0x060004BF RID: 1215 RVA: 0x00025B84 File Offset: 0x00023D84
		internal bool IsFillReader
		{
			get
			{
				if (this.m_bclosed)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.OBJECT_CLOSED, new string[0]));
				}
				return this.m_fillReader;
			}
			set
			{
				if (this.m_bclosed)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.OBJECT_CLOSED, new string[0]));
				}
				this.m_fillReader = value;
				if (this.m_fillReader)
				{
					this.m_dataTableList = new ArrayList();
					DataTable minSchemaTable = this.GetMinSchemaTable();
					if (minSchemaTable != null)
					{
						this.m_dataTableList.Add(minSchemaTable);
					}
				}
			}
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x060004C0 RID: 1216 RVA: 0x00025BE0 File Offset: 0x00023DE0
		internal ArrayList SchemaTables
		{
			get
			{
				if (this.m_bclosed)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.OBJECT_CLOSED, new string[0]));
				}
				return this.m_dataTableList;
			}
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x060004C1 RID: 1217 RVA: 0x00025C08 File Offset: 0x00023E08
		// (set) Token: 0x060004C2 RID: 1218 RVA: 0x00025C20 File Offset: 0x00023E20
		internal OracleRefCursor RefCursor
		{
			get
			{
				if (this.m_bclosed)
				{
					throw new InvalidOperationException();
				}
				return this.m_refCursor;
			}
			set
			{
				if (this.m_bclosed)
				{
					throw new InvalidOperationException();
				}
				this.m_refCursor = value;
			}
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x060004C3 RID: 1219 RVA: 0x00025C38 File Offset: 0x00023E38
		public bool UseEdmMapping
		{
			get
			{
				return this.m_isFromEF;
			}
		}

		// Token: 0x060004C4 RID: 1220 RVA: 0x00025C40 File Offset: 0x00023E40
		public override void Close()
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			if (!this.m_bclosed)
			{
				lock (this.lockDataReader)
				{
					if (!this.m_bclosed)
					{
						try
						{
							bool flag2 = false;
							if (this.m_readerImpl != null)
							{
								if (this.m_internalRowCounter < this.m_readerImpl.m_rowsFetched && this.m_readerImpl.m_sqlMetaData != null && this.m_readerImpl.m_sqlMetaData.HasLOBColumns)
								{
									if (-1 == this.m_internalRowCounter)
									{
										this.m_internalRowCounter++;
									}
									for (int i = this.m_internalRowCounter; i < this.m_readerImpl.m_rowsFetched; i++)
									{
										this.m_readerImpl.CollectTempLOBsToBeFreed(i);
									}
								}
								this.m_readerImpl.Close();
								this.m_readerImpl = null;
							}
							else
							{
								flag2 = true;
							}
							this.m_expectedColumnTypes = null;
							if (this.m_dataTable != null)
							{
								this.m_dataTable.Dispose();
								this.m_dataTable = null;
							}
							if (this.m_dataTableEx != null)
							{
								this.m_dataTableEx.Dispose();
								this.m_dataTableEx = null;
							}
							if (this.m_dataTablesReferenceForFill != null)
							{
								this.m_currentDataTableIndex = -1;
								this.m_currentDataTableForFill = null;
								this.m_bUseDataSetAsDupStore = false;
								this.m_dataTablesReferenceForFill = null;
								this.m_initialRowCnt = -1;
								this.m_isRowAddedToDatatable = false;
							}
							if (!flag2 && (this.m_commandBehavior & CommandBehavior.CloseConnection) == CommandBehavior.CloseConnection)
							{
								try
								{
									this.m_connection.Close();
								}
								catch
								{
								}
							}
						}
						catch (Exception ex)
						{
							if (ProviderConfig.m_bTraceLevelPublic)
							{
								Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Error, new string[]
								{
									ex.ToString()
								});
							}
						}
						finally
						{
							this.m_bclosed = true;
							if (!this.m_bDisposed)
							{
								this.Dispose();
							}
							if (ProviderConfig.m_bTraceLevelPublic)
							{
								Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
							}
						}
					}
				}
			}
		}

		// Token: 0x060004C5 RID: 1221 RVA: 0x00025E7C File Offset: 0x0002407C
		public new void Dispose()
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				this.Dispose(true);
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
		}

		// Token: 0x060004C6 RID: 1222 RVA: 0x00025EF4 File Offset: 0x000240F4
		protected override void Dispose(bool disposing)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			if (!this.m_bDisposed)
			{
				lock (this.lockDataReader)
				{
					if (!this.m_bDisposed)
					{
						try
						{
							this.m_bDisposed = true;
							if (!this.m_bclosed)
							{
								this.Close();
							}
						}
						finally
						{
							GC.SuppressFinalize(this);
							if (ProviderConfig.m_bTraceLevelPublic)
							{
								Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
							}
						}
					}
				}
			}
		}

		// Token: 0x060004C7 RID: 1223 RVA: 0x00025F98 File Offset: 0x00024198
		public override short GetInt16(int i)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			short result;
			try
			{
				if (!this.m_bInternalCall && this.IsDBNull(i))
				{
					throw new InvalidCastException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.DR_NULL_COL_DATA, new string[0]));
				}
				if (OraType.ORA_NUMBER != this.m_readerImpl.m_accessors[i].m_internalType)
				{
					throw new InvalidCastException();
				}
				byte[] array = null;
				int offset = 0;
				int length = 0;
				bool flag = false;
				bool flag2 = false;
				OraColumnData oraColumnData = null;
				if (!this.m_isRowAddedToDatatable)
				{
					flag2 = this.m_readerImpl.m_dataUnmarshaller.TryGetValueIfDuplicate(this.m_internalRowCounter, i, out oraColumnData);
				}
				if (flag2)
				{
					if (oraColumnData.m_netTypeData is short)
					{
						return (short)oraColumnData.m_netTypeData;
					}
					array = oraColumnData.m_rawData;
					length = array.Length;
				}
				else
				{
					if (!this.m_bUseDataSetAsDupStore && this.m_internalRowCounter < this.m_readerImpl.m_rowsFetched - 1)
					{
						flag = this.m_readerImpl.m_dataUnmarshaller.NextRowHasDuplicateData(this.m_internalRowCounter, i);
					}
					if (flag)
					{
						array = this.m_readerImpl.m_accessors[i].GetByteRepresentation(this.m_readerImpl.m_dataUnmarshaller, this.m_internalRowCounter, i);
						length = array.Length;
					}
					else
					{
						this.m_readerImpl.m_accessors[i].GetInternalDataRef(this.m_readerImpl.m_dataUnmarshaller, this.m_internalRowCounter, i, ref array, ref offset, ref length);
					}
				}
				int @int;
				try
				{
					@int = HelperClass.GetInt(array, offset, length);
				}
				catch (OverflowException)
				{
					throw new InvalidCastException();
				}
				if (@int > 32767 || @int < -32768)
				{
					throw new InvalidCastException();
				}
				if (flag && !flag2)
				{
					this.m_readerImpl.m_dataUnmarshaller.SaveColumnData(this.m_internalRowCounter, i, array, (short)@int, true);
				}
				result = (short)@int;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x060004C8 RID: 1224 RVA: 0x000261C8 File Offset: 0x000243C8
		public override int GetInt32(int i)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			int result;
			try
			{
				if (!this.m_bInternalCall && this.IsDBNull(i))
				{
					throw new InvalidCastException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.DR_NULL_COL_DATA, new string[0]));
				}
				if (OraType.ORA_NUMBER != this.m_readerImpl.m_accessors[i].m_internalType)
				{
					throw new InvalidCastException();
				}
				byte[] array = null;
				int offset = 0;
				int length = 0;
				bool flag = false;
				bool flag2 = false;
				OraColumnData oraColumnData = null;
				if (!this.m_isRowAddedToDatatable)
				{
					flag2 = this.m_readerImpl.m_dataUnmarshaller.TryGetValueIfDuplicate(this.m_internalRowCounter, i, out oraColumnData);
				}
				if (flag2)
				{
					if (oraColumnData.m_netTypeData is int)
					{
						return (int)oraColumnData.m_netTypeData;
					}
					array = oraColumnData.m_rawData;
					length = oraColumnData.m_rawData.Length;
				}
				else
				{
					if (!this.m_bUseDataSetAsDupStore && this.m_internalRowCounter < this.m_readerImpl.m_rowsFetched - 1)
					{
						flag = this.m_readerImpl.m_dataUnmarshaller.NextRowHasDuplicateData(this.m_internalRowCounter, i);
					}
					if (flag)
					{
						array = this.m_readerImpl.m_accessors[i].GetByteRepresentation(this.m_readerImpl.m_dataUnmarshaller, this.m_internalRowCounter, i);
						length = array.Length;
					}
					else
					{
						this.m_readerImpl.m_accessors[i].GetInternalDataRef(this.m_readerImpl.m_dataUnmarshaller, this.m_internalRowCounter, i, ref array, ref offset, ref length);
					}
				}
				int @int;
				try
				{
					@int = HelperClass.GetInt(array, offset, length);
				}
				catch (OverflowException)
				{
					throw new InvalidCastException();
				}
				if (flag && !flag2)
				{
					this.m_readerImpl.m_dataUnmarshaller.SaveColumnData(this.m_internalRowCounter, i, array, @int, true);
				}
				result = @int;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x060004C9 RID: 1225 RVA: 0x000263E4 File Offset: 0x000245E4
		public override long GetInt64(int i)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			long result;
			try
			{
				if (!this.m_bInternalCall && this.IsDBNull(i))
				{
					throw new InvalidCastException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.DR_NULL_COL_DATA, new string[0]));
				}
				OraType internalType = this.m_readerImpl.m_accessors[i].m_internalType;
				if (OraType.ORA_NUMBER != internalType && OraType.ORA_INTERVAL_YM != internalType && OraType.ORA_INTERVAL_YM_DTY != internalType)
				{
					throw new InvalidCastException();
				}
				byte[] array = null;
				int num = 0;
				int num2 = 0;
				bool flag = false;
				bool flag2 = false;
				OraColumnData oraColumnData = null;
				if (!this.m_isRowAddedToDatatable)
				{
					flag2 = this.m_readerImpl.m_dataUnmarshaller.TryGetValueIfDuplicate(this.m_internalRowCounter, i, out oraColumnData);
				}
				if (flag2)
				{
					if (oraColumnData.m_netTypeData is long)
					{
						return (long)oraColumnData.m_netTypeData;
					}
					array = oraColumnData.m_rawData;
					num2 = oraColumnData.m_rawData.Length;
				}
				else
				{
					if (!this.m_bUseDataSetAsDupStore && this.m_internalRowCounter < this.m_readerImpl.m_rowsFetched - 1)
					{
						flag = this.m_readerImpl.m_dataUnmarshaller.NextRowHasDuplicateData(this.m_internalRowCounter, i);
					}
					if (flag)
					{
						array = this.m_readerImpl.m_accessors[i].GetByteRepresentation(this.m_readerImpl.m_dataUnmarshaller, this.m_internalRowCounter, i);
						num2 = array.Length;
					}
					else
					{
						this.m_readerImpl.m_accessors[i].GetInternalDataRef(this.m_readerImpl.m_dataUnmarshaller, this.m_internalRowCounter, i, ref array, ref num, ref num2);
					}
				}
				long @long;
				if (OraType.ORA_INTERVAL_YM == this.m_readerImpl.m_accessors[i].m_internalType || OraType.ORA_INTERVAL_YM_DTY == this.m_readerImpl.m_accessors[i].m_internalType)
				{
					@long = OracleIntervalYM.GetLong(array, OracleDbType.IntervalYM, num, num2);
				}
				else
				{
					try
					{
						@long = HelperClass.GetLong(array, num, num2);
					}
					catch (OverflowException)
					{
						throw new InvalidCastException();
					}
				}
				if (flag && !flag2)
				{
					this.m_readerImpl.m_dataUnmarshaller.SaveColumnData(this.m_internalRowCounter, i, array, @long, true);
				}
				result = @long;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x060004CA RID: 1226 RVA: 0x00026658 File Offset: 0x00024858
		public override decimal GetDecimal(int i)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			decimal result;
			try
			{
				if (!this.m_bInternalCall && this.IsDBNull(i))
				{
					throw new InvalidCastException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.DR_NULL_COL_DATA, new string[0]));
				}
				byte[] array = null;
				decimal num = 0m;
				OraColumnData oraColumnData = null;
				bool flag = false;
				int dataPos = 0;
				int length = 0;
				bool flag2 = false;
				if (!this.m_isRowAddedToDatatable)
				{
					flag2 = this.m_readerImpl.m_dataUnmarshaller.TryGetValueIfDuplicate(this.m_internalRowCounter, i, out oraColumnData);
				}
				OraType internalType = this.m_readerImpl.m_accessors[i].m_internalType;
				if (internalType != OraType.ORA_NUMBER)
				{
					if (!this.m_isFromEF || (internalType != OraType.ORA_INTERVAL_DS && internalType != OraType.ORA_INTERVAL_DS_DTY && internalType != OraType.ORA_INTERVAL_YM && internalType != OraType.ORA_INTERVAL_YM_DTY))
					{
						throw new InvalidCastException();
					}
					if (flag2 && oraColumnData.m_netTypeData != null && oraColumnData.m_netTypeData is decimal)
					{
						num = (decimal)oraColumnData.m_netTypeData;
					}
					else
					{
						num = (decimal)this.GetValue(i);
					}
					if (!flag2 && this.m_internalRowCounter < this.m_readerImpl.m_rowsFetched - 1)
					{
						this.m_readerImpl.m_dataUnmarshaller.SaveColumnData(this.m_internalRowCounter, i, array, num, true);
					}
					result = num;
				}
				else
				{
					if (flag2)
					{
						if (oraColumnData.m_netTypeData is decimal)
						{
							return (decimal)oraColumnData.m_netTypeData;
						}
						array = oraColumnData.m_rawData;
						length = oraColumnData.m_rawData.Length;
					}
					else
					{
						if (!this.m_bUseDataSetAsDupStore && this.m_internalRowCounter < this.m_readerImpl.m_rowsFetched - 1)
						{
							flag = this.m_readerImpl.m_dataUnmarshaller.NextRowHasDuplicateData(this.m_internalRowCounter, i);
						}
						if (flag)
						{
							array = this.m_readerImpl.m_accessors[i].GetByteRepresentation(this.m_readerImpl.m_dataUnmarshaller, this.m_internalRowCounter, i);
							length = array.Length;
						}
						else
						{
							this.m_readerImpl.m_accessors[i].GetInternalDataRef(this.m_readerImpl.m_dataUnmarshaller, this.m_internalRowCounter, i, ref array, ref dataPos, ref length);
						}
					}
					try
					{
						num = DecimalConv.GetDecimal(array, dataPos, length);
					}
					catch (OverflowException)
					{
						throw new InvalidCastException();
					}
					if (!flag2 && flag)
					{
						this.m_readerImpl.m_dataUnmarshaller.SaveColumnData(this.m_internalRowCounter, i, array, num, true);
					}
					result = num;
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x060004CB RID: 1227 RVA: 0x00026924 File Offset: 0x00024B24
		public override double GetDouble(int i)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			double result;
			try
			{
				if (!this.m_bInternalCall && this.IsDBNull(i))
				{
					throw new InvalidCastException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.DR_NULL_COL_DATA, new string[0]));
				}
				OraType internalType = this.m_readerImpl.m_accessors[i].m_internalType;
				if (internalType != OraType.ORA_NUMBER && internalType != OraType.ORA_IBDOUBLE)
				{
					throw new InvalidCastException();
				}
				byte[] array = null;
				int offset = 0;
				int len = 0;
				bool flag = false;
				bool flag2 = false;
				OraColumnData oraColumnData = null;
				if (!this.m_isRowAddedToDatatable)
				{
					flag2 = this.m_readerImpl.m_dataUnmarshaller.TryGetValueIfDuplicate(this.m_internalRowCounter, i, out oraColumnData);
				}
				if (flag2)
				{
					if (oraColumnData.m_netTypeData != null && oraColumnData.m_netTypeData is double)
					{
						return (double)oraColumnData.m_netTypeData;
					}
					array = oraColumnData.m_rawData;
					len = oraColumnData.m_rawData.Length;
				}
				else
				{
					if (!this.m_bUseDataSetAsDupStore && this.m_internalRowCounter < this.m_readerImpl.m_rowsFetched - 1)
					{
						flag = this.m_readerImpl.m_dataUnmarshaller.NextRowHasDuplicateData(this.m_internalRowCounter, i);
					}
					if (flag)
					{
						array = this.m_readerImpl.m_accessors[i].GetByteRepresentation(this.m_readerImpl.m_dataUnmarshaller, this.m_internalRowCounter, i);
						len = array.Length;
					}
					else
					{
						this.m_readerImpl.m_accessors[i].GetInternalDataRef(this.m_readerImpl.m_dataUnmarshaller, this.m_internalRowCounter, i, ref array, ref offset, ref len);
					}
				}
				double @double = HelperClass.GetDouble(internalType, array, offset, len);
				if (flag && !flag2)
				{
					this.m_readerImpl.m_dataUnmarshaller.SaveColumnData(this.m_internalRowCounter, i, array, @double, true);
				}
				result = @double;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x060004CC RID: 1228 RVA: 0x00026B34 File Offset: 0x00024D34
		public override float GetFloat(int i)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			float result;
			try
			{
				if (!this.m_bInternalCall && this.IsDBNull(i))
				{
					throw new InvalidCastException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.DR_NULL_COL_DATA, new string[0]));
				}
				OraType internalType = this.m_readerImpl.m_accessors[i].m_internalType;
				if (internalType != OraType.ORA_NUMBER && internalType != OraType.ORA_IBFLOAT)
				{
					throw new InvalidCastException();
				}
				byte[] array = null;
				int offset = 0;
				int len = 0;
				bool flag = false;
				bool flag2 = false;
				OraColumnData oraColumnData = null;
				if (!this.m_isRowAddedToDatatable)
				{
					flag2 = this.m_readerImpl.m_dataUnmarshaller.TryGetValueIfDuplicate(this.m_internalRowCounter, i, out oraColumnData);
				}
				if (flag2)
				{
					if (oraColumnData.m_netTypeData != null && oraColumnData.m_netTypeData is float)
					{
						return (float)oraColumnData.m_netTypeData;
					}
					array = oraColumnData.m_rawData;
					len = oraColumnData.m_rawData.Length;
				}
				else
				{
					if (!this.m_bUseDataSetAsDupStore && this.m_internalRowCounter < this.m_readerImpl.m_rowsFetched - 1)
					{
						flag = this.m_readerImpl.m_dataUnmarshaller.NextRowHasDuplicateData(this.m_internalRowCounter, i);
					}
					if (flag)
					{
						array = this.m_readerImpl.m_accessors[i].GetByteRepresentation(this.m_readerImpl.m_dataUnmarshaller, this.m_internalRowCounter, i);
						len = array.Length;
					}
					else
					{
						this.m_readerImpl.m_accessors[i].GetInternalDataRef(this.m_readerImpl.m_dataUnmarshaller, this.m_internalRowCounter, i, ref array, ref offset, ref len);
					}
				}
				float @float = HelperClass.GetFloat(internalType, array, offset, len);
				if (flag && !flag2)
				{
					this.m_readerImpl.m_dataUnmarshaller.SaveColumnData(this.m_internalRowCounter, i, array, @float, true);
				}
				result = @float;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x060004CD RID: 1229 RVA: 0x00026D44 File Offset: 0x00024F44
		public override string GetString(int i)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			string result;
			try
			{
				if (!this.m_bInternalCall && this.IsDBNull(i))
				{
					throw new InvalidCastException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.DR_NULL_COL_DATA, new string[0]));
				}
				OraType internalType = this.m_readerImpl.m_accessors[i].m_internalType;
				if (internalType != OraType.ORA_CHAR && internalType != OraType.ORA_CHARN && internalType != OraType.ORA_LONG && internalType != OraType.ORA_UROWID && internalType != OraType.ORA_ROWID && internalType != OraType.ORA_OCICLobLocator && internalType != OraType.ORA_XMLTYPE)
				{
					throw new InvalidCastException();
				}
				string text = string.Empty;
				if (OraType.ORA_LONG == internalType)
				{
					if (!this.m_readerImpl.IsCompleteDataForLongAvailable(this.m_internalRowCounter, i))
					{
						text = this.GetLongData(this.m_connection, this.m_internalRowCounter, i, -1);
					}
					else
					{
						text = ((TTCLongAccessor)this.m_readerImpl.m_accessors[i]).GetString(this.m_readerImpl.m_dataUnmarshaller, this.m_internalRowCounter, i);
					}
				}
				else if (internalType == OraType.ORA_OCICLobLocator)
				{
					byte[] lobLocator = null;
					object obj = this.m_LobImplCache[i];
					if (!this.m_fillReader && this.m_LobImplCache != null && obj != null && this.m_LastCachedRowNumber[i] == (long)this.m_RowNumber)
					{
						lobLocator = ((OracleClobImpl)obj).m_lobLocator;
					}
					if (this.m_connection.m_isDb11gR1OrHigher && ((!this.m_fillReader && -1L == this.m_readerImpl.m_clientInitialLOBFS) || (this.m_fillReader && !this.m_returnPSTypes)))
					{
						text = OracleClobImpl.GetCompleteClobData(this.m_internalRowCounter, this.m_readerImpl.m_dataUnmarshaller, this.m_connection.m_oracleConnectionImpl, lobLocator, (TTCLobAccessor)this.m_readerImpl.m_accessors[i]);
					}
					else
					{
						text = OracleClobImpl.GetCompleteClobData(this.m_internalRowCounter, i, this.m_connection.m_oracleConnectionImpl, lobLocator, this.m_readerImpl.m_dataUnmarshaller, (TTCLobAccessor)this.m_readerImpl.m_accessors[i], ref this.m_tempOraClobImpl);
					}
				}
				else
				{
					if (OraType.ORA_XMLTYPE == internalType)
					{
						OracleXmlTypeImpl oracleXmlTypeImpl = new OracleXmlTypeImpl(this.m_connection.m_oracleConnectionImpl, (TTCXmlTypeAccessor)this.m_readerImpl.m_accessors[i], this.m_readerImpl.m_dataUnmarshaller, this.m_internalRowCounter, i);
						oracleXmlTypeImpl.Initialize(this.m_connection);
						return oracleXmlTypeImpl.GetString();
					}
					OraColumnData oraColumnData = null;
					bool flag = false;
					if (!this.m_isRowAddedToDatatable)
					{
						flag = this.m_readerImpl.m_dataUnmarshaller.TryGetValueIfDuplicate(this.m_internalRowCounter, i, out oraColumnData);
					}
					if (flag)
					{
						if (oraColumnData.m_netTypeData is string)
						{
							text = (string)oraColumnData.m_netTypeData;
						}
						else if (oraColumnData.m_rawData != null)
						{
							Accessor accessor = this.m_readerImpl.m_accessors[i];
							text = accessor.GetString(oraColumnData.m_rawData, 0, oraColumnData.m_rawData.Length, (byte)accessor.m_colMetaData.m_characterSetForm, this.m_readerImpl.m_dataUnmarshaller.m_charArrayForConversion);
						}
					}
					if (!flag)
					{
						Accessor accessor2 = this.m_readerImpl.m_accessors[i];
						text = accessor2.GetString(this.m_readerImpl.m_dataUnmarshaller, this.m_internalRowCounter, i, (byte)accessor2.m_colMetaData.m_characterSetForm);
					}
					if (!this.m_bUseDataSetAsDupStore && !flag && this.m_internalRowCounter < this.m_readerImpl.m_rowsFetched - 1)
					{
						this.m_readerImpl.m_dataUnmarshaller.SaveColumnData(this.m_internalRowCounter, i, null, text, true);
					}
				}
				result = text;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x060004CE RID: 1230 RVA: 0x000270F0 File Offset: 0x000252F0
		public TimeSpan GetTimeSpan(int i)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			TimeSpan result;
			try
			{
				if (OraType.ORA_INTERVAL_DS != this.m_readerImpl.m_accessors[i].m_internalType && OraType.ORA_INTERVAL_DS_DTY != this.m_readerImpl.m_accessors[i].m_internalType)
				{
					throw new InvalidCastException();
				}
				if (!this.m_bInternalCall && this.IsDBNull(i))
				{
					throw new InvalidCastException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.DR_NULL_COL_DATA, new string[0]));
				}
				TimeSpan timeSpan = TimeSpan.MinValue;
				byte[] array = null;
				int dataOffset = 0;
				int dataLength = 0;
				bool flag = false;
				bool flag2 = false;
				OraColumnData oraColumnData = null;
				if (!this.m_isRowAddedToDatatable)
				{
					flag2 = this.m_readerImpl.m_dataUnmarshaller.TryGetValueIfDuplicate(this.m_internalRowCounter, i, out oraColumnData);
				}
				if (flag2)
				{
					if (oraColumnData.m_netTypeData != null && oraColumnData.m_netTypeData is TimeSpan)
					{
						return (TimeSpan)oraColumnData.m_netTypeData;
					}
					array = oraColumnData.m_rawData;
					dataLength = array.Length;
				}
				else
				{
					if (!this.m_bUseDataSetAsDupStore && this.m_internalRowCounter < this.m_readerImpl.m_rowsFetched - 1)
					{
						flag = this.m_readerImpl.m_dataUnmarshaller.NextRowHasDuplicateData(this.m_internalRowCounter, i);
					}
					if (flag)
					{
						array = this.m_readerImpl.m_accessors[i].GetByteRepresentation(this.m_readerImpl.m_dataUnmarshaller, this.m_internalRowCounter, i);
						dataLength = array.Length;
					}
					else
					{
						this.m_readerImpl.m_accessors[i].GetInternalDataRef(this.m_readerImpl.m_dataUnmarshaller, this.m_internalRowCounter, i, ref array, ref dataOffset, ref dataLength);
					}
				}
				timeSpan = OracleIntervalDS.GetTimeSpan(array, OracleDbType.IntervalDS, dataOffset, dataLength);
				if (flag && !flag2)
				{
					this.m_readerImpl.m_dataUnmarshaller.SaveColumnData(this.m_internalRowCounter, i, array, timeSpan, true);
				}
				result = timeSpan;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x060004CF RID: 1231 RVA: 0x00027314 File Offset: 0x00025514
		public override bool GetBoolean(int i)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			bool result;
			try
			{
				if (!this.m_isFromEF)
				{
					throw new NotSupportedException();
				}
				object value = this.GetValue(i);
				Type type = value.GetType();
				if (type == typeof(bool))
				{
					result = (bool)value;
				}
				else if (type == typeof(DBNull))
				{
					result = false;
				}
				else
				{
					result = ((decimal)value > 0m);
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x060004D0 RID: 1232 RVA: 0x000273E8 File Offset: 0x000255E8
		public override byte GetByte(int i)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			byte result;
			try
			{
				if (!this.m_bInternalCall && this.IsDBNull(i))
				{
					throw new InvalidCastException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.DR_NULL_COL_DATA, new string[0]));
				}
				if (OraType.ORA_NUMBER != this.m_readerImpl.m_accessors[i].m_internalType)
				{
					throw new InvalidCastException();
				}
				byte[] array = null;
				int offset = 0;
				int length = 0;
				bool flag = false;
				bool flag2 = false;
				OraColumnData oraColumnData = null;
				if (!this.m_isRowAddedToDatatable)
				{
					flag2 = this.m_readerImpl.m_dataUnmarshaller.TryGetValueIfDuplicate(this.m_internalRowCounter, i, out oraColumnData);
				}
				if (flag2)
				{
					if (oraColumnData.m_netTypeData is byte)
					{
						return (byte)oraColumnData.m_netTypeData;
					}
					array = oraColumnData.m_rawData;
					length = oraColumnData.m_rawData.Length;
				}
				else
				{
					if (!this.m_bUseDataSetAsDupStore && this.m_internalRowCounter < this.m_readerImpl.m_rowsFetched - 1)
					{
						flag = this.m_readerImpl.m_dataUnmarshaller.NextRowHasDuplicateData(this.m_internalRowCounter, i);
					}
					if (flag)
					{
						array = this.m_readerImpl.m_accessors[i].GetByteRepresentation(this.m_readerImpl.m_dataUnmarshaller, this.m_internalRowCounter, i);
						length = array.Length;
					}
					else
					{
						this.m_readerImpl.m_accessors[i].GetInternalDataRef(this.m_readerImpl.m_dataUnmarshaller, this.m_internalRowCounter, i, ref array, ref offset, ref length);
					}
				}
				int @int;
				try
				{
					@int = HelperClass.GetInt(array, offset, length);
				}
				catch (OverflowException)
				{
					throw new InvalidCastException();
				}
				if (@int > 255 || @int < 0)
				{
					throw new InvalidCastException();
				}
				byte b = (byte)@int;
				if (flag && !flag2)
				{
					this.m_readerImpl.m_dataUnmarshaller.SaveColumnData(this.m_internalRowCounter, i, array, b, false);
				}
				result = b;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x060004D1 RID: 1233 RVA: 0x00027620 File Offset: 0x00025820
		public override long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferOffset, int length)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			long bytesInternal;
			try
			{
				bytesInternal = this.GetBytesInternal(i, fieldOffset, buffer, bufferOffset, length, true);
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
			return bytesInternal;
		}

		// Token: 0x060004D2 RID: 1234 RVA: 0x0002769C File Offset: 0x0002589C
		internal long GetBytesInternal(int i, long fieldOffset, byte[] buffer, int bufferOffset, int length, bool bThrowException)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			long result;
			try
			{
				if (this.m_bclosed || this.m_bBeginingOfFile || this.m_bEndOfFile)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.OBJECT_CLOSED, new string[0]));
				}
				if (i >= this.m_fieldCount || i < 0)
				{
					throw new IndexOutOfRangeException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.DR_INV_COL_INDEX, new string[0]));
				}
				if (this.m_internalRowCounter < 0)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.DR_NO_READ_CALLED, new string[0]));
				}
				if (buffer != null)
				{
					if (bufferOffset < 0 || bufferOffset > buffer.Length)
					{
						throw new ArgumentOutOfRangeException("bufferOffset");
					}
					if (bufferOffset + length > buffer.Length)
					{
						throw new ArgumentOutOfRangeException(null, OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.DR_INV_DATA_REQ, new string[0]));
					}
					if (length < 0)
					{
						throw new ArgumentOutOfRangeException();
					}
					if (length == 0 || fieldOffset < 0L)
					{
						return 0L;
					}
				}
				OraType internalType = this.m_readerImpl.m_accessors[i].m_internalType;
				if (internalType != OraType.ORA_LONGRAW && internalType != OraType.ORA_RAW && internalType != OraType.ORA_OCIBLobLocator && internalType != OraType.ORA_OCIBFileLocator)
				{
					throw new InvalidCastException();
				}
				if (this.m_readerImpl.m_accessors[i].IsNullIndicatorSet(this.m_readerImpl.m_dataUnmarshaller, (int)this.m_readerImpl.m_sqlMetaData.m_noOfColumns, this.m_internalRowCounter, i))
				{
					if (bThrowException)
					{
						throw new InvalidCastException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.DR_NULL_COL_DATA, new string[0]));
					}
					result = -1L;
				}
				else
				{
					long num = 0L;
					if (OraType.ORA_LONGRAW == internalType)
					{
						int internalRowCounter = this.m_internalRowCounter;
						TTCLongAccessor ttclongAccessor = this.m_readerImpl.m_accessors[i] as TTCLongAccessor;
						if (ttclongAccessor.IsCompleteDataAvailable(internalRowCounter))
						{
							if (buffer == null)
							{
								num = (long)ttclongAccessor.m_totalLengthOfData[internalRowCounter];
							}
							else
							{
								num = ttclongAccessor.GetBytes(this.m_readerImpl.m_dataUnmarshaller, internalRowCounter, i, fieldOffset, buffer, bufferOffset, length);
							}
						}
						else
						{
							num = this.GetLongRawData(this.m_connection, internalRowCounter, i, fieldOffset, ref buffer, bufferOffset, length, false);
						}
					}
					else if (OraType.ORA_OCIBLobLocator == internalType)
					{
						bool bLOBArrayReadDone = false;
						byte[] lobLocator = null;
						object obj = this.m_LobImplCache[i];
						if (!this.m_fillReader && this.m_LobImplCache != null && obj != null && this.m_LastCachedRowNumber[i] == (long)this.m_RowNumber)
						{
							lobLocator = ((OracleBlobImpl)obj).m_lobLocator;
						}
						if (this.m_connection.m_isDb11gR1OrHigher && ((!this.m_fillReader && -1L == this.m_readerImpl.m_clientInitialLOBFS) || (this.m_fillReader && !this.m_returnPSTypes)))
						{
							bLOBArrayReadDone = true;
						}
						num = this.m_readerImpl.GetBytes(this.m_internalRowCounter, i, fieldOffset, buffer, bufferOffset, length, lobLocator, bLOBArrayReadDone, ref this.m_tempOraBlobImpl);
					}
					else
					{
						OraColumnData oraColumnData = null;
						bool flag = false;
						if (!this.m_isRowAddedToDatatable)
						{
							flag = this.m_readerImpl.m_dataUnmarshaller.TryGetValueIfDuplicate(this.m_internalRowCounter, i, out oraColumnData);
						}
						if (flag)
						{
							if (oraColumnData.m_rawData != null)
							{
								byte[] rawData = oraColumnData.m_rawData;
								if (buffer == null)
								{
									num = (long)rawData.Length;
								}
								else
								{
									if (rawData.Length >= length)
									{
										num = (long)length;
									}
									else
									{
										num = (long)rawData.Length;
									}
									int num2 = 0;
									while ((long)num2 < num)
									{
										buffer[bufferOffset + num2] = rawData[num2];
										num2++;
									}
								}
							}
						}
						else
						{
							num = this.m_readerImpl.GetBytes(this.m_connection, this.m_internalRowCounter, i, fieldOffset, buffer, bufferOffset, length);
						}
					}
					result = num;
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
			return result;
		}

		// Token: 0x060004D3 RID: 1235 RVA: 0x00027A40 File Offset: 0x00025C40
		public override char GetChar(int i)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060004D4 RID: 1236 RVA: 0x00027A48 File Offset: 0x00025C48
		public override long GetChars(int i, long fieldOffset, char[] buffer, int bufferOffset, int length)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			long result;
			try
			{
				if (this.m_bclosed || this.m_bBeginingOfFile || this.m_bEndOfFile)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.OBJECT_CLOSED, new string[0]));
				}
				if (i >= this.m_fieldCount || i < 0)
				{
					throw new IndexOutOfRangeException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.DR_INV_COL_INDEX, new string[0]));
				}
				if (this.m_internalRowCounter < 0)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.DR_NO_READ_CALLED, new string[0]));
				}
				if (buffer != null)
				{
					if (bufferOffset < 0 || bufferOffset > buffer.Length)
					{
						throw new ArgumentOutOfRangeException("bufferOffset");
					}
					if (bufferOffset + length > buffer.Length)
					{
						throw new ArgumentOutOfRangeException(null, OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.DR_INV_DATA_REQ, new string[0]));
					}
					if (length < 0)
					{
						throw new ArgumentOutOfRangeException();
					}
					if (length == 0 || fieldOffset < 0L)
					{
						return 0L;
					}
				}
				OraType internalType = this.m_readerImpl.m_accessors[i].m_internalType;
				if (internalType != OraType.ORA_CHAR && internalType != OraType.ORA_CHARN && internalType != OraType.ORA_LONG && internalType != OraType.ORA_UROWID && internalType != OraType.ORA_ROWID && internalType != OraType.ORA_OCICLobLocator)
				{
					throw new InvalidCastException();
				}
				if (this.m_readerImpl.m_accessors[i].IsNullIndicatorSet(this.m_readerImpl.m_dataUnmarshaller, (int)this.m_readerImpl.m_sqlMetaData.m_noOfColumns, this.m_internalRowCounter, i))
				{
					throw new InvalidCastException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.DR_NULL_COL_DATA, new string[0]));
				}
				long num = 0L;
				if (OraType.ORA_LONG == internalType)
				{
					int internalRowCounter = this.m_internalRowCounter;
					int num2 = i;
					TTCLongAccessor ttclongAccessor = this.m_readerImpl.m_accessors[num2] as TTCLongAccessor;
					if (ttclongAccessor.IsCompleteDataAvailable(internalRowCounter))
					{
						num = ttclongAccessor.GetChars(this.m_readerImpl.m_dataUnmarshaller, internalRowCounter, num2, fieldOffset, buffer, bufferOffset, length);
					}
					else if (buffer != null && (long)length + fieldOffset <= (long)ttclongAccessor.AvailableDataSize(this.m_internalRowCounter))
					{
						num = ttclongAccessor.GetChars(this.m_readerImpl.m_dataUnmarshaller, this.m_internalRowCounter, num2, fieldOffset, buffer, bufferOffset, length);
					}
					else
					{
						DataUnmarshaller dataUnmarshaller = null;
						ttclongAccessor = this.GetLongAccessorToFetchMoreData(this.m_connection, ref internalRowCounter, ref num2, length, out dataUnmarshaller);
						num = ttclongAccessor.GetChars(dataUnmarshaller, internalRowCounter, num2, fieldOffset, buffer, bufferOffset, length);
					}
				}
				else if (OraType.ORA_OCICLobLocator == internalType)
				{
					bool bLOBArrayReadDone = false;
					byte[] lobLocator = null;
					object obj = this.m_LobImplCache[i];
					if (!this.m_fillReader && this.m_LobImplCache != null && obj != null && this.m_LastCachedRowNumber[i] == (long)this.m_RowNumber)
					{
						lobLocator = ((OracleClobImpl)obj).m_lobLocator;
					}
					if (this.m_connection.m_isDb11gR1OrHigher && ((!this.m_fillReader && -1L == this.m_readerImpl.m_clientInitialLOBFS) || (this.m_fillReader && !this.m_returnPSTypes)))
					{
						bLOBArrayReadDone = true;
					}
					num = this.m_readerImpl.GetChars(this.m_internalRowCounter, i, fieldOffset, buffer, bufferOffset, length, lobLocator, bLOBArrayReadDone, ref this.m_tempOraClobImpl);
				}
				else
				{
					OraColumnData oraColumnData = null;
					bool flag = false;
					if (!this.m_isRowAddedToDatatable)
					{
						flag = this.m_readerImpl.m_dataUnmarshaller.TryGetValueIfDuplicate(this.m_internalRowCounter, i, out oraColumnData);
					}
					if (flag)
					{
						if (oraColumnData.m_netTypeData is string)
						{
							string text = (string)oraColumnData.m_netTypeData;
							if (buffer == null)
							{
								num = (long)text.Length;
							}
							else
							{
								if (text.Length >= length)
								{
									num = (long)length;
								}
								else
								{
									num = (long)text.Length;
								}
								int num3 = 0;
								while ((long)num3 < num)
								{
									buffer[bufferOffset + num3] = text[num3];
									num3++;
								}
							}
						}
						else if (oraColumnData.m_netTypeData is char[])
						{
							char[] array = (char[])oraColumnData.m_netTypeData;
							if (buffer == null)
							{
								num = (long)array.Length;
							}
							else
							{
								if (array.Length >= length)
								{
									num = (long)length;
								}
								else
								{
									num = (long)array.Length;
								}
								int num4 = 0;
								while ((long)num4 < num)
								{
									buffer[bufferOffset + num4] = array[num4];
									num4++;
								}
							}
						}
						else if (oraColumnData.m_rawData != null)
						{
							this.m_readerImpl.m_accessors[i].GetCharsFromBuffer(oraColumnData.m_rawData, oraColumnData.m_rawData.Length, fieldOffset, buffer, bufferOffset, length, (byte)this.m_readerImpl.m_accessors[i].m_colMetaData.m_characterSetForm);
						}
					}
					else
					{
						num = this.m_readerImpl.GetChars(this.m_connection, this.m_internalRowCounter, i, fieldOffset, buffer, bufferOffset, length);
						if (!this.m_bUseDataSetAsDupStore && this.m_internalRowCounter < this.m_readerImpl.m_rowsFetched - 1 && this.m_readerImpl.m_dataUnmarshaller.NextRowHasDuplicateData(this.m_internalRowCounter, i))
						{
							Accessor accessor = this.m_readerImpl.m_accessors[i];
							string @string = accessor.GetString(this.m_readerImpl.m_dataUnmarshaller, this.m_internalRowCounter, i, (byte)accessor.m_colMetaData.m_characterSetForm);
							this.m_readerImpl.m_dataUnmarshaller.SaveColumnData(this.m_internalRowCounter, i, null, @string, true);
						}
					}
				}
				result = num;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x060004D5 RID: 1237 RVA: 0x00027F80 File Offset: 0x00026180
		public override DateTime GetDateTime(int i)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			DateTime result;
			try
			{
				if (!this.m_bInternalCall && this.IsDBNull(i))
				{
					throw new InvalidCastException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.DR_NULL_COL_DATA, new string[0]));
				}
				OraType internalType = this.m_readerImpl.m_accessors[i].m_internalType;
				if (internalType != OraType.ORA_TIMESTAMP && internalType != OraType.ORA_TIMESTAMP_LTZ && internalType != OraType.ORA_TIMESTAMP_TZ && internalType != OraType.ORA_DATE && internalType != OraType.ORA_TIMESTAMP_DTY && internalType != OraType.ORA_TIMESTAMP_TZ_DTY && internalType != OraType.ORA_TIMESTAMP_LTZ_DTY)
				{
					throw new InvalidCastException();
				}
				byte[] array = null;
				DateTime dateTime = DateTime.MinValue;
				OraColumnData oraColumnData = null;
				bool flag = false;
				int num = 0;
				int length = 0;
				bool flag2 = false;
				DateTime? dateTime2 = null;
				if (!this.m_isRowAddedToDatatable)
				{
					flag2 = this.m_readerImpl.m_dataUnmarshaller.TryGetValueIfDuplicate(this.m_internalRowCounter, i, out oraColumnData);
				}
				if (flag2)
				{
					if (oraColumnData.m_netTypeData != null && oraColumnData.m_netTypeData is DateTime)
					{
						return (DateTime)oraColumnData.m_netTypeData;
					}
					array = oraColumnData.m_rawData;
					length = array.Length;
				}
				else
				{
					if (!this.m_bUseDataSetAsDupStore && this.m_internalRowCounter < this.m_readerImpl.m_rowsFetched - 1)
					{
						flag = this.m_readerImpl.m_dataUnmarshaller.NextRowHasDuplicateData(this.m_internalRowCounter, i);
					}
					if (flag)
					{
						array = this.m_readerImpl.m_accessors[i].GetByteRepresentation(this.m_readerImpl.m_dataUnmarshaller, this.m_internalRowCounter, i);
						length = array.Length;
					}
					else
					{
						this.m_readerImpl.m_accessors[i].GetInternalDataRef(this.m_readerImpl.m_dataUnmarshaller, this.m_internalRowCounter, i, ref array, ref num, ref length);
					}
				}
				if (array != null)
				{
					if (internalType == OraType.ORA_TIMESTAMP_TZ || internalType == OraType.ORA_TIMESTAMP_TZ_DTY)
					{
						if (this.m_connection.m_oracleConnectionImpl.IsTZDataSentAsLocalTime)
						{
							byte[] array2 = null;
							byte[] array3 = new byte[13];
							Array.Copy(array, num, array3, 0, length);
							TimeStamp.GetUTCByteRepFromLocalArray(array3, out array2, out dateTime2, true);
							dateTime = dateTime2.Value;
						}
						else
						{
							dateTime = DateTimeConv.ToDateTime(array, internalType != OraType.ORA_TIMESTAMP_TZ && internalType != OraType.ORA_TIMESTAMP_TZ_DTY, num, length);
						}
					}
					else if (internalType == OraType.ORA_TIMESTAMP_LTZ || internalType == OraType.ORA_TIMESTAMP_LTZ_DTY)
					{
						OracleTimeZoneInfo? dbtimeZoneBytes = this.m_connection.m_oracleConnectionImpl.GetDBTimeZoneBytes();
						byte[] array4 = null;
						byte[] array5 = new byte[11];
						Array.Copy(array, num, array5, 0, length);
						TimeStamp.ConvertDBTimeToLTZData(array5, dbtimeZoneBytes, this.m_readerImpl.m_sessionTimeZone, out array4, out dateTime2, true);
						dateTime = dateTime2.Value;
					}
					else
					{
						dateTime = DateTimeConv.ToDateTime(array, internalType != OraType.ORA_TIMESTAMP_TZ && internalType != OraType.ORA_TIMESTAMP_TZ_DTY, num, length);
					}
				}
				if (flag && !flag2)
				{
					this.m_readerImpl.m_dataUnmarshaller.SaveColumnData(this.m_internalRowCounter, i, array, dateTime, true);
				}
				result = dateTime;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x060004D6 RID: 1238 RVA: 0x000282B0 File Offset: 0x000264B0
		public override Guid GetGuid(int i)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			Guid result;
			try
			{
				if (!this.m_isFromEF)
				{
					throw new NotSupportedException();
				}
				object value = this.GetValue(i);
				result = (Guid)value;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x060004D7 RID: 1239 RVA: 0x0002833C File Offset: 0x0002653C
		public new DbDataReader GetData(int i)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060004D8 RID: 1240 RVA: 0x00028344 File Offset: 0x00026544
		public override string GetDataTypeName(int i)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			string result;
			try
			{
				if (this.m_bclosed)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.OBJECT_CLOSED, new string[0]));
				}
				result = this.GetOraDbType(i).ToString();
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x060004D9 RID: 1241 RVA: 0x000283E4 File Offset: 0x000265E4
		public override IEnumerator GetEnumerator()
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			IEnumerator result;
			try
			{
				bool closeReader = false;
				if ((this.m_commandBehavior & CommandBehavior.CloseConnection) == CommandBehavior.CloseConnection)
				{
					closeReader = true;
				}
				result = new DbEnumerator(this, closeReader);
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x060004DA RID: 1242 RVA: 0x0002846C File Offset: 0x0002666C
		public override Type GetFieldType(int i)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			Type type = null;
			try
			{
				if (this.m_bclosed)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.OBJECT_CLOSED, new string[0]));
				}
				if (i >= this.m_fieldCount || i < 0)
				{
					throw new IndexOutOfRangeException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.DR_INV_COL_INDEX, new string[0]));
				}
				if (this.m_readerImpl.m_sqlMetaData.m_fieldTypes != null)
				{
					if (null != (type = this.m_readerImpl.m_sqlMetaData.m_fieldTypes[i]))
					{
						return type;
					}
				}
				else
				{
					this.m_readerImpl.m_sqlMetaData.m_fieldTypes = new Type[this.m_fieldCount];
				}
				ColumnDescribeInfo columnDescribeInfo = this.m_readerImpl.m_sqlMetaData.m_columnDescribeInfo[i];
				OraType dataType = (OraType)columnDescribeInfo.m_dataType;
				type = (Type)OracleTypeMapper.m_OraToNET[dataType];
				if (dataType == OraType.ORA_XMLTYPE)
				{
					if (columnDescribeInfo.bIsXmlType)
					{
						type = typeof(string);
					}
					else
					{
						type = typeof(object);
					}
				}
				else if (type == typeof(decimal))
				{
					int scale = (int)columnDescribeInfo.m_scale;
					int precision = (int)columnDescribeInfo.m_precision;
					if (scale <= 0 && precision - scale < 5)
					{
						type = typeof(short);
					}
					else if (scale <= 0 && precision - scale < 10)
					{
						type = typeof(int);
					}
					else if (scale <= 0 && precision - scale < 19)
					{
						type = typeof(long);
					}
					else if (precision < 8 && ((scale <= 0 && precision - scale <= 38) || (scale > 0 && scale <= 44)))
					{
						type = typeof(float);
					}
					else if (precision < 16)
					{
						type = typeof(double);
					}
				}
				this.m_readerImpl.m_sqlMetaData.m_fieldTypes[i] = type;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
			return type;
		}

		// Token: 0x060004DB RID: 1243 RVA: 0x000286A4 File Offset: 0x000268A4
		public override string GetName(int i)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				if (this.m_bclosed)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.OBJECT_CLOSED, new string[0]));
				}
				if (i >= this.m_fieldCount || i < 0)
				{
					throw new IndexOutOfRangeException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.DR_INV_COL_INDEX, new string[0]));
				}
				if (this.m_readerImpl.m_sqlMetaData != null && this.m_readerImpl.m_sqlMetaData.m_columnDescribeInfo[i] != null)
				{
					return this.m_readerImpl.m_sqlMetaData.m_columnDescribeInfo[i].pColAlias;
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
			return null;
		}

		// Token: 0x060004DC RID: 1244 RVA: 0x00028794 File Offset: 0x00026994
		public OracleBinary GetOracleBinary(int i)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleBinary result;
			try
			{
				OraType internalType = this.m_readerImpl.m_accessors[i].m_internalType;
				if (internalType != OraType.ORA_LONGRAW && internalType != OraType.ORA_RAW && internalType != OraType.ORA_OCIBLobLocator && internalType != OraType.ORA_OCIBFileLocator)
				{
					throw new InvalidCastException();
				}
				if (this.IsDBNull(i))
				{
					result = OracleBinary.Null;
				}
				else
				{
					byte[] data = (byte[])this.GetValue(i);
					result = new OracleBinary(data);
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x060004DD RID: 1245 RVA: 0x00028858 File Offset: 0x00026A58
		public OracleBFile GetOracleBFile(int i)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleBFile result;
			try
			{
				if (this.m_bclosed || this.m_bBeginingOfFile || this.m_bEndOfFile)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.OBJECT_CLOSED, new string[0]));
				}
				if (i >= this.m_fieldCount || i < 0)
				{
					throw new IndexOutOfRangeException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.DR_INV_COL_INDEX, new string[0]));
				}
				if (this.m_internalRowCounter < 0)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.DR_NO_READ_CALLED, new string[0]));
				}
				if (OraType.ORA_OCIBFileLocator != this.m_readerImpl.m_accessors[i].m_internalType)
				{
					throw new InvalidCastException();
				}
				if (!this.m_readerImpl.m_accessors[i].IsNullIndicatorSet(this.m_readerImpl.m_dataUnmarshaller, (int)this.m_readerImpl.m_sqlMetaData.m_noOfColumns, this.m_internalRowCounter, i))
				{
					byte[] lobLocator = this.m_readerImpl.GetLobLocator(this.m_internalRowCounter, i);
					if (lobLocator != null)
					{
						result = new OracleBFile(this.m_connection, lobLocator);
					}
					else
					{
						result = OracleBFile.Null;
					}
				}
				else
				{
					result = OracleBFile.Null;
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x060004DE RID: 1246 RVA: 0x000289D4 File Offset: 0x00026BD4
		public OracleBlob GetOracleBlob(int i)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleBlob result;
			try
			{
				if (this.m_bclosed || this.m_bBeginingOfFile || this.m_bEndOfFile)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.OBJECT_CLOSED, new string[0]));
				}
				if (i >= this.m_fieldCount || i < 0)
				{
					throw new IndexOutOfRangeException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.DR_INV_COL_INDEX, new string[0]));
				}
				if (this.m_internalRowCounter < 0)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.DR_NO_READ_CALLED, new string[0]));
				}
				if (OraType.ORA_OCIBLobLocator != this.m_readerImpl.m_accessors[i].m_internalType)
				{
					throw new InvalidCastException();
				}
				if (-1L == this.m_readerImpl.m_clientInitialLOBFS && ConfigBaseClass.m_bLegacyNegativeOneILFSBehavior)
				{
					throw new InvalidCastException();
				}
				if (!this.m_readerImpl.m_accessors[i].IsNullIndicatorSet(this.m_readerImpl.m_dataUnmarshaller, (int)this.m_readerImpl.m_sqlMetaData.m_noOfColumns, this.m_internalRowCounter, i))
				{
					OracleBlob oracleBlob;
					if (!this.m_fillReader && this.m_LobImplCache != null && this.m_LobImplCache[i] != null && this.m_LastCachedRowNumber[i] == (long)this.m_RowNumber)
					{
						OracleBlobImpl oracleBlobImpl = (OracleBlobImpl)this.m_LobImplCache[i];
						if (oracleBlobImpl.m_isTemporaryLob && !oracleBlobImpl.m_doneTempLobCreate)
						{
							oracleBlobImpl.CreateTemporaryLob();
							oracleBlobImpl.m_doneTempLobCreate = true;
						}
						oracleBlob = new OracleBlob(this.m_connection, oracleBlobImpl);
						oracleBlobImpl.AddRef();
					}
					else
					{
						OracleBlobImpl oracleBlobImpl2;
						if (this.m_connection.m_isDb11gR1OrHigher && ((!this.m_fillReader && -1L == this.m_readerImpl.m_clientInitialLOBFS) || (this.m_fillReader && !this.m_returnPSTypes)))
						{
							oracleBlobImpl2 = new OracleBlobImpl(this.m_internalRowCounter, this.m_connection.m_oracleConnectionImpl, (TTCLobAccessor)this.m_readerImpl.m_accessors[i]);
						}
						else
						{
							oracleBlobImpl2 = new OracleBlobImpl(this.m_connection.m_oracleConnectionImpl, (TTCLobAccessor)this.m_readerImpl.m_accessors[i], this.m_readerImpl.m_dataUnmarshaller, this.m_internalRowCounter, i);
						}
						if (oracleBlobImpl2.m_isTemporaryLob)
						{
							OracleBlobImpl oracleBlobImpl3 = (OracleBlobImpl)oracleBlobImpl2.m_connectionImpl.TemporaryLobReferenceGet(oracleBlobImpl2.m_lobId);
							if (oracleBlobImpl3 != null)
							{
								oracleBlob = new OracleBlob(this.m_connection, oracleBlobImpl3);
								oracleBlobImpl3.AddRef();
								oracleBlobImpl3.AddRef();
							}
							else
							{
								oracleBlobImpl2.m_connectionImpl.TemporaryLobReferenceAdd(oracleBlobImpl2.m_lobId, oracleBlobImpl2, true);
								oracleBlobImpl2.AddRef();
								oracleBlob = new OracleBlob(this.m_connection, oracleBlobImpl2);
							}
						}
						else
						{
							oracleBlob = new OracleBlob(this.m_connection, oracleBlobImpl2);
						}
						if (!this.m_fillReader && this.m_LobImplCache != null)
						{
							this.m_LobImplCache[i] = oracleBlob.m_blobImpl;
							this.m_LastCachedRowNumber[i] = (long)this.m_RowNumber;
						}
						((TTCLobAccessor)this.m_readerImpl.m_accessors[i]).m_lobLocators[this.m_internalRowCounter].Clear();
					}
					result = oracleBlob;
				}
				else
				{
					result = OracleBlob.Null;
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x060004DF RID: 1247 RVA: 0x00028D10 File Offset: 0x00026F10
		public OracleBlob GetOracleBlobForUpdate(int i)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			if (OraType.ORA_OCIBLobLocator != this.m_readerImpl.m_accessors[i].m_internalType)
			{
				throw new InvalidCastException();
			}
			OracleBlob result;
			try
			{
				byte[] oracleLobForUpdate = this.GetOracleLobForUpdate(i, -1);
				OracleBlob oracleBlob;
				if (oracleLobForUpdate != null)
				{
					oracleBlob = new OracleBlob(this.m_connection, oracleLobForUpdate);
				}
				else
				{
					oracleBlob = OracleBlob.Null;
				}
				result = oracleBlob;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x060004E0 RID: 1248 RVA: 0x00028DC0 File Offset: 0x00026FC0
		public OracleBlob GetOracleBlobForUpdate(int i, int wait)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			if (OraType.ORA_OCIBLobLocator != this.m_readerImpl.m_accessors[i].m_internalType)
			{
				throw new InvalidCastException();
			}
			OracleBlob result;
			try
			{
				byte[] oracleLobForUpdate = this.GetOracleLobForUpdate(i, wait);
				OracleBlob oracleBlob;
				if (oracleLobForUpdate != null)
				{
					oracleBlob = new OracleBlob(this.m_connection, oracleLobForUpdate);
				}
				else
				{
					oracleBlob = OracleBlob.Null;
				}
				result = oracleBlob;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x060004E1 RID: 1249 RVA: 0x00028E70 File Offset: 0x00027070
		public OracleClob GetOracleClob(int i)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleClob result;
			try
			{
				if (this.m_bclosed || this.m_bBeginingOfFile || this.m_bEndOfFile)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.OBJECT_CLOSED, new string[0]));
				}
				if (i >= this.m_fieldCount || i < 0)
				{
					throw new IndexOutOfRangeException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.DR_INV_COL_INDEX, new string[0]));
				}
				if (this.m_internalRowCounter < 0)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.DR_NO_READ_CALLED, new string[0]));
				}
				if (OraType.ORA_OCICLobLocator != this.m_readerImpl.m_accessors[i].m_internalType)
				{
					throw new InvalidCastException();
				}
				if (-1L == this.m_readerImpl.m_clientInitialLOBFS && ConfigBaseClass.m_bLegacyNegativeOneILFSBehavior)
				{
					throw new InvalidCastException();
				}
				if (!this.m_readerImpl.m_accessors[i].IsNullIndicatorSet(this.m_readerImpl.m_dataUnmarshaller, (int)this.m_readerImpl.m_sqlMetaData.m_noOfColumns, this.m_internalRowCounter, i))
				{
					OracleClob oracleClob;
					if (!this.m_fillReader && this.m_LobImplCache != null && this.m_LobImplCache[i] != null && this.m_LastCachedRowNumber[i] == (long)this.m_RowNumber)
					{
						OracleClobImpl oracleClobImpl = (OracleClobImpl)this.m_LobImplCache[i];
						if (oracleClobImpl.m_isTemporaryLob && !oracleClobImpl.m_doneTempLobCreate)
						{
							oracleClobImpl.CreateTemporaryLob();
							oracleClobImpl.m_doneTempLobCreate = true;
						}
						oracleClob = new OracleClob(this.m_connection, oracleClobImpl);
						oracleClobImpl.AddRef();
					}
					else
					{
						OracleClobImpl oracleClobImpl2;
						if (this.m_connection.m_isDb11gR1OrHigher && ((!this.m_fillReader && -1L == this.m_readerImpl.m_clientInitialLOBFS) || (this.m_fillReader && !this.m_returnPSTypes)))
						{
							oracleClobImpl2 = new OracleClobImpl(this.m_internalRowCounter, this.m_connection.m_oracleConnectionImpl, (TTCLobAccessor)this.m_readerImpl.m_accessors[i]);
						}
						else
						{
							oracleClobImpl2 = new OracleClobImpl(this.m_connection.m_oracleConnectionImpl, (TTCLobAccessor)this.m_readerImpl.m_accessors[i], this.m_readerImpl.m_dataUnmarshaller, this.m_internalRowCounter, i);
						}
						if (oracleClobImpl2.m_isTemporaryLob)
						{
							OracleClobImpl oracleClobImpl3 = (OracleClobImpl)oracleClobImpl2.m_connectionImpl.TemporaryLobReferenceGet(oracleClobImpl2.m_lobId);
							if (oracleClobImpl3 != null)
							{
								oracleClob = new OracleClob(this.m_connection, oracleClobImpl3);
								oracleClobImpl3.AddRef();
								oracleClobImpl3.AddRef();
							}
							else
							{
								oracleClobImpl2.m_connectionImpl.TemporaryLobReferenceAdd(oracleClobImpl2.m_lobId, oracleClobImpl2, true);
								oracleClobImpl2.AddRef();
								oracleClob = new OracleClob(this.m_connection, oracleClobImpl2);
							}
						}
						else
						{
							oracleClob = new OracleClob(this.m_connection, oracleClobImpl2);
						}
						if (!this.m_fillReader && this.m_LobImplCache != null)
						{
							this.m_LobImplCache[i] = oracleClob.m_clobImpl;
							this.m_LastCachedRowNumber[i] = (long)this.m_RowNumber;
						}
						((TTCLobAccessor)this.m_readerImpl.m_accessors[i]).m_lobLocators[this.m_internalRowCounter].Clear();
					}
					result = oracleClob;
				}
				else
				{
					result = OracleClob.Null;
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x060004E2 RID: 1250 RVA: 0x000291AC File Offset: 0x000273AC
		public OracleClob GetOracleClobForUpdate(int i)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			if (OraType.ORA_OCICLobLocator != this.m_readerImpl.m_accessors[i].m_internalType)
			{
				throw new InvalidCastException();
			}
			OracleClob result;
			try
			{
				byte[] oracleLobForUpdate = this.GetOracleLobForUpdate(i, -1);
				OracleClob oracleClob;
				if (oracleLobForUpdate != null)
				{
					bool bNClob = this.m_readerImpl.m_sqlMetaData.m_columnDescribeInfo[i].m_characterSetForm == 2;
					oracleClob = new OracleClob(this.m_connection, oracleLobForUpdate, bNClob, false);
				}
				else
				{
					oracleClob = OracleClob.Null;
				}
				result = oracleClob;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x060004E3 RID: 1251 RVA: 0x00029280 File Offset: 0x00027480
		public OracleClob GetOracleClobForUpdate(int i, int wait)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			if (OraType.ORA_OCICLobLocator != this.m_readerImpl.m_accessors[i].m_internalType)
			{
				throw new InvalidCastException();
			}
			OracleClob result;
			try
			{
				byte[] oracleLobForUpdate = this.GetOracleLobForUpdate(i, wait);
				OracleClob oracleClob;
				if (oracleLobForUpdate != null)
				{
					bool bNClob = this.m_readerImpl.m_sqlMetaData.m_columnDescribeInfo[i].m_characterSetForm == 2;
					oracleClob = new OracleClob(this.m_connection, oracleLobForUpdate, bNClob, false);
				}
				else
				{
					oracleClob = OracleClob.Null;
				}
				result = oracleClob;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x060004E4 RID: 1252 RVA: 0x00029354 File Offset: 0x00027554
		public OracleDate GetOracleDate(int i)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleDate result;
			try
			{
				if (this.m_bclosed || this.m_bBeginingOfFile || this.m_bEndOfFile)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.OBJECT_CLOSED, new string[0]));
				}
				if (i >= this.m_fieldCount || i < 0)
				{
					throw new IndexOutOfRangeException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.DR_INV_COL_INDEX, new string[0]));
				}
				if (this.m_internalRowCounter < 0)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.DR_NO_READ_CALLED, new string[0]));
				}
				if (OraType.ORA_DATE != this.m_readerImpl.m_accessors[i].m_internalType)
				{
					throw new InvalidCastException();
				}
				if (this.m_readerImpl.m_accessors[i].IsNullIndicatorSet(this.m_readerImpl.m_dataUnmarshaller, (int)this.m_readerImpl.m_sqlMetaData.m_noOfColumns, this.m_internalRowCounter, i))
				{
					result = OracleDate.Null;
				}
				else
				{
					byte[] array = null;
					OracleDate @null = OracleDate.Null;
					int offset = 0;
					int length = 0;
					bool flag = false;
					bool flag2 = false;
					bool bCopyData = true;
					OraColumnData oraColumnData = null;
					if (!this.m_isRowAddedToDatatable)
					{
						flag2 = this.m_readerImpl.m_dataUnmarshaller.TryGetValueIfDuplicate(this.m_internalRowCounter, i, out oraColumnData);
					}
					if (flag2)
					{
						if (oraColumnData.m_netTypeData != null && oraColumnData.m_netTypeData is OracleDate)
						{
							return (OracleDate)oraColumnData.m_netTypeData;
						}
						array = oraColumnData.m_rawData;
						length = array.Length;
					}
					else
					{
						if (!this.m_bUseDataSetAsDupStore && this.m_internalRowCounter < this.m_readerImpl.m_rowsFetched - 1)
						{
							flag = this.m_readerImpl.m_dataUnmarshaller.NextRowHasDuplicateData(this.m_internalRowCounter, i);
						}
						if (flag)
						{
							array = this.m_readerImpl.m_accessors[i].GetByteRepresentation(this.m_readerImpl.m_dataUnmarshaller, this.m_internalRowCounter, i);
							length = array.Length;
							bCopyData = false;
						}
						else
						{
							this.m_readerImpl.m_accessors[i].GetInternalDataRef(this.m_readerImpl.m_dataUnmarshaller, this.m_internalRowCounter, i, ref array, ref offset, ref length);
						}
					}
					if (array != null)
					{
						@null = new OracleDate(array, bCopyData, offset, length);
					}
					if (!flag2 && flag)
					{
						this.m_readerImpl.m_dataUnmarshaller.SaveColumnData(this.m_internalRowCounter, i, array, @null, true);
					}
					result = @null;
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x060004E5 RID: 1253 RVA: 0x000295F0 File Offset: 0x000277F0
		public OracleDecimal GetOracleDecimal(int i)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleDecimal result;
			try
			{
				if (this.m_bclosed || this.m_bBeginingOfFile || this.m_bEndOfFile)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.OBJECT_CLOSED, new string[0]));
				}
				if (i >= this.m_fieldCount || i < 0)
				{
					throw new IndexOutOfRangeException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.DR_INV_COL_INDEX, new string[0]));
				}
				if (this.m_internalRowCounter < 0)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.DR_NO_READ_CALLED, new string[0]));
				}
				OraType internalType = this.m_readerImpl.m_accessors[i].m_internalType;
				if (internalType != OraType.ORA_NUMBER && internalType != OraType.ORA_IBFLOAT && internalType != OraType.ORA_IBDOUBLE)
				{
					throw new InvalidCastException();
				}
				if (this.m_readerImpl.m_accessors[i].IsNullIndicatorSet(this.m_readerImpl.m_dataUnmarshaller, (int)this.m_readerImpl.m_sqlMetaData.m_noOfColumns, this.m_internalRowCounter, i))
				{
					result = OracleDecimal.Null;
				}
				else
				{
					OracleDecimal oracleDecimal = OracleDecimal.Null;
					byte[] array = null;
					OraColumnData oraColumnData = null;
					bool flag = false;
					if (!this.m_isRowAddedToDatatable)
					{
						flag = this.m_readerImpl.m_dataUnmarshaller.TryGetValueIfDuplicate(this.m_internalRowCounter, i, out oraColumnData);
					}
					if (flag)
					{
						if (oraColumnData.m_netTypeData != null && oraColumnData.m_netTypeData is OracleDecimal)
						{
							oracleDecimal = (OracleDecimal)oraColumnData.m_netTypeData;
						}
						else
						{
							array = oraColumnData.m_rawData;
							oracleDecimal = HelperClass.GetOracleDecimal(internalType, array, 0);
						}
					}
					else if (internalType == OraType.ORA_IBFLOAT)
					{
						float @float = this.m_readerImpl.GetFloat(this.m_internalRowCounter, i, out array);
						oracleDecimal = new OracleDecimal(@float);
					}
					else if (internalType == OraType.ORA_IBDOUBLE)
					{
						double @double = this.m_readerImpl.GetDouble(this.m_internalRowCounter, i, out array);
						oracleDecimal = new OracleDecimal(@double);
					}
					else
					{
						array = this.m_readerImpl.m_accessors[i].GetByteRepresentation(this.m_readerImpl.m_dataUnmarshaller, this.m_internalRowCounter, i);
						if (array != null)
						{
							oracleDecimal = new OracleDecimal(array, false);
						}
					}
					if (!this.m_bUseDataSetAsDupStore && !flag && this.m_internalRowCounter < this.m_readerImpl.m_rowsFetched - 1)
					{
						this.m_readerImpl.m_dataUnmarshaller.SaveColumnData(this.m_internalRowCounter, i, array, oracleDecimal, true);
					}
					result = oracleDecimal;
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x060004E6 RID: 1254 RVA: 0x00029880 File Offset: 0x00027A80
		public OracleIntervalDS GetOracleIntervalDS(int i)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleIntervalDS result;
			try
			{
				if (this.m_bclosed || this.m_bBeginingOfFile || this.m_bEndOfFile)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.OBJECT_CLOSED, new string[0]));
				}
				if (i >= this.m_fieldCount || i < 0)
				{
					throw new IndexOutOfRangeException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.DR_INV_COL_INDEX, new string[0]));
				}
				if (this.m_internalRowCounter < 0)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.DR_NO_READ_CALLED, new string[0]));
				}
				if (OraType.ORA_INTERVAL_DS != this.m_readerImpl.m_accessors[i].m_internalType && OraType.ORA_INTERVAL_DS_DTY != this.m_readerImpl.m_accessors[i].m_internalType)
				{
					throw new InvalidCastException();
				}
				if (this.m_readerImpl.m_accessors[i].IsNullIndicatorSet(this.m_readerImpl.m_dataUnmarshaller, (int)this.m_readerImpl.m_sqlMetaData.m_noOfColumns, this.m_internalRowCounter, i))
				{
					result = OracleIntervalDS.Null;
				}
				else
				{
					OracleIntervalDS @null = OracleIntervalDS.Null;
					bool flag = false;
					OraColumnData oraColumnData = null;
					if (!this.m_isRowAddedToDatatable)
					{
						flag = this.m_readerImpl.m_dataUnmarshaller.TryGetValueIfDuplicate(this.m_internalRowCounter, i, out oraColumnData);
					}
					byte[] array;
					if (flag)
					{
						if (oraColumnData.m_netTypeData != null && oraColumnData.m_netTypeData is OracleIntervalDS)
						{
							return (OracleIntervalDS)oraColumnData.m_netTypeData;
						}
						array = oraColumnData.m_rawData;
					}
					else
					{
						array = this.m_readerImpl.m_accessors[i].GetByteRepresentation(this.m_readerImpl.m_dataUnmarshaller, this.m_internalRowCounter, i);
					}
					if (array != null)
					{
						int precision = (int)this.m_readerImpl.m_accessors[i].m_colMetaData.m_precision;
						int scale = (int)this.m_readerImpl.m_accessors[i].m_colMetaData.m_scale;
						@null = new OracleIntervalDS(array, precision, scale, false);
					}
					if (!this.m_bUseDataSetAsDupStore && !flag && this.m_internalRowCounter < this.m_readerImpl.m_rowsFetched - 1)
					{
						this.m_readerImpl.m_dataUnmarshaller.SaveColumnData(this.m_internalRowCounter, i, array, @null, true);
					}
					result = @null;
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x060004E7 RID: 1255 RVA: 0x00029AFC File Offset: 0x00027CFC
		public OracleIntervalYM GetOracleIntervalYM(int i)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleIntervalYM result;
			try
			{
				if (this.m_bclosed || this.m_bBeginingOfFile || this.m_bEndOfFile)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.OBJECT_CLOSED, new string[0]));
				}
				if (i >= this.m_fieldCount || i < 0)
				{
					throw new IndexOutOfRangeException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.DR_INV_COL_INDEX, new string[0]));
				}
				if (this.m_internalRowCounter < 0)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.DR_NO_READ_CALLED, new string[0]));
				}
				if (OraType.ORA_INTERVAL_YM != this.m_readerImpl.m_accessors[i].m_internalType && OraType.ORA_INTERVAL_YM_DTY != this.m_readerImpl.m_accessors[i].m_internalType)
				{
					throw new InvalidCastException();
				}
				if (this.m_readerImpl.m_accessors[i].IsNullIndicatorSet(this.m_readerImpl.m_dataUnmarshaller, (int)this.m_readerImpl.m_sqlMetaData.m_noOfColumns, this.m_internalRowCounter, i))
				{
					result = OracleIntervalYM.Null;
				}
				else
				{
					OracleIntervalYM @null = OracleIntervalYM.Null;
					bool flag = false;
					OraColumnData oraColumnData = null;
					if (!this.m_isRowAddedToDatatable)
					{
						flag = this.m_readerImpl.m_dataUnmarshaller.TryGetValueIfDuplicate(this.m_internalRowCounter, i, out oraColumnData);
					}
					byte[] array;
					if (flag)
					{
						if (oraColumnData.m_netTypeData != null && oraColumnData.m_netTypeData is OracleIntervalYM)
						{
							return (OracleIntervalYM)oraColumnData.m_netTypeData;
						}
						array = oraColumnData.m_rawData;
					}
					else
					{
						array = this.m_readerImpl.m_accessors[i].GetByteRepresentation(this.m_readerImpl.m_dataUnmarshaller, this.m_internalRowCounter, i);
					}
					if (array != null)
					{
						int precision = (int)this.m_readerImpl.m_accessors[i].m_colMetaData.m_precision;
						@null = new OracleIntervalYM(array, precision, false);
					}
					if (!this.m_bUseDataSetAsDupStore && !flag && this.m_internalRowCounter < this.m_readerImpl.m_rowsFetched - 1)
					{
						this.m_readerImpl.m_dataUnmarshaller.SaveColumnData(this.m_internalRowCounter, i, array, @null, true);
					}
					result = @null;
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x060004E8 RID: 1256 RVA: 0x00029D60 File Offset: 0x00027F60
		public OracleString GetOracleString(int i)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleString result;
			try
			{
				if (this.IsDBNull(i))
				{
					result = OracleString.Null;
				}
				else
				{
					this.m_bInternalCall = true;
					string @string = this.GetString(i);
					this.m_bInternalCall = false;
					result = new OracleString(@string);
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x060004E9 RID: 1257 RVA: 0x00029DFC File Offset: 0x00027FFC
		public OracleTimeStamp GetOracleTimeStamp(int i)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleTimeStamp result;
			try
			{
				if (this.m_bclosed || this.m_bBeginingOfFile || this.m_bEndOfFile)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.OBJECT_CLOSED, new string[0]));
				}
				if (i >= this.m_fieldCount || i < 0)
				{
					throw new IndexOutOfRangeException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.DR_INV_COL_INDEX, new string[0]));
				}
				if (this.m_internalRowCounter < 0)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.DR_NO_READ_CALLED, new string[0]));
				}
				if (OraType.ORA_TIMESTAMP != this.m_readerImpl.m_accessors[i].m_internalType && OraType.ORA_TIMESTAMP_DTY != this.m_readerImpl.m_accessors[i].m_internalType)
				{
					throw new InvalidCastException();
				}
				if (this.m_readerImpl.m_accessors[i].IsNullIndicatorSet(this.m_readerImpl.m_dataUnmarshaller, (int)this.m_readerImpl.m_sqlMetaData.m_noOfColumns, this.m_internalRowCounter, i))
				{
					result = OracleTimeStamp.Null;
				}
				else
				{
					byte[] array = null;
					OracleTimeStamp @null = OracleTimeStamp.Null;
					int dataOffset = 0;
					int num = 0;
					bool bCopyData = true;
					bool flag = false;
					bool flag2 = false;
					OraColumnData oraColumnData = null;
					if (!this.m_isRowAddedToDatatable)
					{
						flag2 = this.m_readerImpl.m_dataUnmarshaller.TryGetValueIfDuplicate(this.m_internalRowCounter, i, out oraColumnData);
					}
					if (flag2)
					{
						if (oraColumnData.m_netTypeData != null && oraColumnData.m_netTypeData is OracleTimeStamp)
						{
							return (OracleTimeStamp)oraColumnData.m_netTypeData;
						}
						array = oraColumnData.m_rawData;
						num = array.Length;
					}
					else
					{
						if (!this.m_bUseDataSetAsDupStore && this.m_internalRowCounter < this.m_readerImpl.m_rowsFetched - 1)
						{
							flag = this.m_readerImpl.m_dataUnmarshaller.NextRowHasDuplicateData(this.m_internalRowCounter, i);
						}
						if (flag)
						{
							array = this.m_readerImpl.m_accessors[i].GetByteRepresentation(this.m_readerImpl.m_dataUnmarshaller, this.m_internalRowCounter, i);
							num = array.Length;
							if (array != null && num == 11)
							{
								bCopyData = false;
							}
						}
						else
						{
							this.m_readerImpl.m_accessors[i].GetInternalDataRef(this.m_readerImpl.m_dataUnmarshaller, this.m_internalRowCounter, i, ref array, ref dataOffset, ref num);
						}
					}
					if (array != null)
					{
						@null = new OracleTimeStamp(array, dataOffset, num, bCopyData);
					}
					if (flag && !flag2)
					{
						this.m_readerImpl.m_dataUnmarshaller.SaveColumnData(this.m_internalRowCounter, i, array, @null, true);
					}
					result = @null;
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x060004EA RID: 1258 RVA: 0x0002A0BC File Offset: 0x000282BC
		public OracleTimeStampLTZ GetOracleTimeStampLTZ(int i)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleTimeStampLTZ result;
			try
			{
				if (this.m_bclosed || this.m_bBeginingOfFile || this.m_bEndOfFile)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.OBJECT_CLOSED, new string[0]));
				}
				if (i >= this.m_fieldCount || i < 0)
				{
					throw new IndexOutOfRangeException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.DR_INV_COL_INDEX, new string[0]));
				}
				if (this.m_internalRowCounter < 0)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.DR_NO_READ_CALLED, new string[0]));
				}
				if (OraType.ORA_TIMESTAMP_LTZ != this.m_readerImpl.m_accessors[i].m_internalType && OraType.ORA_TIMESTAMP_LTZ_DTY != this.m_readerImpl.m_accessors[i].m_internalType)
				{
					throw new InvalidCastException();
				}
				if (this.m_readerImpl.m_accessors[i].IsNullIndicatorSet(this.m_readerImpl.m_dataUnmarshaller, (int)this.m_readerImpl.m_sqlMetaData.m_noOfColumns, this.m_internalRowCounter, i))
				{
					result = OracleTimeStampLTZ.Null;
				}
				else
				{
					byte[] array = null;
					OracleTimeStampLTZ @null = OracleTimeStampLTZ.Null;
					OraColumnData oraColumnData = null;
					bool flag = false;
					int sourceIndex = 0;
					int num = 0;
					bool flag2 = true;
					bool flag3 = false;
					if (!this.m_isRowAddedToDatatable)
					{
						flag3 = this.m_readerImpl.m_dataUnmarshaller.TryGetValueIfDuplicate(this.m_internalRowCounter, i, out oraColumnData);
					}
					if (flag3)
					{
						if (oraColumnData.m_netTypeData != null && oraColumnData.m_netTypeData is OracleTimeStampLTZ)
						{
							return (OracleTimeStampLTZ)oraColumnData.m_netTypeData;
						}
						array = oraColumnData.m_rawData;
						num = array.Length;
					}
					else
					{
						if (!this.m_bUseDataSetAsDupStore && this.m_internalRowCounter < this.m_readerImpl.m_rowsFetched - 1)
						{
							flag = this.m_readerImpl.m_dataUnmarshaller.NextRowHasDuplicateData(this.m_internalRowCounter, i);
						}
						if (flag)
						{
							array = this.m_readerImpl.m_accessors[i].GetByteRepresentation(this.m_readerImpl.m_dataUnmarshaller, this.m_internalRowCounter, i);
							num = array.Length;
							if (array != null && num == 11)
							{
								flag2 = false;
							}
						}
						else
						{
							this.m_readerImpl.m_accessors[i].GetInternalDataRef(this.m_readerImpl.m_dataUnmarshaller, this.m_internalRowCounter, i, ref array, ref sourceIndex, ref num);
						}
					}
					DateTime? dateTime = null;
					if (array != null)
					{
						byte[] binData = null;
						OracleTimeZoneInfo? dbtimeZoneBytes = this.m_connection.m_oracleConnectionImpl.GetDBTimeZoneBytes();
						if (flag2)
						{
							byte[] array2 = new byte[11];
							Array.Copy(array, sourceIndex, array2, 0, num);
							TimeStamp.ConvertDBTimeToLTZData(array2, dbtimeZoneBytes, this.m_readerImpl.m_sessionTimeZone, out binData, out dateTime, false);
						}
						else
						{
							TimeStamp.ConvertDBTimeToLTZData(array, dbtimeZoneBytes, this.m_readerImpl.m_sessionTimeZone, out binData, out dateTime, false);
						}
						@null = new OracleTimeStampLTZ(binData, false);
					}
					if (flag && !flag3)
					{
						this.m_readerImpl.m_dataUnmarshaller.SaveColumnData(this.m_internalRowCounter, i, array, @null, true);
					}
					result = @null;
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x060004EB RID: 1259 RVA: 0x0002A3E0 File Offset: 0x000285E0
		public OracleTimeStampTZ GetOracleTimeStampTZ(int i)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleTimeStampTZ result;
			try
			{
				if (this.m_bclosed || this.m_bBeginingOfFile || this.m_bEndOfFile)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.OBJECT_CLOSED, new string[0]));
				}
				if (i >= this.m_fieldCount || i < 0)
				{
					throw new IndexOutOfRangeException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.DR_INV_COL_INDEX, new string[0]));
				}
				if (this.m_internalRowCounter < 0)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.DR_NO_READ_CALLED, new string[0]));
				}
				if (OraType.ORA_TIMESTAMP_TZ != this.m_readerImpl.m_accessors[i].m_internalType && OraType.ORA_TIMESTAMP_TZ_DTY != this.m_readerImpl.m_accessors[i].m_internalType)
				{
					throw new InvalidCastException();
				}
				if (this.m_readerImpl.m_accessors[i].IsNullIndicatorSet(this.m_readerImpl.m_dataUnmarshaller, (int)this.m_readerImpl.m_sqlMetaData.m_noOfColumns, this.m_internalRowCounter, i))
				{
					result = OracleTimeStampTZ.Null;
				}
				else
				{
					OracleTimeStampTZ @null = OracleTimeStampTZ.Null;
					byte[] array = null;
					int num = 0;
					int num2 = 0;
					bool flag = true;
					bool flag2 = false;
					bool flag3 = false;
					OraColumnData oraColumnData = null;
					if (!this.m_isRowAddedToDatatable)
					{
						flag3 = this.m_readerImpl.m_dataUnmarshaller.TryGetValueIfDuplicate(this.m_internalRowCounter, i, out oraColumnData);
					}
					if (flag3)
					{
						if (oraColumnData.m_netTypeData != null && oraColumnData.m_netTypeData is OracleTimeStampTZ)
						{
							return (OracleTimeStampTZ)oraColumnData.m_netTypeData;
						}
						array = oraColumnData.m_rawData;
						num2 = array.Length;
					}
					else
					{
						if (!this.m_bUseDataSetAsDupStore && this.m_internalRowCounter < this.m_readerImpl.m_rowsFetched - 1)
						{
							flag2 = this.m_readerImpl.m_dataUnmarshaller.NextRowHasDuplicateData(this.m_internalRowCounter, i);
						}
						if (flag2)
						{
							array = this.m_readerImpl.m_accessors[i].GetByteRepresentation(this.m_readerImpl.m_dataUnmarshaller, this.m_internalRowCounter, i);
							num2 = array.Length;
							flag = false;
						}
						else
						{
							this.m_readerImpl.m_accessors[i].GetInternalDataRef(this.m_readerImpl.m_dataUnmarshaller, this.m_internalRowCounter, i, ref array, ref num, ref num2);
						}
					}
					if (array != null)
					{
						if (this.m_connection.m_oracleConnectionImpl.IsTZDataSentAsLocalTime)
						{
							byte[] binData = null;
							DateTime? dateTime = null;
							if (flag)
							{
								byte[] array2 = new byte[13];
								Array.Copy(array, num, array2, 0, num2);
								TimeStamp.GetUTCByteRepFromLocalArray(array2, out binData, out dateTime, false);
							}
							else
							{
								TimeStamp.GetUTCByteRepFromLocalArray(array, out binData, out dateTime, false);
							}
							@null = new OracleTimeStampTZ(binData, false, 0, num2);
						}
						else
						{
							@null = new OracleTimeStampTZ(array, flag, num, num2);
						}
					}
					if (flag2 && !flag3)
					{
						this.m_readerImpl.m_dataUnmarshaller.SaveColumnData(this.m_internalRowCounter, i, array, @null, true);
					}
					result = @null;
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x060004EC RID: 1260 RVA: 0x0002A6F4 File Offset: 0x000288F4
		public OracleXmlType GetOracleXmlType(int i)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleXmlType result;
			try
			{
				if (this.m_bclosed || this.m_bBeginingOfFile || this.m_bEndOfFile)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.OBJECT_CLOSED, new string[0]));
				}
				if (i >= this.m_fieldCount || i < 0)
				{
					throw new IndexOutOfRangeException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.DR_INV_COL_INDEX, new string[0]));
				}
				if (this.m_internalRowCounter < 0)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.DR_NO_READ_CALLED, new string[0]));
				}
				if (OraType.ORA_XMLTYPE != this.m_readerImpl.m_accessors[i].m_internalType)
				{
					throw new InvalidCastException();
				}
				if (!this.m_readerImpl.m_accessors[i].IsNullIndicatorSet(this.m_readerImpl.m_dataUnmarshaller, (int)this.m_readerImpl.m_sqlMetaData.m_noOfColumns, this.m_internalRowCounter, i))
				{
					OraXmlTypeHeader xmlTypeHeader = new OraXmlTypeHeader();
					OraXmlTypeData oraXmlTypeData = null;
					TTCXmlTypeAccessor ttcxmlTypeAccessor = (TTCXmlTypeAccessor)this.m_readerImpl.m_accessors[i];
					ttcxmlTypeAccessor.UnpickleXmlType(this.m_connection.m_oracleConnectionImpl, this.m_readerImpl.m_dataUnmarshaller, this.m_internalRowCounter, i, xmlTypeHeader, out oraXmlTypeData);
					if (oraXmlTypeData != null)
					{
						OracleXmlTypeImpl xmlTypeImpl = new OracleXmlTypeImpl(this.m_connection.m_oracleConnectionImpl, xmlTypeHeader, oraXmlTypeData);
						result = new OracleXmlType(this.m_connection, xmlTypeImpl);
					}
					else
					{
						result = OracleXmlType.Null;
					}
				}
				else
				{
					result = OracleXmlType.Null;
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x060004ED RID: 1261 RVA: 0x0002A8B8 File Offset: 0x00028AB8
		public object GetOracleValue(int i)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			object result;
			try
			{
				if (this.m_bclosed || this.m_bBeginingOfFile || this.m_bEndOfFile)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.OBJECT_CLOSED, new string[0]));
				}
				if (i >= this.m_fieldCount || i < 0)
				{
					throw new IndexOutOfRangeException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.DR_INV_COL_INDEX, new string[0]));
				}
				if (this.m_internalRowCounter < 0)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.DR_NO_READ_CALLED, new string[0]));
				}
				object obj = null;
				OraType internalType = this.m_readerImpl.m_accessors[i].m_internalType;
				if (this.m_bUseDataSetAsDupStore)
				{
					if (this.m_currentDataTableForFill == null)
					{
						this.m_currentDataTableIndex++;
						if (this.m_dataTablesReferenceForFill is DataTable)
						{
							this.m_currentDataTableForFill = (DataTable)this.m_dataTablesReferenceForFill;
						}
						else if (this.m_dataTablesReferenceForFill is DataTable[])
						{
							this.m_currentDataTableForFill = ((DataTable[])this.m_dataTablesReferenceForFill)[this.m_currentDataTableIndex];
						}
						else
						{
							this.m_currentDataTableForFill = ((DataTableCollection)this.m_dataTablesReferenceForFill)[((DataTableCollection)this.m_dataTablesReferenceForFill).Count - 1];
						}
						if (this.m_currentDataTableForFill != null)
						{
							this.m_initialRowCnt = this.m_currentDataTableForFill.Rows.Count;
							this.m_isRowAddedToDatatable = false;
						}
					}
					bool flag = false;
					if (this.m_isRowAddedToDatatable)
					{
						flag = this.m_readerImpl.m_dataUnmarshaller.TryGetValueIfDuplicateInDataSet(this.m_internalRowCounter, i, this.m_currentDataTableForFill, this.m_readerImpl.m_sqlMetaData.m_columnDescribeInfo[i].pColAlias, out obj);
					}
					if (flag)
					{
						return obj;
					}
				}
				OraType oraType = internalType;
				if (oraType <= OraType.ORA_CHAR)
				{
					if (oraType <= OraType.ORA_DATE)
					{
						switch (oraType)
						{
						case OraType.ORA_CHARN:
							break;
						case OraType.ORA_NUMBER:
							obj = this.GetOracleDecimal(i);
							goto IL_3BC;
						default:
							switch (oraType)
							{
							case OraType.ORA_LONG:
							case OraType.ORA_ROWID:
								break;
							case OraType.ORA_VARCHAR:
							case (OraType)10:
								goto IL_3BC;
							case OraType.ORA_DATE:
								obj = this.GetOracleDate(i);
								goto IL_3BC;
							default:
								goto IL_3BC;
							}
							break;
						}
					}
					else
					{
						switch (oraType)
						{
						case OraType.ORA_RAW:
						case OraType.ORA_LONGRAW:
							obj = this.GetOracleBinary(i);
							goto IL_3BC;
						default:
							if (oraType != OraType.ORA_CHAR)
							{
								goto IL_3BC;
							}
							break;
						}
					}
				}
				else if (oraType <= OraType.ORA_OCIBFileLocator)
				{
					switch (oraType)
					{
					case OraType.ORA_IBFLOAT:
					case OraType.ORA_IBDOUBLE:
						obj = this.GetOracleDecimal(i);
						goto IL_3BC;
					default:
						switch (oraType)
						{
						case OraType.ORA_XMLTYPE:
							obj = this.GetOracleXmlType(i);
							goto IL_3BC;
						case OraType.ORA_OCIRef:
						case (OraType)111:
							goto IL_3BC;
						case OraType.ORA_OCICLobLocator:
							if (-1L == this.m_readerImpl.m_clientInitialLOBFS && ConfigBaseClass.m_bLegacyNegativeOneILFSBehavior)
							{
								obj = this.GetOracleString(i);
								goto IL_3BC;
							}
							obj = this.GetOracleClob(i);
							goto IL_3BC;
						case OraType.ORA_OCIBLobLocator:
							if (-1L == this.m_readerImpl.m_clientInitialLOBFS && ConfigBaseClass.m_bLegacyNegativeOneILFSBehavior)
							{
								obj = this.GetOracleBinary(i);
								goto IL_3BC;
							}
							obj = this.GetOracleBlob(i);
							goto IL_3BC;
						case OraType.ORA_OCIBFileLocator:
							obj = this.GetOracleBFile(i);
							goto IL_3BC;
						default:
							goto IL_3BC;
						}
						break;
					}
				}
				else
				{
					switch (oraType)
					{
					case OraType.ORA_TIMESTAMP_DTY:
					case OraType.ORA_TIMESTAMP:
						obj = this.GetOracleTimeStamp(i);
						goto IL_3BC;
					case OraType.ORA_TIMESTAMP_TZ_DTY:
					case OraType.ORA_TIMESTAMP_TZ:
						obj = this.GetOracleTimeStampTZ(i);
						goto IL_3BC;
					case OraType.ORA_INTERVAL_YM_DTY:
					case OraType.ORA_INTERVAL_YM:
						obj = this.GetOracleIntervalYM(i);
						goto IL_3BC;
					case OraType.ORA_INTERVAL_DS_DTY:
					case OraType.ORA_INTERVAL_DS:
						obj = this.GetOracleIntervalDS(i);
						goto IL_3BC;
					case (OraType)184:
					case (OraType)185:
					case OraType.ORA_TIME_TZ:
						goto IL_3BC;
					default:
						if (oraType != OraType.ORA_UROWID)
						{
							switch (oraType)
							{
							case OraType.ORA_TIMESTAMP_LTZ_DTY:
							case OraType.ORA_TIMESTAMP_LTZ:
								obj = this.GetOracleTimeStampLTZ(i);
								goto IL_3BC;
							default:
								goto IL_3BC;
							}
						}
						break;
					}
				}
				obj = this.GetOracleString(i);
				IL_3BC:
				result = obj;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x060004EE RID: 1262 RVA: 0x0002ACE8 File Offset: 0x00028EE8
		public int GetOracleValues(object[] values)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			int result;
			try
			{
				if (this.m_bclosed)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.OBJECT_CLOSED, new string[0]));
				}
				int num = values.Length;
				int num2;
				if (num < this.m_fieldCount)
				{
					num2 = num;
				}
				else
				{
					num2 = this.m_fieldCount;
				}
				for (int i = 0; i < num2; i++)
				{
					values[i] = this.GetOracleValue(i);
				}
				result = num2;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x060004EF RID: 1263 RVA: 0x0002ADA8 File Offset: 0x00028FA8
		public override int GetOrdinal(string name)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			int columnOrdinal;
			try
			{
				if (this.m_bclosed)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.OBJECT_CLOSED, new string[0]));
				}
				columnOrdinal = this.m_readerImpl.GetColumnOrdinal(name);
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
			return columnOrdinal;
		}

		// Token: 0x060004F0 RID: 1264 RVA: 0x0002AE40 File Offset: 0x00029040
		public override Type GetProviderSpecificFieldType(int ordinal)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			Type result;
			try
			{
				if (this.m_bclosed)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.OBJECT_CLOSED, new string[0]));
				}
				if (ordinal >= this.m_fieldCount || ordinal < 0)
				{
					throw new IndexOutOfRangeException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.DR_INV_COL_INDEX, new string[0]));
				}
				Type type = null;
				switch (this.GetOraDbType(ordinal))
				{
				case OracleDbType.BFile:
					type = ODPType.OraBFile;
					break;
				case OracleDbType.Blob:
					type = ODPType.OraBlob;
					break;
				case OracleDbType.Byte:
				case OracleDbType.Decimal:
				case OracleDbType.Double:
				case OracleDbType.Int16:
				case OracleDbType.Int32:
				case OracleDbType.Int64:
				case OracleDbType.Single:
				case OracleDbType.BinaryDouble:
				case OracleDbType.BinaryFloat:
					type = ODPType.OraDecimal;
					break;
				case OracleDbType.Char:
				case OracleDbType.Long:
				case OracleDbType.NChar:
				case OracleDbType.NVarchar2:
				case OracleDbType.Varchar2:
					type = ODPType.OraString;
					break;
				case OracleDbType.Clob:
				case OracleDbType.NClob:
					type = ODPType.OraClob;
					break;
				case OracleDbType.Date:
					type = ODPType.OraDate;
					break;
				case OracleDbType.LongRaw:
				case OracleDbType.Raw:
					type = ODPType.OraBinary;
					break;
				case OracleDbType.IntervalDS:
					type = ODPType.OraIntervalDS;
					break;
				case OracleDbType.IntervalYM:
					type = ODPType.OraIntervalYM;
					break;
				case OracleDbType.RefCursor:
					type = ODPType.OraRefCursor;
					break;
				case OracleDbType.TimeStamp:
					type = ODPType.OraTimeStamp;
					break;
				case OracleDbType.TimeStampLTZ:
					type = ODPType.OraTimeStampLTZ;
					break;
				case OracleDbType.TimeStampTZ:
					type = ODPType.OraTimeStampTZ;
					break;
				case OracleDbType.XmlType:
					type = ODPType.OraXmlType;
					break;
				}
				result = type;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x060004F1 RID: 1265 RVA: 0x0002B010 File Offset: 0x00029210
		public override object GetProviderSpecificValue(int ordinal)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			object oracleValue;
			try
			{
				oracleValue = this.GetOracleValue(ordinal);
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
			return oracleValue;
		}

		// Token: 0x060004F2 RID: 1266 RVA: 0x0002B088 File Offset: 0x00029288
		public override int GetProviderSpecificValues(object[] values)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			int oracleValues;
			try
			{
				oracleValues = this.GetOracleValues(values);
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
			return oracleValues;
		}

		// Token: 0x060004F3 RID: 1267 RVA: 0x0002B100 File Offset: 0x00029300
		private void PopulateSchemaTable(DataTable dt)
		{
			string s = this.m_commandText;
			if (this.m_commandText != null)
			{
				string text = this.m_commandText.TrimEnd(new char[0]);
				if (!text.EndsWith(";"))
				{
					s = this.m_commandText + ";";
				}
			}
			IEnumerable<OracleLpStatement> enumerable = null;
			try
			{
				enumerable = OracleConnection.OracleLpParser.ParseStatements(this.m_connection, s);
			}
			catch (Exception ex)
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					string text2 = this.m_commandText.Replace(OracleDataReader.s_replaceString, string.Empty);
					string text3 = ex.ToString().Replace(OracleDataReader.s_replaceString, string.Empty);
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
			if (enumerable != null)
			{
				foreach (OracleLpStatement oracleLpStatement in enumerable)
				{
					if (oracleLpStatement.StatementType == OracleLpStatementType.Select)
					{
						int num = 0;
						int count = dt.Rows.Count;
						foreach (OracleLpColumnDescriptor oracleLpColumnDescriptor in ((OracleLpSelectStatement)oracleLpStatement).ColumnDescriptors)
						{
							if (oracleLpColumnDescriptor.ColumnName != null)
							{
								DataRow dataRow = dt.Rows[num];
								if (oracleLpColumnDescriptor.ColumnName.DbName == (string)dataRow["ColumnName"])
								{
									if (oracleLpColumnDescriptor.BaseSchemaName != null)
									{
										dataRow["BaseSchemaName"] = oracleLpColumnDescriptor.BaseSchemaName.DbName;
									}
									if (oracleLpColumnDescriptor.BaseTableName != null)
									{
										dataRow["BaseTableName"] = oracleLpColumnDescriptor.BaseTableName.DbName;
									}
									if (oracleLpColumnDescriptor.BaseColumnName != null)
									{
										dataRow["BaseColumnName"] = oracleLpColumnDescriptor.BaseColumnName.DbName;
										if (!string.IsNullOrEmpty(oracleLpColumnDescriptor.BaseColumnName.DbName))
										{
											dataRow["IsAliased"] = (oracleLpColumnDescriptor.BaseColumnName != oracleLpColumnDescriptor.ColumnName);
										}
									}
									if (oracleLpColumnDescriptor.BaseTableName == null || string.IsNullOrEmpty(oracleLpColumnDescriptor.BaseTableName.DbName))
									{
										dataRow["IsExpression"] = true;
										dataRow["IsReadOnly"] = true;
										dataRow["BaseColumnName"] = null;
									}
									else
									{
										dataRow["IsExpression"] = false;
										dataRow["IsReadOnly"] = false;
									}
									dataRow["IsRowID"] = oracleLpColumnDescriptor.IsRowID;
									if (oracleLpColumnDescriptor.IsRowID)
									{
										dataRow["IsReadOnly"] = true;
										dataRow["IsExpression"] = false;
									}
									dataRow["IsHidden"] = oracleLpColumnDescriptor.IsHidden;
								}
							}
							num++;
						}
					}
				}
			}
		}

		// Token: 0x060004F4 RID: 1268 RVA: 0x0002B47C File Offset: 0x0002967C
		public override DataTable GetSchemaTable()
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			DataTable schemaTableCopy;
			try
			{
				schemaTableCopy = this.GetSchemaTableCopy(ref this.m_dataTable, false);
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
			return schemaTableCopy;
		}

		// Token: 0x060004F5 RID: 1269 RVA: 0x0002B4F8 File Offset: 0x000296F8
		internal DataTable GetSchemaTableEx()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			DataTable schemaTableCopy;
			try
			{
				schemaTableCopy = this.GetSchemaTableCopy(ref this.m_dataTableEx, true);
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
			return schemaTableCopy;
		}

		// Token: 0x060004F6 RID: 1270 RVA: 0x0002B574 File Offset: 0x00029774
		private DataTable GetSchemaTableCopy(ref DataTable dataTable, bool isFromEx)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			DataTable result;
			try
			{
				if (this.m_bclosed)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.OBJECT_CLOSED, new string[0]));
				}
				if (this.m_readerImpl == null || this.m_readerImpl.m_sqlMetaData == null)
				{
					result = null;
				}
				else if (this.m_noMoreResults)
				{
					result = null;
				}
				else
				{
					DataTable dataTable2 = null;
					if (dataTable == null)
					{
						this.PopulateMetaData(true);
						this.RetrieveSchemaTable(ref dataTable, isFromEx);
						if (!isFromEx && this.m_sqlStatementType != SqlStatementType.SELECT)
						{
							if (this.m_refCursor != null)
							{
								RefCursorInfo refCursorInfo = this.m_refCursor.m_refCursorInfo;
								if (refCursorInfo != null && refCursorInfo.columnInfo.Rows.Count > 0)
								{
									dataTable2 = refCursorInfo.columnInfo;
								}
							}
							else
							{
								ConfigBaseClass.StoredProcedureInfo storedProcInfo = ConfigBaseClass.GetInstance(true).GetStoredProcInfo(this.m_storedProcName);
								if (storedProcInfo != null)
								{
									if (this.m_numExplicitBoundRefCursors != 0 && storedProcInfo.m_numExplicitBoundRefCursors != this.m_numExplicitBoundRefCursors)
									{
										storedProcInfo.m_numExplicitBoundRefCursors = this.m_numExplicitBoundRefCursors;
									}
									dataTable2 = storedProcInfo.GetColumnInfo(this.m_readerImpl.m_currentRefCursorIndex);
								}
							}
							if (dataTable2 != null)
							{
								DataTable dataTable3 = dataTable2.Copy();
								dataTable3.Columns.Remove("NativeDataType");
								dataTable3.Columns.Remove("ProviderDBType");
								dataTable3.Columns.Remove("ObjectName");
								int num = 0;
								int count = dataTable3.Rows.Count;
								foreach (object obj in dataTable.Rows)
								{
									DataRow srcDataRow = (DataRow)obj;
									if (num >= count)
									{
										break;
									}
									RefCursorInfo refCursorInfo2 = new RefCursorInfo();
									refCursorInfo2.FillMissingValuesFromMetadata(srcDataRow, dataTable3.Rows[num++]);
								}
								dataTable3.AcceptChanges();
								return dataTable3;
							}
						}
					}
					result = dataTable.Copy();
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
			return result;
		}

		// Token: 0x060004F7 RID: 1271 RVA: 0x0002B7CC File Offset: 0x000299CC
		private void RetrieveSchemaTable(ref DataTable dataTable, bool isFromEx)
		{
			dataTable = new DataTable("SchemaTable");
			dataTable.MinimumCapacity = this.m_fieldCount;
			dataTable.Columns.Add("ColumnName", typeof(string));
			dataTable.Columns.Add("ColumnOrdinal", typeof(int));
			dataTable.Columns.Add("ColumnSize", typeof(int));
			dataTable.Columns.Add("NumericPrecision", typeof(short));
			dataTable.Columns.Add("NumericScale", typeof(short));
			dataTable.Columns.Add("IsUnique", typeof(bool));
			dataTable.Columns.Add("IsKey", typeof(bool));
			dataTable.Columns.Add("IsRowID", typeof(bool));
			dataTable.Columns.Add("BaseColumnName", typeof(string));
			dataTable.Columns.Add("BaseSchemaName", typeof(string));
			dataTable.Columns.Add("BaseTableName", typeof(string));
			dataTable.Columns.Add("DataType", typeof(Type));
			dataTable.Columns.Add("ProviderType", typeof(OracleDbType));
			dataTable.Columns.Add("AllowDBNull", typeof(bool));
			dataTable.Columns.Add("IsAliased", typeof(bool));
			dataTable.Columns.Add("IsByteSemantic", typeof(bool));
			dataTable.Columns.Add("IsExpression", typeof(bool));
			dataTable.Columns.Add("IsHidden", typeof(bool));
			dataTable.Columns.Add("IsReadOnly", typeof(bool));
			dataTable.Columns.Add("IsLong", typeof(bool));
			if (isFromEx)
			{
				dataTable.Columns.Add("NativeDataType", typeof(string));
			}
			int columnIndex = dataTable.Columns.Count - 1;
			ColumnLocalParsePrimaryKeyInfo columnLocalParsePrimaryKeyInfo = ColumnLocalParsePrimaryKeyInfo.Null;
			bool flag = this.m_readerImpl.m_sqlMetaData.m_sqlMetaInfo != null && this.m_readerImpl.m_sqlMetaData.m_sqlMetaInfo.m_columnMetaInfo != null;
			bool flag2 = this.m_readerImpl.m_numberOfHiddenColumns > 0;
			int i = 0;
			while (i < this.m_fieldCount)
			{
				ColumnDescribeInfo columnDescribeInfo = this.m_readerImpl.m_sqlMetaData.m_columnDescribeInfo[i];
				if (flag)
				{
					columnLocalParsePrimaryKeyInfo = this.m_readerImpl.m_sqlMetaData.m_sqlMetaInfo.m_columnMetaInfo[i];
				}
				DataRow dataRow = dataTable.NewRow();
				dataRow[0] = columnDescribeInfo.pColAlias;
				dataRow[7] = false;
				dataRow[19] = false;
				dataRow[1] = i;
				dataRow[8] = columnLocalParsePrimaryKeyInfo.m_columnName;
				dataRow[9] = columnLocalParsePrimaryKeyInfo.m_schemaName;
				dataRow[10] = columnLocalParsePrimaryKeyInfo.pTabName;
				dataRow[13] = columnDescribeInfo.m_isNullAllowed;
				OraType dataType = (OraType)columnDescribeInfo.m_dataType;
				OraType oraType = dataType;
				if (oraType <= OraType.ORA_CHAR)
				{
					if (oraType <= OraType.ORA_DATE)
					{
						switch (oraType)
						{
						case OraType.ORA_CHARN:
							break;
						case OraType.ORA_NUMBER:
							dataRow[2] = 22;
							goto IL_5D7;
						default:
							switch (oraType)
							{
							case OraType.ORA_LONG:
								goto IL_526;
							case OraType.ORA_VARCHAR:
								break;
							case (OraType)10:
								goto IL_5C4;
							case OraType.ORA_ROWID:
								goto IL_4A1;
							case OraType.ORA_DATE:
								dataRow[2] = 7;
								goto IL_5D7;
							default:
								goto IL_5C4;
							}
							break;
						}
					}
					else
					{
						if (oraType == OraType.ORA_LONGRAW)
						{
							goto IL_526;
						}
						if (oraType != OraType.ORA_CHAR)
						{
							goto IL_5C4;
						}
					}
					if ((columnDescribeInfo.m_contFlag & 4096) != 4096)
					{
						dataRow[15] = true;
						dataRow[2] = columnDescribeInfo.m_maxLength;
					}
					else
					{
						dataRow[2] = columnDescribeInfo.m_maxLengthOfChars;
						if (columnDescribeInfo.m_characterSetForm != 2)
						{
							dataRow[15] = false;
						}
					}
				}
				else
				{
					if (oraType <= OraType.ORA_INTERVAL_DS)
					{
						switch (oraType)
						{
						case OraType.ORA_XMLTYPE:
							dataRow[2] = int.MaxValue;
							goto IL_5D7;
						case OraType.ORA_OCIRef:
						case (OraType)111:
							goto IL_5C4;
						case OraType.ORA_OCICLobLocator:
						case OraType.ORA_OCIBLobLocator:
						case OraType.ORA_OCIBFileLocator:
							goto IL_526;
						default:
							switch (oraType)
							{
							case OraType.ORA_TIMESTAMP_DTY:
							case OraType.ORA_INTERVAL_DS_DTY:
							case OraType.ORA_TIMESTAMP:
							case OraType.ORA_INTERVAL_DS:
								break;
							case OraType.ORA_TIMESTAMP_TZ_DTY:
							case OraType.ORA_TIMESTAMP_TZ:
								dataRow[2] = 13;
								goto IL_5D7;
							case OraType.ORA_INTERVAL_YM_DTY:
							case OraType.ORA_INTERVAL_YM:
								dataRow[2] = 5;
								goto IL_5D7;
							case (OraType)184:
							case (OraType)185:
							case OraType.ORA_TIME_TZ:
								goto IL_5C4;
							default:
								goto IL_5C4;
							}
							break;
						}
					}
					else
					{
						if (oraType == OraType.ORA_UROWID)
						{
							goto IL_4A1;
						}
						switch (oraType)
						{
						case OraType.ORA_TIMESTAMP_LTZ_DTY:
						case OraType.ORA_TIMESTAMP_LTZ:
							break;
						default:
							goto IL_5C4;
						}
					}
					dataRow[2] = 11;
				}
				IL_5D7:
				if (dataType == OraType.ORA_NUMBER || dataType == OraType.ORA_INTERVAL_DS || dataType == OraType.ORA_INTERVAL_DS_DTY || dataType == OraType.ORA_INTERVAL_YM || dataType == OraType.ORA_INTERVAL_YM_DTY)
				{
					dataRow[3] = columnDescribeInfo.m_precision;
				}
				if (dataType == OraType.ORA_NUMBER || dataType == OraType.ORA_INTERVAL_DS || dataType == OraType.ORA_INTERVAL_DS_DTY || dataType == OraType.ORA_TIMESTAMP || dataType == OraType.ORA_TIMESTAMP_DTY || dataType == OraType.ORA_TIMESTAMP_LTZ || dataType == OraType.ORA_TIMESTAMP_LTZ_DTY || dataType == OraType.ORA_TIMESTAMP_TZ || dataType == OraType.ORA_TIMESTAMP_TZ_DTY)
				{
					dataRow[4] = columnDescribeInfo.m_scale;
				}
				if ((this.m_commandBehavior & CommandBehavior.KeyInfo) == CommandBehavior.KeyInfo)
				{
					dataRow[5] = columnLocalParsePrimaryKeyInfo.bIsUnique;
					dataRow[6] = columnLocalParsePrimaryKeyInfo.bIsKeyColumn;
				}
				if (this.m_returnPSTypes)
				{
					dataRow[11] = this.GetProviderSpecificFieldType(i);
				}
				else
				{
					dataRow[11] = this.GetFieldType(i);
				}
				if (this.IsCorruptible(dataType) && (Type)dataRow[11] == typeof(string))
				{
					dataRow[2] = -1;
				}
				OracleDbType oraDbType = this.GetOraDbType(i);
				dataRow[12] = oraDbType;
				if (this.m_sqlStatementType != SqlStatementType.PLSQL)
				{
					dataRow[14] = (columnLocalParsePrimaryKeyInfo.m_columnName != columnDescribeInfo.pColAlias);
					dataRow[16] = columnLocalParsePrimaryKeyInfo.bIsExpression;
				}
				dataRow[17] = ((flag2 && columnLocalParsePrimaryKeyInfo.m_columnName == "ROWID") || columnLocalParsePrimaryKeyInfo.bIsHidden);
				if (columnLocalParsePrimaryKeyInfo.Updatable || (this.m_sqlStatementType == SqlStatementType.PLSQL && !(bool)dataRow[7]))
				{
					dataRow[18] = false;
				}
				else
				{
					dataRow[18] = true;
				}
				if (isFromEx)
				{
					try
					{
						if (dataType == OraType.ORA_ROWID)
						{
							dataRow[columnIndex] = "ROWID";
						}
						else if (dataType == OraType.ORA_UROWID)
						{
							dataRow[columnIndex] = "UROWID";
						}
						else
						{
							int num = oraDbType - OracleDbType.BFile;
							dataRow[columnIndex] = OracleTypeMapper.m_OraDbToOraNative[num];
						}
					}
					catch
					{
						dataRow[columnIndex] = string.Empty;
					}
				}
				dataTable.Rows.Add(dataRow);
				i++;
				continue;
				IL_4A1:
				dataRow[2] = 18;
				dataRow[7] = true;
				if (!this.m_connection.m_isDb10gR2OrHigher && flag2 && columnLocalParsePrimaryKeyInfo.m_columnName == "ROWID")
				{
					dataRow[13] = false;
					goto IL_5D7;
				}
				goto IL_5D7;
				IL_526:
				dataRow[2] = int.MaxValue;
				dataRow[19] = true;
				goto IL_5D7;
				IL_5C4:
				dataRow[2] = columnDescribeInfo.m_maxLength;
				goto IL_5D7;
			}
			dataTable.AcceptChanges();
		}

		// Token: 0x060004F8 RID: 1272 RVA: 0x0002C034 File Offset: 0x0002A234
		private void PopulateMetaData(bool tryGetPKInfo = true)
		{
			if (this.m_sqlStatementType == SqlStatementType.SELECT)
			{
				if (!this.m_readerImpl.m_sqlMetaData.bStmtParsed && this.m_readerImpl.m_sqlMetaData.m_noOfColumns > 0)
				{
					SQLParser.GetSchemaMetaData(this.m_readerImpl.m_sqlMetaData, this.m_connection, this.m_connection.m_oracleConnectionImpl, this.m_readerImpl.m_numberOfHiddenColumns > 0);
				}
				if ((this.m_commandBehavior & CommandBehavior.KeyInfo) == CommandBehavior.KeyInfo && !this.m_readerImpl.m_sqlMetaData.bPkFetched && tryGetPKInfo)
				{
					SQLMetaData.GetPrimaryKey(this.m_connection, this.m_readerImpl.m_sqlMetaData, this.m_readerImpl.m_numberOfHiddenColumns, true);
				}
			}
		}

		// Token: 0x060004F9 RID: 1273 RVA: 0x0002C0E4 File Offset: 0x0002A2E4
		public override object GetValue(int i)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			object result;
			try
			{
				if (!this.m_bInternalCall && this.IsDBNull(i))
				{
					result = DBNull.Value;
				}
				else
				{
					int internalRowCounter = this.m_internalRowCounter;
					ColumnDescribeInfo columnDescribeInfo = this.m_readerImpl.m_sqlMetaData.m_columnDescribeInfo[i];
					OraType dataType = (OraType)columnDescribeInfo.m_dataType;
					Accessor accessor = this.m_readerImpl.m_accessors[i];
					object obj = null;
					this.m_bInternalCall = true;
					try
					{
						if (this.m_bUseDataSetAsDupStore)
						{
							if (this.m_currentDataTableForFill == null)
							{
								this.m_currentDataTableIndex++;
								if (this.m_dataTablesReferenceForFill is DataTable)
								{
									this.m_currentDataTableForFill = (DataTable)this.m_dataTablesReferenceForFill;
								}
								else if (this.m_dataTablesReferenceForFill is DataTable[])
								{
									this.m_currentDataTableForFill = ((DataTable[])this.m_dataTablesReferenceForFill)[this.m_currentDataTableIndex];
								}
								else
								{
									this.m_currentDataTableForFill = ((DataTableCollection)this.m_dataTablesReferenceForFill)[((DataTableCollection)this.m_dataTablesReferenceForFill).Count - 1];
								}
								if (this.m_currentDataTableForFill != null)
								{
									this.m_initialRowCnt = this.m_currentDataTableForFill.Rows.Count;
									this.m_isRowAddedToDatatable = false;
								}
							}
							bool flag = false;
							if (this.m_isRowAddedToDatatable)
							{
								flag = this.m_readerImpl.m_dataUnmarshaller.TryGetValueIfDuplicateInDataSet(this.m_internalRowCounter, i, this.m_currentDataTableForFill, columnDescribeInfo.pColAlias, out obj);
							}
							if (flag)
							{
								return obj;
							}
						}
						OraType oraType = dataType;
						if (oraType <= OraType.ORA_CHAR)
						{
							if (oraType <= OraType.ORA_DATE)
							{
								switch (oraType)
								{
								case OraType.ORA_CHARN:
									break;
								case OraType.ORA_NUMBER:
								{
									int scale = (int)columnDescribeInfo.m_scale;
									int precision = (int)columnDescribeInfo.m_precision;
									if (scale <= 0 && precision - scale < 5)
									{
										obj = this.GetInt16(i);
										goto IL_7E6;
									}
									if (scale <= 0 && precision - scale < 10)
									{
										obj = this.GetInt32(i);
										goto IL_7E6;
									}
									if (scale <= 0 && precision - scale < 19)
									{
										obj = this.GetInt64(i);
										goto IL_7E6;
									}
									if (precision < 8 && ((scale <= 0 && precision - scale <= 38) || (scale > 0 && scale <= 44)))
									{
										obj = this.GetFloat(i);
										goto IL_7E6;
									}
									if (precision < 16)
									{
										obj = this.GetDouble(i);
										goto IL_7E6;
									}
									obj = this.GetDecimal(i);
									goto IL_7E6;
								}
								default:
									switch (oraType)
									{
									case OraType.ORA_LONG:
										obj = this.GetString(i);
										goto IL_7E6;
									case OraType.ORA_VARCHAR:
									case (OraType)10:
										goto IL_7DB;
									case OraType.ORA_ROWID:
										break;
									case OraType.ORA_DATE:
										goto IL_342;
									default:
										goto IL_7DB;
									}
									break;
								}
							}
							else
							{
								switch (oraType)
								{
								case OraType.ORA_RAW:
								{
									OraColumnData oraColumnData = null;
									bool flag2 = false;
									if (!this.m_isRowAddedToDatatable)
									{
										flag2 = this.m_readerImpl.m_dataUnmarshaller.TryGetValueIfDuplicate(this.m_internalRowCounter, i, out oraColumnData);
									}
									if (!flag2)
									{
										obj = accessor.GetByteRepresentation(this.m_readerImpl.m_dataUnmarshaller, internalRowCounter, i);
										goto IL_7E6;
									}
									if (oraColumnData.m_rawData != null)
									{
										byte[] array = new byte[oraColumnData.m_rawData.Length];
										Buffer.BlockCopy(oraColumnData.m_rawData, 0, array, 0, oraColumnData.m_rawData.Length);
										obj = array;
										goto IL_7E6;
									}
									if (oraColumnData.m_netTypeData is OracleBinary)
									{
										obj = ((OracleBinary)oraColumnData.m_netTypeData).Value;
										goto IL_7E6;
									}
									goto IL_7E6;
								}
								case OraType.ORA_LONGRAW:
								{
									if (this.m_readerImpl.IsCompleteDataForLongAvailable(this.m_internalRowCounter, i))
									{
										obj = ((TTCLongAccessor)accessor).GetByteRepresentation(this.m_readerImpl.m_dataUnmarshaller, internalRowCounter, i);
										goto IL_7E6;
									}
									byte[] array2 = null;
									this.GetLongRawData(this.m_connection, this.m_internalRowCounter, i, 0L, ref array2, 0, -1, true);
									obj = array2;
									goto IL_7E6;
								}
								default:
									if (oraType != OraType.ORA_CHAR)
									{
										goto IL_7DB;
									}
									break;
								}
							}
						}
						else if (oraType <= OraType.ORA_OCIBFileLocator)
						{
							switch (oraType)
							{
							case OraType.ORA_IBFLOAT:
								obj = this.GetFloat(i);
								goto IL_7E6;
							case OraType.ORA_IBDOUBLE:
								obj = this.GetDouble(i);
								goto IL_7E6;
							default:
								switch (oraType)
								{
								case OraType.ORA_XMLTYPE:
								{
									OracleXmlTypeImpl oracleXmlTypeImpl = new OracleXmlTypeImpl(this.m_connection.m_oracleConnectionImpl, (TTCXmlTypeAccessor)this.m_readerImpl.m_accessors[i], this.m_readerImpl.m_dataUnmarshaller, internalRowCounter, i);
									oracleXmlTypeImpl.Initialize(this.m_connection);
									return oracleXmlTypeImpl.GetString();
								}
								case OraType.ORA_OCIRef:
								case (OraType)111:
									goto IL_7DB;
								case OraType.ORA_OCICLobLocator:
								{
									string result2 = string.Empty;
									byte[] lobLocator = null;
									object obj2 = this.m_LobImplCache[i];
									if (!this.m_fillReader && this.m_LobImplCache != null && obj2 != null && this.m_LastCachedRowNumber[i] == (long)this.m_RowNumber)
									{
										lobLocator = ((OracleClobImpl)obj2).m_lobLocator;
									}
									if (this.m_connection.m_isDb11gR1OrHigher && ((!this.m_fillReader && -1L == this.m_readerImpl.m_clientInitialLOBFS) || (this.m_fillReader && !this.m_returnPSTypes)))
									{
										result2 = OracleClobImpl.GetCompleteClobData(internalRowCounter, this.m_readerImpl.m_dataUnmarshaller, this.m_connection.m_oracleConnectionImpl, lobLocator, (TTCLobAccessor)accessor);
									}
									else
									{
										result2 = OracleClobImpl.GetCompleteClobData(internalRowCounter, i, this.m_connection.m_oracleConnectionImpl, lobLocator, this.m_readerImpl.m_dataUnmarshaller, (TTCLobAccessor)accessor, ref this.m_tempOraClobImpl);
									}
									return result2;
								}
								case OraType.ORA_OCIBLobLocator:
								{
									byte[] completeBlobData;
									if (this.m_connection.m_isDb11gR1OrHigher && ((!this.m_fillReader && -1L == this.m_readerImpl.m_clientInitialLOBFS) || (this.m_fillReader && !this.m_returnPSTypes)))
									{
										completeBlobData = OracleBlobImpl.GetCompleteBlobData(internalRowCounter, (TTCLobAccessor)accessor);
									}
									else
									{
										byte[] lobLocator2 = null;
										object obj3 = this.m_LobImplCache[i];
										if (!this.m_fillReader && this.m_LobImplCache != null && obj3 != null && this.m_LastCachedRowNumber[i] == (long)this.m_RowNumber)
										{
											lobLocator2 = ((OracleBlobImpl)obj3).m_lobLocator;
										}
										completeBlobData = OracleBlobImpl.GetCompleteBlobData(internalRowCounter, i, this.m_connection.m_oracleConnectionImpl, lobLocator2, this.m_readerImpl.m_dataUnmarshaller, (TTCLobAccessor)accessor, ref this.m_tempOraBlobImpl);
									}
									return completeBlobData;
								}
								case OraType.ORA_OCIBFileLocator:
								{
									byte[] lobLocator3 = this.m_readerImpl.GetLobLocator(internalRowCounter, i);
									if (lobLocator3 != null)
									{
										OracleBFileImpl oracleBFileImpl = new OracleBFileImpl(this.m_connection.m_oracleConnectionImpl, lobLocator3);
										oracleBFileImpl.OpenFile();
										byte[] result3;
										try
										{
											long length = oracleBFileImpl.GetLength();
											result3 = new byte[length];
											oracleBFileImpl.Read(1L, length, 0L, ref result3);
										}
										finally
										{
											oracleBFileImpl.CloseFile();
										}
										return result3;
									}
									return DBNull.Value;
								}
								default:
									goto IL_7DB;
								}
								break;
							}
						}
						else
						{
							switch (oraType)
							{
							case OraType.ORA_TIMESTAMP_DTY:
							case OraType.ORA_TIMESTAMP_TZ_DTY:
							case OraType.ORA_TIMESTAMP:
							case OraType.ORA_TIMESTAMP_TZ:
								goto IL_342;
							case OraType.ORA_INTERVAL_YM_DTY:
							case OraType.ORA_INTERVAL_YM:
								obj = this.GetInt64(i);
								goto IL_7E6;
							case OraType.ORA_INTERVAL_DS_DTY:
							case OraType.ORA_INTERVAL_DS:
								obj = this.GetTimeSpan(i);
								goto IL_7E6;
							case (OraType)184:
							case (OraType)185:
							case OraType.ORA_TIME_TZ:
								goto IL_7DB;
							default:
								if (oraType != OraType.ORA_UROWID)
								{
									switch (oraType)
									{
									case OraType.ORA_TIMESTAMP_LTZ_DTY:
									case OraType.ORA_TIMESTAMP_LTZ:
										goto IL_342;
									default:
										goto IL_7DB;
									}
								}
								break;
							}
						}
						obj = this.GetString(i);
						goto IL_7E6;
						IL_342:
						if (this.m_isFromEF && this.m_expectedColumnTypes != null && this.m_expectedColumnTypes[i] == typeof(DateTimeOffset))
						{
							OracleTimeStampTZ oracleTimeStampTZ;
							if (dataType == OraType.ORA_TIMESTAMP_TZ || dataType == OraType.ORA_TIMESTAMP_TZ_DTY)
							{
								oracleTimeStampTZ = this.GetOracleTimeStampTZ(i);
							}
							else if (dataType == OraType.ORA_TIMESTAMP_LTZ || dataType == OraType.ORA_TIMESTAMP_LTZ_DTY)
							{
								oracleTimeStampTZ = this.GetOracleTimeStampLTZ(i).ToOracleTimeStampTZ();
							}
							else if (dataType == OraType.ORA_TIMESTAMP || dataType == OraType.ORA_TIMESTAMP_DTY)
							{
								oracleTimeStampTZ = this.GetOracleTimeStamp(i).ToOracleTimeStampTZ();
							}
							else
							{
								oracleTimeStampTZ = this.GetOracleDate(i).ToOracleTimeStamp().ToOracleTimeStampTZ();
							}
							obj = new DateTimeOffset(oracleTimeStampTZ.Value, oracleTimeStampTZ.GetTimeZoneOffset());
							goto IL_7E6;
						}
						obj = this.GetDateTime(i);
						goto IL_7E6;
						IL_7DB:
						throw new Exception("Unsupported Data Type");
						IL_7E6:;
					}
					finally
					{
						this.m_bInternalCall = false;
					}
					if (this.m_isFromEF && this.m_expectedColumnTypes != null && obj.GetType() != this.m_expectedColumnTypes[i])
					{
						obj = this.ChangeType(obj, this.m_expectedColumnTypes[i]);
					}
					result = obj;
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x060004FA RID: 1274 RVA: 0x0002C9B0 File Offset: 0x0002ABB0
		public override int GetValues(object[] values)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			int result;
			try
			{
				if (this.m_bclosed || this.m_bBeginingOfFile || this.m_bEndOfFile)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.OBJECT_CLOSED, new string[0]));
				}
				if (this.m_internalRowCounter < 0)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.DR_NO_READ_CALLED, new string[0]));
				}
				int num = values.Length;
				int num2 = this.FieldCount;
				if (num < num2)
				{
					num2 = num;
				}
				for (int i = 0; i < num2; i++)
				{
					values[i] = this.GetValue(i);
				}
				result = num2;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x060004FB RID: 1275 RVA: 0x0002CA98 File Offset: 0x0002AC98
		public XmlReader GetXmlReader(int i)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
			}
			if (this.m_bclosed || this.m_bBeginingOfFile || this.m_bEndOfFile)
			{
				throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.OBJECT_CLOSED, new string[0]));
			}
			if (i >= this.m_fieldCount || i < 0)
			{
				throw new IndexOutOfRangeException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.DR_INV_COL_INDEX, new string[0]));
			}
			if (this.m_internalRowCounter < 0)
			{
				throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.DR_NO_READ_CALLED, new string[0]));
			}
			if (OraType.ORA_XMLTYPE != this.m_readerImpl.m_accessors[i].m_internalType)
			{
				throw new InvalidCastException();
			}
			if (this.m_readerImpl.m_accessors[i].IsNullIndicatorSet(this.m_readerImpl.m_dataUnmarshaller, (int)this.m_readerImpl.m_sqlMetaData.m_noOfColumns, this.m_internalRowCounter, i))
			{
				throw new InvalidCastException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.DR_NULL_COL_DATA, new string[0]));
			}
			OracleXmlTypeImpl oracleXmlTypeImpl = new OracleXmlTypeImpl(this.m_connection.m_oracleConnectionImpl, (TTCXmlTypeAccessor)this.m_readerImpl.m_accessors[i], this.m_readerImpl.m_dataUnmarshaller, this.m_internalRowCounter, i);
			oracleXmlTypeImpl.Initialize(this.m_connection);
			return oracleXmlTypeImpl.GetXmlReader(null);
		}

		// Token: 0x060004FC RID: 1276 RVA: 0x0002CBE0 File Offset: 0x0002ADE0
		public override bool IsDBNull(int i)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			bool result;
			try
			{
				if (this.m_bclosed || this.m_bBeginingOfFile || this.m_bEndOfFile)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.OBJECT_CLOSED, new string[0]));
				}
				if (i >= this.m_fieldCount || i < 0)
				{
					throw new IndexOutOfRangeException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.DR_INV_COL_INDEX, new string[0]));
				}
				if (this.m_internalRowCounter < 0)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.DR_NO_READ_CALLED, new string[0]));
				}
				result = this.m_readerImpl.m_accessors[i].IsNullIndicatorSet(this.m_readerImpl.m_dataUnmarshaller, (int)this.m_readerImpl.m_sqlMetaData.m_noOfColumns, this.m_internalRowCounter, i);
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x060004FD RID: 1277 RVA: 0x0002CCF4 File Offset: 0x0002AEF4
		public override bool NextResult()
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			bool flag = false;
			try
			{
				if (this.m_bclosed)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.OBJECT_CLOSED, new string[0]));
				}
				this.m_bDoneReadOne = false;
				this.m_bHasRows = false;
				if (this.m_readerImpl == null)
				{
					return false;
				}
				if ((this.m_commandBehavior & CommandBehavior.SingleResult) != CommandBehavior.SingleResult)
				{
					if (this.m_readerImpl.ConfigureNextResult())
					{
						flag = true;
						this.m_internalRowCounter = -1;
						this.m_RowNumber = 0;
						this.m_LobImplCache = null;
						this.m_LastCachedRowNumber = null;
						this.m_bBeginingOfFile = true;
						this.m_bEndOfFile = false;
						if (this.m_dataTable != null)
						{
							this.m_dataTable.Dispose();
							this.m_dataTable = null;
						}
						if (this.m_dataTableEx != null)
						{
							this.m_dataTableEx.Dispose();
							this.m_dataTableEx = null;
						}
						if (this.m_readerImpl.m_sqlMetaData != null)
						{
							this.m_maxRowSize = this.m_readerImpl.m_sqlMetaData.m_maxRowSize + this.m_readerImpl.m_sqlMetaData.m_numOfLOBColumns * Math.Max(86, 86 + (int)this.m_readerImpl.m_clientInitialLOBFS) + this.m_readerImpl.m_sqlMetaData.m_numOfLONGColumns * Math.Max(2, this.m_initialLongFetchSize) + this.m_readerImpl.m_sqlMetaData.m_numOfBFileColumns * 86;
							this.m_fieldCount = (int)this.m_readerImpl.m_sqlMetaData.m_noOfColumns - this.m_readerImpl.m_numberOfHiddenColumns;
							if (this.m_readerImpl.m_sqlMetaData.HasLOBOrLongColumn)
							{
								this.m_LobImplCache = new object[this.m_fieldCount];
								this.m_LastCachedRowNumber = new long[this.m_fieldCount];
							}
							if (this.m_fillReader)
							{
								DataTable minSchemaTable = this.GetMinSchemaTable();
								if (minSchemaTable != null)
								{
									this.m_dataTableList.Add(minSchemaTable);
								}
							}
						}
						else
						{
							this.m_maxRowSize = (this.m_fieldCount = 0);
						}
						if (this.m_isFromEF)
						{
							this.PopulateExpectedTypes();
						}
					}
					else
					{
						this.m_expectedColumnTypes = null;
						this.m_isFromEF = false;
					}
				}
				else
				{
					this.m_expectedColumnTypes = null;
					this.m_isFromEF = false;
				}
				this.m_currentDataTableForFill = null;
				this.m_noMoreResults = (!flag && !this.m_fillReader);
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
			return flag;
		}

		// Token: 0x060004FE RID: 1278 RVA: 0x0002CF8C File Offset: 0x0002B18C
		public override bool Read()
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				this.m_bBeginingOfFile = false;
				this.m_bDoneReadOne = true;
				if (this.m_bclosed)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.OBJECT_CLOSED, new string[0]));
				}
				if (this.m_bEndOfFile || this.m_noMoreResults)
				{
					return false;
				}
				if (this.m_readerImpl.m_rowsFetched <= 0 && !this.m_readerImpl.m_bHasMoreRowsInDB)
				{
					this.m_readerImpl.ReleaseCursor(this.m_commandText);
					this.m_bEndOfFile = true;
					return false;
				}
				this.m_internalRowCounter++;
				this.m_RowNumber++;
				if ((this.m_commandBehavior & CommandBehavior.SchemaOnly) == CommandBehavior.SchemaOnly || ((this.m_commandBehavior & CommandBehavior.SingleRow) == CommandBehavior.SingleRow && this.m_internalRowCounter > 0))
				{
					if (this.m_readerImpl.m_sqlMetaData != null && this.m_readerImpl.m_sqlMetaData.HasLOBColumns)
					{
						this.ProcessAnyTempLOBs(this.m_internalRowCounter - 1);
					}
					this.m_readerImpl.ReleaseCursor(this.m_commandText);
					this.m_bEndOfFile = true;
					return false;
				}
				if (this.m_internalRowCounter < this.m_readerImpl.m_rowsFetched)
				{
					if (this.m_internalRowCounter > 0 && this.m_readerImpl.m_sqlMetaData != null && this.m_readerImpl.m_sqlMetaData.HasLOBColumns)
					{
						this.ProcessAnyTempLOBs(this.m_internalRowCounter - 1);
					}
					if (this.m_internalRowCounter > 0 && this.m_internalRowCounter < this.m_readerImpl.m_rowsFetched - 1)
					{
						this.m_readerImpl.m_dataUnmarshaller.TryOraBufRelease(this.m_internalRowCounter, this.m_connection.m_oracleConnectionImpl.m_oracleCommunication);
					}
					if (!this.m_isRowAddedToDatatable && this.m_currentDataTableForFill != null && this.m_currentDataTableForFill.Rows.Count > this.m_initialRowCnt)
					{
						this.m_isRowAddedToDatatable = true;
					}
					if (this.m_connection.m_isDb11gR1OrHigher && this.m_internalRowCounter <= 0 && ((this.m_readerImpl.m_sqlMetaData.HasLOBColumns && !this.m_fillReader && -1L == this.m_readerImpl.m_clientInitialLOBFS) || (this.m_fillReader && !this.m_returnPSTypes)))
					{
						if (this.m_readerImpl.m_tempOBList != null)
						{
							for (int i = 0; i < this.m_readerImpl.m_tempOBList.Count; i++)
							{
								this.m_readerImpl.m_tempOBList[i].ReturnToPool();
							}
							this.m_readerImpl.m_tempOBList.Clear();
						}
						this.m_readerImpl.DoLobArrayRead();
					}
					this.m_bEndOfFile = false;
					this.m_bHasRows = true;
					return true;
				}
				if (this.m_readerImpl.m_bHasMoreRowsInDB)
				{
					if (this.m_internalRowCounter > 0 && this.m_readerImpl.m_sqlMetaData != null && this.m_readerImpl.m_sqlMetaData.HasLOBColumns)
					{
						this.ProcessAnyTempLOBs(this.m_internalRowCounter - 1);
					}
					int noOfRowsToFetch = 25;
					if (this.m_maxRowSize > 0)
					{
						noOfRowsToFetch = (int)(this.m_fetchSize / (long)this.m_maxRowSize) + 1;
					}
					if (this.m_readerImpl.m_tempOBList != null)
					{
						for (int j = 0; j < this.m_readerImpl.m_tempOBList.Count; j++)
						{
							this.m_readerImpl.m_tempOBList[j].ReturnToPool();
						}
						this.m_readerImpl.m_tempOBList.Clear();
					}
					bool flag = this.m_readerImpl.FetchMoreRows(noOfRowsToFetch, this.m_fillReader, this.m_returnPSTypes) <= 0;
					this.m_connection.CheckForWarnings(this);
					if (flag)
					{
						this.m_readerImpl.ReleaseCursor(this.m_commandText);
						this.m_bEndOfFile = true;
						return false;
					}
					if (!this.m_isRowAddedToDatatable && this.m_currentDataTableForFill != null && this.m_currentDataTableForFill.Rows.Count > this.m_initialRowCnt)
					{
						this.m_isRowAddedToDatatable = true;
					}
					if (this.m_connection.m_isDb11gR1OrHigher && ((this.m_readerImpl.m_sqlMetaData.HasLOBColumns && !this.m_fillReader && -1L == this.m_readerImpl.m_clientInitialLOBFS) || (this.m_fillReader && !this.m_returnPSTypes)))
					{
						this.m_readerImpl.DoLobArrayRead();
					}
					this.m_internalRowCounter = 0;
					this.m_bEndOfFile = false;
					this.m_bHasRows = true;
					return true;
				}
				else
				{
					if (this.m_internalRowCounter > 0 && this.m_readerImpl.m_sqlMetaData != null && this.m_readerImpl.m_sqlMetaData.HasLOBColumns)
					{
						this.ProcessAnyTempLOBs(this.m_internalRowCounter - 1);
					}
					this.m_readerImpl.ReleaseCursor(this.m_commandText);
					this.m_bEndOfFile = true;
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
			return false;
		}

		// Token: 0x060004FF RID: 1279 RVA: 0x0002D48C File Offset: 0x0002B68C
		internal void ProcessAnyTempLOBs(int rowNumber)
		{
			if (rowNumber >= 0)
			{
				this.m_readerImpl.CollectTempLOBsToBeFreed(rowNumber);
			}
			if (this.m_LobImplCache != null)
			{
				for (int i = 0; i < this.m_fieldCount; i++)
				{
					if (this.m_LobImplCache[i] != null)
					{
						if (this.m_LobImplCache[i] is OracleClobImpl)
						{
							((OracleClobImpl)this.m_LobImplCache[i]).RelRef();
						}
						else if (this.m_LobImplCache[i] is OracleBlobImpl)
						{
							((OracleBlobImpl)this.m_LobImplCache[i]).RelRef();
						}
						this.m_LobImplCache[i] = null;
					}
				}
			}
		}

		// Token: 0x06000500 RID: 1280 RVA: 0x0002D51C File Offset: 0x0002B71C
		private OracleDbType GetOraDbType(int i)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			OracleDbType result;
			try
			{
				if (i >= this.m_fieldCount || i < 0)
				{
					throw new IndexOutOfRangeException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.DR_INV_COL_INDEX, new string[0]));
				}
				ColumnDescribeInfo columnDescribeInfo = this.m_readerImpl.m_sqlMetaData.m_columnDescribeInfo[i];
				OraType dataType = (OraType)columnDescribeInfo.m_dataType;
				OracleDbType oracleDbType = (OracleDbType)OracleTypeMapper.m_OraToOraDb[dataType];
				bool flag = columnDescribeInfo.m_characterSetForm == 2;
				OracleDbType oracleDbType2 = oracleDbType;
				switch (oracleDbType2)
				{
				case OracleDbType.Char:
					if (flag)
					{
						oracleDbType = OracleDbType.NChar;
					}
					break;
				case OracleDbType.Clob:
					if (flag)
					{
						oracleDbType = OracleDbType.NClob;
					}
					break;
				case OracleDbType.Date:
					break;
				case OracleDbType.Decimal:
				{
					int scale = (int)columnDescribeInfo.m_scale;
					int precision = (int)columnDescribeInfo.m_precision;
					if (scale <= 0 && precision - scale < 5)
					{
						oracleDbType = OracleDbType.Int16;
					}
					else if (scale <= 0 && precision - scale < 10)
					{
						oracleDbType = OracleDbType.Int32;
					}
					else if (scale <= 0 && precision - scale < 19)
					{
						oracleDbType = OracleDbType.Int64;
					}
					else if (precision < 8 && ((scale <= 0 && precision - scale <= 38) || (scale > 0 && scale <= 44)))
					{
						oracleDbType = OracleDbType.Single;
					}
					else if (precision < 16)
					{
						oracleDbType = OracleDbType.Double;
					}
					break;
				}
				default:
					if (oracleDbType2 == OracleDbType.Varchar2)
					{
						if (flag)
						{
							oracleDbType = OracleDbType.NVarchar2;
						}
					}
					break;
				}
				result = oracleDbType;
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
			return result;
		}

		// Token: 0x06000501 RID: 1281 RVA: 0x0002D6C4 File Offset: 0x0002B8C4
		private bool IsCorruptible(OraType oraType)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			bool result;
			try
			{
				if (oraType <= OraType.ORA_DATE)
				{
					if (oraType != OraType.ORA_NUMBER && oraType != OraType.ORA_DATE)
					{
						goto IL_52;
					}
				}
				else
				{
					switch (oraType)
					{
					case OraType.ORA_TIMESTAMP:
					case OraType.ORA_TIMESTAMP_TZ:
					case OraType.ORA_INTERVAL_DS:
						break;
					case OraType.ORA_INTERVAL_YM:
						goto IL_52;
					default:
						if (oraType != OraType.ORA_TIMESTAMP_LTZ)
						{
							goto IL_52;
						}
						break;
					}
				}
				return true;
				IL_52:
				result = false;
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
			return result;
		}

		// Token: 0x06000502 RID: 1282 RVA: 0x0002D770 File Offset: 0x0002B970
		internal DataTable GetMinSchemaTable()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			DataTable result;
			try
			{
				if (this.m_readerImpl == null || this.m_readerImpl.m_sqlMetaData == null)
				{
					result = null;
				}
				else
				{
					DataTable dataTable = null;
					if (this.m_sqlStatementType != SqlStatementType.SELECT)
					{
						if (this.m_refCursor != null)
						{
							RefCursorInfo refCursorInfo = this.m_refCursor.m_refCursorInfo;
							if (refCursorInfo != null && refCursorInfo.columnInfo.Rows.Count > 0)
							{
								dataTable = refCursorInfo.columnInfo;
							}
						}
						else
						{
							ConfigBaseClass.StoredProcedureInfo storedProcInfo = ConfigBaseClass.GetInstance(true).GetStoredProcInfo(this.m_storedProcName);
							if (storedProcInfo != null)
							{
								dataTable = storedProcInfo.GetColumnInfo(this.m_readerImpl.m_currentRefCursorIndex);
							}
						}
					}
					DataTable dataTable2 = new DataTable("MinSchemaTable");
					this.PopulateMetaData(false);
					dataTable2.MinimumCapacity = this.m_fieldCount;
					if (this.m_sqlStatementType != SqlStatementType.SELECT)
					{
						dataTable2.ExtendedProperties["REFCursorName"] = ((this.m_readerImpl.m_currentRefCursorIndex == 0) ? "REFCursor" : ("REFCursor" + this.m_readerImpl.m_currentRefCursorIndex));
					}
					dataTable2.Columns.Add("ColumnName", typeof(string));
					dataTable2.Columns.Add("BaseColumnName", typeof(string));
					dataTable2.Columns.Add("BaseTableName", typeof(string));
					dataTable2.Columns.Add("OraDbType", typeof(OracleDbType));
					dataTable2.Columns.Add("BaseSchemaName", typeof(string));
					ColumnLocalParsePrimaryKeyInfo columnLocalParsePrimaryKeyInfo = ColumnLocalParsePrimaryKeyInfo.Null;
					bool flag = this.m_readerImpl.m_sqlMetaData.m_sqlMetaInfo != null && this.m_readerImpl.m_sqlMetaData.m_sqlMetaInfo.m_columnMetaInfo != null;
					for (int i = 0; i < this.m_fieldCount; i++)
					{
						DataRow dataRow = dataTable2.NewRow();
						ColumnDescribeInfo columnDescribeInfo = this.m_readerImpl.m_sqlMetaData.m_columnDescribeInfo[i];
						if (flag)
						{
							columnLocalParsePrimaryKeyInfo = this.m_readerImpl.m_sqlMetaData.m_sqlMetaInfo.m_columnMetaInfo[i];
						}
						dataRow[0] = columnDescribeInfo.pColAlias;
						dataRow[1] = columnLocalParsePrimaryKeyInfo.m_columnName;
						dataRow[2] = columnLocalParsePrimaryKeyInfo.pTabName;
						dataRow[3] = this.GetOraDbType(i);
						dataRow[4] = columnLocalParsePrimaryKeyInfo.m_schemaName;
						if (this.m_sqlStatementType != SqlStatementType.SELECT && dataTable != null)
						{
							object obj = dataTable.Rows[i]["ColumnName"];
							if (obj != null && obj != DBNull.Value)
							{
								dataRow[0] = (string)obj;
							}
							object obj2 = dataTable.Rows[i]["BaseColumnName"];
							if (obj2 != null && obj2 != DBNull.Value)
							{
								dataRow[1] = (string)obj2;
							}
							object obj3 = dataTable.Rows[i]["BaseTableName"];
							if (obj3 != null && obj3 != DBNull.Value)
							{
								dataRow[2] = (string)obj3;
							}
							object obj4 = dataTable.Rows[i]["ProviderType"];
							if (obj4 != null && obj4 != DBNull.Value)
							{
								dataRow[3] = (OracleDbType)obj4;
							}
							object obj5 = dataTable.Rows[i]["BaseSchemaName"];
							if (obj5 != null && obj5 != DBNull.Value)
							{
								dataRow[4] = (string)obj5;
							}
							object obj6 = dataTable.Rows[i]["UdtTypeName"];
							if (obj6 != null && obj6 != DBNull.Value)
							{
								dataRow[5] = obj6;
							}
						}
						dataTable2.Rows.Add(dataRow);
					}
					dataTable2.AcceptChanges();
					result = dataTable2;
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
			return result;
		}

		// Token: 0x06000503 RID: 1283 RVA: 0x0002DBAC File Offset: 0x0002BDAC
		private object ChangeType(object sourceValue, Type targetType)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			object result;
			try
			{
				if (sourceValue is byte[] && targetType == typeof(Guid))
				{
					result = new Guid((byte[])sourceValue);
				}
				else if (sourceValue is TimeSpan && targetType == typeof(decimal))
				{
					result = (decimal)((TimeSpan)sourceValue).TotalSeconds;
				}
				else if (sourceValue is OracleTimeStampTZ && targetType == typeof(DateTimeOffset))
				{
					OracleTimeStampTZ oracleTimeStampTZ = (OracleTimeStampTZ)sourceValue;
					if (oracleTimeStampTZ.IsNull)
					{
						result = DBNull.Value;
					}
					else
					{
						result = new DateTimeOffset(oracleTimeStampTZ.Value, oracleTimeStampTZ.GetTimeZoneOffset());
					}
				}
				else
				{
					result = Convert.ChangeType(sourceValue, targetType, CultureInfo.InvariantCulture);
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
			return result;
		}

		// Token: 0x06000504 RID: 1284 RVA: 0x0002DCDC File Offset: 0x0002BEDC
		internal string GetLongData(OracleConnection connection, int currentRow, int columnIndex, int length)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			string @string;
			try
			{
				DataUnmarshaller dataUnmarshaller = null;
				TTCLongAccessor longAccessorToFetchMoreData = this.GetLongAccessorToFetchMoreData(connection, ref currentRow, ref columnIndex, length, out dataUnmarshaller);
				@string = longAccessorToFetchMoreData.GetString(dataUnmarshaller, currentRow, columnIndex);
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
			return @string;
		}

		// Token: 0x06000505 RID: 1285 RVA: 0x0002DD68 File Offset: 0x0002BF68
		internal long GetLongRawData(OracleConnection connection, int currentRow, int columnIndex, long fieldOffset, ref byte[] buffer, int bufferOffset, int length, bool bAllocateBuffer)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			long result;
			try
			{
				long num = 0L;
				DataUnmarshaller dataUnmarshaller = null;
				TTCLongAccessor longAccessorToFetchMoreData = this.GetLongAccessorToFetchMoreData(connection, ref currentRow, ref columnIndex, length, out dataUnmarshaller);
				if (!bAllocateBuffer && buffer == null)
				{
					num = (long)longAccessorToFetchMoreData.AvailableDataSize(currentRow);
				}
				else
				{
					int num2 = length;
					if (buffer == null)
					{
						num2 = longAccessorToFetchMoreData.AvailableDataSize(currentRow);
						buffer = new byte[num2];
					}
					if (num2 > 0)
					{
						num = longAccessorToFetchMoreData.FillDataInUserBuffer(dataUnmarshaller, currentRow, columnIndex, fieldOffset, buffer, bufferOffset, num2);
					}
				}
				result = num;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x06000506 RID: 1286 RVA: 0x0002DE34 File Offset: 0x0002C034
		private TTCLongAccessor GetLongAccessorToFetchMoreData(OracleConnection connection, ref int currentRow, ref int columnIndex, int length, out DataUnmarshaller dtUnmarshaller)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			TTCLongAccessor result;
			try
			{
				string empty = string.Empty;
				dtUnmarshaller = null;
				string commandText;
				OracleDataReaderImpl oracleDataReaderImpl = this.ExecuteQueryToFetchLongData(connection, currentRow, columnIndex, length, out commandText);
				TTCLongAccessor ttclongAccessor = null;
				if (oracleDataReaderImpl != null && oracleDataReaderImpl.m_accessors != null)
				{
					if (oracleDataReaderImpl.m_accessors[0] is TTCLongAccessor)
					{
						ttclongAccessor = (oracleDataReaderImpl.m_accessors[0] as TTCLongAccessor);
						currentRow = 0;
						columnIndex = 0;
						dtUnmarshaller = oracleDataReaderImpl.m_dataUnmarshaller;
						oracleDataReaderImpl.m_dataUnmarshaller = null;
					}
					oracleDataReaderImpl.ReleaseCursor(commandText);
				}
				else
				{
					dtUnmarshaller = this.m_readerImpl.m_dataUnmarshaller;
					ttclongAccessor = (this.m_readerImpl.m_accessors[columnIndex] as TTCLongAccessor);
				}
				result = ttclongAccessor;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x06000507 RID: 1287 RVA: 0x0002DF2C File Offset: 0x0002C12C
		internal OracleDataReaderImpl ExecuteQueryToFetchLongData(OracleConnection connection, int currentRow, int columnIndex, int length, out string commandText)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			SQLMetaData sqlMetaData = this.m_readerImpl.m_sqlMetaData;
			OracleException ex = null;
			OracleDataReaderImpl result;
			try
			{
				if (this.m_readerImpl.m_sqlMetaData.m_sqlMetaInfo == null || (!sqlMetaData.m_sqlMetaInfo.bPkPresent && !sqlMetaData.m_sqlMetaInfo.bRowidPresent))
				{
					SQLMetaData.GetPrimaryKey(connection, sqlMetaData, this.m_readerImpl.m_numberOfHiddenColumns, false);
				}
				commandText = string.Empty;
				SQLLocalParsePrimaryKeyInfo sqlMetaInfo = sqlMetaData.m_sqlMetaInfo;
				if (sqlMetaInfo == null || (!sqlMetaInfo.bPkPresent && !sqlMetaInfo.bRowidPresent))
				{
					result = null;
				}
				else
				{
					if (sqlMetaInfo.m_tableName == null || sqlMetaInfo.m_tableName.Length == 0)
					{
						throw new OracleException(ResourceStringConstants.INT_ERR, "OracleDatReader", "GetLongData", string.Empty);
					}
					StringBuilder stringBuilder = new StringBuilder("SELECT \"");
					stringBuilder.Append(sqlMetaInfo.m_columnMetaInfo[columnIndex].m_columnName);
					stringBuilder.Append("\" from \"");
					if (sqlMetaInfo.m_schemaName != null && sqlMetaInfo.m_schemaName.Length != 0)
					{
						stringBuilder.Append(sqlMetaInfo.m_schemaName).Append("\".\"");
					}
					stringBuilder.Append(sqlMetaInfo.m_tableName).Append("\" where ");
					int num = 0;
					OracleParameterCollection oracleParameterCollection = new OracleParameterCollection();
					int num2 = sqlMetaInfo.m_columnMetaInfo.Length;
					for (int i = 0; i < num2; i++)
					{
						if (sqlMetaInfo.m_columnMetaInfo[i].bIsKeyColumn)
						{
							num++;
							if (num == 1)
							{
								stringBuilder.Append(" \"");
							}
							else
							{
								stringBuilder.Append(" and \"");
							}
							stringBuilder.Append(sqlMetaInfo.m_columnMetaInfo[i].m_columnName);
							if (this.m_readerImpl.m_accessors[i].IsNullIndicatorSet(this.m_readerImpl.m_dataUnmarshaller, (int)this.m_readerImpl.m_sqlMetaData.m_noOfColumns, currentRow, i))
							{
								stringBuilder.Append("\" IS NULL");
							}
							else
							{
								this.m_bInternalCall = true;
								object value = this.GetValue(i);
								this.m_bInternalCall = false;
								stringBuilder.Append("\" = :").Append(num.ToString());
								OracleDbType dbType = (OracleDbType)OracleTypeMapper.m_OraToOraDb[(OraType)sqlMetaData.m_columnDescribeInfo[i].m_dataType];
								oracleParameterCollection.Add(num.ToString(), dbType, value, ParameterDirection.Input);
							}
						}
					}
					commandText = stringBuilder.ToString();
					OracleDataReaderImpl oracleDataReaderImpl = null;
					OracleCommandImpl oracleCommandImpl = new OracleCommandImpl();
					bool flag = false;
					long num3 = 0L;
					OracleLogicalTransaction oracleLogicalTransaction = null;
					long[] array;
					OracleParameterCollection oracleParameterCollection2;
					oracleCommandImpl.ExecuteReader(commandText, oracleParameterCollection, CommandType.Text, this.m_connection.m_oracleConnectionImpl, ref oracleDataReaderImpl, -1, 0L, null, this.m_readerImpl.m_snapshotSCN, out array, out oracleParameterCollection2, ref flag, ref num3, out ex, this.m_connection, ref oracleLogicalTransaction, null, false, false);
					if (oracleDataReaderImpl != null && oracleDataReaderImpl.m_rowsFetched <= 0 && oracleDataReaderImpl.m_bHasMoreRowsInDB)
					{
						oracleDataReaderImpl.FetchMoreRows(1, this.m_fillReader, this.m_returnPSTypes);
						this.m_connection.CheckForWarnings(this);
					}
					result = oracleDataReaderImpl;
				}
			}
			catch (Exception ex2)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex2, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x06000508 RID: 1288 RVA: 0x0002E284 File Offset: 0x0002C484
		internal byte[] GetOracleLobForUpdate(int i, int wait)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			byte[] array = null;
			OracleCommand oracleCommand = null;
			OracleDataReader oracleDataReader = null;
			byte[] result;
			try
			{
				if (this.m_bclosed || this.m_bBeginingOfFile || this.m_bEndOfFile)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.OBJECT_CLOSED, new string[0]));
				}
				if (i >= this.m_fieldCount || i < 0)
				{
					throw new IndexOutOfRangeException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.DR_INV_COL_INDEX, new string[0]));
				}
				if (this.m_internalRowCounter < 0)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.DR_NO_READ_CALLED, new string[0]));
				}
				if (-1L == this.m_readerImpl.m_clientInitialLOBFS && ConfigBaseClass.m_bLegacyNegativeOneILFSBehavior)
				{
					throw new InvalidCastException();
				}
				oracleCommand = this.m_connection.CreateCommand();
				if (this.m_readerImpl.m_sqlMetaData.m_sqlMetaInfo == null || (!this.m_readerImpl.m_sqlMetaData.m_sqlMetaInfo.bPkPresent && !this.m_readerImpl.m_sqlMetaData.m_sqlMetaInfo.bRowidPresent))
				{
					SQLMetaData.GetPrimaryKey(this.m_connection, this.m_readerImpl.m_sqlMetaData, this.m_readerImpl.m_numberOfHiddenColumns, false);
				}
				SQLLocalParsePrimaryKeyInfo sqlMetaInfo = this.m_readerImpl.m_sqlMetaData.m_sqlMetaInfo;
				if (sqlMetaInfo == null || (!sqlMetaInfo.bPkPresent && !sqlMetaInfo.bRowidPresent))
				{
					throw new OracleException(ResourceStringConstants.DAC_PK_REQUIRED, string.Empty, string.Empty, string.Empty);
				}
				int num = 0;
				int num2 = sqlMetaInfo.m_columnMetaInfo.Length;
				StringBuilder stringBuilder = new StringBuilder("SELECT \"", 512);
				stringBuilder.Append(sqlMetaInfo.m_columnMetaInfo[i].m_columnName).Append("\" FROM \"");
				stringBuilder.Append((sqlMetaInfo.m_schemaName == null) ? string.Empty : sqlMetaInfo.m_schemaName);
				stringBuilder.Append((sqlMetaInfo.m_schemaName == null) ? string.Empty : "\".\"");
				stringBuilder.Append(sqlMetaInfo.m_tableName).Append("\" WHERE ");
				for (int j = 0; j < num2; j++)
				{
					if (sqlMetaInfo.m_columnMetaInfo[j].bIsKeyColumn)
					{
						num++;
						if (num == 1)
						{
							stringBuilder.Append("\"");
						}
						else
						{
							stringBuilder.Append(" AND \"");
						}
						stringBuilder.Append(sqlMetaInfo.m_columnMetaInfo[j].m_columnName);
						this.m_bInternalCall = true;
						object value = this.GetValue(j);
						this.m_bInternalCall = false;
						stringBuilder.Append("\" = :").Append(num.ToString());
						OracleDbType dbType = (OracleDbType)OracleTypeMapper.m_OraToOraDb[(OraType)this.m_readerImpl.m_sqlMetaData.m_columnDescribeInfo[j].m_dataType];
						oracleCommand.Parameters.Add(num.ToString(), dbType, value, ParameterDirection.Input);
					}
				}
				stringBuilder.Append(" FOR UPDATE");
				switch (wait)
				{
				case -1:
					break;
				case 0:
					stringBuilder.Append(" NOWAIT");
					break;
				default:
					stringBuilder.Append(" WAIT ").Append(wait.ToString());
					break;
				}
				oracleCommand.CommandText = stringBuilder.ToString();
				oracleDataReader = oracleCommand.ExecuteReader();
				if (!oracleDataReader.Read())
				{
					throw new OracleException(ResourceStringConstants.DR_NO_READ_CALLED, string.Empty, string.Empty, string.Empty);
				}
				if (!oracleDataReader.IsDBNull(0))
				{
					array = oracleDataReader.m_readerImpl.GetLobLocator(0, 0);
				}
				result = array;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				if (oracleCommand != null)
				{
					foreach (object obj in oracleCommand.Parameters)
					{
						OracleParameter oracleParameter = (OracleParameter)obj;
						oracleParameter.Dispose();
					}
					oracleCommand.Dispose();
				}
				if (oracleDataReader != null)
				{
					oracleDataReader.Dispose();
				}
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x06000509 RID: 1289 RVA: 0x0002E6C8 File Offset: 0x0002C8C8
		internal void GetEdmMappingConfigValues()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			int maxPrecision;
			if ((maxPrecision = ConfigBaseClass.GetMaxPrecision("BOOL", false)) > 0)
			{
				this.m_edmMappingMaxBOOL = maxPrecision;
				this.m_bMapNumberToBoolean = true;
			}
			int maxPrecision2;
			if ((maxPrecision2 = ConfigBaseClass.GetMaxPrecision("BYTE", false)) > 0)
			{
				this.m_edmMappingMaxBYTE = maxPrecision2;
				this.m_bMapNumberToByte = true;
			}
			int maxPrecision3;
			if ((maxPrecision3 = ConfigBaseClass.GetMaxPrecision("INT16", false)) > 0)
			{
				this.m_edmMappingMaxINT16 = maxPrecision3;
			}
			int maxPrecision4;
			if ((maxPrecision4 = ConfigBaseClass.GetMaxPrecision("INT32", false)) > 0)
			{
				this.m_edmMappingMaxINT32 = maxPrecision4;
			}
			int maxPrecision5;
			if ((maxPrecision5 = ConfigBaseClass.GetMaxPrecision("INT64", false)) > 0)
			{
				this.m_edmMappingMaxINT64 = maxPrecision5;
			}
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Exit, new string[0]);
			}
		}

		// Token: 0x0600050A RID: 1290 RVA: 0x0002E794 File Offset: 0x0002C994
		internal void PopulateExpectedTypes()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			if (this.m_fieldCount > 0)
			{
				this.m_expectedColumnTypes = new Type[this.m_fieldCount];
				for (int i = 0; i < this.m_fieldCount; i++)
				{
					OraType dataType = (OraType)this.m_readerImpl.m_sqlMetaData.m_columnDescribeInfo[i].m_dataType;
					if (dataType != OraType.ORA_NUMBER)
					{
						this.m_expectedColumnTypes[i] = (Type)OracleTypeMapper.m_OraToNET[dataType];
					}
					else
					{
						int precision = (int)this.m_readerImpl.m_sqlMetaData.m_columnDescribeInfo[i].m_precision;
						int scale = (int)this.m_readerImpl.m_sqlMetaData.m_columnDescribeInfo[i].m_scale;
						if (precision == 1 && scale == 0)
						{
							if (this.m_bMapNumberToBoolean && precision <= this.m_edmMappingMaxBOOL)
							{
								this.m_expectedColumnTypes[i] = Type.GetType("System.Boolean");
							}
							else if (this.m_bMapNumberToByte && precision <= this.m_edmMappingMaxBYTE)
							{
								this.m_expectedColumnTypes[i] = Type.GetType("System.Byte");
							}
							else
							{
								this.m_expectedColumnTypes[i] = Type.GetType("System.Int16");
							}
						}
						else if (this.m_bMapNumberToByte && scale == 0 && precision <= this.m_edmMappingMaxBYTE)
						{
							this.m_expectedColumnTypes[i] = Type.GetType("System.Byte");
						}
						else if (scale == 0 && precision <= this.m_edmMappingMaxINT16)
						{
							this.m_expectedColumnTypes[i] = Type.GetType("System.Int16");
						}
						else if (scale == 0 && precision <= this.m_edmMappingMaxINT32)
						{
							this.m_expectedColumnTypes[i] = Type.GetType("System.Int32");
						}
						else if (scale == 0 && precision <= this.m_edmMappingMaxINT64)
						{
							this.m_expectedColumnTypes[i] = Type.GetType("System.Int64");
						}
						else
						{
							this.m_expectedColumnTypes[i] = Type.GetType("System.Decimal");
						}
					}
				}
			}
			else
			{
				this.m_expectedColumnTypes = null;
			}
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Exit, new string[0]);
			}
		}

		// Token: 0x040005E9 RID: 1513
		private static string s_replaceString = "\r\n";

		// Token: 0x040005EA RID: 1514
		private long m_fetchSize;

		// Token: 0x040005EB RID: 1515
		private int m_maxRowSize;

		// Token: 0x040005EC RID: 1516
		private int m_initialLongFetchSize;

		// Token: 0x040005ED RID: 1517
		private CommandBehavior m_commandBehavior;

		// Token: 0x040005EE RID: 1518
		private bool m_bclosed;

		// Token: 0x040005EF RID: 1519
		private bool m_bDisposed;

		// Token: 0x040005F0 RID: 1520
		internal OracleRefCursor m_refCursor;

		// Token: 0x040005F1 RID: 1521
		private int m_recordsAffected;

		// Token: 0x040005F2 RID: 1522
		private bool m_bBeginingOfFile = true;

		// Token: 0x040005F3 RID: 1523
		internal bool m_bEndOfFile;

		// Token: 0x040005F4 RID: 1524
		private int m_fieldCount;

		// Token: 0x040005F5 RID: 1525
		internal OracleDataReaderImpl m_readerImpl;

		// Token: 0x040005F6 RID: 1526
		private OracleConnection m_connection;

		// Token: 0x040005F7 RID: 1527
		private bool m_bDoneReadOne;

		// Token: 0x040005F8 RID: 1528
		private bool m_bHasRows;

		// Token: 0x040005F9 RID: 1529
		internal int m_internalRowCounter = -1;

		// Token: 0x040005FA RID: 1530
		private string m_commandText;

		// Token: 0x040005FB RID: 1531
		internal bool m_returnPSTypes;

		// Token: 0x040005FC RID: 1532
		internal string m_storedProcName;

		// Token: 0x040005FD RID: 1533
		private DataTable m_dataTableEx;

		// Token: 0x040005FE RID: 1534
		private DataTable m_dataTable;

		// Token: 0x040005FF RID: 1535
		private SqlStatementType m_sqlStatementType;

		// Token: 0x04000600 RID: 1536
		private bool m_noMoreResults;

		// Token: 0x04000601 RID: 1537
		private bool m_fillReader;

		// Token: 0x04000602 RID: 1538
		private ArrayList m_dataTableList;

		// Token: 0x04000603 RID: 1539
		private bool m_bInternalCall;

		// Token: 0x04000604 RID: 1540
		internal Type[] m_expectedColumnTypes;

		// Token: 0x04000605 RID: 1541
		internal bool m_isFromEF;

		// Token: 0x04000606 RID: 1542
		internal bool m_bMapNumberToBoolean;

		// Token: 0x04000607 RID: 1543
		internal int m_edmMappingMaxBOOL = 1;

		// Token: 0x04000608 RID: 1544
		internal bool m_bMapNumberToByte;

		// Token: 0x04000609 RID: 1545
		internal int m_edmMappingMaxBYTE = 3;

		// Token: 0x0400060A RID: 1546
		internal int m_edmMappingMaxINT16 = 5;

		// Token: 0x0400060B RID: 1547
		internal int m_edmMappingMaxINT32 = 10;

		// Token: 0x0400060C RID: 1548
		internal int m_edmMappingMaxINT64 = 19;

		// Token: 0x0400060D RID: 1549
		private object[] m_LobImplCache;

		// Token: 0x0400060E RID: 1550
		private int m_RowNumber;

		// Token: 0x0400060F RID: 1551
		private long[] m_LastCachedRowNumber;

		// Token: 0x04000610 RID: 1552
		private TimeSpan m_LocalTimeAdjustment = TimeSpan.Zero;

		// Token: 0x04000611 RID: 1553
		internal OracleBlobImpl m_tempOraBlobImpl;

		// Token: 0x04000612 RID: 1554
		internal OracleClobImpl m_tempOraClobImpl;

		// Token: 0x04000613 RID: 1555
		internal object m_dataTablesReferenceForFill;

		// Token: 0x04000614 RID: 1556
		private int m_currentDataTableIndex = -1;

		// Token: 0x04000615 RID: 1557
		private DataTable m_currentDataTableForFill;

		// Token: 0x04000616 RID: 1558
		internal bool m_bUseDataSetAsDupStore;

		// Token: 0x04000617 RID: 1559
		private int m_initialRowCnt = -1;

		// Token: 0x04000618 RID: 1560
		private bool m_isRowAddedToDatatable;

		// Token: 0x04000619 RID: 1561
		internal int m_numExplicitBoundRefCursors;

		// Token: 0x0400061A RID: 1562
		private object lockDataReader = new object();
	}
}
