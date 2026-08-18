using System;
using System.Data.Common;
using System.Globalization;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System.Data.SqlTypes
{
	// Token: 0x02000353 RID: 851
	[XmlSchemaProvider("GetXsdType")]
	[Serializable]
	public struct SqlMoney : INullable, IComparable, IXmlSerializable
	{
		// Token: 0x06002E3D RID: 11837 RVA: 0x002D0878 File Offset: 0x002CFC78
		private SqlMoney(bool fNull)
		{
			this.m_fNotNull = false;
			this.m_value = 0L;
		}

		// Token: 0x06002E3E RID: 11838 RVA: 0x002D0898 File Offset: 0x002CFC98
		internal SqlMoney(long value, int ignored)
		{
			this.m_value = value;
			this.m_fNotNull = true;
		}

		// Token: 0x06002E3F RID: 11839 RVA: 0x002D08B8 File Offset: 0x002CFCB8
		public SqlMoney(int value)
		{
			this.m_value = (long)value * SqlMoney.x_lTickBase;
			this.m_fNotNull = true;
		}

		// Token: 0x06002E40 RID: 11840 RVA: 0x002D08E8 File Offset: 0x002CFCE8
		public SqlMoney(long value)
		{
			if (value < SqlMoney.MinLong || value > SqlMoney.MaxLong)
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			this.m_value = value * SqlMoney.x_lTickBase;
			this.m_fNotNull = true;
		}

		// Token: 0x06002E41 RID: 11841 RVA: 0x002D0928 File Offset: 0x002CFD28
		public SqlMoney(decimal value)
		{
			SqlDecimal sqlDecimal = new SqlDecimal(value);
			sqlDecimal.AdjustScale(SqlMoney.x_iMoneyScale - (int)sqlDecimal.Scale, true);
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

		// Token: 0x06002E42 RID: 11842 RVA: 0x002D09D8 File Offset: 0x002CFDD8
		public SqlMoney(double value)
		{
			this = new SqlMoney(new decimal(value));
		}

		// Token: 0x17000758 RID: 1880
		// (get) Token: 0x06002E43 RID: 11843 RVA: 0x002D09F8 File Offset: 0x002CFDF8
		public bool IsNull
		{
			get
			{
				return !this.m_fNotNull;
			}
		}

		// Token: 0x17000759 RID: 1881
		// (get) Token: 0x06002E44 RID: 11844 RVA: 0x002D0A18 File Offset: 0x002CFE18
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

		// Token: 0x06002E45 RID: 11845 RVA: 0x002D0A48 File Offset: 0x002CFE48
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
			return new decimal((int)num, (int)(num >> 32), 0, isNegative, (byte)SqlMoney.x_iMoneyScale);
		}

		// Token: 0x06002E46 RID: 11846 RVA: 0x002D0A98 File Offset: 0x002CFE98
		public long ToInt64()
		{
			if (this.IsNull)
			{
				throw new SqlNullValueException();
			}
			long num = this.m_value / (SqlMoney.x_lTickBase / 10L);
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

		// Token: 0x06002E47 RID: 11847 RVA: 0x002D0AF8 File Offset: 0x002CFEF8
		internal long ToSqlInternalRepresentation()
		{
			if (this.IsNull)
			{
				throw new SqlNullValueException();
			}
			return this.m_value;
		}

		// Token: 0x06002E48 RID: 11848 RVA: 0x002D0B28 File Offset: 0x002CFF28
		public int ToInt32()
		{
			return checked((int)this.ToInt64());
		}

		// Token: 0x06002E49 RID: 11849 RVA: 0x002D0B48 File Offset: 0x002CFF48
		public double ToDouble()
		{
			return decimal.ToDouble(this.ToDecimal());
		}

		// Token: 0x06002E4A RID: 11850 RVA: 0x002D0B68 File Offset: 0x002CFF68
		public static implicit operator SqlMoney(decimal x)
		{
			return new SqlMoney(x);
		}

		// Token: 0x06002E4B RID: 11851 RVA: 0x002D0B88 File Offset: 0x002CFF88
		public static explicit operator SqlMoney(double x)
		{
			return new SqlMoney(x);
		}

		// Token: 0x06002E4C RID: 11852 RVA: 0x002D0BA8 File Offset: 0x002CFFA8
		public static implicit operator SqlMoney(long x)
		{
			return new SqlMoney(new decimal(x));
		}

		// Token: 0x06002E4D RID: 11853 RVA: 0x002D0BC8 File Offset: 0x002CFFC8
		public static explicit operator decimal(SqlMoney x)
		{
			return x.Value;
		}

		// Token: 0x06002E4E RID: 11854 RVA: 0x002D0BE8 File Offset: 0x002CFFE8
		public override string ToString()
		{
			if (this.IsNull)
			{
				return SQLResource.NullString;
			}
			return this.ToDecimal().ToString("#0.00##", null);
		}

		// Token: 0x06002E4F RID: 11855 RVA: 0x002D0C18 File Offset: 0x002D0018
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

		// Token: 0x06002E50 RID: 11856 RVA: 0x002D0C78 File Offset: 0x002D0078
		public static SqlMoney operator -(SqlMoney x)
		{
			if (x.IsNull)
			{
				return SqlMoney.Null;
			}
			if (x.m_value == SqlMoney.MinLong)
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			return new SqlMoney(-x.m_value, 0);
		}

		// Token: 0x06002E51 RID: 11857 RVA: 0x002D0CC8 File Offset: 0x002D00C8
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

		// Token: 0x06002E52 RID: 11858 RVA: 0x002D0D38 File Offset: 0x002D0138
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

		// Token: 0x06002E53 RID: 11859 RVA: 0x002D0DA8 File Offset: 0x002D01A8
		public static SqlMoney operator *(SqlMoney x, SqlMoney y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlMoney(decimal.Multiply(x.ToDecimal(), y.ToDecimal()));
			}
			return SqlMoney.Null;
		}

		// Token: 0x06002E54 RID: 11860 RVA: 0x002D0DE8 File Offset: 0x002D01E8
		public static SqlMoney operator /(SqlMoney x, SqlMoney y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlMoney(decimal.Divide(x.ToDecimal(), y.ToDecimal()));
			}
			return SqlMoney.Null;
		}

		// Token: 0x06002E55 RID: 11861 RVA: 0x002D0E28 File Offset: 0x002D0228
		public static explicit operator SqlMoney(SqlBoolean x)
		{
			if (!x.IsNull)
			{
				return new SqlMoney((int)x.ByteValue);
			}
			return SqlMoney.Null;
		}

		// Token: 0x06002E56 RID: 11862 RVA: 0x002D0E58 File Offset: 0x002D0258
		public static implicit operator SqlMoney(SqlByte x)
		{
			if (!x.IsNull)
			{
				return new SqlMoney((int)x.Value);
			}
			return SqlMoney.Null;
		}

		// Token: 0x06002E57 RID: 11863 RVA: 0x002D0E88 File Offset: 0x002D0288
		public static implicit operator SqlMoney(SqlInt16 x)
		{
			if (!x.IsNull)
			{
				return new SqlMoney((int)x.Value);
			}
			return SqlMoney.Null;
		}

		// Token: 0x06002E58 RID: 11864 RVA: 0x002D0EB8 File Offset: 0x002D02B8
		public static implicit operator SqlMoney(SqlInt32 x)
		{
			if (!x.IsNull)
			{
				return new SqlMoney(x.Value);
			}
			return SqlMoney.Null;
		}

		// Token: 0x06002E59 RID: 11865 RVA: 0x002D0EE8 File Offset: 0x002D02E8
		public static implicit operator SqlMoney(SqlInt64 x)
		{
			if (!x.IsNull)
			{
				return new SqlMoney(x.Value);
			}
			return SqlMoney.Null;
		}

		// Token: 0x06002E5A RID: 11866 RVA: 0x002D0F18 File Offset: 0x002D0318
		public static explicit operator SqlMoney(SqlSingle x)
		{
			if (!x.IsNull)
			{
				return new SqlMoney((double)x.Value);
			}
			return SqlMoney.Null;
		}

		// Token: 0x06002E5B RID: 11867 RVA: 0x002D0F48 File Offset: 0x002D0348
		public static explicit operator SqlMoney(SqlDouble x)
		{
			if (!x.IsNull)
			{
				return new SqlMoney(x.Value);
			}
			return SqlMoney.Null;
		}

		// Token: 0x06002E5C RID: 11868 RVA: 0x002D0F78 File Offset: 0x002D0378
		public static explicit operator SqlMoney(SqlDecimal x)
		{
			if (!x.IsNull)
			{
				return new SqlMoney(x.Value);
			}
			return SqlMoney.Null;
		}

		// Token: 0x06002E5D RID: 11869 RVA: 0x002D0FA8 File Offset: 0x002D03A8
		public static explicit operator SqlMoney(SqlString x)
		{
			if (!x.IsNull)
			{
				return new SqlMoney(decimal.Parse(x.Value, NumberStyles.Currency, null));
			}
			return SqlMoney.Null;
		}

		// Token: 0x06002E5E RID: 11870 RVA: 0x002D0FE8 File Offset: 0x002D03E8
		public static SqlBoolean operator ==(SqlMoney x, SqlMoney y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.m_value == y.m_value);
			}
			return SqlBoolean.Null;
		}

		// Token: 0x06002E5F RID: 11871 RVA: 0x002D1028 File Offset: 0x002D0428
		public static SqlBoolean operator !=(SqlMoney x, SqlMoney y)
		{
			return !(x == y);
		}

		// Token: 0x06002E60 RID: 11872 RVA: 0x002D1048 File Offset: 0x002D0448
		public static SqlBoolean operator <(SqlMoney x, SqlMoney y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.m_value < y.m_value);
			}
			return SqlBoolean.Null;
		}

		// Token: 0x06002E61 RID: 11873 RVA: 0x002D1088 File Offset: 0x002D0488
		public static SqlBoolean operator >(SqlMoney x, SqlMoney y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.m_value > y.m_value);
			}
			return SqlBoolean.Null;
		}

		// Token: 0x06002E62 RID: 11874 RVA: 0x002D10C8 File Offset: 0x002D04C8
		public static SqlBoolean operator <=(SqlMoney x, SqlMoney y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.m_value <= y.m_value);
			}
			return SqlBoolean.Null;
		}

		// Token: 0x06002E63 RID: 11875 RVA: 0x002D1108 File Offset: 0x002D0508
		public static SqlBoolean operator >=(SqlMoney x, SqlMoney y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.m_value >= y.m_value);
			}
			return SqlBoolean.Null;
		}

		// Token: 0x06002E64 RID: 11876 RVA: 0x002D1148 File Offset: 0x002D0548
		public static SqlMoney Add(SqlMoney x, SqlMoney y)
		{
			return x + y;
		}

		// Token: 0x06002E65 RID: 11877 RVA: 0x002D1168 File Offset: 0x002D0568
		public static SqlMoney Subtract(SqlMoney x, SqlMoney y)
		{
			return x - y;
		}

		// Token: 0x06002E66 RID: 11878 RVA: 0x002D1188 File Offset: 0x002D0588
		public static SqlMoney Multiply(SqlMoney x, SqlMoney y)
		{
			return x * y;
		}

		// Token: 0x06002E67 RID: 11879 RVA: 0x002D11A8 File Offset: 0x002D05A8
		public static SqlMoney Divide(SqlMoney x, SqlMoney y)
		{
			return x / y;
		}

		// Token: 0x06002E68 RID: 11880 RVA: 0x002D11C8 File Offset: 0x002D05C8
		public static SqlBoolean Equals(SqlMoney x, SqlMoney y)
		{
			return x == y;
		}

		// Token: 0x06002E69 RID: 11881 RVA: 0x002D11E8 File Offset: 0x002D05E8
		public static SqlBoolean NotEquals(SqlMoney x, SqlMoney y)
		{
			return x != y;
		}

		// Token: 0x06002E6A RID: 11882 RVA: 0x002D1208 File Offset: 0x002D0608
		public static SqlBoolean LessThan(SqlMoney x, SqlMoney y)
		{
			return x < y;
		}

		// Token: 0x06002E6B RID: 11883 RVA: 0x002D1228 File Offset: 0x002D0628
		public static SqlBoolean GreaterThan(SqlMoney x, SqlMoney y)
		{
			return x > y;
		}

		// Token: 0x06002E6C RID: 11884 RVA: 0x002D1248 File Offset: 0x002D0648
		public static SqlBoolean LessThanOrEqual(SqlMoney x, SqlMoney y)
		{
			return x <= y;
		}

		// Token: 0x06002E6D RID: 11885 RVA: 0x002D1268 File Offset: 0x002D0668
		public static SqlBoolean GreaterThanOrEqual(SqlMoney x, SqlMoney y)
		{
			return x >= y;
		}

		// Token: 0x06002E6E RID: 11886 RVA: 0x002D1288 File Offset: 0x002D0688
		public SqlBoolean ToSqlBoolean()
		{
			return (SqlBoolean)this;
		}

		// Token: 0x06002E6F RID: 11887 RVA: 0x002D12A8 File Offset: 0x002D06A8
		public SqlByte ToSqlByte()
		{
			return (SqlByte)this;
		}

		// Token: 0x06002E70 RID: 11888 RVA: 0x002D12C8 File Offset: 0x002D06C8
		public SqlDouble ToSqlDouble()
		{
			return this;
		}

		// Token: 0x06002E71 RID: 11889 RVA: 0x002D12E8 File Offset: 0x002D06E8
		public SqlInt16 ToSqlInt16()
		{
			return (SqlInt16)this;
		}

		// Token: 0x06002E72 RID: 11890 RVA: 0x002D1308 File Offset: 0x002D0708
		public SqlInt32 ToSqlInt32()
		{
			return (SqlInt32)this;
		}

		// Token: 0x06002E73 RID: 11891 RVA: 0x002D1328 File Offset: 0x002D0728
		public SqlInt64 ToSqlInt64()
		{
			return (SqlInt64)this;
		}

		// Token: 0x06002E74 RID: 11892 RVA: 0x002D1348 File Offset: 0x002D0748
		public SqlDecimal ToSqlDecimal()
		{
			return this;
		}

		// Token: 0x06002E75 RID: 11893 RVA: 0x002D1368 File Offset: 0x002D0768
		public SqlSingle ToSqlSingle()
		{
			return this;
		}

		// Token: 0x06002E76 RID: 11894 RVA: 0x002D1388 File Offset: 0x002D0788
		public SqlString ToSqlString()
		{
			return (SqlString)this;
		}

		// Token: 0x06002E77 RID: 11895 RVA: 0x002D13A8 File Offset: 0x002D07A8
		public int CompareTo(object value)
		{
			if (value is SqlMoney)
			{
				SqlMoney value2 = (SqlMoney)value;
				return this.CompareTo(value2);
			}
			throw ADP.WrongType(value.GetType(), typeof(SqlMoney));
		}

		// Token: 0x06002E78 RID: 11896 RVA: 0x002D13E8 File Offset: 0x002D07E8
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

		// Token: 0x06002E79 RID: 11897 RVA: 0x002D1448 File Offset: 0x002D0848
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

		// Token: 0x06002E7A RID: 11898 RVA: 0x002D14A8 File Offset: 0x002D08A8
		public override int GetHashCode()
		{
			if (!this.IsNull)
			{
				return this.m_value.GetHashCode();
			}
			return 0;
		}

		// Token: 0x06002E7B RID: 11899 RVA: 0x002D14D8 File Offset: 0x002D08D8
		XmlSchema IXmlSerializable.GetSchema()
		{
			return null;
		}

		// Token: 0x06002E7C RID: 11900 RVA: 0x002D14E8 File Offset: 0x002D08E8
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			string attribute = reader.GetAttribute("nil", "http://www.w3.org/2001/XMLSchema-instance");
			if (attribute != null && XmlConvert.ToBoolean(attribute))
			{
				this.m_fNotNull = false;
				return;
			}
			SqlMoney sqlMoney = new SqlMoney(XmlConvert.ToDecimal(reader.ReadElementString()));
			this.m_fNotNull = sqlMoney.m_fNotNull;
			this.m_value = sqlMoney.m_value;
		}

		// Token: 0x06002E7D RID: 11901 RVA: 0x002D1548 File Offset: 0x002D0948
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			if (this.IsNull)
			{
				writer.WriteAttributeString("xsi", "nil", "http://www.w3.org/2001/XMLSchema-instance", "true");
				return;
			}
			writer.WriteString(XmlConvert.ToString(this.ToDecimal()));
		}

		// Token: 0x06002E7E RID: 11902 RVA: 0x002D1598 File Offset: 0x002D0998
		public static XmlQualifiedName GetXsdType(XmlSchemaSet schemaSet)
		{
			return new XmlQualifiedName("decimal", "http://www.w3.org/2001/XMLSchema");
		}

		// Token: 0x04001D16 RID: 7446
		private bool m_fNotNull;

		// Token: 0x04001D17 RID: 7447
		private long m_value;

		// Token: 0x04001D18 RID: 7448
		internal static readonly int x_iMoneyScale = 4;

		// Token: 0x04001D19 RID: 7449
		private static readonly long x_lTickBase = 10000L;

		// Token: 0x04001D1A RID: 7450
		private static readonly double x_dTickBase = (double)SqlMoney.x_lTickBase;

		// Token: 0x04001D1B RID: 7451
		private static readonly long MinLong = long.MinValue / SqlMoney.x_lTickBase;

		// Token: 0x04001D1C RID: 7452
		private static readonly long MaxLong = long.MaxValue / SqlMoney.x_lTickBase;

		// Token: 0x04001D1D RID: 7453
		public static readonly SqlMoney Null = new SqlMoney(true);

		// Token: 0x04001D1E RID: 7454
		public static readonly SqlMoney Zero = new SqlMoney(0);

		// Token: 0x04001D1F RID: 7455
		public static readonly SqlMoney MinValue = new SqlMoney(long.MinValue, 0);

		// Token: 0x04001D20 RID: 7456
		public static readonly SqlMoney MaxValue = new SqlMoney(long.MaxValue, 0);
	}
}
