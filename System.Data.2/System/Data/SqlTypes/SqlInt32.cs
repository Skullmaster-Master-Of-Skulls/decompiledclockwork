using System;
using System.Data.Common;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System.Data.SqlTypes
{
	// Token: 0x02000162 RID: 354
	[XmlSchemaProvider("GetXsdType")]
	[Serializable]
	public struct SqlInt32 : INullable, IComparable, IXmlSerializable
	{
		// Token: 0x0600160A RID: 5642 RVA: 0x000A4338 File Offset: 0x000A3738
		private SqlInt32(bool fNull)
		{
			this.m_fNotNull = false;
			this.m_value = 0;
		}

		// Token: 0x0600160B RID: 5643 RVA: 0x000A4354 File Offset: 0x000A3754
		public SqlInt32(int value)
		{
			this.m_value = value;
			this.m_fNotNull = true;
		}

		// Token: 0x1700032D RID: 813
		// (get) Token: 0x0600160C RID: 5644 RVA: 0x000A4370 File Offset: 0x000A3770
		public bool IsNull
		{
			get
			{
				return !this.m_fNotNull;
			}
		}

		// Token: 0x1700032E RID: 814
		// (get) Token: 0x0600160D RID: 5645 RVA: 0x000A4388 File Offset: 0x000A3788
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

		// Token: 0x0600160E RID: 5646 RVA: 0x000A43AC File Offset: 0x000A37AC
		public static implicit operator SqlInt32(int x)
		{
			return new SqlInt32(x);
		}

		// Token: 0x0600160F RID: 5647 RVA: 0x000A43C0 File Offset: 0x000A37C0
		public static explicit operator int(SqlInt32 x)
		{
			return x.Value;
		}

		// Token: 0x06001610 RID: 5648 RVA: 0x000A43D4 File Offset: 0x000A37D4
		public override string ToString()
		{
			if (!this.IsNull)
			{
				return this.m_value.ToString(null);
			}
			return SQLResource.NullString;
		}

		// Token: 0x06001611 RID: 5649 RVA: 0x000A43FC File Offset: 0x000A37FC
		public static SqlInt32 Parse(string s)
		{
			if (s == SQLResource.NullString)
			{
				return SqlInt32.Null;
			}
			return new SqlInt32(int.Parse(s, null));
		}

		// Token: 0x06001612 RID: 5650 RVA: 0x000A4428 File Offset: 0x000A3828
		public static SqlInt32 operator -(SqlInt32 x)
		{
			if (!x.IsNull)
			{
				return new SqlInt32(-x.m_value);
			}
			return SqlInt32.Null;
		}

		// Token: 0x06001613 RID: 5651 RVA: 0x000A4450 File Offset: 0x000A3850
		public static SqlInt32 operator ~(SqlInt32 x)
		{
			if (!x.IsNull)
			{
				return new SqlInt32(~x.m_value);
			}
			return SqlInt32.Null;
		}

		// Token: 0x06001614 RID: 5652 RVA: 0x000A4478 File Offset: 0x000A3878
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

		// Token: 0x06001615 RID: 5653 RVA: 0x000A44E0 File Offset: 0x000A38E0
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

		// Token: 0x06001616 RID: 5654 RVA: 0x000A4548 File Offset: 0x000A3948
		public static SqlInt32 operator *(SqlInt32 x, SqlInt32 y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlInt32.Null;
			}
			long num = (long)x.m_value * (long)y.m_value;
			long num2 = num & -2147483648L;
			if (num2 != 0L && num2 != -2147483648L)
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			return new SqlInt32((int)num);
		}

		// Token: 0x06001617 RID: 5655 RVA: 0x000A45A4 File Offset: 0x000A39A4
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
			if ((long)x.m_value == -2147483648L && y.m_value == -1)
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			return new SqlInt32(x.m_value / y.m_value);
		}

		// Token: 0x06001618 RID: 5656 RVA: 0x000A4614 File Offset: 0x000A3A14
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
			if ((long)x.m_value == -2147483648L && y.m_value == -1)
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			return new SqlInt32(x.m_value % y.m_value);
		}

		// Token: 0x06001619 RID: 5657 RVA: 0x000A4684 File Offset: 0x000A3A84
		public static SqlInt32 operator &(SqlInt32 x, SqlInt32 y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlInt32(x.m_value & y.m_value);
			}
			return SqlInt32.Null;
		}

		// Token: 0x0600161A RID: 5658 RVA: 0x000A46BC File Offset: 0x000A3ABC
		public static SqlInt32 operator |(SqlInt32 x, SqlInt32 y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlInt32(x.m_value | y.m_value);
			}
			return SqlInt32.Null;
		}

		// Token: 0x0600161B RID: 5659 RVA: 0x000A46F4 File Offset: 0x000A3AF4
		public static SqlInt32 operator ^(SqlInt32 x, SqlInt32 y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlInt32(x.m_value ^ y.m_value);
			}
			return SqlInt32.Null;
		}

		// Token: 0x0600161C RID: 5660 RVA: 0x000A472C File Offset: 0x000A3B2C
		public static explicit operator SqlInt32(SqlBoolean x)
		{
			if (!x.IsNull)
			{
				return new SqlInt32((int)x.ByteValue);
			}
			return SqlInt32.Null;
		}

		// Token: 0x0600161D RID: 5661 RVA: 0x000A4754 File Offset: 0x000A3B54
		public static implicit operator SqlInt32(SqlByte x)
		{
			if (!x.IsNull)
			{
				return new SqlInt32((int)x.Value);
			}
			return SqlInt32.Null;
		}

		// Token: 0x0600161E RID: 5662 RVA: 0x000A477C File Offset: 0x000A3B7C
		public static implicit operator SqlInt32(SqlInt16 x)
		{
			if (!x.IsNull)
			{
				return new SqlInt32((int)x.Value);
			}
			return SqlInt32.Null;
		}

		// Token: 0x0600161F RID: 5663 RVA: 0x000A47A4 File Offset: 0x000A3BA4
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

		// Token: 0x06001620 RID: 5664 RVA: 0x000A47EC File Offset: 0x000A3BEC
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

		// Token: 0x06001621 RID: 5665 RVA: 0x000A4834 File Offset: 0x000A3C34
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

		// Token: 0x06001622 RID: 5666 RVA: 0x000A4884 File Offset: 0x000A3C84
		public static explicit operator SqlInt32(SqlMoney x)
		{
			if (!x.IsNull)
			{
				return new SqlInt32(x.ToInt32());
			}
			return SqlInt32.Null;
		}

		// Token: 0x06001623 RID: 5667 RVA: 0x000A48AC File Offset: 0x000A3CAC
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

		// Token: 0x06001624 RID: 5668 RVA: 0x000A491C File Offset: 0x000A3D1C
		public static explicit operator SqlInt32(SqlString x)
		{
			if (!x.IsNull)
			{
				return new SqlInt32(int.Parse(x.Value, null));
			}
			return SqlInt32.Null;
		}

		// Token: 0x06001625 RID: 5669 RVA: 0x000A494C File Offset: 0x000A3D4C
		private static bool SameSignInt(int x, int y)
		{
			return ((long)(x ^ y) & (long)((ulong)int.MinValue)) == 0L;
		}

		// Token: 0x06001626 RID: 5670 RVA: 0x000A4968 File Offset: 0x000A3D68
		public static SqlBoolean operator ==(SqlInt32 x, SqlInt32 y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.m_value == y.m_value);
			}
			return SqlBoolean.Null;
		}

		// Token: 0x06001627 RID: 5671 RVA: 0x000A49A0 File Offset: 0x000A3DA0
		public static SqlBoolean operator !=(SqlInt32 x, SqlInt32 y)
		{
			return !(x == y);
		}

		// Token: 0x06001628 RID: 5672 RVA: 0x000A49BC File Offset: 0x000A3DBC
		public static SqlBoolean operator <(SqlInt32 x, SqlInt32 y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.m_value < y.m_value);
			}
			return SqlBoolean.Null;
		}

		// Token: 0x06001629 RID: 5673 RVA: 0x000A49F4 File Offset: 0x000A3DF4
		public static SqlBoolean operator >(SqlInt32 x, SqlInt32 y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.m_value > y.m_value);
			}
			return SqlBoolean.Null;
		}

		// Token: 0x0600162A RID: 5674 RVA: 0x000A4A2C File Offset: 0x000A3E2C
		public static SqlBoolean operator <=(SqlInt32 x, SqlInt32 y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.m_value <= y.m_value);
			}
			return SqlBoolean.Null;
		}

		// Token: 0x0600162B RID: 5675 RVA: 0x000A4A68 File Offset: 0x000A3E68
		public static SqlBoolean operator >=(SqlInt32 x, SqlInt32 y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.m_value >= y.m_value);
			}
			return SqlBoolean.Null;
		}

		// Token: 0x0600162C RID: 5676 RVA: 0x000A4AA4 File Offset: 0x000A3EA4
		public static SqlInt32 OnesComplement(SqlInt32 x)
		{
			return ~x;
		}

		// Token: 0x0600162D RID: 5677 RVA: 0x000A4AB8 File Offset: 0x000A3EB8
		public static SqlInt32 Add(SqlInt32 x, SqlInt32 y)
		{
			return x + y;
		}

		// Token: 0x0600162E RID: 5678 RVA: 0x000A4ACC File Offset: 0x000A3ECC
		public static SqlInt32 Subtract(SqlInt32 x, SqlInt32 y)
		{
			return x - y;
		}

		// Token: 0x0600162F RID: 5679 RVA: 0x000A4AE0 File Offset: 0x000A3EE0
		public static SqlInt32 Multiply(SqlInt32 x, SqlInt32 y)
		{
			return x * y;
		}

		// Token: 0x06001630 RID: 5680 RVA: 0x000A4AF4 File Offset: 0x000A3EF4
		public static SqlInt32 Divide(SqlInt32 x, SqlInt32 y)
		{
			return x / y;
		}

		// Token: 0x06001631 RID: 5681 RVA: 0x000A4B08 File Offset: 0x000A3F08
		public static SqlInt32 Mod(SqlInt32 x, SqlInt32 y)
		{
			return x % y;
		}

		// Token: 0x06001632 RID: 5682 RVA: 0x000A4B1C File Offset: 0x000A3F1C
		public static SqlInt32 Modulus(SqlInt32 x, SqlInt32 y)
		{
			return x % y;
		}

		// Token: 0x06001633 RID: 5683 RVA: 0x000A4B30 File Offset: 0x000A3F30
		public static SqlInt32 BitwiseAnd(SqlInt32 x, SqlInt32 y)
		{
			return x & y;
		}

		// Token: 0x06001634 RID: 5684 RVA: 0x000A4B44 File Offset: 0x000A3F44
		public static SqlInt32 BitwiseOr(SqlInt32 x, SqlInt32 y)
		{
			return x | y;
		}

		// Token: 0x06001635 RID: 5685 RVA: 0x000A4B58 File Offset: 0x000A3F58
		public static SqlInt32 Xor(SqlInt32 x, SqlInt32 y)
		{
			return x ^ y;
		}

		// Token: 0x06001636 RID: 5686 RVA: 0x000A4B6C File Offset: 0x000A3F6C
		public static SqlBoolean Equals(SqlInt32 x, SqlInt32 y)
		{
			return x == y;
		}

		// Token: 0x06001637 RID: 5687 RVA: 0x000A4B80 File Offset: 0x000A3F80
		public static SqlBoolean NotEquals(SqlInt32 x, SqlInt32 y)
		{
			return x != y;
		}

		// Token: 0x06001638 RID: 5688 RVA: 0x000A4B94 File Offset: 0x000A3F94
		public static SqlBoolean LessThan(SqlInt32 x, SqlInt32 y)
		{
			return x < y;
		}

		// Token: 0x06001639 RID: 5689 RVA: 0x000A4BA8 File Offset: 0x000A3FA8
		public static SqlBoolean GreaterThan(SqlInt32 x, SqlInt32 y)
		{
			return x > y;
		}

		// Token: 0x0600163A RID: 5690 RVA: 0x000A4BBC File Offset: 0x000A3FBC
		public static SqlBoolean LessThanOrEqual(SqlInt32 x, SqlInt32 y)
		{
			return x <= y;
		}

		// Token: 0x0600163B RID: 5691 RVA: 0x000A4BD0 File Offset: 0x000A3FD0
		public static SqlBoolean GreaterThanOrEqual(SqlInt32 x, SqlInt32 y)
		{
			return x >= y;
		}

		// Token: 0x0600163C RID: 5692 RVA: 0x000A4BE4 File Offset: 0x000A3FE4
		public SqlBoolean ToSqlBoolean()
		{
			return (SqlBoolean)this;
		}

		// Token: 0x0600163D RID: 5693 RVA: 0x000A4BFC File Offset: 0x000A3FFC
		public SqlByte ToSqlByte()
		{
			return (SqlByte)this;
		}

		// Token: 0x0600163E RID: 5694 RVA: 0x000A4C14 File Offset: 0x000A4014
		public SqlDouble ToSqlDouble()
		{
			return this;
		}

		// Token: 0x0600163F RID: 5695 RVA: 0x000A4C2C File Offset: 0x000A402C
		public SqlInt16 ToSqlInt16()
		{
			return (SqlInt16)this;
		}

		// Token: 0x06001640 RID: 5696 RVA: 0x000A4C44 File Offset: 0x000A4044
		public SqlInt64 ToSqlInt64()
		{
			return this;
		}

		// Token: 0x06001641 RID: 5697 RVA: 0x000A4C5C File Offset: 0x000A405C
		public SqlMoney ToSqlMoney()
		{
			return this;
		}

		// Token: 0x06001642 RID: 5698 RVA: 0x000A4C74 File Offset: 0x000A4074
		public SqlDecimal ToSqlDecimal()
		{
			return this;
		}

		// Token: 0x06001643 RID: 5699 RVA: 0x000A4C8C File Offset: 0x000A408C
		public SqlSingle ToSqlSingle()
		{
			return this;
		}

		// Token: 0x06001644 RID: 5700 RVA: 0x000A4CA4 File Offset: 0x000A40A4
		public SqlString ToSqlString()
		{
			return (SqlString)this;
		}

		// Token: 0x06001645 RID: 5701 RVA: 0x000A4CBC File Offset: 0x000A40BC
		public int CompareTo(object value)
		{
			if (value is SqlInt32)
			{
				SqlInt32 value2 = (SqlInt32)value;
				return this.CompareTo(value2);
			}
			throw ADP.WrongType(value.GetType(), typeof(SqlInt32));
		}

		// Token: 0x06001646 RID: 5702 RVA: 0x000A4CF8 File Offset: 0x000A40F8
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

		// Token: 0x06001647 RID: 5703 RVA: 0x000A4D50 File Offset: 0x000A4150
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

		// Token: 0x06001648 RID: 5704 RVA: 0x000A4DA8 File Offset: 0x000A41A8
		public override int GetHashCode()
		{
			if (!this.IsNull)
			{
				return this.Value.GetHashCode();
			}
			return 0;
		}

		// Token: 0x06001649 RID: 5705 RVA: 0x000A4DD0 File Offset: 0x000A41D0
		XmlSchema IXmlSerializable.GetSchema()
		{
			return null;
		}

		// Token: 0x0600164A RID: 5706 RVA: 0x000A4DE0 File Offset: 0x000A41E0
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			string attribute = reader.GetAttribute("nil", "http://www.w3.org/2001/XMLSchema-instance");
			if (attribute != null && XmlConvert.ToBoolean(attribute))
			{
				reader.ReadElementString();
				this.m_fNotNull = false;
				return;
			}
			this.m_value = XmlConvert.ToInt32(reader.ReadElementString());
			this.m_fNotNull = true;
		}

		// Token: 0x0600164B RID: 5707 RVA: 0x000A4E30 File Offset: 0x000A4230
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			if (this.IsNull)
			{
				writer.WriteAttributeString("xsi", "nil", "http://www.w3.org/2001/XMLSchema-instance", "true");
				return;
			}
			writer.WriteString(XmlConvert.ToString(this.m_value));
		}

		// Token: 0x0600164C RID: 5708 RVA: 0x000A4E74 File Offset: 0x000A4274
		public static XmlQualifiedName GetXsdType(XmlSchemaSet schemaSet)
		{
			return new XmlQualifiedName("int", "http://www.w3.org/2001/XMLSchema");
		}

		// Token: 0x04000DEF RID: 3567
		private bool m_fNotNull;

		// Token: 0x04000DF0 RID: 3568
		private int m_value;

		// Token: 0x04000DF1 RID: 3569
		private const long x_iIntMin = -2147483648L;

		// Token: 0x04000DF2 RID: 3570
		private const long x_lBitNotIntMax = -2147483648L;

		// Token: 0x04000DF3 RID: 3571
		public static readonly SqlInt32 Null = new SqlInt32(true);

		// Token: 0x04000DF4 RID: 3572
		public static readonly SqlInt32 Zero = new SqlInt32(0);

		// Token: 0x04000DF5 RID: 3573
		public static readonly SqlInt32 MinValue = new SqlInt32(int.MinValue);

		// Token: 0x04000DF6 RID: 3574
		public static readonly SqlInt32 MaxValue = new SqlInt32(int.MaxValue);
	}
}
