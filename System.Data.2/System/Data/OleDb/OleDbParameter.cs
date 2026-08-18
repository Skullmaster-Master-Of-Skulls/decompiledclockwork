using System;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Data.Common;
using System.Globalization;
using System.Reflection;

namespace System.Data.OleDb
{
	// Token: 0x02000259 RID: 601
	[TypeConverter(typeof(OleDbParameter.OleDbParameterConverter))]
	public sealed class OleDbParameter : DbParameter, ICloneable, IDbDataParameter, IDataParameter
	{
		// Token: 0x060025CB RID: 9675 RVA: 0x0010205C File Offset: 0x0010145C
		public OleDbParameter()
		{
		}

		// Token: 0x060025CC RID: 9676 RVA: 0x00102070 File Offset: 0x00101470
		public OleDbParameter(string name, object value) : this()
		{
			this.ParameterName = name;
			this.Value = value;
		}

		// Token: 0x060025CD RID: 9677 RVA: 0x00102094 File Offset: 0x00101494
		public OleDbParameter(string name, OleDbType dataType) : this()
		{
			this.ParameterName = name;
			this.OleDbType = dataType;
		}

		// Token: 0x060025CE RID: 9678 RVA: 0x001020B8 File Offset: 0x001014B8
		public OleDbParameter(string name, OleDbType dataType, int size) : this()
		{
			this.ParameterName = name;
			this.OleDbType = dataType;
			this.Size = size;
		}

		// Token: 0x060025CF RID: 9679 RVA: 0x001020E0 File Offset: 0x001014E0
		public OleDbParameter(string name, OleDbType dataType, int size, string srcColumn) : this()
		{
			this.ParameterName = name;
			this.OleDbType = dataType;
			this.Size = size;
			this.SourceColumn = srcColumn;
		}

		// Token: 0x060025D0 RID: 9680 RVA: 0x00102110 File Offset: 0x00101510
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public OleDbParameter(string parameterName, OleDbType dbType, int size, ParameterDirection direction, bool isNullable, byte precision, byte scale, string srcColumn, DataRowVersion srcVersion, object value) : this()
		{
			this.ParameterName = parameterName;
			this.OleDbType = dbType;
			this.Size = size;
			this.Direction = direction;
			this.IsNullable = isNullable;
			this.PrecisionInternal = precision;
			this.ScaleInternal = scale;
			this.SourceColumn = srcColumn;
			this.SourceVersion = srcVersion;
			this.Value = value;
		}

		// Token: 0x060025D1 RID: 9681 RVA: 0x00102170 File Offset: 0x00101570
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public OleDbParameter(string parameterName, OleDbType dbType, int size, ParameterDirection direction, byte precision, byte scale, string sourceColumn, DataRowVersion sourceVersion, bool sourceColumnNullMapping, object value) : this()
		{
			this.ParameterName = parameterName;
			this.OleDbType = dbType;
			this.Size = size;
			this.Direction = direction;
			this.PrecisionInternal = precision;
			this.ScaleInternal = scale;
			this.SourceColumn = sourceColumn;
			this.SourceVersion = sourceVersion;
			this.SourceColumnNullMapping = sourceColumnNullMapping;
			this.Value = value;
		}

		// Token: 0x17000620 RID: 1568
		// (get) Token: 0x060025D2 RID: 9682 RVA: 0x001021D0 File Offset: 0x001015D0
		internal int ChangeID
		{
			get
			{
				return this._changeID;
			}
		}

		// Token: 0x17000621 RID: 1569
		// (get) Token: 0x060025D3 RID: 9683 RVA: 0x001021E4 File Offset: 0x001015E4
		// (set) Token: 0x060025D4 RID: 9684 RVA: 0x00102204 File Offset: 0x00101604
		public override DbType DbType
		{
			get
			{
				return this.GetBindType(this.Value).enumDbType;
			}
			set
			{
				NativeDBType metaType = this._metaType;
				if (metaType == null || metaType.enumDbType != value)
				{
					this.PropertyTypeChanging();
					this._metaType = NativeDBType.FromDbType(value);
				}
			}
		}

