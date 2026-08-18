using System;
using System.Data.Common;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System.Data.SqlTypes
{
	// Token: 0x02000152 RID: 338
	[XmlSchemaProvider("GetXsdType")]
	[Serializable]
	public struct SqlByte : INullable, IComparable, IXmlSerializable
	{
		// Token: 0x060013DE RID: 5086 RVA: 0x0009B828 File Offset: 0x0009AC28
		private SqlByte(bool fNull)
		{
			this.m_fNotNull = false;
			this.m_value = 0;
		}

		// Token: 0x060013DF RID: 5087 RVA: 0x0009B844 File Offset: 0x0009AC44
		public SqlByte(byte value)
		{
			this.m_value = value;
			this.m_fNotNull = true;
		}

		// Token: 0x170002EB RID: 747
		// (get) Token: 0x060013E0 RID: 5088 RVA: 0x0009B860 File Offset: 0x0009AC60
		public bool IsNull
		{
			get
			{
				return !this.m_fNotNull;
			}
		}

		// Token: 0x170002EC RID: 748
		// (get) Token: 0x060013E1 RID: 5089 RVA: 0x0009B878 File Offset: 0x0009AC78
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

		// Token: 0x060013E2 RID: 5090 RVA: 0x0009B89C File Offset: 0x0009AC9C
		public static implicit operator SqlByte(byte x)
		{
			return new SqlByte(x);
		}

		// Token: 0x060013E3 RID: 5091 RVA: 0x0009B8B0 File Offset: 0x0009ACB0
		public static explicit operator byte(SqlByte x)
		{
			return x.Value;
		}

		// Token: 0x060013E4 RID: 5092 RVA: 0x0009B8C4 File Offset: 0x0009ACC4
		public override string ToString()
		{
			if (!this.IsNull)
			{
				return this.m_value.ToString(null);
			}
			return SQLResource.NullString;
		}

		// Token: 0x060013E5 RID: 5093 RVA: 0x0009B8EC File Offset: 0x0009ACEC
		public static SqlByte Parse(string s)
		{
			if (s == SQLResource.NullString)
			{
				return SqlByte.Null;
			}
			return new SqlByte(byte.Parse(s, null));
		}

		// Token: 0x060013E6 RID: 5094 RVA: 0x0009B918 File Offset: 0x0009AD18
		public static SqlByte operator ~(SqlByte x)
		{
			if (!x.IsNull)
			{
				return new SqlByte(~x.m_value);
			}
			return SqlByte.Null;
		}

		// Token: 0x060013E7 RID: 5095 RVA: 0x0009B944 File Offset: 0x0009AD44
		public static SqlByte operator +(SqlByte x, SqlByte y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlByte.Null;
			}
			int num = (int)(x.m_value + y.m_value);
			if ((num & -256) != 0)
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			return new SqlByte((byte)num);
		}

		// Token: 0x060013E8 RID: 5096 RVA: 0x0009B994 File Offset: 0x0009AD94
		public static SqlByte operator -(SqlByte x, SqlByte y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlByte.Null;
			}
			int num = (int)(x.m_value - y.m_value);
			if ((num & -256) != 0)
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			return new SqlByte((byte)num);
		}

		// Token: 0x060013E9 RID: 5097 RVA: 0x0009B9E4 File Offset: 0x0009ADE4
		public static SqlByte operator *(SqlByte x, SqlByte y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlByte.Null;
			}
			int num = (int)(x.m_value * y.m_value);
			if ((num & -256) != 0)
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			return new SqlByte((byte)num);
		}

		// Token: 0x060013EA RID: 5098 RVA: 0x0009BA34 File Offset: 0x0009AE34
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

		// Token: 0x060013EB RID: 5099 RVA: 0x0009BA80 File Offset: 0x0009AE80
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

		// Token: 0x060013EC RID: 5100 RVA: 0x0009BACC File Offset: 0x0009AECC
		public static SqlByte operator &(SqlByte x, SqlByte y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlByte(x.m_value & y.m_value);
			}
			return SqlByte.Null;
		}

		// Token: 0x060013ED RID: 5101 RVA: 0x0009BB04 File Offset: 0x0009AF04
		public static SqlByte operator |(SqlByte x, SqlByte y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlByte(x.m_value | y.m_value);
			}
			return SqlByte.Null;
		}

		// Token: 0x060013EE RID: 5102 RVA: 0x0009BB3C File Offset: 0x0009AF3C
		public static SqlByte operator ^(SqlByte x, SqlByte y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlByte(x.m_value ^ y.m_value);
			}
			return SqlByte.Null;
		}

		// Token: 0x060013EF RID: 5103 RVA: 0x0009BB74 File Offset: 0x0009AF74
		public static explicit operator SqlByte(SqlBoolean x)
		{
			if (!x.IsNull)
			{
				return new SqlByte(x.ByteValue);
			}
			return SqlByte.Null;
		}

		// Token: 0x060013F0 RID: 5104 RVA: 0x0009BB9C File Offset: 0x0009AF9C
		public static explicit operator SqlByte(SqlMoney x)
		{
			if (!x.IsNull)
			{
				return new SqlByte(checked((byte)x.ToInt32()));
			}
			return SqlByte.Null;
		}

		// Token: 0x060013F1 RID: 5105 RVA: 0x0009BBC8 File Offset: 0x0009AFC8
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

		// Token: 0x060013F2 RID: 5106 RVA: 0x0009BC24 File Offset: 0x0009B024
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

		// Token: 0x060013F3 RID: 5107 RVA: 0x0009BC80 File Offset: 0x0009B080
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

		// Token: 0x060013F4 RID: 5108 RVA: 0x0009BCE0 File Offset: 0x0009B0E0
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

		// Token: 0x060013F5 RID: 5109 RVA: 0x0009BD40 File Offset: 0x0009B140
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

		// Token: 0x060013F6 RID: 5110 RVA: 0x0009BDA8 File Offset: 0x0009B1A8
		public static explicit operator SqlByte(SqlDecimal x)
		{
			return (SqlByte)((SqlInt32)x);
		}

		// Token: 0x060013F7 RID: 5111 RVA: 0x0009BDC0 File Offset: 0x0009B1C0
		public static explicit operator SqlByte(SqlString x)
		{
			if (!x.IsNull)
			{
				return new SqlByte(byte.Parse(x.Value, null));
			}
			return SqlByte.Null;
		}

		// Token: 0x060013F8 RID: 5112 RVA: 0x0009BDF0 File Offset: 0x0009B1F0
		public static SqlBoolean operator ==(SqlByte x, SqlByte y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.m_value == y.m_value);
			}
			return SqlBoolean.Null;
		}

		// Token: 0x060013F9 RID: 5113 RVA: 0x0009BE28 File Offset: 0x0009B228
		public static SqlBoolean operator !=(SqlByte x, SqlByte y)
		{
			return !(x == y);
		}

		// Token: 0x060013FA RID: 5114 RVA: 0x0009BE44 File Offset: 0x0009B244
		public static SqlBoolean operator <(SqlByte x, SqlByte y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.m_value < y.m_value);
			}
			return SqlBoolean.Null;
		}

		// Token: 0x060013FB RID: 5115 RVA: 0x0009BE7C File Offset: 0x0009B27C
		public static SqlBoolean operator >(SqlByte x, SqlByte y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.m_value > y.m_value);
			}
			return SqlBoolean.Null;
		}

		// Token: 0x060013FC RID: 5116 RVA: 0x0009BEB4 File Offset: 0x0009B2B4
		public static SqlBoolean operator <=(SqlByte x, SqlByte y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.m_value <= y.m_value);
			}
			return SqlBoolean.Null;
		}

		// Token: 0x060013FD RID: 5117 RVA: 0x0009BEF0 File Offset: 0x0009B2F0
		public static SqlBoolean operator >=(SqlByte x, SqlByte y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.m_value >= y.m_value);
			}
			return SqlBoolean.Null;
		}

		// Token: 0x060013FE RID: 5118 RVA: 0x0009BF2C File Offset: 0x0009B32C
		public static SqlByte OnesComplement(SqlByte x)
		{
			return ~x;
		}

		// Token: 0x060013FF RID: 5119 RVA: 0x0009BF40 File Offset: 0x0009B340
		public static SqlByte Add(SqlByte x, SqlByte y)
		{
			return x + y;
		}

		// Token: 0x06001400 RID: 5120 RVA: 0x0009BF54 File Offset: 0x0009B354
		public static SqlByte Subtract(SqlByte x, SqlByte y)
		{
			return x - y;
		}

		// Token: 0x06001401 RID: 5121 RVA: 0x0009BF68 File Offset: 0x0009B368
		public static SqlByte Multiply(SqlByte x, SqlByte y)
		{
			return x * y;
		}

		// Token: 0x06001402 RID: 5122 RVA: 0x0009BF7C File Offset: 0x0009B37C
		public static SqlByte Divide(SqlByte x, SqlByte y)
		{
			return x / y;
		}

		// Token: 0x06001403 RID: 5123 RVA: 0x0009BF90 File Offset: 0x0009B390
		public static SqlByte Mod(SqlByte x, SqlByte y)
		{
			return x % y;
		}

		// Token: 0x06001404 RID: 5124 RVA: 0x0009BFA4 File Offset: 0x0009B3A4
		public static SqlByte Modulus(SqlByte x, SqlByte y)
		{
			return x % y;
		}

		// Token: 0x06001405 RID: 5125 RVA: 0x0009BFB8 File Offset: 0x0009B3B8
		public static SqlByte BitwiseAnd(SqlByte x, SqlByte y)
		{
			return x & y;
		}

		// Token: 0x06001406 RID: 5126 RVA: 0x0009BFCC File Offset: 0x0009B3CC
		public static SqlByte BitwiseOr(SqlByte x, SqlByte y)
		{
			return x | y;
		}

		// Token: 0x06001407 RID: 5127 RVA: 0x0009BFE0 File Offset: 0x0009B3E0
		public static SqlByte Xor(SqlByte x, SqlByte y)
		{
			return x ^ y;
		}

		// Token: 0x06001408 RID: 5128 RVA: 0x0009BFF4 File Offset: 0x0009B3F4
		public static SqlBoolean Equals(SqlByte x, SqlByte y)
		{
			return x == y;
		}

		// Token: 0x06001409 RID: 5129 RVA: 0x0009C008 File Offset: 0x0009B408
		public static SqlBoolean NotEquals(SqlByte x, SqlByte y)
		{
			return x != y;
		}

		// Token: 0x0600140A RID: 5130 RVA: 0x0009C01C File Offset: 0x0009B41C
		public static SqlBoolean LessThan(SqlByte x, SqlByte y)
		{
			return x < y;
		}

		// Token: 0x0600140B RID: 5131 RVA: 0x0009C030 File Offset: 0x0009B430
		public static SqlBoolean GreaterThan(SqlByte x, SqlByte y)
		{
			return x > y;
		}

		// Token: 0x0600140C RID: 5132 RVA: 0x0009C044 File Offset: 0x0009B444
		public static SqlBoolean LessThanOrEqual(SqlByte x, SqlByte y)
		{
			return x <= y;
		}

		// Token: 0x0600140D RID: 5133 RVA: 0x0009C058 File Offset: 0x0009B458
		public static SqlBoolean GreaterThanOrEqual(SqlByte x, SqlByte y)
		{
			return x >= y;
		}

		// Token: 0x0600140E RID: 5134 RVA: 0x0009C06C File Offset: 0x0009B46C
		public SqlBoolean ToSqlBoolean()
		{
			return (SqlBoolean)this;
		}

		// Token: 0x0600140F RID: 5135 RVA: 0x0009C084 File Offset: 0x0009B484
		public SqlDouble ToSqlDouble()
		{
			return this;
		}

		// Token: 0x06001410 RID: 5136 RVA: 0x0009C09C File Offset: 0x0009B49C
		public SqlInt16 ToSqlInt16()
		{
			return this;
		}

		// Token: 0x06001411 RID: 5137 RVA: 0x0009C0B4 File Offset: 0x0009B4B4
		public SqlInt32 ToSqlInt32()
		{
			return this;
		}

		// Token: 0x06001412 RID: 5138 RVA: 0x0009C0CC File Offset: 0x0009B4CC
		public SqlInt64 ToSqlInt64()
		{
			return this;
		}

		// Token: 0x06001413 RID: 5139 RVA: 0x0009C0E4 File Offset: 0x0009B4E4
		public SqlMoney ToSqlMoney()
		{
			return this;
		}

		// Token: 0x06001414 RID: 5140 RVA: 0x0009C0FC File Offset: 0x0009B4FC
		public SqlDecimal ToSqlDecimal()
		{
			return this;
		}

		// Token: 0x06001415 RID: 5141 RVA: 0x0009C114 File Offset: 0x0009B514
		public SqlSingle ToSqlSingle()
		{
			return this;
		}

		// Token: 0x06001416 RID: 5142 RVA: 0x0009C12C File Offset: 0x0009B52C
		public SqlString ToSqlString()
		{
			return (SqlString)this;
		}

		// Token: 0x06001417 RID: 5143 RVA: 0x0009C144 File Offset: 0x0009B544
		public int CompareTo(object value)
		{
			if (value is SqlByte)
			{
				SqlByte value2 = (SqlByte)value;
				return this.CompareTo(value2);
			}
			throw ADP.WrongType(value.GetType(), typeof(SqlByte));
		}

		// Token: 0x06001418 RID: 5144 RVA: 0x0009C180 File Offset: 0x0009B580
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

		// Token: 0x06001419 RID: 5145 RVA: 0x0009C1D8 File Offset: 0x0009B5D8
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

		// Token: 0x0600141A RID: 5146 RVA: 0x0009C230 File Offset: 0x0009B630
		public override int GetHashCode()
		{
			if (!this.IsNull)
			{
				return this.Value.GetHashCode();
			}
			return 0;
		}

		// Token: 0x0600141B RID: 5147 RVA: 0x0009C258 File Offset: 0x0009B658
		XmlSchema IXmlSerializable.GetSchema()
		{
			return null;
		}

		// Token: 0x0600141C RID: 5148 RVA: 0x0009C268 File Offset: 0x0009B668
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			string attribute = reader.GetAttribute("nil", "http://www.w3.org/2001/XMLSchema-instance");
			if (attribute != null && XmlConvert.ToBoolean(attribute))
			{
				reader.ReadElementString();
				this.m_fNotNull = false;
				return;
			}
			this.m_value = XmlConvert.ToByte(reader.ReadElementString());
			this.m_fNotNull = true;
		}

		// Token: 0x0600141D RID: 5149 RVA: 0x0009C2B8 File Offset: 0x0009B6B8
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			if (this.IsNull)
			{
				writer.WriteAttributeString("xsi", "nil", "http://www.w3.org/2001/XMLSchema-instance", "true");
				return;
			}
			writer.WriteString(XmlConvert.ToString(this.m_value));
		}

		// Token: 0x0600141E RID: 5150 RVA: 0x0009C2FC File Offset: 0x0009B6FC
		public static XmlQualifiedName GetXsdType(XmlSchemaSet schemaSet)
		{
			return new XmlQualifiedName("unsignedByte", "http://www.w3.org/2001/XMLSchema");
		}

		// Token: 0x04000D55 RID: 3413
		private bool m_fNotNull;

		// Token: 0x04000D56 RID: 3414
		private byte m_value;

		// Token: 0x04000D57 RID: 3415
		private const int x_iBitNotByteMax = -256;

		// Token: 0x04000D58 RID: 3416
		public static readonly SqlByte Null = new SqlByte(true);

		// Token: 0x04000D59 RID: 3417
		public static readonly SqlByte Zero = new SqlByte(0);

		// Token: 0x04000D5A RID: 3418
		public static readonly SqlByte MinValue = new SqlByte(0);

		// Token: 0x04000D5B RID: 3419
		public static readonly SqlByte MaxValue = new SqlByte(byte.MaxValue);
	}
}
