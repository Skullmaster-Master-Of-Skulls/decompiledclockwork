using System;
using System.Data.Common;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System.Data.SqlTypes
{
	// Token: 0x02000163 RID: 355
	[XmlSchemaProvider("GetXsdType")]
	[Serializable]
	public struct SqlInt64 : INullable, IComparable, IXmlSerializable
	{
		// Token: 0x0600164E RID: 5710 RVA: 0x000A4ED4 File Offset: 0x000A42D4
		private SqlInt64(bool fNull)
		{
			this.m_fNotNull = false;
			this.m_value = 0L;
		}

		// Token: 0x0600164F RID: 5711 RVA: 0x000A4EF0 File Offset: 0x000A42F0
		public SqlInt64(long value)
		{
			this.m_value = value;
			this.m_fNotNull = true;
		}

		// Token: 0x1700032F RID: 815
		// (get) Token: 0x06001650 RID: 5712 RVA: 0x000A4F0C File Offset: 0x000A430C
		public bool IsNull
		{
			get
			{
				return !this.m_fNotNull;
			}
		}

		// Token: 0x17000330 RID: 816
		// (get) Token: 0x06001651 RID: 5713 RVA: 0x000A4F24 File Offset: 0x000A4324
		public long Value
		{
			get
			{
				if (this.m_fNotNull)
				{
					return this.m_value;
				}
				throw new SqlNullValueException();
			}
		}

		// Token: 0x06001652 RID: 5714 RVA: 0x000A4F48 File Offset: 0x000A4348
		public static implicit operator SqlInt64(long x)
		{
			return new SqlInt64(x);
		}

		// Token: 0x06001653 RID: 5715 RVA: 0x000A4F5C File Offset: 0x000A435C
		public static explicit operator long(SqlInt64 x)
		{
			return x.Value;
		}

		// Token: 0x06001654 RID: 5716 RVA: 0x000A4F70 File Offset: 0x000A4370
		public override string ToString()
		{
			if (!this.IsNull)
			{
				return this.m_value.ToString(null);
			}
			return SQLResource.NullString;
		}

		// Token: 0x06001655 RID: 5717 RVA: 0x000A4F98 File Offset: 0x000A4398
		public static SqlInt64 Parse(string s)
		{
			if (s == SQLResource.NullString)
			{
				return SqlInt64.Null;
			}
			return new SqlInt64(long.Parse(s, null));
		}

		// Token: 0x06001656 RID: 5718 RVA: 0x000A4FC4 File Offset: 0x000A43C4
		public static SqlInt64 operator -(SqlInt64 x)
		{
			if (!x.IsNull)
			{
				return new SqlInt64(-x.m_value);
			}
			return SqlInt64.Null;
		}

		// Token: 0x06001657 RID: 5719 RVA: 0x000A4FEC File Offset: 0x000A43EC
		public static SqlInt64 operator ~(SqlInt64 x)
		{
			if (!x.IsNull)
			{
				return new SqlInt64(~x.m_value);
			}
			return SqlInt64.Null;
		}

		// Token: 0x06001658 RID: 5720 RVA: 0x000A5014 File Offset: 0x000A4414
		public static SqlInt64 operator +(SqlInt64 x, SqlInt64 y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlInt64.Null;
			}
			long num = x.m_value + y.m_value;
			if (SqlInt64.SameSignLong(x.m_value, y.m_value) && !SqlInt64.SameSignLong(x.m_value, num))
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			return new SqlInt64(num);
		}

		// Token: 0x06001659 RID: 5721 RVA: 0x000A507C File Offset: 0x000A447C
		public static SqlInt64 operator -(SqlInt64 x, SqlInt64 y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlInt64.Null;
			}
			long num = x.m_value - y.m_value;
			if (!SqlInt64.SameSignLong(x.m_value, y.m_value) && SqlInt64.SameSignLong(y.m_value, num))
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			return new SqlInt64(num);
		}

		// Token: 0x0600165A RID: 5722 RVA: 0x000A50E4 File Offset: 0x000A44E4
		public static SqlInt64 operator *(SqlInt64 x, SqlInt64 y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlInt64.Null;
			}
			bool flag = false;
			long num = x.m_value;
			long num2 = y.m_value;
			long num3 = 0L;
			if (num < 0L)
			{
				flag = true;
				num = -num;
			}
			if (num2 < 0L)
			{
				flag = !flag;
				num2 = -num2;
			}
			long num4 = num & (long)((ulong)-1);
			long num5 = num >> 32 & (long)((ulong)-1);
			long num6 = num2 & (long)((ulong)-1);
			long num7 = num2 >> 32 & (long)((ulong)-1);
			if (num5 != 0L && num7 != 0L)
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			long num8 = num4 * num6;
			if (num8 < 0L)
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			if (num5 != 0L)
			{
				num3 = num5 * num6;
				if (num3 < 0L || num3 > 9223372036854775807L)
				{
					throw new OverflowException(SQLResource.ArithOverflowMessage);
				}
			}
			else if (num7 != 0L)
			{
				num3 = num4 * num7;
				if (num3 < 0L || num3 > 9223372036854775807L)
				{
					throw new OverflowException(SQLResource.ArithOverflowMessage);
				}
			}
			num8 += num3 << 32;
			if (num8 < 0L)
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			if (flag)
			{
				num8 = -num8;
			}
			return new SqlInt64(num8);
		}

		// Token: 0x0600165B RID: 5723 RVA: 0x000A51F0 File Offset: 0x000A45F0
		public static SqlInt64 operator /(SqlInt64 x, SqlInt64 y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlInt64.Null;
			}
			if (y.m_value == 0L)
			{
				throw new DivideByZeroException(SQLResource.DivideByZeroMessage);
			}
			if (x.m_value == -9223372036854775808L && y.m_value == -1L)
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			return new SqlInt64(x.m_value / y.m_value);
		}

		// Token: 0x0600165C RID: 5724 RVA: 0x000A5260 File Offset: 0x000A4660
		public static SqlInt64 operator %(SqlInt64 x, SqlInt64 y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlInt64.Null;
			}
			if (y.m_value == 0L)
			{
				throw new DivideByZeroException(SQLResource.DivideByZeroMessage);
			}
			if (x.m_value == -9223372036854775808L && y.m_value == -1L)
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			return new SqlInt64(x.m_value % y.m_value);
		}

		// Token: 0x0600165D RID: 5725 RVA: 0x000A52D0 File Offset: 0x000A46D0
		public static SqlInt64 operator &(SqlInt64 x, SqlInt64 y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlInt64(x.m_value & y.m_value);
			}
			return SqlInt64.Null;
		}

		// Token: 0x0600165E RID: 5726 RVA: 0x000A5308 File Offset: 0x000A4708
		public static SqlInt64 operator |(SqlInt64 x, SqlInt64 y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlInt64(x.m_value | y.m_value);
			}
			return SqlInt64.Null;
		}

		// Token: 0x0600165F RID: 5727 RVA: 0x000A5340 File Offset: 0x000A4740
		public static SqlInt64 operator ^(SqlInt64 x, SqlInt64 y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlInt64(x.m_value ^ y.m_value);
			}
			return SqlInt64.Null;
		}

		// Token: 0x06001660 RID: 5728 RVA: 0x000A5378 File Offset: 0x000A4778
		public static explicit operator SqlInt64(SqlBoolean x)
		{
			if (!x.IsNull)
			{
				return new SqlInt64((long)((ulong)x.ByteValue));
			}
			return SqlInt64.Null;
		}

		// Token: 0x06001661 RID: 5729 RVA: 0x000A53A4 File Offset: 0x000A47A4
		public static implicit operator SqlInt64(SqlByte x)
		{
			if (!x.IsNull)
			{
				return new SqlInt64((long)((ulong)x.Value));
			}
			return SqlInt64.Null;
		}

		// Token: 0x06001662 RID: 5730 RVA: 0x000A53D0 File Offset: 0x000A47D0
		public static implicit operator SqlInt64(SqlInt16 x)
		{
			if (!x.IsNull)
			{
				return new SqlInt64((long)x.Value);
			}
			return SqlInt64.Null;
		}

		// Token: 0x06001663 RID: 5731 RVA: 0x000A53FC File Offset: 0x000A47FC
		public static implicit operator SqlInt64(SqlInt32 x)
		{
			if (!x.IsNull)
			{
				return new SqlInt64((long)x.Value);
			}
			return SqlInt64.Null;
		}

		// Token: 0x06001664 RID: 5732 RVA: 0x000A5428 File Offset: 0x000A4828
		public static explicit operator SqlInt64(SqlSingle x)
		{
			if (x.IsNull)
			{
				return SqlInt64.Null;
			}
			float value = x.Value;
			if (value > 9.223372E+18f || value < -9.223372E+18f)
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			return new SqlInt64((long)value);
		}

		// Token: 0x06001665 RID: 5733 RVA: 0x000A5470 File Offset: 0x000A4870
		public static explicit operator SqlInt64(SqlDouble x)
		{
			if (x.IsNull)
			{
				return SqlInt64.Null;
			}
			double value = x.Value;
			if (value > 9.223372036854776E+18 || value < -9.223372036854776E+18)
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			return new SqlInt64((long)value);
		}

		// Token: 0x06001666 RID: 5734 RVA: 0x000A54C0 File Offset: 0x000A48C0
		public static explicit operator SqlInt64(SqlMoney x)
		{
			if (!x.IsNull)
			{
				return new SqlInt64(x.ToInt64());
			}
			return SqlInt64.Null;
		}

		// Token: 0x06001667 RID: 5735 RVA: 0x000A54E8 File Offset: 0x000A48E8
		public static explicit operator SqlInt64(SqlDecimal x)
		{
			if (x.IsNull)
			{
				return SqlInt64.Null;
			}
			SqlDecimal sqlDecimal = x;
			sqlDecimal.AdjustScale((int)(-(int)sqlDecimal.m_bScale), false);
			if (sqlDecimal.m_bLen > 2)
			{
				throw new OverflowException(SQLResource.ConversionOverflowMessage);
			}
			long num2;
			if (sqlDecimal.m_bLen == 2)
			{
				ulong num = SqlDecimal.DWL(sqlDecimal.m_data1, sqlDecimal.m_data2);
				if (num > 9223372036854775807UL && (sqlDecimal.IsPositive || num != 9223372036854775808UL))
				{
					throw new OverflowException(SQLResource.ConversionOverflowMessage);
				}
				num2 = (long)num;
			}
			else
			{
				num2 = (long)((ulong)sqlDecimal.m_data1);
			}
			if (!sqlDecimal.IsPositive)
			{
				num2 = -num2;
			}
			return new SqlInt64(num2);
		}

		// Token: 0x06001668 RID: 5736 RVA: 0x000A5590 File Offset: 0x000A4990
		public static explicit operator SqlInt64(SqlString x)
		{
			if (!x.IsNull)
			{
				return new SqlInt64(long.Parse(x.Value, null));
			}
			return SqlInt64.Null;
		}

		// Token: 0x06001669 RID: 5737 RVA: 0x000A55C0 File Offset: 0x000A49C0
		private static bool SameSignLong(long x, long y)
		{
			return ((x ^ y) & long.MinValue) == 0L;
		}

		// Token: 0x0600166A RID: 5738 RVA: 0x000A55E0 File Offset: 0x000A49E0
		public static SqlBoolean operator ==(SqlInt64 x, SqlInt64 y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.m_value == y.m_value);
			}
			return SqlBoolean.Null;
		}

		// Token: 0x0600166B RID: 5739 RVA: 0x000A5618 File Offset: 0x000A4A18
		public static SqlBoolean operator !=(SqlInt64 x, SqlInt64 y)
		{
			return !(x == y);
		}

		// Token: 0x0600166C RID: 5740 RVA: 0x000A5634 File Offset: 0x000A4A34
		public static SqlBoolean operator <(SqlInt64 x, SqlInt64 y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.m_value < y.m_value);
			}
			return SqlBoolean.Null;
		}

		// Token: 0x0600166D RID: 5741 RVA: 0x000A566C File Offset: 0x000A4A6C
		public static SqlBoolean operator >(SqlInt64 x, SqlInt64 y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.m_value > y.m_value);
			}
			return SqlBoolean.Null;
		}

		// Token: 0x0600166E RID: 5742 RVA: 0x000A56A4 File Offset: 0x000A4AA4
		public static SqlBoolean operator <=(SqlInt64 x, SqlInt64 y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.m_value <= y.m_value);
			}
			return SqlBoolean.Null;
		}

		// Token: 0x0600166F RID: 5743 RVA: 0x000A56E0 File Offset: 0x000A4AE0
		public static SqlBoolean operator >=(SqlInt64 x, SqlInt64 y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.m_value >= y.m_value);
			}
			return SqlBoolean.Null;
		}

		// Token: 0x06001670 RID: 5744 RVA: 0x000A571C File Offset: 0x000A4B1C
		public static SqlInt64 OnesComplement(SqlInt64 x)
		{
			return ~x;
		}

		// Token: 0x06001671 RID: 5745 RVA: 0x000A5730 File Offset: 0x000A4B30
		public static SqlInt64 Add(SqlInt64 x, SqlInt64 y)
		{
			return x + y;
		}

		// Token: 0x06001672 RID: 5746 RVA: 0x000A5744 File Offset: 0x000A4B44
		public static SqlInt64 Subtract(SqlInt64 x, SqlInt64 y)
		{
			return x - y;
		}

		// Token: 0x06001673 RID: 5747 RVA: 0x000A5758 File Offset: 0x000A4B58
		public static SqlInt64 Multiply(SqlInt64 x, SqlInt64 y)
		{
			return x * y;
		}

		// Token: 0x06001674 RID: 5748 RVA: 0x000A576C File Offset: 0x000A4B6C
		public static SqlInt64 Divide(SqlInt64 x, SqlInt64 y)
		{
			return x / y;
		}

		// Token: 0x06001675 RID: 5749 RVA: 0x000A5780 File Offset: 0x000A4B80
		public static SqlInt64 Mod(SqlInt64 x, SqlInt64 y)
		{
			return x % y;
		}

		// Token: 0x06001676 RID: 5750 RVA: 0x000A5794 File Offset: 0x000A4B94
		public static SqlInt64 Modulus(SqlInt64 x, SqlInt64 y)
		{
			return x % y;
		}

		// Token: 0x06001677 RID: 5751 RVA: 0x000A57A8 File Offset: 0x000A4BA8
		public static SqlInt64 BitwiseAnd(SqlInt64 x, SqlInt64 y)
		{
			return x & y;
		}

		// Token: 0x06001678 RID: 5752 RVA: 0x000A57BC File Offset: 0x000A4BBC
		public static SqlInt64 BitwiseOr(SqlInt64 x, SqlInt64 y)
		{
			return x | y;
		}

		// Token: 0x06001679 RID: 5753 RVA: 0x000A57D0 File Offset: 0x000A4BD0
		public static SqlInt64 Xor(SqlInt64 x, SqlInt64 y)
		{
			return x ^ y;
		}

		// Token: 0x0600167A RID: 5754 RVA: 0x000A57E4 File Offset: 0x000A4BE4
		public static SqlBoolean Equals(SqlInt64 x, SqlInt64 y)
		{
			return x == y;
		}

		// Token: 0x0600167B RID: 5755 RVA: 0x000A57F8 File Offset: 0x000A4BF8
		public static SqlBoolean NotEquals(SqlInt64 x, SqlInt64 y)
		{
			return x != y;
		}

		// Token: 0x0600167C RID: 5756 RVA: 0x000A580C File Offset: 0x000A4C0C
		public static SqlBoolean LessThan(SqlInt64 x, SqlInt64 y)
		{
			return x < y;
		}

		// Token: 0x0600167D RID: 5757 RVA: 0x000A5820 File Offset: 0x000A4C20
		public static SqlBoolean GreaterThan(SqlInt64 x, SqlInt64 y)
		{
			return x > y;
		}

		// Token: 0x0600167E RID: 5758 RVA: 0x000A5834 File Offset: 0x000A4C34
		public static SqlBoolean LessThanOrEqual(SqlInt64 x, SqlInt64 y)
		{
			return x <= y;
		}

		// Token: 0x0600167F RID: 5759 RVA: 0x000A5848 File Offset: 0x000A4C48
		public static SqlBoolean GreaterThanOrEqual(SqlInt64 x, SqlInt64 y)
		{
			return x >= y;
		}

		// Token: 0x06001680 RID: 5760 RVA: 0x000A585C File Offset: 0x000A4C5C
		public SqlBoolean ToSqlBoolean()
		{
			return (SqlBoolean)this;
		}

		// Token: 0x06001681 RID: 5761 RVA: 0x000A5874 File Offset: 0x000A4C74
		public SqlByte ToSqlByte()
		{
			return (SqlByte)this;
		}

		// Token: 0x06001682 RID: 5762 RVA: 0x000A588C File Offset: 0x000A4C8C
		public SqlDouble ToSqlDouble()
		{
			return this;
		}

		// Token: 0x06001683 RID: 5763 RVA: 0x000A58A4 File Offset: 0x000A4CA4
		public SqlInt16 ToSqlInt16()
		{
			return (SqlInt16)this;
		}

		// Token: 0x06001684 RID: 5764 RVA: 0x000A58BC File Offset: 0x000A4CBC
		public SqlInt32 ToSqlInt32()
		{
			return (SqlInt32)this;
		}

		// Token: 0x06001685 RID: 5765 RVA: 0x000A58D4 File Offset: 0x000A4CD4
		public SqlMoney ToSqlMoney()
		{
			return this;
		}

		// Token: 0x06001686 RID: 5766 RVA: 0x000A58EC File Offset: 0x000A4CEC
		public SqlDecimal ToSqlDecimal()
		{
			return this;
		}

		// Token: 0x06001687 RID: 5767 RVA: 0x000A5904 File Offset: 0x000A4D04
		public SqlSingle ToSqlSingle()
		{
			return this;
		}

		// Token: 0x06001688 RID: 5768 RVA: 0x000A591C File Offset: 0x000A4D1C
		public SqlString ToSqlString()
		{
			return (SqlString)this;
		}

		// Token: 0x06001689 RID: 5769 RVA: 0x000A5934 File Offset: 0x000A4D34
		public int CompareTo(object value)
		{
			if (value is SqlInt64)
			{
				SqlInt64 value2 = (SqlInt64)value;
				return this.CompareTo(value2);
			}
			throw ADP.WrongType(value.GetType(), typeof(SqlInt64));
		}

		// Token: 0x0600168A RID: 5770 RVA: 0x000A5970 File Offset: 0x000A4D70
		public int CompareTo(SqlInt64 value)
		{
			if (this.IsNull)
			{
				if (!value.IsNull)
				{
					return -1;
				}
				return 0;
			}
			else
			{
				if (value.IsNull)
				{
					return 1;
				}
				if (this < value)
				{
					return -1;
				}
				if (this > value)
				{
					return 1;
				}
				return 0;
			}
		}

		// Token: 0x0600168B RID: 5771 RVA: 0x000A59C8 File Offset: 0x000A4DC8
		public override bool Equals(object value)
		{
			if (!(value is SqlInt64))
			{
				return false;
			}
			SqlInt64 y = (SqlInt64)value;
			if (y.IsNull || this.IsNull)
			{
				return y.IsNull && this.IsNull;
			}
			return (this == y).Value;
		}

		// Token: 0x0600168C RID: 5772 RVA: 0x000A5A20 File Offset: 0x000A4E20
		public override int GetHashCode()
		{
			if (!this.IsNull)
			{
				return this.Value.GetHashCode();
			}
			return 0;
		}

		// Token: 0x0600168D RID: 5773 RVA: 0x000A5A48 File Offset: 0x000A4E48
		XmlSchema IXmlSerializable.GetSchema()
		{
			return null;
		}

		// Token: 0x0600168E RID: 5774 RVA: 0x000A5A58 File Offset: 0x000A4E58
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			string attribute = reader.GetAttribute("nil", "http://www.w3.org/2001/XMLSchema-instance");
			if (attribute != null && XmlConvert.ToBoolean(attribute))
			{
				reader.ReadElementString();
				this.m_fNotNull = false;
				return;
			}
			this.m_value = XmlConvert.ToInt64(reader.ReadElementString());
			this.m_fNotNull = true;
		}

		// Token: 0x0600168F RID: 5775 RVA: 0x000A5AA8 File Offset: 0x000A4EA8
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			if (this.IsNull)
			{
				writer.WriteAttributeString("xsi", "nil", "http://www.w3.org/2001/XMLSchema-instance", "true");
				return;
			}
			writer.WriteString(XmlConvert.ToString(this.m_value));
		}

		// Token: 0x06001690 RID: 5776 RVA: 0x000A5AEC File Offset: 0x000A4EEC
		public static XmlQualifiedName GetXsdType(XmlSchemaSet schemaSet)
		{
			return new XmlQualifiedName("long", "http://www.w3.org/2001/XMLSchema");
		}

		// Token: 0x04000DF7 RID: 3575
		private bool m_fNotNull;

		// Token: 0x04000DF8 RID: 3576
		private long m_value;

		// Token: 0x04000DF9 RID: 3577
		private const long x_lLowIntMask = 4294967295L;

		// Token: 0x04000DFA RID: 3578
		private const long x_lHighIntMask = -4294967296L;

		// Token: 0x04000DFB RID: 3579
		public static readonly SqlInt64 Null = new SqlInt64(true);

		// Token: 0x04000DFC RID: 3580
		public static readonly SqlInt64 Zero = new SqlInt64(0L);

		// Token: 0x04000DFD RID: 3581
		public static readonly SqlInt64 MinValue = new SqlInt64(long.MinValue);

		// Token: 0x04000DFE RID: 3582
		public static readonly SqlInt64 MaxValue = new SqlInt64(long.MaxValue);
	}
}
