using System;
using System.Data.Common;
using System.Globalization;

namespace System.Data.OracleClient
{
	// Token: 0x0200004C RID: 76
	public struct OracleBoolean : IComparable
	{
		// Token: 0x06000262 RID: 610 RVA: 0x0005DAA4 File Offset: 0x0005CEA4
		public OracleBoolean(bool value)
		{
			this._value = (value ? 1 : 2);
		}

		// Token: 0x06000263 RID: 611 RVA: 0x0005DAC4 File Offset: 0x0005CEC4
		public OracleBoolean(int value)
		{
			this = new OracleBoolean(value, false);
		}

		// Token: 0x06000264 RID: 612 RVA: 0x0005DAE4 File Offset: 0x0005CEE4
		private OracleBoolean(int value, bool isNull)
		{
			if (isNull)
			{
				this._value = 0;
				return;
			}
			this._value = ((value != 0) ? 1 : 2);
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000265 RID: 613 RVA: 0x0005DB14 File Offset: 0x0005CF14
		private byte ByteValue
		{
			get
			{
				return this._value;
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000266 RID: 614 RVA: 0x0005DB34 File Offset: 0x0005CF34
		public bool IsFalse
		{
			get
			{
				return this._value == 2;
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000267 RID: 615 RVA: 0x0005DB54 File Offset: 0x0005CF54
		public bool IsNull
		{
			get
			{
				return this._value == 0;
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000268 RID: 616 RVA: 0x0005DB74 File Offset: 0x0005CF74
		public bool IsTrue
		{
			get
			{
				return this._value == 1;
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000269 RID: 617 RVA: 0x0005DB94 File Offset: 0x0005CF94
		public bool Value
		{
			get
			{
				switch (this._value)
				{
				case 1:
					return true;
				case 2:
					return false;
				default:
					throw ADP.DataIsNull();
				}
			}
		}

		// Token: 0x0600026A RID: 618 RVA: 0x0005DBC4 File Offset: 0x0005CFC4
		public int CompareTo(object obj)
		{
			if (!(obj is OracleBoolean))
			{
				throw ADP.WrongType(obj.GetType(), typeof(OracleBoolean));
			}
			OracleBoolean oracleBoolean = (OracleBoolean)obj;
			if (this.IsNull)
			{
				if (!oracleBoolean.IsNull)
				{
					return -1;
				}
				return 0;
			}
			else
			{
				if (oracleBoolean.IsNull)
				{
					return 1;
				}
				if (this.ByteValue < oracleBoolean.ByteValue)
				{
					return -1;
				}
				if (this.ByteValue > oracleBoolean.ByteValue)
				{
					return 1;
				}
				return 0;
			}
		}

		// Token: 0x0600026B RID: 619 RVA: 0x0005DC44 File Offset: 0x0005D044
		public override bool Equals(object value)
		{
			if (!(value is OracleBoolean))
			{
				return false;
			}
			OracleBoolean y = (OracleBoolean)value;
			if (y.IsNull || this.IsNull)
			{
				return y.IsNull && this.IsNull;
			}
			return (this == y).Value;
		}

		// Token: 0x0600026C RID: 620 RVA: 0x0005DCA4 File Offset: 0x0005D0A4
		public override int GetHashCode()
		{
			if (!this.IsNull)
			{
				return this._value.GetHashCode();
			}
			return 0;
		}

		// Token: 0x0600026D RID: 621 RVA: 0x0005DCD4 File Offset: 0x0005D0D4
		public static OracleBoolean Parse(string s)
		{
			OracleBoolean result;
			try
			{
				result = new OracleBoolean(int.Parse(s, CultureInfo.InvariantCulture));
			}
			catch (Exception ex)
			{
				Type type = ex.GetType();
				if (type != ADP.ArgumentNullExceptionType && type != ADP.FormatExceptionType && type != ADP.OverflowExceptionType)
				{
					throw ex;
				}
				result = new OracleBoolean(bool.Parse(s));
			}
			return result;
		}

		// Token: 0x0600026E RID: 622 RVA: 0x0005DD44 File Offset: 0x0005D144
		public override string ToString()
		{
			if (this.IsNull)
			{
				return ADP.NullString;
			}
			return this.Value.ToString(CultureInfo.CurrentCulture);
		}

		// Token: 0x0600026F RID: 623 RVA: 0x0005DD74 File Offset: 0x0005D174
		public static OracleBoolean And(OracleBoolean x, OracleBoolean y)
		{
			return x & y;
		}

		// Token: 0x06000270 RID: 624 RVA: 0x0005DD94 File Offset: 0x0005D194
		public static OracleBoolean Equals(OracleBoolean x, OracleBoolean y)
		{
			return x == y;
		}

		// Token: 0x06000271 RID: 625 RVA: 0x0005DDB4 File Offset: 0x0005D1B4
		public static OracleBoolean NotEquals(OracleBoolean x, OracleBoolean y)
		{
			return x != y;
		}

		// Token: 0x06000272 RID: 626 RVA: 0x0005DDD4 File Offset: 0x0005D1D4
		public static OracleBoolean OnesComplement(OracleBoolean x)
		{
			return ~x;
		}

		// Token: 0x06000273 RID: 627 RVA: 0x0005DDF4 File Offset: 0x0005D1F4
		public static OracleBoolean Or(OracleBoolean x, OracleBoolean y)
		{
			return x | y;
		}

		// Token: 0x06000274 RID: 628 RVA: 0x0005DE14 File Offset: 0x0005D214
		public static OracleBoolean Xor(OracleBoolean x, OracleBoolean y)
		{
			return x ^ y;
		}

		// Token: 0x06000275 RID: 629 RVA: 0x0005DE34 File Offset: 0x0005D234
		public static implicit operator OracleBoolean(bool x)
		{
			return new OracleBoolean(x);
		}

		// Token: 0x06000276 RID: 630 RVA: 0x0005DE54 File Offset: 0x0005D254
		public static explicit operator OracleBoolean(string x)
		{
			return OracleBoolean.Parse(x);
		}

		// Token: 0x06000277 RID: 631 RVA: 0x0005DE74 File Offset: 0x0005D274
		public static explicit operator OracleBoolean(OracleNumber x)
		{
			if (!x.IsNull)
			{
				return new OracleBoolean(x.Value != 0m);
			}
			return OracleBoolean.Null;
		}

		// Token: 0x06000278 RID: 632 RVA: 0x0005DEB4 File Offset: 0x0005D2B4
		public static explicit operator bool(OracleBoolean x)
		{
			return x.Value;
		}

		// Token: 0x06000279 RID: 633 RVA: 0x0005DED4 File Offset: 0x0005D2D4
		public static OracleBoolean operator !(OracleBoolean x)
		{
			switch (x._value)
			{
			case 1:
				return OracleBoolean.False;
			case 2:
				return OracleBoolean.True;
			default:
				return OracleBoolean.Null;
			}
		}

		// Token: 0x0600027A RID: 634 RVA: 0x0005DF14 File Offset: 0x0005D314
		public static OracleBoolean operator ~(OracleBoolean x)
		{
			return !x;
		}

		// Token: 0x0600027B RID: 635 RVA: 0x0005DF34 File Offset: 0x0005D334
		public static bool operator true(OracleBoolean x)
		{
			return x.IsTrue;
		}

		// Token: 0x0600027C RID: 636 RVA: 0x0005DF54 File Offset: 0x0005D354
		public static bool operator false(OracleBoolean x)
		{
			return x.IsFalse;
		}

		// Token: 0x0600027D RID: 637 RVA: 0x0005DF74 File Offset: 0x0005D374
		public static OracleBoolean operator &(OracleBoolean x, OracleBoolean y)
		{
			if (x._value == 2 || y._value == 2)
			{
				return OracleBoolean.False;
			}
			if (x._value == 1 && y._value == 1)
			{
				return OracleBoolean.True;
			}
			return OracleBoolean.Null;
		}

		// Token: 0x0600027E RID: 638 RVA: 0x0005DFC4 File Offset: 0x0005D3C4
		public static OracleBoolean operator ==(OracleBoolean x, OracleBoolean y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new OracleBoolean(x._value == y._value);
			}
			return OracleBoolean.Null;
		}

		// Token: 0x0600027F RID: 639 RVA: 0x0005E004 File Offset: 0x0005D404
		public static OracleBoolean operator !=(OracleBoolean x, OracleBoolean y)
		{
			return !(x == y);
		}

		// Token: 0x06000280 RID: 640 RVA: 0x0005E024 File Offset: 0x0005D424
		public static OracleBoolean operator |(OracleBoolean x, OracleBoolean y)
		{
			if (x._value == 1 || y._value == 1)
			{
				return OracleBoolean.True;
			}
			if (x._value == 2 && y._value == 2)
			{
				return OracleBoolean.False;
			}
			return OracleBoolean.Null;
		}

		// Token: 0x06000281 RID: 641 RVA: 0x0005E074 File Offset: 0x0005D474
		public static OracleBoolean operator ^(OracleBoolean x, OracleBoolean y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new OracleBoolean(x._value != y._value);
			}
			return OracleBoolean.Null;
		}

		// Token: 0x04000333 RID: 819
		private const byte x_Null = 0;

		// Token: 0x04000334 RID: 820
		private const byte x_True = 1;

		// Token: 0x04000335 RID: 821
		private const byte x_False = 2;

		// Token: 0x04000336 RID: 822
		private byte _value;

		// Token: 0x04000337 RID: 823
		public static readonly OracleBoolean False = new OracleBoolean(false);

		// Token: 0x04000338 RID: 824
		public static readonly OracleBoolean Null = new OracleBoolean(0, true);

		// Token: 0x04000339 RID: 825
		public static readonly OracleBoolean One = new OracleBoolean(1);

		// Token: 0x0400033A RID: 826
		public static readonly OracleBoolean True = new OracleBoolean(true);

		// Token: 0x0400033B RID: 827
		public static readonly OracleBoolean Zero = new OracleBoolean(0);
	}
}
