using System;
using System.Data.Common;
using System.Data.ProviderBase;
using System.Data.SqlTypes;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.SqlServer.Server;

namespace System.Data.SqlClient
{
	// Token: 0x020001C6 RID: 454
	internal sealed class SqlDataReaderSmi : SqlDataReader
	{
		// Token: 0x17000454 RID: 1108
		// (get) Token: 0x06001C54 RID: 7252 RVA: 0x000C7E90 File Offset: 0x000C7290
		public override int FieldCount
		{
			get
			{
				this.ThrowIfClosed("FieldCount");
				return this.InternalFieldCount;
			}
		}

		// Token: 0x17000455 RID: 1109
		// (get) Token: 0x06001C55 RID: 7253 RVA: 0x000C7EB0 File Offset: 0x000C72B0
		public override int VisibleFieldCount
		{
			get
			{
				this.ThrowIfClosed("VisibleFieldCount");
				if (this.FNotInResults())
				{
					return 0;
				}
				return this._visibleColumnCount;
			}
		}

		// Token: 0x06001C56 RID: 7254 RVA: 0x000C7ED8 File Offset: 0x000C72D8
		public override string GetName(int ordinal)
		{
			this.EnsureCanGetMetaData("GetName");
			return this._currentMetaData[ordinal].Name;
		}

		// Token: 0x06001C57 RID: 7255 RVA: 0x000C7F00 File Offset: 0x000C7300
		public override string GetDataTypeName(int ordinal)
		{
			this.EnsureCanGetMetaData("GetDataTypeName");
			SmiExtendedMetaData smiExtendedMetaData = this._currentMetaData[ordinal];
			if (SqlDbType.Udt == smiExtendedMetaData.SqlDbType)
			{
				return string.Concat(new string[]
				{
					smiExtendedMetaData.TypeSpecificNamePart1,
					".",
					smiExtendedMetaData.TypeSpecificNamePart2,
					".",
					smiExtendedMetaData.TypeSpecificNamePart3
				});
			}
			return smiExtendedMetaData.TypeName;
		}

		// Token: 0x06001C58 RID: 7256 RVA: 0x000C7F68 File Offset: 0x000C7368
		public override Type GetFieldType(int ordinal)
		{
			this.EnsureCanGetMetaData("GetFieldType");
			if (SqlDbType.Udt == this._currentMetaData[ordinal].SqlDbType)
			{
				return this._currentMetaData[ordinal].Type;
			}
			return MetaType.GetMetaTypeFromSqlDbType(this._currentMetaData[ordinal].SqlDbType, this._currentMetaData[ordinal].IsMultiValued).ClassType;
		}

		// Token: 0x06001C59 RID: 7257 RVA: 0x000C7FC4 File Offset: 0x000C73C4
		public override Type GetProviderSpecificFieldType(int ordinal)
		{
			this.EnsureCanGetMetaData("GetProviderSpecificFieldType");
			if (SqlDbType.Udt == this._currentMetaData[ordinal].SqlDbType)
			{
				return this._currentMetaData[ordinal].Type;
			}
			return MetaType.GetMetaTypeFromSqlDbType(this._currentMetaData[ordinal].SqlDbType, this._currentMetaData[ordinal].IsMultiValued).SqlType;
		}

		// Token: 0x17000456 RID: 1110
		// (get) Token: 0x06001C5A RID: 7258 RVA: 0x000C8020 File Offset: 0x000C7420
		public override int Depth
		{
			get
			{
				this.ThrowIfClosed("Depth");
				return 0;
			}
		}

		// Token: 0x06001C5B RID: 7259 RVA: 0x000C803C File Offset: 0x000C743C
		public override object GetValue(int ordinal)
		{
			this.EnsureCanGetCol("GetValue", ordinal);
			SmiQueryMetaData metaData = this._currentMetaData[ordinal];
			if (this._currentConnection.IsKatmaiOrNewer)
			{
				return ValueUtilsSmi.GetValue200(this._readerEventSink, (SmiTypedGetterSetter)this._currentColumnValuesV3, ordinal, metaData, this._currentConnection.InternalContext);
			}
			return ValueUtilsSmi.GetValue(this._readerEventSink, this._currentColumnValuesV3, ordinal, metaData, this._currentConnection.InternalContext);
		}

		// Token: 0x06001C5C RID: 7260 RVA: 0x000C80B0 File Offset: 0x000C74B0
		public override T GetFieldValue<T>(int ordinal)
		{
			this.EnsureCanGetCol("GetFieldValue<T>", ordinal);
			SmiQueryMetaData metaData = this._currentMetaData[ordinal];
			if (SqlDataReader._typeofINullable.IsAssignableFrom(typeof(T)))
			{
				if (this._currentConnection.IsKatmaiOrNewer)
				{
					return (T)((object)ValueUtilsSmi.GetSqlValue200(this._readerEventSink, (SmiTypedGetterSetter)this._currentColumnValuesV3, ordinal, metaData, this._currentConnection.InternalContext));
				}
				return (T)((object)ValueUtilsSmi.GetSqlValue(this._readerEventSink, this._currentColumnValuesV3, ordinal, metaData, this._currentConnection.InternalContext));
			}
			else
			{
				if (this._currentConnection.IsKatmaiOrNewer)
				{
					return (T)((object)ValueUtilsSmi.GetValue200(this._readerEventSink, (SmiTypedGetterSetter)this._currentColumnValuesV3, ordinal, metaData, this._currentConnection.InternalContext));
				}
				return (T)((object)ValueUtilsSmi.GetValue(this._readerEventSink, this._currentColumnValuesV3, ordinal, metaData, this._currentConnection.InternalContext));
			}
		}

		// Token: 0x06001C5D RID: 7261 RVA: 0x000C819C File Offset: 0x000C759C
		public override Task<T> GetFieldValueAsync<T>(int ordinal, CancellationToken cancellationToken)
		{
			return ADP.CreatedTaskWithException<T>(ADP.ExceptionWithStackTrace(SQL.NotAvailableOnContextConnection()));
		}

		// Token: 0x06001C5E RID: 7262 RVA: 0x000C81B8 File Offset: 0x000C75B8
		internal override SqlBuffer.StorageType GetVariantInternalStorageType(int ordinal)
		{
			if (this.IsDBNull(ordinal))
			{
				return SqlBuffer.StorageType.Empty;
			}
			SmiMetaData variantType = this._currentColumnValuesV3.GetVariantType(this._readerEventSink, ordinal);
			if (variantType == null)
			{
				return SqlBuffer.StorageType.Empty;
			}
			return ValueUtilsSmi.SqlDbTypeToStorageType(variantType.SqlDbType);
		}

