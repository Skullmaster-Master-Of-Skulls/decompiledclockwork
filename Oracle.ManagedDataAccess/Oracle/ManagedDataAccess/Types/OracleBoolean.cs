using System;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using OracleInternal.Common;

namespace Oracle.ManagedDataAccess.Types
{
	// Token: 0x02000246 RID: 582
	[XmlSchemaProvider("GetXsdType")]
	[Serializable]
	public struct OracleBoolean : IComparable, IXmlSerializable, INullable
	{
		// Token: 0x06001568 RID: 5480 RVA: 0x000E7000 File Offset: 0x000E5200
		public OracleBoolean(bool value)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			this.m_bNotNull = true;
			this.m_valueBool = value;
			if (this.m_valueBool)
			{
				this.m_valueByte = 1;
			}
			else
			{
				this.m_valueByte = 0;
			}
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
			}
		}

		// Token: 0x06001569 RID: 5481 RVA: 0x000E7064 File Offset: 0x000E5264
		public OracleBoolean(int value)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			this.m_bNotNull = true;
			if (value == 0)
			{
				this.m_valueByte = 0;
				this.m_valueBool = false;
			}
			else
			{
				this.m_valueByte = 1;
				this.m_valueBool = true;
			}
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
			}
		}

		// Token: 0x0600156A RID: 5482 RVA: 0x000E70CC File Offset: 0x000E52CC
		public static XmlQualifiedName GetXsdType(XmlSchemaSet schemaSet)
		{
			return new XmlQualifiedName("boolean", "http://www.w3.org/2001/XMLSchema");
		}

		// Token: 0x0600156B RID: 5483 RVA: 0x000E70E0 File Offset: 0x000E52E0
		XmlSchema IXmlSerializable.GetSchema()
		{
			return null;
		}

		// Token: 0x0600156C RID: 5484 RVA: 0x000E70E4 File Offset: 0x000E52E4
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			string attribute = reader.GetAttribute("null", "http://www.w3.org/2001/XMLSchema-instance");
			if (attribute == null || !XmlConvert.ToBoolean(attribute))
			{
				this.m_valueBool = Convert.ToBoolean(reader.ReadElementString());
				this.m_bNotNull = true;
				return;
			}
			this.m_bNotNull = false;
		}

		// Token: 0x0600156D RID: 5485 RVA: 0x000E7130 File Offset: 0x000E5330
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			if (this.m_bNotNull)
			{
				writer.WriteString(Convert.ToString(this.m_valueBool));
				return;
			}
			writer.WriteAttributeString("xsi", "null", "http://www.w3.org/2001/XMLSchema-instance", "true");
		}

		// Token: 0x17000364 RID: 868
		// (get) Token: 0x0600156E RID: 5486 RVA: 0x000E7168 File Offset: 0x000E5368
		public bool IsTrue
		{
			get
			{
				return this.m_bNotNull && this.m_valueBool;
			}
		}

		// Token: 0x17000365 RID: 869
		// (get) Token: 0x0600156F RID: 5487 RVA: 0x000E7180 File Offset: 0x000E5380
		public bool IsFalse
		{
			get
			{
				return this.m_bNotNull && !this.m_valueBool;
			}
		}

		// Token: 0x17000366 RID: 870
		// (get) Token: 0x06001570 RID: 5488 RVA: 0x000E7198 File Offset: 0x000E5398
		public byte ByteValue
		{
			get
			{
				if (this.m_bNotNull)
				{
					return this.m_valueByte;
				}
				throw new OracleNullValueException();
			}
		}

		// Token: 0x17000367 RID: 871
		// (get) Token: 0x06001571 RID: 5489 RVA: 0x000E71B0 File Offset: 0x000E53B0
		public bool IsNull
		{
			get
			{
				return !this.m_bNotNull;
			}
		}

		// Token: 0x17000368 RID: 872
		// (get) Token: 0x06001572 RID: 5490 RVA: 0x000E71BC File Offset: 0x000E53BC
		public bool Value
		{
			get
			{
				if (this.m_bNotNull)
				{
					return this.m_valueBool;
				}
				throw new OracleNullValueException();
			}
		}

		// Token: 0x06001573 RID: 5491 RVA: 0x000E71D4 File Offset: 0x000E53D4
		public int CompareTo(object obj)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			if (obj.GetType() != typeof(OracleBoolean))
			{
				throw new ArgumentException();
			}
			OracleBoolean value = (OracleBoolean)obj;
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
			}
			return this.CompareTo(value);
		}

		// Token: 0x06001574 RID: 5492 RVA: 0x000E723C File Offset: 0x000E543C
		public int CompareTo(OracleBoolean value)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			int result;
			try
			{
				if (!this.m_bNotNull && !value.m_bNotNull)
				{
					result = 0;
				}
				else if (!this.m_bNotNull && value.m_bNotNull)
				{
					result = -1;
				}
				else if (this.m_bNotNull && !value.m_bNotNull)
				{
					result = 1;
				}
				else if (this.m_valueByte < value.m_valueByte)
				{
					result = -1;
				}
				else if (this.m_valueByte > value.m_valueByte)
				{
					result = 1;
				}
				else
				{
					result = 0;
				}
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x06001575 RID: 5493 RVA: 0x000E72F4 File Offset: 0x000E54F4
		public override bool Equals(object obj)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			bool result;
			try
			{
				if (obj == null)
				{
					result = false;
				}
				else if (obj.GetType() != typeof(OracleBoolean))
				{
					result = false;
				}
				else
				{
					OracleBoolean value = (OracleBoolean)obj;
					if (!this.m_bNotNull && !value.m_bNotNull)
					{
						result = true;
					}
					else if (!this.m_bNotNull || !value.m_bNotNull)
					{
						result = false;
					}
					else
					{
						result = (this == value).m_valueBool;
					}
				}
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x06001576 RID: 5494 RVA: 0x000E73AC File Offset: 0x000E55AC
		public static OracleBoolean Equals(OracleBoolean value1, OracleBoolean value2)
		{
			return value1 == value2;
		}

		// Token: 0x06001577 RID: 5495 RVA: 0x000E73B8 File Offset: 0x000E55B8
		public static OracleBoolean NotEquals(OracleBoolean value1, OracleBoolean value2)
		{
			return value1 != value2;
		}

		// Token: 0x06001578 RID: 5496 RVA: 0x000E73C4 File Offset: 0x000E55C4
		public static OracleBoolean And(OracleBoolean value1, OracleBoolean value2)
		{
			return value1 & value2;
		}

		// Token: 0x06001579 RID: 5497 RVA: 0x000E73D0 File Offset: 0x000E55D0
		public static OracleBoolean Or(OracleBoolean value1, OracleBoolean value2)
		{
			return value1 | value2;
		}

		// Token: 0x0600157A RID: 5498 RVA: 0x000E73DC File Offset: 0x000E55DC
		public static OracleBoolean Xor(OracleBoolean value1, OracleBoolean value2)
		{
			return value1 ^ value2;
		}

		// Token: 0x0600157B RID: 5499 RVA: 0x000E73E8 File Offset: 0x000E55E8
		public static OracleBoolean OnesComplement(OracleBoolean value1)
		{
			return ~value1;
		}

		// Token: 0x0600157C RID: 5500 RVA: 0x000E73F0 File Offset: 0x000E55F0
		public static OracleBoolean GreaterThan(OracleBoolean value1, OracleBoolean value2)
		{
			return value1 > value2;
		}

		// Token: 0x0600157D RID: 5501 RVA: 0x000E73FC File Offset: 0x000E55FC
		public static OracleBoolean GreaterThanOrEquals(OracleBoolean value1, OracleBoolean value2)
		{
			return value1 >= value2;
		}

		// Token: 0x0600157E RID: 5502 RVA: 0x000E7408 File Offset: 0x000E5608
		public static OracleBoolean LessThan(OracleBoolean value1, OracleBoolean value2)
		{
			return value1 < value2;
		}

		// Token: 0x0600157F RID: 5503 RVA: 0x000E7414 File Offset: 0x000E5614
		public static OracleBoolean LessThanOrEquals(OracleBoolean value1, OracleBoolean value2)
		{
			return value1 <= value2;
		}

		// Token: 0x06001580 RID: 5504 RVA: 0x000E7420 File Offset: 0x000E5620
		public static OracleBoolean Parse(string str)
		{
			if (str == null)
			{
				throw new ArgumentNullException("str");
			}
			if (str == "")
			{
				throw new IndexOutOfRangeException();
			}
			return new OracleBoolean(bool.Parse(str));
		}

		// Token: 0x06001581 RID: 5505 RVA: 0x000E7450 File Offset: 0x000E5650
		public override int GetHashCode()
		{
			if (this.m_bNotNull)
			{
				return this.m_valueBool.GetHashCode();
			}
			return 0;
		}

		// Token: 0x06001582 RID: 5506 RVA: 0x000E7468 File Offset: 0x000E5668
		public override string ToString()
		{
			if (!this.m_bNotNull)
			{
				return "null";
			}
			if (this.m_valueBool)
			{
				return "True";
			}
			return "False";
		}

		// Token: 0x06001583 RID: 5507 RVA: 0x000E748C File Offset: 0x000E568C
		public static OracleBoolean operator &(OracleBoolean value1, OracleBoolean value2)
		{
			if (value1.m_bNotNull && value2.m_bNotNull)
			{
				return new OracleBoolean((int)(value1.m_valueByte & value2.m_valueByte));
			}
			return OracleBoolean.Null;
		}

		// Token: 0x06001584 RID: 5508 RVA: 0x000E74BC File Offset: 0x000E56BC
		public static OracleBoolean operator |(OracleBoolean value1, OracleBoolean value2)
		{
			if (value1.m_bNotNull && value2.m_bNotNull)
			{
				return new OracleBoolean((int)(value1.m_valueByte | value2.m_valueByte));
			}
			return OracleBoolean.Null;
		}

		// Token: 0x06001585 RID: 5509 RVA: 0x000E74EC File Offset: 0x000E56EC
		public static OracleBoolean operator ==(OracleBoolean value1, OracleBoolean value2)
		{
			if (value1.m_bNotNull && value2.m_bNotNull)
			{
				return new OracleBoolean(value1.m_valueByte == value2.m_valueByte);
			}
			return OracleBoolean.Null;
		}

		// Token: 0x06001586 RID: 5510 RVA: 0x000E751C File Offset: 0x000E571C
		public static OracleBoolean operator !=(OracleBoolean value1, OracleBoolean value2)
		{
			if (value1.m_bNotNull && value2.m_bNotNull)
			{
				return new OracleBoolean(value1.m_valueByte != value2.m_valueByte);
			}
			return OracleBoolean.Null;
		}

		// Token: 0x06001587 RID: 5511 RVA: 0x000E7550 File Offset: 0x000E5750
		public static OracleBoolean operator ^(OracleBoolean value1, OracleBoolean value2)
		{
			if (value1.m_bNotNull && value2.m_bNotNull)
			{
				return new OracleBoolean((int)(value1.m_valueByte ^ value2.m_valueByte));
			}
			return OracleBoolean.Null;
		}

		// Token: 0x06001588 RID: 5512 RVA: 0x000E7580 File Offset: 0x000E5780
		public static OracleBoolean operator !(OracleBoolean value1)
		{
			if (value1.m_bNotNull)
			{
				return new OracleBoolean(!value1.m_valueBool);
			}
			return OracleBoolean.Null;
		}

		// Token: 0x06001589 RID: 5513 RVA: 0x000E75A0 File Offset: 0x000E57A0
		public static OracleBoolean operator ~(OracleBoolean value1)
		{
			if (value1.m_bNotNull)
			{
				return !value1;
			}
			return OracleBoolean.Null;
		}

		// Token: 0x0600158A RID: 5514 RVA: 0x000E75B8 File Offset: 0x000E57B8
		public static bool operator true(OracleBoolean value1)
		{
			return value1.IsTrue;
		}

		// Token: 0x0600158B RID: 5515 RVA: 0x000E75C4 File Offset: 0x000E57C4
		public static bool operator false(OracleBoolean value1)
		{
			return value1.IsFalse;
		}

		// Token: 0x0600158C RID: 5516 RVA: 0x000E75D0 File Offset: 0x000E57D0
		public static OracleBoolean operator >(OracleBoolean value1, OracleBoolean value2)
		{
			if (value1.m_bNotNull && value2.m_bNotNull)
			{
				return new OracleBoolean(value1.m_valueByte > value2.m_valueByte);
			}
			return OracleBoolean.Null;
		}

		// Token: 0x0600158D RID: 5517 RVA: 0x000E7600 File Offset: 0x000E5800
		public static OracleBoolean operator <(OracleBoolean value1, OracleBoolean value2)
		{
			if (value1.m_bNotNull && value2.m_bNotNull)
			{
				return new OracleBoolean(value1.m_valueByte < value2.m_valueByte);
			}
			return OracleBoolean.Null;
		}

		// Token: 0x0600158E RID: 5518 RVA: 0x000E7630 File Offset: 0x000E5830
		public static OracleBoolean operator >=(OracleBoolean value1, OracleBoolean value2)
		{
			if (value1.m_bNotNull && value2.m_bNotNull)
			{
				return new OracleBoolean(value1.m_valueByte >= value2.m_valueByte);
			}
			return OracleBoolean.Null;
		}

		// Token: 0x0600158F RID: 5519 RVA: 0x000E7664 File Offset: 0x000E5864
		public static OracleBoolean operator <=(OracleBoolean value1, OracleBoolean value2)
		{
			if (value1.m_bNotNull && value2.m_bNotNull)
			{
				return new OracleBoolean(value1.m_valueByte <= value2.m_valueByte);
			}
			return OracleBoolean.Null;
		}

		// Token: 0x06001590 RID: 5520 RVA: 0x000E7698 File Offset: 0x000E5898
		public static implicit operator OracleBoolean(bool value1)
		{
			return new OracleBoolean(value1);
		}

		// Token: 0x06001591 RID: 5521 RVA: 0x000E76A0 File Offset: 0x000E58A0
		public static explicit operator bool(OracleBoolean value1)
		{
			return value1.Value;
		}

		// Token: 0x06001592 RID: 5522 RVA: 0x000E76AC File Offset: 0x000E58AC
		public static explicit operator OracleBoolean(byte value1)
		{
			if (value1 == 0)
			{
				return new OracleBoolean(false);
			}
			return new OracleBoolean(true);
		}

		// Token: 0x06001593 RID: 5523 RVA: 0x000E76C0 File Offset: 0x000E58C0
		public static explicit operator OracleBoolean(decimal value1)
		{
			if (value1 == 0m)
			{
				return new OracleBoolean(false);
			}
			return new OracleBoolean(true);
		}

		// Token: 0x06001594 RID: 5524 RVA: 0x000E76E0 File Offset: 0x000E58E0
		public static explicit operator OracleBoolean(double value1)
		{
			if (value1 == 0.0)
			{
				return new OracleBoolean(false);
			}
			return new OracleBoolean(true);
		}

		// Token: 0x06001595 RID: 5525 RVA: 0x000E76FC File Offset: 0x000E58FC
		public static explicit operator OracleBoolean(short value1)
		{
			if (value1 == 0)
			{
				return new OracleBoolean(false);
			}
			return new OracleBoolean(true);
		}

		// Token: 0x06001596 RID: 5526 RVA: 0x000E7710 File Offset: 0x000E5910
		public static explicit operator OracleBoolean(int value1)
		{
			if (value1 == 0)
			{
				return new OracleBoolean(false);
			}
			return new OracleBoolean(true);
		}

		// Token: 0x06001597 RID: 5527 RVA: 0x000E7724 File Offset: 0x000E5924
		public static explicit operator OracleBoolean(long value1)
		{
			if (value1 == 0L)
			{
				return new OracleBoolean(false);
			}
			return new OracleBoolean(true);
		}

		// Token: 0x06001598 RID: 5528 RVA: 0x000E7738 File Offset: 0x000E5938
		public static explicit operator OracleBoolean(float value1)
		{
			if ((double)value1 == 0.0)
			{
				return new OracleBoolean(false);
			}
			return new OracleBoolean(true);
		}

		// Token: 0x06001599 RID: 5529 RVA: 0x000E7754 File Offset: 0x000E5954
		public static explicit operator OracleBoolean(string value1)
		{
			return OracleBoolean.Parse(value1);
		}

		// Token: 0x040019AE RID: 6574
		public static readonly OracleBoolean False = new OracleBoolean(false);

		// Token: 0x040019AF RID: 6575
		public static readonly OracleBoolean True = new OracleBoolean(true);

		// Token: 0x040019B0 RID: 6576
		public static readonly OracleBoolean Null;

		// Token: 0x040019B1 RID: 6577
		public static readonly OracleBoolean One = new OracleBoolean(1);

		// Token: 0x040019B2 RID: 6578
		public static readonly OracleBoolean Zero = new OracleBoolean(0);

		// Token: 0x040019B3 RID: 6579
		private bool m_valueBool;

		// Token: 0x040019B4 RID: 6580
		private byte m_valueByte;

		// Token: 0x040019B5 RID: 6581
		private bool m_bNotNull;
	}
}
