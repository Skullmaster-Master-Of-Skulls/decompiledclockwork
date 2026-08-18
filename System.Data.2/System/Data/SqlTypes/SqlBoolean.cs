using System;
using System.Data.Common;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System.Data.SqlTypes
{
	// Token: 0x02000151 RID: 337
	[XmlSchemaProvider("GetXsdType")]
	[Serializable]
	public struct SqlBoolean : INullable, IComparable, IXmlSerializable
	{
		// Token: 0x060013A0 RID: 5024 RVA: 0x0009AE90 File Offset: 0x0009A290
		public SqlBoolean(bool value)
		{
			this.m_value = (value ? 2 : 1);
		}

		// Token: 0x060013A1 RID: 5025 RVA: 0x0009AEAC File Offset: 0x0009A2AC
		public SqlBoolean(int value)
		{
			this = new SqlBoolean(value, false);
		}

		// Token: 0x060013A2 RID: 5026 RVA: 0x0009AEC4 File Offset: 0x0009A2C4
		private SqlBoolean(int value, bool fNull)
		{
			if (fNull)
			{
				this.m_value = 0;
				return;
			}
			this.m_value = ((value != 0) ? 2 : 1);
		}

		// Token: 0x170002E6 RID: 742
		// (get) Token: 0x060013A3 RID: 5027 RVA: 0x0009AEEC File Offset: 0x0009A2EC
		public bool IsNull
		{
			get
			{
				return this.m_value == 0;
			}
		}

		// Token: 0x170002E7 RID: 743
		// (get) Token: 0x060013A4 RID: 5028 RVA: 0x0009AF04 File Offset: 0x0009A304
		public bool Value
		{
			get
			{
				byte value = this.m_value;
				if (value == 1)
				{
					return false;
				}
				if (value == 2)
				{
					return true;
				}
				throw new SqlNullValueException();
			}
		}

		// Token: 0x170002E8 RID: 744
		// (get) Token: 0x060013A5 RID: 5029 RVA: 0x0009AF2C File Offset: 0x0009A32C
		public bool IsTrue
		{
			get
			{
				return this.m_value == 2;
			}
		}

		// Token: 0x170002E9 RID: 745
		// (get) Token: 0x060013A6 RID: 5030 RVA: 0x0009AF44 File Offset: 0x0009A344
		public bool IsFalse
		{
			get
			{
				return this.m_value == 1;
			}
		}

		// Token: 0x060013A7 RID: 5031 RVA: 0x0009AF5C File Offset: 0x0009A35C
		public static implicit operator SqlBoolean(bool x)
		{
			return new SqlBoolean(x);
		}

		// Token: 0x060013A8 RID: 5032 RVA: 0x0009AF70 File Offset: 0x0009A370
		public static explicit operator bool(SqlBoolean x)
		{
			return x.Value;
		}

		// Token: 0x060013A9 RID: 5033 RVA: 0x0009AF84 File Offset: 0x0009A384
		public static SqlBoolean operator !(SqlBoolean x)
		{
			byte value = x.m_value;
			if (value == 1)
			{
				return SqlBoolean.True;
			}
			if (value == 2)
			{
				return SqlBoolean.False;
			}
			return SqlBoolean.Null;
		}

		// Token: 0x060013AA RID: 5034 RVA: 0x0009AFB4 File Offset: 0x0009A3B4
		public static bool operator true(SqlBoolean x)
		{
			return x.IsTrue;
		}

		// Token: 0x060013AB RID: 5035 RVA: 0x0009AFC8 File Offset: 0x0009A3C8
		public static bool operator false(SqlBoolean x)
		{
			return x.IsFalse;
		}

		// Token: 0x060013AC RID: 5036 RVA: 0x0009AFDC File Offset: 0x0009A3DC
		public static SqlBoolean operator &(SqlBoolean x, SqlBoolean y)
		{
			if (x.m_value == 1 || y.m_value == 1)
			{
				return SqlBoolean.False;
			}
			if (x.m_value == 2 && y.m_value == 2)
			{
				return SqlBoolean.True;
			}
			return SqlBoolean.Null;
		}

		// Token: 0x060013AD RID: 5037 RVA: 0x0009B020 File Offset: 0x0009A420
		public static SqlBoolean operator |(SqlBoolean x, SqlBoolean y)
		{
			if (x.m_value == 2 || y.m_value == 2)
			{
				return SqlBoolean.True;
			}
			if (x.m_value == 1 && y.m_value == 1)
			{
				return SqlBoolean.False;
			}
			return SqlBoolean.Null;
		}

		// Token: 0x170002EA RID: 746
		// (get) Token: 0x060013AE RID: 5038 RVA: 0x0009B064 File Offset: 0x0009A464
		public byte ByteValue
		{
			get
			{
				if (this.IsNull)
				{
					throw new SqlNullValueException();
				}
				if (this.m_value != 2)
				{
					return 0;
				}
				return 1;
			}
		}

		// Token: 0x060013AF RID: 5039 RVA: 0x0009B08C File Offset: 0x0009A48C
		public override string ToString()
		{
			if (!this.IsNull)
			{
				return this.Value.ToString(null);
			}
			return SQLResource.NullString;
		}

		// Token: 0x060013B0 RID: 5040 RVA: 0x0009B0B8 File Offset: 0x0009A4B8
		public static SqlBoolean Parse(string s)
		{
			if (s == null)
			{
				return new SqlBoolean(bool.Parse(s));
			}
			if (s == SQLResource.NullString)
			{
				return SqlBoolean.Null;
			}
			s = s.TrimStart(new char[0]);
			char c = s[0];
			if (char.IsNumber(c) || '-' == c || '+' == c)
			{
				return new SqlBoolean(int.Parse(s, null));
			}
			return new SqlBoolean(bool.Parse(s));
		}

		// Token: 0x060013B1 RID: 5041 RVA: 0x0009B128 File Offset: 0x0009A528
		public static SqlBoolean operator ~(SqlBoolean x)
		{
			return !x;
		}

		// Token: 0x060013B2 RID: 5042 RVA: 0x0009B13C File Offset: 0x0009A53C
		public static SqlBoolean operator ^(SqlBoolean x, SqlBoolean y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.m_value != y.m_value);
			}
			return SqlBoolean.Null;
		}

		// Token: 0x060013B3 RID: 5043 RVA: 0x0009B178 File Offset: 0x0009A578
		public static explicit operator SqlBoolean(SqlByte x)
		{
			if (!x.IsNull)
			{
				return new SqlBoolean(x.Value > 0);
			}
			return SqlBoolean.Null;
		}

		// Token: 0x060013B4 RID: 5044 RVA: 0x0009B1A4 File Offset: 0x0009A5A4
		public static explicit operator SqlBoolean(SqlInt16 x)
		{
			if (!x.IsNull)
			{
				return new SqlBoolean(x.Value != 0);
			}
			return SqlBoolean.Null;
		}

		// Token: 0x060013B5 RID: 5045 RVA: 0x0009B1D0 File Offset: 0x0009A5D0
		public static explicit operator SqlBoolean(SqlInt32 x)
		{
			if (!x.IsNull)
			{
				return new SqlBoolean(x.Value != 0);
			}
			return SqlBoolean.Null;
		}

		// Token: 0x060013B6 RID: 5046 RVA: 0x0009B1FC File Offset: 0x0009A5FC
		public static explicit operator SqlBoolean(SqlInt64 x)
		{
			if (!x.IsNull)
			{
				return new SqlBoolean(x.Value != 0L);
			}
			return SqlBoolean.Null;
		}

		// Token: 0x060013B7 RID: 5047 RVA: 0x0009B228 File Offset: 0x0009A628
		public static explicit operator SqlBoolean(SqlDouble x)
		{
			if (!x.IsNull)
			{
				return new SqlBoolean(x.Value != 0.0);
			}
			return SqlBoolean.Null;
		}

		// Token: 0x060013B8 RID: 5048 RVA: 0x0009B260 File Offset: 0x0009A660
		public static explicit operator SqlBoolean(SqlSingle x)
		{
			if (!x.IsNull)
			{
				return new SqlBoolean((double)x.Value != 0.0);
			}
			return SqlBoolean.Null;
		}

		// Token: 0x060013B9 RID: 5049 RVA: 0x0009B298 File Offset: 0x0009A698
		public static explicit operator SqlBoolean(SqlMoney x)
		{
			if (!x.IsNull)
			{
				return x != SqlMoney.Zero;
			}
			return SqlBoolean.Null;
		}

		// Token: 0x060013BA RID: 5050 RVA: 0x0009B2C0 File Offset: 0x0009A6C0
		public static explicit operator SqlBoolean(SqlDecimal x)
		{
			if (!x.IsNull)
			{
				return new SqlBoolean(x.m_data1 != 0U || x.m_data2 != 0U || x.m_data3 != 0U || x.m_data4 > 0U);
			}
			return SqlBoolean.Null;
		}

		// Token: 0x060013BB RID: 5051 RVA: 0x0009B308 File Offset: 0x0009A708
		public static explicit operator SqlBoolean(SqlString x)
		{
			if (!x.IsNull)
			{
				return SqlBoolean.Parse(x.Value);
			}
			return SqlBoolean.Null;
		}

		// Token: 0x060013BC RID: 5052 RVA: 0x0009B330 File Offset: 0x0009A730
		public static SqlBoolean operator ==(SqlBoolean x, SqlBoolean y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.m_value == y.m_value);
			}
			return SqlBoolean.Null;
		}

		// Token: 0x060013BD RID: 5053 RVA: 0x0009B368 File Offset: 0x0009A768
		public static SqlBoolean operator !=(SqlBoolean x, SqlBoolean y)
		{
			return !(x == y);
		}

		// Token: 0x060013BE RID: 5054 RVA: 0x0009B384 File Offset: 0x0009A784
		public static SqlBoolean operator <(SqlBoolean x, SqlBoolean y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.m_value < y.m_value);
			}
			return SqlBoolean.Null;
		}

		// Token: 0x060013BF RID: 5055 RVA: 0x0009B3BC File Offset: 0x0009A7BC
		public static SqlBoolean operator >(SqlBoolean x, SqlBoolean y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.m_value > y.m_value);
			}
			return SqlBoolean.Null;
		}

		// Token: 0x060013C0 RID: 5056 RVA: 0x0009B3F4 File Offset: 0x0009A7F4
		public static SqlBoolean operator <=(SqlBoolean x, SqlBoolean y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.m_value <= y.m_value);
			}
			return SqlBoolean.Null;
		}

		// Token: 0x060013C1 RID: 5057 RVA: 0x0009B430 File Offset: 0x0009A830
		public static SqlBoolean operator >=(SqlBoolean x, SqlBoolean y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.m_value >= y.m_value);
			}
			return SqlBoolean.Null;
		}

		// Token: 0x060013C2 RID: 5058 RVA: 0x0009B46C File Offset: 0x0009A86C
		public static SqlBoolean OnesComplement(SqlBoolean x)
		{
			return ~x;
		}

		// Token: 0x060013C3 RID: 5059 RVA: 0x0009B480 File Offset: 0x0009A880
		public static SqlBoolean And(SqlBoolean x, SqlBoolean y)
		{
			return x & y;
		}

		// Token: 0x060013C4 RID: 5060 RVA: 0x0009B494 File Offset: 0x0009A894
		public static SqlBoolean Or(SqlBoolean x, SqlBoolean y)
		{
			return x | y;
		}

		// Token: 0x060013C5 RID: 5061 RVA: 0x0009B4A8 File Offset: 0x0009A8A8
		public static SqlBoolean Xor(SqlBoolean x, SqlBoolean y)
		{
			return x ^ y;
		}

		// Token: 0x060013C6 RID: 5062 RVA: 0x0009B4BC File Offset: 0x0009A8BC
		public static SqlBoolean Equals(SqlBoolean x, SqlBoolean y)
		{
			return x == y;
		}

		// Token: 0x060013C7 RID: 5063 RVA: 0x0009B4D0 File Offset: 0x0009A8D0
		public static SqlBoolean NotEquals(SqlBoolean x, SqlBoolean y)
		{
			return x != y;
		}

		// Token: 0x060013C8 RID: 5064 RVA: 0x0009B4E4 File Offset: 0x0009A8E4
		public static SqlBoolean GreaterThan(SqlBoolean x, SqlBoolean y)
		{
			return x > y;
		}

		// Token: 0x060013C9 RID: 5065 RVA: 0x0009B4F8 File Offset: 0x0009A8F8
		public static SqlBoolean LessThan(SqlBoolean x, SqlBoolean y)
		{
			return x < y;
		}

		// Token: 0x060013CA RID: 5066 RVA: 0x0009B50C File Offset: 0x0009A90C
		public static SqlBoolean GreaterThanOrEquals(SqlBoolean x, SqlBoolean y)
		{
			return x >= y;
		}

		// Token: 0x060013CB RID: 5067 RVA: 0x0009B520 File Offset: 0x0009A920
		public static SqlBoolean LessThanOrEquals(SqlBoolean x, SqlBoolean y)
		{
			return x <= y;
		}

		// Token: 0x060013CC RID: 5068 RVA: 0x0009B534 File Offset: 0x0009A934
		public SqlByte ToSqlByte()
		{
			return (SqlByte)this;
		}

		// Token: 0x060013CD RID: 5069 RVA: 0x0009B54C File Offset: 0x0009A94C
		public SqlDouble ToSqlDouble()
		{
			return (SqlDouble)this;
		}

		// Token: 0x060013CE RID: 5070 RVA: 0x0009B564 File Offset: 0x0009A964
		public SqlInt16 ToSqlInt16()
		{
			return (SqlInt16)this;
		}

		// Token: 0x060013CF RID: 5071 RVA: 0x0009B57C File Offset: 0x0009A97C
		public SqlInt32 ToSqlInt32()
		{
			return (SqlInt32)this;
		}

		// Token: 0x060013D0 RID: 5072 RVA: 0x0009B594 File Offset: 0x0009A994
		public SqlInt64 ToSqlInt64()
		{
			return (SqlInt64)this;
		}

		// Token: 0x060013D1 RID: 5073 RVA: 0x0009B5AC File Offset: 0x0009A9AC
		public SqlMoney ToSqlMoney()
		{
			return (SqlMoney)this;
		}

		// Token: 0x060013D2 RID: 5074 RVA: 0x0009B5C4 File Offset: 0x0009A9C4
		public SqlDecimal ToSqlDecimal()
		{
			return (SqlDecimal)this;
		}

		// Token: 0x060013D3 RID: 5075 RVA: 0x0009B5DC File Offset: 0x0009A9DC
		public SqlSingle ToSqlSingle()
		{
			return (SqlSingle)this;
		}

		// Token: 0x060013D4 RID: 5076 RVA: 0x0009B5F4 File Offset: 0x0009A9F4
		public SqlString ToSqlString()
		{
			return (SqlString)this;
		}

		// Token: 0x060013D5 RID: 5077 RVA: 0x0009B60C File Offset: 0x0009AA0C
		public int CompareTo(object value)
		{
			if (value is SqlBoolean)
			{
				SqlBoolean value2 = (SqlBoolean)value;
				return this.CompareTo(value2);
			}
			throw ADP.WrongType(value.GetType(), typeof(SqlBoolean));
		}

		// Token: 0x060013D6 RID: 5078 RVA: 0x0009B648 File Offset: 0x0009AA48
		public int CompareTo(SqlBoolean value)
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
				if (this.ByteValue < value.ByteValue)
				{
					return -1;
				}
				if (this.ByteValue > value.ByteValue)
				{
					return 1;
				}
				return 0;
			}
		}

		// Token: 0x060013D7 RID: 5079 RVA: 0x0009B698 File Offset: 0x0009AA98
		public override bool Equals(object value)
		{
			if (!(value is SqlBoolean))
			{
				return false;
			}
			SqlBoolean y = (SqlBoolean)value;
			if (y.IsNull || this.IsNull)
			{
				return y.IsNull && this.IsNull;
			}
			return (this == y).Value;
		}

		// Token: 0x060013D8 RID: 5080 RVA: 0x0009B6F0 File Offset: 0x0009AAF0
		public override int GetHashCode()
		{
			if (!this.IsNull)
			{
				return this.Value.GetHashCode();
			}
			return 0;
		}

		// Token: 0x060013D9 RID: 5081 RVA: 0x0009B718 File Offset: 0x0009AB18
		XmlSchema IXmlSerializable.GetSchema()
		{
			return null;
		}

		// Token: 0x060013DA RID: 5082 RVA: 0x0009B728 File Offset: 0x0009AB28
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			string attribute = reader.GetAttribute("nil", "http://www.w3.org/2001/XMLSchema-instance");
			if (attribute != null && XmlConvert.ToBoolean(attribute))
			{
				reader.ReadElementString();
				this.m_value = 0;
				return;
			}
			this.m_value = (XmlConvert.ToBoolean(reader.ReadElementString()) ? 2 : 1);
		}

		// Token: 0x060013DB RID: 5083 RVA: 0x0009B778 File Offset: 0x0009AB78
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			if (this.IsNull)
			{
				writer.WriteAttributeString("xsi", "nil", "http://www.w3.org/2001/XMLSchema-instance", "true");
				return;
			}
			writer.WriteString((this.m_value == 2) ? "true" : "false");
		}

		// Token: 0x060013DC RID: 5084 RVA: 0x0009B7C4 File Offset: 0x0009ABC4
		public static XmlQualifiedName GetXsdType(XmlSchemaSet schemaSet)
		{
			return new XmlQualifiedName("boolean", "http://www.w3.org/2001/XMLSchema");
		}

		// Token: 0x04000D4C RID: 3404
		private byte m_value;

		// Token: 0x04000D4D RID: 3405
		private const byte x_Null = 0;

		// Token: 0x04000D4E RID: 3406
		private const byte x_False = 1;

		// Token: 0x04000D4F RID: 3407
		private const byte x_True = 2;

		// Token: 0x04000D50 RID: 3408
		public static readonly SqlBoolean True = new SqlBoolean(true);

		// Token: 0x04000D51 RID: 3409
		public static readonly SqlBoolean False = new SqlBoolean(false);

		// Token: 0x04000D52 RID: 3410
		public static readonly SqlBoolean Null = new SqlBoolean(0, true);

		// Token: 0x04000D53 RID: 3411
		public static readonly SqlBoolean Zero = new SqlBoolean(0);

		// Token: 0x04000D54 RID: 3412
		public static readonly SqlBoolean One = new SqlBoolean(1);
	}
}
