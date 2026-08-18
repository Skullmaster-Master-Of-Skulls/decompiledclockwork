using System;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Text;
using Microsoft.SqlServer.Server;

namespace System.Data.SqlClient
{
	// Token: 0x02000235 RID: 565
	internal class TdsValueSetter
	{
		// Token: 0x06002305 RID: 8965 RVA: 0x000F1E20 File Offset: 0x000F1220
		internal TdsValueSetter(TdsParserStateObject stateObj, SmiMetaData md)
		{
			this._stateObj = stateObj;
			this._metaData = md;
			this._isPlp = MetaDataUtilsSmi.IsPlpFormat(md);
			this._plpUnknownSent = false;
			this._encoder = null;
		}

		// Token: 0x06002306 RID: 8966 RVA: 0x000F1E5C File Offset: 0x000F125C
		internal void SetDBNull()
		{
			if (this._isPlp)
			{
				this._stateObj.Parser.WriteUnsignedLong(ulong.MaxValue, this._stateObj);
				return;
			}
			switch (this._metaData.SqlDbType)
			{
			case SqlDbType.BigInt:
			case SqlDbType.Bit:
			case SqlDbType.DateTime:
			case SqlDbType.Decimal:
			case SqlDbType.Float:
			case SqlDbType.Int:
			case SqlDbType.Money:
			case SqlDbType.Real:
			case SqlDbType.UniqueIdentifier:
			case SqlDbType.SmallDateTime:
			case SqlDbType.SmallInt:
			case SqlDbType.SmallMoney:
			case SqlDbType.TinyInt:
			case SqlDbType.Date:
			case SqlDbType.Time:
			case SqlDbType.DateTime2:
			case SqlDbType.DateTimeOffset:
				this._stateObj.WriteByte(0);
				return;
			case SqlDbType.Binary:
			case SqlDbType.Char:
			case SqlDbType.Image:
			case SqlDbType.NChar:
			case SqlDbType.NText:
			case SqlDbType.NVarChar:
			case SqlDbType.Text:
			case SqlDbType.Timestamp:
			case SqlDbType.VarBinary:
			case SqlDbType.VarChar:
				this._stateObj.Parser.WriteShort(65535, this._stateObj);
				return;
			case SqlDbType.Variant:
				this._stateObj.Parser.WriteInt(0, this._stateObj);
				break;
			case (SqlDbType)24:
			case SqlDbType.Xml:
			case (SqlDbType)26:
			case (SqlDbType)27:
			case (SqlDbType)28:
			case SqlDbType.Udt:
			case SqlDbType.Structured:
				break;
			default:
				return;
			}
		}

		// Token: 0x06002307 RID: 8967 RVA: 0x000F1F6C File Offset: 0x000F136C
		internal void SetBoolean(bool value)
		{
			if (SqlDbType.Variant == this._metaData.SqlDbType)
			{
				this._stateObj.Parser.WriteSqlVariantHeader(3, 50, 0, this._stateObj);
			}
			else
			{
				this._stateObj.WriteByte((byte)this._metaData.MaxLength);
			}
			if (value)
			{
				this._stateObj.WriteByte(1);
				return;
			}
			this._stateObj.WriteByte(0);
		}

		// Token: 0x06002308 RID: 8968 RVA: 0x000F1FD8 File Offset: 0x000F13D8
		internal void SetByte(byte value)
		{
			if (SqlDbType.Variant == this._metaData.SqlDbType)
			{
				this._stateObj.Parser.WriteSqlVariantHeader(3, 48, 0, this._stateObj);
			}
			else
			{
				this._stateObj.WriteByte((byte)this._metaData.MaxLength);
			}
			this._stateObj.WriteByte(value);
		}

		// Token: 0x06002309 RID: 8969 RVA: 0x000F2034 File Offset: 0x000F1434
		internal int SetBytes(long fieldOffset, byte[] buffer, int bufferOffset, int length)
		{
			this.SetBytesNoOffsetHandling(fieldOffset, buffer, bufferOffset, length);
			return length;
		}

