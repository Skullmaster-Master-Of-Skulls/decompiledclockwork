using System;
using System.Collections;
using System.ComponentModel;
using System.Data.Common;
using System.Data.ProviderBase;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.SqlServer.Server;

namespace System.Data.SqlClient
{
	// Token: 0x020001C5 RID: 453
	public class SqlDataReader : DbDataReader, IDataReader, IDisposable, IDataRecord
	{
		// Token: 0x06001BC5 RID: 7109 RVA: 0x000C19F0 File Offset: 0x000C0DF0
		internal SqlDataReader(SqlCommand command, CommandBehavior behavior)
		{
			this._command = command;
			this._commandBehavior = behavior;
			if (this._command != null)
			{
				this._defaultTimeoutMilliseconds = (long)command.CommandTimeout * 1000L;
				this._connection = command.Connection;
				if (this._connection != null)
				{
					this._statistics = this._connection.Statistics;
					this._typeSystem = this._connection.TypeSystem;
				}
			}
			this._sharedState._dataReady = false;
			this._metaDataConsumed = false;
			this._hasRows = false;
			this._browseModeInfoConsumed = false;
			this._currentStream = null;
			this._currentTextReader = null;
			this._cancelAsyncOnCloseTokenSource = new CancellationTokenSource();
			this._cancelAsyncOnCloseToken = this._cancelAsyncOnCloseTokenSource.Token;
			this._columnDataCharsIndex = -1;
		}

		// Token: 0x17000444 RID: 1092
		// (set) Token: 0x06001BC6 RID: 7110 RVA: 0x000C1AD8 File Offset: 0x000C0ED8
		internal bool BrowseModeInfoConsumed
		{
			set
			{
				this._browseModeInfoConsumed = value;
			}
		}

		// Token: 0x17000445 RID: 1093
		// (get) Token: 0x06001BC7 RID: 7111 RVA: 0x000C1AEC File Offset: 0x000C0EEC
		internal SqlCommand Command
		{
			get
			{
				return this._command;
			}
		}

		// Token: 0x17000446 RID: 1094
		// (get) Token: 0x06001BC8 RID: 7112 RVA: 0x000C1B00 File Offset: 0x000C0F00
		protected SqlConnection Connection
		{
			get
			{
				return this._connection;
			}
		}

		// Token: 0x17000447 RID: 1095
		// (get) Token: 0x06001BC9 RID: 7113 RVA: 0x000C1B14 File Offset: 0x000C0F14
		public override int Depth
		{
			get
			{
				if (this.IsClosed)
				{
					throw ADP.DataReaderClosed("Depth");
				}
				return 0;
			}
		}

		// Token: 0x17000448 RID: 1096
		// (get) Token: 0x06001BCA RID: 7114 RVA: 0x000C1B38 File Offset: 0x000C0F38
		public override int FieldCount
		{
			get
			{
				if (this.IsClosed)
				{
					throw ADP.DataReaderClosed("FieldCount");
				}
				if (this._currentTask != null)
				{
					throw ADP.AsyncOperationPending();
				}
				if (this.MetaData == null)
				{
					return 0;
				}
				return this._metaData.Length;
			}
		}

		// Token: 0x17000449 RID: 1097
		// (get) Token: 0x06001BCB RID: 7115 RVA: 0x000C1B7C File Offset: 0x000C0F7C
		public override bool HasRows
		{
			get
			{
				if (this.IsClosed)
				{
					throw ADP.DataReaderClosed("HasRows");
				}
				if (this._currentTask != null)
				{
					throw ADP.AsyncOperationPending();
				}
				return this._hasRows;
			}
		}

		// Token: 0x1700044A RID: 1098
		// (get) Token: 0x06001BCC RID: 7116 RVA: 0x000C1BB0 File Offset: 0x000C0FB0
		public override bool IsClosed
		{
			get
			{
				return this._isClosed;
			}
		}

		// Token: 0x1700044B RID: 1099
		// (get) Token: 0x06001BCD RID: 7117 RVA: 0x000C1BC4 File Offset: 0x000C0FC4
		// (set) Token: 0x06001BCE RID: 7118 RVA: 0x000C1BD8 File Offset: 0x000C0FD8
		internal bool IsInitialized
		{
			get
			{
				return this._isInitialized;
			}
			set
			{
				this._isInitialized = value;
			}
		}

		// Token: 0x06001BCF RID: 7119 RVA: 0x000C1BEC File Offset: 0x000C0FEC
		internal long ColumnDataBytesRemaining()
		{
			if (-1L == this._sharedState._columnDataBytesRemaining)
			{
				this._sharedState._columnDataBytesRemaining = (long)this._parser.PlpBytesLeft(this._stateObj);
			}
			return this._sharedState._columnDataBytesRemaining;
		}

		// Token: 0x1700044C RID: 1100
		// (get) Token: 0x06001BD0 RID: 7120 RVA: 0x000C1C30 File Offset: 0x000C1030
		internal _SqlMetaDataSet MetaData
		{
			get
			{
				if (this.IsClosed)
				{
					throw ADP.DataReaderClosed("MetaData");
				}
				if (this._metaData == null && !this._metaDataConsumed)
				{
					if (this._currentTask != null)
					{
						throw SQL.PendingBeginXXXExists();
					}
					RuntimeHelpers.PrepareConstrainedRegions();
					try
					{
						if (!this.TryConsumeMetaData())
						{
							throw SQL.SynchronousCallMayNotPend();
						}
					}
					catch (OutOfMemoryException e)
					{
						this._isClosed = true;
						if (this._connection != null)
						{
							this._connection.Abort(e);
						}
						throw;
					}
					catch (StackOverflowException e2)
					{
						this._isClosed = true;
						if (this._connection != null)
						{
							this._connection.Abort(e2);
						}
						throw;
					}
					catch (ThreadAbortException e3)
					{
						this._isClosed = true;
						if (this._connection != null)
						{
							this._connection.Abort(e3);
						}
						throw;
					}
				}
				return this._metaData;
			}
		}

		// Token: 0x06001BD1 RID: 7121 RVA: 0x000C1D3C File Offset: 0x000C113C
		internal virtual SmiExtendedMetaData[] GetInternalSmiMetaData()
		{
			SmiExtendedMetaData[] array = null;
			_SqlMetaDataSet metaData = this.MetaData;
			if (metaData != null && 0 < metaData.Length)
			{
				array = new SmiExtendedMetaData[metaData.visibleColumns];
				for (int i = 0; i < metaData.Length; i++)
				{
					_SqlMetaData sqlMetaData = metaData[i];
					if (!sqlMetaData.isHidden)
					{
						SqlCollation collation = sqlMetaData.collation;
						string typeSpecificNamePart = null;
						string typeSpecificNamePart2 = null;
						string typeSpecificNamePart3 = null;
						if (SqlDbType.Xml == sqlMetaData.type)
						{
							typeSpecificNamePart = sqlMetaData.xmlSchemaCollectionDatabase;
							typeSpecificNamePart2 = sqlMetaData.xmlSchemaCollectionOwningSchema;
							typeSpecificNamePart3 = sqlMetaData.xmlSchemaCollectionName;
						}
						else if (SqlDbType.Udt == sqlMetaData.type)
						{
							this.Connection.CheckGetExtendedUDTInfo(sqlMetaData, true);
							typeSpecificNamePart = sqlMetaData.udtDatabaseName;
							typeSpecificNamePart2 = sqlMetaData.udtSchemaName;
							typeSpecificNamePart3 = sqlMetaData.udtTypeName;
						}
						int num = sqlMetaData.length;
						if (num > 8000)
						{
							num = -1;
						}
						else if (SqlDbType.NChar == sqlMetaData.type || SqlDbType.NVarChar == sqlMetaData.type)
						{
							num /= 2;
						}
						array[i] = new SmiQueryMetaData(sqlMetaData.type, (long)num, sqlMetaData.precision, sqlMetaData.scale, (long)((collation != null) ? collation.LCID : this._defaultLCID), (collation != null) ? collation.SqlCompareOptions : SqlCompareOptions.None, sqlMetaData.udtType, false, null, null, sqlMetaData.column, typeSpecificNamePart, typeSpecificNamePart2, typeSpecificNamePart3, sqlMetaData.isNullable, sqlMetaData.serverName, sqlMetaData.catalogName, sqlMetaData.schemaName, sqlMetaData.tableName, sqlMetaData.baseColumn, sqlMetaData.isKey, sqlMetaData.isIdentity, sqlMetaData.updatability == 0, sqlMetaData.isExpression, sqlMetaData.isDifferentName, sqlMetaData.isHidden);
					}
				}
			}
			return array;
		}

		// Token: 0x1700044D RID: 1101
		// (get) Token: 0x06001BD2 RID: 7122 RVA: 0x000C1EE0 File Offset: 0x000C12E0
		public override int RecordsAffected
		{
			get
			{
				if (this._command != null)
				{
					return this._command.InternalRecordsAffected;
				}
				return this._recordsAffected;
			}
		}

		// Token: 0x1700044E RID: 1102
		// (set) Token: 0x06001BD3 RID: 7123 RVA: 0x000C1F08 File Offset: 0x000C1308
		internal string ResetOptionsString
		{
			set
			{
				this._resetOptionsString = value;
			}
		}

		// Token: 0x1700044F RID: 1103
		// (get) Token: 0x06001BD4 RID: 7124 RVA: 0x000C1F1C File Offset: 0x000C131C
		private SqlStatistics Statistics
		{
			get
			{
				return this._statistics;
			}
		}

		// Token: 0x17000450 RID: 1104
		// (get) Token: 0x06001BD5 RID: 7125 RVA: 0x000C1F30 File Offset: 0x000C1330
		// (set) Token: 0x06001BD6 RID: 7126 RVA: 0x000C1F44 File Offset: 0x000C1344
		internal MultiPartTableName[] TableNames
		{
			get
			{
				return this._tableNames;
			}
			set
			{
				this._tableNames = value;
			}
		}

		// Token: 0x17000451 RID: 1105
		// (get) Token: 0x06001BD7 RID: 7127 RVA: 0x000C1F58 File Offset: 0x000C1358
		public override int VisibleFieldCount
		{
			get
			{
				if (this.IsClosed)
				{
					throw ADP.DataReaderClosed("VisibleFieldCount");
				}
				_SqlMetaDataSet metaData = this.MetaData;
				if (metaData == null)
				{
					return 0;
				}
				return metaData.visibleColumns;
			}
		}

		// Token: 0x17000452 RID: 1106
		public override object this[int i]
		{
			get
			{
				return this.GetValue(i);
			}
		}

		// Token: 0x17000453 RID: 1107
		public override object this[string name]
		{
			get
			{
				return this.GetValue(this.GetOrdinal(name));
			}
		}

		// Token: 0x06001BDA RID: 7130 RVA: 0x000C1FBC File Offset: 0x000C13BC
		internal void Bind(TdsParserStateObject stateObj)
		{
			stateObj.Owner = this;
			this._stateObj = stateObj;
			this._parser = stateObj.Parser;
			this._defaultLCID = this._parser.DefaultLCID;
		}

		// Token: 0x06001BDB RID: 7131 RVA: 0x000C1FF4 File Offset: 0x000C13F4
		internal DataTable BuildSchemaTable()
		{
			_SqlMetaDataSet metaData = this.MetaData;
			DataTable dataTable = new DataTable("SchemaTable");
			dataTable.Locale = CultureInfo.InvariantCulture;
			dataTable.MinimumCapacity = metaData.Length;
			DataColumn column = new DataColumn(SchemaTableColumn.ColumnName, typeof(string));
			DataColumn dataColumn = new DataColumn(SchemaTableColumn.ColumnOrdinal, typeof(int));
			DataColumn column2 = new DataColumn(SchemaTableColumn.ColumnSize, typeof(int));
			DataColumn column3 = new DataColumn(SchemaTableColumn.NumericPrecision, typeof(short));
			DataColumn column4 = new DataColumn(SchemaTableColumn.NumericScale, typeof(short));
			DataColumn column5 = new DataColumn(SchemaTableColumn.DataType, typeof(Type));
			DataColumn column6 = new DataColumn(SchemaTableOptionalColumn.ProviderSpecificDataType, typeof(Type));
			DataColumn column7 = new DataColumn(SchemaTableColumn.NonVersionedProviderType, typeof(int));
			DataColumn column8 = new DataColumn(SchemaTableColumn.ProviderType, typeof(int));
			DataColumn dataColumn2 = new DataColumn(SchemaTableColumn.IsLong, typeof(bool));
			DataColumn column9 = new DataColumn(SchemaTableColumn.AllowDBNull, typeof(bool));
			DataColumn column10 = new DataColumn(SchemaTableOptionalColumn.IsReadOnly, typeof(bool));
			DataColumn column11 = new DataColumn(SchemaTableOptionalColumn.IsRowVersion, typeof(bool));
			DataColumn column12 = new DataColumn(SchemaTableColumn.IsUnique, typeof(bool));
			DataColumn column13 = new DataColumn(SchemaTableColumn.IsKey, typeof(bool));
			DataColumn column14 = new DataColumn(SchemaTableOptionalColumn.IsAutoIncrement, typeof(bool));
			DataColumn column15 = new DataColumn(SchemaTableOptionalColumn.IsHidden, typeof(bool));
			DataColumn column16 = new DataColumn(SchemaTableOptionalColumn.BaseCatalogName, typeof(string));
			DataColumn column17 = new DataColumn(SchemaTableColumn.BaseSchemaName, typeof(string));
			DataColumn column18 = new DataColumn(SchemaTableColumn.BaseTableName, typeof(string));
			DataColumn column19 = new DataColumn(SchemaTableColumn.BaseColumnName, typeof(string));
			DataColumn column20 = new DataColumn(SchemaTableOptionalColumn.BaseServerName, typeof(string));
			DataColumn column21 = new DataColumn(SchemaTableColumn.IsAliased, typeof(bool));
			DataColumn column22 = new DataColumn(SchemaTableColumn.IsExpression, typeof(bool));
			DataColumn column23 = new DataColumn("IsIdentity", typeof(bool));
			DataColumn column24 = new DataColumn("DataTypeName", typeof(string));
			DataColumn column25 = new DataColumn("UdtAssemblyQualifiedName", typeof(string));
			DataColumn column26 = new DataColumn("XmlSchemaCollectionDatabase", typeof(string));
			DataColumn column27 = new DataColumn("XmlSchemaCollectionOwningSchema", typeof(string));
			DataColumn column28 = new DataColumn("XmlSchemaCollectionName", typeof(string));
			DataColumn column29 = new DataColumn("IsColumnSet", typeof(bool));
			dataColumn.DefaultValue = 0;
			dataColumn2.DefaultValue = false;
			DataColumnCollection columns = dataTable.Columns;
			columns.Add(column);
			columns.Add(dataColumn);
			columns.Add(column2);
			columns.Add(column3);
			columns.Add(column4);
			columns.Add(column12);
			columns.Add(column13);
			columns.Add(column20);
			columns.Add(column16);
			columns.Add(column19);
			columns.Add(column17);
			columns.Add(column18);
			columns.Add(column5);
			columns.Add(column9);
			columns.Add(column8);
			columns.Add(column21);
			columns.Add(column22);
			columns.Add(column23);
			columns.Add(column14);
			columns.Add(column11);
			columns.Add(column15);
			columns.Add(dataColumn2);
			columns.Add(column10);
			columns.Add(column6);
			columns.Add(column24);
			columns.Add(column26);
			columns.Add(column27);
			columns.Add(column28);
			columns.Add(column25);
			columns.Add(column7);
			columns.Add(column29);
			for (int i = 0; i < metaData.Length; i++)
			{
				_SqlMetaData sqlMetaData = metaData[i];
				DataRow dataRow = dataTable.NewRow();
				dataRow[column] = sqlMetaData.column;
				dataRow[dataColumn] = sqlMetaData.ordinal;
				if (sqlMetaData.cipherMD != null)
				{
					dataRow[column2] = ((sqlMetaData.baseTI.metaType.IsSizeInCharacters && sqlMetaData.baseTI.length != int.MaxValue) ? (sqlMetaData.baseTI.length / 2) : sqlMetaData.baseTI.length);
				}
				else
				{
					dataRow[column2] = ((sqlMetaData.metaType.IsSizeInCharacters && sqlMetaData.length != int.MaxValue) ? (sqlMetaData.length / 2) : sqlMetaData.length);
				}
				dataRow[column5] = this.GetFieldTypeInternal(sqlMetaData);
				dataRow[column6] = this.GetProviderSpecificFieldTypeInternal(sqlMetaData);
				dataRow[column7] = (int)((sqlMetaData.cipherMD != null) ? sqlMetaData.baseTI.type : sqlMetaData.type);
				dataRow[column24] = this.GetDataTypeNameInternal(sqlMetaData);
				if (this._typeSystem <= SqlConnectionString.TypeSystem.SQLServer2005 && sqlMetaData.IsNewKatmaiDateTimeType)
				{
					dataRow[column8] = SqlDbType.NVarChar;
					switch (sqlMetaData.type)
					{
					case SqlDbType.Date:
						dataRow[column2] = 10;
						break;
					case SqlDbType.Time:
						dataRow[column2] = TdsEnums.WHIDBEY_TIME_LENGTH[(int)((byte.MaxValue != sqlMetaData.scale) ? sqlMetaData.scale : sqlMetaData.metaType.Scale)];
						break;
					case SqlDbType.DateTime2:
						dataRow[column2] = TdsEnums.WHIDBEY_DATETIME2_LENGTH[(int)((byte.MaxValue != sqlMetaData.scale) ? sqlMetaData.scale : sqlMetaData.metaType.Scale)];
						break;
					case SqlDbType.DateTimeOffset:
						dataRow[column2] = TdsEnums.WHIDBEY_DATETIMEOFFSET_LENGTH[(int)((byte.MaxValue != sqlMetaData.scale) ? sqlMetaData.scale : sqlMetaData.metaType.Scale)];
						break;
					}
				}
				else if (this._typeSystem <= SqlConnectionString.TypeSystem.SQLServer2005 && sqlMetaData.IsLargeUdt)
				{
					if (this._typeSystem == SqlConnectionString.TypeSystem.SQLServer2005)
					{
						dataRow[column8] = SqlDbType.VarBinary;
					}
					else
					{
						dataRow[column8] = SqlDbType.Image;
					}
				}
				else if (this._typeSystem != SqlConnectionString.TypeSystem.SQLServer2000)
				{
					dataRow[column8] = (int)((sqlMetaData.cipherMD != null) ? sqlMetaData.baseTI.type : sqlMetaData.type);
					if (sqlMetaData.type == SqlDbType.Udt)
					{
						dataRow[column25] = sqlMetaData.udtAssemblyQualifiedName;
					}
					else if (sqlMetaData.type == SqlDbType.Xml)
					{
						dataRow[column26] = sqlMetaData.xmlSchemaCollectionDatabase;
						dataRow[column27] = sqlMetaData.xmlSchemaCollectionOwningSchema;
						dataRow[column28] = sqlMetaData.xmlSchemaCollectionName;
					}
				}
				else
				{
					dataRow[column8] = this.GetVersionedMetaType(sqlMetaData.metaType).SqlDbType;
				}
				if (sqlMetaData.cipherMD != null)
				{
					if (255 != sqlMetaData.baseTI.precision)
					{
						dataRow[column3] = sqlMetaData.baseTI.precision;
					}
					else
					{
						dataRow[column3] = sqlMetaData.baseTI.metaType.Precision;
					}
				}
				else if (255 != sqlMetaData.precision)
				{
					dataRow[column3] = sqlMetaData.precision;
				}
				else
				{
					dataRow[column3] = sqlMetaData.metaType.Precision;
				}
				if (this._typeSystem <= SqlConnectionString.TypeSystem.SQLServer2005 && sqlMetaData.IsNewKatmaiDateTimeType)
				{
					dataRow[column4] = MetaType.MetaNVarChar.Scale;
				}
				else if (sqlMetaData.cipherMD != null)
				{
					if (255 != sqlMetaData.baseTI.scale)
					{
						dataRow[column4] = sqlMetaData.baseTI.scale;
					}
					else
					{
						dataRow[column4] = sqlMetaData.baseTI.metaType.Scale;
					}
				}
				else if (255 != sqlMetaData.scale)
				{
					dataRow[column4] = sqlMetaData.scale;
				}
				else
				{
					dataRow[column4] = sqlMetaData.metaType.Scale;
				}
				dataRow[column9] = sqlMetaData.isNullable;
				if (this._browseModeInfoConsumed)
				{
					dataRow[column21] = sqlMetaData.isDifferentName;
					dataRow[column13] = sqlMetaData.isKey;
					dataRow[column15] = sqlMetaData.isHidden;
					dataRow[column22] = sqlMetaData.isExpression;
				}
				dataRow[column23] = sqlMetaData.isIdentity;
				dataRow[column14] = sqlMetaData.isIdentity;
				if (sqlMetaData.cipherMD != null)
				{
					dataRow[dataColumn2] = sqlMetaData.baseTI.metaType.IsLong;
				}
				else
				{
					dataRow[dataColumn2] = sqlMetaData.metaType.IsLong;
				}
				if (SqlDbType.Timestamp == sqlMetaData.type)
				{
					dataRow[column12] = true;
					dataRow[column11] = true;
				}
				else
				{
					dataRow[column12] = false;
					dataRow[column11] = false;
				}
				dataRow[column10] = (sqlMetaData.updatability == 0);
				dataRow[column29] = sqlMetaData.isColumnSet;
				if (!ADP.IsEmpty(sqlMetaData.serverName))
				{
					dataRow[column20] = sqlMetaData.serverName;
				}
				if (!ADP.IsEmpty(sqlMetaData.catalogName))
				{
					dataRow[column16] = sqlMetaData.catalogName;
				}
				if (!ADP.IsEmpty(sqlMetaData.schemaName))
				{
					dataRow[column17] = sqlMetaData.schemaName;
				}
				if (!ADP.IsEmpty(sqlMetaData.tableName))
				{
					dataRow[column18] = sqlMetaData.tableName;
				}
				if (!ADP.IsEmpty(sqlMetaData.baseColumn))
				{
					dataRow[column19] = sqlMetaData.baseColumn;
				}
				else if (!ADP.IsEmpty(sqlMetaData.column))
				{
					dataRow[column19] = sqlMetaData.column;
				}
				dataTable.Rows.Add(dataRow);
				dataRow.AcceptChanges();
			}
			foreach (object obj in columns)
			{
				DataColumn dataColumn3 = (DataColumn)obj;
				dataColumn3.ReadOnly = true;
			}
			return dataTable;
		}