		// Token: 0x060025D5 RID: 9685 RVA: 0x00102238 File Offset: 0x00101638
		public override void ResetDbType()
		{
			this.ResetOleDbType();
		}

		// Token: 0x17000622 RID: 1570
		// (get) Token: 0x060025D6 RID: 9686 RVA: 0x0010224C File Offset: 0x0010164C
		// (set) Token: 0x060025D7 RID: 9687 RVA: 0x0010226C File Offset: 0x0010166C
		[DbProviderSpecificTypeProperty(true)]
		[ResDescription("OleDbParameter_OleDbType")]
		[RefreshProperties(RefreshProperties.All)]
		[ResCategory("DataCategory_Data")]
		public OleDbType OleDbType
		{
			get
			{
				return this.GetBindType(this.Value).enumOleDbType;
			}
			set
			{
				NativeDBType metaType = this._metaType;
				if (metaType == null || metaType.enumOleDbType != value)
				{
					this.PropertyTypeChanging();
					this._metaType = NativeDBType.FromDataType(value);
				}
			}
		}

		// Token: 0x060025D8 RID: 9688 RVA: 0x001022A0 File Offset: 0x001016A0
		private bool ShouldSerializeOleDbType()
		{
			return this._metaType != null;
		}

		// Token: 0x060025D9 RID: 9689 RVA: 0x001022B8 File Offset: 0x001016B8
		public void ResetOleDbType()
		{
			if (this._metaType != null)
			{
				this.PropertyTypeChanging();
				this._metaType = null;
			}
		}

