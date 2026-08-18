using System;
using System.Data.Common;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System.Data.SqlTypes
{
	// Token: 0x02000161 RID: 353
	[XmlSchemaProvider("GetXsdType")]
	[Serializable]
	public struct SqlInt16 : INullable, IComparable, IXmlSerializable
	{
		// Token: 0x060015C7 RID: 5575 RVA: 0x000A3814 File Offset: 0x000A2C14
		private SqlInt16(bool fNull)
		{
			this.m_fNotNull = false;
			this.m_value = 0;
		}

		// Token: 0x060015C8 RID: 5576 RVA: 0x000A3830 File Offset: 0x000A2C30
		public SqlInt16(short value)
		{
			this.m_value = value;
			this.m_fNotNull = true;
		}

		// Token: 0x1700032B RID: 811
		// (get) Token: 0x060015C9 RID: 5577 RVA: 0x000A384C File Offset: 0x000A2C4C
		public bool IsNull
		{
			get
			{
				return !this.m_fNotNull;
			}
		}

		// Token: 0x1700032C RID: 812
		// (get) Token: 0x060015CA RID: 5578 RVA: 0x000A3864 File Offset: 0x000A2C64
		public short Value
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

		// Token: 0x060015CB RID: 5579 RVA: 0x000A3888 File Offset: 0x000A2C88
		public static implicit operator SqlInt16(short x)
		{
			return new SqlInt16(x);
		}

		// Token: 0x060015CC RID: 5580 RVA: 0x000A389C File Offset: 0x000A2C9C
		public static explicit operator short(SqlInt16 x)
		{
			return x.Value;
		}

		// Token: 0x060015CD RID: 5581 RVA: 0x000A38B0 File Offset: 0x000A2CB0
		public override string ToString()
		{
			if (!this.IsNull)
			{
				return this.m_value.ToString(null);
			}
			return SQLResource.NullString;
		}

		// Token: 0x060015CE RID: 5582 RVA: 0x000A38D8 File Offset: 0x000A2CD8
		public static SqlInt16 Parse(string s)
		{
			if (s == SQLResource.NullString)
			{
				return SqlInt16.Null;
			}
			return new SqlInt16(short.Parse(s, null));
		}

		// Token: 0x060015CF RID: 5583 RVA: 0x000A3904 File Offset: 0x000A2D04
		public static SqlInt16 operator -(SqlInt16 x)
		{
			if (!x.IsNull)
			{
				return new SqlInt16(-x.m_value);
			}
			return SqlInt16.Null;
		}

		// Token: 0x060015D0 RID: 5584 RVA: 0x000A3930 File Offset: 0x000A2D30
		public static SqlInt16 operator ~(SqlInt16 x)
		{
			if (!x.IsNull)
			{
				return new SqlInt16(~x.m_value);
			}
			return SqlInt16.Null;
		}

		// Token: 0x060015D1 RID: 5585 RVA: 0x000A395C File Offset: 0x000A2D5C
		public static SqlInt16 operator +(SqlInt16 x, SqlInt16 y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlInt16.Null;
			}
			int num = (int)(x.m_value + y.m_value);
			if (((num >> 15 ^ num >> 16) & 1) != 0)
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			return new SqlInt16((short)num);
		}

		// Token: 0x060015D2 RID: 5586 RVA: 0x000A39B0 File Offset: 0x000A2DB0
		public static SqlInt16 operator -(SqlInt16 x, SqlInt16 y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlInt16.Null;
			}
			int num = (int)(x.m_value - y.m_value);
			if (((num >> 15 ^ num >> 16) & 1) != 0)
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			return new SqlInt16((short)num);
		}

		// Token: 0x060015D3 RID: 5587 RVA: 0x000A3A04 File Offset: 0x000A2E04
		public static SqlInt16 operator *(SqlInt16 x, SqlInt16 y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlInt16.Null;
			}
			int num = (int)(x.m_value * y.m_value);
			int num2 = num & -32768;
			if (num2 != 0 && num2 != -32768)
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			return new SqlInt16((short)num);
		}

		// Token: 0x060015D4 RID: 5588 RVA: 0x000A3A5C File Offset: 0x000A2E5C
		public static SqlInt16 operator /(SqlInt16 x, SqlInt16 y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlInt16.Null;
			}
			if (y.m_value == 0)
			{
				throw new DivideByZeroException(SQLResource.DivideByZeroMessage);
			}
			if (x.m_value == -32768 && y.m_value == -1)
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			return new SqlInt16(x.m_value / y.m_value);
		}

		// Token: 0x060015D5 RID: 5589 RVA: 0x000A3AC8 File Offset: 0x000A2EC8
		public static SqlInt16 operator %(SqlInt16 x, SqlInt16 y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlInt16.Null;
			}
			if (y.m_value == 0)
			{
				throw new DivideByZeroException(SQLResource.DivideByZeroMessage);
			}
			if (x.m_value == -32768 && y.m_value == -1)
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			return new SqlInt16(x.m_value % y.m_value);
		}

		// Token: 0x060015D6 RID: 5590 RVA: 0x000A3B34 File Offset: 0x000A2F34
		public static SqlInt16 operator &(SqlInt16 x, SqlInt16 y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlInt16(x.m_value & y.m_value);
			}
			return SqlInt16.Null;
		}

		// Token: 0x060015D7 RID: 5591 RVA: 0x000A3B6C File Offset: 0x000A2F6C
		public static SqlInt16 operator |(SqlInt16 x, SqlInt16 y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlInt16((short)((ushort)x.m_value | (ushort)y.m_value));
			}
			return SqlInt16.Null;
		}

		// Token: 0x060015D8 RID: 5592 RVA: 0x000A3BA8 File Offset: 0x000A2FA8
		public static SqlInt16 operator ^(SqlInt16 x, SqlInt16 y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlInt16(x.m_value ^ y.m_value);
			}
			return SqlInt16.Null;
		}

		// Token: 0x060015D9 RID: 5593 RVA: 0x000A3BE0 File Offset: 0x000A2FE0
		public static explicit operator SqlInt16(SqlBoolean x)
		{
			if (!x.IsNull)
			{
				return new SqlInt16((short)x.ByteValue);
			}
			return SqlInt16.Null;
		}

		// Token: 0x060015DA RID: 5594 RVA: 0x000A3C08 File Offset: 0x000A3008
		public static implicit operator SqlInt16(SqlByte x)
		{
			if (!x.IsNull)
			{
				return new SqlInt16((short)x.Value);
			}
			return SqlInt16.Null;
		}

		// Token: 0x060015DB RID: 5595 RVA: 0x000A3C30 File Offset: 0x000A3030
		public static explicit operator SqlInt16(SqlInt32 x)
		{
			if (x.IsNull)
			{
				return SqlInt16.Null;
			}
			int value = x.Value;
			if (value > 32767 || value < -32768)
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			return new SqlInt16((short)value);
		}

		// Token: 0x060015DC RID: 5596 RVA: 0x000A3C78 File Offset: 0x000A3078
		public static explicit operator SqlInt16(SqlInt64 x)
		{
			if (x.IsNull)
			{
				return SqlInt16.Null;
			}
			long value = x.Value;
			if (value > 32767L || value < -32768L)
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			return new SqlInt16((short)value);
		}

		// Token: 0x060015DD RID: 5597 RVA: 0x000A3CC0 File Offset: 0x000A30C0
		public static explicit operator SqlInt16(SqlSingle x)
		{
			if (x.IsNull)
			{
				return SqlInt16.Null;
			}
			float value = x.Value;
			if (value < -32768f || value > 32767f)
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			return new SqlInt16((short)value);
		}

		// Token: 0x060015DE RID: 5598 RVA: 0x000A3D08 File Offset: 0x000A3108
		public static explicit operator SqlInt16(SqlDouble x)
		{
			if (x.IsNull)
			{
				return SqlInt16.Null;
			}
			double value = x.Value;
			if (value < -32768.0 || value > 32767.0)
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			return new SqlInt16((short)value);
		}

		// Token: 0x060015DF RID: 5599 RVA: 0x000A3D58 File Offset: 0x000A3158
		public static explicit operator SqlInt16(SqlMoney x)
		{
			if (!x.IsNull)
			{
				return new SqlInt16(checked((short)x.ToInt32()));
			}
			return SqlInt16.Null;
		}

		// Token: 0x060015E0 RID: 5600 RVA: 0x000A3D84 File Offset: 0x000A3184
		public static explicit operator SqlInt16(SqlDecimal x)
		{
			return (SqlInt16)((SqlInt32)x);
		}

		// Token: 0x060015E1 RID: 5601 RVA: 0x000A3D9C File Offset: 0x000A319C
		public static explicit operator SqlInt16(SqlString x)
		{
			if (!x.IsNull)
			{
				return new SqlInt16(short.Parse(x.Value, null));
			}
			return SqlInt16.Null;
		}

		// Token: 0x060015E2 RID: 5602 RVA: 0x000A3DCC File Offset: 0x000A31CC
		public static SqlBoolean operator ==(SqlInt16 x, SqlInt16 y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.m_value == y.m_value);
			}
			return SqlBoolean.Null;
		}

		// Token: 0x060015E3 RID: 5603 RVA: 0x000A3E04 File Offset: 0x000A3204
		public static SqlBoolean operator !=(SqlInt16 x, SqlInt16 y)
		{
			return !(x == y);
		}

		// Token: 0x060015E4 RID: 5604 RVA: 0x000A3E20 File Offset: 0x000A3220
		public static SqlBoolean operator <(SqlInt16 x, SqlInt16 y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.m_value < y.m_value);
			}
			return SqlBoolean.Null;
		}

		// Token: 0x060015E5 RID: 5605 RVA: 0x000A3E58 File Offset: 0x000A3258
		public static SqlBoolean operator >(SqlInt16 x, SqlInt16 y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.m_value > y.m_value);
			}
			return SqlBoolean.Null;
		}

		// Token: 0x060015E6 RID: 5606 RVA: 0x000A3E90 File Offset: 0x000A3290
		public static SqlBoolean operator <=(SqlInt16 x, SqlInt16 y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.m_value <= y.m_value);
			}
			return SqlBoolean.Null;
		}

		// Token: 0x060015E7 RID: 5607 RVA: 0x000A3ECC File Offset: 0x000A32CC
		public static SqlBoolean operator >=(SqlInt16 x, SqlInt16 y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.m_value >= y.m_value);
			}
			return SqlBoolean.Null;
		}

		// Token: 0x060015E8 RID: 5608 RVA: 0x000A3F08 File Offset: 0x000A3308
		public static SqlInt16 OnesComplement(SqlInt16 x)
		{
			return ~x;
		}

		// Token: 0x060015E9 RID: 5609 RVA: 0x000A3F1C File Offset: 0x000A331C
		public static SqlInt16 Add(SqlInt16 x, SqlInt16 y)
		{
			return x + y;
		}

		// Token: 0x060015EA RID: 5610 RVA: 0x000A3F30 File Offset: 0x000A3330
		public static SqlInt16 Subtract(SqlInt16 x, SqlInt16 y)
		{
			return x - y;
		}

		// Token: 0x060015EB RID: 5611 RVA: 0x000A3F44 File Offset: 0x000A3344
		public static SqlInt16 Multiply(SqlInt16 x, SqlInt16 y)
		{
			return x * y;
		}

		// Token: 0x060015EC RID: 5612 RVA: 0x000A3F58 File Offset: 0x000A3358
		public static SqlInt16 Divide(SqlInt16 x, SqlInt16 y)
		{
			return x / y;
		}

		// Token: 0x060015ED RID: 5613 RVA: 0x000A3F6C File Offset: 0x000A336C
		public static SqlInt16 Mod(SqlInt16 x, SqlInt16 y)
		{
			return x % y;
		}

		// Token: 0x060015EE RID: 5614 RVA: 0x000A3F80 File Offset: 0x000A3380
		public static SqlInt16 Modulus(SqlInt16 x, SqlInt16 y)
		{
			return x % y;
		}

		// Token: 0x060015EF RID: 5615 RVA: 0x000A3F94 File Offset: 0x000A3394
		public static SqlInt16 BitwiseAnd(SqlInt16 x, SqlInt16 y)
		{
			return x & y;
		}

		// Token: 0x060015F0 RID: 5616 RVA: 0x000A3FA8 File Offset: 0x000A33A8
		public static SqlInt16 BitwiseOr(SqlInt16 x, SqlInt16 y)
		{
			return x | y;
		}

		// Token: 0x060015F1 RID: 5617 RVA: 0x000A3FBC File Offset: 0x000A33BC
		public static SqlInt16 Xor(SqlInt16 x, SqlInt16 y)
		{
			return x ^ y;
		}

		// Token: 0x060015F2 RID: 5618 RVA: 0x000A3FD0 File Offset: 0x000A33D0
		public static SqlBoolean Equals(SqlInt16 x, SqlInt16 y)
		{
			return x == y;
		}

		// Token: 0x060015F3 RID: 5619 RVA: 0x000A3FE4 File Offset: 0x000A33E4
		public static SqlBoolean NotEquals(SqlInt16 x, SqlInt16 y)
		{
			return x != y;
		}

		// Token: 0x060015F4 RID: 5620 RVA: 0x000A3FF8 File Offset: 0x000A33F8
		public static SqlBoolean LessThan(SqlInt16 x, SqlInt16 y)
		{
			return x < y;
		}

		// Token: 0x060015F5 RID: 5621 RVA: 0x000A400C File Offset: 0x000A340C
		public static SqlBoolean GreaterThan(SqlInt16 x, SqlInt16 y)
		{
			return x > y;
		}

		// Token: 0x060015F6 RID: 5622 RVA: 0x000A4020 File Offset: 0x000A3420
		public static SqlBoolean LessThanOrEqual(SqlInt16 x, SqlInt16 y)
		{
			return x <= y;
		}

		// Token: 0x060015F7 RID: 5623 RVA: 0x000A4034 File Offset: 0x000A3434
		public static SqlBoolean GreaterThanOrEqual(SqlInt16 x, SqlInt16 y)
		{
			return x >= y;
		}

		// Token: 0x060015F8 RID: 5624 RVA: 0x000A4048 File Offset: 0x000A3448
		public SqlBoolean ToSqlBoolean()
		{
			return (SqlBoolean)this;
		}

		// Token: 0x060015F9 RID: 5625 RVA: 0x000A4060 File Offset: 0x000A3460
		public SqlByte ToSqlByte()
		{
			return (SqlByte)this;
		}

		// Token: 0x060015FA RID: 5626 RVA: 0x000A4078 File Offset: 0x000A3478
		public SqlDouble ToSqlDouble()
		{
			return this;
		}

		// Token: 0x060015FB RID: 5627 RVA: 0x000A4090 File Offset: 0x000A3490
		public SqlInt32 ToSqlInt32()
		{
			return this;
		}

		// Token: 0x060015FC RID: 5628 RVA: 0x000A40A8 File Offset: 0x000A34A8
		public SqlInt64 ToSqlInt64()
		{
			return this;
		}

		// Token: 0x060015FD RID: 5629 RVA: 0x000A40C0 File Offset: 0x000A34C0
		public SqlMoney ToSqlMoney()
		{
			return this;
		}

		// Token: 0x060015FE RID: 5630 RVA: 0x000A40D8 File Offset: 0x000A34D8
		public SqlDecimal ToSqlDecimal()
		{
			return this;
		}

		// Token: 0x060015FF RID: 5631 RVA: 0x000A40F0 File Offset: 0x000A34F0
		public SqlSingle ToSqlSingle()
		{
			return this;
		}

		// Token: 0x06001600 RID: 5632 RVA: 0x000A4108 File Offset: 0x000A3508
		public SqlString ToSqlString()
		{
			return (SqlString)this;
		}

		// Token: 0x06001601 RID: 5633 RVA: 0x000A4120 File Offset: 0x000A3520
		public int CompareTo(object value)
		{
			if (value is SqlInt16)
			{
				SqlInt16 value2 = (SqlInt16)value;
				return this.CompareTo(value2);
			}
			throw ADP.WrongType(value.GetType(), typeof(SqlInt16));
		}

		// Token: 0x06001602 RID: 5634 RVA: 0x000A415C File Offset: 0x000A355C
		public int CompareTo(SqlInt16 value)
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

		// Token: 0x06001603 RID: 5635 RVA: 0x000A41B4 File Offset: 0x000A35B4
		public override bool Equals(object value)
		{
			if (!(value is SqlInt16))
			{
				return false;
			}
			SqlInt16 y = (SqlInt16)value;
			if (y.IsNull || this.IsNull)
			{
				return y.IsNull && this.IsNull;
			}
			return (this == y).Value;
		}

		// Token: 0x06001604 RID: 5636 RVA: 0x000A420C File Offset: 0x000A360C
		public override int GetHashCode()
		{
			if (!this.IsNull)
			{
				return this.Value.GetHashCode();
			}
			return 0;
		}

		// Token: 0x06001605 RID: 5637 RVA: 0x000A4234 File Offset: 0x000A3634
		XmlSchema IXmlSerializable.GetSchema()
		{
			return null;
		}

		// Token: 0x06001606 RID: 5638 RVA: 0x000A4244 File Offset: 0x000A3644
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			string attribute = reader.GetAttribute("nil", "http://www.w3.org/2001/XMLSchema-instance");
			if (attribute != null && XmlConvert.ToBoolean(attribute))
			{
				reader.ReadElementString();
				this.m_fNotNull = false;
				return;
			}
			this.m_value = XmlConvert.ToInt16(reader.ReadElementString());
			this.m_fNotNull = true;
		}

		// Token: 0x06001607 RID: 5639 RVA: 0x000A4294 File Offset: 0x000A3694
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			if (this.IsNull)
			{
				writer.WriteAttributeString("xsi", "nil", "http://www.w3.org/2001/XMLSchema-instance", "true");
				return;
			}
			writer.WriteString(XmlConvert.ToString(this.m_value));
		}

		// Token: 0x06001608 RID: 5640 RVA: 0x000A42D8 File Offset: 0x000A36D8
		public static XmlQualifiedName GetXsdType(XmlSchemaSet schemaSet)
		{
			return new XmlQualifiedName("short", "http://www.w3.org/2001/XMLSchema");
		}

		// Token: 0x04000DE8 RID: 3560
		private bool m_fNotNull;

		// Token: 0x04000DE9 RID: 3561
		private short m_value;

		// Token: 0x04000DEA RID: 3562
		private const int O_MASKI2 = -32768;

		// Token: 0x04000DEB RID: 3563
		public static readonly SqlInt16 Null = new SqlInt16(true);

		// Token: 0x04000DEC RID: 3564
		public static readonly SqlInt16 Zero = new SqlInt16(0);

		// Token: 0x04000DED RID: 3565
		public static readonly SqlInt16 MinValue = new SqlInt16(short.MinValue);

		// Token: 0x04000DEE RID: 3566
		public static readonly SqlInt16 MaxValue = new SqlInt16(short.MaxValue);
	}
}
