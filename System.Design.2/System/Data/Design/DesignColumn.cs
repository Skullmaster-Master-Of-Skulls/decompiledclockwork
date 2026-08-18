using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data.Common;

namespace System.Data.Design
{
	// Token: 0x02000234 RID: 564
	internal class DesignColumn : DataSourceComponent, IDataSourceNamedObject, INamedObject, ICloneable
	{
		// Token: 0x06001508 RID: 5384 RVA: 0x00077E64 File Offset: 0x00076064
		public DesignColumn()
		{
			this.dataColumn = new DataColumn();
			this.designTable = null;
			this.namingPropNames.Add("typedName");
		}

		// Token: 0x06001509 RID: 5385 RVA: 0x00077E9A File Offset: 0x0007609A
		public DesignColumn(DataColumn dataColumn)
		{
			if (dataColumn == null)
			{
				throw new InternalException("DesignColumn object needs a valid DataColumn", 20009);
			}
			this.dataColumn = dataColumn;
			this.namingPropNames.Add("typedName");
		}

		// Token: 0x17000499 RID: 1177
		// (get) Token: 0x0600150A RID: 5386 RVA: 0x00077ED8 File Offset: 0x000760D8
		// (set) Token: 0x0600150B RID: 5387 RVA: 0x00077EE8 File Offset: 0x000760E8
		[RefreshProperties(RefreshProperties.All)]
		[DefaultValue(false)]
		public bool AutoIncrement
		{
			get
			{
				return this.dataColumn.AutoIncrement;
			}
			set
			{
				if (this.dataColumn.AutoIncrement != value)
				{
					Type dataType = this.DataType;
					this.dataColumn.AutoIncrement = value;
					this.DataType != dataType;
				}
			}
		}

		// Token: 0x1700049A RID: 1178
		// (get) Token: 0x0600150C RID: 5388 RVA: 0x00077F23 File Offset: 0x00076123
		public DataColumn DataColumn
		{
			get
			{
				return this.dataColumn;
			}
		}

		// Token: 0x1700049B RID: 1179
		// (get) Token: 0x0600150D RID: 5389 RVA: 0x00077F2B File Offset: 0x0007612B
		// (set) Token: 0x0600150E RID: 5390 RVA: 0x00077F38 File Offset: 0x00076138
		[RefreshProperties(RefreshProperties.All)]
		[DefaultValue(typeof(string))]
		public Type DataType
		{
			get
			{
				return this.dataColumn.DataType;
			}
			set
			{
				if (this.dataColumn.DataType != value)
				{
					bool autoIncrement = this.AutoIncrement;
					this.dataColumn.DataType = value;
					this.OnDataTypeChanged();
					bool autoIncrement2 = this.AutoIncrement;
				}
			}
		}

		// Token: 0x1700049C RID: 1180
		// (get) Token: 0x0600150F RID: 5391 RVA: 0x00077F7A File Offset: 0x0007617A
		// (set) Token: 0x06001510 RID: 5392 RVA: 0x00077F82 File Offset: 0x00076182
		internal DesignTable DesignTable
		{
			get
			{
				return this.designTable;
			}
			set
			{
				this.designTable = value;
			}
		}

		// Token: 0x1700049D RID: 1181
		// (get) Token: 0x06001511 RID: 5393 RVA: 0x00077F8B File Offset: 0x0007618B
		// (set) Token: 0x06001512 RID: 5394 RVA: 0x00077F98 File Offset: 0x00076198
		[RefreshProperties(RefreshProperties.All)]
		[DefaultValue("")]
		public string Expression
		{
			get
			{
				return this.dataColumn.Expression;
			}
			set
			{
				bool readOnly = this.dataColumn.ReadOnly;
				this.dataColumn.Expression = value;
			}
		}

		// Token: 0x1700049E RID: 1182
		// (get) Token: 0x06001513 RID: 5395 RVA: 0x00077F23 File Offset: 0x00076123
		protected override object ExternalPropertyHost
		{
			get
			{
				return this.dataColumn;
			}
		}

		// Token: 0x1700049F RID: 1183
		// (get) Token: 0x06001514 RID: 5396 RVA: 0x00077FBD File Offset: 0x000761BD
		// (set) Token: 0x06001515 RID: 5397 RVA: 0x00077FCA File Offset: 0x000761CA
		[DefaultValue(-1)]
		public int MaxLength
		{
			get
			{
				return this.dataColumn.MaxLength;
			}
			set
			{
				if (this.MaxLength >= 0 && value > this.MaxLength)
				{
					this.dataColumn.MaxLength = -1;
				}
				this.dataColumn.MaxLength = value;
			}
		}

