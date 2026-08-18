using System;
using System.Collections;
using System.Data.Common;
using System.Globalization;

namespace System.Data
{
	// Token: 0x020000D5 RID: 213
	public sealed class DataTableReader : DbDataReader
	{
		// Token: 0x06000DDC RID: 3548 RVA: 0x00073C98 File Offset: 0x00073098
		public DataTableReader(DataTable dataTable)
		{
			if (dataTable == null)
			{
				throw ExceptionBuilder.ArgumentNull("DataTable");
			}
			this.tables = new DataTable[]
			{
				dataTable
			};
			this.init();
		}

		// Token: 0x06000DDD RID: 3549 RVA: 0x00073CEC File Offset: 0x000730EC
		public DataTableReader(DataTable[] dataTables)
		{
			if (dataTables == null)
			{
				throw ExceptionBuilder.ArgumentNull("DataTable");
			}
			if (dataTables.Length == 0)
			{
				throw ExceptionBuilder.DataTableReaderArgumentIsEmpty();
			}
			this.tables = new DataTable[dataTables.Length];
			for (int i = 0; i < dataTables.Length; i++)
			{
				if (dataTables[i] == null)
				{
					throw ExceptionBuilder.ArgumentNull("DataTable");
				}
				this.tables[i] = dataTables[i];
			}
			this.init();
		}

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x06000DDE RID: 3550 RVA: 0x00073D70 File Offset: 0x00073170
		// (set) Token: 0x06000DDF RID: 3551 RVA: 0x00073D84 File Offset: 0x00073184
		private bool ReaderIsInvalid
		{
			get
			{
				return this.readerIsInvalid;
			}
			set
			{
				if (this.readerIsInvalid == value)
				{
					return;
				}
				this.readerIsInvalid = value;
				if (this.readerIsInvalid && this.listener != null)
				{
					this.listener.CleanUp();
				}
			}
		}

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x06000DE0 RID: 3552 RVA: 0x00073DC0 File Offset: 0x000731C0
		// (set) Token: 0x06000DE1 RID: 3553 RVA: 0x00073DD4 File Offset: 0x000731D4
		private bool IsSchemaChanged
		{
			get
			{
				return this.schemaIsChanged;
			}
			set
			{
				if (!value || this.schemaIsChanged == value)
				{
					return;
				}
				this.schemaIsChanged = value;
				if (this.listener != null)
				{
					this.listener.CleanUp();
				}
			}
		}

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x06000DE2 RID: 3554 RVA: 0x00073E08 File Offset: 0x00073208
		internal DataTable CurrentDataTable
		{
			get
			{
				return this.currentDataTable;
			}
		}

		// Token: 0x06000DE3 RID: 3555 RVA: 0x00073E1C File Offset: 0x0007321C
		private void init()
		{
			this.tableCounter = 0;
			this.reachEORows = false;
			this.schemaIsChanged = false;
			this.currentDataTable = this.tables[this.tableCounter];
			this.hasRows = (this.currentDataTable.Rows.Count > 0);
			this.ReaderIsInvalid = false;
			this.listener = new DataTableReaderListener(this);
		}

		// Token: 0x06000DE4 RID: 3556 RVA: 0x00073E80 File Offset: 0x00073280
		public override void Close()
		{
			if (!this.isOpen)
			{
				return;
			}
			if (this.listener != null)
			{
				this.listener.CleanUp();
			}
			this.listener = null;
			this.schemaTable = null;
			this.isOpen = false;
		}

		// Token: 0x06000DE5 RID: 3557 RVA: 0x00073EC0 File Offset: 0x000732C0
		public override DataTable GetSchemaTable()
		{
			this.ValidateOpen("GetSchemaTable");
			this.ValidateReader();
			if (this.schemaTable == null)
			{
				this.schemaTable = DataTableReader.GetSchemaTableFromDataTable(this.currentDataTable);
			}
			return this.schemaTable;
		}

