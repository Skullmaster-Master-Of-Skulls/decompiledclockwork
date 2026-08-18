using System;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Oracle.ManagedDataAccess.Client;
using OracleInternal.Common;

namespace Oracle.ManagedDataAccess.Types
{
	// Token: 0x02000248 RID: 584
	[XmlSchemaProvider("GetXsdType")]
	[Serializable]
	public struct OracleDate : IComparable, IXmlSerializable, INullable
	{
		// Token: 0x060015CE RID: 5582 RVA: 0x000EA42C File Offset: 0x000E862C
		public OracleDate(int year, int month, int day, int hour, int minute, int second)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				if (!TimeStamp.IsValidDateTime(year, month, day, hour, minute, second, 0))
				{
					throw new ArgumentOutOfRangeException();
				}
				this.m_bNotNull = true;
				this.m_byteRepresentation = new byte[7];
				this.m_byteRepresentation[0] = (byte)(year / 100 + 100);
				this.m_byteRepresentation[1] = (byte)(year % 100 + 100);
				this.m_byteRepresentation[2] = (byte)month;
				this.m_byteRepresentation[3] = (byte)day;
				this.m_byteRepresentation[4] = (byte)(hour + 1);
				this.m_byteRepresentation[5] = (byte)(minute + 1);
				this.m_byteRepresentation[6] = (byte)(second + 1);
				this.m_year = year;
				this.m_month = month;
				this.m_day = day;
				this.m_hour = hour;
				this.m_minute = minute;
				this.m_second = second;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
		}

		// Token: 0x060015CF RID: 5583 RVA: 0x000EA54C File Offset: 0x000E874C
		public OracleDate(int year, int month, int day)
		{
			this = new OracleDate(year, month, day, 0, 0, 0);
		}

		// Token: 0x060015D0 RID: 5584 RVA: 0x000EA55C File Offset: 0x000E875C
		public OracleDate(DateTime data)
		{
			this = new OracleDate(data.Year, data.Month, data.Day, data.Hour, data.Minute, data.Second);
		}

		// Token: 0x060015D1 RID: 5585 RVA: 0x000EA590 File Offset: 0x000E8790
		public OracleDate(byte[] binData)
		{
			this = new OracleDate(binData, true, 0, -1);
		}