		// Token: 0x170004A0 RID: 1184
		// (get) Token: 0x06001516 RID: 5398 RVA: 0x00077FF6 File Offset: 0x000761F6
		// (set) Token: 0x06001517 RID: 5399 RVA: 0x00078004 File Offset: 0x00076204
		[DefaultValue("")]
		[MergableProperty(false)]
		public string Name
		{
			get
			{
				return this.dataColumn.ColumnName;
			}
			set
			{
				string columnName = this.dataColumn.ColumnName;
				if (!StringUtil.EqualValue(value, columnName))
				{
					if (this.CollectionParent != null)
					{
						this.CollectionParent.ValidateUniqueName(this, value);
					}
					this.dataColumn.ColumnName = value;
					if (columnName.Length > 0 && value.Length > 0)
					{
						DesignTable designTable = this.DesignTable;
						if (designTable != null)
						{
							designTable.UpdateColumnMappingDataSetColumnName(columnName, value);
						}
					}
				}
			}
		}

		// Token: 0x170004A1 RID: 1185
		// (get) Token: 0x06001518 RID: 5400 RVA: 0x0007806B File Offset: 0x0007626B
		// (set) Token: 0x06001519 RID: 5401 RVA: 0x000780A4 File Offset: 0x000762A4
		[DefaultValue("_throw")]
		public string NullValue
		{
			get
			{
				if (this.dataColumn.ExtendedProperties.Contains("nullValue"))
				{
					return this.dataColumn.ExtendedProperties["nullValue"] as string;
				}
				return "_throw";
			}
			set
			{
				if (value != this.NullValue)
				{
					this.dataColumn.ExtendedProperties["nullValue"] = value;
				}
			}
		}

		// Token: 0x170004A2 RID: 1186
		// (get) Token: 0x0600151A RID: 5402 RVA: 0x000780CA File Offset: 0x000762CA
		[Browsable(false)]
		public string PublicTypeName
		{
			get
			{
				return "Column";
			}
		}

		// Token: 0x170004A3 RID: 1187
		// (get) Token: 0x0600151B RID: 5403 RVA: 0x000780D4 File Offset: 0x000762D4
		// (set) Token: 0x0600151C RID: 5404 RVA: 0x00078143 File Offset: 0x00076343
		[DefaultValue("")]
		public string Source
		{
			get
			{
				if (this.DesignTable != null && this.DesignTable.Mappings != null)
				{
					int num = this.DesignTable.Mappings.IndexOfDataSetColumn(this.DataColumn.ColumnName);
					DataColumnMapping dataColumnMapping = null;
					if (num >= 0)
					{
						dataColumnMapping = this.DesignTable.Mappings.GetByDataSetColumn(this.DataColumn.ColumnName);
					}
					if (dataColumnMapping != null)
					{
						return dataColumnMapping.SourceColumn;
					}
				}
				return string.Empty;
			}
			set
			{
				if (this.DesignTable != null)
				{
					this.DesignTable.UpdateColumnMappingSourceColumnName(this.DataColumn.ColumnName, value);
				}
			}
		}

		// Token: 0x170004A4 RID: 1188
		// (get) Token: 0x0600151D RID: 5405 RVA: 0x00078164 File Offset: 0x00076364
		// (set) Token: 0x0600151E RID: 5406 RVA: 0x00003937 File Offset: 0x00001B37
		[DefaultValue(false)]
		public bool Unique
		{
			get
			{
				return this.dataColumn.Unique;
			}
			set
			{
			}
		}

		// Token: 0x0600151F RID: 5407 RVA: 0x00078174 File Offset: 0x00076374
		public object Clone()
		{
			DataColumn dataColumn = DataDesignUtil.CloneColumn(this.dataColumn);
			return new DesignColumn(dataColumn);
		}

		// Token: 0x06001520 RID: 5408 RVA: 0x00078198 File Offset: 0x00076398
		internal bool IsKeyColumn()
		{
			if (this.DesignTable == null)
			{
				return false;
			}
			ArrayList relatedDataConstraints = this.DesignTable.GetRelatedDataConstraints(new DesignColumn[]
			{
				this
			}, true);
			return relatedDataConstraints != null && relatedDataConstraints.Count > 0;
		}

		// Token: 0x06001521 RID: 5409 RVA: 0x00003937 File Offset: 0x00001B37
		private void OnDataTypeChanged()
		{
		}

		// Token: 0x06001522 RID: 5410 RVA: 0x000781D4 File Offset: 0x000763D4
		public override string ToString()
		{
			return this.PublicTypeName + " " + this.Name;
		}

