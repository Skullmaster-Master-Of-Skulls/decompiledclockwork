using System;
using System.Data.Common;
using System.Globalization;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System.Data.SqlTypes
{
	// Token: 0x0200034A RID: 842
	[XmlSchemaProvider("GetXsdType")]
	[Serializable]
	public struct SqlDouble : INullable, IComparable, IXmlSerializable
	{
		// Token: 0x06002CE5 RID: 11493 RVA: 0x002CBB88 File Offset: 0x002CAF88
		private SqlDouble(bool fNull)
		{
			this.m_fNotNull = false;
			this.m_value = 0.0;
		}

		// Token: 0x06002CE6 RID: 11494 RVA: 0x002CBBB8 File Offset: 0x002CAFB8
		public SqlDouble(double value)
		{
			if (double.IsInfinity(value) || double.IsNaN(value))
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			this.m_value = value;
			this.m_fNotNull = true;
		}

		// Token: 0x17000743 RID: 1859
		// (get) Token: 0x06002CE7 RID: 11495 RVA: 0x002CBBF8 File Offset: 0x002CAFF8
		public bool IsNull
		{
			get
			{
				return !this.m_fNotNull;
			}
		}

		// Token: 0x17000744 RID: 1860
		// (get) Token: 0x06002CE8 RID: 11496 RVA: 0x002CBC18 File Offset: 0x002CB018
		public double Value
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

		// Token: 0x06002CE9 RID: 11497 RVA: 0x002CBC48 File Offset: 0x002CB048
		public static implicit operator SqlDouble(double x)
		{
			return new SqlDouble(x);
		}

		// Token: 0x06002CEA RID: 11498 RVA: 0x002CBC68 File Offset: 0x002CB068
		public static explicit operator double(SqlDouble x)
		{
			return x.Value;
		}

		// Token: 0x06002CEB RID: 11499 RVA: 0x002CBC88 File Offset: 0x002CB088
		public override string ToString()
		{
			if (!this.IsNull)
			{
				return this.m_value.ToString(null);
			}
			return SQLResource.NullString;
		}

		// Token: 0x06002CEC RID: 11500 RVA: 0x002CBCB8 File Offset: 0x002CB0B8
		public static SqlDouble Parse(string s)
		{
			if (s == SQLResource.NullString)
			{
				return SqlDouble.Null;
			}
			return new SqlDouble(double.Parse(s, CultureInfo.InvariantCulture));
		}

		// Token: 0x06002CED RID: 11501 RVA: 0x002CBCE8 File Offset: 0x002CB0E8
		public static SqlDouble operator -(SqlDouble x)
		{
			if (!x.IsNull)
			{
				return new SqlDouble(-x.m_value);
			}
			return SqlDouble.Null;
		}

		// Token: 0x06002CEE RID: 11502 RVA: 0x002CBD18 File Offset: 0x002CB118
		public static SqlDouble operator +(SqlDouble x, SqlDouble y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlDouble.Null;
			}
			double num = x.m_value + y.m_value;
			if (double.IsInfinity(num))
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			return new SqlDouble(num);
		}

		// Token: 0x06002CEF RID: 11503 RVA: 0x002CBD68 File Offset: 0x002CB168
		public static SqlDouble operator -(SqlDouble x, SqlDouble y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlDouble.Null;
			}
			double num = x.m_value - y.m_value;
			if (double.IsInfinity(num))
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			return new SqlDouble(num);
		}

		// Token: 0x06002CF0 RID: 11504 RVA: 0x002CBDB8 File Offset: 0x002CB1B8
		public static SqlDouble operator *(SqlDouble x, SqlDouble y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlDouble.Null;
			}
			double num = x.m_value * y.m_value;
			if (double.IsInfinity(num))
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			return new SqlDouble(num);
		}

		// Token: 0x06002CF1 RID: 11505 RVA: 0x002CBE08 File Offset: 0x002CB208
		public static SqlDouble operator /(SqlDouble x, SqlDouble y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlDouble.Null;
			}
			if (y.m_value == 0.0)
			{
				throw new DivideByZeroException(SQLResource.DivideByZeroMessage);
			}
			double num = x.m_value / y.m_value;
			if (double.IsInfinity(num))
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			return new SqlDouble(num);
		}

		// Token: 0x06002CF2 RID: 11506 RVA: 0x002CBE78 File Offset: 0x002CB278
		public static explicit operator SqlDouble(SqlBoolean x)
		{
			if (!x.IsNull)
			{
				return new SqlDouble((double)x.ByteValue);
			}
			return SqlDouble.Null;
		}

		// Token: 0x06002CF3 RID: 11507 RVA: 0x002CBEA8 File Offset: 0x002CB2A8
		public static implicit operator SqlDouble(SqlByte x)
		{
			if (!x.IsNull)
			{
				return new SqlDouble((double)x.Value);
			}
			return SqlDouble.Null;
		}

		// Token: 0x06002CF4 RID: 11508 RVA: 0x002CBED8 File Offset: 0x002CB2D8
		public static implicit operator SqlDouble(SqlInt16 x)
		{
			if (!x.IsNull)
			{
				return new SqlDouble((double)x.Value);
			}
			return SqlDouble.Null;
		}

		// Token: 0x06002CF5 RID: 11509 RVA: 0x002CBF08 File Offset: 0x002CB308
		public static implicit operator SqlDouble(SqlInt32 x)
		{
			if (!x.IsNull)
			{
				return new SqlDouble((double)x.Value);
			}
			return SqlDouble.Null;
		}

		// Token: 0x06002CF6 RID: 11510 RVA: 0x002CBF38 File Offset: 0x002CB338
		public static implicit operator SqlDouble(SqlInt64 x)
		{
			if (!x.IsNull)
			{
				return new SqlDouble((double)x.Value);
			}
			return SqlDouble.Null;
		}

		// Token: 0x06002CF7 RID: 11511 RVA: 0x002CBF68 File Offset: 0x002CB368
		public static implicit operator SqlDouble(SqlSingle x)
		{
			if (!x.IsNull)
			{
				return new SqlDouble((double)x.Value);
			}
			return SqlDouble.Null;
		}

		// Token: 0x06002CF8 RID: 11512 RVA: 0x002CBF98 File Offset: 0x002CB398
		public static implicit operator SqlDouble(SqlMoney x)
		{
			if (!x.IsNull)
			{
				return new SqlDouble(x.ToDouble());
			}
			return SqlDouble.Null;
		}

		// Token: 0x06002CF9 RID: 11513 RVA: 0x002CBFC8 File Offset: 0x002CB3C8
		public static implicit operator SqlDouble(SqlDecimal x)
		{
			if (!x.IsNull)
			{
				return new SqlDouble(x.ToDouble());
			}
			return SqlDouble.Null;
		}

		// Token: 0x06002CFA RID: 11514 RVA: 0x002CBFF8 File Offset: 0x002CB3F8
		public static explicit operator SqlDouble(SqlString x)
		{
			if (x.IsNull)
			{
				return SqlDouble.Null;
			}
			return SqlDouble.Parse(x.Value);
		}

		// Token: 0x06002CFB RID: 11515 RVA: 0x002CC028 File Offset: 0x002CB428
		public static SqlBoolean operator ==(SqlDouble x, SqlDouble y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.m_value == y.m_value);
			}
			return SqlBoolean.Null;
		}

		// Token: 0x06002CFC RID: 11516 RVA: 0x002CC068 File Offset: 0x002CB468
		public static SqlBoolean operator !=(SqlDouble x, SqlDouble y)
		{
			return !(x == y);
		}

		// Token: 0x06002CFD RID: 11517 RVA: 0x002CC088 File Offset: 0x002CB488
		public static SqlBoolean operator <(SqlDouble x, SqlDouble y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.m_value < y.m_value);
			}
			return SqlBoolean.Null;
		}

		// Token: 0x06002CFE RID: 11518 RVA: 0x002CC0C8 File Offset: 0x002CB4C8
		public static SqlBoolean operator >(SqlDouble x, SqlDouble y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.m_value > y.m_value);
			}
			return SqlBoolean.Null;
		}

		// Token: 0x06002CFF RID: 11519 RVA: 0x002CC108 File Offset: 0x002CB508
		public static SqlBoolean operator <=(SqlDouble x, SqlDouble y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.m_value <= y.m_value);
			}
			return SqlBoolean.Null;
		}

		// Token: 0x06002D00 RID: 11520 RVA: 0x002CC148 File Offset: 0x002CB548
		public static SqlBoolean operator >=(SqlDouble x, SqlDouble y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.m_value >= y.m_value);
			}
			return SqlBoolean.Null;
		}

		// Token: 0x06002D01 RID: 11521 RVA: 0x002CC188 File Offset: 0x002CB588
		public static SqlDouble Add(SqlDouble x, SqlDouble y)
		{
			return x + y;
		}

		// Token: 0x06002D02 RID: 11522 RVA: 0x002CC1A8 File Offset: 0x002CB5A8
		public static SqlDouble Subtract(SqlDouble x, SqlDouble y)
		{
			return x - y;
		}

		// Token: 0x06002D03 RID: 11523 RVA: 0x002CC1C8 File Offset: 0x002CB5C8
		public static SqlDouble Multiply(SqlDouble x, SqlDouble y)
		{
			return x * y;
		}

		// Token: 0x06002D04 RID: 11524 RVA: 0x002CC1E8 File Offset: 0x002CB5E8
		public static SqlDouble Divide(SqlDouble x, SqlDouble y)
		{
			return x / y;
		}

		// Token: 0x06002D05 RID: 11525 RVA: 0x002CC208 File Offset: 0x002CB608
		public static SqlBoolean Equals(SqlDouble x, SqlDouble y)
		{
			return x == y;
		}

		// Token: 0x06002D06 RID: 11526 RVA: 0x002CC228 File Offset: 0x002CB628
		public static SqlBoolean NotEquals(SqlDouble x, SqlDouble y)
		{
			return x != y;
		}

		// Token: 0x06002D07 RID: 11527 RVA: 0x002CC248 File Offset: 0x002CB648
		public static SqlBoolean LessThan(SqlDouble x, SqlDouble y)
		{
			return x < y;
		}

		// Token: 0x06002D08 RID: 11528 RVA: 0x002CC268 File Offset: 0x002CB668
		public static SqlBoolean GreaterThan(SqlDouble x, SqlDouble y)
		{
			return x > y;
		}

		// Token: 0x06002D09 RID: 11529 RVA: 0x002CC288 File Offset: 0x002CB688
		public static SqlBoolean LessThanOrEqual(SqlDouble x, SqlDouble y)
		{
			return x <= y;
		}

		// Token: 0x06002D0A RID: 11530 RVA: 0x002CC2A8 File Offset: 0x002CB6A8
		public static SqlBoolean GreaterThanOrEqual(SqlDouble x, SqlDouble y)
		{
			return x >= y;
		}

		// Token: 0x06002D0B RID: 11531 RVA: 0x002CC2C8 File Offset: 0x002CB6C8
		public SqlBoolean ToSqlBoolean()
		{
			return (SqlBoolean)this;
		}

		// Token: 0x06002D0C RID: 11532 RVA: 0x002CC2E8 File Offset: 0x002CB6E8
		public SqlByte ToSqlByte()
		{
			return (SqlByte)this;
		}

		// Token: 0x06002D0D RID: 11533 RVA: 0x002CC308 File Offset: 0x002CB708
		public SqlInt16 ToSqlInt16()
		{
			return (SqlInt16)this;
		}

		// Token: 0x06002D0E RID: 11534 RVA: 0x002CC328 File Offset: 0x002CB728
		public SqlInt32 ToSqlInt32()
		{
			return (SqlInt32)this;
		}

		// Token: 0x06002D0F RID: 11535 RVA: 0x002CC348 File Offset: 0x002CB748
		public SqlInt64 ToSqlInt64()
		{
			return (SqlInt64)this;
		}

		// Token: 0x06002D10 RID: 11536 RVA: 0x002CC368 File Offset: 0x002CB768
		public SqlMoney ToSqlMoney()
		{
			return (SqlMoney)this;
		}

		// Token: 0x06002D11 RID: 11537 RVA: 0x002CC388 File Offset: 0x002CB788
		public SqlDecimal ToSqlDecimal()
		{
			return (SqlDecimal)this;
		}

		// Token: 0x06002D12 RID: 11538 RVA: 0x002CC3A8 File Offset: 0x002CB7A8
		public SqlSingle ToSqlSingle()
		{
			return (SqlSingle)this;
		}

		// Token: 0x06002D13 RID: 11539 RVA: 0x002CC3C8 File Offset: 0x002CB7C8
		public SqlString ToSqlString()
		{
			return (SqlString)this;
		}

		// Token: 0x06002D14 RID: 11540 RVA: 0x002CC3E8 File Offset: 0x002CB7E8
		public int CompareTo(object value)
		{
			if (value is SqlDouble)
			{
				SqlDouble value2 = (SqlDouble)value;
				return this.CompareTo(value2);
			}
			throw ADP.WrongType(value.GetType(), typeof(SqlDouble));
		}

		// Token: 0x06002D15 RID: 11541 RVA: 0x002CC428 File Offset: 0x002CB828
		public int CompareTo(SqlDouble value)
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

		// Token: 0x06002D16 RID: 11542 RVA: 0x002CC488 File Offset: 0x002CB888
		public override bool Equals(object value)
		{
			if (!(value is SqlDouble))
			{
				return false;
			}
			SqlDouble y = (SqlDouble)value;
			if (y.IsNull || this.IsNull)
			{
				return y.IsNull && this.IsNull;
			}
			return (this == y).Value;
		}

		// Token: 0x06002D17 RID: 11543 RVA: 0x002CC4E8 File Offset: 0x002CB8E8
		public override int GetHashCode()
		{
			if (!this.IsNull)
			{
				return this.Value.GetHashCode();
			}
			return 0;
		}

		// Token: 0x06002D18 RID: 11544 RVA: 0x002CC518 File Offset: 0x002CB918
		XmlSchema IXmlSerializable.GetSchema()
		{
			return null;
		}

		// Token: 0x06002D19 RID: 11545 RVA: 0x002CC528 File Offset: 0x002CB928
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			string attribute = reader.GetAttribute("nil", "http://www.w3.org/2001/XMLSchema-instance");
			if (attribute != null && XmlConvert.ToBoolean(attribute))
			{
				this.m_fNotNull = false;
				return;
			}
			this.m_value = XmlConvert.ToDouble(reader.ReadElementString());
			this.m_fNotNull = true;
		}

		// Token: 0x06002D1A RID: 11546 RVA: 0x002CC578 File Offset: 0x002CB978
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			if (this.IsNull)
			{
				writer.WriteAttributeString("xsi", "nil", "http://www.w3.org/2001/XMLSchema-instance", "true");
				return;
			}
			writer.WriteString(XmlConvert.ToString(this.m_value));
		}

		// Token: 0x06002D1B RID: 11547 RVA: 0x002CC5C8 File Offset: 0x002CB9C8
		public static XmlQualifiedName GetXsdType(XmlSchemaSet schemaSet)
		{
			return new XmlQualifiedName("double", "http://www.w3.org/2001/XMLSchema");
		}

		// Token: 0x04001CE7 RID: 7399
		private bool m_fNotNull;

		// Token: 0x04001CE8 RID: 7400
		private double m_value;

		// Token: 0x04001CE9 RID: 7401
		public static readonly SqlDouble Null = new SqlDouble(true);

		// Token: 0x04001CEA RID: 7402
		public static readonly SqlDouble Zero = new SqlDouble(0.0);

		// Token: 0x04001CEB RID: 7403
		public static readonly SqlDouble MinValue = new SqlDouble(double.MinValue);

		// Token: 0x04001CEC RID: 7404
		public static readonly SqlDouble MaxValue = new SqlDouble(double.MaxValue);
	}
}
