using System;
using System.Data.Common;
using System.Data.ProviderBase;
using System.Data.SqlTypes;
using System.Globalization;
using Microsoft.SqlServer.Server;

namespace System.Data.SqlClient
{
	// Token: 0x020002E0 RID: 736
	internal sealed class SqlDataReaderSmi : SqlDataReader
	{
		// Token: 0x17000604 RID: 1540
		// (get) Token: 0x0600262A RID: 9770 RVA: 0x002A1388 File Offset: 0x002A0788
		public override int FieldCount
		{
			get
			{
				this.ThrowIfClosed("FieldCount");
				return this.InternalFieldCount;
			}
		}

		// Token: 0x17000605 RID: 1541
		// (get) Token: 0x0600262B RID: 9771 RVA: 0x002A13A8 File Offset: 0x002A07A8
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

		// Token: 0x0600262C RID: 9772 RVA: 0x002A13D8 File Offset: 0x002A07D8
		public override string GetName(int ordinal)
		{
			this.EnsureCanGetMetaData("GetName");
			return this._currentMetaData[ordinal].Name;
		}

		// Token: 0x0600262D RID: 9773 RVA: 0x002A1408 File Offset: 0x002A0808
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

		// Token: 0x0600262E RID: 9774 RVA: 0x002A1478 File Offset: 0x002A0878
		public override Type GetFieldType(int ordinal)
		{
			this.EnsureCanGetMetaData("GetFieldType");
			if (SqlDbType.Udt == this._currentMetaData[ordinal].SqlDbType)
			{
				return this._currentMetaData[ordinal].Type;
			}
			return MetaType.GetMetaTypeFromSqlDbType(this._currentMetaData[ordinal].SqlDbType, this._currentMetaData[ordinal].IsMultiValued).ClassType;
		}

		// Token: 0x0600262F RID: 9775 RVA: 0x002A14D8 File Offset: 0x002A08D8
		public override Type GetProviderSpecificFieldType(int ordinal)
		{
			this.EnsureCanGetMetaData("GetProviderSpecificFieldType");
			if (SqlDbType.Udt == this._currentMetaData[ordinal].SqlDbType)
			{
				return this._currentMetaData[ordinal].Type;
			}
			return MetaType.GetMetaTypeFromSqlDbType(this._currentMetaData[ordinal].SqlDbType, this._currentMetaData[ordinal].IsMultiValued).SqlType;
		}

		// Token: 0x17000606 RID: 1542
		// (get) Token: 0x06002630 RID: 9776 RVA: 0x002A1538 File Offset: 0x002A0938
		public override int Depth
		{
			get
			{
				this.ThrowIfClosed("Depth");
				return 0;
			}
		}

		// Token: 0x06002631 RID: 9777 RVA: 0x002A1558 File Offset: 0x002A0958
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

		// Token: 0x06002632 RID: 9778 RVA: 0x002A15D8 File Offset: 0x002A09D8
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

		// Token: 0x06002633 RID: 9779 RVA: 0x002A1638 File Offset: 0x002A0A38
		public override int GetOrdinal(string name)
		{
			this.EnsureCanGetMetaData("GetOrdinal");
			if (this._fieldNameLookup == null)
			{
				this._fieldNameLookup = new FieldNameLookup(this, -1);
			}
			return this._fieldNameLookup.GetOrdinal(name);
		}

		// Token: 0x17000607 RID: 1543
		public override object this[int ordinal]
		{
			get
			{
				return this.GetValue(ordinal);
			}
		}

		// Token: 0x17000608 RID: 1544
		public override object this[string strName]
		{
			get
			{
				return this.GetValue(this.GetOrdinal(strName));
			}
		}

		// Token: 0x06002636 RID: 9782 RVA: 0x002A16B8 File Offset: 0x002A0AB8
		public override bool IsDBNull(int ordinal)
		{
			this.EnsureCanGetCol("IsDBNull", ordinal);
			return ValueUtilsSmi.IsDBNull(this._readerEventSink, this._currentColumnValuesV3, ordinal);
		}

		// Token: 0x06002637 RID: 9783 RVA: 0x002A16E8 File Offset: 0x002A0AE8
		public override bool GetBoolean(int ordinal)
		{
			this.EnsureCanGetCol("GetBoolean", ordinal);
			return ValueUtilsSmi.GetBoolean(this._readerEventSink, this._currentColumnValuesV3, ordinal, this._currentMetaData[ordinal]);
		}