		// Token: 0x06001C5F RID: 7263 RVA: 0x000C81F4 File Offset: 0x000C75F4
		public override int GetValues(object[] values)
		{
			this.EnsureCanGetCol("GetValues", 0);
			if (values == null)
			{
				throw ADP.ArgumentNull("values");
			}
			int num = (values.Length < this._visibleColumnCount) ? values.Length : this._visibleColumnCount;
			for (int i = 0; i < num; i++)
			{
				values[this._indexMap[i]] = this.GetValue(i);
			}
			return num;
		}

		// Token: 0x06001C60 RID: 7264 RVA: 0x000C8250 File Offset: 0x000C7650
		public override int GetOrdinal(string name)
		{
			this.EnsureCanGetMetaData("GetOrdinal");
			if (this._fieldNameLookup == null)
			{
				this._fieldNameLookup = new FieldNameLookup(this, -1);
			}
			return this._fieldNameLookup.GetOrdinal(name);
		}

		// Token: 0x17000457 RID: 1111
		public override object this[int ordinal]
		{
			get
			{
				return this.GetValue(ordinal);
			}
		}

		// Token: 0x17000458 RID: 1112
		public override object this[string strName]
		{
			get
			{
				return this.GetValue(this.GetOrdinal(strName));
			}
		}

		// Token: 0x06001C63 RID: 7267 RVA: 0x000C82BC File Offset: 0x000C76BC
		public override bool IsDBNull(int ordinal)
		{
			this.EnsureCanGetCol("IsDBNull", ordinal);
			return ValueUtilsSmi.IsDBNull(this._readerEventSink, this._currentColumnValuesV3, ordinal);
		}

		// Token: 0x06001C64 RID: 7268 RVA: 0x000C82E8 File Offset: 0x000C76E8
		public override Task<bool> IsDBNullAsync(int ordinal, CancellationToken cancellationToken)
		{
			return ADP.CreatedTaskWithException<bool>(ADP.ExceptionWithStackTrace(SQL.NotAvailableOnContextConnection()));
		}

		// Token: 0x06001C65 RID: 7269 RVA: 0x000C8304 File Offset: 0x000C7704
		public override bool GetBoolean(int ordinal)
		{
			this.EnsureCanGetCol("GetBoolean", ordinal);
			return ValueUtilsSmi.GetBoolean(this._readerEventSink, this._currentColumnValuesV3, ordinal, this._currentMetaData[ordinal]);
		}

		// Token: 0x06001C66 RID: 7270 RVA: 0x000C8338 File Offset: 0x000C7738
		public override byte GetByte(int ordinal)
		{
			this.EnsureCanGetCol("GetByte", ordinal);
			return ValueUtilsSmi.GetByte(this._readerEventSink, this._currentColumnValuesV3, ordinal, this._currentMetaData[ordinal]);
		}

		// Token: 0x06001C67 RID: 7271 RVA: 0x000C836C File Offset: 0x000C776C
		public override long GetBytes(int ordinal, long fieldOffset, byte[] buffer, int bufferOffset, int length)
		{
			this.EnsureCanGetCol("GetBytes", ordinal);
			return ValueUtilsSmi.GetBytes(this._readerEventSink, this._currentColumnValuesV3, ordinal, this._currentMetaData[ordinal], fieldOffset, buffer, bufferOffset, length, true);
		}

		// Token: 0x06001C68 RID: 7272 RVA: 0x000C83A8 File Offset: 0x000C77A8
		internal override long GetBytesInternal(int ordinal, long fieldOffset, byte[] buffer, int bufferOffset, int length)
		{
			this.EnsureCanGetCol("GetBytes", ordinal);
			return ValueUtilsSmi.GetBytesInternal(this._readerEventSink, this._currentColumnValuesV3, ordinal, this._currentMetaData[ordinal], fieldOffset, buffer, bufferOffset, length, false);
		}

		// Token: 0x06001C69 RID: 7273 RVA: 0x000C83E4 File Offset: 0x000C77E4
		public override char GetChar(int ordinal)
		{
			throw ADP.NotSupported();
		}

		// Token: 0x06001C6A RID: 7274 RVA: 0x000C83F8 File Offset: 0x000C77F8
		public override long GetChars(int ordinal, long fieldOffset, char[] buffer, int bufferOffset, int length)
		{
			this.EnsureCanGetCol("GetChars", ordinal);
			SmiExtendedMetaData smiExtendedMetaData = this._currentMetaData[ordinal];
			if (base.IsCommandBehavior(CommandBehavior.SequentialAccess) && smiExtendedMetaData.SqlDbType == SqlDbType.Xml)
			{
				return base.GetStreamingXmlChars(ordinal, fieldOffset, buffer, bufferOffset, length);
			}
			return ValueUtilsSmi.GetChars(this._readerEventSink, this._currentColumnValuesV3, ordinal, smiExtendedMetaData, fieldOffset, buffer, bufferOffset, length);
		}

		// Token: 0x06001C6B RID: 7275 RVA: 0x000C8458 File Offset: 0x000C7858
		public override Guid GetGuid(int ordinal)
		{
			this.EnsureCanGetCol("GetGuid", ordinal);
			return ValueUtilsSmi.GetGuid(this._readerEventSink, this._currentColumnValuesV3, ordinal, this._currentMetaData[ordinal]);
		}

		// Token: 0x06001C6C RID: 7276 RVA: 0x000C848C File Offset: 0x000C788C
		public override short GetInt16(int ordinal)
		{
			this.EnsureCanGetCol("GetInt16", ordinal);
			return ValueUtilsSmi.GetInt16(this._readerEventSink, this._currentColumnValuesV3, ordinal, this._currentMetaData[ordinal]);
		}

		// Token: 0x06001C6D RID: 7277 RVA: 0x000C84C0 File Offset: 0x000C78C0
		public override int GetInt32(int ordinal)
		{
			this.EnsureCanGetCol("GetInt32", ordinal);
			return ValueUtilsSmi.GetInt32(this._readerEventSink, this._currentColumnValuesV3, ordinal, this._currentMetaData[ordinal]);
		}

		// Token: 0x06001C6E RID: 7278 RVA: 0x000C84F4 File Offset: 0x000C78F4
		public override long GetInt64(int ordinal)
		{
			this.EnsureCanGetCol("GetInt64", ordinal);
			return ValueUtilsSmi.GetInt64(this._readerEventSink, this._currentColumnValuesV3, ordinal, this._currentMetaData[ordinal]);
		}

		// Token: 0x06001C6F RID: 7279 RVA: 0x000C8528 File Offset: 0x000C7928
		public override float GetFloat(int ordinal)
		{
			this.EnsureCanGetCol("GetFloat", ordinal);
			return ValueUtilsSmi.GetSingle(this._readerEventSink, this._currentColumnValuesV3, ordinal, this._currentMetaData[ordinal]);
		}

