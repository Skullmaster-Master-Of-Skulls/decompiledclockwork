using System;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Data.Common;
using System.Globalization;
using System.Reflection;

namespace System.Data.OleDb
{
	// Token: 0x02000232 RID: 562
	[TypeConverter(typeof(OleDbParameter.OleDbParameterConverter))]
	public sealed class OleDbParameter : DbParameter, ICloneable, IDbDataParameter, IDataParameter
	{
		// Token: 0x06001FB5 RID: 8117 RVA: 0x0027CD78 File Offset: 0x0027C178
		public OleDbParameter()
		{
		}

		// Token: 0x06001FB6 RID: 8118 RVA: 0x0027CD98 File Offset: 0x0027C198
		public OleDbParameter(string name, object value) : this()
		{
			this.ParameterName = name;
			this.Value = value;
		}

		// Token: 0x06001FB7 RID: 8119 RVA: 0x0027CDC8 File Offset: 0x0027C1C8
		public OleDbParameter(string name, OleDbType dataType) : this()
		{
			this.ParameterName = name;
			this.OleDbType = dataType;
		}

		// Token: 0x06001FB8 RID: 8120 RVA: 0x0027CDF8 File Offset: 0x0027C1F8
		public OleDbParameter(string name, OleDbType dataType, int size) : this()
		{
			this.ParameterName = name;
			this.OleDbType = dataType;
			this.Size = size;
		}

		// Token: 0x06001FB9 RID: 8121 RVA: 0x0027CE28 File Offset: 0x0027C228
		public OleDbParameter(string name, OleDbType dataType, int size, string srcColumn) : this()
		{
			this.ParameterName = name;
			this.OleDbType = dataType;
			this.Size = size;
			this.SourceColumn = srcColumn;
		}

		// Token: 0x06001FBA RID: 8122 RVA: 0x0027CE58 File Offset: 0x0027C258
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

		// Token: 0x06001FBB RID: 8123 RVA: 0x0027CEB8 File Offset: 0x0027C2B8
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

		// Token: 0x17000457 RID: 1111
		// (get) Token: 0x06001FBC RID: 8124 RVA: 0x0027CF18 File Offset: 0x0027C318
		internal int ChangeID
		{
			get
			{
				return this._changeID;
			}
		}

		// Token: 0x17000458 RID: 1112
		// (get) Token: 0x06001FBD RID: 8125 RVA: 0x0027CF38 File Offset: 0x0027C338
		// (set) Token: 0x06001FBE RID: 8126 RVA: 0x0027CF58 File Offset: 0x0027C358
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

		// Token: 0x06001FBF RID: 8127 RVA: 0x0027CF98 File Offset: 0x0027C398
		public override void ResetDbType()
		{
			this.ResetOleDbType();
		}

		// Token: 0x17000459 RID: 1113
		// (get) Token: 0x06001FC0 RID: 8128 RVA: 0x0027CFB8 File Offset: 0x0027C3B8
		// (set) Token: 0x06001FC1 RID: 8129 RVA: 0x0027CFD8 File Offset: 0x0027C3D8
		[ResDescription("OleDbParameter_OleDbType")]
		[RefreshProperties(RefreshProperties.All)]
		[DbProviderSpecificTypeProperty(true)]
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

		// Token: 0x06001FC2 RID: 8130 RVA: 0x0027D018 File Offset: 0x0027C418
		private bool ShouldSerializeOleDbType()
		{
			return null != this._metaType;
		}

		// Token: 0x06001FC3 RID: 8131 RVA: 0x0027D038 File Offset: 0x0027C438
		public void ResetOleDbType()
		{
			if (this._metaType != null)
			{
				this.PropertyTypeChanging();
				this._metaType = null;
			}
		}