		// Token: 0x06002638 RID: 9784 RVA: 0x002A1728 File Offset: 0x002A0B28
		public override byte GetByte(int ordinal)
		{
			this.EnsureCanGetCol("GetByte", ordinal);
			return ValueUtilsSmi.GetByte(this._readerEventSink, this._currentColumnValuesV3, ordinal, this._currentMetaData[ordinal]);
		}

		// Token: 0x06002639 RID: 9785 RVA: 0x002A1768 File Offset: 0x002A0B68
		public override long GetBytes(int ordinal, long fieldOffset, byte[] buffer, int bufferOffset, int length)
		{
			this.EnsureCanGetCol("GetBytes", ordinal);
			return ValueUtilsSmi.GetBytes(this._readerEventSink, this._currentColumnValuesV3, ordinal, this._currentMetaData[ordinal], fieldOffset, buffer, bufferOffset, length, true);
		}

		// Token: 0x0600263A RID: 9786 RVA: 0x002A17A8 File Offset: 0x002A0BA8
		internal override long GetBytesInternal(int ordinal, long fieldOffset, byte[] buffer, int bufferOffset, int length)
		{
			this.EnsureCanGetCol("GetBytes", ordinal);
			return ValueUtilsSmi.GetBytesInternal(this._readerEventSink, this._currentColumnValuesV3, ordinal, this._currentMetaData[ordinal], fieldOffset, buffer, bufferOffset, length, false);
		}

		// Token: 0x0600263B RID: 9787 RVA: 0x002A17E8 File Offset: 0x002A0BE8
		public override char GetChar(int ordinal)
		{
			throw ADP.NotSupported();
		}

		// Token: 0x0600263C RID: 9788 RVA: 0x002A1808 File Offset: 0x002A0C08
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

		// Token: 0x0600263D RID: 9789 RVA: 0x002A1868 File Offset: 0x002A0C68
		public override Guid GetGuid(int ordinal)
		{
			this.EnsureCanGetCol("GetGuid", ordinal);
			return ValueUtilsSmi.GetGuid(this._readerEventSink, this._currentColumnValuesV3, ordinal, this._currentMetaData[ordinal]);
		}

		// Token: 0x0600263E RID: 9790 RVA: 0x002A18A8 File Offset: 0x002A0CA8
		public override short GetInt16(int ordinal)
		{
			this.EnsureCanGetCol("GetInt16", ordinal);
			return ValueUtilsSmi.GetInt16(this._readerEventSink, this._currentColumnValuesV3, ordinal, this._currentMetaData[ordinal]);
		}

		// Token: 0x0600263F RID: 9791 RVA: 0x002A18E8 File Offset: 0x002A0CE8
		public override int GetInt32(int ordinal)
		{
			this.EnsureCanGetCol("GetInt32", ordinal);
			return ValueUtilsSmi.GetInt32(this._readerEventSink, this._currentColumnValuesV3, ordinal, this._currentMetaData[ordinal]);
		}

		// Token: 0x06002640 RID: 9792 RVA: 0x002A1928 File Offset: 0x002A0D28
		public override long GetInt64(int ordinal)
		{
			this.EnsureCanGetCol("GetInt64", ordinal);
			return ValueUtilsSmi.GetInt64(this._readerEventSink, this._currentColumnValuesV3, ordinal, this._currentMetaData[ordinal]);
		}

		// Token: 0x06002641 RID: 9793 RVA: 0x002A1968 File Offset: 0x002A0D68
		public override float GetFloat(int ordinal)
		{
			this.EnsureCanGetCol("GetFloat", ordinal);
			return ValueUtilsSmi.GetSingle(this._readerEventSink, this._currentColumnValuesV3, ordinal, this._currentMetaData[ordinal]);
		}

		// Token: 0x06002642 RID: 9794 RVA: 0x002A19A8 File Offset: 0x002A0DA8
		public override double GetDouble(int ordinal)
		{
			this.EnsureCanGetCol("GetDouble", ordinal);
			return ValueUtilsSmi.GetDouble(this._readerEventSink, this._currentColumnValuesV3, ordinal, this._currentMetaData[ordinal]);
		}

		// Token: 0x06002643 RID: 9795 RVA: 0x002A19E8 File Offset: 0x002A0DE8
		public override string GetString(int ordinal)
		{
			this.EnsureCanGetCol("GetString", ordinal);
			return ValueUtilsSmi.GetString(this._readerEventSink, this._currentColumnValuesV3, ordinal, this._currentMetaData[ordinal]);
		}

