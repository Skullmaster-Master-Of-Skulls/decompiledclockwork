using System;
using System.Data.Common;
using System.Data.SqlTypes;
using System.Runtime.InteropServices;

namespace System.Data.OracleClient
{
	// Token: 0x0200007D RID: 125
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct OracleTimeSpan : IComparable, INullable
	{
		// Token: 0x060006B7 RID: 1719 RVA: 0x00072BB4 File Offset: 0x00071FB4
		private OracleTimeSpan(bool isNull)
		{
			this._value = null;
		}

		// Token: 0x060006B8 RID: 1720 RVA: 0x00072BD4 File Offset: 0x00071FD4
		public OracleTimeSpan(TimeSpan ts)
		{
			this._value = new byte[11];
			OracleTimeSpan.Pack(this._value, ts.Days, ts.Hours, ts.Minutes, ts.Seconds, (int)(ts.Ticks % 10000000L) * 100);
		}

		// Token: 0x060006B9 RID: 1721 RVA: 0x00072C34 File Offset: 0x00072034
		public OracleTimeSpan(long ticks)
		{
			this._value = new byte[11];
			TimeSpan timeSpan = new TimeSpan(ticks);
			OracleTimeSpan.Pack(this._value, timeSpan.Days, timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds, (int)(timeSpan.Ticks % 10000000L) * 100);
		}

		// Token: 0x060006BA RID: 1722 RVA: 0x00072C94 File Offset: 0x00072094
		public OracleTimeSpan(int hours, int minutes, int seconds)
		{
			this = new OracleTimeSpan(0, hours, minutes, seconds, 0);
		}

		// Token: 0x060006BB RID: 1723 RVA: 0x00072CB4 File Offset: 0x000720B4
		public OracleTimeSpan(int days, int hours, int minutes, int seconds)
		{
			this = new OracleTimeSpan(days, hours, minutes, seconds, 0);
		}

		// Token: 0x060006BC RID: 1724 RVA: 0x00072CD4 File Offset: 0x000720D4
		public OracleTimeSpan(int days, int hours, int minutes, int seconds, int milliseconds)
		{
			this._value = new byte[11];
			OracleTimeSpan.Pack(this._value, days, hours, minutes, seconds, (int)((long)milliseconds * 10000L) * 100);
		}

		// Token: 0x060006BD RID: 1725 RVA: 0x00072D14 File Offset: 0x00072114
		public OracleTimeSpan(OracleTimeSpan from)
		{
			this._value = new byte[from._value.Length];
			from._value.CopyTo(this._value, 0);
		}

		// Token: 0x060006BE RID: 1726 RVA: 0x00072D54 File Offset: 0x00072154
		internal OracleTimeSpan(NativeBuffer buffer, int valueOffset)
		{
			this = new OracleTimeSpan(true);
			this._value = buffer.ReadBytes(valueOffset, 11);
		}

		// Token: 0x060006BF RID: 1727 RVA: 0x00072D84 File Offset: 0x00072184
		private static void Pack(byte[] spanval, int days, int hours, int minutes, int seconds, int fsecs)
		{
			days = (int)((long)days + (long)((ulong)int.MinValue));
			fsecs = (int)((long)fsecs + (long)((ulong)int.MinValue));
			spanval[0] = (byte)(days >> 24);
			spanval[1] = (byte)(days >> 16 & 255);
			spanval[2] = (byte)(days >> 8 & 255);
			spanval[3] = (byte)(days & 255);
			spanval[4] = (byte)(hours + 60);
			spanval[5] = (byte)(minutes + 60);
			spanval[6] = (byte)(seconds + 60);
			spanval[7] = (byte)(fsecs >> 24);
			spanval[8] = (byte)(fsecs >> 16 & 255);
			spanval[9] = (byte)(fsecs >> 8 & 255);
			spanval[10] = (byte)(fsecs & 255);
		}

		// Token: 0x060006C0 RID: 1728 RVA: 0x00072E34 File Offset: 0x00072234
		private static void Unpack(byte[] spanval, out int days, out int hours, out int minutes, out int seconds, out int fsecs)
		{
			days = (int)((long)((int)spanval[0] << 24 | (int)spanval[1] << 16 | (int)spanval[2] << 8 | (int)spanval[3]) - (long)((ulong)int.MinValue));
			hours = (int)(spanval[4] - 60);
			minutes = (int)(spanval[5] - 60);
			seconds = (int)(spanval[6] - 60);
			fsecs = (int)((long)((int)spanval[7] << 24 | (int)spanval[8] << 16 | (int)spanval[9] << 8 | (int)spanval[10]) - (long)((ulong)int.MinValue));
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x060006C1 RID: 1729 RVA: 0x00072EA4 File Offset: 0x000722A4
		public bool IsNull
		{
			get
			{
				return null == this._value;
			}
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x060006C2 RID: 1730 RVA: 0x00072EC4 File Offset: 0x000722C4
		public TimeSpan Value
		{
			get
			{
				if (this.IsNull)
				{
					throw ADP.DataIsNull();
				}
				return OracleTimeSpan.ToTimeSpan(this._value);
			}
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x060006C3 RID: 1731 RVA: 0x00072EF4 File Offset: 0x000722F4
		public int Days
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
				OracleTimeSpan.Unpack(this._value, out result, out num, out num2, out num3, out num4);
				return result;
			}
		}

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x060006C4 RID: 1732 RVA: 0x00072F34 File Offset: 0x00072334
		public int Hours
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
				OracleTimeSpan.Unpack(this._value, out num, out result, out num2, out num3, out num4);
				return result;
			}
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x060006C5 RID: 1733 RVA: 0x00072F74 File Offset: 0x00072374
		public int Minutes
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
				OracleTimeSpan.Unpack(this._value, out num, out num2, out result, out num3, out num4);
				return result;
			}
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x060006C6 RID: 1734 RVA: 0x00072FB4 File Offset: 0x000723B4
		public int Seconds
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
				OracleTimeSpan.Unpack(this._value, out num, out num2, out num3, out result, out num4);
				return result;
			}
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x060006C7 RID: 1735 RVA: 0x00072FF4 File Offset: 0x000723F4
		public int Milliseconds
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
				OracleTimeSpan.Unpack(this._value, out num, out num2, out num3, out num4, out num5);
				return (int)((long)(num5 / 100) / 10000L);
			}
		}

		// Token: 0x060006C8 RID: 1736 RVA: 0x00073034 File Offset: 0x00072434
		public int CompareTo(object obj)
		{
			if (obj.GetType() != typeof(OracleTimeSpan))
			{
				throw ADP.WrongType(obj.GetType(), typeof(OracleTimeSpan));
			}
			OracleTimeSpan oracleTimeSpan = (OracleTimeSpan)obj;
			if (this.IsNull)
			{
				if (!oracleTimeSpan.IsNull)
				{
					return -1;
				}
				return 0;
			}
			else
			{
				if (oracleTimeSpan.IsNull)
				{
					return 1;
				}
				int num;
				int num2;
				int num3;
				int num4;
				int num5;
				OracleTimeSpan.Unpack(this._value, out num, out num2, out num3, out num4, out num5);
				int num6;
				int num7;
				int num8;
				int num9;
				int num10;
				OracleTimeSpan.Unpack(oracleTimeSpan._value, out num6, out num7, out num8, out num9, out num10);
				int num11 = num - num6;
				if (num11 != 0)
				{
					return num11;
				}
				num11 = num2 - num7;
				if (num11 != 0)
				{
					return num11;
				}
				num11 = num3 - num8;
				if (num11 != 0)
				{
					return num11;
				}
				num11 = num4 - num9;
				if (num11 != 0)
				{
					return num11;
				}
				num11 = num5 - num10;
				if (num11 != 0)
				{
					return num11;
				}
				return 0;
			}
		}

		// Token: 0x060006C9 RID: 1737 RVA: 0x000730F4 File Offset: 0x000724F4
		public override bool Equals(object value)
		{
			return value is OracleTimeSpan && (this == (OracleTimeSpan)value).Value;
		}

		// Token: 0x060006CA RID: 1738 RVA: 0x00073124 File Offset: 0x00072524
		public override int GetHashCode()
		{
			if (!this.IsNull)
			{
				return this._value.GetHashCode();
			}
			return 0;
		}

		// Token: 0x060006CB RID: 1739 RVA: 0x00073154 File Offset: 0x00072554
		internal static TimeSpan MarshalToTimeSpan(NativeBuffer buffer, int valueOffset)
		{
			byte[] rawValue = buffer.ReadBytes(valueOffset, 11);
			return OracleTimeSpan.ToTimeSpan(rawValue);
		}

		// Token: 0x060006CC RID: 1740 RVA: 0x00073174 File Offset: 0x00072574
		internal static int MarshalToNative(object value, NativeBuffer buffer, int offset)
		{
			byte[] array;
			if (value is OracleTimeSpan)
			{
				array = ((OracleTimeSpan)value)._value;
			}
			else
			{
				TimeSpan timeSpan = (TimeSpan)value;
				array = new byte[11];
				OracleTimeSpan.Pack(array, timeSpan.Days, timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds, (int)(timeSpan.Ticks % 10000000L) * 100);
			}
			buffer.WriteBytes(offset, array, 0, 11);
			return 11;
		}

		// Token: 0x060006CD RID: 1741 RVA: 0x000731F4 File Offset: 0x000725F4
		public static OracleTimeSpan Parse(string s)
		{
			TimeSpan ts = TimeSpan.Parse(s);
			return new OracleTimeSpan(ts);
		}

		// Token: 0x060006CE RID: 1742 RVA: 0x00073214 File Offset: 0x00072614
		public override string ToString()
		{
			if (this.IsNull)
			{
				return ADP.NullString;
			}
			return this.Value.ToString();
		}

		// Token: 0x060006CF RID: 1743 RVA: 0x00073254 File Offset: 0x00072654
		private static TimeSpan ToTimeSpan(byte[] rawValue)
		{
			int num;
			int num2;
			int num3;
			int num4;
			int num5;
			OracleTimeSpan.Unpack(rawValue, out num, out num2, out num3, out num4, out num5);
			long num6 = (long)num * 864000000000L + (long)num2 * 36000000000L + (long)num3 * 600000000L + (long)num4 * 10000000L;
			if (num5 < 100 || num5 > 100)
			{
				num6 += (long)num5 / 100L;
			}
			TimeSpan result = new TimeSpan(num6);
			return result;
		}

		// Token: 0x060006D0 RID: 1744 RVA: 0x000732C4 File Offset: 0x000726C4
		public static OracleBoolean Equals(OracleTimeSpan x, OracleTimeSpan y)
		{
			return x == y;
		}

		// Token: 0x060006D1 RID: 1745 RVA: 0x000732E4 File Offset: 0x000726E4
		public static OracleBoolean GreaterThan(OracleTimeSpan x, OracleTimeSpan y)
		{
			return x > y;
		}

		// Token: 0x060006D2 RID: 1746 RVA: 0x00073304 File Offset: 0x00072704
		public static OracleBoolean GreaterThanOrEqual(OracleTimeSpan x, OracleTimeSpan y)
		{
			return x >= y;
		}

		// Token: 0x060006D3 RID: 1747 RVA: 0x00073324 File Offset: 0x00072724
		public static OracleBoolean LessThan(OracleTimeSpan x, OracleTimeSpan y)
		{
			return x < y;
		}

		// Token: 0x060006D4 RID: 1748 RVA: 0x00073344 File Offset: 0x00072744
		public static OracleBoolean LessThanOrEqual(OracleTimeSpan x, OracleTimeSpan y)
		{
			return x <= y;
		}

		// Token: 0x060006D5 RID: 1749 RVA: 0x00073364 File Offset: 0x00072764
		public static OracleBoolean NotEquals(OracleTimeSpan x, OracleTimeSpan y)
		{
			return x != y;
		}

		// Token: 0x060006D6 RID: 1750 RVA: 0x00073384 File Offset: 0x00072784
		public static explicit operator TimeSpan(OracleTimeSpan x)
		{
			if (x.IsNull)
			{
				throw ADP.DataIsNull();
			}
			return x.Value;
		}

		// Token: 0x060006D7 RID: 1751 RVA: 0x000733B4 File Offset: 0x000727B4
		public static explicit operator OracleTimeSpan(string x)
		{
			return OracleTimeSpan.Parse(x);
		}

		// Token: 0x060006D8 RID: 1752 RVA: 0x000733D4 File Offset: 0x000727D4
		public static OracleBoolean operator ==(OracleTimeSpan x, OracleTimeSpan y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new OracleBoolean(x.CompareTo(y) == 0);
			}
			return OracleBoolean.Null;
		}

		// Token: 0x060006D9 RID: 1753 RVA: 0x00073414 File Offset: 0x00072814
		public static OracleBoolean operator >(OracleTimeSpan x, OracleTimeSpan y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new OracleBoolean(x.CompareTo(y) > 0);
			}
			return OracleBoolean.Null;
		}

		// Token: 0x060006DA RID: 1754 RVA: 0x00073454 File Offset: 0x00072854
		public static OracleBoolean operator >=(OracleTimeSpan x, OracleTimeSpan y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new OracleBoolean(x.CompareTo(y) >= 0);
			}
			return OracleBoolean.Null;
		}

		// Token: 0x060006DB RID: 1755 RVA: 0x00073494 File Offset: 0x00072894
		public static OracleBoolean operator <(OracleTimeSpan x, OracleTimeSpan y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new OracleBoolean(x.CompareTo(y) < 0);
			}
			return OracleBoolean.Null;
		}

		// Token: 0x060006DC RID: 1756 RVA: 0x000734D4 File Offset: 0x000728D4
		public static OracleBoolean operator <=(OracleTimeSpan x, OracleTimeSpan y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new OracleBoolean(x.CompareTo(y) <= 0);
			}
			return OracleBoolean.Null;
		}

		// Token: 0x060006DD RID: 1757 RVA: 0x00073514 File Offset: 0x00072914
		public static OracleBoolean operator !=(OracleTimeSpan x, OracleTimeSpan y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new OracleBoolean(x.CompareTo(y) != 0);
			}
			return OracleBoolean.Null;
		}

		// Token: 0x040004C6 RID: 1222
		private const int FractionalSecondsPerTick = 100;

		// Token: 0x040004C7 RID: 1223
		private byte[] _value;

		// Token: 0x040004C8 RID: 1224
		public static readonly OracleTimeSpan MaxValue = new OracleTimeSpan(TimeSpan.MaxValue);

		// Token: 0x040004C9 RID: 1225
		public static readonly OracleTimeSpan MinValue = new OracleTimeSpan(TimeSpan.MinValue);

		// Token: 0x040004CA RID: 1226
		public static readonly OracleTimeSpan Null = new OracleTimeSpan(true);
	}
}