		// Token: 0x06000DE6 RID: 3558 RVA: 0x00073F00 File Offset: 0x00073300
		public override bool NextResult()
		{
			this.ValidateOpen("NextResult");
			if (this.tableCounter == this.tables.Length - 1)
			{
				return false;
			}
			DataTable[] array = this.tables;
			int num = this.tableCounter + 1;
			this.tableCounter = num;
			this.currentDataTable = array[num];
			if (this.listener != null)
			{
				this.listener.UpdataTable(this.currentDataTable);
			}
			this.schemaTable = null;
			this.rowCounter = -1;
			this.currentRowRemoved = false;
			this.reachEORows = false;
			this.schemaIsChanged = false;
			this.started = false;
			this.ReaderIsInvalid = false;
			this.tableCleared = false;
			this.hasRows = (this.currentDataTable.Rows.Count > 0);
			return true;
		}

		// Token: 0x06000DE7 RID: 3559 RVA: 0x00073FB8 File Offset: 0x000733B8
		public override bool Read()
		{
			if (!this.started)
			{
				this.started = true;
			}
			this.ValidateOpen("Read");
			this.ValidateReader();
			if (this.reachEORows)
			{
				return false;
			}
			if (this.rowCounter >= this.currentDataTable.Rows.Count - 1)
			{
				this.reachEORows = true;
				if (this.listener != null)
				{
					this.listener.CleanUp();
				}
				return false;
			}
			this.rowCounter++;
			this.ValidateRow(this.rowCounter);
			this.currentDataRow = this.currentDataTable.Rows[this.rowCounter];
			while (this.currentDataRow.RowState == DataRowState.Deleted)
			{
				this.rowCounter++;
				if (this.rowCounter == this.currentDataTable.Rows.Count)
				{
					this.reachEORows = true;
					if (this.listener != null)
					{
						this.listener.CleanUp();
					}
					return false;
				}
				this.ValidateRow(this.rowCounter);
				this.currentDataRow = this.currentDataTable.Rows[this.rowCounter];
			}
			if (this.currentRowRemoved)
			{
				this.currentRowRemoved = false;
			}
			return true;
		}

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x06000DE8 RID: 3560 RVA: 0x000740E8 File Offset: 0x000734E8
		public override int Depth
		{
			get
			{
				this.ValidateOpen("Depth");
				this.ValidateReader();
				return 0;
			}
		}

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x06000DE9 RID: 3561 RVA: 0x00074108 File Offset: 0x00073508
		public override bool IsClosed
		{
			get
			{
				return !this.isOpen;
			}
		}

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x06000DEA RID: 3562 RVA: 0x00074120 File Offset: 0x00073520
		public override int RecordsAffected
		{
			get
			{
				this.ValidateReader();
				return 0;
			}
		}

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x06000DEB RID: 3563 RVA: 0x00074134 File Offset: 0x00073534
		public override bool HasRows
		{
			get
			{
				this.ValidateOpen("HasRows");
				this.ValidateReader();
				return this.hasRows;
			}
		}

		// Token: 0x17000200 RID: 512
		public override object this[int ordinal]
		{
			get
			{
				this.ValidateOpen("Item");
				this.ValidateReader();
				if (this.currentDataRow == null || this.currentDataRow.RowState == DataRowState.Deleted)
				{
					this.ReaderIsInvalid = true;
					throw ExceptionBuilder.InvalidDataTableReader(this.currentDataTable.TableName);
				}
				object result;
				try
				{
					result = this.currentDataRow[ordinal];
				}
				catch (IndexOutOfRangeException e)
				{
					ExceptionBuilder.TraceExceptionWithoutRethrow(e);
					throw ExceptionBuilder.ArgumentOutOfRange("ordinal");
				}
				return result;
			}
		}

		// Token: 0x17000201 RID: 513
		public override object this[string name]
		{
			get
			{
				this.ValidateOpen("Item");
				this.ValidateReader();
				if (this.currentDataRow == null || this.currentDataRow.RowState == DataRowState.Deleted)
				{
					this.ReaderIsInvalid = true;
					throw ExceptionBuilder.InvalidDataTableReader(this.currentDataTable.TableName);
				}
				return this.currentDataRow[name];
			}
		}

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x06000DEE RID: 3566 RVA: 0x00074240 File Offset: 0x00073640
		public override int FieldCount
		{
			get
			{
				this.ValidateOpen("FieldCount");
				this.ValidateReader();
				return this.currentDataTable.Columns.Count;
			}
		}

		// Token: 0x06000DEF RID: 3567 RVA: 0x00074270 File Offset: 0x00073670
		public override Type GetProviderSpecificFieldType(int ordinal)
		{
			this.ValidateOpen("GetProviderSpecificFieldType");
			this.ValidateReader();
			return this.GetFieldType(ordinal);
		}