		// Token: 0x06002644 RID: 9796 RVA: 0x002A1A28 File Offset: 0x002A0E28
		public override decimal GetDecimal(int ordinal)
		{
			this.EnsureCanGetCol("GetDecimal", ordinal);
			return ValueUtilsSmi.GetDecimal(this._readerEventSink, this._currentColumnValuesV3, ordinal, this._currentMetaData[ordinal]);
		}

		// Token: 0x06002645 RID: 9797 RVA: 0x002A1A68 File Offset: 0x002A0E68
		public override DateTime GetDateTime(int ordinal)
		{
			this.EnsureCanGetCol("GetDateTime", ordinal);
			return ValueUtilsSmi.GetDateTime(this._readerEventSink, this._currentColumnValuesV3, ordinal, this._currentMetaData[ordinal]);
		}

		// Token: 0x17000609 RID: 1545
		// (get) Token: 0x06002646 RID: 9798 RVA: 0x002A1AA8 File Offset: 0x002A0EA8
		public override bool IsClosed
		{
			get
			{
				return this.IsReallyClosed();
			}
		}

		// Token: 0x1700060A RID: 1546
		// (get) Token: 0x06002647 RID: 9799 RVA: 0x002A1AC8 File Offset: 0x002A0EC8
		public override int RecordsAffected
		{
			get
			{
				return base.Command.InternalRecordsAffected;
			}
		}