		// Token: 0x170004A5 RID: 1189
		// (get) Token: 0x06001523 RID: 5411 RVA: 0x000781EC File Offset: 0x000763EC
		// (set) Token: 0x06001524 RID: 5412 RVA: 0x00078208 File Offset: 0x00076408
		internal string UserColumnName
		{
			get
			{
				return this.dataColumn.ExtendedProperties[DesignColumn.EXTPROPNAME_USER_COLUMNNAME] as string;
			}
			set
			{
				this.dataColumn.ExtendedProperties[DesignColumn.EXTPROPNAME_USER_COLUMNNAME] = value;
			}
		}

		// Token: 0x170004A6 RID: 1190
		// (get) Token: 0x06001525 RID: 5413 RVA: 0x00078220 File Offset: 0x00076420
		// (set) Token: 0x06001526 RID: 5414 RVA: 0x0007823C File Offset: 0x0007643C
		internal string GeneratorColumnPropNameInTable
		{
			get
			{
				return this.dataColumn.ExtendedProperties[DesignColumn.EXTPROPNAME_GENERATOR_COLUMNPROPNAMEINTABLE] as string;
			}
			set
			{
				this.dataColumn.ExtendedProperties[DesignColumn.EXTPROPNAME_GENERATOR_COLUMNPROPNAMEINTABLE] = value;
			}
		}

		// Token: 0x170004A7 RID: 1191
		// (get) Token: 0x06001527 RID: 5415 RVA: 0x00078254 File Offset: 0x00076454
		// (set) Token: 0x06001528 RID: 5416 RVA: 0x00078270 File Offset: 0x00076470
		internal string GeneratorColumnVarNameInTable
		{
			get
			{
				return this.dataColumn.ExtendedProperties[DesignColumn.EXTPROPNAME_GENERATOR_COLUMNVARNAMEINTABLE] as string;
			}
			set
			{
				this.dataColumn.ExtendedProperties[DesignColumn.EXTPROPNAME_GENERATOR_COLUMNVARNAMEINTABLE] = value;
			}
		}

		// Token: 0x170004A8 RID: 1192
		// (get) Token: 0x06001529 RID: 5417 RVA: 0x00078288 File Offset: 0x00076488
		// (set) Token: 0x0600152A RID: 5418 RVA: 0x000782A4 File Offset: 0x000764A4
		internal string GeneratorColumnPropNameInRow
		{
			get
			{
				return this.dataColumn.ExtendedProperties[DesignColumn.EXTPROPNAME_GENERATOR_COLUMNPROPNAMEINROW] as string;
			}
			set
			{
				this.dataColumn.ExtendedProperties[DesignColumn.EXTPROPNAME_GENERATOR_COLUMNPROPNAMEINROW] = value;
			}
		}

		// Token: 0x170004A9 RID: 1193
		// (get) Token: 0x0600152B RID: 5419 RVA: 0x000782BC File Offset: 0x000764BC
		internal override StringCollection NamingPropertyNames
		{
			get
			{
				return this.namingPropNames;
			}
		}

		// Token: 0x170004AA RID: 1194
		// (get) Token: 0x0600152C RID: 5420 RVA: 0x000782C4 File Offset: 0x000764C4
		[Browsable(false)]
		public override string GeneratorName
		{
			get
			{
				return this.GeneratorColumnPropNameInRow;
			}
		}

		// Token: 0x04000B16 RID: 2838
		private const string NullValuePropertyName = "nullValue";

		// Token: 0x04000B17 RID: 2839
		private const string NullValueThrow = "_throw";

		// Token: 0x04000B18 RID: 2840
		private DataColumn dataColumn;

		// Token: 0x04000B19 RID: 2841
		private DesignTable designTable;

		// Token: 0x04000B1A RID: 2842
		private StringCollection namingPropNames = new StringCollection();

		// Token: 0x04000B1B RID: 2843
		internal static string EXTPROPNAME_USER_COLUMNNAME = "Generator_UserColumnName";

		// Token: 0x04000B1C RID: 2844
		internal static string EXTPROPNAME_GENERATOR_COLUMNPROPNAMEINTABLE = "Generator_ColumnPropNameInTable";

		// Token: 0x04000B1D RID: 2845
		internal static string EXTPROPNAME_GENERATOR_COLUMNVARNAMEINTABLE = "Generator_ColumnVarNameInTable";

		// Token: 0x04000B1E RID: 2846
		internal static string EXTPROPNAME_GENERATOR_COLUMNPROPNAMEINROW = "Generator_ColumnPropNameInRow";

		// Token: 0x04000B1F RID: 2847
		private const string ROPNAME_EXPRESSION = "Expression";
	}
}