		// Token: 0x06000DF0 RID: 3568 RVA: 0x00074298 File Offset: 0x00073698
		public override object GetProviderSpecificValue(int ordinal)
		{
			this.ValidateOpen("GetProviderSpecificValue");
			this.ValidateReader();
			return this.GetValue(ordinal);
		}

		// Token: 0x06000DF1 RID: 3569 RVA: 0x000742C0 File Offset: 0x000736C0
		public override int GetProviderSpecificValues(object[] values)
		{
			this.ValidateOpen("GetProviderSpecificValues");
			this.ValidateReader();
			return this.GetValues(values);
		}

		// Token: 0x06000DF2 RID: 3570 RVA: 0x000742E8 File Offset: 0x000736E8
		public override bool GetBoolean(int ordinal)
		{
			this.ValidateState("GetBoolean");
			this.ValidateReader();
			bool result;
			try
			{
				result = (bool)this.currentDataRow[ordinal];
			}
			catch (IndexOutOfRangeException e)
			{
				ExceptionBuilder.TraceExceptionWithoutRethrow(e);
				throw ExceptionBuilder.ArgumentOutOfRange("ordinal");
			}
			return result;
		}

		// Token: 0x06000DF3 RID: 3571 RVA: 0x0007434C File Offset: 0x0007374C
		public override byte GetByte(int ordinal)
		{
			this.ValidateState("GetByte");
			this.ValidateReader();
			byte result;
			try
			{
				result = (byte)this.currentDataRow[ordinal];
			}
			catch (IndexOutOfRangeException e)
			{
				ExceptionBuilder.TraceExceptionWithoutRethrow(e);
				throw ExceptionBuilder.ArgumentOutOfRange("ordinal");
			}
			return result;
		}