		// Token: 0x0600230A RID: 8970 RVA: 0x000F2050 File Offset: 0x000F1450
		private void SetBytesNoOffsetHandling(long fieldOffset, byte[] buffer, int bufferOffset, int length)
		{
			if (this._isPlp)
			{
				if (!this._plpUnknownSent)
				{
					this._stateObj.Parser.WriteUnsignedLong(18446744073709551614UL, this._stateObj);
					this._plpUnknownSent = true;
				}
				this._stateObj.Parser.WriteInt(length, this._stateObj);
				this._stateObj.WriteByteArray(buffer, length, bufferOffset, true, null);
				return;
			}
			if (SqlDbType.Variant == this._metaData.SqlDbType)
			{
				this._stateObj.Parser.WriteSqlVariantHeader(4 + length, 165, 2, this._stateObj);
			}
			this._stateObj.Parser.WriteShort(length, this._stateObj);
			this._stateObj.WriteByteArray(buffer, length, bufferOffset, true, null);
		}

		// Token: 0x0600230B RID: 8971 RVA: 0x000F2114 File Offset: 0x000F1514
		internal void SetBytesLength(long length)
		{
			if (length == 0L)
			{
				if (this._isPlp)
				{
					this._stateObj.Parser.WriteLong(0L, this._stateObj);
					this._plpUnknownSent = true;
				}
				else
				{
					if (SqlDbType.Variant == this._metaData.SqlDbType)
					{
						this._stateObj.Parser.WriteSqlVariantHeader(4, 165, 2, this._stateObj);
					}
					this._stateObj.Parser.WriteShort(0, this._stateObj);
				}
			}
			if (this._plpUnknownSent)
			{
				this._stateObj.Parser.WriteInt(0, this._stateObj);
				this._plpUnknownSent = false;
			}
		}

		// Token: 0x0600230C RID: 8972 RVA: 0x000F21B8 File Offset: 0x000F15B8
		internal int SetChars(long fieldOffset, char[] buffer, int bufferOffset, int length)
		{
			if (MetaDataUtilsSmi.IsAnsiType(this._metaData.SqlDbType))
			{
				if (this._encoder == null)
				{
					this._encoder = this._stateObj.Parser._defaultEncoding.GetEncoder();
				}
				byte[] array = new byte[this._encoder.GetByteCount(buffer, bufferOffset, length, false)];
				this._encoder.GetBytes(buffer, bufferOffset, length, array, 0, false);
				this.SetBytesNoOffsetHandling(fieldOffset, array, 0, array.Length);
			}
			else if (this._isPlp)
			{
				if (!this._plpUnknownSent)
				{
					this._stateObj.Parser.WriteUnsignedLong(18446744073709551614UL, this._stateObj);
					this._plpUnknownSent = true;
				}
				this._stateObj.Parser.WriteInt(length * 2, this._stateObj);
				this._stateObj.Parser.WriteCharArray(buffer, length, bufferOffset, this._stateObj, true);
			}
			else if (SqlDbType.Variant == this._metaData.SqlDbType)
			{
				this._stateObj.Parser.WriteSqlVariantValue(new string(buffer, bufferOffset, length), length, 0, this._stateObj, true);
			}
			else
			{
				this._stateObj.Parser.WriteShort(length * 2, this._stateObj);
				this._stateObj.Parser.WriteCharArray(buffer, length, bufferOffset, this._stateObj, true);
			}
			return length;
		}

		// Token: 0x0600230D RID: 8973 RVA: 0x000F230C File Offset: 0x000F170C
		internal void SetCharsLength(long length)
		{
			if (length == 0L)
			{
				if (this._isPlp)
				{
					this._stateObj.Parser.WriteLong(0L, this._stateObj);
					this._plpUnknownSent = true;
				}
				else
				{
					this._stateObj.Parser.WriteShort(0, this._stateObj);
				}
			}
			if (this._plpUnknownSent)
			{
				this._stateObj.Parser.WriteInt(0, this._stateObj);
				this._plpUnknownSent = false;
			}
			this._encoder = null;
		}

