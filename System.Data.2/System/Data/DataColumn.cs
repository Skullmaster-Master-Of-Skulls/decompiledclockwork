using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Globalization;
using System.Numerics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Xml;
using System.Xml.Serialization;

namespace System.Data
{
	// Token: 0x020000A4 RID: 164
	[DesignTimeVisible(false)]
	[Editor("Microsoft.VSDesigner.Data.Design.DataColumnEditor, Microsoft.VSDesigner, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[ToolboxItem(false)]
	[DefaultProperty("ColumnName")]
	public class DataColumn : MarshalByValueComponent
	{
		// Token: 0x06000832 RID: 2098 RVA: 0x00057FE4 File Offset: 0x000573E4
		public DataColumn() : this(null, typeof(string), null, MappingType.Element)
		{
		}

		// Token: 0x06000833 RID: 2099 RVA: 0x00058004 File Offset: 0x00057404
		public DataColumn(string columnName) : this(columnName, typeof(string), null, MappingType.Element)
		{
		}

		// Token: 0x06000834 RID: 2100 RVA: 0x00058024 File Offset: 0x00057424
		public DataColumn(string columnName, Type dataType) : this(columnName, dataType, null, MappingType.Element)
		{
		}

		// Token: 0x06000835 RID: 2101 RVA: 0x0005803C File Offset: 0x0005743C
		public DataColumn(string columnName, Type dataType, string expr) : this(columnName, dataType, expr, MappingType.Element)
		{
		}

		// Token: 0x06000836 RID: 2102 RVA: 0x00058054 File Offset: 0x00057454
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
			this._columnName = (columnName ?? string.Empty);
			SimpleType simpleType = SimpleType.CreateSimpleType(storageType, dataType);
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

		// Token: 0x06000837 RID: 2103 RVA: 0x0005814C File Offset: 0x0005754C
		private void UpdateColumnType(Type type, StorageType typeCode)
		{
			TypeLimiter.EnsureTypeIsAllowed(type, null);
			this.dataType = type;
			this._storageType = typeCode;
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

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x06000838 RID: 2104 RVA: 0x000581B8 File Offset: 0x000575B8
		// (set) Token: 0x06000839 RID: 2105 RVA: 0x000581CC File Offset: 0x000575CC
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

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x0600083A RID: 2106 RVA: 0x00058240 File Offset: 0x00057640
		// (set) Token: 0x0600083B RID: 2107 RVA: 0x00058264 File Offset: 0x00057664
		[ResCategory("DataCategory_Data")]
		[ResDescription("DataColumnAutoIncrementDescr")]
		[RefreshProperties(RefreshProperties.All)]
		[DefaultValue(false)]
		public bool AutoIncrement
		{
			get
			{
				return this.autoInc != null && this.autoInc.Auto;
			}
			set
			{
				Bid.Trace("<ds.DataColumn.set_AutoIncrement|API> %d#, %d{bool}\n", this.ObjectID, value);
				if (this.AutoIncrement != value)
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
					this.AutoInc.Auto = value;
				}
			}
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x0600083C RID: 2108 RVA: 0x000582EC File Offset: 0x000576EC
		// (set) Token: 0x0600083D RID: 2109 RVA: 0x00058318 File Offset: 0x00057718
		internal object AutoIncrementCurrent
		{
			get
			{
				if (this.autoInc == null)
				{
					return this.AutoIncrementSeed;
				}
				return this.autoInc.Current;
			}
			set
			{
				if (this.AutoIncrementSeed != BigIntegerStorage.ConvertToBigInteger(value, this.FormatProvider))
				{
					this.AutoInc.SetCurrent(value, this.FormatProvider);
				}
			}
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x0600083E RID: 2110 RVA: 0x00058358 File Offset: 0x00057758
		internal AutoIncrementValue AutoInc
		{
			get
			{
				AutoIncrementValue result;
				if ((result = this.autoInc) == null)
				{
					result = (this.autoInc = ((this.DataType == typeof(BigInteger)) ? new AutoIncrementBigInteger() : new AutoIncrementInt64()));
				}
				return result;
			}
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x0600083F RID: 2111 RVA: 0x0005839C File Offset: 0x0005779C
		// (set) Token: 0x06000840 RID: 2112 RVA: 0x000583C0 File Offset: 0x000577C0
		[ResCategory("DataCategory_Data")]
		[ResDescription("DataColumnAutoIncrementSeedDescr")]
		[DefaultValue(0L)]
		public long AutoIncrementSeed
		{
			get
			{
				if (this.autoInc == null)
				{
					return 0L;
				}
				return this.autoInc.Seed;
			}
			set
			{
				Bid.Trace("<ds.DataColumn.set_AutoIncrementSeed|API> %d#, %I64d\n", this.ObjectID, value);
				if (this.AutoIncrementSeed != value)
				{
					this.AutoInc.Seed = value;
				}
			}
		}

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x06000841 RID: 2113 RVA: 0x000583F4 File Offset: 0x000577F4
		// (set) Token: 0x06000842 RID: 2114 RVA: 0x00058418 File Offset: 0x00057818
		[ResDescription("DataColumnAutoIncrementStepDescr")]
		[DefaultValue(1L)]
		[ResCategory("DataCategory_Data")]
		public long AutoIncrementStep
		{
			get
			{
				if (this.autoInc == null)
				{
					return 1L;
				}
				return this.autoInc.Step;
			}
			set
			{
				Bid.Trace("<ds.DataColumn.set_AutoIncrementStep|API> %d#, %I64d\n", this.ObjectID, value);
				if (this.AutoIncrementStep != value)
				{
					this.AutoInc.Step = value;
				}
			}
		}

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x06000843 RID: 2115 RVA: 0x0005844C File Offset: 0x0005784C
		// (set) Token: 0x06000844 RID: 2116 RVA: 0x00058470 File Offset: 0x00057870
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

		// Token: 0x06000845 RID: 2117 RVA: 0x000584AC File Offset: 0x000578AC
		private void ResetCaption()
		{
			if (this.caption != null)
			{
				this.caption = null;
			}
		}

		// Token: 0x06000846 RID: 2118 RVA: 0x000584C8 File Offset: 0x000578C8
		private bool ShouldSerializeCaption()
		{
			return this.caption != null;
		}

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x06000847 RID: 2119 RVA: 0x000584E0 File Offset: 0x000578E0
		// (set) Token: 0x06000848 RID: 2120 RVA: 0x000584F4 File Offset: 0x000578F4
		[RefreshProperties(RefreshProperties.All)]
		[ResCategory("DataCategory_Data")]
		[DefaultValue("")]
		[ResDescription("DataColumnColumnNameDescr")]
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
							this.table.Columns.RegisterColumnName(value, this);
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

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x06000849 RID: 2121 RVA: 0x00058628 File Offset: 0x00057A28
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

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x0600084A RID: 2122 RVA: 0x00058654 File Offset: 0x00057A54
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

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x0600084B RID: 2123 RVA: 0x0005867C File Offset: 0x00057A7C
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

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x0600084C RID: 2124 RVA: 0x000586A4 File Offset: 0x00057AA4
		internal int ObjectID
		{
			get
			{
				return this._objectID;
			}
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x0600084D RID: 2125 RVA: 0x000586B8 File Offset: 0x00057AB8
		// (set) Token: 0x0600084E RID: 2126 RVA: 0x000586CC File Offset: 0x00057ACC
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

		// Token: 0x0600084F RID: 2127 RVA: 0x00058720 File Offset: 0x00057B20
		internal string GetColumnValueAsString(DataRow row, DataRowVersion version)
		{
			object value = this[row.GetRecordFromVersion(version)];
			if (DataStorage.IsObjectNull(value))
			{
				return null;
			}
			return this.ConvertObjectToXml(value);
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x06000850 RID: 2128 RVA: 0x00058750 File Offset: 0x00057B50
		internal bool Computed
		{
			get
			{
				return this.expression != null;
			}
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x06000851 RID: 2129 RVA: 0x00058768 File Offset: 0x00057B68
		internal DataExpression DataExpression
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x06000852 RID: 2130 RVA: 0x0005877C File Offset: 0x00057B7C
		// (set) Token: 0x06000853 RID: 2131 RVA: 0x00058790 File Offset: 0x00057B90
		[RefreshProperties(RefreshProperties.All)]
		[ResDescription("DataColumnDataTypeDescr")]
		[ResCategory("DataCategory_Data")]
		[DefaultValue(typeof(string))]
		[TypeConverter(typeof(ColumnTypeConverter))]
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
					if (storageType == StorageType.BigInteger && this.expression != null)
					{
						throw ExprException.UnsupportedDataType(value);
					}
					if (!this.DefaultValueIsNull)
					{
						try
						{
							if (this.defaultValue is BigInteger)
							{
								this.defaultValue = BigIntegerStorage.ConvertFromBigInteger((BigInteger)this.defaultValue, value, this.FormatProvider);
							}
							else if (typeof(BigInteger) == value)
							{
								this.defaultValue = BigIntegerStorage.ConvertToBigInteger(this.defaultValue, this.FormatProvider);
							}
							else if (typeof(string) == value)
							{
								this.defaultValue = this.DefaultValue.ToString();
							}
							else if (typeof(SqlString) == value)
							{
								this.defaultValue = SqlConvert.ConvertToSqlString(this.DefaultValue);
							}
							else if (typeof(object) != value)
							{
								this.DefaultValue = SqlConvert.ChangeTypeForDefaultValue(this.DefaultValue, value, this.FormatProvider);
							}
						}
						catch (InvalidCastException inner)
						{
							throw ExceptionBuilder.DefaultValueDataType(this.ColumnName, this.DefaultValue.GetType(), value, inner);
						}
						catch (FormatException inner2)
						{
							throw ExceptionBuilder.DefaultValueDataType(this.ColumnName, this.DefaultValue.GetType(), value, inner2);
						}
					}
					if (this.ColumnMapping == MappingType.SimpleContent && value == typeof(char))
					{
						throw ExceptionBuilder.CannotSetSimpleContentType(this.ColumnName, value);
					}
					this.SimpleType = SimpleType.CreateSimpleType(storageType, value);
					if (StorageType.String == storageType)
					{
						this.maxLength = -1;
					}
					this.UpdateColumnType(value, storageType);
					this.XmlDataType = null;
					if (this.AutoIncrement)
					{
						if (!DataColumn.IsAutoIncrementType(value))
						{
							this.AutoIncrement = false;
						}
						if (this.autoInc != null)
						{
							AutoIncrementValue autoIncrementValue = this.autoInc;
							this.autoInc = null;
							this.AutoInc.Auto = autoIncrementValue.Auto;
							this.AutoInc.Seed = autoIncrementValue.Seed;
							this.AutoInc.Step = autoIncrementValue.Step;
							if (this.autoInc.DataType == autoIncrementValue.DataType)
							{
								this.autoInc.Current = autoIncrementValue.Current;
								return;
							}
							if (autoIncrementValue.DataType == typeof(long))
							{
								this.AutoInc.Current = (long)autoIncrementValue.Current;
								return;
							}
							this.AutoInc.Current = (long)((BigInteger)autoIncrementValue.Current);
						}
					}
				}
			}
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x06000854 RID: 2132 RVA: 0x00058A98 File Offset: 0x00057E98
		// (set) Token: 0x06000855 RID: 2133 RVA: 0x00058AAC File Offset: 0x00057EAC
		[ResCategory("DataCategory_Data")]
		[RefreshProperties(RefreshProperties.All)]
		[DefaultValue(DataSetDateTime.UnspecifiedLocal)]
		[ResDescription("DataColumnDateTimeModeDescr")]
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

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x06000856 RID: 2134 RVA: 0x00058B4C File Offset: 0x00057F4C
		// (set) Token: 0x06000857 RID: 2135 RVA: 0x00058BF0 File Offset: 0x00057FF0
		[TypeConverter(typeof(DefaultValueTypeConverter))]
		[ResCategory("DataCategory_Data")]
		[ResDescription("DataColumnDefaultValueDescr")]
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
						this.defaultValue = SqlConvert.ChangeTypeForDefaultValue(this.defaultValue, this.dataType, this.FormatProvider);
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
							obj = SqlConvert.ChangeTypeForDefaultValue(obj, this.DataType, this.FormatProvider);
						}
						catch (InvalidCastException inner)
						{
							throw ExceptionBuilder.DefaultValueColumnDataType(this.ColumnName, obj.GetType(), this.DataType, inner);
						}
					}
					this.defaultValue = obj;
					this.defaultValueIsNull = (obj == DBNull.Value || (this.ImplementsINullable && DataStorage.IsObjectSqlNull(obj)));
				}
			}
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x06000858 RID: 2136 RVA: 0x00058CD8 File Offset: 0x000580D8
		internal bool DefaultValueIsNull
		{
			get
			{
				return this.defaultValueIsNull;
			}
		}

		// Token: 0x06000859 RID: 2137 RVA: 0x00058CEC File Offset: 0x000580EC
		internal void BindExpression()
		{
			this.DataExpression.Bind(this.table);
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x0600085A RID: 2138 RVA: 0x00058D0C File Offset: 0x0005810C
		// (set) Token: 0x0600085B RID: 2139 RVA: 0x00058D34 File Offset: 0x00058134
		[RefreshProperties(RefreshProperties.All)]
		[ResCategory("DataCategory_Data")]
		[DefaultValue("")]
		[ResDescription("DataColumnExpressionDescr")]
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
							return;
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
				}
				finally
				{
					Bid.ScopeLeave(ref intPtr);
				}
			}
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x0600085C RID: 2140 RVA: 0x00058FB4 File Offset: 0x000583B4
		[ResCategory("DataCategory_Data")]
		[Browsable(false)]
		[ResDescription("ExtendedPropertiesDescr")]
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

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x0600085D RID: 2141 RVA: 0x00058FDC File Offset: 0x000583DC
		internal bool HasData
		{
			get
			{
				return this._storage != null;
			}
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x0600085E RID: 2142 RVA: 0x00058FF4 File Offset: 0x000583F4
		internal bool ImplementsINullable
		{
			get
			{
				return this.implementsINullable;
			}
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x0600085F RID: 2143 RVA: 0x00059008 File Offset: 0x00058408
		internal bool ImplementsIChangeTracking
		{
			get
			{
				return this.implementsIChangeTracking;
			}
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x06000860 RID: 2144 RVA: 0x0005901C File Offset: 0x0005841C
		internal bool ImplementsIRevertibleChangeTracking
		{
			get
			{
				return this.implementsIRevertibleChangeTracking;
			}
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x06000861 RID: 2145 RVA: 0x00059030 File Offset: 0x00058430
		internal bool IsCloneable
		{
			get
			{
				return this._storage.IsCloneable;
			}
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x06000862 RID: 2146 RVA: 0x00059048 File Offset: 0x00058448
		internal bool IsStringType
		{
			get
			{
				return this._storage.IsStringType;
			}
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x06000863 RID: 2147 RVA: 0x00059060 File Offset: 0x00058460
		internal bool IsValueType
		{
			get
			{
				return this._storage.IsValueType;
			}
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x06000864 RID: 2148 RVA: 0x00059078 File Offset: 0x00058478
		internal bool IsSqlType
		{
			get
			{
				return this.isSqlType;
			}
		}

		// Token: 0x06000865 RID: 2149 RVA: 0x0005908C File Offset: 0x0005848C
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

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x06000866 RID: 2150 RVA: 0x0005910C File Offset: 0x0005850C
		// (set) Token: 0x06000867 RID: 2151 RVA: 0x00059120 File Offset: 0x00058520
		[ResCategory("DataCategory_Data")]
		[DefaultValue(-1)]
		[ResDescription("DataColumnMaxLengthDescr")]
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

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x06000868 RID: 2152 RVA: 0x00059208 File Offset: 0x00058608
		// (set) Token: 0x06000869 RID: 2153 RVA: 0x00059248 File Offset: 0x00058648
		[ResDescription("DataColumnNamespaceDescr")]
		[ResCategory("DataCategory_Data")]
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

		// Token: 0x0600086A RID: 2154 RVA: 0x000592AC File Offset: 0x000586AC
		private bool ShouldSerializeNamespace()
		{
			return this._columnUri != null;
		}

		// Token: 0x0600086B RID: 2155 RVA: 0x000592C4 File Offset: 0x000586C4
		private void ResetNamespace()
		{
			this.Namespace = null;
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x0600086C RID: 2156 RVA: 0x000592D8 File Offset: 0x000586D8
		[ResCategory("DataCategory_Data")]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[ResDescription("DataColumnOrdinalDescr")]
		public int Ordinal
		{
			get
			{
				return this._ordinal;
			}
		}

		// Token: 0x0600086D RID: 2157 RVA: 0x000592EC File Offset: 0x000586EC
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

		// Token: 0x0600086E RID: 2158 RVA: 0x00059324 File Offset: 0x00058724
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

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x0600086F RID: 2159 RVA: 0x000593E0 File Offset: 0x000587E0
		// (set) Token: 0x06000870 RID: 2160 RVA: 0x000593F4 File Offset: 0x000587F4
		[ResDescription("DataColumnReadOnlyDescr")]
		[DefaultValue(false)]
		[ResCategory("DataCategory_Data")]
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

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x06000871 RID: 2161 RVA: 0x00059434 File Offset: 0x00058834
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

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x06000872 RID: 2162 RVA: 0x00059484 File Offset: 0x00058884
		[Browsable(false)]
		[ResDescription("DataColumnDataTableDescr")]
		[ResCategory("DataCategory_Data")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public DataTable Table
		{
			get
			{
				return this.table;
			}
		}

		// Token: 0x06000873 RID: 2163 RVA: 0x00059498 File Offset: 0x00058898
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

		// Token: 0x06000874 RID: 2164 RVA: 0x00059530 File Offset: 0x00058930
		private DataRow GetDataRow(int index)
		{
			return this.table.recordManager[index];
		}

		// Token: 0x1700014A RID: 330
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
				if (this.AutoIncrement && !this._storage.IsNull(record))
				{
					this.AutoInc.SetCurrentAndIncrement(this._storage.Get(record));
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

		// Token: 0x06000877 RID: 2167 RVA: 0x00059604 File Offset: 0x00058A04
		internal void InitializeRecord(int record)
		{
			this._storage.Set(record, this.DefaultValue);
		}

		// Token: 0x06000878 RID: 2168 RVA: 0x00059624 File Offset: 0x00058A24
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

		// Token: 0x06000879 RID: 2169 RVA: 0x00059688 File Offset: 0x00058A88
		internal void FreeRecord(int record)
		{
			this._storage.Set(record, this._storage.NullValue);
		}

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x0600087A RID: 2170 RVA: 0x000596AC File Offset: 0x00058AAC
		// (set) Token: 0x0600087B RID: 2171 RVA: 0x000596C0 File Offset: 0x00058AC0
		[ResCategory("DataCategory_Data")]
		[ResDescription("DataColumnUniqueDescr")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[DefaultValue(false)]
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

		// Token: 0x0600087C RID: 2172 RVA: 0x000597CC File Offset: 0x00058BCC
		internal void InternalUnique(bool value)
		{
			this.unique = value;
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x0600087D RID: 2173 RVA: 0x000597E0 File Offset: 0x00058BE0
		// (set) Token: 0x0600087E RID: 2174 RVA: 0x000597F4 File Offset: 0x00058BF4
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

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x0600087F RID: 2175 RVA: 0x00059808 File Offset: 0x00058C08
		// (set) Token: 0x06000880 RID: 2176 RVA: 0x0005981C File Offset: 0x00058C1C
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

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x06000881 RID: 2177 RVA: 0x0005984C File Offset: 0x00058C4C
		// (set) Token: 0x06000882 RID: 2178 RVA: 0x00059860 File Offset: 0x00058C60
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
							DataTable dataTable = this.table;
							int elementColumnCount = dataTable.ElementColumnCount;
							dataTable.ElementColumnCount = elementColumnCount + 1;
						}
						else if (this.columnMapping == MappingType.Element)
						{
							DataTable dataTable2 = this.table;
							int elementColumnCount = dataTable2.ElementColumnCount;
							dataTable2.ElementColumnCount = elementColumnCount - 1;
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

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06000883 RID: 2179 RVA: 0x00059998 File Offset: 0x00058D98
		// (remove) Token: 0x06000884 RID: 2180 RVA: 0x000599BC File Offset: 0x00058DBC
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

		// Token: 0x06000885 RID: 2181 RVA: 0x000599E0 File Offset: 0x00058DE0
		internal void CheckColumnConstraint(DataRow row, DataRowAction action)
		{
			if (this.table.UpdatingCurrent(row, action))
			{
				this.CheckNullable(row);
				this.CheckMaxLength(row);
			}
		}

		// Token: 0x06000886 RID: 2182 RVA: 0x00059A0C File Offset: 0x00058E0C
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

		// Token: 0x06000887 RID: 2183 RVA: 0x00059AC0 File Offset: 0x00058EC0
		internal void CheckMaxLength(DataRow dr)
		{
			if (0 <= this.maxLength && this.maxLength < this.GetStringLength(dr.GetDefaultRecord()))
			{
				throw ExceptionBuilder.LongerThanMaxLength(this);
			}
		}

		// Token: 0x06000888 RID: 2184 RVA: 0x00059AF4 File Offset: 0x00058EF4
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

		// Token: 0x06000889 RID: 2185 RVA: 0x00059BD4 File Offset: 0x00058FD4
		internal void CheckNullable(DataRow row)
		{
			if (!this.AllowDBNull && this._storage.IsNull(row.GetDefaultRecord()))
			{
				throw ExceptionBuilder.NullValues(this.ColumnName);
			}
		}

		// Token: 0x0600088A RID: 2186 RVA: 0x00059C08 File Offset: 0x00059008
		protected void CheckUnique()
		{
			if (!this.SortIndex.CheckUnique())
			{
				throw ExceptionBuilder.NonUniqueValues(this.ColumnName);
			}
		}

		// Token: 0x0600088B RID: 2187 RVA: 0x00059C30 File Offset: 0x00059030
		internal int Compare(int record1, int record2)
		{
			return this._storage.Compare(record1, record2);
		}

		// Token: 0x0600088C RID: 2188 RVA: 0x00059C4C File Offset: 0x0005904C
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

		// Token: 0x0600088D RID: 2189 RVA: 0x00059CD0 File Offset: 0x000590D0
		internal int CompareValueTo(int record1, object value)
		{
			return this._storage.CompareValueTo(record1, value);
		}

		// Token: 0x0600088E RID: 2190 RVA: 0x00059CEC File Offset: 0x000590EC
		internal object ConvertValue(object value)
		{
			return this._storage.ConvertValue(value);
		}

		// Token: 0x0600088F RID: 2191 RVA: 0x00059D08 File Offset: 0x00059108
		internal void Copy(int srcRecordNo, int dstRecordNo)
		{
			this._storage.Copy(srcRecordNo, dstRecordNo);
		}

		// Token: 0x06000890 RID: 2192 RVA: 0x00059D24 File Offset: 0x00059124
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal DataColumn Clone()
		{
			DataColumn dataColumn = (DataColumn)Activator.CreateInstance(base.GetType());
			dataColumn.SimpleType = this.SimpleType;
			dataColumn.allowNull = this.allowNull;
			if (this.autoInc != null)
			{
				dataColumn.autoInc = this.autoInc.Clone();
			}
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

		// Token: 0x06000891 RID: 2193 RVA: 0x00059E98 File Offset: 0x00059298
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

		// Token: 0x06000892 RID: 2194 RVA: 0x00059F04 File Offset: 0x00059304
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

		// Token: 0x06000893 RID: 2195 RVA: 0x00059F38 File Offset: 0x00059338
		private int GetStringLength(int record)
		{
			return this._storage.GetStringLength(record);
		}

		// Token: 0x06000894 RID: 2196 RVA: 0x00059F54 File Offset: 0x00059354
		internal void Init(int record)
		{
			if (this.AutoIncrement)
			{
				object value = this.autoInc.Current;
				this.autoInc.MoveAfter();
				this._storage.Set(record, value);
				return;
			}
			this[record] = this.defaultValue;
		}

		// Token: 0x06000895 RID: 2197 RVA: 0x00059F9C File Offset: 0x0005939C
		internal static bool IsAutoIncrementType(Type dataType)
		{
			return dataType == typeof(int) || dataType == typeof(long) || dataType == typeof(short) || dataType == typeof(decimal) || dataType == typeof(BigInteger) || dataType == typeof(SqlInt32) || dataType == typeof(SqlInt64) || dataType == typeof(SqlInt16) || dataType == typeof(SqlDecimal);
		}

		// Token: 0x06000896 RID: 2198 RVA: 0x0005A054 File Offset: 0x00059454
		private bool IsColumnMappingValid(StorageType typeCode, MappingType mapping)
		{
			return mapping == MappingType.Element || !DataStorage.IsTypeCustomType(typeCode);
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x06000897 RID: 2199 RVA: 0x0005A070 File Offset: 0x00059470
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

		// Token: 0x06000898 RID: 2200 RVA: 0x0005A09C File Offset: 0x0005949C
		internal bool IsValueCustomTypeInstance(object value)
		{
			return DataStorage.IsTypeCustomType(value.GetType()) && !(value is Type);
		}

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x06000899 RID: 2201 RVA: 0x0005A0C4 File Offset: 0x000594C4
		internal bool ImplementsIXMLSerializable
		{
			get
			{
				return this.implementsIXMLSerializable;
			}
		}

		// Token: 0x0600089A RID: 2202 RVA: 0x0005A0D8 File Offset: 0x000594D8
		internal bool IsNull(int record)
		{
			return this._storage.IsNull(record);
		}

		// Token: 0x0600089B RID: 2203 RVA: 0x0005A0F4 File Offset: 0x000594F4
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

		// Token: 0x0600089C RID: 2204 RVA: 0x0005A170 File Offset: 0x00059570
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

		// Token: 0x0600089D RID: 2205 RVA: 0x0005A29C File Offset: 0x0005969C
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
			return rows.Length != 0;
		}

		// Token: 0x0600089E RID: 2206 RVA: 0x0005A2F4 File Offset: 0x000596F4
		internal void FinishInitInProgress()
		{
			if (this.Computed)
			{
				this.BindExpression();
			}
		}

		// Token: 0x0600089F RID: 2207 RVA: 0x0005A310 File Offset: 0x00059710
		protected virtual void OnPropertyChanging(PropertyChangedEventArgs pcevent)
		{
			if (this.onPropertyChangingDelegate != null)
			{
				this.onPropertyChangingDelegate(this, pcevent);
			}
		}

		// Token: 0x060008A0 RID: 2208 RVA: 0x0005A334 File Offset: 0x00059734
		protected internal void RaisePropertyChanging(string name)
		{
			this.OnPropertyChanging(new PropertyChangedEventArgs(name));
		}

		// Token: 0x060008A1 RID: 2209 RVA: 0x0005A350 File Offset: 0x00059750
		private void InsureStorage()
		{
			if (this._storage == null)
			{
				this._storage = DataStorage.CreateStorage(this, this.dataType, this._storageType);
			}
		}

		// Token: 0x060008A2 RID: 2210 RVA: 0x0005A380 File Offset: 0x00059780
		internal void SetCapacity(int capacity)
		{
			this.InsureStorage();
			this._storage.SetCapacity(capacity);
		}

		// Token: 0x060008A3 RID: 2211 RVA: 0x0005A3A0 File Offset: 0x000597A0
		private bool ShouldSerializeDefaultValue()
		{
			return !this.DefaultValueIsNull;
		}

		// Token: 0x060008A4 RID: 2212 RVA: 0x0005A3B8 File Offset: 0x000597B8
		internal void OnSetDataSet()
		{
		}

		// Token: 0x060008A5 RID: 2213 RVA: 0x0005A3C8 File Offset: 0x000597C8
		public override string ToString()
		{
			if (this.expression == null)
			{
				return this.ColumnName;
			}
			return this.ColumnName + " + " + this.Expression;
		}

		// Token: 0x060008A6 RID: 2214 RVA: 0x0005A3FC File Offset: 0x000597FC
		internal object ConvertXmlToObject(string s)
		{
			this.InsureStorage();
			return this._storage.ConvertXmlToObject(s);
		}

		// Token: 0x060008A7 RID: 2215 RVA: 0x0005A41C File Offset: 0x0005981C
		internal object ConvertXmlToObject(XmlReader xmlReader, XmlRootAttribute xmlAttrib)
		{
			this.InsureStorage();
			return this._storage.ConvertXmlToObject(xmlReader, xmlAttrib);
		}

		// Token: 0x060008A8 RID: 2216 RVA: 0x0005A43C File Offset: 0x0005983C
		internal string ConvertObjectToXml(object value)
		{
			this.InsureStorage();
			return this._storage.ConvertObjectToXml(value);
		}

		// Token: 0x060008A9 RID: 2217 RVA: 0x0005A45C File Offset: 0x0005985C
		internal void ConvertObjectToXml(object value, XmlWriter xmlWriter, XmlRootAttribute xmlAttrib)
		{
			this.InsureStorage();
			this._storage.ConvertObjectToXml(value, xmlWriter, xmlAttrib);
		}

		// Token: 0x060008AA RID: 2218 RVA: 0x0005A480 File Offset: 0x00059880
		internal object GetEmptyColumnStore(int recordCount)
		{
			this.InsureStorage();
			return this._storage.GetEmptyStorageInternal(recordCount);
		}

		// Token: 0x060008AB RID: 2219 RVA: 0x0005A4A0 File Offset: 0x000598A0
		internal void CopyValueIntoStore(int record, object store, BitArray nullbits, int storeIndex)
		{
			this._storage.CopyValueInternal(record, store, nullbits, storeIndex);
		}

		// Token: 0x060008AC RID: 2220 RVA: 0x0005A4C0 File Offset: 0x000598C0
		internal void SetStorage(object store, BitArray nullbits)
		{
			this.InsureStorage();
			this._storage.SetStorageInternal(store, nullbits);
		}

		// Token: 0x060008AD RID: 2221 RVA: 0x0005A4E0 File Offset: 0x000598E0
		internal void AddDependentColumn(DataColumn expressionColumn)
		{
			if (this.dependentColumns == null)
			{
				this.dependentColumns = new List<DataColumn>();
			}
			this.dependentColumns.Add(expressionColumn);
			this.table.AddDependentColumn(expressionColumn);
		}

		// Token: 0x060008AE RID: 2222 RVA: 0x0005A518 File Offset: 0x00059918
		internal void RemoveDependentColumn(DataColumn expressionColumn)
		{
			if (this.dependentColumns != null && this.dependentColumns.Contains(expressionColumn))
			{
				this.dependentColumns.Remove(expressionColumn);
			}
			this.table.RemoveDependentColumn(expressionColumn);
		}

		// Token: 0x060008AF RID: 2223 RVA: 0x0005A554 File Offset: 0x00059954
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

		// Token: 0x040002ED RID: 749
		private bool allowNull = true;

		// Token: 0x040002EE RID: 750
		private string caption;

		// Token: 0x040002EF RID: 751
		private string _columnName;

		// Token: 0x040002F0 RID: 752
		private Type dataType;

		// Token: 0x040002F1 RID: 753
		private StorageType _storageType;

		// Token: 0x040002F2 RID: 754
		internal object defaultValue = DBNull.Value;

		// Token: 0x040002F3 RID: 755
		private DataSetDateTime _dateTimeMode = DataSetDateTime.UnspecifiedLocal;

		// Token: 0x040002F4 RID: 756
		private DataExpression expression;

		// Token: 0x040002F5 RID: 757
		private int maxLength = -1;

		// Token: 0x040002F6 RID: 758
		private int _ordinal = -1;

		// Token: 0x040002F7 RID: 759
		private bool readOnly;

		// Token: 0x040002F8 RID: 760
		internal Index sortIndex;

		// Token: 0x040002F9 RID: 761
		internal DataTable table;

		// Token: 0x040002FA RID: 762
		private bool unique;

		// Token: 0x040002FB RID: 763
		internal MappingType columnMapping = MappingType.Element;

		// Token: 0x040002FC RID: 764
		internal int _hashCode;

		// Token: 0x040002FD RID: 765
		internal int errors;

		// Token: 0x040002FE RID: 766
		private bool isSqlType;

		// Token: 0x040002FF RID: 767
		private bool implementsINullable;

		// Token: 0x04000300 RID: 768
		private bool implementsIChangeTracking;

		// Token: 0x04000301 RID: 769
		private bool implementsIRevertibleChangeTracking;

		// Token: 0x04000302 RID: 770
		private bool implementsIXMLSerializable;

		// Token: 0x04000303 RID: 771
		private bool defaultValueIsNull = true;

		// Token: 0x04000304 RID: 772
		internal List<DataColumn> dependentColumns;

		// Token: 0x04000305 RID: 773
		internal PropertyCollection extendedProperties;

		// Token: 0x04000306 RID: 774
		private PropertyChangedEventHandler onPropertyChangingDelegate;

		// Token: 0x04000307 RID: 775
		private DataStorage _storage;

		// Token: 0x04000308 RID: 776
		private AutoIncrementValue autoInc;

		// Token: 0x04000309 RID: 777
		internal string _columnUri;

		// Token: 0x0400030A RID: 778
		private string _columnPrefix = "";

		// Token: 0x0400030B RID: 779
		internal string encodedColumnName;

		// Token: 0x0400030C RID: 780
		internal string dttype = "";

		// Token: 0x0400030D RID: 781
		internal SimpleType simpleType;

		// Token: 0x0400030E RID: 782
		private static int _objectTypeCount;

		// Token: 0x0400030F RID: 783
		private readonly int _objectID = Interlocked.Increment(ref DataColumn._objectTypeCount);
	}
}