		// Token: 0x06000DF4 RID: 3572 RVA: 0x000743B0 File Offset: 0x000737B0
		public override long GetBytes(int ordinal, long dataIndex, byte[] buffer, int bufferIndex, int length)
		{
			this.ValidateState("GetBytes");
			this.ValidateReader();
			byte[] array;
			try
			{
				array = (byte[])this.currentDataRow[ordinal];
			}
			catch (IndexOutOfRangeException e)
			{
				ExceptionBuilder.TraceExceptionWithoutRethrow(e);
				throw ExceptionBuilder.ArgumentOutOfRange("ordinal");
			}
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
			if (bufferIndex < 0 || (bufferIndex > 0 && bufferIndex >= buffer.Length))
			{
				throw ADP.InvalidDestinationBufferIndex(buffer.Length, bufferIndex, "bufferIndex");
			}
			if (0 < num2)
			{
				Array.Copy(array, dataIndex, buffer, (long)bufferIndex, (long)num2);
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

		// Token: 0x06000DF5 RID: 3573 RVA: 0x00074484 File Offset: 0x00073884
		public override char GetChar(int ordinal)
		{
			this.ValidateState("GetChar");
			this.ValidateReader();
			char result;
			try
			{
				result = (char)this.currentDataRow[ordinal];
			}
			catch (IndexOutOfRangeException e)
			{
				ExceptionBuilder.TraceExceptionWithoutRethrow(e);
				throw ExceptionBuilder.ArgumentOutOfRange("ordinal");
			}
			return result;
		}

		// Token: 0x06000DF6 RID: 3574 RVA: 0x000744E8 File Offset: 0x000738E8
		public override long GetChars(int ordinal, long dataIndex, char[] buffer, int bufferIndex, int length)
		{
			this.ValidateState("GetChars");
			this.ValidateReader();
			char[] array;
			try
			{
				array = (char[])this.currentDataRow[ordinal];
			}
			catch (IndexOutOfRangeException e)
			{
				ExceptionBuilder.TraceExceptionWithoutRethrow(e);
				throw ExceptionBuilder.ArgumentOutOfRange("ordinal");
			}
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
			if (bufferIndex < 0 || (bufferIndex > 0 && bufferIndex >= buffer.Length))
			{
				throw ADP.InvalidDestinationBufferIndex(buffer.Length, bufferIndex, "bufferIndex");
			}
			if (0 < num2)
			{
				Array.Copy(array, dataIndex, buffer, (long)bufferIndex, (long)num2);
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

		// Token: 0x06000DF7 RID: 3575 RVA: 0x000745BC File Offset: 0x000739BC
		public override string GetDataTypeName(int ordinal)
		{
			this.ValidateOpen("GetDataTypeName");
			this.ValidateReader();
			return this.GetFieldType(ordinal).Name;
		}

		// Token: 0x06000DF8 RID: 3576 RVA: 0x000745E8 File Offset: 0x000739E8
		public override DateTime GetDateTime(int ordinal)
		{
			this.ValidateState("GetDateTime");
			this.ValidateReader();
			DateTime result;
			try
			{
				result = (DateTime)this.currentDataRow[ordinal];
			}
			catch (IndexOutOfRangeException e)
			{
				ExceptionBuilder.TraceExceptionWithoutRethrow(e);
				throw ExceptionBuilder.ArgumentOutOfRange("ordinal");
			}
			return result;
		}

		// Token: 0x06000DF9 RID: 3577 RVA: 0x0007464C File Offset: 0x00073A4C
		public override decimal GetDecimal(int ordinal)
		{
			this.ValidateState("GetDecimal");
			this.ValidateReader();
			decimal result;
			try
			{
				result = (decimal)this.currentDataRow[ordinal];
			}
			catch (IndexOutOfRangeException e)
			{
				ExceptionBuilder.TraceExceptionWithoutRethrow(e);
				throw ExceptionBuilder.ArgumentOutOfRange("ordinal");
			}
			return result;
		}

		// Token: 0x06000DFA RID: 3578 RVA: 0x000746B0 File Offset: 0x00073AB0
		public override double GetDouble(int ordinal)
		{
			this.ValidateState("GetDouble");
			this.ValidateReader();
			double result;
			try
			{
				result = (double)this.currentDataRow[ordinal];
			}
			catch (IndexOutOfRangeException e)
			{
				ExceptionBuilder.TraceExceptionWithoutRethrow(e);
				throw ExceptionBuilder.ArgumentOutOfRange("ordinal");
			}
			return result;
		}

		// Token: 0x06000DFB RID: 3579 RVA: 0x00074714 File Offset: 0x00073B14
		public override Type GetFieldType(int ordinal)
		{
			this.ValidateOpen("GetFieldType");
			this.ValidateReader();
			Type dataType;
			try
			{
				dataType = this.currentDataTable.Columns[ordinal].DataType;
			}
			catch (IndexOutOfRangeException e)
			{
				ExceptionBuilder.TraceExceptionWithoutRethrow(e);
				throw ExceptionBuilder.ArgumentOutOfRange("ordinal");
			}
			return dataType;
		}

		// Token: 0x06000DFC RID: 3580 RVA: 0x0007477C File Offset: 0x00073B7C
		public override float GetFloat(int ordinal)
		{
			this.ValidateState("GetFloat");
			this.ValidateReader();
			float result;
			try
			{
				result = (float)this.currentDataRow[ordinal];
			}
			catch (IndexOutOfRangeException e)
			{
				ExceptionBuilder.TraceExceptionWithoutRethrow(e);
				throw ExceptionBuilder.ArgumentOutOfRange("ordinal");
			}
			return result;
		}

		// Token: 0x06000DFD RID: 3581 RVA: 0x000747E0 File Offset: 0x00073BE0
		public override Guid GetGuid(int ordinal)
		{
			this.ValidateState("GetGuid");
			this.ValidateReader();
			Guid result;
			try
			{
				result = (Guid)this.currentDataRow[ordinal];
			}
			catch (IndexOutOfRangeException e)
			{
				ExceptionBuilder.TraceExceptionWithoutRethrow(e);
				throw ExceptionBuilder.ArgumentOutOfRange("ordinal");
			}
			return result;
		}

		// Token: 0x06000DFE RID: 3582 RVA: 0x00074844 File Offset: 0x00073C44
		public override short GetInt16(int ordinal)
		{
			this.ValidateState("GetInt16");
			this.ValidateReader();
			short result;
			try
			{
				result = (short)this.currentDataRow[ordinal];
			}
			catch (IndexOutOfRangeException e)
			{
				ExceptionBuilder.TraceExceptionWithoutRethrow(e);
				throw ExceptionBuilder.ArgumentOutOfRange("ordinal");
			}
			return result;
		}

		// Token: 0x06000DFF RID: 3583 RVA: 0x000748A8 File Offset: 0x00073CA8
		public override int GetInt32(int ordinal)
		{
			this.ValidateState("GetInt32");
			this.ValidateReader();
			int result;
			try
			{
				result = (int)this.currentDataRow[ordinal];
			}
			catch (IndexOutOfRangeException e)
			{
				ExceptionBuilder.TraceExceptionWithoutRethrow(e);
				throw ExceptionBuilder.ArgumentOutOfRange("ordinal");
			}
			return result;
		}

		// Token: 0x06000E00 RID: 3584 RVA: 0x0007490C File Offset: 0x00073D0C
		public override long GetInt64(int ordinal)
		{
			this.ValidateState("GetInt64");
			this.ValidateReader();
			long result;
			try
			{
				result = (long)this.currentDataRow[ordinal];
			}
			catch (IndexOutOfRangeException e)
			{
				ExceptionBuilder.TraceExceptionWithoutRethrow(e);
				throw ExceptionBuilder.ArgumentOutOfRange("ordinal");
			}
			return result;
		}

		// Token: 0x06000E01 RID: 3585 RVA: 0x00074970 File Offset: 0x00073D70
		public override string GetName(int ordinal)
		{
			this.ValidateOpen("GetName");
			this.ValidateReader();
			string columnName;
			try
			{
				columnName = this.currentDataTable.Columns[ordinal].ColumnName;
			}
			catch (IndexOutOfRangeException e)
			{
				ExceptionBuilder.TraceExceptionWithoutRethrow(e);
				throw ExceptionBuilder.ArgumentOutOfRange("ordinal");
			}
			return columnName;
		}

		// Token: 0x06000E02 RID: 3586 RVA: 0x000749D8 File Offset: 0x00073DD8
		public override int GetOrdinal(string name)
		{
			this.ValidateOpen("GetOrdinal");
			this.ValidateReader();
			DataColumn dataColumn = this.currentDataTable.Columns[name];
			if (dataColumn != null)
			{
				return dataColumn.Ordinal;
			}
			throw ExceptionBuilder.ColumnNotInTheTable(name, this.currentDataTable.TableName);
		}

		// Token: 0x06000E03 RID: 3587 RVA: 0x00074A24 File Offset: 0x00073E24
		public override string GetString(int ordinal)
		{
			this.ValidateState("GetString");
			this.ValidateReader();
			string result;
			try
			{
				result = (string)this.currentDataRow[ordinal];
			}
			catch (IndexOutOfRangeException e)
			{
				ExceptionBuilder.TraceExceptionWithoutRethrow(e);
				throw ExceptionBuilder.ArgumentOutOfRange("ordinal");
			}
			return result;
		}

		// Token: 0x06000E04 RID: 3588 RVA: 0x00074A88 File Offset: 0x00073E88
		public override object GetValue(int ordinal)
		{
			this.ValidateState("GetValue");
			this.ValidateReader();
			object result;
			try
			{
				result = this.currentDataRow[ordinal];
			}
			catch (IndexOutOfRangeException e)
			{
				ExceptionBuilder.TraceExceptionWithoutRethrow(e);
				throw ExceptionBuilder.ArgumentOutOfRange("ordinal");
			}
			return result;
		}

		// Token: 0x06000E05 RID: 3589 RVA: 0x00074AE8 File Offset: 0x00073EE8
		public override int GetValues(object[] values)
		{
			this.ValidateState("GetValues");
			this.ValidateReader();
			if (values == null)
			{
				throw ExceptionBuilder.ArgumentNull("values");
			}
			Array.Copy(this.currentDataRow.ItemArray, values, (this.currentDataRow.ItemArray.Length > values.Length) ? values.Length : this.currentDataRow.ItemArray.Length);
			if (this.currentDataRow.ItemArray.Length <= values.Length)
			{
				return this.currentDataRow.ItemArray.Length;
			}
			return values.Length;
		}

		// Token: 0x06000E06 RID: 3590 RVA: 0x00074B6C File Offset: 0x00073F6C
		public override bool IsDBNull(int ordinal)
		{
			this.ValidateState("IsDBNull");
			this.ValidateReader();
			bool result;
			try
			{
				result = this.currentDataRow.IsNull(ordinal);
			}
			catch (IndexOutOfRangeException e)
			{
				ExceptionBuilder.TraceExceptionWithoutRethrow(e);
				throw ExceptionBuilder.ArgumentOutOfRange("ordinal");
			}
			return result;
		}

		// Token: 0x06000E07 RID: 3591 RVA: 0x00074BCC File Offset: 0x00073FCC
		public override IEnumerator GetEnumerator()
		{
			this.ValidateOpen("GetEnumerator");
			return new DbEnumerator(this);
		}

		// Token: 0x06000E08 RID: 3592 RVA: 0x00074BEC File Offset: 0x00073FEC
		internal static DataTable GetSchemaTableFromDataTable(DataTable table)
		{
			if (table == null)
			{
				throw ExceptionBuilder.ArgumentNull("DataTable");
			}
			DataTable dataTable = new DataTable("SchemaTable");
			dataTable.Locale = CultureInfo.InvariantCulture;
			DataColumn column = new DataColumn(SchemaTableColumn.ColumnName, typeof(string));
			DataColumn column2 = new DataColumn(SchemaTableColumn.ColumnOrdinal, typeof(int));
			DataColumn dataColumn = new DataColumn(SchemaTableColumn.ColumnSize, typeof(int));
			DataColumn column3 = new DataColumn(SchemaTableColumn.NumericPrecision, typeof(short));
			DataColumn column4 = new DataColumn(SchemaTableColumn.NumericScale, typeof(short));
			DataColumn column5 = new DataColumn(SchemaTableColumn.DataType, typeof(Type));
			DataColumn column6 = new DataColumn(SchemaTableColumn.ProviderType, typeof(int));
			DataColumn dataColumn2 = new DataColumn(SchemaTableColumn.IsLong, typeof(bool));
			DataColumn column7 = new DataColumn(SchemaTableColumn.AllowDBNull, typeof(bool));
			DataColumn dataColumn3 = new DataColumn(SchemaTableOptionalColumn.IsReadOnly, typeof(bool));
			DataColumn dataColumn4 = new DataColumn(SchemaTableOptionalColumn.IsRowVersion, typeof(bool));
			DataColumn column8 = new DataColumn(SchemaTableColumn.IsUnique, typeof(bool));
			DataColumn dataColumn5 = new DataColumn(SchemaTableColumn.IsKey, typeof(bool));
			DataColumn dataColumn6 = new DataColumn(SchemaTableOptionalColumn.IsAutoIncrement, typeof(bool));
			DataColumn column9 = new DataColumn(SchemaTableColumn.BaseSchemaName, typeof(string));
			DataColumn dataColumn7 = new DataColumn(SchemaTableOptionalColumn.BaseCatalogName, typeof(string));
			DataColumn dataColumn8 = new DataColumn(SchemaTableColumn.BaseTableName, typeof(string));
			DataColumn column10 = new DataColumn(SchemaTableColumn.BaseColumnName, typeof(string));
			DataColumn dataColumn9 = new DataColumn(SchemaTableOptionalColumn.AutoIncrementSeed, typeof(long));
			DataColumn dataColumn10 = new DataColumn(SchemaTableOptionalColumn.AutoIncrementStep, typeof(long));
			DataColumn column11 = new DataColumn(SchemaTableOptionalColumn.DefaultValue, typeof(object));
			DataColumn column12 = new DataColumn(SchemaTableOptionalColumn.Expression, typeof(string));
			DataColumn column13 = new DataColumn(SchemaTableOptionalColumn.ColumnMapping, typeof(MappingType));
			DataColumn dataColumn11 = new DataColumn(SchemaTableOptionalColumn.BaseTableNamespace, typeof(string));
			DataColumn column14 = new DataColumn(SchemaTableOptionalColumn.BaseColumnNamespace, typeof(string));
			dataColumn.DefaultValue = -1;
			if (table.DataSet != null)
			{
				dataColumn7.DefaultValue = table.DataSet.DataSetName;
			}
			dataColumn8.DefaultValue = table.TableName;
			dataColumn11.DefaultValue = table.Namespace;
			dataColumn4.DefaultValue = false;
			dataColumn2.DefaultValue = false;
			dataColumn3.DefaultValue = false;
			dataColumn5.DefaultValue = false;
			dataColumn6.DefaultValue = false;
			dataColumn9.DefaultValue = 0;
			dataColumn10.DefaultValue = 1;
			dataTable.Columns.Add(column);
			dataTable.Columns.Add(column2);
			dataTable.Columns.Add(dataColumn);
			dataTable.Columns.Add(column3);
			dataTable.Columns.Add(column4);
			dataTable.Columns.Add(column5);
			dataTable.Columns.Add(column6);
			dataTable.Columns.Add(dataColumn2);
			dataTable.Columns.Add(column7);
			dataTable.Columns.Add(dataColumn3);
			dataTable.Columns.Add(dataColumn4);
			dataTable.Columns.Add(column8);
			dataTable.Columns.Add(dataColumn5);
			dataTable.Columns.Add(dataColumn6);
			dataTable.Columns.Add(dataColumn7);
			dataTable.Columns.Add(column9);
			dataTable.Columns.Add(dataColumn8);
			dataTable.Columns.Add(column10);
			dataTable.Columns.Add(dataColumn9);
			dataTable.Columns.Add(dataColumn10);
			dataTable.Columns.Add(column11);
			dataTable.Columns.Add(column12);
			dataTable.Columns.Add(column13);
			dataTable.Columns.Add(dataColumn11);
			dataTable.Columns.Add(column14);
			foreach (object obj in table.Columns)
			{
				DataColumn dataColumn12 = (DataColumn)obj;
				DataRow dataRow = dataTable.NewRow();
				dataRow[column] = dataColumn12.ColumnName;
				dataRow[column2] = dataColumn12.Ordinal;
				dataRow[column5] = dataColumn12.DataType;
				if (dataColumn12.DataType == typeof(string))
				{
					dataRow[dataColumn] = dataColumn12.MaxLength;
				}
				dataRow[column7] = dataColumn12.AllowDBNull;
				dataRow[dataColumn3] = dataColumn12.ReadOnly;
				dataRow[column8] = dataColumn12.Unique;
				if (dataColumn12.AutoIncrement)
				{
					dataRow[dataColumn6] = true;
					dataRow[dataColumn9] = dataColumn12.AutoIncrementSeed;
					dataRow[dataColumn10] = dataColumn12.AutoIncrementStep;
				}
				if (dataColumn12.DefaultValue != DBNull.Value)
				{
					dataRow[column11] = dataColumn12.DefaultValue;
				}
				if (dataColumn12.Expression.Length != 0)
				{
					bool flag = false;
					DataColumn[] dependency = dataColumn12.DataExpression.GetDependency();
					for (int i = 0; i < dependency.Length; i++)
					{
						if (dependency[i].Table != table)
						{
							flag = true;
							break;
						}
					}
					if (!flag)
					{
						dataRow[column12] = dataColumn12.Expression;
					}
				}
				dataRow[column13] = dataColumn12.ColumnMapping;
				dataRow[column10] = dataColumn12.ColumnName;
				dataRow[column14] = dataColumn12.Namespace;
				dataTable.Rows.Add(dataRow);
			}
			foreach (DataColumn dataColumn13 in table.PrimaryKey)
			{
				dataTable.Rows[dataColumn13.Ordinal][dataColumn5] = true;
			}
			dataTable.AcceptChanges();
			return dataTable;
		}

		// Token: 0x06000E09 RID: 3593 RVA: 0x00075244 File Offset: 0x00074644
		private void ValidateOpen(string caller)
		{
			if (!this.isOpen)
			{
				throw ADP.DataReaderClosed(caller);
			}
		}

		// Token: 0x06000E0A RID: 3594 RVA: 0x00075260 File Offset: 0x00074660
		private void ValidateReader()
		{
			if (this.ReaderIsInvalid)
			{
				throw ExceptionBuilder.InvalidDataTableReader(this.currentDataTable.TableName);
			}
			if (this.IsSchemaChanged)
			{
				throw ExceptionBuilder.DataTableReaderSchemaIsInvalid(this.currentDataTable.TableName);
			}
		}

		// Token: 0x06000E0B RID: 3595 RVA: 0x000752A0 File Offset: 0x000746A0
		private void ValidateState(string caller)
		{
			this.ValidateOpen(caller);
			if (this.tableCleared)
			{
				throw ExceptionBuilder.EmptyDataTableReader(this.currentDataTable.TableName);
			}
			if (this.currentDataRow == null || this.currentDataTable == null)
			{
				this.ReaderIsInvalid = true;
				throw ExceptionBuilder.InvalidDataTableReader(this.currentDataTable.TableName);
			}
			if (this.currentDataRow.RowState == DataRowState.Deleted || this.currentDataRow.RowState == DataRowState.Detached || this.currentRowRemoved)
			{
				throw ExceptionBuilder.InvalidCurrentRowInDataTableReader();
			}
			if (0 > this.rowCounter || this.currentDataTable.Rows.Count <= this.rowCounter)
			{
				this.ReaderIsInvalid = true;
				throw ExceptionBuilder.InvalidDataTableReader(this.currentDataTable.TableName);
			}
		}

		// Token: 0x06000E0C RID: 3596 RVA: 0x00075358 File Offset: 0x00074758
		private void ValidateRow(int rowPosition)
		{
			if (this.ReaderIsInvalid)
			{
				throw ExceptionBuilder.InvalidDataTableReader(this.currentDataTable.TableName);
			}
			if (0 > rowPosition || this.currentDataTable.Rows.Count <= rowPosition)
			{
				this.ReaderIsInvalid = true;
				throw ExceptionBuilder.InvalidDataTableReader(this.currentDataTable.TableName);
			}
		}

		// Token: 0x06000E0D RID: 3597 RVA: 0x000753B0 File Offset: 0x000747B0
		internal void SchemaChanged()
		{
			this.IsSchemaChanged = true;
		}

		// Token: 0x06000E0E RID: 3598 RVA: 0x000753C4 File Offset: 0x000747C4
		internal void DataTableCleared()
		{
			if (!this.started)
			{
				return;
			}
			this.rowCounter = -1;
			if (!this.reachEORows)
			{
				this.currentRowRemoved = true;
			}
		}

		// Token: 0x06000E0F RID: 3599 RVA: 0x000753F0 File Offset: 0x000747F0
		internal void DataChanged(DataRowChangeEventArgs args)
		{
			if (!this.started || (this.rowCounter == -1 && !this.tableCleared))
			{
				return;
			}
			DataRowAction action = args.Action;
			if (action <= DataRowAction.Rollback)
			{
				if (action != DataRowAction.Delete && action != DataRowAction.Rollback)
				{
					return;
				}
			}
			else if (action != DataRowAction.Commit)
			{
				if (action != DataRowAction.Add)
				{
					return;
				}
				this.ValidateRow(this.rowCounter + 1);
				if (this.currentDataRow == this.currentDataTable.Rows[this.rowCounter + 1])
				{
					this.rowCounter++;
					return;
				}
				return;
			}
			if (args.Row.RowState == DataRowState.Detached)
			{
				if (args.Row != this.currentDataRow)
				{
					if (this.rowCounter != 0)
					{
						this.ValidateRow(this.rowCounter - 1);
						if (this.currentDataRow == this.currentDataTable.Rows[this.rowCounter - 1])
						{
							this.rowCounter--;
							return;
						}
					}
				}
				else
				{
					this.currentRowRemoved = true;
					if (this.rowCounter > 0)
					{
						this.rowCounter--;
						this.currentDataRow = this.currentDataTable.Rows[this.rowCounter];
						return;
					}
					this.rowCounter = -1;
					this.currentDataRow = null;
				}
			}
		}

		// Token: 0x04000406 RID: 1030
		private readonly DataTable[] tables;

		// Token: 0x04000407 RID: 1031
		private bool isOpen = true;

		// Token: 0x04000408 RID: 1032
		private DataTable schemaTable;

		// Token: 0x04000409 RID: 1033
		private int tableCounter = -1;

		// Token: 0x0400040A RID: 1034
		private int rowCounter = -1;

		// Token: 0x0400040B RID: 1035
		private DataTable currentDataTable;

		// Token: 0x0400040C RID: 1036
		private DataRow currentDataRow;

		// Token: 0x0400040D RID: 1037
		private bool hasRows = true;

		// Token: 0x0400040E RID: 1038
		private bool reachEORows;

		// Token: 0x0400040F RID: 1039
		private bool currentRowRemoved;

		// Token: 0x04000410 RID: 1040
		private bool schemaIsChanged;

		// Token: 0x04000411 RID: 1041
		private bool started;

		// Token: 0x04000412 RID: 1042
		private bool readerIsInvalid;

		// Token: 0x04000413 RID: 1043
		private DataTableReaderListener listener;

		// Token: 0x04000414 RID: 1044
		private bool tableCleared;
	}
}
