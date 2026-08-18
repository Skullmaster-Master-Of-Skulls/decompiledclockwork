using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Threading;
using System.Xml;
using System.Xml.Serialization;

namespace System.Data
{
	// Token: 0x02000068 RID: 104
	[DesignTimeVisible(false)]
	[Editor("Microsoft.VSDesigner.Data.Design.DataColumnEditor, Microsoft.VSDesigner, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[DefaultProperty("ColumnName")]
	[ToolboxItem(false)]
	public class DataColumn : MarshalByValueComponent
	{
		// Token: 0x060004B4 RID: 1204 RVA: 0x001E92A8 File Offset: 0x001E86A8
		public DataColumn() : this(null, typeof(string), null, MappingType.Element)
		{
		}

		// Token: 0x060004B5 RID: 1205 RVA: 0x001E92C8 File Offset: 0x001E86C8
		public DataColumn(string columnName) : this(columnName, typeof(string), null, MappingType.Element)
		{
		}

		// Token: 0x060004B6 RID: 1206 RVA: 0x001E92E8 File Offset: 0x001E86E8
		public DataColumn(string columnName, Type dataType) : this(columnName, dataType, null, MappingType.Element)
		{
		}

		// Token: 0x060004B7 RID: 1207 RVA: 0x001E9308 File Offset: 0x001E8708
		public DataColumn(string columnName, Type dataType, string expr) : this(columnName, dataType, expr, MappingType.Element)
		{
		}

		// Token: 0x060004B8 RID: 1208 RVA: 0x001E9328 File Offset: 0x001E8728
		public DataColumn(string columnName, Type dataType, string expr, MappingType type)
		{
			GC.SuppressFinalize(this);
			Bid.Trace("<ds.DataColumn.DataColumn|API> %d#, columnName='%ls', expr='%ls', type=%d{ds.MappingType}\n", this.ObjectID, columnName, expr, (int)type);
			if (dataType == null)
			{
				throw ExceptionBuilder.ArgumentNull("dataType");
			}
			StorageType storageType = DataStorage.GetStorageType(dataType);
			if (DataStorage.ImplementsINullableValue(storageType, dataType))
			{
				throw ExceptionBuilder.ColumnTypeNotSupported();
			}
			this._columnName = ((columnName == null) ? "" : columnName);
			SimpleType simpleType = SimpleType.CreateSimpleType(dataType);
			if (simpleType != null)
			{
				this.SimpleType = simpleType;
			}
			this.UpdateColumnType(dataType, storageType);
			if (expr != null && 0 < expr.Length)
			{
				this.Expression = expr;
			}
			this.columnMapping = type;
		}

		// Token: 0x060004B9 RID: 1209 RVA: 0x001E9438 File Offset: 0x001E8838
		private void UpdateColumnType(Type type, StorageType typeCode)
		{
			TypeLimiter.EnsureTypeIsAllowed(type);
			this.dataType = type;
			if (StorageType.DateTime != typeCode)
			{
				this._dateTimeMode = DataSetDateTime.UnspecifiedLocal;
			}
			DataStorage.ImplementsInterfaces(typeCode, type, out this.isSqlType, out this.implementsINullable, out this.implementsIXMLSerializable, out this.implementsIChangeTracking, out this.implementsIRevertibleChangeTracking);
			if (!this.isSqlType && this.implementsINullable)
			{
				SqlUdtStorage.GetStaticNullForUdtType(type);
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060004BA RID: 1210 RVA: 0x001E94A8 File Offset: 0x001E88A8
		// (set) Token: 0x060004BB RID: 1211 RVA: 0x001E94C8 File Offset: 0x001E88C8
		[ResCategory("DataCategory_Data")]
		[ResDescription("DataColumnAllowNullDescr")]
		[DefaultValue(true)]
		public bool AllowDBNull
		{
			get
			{
				return this.allowNull;
			}
			set
			{
				IntPtr intPtr;
				Bid.ScopeEnter(out intPtr, "<ds.DataColumn.set_AllowDBNull|API> %d#, %d{bool}\n", this.ObjectID, value);
				try
				{
					if (this.allowNull != value)
					{
						if (this.table != null && !value && this.table.EnforceConstraints)
						{
							this.CheckNotAllowNull();
						}
						this.allowNull = value;
					}
				}
				finally
				{
					Bid.ScopeLeave(ref intPtr);
				}
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060004BC RID: 1212 RVA: 0x001E9548 File Offset: 0x001E8948
		// (set) Token: 0x060004BD RID: 1213 RVA: 0x001E9568 File Offset: 0x001E8968
		[ResDescription("DataColumnAutoIncrementDescr")]
		[ResCategory("DataCategory_Data")]
		[DefaultValue(false)]
		[RefreshProperties(RefreshProperties.All)]
		public bool AutoIncrement
		{
			get
			{
				return this.autoIncrement;
			}
			set
			{
				Bid.Trace("<ds.DataColumn.set_AutoIncrement|API> %d#, %d{bool}\n", this.ObjectID, value);
				if (this.autoIncrement != value)
				{
					if (value)
					{
						if (this.expression != null)
						{
							throw ExceptionBuilder.AutoIncrementAndExpression();
						}
						if (!this.DefaultValueIsNull)
						{
							throw ExceptionBuilder.AutoIncrementAndDefaultValue();
						}
						if (!DataColumn.IsAutoIncrementType(this.DataType))
						{
							if (this.HasData)
							{
								throw ExceptionBuilder.AutoIncrementCannotSetIfHasData(this.DataType.Name);
							}
							this.DataType = typeof(int);
						}
					}
					this.autoIncrement = value;
				}
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060004BE RID: 1214 RVA: 0x001E95F8 File Offset: 0x001E89F8
		// (set) Token: 0x060004BF RID: 1215 RVA: 0x001E9618 File Offset: 0x001E8A18
		[DefaultValue(0L)]
		[ResCategory("DataCategory_Data")]
		[ResDescription("DataColumnAutoIncrementSeedDescr")]
		public long AutoIncrementSeed
		{
			get
			{
				return this.autoIncrementSeed;
			}
			set
			{
				Bid.Trace("<ds.DataColumn.set_AutoIncrementSeed|API> %d#, %I64d\n", this.ObjectID, value);
				if (this.autoIncrementSeed != value)
				{
					if (this.autoIncrementCurrent == this.autoIncrementSeed)
					{
						this.autoIncrementCurrent = value;
					}
					if (this.AutoIncrementStep > 0L)
					{
						if (this.autoIncrementCurrent < value)
						{
							this.autoIncrementCurrent = value;
						}
					}
					else if (this.autoIncrementCurrent > value)
					{
						this.autoIncrementCurrent = value;
					}
					this.autoIncrementSeed = value;
				}
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060004C0 RID: 1216 RVA: 0x001E9688 File Offset: 0x001E8A88
		// (set) Token: 0x060004C1 RID: 1217 RVA: 0x001E96A8 File Offset: 0x001E8AA8
		[ResDescription("DataColumnAutoIncrementStepDescr")]
		[DefaultValue(1L)]
		[ResCategory("DataCategory_Data")]
		public long AutoIncrementStep
		{
			get
			{
				return this.autoIncrementStep;
			}
			set
			{
				Bid.Trace("<ds.DataColumn.set_AutoIncrementStep|API> %d#, %I64d\n", this.ObjectID, value);
				if (this.autoIncrementStep != value)
				{
					if (value == 0L)
					{
						throw ExceptionBuilder.AutoIncrementSeed();
					}
					if (this.autoIncrementCurrent != this.autoIncrementSeed)
					{
						this.autoIncrementCurrent += value - this.autoIncrementStep;
					}
					this.autoIncrementStep = value;
				}
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060004C2 RID: 1218 RVA: 0x001E9708 File Offset: 0x001E8B08
		// (set) Token: 0x060004C3 RID: 1219 RVA: 0x001E9738 File Offset: 0x001E8B38
		[ResDescription("DataColumnCaptionDescr")]
		[ResCategory("DataCategory_Data")]
		public string Caption
		{
			get
			{
				if (this.caption == null)
				{
					return this._columnName;
				}
				return this.caption;
			}
			set
			{
				if (value == null)
				{
					value = "";
				}
				if (this.caption == null || string.Compare(this.caption, value, true, this.Locale) != 0)
				{
					this.caption = value;
				}
			}
		}

		// Token: 0x060004C4 RID: 1220 RVA: 0x001E9778 File Offset: 0x001E8B78
		private void ResetCaption()
		{
			if (this.caption != null)
			{
				this.caption = null;
			}
		}

		// Token: 0x060004C5 RID: 1221 RVA: 0x001E9798 File Offset: 0x001E8B98
		private bool ShouldSerializeCaption()
		{
			return this.caption != null;
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060004C6 RID: 1222 RVA: 0x001E97B8 File Offset: 0x001E8BB8
		// (set) Token: 0x060004C7 RID: 1223 RVA: 0x001E97D8 File Offset: 0x001E8BD8
		[ResDescription("DataColumnColumnNameDescr")]
		[RefreshProperties(RefreshProperties.All)]
		[DefaultValue("")]
		[ResCategory("DataCategory_Data")]
		public string ColumnName
		{
			get
			{
				return this._columnName;
			}
			set
			{
				IntPtr intPtr;
				Bid.ScopeEnter(out intPtr, "<ds.DataColumn.set_ColumnName|API> %d#, '%ls'\n", this.ObjectID, value);
				try
				{
					if (value == null)
					{
						value = "";
					}
					if (string.Compare(this._columnName, value, true, this.Locale) != 0)
					{
						if (this.table != null)
						{
							if (value.Length == 0)
							{
								throw ExceptionBuilder.ColumnNameRequired();
							}
							this.table.Columns.RegisterColumnName(value, this, null);
							if (this._columnName.Length != 0)
							{
								this.table.Columns.UnregisterName(this._columnName);
							}
						}
						this.RaisePropertyChanging("ColumnName");
						this._columnName = value;
						this.encodedColumnName = null;
						if (this.table != null)
						{
							this.table.Columns.OnColumnPropertyChanged(new CollectionChangeEventArgs(CollectionChangeAction.Refresh, this));
						}
					}
					else if (this._columnName != value)
					{
						this.RaisePropertyChanging("ColumnName");
						this._columnName = value;
						this.encodedColumnName = null;
						if (this.table != null)
						{
							this.table.Columns.OnColumnPropertyChanged(new CollectionChangeEventArgs(CollectionChangeAction.Refresh, this));
						}
					}
				}
				finally
				{
					Bid.ScopeLeave(ref intPtr);
				}
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060004C8 RID: 1224 RVA: 0x001E9918 File Offset: 0x001E8D18
		internal string EncodedColumnName
		{
			get
			{
				if (this.encodedColumnName == null)
				{
					this.encodedColumnName = XmlConvert.EncodeLocalName(this.ColumnName);
				}
				return this.encodedColumnName;
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060004C9 RID: 1225 RVA: 0x001E9948 File Offset: 0x001E8D48
		internal IFormatProvider FormatProvider
		{
			get
			{
				if (this.table == null)
				{
					return CultureInfo.CurrentCulture;
				}
				return this.table.FormatProvider;
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060004CA RID: 1226 RVA: 0x001E9978 File Offset: 0x001E8D78
		internal CultureInfo Locale
		{
			get
			{
				if (this.table == null)
				{
					return CultureInfo.CurrentCulture;
				}
				return this.table.Locale;
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060004CB RID: 1227 RVA: 0x001E99A8 File Offset: 0x001E8DA8
		internal int ObjectID
		{
			get
			{
				return this._objectID;
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060004CC RID: 1228 RVA: 0x001E99C8 File Offset: 0x001E8DC8
		// (set) Token: 0x060004CD RID: 1229 RVA: 0x001E99E8 File Offset: 0x001E8DE8
		[ResDescription("DataColumnPrefixDescr")]
		[ResCategory("DataCategory_Data")]
		[DefaultValue("")]
		public string Prefix
		{
			get
			{
				return this._columnPrefix;
			}
			set
			{
				if (value == null)
				{
					value = "";
				}
				Bid.Trace("<ds.DataColumn.set_Prefix|API> %d#, '%ls'\n", this.ObjectID, value);
				if (XmlConvert.DecodeName(value) == value && XmlConvert.EncodeName(value) != value)
				{
					throw ExceptionBuilder.InvalidPrefix(value);
				}
				this._columnPrefix = value;
			}
		}

		// Token: 0x060004CE RID: 1230 RVA: 0x001E9A48 File Offset: 0x001E8E48
		internal string GetColumnValueAsString(DataRow row, DataRowVersion version)
		{
			object value = this[row.GetRecordFromVersion(version)];
			if (DataStorage.IsObjectNull(value))
			{
				return null;
			}
			return this.ConvertObjectToXml(value);
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060004CF RID: 1231 RVA: 0x001E9A78 File Offset: 0x001E8E78
		internal bool Computed
		{
			get
			{
				return this.expression != null;
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060004D0 RID: 1232 RVA: 0x001E9A98 File Offset: 0x001E8E98
		internal DataExpression DataExpression
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060004D1 RID: 1233 RVA: 0x001E9AB8 File Offset: 0x001E8EB8
		// (set) Token: 0x060004D2 RID: 1234 RVA: 0x001E9AD8 File Offset: 0x001E8ED8
		[TypeConverter(typeof(ColumnTypeConverter))]
		[RefreshProperties(RefreshProperties.All)]
		[ResCategory("DataCategory_Data")]
		[ResDescription("DataColumnDataTypeDescr")]
		[DefaultValue(typeof(string))]
		public Type DataType
		{
			get
			{
				return this.dataType;
			}
			set
			{
				if (this.dataType != value)
				{
					if (this.HasData)
					{
						throw ExceptionBuilder.CantChangeDataType();
					}
					if (value == null)
					{
						throw ExceptionBuilder.NullDataType();
					}
					StorageType storageType = DataStorage.GetStorageType(value);
					if (DataStorage.ImplementsINullableValue(storageType, value))
					{
						throw ExceptionBuilder.ColumnTypeNotSupported();
					}
					if (this.table != null && this.IsInRelation())
					{
						throw ExceptionBuilder.ColumnsTypeMismatch();
					}
					if (!this.DefaultValueIsNull)
					{
						try
						{
							if (typeof(string) == value)
							{
								this.defaultValue = this.DefaultValue.ToString();
							}
							else if (typeof(SqlString) == value)
							{
								this.defaultValue = SqlConvert.ConvertToSqlString(this.DefaultValue);
							}
							else if (typeof(object) != value)
							{
								this.DefaultValue = SqlConvert.ChangeType(this.DefaultValue, value, this.FormatProvider);
							}
						}
						catch (InvalidCastException)
						{
							throw ExceptionBuilder.DefaultValueDataType(this.ColumnName, this.DefaultValue.GetType(), value);
						}
						catch (FormatException)
						{
							throw ExceptionBuilder.DefaultValueDataType(this.ColumnName, this.DefaultValue.GetType(), value);
						}
					}
					if (this.ColumnMapping == MappingType.SimpleContent && value == typeof(char))
					{
						throw ExceptionBuilder.CannotSetSimpleContentType(this.ColumnName, value);
					}
					this.SimpleType = SimpleType.CreateSimpleType(value);
					if (StorageType.String == storageType)
					{
						this.maxLength = -1;
					}
					this.UpdateColumnType(value, storageType);
					this.XmlDataType = null;
					if (this.AutoIncrement && !DataColumn.IsAutoIncrementType(value))
					{
						this.AutoIncrement = false;
					}
				}
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060004D3 RID: 1235 RVA: 0x001E9C78 File Offset: 0x001E9078
		// (set) Token: 0x060004D4 RID: 1236 RVA: 0x001E9C98 File Offset: 0x001E9098
		[RefreshProperties(RefreshProperties.All)]
		[ResDescription("DataColumnDateTimeModeDescr")]
		[DefaultValue(DataSetDateTime.UnspecifiedLocal)]
		[ResCategory("DataCategory_Data")]
		public DataSetDateTime DateTimeMode
		{
			get
			{
				return this._dateTimeMode;
			}
			set
			{
				if (this._dateTimeMode != value)
				{
					if (this.DataType != typeof(DateTime) && value != DataSetDateTime.UnspecifiedLocal)
					{
						throw ExceptionBuilder.CannotSetDateTimeModeForNonDateTimeColumns();
					}
					switch (value)
					{
					case DataSetDateTime.Local:
					case DataSetDateTime.Utc:
						if (this.HasData)
						{
							throw ExceptionBuilder.CantChangeDateTimeMode(this._dateTimeMode, value);
						}
						break;
					case DataSetDateTime.Unspecified:
					case DataSetDateTime.UnspecifiedLocal:
						if (this._dateTimeMode != DataSetDateTime.Unspecified && this._dateTimeMode != DataSetDateTime.UnspecifiedLocal && this.HasData)
						{
							throw ExceptionBuilder.CantChangeDateTimeMode(this._dateTimeMode, value);
						}
						break;
					default:
						throw ExceptionBuilder.InvalidDateTimeMode(value);
					}
					this._dateTimeMode = value;
				}
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060004D5 RID: 1237 RVA: 0x001E9D38 File Offset: 0x001E9138
		// (set) Token: 0x060004D6 RID: 1238 RVA: 0x001E9DD8 File Offset: 0x001E91D8
		[ResDescription("DataColumnDefaultValueDescr")]
		[TypeConverter(typeof(DefaultValueTypeConverter))]
		[ResCategory("DataCategory_Data")]
		public object DefaultValue
		{
			get
			{
				if (this.defaultValue == DBNull.Value && this.implementsINullable)
				{
					if (this._storage != null)
					{
						this.defaultValue = this._storage.NullValue;
					}
					else if (this.isSqlType)
					{
						this.defaultValue = SqlConvert.ChangeType(this.defaultValue, this.dataType, this.FormatProvider);
					}
					else if (this.implementsINullable)
					{
						PropertyInfo property = this.dataType.GetProperty("Null", BindingFlags.Static | BindingFlags.Public);
						if (property != null)
						{
							this.defaultValue = property.GetValue(null, null);
						}
					}
				}
				return this.defaultValue;
			}
			set
			{
				Bid.Trace("<ds.DataColumn.set_DefaultValue|API> %d#\n", this.ObjectID);
				if (this.defaultValue == null || !this.DefaultValue.Equals(value))
				{
					if (this.AutoIncrement)
					{
						throw ExceptionBuilder.DefaultValueAndAutoIncrement();
					}
					object obj = (value == null) ? DBNull.Value : value;
					if (obj != DBNull.Value && this.DataType != typeof(object))
					{
						try
						{
							obj = SqlConvert.ChangeType(obj, this.DataType, this.FormatProvider);
						}
						catch (InvalidCastException)
						{
							throw ExceptionBuilder.DefaultValueColumnDataType(this.ColumnName, this.DefaultValue.GetType(), this.DataType);
						}
					}
					this.defaultValue = obj;
					this.defaultValueIsNull = (obj == DBNull.Value || (this.ImplementsINullable && DataStorage.IsObjectSqlNull(obj)));
				}
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060004D7 RID: 1239 RVA: 0x001E9EC8 File Offset: 0x001E92C8
		internal bool DefaultValueIsNull
		{
			get
			{
				return this.defaultValueIsNull;
			}
		}

		// Token: 0x060004D8 RID: 1240 RVA: 0x001E9EE8 File Offset: 0x001E92E8
		internal void BindExpression()
		{
			this.DataExpression.Bind(this.table);
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060004D9 RID: 1241 RVA: 0x001E9F08 File Offset: 0x001E9308
		// (set) Token: 0x060004DA RID: 1242 RVA: 0x001E9F38 File Offset: 0x001E9338
		[ResCategory("DataCategory_Data")]
		[DefaultValue("")]
		[ResDescription("DataColumnExpressionDescr")]
		[RefreshProperties(RefreshProperties.All)]
		public string Expression
		{
			get
			{
				if (this.expression != null)
				{
					return this.expression.Expression;
				}
				return "";
			}
			set
			{
				IntPtr intPtr;
				Bid.ScopeEnter(out intPtr, "<ds.DataColumn.set_Expression|API> %d#, '%ls'\n", this.ObjectID, value);
				if (value == null)
				{
					value = "";
				}
				try
				{
					DataExpression dataExpression = null;
					if (value.Length > 0)
					{
						DataExpression dataExpression2 = new DataExpression(this.table, value, this.dataType);
						if (dataExpression2.HasValue)
						{
							dataExpression = dataExpression2;
						}
					}
					if (this.expression == null && dataExpression != null)
					{
						if (this.AutoIncrement || this.Unique)
						{
							throw ExceptionBuilder.ExpressionAndUnique();
						}
						if (this.table != null)
						{
							for (int i = 0; i < this.table.Constraints.Count; i++)
							{
								if (this.table.Constraints[i].ContainsColumn(this))
								{
									throw ExceptionBuilder.ExpressionAndConstraint(this, this.table.Constraints[i]);
								}
							}
						}
						bool flag = this.ReadOnly;
						try
						{
							this.ReadOnly = true;
						}
						catch (ReadOnlyException e)
						{
							ExceptionBuilder.TraceExceptionForCapture(e);
							this.ReadOnly = flag;
							throw ExceptionBuilder.ExpressionAndReadOnly();
						}
					}
					if (this.table != null)
					{
						if (dataExpression != null && dataExpression.DependsOn(this))
						{
							throw ExceptionBuilder.ExpressionCircular();
						}
						this.HandleDependentColumnList(this.expression, dataExpression);
						DataExpression dataExpression3 = this.expression;
						this.expression = dataExpression;
						try
						{
							if (dataExpression == null)
							{
								for (int j = 0; j < this.table.RecordCapacity; j++)
								{
									this.InitializeRecord(j);
								}
							}
							else
							{
								this.table.EvaluateExpressions(this);
							}
							this.table.ResetInternalIndexes(this);
							this.table.EvaluateDependentExpressions(this);
							goto IL_205;
						}
						catch (Exception e2)
						{
							if (!ADP.IsCatchableExceptionType(e2))
							{
								throw;
							}
							ExceptionBuilder.TraceExceptionForCapture(e2);
							try
							{
								this.expression = dataExpression3;
								this.HandleDependentColumnList(dataExpression, this.expression);
								if (dataExpression3 == null)
								{
									for (int k = 0; k < this.table.RecordCapacity; k++)
									{
										this.InitializeRecord(k);
									}
								}
								else
								{
									this.table.EvaluateExpressions(this);
								}
								this.table.ResetInternalIndexes(this);
								this.table.EvaluateDependentExpressions(this);
							}
							catch (Exception e3)
							{
								if (!ADP.IsCatchableExceptionType(e3))
								{
									throw;
								}
								ExceptionBuilder.TraceExceptionWithoutRethrow(e3);
							}
							throw;
						}
					}
					this.expression = dataExpression;
					IL_205:;
				}
				finally
				{
					Bid.ScopeLeave(ref intPtr);
				}
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060004DB RID: 1243 RVA: 0x001EA1B8 File Offset: 0x001E95B8
		[ResCategory("DataCategory_Data")]
		[ResDescription("ExtendedPropertiesDescr")]
		[Browsable(false)]
		public PropertyCollection ExtendedProperties
		{
			get
			{
				if (this.extendedProperties == null)
				{
					this.extendedProperties = new PropertyCollection();
				}
				return this.extendedProperties;
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060004DC RID: 1244 RVA: 0x001EA1E8 File Offset: 0x001E95E8
		internal bool HasData
		{
			get
			{
				return this._storage != null;
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060004DD RID: 1245 RVA: 0x001EA208 File Offset: 0x001E9608
		internal bool ImplementsINullable
		{
			get
			{
				return this.implementsINullable;
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060004DE RID: 1246 RVA: 0x001EA228 File Offset: 0x001E9628
		internal bool ImplementsIChangeTracking
		{
			get
			{
				return this.implementsIChangeTracking;
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060004DF RID: 1247 RVA: 0x001EA248 File Offset: 0x001E9648
		internal bool ImplementsIRevertibleChangeTracking
		{
			get
			{
				return this.implementsIRevertibleChangeTracking;
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060004E0 RID: 1248 RVA: 0x001EA268 File Offset: 0x001E9668
		internal bool IsCloneable
		{
			get
			{
				return this._storage.IsCloneable;
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060004E1 RID: 1249 RVA: 0x001EA288 File Offset: 0x001E9688
		internal bool IsStringType
		{
			get
			{
				return this._storage.IsStringType;
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060004E2 RID: 1250 RVA: 0x001EA2A8 File Offset: 0x001E96A8
		internal bool IsValueType
		{
			get
			{
				return this._storage.IsValueType;
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060004E3 RID: 1251 RVA: 0x001EA2C8 File Offset: 0x001E96C8
		internal bool IsSqlType
		{
			get
			{
				return this.isSqlType;
			}
		}

		// Token: 0x060004E4 RID: 1252 RVA: 0x001EA2E8 File Offset: 0x001E96E8
		private void SetMaxLengthSimpleType()
		{
			if (this.simpleType != null)
			{
				this.simpleType.MaxLength = this.maxLength;
				if (this.simpleType.IsPlainString())
				{
					this.simpleType = null;
					return;
				}
				if (this.simpleType.Name != null && this.dttype != null)
				{
					this.simpleType.ConvertToAnnonymousSimpleType();
					this.dttype = null;
					return;
				}
			}
			else if (-1 < this.maxLength)
			{
				this.SimpleType = SimpleType.CreateLimitedStringType(this.maxLength);
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060004E5 RID: 1253 RVA: 0x001EA368 File Offset: 0x001E9768
		// (set) Token: 0x060004E6 RID: 1254 RVA: 0x001EA388 File Offset: 0x001E9788
		[DefaultValue(-1)]
		[ResDescription("DataColumnMaxLengthDescr")]
		[ResCategory("DataCategory_Data")]
		public int MaxLength
		{
			get
			{
				return this.maxLength;
			}
			set
			{
				IntPtr intPtr;
				Bid.ScopeEnter(out intPtr, "<ds.DataColumn.set_MaxLength|API> %d#, %d\n", this.ObjectID, value);
				try
				{
					if (this.maxLength != value)
					{
						if (this.ColumnMapping == MappingType.SimpleContent)
						{
							throw ExceptionBuilder.CannotSetMaxLength2(this);
						}
						if (this.DataType != typeof(string) && this.DataType != typeof(SqlString))
						{
							throw ExceptionBuilder.HasToBeStringType(this);
						}
						int num = this.maxLength;
						this.maxLength = Math.Max(value, -1);
						if ((num < 0 || value < num) && this.table != null && this.table.EnforceConstraints && !this.CheckMaxLength())
						{
							this.maxLength = num;
							throw ExceptionBuilder.CannotSetMaxLength(this, value);
						}
						this.SetMaxLengthSimpleType();
					}
				}
				finally
				{
					Bid.ScopeLeave(ref intPtr);
				}
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060004E7 RID: 1255 RVA: 0x001EA468 File Offset: 0x001E9868
		// (set) Token: 0x060004E8 RID: 1256 RVA: 0x001EA4A8 File Offset: 0x001E98A8
		[ResCategory("DataCategory_Data")]
		[ResDescription("DataColumnNamespaceDescr")]
		public string Namespace
		{
			get
			{
				if (this._columnUri != null)
				{
					return this._columnUri;
				}
				if (this.Table != null && this.columnMapping != MappingType.Attribute)
				{
					return this.Table.Namespace;
				}
				return "";
			}
			set
			{
				Bid.Trace("<ds.DataColumn.set_Namespace|API> %d#, '%ls'\n", this.ObjectID, value);
				if (this._columnUri != value)
				{
					if (this.columnMapping != MappingType.SimpleContent)
					{
						this.RaisePropertyChanging("Namespace");
						this._columnUri = value;
						return;
					}
					if (value != this.Namespace)
					{
						throw ExceptionBuilder.CannotChangeNamespace(this.ColumnName);
					}
				}
			}
		}

		// Token: 0x060004E9 RID: 1257 RVA: 0x001EA518 File Offset: 0x001E9918
		private bool ShouldSerializeNamespace()
		{
			return this._columnUri != null;
		}

		// Token: 0x060004EA RID: 1258 RVA: 0x001EA538 File Offset: 0x001E9938
		private void ResetNamespace()
		{
			this.Namespace = null;
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060004EB RID: 1259 RVA: 0x001EA558 File Offset: 0x001E9958
		[ResDescription("DataColumnOrdinalDescr")]
		[ResCategory("DataCategory_Data")]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int Ordinal
		{
			get
			{
				return this._ordinal;
			}
		}

		// Token: 0x060004EC RID: 1260 RVA: 0x001EA578 File Offset: 0x001E9978
		public void SetOrdinal(int ordinal)
		{
			if (this._ordinal == -1)
			{
				throw ExceptionBuilder.ColumnNotInAnyTable();
			}
			if (this._ordinal != ordinal)
			{
				this.table.Columns.MoveTo(this, ordinal);
			}
		}

		// Token: 0x060004ED RID: 1261 RVA: 0x001EA5B8 File Offset: 0x001E99B8
		internal void SetOrdinalInternal(int ordinal)
		{
			if (this._ordinal != ordinal)
			{
				if (this.Unique && this._ordinal != -1 && ordinal == -1)
				{
					UniqueConstraint uniqueConstraint = this.table.Constraints.FindKeyConstraint(this);
					if (uniqueConstraint != null)
					{
						this.table.Constraints.Remove(uniqueConstraint);
					}
				}
				if (this.sortIndex != null && -1 == ordinal)
				{
					this.sortIndex.RemoveRef();
					this.sortIndex.RemoveRef();
					this.sortIndex = null;
				}
				int ordinal2 = this._ordinal;
				this._ordinal = ordinal;
				if (ordinal2 == -1 && this._ordinal != -1 && this.Unique)
				{
					UniqueConstraint constraint = new UniqueConstraint(this);
					this.table.Constraints.Add(constraint);
				}
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060004EE RID: 1262 RVA: 0x001EA678 File Offset: 0x001E9A78
		// (set) Token: 0x060004EF RID: 1263 RVA: 0x001EA698 File Offset: 0x001E9A98
		[DefaultValue(false)]
		[ResCategory("DataCategory_Data")]
		[ResDescription("DataColumnReadOnlyDescr")]
		public bool ReadOnly
		{
			get
			{
				return this.readOnly;
			}
			set
			{
				Bid.Trace("<ds.DataColumn.set_ReadOnly|API> %d#, %d{bool}\n", this.ObjectID, value);
				if (this.readOnly != value)
				{
					if (!value && this.expression != null)
					{
						throw ExceptionBuilder.ReadOnlyAndExpression();
					}
					this.readOnly = value;
				}
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060004F0 RID: 1264 RVA: 0x001EA6D8 File Offset: 0x001E9AD8
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Index SortIndex
		{
			get
			{
				if (this.sortIndex == null)
				{
					IndexField[] indexDesc = new IndexField[]
					{
						new IndexField(this, false)
					};
					this.sortIndex = this.table.GetIndex(indexDesc, DataViewRowState.CurrentRows, null);
					this.sortIndex.AddRef();
				}
				return this.sortIndex;
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060004F1 RID: 1265 RVA: 0x001EA738 File Offset: 0x001E9B38
		[ResCategory("DataCategory_Data")]
		[ResDescription("DataColumnDataTableDescr")]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public DataTable Table
		{
			get
			{
				return this.table;
			}
		}

		// Token: 0x060004F2 RID: 1266 RVA: 0x001EA758 File Offset: 0x001E9B58
		internal void SetTable(DataTable table)
		{
			if (this.table != table)
			{
				if (this.Computed && (table == null || (!table.fInitInProgress && (table.DataSet == null || (!table.DataSet.fIsSchemaLoading && !table.DataSet.fInitInProgress)))))
				{
					this.DataExpression.Bind(table);
				}
				if (this.Unique && this.table != null)
				{
					UniqueConstraint uniqueConstraint = table.Constraints.FindKeyConstraint(this);
					if (uniqueConstraint != null)
					{
						table.Constraints.CanRemove(uniqueConstraint, true);
					}
				}
				this.table = table;
				this._storage = null;
			}
		}

		// Token: 0x060004F3 RID: 1267 RVA: 0x001EA7F8 File Offset: 0x001E9BF8
		private DataRow GetDataRow(int index)
		{
			return this.table.recordManager[index];
		}

		// Token: 0x170000A9 RID: 169
		internal object this[int record]
		{
			get
			{
				return this._storage.Get(record);
			}
			set
			{
				try
				{
					this._storage.Set(record, value);
				}
				catch (Exception ex)
				{
					ExceptionBuilder.TraceExceptionForCapture(ex);
					throw ExceptionBuilder.SetFailed(value, this, this.DataType, ex);
				}
				if (this.autoIncrement && !DataStorage.IsObjectNull(value))
				{
					long num = (long)SqlConvert.ChangeType2(value, StorageType.Int64, typeof(long), this.FormatProvider);
					if (this.autoIncrementStep > 0L)
					{
						if (num >= this.autoIncrementCurrent)
						{
							this.autoIncrementCurrent = num + this.autoIncrementStep;
						}
					}
					else if (num <= this.autoIncrementCurrent)
					{
						this.autoIncrementCurrent = num + this.autoIncrementStep;
					}
				}
				if (this.Computed)
				{
					DataRow dataRow = this.GetDataRow(record);
					if (dataRow != null)
					{
						dataRow.LastChangedColumn = this;
					}
				}
			}
		}

		// Token: 0x060004F6 RID: 1270 RVA: 0x001EA918 File Offset: 0x001E9D18
		internal void InitializeRecord(int record)
		{
			this._storage.Set(record, this.DefaultValue);
		}

		// Token: 0x060004F7 RID: 1271 RVA: 0x001EA938 File Offset: 0x001E9D38
		internal void SetValue(int record, object value)
		{
			try
			{
				this._storage.Set(record, value);
			}
			catch (Exception ex)
			{
				ExceptionBuilder.TraceExceptionForCapture(ex);
				throw ExceptionBuilder.SetFailed(value, this, this.DataType, ex);
			}
			DataRow dataRow = this.GetDataRow(record);
			if (dataRow != null)
			{
				dataRow.LastChangedColumn = this;
			}
		}

		// Token: 0x060004F8 RID: 1272 RVA: 0x001EA9A8 File Offset: 0x001E9DA8
		internal void FreeRecord(int record)
		{
			this._storage.Set(record, this._storage.NullValue);
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060004F9 RID: 1273 RVA: 0x001EA9D8 File Offset: 0x001E9DD8
		// (set) Token: 0x060004FA RID: 1274 RVA: 0x001EA9F8 File Offset: 0x001E9DF8
		[ResCategory("DataCategory_Data")]
		[DefaultValue(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[ResDescription("DataColumnUniqueDescr")]
		public bool Unique
		{
			get
			{
				return this.unique;
			}
			set
			{
				IntPtr intPtr;
				Bid.ScopeEnter(out intPtr, "<ds.DataColumn.set_Unique|API> %d#, %d{bool}\n", this.ObjectID, value);
				try
				{
					if (this.unique != value)
					{
						if (value && this.expression != null)
						{
							throw ExceptionBuilder.UniqueAndExpression();
						}
						UniqueConstraint constraint = null;
						if (this.table != null)
						{
							if (value)
							{
								this.CheckUnique();
							}
							else
							{
								foreach (object obj in this.Table.Constraints)
								{
									UniqueConstraint uniqueConstraint = obj as UniqueConstraint;
									if (uniqueConstraint != null && uniqueConstraint.ColumnsReference.Length == 1 && uniqueConstraint.ColumnsReference[0] == this)
									{
										constraint = uniqueConstraint;
									}
								}
								this.table.Constraints.CanRemove(constraint, true);
							}
						}
						this.unique = value;
						if (this.table != null)
						{
							if (value)
							{
								UniqueConstraint constraint2 = new UniqueConstraint(this);
								this.table.Constraints.Add(constraint2);
							}
							else
							{
								this.table.Constraints.Remove(constraint);
							}
						}
					}
				}
				finally
				{
					Bid.ScopeLeave(ref intPtr);
				}
			}
		}

		// Token: 0x060004FB RID: 1275 RVA: 0x001EAB08 File Offset: 0x001E9F08
		internal void InternalUnique(bool value)
		{
			this.unique = value;
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060004FC RID: 1276 RVA: 0x001EAB28 File Offset: 0x001E9F28
		// (set) Token: 0x060004FD RID: 1277 RVA: 0x001EAB48 File Offset: 0x001E9F48
		internal string XmlDataType
		{
			get
			{
				return this.dttype;
			}
			set
			{
				this.dttype = value;
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060004FE RID: 1278 RVA: 0x001EAB68 File Offset: 0x001E9F68
		// (set) Token: 0x060004FF RID: 1279 RVA: 0x001EAB88 File Offset: 0x001E9F88
		internal SimpleType SimpleType
		{
			get
			{
				return this.simpleType;
			}
			set
			{
				this.simpleType = value;
				if (value != null && value.CanHaveMaxLength())
				{
					this.maxLength = this.simpleType.MaxLength;
				}
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000500 RID: 1280 RVA: 0x001EABB8 File Offset: 0x001E9FB8
		// (set) Token: 0x06000501 RID: 1281 RVA: 0x001EABD8 File Offset: 0x001E9FD8
		[ResDescription("DataColumnMappingDescr")]
		[DefaultValue(MappingType.Element)]
		public virtual MappingType ColumnMapping
		{
			get
			{
				return this.columnMapping;
			}
			set
			{
				Bid.Trace("<ds.DataColumn.set_ColumnMapping|API> %d#, %d{ds.MappingType}\n", this.ObjectID, (int)value);
				if (value != this.columnMapping)
				{
					if (value == MappingType.SimpleContent && this.table != null)
					{
						int num = 0;
						if (this.columnMapping == MappingType.Element)
						{
							num = 1;
						}
						if (this.dataType == typeof(char))
						{
							throw ExceptionBuilder.CannotSetSimpleContent(this.ColumnName, this.dataType);
						}
						if (this.table.XmlText != null && this.table.XmlText != this)
						{
							throw ExceptionBuilder.CannotAddColumn3();
						}
						if (this.table.ElementColumnCount > num)
						{
							throw ExceptionBuilder.CannotAddColumn4(this.ColumnName);
						}
					}
					this.RaisePropertyChanging("ColumnMapping");
					if (this.table != null)
					{
						if (this.columnMapping == MappingType.SimpleContent)
						{
							this.table.xmlText = null;
						}
						if (value == MappingType.Element)
						{
							this.table.ElementColumnCount++;
						}
						else if (this.columnMapping == MappingType.Element)
						{
							this.table.ElementColumnCount--;
						}
					}
					this.columnMapping = value;
					if (value == MappingType.SimpleContent)
					{
						this._columnUri = null;
						if (this.table != null)
						{
							this.table.XmlText = this;
						}
						this.SimpleType = null;
					}
				}
			}
		}

		// Token: 0x06000502 RID: 1282 RVA: 0x001EAD08 File Offset: 0x001EA108
		internal void Description(string value)
		{
			if (value == null)
			{
				value = "";
			}
			this.description = value;
		}

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06000503 RID: 1283 RVA: 0x001EAD28 File Offset: 0x001EA128
		// (remove) Token: 0x06000504 RID: 1284 RVA: 0x001EAD58 File Offset: 0x001EA158
		internal event PropertyChangedEventHandler PropertyChanging
		{
			add
			{
				this.onPropertyChangingDelegate = (PropertyChangedEventHandler)Delegate.Combine(this.onPropertyChangingDelegate, value);
			}
			remove
			{
				this.onPropertyChangingDelegate = (PropertyChangedEventHandler)Delegate.Remove(this.onPropertyChangingDelegate, value);
			}
		}

		// Token: 0x06000505 RID: 1285 RVA: 0x001EAD88 File Offset: 0x001EA188
		internal void CheckColumnConstraint(DataRow row, DataRowAction action)
		{
			if (this.table.UpdatingCurrent(row, action))
			{
				this.CheckNullable(row);
				this.CheckMaxLength(row);
			}
		}

		// Token: 0x06000506 RID: 1286 RVA: 0x001EADB8 File Offset: 0x001EA1B8
		internal bool CheckMaxLength()
		{
			if (0 <= this.maxLength && this.Table != null && 0 < this.Table.Rows.Count)
			{
				foreach (object obj in this.Table.Rows)
				{
					DataRow dataRow = (DataRow)obj;
					if (dataRow.HasVersion(DataRowVersion.Current) && this.maxLength < this.GetStringLength(dataRow.GetCurrentRecordNo()))
					{
						return false;
					}
				}
				return true;
			}
			return true;
		}

		// Token: 0x06000507 RID: 1287 RVA: 0x001EAE78 File Offset: 0x001EA278
		internal void CheckMaxLength(DataRow dr)
		{
			if (0 <= this.maxLength && this.maxLength < this.GetStringLength(dr.GetDefaultRecord()))
			{
				throw ExceptionBuilder.LongerThanMaxLength(this);
			}
		}

		// Token: 0x06000508 RID: 1288 RVA: 0x001EAEB8 File Offset: 0x001EA2B8
		protected internal void CheckNotAllowNull()
		{
			if (this._storage == null)
			{
				return;
			}
			if (this.sortIndex != null)
			{
				if (this.sortIndex.IsKeyInIndex(this._storage.NullValue))
				{
					throw ExceptionBuilder.NullKeyValues(this.ColumnName);
				}
			}
			else
			{
				foreach (object obj in this.table.Rows)
				{
					DataRow dataRow = (DataRow)obj;
					if (dataRow.RowState != DataRowState.Deleted)
					{
						if (!this.implementsINullable)
						{
							if (dataRow[this] == DBNull.Value)
							{
								throw ExceptionBuilder.NullKeyValues(this.ColumnName);
							}
						}
						else if (DataStorage.IsObjectNull(dataRow[this]))
						{
							throw ExceptionBuilder.NullKeyValues(this.ColumnName);
						}
					}
				}
			}
		}

		// Token: 0x06000509 RID: 1289 RVA: 0x001EAF98 File Offset: 0x001EA398
		internal void CheckNullable(DataRow row)
		{
			if (!this.AllowDBNull && this._storage.IsNull(row.GetDefaultRecord()))
			{
				throw ExceptionBuilder.NullValues(this.ColumnName);
			}
		}

		// Token: 0x0600050A RID: 1290 RVA: 0x001EAFD8 File Offset: 0x001EA3D8
		protected void CheckUnique()
		{
			if (!this.SortIndex.CheckUnique())
			{
				throw ExceptionBuilder.NonUniqueValues(this.ColumnName);
			}
		}

		// Token: 0x0600050B RID: 1291 RVA: 0x001EB008 File Offset: 0x001EA408
		internal int Compare(int record1, int record2)
		{
			return this._storage.Compare(record1, record2);
		}

		// Token: 0x0600050C RID: 1292 RVA: 0x001EB028 File Offset: 0x001EA428
		internal bool CompareValueTo(int record1, object value, bool checkType)
		{
			if (this.CompareValueTo(record1, value) == 0)
			{
				Type type = value.GetType();
				Type type2 = this._storage.Get(record1).GetType();
				if (type == typeof(string) && type2 == typeof(string))
				{
					return string.CompareOrdinal((string)this._storage.Get(record1), (string)value) == 0;
				}
				if (type == type2)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600050D RID: 1293 RVA: 0x001EB0A8 File Offset: 0x001EA4A8
		internal int CompareValueTo(int record1, object value)
		{
			return this._storage.CompareValueTo(record1, value);
		}

		// Token: 0x0600050E RID: 1294 RVA: 0x001EB0C8 File Offset: 0x001EA4C8
		internal object ConvertValue(object value)
		{
			return this._storage.ConvertValue(value);
		}

		// Token: 0x0600050F RID: 1295 RVA: 0x001EB0E8 File Offset: 0x001EA4E8
		internal void Copy(int srcRecordNo, int dstRecordNo)
		{
			this._storage.Copy(srcRecordNo, dstRecordNo);
		}

		// Token: 0x06000510 RID: 1296 RVA: 0x001EB108 File Offset: 0x001EA508
		internal DataColumn Clone()
		{
			DataColumn dataColumn = (DataColumn)Activator.CreateInstance(base.GetType());
			dataColumn.SimpleType = this.SimpleType;
			dataColumn.allowNull = this.allowNull;
			dataColumn.autoIncrement = this.autoIncrement;
			dataColumn.autoIncrementStep = this.autoIncrementStep;
			dataColumn.autoIncrementSeed = this.autoIncrementSeed;
			dataColumn.autoIncrementCurrent = this.autoIncrementCurrent;
			dataColumn.caption = this.caption;
			dataColumn.ColumnName = this.ColumnName;
			dataColumn._columnUri = this._columnUri;
			dataColumn._columnPrefix = this._columnPrefix;
			dataColumn.DataType = this.DataType;
			dataColumn.defaultValue = this.defaultValue;
			dataColumn.defaultValueIsNull = (this.defaultValue == DBNull.Value || (dataColumn.ImplementsINullable && DataStorage.IsObjectSqlNull(this.defaultValue)));
			dataColumn.columnMapping = this.columnMapping;
			dataColumn.readOnly = this.readOnly;
			dataColumn.MaxLength = this.MaxLength;
			dataColumn.dttype = this.dttype;
			dataColumn._dateTimeMode = this._dateTimeMode;
			if (this.extendedProperties != null)
			{
				foreach (object key in this.extendedProperties.Keys)
				{
					dataColumn.ExtendedProperties[key] = this.extendedProperties[key];
				}
			}
			return dataColumn;
		}

		// Token: 0x06000511 RID: 1297 RVA: 0x001EB298 File Offset: 0x001EA698
		internal DataRelation FindParentRelation()
		{
			DataRelation[] array = new DataRelation[this.Table.ParentRelations.Count];
			this.Table.ParentRelations.CopyTo(array, 0);
			foreach (DataRelation dataRelation in array)
			{
				DataKey childKey = dataRelation.ChildKey;
				if (childKey.ColumnsReference.Length == 1 && childKey.ColumnsReference[0] == this)
				{
					return dataRelation;
				}
			}
			return null;
		}

		// Token: 0x06000512 RID: 1298 RVA: 0x001EB308 File Offset: 0x001EA708
		internal object GetAggregateValue(int[] records, AggregateType kind)
		{
			if (this._storage != null)
			{
				return this._storage.Aggregate(records, kind);
			}
			if (kind == AggregateType.Count)
			{
				return 0;
			}
			return DBNull.Value;
		}

		// Token: 0x06000513 RID: 1299 RVA: 0x001EB348 File Offset: 0x001EA748
		private int GetStringLength(int record)
		{
			return this._storage.GetStringLength(record);
		}

		// Token: 0x06000514 RID: 1300 RVA: 0x001EB368 File Offset: 0x001EA768
		internal void Init(int record)
		{
			if (this.AutoIncrement)
			{
				object value = this.autoIncrementCurrent;
				this.autoIncrementCurrent += this.autoIncrementStep;
				this._storage.Set(record, value);
				return;
			}
			this[record] = this.defaultValue;
		}

		// Token: 0x06000515 RID: 1301 RVA: 0x001EB3B8 File Offset: 0x001EA7B8
		internal static bool IsAutoIncrementType(Type dataType)
		{
			return dataType == typeof(int) || dataType == typeof(long) || dataType == typeof(short) || dataType == typeof(decimal) || dataType == typeof(SqlInt32) || dataType == typeof(SqlInt64) || dataType == typeof(SqlInt16) || dataType == typeof(SqlDecimal);
		}

		// Token: 0x06000516 RID: 1302 RVA: 0x001EB438 File Offset: 0x001EA838
		private bool IsColumnMappingValid(StorageType typeCode, MappingType mapping)
		{
			return mapping == MappingType.Element || !DataStorage.IsTypeCustomType(typeCode);
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06000517 RID: 1303 RVA: 0x001EB458 File Offset: 0x001EA858
		internal bool IsCustomType
		{
			get
			{
				if (this._storage != null)
				{
					return this._storage.IsCustomDefinedType;
				}
				return DataStorage.IsTypeCustomType(this.DataType);
			}
		}

		// Token: 0x06000518 RID: 1304 RVA: 0x001EB488 File Offset: 0x001EA888
		internal bool IsValueCustomTypeInstance(object value)
		{
			return DataStorage.IsTypeCustomType(value.GetType()) && !(value is Type);
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x06000519 RID: 1305 RVA: 0x001EB4B8 File Offset: 0x001EA8B8
		internal bool ImplementsIXMLSerializable
		{
			get
			{
				return this.implementsIXMLSerializable;
			}
		}

		// Token: 0x0600051A RID: 1306 RVA: 0x001EB4D8 File Offset: 0x001EA8D8
		internal bool IsNull(int record)
		{
			return this._storage.IsNull(record);
		}

		// Token: 0x0600051B RID: 1307 RVA: 0x001EB4F8 File Offset: 0x001EA8F8
		internal bool IsInRelation()
		{
			DataRelationCollection dataRelationCollection = this.table.ParentRelations;
			for (int i = 0; i < dataRelationCollection.Count; i++)
			{
				if (dataRelationCollection[i].ChildKey.ContainsColumn(this))
				{
					return true;
				}
			}
			dataRelationCollection = this.table.ChildRelations;
			for (int j = 0; j < dataRelationCollection.Count; j++)
			{
				if (dataRelationCollection[j].ParentKey.ContainsColumn(this))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600051C RID: 1308 RVA: 0x001EB578 File Offset: 0x001EA978
		internal bool IsMaxLengthViolated()
		{
			if (this.MaxLength < 0)
			{
				return true;
			}
			bool result = false;
			string text = null;
			foreach (object obj in this.Table.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				if (dataRow.HasVersion(DataRowVersion.Current))
				{
					object obj2 = dataRow[this];
					if (!this.isSqlType)
					{
						if (obj2 != null && obj2 != DBNull.Value && ((string)obj2).Length > this.MaxLength)
						{
							if (text == null)
							{
								text = ExceptionBuilder.MaxLengthViolationText(this.ColumnName);
							}
							dataRow.RowError = text;
							dataRow.SetColumnError(this, text);
							result = true;
						}
					}
					else if (!DataStorage.IsObjectNull(obj2) && ((SqlString)obj2).Value.Length > this.MaxLength)
					{
						if (text == null)
						{
							text = ExceptionBuilder.MaxLengthViolationText(this.ColumnName);
						}
						dataRow.RowError = text;
						dataRow.SetColumnError(this, text);
						result = true;
					}
				}
			}
			return result;
		}

		// Token: 0x0600051D RID: 1309 RVA: 0x001EB6A8 File Offset: 0x001EAAA8
		internal bool IsNotAllowDBNullViolated()
		{
			Index index = this.SortIndex;
			DataRow[] rows = index.GetRows(index.FindRecords(DBNull.Value));
			for (int i = 0; i < rows.Length; i++)
			{
				string text = ExceptionBuilder.NotAllowDBNullViolationText(this.ColumnName);
				rows[i].RowError = text;
				rows[i].SetColumnError(this, text);
			}
			return rows.Length > 0;
		}

		// Token: 0x0600051E RID: 1310 RVA: 0x001EB708 File Offset: 0x001EAB08
		internal void FinishInitInProgress()
		{
			if (this.Computed)
			{
				this.BindExpression();
			}
		}

		// Token: 0x0600051F RID: 1311 RVA: 0x001EB728 File Offset: 0x001EAB28
		protected virtual void OnPropertyChanging(PropertyChangedEventArgs pcevent)
		{
			if (this.onPropertyChangingDelegate != null)
			{
				this.onPropertyChangingDelegate(this, pcevent);
			}
		}

		// Token: 0x06000520 RID: 1312 RVA: 0x001EB758 File Offset: 0x001EAB58
		protected internal void RaisePropertyChanging(string name)
		{
			this.OnPropertyChanging(new PropertyChangedEventArgs(name));
		}

		// Token: 0x06000521 RID: 1313 RVA: 0x001EB778 File Offset: 0x001EAB78
		private void InsureStorage()
		{
			if (this._storage == null)
			{
				this._storage = DataStorage.CreateStorage(this, this.dataType);
			}
		}

		// Token: 0x06000522 RID: 1314 RVA: 0x001EB7A8 File Offset: 0x001EABA8
		internal void SetCapacity(int capacity)
		{
			this.InsureStorage();
			this._storage.SetCapacity(capacity);
		}

		// Token: 0x06000523 RID: 1315 RVA: 0x001EB7C8 File Offset: 0x001EABC8
		private bool ShouldSerializeDefaultValue()
		{
			return !this.DefaultValueIsNull;
		}

		// Token: 0x06000524 RID: 1316 RVA: 0x001EB7E8 File Offset: 0x001EABE8
		internal void OnSetDataSet()
		{
		}

		// Token: 0x06000525 RID: 1317 RVA: 0x001EB7F8 File Offset: 0x001EABF8
		public override string ToString()
		{
			if (this.expression == null)
			{
				return this.ColumnName;
			}
			return this.ColumnName + " + " + this.Expression;
		}

		// Token: 0x06000526 RID: 1318 RVA: 0x001EB838 File Offset: 0x001EAC38
		internal object ConvertXmlToObject(string s)
		{
			this.InsureStorage();
			return this._storage.ConvertXmlToObject(s);
		}

		// Token: 0x06000527 RID: 1319 RVA: 0x001EB858 File Offset: 0x001EAC58
		internal object ConvertXmlToObject(XmlReader xmlReader, XmlRootAttribute xmlAttrib)
		{
			this.InsureStorage();
			return this._storage.ConvertXmlToObject(xmlReader, xmlAttrib);
		}

		// Token: 0x06000528 RID: 1320 RVA: 0x001EB878 File Offset: 0x001EAC78
		internal string ConvertObjectToXml(object value)
		{
			this.InsureStorage();
			return this._storage.ConvertObjectToXml(value);
		}

		// Token: 0x06000529 RID: 1321 RVA: 0x001EB898 File Offset: 0x001EAC98
		internal void ConvertObjectToXml(object value, XmlWriter xmlWriter, XmlRootAttribute xmlAttrib)
		{
			this.InsureStorage();
			this._storage.ConvertObjectToXml(value, xmlWriter, xmlAttrib);
		}

		// Token: 0x0600052A RID: 1322 RVA: 0x001EB8C8 File Offset: 0x001EACC8
		internal object GetEmptyColumnStore(int recordCount)
		{
			this.InsureStorage();
			return this._storage.GetEmptyStorageInternal(recordCount);
		}

		// Token: 0x0600052B RID: 1323 RVA: 0x001EB8E8 File Offset: 0x001EACE8
		internal void CopyValueIntoStore(int record, object store, BitArray nullbits, int storeIndex)
		{
			this._storage.CopyValueInternal(record, store, nullbits, storeIndex);
		}

		// Token: 0x0600052C RID: 1324 RVA: 0x001EB908 File Offset: 0x001EAD08
		internal void SetStorage(object store, BitArray nullbits)
		{
			this.InsureStorage();
			this._storage.SetStorageInternal(store, nullbits);
		}

		// Token: 0x0600052D RID: 1325 RVA: 0x001EB928 File Offset: 0x001EAD28
		internal void AddDependentColumn(DataColumn expressionColumn)
		{
			if (this.dependentColumns == null)
			{
				this.dependentColumns = new List<DataColumn>();
			}
			this.dependentColumns.Add(expressionColumn);
			this.table.AddDependentColumn(expressionColumn);
		}

		// Token: 0x0600052E RID: 1326 RVA: 0x001EB968 File Offset: 0x001EAD68
		internal void RemoveDependentColumn(DataColumn expressionColumn)
		{
			if (this.dependentColumns != null && this.dependentColumns.Contains(expressionColumn))
			{
				this.dependentColumns.Remove(expressionColumn);
			}
			this.table.RemoveDependentColumn(expressionColumn);
		}

		// Token: 0x0600052F RID: 1327 RVA: 0x001EB9A8 File Offset: 0x001EADA8
		internal void HandleDependentColumnList(DataExpression oldExpression, DataExpression newExpression)
		{
			if (oldExpression != null)
			{
				DataColumn[] dependency = oldExpression.GetDependency();
				foreach (DataColumn dataColumn in dependency)
				{
					dataColumn.RemoveDependentColumn(this);
					if (dataColumn.table != this.table)
					{
						this.table.RemoveDependentColumn(this);
					}
				}
				this.table.RemoveDependentColumn(this);
			}
			if (newExpression != null)
			{
				DataColumn[] dependency = newExpression.GetDependency();
				foreach (DataColumn dataColumn2 in dependency)
				{
					dataColumn2.AddDependentColumn(this);
					if (dataColumn2.table != this.table)
					{
						this.table.AddDependentColumn(this);
					}
				}
				this.table.AddDependentColumn(this);
			}
		}

		// Token: 0x040006E3 RID: 1763
		private bool allowNull = true;

		// Token: 0x040006E4 RID: 1764
		private bool autoIncrement;

		// Token: 0x040006E5 RID: 1765
		private long autoIncrementStep = 1L;

		// Token: 0x040006E6 RID: 1766
		private long autoIncrementSeed;

		// Token: 0x040006E7 RID: 1767
		private string caption;

		// Token: 0x040006E8 RID: 1768
		private string _columnName;

		// Token: 0x040006E9 RID: 1769
		private Type dataType;

		// Token: 0x040006EA RID: 1770
		internal object defaultValue = DBNull.Value;

		// Token: 0x040006EB RID: 1771
		private DataSetDateTime _dateTimeMode = DataSetDateTime.UnspecifiedLocal;

		// Token: 0x040006EC RID: 1772
		private DataExpression expression;

		// Token: 0x040006ED RID: 1773
		private int maxLength = -1;

		// Token: 0x040006EE RID: 1774
		private int _ordinal = -1;

		// Token: 0x040006EF RID: 1775
		private bool readOnly;

		// Token: 0x040006F0 RID: 1776
		internal Index sortIndex;

		// Token: 0x040006F1 RID: 1777
		internal DataTable table;

		// Token: 0x040006F2 RID: 1778
		private bool unique;

		// Token: 0x040006F3 RID: 1779
		internal MappingType columnMapping = MappingType.Element;

		// Token: 0x040006F4 RID: 1780
		internal int _hashCode;

		// Token: 0x040006F5 RID: 1781
		internal int errors;

		// Token: 0x040006F6 RID: 1782
		private bool isSqlType;

		// Token: 0x040006F7 RID: 1783
		private bool implementsINullable;

		// Token: 0x040006F8 RID: 1784
		private bool implementsIChangeTracking;

		// Token: 0x040006F9 RID: 1785
		private bool implementsIRevertibleChangeTracking;

		// Token: 0x040006FA RID: 1786
		private bool implementsIXMLSerializable;

		// Token: 0x040006FB RID: 1787
		private bool defaultValueIsNull = true;

		// Token: 0x040006FC RID: 1788
		internal List<DataColumn> dependentColumns;

		// Token: 0x040006FD RID: 1789
		internal PropertyCollection extendedProperties;

		// Token: 0x040006FE RID: 1790
		private PropertyChangedEventHandler onPropertyChangingDelegate;

		// Token: 0x040006FF RID: 1791
		private DataStorage _storage;

		// Token: 0x04000700 RID: 1792
		internal long autoIncrementCurrent;

		// Token: 0x04000701 RID: 1793
		internal string _columnUri;

		// Token: 0x04000702 RID: 1794
		private string _columnPrefix = "";

		// Token: 0x04000703 RID: 1795
		internal string encodedColumnName;

		// Token: 0x04000704 RID: 1796
		internal string description = "";

		// Token: 0x04000705 RID: 1797
		internal string dttype = "";

		// Token: 0x04000706 RID: 1798
		internal SimpleType simpleType;

		// Token: 0x04000707 RID: 1799
		private static int _objectTypeCount;

		// Token: 0x04000708 RID: 1800
		private readonly int _objectID = Interlocked.Increment(ref DataColumn._objectTypeCount);
	}
}