		// Token: 0x0600230E RID: 8974 RVA: 0x000F238C File Offset: 0x000F178C
		internal void SetString(string value, int offset, int length)
		{
			if (MetaDataUtilsSmi.IsAnsiType(this._metaData.SqlDbType))
			{
				byte[] bytes;
				if (offset == 0 && value.Length <= length)
				{
					bytes = this._stateObj.Parser._defaultEncoding.GetBytes(value);
				}
				else
				{
					char[] chars = value.ToCharArray(offset, length);
					bytes = this._stateObj.Parser._defaultEncoding.GetBytes(chars);
				}
				this.SetBytes(0L, bytes, 0, bytes.Length);
				this.SetBytesLength((long)bytes.Length);
				return;
			}
			if (SqlDbType.Variant == this._metaData.SqlDbType)
			{
				SqlCollation sqlCollation = new SqlCollation();
				sqlCollation.LCID = checked((int)this._variantType.LocaleId);
				sqlCollation.SqlCompareOptions = this._variantType.CompareOptions;
				if (length * 2 > 8000)
				{
					byte[] bytes2;
					if (offset == 0 && value.Length <= length)
					{
						bytes2 = this._stateObj.Parser._defaultEncoding.GetBytes(value);
					}
					else
					{
						bytes2 = this._stateObj.Parser._defaultEncoding.GetBytes(value.ToCharArray(offset, length));
					}
					this._stateObj.Parser.WriteSqlVariantHeader(9 + bytes2.Length, 167, 7, this._stateObj);
					this._stateObj.Parser.WriteUnsignedInt(sqlCollation.info, this._stateObj);
					this._stateObj.WriteByte(sqlCollation.sortId);
					this._stateObj.Parser.WriteShort(bytes2.Length, this._stateObj);
					this._stateObj.WriteByteArray(bytes2, bytes2.Length, 0, true, null);
				}
				else
				{
					this._stateObj.Parser.WriteSqlVariantHeader(9 + length * 2, 231, 7, this._stateObj);
					this._stateObj.Parser.WriteUnsignedInt(sqlCollation.info, this._stateObj);
					this._stateObj.WriteByte(sqlCollation.sortId);
					this._stateObj.Parser.WriteShort(length * 2, this._stateObj);
					this._stateObj.Parser.WriteString(value, length, offset, this._stateObj, true);
				}
				this._variantType = null;
				return;
			}
			if (this._isPlp)
			{
				this._stateObj.Parser.WriteLong((long)(length * 2), this._stateObj);
				this._stateObj.Parser.WriteInt(length * 2, this._stateObj);
				this._stateObj.Parser.WriteString(value, length, offset, this._stateObj, true);
				if (length != 0)
				{
					this._stateObj.Parser.WriteInt(0, this._stateObj);
					return;
				}
			}
			else
			{
				this._stateObj.Parser.WriteShort(length * 2, this._stateObj);
				this._stateObj.Parser.WriteString(value, length, offset, this._stateObj, true);
			}
		}

		// Token: 0x0600230F RID: 8975 RVA: 0x000F2644 File Offset: 0x000F1A44
		internal void SetInt16(short value)
		{
			if (SqlDbType.Variant == this._metaData.SqlDbType)
			{
				this._stateObj.Parser.WriteSqlVariantHeader(4, 52, 0, this._stateObj);
			}
			else
			{
				this._stateObj.WriteByte((byte)this._metaData.MaxLength);
			}
			this._stateObj.Parser.WriteShort((int)value, this._stateObj);
		}

		// Token: 0x06002310 RID: 8976 RVA: 0x000F26AC File Offset: 0x000F1AAC
		internal void SetInt32(int value)
		{
			if (SqlDbType.Variant == this._metaData.SqlDbType)
			{
				this._stateObj.Parser.WriteSqlVariantHeader(6, 56, 0, this._stateObj);
			}
			else
			{
				this._stateObj.WriteByte((byte)this._metaData.MaxLength);
			}
			this._stateObj.Parser.WriteInt(value, this._stateObj);
		}

		// Token: 0x06002311 RID: 8977 RVA: 0x000F2714 File Offset: 0x000F1B14
		internal void SetInt64(long value)
		{
			if (SqlDbType.Variant == this._metaData.SqlDbType)
			{
				if (this._variantType == null)
				{
					this._stateObj.Parser.WriteSqlVariantHeader(10, 127, 0, this._stateObj);
					this._stateObj.Parser.WriteLong(value, this._stateObj);
					return;
				}
				this._stateObj.Parser.WriteSqlVariantHeader(10, 60, 0, this._stateObj);
				this._stateObj.Parser.WriteInt((int)(value >> 32), this._stateObj);
				this._stateObj.Parser.WriteInt((int)value, this._stateObj);
				this._variantType = null;
				return;
			}
			else
			{
				this._stateObj.WriteByte((byte)this._metaData.MaxLength);
				if (SqlDbType.SmallMoney == this._metaData.SqlDbType)
				{
					this._stateObj.Parser.WriteInt((int)value, this._stateObj);
					return;
				}
				if (SqlDbType.Money == this._metaData.SqlDbType)
				{
					this._stateObj.Parser.WriteInt((int)(value >> 32), this._stateObj);
					this._stateObj.Parser.WriteInt((int)value, this._stateObj);
					return;
				}
				this._stateObj.Parser.WriteLong(value, this._stateObj);
				return;
			}
		}