		// Token: 0x06001C70 RID: 7280 RVA: 0x000C855C File Offset: 0x000C795C
		public override double GetDouble(int ordinal)
		{
			this.EnsureCanGetCol("GetDouble", ordinal);
			return ValueUtilsSmi.GetDouble(this._readerEventSink, this._currentColumnValuesV3, ordinal, this._currentMetaData[ordinal]);
		}

		// Token: 0x06001C71 RID: 7281 RVA: 0x000C8590 File Offset: 0x000C7990
		public override string GetString(int ordinal)
		{
			this.EnsureCanGetCol("GetString", ordinal);
			return ValueUtilsSmi.GetString(this._readerEventSink, this._currentColumnValuesV3, ordinal, this._currentMetaData[ordinal]);
		}

		// Token: 0x06001C72 RID: 7282 RVA: 0x000C85C4 File Offset: 0x000C79C4
		public override decimal GetDecimal(int ordinal)
		{
			this.EnsureCanGetCol("GetDecimal", ordinal);
			return ValueUtilsSmi.GetDecimal(this._readerEventSink, this._currentColumnValuesV3, ordinal, this._currentMetaData[ordinal]);
		}

		// Token: 0x06001C73 RID: 7283 RVA: 0x000C85F8 File Offset: 0x000C79F8
		public override DateTime GetDateTime(int ordinal)
		{
			this.EnsureCanGetCol("GetDateTime", ordinal);
			return ValueUtilsSmi.GetDateTime(this._readerEventSink, this._currentColumnValuesV3, ordinal, this._currentMetaData[ordinal]);
		}

		// Token: 0x17000459 RID: 1113
		// (get) Token: 0x06001C74 RID: 7284 RVA: 0x000C862C File Offset: 0x000C7A2C
		public override bool IsClosed
		{
			get
			{
				return this.IsReallyClosed();
			}
		}

		// Token: 0x1700045A RID: 1114
		// (get) Token: 0x06001C75 RID: 7285 RVA: 0x000C8640 File Offset: 0x000C7A40
		public override int RecordsAffected
		{
			get
			{
				return base.Command.InternalRecordsAffected;
			}
		}

		// Token: 0x06001C76 RID: 7286 RVA: 0x000C8658 File Offset: 0x000C7A58
		internal override void CloseReaderFromConnection()
		{
			this.CloseInternal(false);
		}

		// Token: 0x06001C77 RID: 7287 RVA: 0x000C866C File Offset: 0x000C7A6C
		public override void Close()
		{
			this.CloseInternal(base.IsCommandBehavior(CommandBehavior.CloseConnection));
		}

		// Token: 0x06001C78 RID: 7288 RVA: 0x000C8688 File Offset: 0x000C7A88
		private void CloseInternal(bool closeConnection)
		{
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<sc.SqlDataReaderSmi.Close|API> %d#", this.ObjectID);
			bool flag = true;
			try
			{
				if (!this.IsClosed)
				{
					this._hasRows = false;
					while (this._eventStream.HasEvents)
					{
						this._eventStream.ProcessEvent(this._readerEventSink);
						this._readerEventSink.ProcessMessagesAndThrow(true);
					}
					this._requestExecutor.Close(this._readerEventSink);
					this._readerEventSink.ProcessMessagesAndThrow(true);
				}
			}
			catch (Exception e)
			{
				flag = ADP.IsCatchableExceptionType(e);
				throw;
			}
			finally
			{
				if (flag)
				{
					this._isOpen = false;
					if (closeConnection && base.Connection != null)
					{
						base.Connection.Close();
					}
					Bid.ScopeLeave(ref intPtr);
				}
			}
		}

		// Token: 0x06001C79 RID: 7289 RVA: 0x000C8770 File Offset: 0x000C7B70
		public override bool NextResult()
		{
			this.ThrowIfClosed("NextResult");
			return this.InternalNextResult(false);
		}

		// Token: 0x06001C7A RID: 7290 RVA: 0x000C8794 File Offset: 0x000C7B94
		public override Task<bool> NextResultAsync(CancellationToken cancellationToken)
		{
			return ADP.CreatedTaskWithException<bool>(ADP.ExceptionWithStackTrace(SQL.NotAvailableOnContextConnection()));
		}

		// Token: 0x06001C7B RID: 7291 RVA: 0x000C87B0 File Offset: 0x000C7BB0
		internal bool InternalNextResult(bool ignoreNonFatalMessages)
		{
			IntPtr zero = IntPtr.Zero;
			if (Bid.AdvancedOn)
			{
				Bid.ScopeEnter(out zero, "<sc.SqlDataReaderSmi.InternalNextResult|ADV> %d#", this.ObjectID);
			}
			bool result;
			try
			{
				this._hasRows = false;
				if (SqlDataReaderSmi.PositionState.AfterResults != this._currentPosition)
				{
					while (this.InternalRead(ignoreNonFatalMessages))
					{
					}
					this.ResetResultSet();
					while (this._currentMetaData == null && this._eventStream.HasEvents)
					{
						this._eventStream.ProcessEvent(this._readerEventSink);
						this._readerEventSink.ProcessMessagesAndThrow(ignoreNonFatalMessages);
					}
				}
				result = (SqlDataReaderSmi.PositionState.AfterResults != this._currentPosition);
			}
			finally
			{
				if (Bid.AdvancedOn)
				{
					Bid.ScopeLeave(ref zero);
				}
			}
			return result;
		}

		// Token: 0x06001C7C RID: 7292 RVA: 0x000C886C File Offset: 0x000C7C6C
		public override bool Read()
		{
			this.ThrowIfClosed("Read");
			return this.InternalRead(false);
		}

		// Token: 0x06001C7D RID: 7293 RVA: 0x000C8890 File Offset: 0x000C7C90
		public override Task<bool> ReadAsync(CancellationToken cancellationToken)
		{
			return ADP.CreatedTaskWithException<bool>(ADP.ExceptionWithStackTrace(SQL.NotAvailableOnContextConnection()));
		}

		// Token: 0x06001C7E RID: 7294 RVA: 0x000C88AC File Offset: 0x000C7CAC
		internal bool InternalRead(bool ignoreNonFatalErrors)
		{
			IntPtr zero = IntPtr.Zero;
			if (Bid.AdvancedOn)
			{
				Bid.ScopeEnter(out zero, "<sc.SqlDataReaderSmi.InternalRead|ADV> %d#", this.ObjectID);
			}
			bool result;
			try
			{
				if (this.FInResults())
				{
					this._currentColumnValues = null;
					this._currentColumnValuesV3 = null;
					if (this._currentStream != null)
					{
						this._currentStream.SetClosed();
						this._currentStream = null;
					}
					if (this._currentTextReader != null)
					{
						this._currentTextReader.SetClosed();
						this._currentTextReader = null;
					}
					while (this._currentColumnValues == null && this._currentColumnValuesV3 == null && this.FInResults() && SqlDataReaderSmi.PositionState.AfterRows != this._currentPosition && this._eventStream.HasEvents)
					{
						this._eventStream.ProcessEvent(this._readerEventSink);
						this._readerEventSink.ProcessMessagesAndThrow(ignoreNonFatalErrors);
					}
				}
				result = (SqlDataReaderSmi.PositionState.OnRow == this._currentPosition);
			}
			finally
			{
				if (Bid.AdvancedOn)
				{
					Bid.ScopeLeave(ref zero);
				}
			}
			return result;
		}

