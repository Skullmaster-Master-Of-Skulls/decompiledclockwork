using System;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Reflection;

namespace System.Data.Common
{
	// Token: 0x02000120 RID: 288
	[TypeConverter(typeof(DataTableMapping.DataTableMappingConverter))]
	public sealed class DataTableMapping : MarshalByRefObject, ITableMapping, ICloneable
	{
		// Token: 0x0600126D RID: 4717 RVA: 0x00236E68 File Offset: 0x00236268
		public DataTableMapping()
		{
		}

		// Token: 0x0600126E RID: 4718 RVA: 0x00236E88 File Offset: 0x00236288
		public DataTableMapping(string sourceTable, string dataSetTable)
		{
			this.SourceTable = sourceTable;
			this.DataSetTable = dataSetTable;
		}

		// Token: 0x0600126F RID: 4719 RVA: 0x00236EB8 File Offset: 0x002362B8
		public DataTableMapping(string sourceTable, string dataSetTable, DataColumnMapping[] columnMappings)
		{
			this.SourceTable = sourceTable;
			this.DataSetTable = dataSetTable;
			if (columnMappings != null && 0 < columnMappings.Length)
			{
				this.ColumnMappings.AddRange(columnMappings);
			}
		}

		// Token: 0x17000263 RID: 611
		// (get) Token: 0x06001270 RID: 4720 RVA: 0x00236EF8 File Offset: 0x002362F8
		IColumnMappingCollection ITableMapping.ColumnMappings
		{
			get
			{
				return this.ColumnMappings;
			}
		}

		// Token: 0x17000264 RID: 612
		// (get) Token: 0x06001271 RID: 4721 RVA: 0x00236F18 File Offset: 0x00236318
		[ResDescription("DataTableMapping_ColumnMappings")]
		[ResCategory("DataCategory_Mapping")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
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

		// Token: 0x17000265 RID: 613
		// (get) Token: 0x06001272 RID: 4722 RVA: 0x00236F48 File Offset: 0x00236348
		// (set) Token: 0x06001273 RID: 4723 RVA: 0x00236F68 File Offset: 0x00236368
		[ResDescription("DataTableMapping_DataSetTable")]
		[DefaultValue("")]
		[ResCategory("DataCategory_Mapping")]
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

		// Token: 0x17000266 RID: 614
		// (get) Token: 0x06001274 RID: 4724 RVA: 0x00236F88 File Offset: 0x00236388
		// (set) Token: 0x06001275 RID: 4725 RVA: 0x00236FA8 File Offset: 0x002363A8
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

		// Token: 0x17000267 RID: 615
		// (get) Token: 0x06001276 RID: 4726 RVA: 0x00236FC8 File Offset: 0x002363C8
		// (set) Token: 0x06001277 RID: 4727 RVA: 0x00236FE8 File Offset: 0x002363E8
		[ResCategory("DataCategory_Mapping")]
		[ResDescription("DataTableMapping_SourceTable")]
		[DefaultValue("")]
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

		// Token: 0x06001278 RID: 4728 RVA: 0x00237028 File Offset: 0x00236428
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

		// Token: 0x06001279 RID: 4729 RVA: 0x002370D8 File Offset: 0x002364D8
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public DataColumn GetDataColumn(string sourceColumn, Type dataType, DataTable dataTable, MissingMappingAction mappingAction, MissingSchemaAction schemaAction)
		{
			return DataColumnMappingCollection.GetDataColumn(this._columnMappings, sourceColumn, dataType, dataTable, mappingAction, schemaAction);
		}

		// Token: 0x0600127A RID: 4730 RVA: 0x002370F8 File Offset: 0x002364F8
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public DataColumnMapping GetColumnMappingBySchemaAction(string sourceColumn, MissingMappingAction mappingAction)
		{
			return DataColumnMappingCollection.GetColumnMappingBySchemaAction(this._columnMappings, sourceColumn, mappingAction);
		}

		// Token: 0x0600127B RID: 4731 RVA: 0x00237118 File Offset: 0x00236518
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

		// Token: 0x0600127C RID: 4732 RVA: 0x002371A8 File Offset: 0x002365A8
		public override string ToString()
		{
			return this.SourceTable;
		}

		// Token: 0x04000BBD RID: 3005
		private DataTableMappingCollection parent;

		// Token: 0x04000BBE RID: 3006
		private DataColumnMappingCollection _columnMappings;

		// Token: 0x04000BBF RID: 3007
		private string _dataSetTableName;

		// Token: 0x04000BC0 RID: 3008
		private string _sourceTableName;

		// Token: 0x02000121 RID: 289
		internal sealed class DataTableMappingConverter : ExpandableObjectConverter
		{
			// Token: 0x0600127E RID: 4734 RVA: 0x002371E8 File Offset: 0x002365E8
			public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
			{
				return typeof(InstanceDescriptor) == destinationType || base.CanConvertTo(context, destinationType);
			}

			// Token: 0x0600127F RID: 4735 RVA: 0x00237218 File Offset: 0x00236618
			public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
			{
				if (destinationType == null)
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
