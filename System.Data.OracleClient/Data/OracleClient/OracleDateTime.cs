using System;
using System.Data.Common;
using System.Data.SqlTypes;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Data.OracleClient
{
	// Token: 0x02000063 RID: 99
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct OracleDateTime : IComparable, INullable
	{
		// Token: 0x06000464 RID: 1124 RVA: 0x000680B4 File Offset: 0x000674B4
		private OracleDateTime(bool isNull)
		{
			this._value = null;
		}

		// Token: 0x06000465 RID: 1125 RVA: 0x000680D4 File Offset: 0x000674D4
		public OracleDateTime(DateTime dt)
		{
			this._value = new byte[11];
			OracleDateTime.Pack(this._value, dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second, (int)(dt.Ticks % 10000000L) * 100);
		}

		// Token: 0x06000466 RID: 1126 RVA: 0x00068144 File Offset: 0x00067544
		public OracleDateTime(long ticks)
		{
			this._value = new byte[11];
			DateTime dateTime = new DateTime(ticks);
			OracleDateTime.Pack(this._value, dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, dateTime.Second, (int)(dateTime.Ticks % 10000000L) * 100);
		}

		// Token: 0x06000467 RID: 1127 RVA: 0x000681B4 File Offset: 0x000675B4
		public OracleDateTime(int year, int month, int day)
		{
			this = new OracleDateTime(year, month, day, 0, 0, 0, 0);
		}

		// Token: 0x06000468 RID: 1128 RVA: 0x000681D4 File Offset: 0x000675D4
		public OracleDateTime(int year, int month, int day, Calendar calendar)
		{
			this = new OracleDateTime(year, month, day, 0, 0, 0, 0, calendar);
		}

		// Token: 0x06000469 RID: 1129 RVA: 0x000681F4 File Offset: 0x000675F4
		public OracleDateTime(int year, int month, int day, int hour, int minute, int second)
		{
			this = new OracleDateTime(year, month, day, hour, minute, second, 0);
		}

		// Token: 0x0600046A RID: 1130 RVA: 0x00068214 File Offset: 0x00067614
		public OracleDateTime(int year, int month, int day, int hour, int minute, int second, Calendar calendar)
		{
			this = new OracleDateTime(year, month, day, hour, minute, second, 0, calendar);
		}

		// Token: 0x0600046B RID: 1131 RVA: 0x00068234 File Offset: 0x00067634
		public OracleDateTime(int year, int month, int day, int hour, int minute, int second, int millisecond)
		{
			this._value = new byte[11];
			new DateTime((year < 0) ? 0 : year, month, (year < 0) ? 1 : day, hour, minute, second, millisecond);
			OracleDateTime.Pack(this._value, year, month, day, hour, minute, second, (int)((long)millisecond * 10000L) * 100);
		}

		// Token: 0x0600046C RID: 1132 RVA: 0x00068294 File Offset: 0x00067694
		public OracleDateTime(int year, int month, int day, int hour, int minute, int second, int millisecond, Calendar calendar)
		{
			this._value = new byte[11];
			DateTime dateTime = new DateTime(year, month, day, hour, minute, second, millisecond, calendar);
			OracleDateTime.Pack(this._value, dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, dateTime.Second, (int)(dateTime.Ticks % 10000000L) * 100);
		}

		// Token: 0x0600046D RID: 1133 RVA: 0x00068314 File Offset: 0x00067714
		public OracleDateTime(OracleDateTime from)
		{
			this._value = new byte[from._value.Length];
			from._value.CopyTo(this._value, 0);
		}

		// Token: 0x0600046E RID: 1134 RVA: 0x00068354 File Offset: 0x00067754
		internal OracleDateTime(NativeBuffer buffer, int valueOffset, int lengthOffset, MetaType metaType, OracleConnection connection)
		{
			this._value = OracleDateTime.GetBytesFromBuffer(buffer, valueOffset, lengthOffset, metaType, connection);
		}

		// Token: 0x0600046F RID: 1135 RVA: 0x00068374 File Offset: 0x00067774
		internal OracleDateTime(OciDateTimeDescriptor dateTimeDescriptor, MetaType metaType, OracleConnection connection)
		{
			this._value = OracleDateTime.GetBytesFromDescriptor(dateTimeDescriptor, metaType, connection);
		}

		// Token: 0x06000470 RID: 1136 RVA: 0x00068394 File Offset: 0x00067794
		private static void Pack(byte[] dateval, int year, int month, int day, int hour, int minute, int second, int fsecs)
		{
			dateval[0] = (byte)(year / 100 + 100);
			dateval[1] = (byte)(year % 100 + 100);
			dateval[2] = (byte)month;
			dateval[3] = (byte)day;
			dateval[4] = (byte)(hour + 1);
			dateval[5] = (byte)(minute + 1);
			dateval[6] = (byte)(second + 1);
			dateval[7] = (byte)(fsecs >> 24);
			dateval[8] = (byte)(fsecs >> 16 & 255);
			dateval[9] = (byte)(fsecs >> 8 & 255);
			dateval[10] = (byte)(fsecs & 255);
		}

		// Token: 0x06000471 RID: 1137 RVA: 0x00068414 File Offset: 0x00067814
		private static int Unpack(byte[] dateval, out int year, out int month, out int day, out int hour, out int minute, out int second, out int fsec)
		{
			year = (int)((dateval[0] - 100) * 100 + (dateval[1] - 100));
			month = (int)dateval[2];
			day = (int)dateval[3];
			hour = (int)(dateval[4] - 1);
			minute = (int)(dateval[5] - 1);
			second = (int)(dateval[6] - 1);
			int minutes;
			int hours;
			if (7 == dateval.Length)
			{
				hours = (fsec = (minutes = 0));
			}
			else
			{
				fsec = ((int)dateval[7] << 24 | (int)dateval[8] << 16 | (int)dateval[9] << 8 | (int)dateval[10]);
				if (11 == dateval.Length)
				{
					minutes = (hours = 0);
				}
				else
				{
					hours = (int)(dateval[11] - 20);
					minutes = (int)(dateval[12] - 60);
				}
			}
			if (13 == dateval.Length)
			{
				DateTime dateTime = new DateTime(year, month, day, hour, minute, second) + new TimeSpan(hours, minutes, 0);
				year = dateTime.Year;
				month = dateTime.Month;
				day = dateTime.Day;
				hour = dateTime.Hour;
				minute = dateTime.Minute;
			}
			return dateval.Length;
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x06000472 RID: 1138 RVA: 0x00068504 File Offset: 0x00067904
		public bool IsNull
		{
			get
			{
				return null == this._value;
			}
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x06000473 RID: 1139 RVA: 0x00068524 File Offset: 0x00067924
		public DateTime Value
		{
			get
			{
				if (this.IsNull)
				{
					throw ADP.DataIsNull();
				}
				return OracleDateTime.ToDateTime(this._value);
			}
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x06000474 RID: 1140 RVA: 0x00068554 File Offset: 0x00067954
		public int Year
		{
			get
			{
				if (this.IsNull)
				{
					throw ADP.DataIsNull();
				}
				int result;
				int num;
				int num2;
				int num3;
				int num4;
				int num5;
				int num6;
				OracleDateTime.Unpack(this._value, out result, out num, out num2, out num3, out num4, out num5, out num6);
				return result;
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x06000475 RID: 1141 RVA: 0x00068594 File Offset: 0x00067994
		public int Month
		{
			get
			{
				if (this.IsNull)
				{
					throw ADP.DataIsNull();
				}
				int num;
				int result;
				int num2;
				int num3;
				int num4;
				int num5;
				int num6;
				OracleDateTime.Unpack(this._value, out num, out result, out num2, out num3, out num4, out num5, out num6);
				return result;
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x06000476 RID: 1142 RVA: 0x000685D4 File Offset: 0x000679D4
		public int Day
		{
			get
			{
				if (this.IsNull)
				{
					throw ADP.DataIsNull();
				}
				int num;
				int num2;
				int result;
				int num3;
				int num4;
				int num5;
				int num6;
				OracleDateTime.Unpack(this._value, out num, out num2, out result, out num3, out num4, out num5, out num6);
				return result;
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x06000477 RID: 1143 RVA: 0x00068614 File Offset: 0x00067A14
		public int Hour
		{
			get
			{
				if (this.IsNull)
				{
					throw ADP.DataIsNull();
				}
				int num;
				int num2;
				int num3;
				int result;
				int num4;
				int num5;
				int num6;
				OracleDateTime.Unpack(this._value, out num, out num2, out num3, out result, out num4, out num5, out num6);
				return result;
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x06000478 RID: 1144 RVA: 0x00068654 File Offset: 0x00067A54
		public int Minute
		{
			get
			{
				if (this.IsNull)
				{
					throw ADP.DataIsNull();
				}
				int num;
				int num2;
				int num3;
				int num4;
				int result;
				int num5;
				int num6;
				OracleDateTime.Unpack(this._value, out num, out num2, out num3, out num4, out result, out num5, out num6);
				return result;
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x06000479 RID: 1145 RVA: 0x00068694 File Offset: 0x00067A94
		public int Second
		{
			get
			{
				if (this.IsNull)
				{
					throw ADP.DataIsNull();
				}
				int num;
				int num2;
				int num3;
				int num4;
				int num5;
				int result;
				int num6;
				OracleDateTime.Unpack(this._value, out num, out num2, out num3, out num4, out num5, out result, out num6);
				return result;
			}
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x0600047A RID: 1146 RVA: 0x000686D4 File Offset: 0x00067AD4
		public int Millisecond
		{
			get
			{
				if (this.IsNull)
				{
					throw ADP.DataIsNull();
				}
				int num;
				int num2;
				int num3;
				int num4;
				int num5;
				int num6;
				int num7;
				OracleDateTime.Unpack(this._value, out num, out num2, out num3, out num4, out num5, out num6, out num7);
				return (int)((long)(num7 / 100) / 10000L);
			}
		}

		// Token: 0x0600047B RID: 1147 RVA: 0x00068724 File Offset: 0x00067B24
		public int CompareTo(object obj)
		{
			if (obj.GetType() != typeof(OracleDateTime))
			{
				throw ADP.WrongType(obj.GetType(), typeof(OracleDateTime));
			}
			OracleDateTime oracleDateTime = (OracleDateTime)obj;
			if (this.IsNull)
			{
				if (!oracleDateTime.IsNull)
				{
					return -1;
				}
				return 0;
			}
			else
			{
				if (oracleDateTime.IsNull)
				{
					return 1;
				}
				int num;
				int num2;
				int num3;
				int num4;
				int num5;
				int num6;
				int num7;
				OracleDateTime.Unpack(this._value, out num, out num2, out num3, out num4, out num5, out num6, out num7);
				int num8;
				int num9;
				int num10;
				int num11;
				int num12;
				int num13;
				int num14;
				OracleDateTime.Unpack(oracleDateTime._value, out num8, out num9, out num10, out num11, out num12, out num13, out num14);
				int num15 = num - num8;
				if (num15 != 0)
				{
					return num15;
				}
				num15 = num2 - num9;
				if (num15 != 0)
				{
					return num15;
				}
				num15 = num3 - num10;
				if (num15 != 0)
				{
					return num15;
				}
				num15 = num4 - num11;
				if (num15 != 0)
				{
					return num15;
				}
				num15 = num5 - num12;
				if (num15 != 0)
				{
					return num15;
				}
				num15 = num6 - num13;
				if (num15 != 0)
				{
					return num15;
				}
				num15 = num7 - num14;
				if (num15 != 0)
				{
					return num15;
				}
				return 0;
			}
		}

		// Token: 0x0600047C RID: 1148 RVA: 0x00068804 File Offset: 0x00067C04
		public override bool Equals(object value)
		{
			return value is OracleDateTime && (this == (OracleDateTime)value).Value;
		}

		// Token: 0x0600047D RID: 1149 RVA: 0x00068834 File Offset: 0x00067C34
		internal static byte[] GetBytesFromDescriptor(OciDateTimeDescriptor dateTimeDescriptor, MetaType metaType, OracleConnection connection)
		{
			OCI.DATATYPE ociType = metaType.OciType;
			OCI.DATATYPE datatype = ociType;
			uint num;
			if (datatype != OCI.DATATYPE.INT_TIMESTAMP)
			{
				if (datatype != OCI.DATATYPE.INT_TIMESTAMP_LTZ)
				{
					num = 13U;
				}
				else
				{
					num = 13U;
				}
			}
			else
			{
				num = 11U;
			}
			byte[] array = new byte[num];
			uint num2 = num;
			OciIntervalDescriptor reftz = new OciIntervalDescriptor(connection.EnvironmentHandle);
			int num3 = UnsafeNativeMethods.OCIDateTimeToArray(connection.EnvironmentHandle, connection.ErrorHandle, dateTimeDescriptor, reftz, array, ref num2, 9);
			if (num3 != 0)
			{
				connection.CheckError(connection.ErrorHandle, num3);
			}
			if (OCI.DATATYPE.INT_TIMESTAMP_LTZ == ociType)
			{
				TimeSpan serverTimeZoneAdjustmentToUTC = connection.ServerTimeZoneAdjustmentToUTC;
				array[11] = (byte)(serverTimeZoneAdjustmentToUTC.Hours + 20);
				array[12] = (byte)(serverTimeZoneAdjustmentToUTC.Minutes + 60);
			}
			else if (OCI.DATATYPE.INT_TIMESTAMP_TZ == ociType)
			{
				sbyte b;
				sbyte b2;
				num3 = UnsafeNativeMethods.OCIDateTimeGetTimeZoneOffset(connection.EnvironmentHandle, connection.ErrorHandle, dateTimeDescriptor, out b, out b2);
				if (num3 != 0)
				{
					connection.CheckError(connection.ErrorHandle, num3);
				}
				array[11] = (byte)(b + 20);
				array[12] = (byte)(b2 + 60);
			}
			return array;
		}

		// Token: 0x0600047E RID: 1150 RVA: 0x00068924 File Offset: 0x00067D24
		internal static byte[] GetBytesFromBuffer(NativeBuffer buffer, int valueOffset, int lengthOffset, MetaType metaType, OracleConnection connection)
		{
			OCI.DATATYPE ociType = metaType.OciType;
			short length = buffer.ReadInt16(lengthOffset);
			OCI.DATATYPE datatype = ociType;
			uint num;
			if (datatype != OCI.DATATYPE.DATE)
			{
				if (datatype != OCI.DATATYPE.INT_TIMESTAMP)
				{
					if (datatype != OCI.DATATYPE.INT_TIMESTAMP_LTZ)
					{
						num = 13U;
					}
					else
					{
						num = 13U;
					}
				}
				else
				{
					num = 11U;
				}
			}
			else
			{
				num = 7U;
			}
			byte[] array = new byte[num];
			buffer.ReadBytes(valueOffset, array, 0, (int)length);
			if (OCI.DATATYPE.INT_TIMESTAMP_LTZ == ociType)
			{
				TimeSpan serverTimeZoneAdjustmentToUTC = connection.ServerTimeZoneAdjustmentToUTC;
				array[11] = (byte)(serverTimeZoneAdjustmentToUTC.Hours + 20);
				array[12] = (byte)(serverTimeZoneAdjustmentToUTC.Minutes + 60);
			}
			else if (OCI.DATATYPE.INT_TIMESTAMP_TZ == ociType && 128 < array[11])
			{
				OciIntervalDescriptor reftz = new OciIntervalDescriptor(connection.EnvironmentHandle);
				OciDateTimeDescriptor datetime = new OciDateTimeDescriptor(connection.EnvironmentHandle, OCI.HTYPE.OCI_DTYPE_TIMESTAMP_TZ);
				int num2 = UnsafeNativeMethods.OCIDateTimeFromArray(connection.EnvironmentHandle, connection.ErrorHandle, array, num, 188, datetime, reftz, 0);
				if (num2 != 0)
				{
					connection.CheckError(connection.ErrorHandle, num2);
				}
				sbyte b;
				sbyte b2;
				num2 = UnsafeNativeMethods.OCIDateTimeGetTimeZoneOffset(connection.EnvironmentHandle, connection.ErrorHandle, datetime, out b, out b2);
				if (num2 != 0)
				{
					connection.CheckError(connection.ErrorHandle, num2);
				}
				array[11] = (byte)(b + 20);
				array[12] = (byte)(b2 + 60);
			}
			return array;
		}

		// Token: 0x0600047F RID: 1151 RVA: 0x00068A64 File Offset: 0x00067E64
		public override int GetHashCode()
		{
			return this.IsNull ? 0 : this._value.GetHashCode();
		}

		// Token: 0x06000480 RID: 1152 RVA: 0x00068A94 File Offset: 0x00067E94
		internal static DateTime MarshalToDateTime(NativeBuffer buffer, int valueOffset, int lengthOffset, MetaType metaType, OracleConnection connection)
		{
			byte[] bytesFromBuffer = OracleDateTime.GetBytesFromBuffer(buffer, valueOffset, lengthOffset, metaType, connection);
			return OracleDateTime.ToDateTime(bytesFromBuffer);
		}

		// Token: 0x06000481 RID: 1153 RVA: 0x00068AC4 File Offset: 0x00067EC4
		internal static int MarshalDateToNative(object value, NativeBuffer buffer, int offset, OCI.DATATYPE ociType, OracleConnection connection)
		{
			byte[] array;
			if (value is OracleDateTime)
			{
				array = ((OracleDateTime)value)._value;
			}
			else
			{
				DateTime dateTime = (DateTime)value;
				array = new byte[11];
				OracleDateTime.Pack(array, dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, dateTime.Second, 0);
			}
			int num = 7;
			buffer.WriteBytes(offset, array, 0, num);
			return num;
		}

		// Token: 0x06000482 RID: 1154 RVA: 0x00068B34 File Offset: 0x00067F34
		internal static DateTime MarshalTimestampToDateTime(OciDateTimeDescriptor dateTimeDescriptor, MetaType metaType, OracleConnection connection)
		{
			byte[] bytesFromDescriptor = OracleDateTime.GetBytesFromDescriptor(dateTimeDescriptor, metaType, connection);
			return OracleDateTime.ToDateTime(bytesFromDescriptor);
		}

		// Token: 0x06000483 RID: 1155 RVA: 0x00068B54 File Offset: 0x00067F54
		internal static OciDateTimeDescriptor CreateEmptyDescriptor(OCI.DATATYPE ociType, OracleConnection connection)
		{
			OCI.HTYPE dateTimeType;
			switch (ociType)
			{
			case OCI.DATATYPE.INT_TIMESTAMP:
				dateTimeType = OCI.HTYPE.OCI_DTYPE_TIMESTAMP;
				break;
			case OCI.DATATYPE.INT_TIMESTAMP_TZ:
				dateTimeType = OCI.HTYPE.OCI_DTYPE_TIMESTAMP_TZ;
				break;
			default:
				if (ociType != OCI.DATATYPE.INT_TIMESTAMP_LTZ)
				{
				}
				dateTimeType = OCI.HTYPE.OCI_DTYPE_TIMESTAMP_LTZ;
				break;
			}
			return new OciDateTimeDescriptor(connection.EnvironmentHandle, dateTimeType);
		}

		// Token: 0x06000484 RID: 1156 RVA: 0x00068BA4 File Offset: 0x00067FA4
		internal static OciDateTimeDescriptor CreateDescriptor(OCI.DATATYPE ociType, OracleConnection connection, object value)
		{
			byte[] array;
			if (value is OracleDateTime)
			{
				array = ((OracleDateTime)value)._value;
			}
			else
			{
				DateTime dt = (DateTime)value;
				OracleDateTime oracleDateTime = new OracleDateTime(dt);
				array = oracleDateTime._value;
			}
			OCI.DATATYPE datatype;
			switch (ociType)
			{
			case OCI.DATATYPE.INT_TIMESTAMP:
				datatype = OCI.DATATYPE.TIMESTAMP;
				goto IL_AE;
			case OCI.DATATYPE.INT_TIMESTAMP_TZ:
				break;
			default:
				if (ociType == OCI.DATATYPE.INT_TIMESTAMP_LTZ)
				{
					datatype = OCI.DATATYPE.TIMESTAMP_LTZ;
					goto IL_AE;
				}
				break;
			}
			datatype = OCI.DATATYPE.TIMESTAMP_TZ;
			TimeSpan serverTimeZoneAdjustmentToUTC = connection.ServerTimeZoneAdjustmentToUTC;
			if (array.Length < 13)
			{
				byte[] array2 = new byte[13];
				Buffer.BlockCopy(array, 0, array2, 0, array.Length);
				array = array2;
				array[11] = (byte)(20 + serverTimeZoneAdjustmentToUTC.Hours);
				array[12] = (byte)(60 + serverTimeZoneAdjustmentToUTC.Minutes);
			}
			IL_AE:
			OciDateTimeDescriptor ociDateTimeDescriptor = OracleDateTime.CreateEmptyDescriptor(ociType, connection);
			OciIntervalDescriptor reftz = new OciIntervalDescriptor(connection.EnvironmentHandle);
			int num = UnsafeNativeMethods.OCIDateTimeFromArray(connection.EnvironmentHandle, connection.ErrorHandle, array, (uint)array.Length, (byte)datatype, ociDateTimeDescriptor, reftz, 9);
			if (num != 0)
			{
				connection.CheckError(connection.ErrorHandle, num);
			}
			return ociDateTimeDescriptor;
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x06000485 RID: 1157 RVA: 0x00068CA4 File Offset: 0x000680A4
		internal bool HasTimeZoneInfo
		{
			get
			{
				return this._value != null && this._value.Length >= 13;
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x06000486 RID: 1158 RVA: 0x00068CD4 File Offset: 0x000680D4
		internal bool HasTimeInfo
		{
			get
			{
				return this._value != null && this._value.Length >= 11;
			}
		}

		// Token: 0x06000487 RID: 1159 RVA: 0x00068D04 File Offset: 0x00068104
		public static OracleDateTime Parse(string s)
		{
			DateTime dt = DateTime.Parse(s, null);
			return new OracleDateTime(dt);
		}

		// Token: 0x06000488 RID: 1160 RVA: 0x00068D24 File Offset: 0x00068124
		private static DateTime ToDateTime(byte[] rawValue)
		{
			int year;
			int month;
			int day;
			int hour;
			int minute;
			int second;
			int num2;
			int num = OracleDateTime.Unpack(rawValue, out year, out month, out day, out hour, out minute, out second, out num2);
			DateTime result = new DateTime(year, month, day, hour, minute, second);
			if (num > 7 && num2 > 100)
			{
				result = result.AddTicks((long)num2 / 100L);
			}
			return result;
		}

		// Token: 0x06000489 RID: 1161 RVA: 0x00068D74 File Offset: 0x00068174
		public override string ToString()
		{
			if (this.IsNull)
			{
				return ADP.NullString;
			}
			return this.Value.ToString(null);
		}

		// Token: 0x0600048A RID: 1162 RVA: 0x00068DA4 File Offset: 0x000681A4
		public static OracleBoolean Equals(OracleDateTime x, OracleDateTime y)
		{
			return x == y;
		}

		// Token: 0x0600048B RID: 1163 RVA: 0x00068DC4 File Offset: 0x000681C4
		public static OracleBoolean GreaterThan(OracleDateTime x, OracleDateTime y)
		{
			return x > y;
		}

		// Token: 0x0600048C RID: 1164 RVA: 0x00068DE4 File Offset: 0x000681E4
		public static OracleBoolean GreaterThanOrEqual(OracleDateTime x, OracleDateTime y)
		{
			return x >= y;
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x00068E04 File Offset: 0x00068204
		public static OracleBoolean LessThan(OracleDateTime x, OracleDateTime y)
		{
			return x < y;
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x00068E24 File Offset: 0x00068224
		public static OracleBoolean LessThanOrEqual(OracleDateTime x, OracleDateTime y)
		{
			return x <= y;
		}

		// Token: 0x0600048F RID: 1167 RVA: 0x00068E44 File Offset: 0x00068244
		public static OracleBoolean NotEquals(OracleDateTime x, OracleDateTime y)
		{
			return x != y;
		}

		// Token: 0x06000490 RID: 1168 RVA: 0x00068E64 File Offset: 0x00068264
		public static explicit operator DateTime(OracleDateTime x)
		{
			if (x.IsNull)
			{
				throw ADP.DataIsNull();
			}
			return x.Value;
		}

		// Token: 0x06000491 RID: 1169 RVA: 0x00068E94 File Offset: 0x00068294
		public static explicit operator OracleDateTime(string x)
		{
			return OracleDateTime.Parse(x);
		}

		// Token: 0x06000492 RID: 1170 RVA: 0x00068EB4 File Offset: 0x000682B4
		public static OracleBoolean operator ==(OracleDateTime x, OracleDateTime y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new OracleBoolean(x.CompareTo(y) == 0);
			}
			return OracleBoolean.Null;
		}

		// Token: 0x06000493 RID: 1171 RVA: 0x00068EF4 File Offset: 0x000682F4
		public static OracleBoolean operator >(OracleDateTime x, OracleDateTime y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new OracleBoolean(x.CompareTo(y) > 0);
			}
			return OracleBoolean.Null;
		}

		// Token: 0x06000494 RID: 1172 RVA: 0x00068F34 File Offset: 0x00068334
		public static OracleBoolean operator >=(OracleDateTime x, OracleDateTime y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new OracleBoolean(x.CompareTo(y) >= 0);
			}
			return OracleBoolean.Null;
		}

		// Token: 0x06000495 RID: 1173 RVA: 0x00068F74 File Offset: 0x00068374
		public static OracleBoolean operator <(OracleDateTime x, OracleDateTime y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new OracleBoolean(x.CompareTo(y) < 0);
			}
			return OracleBoolean.Null;
		}

		// Token: 0x06000496 RID: 1174 RVA: 0x00068FB4 File Offset: 0x000683B4
		public static OracleBoolean operator <=(OracleDateTime x, OracleDateTime y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new OracleBoolean(x.CompareTo(y) <= 0);
			}
			return OracleBoolean.Null;
		}

		// Token: 0x06000497 RID: 1175 RVA: 0x00068FF4 File Offset: 0x000683F4
		public static OracleBoolean operator !=(OracleDateTime x, OracleDateTime y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new OracleBoolean(x.CompareTo(y) != 0);
			}
			return OracleBoolean.Null;
		}

		// Token: 0x04000421 RID: 1057
		private const int MaxOracleFSecPrecision = 9;

		// Token: 0x04000422 RID: 1058
		private const byte x_DATE_Length = 7;

		// Token: 0x04000423 RID: 1059
		private const byte x_TIMESTAMP_Length = 11;

		// Token: 0x04000424 RID: 1060
		private const byte x_TIMESTAMP_WITH_TIMEZONE_Length = 13;

		// Token: 0x04000425 RID: 1061
		private const int FractionalSecondsPerTick = 100;

		// Token: 0x04000426 RID: 1062
		private byte[] _value;

		// Token: 0x04000427 RID: 1063
		public static readonly OracleDateTime MaxValue = new OracleDateTime(DateTime.MaxValue);

		// Token: 0x04000428 RID: 1064
		public static readonly OracleDateTime MinValue = new OracleDateTime(DateTime.MinValue);

		// Token: 0x04000429 RID: 1065
		public static readonly OracleDateTime Null = new OracleDateTime(true);
	}
}
