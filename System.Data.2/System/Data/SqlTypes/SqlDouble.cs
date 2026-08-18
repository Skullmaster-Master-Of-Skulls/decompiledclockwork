using System;
using System.Data.Common;
using System.Globalization;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System.Data.SqlTypes
{
	// Token: 0x0200015B RID: 347
	[XmlSchemaProvider("GetXsdType")]
	[Serializable]
	public struct SqlDouble : INullable, IComparable, IXmlSerializable
	{
		// Token: 0x0600153A RID: 5434 RVA: 0x000A1978 File Offset: 0x000A0D78
		private SqlDouble(bool fNull)
		{
			this.m_fNotNull = false;
			this.m_value = 0.0;
		}

		// Token: 0x0600153B RID: 5435 RVA: 0x000A199C File Offset: 0x000A0D9C
		public SqlDouble(double value)
		{
			if (double.IsInfinity(value) || double.IsNaN(value))
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			this.m_value = value;
			this.m_fNotNull = true;
		}

		// Token: 0x1700031C RID: 796
		// (get) Token: 0x0600153C RID: 5436 RVA: 0x000A19D4 File Offset: 0x000A0DD4
		public bool IsNull
		{
			get
			{
				return !this.m_fNotNull;
			}
		}

		// Token: 0x1700031D RID: 797
		// (get) Token: 0x0600153D RID: 5437 RVA: 0x000A19EC File Offset: 0x000A0DEC
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

		// Token: 0x0600153E RID: 5438 RVA: 0x000A1A10 File Offset: 0x000A0E10
		public static implicit operator SqlDouble(double x)
		{
			return new SqlDouble(x);
		}

		// Token: 0x0600153F RID: 5439 RVA: 0x000A1A24 File Offset: 0x000A0E24
		public static explicit operator double(SqlDouble x)
		{
			return x.Value;
		}

		// Token: 0x06001540 RID: 5440 RVA: 0x000A1A38 File Offset: 0x000A0E38
		public override string ToString()
		{
			if (!this.IsNull)
			{
				return this.m_value.ToString(null);
			}
			return SQLResource.NullString;
		}

		// Token: 0x06001541 RID: 5441 RVA: 0x000A1A60 File Offset: 0x000A0E60
		public static SqlDouble Parse(string s)
		{
			if (s == SQLResource.NullString)
			{
				return SqlDouble.Null;
			}
			return new SqlDouble(double.Parse(s, CultureInfo.InvariantCulture));
		}

		// Token: 0x06001542 RID: 5442 RVA: 0x000A1A90 File Offset: 0x000A0E90
		public static SqlDouble operator -(SqlDouble x)
		{
			if (!x.IsNull)
			{
				return new SqlDouble(-x.m_value);
			}
			return SqlDouble.Null;
		}

		// Token: 0x06001543 RID: 5443 RVA: 0x000A1AB8 File Offset: 0x000A0EB8
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

		// Token: 0x06001544 RID: 5444 RVA: 0x000A1B04 File Offset: 0x000A0F04
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

		// Token: 0x06001545 RID: 5445 RVA: 0x000A1B50 File Offset: 0x000A0F50
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

		// Token: 0x06001546 RID: 5446 RVA: 0x000A1B9C File Offset: 0x000A0F9C
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

		// Token: 0x06001547 RID: 5447 RVA: 0x000A1C04 File Offset: 0x000A1004
		public static explicit operator SqlDouble(SqlBoolean x)
		{
			if (!x.IsNull)
			{
				return new SqlDouble((double)x.ByteValue);
			}
			return SqlDouble.Null;
		}

		// Token: 0x06001548 RID: 5448 RVA: 0x000A1C30 File Offset: 0x000A1030
		public static implicit operator SqlDouble(SqlByte x)
		{
			if (!x.IsNull)
			{
				return new SqlDouble((double)x.Value);
			}
			return SqlDouble.Null;
		}

		// Token: 0x06001549 RID: 5449 RVA: 0x000A1C5C File Offset: 0x000A105C
		public static implicit operator SqlDouble(SqlInt16 x)
		{
			if (!x.IsNull)
			{
				return new SqlDouble((double)x.Value);
			}
			return SqlDouble.Null;
		}

		// Token: 0x0600154A RID: 5450 RVA: 0x000A1C88 File Offset: 0x000A1088
		public static implicit operator SqlDouble(SqlInt32 x)
		{
			if (!x.IsNull)
			{
				return new SqlDouble((double)x.Value);
			}
			return SqlDouble.Null;
		}

		// Token: 0x0600154B RID: 5451 RVA: 0x000A1CB4 File Offset: 0x000A10B4
		public static implicit operator SqlDouble(SqlInt64 x)
		{
			if (!x.IsNull)
			{
				return new SqlDouble((double)x.Value);
			}
			return SqlDouble.Null;
		}

		// Token: 0x0600154C RID: 5452 RVA: 0x000A1CE0 File Offset: 0x000A10E0
		public static implicit operator SqlDouble(SqlSingle x)
		{
			if (!x.IsNull)
			{
				return new SqlDouble((double)x.Value);
			}
			return SqlDouble.Null;
		}

		// Token: 0x0600154D RID: 5453 RVA: 0x000A1D0C File Offset: 0x000A110C
		public static implicit operator SqlDouble(SqlMoney x)
		{
			if (!x.IsNull)
			{
				return new SqlDouble(x.ToDouble());
			}
			return SqlDouble.Null;
		}

		// Token: 0x0600154E RID: 5454 RVA: 0x000A1D34 File Offset: 0x000A1134
		public static implicit operator SqlDouble(SqlDecimal x)
		{
			if (!x.IsNull)
			{
				return new SqlDouble(x.ToDouble());
			}
			return SqlDouble.Null;
		}

		// Token: 0x0600154F RID: 5455 RVA: 0x000A1D5C File Offset: 0x000A115C
		public static explicit operator SqlDouble(SqlString x)
		{
			if (x.IsNull)
			{
				return SqlDouble.Null;
			}
			return SqlDouble.Parse(x.Value);
		}

		// Token: 0x06001550 RID: 5456 RVA: 0x000A1D84 File Offset: 0x000A1184
		public static SqlBoolean operator ==(SqlDouble x, SqlDouble y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.m_value == y.m_value);
			}
			return SqlBoolean.Null;
		}

		// Token: 0x06001551 RID: 5457 RVA: 0x000A1DBC File Offset: 0x000A11BC
		public static SqlBoolean operator !=(SqlDouble x, SqlDouble y)
		{
			return !(x == y);
		}

		// Token: 0x06001552 RID: 5458 RVA: 0x000A1DD8 File Offset: 0x000A11D8
		public static SqlBoolean operator <(SqlDouble x, SqlDouble y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.m_value < y.m_value);
			}
			return SqlBoolean.Null;
		}

		// Token: 0x06001553 RID: 5459 RVA: 0x000A1E10 File Offset: 0x000A1210
		public static SqlBoolean operator >(SqlDouble x, SqlDouble y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.m_value > y.m_value);
			}
			return SqlBoolean.Null;
		}

		// Token: 0x06001554 RID: 5460 RVA: 0x000A1E48 File Offset: 0x000A1248
		public static SqlBoolean operator <=(SqlDouble x, SqlDouble y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.m_value <= y.m_value);
			}
			return SqlBoolean.Null;
		}

		// Token: 0x06001555 RID: 5461 RVA: 0x000A1E84 File Offset: 0x000A1284
		public static SqlBoolean operator >=(SqlDouble x, SqlDouble y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.m_value >= y.m_value);
			}
			return SqlBoolean.Null;
		}

		// Token: 0x06001556 RID: 5462 RVA: 0x000A1EC0 File Offset: 0x000A12C0
		public static SqlDouble Add(SqlDouble x, SqlDouble y)
		{
			return x + y;
		}

		// Token: 0x06001557 RID: 5463 RVA: 0x000A1ED4 File Offset: 0x000A12D4
		public static SqlDouble Subtract(SqlDouble x, SqlDouble y)
		{
			return x - y;
		}

		// Token: 0x06001558 RID: 5464 RVA: 0x000A1EE8 File Offset: 0x000A12E8
		public static SqlDouble Multiply(SqlDouble x, SqlDouble y)
		{
			return x * y;
		}

		// Token: 0x06001559 RID: 5465 RVA: 0x000A1EFC File Offset: 0x000A12FC
		public static SqlDouble Divide(SqlDouble x, SqlDouble y)
		{
			return x / y;
		}

		// Token: 0x0600155A RID: 5466 RVA: 0x000A1F10 File Offset: 0x000A1310
		public static SqlBoolean Equals(SqlDouble x, SqlDouble y)
		{
			return x == y;
		}

		// Token: 0x0600155B RID: 5467 RVA: 0x000A1F24 File Offset: 0x000A1324
		public static SqlBoolean NotEquals(SqlDouble x, SqlDouble y)
		{
			return x != y;
		}

		// Token: 0x0600155C RID: 5468 RVA: 0x000A1F38 File Offset: 0x000A1338
		public static SqlBoolean LessThan(SqlDouble x, SqlDouble y)
		{
			return x < y;
		}

		// Token: 0x0600155D RID: 5469 RVA: 0x000A1F4C File Offset: 0x000A134C
		public static SqlBoolean GreaterThan(SqlDouble x, SqlDouble y)
		{
			return x > y;
		}

		// Token: 0x0600155E RID: 5470 RVA: 0x000A1F60 File Offset: 0x000A1360
		public static SqlBoolean LessThanOrEqual(SqlDouble x, SqlDouble y)
		{
			return x <= y;
		}

		// Token: 0x0600155F RID: 5471 RVA: 0x000A1F74 File Offset: 0x000A1374
		public static SqlBoolean GreaterThanOrEqual(SqlDouble x, SqlDouble y)
		{
			return x >= y;
		}

		// Token: 0x06001560 RID: 5472 RVA: 0x000A1F88 File Offset: 0x000A1388
		public SqlBoolean ToSqlBoolean()
		{
			return (SqlBoolean)this;
		}

		// Token: 0x06001561 RID: 5473 RVA: 0x000A1FA0 File Offset: 0x000A13A0
		public SqlByte ToSqlByte()
		{
			return (SqlByte)this;
		}

		// Token: 0x06001562 RID: 5474 RVA: 0x000A1FB8 File Offset: 0x000A13B8
		public SqlInt16 ToSqlInt16()
		{
			return (SqlInt16)this;
		}

		// Token: 0x06001563 RID: 5475 RVA: 0x000A1FD0 File Offset: 0x000A13D0
		public SqlInt32 ToSqlInt32()
		{
			return (SqlInt32)this;
		}

		// Token: 0x06001564 RID: 5476 RVA: 0x000A1FE8 File Offset: 0x000A13E8
		public SqlInt64 ToSqlInt64()
		{
			return (SqlInt64)this;
		}

		// Token: 0x06001565 RID: 5477 RVA: 0x000A2000 File Offset: 0x000A1400
		public SqlMoney ToSqlMoney()
		{
			return (SqlMoney)this;
		}

		// Token: 0x06001566 RID: 5478 RVA: 0x000A2018 File Offset: 0x000A1418
		public SqlDecimal ToSqlDecimal()
		{
			return (SqlDecimal)this;
		}

		// Token: 0x06001567 RID: 5479 RVA: 0x000A2030 File Offset: 0x000A1430
		public SqlSingle ToSqlSingle()
		{
			return (SqlSingle)this;
		}

		// Token: 0x06001568 RID: 5480 RVA: 0x000A2048 File Offset: 0x000A1448
		public SqlString ToSqlString()
		{
			return (SqlString)this;
		}

		// Token: 0x06001569 RID: 5481 RVA: 0x000A2060 File Offset: 0x000A1460
		public int CompareTo(object value)
		{
			if (value is SqlDouble)
			{
				SqlDouble value2 = (SqlDouble)value;
				return this.CompareTo(value2);
			}
			throw ADP.WrongType(value.GetType(), typeof(SqlDouble));
		}

		// Token: 0x0600156A RID: 5482 RVA: 0x000A209C File Offset: 0x000A149C
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

		// Token: 0x0600156B RID: 5483 RVA: 0x000A20F4 File Offset: 0x000A14F4
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

		// Token: 0x0600156C RID: 5484 RVA: 0x000A214C File Offset: 0x000A154C
		public override int GetHashCode()
		{
			if (!this.IsNull)
			{
				return this.Value.GetHashCode();
			}
			return 0;
		}

		// Token: 0x0600156D RID: 5485 RVA: 0x000A2174 File Offset: 0x000A1574
		XmlSchema IXmlSerializable.GetSchema()
		{
			return null;
		}

		// Token: 0x0600156E RID: 5486 RVA: 0x000A2184 File Offset: 0x000A1584
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			string attribute = reader.GetAttribute("nil", "http://www.w3.org/2001/XMLSchema-instance");
			if (attribute != null && XmlConvert.ToBoolean(attribute))
			{
				reader.ReadElementString();
				this.m_fNotNull = false;
				return;
			}
			this.m_value = XmlConvert.ToDouble(reader.ReadElementString());
			this.m_fNotNull = true;
		}

		// Token: 0x0600156F RID: 5487 RVA: 0x000A21D4 File Offset: 0x000A15D4
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			if (this.IsNull)
			{
				writer.WriteAttributeString("xsi", "nil", "http://www.w3.org/2001/XMLSchema-instance", "true");
				return;
			}
			writer.WriteString(XmlConvert.ToString(this.m_value));
		}

		// Token: 0x06001570 RID: 5488 RVA: 0x000A2218 File Offset: 0x000A1618
		public static XmlQualifiedName GetXsdType(XmlSchemaSet schemaSet)
		{
			return new XmlQualifiedName("double", "http://www.w3.org/2001/XMLSchema");
		}

		// Token: 0x04000DD0 RID: 3536
		private bool m_fNotNull;

		// Token: 0x04000DD1 RID: 3537
		private double m_value;

		// Token: 0x04000DD2 RID: 3538
		public static readonly SqlDouble Null = new SqlDouble(true);

		// Token: 0x04000DD3 RID: 3539
		public static readonly SqlDouble Zero = new SqlDouble(0.0);

		// Token: 0x04000DD4 RID: 3540
		public static readonly SqlDouble MinValue = new SqlDouble(double.MinValue);

		// Token: 0x04000DD5 RID: 3541
		public static readonly SqlDouble MaxValue = new SqlDouble(double.MaxValue);
	}
}
