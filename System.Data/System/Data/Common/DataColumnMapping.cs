using System;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Reflection;

namespace System.Data.Common
{
	// Token: 0x02000119 RID: 281
	[TypeConverter(typeof(DataColumnMapping.DataColumnMappingConverter))]
	public sealed class DataColumnMapping : MarshalByRefObject, IColumnMapping, ICloneable
	{
		// Token: 0x060011DD RID: 4573 RVA: 0x002358A8 File Offset: 0x00234CA8
		public DataColumnMapping()
		{
		}

		// Token: 0x060011DE RID: 4574 RVA: 0x002358C8 File Offset: 0x00234CC8
		public DataColumnMapping(string sourceColumn, string dataSetColumn)
		{
			this.SourceColumn = sourceColumn;
			this.DataSetColumn = dataSetColumn;
		}

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x060011DF RID: 4575 RVA: 0x002358F8 File Offset: 0x00234CF8
		// (set) Token: 0x060011E0 RID: 4576 RVA: 0x00235918 File Offset: 0x00234D18
		[ResCategory("DataCategory_Mapping")]
		[DefaultValue("")]
		[ResDescription("DataColumnMapping_DataSetColumn")]
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

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x060011E1 RID: 4577 RVA: 0x00235938 File Offset: 0x00234D38
		// (set) Token: 0x060011E2 RID: 4578 RVA: 0x00235958 File Offset: 0x00234D58
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

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x060011E3 RID: 4579 RVA: 0x00235978 File Offset: 0x00234D78
		// (set) Token: 0x060011E4 RID: 4580 RVA: 0x00235998 File Offset: 0x00234D98
		[ResCategory("DataCategory_Mapping")]
		[DefaultValue("")]
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

		// Token: 0x060011E5 RID: 4581 RVA: 0x002359D8 File Offset: 0x00234DD8
		object ICloneable.Clone()
		{
			return new DataColumnMapping
			{
				_sourceColumnName = this._sourceColumnName,
				_dataSetColumnName = this._dataSetColumnName
			};
		}

		// Token: 0x060011E6 RID: 4582 RVA: 0x00235A08 File Offset: 0x00234E08
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public DataColumn GetDataColumnBySchemaAction(DataTable dataTable, Type dataType, MissingSchemaAction schemaAction)
		{
			return DataColumnMapping.GetDataColumnBySchemaAction(this.SourceColumn, this.DataSetColumn, dataTable, dataType, schemaAction);
		}

		// Token: 0x060011E7 RID: 4583 RVA: 0x00235A38 File Offset: 0x00234E38
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
			if (0 <= num && num < columns.Count)
			{
				DataColumn dataColumn = columns[num];
				if (!ADP.IsEmpty(dataColumn.Expression))
				{
					throw ADP.ColumnSchemaExpression(sourceColumn, dataSetColumn);
				}
				if (dataType == null || dataType.IsArray == dataColumn.DataType.IsArray)
				{
					return dataColumn;
				}
				throw ADP.ColumnSchemaMismatch(sourceColumn, dataType, dataColumn);
			}
			else
			{
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
		}

		// Token: 0x060011E8 RID: 4584 RVA: 0x00235AF8 File Offset: 0x00234EF8
		public override string ToString()
		{
			return this.SourceColumn;
		}

		// Token: 0x04000B89 RID: 2953
		private DataColumnMappingCollection parent;

		// Token: 0x04000B8A RID: 2954
		private string _dataSetColumnName;

		// Token: 0x04000B8B RID: 2955
		private string _sourceColumnName;

		// Token: 0x0200011A RID: 282
		internal sealed class DataColumnMappingConverter : ExpandableObjectConverter
		{
			// Token: 0x060011EA RID: 4586 RVA: 0x00235B38 File Offset: 0x00234F38
			public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
			{
				return typeof(InstanceDescriptor) == destinationType || base.CanConvertTo(context, destinationType);
			}

			// Token: 0x060011EB RID: 4587 RVA: 0x00235B68 File Offset: 0x00234F68
			public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
			{
				if (destinationType == null)
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
