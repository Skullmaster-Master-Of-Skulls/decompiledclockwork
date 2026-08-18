using System;
using System.Data.Common;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System.Data.SqlTypes
{
	// Token: 0x02000160 RID: 352
	[XmlSchemaProvider("GetXsdType")]
	[Serializable]
	public struct SqlGuid : INullable, IComparable, IXmlSerializable
	{
		// Token: 0x060015A0 RID: 5536 RVA: 0x000A31BC File Offset: 0x000A25BC
		private SqlGuid(bool fNull)
		{
			this.m_value = null;
		}

		// Token: 0x060015A1 RID: 5537 RVA: 0x000A31D0 File Offset: 0x000A25D0
		public SqlGuid(byte[] value)
		{
			if (value == null || value.Length != 16)
			{
				throw new ArgumentException(SQLResource.InvalidArraySizeMessage);
			}
			this.m_value = new byte[16];
			value.CopyTo(this.m_value, 0);
		}

		// Token: 0x060015A2 RID: 5538 RVA: 0x000A320C File Offset: 0x000A260C
		internal SqlGuid(byte[] value, bool ignored)
		{
			if (value == null || value.Length != 16)
			{
				throw new ArgumentException(SQLResource.InvalidArraySizeMessage);
			}
			this.m_value = value;
		}

		// Token: 0x060015A3 RID: 5539 RVA: 0x000A3238 File Offset: 0x000A2638
		public SqlGuid(string s)
		{
			this.m_value = new Guid(s).ToByteArray();
		}

		// Token: 0x060015A4 RID: 5540 RVA: 0x000A325C File Offset: 0x000A265C
		public SqlGuid(Guid g)
		{
			this.m_value = g.ToByteArray();
		}

		// Token: 0x060015A5 RID: 5541 RVA: 0x000A3278 File Offset: 0x000A2678
		public SqlGuid(int a, short b, short c, byte d, byte e, byte f, byte g, byte h, byte i, byte j, byte k)
		{
			this = new SqlGuid(new Guid(a, b, c, d, e, f, g, h, i, j, k));
		}

		// Token: 0x17000329 RID: 809
		// (get) Token: 0x060015A6 RID: 5542 RVA: 0x000A32A4 File Offset: 0x000A26A4
		public bool IsNull
		{
			get
			{
				return this.m_value == null;
			}
		}

		// Token: 0x1700032A RID: 810
		// (get) Token: 0x060015A7 RID: 5543 RVA: 0x000A32BC File Offset: 0x000A26BC
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

		// Token: 0x060015A8 RID: 5544 RVA: 0x000A32E4 File Offset: 0x000A26E4
		public static implicit operator SqlGuid(Guid x)
		{
			return new SqlGuid(x);
		}

		// Token: 0x060015A9 RID: 5545 RVA: 0x000A32F8 File Offset: 0x000A26F8
		public static explicit operator Guid(SqlGuid x)
		{
			return x.Value;
		}

		// Token: 0x060015AA RID: 5546 RVA: 0x000A330C File Offset: 0x000A270C
		public byte[] ToByteArray()
		{
			byte[] array = new byte[16];
			this.m_value.CopyTo(array, 0);
			return array;
		}

		// Token: 0x060015AB RID: 5547 RVA: 0x000A3330 File Offset: 0x000A2730
		public override string ToString()
		{
			if (this.IsNull)
			{
				return SQLResource.NullString;
			}
			Guid guid = new Guid(this.m_value);
			return guid.ToString();
		}

		// Token: 0x060015AC RID: 5548 RVA: 0x000A3368 File Offset: 0x000A2768
		public static SqlGuid Parse(string s)
		{
			if (s == SQLResource.NullString)
			{
				return SqlGuid.Null;
			}
			return new SqlGuid(s);
		}

		// Token: 0x060015AD RID: 5549 RVA: 0x000A3390 File Offset: 0x000A2790
		private static EComparison Compare(SqlGuid x, SqlGuid y)
		{
			int i = 0;
			while (i < 16)
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

		// Token: 0x060015AE RID: 5550 RVA: 0x000A33D8 File Offset: 0x000A27D8
		public static explicit operator SqlGuid(SqlString x)
		{
			if (!x.IsNull)
			{
				return new SqlGuid(x.Value);
			}
			return SqlGuid.Null;
		}

		// Token: 0x060015AF RID: 5551 RVA: 0x000A3400 File Offset: 0x000A2800
		public static explicit operator SqlGuid(SqlBinary x)
		{
			if (!x.IsNull)
			{
				return new SqlGuid(x.Value);
			}
			return SqlGuid.Null;
		}

		// Token: 0x060015B0 RID: 5552 RVA: 0x000A3428 File Offset: 0x000A2828
		public static SqlBoolean operator ==(SqlGuid x, SqlGuid y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(SqlGuid.Compare(x, y) == EComparison.EQ);
			}
			return SqlBoolean.Null;
		}

		// Token: 0x060015B1 RID: 5553 RVA: 0x000A345C File Offset: 0x000A285C
		public static SqlBoolean operator !=(SqlGuid x, SqlGuid y)
		{
			return !(x == y);
		}

		// Token: 0x060015B2 RID: 5554 RVA: 0x000A3478 File Offset: 0x000A2878
		public static SqlBoolean operator <(SqlGuid x, SqlGuid y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(SqlGuid.Compare(x, y) == EComparison.LT);
			}
			return SqlBoolean.Null;
		}

		// Token: 0x060015B3 RID: 5555 RVA: 0x000A34AC File Offset: 0x000A28AC
		public static SqlBoolean operator >(SqlGuid x, SqlGuid y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(SqlGuid.Compare(x, y) == EComparison.GT);
			}
			return SqlBoolean.Null;
		}

		// Token: 0x060015B4 RID: 5556 RVA: 0x000A34E0 File Offset: 0x000A28E0
		public static SqlBoolean operator <=(SqlGuid x, SqlGuid y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlBoolean.Null;
			}
			EComparison ecomparison = SqlGuid.Compare(x, y);
			return new SqlBoolean(ecomparison == EComparison.LT || ecomparison == EComparison.EQ);
		}

		// Token: 0x060015B5 RID: 5557 RVA: 0x000A351C File Offset: 0x000A291C
		public static SqlBoolean operator >=(SqlGuid x, SqlGuid y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlBoolean.Null;
			}
			EComparison ecomparison = SqlGuid.Compare(x, y);
			return new SqlBoolean(ecomparison == EComparison.GT || ecomparison == EComparison.EQ);
		}

		// Token: 0x060015B6 RID: 5558 RVA: 0x000A355C File Offset: 0x000A295C
		public static SqlBoolean Equals(SqlGuid x, SqlGuid y)
		{
			return x == y;
		}

		// Token: 0x060015B7 RID: 5559 RVA: 0x000A3570 File Offset: 0x000A2970
		public static SqlBoolean NotEquals(SqlGuid x, SqlGuid y)
		{
			return x != y;
		}

		// Token: 0x060015B8 RID: 5560 RVA: 0x000A3584 File Offset: 0x000A2984
		public static SqlBoolean LessThan(SqlGuid x, SqlGuid y)
		{
			return x < y;
		}

		// Token: 0x060015B9 RID: 5561 RVA: 0x000A3598 File Offset: 0x000A2998
		public static SqlBoolean GreaterThan(SqlGuid x, SqlGuid y)
		{
			return x > y;
		}

		// Token: 0x060015BA RID: 5562 RVA: 0x000A35AC File Offset: 0x000A29AC
		public static SqlBoolean LessThanOrEqual(SqlGuid x, SqlGuid y)
		{
			return x <= y;
		}

		// Token: 0x060015BB RID: 5563 RVA: 0x000A35C0 File Offset: 0x000A29C0
		public static SqlBoolean GreaterThanOrEqual(SqlGuid x, SqlGuid y)
		{
			return x >= y;
		}

		// Token: 0x060015BC RID: 5564 RVA: 0x000A35D4 File Offset: 0x000A29D4
		public SqlString ToSqlString()
		{
			return (SqlString)this;
		}

		// Token: 0x060015BD RID: 5565 RVA: 0x000A35EC File Offset: 0x000A29EC
		public SqlBinary ToSqlBinary()
		{
			return (SqlBinary)this;
		}

		// Token: 0x060015BE RID: 5566 RVA: 0x000A3604 File Offset: 0x000A2A04
		public int CompareTo(object value)
		{
			if (value is SqlGuid)
			{
				SqlGuid value2 = (SqlGuid)value;
				return this.CompareTo(value2);
			}
			throw ADP.WrongType(value.GetType(), typeof(SqlGuid));
		}

		// Token: 0x060015BF RID: 5567 RVA: 0x000A3640 File Offset: 0x000A2A40
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

		// Token: 0x060015C0 RID: 5568 RVA: 0x000A3698 File Offset: 0x000A2A98
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

		// Token: 0x060015C1 RID: 5569 RVA: 0x000A36F0 File Offset: 0x000A2AF0
		public override int GetHashCode()
		{
			if (!this.IsNull)
			{
				return this.Value.GetHashCode();
			}
			return 0;
		}

		// Token: 0x060015C2 RID: 5570 RVA: 0x000A371C File Offset: 0x000A2B1C
		XmlSchema IXmlSerializable.GetSchema()
		{
			return null;
		}

		// Token: 0x060015C3 RID: 5571 RVA: 0x000A372C File Offset: 0x000A2B2C
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			string attribute = reader.GetAttribute("nil", "http://www.w3.org/2001/XMLSchema-instance");
			if (attribute != null && XmlConvert.ToBoolean(attribute))
			{
				reader.ReadElementString();
				this.m_value = null;
				return;
			}
			this.m_value = new Guid(reader.ReadElementString()).ToByteArray();
		}

		// Token: 0x060015C4 RID: 5572 RVA: 0x000A3780 File Offset: 0x000A2B80
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			if (this.IsNull)
			{
				writer.WriteAttributeString("xsi", "nil", "http://www.w3.org/2001/XMLSchema-instance", "true");
				return;
			}
			writer.WriteString(XmlConvert.ToString(new Guid(this.m_value)));
		}

		// Token: 0x060015C5 RID: 5573 RVA: 0x000A37C8 File Offset: 0x000A2BC8
		public static XmlQualifiedName GetXsdType(XmlSchemaSet schemaSet)
		{
			return new XmlQualifiedName("string", "http://www.w3.org/2001/XMLSchema");
		}

		// Token: 0x04000DE4 RID: 3556
		private const int SizeOfGuid = 16;

		// Token: 0x04000DE5 RID: 3557
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

		// Token: 0x04000DE6 RID: 3558
		private byte[] m_value;

		// Token: 0x04000DE7 RID: 3559
		public static readonly SqlGuid Null = new SqlGuid(true);
	}
}
