using System;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Reflection;

namespace System.Data.Common
{
	// Token: 0x020002DB RID: 731
	[TypeConverter(typeof(DataTableMapping.DataTableMappingConverter))]
	public sealed class DataTableMapping : MarshalByRefObject, ITableMapping, ICloneable
	{
		// Token: 0x06002D8E RID: 11662 RVA: 0x001246A4 File Offset: 0x00123AA4
		public DataTableMapping()
		{
		}

		// Token: 0x06002D8F RID: 11663 RVA: 0x001246B8 File Offset: 0x00123AB8
		public DataTableMapping(string sourceTable, string dataSetTable)
		{
			this.SourceTable = sourceTable;
			this.DataSetTable = dataSetTable;
		}

		// Token: 0x06002D90 RID: 11664 RVA: 0x001246DC File Offset: 0x00123ADC
		public DataTableMapping(string sourceTable, string dataSetTable, DataColumnMapping[] columnMappings)
		{
			this.SourceTable = sourceTable;
			this.DataSetTable = dataSetTable;
			if (columnMappings != null && columnMappings.Length != 0)
			{
				this.ColumnMappings.AddRange(columnMappings);
			}
		}

		// Token: 0x17000743 RID: 1859
		// (get) Token: 0x06002D91 RID: 11665 RVA: 0x00124710 File Offset: 0x00123B10
		IColumnMappingCollection ITableMapping.ColumnMappings
		{
			get
			{
				return this.ColumnMappings;
			}
		}

