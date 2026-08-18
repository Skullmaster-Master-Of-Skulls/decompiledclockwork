using System;
using System.Data.Common;
using System.Data.ProviderBase;
using System.Runtime.InteropServices;

namespace System.Data.Odbc
{
	// Token: 0x020002B3 RID: 691
	internal sealed class CNativeBuffer : DbBuffer
	{
		// Token: 0x060029E3 RID: 10723 RVA: 0x001153C8 File Offset: 0x001147C8
		internal CNativeBuffer(int initialSize) : base(initialSize)
		{
		}

		// Token: 0x170006D4 RID: 1748
		// (get) Token: 0x060029E4 RID: 10724 RVA: 0x001153DC File Offset: 0x001147DC
		internal short ShortLength
		{
			get
			{
				return checked((short)base.Length);
			}
		}

		// Token: 0x060029E5 RID: 10725 RVA: 0x001153F0 File Offset: 0x001147F0
		internal object MarshalToManaged(int offset, ODBC32.SQL_C sqlctype, int cb)
		{
			if (sqlctype <= ODBC32.SQL_C.SSHORT)
			{
				if (sqlctype <= ODBC32.SQL_C.SBIGINT)
				{
					if (sqlctype == ODBC32.SQL_C.UTINYINT)
					{
						return base.ReadByte(offset);
					}
					if (sqlctype == ODBC32.SQL_C.SBIGINT)
					{
						return base.ReadInt64(offset);
					}
				}
				else
				{
					if (sqlctype == ODBC32.SQL_C.SLONG)
					{
						return base.ReadInt32(offset);
					}
					if (sqlctype == ODBC32.SQL_C.SSHORT)
					{
						return base.ReadInt16(offset);
					}
				}
			}
			else if (sqlctype <= ODBC32.SQL_C.NUMERIC)
			{
				switch (sqlctype)
				{
				case ODBC32.SQL_C.GUID:
					return base.ReadGuid(offset);
				case (ODBC32.SQL_C)(-10):
				case (ODBC32.SQL_C)(-9):
					break;
				case ODBC32.SQL_C.WCHAR:
					if (cb == -3)
					{
						return base.PtrToStringUni(offset);
					}
					cb = Math.Min(cb / 2, (base.Length - 2) / 2);
					return base.PtrToStringUni(offset, cb);
				case ODBC32.SQL_C.BIT:
				{
					byte b = base.ReadByte(offset);
					return b > 0;
				}
				default:
					switch (sqlctype)
					{
					case ODBC32.SQL_C.BINARY:
					case ODBC32.SQL_C.CHAR:
						cb = Math.Min(cb, base.Length);
						return base.ReadBytes(offset, cb);
					case ODBC32.SQL_C.NUMERIC:
						return base.ReadNumeric(offset);
					}
					break;
				}
			}
			else
			{
				if (sqlctype == ODBC32.SQL_C.REAL)
				{
					return base.ReadSingle(offset);
				}
				if (sqlctype == ODBC32.SQL_C.DOUBLE)
				{
					return base.ReadDouble(offset);
				}
				switch (sqlctype)
				{
				case ODBC32.SQL_C.TYPE_DATE:
					return base.ReadDate(offset);
				case ODBC32.SQL_C.TYPE_TIME:
					return base.ReadTime(offset);
				case ODBC32.SQL_C.TYPE_TIMESTAMP:
					return base.ReadDateTime(offset);
				}
			}
			return null;
		}

