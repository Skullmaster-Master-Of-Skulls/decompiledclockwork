using System;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Data.Common;
using System.Globalization;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace System.Data.Odbc
{
	// Token: 0x020001F7 RID: 503
	[TypeConverter(typeof(OdbcParameter.OdbcParameterConverter))]
	public sealed class OdbcParameter : DbParameter, ICloneable, IDbDataParameter, IDataParameter
	{
		// Token: 0x06001BC4 RID: 7108 RVA: 0x00265FC8 File Offset: 0x002653C8
		public OdbcParameter()
		{
		}

		// Token: 0x06001BC5 RID: 7109 RVA: 0x00265FE8 File Offset: 0x002653E8
		public OdbcParameter(string name, object value) : this()
		{
			this.ParameterName = name;
			this.Value = value;
		}

		// Token: 0x06001BC6 RID: 7110 RVA: 0x00266018 File Offset: 0x00265418
		public OdbcParameter(string name, OdbcType type) : this()
		{
			this.ParameterName = name;
			this.OdbcType = type;
		}

		// Token: 0x06001BC7 RID: 7111 RVA: 0x00266048 File Offset: 0x00265448
		public OdbcParameter(string name, OdbcType type, int size) : this()
		{
			this.ParameterName = name;
			this.OdbcType = type;
			this.Size = size;
		}

		// Token: 0x06001BC8 RID: 7112 RVA: 0x00266078 File Offset: 0x00265478
		public OdbcParameter(string name, OdbcType type, int size, string sourcecolumn) : this()
		{
			this.ParameterName = name;
			this.OdbcType = type;
			this.Size = size;
			this.SourceColumn = sourcecolumn;
		}

		// Token: 0x06001BC9 RID: 7113 RVA: 0x002660A8 File Offset: 0x002654A8
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public OdbcParameter(string parameterName, OdbcType odbcType, int size, ParameterDirection parameterDirection, bool isNullable, byte precision, byte scale, string srcColumn, DataRowVersion srcVersion, object value) : this()
		{
			this.ParameterName = parameterName;
			this.OdbcType = odbcType;
			this.Size = size;
			this.Direction = parameterDirection;
			this.IsNullable = isNullable;
			this.PrecisionInternal = precision;
			this.ScaleInternal = scale;
			this.SourceColumn = srcColumn;
			this.SourceVersion = srcVersion;
			this.Value = value;
		}

		// Token: 0x06001BCA RID: 7114 RVA: 0x00266108 File Offset: 0x00265508
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public OdbcParameter(string parameterName, OdbcType odbcType, int size, ParameterDirection parameterDirection, byte precision, byte scale, string sourceColumn, DataRowVersion sourceVersion, bool sourceColumnNullMapping, object value) : this()
		{
			this.ParameterName = parameterName;
			this.OdbcType = odbcType;
			this.Size = size;
			this.Direction = parameterDirection;
			this.PrecisionInternal = precision;
			this.ScaleInternal = scale;
			this.SourceColumn = sourceColumn;
			this.SourceVersion = sourceVersion;
			this.SourceColumnNullMapping = sourceColumnNullMapping;
			this.Value = value;
		}

		// Token: 0x170003B8 RID: 952
		// (get) Token: 0x06001BCB RID: 7115 RVA: 0x00266168 File Offset: 0x00265568
		// (set) Token: 0x06001BCC RID: 7116 RVA: 0x00266198 File Offset: 0x00265598
		public override DbType DbType
		{
			get
			{
				if (this._userSpecifiedType)
				{
					return this._typemap._dbType;
				}
				return TypeMap._NVarChar._dbType;
			}
			set
			{
				if (this._typemap == null || this._typemap._dbType != value)
				{
					this.PropertyTypeChanging();
					this._typemap = TypeMap.FromDbType(value);
					this._userSpecifiedType = true;
				}
			}
		}

		// Token: 0x06001BCD RID: 7117 RVA: 0x002661D8 File Offset: 0x002655D8
		public override void ResetDbType()
		{
			this.ResetOdbcType();
		}

		// Token: 0x170003B9 RID: 953
		// (get) Token: 0x06001BCE RID: 7118 RVA: 0x002661F8 File Offset: 0x002655F8
		// (set) Token: 0x06001BCF RID: 7119 RVA: 0x00266228 File Offset: 0x00265628
		[RefreshProperties(RefreshProperties.All)]
		[DefaultValue(OdbcType.NChar)]
		[DbProviderSpecificTypeProperty(true)]
		[ResDescription("OdbcParameter_OdbcType")]
		[ResCategory("DataCategory_Data")]
		public OdbcType OdbcType
		{
			get
			{
				if (this._userSpecifiedType)
				{
					return this._typemap._odbcType;
				}
				return TypeMap._NVarChar._odbcType;
			}
			set
			{
				if (this._typemap == null || this._typemap._odbcType != value)
				{
					this.PropertyTypeChanging();
					this._typemap = TypeMap.FromOdbcType(value);
					this._userSpecifiedType = true;
				}
			}
		}

		// Token: 0x06001BD0 RID: 7120 RVA: 0x00266268 File Offset: 0x00265668
		public void ResetOdbcType()
		{
			this.PropertyTypeChanging();
			this._typemap = null;
			this._userSpecifiedType = false;
		}

		// Token: 0x170003BA RID: 954
		// (set) Token: 0x06001BD1 RID: 7121 RVA: 0x00266298 File Offset: 0x00265698
		internal bool HasChanged
		{
			set
			{
				this._hasChanged = value;
			}
		}

		// Token: 0x170003BB RID: 955
		// (get) Token: 0x06001BD2 RID: 7122 RVA: 0x002662B8 File Offset: 0x002656B8
		internal bool UserSpecifiedType
		{
			get
			{
				return this._userSpecifiedType;
			}
		}

		// Token: 0x170003BC RID: 956
		// (get) Token: 0x06001BD3 RID: 7123 RVA: 0x002662D8 File Offset: 0x002656D8
		// (set) Token: 0x06001BD4 RID: 7124 RVA: 0x002662F8 File Offset: 0x002656F8
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

		// Token: 0x170003BD RID: 957
		// (get) Token: 0x06001BD5 RID: 7125 RVA: 0x00266328 File Offset: 0x00265728
		// (set) Token: 0x06001BD6 RID: 7126 RVA: 0x00266348 File Offset: 0x00265748
		[ResCategory("DataCategory_Data")]
		[ResDescription("DbDataParameter_Precision")]
		[DefaultValue(0)]
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

		// Token: 0x170003BE RID: 958
		// (get) Token: 0x06001BD7 RID: 7127 RVA: 0x00266368 File Offset: 0x00265768
		// (set) Token: 0x06001BD8 RID: 7128 RVA: 0x00266398 File Offset: 0x00265798
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

		// Token: 0x06001BD9 RID: 7129 RVA: 0x002663C8 File Offset: 0x002657C8
		private bool ShouldSerializePrecision()
		{
			return 0 != this._precision;
		}

		// Token: 0x170003BF RID: 959
		// (get) Token: 0x06001BDA RID: 7130 RVA: 0x002663E8 File Offset: 0x002657E8
		// (set) Token: 0x06001BDB RID: 7131 RVA: 0x00266408 File Offset: 0x00265808
		[DefaultValue(0)]
		[ResDescription("DbDataParameter_Scale")]
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

		// Token: 0x170003C0 RID: 960
		// (get) Token: 0x06001BDC RID: 7132 RVA: 0x00266428 File Offset: 0x00265828
		// (set) Token: 0x06001BDD RID: 7133 RVA: 0x00266458 File Offset: 0x00265858
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

		// Token: 0x06001BDE RID: 7134 RVA: 0x00266498 File Offset: 0x00265898
		private bool ShouldSerializeScale()
		{
			return this.ShouldSerializeScale(this._scale);
		}

		// Token: 0x06001BDF RID: 7135 RVA: 0x002664B8 File Offset: 0x002658B8
		private bool ShouldSerializeScale(byte scale)
		{
			return this._hasScale && (scale != 0 || this.ShouldSerializePrecision());
		}

		// Token: 0x06001BE0 RID: 7136 RVA: 0x002664E8 File Offset: 0x002658E8
		private int GetColumnSize(object value, int offset, int ordinal)
		{
			if (ODBC32.SQL_C.NUMERIC == this._bindtype._sql_c && this._internalPrecision != 0)
			{
				return Math.Min((int)this._internalPrecision, 29);
			}
			int num = this._bindtype._columnSize;
			if (0 >= num)
			{
				if (ODBC32.SQL_C.NUMERIC == this._typemap._sql_c)
				{
					num = 62;
				}
				else
				{
					num = this._internalSize;
					if (!this._internalShouldSerializeSize || 1073741823 <= num || num < 0)
					{
						if (!this._internalShouldSerializeSize && (ParameterDirection.Output & this._internalDirection) != (ParameterDirection)0)
						{
							throw ADP.UninitializedParameterSize(ordinal, this._bindtype._type);
						}
						if (value == null || Convert.IsDBNull(value))
						{
							num = 0;
						}
						else if (value is string)
						{
							num = ((string)value).Length - offset;
							if ((ParameterDirection.Output & this._internalDirection) != (ParameterDirection)0 && 1073741823 <= this._internalSize)
							{
								num = Math.Max(num, 4096);
							}
							if (ODBC32.SQL_TYPE.CHAR == this._bindtype._sql_type || ODBC32.SQL_TYPE.VARCHAR == this._bindtype._sql_type || ODBC32.SQL_TYPE.LONGVARCHAR == this._bindtype._sql_type)
							{
								num = Encoding.Default.GetMaxByteCount(num);
							}
						}
						else if (value is char[])
						{
							num = ((char[])value).Length - offset;
							if ((ParameterDirection.Output & this._internalDirection) != (ParameterDirection)0 && 1073741823 <= this._internalSize)
							{
								num = Math.Max(num, 4096);
							}
							if (ODBC32.SQL_TYPE.CHAR == this._bindtype._sql_type || ODBC32.SQL_TYPE.VARCHAR == this._bindtype._sql_type || ODBC32.SQL_TYPE.LONGVARCHAR == this._bindtype._sql_type)
							{
								num = Encoding.Default.GetMaxByteCount(num);
							}
						}
						else if (value is byte[])
						{
							num = ((byte[])value).Length - offset;
							if ((ParameterDirection.Output & this._internalDirection) != (ParameterDirection)0 && 1073741823 <= this._internalSize)
							{
								num = Math.Max(num, 8192);
							}
						}
						num = Math.Max(2, num);
					}
				}
			}
			return num;
		}

		// Token: 0x06001BE1 RID: 7137 RVA: 0x002666C8 File Offset: 0x00265AC8
		private int GetValueSize(object value, int offset)
		{
			if (ODBC32.SQL_C.NUMERIC == this._bindtype._sql_c && this._internalPrecision != 0)
			{
				return Math.Min((int)this._internalPrecision, 29);
			}
			int num = this._bindtype._columnSize;
			if (0 >= num)
			{
				bool flag = false;
				if (value is string)
				{
					num = ((string)value).Length - offset;
					flag = true;
				}
				else if (value is char[])
				{
					num = ((char[])value).Length - offset;
					flag = true;
				}
				else if (value is byte[])
				{
					num = ((byte[])value).Length - offset;
				}
				else
				{
					num = 0;
				}
				if (this._internalShouldSerializeSize && this._internalSize >= 0 && this._internalSize < num && this._bindtype == this._originalbindtype)
				{
					num = this._internalSize;
				}
				if (flag)
				{
					num *= 2;
				}
			}
			return num;
		}

		// Token: 0x06001BE2 RID: 7138 RVA: 0x00266798 File Offset: 0x00265B98
		private int GetParameterSize(object value, int offset, int ordinal)
		{
			int num = this._bindtype._bufferSize;
			if (0 >= num)
			{
				if (ODBC32.SQL_C.NUMERIC == this._typemap._sql_c)
				{
					num = 518;
				}
				else
				{
					num = this._internalSize;
					if (!this._internalShouldSerializeSize || 1073741823 <= num || num < 0)
					{
						if (num <= 0 && (ParameterDirection.Output & this._internalDirection) != (ParameterDirection)0)
						{
							throw ADP.UninitializedParameterSize(ordinal, this._bindtype._type);
						}
						if (value == null || Convert.IsDBNull(value))
						{
							if (this._bindtype._sql_c == ODBC32.SQL_C.WCHAR)
							{
								num = 2;
							}
							else
							{
								num = 0;
							}
						}
						else if (value is string)
						{
							num = (((string)value).Length - offset) * 2 + 2;
						}
						else if (value is char[])
						{
							num = (((char[])value).Length - offset) * 2 + 2;
						}
						else if (value is byte[])
						{
							num = ((byte[])value).Length - offset;
						}
						if ((ParameterDirection.Output & this._internalDirection) != (ParameterDirection)0 && 1073741823 <= this._internalSize)
						{
							num = Math.Max(num, 8192);
						}
					}
					else if (ODBC32.SQL_C.WCHAR == this._bindtype._sql_c)
					{
						if (value is string && num < ((string)value).Length && this._bindtype == this._originalbindtype)
						{
							num = ((string)value).Length;
						}
						num = num * 2 + 2;
					}
					else if (value is byte[] && num < ((byte[])value).Length && this._bindtype == this._originalbindtype)
					{
						num = ((byte[])value).Length;
					}
				}
			}
			return num;
		}

		// Token: 0x06001BE3 RID: 7139 RVA: 0x00266918 File Offset: 0x00265D18
		private byte GetParameterPrecision(object value)
		{
			if (this._internalPrecision != 0 && value is decimal)
			{
				if (this._internalPrecision < 29)
				{
					if (this._internalPrecision != 0)
					{
						byte precision = ((decimal)value).Precision;
						this._internalPrecision = Math.Max(this._internalPrecision, precision);
					}
					return this._internalPrecision;
				}
				return 29;
			}
			else
			{
				if (value == null || value is decimal || Convert.IsDBNull(value))
				{
					return 28;
				}
				return 0;
			}
		}

		// Token: 0x06001BE4 RID: 7140 RVA: 0x00266998 File Offset: 0x00265D98
		private byte GetParameterScale(object value)
		{
			if (!(value is decimal))
			{
				return this._internalScale;
			}
			byte b = (byte)((decimal.GetBits((decimal)value)[3] & 16711680) >> 16);
			if (this._internalScale > 0 && this._internalScale < b)
			{
				return this._internalScale;
			}
			return b;
		}

		// Token: 0x06001BE5 RID: 7141 RVA: 0x002669E8 File Offset: 0x00265DE8
		object ICloneable.Clone()
		{
			return new OdbcParameter(this);
		}

		// Token: 0x06001BE6 RID: 7142 RVA: 0x00266A08 File Offset: 0x00265E08
		private void CopyParameterInternal()
		{
			this._internalValue = this.Value;
			this._internalPrecision = (this.ShouldSerializePrecision() ? this.PrecisionInternal : this.ValuePrecision(this._internalValue));
			this._internalShouldSerializeSize = this.ShouldSerializeSize();
			this._internalSize = (this._internalShouldSerializeSize ? this.Size : this.ValueSize(this._internalValue));
			this._internalDirection = this.Direction;
			this._internalScale = (this.ShouldSerializeScale() ? this.ScaleInternal : this.ValueScale(this._internalValue));
			this._internalOffset = this.Offset;
			this._internalUserSpecifiedType = this.UserSpecifiedType;
		}

		// Token: 0x06001BE7 RID: 7143 RVA: 0x00266AB8 File Offset: 0x00265EB8
		private void CloneHelper(OdbcParameter destination)
		{
			this.CloneHelperCore(destination);
			destination._userSpecifiedType = this._userSpecifiedType;
			destination._typemap = this._typemap;
			destination._parameterName = this._parameterName;
			destination._precision = this._precision;
			destination._scale = this._scale;
			destination._hasScale = this._hasScale;
		}

		// Token: 0x06001BE8 RID: 7144 RVA: 0x00266B18 File Offset: 0x00265F18
		internal void ClearBinding()
		{
			if (!this._userSpecifiedType)
			{
				this._typemap = null;
			}
			this._bindtype = null;
		}

		// Token: 0x06001BE9 RID: 7145 RVA: 0x00266B48 File Offset: 0x00265F48
		internal void PrepareForBind(OdbcCommand command, short ordinal, ref int parameterBufferSize)
		{
			this.CopyParameterInternal();
			object obj = this.ProcessAndGetParameterValue();
			int num = this._internalOffset;
			int preparedSize = this._internalSize;
			if (num > 0)
			{
				if (obj is string)
				{
					if (num > ((string)obj).Length)
					{
						throw ADP.OffsetOutOfRangeException();
					}
				}
				else if (obj is char[])
				{
					if (num > ((char[])obj).Length)
					{
						throw ADP.OffsetOutOfRangeException();
					}
				}
				else if (obj is byte[])
				{
					if (num > ((byte[])obj).Length)
					{
						throw ADP.OffsetOutOfRangeException();
					}
				}
				else
				{
					num = 0;
				}
			}
			ODBC32.SQL_TYPE sql_type = this._bindtype._sql_type;
			switch (sql_type)
			{
			case ODBC32.SQL_TYPE.WLONGVARCHAR:
			case ODBC32.SQL_TYPE.WVARCHAR:
			case ODBC32.SQL_TYPE.WCHAR:
				if (obj is char)
				{
					obj = obj.ToString();
					preparedSize = ((string)obj).Length;
					num = 0;
				}
				if (!command.Connection.TestTypeSupport(this._bindtype._sql_type))
				{
					if (ODBC32.SQL_TYPE.WCHAR == this._bindtype._sql_type)
					{
						this._bindtype = TypeMap._Char;
					}
					else if (ODBC32.SQL_TYPE.WVARCHAR == this._bindtype._sql_type)
					{
						this._bindtype = TypeMap._VarChar;
					}
					else if (ODBC32.SQL_TYPE.WLONGVARCHAR == this._bindtype._sql_type)
					{
						this._bindtype = TypeMap._Text;
					}
				}
				break;
			case ODBC32.SQL_TYPE.BIT:
			case ODBC32.SQL_TYPE.TINYINT:
				break;
			case ODBC32.SQL_TYPE.BIGINT:
				if (!command.Connection.IsV3Driver)
				{
					this._bindtype = TypeMap._VarChar;
					if (obj != null && !Convert.IsDBNull(obj))
					{
						obj = ((long)obj).ToString(CultureInfo.CurrentCulture);
						preparedSize = ((string)obj).Length;
						num = 0;
					}
				}
				break;
			default:
				switch (sql_type)
				{
				case ODBC32.SQL_TYPE.NUMERIC:
				case ODBC32.SQL_TYPE.DECIMAL:
					if (!command.Connection.IsV3Driver || !command.Connection.TestTypeSupport(ODBC32.SQL_TYPE.NUMERIC) || command.Connection.TestRestrictedSqlBindType(this._bindtype._sql_type))
					{
						this._bindtype = TypeMap._VarChar;
						if (obj != null && !Convert.IsDBNull(obj))
						{
							obj = ((decimal)obj).ToString(CultureInfo.CurrentCulture);
							preparedSize = ((string)obj).Length;
							num = 0;
						}
					}
					break;
				}
				break;
			}
			ODBC32.SQL_C sql_C = this._bindtype._sql_c;
			if (!command.Connection.IsV3Driver && sql_C == ODBC32.SQL_C.WCHAR)
			{
				sql_C = ODBC32.SQL_C.CHAR;
				if (obj != null && !Convert.IsDBNull(obj) && obj is string)
				{
					int lcid = CultureInfo.CurrentCulture.LCID;
					CultureInfo cultureInfo = new CultureInfo(lcid);
					Encoding encoding = Encoding.GetEncoding(cultureInfo.TextInfo.ANSICodePage);
					obj = encoding.GetBytes(obj.ToString());
					preparedSize = ((byte[])obj).Length;
				}
			}
			int parameterSize = this.GetParameterSize(obj, num, (int)ordinal);
			ODBC32.SQL_TYPE sql_type2 = this._bindtype._sql_type;
			if (sql_type2 != ODBC32.SQL_TYPE.WVARCHAR)
			{
				if (sql_type2 != ODBC32.SQL_TYPE.VARBINARY)
				{
					if (sql_type2 == ODBC32.SQL_TYPE.VARCHAR)
					{
						if (parameterSize > 8000)
						{
							this._bindtype = TypeMap._Text;
						}
					}
				}
				else if (parameterSize > 8000)
				{
					this._bindtype = TypeMap._Image;
				}
			}
			else if (parameterSize > 4000)
			{
				this._bindtype = TypeMap._NText;
			}
			this._prepared_Sql_C_Type = sql_C;
			this._preparedOffset = num;
			this._preparedSize = preparedSize;
			this._preparedValue = obj;
			this._preparedBufferSize = parameterSize;
			this._preparedIntOffset = parameterBufferSize;
			this._preparedValueOffset = this._preparedIntOffset + IntPtr.Size;
			parameterBufferSize += parameterSize + IntPtr.Size;
		}

		// Token: 0x06001BEA RID: 7146 RVA: 0x00266E88 File Offset: 0x00266288
		internal void Bind(OdbcStatementHandle hstmt, OdbcCommand command, short ordinal, CNativeBuffer parameterBuffer, bool allowReentrance)
		{
			ODBC32.SQL_C prepared_Sql_C_Type = this._prepared_Sql_C_Type;
			ODBC32.SQL_PARAM sql_PARAM = this.SqlDirectionFromParameterDirection();
			int preparedOffset = this._preparedOffset;
			int preparedSize = this._preparedSize;
			object obj = this._preparedValue;
			int valueSize = this.GetValueSize(obj, preparedOffset);
			int columnSize = this.GetColumnSize(obj, preparedOffset, (int)ordinal);
			byte parameterPrecision = this.GetParameterPrecision(obj);
			byte b = this.GetParameterScale(obj);
			HandleRef handleRef = parameterBuffer.PtrOffset(this._preparedValueOffset, this._preparedBufferSize);
			HandleRef intbuffer = parameterBuffer.PtrOffset(this._preparedIntOffset, IntPtr.Size);
			if (ODBC32.SQL_C.NUMERIC == prepared_Sql_C_Type)
			{
				if (ODBC32.SQL_PARAM.INPUT_OUTPUT == sql_PARAM && obj is decimal && b < this._internalScale)
				{
					while (b < this._internalScale)
					{
						obj = (decimal)obj * 10m;
						b += 1;
					}
				}
				this.SetInputValue(obj, prepared_Sql_C_Type, valueSize, (int)parameterPrecision, 0, parameterBuffer);
				if (ODBC32.SQL_PARAM.INPUT != sql_PARAM)
				{
					parameterBuffer.WriteInt16(this._preparedValueOffset, (short)((int)b << 8 | (int)parameterPrecision));
				}
			}
			else
			{
				this.SetInputValue(obj, prepared_Sql_C_Type, valueSize, preparedSize, preparedOffset, parameterBuffer);
			}
			if (!this._hasChanged && this._boundSqlCType == prepared_Sql_C_Type && this._boundParameterType == this._bindtype._sql_type && this._boundSize == columnSize && this._boundScale == (int)b && this._boundBuffer == handleRef.Handle && this._boundIntbuffer == intbuffer.Handle)
			{
				return;
			}
			ODBC32.RetCode retCode = hstmt.BindParameter(ordinal, (short)sql_PARAM, prepared_Sql_C_Type, this._bindtype._sql_type, (IntPtr)columnSize, (IntPtr)((int)b), handleRef, (IntPtr)this._preparedBufferSize, intbuffer);
			if (retCode != ODBC32.RetCode.SUCCESS)
			{
				if ("07006" == command.GetDiagSqlState())
				{
					Bid.Trace("<odbc.OdbcParameter.Bind|ERR> Call to BindParameter returned errorcode [07006]\n");
					command.Connection.FlagRestrictedSqlBindType(this._bindtype._sql_type);
					if (allowReentrance)
					{
						this.Bind(hstmt, command, ordinal, parameterBuffer, false);
						return;
					}
				}
				command.Connection.HandleError(hstmt, retCode);
			}
			this._hasChanged = false;
			this._boundSqlCType = prepared_Sql_C_Type;
			this._boundParameterType = this._bindtype._sql_type;
			this._boundSize = columnSize;
			this._boundScale = (int)b;
			this._boundBuffer = handleRef.Handle;
			this._boundIntbuffer = intbuffer.Handle;
			if (ODBC32.SQL_C.NUMERIC == prepared_Sql_C_Type)
			{
				OdbcDescriptorHandle descriptorHandle = command.GetDescriptorHandle(ODBC32.SQL_ATTR.APP_PARAM_DESC);
				retCode = descriptorHandle.SetDescriptionField1(ordinal, ODBC32.SQL_DESC.TYPE, (IntPtr)2L);
				if (retCode != ODBC32.RetCode.SUCCESS)
				{
					command.Connection.HandleError(hstmt, retCode);
				}
				int value = (int)parameterPrecision;
				retCode = descriptorHandle.SetDescriptionField1(ordinal, ODBC32.SQL_DESC.PRECISION, (IntPtr)value);
				if (retCode != ODBC32.RetCode.SUCCESS)
				{
					command.Connection.HandleError(hstmt, retCode);
				}
				value = (int)b;
				retCode = descriptorHandle.SetDescriptionField1(ordinal, ODBC32.SQL_DESC.SCALE, (IntPtr)value);
				if (retCode != ODBC32.RetCode.SUCCESS)
				{
					command.Connection.HandleError(hstmt, retCode);
				}
				retCode = descriptorHandle.SetDescriptionField2(ordinal, ODBC32.SQL_DESC.DATA_PTR, handleRef);
				if (retCode != ODBC32.RetCode.SUCCESS)
				{
					command.Connection.HandleError(hstmt, retCode);
				}
			}
		}

		// Token: 0x06001BEB RID: 7147 RVA: 0x00267168 File Offset: 0x00266568
		internal void GetOutputValue(CNativeBuffer parameterBuffer)
		{
			if (this._hasChanged)
			{
				return;
			}
			if (this._bindtype != null && this._internalDirection != ParameterDirection.Input)
			{
				TypeMap bindtype = this._bindtype;
				this._bindtype = null;
				int num = (int)parameterBuffer.ReadIntPtr(this._preparedIntOffset);
				if (-1 == num)
				{
					this.Value = DBNull.Value;
					return;
				}
				if (0 <= num || num == -3)
				{
					this.Value = parameterBuffer.MarshalToManaged(this._preparedValueOffset, this._boundSqlCType, num);
					if (this._boundSqlCType == ODBC32.SQL_C.CHAR && this.Value != null && !Convert.IsDBNull(this.Value))
					{
						int lcid = CultureInfo.CurrentCulture.LCID;
						CultureInfo cultureInfo = new CultureInfo(lcid);
						Encoding encoding = Encoding.GetEncoding(cultureInfo.TextInfo.ANSICodePage);
						this.Value = encoding.GetString((byte[])this.Value);
					}
					if (bindtype != this._typemap && this.Value != null && !Convert.IsDBNull(this.Value) && this.Value.GetType() != this._typemap._type)
					{
						this.Value = decimal.Parse((string)this.Value, CultureInfo.CurrentCulture);
					}
				}
			}
		}

		// Token: 0x06001BEC RID: 7148 RVA: 0x002672A8 File Offset: 0x002666A8
		private object ProcessAndGetParameterValue()
		{
			object obj = this._internalValue;
			if (this._internalUserSpecifiedType)
			{
				if (obj != null && !Convert.IsDBNull(obj))
				{
					Type type = obj.GetType();
					if (!type.IsArray)
					{
						if (type == this._typemap._type)
						{
							goto IL_C8;
						}
						try
						{
							obj = Convert.ChangeType(obj, this._typemap._type, null);
							goto IL_C8;
						}
						catch (Exception ex)
						{
							if (!ADP.IsCatchableExceptionType(ex))
							{
								throw;
							}
							throw ADP.ParameterConversionFailed(obj, this._typemap._type, ex);
						}
					}
					if (type == typeof(char[]))
					{
						obj = new string((char[])obj);
					}
				}
			}
			else if (this._typemap == null)
			{
				if (obj == null || Convert.IsDBNull(obj))
				{
					this._typemap = TypeMap._NVarChar;
				}
				else
				{
					Type type2 = obj.GetType();
					this._typemap = TypeMap.FromSystemType(type2);
				}
			}
			IL_C8:
			this._originalbindtype = (this._bindtype = this._typemap);
			return obj;
		}

		// Token: 0x06001BED RID: 7149 RVA: 0x002673B8 File Offset: 0x002667B8
		private void PropertyChanging()
		{
			this._hasChanged = true;
		}

		// Token: 0x06001BEE RID: 7150 RVA: 0x002673D8 File Offset: 0x002667D8
		private void PropertyTypeChanging()
		{
			this.PropertyChanging();
		}

		// Token: 0x06001BEF RID: 7151 RVA: 0x002673F8 File Offset: 0x002667F8
		internal void SetInputValue(object value, ODBC32.SQL_C sql_c_type, int cbsize, int sizeorprecision, int offset, CNativeBuffer parameterBuffer)
		{
			if (ParameterDirection.Input != this._internalDirection && ParameterDirection.InputOutput != this._internalDirection)
			{
				this._internalValue = null;
				parameterBuffer.WriteIntPtr(this._preparedIntOffset, (IntPtr)(-1));
				return;
			}
			if (value == null)
			{
				parameterBuffer.WriteIntPtr(this._preparedIntOffset, (IntPtr)(-5));
				return;
			}
			if (Convert.IsDBNull(value))
			{
				parameterBuffer.WriteIntPtr(this._preparedIntOffset, (IntPtr)(-1));
				return;
			}
			if (sql_c_type == ODBC32.SQL_C.WCHAR || sql_c_type == ODBC32.SQL_C.BINARY || sql_c_type == ODBC32.SQL_C.CHAR)
			{
				parameterBuffer.WriteIntPtr(this._preparedIntOffset, (IntPtr)cbsize);
			}
			else
			{
				parameterBuffer.WriteIntPtr(this._preparedIntOffset, IntPtr.Zero);
			}
			parameterBuffer.MarshalToNative(this._preparedValueOffset, value, sql_c_type, sizeorprecision, offset);
		}

		// Token: 0x06001BF0 RID: 7152 RVA: 0x002674B8 File Offset: 0x002668B8
		private ODBC32.SQL_PARAM SqlDirectionFromParameterDirection()
		{
			switch (this._internalDirection)
			{
			case ParameterDirection.Input:
				return ODBC32.SQL_PARAM.INPUT;
			case ParameterDirection.Output:
			case ParameterDirection.ReturnValue:
				return ODBC32.SQL_PARAM.OUTPUT;
			case ParameterDirection.InputOutput:
				return ODBC32.SQL_PARAM.INPUT_OUTPUT;
			}
			return ODBC32.SQL_PARAM.INPUT;
		}

		// Token: 0x170003C1 RID: 961
		// (get) Token: 0x06001BF1 RID: 7153 RVA: 0x002674F8 File Offset: 0x002668F8
		// (set) Token: 0x06001BF2 RID: 7154 RVA: 0x00267518 File Offset: 0x00266918
		[ResDescription("DbParameter_Value")]
		[ResCategory("DataCategory_Data")]
		[TypeConverter(typeof(StringConverter))]
		[RefreshProperties(RefreshProperties.All)]
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

		// Token: 0x06001BF3 RID: 7155 RVA: 0x00267538 File Offset: 0x00266938
		private byte ValuePrecision(object value)
		{
			return this.ValuePrecisionCore(value);
		}

		// Token: 0x06001BF4 RID: 7156 RVA: 0x00267558 File Offset: 0x00266958
		private byte ValueScale(object value)
		{
			return this.ValueScaleCore(value);
		}

		// Token: 0x06001BF5 RID: 7157 RVA: 0x00267578 File Offset: 0x00266978
		private int ValueSize(object value)
		{
			return this.ValueSizeCore(value);
		}

		// Token: 0x06001BF6 RID: 7158 RVA: 0x00267598 File Offset: 0x00266998
		private OdbcParameter(OdbcParameter source) : this()
		{
			ADP.CheckArgumentNull(source, "source");
			source.CloneHelper(this);
			ICloneable cloneable = this._value as ICloneable;
			if (cloneable != null)
			{
				this._value = cloneable.Clone();
			}
		}

		// Token: 0x170003C2 RID: 962
		// (get) Token: 0x06001BF7 RID: 7159 RVA: 0x002675D8 File Offset: 0x002669D8
		// (set) Token: 0x06001BF8 RID: 7160 RVA: 0x002675F8 File Offset: 0x002669F8
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

		// Token: 0x170003C3 RID: 963
		// (get) Token: 0x06001BF9 RID: 7161 RVA: 0x00267618 File Offset: 0x00266A18
		// (set) Token: 0x06001BFA RID: 7162 RVA: 0x00267638 File Offset: 0x00266A38
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

		// Token: 0x170003C4 RID: 964
		// (get) Token: 0x06001BFB RID: 7163 RVA: 0x00267688 File Offset: 0x00266A88
		// (set) Token: 0x06001BFC RID: 7164 RVA: 0x002676A8 File Offset: 0x00266AA8
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

		// Token: 0x170003C5 RID: 965
		// (get) Token: 0x06001BFD RID: 7165 RVA: 0x002676C8 File Offset: 0x00266AC8
		internal int Offset
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170003C6 RID: 966
		// (get) Token: 0x06001BFE RID: 7166 RVA: 0x002676D8 File Offset: 0x00266AD8
		// (set) Token: 0x06001BFF RID: 7167 RVA: 0x00267708 File Offset: 0x00266B08
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

		// Token: 0x06001C00 RID: 7168 RVA: 0x00267738 File Offset: 0x00266B38
		private void ResetSize()
		{
			if (this._size != 0)
			{
				this.PropertyChanging();
				this._size = 0;
			}
		}

		// Token: 0x06001C01 RID: 7169 RVA: 0x00267768 File Offset: 0x00266B68
		private bool ShouldSerializeSize()
		{
			return 0 != this._size;
		}

		// Token: 0x170003C7 RID: 967
		// (get) Token: 0x06001C02 RID: 7170 RVA: 0x00267788 File Offset: 0x00266B88
		// (set) Token: 0x06001C03 RID: 7171 RVA: 0x002677A8 File Offset: 0x00266BA8
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

		// Token: 0x170003C8 RID: 968
		// (get) Token: 0x06001C04 RID: 7172 RVA: 0x002677C8 File Offset: 0x00266BC8
		// (set) Token: 0x06001C05 RID: 7173 RVA: 0x002677E8 File Offset: 0x00266BE8
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

		// Token: 0x170003C9 RID: 969
		// (get) Token: 0x06001C06 RID: 7174 RVA: 0x00267808 File Offset: 0x00266C08
		// (set) Token: 0x06001C07 RID: 7175 RVA: 0x00267828 File Offset: 0x00266C28
		[ResDescription("DbParameter_SourceVersion")]
		[ResCategory("DataCategory_Update")]
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

		// Token: 0x06001C08 RID: 7176 RVA: 0x00267878 File Offset: 0x00266C78
		private void CloneHelperCore(OdbcParameter destination)
		{
			destination._value = this._value;
			destination._direction = this._direction;
			destination._size = this._size;
			destination._sourceColumn = this._sourceColumn;
			destination._sourceVersion = this._sourceVersion;
			destination._sourceColumnNullMapping = this._sourceColumnNullMapping;
			destination._isNullable = this._isNullable;
		}

		// Token: 0x06001C09 RID: 7177 RVA: 0x002678E8 File Offset: 0x00266CE8
		internal void CopyTo(DbParameter destination)
		{
			ADP.CheckArgumentNull(destination, "destination");
			this.CloneHelper((OdbcParameter)destination);
		}

		// Token: 0x06001C0A RID: 7178 RVA: 0x00267918 File Offset: 0x00266D18
		internal object CompareExchangeParent(object value, object comparand)
		{
			object parent = this._parent;
			if (comparand == parent)
			{
				this._parent = value;
			}
			return parent;
		}

		// Token: 0x06001C0B RID: 7179 RVA: 0x00267938 File Offset: 0x00266D38
		internal void ResetParent()
		{
			this._parent = null;
		}

		// Token: 0x06001C0C RID: 7180 RVA: 0x00267958 File Offset: 0x00266D58
		public override string ToString()
		{
			return this.ParameterName;
		}

		// Token: 0x06001C0D RID: 7181 RVA: 0x00267978 File Offset: 0x00266D78
		private byte ValuePrecisionCore(object value)
		{
			if (value is decimal)
			{
				return ((decimal)value).Precision;
			}
			return 0;
		}

		// Token: 0x06001C0E RID: 7182 RVA: 0x002679A8 File Offset: 0x00266DA8
		private byte ValueScaleCore(object value)
		{
			if (value is decimal)
			{
				return (byte)((decimal.GetBits((decimal)value)[3] & 16711680) >> 16);
			}
			return 0;
		}

		// Token: 0x06001C0F RID: 7183 RVA: 0x002679D8 File Offset: 0x00266DD8
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

		// Token: 0x04001043 RID: 4163
		private bool _hasChanged;

		// Token: 0x04001044 RID: 4164
		private bool _userSpecifiedType;

		// Token: 0x04001045 RID: 4165
		private TypeMap _typemap;

		// Token: 0x04001046 RID: 4166
		private TypeMap _bindtype;

		// Token: 0x04001047 RID: 4167
		private string _parameterName;

		// Token: 0x04001048 RID: 4168
		private byte _precision;

		// Token: 0x04001049 RID: 4169
		private byte _scale;

		// Token: 0x0400104A RID: 4170
		private bool _hasScale;

		// Token: 0x0400104B RID: 4171
		private ODBC32.SQL_C _boundSqlCType;

		// Token: 0x0400104C RID: 4172
		private ODBC32.SQL_TYPE _boundParameterType;

		// Token: 0x0400104D RID: 4173
		private int _boundSize;

		// Token: 0x0400104E RID: 4174
		private int _boundScale;

		// Token: 0x0400104F RID: 4175
		private IntPtr _boundBuffer;

		// Token: 0x04001050 RID: 4176
		private IntPtr _boundIntbuffer;

		// Token: 0x04001051 RID: 4177
		private TypeMap _originalbindtype;

		// Token: 0x04001052 RID: 4178
		private byte _internalPrecision;

		// Token: 0x04001053 RID: 4179
		private bool _internalShouldSerializeSize;

		// Token: 0x04001054 RID: 4180
		private int _internalSize;

		// Token: 0x04001055 RID: 4181
		private ParameterDirection _internalDirection;

		// Token: 0x04001056 RID: 4182
		private byte _internalScale;

		// Token: 0x04001057 RID: 4183
		private int _internalOffset;

		// Token: 0x04001058 RID: 4184
		internal bool _internalUserSpecifiedType;

		// Token: 0x04001059 RID: 4185
		private object _internalValue;

		// Token: 0x0400105A RID: 4186
		private int _preparedOffset;

		// Token: 0x0400105B RID: 4187
		private int _preparedSize;

		// Token: 0x0400105C RID: 4188
		private int _preparedBufferSize;

		// Token: 0x0400105D RID: 4189
		private object _preparedValue;

		// Token: 0x0400105E RID: 4190
		private int _preparedIntOffset;

		// Token: 0x0400105F RID: 4191
		private int _preparedValueOffset;

		// Token: 0x04001060 RID: 4192
		private ODBC32.SQL_C _prepared_Sql_C_Type;

		// Token: 0x04001061 RID: 4193
		private object _value;

		// Token: 0x04001062 RID: 4194
		private object _parent;

		// Token: 0x04001063 RID: 4195
		private ParameterDirection _direction;

		// Token: 0x04001064 RID: 4196
		private int _size;

		// Token: 0x04001065 RID: 4197
		private string _sourceColumn;

		// Token: 0x04001066 RID: 4198
		private DataRowVersion _sourceVersion;

		// Token: 0x04001067 RID: 4199
		private bool _sourceColumnNullMapping;

		// Token: 0x04001068 RID: 4200
		private bool _isNullable;

		// Token: 0x04001069 RID: 4201
		private object _coercedValue;

		// Token: 0x020001F8 RID: 504
		internal sealed class OdbcParameterConverter : ExpandableObjectConverter
		{
			// Token: 0x06001C11 RID: 7185 RVA: 0x00267A58 File Offset: 0x00266E58
			public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
			{
				return destinationType == typeof(InstanceDescriptor) || base.CanConvertTo(context, destinationType);
			}

			// Token: 0x06001C12 RID: 7186 RVA: 0x00267A88 File Offset: 0x00266E88
			public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
			{
				if (destinationType == null)
				{
					throw ADP.ArgumentNull("destinationType");
				}
				if (destinationType == typeof(InstanceDescriptor) && value is OdbcParameter)
				{
					OdbcParameter odbcParameter = (OdbcParameter)value;
					int num = 0;
					if (OdbcType.NChar != odbcParameter.OdbcType)
					{
						num |= 1;
					}
					if (odbcParameter.ShouldSerializeSize())
					{
						num |= 2;
					}
					if (!ADP.IsEmpty(odbcParameter.SourceColumn))
					{
						num |= 4;
					}
					if (odbcParameter.Value != null)
					{
						num |= 8;
					}
					if (ParameterDirection.Input != odbcParameter.Direction || odbcParameter.IsNullable || odbcParameter.ShouldSerializePrecision() || odbcParameter.ShouldSerializeScale() || DataRowVersion.Current != odbcParameter.SourceVersion)
					{
						num |= 16;
					}
					if (odbcParameter.SourceColumnNullMapping)
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
							typeof(OdbcType)
						};
						arguments = new object[]
						{
							odbcParameter.ParameterName,
							odbcParameter.OdbcType
						};
						break;
					case 2:
					case 3:
						types = new Type[]
						{
							typeof(string),
							typeof(OdbcType),
							typeof(int)
						};
						arguments = new object[]
						{
							odbcParameter.ParameterName,
							odbcParameter.OdbcType,
							odbcParameter.Size
						};
						break;
					case 4:
					case 5:
					case 6:
					case 7:
						types = new Type[]
						{
							typeof(string),
							typeof(OdbcType),
							typeof(int),
							typeof(string)
						};
						arguments = new object[]
						{
							odbcParameter.ParameterName,
							odbcParameter.OdbcType,
							odbcParameter.Size,
							odbcParameter.SourceColumn
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
							odbcParameter.ParameterName,
							odbcParameter.Value
						};
						break;
					default:
						if ((32 & num) == 0)
						{
							types = new Type[]
							{
								typeof(string),
								typeof(OdbcType),
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
								odbcParameter.ParameterName,
								odbcParameter.OdbcType,
								odbcParameter.Size,
								odbcParameter.Direction,
								odbcParameter.IsNullable,
								odbcParameter.PrecisionInternal,
								odbcParameter.ScaleInternal,
								odbcParameter.SourceColumn,
								odbcParameter.SourceVersion,
								odbcParameter.Value
							};
						}
						else
						{
							types = new Type[]
							{
								typeof(string),
								typeof(OdbcType),
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
								odbcParameter.ParameterName,
								odbcParameter.OdbcType,
								odbcParameter.Size,
								odbcParameter.Direction,
								odbcParameter.PrecisionInternal,
								odbcParameter.ScaleInternal,
								odbcParameter.SourceColumn,
								odbcParameter.SourceVersion,
								odbcParameter.SourceColumnNullMapping,
								odbcParameter.Value
							};
						}
						break;
					}
					ConstructorInfo constructor = typeof(OdbcParameter).GetConstructor(types);
					if (constructor != null)
					{
						return new InstanceDescriptor(constructor, arguments);
					}
				}
				return base.ConvertTo(context, culture, value, destinationType);
			}
		}
	}
}