		// Token: 0x060015D2 RID: 5586 RVA: 0x000EA59C File Offset: 0x000E879C
		public OracleDate(string datStr)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				this.m_year = (this.m_month = (this.m_day = (this.m_hour = (this.m_minute = (this.m_second = 0)))));
				this.m_bNotNull = false;
				this.m_byteRepresentation = null;
				this.FromString(datStr);
				this.m_bNotNull = true;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
		}

		// Token: 0x060015D3 RID: 5587 RVA: 0x000EA660 File Offset: 0x000E8860
		internal OracleDate(byte[] binData, bool bCopyData, int offset = 0, int length = -1)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				if (binData == null)
				{
					throw new ArgumentNullException();
				}
				if (length == -1)
				{
					length = binData.Length;
				}
				if (length != 7)
				{
					throw new ArgumentException();
				}
				this.m_bNotNull = true;
				if (bCopyData)
				{
					this.m_byteRepresentation = new byte[length];
					Array.Copy(binData, offset, this.m_byteRepresentation, 0, length);
				}
				else
				{
					this.m_byteRepresentation = binData;
				}
				this.m_year = OracleDate.GetDateData(this.m_byteRepresentation, 0);
				this.m_month = OracleDate.GetDateData(this.m_byteRepresentation, 1);
				this.m_day = OracleDate.GetDateData(this.m_byteRepresentation, 2);
				this.m_hour = OracleDate.GetDateData(this.m_byteRepresentation, 3) - 1;
				this.m_minute = OracleDate.GetDateData(this.m_byteRepresentation, 4) - 1;
				this.m_second = OracleDate.GetDateData(this.m_byteRepresentation, 5) - 1;
				if (!TimeStamp.IsValidDateTime(this.m_year, this.m_month, this.m_day, this.m_hour, this.m_minute, this.m_second, 0))
				{
					throw new OverflowException();
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Exit, new string[0]);
				}
			}
		}

		// Token: 0x060015D4 RID: 5588 RVA: 0x000EA7D8 File Offset: 0x000E89D8
		public static XmlQualifiedName GetXsdType(XmlSchemaSet schemaSet)
		{
			return new XmlQualifiedName("DateTime", "http://www.w3.org/2001/XMLSchema");
		}

		// Token: 0x060015D5 RID: 5589 RVA: 0x000EA7EC File Offset: 0x000E89EC
		XmlSchema IXmlSerializable.GetSchema()
		{
			return null;
		}

		// Token: 0x060015D6 RID: 5590 RVA: 0x000EA7F0 File Offset: 0x000E89F0
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			string attribute = reader.GetAttribute("null", "http://www.w3.org/2001/XMLSchema-instance");
			if (attribute == null || !XmlConvert.ToBoolean(attribute))
			{
				this.FromString(reader.ReadElementString());
				this.m_bNotNull = true;
				return;
			}
			this.m_bNotNull = false;
		}

		// Token: 0x060015D7 RID: 5591 RVA: 0x000EA834 File Offset: 0x000E8A34
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			if (this.m_bNotNull)
			{
				writer.WriteString(this.ToString());
				return;
			}
			writer.WriteAttributeString("xsi", "null", "http://www.w3.org/2001/XMLSchema-instance", "true");
		}

		// Token: 0x17000376 RID: 886
		// (get) Token: 0x060015D8 RID: 5592 RVA: 0x000EA86C File Offset: 0x000E8A6C
		public byte[] BinData
		{
			get
			{
				if (this.m_bNotNull)
				{
					byte[] array = null;
					if (this.m_byteRepresentation != null)
					{
						array = new byte[7];
						Array.Copy(this.m_byteRepresentation, array, 7);
					}
					return array;
				}
				throw new OracleNullValueException();
			}
		}

		// Token: 0x17000377 RID: 887
		// (get) Token: 0x060015D9 RID: 5593 RVA: 0x000EA8A8 File Offset: 0x000E8AA8
		public bool IsNull
		{
			get
			{
				return !this.m_bNotNull;
			}
		}

		// Token: 0x17000378 RID: 888
		// (get) Token: 0x060015DA RID: 5594 RVA: 0x000EA8B4 File Offset: 0x000E8AB4
		public DateTime Value
		{
			get
			{
				if (this.m_bNotNull)
				{
					return DateTimeConv.GetDateTime(this.m_byteRepresentation, OracleDbType.Date, 0, -1);
				}
				throw new OracleNullValueException();
			}
		}

		// Token: 0x17000379 RID: 889
		// (get) Token: 0x060015DB RID: 5595 RVA: 0x000EA8D4 File Offset: 0x000E8AD4
		public int Year
		{
			get
			{
				if (this.m_bNotNull)
				{
					return OracleDate.GetDateData(this.m_byteRepresentation, 0);
				}
				throw new OracleNullValueException();
			}
		}

		// Token: 0x1700037A RID: 890
		// (get) Token: 0x060015DC RID: 5596 RVA: 0x000EA8F0 File Offset: 0x000E8AF0
		public int Month
		{
			get
			{
				if (this.m_bNotNull)
				{
					return OracleDate.GetDateData(this.m_byteRepresentation, 1);
				}
				throw new OracleNullValueException();
			}
		}

		// Token: 0x1700037B RID: 891
		// (get) Token: 0x060015DD RID: 5597 RVA: 0x000EA90C File Offset: 0x000E8B0C
		public int Day
		{
			get
			{
				if (this.m_bNotNull)
				{
					return OracleDate.GetDateData(this.m_byteRepresentation, 2);
				}
				throw new OracleNullValueException();
			}
		}

		// Token: 0x1700037C RID: 892
		// (get) Token: 0x060015DE RID: 5598 RVA: 0x000EA928 File Offset: 0x000E8B28
		public int Hour
		{
			get
			{
				if (this.m_bNotNull)
				{
					return OracleDate.GetDateData(this.m_byteRepresentation, 3) - 1;
				}
				throw new OracleNullValueException();
			}
		}

		// Token: 0x1700037D RID: 893
		// (get) Token: 0x060015DF RID: 5599 RVA: 0x000EA948 File Offset: 0x000E8B48
		public int Minute
		{
			get
			{
				if (this.m_bNotNull)
				{
					return OracleDate.GetDateData(this.m_byteRepresentation, 4) - 1;
				}
				throw new OracleNullValueException();
			}
		}

		// Token: 0x1700037E RID: 894
		// (get) Token: 0x060015E0 RID: 5600 RVA: 0x000EA968 File Offset: 0x000E8B68
		public int Second
		{
			get
			{
				if (this.m_bNotNull)
				{
					return OracleDate.GetDateData(this.m_byteRepresentation, 5) - 1;
				}
				throw new OracleNullValueException();
			}
		}

		// Token: 0x060015E1 RID: 5601 RVA: 0x000EA988 File Offset: 0x000E8B88
		public override string ToString()
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			string result;
			try
			{
				if (this.m_bNotNull)
				{
					result = DateTimeStringUtilities.ToString(this.m_year, this.m_month, this.m_day, this.m_hour, this.m_minute, this.m_second, 0, null);
				}
				else
				{
					result = "null";
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
				throw;
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

		// Token: 0x060015E2 RID: 5602 RVA: 0x000EAA34 File Offset: 0x000E8C34
		public int CompareTo(object obj)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			int result;
			try
			{
				if (obj == null)
				{
					throw new ArgumentNullException("obj");
				}
				if (obj.GetType() != typeof(OracleDate))
				{
					throw new ArgumentException("obj");
				}
				result = HelperClass.CompareBytes(this.m_byteRepresentation, ((OracleDate)obj).m_byteRepresentation);
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
				throw;
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

		// Token: 0x060015E3 RID: 5603 RVA: 0x000EAAE8 File Offset: 0x000E8CE8
		public override bool Equals(object obj)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			bool result;
			try
			{
				if (obj == null || obj.GetType() != typeof(OracleDate))
				{
					result = false;
				}
				else
				{
					result = (this.CompareTo(obj) == 0);
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
				throw;
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

		// Token: 0x060015E4 RID: 5604 RVA: 0x000EAB80 File Offset: 0x000E8D80
		public override int GetHashCode()
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			int result;
			try
			{
				if (this.m_bNotNull)
				{
					result = this.m_byteRepresentation.GetHashCode();
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

		// Token: 0x060015E5 RID: 5605 RVA: 0x000EABEC File Offset: 0x000E8DEC
		public int GetDaysBetween(OracleDate value1)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			int result;
			try
			{
				if (!this.m_bNotNull || !value1.m_bNotNull)
				{
					throw new OracleNullValueException();
				}
				if (this > value1)
				{
					result = TimeStamp.GetDaysBetweenTimeStamp(this.m_byteRepresentation, value1.m_hour, value1.m_minute, value1.m_second, 0, this.m_year, this.m_month, this.m_day, value1.m_year, value1.m_month, value1.m_day, true, OracleDbType.Date).Days;
				}
				else if (this < value1)
				{
					result = TimeStamp.GetDaysBetweenTimeStamp(value1.InternalByteRepresentation, this.m_hour, this.m_minute, this.m_second, 0, value1.m_year, value1.m_month, value1.m_day, this.m_year, this.m_month, this.m_day, false, OracleDbType.Date).Days;
				}
				else
				{
					result = 0;
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
				throw;
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

		// Token: 0x060015E6 RID: 5606 RVA: 0x000EAD44 File Offset: 0x000E8F44
		public OracleTimeStamp ToOracleTimeStamp()
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleTimeStamp result;
			try
			{
				if (this.m_bNotNull)
				{
					result = new OracleTimeStamp(this.m_year, this.m_month, this.m_day, this.m_hour, this.m_minute, this.m_second);
				}
				else
				{
					result = OracleTimeStamp.Null;
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
				throw;
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

		// Token: 0x060015E7 RID: 5607 RVA: 0x000EADEC File Offset: 0x000E8FEC
		public static bool Equals(OracleDate value1, OracleDate value2)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			bool result;
			try
			{
				CompareNullEnum compareNullEnum = InternalTypes.CompareNull(!value1.m_bNotNull, !value2.m_bNotNull);
				if (compareNullEnum == CompareNullEnum.BothNull)
				{
					result = true;
				}
				else if (compareNullEnum != CompareNullEnum.BothNotNull)
				{
					result = false;
				}
				else
				{
					result = (value1.CompareTo(value2) == 0);
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
				throw;
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

		// Token: 0x060015E8 RID: 5608 RVA: 0x000EAE94 File Offset: 0x000E9094
		public static bool GreaterThan(OracleDate value1, OracleDate value2)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			bool result;
			try
			{
				result = (value1.CompareTo(value2) == 1);
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

		// Token: 0x060015E9 RID: 5609 RVA: 0x000EAEF8 File Offset: 0x000E90F8
		public static bool GreaterThanOrEqual(OracleDate value1, OracleDate value2)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			bool result;
			try
			{
				result = (value1.CompareTo(value2) >= 0);
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

		// Token: 0x060015EA RID: 5610 RVA: 0x000EAF60 File Offset: 0x000E9160
		public static bool LessThan(OracleDate value1, OracleDate value2)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			bool result;
			try
			{
				result = (value1.CompareTo(value2) == -1);
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

		// Token: 0x060015EB RID: 5611 RVA: 0x000EAFC4 File Offset: 0x000E91C4
		public static bool LessThanOrEqual(OracleDate value1, OracleDate value2)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			bool result;
			try
			{
				result = (value1.CompareTo(value2) <= 0);
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

		// Token: 0x060015EC RID: 5612 RVA: 0x000EB02C File Offset: 0x000E922C
		public static bool NotEquals(OracleDate value1, OracleDate value2)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			bool result;
			try
			{
				result = (value1.CompareTo(value2) != 0);
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

		// Token: 0x060015ED RID: 5613 RVA: 0x000EB094 File Offset: 0x000E9294
		public static OracleDate GetSysDate()
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleDate result;
			try
			{
				DateTime now = DateTime.Now;
				result = new OracleDate(now);
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
				throw;
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

		// Token: 0x060015EE RID: 5614 RVA: 0x000EB110 File Offset: 0x000E9310
		public static OracleDate Parse(string datStr)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleDate result;
			try
			{
				result = new OracleDate(datStr);
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
				throw;
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

		// Token: 0x060015EF RID: 5615 RVA: 0x000EB184 File Offset: 0x000E9384
		public static bool operator ==(OracleDate value1, OracleDate value2)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			bool result;
			try
			{
				result = OracleDate.Equals(value1, value2);
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

		// Token: 0x060015F0 RID: 5616 RVA: 0x000EB1E0 File Offset: 0x000E93E0
		public static bool operator >(OracleDate value1, OracleDate value2)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			bool result;
			try
			{
				result = OracleDate.GreaterThan(value1, value2);
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

		// Token: 0x060015F1 RID: 5617 RVA: 0x000EB23C File Offset: 0x000E943C
		public static bool operator >=(OracleDate value1, OracleDate value2)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			bool result;
			try
			{
				result = OracleDate.GreaterThanOrEqual(value1, value2);
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

		// Token: 0x060015F2 RID: 5618 RVA: 0x000EB298 File Offset: 0x000E9498
		public static bool operator <(OracleDate value1, OracleDate value2)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			bool result;
			try
			{
				result = OracleDate.LessThan(value1, value2);
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

		// Token: 0x060015F3 RID: 5619 RVA: 0x000EB2F4 File Offset: 0x000E94F4
		public static bool operator <=(OracleDate value1, OracleDate value2)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			bool result;
			try
			{
				result = OracleDate.LessThanOrEqual(value1, value2);
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

		// Token: 0x060015F4 RID: 5620 RVA: 0x000EB350 File Offset: 0x000E9550
		public static bool operator !=(OracleDate value1, OracleDate value2)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			bool result;
			try
			{
				result = OracleDate.NotEquals(value1, value2);
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

		// Token: 0x060015F5 RID: 5621 RVA: 0x000EB3AC File Offset: 0x000E95AC
		public static explicit operator OracleDate(OracleTimeStamp value1)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleDate result;
			try
			{
				if (!value1.IsNull)
				{
					byte[] byteRepresentation = value1.m_byteRepresentation;
					byte[] array = new byte[7];
					Array.Copy(byteRepresentation, array, 7);
					result = new OracleDate(array, false, 0, -1);
				}
				else
				{
					result = OracleDate.Null;
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
				throw;
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

		// Token: 0x060015F6 RID: 5622 RVA: 0x000EB44C File Offset: 0x000E964C
		public static explicit operator OracleDate(string dateStr)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleDate result;
			try
			{
				result = new OracleDate(dateStr);
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
				throw;
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

		// Token: 0x060015F7 RID: 5623 RVA: 0x000EB4C0 File Offset: 0x000E96C0
		public static explicit operator DateTime(OracleDate value1)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			DateTime dateTime;
			try
			{
				if (!value1.m_bNotNull)
				{
					throw new OracleNullValueException();
				}
				dateTime = DateTimeConv.GetDateTime(value1.m_byteRepresentation, OracleDbType.Date, 0, -1);
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
			return dateTime;
		}

		// Token: 0x060015F8 RID: 5624 RVA: 0x000EB550 File Offset: 0x000E9750
		public static explicit operator OracleDate(DateTime value1)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleDate result;
			try
			{
				result = new OracleDate(value1);
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
				throw;
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

		// Token: 0x060015F9 RID: 5625 RVA: 0x000EB5C4 File Offset: 0x000E97C4
		internal static int GetDateData(byte[] byteRep, byte tsComponent)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			int result;
			try
			{
				int num = 0;
				switch (tsComponent)
				{
				case 0:
					num = (int)((byteRep[0] - 100) * 100 + (byteRep[1] - 100));
					break;
				case 1:
					num = (int)(byteRep[2] & byte.MaxValue);
					break;
				case 2:
					num = (int)(byteRep[3] & byte.MaxValue);
					break;
				case 3:
					num = (int)(byteRep[4] & byte.MaxValue);
					break;
				case 4:
					num = (int)(byteRep[5] & byte.MaxValue);
					break;
				case 5:
					num = (int)(byteRep[6] & byte.MaxValue);
					break;
				}
				result = num;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Exit, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x060015FA RID: 5626 RVA: 0x000EB6A4 File Offset: 0x000E98A4
		internal static byte[] ToBytes(int year, int month, int day, int hour, int minute, int second)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			byte[] result;
			try
			{
				result = new byte[]
				{
					(byte)(year / 100 + 100),
					(byte)(year % 100 + 100),
					(byte)month,
					(byte)day,
					(byte)(hour + 1),
					(byte)(minute + 1),
					(byte)(second + 1)
				};
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Exit, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x060015FB RID: 5627 RVA: 0x000EB754 File Offset: 0x000E9954
		private void FromString(string datStr)
		{
			if (datStr == null)
			{
				throw new ArgumentNullException();
			}
			int num = 0;
			string text = null;
			DateTimeStringUtilities.FromString(datStr, out this.m_year, out this.m_month, out this.m_day, out this.m_hour, out this.m_minute, out this.m_second, out num, out text, true, true);
			this.m_byteRepresentation = OracleDate.ToBytes(this.m_year, this.m_month, this.m_day, this.m_hour, this.m_minute, this.m_second);
		}

		// Token: 0x1700037F RID: 895
		// (get) Token: 0x060015FC RID: 5628 RVA: 0x000EB7D0 File Offset: 0x000E99D0
		internal byte[] InternalByteRepresentation
		{
			get
			{
				return this.m_byteRepresentation;
			}
		}

		// Token: 0x040019C2 RID: 6594
		internal const byte MaxArrSize = 7;

		// Token: 0x040019C3 RID: 6595
		internal const byte YEAR = 0;

		// Token: 0x040019C4 RID: 6596
		internal const byte MONTH = 1;

		// Token: 0x040019C5 RID: 6597
		internal const byte DAY = 2;

		// Token: 0x040019C6 RID: 6598
		internal const byte HOUR = 3;

		// Token: 0x040019C7 RID: 6599
		internal const byte MINUTE = 4;

		// Token: 0x040019C8 RID: 6600
		internal const byte SECOND = 5;

		// Token: 0x040019C9 RID: 6601
		private bool m_bNotNull;

		// Token: 0x040019CA RID: 6602
		internal byte[] m_byteRepresentation;

		// Token: 0x040019CB RID: 6603
		private int m_year;

		// Token: 0x040019CC RID: 6604
		private int m_month;

		// Token: 0x040019CD RID: 6605
		private int m_day;

		// Token: 0x040019CE RID: 6606
		private int m_hour;

		// Token: 0x040019CF RID: 6607
		private int m_minute;

		// Token: 0x040019D0 RID: 6608
		private int m_second;

		// Token: 0x040019D1 RID: 6609
		public static readonly OracleDate MaxValue = new OracleDate(9999, 12, 31, 23, 59, 59);

		// Token: 0x040019D2 RID: 6610
		public static readonly OracleDate MinValue = new OracleDate(-4712, 1, 1, 0, 0, 0);

		// Token: 0x040019D3 RID: 6611
		public static readonly OracleDate Null;
	}
}