		// Token: 0x06002648 RID: 9800 RVA: 0x002A1AE8 File Offset: 0x002A0EE8
		public override void Close()
		{
			bool flag = base.IsCommandBehavior(CommandBehavior.CloseConnection);
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<sc.SqlDataReaderSmi.Close|API> %d#", this.ObjectID);
			bool flag2 = true;
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
				}
			}
			catch (Exception e)
			{
				flag2 = ADP.IsCatchableExceptionType(e);
				throw;
			}
			finally
			{
				if (flag2)
				{
					this._isOpen = false;
					if (flag)
					{
						if (base.Connection != null)
						{
							base.Connection.Close();
						}
						Bid.ScopeLeave(ref intPtr);
					}
				}
			}
		}

		// Token: 0x06002649 RID: 9801 RVA: 0x002A1BC8 File Offset: 0x002A0FC8
		public override bool NextResult()
		{
			this.ThrowIfClosed("NextResult");
			return this.InternalNextResult(false);
		}

		// Token: 0x0600264A RID: 9802 RVA: 0x002A1BF8 File Offset: 0x002A0FF8
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

		// Token: 0x0600264B RID: 9803 RVA: 0x002A1CB8 File Offset: 0x002A10B8
		public override bool Read()
		{
			this.ThrowIfClosed("Read");
			return this.InternalRead(false);
		}

		// Token: 0x0600264C RID: 9804 RVA: 0x002A1CE8 File Offset: 0x002A10E8
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

		// Token: 0x0600264D RID: 9805 RVA: 0x002A1DB8 File Offset: 0x002A11B8
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

		// Token: 0x0600264E RID: 9806 RVA: 0x002A2728 File Offset: 0x002A1B28
		public override SqlBinary GetSqlBinary(int ordinal)
		{
			this.EnsureCanGetCol("GetSqlBinary", ordinal);
			return ValueUtilsSmi.GetSqlBinary(this._readerEventSink, this._currentColumnValuesV3, ordinal, this._currentMetaData[ordinal]);
		}

		// Token: 0x0600264F RID: 9807 RVA: 0x002A2768 File Offset: 0x002A1B68
		public override SqlBoolean GetSqlBoolean(int ordinal)
		{
			this.EnsureCanGetCol("GetSqlBoolean", ordinal);
			return ValueUtilsSmi.GetSqlBoolean(this._readerEventSink, this._currentColumnValuesV3, ordinal, this._currentMetaData[ordinal]);
		}

		// Token: 0x06002650 RID: 9808 RVA: 0x002A27A8 File Offset: 0x002A1BA8
		public override SqlByte GetSqlByte(int ordinal)
		{
			this.EnsureCanGetCol("GetSqlByte", ordinal);
			return ValueUtilsSmi.GetSqlByte(this._readerEventSink, this._currentColumnValuesV3, ordinal, this._currentMetaData[ordinal]);
		}

		// Token: 0x06002651 RID: 9809 RVA: 0x002A27E8 File Offset: 0x002A1BE8
		public override SqlInt16 GetSqlInt16(int ordinal)
		{
			this.EnsureCanGetCol("GetSqlInt16", ordinal);
			return ValueUtilsSmi.GetSqlInt16(this._readerEventSink, this._currentColumnValuesV3, ordinal, this._currentMetaData[ordinal]);
		}

		// Token: 0x06002652 RID: 9810 RVA: 0x002A2828 File Offset: 0x002A1C28
		public override SqlInt32 GetSqlInt32(int ordinal)
		{
			this.EnsureCanGetCol("GetSqlInt32", ordinal);
			return ValueUtilsSmi.GetSqlInt32(this._readerEventSink, this._currentColumnValuesV3, ordinal, this._currentMetaData[ordinal]);
		}

		// Token: 0x06002653 RID: 9811 RVA: 0x002A2868 File Offset: 0x002A1C68
		public override SqlInt64 GetSqlInt64(int ordinal)
		{
			this.EnsureCanGetCol("GetSqlInt64", ordinal);
			return ValueUtilsSmi.GetSqlInt64(this._readerEventSink, this._currentColumnValuesV3, ordinal, this._currentMetaData[ordinal]);
		}

		// Token: 0x06002654 RID: 9812 RVA: 0x002A28A8 File Offset: 0x002A1CA8
		public override SqlSingle GetSqlSingle(int ordinal)
		{
			this.EnsureCanGetCol("GetSqlSingle", ordinal);
			return ValueUtilsSmi.GetSqlSingle(this._readerEventSink, this._currentColumnValuesV3, ordinal, this._currentMetaData[ordinal]);
		}

		// Token: 0x06002655 RID: 9813 RVA: 0x002A28E8 File Offset: 0x002A1CE8
		public override SqlDouble GetSqlDouble(int ordinal)
		{
			this.EnsureCanGetCol("GetSqlDouble", ordinal);
			return ValueUtilsSmi.GetSqlDouble(this._readerEventSink, this._currentColumnValuesV3, ordinal, this._currentMetaData[ordinal]);
		}

		// Token: 0x06002656 RID: 9814 RVA: 0x002A2928 File Offset: 0x002A1D28
		public override SqlMoney GetSqlMoney(int ordinal)
		{
			this.EnsureCanGetCol("GetSqlMoney", ordinal);
			return ValueUtilsSmi.GetSqlMoney(this._readerEventSink, this._currentColumnValuesV3, ordinal, this._currentMetaData[ordinal]);
		}

		// Token: 0x06002657 RID: 9815 RVA: 0x002A2968 File Offset: 0x002A1D68
		public override SqlDateTime GetSqlDateTime(int ordinal)
		{
			this.EnsureCanGetCol("GetSqlDateTime", ordinal);
			return ValueUtilsSmi.GetSqlDateTime(this._readerEventSink, this._currentColumnValuesV3, ordinal, this._currentMetaData[ordinal]);
		}

		// Token: 0x06002658 RID: 9816 RVA: 0x002A29A8 File Offset: 0x002A1DA8
		public override SqlDecimal GetSqlDecimal(int ordinal)
		{
			this.EnsureCanGetCol("GetSqlDecimal", ordinal);
			return ValueUtilsSmi.GetSqlDecimal(this._readerEventSink, this._currentColumnValuesV3, ordinal, this._currentMetaData[ordinal]);
		}

		// Token: 0x06002659 RID: 9817 RVA: 0x002A29E8 File Offset: 0x002A1DE8
		public override SqlString GetSqlString(int ordinal)
		{
			this.EnsureCanGetCol("GetSqlString", ordinal);
			return ValueUtilsSmi.GetSqlString(this._readerEventSink, this._currentColumnValuesV3, ordinal, this._currentMetaData[ordinal]);
		}

		// Token: 0x0600265A RID: 9818 RVA: 0x002A2A28 File Offset: 0x002A1E28
		public override SqlGuid GetSqlGuid(int ordinal)
		{
			this.EnsureCanGetCol("GetSqlGuid", ordinal);
			return ValueUtilsSmi.GetSqlGuid(this._readerEventSink, this._currentColumnValuesV3, ordinal, this._currentMetaData[ordinal]);
		}

		// Token: 0x0600265B RID: 9819 RVA: 0x002A2A68 File Offset: 0x002A1E68
		public override SqlChars GetSqlChars(int ordinal)
		{
			this.EnsureCanGetCol("GetSqlChars", ordinal);
			return ValueUtilsSmi.GetSqlChars(this._readerEventSink, this._currentColumnValuesV3, ordinal, this._currentMetaData[ordinal], this._currentConnection.InternalContext);
		}

		// Token: 0x0600265C RID: 9820 RVA: 0x002A2AA8 File Offset: 0x002A1EA8
		public override SqlBytes GetSqlBytes(int ordinal)
		{
			this.EnsureCanGetCol("GetSqlBytes", ordinal);
			return ValueUtilsSmi.GetSqlBytes(this._readerEventSink, this._currentColumnValuesV3, ordinal, this._currentMetaData[ordinal], this._currentConnection.InternalContext);
		}

		// Token: 0x0600265D RID: 9821 RVA: 0x002A2AE8 File Offset: 0x002A1EE8
		public override SqlXml GetSqlXml(int ordinal)
		{
			this.EnsureCanGetCol("GetSqlXml", ordinal);
			return ValueUtilsSmi.GetSqlXml(this._readerEventSink, this._currentColumnValuesV3, ordinal, this._currentMetaData[ordinal], this._currentConnection.InternalContext);
		}

		// Token: 0x0600265E RID: 9822 RVA: 0x002A2B28 File Offset: 0x002A1F28
		public override TimeSpan GetTimeSpan(int ordinal)
		{
			this.EnsureCanGetCol("GetTimeSpan", ordinal);
			return ValueUtilsSmi.GetTimeSpan(this._readerEventSink, this._currentColumnValuesV3, ordinal, this._currentMetaData[ordinal], this._currentConnection.IsKatmaiOrNewer);
		}

		// Token: 0x0600265F RID: 9823 RVA: 0x002A2B68 File Offset: 0x002A1F68
		public override DateTimeOffset GetDateTimeOffset(int ordinal)
		{
			this.EnsureCanGetCol("GetDateTimeOffset", ordinal);
			return ValueUtilsSmi.GetDateTimeOffset(this._readerEventSink, this._currentColumnValuesV3, ordinal, this._currentMetaData[ordinal], this._currentConnection.IsKatmaiOrNewer);
		}

		// Token: 0x06002660 RID: 9824 RVA: 0x002A2BA8 File Offset: 0x002A1FA8
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

		// Token: 0x06002661 RID: 9825 RVA: 0x002A2C28 File Offset: 0x002A2028
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

		// Token: 0x1700060B RID: 1547
		// (get) Token: 0x06002662 RID: 9826 RVA: 0x002A2C88 File Offset: 0x002A2088
		public override bool HasRows
		{
			get
			{
				return this._hasRows;
			}
		}

		// Token: 0x06002663 RID: 9827 RVA: 0x002A2CA8 File Offset: 0x002A20A8
		internal SqlDataReaderSmi(SmiEventStream eventStream, SqlCommand parent, CommandBehavior behavior, SqlInternalConnectionSmi connection, SmiEventSink parentSink) : base(parent, behavior)
		{
			this._eventStream = eventStream;
			this._currentConnection = connection;
			this._readerEventSink = new SqlDataReaderSmi.ReaderEventSink(this, parentSink);
			this._currentPosition = SqlDataReaderSmi.PositionState.BeforeResults;
			this._isOpen = true;
			this._indexMap = null;
			this._visibleColumnCount = 0;
		}

		// Token: 0x06002664 RID: 9828 RVA: 0x002A2CF8 File Offset: 0x002A20F8
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

		// Token: 0x06002665 RID: 9829 RVA: 0x002A2D58 File Offset: 0x002A2158
		internal override int GetLocaleId(int ordinal)
		{
			this.EnsureCanGetMetaData("GetLocaleId");
			return (int)this._currentMetaData[ordinal].LocaleId;
		}

		// Token: 0x1700060C RID: 1548
		// (get) Token: 0x06002666 RID: 9830 RVA: 0x002A2D88 File Offset: 0x002A2188
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

		// Token: 0x06002667 RID: 9831 RVA: 0x002A2DA8 File Offset: 0x002A21A8
		private bool IsReallyClosed()
		{
			return !this._isOpen;
		}

		// Token: 0x06002668 RID: 9832 RVA: 0x002A2DC8 File Offset: 0x002A21C8
		internal void ThrowIfClosed(string operationName)
		{
			if (this.IsClosed)
			{
				throw ADP.DataReaderClosed(operationName);
			}
		}

		// Token: 0x06002669 RID: 9833 RVA: 0x002A2DE8 File Offset: 0x002A21E8
		private void EnsureCanGetCol(string operationName, int ordinal)
		{
			this.EnsureOnRow(operationName);
		}

		// Token: 0x0600266A RID: 9834 RVA: 0x002A2E08 File Offset: 0x002A2208
		internal void EnsureOnRow(string operationName)
		{
			this.ThrowIfClosed(operationName);
			if (this._currentPosition != SqlDataReaderSmi.PositionState.OnRow)
			{
				throw SQL.InvalidRead();
			}
		}

		// Token: 0x0600266B RID: 9835 RVA: 0x002A2E38 File Offset: 0x002A2238
		internal void EnsureCanGetMetaData(string operationName)
		{
			this.ThrowIfClosed(operationName);
			if (this.FNotInResults())
			{
				throw SQL.InvalidRead();
			}
		}

		// Token: 0x0600266C RID: 9836 RVA: 0x002A2E68 File Offset: 0x002A2268
		private bool FInResults()
		{
			return !this.FNotInResults();
		}

		// Token: 0x0600266D RID: 9837 RVA: 0x002A2E88 File Offset: 0x002A2288
		private bool FNotInResults()
		{
			return SqlDataReaderSmi.PositionState.AfterResults == this._currentPosition || SqlDataReaderSmi.PositionState.BeforeResults == this._currentPosition;
		}

		// Token: 0x0600266E RID: 9838 RVA: 0x002A2EB8 File Offset: 0x002A22B8
		private void MetaDataAvailable(SmiQueryMetaData[] md, bool nextEventIsRow)
		{
			this._currentMetaData = md;
			this._hasRows = nextEventIsRow;
			this._fieldNameLookup = null;
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

		// Token: 0x0600266F RID: 9839 RVA: 0x002A2F38 File Offset: 0x002A2338
		private void RowAvailable(ITypedGetters row)
		{
			this._currentColumnValues = row;
			this._currentPosition = SqlDataReaderSmi.PositionState.OnRow;
		}

		// Token: 0x06002670 RID: 9840 RVA: 0x002A2F58 File Offset: 0x002A2358
		private void RowAvailable(ITypedGettersV3 row)
		{
			this._currentColumnValuesV3 = row;
			this._currentPosition = SqlDataReaderSmi.PositionState.OnRow;
		}

		// Token: 0x06002671 RID: 9841 RVA: 0x002A2F78 File Offset: 0x002A2378
		private void StatementCompleted()
		{
			this._currentMetaData = null;
			this._visibleColumnCount = 0;
			this._schemaTable = null;
			this._currentPosition = SqlDataReaderSmi.PositionState.AfterRows;
		}

		// Token: 0x06002672 RID: 9842 RVA: 0x002A2FA8 File Offset: 0x002A23A8
		private void BatchCompleted()
		{
			this._currentPosition = SqlDataReaderSmi.PositionState.AfterResults;
			this._eventStream.Close(this._readerEventSink);
		}

		// Token: 0x0400183A RID: 6202
		private SqlDataReaderSmi.PositionState _currentPosition;

		// Token: 0x0400183B RID: 6203
		private bool _isOpen;

		// Token: 0x0400183C RID: 6204
		private SmiQueryMetaData[] _currentMetaData;

		// Token: 0x0400183D RID: 6205
		private int[] _indexMap;

		// Token: 0x0400183E RID: 6206
		private int _visibleColumnCount;

		// Token: 0x0400183F RID: 6207
		private DataTable _schemaTable;

		// Token: 0x04001840 RID: 6208
		private ITypedGetters _currentColumnValues;

		// Token: 0x04001841 RID: 6209
		private ITypedGettersV3 _currentColumnValuesV3;

		// Token: 0x04001842 RID: 6210
		private bool _hasRows;

		// Token: 0x04001843 RID: 6211
		private SmiEventStream _eventStream;

		// Token: 0x04001844 RID: 6212
		private SqlInternalConnectionSmi _currentConnection;

		// Token: 0x04001845 RID: 6213
		private SqlDataReaderSmi.ReaderEventSink _readerEventSink;

		// Token: 0x04001846 RID: 6214
		private FieldNameLookup _fieldNameLookup;

		// Token: 0x020002E1 RID: 737
		internal enum PositionState
		{
			// Token: 0x04001848 RID: 6216
			BeforeResults,
			// Token: 0x04001849 RID: 6217
			BeforeRows,
			// Token: 0x0400184A RID: 6218
			OnRow,
			// Token: 0x0400184B RID: 6219
			AfterRows,
			// Token: 0x0400184C RID: 6220
			AfterResults
		}

		// Token: 0x020002E2 RID: 738
		private sealed class ReaderEventSink : SmiEventSink_Default
		{
			// Token: 0x06002673 RID: 9843 RVA: 0x002A2FD8 File Offset: 0x002A23D8
			internal ReaderEventSink(SqlDataReaderSmi reader, SmiEventSink parent) : base(parent)
			{
				this.reader = reader;
			}

			// Token: 0x06002674 RID: 9844 RVA: 0x002A2FF8 File Offset: 0x002A23F8
			internal override void MetaDataAvailable(SmiQueryMetaData[] md, bool nextEventIsRow)
			{
				if (Bid.AdvancedOn)
				{
					Bid.Trace("<sc.SqlDataReaderSmi.ReaderEventSink.MetaDataAvailable|ADV> %d#, md.Length=%d nextEventIsRow=%d.\n", this.reader.ObjectID, (md != null) ? md.Length : -1, nextEventIsRow);
					if (md != null)
					{
						for (int i = 0; i < md.Length; i++)
						{
							Bid.Trace("<sc.SqlDataReaderSmi.ReaderEventSink.MetaDataAvailable|ADV> %d#, metaData[%d] is %s%s\n", this.reader.ObjectID, i, md[i].GetType().ToString(), md[i].TraceString());
						}
					}
				}
				this.reader.MetaDataAvailable(md, nextEventIsRow);
			}

			// Token: 0x06002675 RID: 9845 RVA: 0x002A3078 File Offset: 0x002A2478
			internal override void RowAvailable(ITypedGetters row)
			{
				if (Bid.AdvancedOn)
				{
					Bid.Trace("<sc.SqlDataReaderSmi.ReaderEventSink.RowAvailable|ADV> %d# (v2).\n", this.reader.ObjectID);
				}
				this.reader.RowAvailable(row);
			}

			// Token: 0x06002676 RID: 9846 RVA: 0x002A30B8 File Offset: 0x002A24B8
			internal override void RowAvailable(ITypedGettersV3 row)
			{
				if (Bid.AdvancedOn)
				{
					Bid.Trace("<sc.SqlDataReaderSmi.ReaderEventSink.RowAvailable|ADV> %d# (ITypedGettersV3).\n", this.reader.ObjectID);
				}
				this.reader.RowAvailable(row);
			}

			// Token: 0x06002677 RID: 9847 RVA: 0x002A30F8 File Offset: 0x002A24F8
			internal override void RowAvailable(SmiTypedGetterSetter rowData)
			{
				if (Bid.AdvancedOn)
				{
					Bid.Trace("<sc.SqlDataReaderSmi.ReaderEventSink.RowAvailable|ADV> %d# (SmiTypedGetterSetter).\n", this.reader.ObjectID);
				}
				this.reader.RowAvailable(rowData);
			}

			// Token: 0x06002678 RID: 9848 RVA: 0x002A3138 File Offset: 0x002A2538
			internal override void StatementCompleted(int recordsAffected)
			{
				if (Bid.AdvancedOn)
				{
					Bid.Trace("<sc.SqlDataReaderSmi.ReaderEventSink.StatementCompleted|ADV> %d# recordsAffected=%d.\n", this.reader.ObjectID, recordsAffected);
				}
				base.StatementCompleted(recordsAffected);
				this.reader.StatementCompleted();
			}

			// Token: 0x06002679 RID: 9849 RVA: 0x002A3178 File Offset: 0x002A2578
			internal override void BatchCompleted()
			{
				if (Bid.AdvancedOn)
				{
					Bid.Trace("<sc.SqlDataReaderSmi.ReaderEventSink.BatchCompleted|ADV> %d#.\n", this.reader.ObjectID);
				}
				base.BatchCompleted();
				this.reader.BatchCompleted();
			}

			// Token: 0x0400184D RID: 6221
			private readonly SqlDataReaderSmi reader;
		}
	}
}