		// Token: 0x17000744 RID: 1860
		// (get) Token: 0x06002D92 RID: 11666 RVA: 0x00124724 File Offset: 0x00123B24
		[ResCategory("DataCategory_Mapping")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[ResDescription("DataTableMapping_ColumnMappings")]
		public DataColumnMappingCollection ColumnMappings
		{
			get
			{
				DataColumnMappingCollection dataColumnMappingCollection = this._columnMappings;
				if (dataColumnMappingCollection == null)
				{
					dataColumnMappingCollection = new DataColumnMappingCollection();
					this._columnMappings = dataColumnMappingCollection;
				}
				return dataColumnMappingCollection;
			}
		}

		// Token: 0x17000745 RID: 1861
		// (get) Token: 0x06002D93 RID: 11667 RVA: 0x0012474C File Offset: 0x00123B4C
		// (set) Token: 0x06002D94 RID: 11668 RVA: 0x0012476C File Offset: 0x00123B6C
		[ResCategory("DataCategory_Mapping")]
		[DefaultValue("")]
		[ResDescription("DataTableMapping_DataSetTable")]
		public string DataSetTable
		{
			get
			{
				string dataSetTableName = this._dataSetTableName;
				if (dataSetTableName == null)
				{
					return ADP.StrEmpty;
				}
				return dataSetTableName;
			}
			set
			{
				this._dataSetTableName = value;
			}
		}

		// Token: 0x17000746 RID: 1862
		// (get) Token: 0x06002D95 RID: 11669 RVA: 0x00124780 File Offset: 0x00123B80
		// (set) Token: 0x06002D96 RID: 11670 RVA: 0x00124794 File Offset: 0x00123B94
		internal DataTableMappingCollection Parent
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

		// Token: 0x17000747 RID: 1863
		// (get) Token: 0x06002D97 RID: 11671 RVA: 0x001247A8 File Offset: 0x00123BA8
		// (set) Token: 0x06002D98 RID: 11672 RVA: 0x001247C8 File Offset: 0x00123BC8
		[DefaultValue("")]
		[ResCategory("DataCategory_Mapping")]
		[ResDescription("DataTableMapping_SourceTable")]
		public string SourceTable
		{
			get
			{
				string sourceTableName = this._sourceTableName;
				if (sourceTableName == null)
				{
					return ADP.StrEmpty;
				}
				return sourceTableName;
			}
			set
			{
				if (this.Parent != null && ADP.SrcCompare(this._sourceTableName, value) != 0)
				{
					this.Parent.ValidateSourceTable(-1, value);
				}
				this._sourceTableName = value;
			}
		}

		// Token: 0x06002D99 RID: 11673 RVA: 0x00124800 File Offset: 0x00123C00
		object ICloneable.Clone()
		{
			DataTableMapping dataTableMapping = new DataTableMapping();
			dataTableMapping._dataSetTableName = this._dataSetTableName;
			dataTableMapping._sourceTableName = this._sourceTableName;
			if (this._columnMappings != null && 0 < this.ColumnMappings.Count)
			{
				DataColumnMappingCollection columnMappings = dataTableMapping.ColumnMappings;
				foreach (object obj in this.ColumnMappings)
				{
					ICloneable cloneable = (ICloneable)obj;
					columnMappings.Add(cloneable.Clone());
				}
			}
			return dataTableMapping;
		}

		// Token: 0x06002D9A RID: 11674 RVA: 0x001248AC File Offset: 0x00123CAC
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public DataColumn GetDataColumn(string sourceColumn, Type dataType, DataTable dataTable, MissingMappingAction mappingAction, MissingSchemaAction schemaAction)
		{
			return DataColumnMappingCollection.GetDataColumn(this._columnMappings, sourceColumn, dataType, dataTable, mappingAction, schemaAction);
		}

		// Token: 0x06002D9B RID: 11675 RVA: 0x001248CC File Offset: 0x00123CCC
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public DataColumnMapping GetColumnMappingBySchemaAction(string sourceColumn, MissingMappingAction mappingAction)
		{
			return DataColumnMappingCollection.GetColumnMappingBySchemaAction(this._columnMappings, sourceColumn, mappingAction);
		}

		// Token: 0x06002D9C RID: 11676 RVA: 0x001248E8 File Offset: 0x00123CE8
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public DataTable GetDataTableBySchemaAction(DataSet dataSet, MissingSchemaAction schemaAction)
		{
			if (dataSet == null)
			{
				throw ADP.ArgumentNull("dataSet");
			}
			string dataSetTable = this.DataSetTable;
			if (ADP.IsEmpty(dataSetTable))
			{
				return null;
			}
			DataTableCollection tables = dataSet.Tables;
			int num = tables.IndexOf(dataSetTable);
			if (0 <= num && num < tables.Count)
			{
				return tables[num];
			}
			switch (schemaAction)
			{
			case MissingSchemaAction.Add:
			case MissingSchemaAction.AddWithKey:
				return new DataTable(dataSetTable);
			case MissingSchemaAction.Ignore:
				return null;
			case MissingSchemaAction.Error:
				throw ADP.MissingTableSchema(dataSetTable, this.SourceTable);
			default:
				throw ADP.InvalidMissingSchemaAction(schemaAction);
			}
		}

		// Token: 0x06002D9D RID: 11677 RVA: 0x00124970 File Offset: 0x00123D70
		public override string ToString()
		{
			return this.SourceTable;
		}

		// Token: 0x04001C79 RID: 7289
		private DataTableMappingCollection parent;

		// Token: 0x04001C7A RID: 7290
		private DataColumnMappingCollection _columnMappings;

		// Token: 0x04001C7B RID: 7291
		private string _dataSetTableName;

		// Token: 0x04001C7C RID: 7292
		private string _sourceTableName;

		// Token: 0x02000434 RID: 1076
		internal sealed class DataTableMappingConverter : ExpandableObjectConverter
		{
			// Token: 0x0600362D RID: 13869 RVA: 0x00149050 File Offset: 0x00148450
			public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
			{
				return typeof(InstanceDescriptor) == destinationType || base.CanConvertTo(context, destinationType);
			}

			// Token: 0x0600362E RID: 13870 RVA: 0x0014907C File Offset: 0x0014847C
			public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
			{
				if (null == destinationType)
				{
					throw ADP.ArgumentNull("destinationType");
				}
				if (typeof(InstanceDescriptor) == destinationType && value is DataTableMapping)
				{
					DataTableMapping dataTableMapping = (DataTableMapping)value;
					DataColumnMapping[] array = new DataColumnMapping[dataTableMapping.ColumnMappings.Count];
					dataTableMapping.ColumnMappings.CopyTo(array, 0);
					object[] arguments = new object[]
					{
						dataTableMapping.SourceTable,
						dataTableMapping.DataSetTable,
						array
					};
					Type[] types = new Type[]
					{
						typeof(string),
						typeof(string),
						typeof(DataColumnMapping[])
					};
					ConstructorInfo constructor = typeof(DataTableMapping).GetConstructor(types);
					return new InstanceDescriptor(constructor, arguments);
				}
				return base.ConvertTo(context, culture, value, destinationType);
			}
		}
	}
}
