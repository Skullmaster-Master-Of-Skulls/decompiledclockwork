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
	// Token: 0x020002A6 RID: 678
	[TypeConverter(typeof(OdbcParameter.OdbcParameterConverter))]
	public sealed class OdbcParameter : DbParameter, ICloneable, IDbDataParameter, IDataParameter
	{
		// Token: 0x06002919 RID: 10521 RVA: 0x00112798 File Offset: 0x00111B98
		public OdbcParameter()
		{
		}

		// Token: 0x0600291A RID: 10522 RVA: 0x001127AC File Offset: 0x00111BAC
		public OdbcParameter(string name, object value) : this()
		{
			this.ParameterName = name;
			this.Value = value;
		}

		// Token: 0x0600291B RID: 10523 RVA: 0x001127D0 File Offset: 0x00111BD0
		public OdbcParameter(string name, OdbcType type) : this()
		{
			this.ParameterName = name;
			this.OdbcType = type;
		}

		// Token: 0x0600291C RID: 10524 RVA: 0x001127F4 File Offset: 0x00111BF4
		public OdbcParameter(string name, OdbcType type, int size) : this()
		{
			this.ParameterName = name;
			this.OdbcType = type;
			this.Size = size;
		}

		// Token: 0x0600291D RID: 10525 RVA: 0x0011281C File Offset: 0x00111C1C
		public OdbcParameter(string name, OdbcType type, int size, string sourcecolumn) : this()
		{
			this.ParameterName = name;
			this.OdbcType = type;
			this.Size = size;
			this.SourceColumn = sourcecolumn;
		}

		// Token: 0x0600291E RID: 10526 RVA: 0x0011284C File Offset: 0x00111C4C
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

		// Token: 0x0600291F RID: 10527 RVA: 0x001128AC File Offset: 0x00111CAC
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

		// Token: 0x170006B3 RID: 1715
		// (get) Token: 0x06002920 RID: 10528 RVA: 0x0011290C File Offset: 0x00111D0C
		// (set) Token: 0x06002921 RID: 10529 RVA: 0x00112938 File Offset: 0x00111D38
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

		// Token: 0x06002922 RID: 10530 RVA: 0x00112974 File Offset: 0x00111D74
		public override void ResetDbType()
		{
			this.ResetOdbcType();
		}

		// Token: 0x170006B4 RID: 1716
		// (get) Token: 0x06002923 RID: 10531 RVA: 0x00112988 File Offset: 0x00111D88
		// (set) Token: 0x06002924 RID: 10532 RVA: 0x001129B4 File Offset: 0x00111DB4
		[DefaultValue(OdbcType.NChar)]
		[ResCategory("DataCategory_Data")]
		[RefreshProperties(RefreshProperties.All)]
		[ResDescription("OdbcParameter_OdbcType")]
		[DbProviderSpecificTypeProperty(true)]
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

		// Token: 0x06002925 RID: 10533 RVA: 0x001129F0 File Offset: 0x00111DF0
		public void ResetOdbcType()
		{
			this.PropertyTypeChanging();
			this._typemap = null;
			this._userSpecifiedType = false;
		}

		// Token: 0x170006B5 RID: 1717
		// (set) Token: 0x06002926 RID: 10534 RVA: 0x00112A14 File Offset: 0x00111E14
		internal bool HasChanged
		{
			set
			{
				this._hasChanged = value;
			}
		}

		// Token: 0x170006B6 RID: 1718
		// (get) Token: 0x06002927 RID: 10535 RVA: 0x00112A28 File Offset: 0x00111E28
		internal bool UserSpecifiedType
		{
			get
			{
				return this._userSpecifiedType;
			}
		}

		// Token: 0x170006B7 RID: 1719
		// (get) Token: 0x06002928 RID: 10536 RVA: 0x00112A3C File Offset: 0x00111E3C
		// (set) Token: 0x06002929 RID: 10537 RVA: 0x00112A5C File Offset: 0x00111E5C
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

		// Token: 0x170006B8 RID: 1720
		// (get) Token: 0x0600292A RID: 10538 RVA: 0x00112A84 File Offset: 0x00111E84
		// (set) Token: 0x0600292B RID: 10539 RVA: 0x00112A98 File Offset: 0x00111E98
		[ResDescription("DbDataParameter_Precision")]
		[ResCategory("DataCategory_Data")]
		[DefaultValue(0)]
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

		// Token: 0x170006B9 RID: 1721
		// (get) Token: 0x0600292C RID: 10540 RVA: 0x00112AAC File Offset: 0x00111EAC
		// (set) Token: 0x0600292D RID: 10541 RVA: 0x00112AD4 File Offset: 0x00111ED4
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

		// Token: 0x0600292E RID: 10542 RVA: 0x00112AF8 File Offset: 0x00111EF8
		private bool ShouldSerializePrecision()
		{
			return this._precision > 0;
		}

		// Token: 0x170006BA RID: 1722
		// (get) Token: 0x0600292F RID: 10543 RVA: 0x00112B10 File Offset: 0x00111F10
		// (set) Token: 0x06002930 RID: 10544 RVA: 0x00112B24 File Offset: 0x00111F24
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

		// Token: 0x170006BB RID: 1723
		// (get) Token: 0x06002931 RID: 10545 RVA: 0x00112B38 File Offset: 0x00111F38
		// (set) Token: 0x06002932 RID: 10546 RVA: 0x00112B64 File Offset: 0x00111F64
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

		// Token: 0x06002933 RID: 10547 RVA: 0x00112B98 File Offset: 0x00111F98
		private bool ShouldSerializeScale()
		{
			return this.ShouldSerializeScale(this._scale);
		}

		// Token: 0x06002934 RID: 10548 RVA: 0x00112BB4 File Offset: 0x00111FB4
		private bool ShouldSerializeScale(byte scale)
		{
			return this._hasScale && (scale != 0 || this.ShouldSerializePrecision());
		}

		// Token: 0x06002935 RID: 10549 RVA: 0x00112BD8 File Offset: 0x00111FD8
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

		// Token: 0x06002936 RID: 10550 RVA: 0x00112DAC File Offset: 0x001121AC
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

		// Token: 0x06002937 RID: 10551 RVA: 0x00112E74 File Offset: 0x00112274
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

		// Token: 0x06002938 RID: 10552 RVA: 0x00112FF4 File Offset: 0x001123F4
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

		// Token: 0x06002939 RID: 10553 RVA: 0x0011306C File Offset: 0x0011246C
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

		// Token: 0x0600293A RID: 10554 RVA: 0x001130BC File Offset: 0x001124BC
		object ICloneable.Clone()
		{
			return new OdbcParameter(this);
		}

		// Token: 0x0600293B RID: 10555 RVA: 0x001130D0 File Offset: 0x001124D0
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

		// Token: 0x0600293C RID: 10556 RVA: 0x00113180 File Offset: 0x00112580
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

		// Token: 0x0600293D RID: 10557 RVA: 0x001131DC File Offset: 0x001125DC
		internal void ClearBinding()
		{
			if (!this._userSpecifiedType)
			{
				this._typemap = null;
			}
			this._bindtype = null;
		}

		// Token: 0x0600293E RID: 10558 RVA: 0x00113200 File Offset: 0x00112600
		internal void PrepareForBind(OdbcCommand command, short ordinal, ref int parameterBufferSize)
		{
			this.CopyParameterInternal();
			object obj = this.ProcessAndGetParameterValue();
			int num = this._internalOffset;
			int num2 = this._internalSize;
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
			if (sql_type - ODBC32.SQL_TYPE.WLONGVARCHAR > 2)
			{
				if (sql_type != ODBC32.SQL_TYPE.BIGINT)
				{
					if (sql_type - ODBC32.SQL_TYPE.NUMERIC <= 1 && (!command.Connection.IsV3Driver || !command.Connection.TestTypeSupport(ODBC32.SQL_TYPE.NUMERIC) || command.Connection.TestRestrictedSqlBindType(this._bindtype._sql_type)))
					{
						this._bindtype = TypeMap._VarChar;
						if (obj != null && !Convert.IsDBNull(obj))
						{
							obj = ((decimal)obj).ToString(CultureInfo.CurrentCulture);
							num2 = ((string)obj).Length;
							num = 0;
						}
					}
				}
				else if (!command.Connection.IsV3Driver)
				{
					this._bindtype = TypeMap._VarChar;
					if (obj != null && !Convert.IsDBNull(obj))
					{
						obj = ((long)obj).ToString(CultureInfo.CurrentCulture);
						num2 = ((string)obj).Length;
						num = 0;
					}
				}
			}
			else
			{
				if (obj is char)
				{
					obj = obj.ToString();
					num2 = ((string)obj).Length;
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
					num2 = ((byte[])obj).Length;
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
						if (num2 > 8000)
						{
							this._bindtype = TypeMap._Text;
						}
					}
				}
				else if (num2 > 8000)
				{
					this._bindtype = TypeMap._Image;
				}
			}
			else if (num2 > 4000)
			{
				this._bindtype = TypeMap._NText;
			}
			this._prepared_Sql_C_Type = sql_C;
			this._preparedOffset = num;
			this._preparedSize = num2;
			this._preparedValue = obj;
			this._preparedBufferSize = parameterSize;
			this._preparedIntOffset = parameterBufferSize;
			this._preparedValueOffset = this._preparedIntOffset + IntPtr.Size;
			parameterBufferSize += parameterSize + IntPtr.Size;
		}

		// Token: 0x0600293F RID: 10559 RVA: 0x00113524 File Offset: 0x00112924
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
				retCode = descriptorHandle.SetDescriptionField1(ordinal, ODBC32.SQL_DESC.TYPE, (IntPtr)2);
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

		// Token: 0x06002940 RID: 10560 RVA: 0x001137FC File Offset: 0x00112BFC
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

		// Token: 0x06002941 RID: 10561 RVA: 0x00113934 File Offset: 0x00112D34
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
						if (!(type != this._typemap._type))
						{
							goto IL_D5;
						}
						try
						{
							obj = Convert.ChangeType(obj, this._typemap._type, null);
							goto IL_D5;
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
			IL_D5:
			this._originalbindtype = (this._bindtype = this._typemap);
			return obj;
		}

		// Token: 0x06002942 RID: 10562 RVA: 0x00113A48 File Offset: 0x00112E48
		private void PropertyChanging()
		{
			this._hasChanged = true;
		}

		// Token: 0x06002943 RID: 10563 RVA: 0x00113A5C File Offset: 0x00112E5C
		private void PropertyTypeChanging()
		{
			this.PropertyChanging();
		}

		// Token: 0x06002944 RID: 10564 RVA: 0x00113A70 File Offset: 0x00112E70
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

		// Token: 0x06002945 RID: 10565 RVA: 0x00113B2C File Offset: 0x00112F2C
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

		// Token: 0x170006BC RID: 1724
		// (get) Token: 0x06002946 RID: 10566 RVA: 0x00113B6C File Offset: 0x00112F6C
		// (set) Token: 0x06002947 RID: 10567 RVA: 0x00113B80 File Offset: 0x00112F80
		[ResDescription("DbParameter_Value")]
		[RefreshProperties(RefreshProperties.All)]
		[ResCategory("DataCategory_Data")]
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

		// Token: 0x06002948 RID: 10568 RVA: 0x00113B9C File Offset: 0x00112F9C
		private byte ValuePrecision(object value)
		{
			return this.ValuePrecisionCore(value);
		}

		// Token: 0x06002949 RID: 10569 RVA: 0x00113BB0 File Offset: 0x00112FB0
		private byte ValueScale(object value)
		{
			return this.ValueScaleCore(value);
		}

		// Token: 0x0600294A RID: 10570 RVA: 0x00113BC4 File Offset: 0x00112FC4
		private int ValueSize(object value)
		{
			return this.ValueSizeCore(value);
		}

		// Token: 0x0600294B RID: 10571 RVA: 0x00113BD8 File Offset: 0x00112FD8
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

		// Token: 0x170006BD RID: 1725
		// (get) Token: 0x0600294C RID: 10572 RVA: 0x00113C18 File Offset: 0x00113018
		// (set) Token: 0x0600294D RID: 10573 RVA: 0x00113C2C File Offset: 0x0011302C
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

		// Token: 0x170006BE RID: 1726
		// (get) Token: 0x0600294E RID: 10574 RVA: 0x00113C40 File Offset: 0x00113040
		// (set) Token: 0x0600294F RID: 10575 RVA: 0x00113C5C File Offset: 0x0011305C
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

		// Token: 0x170006BF RID: 1727
		// (get) Token: 0x06002950 RID: 10576 RVA: 0x00113C94 File Offset: 0x00113094
		// (set) Token: 0x06002951 RID: 10577 RVA: 0x00113CA8 File Offset: 0x001130A8
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

		// Token: 0x170006C0 RID: 1728
		// (get) Token: 0x06002952 RID: 10578 RVA: 0x00113CBC File Offset: 0x001130BC
		internal int Offset
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170006C1 RID: 1729
		// (get) Token: 0x06002953 RID: 10579 RVA: 0x00113CCC File Offset: 0x001130CC
		// (set) Token: 0x06002954 RID: 10580 RVA: 0x00113CF4 File Offset: 0x001130F4
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

		// Token: 0x06002955 RID: 10581 RVA: 0x00113D24 File Offset: 0x00113124
		private void ResetSize()
		{
			if (this._size != 0)
			{
				this.PropertyChanging();
				this._size = 0;
			}
		}

		// Token: 0x06002956 RID: 10582 RVA: 0x00113D48 File Offset: 0x00113148
		private bool ShouldSerializeSize()
		{
			return this._size != 0;
		}

		// Token: 0x170006C2 RID: 1730
		// (get) Token: 0x06002957 RID: 10583 RVA: 0x00113D60 File Offset: 0x00113160
		// (set) Token: 0x06002958 RID: 10584 RVA: 0x00113D80 File Offset: 0x00113180
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

		// Token: 0x170006C3 RID: 1731
		// (get) Token: 0x06002959 RID: 10585 RVA: 0x00113D94 File Offset: 0x00113194
		// (set) Token: 0x0600295A RID: 10586 RVA: 0x00113DA8 File Offset: 0x001131A8
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

		// Token: 0x170006C4 RID: 1732
		// (get) Token: 0x0600295B RID: 10587 RVA: 0x00113DBC File Offset: 0x001131BC
		// (set) Token: 0x0600295C RID: 10588 RVA: 0x00113DDC File Offset: 0x001131DC
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

		// Token: 0x0600295D RID: 10589 RVA: 0x00113E24 File Offset: 0x00113224
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

		// Token: 0x0600295E RID: 10590 RVA: 0x00113E88 File Offset: 0x00113288
		internal void CopyTo(DbParameter destination)
		{
			ADP.CheckArgumentNull(destination, "destination");
			this.CloneHelper((OdbcParameter)destination);
		}

		// Token: 0x0600295F RID: 10591 RVA: 0x00113EAC File Offset: 0x001132AC
		internal object CompareExchangeParent(object value, object comparand)
		{
			object parent = this._parent;
			if (comparand == parent)
			{
				this._parent = value;
			}
			return parent;
		}

		// Token: 0x06002960 RID: 10592 RVA: 0x00113ECC File Offset: 0x001132CC
		internal void ResetParent()
		{
			this._parent = null;
		}

		// Token: 0x06002961 RID: 10593 RVA: 0x00113EE0 File Offset: 0x001132E0
		public override string ToString()
		{
			return this.ParameterName;
		}

		// Token: 0x06002962 RID: 10594 RVA: 0x00113EF4 File Offset: 0x001132F4
		private byte ValuePrecisionCore(object value)
		{
			if (value is decimal)
			{
				return ((decimal)value).Precision;
			}
			return 0;
		}

		// Token: 0x06002963 RID: 10595 RVA: 0x00113F20 File Offset: 0x00113320
		private byte ValueScaleCore(object value)
		{
			if (value is decimal)
			{
				return (byte)((decimal.GetBits((decimal)value)[3] & 16711680) >> 16);
			}
			return 0;
		}

		// Token: 0x06002964 RID: 10596 RVA: 0x00113F50 File Offset: 0x00113350
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

		// Token: 0x04001ABD RID: 6845
		private bool _hasChanged;

		// Token: 0x04001ABE RID: 6846
		private bool _userSpecifiedType;

		// Token: 0x04001ABF RID: 6847
		private TypeMap _typemap;

		// Token: 0x04001AC0 RID: 6848
		private TypeMap _bindtype;

		// Token: 0x04001AC1 RID: 6849
		private string _parameterName;

		// Token: 0x04001AC2 RID: 6850
		private byte _precision;

		// Token: 0x04001AC3 RID: 6851
		private byte _scale;

		// Token: 0x04001AC4 RID: 6852
		private bool _hasScale;

		// Token: 0x04001AC5 RID: 6853
		private ODBC32.SQL_C _boundSqlCType;

		// Token: 0x04001AC6 RID: 6854
		private ODBC32.SQL_TYPE _boundParameterType;

		// Token: 0x04001AC7 RID: 6855
		private int _boundSize;

		// Token: 0x04001AC8 RID: 6856
		private int _boundScale;

		// Token: 0x04001AC9 RID: 6857
		private IntPtr _boundBuffer;

		// Token: 0x04001ACA RID: 6858
		private IntPtr _boundIntbuffer;

		// Token: 0x04001ACB RID: 6859
		private TypeMap _originalbindtype;

		// Token: 0x04001ACC RID: 6860
		private byte _internalPrecision;

		// Token: 0x04001ACD RID: 6861
		private bool _internalShouldSerializeSize;

		// Token: 0x04001ACE RID: 6862
		private int _internalSize;

		// Token: 0x04001ACF RID: 6863
		private ParameterDirection _internalDirection;

		// Token: 0x04001AD0 RID: 6864
		private byte _internalScale;

		// Token: 0x04001AD1 RID: 6865
		private int _internalOffset;

		// Token: 0x04001AD2 RID: 6866
		internal bool _internalUserSpecifiedType;

		// Token: 0x04001AD3 RID: 6867
		private object _internalValue;

		// Token: 0x04001AD4 RID: 6868
		private int _preparedOffset;

		// Token: 0x04001AD5 RID: 6869
		private int _preparedSize;

		// Token: 0x04001AD6 RID: 6870
		private int _preparedBufferSize;

		// Token: 0x04001AD7 RID: 6871
		private object _preparedValue;

		// Token: 0x04001AD8 RID: 6872
		private int _preparedIntOffset;

		// Token: 0x04001AD9 RID: 6873
		private int _preparedValueOffset;

		// Token: 0x04001ADA RID: 6874
		private ODBC32.SQL_C _prepared_Sql_C_Type;

		// Token: 0x04001ADB RID: 6875
		private object _value;

		// Token: 0x04001ADC RID: 6876
		private object _parent;

		// Token: 0x04001ADD RID: 6877
		private ParameterDirection _direction;

		// Token: 0x04001ADE RID: 6878
		private int _size;

		// Token: 0x04001ADF RID: 6879
		private string _sourceColumn;

		// Token: 0x04001AE0 RID: 6880
		private DataRowVersion _sourceVersion;

		// Token: 0x04001AE1 RID: 6881
		private bool _sourceColumnNullMapping;

		// Token: 0x04001AE2 RID: 6882
		private bool _isNullable;

		// Token: 0x04001AE3 RID: 6883
		private object _coercedValue;

		// Token: 0x02000424 RID: 1060
		internal sealed class OdbcParameterConverter : ExpandableObjectConverter
		{
			// Token: 0x060035F6 RID: 13814 RVA: 0x00147D30 File Offset: 0x00147130
			public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
			{
				return destinationType == typeof(InstanceDescriptor) || base.CanConvertTo(context, destinationType);
			}

			// Token: 0x060035F7 RID: 13815 RVA: 0x00147D5C File Offset: 0x0014715C
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
					if (null != constructor)
					{
						return new InstanceDescriptor(constructor, arguments);
					}
				}
				return base.ConvertTo(context, culture, value, destinationType);
			}
		}
	}
}
