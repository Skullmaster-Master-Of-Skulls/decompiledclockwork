using System;
using System.Data.Common;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System.Data.SqlTypes
{
	// Token: 0x02000352 RID: 850
	[XmlSchemaProvider("GetXsdType")]
	[Serializable]
	public struct SqlInt64 : INullable, IComparable, IXmlSerializable
	{
		// Token: 0x06002DF9 RID: 11769 RVA: 0x002CF9B8 File Offset: 0x002CEDB8
		private SqlInt64(bool fNull)
		{
			this.m_fNotNull = false;
			this.m_value = 0L;
		}

		// Token: 0x06002DFA RID: 11770 RVA: 0x002CF9D8 File Offset: 0x002CEDD8
		public SqlInt64(long value)
		{
			this.m_value = value;
			this.m_fNotNull = true;
		}

		// Token: 0x17000756 RID: 1878
		// (get) Token: 0x06002DFB RID: 11771 RVA: 0x002CF9F8 File Offset: 0x002CEDF8
		public bool IsNull
		{
			get
			{
				return !this.m_fNotNull;
			}
		}

		// Token: 0x17000757 RID: 1879
		// (get) Token: 0x06002DFC RID: 11772 RVA: 0x002CFA18 File Offset: 0x002CEE18
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

		// Token: 0x06002DFD RID: 11773 RVA: 0x002CFA48 File Offset: 0x002CEE48
		public static implicit operator SqlInt64(long x)
		{
			return new SqlInt64(x);
		}

		// Token: 0x06002DFE RID: 11774 RVA: 0x002CFA68 File Offset: 0x002CEE68
		public static explicit operator long(SqlInt64 x)
		{
			return x.Value;
		}

		// Token: 0x06002DFF RID: 11775 RVA: 0x002CFA88 File Offset: 0x002CEE88
		public override string ToString()
		{
			if (!this.IsNull)
			{
				return this.m_value.ToString(null);
			}
			return SQLResource.NullString;
		}

		// Token: 0x06002E00 RID: 11776 RVA: 0x002CFAB8 File Offset: 0x002CEEB8
		public static SqlInt64 Parse(string s)
		{
			if (s == SQLResource.NullString)
			{
				return SqlInt64.Null;
			}
			return new SqlInt64(long.Parse(s, null));
		}

		// Token: 0x06002E01 RID: 11777 RVA: 0x002CFAE8 File Offset: 0x002CEEE8
		public static SqlInt64 operator -(SqlInt64 x)
		{
			if (!x.IsNull)
			{
				return new SqlInt64(-x.m_value);
			}
			return SqlInt64.Null;
		}

		// Token: 0x06002E02 RID: 11778 RVA: 0x002CFB18 File Offset: 0x002CEF18
		public static SqlInt64 operator ~(SqlInt64 x)
		{
			if (!x.IsNull)
			{
				return new SqlInt64(~x.m_value);
			}
			return SqlInt64.Null;
		}

		// Token: 0x06002E03 RID: 11779 RVA: 0x002CFB48 File Offset: 0x002CEF48
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

		// Token: 0x06002E04 RID: 11780 RVA: 0x002CFBB8 File Offset: 0x002CEFB8
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

		// Token: 0x06002E05 RID: 11781 RVA: 0x002CFC28 File Offset: 0x002CF028
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
			long num4 = num & SqlInt64.x_lLowIntMask;
			long num5 = num >> 32 & SqlInt64.x_lLowIntMask;
			long num6 = num2 & SqlInt64.x_lLowIntMask;
			long num7 = num2 >> 32 & SqlInt64.x_lLowIntMask;
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

		// Token: 0x06002E06 RID: 11782 RVA: 0x002CFD48 File Offset: 0x002CF148
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

		// Token: 0x06002E07 RID: 11783 RVA: 0x002CFDC8 File Offset: 0x002CF1C8
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

		// Token: 0x06002E08 RID: 11784 RVA: 0x002CFE48 File Offset: 0x002CF248
		public static SqlInt64 operator &(SqlInt64 x, SqlInt64 y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlInt64(x.m_value & y.m_value);
			}
			return SqlInt64.Null;
		}

		// Token: 0x06002E09 RID: 11785 RVA: 0x002CFE88 File Offset: 0x002CF288
		public static SqlInt64 operator |(SqlInt64 x, SqlInt64 y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlInt64(x.m_value | y.m_value);
			}
			return SqlInt64.Null;
		}

		// Token: 0x06002E0A RID: 11786 RVA: 0x002CFEC8 File Offset: 0x002CF2C8
		public static SqlInt64 operator ^(SqlInt64 x, SqlInt64 y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlInt64(x.m_value ^ y.m_value);
			}
			return SqlInt64.Null;
		}

		// Token: 0x06002E0B RID: 11787 RVA: 0x002CFF08 File Offset: 0x002CF308
		public static explicit operator SqlInt64(SqlBoolean x)
		{
			if (!x.IsNull)
			{
				return new SqlInt64((long)((ulong)x.ByteValue));
			}
			return SqlInt64.Null;
		}

		// Token: 0x06002E0C RID: 11788 RVA: 0x002CFF38 File Offset: 0x002CF338
		public static implicit operator SqlInt64(SqlByte x)
		{
			if (!x.IsNull)
			{
				return new SqlInt64((long)((ulong)x.Value));
			}
			return SqlInt64.Null;
		}

		// Token: 0x06002E0D RID: 11789 RVA: 0x002CFF68 File Offset: 0x002CF368
		public static implicit operator SqlInt64(SqlInt16 x)
		{
			if (!x.IsNull)
			{
				return new SqlInt64((long)x.Value);
			}
			return SqlInt64.Null;
		}

		// Token: 0x06002E0E RID: 11790 RVA: 0x002CFF98 File Offset: 0x002CF398
		public static implicit operator SqlInt64(SqlInt32 x)
		{
			if (!x.IsNull)
			{
				return new SqlInt64((long)x.Value);
			}
			return SqlInt64.Null;
		}

		// Token: 0x06002E0F RID: 11791 RVA: 0x002CFFC8 File Offset: 0x002CF3C8
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

		// Token: 0x06002E10 RID: 11792 RVA: 0x002D0018 File Offset: 0x002CF418
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

		// Token: 0x06002E11 RID: 11793 RVA: 0x002D0068 File Offset: 0x002CF468
		public static explicit operator SqlInt64(SqlMoney x)
		{
			if (!x.IsNull)
			{
				return new SqlInt64(x.ToInt64());
			}
			return SqlInt64.Null;
		}

		// Token: 0x06002E12 RID: 11794 RVA: 0x002D0098 File Offset: 0x002CF498
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
				if (num > SqlDecimal.x_llMax && (sqlDecimal.IsPositive || num != 1UL + SqlDecimal.x_llMax))
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

		// Token: 0x06002E13 RID: 11795 RVA: 0x002D0148 File Offset: 0x002CF548
		public static explicit operator SqlInt64(SqlString x)
		{
			if (!x.IsNull)
			{
				return new SqlInt64(long.Parse(x.Value, null));
			}
			return SqlInt64.Null;
		}

		// Token: 0x06002E14 RID: 11796 RVA: 0x002D0178 File Offset: 0x002CF578
		private static bool SameSignLong(long x, long y)
		{
			return ((x ^ y) & long.MinValue) == 0L;
		}

		// Token: 0x06002E15 RID: 11797 RVA: 0x002D0198 File Offset: 0x002CF598
		public static SqlBoolean operator ==(SqlInt64 x, SqlInt64 y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.m_value == y.m_value);
			}
			return SqlBoolean.Null;
		}

		// Token: 0x06002E16 RID: 11798 RVA: 0x002D01D8 File Offset: 0x002CF5D8
		public static SqlBoolean operator !=(SqlInt64 x, SqlInt64 y)
		{
			return !(x == y);
		}

		// Token: 0x06002E17 RID: 11799 RVA: 0x002D01F8 File Offset: 0x002CF5F8
		public static SqlBoolean operator <(SqlInt64 x, SqlInt64 y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.m_value < y.m_value);
			}
			return SqlBoolean.Null;
		}

		// Token: 0x06002E18 RID: 11800 RVA: 0x002D0238 File Offset: 0x002CF638
		public static SqlBoolean operator >(SqlInt64 x, SqlInt64 y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.m_value > y.m_value);
			}
			return SqlBoolean.Null;
		}

		// Token: 0x06002E19 RID: 11801 RVA: 0x002D0278 File Offset: 0x002CF678
		public static SqlBoolean operator <=(SqlInt64 x, SqlInt64 y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.m_value <= y.m_value);
			}
			return SqlBoolean.Null;
		}

		// Token: 0x06002E1A RID: 11802 RVA: 0x002D02B8 File Offset: 0x002CF6B8
		public static SqlBoolean operator >=(SqlInt64 x, SqlInt64 y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.m_value >= y.m_value);
			}
			return SqlBoolean.Null;
		}

		// Token: 0x06002E1B RID: 11803 RVA: 0x002D02F8 File Offset: 0x002CF6F8
		public static SqlInt64 OnesComplement(SqlInt64 x)
		{
			return ~x;
		}

		// Token: 0x06002E1C RID: 11804 RVA: 0x002D0318 File Offset: 0x002CF718
		public static SqlInt64 Add(SqlInt64 x, SqlInt64 y)
		{
			return x + y;
		}

		// Token: 0x06002E1D RID: 11805 RVA: 0x002D0338 File Offset: 0x002CF738
		public static SqlInt64 Subtract(SqlInt64 x, SqlInt64 y)
		{
			return x - y;
		}

		// Token: 0x06002E1E RID: 11806 RVA: 0x002D0358 File Offset: 0x002CF758
		public static SqlInt64 Multiply(SqlInt64 x, SqlInt64 y)
		{
			return x * y;
		}

		// Token: 0x06002E1F RID: 11807 RVA: 0x002D0378 File Offset: 0x002CF778
		public static SqlInt64 Divide(SqlInt64 x, SqlInt64 y)
		{
			return x / y;
		}

		// Token: 0x06002E20 RID: 11808 RVA: 0x002D0398 File Offset: 0x002CF798
		public static SqlInt64 Mod(SqlInt64 x, SqlInt64 y)
		{
			return x % y;
		}

		// Token: 0x06002E21 RID: 11809 RVA: 0x002D03B8 File Offset: 0x002CF7B8
		public static SqlInt64 Modulus(SqlInt64 x, SqlInt64 y)
		{
			return x % y;
		}

		// Token: 0x06002E22 RID: 11810 RVA: 0x002D03D8 File Offset: 0x002CF7D8
		public static SqlInt64 BitwiseAnd(SqlInt64 x, SqlInt64 y)
		{
			return x & y;
		}

		// Token: 0x06002E23 RID: 11811 RVA: 0x002D03F8 File Offset: 0x002CF7F8
		public static SqlInt64 BitwiseOr(SqlInt64 x, SqlInt64 y)
		{
			return x | y;
		}

		// Token: 0x06002E24 RID: 11812 RVA: 0x002D0418 File Offset: 0x002CF818
		public static SqlInt64 Xor(SqlInt64 x, SqlInt64 y)
		{
			return x ^ y;
		}

		// Token: 0x06002E25 RID: 11813 RVA: 0x002D0438 File Offset: 0x002CF838
		public static SqlBoolean Equals(SqlInt64 x, SqlInt64 y)
		{
			return x == y;
		}

		// Token: 0x06002E26 RID: 11814 RVA: 0x002D0458 File Offset: 0x002CF858
		public static SqlBoolean NotEquals(SqlInt64 x, SqlInt64 y)
		{
			return x != y;
		}

		// Token: 0x06002E27 RID: 11815 RVA: 0x002D0478 File Offset: 0x002CF878
		public static SqlBoolean LessThan(SqlInt64 x, SqlInt64 y)
		{
			return x < y;
		}

		// Token: 0x06002E28 RID: 11816 RVA: 0x002D0498 File Offset: 0x002CF898
		public static SqlBoolean GreaterThan(SqlInt64 x, SqlInt64 y)
		{
			return x > y;
		}

		// Token: 0x06002E29 RID: 11817 RVA: 0x002D04B8 File Offset: 0x002CF8B8
		public static SqlBoolean LessThanOrEqual(SqlInt64 x, SqlInt64 y)
		{
			return x <= y;
		}

		// Token: 0x06002E2A RID: 11818 RVA: 0x002D04D8 File Offset: 0x002CF8D8
		public static SqlBoolean GreaterThanOrEqual(SqlInt64 x, SqlInt64 y)
		{
			return x >= y;
		}

		// Token: 0x06002E2B RID: 11819 RVA: 0x002D04F8 File Offset: 0x002CF8F8
		public SqlBoolean ToSqlBoolean()
		{
			return (SqlBoolean)this;
		}

		// Token: 0x06002E2C RID: 11820 RVA: 0x002D0518 File Offset: 0x002CF918
		public SqlByte ToSqlByte()
		{
			return (SqlByte)this;
		}

		// Token: 0x06002E2D RID: 11821 RVA: 0x002D0538 File Offset: 0x002CF938
		public SqlDouble ToSqlDouble()
		{
			return this;
		}

		// Token: 0x06002E2E RID: 11822 RVA: 0x002D0558 File Offset: 0x002CF958
		public SqlInt16 ToSqlInt16()
		{
			return (SqlInt16)this;
		}

		// Token: 0x06002E2F RID: 11823 RVA: 0x002D0578 File Offset: 0x002CF978
		public SqlInt32 ToSqlInt32()
		{
			return (SqlInt32)this;
		}

		// Token: 0x06002E30 RID: 11824 RVA: 0x002D0598 File Offset: 0x002CF998
		public SqlMoney ToSqlMoney()
		{
			return this;
		}

		// Token: 0x06002E31 RID: 11825 RVA: 0x002D05B8 File Offset: 0x002CF9B8
		public SqlDecimal ToSqlDecimal()
		{
			return this;
		}

		// Token: 0x06002E32 RID: 11826 RVA: 0x002D05D8 File Offset: 0x002CF9D8
		public SqlSingle ToSqlSingle()
		{
			return this;
		}

		// Token: 0x06002E33 RID: 11827 RVA: 0x002D05F8 File Offset: 0x002CF9F8
		public SqlString ToSqlString()
		{
			return (SqlString)this;
		}

		// Token: 0x06002E34 RID: 11828 RVA: 0x002D0618 File Offset: 0x002CFA18
		public int CompareTo(object value)
		{
			if (value is SqlInt64)
			{
				SqlInt64 value2 = (SqlInt64)value;
				return this.CompareTo(value2);
			}
			throw ADP.WrongType(value.GetType(), typeof(SqlInt64));
		}

		// Token: 0x06002E35 RID: 11829 RVA: 0x002D0658 File Offset: 0x002CFA58
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

		// Token: 0x06002E36 RID: 11830 RVA: 0x002D06B8 File Offset: 0x002CFAB8
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

		// Token: 0x06002E37 RID: 11831 RVA: 0x002D0718 File Offset: 0x002CFB18
		public override int GetHashCode()
		{
			if (!this.IsNull)
			{
				return this.Value.GetHashCode();
			}
			return 0;
		}

		// Token: 0x06002E38 RID: 11832 RVA: 0x002D0748 File Offset: 0x002CFB48
		XmlSchema IXmlSerializable.GetSchema()
		{
			return null;
		}

		// Token: 0x06002E39 RID: 11833 RVA: 0x002D0758 File Offset: 0x002CFB58
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			string attribute = reader.GetAttribute("nil", "http://www.w3.org/2001/XMLSchema-instance");
			if (attribute != null && XmlConvert.ToBoolean(attribute))
			{
				this.m_fNotNull = false;
				return;
			}
			this.m_value = XmlConvert.ToInt64(reader.ReadElementString());
			this.m_fNotNull = true;
		}

		// Token: 0x06002E3A RID: 11834 RVA: 0x002D07A8 File Offset: 0x002CFBA8
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			if (this.IsNull)
			{
				writer.WriteAttributeString("xsi", "nil", "http://www.w3.org/2001/XMLSchema-instance", "true");
				return;
			}
			writer.WriteString(XmlConvert.ToString(this.m_value));
		}

		// Token: 0x06002E3B RID: 11835 RVA: 0x002D07F8 File Offset: 0x002CFBF8
		public static XmlQualifiedName GetXsdType(XmlSchemaSet schemaSet)
		{
			return new XmlQualifiedName("long", "http://www.w3.org/2001/XMLSchema");
		}

		// Token: 0x04001D0E RID: 7438
		private bool m_fNotNull;

		// Token: 0x04001D0F RID: 7439
		private long m_value;

		// Token: 0x04001D10 RID: 7440
		private static readonly long x_lLowIntMask = (long)((ulong)-1);

		// Token: 0x04001D11 RID: 7441
		private static readonly long x_lHighIntMask = -4294967296L;

		// Token: 0x04001D12 RID: 7442
		public static readonly SqlInt64 Null = new SqlInt64(true);

		// Token: 0x04001D13 RID: 7443
		public static readonly SqlInt64 Zero = new SqlInt64(0L);

		// Token: 0x04001D14 RID: 7444
		public static readonly SqlInt64 MinValue = new SqlInt64(long.MinValue);

		// Token: 0x04001D15 RID: 7445
		public static readonly SqlInt64 MaxValue = new SqlInt64(long.MaxValue);
	}
}
