using System;
using System.Data.Common;
using System.Globalization;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System.Data.SqlTypes
{
	// Token: 0x02000150 RID: 336
	[XmlSchemaProvider("GetXsdType")]
	[Serializable]
	public struct SqlBinary : INullable, IComparable, IXmlSerializable
	{
		// Token: 0x0600137A RID: 4986 RVA: 0x0009A780 File Offset: 0x00099B80
		private SqlBinary(bool fNull)
		{
			this.m_value = null;
		}

		// Token: 0x0600137B RID: 4987 RVA: 0x0009A794 File Offset: 0x00099B94
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

		// Token: 0x0600137C RID: 4988 RVA: 0x0009A7C8 File Offset: 0x00099BC8
		internal SqlBinary(byte[] value, bool ignored)
		{
			if (value == null)
			{
				this.m_value = null;
				return;
			}
			this.m_value = value;
		}

		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x0600137D RID: 4989 RVA: 0x0009A7E8 File Offset: 0x00099BE8
		public bool IsNull
		{
			get
			{
				return this.m_value == null;
			}
		}

		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x0600137E RID: 4990 RVA: 0x0009A800 File Offset: 0x00099C00
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

		// Token: 0x170002E4 RID: 740
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

		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x06001380 RID: 4992 RVA: 0x0009A85C File Offset: 0x00099C5C
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

		// Token: 0x06001381 RID: 4993 RVA: 0x0009A880 File Offset: 0x00099C80
		public static implicit operator SqlBinary(byte[] x)
		{
			return new SqlBinary(x);
		}

		// Token: 0x06001382 RID: 4994 RVA: 0x0009A894 File Offset: 0x00099C94
		public static explicit operator byte[](SqlBinary x)
		{
			return x.Value;
		}

		// Token: 0x06001383 RID: 4995 RVA: 0x0009A8A8 File Offset: 0x00099CA8
		public override string ToString()
		{
			if (!this.IsNull)
			{
				return "SqlBinary(" + this.m_value.Length.ToString(CultureInfo.InvariantCulture) + ")";
			}
			return SQLResource.NullString;
		}

		// Token: 0x06001384 RID: 4996 RVA: 0x0009A8E8 File Offset: 0x00099CE8
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

		// Token: 0x06001385 RID: 4997 RVA: 0x0009A950 File Offset: 0x00099D50
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

		// Token: 0x06001386 RID: 4998 RVA: 0x0009A9D4 File Offset: 0x00099DD4
		public static explicit operator SqlBinary(SqlGuid x)
		{
			if (!x.IsNull)
			{
				return new SqlBinary(x.ToByteArray());
			}
			return SqlBinary.Null;
		}

		// Token: 0x06001387 RID: 4999 RVA: 0x0009A9FC File Offset: 0x00099DFC
		public static SqlBoolean operator ==(SqlBinary x, SqlBinary y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlBoolean.Null;
			}
			return new SqlBoolean(SqlBinary.PerformCompareByte(x.Value, y.Value) == EComparison.EQ);
		}

		// Token: 0x06001388 RID: 5000 RVA: 0x0009AA3C File Offset: 0x00099E3C
		public static SqlBoolean operator !=(SqlBinary x, SqlBinary y)
		{
			return !(x == y);
		}

		// Token: 0x06001389 RID: 5001 RVA: 0x0009AA58 File Offset: 0x00099E58
		public static SqlBoolean operator <(SqlBinary x, SqlBinary y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlBoolean.Null;
			}
			return new SqlBoolean(SqlBinary.PerformCompareByte(x.Value, y.Value) == EComparison.LT);
		}

		// Token: 0x0600138A RID: 5002 RVA: 0x0009AA98 File Offset: 0x00099E98
		public static SqlBoolean operator >(SqlBinary x, SqlBinary y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlBoolean.Null;
			}
			return new SqlBoolean(SqlBinary.PerformCompareByte(x.Value, y.Value) == EComparison.GT);
		}

		// Token: 0x0600138B RID: 5003 RVA: 0x0009AAD8 File Offset: 0x00099ED8
		public static SqlBoolean operator <=(SqlBinary x, SqlBinary y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlBoolean.Null;
			}
			EComparison ecomparison = SqlBinary.PerformCompareByte(x.Value, y.Value);
			return new SqlBoolean(ecomparison == EComparison.LT || ecomparison == EComparison.EQ);
		}

		// Token: 0x0600138C RID: 5004 RVA: 0x0009AB20 File Offset: 0x00099F20
		public static SqlBoolean operator >=(SqlBinary x, SqlBinary y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlBoolean.Null;
			}
			EComparison ecomparison = SqlBinary.PerformCompareByte(x.Value, y.Value);
			return new SqlBoolean(ecomparison == EComparison.GT || ecomparison == EComparison.EQ);
		}

		// Token: 0x0600138D RID: 5005 RVA: 0x0009AB6C File Offset: 0x00099F6C
		public static SqlBinary Add(SqlBinary x, SqlBinary y)
		{
			return x + y;
		}

		// Token: 0x0600138E RID: 5006 RVA: 0x0009AB80 File Offset: 0x00099F80
		public static SqlBinary Concat(SqlBinary x, SqlBinary y)
		{
			return x + y;
		}

		// Token: 0x0600138F RID: 5007 RVA: 0x0009AB94 File Offset: 0x00099F94
		public static SqlBoolean Equals(SqlBinary x, SqlBinary y)
		{
			return x == y;
		}

		// Token: 0x06001390 RID: 5008 RVA: 0x0009ABA8 File Offset: 0x00099FA8
		public static SqlBoolean NotEquals(SqlBinary x, SqlBinary y)
		{
			return x != y;
		}

		// Token: 0x06001391 RID: 5009 RVA: 0x0009ABBC File Offset: 0x00099FBC
		public static SqlBoolean LessThan(SqlBinary x, SqlBinary y)
		{
			return x < y;
		}

		// Token: 0x06001392 RID: 5010 RVA: 0x0009ABD0 File Offset: 0x00099FD0
		public static SqlBoolean GreaterThan(SqlBinary x, SqlBinary y)
		{
			return x > y;
		}

		// Token: 0x06001393 RID: 5011 RVA: 0x0009ABE4 File Offset: 0x00099FE4
		public static SqlBoolean LessThanOrEqual(SqlBinary x, SqlBinary y)
		{
			return x <= y;
		}

		// Token: 0x06001394 RID: 5012 RVA: 0x0009ABF8 File Offset: 0x00099FF8
		public static SqlBoolean GreaterThanOrEqual(SqlBinary x, SqlBinary y)
		{
			return x >= y;
		}

		// Token: 0x06001395 RID: 5013 RVA: 0x0009AC0C File Offset: 0x0009A00C
		public SqlGuid ToSqlGuid()
		{
			return (SqlGuid)this;
		}

		// Token: 0x06001396 RID: 5014 RVA: 0x0009AC24 File Offset: 0x0009A024
		public int CompareTo(object value)
		{
			if (value is SqlBinary)
			{
				SqlBinary value2 = (SqlBinary)value;
				return this.CompareTo(value2);
			}
			throw ADP.WrongType(value.GetType(), typeof(SqlBinary));
		}

		// Token: 0x06001397 RID: 5015 RVA: 0x0009AC60 File Offset: 0x0009A060
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

		// Token: 0x06001398 RID: 5016 RVA: 0x0009ACB8 File Offset: 0x0009A0B8
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

		// Token: 0x06001399 RID: 5017 RVA: 0x0009AD10 File Offset: 0x0009A110
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

		// Token: 0x0600139A RID: 5018 RVA: 0x0009AD4C File Offset: 0x0009A14C
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

		// Token: 0x0600139B RID: 5019 RVA: 0x0009AD90 File Offset: 0x0009A190
		XmlSchema IXmlSerializable.GetSchema()
		{
			return null;
		}

		// Token: 0x0600139C RID: 5020 RVA: 0x0009ADA0 File Offset: 0x0009A1A0
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			string attribute = reader.GetAttribute("nil", "http://www.w3.org/2001/XMLSchema-instance");
			if (attribute != null && XmlConvert.ToBoolean(attribute))
			{
				reader.ReadElementString();
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

		// Token: 0x0600139D RID: 5021 RVA: 0x0009AE18 File Offset: 0x0009A218
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			if (this.IsNull)
			{
				writer.WriteAttributeString("xsi", "nil", "http://www.w3.org/2001/XMLSchema-instance", "true");
				return;
			}
			writer.WriteString(Convert.ToBase64String(this.m_value));
		}

		// Token: 0x0600139E RID: 5022 RVA: 0x0009AE5C File Offset: 0x0009A25C
		public static XmlQualifiedName GetXsdType(XmlSchemaSet schemaSet)
		{
			return new XmlQualifiedName("base64Binary", "http://www.w3.org/2001/XMLSchema");
		}

		// Token: 0x04000D4A RID: 3402
		private byte[] m_value;

		// Token: 0x04000D4B RID: 3403
		public static readonly SqlBinary Null = new SqlBinary(true);
	}
}
