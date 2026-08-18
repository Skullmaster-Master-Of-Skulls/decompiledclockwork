using System;
using System.Collections;
using System.Data.Common;
using System.Globalization;

namespace System.Data
{
	// Token: 0x020000A3 RID: 163
	public sealed class DataTableReader : DbDataReader
	{
		// Token: 0x06000ACE RID: 2766 RVA: 0x00209BA8 File Offset: 0x00208FA8
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

		// Token: 0x06000ACF RID: 2767 RVA: 0x00209C08 File Offset: 0x00209008
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

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x06000AD0 RID: 2768 RVA: 0x00209C98 File Offset: 0x00209098
		// (set) Token: 0x06000AD1 RID: 2769 RVA: 0x00209CB8 File Offset: 0x002090B8
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

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x06000AD2 RID: 2770 RVA: 0x00209CF8 File Offset: 0x002090F8
		// (set) Token: 0x06000AD3 RID: 2771 RVA: 0x00209D18 File Offset: 0x00209118
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

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x06000AD4 RID: 2772 RVA: 0x00209D58 File Offset: 0x00209158
		internal DataTable CurrentDataTable
		{
			get
			{
				return this.currentDataTable;
			}
		}

		// Token: 0x06000AD5 RID: 2773 RVA: 0x00209D78 File Offset: 0x00209178
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

		// Token: 0x06000AD6 RID: 2774 RVA: 0x00209DE8 File Offset: 0x002091E8
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

		// Token: 0x06000AD7 RID: 2775 RVA: 0x00209E28 File Offset: 0x00209228
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

