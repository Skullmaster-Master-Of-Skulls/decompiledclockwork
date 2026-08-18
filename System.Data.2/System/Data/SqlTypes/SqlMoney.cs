using System;
using System.Data.Common;
using System.Globalization;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System.Data.SqlTypes
{
	// Token: 0x02000164 RID: 356
	[XmlSchemaProvider("GetXsdType")]
	[Serializable]
	public struct SqlMoney : INullable, IComparable, IXmlSerializable
	{
		// Token: 0x06001692 RID: 5778 RVA: 0x000A5B54 File Offset: 0x000A4F54
		private SqlMoney(bool fNull)
		{
			this.m_fNotNull = false;
			this.m_value = 0L;
		}

		// Token: 0x06001693 RID: 5779 RVA: 0x000A5B70 File Offset: 0x000A4F70
		internal SqlMoney(long value, int ignored)
		{
			this.m_value = value;
			this.m_fNotNull = true;
		}

		// Token: 0x06001694 RID: 5780 RVA: 0x000A5B8C File Offset: 0x000A4F8C
		public SqlMoney(int value)
		{
			this.m_value = (long)value * 10000L;
			this.m_fNotNull = true;
		}

		// Token: 0x06001695 RID: 5781 RVA: 0x000A5BB0 File Offset: 0x000A4FB0
		public SqlMoney(long value)
		{
			if (value < -922337203685477L || value > 922337203685477L)
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			this.m_value = value * 10000L;
			this.m_fNotNull = true;
		}

		// Token: 0x06001696 RID: 5782 RVA: 0x000A5BF8 File Offset: 0x000A4FF8
		public SqlMoney(decimal value)
		{
			SqlDecimal sqlDecimal = new SqlDecimal(value);
			sqlDecimal.AdjustScale((int)(4 - sqlDecimal.Scale), true);
			if (sqlDecimal.m_data3 != 0U || sqlDecimal.m_data4 != 0U)
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			bool isPositive = sqlDecimal.IsPositive;
			ulong num = (ulong)sqlDecimal.m_data1 + ((ulong)sqlDecimal.m_data2 << 32);
			if ((isPositive && num > 9223372036854775807UL) || (!isPositive && num > 9223372036854775808UL))
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			this.m_value = (long)(isPositive ? num : (-(long)num));
			this.m_fNotNull = true;
		}

		// Token: 0x06001697 RID: 5783 RVA: 0x000A5C94 File Offset: 0x000A5094
		public SqlMoney(double value)
		{
			this = new SqlMoney(new decimal(value));
		}

		// Token: 0x17000331 RID: 817
		// (get) Token: 0x06001698 RID: 5784 RVA: 0x000A5CB0 File Offset: 0x000A50B0
		public bool IsNull
		{
			get
			{
				return !this.m_fNotNull;
			}
		}

		// Token: 0x17000332 RID: 818
		// (get) Token: 0x06001699 RID: 5785 RVA: 0x000A5CC8 File Offset: 0x000A50C8
		public decimal Value
		{
			get
			{
				if (this.m_fNotNull)
				{
					return this.ToDecimal();
				}
				throw new SqlNullValueException();
			}
		}

		// Token: 0x0600169A RID: 5786 RVA: 0x000A5CEC File Offset: 0x000A50EC
		public decimal ToDecimal()
		{
			if (this.IsNull)
			{
				throw new SqlNullValueException();
			}
			bool isNegative = false;
			long num = this.m_value;
			if (this.m_value < 0L)
			{
				isNegative = true;
				num = -this.m_value;
			}
			return new decimal((int)num, (int)(num >> 32), 0, isNegative, 4);
		}

		// Token: 0x0600169B RID: 5787 RVA: 0x000A5D34 File Offset: 0x000A5134
		public long ToInt64()
		{
			if (this.IsNull)
			{
				throw new SqlNullValueException();
			}
			long num = this.m_value / 1000L;
			bool flag = num >= 0L;
			long num2 = num % 10L;
			num /= 10L;
			if (num2 >= 5L)
			{
				if (flag)
				{
					num += 1L;
				}
				else
				{
					num -= 1L;
				}
			}
			return num;
		}

		// Token: 0x0600169C RID: 5788 RVA: 0x000A5D88 File Offset: 0x000A5188
		internal long ToSqlInternalRepresentation()
		{
			if (this.IsNull)
			{
				throw new SqlNullValueException();
			}
			return this.m_value;
		}

		// Token: 0x0600169D RID: 5789 RVA: 0x000A5DAC File Offset: 0x000A51AC
		public int ToInt32()
		{
			return checked((int)this.ToInt64());
		}

		// Token: 0x0600169E RID: 5790 RVA: 0x000A5DC0 File Offset: 0x000A51C0
		public double ToDouble()
		{
			return decimal.ToDouble(this.ToDecimal());
		}

		// Token: 0x0600169F RID: 5791 RVA: 0x000A5DD8 File Offset: 0x000A51D8
		public static implicit operator SqlMoney(decimal x)
		{
			return new SqlMoney(x);
		}

		// Token: 0x060016A0 RID: 5792 RVA: 0x000A5DEC File Offset: 0x000A51EC
		public static explicit operator SqlMoney(double x)
		{
			return new SqlMoney(x);
		}

		// Token: 0x060016A1 RID: 5793 RVA: 0x000A5E00 File Offset: 0x000A5200
		public static implicit operator SqlMoney(long x)
		{
			return new SqlMoney(new decimal(x));
		}

		// Token: 0x060016A2 RID: 5794 RVA: 0x000A5E18 File Offset: 0x000A5218
		public static explicit operator decimal(SqlMoney x)
		{
			return x.Value;
		}

		// Token: 0x060016A3 RID: 5795 RVA: 0x000A5E2C File Offset: 0x000A522C
		public override string ToString()
		{
			if (this.IsNull)
			{
				return SQLResource.NullString;
			}
			return this.ToDecimal().ToString("#0.00##", null);
		}

		// Token: 0x060016A4 RID: 5796 RVA: 0x000A5E5C File Offset: 0x000A525C
		public static SqlMoney Parse(string s)
		{
			SqlMoney @null;
			decimal value;
			if (s == SQLResource.NullString)
			{
				@null = SqlMoney.Null;
			}
			else if (decimal.TryParse(s, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingSign | NumberStyles.AllowTrailingSign | NumberStyles.AllowParentheses | NumberStyles.AllowDecimalPoint | NumberStyles.AllowCurrencySymbol, NumberFormatInfo.InvariantInfo, out value))
			{
				@null = new SqlMoney(value);
			}
			else
			{
				@null = new SqlMoney(decimal.Parse(s, NumberStyles.Currency, NumberFormatInfo.CurrentInfo));
			}
			return @null;
		}

		// Token: 0x060016A5 RID: 5797 RVA: 0x000A5EB4 File Offset: 0x000A52B4
		public static SqlMoney operator -(SqlMoney x)
		{
			if (x.IsNull)
			{
				return SqlMoney.Null;
			}
			if (x.m_value == -922337203685477L)
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			return new SqlMoney(-x.m_value, 0);
		}

		// Token: 0x060016A6 RID: 5798 RVA: 0x000A5EFC File Offset: 0x000A52FC
		public static SqlMoney operator +(SqlMoney x, SqlMoney y)
		{
			SqlMoney result;
			try
			{
				result = ((x.IsNull || y.IsNull) ? SqlMoney.Null : new SqlMoney(checked(x.m_value + y.m_value), 0));
			}
			catch (OverflowException)
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			return result;
		}

		// Token: 0x060016A7 RID: 5799 RVA: 0x000A5F64 File Offset: 0x000A5364
		public static SqlMoney operator -(SqlMoney x, SqlMoney y)
		{
			SqlMoney result;
			try
			{
				result = ((x.IsNull || y.IsNull) ? SqlMoney.Null : new SqlMoney(checked(x.m_value - y.m_value), 0));
			}
			catch (OverflowException)
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			return result;
		}

		// Token: 0x060016A8 RID: 5800 RVA: 0x000A5FCC File Offset: 0x000A53CC
		public static SqlMoney operator *(SqlMoney x, SqlMoney y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlMoney(decimal.Multiply(x.ToDecimal(), y.ToDecimal()));
			}
			return SqlMoney.Null;
		}

		// Token: 0x060016A9 RID: 5801 RVA: 0x000A600C File Offset: 0x000A540C
		public static SqlMoney operator /(SqlMoney x, SqlMoney y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlMoney(decimal.Divide(x.ToDecimal(), y.ToDecimal()));
			}
			return SqlMoney.Null;
		}

		// Token: 0x060016AA RID: 5802 RVA: 0x000A604C File Offset: 0x000A544C
		public static explicit operator SqlMoney(SqlBoolean x)
		{
			if (!x.IsNull)
			{
				return new SqlMoney((int)x.ByteValue);
			}
			return SqlMoney.Null;
		}

		// Token: 0x060016AB RID: 5803 RVA: 0x000A6074 File Offset: 0x000A5474
		public static implicit operator SqlMoney(SqlByte x)
		{
			if (!x.IsNull)
			{
				return new SqlMoney((int)x.Value);
			}
			return SqlMoney.Null;
		}

		// Token: 0x060016AC RID: 5804 RVA: 0x000A609C File Offset: 0x000A549C
		public static implicit operator SqlMoney(SqlInt16 x)
		{
			if (!x.IsNull)
			{
				return new SqlMoney((int)x.Value);
			}
			return SqlMoney.Null;
		}

		// Token: 0x060016AD RID: 5805 RVA: 0x000A60C4 File Offset: 0x000A54C4
		public static implicit operator SqlMoney(SqlInt32 x)
		{
			if (!x.IsNull)
			{
				return new SqlMoney(x.Value);
			}
			return SqlMoney.Null;
		}

		// Token: 0x060016AE RID: 5806 RVA: 0x000A60EC File Offset: 0x000A54EC
		public static implicit operator SqlMoney(SqlInt64 x)
		{
			if (!x.IsNull)
			{
				return new SqlMoney(x.Value);
			}
			return SqlMoney.Null;
		}

		// Token: 0x060016AF RID: 5807 RVA: 0x000A6114 File Offset: 0x000A5514
		public static explicit operator SqlMoney(SqlSingle x)
		{
			if (!x.IsNull)
			{
				return new SqlMoney((double)x.Value);
			}
			return SqlMoney.Null;
		}

		// Token: 0x060016B0 RID: 5808 RVA: 0x000A6140 File Offset: 0x000A5540
		public static explicit operator SqlMoney(SqlDouble x)
		{
			if (!x.IsNull)
			{
				return new SqlMoney(x.Value);
			}
			return SqlMoney.Null;
		}

		// Token: 0x060016B1 RID: 5809 RVA: 0x000A6168 File Offset: 0x000A5568
		public static explicit operator SqlMoney(SqlDecimal x)
		{
			if (!x.IsNull)
			{
				return new SqlMoney(x.Value);
			}
			return SqlMoney.Null;
		}

		// Token: 0x060016B2 RID: 5810 RVA: 0x000A6190 File Offset: 0x000A5590
		public static explicit operator SqlMoney(SqlString x)
		{
			if (!x.IsNull)
			{
				return new SqlMoney(decimal.Parse(x.Value, NumberStyles.Currency, null));
			}
			return SqlMoney.Null;
		}

		// Token: 0x060016B3 RID: 5811 RVA: 0x000A61C4 File Offset: 0x000A55C4
		public static SqlBoolean operator ==(SqlMoney x, SqlMoney y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.m_value == y.m_value);
			}
			return SqlBoolean.Null;
		}

		// Token: 0x060016B4 RID: 5812 RVA: 0x000A61FC File Offset: 0x000A55FC
		public static SqlBoolean operator !=(SqlMoney x, SqlMoney y)
		{
			return !(x == y);
		}

		// Token: 0x060016B5 RID: 5813 RVA: 0x000A6218 File Offset: 0x000A5618
		public static SqlBoolean operator <(SqlMoney x, SqlMoney y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.m_value < y.m_value);
			}
			return SqlBoolean.Null;
		}

		// Token: 0x060016B6 RID: 5814 RVA: 0x000A6250 File Offset: 0x000A5650
		public static SqlBoolean operator >(SqlMoney x, SqlMoney y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.m_value > y.m_value);
			}
			return SqlBoolean.Null;
		}

		// Token: 0x060016B7 RID: 5815 RVA: 0x000A6288 File Offset: 0x000A5688
		public static SqlBoolean operator <=(SqlMoney x, SqlMoney y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.m_value <= y.m_value);
			}
			return SqlBoolean.Null;
		}

		// Token: 0x060016B8 RID: 5816 RVA: 0x000A62C4 File Offset: 0x000A56C4
		public static SqlBoolean operator >=(SqlMoney x, SqlMoney y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.m_value >= y.m_value);
			}
			return SqlBoolean.Null;
		}

		// Token: 0x060016B9 RID: 5817 RVA: 0x000A6300 File Offset: 0x000A5700
		public static SqlMoney Add(SqlMoney x, SqlMoney y)
		{
			return x + y;
		}

		// Token: 0x060016BA RID: 5818 RVA: 0x000A6314 File Offset: 0x000A5714
		public static SqlMoney Subtract(SqlMoney x, SqlMoney y)
		{
			return x - y;
		}

		// Token: 0x060016BB RID: 5819 RVA: 0x000A6328 File Offset: 0x000A5728
		public static SqlMoney Multiply(SqlMoney x, SqlMoney y)
		{
			return x * y;
		}

		// Token: 0x060016BC RID: 5820 RVA: 0x000A633C File Offset: 0x000A573C
		public static SqlMoney Divide(SqlMoney x, SqlMoney y)
		{
			return x / y;
		}

		// Token: 0x060016BD RID: 5821 RVA: 0x000A6350 File Offset: 0x000A5750
		public static SqlBoolean Equals(SqlMoney x, SqlMoney y)
		{
			return x == y;
		}

		// Token: 0x060016BE RID: 5822 RVA: 0x000A6364 File Offset: 0x000A5764
		public static SqlBoolean NotEquals(SqlMoney x, SqlMoney y)
		{
			return x != y;
		}

		// Token: 0x060016BF RID: 5823 RVA: 0x000A6378 File Offset: 0x000A5778
		public static SqlBoolean LessThan(SqlMoney x, SqlMoney y)
		{
			return x < y;
		}

		// Token: 0x060016C0 RID: 5824 RVA: 0x000A638C File Offset: 0x000A578C
		public static SqlBoolean GreaterThan(SqlMoney x, SqlMoney y)
		{
			return x > y;
		}

		// Token: 0x060016C1 RID: 5825 RVA: 0x000A63A0 File Offset: 0x000A57A0
		public static SqlBoolean LessThanOrEqual(SqlMoney x, SqlMoney y)
		{
			return x <= y;
		}

		// Token: 0x060016C2 RID: 5826 RVA: 0x000A63B4 File Offset: 0x000A57B4
		public static SqlBoolean GreaterThanOrEqual(SqlMoney x, SqlMoney y)
		{
			return x >= y;
		}

		// Token: 0x060016C3 RID: 5827 RVA: 0x000A63C8 File Offset: 0x000A57C8
		public SqlBoolean ToSqlBoolean()
		{
			return (SqlBoolean)this;
		}

		// Token: 0x060016C4 RID: 5828 RVA: 0x000A63E0 File Offset: 0x000A57E0
		public SqlByte ToSqlByte()
		{
			return (SqlByte)this;
		}

		// Token: 0x060016C5 RID: 5829 RVA: 0x000A63F8 File Offset: 0x000A57F8
		public SqlDouble ToSqlDouble()
		{
			return this;
		}

		// Token: 0x060016C6 RID: 5830 RVA: 0x000A6410 File Offset: 0x000A5810
		public SqlInt16 ToSqlInt16()
		{
			return (SqlInt16)this;
		}

		// Token: 0x060016C7 RID: 5831 RVA: 0x000A6428 File Offset: 0x000A5828
		public SqlInt32 ToSqlInt32()
		{
			return (SqlInt32)this;
		}

		// Token: 0x060016C8 RID: 5832 RVA: 0x000A6440 File Offset: 0x000A5840
		public SqlInt64 ToSqlInt64()
		{
			return (SqlInt64)this;
		}

		// Token: 0x060016C9 RID: 5833 RVA: 0x000A6458 File Offset: 0x000A5858
		public SqlDecimal ToSqlDecimal()
		{
			return this;
		}

		// Token: 0x060016CA RID: 5834 RVA: 0x000A6470 File Offset: 0x000A5870
		public SqlSingle ToSqlSingle()
		{
			return this;
		}

		// Token: 0x060016CB RID: 5835 RVA: 0x000A6488 File Offset: 0x000A5888
		public SqlString ToSqlString()
		{
			return (SqlString)this;
		}

		// Token: 0x060016CC RID: 5836 RVA: 0x000A64A0 File Offset: 0x000A58A0
		public int CompareTo(object value)
		{
			if (value is SqlMoney)
			{
				SqlMoney value2 = (SqlMoney)value;
				return this.CompareTo(value2);
			}
			throw ADP.WrongType(value.GetType(), typeof(SqlMoney));
		}

		// Token: 0x060016CD RID: 5837 RVA: 0x000A64DC File Offset: 0x000A58DC
		public int CompareTo(SqlMoney value)
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

		// Token: 0x060016CE RID: 5838 RVA: 0x000A6534 File Offset: 0x000A5934
		public override bool Equals(object value)
		{
			if (!(value is SqlMoney))
			{
				return false;
			}
			SqlMoney y = (SqlMoney)value;
			if (y.IsNull || this.IsNull)
			{
				return y.IsNull && this.IsNull;
			}
			return (this == y).Value;
		}

		// Token: 0x060016CF RID: 5839 RVA: 0x000A658C File Offset: 0x000A598C
		public override int GetHashCode()
		{
			if (!this.IsNull)
			{
				return this.m_value.GetHashCode();
			}
			return 0;
		}

		// Token: 0x060016D0 RID: 5840 RVA: 0x000A65B0 File Offset: 0x000A59B0
		XmlSchema IXmlSerializable.GetSchema()
		{
			return null;
		}

		// Token: 0x060016D1 RID: 5841 RVA: 0x000A65C0 File Offset: 0x000A59C0
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			string attribute = reader.GetAttribute("nil", "http://www.w3.org/2001/XMLSchema-instance");
			if (attribute != null && XmlConvert.ToBoolean(attribute))
			{
				reader.ReadElementString();
				this.m_fNotNull = false;
				return;
			}
			SqlMoney sqlMoney = new SqlMoney(XmlConvert.ToDecimal(reader.ReadElementString()));
			this.m_fNotNull = sqlMoney.m_fNotNull;
			this.m_value = sqlMoney.m_value;
		}

		// Token: 0x060016D2 RID: 5842 RVA: 0x000A6624 File Offset: 0x000A5A24
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			if (this.IsNull)
			{
				writer.WriteAttributeString("xsi", "nil", "http://www.w3.org/2001/XMLSchema-instance", "true");
				return;
			}
			writer.WriteString(XmlConvert.ToString(this.ToDecimal()));
		}

		// Token: 0x060016D3 RID: 5843 RVA: 0x000A6668 File Offset: 0x000A5A68
		public static XmlQualifiedName GetXsdType(XmlSchemaSet schemaSet)
		{
			return new XmlQualifiedName("decimal", "http://www.w3.org/2001/XMLSchema");
		}

		// Token: 0x04000DFF RID: 3583
		private bool m_fNotNull;

		// Token: 0x04000E00 RID: 3584
		private long m_value;

		// Token: 0x04000E01 RID: 3585
		internal const int x_iMoneyScale = 4;

		// Token: 0x04000E02 RID: 3586
		private const long x_lTickBase = 10000L;

		// Token: 0x04000E03 RID: 3587
		private const double x_dTickBase = 10000.0;

		// Token: 0x04000E04 RID: 3588
		private const long MinLong = -922337203685477L;

		// Token: 0x04000E05 RID: 3589
		private const long MaxLong = 922337203685477L;

		// Token: 0x04000E06 RID: 3590
		public static readonly SqlMoney Null = new SqlMoney(true);

		// Token: 0x04000E07 RID: 3591
		public static readonly SqlMoney Zero = new SqlMoney(0);

		// Token: 0x04000E08 RID: 3592
		public static readonly SqlMoney MinValue = new SqlMoney(long.MinValue, 0);

		// Token: 0x04000E09 RID: 3593
		public static readonly SqlMoney MaxValue = new SqlMoney(long.MaxValue, 0);
	}
}