		// Token: 0x06001BDC RID: 7132 RVA: 0x000C2AB0 File Offset: 0x000C1EB0
		internal void Cancel(int objectID)
		{
			TdsParserStateObject stateObj = this._stateObj;
			if (stateObj != null)
			{
				stateObj.Cancel(objectID);
			}
		}

		// Token: 0x06001BDD RID: 7133 RVA: 0x000C2AD0 File Offset: 0x000C1ED0
		private bool TryCleanPartialRead()
		{
			if (this._stateObj._partialHeaderBytesRead > 0 && !this._stateObj.TryProcessHeader())
			{
				return false;
			}
			if (-1 != this._lastColumnWithDataChunkRead)
			{
				this.CloseActiveSequentialStreamAndTextReader();
			}
			if (this._sharedState._nextColumnHeaderToRead == 0)
			{
				if (!this._stateObj.Parser.TrySkipRow(this._metaData, this._stateObj))
				{
					return false;
				}
			}
			else
			{
				if (!this.TryResetBlobState())
				{
					return false;
				}
				if (!this._stateObj.Parser.TrySkipRow(this._metaData, this._sharedState._nextColumnHeaderToRead, this._stateObj))
				{
					return false;
				}
			}
			this._sharedState._dataReady = false;
			return true;
		}

		// Token: 0x06001BDE RID: 7134 RVA: 0x000C2B78 File Offset: 0x000C1F78
		private void CleanPartialReadReliable()
		{
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				bool flag = this.TryCleanPartialRead();
			}
			catch (OutOfMemoryException e)
			{
				this._isClosed = true;
				if (this._connection != null)
				{
					this._connection.Abort(e);
				}
				throw;
			}
			catch (StackOverflowException e2)
			{
				this._isClosed = true;
				if (this._connection != null)
				{
					this._connection.Abort(e2);
				}
				throw;
			}
			catch (ThreadAbortException e3)
			{
				this._isClosed = true;
				if (this._connection != null)
				{
					this._connection.Abort(e3);
				}
				throw;
			}
		}

		// Token: 0x06001BDF RID: 7135 RVA: 0x000C2C40 File Offset: 0x000C2040
		public override void Close()
		{
			SqlStatistics statistics = null;
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<sc.SqlDataReader.Close|API> %d#", this.ObjectID);
			try
			{
				statistics = SqlStatistics.StartTimer(this.Statistics);
				TdsParserStateObject stateObj = this._stateObj;
				this._cancelAsyncOnCloseTokenSource.Cancel();
				Task currentTask = this._currentTask;
				if (currentTask != null && !currentTask.IsCompleted)
				{
					try
					{
						((IAsyncResult)currentTask).AsyncWaitHandle.WaitOne();
						TaskCompletionSource<object> networkPacketTaskSource = stateObj._networkPacketTaskSource;
						if (networkPacketTaskSource != null)
						{
							((IAsyncResult)networkPacketTaskSource.Task).AsyncWaitHandle.WaitOne();
						}
					}
					catch (Exception)
					{
						this._connection.InnerConnection.DoomThisConnection();
						this._isClosed = true;
						if (stateObj != null)
						{
							TdsParserStateObject obj = stateObj;
							lock (obj)
							{
								this._stateObj = null;
								this._command = null;
								this._connection = null;
							}
						}
						throw;
					}
				}
				this.CloseActiveSequentialStreamAndTextReader();
				if (stateObj != null)
				{
					TdsParserStateObject obj2 = stateObj;
					lock (obj2)
					{
						if (this._stateObj != null)
						{
							if (this._snapshot != null)
							{
								this.PrepareForAsyncContinuation();
							}
							this.SetTimeout(this._defaultTimeoutMilliseconds);
							stateObj._syncOverAsync = true;
							if (!this.TryCloseInternal(true))
							{
								throw SQL.SynchronousCallMayNotPend();
							}
						}
					}
				}
			}
			finally
			{
				SqlStatistics.StopTimer(statistics);
				Bid.ScopeLeave(ref intPtr);
			}
		}

		// Token: 0x06001BE0 RID: 7136 RVA: 0x000C2DE4 File Offset: 0x000C21E4
		private bool TryCloseInternal(bool closeReader)
		{
			TdsParser parser = this._parser;
			TdsParserStateObject stateObj = this._stateObj;
			bool flag = this.IsCommandBehavior(CommandBehavior.CloseConnection);
			bool flag2 = false;
			bool flag3 = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			bool result;
			try
			{
				if (!this._isClosed && parser != null && stateObj != null && stateObj._pendingData && parser.State == TdsParserState.OpenLoggedIn)
				{
					if (this._altRowStatus == SqlDataReader.ALTROWSTATUS.AltRow)
					{
						this._sharedState._dataReady = true;
					}
					if (LocalAppContextSwitches.DisableHardenedQueryTimeouts)
					{
						this._stateObj._internalTimeout = false;
					}
					else
					{
						this._stateObj.SetTimeoutStateStopped();
					}
					if (this._sharedState._dataReady)
					{
						flag3 = true;
						if (!this.TryCleanPartialRead())
						{
							return false;
						}
						flag3 = false;
					}
					bool flag4;
					if (!parser.TryRun(RunBehavior.Clean, this._command, this, null, stateObj, out flag4))
					{
						return false;
					}
				}
				this.RestoreServerSettings(parser, stateObj);
				result = true;
			}
			catch (OutOfMemoryException e)
			{
				this._isClosed = true;
				flag2 = true;
				if (this._connection != null)
				{
					this._connection.Abort(e);
				}
				throw;
			}
			catch (StackOverflowException e2)
			{
				this._isClosed = true;
				flag2 = true;
				if (this._connection != null)
				{
					this._connection.Abort(e2);
				}
				throw;
			}
			catch (ThreadAbortException e3)
			{
				this._isClosed = true;
				flag2 = true;
				if (this._connection != null)
				{
					this._connection.Abort(e3);
				}
				throw;
			}
			finally
			{
				if (flag2)
				{
					this._isClosed = true;
					this._command = null;
					this._connection = null;
					this._statistics = null;
					this._stateObj = null;
					this._parser = null;
				}
				else if (closeReader)
				{
					bool isClosed = this._isClosed;
					this._isClosed = true;
					this._parser = null;
					this._stateObj = null;
					this._data = null;
					if (this._snapshot != null)
					{
						this.CleanupAfterAsyncInvocationInternal(stateObj, true);
					}
					if (this.Connection != null)
					{
						this.Connection.RemoveWeakReference(this);
					}
					RuntimeHelpers.PrepareConstrainedRegions();
					try
					{
						if (!isClosed && stateObj != null)
						{
							if (!flag3)
							{
								stateObj.CloseSession();
							}
							else if (parser != null)
							{
								parser.State = TdsParserState.Broken;
								parser.PutSession(stateObj);
								parser.Connection.BreakConnection();
							}
						}
					}
					catch (OutOfMemoryException e4)
					{
						if (this._connection != null)
						{
							this._connection.Abort(e4);
						}
						throw;
					}
					catch (StackOverflowException e5)
					{
						if (this._connection != null)
						{
							this._connection.Abort(e5);
						}
						throw;
					}
					catch (ThreadAbortException e6)
					{
						if (this._connection != null)
						{
							this._connection.Abort(e6);
						}
						throw;
					}
					bool flag5 = this.TrySetMetaData(null, false);
					this._fieldNameLookup = null;
					if (flag && this.Connection != null)
					{
						this.Connection.Close();
					}
					if (this._command != null)
					{
						this._recordsAffected = this._command.InternalRecordsAffected;
					}
					this._command = null;
					this._connection = null;
					this._statistics = null;
				}
			}
			return result;
		}

		// Token: 0x06001BE1 RID: 7137 RVA: 0x000C3140 File Offset: 0x000C2540
		internal virtual void CloseReaderFromConnection()
		{
			TdsParser parser = this._parser;
			if (parser != null && parser.State == TdsParserState.OpenLoggedIn)
			{
				this.Close();
				return;
			}
			TdsParserStateObject stateObj = this._stateObj;
			this._isClosed = true;
			this._cancelAsyncOnCloseTokenSource.Cancel();
			if (stateObj != null)
			{
				TaskCompletionSource<object> networkPacketTaskSource = stateObj._networkPacketTaskSource;
				if (networkPacketTaskSource != null)
				{
					networkPacketTaskSource.TrySetException(ADP.ClosedConnectionError());
				}
				if (this._snapshot != null)
				{
					this.CleanupAfterAsyncInvocationInternal(stateObj, false);
				}
				stateObj._syncOverAsync = true;
				stateObj.RemoveOwner();
			}
		}

