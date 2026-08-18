using System;
using System.Data.Common;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System.Data.SqlTypes
{
	// Token: 0x0200034F RID: 847
	[XmlSchemaProvider("GetXsdType")]
	[Serializable]
	public struct SqlGuid : INullable, IComparable, IXmlSerializable
	{
		// Token: 0x06002D4B RID: 11595 RVA: 0x002CD6D8 File Offset: 0x002CCAD8
		private SqlGuid(bool fNull)
		{
			this.m_value = null;
		}

		// Token: 0x06002D4C RID: 11596 RVA: 0x002CD6F8 File Offset: 0x002CCAF8
		public SqlGuid(byte[] value)
		{
			if (value == null || value.Length != SqlGuid.SizeOfGuid)
			{
				throw new ArgumentException(SQLResource.InvalidArraySizeMessage);
			}
			this.m_value = new byte[SqlGuid.SizeOfGuid];
			value.CopyTo(this.m_value, 0);
		}

		// Token: 0x06002D4D RID: 11597 RVA: 0x002CD748 File Offset: 0x002CCB48
		internal SqlGuid(byte[] value, bool ignored)
		{
			if (value == null || value.Length != SqlGuid.SizeOfGuid)
			{
				throw new ArgumentException(SQLResource.InvalidArraySizeMessage);
			}
			this.m_value = value;
		}

		// Token: 0x06002D4E RID: 11598 RVA: 0x002CD778 File Offset: 0x002CCB78
		public SqlGuid(string s)
		{
			this.m_value = new Guid(s).ToByteArray();
		}

		// Token: 0x06002D4F RID: 11599 RVA: 0x002CD7A8 File Offset: 0x002CCBA8
		public SqlGuid(Guid g)
		{
			this.m_value = g.ToByteArray();
		}

		// Token: 0x06002D50 RID: 11600 RVA: 0x002CD7C8 File Offset: 0x002CCBC8
		public SqlGuid(int a, short b, short c, byte d, byte e, byte f, byte g, byte h, byte i, byte j, byte k)
		{
			this = new SqlGuid(new Guid(a, b, c, d, e, f, g, h, i, j, k));
		}

		// Token: 0x17000750 RID: 1872
		// (get) Token: 0x06002D51 RID: 11601 RVA: 0x002CD7F8 File Offset: 0x002CCBF8
		public bool IsNull
		{
			get
			{
				return this.m_value == null;
			}
		}

		// Token: 0x17000751 RID: 1873
		// (get) Token: 0x06002D52 RID: 11602 RVA: 0x002CD818 File Offset: 0x002CCC18
		public Guid Value
		{
			get
			{
				if (this.IsNull)
				{
					throw new SqlNullValueException();
				}
				return new Guid(this.m_value);
			}
		}

		// Token: 0x06002D53 RID: 11603 RVA: 0x002CD848 File Offset: 0x002CCC48
		public static implicit operator SqlGuid(Guid x)
		{
			return new SqlGuid(x);
		}

		// Token: 0x06002D54 RID: 11604 RVA: 0x002CD868 File Offset: 0x002CCC68
		public static explicit operator Guid(SqlGuid x)
		{
			return x.Value;
		}

		// Token: 0x06002D55 RID: 11605 RVA: 0x002CD888 File Offset: 0x002CCC88
		public byte[] ToByteArray()
		{
			byte[] array = new byte[SqlGuid.SizeOfGuid];
			this.m_value.CopyTo(array, 0);
			return array;
		}

		// Token: 0x06002D56 RID: 11606 RVA: 0x002CD8B8 File Offset: 0x002CCCB8
		public override string ToString()
		{
			if (this.IsNull)
			{
				return SQLResource.NullString;
			}
			Guid guid = new Guid(this.m_value);
			return guid.ToString();
		}

		// Token: 0x06002D57 RID: 11607 RVA: 0x002CD8F8 File Offset: 0x002CCCF8
		public static SqlGuid Parse(string s)
		{
			if (s == SQLResource.NullString)
			{
				return SqlGuid.Null;
			}
			return new SqlGuid(s);
		}

		// Token: 0x06002D58 RID: 11608 RVA: 0x002CD928 File Offset: 0x002CCD28
		private static EComparison Compare(SqlGuid x, SqlGuid y)
		{
			int i = 0;
			while (i < SqlGuid.SizeOfGuid)
			{
				byte b = x.m_value[SqlGuid.x_rgiGuidOrder[i]];
				byte b2 = y.m_value[SqlGuid.x_rgiGuidOrder[i]];
				if (b != b2)
				{
					if (b >= b2)
					{
						return EComparison.GT;
					}
					return EComparison.LT;
				}
				else
				{
					i++;
				}
			}
			return EComparison.EQ;
		}

		// Token: 0x06002D59 RID: 11609 RVA: 0x002CD978 File Offset: 0x002CCD78
		public static explicit operator SqlGuid(SqlString x)
		{
			if (!x.IsNull)
			{
				return new SqlGuid(x.Value);
			}
			return SqlGuid.Null;
		}

		// Token: 0x06002D5A RID: 11610 RVA: 0x002CD9A8 File Offset: 0x002CCDA8
		public static explicit operator SqlGuid(SqlBinary x)
		{
			if (!x.IsNull)
			{
				return new SqlGuid(x.Value);
			}
			return SqlGuid.Null;
		}

		// Token: 0x06002D5B RID: 11611 RVA: 0x002CD9D8 File Offset: 0x002CCDD8
		public static SqlBoolean operator ==(SqlGuid x, SqlGuid y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(SqlGuid.Compare(x, y) == EComparison.EQ);
			}
			return SqlBoolean.Null;
		}

		// Token: 0x06002D5C RID: 11612 RVA: 0x002CDA18 File Offset: 0x002CCE18
		public static SqlBoolean operator !=(SqlGuid x, SqlGuid y)
		{
			return !(x == y);
		}

		// Token: 0x06002D5D RID: 11613 RVA: 0x002CDA38 File Offset: 0x002CCE38
		public static SqlBoolean operator <(SqlGuid x, SqlGuid y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(SqlGuid.Compare(x, y) == EComparison.LT);
			}
			return SqlBoolean.Null;
		}

		// Token: 0x06002D5E RID: 11614 RVA: 0x002CDA78 File Offset: 0x002CCE78
		public static SqlBoolean operator >(SqlGuid x, SqlGuid y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(SqlGuid.Compare(x, y) == EComparison.GT);
			}
			return SqlBoolean.Null;
		}

		// Token: 0x06002D5F RID: 11615 RVA: 0x002CDAB8 File Offset: 0x002CCEB8
		public static SqlBoolean operator <=(SqlGuid x, SqlGuid y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlBoolean.Null;
			}
			EComparison ecomparison = SqlGuid.Compare(x, y);
			return new SqlBoolean(ecomparison == EComparison.LT || ecomparison == EComparison.EQ);
		}

		// Token: 0x06002D60 RID: 11616 RVA: 0x002CDAF8 File Offset: 0x002CCEF8
		public static SqlBoolean operator >=(SqlGuid x, SqlGuid y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlBoolean.Null;
			}
			EComparison ecomparison = SqlGuid.Compare(x, y);
			return new SqlBoolean(ecomparison == EComparison.GT || ecomparison == EComparison.EQ);
		}

		// Token: 0x06002D61 RID: 11617 RVA: 0x002CDB38 File Offset: 0x002CCF38
		public static SqlBoolean Equals(SqlGuid x, SqlGuid y)
		{
			return x == y;
		}

		// Token: 0x06002D62 RID: 11618 RVA: 0x002CDB58 File Offset: 0x002CCF58
		public static SqlBoolean NotEquals(SqlGuid x, SqlGuid y)
		{
			return x != y;
		}

		// Token: 0x06002D63 RID: 11619 RVA: 0x002CDB78 File Offset: 0x002CCF78
		public static SqlBoolean LessThan(SqlGuid x, SqlGuid y)
		{
			return x < y;
		}

		// Token: 0x06002D64 RID: 11620 RVA: 0x002CDB98 File Offset: 0x002CCF98
		public static SqlBoolean GreaterThan(SqlGuid x, SqlGuid y)
		{
			return x > y;
		}

		// Token: 0x06002D65 RID: 11621 RVA: 0x002CDBB8 File Offset: 0x002CCFB8
		public static SqlBoolean LessThanOrEqual(SqlGuid x, SqlGuid y)
		{
			return x <= y;
		}

		// Token: 0x06002D66 RID: 11622 RVA: 0x002CDBD8 File Offset: 0x002CCFD8
		public static SqlBoolean GreaterThanOrEqual(SqlGuid x, SqlGuid y)
		{
			return x >= y;
		}

		// Token: 0x06002D67 RID: 11623 RVA: 0x002CDBF8 File Offset: 0x002CCFF8
		public SqlString ToSqlString()
		{
			return (SqlString)this;
		}

		// Token: 0x06002D68 RID: 11624 RVA: 0x002CDC18 File Offset: 0x002CD018
		public SqlBinary ToSqlBinary()
		{
			return (SqlBinary)this;
		}

		// Token: 0x06002D69 RID: 11625 RVA: 0x002CDC38 File Offset: 0x002CD038
		public int CompareTo(object value)
		{
			if (value is SqlGuid)
			{
				SqlGuid value2 = (SqlGuid)value;
				return this.CompareTo(value2);
			}
			throw ADP.WrongType(value.GetType(), typeof(SqlGuid));
		}

		// Token: 0x06002D6A RID: 11626 RVA: 0x002CDC78 File Offset: 0x002CD078
		public int CompareTo(SqlGuid value)
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

		// Token: 0x06002D6B RID: 11627 RVA: 0x002CDCD8 File Offset: 0x002CD0D8
		public override bool Equals(object value)
		{
			if (!(value is SqlGuid))
			{
				return false;
			}
			SqlGuid y = (SqlGuid)value;
			if (y.IsNull || this.IsNull)
			{
				return y.IsNull && this.IsNull;
			}
			return (this == y).Value;
		}

		// Token: 0x06002D6C RID: 11628 RVA: 0x002CDD38 File Offset: 0x002CD138
		public override int GetHashCode()
		{
			if (!this.IsNull)
			{
				return this.Value.GetHashCode();
			}
			return 0;
		}

		// Token: 0x06002D6D RID: 11629 RVA: 0x002CDD68 File Offset: 0x002CD168
		XmlSchema IXmlSerializable.GetSchema()
		{
			return null;
		}

		// Token: 0x06002D6E RID: 11630 RVA: 0x002CDD78 File Offset: 0x002CD178
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			string attribute = reader.GetAttribute("nil", "http://www.w3.org/2001/XMLSchema-instance");
			if (attribute != null && XmlConvert.ToBoolean(attribute))
			{
				this.m_value = null;
				return;
			}
			this.m_value = new Guid(reader.ReadElementString()).ToByteArray();
		}

		// Token: 0x06002D6F RID: 11631 RVA: 0x002CDDC8 File Offset: 0x002CD1C8
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			if (this.IsNull)
			{
				writer.WriteAttributeString("xsi", "nil", "http://www.w3.org/2001/XMLSchema-instance", "true");
				return;
			}
			writer.WriteString(XmlConvert.ToString(new Guid(this.m_value)));
		}

		// Token: 0x06002D70 RID: 11632 RVA: 0x002CDE18 File Offset: 0x002CD218
		public static XmlQualifiedName GetXsdType(XmlSchemaSet schemaSet)
		{
			return new XmlQualifiedName("string", "http://www.w3.org/2001/XMLSchema");
		}

		// Token: 0x04001CFB RID: 7419
		private static readonly int SizeOfGuid = 16;

		// Token: 0x04001CFC RID: 7420
		private static readonly int[] x_rgiGuidOrder = new int[]
		{
			10,
			11,
			12,
			13,
			14,
			15,
			8,
			9,
			6,
			7,
			4,
			5,
			0,
			1,
			2,
			3
		};

		// Token: 0x04001CFD RID: 7421
		private byte[] m_value;

		// Token: 0x04001CFE RID: 7422
		public static readonly SqlGuid Null = new SqlGuid(true);
	}
}