		// Token: 0x06002312 RID: 8978 RVA: 0x000F2860 File Offset: 0x000F1C60
		internal void SetSingle(float value)
		{
			if (SqlDbType.Variant == this._metaData.SqlDbType)
			{
				this._stateObj.Parser.WriteSqlVariantHeader(6, 59, 0, this._stateObj);
			}
			else
			{
				this._stateObj.WriteByte((byte)this._metaData.MaxLength);
			}
			this._stateObj.Parser.WriteFloat(value, this._stateObj);
		}

		// Token: 0x06002313 RID: 8979 RVA: 0x000F28C8 File Offset: 0x000F1CC8
		internal void SetDouble(double value)
		{
			if (SqlDbType.Variant == this._metaData.SqlDbType)
			{
				this._stateObj.Parser.WriteSqlVariantHeader(10, 62, 0, this._stateObj);
			}
			else
			{
				this._stateObj.WriteByte((byte)this._metaData.MaxLength);
			}
			this._stateObj.Parser.WriteDouble(value, this._stateObj);
		}

		// Token: 0x06002314 RID: 8980 RVA: 0x000F2930 File Offset: 0x000F1D30
		internal void SetSqlDecimal(SqlDecimal value)
		{
			if (SqlDbType.Variant == this._metaData.SqlDbType)
			{
				this._stateObj.Parser.WriteSqlVariantHeader(21, 108, 2, this._stateObj);
				this._stateObj.WriteByte(value.Precision);
				this._stateObj.WriteByte(value.Scale);
				this._stateObj.Parser.WriteSqlDecimal(value, this._stateObj);
				return;
			}
			this._stateObj.WriteByte(checked((byte)MetaType.MetaDecimal.FixedLength));
			this._stateObj.Parser.WriteSqlDecimal(SqlDecimal.ConvertToPrecScale(value, (int)this._metaData.Precision, (int)this._metaData.Scale), this._stateObj);
		}

		// Token: 0x06002315 RID: 8981 RVA: 0x000F29EC File Offset: 0x000F1DEC
		internal void SetDateTime(DateTime value)
		{
			if (SqlDbType.Variant == this._metaData.SqlDbType)
			{
				if (this._variantType != null && this._variantType.SqlDbType == SqlDbType.DateTime2)
				{
					this._stateObj.Parser.WriteSqlVariantDateTime2(value, this._stateObj);
				}
				else if (this._variantType != null && this._variantType.SqlDbType == SqlDbType.Date)
				{
					this._stateObj.Parser.WriteSqlVariantDate(value, this._stateObj);
				}
				else
				{
					TdsDateTime tdsDateTime = MetaType.FromDateTime(value, 8);
					this._stateObj.Parser.WriteSqlVariantHeader(10, 61, 0, this._stateObj);
					this._stateObj.Parser.WriteInt(tdsDateTime.days, this._stateObj);
					this._stateObj.Parser.WriteInt(tdsDateTime.time, this._stateObj);
				}
				this._variantType = null;
				return;
			}
			this._stateObj.WriteByte((byte)this._metaData.MaxLength);
			if (SqlDbType.SmallDateTime == this._metaData.SqlDbType)
			{
				TdsDateTime tdsDateTime2 = MetaType.FromDateTime(value, (byte)this._metaData.MaxLength);
				this._stateObj.Parser.WriteShort(tdsDateTime2.days, this._stateObj);
				this._stateObj.Parser.WriteShort(tdsDateTime2.time, this._stateObj);
				return;
			}
			if (SqlDbType.DateTime == this._metaData.SqlDbType)
			{
				TdsDateTime tdsDateTime3 = MetaType.FromDateTime(value, (byte)this._metaData.MaxLength);
				this._stateObj.Parser.WriteInt(tdsDateTime3.days, this._stateObj);
				this._stateObj.Parser.WriteInt(tdsDateTime3.time, this._stateObj);
				return;
			}
			int days = value.Subtract(DateTime.MinValue).Days;
			if (SqlDbType.DateTime2 == this._metaData.SqlDbType)
			{
				long value2 = value.TimeOfDay.Ticks / TdsEnums.TICKS_FROM_SCALE[(int)this._metaData.Scale];
				this._stateObj.WriteByteArray(BitConverter.GetBytes(value2), (int)this._metaData.MaxLength - 3, 0, true, null);
			}
			this._stateObj.WriteByteArray(BitConverter.GetBytes(days), 3, 0, true, null);
		}

