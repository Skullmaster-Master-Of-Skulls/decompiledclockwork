using System;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Reflection;

namespace System.Data.Common
{
	// Token: 0x020002D7 RID: 727
	[TypeConverter(typeof(DataColumnMapping.DataColumnMappingConverter))]
	public sealed class DataColumnMapping : MarshalByRefObject, IColumnMapping, ICloneable
	{
		// Token: 0x06002D1E RID: 11550 RVA: 0x001230BC File Offset: 0x001224BC
		public DataColumnMapping()
		{
		}

		// Token: 0x06002D1F RID: 11551 RVA: 0x001230D0 File Offset: 0x001224D0
		public DataColumnMapping(string sourceColumn, string dataSetColumn)
		{
			this.SourceColumn = sourceColumn;
			this.DataSetColumn = dataSetColumn;
		}

		// Token: 0x17000734 RID: 1844
		// (get) Token: 0x06002D20 RID: 11552 RVA: 0x001230F4 File Offset: 0x001224F4
		// (set) Token: 0x06002D21 RID: 11553 RVA: 0x00123114 File Offset: 0x00122514
		[ResDescription("DataColumnMapping_DataSetColumn")]
		[DefaultValue("")]
		[ResCategory("DataCategory_Mapping")]
		public string DataSetColumn
		{
			get
			{
				string dataSetColumnName = this._dataSetColumnName;
				if (dataSetColumnName == null)
				{
					return ADP.StrEmpty;
				}
				return dataSetColumnName;
			}
			set
			{
				this._dataSetColumnName = value;
			}
		}

		// Token: 0x17000735 RID: 1845
		// (get) Token: 0x06002D22 RID: 11554 RVA: 0x00123128 File Offset: 0x00122528
		// (set) Token: 0x06002D23 RID: 11555 RVA: 0x0012313C File Offset: 0x0012253C
		internal DataColumnMappingCollection Parent
		{
			get
			{
				return this.parent;
			}
			set
			{
				this.parent = value;
			}
		}

		// Token: 0x17000736 RID: 1846
		// (get) Token: 0x06002D24 RID: 11556 RVA: 0x00123150 File Offset: 0x00122550
		// (set) Token: 0x06002D25 RID: 11557 RVA: 0x00123170 File Offset: 0x00122570
		[DefaultValue("")]
		[ResCategory("DataCategory_Mapping")]
		[ResDescription("DataColumnMapping_SourceColumn")]
		public string SourceColumn
		{
			get
			{
				string sourceColumnName = this._sourceColumnName;
				if (sourceColumnName == null)
				{
					return ADP.StrEmpty;
				}
				return sourceColumnName;
			}
			set
			{
				if (this.Parent != null && ADP.SrcCompare(this._sourceColumnName, value) != 0)
				{
					this.Parent.ValidateSourceColumn(-1, value);
				}
				this._sourceColumnName = value;
			}
		}

		// Token: 0x06002D26 RID: 11558 RVA: 0x001231A8 File Offset: 0x001225A8
		object ICloneable.Clone()
		{
			return new DataColumnMapping
			{
				_sourceColumnName = this._sourceColumnName,
				_dataSetColumnName = this._dataSetColumnName
			};
		}

		// Token: 0x06002D27 RID: 11559 RVA: 0x001231D4 File Offset: 0x001225D4
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public DataColumn GetDataColumnBySchemaAction(DataTable dataTable, Type dataType, MissingSchemaAction schemaAction)
		{
			return DataColumnMapping.GetDataColumnBySchemaAction(this.SourceColumn, this.DataSetColumn, dataTable, dataType, schemaAction);
		}

		// Token: 0x06002D28 RID: 11560 RVA: 0x001231F8 File Offset: 0x001225F8
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static DataColumn GetDataColumnBySchemaAction(string sourceColumn, string dataSetColumn, DataTable dataTable, Type dataType, MissingSchemaAction schemaAction)
		{
			if (dataTable == null)
			{
				throw ADP.ArgumentNull("dataTable");
			}
			if (ADP.IsEmpty(dataSetColumn))
			{
				return null;
			}
			DataColumnCollection columns = dataTable.Columns;
			int num = columns.IndexOf(dataSetColumn);
			if (0 > num || num >= columns.Count)
			{
				return DataColumnMapping.CreateDataColumnBySchemaAction(sourceColumn, dataSetColumn, dataTable, dataType, schemaAction);
			}
			DataColumn dataColumn = columns[num];
			if (!ADP.IsEmpty(dataColumn.Expression))
			{
				throw ADP.ColumnSchemaExpression(sourceColumn, dataSetColumn);
			}
			if (null == dataType || dataType.IsArray == dataColumn.DataType.IsArray)
			{
				return dataColumn;
			}
			throw ADP.ColumnSchemaMismatch(sourceColumn, dataType, dataColumn);
		}

		// Token: 0x06002D29 RID: 11561 RVA: 0x00123288 File Offset: 0x00122688
		internal static DataColumn CreateDataColumnBySchemaAction(string sourceColumn, string dataSetColumn, DataTable dataTable, Type dataType, MissingSchemaAction schemaAction)
		{
			if (ADP.IsEmpty(dataSetColumn))
			{
				return null;
			}
			switch (schemaAction)
			{
			case MissingSchemaAction.Add:
			case MissingSchemaAction.AddWithKey:
				return new DataColumn(dataSetColumn, dataType);
			case MissingSchemaAction.Ignore:
				return null;
			case MissingSchemaAction.Error:
				throw ADP.ColumnSchemaMissing(dataSetColumn, dataTable.TableName, sourceColumn);
			default:
				throw ADP.InvalidMissingSchemaAction(schemaAction);
			}
		}

		// Token: 0x06002D2A RID: 11562 RVA: 0x001232DC File Offset: 0x001226DC
		public override string ToString()
		{
			return this.SourceColumn;
		}

		// Token: 0x04001C3D RID: 7229
		private DataColumnMappingCollection parent;

		// Token: 0x04001C3E RID: 7230
		private string _dataSetColumnName;

		// Token: 0x04001C3F RID: 7231
		private string _sourceColumnName;

		// Token: 0x02000433 RID: 1075
		internal sealed class DataColumnMappingConverter : ExpandableObjectConverter
		{
			// Token: 0x0600362A RID: 13866 RVA: 0x00148F6C File Offset: 0x0014836C
			public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
			{
				return typeof(InstanceDescriptor) == destinationType || base.CanConvertTo(context, destinationType);
			}

			// Token: 0x0600362B RID: 13867 RVA: 0x00148F98 File Offset: 0x00148398
			public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
			{
				if (null == destinationType)
				{
					throw ADP.ArgumentNull("destinationType");
				}
				if (typeof(InstanceDescriptor) == destinationType && value is DataColumnMapping)
				{
					DataColumnMapping dataColumnMapping = (DataColumnMapping)value;
					object[] arguments = new object[]
					{
						dataColumnMapping.SourceColumn,
						dataColumnMapping.DataSetColumn
					};
					Type[] types = new Type[]
					{
						typeof(string),
						typeof(string)
					};
					ConstructorInfo constructor = typeof(DataColumnMapping).GetConstructor(types);
					return new InstanceDescriptor(constructor, arguments);
				}
				return base.ConvertTo(context, culture, value, destinationType);
			}
		}
	}
}
