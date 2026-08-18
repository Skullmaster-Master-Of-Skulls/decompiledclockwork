using System;
using System.Data.Common;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Text;
using Microsoft.SqlServer.Server;

namespace System.Data.SqlClient
{
	// Token: 0x02000337 RID: 823
	internal class TdsValueSetter
	{
		// Token: 0x06002B02 RID: 11010 RVA: 0x002C1F68 File Offset: 0x002C1368
		internal TdsValueSetter(TdsParserStateObject stateObj, SmiMetaData md)
		{
			this._stateObj = stateObj;
			this._metaData = md;
			this._isPlp = MetaDataUtilsSmi.IsPlpFormat(md);
			this._plpUnknownSent = false;
			this._encoder = null;
		}

		// Token: 0x06002B03 RID: 11011 RVA: 0x002C1FA8 File Offset: 0x002C13A8
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
				this._stateObj.Parser.WriteByte(0, this._stateObj);
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

		// Token: 0x06002B04 RID: 11012 RVA: 0x002C20C8 File Offset: 0x002C14C8
		internal void SetBoolean(bool value)
		{
			if (SqlDbType.Variant == this._metaData.SqlDbType)
			{
				this._stateObj.Parser.WriteSqlVariantHeader(3, 50, 0, this._stateObj);
			}
			else
			{
				this._stateObj.Parser.WriteByte((byte)this._metaData.MaxLength, this._stateObj);
			}
			if (value)
			{
				this._stateObj.Parser.WriteByte(1, this._stateObj);
				return;
			}
			this._stateObj.Parser.WriteByte(0, this._stateObj);
		}

		// Token: 0x06002B05 RID: 11013 RVA: 0x002C2158 File Offset: 0x002C1558
		internal void SetByte(byte value)
		{
			if (SqlDbType.Variant == this._metaData.SqlDbType)
			{
				this._stateObj.Parser.WriteSqlVariantHeader(3, 48, 0, this._stateObj);
			}
			else
			{
				this._stateObj.Parser.WriteByte((byte)this._metaData.MaxLength, this._stateObj);
			}
			this._stateObj.Parser.WriteByte(value, this._stateObj);
		}

		// Token: 0x06002B06 RID: 11014 RVA: 0x002C21D8 File Offset: 0x002C15D8
		internal int SetBytes(long fieldOffset, byte[] buffer, int bufferOffset, int length)
		{
			this.SetBytesNoOffsetHandling(fieldOffset, buffer, bufferOffset, length);
			return length;
		}

