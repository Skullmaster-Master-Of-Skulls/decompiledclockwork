using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Data.ProviderBase;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;

namespace System.Data.OleDb
{
	// Token: 0x0200024B RID: 587
	public sealed class OleDbDataReader : DbDataReader
	{
		// Token: 0x0600252D RID: 9517 RVA: 0x000FD1FC File Offset: 0x000FC5FC
		internal OleDbDataReader(OleDbConnection connection, OleDbCommand command, int depth, CommandBehavior commandBehavior)
		{
			this._connection = connection;
			this._command = command;
			this._commandBehavior = commandBehavior;
			if (command != null && this._depth == 0)
			{
				this._parameterBindings = command.TakeBindingOwnerShip();
			}
			this._depth = depth;
		}

		// Token: 0x0600252E RID: 9518 RVA: 0x000FD26C File Offset: 0x000FC66C
		private void Initialize()
		{
			CommandBehavior commandBehavior = this._commandBehavior;
			this._useIColumnsRowset = ((CommandBehavior.KeyInfo & commandBehavior) > CommandBehavior.Default);
			this._sequentialAccess = ((CommandBehavior.SequentialAccess & commandBehavior) > CommandBehavior.Default);
			if (this._depth == 0)
			{
				this._singleRow = ((CommandBehavior.SingleRow & commandBehavior) > CommandBehavior.Default);
			}
		}

		// Token: 0x0600252F RID: 9519 RVA: 0x000FD2B0 File Offset: 0x000FC6B0
		internal void InitializeIMultipleResults(object result)
		{
			this.Initialize();
			this._imultipleResults = (UnsafeNativeMethods.IMultipleResults)result;
		}

		// Token: 0x06002530 RID: 9520 RVA: 0x000FD2D0 File Offset: 0x000FC6D0
		internal void InitializeIRowset(object result, ChapterHandle chapterHandle, IntPtr recordsAffected)
		{
			if (this._connection == null || ChapterHandle.DB_NULL_HCHAPTER != chapterHandle)
			{
				this._rowHandleFetchCount = new IntPtr(1);
			}
			this.Initialize();
			this._recordsAffected = recordsAffected;
			this._irowset = (UnsafeNativeMethods.IRowset)result;
			this._chapterHandle = chapterHandle;
		}

		// Token: 0x06002531 RID: 9521 RVA: 0x000FD31C File Offset: 0x000FC71C
		internal void InitializeIRow(object result, IntPtr recordsAffected)
		{
			this.Initialize();
			this._singleRow = true;
			this._recordsAffected = recordsAffected;
			this._irow = (UnsafeNativeMethods.IRow)result;
			this._hasRows = (this._irow != null);
		}

		// Token: 0x17000608 RID: 1544
		// (get) Token: 0x06002532 RID: 9522 RVA: 0x000FD358 File Offset: 0x000FC758
		internal OleDbCommand Command
		{
			get
			{
				return this._command;
			}
		}

		// Token: 0x17000609 RID: 1545
		// (get) Token: 0x06002533 RID: 9523 RVA: 0x000FD36C File Offset: 0x000FC76C
		public override int Depth
		{
			get
			{
				Bid.Trace("<oledb.OleDbDataReader.get_Depth|API> %d#\n", this.ObjectID);
				if (this.IsClosed)
				{
					throw ADP.DataReaderClosed("Depth");
				}
				return this._depth;
			}
		}

		// Token: 0x1700060A RID: 1546
		// (get) Token: 0x06002534 RID: 9524 RVA: 0x000FD3A4 File Offset: 0x000FC7A4
		public override int FieldCount
		{
			get
			{
				Bid.Trace("<oledb.OleDbDataReader.get_FieldCount|API> %d#\n", this.ObjectID);
				if (this.IsClosed)
				{
					throw ADP.DataReaderClosed("FieldCount");
				}
				MetaData[] metaData = this.MetaData;
				if (metaData == null)
				{
					return 0;
				}
				return metaData.Length;
			}
		}

		// Token: 0x1700060B RID: 1547
		// (get) Token: 0x06002535 RID: 9525 RVA: 0x000FD3E4 File Offset: 0x000FC7E4
		public override bool HasRows
		{
			get
			{
				Bid.Trace("<oledb.OleDbDataReader.get_HasRows|API> %d#\n", this.ObjectID);
				if (this.IsClosed)
				{
					throw ADP.DataReaderClosed("HasRows");
				}
				return this._hasRows;
			}
		}

		// Token: 0x1700060C RID: 1548
		// (get) Token: 0x06002536 RID: 9526 RVA: 0x000FD41C File Offset: 0x000FC81C
		public override bool IsClosed
		{
			get
			{
				Bid.Trace("<oledb.OleDbDataReader.get_IsClosed|API> %d#\n", this.ObjectID);
				return this._isClosed;
			}
		}

		// Token: 0x1700060D RID: 1549
		// (get) Token: 0x06002537 RID: 9527 RVA: 0x000FD440 File Offset: 0x000FC840
		private MetaData[] MetaData
		{
			get
			{
				return this._metadata;
			}
		}

		// Token: 0x1700060E RID: 1550
		// (get) Token: 0x06002538 RID: 9528 RVA: 0x000FD454 File Offset: 0x000FC854
		public override int RecordsAffected
		{
			get
			{
				Bid.Trace("<oledb.OleDbDataReader.get_RecordsAffected|API> %d#\n", this.ObjectID);
				return ADP.IntPtrToInt32(this._recordsAffected);
			}
		}

		// Token: 0x1700060F RID: 1551
		public override object this[int index]
		{
			get
			{
				return this.GetValue(index);
			}
		}

		// Token: 0x17000610 RID: 1552
		public override object this[string name]
		{
			get
			{
				int ordinal = this.GetOrdinal(name);
				return this.GetValue(ordinal);
			}
		}

		// Token: 0x0600253B RID: 9531 RVA: 0x000FD4AC File Offset: 0x000FC8AC
		private UnsafeNativeMethods.IAccessor IAccessor()
		{
			Bid.Trace("<oledb.IUnknown.QueryInterface|API|OLEDB|rowset> %d#, IAccessor\n", this.ObjectID);
			return (UnsafeNativeMethods.IAccessor)this.IRowset();
		}

		// Token: 0x0600253C RID: 9532 RVA: 0x000FD4D4 File Offset: 0x000FC8D4
		private UnsafeNativeMethods.IRowsetInfo IRowsetInfo()
		{
			Bid.Trace("<oledb.IUnknown.QueryInterface|API|OLEDB|rowset> %d#, IRowsetInfo\n", this.ObjectID);
			return (UnsafeNativeMethods.IRowsetInfo)this.IRowset();
		}

		// Token: 0x0600253D RID: 9533 RVA: 0x000FD4FC File Offset: 0x000FC8FC
		private UnsafeNativeMethods.IRowset IRowset()
		{
			UnsafeNativeMethods.IRowset irowset = this._irowset;
			if (irowset == null)
			{
				throw new ObjectDisposedException(base.GetType().Name);
			}
			return irowset;
		}

		// Token: 0x0600253E RID: 9534 RVA: 0x000FD528 File Offset: 0x000FC928
		private UnsafeNativeMethods.IRow IRow()
		{
			UnsafeNativeMethods.IRow irow = this._irow;
			if (irow == null)
			{
				throw new ObjectDisposedException(base.GetType().Name);
			}
			return irow;
		}