		// Token: 0x06001C7F RID: 7295 RVA: 0x000C89AC File Offset: 0x000C7DAC
		public override DataTable GetSchemaTable()
		{
			this.ThrowIfClosed("GetSchemaTable");
			if (this._schemaTable == null && this.FInResults())
			{
				DataTable dataTable = new DataTable("SchemaTable");
				dataTable.Locale = CultureInfo.InvariantCulture;
				dataTable.MinimumCapacity = this.InternalFieldCount;
				DataColumn column = new DataColumn(SchemaTableColumn.ColumnName, typeof(string));
				DataColumn dataColumn = new DataColumn(SchemaTableColumn.ColumnOrdinal, typeof(int));
				DataColumn column2 = new DataColumn(SchemaTableColumn.ColumnSize, typeof(int));
				DataColumn column3 = new DataColumn(SchemaTableColumn.NumericPrecision, typeof(short));
				DataColumn column4 = new DataColumn(SchemaTableColumn.NumericScale, typeof(short));
				DataColumn column5 = new DataColumn(SchemaTableColumn.DataType, typeof(Type));
				DataColumn column6 = new DataColumn(SchemaTableOptionalColumn.ProviderSpecificDataType, typeof(Type));
				DataColumn column7 = new DataColumn(SchemaTableColumn.ProviderType, typeof(int));
				DataColumn column8 = new DataColumn(SchemaTableColumn.NonVersionedProviderType, typeof(int));
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
				columns.Add(column7);
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
				columns.Add(column8);
				columns.Add(column29);
				int i = 0;
				while (i < this.InternalFieldCount)
				{
					SmiQueryMetaData smiQueryMetaData = this._currentMetaData[i];
					long num = smiQueryMetaData.MaxLength;
					MetaType metaType = MetaType.GetMetaTypeFromSqlDbType(smiQueryMetaData.SqlDbType, smiQueryMetaData.IsMultiValued);
					if (-1L == num)
					{
						metaType = MetaType.GetMaxMetaTypeFromMetaType(metaType);
						num = ((metaType.IsSizeInCharacters && !metaType.IsPlp) ? 1073741823L : 2147483647L);
					}
					DataRow dataRow = dataTable.NewRow();
					if (SqlDbType.Decimal == smiQueryMetaData.SqlDbType)
					{
						num = 17L;
					}
					else if (SqlDbType.Variant == smiQueryMetaData.SqlDbType)
					{
						num = 8009L;
					}
					dataRow[column] = smiQueryMetaData.Name;
					dataRow[dataColumn] = i;
					dataRow[column2] = num;
					dataRow[column7] = (int)smiQueryMetaData.SqlDbType;
					dataRow[column8] = (int)smiQueryMetaData.SqlDbType;
					if (smiQueryMetaData.SqlDbType != SqlDbType.Udt)
					{
						dataRow[column5] = metaType.ClassType;
						dataRow[column6] = metaType.SqlType;
					}
					else
					{
						dataRow[column25] = smiQueryMetaData.Type.AssemblyQualifiedName;
						dataRow[column5] = smiQueryMetaData.Type;
						dataRow[column6] = smiQueryMetaData.Type;
					}
					byte b;
					switch (smiQueryMetaData.SqlDbType)
					{
					case SqlDbType.BigInt:
					case SqlDbType.DateTime:
					case SqlDbType.Decimal:
					case SqlDbType.Int:
					case SqlDbType.Money:
					case SqlDbType.SmallDateTime:
					case SqlDbType.SmallInt:
					case SqlDbType.SmallMoney:
					case SqlDbType.TinyInt:
						b = smiQueryMetaData.Precision;
						break;
					case SqlDbType.Binary:
					case SqlDbType.Bit:
					case SqlDbType.Char:
					case SqlDbType.Image:
					case SqlDbType.NChar:
					case SqlDbType.NText:
					case SqlDbType.NVarChar:
					case SqlDbType.UniqueIdentifier:
					case SqlDbType.Text:
					case SqlDbType.Timestamp:
						goto IL_5B6;
					case SqlDbType.Float:
						b = 15;
						break;
					case SqlDbType.Real:
						b = 7;
						break;
					default:
						goto IL_5B6;
					}
					IL_5BD:
					dataRow[column3] = b;
					if (SqlDbType.Decimal == smiQueryMetaData.SqlDbType || SqlDbType.Time == smiQueryMetaData.SqlDbType || SqlDbType.DateTime2 == smiQueryMetaData.SqlDbType || SqlDbType.DateTimeOffset == smiQueryMetaData.SqlDbType)
					{
						dataRow[column4] = smiQueryMetaData.Scale;
					}
					else
					{
						dataRow[column4] = MetaType.GetMetaTypeFromSqlDbType(smiQueryMetaData.SqlDbType, smiQueryMetaData.IsMultiValued).Scale;
					}
					dataRow[column9] = smiQueryMetaData.AllowsDBNull;
					if (!smiQueryMetaData.IsAliased.IsNull)
					{
						dataRow[column21] = smiQueryMetaData.IsAliased.Value;
					}
					if (!smiQueryMetaData.IsKey.IsNull)
					{
						dataRow[column13] = smiQueryMetaData.IsKey.Value;
					}
					if (!smiQueryMetaData.IsHidden.IsNull)
					{
						dataRow[column15] = smiQueryMetaData.IsHidden.Value;
					}
					if (!smiQueryMetaData.IsExpression.IsNull)
					{
						dataRow[column22] = smiQueryMetaData.IsExpression.Value;
					}
					dataRow[column10] = smiQueryMetaData.IsReadOnly;
					dataRow[column23] = smiQueryMetaData.IsIdentity;
					dataRow[column29] = smiQueryMetaData.IsColumnSet;
					dataRow[column14] = smiQueryMetaData.IsIdentity;
					dataRow[dataColumn2] = metaType.IsLong;
					if (SqlDbType.Timestamp == smiQueryMetaData.SqlDbType)
					{
						dataRow[column12] = true;
						dataRow[column11] = true;
					}
					else
					{
						dataRow[column12] = false;
						dataRow[column11] = false;
					}
					if (!ADP.IsEmpty(smiQueryMetaData.ColumnName))
					{
						dataRow[column19] = smiQueryMetaData.ColumnName;
					}
					else if (!ADP.IsEmpty(smiQueryMetaData.Name))
					{
						dataRow[column19] = smiQueryMetaData.Name;
					}
					if (!ADP.IsEmpty(smiQueryMetaData.TableName))
					{
						dataRow[column18] = smiQueryMetaData.TableName;
					}
					if (!ADP.IsEmpty(smiQueryMetaData.SchemaName))
					{
						dataRow[column17] = smiQueryMetaData.SchemaName;
					}
					if (!ADP.IsEmpty(smiQueryMetaData.CatalogName))
					{
						dataRow[column16] = smiQueryMetaData.CatalogName;
					}
					if (!ADP.IsEmpty(smiQueryMetaData.ServerName))
					{
						dataRow[column20] = smiQueryMetaData.ServerName;
					}
					if (SqlDbType.Udt == smiQueryMetaData.SqlDbType)
					{
						dataRow[column24] = string.Concat(new string[]
						{
							smiQueryMetaData.TypeSpecificNamePart1,
							".",
							smiQueryMetaData.TypeSpecificNamePart2,
							".",
							smiQueryMetaData.TypeSpecificNamePart3
						});
					}
					else
					{
						dataRow[column24] = metaType.TypeName;
					}
					if (SqlDbType.Xml == smiQueryMetaData.SqlDbType)
					{
						dataRow[column26] = smiQueryMetaData.TypeSpecificNamePart1;
						dataRow[column27] = smiQueryMetaData.TypeSpecificNamePart2;
						dataRow[column28] = smiQueryMetaData.TypeSpecificNamePart3;
					}
					dataTable.Rows.Add(dataRow);
					dataRow.AcceptChanges();
					i++;
					continue;
					IL_5B6:
					b = byte.MaxValue;
					goto IL_5BD;
				}
				foreach (object obj in columns)
				{
					DataColumn dataColumn3 = (DataColumn)obj;
					dataColumn3.ReadOnly = true;
				}
				this._schemaTable = dataTable;
			}
			return this._schemaTable;
		}