		// Token: 0x06002B07 RID: 11015 RVA: 0x002C21F8 File Offset: 0x002C15F8
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
				this._stateObj.Parser.WriteByteArray(buffer, length, bufferOffset, this._stateObj);
				return;
			}
			if (SqlDbType.Variant == this._metaData.SqlDbType)
			{
				this._stateObj.Parser.WriteSqlVariantHeader(4 + length, 165, 2, this._stateObj);
			}
			this._stateObj.Parser.WriteShort(length, this._stateObj);
			this._stateObj.Parser.WriteByteArray(buffer, length, bufferOffset, this._stateObj);
		}

		// Token: 0x06002B08 RID: 11016 RVA: 0x002C22D8 File Offset: 0x002C16D8
		internal void SetBytesLength(long length)
		{
			if (0L == length)
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

		// Token: 0x06002B09 RID: 11017 RVA: 0x002C2388 File Offset: 0x002C1788
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
				this._stateObj.Parser.WriteInt(length * ADP.CharSize, this._stateObj);
				this._stateObj.Parser.WriteCharArray(buffer, length, bufferOffset, this._stateObj);
			}
			else if (SqlDbType.Variant == this._metaData.SqlDbType)
			{
				this._stateObj.Parser.WriteSqlVariantValue(new string(buffer, bufferOffset, length), length, 0, this._stateObj);
			}
			else
			{
				this._stateObj.Parser.WriteShort(length * ADP.CharSize, this._stateObj);
				this._stateObj.Parser.WriteCharArray(buffer, length, bufferOffset, this._stateObj);
			}
			return length;
		}

		// Token: 0x06002B0A RID: 11018 RVA: 0x002C24E8 File Offset: 0x002C18E8
		internal void SetCharsLength(long length)
		{
			if (0L == length)
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

		// Token: 0x06002B0B RID: 11019 RVA: 0x002C2568 File Offset: 0x002C1968
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
				if (length * ADP.CharSize > 8000)
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
					this._stateObj.Parser.WriteByte(sqlCollation.sortId, this._stateObj);
					this._stateObj.Parser.WriteShort(bytes2.Length, this._stateObj);
					this._stateObj.Parser.WriteByteArray(bytes2, bytes2.Length, 0, this._stateObj);
				}
				else
				{
					this._stateObj.Parser.WriteSqlVariantHeader(9 + length * ADP.CharSize, 231, 7, this._stateObj);
					this._stateObj.Parser.WriteUnsignedInt(sqlCollation.info, this._stateObj);
					this._stateObj.Parser.WriteByte(sqlCollation.sortId, this._stateObj);
					this._stateObj.Parser.WriteShort(length * ADP.CharSize, this._stateObj);
					this._stateObj.Parser.WriteString(value, length, offset, this._stateObj);
				}
				this._variantType = null;
				return;
			}
			if (this._isPlp)
			{
				this._stateObj.Parser.WriteLong((long)(length * ADP.CharSize), this._stateObj);
				this._stateObj.Parser.WriteInt(length * ADP.CharSize, this._stateObj);
				this._stateObj.Parser.WriteString(value, length, offset, this._stateObj);
				if (length != 0)
				{
					this._stateObj.Parser.WriteInt(0, this._stateObj);
					return;
				}
			}
			else
			{
				this._stateObj.Parser.WriteShort(length * ADP.CharSize, this._stateObj);
				this._stateObj.Parser.WriteString(value, length, offset, this._stateObj);
			}
		}

		// Token: 0x06002B0C RID: 11020 RVA: 0x002C2858 File Offset: 0x002C1C58
		internal void SetInt16(short value)
		{
			if (SqlDbType.Variant == this._metaData.SqlDbType)
			{
				this._stateObj.Parser.WriteSqlVariantHeader(4, 52, 0, this._stateObj);
			}
			else
			{
				this._stateObj.Parser.WriteByte((byte)this._metaData.MaxLength, this._stateObj);
			}
			this._stateObj.Parser.WriteShort((int)value, this._stateObj);
		}

		// Token: 0x06002B0D RID: 11021 RVA: 0x002C28D8 File Offset: 0x002C1CD8
		internal void SetInt32(int value)
		{
			if (SqlDbType.Variant == this._metaData.SqlDbType)
			{
				this._stateObj.Parser.WriteSqlVariantHeader(6, 56, 0, this._stateObj);
			}
			else
			{
				this._stateObj.Parser.WriteByte((byte)this._metaData.MaxLength, this._stateObj);
			}
			this._stateObj.Parser.WriteInt(value, this._stateObj);
		}

		// Token: 0x06002B0E RID: 11022 RVA: 0x002C2958 File Offset: 0x002C1D58
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
				this._stateObj.Parser.WriteByte((byte)this._metaData.MaxLength, this._stateObj);
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

		// Token: 0x06002B0F RID: 11023 RVA: 0x002C2AB8 File Offset: 0x002C1EB8
		internal void SetSingle(float value)
		{
			if (SqlDbType.Variant == this._metaData.SqlDbType)
			{
				this._stateObj.Parser.WriteSqlVariantHeader(6, 59, 0, this._stateObj);
			}
			else
			{
				this._stateObj.Parser.WriteByte((byte)this._metaData.MaxLength, this._stateObj);
			}
			this._stateObj.Parser.WriteFloat(value, this._stateObj);
		}

		// Token: 0x06002B10 RID: 11024 RVA: 0x002C2B38 File Offset: 0x002C1F38
		internal void SetDouble(double value)
		{
			if (SqlDbType.Variant == this._metaData.SqlDbType)
			{
				this._stateObj.Parser.WriteSqlVariantHeader(10, 62, 0, this._stateObj);
			}
			else
			{
				this._stateObj.Parser.WriteByte((byte)this._metaData.MaxLength, this._stateObj);
			}
			this._stateObj.Parser.WriteDouble(value, this._stateObj);
		}

		// Token: 0x06002B11 RID: 11025 RVA: 0x002C2BB8 File Offset: 0x002C1FB8
		internal void SetSqlDecimal(SqlDecimal value)
		{
			if (SqlDbType.Variant == this._metaData.SqlDbType)
			{
				this._stateObj.Parser.WriteSqlVariantHeader(21, 108, 2, this._stateObj);
				this._stateObj.Parser.WriteByte(value.Precision, this._stateObj);
				this._stateObj.Parser.WriteByte(value.Scale, this._stateObj);
				this._stateObj.Parser.WriteSqlDecimal(value, this._stateObj);
				return;
			}
			this._stateObj.Parser.WriteByte(checked((byte)MetaType.MetaDecimal.FixedLength), this._stateObj);
			this._stateObj.Parser.WriteSqlDecimal(SqlDecimal.ConvertToPrecScale(value, (int)this._metaData.Precision, (int)this._metaData.Scale), this._stateObj);
		}

		// Token: 0x06002B12 RID: 11026 RVA: 0x002C2C98 File Offset: 0x002C2098
		internal void SetDateTime(DateTime value)
		{
			if (SqlDbType.Variant == this._metaData.SqlDbType)
			{
				TdsDateTime tdsDateTime = MetaType.FromDateTime(value, 8);
				this._stateObj.Parser.WriteSqlVariantHeader(10, 61, 0, this._stateObj);
				this._stateObj.Parser.WriteInt(tdsDateTime.days, this._stateObj);
				this._stateObj.Parser.WriteInt(tdsDateTime.time, this._stateObj);
				return;
			}
			this._stateObj.Parser.WriteByte((byte)this._metaData.MaxLength, this._stateObj);
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
				this._stateObj.Parser.WriteByteArray(BitConverter.GetBytes(value2), (int)this._metaData.MaxLength - 3, 0, this._stateObj);
			}
			this._stateObj.Parser.WriteByteArray(BitConverter.GetBytes(days), 3, 0, this._stateObj);
		}

		// Token: 0x06002B13 RID: 11027 RVA: 0x002C2E88 File Offset: 0x002C2288
		internal void SetGuid(Guid value)
		{
			byte[] array = value.ToByteArray();
			if (SqlDbType.Variant == this._metaData.SqlDbType)
			{
				this._stateObj.Parser.WriteSqlVariantHeader(18, 36, 0, this._stateObj);
			}
			else
			{
				this._stateObj.Parser.WriteByte((byte)this._metaData.MaxLength, this._stateObj);
			}
			this._stateObj.Parser.WriteByteArray(array, array.Length, 0, this._stateObj);
		}

		// Token: 0x06002B14 RID: 11028 RVA: 0x002C2F08 File Offset: 0x002C2308
		internal void SetTimeSpan(TimeSpan value)
		{
			byte scale;
			byte b;
			if (SqlDbType.Variant == this._metaData.SqlDbType)
			{
				scale = MetaType.MetaTime.Scale;
				b = (byte)MetaType.MetaTime.FixedLength;
				this._stateObj.Parser.WriteSqlVariantHeader(8, 41, 1, this._stateObj);
				this._stateObj.Parser.WriteByte(scale, this._stateObj);
			}
			else
			{
				scale = this._metaData.Scale;
				b = (byte)this._metaData.MaxLength;
				this._stateObj.Parser.WriteByte(b, this._stateObj);
			}
			long value2 = value.Ticks / TdsEnums.TICKS_FROM_SCALE[(int)scale];
			this._stateObj.Parser.WriteByteArray(BitConverter.GetBytes(value2), (int)b, 0, this._stateObj);
		}

		// Token: 0x06002B15 RID: 11029 RVA: 0x002C2FD8 File Offset: 0x002C23D8
		internal void SetDateTimeOffset(DateTimeOffset value)
		{
			byte scale;
			byte b;
			if (SqlDbType.Variant == this._metaData.SqlDbType)
			{
				scale = MetaType.MetaDateTimeOffset.Scale;
				b = (byte)MetaType.MetaDateTimeOffset.FixedLength;
				this._stateObj.Parser.WriteSqlVariantHeader(13, 43, 1, this._stateObj);
				this._stateObj.Parser.WriteByte(scale, this._stateObj);
			}
			else
			{
				scale = this._metaData.Scale;
				b = (byte)this._metaData.MaxLength;
				this._stateObj.Parser.WriteByte(b, this._stateObj);
			}
			DateTime utcDateTime = value.UtcDateTime;
			long value2 = utcDateTime.TimeOfDay.Ticks / TdsEnums.TICKS_FROM_SCALE[(int)scale];
			int days = utcDateTime.Subtract(DateTime.MinValue).Days;
			short num = (short)value.Offset.TotalMinutes;
			this._stateObj.Parser.WriteByteArray(BitConverter.GetBytes(value2), (int)(b - 5), 0, this._stateObj);
			this._stateObj.Parser.WriteByteArray(BitConverter.GetBytes(days), 3, 0, this._stateObj);
			this._stateObj.Parser.WriteByte((byte)(num & 255), this._stateObj);
			this._stateObj.Parser.WriteByte((byte)(num >> 8 & 255), this._stateObj);
		}

		// Token: 0x06002B16 RID: 11030 RVA: 0x002C3138 File Offset: 0x002C2538
		internal void SetVariantType(SmiMetaData value)
		{
			this._variantType = value;
		}

		// Token: 0x06002B17 RID: 11031 RVA: 0x002C3158 File Offset: 0x002C2558
		[Conditional("DEBUG")]
		private void CheckSettingOffset(long offset)
		{
		}

		// Token: 0x04001C49 RID: 7241
		private TdsParserStateObject _stateObj;

		// Token: 0x04001C4A RID: 7242
		private SmiMetaData _metaData;

		// Token: 0x04001C4B RID: 7243
		private bool _isPlp;

		// Token: 0x04001C4C RID: 7244
		private bool _plpUnknownSent;

		// Token: 0x04001C4D RID: 7245
		private Encoder _encoder;

		// Token: 0x04001C4E RID: 7246
		private SmiMetaData _variantType;
	}
}
