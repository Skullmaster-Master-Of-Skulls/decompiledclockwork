using System;
using System.Collections;
using System.ComponentModel;
using System.Data.Common;
using System.Data.ProviderBase;
using System.Data.SqlTypes;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.SqlServer.Server;

namespace System.Data.SqlClient
{
	// Token: 0x020002DE RID: 734
	public class SqlDataReader : DbDataReader, IDataReader, IDisposable, IDataRecord
	{
		// Token: 0x060025C4 RID: 9668 RVA: 0x0029D438 File Offset: 0x0029C838
		internal SqlDataReader(SqlCommand command, CommandBehavior behavior)
		{
			this._command = command;
			this._commandBehavior = behavior;
			if (this._command != null)
			{
				this._timeoutSeconds = command.CommandTimeout;
				this._connection = command.Connection;
				if (this._connection != null)
				{
					this._statistics = this._connection.Statistics;
					this._typeSystem = this._connection.TypeSystem;
				}
			}
			this._dataReady = false;
			this._metaDataConsumed = false;
			this._hasRows = false;
			this._browseModeInfoConsumed = false;
		}

		// Token: 0x170005F4 RID: 1524
		// (set) Token: 0x060025C5 RID: 9669 RVA: 0x0029D4D8 File Offset: 0x0029C8D8
		internal bool BrowseModeInfoConsumed
		{
			set
			{
				this._browseModeInfoConsumed = value;
			}
		}

		// Token: 0x170005F5 RID: 1525
		// (get) Token: 0x060025C6 RID: 9670 RVA: 0x0029D4F8 File Offset: 0x0029C8F8
		internal SqlCommand Command
		{
			get
			{
				return this._command;
			}
		}

		// Token: 0x170005F6 RID: 1526
		// (get) Token: 0x060025C7 RID: 9671 RVA: 0x0029D518 File Offset: 0x0029C918
		protected SqlConnection Connection
		{
			get
			{
				return this._connection;
			}
		}

		// Token: 0x170005F7 RID: 1527
		// (get) Token: 0x060025C8 RID: 9672 RVA: 0x0029D538 File Offset: 0x0029C938
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

		// Token: 0x170005F8 RID: 1528
		// (get) Token: 0x060025C9 RID: 9673 RVA: 0x0029D568 File Offset: 0x0029C968
		public override int FieldCount
		{
			get
			{
				if (this.IsClosed)
				{
					throw ADP.DataReaderClosed("FieldCount");
				}
				if (this.MetaData == null)
				{
					return 0;
				}
				return this._metaData.Length;
			}
		}

		// Token: 0x170005F9 RID: 1529
		// (get) Token: 0x060025CA RID: 9674 RVA: 0x0029D5A8 File Offset: 0x0029C9A8
		public override bool HasRows
		{
			get
			{
				if (this.IsClosed)
				{
					throw ADP.DataReaderClosed("HasRows");
				}
				return this._hasRows;
			}
		}

		// Token: 0x170005FA RID: 1530
		// (get) Token: 0x060025CB RID: 9675 RVA: 0x0029D5D8 File Offset: 0x0029C9D8
		public override bool IsClosed
		{
			get
			{
				return this._isClosed;
			}
		}

		// Token: 0x170005FB RID: 1531
		// (get) Token: 0x060025CC RID: 9676 RVA: 0x0029D5F8 File Offset: 0x0029C9F8
		// (set) Token: 0x060025CD RID: 9677 RVA: 0x0029D618 File Offset: 0x0029CA18
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