		// Token: 0x06001C80 RID: 7296 RVA: 0x000C9310 File Offset: 0x000C8710
		public override SqlBinary GetSqlBinary(int ordinal)
		{
			this.EnsureCanGetCol("GetSqlBinary", ordinal);
			return ValueUtilsSmi.GetSqlBinary(this._readerEventSink, this._currentColumnValuesV3, ordinal, this._currentMetaData[ordinal]);
		}

		// Token: 0x06001C81 RID: 7297 RVA: 0x000C9344 File Offset: 0x000C8744
		public override SqlBoolean GetSqlBoolean(int ordinal)
		{
			this.EnsureCanGetCol("GetSqlBoolean", ordinal);
			return ValueUtilsSmi.GetSqlBoolean(this._readerEventSink, this._currentColumnValuesV3, ordinal, this._currentMetaData[ordinal]);
		}

		// Token: 0x06001C82 RID: 7298 RVA: 0x000C9378 File Offset: 0x000C8778
		public override SqlByte GetSqlByte(int ordinal)
		{
			this.EnsureCanGetCol("GetSqlByte", ordinal);
			return ValueUtilsSmi.GetSqlByte(this._readerEventSink, this._currentColumnValuesV3, ordinal, this._currentMetaData[ordinal]);
		}

		// Token: 0x06001C83 RID: 7299 RVA: 0x000C93AC File Offset: 0x000C87AC
		public override SqlInt16 GetSqlInt16(int ordinal)
		{
			this.EnsureCanGetCol("GetSqlInt16", ordinal);
			return ValueUtilsSmi.GetSqlInt16(this._readerEventSink, this._currentColumnValuesV3, ordinal, this._currentMetaData[ordinal]);
		}

		// Token: 0x06001C84 RID: 7300 RVA: 0x000C93E0 File Offset: 0x000C87E0
		public override SqlInt32 GetSqlInt32(int ordinal)
		{
			this.EnsureCanGetCol("GetSqlInt32", ordinal);
			return ValueUtilsSmi.GetSqlInt32(this._readerEventSink, this._currentColumnValuesV3, ordinal, this._currentMetaData[ordinal]);
		}

		// Token: 0x06001C85 RID: 7301 RVA: 0x000C9414 File Offset: 0x000C8814
		public override SqlInt64 GetSqlInt64(int ordinal)
		{
			this.EnsureCanGetCol("GetSqlInt64", ordinal);
			return ValueUtilsSmi.GetSqlInt64(this._readerEventSink, this._currentColumnValuesV3, ordinal, this._currentMetaData[ordinal]);
		}

		// Token: 0x06001C86 RID: 7302 RVA: 0x000C9448 File Offset: 0x000C8848
		public override SqlSingle GetSqlSingle(int ordinal)
		{
			this.EnsureCanGetCol("GetSqlSingle", ordinal);
			return ValueUtilsSmi.GetSqlSingle(this._readerEventSink, this._currentColumnValuesV3, ordinal, this._currentMetaData[ordinal]);
		}

		// Token: 0x06001C87 RID: 7303 RVA: 0x000C947C File Offset: 0x000C887C
		public override SqlDouble GetSqlDouble(int ordinal)
		{
			this.EnsureCanGetCol("GetSqlDouble", ordinal);
			return ValueUtilsSmi.GetSqlDouble(this._readerEventSink, this._currentColumnValuesV3, ordinal, this._currentMetaData[ordinal]);
		}

		// Token: 0x06001C88 RID: 7304 RVA: 0x000C94B0 File Offset: 0x000C88B0
		public override SqlMoney GetSqlMoney(int ordinal)
		{
			this.EnsureCanGetCol("GetSqlMoney", ordinal);
			return ValueUtilsSmi.GetSqlMoney(this._readerEventSink, this._currentColumnValuesV3, ordinal, this._currentMetaData[ordinal]);
		}

		// Token: 0x06001C89 RID: 7305 RVA: 0x000C94E4 File Offset: 0x000C88E4
		public override SqlDateTime GetSqlDateTime(int ordinal)
		{
			this.EnsureCanGetCol("GetSqlDateTime", ordinal);
			return ValueUtilsSmi.GetSqlDateTime(this._readerEventSink, this._currentColumnValuesV3, ordinal, this._currentMetaData[ordinal]);
		}

		// Token: 0x06001C8A RID: 7306 RVA: 0x000C9518 File Offset: 0x000C8918
		public override SqlDecimal GetSqlDecimal(int ordinal)
		{
			this.EnsureCanGetCol("GetSqlDecimal", ordinal);
			return ValueUtilsSmi.GetSqlDecimal(this._readerEventSink, this._currentColumnValuesV3, ordinal, this._currentMetaData[ordinal]);
		}

		// Token: 0x06001C8B RID: 7307 RVA: 0x000C954C File Offset: 0x000C894C
		public override SqlString GetSqlString(int ordinal)
		{
			this.EnsureCanGetCol("GetSqlString", ordinal);
			return ValueUtilsSmi.GetSqlString(this._readerEventSink, this._currentColumnValuesV3, ordinal, this._currentMetaData[ordinal]);
		}

