using System;
using System.Data.SqlTypes;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Data.SqlClient
{
	// Token: 0x020001A4 RID: 420
	internal sealed class SqlBuffer
	{
		// Token: 0x06001856 RID: 6230 RVA: 0x000AC328 File Offset: 0x000AB728
		internal SqlBuffer()
		{
		}

		// Token: 0x06001857 RID: 6231 RVA: 0x000AC33C File Offset: 0x000AB73C
		private SqlBuffer(SqlBuffer value)
		{
			this._isNull = value._isNull;
			this._type = value._type;
			this._value = value._value;
			this._object = value._object;
		}

		// Token: 0x17000350 RID: 848
		// (get) Token: 0x06001858 RID: 6232 RVA: 0x000AC380 File Offset: 0x000AB780
		internal bool IsEmpty
		{
			get
			{
				return this._type == SqlBuffer.StorageType.Empty;
			}
		}

		// Token: 0x17000351 RID: 849
		// (get) Token: 0x06001859 RID: 6233 RVA: 0x000AC398 File Offset: 0x000AB798
		internal bool IsNull
		{
			get
			{
				return this._isNull;
			}
		}

		// Token: 0x17000352 RID: 850
		// (get) Token: 0x0600185A RID: 6234 RVA: 0x000AC3AC File Offset: 0x000AB7AC
		internal SqlBuffer.StorageType VariantInternalStorageType
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x17000353 RID: 851
		// (get) Token: 0x0600185B RID: 6235 RVA: 0x000AC3C0 File Offset: 0x000AB7C0
		// (set) Token: 0x0600185C RID: 6236 RVA: 0x000AC3F4 File Offset: 0x000AB7F4
		internal bool Boolean
		{
			get
			{
				this.ThrowIfNull();
				if (SqlBuffer.StorageType.Boolean == this._type)
				{
					return this._value._boolean;
				}
				return (bool)this.Value;
			}
			set
			{
				this._value._boolean = value;
				this._type = SqlBuffer.StorageType.Boolean;
				this._isNull = false;
			}
		}

		// Token: 0x17000354 RID: 852
		// (get) Token: 0x0600185D RID: 6237 RVA: 0x000AC41C File Offset: 0x000AB81C
		// (set) Token: 0x0600185E RID: 6238 RVA: 0x000AC450 File Offset: 0x000AB850
		internal byte Byte
		{
			get
			{
				this.ThrowIfNull();
				if (SqlBuffer.StorageType.Byte == this._type)
				{
					return this._value._byte;
				}
				return (byte)this.Value;
			}
			set
			{
				this._value._byte = value;
				this._type = SqlBuffer.StorageType.Byte;
				this._isNull = false;
			}
		}

		// Token: 0x17000355 RID: 853
		// (get) Token: 0x0600185F RID: 6239 RVA: 0x000AC478 File Offset: 0x000AB878
		internal byte[] ByteArray
		{
			get
			{
				this.ThrowIfNull();
				return this.SqlBinary.Value;
			}
		}

		// Token: 0x17000356 RID: 854
		// (get) Token: 0x06001860 RID: 6240 RVA: 0x000AC49C File Offset: 0x000AB89C
		internal DateTime DateTime
		{
			get
			{
				this.ThrowIfNull();
				if (SqlBuffer.StorageType.Date == this._type)
				{
					return DateTime.MinValue.AddDays((double)this._value._int32);
				}
				if (SqlBuffer.StorageType.DateTime2 == this._type)
				{
					return new DateTime(SqlBuffer.GetTicksFromDateTime2Info(this._value._dateTime2Info));
				}
				if (SqlBuffer.StorageType.DateTime == this._type)
				{
					return SqlDateTime.ToDateTime(this._value._dateTimeInfo.daypart, this._value._dateTimeInfo.timepart);
				}
				return (DateTime)this.Value;
			}
		}

		// Token: 0x17000357 RID: 855
		// (get) Token: 0x06001861 RID: 6241 RVA: 0x000AC530 File Offset: 0x000AB930
		internal decimal Decimal
		{
			get
			{
				this.ThrowIfNull();
				if (SqlBuffer.StorageType.Decimal == this._type)
				{
					if (this._value._numericInfo.data4 != 0 || this._value._numericInfo.scale > 28)
					{
						throw new OverflowException(SQLResource.ConversionOverflowMessage);
					}
					return new decimal(this._value._numericInfo.data1, this._value._numericInfo.data2, this._value._numericInfo.data3, !this._value._numericInfo.positive, this._value._numericInfo.scale);
				}
				else
				{
					if (SqlBuffer.StorageType.Money == this._type)
					{
						long num = this._value._int64;
						bool isNegative = false;
						if (num < 0L)
						{
							isNegative = true;
							num = -num;
						}
						return new decimal((int)(num & (long)((ulong)-1)), (int)(num >> 32), 0, isNegative, 4);
					}
					return (decimal)this.Value;
				}
			}
		}

		// Token: 0x17000358 RID: 856
		// (get) Token: 0x06001862 RID: 6242 RVA: 0x000AC61C File Offset: 0x000ABA1C
		// (set) Token: 0x06001863 RID: 6243 RVA: 0x000AC650 File Offset: 0x000ABA50
		internal double Double
		{
			get
			{
				this.ThrowIfNull();
				if (SqlBuffer.StorageType.Double == this._type)
				{
					return this._value._double;
				}
				return (double)this.Value;
			}
			set
			{
				this._value._double = value;
				this._type = SqlBuffer.StorageType.Double;
				this._isNull = false;
			}
		}

		// Token: 0x17000359 RID: 857
		// (get) Token: 0x06001864 RID: 6244 RVA: 0x000AC678 File Offset: 0x000ABA78
		internal Guid Guid
		{
			get
			{
				this.ThrowIfNull();
				return this.SqlGuid.Value;
			}
		}

		// Token: 0x1700035A RID: 858
		// (get) Token: 0x06001865 RID: 6245 RVA: 0x000AC69C File Offset: 0x000ABA9C
		// (set) Token: 0x06001866 RID: 6246 RVA: 0x000AC6D0 File Offset: 0x000ABAD0
		internal short Int16
		{
			get
			{
				this.ThrowIfNull();
				if (SqlBuffer.StorageType.Int16 == this._type)
				{
					return this._value._int16;
				}
				return (short)this.Value;
			}
			set
			{
				this._value._int16 = value;
				this._type = SqlBuffer.StorageType.Int16;
				this._isNull = false;
			}
		}

		// Token: 0x1700035B RID: 859
		// (get) Token: 0x06001867 RID: 6247 RVA: 0x000AC6F8 File Offset: 0x000ABAF8
		// (set) Token: 0x06001868 RID: 6248 RVA: 0x000AC72C File Offset: 0x000ABB2C
		internal int Int32
		{
			get
			{
				this.ThrowIfNull();
				if (SqlBuffer.StorageType.Int32 == this._type)
				{
					return this._value._int32;
				}
				return (int)this.Value;
			}
			set
			{
				this._value._int32 = value;
				this._type = SqlBuffer.StorageType.Int32;
				this._isNull = false;
			}
		}

		// Token: 0x1700035C RID: 860
		// (get) Token: 0x06001869 RID: 6249 RVA: 0x000AC754 File Offset: 0x000ABB54
		// (set) Token: 0x0600186A RID: 6250 RVA: 0x000AC788 File Offset: 0x000ABB88
		internal long Int64
		{
			get
			{
				this.ThrowIfNull();
				if (SqlBuffer.StorageType.Int64 == this._type)
				{
					return this._value._int64;
				}
				return (long)this.Value;
			}
			set
			{
				this._value._int64 = value;
				this._type = SqlBuffer.StorageType.Int64;
				this._isNull = false;
			}
		}

		// Token: 0x1700035D RID: 861
		// (get) Token: 0x0600186B RID: 6251 RVA: 0x000AC7B0 File Offset: 0x000ABBB0
		// (set) Token: 0x0600186C RID: 6252 RVA: 0x000AC7E4 File Offset: 0x000ABBE4
		internal float Single
		{
			get
			{
				this.ThrowIfNull();
				if (SqlBuffer.StorageType.Single == this._type)
				{
					return this._value._single;
				}
				return (float)this.Value;
			}
			set
			{
				this._value._single = value;
				this._type = SqlBuffer.StorageType.Single;
				this._isNull = false;
			}
		}

		// Token: 0x1700035E RID: 862
		// (get) Token: 0x0600186D RID: 6253 RVA: 0x000AC80C File Offset: 0x000ABC0C
		internal string String
		{
			get
			{
				this.ThrowIfNull();
				if (SqlBuffer.StorageType.String == this._type)
				{
					return (string)this._object;
				}
				if (SqlBuffer.StorageType.SqlCachedBuffer == this._type)
				{
					return ((SqlCachedBuffer)this._object).ToString();
				}
				return (string)this.Value;
			}
		}

		// Token: 0x1700035F RID: 863
		// (get) Token: 0x0600186E RID: 6254 RVA: 0x000AC85C File Offset: 0x000ABC5C
		internal string KatmaiDateTimeString
		{
			get
			{
				this.ThrowIfNull();
				if (SqlBuffer.StorageType.Date == this._type)
				{
					return this.DateTime.ToString("yyyy-MM-dd", DateTimeFormatInfo.InvariantInfo);
				}
				if (SqlBuffer.StorageType.Time == this._type)
				{
					byte scale = this._value._timeInfo.scale;
					return new DateTime(this._value._timeInfo.ticks).ToString(SqlBuffer.__katmaiTimeFormatByScale[(int)scale], DateTimeFormatInfo.InvariantInfo);
				}
				if (SqlBuffer.StorageType.DateTime2 == this._type)
				{
					byte scale2 = this._value._dateTime2Info.timeInfo.scale;
					return this.DateTime.ToString(SqlBuffer.__katmaiDateTime2FormatByScale[(int)scale2], DateTimeFormatInfo.InvariantInfo);
				}
				if (SqlBuffer.StorageType.DateTimeOffset == this._type)
				{
					DateTimeOffset dateTimeOffset = this.DateTimeOffset;
					byte scale3 = this._value._dateTimeOffsetInfo.dateTime2Info.timeInfo.scale;
					return dateTimeOffset.ToString(SqlBuffer.__katmaiDateTimeOffsetFormatByScale[(int)scale3], DateTimeFormatInfo.InvariantInfo);
				}
				return (string)this.Value;
			}
		}

		// Token: 0x17000360 RID: 864
		// (get) Token: 0x0600186F RID: 6255 RVA: 0x000AC960 File Offset: 0x000ABD60
		internal SqlString KatmaiDateTimeSqlString
		{
			get
			{
				if (SqlBuffer.StorageType.Date != this._type && SqlBuffer.StorageType.Time != this._type && SqlBuffer.StorageType.DateTime2 != this._type && SqlBuffer.StorageType.DateTimeOffset != this._type)
				{
					return (SqlString)this.SqlValue;
				}
				if (this.IsNull)
				{
					return SqlString.Null;
				}
				return new SqlString(this.KatmaiDateTimeString);
			}
		}

		// Token: 0x17000361 RID: 865
		// (get) Token: 0x06001870 RID: 6256 RVA: 0x000AC9BC File Offset: 0x000ABDBC
		internal TimeSpan Time
		{
			get
			{
				this.ThrowIfNull();
				if (SqlBuffer.StorageType.Time == this._type)
				{
					return new TimeSpan(this._value._timeInfo.ticks);
				}
				return (TimeSpan)this.Value;
			}
		}

		// Token: 0x17000362 RID: 866
		// (get) Token: 0x06001871 RID: 6257 RVA: 0x000AC9FC File Offset: 0x000ABDFC
		internal DateTimeOffset DateTimeOffset
		{
			get
			{
				this.ThrowIfNull();
				if (SqlBuffer.StorageType.DateTimeOffset == this._type)
				{
					TimeSpan offset = new TimeSpan(0, (int)this._value._dateTimeOffsetInfo.offset, 0);
					return new DateTimeOffset(SqlBuffer.GetTicksFromDateTime2Info(this._value._dateTimeOffsetInfo.dateTime2Info) + offset.Ticks, offset);
				}
				return (DateTimeOffset)this.Value;
			}
		}

		// Token: 0x06001872 RID: 6258 RVA: 0x000ACA64 File Offset: 0x000ABE64
		private static long GetTicksFromDateTime2Info(SqlBuffer.DateTime2Info dateTime2Info)
		{
			return (long)dateTime2Info.date * 864000000000L + dateTime2Info.timeInfo.ticks;
		}

		// Token: 0x17000363 RID: 867
		// (get) Token: 0x06001873 RID: 6259 RVA: 0x000ACA90 File Offset: 0x000ABE90
		// (set) Token: 0x06001874 RID: 6260 RVA: 0x000ACAC0 File Offset: 0x000ABEC0
		internal SqlBinary SqlBinary
		{
			get
			{
				if (SqlBuffer.StorageType.SqlBinary == this._type)
				{
					return (SqlBinary)this._object;
				}
				return (SqlBinary)this.SqlValue;
			}
			set
			{
				this._object = value;
				this._type = SqlBuffer.StorageType.SqlBinary;
				this._isNull = value.IsNull;
			}
		}

		// Token: 0x17000364 RID: 868
		// (get) Token: 0x06001875 RID: 6261 RVA: 0x000ACAF0 File Offset: 0x000ABEF0
		internal SqlBoolean SqlBoolean
		{
			get
			{
				if (SqlBuffer.StorageType.Boolean != this._type)
				{
					return (SqlBoolean)this.SqlValue;
				}
				if (this.IsNull)
				{
					return SqlBoolean.Null;
				}
				return new SqlBoolean(this._value._boolean);
			}
		}

		// Token: 0x17000365 RID: 869
		// (get) Token: 0x06001876 RID: 6262 RVA: 0x000ACB30 File Offset: 0x000ABF30
		internal SqlByte SqlByte
		{
			get
			{
				if (SqlBuffer.StorageType.Byte != this._type)
				{
					return (SqlByte)this.SqlValue;
				}
				if (this.IsNull)
				{
					return SqlByte.Null;
				}
				return new SqlByte(this._value._byte);
			}
		}

		// Token: 0x17000366 RID: 870
		// (get) Token: 0x06001877 RID: 6263 RVA: 0x000ACB70 File Offset: 0x000ABF70
		// (set) Token: 0x06001878 RID: 6264 RVA: 0x000ACBAC File Offset: 0x000ABFAC
		internal SqlCachedBuffer SqlCachedBuffer
		{
			get
			{
				if (SqlBuffer.StorageType.SqlCachedBuffer != this._type)
				{
					return (SqlCachedBuffer)this.SqlValue;
				}
				if (this.IsNull)
				{
					return SqlCachedBuffer.Null;
				}
				return (SqlCachedBuffer)this._object;
			}
			set
			{
				this._object = value;
				this._type = SqlBuffer.StorageType.SqlCachedBuffer;
				this._isNull = value.IsNull;
			}
		}

		// Token: 0x17000367 RID: 871
		// (get) Token: 0x06001879 RID: 6265 RVA: 0x000ACBD4 File Offset: 0x000ABFD4
		// (set) Token: 0x0600187A RID: 6266 RVA: 0x000ACC10 File Offset: 0x000AC010
		internal SqlXml SqlXml
		{
			get
			{
				if (SqlBuffer.StorageType.SqlXml != this._type)
				{
					return (SqlXml)this.SqlValue;
				}
				if (this.IsNull)
				{
					return SqlXml.Null;
				}
				return (SqlXml)this._object;
			}
			set
			{
				this._object = value;
				this._type = SqlBuffer.StorageType.SqlXml;
				this._isNull = value.IsNull;
			}
		}

		// Token: 0x17000368 RID: 872
		// (get) Token: 0x0600187B RID: 6267 RVA: 0x000ACC38 File Offset: 0x000AC038
		internal SqlDateTime SqlDateTime
		{
			get
			{
				if (SqlBuffer.StorageType.DateTime != this._type)
				{
					return (SqlDateTime)this.SqlValue;
				}
				if (this.IsNull)
				{
					return SqlDateTime.Null;
				}
				return new SqlDateTime(this._value._dateTimeInfo.daypart, this._value._dateTimeInfo.timepart);
			}
		}

		// Token: 0x17000369 RID: 873
		// (get) Token: 0x0600187C RID: 6268 RVA: 0x000ACC90 File Offset: 0x000AC090
		internal SqlDecimal SqlDecimal
		{
			get
			{
				if (SqlBuffer.StorageType.Decimal != this._type)
				{
					return (SqlDecimal)this.SqlValue;
				}
				if (this.IsNull)
				{
					return SqlDecimal.Null;
				}
				return new SqlDecimal(this._value._numericInfo.precision, this._value._numericInfo.scale, this._value._numericInfo.positive, this._value._numericInfo.data1, this._value._numericInfo.data2, this._value._numericInfo.data3, this._value._numericInfo.data4);
			}
		}

		// Token: 0x1700036A RID: 874
		// (get) Token: 0x0600187D RID: 6269 RVA: 0x000ACD38 File Offset: 0x000AC138
		internal SqlDouble SqlDouble
		{
			get
			{
				if (SqlBuffer.StorageType.Double != this._type)
				{
					return (SqlDouble)this.SqlValue;
				}
				if (this.IsNull)
				{
					return SqlDouble.Null;
				}
				return new SqlDouble(this._value._double);
			}
		}

		// Token: 0x1700036B RID: 875
		// (get) Token: 0x0600187E RID: 6270 RVA: 0x000ACD78 File Offset: 0x000AC178
		// (set) Token: 0x0600187F RID: 6271 RVA: 0x000ACDA8 File Offset: 0x000AC1A8
		internal SqlGuid SqlGuid
		{
			get
			{
				if (SqlBuffer.StorageType.SqlGuid == this._type)
				{
					return (SqlGuid)this._object;
				}
				return (SqlGuid)this.SqlValue;
			}
			set
			{
				this._object = value;
				this._type = SqlBuffer.StorageType.SqlGuid;
				this._isNull = value.IsNull;
			}
		}

		// Token: 0x1700036C RID: 876
		// (get) Token: 0x06001880 RID: 6272 RVA: 0x000ACDD8 File Offset: 0x000AC1D8
		internal SqlInt16 SqlInt16
		{
			get
			{
				if (SqlBuffer.StorageType.Int16 != this._type)
				{
					return (SqlInt16)this.SqlValue;
				}
				if (this.IsNull)
				{
					return SqlInt16.Null;
				}
				return new SqlInt16(this._value._int16);
			}
		}

		// Token: 0x1700036D RID: 877
		// (get) Token: 0x06001881 RID: 6273 RVA: 0x000ACE18 File Offset: 0x000AC218
		internal SqlInt32 SqlInt32
		{
			get
			{
				if (SqlBuffer.StorageType.Int32 != this._type)
				{
					return (SqlInt32)this.SqlValue;
				}
				if (this.IsNull)
				{
					return SqlInt32.Null;
				}
				return new SqlInt32(this._value._int32);
			}
		}

		// Token: 0x1700036E RID: 878
		// (get) Token: 0x06001882 RID: 6274 RVA: 0x000ACE58 File Offset: 0x000AC258
		internal SqlInt64 SqlInt64
		{
			get
			{
				if (SqlBuffer.StorageType.Int64 != this._type)
				{
					return (SqlInt64)this.SqlValue;
				}
				if (this.IsNull)
				{
					return SqlInt64.Null;
				}
				return new SqlInt64(this._value._int64);
			}
		}

		// Token: 0x1700036F RID: 879
		// (get) Token: 0x06001883 RID: 6275 RVA: 0x000ACE98 File Offset: 0x000AC298
		internal SqlMoney SqlMoney
		{
			get
			{
				if (SqlBuffer.StorageType.Money != this._type)
				{
					return (SqlMoney)this.SqlValue;
				}
				if (this.IsNull)
				{
					return SqlMoney.Null;
				}
				return new SqlMoney(this._value._int64, 1);
			}
		}

		// Token: 0x17000370 RID: 880
		// (get) Token: 0x06001884 RID: 6276 RVA: 0x000ACEDC File Offset: 0x000AC2DC
		internal SqlSingle SqlSingle
		{
			get
			{
				if (SqlBuffer.StorageType.Single != this._type)
				{
					return (SqlSingle)this.SqlValue;
				}
				if (this.IsNull)
				{
					return SqlSingle.Null;
				}
				return new SqlSingle(this._value._single);
			}
		}

		// Token: 0x17000371 RID: 881
		// (get) Token: 0x06001885 RID: 6277 RVA: 0x000ACF20 File Offset: 0x000AC320
		internal SqlString SqlString
		{
			get
			{
				if (SqlBuffer.StorageType.String == this._type)
				{
					if (this.IsNull)
					{
						return SqlString.Null;
					}
					return new SqlString((string)this._object);
				}
				else
				{
					if (SqlBuffer.StorageType.SqlCachedBuffer != this._type)
					{
						return (SqlString)this.SqlValue;
					}
					SqlCachedBuffer sqlCachedBuffer = (SqlCachedBuffer)this._object;
					if (sqlCachedBuffer.IsNull)
					{
						return SqlString.Null;
					}
					return sqlCachedBuffer.ToSqlString();
				}
			}
		}

		// Token: 0x17000372 RID: 882
		// (get) Token: 0x06001886 RID: 6278 RVA: 0x000ACF8C File Offset: 0x000AC38C
		internal object SqlValue
		{
			get
			{
				switch (this._type)
				{
				case SqlBuffer.StorageType.Empty:
					return DBNull.Value;
				case SqlBuffer.StorageType.Boolean:
					return this.SqlBoolean;
				case SqlBuffer.StorageType.Byte:
					return this.SqlByte;
				case SqlBuffer.StorageType.DateTime:
					return this.SqlDateTime;
				case SqlBuffer.StorageType.Decimal:
					return this.SqlDecimal;
				case SqlBuffer.StorageType.Double:
					return this.SqlDouble;
				case SqlBuffer.StorageType.Int16:
					return this.SqlInt16;
				case SqlBuffer.StorageType.Int32:
					return this.SqlInt32;
				case SqlBuffer.StorageType.Int64:
					return this.SqlInt64;
				case SqlBuffer.StorageType.Money:
					return this.SqlMoney;
				case SqlBuffer.StorageType.Single:
					return this.SqlSingle;
				case SqlBuffer.StorageType.String:
					return this.SqlString;
				case SqlBuffer.StorageType.SqlBinary:
				case SqlBuffer.StorageType.SqlGuid:
					return this._object;
				case SqlBuffer.StorageType.SqlCachedBuffer:
				{
					SqlCachedBuffer sqlCachedBuffer = (SqlCachedBuffer)this._object;
					if (sqlCachedBuffer.IsNull)
					{
						return SqlXml.Null;
					}
					return sqlCachedBuffer.ToSqlXml();
				}
				case SqlBuffer.StorageType.SqlXml:
					if (this._isNull)
					{
						return SqlXml.Null;
					}
					return (SqlXml)this._object;
				case SqlBuffer.StorageType.Date:
				case SqlBuffer.StorageType.DateTime2:
					if (this._isNull)
					{
						return DBNull.Value;
					}
					return this.DateTime;
				case SqlBuffer.StorageType.DateTimeOffset:
					if (this._isNull)
					{
						return DBNull.Value;
					}
					return this.DateTimeOffset;
				case SqlBuffer.StorageType.Time:
					if (this._isNull)
					{
						return DBNull.Value;
					}
					return this.Time;
				default:
					return null;
				}
			}
		}

		// Token: 0x17000373 RID: 883
		// (get) Token: 0x06001887 RID: 6279 RVA: 0x000AD118 File Offset: 0x000AC518
		internal object Value
		{
			get
			{
				if (this.IsNull)
				{
					return DBNull.Value;
				}
				switch (this._type)
				{
				case SqlBuffer.StorageType.Empty:
					return DBNull.Value;
				case SqlBuffer.StorageType.Boolean:
					return this.Boolean;
				case SqlBuffer.StorageType.Byte:
					return this.Byte;
				case SqlBuffer.StorageType.DateTime:
					return this.DateTime;
				case SqlBuffer.StorageType.Decimal:
					return this.Decimal;
				case SqlBuffer.StorageType.Double:
					return this.Double;
				case SqlBuffer.StorageType.Int16:
					return this.Int16;
				case SqlBuffer.StorageType.Int32:
					return this.Int32;
				case SqlBuffer.StorageType.Int64:
					return this.Int64;
				case SqlBuffer.StorageType.Money:
					return this.Decimal;
				case SqlBuffer.StorageType.Single:
					return this.Single;
				case SqlBuffer.StorageType.String:
					return this.String;
				case SqlBuffer.StorageType.SqlBinary:
					return this.ByteArray;
				case SqlBuffer.StorageType.SqlCachedBuffer:
					return ((SqlCachedBuffer)this._object).ToString();
				case SqlBuffer.StorageType.SqlGuid:
					return this.Guid;
				case SqlBuffer.StorageType.SqlXml:
				{
					SqlXml sqlXml = (SqlXml)this._object;
					return sqlXml.Value;
				}
				case SqlBuffer.StorageType.Date:
					return this.DateTime;
				case SqlBuffer.StorageType.DateTime2:
					return this.DateTime;
				case SqlBuffer.StorageType.DateTimeOffset:
					return this.DateTimeOffset;
				case SqlBuffer.StorageType.Time:
					return this.Time;
				default:
					return null;
				}
			}
		}

		// Token: 0x06001888 RID: 6280 RVA: 0x000AD284 File Offset: 0x000AC684
		internal Type GetTypeFromStorageType(bool isSqlType)
		{
			if (isSqlType)
			{
				switch (this._type)
				{
				case SqlBuffer.StorageType.Empty:
					return null;
				case SqlBuffer.StorageType.Boolean:
					return typeof(SqlBoolean);
				case SqlBuffer.StorageType.Byte:
					return typeof(SqlByte);
				case SqlBuffer.StorageType.DateTime:
					return typeof(SqlDateTime);
				case SqlBuffer.StorageType.Decimal:
					return typeof(SqlDecimal);
				case SqlBuffer.StorageType.Double:
					return typeof(SqlDouble);
				case SqlBuffer.StorageType.Int16:
					return typeof(SqlInt16);
				case SqlBuffer.StorageType.Int32:
					return typeof(SqlInt32);
				case SqlBuffer.StorageType.Int64:
					return typeof(SqlInt64);
				case SqlBuffer.StorageType.Money:
					return typeof(SqlMoney);
				case SqlBuffer.StorageType.Single:
					return typeof(SqlSingle);
				case SqlBuffer.StorageType.String:
					return typeof(SqlString);
				case SqlBuffer.StorageType.SqlBinary:
					return typeof(object);
				case SqlBuffer.StorageType.SqlCachedBuffer:
					return typeof(SqlString);
				case SqlBuffer.StorageType.SqlGuid:
					return typeof(object);
				case SqlBuffer.StorageType.SqlXml:
					return typeof(SqlXml);
				}
			}
			else
			{
				switch (this._type)
				{
				case SqlBuffer.StorageType.Empty:
					return null;
				case SqlBuffer.StorageType.Boolean:
					return typeof(bool);
				case SqlBuffer.StorageType.Byte:
					return typeof(byte);
				case SqlBuffer.StorageType.DateTime:
					return typeof(DateTime);
				case SqlBuffer.StorageType.Decimal:
					return typeof(decimal);
				case SqlBuffer.StorageType.Double:
					return typeof(double);
				case SqlBuffer.StorageType.Int16:
					return typeof(short);
				case SqlBuffer.StorageType.Int32:
					return typeof(int);
				case SqlBuffer.StorageType.Int64:
					return typeof(long);
				case SqlBuffer.StorageType.Money:
					return typeof(decimal);
				case SqlBuffer.StorageType.Single:
					return typeof(float);
				case SqlBuffer.StorageType.String:
					return typeof(string);
				case SqlBuffer.StorageType.SqlBinary:
					return typeof(byte[]);
				case SqlBuffer.StorageType.SqlCachedBuffer:
					return typeof(string);
				case SqlBuffer.StorageType.SqlGuid:
					return typeof(Guid);
				case SqlBuffer.StorageType.SqlXml:
					return typeof(string);
				}
			}
			return null;
		}

		// Token: 0x06001889 RID: 6281 RVA: 0x000AD48C File Offset: 0x000AC88C
		internal static SqlBuffer[] CreateBufferArray(int length)
		{
			SqlBuffer[] array = new SqlBuffer[length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = new SqlBuffer();
			}
			return array;
		}

		// Token: 0x0600188A RID: 6282 RVA: 0x000AD4B8 File Offset: 0x000AC8B8
		internal static SqlBuffer[] CloneBufferArray(SqlBuffer[] values)
		{
			SqlBuffer[] array = new SqlBuffer[values.Length];
			for (int i = 0; i < values.Length; i++)
			{
				array[i] = new SqlBuffer(values[i]);
			}
			return array;
		}

		// Token: 0x0600188B RID: 6283 RVA: 0x000AD4E8 File Offset: 0x000AC8E8
		internal static void Clear(SqlBuffer[] values)
		{
			if (values != null)
			{
				for (int i = 0; i < values.Length; i++)
				{
					values[i].Clear();
				}
			}
		}

		// Token: 0x0600188C RID: 6284 RVA: 0x000AD510 File Offset: 0x000AC910
		internal void Clear()
		{
			this._isNull = false;
			this._type = SqlBuffer.StorageType.Empty;
			this._object = null;
		}

		// Token: 0x0600188D RID: 6285 RVA: 0x000AD534 File Offset: 0x000AC934
		internal void SetToDateTime(int daypart, int timepart)
		{
			this._value._dateTimeInfo.daypart = daypart;
			this._value._dateTimeInfo.timepart = timepart;
			this._type = SqlBuffer.StorageType.DateTime;
			this._isNull = false;
		}

		// Token: 0x0600188E RID: 6286 RVA: 0x000AD574 File Offset: 0x000AC974
		internal void SetToDecimal(byte precision, byte scale, bool positive, int[] bits)
		{
			this._value._numericInfo.precision = precision;
			this._value._numericInfo.scale = scale;
			this._value._numericInfo.positive = positive;
			this._value._numericInfo.data1 = bits[0];
			this._value._numericInfo.data2 = bits[1];
			this._value._numericInfo.data3 = bits[2];
			this._value._numericInfo.data4 = bits[3];
			this._type = SqlBuffer.StorageType.Decimal;
			this._isNull = false;
		}

		// Token: 0x0600188F RID: 6287 RVA: 0x000AD614 File Offset: 0x000ACA14
		internal void SetToMoney(long value)
		{
			this._value._int64 = value;
			this._type = SqlBuffer.StorageType.Money;
			this._isNull = false;
		}

		// Token: 0x06001890 RID: 6288 RVA: 0x000AD63C File Offset: 0x000ACA3C
		internal void SetToNullOfType(SqlBuffer.StorageType storageType)
		{
			this._type = storageType;
			this._isNull = true;
			this._object = null;
		}

		// Token: 0x06001891 RID: 6289 RVA: 0x000AD660 File Offset: 0x000ACA60
		internal void SetToString(string value)
		{
			this._object = value;
			this._type = SqlBuffer.StorageType.String;
			this._isNull = false;
		}

		// Token: 0x06001892 RID: 6290 RVA: 0x000AD684 File Offset: 0x000ACA84
		internal void SetToDate(byte[] bytes)
		{
			this._type = SqlBuffer.StorageType.Date;
			this._value._int32 = SqlBuffer.GetDateFromByteArray(bytes, 0);
			this._isNull = false;
		}

		// Token: 0x06001893 RID: 6291 RVA: 0x000AD6B4 File Offset: 0x000ACAB4
		internal void SetToDate(DateTime date)
		{
			this._type = SqlBuffer.StorageType.Date;
			this._value._int32 = date.Subtract(DateTime.MinValue).Days;
			this._isNull = false;
		}

		// Token: 0x06001894 RID: 6292 RVA: 0x000AD6F0 File Offset: 0x000ACAF0
		internal void SetToTime(byte[] bytes, int length, byte scale, byte denormalizedScale)
		{
			this._type = SqlBuffer.StorageType.Time;
			SqlBuffer.FillInTimeInfo(ref this._value._timeInfo, bytes, length, scale, denormalizedScale);
			this._isNull = false;
		}

		// Token: 0x06001895 RID: 6293 RVA: 0x000AD724 File Offset: 0x000ACB24
		internal void SetToTime(TimeSpan timeSpan, byte scale)
		{
			this._type = SqlBuffer.StorageType.Time;
			this._value._timeInfo.ticks = timeSpan.Ticks;
			this._value._timeInfo.scale = scale;
			this._isNull = false;
		}

		// Token: 0x06001896 RID: 6294 RVA: 0x000AD768 File Offset: 0x000ACB68
		internal void SetToDateTime2(byte[] bytes, int length, byte scale, byte denormalizedScale)
		{
			this._type = SqlBuffer.StorageType.DateTime2;
			SqlBuffer.FillInTimeInfo(ref this._value._dateTime2Info.timeInfo, bytes, length - 3, scale, denormalizedScale);
			this._value._dateTime2Info.date = SqlBuffer.GetDateFromByteArray(bytes, length - 3);
			this._isNull = false;
		}

		// Token: 0x06001897 RID: 6295 RVA: 0x000AD7BC File Offset: 0x000ACBBC
		internal void SetToDateTime2(DateTime dateTime, byte scale)
		{
			this._type = SqlBuffer.StorageType.DateTime2;
			this._value._dateTime2Info.timeInfo.ticks = dateTime.TimeOfDay.Ticks;
			this._value._dateTime2Info.timeInfo.scale = scale;
			this._value._dateTime2Info.date = dateTime.Subtract(DateTime.MinValue).Days;
			this._isNull = false;
		}

		// Token: 0x06001898 RID: 6296 RVA: 0x000AD838 File Offset: 0x000ACC38
		internal void SetToDateTimeOffset(byte[] bytes, int length, byte scale, byte denormalizedScale)
		{
			this._type = SqlBuffer.StorageType.DateTimeOffset;
			SqlBuffer.FillInTimeInfo(ref this._value._dateTimeOffsetInfo.dateTime2Info.timeInfo, bytes, length - 5, scale, denormalizedScale);
			this._value._dateTimeOffsetInfo.dateTime2Info.date = SqlBuffer.GetDateFromByteArray(bytes, length - 5);
			this._value._dateTimeOffsetInfo.offset = (short)((int)bytes[length - 2] + ((int)bytes[length - 1] << 8));
			this._isNull = false;
		}

		// Token: 0x06001899 RID: 6297 RVA: 0x000AD8B4 File Offset: 0x000ACCB4
		internal void SetToDateTimeOffset(DateTimeOffset dateTimeOffset, byte scale)
		{
			this._type = SqlBuffer.StorageType.DateTimeOffset;
			DateTime utcDateTime = dateTimeOffset.UtcDateTime;
			this._value._dateTimeOffsetInfo.dateTime2Info.timeInfo.ticks = utcDateTime.TimeOfDay.Ticks;
			this._value._dateTimeOffsetInfo.dateTime2Info.timeInfo.scale = scale;
			this._value._dateTimeOffsetInfo.dateTime2Info.date = utcDateTime.Subtract(DateTime.MinValue).Days;
			this._value._dateTimeOffsetInfo.offset = (short)dateTimeOffset.Offset.TotalMinutes;
			this._isNull = false;
		}

		// Token: 0x0600189A RID: 6298 RVA: 0x000AD968 File Offset: 0x000ACD68
		private static void FillInTimeInfo(ref SqlBuffer.TimeInfo timeInfo, byte[] timeBytes, int length, byte scale, byte denormalizedScale)
		{
			long num = (long)((ulong)timeBytes[0] + ((ulong)timeBytes[1] << 8) + ((ulong)timeBytes[2] << 16));
			if (length > 3)
			{
				num += (long)((long)((ulong)timeBytes[3]) << 24);
			}
			if (length > 4)
			{
				num += (long)((long)((ulong)timeBytes[4]) << 32);
			}
			timeInfo.ticks = num * TdsEnums.TICKS_FROM_SCALE[(int)scale];
			timeInfo.scale = denormalizedScale;
		}

		// Token: 0x0600189B RID: 6299 RVA: 0x000AD9BC File Offset: 0x000ACDBC
		private static int GetDateFromByteArray(byte[] buf, int offset)
		{
			return (int)buf[offset] + ((int)buf[offset + 1] << 8) + ((int)buf[offset + 2] << 16);
		}

		// Token: 0x0600189C RID: 6300 RVA: 0x000AD9E0 File Offset: 0x000ACDE0
		private void ThrowIfNull()
		{
			if (this.IsNull)
			{
				throw new SqlNullValueException();
			}
		}

		// Token: 0x04000EA9 RID: 3753
		private bool _isNull;

		// Token: 0x04000EAA RID: 3754
		private SqlBuffer.StorageType _type;

		// Token: 0x04000EAB RID: 3755
		private SqlBuffer.Storage _value;

		// Token: 0x04000EAC RID: 3756
		private object _object;

		// Token: 0x04000EAD RID: 3757
		private static string[] __katmaiDateTimeOffsetFormatByScale = new string[]
		{
			"yyyy-MM-dd HH:mm:ss zzz",
			"yyyy-MM-dd HH:mm:ss.f zzz",
			"yyyy-MM-dd HH:mm:ss.ff zzz",
			"yyyy-MM-dd HH:mm:ss.fff zzz",
			"yyyy-MM-dd HH:mm:ss.ffff zzz",
			"yyyy-MM-dd HH:mm:ss.fffff zzz",
			"yyyy-MM-dd HH:mm:ss.ffffff zzz",
			"yyyy-MM-dd HH:mm:ss.fffffff zzz"
		};

		// Token: 0x04000EAE RID: 3758
		private static string[] __katmaiDateTime2FormatByScale = new string[]
		{
			"yyyy-MM-dd HH:mm:ss",
			"yyyy-MM-dd HH:mm:ss.f",
			"yyyy-MM-dd HH:mm:ss.ff",
			"yyyy-MM-dd HH:mm:ss.fff",
			"yyyy-MM-dd HH:mm:ss.ffff",
			"yyyy-MM-dd HH:mm:ss.fffff",
			"yyyy-MM-dd HH:mm:ss.ffffff",
			"yyyy-MM-dd HH:mm:ss.fffffff"
		};

		// Token: 0x04000EAF RID: 3759
		private static string[] __katmaiTimeFormatByScale = new string[]
		{
			"HH:mm:ss",
			"HH:mm:ss.f",
			"HH:mm:ss.ff",
			"HH:mm:ss.fff",
			"HH:mm:ss.ffff",
			"HH:mm:ss.fffff",
			"HH:mm:ss.ffffff",
			"HH:mm:ss.fffffff"
		};

		// Token: 0x02000375 RID: 885
		internal enum StorageType
		{
			// Token: 0x04001F3B RID: 7995
			Empty,
			// Token: 0x04001F3C RID: 7996
			Boolean,
			// Token: 0x04001F3D RID: 7997
			Byte,
			// Token: 0x04001F3E RID: 7998
			DateTime,
			// Token: 0x04001F3F RID: 7999
			Decimal,
			// Token: 0x04001F40 RID: 8000
			Double,
			// Token: 0x04001F41 RID: 8001
			Int16,
			// Token: 0x04001F42 RID: 8002
			Int32,
			// Token: 0x04001F43 RID: 8003
			Int64,
			// Token: 0x04001F44 RID: 8004
			Money,
			// Token: 0x04001F45 RID: 8005
			Single,
			// Token: 0x04001F46 RID: 8006
			String,
			// Token: 0x04001F47 RID: 8007
			SqlBinary,
			// Token: 0x04001F48 RID: 8008
			SqlCachedBuffer,
			// Token: 0x04001F49 RID: 8009
			SqlGuid,
			// Token: 0x04001F4A RID: 8010
			SqlXml,
			// Token: 0x04001F4B RID: 8011
			Date,
			// Token: 0x04001F4C RID: 8012
			DateTime2,
			// Token: 0x04001F4D RID: 8013
			DateTimeOffset,
			// Token: 0x04001F4E RID: 8014
			Time
		}

		// Token: 0x02000376 RID: 886
		internal struct DateTimeInfo
		{
			// Token: 0x04001F4F RID: 8015
			internal int daypart;

			// Token: 0x04001F50 RID: 8016
			internal int timepart;
		}

		// Token: 0x02000377 RID: 887
		internal struct NumericInfo
		{
			// Token: 0x04001F51 RID: 8017
			internal int data1;

			// Token: 0x04001F52 RID: 8018
			internal int data2;

			// Token: 0x04001F53 RID: 8019
			internal int data3;

			// Token: 0x04001F54 RID: 8020
			internal int data4;

			// Token: 0x04001F55 RID: 8021
			internal byte precision;

			// Token: 0x04001F56 RID: 8022
			internal byte scale;

			// Token: 0x04001F57 RID: 8023
			internal bool positive;
		}

		// Token: 0x02000378 RID: 888
		internal struct TimeInfo
		{
			// Token: 0x04001F58 RID: 8024
			internal long ticks;

			// Token: 0x04001F59 RID: 8025
			internal byte scale;
		}

		// Token: 0x02000379 RID: 889
		internal struct DateTime2Info
		{
			// Token: 0x04001F5A RID: 8026
			internal int date;

			// Token: 0x04001F5B RID: 8027
			internal SqlBuffer.TimeInfo timeInfo;
		}

		// Token: 0x0200037A RID: 890
		internal struct DateTimeOffsetInfo
		{
			// Token: 0x04001F5C RID: 8028
			internal SqlBuffer.DateTime2Info dateTime2Info;

			// Token: 0x04001F5D RID: 8029
			internal short offset;
		}

		// Token: 0x0200037B RID: 891
		[StructLayout(LayoutKind.Explicit)]
		internal struct Storage
		{
			// Token: 0x04001F5E RID: 8030
			[FieldOffset(0)]
			internal bool _boolean;

			// Token: 0x04001F5F RID: 8031
			[FieldOffset(0)]
			internal byte _byte;

			// Token: 0x04001F60 RID: 8032
			[FieldOffset(0)]
			internal SqlBuffer.DateTimeInfo _dateTimeInfo;

			// Token: 0x04001F61 RID: 8033
			[FieldOffset(0)]
			internal double _double;

			// Token: 0x04001F62 RID: 8034
			[FieldOffset(0)]
			internal SqlBuffer.NumericInfo _numericInfo;

			// Token: 0x04001F63 RID: 8035
			[FieldOffset(0)]
			internal short _int16;

			// Token: 0x04001F64 RID: 8036
			[FieldOffset(0)]
			internal int _int32;

			// Token: 0x04001F65 RID: 8037
			[FieldOffset(0)]
			internal long _int64;

			// Token: 0x04001F66 RID: 8038
			[FieldOffset(0)]
			internal float _single;

			// Token: 0x04001F67 RID: 8039
			[FieldOffset(0)]
			internal SqlBuffer.TimeInfo _timeInfo;

			// Token: 0x04001F68 RID: 8040
			[FieldOffset(0)]
			internal SqlBuffer.DateTime2Info _dateTime2Info;

			// Token: 0x04001F69 RID: 8041
			[FieldOffset(0)]
			internal SqlBuffer.DateTimeOffsetInfo _dateTimeOffsetInfo;
		}
	}
}