		// Token: 0x06001BE2 RID: 7138 RVA: 0x000C31B8 File Offset: 0x000C25B8
		private bool TryConsumeMetaData()
		{
			while (this._parser != null && this._stateObj != null && this._stateObj._pendingData && !this._metaDataConsumed)
			{
				if (this._parser.State == TdsParserState.Broken || this._parser.State == TdsParserState.Closed)
				{
					if (this._parser.Connection != null)
					{
						this._parser.Connection.DoomThisConnection();
					}
					throw SQL.ConnectionDoomed();
				}
				bool flag;
				if (!this._parser.TryRun(RunBehavior.ReturnImmediately, this._command, this, null, this._stateObj, out flag))
				{
					return false;
				}
			}
			if (this._metaData != null)
			{
				if (this._snapshot != null && this._snapshot._metadata == this._metaData)
				{
					this._metaData = (_SqlMetaDataSet)this._metaData.Clone();
				}
				this._metaData.visibleColumns = 0;
				int[] array = new int[this._metaData.Length];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = this._metaData.visibleColumns;
					if (!this._metaData[i].isHidden)
					{
						this._metaData.visibleColumns++;
					}
				}
				this._metaData.indexMap = array;
			}
			return true;
		}

		// Token: 0x06001BE3 RID: 7139 RVA: 0x000C32F8 File Offset: 0x000C26F8
		public override string GetDataTypeName(int i)
		{
			SqlStatistics statistics = null;
			string dataTypeNameInternal;
			try
			{
				statistics = SqlStatistics.StartTimer(this.Statistics);
				this.CheckMetaDataIsReady(i, false);
				dataTypeNameInternal = this.GetDataTypeNameInternal(this._metaData[i]);
			}
			finally
			{
				SqlStatistics.StopTimer(statistics);
			}
			return dataTypeNameInternal;
		}

		// Token: 0x06001BE4 RID: 7140 RVA: 0x000C3354 File Offset: 0x000C2754
		private string GetDataTypeNameInternal(_SqlMetaData metaData)
		{
			string result;
			if (this._typeSystem <= SqlConnectionString.TypeSystem.SQLServer2005 && metaData.IsNewKatmaiDateTimeType)
			{
				result = MetaType.MetaNVarChar.TypeName;
			}
			else if (this._typeSystem <= SqlConnectionString.TypeSystem.SQLServer2005 && metaData.IsLargeUdt)
			{
				if (this._typeSystem == SqlConnectionString.TypeSystem.SQLServer2005)
				{
					result = MetaType.MetaMaxVarBinary.TypeName;
				}
				else
				{
					result = MetaType.MetaImage.TypeName;
				}
			}
			else if (this._typeSystem != SqlConnectionString.TypeSystem.SQLServer2000)
			{
				if (metaData.type == SqlDbType.Udt)
				{
					result = string.Concat(new string[]
					{
						metaData.udtDatabaseName,
						".",
						metaData.udtSchemaName,
						".",
						metaData.udtTypeName
					});
				}
				else if (metaData.cipherMD != null)
				{
					result = metaData.baseTI.metaType.TypeName;
				}
				else
				{
					result = metaData.metaType.TypeName;
				}
			}
			else
			{
				result = this.GetVersionedMetaType(metaData.metaType).TypeName;
			}
			return result;
		}

		// Token: 0x06001BE5 RID: 7141 RVA: 0x000C3458 File Offset: 0x000C2858
		internal virtual SqlBuffer.StorageType GetVariantInternalStorageType(int i)
		{
			return this._data[i].VariantInternalStorageType;
		}

		// Token: 0x06001BE6 RID: 7142 RVA: 0x000C3474 File Offset: 0x000C2874
		public override IEnumerator GetEnumerator()
		{
			return new DbEnumerator(this, this.IsCommandBehavior(CommandBehavior.CloseConnection));
		}

		// Token: 0x06001BE7 RID: 7143 RVA: 0x000C3490 File Offset: 0x000C2890
		public override Type GetFieldType(int i)
		{
			SqlStatistics statistics = null;
			Type fieldTypeInternal;
			try
			{
				statistics = SqlStatistics.StartTimer(this.Statistics);
				this.CheckMetaDataIsReady(i, false);
				fieldTypeInternal = this.GetFieldTypeInternal(this._metaData[i]);
			}
			finally
			{
				SqlStatistics.StopTimer(statistics);
			}
			return fieldTypeInternal;
		}

		// Token: 0x06001BE8 RID: 7144 RVA: 0x000C34EC File Offset: 0x000C28EC
		private Type GetFieldTypeInternal(_SqlMetaData metaData)
		{
			Type result;
			if (this._typeSystem <= SqlConnectionString.TypeSystem.SQLServer2005 && metaData.IsNewKatmaiDateTimeType)
			{
				result = MetaType.MetaNVarChar.ClassType;
			}
			else if (this._typeSystem <= SqlConnectionString.TypeSystem.SQLServer2005 && metaData.IsLargeUdt)
			{
				if (this._typeSystem == SqlConnectionString.TypeSystem.SQLServer2005)
				{
					result = MetaType.MetaMaxVarBinary.ClassType;
				}
				else
				{
					result = MetaType.MetaImage.ClassType;
				}
			}
			else if (this._typeSystem != SqlConnectionString.TypeSystem.SQLServer2000)
			{
				if (metaData.type == SqlDbType.Udt)
				{
					this.Connection.CheckGetExtendedUDTInfo(metaData, false);
					result = metaData.udtType;
				}
				else if (metaData.cipherMD != null)
				{
					result = metaData.baseTI.metaType.ClassType;
				}
				else
				{
					result = metaData.metaType.ClassType;
				}
			}
			else
			{
				result = this.GetVersionedMetaType(metaData.metaType).ClassType;
			}
			return result;
		}

		// Token: 0x06001BE9 RID: 7145 RVA: 0x000C35C8 File Offset: 0x000C29C8
		internal virtual int GetLocaleId(int i)
		{
			_SqlMetaData sqlMetaData = this.MetaData[i];
			int result;
			if (sqlMetaData.cipherMD != null)
			{
				if (sqlMetaData.baseTI.collation != null)
				{
					result = sqlMetaData.baseTI.collation.LCID;
				}
				else
				{
					result = 0;
				}
			}
			else if (sqlMetaData.collation != null)
			{
				result = sqlMetaData.collation.LCID;
			}
			else
			{
				result = 0;
			}
			return result;
		}

		// Token: 0x06001BEA RID: 7146 RVA: 0x000C3628 File Offset: 0x000C2A28
		public override string GetName(int i)
		{
			this.CheckMetaDataIsReady(i, false);
			return this._metaData[i].column;
		}

		// Token: 0x06001BEB RID: 7147 RVA: 0x000C3650 File Offset: 0x000C2A50
		public override Type GetProviderSpecificFieldType(int i)
		{
			SqlStatistics statistics = null;
			Type providerSpecificFieldTypeInternal;
			try
			{
				statistics = SqlStatistics.StartTimer(this.Statistics);
				this.CheckMetaDataIsReady(i, false);
				providerSpecificFieldTypeInternal = this.GetProviderSpecificFieldTypeInternal(this._metaData[i]);
			}
			finally
			{
				SqlStatistics.StopTimer(statistics);
			}
			return providerSpecificFieldTypeInternal;
		}

		// Token: 0x06001BEC RID: 7148 RVA: 0x000C36AC File Offset: 0x000C2AAC
		private Type GetProviderSpecificFieldTypeInternal(_SqlMetaData metaData)
		{
			Type result;
			if (this._typeSystem <= SqlConnectionString.TypeSystem.SQLServer2005 && metaData.IsNewKatmaiDateTimeType)
			{
				result = MetaType.MetaNVarChar.SqlType;
			}
			else if (this._typeSystem <= SqlConnectionString.TypeSystem.SQLServer2005 && metaData.IsLargeUdt)
			{
				if (this._typeSystem == SqlConnectionString.TypeSystem.SQLServer2005)
				{
					result = MetaType.MetaMaxVarBinary.SqlType;
				}
				else
				{
					result = MetaType.MetaImage.SqlType;
				}
			}
			else if (this._typeSystem != SqlConnectionString.TypeSystem.SQLServer2000)
			{
				if (metaData.type == SqlDbType.Udt)
				{
					this.Connection.CheckGetExtendedUDTInfo(metaData, false);
					result = metaData.udtType;
				}
				else if (metaData.cipherMD != null)
				{
					result = metaData.baseTI.metaType.SqlType;
				}
				else
				{
					result = metaData.metaType.SqlType;
				}
			}
			else
			{
				result = this.GetVersionedMetaType(metaData.metaType).SqlType;
			}
			return result;
		}

		// Token: 0x06001BED RID: 7149 RVA: 0x000C3788 File Offset: 0x000C2B88
		public override int GetOrdinal(string name)
		{
			SqlStatistics statistics = null;
			int ordinal;
			try
			{
				statistics = SqlStatistics.StartTimer(this.Statistics);
				if (this._fieldNameLookup == null)
				{
					this.CheckMetaDataIsReady();
					this._fieldNameLookup = new FieldNameLookup(this, this._defaultLCID);
				}
				ordinal = this._fieldNameLookup.GetOrdinal(name);
			}
			finally
			{
				SqlStatistics.StopTimer(statistics);
			}
			return ordinal;
		}

		// Token: 0x06001BEE RID: 7150 RVA: 0x000C37F8 File Offset: 0x000C2BF8
		public override object GetProviderSpecificValue(int i)
		{
			return this.GetSqlValue(i);
		}

		// Token: 0x06001BEF RID: 7151 RVA: 0x000C380C File Offset: 0x000C2C0C
		public override int GetProviderSpecificValues(object[] values)
		{
			return this.GetSqlValues(values);
		}

		// Token: 0x06001BF0 RID: 7152 RVA: 0x000C3820 File Offset: 0x000C2C20
		public override DataTable GetSchemaTable()
		{
			SqlStatistics statistics = null;
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<sc.SqlDataReader.GetSchemaTable|API> %d#", this.ObjectID);
			DataTable result;
			try
			{
				statistics = SqlStatistics.StartTimer(this.Statistics);
				if ((this._metaData == null || this._metaData.schemaTable == null) && this.MetaData != null)
				{
					this._metaData.schemaTable = this.BuildSchemaTable();
				}
				if (this._metaData != null)
				{
					result = this._metaData.schemaTable;
				}
				else
				{
					result = null;
				}
			}
			finally
			{
				SqlStatistics.StopTimer(statistics);
				Bid.ScopeLeave(ref intPtr);
			}
			return result;
		}

		// Token: 0x06001BF1 RID: 7153 RVA: 0x000C38C0 File Offset: 0x000C2CC0
		public override bool GetBoolean(int i)
		{
			this.ReadColumn(i, true, false);
			return this._data[i].Boolean;
		}

		// Token: 0x06001BF2 RID: 7154 RVA: 0x000C38E4 File Offset: 0x000C2CE4
		public virtual XmlReader GetXmlReader(int i)
		{
			this.CheckDataIsReady(i, false, false, "GetXmlReader");
			MetaType metaType = this._metaData[i].metaType;
			if (metaType.SqlDbType != SqlDbType.Xml)
			{
				throw SQL.XmlReaderNotSupportOnColumnType(this._metaData[i].column);
			}
			if (this.IsCommandBehavior(CommandBehavior.SequentialAccess))
			{
				this._currentStream = new SqlSequentialStream(this, i);
				this._lastColumnWithDataChunkRead = i;
				return SqlXml.CreateSqlXmlReader(this._currentStream, true, false);
			}
			this.ReadColumn(i, true, false);
			if (this._data[i].IsNull)
			{
				return SqlXml.CreateSqlXmlReader(new MemoryStream(new byte[0], false), true, false);
			}
			return this._data[i].SqlXml.CreateReader();
		}

		// Token: 0x06001BF3 RID: 7155 RVA: 0x000C399C File Offset: 0x000C2D9C
		public override Stream GetStream(int i)
		{
			this.CheckDataIsReady(i, false, false, "GetStream");
			if (this._metaData[i] != null && this._metaData[i].cipherMD != null)
			{
				throw SQL.StreamNotSupportOnEncryptedColumn(this._metaData[i].column);
			}
			MetaType metaType = this._metaData[i].metaType;
			if ((!metaType.IsBinType || metaType.SqlDbType == SqlDbType.Timestamp) && metaType.SqlDbType != SqlDbType.Variant)
			{
				throw SQL.StreamNotSupportOnColumnType(this._metaData[i].column);
			}
			if (metaType.SqlDbType != SqlDbType.Variant && this.IsCommandBehavior(CommandBehavior.SequentialAccess))
			{
				this._currentStream = new SqlSequentialStream(this, i);
				this._lastColumnWithDataChunkRead = i;
				return this._currentStream;
			}
			this.ReadColumn(i, true, false);
			byte[] buffer;
			if (this._data[i].IsNull)
			{
				buffer = new byte[0];
			}
			else
			{
				buffer = this._data[i].SqlBinary.Value;
			}
			return new MemoryStream(buffer, false);
		}

		// Token: 0x06001BF4 RID: 7156 RVA: 0x000C3AA4 File Offset: 0x000C2EA4
		public override byte GetByte(int i)
		{
			this.ReadColumn(i, true, false);
			return this._data[i].Byte;
		}

		// Token: 0x06001BF5 RID: 7157 RVA: 0x000C3AC8 File Offset: 0x000C2EC8
		public override long GetBytes(int i, long dataIndex, byte[] buffer, int bufferIndex, int length)
		{
			SqlStatistics statistics = null;
			long result = 0L;
			this.CheckDataIsReady(i, true, false, "GetBytes");
			MetaType metaType = this._metaData[i].metaType;
			if ((!metaType.IsLong && !metaType.IsBinType) || SqlDbType.Xml == metaType.SqlDbType)
			{
				throw SQL.NonBlobColumn(this._metaData[i].column);
			}
			try
			{
				statistics = SqlStatistics.StartTimer(this.Statistics);
				this.SetTimeout(this._defaultTimeoutMilliseconds);
				result = this.GetBytesInternal(i, dataIndex, buffer, bufferIndex, length);
				this._lastColumnWithDataChunkRead = i;
			}
			finally
			{
				SqlStatistics.StopTimer(statistics);
			}
			return result;
		}

		// Token: 0x06001BF6 RID: 7158 RVA: 0x000C3B80 File Offset: 0x000C2F80
		internal virtual long GetBytesInternal(int i, long dataIndex, byte[] buffer, int bufferIndex, int length)
		{
			if (this._currentTask != null)
			{
				throw ADP.AsyncOperationPending();
			}
			long result;
			if (!this.TryGetBytesInternal(i, dataIndex, buffer, bufferIndex, length, out result))
			{
				throw SQL.SynchronousCallMayNotPend();
			}
			return result;
		}

		// Token: 0x06001BF7 RID: 7159 RVA: 0x000C3BB8 File Offset: 0x000C2FB8
		private bool TryGetBytesInternal(int i, long dataIndex, byte[] buffer, int bufferIndex, int length, out long remaining)
		{
			remaining = 0L;
			RuntimeHelpers.PrepareConstrainedRegions();
			bool result;
			try
			{
				int num = 0;
				if (this.IsCommandBehavior(CommandBehavior.SequentialAccess))
				{
					if (this._metaData[i] != null && this._metaData[i].cipherMD != null)
					{
						throw SQL.SequentialAccessNotSupportedOnEncryptedColumn(this._metaData[i].column);
					}
					if (this._sharedState._nextColumnHeaderToRead <= i && !this.TryReadColumnHeader(i))
					{
						result = false;
					}
					else
					{
						if (this._data[i] != null && this._data[i].IsNull)
						{
							throw new SqlNullValueException();
						}
						if (-1L == this._sharedState._columnDataBytesRemaining && this._metaData[i].metaType.IsPlp)
						{
							ulong columnDataBytesRemaining;
							if (!this._parser.TryPlpBytesLeft(this._stateObj, out columnDataBytesRemaining))
							{
								return false;
							}
							this._sharedState._columnDataBytesRemaining = (long)columnDataBytesRemaining;
						}
						if (this._sharedState._columnDataBytesRemaining == 0L)
						{
							result = true;
						}
						else if (buffer == null)
						{
							if (this._metaData[i].metaType.IsPlp)
							{
								remaining = (long)this._parser.PlpBytesTotalLength(this._stateObj);
								result = true;
							}
							else
							{
								remaining = this._sharedState._columnDataBytesRemaining;
								result = true;
							}
						}
						else
						{
							if (dataIndex < 0L)
							{
								throw ADP.NegativeParameter("dataIndex");
							}
							if (dataIndex < this._columnDataBytesRead)
							{
								throw ADP.NonSeqByteAccess(dataIndex, this._columnDataBytesRead, "GetBytes");
							}
							long num2 = dataIndex - this._columnDataBytesRead;
							if (num2 > this._sharedState._columnDataBytesRemaining && !this._metaData[i].metaType.IsPlp)
							{
								result = true;
							}
							else
							{
								if (bufferIndex < 0 || bufferIndex >= buffer.Length)
								{
									throw ADP.InvalidDestinationBufferIndex(buffer.Length, bufferIndex, "bufferIndex");
								}
								if (length + bufferIndex > buffer.Length)
								{
									throw ADP.InvalidBufferSizeOrIndex(length, bufferIndex);
								}
								if (length < 0)
								{
									throw ADP.InvalidDataLength((long)length);
								}
								if (num2 > 0L)
								{
									if (this._metaData[i].metaType.IsPlp)
									{
										ulong num3;
										if (!this._parser.TrySkipPlpValue((ulong)num2, this._stateObj, out num3))
										{
											return false;
										}
										this._columnDataBytesRead += (long)num3;
									}
									else
									{
										if (!this._stateObj.TrySkipLongBytes(num2))
										{
											return false;
										}
										this._columnDataBytesRead += num2;
										this._sharedState._columnDataBytesRemaining -= num2;
									}
								}
								int num4;
								bool flag = this.TryGetBytesInternalSequential(i, buffer, bufferIndex, length, out num4);
								remaining = (long)num4;
								result = flag;
							}
						}
					}
				}
				else
				{
					if (dataIndex < 0L)
					{
						throw ADP.NegativeParameter("dataIndex");
					}
					if (dataIndex > 2147483647L)
					{
						throw ADP.InvalidSourceBufferIndex(num, dataIndex, "dataIndex");
					}
					int num5 = (int)dataIndex;
					byte[] array;
					if (this._metaData[i].metaType.IsBinType)
					{
						array = this.GetSqlBinary(i).Value;
					}
					else
					{
						SqlString sqlString = this.GetSqlString(i);
						if (this._metaData[i].metaType.IsNCharType)
						{
							array = sqlString.GetUnicodeBytes();
						}
						else
						{
							array = sqlString.GetNonUnicodeBytes();
						}
					}
					num = array.Length;
					if (buffer == null)
					{
						remaining = (long)num;
						result = true;
					}
					else if (num5 < 0 || num5 >= num)
					{
						result = true;
					}
					else
					{
						try
						{
							if (num5 < num)
							{
								if (num5 + length > num)
								{
									num -= num5;
								}
								else
								{
									num = length;
								}
							}
							Array.Copy(array, num5, buffer, bufferIndex, num);
						}
						catch (Exception e)
						{
							if (!ADP.IsCatchableExceptionType(e))
							{
								throw;
							}
							num = array.Length;
							if (length < 0)
							{
								throw ADP.InvalidDataLength((long)length);
							}
							if (bufferIndex < 0 || bufferIndex >= buffer.Length)
							{
								throw ADP.InvalidDestinationBufferIndex(buffer.Length, bufferIndex, "bufferIndex");
							}
							if (num + bufferIndex > buffer.Length)
							{
								throw ADP.InvalidBufferSizeOrIndex(num, bufferIndex);
							}
							throw;
						}
						remaining = (long)num;
						result = true;
					}
				}
			}
			catch (OutOfMemoryException e2)
			{
				this._isClosed = true;
				if (this._connection != null)
				{
					this._connection.Abort(e2);
				}
				throw;
			}
			catch (StackOverflowException e3)
			{
				this._isClosed = true;
				if (this._connection != null)
				{
					this._connection.Abort(e3);
				}
				throw;
			}
			catch (ThreadAbortException e4)
			{
				this._isClosed = true;
				if (this._connection != null)
				{
					this._connection.Abort(e4);
				}
				throw;
			}
			return result;
		}

		// Token: 0x06001BF8 RID: 7160 RVA: 0x000C403C File Offset: 0x000C343C
		internal int GetBytesInternalSequential(int i, byte[] buffer, int index, int length, long? timeoutMilliseconds = null)
		{
			if (this._currentTask != null)
			{
				throw ADP.AsyncOperationPending();
			}
			SqlStatistics statistics = null;
			int result;
			try
			{
				statistics = SqlStatistics.StartTimer(this.Statistics);
				this.SetTimeout(timeoutMilliseconds ?? this._defaultTimeoutMilliseconds);
				if (!this.TryReadColumnHeader(i))
				{
					throw SQL.SynchronousCallMayNotPend();
				}
				if (!this.TryGetBytesInternalSequential(i, buffer, index, length, out result))
				{
					throw SQL.SynchronousCallMayNotPend();
				}
			}
			finally
			{
				SqlStatistics.StopTimer(statistics);
			}
			return result;
		}

		// Token: 0x06001BF9 RID: 7161 RVA: 0x000C40D4 File Offset: 0x000C34D4
		internal bool TryGetBytesInternalSequential(int i, byte[] buffer, int index, int length, out int bytesRead)
		{
			bytesRead = 0;
			RuntimeHelpers.PrepareConstrainedRegions();
			bool result;
			try
			{
				if (this._sharedState._columnDataBytesRemaining == 0L || length == 0)
				{
					bytesRead = 0;
					result = true;
				}
				else if (this._metaData[i].metaType.IsPlp)
				{
					bool flag = this._stateObj.TryReadPlpBytes(ref buffer, index, length, out bytesRead);
					this._columnDataBytesRead += (long)bytesRead;
					ulong columnDataBytesRemaining;
					if (!flag)
					{
						result = false;
					}
					else if (!this._parser.TryPlpBytesLeft(this._stateObj, out columnDataBytesRemaining))
					{
						this._sharedState._columnDataBytesRemaining = -1L;
						result = false;
					}
					else
					{
						this._sharedState._columnDataBytesRemaining = (long)columnDataBytesRemaining;
						result = true;
					}
				}
				else
				{
					int len = (int)Math.Min((long)length, this._sharedState._columnDataBytesRemaining);
					bool flag2 = this._stateObj.TryReadByteArray(buffer, index, len, out bytesRead);
					this._columnDataBytesRead += (long)bytesRead;
					this._sharedState._columnDataBytesRemaining -= (long)bytesRead;
					result = flag2;
				}
			}
			catch (OutOfMemoryException e)
			{
				this._isClosed = true;
				if (this._connection != null)
				{
					this._connection.Abort(e);
				}
				throw;
			}
			catch (StackOverflowException e2)
			{
				this._isClosed = true;
				if (this._connection != null)
				{
					this._connection.Abort(e2);
				}
				throw;
			}
			catch (ThreadAbortException e3)
			{
				this._isClosed = true;
				if (this._connection != null)
				{
					this._connection.Abort(e3);
				}
				throw;
			}
			return result;
		}

		// Token: 0x06001BFA RID: 7162 RVA: 0x000C4290 File Offset: 0x000C3690
		public override TextReader GetTextReader(int i)
		{
			this.CheckDataIsReady(i, false, false, "GetTextReader");
			MetaType metaType;
			if (this._metaData[i].cipherMD != null)
			{
				metaType = this._metaData[i].baseTI.metaType;
			}
			else
			{
				metaType = this._metaData[i].metaType;
			}
			if ((!metaType.IsCharType && metaType.SqlDbType != SqlDbType.Variant) || metaType.SqlDbType == SqlDbType.Xml)
			{
				throw SQL.TextReaderNotSupportOnColumnType(this._metaData[i].column);
			}
			if (metaType.SqlDbType == SqlDbType.Variant || !this.IsCommandBehavior(CommandBehavior.SequentialAccess))
			{
				this.ReadColumn(i, true, false);
				string s;
				if (this._data[i].IsNull)
				{
					s = string.Empty;
				}
				else
				{
					s = this._data[i].SqlString.Value;
				}
				return new StringReader(s);
			}
			if (this._metaData[i].cipherMD != null)
			{
				throw SQL.SequentialAccessNotSupportedOnEncryptedColumn(this._metaData[i].column);
			}
			Encoding encoding;
			if (metaType.IsNCharType)
			{
				encoding = SqlUnicodeEncoding.SqlUnicodeEncodingInstance;
			}
			else
			{
				encoding = this._metaData[i].encoding;
			}
			this._currentTextReader = new SqlSequentialTextReader(this, i, encoding);
			this._lastColumnWithDataChunkRead = i;
			return this._currentTextReader;
		}

		// Token: 0x06001BFB RID: 7163 RVA: 0x000C43D8 File Offset: 0x000C37D8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override char GetChar(int i)
		{
			throw ADP.NotSupported();
		}

		// Token: 0x06001BFC RID: 7164 RVA: 0x000C43EC File Offset: 0x000C37EC
		public override long GetChars(int i, long dataIndex, char[] buffer, int bufferIndex, int length)
		{
			SqlStatistics statistics = null;
			this.CheckMetaDataIsReady(i, false);
			if (this._currentTask != null)
			{
				throw ADP.AsyncOperationPending();
			}
			MetaType metaType;
			if (this._metaData[i].cipherMD != null)
			{
				metaType = this._metaData[i].baseTI.metaType;
			}
			else
			{
				metaType = this._metaData[i].metaType;
			}
			SqlDbType type;
			if (this._metaData[i].cipherMD != null)
			{
				type = this._metaData[i].baseTI.type;
			}
			else
			{
				type = this._metaData[i].type;
			}
			long result;
			try
			{
				statistics = SqlStatistics.StartTimer(this.Statistics);
				this.SetTimeout(this._defaultTimeoutMilliseconds);
				if (metaType.IsPlp && this.IsCommandBehavior(CommandBehavior.SequentialAccess))
				{
					if (length < 0)
					{
						throw ADP.InvalidDataLength((long)length);
					}
					if (this._metaData[i].cipherMD != null)
					{
						throw SQL.SequentialAccessNotSupportedOnEncryptedColumn(this._metaData[i].column);
					}
					if (bufferIndex < 0 || (buffer != null && bufferIndex >= buffer.Length))
					{
						throw ADP.InvalidDestinationBufferIndex(buffer.Length, bufferIndex, "bufferIndex");
					}
					if (buffer != null && length + bufferIndex > buffer.Length)
					{
						throw ADP.InvalidBufferSizeOrIndex(length, bufferIndex);
					}
					long num;
					if (type == SqlDbType.Xml)
					{
						try
						{
							this.CheckDataIsReady(i, true, false, "GetChars");
						}
						catch (Exception ex)
						{
							if (ADP.IsCatchableExceptionType(ex))
							{
								throw new TargetInvocationException(ex);
							}
							throw;
						}
						num = this.GetStreamingXmlChars(i, dataIndex, buffer, bufferIndex, length);
					}
					else
					{
						this.CheckDataIsReady(i, true, false, "GetChars");
						num = this.GetCharsFromPlpData(i, dataIndex, buffer, bufferIndex, length);
					}
					this._lastColumnWithDataChunkRead = i;
					result = num;
				}
				else
				{
					if (this._sharedState._nextColumnDataToRead == i + 1 && this._sharedState._nextColumnHeaderToRead == i + 1 && this._columnDataChars != null && this.IsCommandBehavior(CommandBehavior.SequentialAccess) && dataIndex < this._columnDataCharsRead)
					{
						throw ADP.NonSeqByteAccess(dataIndex, this._columnDataCharsRead, "GetChars");
					}
					if (this._columnDataCharsIndex != i)
					{
						string value = this.GetSqlString(i).Value;
						this._columnDataChars = value.ToCharArray();
						this._columnDataCharsRead = 0L;
						this._columnDataCharsIndex = i;
					}
					int num2 = this._columnDataChars.Length;
					if (dataIndex > 2147483647L)
					{
						throw ADP.InvalidSourceBufferIndex(num2, dataIndex, "dataIndex");
					}
					int num3 = (int)dataIndex;
					if (buffer == null)
					{
						result = (long)num2;
					}
					else if (num3 < 0 || num3 >= num2)
					{
						result = 0L;
					}
					else
					{
						try
						{
							if (num3 < num2)
							{
								if (num3 + length > num2)
								{
									num2 -= num3;
								}
								else
								{
									num2 = length;
								}
							}
							Array.Copy(this._columnDataChars, num3, buffer, bufferIndex, num2);
							this._columnDataCharsRead += (long)num2;
						}
						catch (Exception e)
						{
							if (!ADP.IsCatchableExceptionType(e))
							{
								throw;
							}
							num2 = this._columnDataChars.Length;
							if (length < 0)
							{
								throw ADP.InvalidDataLength((long)length);
							}
							if (bufferIndex < 0 || bufferIndex >= buffer.Length)
							{
								throw ADP.InvalidDestinationBufferIndex(buffer.Length, bufferIndex, "bufferIndex");
							}
							if (num2 + bufferIndex > buffer.Length)
							{
								throw ADP.InvalidBufferSizeOrIndex(num2, bufferIndex);
							}
							throw;
						}
						result = (long)num2;
					}
				}
			}
			finally
			{
				SqlStatistics.StopTimer(statistics);
			}
			return result;
		}

		// Token: 0x06001BFD RID: 7165 RVA: 0x000C4748 File Offset: 0x000C3B48
		private long GetCharsFromPlpData(int i, long dataIndex, char[] buffer, int bufferIndex, int length)
		{
			RuntimeHelpers.PrepareConstrainedRegions();
			long result;
			try
			{
				if (!this._metaData[i].metaType.IsCharType)
				{
					throw SQL.NonCharColumn(this._metaData[i].column);
				}
				if (this._sharedState._nextColumnHeaderToRead <= i)
				{
					this.ReadColumnHeader(i);
				}
				if (this._data[i] != null && this._data[i].IsNull)
				{
					throw new SqlNullValueException();
				}
				if (dataIndex < this._columnDataCharsRead)
				{
					throw ADP.NonSeqByteAccess(dataIndex, this._columnDataCharsRead, "GetChars");
				}
				if (dataIndex == 0L)
				{
					this._stateObj._plpdecoder = null;
				}
				bool isNCharType = this._metaData[i].metaType.IsNCharType;
				if (-1L == this._sharedState._columnDataBytesRemaining)
				{
					this._sharedState._columnDataBytesRemaining = (long)this._parser.PlpBytesLeft(this._stateObj);
				}
				if (this._sharedState._columnDataBytesRemaining == 0L)
				{
					this._stateObj._plpdecoder = null;
					result = 0L;
				}
				else if (buffer == null)
				{
					long num = (long)this._parser.PlpBytesTotalLength(this._stateObj);
					result = ((isNCharType && num > 0L) ? (num >> 1) : num);
				}
				else
				{
					long num;
					if (dataIndex > this._columnDataCharsRead)
					{
						this._stateObj._plpdecoder = null;
						num = dataIndex - this._columnDataCharsRead;
						num = (isNCharType ? (num << 1) : num);
						num = (long)this._parser.SkipPlpValue((ulong)num, this._stateObj);
						this._columnDataBytesRead += num;
						this._columnDataCharsRead += ((isNCharType && num > 0L) ? (num >> 1) : num);
					}
					num = (long)length;
					if (isNCharType)
					{
						num = (long)this._parser.ReadPlpUnicodeChars(ref buffer, bufferIndex, length, this._stateObj);
						this._columnDataBytesRead += num << 1;
					}
					else
					{
						num = (long)this._parser.ReadPlpAnsiChars(ref buffer, bufferIndex, length, this._metaData[i], this._stateObj);
						this._columnDataBytesRead += num << 1;
					}
					this._columnDataCharsRead += num;
					this._sharedState._columnDataBytesRemaining = (long)this._parser.PlpBytesLeft(this._stateObj);
					result = num;
				}
			}
			catch (OutOfMemoryException e)
			{
				this._isClosed = true;
				if (this._connection != null)
				{
					this._connection.Abort(e);
				}
				throw;
			}
			catch (StackOverflowException e2)
			{
				this._isClosed = true;
				if (this._connection != null)
				{
					this._connection.Abort(e2);
				}
				throw;
			}
			catch (ThreadAbortException e3)
			{
				this._isClosed = true;
				if (this._connection != null)
				{
					this._connection.Abort(e3);
				}
				throw;
			}
			return result;
		}

		// Token: 0x06001BFE RID: 7166 RVA: 0x000C4A20 File Offset: 0x000C3E20
		internal long GetStreamingXmlChars(int i, long dataIndex, char[] buffer, int bufferIndex, int length)
		{
			if (this._streamingXml != null && this._streamingXml.ColumnOrdinal != i)
			{
				this._streamingXml.Close();
				this._streamingXml = null;
			}
			SqlStreamingXml sqlStreamingXml;
			if (this._streamingXml == null)
			{
				sqlStreamingXml = new SqlStreamingXml(i, this);
			}
			else
			{
				sqlStreamingXml = this._streamingXml;
			}
			long chars = sqlStreamingXml.GetChars(dataIndex, buffer, bufferIndex, length);
			if (this._streamingXml == null)
			{
				this._streamingXml = sqlStreamingXml;
			}
			return chars;
		}

		// Token: 0x06001BFF RID: 7167 RVA: 0x000C4A90 File Offset: 0x000C3E90
		[EditorBrowsable(EditorBrowsableState.Never)]
		IDataReader IDataRecord.GetData(int i)
		{
			throw ADP.NotSupported();
		}

		// Token: 0x06001C00 RID: 7168 RVA: 0x000C4AA4 File Offset: 0x000C3EA4
		public override DateTime GetDateTime(int i)
		{
			this.ReadColumn(i, true, false);
			DateTime result = this._data[i].DateTime;
			if (this._typeSystem <= SqlConnectionString.TypeSystem.SQLServer2005 && this._metaData[i].IsNewKatmaiDateTimeType)
			{
				object @string = this._data[i].String;
				result = (DateTime)@string;
			}
			return result;
		}

		// Token: 0x06001C01 RID: 7169 RVA: 0x000C4B00 File Offset: 0x000C3F00
		public override decimal GetDecimal(int i)
		{
			this.ReadColumn(i, true, false);
			return this._data[i].Decimal;
		}

		// Token: 0x06001C02 RID: 7170 RVA: 0x000C4B24 File Offset: 0x000C3F24
		public override double GetDouble(int i)
		{
			this.ReadColumn(i, true, false);
			return this._data[i].Double;
		}

		// Token: 0x06001C03 RID: 7171 RVA: 0x000C4B48 File Offset: 0x000C3F48
		public override float GetFloat(int i)
		{
			this.ReadColumn(i, true, false);
			return this._data[i].Single;
		}

		// Token: 0x06001C04 RID: 7172 RVA: 0x000C4B6C File Offset: 0x000C3F6C
		public override Guid GetGuid(int i)
		{
			this.ReadColumn(i, true, false);
			return this._data[i].SqlGuid.Value;
		}

		// Token: 0x06001C05 RID: 7173 RVA: 0x000C4B98 File Offset: 0x000C3F98
		public override short GetInt16(int i)
		{
			this.ReadColumn(i, true, false);
			return this._data[i].Int16;
		}

		// Token: 0x06001C06 RID: 7174 RVA: 0x000C4BBC File Offset: 0x000C3FBC
		public override int GetInt32(int i)
		{
			this.ReadColumn(i, true, false);
			return this._data[i].Int32;
		}

		// Token: 0x06001C07 RID: 7175 RVA: 0x000C4BE0 File Offset: 0x000C3FE0
		public override long GetInt64(int i)
		{
			this.ReadColumn(i, true, false);
			return this._data[i].Int64;
		}

		// Token: 0x06001C08 RID: 7176 RVA: 0x000C4C04 File Offset: 0x000C4004
		public virtual SqlBoolean GetSqlBoolean(int i)
		{
			this.ReadColumn(i, true, false);
			return this._data[i].SqlBoolean;
		}

		// Token: 0x06001C09 RID: 7177 RVA: 0x000C4C28 File Offset: 0x000C4028
		public virtual SqlBinary GetSqlBinary(int i)
		{
			this.ReadColumn(i, true, true);
			return this._data[i].SqlBinary;
		}

		// Token: 0x06001C0A RID: 7178 RVA: 0x000C4C4C File Offset: 0x000C404C
		public virtual SqlByte GetSqlByte(int i)
		{
			this.ReadColumn(i, true, false);
			return this._data[i].SqlByte;
		}

		// Token: 0x06001C0B RID: 7179 RVA: 0x000C4C70 File Offset: 0x000C4070
		public virtual SqlBytes GetSqlBytes(int i)
		{
			this.ReadColumn(i, true, false);
			SqlBinary sqlBinary = this._data[i].SqlBinary;
			return new SqlBytes(sqlBinary);
		}

		// Token: 0x06001C0C RID: 7180 RVA: 0x000C4C9C File Offset: 0x000C409C
		public virtual SqlChars GetSqlChars(int i)
		{
			this.ReadColumn(i, true, false);
			SqlString value;
			if (this._typeSystem <= SqlConnectionString.TypeSystem.SQLServer2005 && this._metaData[i].IsNewKatmaiDateTimeType)
			{
				value = this._data[i].KatmaiDateTimeSqlString;
			}
			else
			{
				value = this._data[i].SqlString;
			}
			return new SqlChars(value);
		}

		// Token: 0x06001C0D RID: 7181 RVA: 0x000C4CF8 File Offset: 0x000C40F8
		public virtual SqlDateTime GetSqlDateTime(int i)
		{
			this.ReadColumn(i, true, false);
			return this._data[i].SqlDateTime;
		}

		// Token: 0x06001C0E RID: 7182 RVA: 0x000C4D1C File Offset: 0x000C411C
		public virtual SqlDecimal GetSqlDecimal(int i)
		{
			this.ReadColumn(i, true, false);
			return this._data[i].SqlDecimal;
		}

		// Token: 0x06001C0F RID: 7183 RVA: 0x000C4D40 File Offset: 0x000C4140
		public virtual SqlGuid GetSqlGuid(int i)
		{
			this.ReadColumn(i, true, false);
			return this._data[i].SqlGuid;
		}

		// Token: 0x06001C10 RID: 7184 RVA: 0x000C4D64 File Offset: 0x000C4164
		public virtual SqlDouble GetSqlDouble(int i)
		{
			this.ReadColumn(i, true, false);
			return this._data[i].SqlDouble;
		}

		// Token: 0x06001C11 RID: 7185 RVA: 0x000C4D88 File Offset: 0x000C4188
		public virtual SqlInt16 GetSqlInt16(int i)
		{
			this.ReadColumn(i, true, false);
			return this._data[i].SqlInt16;
		}

		// Token: 0x06001C12 RID: 7186 RVA: 0x000C4DAC File Offset: 0x000C41AC
		public virtual SqlInt32 GetSqlInt32(int i)
		{
			this.ReadColumn(i, true, false);
			return this._data[i].SqlInt32;
		}

		// Token: 0x06001C13 RID: 7187 RVA: 0x000C4DD0 File Offset: 0x000C41D0
		public virtual SqlInt64 GetSqlInt64(int i)
		{
			this.ReadColumn(i, true, false);
			return this._data[i].SqlInt64;
		}

		// Token: 0x06001C14 RID: 7188 RVA: 0x000C4DF4 File Offset: 0x000C41F4
		public virtual SqlMoney GetSqlMoney(int i)
		{
			this.ReadColumn(i, true, false);
			return this._data[i].SqlMoney;
		}

		// Token: 0x06001C15 RID: 7189 RVA: 0x000C4E18 File Offset: 0x000C4218
		public virtual SqlSingle GetSqlSingle(int i)
		{
			this.ReadColumn(i, true, false);
			return this._data[i].SqlSingle;
		}

		// Token: 0x06001C16 RID: 7190 RVA: 0x000C4E3C File Offset: 0x000C423C
		public virtual SqlString GetSqlString(int i)
		{
			this.ReadColumn(i, true, false);
			if (this._typeSystem <= SqlConnectionString.TypeSystem.SQLServer2005 && this._metaData[i].IsNewKatmaiDateTimeType)
			{
				return this._data[i].KatmaiDateTimeSqlString;
			}
			return this._data[i].SqlString;
		}

		// Token: 0x06001C17 RID: 7191 RVA: 0x000C4E90 File Offset: 0x000C4290
		public virtual SqlXml GetSqlXml(int i)
		{
			this.ReadColumn(i, true, false);
			SqlXml result;
			if (this._typeSystem != SqlConnectionString.TypeSystem.SQLServer2000)
			{
				result = (this._data[i].IsNull ? SqlXml.Null : this._data[i].SqlCachedBuffer.ToSqlXml());
			}
			else
			{
				SqlXml sqlXml = this._data[i].IsNull ? SqlXml.Null : this._data[i].SqlCachedBuffer.ToSqlXml();
				object @string = this._data[i].String;
				result = (SqlXml)@string;
			}
			return result;
		}

		// Token: 0x06001C18 RID: 7192 RVA: 0x000C4F20 File Offset: 0x000C4320
		public virtual object GetSqlValue(int i)
		{
			SqlStatistics statistics = null;
			object sqlValueInternal;
			try
			{
				statistics = SqlStatistics.StartTimer(this.Statistics);
				this.SetTimeout(this._defaultTimeoutMilliseconds);
				sqlValueInternal = this.GetSqlValueInternal(i);
			}
			finally
			{
				SqlStatistics.StopTimer(statistics);
			}
			return sqlValueInternal;
		}

		// Token: 0x06001C19 RID: 7193 RVA: 0x000C4F78 File Offset: 0x000C4378
		private object GetSqlValueInternal(int i)
		{
			if (this._currentTask != null)
			{
				throw ADP.AsyncOperationPending();
			}
			if (!this.TryReadColumn(i, false, false))
			{
				throw SQL.SynchronousCallMayNotPend();
			}
			return this.GetSqlValueFromSqlBufferInternal(this._data[i], this._metaData[i]);
		}

		// Token: 0x06001C1A RID: 7194 RVA: 0x000C4FC0 File Offset: 0x000C43C0
		private object GetSqlValueFromSqlBufferInternal(SqlBuffer data, _SqlMetaData metaData)
		{
			if (this._typeSystem <= SqlConnectionString.TypeSystem.SQLServer2005 && metaData.IsNewKatmaiDateTimeType)
			{
				return data.KatmaiDateTimeSqlString;
			}
			if (this._typeSystem <= SqlConnectionString.TypeSystem.SQLServer2005 && metaData.IsLargeUdt)
			{
				return data.SqlValue;
			}
			if (this._typeSystem != SqlConnectionString.TypeSystem.SQLServer2000)
			{
				if (metaData.type != SqlDbType.Udt)
				{
					return data.SqlValue;
				}
				SqlConnection connection = this._connection;
				if (connection != null)
				{
					connection.CheckGetExtendedUDTInfo(metaData, true);
					return connection.GetUdtValue(data.Value, metaData, false);
				}
				throw ADP.DataReaderClosed("GetSqlValueFromSqlBufferInternal");
			}
			else
			{
				if (metaData.type == SqlDbType.Xml)
				{
					return data.SqlString;
				}
				return data.SqlValue;
			}
		}

		// Token: 0x06001C1B RID: 7195 RVA: 0x000C5070 File Offset: 0x000C4470
		public virtual int GetSqlValues(object[] values)
		{
			SqlStatistics statistics = null;
			int result;
			try
			{
				statistics = SqlStatistics.StartTimer(this.Statistics);
				this.CheckDataIsReady();
				if (values == null)
				{
					throw ADP.ArgumentNull("values");
				}
				this.SetTimeout(this._defaultTimeoutMilliseconds);
				int num = (values.Length < this._metaData.visibleColumns) ? values.Length : this._metaData.visibleColumns;
				for (int i = 0; i < num; i++)
				{
					values[this._metaData.indexMap[i]] = this.GetSqlValueInternal(i);
				}
				result = num;
			}
			finally
			{
				SqlStatistics.StopTimer(statistics);
			}
			return result;
		}

		// Token: 0x06001C1C RID: 7196 RVA: 0x000C5118 File Offset: 0x000C4518
		public override string GetString(int i)
		{
			this.ReadColumn(i, true, false);
			if (this._typeSystem <= SqlConnectionString.TypeSystem.SQLServer2005 && this._metaData[i].IsNewKatmaiDateTimeType)
			{
				return this._data[i].KatmaiDateTimeString;
			}
			return this._data[i].String;
		}

		// Token: 0x06001C1D RID: 7197 RVA: 0x000C516C File Offset: 0x000C456C
		public override T GetFieldValue<T>(int i)
		{
			SqlStatistics statistics = null;
			T fieldValueInternal;
			try
			{
				statistics = SqlStatistics.StartTimer(this.Statistics);
				this.SetTimeout(this._defaultTimeoutMilliseconds);
				fieldValueInternal = this.GetFieldValueInternal<T>(i);
			}
			finally
			{
				SqlStatistics.StopTimer(statistics);
			}
			return fieldValueInternal;
		}

		// Token: 0x06001C1E RID: 7198 RVA: 0x000C51C4 File Offset: 0x000C45C4
		public override object GetValue(int i)
		{
			SqlStatistics statistics = null;
			object valueInternal;
			try
			{
				statistics = SqlStatistics.StartTimer(this.Statistics);
				this.SetTimeout(this._defaultTimeoutMilliseconds);
				valueInternal = this.GetValueInternal(i);
			}
			finally
			{
				SqlStatistics.StopTimer(statistics);
			}
			return valueInternal;
		}

		// Token: 0x06001C1F RID: 7199 RVA: 0x000C521C File Offset: 0x000C461C
		public virtual TimeSpan GetTimeSpan(int i)
		{
			this.ReadColumn(i, true, false);
			TimeSpan result = this._data[i].Time;
			if (this._typeSystem <= SqlConnectionString.TypeSystem.SQLServer2005)
			{
				object @string = this._data[i].String;
				result = (TimeSpan)@string;
			}
			return result;
		}

		// Token: 0x06001C20 RID: 7200 RVA: 0x000C5264 File Offset: 0x000C4664
		public virtual DateTimeOffset GetDateTimeOffset(int i)
		{
			this.ReadColumn(i, true, false);
			DateTimeOffset result = this._data[i].DateTimeOffset;
			if (this._typeSystem <= SqlConnectionString.TypeSystem.SQLServer2005)
			{
				object @string = this._data[i].String;
				result = (DateTimeOffset)@string;
			}
			return result;
		}

		// Token: 0x06001C21 RID: 7201 RVA: 0x000C52AC File Offset: 0x000C46AC
		private object GetValueInternal(int i)
		{
			if (this._currentTask != null)
			{
				throw ADP.AsyncOperationPending();
			}
			if (!this.TryReadColumn(i, false, false))
			{
				throw SQL.SynchronousCallMayNotPend();
			}
			return this.GetValueFromSqlBufferInternal(this._data[i], this._metaData[i]);
		}

		// Token: 0x06001C22 RID: 7202 RVA: 0x000C52F4 File Offset: 0x000C46F4
		private object GetValueFromSqlBufferInternal(SqlBuffer data, _SqlMetaData metaData)
		{
			if (this._typeSystem <= SqlConnectionString.TypeSystem.SQLServer2005 && metaData.IsNewKatmaiDateTimeType)
			{
				if (data.IsNull)
				{
					return DBNull.Value;
				}
				return data.KatmaiDateTimeString;
			}
			else
			{
				if (this._typeSystem <= SqlConnectionString.TypeSystem.SQLServer2005 && metaData.IsLargeUdt)
				{
					return data.Value;
				}
				if (this._typeSystem == SqlConnectionString.TypeSystem.SQLServer2000)
				{
					return data.Value;
				}
				if (metaData.type != SqlDbType.Udt)
				{
					return data.Value;
				}
				SqlConnection connection = this._connection;
				if (connection != null)
				{
					connection.CheckGetExtendedUDTInfo(metaData, true);
					return connection.GetUdtValue(data.Value, metaData, true);
				}
				throw ADP.DataReaderClosed("GetValueFromSqlBufferInternal");
			}
		}

		// Token: 0x06001C23 RID: 7203 RVA: 0x000C5398 File Offset: 0x000C4798
		private T GetFieldValueInternal<T>(int i)
		{
			if (this._currentTask != null)
			{
				throw ADP.AsyncOperationPending();
			}
			if (!this.TryReadColumn(i, false, false))
			{
				throw SQL.SynchronousCallMayNotPend();
			}
			return this.GetFieldValueFromSqlBufferInternal<T>(this._data[i], this._metaData[i]);
		}

		// Token: 0x06001C24 RID: 7204 RVA: 0x000C53E0 File Offset: 0x000C47E0
		private T GetFieldValueFromSqlBufferInternal<T>(SqlBuffer data, _SqlMetaData metaData)
		{
			Type typeFromHandle = typeof(T);
			if (SqlDataReader._typeofINullable.IsAssignableFrom(typeFromHandle))
			{
				object obj = this.GetSqlValueFromSqlBufferInternal(data, metaData);
				if (typeFromHandle == SqlDataReader._typeofSqlString)
				{
					SqlXml sqlXml = obj as SqlXml;
					if (sqlXml != null)
					{
						if (sqlXml.IsNull)
						{
							obj = SqlString.Null;
						}
						else
						{
							obj = new SqlString(sqlXml.Value);
						}
					}
				}
				return (T)((object)obj);
			}
			T result;
			try
			{
				result = (T)((object)this.GetValueFromSqlBufferInternal(data, metaData));
			}
			catch (InvalidCastException)
			{
				if (data.IsNull)
				{
					throw SQL.SqlNullValue();
				}
				throw;
			}
			return result;
		}

		// Token: 0x06001C25 RID: 7205 RVA: 0x000C5494 File Offset: 0x000C4894
		public override int GetValues(object[] values)
		{
			SqlStatistics statistics = null;
			bool flag = this.IsCommandBehavior(CommandBehavior.SequentialAccess);
			int result;
			try
			{
				statistics = SqlStatistics.StartTimer(this.Statistics);
				if (values == null)
				{
					throw ADP.ArgumentNull("values");
				}
				this.CheckMetaDataIsReady();
				int num = (values.Length < this._metaData.visibleColumns) ? values.Length : this._metaData.visibleColumns;
				int num2 = num - 1;
				this.SetTimeout(this._defaultTimeoutMilliseconds);
				this._commandBehavior &= ~CommandBehavior.SequentialAccess;
				if (!this.TryReadColumn(num2, false, false))
				{
					throw SQL.SynchronousCallMayNotPend();
				}
				for (int i = 0; i < num; i++)
				{
					values[this._metaData.indexMap[i]] = this.GetValueFromSqlBufferInternal(this._data[i], this._metaData[i]);
					if (flag && i < num2)
					{
						this._data[i].Clear();
					}
				}
				result = num;
			}
			finally
			{
				if (flag)
				{
					this._commandBehavior |= CommandBehavior.SequentialAccess;
				}
				SqlStatistics.StopTimer(statistics);
			}
			return result;
		}

		// Token: 0x06001C26 RID: 7206 RVA: 0x000C55AC File Offset: 0x000C49AC
		private MetaType GetVersionedMetaType(MetaType actualMetaType)
		{
			MetaType result;
			if (actualMetaType == MetaType.MetaUdt)
			{
				result = MetaType.MetaVarBinary;
			}
			else if (actualMetaType == MetaType.MetaXml)
			{
				result = MetaType.MetaNText;
			}
			else if (actualMetaType == MetaType.MetaMaxVarBinary)
			{
				result = MetaType.MetaImage;
			}
			else if (actualMetaType == MetaType.MetaMaxVarChar)
			{
				result = MetaType.MetaText;
			}
			else if (actualMetaType == MetaType.MetaMaxNVarChar)
			{
				result = MetaType.MetaNText;
			}
			else
			{
				result = actualMetaType;
			}
			return result;
		}

		// Token: 0x06001C27 RID: 7207 RVA: 0x000C5610 File Offset: 0x000C4A10
		private bool TryHasMoreResults(out bool moreResults)
		{
			if (this._parser != null)
			{
				bool flag;
				if (!this.TryHasMoreRows(out flag))
				{
					moreResults = false;
					return false;
				}
				if (flag)
				{
					moreResults = false;
					return true;
				}
				while (this._stateObj._pendingData)
				{
					byte b;
					if (!this._stateObj.TryPeekByte(out b))
					{
						moreResults = false;
						return false;
					}
					if (b <= 210)
					{
						if (b == 129)
						{
							moreResults = true;
							return true;
						}
						if (b - 209 <= 1)
						{
							moreResults = true;
							return true;
						}
					}
					else
					{
						if (b == 211)
						{
							if (this._altRowStatus == SqlDataReader.ALTROWSTATUS.Null)
							{
								this._altMetaDataSetCollection.metaDataSet = this._metaData;
								this._metaData = null;
							}
							this._altRowStatus = SqlDataReader.ALTROWSTATUS.AltRow;
							this._hasRows = true;
							moreResults = true;
							return true;
						}
						if (b == 253)
						{
							this._altRowStatus = SqlDataReader.ALTROWSTATUS.Null;
							this._metaData = null;
							this._altMetaDataSetCollection = null;
							moreResults = true;
							return true;
						}
					}
					if (this._parser.State == TdsParserState.Broken || this._parser.State == TdsParserState.Closed)
					{
						throw ADP.ClosedConnectionError();
					}
					bool flag2;
					if (!this._parser.TryRun(RunBehavior.ReturnImmediately, this._command, this, null, this._stateObj, out flag2))
					{
						moreResults = false;
						return false;
					}
				}
			}
			moreResults = false;
			return true;
		}

		// Token: 0x06001C28 RID: 7208 RVA: 0x000C5734 File Offset: 0x000C4B34
		private bool TryHasMoreRows(out bool moreRows)
		{
			if (this._parser != null)
			{
				if (this._sharedState._dataReady)
				{
					moreRows = true;
					return true;
				}
				SqlDataReader.ALTROWSTATUS altRowStatus = this._altRowStatus;
				if (altRowStatus == SqlDataReader.ALTROWSTATUS.AltRow)
				{
					moreRows = true;
					return true;
				}
				if (altRowStatus == SqlDataReader.ALTROWSTATUS.Done)
				{
					moreRows = false;
					return true;
				}
				if (this._stateObj._pendingData)
				{
					byte b;
					if (!this._stateObj.TryPeekByte(out b))
					{
						moreRows = false;
						return false;
					}
					bool flag = false;
					while (b == 253 || b == 254 || b == 255 || (!flag && (b == 228 || b == 227 || b == 169 || b == 170 || b == 171)))
					{
						if (b == 253 || b == 254 || b == 255)
						{
							flag = true;
						}
						if (this._parser.State == TdsParserState.Broken || this._parser.State == TdsParserState.Closed)
						{
							throw ADP.ClosedConnectionError();
						}
						bool flag2;
						if (!this._parser.TryRun(RunBehavior.ReturnImmediately, this._command, this, null, this._stateObj, out flag2))
						{
							moreRows = false;
							return false;
						}
						if (!this._stateObj._pendingData)
						{
							break;
						}
						if (!this._stateObj.TryPeekByte(out b))
						{
							moreRows = false;
							return false;
						}
					}
					if (this.IsRowToken(b))
					{
						moreRows = true;
						return true;
					}
				}
			}
			moreRows = false;
			return true;
		}

		// Token: 0x06001C29 RID: 7209 RVA: 0x000C5890 File Offset: 0x000C4C90
		private bool IsRowToken(byte token)
		{
			return 209 == token || 210 == token;
		}

		// Token: 0x06001C2A RID: 7210 RVA: 0x000C58B0 File Offset: 0x000C4CB0
		public override bool IsDBNull(int i)
		{
			if (this.IsCommandBehavior(CommandBehavior.SequentialAccess) && (this._sharedState._nextColumnHeaderToRead > i + 1 || this._lastColumnWithDataChunkRead > i))
			{
				this.CheckMetaDataIsReady(i, false);
			}
			else
			{
				this.CheckHeaderIsReady(i, false, "IsDBNull");
				this.SetTimeout(this._defaultTimeoutMilliseconds);
				this.ReadColumnHeader(i);
			}
			return this._data[i].IsNull;
		}

		// Token: 0x06001C2B RID: 7211 RVA: 0x000C5918 File Offset: 0x000C4D18
		protected internal bool IsCommandBehavior(CommandBehavior condition)
		{
			return condition == (condition & this._commandBehavior);
		}

		// Token: 0x06001C2C RID: 7212 RVA: 0x000C5930 File Offset: 0x000C4D30
		public override bool NextResult()
		{
			if (this._currentTask != null)
			{
				throw SQL.PendingBeginXXXExists();
			}
			bool result;
			if (!this.TryNextResult(out result))
			{
				throw SQL.SynchronousCallMayNotPend();
			}
			return result;
		}

		// Token: 0x06001C2D RID: 7213 RVA: 0x000C5960 File Offset: 0x000C4D60
		private bool TryNextResult(out bool more)
		{
			SqlStatistics statistics = null;
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<sc.SqlDataReader.NextResult|API> %d#", this.ObjectID);
			RuntimeHelpers.PrepareConstrainedRegions();
			bool result;
			try
			{
				statistics = SqlStatistics.StartTimer(this.Statistics);
				this.SetTimeout(this._defaultTimeoutMilliseconds);
				if (this.IsClosed)
				{
					throw ADP.DataReaderClosed("NextResult");
				}
				this._fieldNameLookup = null;
				bool flag = false;
				this._hasRows = false;
				if (this.IsCommandBehavior(CommandBehavior.SingleResult))
				{
					if (!this.TryCloseInternal(false))
					{
						more = false;
						result = false;
					}
					else
					{
						this.ClearMetaData();
						more = flag;
						result = true;
					}
				}
				else
				{
					if (this._parser != null)
					{
						bool flag2 = true;
						while (flag2)
						{
							if (!this.TryReadInternal(false, out flag2))
							{
								more = false;
								return false;
							}
						}
					}
					if (this._parser != null)
					{
						bool flag3;
						if (!this.TryHasMoreResults(out flag3))
						{
							more = false;
							return false;
						}
						if (flag3)
						{
							this._metaDataConsumed = false;
							this._browseModeInfoConsumed = false;
							SqlDataReader.ALTROWSTATUS altRowStatus = this._altRowStatus;
							if (altRowStatus != SqlDataReader.ALTROWSTATUS.AltRow)
							{
								if (altRowStatus != SqlDataReader.ALTROWSTATUS.Done)
								{
									if (!this.TryConsumeMetaData())
									{
										more = false;
										return false;
									}
									if (this._metaData == null)
									{
										more = false;
										return true;
									}
								}
								else
								{
									this._metaData = this._altMetaDataSetCollection.metaDataSet;
									this._altRowStatus = SqlDataReader.ALTROWSTATUS.Null;
								}
							}
							else
							{
								int id;
								if (!this._parser.TryGetAltRowId(this._stateObj, out id))
								{
									more = false;
									return false;
								}
								_SqlMetaDataSet altMetaData = this._altMetaDataSetCollection.GetAltMetaData(id);
								if (altMetaData != null)
								{
									this._metaData = altMetaData;
								}
							}
							flag = true;
						}
						else
						{
							if (!this.TryCloseInternal(false))
							{
								more = false;
								return false;
							}
							if (!this.TrySetMetaData(null, false))
							{
								more = false;
								return false;
							}
						}
					}
					else
					{
						this.ClearMetaData();
					}
					more = flag;
					result = true;
				}
			}
			catch (OutOfMemoryException e)
			{
				this._isClosed = true;
				if (this._connection != null)
				{
					this._connection.Abort(e);
				}
				throw;
			}
			catch (StackOverflowException e2)
			{
				this._isClosed = true;
				if (this._connection != null)
				{
					this._connection.Abort(e2);
				}
				throw;
			}
			catch (ThreadAbortException e3)
			{
				this._isClosed = true;
				if (this._connection != null)
				{
					this._connection.Abort(e3);
				}
				throw;
			}
			finally
			{
				SqlStatistics.StopTimer(statistics);
				Bid.ScopeLeave(ref intPtr);
			}
			return result;
		}

		// Token: 0x06001C2E RID: 7214 RVA: 0x000C5BE8 File Offset: 0x000C4FE8
		public override bool Read()
		{
			if (this._currentTask != null)
			{
				throw SQL.PendingBeginXXXExists();
			}
			bool result;
			if (!this.TryReadInternal(true, out result))
			{
				throw SQL.SynchronousCallMayNotPend();
			}
			return result;
		}

		// Token: 0x06001C2F RID: 7215 RVA: 0x000C5C18 File Offset: 0x000C5018
		private bool TryReadInternal(bool setTimeout, out bool more)
		{
			SqlStatistics statistics = null;
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<sc.SqlDataReader.Read|API> %d#", this.ObjectID);
			RuntimeHelpers.PrepareConstrainedRegions();
			bool result;
			try
			{
				statistics = SqlStatistics.StartTimer(this.Statistics);
				if (this._parser != null)
				{
					if (setTimeout)
					{
						this.SetTimeout(this._defaultTimeoutMilliseconds);
					}
					if (this._sharedState._dataReady && !this.TryCleanPartialRead())
					{
						more = false;
						return false;
					}
					SqlBuffer.Clear(this._data);
					this._sharedState._nextColumnHeaderToRead = 0;
					this._sharedState._nextColumnDataToRead = 0;
					this._sharedState._columnDataBytesRemaining = -1L;
					this._lastColumnWithDataChunkRead = -1;
					if (!this._haltRead)
					{
						bool flag;
						if (!this.TryHasMoreRows(out flag))
						{
							more = false;
							return false;
						}
						if (flag)
						{
							while (this._stateObj._pendingData)
							{
								if (this._altRowStatus == SqlDataReader.ALTROWSTATUS.AltRow)
								{
									this._altRowStatus = SqlDataReader.ALTROWSTATUS.Done;
									this._sharedState._dataReady = true;
									break;
								}
								if (!this._parser.TryRun(RunBehavior.ReturnImmediately, this._command, this, null, this._stateObj, out this._sharedState._dataReady))
								{
									more = false;
									return false;
								}
								if (this._sharedState._dataReady)
								{
									break;
								}
							}
							if (this._sharedState._dataReady)
							{
								this._haltRead = this.IsCommandBehavior(CommandBehavior.SingleRow);
								more = true;
								return true;
							}
						}
						if (!this._stateObj._pendingData && !this.TryCloseInternal(false))
						{
							more = false;
							return false;
						}
					}
					else
					{
						bool flag2;
						if (!this.TryHasMoreRows(out flag2))
						{
							more = false;
							return false;
						}
						while (flag2)
						{
							while (this._stateObj._pendingData && !this._sharedState._dataReady)
							{
								if (!this._parser.TryRun(RunBehavior.ReturnImmediately, this._command, this, null, this._stateObj, out this._sharedState._dataReady))
								{
									more = false;
									return false;
								}
							}
							if (this._sharedState._dataReady && !this.TryCleanPartialRead())
							{
								more = false;
								return false;
							}
							SqlBuffer.Clear(this._data);
							this._sharedState._nextColumnHeaderToRead = 0;
							if (!this.TryHasMoreRows(out flag2))
							{
								more = false;
								return false;
							}
						}
						this._haltRead = false;
					}
				}
				else if (this.IsClosed)
				{
					throw ADP.DataReaderClosed("Read");
				}
				more = false;
				result = true;
			}
			catch (OutOfMemoryException e)
			{
				this._isClosed = true;
				SqlConnection connection = this._connection;
				if (connection != null)
				{
					connection.Abort(e);
				}
				throw;
			}
			catch (StackOverflowException e2)
			{
				this._isClosed = true;
				SqlConnection connection2 = this._connection;
				if (connection2 != null)
				{
					connection2.Abort(e2);
				}
				throw;
			}
			catch (ThreadAbortException e3)
			{
				this._isClosed = true;
				SqlConnection connection3 = this._connection;
				if (connection3 != null)
				{
					connection3.Abort(e3);
				}
				throw;
			}
			finally
			{
				SqlStatistics.StopTimer(statistics);
				Bid.ScopeLeave(ref intPtr);
			}
			return result;
		}

		// Token: 0x06001C30 RID: 7216 RVA: 0x000C5F40 File Offset: 0x000C5340
		private void ReadColumn(int i, bool setTimeout = true, bool allowPartiallyReadColumn = false)
		{
			if (this._currentTask != null)
			{
				throw ADP.AsyncOperationPending();
			}
			if (!this.TryReadColumn(i, setTimeout, allowPartiallyReadColumn))
			{
				throw SQL.SynchronousCallMayNotPend();
			}
		}

		// Token: 0x06001C31 RID: 7217 RVA: 0x000C5F70 File Offset: 0x000C5370
		private bool TryReadColumn(int i, bool setTimeout, bool allowPartiallyReadColumn = false)
		{
			this.CheckDataIsReady(i, allowPartiallyReadColumn, true, null);
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				if (setTimeout)
				{
					this.SetTimeout(this._defaultTimeoutMilliseconds);
				}
				if (!this.TryReadColumnInternal(i, false))
				{
					return false;
				}
			}
			catch (OutOfMemoryException e)
			{
				this._isClosed = true;
				if (this._connection != null)
				{
					this._connection.Abort(e);
				}
				throw;
			}
			catch (StackOverflowException e2)
			{
				this._isClosed = true;
				if (this._connection != null)
				{
					this._connection.Abort(e2);
				}
				throw;
			}
			catch (ThreadAbortException e3)
			{
				this._isClosed = true;
				if (this._connection != null)
				{
					this._connection.Abort(e3);
				}
				throw;
			}
			return true;
		}

		// Token: 0x06001C32 RID: 7218 RVA: 0x000C605C File Offset: 0x000C545C
		private bool TryReadColumnData()
		{
			if (!this._data[this._sharedState._nextColumnDataToRead].IsNull)
			{
				_SqlMetaData sqlMetaData = this._metaData[this._sharedState._nextColumnDataToRead];
				if (!this._parser.TryReadSqlValue(this._data[this._sharedState._nextColumnDataToRead], sqlMetaData, (int)this._sharedState._columnDataBytesRemaining, this._stateObj, (this._command != null) ? this._command.ColumnEncryptionSetting : SqlCommandColumnEncryptionSetting.UseConnectionSetting, sqlMetaData.column))
				{
					return false;
				}
				this._sharedState._columnDataBytesRemaining = 0L;
			}
			this._sharedState._nextColumnDataToRead++;
			return true;
		}

		// Token: 0x06001C33 RID: 7219 RVA: 0x000C6110 File Offset: 0x000C5510
		private void ReadColumnHeader(int i)
		{
			if (!this.TryReadColumnHeader(i))
			{
				throw SQL.SynchronousCallMayNotPend();
			}
		}

		// Token: 0x06001C34 RID: 7220 RVA: 0x000C6130 File Offset: 0x000C5530
		private bool TryReadColumnHeader(int i)
		{
			if (!this._sharedState._dataReady)
			{
				throw SQL.InvalidRead();
			}
			RuntimeHelpers.PrepareConstrainedRegions();
			bool result;
			try
			{
				result = this.TryReadColumnInternal(i, true);
			}
			catch (OutOfMemoryException e)
			{
				this._isClosed = true;
				if (this._connection != null)
				{
					this._connection.Abort(e);
				}
				throw;
			}
			catch (StackOverflowException e2)
			{
				this._isClosed = true;
				if (this._connection != null)
				{
					this._connection.Abort(e2);
				}
				throw;
			}
			catch (ThreadAbortException e3)
			{
				this._isClosed = true;
				if (this._connection != null)
				{
					this._connection.Abort(e3);
				}
				throw;
			}
			return result;
		}

		// Token: 0x06001C35 RID: 7221 RVA: 0x000C6210 File Offset: 0x000C5610
		private bool TryReadColumnInternal(int i, bool readHeaderOnly = false)
		{
			if (i < this._sharedState._nextColumnHeaderToRead)
			{
				return i != this._sharedState._nextColumnDataToRead || readHeaderOnly || this.TryReadColumnData();
			}
			bool flag = this.IsCommandBehavior(CommandBehavior.SequentialAccess);
			if (flag)
			{
				if (0 < this._sharedState._nextColumnDataToRead)
				{
					this._data[this._sharedState._nextColumnDataToRead - 1].Clear();
				}
				if (this._lastColumnWithDataChunkRead > -1 && i > this._lastColumnWithDataChunkRead)
				{
					this.CloseActiveSequentialStreamAndTextReader();
				}
			}
			else if (this._sharedState._nextColumnDataToRead < this._sharedState._nextColumnHeaderToRead && !this.TryReadColumnData())
			{
				return false;
			}
			if (!this.TryResetBlobState())
			{
				return false;
			}
			for (;;)
			{
				_SqlMetaData sqlMetaData = this._metaData[this._sharedState._nextColumnHeaderToRead];
				if (flag && this._sharedState._nextColumnHeaderToRead < i)
				{
					if (!this._parser.TrySkipValue(sqlMetaData, this._sharedState._nextColumnHeaderToRead, this._stateObj))
					{
						break;
					}
					this._sharedState._nextColumnDataToRead = this._sharedState._nextColumnHeaderToRead;
					this._sharedState._nextColumnHeaderToRead++;
				}
				else
				{
					bool flag2;
					ulong num;
					if (!this._parser.TryProcessColumnHeader(sqlMetaData, this._stateObj, this._sharedState._nextColumnHeaderToRead, out flag2, out num))
					{
						return false;
					}
					this._sharedState._nextColumnDataToRead = this._sharedState._nextColumnHeaderToRead;
					this._sharedState._nextColumnHeaderToRead++;
					if (flag2 && sqlMetaData.type != SqlDbType.Timestamp)
					{
						TdsParser.GetNullSqlValue(this._data[this._sharedState._nextColumnDataToRead], sqlMetaData, (this._command != null) ? this._command.ColumnEncryptionSetting : SqlCommandColumnEncryptionSetting.UseConnectionSetting, this._parser.Connection);
						if (!readHeaderOnly)
						{
							this._sharedState._nextColumnDataToRead++;
						}
					}
					else if (i > this._sharedState._nextColumnDataToRead || !readHeaderOnly)
					{
						if (!this._parser.TryReadSqlValue(this._data[this._sharedState._nextColumnDataToRead], sqlMetaData, (int)num, this._stateObj, (this._command != null) ? this._command.ColumnEncryptionSetting : SqlCommandColumnEncryptionSetting.UseConnectionSetting, sqlMetaData.column))
						{
							return false;
						}
						this._sharedState._nextColumnDataToRead++;
					}
					else
					{
						this._sharedState._columnDataBytesRemaining = (long)num;
					}
				}
				if (this._snapshot != null)
				{
					this._snapshot = null;
					this.PrepareAsyncInvocation(true);
				}
				if (this._sharedState._nextColumnHeaderToRead > i)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001C36 RID: 7222 RVA: 0x000C6488 File Offset: 0x000C5888
		private bool WillHaveEnoughData(int targetColumn, bool headerOnly = false)
		{
			if (this._lastColumnWithDataChunkRead == this._sharedState._nextColumnDataToRead && this._metaData[this._lastColumnWithDataChunkRead].metaType.IsPlp)
			{
				return false;
			}
			int num = Math.Min(checked(this._stateObj._inBytesRead - this._stateObj._inBytesUsed), this._stateObj._inBytesPacket);
			num--;
			if (targetColumn >= this._sharedState._nextColumnDataToRead && this._sharedState._nextColumnDataToRead < this._sharedState._nextColumnHeaderToRead)
			{
				if (this._sharedState._columnDataBytesRemaining > (long)num)
				{
					return false;
				}
				checked
				{
					num -= (int)this._sharedState._columnDataBytesRemaining;
				}
			}
			int num2 = this._sharedState._nextColumnHeaderToRead;
			while (num >= 0 && num2 <= targetColumn)
			{
				checked
				{
					if (!this._stateObj.IsNullCompressionBitSet(num2))
					{
						MetaType metaType = this._metaData[num2].metaType;
						if (metaType.IsLong || metaType.IsPlp || metaType.SqlDbType == SqlDbType.Udt || metaType.SqlDbType == SqlDbType.Structured)
						{
							return false;
						}
						byte b = this._metaData[num2].tdsType & 48;
						int num3;
						if (b == 32 || b == 0)
						{
							if ((this._metaData[num2].tdsType & 128) != 0)
							{
								num3 = 2;
							}
							else if ((this._metaData[num2].tdsType & 12) == 0)
							{
								num3 = 4;
							}
							else
							{
								num3 = 1;
							}
						}
						else
						{
							num3 = 0;
						}
						num -= num3;
						if (num2 < targetColumn || !headerOnly)
						{
							num -= this._metaData[num2].length;
						}
					}
				}
				num2++;
			}
			return num >= 0;
		}

		// Token: 0x06001C37 RID: 7223 RVA: 0x000C6628 File Offset: 0x000C5A28
		private bool TryResetBlobState()
		{
			if (this._sharedState._nextColumnDataToRead < this._sharedState._nextColumnHeaderToRead)
			{
				if (this._sharedState._nextColumnHeaderToRead > 0 && this._metaData[this._sharedState._nextColumnHeaderToRead - 1].metaType.IsPlp)
				{
					ulong num;
					if (this._stateObj._longlen != 0UL && !this._stateObj.Parser.TrySkipPlpValue(18446744073709551615UL, this._stateObj, out num))
					{
						return false;
					}
					if (this._streamingXml != null)
					{
						SqlStreamingXml streamingXml = this._streamingXml;
						this._streamingXml = null;
						streamingXml.Close();
					}
				}
				else if (0L < this._sharedState._columnDataBytesRemaining && !this._stateObj.TrySkipLongBytes(this._sharedState._columnDataBytesRemaining))
				{
					return false;
				}
			}
			this._sharedState._columnDataBytesRemaining = 0L;
			this._columnDataBytesRead = 0L;
			this._columnDataCharsRead = 0L;
			this._columnDataChars = null;
			this._columnDataCharsIndex = -1;
			this._stateObj._plpdecoder = null;
			return true;
		}

		// Token: 0x06001C38 RID: 7224 RVA: 0x000C672C File Offset: 0x000C5B2C
		private void CloseActiveSequentialStreamAndTextReader()
		{
			if (this._currentStream != null)
			{
				this._currentStream.SetClosed();
				this._currentStream = null;
			}
			if (this._currentTextReader != null)
			{
				this._currentTextReader.SetClosed();
				this._currentStream = null;
			}
		}

		// Token: 0x06001C39 RID: 7225 RVA: 0x000C6770 File Offset: 0x000C5B70
		private void RestoreServerSettings(TdsParser parser, TdsParserStateObject stateObj)
		{
			if (parser != null && this._resetOptionsString != null)
			{
				if (parser.State == TdsParserState.OpenLoggedIn)
				{
					Bid.CorrelationTrace("<sc.SqlDataReader.RestoreServerSettings|Info|Correlation> ObjectID%d#, ActivityID %ls\n", this.ObjectID);
					Task task = parser.TdsExecuteSQLBatch(this._resetOptionsString, (this._command != null) ? this._command.CommandTimeout : 0, null, stateObj, true, false, null);
					parser.Run(RunBehavior.UntilDone, this._command, this, null, stateObj);
				}
				this._resetOptionsString = null;
			}
		}

		// Token: 0x06001C3A RID: 7226 RVA: 0x000C67E4 File Offset: 0x000C5BE4
		internal bool TrySetAltMetaDataSet(_SqlMetaDataSet metaDataSet, bool metaDataConsumed)
		{
			if (this._altMetaDataSetCollection == null)
			{
				this._altMetaDataSetCollection = new _SqlMetaDataSetCollection();
			}
			else if (this._snapshot != null && this._snapshot._altMetaDataSetCollection == this._altMetaDataSetCollection)
			{
				this._altMetaDataSetCollection = (_SqlMetaDataSetCollection)this._altMetaDataSetCollection.Clone();
			}
			this._altMetaDataSetCollection.SetAltMetaData(metaDataSet);
			this._metaDataConsumed = metaDataConsumed;
			if (this._metaDataConsumed && this._parser != null)
			{
				byte b;
				if (!this._stateObj.TryPeekByte(out b))
				{
					return false;
				}
				if (169 == b)
				{
					bool flag;
					if (!this._parser.TryRun(RunBehavior.ReturnImmediately, this._command, this, null, this._stateObj, out flag))
					{
						return false;
					}
					if (!this._stateObj.TryPeekByte(out b))
					{
						return false;
					}
				}
				if (b == 171)
				{
					try
					{
						this._stateObj._accumulateInfoEvents = true;
						bool flag2;
						if (!this._parser.TryRun(RunBehavior.ReturnImmediately, this._command, null, null, this._stateObj, out flag2))
						{
							return false;
						}
					}
					finally
					{
						this._stateObj._accumulateInfoEvents = false;
					}
					if (!this._stateObj.TryPeekByte(out b))
					{
						return false;
					}
				}
				this._hasRows = this.IsRowToken(b);
			}
			if (metaDataSet != null && (this._data == null || this._data.Length < metaDataSet.Length))
			{
				this._data = SqlBuffer.CreateBufferArray(metaDataSet.Length);
			}
			return true;
		}

		// Token: 0x06001C3B RID: 7227 RVA: 0x000C6958 File Offset: 0x000C5D58
		private void ClearMetaData()
		{
			this._metaData = null;
			this._tableNames = null;
			this._fieldNameLookup = null;
			this._metaDataConsumed = false;
			this._browseModeInfoConsumed = false;
		}

		// Token: 0x06001C3C RID: 7228 RVA: 0x000C6988 File Offset: 0x000C5D88
		internal bool TrySetMetaData(_SqlMetaDataSet metaData, bool moreInfo)
		{
			this._metaData = metaData;
			this._tableNames = null;
			if (this._metaData != null)
			{
				this._metaData.schemaTable = null;
				this._data = SqlBuffer.CreateBufferArray(metaData.Length);
			}
			this._fieldNameLookup = null;
			if (metaData != null)
			{
				if (!moreInfo)
				{
					this._metaDataConsumed = true;
					if (this._parser != null)
					{
						byte b;
						if (!this._stateObj.TryPeekByte(out b))
						{
							return false;
						}
						if (b == 169)
						{
							bool flag;
							if (!this._parser.TryRun(RunBehavior.ReturnImmediately, null, null, null, this._stateObj, out flag))
							{
								return false;
							}
							if (!this._stateObj.TryPeekByte(out b))
							{
								return false;
							}
						}
						if (b == 171)
						{
							try
							{
								this._stateObj._accumulateInfoEvents = true;
								bool flag2;
								if (!this._parser.TryRun(RunBehavior.ReturnImmediately, null, null, null, this._stateObj, out flag2))
								{
									return false;
								}
							}
							finally
							{
								this._stateObj._accumulateInfoEvents = false;
							}
							if (!this._stateObj.TryPeekByte(out b))
							{
								return false;
							}
						}
						this._hasRows = this.IsRowToken(b);
						if (136 == b)
						{
							this._metaDataConsumed = false;
						}
					}
				}
			}
			else
			{
				this._metaDataConsumed = false;
			}
			this._browseModeInfoConsumed = false;
			return true;
		}

		// Token: 0x06001C3D RID: 7229 RVA: 0x000C6AD0 File Offset: 0x000C5ED0
		private void SetTimeout(long timeoutMilliseconds)
		{
			TdsParserStateObject stateObj = this._stateObj;
			if (stateObj != null)
			{
				stateObj.SetTimeoutMilliseconds(timeoutMilliseconds);
			}
		}

		// Token: 0x06001C3E RID: 7230 RVA: 0x000C6AF0 File Offset: 0x000C5EF0
		private bool HasActiveStreamOrTextReaderOnColumn(int columnIndex)
		{
			bool flag = false;
			flag |= (this._currentStream != null && this._currentStream.ColumnIndex == columnIndex);
			return flag | (this._currentTextReader != null && this._currentTextReader.ColumnIndex == columnIndex);
		}

		// Token: 0x06001C3F RID: 7231 RVA: 0x000C6B38 File Offset: 0x000C5F38
		private void CheckMetaDataIsReady()
		{
			if (this._currentTask != null)
			{
				throw ADP.AsyncOperationPending();
			}
			if (this.MetaData == null)
			{
				throw SQL.InvalidRead();
			}
		}

		// Token: 0x06001C40 RID: 7232 RVA: 0x000C6B64 File Offset: 0x000C5F64
		private void CheckMetaDataIsReady(int columnIndex, bool permitAsync = false)
		{
			if (!permitAsync && this._currentTask != null)
			{
				throw ADP.AsyncOperationPending();
			}
			if (this.MetaData == null)
			{
				throw SQL.InvalidRead();
			}
			if (columnIndex < 0 || columnIndex >= this._metaData.Length)
			{
				throw ADP.IndexOutOfRange();
			}
		}

		// Token: 0x06001C41 RID: 7233 RVA: 0x000C6BA8 File Offset: 0x000C5FA8
		private void CheckDataIsReady()
		{
			if (this._currentTask != null)
			{
				throw ADP.AsyncOperationPending();
			}
			if (!this._sharedState._dataReady || this._metaData == null)
			{
				throw SQL.InvalidRead();
			}
		}

		// Token: 0x06001C42 RID: 7234 RVA: 0x000C6BE0 File Offset: 0x000C5FE0
		private void CheckHeaderIsReady(int columnIndex, bool permitAsync = false, string methodName = null)
		{
			if (this._isClosed)
			{
				throw ADP.DataReaderClosed(methodName ?? "CheckHeaderIsReady");
			}
			if (!permitAsync && this._currentTask != null)
			{
				throw ADP.AsyncOperationPending();
			}
			if (!this._sharedState._dataReady || this._metaData == null)
			{
				throw SQL.InvalidRead();
			}
			if (columnIndex < 0 || columnIndex >= this._metaData.Length)
			{
				throw ADP.IndexOutOfRange();
			}
			if (this.IsCommandBehavior(CommandBehavior.SequentialAccess) && (this._sharedState._nextColumnHeaderToRead > columnIndex + 1 || this._lastColumnWithDataChunkRead > columnIndex))
			{
				throw ADP.NonSequentialColumnAccess(columnIndex, Math.Max(this._sharedState._nextColumnHeaderToRead - 1, this._lastColumnWithDataChunkRead));
			}
		}

		// Token: 0x06001C43 RID: 7235 RVA: 0x000C6C8C File Offset: 0x000C608C
		private void CheckDataIsReady(int columnIndex, bool allowPartiallyReadColumn = false, bool permitAsync = false, string methodName = null)
		{
			if (this._isClosed)
			{
				throw ADP.DataReaderClosed(methodName ?? "CheckDataIsReady");
			}
			if (!permitAsync && this._currentTask != null)
			{
				throw ADP.AsyncOperationPending();
			}
			if (!this._sharedState._dataReady || this._metaData == null)
			{
				throw SQL.InvalidRead();
			}
			if (columnIndex < 0 || columnIndex >= this._metaData.Length)
			{
				throw ADP.IndexOutOfRange();
			}
			if (this.IsCommandBehavior(CommandBehavior.SequentialAccess) && (this._sharedState._nextColumnDataToRead > columnIndex || this._lastColumnWithDataChunkRead > columnIndex || (!allowPartiallyReadColumn && this._lastColumnWithDataChunkRead == columnIndex) || (allowPartiallyReadColumn && this.HasActiveStreamOrTextReaderOnColumn(columnIndex))))
			{
				throw ADP.NonSequentialColumnAccess(columnIndex, Math.Max(this._sharedState._nextColumnDataToRead, this._lastColumnWithDataChunkRead + 1));
			}
		}

		// Token: 0x06001C44 RID: 7236 RVA: 0x000C6D50 File Offset: 0x000C6150
		[Conditional("DEBUG")]
		private void AssertReaderState(bool requireData, bool permitAsync, int? columnIndex = null, bool enforceSequentialAccess = false)
		{
			bool flag = columnIndex != null;
		}

		// Token: 0x06001C45 RID: 7237 RVA: 0x000C6D68 File Offset: 0x000C6168
		public override Task<bool> NextResultAsync(CancellationToken cancellationToken)
		{
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<sc.SqlDataReader.NextResultAsync|API> %d#", this.ObjectID);
			Task<bool> result;
			try
			{
				TaskCompletionSource<bool> taskCompletionSource = new TaskCompletionSource<bool>();
				if (this.IsClosed)
				{
					taskCompletionSource.SetException(ADP.ExceptionWithStackTrace(ADP.DataReaderClosed("NextResultAsync")));
					result = taskCompletionSource.Task;
				}
				else
				{
					IDisposable objectToDispose = null;
					if (cancellationToken.CanBeCanceled)
					{
						if (cancellationToken.IsCancellationRequested)
						{
							taskCompletionSource.SetCanceled();
							return taskCompletionSource.Task;
						}
						objectToDispose = cancellationToken.Register(new Action(this._command.CancelIgnoreFailure));
					}
					Task task = Interlocked.CompareExchange<Task>(ref this._currentTask, taskCompletionSource.Task, null);
					if (task != null)
					{
						taskCompletionSource.SetException(ADP.ExceptionWithStackTrace(SQL.PendingBeginXXXExists()));
						result = taskCompletionSource.Task;
					}
					else if (this._cancelAsyncOnCloseToken.IsCancellationRequested)
					{
						taskCompletionSource.SetCanceled();
						this._currentTask = null;
						result = taskCompletionSource.Task;
					}
					else
					{
						this.PrepareAsyncInvocation(true);
						Func<Task, Task<bool>> moreFunc = null;
						moreFunc = delegate(Task t)
						{
							if (t != null)
							{
								Bid.Trace("<sc.SqlDataReader.NextResultAsync> attempt retry %d#\n", this.ObjectID);
								this.PrepareForAsyncContinuation();
							}
							bool flag;
							if (!this.TryNextResult(out flag))
							{
								return this.ContinueRetryable<bool>(moreFunc);
							}
							if (!flag)
							{
								return ADP.FalseTask;
							}
							return ADP.TrueTask;
						};
						result = this.InvokeRetryable<bool>(moreFunc, taskCompletionSource, objectToDispose);
					}
				}
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
			return result;
		}

		// Token: 0x06001C46 RID: 7238 RVA: 0x000C6EB4 File Offset: 0x000C62B4
		internal Task<int> GetBytesAsync(int i, byte[] buffer, int index, int length, int timeout, CancellationToken cancellationToken, out int bytesRead)
		{
			bytesRead = 0;
			if (this.IsClosed)
			{
				TaskCompletionSource<int> taskCompletionSource = new TaskCompletionSource<int>();
				taskCompletionSource.SetException(ADP.ExceptionWithStackTrace(ADP.DataReaderClosed("GetBytesAsync")));
				return taskCompletionSource.Task;
			}
			if (this._currentTask != null)
			{
				TaskCompletionSource<int> taskCompletionSource2 = new TaskCompletionSource<int>();
				taskCompletionSource2.SetException(ADP.ExceptionWithStackTrace(ADP.AsyncOperationPending()));
				return taskCompletionSource2.Task;
			}
			if (cancellationToken.CanBeCanceled && cancellationToken.IsCancellationRequested)
			{
				return null;
			}
			if (this._sharedState._nextColumnHeaderToRead > this._lastColumnWithDataChunkRead && this._sharedState._nextColumnDataToRead >= this._lastColumnWithDataChunkRead)
			{
				this.PrepareAsyncInvocation(false);
				Task<int> bytesAsyncReadDataStage;
				try
				{
					bytesAsyncReadDataStage = this.GetBytesAsyncReadDataStage(i, buffer, index, length, timeout, false, cancellationToken, CancellationToken.None, out bytesRead);
				}
				catch
				{
					this.CleanupAfterAsyncInvocation(false);
					throw;
				}
				return bytesAsyncReadDataStage;
			}
			TaskCompletionSource<int> taskCompletionSource3 = new TaskCompletionSource<int>();
			Task task = Interlocked.CompareExchange<Task>(ref this._currentTask, taskCompletionSource3.Task, null);
			if (task != null)
			{
				taskCompletionSource3.SetException(ADP.ExceptionWithStackTrace(ADP.AsyncOperationPending()));
				return taskCompletionSource3.Task;
			}
			this.PrepareAsyncInvocation(true);
			Func<Task, Task<int>> moreFunc = null;
			CancellationToken timeoutToken = CancellationToken.None;
			CancellationTokenSource cancellationTokenSource = null;
			if (timeout > 0)
			{
				cancellationTokenSource = new CancellationTokenSource();
				cancellationTokenSource.CancelAfter(timeout);
				timeoutToken = cancellationTokenSource.Token;
			}
			moreFunc = delegate(Task t)
			{
				if (t != null)
				{
					Bid.Trace("<sc.SqlDataReader.GetBytesAsync> attempt retry %d#\n", this.ObjectID);
					this.PrepareForAsyncContinuation();
				}
				this.SetTimeout(this._defaultTimeoutMilliseconds);
				if (!this.TryReadColumnHeader(i))
				{
					return this.ContinueRetryable<int>(moreFunc);
				}
				if (cancellationToken.IsCancellationRequested)
				{
					return ADP.CreatedTaskWithCancellation<int>();
				}
				if (timeoutToken.IsCancellationRequested)
				{
					return ADP.CreatedTaskWithException<int>(ADP.ExceptionWithStackTrace(ADP.IO(SQLMessage.Timeout())));
				}
				this.SwitchToAsyncWithoutSnapshot();
				int result;
				Task<int> bytesAsyncReadDataStage2 = this.GetBytesAsyncReadDataStage(i, buffer, index, length, timeout, true, cancellationToken, timeoutToken, out result);
				if (bytesAsyncReadDataStage2 == null)
				{
					return Task.FromResult<int>(result);
				}
				return bytesAsyncReadDataStage2;
			};
			return this.InvokeRetryable<int>(moreFunc, taskCompletionSource3, cancellationTokenSource);
		}

		// Token: 0x06001C47 RID: 7239 RVA: 0x000C7098 File Offset: 0x000C6498
		private Task<int> GetBytesAsyncReadDataStage(int i, byte[] buffer, int index, int length, int timeout, bool isContinuation, CancellationToken cancellationToken, CancellationToken timeoutToken, out int bytesRead)
		{
			SqlDataReader.<>c__DisplayClass188_0 CS$<>8__locals1 = new SqlDataReader.<>c__DisplayClass188_0();
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.cancellationToken = cancellationToken;
			CS$<>8__locals1.timeoutToken = timeoutToken;
			CS$<>8__locals1.i = i;
			CS$<>8__locals1.buffer = buffer;
			CS$<>8__locals1.index = index;
			CS$<>8__locals1.length = length;
			this._lastColumnWithDataChunkRead = CS$<>8__locals1.i;
			CS$<>8__locals1.source = null;
			CS$<>8__locals1.timeoutCancellationSource = null;
			this.SetTimeout(this._defaultTimeoutMilliseconds);
			if (this.TryGetBytesInternalSequential(CS$<>8__locals1.i, CS$<>8__locals1.buffer, CS$<>8__locals1.index, CS$<>8__locals1.length, out bytesRead))
			{
				if (!isContinuation)
				{
					this.CleanupAfterAsyncInvocation(false);
				}
				return null;
			}
			int totalBytesRead = bytesRead;
			if (!isContinuation)
			{
				CS$<>8__locals1.source = new TaskCompletionSource<int>();
				Task task = Interlocked.CompareExchange<Task>(ref this._currentTask, CS$<>8__locals1.source.Task, null);
				if (task != null)
				{
					CS$<>8__locals1.source.SetException(ADP.ExceptionWithStackTrace(ADP.AsyncOperationPending()));
					return CS$<>8__locals1.source.Task;
				}
				if (this._cancelAsyncOnCloseToken.IsCancellationRequested)
				{
					CS$<>8__locals1.source.SetCanceled();
					this._currentTask = null;
					return CS$<>8__locals1.source.Task;
				}
				if (timeout > 0)
				{
					CS$<>8__locals1.timeoutCancellationSource = new CancellationTokenSource();
					CS$<>8__locals1.timeoutCancellationSource.CancelAfter(timeout);
					CS$<>8__locals1.timeoutToken = CS$<>8__locals1.timeoutCancellationSource.Token;
				}
			}
			Func<Task, Task<int>> moreFunc = null;
			moreFunc = delegate(Task _)
			{
				CS$<>8__locals1.<>4__this.PrepareForAsyncContinuation();
				if (CS$<>8__locals1.cancellationToken.IsCancellationRequested)
				{
					return ADP.CreatedTaskWithCancellation<int>();
				}
				if (CS$<>8__locals1.timeoutToken.IsCancellationRequested)
				{
					return ADP.CreatedTaskWithException<int>(ADP.ExceptionWithStackTrace(ADP.IO(SQLMessage.Timeout())));
				}
				CS$<>8__locals1.<>4__this.SetTimeout(CS$<>8__locals1.<>4__this._defaultTimeoutMilliseconds);
				int num;
				bool flag = CS$<>8__locals1.<>4__this.TryGetBytesInternalSequential(CS$<>8__locals1.i, CS$<>8__locals1.buffer, CS$<>8__locals1.index + totalBytesRead, CS$<>8__locals1.length - totalBytesRead, out num);
				totalBytesRead += num;
				if (flag)
				{
					return Task.FromResult<int>(totalBytesRead);
				}
				return CS$<>8__locals1.<>4__this.ContinueRetryable<int>(moreFunc);
			};
			Task<int> task2 = this.ContinueRetryable<int>(moreFunc);
			if (isContinuation)
			{
				return task2;
			}
			task2.ContinueWith(delegate(Task<int> t)
			{
				CS$<>8__locals1.<>4__this.CompleteRetryable<int>(t, CS$<>8__locals1.source, CS$<>8__locals1.timeoutCancellationSource);
			}, TaskScheduler.Default);
			return CS$<>8__locals1.source.Task;
		}

		// Token: 0x06001C48 RID: 7240 RVA: 0x000C7280 File Offset: 0x000C6680
		public override Task<bool> ReadAsync(CancellationToken cancellationToken)
		{
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<sc.SqlDataReader.ReadAsync|API> %d#", this.ObjectID);
			Task<bool> result;
			try
			{
				if (this.IsClosed)
				{
					result = ADP.CreatedTaskWithException<bool>(ADP.ExceptionWithStackTrace(ADP.DataReaderClosed("ReadAsync")));
				}
				else if (cancellationToken.IsCancellationRequested)
				{
					result = ADP.CreatedTaskWithCancellation<bool>();
				}
				else if (this._currentTask != null)
				{
					result = ADP.CreatedTaskWithException<bool>(ADP.ExceptionWithStackTrace(SQL.PendingBeginXXXExists()));
				}
				else
				{
					bool rowTokenRead = false;
					bool more = false;
					try
					{
						if (!this._haltRead && (!this._sharedState._dataReady || this.WillHaveEnoughData(this._metaData.Length - 1, false)))
						{
							if (this._sharedState._dataReady)
							{
								this.CleanPartialReadReliable();
							}
							if (this._stateObj.IsRowTokenReady())
							{
								bool flag = this.TryReadInternal(true, out more);
								rowTokenRead = true;
								if (!more)
								{
									return ADP.FalseTask;
								}
								if (this.IsCommandBehavior(CommandBehavior.SequentialAccess))
								{
									return ADP.TrueTask;
								}
								if (this.WillHaveEnoughData(this._metaData.Length - 1, false))
								{
									flag = this.TryReadColumn(this._metaData.Length - 1, true, false);
									return ADP.TrueTask;
								}
							}
						}
					}
					catch (Exception ex)
					{
						if (!ADP.IsCatchableExceptionType(ex))
						{
							throw;
						}
						return ADP.CreatedTaskWithException<bool>(ex);
					}
					TaskCompletionSource<bool> taskCompletionSource = new TaskCompletionSource<bool>();
					Task task = Interlocked.CompareExchange<Task>(ref this._currentTask, taskCompletionSource.Task, null);
					if (task != null)
					{
						taskCompletionSource.SetException(ADP.ExceptionWithStackTrace(SQL.PendingBeginXXXExists()));
						result = taskCompletionSource.Task;
					}
					else if (this._cancelAsyncOnCloseToken.IsCancellationRequested)
					{
						taskCompletionSource.SetCanceled();
						this._currentTask = null;
						result = taskCompletionSource.Task;
					}
					else
					{
						IDisposable objectToDispose = null;
						if (cancellationToken.CanBeCanceled)
						{
							objectToDispose = cancellationToken.Register(new Action(this._command.CancelIgnoreFailure));
						}
						this.PrepareAsyncInvocation(true);
						Func<Task, Task<bool>> moreFunc = null;
						moreFunc = delegate(Task t)
						{
							if (t != null)
							{
								Bid.Trace("<sc.SqlDataReader.ReadAsync> attempt retry %d#\n", this.ObjectID);
								this.PrepareForAsyncContinuation();
							}
							if (rowTokenRead || this.TryReadInternal(true, out more))
							{
								if (!more || (this._commandBehavior & CommandBehavior.SequentialAccess) == CommandBehavior.SequentialAccess)
								{
									if (!more)
									{
										return ADP.FalseTask;
									}
									return ADP.TrueTask;
								}
								else
								{
									if (!rowTokenRead)
									{
										rowTokenRead = true;
										this._snapshot = null;
										this.PrepareAsyncInvocation(true);
									}
									if (this.TryReadColumn(this._metaData.Length - 1, true, false))
									{
										return ADP.TrueTask;
									}
								}
							}
							return this.ContinueRetryable<bool>(moreFunc);
						};
						result = this.InvokeRetryable<bool>(moreFunc, taskCompletionSource, objectToDispose);
					}
				}
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
			return result;
		}

		// Token: 0x06001C49 RID: 7241 RVA: 0x000C74E4 File Offset: 0x000C68E4
		public override Task<bool> IsDBNullAsync(int i, CancellationToken cancellationToken)
		{
			try
			{
				this.CheckHeaderIsReady(i, false, "IsDBNullAsync");
			}
			catch (Exception ex)
			{
				if (!ADP.IsCatchableExceptionType(ex))
				{
					throw;
				}
				return ADP.CreatedTaskWithException<bool>(ex);
			}
			if (this._sharedState._nextColumnHeaderToRead > i && !cancellationToken.IsCancellationRequested && this._currentTask == null)
			{
				SqlBuffer[] data = this._data;
				if (data == null)
				{
					return ADP.CreatedTaskWithException<bool>(ADP.ExceptionWithStackTrace(ADP.DataReaderClosed("IsDBNullAsync")));
				}
				if (!data[i].IsNull)
				{
					return ADP.FalseTask;
				}
				return ADP.TrueTask;
			}
			else
			{
				if (this._currentTask != null)
				{
					return ADP.CreatedTaskWithException<bool>(ADP.ExceptionWithStackTrace(ADP.AsyncOperationPending()));
				}
				if (cancellationToken.IsCancellationRequested)
				{
					return ADP.CreatedTaskWithCancellation<bool>();
				}
				try
				{
					if (this.WillHaveEnoughData(i, true))
					{
						this.ReadColumnHeader(i);
						return this._data[i].IsNull ? ADP.TrueTask : ADP.FalseTask;
					}
				}
				catch (Exception ex2)
				{
					if (!ADP.IsCatchableExceptionType(ex2))
					{
						throw;
					}
					return ADP.CreatedTaskWithException<bool>(ex2);
				}
				TaskCompletionSource<bool> taskCompletionSource = new TaskCompletionSource<bool>();
				Task task = Interlocked.CompareExchange<Task>(ref this._currentTask, taskCompletionSource.Task, null);
				if (task != null)
				{
					taskCompletionSource.SetException(ADP.ExceptionWithStackTrace(ADP.AsyncOperationPending()));
					return taskCompletionSource.Task;
				}
				if (this._cancelAsyncOnCloseToken.IsCancellationRequested)
				{
					taskCompletionSource.SetCanceled();
					this._currentTask = null;
					return taskCompletionSource.Task;
				}
				IDisposable objectToDispose = null;
				if (cancellationToken.CanBeCanceled)
				{
					objectToDispose = cancellationToken.Register(new Action(this._command.CancelIgnoreFailure));
				}
				this.PrepareAsyncInvocation(true);
				Func<Task, Task<bool>> moreFunc = null;
				moreFunc = delegate(Task t)
				{
					if (t != null)
					{
						this.PrepareForAsyncContinuation();
					}
					if (!this.TryReadColumnHeader(i))
					{
						return this.ContinueRetryable<bool>(moreFunc);
					}
					if (!this._data[i].IsNull)
					{
						return ADP.FalseTask;
					}
					return ADP.TrueTask;
				};
				return this.InvokeRetryable<bool>(moreFunc, taskCompletionSource, objectToDispose);
			}
			Task<bool> result;
			return result;
		}

		// Token: 0x06001C4A RID: 7242 RVA: 0x000C76FC File Offset: 0x000C6AFC
		public override Task<T> GetFieldValueAsync<T>(int i, CancellationToken cancellationToken)
		{
			try
			{
				this.CheckDataIsReady(i, false, false, "GetFieldValueAsync");
				if (!this.IsCommandBehavior(CommandBehavior.SequentialAccess) && this._sharedState._nextColumnDataToRead > i && !cancellationToken.IsCancellationRequested && this._currentTask == null)
				{
					SqlBuffer[] data = this._data;
					_SqlMetaDataSet metaData = this._metaData;
					if (data != null && metaData != null)
					{
						return Task.FromResult<T>(this.GetFieldValueFromSqlBufferInternal<T>(data[i], metaData[i]));
					}
					return ADP.CreatedTaskWithException<T>(ADP.ExceptionWithStackTrace(ADP.DataReaderClosed("GetFieldValueAsync")));
				}
			}
			catch (Exception ex)
			{
				if (!ADP.IsCatchableExceptionType(ex))
				{
					throw;
				}
				return ADP.CreatedTaskWithException<T>(ex);
			}
			if (this._currentTask != null)
			{
				return ADP.CreatedTaskWithException<T>(ADP.ExceptionWithStackTrace(ADP.AsyncOperationPending()));
			}
			if (cancellationToken.IsCancellationRequested)
			{
				return ADP.CreatedTaskWithCancellation<T>();
			}
			try
			{
				if (this.WillHaveEnoughData(i, false))
				{
					return Task.FromResult<T>(this.GetFieldValueInternal<T>(i));
				}
			}
			catch (Exception ex2)
			{
				if (!ADP.IsCatchableExceptionType(ex2))
				{
					throw;
				}
				return ADP.CreatedTaskWithException<T>(ex2);
			}
			TaskCompletionSource<T> taskCompletionSource = new TaskCompletionSource<T>();
			Task task = Interlocked.CompareExchange<Task>(ref this._currentTask, taskCompletionSource.Task, null);
			if (task != null)
			{
				taskCompletionSource.SetException(ADP.ExceptionWithStackTrace(ADP.AsyncOperationPending()));
				return taskCompletionSource.Task;
			}
			if (this._cancelAsyncOnCloseToken.IsCancellationRequested)
			{
				taskCompletionSource.SetCanceled();
				this._currentTask = null;
				return taskCompletionSource.Task;
			}
			IDisposable objectToDispose = null;
			if (cancellationToken.CanBeCanceled)
			{
				objectToDispose = cancellationToken.Register(new Action(this._command.CancelIgnoreFailure));
			}
			this.PrepareAsyncInvocation(true);
			Func<Task, Task<T>> moreFunc = null;
			moreFunc = delegate(Task t)
			{
				if (t != null)
				{
					this.PrepareForAsyncContinuation();
				}
				if (this.TryReadColumn(i, false, false))
				{
					return Task.FromResult<T>(this.GetFieldValueFromSqlBufferInternal<T>(this._data[i], this._metaData[i]));
				}
				return this.ContinueRetryable<T>(moreFunc);
			};
			return this.InvokeRetryable<T>(moreFunc, taskCompletionSource, objectToDispose);
		}

		// Token: 0x06001C4B RID: 7243 RVA: 0x000C7924 File Offset: 0x000C6D24
		private Task<T> ContinueRetryable<T>(Func<Task, Task<T>> moreFunc)
		{
			TaskCompletionSource<object> networkPacketTaskSource = this._stateObj._networkPacketTaskSource;
			if (this._cancelAsyncOnCloseToken.IsCancellationRequested || networkPacketTaskSource == null)
			{
				TaskCompletionSource<T> taskCompletionSource = new TaskCompletionSource<T>();
				taskCompletionSource.TrySetException(ADP.ExceptionWithStackTrace(ADP.ClosedConnectionError()));
				return taskCompletionSource.Task;
			}
			return networkPacketTaskSource.Task.ContinueWith<Task<T>>(delegate(Task<object> retryTask)
			{
				if (retryTask.IsFaulted)
				{
					TaskCompletionSource<T> taskCompletionSource2 = new TaskCompletionSource<T>();
					taskCompletionSource2.TrySetException(retryTask.Exception.InnerException);
					return taskCompletionSource2.Task;
				}
				if (!this._cancelAsyncOnCloseToken.IsCancellationRequested)
				{
					TdsParserStateObject stateObj = this._stateObj;
					if (stateObj != null)
					{
						TdsParserStateObject obj = stateObj;
						lock (obj)
						{
							if (this._stateObj != null)
							{
								if (retryTask.IsCanceled)
								{
									if (this._parser != null)
									{
										this._parser.State = TdsParserState.Broken;
										this._parser.Connection.BreakConnection();
										this._parser.ThrowExceptionAndWarning(this._stateObj, false, false);
									}
								}
								else if (!this.IsClosed)
								{
									try
									{
										return moreFunc(retryTask);
									}
									catch (Exception)
									{
										this.CleanupAfterAsyncInvocation(false);
										throw;
									}
								}
							}
						}
					}
				}
				TaskCompletionSource<T> taskCompletionSource3 = new TaskCompletionSource<T>();
				taskCompletionSource3.SetException(ADP.ExceptionWithStackTrace(ADP.ClosedConnectionError()));
				return taskCompletionSource3.Task;
			}, TaskScheduler.Default).Unwrap<T>();
		}

		// Token: 0x06001C4C RID: 7244 RVA: 0x000C79A0 File Offset: 0x000C6DA0
		private Task<T> InvokeRetryable<T>(Func<Task, Task<T>> moreFunc, TaskCompletionSource<T> source, IDisposable objectToDispose = null)
		{
			try
			{
				Task<T> task;
				try
				{
					task = moreFunc(null);
				}
				catch (Exception ex)
				{
					task = ADP.CreatedTaskWithException<T>(ex);
				}
				if (task.IsCompleted)
				{
					this.CompleteRetryable<T>(task, source, objectToDispose);
				}
				else
				{
					task.ContinueWith(delegate(Task<T> t)
					{
						this.CompleteRetryable<T>(t, source, objectToDispose);
					}, TaskScheduler.Default);
				}
			}
			catch (AggregateException ex2)
			{
				source.TrySetException(ex2.InnerException);
			}
			catch (Exception exception)
			{
				source.TrySetException(exception);
			}
			return source.Task;
		}

		// Token: 0x06001C4D RID: 7245 RVA: 0x000C7A94 File Offset: 0x000C6E94
		private void CompleteRetryable<T>(Task<T> task, TaskCompletionSource<T> source, IDisposable objectToDispose)
		{
			if (objectToDispose != null)
			{
				objectToDispose.Dispose();
			}
			TdsParserStateObject stateObj = this._stateObj;
			bool ignoreCloseToken = stateObj != null && stateObj._syncOverAsync;
			this.CleanupAfterAsyncInvocation(ignoreCloseToken);
			Task task2 = Interlocked.CompareExchange<Task>(ref this._currentTask, null, source.Task);
			if (task.IsFaulted)
			{
				Exception innerException = task.Exception.InnerException;
				source.TrySetException(innerException);
				return;
			}
			if (task.IsCanceled)
			{
				source.TrySetCanceled();
				return;
			}
			source.TrySetResult(task.Result);
		}

		// Token: 0x06001C4E RID: 7246 RVA: 0x000C7B14 File Offset: 0x000C6F14
		private void PrepareAsyncInvocation(bool useSnapshot)
		{
			if (useSnapshot)
			{
				if (this._snapshot == null)
				{
					this._snapshot = new SqlDataReader.Snapshot
					{
						_dataReady = this._sharedState._dataReady,
						_haltRead = this._haltRead,
						_metaDataConsumed = this._metaDataConsumed,
						_browseModeInfoConsumed = this._browseModeInfoConsumed,
						_hasRows = this._hasRows,
						_altRowStatus = this._altRowStatus,
						_nextColumnDataToRead = this._sharedState._nextColumnDataToRead,
						_nextColumnHeaderToRead = this._sharedState._nextColumnHeaderToRead,
						_columnDataBytesRead = this._columnDataBytesRead,
						_columnDataBytesRemaining = this._sharedState._columnDataBytesRemaining,
						_metadata = this._metaData,
						_altMetaDataSetCollection = this._altMetaDataSetCollection,
						_tableNames = this._tableNames,
						_currentStream = this._currentStream,
						_currentTextReader = this._currentTextReader
					};
					this._stateObj.SetSnapshot();
				}
			}
			else
			{
				this._stateObj._asyncReadWithoutSnapshot = true;
			}
			this._stateObj._syncOverAsync = false;
			this._stateObj._executionContext = ExecutionContext.Capture();
		}

		// Token: 0x06001C4F RID: 7247 RVA: 0x000C7C3C File Offset: 0x000C703C
		private void CleanupAfterAsyncInvocation(bool ignoreCloseToken = false)
		{
			TdsParserStateObject stateObj = this._stateObj;
			if (stateObj != null && (ignoreCloseToken || !this._cancelAsyncOnCloseToken.IsCancellationRequested || stateObj._asyncReadWithoutSnapshot))
			{
				TdsParserStateObject obj = stateObj;
				lock (obj)
				{
					if (this._stateObj != null)
					{
						this.CleanupAfterAsyncInvocationInternal(this._stateObj, true);
					}
				}
			}
		}

		// Token: 0x06001C50 RID: 7248 RVA: 0x000C7CB4 File Offset: 0x000C70B4
		private void CleanupAfterAsyncInvocationInternal(TdsParserStateObject stateObj, bool resetNetworkPacketTaskSource = true)
		{
			if (resetNetworkPacketTaskSource)
			{
				stateObj._networkPacketTaskSource = null;
			}
			stateObj.ResetSnapshot();
			stateObj._syncOverAsync = true;
			stateObj._executionContext = null;
			stateObj._asyncReadWithoutSnapshot = false;
			this._snapshot = null;
		}

		// Token: 0x06001C51 RID: 7249 RVA: 0x000C7CF0 File Offset: 0x000C70F0
		private void PrepareForAsyncContinuation()
		{
			if (this._snapshot != null)
			{
				this._sharedState._dataReady = this._snapshot._dataReady;
				this._haltRead = this._snapshot._haltRead;
				this._metaDataConsumed = this._snapshot._metaDataConsumed;
				this._browseModeInfoConsumed = this._snapshot._browseModeInfoConsumed;
				this._hasRows = this._snapshot._hasRows;
				this._altRowStatus = this._snapshot._altRowStatus;
				this._sharedState._nextColumnDataToRead = this._snapshot._nextColumnDataToRead;
				this._sharedState._nextColumnHeaderToRead = this._snapshot._nextColumnHeaderToRead;
				this._columnDataBytesRead = this._snapshot._columnDataBytesRead;
				this._sharedState._columnDataBytesRemaining = this._snapshot._columnDataBytesRemaining;
				this._metaData = this._snapshot._metadata;
				this._altMetaDataSetCollection = this._snapshot._altMetaDataSetCollection;
				this._tableNames = this._snapshot._tableNames;
				this._currentStream = this._snapshot._currentStream;
				this._currentTextReader = this._snapshot._currentTextReader;
				this._stateObj.PrepareReplaySnapshot();
			}
			this._stateObj._executionContext = ExecutionContext.Capture();
		}

		// Token: 0x06001C52 RID: 7250 RVA: 0x000C7E38 File Offset: 0x000C7238
		private void SwitchToAsyncWithoutSnapshot()
		{
			this._snapshot = null;
			this._stateObj.ResetSnapshot();
			this._stateObj._asyncReadWithoutSnapshot = true;
		}

		// Token: 0x0400100E RID: 4110
		internal SqlDataReader.SharedState _sharedState = new SqlDataReader.SharedState();

		// Token: 0x0400100F RID: 4111
		private TdsParser _parser;

		// Token: 0x04001010 RID: 4112
		private TdsParserStateObject _stateObj;

		// Token: 0x04001011 RID: 4113
		private SqlCommand _command;

		// Token: 0x04001012 RID: 4114
		private SqlConnection _connection;

		// Token: 0x04001013 RID: 4115
		private int _defaultLCID;

		// Token: 0x04001014 RID: 4116
		private bool _haltRead;

		// Token: 0x04001015 RID: 4117
		private bool _metaDataConsumed;

		// Token: 0x04001016 RID: 4118
		private bool _browseModeInfoConsumed;

		// Token: 0x04001017 RID: 4119
		private bool _isClosed;

		// Token: 0x04001018 RID: 4120
		private bool _isInitialized;

		// Token: 0x04001019 RID: 4121
		private bool _hasRows;

		// Token: 0x0400101A RID: 4122
		private SqlDataReader.ALTROWSTATUS _altRowStatus;

		// Token: 0x0400101B RID: 4123
		private int _recordsAffected = -1;

		// Token: 0x0400101C RID: 4124
		private long _defaultTimeoutMilliseconds;

		// Token: 0x0400101D RID: 4125
		private SqlConnectionString.TypeSystem _typeSystem;

		// Token: 0x0400101E RID: 4126
		private SqlStatistics _statistics;

		// Token: 0x0400101F RID: 4127
		private SqlBuffer[] _data;

		// Token: 0x04001020 RID: 4128
		private SqlStreamingXml _streamingXml;

		// Token: 0x04001021 RID: 4129
		private _SqlMetaDataSet _metaData;

		// Token: 0x04001022 RID: 4130
		private _SqlMetaDataSetCollection _altMetaDataSetCollection;

		// Token: 0x04001023 RID: 4131
		private FieldNameLookup _fieldNameLookup;

		// Token: 0x04001024 RID: 4132
		private CommandBehavior _commandBehavior;

		// Token: 0x04001025 RID: 4133
		private static int _objectTypeCount;

		// Token: 0x04001026 RID: 4134
		internal readonly int ObjectID = Interlocked.Increment(ref SqlDataReader._objectTypeCount);

		// Token: 0x04001027 RID: 4135
		private MultiPartTableName[] _tableNames;

		// Token: 0x04001028 RID: 4136
		private string _resetOptionsString;

		// Token: 0x04001029 RID: 4137
		private int _lastColumnWithDataChunkRead;

		// Token: 0x0400102A RID: 4138
		private long _columnDataBytesRead;

		// Token: 0x0400102B RID: 4139
		private long _columnDataCharsRead;

		// Token: 0x0400102C RID: 4140
		private char[] _columnDataChars;

		// Token: 0x0400102D RID: 4141
		private int _columnDataCharsIndex;

		// Token: 0x0400102E RID: 4142
		private Task _currentTask;

		// Token: 0x0400102F RID: 4143
		private SqlDataReader.Snapshot _snapshot;

		// Token: 0x04001030 RID: 4144
		private CancellationTokenSource _cancelAsyncOnCloseTokenSource;

		// Token: 0x04001031 RID: 4145
		private CancellationToken _cancelAsyncOnCloseToken;

		// Token: 0x04001032 RID: 4146
		internal static readonly Type _typeofINullable = typeof(INullable);

		// Token: 0x04001033 RID: 4147
		private static readonly Type _typeofSqlString = typeof(SqlString);

		// Token: 0x04001034 RID: 4148
		private SqlSequentialStream _currentStream;

		// Token: 0x04001035 RID: 4149
		private SqlSequentialTextReader _currentTextReader;

		// Token: 0x020003B2 RID: 946
		private enum ALTROWSTATUS
		{
			// Token: 0x04002083 RID: 8323
			Null,
			// Token: 0x04002084 RID: 8324
			AltRow,
			// Token: 0x04002085 RID: 8325
			Done
		}

		// Token: 0x020003B3 RID: 947
		internal class SharedState
		{
			// Token: 0x04002086 RID: 8326
			internal int _nextColumnHeaderToRead;

			// Token: 0x04002087 RID: 8327
			internal int _nextColumnDataToRead;

			// Token: 0x04002088 RID: 8328
			internal long _columnDataBytesRemaining;

			// Token: 0x04002089 RID: 8329
			internal bool _dataReady;
		}

		// Token: 0x020003B4 RID: 948
		private class Snapshot
		{
			// Token: 0x0400208A RID: 8330
			public bool _dataReady;

			// Token: 0x0400208B RID: 8331
			public bool _haltRead;

			// Token: 0x0400208C RID: 8332
			public bool _metaDataConsumed;

			// Token: 0x0400208D RID: 8333
			public bool _browseModeInfoConsumed;

			// Token: 0x0400208E RID: 8334
			public bool _hasRows;

			// Token: 0x0400208F RID: 8335
			public SqlDataReader.ALTROWSTATUS _altRowStatus;

			// Token: 0x04002090 RID: 8336
			public int _nextColumnDataToRead;

			// Token: 0x04002091 RID: 8337
			public int _nextColumnHeaderToRead;

			// Token: 0x04002092 RID: 8338
			public long _columnDataBytesRead;

			// Token: 0x04002093 RID: 8339
			public long _columnDataBytesRemaining;

			// Token: 0x04002094 RID: 8340
			public _SqlMetaDataSet _metadata;

			// Token: 0x04002095 RID: 8341
			public _SqlMetaDataSetCollection _altMetaDataSetCollection;

			// Token: 0x04002096 RID: 8342
			public MultiPartTableName[] _tableNames;

			// Token: 0x04002097 RID: 8343
			public SqlSequentialStream _currentStream;

			// Token: 0x04002098 RID: 8344
			public SqlSequentialTextReader _currentTextReader;
		}
	}
}
