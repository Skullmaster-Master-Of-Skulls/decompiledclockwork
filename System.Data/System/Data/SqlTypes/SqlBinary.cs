using System;
using System.Data.Common;
using System.Globalization;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System.Data.SqlTypes
{
	// Token: 0x02000340 RID: 832
	[XmlSchemaProvider("GetXsdType")]
	[Serializable]
	public struct SqlBinary : INullable, IComparable, IXmlSerializable
	{
		// Token: 0x06002B38 RID: 11064 RVA: 0x002C3A58 File Offset: 0x002C2E58
		private SqlBinary(bool fNull)
		{
			this.m_value = null;
		}

		// Token: 0x06002B39 RID: 11065 RVA: 0x002C3A78 File Offset: 0x002C2E78
		public SqlBinary(byte[] value)
		{
			if (value == null)
			{
				this.m_value = null;
				return;
			}
			this.m_value = new byte[value.Length];
			value.CopyTo(this.m_value, 0);
		}

		// Token: 0x06002B3A RID: 11066 RVA: 0x002C3AB8 File Offset: 0x002C2EB8
		internal SqlBinary(byte[] value, bool ignored)
		{
			if (value == null)
			{
				this.m_value = null;
				return;
			}
			this.m_value = value;
		}

		// Token: 0x17000710 RID: 1808
		// (get) Token: 0x06002B3B RID: 11067 RVA: 0x002C3AD8 File Offset: 0x002C2ED8
		public bool IsNull
		{
			get
			{
				return this.m_value == null;
			}
		}

		// Token: 0x17000711 RID: 1809
		// (get) Token: 0x06002B3C RID: 11068 RVA: 0x002C3AF8 File Offset: 0x002C2EF8
		public byte[] Value
		{
			get
			{
				if (this.IsNull)
				{
					throw new SqlNullValueException();
				}
				byte[] array = new byte[this.m_value.Length];
				this.m_value.CopyTo(array, 0);
				return array;
			}
		}

		// Token: 0x17000712 RID: 1810
		public byte this[int index]
		{
			get
			{
				if (this.IsNull)
				{
					throw new SqlNullValueException();
				}
				return this.m_value[index];
			}
		}

		// Token: 0x17000713 RID: 1811
		// (get) Token: 0x06002B3E RID: 11070 RVA: 0x002C3B68 File Offset: 0x002C2F68
		public int Length
		{
			get
			{
				if (!this.IsNull)
				{
					return this.m_value.Length;
				}
				throw new SqlNullValueException();
			}
		}

		// Token: 0x06002B3F RID: 11071 RVA: 0x002C3B98 File Offset: 0x002C2F98
		public static implicit operator SqlBinary(byte[] x)
		{
			return new SqlBinary(x);
		}

		// Token: 0x06002B40 RID: 11072 RVA: 0x002C3BB8 File Offset: 0x002C2FB8
		public static explicit operator byte[](SqlBinary x)
		{
			return x.Value;
		}

		// Token: 0x06002B41 RID: 11073 RVA: 0x002C3BD8 File Offset: 0x002C2FD8
		public override string ToString()
		{
			if (!this.IsNull)
			{
				return "SqlBinary(" + this.m_value.Length.ToString(CultureInfo.InvariantCulture) + ")";
			}
			return SQLResource.NullString;
		}

		// Token: 0x06002B42 RID: 11074 RVA: 0x002C3C18 File Offset: 0x002C3018
		public static SqlBinary operator +(SqlBinary x, SqlBinary y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlBinary.Null;
			}
			byte[] array = new byte[x.Value.Length + y.Value.Length];
			x.Value.CopyTo(array, 0);
			y.Value.CopyTo(array, x.Value.Length);
			return new SqlBinary(array);
		}

		// Token: 0x06002B43 RID: 11075 RVA: 0x002C3C88 File Offset: 0x002C3088
		private static EComparison PerformCompareByte(byte[] x, byte[] y)
		{
			int num = (x.Length < y.Length) ? x.Length : y.Length;
			int i = 0;
			while (i < num)
			{
				if (x[i] != y[i])
				{
					if (x[i] < y[i])
					{
						return EComparison.LT;
					}
					return EComparison.GT;
				}
				else
				{
					i++;
				}
			}
			if (x.Length == y.Length)
			{
				return EComparison.EQ;
			}
			byte b = 0;
			if (x.Length < y.Length)
			{
				for (i = num; i < y.Length; i++)
				{
					if (y[i] != b)
					{
						return EComparison.LT;
					}
				}
			}
			else
			{
				for (i = num; i < x.Length; i++)
				{
					if (x[i] != b)
					{
						return EComparison.GT;
					}
				}
			}
			return EComparison.EQ;
		}

		// Token: 0x06002B44 RID: 11076 RVA: 0x002C3D18 File Offset: 0x002C3118
		public static explicit operator SqlBinary(SqlGuid x)
		{
			if (!x.IsNull)
			{
				return new SqlBinary(x.ToByteArray());
			}
			return SqlBinary.Null;
		}

		// Token: 0x06002B45 RID: 11077 RVA: 0x002C3D48 File Offset: 0x002C3148
		public static SqlBoolean operator ==(SqlBinary x, SqlBinary y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlBoolean.Null;
			}
			return new SqlBoolean(SqlBinary.PerformCompareByte(x.Value, y.Value) == EComparison.EQ);
		}

		// Token: 0x06002B46 RID: 11078 RVA: 0x002C3D88 File Offset: 0x002C3188
		public static SqlBoolean operator !=(SqlBinary x, SqlBinary y)
		{
			return !(x == y);
		}

		// Token: 0x06002B47 RID: 11079 RVA: 0x002C3DA8 File Offset: 0x002C31A8
		public static SqlBoolean operator <(SqlBinary x, SqlBinary y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlBoolean.Null;
			}
			return new SqlBoolean(SqlBinary.PerformCompareByte(x.Value, y.Value) == EComparison.LT);
		}

		// Token: 0x06002B48 RID: 11080 RVA: 0x002C3DE8 File Offset: 0x002C31E8
		public static SqlBoolean operator >(SqlBinary x, SqlBinary y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlBoolean.Null;
			}
			return new SqlBoolean(SqlBinary.PerformCompareByte(x.Value, y.Value) == EComparison.GT);
		}

		// Token: 0x06002B49 RID: 11081 RVA: 0x002C3E28 File Offset: 0x002C3228
		public static SqlBoolean operator <=(SqlBinary x, SqlBinary y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlBoolean.Null;
			}
			EComparison ecomparison = SqlBinary.PerformCompareByte(x.Value, y.Value);
			return new SqlBoolean(ecomparison == EComparison.LT || ecomparison == EComparison.EQ);
		}

		// Token: 0x06002B4A RID: 11082 RVA: 0x002C3E78 File Offset: 0x002C3278
		public static SqlBoolean operator >=(SqlBinary x, SqlBinary y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlBoolean.Null;
			}
			EComparison ecomparison = SqlBinary.PerformCompareByte(x.Value, y.Value);
			return new SqlBoolean(ecomparison == EComparison.GT || ecomparison == EComparison.EQ);
		}

		// Token: 0x06002B4B RID: 11083 RVA: 0x002C3EC8 File Offset: 0x002C32C8
		public static SqlBinary Add(SqlBinary x, SqlBinary y)
		{
			return x + y;
		}

		// Token: 0x06002B4C RID: 11084 RVA: 0x002C3EE8 File Offset: 0x002C32E8
		public static SqlBinary Concat(SqlBinary x, SqlBinary y)
		{
			return x + y;
		}

		// Token: 0x06002B4D RID: 11085 RVA: 0x002C3F08 File Offset: 0x002C3308
		public static SqlBoolean Equals(SqlBinary x, SqlBinary y)
		{
			return x == y;
		}

		// Token: 0x06002B4E RID: 11086 RVA: 0x002C3F28 File Offset: 0x002C3328
		public static SqlBoolean NotEquals(SqlBinary x, SqlBinary y)
		{
			return x != y;
		}

		// Token: 0x06002B4F RID: 11087 RVA: 0x002C3F48 File Offset: 0x002C3348
		public static SqlBoolean LessThan(SqlBinary x, SqlBinary y)
		{
			return x < y;
		}

		// Token: 0x06002B50 RID: 11088 RVA: 0x002C3F68 File Offset: 0x002C3368
		public static SqlBoolean GreaterThan(SqlBinary x, SqlBinary y)
		{
			return x > y;
		}

		// Token: 0x06002B51 RID: 11089 RVA: 0x002C3F88 File Offset: 0x002C3388
		public static SqlBoolean LessThanOrEqual(SqlBinary x, SqlBinary y)
		{
			return x <= y;
		}

		// Token: 0x06002B52 RID: 11090 RVA: 0x002C3FA8 File Offset: 0x002C33A8
		public static SqlBoolean GreaterThanOrEqual(SqlBinary x, SqlBinary y)
		{
			return x >= y;
		}

		// Token: 0x06002B53 RID: 11091 RVA: 0x002C3FC8 File Offset: 0x002C33C8
		public SqlGuid ToSqlGuid()
		{
			return (SqlGuid)this;
		}

		// Token: 0x06002B54 RID: 11092 RVA: 0x002C3FE8 File Offset: 0x002C33E8
		public int CompareTo(object value)
		{
			if (value is SqlBinary)
			{
				SqlBinary value2 = (SqlBinary)value;
				return this.CompareTo(value2);
			}
			throw ADP.WrongType(value.GetType(), typeof(SqlBinary));
		}

		// Token: 0x06002B55 RID: 11093 RVA: 0x002C4028 File Offset: 0x002C3428
		public int CompareTo(SqlBinary value)
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

		// Token: 0x06002B56 RID: 11094 RVA: 0x002C4088 File Offset: 0x002C3488
		public override bool Equals(object value)
		{
			if (!(value is SqlBinary))
			{
				return false;
			}
			SqlBinary y = (SqlBinary)value;
			if (y.IsNull || this.IsNull)
			{
				return y.IsNull && this.IsNull;
			}
			return (this == y).Value;
		}

		// Token: 0x06002B57 RID: 11095 RVA: 0x002C40E8 File Offset: 0x002C34E8
		internal static int HashByteArray(byte[] rgbValue, int length)
		{
			if (length <= 0)
			{
				return 0;
			}
			int num = 0;
			for (int i = 0; i < length; i++)
			{
				int num2 = num >> 28 & 255;
				num <<= 4;
				num = (num ^ (int)rgbValue[i] ^ num2);
			}
			return num;
		}

		// Token: 0x06002B58 RID: 11096 RVA: 0x002C4128 File Offset: 0x002C3528
		public override int GetHashCode()
		{
			if (this.IsNull)
			{
				return 0;
			}
			int num = this.m_value.Length;
			while (num > 0 && this.m_value[num - 1] == 0)
			{
				num--;
			}
			return SqlBinary.HashByteArray(this.m_value, num);
		}

		// Token: 0x06002B59 RID: 11097 RVA: 0x002C4178 File Offset: 0x002C3578
		XmlSchema IXmlSerializable.GetSchema()
		{
			return null;
		}

		// Token: 0x06002B5A RID: 11098 RVA: 0x002C4188 File Offset: 0x002C3588
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			string attribute = reader.GetAttribute("nil", "http://www.w3.org/2001/XMLSchema-instance");
			if (attribute != null && XmlConvert.ToBoolean(attribute))
			{
				this.m_value = null;
				return;
			}
			string text = reader.ReadElementString();
			if (text == null)
			{
				this.m_value = new byte[0];
				return;
			}
			text = text.Trim();
			if (text.Length == 0)
			{
				this.m_value = new byte[0];
				return;
			}
			this.m_value = Convert.FromBase64String(text);
		}

		// Token: 0x06002B5B RID: 11099 RVA: 0x002C41F8 File Offset: 0x002C35F8
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			if (this.IsNull)
			{
				writer.WriteAttributeString("xsi", "nil", "http://www.w3.org/2001/XMLSchema-instance", "true");
				return;
			}
			writer.WriteString(Convert.ToBase64String(this.m_value));
		}

		// Token: 0x06002B5C RID: 11100 RVA: 0x002C4248 File Offset: 0x002C3648
		public static XmlQualifiedName GetXsdType(XmlSchemaSet schemaSet)
		{
			return new XmlQualifiedName("base64Binary", "http://www.w3.org/2001/XMLSchema");
		}

		// Token: 0x04001C5F RID: 7263
		private byte[] m_value;

		// Token: 0x04001C60 RID: 7264
		public static readonly SqlBinary Null = new SqlBinary(true);
	}
}
