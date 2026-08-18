using System;
using System.Data.Common;
using System.Data.SqlTypes;

namespace System.Data.OracleClient
{
	// Token: 0x0200004B RID: 75
	public struct OracleBinary : IComparable, INullable
	{
		// Token: 0x06000244 RID: 580 RVA: 0x0005D474 File Offset: 0x0005C874
		private OracleBinary(bool isNull)
		{
			this._value = (isNull ? null : new byte[0]);
		}

		// Token: 0x06000245 RID: 581 RVA: 0x0005D494 File Offset: 0x0005C894
		public OracleBinary(byte[] b)
		{
			this._value = ((b == null) ? b : ((byte[])b.Clone()));
		}

		// Token: 0x06000246 RID: 582 RVA: 0x0005D4C4 File Offset: 0x0005C8C4
		internal OracleBinary(NativeBuffer buffer, int valueOffset, int lengthOffset, MetaType metaType)
		{
			int length = OracleBinary.GetLength(buffer, lengthOffset, metaType);
			this._value = new byte[length];
			OracleBinary.GetBytes(buffer, valueOffset, metaType, 0, this._value, 0, length);
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000247 RID: 583 RVA: 0x0005D504 File Offset: 0x0005C904
		public bool IsNull
		{
			get
			{
				return null == this._value;
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000248 RID: 584 RVA: 0x0005D524 File Offset: 0x0005C924
		public int Length
		{
			get
			{
				if (this.IsNull)
				{
					throw ADP.DataIsNull();
				}
				return this._value.Length;
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000249 RID: 585 RVA: 0x0005D554 File Offset: 0x0005C954
		public byte[] Value
		{
			get
			{
				if (this.IsNull)
				{
					throw ADP.DataIsNull();
				}
				return (byte[])this._value.Clone();
			}
		}

		// Token: 0x1700005A RID: 90
		public byte this[int index]
		{
			get
			{
				if (this.IsNull)
				{
					throw ADP.DataIsNull();
				}
				return this._value[index];
			}
		}

		// Token: 0x0600024B RID: 587 RVA: 0x0005D5B4 File Offset: 0x0005C9B4
		public int CompareTo(object obj)
		{
			if (obj.GetType() != typeof(OracleBinary))
			{
				throw ADP.WrongType(obj.GetType(), typeof(OracleBinary));
			}
			OracleBinary oracleBinary = (OracleBinary)obj;
			if (this.IsNull)
			{
				if (!oracleBinary.IsNull)
				{
					return -1;
				}
				return 0;
			}
			else
			{
				if (oracleBinary.IsNull)
				{
					return 1;
				}
				return OracleBinary.PerformCompareByte(this._value, oracleBinary._value);
			}
		}

		// Token: 0x0600024C RID: 588 RVA: 0x0005D624 File Offset: 0x0005CA24
		public override bool Equals(object value)
		{
			return value is OracleBinary && (this == (OracleBinary)value).Value;
		}

		// Token: 0x0600024D RID: 589 RVA: 0x0005D654 File Offset: 0x0005CA54
		internal static int GetBytes(NativeBuffer buffer, int valueOffset, MetaType metaType, int sourceOffset, byte[] destinationBuffer, int destinationOffset, int byteCount)
		{
			if (!metaType.IsLong)
			{
				buffer.ReadBytes(valueOffset + sourceOffset, destinationBuffer, destinationOffset, byteCount);
			}
			else
			{
				NativeBuffer_LongColumnData.CopyOutOfLineBytes(buffer.ReadIntPtr(valueOffset), sourceOffset, destinationBuffer, destinationOffset, byteCount);
			}
			return byteCount;
		}

		// Token: 0x0600024E RID: 590 RVA: 0x0005D694 File Offset: 0x0005CA94
		internal static int GetLength(NativeBuffer buffer, int lengthOffset, MetaType metaType)
		{
			if (metaType.IsLong)
			{
				return buffer.ReadInt32(lengthOffset);
			}
			return (int)buffer.ReadInt16(lengthOffset);
		}

		// Token: 0x0600024F RID: 591 RVA: 0x0005D6C4 File Offset: 0x0005CAC4
		public override int GetHashCode()
		{
			if (!this.IsNull)
			{
				return this._value.GetHashCode();
			}
			return 0;
		}

		// Token: 0x06000250 RID: 592 RVA: 0x0005D6F4 File Offset: 0x0005CAF4
		private static int PerformCompareByte(byte[] x, byte[] y)
		{
			int num = x.Length;
			int num2 = y.Length;
			bool flag = num < num2;
			int num3 = flag ? num : num2;
			int i = 0;
			while (i < num3)
			{
				if (x[i] != y[i])
				{
					if (x[i] < y[i])
					{
						return -1;
					}
					return 1;
				}
				else
				{
					i++;
				}
			}
			if (num == num2)
			{
				return 0;
			}
			byte b = 0;
			if (flag)
			{
				for (i = num3; i < num2; i++)
				{
					if (y[i] != b)
					{
						return -1;
					}
				}
			}
			else
			{
				for (i = num3; i < num; i++)
				{
					if (x[i] != b)
					{
						return 1;
					}
				}
			}
			return 0;
		}

		// Token: 0x06000251 RID: 593 RVA: 0x0005D774 File Offset: 0x0005CB74
		public static OracleBinary Concat(OracleBinary x, OracleBinary y)
		{
			return x + y;
		}

		// Token: 0x06000252 RID: 594 RVA: 0x0005D794 File Offset: 0x0005CB94
		public static OracleBoolean Equals(OracleBinary x, OracleBinary y)
		{
			return x == y;
		}

		// Token: 0x06000253 RID: 595 RVA: 0x0005D7B4 File Offset: 0x0005CBB4
		public static OracleBoolean GreaterThan(OracleBinary x, OracleBinary y)
		{
			return x > y;
		}

		// Token: 0x06000254 RID: 596 RVA: 0x0005D7D4 File Offset: 0x0005CBD4
		public static OracleBoolean GreaterThanOrEqual(OracleBinary x, OracleBinary y)
		{
			return x >= y;
		}

		// Token: 0x06000255 RID: 597 RVA: 0x0005D7F4 File Offset: 0x0005CBF4
		public static OracleBoolean LessThan(OracleBinary x, OracleBinary y)
		{
			return x < y;
		}

		// Token: 0x06000256 RID: 598 RVA: 0x0005D814 File Offset: 0x0005CC14
		public static OracleBoolean LessThanOrEqual(OracleBinary x, OracleBinary y)
		{
			return x <= y;
		}

		// Token: 0x06000257 RID: 599 RVA: 0x0005D834 File Offset: 0x0005CC34
		public static OracleBoolean NotEquals(OracleBinary x, OracleBinary y)
		{
			return x != y;
		}

		// Token: 0x06000258 RID: 600 RVA: 0x0005D854 File Offset: 0x0005CC54
		public static implicit operator OracleBinary(byte[] b)
		{
			return new OracleBinary(b);
		}

		// Token: 0x06000259 RID: 601 RVA: 0x0005D874 File Offset: 0x0005CC74
		public static explicit operator byte[](OracleBinary x)
		{
			return x.Value;
		}

		// Token: 0x0600025A RID: 602 RVA: 0x0005D894 File Offset: 0x0005CC94
		public static OracleBinary operator +(OracleBinary x, OracleBinary y)
		{
			if (x.IsNull || y.IsNull)
			{
				return OracleBinary.Null;
			}
			byte[] array = new byte[x._value.Length + y._value.Length];
			x._value.CopyTo(array, 0);
			y._value.CopyTo(array, x.Value.Length);
			OracleBinary result = new OracleBinary(array);
			return result;
		}

		// Token: 0x0600025B RID: 603 RVA: 0x0005D904 File Offset: 0x0005CD04
		public static OracleBoolean operator ==(OracleBinary x, OracleBinary y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new OracleBoolean(x.CompareTo(y) == 0);
			}
			return OracleBoolean.Null;
		}

		// Token: 0x0600025C RID: 604 RVA: 0x0005D944 File Offset: 0x0005CD44
		public static OracleBoolean operator >(OracleBinary x, OracleBinary y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new OracleBoolean(x.CompareTo(y) > 0);
			}
			return OracleBoolean.Null;
		}

		// Token: 0x0600025D RID: 605 RVA: 0x0005D984 File Offset: 0x0005CD84
		public static OracleBoolean operator >=(OracleBinary x, OracleBinary y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new OracleBoolean(x.CompareTo(y) >= 0);
			}
			return OracleBoolean.Null;
		}

		// Token: 0x0600025E RID: 606 RVA: 0x0005D9C4 File Offset: 0x0005CDC4
		public static OracleBoolean operator <(OracleBinary x, OracleBinary y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new OracleBoolean(x.CompareTo(y) < 0);
			}
			return OracleBoolean.Null;
		}

		// Token: 0x0600025F RID: 607 RVA: 0x0005DA04 File Offset: 0x0005CE04
		public static OracleBoolean operator <=(OracleBinary x, OracleBinary y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new OracleBoolean(x.CompareTo(y) <= 0);
			}
			return OracleBoolean.Null;
		}

		// Token: 0x06000260 RID: 608 RVA: 0x0005DA44 File Offset: 0x0005CE44
		public static OracleBoolean operator !=(OracleBinary x, OracleBinary y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new OracleBoolean(x.CompareTo(y) != 0);
			}
			return OracleBoolean.Null;
		}

		// Token: 0x04000331 RID: 817
		private byte[] _value;

		// Token: 0x04000332 RID: 818
		public static readonly OracleBinary Null = new OracleBinary(true);
	}
}
