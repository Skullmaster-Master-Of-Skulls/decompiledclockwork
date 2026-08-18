using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Runtime.InteropServices;
using System.Timers;
using Oracle.DataAccess.Types;

namespace Oracle.DataAccess.Client
{
	// Token: 0x02000062 RID: 98
	public sealed class OracleBulkCopy : IDisposable
	{
		// Token: 0x06000487 RID: 1159 RVA: 0x000339F8 File Offset: 0x000329F8
		~OracleBulkCopy()
		{
			this.Dispose(false);
		}

		// Token: 0x06000488 RID: 1160 RVA: 0x00033A28 File Offset: 0x00032A28
		static OracleBulkCopy()
		{
			if (!OracleInit.bSetDllDirectoryInvoked)
			{
				OracleInit.Initialize();
			}
		}

		// Token: 0x06000489 RID: 1161 RVA: 0x00033A44 File Offset: 0x00032A44
		public OracleBulkCopy(OracleConnection connection)
		{
			if (connection == null)
			{
				throw new ArgumentNullException("connection");
			}
			if (connection.State != ConnectionState.Open)
			{
				throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_CLOSED, new string[0]));
			}
			this.m_connection = connection;
			this.m_conSignature = connection.m_conSignature;
			this.m_timeout = 30;
			this.m_bulkCopyOptions = OracleBulkCopyOptions.Default;
		}

		// Token: 0x0600048A RID: 1162 RVA: 0x00033ACC File Offset: 0x00032ACC
		public OracleBulkCopy(string connectionString)
		{
			if (connectionString == null)
			{
				throw new ArgumentNullException("connectionString");
			}
			if (connectionString == string.Empty)
			{
				throw new ArgumentException(OpoErrResManager.GetErrorMesg(ErrRes.ODP_INVALID_VALUE, new string[]
				{
					"connectionString"
				}));
			}
			this.m_connection = new OracleConnection(connectionString);
			this.m_ownConnection = true;
			this.m_timeout = 30;
			this.m_bulkCopyOptions = OracleBulkCopyOptions.Default;
		}

		// Token: 0x0600048B RID: 1163 RVA: 0x00033B60 File Offset: 0x00032B60
		public OracleBulkCopy(OracleConnection connection, OracleBulkCopyOptions copyOptions) : this(connection)
		{
			this.m_bulkCopyOptions = copyOptions;
		}

		// Token: 0x0600048C RID: 1164 RVA: 0x00033B70 File Offset: 0x00032B70
		public OracleBulkCopy(string connectionString, OracleBulkCopyOptions copyOptions) : this(connectionString)
		{
			this.m_bulkCopyOptions = copyOptions;
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x00033B80 File Offset: 0x00032B80
		public void Close()
		{
			if (this.m_insideRowsCopiedEvent)
			{
				throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.BC_INV_OPER_INSIDE_EVENT, new string[0]));
			}
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x00033BAD File Offset: 0x00032BAD
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600048F RID: 1167 RVA: 0x00033BBC File Offset: 0x00032BBC
		public void WriteToServer(DataRow[] rows)
		{
			if (this.m_disposed)
			{
				throw new ObjectDisposedException(base.GetType().Name);
			}
			if (rows == null)
			{
				throw new ArgumentNullException("rows");
			}
			if (this.m_destinationTableName == null)
			{
				throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.ODP_INVALID_VALUE, new string[]
				{
					"Destination table"
				}));
			}
			if (rows.Length != 0)
			{
				DataTable table = rows[0].Table;
				this.m_dataSource = table;
				this.m_sourceType = OracleBulkCopy.SourceType.RowArray;
				this.m_rowEnumerator = table.Rows.GetEnumerator();
				this.m_srcColumnCount = table.Columns.Count;
				this.WriteDataSourceToServer();
			}
		}

		// Token: 0x06000490 RID: 1168 RVA: 0x00033C5C File Offset: 0x00032C5C
		public void WriteToServer(DataTable table)
		{
			this.WriteToServer(table, (DataRowState)0);
		}

		// Token: 0x06000491 RID: 1169 RVA: 0x00033C68 File Offset: 0x00032C68
		public void WriteToServer(IDataReader reader)
		{
			if (this.m_disposed)
			{
				throw new ObjectDisposedException(base.GetType().Name);
			}
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			if (this.m_destinationTableName == null)
			{
				throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.ODP_INVALID_VALUE, new string[]
				{
					"Destination table"
				}));
			}
			this.m_dataSource = reader;
			this.m_sourceType = OracleBulkCopy.SourceType.IDataReader;
			this.m_srcColumnCount = reader.FieldCount;
			this.WriteDataSourceToServer();
		}

		// Token: 0x06000492 RID: 1170 RVA: 0x00033CE4 File Offset: 0x00032CE4
		public void WriteToServer(DataTable table, DataRowState rowState)
		{
			if (this.m_disposed)
			{
				throw new ObjectDisposedException(base.GetType().Name);
			}
			if (table == null)
			{
				throw new ArgumentNullException("table");
			}
			if (this.m_destinationTableName == null)
			{
				throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.ODP_INVALID_VALUE, new string[]
				{
					"Destination table"
				}));
			}
			this.m_rowState = (rowState & ~DataRowState.Deleted);
			this.m_dataSource = table;
			this.m_sourceType = OracleBulkCopy.SourceType.DataTable;
			this.m_rowEnumerator = table.Rows.GetEnumerator();
			this.m_srcColumnCount = table.Columns.Count;
			this.WriteDataSourceToServer();
		}

		// Token: 0x06000493 RID: 1171 RVA: 0x00033D80 File Offset: 0x00032D80
		public void WriteToServer(OracleRefCursor refCursor)
		{
			this.WriteToServer(refCursor.GetDataReader());
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x06000494 RID: 1172 RVA: 0x00033D8E File Offset: 0x00032D8E
		// (set) Token: 0x06000495 RID: 1173 RVA: 0x00033D96 File Offset: 0x00032D96
		[DefaultValue(0)]
		public int BatchSize
		{
			get
			{
				return this.m_batchSize;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("BatchSize");
				}
				this.m_batchSize = value;
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06000496 RID: 1174 RVA: 0x00033DAE File Offset: 0x00032DAE
		// (set) Token: 0x06000497 RID: 1175 RVA: 0x00033DB6 File Offset: 0x00032DB6
		[DefaultValue(OracleBulkCopyOptions.Default)]
		public OracleBulkCopyOptions BulkCopyOptions
		{
			get
			{
				return this.m_bulkCopyOptions;
			}
			set
			{
				this.m_bulkCopyOptions = value;
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000498 RID: 1176 RVA: 0x00033DBF File Offset: 0x00032DBF
		// (set) Token: 0x06000499 RID: 1177 RVA: 0x00033DC7 File Offset: 0x00032DC7
		[DefaultValue(30)]
		public int BulkCopyTimeout
		{
			get
			{
				return this.m_timeout;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("BulkCopyTimeout");
				}
				this.m_timeout = value;
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x0600049A RID: 1178 RVA: 0x00033DDF File Offset: 0x00032DDF
		public OracleBulkCopyColumnMappingCollection ColumnMappings
		{
			get
			{
				if (this.m_columnMappings == null)
				{
					this.m_columnMappings = new OracleBulkCopyColumnMappingCollection();
				}
				return this.m_columnMappings;
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x0600049B RID: 1179 RVA: 0x00033DFA File Offset: 0x00032DFA
		public OracleConnection Connection
		{
			get
			{
				return this.m_connection;
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x0600049C RID: 1180 RVA: 0x00033E02 File Offset: 0x00032E02
		// (set) Token: 0x0600049D RID: 1181 RVA: 0x00033E18 File Offset: 0x00032E18
		public string DestinationTableName
		{
			get
			{
				if (this.m_destinationTableName == null)
				{
					return string.Empty;
				}
				return this.m_destinationTableName;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("DestinationTableName");
				}
				if (value.Length == 0)
				{
					throw new ArgumentException("DestinationTableName");
				}
				if (this.m_destinationTableName != value)
				{
					this.m_fetchMeta = true;
					this.m_destinationTableName = value;
				}
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x0600049E RID: 1182 RVA: 0x00033E57 File Offset: 0x00032E57
		// (set) Token: 0x0600049F RID: 1183 RVA: 0x00033E6D File Offset: 0x00032E6D
		public string DestinationPartitionName
		{
			get
			{
				if (this.m_destinationPartitionName == null)
				{
					return string.Empty;
				}
				return this.m_destinationPartitionName;
			}
			set
			{
				this.m_destinationPartitionName = value;
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x060004A0 RID: 1184 RVA: 0x00033E76 File Offset: 0x00032E76
		// (set) Token: 0x060004A1 RID: 1185 RVA: 0x00033E7E File Offset: 0x00032E7E
		[DefaultValue(0)]
		public int NotifyAfter
		{
			get
			{
				return this.m_notifyAfter;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("NotifyAfter");
				}
				this.m_notifyAfter = value;
			}
		}

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x060004A2 RID: 1186 RVA: 0x00033E96 File Offset: 0x00032E96
		// (remove) Token: 0x060004A3 RID: 1187 RVA: 0x00033EAF File Offset: 0x00032EAF
		public event OracleRowsCopiedEventHandler OracleRowsCopied
		{
			add
			{
				this.m_rowsCopiedEventHandler = (OracleRowsCopiedEventHandler)Delegate.Combine(this.m_rowsCopiedEventHandler, value);
			}
			remove
			{
				this.m_rowsCopiedEventHandler = (OracleRowsCopiedEventHandler)Delegate.Remove(this.m_rowsCopiedEventHandler, value);
			}
		}

		// Token: 0x060004A4 RID: 1188 RVA: 0x00033EC8 File Offset: 0x00032EC8
		private void Dispose(bool disposing)
		{
			if (!this.m_disposed)
			{
				if (disposing)
				{
					this.m_columnMappings = null;
					if (this.m_internalTransaction != null)
					{
						this.m_internalTransaction.Dispose();
						this.m_internalTransaction = null;
					}
					if (this.m_connection != null)
					{
						if (this.m_ownConnection)
						{
							this.m_connection.Dispose();
						}
						this.m_connection = null;
					}
				}
				if (this.m_pBlkColCtx != null)
				{
					try
					{
						OpsBC.FreeColCtx(this.m_pBlkColCtx, this.m_dstColumnCount);
					}
					catch (Exception ex)
					{
						if (OraTrace.m_TraceLevel != 0U)
						{
							OraTrace.TraceExceptionInfo(ex);
						}
						throw;
					}
				}
				if (this.m_pOpoBulkCopyValCtx != null)
				{
					try
					{
						OpsBC.FreeValCtx(this.m_pOpoBulkCopyValCtx);
					}
					catch (Exception ex2)
					{
						if (OraTrace.m_TraceLevel != 0U)
						{
							OraTrace.TraceExceptionInfo(ex2);
						}
						throw;
					}
				}
				if (this.m_opsErrCtx != IntPtr.Zero)
				{
					try
					{
						OpsErr.FreeCtx(ref this.m_opsErrCtx);
					}
					catch (Exception ex3)
					{
						if (OraTrace.m_TraceLevel != 0U)
						{
							OraTrace.TraceExceptionInfo(ex3);
						}
					}
					this.m_opsErrCtx = IntPtr.Zero;
				}
				if (this.m_opsConCtx != IntPtr.Zero)
				{
					try
					{
						OpsCon.RelRef(ref this.m_opsConCtx);
					}
					catch (Exception ex4)
					{
						if (OraTrace.m_TraceLevel != 0U)
						{
							OraTrace.TraceExceptionInfo(ex4);
						}
					}
					this.m_opsConCtx = IntPtr.Zero;
				}
				this.m_disposed = true;
			}
		}

		// Token: 0x060004A5 RID: 1189 RVA: 0x0003402C File Offset: 0x0003302C
		private unsafe void Abort()
		{
			int num = 0;
			this.m_internalTransaction = null;
			try
			{
				num = OpsBC.Abort(this.m_pOpoBulkCopyValCtx->pOPOBulkCopyCtx, this.m_opsErrCtx);
			}
			catch (Exception ex)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex);
				}
				throw;
			}
			finally
			{
				if (num != 0)
				{
					OracleException.HandleError(num, this.m_connection, this.m_opsErrCtx, this);
				}
			}
			try
			{
				num = OpsBC.FreeInputBuffer(this.m_pOpoBulkCopyValCtx);
			}
			catch (Exception ex2)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex2);
				}
				throw;
			}
			finally
			{
				if (num != 0)
				{
					OracleException.HandleError(num, this.m_connection, this.m_opsErrCtx, this);
				}
			}
			try
			{
				num = OpsBC.Cleanup(this.m_pOpoBulkCopyValCtx);
			}
			catch (Exception ex3)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex3);
				}
				throw;
			}
			finally
			{
				if (num != 0)
				{
					OracleException.HandleError(num, this.m_connection, this.m_opsErrCtx, this);
				}
			}
		}

		// Token: 0x060004A6 RID: 1190 RVA: 0x00034140 File Offset: 0x00033140
		private unsafe void WriteDataSourceToServer()
		{
			bool flag = false;
			int num = 0;
			OPOBulkCopyColCtx* ptr = null;
			OPOBulkCopyColRefCtx opobulkCopyColRefCtx = new OPOBulkCopyColRefCtx();
			this.ValidateConnection();
			if (this.m_fetchMeta)
			{
				try
				{
					if (this.m_pBlkColCtx != null)
					{
						num = OpsBC.FreeColCtx(this.m_pBlkColCtx, this.m_dstColumnCount);
					}
				}
				catch (Exception ex)
				{
					if (OraTrace.m_TraceLevel != 0U)
					{
						OraTrace.TraceExceptionInfo(ex);
					}
					throw;
				}
				finally
				{
					if (num != 0)
					{
						OracleException.HandleError(num, this.m_connection, this.m_opsErrCtx, this);
					}
				}
				try
				{
					if (this.m_pOpoBulkCopyValCtx != null)
					{
						num = OpsBC.FreeValCtx(this.m_pOpoBulkCopyValCtx);
						this.m_pOpoBulkCopyValCtx = null;
					}
				}
				catch (Exception ex2)
				{
					if (OraTrace.m_TraceLevel != 0U)
					{
						OraTrace.TraceExceptionInfo(ex2);
					}
					throw;
				}
				finally
				{
					if (num != 0)
					{
						OracleException.HandleError(num, this.m_connection, this.m_opsErrCtx, this);
					}
				}
				try
				{
					num = OpsBC.AllocValCtx(ref this.m_pOpoBulkCopyValCtx);
				}
				catch (Exception ex3)
				{
					if (OraTrace.m_TraceLevel != 0U)
					{
						OraTrace.TraceExceptionInfo(ex3);
					}
					throw;
				}
				finally
				{
					if (num != 0)
					{
						OracleException.HandleError(num, this.m_connection, this.m_opsErrCtx, this);
					}
				}
				this.GetMetaData();
				this.m_fetchMeta = false;
			}
			else
			{
				this.m_pOpoBulkCopyValCtx->MaxRowsInBuffer = 0;
				this.m_pOpoBulkCopyValCtx->NoOfRows = 0;
				this.m_pOpoBulkCopyValCtx->RowsInColArr = 0;
			}
			bool flag2 = false;
			if (this.m_internalColumnMappings == null)
			{
				this.m_internalColumnMappings = new OracleBulkCopyColumnMappingCollection();
			}
			else
			{
				this.m_internalColumnMappings.Clear();
			}
			if (this.m_columnMappings != null)
			{
				foreach (object obj in this.m_columnMappings)
				{
					OracleBulkCopyColumnMapping oracleBulkCopyColumnMapping = (OracleBulkCopyColumnMapping)obj;
					this.m_internalColumnMappings.Add(oracleBulkCopyColumnMapping.Clone());
				}
			}
			if (this.m_internalColumnMappings.Count > 0)
			{
				new ArrayList();
				this.m_internalColumnMappings.ValidateCollection();
				foreach (object obj2 in this.m_internalColumnMappings)
				{
					OracleBulkCopyColumnMapping oracleBulkCopyColumnMapping2 = (OracleBulkCopyColumnMapping)obj2;
					if (oracleBulkCopyColumnMapping2.SourceOrdinal == -1)
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					goto IL_328;
				}
				flag = false;
				using (IEnumerator enumerator3 = this.m_internalColumnMappings.GetEnumerator())
				{
					while (enumerator3.MoveNext())
					{
						object obj3 = enumerator3.Current;
						OracleBulkCopyColumnMapping oracleBulkCopyColumnMapping3 = (OracleBulkCopyColumnMapping)obj3;
						if (oracleBulkCopyColumnMapping3.SourceOrdinal == -1)
						{
							int num2;
							if (this.m_sourceType == OracleBulkCopy.SourceType.DataTable || this.m_sourceType == OracleBulkCopy.SourceType.RowArray)
							{
								num2 = ((DataTable)this.m_dataSource).Columns.IndexOf(oracleBulkCopyColumnMapping3.SourceColumn);
							}
							else
							{
								num2 = ((IDataReader)this.m_dataSource).GetOrdinal(oracleBulkCopyColumnMapping3.SourceColumn);
							}
							if (num2 == -1)
							{
								throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.ODP_INVALID_VALUE, new string[]
								{
									"Column mapping"
								}));
							}
							oracleBulkCopyColumnMapping3.SourceOrdinal = num2;
						}
					}
					goto IL_328;
				}
			}
			if (this.m_srcColumnCount > (int)this.m_pOpoBulkCopyValCtx->NoOfCols)
			{
				throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.ODP_INVALID_VALUE, new string[]
				{
					"Column mapping"
				}));
			}
			this.m_internalColumnMappings.CreateDefaultColumnMapping(this.m_srcColumnCount);
			flag2 = true;
			IL_328:
			foreach (object obj4 in this.m_internalColumnMappings)
			{
				OracleBulkCopyColumnMapping oracleBulkCopyColumnMapping4 = (OracleBulkCopyColumnMapping)obj4;
				if (oracleBulkCopyColumnMapping4.DestinationColumn.Equals(string.Empty))
				{
					OPOBulkCopyColCtx* ptr2 = this.m_pBlkColCtx;
					if (oracleBulkCopyColumnMapping4.DestinationOrdinal >= this.m_dstColumnCount)
					{
						throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.ODP_INVALID_VALUE, new string[]
						{
							"Column mapping"
						}));
					}
					for (int i = 0; i < oracleBulkCopyColumnMapping4.DestinationOrdinal; i++)
					{
						ptr2++;
					}
					Marshal.PtrToStructure(ptr2->pOPOBulkCopyColRefCtx, opobulkCopyColRefCtx);
					oracleBulkCopyColumnMapping4.DestinationColumn = opobulkCopyColRefCtx.pColName;
				}
				else
				{
					bool flag3 = false;
					int i = 0;
					OPOBulkCopyColCtx* ptr2 = this.m_pBlkColCtx;
					while (i < this.m_dstColumnCount)
					{
						Marshal.PtrToStructure(ptr2->pOPOBulkCopyColRefCtx, opobulkCopyColRefCtx);
						if (oracleBulkCopyColumnMapping4.DestinationColumn.ToUpper().Equals(opobulkCopyColRefCtx.pColName.ToUpper()))
						{
							flag3 = true;
							break;
						}
						i++;
						ptr2++;
					}
					if (!flag3)
					{
						throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.ODP_INVALID_VALUE, new string[]
						{
							"Column mapping"
						}));
					}
				}
			}
			if (!flag2)
			{
				this.m_internalColumnMappings.Sort();
			}
			try
			{
				num = OpsBC.AllocColCtx(ref ptr, this.m_internalColumnMappings.Count);
			}
			catch (Exception ex4)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex4);
				}
				throw;
			}
			finally
			{
				if (num != 0)
				{
					OracleException.HandleError(num, this.m_connection, this.m_opsErrCtx, this);
				}
			}
			this.m_pOpoBulkCopyValCtx->pOPOBulkCopyColCtx = ptr;
			foreach (object obj5 in this.m_internalColumnMappings)
			{
				OracleBulkCopyColumnMapping oracleBulkCopyColumnMapping5 = (OracleBulkCopyColumnMapping)obj5;
				int i = 0;
				OPOBulkCopyColCtx* ptr2 = this.m_pBlkColCtx;
				while (i < this.m_dstColumnCount)
				{
					Marshal.PtrToStructure(ptr2->pOPOBulkCopyColRefCtx, opobulkCopyColRefCtx);
					if (oracleBulkCopyColumnMapping5.DestinationColumn.ToUpper().Equals(opobulkCopyColRefCtx.pColName.ToUpper()))
					{
						try
						{
							try
							{
								num = OpsBC.CopyColCtx(ptr2, ptr);
							}
							catch (Exception ex5)
							{
								if (OraTrace.m_TraceLevel != 0U)
								{
									OraTrace.TraceExceptionInfo(ex5);
								}
								throw;
							}
							break;
						}
						finally
						{
							if (num != 0)
							{
								OracleException.HandleError(num, this.m_connection, this.m_opsErrCtx, this);
							}
						}
					}
					i++;
					ptr2++;
				}
				ptr++;
			}
			this.m_pOpoBulkCopyValCtx->NoOfCols = (ushort)this.m_internalColumnMappings.Count;
			this.ColumnMappings.BulkCopyInProgress = true;
			this.ProcessSrcColumns();
			if (this.m_dataSource is OracleDataReader && this.m_batchSize > 0 && this.m_OptimizedPathForOraDataReader)
			{
				this.m_sourceType = OracleBulkCopy.SourceType.OracleDataReader;
				this.PerformBulkCopyForOraDataReader();
			}
			else
			{
				this.PerformBulkCopy();
			}
			this.ColumnMappings.BulkCopyInProgress = false;
		}

		// Token: 0x060004A7 RID: 1191 RVA: 0x000348BC File Offset: 0x000338BC
		private bool IsBulkCopyOption(OracleBulkCopyOptions copyOption)
		{
			return (this.m_bulkCopyOptions & copyOption) == copyOption;
		}

		// Token: 0x060004A8 RID: 1192 RVA: 0x000348CC File Offset: 0x000338CC
		private bool ReadFromSource()
		{
			switch (this.m_sourceType)
			{
			case OracleBulkCopy.SourceType.IDataReader:
				return ((IDataReader)this.m_dataSource).Read();
			case OracleBulkCopy.SourceType.DataTable:
				while (this.m_rowEnumerator.MoveNext())
				{
					this.m_currentRow = (DataRow)this.m_rowEnumerator.Current;
					if ((this.m_currentRow.RowState & DataRowState.Deleted) == (DataRowState)0 || (this.m_rowState != (DataRowState)0 && (this.m_currentRow.RowState & this.m_rowState) == (DataRowState)0))
					{
						return true;
					}
				}
				return false;
			case OracleBulkCopy.SourceType.RowArray:
				if (this.m_rowEnumerator.MoveNext())
				{
					this.m_currentRow = (DataRow)this.m_rowEnumerator.Current;
					return true;
				}
				return false;
			}
			throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.ODP_INVALID_VALUE, new string[]
			{
				"Source Type " + this.m_sourceType
			}));
		}

		// Token: 0x060004A9 RID: 1193 RVA: 0x000349B8 File Offset: 0x000339B8
		internal unsafe void GetMetaData()
		{
			int num = 0;
			string pCommandText = "select * from " + this.m_destinationTableName;
			OPOBulkCopyRefCtx opobulkCopyRefCtx = new OPOBulkCopyRefCtx();
			Marshal.PtrToStructure(this.m_pOpoBulkCopyValCtx->pOPOBulkCopyRefCtx, opobulkCopyRefCtx);
			opobulkCopyRefCtx.pTableName = this.m_destinationTableName;
			if (this.m_destinationPartitionName != null && !this.m_destinationPartitionName.Equals(string.Empty))
			{
				opobulkCopyRefCtx.pPartitionName = this.m_destinationPartitionName;
			}
			Marshal.StructureToPtr(opobulkCopyRefCtx, this.m_pOpoBulkCopyValCtx->pOPOBulkCopyRefCtx, true);
			try
			{
				num = OpsBC.GetMeta(this.m_opsConCtx, ref this.m_opsErrCtx, ref this.m_opsSqlCtx, pCommandText, this.m_pOpoBulkCopyValCtx);
			}
			catch (Exception ex)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex);
				}
				throw;
			}
			finally
			{
				if (num != 0)
				{
					OracleException.HandleError(num, this.m_connection, this.m_opsErrCtx, this);
				}
			}
			this.m_pBlkColCtx = this.m_pOpoBulkCopyValCtx->pOPOBulkCopyColCtx;
			this.m_dstColumnCount = (int)this.m_pOpoBulkCopyValCtx->NoOfCols;
			this.m_pOpoBulkCopyValCtx->NoLog = 1;
			this.m_pOpoBulkCopyValCtx->pOPOBulkCopyColCtx = null;
		}

		// Token: 0x060004AA RID: 1194 RVA: 0x00034AD4 File Offset: 0x00033AD4
		private unsafe void PrintMetaData(OPOBulkCopyColCtx* pColumn, int numCols)
		{
			int i = 0;
			while (i < numCols)
			{
				OPOBulkCopyColRefCtx opobulkCopyColRefCtx = new OPOBulkCopyColRefCtx();
				Marshal.PtrToStructure(pColumn->pOPOBulkCopyColRefCtx, opobulkCopyColRefCtx);
				Console.WriteLine("Ordinal : " + pColumn->Ordinal);
				Console.WriteLine("Name: " + opobulkCopyColRefCtx.pColName);
				Console.WriteLine("OraType : " + pColumn->OraType);
				Console.WriteLine("Precision : " + pColumn->Precision);
				Console.WriteLine("Scale :" + pColumn->Scale);
				Console.WriteLine("MaxSize :" + pColumn->MaxSize);
				Console.WriteLine("MaxCharSize :" + pColumn->MaxCharSize);
				Console.WriteLine("CharsetID :" + pColumn->CharsetID);
				Console.WriteLine("CharsetForm :" + pColumn->CharsetForm);
				Console.WriteLine();
				i++;
				pColumn++;
			}
		}

		// Token: 0x060004AB RID: 1195 RVA: 0x00034BFC File Offset: 0x00033BFC
		private unsafe void SetColumnInfo(Type srcColType, OPOBulkCopyColCtx* pColumn)
		{
			OraType oraType = (OraType)pColumn->OraType;
			if (!this.m_OptimizedPathForOraDataReader || oraType == OraType.ORA_INTERVAL_DS || oraType == OraType.ORA_INTERVAL_YM || oraType == OraType.ORA_OCIBFileLocator || oraType == OraType.ORA_OCIBLobLocator || oraType == OraType.ORA_OCICLobLocator || oraType == OraType.ORA_TIMESTAMP || oraType == OraType.ORA_TIMESTAMP_TZ || oraType == OraType.ORA_TIMESTAMP_LTZ)
			{
				this.m_OptimizedPathForOraDataReader = false;
			}
			if (oraType == OraType.ORA_DATE || oraType == OraType.ORA_TIMESTAMP || oraType == OraType.ORA_TIMESTAMP_TZ || oraType == OraType.ORA_TIMESTAMP_LTZ || oraType == OraType.ORA_IBDOUBLE || oraType == OraType.ORA_IBFLOAT)
			{
				return;
			}
			string key;
			if ((key = srcColType.ToString()) != null)
			{
				if (<PrivateImplementationDetails>{644819B4-6087-43CF-9BCE-A751E23BE24E}.$$method0x60004a4-1 == null)
				{
					<PrivateImplementationDetails>{644819B4-6087-43CF-9BCE-A751E23BE24E}.$$method0x60004a4-1 = new Dictionary<string, int>(24)
					{
						{
							"System.Boolean",
							0
						},
						{
							"System.SByte",
							1
						},
						{
							"System.Byte",
							2
						},
						{
							"System.Decimal",
							3
						},
						{
							"System.UInt64",
							4
						},
						{
							"Oracle.DataAccess.Types.OracleDecimal",
							5
						},
						{
							"System.Single",
							6
						},
						{
							"System.Double",
							7
						},
						{
							"System.UInt16",
							8
						},
						{
							"System.Int16",
							9
						},
						{
							"System.UInt32",
							10
						},
						{
							"System.Int32",
							11
						},
						{
							"System.String",
							12
						},
						{
							"System.Char",
							13
						},
						{
							"System.Char[]",
							14
						},
						{
							"Oracle.DataAccess.Types.OracleString",
							15
						},
						{
							"Oracle.DataAccess.Types.OracleClob",
							16
						},
						{
							"System.Byte[]",
							17
						},
						{
							"Oracle.DataAccess.Types.OracleBinary",
							18
						},
						{
							"Oracle.DataAccess.Types.OracleBlob",
							19
						},
						{
							"System.TimeSpan",
							20
						},
						{
							"Oracle.DataAccess.Types.OracleIntervalDS",
							21
						},
						{
							"System.Int64",
							22
						},
						{
							"Oracle.DataAccess.Types.OracleIntervalYM",
							23
						}
					};
				}
				int num;
				if (<PrivateImplementationDetails>{644819B4-6087-43CF-9BCE-A751E23BE24E}.$$method0x60004a4-1.TryGetValue(key, out num))
				{
					switch (num)
					{
					case 0:
					case 1:
					case 2:
						pColumn->OraType = 3;
						pColumn->MaxSize = 1U;
						return;
					case 3:
					case 4:
					case 5:
					case 6:
					case 7:
					case 8:
					case 9:
					case 10:
					case 11:
						pColumn->OraType = 6;
						pColumn->MaxSize = 22U;
						return;
					case 12:
					case 13:
					case 14:
					case 15:
					case 16:
						pColumn->OraType = 1;
						pColumn->IsPtrData = 1U;
						return;
					case 17:
					case 18:
					case 19:
						pColumn->OraType = 23;
						pColumn->IsPtrData = 1U;
						return;
					case 20:
					case 21:
						oraType = (OraType)pColumn->OraType;
						pColumn->MaxSize = (uint)sizeof(OpoTSValCtx);
						if (oraType != OraType.ORA_INTERVAL_DS)
						{
							throw new ArgumentException(OpoErrResManager.GetErrorMesg(ErrRes.PRM_INVALID_BIND, new string[0]), srcColType.ToString());
						}
						break;
					case 22:
					case 23:
						oraType = (OraType)pColumn->OraType;
						pColumn->MaxSize = (uint)sizeof(OpoITLValCtx);
						if (oraType != OraType.ORA_INTERVAL_YM)
						{
							if (oraType != OraType.ORA_NUMBER)
							{
								throw new ArgumentException(OpoErrResManager.GetErrorMesg(ErrRes.PRM_INVALID_BIND, new string[0]), srcColType.ToString());
							}
							pColumn->OraType = 6;
							pColumn->MaxSize = 22U;
							return;
						}
						break;
					default:
						goto IL_30D;
					}
					return;
				}
			}
			IL_30D:
			throw new ArgumentException(OpoErrResManager.GetErrorMesg(ErrRes.PRM_INVALID_BIND, new string[0]), srcColType.ToString());
		}

		// Token: 0x060004AC RID: 1196 RVA: 0x00034F34 File Offset: 0x00033F34
		private unsafe void ProcessSrcColumns()
		{
			this.m_OptimizedPathForOraDataReader = true;
			int i = 0;
			OPOBulkCopyColCtx* ptr = this.m_pOpoBulkCopyValCtx->pOPOBulkCopyColCtx;
			while (i < (int)this.m_pOpoBulkCopyValCtx->NoOfCols)
			{
				Type srcColType;
				if (this.m_sourceType == OracleBulkCopy.SourceType.DataTable || this.m_sourceType == OracleBulkCopy.SourceType.RowArray)
				{
					srcColType = ((DataTable)this.m_dataSource).Columns[i].DataType;
				}
				else
				{
					srcColType = ((IDataReader)this.m_dataSource).GetFieldType(i);
				}
				this.SetColumnInfo(srcColType, ptr);
				if (ptr->IsPtrData == 1U)
				{
					this.m_rowSize += this.SIZE_OF_PTR;
				}
				else
				{
					if (ptr->OraType == 187 || ptr->OraType == 188 || ptr->OraType == 232)
					{
						ptr->MaxSize = (uint)sizeof(OpoTSValCtx);
					}
					else if (ptr->OraType == 190 || ptr->OraType == 189)
					{
						ptr->MaxSize = (uint)sizeof(OpoITLValCtx);
					}
					this.m_rowSize += (int)ptr->MaxSize;
				}
				i++;
				ptr++;
			}
		}

		// Token: 0x060004AD RID: 1197 RVA: 0x00035050 File Offset: 0x00034050
		private unsafe void PerformBulkCopy()
		{
			bool flag = false;
			bool flag2 = false;
			int num = 0;
			int num2 = 0;
			int num3 = this.m_batchSize;
			int notifyAfter = this.m_notifyAfter;
			int count = this.m_internalColumnMappings.Count;
			OracleBulkCopy.m_timeElapsed = false;
			Timer timer = new Timer();
			timer.Elapsed += OracleBulkCopy.OnTimeElapsed;
			timer.Interval = (double)(this.m_timeout * 1000);
			timer.AutoReset = false;
			timer.Enabled = true;
			try
			{
				this.m_rowsCopied = 0;
				int num4 = notifyAfter;
				if (num3 > 0)
				{
					num2 = num3;
				}
				else
				{
					num2 = 10000;
					num3 = 10000;
				}
				int num5 = num3;
				if (this.ReadFromSource())
				{
					OPOBufferNode* ptr = (OPOBufferNode*)((void*)IntPtr.Zero);
					try
					{
						num = OpsBC.AllocBufferNode(ref ptr, num2, this.m_rowSize + count * this.COL_HEADER_SIZE);
					}
					catch (Exception ex)
					{
						if (OraTrace.m_TraceLevel != 0U)
						{
							OraTrace.TraceExceptionInfo(ex);
						}
						throw;
					}
					finally
					{
						if (num != 0)
						{
							OracleException.HandleError(num, this.m_connection, this.m_opsErrCtx, this);
						}
					}
					this.m_pOpoBulkCopyValCtx->pInputBuffer = ptr;
					this.m_pOpoBulkCopyValCtx->MaxRowsInBuffer = num2;
					if (this.IsBulkCopyOption(OracleBulkCopyOptions.UseInternalTransaction))
					{
						this.m_internalTransaction = this.m_connection.BeginTransaction();
					}
					void* ptr2 = (void*)ptr->pBuffer;
					try
					{
						num = OpsBC.Init(this.m_opsConCtx, this.m_pOpoBulkCopyValCtx, this.m_opsErrCtx);
					}
					catch (Exception ex2)
					{
						if (OraTrace.m_TraceLevel != 0U)
						{
							OraTrace.TraceExceptionInfo(ex2);
						}
						throw;
					}
					finally
					{
						if (num != 0)
						{
							OracleException.HandleError(num, this.m_connection, this.m_opsErrCtx, this);
						}
					}
					for (;;)
					{
						int i = 0;
						OPOBulkCopyColCtx* ptr3 = this.m_pOpoBulkCopyValCtx->pOPOBulkCopyColCtx;
						while (i < count)
						{
							object obj;
							if (this.m_sourceType == OracleBulkCopy.SourceType.DataTable || this.m_sourceType == OracleBulkCopy.SourceType.RowArray)
							{
								obj = this.m_currentRow[this.m_internalColumnMappings[i].m_sourceColumnOrdinal];
							}
							else
							{
								obj = ((IDataReader)this.m_dataSource).GetValue(this.m_internalColumnMappings[i].m_sourceColumnOrdinal);
							}
							if (obj is DBNull)
							{
								*(short*)ptr2 = -1;
								ptr2 = (void*)((byte*)ptr2 + OracleBulkCopy.COL_NULLIND_SIZE);
							}
							else
							{
								*(short*)ptr2 = 0;
								ptr2 = (void*)((byte*)ptr2 + OracleBulkCopy.COL_NULLIND_SIZE);
								if (ptr3->IsPtrData == 1U)
								{
									if (obj is byte[])
									{
										*(int*)ptr2 = ((byte[])obj).Length;
									}
									else if (obj is OracleBinary)
									{
										*(int*)ptr2 = ((OracleBinary)obj).Length;
									}
									else if (obj is OracleBlob)
									{
										*(int*)ptr2 = (int)((OracleBlob)obj).Length;
									}
									else if (obj is OracleClob)
									{
										*(int*)ptr2 = ((OracleClob)obj).Value.Length * this.SIZE_OF_CHAR;
									}
									else
									{
										*(int*)ptr2 = obj.ToString().Length * this.SIZE_OF_CHAR;
									}
								}
								else
								{
									*(int*)ptr2 = (int)ptr3->MaxSize;
								}
								ptr2 = (void*)((byte*)ptr2 + OracleBulkCopy.COL_LEN_SIZE);
								OraType oraType = (OraType)ptr3->OraType;
								if (oraType <= OraType.ORA_RAW)
								{
									switch (oraType)
									{
									case OraType.ORA_CHARN:
										if (obj is OracleClob)
										{
											*(IntPtr*)ptr2 = Marshal.StringToCoTaskMemUni(((OracleClob)obj).Value).ToPointer();
											goto IL_BB1;
										}
										*(IntPtr*)ptr2 = Marshal.StringToCoTaskMemUni(obj.ToString()).ToPointer();
										goto IL_BB1;
									case OraType.ORA_NUMBER:
									case OraType.ORA_NULLSTR:
										goto IL_B8C;
									case OraType.ORA_SB1:
										if (obj is byte || obj is sbyte || obj is bool)
										{
											*(byte*)ptr2 = Convert.ToByte(obj);
											goto IL_BB1;
										}
										if (obj is short || obj is ushort)
										{
											*(short*)ptr2 = Convert.ToInt16(obj);
											goto IL_BB1;
										}
										if (obj is int || obj is uint)
										{
											*(int*)ptr2 = Convert.ToInt32(obj);
											goto IL_BB1;
										}
										goto IL_BB1;
									case OraType.ORA_FLOAT:
										break;
									case OraType.ORA_VARNUM:
									{
										if (obj is decimal)
										{
											DecimalConv.GetBytes(Convert.ToDecimal(obj), (IntPtr)ptr2);
											goto IL_BB1;
										}
										if (obj is float)
										{
											OracleDecimal oracleDecimal = new OracleDecimal((double)((float)obj));
											try
											{
												OpsDec.GetValCtxForSetPrecNoRound(oracleDecimal.m_opoDecCtx.m_pValCtx, 7, (IntPtr)ptr2);
												goto IL_BB1;
											}
											catch (Exception ex3)
											{
												if (OraTrace.m_TraceLevel != 0U)
												{
													OraTrace.TraceExceptionInfo(ex3);
												}
												throw;
											}
										}
										if (obj is OracleDecimal)
										{
											byte[] binData = ((OracleDecimal)obj).BinData;
											byte* ptr4 = (byte*)ptr2;
											for (int j = 0; j <= (int)binData[0]; j++)
											{
												ptr4[j] = binData[j];
											}
											goto IL_BB1;
										}
										if (obj is double)
										{
											OracleDecimal oracleDecimal2 = new OracleDecimal((double)obj);
											try
											{
												OpsDec.GetValCtxForSetPrecNoRound(oracleDecimal2.m_opoDecCtx.m_pValCtx, 16, (IntPtr)ptr2);
												goto IL_BB1;
											}
											catch (Exception ex4)
											{
												if (OraTrace.m_TraceLevel != 0U)
												{
													OraTrace.TraceExceptionInfo(ex4);
												}
												throw;
											}
										}
										long num6 = Convert.ToInt64(obj);
										try
										{
											OpsDec.GetValCtxFromInteger((void*)(&num6), 8, (IntPtr)ptr2);
											goto IL_BB1;
										}
										catch (Exception ex5)
										{
											if (OraTrace.m_TraceLevel != 0U)
											{
												OraTrace.TraceExceptionInfo(ex5);
											}
											throw;
										}
										break;
									}
									default:
										if (oraType != OraType.ORA_DATE)
										{
											switch (oraType)
											{
											case OraType.ORA_BFLOAT:
												*(float*)ptr2 = Convert.ToSingle(obj);
												goto IL_BB1;
											case OraType.ORA_BDOUBLE:
												*(double*)ptr2 = Convert.ToDouble(obj);
												goto IL_BB1;
											case OraType.ORA_RAW:
												if (obj is byte[])
												{
													int num7 = ((byte[])obj).Length;
													IntPtr destination = Marshal.AllocCoTaskMem(num7);
													Marshal.Copy((byte[])obj, 0, destination, num7);
													*(IntPtr*)ptr2 = destination.ToPointer();
													goto IL_BB1;
												}
												if (obj is OracleBinary)
												{
													int length = ((OracleBinary)obj).Length;
													IntPtr destination2 = Marshal.AllocCoTaskMem(length);
													Marshal.Copy(((OracleBinary)obj).Value, 0, destination2, length);
													*(IntPtr*)ptr2 = destination2.ToPointer();
													goto IL_BB1;
												}
												if (obj is OracleBlob)
												{
													int num8 = (int)((OracleBlob)obj).Length;
													IntPtr destination3 = Marshal.AllocCoTaskMem(num8);
													Marshal.Copy(((OracleBlob)obj).Value, 0, destination3, num8);
													*(IntPtr*)ptr2 = destination3.ToPointer();
													goto IL_BB1;
												}
												goto IL_BB1;
											}
											goto Block_18;
										}
										if (obj is DateTime)
										{
											DateTimeConv.ToBytes((DateTime)obj, (byte*)ptr2);
											goto IL_BB1;
										}
										if (obj is OracleDate)
										{
											OracleDate.ToBytes(((OracleDate)obj).GetValCtx(), (byte*)ptr2);
											goto IL_BB1;
										}
										if (obj is OracleTimeStamp)
										{
											OracleDate.ToBytes(((OracleTimeStamp)obj).GetValCtx(), (byte*)ptr2);
											goto IL_BB1;
										}
										if (obj is OracleTimeStampTZ)
										{
											OracleDate.ToBytes(((OracleTimeStampTZ)obj).GetValCtx(), (byte*)ptr2);
											goto IL_BB1;
										}
										if (obj is OracleTimeStampLTZ)
										{
											OracleDate.ToBytes(((OracleTimeStampLTZ)obj).GetValCtx(), (byte*)ptr2);
											goto IL_BB1;
										}
										if (obj is string)
										{
											OracleDate oracleDate = new OracleDate(obj.ToString());
											DateTimeConv.ToBytes(oracleDate.Value, (byte*)ptr2);
											goto IL_BB1;
										}
										goto IL_BB1;
									}
									*(double*)ptr2 = Convert.ToDouble(obj);
								}
								else
								{
									switch (oraType)
									{
									case OraType.ORA_IBFLOAT:
										OpsBC.ConvertToBinaryFloat(this.m_pOpoBulkCopyValCtx->lfpContext, obj.ToString(), (byte*)ptr2);
										break;
									case OraType.ORA_IBDOUBLE:
										OpsBC.ConvertToBinaryDouble(this.m_pOpoBulkCopyValCtx->lfpContext, obj.ToString(), (byte*)ptr2);
										break;
									default:
										switch (oraType)
										{
										case OraType.ORA_TIMESTAMP:
										{
											OpoTSValCtx* ptr5 = null;
											if (obj is DateTime)
											{
												OracleTimeStamp oracleTimeStamp = new OracleTimeStamp((DateTime)obj);
												ptr5 = oracleTimeStamp.GetValCtx();
											}
											else if (obj is OracleDate)
											{
												OracleTimeStamp oracleTimeStamp = ((OracleDate)obj).ToOracleTimeStamp();
												ptr5 = oracleTimeStamp.GetValCtx();
											}
											else if (obj is OracleTimeStamp)
											{
												OracleTimeStamp oracleTimeStamp = (OracleTimeStamp)obj;
												ptr5 = oracleTimeStamp.GetValCtx();
											}
											else if (obj is OracleTimeStampTZ)
											{
												OracleTimeStamp oracleTimeStamp = ((OracleTimeStampTZ)obj).ToOracleTimeStamp();
												ptr5 = oracleTimeStamp.GetValCtx();
											}
											else if (obj is OracleTimeStampLTZ)
											{
												OracleTimeStamp oracleTimeStamp = ((OracleTimeStampLTZ)obj).ToOracleTimeStamp();
												ptr5 = oracleTimeStamp.GetValCtx();
											}
											else if (obj is string)
											{
												OracleTimeStamp oracleTimeStamp = new OracleTimeStamp(obj.ToString());
												ptr5 = oracleTimeStamp.GetValCtx();
											}
											if (null != ptr5)
											{
												byte* ptr6 = (byte*)ptr2;
												byte* ptr7 = (byte*)ptr5;
												int num9 = 0;
												while ((long)num9 < (long)((ulong)ptr3->MaxSize))
												{
													ptr6[num9] = ptr7[num9];
													num9++;
												}
											}
											break;
										}
										case OraType.ORA_TIMESTAMP_TZ:
										{
											OpoTSValCtx* ptr8 = null;
											if (obj is DateTime)
											{
												OracleTimeStampTZ oracleTimeStampTZ = new OracleTimeStampTZ((DateTime)obj);
												ptr8 = oracleTimeStampTZ.GetValCtx();
											}
											else if (obj is OracleDate)
											{
												OracleTimeStampTZ oracleTimeStampTZ = new OracleTimeStampTZ(((OracleDate)obj).Value);
												ptr8 = oracleTimeStampTZ.GetValCtx();
											}
											else if (obj is OracleTimeStamp)
											{
												OracleTimeStampTZ oracleTimeStampTZ = ((OracleTimeStamp)obj).ToOracleTimeStampTZ();
												ptr8 = oracleTimeStampTZ.GetValCtx();
											}
											else if (obj is OracleTimeStampTZ)
											{
												OracleTimeStampTZ oracleTimeStampTZ = (OracleTimeStampTZ)obj;
												ptr8 = oracleTimeStampTZ.GetValCtx();
											}
											else if (obj is OracleTimeStampLTZ)
											{
												OracleTimeStampTZ oracleTimeStampTZ = ((OracleTimeStampLTZ)obj).ToOracleTimeStampTZ();
												ptr8 = oracleTimeStampTZ.GetValCtx();
											}
											else if (obj is string)
											{
												OracleTimeStampTZ oracleTimeStampTZ = new OracleTimeStampTZ(obj.ToString());
												ptr8 = oracleTimeStampTZ.GetValCtx();
											}
											if (null != ptr8)
											{
												byte* ptr9 = (byte*)ptr2;
												byte* ptr10 = (byte*)ptr8;
												int num10 = 0;
												while ((long)num10 < (long)((ulong)ptr3->MaxSize))
												{
													ptr9[num10] = ptr10[num10];
													num10++;
												}
											}
											break;
										}
										case OraType.ORA_INTERVAL_YM:
										{
											OpoITLValCtx* ptr11 = null;
											if (obj is OracleIntervalYM)
											{
												OracleIntervalYM oracleIntervalYM = (OracleIntervalYM)obj;
												ptr11 = oracleIntervalYM.GetValCtx();
											}
											else if (obj is long)
											{
												OracleIntervalYM oracleIntervalYM = new OracleIntervalYM(Convert.ToInt64(obj));
												ptr11 = oracleIntervalYM.GetValCtx();
											}
											if (null != ptr11)
											{
												byte* ptr12 = (byte*)ptr2;
												byte* ptr13 = (byte*)ptr11;
												int num11 = 0;
												while ((long)num11 < (long)((ulong)ptr3->MaxSize))
												{
													ptr12[num11] = ptr13[num11];
													num11++;
												}
											}
											break;
										}
										case OraType.ORA_INTERVAL_DS:
										{
											OpoITLValCtx* ptr14 = null;
											if (obj is TimeSpan)
											{
												OracleIntervalDS oracleIntervalDS = new OracleIntervalDS((TimeSpan)obj);
												ptr14 = oracleIntervalDS.GetValCtx();
											}
											else if (obj is OracleIntervalDS)
											{
												OracleIntervalDS oracleIntervalDS = (OracleIntervalDS)obj;
												ptr14 = oracleIntervalDS.GetValCtx();
											}
											if (null != ptr14)
											{
												byte* ptr15 = (byte*)ptr2;
												byte* ptr16 = (byte*)ptr14;
												int num12 = 0;
												while ((long)num12 < (long)((ulong)ptr3->MaxSize))
												{
													ptr15[num12] = ptr16[num12];
													num12++;
												}
											}
											break;
										}
										default:
										{
											if (oraType != OraType.ORA_TIMESTAMP_LTZ)
											{
												goto Block_21;
											}
											OpoTSValCtx* ptr17 = null;
											if (obj is DateTime)
											{
												OracleTimeStampLTZ oracleTimeStampLTZ = new OracleTimeStampLTZ((DateTime)obj);
												ptr17 = oracleTimeStampLTZ.GetValCtx();
											}
											else if (obj is OracleDate)
											{
												OracleTimeStampLTZ oracleTimeStampLTZ = new OracleTimeStampLTZ(((OracleDate)obj).Value);
												ptr17 = oracleTimeStampLTZ.GetValCtx();
											}
											else if (obj is OracleTimeStamp)
											{
												OracleTimeStampLTZ oracleTimeStampLTZ = ((OracleTimeStamp)obj).ToOracleTimeStampLTZ();
												ptr17 = oracleTimeStampLTZ.GetValCtx();
											}
											else if (obj is OracleTimeStampTZ)
											{
												OracleTimeStampLTZ oracleTimeStampLTZ = ((OracleTimeStampTZ)obj).ToOracleTimeStampLTZ();
												ptr17 = oracleTimeStampLTZ.GetValCtx();
											}
											else if (obj is OracleTimeStampLTZ)
											{
												OracleTimeStampLTZ oracleTimeStampLTZ = (OracleTimeStampLTZ)obj;
												ptr17 = oracleTimeStampLTZ.GetValCtx();
											}
											else if (obj is string)
											{
												OracleTimeStampLTZ oracleTimeStampLTZ = new OracleTimeStampLTZ(obj.ToString());
												ptr17 = oracleTimeStampLTZ.GetValCtx();
											}
											if (null != ptr17)
											{
												byte* ptr18 = (byte*)ptr2;
												byte* ptr19 = (byte*)ptr17;
												int num13 = 0;
												while ((long)num13 < (long)((ulong)ptr3->MaxSize))
												{
													ptr18[num13] = ptr19[num13];
													num13++;
												}
											}
											break;
										}
										}
										break;
									}
								}
								IL_BB1:
								if (ptr3->IsPtrData == 1U)
								{
									ptr2 = (void*)((byte*)ptr2 + this.SIZE_OF_PTR);
								}
								else
								{
									ptr2 = (void*)((byte*)ptr2 + ptr3->MaxSize);
								}
							}
							i++;
							ptr3++;
						}
						this.m_rowsCopied++;
						this.m_pOpoBulkCopyValCtx->NoOfRows++;
						if (OracleBulkCopy.m_timeElapsed)
						{
							goto Block_78;
						}
						if (num4 > 0)
						{
							num4--;
						}
						if (notifyAfter > 0 && num4 == 0)
						{
							bool flag3 = this.FireRowsCopiedEvent((long)this.m_rowsCopied);
							if (flag3)
							{
								goto Block_82;
							}
							num4 = notifyAfter;
						}
						if (num5 > 0)
						{
							num5--;
						}
						if (num3 > 0 && num5 == 0)
						{
							try
							{
								this.m_badRowNumber = -1;
								this.m_badColNumber = -1;
								num = OpsBC.Load(this.m_opsConCtx, this.m_pOpoBulkCopyValCtx, this.m_opsErrCtx, ref this.m_badRowNumber, ref this.m_badColNumber, 0, IntPtr.Zero, (OpoMetValCtx*)((void*)IntPtr.Zero), (OpoDacValCtx*)((void*)IntPtr.Zero));
							}
							catch (Exception ex6)
							{
								if (OraTrace.m_TraceLevel != 0U)
								{
									OraTrace.TraceExceptionInfo(ex6);
								}
								throw;
							}
							finally
							{
								if (num != 0)
								{
									OpsBC.FreeInputBuffer(this.m_pOpoBulkCopyValCtx);
									OracleException ex7 = new OracleException(this.m_opsErrCtx, null, this.m_opsConCtx, this.m_connection.DataSource, string.Empty);
									OracleError value = new OracleError(ErrRes.BC_ERROR, string.Empty, string.Empty, OpoErrResManager.GetErrorMesg(ErrRes.BC_ERROR, new string[]
									{
										this.m_badRowNumber.ToString(),
										this.m_badColNumber.ToString()
									}));
									ex7.Errors.Insert(0, value);
									throw ex7;
								}
							}
							try
							{
								num = OpsBC.FreeDataPointers(this.m_pOpoBulkCopyValCtx);
								num = OpsBC.Finish(this.m_opsConCtx, this.m_pOpoBulkCopyValCtx->pOPOBulkCopyCtx, this.m_opsErrCtx);
								if (num == 0)
								{
									num = OpsBC.Cleanup(this.m_pOpoBulkCopyValCtx);
								}
							}
							catch (Exception ex8)
							{
								if (OraTrace.m_TraceLevel != 0U)
								{
									OraTrace.TraceExceptionInfo(ex8);
								}
								throw;
							}
							finally
							{
								if (num != 0)
								{
									OracleException.HandleError(num, this.m_connection, this.m_opsErrCtx, this);
								}
							}
							if (this.m_internalTransaction != null)
							{
								this.m_internalTransaction.Commit();
								this.m_internalTransaction.Dispose();
								this.m_internalTransaction = null;
							}
							num5 = num3;
							flag2 = true;
						}
						flag = this.ReadFromSource();
						if (!flag)
						{
							goto IL_F70;
						}
						if (num3 == 0 && this.m_pOpoBulkCopyValCtx->NoOfRows % num2 == 0)
						{
							OPOBufferNode* value2 = (OPOBufferNode*)((void*)IntPtr.Zero);
							try
							{
								num = OpsBC.AllocBufferNode(ref value2, num2, this.m_rowSize + count * this.COL_HEADER_SIZE);
							}
							catch (Exception ex9)
							{
								if (OraTrace.m_TraceLevel != 0U)
								{
									OraTrace.TraceExceptionInfo(ex9);
								}
								throw;
							}
							finally
							{
								if (num != 0)
								{
									OracleException.HandleError(num, this.m_connection, this.m_opsErrCtx, this);
								}
							}
							ptr->pNext = (IntPtr)((void*)value2);
							ptr = (OPOBufferNode*)((void*)ptr->pNext);
							ptr2 = (void*)ptr->pBuffer;
						}
						if (flag2)
						{
							flag2 = false;
							this.m_pOpoBulkCopyValCtx->NoOfRows = 0;
							if (this.IsBulkCopyOption(OracleBulkCopyOptions.UseInternalTransaction))
							{
								this.m_internalTransaction = this.m_connection.BeginTransaction();
							}
							ptr2 = (void*)this.m_pOpoBulkCopyValCtx->pInputBuffer->pBuffer;
							try
							{
								try
								{
									num = OpsBC.Init(this.m_opsConCtx, this.m_pOpoBulkCopyValCtx, this.m_opsErrCtx);
								}
								catch (Exception ex10)
								{
									if (OraTrace.m_TraceLevel != 0U)
									{
										OraTrace.TraceExceptionInfo(ex10);
									}
									throw;
								}
								goto IL_10F2;
							}
							finally
							{
								if (num != 0)
								{
									OracleException.HandleError(num, this.m_connection, this.m_opsErrCtx, this);
								}
							}
							goto IL_F70;
						}
						IL_10F2:
						if (!flag)
						{
							goto Block_97;
						}
						continue;
						IL_F70:
						if (!flag2)
						{
							try
							{
								this.m_badRowNumber = -1;
								this.m_badColNumber = -1;
								num = OpsBC.Load(this.m_opsConCtx, this.m_pOpoBulkCopyValCtx, this.m_opsErrCtx, ref this.m_badRowNumber, ref this.m_badColNumber, 0, IntPtr.Zero, (OpoMetValCtx*)((void*)IntPtr.Zero), (OpoDacValCtx*)((void*)IntPtr.Zero));
							}
							catch (Exception ex11)
							{
								if (OraTrace.m_TraceLevel != 0U)
								{
									OraTrace.TraceExceptionInfo(ex11);
								}
								throw;
							}
							finally
							{
								if (num != 0)
								{
									OpsBC.FreeInputBuffer(this.m_pOpoBulkCopyValCtx);
									OracleException ex12 = new OracleException(this.m_opsErrCtx, null, this.m_opsConCtx, this.m_connection.DataSource, string.Empty);
									OracleError value3 = new OracleError(ErrRes.BC_ERROR, string.Empty, string.Empty, OpoErrResManager.GetErrorMesg(ErrRes.BC_ERROR, new string[]
									{
										this.m_badRowNumber.ToString(),
										this.m_badColNumber.ToString()
									}));
									ex12.Errors.Insert(0, value3);
									throw ex12;
								}
							}
							try
							{
								num = OpsBC.Finish(this.m_opsConCtx, this.m_pOpoBulkCopyValCtx->pOPOBulkCopyCtx, this.m_opsErrCtx);
								if (num == 0)
								{
									num = OpsBC.Cleanup(this.m_pOpoBulkCopyValCtx);
								}
								if (this.m_internalTransaction != null)
								{
									this.m_internalTransaction.Commit();
									this.m_internalTransaction.Dispose();
									this.m_internalTransaction = null;
								}
							}
							catch (Exception ex13)
							{
								if (OraTrace.m_TraceLevel != 0U)
								{
									OraTrace.TraceExceptionInfo(ex13);
								}
								throw;
							}
							finally
							{
								if (num != 0)
								{
									OracleException.HandleError(num, this.m_connection, this.m_opsErrCtx, this);
								}
							}
							goto IL_10F2;
						}
						goto IL_10F2;
					}
					Block_18:
					Block_21:
					IL_B8C:
					throw new OracleException(ErrRes.CMD_TYPE_NOT_SUPPORTED, string.Empty, string.Empty, OpoErrResManager.GetErrorMesg(ErrRes.CMD_TYPE_NOT_SUPPORTED, new string[0]));
					Block_78:
					this.Abort();
					throw new OracleException(ErrRes.BC_OPER_TIMEOUT, string.Empty, string.Empty, OpoErrResManager.GetErrorMesg(ErrRes.BC_OPER_TIMEOUT, new string[0]));
					Block_82:
					this.Abort();
					throw new OracleException(ErrRes.BC_OPER_ABORT, string.Empty, string.Empty, OpoErrResManager.GetErrorMesg(ErrRes.BC_OPER_ABORT, new string[0]));
					Block_97:
					try
					{
						num = OpsBC.FreeInputBuffer(this.m_pOpoBulkCopyValCtx);
					}
					catch (Exception ex14)
					{
						if (OraTrace.m_TraceLevel != 0U)
						{
							OraTrace.TraceExceptionInfo(ex14);
						}
						throw;
					}
					finally
					{
						if (num != 0)
						{
							OracleException.HandleError(num, this.m_connection, this.m_opsErrCtx, this);
						}
					}
				}
			}
			finally
			{
				timer.Enabled = false;
				OracleBulkCopy.m_timeElapsed = false;
				timer.Dispose();
				if (this.m_internalTransaction != null)
				{
					this.m_internalTransaction.Rollback();
					this.m_internalTransaction.Dispose();
					this.m_internalTransaction = null;
				}
			}
		}

		// Token: 0x060004AE RID: 1198 RVA: 0x000363E4 File Offset: 0x000353E4
		private unsafe void PerformBulkCopyForOraDataReader()
		{
			bool flag = false;
			int num = 0;
			int batchSize = this.m_batchSize;
			int notifyAfter = this.m_notifyAfter;
			OracleDataReader oracleDataReader = this.m_dataSource as OracleDataReader;
			OracleBulkCopy.m_timeElapsed = false;
			Timer timer = new Timer();
			timer.Elapsed += OracleBulkCopy.OnTimeElapsed;
			timer.Interval = (double)(this.m_timeout * 1000);
			timer.AutoReset = false;
			timer.Enabled = true;
			try
			{
				this.m_rowsCopied = 0;
				int num2 = notifyAfter;
				int num3 = batchSize;
				oracleDataReader.FetchSizeInRows = (long)(batchSize - 1);
				oracleDataReader.m_currentClientRow = oracleDataReader.m_pOpoDacValCtx->CurrentClientRow;
				bool flag2 = oracleDataReader.Read();
				if (flag2)
				{
					for (;;)
					{
						this.m_rowsCopied++;
						this.m_pOpoBulkCopyValCtx->NoOfRows++;
						if (OracleBulkCopy.m_timeElapsed)
						{
							break;
						}
						if (oracleDataReader.m_pOpoDacValCtx->RecordCount % batchSize != 0)
						{
							flag = true;
						}
						if (num2 > 0)
						{
							num2--;
						}
						if (notifyAfter > 0 && num2 == 0)
						{
							bool flag3 = this.FireRowsCopiedEvent((long)this.m_rowsCopied);
							if (flag3)
							{
								goto Block_8;
							}
							num2 = notifyAfter;
						}
						if (num3 > 0)
						{
							num3--;
						}
						if ((batchSize > 0 && num3 == 0) || flag)
						{
							if (this.IsBulkCopyOption(OracleBulkCopyOptions.UseInternalTransaction))
							{
								this.m_internalTransaction = this.m_connection.BeginTransaction();
							}
							try
							{
								num = OpsBC.Init(this.m_opsConCtx, this.m_pOpoBulkCopyValCtx, this.m_opsErrCtx);
							}
							catch (Exception ex)
							{
								if (OraTrace.m_TraceLevel != 0U)
								{
									OraTrace.TraceExceptionInfo(ex);
								}
								throw;
							}
							finally
							{
								if (num != 0)
								{
									OracleException.HandleError(num, this.m_connection, this.m_opsErrCtx, this);
								}
							}
							try
							{
								this.m_badRowNumber = -1;
								this.m_badColNumber = -1;
								num = OpsBC.Load(this.m_opsConCtx, this.m_pOpoBulkCopyValCtx, this.m_opsErrCtx, ref this.m_badRowNumber, ref this.m_badColNumber, 1, oracleDataReader.m_opsDacCtx, oracleDataReader.m_pOpoMetValCtx, oracleDataReader.m_pOpoDacValCtx);
							}
							catch (Exception ex2)
							{
								if (OraTrace.m_TraceLevel != 0U)
								{
									OraTrace.TraceExceptionInfo(ex2);
								}
								throw;
							}
							finally
							{
								if (num != 0)
								{
									OracleException ex3 = new OracleException(this.m_opsErrCtx, null, this.m_opsConCtx, this.m_connection.DataSource, string.Empty);
									OracleError value = new OracleError(ErrRes.BC_ERROR, string.Empty, string.Empty, OpoErrResManager.GetErrorMesg(ErrRes.BC_ERROR, new string[]
									{
										this.m_badRowNumber.ToString(),
										this.m_badColNumber.ToString()
									}));
									ex3.Errors.Insert(0, value);
									throw ex3;
								}
							}
							try
							{
								num = OpsBC.Finish(this.m_opsConCtx, this.m_pOpoBulkCopyValCtx->pOPOBulkCopyCtx, this.m_opsErrCtx);
								if (num == 0)
								{
									num = OpsBC.Cleanup(this.m_pOpoBulkCopyValCtx);
								}
							}
							catch (Exception ex4)
							{
								if (OraTrace.m_TraceLevel != 0U)
								{
									OraTrace.TraceExceptionInfo(ex4);
								}
								throw;
							}
							finally
							{
								if (num != 0)
								{
									OracleException.HandleError(num, this.m_connection, this.m_opsErrCtx, this);
								}
							}
							if (this.m_internalTransaction != null)
							{
								this.m_internalTransaction.Commit();
								this.m_internalTransaction.Dispose();
								this.m_internalTransaction = null;
							}
							num3 = batchSize;
							oracleDataReader.m_currentClientRow = oracleDataReader.m_pOpoDacValCtx->CurrentClientRow;
							flag2 = oracleDataReader.Read();
							this.m_pOpoBulkCopyValCtx->NoOfRows = 0;
						}
						if (!flag2)
						{
							goto IL_370;
						}
					}
					this.Abort();
					throw new OracleException(ErrRes.BC_OPER_TIMEOUT, string.Empty, string.Empty, OpoErrResManager.GetErrorMesg(ErrRes.BC_OPER_TIMEOUT, new string[0]));
					Block_8:
					this.Abort();
					throw new OracleException(ErrRes.BC_OPER_ABORT, string.Empty, string.Empty, OpoErrResManager.GetErrorMesg(ErrRes.BC_OPER_ABORT, new string[0]));
				}
				IL_370:;
			}
			finally
			{
				timer.Enabled = false;
				OracleBulkCopy.m_timeElapsed = false;
				timer.Dispose();
				if (this.m_internalTransaction != null)
				{
					this.m_internalTransaction.Rollback();
					this.m_internalTransaction.Dispose();
					this.m_internalTransaction = null;
				}
			}
		}

		// Token: 0x060004AF RID: 1199 RVA: 0x0003684C File Offset: 0x0003584C
		private void ValidateConnection()
		{
			int num = 0;
			if (this.m_ownConnection && this.m_connection.State != ConnectionState.Open)
			{
				this.m_connection.Open();
				this.m_conSignature = this.m_connection.m_conSignature;
				this.m_opsConCtx = this.m_connection.m_opoConCtx.opsConCtx;
				try
				{
					int num2 = OpsCon.AddRef(this.m_opsConCtx);
					if (num2 <= 1)
					{
						num = ErrRes.CON_CLOSED;
						throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_CLOSED, new string[0]));
					}
					return;
				}
				catch (Exception ex)
				{
					if (num != 0 && num != ErrRes.CON_CLOSED && OraTrace.m_TraceLevel != 0U)
					{
						OraTrace.TraceExceptionInfo(ex);
					}
					throw;
				}
			}
			if (this.m_connection.m_opoConCtx.opsConCtx == IntPtr.Zero)
			{
				throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_CLOSED, new string[0]));
			}
			if (this.m_opsConCtx != this.m_connection.m_opoConCtx.opsConCtx)
			{
				if (this.m_opsConCtx != IntPtr.Zero)
				{
					try
					{
						OpsCon.RelRef(ref this.m_opsConCtx);
					}
					catch (Exception ex2)
					{
						if (OraTrace.m_TraceLevel != 0U)
						{
							OraTrace.TraceExceptionInfo(ex2);
						}
					}
				}
				this.m_opsConCtx = this.m_connection.m_opoConCtx.opsConCtx;
				try
				{
					int num3 = OpsCon.AddRef(this.m_opsConCtx);
					if (num3 <= 1)
					{
						num = ErrRes.CON_CLOSED;
						throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_CLOSED, new string[0]));
					}
				}
				catch (Exception ex3)
				{
					if (num != 0 && num != ErrRes.CON_CLOSED && OraTrace.m_TraceLevel != 0U)
					{
						OraTrace.TraceExceptionInfo(ex3);
					}
					throw;
				}
			}
			if (this.m_conSignature != this.m_connection.m_conSignature)
			{
				if (this.m_opsErrCtx != IntPtr.Zero)
				{
					try
					{
						OpsErr.FreeCtx(ref this.m_opsErrCtx);
					}
					catch (Exception ex4)
					{
						if (OraTrace.m_TraceLevel != 0U)
						{
							OraTrace.TraceExceptionInfo(ex4);
						}
					}
					this.m_opsErrCtx = IntPtr.Zero;
				}
				this.m_fetchMeta = true;
				this.m_conSignature = this.m_connection.m_conSignature;
			}
		}

		// Token: 0x060004B0 RID: 1200 RVA: 0x00036A74 File Offset: 0x00035A74
		private bool FireRowsCopiedEvent(long rowsCopied)
		{
			OracleRowsCopiedEventArgs oracleRowsCopiedEventArgs;
			try
			{
				oracleRowsCopiedEventArgs = new OracleRowsCopiedEventArgs(rowsCopied);
				this.m_insideRowsCopiedEvent = true;
				this.OnRowsCopied(oracleRowsCopiedEventArgs);
			}
			finally
			{
				this.m_insideRowsCopiedEvent = false;
			}
			return oracleRowsCopiedEventArgs.Abort;
		}

		// Token: 0x060004B1 RID: 1201 RVA: 0x00036AB8 File Offset: 0x00035AB8
		internal void OnRowsCopied(OracleRowsCopiedEventArgs eventArgs)
		{
			if (this.m_rowsCopiedEventHandler != null)
			{
				this.m_rowsCopiedEventHandler(this, eventArgs);
			}
		}

		// Token: 0x060004B2 RID: 1202 RVA: 0x00036ACF File Offset: 0x00035ACF
		private static void OnTimeElapsed(object source, ElapsedEventArgs e)
		{
			OracleBulkCopy.m_timeElapsed = true;
		}

		// Token: 0x04000304 RID: 772
		private const int INTERNAL_BATCH_SIZE = 10000;

		// Token: 0x04000305 RID: 773
		private const int LONG_MAX_SIZE = 2147483647;

		// Token: 0x04000306 RID: 774
		private const int DEFAULT_TIMEOUT = 30;

		// Token: 0x04000307 RID: 775
		private const int ONE_SECOND = 1000;

		// Token: 0x04000308 RID: 776
		private unsafe OPOBulkCopyValCtx* m_pOpoBulkCopyValCtx;

		// Token: 0x04000309 RID: 777
		private unsafe OPOBulkCopyColCtx* m_pBlkColCtx;

		// Token: 0x0400030A RID: 778
		private IntPtr m_opsSqlCtx;

		// Token: 0x0400030B RID: 779
		private IntPtr m_opsConCtx;

		// Token: 0x0400030C RID: 780
		private IntPtr m_opsErrCtx;

		// Token: 0x0400030D RID: 781
		private int m_batchSize;

		// Token: 0x0400030E RID: 782
		private OracleBulkCopyOptions m_bulkCopyOptions;

		// Token: 0x0400030F RID: 783
		private int m_timeout;

		// Token: 0x04000310 RID: 784
		private OracleBulkCopyColumnMappingCollection m_columnMappings;

		// Token: 0x04000311 RID: 785
		private OracleBulkCopyColumnMappingCollection m_internalColumnMappings;

		// Token: 0x04000312 RID: 786
		private OracleConnection m_connection;

		// Token: 0x04000313 RID: 787
		private string m_destinationTableName;

		// Token: 0x04000314 RID: 788
		private string m_destinationPartitionName;

		// Token: 0x04000315 RID: 789
		private int m_notifyAfter;

		// Token: 0x04000316 RID: 790
		private int m_badRowNumber;

		// Token: 0x04000317 RID: 791
		private int m_badColNumber;

		// Token: 0x04000318 RID: 792
		private bool m_ownConnection;

		// Token: 0x04000319 RID: 793
		private bool m_insideRowsCopiedEvent;

		// Token: 0x0400031A RID: 794
		private bool m_fetchMeta;

		// Token: 0x0400031B RID: 795
		private bool m_OptimizedPathForOraDataReader;

		// Token: 0x0400031C RID: 796
		private OracleRowsCopiedEventHandler m_rowsCopiedEventHandler;

		// Token: 0x0400031D RID: 797
		private OracleTransaction m_internalTransaction;

		// Token: 0x0400031E RID: 798
		private DataRowState m_rowState;

		// Token: 0x0400031F RID: 799
		private int m_rowsCopied;

		// Token: 0x04000320 RID: 800
		private int m_conSignature;

		// Token: 0x04000321 RID: 801
		private int m_rowSize;

		// Token: 0x04000322 RID: 802
		private bool m_disposed;

		// Token: 0x04000323 RID: 803
		private static bool m_timeElapsed;

		// Token: 0x04000324 RID: 804
		private object m_dataSource;

		// Token: 0x04000325 RID: 805
		private IEnumerator m_rowEnumerator;

		// Token: 0x04000326 RID: 806
		private DataRow m_currentRow;

		// Token: 0x04000327 RID: 807
		private int m_srcColumnCount;

		// Token: 0x04000328 RID: 808
		private int m_dstColumnCount;

		// Token: 0x04000329 RID: 809
		private OracleBulkCopy.SourceType m_sourceType;

		// Token: 0x0400032A RID: 810
		private static int COL_NULLIND_SIZE = 2;

		// Token: 0x0400032B RID: 811
		private static int COL_LEN_SIZE = 4;

		// Token: 0x0400032C RID: 812
		private int COL_HEADER_SIZE = OracleBulkCopy.COL_NULLIND_SIZE + OracleBulkCopy.COL_LEN_SIZE;

		// Token: 0x0400032D RID: 813
		private int SIZE_OF_CHAR = 2;

		// Token: 0x0400032E RID: 814
		private int SIZE_OF_PTR = sizeof(IntPtr);

		// Token: 0x02000063 RID: 99
		private enum SourceType
		{
			// Token: 0x04000330 RID: 816
			Unspecified,
			// Token: 0x04000331 RID: 817
			IDataReader,
			// Token: 0x04000332 RID: 818
			OracleDataReader,
			// Token: 0x04000333 RID: 819
			DataTable,
			// Token: 0x04000334 RID: 820
			RowArray
		}
	}
}
