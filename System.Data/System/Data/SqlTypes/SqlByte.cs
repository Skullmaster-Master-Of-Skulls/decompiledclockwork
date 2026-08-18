using System;
using System.Data.Common;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System.Data.SqlTypes
{
	// Token: 0x02000342 RID: 834
	[XmlSchemaProvider("GetXsdType")]
	[Serializable]
	public struct SqlByte : INullable, IComparable, IXmlSerializable
	{
		// Token: 0x06002B9C RID: 11164 RVA: 0x002C4E08 File Offset: 0x002C4208
		private SqlByte(bool fNull)
		{
			this.m_fNotNull = false;
			this.m_value = 0;
		}

		// Token: 0x06002B9D RID: 11165 RVA: 0x002C4E28 File Offset: 0x002C4228
		public SqlByte(byte value)
		{
			this.m_value = value;
			this.m_fNotNull = true;
		}

		// Token: 0x17000719 RID: 1817
		// (get) Token: 0x06002B9E RID: 11166 RVA: 0x002C4E48 File Offset: 0x002C4248
		public bool IsNull
		{
			get
			{
				return !this.m_fNotNull;
			}
		}

		// Token: 0x1700071A RID: 1818
		// (get) Token: 0x06002B9F RID: 11167 RVA: 0x002C4E68 File Offset: 0x002C4268
		public byte Value
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

		// Token: 0x06002BA0 RID: 11168 RVA: 0x002C4E98 File Offset: 0x002C4298
		public static implicit operator SqlByte(byte x)
		{
			return new SqlByte(x);
		}

		// Token: 0x06002BA1 RID: 11169 RVA: 0x002C4EB8 File Offset: 0x002C42B8
		public static explicit operator byte(SqlByte x)
		{
			return x.Value;
		}

		// Token: 0x06002BA2 RID: 11170 RVA: 0x002C4ED8 File Offset: 0x002C42D8
		public override string ToString()
		{
			if (!this.IsNull)
			{
				return this.m_value.ToString(null);
			}
			return SQLResource.NullString;
		}

		// Token: 0x06002BA3 RID: 11171 RVA: 0x002C4F08 File Offset: 0x002C4308
		public static SqlByte Parse(string s)
		{
			if (s == SQLResource.NullString)
			{
				return SqlByte.Null;
			}
			return new SqlByte(byte.Parse(s, null));
		}

		// Token: 0x06002BA4 RID: 11172 RVA: 0x002C4F38 File Offset: 0x002C4338
		public static SqlByte operator ~(SqlByte x)
		{
			if (!x.IsNull)
			{
				return new SqlByte(~x.m_value);
			}
			return SqlByte.Null;
		}

		// Token: 0x06002BA5 RID: 11173 RVA: 0x002C4F68 File Offset: 0x002C4368
		public static SqlByte operator +(SqlByte x, SqlByte y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlByte.Null;
			}
			int num = (int)(x.m_value + y.m_value);
			if ((num & SqlByte.x_iBitNotByteMax) != 0)
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			return new SqlByte((byte)num);
		}

		// Token: 0x06002BA6 RID: 11174 RVA: 0x002C4FB8 File Offset: 0x002C43B8
		public static SqlByte operator -(SqlByte x, SqlByte y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlByte.Null;
			}
			int num = (int)(x.m_value - y.m_value);
			if ((num & SqlByte.x_iBitNotByteMax) != 0)
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			return new SqlByte((byte)num);
		}

		// Token: 0x06002BA7 RID: 11175 RVA: 0x002C5008 File Offset: 0x002C4408
		public static SqlByte operator *(SqlByte x, SqlByte y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlByte.Null;
			}
			int num = (int)(x.m_value * y.m_value);
			if ((num & SqlByte.x_iBitNotByteMax) != 0)
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			return new SqlByte((byte)num);
		}

		// Token: 0x06002BA8 RID: 11176 RVA: 0x002C5058 File Offset: 0x002C4458
		public static SqlByte operator /(SqlByte x, SqlByte y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlByte.Null;
			}
			if (y.m_value != 0)
			{
				return new SqlByte(x.m_value / y.m_value);
			}
			throw new DivideByZeroException(SQLResource.DivideByZeroMessage);
		}

		// Token: 0x06002BA9 RID: 11177 RVA: 0x002C50A8 File Offset: 0x002C44A8
		public static SqlByte operator %(SqlByte x, SqlByte y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlByte.Null;
			}
			if (y.m_value != 0)
			{
				return new SqlByte(x.m_value % y.m_value);
			}
			throw new DivideByZeroException(SQLResource.DivideByZeroMessage);
		}

		// Token: 0x06002BAA RID: 11178 RVA: 0x002C50F8 File Offset: 0x002C44F8
		public static SqlByte operator &(SqlByte x, SqlByte y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlByte(x.m_value & y.m_value);
			}
			return SqlByte.Null;
		}

		// Token: 0x06002BAB RID: 11179 RVA: 0x002C5138 File Offset: 0x002C4538
		public static SqlByte operator |(SqlByte x, SqlByte y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlByte(x.m_value | y.m_value);
			}
			return SqlByte.Null;
		}

		// Token: 0x06002BAC RID: 11180 RVA: 0x002C5178 File Offset: 0x002C4578
		public static SqlByte operator ^(SqlByte x, SqlByte y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlByte(x.m_value ^ y.m_value);
			}
			return SqlByte.Null;
		}

		// Token: 0x06002BAD RID: 11181 RVA: 0x002C51B8 File Offset: 0x002C45B8
		public static explicit operator SqlByte(SqlBoolean x)
		{
			if (!x.IsNull)
			{
				return new SqlByte(x.ByteValue);
			}
			return SqlByte.Null;
		}

		// Token: 0x06002BAE RID: 11182 RVA: 0x002C51E8 File Offset: 0x002C45E8
		public static explicit operator SqlByte(SqlMoney x)
		{
			if (!x.IsNull)
			{
				return new SqlByte(checked((byte)x.ToInt32()));
			}
			return SqlByte.Null;
		}

		// Token: 0x06002BAF RID: 11183 RVA: 0x002C5218 File Offset: 0x002C4618
		public static explicit operator SqlByte(SqlInt16 x)
		{
			if (x.IsNull)
			{
				return SqlByte.Null;
			}
			if (x.Value > 255 || x.Value < 0)
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			if (!x.IsNull)
			{
				return new SqlByte((byte)x.Value);
			}
			return SqlByte.Null;
		}

		// Token: 0x06002BB0 RID: 11184 RVA: 0x002C5278 File Offset: 0x002C4678
		public static explicit operator SqlByte(SqlInt32 x)
		{
			if (x.IsNull)
			{
				return SqlByte.Null;
			}
			if (x.Value > 255 || x.Value < 0)
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			if (!x.IsNull)
			{
				return new SqlByte((byte)x.Value);
			}
			return SqlByte.Null;
		}

		// Token: 0x06002BB1 RID: 11185 RVA: 0x002C52D8 File Offset: 0x002C46D8
		public static explicit operator SqlByte(SqlInt64 x)
		{
			if (x.IsNull)
			{
				return SqlByte.Null;
			}
			if (x.Value > 255L || x.Value < 0L)
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			if (!x.IsNull)
			{
				return new SqlByte((byte)x.Value);
			}
			return SqlByte.Null;
		}

		// Token: 0x06002BB2 RID: 11186 RVA: 0x002C5338 File Offset: 0x002C4738
		public static explicit operator SqlByte(SqlSingle x)
		{
			if (x.IsNull)
			{
				return SqlByte.Null;
			}
			if (x.Value > 255f || x.Value < 0f)
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			if (!x.IsNull)
			{
				return new SqlByte((byte)x.Value);
			}
			return SqlByte.Null;
		}

		// Token: 0x06002BB3 RID: 11187 RVA: 0x002C5398 File Offset: 0x002C4798
		public static explicit operator SqlByte(SqlDouble x)
		{
			if (x.IsNull)
			{
				return SqlByte.Null;
			}
			if (x.Value > 255.0 || x.Value < 0.0)
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			if (!x.IsNull)
			{
				return new SqlByte((byte)x.Value);
			}
			return SqlByte.Null;
		}

		// Token: 0x06002BB4 RID: 11188 RVA: 0x002C5408 File Offset: 0x002C4808
		public static explicit operator SqlByte(SqlDecimal x)
		{
			return (SqlByte)((SqlInt32)x);
		}

		// Token: 0x06002BB5 RID: 11189 RVA: 0x002C5428 File Offset: 0x002C4828
		public static explicit operator SqlByte(SqlString x)
		{
			if (!x.IsNull)
			{
				return new SqlByte(byte.Parse(x.Value, null));
			}
			return SqlByte.Null;
		}

		// Token: 0x06002BB6 RID: 11190 RVA: 0x002C5458 File Offset: 0x002C4858
		public static SqlBoolean operator ==(SqlByte x, SqlByte y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.m_value == y.m_value);
			}
			return SqlBoolean.Null;
		}

		// Token: 0x06002BB7 RID: 11191 RVA: 0x002C5498 File Offset: 0x002C4898
		public static SqlBoolean operator !=(SqlByte x, SqlByte y)
		{
			return !(x == y);
		}

		// Token: 0x06002BB8 RID: 11192 RVA: 0x002C54B8 File Offset: 0x002C48B8
		public static SqlBoolean operator <(SqlByte x, SqlByte y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.m_value < y.m_value);
			}
			return SqlBoolean.Null;
		}

		// Token: 0x06002BB9 RID: 11193 RVA: 0x002C54F8 File Offset: 0x002C48F8
		public static SqlBoolean operator >(SqlByte x, SqlByte y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.m_value > y.m_value);
			}
			return SqlBoolean.Null;
		}

		// Token: 0x06002BBA RID: 11194 RVA: 0x002C5538 File Offset: 0x002C4938
		public static SqlBoolean operator <=(SqlByte x, SqlByte y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.m_value <= y.m_value);
			}
			return SqlBoolean.Null;
		}

		// Token: 0x06002BBB RID: 11195 RVA: 0x002C5578 File Offset: 0x002C4978
		public static SqlBoolean operator >=(SqlByte x, SqlByte y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.m_value >= y.m_value);
			}
			return SqlBoolean.Null;
		}

		// Token: 0x06002BBC RID: 11196 RVA: 0x002C55B8 File Offset: 0x002C49B8
		public static SqlByte OnesComplement(SqlByte x)
		{
			return ~x;
		}

		// Token: 0x06002BBD RID: 11197 RVA: 0x002C55D8 File Offset: 0x002C49D8
		public static SqlByte Add(SqlByte x, SqlByte y)
		{
			return x + y;
		}

		// Token: 0x06002BBE RID: 11198 RVA: 0x002C55F8 File Offset: 0x002C49F8
		public static SqlByte Subtract(SqlByte x, SqlByte y)
		{
			return x - y;
		}

		// Token: 0x06002BBF RID: 11199 RVA: 0x002C5618 File Offset: 0x002C4A18
		public static SqlByte Multiply(SqlByte x, SqlByte y)
		{
			return x * y;
		}

		// Token: 0x06002BC0 RID: 11200 RVA: 0x002C5638 File Offset: 0x002C4A38
		public static SqlByte Divide(SqlByte x, SqlByte y)
		{
			return x / y;
		}

		// Token: 0x06002BC1 RID: 11201 RVA: 0x002C5658 File Offset: 0x002C4A58
		public static SqlByte Mod(SqlByte x, SqlByte y)
		{
			return x % y;
		}

		// Token: 0x06002BC2 RID: 11202 RVA: 0x002C5678 File Offset: 0x002C4A78
		public static SqlByte Modulus(SqlByte x, SqlByte y)
		{
			return x % y;
		}

		// Token: 0x06002BC3 RID: 11203 RVA: 0x002C5698 File Offset: 0x002C4A98
		public static SqlByte BitwiseAnd(SqlByte x, SqlByte y)
		{
			return x & y;
		}

		// Token: 0x06002BC4 RID: 11204 RVA: 0x002C56B8 File Offset: 0x002C4AB8
		public static SqlByte BitwiseOr(SqlByte x, SqlByte y)
		{
			return x | y;
		}

		// Token: 0x06002BC5 RID: 11205 RVA: 0x002C56D8 File Offset: 0x002C4AD8
		public static SqlByte Xor(SqlByte x, SqlByte y)
		{
			return x ^ y;
		}

		// Token: 0x06002BC6 RID: 11206 RVA: 0x002C56F8 File Offset: 0x002C4AF8
		public static SqlBoolean Equals(SqlByte x, SqlByte y)
		{
			return x == y;
		}

		// Token: 0x06002BC7 RID: 11207 RVA: 0x002C5718 File Offset: 0x002C4B18
		public static SqlBoolean NotEquals(SqlByte x, SqlByte y)
		{
			return x != y;
		}

		// Token: 0x06002BC8 RID: 11208 RVA: 0x002C5738 File Offset: 0x002C4B38
		public static SqlBoolean LessThan(SqlByte x, SqlByte y)
		{
			return x < y;
		}

		// Token: 0x06002BC9 RID: 11209 RVA: 0x002C5758 File Offset: 0x002C4B58
		public static SqlBoolean GreaterThan(SqlByte x, SqlByte y)
		{
			return x > y;
		}

		// Token: 0x06002BCA RID: 11210 RVA: 0x002C5778 File Offset: 0x002C4B78
		public static SqlBoolean LessThanOrEqual(SqlByte x, SqlByte y)
		{
			return x <= y;
		}

		// Token: 0x06002BCB RID: 11211 RVA: 0x002C5798 File Offset: 0x002C4B98
		public static SqlBoolean GreaterThanOrEqual(SqlByte x, SqlByte y)
		{
			return x >= y;
		}

		// Token: 0x06002BCC RID: 11212 RVA: 0x002C57B8 File Offset: 0x002C4BB8
		public SqlBoolean ToSqlBoolean()
		{
			return (SqlBoolean)this;
		}

		// Token: 0x06002BCD RID: 11213 RVA: 0x002C57D8 File Offset: 0x002C4BD8
		public SqlDouble ToSqlDouble()
		{
			return this;
		}

		// Token: 0x06002BCE RID: 11214 RVA: 0x002C57F8 File Offset: 0x002C4BF8
		public SqlInt16 ToSqlInt16()
		{
			return this;
		}

		// Token: 0x06002BCF RID: 11215 RVA: 0x002C5818 File Offset: 0x002C4C18
		public SqlInt32 ToSqlInt32()
		{
			return this;
		}

		// Token: 0x06002BD0 RID: 11216 RVA: 0x002C5838 File Offset: 0x002C4C38
		public SqlInt64 ToSqlInt64()
		{
			return this;
		}

		// Token: 0x06002BD1 RID: 11217 RVA: 0x002C5858 File Offset: 0x002C4C58
		public SqlMoney ToSqlMoney()
		{
			return this;
		}

		// Token: 0x06002BD2 RID: 11218 RVA: 0x002C5878 File Offset: 0x002C4C78
		public SqlDecimal ToSqlDecimal()
		{
			return this;
		}

		// Token: 0x06002BD3 RID: 11219 RVA: 0x002C5898 File Offset: 0x002C4C98
		public SqlSingle ToSqlSingle()
		{
			return this;
		}

		// Token: 0x06002BD4 RID: 11220 RVA: 0x002C58B8 File Offset: 0x002C4CB8
		public SqlString ToSqlString()
		{
			return (SqlString)this;
		}

		// Token: 0x06002BD5 RID: 11221 RVA: 0x002C58D8 File Offset: 0x002C4CD8
		public int CompareTo(object value)
		{
			if (value is SqlByte)
			{
				SqlByte value2 = (SqlByte)value;
				return this.CompareTo(value2);
			}
			throw ADP.WrongType(value.GetType(), typeof(SqlByte));
		}

		// Token: 0x06002BD6 RID: 11222 RVA: 0x002C5918 File Offset: 0x002C4D18
		public int CompareTo(SqlByte value)
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

		// Token: 0x06002BD7 RID: 11223 RVA: 0x002C5978 File Offset: 0x002C4D78
		public override bool Equals(object value)
		{
			if (!(value is SqlByte))
			{
				return false;
			}
			SqlByte y = (SqlByte)value;
			if (y.IsNull || this.IsNull)
			{
				return y.IsNull && this.IsNull;
			}
			return (this == y).Value;
		}

		// Token: 0x06002BD8 RID: 11224 RVA: 0x002C59D8 File Offset: 0x002C4DD8
		public override int GetHashCode()
		{
			if (!this.IsNull)
			{
				return this.Value.GetHashCode();
			}
			return 0;
		}

		// Token: 0x06002BD9 RID: 11225 RVA: 0x002C5A08 File Offset: 0x002C4E08
		XmlSchema IXmlSerializable.GetSchema()
		{
			return null;
		}

		// Token: 0x06002BDA RID: 11226 RVA: 0x002C5A18 File Offset: 0x002C4E18
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			string attribute = reader.GetAttribute("nil", "http://www.w3.org/2001/XMLSchema-instance");
			if (attribute != null && XmlConvert.ToBoolean(attribute))
			{
				this.m_fNotNull = false;
				return;
			}
			this.m_value = XmlConvert.ToByte(reader.ReadElementString());
			this.m_fNotNull = true;
		}

		// Token: 0x06002BDB RID: 11227 RVA: 0x002C5A68 File Offset: 0x002C4E68
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			if (this.IsNull)
			{
				writer.WriteAttributeString("xsi", "nil", "http://www.w3.org/2001/XMLSchema-instance", "true");
				return;
			}
			writer.WriteString(XmlConvert.ToString(this.m_value));
		}

		// Token: 0x06002BDC RID: 11228 RVA: 0x002C5AB8 File Offset: 0x002C4EB8
		public static XmlQualifiedName GetXsdType(XmlSchemaSet schemaSet)
		{
			return new XmlQualifiedName("unsignedByte", "http://www.w3.org/2001/XMLSchema");
		}

		// Token: 0x04001C6A RID: 7274
		private bool m_fNotNull;

		// Token: 0x04001C6B RID: 7275
		private byte m_value;

		// Token: 0x04001C6C RID: 7276
		private static readonly int x_iBitNotByteMax = -256;

		// Token: 0x04001C6D RID: 7277
		public static readonly SqlByte Null = new SqlByte(true);

		// Token: 0x04001C6E RID: 7278
		public static readonly SqlByte Zero = new SqlByte(0);

		// Token: 0x04001C6F RID: 7279
		public static readonly SqlByte MinValue = new SqlByte(0);

		// Token: 0x04001C70 RID: 7280
		public static readonly SqlByte MaxValue = new SqlByte(byte.MaxValue);
	}
}