		// Token: 0x06000AD8 RID: 2776 RVA: 0x00209E68 File Offset: 0x00209268
		public override bool NextResult()
		{
			this.ValidateOpen("NextResult");
			if (this.tableCounter == this.tables.Length - 1)
			{
				return false;
			}
			this.currentDataTable = this.tables[++this.tableCounter];
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

		// Token: 0x06000AD9 RID: 2777 RVA: 0x00209F28 File Offset: 0x00209328
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

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x06000ADA RID: 2778 RVA: 0x0020A058 File Offset: 0x00209458
		public override int Depth
		{
			get
			{
				this.ValidateOpen("Depth");
				this.ValidateReader();
				return 0;
			}
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x06000ADB RID: 2779 RVA: 0x0020A078 File Offset: 0x00209478
		public override bool IsClosed
		{
			get
			{
				return !this.isOpen;
			}
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x06000ADC RID: 2780 RVA: 0x0020A098 File Offset: 0x00209498
		public override int RecordsAffected
		{
			get
			{
				this.ValidateReader();
				return 0;
			}
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x06000ADD RID: 2781 RVA: 0x0020A0B8 File Offset: 0x002094B8
		public override bool HasRows
		{
			get
			{
				this.ValidateOpen("HasRows");
				this.ValidateReader();
				return this.hasRows;
			}
		}

		// Token: 0x1700016C RID: 364
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

		// Token: 0x1700016D RID: 365
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

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x06000AE0 RID: 2784 RVA: 0x0020A1D8 File Offset: 0x002095D8
		public override int FieldCount
		{
			get
			{
				this.ValidateOpen("FieldCount");
				this.ValidateReader();
				return this.currentDataTable.Columns.Count;
			}
		}

		// Token: 0x06000AE1 RID: 2785 RVA: 0x0020A208 File Offset: 0x00209608
		public override Type GetProviderSpecificFieldType(int ordinal)
		{
			this.ValidateOpen("GetProviderSpecificFieldType");
			this.ValidateReader();
			return this.GetFieldType(ordinal);
		}

		// Token: 0x06000AE2 RID: 2786 RVA: 0x0020A238 File Offset: 0x00209638
		public override object GetProviderSpecificValue(int ordinal)
		{
			this.ValidateOpen("GetProviderSpecificValue");
			this.ValidateReader();
			return this.GetValue(ordinal);
		}

		// Token: 0x06000AE3 RID: 2787 RVA: 0x0020A268 File Offset: 0x00209668
		public override int GetProviderSpecificValues(object[] values)
		{
			this.ValidateOpen("GetProviderSpecificValues");
			this.ValidateReader();
			return this.GetValues(values);
		}

		// Token: 0x06000AE4 RID: 2788 RVA: 0x0020A298 File Offset: 0x00209698
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

		// Token: 0x06000AE5 RID: 2789 RVA: 0x0020A308 File Offset: 0x00209708
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

		// Token: 0x06000AE6 RID: 2790 RVA: 0x0020A378 File Offset: 0x00209778
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

		// Token: 0x06000AE7 RID: 2791 RVA: 0x0020A458 File Offset: 0x00209858
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

		// Token: 0x06000AE8 RID: 2792 RVA: 0x0020A4C8 File Offset: 0x002098C8
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

		// Token: 0x06000AE9 RID: 2793 RVA: 0x0020A5A8 File Offset: 0x002099A8
		public override string GetDataTypeName(int ordinal)
		{
			this.ValidateOpen("GetDataTypeName");
			this.ValidateReader();
			return this.GetFieldType(ordinal).Name;
		}

		// Token: 0x06000AEA RID: 2794 RVA: 0x0020A5D8 File Offset: 0x002099D8
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

		// Token: 0x06000AEB RID: 2795 RVA: 0x0020A648 File Offset: 0x00209A48
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

		// Token: 0x06000AEC RID: 2796 RVA: 0x0020A6B8 File Offset: 0x00209AB8
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

		// Token: 0x06000AED RID: 2797 RVA: 0x0020A728 File Offset: 0x00209B28
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

		// Token: 0x06000AEE RID: 2798 RVA: 0x0020A798 File Offset: 0x00209B98
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

		// Token: 0x06000AEF RID: 2799 RVA: 0x0020A808 File Offset: 0x00209C08
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

		// Token: 0x06000AF0 RID: 2800 RVA: 0x0020A878 File Offset: 0x00209C78
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

		// Token: 0x06000AF1 RID: 2801 RVA: 0x0020A8E8 File Offset: 0x00209CE8
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

		// Token: 0x06000AF2 RID: 2802 RVA: 0x0020A958 File Offset: 0x00209D58
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

		// Token: 0x06000AF3 RID: 2803 RVA: 0x0020A9C8 File Offset: 0x00209DC8
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

		// Token: 0x06000AF4 RID: 2804 RVA: 0x0020AA38 File Offset: 0x00209E38
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

		// Token: 0x06000AF5 RID: 2805 RVA: 0x0020AA88 File Offset: 0x00209E88
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

		// Token: 0x06000AF6 RID: 2806 RVA: 0x0020AAF8 File Offset: 0x00209EF8
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

		// Token: 0x06000AF7 RID: 2807 RVA: 0x0020AB58 File Offset: 0x00209F58
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

		// Token: 0x06000AF8 RID: 2808 RVA: 0x0020ABE8 File Offset: 0x00209FE8
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

		// Token: 0x06000AF9 RID: 2809 RVA: 0x0020AC48 File Offset: 0x0020A048
		public override IEnumerator GetEnumerator()
		{
			this.ValidateOpen("GetEnumerator");
			return new DbEnumerator(this);
		}

		// Token: 0x06000AFA RID: 2810 RVA: 0x0020AC68 File Offset: 0x0020A068
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

		// Token: 0x06000AFB RID: 2811 RVA: 0x0020B2C8 File Offset: 0x0020A6C8
		private void ValidateOpen(string caller)
		{
			if (!this.isOpen)
			{
				throw ADP.DataReaderClosed(caller);
			}
		}

		// Token: 0x06000AFC RID: 2812 RVA: 0x0020B2E8 File Offset: 0x0020A6E8
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

		// Token: 0x06000AFD RID: 2813 RVA: 0x0020B328 File Offset: 0x0020A728
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

		// Token: 0x06000AFE RID: 2814 RVA: 0x0020B3E8 File Offset: 0x0020A7E8
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

		// Token: 0x06000AFF RID: 2815 RVA: 0x0020B448 File Offset: 0x0020A848
		internal void SchemaChanged()
		{
			this.IsSchemaChanged = true;
		}

		// Token: 0x06000B00 RID: 2816 RVA: 0x0020B468 File Offset: 0x0020A868
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

		// Token: 0x06000B01 RID: 2817 RVA: 0x0020B498 File Offset: 0x0020A898
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
					if (this.rowCounter == 0)
					{
						return;
					}
					this.ValidateRow(this.rowCounter - 1);
					if (this.currentDataRow == this.currentDataTable.Rows[this.rowCounter - 1])
					{
						this.rowCounter--;
						return;
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

		// Token: 0x04000828 RID: 2088
		private readonly DataTable[] tables;

		// Token: 0x04000829 RID: 2089
		private bool isOpen = true;

		// Token: 0x0400082A RID: 2090
		private DataTable schemaTable;

		// Token: 0x0400082B RID: 2091
		private int tableCounter = -1;

		// Token: 0x0400082C RID: 2092
		private int rowCounter = -1;

		// Token: 0x0400082D RID: 2093
		private DataTable currentDataTable;

		// Token: 0x0400082E RID: 2094
		private DataRow currentDataRow;

		// Token: 0x0400082F RID: 2095
		private bool hasRows = true;

		// Token: 0x04000830 RID: 2096
		private bool reachEORows;

		// Token: 0x04000831 RID: 2097
		private bool currentRowRemoved;

		// Token: 0x04000832 RID: 2098
		private bool schemaIsChanged;

		// Token: 0x04000833 RID: 2099
		private bool started;

		// Token: 0x04000834 RID: 2100
		private bool readerIsInvalid;

		// Token: 0x04000835 RID: 2101
		private DataTableReaderListener listener;

		// Token: 0x04000836 RID: 2102
		private bool tableCleared;
	}
}
