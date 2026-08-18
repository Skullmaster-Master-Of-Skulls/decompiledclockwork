using System;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Data.Common;
using System.Globalization;
using System.Reflection;

namespace System.Data.OracleClient
{
	// Token: 0x02000070 RID: 112
	[TypeConverter(typeof(OracleParameter.OracleParameterConverter))]
	public sealed class OracleParameter : DbParameter, ICloneable, IDbDataParameter, IDataParameter
	{
		// Token: 0x060005D9 RID: 1497 RVA: 0x0006E594 File Offset: 0x0006D994
		public OracleParameter()
		{
		}

		// Token: 0x060005DA RID: 1498 RVA: 0x0006E5B4 File Offset: 0x0006D9B4
		public OracleParameter(string name, object value)
		{
			this.ParameterName = name;
			this.Value = value;
		}

		// Token: 0x060005DB RID: 1499 RVA: 0x0006E5E4 File Offset: 0x0006D9E4
		public OracleParameter(string name, OracleType oracleType) : this()
		{
			this.ParameterName = name;
			this.OracleType = oracleType;
		}

		// Token: 0x060005DC RID: 1500 RVA: 0x0006E614 File Offset: 0x0006DA14
		public OracleParameter(string name, OracleType oracleType, int size) : this()
		{
			this.ParameterName = name;
			this.OracleType = oracleType;
			this.Size = size;
		}

		// Token: 0x060005DD RID: 1501 RVA: 0x0006E644 File Offset: 0x0006DA44
		public OracleParameter(string name, OracleType oracleType, int size, string srcColumn) : this()
		{
			this.ParameterName = name;
			this.OracleType = oracleType;
			this.Size = size;
			this.SourceColumn = srcColumn;
		}

		// Token: 0x060005DE RID: 1502 RVA: 0x0006E674 File Offset: 0x0006DA74
		public OracleParameter(string name, OracleType oracleType, int size, ParameterDirection direction, bool isNullable, byte precision, byte scale, string srcColumn, DataRowVersion srcVersion, object value) : this()
		{
			this.ParameterName = name;
			this.OracleType = oracleType;
			this.Size = size;
			this.Direction = direction;
			this.IsNullable = isNullable;
			this.PrecisionInternal = precision;
			this.ScaleInternal = scale;
			this.SourceColumn = srcColumn;
			this.SourceVersion = srcVersion;
			this.Value = value;
		}

