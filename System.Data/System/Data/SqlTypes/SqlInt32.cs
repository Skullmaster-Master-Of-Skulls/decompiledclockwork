using System;
using System.Data.Common;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System.Data.SqlTypes
{
	// Token: 0x02000351 RID: 849
	[XmlSchemaProvider("GetXsdType")]
	[Serializable]
	public struct SqlInt32 : INullable, IComparable, IXmlSerializable
	{
		// Token: 0x06002DB5 RID: 11701 RVA: 0x002CEBD8 File Offset: 0x002CDFD8
		private SqlInt32(bool fNull)
		{
			this.m_fNotNull = false;
			this.m_value = 0;
		}

		// Token: 0x06002DB6 RID: 11702 RVA: 0x002CEBF8 File Offset: 0x002CDFF8
		public SqlInt32(int value)
		{
			this.m_value = value;
			this.m_fNotNull = true;
		}

		// Token: 0x17000754 RID: 1876
		// (get) Token: 0x06002DB7 RID: 11703 RVA: 0x002CEC18 File Offset: 0x002CE018
		public bool IsNull
		{
			get
			{
				return !this.m_fNotNull;
			}
		}

		// Token: 0x17000755 RID: 1877
		// (get) Token: 0x06002DB8 RID: 11704 RVA: 0x002CEC38 File Offset: 0x002CE038
		public int Value
		{
			get
			{
				if (this.IsNull)
				{
					throw new SqlNullValueException();
				}
				return this.m_value;
			}
		}

		// Token: 0x06002DB9 RID: 11705 RVA: 0x002CEC68 File Offset: 0x002CE068
		public static implicit operator SqlInt32(int x)
		{
			return new SqlInt32(x);
		}

		// Token: 0x06002DBA RID: 11706 RVA: 0x002CEC88 File Offset: 0x002CE088
		public static explicit operator int(SqlInt32 x)
		{
			return x.Value;
		}

		// Token: 0x06002DBB RID: 11707 RVA: 0x002CECA8 File Offset: 0x002CE0A8
		public override string ToString()
		{
			if (!this.IsNull)
			{
				return this.m_value.ToString(null);
			}
			return SQLResource.NullString;
		}

		// Token: 0x06002DBC RID: 11708 RVA: 0x002CECD8 File Offset: 0x002CE0D8
		public static SqlInt32 Parse(string s)
		{
			if (s == SQLResource.NullString)
			{
				return SqlInt32.Null;
			}
			return new SqlInt32(int.Parse(s, null));
		}

		// Token: 0x06002DBD RID: 11709 RVA: 0x002CED08 File Offset: 0x002CE108
		public static SqlInt32 operator -(SqlInt32 x)
		{
			if (!x.IsNull)
			{
				return new SqlInt32(-x.m_value);
			}
			return SqlInt32.Null;
		}

		// Token: 0x06002DBE RID: 11710 RVA: 0x002CED38 File Offset: 0x002CE138
		public static SqlInt32 operator ~(SqlInt32 x)
		{
			if (!x.IsNull)
			{
				return new SqlInt32(~x.m_value);
			}
			return SqlInt32.Null;
		}

		// Token: 0x06002DBF RID: 11711 RVA: 0x002CED68 File Offset: 0x002CE168
		public static SqlInt32 operator +(SqlInt32 x, SqlInt32 y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlInt32.Null;
			}
			int num = x.m_value + y.m_value;
			if (SqlInt32.SameSignInt(x.m_value, y.m_value) && !SqlInt32.SameSignInt(x.m_value, num))
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			return new SqlInt32(num);
		}

		// Token: 0x06002DC0 RID: 11712 RVA: 0x002CEDD8 File Offset: 0x002CE1D8
		public static SqlInt32 operator -(SqlInt32 x, SqlInt32 y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlInt32.Null;
			}
			int num = x.m_value - y.m_value;
			if (!SqlInt32.SameSignInt(x.m_value, y.m_value) && SqlInt32.SameSignInt(y.m_value, num))
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			return new SqlInt32(num);
		}

		// Token: 0x06002DC1 RID: 11713 RVA: 0x002CEE48 File Offset: 0x002CE248
		public static SqlInt32 operator *(SqlInt32 x, SqlInt32 y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlInt32.Null;
			}
			long num = (long)x.m_value * (long)y.m_value;
			long num2 = num & SqlInt32.x_lBitNotIntMax;
			if (num2 != 0L && num2 != SqlInt32.x_lBitNotIntMax)
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			return new SqlInt32((int)num);
		}

		// Token: 0x06002DC2 RID: 11714 RVA: 0x002CEEA8 File Offset: 0x002CE2A8
		public static SqlInt32 operator /(SqlInt32 x, SqlInt32 y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlInt32.Null;
			}
			if (y.m_value == 0)
			{
				throw new DivideByZeroException(SQLResource.DivideByZeroMessage);
			}
			if ((long)x.m_value == SqlInt32.x_iIntMin && y.m_value == -1)
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			return new SqlInt32(x.m_value / y.m_value);
		}

		// Token: 0x06002DC3 RID: 11715 RVA: 0x002CEF28 File Offset: 0x002CE328
		public static SqlInt32 operator %(SqlInt32 x, SqlInt32 y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlInt32.Null;
			}
			if (y.m_value == 0)
			{
				throw new DivideByZeroException(SQLResource.DivideByZeroMessage);
			}
			if ((long)x.m_value == SqlInt32.x_iIntMin && y.m_value == -1)
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			return new SqlInt32(x.m_value % y.m_value);
		}

		// Token: 0x06002DC4 RID: 11716 RVA: 0x002CEFA8 File Offset: 0x002CE3A8
		public static SqlInt32 operator &(SqlInt32 x, SqlInt32 y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlInt32(x.m_value & y.m_value);
			}
			return SqlInt32.Null;
		}

		// Token: 0x06002DC5 RID: 11717 RVA: 0x002CEFE8 File Offset: 0x002CE3E8
		public static SqlInt32 operator |(SqlInt32 x, SqlInt32 y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlInt32(x.m_value | y.m_value);
			}
			return SqlInt32.Null;
		}

		// Token: 0x06002DC6 RID: 11718 RVA: 0x002CF028 File Offset: 0x002CE428
		public static SqlInt32 operator ^(SqlInt32 x, SqlInt32 y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlInt32(x.m_value ^ y.m_value);
			}
			return SqlInt32.Null;
		}

		// Token: 0x06002DC7 RID: 11719 RVA: 0x002CF068 File Offset: 0x002CE468
		public static explicit operator SqlInt32(SqlBoolean x)
		{
			if (!x.IsNull)
			{
				return new SqlInt32((int)x.ByteValue);
			}
			return SqlInt32.Null;
		}

		// Token: 0x06002DC8 RID: 11720 RVA: 0x002CF098 File Offset: 0x002CE498
		public static implicit operator SqlInt32(SqlByte x)
		{
			if (!x.IsNull)
			{
				return new SqlInt32((int)x.Value);
			}
			return SqlInt32.Null;
		}

		// Token: 0x06002DC9 RID: 11721 RVA: 0x002CF0C8 File Offset: 0x002CE4C8
		public static implicit operator SqlInt32(SqlInt16 x)
		{
			if (!x.IsNull)
			{
				return new SqlInt32((int)x.Value);
			}
			return SqlInt32.Null;
		}

		// Token: 0x06002DCA RID: 11722 RVA: 0x002CF0F8 File Offset: 0x002CE4F8
		public static explicit operator SqlInt32(SqlInt64 x)
		{
			if (x.IsNull)
			{
				return SqlInt32.Null;
			}
			long value = x.Value;
			if (value > 2147483647L || value < -2147483648L)
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			return new SqlInt32((int)value);
		}

		// Token: 0x06002DCB RID: 11723 RVA: 0x002CF148 File Offset: 0x002CE548
		public static explicit operator SqlInt32(SqlSingle x)
		{
			if (x.IsNull)
			{
				return SqlInt32.Null;
			}
			float value = x.Value;
			if (value > 2.1474836E+09f || value < -2.1474836E+09f)
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			return new SqlInt32((int)value);
		}

		// Token: 0x06002DCC RID: 11724 RVA: 0x002CF198 File Offset: 0x002CE598
		public static explicit operator SqlInt32(SqlDouble x)
		{
			if (x.IsNull)
			{
				return SqlInt32.Null;
			}
			double value = x.Value;
			if (value > 2147483647.0 || value < -2147483648.0)
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			return new SqlInt32((int)value);
		}

		// Token: 0x06002DCD RID: 11725 RVA: 0x002CF1E8 File Offset: 0x002CE5E8
		public static explicit operator SqlInt32(SqlMoney x)
		{
			if (!x.IsNull)
			{
				return new SqlInt32(x.ToInt32());
			}
			return SqlInt32.Null;
		}

		// Token: 0x06002DCE RID: 11726 RVA: 0x002CF218 File Offset: 0x002CE618
		public static explicit operator SqlInt32(SqlDecimal x)
		{
			if (x.IsNull)
			{
				return SqlInt32.Null;
			}
			x.AdjustScale((int)(-(int)x.Scale), true);
			long num = (long)((ulong)x.m_data1);
			if (!x.IsPositive)
			{
				num = -num;
			}
			if (x.m_bLen > 1 || num > 2147483647L || num < -2147483648L)
			{
				throw new OverflowException(SQLResource.ConversionOverflowMessage);
			}
			return new SqlInt32((int)num);
		}

		// Token: 0x06002DCF RID: 11727 RVA: 0x002CF288 File Offset: 0x002CE688
		public static explicit operator SqlInt32(SqlString x)
		{
			if (!x.IsNull)
			{
				return new SqlInt32(int.Parse(x.Value, null));
			}
			return SqlInt32.Null;
		}

		// Token: 0x06002DD0 RID: 11728 RVA: 0x002CF2B8 File Offset: 0x002CE6B8
		private static bool SameSignInt(int x, int y)
		{
			return ((long)(x ^ y) & (long)((ulong)int.MinValue)) == 0L;
		}

		// Token: 0x06002DD1 RID: 11729 RVA: 0x002CF2D8 File Offset: 0x002CE6D8
		public static SqlBoolean operator ==(SqlInt32 x, SqlInt32 y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.m_value == y.m_value);
			}
			return SqlBoolean.Null;
		}

		// Token: 0x06002DD2 RID: 11730 RVA: 0x002CF318 File Offset: 0x002CE718
		public static SqlBoolean operator !=(SqlInt32 x, SqlInt32 y)
		{
			return !(x == y);
		}

		// Token: 0x06002DD3 RID: 11731 RVA: 0x002CF338 File Offset: 0x002CE738
		public static SqlBoolean operator <(SqlInt32 x, SqlInt32 y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.m_value < y.m_value);
			}
			return SqlBoolean.Null;
		}

		// Token: 0x06002DD4 RID: 11732 RVA: 0x002CF378 File Offset: 0x002CE778
		public static SqlBoolean operator >(SqlInt32 x, SqlInt32 y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.m_value > y.m_value);
			}
			return SqlBoolean.Null;
		}

		// Token: 0x06002DD5 RID: 11733 RVA: 0x002CF3B8 File Offset: 0x002CE7B8
		public static SqlBoolean operator <=(SqlInt32 x, SqlInt32 y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.m_value <= y.m_value);
			}
			return SqlBoolean.Null;
		}

		// Token: 0x06002DD6 RID: 11734 RVA: 0x002CF3F8 File Offset: 0x002CE7F8
		public static SqlBoolean operator >=(SqlInt32 x, SqlInt32 y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.m_value >= y.m_value);
			}
			return SqlBoolean.Null;
		}

		// Token: 0x06002DD7 RID: 11735 RVA: 0x002CF438 File Offset: 0x002CE838
		public static SqlInt32 OnesComplement(SqlInt32 x)
		{
			return ~x;
		}

		// Token: 0x06002DD8 RID: 11736 RVA: 0x002CF458 File Offset: 0x002CE858
		public static SqlInt32 Add(SqlInt32 x, SqlInt32 y)
		{
			return x + y;
		}

		// Token: 0x06002DD9 RID: 11737 RVA: 0x002CF478 File Offset: 0x002CE878
		public static SqlInt32 Subtract(SqlInt32 x, SqlInt32 y)
		{
			return x - y;
		}

		// Token: 0x06002DDA RID: 11738 RVA: 0x002CF498 File Offset: 0x002CE898
		public static SqlInt32 Multiply(SqlInt32 x, SqlInt32 y)
		{
			return x * y;
		}

		// Token: 0x06002DDB RID: 11739 RVA: 0x002CF4B8 File Offset: 0x002CE8B8
		public static SqlInt32 Divide(SqlInt32 x, SqlInt32 y)
		{
			return x / y;
		}

		// Token: 0x06002DDC RID: 11740 RVA: 0x002CF4D8 File Offset: 0x002CE8D8
		public static SqlInt32 Mod(SqlInt32 x, SqlInt32 y)
		{
			return x % y;
		}

		// Token: 0x06002DDD RID: 11741 RVA: 0x002CF4F8 File Offset: 0x002CE8F8
		public static SqlInt32 Modulus(SqlInt32 x, SqlInt32 y)
		{
			return x % y;
		}

		// Token: 0x06002DDE RID: 11742 RVA: 0x002CF518 File Offset: 0x002CE918
		public static SqlInt32 BitwiseAnd(SqlInt32 x, SqlInt32 y)
		{
			return x & y;
		}

		// Token: 0x06002DDF RID: 11743 RVA: 0x002CF538 File Offset: 0x002CE938
		public static SqlInt32 BitwiseOr(SqlInt32 x, SqlInt32 y)
		{
			return x | y;
		}

		// Token: 0x06002DE0 RID: 11744 RVA: 0x002CF558 File Offset: 0x002CE958
		public static SqlInt32 Xor(SqlInt32 x, SqlInt32 y)
		{
			return x ^ y;
		}

		// Token: 0x06002DE1 RID: 11745 RVA: 0x002CF578 File Offset: 0x002CE978
		public static SqlBoolean Equals(SqlInt32 x, SqlInt32 y)
		{
			return x == y;
		}

		// Token: 0x06002DE2 RID: 11746 RVA: 0x002CF598 File Offset: 0x002CE998
		public static SqlBoolean NotEquals(SqlInt32 x, SqlInt32 y)
		{
			return x != y;
		}

		// Token: 0x06002DE3 RID: 11747 RVA: 0x002CF5B8 File Offset: 0x002CE9B8
		public static SqlBoolean LessThan(SqlInt32 x, SqlInt32 y)
		{
			return x < y;
		}

		// Token: 0x06002DE4 RID: 11748 RVA: 0x002CF5D8 File Offset: 0x002CE9D8
		public static SqlBoolean GreaterThan(SqlInt32 x, SqlInt32 y)
		{
			return x > y;
		}

		// Token: 0x06002DE5 RID: 11749 RVA: 0x002CF5F8 File Offset: 0x002CE9F8
		public static SqlBoolean LessThanOrEqual(SqlInt32 x, SqlInt32 y)
		{
			return x <= y;
		}

		// Token: 0x06002DE6 RID: 11750 RVA: 0x002CF618 File Offset: 0x002CEA18
		public static SqlBoolean GreaterThanOrEqual(SqlInt32 x, SqlInt32 y)
		{
			return x >= y;
		}

		// Token: 0x06002DE7 RID: 11751 RVA: 0x002CF638 File Offset: 0x002CEA38
		public SqlBoolean ToSqlBoolean()
		{
			return (SqlBoolean)this;
		}

		// Token: 0x06002DE8 RID: 11752 RVA: 0x002CF658 File Offset: 0x002CEA58
		public SqlByte ToSqlByte()
		{
			return (SqlByte)this;
		}

		// Token: 0x06002DE9 RID: 11753 RVA: 0x002CF678 File Offset: 0x002CEA78
		public SqlDouble ToSqlDouble()
		{
			return this;
		}

		// Token: 0x06002DEA RID: 11754 RVA: 0x002CF698 File Offset: 0x002CEA98
		public SqlInt16 ToSqlInt16()
		{
			return (SqlInt16)this;
		}

		// Token: 0x06002DEB RID: 11755 RVA: 0x002CF6B8 File Offset: 0x002CEAB8
		public SqlInt64 ToSqlInt64()
		{
			return this;
		}

		// Token: 0x06002DEC RID: 11756 RVA: 0x002CF6D8 File Offset: 0x002CEAD8
		public SqlMoney ToSqlMoney()
		{
			return this;
		}

		// Token: 0x06002DED RID: 11757 RVA: 0x002CF6F8 File Offset: 0x002CEAF8
		public SqlDecimal ToSqlDecimal()
		{
			return this;
		}

		// Token: 0x06002DEE RID: 11758 RVA: 0x002CF718 File Offset: 0x002CEB18
		public SqlSingle ToSqlSingle()
		{
			return this;
		}

		// Token: 0x06002DEF RID: 11759 RVA: 0x002CF738 File Offset: 0x002CEB38
		public SqlString ToSqlString()
		{
			return (SqlString)this;
		}

		// Token: 0x06002DF0 RID: 11760 RVA: 0x002CF758 File Offset: 0x002CEB58
		public int CompareTo(object value)
		{
			if (value is SqlInt32)
			{
				SqlInt32 value2 = (SqlInt32)value;
				return this.CompareTo(value2);
			}
			throw ADP.WrongType(value.GetType(), typeof(SqlInt32));
		}

		// Token: 0x06002DF1 RID: 11761 RVA: 0x002CF798 File Offset: 0x002CEB98
		public int CompareTo(SqlInt32 value)
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

		// Token: 0x06002DF2 RID: 11762 RVA: 0x002CF7F8 File Offset: 0x002CEBF8
		public override bool Equals(object value)
		{
			if (!(value is SqlInt32))
			{
				return false;
			}
			SqlInt32 y = (SqlInt32)value;
			if (y.IsNull || this.IsNull)
			{
				return y.IsNull && this.IsNull;
			}
			return (this == y).Value;
		}

		// Token: 0x06002DF3 RID: 11763 RVA: 0x002CF858 File Offset: 0x002CEC58
		public override int GetHashCode()
		{
			if (!this.IsNull)
			{
				return this.Value.GetHashCode();
			}
			return 0;
		}

		// Token: 0x06002DF4 RID: 11764 RVA: 0x002CF888 File Offset: 0x002CEC88
		XmlSchema IXmlSerializable.GetSchema()
		{
			return null;
		}

		// Token: 0x06002DF5 RID: 11765 RVA: 0x002CF898 File Offset: 0x002CEC98
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			string attribute = reader.GetAttribute("nil", "http://www.w3.org/2001/XMLSchema-instance");
			if (attribute != null && XmlConvert.ToBoolean(attribute))
			{
				this.m_fNotNull = false;
				return;
			}
			this.m_value = XmlConvert.ToInt32(reader.ReadElementString());
			this.m_fNotNull = true;
		}

		// Token: 0x06002DF6 RID: 11766 RVA: 0x002CF8E8 File Offset: 0x002CECE8
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			if (this.IsNull)
			{
				writer.WriteAttributeString("xsi", "nil", "http://www.w3.org/2001/XMLSchema-instance", "true");
				return;
			}
			writer.WriteString(XmlConvert.ToString(this.m_value));
		}

		// Token: 0x06002DF7 RID: 11767 RVA: 0x002CF938 File Offset: 0x002CED38
		public static XmlQualifiedName GetXsdType(XmlSchemaSet schemaSet)
		{
			return new XmlQualifiedName("int", "http://www.w3.org/2001/XMLSchema");
		}

		// Token: 0x04001D06 RID: 7430
		private bool m_fNotNull;

		// Token: 0x04001D07 RID: 7431
		private int m_value;

		// Token: 0x04001D08 RID: 7432
		private static readonly long x_iIntMin = -2147483648L;

		// Token: 0x04001D09 RID: 7433
		private static readonly long x_lBitNotIntMax = -2147483648L;

		// Token: 0x04001D0A RID: 7434
		public static readonly SqlInt32 Null = new SqlInt32(true);

		// Token: 0x04001D0B RID: 7435
		public static readonly SqlInt32 Zero = new SqlInt32(0);

		// Token: 0x04001D0C RID: 7436
		public static readonly SqlInt32 MinValue = new SqlInt32(int.MinValue);

		// Token: 0x04001D0D RID: 7437
		public static readonly SqlInt32 MaxValue = new SqlInt32(int.MaxValue);
	}
}
