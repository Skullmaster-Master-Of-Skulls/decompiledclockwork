using System;
using System.Data.Common;
using System.Globalization;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System.Data.SqlTypes
{
	// Token: 0x02000166 RID: 358
	[XmlSchemaProvider("GetXsdType")]
	[Serializable]
	public struct SqlSingle : INullable, IComparable, IXmlSerializable
	{
		// Token: 0x060016DB RID: 5851 RVA: 0x000A68B0 File Offset: 0x000A5CB0
		private SqlSingle(bool fNull)
		{
			this.m_fNotNull = false;
			this.m_value = 0f;
		}

		// Token: 0x060016DC RID: 5852 RVA: 0x000A68D0 File Offset: 0x000A5CD0
		public SqlSingle(float value)
		{
			if (float.IsInfinity(value) || float.IsNaN(value))
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			this.m_fNotNull = true;
			this.m_value = value;
		}

		// Token: 0x060016DD RID: 5853 RVA: 0x000A6908 File Offset: 0x000A5D08
		public SqlSingle(double value)
		{
			this = new SqlSingle((float)value);
		}

		// Token: 0x17000333 RID: 819
		// (get) Token: 0x060016DE RID: 5854 RVA: 0x000A6920 File Offset: 0x000A5D20
		public bool IsNull
		{
			get
			{
				return !this.m_fNotNull;
			}
		}

		// Token: 0x17000334 RID: 820
		// (get) Token: 0x060016DF RID: 5855 RVA: 0x000A6938 File Offset: 0x000A5D38
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

		// Token: 0x060016E0 RID: 5856 RVA: 0x000A695C File Offset: 0x000A5D5C
		public static implicit operator SqlSingle(float x)
		{
			return new SqlSingle(x);
		}

		// Token: 0x060016E1 RID: 5857 RVA: 0x000A6970 File Offset: 0x000A5D70
		public static explicit operator float(SqlSingle x)
		{
			return x.Value;
		}

		// Token: 0x060016E2 RID: 5858 RVA: 0x000A6984 File Offset: 0x000A5D84
		public override string ToString()
		{
			if (!this.IsNull)
			{
				return this.m_value.ToString(null);
			}
			return SQLResource.NullString;
		}

		// Token: 0x060016E3 RID: 5859 RVA: 0x000A69AC File Offset: 0x000A5DAC
		public static SqlSingle Parse(string s)
		{
			if (s == SQLResource.NullString)
			{
				return SqlSingle.Null;
			}
			return new SqlSingle(float.Parse(s, CultureInfo.InvariantCulture));
		}

		// Token: 0x060016E4 RID: 5860 RVA: 0x000A69DC File Offset: 0x000A5DDC
		public static SqlSingle operator -(SqlSingle x)
		{
			if (!x.IsNull)
			{
				return new SqlSingle(-x.m_value);
			}
			return SqlSingle.Null;
		}

		// Token: 0x060016E5 RID: 5861 RVA: 0x000A6A04 File Offset: 0x000A5E04
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

		// Token: 0x060016E6 RID: 5862 RVA: 0x000A6A50 File Offset: 0x000A5E50
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

		// Token: 0x060016E7 RID: 5863 RVA: 0x000A6A9C File Offset: 0x000A5E9C
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

		// Token: 0x060016E8 RID: 5864 RVA: 0x000A6AE8 File Offset: 0x000A5EE8
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

		// Token: 0x060016E9 RID: 5865 RVA: 0x000A6B4C File Offset: 0x000A5F4C
		public static explicit operator SqlSingle(SqlBoolean x)
		{
			if (!x.IsNull)
			{
				return new SqlSingle((float)x.ByteValue);
			}
			return SqlSingle.Null;
		}

		// Token: 0x060016EA RID: 5866 RVA: 0x000A6B78 File Offset: 0x000A5F78
		public static implicit operator SqlSingle(SqlByte x)
		{
			if (!x.IsNull)
			{
				return new SqlSingle((float)x.Value);
			}
			return SqlSingle.Null;
		}

		// Token: 0x060016EB RID: 5867 RVA: 0x000A6BA4 File Offset: 0x000A5FA4
		public static implicit operator SqlSingle(SqlInt16 x)
		{
			if (!x.IsNull)
			{
				return new SqlSingle((float)x.Value);
			}
			return SqlSingle.Null;
		}

		// Token: 0x060016EC RID: 5868 RVA: 0x000A6BD0 File Offset: 0x000A5FD0
		public static implicit operator SqlSingle(SqlInt32 x)
		{
			if (!x.IsNull)
			{
				return new SqlSingle((float)x.Value);
			}
			return SqlSingle.Null;
		}

		// Token: 0x060016ED RID: 5869 RVA: 0x000A6BFC File Offset: 0x000A5FFC
		public static implicit operator SqlSingle(SqlInt64 x)
		{
			if (!x.IsNull)
			{
				return new SqlSingle((float)x.Value);
			}
			return SqlSingle.Null;
		}

		// Token: 0x060016EE RID: 5870 RVA: 0x000A6C28 File Offset: 0x000A6028
		public static implicit operator SqlSingle(SqlMoney x)
		{
			if (!x.IsNull)
			{
				return new SqlSingle(x.ToDouble());
			}
			return SqlSingle.Null;
		}

		// Token: 0x060016EF RID: 5871 RVA: 0x000A6C50 File Offset: 0x000A6050
		public static implicit operator SqlSingle(SqlDecimal x)
		{
			if (!x.IsNull)
			{
				return new SqlSingle(x.ToDouble());
			}
			return SqlSingle.Null;
		}

		// Token: 0x060016F0 RID: 5872 RVA: 0x000A6C78 File Offset: 0x000A6078
		public static explicit operator SqlSingle(SqlDouble x)
		{
			if (!x.IsNull)
			{
				return new SqlSingle(x.Value);
			}
			return SqlSingle.Null;
		}

		// Token: 0x060016F1 RID: 5873 RVA: 0x000A6CA0 File Offset: 0x000A60A0
		public static explicit operator SqlSingle(SqlString x)
		{
			if (x.IsNull)
			{
				return SqlSingle.Null;
			}
			return SqlSingle.Parse(x.Value);
		}

		// Token: 0x060016F2 RID: 5874 RVA: 0x000A6CC8 File Offset: 0x000A60C8
		public static SqlBoolean operator ==(SqlSingle x, SqlSingle y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.m_value == y.m_value);
			}
			return SqlBoolean.Null;
		}

		// Token: 0x060016F3 RID: 5875 RVA: 0x000A6D00 File Offset: 0x000A6100
		public static SqlBoolean operator !=(SqlSingle x, SqlSingle y)
		{
			return !(x == y);
		}

		// Token: 0x060016F4 RID: 5876 RVA: 0x000A6D1C File Offset: 0x000A611C
		public static SqlBoolean operator <(SqlSingle x, SqlSingle y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.m_value < y.m_value);
			}
			return SqlBoolean.Null;
		}

		// Token: 0x060016F5 RID: 5877 RVA: 0x000A6D54 File Offset: 0x000A6154
		public static SqlBoolean operator >(SqlSingle x, SqlSingle y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.m_value > y.m_value);
			}
			return SqlBoolean.Null;
		}

		// Token: 0x060016F6 RID: 5878 RVA: 0x000A6D8C File Offset: 0x000A618C
		public static SqlBoolean operator <=(SqlSingle x, SqlSingle y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.m_value <= y.m_value);
			}
			return SqlBoolean.Null;
		}

		// Token: 0x060016F7 RID: 5879 RVA: 0x000A6DC8 File Offset: 0x000A61C8
		public static SqlBoolean operator >=(SqlSingle x, SqlSingle y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.m_value >= y.m_value);
			}
			return SqlBoolean.Null;
		}

		// Token: 0x060016F8 RID: 5880 RVA: 0x000A6E04 File Offset: 0x000A6204
		public static SqlSingle Add(SqlSingle x, SqlSingle y)
		{
			return x + y;
		}

		// Token: 0x060016F9 RID: 5881 RVA: 0x000A6E18 File Offset: 0x000A6218
		public static SqlSingle Subtract(SqlSingle x, SqlSingle y)
		{
			return x - y;
		}

		// Token: 0x060016FA RID: 5882 RVA: 0x000A6E2C File Offset: 0x000A622C
		public static SqlSingle Multiply(SqlSingle x, SqlSingle y)
		{
			return x * y;
		}

		// Token: 0x060016FB RID: 5883 RVA: 0x000A6E40 File Offset: 0x000A6240
		public static SqlSingle Divide(SqlSingle x, SqlSingle y)
		{
			return x / y;
		}

		// Token: 0x060016FC RID: 5884 RVA: 0x000A6E54 File Offset: 0x000A6254
		public static SqlBoolean Equals(SqlSingle x, SqlSingle y)
		{
			return x == y;
		}

		// Token: 0x060016FD RID: 5885 RVA: 0x000A6E68 File Offset: 0x000A6268
		public static SqlBoolean NotEquals(SqlSingle x, SqlSingle y)
		{
			return x != y;
		}

		// Token: 0x060016FE RID: 5886 RVA: 0x000A6E7C File Offset: 0x000A627C
		public static SqlBoolean LessThan(SqlSingle x, SqlSingle y)
		{
			return x < y;
		}

		// Token: 0x060016FF RID: 5887 RVA: 0x000A6E90 File Offset: 0x000A6290
		public static SqlBoolean GreaterThan(SqlSingle x, SqlSingle y)
		{
			return x > y;
		}

		// Token: 0x06001700 RID: 5888 RVA: 0x000A6EA4 File Offset: 0x000A62A4
		public static SqlBoolean LessThanOrEqual(SqlSingle x, SqlSingle y)
		{
			return x <= y;
		}

		// Token: 0x06001701 RID: 5889 RVA: 0x000A6EB8 File Offset: 0x000A62B8
		public static SqlBoolean GreaterThanOrEqual(SqlSingle x, SqlSingle y)
		{
			return x >= y;
		}

		// Token: 0x06001702 RID: 5890 RVA: 0x000A6ECC File Offset: 0x000A62CC
		public SqlBoolean ToSqlBoolean()
		{
			return (SqlBoolean)this;
		}

		// Token: 0x06001703 RID: 5891 RVA: 0x000A6EE4 File Offset: 0x000A62E4
		public SqlByte ToSqlByte()
		{
			return (SqlByte)this;
		}

		// Token: 0x06001704 RID: 5892 RVA: 0x000A6EFC File Offset: 0x000A62FC
		public SqlDouble ToSqlDouble()
		{
			return this;
		}

		// Token: 0x06001705 RID: 5893 RVA: 0x000A6F14 File Offset: 0x000A6314
		public SqlInt16 ToSqlInt16()
		{
			return (SqlInt16)this;
		}

		// Token: 0x06001706 RID: 5894 RVA: 0x000A6F2C File Offset: 0x000A632C
		public SqlInt32 ToSqlInt32()
		{
			return (SqlInt32)this;
		}

		// Token: 0x06001707 RID: 5895 RVA: 0x000A6F44 File Offset: 0x000A6344
		public SqlInt64 ToSqlInt64()
		{
			return (SqlInt64)this;
		}

		// Token: 0x06001708 RID: 5896 RVA: 0x000A6F5C File Offset: 0x000A635C
		public SqlMoney ToSqlMoney()
		{
			return (SqlMoney)this;
		}

		// Token: 0x06001709 RID: 5897 RVA: 0x000A6F74 File Offset: 0x000A6374
		public SqlDecimal ToSqlDecimal()
		{
			return (SqlDecimal)this;
		}

		// Token: 0x0600170A RID: 5898 RVA: 0x000A6F8C File Offset: 0x000A638C
		public SqlString ToSqlString()
		{
			return (SqlString)this;
		}

		// Token: 0x0600170B RID: 5899 RVA: 0x000A6FA4 File Offset: 0x000A63A4
		public int CompareTo(object value)
		{
			if (value is SqlSingle)
			{
				SqlSingle value2 = (SqlSingle)value;
				return this.CompareTo(value2);
			}
			throw ADP.WrongType(value.GetType(), typeof(SqlSingle));
		}

		// Token: 0x0600170C RID: 5900 RVA: 0x000A6FE0 File Offset: 0x000A63E0
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

		// Token: 0x0600170D RID: 5901 RVA: 0x000A7038 File Offset: 0x000A6438
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

		// Token: 0x0600170E RID: 5902 RVA: 0x000A7090 File Offset: 0x000A6490
		public override int GetHashCode()
		{
			if (!this.IsNull)
			{
				return this.Value.GetHashCode();
			}
			return 0;
		}

		// Token: 0x0600170F RID: 5903 RVA: 0x000A70B8 File Offset: 0x000A64B8
		XmlSchema IXmlSerializable.GetSchema()
		{
			return null;
		}

		// Token: 0x06001710 RID: 5904 RVA: 0x000A70C8 File Offset: 0x000A64C8
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			string attribute = reader.GetAttribute("nil", "http://www.w3.org/2001/XMLSchema-instance");
			if (attribute != null && XmlConvert.ToBoolean(attribute))
			{
				reader.ReadElementString();
				this.m_fNotNull = false;
				return;
			}
			this.m_value = XmlConvert.ToSingle(reader.ReadElementString());
			this.m_fNotNull = true;
		}

		// Token: 0x06001711 RID: 5905 RVA: 0x000A7118 File Offset: 0x000A6518
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			if (this.IsNull)
			{
				writer.WriteAttributeString("xsi", "nil", "http://www.w3.org/2001/XMLSchema-instance", "true");
				return;
			}
			writer.WriteString(XmlConvert.ToString(this.m_value));
		}

		// Token: 0x06001712 RID: 5906 RVA: 0x000A715C File Offset: 0x000A655C
		public static XmlQualifiedName GetXsdType(XmlSchemaSet schemaSet)
		{
			return new XmlQualifiedName("float", "http://www.w3.org/2001/XMLSchema");
		}

		// Token: 0x04000E1E RID: 3614
		private bool m_fNotNull;

		// Token: 0x04000E1F RID: 3615
		private float m_value;

		// Token: 0x04000E20 RID: 3616
		public static readonly SqlSingle Null = new SqlSingle(true);

		// Token: 0x04000E21 RID: 3617
		public static readonly SqlSingle Zero = new SqlSingle(0f);

		// Token: 0x04000E22 RID: 3618
		public static readonly SqlSingle MinValue = new SqlSingle(float.MinValue);

		// Token: 0x04000E23 RID: 3619
		public static readonly SqlSingle MaxValue = new SqlSingle(float.MaxValue);
	}
}
