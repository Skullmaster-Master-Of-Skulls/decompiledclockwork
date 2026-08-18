using System;
using System.Data.Common;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System.Data.SqlTypes
{
	// Token: 0x02000341 RID: 833
	[XmlSchemaProvider("GetXsdType")]
	[Serializable]
	public struct SqlBoolean : INullable, IComparable, IXmlSerializable
	{
		// Token: 0x06002B5E RID: 11102 RVA: 0x002C4288 File Offset: 0x002C3688
		public SqlBoolean(bool value)
		{
			this.m_value = (value ? 2 : 1);
		}

		// Token: 0x06002B5F RID: 11103 RVA: 0x002C42A8 File Offset: 0x002C36A8
		public SqlBoolean(int value)
		{
			this = new SqlBoolean(value, false);
		}

		// Token: 0x06002B60 RID: 11104 RVA: 0x002C42C8 File Offset: 0x002C36C8
		private SqlBoolean(int value, bool fNull)
		{
			if (fNull)
			{
				this.m_value = 0;
				return;
			}
			this.m_value = ((value != 0) ? 2 : 1);
		}

		// Token: 0x17000714 RID: 1812
		// (get) Token: 0x06002B61 RID: 11105 RVA: 0x002C42F8 File Offset: 0x002C36F8
		public bool IsNull
		{
			get
			{
				return this.m_value == 0;
			}
		}

		// Token: 0x17000715 RID: 1813
		// (get) Token: 0x06002B62 RID: 11106 RVA: 0x002C4318 File Offset: 0x002C3718
		public bool Value
		{
			get
			{
				switch (this.m_value)
				{
				case 1:
					return false;
				case 2:
					return true;
				default:
					throw new SqlNullValueException();
				}
			}
		}

		// Token: 0x17000716 RID: 1814
		// (get) Token: 0x06002B63 RID: 11107 RVA: 0x002C4348 File Offset: 0x002C3748
		public bool IsTrue
		{
			get
			{
				return this.m_value == 2;
			}
		}

		// Token: 0x17000717 RID: 1815
		// (get) Token: 0x06002B64 RID: 11108 RVA: 0x002C4368 File Offset: 0x002C3768
		public bool IsFalse
		{
			get
			{
				return this.m_value == 1;
			}
		}

		// Token: 0x06002B65 RID: 11109 RVA: 0x002C4388 File Offset: 0x002C3788
		public static implicit operator SqlBoolean(bool x)
		{
			return new SqlBoolean(x);
		}

		// Token: 0x06002B66 RID: 11110 RVA: 0x002C43A8 File Offset: 0x002C37A8
		public static explicit operator bool(SqlBoolean x)
		{
			return x.Value;
		}

		// Token: 0x06002B67 RID: 11111 RVA: 0x002C43C8 File Offset: 0x002C37C8
		public static SqlBoolean operator !(SqlBoolean x)
		{
			switch (x.m_value)
			{
			case 1:
				return SqlBoolean.True;
			case 2:
				return SqlBoolean.False;
			default:
				return SqlBoolean.Null;
			}
		}

		// Token: 0x06002B68 RID: 11112 RVA: 0x002C4408 File Offset: 0x002C3808
		public static bool operator true(SqlBoolean x)
		{
			return x.IsTrue;
		}

		// Token: 0x06002B69 RID: 11113 RVA: 0x002C4428 File Offset: 0x002C3828
		public static bool operator false(SqlBoolean x)
		{
			return x.IsFalse;
		}

		// Token: 0x06002B6A RID: 11114 RVA: 0x002C4448 File Offset: 0x002C3848
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

		// Token: 0x06002B6B RID: 11115 RVA: 0x002C4498 File Offset: 0x002C3898
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

		// Token: 0x17000718 RID: 1816
		// (get) Token: 0x06002B6C RID: 11116 RVA: 0x002C44E8 File Offset: 0x002C38E8
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

		// Token: 0x06002B6D RID: 11117 RVA: 0x002C4518 File Offset: 0x002C3918
		public override string ToString()
		{
			if (!this.IsNull)
			{
				return this.Value.ToString(null);
			}
			return SQLResource.NullString;
		}

		// Token: 0x06002B6E RID: 11118 RVA: 0x002C4548 File Offset: 0x002C3948
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

		// Token: 0x06002B6F RID: 11119 RVA: 0x002C45B8 File Offset: 0x002C39B8
		public static SqlBoolean operator ~(SqlBoolean x)
		{
			return !x;
		}

		// Token: 0x06002B70 RID: 11120 RVA: 0x002C45D8 File Offset: 0x002C39D8
		public static SqlBoolean operator ^(SqlBoolean x, SqlBoolean y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.m_value != y.m_value);
			}
			return SqlBoolean.Null;
		}

		// Token: 0x06002B71 RID: 11121 RVA: 0x002C4618 File Offset: 0x002C3A18
		public static explicit operator SqlBoolean(SqlByte x)
		{
			if (!x.IsNull)
			{
				return new SqlBoolean(x.Value != 0);
			}
			return SqlBoolean.Null;
		}

		// Token: 0x06002B72 RID: 11122 RVA: 0x002C4648 File Offset: 0x002C3A48
		public static explicit operator SqlBoolean(SqlInt16 x)
		{
			if (!x.IsNull)
			{
				return new SqlBoolean(x.Value != 0);
			}
			return SqlBoolean.Null;
		}

		// Token: 0x06002B73 RID: 11123 RVA: 0x002C4678 File Offset: 0x002C3A78
		public static explicit operator SqlBoolean(SqlInt32 x)
		{
			if (!x.IsNull)
			{
				return new SqlBoolean(x.Value != 0);
			}
			return SqlBoolean.Null;
		}

		// Token: 0x06002B74 RID: 11124 RVA: 0x002C46A8 File Offset: 0x002C3AA8
		public static explicit operator SqlBoolean(SqlInt64 x)
		{
			if (!x.IsNull)
			{
				return new SqlBoolean(x.Value != 0L);
			}
			return SqlBoolean.Null;
		}

		// Token: 0x06002B75 RID: 11125 RVA: 0x002C46D8 File Offset: 0x002C3AD8
		public static explicit operator SqlBoolean(SqlDouble x)
		{
			if (!x.IsNull)
			{
				return new SqlBoolean(x.Value != 0.0);
			}
			return SqlBoolean.Null;
		}

		// Token: 0x06002B76 RID: 11126 RVA: 0x002C4718 File Offset: 0x002C3B18
		public static explicit operator SqlBoolean(SqlSingle x)
		{
			if (!x.IsNull)
			{
				return new SqlBoolean((double)x.Value != 0.0);
			}
			return SqlBoolean.Null;
		}

		// Token: 0x06002B77 RID: 11127 RVA: 0x002C4758 File Offset: 0x002C3B58
		public static explicit operator SqlBoolean(SqlMoney x)
		{
			if (!x.IsNull)
			{
				return x != SqlMoney.Zero;
			}
			return SqlBoolean.Null;
		}

		// Token: 0x06002B78 RID: 11128 RVA: 0x002C4788 File Offset: 0x002C3B88
		public static explicit operator SqlBoolean(SqlDecimal x)
		{
			if (!x.IsNull)
			{
				return new SqlBoolean(x.m_data1 != 0U || x.m_data2 != 0U || x.m_data3 != 0U || x.m_data4 != 0U);
			}
			return SqlBoolean.Null;
		}

		// Token: 0x06002B79 RID: 11129 RVA: 0x002C47D8 File Offset: 0x002C3BD8
		public static explicit operator SqlBoolean(SqlString x)
		{
			if (!x.IsNull)
			{
				return SqlBoolean.Parse(x.Value);
			}
			return SqlBoolean.Null;
		}

		// Token: 0x06002B7A RID: 11130 RVA: 0x002C4808 File Offset: 0x002C3C08
		public static SqlBoolean operator ==(SqlBoolean x, SqlBoolean y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.m_value == y.m_value);
			}
			return SqlBoolean.Null;
		}

		// Token: 0x06002B7B RID: 11131 RVA: 0x002C4848 File Offset: 0x002C3C48
		public static SqlBoolean operator !=(SqlBoolean x, SqlBoolean y)
		{
			return !(x == y);
		}

		// Token: 0x06002B7C RID: 11132 RVA: 0x002C4868 File Offset: 0x002C3C68
		public static SqlBoolean operator <(SqlBoolean x, SqlBoolean y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.m_value < y.m_value);
			}
			return SqlBoolean.Null;
		}

		// Token: 0x06002B7D RID: 11133 RVA: 0x002C48A8 File Offset: 0x002C3CA8
		public static SqlBoolean operator >(SqlBoolean x, SqlBoolean y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.m_value > y.m_value);
			}
			return SqlBoolean.Null;
		}

		// Token: 0x06002B7E RID: 11134 RVA: 0x002C48E8 File Offset: 0x002C3CE8
		public static SqlBoolean operator <=(SqlBoolean x, SqlBoolean y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.m_value <= y.m_value);
			}
			return SqlBoolean.Null;
		}

		// Token: 0x06002B7F RID: 11135 RVA: 0x002C4928 File Offset: 0x002C3D28
		public static SqlBoolean operator >=(SqlBoolean x, SqlBoolean y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.m_value >= y.m_value);
			}
			return SqlBoolean.Null;
		}

		// Token: 0x06002B80 RID: 11136 RVA: 0x002C4968 File Offset: 0x002C3D68
		public static SqlBoolean OnesComplement(SqlBoolean x)
		{
			return ~x;
		}

		// Token: 0x06002B81 RID: 11137 RVA: 0x002C4988 File Offset: 0x002C3D88
		public static SqlBoolean And(SqlBoolean x, SqlBoolean y)
		{
			return x & y;
		}

		// Token: 0x06002B82 RID: 11138 RVA: 0x002C49A8 File Offset: 0x002C3DA8
		public static SqlBoolean Or(SqlBoolean x, SqlBoolean y)
		{
			return x | y;
		}

		// Token: 0x06002B83 RID: 11139 RVA: 0x002C49C8 File Offset: 0x002C3DC8
		public static SqlBoolean Xor(SqlBoolean x, SqlBoolean y)
		{
			return x ^ y;
		}

		// Token: 0x06002B84 RID: 11140 RVA: 0x002C49E8 File Offset: 0x002C3DE8
		public static SqlBoolean Equals(SqlBoolean x, SqlBoolean y)
		{
			return x == y;
		}

		// Token: 0x06002B85 RID: 11141 RVA: 0x002C4A08 File Offset: 0x002C3E08
		public static SqlBoolean NotEquals(SqlBoolean x, SqlBoolean y)
		{
			return x != y;
		}

		// Token: 0x06002B86 RID: 11142 RVA: 0x002C4A28 File Offset: 0x002C3E28
		public static SqlBoolean GreaterThan(SqlBoolean x, SqlBoolean y)
		{
			return x > y;
		}

		// Token: 0x06002B87 RID: 11143 RVA: 0x002C4A48 File Offset: 0x002C3E48
		public static SqlBoolean LessThan(SqlBoolean x, SqlBoolean y)
		{
			return x < y;
		}

		// Token: 0x06002B88 RID: 11144 RVA: 0x002C4A68 File Offset: 0x002C3E68
		public static SqlBoolean GreaterThanOrEquals(SqlBoolean x, SqlBoolean y)
		{
			return x >= y;
		}

		// Token: 0x06002B89 RID: 11145 RVA: 0x002C4A88 File Offset: 0x002C3E88
		public static SqlBoolean LessThanOrEquals(SqlBoolean x, SqlBoolean y)
		{
			return x <= y;
		}

		// Token: 0x06002B8A RID: 11146 RVA: 0x002C4AA8 File Offset: 0x002C3EA8
		public SqlByte ToSqlByte()
		{
			return (SqlByte)this;
		}

		// Token: 0x06002B8B RID: 11147 RVA: 0x002C4AC8 File Offset: 0x002C3EC8
		public SqlDouble ToSqlDouble()
		{
			return (SqlDouble)this;
		}

		// Token: 0x06002B8C RID: 11148 RVA: 0x002C4AE8 File Offset: 0x002C3EE8
		public SqlInt16 ToSqlInt16()
		{
			return (SqlInt16)this;
		}

		// Token: 0x06002B8D RID: 11149 RVA: 0x002C4B08 File Offset: 0x002C3F08
		public SqlInt32 ToSqlInt32()
		{
			return (SqlInt32)this;
		}

		// Token: 0x06002B8E RID: 11150 RVA: 0x002C4B28 File Offset: 0x002C3F28
		public SqlInt64 ToSqlInt64()
		{
			return (SqlInt64)this;
		}

		// Token: 0x06002B8F RID: 11151 RVA: 0x002C4B48 File Offset: 0x002C3F48
		public SqlMoney ToSqlMoney()
		{
			return (SqlMoney)this;
		}

		// Token: 0x06002B90 RID: 11152 RVA: 0x002C4B68 File Offset: 0x002C3F68
		public SqlDecimal ToSqlDecimal()
		{
			return (SqlDecimal)this;
		}

		// Token: 0x06002B91 RID: 11153 RVA: 0x002C4B88 File Offset: 0x002C3F88
		public SqlSingle ToSqlSingle()
		{
			return (SqlSingle)this;
		}

		// Token: 0x06002B92 RID: 11154 RVA: 0x002C4BA8 File Offset: 0x002C3FA8
		public SqlString ToSqlString()
		{
			return (SqlString)this;
		}

		// Token: 0x06002B93 RID: 11155 RVA: 0x002C4BC8 File Offset: 0x002C3FC8
		public int CompareTo(object value)
		{
			if (value is SqlBoolean)
			{
				SqlBoolean value2 = (SqlBoolean)value;
				return this.CompareTo(value2);
			}
			throw ADP.WrongType(value.GetType(), typeof(SqlBoolean));
		}

		// Token: 0x06002B94 RID: 11156 RVA: 0x002C4C08 File Offset: 0x002C4008
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

		// Token: 0x06002B95 RID: 11157 RVA: 0x002C4C58 File Offset: 0x002C4058
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

		// Token: 0x06002B96 RID: 11158 RVA: 0x002C4CB8 File Offset: 0x002C40B8
		public override int GetHashCode()
		{
			if (!this.IsNull)
			{
				return this.Value.GetHashCode();
			}
			return 0;
		}

		// Token: 0x06002B97 RID: 11159 RVA: 0x002C4CE8 File Offset: 0x002C40E8
		XmlSchema IXmlSerializable.GetSchema()
		{
			return null;
		}

		// Token: 0x06002B98 RID: 11160 RVA: 0x002C4CF8 File Offset: 0x002C40F8
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			string attribute = reader.GetAttribute("nil", "http://www.w3.org/2001/XMLSchema-instance");
			if (attribute != null && XmlConvert.ToBoolean(attribute))
			{
				this.m_value = 0;
				return;
			}
			this.m_value = (XmlConvert.ToBoolean(reader.ReadElementString()) ? 2 : 1);
		}

		// Token: 0x06002B99 RID: 11161 RVA: 0x002C4D48 File Offset: 0x002C4148
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			if (this.IsNull)
			{
				writer.WriteAttributeString("xsi", "nil", "http://www.w3.org/2001/XMLSchema-instance", "true");
				return;
			}
			writer.WriteString((this.m_value == 2) ? "true" : "false");
		}

		// Token: 0x06002B9A RID: 11162 RVA: 0x002C4D98 File Offset: 0x002C4198
		public static XmlQualifiedName GetXsdType(XmlSchemaSet schemaSet)
		{
			return new XmlQualifiedName("boolean", "http://www.w3.org/2001/XMLSchema");
		}

		// Token: 0x04001C61 RID: 7265
		private const byte x_Null = 0;

		// Token: 0x04001C62 RID: 7266
		private const byte x_False = 1;

		// Token: 0x04001C63 RID: 7267
		private const byte x_True = 2;

		// Token: 0x04001C64 RID: 7268
		private byte m_value;

		// Token: 0x04001C65 RID: 7269
		public static readonly SqlBoolean True = new SqlBoolean(true);

		// Token: 0x04001C66 RID: 7270
		public static readonly SqlBoolean False = new SqlBoolean(false);

		// Token: 0x04001C67 RID: 7271
		public static readonly SqlBoolean Null = new SqlBoolean(0, true);

		// Token: 0x04001C68 RID: 7272
		public static readonly SqlBoolean Zero = new SqlBoolean(0);

		// Token: 0x04001C69 RID: 7273
		public static readonly SqlBoolean One = new SqlBoolean(1);
	}
}