		// Token: 0x06001C8C RID: 7308 RVA: 0x000C9580 File Offset: 0x000C8980
		public override SqlGuid GetSqlGuid(int ordinal)
		{
			this.EnsureCanGetCol("GetSqlGuid", ordinal);
			return ValueUtilsSmi.GetSqlGuid(this._readerEventSink, this._currentColumnValuesV3, ordinal, this._currentMetaData[ordinal]);
		}

		// Token: 0x06001C8D RID: 7309 RVA: 0x000C95B4 File Offset: 0x000C89B4
		public override SqlChars GetSqlChars(int ordinal)
		{
			this.EnsureCanGetCol("GetSqlChars", ordinal);
			return ValueUtilsSmi.GetSqlChars(this._readerEventSink, this._currentColumnValuesV3, ordinal, this._currentMetaData[ordinal], this._currentConnection.InternalContext);
		}

		// Token: 0x06001C8E RID: 7310 RVA: 0x000C95F4 File Offset: 0x000C89F4
		public override SqlBytes GetSqlBytes(int ordinal)
		{
			this.EnsureCanGetCol("GetSqlBytes", ordinal);
			return ValueUtilsSmi.GetSqlBytes(this._readerEventSink, this._currentColumnValuesV3, ordinal, this._currentMetaData[ordinal], this._currentConnection.InternalContext);
		}

		// Token: 0x06001C8F RID: 7311 RVA: 0x000C9634 File Offset: 0x000C8A34
		public override SqlXml GetSqlXml(int ordinal)
		{
			this.EnsureCanGetCol("GetSqlXml", ordinal);
			return ValueUtilsSmi.GetSqlXml(this._readerEventSink, this._currentColumnValuesV3, ordinal, this._currentMetaData[ordinal], this._currentConnection.InternalContext);
		}

		// Token: 0x06001C90 RID: 7312 RVA: 0x000C9674 File Offset: 0x000C8A74
		public override TimeSpan GetTimeSpan(int ordinal)
		{
			this.EnsureCanGetCol("GetTimeSpan", ordinal);
			return ValueUtilsSmi.GetTimeSpan(this._readerEventSink, this._currentColumnValuesV3, ordinal, this._currentMetaData[ordinal], this._currentConnection.IsKatmaiOrNewer);
		}

		// Token: 0x06001C91 RID: 7313 RVA: 0x000C96B4 File Offset: 0x000C8AB4
		public override DateTimeOffset GetDateTimeOffset(int ordinal)
		{
			this.EnsureCanGetCol("GetDateTimeOffset", ordinal);
			return ValueUtilsSmi.GetDateTimeOffset(this._readerEventSink, this._currentColumnValuesV3, ordinal, this._currentMetaData[ordinal], this._currentConnection.IsKatmaiOrNewer);
		}

		// Token: 0x06001C92 RID: 7314 RVA: 0x000C96F4 File Offset: 0x000C8AF4
		public override object GetSqlValue(int ordinal)
		{
			this.EnsureCanGetCol("GetSqlValue", ordinal);
			SmiMetaData metaData = this._currentMetaData[ordinal];
			if (this._currentConnection.IsKatmaiOrNewer)
			{
				return ValueUtilsSmi.GetSqlValue200(this._readerEventSink, (SmiTypedGetterSetter)this._currentColumnValuesV3, ordinal, metaData, this._currentConnection.InternalContext);
			}
			return ValueUtilsSmi.GetSqlValue(this._readerEventSink, this._currentColumnValuesV3, ordinal, metaData, this._currentConnection.InternalContext);
		}

		// Token: 0x06001C93 RID: 7315 RVA: 0x000C9768 File Offset: 0x000C8B68
		public override int GetSqlValues(object[] values)
		{
			this.EnsureCanGetCol("GetSqlValues", 0);
			if (values == null)
			{
				throw ADP.ArgumentNull("values");
			}
			int num = (values.Length < this._visibleColumnCount) ? values.Length : this._visibleColumnCount;
			for (int i = 0; i < num; i++)
			{
				values[this._indexMap[i]] = this.GetSqlValue(i);
			}
			return num;
		}

		// Token: 0x1700045B RID: 1115
		// (get) Token: 0x06001C94 RID: 7316 RVA: 0x000C97C4 File Offset: 0x000C8BC4
		public override bool HasRows
		{
			get
			{
				return this._hasRows;
			}
		}

		// Token: 0x06001C95 RID: 7317 RVA: 0x000C97D8 File Offset: 0x000C8BD8
		public override Stream GetStream(int ordinal)
		{
			this.EnsureCanGetCol("GetStream", ordinal);
			SmiQueryMetaData smiQueryMetaData = this._currentMetaData[ordinal];
			if (smiQueryMetaData.SqlDbType == SqlDbType.Variant || !base.IsCommandBehavior(CommandBehavior.SequentialAccess) || ValueUtilsSmi.IsDBNull(this._readerEventSink, this._currentColumnValuesV3, ordinal))
			{
				return ValueUtilsSmi.GetStream(this._readerEventSink, this._currentColumnValuesV3, ordinal, smiQueryMetaData, false);
			}
			if (this.HasActiveStreamOrTextReaderOnColumn(ordinal))
			{
				throw ADP.NonSequentialColumnAccess(ordinal, ordinal + 1);
			}
			this._currentStream = ValueUtilsSmi.GetSequentialStream(this._readerEventSink, this._currentColumnValuesV3, ordinal, smiQueryMetaData, false);
			return this._currentStream;
		}

		// Token: 0x06001C96 RID: 7318 RVA: 0x000C986C File Offset: 0x000C8C6C
		public override TextReader GetTextReader(int ordinal)
		{
			this.EnsureCanGetCol("GetTextReader", ordinal);
			SmiQueryMetaData smiQueryMetaData = this._currentMetaData[ordinal];
			if (smiQueryMetaData.SqlDbType == SqlDbType.Variant || !base.IsCommandBehavior(CommandBehavior.SequentialAccess) || ValueUtilsSmi.IsDBNull(this._readerEventSink, this._currentColumnValuesV3, ordinal))
			{
				return ValueUtilsSmi.GetTextReader(this._readerEventSink, this._currentColumnValuesV3, ordinal, smiQueryMetaData);
			}
			if (this.HasActiveStreamOrTextReaderOnColumn(ordinal))
			{
				throw ADP.NonSequentialColumnAccess(ordinal, ordinal + 1);
			}
			this._currentTextReader = ValueUtilsSmi.GetSequentialTextReader(this._readerEventSink, this._currentColumnValuesV3, ordinal, smiQueryMetaData);
			return this._currentTextReader;
		}

