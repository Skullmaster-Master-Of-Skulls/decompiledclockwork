using System;
using System.Data.Common;
using System.Data.SqlTypes;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Data.OracleClient
{
	// Token: 0x0200006E RID: 110
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct OracleMonthSpan : IComparable, INullable
	{
		// Token: 0x06000551 RID: 1361 RVA: 0x0006C1B4 File Offset: 0x0006B5B4
		internal OracleMonthSpan(bool isNull)
		{
			this._value = int.MaxValue;
		}

		// Token: 0x06000552 RID: 1362 RVA: 0x0006C1D4 File Offset: 0x0006B5D4
		public OracleMonthSpan(int months)
		{
			this._value = months;
			OracleMonthSpan.AssertValid(this._value);
		}

		// Token: 0x06000553 RID: 1363 RVA: 0x0006C1F4 File Offset: 0x0006B5F4
		public OracleMonthSpan(int years, int months)
		{
			try
			{
				this._value = checked(years * 12 + months);
			}
			catch (OverflowException)
			{
				throw ADP.MonthOutOfRange();
			}
			OracleMonthSpan.AssertValid(this._value);
		}

		// Token: 0x06000554 RID: 1364 RVA: 0x0006C244 File Offset: 0x0006B644
		public OracleMonthSpan(OracleMonthSpan from)
		{
			this._value = from._value;
		}

		// Token: 0x06000555 RID: 1365 RVA: 0x0006C264 File Offset: 0x0006B664
		internal OracleMonthSpan(NativeBuffer buffer, int valueOffset)
		{
			this._value = OracleMonthSpan.MarshalToInt32(buffer, valueOffset);
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x06000556 RID: 1366 RVA: 0x0006C284 File Offset: 0x0006B684
		public bool IsNull
		{
			get
			{
				return int.MaxValue == this._value;
			}
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x06000557 RID: 1367 RVA: 0x0006C2A4 File Offset: 0x0006B6A4
		public int Value
		{
			get
			{
				if (this.IsNull)
				{
					throw ADP.DataIsNull();
				}
				return this._value;
			}
		}

		// Token: 0x06000558 RID: 1368 RVA: 0x0006C2D4 File Offset: 0x0006B6D4
		private static void AssertValid(int monthSpan)
		{
			if (monthSpan < -176556 || monthSpan > 176556)
			{
				throw ADP.MonthOutOfRange();
			}
		}

		// Token: 0x06000559 RID: 1369 RVA: 0x0006C304 File Offset: 0x0006B704
		public int CompareTo(object obj)
		{
			if (obj.GetType() != typeof(OracleMonthSpan))
			{
				throw ADP.WrongType(obj.GetType(), typeof(OracleMonthSpan));
			}
			OracleMonthSpan oracleMonthSpan = (OracleMonthSpan)obj;
			if (this.IsNull)
			{
				if (!oracleMonthSpan.IsNull)
				{
					return -1;
				}
				return 0;
			}
			else
			{
				if (oracleMonthSpan.IsNull)
				{
					return 1;
				}
				return this._value.CompareTo(oracleMonthSpan._value);
			}
		}

		// Token: 0x0600055A RID: 1370 RVA: 0x0006C374 File Offset: 0x0006B774
		public override bool Equals(object value)
		{
			return value is OracleMonthSpan && (this == (OracleMonthSpan)value).Value;
		}

		// Token: 0x0600055B RID: 1371 RVA: 0x0006C3A4 File Offset: 0x0006B7A4
		public override int GetHashCode()
		{
			if (!this.IsNull)
			{
				return this._value.GetHashCode();
			}
			return 0;
		}

		// Token: 0x0600055C RID: 1372 RVA: 0x0006C3D4 File Offset: 0x0006B7D4
		internal static int MarshalToInt32(NativeBuffer buffer, int valueOffset)
		{
			byte[] array = buffer.ReadBytes(valueOffset, 5);
			int num = (int)((long)((int)array[0] << 24 | (int)array[1] << 16 | (int)array[2] << 8 | (int)array[3]) - (long)((ulong)int.MinValue));
			int num2 = (int)(array[4] - 60);
			int num3 = num * 12 + num2;
			OracleMonthSpan.AssertValid(num3);
			return num3;
		}

		// Token: 0x0600055D RID: 1373 RVA: 0x0006C424 File Offset: 0x0006B824
		internal static int MarshalToNative(object value, NativeBuffer buffer, int offset)
		{
			int num;
			if (value is OracleMonthSpan)
			{
				num = ((OracleMonthSpan)value)._value;
			}
			else
			{
				num = (int)value;
			}
			byte[] array = new byte[5];
			int num2 = (int)((long)(num / 12) + (long)((ulong)int.MinValue));
			int num3 = num % 12;
			array[0] = (byte)(num2 >> 24);
			array[1] = (byte)(num2 >> 16 & 255);
			array[2] = (byte)(num2 >> 8 & 255);
			array[3] = (byte)(num2 & 255);
			array[4] = (byte)(num3 + 60);
			buffer.WriteBytes(offset, array, 0, 5);
			return 5;
		}

		// Token: 0x0600055E RID: 1374 RVA: 0x0006C4B4 File Offset: 0x0006B8B4
		public static OracleMonthSpan Parse(string s)
		{
			int months = int.Parse(s, CultureInfo.InvariantCulture);
			return new OracleMonthSpan(months);
		}

		// Token: 0x0600055F RID: 1375 RVA: 0x0006C4D4 File Offset: 0x0006B8D4
		public override string ToString()
		{
			if (this.IsNull)
			{
				return ADP.NullString;
			}
			return this.Value.ToString(CultureInfo.CurrentCulture);
		}

		// Token: 0x06000560 RID: 1376 RVA: 0x0006C504 File Offset: 0x0006B904
		public static OracleBoolean Equals(OracleMonthSpan x, OracleMonthSpan y)
		{
			return x == y;
		}

		// Token: 0x06000561 RID: 1377 RVA: 0x0006C524 File Offset: 0x0006B924
		public static OracleBoolean GreaterThan(OracleMonthSpan x, OracleMonthSpan y)
		{
			return x > y;
		}

		// Token: 0x06000562 RID: 1378 RVA: 0x0006C544 File Offset: 0x0006B944
		public static OracleBoolean GreaterThanOrEqual(OracleMonthSpan x, OracleMonthSpan y)
		{
			return x >= y;
		}

		// Token: 0x06000563 RID: 1379 RVA: 0x0006C564 File Offset: 0x0006B964
		public static OracleBoolean LessThan(OracleMonthSpan x, OracleMonthSpan y)
		{
			return x < y;
		}

		// Token: 0x06000564 RID: 1380 RVA: 0x0006C584 File Offset: 0x0006B984
		public static OracleBoolean LessThanOrEqual(OracleMonthSpan x, OracleMonthSpan y)
		{
			return x <= y;
		}

		// Token: 0x06000565 RID: 1381 RVA: 0x0006C5A4 File Offset: 0x0006B9A4
		public static OracleBoolean NotEquals(OracleMonthSpan x, OracleMonthSpan y)
		{
			return x != y;
		}

		// Token: 0x06000566 RID: 1382 RVA: 0x0006C5C4 File Offset: 0x0006B9C4
		public static explicit operator int(OracleMonthSpan x)
		{
			if (x.IsNull)
			{
				throw ADP.DataIsNull();
			}
			return x.Value;
		}

		// Token: 0x06000567 RID: 1383 RVA: 0x0006C5F4 File Offset: 0x0006B9F4
		public static explicit operator OracleMonthSpan(string x)
		{
			return OracleMonthSpan.Parse(x);
		}

		// Token: 0x06000568 RID: 1384 RVA: 0x0006C614 File Offset: 0x0006BA14
		public static OracleBoolean operator ==(OracleMonthSpan x, OracleMonthSpan y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new OracleBoolean(x.CompareTo(y) == 0);
			}
			return OracleBoolean.Null;
		}

		// Token: 0x06000569 RID: 1385 RVA: 0x0006C654 File Offset: 0x0006BA54
		public static OracleBoolean operator >(OracleMonthSpan x, OracleMonthSpan y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new OracleBoolean(x.CompareTo(y) > 0);
			}
			return OracleBoolean.Null;
		}

		// Token: 0x0600056A RID: 1386 RVA: 0x0006C694 File Offset: 0x0006BA94
		public static OracleBoolean operator >=(OracleMonthSpan x, OracleMonthSpan y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new OracleBoolean(x.CompareTo(y) >= 0);
			}
			return OracleBoolean.Null;
		}

		// Token: 0x0600056B RID: 1387 RVA: 0x0006C6D4 File Offset: 0x0006BAD4
		public static OracleBoolean operator <(OracleMonthSpan x, OracleMonthSpan y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new OracleBoolean(x.CompareTo(y) < 0);
			}
			return OracleBoolean.Null;
		}

		// Token: 0x0600056C RID: 1388 RVA: 0x0006C714 File Offset: 0x0006BB14
		public static OracleBoolean operator <=(OracleMonthSpan x, OracleMonthSpan y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new OracleBoolean(x.CompareTo(y) <= 0);
			}
			return OracleBoolean.Null;
		}

		// Token: 0x0600056D RID: 1389 RVA: 0x0006C754 File Offset: 0x0006BB54
		public static OracleBoolean operator !=(OracleMonthSpan x, OracleMonthSpan y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new OracleBoolean(x.CompareTo(y) != 0);
			}
			return OracleBoolean.Null;
		}

		// Token: 0x04000463 RID: 1123
		private const int MaxMonth = 176556;

		// Token: 0x04000464 RID: 1124
		private const int MinMonth = -176556;

		// Token: 0x04000465 RID: 1125
		private const int NullValue = 2147483647;

		// Token: 0x04000466 RID: 1126
		private int _value;

		// Token: 0x04000467 RID: 1127
		public static readonly OracleMonthSpan MaxValue = new OracleMonthSpan(176556);

		// Token: 0x04000468 RID: 1128
		public static readonly OracleMonthSpan MinValue = new OracleMonthSpan(-176556);

		// Token: 0x04000469 RID: 1129
		public static readonly OracleMonthSpan Null = new OracleMonthSpan(true);
	}
}