		// Token: 0x170005FC RID: 1532
		// (get) Token: 0x060025CE RID: 9678 RVA: 0x0029D638 File Offset: 0x0029CA38
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
					RuntimeHelpers.PrepareConstrainedRegions();
					try
					{
						this.ConsumeMetaData();
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

		// Token: 0x060025CF RID: 9679 RVA: 0x0029D738 File Offset: 0x0029CB38
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
							SqlConnection.CheckGetExtendedUDTInfo(sqlMetaData, true);
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
							num /= ADP.CharSize;
						}
						array[i] = new SmiQueryMetaData(sqlMetaData.type, (long)num, sqlMetaData.precision, sqlMetaData.scale, (long)((collation != null) ? collation.LCID : this._defaultLCID), (collation != null) ? collation.SqlCompareOptions : SqlCompareOptions.None, sqlMetaData.udtType, false, null, null, sqlMetaData.column, typeSpecificNamePart, typeSpecificNamePart2, typeSpecificNamePart3, sqlMetaData.isNullable, sqlMetaData.serverName, sqlMetaData.catalogName, sqlMetaData.schemaName, sqlMetaData.tableName, sqlMetaData.baseColumn, sqlMetaData.isKey, sqlMetaData.isIdentity, 0 == sqlMetaData.updatability, sqlMetaData.isExpression, sqlMetaData.isDifferentName, sqlMetaData.isHidden);
					}
				}
			}
			return array;
		}

		// Token: 0x170005FD RID: 1533
		// (get) Token: 0x060025D0 RID: 9680 RVA: 0x0029D8E8 File Offset: 0x0029CCE8
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

		// Token: 0x170005FE RID: 1534
		// (set) Token: 0x060025D1 RID: 9681 RVA: 0x0029D918 File Offset: 0x0029CD18
		internal string ResetOptionsString
		{
			set
			{
				this._resetOptionsString = value;
			}
		}

		// Token: 0x170005FF RID: 1535
		// (get) Token: 0x060025D2 RID: 9682 RVA: 0x0029D938 File Offset: 0x0029CD38
		private SqlStatistics Statistics
		{
			get
			{
				return this._statistics;
			}
		}

		// Token: 0x17000600 RID: 1536
		// (get) Token: 0x060025D3 RID: 9683 RVA: 0x0029D958 File Offset: 0x0029CD58
		// (set) Token: 0x060025D4 RID: 9684 RVA: 0x0029D978 File Offset: 0x0029CD78
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

		// Token: 0x17000601 RID: 1537
		// (get) Token: 0x060025D5 RID: 9685 RVA: 0x0029D998 File Offset: 0x0029CD98
		public override int VisibleFieldCount
		{
			get
			{
				if (this.IsClosed)
				{
					throw ADP.DataReaderClosed("VisibleFieldCount");
				}
				if (this.MetaData == null)
				{
					return 0;
				}
				return this.MetaData.visibleColumns;
			}
		}

		// Token: 0x17000602 RID: 1538
		public override object this[int i]
		{
			get
			{
				return this.GetValue(i);
			}
		}

		// Token: 0x17000603 RID: 1539
		public override object this[string name]
		{
			get
			{
				return this.GetValue(this.GetOrdinal(name));
			}
		}

		// Token: 0x060025D8 RID: 9688 RVA: 0x0029DA18 File Offset: 0x0029CE18
		internal void Bind(TdsParserStateObject stateObj)
		{
			stateObj.Owner = this;
			this._stateObj = stateObj;
			this._parser = stateObj.Parser;
			this._defaultLCID = this._parser.DefaultLCID;
		}

		// Token: 0x060025D9 RID: 9689 RVA: 0x0029DA58 File Offset: 0x0029CE58
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
				dataRow[column2] = ((sqlMetaData.metaType.IsSizeInCharacters && sqlMetaData.length != int.MaxValue) ? (sqlMetaData.length / 2) : sqlMetaData.length);
				dataRow[column5] = this.GetFieldTypeInternal(sqlMetaData);
				dataRow[column6] = this.GetProviderSpecificFieldTypeInternal(sqlMetaData);
				dataRow[column7] = (int)sqlMetaData.type;
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
					dataRow[column8] = (int)sqlMetaData.type;
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
				if (255 != sqlMetaData.precision)
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
				dataRow[dataColumn2] = sqlMetaData.metaType.IsLong;
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
				dataRow[column10] = (0 == sqlMetaData.updatability);
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

		// Token: 0x060025DA RID: 9690 RVA: 0x0029E3C8 File Offset: 0x0029D7C8
		internal void Cancel(int objectID)
		{
			TdsParserStateObject stateObj = this._stateObj;
			if (stateObj != null)
			{
				stateObj.Cancel(objectID);
			}
		}

		// Token: 0x060025DB RID: 9691 RVA: 0x0029E3E8 File Offset: 0x0029D7E8
		private void CleanPartialRead()
		{
			if (this._nextColumnHeaderToRead == 0)
			{
				this._stateObj.Parser.SkipRow(this._metaData, this._stateObj);
				return;
			}
			this.ResetBlobState();
			this._stateObj.Parser.SkipRow(this._metaData, this._nextColumnHeaderToRead, this._stateObj);
		}

		// Token: 0x060025DC RID: 9692 RVA: 0x0029E448 File Offset: 0x0029D848
		public override void Close()
		{
			SqlStatistics statistics = null;
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<sc.SqlDataReader.Close|API> %d#", this.ObjectID);
			try
			{
				statistics = SqlStatistics.StartTimer(this.Statistics);
				if (!this.IsClosed)
				{
					this.SetTimeout();
					this.CloseInternal(true);
				}
			}
			finally
			{
				SqlStatistics.StopTimer(statistics);
				Bid.ScopeLeave(ref intPtr);
			}
		}

		// Token: 0x060025DD RID: 9693 RVA: 0x0029E4B8 File Offset: 0x0029D8B8
		private void CloseInternal(bool closeReader)
		{
			TdsParser parser = this._parser;
			TdsParserStateObject stateObj = this._stateObj;
			bool flag = this.IsCommandBehavior(CommandBehavior.CloseConnection);
			this._parser = null;
			bool flag2 = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				if (parser != null && stateObj != null && stateObj._pendingData && parser.State == TdsParserState.OpenLoggedIn)
				{
					if (this._altRowStatus == SqlDataReader.ALTROWSTATUS.AltRow)
					{
						this._dataReady = true;
					}
					if (this._dataReady)
					{
						this.CleanPartialRead();
					}
					parser.Run(RunBehavior.Clean, this._command, this, null, stateObj);
				}
				this.RestoreServerSettings(parser, stateObj);
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
				}
				else if (closeReader)
				{
					this._stateObj = null;
					this._data = null;
					if (this.Connection != null)
					{
						this.Connection.RemoveWeakReference(this);
					}
					RuntimeHelpers.PrepareConstrainedRegions();
					try
					{
						if (this._command != null && stateObj != null)
						{
							stateObj.CloseSession();
						}
					}
					catch (OutOfMemoryException e4)
					{
						this._isClosed = true;
						flag2 = true;
						if (this._connection != null)
						{
							this._connection.Abort(e4);
						}
						throw;
					}
					catch (StackOverflowException e5)
					{
						this._isClosed = true;
						flag2 = true;
						if (this._connection != null)
						{
							this._connection.Abort(e5);
						}
						throw;
					}
					catch (ThreadAbortException e6)
					{
						this._isClosed = true;
						flag2 = true;
						if (this._connection != null)
						{
							this._connection.Abort(e6);
						}
						throw;
					}
					this.SetMetaData(null, false);
					this._dataReady = false;
					this._isClosed = true;
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
		}

		// Token: 0x060025DE RID: 9694 RVA: 0x0029E798 File Offset: 0x0029DB98
		internal void CloseReaderFromConnection()
		{
			this.Close();
		}

		// Token: 0x060025DF RID: 9695 RVA: 0x0029E7B8 File Offset: 0x0029DBB8
		private void ConsumeMetaData()
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
				this._parser.Run(RunBehavior.ReturnImmediately, this._command, this, null, this._stateObj);
			}
			if (this._metaData != null)
			{
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
		}

		// Token: 0x060025E0 RID: 9696 RVA: 0x0029E8B8 File Offset: 0x0029DCB8
		public override string GetDataTypeName(int i)
		{
			SqlStatistics statistics = null;
			string dataTypeNameInternal;
			try
			{
				statistics = SqlStatistics.StartTimer(this.Statistics);
				if (this.MetaData == null)
				{
					throw SQL.InvalidRead();
				}
				dataTypeNameInternal = this.GetDataTypeNameInternal(this._metaData[i]);
			}
			finally
			{
				SqlStatistics.StopTimer(statistics);
			}
			return dataTypeNameInternal;
		}

		// Token: 0x060025E1 RID: 9697 RVA: 0x0029E928 File Offset: 0x0029DD28
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

		// Token: 0x060025E2 RID: 9698 RVA: 0x0029EA18 File Offset: 0x0029DE18
		public override IEnumerator GetEnumerator()
		{
			return new DbEnumerator(this, this.IsCommandBehavior(CommandBehavior.CloseConnection));
		}

		// Token: 0x060025E3 RID: 9699 RVA: 0x0029EA38 File Offset: 0x0029DE38
		public override Type GetFieldType(int i)
		{
			SqlStatistics statistics = null;
			Type fieldTypeInternal;
			try
			{
				statistics = SqlStatistics.StartTimer(this.Statistics);
				if (this.MetaData == null)
				{
					throw SQL.InvalidRead();
				}
				fieldTypeInternal = this.GetFieldTypeInternal(this._metaData[i]);
			}
			finally
			{
				SqlStatistics.StopTimer(statistics);
			}
			return fieldTypeInternal;
		}

		// Token: 0x060025E4 RID: 9700 RVA: 0x0029EAA8 File Offset: 0x0029DEA8
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
					SqlConnection.CheckGetExtendedUDTInfo(metaData, false);
					result = metaData.udtType;
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

		// Token: 0x060025E5 RID: 9701 RVA: 0x0029EB68 File Offset: 0x0029DF68
		internal virtual int GetLocaleId(int i)
		{
			_SqlMetaData sqlMetaData = this.MetaData[i];
			int result;
			if (sqlMetaData.collation != null)
			{
				result = sqlMetaData.collation.LCID;
			}
			else
			{
				result = 0;
			}
			return result;
		}

		// Token: 0x060025E6 RID: 9702 RVA: 0x0029EBA8 File Offset: 0x0029DFA8
		public override string GetName(int i)
		{
			if (this.MetaData == null)
			{
				throw SQL.InvalidRead();
			}
			return this._metaData[i].column;
		}

		// Token: 0x060025E7 RID: 9703 RVA: 0x0029EBD8 File Offset: 0x0029DFD8
		public override Type GetProviderSpecificFieldType(int i)
		{
			SqlStatistics statistics = null;
			Type providerSpecificFieldTypeInternal;
			try
			{
				statistics = SqlStatistics.StartTimer(this.Statistics);
				if (this.MetaData == null)
				{
					throw SQL.InvalidRead();
				}
				providerSpecificFieldTypeInternal = this.GetProviderSpecificFieldTypeInternal(this._metaData[i]);
			}
			finally
			{
				SqlStatistics.StopTimer(statistics);
			}
			return providerSpecificFieldTypeInternal;
		}

		// Token: 0x060025E8 RID: 9704 RVA: 0x0029EC48 File Offset: 0x0029E048
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
					SqlConnection.CheckGetExtendedUDTInfo(metaData, false);
					result = metaData.udtType;
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

		// Token: 0x060025E9 RID: 9705 RVA: 0x0029ED08 File Offset: 0x0029E108
		public override int GetOrdinal(string name)
		{
			SqlStatistics statistics = null;
			int ordinal;
			try
			{
				statistics = SqlStatistics.StartTimer(this.Statistics);
				if (this._fieldNameLookup == null)
				{
					if (this.MetaData == null)
					{
						throw SQL.InvalidRead();
					}
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

		// Token: 0x060025EA RID: 9706 RVA: 0x0029ED88 File Offset: 0x0029E188
		public override object GetProviderSpecificValue(int i)
		{
			return this.GetSqlValue(i);
		}

		// Token: 0x060025EB RID: 9707 RVA: 0x0029EDA8 File Offset: 0x0029E1A8
		public override int GetProviderSpecificValues(object[] values)
		{
			return this.GetSqlValues(values);
		}

		// Token: 0x060025EC RID: 9708 RVA: 0x0029EDC8 File Offset: 0x0029E1C8
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

		// Token: 0x060025ED RID: 9709 RVA: 0x0029EE68 File Offset: 0x0029E268
		public override bool GetBoolean(int i)
		{
			this.ReadColumn(i);
			return this._data[i].Boolean;
		}

		// Token: 0x060025EE RID: 9710 RVA: 0x0029EE98 File Offset: 0x0029E298
		public override byte GetByte(int i)
		{
			this.ReadColumn(i);
			return this._data[i].Byte;
		}

		// Token: 0x060025EF RID: 9711 RVA: 0x0029EEC8 File Offset: 0x0029E2C8
		public override long GetBytes(int i, long dataIndex, byte[] buffer, int bufferIndex, int length)
		{
			SqlStatistics statistics = null;
			long result = 0L;
			if (this.MetaData == null || !this._dataReady)
			{
				throw SQL.InvalidRead();
			}
			MetaType metaType = this._metaData[i].metaType;
			if ((!metaType.IsLong && !metaType.IsBinType) || SqlDbType.Xml == metaType.SqlDbType)
			{
				throw SQL.NonBlobColumn(this._metaData[i].column);
			}
			try
			{
				statistics = SqlStatistics.StartTimer(this.Statistics);
				this.SetTimeout();
				result = this.GetBytesInternal(i, dataIndex, buffer, bufferIndex, length);
			}
			finally
			{
				SqlStatistics.StopTimer(statistics);
			}
			return result;
		}

		// Token: 0x060025F0 RID: 9712 RVA: 0x0029EF88 File Offset: 0x0029E388
		internal virtual long GetBytesInternal(int i, long dataIndex, byte[] buffer, int bufferIndex, int length)
		{
			RuntimeHelpers.PrepareConstrainedRegions();
			long result;
			try
			{
				int num = 0;
				if (this.IsCommandBehavior(CommandBehavior.SequentialAccess))
				{
					if (0 > i || i >= this._metaData.Length)
					{
						throw new IndexOutOfRangeException();
					}
					if (this._nextColumnDataToRead > i)
					{
						throw ADP.NonSequentialColumnAccess(i, this._nextColumnDataToRead);
					}
					if (this._nextColumnHeaderToRead <= i)
					{
						this.ReadColumnHeader(i);
					}
					if (this._data[i] != null && this._data[i].IsNull)
					{
						throw new SqlNullValueException();
					}
					if (0L == this._columnDataBytesRemaining)
					{
						result = 0L;
					}
					else if (buffer == null)
					{
						if (this._metaData[i].metaType.IsPlp)
						{
							result = (long)this._parser.PlpBytesTotalLength(this._stateObj);
						}
						else
						{
							result = this._columnDataBytesRemaining;
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
						if (num2 > this._columnDataBytesRemaining && !this._metaData[i].metaType.IsPlp)
						{
							result = 0L;
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
							if (this._metaData[i].metaType.IsPlp)
							{
								if (num2 > 0L)
								{
									num2 = (long)this._parser.SkipPlpValue((ulong)num2, this._stateObj);
									this._columnDataBytesRead += num2;
								}
								num2 = (long)this._stateObj.ReadPlpBytes(ref buffer, bufferIndex, length);
								this._columnDataBytesRead += num2;
								this._columnDataBytesRemaining = (long)this._parser.PlpBytesLeft(this._stateObj);
								result = num2;
							}
							else
							{
								if (num2 > 0L)
								{
									this._parser.SkipLongBytes((ulong)num2, this._stateObj);
									this._columnDataBytesRead += num2;
									this._columnDataBytesRemaining -= num2;
								}
								num2 = ((this._columnDataBytesRemaining < (long)length) ? this._columnDataBytesRemaining : ((long)length));
								this._stateObj.ReadByteArray(buffer, bufferIndex, (int)num2);
								this._columnDataBytesRead += num2;
								this._columnDataBytesRemaining -= num2;
								result = num2;
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
					int num3 = (int)dataIndex;
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
						result = (long)num;
					}
					else if (num3 < 0 || num3 >= num)
					{
						result = 0L;
					}
					else
					{
						try
						{
							if (num3 < num)
							{
								if (num3 + length > num)
								{
									num -= num3;
								}
								else
								{
									num = length;
								}
							}
							Array.Copy(array, num3, buffer, bufferIndex, num);
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
						result = (long)num;
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

		// Token: 0x060025F1 RID: 9713 RVA: 0x0029F3E8 File Offset: 0x0029E7E8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override char GetChar(int i)
		{
			throw ADP.NotSupported();
		}

		// Token: 0x060025F2 RID: 9714 RVA: 0x0029F408 File Offset: 0x0029E808
		public override long GetChars(int i, long dataIndex, char[] buffer, int bufferIndex, int length)
		{
			SqlStatistics statistics = null;
			if (this.MetaData == null || !this._dataReady)
			{
				throw SQL.InvalidRead();
			}
			if (0 > i || i >= this._metaData.Length)
			{
				throw new IndexOutOfRangeException();
			}
			long result;
			try
			{
				statistics = SqlStatistics.StartTimer(this.Statistics);
				this.SetTimeout();
				if (this._metaData[i].metaType.IsPlp && this.IsCommandBehavior(CommandBehavior.SequentialAccess))
				{
					if (length < 0)
					{
						throw ADP.InvalidDataLength((long)length);
					}
					if (bufferIndex < 0 || (buffer != null && bufferIndex >= buffer.Length))
					{
						throw ADP.InvalidDestinationBufferIndex(buffer.Length, bufferIndex, "bufferIndex");
					}
					if (buffer != null && length + bufferIndex > buffer.Length)
					{
						throw ADP.InvalidBufferSizeOrIndex(length, bufferIndex);
					}
					if (this._metaData[i].type == SqlDbType.Xml)
					{
						result = this.GetStreamingXmlChars(i, dataIndex, buffer, bufferIndex, length);
					}
					else
					{
						result = this.GetCharsFromPlpData(i, dataIndex, buffer, bufferIndex, length);
					}
				}
				else
				{
					if (this._nextColumnDataToRead == i + 1 && this._nextColumnHeaderToRead == i + 1 && this._columnDataChars != null)
					{
						if (this.IsCommandBehavior(CommandBehavior.SequentialAccess) && dataIndex < this._columnDataCharsRead)
						{
							throw ADP.NonSeqByteAccess(dataIndex, this._columnDataCharsRead, "GetChars");
						}
					}
					else
					{
						string value = this.GetSqlString(i).Value;
						this._columnDataChars = value.ToCharArray();
						this._columnDataCharsRead = 0L;
					}
					int num = this._columnDataChars.Length;
					if (dataIndex > 2147483647L)
					{
						throw ADP.InvalidSourceBufferIndex(num, dataIndex, "dataIndex");
					}
					int num2 = (int)dataIndex;
					if (buffer == null)
					{
						result = (long)num;
					}
					else if (num2 < 0 || num2 >= num)
					{
						result = 0L;
					}
					else
					{
						try
						{
							if (num2 < num)
							{
								if (num2 + length > num)
								{
									num -= num2;
								}
								else
								{
									num = length;
								}
							}
							Array.Copy(this._columnDataChars, num2, buffer, bufferIndex, num);
							this._columnDataCharsRead += (long)num;
						}
						catch (Exception e)
						{
							if (!ADP.IsCatchableExceptionType(e))
							{
								throw;
							}
							num = this._columnDataChars.Length;
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
						result = (long)num;
					}
				}
			}
			finally
			{
				SqlStatistics.StopTimer(statistics);
			}
			return result;
		}

		// Token: 0x060025F3 RID: 9715 RVA: 0x0029F678 File Offset: 0x0029EA78
		private long GetCharsFromPlpData(int i, long dataIndex, char[] buffer, int bufferIndex, int length)
		{
			RuntimeHelpers.PrepareConstrainedRegions();
			long result;
			try
			{
				if (this.MetaData == null || !this._dataReady)
				{
					throw SQL.InvalidRead();
				}
				if (this._nextColumnDataToRead > i)
				{
					throw ADP.NonSequentialColumnAccess(i, this._nextColumnDataToRead);
				}
				if (!this._metaData[i].metaType.IsCharType)
				{
					throw SQL.NonCharColumn(this._metaData[i].column);
				}
				if (this._nextColumnHeaderToRead <= i)
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
				bool isNCharType = this._metaData[i].metaType.IsNCharType;
				if (0L == this._columnDataBytesRemaining)
				{
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
					this._columnDataBytesRemaining = (long)this._parser.PlpBytesLeft(this._stateObj);
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

		// Token: 0x060025F4 RID: 9716 RVA: 0x0029F928 File Offset: 0x0029ED28
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

		// Token: 0x060025F5 RID: 9717 RVA: 0x0029F998 File Offset: 0x0029ED98
		[EditorBrowsable(EditorBrowsableState.Never)]
		IDataReader IDataRecord.GetData(int i)
		{
			throw ADP.NotSupported();
		}

		// Token: 0x060025F6 RID: 9718 RVA: 0x0029F9B8 File Offset: 0x0029EDB8
		public override DateTime GetDateTime(int i)
		{
			this.ReadColumn(i);
			DateTime result = this._data[i].DateTime;
			if (this._typeSystem <= SqlConnectionString.TypeSystem.SQLServer2005 && this._metaData[i].IsNewKatmaiDateTimeType)
			{
				object @string = this._data[i].String;
				result = (DateTime)@string;
			}
			return result;
		}

		// Token: 0x060025F7 RID: 9719 RVA: 0x0029FA18 File Offset: 0x0029EE18
		public override decimal GetDecimal(int i)
		{
			this.ReadColumn(i);
			return this._data[i].Decimal;
		}

		// Token: 0x060025F8 RID: 9720 RVA: 0x0029FA48 File Offset: 0x0029EE48
		public override double GetDouble(int i)
		{
			this.ReadColumn(i);
			return this._data[i].Double;
		}

		// Token: 0x060025F9 RID: 9721 RVA: 0x0029FA78 File Offset: 0x0029EE78
		public override float GetFloat(int i)
		{
			this.ReadColumn(i);
			return this._data[i].Single;
		}

		// Token: 0x060025FA RID: 9722 RVA: 0x0029FAA8 File Offset: 0x0029EEA8
		public override Guid GetGuid(int i)
		{
			this.ReadColumn(i);
			return this._data[i].SqlGuid.Value;
		}

		// Token: 0x060025FB RID: 9723 RVA: 0x0029FAD8 File Offset: 0x0029EED8
		public override short GetInt16(int i)
		{
			this.ReadColumn(i);
			return this._data[i].Int16;
		}

		// Token: 0x060025FC RID: 9724 RVA: 0x0029FB08 File Offset: 0x0029EF08
		public override int GetInt32(int i)
		{
			this.ReadColumn(i);
			return this._data[i].Int32;
		}

		// Token: 0x060025FD RID: 9725 RVA: 0x0029FB38 File Offset: 0x0029EF38
		public override long GetInt64(int i)
		{
			this.ReadColumn(i);
			return this._data[i].Int64;
		}

		// Token: 0x060025FE RID: 9726 RVA: 0x0029FB68 File Offset: 0x0029EF68
		public virtual SqlBoolean GetSqlBoolean(int i)
		{
			this.ReadColumn(i);
			return this._data[i].SqlBoolean;
		}

		// Token: 0x060025FF RID: 9727 RVA: 0x0029FB98 File Offset: 0x0029EF98
		public virtual SqlBinary GetSqlBinary(int i)
		{
			this.ReadColumn(i);
			return this._data[i].SqlBinary;
		}

		// Token: 0x06002600 RID: 9728 RVA: 0x0029FBC8 File Offset: 0x0029EFC8
		public virtual SqlByte GetSqlByte(int i)
		{
			this.ReadColumn(i);
			return this._data[i].SqlByte;
		}

		// Token: 0x06002601 RID: 9729 RVA: 0x0029FBF8 File Offset: 0x0029EFF8
		public virtual SqlBytes GetSqlBytes(int i)
		{
			if (this.MetaData == null)
			{
				throw SQL.InvalidRead();
			}
			this.ReadColumn(i);
			SqlBinary sqlBinary = this._data[i].SqlBinary;
			return new SqlBytes(sqlBinary);
		}

		// Token: 0x06002602 RID: 9730 RVA: 0x0029FC38 File Offset: 0x0029F038
		public virtual SqlChars GetSqlChars(int i)
		{
			this.ReadColumn(i);
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

		// Token: 0x06002603 RID: 9731 RVA: 0x0029FC98 File Offset: 0x0029F098
		public virtual SqlDateTime GetSqlDateTime(int i)
		{
			this.ReadColumn(i);
			return this._data[i].SqlDateTime;
		}

		// Token: 0x06002604 RID: 9732 RVA: 0x0029FCC8 File Offset: 0x0029F0C8
		public virtual SqlDecimal GetSqlDecimal(int i)
		{
			this.ReadColumn(i);
			return this._data[i].SqlDecimal;
		}

		// Token: 0x06002605 RID: 9733 RVA: 0x0029FCF8 File Offset: 0x0029F0F8
		public virtual SqlGuid GetSqlGuid(int i)
		{
			this.ReadColumn(i);
			return this._data[i].SqlGuid;
		}

		// Token: 0x06002606 RID: 9734 RVA: 0x0029FD28 File Offset: 0x0029F128
		public virtual SqlDouble GetSqlDouble(int i)
		{
			this.ReadColumn(i);
			return this._data[i].SqlDouble;
		}

		// Token: 0x06002607 RID: 9735 RVA: 0x0029FD58 File Offset: 0x0029F158
		public virtual SqlInt16 GetSqlInt16(int i)
		{
			this.ReadColumn(i);
			return this._data[i].SqlInt16;
		}

		// Token: 0x06002608 RID: 9736 RVA: 0x0029FD88 File Offset: 0x0029F188
		public virtual SqlInt32 GetSqlInt32(int i)
		{
			this.ReadColumn(i);
			return this._data[i].SqlInt32;
		}

		// Token: 0x06002609 RID: 9737 RVA: 0x0029FDB8 File Offset: 0x0029F1B8
		public virtual SqlInt64 GetSqlInt64(int i)
		{
			this.ReadColumn(i);
			return this._data[i].SqlInt64;
		}

		// Token: 0x0600260A RID: 9738 RVA: 0x0029FDE8 File Offset: 0x0029F1E8
		public virtual SqlMoney GetSqlMoney(int i)
		{
			this.ReadColumn(i);
			return this._data[i].SqlMoney;
		}

		// Token: 0x0600260B RID: 9739 RVA: 0x0029FE18 File Offset: 0x0029F218
		public virtual SqlSingle GetSqlSingle(int i)
		{
			this.ReadColumn(i);
			return this._data[i].SqlSingle;
		}

		// Token: 0x0600260C RID: 9740 RVA: 0x0029FE48 File Offset: 0x0029F248
		public virtual SqlString GetSqlString(int i)
		{
			this.ReadColumn(i);
			if (this._typeSystem <= SqlConnectionString.TypeSystem.SQLServer2005 && this._metaData[i].IsNewKatmaiDateTimeType)
			{
				return this._data[i].KatmaiDateTimeSqlString;
			}
			return this._data[i].SqlString;
		}

		// Token: 0x0600260D RID: 9741 RVA: 0x0029FE98 File Offset: 0x0029F298
		public virtual SqlXml GetSqlXml(int i)
		{
			this.ReadColumn(i);
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

		// Token: 0x0600260E RID: 9742 RVA: 0x0029FF28 File Offset: 0x0029F328
		public virtual object GetSqlValue(int i)
		{
			SqlStatistics statistics = null;
			object result;
			try
			{
				statistics = SqlStatistics.StartTimer(this.Statistics);
				if (this.MetaData == null || !this._dataReady)
				{
					throw SQL.InvalidRead();
				}
				this.SetTimeout();
				object sqlValueInternal = this.GetSqlValueInternal(i);
				result = sqlValueInternal;
			}
			finally
			{
				SqlStatistics.StopTimer(statistics);
			}
			return result;
		}

		// Token: 0x0600260F RID: 9743 RVA: 0x0029FF98 File Offset: 0x0029F398
		private object GetSqlValueInternal(int i)
		{
			this.ReadColumn(i, false);
			if (this._typeSystem <= SqlConnectionString.TypeSystem.SQLServer2005 && this._metaData[i].IsNewKatmaiDateTimeType)
			{
				return this._data[i].KatmaiDateTimeSqlString;
			}
			object result;
			if (this._typeSystem <= SqlConnectionString.TypeSystem.SQLServer2005 && this._metaData[i].IsLargeUdt)
			{
				result = this._data[i].SqlValue;
			}
			else if (this._typeSystem != SqlConnectionString.TypeSystem.SQLServer2000)
			{
				if (this._metaData[i].type == SqlDbType.Udt)
				{
					SqlConnection.CheckGetExtendedUDTInfo(this._metaData[i], true);
					result = this.Connection.GetUdtValue(this._data[i].Value, this._metaData[i], false);
				}
				else
				{
					result = this._data[i].SqlValue;
				}
			}
			else if (this._metaData[i].type == SqlDbType.Xml)
			{
				result = this._data[i].SqlString;
			}
			else
			{
				result = this._data[i].SqlValue;
			}
			return result;
		}

		// Token: 0x06002610 RID: 9744 RVA: 0x002A00B8 File Offset: 0x0029F4B8
		public virtual int GetSqlValues(object[] values)
		{
			SqlStatistics statistics = null;
			int result;
			try
			{
				statistics = SqlStatistics.StartTimer(this.Statistics);
				if (this.MetaData == null || !this._dataReady)
				{
					throw SQL.InvalidRead();
				}
				if (values == null)
				{
					throw ADP.ArgumentNull("values");
				}
				this.SetTimeout();
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

		// Token: 0x06002611 RID: 9745 RVA: 0x002A0168 File Offset: 0x0029F568
		public override string GetString(int i)
		{
			this.ReadColumn(i);
			if (this._typeSystem <= SqlConnectionString.TypeSystem.SQLServer2005 && this._metaData[i].IsNewKatmaiDateTimeType)
			{
				return this._data[i].KatmaiDateTimeString;
			}
			return this._data[i].String;
		}

		// Token: 0x06002612 RID: 9746 RVA: 0x002A01B8 File Offset: 0x0029F5B8
		public override object GetValue(int i)
		{
			SqlStatistics statistics = null;
			object result;
			try
			{
				statistics = SqlStatistics.StartTimer(this.Statistics);
				if (this.MetaData == null || !this._dataReady)
				{
					throw SQL.InvalidRead();
				}
				this.SetTimeout();
				object valueInternal = this.GetValueInternal(i);
				result = valueInternal;
			}
			finally
			{
				SqlStatistics.StopTimer(statistics);
			}
			return result;
		}

		// Token: 0x06002613 RID: 9747 RVA: 0x002A0228 File Offset: 0x0029F628
		public virtual TimeSpan GetTimeSpan(int i)
		{
			this.ReadColumn(i);
			TimeSpan result = this._data[i].Time;
			if (this._typeSystem <= SqlConnectionString.TypeSystem.SQLServer2005)
			{
				object @string = this._data[i].String;
				result = (TimeSpan)@string;
			}
			return result;
		}

		// Token: 0x06002614 RID: 9748 RVA: 0x002A0278 File Offset: 0x0029F678
		public virtual DateTimeOffset GetDateTimeOffset(int i)
		{
			this.ReadColumn(i);
			DateTimeOffset result = this._data[i].DateTimeOffset;
			if (this._typeSystem <= SqlConnectionString.TypeSystem.SQLServer2005)
			{
				object @string = this._data[i].String;
				result = (DateTimeOffset)@string;
			}
			return result;
		}

		// Token: 0x06002615 RID: 9749 RVA: 0x002A02C8 File Offset: 0x0029F6C8
		private object GetValueInternal(int i)
		{
			this.ReadColumn(i, false);
			if (this._typeSystem > SqlConnectionString.TypeSystem.SQLServer2005 || !this._metaData[i].IsNewKatmaiDateTimeType)
			{
				object result;
				if (this._typeSystem <= SqlConnectionString.TypeSystem.SQLServer2005 && this._metaData[i].IsLargeUdt)
				{
					result = this._data[i].Value;
				}
				else if (this._typeSystem != SqlConnectionString.TypeSystem.SQLServer2000)
				{
					if (this._metaData[i].type != SqlDbType.Udt)
					{
						result = this._data[i].Value;
					}
					else
					{
						SqlConnection.CheckGetExtendedUDTInfo(this._metaData[i], true);
						result = this.Connection.GetUdtValue(this._data[i].Value, this._metaData[i], true);
					}
				}
				else
				{
					result = this._data[i].Value;
				}
				return result;
			}
			if (this._data[i].IsNull)
			{
				return DBNull.Value;
			}
			return this._data[i].KatmaiDateTimeString;
		}

		// Token: 0x06002616 RID: 9750 RVA: 0x002A03D8 File Offset: 0x0029F7D8
		public override int GetValues(object[] values)
		{
			SqlStatistics statistics = null;
			int result;
			try
			{
				statistics = SqlStatistics.StartTimer(this.Statistics);
				if (this.MetaData == null || !this._dataReady)
				{
					throw SQL.InvalidRead();
				}
				if (values == null)
				{
					throw ADP.ArgumentNull("values");
				}
				int num = (values.Length < this._metaData.visibleColumns) ? values.Length : this._metaData.visibleColumns;
				this.SetTimeout();
				for (int i = 0; i < num; i++)
				{
					values[this._metaData.indexMap[i]] = this.GetValueInternal(i);
				}
				if (this._rowException != null)
				{
					throw this._rowException;
				}
				result = num;
			}
			finally
			{
				SqlStatistics.StopTimer(statistics);
			}
			return result;
		}

		// Token: 0x06002617 RID: 9751 RVA: 0x002A0498 File Offset: 0x0029F898
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

		// Token: 0x06002618 RID: 9752 RVA: 0x002A0508 File Offset: 0x0029F908
		private bool HasMoreResults()
		{
			if (this._parser != null)
			{
				if (this.HasMoreRows())
				{
					return true;
				}
				while (this._stateObj._pendingData)
				{
					byte b = this._stateObj.PeekByte();
					byte b2 = b;
					if (b2 == 129)
					{
						return true;
					}
					switch (b2)
					{
					case 209:
						return true;
					case 210:
						break;
					case 211:
						if (this._altRowStatus == SqlDataReader.ALTROWSTATUS.Null)
						{
							this._altMetaDataSetCollection.metaDataSet = this._metaData;
							this._metaData = null;
						}
						this._altRowStatus = SqlDataReader.ALTROWSTATUS.AltRow;
						this._hasRows = true;
						return true;
					default:
						if (b2 == 253)
						{
							this._altRowStatus = SqlDataReader.ALTROWSTATUS.Null;
							this._metaData = null;
							this._altMetaDataSetCollection = null;
							return true;
						}
						break;
					}
					this._parser.Run(RunBehavior.ReturnImmediately, this._command, this, null, this._stateObj);
				}
			}
			return false;
		}

		// Token: 0x06002619 RID: 9753 RVA: 0x002A05E8 File Offset: 0x0029F9E8
		private bool HasMoreRows()
		{
			if (this._parser != null)
			{
				if (this._dataReady)
				{
					return true;
				}
				switch (this._altRowStatus)
				{
				case SqlDataReader.ALTROWSTATUS.AltRow:
					return true;
				case SqlDataReader.ALTROWSTATUS.Done:
					return false;
				default:
					if (this._stateObj._pendingData)
					{
						byte b = this._stateObj.PeekByte();
						bool flag = false;
						while (b == 253 || b == 254 || b == 255 || (!flag && b == 169) || (!flag && b == 170) || (!flag && b == 171))
						{
							if (b == 253 || b == 254 || b == 255)
							{
								flag = true;
							}
							this._parser.Run(RunBehavior.ReturnImmediately, this._command, this, null, this._stateObj);
							if (!this._stateObj._pendingData)
							{
								break;
							}
							b = this._stateObj.PeekByte();
						}
						if (209 == b)
						{
							return true;
						}
					}
					break;
				}
			}
			return false;
		}

		// Token: 0x0600261A RID: 9754 RVA: 0x002A06E8 File Offset: 0x0029FAE8
		public override bool IsDBNull(int i)
		{
			this.SetTimeout();
			this.ReadColumnHeader(i);
			return this._data[i].IsNull;
		}

		// Token: 0x0600261B RID: 9755 RVA: 0x002A0718 File Offset: 0x0029FB18
		protected bool IsCommandBehavior(CommandBehavior condition)
		{
			return condition == (condition & this._commandBehavior);
		}

		// Token: 0x0600261C RID: 9756 RVA: 0x002A0738 File Offset: 0x0029FB38
		public override bool NextResult()
		{
			SqlStatistics statistics = null;
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<sc.SqlDataReader.NextResult|API> %d#", this.ObjectID);
			RuntimeHelpers.PrepareConstrainedRegions();
			bool result;
			try
			{
				statistics = SqlStatistics.StartTimer(this.Statistics);
				this.SetTimeout();
				if (this.IsClosed)
				{
					throw ADP.DataReaderClosed("NextResult");
				}
				this._fieldNameLookup = null;
				bool flag = false;
				this._hasRows = false;
				if (this.IsCommandBehavior(CommandBehavior.SingleResult))
				{
					this.CloseInternal(false);
					this.ClearMetaData();
					result = flag;
				}
				else
				{
					if (this._parser != null)
					{
						while (this.ReadInternal(false))
						{
						}
					}
					if (this._parser != null)
					{
						if (this.HasMoreResults())
						{
							this._metaDataConsumed = false;
							this._browseModeInfoConsumed = false;
							switch (this._altRowStatus)
							{
							case SqlDataReader.ALTROWSTATUS.AltRow:
							{
								int altRowId = this._parser.GetAltRowId(this._stateObj);
								_SqlMetaDataSet sqlMetaDataSet = this._altMetaDataSetCollection[altRowId];
								if (sqlMetaDataSet != null)
								{
									this._metaData = sqlMetaDataSet;
									this._metaData.indexMap = sqlMetaDataSet.indexMap;
								}
								break;
							}
							case SqlDataReader.ALTROWSTATUS.Done:
								this._metaData = this._altMetaDataSetCollection.metaDataSet;
								this._altRowStatus = SqlDataReader.ALTROWSTATUS.Null;
								break;
							default:
								this.ConsumeMetaData();
								if (this._metaData == null)
								{
									return false;
								}
								break;
							}
							flag = true;
						}
						else
						{
							this.CloseInternal(false);
							this.SetMetaData(null, false);
						}
					}
					else
					{
						this.ClearMetaData();
					}
					result = flag;
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

		// Token: 0x0600261D RID: 9757 RVA: 0x002A0978 File Offset: 0x0029FD78
		public override bool Read()
		{
			return this.ReadInternal(true);
		}

		// Token: 0x0600261E RID: 9758 RVA: 0x002A0998 File Offset: 0x0029FD98
		private bool ReadInternal(bool setTimeout)
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
						this.SetTimeout();
					}
					if (this._dataReady)
					{
						this.CleanPartialRead();
					}
					this._dataReady = false;
					SqlBuffer.Clear(this._data);
					this._nextColumnHeaderToRead = 0;
					this._nextColumnDataToRead = 0;
					this._columnDataBytesRemaining = -1L;
					if (!this._haltRead)
					{
						if (this.HasMoreRows())
						{
							while (this._stateObj._pendingData)
							{
								if (this._altRowStatus == SqlDataReader.ALTROWSTATUS.AltRow)
								{
									this._altRowStatus = SqlDataReader.ALTROWSTATUS.Done;
									this._dataReady = true;
									break;
								}
								this._dataReady = this._parser.Run(RunBehavior.ReturnImmediately, this._command, this, null, this._stateObj);
								if (this._dataReady)
								{
									break;
								}
							}
							if (this._dataReady)
							{
								this._haltRead = this.IsCommandBehavior(CommandBehavior.SingleRow);
								return true;
							}
						}
						if (!this._stateObj._pendingData)
						{
							this.CloseInternal(false);
						}
					}
					else
					{
						while (this.HasMoreRows())
						{
							while (this._stateObj._pendingData && !this._dataReady)
							{
								this._dataReady = this._parser.Run(RunBehavior.ReturnImmediately, this._command, this, null, this._stateObj);
							}
							if (this._dataReady)
							{
								this.CleanPartialRead();
							}
							this._dataReady = false;
							SqlBuffer.Clear(this._data);
							this._nextColumnHeaderToRead = 0;
						}
						this._haltRead = false;
					}
				}
				else if (this.IsClosed)
				{
					throw ADP.DataReaderClosed("Read");
				}
				result = false;
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

		// Token: 0x0600261F RID: 9759 RVA: 0x002A0C18 File Offset: 0x002A0018
		private void ReadColumn(int i)
		{
			this.ReadColumn(i, true);
		}

		// Token: 0x06002620 RID: 9760 RVA: 0x002A0C38 File Offset: 0x002A0038
		private void ReadColumn(int i, bool setTimeout)
		{
			if (this.MetaData == null || !this._dataReady)
			{
				throw SQL.InvalidRead();
			}
			if (0 > i || i >= this._metaData.Length)
			{
				throw new IndexOutOfRangeException();
			}
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				if (setTimeout)
				{
					this.SetTimeout();
				}
				if (this._nextColumnHeaderToRead <= i)
				{
					this.ReadColumnHeader(i);
				}
				if (this._nextColumnDataToRead == i)
				{
					this.ReadColumnData();
				}
				else if (this._nextColumnDataToRead > i && this.IsCommandBehavior(CommandBehavior.SequentialAccess))
				{
					throw ADP.NonSequentialColumnAccess(i, this._nextColumnDataToRead);
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

		// Token: 0x06002621 RID: 9761 RVA: 0x002A0D78 File Offset: 0x002A0178
		private void ReadColumnData()
		{
			if (!this._data[this._nextColumnDataToRead].IsNull)
			{
				_SqlMetaData md = this._metaData[this._nextColumnDataToRead];
				this._parser.ReadSqlValue(this._data[this._nextColumnDataToRead], md, (int)this._columnDataBytesRemaining, this._stateObj);
				this._columnDataBytesRemaining = 0L;
			}
			this._nextColumnDataToRead++;
		}

		// Token: 0x06002622 RID: 9762 RVA: 0x002A0DE8 File Offset: 0x002A01E8
		private void ReadColumnHeader(int i)
		{
			if (!this._dataReady)
			{
				throw SQL.InvalidRead();
			}
			if (i < this._nextColumnDataToRead)
			{
				return;
			}
			bool flag = this.IsCommandBehavior(CommandBehavior.SequentialAccess);
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				if (flag)
				{
					if (0 < this._nextColumnDataToRead)
					{
						this._data[this._nextColumnDataToRead - 1].Clear();
					}
				}
				else if (this._nextColumnDataToRead < this._nextColumnHeaderToRead)
				{
					this.ReadColumnData();
				}
				while (this._nextColumnHeaderToRead <= i)
				{
					this.ResetBlobState();
					if (flag)
					{
						flag = (this._nextColumnHeaderToRead < i);
					}
					_SqlMetaData sqlMetaData = this._metaData[this._nextColumnHeaderToRead];
					if (flag && sqlMetaData.metaType.IsPlp)
					{
						this._parser.SkipPlpValue(ulong.MaxValue, this._stateObj);
						this._nextColumnDataToRead = this._nextColumnHeaderToRead;
						this._nextColumnHeaderToRead++;
						this._columnDataBytesRemaining = 0L;
					}
					else
					{
						bool flag2 = false;
						ulong num = this._parser.ProcessColumnHeader(sqlMetaData, this._stateObj, out flag2);
						this._nextColumnDataToRead = this._nextColumnHeaderToRead;
						this._nextColumnHeaderToRead++;
						if (flag)
						{
							this._parser.SkipLongBytes(num, this._stateObj);
							this._columnDataBytesRemaining = 0L;
						}
						else if (flag2)
						{
							this._parser.GetNullSqlValue(this._data[this._nextColumnDataToRead], sqlMetaData);
							this._columnDataBytesRemaining = 0L;
						}
						else
						{
							this._columnDataBytesRemaining = (long)num;
							if (i > this._nextColumnDataToRead)
							{
								this.ReadColumnData();
							}
						}
					}
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

		// Token: 0x06002623 RID: 9763 RVA: 0x002A1028 File Offset: 0x002A0428
		private void ResetBlobState()
		{
			int num = this._nextColumnHeaderToRead - 1;
			if (num >= 0 && this._metaData[num].metaType.IsPlp)
			{
				if (this._stateObj._longlen != 0UL)
				{
					this._stateObj.Parser.SkipPlpValue(ulong.MaxValue, this._stateObj);
				}
				if (this._streamingXml != null)
				{
					SqlStreamingXml streamingXml = this._streamingXml;
					this._streamingXml = null;
					streamingXml.Close();
				}
			}
			else if (0L < this._columnDataBytesRemaining)
			{
				this._stateObj.Parser.SkipLongBytes((ulong)this._columnDataBytesRemaining, this._stateObj);
			}
			this._columnDataBytesRemaining = -1L;
			this._columnDataBytesRead = 0L;
			this._columnDataCharsRead = 0L;
			this._columnDataChars = null;
		}

		// Token: 0x06002624 RID: 9764 RVA: 0x002A10E8 File Offset: 0x002A04E8
		private void RestoreServerSettings(TdsParser parser, TdsParserStateObject stateObj)
		{
			if (parser != null && this._resetOptionsString != null)
			{
				if (parser.State == TdsParserState.OpenLoggedIn)
				{
					parser.TdsExecuteSQLBatch(this._resetOptionsString, (this._command != null) ? this._command.CommandTimeout : 0, null, stateObj);
					parser.Run(RunBehavior.UntilDone, this._command, this, null, stateObj);
				}
				this._resetOptionsString = null;
			}
		}

		// Token: 0x06002625 RID: 9765 RVA: 0x002A1148 File Offset: 0x002A0548
		internal void SetAltMetaDataSet(_SqlMetaDataSet metaDataSet, bool metaDataConsumed)
		{
			if (this._altMetaDataSetCollection == null)
			{
				this._altMetaDataSetCollection = new _SqlMetaDataSetCollection();
			}
			this._altMetaDataSetCollection.Add(metaDataSet);
			this._metaDataConsumed = metaDataConsumed;
			if (this._metaDataConsumed)
			{
				byte b = this._stateObj.PeekByte();
				if (169 == b)
				{
					this._parser.Run(RunBehavior.ReturnImmediately, this._command, this, null, this._stateObj);
					b = this._stateObj.PeekByte();
				}
				this._hasRows = (209 == b);
			}
			if (metaDataSet != null && (this._data == null || this._data.Length < metaDataSet.Length))
			{
				this._data = SqlBuffer.CreateBufferArray(metaDataSet.Length);
			}
		}

		// Token: 0x06002626 RID: 9766 RVA: 0x002A11F8 File Offset: 0x002A05F8
		private void ClearMetaData()
		{
			this._metaData = null;
			this._tableNames = null;
			this._fieldNameLookup = null;
			this._metaDataConsumed = false;
			this._browseModeInfoConsumed = false;
		}

		// Token: 0x06002627 RID: 9767 RVA: 0x002A1228 File Offset: 0x002A0628
		internal void SetMetaData(_SqlMetaDataSet metaData, bool moreInfo)
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
						byte b = this._stateObj.PeekByte();
						if (b == 169)
						{
							this._parser.Run(RunBehavior.ReturnImmediately, null, null, null, this._stateObj);
							b = this._stateObj.PeekByte();
						}
						this._hasRows = (209 == b);
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
		}

		// Token: 0x06002628 RID: 9768 RVA: 0x002A12E8 File Offset: 0x002A06E8
		private void SetTimeout()
		{
			TdsParserStateObject stateObj = this._stateObj;
			if (stateObj != null)
			{
				stateObj.SetTimeoutSeconds(this._timeoutSeconds);
			}
		}

		// Token: 0x06002629 RID: 9769 RVA: 0x002A1318 File Offset: 0x002A0718
		internal object GetSqlValueWithNoConvert(int i)
		{
			if (this.MetaData == null || !this._dataReady)
			{
				throw SQL.InvalidRead();
			}
			this.ReadColumn(i, false);
			object result;
			if (this._metaData[i].type == SqlDbType.Xml)
			{
				result = this._data[i].SqlCachedBuffer;
			}
			else
			{
				result = this._data[i].SqlValue;
			}
			return result;
		}

		// Token: 0x04001814 RID: 6164
		private TdsParser _parser;

		// Token: 0x04001815 RID: 6165
		private TdsParserStateObject _stateObj;

		// Token: 0x04001816 RID: 6166
		private SqlCommand _command;

		// Token: 0x04001817 RID: 6167
		private SqlConnection _connection;

		// Token: 0x04001818 RID: 6168
		private int _defaultLCID;

		// Token: 0x04001819 RID: 6169
		private bool _dataReady;

		// Token: 0x0400181A RID: 6170
		private bool _haltRead;

		// Token: 0x0400181B RID: 6171
		private bool _metaDataConsumed;

		// Token: 0x0400181C RID: 6172
		private bool _browseModeInfoConsumed;

		// Token: 0x0400181D RID: 6173
		private bool _isClosed;

		// Token: 0x0400181E RID: 6174
		private bool _isInitialized;

		// Token: 0x0400181F RID: 6175
		private bool _hasRows;

		// Token: 0x04001820 RID: 6176
		private SqlDataReader.ALTROWSTATUS _altRowStatus;

		// Token: 0x04001821 RID: 6177
		private int _recordsAffected = -1;

		// Token: 0x04001822 RID: 6178
		private int _timeoutSeconds;

		// Token: 0x04001823 RID: 6179
		private SqlConnectionString.TypeSystem _typeSystem;

		// Token: 0x04001824 RID: 6180
		private SqlStatistics _statistics;

		// Token: 0x04001825 RID: 6181
		private SqlBuffer[] _data;

		// Token: 0x04001826 RID: 6182
		private SqlStreamingXml _streamingXml;

		// Token: 0x04001827 RID: 6183
		private _SqlMetaDataSet _metaData;

		// Token: 0x04001828 RID: 6184
		private _SqlMetaDataSetCollection _altMetaDataSetCollection;

		// Token: 0x04001829 RID: 6185
		private FieldNameLookup _fieldNameLookup;

		// Token: 0x0400182A RID: 6186
		private CommandBehavior _commandBehavior;

		// Token: 0x0400182B RID: 6187
		private static int _objectTypeCount;

		// Token: 0x0400182C RID: 6188
		internal readonly int ObjectID = Interlocked.Increment(ref SqlDataReader._objectTypeCount);

		// Token: 0x0400182D RID: 6189
		private MultiPartTableName[] _tableNames;

		// Token: 0x0400182E RID: 6190
		private string _resetOptionsString;

		// Token: 0x0400182F RID: 6191
		private int _nextColumnDataToRead;

		// Token: 0x04001830 RID: 6192
		private int _nextColumnHeaderToRead;

		// Token: 0x04001831 RID: 6193
		private long _columnDataBytesRead;

		// Token: 0x04001832 RID: 6194
		private long _columnDataBytesRemaining;

		// Token: 0x04001833 RID: 6195
		private long _columnDataCharsRead;

		// Token: 0x04001834 RID: 6196
		private char[] _columnDataChars;

		// Token: 0x04001835 RID: 6197
		private Exception _rowException;

		// Token: 0x020002DF RID: 735
		private enum ALTROWSTATUS
		{
			// Token: 0x04001837 RID: 6199
			Null,
			// Token: 0x04001838 RID: 6200
			AltRow,
			// Token: 0x04001839 RID: 6201
			Done
		}
	}
}