		// Token: 0x060005DF RID: 1503 RVA: 0x0006E6D4 File Offset: 0x0006DAD4
		public OracleParameter(string name, OracleType oracleType, int size, ParameterDirection direction, string sourceColumn, DataRowVersion sourceVersion, bool sourceColumnNullMapping, object value) : this()
		{
			this.ParameterName = name;
			this.OracleType = oracleType;
			this.Size = size;
			this.Direction = direction;
			this.SourceColumn = sourceColumn;
			this.SourceVersion = sourceVersion;
			this.SourceColumnNullMapping = sourceColumnNullMapping;
			this.Value = value;
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x060005E0 RID: 1504 RVA: 0x0006E724 File Offset: 0x0006DB24
		internal int BindSize
		{
			get
			{
				int num = this.GetActualSize();
				if (32767 < num && ParameterDirection.Input == this.Direction)
				{
					num = this.ValueSize(this.GetCoercedValueInternal());
				}
				return num;
			}
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x060005E1 RID: 1505 RVA: 0x0006E764 File Offset: 0x0006DB64
		// (set) Token: 0x060005E2 RID: 1506 RVA: 0x0006E784 File Offset: 0x0006DB84
		internal int CommandSetResult
		{
			get
			{
				return this._commandSetResult;
			}
			set
			{
				this._commandSetResult = value;
			}
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x060005E3 RID: 1507 RVA: 0x0006E7A4 File Offset: 0x0006DBA4
		// (set) Token: 0x060005E4 RID: 1508 RVA: 0x0006E7C4 File Offset: 0x0006DBC4
		public override DbType DbType
		{
			get
			{
				return this.GetMetaType().DbType;
			}
			set
			{
				if (this._metaType == null || this._metaType.DbType != value)
				{
					this.PropertyTypeChanging();
					this._metaType = MetaType.GetMetaTypeForType(value);
				}
			}
		}

		// Token: 0x060005E5 RID: 1509 RVA: 0x0006E804 File Offset: 0x0006DC04
		public override void ResetDbType()
		{
			this.ResetOracleType();
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x060005E6 RID: 1510 RVA: 0x0006E824 File Offset: 0x0006DC24
		// (set) Token: 0x060005E7 RID: 1511 RVA: 0x0006E844 File Offset: 0x0006DC44
		[RefreshProperties(RefreshProperties.All)]
		[ResCategory("OracleCategory_Data")]
		[ResDescription("OracleParameter_OracleType")]
		[DbProviderSpecificTypeProperty(true)]
		[DefaultValue(OracleType.VarChar)]
		public OracleType OracleType
		{
			get
			{
				return this.GetMetaType().OracleType;
			}
			set
			{
				MetaType metaType = this._metaType;
				if (metaType == null || metaType.OracleType != value)
				{
					this.PropertyTypeChanging();
					this._metaType = MetaType.GetMetaTypeForType(value);
				}
			}
		}

		// Token: 0x060005E8 RID: 1512 RVA: 0x0006E884 File Offset: 0x0006DC84
		public void ResetOracleType()
		{
			if (this._metaType != null)
			{
				this.PropertyTypeChanging();
				this._metaType = null;
			}
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x060005E9 RID: 1513 RVA: 0x0006E8B4 File Offset: 0x0006DCB4
		// (set) Token: 0x060005EA RID: 1514 RVA: 0x0006E8D4 File Offset: 0x0006DCD4
		[ResCategory("DataCategory_Data")]
		[ResDescription("DbParameter_ParameterName")]
		public override string ParameterName
		{
			get
			{
				string parameterName = this._parameterName;
				if (parameterName == null)
				{
					return ADP.StrEmpty;
				}
				return parameterName;
			}
			set
			{
				if (this._parameterName != value)
				{
					this._parameterName = value;
				}
			}
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x060005EB RID: 1515 RVA: 0x0006E904 File Offset: 0x0006DD04
		// (set) Token: 0x060005EC RID: 1516 RVA: 0x0006E924 File Offset: 0x0006DD24
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		[Obsolete("Precision has been deprecated.  Use the Math classes to explicitly set the precision of a decimal.  http://go.microsoft.com/fwlink/?linkid=14202")]
		public byte Precision
		{
			get
			{
				return this.PrecisionInternal;
			}
			set
			{
				this.PrecisionInternal = value;
			}
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x060005ED RID: 1517 RVA: 0x0006E944 File Offset: 0x0006DD44
		// (set) Token: 0x060005EE RID: 1518 RVA: 0x0006E964 File Offset: 0x0006DD64
		private byte PrecisionInternal
		{
			get
			{
				return this._precision;
			}
			set
			{
				if (this._precision != value)
				{
					this._precision = value;
				}
			}
		}

		// Token: 0x060005EF RID: 1519 RVA: 0x0006E984 File Offset: 0x0006DD84
		private bool ShouldSerializePrecision()
		{
			return 0 != this._precision;
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x060005F0 RID: 1520 RVA: 0x0006E9A4 File Offset: 0x0006DDA4
		// (set) Token: 0x060005F1 RID: 1521 RVA: 0x0006E9C4 File Offset: 0x0006DDC4
		[Obsolete("Scale has been deprecated.  Use the Math classes to explicitly set the scale of a decimal.  http://go.microsoft.com/fwlink/?linkid=14202")]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public byte Scale
		{
			get
			{
				return this.ScaleInternal;
			}
			set
			{
				this.ScaleInternal = value;
			}
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x060005F2 RID: 1522 RVA: 0x0006E9E4 File Offset: 0x0006DDE4
		// (set) Token: 0x060005F3 RID: 1523 RVA: 0x0006EA04 File Offset: 0x0006DE04
		private byte ScaleInternal
		{
			get
			{
				return this._scale;
			}
			set
			{
				if (this._scale != value || !this._hasScale)
				{
					this._scale = value;
					this._hasScale = true;
				}
			}
		}

		// Token: 0x060005F4 RID: 1524 RVA: 0x0006EA34 File Offset: 0x0006DE34
		private bool ShouldSerializeScale()
		{
			return this._hasScale;
		}

		// Token: 0x060005F5 RID: 1525 RVA: 0x0006EA54 File Offset: 0x0006DE54
		private static object CoerceValue(object value, MetaType destinationType)
		{
			if (value != null && !Convert.IsDBNull(value) && typeof(object) != destinationType.BaseType)
			{
				Type type = value.GetType();
				if (type != destinationType.BaseType && type != destinationType.NoConvertType)
				{
					try
					{
						if (typeof(string) == destinationType.BaseType && typeof(char[]) == type)
						{
							value = new string((char[])value);
						}
						else if (DbType.Currency == destinationType.DbType && typeof(string) == type)
						{
							value = decimal.Parse((string)value, NumberStyles.Currency, null);
						}
						else
						{
							value = Convert.ChangeType(value, destinationType.BaseType, null);
						}
					}
					catch (Exception ex)
					{
						if (!ADP.IsCatchableExceptionType(ex))
						{
							throw;
						}
						throw ADP.ParameterConversionFailed(value, destinationType.BaseType, ex);
					}
				}
			}
			return value;
		}

		// Token: 0x060005F6 RID: 1526 RVA: 0x0006EB54 File Offset: 0x0006DF54
		object ICloneable.Clone()
		{
			return new OracleParameter(this);
		}

		// Token: 0x060005F7 RID: 1527 RVA: 0x0006EB74 File Offset: 0x0006DF74
		private void CloneHelper(OracleParameter destination)
		{
			this.CloneHelperCore(destination);
			destination._metaType = this._metaType;
			destination._parameterName = this._parameterName;
			destination._precision = this._precision;
			destination._scale = this._scale;
			destination._hasScale = this._hasScale;
		}

		// Token: 0x060005F8 RID: 1528 RVA: 0x0006EBC4 File Offset: 0x0006DFC4
		internal int GetActualSize()
		{
			if (!this.ShouldSerializeSize())
			{
				return this.ValueSize(this.CoercedValue);
			}
			return this.Size;
		}

		// Token: 0x060005F9 RID: 1529 RVA: 0x0006EBF4 File Offset: 0x0006DFF4
		private MetaType GetMetaType()
		{
			return this.GetMetaType(this.Value);
		}

		// Token: 0x060005FA RID: 1530 RVA: 0x0006EC14 File Offset: 0x0006E014
		internal MetaType GetMetaType(object value)
		{
			MetaType metaType = this._metaType;
			if (metaType == null)
			{
				if (value != null && !Convert.IsDBNull(value))
				{
					metaType = MetaType.GetMetaTypeForObject(value);
				}
				else
				{
					metaType = MetaType.GetDefaultMetaType();
				}
				this._metaType = metaType;
			}
			return metaType;
		}

		// Token: 0x060005FB RID: 1531 RVA: 0x0006EC54 File Offset: 0x0006E054
		internal object GetCoercedValueInternal()
		{
			object obj = this.CoercedValue;
			if (obj == null)
			{
				obj = OracleParameter.CoerceValue(this.Value, this._coercedMetaType);
				this.CoercedValue = obj;
			}
			return obj;
		}

		// Token: 0x060005FC RID: 1532 RVA: 0x0006EC94 File Offset: 0x0006E094
		private void PropertyChanging()
		{
		}

		// Token: 0x060005FD RID: 1533 RVA: 0x0006ECA4 File Offset: 0x0006E0A4
		private void PropertyTypeChanging()
		{
			this.PropertyChanging();
			this.CoercedValue = null;
		}

		// Token: 0x060005FE RID: 1534 RVA: 0x0006ECC4 File Offset: 0x0006E0C4
		internal void SetCoercedValueInternal(object value, MetaType metaType)
		{
			this._coercedMetaType = metaType;
			this.CoercedValue = OracleParameter.CoerceValue(value, metaType);
		}

		// Token: 0x060005FF RID: 1535 RVA: 0x0006ECF4 File Offset: 0x0006E0F4
		private bool ShouldSerializeOracleType()
		{
			return null != this._metaType;
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x06000600 RID: 1536 RVA: 0x0006ED14 File Offset: 0x0006E114
		// (set) Token: 0x06000601 RID: 1537 RVA: 0x0006ED34 File Offset: 0x0006E134
		[ResDescription("DbParameter_Value")]
		[ResCategory("DataCategory_Data")]
		[RefreshProperties(RefreshProperties.All)]
		[TypeConverter(typeof(StringConverter))]
		public override object Value
		{
			get
			{
				return this._value;
			}
			set
			{
				this._coercedValue = null;
				this._value = value;
			}
		}

		// Token: 0x06000602 RID: 1538 RVA: 0x0006ED54 File Offset: 0x0006E154
		private int ValueSize(object value)
		{
			if (value is OracleString)
			{
				return ((OracleString)value).Length;
			}
			if (value is string)
			{
				return ((string)value).Length;
			}
			if (value is char[])
			{
				return ((char[])value).Length;
			}
			if (value is OracleBinary)
			{
				return ((OracleBinary)value).Length;
			}
			return this.ValueSizeCore(value);
		}

		// Token: 0x06000603 RID: 1539 RVA: 0x0006EDC4 File Offset: 0x0006E1C4
		private OracleParameter(OracleParameter source) : this()
		{
			ADP.CheckArgumentNull(source, "source");
			source.CloneHelper(this);
			ICloneable cloneable = this._value as ICloneable;
			if (cloneable != null)
			{
				this._value = cloneable.Clone();
			}
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x06000604 RID: 1540 RVA: 0x0006EE04 File Offset: 0x0006E204
		// (set) Token: 0x06000605 RID: 1541 RVA: 0x0006EE24 File Offset: 0x0006E224
		private object CoercedValue
		{
			get
			{
				return this._coercedValue;
			}
			set
			{
				this._coercedValue = value;
			}
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x06000606 RID: 1542 RVA: 0x0006EE44 File Offset: 0x0006E244
		// (set) Token: 0x06000607 RID: 1543 RVA: 0x0006EE64 File Offset: 0x0006E264
		[ResDescription("DbParameter_Direction")]
		[RefreshProperties(RefreshProperties.All)]
		[ResCategory("DataCategory_Data")]
		public override ParameterDirection Direction
		{
			get
			{
				ParameterDirection direction = this._direction;
				if (direction == (ParameterDirection)0)
				{
					return ParameterDirection.Input;
				}
				return direction;
			}
			set
			{
				if (this._direction != value)
				{
					switch (value)
					{
					case ParameterDirection.Input:
					case ParameterDirection.Output:
					case ParameterDirection.InputOutput:
					case ParameterDirection.ReturnValue:
						this.PropertyChanging();
						this._direction = value;
						return;
					}
					throw ADP.InvalidParameterDirection(value);
				}
			}
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x06000608 RID: 1544 RVA: 0x0006EEB4 File Offset: 0x0006E2B4
		// (set) Token: 0x06000609 RID: 1545 RVA: 0x0006EED4 File Offset: 0x0006E2D4
		public override bool IsNullable
		{
			get
			{
				return this._isNullable;
			}
			set
			{
				this._isNullable = value;
			}
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x0600060A RID: 1546 RVA: 0x0006EEF4 File Offset: 0x0006E2F4
		// (set) Token: 0x0600060B RID: 1547 RVA: 0x0006EF14 File Offset: 0x0006E314
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[ResCategory("DataCategory_Data")]
		[ResDescription("DbParameter_Offset")]
		public int Offset
		{
			get
			{
				return this._offset;
			}
			set
			{
				if (value < 0)
				{
					throw ADP.InvalidOffsetValue(value);
				}
				this._offset = value;
			}
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x0600060C RID: 1548 RVA: 0x0006EF34 File Offset: 0x0006E334
		// (set) Token: 0x0600060D RID: 1549 RVA: 0x0006EF64 File Offset: 0x0006E364
		[ResCategory("DataCategory_Data")]
		[ResDescription("DbParameter_Size")]
		public override int Size
		{
			get
			{
				int num = this._size;
				if (num == 0)
				{
					num = this.ValueSize(this.Value);
				}
				return num;
			}
			set
			{
				if (this._size != value)
				{
					if (value < -1)
					{
						throw ADP.InvalidSizeValue(value);
					}
					this.PropertyChanging();
					this._size = value;
				}
			}
		}

		// Token: 0x0600060E RID: 1550 RVA: 0x0006EF94 File Offset: 0x0006E394
		private void ResetSize()
		{
			if (this._size != 0)
			{
				this.PropertyChanging();
				this._size = 0;
			}
		}

		// Token: 0x0600060F RID: 1551 RVA: 0x0006EFC4 File Offset: 0x0006E3C4
		private bool ShouldSerializeSize()
		{
			return 0 != this._size;
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x06000610 RID: 1552 RVA: 0x0006EFE4 File Offset: 0x0006E3E4
		// (set) Token: 0x06000611 RID: 1553 RVA: 0x0006F004 File Offset: 0x0006E404
		[ResDescription("DbParameter_SourceColumn")]
		[ResCategory("DataCategory_Update")]
		public override string SourceColumn
		{
			get
			{
				string sourceColumn = this._sourceColumn;
				if (sourceColumn == null)
				{
					return ADP.StrEmpty;
				}
				return sourceColumn;
			}
			set
			{
				this._sourceColumn = value;
			}
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x06000612 RID: 1554 RVA: 0x0006F024 File Offset: 0x0006E424
		// (set) Token: 0x06000613 RID: 1555 RVA: 0x0006F044 File Offset: 0x0006E444
		public override bool SourceColumnNullMapping
		{
			get
			{
				return this._sourceColumnNullMapping;
			}
			set
			{
				this._sourceColumnNullMapping = value;
			}
		}

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x06000614 RID: 1556 RVA: 0x0006F064 File Offset: 0x0006E464
		// (set) Token: 0x06000615 RID: 1557 RVA: 0x0006F084 File Offset: 0x0006E484
		[ResCategory("DataCategory_Update")]
		[ResDescription("DbParameter_SourceVersion")]
		public override DataRowVersion SourceVersion
		{
			get
			{
				DataRowVersion sourceVersion = this._sourceVersion;
				if (sourceVersion == (DataRowVersion)0)
				{
					return DataRowVersion.Current;
				}
				return sourceVersion;
			}
			set
			{
				if (value <= DataRowVersion.Current)
				{
					if (value != DataRowVersion.Original && value != DataRowVersion.Current)
					{
						goto IL_34;
					}
				}
				else if (value != DataRowVersion.Proposed && value != DataRowVersion.Default)
				{
					goto IL_34;
				}
				this._sourceVersion = value;
				return;
				IL_34:
				throw ADP.InvalidDataRowVersion(value);
			}
		}

		// Token: 0x06000616 RID: 1558 RVA: 0x0006F0D4 File Offset: 0x0006E4D4
		private void CloneHelperCore(OracleParameter destination)
		{
			destination._value = this._value;
			destination._direction = this._direction;
			destination._size = this._size;
			destination._offset = this._offset;
			destination._sourceColumn = this._sourceColumn;
			destination._sourceVersion = this._sourceVersion;
			destination._sourceColumnNullMapping = this._sourceColumnNullMapping;
			destination._isNullable = this._isNullable;
		}

		// Token: 0x06000617 RID: 1559 RVA: 0x0006F144 File Offset: 0x0006E544
		internal void CopyTo(DbParameter destination)
		{
			ADP.CheckArgumentNull(destination, "destination");
			this.CloneHelper((OracleParameter)destination);
		}

		// Token: 0x06000618 RID: 1560 RVA: 0x0006F174 File Offset: 0x0006E574
		internal object CompareExchangeParent(object value, object comparand)
		{
			object parent = this._parent;
			if (comparand == parent)
			{
				this._parent = value;
			}
			return parent;
		}

		// Token: 0x06000619 RID: 1561 RVA: 0x0006F194 File Offset: 0x0006E594
		internal void ResetParent()
		{
			this._parent = null;
		}

		// Token: 0x0600061A RID: 1562 RVA: 0x0006F1B4 File Offset: 0x0006E5B4
		public override string ToString()
		{
			return this.ParameterName;
		}

		// Token: 0x0600061B RID: 1563 RVA: 0x0006F1D4 File Offset: 0x0006E5D4
		private byte ValuePrecisionCore(object value)
		{
			if (value is decimal)
			{
				return ((decimal)value).Precision;
			}
			return 0;
		}

		// Token: 0x0600061C RID: 1564 RVA: 0x0006F204 File Offset: 0x0006E604
		private byte ValueScaleCore(object value)
		{
			if (value is decimal)
			{
				return (byte)((decimal.GetBits((decimal)value)[3] & 16711680) >> 16);
			}
			return 0;
		}

		// Token: 0x0600061D RID: 1565 RVA: 0x0006F234 File Offset: 0x0006E634
		private int ValueSizeCore(object value)
		{
			if (!ADP.IsNull(value))
			{
				string text = value as string;
				if (text != null)
				{
					return text.Length;
				}
				byte[] array = value as byte[];
				if (array != null)
				{
					return array.Length;
				}
				char[] array2 = value as char[];
				if (array2 != null)
				{
					return array2.Length;
				}
				if (value is byte || value is char)
				{
					return 1;
				}
			}
			return 0;
		}

		// Token: 0x04000484 RID: 1156
		private MetaType _metaType;

		// Token: 0x04000485 RID: 1157
		private int _commandSetResult;

		// Token: 0x04000486 RID: 1158
		private MetaType _coercedMetaType;

		// Token: 0x04000487 RID: 1159
		private string _parameterName;

		// Token: 0x04000488 RID: 1160
		private byte _precision;

		// Token: 0x04000489 RID: 1161
		private byte _scale;

		// Token: 0x0400048A RID: 1162
		private bool _hasScale;

		// Token: 0x0400048B RID: 1163
		private object _value;

		// Token: 0x0400048C RID: 1164
		private object _parent;

		// Token: 0x0400048D RID: 1165
		private ParameterDirection _direction;

		// Token: 0x0400048E RID: 1166
		private int _size;

		// Token: 0x0400048F RID: 1167
		private int _offset;

		// Token: 0x04000490 RID: 1168
		private string _sourceColumn;

		// Token: 0x04000491 RID: 1169
		private DataRowVersion _sourceVersion;

		// Token: 0x04000492 RID: 1170
		private bool _sourceColumnNullMapping;

		// Token: 0x04000493 RID: 1171
		private bool _isNullable;

		// Token: 0x04000494 RID: 1172
		private object _coercedValue;

		// Token: 0x02000071 RID: 113
		internal sealed class OracleParameterConverter : ExpandableObjectConverter
		{
			// Token: 0x0600061F RID: 1567 RVA: 0x0006F2B4 File Offset: 0x0006E6B4
			public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
			{
				return destinationType == typeof(InstanceDescriptor) || base.CanConvertTo(context, destinationType);
			}

			// Token: 0x06000620 RID: 1568 RVA: 0x0006F2E4 File Offset: 0x0006E6E4
			public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
			{
				if (destinationType == null)
				{
					throw ADP.ArgumentNull("destinationType");
				}
				if (destinationType == typeof(InstanceDescriptor) && value is OracleParameter)
				{
					return this.ConvertToInstanceDescriptor(value as OracleParameter);
				}
				return base.ConvertTo(context, culture, value, destinationType);
			}

			// Token: 0x06000621 RID: 1569 RVA: 0x0006F334 File Offset: 0x0006E734
			private InstanceDescriptor ConvertToInstanceDescriptor(OracleParameter p)
			{
				int num = 0;
				if (p.ShouldSerializeOracleType())
				{
					num |= 1;
				}
				if (p.ShouldSerializeSize())
				{
					num |= 2;
				}
				if (!ADP.IsEmpty(p.SourceColumn))
				{
					num |= 4;
				}
				if (p.Value != null)
				{
					num |= 8;
				}
				if (ParameterDirection.Input != p.Direction || p.IsNullable || p.ShouldSerializePrecision() || p.ShouldSerializeScale() || DataRowVersion.Current != p.SourceVersion)
				{
					num |= 16;
				}
				if (p.SourceColumnNullMapping)
				{
					num |= 32;
				}
				Type[] types;
				object[] arguments;
				switch (num)
				{
				case 0:
				case 1:
					types = new Type[]
					{
						typeof(string),
						typeof(OracleType)
					};
					arguments = new object[]
					{
						p.ParameterName,
						p.OracleType
					};
					break;
				case 2:
				case 3:
					types = new Type[]
					{
						typeof(string),
						typeof(OracleType),
						typeof(int)
					};
					arguments = new object[]
					{
						p.ParameterName,
						p.OracleType,
						p.Size
					};
					break;
				case 4:
				case 5:
				case 6:
				case 7:
					types = new Type[]
					{
						typeof(string),
						typeof(OracleType),
						typeof(int),
						typeof(string)
					};
					arguments = new object[]
					{
						p.ParameterName,
						p.OracleType,
						p.Size,
						p.SourceColumn
					};
					break;
				case 8:
					types = new Type[]
					{
						typeof(string),
						typeof(object)
					};
					arguments = new object[]
					{
						p.ParameterName,
						p.Value
					};
					break;
				default:
					if ((32 & num) == 0)
					{
						types = new Type[]
						{
							typeof(string),
							typeof(OracleType),
							typeof(int),
							typeof(ParameterDirection),
							typeof(bool),
							typeof(byte),
							typeof(byte),
							typeof(string),
							typeof(DataRowVersion),
							typeof(object)
						};
						arguments = new object[]
						{
							p.ParameterName,
							p.OracleType,
							p.Size,
							p.Direction,
							p.IsNullable,
							p.PrecisionInternal,
							p.ScaleInternal,
							p.SourceColumn,
							p.SourceVersion,
							p.Value
						};
					}
					else
					{
						types = new Type[]
						{
							typeof(string),
							typeof(OracleType),
							typeof(int),
							typeof(ParameterDirection),
							typeof(string),
							typeof(DataRowVersion),
							typeof(bool),
							typeof(object)
						};
						arguments = new object[]
						{
							p.ParameterName,
							p.OracleType,
							p.Size,
							p.Direction,
							p.SourceColumn,
							p.SourceVersion,
							p.SourceColumnNullMapping,
							p.Value
						};
					}
					break;
				}
				ConstructorInfo constructor = typeof(OracleParameter).GetConstructor(types);
				return new InstanceDescriptor(constructor, arguments);
			}
		}
	}
}