		// Token: 0x06002316 RID: 8982 RVA: 0x000F2C20 File Offset: 0x000F2020
		internal void SetGuid(Guid value)
		{
			byte[] array = value.ToByteArray();
			if (SqlDbType.Variant == this._metaData.SqlDbType)
			{
				this._stateObj.Parser.WriteSqlVariantHeader(18, 36, 0, this._stateObj);
			}
			else
			{
				this._stateObj.WriteByte((byte)this._metaData.MaxLength);
			}
			this._stateObj.WriteByteArray(array, array.Length, 0, true, null);
		}

		// Token: 0x06002317 RID: 8983 RVA: 0x000F2C8C File Offset: 0x000F208C
		internal void SetTimeSpan(TimeSpan value)
		{
			byte scale;
			byte b;
			if (SqlDbType.Variant == this._metaData.SqlDbType)
			{
				scale = SmiMetaData.DefaultTime.Scale;
				b = (byte)SmiMetaData.DefaultTime.MaxLength;
				this._stateObj.Parser.WriteSqlVariantHeader(8, 41, 1, this._stateObj);
				this._stateObj.WriteByte(scale);
			}
			else
			{
				scale = this._metaData.Scale;
				b = (byte)this._metaData.MaxLength;
				this._stateObj.WriteByte(b);
			}
			long value2 = value.Ticks / TdsEnums.TICKS_FROM_SCALE[(int)scale];
			this._stateObj.WriteByteArray(BitConverter.GetBytes(value2), (int)b, 0, true, null);
		}

		// Token: 0x06002318 RID: 8984 RVA: 0x000F2D34 File Offset: 0x000F2134
		internal void SetDateTimeOffset(DateTimeOffset value)
		{
			byte scale;
			byte b;
			if (SqlDbType.Variant == this._metaData.SqlDbType)
			{
				SmiMetaData defaultDateTimeOffset = SmiMetaData.DefaultDateTimeOffset;
				scale = MetaType.MetaDateTimeOffset.Scale;
				b = (byte)defaultDateTimeOffset.MaxLength;
				this._stateObj.Parser.WriteSqlVariantHeader(13, 43, 1, this._stateObj);
				this._stateObj.WriteByte(scale);
			}
			else
			{
				scale = this._metaData.Scale;
				b = (byte)this._metaData.MaxLength;
				this._stateObj.WriteByte(b);
			}
			DateTime utcDateTime = value.UtcDateTime;
			long value2 = utcDateTime.TimeOfDay.Ticks / TdsEnums.TICKS_FROM_SCALE[(int)scale];
			int days = utcDateTime.Subtract(DateTime.MinValue).Days;
			short num = (short)value.Offset.TotalMinutes;
			this._stateObj.WriteByteArray(BitConverter.GetBytes(value2), (int)(b - 5), 0, true, null);
			this._stateObj.WriteByteArray(BitConverter.GetBytes(days), 3, 0, true, null);
			this._stateObj.WriteByte((byte)(num & 255));
			this._stateObj.WriteByte((byte)(num >> 8 & 255));
		}

		// Token: 0x06002319 RID: 8985 RVA: 0x000F2E5C File Offset: 0x000F225C
		internal void SetVariantType(SmiMetaData value)
		{
			this._variantType = value;
		}

		// Token: 0x0600231A RID: 8986 RVA: 0x000F2E70 File Offset: 0x000F2270
		[Conditional("DEBUG")]
		private void CheckSettingOffset(long offset)
		{
		}

		// Token: 0x04001537 RID: 5431
		private TdsParserStateObject _stateObj;

		// Token: 0x04001538 RID: 5432
		private SmiMetaData _metaData;

		// Token: 0x04001539 RID: 5433
		private bool _isPlp;

		// Token: 0x0400153A RID: 5434
		private bool _plpUnknownSent;

		// Token: 0x0400153B RID: 5435
		private Encoder _encoder;

		// Token: 0x0400153C RID: 5436
		private SmiMetaData _variantType;
	}
}