		// Token: 0x060029E6 RID: 10726 RVA: 0x001155B4 File Offset: 0x001149B4
		internal void MarshalToNative(int offset, object value, ODBC32.SQL_C sqlctype, int sizeorprecision, int valueOffset)
		{
			if (sqlctype <= ODBC32.SQL_C.SSHORT)
			{
				if (sqlctype <= ODBC32.SQL_C.SBIGINT)
				{
					if (sqlctype == ODBC32.SQL_C.UTINYINT)
					{
						base.WriteByte(offset, (byte)value);
						return;
					}
					if (sqlctype != ODBC32.SQL_C.SBIGINT)
					{
						return;
					}
					base.WriteInt64(offset, (long)value);
					return;
				}
				else
				{
					if (sqlctype == ODBC32.SQL_C.SLONG)
					{
						base.WriteInt32(offset, (int)value);
						return;
					}
					if (sqlctype != ODBC32.SQL_C.SSHORT)
					{
						return;
					}
					base.WriteInt16(offset, (short)value);
					return;
				}
			}
			else
			{
				if (sqlctype <= ODBC32.SQL_C.NUMERIC)
				{
					switch (sqlctype)
					{
					case ODBC32.SQL_C.GUID:
						base.WriteGuid(offset, (Guid)value);
						return;
					case (ODBC32.SQL_C)(-10):
					case (ODBC32.SQL_C)(-9):
						break;
					case ODBC32.SQL_C.WCHAR:
					{
						int num;
						char[] array;
						if (value is string)
						{
							num = Math.Max(0, ((string)value).Length - valueOffset);
							if (sizeorprecision > 0 && sizeorprecision < num)
							{
								num = sizeorprecision;
							}
							array = ((string)value).ToCharArray(valueOffset, num);
							base.WriteCharArray(offset, array, 0, array.Length);
							base.WriteInt16(offset + array.Length * 2, 0);
							return;
						}
						num = Math.Max(0, ((char[])value).Length - valueOffset);
						if (sizeorprecision > 0 && sizeorprecision < num)
						{
							num = sizeorprecision;
						}
						array = (char[])value;
						base.WriteCharArray(offset, array, valueOffset, num);
						base.WriteInt16(offset + array.Length * 2, 0);
						return;
					}
					case ODBC32.SQL_C.BIT:
						base.WriteByte(offset, ((bool)value) ? 1 : 0);
						return;
					default:
						switch (sqlctype)
						{
						case ODBC32.SQL_C.BINARY:
						case ODBC32.SQL_C.CHAR:
						{
							byte[] array2 = (byte[])value;
							int num2 = array2.Length;
							num2 -= valueOffset;
							if (sizeorprecision > 0 && sizeorprecision < num2)
							{
								num2 = sizeorprecision;
							}
							base.WriteBytes(offset, array2, valueOffset, num2);
							return;
						}
						case (ODBC32.SQL_C)(-1):
						case (ODBC32.SQL_C)0:
							break;
						case ODBC32.SQL_C.NUMERIC:
							base.WriteNumeric(offset, (decimal)value, checked((byte)sizeorprecision));
							break;
						default:
							return;
						}
						break;
					}
					return;
				}
				if (sqlctype == ODBC32.SQL_C.REAL)
				{
					base.WriteSingle(offset, (float)value);
					return;
				}
				if (sqlctype == ODBC32.SQL_C.DOUBLE)
				{
					base.WriteDouble(offset, (double)value);
					return;
				}
				switch (sqlctype)
				{
				case ODBC32.SQL_C.TYPE_DATE:
					base.WriteDate(offset, (DateTime)value);
					return;
				case ODBC32.SQL_C.TYPE_TIME:
					base.WriteTime(offset, (TimeSpan)value);
					return;
				case ODBC32.SQL_C.TYPE_TIMESTAMP:
					this.WriteODBCDateTime(offset, (DateTime)value);
					return;
				default:
					return;
				}
			}
		}

		// Token: 0x060029E7 RID: 10727 RVA: 0x001157C0 File Offset: 0x00114BC0
		internal HandleRef PtrOffset(int offset, int length)
		{
			base.Validate(offset, length);
			IntPtr handle = ADP.IntPtrOffset(base.DangerousGetHandle(), offset);
			return new HandleRef(this, handle);
		}

		// Token: 0x060029E8 RID: 10728 RVA: 0x001157EC File Offset: 0x00114BEC
		internal void WriteODBCDateTime(int offset, DateTime value)
		{
			short[] source = new short[]
			{
				(short)value.Year,
				(short)value.Month,
				(short)value.Day,
				(short)value.Hour,
				(short)value.Minute,
				(short)value.Second
			};
			base.WriteInt16Array(offset, source, 0, 6);
			base.WriteInt32(offset + 12, value.Millisecond * 1000000);
		}
	}
}