		// Token: 0x06001C97 RID: 7319 RVA: 0x000C98FC File Offset: 0x000C8CFC
		public override XmlReader GetXmlReader(int ordinal)
		{
			this.EnsureCanGetCol("GetXmlReader", ordinal);
			if (this._currentMetaData[ordinal].SqlDbType != SqlDbType.Xml)
			{
				throw ADP.InvalidCast();
			}
			Stream stream;
			if (base.IsCommandBehavior(CommandBehavior.SequentialAccess) && !ValueUtilsSmi.IsDBNull(this._readerEventSink, this._currentColumnValuesV3, ordinal))
			{
				if (this.HasActiveStreamOrTextReaderOnColumn(ordinal))
				{
					throw ADP.NonSequentialColumnAccess(ordinal, ordinal + 1);
				}
				this._currentStream = ValueUtilsSmi.GetSequentialStream(this._readerEventSink, this._currentColumnValuesV3, ordinal, this._currentMetaData[ordinal], true);
				stream = this._currentStream;
			}
			else
			{
				stream = ValueUtilsSmi.GetStream(this._readerEventSink, this._currentColumnValuesV3, ordinal, this._currentMetaData[ordinal], true);
			}
			return SqlXml.CreateSqlXmlReader(stream, false, false);
		}

		// Token: 0x06001C98 RID: 7320 RVA: 0x000C99B0 File Offset: 0x000C8DB0
		internal SqlDataReaderSmi(SmiEventStream eventStream, SqlCommand parent, CommandBehavior behavior, SqlInternalConnectionSmi connection, SmiEventSink parentSink, SmiRequestExecutor requestExecutor) : base(parent, behavior)
		{
			this._eventStream = eventStream;
			this._currentConnection = connection;
			this._readerEventSink = new SqlDataReaderSmi.ReaderEventSink(this, parentSink);
			this._currentPosition = SqlDataReaderSmi.PositionState.BeforeResults;
			this._isOpen = true;
			this._indexMap = null;
			this._visibleColumnCount = 0;
			this._currentStream = null;
			this._currentTextReader = null;
			this._requestExecutor = requestExecutor;
		}

		// Token: 0x06001C99 RID: 7321 RVA: 0x000C9A14 File Offset: 0x000C8E14
		internal override SmiExtendedMetaData[] GetInternalSmiMetaData()
		{
			if (this._currentMetaData == null || this._visibleColumnCount == this.InternalFieldCount)
			{
				return this._currentMetaData;
			}
			SmiExtendedMetaData[] array = new SmiExtendedMetaData[this._visibleColumnCount];
			for (int i = 0; i < this._visibleColumnCount; i++)
			{
				array[i] = this._currentMetaData[this._indexMap[i]];
			}
			return array;
		}

		// Token: 0x06001C9A RID: 7322 RVA: 0x000C9A70 File Offset: 0x000C8E70
		internal override int GetLocaleId(int ordinal)
		{
			this.EnsureCanGetMetaData("GetLocaleId");
			return (int)this._currentMetaData[ordinal].LocaleId;
		}

		// Token: 0x1700045C RID: 1116
		// (get) Token: 0x06001C9B RID: 7323 RVA: 0x000C9A98 File Offset: 0x000C8E98
		private int InternalFieldCount
		{
			get
			{
				if (this.FNotInResults())
				{
					return 0;
				}
				return this._currentMetaData.Length;
			}
		}

		// Token: 0x06001C9C RID: 7324 RVA: 0x000C9AB8 File Offset: 0x000C8EB8
		private bool IsReallyClosed()
		{
			return !this._isOpen;
		}

		// Token: 0x06001C9D RID: 7325 RVA: 0x000C9AD0 File Offset: 0x000C8ED0
		internal void ThrowIfClosed(string operationName)
		{
			if (this.IsClosed)
			{
				throw ADP.DataReaderClosed(operationName);
			}
		}

		// Token: 0x06001C9E RID: 7326 RVA: 0x000C9AEC File Offset: 0x000C8EEC
		private void EnsureCanGetCol(string operationName, int ordinal)
		{
			this.EnsureOnRow(operationName);
		}

		// Token: 0x06001C9F RID: 7327 RVA: 0x000C9B00 File Offset: 0x000C8F00
		internal void EnsureOnRow(string operationName)
		{
			this.ThrowIfClosed(operationName);
			if (this._currentPosition != SqlDataReaderSmi.PositionState.OnRow)
			{
				throw SQL.InvalidRead();
			}
		}

		// Token: 0x06001CA0 RID: 7328 RVA: 0x000C9B24 File Offset: 0x000C8F24
		internal void EnsureCanGetMetaData(string operationName)
		{
			this.ThrowIfClosed(operationName);
			if (this.FNotInResults())
			{
				throw SQL.InvalidRead();
			}
		}

		// Token: 0x06001CA1 RID: 7329 RVA: 0x000C9B48 File Offset: 0x000C8F48
		private bool FInResults()
		{
			return !this.FNotInResults();
		}

		// Token: 0x06001CA2 RID: 7330 RVA: 0x000C9B60 File Offset: 0x000C8F60
		private bool FNotInResults()
		{
			return SqlDataReaderSmi.PositionState.AfterResults == this._currentPosition || this._currentPosition == SqlDataReaderSmi.PositionState.BeforeResults;
		}

		// Token: 0x06001CA3 RID: 7331 RVA: 0x000C9B84 File Offset: 0x000C8F84
		private void MetaDataAvailable(SmiQueryMetaData[] md, bool nextEventIsRow)
		{
			this._currentMetaData = md;
			this._hasRows = nextEventIsRow;
			this._fieldNameLookup = null;
			this._schemaTable = null;
			this._currentPosition = SqlDataReaderSmi.PositionState.BeforeRows;
			this._indexMap = new int[this._currentMetaData.Length];
			int num = 0;
			for (int i = 0; i < this._currentMetaData.Length; i++)
			{
				if (!this._currentMetaData[i].IsHidden.IsTrue)
				{
					this._indexMap[num] = i;
					num++;
				}
			}
			this._visibleColumnCount = num;
		}

		// Token: 0x06001CA4 RID: 7332 RVA: 0x000C9C08 File Offset: 0x000C9008
		private bool HasActiveStreamOrTextReaderOnColumn(int columnIndex)
		{
			bool flag = false;
			flag |= (this._currentStream != null && this._currentStream.ColumnIndex == columnIndex);
			return flag | (this._currentTextReader != null && this._currentTextReader.ColumnIndex == columnIndex);
		}

		// Token: 0x06001CA5 RID: 7333 RVA: 0x000C9C50 File Offset: 0x000C9050
		private void RowAvailable(ITypedGetters row)
		{
			this._currentColumnValues = row;
			this._currentPosition = SqlDataReaderSmi.PositionState.OnRow;
		}