		// Token: 0x1700045A RID: 1114
		// (get) Token: 0x06001FC4 RID: 8132 RVA: 0x0027D068 File Offset: 0x0027C468
		// (set) Token: 0x06001FC5 RID: 8133 RVA: 0x0027D088 File Offset: 0x0027C488
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
					this.PropertyChanging();
					this._parameterName = value;
				}
			}
		}

		// Token: 0x1700045B RID: 1115
		// (get) Token: 0x06001FC6 RID: 8134 RVA: 0x0027D0B8 File Offset: 0x0027C4B8
		// (set) Token: 0x06001FC7 RID: 8135 RVA: 0x0027D0D8 File Offset: 0x0027C4D8
		[ResCategory("DataCategory_Data")]
		[DefaultValue(0)]
		[ResDescription("DbDataParameter_Precision")]
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

		// Token: 0x1700045C RID: 1116
		// (get) Token: 0x06001FC8 RID: 8136 RVA: 0x0027D0F8 File Offset: 0x0027C4F8
		// (set) Token: 0x06001FC9 RID: 8137 RVA: 0x0027D128 File Offset: 0x0027C528
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

		// Token: 0x06001FCA RID: 8138 RVA: 0x0027D158 File Offset: 0x0027C558
		private bool ShouldSerializePrecision()
		{
			return 0 != this._precision;
		}

		// Token: 0x1700045D RID: 1117
		// (get) Token: 0x06001FCB RID: 8139 RVA: 0x0027D178 File Offset: 0x0027C578
		// (set) Token: 0x06001FCC RID: 8140 RVA: 0x0027D198 File Offset: 0x0027C598
		[ResDescription("DbDataParameter_Scale")]
		[DefaultValue(0)]
		[ResCategory("DataCategory_Data")]
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

		// Token: 0x1700045E RID: 1118
		// (get) Token: 0x06001FCD RID: 8141 RVA: 0x0027D1B8 File Offset: 0x0027C5B8
		// (set) Token: 0x06001FCE RID: 8142 RVA: 0x0027D1E8 File Offset: 0x0027C5E8
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

		// Token: 0x06001FCF RID: 8143 RVA: 0x0027D228 File Offset: 0x0027C628
		private bool ShouldSerializeScale()
		{
			return this.ShouldSerializeScale(this._scale);
		}

		// Token: 0x06001FD0 RID: 8144 RVA: 0x0027D248 File Offset: 0x0027C648
		private bool ShouldSerializeScale(byte scale)
		{
			return this._hasScale && (scale != 0 || this.ShouldSerializePrecision());
		}

		// Token: 0x06001FD1 RID: 8145 RVA: 0x0027D278 File Offset: 0x0027C678
		object ICloneable.Clone()
		{
			return new OleDbParameter(this);
		}

		// Token: 0x06001FD2 RID: 8146 RVA: 0x0027D298 File Offset: 0x0027C698
		private void CloneHelper(OleDbParameter destination)
		{
			this.CloneHelperCore(destination);
			destination._metaType = this._metaType;
			destination._parameterName = this._parameterName;
			destination._precision = this._precision;
			destination._scale = this._scale;
			destination._hasScale = this._hasScale;
		}

		// Token: 0x06001FD3 RID: 8147 RVA: 0x0027D2E8 File Offset: 0x0027C6E8
		private void PropertyChanging()
		{
			this._changeID++;
		}

		// Token: 0x06001FD4 RID: 8148 RVA: 0x0027D308 File Offset: 0x0027C708
		private void PropertyTypeChanging()
		{
			this.PropertyChanging();
			this._coerceMetaType = null;
			this.CoercedValue = null;
		}

		// Token: 0x06001FD5 RID: 8149 RVA: 0x0027D338 File Offset: 0x0027C738
		internal bool BindParameter(int index, Bindings bindings)
		{
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

		// Token: 0x06001FD6 RID: 8150 RVA: 0x0027D5C8 File Offset: 0x0027C9C8
		private static object CoerceValue(object value, NativeDBType destinationType)
		{
			if (value != null && DBNull.Value != value && typeof(object) != destinationType.dataType)
			{
				Type type = value.GetType();
				if (type != destinationType.dataType)
				{
					try
					{
						if (typeof(string) != destinationType.dataType || typeof(char[]) != type)
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

		// Token: 0x06001FD7 RID: 8151 RVA: 0x0027D6A8 File Offset: 0x0027CAA8
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

		// Token: 0x06001FD8 RID: 8152 RVA: 0x0027D6D8 File Offset: 0x0027CAD8
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

		// Token: 0x06001FD9 RID: 8153 RVA: 0x0027D718 File Offset: 0x0027CB18
		internal bool IsParameterComputed()
		{
			NativeDBType metaType = this._metaType;
			return metaType == null || (!this.ShouldSerializeSize() && metaType.IsVariableLength) || 14 == metaType.dbType || (131 == metaType.dbType && (!this.ShouldSerializeScale() || !this.ShouldSerializePrecision()));
		}

		// Token: 0x06001FDA RID: 8154 RVA: 0x0027D778 File Offset: 0x0027CB78
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

		// Token: 0x1700045F RID: 1119
		// (get) Token: 0x06001FDB RID: 8155 RVA: 0x0027D808 File Offset: 0x0027CC08
		// (set) Token: 0x06001FDC RID: 8156 RVA: 0x0027D828 File Offset: 0x0027CC28
		[ResCategory("DataCategory_Data")]
		[RefreshProperties(RefreshProperties.All)]
		[ResDescription("DbParameter_Value")]
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

		// Token: 0x06001FDD RID: 8157 RVA: 0x0027D848 File Offset: 0x0027CC48
		private byte ValuePrecision(object value)
		{
			return this.ValuePrecisionCore(value);
		}

		// Token: 0x06001FDE RID: 8158 RVA: 0x0027D868 File Offset: 0x0027CC68
		private byte ValueScale(object value)
		{
			return this.ValueScaleCore(value);
		}

		// Token: 0x06001FDF RID: 8159 RVA: 0x0027D888 File Offset: 0x0027CC88
		private int ValueSize(object value)
		{
			return this.ValueSizeCore(value);
		}

		// Token: 0x06001FE0 RID: 8160 RVA: 0x0027D8A8 File Offset: 0x0027CCA8
		private static int GetBindDirection(ParameterDirection direction)
		{
			return (int)(ParameterDirection.InputOutput & direction);
		}

		// Token: 0x06001FE1 RID: 8161 RVA: 0x0027D8B8 File Offset: 0x0027CCB8
		private static int GetBindFlags(ParameterDirection direction)
		{
			return (int)(ParameterDirection.InputOutput & direction);
		}

		// Token: 0x06001FE2 RID: 8162 RVA: 0x0027D8C8 File Offset: 0x0027CCC8
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

		// Token: 0x17000460 RID: 1120
		// (get) Token: 0x06001FE3 RID: 8163 RVA: 0x0027D908 File Offset: 0x0027CD08
		// (set) Token: 0x06001FE4 RID: 8164 RVA: 0x0027D928 File Offset: 0x0027CD28
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

		// Token: 0x17000461 RID: 1121
		// (get) Token: 0x06001FE5 RID: 8165 RVA: 0x0027D948 File Offset: 0x0027CD48
		// (set) Token: 0x06001FE6 RID: 8166 RVA: 0x0027D968 File Offset: 0x0027CD68
		[ResCategory("DataCategory_Data")]
		[ResDescription("DbParameter_Direction")]
		[RefreshProperties(RefreshProperties.All)]
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

		// Token: 0x17000462 RID: 1122
		// (get) Token: 0x06001FE7 RID: 8167 RVA: 0x0027D9B8 File Offset: 0x0027CDB8
		// (set) Token: 0x06001FE8 RID: 8168 RVA: 0x0027D9D8 File Offset: 0x0027CDD8
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

		// Token: 0x17000463 RID: 1123
		// (get) Token: 0x06001FE9 RID: 8169 RVA: 0x0027D9F8 File Offset: 0x0027CDF8
		internal int Offset
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000464 RID: 1124
		// (get) Token: 0x06001FEA RID: 8170 RVA: 0x0027DA08 File Offset: 0x0027CE08
		// (set) Token: 0x06001FEB RID: 8171 RVA: 0x0027DA38 File Offset: 0x0027CE38
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

		// Token: 0x06001FEC RID: 8172 RVA: 0x0027DA68 File Offset: 0x0027CE68
		private void ResetSize()
		{
			if (this._size != 0)
			{
				this.PropertyChanging();
				this._size = 0;
			}
		}

		// Token: 0x06001FED RID: 8173 RVA: 0x0027DA98 File Offset: 0x0027CE98
		private bool ShouldSerializeSize()
		{
			return 0 != this._size;
		}

		// Token: 0x17000465 RID: 1125
		// (get) Token: 0x06001FEE RID: 8174 RVA: 0x0027DAB8 File Offset: 0x0027CEB8
		// (set) Token: 0x06001FEF RID: 8175 RVA: 0x0027DAD8 File Offset: 0x0027CED8
		[ResCategory("DataCategory_Update")]
		[ResDescription("DbParameter_SourceColumn")]
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

		// Token: 0x17000466 RID: 1126
		// (get) Token: 0x06001FF0 RID: 8176 RVA: 0x0027DAF8 File Offset: 0x0027CEF8
		// (set) Token: 0x06001FF1 RID: 8177 RVA: 0x0027DB18 File Offset: 0x0027CF18
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

		// Token: 0x17000467 RID: 1127
		// (get) Token: 0x06001FF2 RID: 8178 RVA: 0x0027DB38 File Offset: 0x0027CF38
		// (set) Token: 0x06001FF3 RID: 8179 RVA: 0x0027DB58 File Offset: 0x0027CF58
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

		// Token: 0x06001FF4 RID: 8180 RVA: 0x0027DBA8 File Offset: 0x0027CFA8
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

		// Token: 0x06001FF5 RID: 8181 RVA: 0x0027DC18 File Offset: 0x0027D018
		internal void CopyTo(DbParameter destination)
		{
			ADP.CheckArgumentNull(destination, "destination");
			this.CloneHelper((OleDbParameter)destination);
		}

		// Token: 0x06001FF6 RID: 8182 RVA: 0x0027DC48 File Offset: 0x0027D048
		internal object CompareExchangeParent(object value, object comparand)
		{
			object parent = this._parent;
			if (comparand == parent)
			{
				this._parent = value;
			}
			return parent;
		}

		// Token: 0x06001FF7 RID: 8183 RVA: 0x0027DC68 File Offset: 0x0027D068
		internal void ResetParent()
		{
			this._parent = null;
		}

		// Token: 0x06001FF8 RID: 8184 RVA: 0x0027DC88 File Offset: 0x0027D088
		public override string ToString()
		{
			return this.ParameterName;
		}

		// Token: 0x06001FF9 RID: 8185 RVA: 0x0027DCA8 File Offset: 0x0027D0A8
		private byte ValuePrecisionCore(object value)
		{
			if (value is decimal)
			{
				return ((decimal)value).Precision;
			}
			return 0;
		}

		// Token: 0x06001FFA RID: 8186 RVA: 0x0027DCD8 File Offset: 0x0027D0D8
		private byte ValueScaleCore(object value)
		{
			if (value is decimal)
			{
				return (byte)((decimal.GetBits((decimal)value)[3] & 16711680) >> 16);
			}
			return 0;
		}

		// Token: 0x06001FFB RID: 8187 RVA: 0x0027DD08 File Offset: 0x0027D108
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

		// Token: 0x04001445 RID: 5189
		private NativeDBType _metaType;

		// Token: 0x04001446 RID: 5190
		private int _changeID;

		// Token: 0x04001447 RID: 5191
		private string _parameterName;

		// Token: 0x04001448 RID: 5192
		private byte _precision;

		// Token: 0x04001449 RID: 5193
		private byte _scale;

		// Token: 0x0400144A RID: 5194
		private bool _hasScale;

		// Token: 0x0400144B RID: 5195
		private NativeDBType _coerceMetaType;

		// Token: 0x0400144C RID: 5196
		private object _value;

		// Token: 0x0400144D RID: 5197
		private object _parent;

		// Token: 0x0400144E RID: 5198
		private ParameterDirection _direction;

		// Token: 0x0400144F RID: 5199
		private int _size;

		// Token: 0x04001450 RID: 5200
		private string _sourceColumn;

		// Token: 0x04001451 RID: 5201
		private DataRowVersion _sourceVersion;

		// Token: 0x04001452 RID: 5202
		private bool _sourceColumnNullMapping;

		// Token: 0x04001453 RID: 5203
		private bool _isNullable;

		// Token: 0x04001454 RID: 5204
		private object _coercedValue;

		// Token: 0x02000233 RID: 563
		internal sealed class OleDbParameterConverter : ExpandableObjectConverter
		{
			// Token: 0x06001FFD RID: 8189 RVA: 0x0027DD88 File Offset: 0x0027D188
			public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
			{
				return typeof(InstanceDescriptor) == destinationType || base.CanConvertTo(context, destinationType);
			}

			// Token: 0x06001FFE RID: 8190 RVA: 0x0027DDB8 File Offset: 0x0027D1B8
			public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
			{
				if (destinationType == null)
				{
					throw ADP.ArgumentNull("destinationType");
				}
				if (typeof(InstanceDescriptor) == destinationType && value is OleDbParameter)
				{
					return this.ConvertToInstanceDescriptor(value as OleDbParameter);
				}
				return base.ConvertTo(context, culture, value, destinationType);
			}

			// Token: 0x06001FFF RID: 8191 RVA: 0x0027DE08 File Offset: 0x0027D208
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