		// Token: 0x0600253F RID: 9535 RVA: 0x000FD554 File Offset: 0x000FC954
		public override DataTable GetSchemaTable()
		{
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<oledb.OleDbDataReader.GetSchemaTable|API> %d#\n", this.ObjectID);
			DataTable result;
			try
			{
				DataTable dataTable = this._dbSchemaTable;
				if (dataTable == null)
				{
					MetaData[] metaData = this.MetaData;
					if (metaData != null && metaData.Length != 0)
					{
						if (metaData.Length != 0 && this._useIColumnsRowset && this._connection != null)
						{
							this.AppendSchemaInfo();
						}
						dataTable = this.BuildSchemaTable(metaData);
					}
					else if (this.IsClosed)
					{
						throw ADP.DataReaderClosed("GetSchemaTable");
					}
				}
				result = dataTable;
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
			return result;
		}

		// Token: 0x06002540 RID: 9536 RVA: 0x000FD5EC File Offset: 0x000FC9EC
		internal void BuildMetaInfo()
		{
			if (this._irowset != null)
			{
				if (this._useIColumnsRowset)
				{
					this.BuildSchemaTableRowset(this._irowset);
				}
				else
				{
					this.BuildSchemaTableInfo(this._irowset, false, false);
				}
				if (this._metadata != null && this._metadata.Length != 0)
				{
					this.CreateAccessors(true);
				}
			}
			else if (this._irow != null)
			{
				this.BuildSchemaTableInfo(this._irow, false, false);
				if (this._metadata != null && this._metadata.Length != 0)
				{
					this.CreateBindingsFromMetaData(true);
				}
			}
			if (this._metadata == null)
			{
				this._hasRows = false;
				this._visibleFieldCount = 0;
				this._metadata = new MetaData[0];
			}
		}

		// Token: 0x06002541 RID: 9537 RVA: 0x000FD690 File Offset: 0x000FCA90
		private DataTable BuildSchemaTable(MetaData[] metadata)
		{
			DataTable dataTable = new DataTable("SchemaTable");
			dataTable.Locale = CultureInfo.InvariantCulture;
			dataTable.MinimumCapacity = metadata.Length;
			DataColumn column = new DataColumn("ColumnName", typeof(string));
			DataColumn dataColumn = new DataColumn("ColumnOrdinal", typeof(int));
			DataColumn column2 = new DataColumn("ColumnSize", typeof(int));
			DataColumn column3 = new DataColumn("NumericPrecision", typeof(short));
			DataColumn column4 = new DataColumn("NumericScale", typeof(short));
			DataColumn column5 = new DataColumn("DataType", typeof(Type));
			DataColumn column6 = new DataColumn("ProviderType", typeof(int));
			DataColumn dataColumn2 = new DataColumn("IsLong", typeof(bool));
			DataColumn column7 = new DataColumn("AllowDBNull", typeof(bool));
			DataColumn column8 = new DataColumn("IsReadOnly", typeof(bool));
			DataColumn column9 = new DataColumn("IsRowVersion", typeof(bool));
			DataColumn column10 = new DataColumn("IsUnique", typeof(bool));
			DataColumn column11 = new DataColumn("IsKey", typeof(bool));
			DataColumn column12 = new DataColumn("IsAutoIncrement", typeof(bool));
			DataColumn column13 = new DataColumn("IsHidden", typeof(bool));
			DataColumn column14 = new DataColumn("BaseSchemaName", typeof(string));
			DataColumn column15 = new DataColumn("BaseCatalogName", typeof(string));
			DataColumn column16 = new DataColumn("BaseTableName", typeof(string));
			DataColumn column17 = new DataColumn("BaseColumnName", typeof(string));
			dataColumn.DefaultValue = 0;
			dataColumn2.DefaultValue = false;
			DataColumnCollection columns = dataTable.Columns;
			columns.Add(column);
			columns.Add(dataColumn);
			columns.Add(column2);
			columns.Add(column3);
			columns.Add(column4);
			columns.Add(column5);
			columns.Add(column6);
			columns.Add(dataColumn2);
			columns.Add(column7);
			columns.Add(column8);
			columns.Add(column9);
			columns.Add(column10);
			columns.Add(column11);
			columns.Add(column12);
			if (this._visibleFieldCount < metadata.Length)
			{
				columns.Add(column13);
			}
			columns.Add(column14);
			columns.Add(column15);
			columns.Add(column16);
			columns.Add(column17);
			for (int i = 0; i < metadata.Length; i++)
			{
				MetaData metaData = metadata[i];
				DataRow dataRow = dataTable.NewRow();
				dataRow[column] = metaData.columnName;
				dataRow[dataColumn] = i;
				dataRow[column2] = ((metaData.type.enumOleDbType != OleDbType.BSTR) ? metaData.size : -1);
				dataRow[column3] = metaData.precision;
				dataRow[column4] = metaData.scale;
				dataRow[column5] = metaData.type.dataType;
				dataRow[column6] = metaData.type.enumOleDbType;
				dataRow[dataColumn2] = OleDbDataReader.IsLong(metaData.flags);
				if (metaData.isKeyColumn)
				{
					dataRow[column7] = OleDbDataReader.AllowDBNull(metaData.flags);
				}
				else
				{
					dataRow[column7] = OleDbDataReader.AllowDBNullMaybeNull(metaData.flags);
				}
				dataRow[column8] = OleDbDataReader.IsReadOnly(metaData.flags);
				dataRow[column9] = OleDbDataReader.IsRowVersion(metaData.flags);
				dataRow[column10] = metaData.isUnique;
				dataRow[column11] = metaData.isKeyColumn;
				dataRow[column12] = metaData.isAutoIncrement;
				if (this._visibleFieldCount < metadata.Length)
				{
					dataRow[column13] = metaData.isHidden;
				}
				if (metaData.baseSchemaName != null)
				{
					dataRow[column14] = metaData.baseSchemaName;
				}
				if (metaData.baseCatalogName != null)
				{
					dataRow[column15] = metaData.baseCatalogName;
				}
				if (metaData.baseTableName != null)
				{
					dataRow[column16] = metaData.baseTableName;
				}
				if (metaData.baseColumnName != null)
				{
					dataRow[column17] = metaData.baseColumnName;
				}
				dataTable.Rows.Add(dataRow);
				dataRow.AcceptChanges();
			}
			int count = columns.Count;
			for (int j = 0; j < count; j++)
			{
				columns[j].ReadOnly = true;
			}
			this._dbSchemaTable = dataTable;
			return dataTable;
		}

		// Token: 0x06002542 RID: 9538 RVA: 0x000FDB4C File Offset: 0x000FCF4C
		private void BuildSchemaTableInfo(object handle, bool filterITypeInfo, bool filterChapters)
		{
			Bid.Trace("<oledb.IUnknown.QueryInterface|API|OLEDB|rowset_row> %d#, IColumnsInfo\n", this.ObjectID);
			UnsafeNativeMethods.IColumnsInfo columnsInfo = handle as UnsafeNativeMethods.IColumnsInfo;
			if (columnsInfo == null)
			{
				Bid.Trace("<oledb.IUnknown.QueryInterface|API|OLEDB|RET> %08X{HRESULT}\n", OleDbHResult.E_NOINTERFACE);
				this._dbSchemaTable = null;
				return;
			}
			IntPtr ptrZero = ADP.PtrZero;
			IntPtr ptrZero2 = ADP.PtrZero;
			OleDbHResult oleDbHResult;
			using (new DualCoTaskMem(columnsInfo, ref ptrZero, ref ptrZero2, ref oleDbHResult))
			{
				if (oleDbHResult < OleDbHResult.S_OK)
				{
					this.ProcessResults(oleDbHResult);
				}
				if (0 < (int)ptrZero)
				{
					this.BuildSchemaTableInfoTable(ptrZero.ToInt32(), ptrZero2, filterITypeInfo, filterChapters);
				}
			}
		}

		// Token: 0x06002543 RID: 9539 RVA: 0x000FDBF4 File Offset: 0x000FCFF4
		private void BuildSchemaTableInfoTable(int columnCount, IntPtr columnInfos, bool filterITypeInfo, bool filterChapters)
		{
			int num = 0;
			MetaData[] array = new MetaData[columnCount];
			tagDBCOLUMNINFO tagDBCOLUMNINFO = new tagDBCOLUMNINFO();
			int i = 0;
			int num2 = 0;
			while (i < columnCount)
			{
				Marshal.PtrToStructure(ADP.IntPtrOffset(columnInfos, num2), tagDBCOLUMNINFO);
				if (0L < (long)tagDBCOLUMNINFO.iOrdinal && !OleDbDataReader.DoColumnDropFilter(tagDBCOLUMNINFO.dwFlags))
				{
					if (tagDBCOLUMNINFO.pwszName == null)
					{
						tagDBCOLUMNINFO.pwszName = "";
					}
					if ((!filterITypeInfo || !("DBCOLUMN_TYPEINFO" == tagDBCOLUMNINFO.pwszName)) && (!filterChapters || 136 != tagDBCOLUMNINFO.wType))
					{
						bool isLong = OleDbDataReader.IsLong(tagDBCOLUMNINFO.dwFlags);
						bool isFixed = OleDbDataReader.IsFixed(tagDBCOLUMNINFO.dwFlags);
						NativeDBType type = NativeDBType.FromDBType(tagDBCOLUMNINFO.wType, isLong, isFixed);
						MetaData metaData = new MetaData();
						metaData.columnName = tagDBCOLUMNINFO.pwszName;
						metaData.type = type;
						metaData.ordinal = tagDBCOLUMNINFO.iOrdinal;
						long num3 = (long)tagDBCOLUMNINFO.ulColumnSize;
						metaData.size = ((num3 < 0L || 2147483647L < num3) ? int.MaxValue : ((int)num3));
						metaData.flags = tagDBCOLUMNINFO.dwFlags;
						metaData.precision = tagDBCOLUMNINFO.bPrecision;
						metaData.scale = tagDBCOLUMNINFO.bScale;
						metaData.kind = tagDBCOLUMNINFO.columnid.eKind;
						int eKind = tagDBCOLUMNINFO.columnid.eKind;
						if (eKind <= 1 || eKind == 6)
						{
							metaData.guid = tagDBCOLUMNINFO.columnid.uGuid;
						}
						else
						{
							metaData.guid = Guid.Empty;
						}
						switch (tagDBCOLUMNINFO.columnid.eKind)
						{
						case 0:
						case 2:
							if (ADP.PtrZero != tagDBCOLUMNINFO.columnid.ulPropid)
							{
								metaData.idname = Marshal.PtrToStringUni(tagDBCOLUMNINFO.columnid.ulPropid);
							}
							else
							{
								metaData.idname = null;
							}
							break;
						case 1:
						case 5:
							metaData.propid = tagDBCOLUMNINFO.columnid.ulPropid;
							break;
						case 3:
						case 4:
							goto IL_1EB;
						default:
							goto IL_1EB;
						}
						IL_1F6:
						array[num] = metaData;
						num++;
						goto IL_1FF;
						IL_1EB:
						metaData.propid = ADP.PtrZero;
						goto IL_1F6;
					}
				}
				IL_1FF:
				i++;
				num2 += ODB.SizeOf_tagDBCOLUMNINFO;
			}
			if (num < columnCount)
			{
				MetaData[] array2 = new MetaData[num];
				for (int j = 0; j < num; j++)
				{
					array2[j] = array[j];
				}
				array = array2;
			}
			this._visibleFieldCount = num;
			this._metadata = array;
		}

		// Token: 0x06002544 RID: 9540 RVA: 0x000FDE4C File Offset: 0x000FD24C
		private void BuildSchemaTableRowset(object handle)
		{
			Bid.Trace("<oledb.IUnknown.QueryInterface|API|OLEDB|rowset_row> %d, IColumnsRowset\n", this.ObjectID);
			UnsafeNativeMethods.IColumnsRowset columnsRowset = handle as UnsafeNativeMethods.IColumnsRowset;
			if (columnsRowset != null)
			{
				UnsafeNativeMethods.IRowset rowset = null;
				IntPtr cOptColumns;
				OleDbHResult columnsRowset2;
				using (DualCoTaskMem dualCoTaskMem = new DualCoTaskMem(columnsRowset, ref cOptColumns, ref columnsRowset2))
				{
					Bid.Trace("<oledb.IColumnsRowset.GetColumnsRowset|API|OLEDB> %d#, IID_IRowset\n", this.ObjectID);
					columnsRowset2 = columnsRowset.GetColumnsRowset(ADP.PtrZero, cOptColumns, dualCoTaskMem, ref ODB.IID_IRowset, 0, ADP.PtrZero, out rowset);
					Bid.Trace("<oledb.IColumnsRowset.GetColumnsRowset|API|OLEDB|RET> %08X{HRESULT}\n", columnsRowset2);
				}
				if (columnsRowset2 < OleDbHResult.S_OK)
				{
					this.ProcessResults(columnsRowset2);
				}
				this.DumpToSchemaTable(rowset);
				if (rowset != null)
				{
					Marshal.ReleaseComObject(rowset);
					return;
				}
			}
			else
			{
				Bid.Trace("<oledb.IUnknown.QueryInterface|API|OLEDB|RET> %08X{HRESULT}\n", OleDbHResult.E_NOINTERFACE);
				this._useIColumnsRowset = false;
				this.BuildSchemaTableInfo(handle, false, false);
			}
		}

		// Token: 0x06002545 RID: 9541 RVA: 0x000FDF1C File Offset: 0x000FD31C
		public override void Close()
		{
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<oledb.OleDbDataReader.Close|API> %d#\n", this.ObjectID);
			try
			{
				OleDbConnection connection = this._connection;
				OleDbCommand command = this._command;
				Bindings bindings = this._parameterBindings;
				this._connection = null;
				this._command = null;
				this._parameterBindings = null;
				this._isClosed = true;
				this.DisposeOpenResults();
				this._hasRows = false;
				if (command != null && command.canceling)
				{
					this.DisposeNativeMultipleResults();
					if (bindings != null)
					{
						bindings.CloseFromConnection();
						bindings = null;
					}
				}
				else
				{
					UnsafeNativeMethods.IMultipleResults imultipleResults = this._imultipleResults;
					this._imultipleResults = null;
					if (imultipleResults != null)
					{
						try
						{
							if (command != null && !command.canceling)
							{
								IntPtr zero = IntPtr.Zero;
								OleDbException ex = OleDbDataReader.NextResults(imultipleResults, null, command, out zero);
								this._recordsAffected = OleDbDataReader.AddRecordsAffected(this._recordsAffected, zero);
								if (ex != null)
								{
									throw ex;
								}
							}
						}
						finally
						{
							if (imultipleResults != null)
							{
								Marshal.ReleaseComObject(imultipleResults);
							}
						}
					}
				}
				if (command != null && this._depth == 0)
				{
					command.CloseFromDataReader(bindings);
				}
				if (connection != null)
				{
					connection.RemoveWeakReference(this);
					if (this.IsCommandBehavior(CommandBehavior.CloseConnection))
					{
						connection.Close();
					}
				}
				RowHandleBuffer rowHandleNativeBuffer = this._rowHandleNativeBuffer;
				this._rowHandleNativeBuffer = null;
				if (rowHandleNativeBuffer != null)
				{
					rowHandleNativeBuffer.Dispose();
				}
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
		}

		// Token: 0x06002546 RID: 9542 RVA: 0x000FE074 File Offset: 0x000FD474
		internal void CloseReaderFromConnection(bool canceling)
		{
			if (this._command != null)
			{
				this._command.canceling = canceling;
			}
			this._connection = null;
			this.Close();
		}

		// Token: 0x06002547 RID: 9543 RVA: 0x000FE0A4 File Offset: 0x000FD4A4
		private void DisposeManagedRowset()
		{
			this._isRead = false;
			this._hasRowsReadCheck = false;
			this._nextAccessorForRetrieval = 0;
			this._nextValueForRetrieval = 0;
			Bindings[] bindings = this._bindings;
			this._bindings = null;
			if (bindings != null)
			{
				for (int i = 0; i < bindings.Length; i++)
				{
					if (bindings[i] != null)
					{
						bindings[i].Dispose();
					}
				}
			}
			this._currentRow = 0;
			this._rowFetchedCount = IntPtr.Zero;
			this._dbSchemaTable = null;
			this._visibleFieldCount = 0;
			this._metadata = null;
			this._fieldNameLookup = null;
		}

		// Token: 0x06002548 RID: 9544 RVA: 0x000FE128 File Offset: 0x000FD528
		private void DisposeNativeMultipleResults()
		{
			UnsafeNativeMethods.IMultipleResults imultipleResults = this._imultipleResults;
			this._imultipleResults = null;
			if (imultipleResults != null)
			{
				Marshal.ReleaseComObject(imultipleResults);
			}
		}

		// Token: 0x06002549 RID: 9545 RVA: 0x000FE150 File Offset: 0x000FD550
		private void DisposeNativeRowset()
		{
			UnsafeNativeMethods.IRowset irowset = this._irowset;
			this._irowset = null;
			ChapterHandle chapterHandle = this._chapterHandle;
			this._chapterHandle = ChapterHandle.DB_NULL_HCHAPTER;
			if (ChapterHandle.DB_NULL_HCHAPTER != chapterHandle)
			{
				chapterHandle.Dispose();
			}
			if (irowset != null)
			{
				Marshal.ReleaseComObject(irowset);
			}
		}

		// Token: 0x0600254A RID: 9546 RVA: 0x000FE198 File Offset: 0x000FD598
		private void DisposeNativeRow()
		{
			UnsafeNativeMethods.IRow irow = this._irow;
			this._irow = null;
			if (irow != null)
			{
				Marshal.ReleaseComObject(irow);
			}
		}

		// Token: 0x0600254B RID: 9547 RVA: 0x000FE1C0 File Offset: 0x000FD5C0
		private void DisposeOpenResults()
		{
			this.DisposeManagedRowset();
			this.DisposeNativeRow();
			this.DisposeNativeRowset();
		}

		// Token: 0x0600254C RID: 9548 RVA: 0x000FE1E0 File Offset: 0x000FD5E0
		public override bool GetBoolean(int ordinal)
		{
			ColumnBinding columnBinding = this.GetColumnBinding(ordinal);
			return columnBinding.ValueBoolean();
		}

		// Token: 0x0600254D RID: 9549 RVA: 0x000FE1FC File Offset: 0x000FD5FC
		public override byte GetByte(int ordinal)
		{
			ColumnBinding columnBinding = this.GetColumnBinding(ordinal);
			return columnBinding.ValueByte();
		}

		// Token: 0x0600254E RID: 9550 RVA: 0x000FE218 File Offset: 0x000FD618
		private ColumnBinding DoSequentialCheck(int ordinal, long dataIndex, string method)
		{
			ColumnBinding columnBinding = this.GetColumnBinding(ordinal);
			if (dataIndex > 2147483647L)
			{
				throw ADP.InvalidSourceBufferIndex(0, dataIndex, "dataIndex");
			}
			if (this._sequentialOrdinal != ordinal)
			{
				this._sequentialOrdinal = ordinal;
				this._sequentialBytesRead = 0L;
			}
			else if (this._sequentialAccess && this._sequentialBytesRead < dataIndex)
			{
				throw ADP.NonSeqByteAccess(dataIndex, this._sequentialBytesRead, method);
			}
			return columnBinding;
		}

		// Token: 0x0600254F RID: 9551 RVA: 0x000FE280 File Offset: 0x000FD680
		public override long GetBytes(int ordinal, long dataIndex, byte[] buffer, int bufferIndex, int length)
		{
			ColumnBinding columnBinding = this.DoSequentialCheck(ordinal, dataIndex, "GetBytes");
			byte[] array = columnBinding.ValueByteArray();
			if (buffer == null)
			{
				return (long)array.Length;
			}
			int num = (int)dataIndex;
			int num2 = Math.Min(array.Length - num, length);
			if (num < 0)
			{
				throw ADP.InvalidSourceBufferIndex(array.Length, (long)num, "dataIndex");
			}
			if (bufferIndex < 0 || bufferIndex >= buffer.Length)
			{
				throw ADP.InvalidDestinationBufferIndex(buffer.Length, bufferIndex, "bufferIndex");
			}
			if (0 < num2)
			{
				Buffer.BlockCopy(array, num, buffer, bufferIndex, num2);
				this._sequentialBytesRead = (long)(num + num2);
			}
			else
			{
				if (length < 0)
				{
					throw ADP.InvalidDataLength((long)length);
				}
				num2 = 0;
			}
			return (long)num2;
		}

		// Token: 0x06002550 RID: 9552 RVA: 0x000FE318 File Offset: 0x000FD718
		public override long GetChars(int ordinal, long dataIndex, char[] buffer, int bufferIndex, int length)
		{
			ColumnBinding columnBinding = this.DoSequentialCheck(ordinal, dataIndex, "GetChars");
			string text = columnBinding.ValueString();
			if (buffer == null)
			{
				return (long)text.Length;
			}
			int num = (int)dataIndex;
			int num2 = Math.Min(text.Length - num, length);
			if (num < 0)
			{
				throw ADP.InvalidSourceBufferIndex(text.Length, (long)num, "dataIndex");
			}
			if (bufferIndex < 0 || bufferIndex >= buffer.Length)
			{
				throw ADP.InvalidDestinationBufferIndex(buffer.Length, bufferIndex, "bufferIndex");
			}
			if (0 < num2)
			{
				text.CopyTo(num, buffer, bufferIndex, num2);
				this._sequentialBytesRead = (long)(num + num2);
			}
			else
			{
				if (length < 0)
				{
					throw ADP.InvalidDataLength((long)length);
				}
				num2 = 0;
			}
			return (long)num2;
		}

		// Token: 0x06002551 RID: 9553 RVA: 0x000FE3B8 File Offset: 0x000FD7B8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override char GetChar(int ordinal)
		{
			throw ADP.NotSupported();
		}

		// Token: 0x06002552 RID: 9554 RVA: 0x000FE3CC File Offset: 0x000FD7CC
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public new OleDbDataReader GetData(int ordinal)
		{
			ColumnBinding columnBinding = this.GetColumnBinding(ordinal);
			return columnBinding.ValueChapter();
		}

		// Token: 0x06002553 RID: 9555 RVA: 0x000FE3E8 File Offset: 0x000FD7E8
		protected override DbDataReader GetDbDataReader(int ordinal)
		{
			return this.GetData(ordinal);
		}

		// Token: 0x06002554 RID: 9556 RVA: 0x000FE3FC File Offset: 0x000FD7FC
		internal OleDbDataReader ResetChapter(int bindingIndex, int index, RowBinding rowbinding, int valueOffset)
		{
			return this.GetDataForReader(this._metadata[bindingIndex + index].ordinal, rowbinding, valueOffset);
		}

		// Token: 0x06002555 RID: 9557 RVA: 0x000FE424 File Offset: 0x000FD824
		private OleDbDataReader GetDataForReader(IntPtr ordinal, RowBinding rowbinding, int valueOffset)
		{
			UnsafeNativeMethods.IRowsetInfo rowsetInfo = this.IRowsetInfo();
			Bid.Trace("<oledb.IRowsetInfo.GetReferencedRowset|API|OLEDB> %d#, ColumnOrdinal=%Id\n", this.ObjectID, ordinal);
			UnsafeNativeMethods.IRowset rowset;
			OleDbHResult referencedRowset = rowsetInfo.GetReferencedRowset(ordinal, ref ODB.IID_IRowset, out rowset);
			Bid.Trace("<oledb.IRowsetInfo.GetReferencedRowset|API|OLEDB|RET> %08X{HRESULT}\n", referencedRowset);
			this.ProcessResults(referencedRowset);
			OleDbDataReader oleDbDataReader = null;
			if (rowset != null)
			{
				ChapterHandle chapterHandle = ChapterHandle.CreateChapterHandle(rowset, rowbinding, valueOffset);
				oleDbDataReader = new OleDbDataReader(this._connection, this._command, 1 + this.Depth, this._commandBehavior & ~CommandBehavior.CloseConnection);
				oleDbDataReader.InitializeIRowset(rowset, chapterHandle, ADP.RecordsUnaffected);
				oleDbDataReader.BuildMetaInfo();
				oleDbDataReader.HasRowsRead();
				if (this._connection != null)
				{
					this._connection.AddWeakReference(oleDbDataReader, 2);
				}
			}
			return oleDbDataReader;
		}

		// Token: 0x06002556 RID: 9558 RVA: 0x000FE4CC File Offset: 0x000FD8CC
		public override string GetDataTypeName(int index)
		{
			if (this._metadata != null)
			{
				return this._metadata[index].type.dataSourceType;
			}
			throw ADP.DataReaderNoData();
		}

		// Token: 0x06002557 RID: 9559 RVA: 0x000FE4FC File Offset: 0x000FD8FC
		public override DateTime GetDateTime(int ordinal)
		{
			ColumnBinding columnBinding = this.GetColumnBinding(ordinal);
			return columnBinding.ValueDateTime();
		}

		// Token: 0x06002558 RID: 9560 RVA: 0x000FE518 File Offset: 0x000FD918
		public override decimal GetDecimal(int ordinal)
		{
			ColumnBinding columnBinding = this.GetColumnBinding(ordinal);
			return columnBinding.ValueDecimal();
		}

		// Token: 0x06002559 RID: 9561 RVA: 0x000FE534 File Offset: 0x000FD934
		public override double GetDouble(int ordinal)
		{
			ColumnBinding columnBinding = this.GetColumnBinding(ordinal);
			return columnBinding.ValueDouble();
		}

		// Token: 0x0600255A RID: 9562 RVA: 0x000FE550 File Offset: 0x000FD950
		public override IEnumerator GetEnumerator()
		{
			return new DbEnumerator(this, this.IsCommandBehavior(CommandBehavior.CloseConnection));
		}

		// Token: 0x0600255B RID: 9563 RVA: 0x000FE56C File Offset: 0x000FD96C
		public override Type GetFieldType(int index)
		{
			if (this._metadata != null)
			{
				return this._metadata[index].type.dataType;
			}
			throw ADP.DataReaderNoData();
		}

		// Token: 0x0600255C RID: 9564 RVA: 0x000FE59C File Offset: 0x000FD99C
		public override float GetFloat(int ordinal)
		{
			ColumnBinding columnBinding = this.GetColumnBinding(ordinal);
			return columnBinding.ValueSingle();
		}

		// Token: 0x0600255D RID: 9565 RVA: 0x000FE5B8 File Offset: 0x000FD9B8
		public override Guid GetGuid(int ordinal)
		{
			ColumnBinding columnBinding = this.GetColumnBinding(ordinal);
			return columnBinding.ValueGuid();
		}

		// Token: 0x0600255E RID: 9566 RVA: 0x000FE5D4 File Offset: 0x000FD9D4
		public override short GetInt16(int ordinal)
		{
			ColumnBinding columnBinding = this.GetColumnBinding(ordinal);
			return columnBinding.ValueInt16();
		}

		// Token: 0x0600255F RID: 9567 RVA: 0x000FE5F0 File Offset: 0x000FD9F0
		public override int GetInt32(int ordinal)
		{
			ColumnBinding columnBinding = this.GetColumnBinding(ordinal);
			return columnBinding.ValueInt32();
		}

		// Token: 0x06002560 RID: 9568 RVA: 0x000FE60C File Offset: 0x000FDA0C
		public override long GetInt64(int ordinal)
		{
			ColumnBinding columnBinding = this.GetColumnBinding(ordinal);
			return columnBinding.ValueInt64();
		}

		// Token: 0x06002561 RID: 9569 RVA: 0x000FE628 File Offset: 0x000FDA28
		public override string GetName(int index)
		{
			if (this._metadata != null)
			{
				return this._metadata[index].columnName;
			}
			throw ADP.DataReaderNoData();
		}

		// Token: 0x06002562 RID: 9570 RVA: 0x000FE650 File Offset: 0x000FDA50
		public override int GetOrdinal(string name)
		{
			if (this._fieldNameLookup == null)
			{
				if (this._metadata == null)
				{
					throw ADP.DataReaderNoData();
				}
				this._fieldNameLookup = new FieldNameLookup(this, -1);
			}
			return this._fieldNameLookup.GetOrdinal(name);
		}

		// Token: 0x06002563 RID: 9571 RVA: 0x000FE68C File Offset: 0x000FDA8C
		public override string GetString(int ordinal)
		{
			ColumnBinding columnBinding = this.GetColumnBinding(ordinal);
			return columnBinding.ValueString();
		}

		// Token: 0x06002564 RID: 9572 RVA: 0x000FE6A8 File Offset: 0x000FDAA8
		public TimeSpan GetTimeSpan(int ordinal)
		{
			return (TimeSpan)this.GetValue(ordinal);
		}

		// Token: 0x06002565 RID: 9573 RVA: 0x000FE6C4 File Offset: 0x000FDAC4
		private MetaData DoValueCheck(int ordinal)
		{
			if (!this._isRead)
			{
				throw ADP.DataReaderNoData();
			}
			if (this._sequentialAccess && ordinal < this._nextValueForRetrieval)
			{
				throw ADP.NonSequentialColumnAccess(ordinal, this._nextValueForRetrieval);
			}
			return this._metadata[ordinal];
		}

		// Token: 0x06002566 RID: 9574 RVA: 0x000FE708 File Offset: 0x000FDB08
		private ColumnBinding GetColumnBinding(int ordinal)
		{
			MetaData info = this.DoValueCheck(ordinal);
			return this.GetValueBinding(info);
		}

		// Token: 0x06002567 RID: 9575 RVA: 0x000FE724 File Offset: 0x000FDB24
		private ColumnBinding GetValueBinding(MetaData info)
		{
			ColumnBinding columnBinding = info.columnBinding;
			for (int i = this._nextAccessorForRetrieval; i <= columnBinding.IndexForAccessor; i++)
			{
				if (this._sequentialAccess)
				{
					if (this._nextValueForRetrieval != columnBinding.Index)
					{
						this._metadata[this._nextValueForRetrieval].columnBinding.ResetValue();
					}
					this._nextAccessorForRetrieval = columnBinding.IndexForAccessor;
				}
				if (this._irowset != null)
				{
					this.GetRowDataFromHandle();
				}
				else
				{
					if (this._irow == null)
					{
						throw ADP.DataReaderNoData();
					}
					this.GetRowValue();
				}
			}
			this._nextValueForRetrieval = columnBinding.Index;
			return columnBinding;
		}

		// Token: 0x06002568 RID: 9576 RVA: 0x000FE7BC File Offset: 0x000FDBBC
		public override object GetValue(int ordinal)
		{
			ColumnBinding columnBinding = this.GetColumnBinding(ordinal);
			return columnBinding.Value();
		}

		// Token: 0x06002569 RID: 9577 RVA: 0x000FE7DC File Offset: 0x000FDBDC
		public override int GetValues(object[] values)
		{
			if (values == null)
			{
				throw ADP.ArgumentNull("values");
			}
			MetaData metaData = this.DoValueCheck(0);
			int num = Math.Min(values.Length, this._visibleFieldCount);
			int num2 = 0;
			while (num2 < this._metadata.Length && num2 < num)
			{
				ColumnBinding valueBinding = this.GetValueBinding(this._metadata[num2]);
				values[num2] = valueBinding.Value();
				num2++;
			}
			return num;
		}

		// Token: 0x0600256A RID: 9578 RVA: 0x000FE840 File Offset: 0x000FDC40
		private bool IsCommandBehavior(CommandBehavior condition)
		{
			return condition == (condition & this._commandBehavior);
		}

		// Token: 0x0600256B RID: 9579 RVA: 0x000FE858 File Offset: 0x000FDC58
		public override bool IsDBNull(int ordinal)
		{
			ColumnBinding columnBinding = this.GetColumnBinding(ordinal);
			return columnBinding.IsValueNull();
		}

		// Token: 0x0600256C RID: 9580 RVA: 0x000FE874 File Offset: 0x000FDC74
		private void ProcessResults(OleDbHResult hr)
		{
			Exception ex;
			if (this._command != null)
			{
				ex = OleDbConnection.ProcessResults(hr, this._connection, this._command);
			}
			else
			{
				ex = OleDbConnection.ProcessResults(hr, this._connection, this._connection);
			}
			if (ex != null)
			{
				throw ex;
			}
		}

		// Token: 0x0600256D RID: 9581 RVA: 0x000FE8B8 File Offset: 0x000FDCB8
		private static IntPtr AddRecordsAffected(IntPtr recordsAffected, IntPtr affected)
		{
			if (0L > (long)affected)
			{
				return recordsAffected;
			}
			if (0L <= (long)recordsAffected)
			{
				return (IntPtr)((long)recordsAffected + (long)affected);
			}
			return affected;
		}

		// Token: 0x17000611 RID: 1553
		// (get) Token: 0x0600256E RID: 9582 RVA: 0x000FE8F0 File Offset: 0x000FDCF0
		public override int VisibleFieldCount
		{
			get
			{
				Bid.Trace("<oledb.OleDbDataReader.get_VisibleFieldCount|API> %d#\n", this.ObjectID);
				if (this.IsClosed)
				{
					throw ADP.DataReaderClosed("VisibleFieldCount");
				}
				return this._visibleFieldCount;
			}
		}

		// Token: 0x0600256F RID: 9583 RVA: 0x000FE928 File Offset: 0x000FDD28
		internal void HasRowsRead()
		{
			bool hasRows = this.Read();
			this._hasRows = hasRows;
			this._hasRowsReadCheck = true;
			this._isRead = false;
		}

		// Token: 0x06002570 RID: 9584 RVA: 0x000FE954 File Offset: 0x000FDD54
		internal static OleDbException NextResults(UnsafeNativeMethods.IMultipleResults imultipleResults, OleDbConnection connection, OleDbCommand command, out IntPtr recordsAffected)
		{
			recordsAffected = ADP.RecordsUnaffected;
			List<OleDbException> list = null;
			if (imultipleResults != null)
			{
				int num = 0;
				while (command == null || !command.canceling)
				{
					Bid.Trace("<oledb.IMultipleResults.GetResult|API|OLEDB> DBRESULTFLAG_DEFAULT, IID_NULL\n");
					IntPtr intPtr;
					object obj;
					OleDbHResult result = imultipleResults.GetResult(ADP.PtrZero, ODB.DBRESULTFLAG_DEFAULT, ref ODB.IID_NULL, out intPtr, out obj);
					Bid.Trace("<oledb.IMultipleResults.GetResult|API|OLEDB|RET> %08X{HRESULT}, RecordAffected=%Id\n", result, intPtr);
					if (OleDbHResult.DB_S_NORESULT == result || OleDbHResult.E_NOINTERFACE == result)
					{
						break;
					}
					if (connection != null)
					{
						Exception ex = OleDbConnection.ProcessResults(result, connection, command);
						if (ex != null)
						{
							OleDbException ex2 = ex as OleDbException;
							if (ex2 == null)
							{
								throw ex;
							}
							if (list == null)
							{
								list = new List<OleDbException>();
							}
							list.Add(ex2);
						}
					}
					else if (result < OleDbHResult.S_OK)
					{
						SafeNativeMethods.Wrapper.ClearErrorInfo();
						break;
					}
					recordsAffected = OleDbDataReader.AddRecordsAffected(recordsAffected, intPtr);
					if ((int)intPtr != 0)
					{
						num = 0;
					}
					else if (2000 <= num)
					{
						OleDbDataReader.NextResultsInfinite();
						break;
					}
					num++;
				}
			}
			if (list != null)
			{
				return OleDbException.CombineExceptions(list);
			}
			return null;
		}

		// Token: 0x06002571 RID: 9585 RVA: 0x000FEA38 File Offset: 0x000FDE38
		private static void NextResultsInfinite()
		{
			Bid.Trace("<oledb.OleDbDataReader.NextResultsInfinite|INFO> System.Data.OleDb.OleDbDataReader: 2000 IMultipleResult.GetResult(NULL, DBRESULTFLAG_DEFAULT, IID_NULL, NULL, NULL) iterations with 0 records affected. Stopping suspect infinite loop. To work-around try using ExecuteReader() and iterating through results with NextResult().\n");
		}

		// Token: 0x06002572 RID: 9586 RVA: 0x000FEA50 File Offset: 0x000FDE50
		public override bool NextResult()
		{
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<oledb.OleDbDataReader.NextResult|API> %d#\n", this.ObjectID);
			bool result2;
			try
			{
				bool flag = false;
				if (this.IsClosed)
				{
					throw ADP.DataReaderClosed("NextResult");
				}
				this._fieldNameLookup = null;
				OleDbCommand command = this._command;
				UnsafeNativeMethods.IMultipleResults imultipleResults = this._imultipleResults;
				if (imultipleResults != null)
				{
					this.DisposeOpenResults();
					this._hasRows = false;
					for (;;)
					{
						object obj = null;
						if (command != null && command.canceling)
						{
							break;
						}
						Bid.Trace("<oledb.IMultipleResults.GetResult|API|OLEDB> %d#, IID_IRowset\n", this.ObjectID);
						IntPtr intPtr2;
						OleDbHResult result = imultipleResults.GetResult(ADP.PtrZero, ODB.DBRESULTFLAG_DEFAULT, ref ODB.IID_IRowset, out intPtr2, out obj);
						Bid.Trace("<oledb.IMultipleResults.GetResult|API|OLEDB|RET> %08X{HRESULT}, RecordAffected=%Id\n", result, intPtr2);
						if (OleDbHResult.S_OK <= result && obj != null)
						{
							Bid.Trace("<oledb.IUnknown.QueryInterface|API|OLEDB|RowSet> %d#, IRowset\n", this.ObjectID);
							this._irowset = (UnsafeNativeMethods.IRowset)obj;
						}
						this._recordsAffected = OleDbDataReader.AddRecordsAffected(this._recordsAffected, intPtr2);
						if (OleDbHResult.DB_S_NORESULT == result)
						{
							goto Block_9;
						}
						this.ProcessResults(result);
						if (this._irowset != null)
						{
							goto Block_10;
						}
					}
					this.Close();
					goto IL_116;
					Block_9:
					this.DisposeNativeMultipleResults();
					goto IL_116;
					Block_10:
					this.BuildMetaInfo();
					this.HasRowsRead();
					flag = true;
				}
				else
				{
					this.DisposeOpenResults();
					this._hasRows = false;
				}
				IL_116:
				result2 = flag;
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
			return result2;
		}

		// Token: 0x06002573 RID: 9587 RVA: 0x000FEBA0 File Offset: 0x000FDFA0
		public override bool Read()
		{
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<oledb.OleDbDataReader.Read|API> %d#\n", this.ObjectID);
			bool result;
			try
			{
				bool flag = false;
				OleDbCommand command = this._command;
				if (command != null && command.canceling)
				{
					this.DisposeOpenResults();
				}
				else if (this._irowset != null)
				{
					if (this._hasRowsReadCheck)
					{
						flag = (this._isRead = this._hasRows);
						this._hasRowsReadCheck = false;
					}
					else if (this._singleRow && this._isRead)
					{
						this.DisposeOpenResults();
					}
					else
					{
						flag = this.ReadRowset();
					}
				}
				else if (this._irow != null)
				{
					flag = this.ReadRow();
				}
				else if (this.IsClosed)
				{
					throw ADP.DataReaderClosed("Read");
				}
				result = flag;
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
			return result;
		}

		// Token: 0x06002574 RID: 9588 RVA: 0x000FEC70 File Offset: 0x000FE070
		private bool ReadRow()
		{
			if (this._isRead)
			{
				this._isRead = false;
				this.DisposeNativeRow();
				this._sequentialOrdinal = -1;
				return false;
			}
			this._isRead = true;
			return this._metadata.Length != 0;
		}

		// Token: 0x06002575 RID: 9589 RVA: 0x000FECB0 File Offset: 0x000FE0B0
		private bool ReadRowset()
		{
			this.ReleaseCurrentRow();
			this._sequentialOrdinal = -1;
			if (IntPtr.Zero == this._rowFetchedCount)
			{
				this.GetRowHandles();
			}
			return this._currentRow <= (int)this._rowFetchedCount && this._isRead;
		}

		// Token: 0x06002576 RID: 9590 RVA: 0x000FED00 File Offset: 0x000FE100
		private void ReleaseCurrentRow()
		{
			if (0 < (int)this._rowFetchedCount)
			{
				Bindings[] bindings = this._bindings;
				int num = 0;
				while (num < bindings.Length && num < this._nextAccessorForRetrieval)
				{
					bindings[num].CleanupBindings();
					num++;
				}
				this._nextAccessorForRetrieval = 0;
				this._nextValueForRetrieval = 0;
				this._currentRow++;
				if (this._currentRow == (int)this._rowFetchedCount)
				{
					this.ReleaseRowHandles();
				}
			}
		}

		// Token: 0x06002577 RID: 9591 RVA: 0x000FED78 File Offset: 0x000FE178
		private void CreateAccessors(bool allowMultipleAccessor)
		{
			Bindings[] array = this.CreateBindingsFromMetaData(allowMultipleAccessor);
			UnsafeNativeMethods.IAccessor iaccessor = this.IAccessor();
			for (int i = 0; i < array.Length; i++)
			{
				OleDbHResult oleDbHResult = array[i].CreateAccessor(iaccessor, 2);
				if (oleDbHResult < OleDbHResult.S_OK)
				{
					this.ProcessResults(oleDbHResult);
				}
			}
			if (IntPtr.Zero == this._rowHandleFetchCount)
			{
				this._rowHandleFetchCount = new IntPtr(1);
				object propertyValue = this.GetPropertyValue(73);
				if (propertyValue is int)
				{
					this._rowHandleFetchCount = new IntPtr((int)propertyValue);
					if (ADP.PtrZero == this._rowHandleFetchCount || 20 <= (int)this._rowHandleFetchCount)
					{
						this._rowHandleFetchCount = new IntPtr(20);
					}
				}
				else if (propertyValue is long)
				{
					this._rowHandleFetchCount = new IntPtr((long)propertyValue);
					if (ADP.PtrZero == this._rowHandleFetchCount || 20L <= (long)this._rowHandleFetchCount)
					{
						this._rowHandleFetchCount = new IntPtr(20);
					}
				}
			}
			if (this._rowHandleNativeBuffer == null)
			{
				this._rowHandleNativeBuffer = new RowHandleBuffer(this._rowHandleFetchCount);
			}
		}

		// Token: 0x06002578 RID: 9592 RVA: 0x000FEE90 File Offset: 0x000FE290
		private Bindings[] CreateBindingsFromMetaData(bool allowMultipleAccessor)
		{
			int num = 0;
			int num2 = 0;
			MetaData[] metadata = this._metadata;
			int[] array = new int[metadata.Length];
			int[] array2 = new int[metadata.Length];
			if (allowMultipleAccessor)
			{
				if (this._irowset != null)
				{
					for (int i = 0; i < array.Length; i++)
					{
						array[i] = num;
						array2[i] = num2;
						num2++;
					}
					if (0 < num2)
					{
						num++;
					}
				}
				else if (this._irow != null)
				{
					for (int j = 0; j < array.Length; j++)
					{
						array[j] = j;
						array2[j] = 0;
					}
					num = metadata.Length;
				}
			}
			else
			{
				for (int k = 0; k < array.Length; k++)
				{
					array[k] = 0;
					array2[k] = k;
				}
				num = 1;
			}
			Bindings[] array3 = new Bindings[num];
			for (int l = 0; l < metadata.Length; l++)
			{
				Bindings bindings = array3[array[l]];
				if (bindings == null)
				{
					num = 0;
					int num3 = l;
					while (num3 < metadata.Length && num == array2[num3])
					{
						num++;
						num3++;
					}
					bindings = (array3[array[l]] = new Bindings(this, this._irowset != null, num));
				}
				MetaData metaData = metadata[l];
				int num4 = metaData.type.fixlen;
				short num5 = metaData.type.wType;
				if (-1 != metaData.size)
				{
					if (metaData.type.islong)
					{
						num4 = ADP.PtrSize;
						num5 = (short)((ushort)num5 | 16384);
					}
					else if (-1 == num4)
					{
						if (8192 < metaData.size)
						{
							num4 = ADP.PtrSize;
							num5 = (short)((ushort)num5 | 16384);
						}
						else if (130 == num5 && -1 != metaData.size)
						{
							num4 = metaData.size * 2 + 2;
						}
						else
						{
							num4 = metaData.size;
						}
					}
				}
				else if (num4 < 0)
				{
					num4 = ADP.PtrSize;
					num5 = (short)((ushort)num5 | 16384);
				}
				num2 = array2[l];
				bindings.CurrentIndex = num2;
				bindings.Ordinal = metaData.ordinal;
				bindings.Part = metaData.type.dbPart;
				bindings.Precision = metaData.precision;
				bindings.Scale = metaData.scale;
				bindings.DbType = (int)num5;
				bindings.MaxLen = num4;
				if (Bid.AdvancedOn)
				{
					Bid.Trace("<oledb.struct.tagDBBINDING|INFO|ADV> index=%d, columnName='%ls'\n", l, metaData.columnName);
				}
			}
			int num6 = 0;
			int indexStart = 0;
			for (int m = 0; m < array3.Length; m++)
			{
				indexStart = array3[m].AllocateForAccessor(this, indexStart, m);
				ColumnBinding[] array4 = array3[m].ColumnBindings();
				for (int n = 0; n < array4.Length; n++)
				{
					metadata[num6].columnBinding = array4[n];
					metadata[num6].bindings = array3[m];
					num6++;
				}
			}
			this._bindings = array3;
			return array3;
		}

		// Token: 0x06002579 RID: 9593 RVA: 0x000FF148 File Offset: 0x000FE548
		private void GetRowHandles()
		{
			OleDbHResult oleDbHResult = OleDbHResult.S_OK;
			RowHandleBuffer rowHandleNativeBuffer = this._rowHandleNativeBuffer;
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				rowHandleNativeBuffer.DangerousAddRef(ref flag);
				IntPtr intPtr = rowHandleNativeBuffer.DangerousGetHandle();
				UnsafeNativeMethods.IRowset rowset = this.IRowset();
				try
				{
					Bid.Trace("<oledb.IRowset.GetNextRows|API|OLEDB> %d#, Chapter=%Id, RowsRequested=%Id\n", this.ObjectID, this._chapterHandle.HChapter, this._rowHandleFetchCount);
					oleDbHResult = rowset.GetNextRows(this._chapterHandle.HChapter, IntPtr.Zero, this._rowHandleFetchCount, out this._rowFetchedCount, ref intPtr);
					Bid.Trace("<oledb.IRowset.GetNextRows|API|OLEDB|RET> %08X{HRESULT}, RowsObtained=%Id\n", oleDbHResult, this._rowFetchedCount);
				}
				catch (InvalidCastException innerException)
				{
					throw ODB.ThreadApartmentState(innerException);
				}
			}
			finally
			{
				if (flag)
				{
					rowHandleNativeBuffer.DangerousRelease();
				}
			}
			if (oleDbHResult < OleDbHResult.S_OK)
			{
				this.ProcessResults(oleDbHResult);
			}
			this._isRead = (OleDbHResult.DB_S_ENDOFROWSET != oleDbHResult || 0 < (int)this._rowFetchedCount);
			this._rowFetchedCount = (IntPtr)Math.Max((int)this._rowFetchedCount, 0);
		}

		// Token: 0x0600257A RID: 9594 RVA: 0x000FF268 File Offset: 0x000FE668
		private void GetRowDataFromHandle()
		{
			OleDbHResult oleDbHResult = OleDbHResult.S_OK;
			UnsafeNativeMethods.IRowset rowset = this.IRowset();
			IntPtr rowHandle = this._rowHandleNativeBuffer.GetRowHandle(this._currentRow);
			RowBinding rowBinding = this._bindings[this._nextAccessorForRetrieval].RowBinding();
			IntPtr intPtr = rowBinding.DangerousGetAccessorHandle();
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				rowBinding.DangerousAddRef(ref flag);
				rowBinding.StartDataBlock();
				IntPtr pData = rowBinding.DangerousGetDataPtr();
				Bid.Trace("<oledb.IRowset.GetData|API|OLEDB> %d#, RowHandle=%Id, AccessorHandle=%Id\n", this.ObjectID, rowHandle, intPtr);
				oleDbHResult = rowset.GetData(rowHandle, intPtr, pData);
				Bid.Trace("<oledb.IRowset.GetData|API|OLEDB|RET> %08X{HRESULT}\n", oleDbHResult);
			}
			finally
			{
				if (flag)
				{
					rowBinding.DangerousRelease();
				}
			}
			this._nextAccessorForRetrieval++;
			if (oleDbHResult < OleDbHResult.S_OK)
			{
				this.ProcessResults(oleDbHResult);
			}
		}

		// Token: 0x0600257B RID: 9595 RVA: 0x000FF334 File Offset: 0x000FE734
		private void ReleaseRowHandles()
		{
			UnsafeNativeMethods.IRowset rowset = this.IRowset();
			Bid.Trace("<oledb.IRowset.ReleaseRows|API|OLEDB> %d#, Request=%Id\n", this.ObjectID, this._rowFetchedCount);
			OleDbHResult oleDbHResult = rowset.ReleaseRows(this._rowFetchedCount, this._rowHandleNativeBuffer, ADP.PtrZero, ADP.PtrZero, ADP.PtrZero);
			Bid.Trace("<oledb.IRowset.ReleaseRows|API|OLEDB|RET> %08X{HRESULT}\n", oleDbHResult);
			if (oleDbHResult < OleDbHResult.S_OK)
			{
				SafeNativeMethods.Wrapper.ClearErrorInfo();
			}
			this._rowFetchedCount = IntPtr.Zero;
			this._currentRow = 0;
			this._isRead = false;
		}

		// Token: 0x0600257C RID: 9596 RVA: 0x000FF3B0 File Offset: 0x000FE7B0
		private object GetPropertyValue(int propertyId)
		{
			if (this._irowset != null)
			{
				return this.GetPropertyOnRowset(OleDbPropertySetGuid.Rowset, propertyId);
			}
			if (this._command != null)
			{
				return this._command.GetPropertyValue(OleDbPropertySetGuid.Rowset, propertyId);
			}
			return OleDbPropertyStatus.NotSupported;
		}

		// Token: 0x0600257D RID: 9597 RVA: 0x000FF3F4 File Offset: 0x000FE7F4
		private object GetPropertyOnRowset(Guid propertySet, int propertyID)
		{
			UnsafeNativeMethods.IRowsetInfo properties = this.IRowsetInfo();
			tagDBPROP[] propertySet2;
			using (PropertyIDSet propertyIDSet = new PropertyIDSet(propertySet, propertyID))
			{
				OleDbHResult oleDbHResult;
				using (DBPropSet dbpropSet = new DBPropSet(properties, propertyIDSet, ref oleDbHResult))
				{
					if (oleDbHResult < OleDbHResult.S_OK)
					{
						SafeNativeMethods.Wrapper.ClearErrorInfo();
					}
					propertySet2 = dbpropSet.GetPropertySet(0, out propertySet);
				}
			}
			if (propertySet2[0].dwStatus == OleDbPropertyStatus.Ok)
			{
				return propertySet2[0].vValue;
			}
			return propertySet2[0].dwStatus;
		}

		// Token: 0x0600257E RID: 9598 RVA: 0x000FF49C File Offset: 0x000FE89C
		private void GetRowValue()
		{
			Bindings bindings = this._bindings[this._nextAccessorForRetrieval];
			ColumnBinding[] array = bindings.ColumnBindings();
			RowBinding rowBinding = bindings.RowBinding();
			bool flag = false;
			bool[] array2 = new bool[array.Length];
			StringMemHandle[] array3 = new StringMemHandle[array.Length];
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				for (int i = 0; i < array.Length; i++)
				{
					bindings.CurrentIndex = i;
					array3[i] = null;
					MetaData metaData = this._metadata[array[i].Index];
					if (metaData.kind == 0 || 2 == metaData.kind)
					{
						array3[i] = new StringMemHandle(metaData.idname);
						array[i]._sptr = array3[i];
					}
					array3[i].DangerousAddRef(ref array2[i]);
					IntPtr propid = (array3[i] != null) ? array3[i].DangerousGetHandle() : metaData.propid;
					bindings.GuidKindName(metaData.guid, metaData.kind, propid);
				}
				tagDBCOLUMNACCESS[] dbcolumnAccess = bindings.DBColumnAccess;
				rowBinding.DangerousAddRef(ref flag);
				rowBinding.StartDataBlock();
				UnsafeNativeMethods.IRow row = this.IRow();
				Bid.Trace("<oledb.IRow.GetColumns|API|OLEDB> %d#\n", this.ObjectID);
				OleDbHResult columns = row.GetColumns((IntPtr)dbcolumnAccess.Length, dbcolumnAccess);
				Bid.Trace("<oledb.IRow.GetColumns|API|OLEDB|RET> %08X{HRESULT}\n", columns);
			}
			finally
			{
				if (flag)
				{
					rowBinding.DangerousRelease();
				}
				for (int j = 0; j < array2.Length; j++)
				{
					if (array2[j])
					{
						array3[j].DangerousRelease();
					}
				}
			}
			this._nextAccessorForRetrieval++;
		}

		// Token: 0x0600257F RID: 9599 RVA: 0x000FF628 File Offset: 0x000FEA28
		private int IndexOf(Hashtable hash, string name)
		{
			object obj = hash[name];
			if (obj != null)
			{
				return (int)obj;
			}
			string key = name.ToLower(CultureInfo.InvariantCulture);
			obj = hash[key];
			if (obj == null)
			{
				return -1;
			}
			return (int)obj;
		}

		// Token: 0x06002580 RID: 9600 RVA: 0x000FF668 File Offset: 0x000FEA68
		private void AppendSchemaInfo()
		{
			if (this._metadata.Length == 0)
			{
				return;
			}
			int num = 0;
			for (int i = 0; i < this._metadata.Length; i++)
			{
				if (this._metadata[i].isKeyColumn && !this._metadata[i].isHidden)
				{
					num++;
				}
			}
			if (num != 0)
			{
				return;
			}
			string text = null;
			string text2 = null;
			string text3 = null;
			for (int j = 0; j < this._metadata.Length; j++)
			{
				MetaData metaData = this._metadata[j];
				if (metaData.baseTableName != null && 0 < metaData.baseTableName.Length)
				{
					string text4 = (metaData.baseCatalogName != null) ? metaData.baseCatalogName : "";
					string text5 = (metaData.baseSchemaName != null) ? metaData.baseSchemaName : "";
					if (text3 == null)
					{
						text = text5;
						text2 = text4;
						text3 = metaData.baseTableName;
					}
					else if (ADP.SrcCompare(text3, metaData.baseTableName) != 0 || ADP.SrcCompare(text2, text4) != 0 || ADP.SrcCompare(text, text5) != 0)
					{
						text3 = null;
						break;
					}
				}
			}
			if (text3 == null)
			{
				return;
			}
			text2 = (ADP.IsEmpty(text2) ? null : text2);
			text = (ADP.IsEmpty(text) ? null : text);
			if (this._connection != null && 4 == this._connection.QuotedIdentifierCase())
			{
				string text6 = null;
				string text7 = null;
				this._connection.GetLiteralQuotes("GetSchemaTable", out text7, out text6);
				if (text7 == null)
				{
					text7 = "";
				}
				if (text6 == null)
				{
					text6 = "";
				}
				text3 = text7 + text3 + text6;
			}
			Hashtable hashtable = new Hashtable(this._metadata.Length * 2);
			int num2 = this._metadata.Length - 1;
			while (0 <= num2)
			{
				string baseColumnName = this._metadata[num2].baseColumnName;
				if (!ADP.IsEmpty(baseColumnName))
				{
					hashtable[baseColumnName] = num2;
				}
				num2--;
			}
			for (int k = 0; k < this._metadata.Length; k++)
			{
				string text8 = this._metadata[k].baseColumnName;
				if (!ADP.IsEmpty(text8))
				{
					text8 = text8.ToLower(CultureInfo.InvariantCulture);
					if (!hashtable.Contains(text8))
					{
						hashtable[text8] = k;
					}
				}
			}
			if (this._connection.SupportSchemaRowset(OleDbSchemaGuid.Primary_Keys))
			{
				object[] restrictions = new object[]
				{
					text2,
					text,
					text3
				};
				num = this.AppendSchemaPrimaryKey(hashtable, restrictions);
			}
			if (num != 0)
			{
				return;
			}
			if (this._connection.SupportSchemaRowset(OleDbSchemaGuid.Indexes))
			{
				object[] restrictions2 = new object[]
				{
					text2,
					text,
					null,
					null,
					text3
				};
				this.AppendSchemaUniqueIndexAsKey(hashtable, restrictions2);
			}
		}

		// Token: 0x06002581 RID: 9601 RVA: 0x000FF8F4 File Offset: 0x000FECF4
		private int AppendSchemaPrimaryKey(Hashtable baseColumnNames, object[] restrictions)
		{
			int num = 0;
			bool flag = false;
			DataTable dataTable = null;
			try
			{
				dataTable = this._connection.GetSchemaRowset(OleDbSchemaGuid.Primary_Keys, restrictions);
			}
			catch (Exception e)
			{
				if (!ADP.IsCatchableExceptionType(e))
				{
					throw;
				}
				ADP.TraceExceptionWithoutRethrow(e);
			}
			if (dataTable != null)
			{
				DataColumnCollection columns = dataTable.Columns;
				int num2 = columns.IndexOf("COLUMN_NAME");
				if (-1 != num2)
				{
					DataColumn column = columns[num2];
					foreach (object obj in dataTable.Rows)
					{
						DataRow dataRow = (DataRow)obj;
						string name = (string)dataRow[column, DataRowVersion.Default];
						int num3 = this.IndexOf(baseColumnNames, name);
						if (0 > num3)
						{
							flag = true;
							break;
						}
						MetaData metaData = this._metadata[num3];
						metaData.isKeyColumn = true;
						metaData.flags &= -33;
						num++;
					}
				}
			}
			if (flag)
			{
				for (int i = 0; i < this._metadata.Length; i++)
				{
					this._metadata[i].isKeyColumn = false;
				}
				return -1;
			}
			return num;
		}

		// Token: 0x06002582 RID: 9602 RVA: 0x000FFA4C File Offset: 0x000FEE4C
		private void AppendSchemaUniqueIndexAsKey(Hashtable baseColumnNames, object[] restrictions)
		{
			bool flag = false;
			DataTable dataTable = null;
			try
			{
				dataTable = this._connection.GetSchemaRowset(OleDbSchemaGuid.Indexes, restrictions);
			}
			catch (Exception e)
			{
				if (!ADP.IsCatchableExceptionType(e))
				{
					throw;
				}
				ADP.TraceExceptionWithoutRethrow(e);
			}
			if (dataTable != null)
			{
				DataColumnCollection columns = dataTable.Columns;
				int num = columns.IndexOf("INDEX_NAME");
				int num2 = columns.IndexOf("PRIMARY_KEY");
				int num3 = columns.IndexOf("UNIQUE");
				int num4 = columns.IndexOf("COLUMN_NAME");
				int num5 = columns.IndexOf("NULLS");
				if (-1 != num && -1 != num2 && -1 != num3 && -1 != num4)
				{
					DataColumn column = columns[num];
					DataColumn column2 = columns[num2];
					DataColumn column3 = columns[num3];
					DataColumn column4 = columns[num4];
					DataColumn dataColumn = (-1 != num5) ? columns[num5] : null;
					bool[] array = new bool[this._metadata.Length];
					bool[] array2 = new bool[this._metadata.Length];
					string text = null;
					foreach (object obj in dataTable.Rows)
					{
						DataRow dataRow = (DataRow)obj;
						bool flag2 = !dataRow.IsNull(column2, DataRowVersion.Default) && (bool)dataRow[column2, DataRowVersion.Default];
						bool flag3 = !dataRow.IsNull(column3, DataRowVersion.Default) && (bool)dataRow[column3, DataRowVersion.Default];
						bool flag4 = dataColumn != null && (dataRow.IsNull(dataColumn, DataRowVersion.Default) || Convert.ToInt32(dataRow[dataColumn, DataRowVersion.Default], CultureInfo.InvariantCulture) == 0);
						if (flag2 || flag3)
						{
							string name = (string)dataRow[column4, DataRowVersion.Default];
							int num6 = this.IndexOf(baseColumnNames, name);
							if (0 <= num6)
							{
								if (flag2)
								{
									array[num6] = true;
								}
								if (flag3 && array2 != null)
								{
									array2[num6] = true;
									string text2 = (string)dataRow[column, DataRowVersion.Default];
									if (text == null)
									{
										text = text2;
									}
									else if (text2 != text)
									{
										array2 = null;
									}
								}
							}
							else
							{
								if (flag2)
								{
									flag = true;
									break;
								}
								if (text != null)
								{
									string a = (string)dataRow[column, DataRowVersion.Default];
									if (a != text)
									{
										array2 = null;
									}
								}
							}
						}
					}
					if (flag)
					{
						for (int i = 0; i < this._metadata.Length; i++)
						{
							this._metadata[i].isKeyColumn = false;
						}
						return;
					}
					if (array2 != null)
					{
						for (int j = 0; j < this._metadata.Length; j++)
						{
							this._metadata[j].isKeyColumn = array2[j];
						}
					}
				}
			}
		}

		// Token: 0x06002583 RID: 9603 RVA: 0x000FFD3C File Offset: 0x000FF13C
		private MetaData FindMetaData(string name)
		{
			int num = this._fieldNameLookup.IndexOfName(name);
			if (-1 == num)
			{
				return null;
			}
			return this._metadata[num];
		}

		// Token: 0x06002584 RID: 9604 RVA: 0x000FFD64 File Offset: 0x000FF164
		internal void DumpToSchemaTable(UnsafeNativeMethods.IRowset rowset)
		{
			List<MetaData> list = new List<MetaData>();
			object obj = null;
			using (OleDbDataReader oleDbDataReader = new OleDbDataReader(this._connection, this._command, int.MinValue, CommandBehavior.Default))
			{
				oleDbDataReader.InitializeIRowset(rowset, ChapterHandle.DB_NULL_HCHAPTER, IntPtr.Zero);
				oleDbDataReader.BuildSchemaTableInfo(rowset, true, false);
				obj = this.GetPropertyValue(258);
				if (oleDbDataReader.FieldCount == 0)
				{
					return;
				}
				FieldNameLookup fieldNameLookup = new FieldNameLookup(oleDbDataReader, -1);
				oleDbDataReader._fieldNameLookup = fieldNameLookup;
				MetaData metaData = oleDbDataReader.FindMetaData("DBCOLUMN_IDNAME");
				MetaData metaData2 = oleDbDataReader.FindMetaData("DBCOLUMN_GUID");
				MetaData metaData3 = oleDbDataReader.FindMetaData("DBCOLUMN_PROPID");
				MetaData metaData4 = oleDbDataReader.FindMetaData("DBCOLUMN_NAME");
				MetaData metaData5 = oleDbDataReader.FindMetaData("DBCOLUMN_NUMBER");
				MetaData metaData6 = oleDbDataReader.FindMetaData("DBCOLUMN_TYPE");
				MetaData metaData7 = oleDbDataReader.FindMetaData("DBCOLUMN_COLUMNSIZE");
				MetaData metaData8 = oleDbDataReader.FindMetaData("DBCOLUMN_PRECISION");
				MetaData metaData9 = oleDbDataReader.FindMetaData("DBCOLUMN_SCALE");
				MetaData metaData10 = oleDbDataReader.FindMetaData("DBCOLUMN_FLAGS");
				MetaData metaData11 = oleDbDataReader.FindMetaData("DBCOLUMN_BASESCHEMANAME");
				MetaData metaData12 = oleDbDataReader.FindMetaData("DBCOLUMN_BASECATALOGNAME");
				MetaData metaData13 = oleDbDataReader.FindMetaData("DBCOLUMN_BASETABLENAME");
				MetaData metaData14 = oleDbDataReader.FindMetaData("DBCOLUMN_BASECOLUMNNAME");
				MetaData metaData15 = oleDbDataReader.FindMetaData("DBCOLUMN_ISAUTOINCREMENT");
				MetaData metaData16 = oleDbDataReader.FindMetaData("DBCOLUMN_ISUNIQUE");
				MetaData metaData17 = oleDbDataReader.FindMetaData("DBCOLUMN_KEYCOLUMN");
				oleDbDataReader.CreateAccessors(false);
				while (oleDbDataReader.ReadRowset())
				{
					oleDbDataReader.GetRowDataFromHandle();
					MetaData metaData18 = new MetaData();
					ColumnBinding columnBinding = metaData.columnBinding;
					if (!columnBinding.IsValueNull())
					{
						metaData18.idname = (string)columnBinding.Value();
						metaData18.kind = 2;
					}
					columnBinding = metaData2.columnBinding;
					if (!columnBinding.IsValueNull())
					{
						metaData18.guid = columnBinding.Value_GUID();
						metaData18.kind = ((2 == metaData18.kind) ? 0 : 6);
					}
					columnBinding = metaData3.columnBinding;
					if (!columnBinding.IsValueNull())
					{
						metaData18.propid = new IntPtr((long)((ulong)columnBinding.Value_UI4()));
						metaData18.kind = ((6 == metaData18.kind) ? 1 : 5);
					}
					columnBinding = metaData4.columnBinding;
					if (!columnBinding.IsValueNull())
					{
						metaData18.columnName = (string)columnBinding.Value();
					}
					else
					{
						metaData18.columnName = "";
					}
					if (4 == ADP.PtrSize)
					{
						metaData18.ordinal = (IntPtr)((long)((ulong)metaData5.columnBinding.Value_UI4()));
					}
					else
					{
						metaData18.ordinal = (IntPtr)((long)metaData5.columnBinding.Value_UI8());
					}
					short dbType = (short)metaData6.columnBinding.Value_UI2();
					if (4 == ADP.PtrSize)
					{
						metaData18.size = (int)metaData7.columnBinding.Value_UI4();
					}
					else
					{
						metaData18.size = ADP.IntPtrToInt32((IntPtr)((long)metaData7.columnBinding.Value_UI8()));
					}
					columnBinding = metaData8.columnBinding;
					if (!columnBinding.IsValueNull())
					{
						metaData18.precision = (byte)columnBinding.Value_UI2();
					}
					columnBinding = metaData9.columnBinding;
					if (!columnBinding.IsValueNull())
					{
						metaData18.scale = (byte)columnBinding.Value_I2();
					}
					metaData18.flags = (int)metaData10.columnBinding.Value_UI4();
					bool isLong = OleDbDataReader.IsLong(metaData18.flags);
					bool isFixed = OleDbDataReader.IsFixed(metaData18.flags);
					NativeDBType type = NativeDBType.FromDBType(dbType, isLong, isFixed);
					metaData18.type = type;
					if (metaData15 != null)
					{
						columnBinding = metaData15.columnBinding;
						if (!columnBinding.IsValueNull())
						{
							metaData18.isAutoIncrement = columnBinding.Value_BOOL();
						}
					}
					if (metaData16 != null)
					{
						columnBinding = metaData16.columnBinding;
						if (!columnBinding.IsValueNull())
						{
							metaData18.isUnique = columnBinding.Value_BOOL();
						}
					}
					if (metaData17 != null)
					{
						columnBinding = metaData17.columnBinding;
						if (!columnBinding.IsValueNull())
						{
							metaData18.isKeyColumn = columnBinding.Value_BOOL();
						}
					}
					if (metaData11 != null)
					{
						columnBinding = metaData11.columnBinding;
						if (!columnBinding.IsValueNull())
						{
							metaData18.baseSchemaName = columnBinding.ValueString();
						}
					}
					if (metaData12 != null)
					{
						columnBinding = metaData12.columnBinding;
						if (!columnBinding.IsValueNull())
						{
							metaData18.baseCatalogName = columnBinding.ValueString();
						}
					}
					if (metaData13 != null)
					{
						columnBinding = metaData13.columnBinding;
						if (!columnBinding.IsValueNull())
						{
							metaData18.baseTableName = columnBinding.ValueString();
						}
					}
					if (metaData14 != null)
					{
						columnBinding = metaData14.columnBinding;
						if (!columnBinding.IsValueNull())
						{
							metaData18.baseColumnName = columnBinding.ValueString();
						}
					}
					list.Add(metaData18);
				}
			}
			int i = list.Count;
			if (obj is int)
			{
				i -= (int)obj;
			}
			bool flag = false;
			int j = list.Count - 1;
			while (i <= j)
			{
				MetaData metaData19 = list[j];
				metaData19.isHidden = true;
				if (flag)
				{
					metaData19.isKeyColumn = false;
				}
				else if (metaData19.guid.Equals(ODB.DBCOL_SPECIALCOL))
				{
					metaData19.isKeyColumn = false;
					flag = true;
					int num = list.Count - 1;
					while (j < num)
					{
						list[num].isKeyColumn = false;
						num--;
					}
				}
				j--;
			}
			int num2 = i - 1;
			while (0 <= num2)
			{
				MetaData metaData20 = list[num2];
				if (flag)
				{
					metaData20.isKeyColumn = false;
				}
				if (metaData20.guid.Equals(ODB.DBCOL_SPECIALCOL))
				{
					metaData20.isHidden = true;
					i--;
				}
				else if (0L >= (long)metaData20.ordinal)
				{
					metaData20.isHidden = true;
					i--;
				}
				else if (OleDbDataReader.DoColumnDropFilter(metaData20.flags))
				{
					metaData20.isHidden = true;
					i--;
				}
				num2--;
			}
			list.Sort();
			this._visibleFieldCount = i;
			this._metadata = list.ToArray();
		}

		// Token: 0x06002585 RID: 9605 RVA: 0x001002EC File Offset: 0x000FF6EC
		internal static void GenerateSchemaTable(OleDbDataReader dataReader, object handle, CommandBehavior behavior)
		{
			if ((CommandBehavior.KeyInfo & behavior) != CommandBehavior.Default)
			{
				dataReader.BuildSchemaTableRowset(handle);
				dataReader.AppendSchemaInfo();
			}
			else
			{
				dataReader.BuildSchemaTableInfo(handle, false, false);
			}
			MetaData[] metaData = dataReader.MetaData;
			if (metaData != null && metaData.Length != 0)
			{
				dataReader.BuildSchemaTable(metaData);
			}
		}

		// Token: 0x06002586 RID: 9606 RVA: 0x0010032C File Offset: 0x000FF72C
		private static bool DoColumnDropFilter(int flags)
		{
			return (1 & flags) != 0;
		}

		// Token: 0x06002587 RID: 9607 RVA: 0x00100340 File Offset: 0x000FF740
		private static bool IsLong(int flags)
		{
			return (128 & flags) != 0;
		}

		// Token: 0x06002588 RID: 9608 RVA: 0x00100358 File Offset: 0x000FF758
		private static bool IsFixed(int flags)
		{
			return (16 & flags) != 0;
		}

		// Token: 0x06002589 RID: 9609 RVA: 0x0010036C File Offset: 0x000FF76C
		private static bool IsRowVersion(int flags)
		{
			return (768 & flags) != 0;
		}

		// Token: 0x0600258A RID: 9610 RVA: 0x00100384 File Offset: 0x000FF784
		private static bool AllowDBNull(int flags)
		{
			return (32 & flags) != 0;
		}

		// Token: 0x0600258B RID: 9611 RVA: 0x00100398 File Offset: 0x000FF798
		private static bool AllowDBNullMaybeNull(int flags)
		{
			return (96 & flags) != 0;
		}

		// Token: 0x0600258C RID: 9612 RVA: 0x001003AC File Offset: 0x000FF7AC
		private static bool IsReadOnly(int flags)
		{
			return (12 & flags) == 0;
		}

		// Token: 0x040015D1 RID: 5585
		private CommandBehavior _commandBehavior;

		// Token: 0x040015D2 RID: 5586
		private static int _objectTypeCount;

		// Token: 0x040015D3 RID: 5587
		internal readonly int ObjectID = Interlocked.Increment(ref OleDbDataReader._objectTypeCount);

		// Token: 0x040015D4 RID: 5588
		private OleDbConnection _connection;

		// Token: 0x040015D5 RID: 5589
		private OleDbCommand _command;

		// Token: 0x040015D6 RID: 5590
		private Bindings _parameterBindings;

		// Token: 0x040015D7 RID: 5591
		private UnsafeNativeMethods.IMultipleResults _imultipleResults;

		// Token: 0x040015D8 RID: 5592
		private UnsafeNativeMethods.IRowset _irowset;

		// Token: 0x040015D9 RID: 5593
		private UnsafeNativeMethods.IRow _irow;

		// Token: 0x040015DA RID: 5594
		private ChapterHandle _chapterHandle = ChapterHandle.DB_NULL_HCHAPTER;

		// Token: 0x040015DB RID: 5595
		private int _depth;

		// Token: 0x040015DC RID: 5596
		private bool _isClosed;

		// Token: 0x040015DD RID: 5597
		private bool _isRead;

		// Token: 0x040015DE RID: 5598
		private bool _hasRows;

		// Token: 0x040015DF RID: 5599
		private bool _hasRowsReadCheck;

		// Token: 0x040015E0 RID: 5600
		private long _sequentialBytesRead;

		// Token: 0x040015E1 RID: 5601
		private int _sequentialOrdinal;

		// Token: 0x040015E2 RID: 5602
		private Bindings[] _bindings;

		// Token: 0x040015E3 RID: 5603
		private int _nextAccessorForRetrieval;

		// Token: 0x040015E4 RID: 5604
		private int _nextValueForRetrieval;

		// Token: 0x040015E5 RID: 5605
		private IntPtr _recordsAffected = ADP.RecordsUnaffected;

		// Token: 0x040015E6 RID: 5606
		private bool _useIColumnsRowset;

		// Token: 0x040015E7 RID: 5607
		private bool _sequentialAccess;

		// Token: 0x040015E8 RID: 5608
		private bool _singleRow;

		// Token: 0x040015E9 RID: 5609
		private IntPtr _rowHandleFetchCount;

		// Token: 0x040015EA RID: 5610
		private RowHandleBuffer _rowHandleNativeBuffer;

		// Token: 0x040015EB RID: 5611
		private IntPtr _rowFetchedCount;

		// Token: 0x040015EC RID: 5612
		private int _currentRow;

		// Token: 0x040015ED RID: 5613
		private DataTable _dbSchemaTable;

		// Token: 0x040015EE RID: 5614
		private int _visibleFieldCount;

		// Token: 0x040015EF RID: 5615
		private MetaData[] _metadata;

		// Token: 0x040015F0 RID: 5616
		private FieldNameLookup _fieldNameLookup;
	}
}