		// Token: 0x06001CA6 RID: 7334 RVA: 0x000C9C6C File Offset: 0x000C906C
		private void RowAvailable(ITypedGettersV3 row)
		{
			this._currentColumnValuesV3 = row;
			this._currentPosition = SqlDataReaderSmi.PositionState.OnRow;
		}

		// Token: 0x06001CA7 RID: 7335 RVA: 0x000C9C88 File Offset: 0x000C9088
		private void StatementCompleted()
		{
			this._currentPosition = SqlDataReaderSmi.PositionState.AfterRows;
		}

		// Token: 0x06001CA8 RID: 7336 RVA: 0x000C9C9C File Offset: 0x000C909C
		private void ResetResultSet()
		{
			this._currentMetaData = null;
			this._visibleColumnCount = 0;
			this._schemaTable = null;
		}

		// Token: 0x06001CA9 RID: 7337 RVA: 0x000C9CC0 File Offset: 0x000C90C0
		private void BatchCompleted()
		{
			this.ResetResultSet();
			this._currentPosition = SqlDataReaderSmi.PositionState.AfterResults;
			this._eventStream.Close(this._readerEventSink);
		}

		// Token: 0x04001036 RID: 4150
		private SqlDataReaderSmi.PositionState _currentPosition;

		// Token: 0x04001037 RID: 4151
		private bool _isOpen;

		// Token: 0x04001038 RID: 4152
		private SmiQueryMetaData[] _currentMetaData;

		// Token: 0x04001039 RID: 4153
		private int[] _indexMap;

		// Token: 0x0400103A RID: 4154
		private int _visibleColumnCount;

		// Token: 0x0400103B RID: 4155
		private DataTable _schemaTable;

		// Token: 0x0400103C RID: 4156
		private ITypedGetters _currentColumnValues;

		// Token: 0x0400103D RID: 4157
		private ITypedGettersV3 _currentColumnValuesV3;

		// Token: 0x0400103E RID: 4158
		private bool _hasRows;

		// Token: 0x0400103F RID: 4159
		private SmiEventStream _eventStream;

		// Token: 0x04001040 RID: 4160
		private SmiRequestExecutor _requestExecutor;

		// Token: 0x04001041 RID: 4161
		private SqlInternalConnectionSmi _currentConnection;

		// Token: 0x04001042 RID: 4162
		private SqlDataReaderSmi.ReaderEventSink _readerEventSink;

		// Token: 0x04001043 RID: 4163
		private FieldNameLookup _fieldNameLookup;

		// Token: 0x04001044 RID: 4164
		private SqlSequentialStreamSmi _currentStream;

		// Token: 0x04001045 RID: 4165
		private SqlSequentialTextReaderSmi _currentTextReader;

		// Token: 0x020003BE RID: 958
		internal enum PositionState
		{
			// Token: 0x040020C0 RID: 8384
			BeforeResults,
			// Token: 0x040020C1 RID: 8385
			BeforeRows,
			// Token: 0x040020C2 RID: 8386
			OnRow,
			// Token: 0x040020C3 RID: 8387
			AfterRows,
			// Token: 0x040020C4 RID: 8388
			AfterResults
		}

		// Token: 0x020003BF RID: 959
		private sealed class ReaderEventSink : SmiEventSink_Default
		{
			// Token: 0x0600350B RID: 13579 RVA: 0x00143B0C File Offset: 0x00142F0C
			internal ReaderEventSink(SqlDataReaderSmi reader, SmiEventSink parent) : base(parent)
			{
				this.reader = reader;
			}

			// Token: 0x0600350C RID: 13580 RVA: 0x00143B28 File Offset: 0x00142F28
			internal override void MetaDataAvailable(SmiQueryMetaData[] md, bool nextEventIsRow)
			{
				if (Bid.AdvancedOn)
				{
					Bid.Trace("<sc.SqlDataReaderSmi.ReaderEventSink.MetaDataAvailable|ADV> %d#, md.Length=%d nextEventIsRow=%d.\n", this.reader.ObjectID, (md != null) ? md.Length : -1, nextEventIsRow);
					if (md != null)
					{
						for (int i = 0; i < md.Length; i++)
						{
							Bid.Trace("<sc.SqlDataReaderSmi.ReaderEventSink.MetaDataAvailable|ADV> %d#, metaData[%d] is %ls%ls\n", this.reader.ObjectID, i, md[i].GetType().ToString(), md[i].TraceString());
						}
					}
				}
				this.reader.MetaDataAvailable(md, nextEventIsRow);
			}

			// Token: 0x0600350D RID: 13581 RVA: 0x00143BA4 File Offset: 0x00142FA4
			internal override void RowAvailable(ITypedGetters row)
			{
				if (Bid.AdvancedOn)
				{
					Bid.Trace("<sc.SqlDataReaderSmi.ReaderEventSink.RowAvailable|ADV> %d# (v2).\n", this.reader.ObjectID);
				}
				this.reader.RowAvailable(row);
			}

			// Token: 0x0600350E RID: 13582 RVA: 0x00143BDC File Offset: 0x00142FDC
			internal override void RowAvailable(ITypedGettersV3 row)
			{
				if (Bid.AdvancedOn)
				{
					Bid.Trace("<sc.SqlDataReaderSmi.ReaderEventSink.RowAvailable|ADV> %d# (ITypedGettersV3).\n", this.reader.ObjectID);
				}
				this.reader.RowAvailable(row);
			}

			// Token: 0x0600350F RID: 13583 RVA: 0x00143C14 File Offset: 0x00143014
			internal override void RowAvailable(SmiTypedGetterSetter rowData)
			{
				if (Bid.AdvancedOn)
				{
					Bid.Trace("<sc.SqlDataReaderSmi.ReaderEventSink.RowAvailable|ADV> %d# (SmiTypedGetterSetter).\n", this.reader.ObjectID);
				}
				this.reader.RowAvailable(rowData);
			}

			// Token: 0x06003510 RID: 13584 RVA: 0x00143C4C File Offset: 0x0014304C
			internal override void StatementCompleted(int recordsAffected)
			{
				if (Bid.AdvancedOn)
				{
					Bid.Trace("<sc.SqlDataReaderSmi.ReaderEventSink.StatementCompleted|ADV> %d# recordsAffected=%d.\n", this.reader.ObjectID, recordsAffected);
				}
				base.StatementCompleted(recordsAffected);
				this.reader.StatementCompleted();
			}

			// Token: 0x06003511 RID: 13585 RVA: 0x00143C88 File Offset: 0x00143088
			internal override void BatchCompleted()
			{
				if (Bid.AdvancedOn)
				{
					Bid.Trace("<sc.SqlDataReaderSmi.ReaderEventSink.BatchCompleted|ADV> %d#.\n", this.reader.ObjectID);
				}
				base.BatchCompleted();
				this.reader.BatchCompleted();
			}

			// Token: 0x040020C5 RID: 8389
			private readonly SqlDataReaderSmi reader;
		}
	}
}