		// Token: 0x17000623 RID: 1571
		// (get) Token: 0x060025DA RID: 9690 RVA: 0x001022DC File Offset: 0x001016DC
		// (set) Token: 0x060025DB RID: 9691 RVA: 0x001022FC File Offset: 0x001016FC
		[ResDescription("DbParameter_ParameterName")]
		[ResCategory("DataCategory_Data")]
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
					this.PropertyChanging();
					this._parameterName = value;
				}
			}
		}

		// Token: 0x17000624 RID: 1572
		// (get) Token: 0x060025DC RID: 9692 RVA: 0x00102324 File Offset: 0x00101724
		// (set) Token: 0x060025DD RID: 9693 RVA: 0x00102338 File Offset: 0x00101738
		[ResCategory("DataCategory_Data")]
		[DefaultValue(0)]
		[ResDescription("DbDataParameter_Precision")]
		public new byte Precision
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

		// Token: 0x17000625 RID: 1573
		// (get) Token: 0x060025DE RID: 9694 RVA: 0x0010234C File Offset: 0x0010174C
		// (set) Token: 0x060025DF RID: 9695 RVA: 0x00102374 File Offset: 0x00101774
		internal byte PrecisionInternal
		{
			get
			{
				byte b = this._precision;
				if (b == 0)
				{
					b = this.ValuePrecision(this.Value);
				}
				return b;
			}
			set
			{
				if (this._precision != value)
				{
					this.PropertyChanging();
					this._precision = value;
				}
			}
		}

		// Token: 0x060025E0 RID: 9696 RVA: 0x00102398 File Offset: 0x00101798
		private bool ShouldSerializePrecision()
		{
			return this._precision > 0;
		}

		// Token: 0x17000626 RID: 1574
		// (get) Token: 0x060025E1 RID: 9697 RVA: 0x001023B0 File Offset: 0x001017B0
		// (set) Token: 0x060025E2 RID: 9698 RVA: 0x001023C4 File Offset: 0x001017C4
		[ResDescription("DbDataParameter_Scale")]
		[DefaultValue(0)]
		[ResCategory("DataCategory_Data")]
		public new byte Scale
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

		// Token: 0x17000627 RID: 1575
		// (get) Token: 0x060025E3 RID: 9699 RVA: 0x001023D8 File Offset: 0x001017D8
		// (set) Token: 0x060025E4 RID: 9700 RVA: 0x00102404 File Offset: 0x00101804
		internal byte ScaleInternal
		{
			get
			{
				byte b = this._scale;
				if (!this.ShouldSerializeScale(b))
				{
					b = this.ValueScale(this.Value);
				}
				return b;
			}
			set
			{
				if (this._scale != value || !this._hasScale)
				{
					this.PropertyChanging();
					this._scale = value;
					this._hasScale = true;
				}
			}
		}

		// Token: 0x060025E5 RID: 9701 RVA: 0x00102438 File Offset: 0x00101838
		private bool ShouldSerializeScale()
		{
			return this.ShouldSerializeScale(this._scale);
		}

		// Token: 0x060025E6 RID: 9702 RVA: 0x00102454 File Offset: 0x00101854
		private bool ShouldSerializeScale(byte scale)
		{
			return this._hasScale && (scale != 0 || this.ShouldSerializePrecision());
		}

		// Token: 0x060025E7 RID: 9703 RVA: 0x00102478 File Offset: 0x00101878
		object ICloneable.Clone()
		{
			return new OleDbParameter(this);
		}

		// Token: 0x060025E8 RID: 9704 RVA: 0x0010248C File Offset: 0x0010188C
		private void CloneHelper(OleDbParameter destination)
		{
			this.CloneHelperCore(destination);
			destination._metaType = this._metaType;
			destination._parameterName = this._parameterName;
			destination._precision = this._precision;
			destination._scale = this._scale;
			destination._hasScale = this._hasScale;
		}

		// Token: 0x060025E9 RID: 9705 RVA: 0x001024DC File Offset: 0x001018DC
		private void PropertyChanging()
		{
			this._changeID++;
		}

		// Token: 0x060025EA RID: 9706 RVA: 0x001024F8 File Offset: 0x001018F8
		private void PropertyTypeChanging()
		{
			this.PropertyChanging();
			this._coerceMetaType = null;
			this.CoercedValue = null;
		}

		// Token: 0x060025EB RID: 9707 RVA: 0x0010251C File Offset: 0x0010191C
		internal bool BindParameter(int index, Bindings bindings)
		{
			int changeID = this._changeID;
			object obj = this.Value;
			NativeDBType bindType = this.GetBindType(obj);
			if (bindType.enumOleDbType == OleDbType.Empty)
			{
				throw ODB.UninitializedParameters(index, bindType.enumOleDbType);
			}
			this._coerceMetaType = bindType;
			obj = OleDbParameter.CoerceValue(obj, bindType);
			this.CoercedValue = obj;
			ParameterDirection direction = this.Direction;
			byte b;
			if (this.ShouldSerializePrecision())
			{
				b = this.PrecisionInternal;
			}
			else
			{
				b = this.ValuePrecision(obj);
			}
			if (b == 0)
			{
				b = bindType.maxpre;
			}
			byte scale;
			if (this.ShouldSerializeScale())
			{
				scale = this.ScaleInternal;
			}
			else
			{
				scale = this.ValueScale(obj);
			}
			int num = (int)bindType.wType;
			int num2;
			int num3;
			if (bindType.islong)
			{
				num2 = ADP.PtrSize;
				if (this.ShouldSerializeSize())
				{
					num3 = this.Size;
				}
				else if (129 == bindType.dbType)
				{
					num3 = int.MaxValue;
				}
				else if (130 == bindType.dbType)
				{
					num3 = 1073741823;
				}
				else
				{
					num3 = int.MaxValue;
				}
				num |= 16384;
			}
			else if (bindType.IsVariableLength)
			{
				if (!this.ShouldSerializeSize() && ADP.IsDirection(this, ParameterDirection.Output))
				{
					throw ADP.UninitializedParameterSize(index, this._coerceMetaType.dataType);
				}
				bool flag;
				if (this.ShouldSerializeSize())
				{
					num3 = this.Size;
					flag = false;
				}
				else
				{
					num3 = this.ValueSize(obj);
					flag = true;
				}
				if (0 < num3)
				{
					if (130 == bindType.wType)
					{
						num2 = Math.Min(num3, 1073741822) * 2 + 2;
					}
					else
					{
						num2 = num3;
					}
					if (flag && 129 == bindType.dbType)
					{
						num3 = Math.Min(num3, 1073741822) * 2;
					}
					if (8192 < num2)
					{
						num2 = ADP.PtrSize;
						num |= 16384;
					}
				}
				else if (num3 == 0)
				{
					if (130 == num)
					{
						num2 = 2;
					}
					else
					{
						num2 = 0;
					}
				}
				else
				{
					if (-1 != num3)
					{
						throw ADP.InvalidSizeValue(num3);
					}
					num2 = ADP.PtrSize;
					num |= 16384;
				}
			}
			else
			{
				num2 = bindType.fixlen;
				num3 = num2;
			}
			bindings.CurrentIndex = index;
			bindings.DataSourceType = bindType.dbString.DangerousGetHandle();
			bindings.Name = ADP.PtrZero;
			bindings.ParamSize = new IntPtr(num3);
			bindings.Flags = OleDbParameter.GetBindFlags(direction);
			bindings.Ordinal = (IntPtr)(index + 1);
			bindings.Part = bindType.dbPart;
			bindings.ParamIO = OleDbParameter.GetBindDirection(direction);
			bindings.Precision = b;
			bindings.Scale = scale;
			bindings.DbType = num;
			bindings.MaxLen = num2;
			if (Bid.AdvancedOn)
			{
				Bid.Trace("<oledb.struct.tagDBPARAMBINDINFO|INFO|ADV> index=%d, parameterName='%ls'\n", index, this.ParameterName);
				Bid.Trace("<oledb.struct.tagDBBINDING|INFO|ADV>\n");
			}
			return this.IsParameterComputed();
		}

		// Token: 0x060025EC RID: 9708 RVA: 0x001027AC File Offset: 0x00101BAC
		private static object CoerceValue(object value, NativeDBType destinationType)
		{
			if (value != null && DBNull.Value != value && typeof(object) != destinationType.dataType)
			{
				Type type = value.GetType();
				if (type != destinationType.dataType)
				{
					try
					{
						if (!(typeof(string) == destinationType.dataType) || !(typeof(char[]) == type))
						{
							if (6 == destinationType.dbType && typeof(string) == type)
							{
								value = decimal.Parse((string)value, NumberStyles.Currency, null);
							}
							else
							{
								value = Convert.ChangeType(value, destinationType.dataType, null);
							}
						}
					}
					catch (Exception ex)
					{
						if (!ADP.IsCatchableExceptionType(ex))
						{
							throw;
						}
						throw ADP.ParameterConversionFailed(value, destinationType.dataType, ex);
					}
				}
			}
			return value;
		}

		// Token: 0x060025ED RID: 9709 RVA: 0x001028A4 File Offset: 0x00101CA4
		private NativeDBType GetBindType(object value)
		{
			NativeDBType nativeDBType = this._metaType;
			if (nativeDBType == null)
			{
				if (ADP.IsNull(value))
				{
					nativeDBType = NativeDBType.Default;
				}
				else
				{
					nativeDBType = NativeDBType.FromSystemType(value);
				}
			}
			return nativeDBType;
		}

		// Token: 0x060025EE RID: 9710 RVA: 0x001028D4 File Offset: 0x00101CD4
		internal object GetCoercedValue()
		{
			object obj = this.CoercedValue;
			if (obj == null)
			{
				obj = OleDbParameter.CoerceValue(this.Value, this._coerceMetaType);
				this.CoercedValue = obj;
			}
			return obj;
		}

		// Token: 0x060025EF RID: 9711 RVA: 0x00102908 File Offset: 0x00101D08
		internal bool IsParameterComputed()
		{
			NativeDBType metaType = this._metaType;
			return metaType == null || (!this.ShouldSerializeSize() && metaType.IsVariableLength) || 14 == metaType.dbType || (131 == metaType.dbType && (!this.ShouldSerializeScale() || !this.ShouldSerializePrecision()));
		}

		// Token: 0x060025F0 RID: 9712 RVA: 0x00102960 File Offset: 0x00101D60
		internal void Prepare(OleDbCommand cmd)
		{
			if (this._metaType == null)
			{
				throw ADP.PrepareParameterType(cmd);
			}
			if (!this.ShouldSerializeSize() && this._metaType.IsVariableLength)
			{
				throw ADP.PrepareParameterSize(cmd);
			}
			if (!this.ShouldSerializePrecision() && !this.ShouldSerializeScale() && (14 == this._metaType.wType || 131 == this._metaType.wType))
			{
				throw ADP.PrepareParameterScale(cmd, this._metaType.wType.ToString("G", CultureInfo.InvariantCulture));
			}
		}

		// Token: 0x17000628 RID: 1576
		// (get) Token: 0x060025F1 RID: 9713 RVA: 0x001029F0 File Offset: 0x00101DF0
		// (set) Token: 0x060025F2 RID: 9714 RVA: 0x00102A04 File Offset: 0x00101E04
		[RefreshProperties(RefreshProperties.All)]
		[TypeConverter(typeof(StringConverter))]
		[ResCategory("DataCategory_Data")]
		[ResDescription("DbParameter_Value")]
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

		// Token: 0x060025F3 RID: 9715 RVA: 0x00102A20 File Offset: 0x00101E20
		private byte ValuePrecision(object value)
		{
			return this.ValuePrecisionCore(value);
		}

		// Token: 0x060025F4 RID: 9716 RVA: 0x00102A34 File Offset: 0x00101E34
		private byte ValueScale(object value)
		{
			return this.ValueScaleCore(value);
		}

		// Token: 0x060025F5 RID: 9717 RVA: 0x00102A48 File Offset: 0x00101E48
		private int ValueSize(object value)
		{
			return this.ValueSizeCore(value);
		}

		// Token: 0x060025F6 RID: 9718 RVA: 0x00102A5C File Offset: 0x00101E5C
		private static int GetBindDirection(ParameterDirection direction)
		{
			return (int)(ParameterDirection.InputOutput & direction);
		}

		// Token: 0x060025F7 RID: 9719 RVA: 0x00102A6C File Offset: 0x00101E6C
		private static int GetBindFlags(ParameterDirection direction)
		{
			return (int)(ParameterDirection.InputOutput & direction);
		}

		// Token: 0x060025F8 RID: 9720 RVA: 0x00102A7C File Offset: 0x00101E7C
		private OleDbParameter(OleDbParameter source) : this()
		{
			ADP.CheckArgumentNull(source, "source");
			source.CloneHelper(this);
			ICloneable cloneable = this._value as ICloneable;
			if (cloneable != null)
			{
				this._value = cloneable.Clone();
			}
		}

		// Token: 0x17000629 RID: 1577
		// (get) Token: 0x060025F9 RID: 9721 RVA: 0x00102ABC File Offset: 0x00101EBC
		// (set) Token: 0x060025FA RID: 9722 RVA: 0x00102AD0 File Offset: 0x00101ED0
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

		// Token: 0x1700062A RID: 1578
		// (get) Token: 0x060025FB RID: 9723 RVA: 0x00102AE4 File Offset: 0x00101EE4
		// (set) Token: 0x060025FC RID: 9724 RVA: 0x00102B00 File Offset: 0x00101F00
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
				if (this._direction == value)
				{
					return;
				}
				if (value - ParameterDirection.Input <= 2 || value == ParameterDirection.ReturnValue)
				{
					this.PropertyChanging();
					this._direction = value;
					return;
				}
				throw ADP.InvalidParameterDirection(value);
			}
		}

		// Token: 0x1700062B RID: 1579
		// (get) Token: 0x060025FD RID: 9725 RVA: 0x00102B38 File Offset: 0x00101F38
		// (set) Token: 0x060025FE RID: 9726 RVA: 0x00102B4C File Offset: 0x00101F4C
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

		// Token: 0x1700062C RID: 1580
		// (get) Token: 0x060025FF RID: 9727 RVA: 0x00102B60 File Offset: 0x00101F60
		internal int Offset
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x1700062D RID: 1581
		// (get) Token: 0x06002600 RID: 9728 RVA: 0x00102B70 File Offset: 0x00101F70
		// (set) Token: 0x06002601 RID: 9729 RVA: 0x00102B98 File Offset: 0x00101F98
		[ResDescription("DbParameter_Size")]
		[ResCategory("DataCategory_Data")]
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

		// Token: 0x06002602 RID: 9730 RVA: 0x00102BC8 File Offset: 0x00101FC8
		private void ResetSize()
		{
			if (this._size != 0)
			{
				this.PropertyChanging();
				this._size = 0;
			}
		}

		// Token: 0x06002603 RID: 9731 RVA: 0x00102BEC File Offset: 0x00101FEC
		private bool ShouldSerializeSize()
		{
			return this._size != 0;
		}

		// Token: 0x1700062E RID: 1582
		// (get) Token: 0x06002604 RID: 9732 RVA: 0x00102C04 File Offset: 0x00102004
		// (set) Token: 0x06002605 RID: 9733 RVA: 0x00102C24 File Offset: 0x00102024
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

		// Token: 0x1700062F RID: 1583
		// (get) Token: 0x06002606 RID: 9734 RVA: 0x00102C38 File Offset: 0x00102038
		// (set) Token: 0x06002607 RID: 9735 RVA: 0x00102C4C File Offset: 0x0010204C
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

		// Token: 0x17000630 RID: 1584
		// (get) Token: 0x06002608 RID: 9736 RVA: 0x00102C60 File Offset: 0x00102060
		// (set) Token: 0x06002609 RID: 9737 RVA: 0x00102C80 File Offset: 0x00102080
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
						goto IL_32;
					}
				}
				else if (value != DataRowVersion.Proposed && value != DataRowVersion.Default)
				{
					goto IL_32;
				}
				this._sourceVersion = value;
				return;
				IL_32:
				throw ADP.InvalidDataRowVersion(value);
			}
		}

		// Token: 0x0600260A RID: 9738 RVA: 0x00102CC8 File Offset: 0x001020C8
		private void CloneHelperCore(OleDbParameter destination)
		{
			destination._value = this._value;
			destination._direction = this._direction;
			destination._size = this._size;
			destination._sourceColumn = this._sourceColumn;
			destination._sourceVersion = this._sourceVersion;
			destination._sourceColumnNullMapping = this._sourceColumnNullMapping;
			destination._isNullable = this._isNullable;
		}

		// Token: 0x0600260B RID: 9739 RVA: 0x00102D2C File Offset: 0x0010212C
		internal void CopyTo(DbParameter destination)
		{
			ADP.CheckArgumentNull(destination, "destination");
			this.CloneHelper((OleDbParameter)destination);
		}

		// Token: 0x0600260C RID: 9740 RVA: 0x00102D50 File Offset: 0x00102150
		internal object CompareExchangeParent(object value, object comparand)
		{
			object parent = this._parent;
			if (comparand == parent)
			{
				this._parent = value;
			}
			return parent;
		}

		// Token: 0x0600260D RID: 9741 RVA: 0x00102D70 File Offset: 0x00102170
		internal void ResetParent()
		{
			this._parent = null;
		}

		// Token: 0x0600260E RID: 9742 RVA: 0x00102D84 File Offset: 0x00102184
		public override string ToString()
		{
			return this.ParameterName;
		}

		// Token: 0x0600260F RID: 9743 RVA: 0x00102D98 File Offset: 0x00102198
		private byte ValuePrecisionCore(object value)
		{
			if (value is decimal)
			{
				return ((decimal)value).Precision;
			}
			return 0;
		}

		// Token: 0x06002610 RID: 9744 RVA: 0x00102DC4 File Offset: 0x001021C4
		private byte ValueScaleCore(object value)
		{
			if (value is decimal)
			{
				return (byte)((decimal.GetBits((decimal)value)[3] & 16711680) >> 16);
			}
			return 0;
		}

		// Token: 0x06002611 RID: 9745 RVA: 0x00102DF4 File Offset: 0x001021F4
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

		// Token: 0x04001758 RID: 5976
		private NativeDBType _metaType;

		// Token: 0x04001759 RID: 5977
		private int _changeID;

		// Token: 0x0400175A RID: 5978
		private string _parameterName;

		// Token: 0x0400175B RID: 5979
		private byte _precision;

		// Token: 0x0400175C RID: 5980
		private byte _scale;

		// Token: 0x0400175D RID: 5981
		private bool _hasScale;

		// Token: 0x0400175E RID: 5982
		private NativeDBType _coerceMetaType;

		// Token: 0x0400175F RID: 5983
		private object _value;

		// Token: 0x04001760 RID: 5984
		private object _parent;

		// Token: 0x04001761 RID: 5985
		private ParameterDirection _direction;

		// Token: 0x04001762 RID: 5986
		private int _size;

		// Token: 0x04001763 RID: 5987
		private string _sourceColumn;

		// Token: 0x04001764 RID: 5988
		private DataRowVersion _sourceVersion;

		// Token: 0x04001765 RID: 5989
		private bool _sourceColumnNullMapping;

		// Token: 0x04001766 RID: 5990
		private bool _isNullable;

		// Token: 0x04001767 RID: 5991
		private object _coercedValue;

		// Token: 0x02000407 RID: 1031
		internal sealed class OleDbParameterConverter : ExpandableObjectConverter
		{
			// Token: 0x060035DE RID: 13790 RVA: 0x001473B8 File Offset: 0x001467B8
			public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
			{
				return typeof(InstanceDescriptor) == destinationType || base.CanConvertTo(context, destinationType);
			}

			// Token: 0x060035DF RID: 13791 RVA: 0x001473E4 File Offset: 0x001467E4
			public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
			{
				if (null == destinationType)
				{
					throw ADP.ArgumentNull("destinationType");
				}
				if (typeof(InstanceDescriptor) == destinationType && value is OleDbParameter)
				{
					return this.ConvertToInstanceDescriptor(value as OleDbParameter);
				}
				return base.ConvertTo(context, culture, value, destinationType);
			}

			// Token: 0x060035E0 RID: 13792 RVA: 0x0014743C File Offset: 0x0014683C
			private InstanceDescriptor ConvertToInstanceDescriptor(OleDbParameter p)
			{
				int num = 0;
				if (p.ShouldSerializeOleDbType())
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
						typeof(OleDbType)
					};
					arguments = new object[]
					{
						p.ParameterName,
						p.OleDbType
					};
					break;
				case 2:
				case 3:
					types = new Type[]
					{
						typeof(string),
						typeof(OleDbType),
						typeof(int)
					};
					arguments = new object[]
					{
						p.ParameterName,
						p.OleDbType,
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
						typeof(OleDbType),
						typeof(int),
						typeof(string)
					};
					arguments = new object[]
					{
						p.ParameterName,
						p.OleDbType,
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
							typeof(OleDbType),
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
							p.OleDbType,
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
							typeof(OleDbType),
							typeof(int),
							typeof(ParameterDirection),
							typeof(byte),
							typeof(byte),
							typeof(string),
							typeof(DataRowVersion),
							typeof(bool),
							typeof(object)
						};
						arguments = new object[]
						{
							p.ParameterName,
							p.OleDbType,
							p.Size,
							p.Direction,
							p.PrecisionInternal,
							p.ScaleInternal,
							p.SourceColumn,
							p.SourceVersion,
							p.SourceColumnNullMapping,
							p.Value
						};
					}
					break;
				}
				ConstructorInfo constructor = typeof(OleDbParameter).GetConstructor(types);
				return new InstanceDescriptor(constructor, arguments);
			}
		}
	}
}
