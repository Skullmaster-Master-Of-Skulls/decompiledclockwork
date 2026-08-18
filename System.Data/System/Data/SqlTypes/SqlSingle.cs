using System;
using System.Data.Common;
using System.Globalization;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System.Data.SqlTypes
{
	// Token: 0x02000355 RID: 853
	[XmlSchemaProvider("GetXsdType")]
	[Serializable]
	public struct SqlSingle : INullable, IComparable, IXmlSerializable
	{
		// Token: 0x06002E86 RID: 11910 RVA: 0x002D1868 File Offset: 0x002D0C68
		private SqlSingle(bool fNull)
		{
			this.m_fNotNull = false;
			this.m_value = 0f;
		}

		// Token: 0x06002E87 RID: 11911 RVA: 0x002D1888 File Offset: 0x002D0C88
		public SqlSingle(float value)
		{
			if (float.IsInfinity(value) || float.IsNaN(value))
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			this.m_fNotNull = true;
			this.m_value = value;
		}

		// Token: 0x06002E88 RID: 11912 RVA: 0x002D18C8 File Offset: 0x002D0CC8
		public SqlSingle(double value)
		{
			this = new SqlSingle((float)value);
		}

		// Token: 0x1700075A RID: 1882
		// (get) Token: 0x06002E89 RID: 11913 RVA: 0x002D18E8 File Offset: 0x002D0CE8
		public bool IsNull
		{
			get
			{
				return !this.m_fNotNull;
			}
		}

		// Token: 0x1700075B RID: 1883
		// (get) Token: 0x06002E8A RID: 11914 RVA: 0x002D1908 File Offset: 0x002D0D08
		public float Value
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

		// Token: 0x06002E8B RID: 11915 RVA: 0x002D1938 File Offset: 0x002D0D38
		public static implicit operator SqlSingle(float x)
		{
			return new SqlSingle(x);
		}

		// Token: 0x06002E8C RID: 11916 RVA: 0x002D1958 File Offset: 0x002D0D58
		public static explicit operator float(SqlSingle x)
		{
			return x.Value;
		}

		// Token: 0x06002E8D RID: 11917 RVA: 0x002D1978 File Offset: 0x002D0D78
		public override string ToString()
		{
			if (!this.IsNull)
			{
				return this.m_value.ToString(null);
			}
			return SQLResource.NullString;
		}

		// Token: 0x06002E8E RID: 11918 RVA: 0x002D19A8 File Offset: 0x002D0DA8
		public static SqlSingle Parse(string s)
		{
			if (s == SQLResource.NullString)
			{
				return SqlSingle.Null;
			}
			return new SqlSingle(float.Parse(s, CultureInfo.InvariantCulture));
		}

		// Token: 0x06002E8F RID: 11919 RVA: 0x002D19D8 File Offset: 0x002D0DD8
		public static SqlSingle operator -(SqlSingle x)
		{
			if (!x.IsNull)
			{
				return new SqlSingle(-x.m_value);
			}
			return SqlSingle.Null;
		}

		// Token: 0x06002E90 RID: 11920 RVA: 0x002D1A08 File Offset: 0x002D0E08
		public static SqlSingle operator +(SqlSingle x, SqlSingle y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlSingle.Null;
			}
			float num = x.m_value + y.m_value;
			if (float.IsInfinity(num))
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			return new SqlSingle(num);
		}

		// Token: 0x06002E91 RID: 11921 RVA: 0x002D1A58 File Offset: 0x002D0E58
		public static SqlSingle operator -(SqlSingle x, SqlSingle y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlSingle.Null;
			}
			float num = x.m_value - y.m_value;
			if (float.IsInfinity(num))
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			return new SqlSingle(num);
		}

		// Token: 0x06002E92 RID: 11922 RVA: 0x002D1AA8 File Offset: 0x002D0EA8
		public static SqlSingle operator *(SqlSingle x, SqlSingle y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlSingle.Null;
			}
			float num = x.m_value * y.m_value;
			if (float.IsInfinity(num))
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			return new SqlSingle(num);
		}

		// Token: 0x06002E93 RID: 11923 RVA: 0x002D1AF8 File Offset: 0x002D0EF8
		public static SqlSingle operator /(SqlSingle x, SqlSingle y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlSingle.Null;
			}
			if (y.m_value == 0f)
			{
				throw new DivideByZeroException(SQLResource.DivideByZeroMessage);
			}
			float num = x.m_value / y.m_value;
			if (float.IsInfinity(num))
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			return new SqlSingle(num);
		}

		// Token: 0x06002E94 RID: 11924 RVA: 0x002D1B68 File Offset: 0x002D0F68
		public static explicit operator SqlSingle(SqlBoolean x)
		{
			if (!x.IsNull)
			{
				return new SqlSingle((float)x.ByteValue);
			}
			return SqlSingle.Null;
		}

		// Token: 0x06002E95 RID: 11925 RVA: 0x002D1B98 File Offset: 0x002D0F98
		public static implicit operator SqlSingle(SqlByte x)
		{
			if (!x.IsNull)
			{
				return new SqlSingle((float)x.Value);
			}
			return SqlSingle.Null;
		}

		// Token: 0x06002E96 RID: 11926 RVA: 0x002D1BC8 File Offset: 0x002D0FC8
		public static implicit operator SqlSingle(SqlInt16 x)
		{
			if (!x.IsNull)
			{
				return new SqlSingle((float)x.Value);
			}
			return SqlSingle.Null;
		}

		// Token: 0x06002E97 RID: 11927 RVA: 0x002D1BF8 File Offset: 0x002D0FF8
		public static implicit operator SqlSingle(SqlInt32 x)
		{
			if (!x.IsNull)
			{
				return new SqlSingle((float)x.Value);
			}
			return SqlSingle.Null;
		}

		// Token: 0x06002E98 RID: 11928 RVA: 0x002D1C28 File Offset: 0x002D1028
		public static implicit operator SqlSingle(SqlInt64 x)
		{
			if (!x.IsNull)
			{
				return new SqlSingle((float)x.Value);
			}
			return SqlSingle.Null;
		}

		// Token: 0x06002E99 RID: 11929 RVA: 0x002D1C58 File Offset: 0x002D1058
		public static implicit operator SqlSingle(SqlMoney x)
		{
			if (!x.IsNull)
			{
				return new SqlSingle(x.ToDouble());
			}
			return SqlSingle.Null;
		}

		// Token: 0x06002E9A RID: 11930 RVA: 0x002D1C88 File Offset: 0x002D1088
		public static implicit operator SqlSingle(SqlDecimal x)
		{
			if (!x.IsNull)
			{
				return new SqlSingle(x.ToDouble());
			}
			return SqlSingle.Null;
		}

		// Token: 0x06002E9B RID: 11931 RVA: 0x002D1CB8 File Offset: 0x002D10B8
		public static explicit operator SqlSingle(SqlDouble x)
		{
			if (!x.IsNull)
			{
				return new SqlSingle(x.Value);
			}
			return SqlSingle.Null;
		}

		// Token: 0x06002E9C RID: 11932 RVA: 0x002D1CE8 File Offset: 0x002D10E8
		public static explicit operator SqlSingle(SqlString x)
		{
			if (x.IsNull)
			{
				return SqlSingle.Null;
			}
			return SqlSingle.Parse(x.Value);
		}

		// Token: 0x06002E9D RID: 11933 RVA: 0x002D1D18 File Offset: 0x002D1118
		public static SqlBoolean operator ==(SqlSingle x, SqlSingle y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.m_value == y.m_value);
			}
			return SqlBoolean.Null;
		}

		// Token: 0x06002E9E RID: 11934 RVA: 0x002D1D58 File Offset: 0x002D1158
		public static SqlBoolean operator !=(SqlSingle x, SqlSingle y)
		{
			return !(x == y);
		}

		// Token: 0x06002E9F RID: 11935 RVA: 0x002D1D78 File Offset: 0x002D1178
		public static SqlBoolean operator <(SqlSingle x, SqlSingle y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.m_value < y.m_value);
			}
			return SqlBoolean.Null;
		}

		// Token: 0x06002EA0 RID: 11936 RVA: 0x002D1DB8 File Offset: 0x002D11B8
		public static SqlBoolean operator >(SqlSingle x, SqlSingle y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.m_value > y.m_value);
			}
			return SqlBoolean.Null;
		}

		// Token: 0x06002EA1 RID: 11937 RVA: 0x002D1DF8 File Offset: 0x002D11F8
		public static SqlBoolean operator <=(SqlSingle x, SqlSingle y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.m_value <= y.m_value);
			}
			return SqlBoolean.Null;
		}

		// Token: 0x06002EA2 RID: 11938 RVA: 0x002D1E38 File Offset: 0x002D1238
		public static SqlBoolean operator >=(SqlSingle x, SqlSingle y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.m_value >= y.m_value);
			}
			return SqlBoolean.Null;
		}

		// Token: 0x06002EA3 RID: 11939 RVA: 0x002D1E78 File Offset: 0x002D1278
		public static SqlSingle Add(SqlSingle x, SqlSingle y)
		{
			return x + y;
		}

		// Token: 0x06002EA4 RID: 11940 RVA: 0x002D1E98 File Offset: 0x002D1298
		public static SqlSingle Subtract(SqlSingle x, SqlSingle y)
		{
			return x - y;
		}

		// Token: 0x06002EA5 RID: 11941 RVA: 0x002D1EB8 File Offset: 0x002D12B8
		public static SqlSingle Multiply(SqlSingle x, SqlSingle y)
		{
			return x * y;
		}

		// Token: 0x06002EA6 RID: 11942 RVA: 0x002D1ED8 File Offset: 0x002D12D8
		public static SqlSingle Divide(SqlSingle x, SqlSingle y)
		{
			return x / y;
		}

		// Token: 0x06002EA7 RID: 11943 RVA: 0x002D1EF8 File Offset: 0x002D12F8
		public static SqlBoolean Equals(SqlSingle x, SqlSingle y)
		{
			return x == y;
		}

		// Token: 0x06002EA8 RID: 11944 RVA: 0x002D1F18 File Offset: 0x002D1318
		public static SqlBoolean NotEquals(SqlSingle x, SqlSingle y)
		{
			return x != y;
		}

		// Token: 0x06002EA9 RID: 11945 RVA: 0x002D1F38 File Offset: 0x002D1338
		public static SqlBoolean LessThan(SqlSingle x, SqlSingle y)
		{
			return x < y;
		}

		// Token: 0x06002EAA RID: 11946 RVA: 0x002D1F58 File Offset: 0x002D1358
		public static SqlBoolean GreaterThan(SqlSingle x, SqlSingle y)
		{
			return x > y;
		}

		// Token: 0x06002EAB RID: 11947 RVA: 0x002D1F78 File Offset: 0x002D1378
		public static SqlBoolean LessThanOrEqual(SqlSingle x, SqlSingle y)
		{
			return x <= y;
		}

		// Token: 0x06002EAC RID: 11948 RVA: 0x002D1F98 File Offset: 0x002D1398
		public static SqlBoolean GreaterThanOrEqual(SqlSingle x, SqlSingle y)
		{
			return x >= y;
		}

		// Token: 0x06002EAD RID: 11949 RVA: 0x002D1FB8 File Offset: 0x002D13B8
		public SqlBoolean ToSqlBoolean()
		{
			return (SqlBoolean)this;
		}

		// Token: 0x06002EAE RID: 11950 RVA: 0x002D1FD8 File Offset: 0x002D13D8
		public SqlByte ToSqlByte()
		{
			return (SqlByte)this;
		}

		// Token: 0x06002EAF RID: 11951 RVA: 0x002D1FF8 File Offset: 0x002D13F8
		public SqlDouble ToSqlDouble()
		{
			return this;
		}

		// Token: 0x06002EB0 RID: 11952 RVA: 0x002D2018 File Offset: 0x002D1418
		public SqlInt16 ToSqlInt16()
		{
			return (SqlInt16)this;
		}

		// Token: 0x06002EB1 RID: 11953 RVA: 0x002D2038 File Offset: 0x002D1438
		public SqlInt32 ToSqlInt32()
		{
			return (SqlInt32)this;
		}

		// Token: 0x06002EB2 RID: 11954 RVA: 0x002D2058 File Offset: 0x002D1458
		public SqlInt64 ToSqlInt64()
		{
			return (SqlInt64)this;
		}

		// Token: 0x06002EB3 RID: 11955 RVA: 0x002D2078 File Offset: 0x002D1478
		public SqlMoney ToSqlMoney()
		{
			return (SqlMoney)this;
		}

		// Token: 0x06002EB4 RID: 11956 RVA: 0x002D2098 File Offset: 0x002D1498
		public SqlDecimal ToSqlDecimal()
		{
			return (SqlDecimal)this;
		}

		// Token: 0x06002EB5 RID: 11957 RVA: 0x002D20B8 File Offset: 0x002D14B8
		public SqlString ToSqlString()
		{
			return (SqlString)this;
		}

		// Token: 0x06002EB6 RID: 11958 RVA: 0x002D20D8 File Offset: 0x002D14D8
		public int CompareTo(object value)
		{
			if (value is SqlSingle)
			{
				SqlSingle value2 = (SqlSingle)value;
				return this.CompareTo(value2);
			}
			throw ADP.WrongType(value.GetType(), typeof(SqlSingle));
		}

		// Token: 0x06002EB7 RID: 11959 RVA: 0x002D2118 File Offset: 0x002D1518
		public int CompareTo(SqlSingle value)
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

		// Token: 0x06002EB8 RID: 11960 RVA: 0x002D2178 File Offset: 0x002D1578
		public override bool Equals(object value)
		{
			if (!(value is SqlSingle))
			{
				return false;
			}
			SqlSingle y = (SqlSingle)value;
			if (y.IsNull || this.IsNull)
			{
				return y.IsNull && this.IsNull;
			}
			return (this == y).Value;
		}

		// Token: 0x06002EB9 RID: 11961 RVA: 0x002D21D8 File Offset: 0x002D15D8
		public override int GetHashCode()
		{
			if (!this.IsNull)
			{
				return this.Value.GetHashCode();
			}
			return 0;
		}

		// Token: 0x06002EBA RID: 11962 RVA: 0x002D2208 File Offset: 0x002D1608
		XmlSchema IXmlSerializable.GetSchema()
		{
			return null;
		}

		// Token: 0x06002EBB RID: 11963 RVA: 0x002D2218 File Offset: 0x002D1618
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			string attribute = reader.GetAttribute("nil", "http://www.w3.org/2001/XMLSchema-instance");
			if (attribute != null && XmlConvert.ToBoolean(attribute))
			{
				this.m_fNotNull = false;
				return;
			}
			this.m_value = XmlConvert.ToSingle(reader.ReadElementString());
			this.m_fNotNull = true;
		}

		// Token: 0x06002EBC RID: 11964 RVA: 0x002D2268 File Offset: 0x002D1668
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			if (this.IsNull)
			{
				writer.WriteAttributeString("xsi", "nil", "http://www.w3.org/2001/XMLSchema-instance", "true");
				return;
			}
			writer.WriteString(XmlConvert.ToString(this.m_value));
		}

		// Token: 0x06002EBD RID: 11965 RVA: 0x002D22B8 File Offset: 0x002D16B8
		public static XmlQualifiedName GetXsdType(XmlSchemaSet schemaSet)
		{
			return new XmlQualifiedName("float", "http://www.w3.org/2001/XMLSchema");
		}

		// Token: 0x04001D35 RID: 7477
		private bool m_fNotNull;

		// Token: 0x04001D36 RID: 7478
		private float m_value;

		// Token: 0x04001D37 RID: 7479
		public static readonly SqlSingle Null = new SqlSingle(true);

		// Token: 0x04001D38 RID: 7480
		public static readonly SqlSingle Zero = new SqlSingle(0f);

		// Token: 0x04001D39 RID: 7481
		public static readonly SqlSingle MinValue = new SqlSingle(float.MinValue);

		// Token: 0x04001D3A RID: 7482
		public static readonly SqlSingle MaxValue = new SqlSingle(float.MaxValue);
	}
}
