using System;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Oracle.ManagedDataAccess.Client;
using OracleInternal.Common;

namespace Oracle.ManagedDataAccess.Types
{
	// Token: 0x02000253 RID: 595
	[XmlSchemaProvider("GetXsdType")]
	[Serializable]
	public struct OracleTimeStampTZ : IComparable, IXmlSerializable, INullable
	{
		// Token: 0x060017B0 RID: 6064 RVA: 0x000FA01C File Offset: 0x000F821C
		public OracleTimeStampTZ(int year, int month, int day, int hour, int minute, int second, int nanosecond, string timeZone)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				if (!TimeStamp.IsValidDateTime(year, month, day, hour, minute, second, nanosecond))
				{
					throw new ArgumentOutOfRangeException();
				}
				this.m_year = year;
				this.m_month = month;
				this.m_day = day;
				this.m_hour = hour;
				this.m_minute = minute;
				this.m_second = second;
				this.m_nanosecond = nanosecond;
				this.m_hourTZ = 0;
				this.m_minuteTZ = 0;
				this.m_zoneID = -1;
				DateTime? date = null;
				if (this.m_year >= 1800)
				{
					date = new DateTime?(new DateTime(this.m_year, this.m_month, this.m_day, this.m_hour, this.m_minute, this.m_second));
				}
				TimeStamp.ProcessTimeZone(date, ref timeZone, out this.m_hourTZ, out this.m_minuteTZ, out this.m_zoneID);
				this.m_byteRepresentation = TimeStamp.ConstructUTCByteArray(this.m_year, this.m_month, this.m_day, this.m_hour, this.m_minute, this.m_second, this.m_nanosecond, this.m_hourTZ, this.m_minuteTZ, this.m_zoneID);
				this.m_bNotNull = true;
				this.m_fSecondPrec = 9;
				if (this.m_zoneID > 0)
				{
					this.m_timeZone = timeZone;
				}
				else
				{
					this.m_timeZone = OracleTimeStampTZ.ConstructTimeZone(this.m_hourTZ, this.m_minuteTZ);
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
		}

		// Token: 0x060017B1 RID: 6065 RVA: 0x000FA1E4 File Offset: 0x000F83E4
		public OracleTimeStampTZ(int year, int month, int day, int hour, int minute, int second, int nanosecond)
		{
			this = new OracleTimeStampTZ(year, month, day, hour, minute, second, nanosecond, null);
		}

		// Token: 0x060017B2 RID: 6066 RVA: 0x000FA204 File Offset: 0x000F8404
		public OracleTimeStampTZ(int year, int month, int day, int hour, int minute, int second, double millisecond, string timeZone)
		{
			this = new OracleTimeStampTZ(year, month, day, hour, minute, second, (int)(millisecond * 1000000.0), timeZone);
		}

		// Token: 0x060017B3 RID: 6067 RVA: 0x000FA230 File Offset: 0x000F8430
		public OracleTimeStampTZ(int year, int month, int day, int hour, int minute, int second, double millisecond)
		{
			this = new OracleTimeStampTZ(year, month, day, hour, minute, second, millisecond, null);
		}

		// Token: 0x060017B4 RID: 6068 RVA: 0x000FA250 File Offset: 0x000F8450
		public OracleTimeStampTZ(int year, int month, int day, string timeZone)
		{
			this = new OracleTimeStampTZ(year, month, day, 0, 0, 0, 0, timeZone);
		}

		// Token: 0x060017B5 RID: 6069 RVA: 0x000FA26C File Offset: 0x000F846C
		public OracleTimeStampTZ(int year, int month, int day)
		{
			this = new OracleTimeStampTZ(year, month, day, 0, 0, 0, 0);
		}

		// Token: 0x060017B6 RID: 6070 RVA: 0x000FA27C File Offset: 0x000F847C
		public OracleTimeStampTZ(int year, int month, int day, int hour, int minute, int second, string timeZone)
		{
			this = new OracleTimeStampTZ(year, month, day, hour, minute, second, 0, timeZone);
		}

		// Token: 0x060017B7 RID: 6071 RVA: 0x000FA29C File Offset: 0x000F849C
		public OracleTimeStampTZ(int year, int month, int day, int hour, int minute, int second)
		{
			this = new OracleTimeStampTZ(year, month, day, hour, minute, second, 0);
		}

		// Token: 0x060017B8 RID: 6072 RVA: 0x000FA2B0 File Offset: 0x000F84B0
		public OracleTimeStampTZ(DateTime data, string timeZone)
		{
			this = new OracleTimeStampTZ(data.Year, data.Month, data.Day, data.Hour, data.Minute, data.Second, TimeStamp.GetFsecond(data), timeZone);
		}

		// Token: 0x060017B9 RID: 6073 RVA: 0x000FA2F4 File Offset: 0x000F84F4
		public OracleTimeStampTZ(DateTime data)
		{
			this = new OracleTimeStampTZ(data, null);
		}

		// Token: 0x060017BA RID: 6074 RVA: 0x000FA300 File Offset: 0x000F8500
		public OracleTimeStampTZ(byte[] binData)
		{
			this = new OracleTimeStampTZ(binData, true, 0, -1);
		}

		// Token: 0x060017BB RID: 6075 RVA: 0x000FA30C File Offset: 0x000F850C
		public OracleTimeStampTZ(string tsStr)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				this.m_year = (this.m_month = (this.m_day = (this.m_hour = (this.m_minute = (this.m_second = (this.m_nanosecond = (this.m_fSecondPrec = (this.m_hourTZ = (this.m_minuteTZ = 0)))))))));
				this.m_zoneID = -1;
				this.m_timeZone = null;
				this.m_bNotNull = false;
				this.m_byteRepresentation = null;
				this.FromString(tsStr);
				this.m_bNotNull = true;
				this.m_fSecondPrec = 9;
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

		// Token: 0x060017BC RID: 6076 RVA: 0x000FA410 File Offset: 0x000F8610
		internal OracleTimeStampTZ(byte[] binData, bool bCopyData, int dataOffset = 0, int dataLength = -1)
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
				if (dataLength == -1)
				{
					dataLength = binData.Length;
				}
				if (dataLength != 13)
				{
					throw new ArgumentException();
				}
				if (bCopyData)
				{
					this.m_byteRepresentation = new byte[dataLength];
					Array.Copy(binData, dataOffset, this.m_byteRepresentation, 0, dataLength);
				}
				else
				{
					this.m_byteRepresentation = binData;
				}
				TimeStamp.ExtractTimeStampValuesFromByteArray(this.m_byteRepresentation, out this.m_year, out this.m_month, out this.m_day, out this.m_hour, out this.m_minute, out this.m_second, out this.m_nanosecond, out this.m_hourTZ, out this.m_minuteTZ, out this.m_zoneID, TSType.LocalTimeTZ);
				if (!TimeStamp.IsValidDateTime(this.m_year, this.m_month, this.m_day, this.m_hour, this.m_minute, this.m_second, this.m_nanosecond))
				{
					throw new OverflowException();
				}
				this.m_bNotNull = true;
				this.m_fSecondPrec = 9;
				if (this.m_zoneID > 0)
				{
					this.m_timeZone = TimeStamp.GetZoneName(this.m_zoneID);
				}
				else
				{
					this.m_timeZone = OracleTimeStampTZ.ConstructTimeZone(this.m_hourTZ, this.m_minuteTZ);
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

		// Token: 0x060017BD RID: 6077 RVA: 0x000FA5A0 File Offset: 0x000F87A0
		internal OracleTimeStampTZ(byte[] binData, int fSecondPrec)
		{
			this = new OracleTimeStampTZ(binData);
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				int num = this.m_nanosecond;
				this.m_bNotNull = true;
				this.m_fSecondPrec = fSecondPrec;
				double num2 = (double)num / 1000000000.0;
				num2 = Math.Round(num2, fSecondPrec);
				num2 *= 1000000000.0;
				num = Convert.ToInt32(num2);
				if (num < 1000000000)
				{
					this.m_byteRepresentation[7] = (byte)(num >> 24);
					this.m_byteRepresentation[8] = (byte)(num >> 16 & 255);
					this.m_byteRepresentation[9] = (byte)(num >> 8 & 255);
					this.m_byteRepresentation[10] = (byte)(num & 255);
					this.m_nanosecond = num;
				}
				else
				{
					int num3 = this.m_year;
					int num4 = this.m_month;
					int num5 = this.m_day;
					int num6 = this.m_hour;
					int num7 = this.m_minute;
					int num8 = this.m_second;
					int num9 = 1;
					if (num3 < 0)
					{
						num9 = -1;
					}
					if (num > 999999999)
					{
						num = 0;
						num8++;
					}
					if (num8 > 59)
					{
						num8 = 0;
						num7++;
					}
					if (num7 > 59)
					{
						num7 = 0;
						num6++;
					}
					if (num6 > 23)
					{
						num6 = 0;
						num5++;
					}
					if (num5 > DateTime.DaysInMonth((num3 == 0) ? 4 : (num3 * num9), num4))
					{
						num5 = 1;
						num4++;
					}
					if (num4 > 12)
					{
						num4 = 1;
						num3++;
					}
					if (num3 > 9999 || num3 < -4712)
					{
						throw new OverflowException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.YEAR_OUT_OF_RANGE, new string[0]));
					}
					this.m_year = num3;
					this.m_month = num4;
					this.m_day = num5;
					this.m_hour = num6;
					this.m_minute = num7;
					this.m_second = num8;
					this.m_nanosecond = num;
					this.m_byteRepresentation = TimeStamp.ConstructUTCByteArray(this.m_year, this.m_month, this.m_day, this.m_hour, this.m_minute, this.m_second, this.m_nanosecond, this.m_hourTZ, this.m_minuteTZ, this.m_zoneID);
				}
				this.m_bNotNull = true;
				this.m_fSecondPrec = fSecondPrec;
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

		// Token: 0x060017BE RID: 6078 RVA: 0x000FA810 File Offset: 0x000F8A10
		public static XmlQualifiedName GetXsdType(XmlSchemaSet schemaSet)
		{
			return new XmlQualifiedName("DateTime", "http://www.w3.org/2001/XMLSchema");
		}

		// Token: 0x060017BF RID: 6079 RVA: 0x000FA824 File Offset: 0x000F8A24
		XmlSchema IXmlSerializable.GetSchema()
		{
			return null;
		}

		// Token: 0x060017C0 RID: 6080 RVA: 0x000FA828 File Offset: 0x000F8A28
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

		// Token: 0x060017C1 RID: 6081 RVA: 0x000FA86C File Offset: 0x000F8A6C
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			if (this.m_bNotNull)
			{
				writer.WriteString(this.ToString());
				return;
			}
			writer.WriteAttributeString("xsi", "null", "http://www.w3.org/2001/XMLSchema-instance", "true");
		}

		// Token: 0x170003C1 RID: 961
		// (get) Token: 0x060017C2 RID: 6082 RVA: 0x000FA8A4 File Offset: 0x000F8AA4
		public bool IsNull
		{
			get
			{
				return !this.m_bNotNull;
			}
		}

		// Token: 0x170003C2 RID: 962
		// (get) Token: 0x060017C3 RID: 6083 RVA: 0x000FA8B0 File Offset: 0x000F8AB0
		public DateTime Value
		{
			get
			{
				if (this.m_bNotNull)
				{
					return DateTimeConv.GetDateTime(this.m_byteRepresentation, OracleDbType.TimeStampTZ, 0, -1);
				}
				throw new OracleNullValueException();
			}
		}

		// Token: 0x170003C3 RID: 963
		// (get) Token: 0x060017C4 RID: 6084 RVA: 0x000FA8D0 File Offset: 0x000F8AD0
		public byte[] BinData
		{
			get
			{
				if (this.m_bNotNull)
				{
					byte[] array = new byte[13];
					Array.Copy(this.m_byteRepresentation, array, 13);
					return array;
				}
				throw new OracleNullValueException();
			}
		}

		// Token: 0x170003C4 RID: 964
		// (get) Token: 0x060017C5 RID: 6085 RVA: 0x000FA904 File Offset: 0x000F8B04
		public int Year
		{
			get
			{
				if (this.m_bNotNull)
				{
					return this.m_year;
				}
				throw new OracleNullValueException();
			}
		}

		// Token: 0x170003C5 RID: 965
		// (get) Token: 0x060017C6 RID: 6086 RVA: 0x000FA91C File Offset: 0x000F8B1C
		public int Month
		{
			get
			{
				if (this.m_bNotNull)
				{
					return this.m_month;
				}
				throw new OracleNullValueException();
			}
		}

		// Token: 0x170003C6 RID: 966
		// (get) Token: 0x060017C7 RID: 6087 RVA: 0x000FA934 File Offset: 0x000F8B34
		public int Day
		{
			get
			{
				if (this.m_bNotNull)
				{
					return this.m_day;
				}
				throw new OracleNullValueException();
			}
		}

		// Token: 0x170003C7 RID: 967
		// (get) Token: 0x060017C8 RID: 6088 RVA: 0x000FA94C File Offset: 0x000F8B4C
		public int Hour
		{
			get
			{
				if (this.m_bNotNull)
				{
					return this.m_hour;
				}
				throw new OracleNullValueException();
			}
		}

		// Token: 0x170003C8 RID: 968
		// (get) Token: 0x060017C9 RID: 6089 RVA: 0x000FA964 File Offset: 0x000F8B64
		public int Minute
		{
			get
			{
				if (this.m_bNotNull)
				{
					return this.m_minute;
				}
				throw new OracleNullValueException();
			}
		}

		// Token: 0x170003C9 RID: 969
		// (get) Token: 0x060017CA RID: 6090 RVA: 0x000FA97C File Offset: 0x000F8B7C
		public int Second
		{
			get
			{
				if (this.m_bNotNull)
				{
					return this.m_second;
				}
				throw new OracleNullValueException();
			}
		}

		// Token: 0x170003CA RID: 970
		// (get) Token: 0x060017CB RID: 6091 RVA: 0x000FA994 File Offset: 0x000F8B94
		public double Millisecond
		{
			get
			{
				if (this.m_bNotNull)
				{
					return (double)TimeStamp.GetTSTZData(this.m_byteRepresentation, 7) / 1000000.0;
				}
				throw new OracleNullValueException();
			}
		}

		// Token: 0x170003CB RID: 971
		// (get) Token: 0x060017CC RID: 6092 RVA: 0x000FA9BC File Offset: 0x000F8BBC
		public int Nanosecond
		{
			get
			{
				if (this.m_bNotNull)
				{
					return this.m_nanosecond;
				}
				throw new OracleNullValueException();
			}
		}

		// Token: 0x170003CC RID: 972
		// (get) Token: 0x060017CD RID: 6093 RVA: 0x000FA9D4 File Offset: 0x000F8BD4
		public string TimeZone
		{
			get
			{
				if (this.m_bNotNull)
				{
					if (this.m_timeZone == null)
					{
						this.m_timeZone = OracleTimeStampTZ.ConstructTimeZone(TimeStamp.LocalTZOffset.m_tzHours, TimeStamp.LocalTZOffset.m_tzMinutes);
					}
					return this.m_timeZone;
				}
				throw new OracleNullValueException();
			}
		}

		// Token: 0x060017CE RID: 6094 RVA: 0x000FAA14 File Offset: 0x000F8C14
		public OracleTimeStampTZ AddYears(int years)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleTimeStampTZ result;
			try
			{
				if (years < -999999999 || years > 999999999)
				{
					throw new ArgumentOutOfRangeException("years");
				}
				if (!this.m_bNotNull)
				{
					throw new OracleNullValueException();
				}
				int num = this.m_year + years;
				if (num > 9999 || num < -4712)
				{
					throw new OverflowException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.YEAR_OUT_OF_RANGE, new string[0]));
				}
				result = OracleTimeStampTZ.AddYearMonthToTimeStampTZ(this.m_byteRepresentation, years, 0L);
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

		// Token: 0x060017CF RID: 6095 RVA: 0x000FAAE8 File Offset: 0x000F8CE8
		public OracleTimeStampTZ AddMonths(long months)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleTimeStampTZ result;
			try
			{
				if (months <= -12000000000L || months >= 12000000000L)
				{
					throw new ArgumentOutOfRangeException("months");
				}
				if (!this.m_bNotNull)
				{
					throw new OracleNullValueException();
				}
				result = OracleTimeStampTZ.AddYearMonthToTimeStampTZ(this.m_byteRepresentation, 0, months);
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

		// Token: 0x060017D0 RID: 6096 RVA: 0x000FAB94 File Offset: 0x000F8D94
		public OracleTimeStampTZ AddDays(double days)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleTimeStampTZ result;
			try
			{
				if (days <= -1000000000.0 || days >= 1000000000.0)
				{
					throw new ArgumentOutOfRangeException("days");
				}
				if (!this.m_bNotNull)
				{
					throw new OracleNullValueException();
				}
				result = OracleTimeStampTZ.AddDayTimeToTimeStampTZ(this.m_byteRepresentation, days, 2);
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

		// Token: 0x060017D1 RID: 6097 RVA: 0x000FAC48 File Offset: 0x000F8E48
		public OracleTimeStampTZ AddHours(double hours)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleTimeStampTZ result;
			try
			{
				if (hours <= -24000000000.0 || hours >= 24000000000.0)
				{
					throw new ArgumentOutOfRangeException("hours");
				}
				if (!this.m_bNotNull)
				{
					throw new OracleNullValueException();
				}
				result = OracleTimeStampTZ.AddDayTimeToTimeStampTZ(this.m_byteRepresentation, hours, 3);
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

		// Token: 0x060017D2 RID: 6098 RVA: 0x000FACFC File Offset: 0x000F8EFC
		public OracleTimeStampTZ AddMinutes(double minutes)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleTimeStampTZ result;
			try
			{
				if (minutes <= -1440000000000.0 || minutes >= 1440000000000.0)
				{
					throw new ArgumentOutOfRangeException("minutes");
				}
				if (!this.m_bNotNull)
				{
					throw new OracleNullValueException();
				}
				result = OracleTimeStampTZ.AddDayTimeToTimeStampTZ(this.m_byteRepresentation, minutes, 4);
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

		// Token: 0x060017D3 RID: 6099 RVA: 0x000FADB0 File Offset: 0x000F8FB0
		public OracleTimeStampTZ AddSeconds(double seconds)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleTimeStampTZ result;
			try
			{
				if (seconds <= -86400000000000.0 || seconds >= 86400000000000.0)
				{
					throw new ArgumentOutOfRangeException("seconds");
				}
				if (!this.m_bNotNull)
				{
					throw new OracleNullValueException();
				}
				result = OracleTimeStampTZ.AddDayTimeToTimeStampTZ(this.m_byteRepresentation, seconds, 5);
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

		// Token: 0x060017D4 RID: 6100 RVA: 0x000FAE64 File Offset: 0x000F9064
		public OracleTimeStampTZ AddMilliseconds(double milliseconds)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleTimeStampTZ result;
			try
			{
				if (milliseconds <= -86400000000000000.0 || milliseconds >= 86400000000000000.0)
				{
					throw new ArgumentOutOfRangeException("milliseconds");
				}
				if (!this.m_bNotNull)
				{
					throw new OracleNullValueException();
				}
				long num = (long)(milliseconds * 1000000.0);
				result = OracleTimeStampTZ.AddDayTimeToTimeStampTZ(this.m_byteRepresentation, num, 7);
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

		// Token: 0x060017D5 RID: 6101 RVA: 0x000FAF24 File Offset: 0x000F9124
		public OracleTimeStampTZ AddNanoseconds(long nanoseconds)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleTimeStampTZ result;
			try
			{
				if (!this.m_bNotNull)
				{
					throw new OracleNullValueException();
				}
				result = OracleTimeStampTZ.AddDayTimeToTimeStampTZ(this.m_byteRepresentation, nanoseconds, 7);
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

		// Token: 0x060017D6 RID: 6102 RVA: 0x000FAFB4 File Offset: 0x000F91B4
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
				if (obj.GetType() != typeof(OracleTimeStampTZ))
				{
					throw new ArgumentException("obj");
				}
				result = HelperClass.CompareBytes(this.ToUniversalTime().m_byteRepresentation, ((OracleTimeStampTZ)obj).ToUniversalTime().m_byteRepresentation);
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

		// Token: 0x060017D7 RID: 6103 RVA: 0x000FB078 File Offset: 0x000F9278
		public override bool Equals(object obj)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			bool result;
			try
			{
				if (obj == null || obj.GetType() != typeof(OracleTimeStampTZ))
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

		// Token: 0x060017D8 RID: 6104 RVA: 0x000FB110 File Offset: 0x000F9310
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

		// Token: 0x060017D9 RID: 6105 RVA: 0x000FB17C File Offset: 0x000F937C
		public OracleIntervalDS GetDaysBetween(OracleTimeStampTZ value)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleIntervalDS result;
			try
			{
				if (this.m_bNotNull && value.m_bNotNull)
				{
					OracleTimeStampTZ oracleTimeStampTZ = new OracleTimeStampTZ(this.m_byteRepresentation, true, 0, -1);
					OracleTimeStampTZ value2;
					OracleTimeStampTZ value3;
					if (value.TimeZone == oracleTimeStampTZ.TimeZone)
					{
						value2 = oracleTimeStampTZ;
						value3 = value;
					}
					else
					{
						value2 = oracleTimeStampTZ.ToUniversalTime();
						value3 = value.ToUniversalTime();
					}
					if (value2 > value3)
					{
						result = TimeStamp.GetDaysBetweenTimeStamp(value2.InternalByteRepresentation, value3.m_hour, value3.m_minute, value3.m_second, value3.m_nanosecond, value2.m_year, value2.m_month, value2.m_day, value3.m_year, value3.m_month, value3.m_day, true, OracleDbType.TimeStampTZ);
					}
					else if (value2 < value3)
					{
						result = TimeStamp.GetDaysBetweenTimeStamp(value3.InternalByteRepresentation, value2.m_hour, value2.m_minute, value2.m_second, value2.m_nanosecond, value3.m_year, value3.m_month, value3.m_day, value2.m_year, value2.m_month, value2.m_day, false, OracleDbType.TimeStampTZ);
					}
					else
					{
						result = new OracleIntervalDS(0, 0, 0, 0, 0);
					}
				}
				else
				{
					result = OracleIntervalDS.Null;
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

		// Token: 0x060017DA RID: 6106 RVA: 0x000FB330 File Offset: 0x000F9530
		public OracleIntervalYM GetYearsBetween(OracleTimeStampTZ value)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleIntervalYM result;
			try
			{
				if (this.m_bNotNull && value.m_bNotNull)
				{
					OracleTimeStampTZ oracleTimeStampTZ = new OracleTimeStampTZ(this.m_byteRepresentation, true, 0, -1);
					OracleTimeStampTZ oracleTimeStampTZ2;
					OracleTimeStampTZ value2;
					if (value.TimeZone == oracleTimeStampTZ.TimeZone)
					{
						oracleTimeStampTZ2 = oracleTimeStampTZ;
						value2 = value;
					}
					else
					{
						oracleTimeStampTZ2 = oracleTimeStampTZ.ToUniversalTime();
						value2 = value.ToUniversalTime();
					}
					if (this < value2)
					{
						int sumDay = value2.Day - oracleTimeStampTZ2.m_day;
						int sumMonth = value2.Month - oracleTimeStampTZ2.m_month;
						int sumYear = value2.Year - oracleTimeStampTZ2.m_year;
						result = TimeStamp.GetYearsBetweenTimeStamp(sumDay, sumMonth, sumYear, value2.m_year, false);
					}
					else if (this > value2)
					{
						int sumDay = oracleTimeStampTZ2.m_day - value2.Day;
						int sumMonth = oracleTimeStampTZ2.m_month - value2.Month;
						int sumYear = oracleTimeStampTZ2.m_year - value2.Year;
						result = TimeStamp.GetYearsBetweenTimeStamp(sumDay, sumMonth, sumYear, oracleTimeStampTZ2.m_year, true);
					}
					else
					{
						result = new OracleIntervalYM(0, 0);
					}
				}
				else
				{
					result = OracleIntervalYM.Null;
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

		// Token: 0x060017DB RID: 6107 RVA: 0x000FB4D4 File Offset: 0x000F96D4
		public TimeSpan GetTimeZoneOffset()
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			TimeSpan result;
			try
			{
				if (!this.m_bNotNull)
				{
					throw new OracleNullValueException();
				}
				int tstzdata = TimeStamp.GetTSTZData(this.m_byteRepresentation, 8);
				int tstzdata2 = TimeStamp.GetTSTZData(this.m_byteRepresentation, 9);
				result = new TimeSpan(tstzdata, tstzdata2, 0);
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

		// Token: 0x060017DC RID: 6108 RVA: 0x000FB574 File Offset: 0x000F9774
		public OracleTimeStampLTZ ToLocalTime()
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleTimeStampLTZ result;
			try
			{
				if (this.m_bNotNull)
				{
					result = TimeStamp.ToLocalTime(this.m_year, this.m_month, this.m_day, this.m_hour, this.m_minute, this.m_second, this.m_nanosecond, this.m_hourTZ, this.m_minuteTZ);
				}
				else
				{
					result = OracleTimeStampLTZ.Null;
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

		// Token: 0x060017DD RID: 6109 RVA: 0x000FB630 File Offset: 0x000F9830
		public OracleDate ToOracleDate()
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleDate result;
			try
			{
				if (this.m_bNotNull)
				{
					result = new OracleDate(this.m_year, this.m_month, this.m_day, this.m_hour, this.m_minute, this.m_second);
				}
				else
				{
					result = OracleDate.Null;
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

		// Token: 0x060017DE RID: 6110 RVA: 0x000FB6BC File Offset: 0x000F98BC
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
					result = new OracleTimeStamp(this.m_year, this.m_month, this.m_day, this.m_hour, this.m_minute, this.m_second, this.m_nanosecond);
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

		// Token: 0x060017DF RID: 6111 RVA: 0x000FB76C File Offset: 0x000F996C
		public OracleTimeStampLTZ ToOracleTimeStampLTZ()
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleTimeStampLTZ result;
			try
			{
				if (this.m_bNotNull)
				{
					result = TimeStamp.ToLocalTime(this.m_year, this.m_month, this.m_day, this.m_hour, this.m_minute, this.m_second, this.m_nanosecond, this.m_hourTZ, this.m_minuteTZ);
				}
				else
				{
					result = OracleTimeStampLTZ.Null;
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

		// Token: 0x060017E0 RID: 6112 RVA: 0x000FB828 File Offset: 0x000F9A28
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
					result = DateTimeStringUtilities.ToString(this.m_year, this.m_month, this.m_day, this.m_hour, this.m_minute, this.m_second, this.m_nanosecond, this.m_timeZone);
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

		// Token: 0x060017E1 RID: 6113 RVA: 0x000FB8DC File Offset: 0x000F9ADC
		public OracleTimeStampTZ ToUniversalTime()
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleTimeStampTZ result;
			try
			{
				if (this.m_bNotNull)
				{
					int year = this.m_year;
					int month = this.m_month;
					int day = this.m_day;
					int hour = this.m_hour;
					int minute = this.m_minute;
					int second = this.m_second;
					int nanosecond = this.m_nanosecond;
					int hourTZ = this.m_hourTZ;
					int minuteTZ = this.m_minuteTZ;
					TimeStamp.ToUniversalTime(ref year, ref month, ref day, ref hour, ref minute, ref second, ref nanosecond, ref hourTZ, ref minuteTZ);
					result = new OracleTimeStampTZ(year, month, day, hour, minute, second, nanosecond, "00:00");
				}
				else
				{
					result = OracleTimeStampTZ.Null;
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

		// Token: 0x060017E2 RID: 6114 RVA: 0x000FB9D0 File Offset: 0x000F9BD0
		public static bool Equals(OracleTimeStampTZ value1, OracleTimeStampTZ value2)
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
					result = (value1.ToUniversalTime().CompareTo(value2.ToUniversalTime()) == 0);
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

		// Token: 0x060017E3 RID: 6115 RVA: 0x000FBA88 File Offset: 0x000F9C88
		public static bool GreaterThan(OracleTimeStampTZ value1, OracleTimeStampTZ value2)
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

		// Token: 0x060017E4 RID: 6116 RVA: 0x000FBB08 File Offset: 0x000F9D08
		public static bool GreaterThanOrEqual(OracleTimeStampTZ value1, OracleTimeStampTZ value2)
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

		// Token: 0x060017E5 RID: 6117 RVA: 0x000FBB8C File Offset: 0x000F9D8C
		public static bool LessThan(OracleTimeStampTZ value1, OracleTimeStampTZ value2)
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

		// Token: 0x060017E6 RID: 6118 RVA: 0x000FBC0C File Offset: 0x000F9E0C
		public static bool LessThanOrEqual(OracleTimeStampTZ value1, OracleTimeStampTZ value2)
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

		// Token: 0x060017E7 RID: 6119 RVA: 0x000FBC90 File Offset: 0x000F9E90
		public static bool NotEquals(OracleTimeStampTZ value1, OracleTimeStampTZ value2)
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

		// Token: 0x060017E8 RID: 6120 RVA: 0x000FBD14 File Offset: 0x000F9F14
		public static OracleTimeStampTZ GetSysDate()
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleTimeStampTZ result;
			try
			{
				DateTimeOffset now = DateTimeOffset.Now;
				result = new OracleTimeStampTZ(now.DateTime, OracleTimeStampTZ.ConstructTimeZone(now.Offset.Hours, now.Offset.Minutes));
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

		// Token: 0x060017E9 RID: 6121 RVA: 0x000FBDB8 File Offset: 0x000F9FB8
		public static OracleTimeStampTZ Parse(string tsStr)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleTimeStampTZ result;
			try
			{
				result = new OracleTimeStampTZ(tsStr);
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

		// Token: 0x060017EA RID: 6122 RVA: 0x000FBE2C File Offset: 0x000FA02C
		public static OracleTimeStampTZ SetPrecision(OracleTimeStampTZ value1, int fracSecPrecision)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleTimeStampTZ result;
			try
			{
				if (!value1.m_bNotNull)
				{
					throw new OracleNullValueException();
				}
				if (fracSecPrecision < 0 || fracSecPrecision > 9)
				{
					throw new ArgumentOutOfRangeException("fracSecPrecision");
				}
				result = new OracleTimeStampTZ(value1.InternalByteRepresentation, fracSecPrecision);
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

		// Token: 0x060017EB RID: 6123 RVA: 0x000FBECC File Offset: 0x000FA0CC
		public static bool operator ==(OracleTimeStampTZ value1, OracleTimeStampTZ value2)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			bool result;
			try
			{
				result = OracleTimeStampTZ.Equals(value1, value2);
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

		// Token: 0x060017EC RID: 6124 RVA: 0x000FBF28 File Offset: 0x000FA128
		public static bool operator >(OracleTimeStampTZ value1, OracleTimeStampTZ value2)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			bool result;
			try
			{
				result = OracleTimeStampTZ.GreaterThan(value1, value2);
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

		// Token: 0x060017ED RID: 6125 RVA: 0x000FBF84 File Offset: 0x000FA184
		public static bool operator >=(OracleTimeStampTZ value1, OracleTimeStampTZ value2)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			bool result;
			try
			{
				result = OracleTimeStampTZ.GreaterThanOrEqual(value1, value2);
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

		// Token: 0x060017EE RID: 6126 RVA: 0x000FBFE0 File Offset: 0x000FA1E0
		public static bool operator <(OracleTimeStampTZ value1, OracleTimeStampTZ value2)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			bool result;
			try
			{
				result = OracleTimeStampTZ.LessThan(value1, value2);
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

		// Token: 0x060017EF RID: 6127 RVA: 0x000FC03C File Offset: 0x000FA23C
		public static bool operator <=(OracleTimeStampTZ value1, OracleTimeStampTZ value2)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			bool result;
			try
			{
				result = OracleTimeStampTZ.LessThanOrEqual(value1, value2);
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

		// Token: 0x060017F0 RID: 6128 RVA: 0x000FC098 File Offset: 0x000FA298
		public static bool operator !=(OracleTimeStampTZ value1, OracleTimeStampTZ value2)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			bool result;
			try
			{
				result = OracleTimeStampTZ.NotEquals(value1, value2);
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

		// Token: 0x060017F1 RID: 6129 RVA: 0x000FC110 File Offset: 0x000FA310
		public static OracleTimeStampTZ operator +(OracleTimeStampTZ value1, OracleIntervalDS value2)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleTimeStampTZ result;
			try
			{
				if (value1.IsNull || value2.IsNull)
				{
					result = OracleTimeStampTZ.Null;
				}
				else
				{
					result = OracleTimeStampTZ.AddDayTimeToTimeStampTZ(value1.m_byteRepresentation, value2, 11);
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

		// Token: 0x060017F2 RID: 6130 RVA: 0x000FC1AC File Offset: 0x000FA3AC
		public static OracleTimeStampTZ operator +(OracleTimeStampTZ value1, OracleIntervalYM value2)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleTimeStampTZ result;
			try
			{
				if (value1.IsNull || value2.IsNull)
				{
					result = OracleTimeStampTZ.Null;
				}
				else
				{
					result = OracleTimeStampTZ.AddYearMonthToTimeStampTZ(value1.InternalByteRepresentation, value2.Years, (long)value2.Months);
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

		// Token: 0x060017F3 RID: 6131 RVA: 0x000FC250 File Offset: 0x000FA450
		public static OracleTimeStampTZ operator +(OracleTimeStampTZ value1, TimeSpan value2)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleTimeStampTZ result;
			try
			{
				if (value1.IsNull)
				{
					result = OracleTimeStampTZ.Null;
				}
				else
				{
					result = OracleTimeStampTZ.AddDayTimeToTimeStampTZ(value1.m_byteRepresentation, value2, 10);
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

		// Token: 0x060017F4 RID: 6132 RVA: 0x000FC2E4 File Offset: 0x000FA4E4
		public static OracleTimeStampTZ operator -(OracleTimeStampTZ value1, OracleIntervalDS value2)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleTimeStampTZ result;
			try
			{
				if (value1.IsNull || value2.IsNull)
				{
					result = OracleTimeStampTZ.Null;
				}
				else
				{
					result = OracleTimeStampTZ.AddDayTimeToTimeStampTZ(value1.m_byteRepresentation, -value2, 11);
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

		// Token: 0x060017F5 RID: 6133 RVA: 0x000FC388 File Offset: 0x000FA588
		public static OracleTimeStampTZ operator -(OracleTimeStampTZ value1, OracleIntervalYM value2)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleTimeStampTZ result;
			try
			{
				if (value1.IsNull || value2.IsNull)
				{
					result = OracleTimeStampTZ.Null;
				}
				else
				{
					result = OracleTimeStampTZ.AddYearMonthToTimeStampTZ(value1.InternalByteRepresentation, -value2.Years, -(long)value2.Months);
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

		// Token: 0x060017F6 RID: 6134 RVA: 0x000FC430 File Offset: 0x000FA630
		public static OracleTimeStampTZ operator -(OracleTimeStampTZ value1, TimeSpan value2)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleTimeStampTZ result;
			try
			{
				if (value1.IsNull)
				{
					result = OracleTimeStampTZ.Null;
				}
				else
				{
					result = OracleTimeStampTZ.AddDayTimeToTimeStampTZ(value1.m_byteRepresentation, -value2, 10);
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

		// Token: 0x060017F7 RID: 6135 RVA: 0x000FC4C8 File Offset: 0x000FA6C8
		public static explicit operator OracleTimeStampTZ(string tsStr)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleTimeStampTZ result;
			try
			{
				result = new OracleTimeStampTZ(tsStr);
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

		// Token: 0x060017F8 RID: 6136 RVA: 0x000FC53C File Offset: 0x000FA73C
		public static explicit operator DateTime(OracleTimeStampTZ value1)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			DateTime value2;
			try
			{
				if (!value1.m_bNotNull)
				{
					throw new OracleNullValueException();
				}
				value2 = value1.Value;
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
			return value2;
		}

		// Token: 0x060017F9 RID: 6137 RVA: 0x000FC5C0 File Offset: 0x000FA7C0
		public static implicit operator OracleTimeStampTZ(OracleDate value1)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleTimeStampTZ result;
			try
			{
				if (!value1.IsNull)
				{
					result = new OracleTimeStampTZ(value1.Year, value1.Month, value1.Day, value1.Hour, value1.Minute, value1.Second);
				}
				else
				{
					result = OracleTimeStampTZ.Null;
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

		// Token: 0x060017FA RID: 6138 RVA: 0x000FC670 File Offset: 0x000FA870
		public static explicit operator OracleTimeStampTZ(OracleTimeStamp value1)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleTimeStampTZ result;
			try
			{
				if (!value1.IsNull)
				{
					result = new OracleTimeStampTZ(value1.Year, value1.Month, value1.Day, value1.Hour, value1.Minute, value1.Second, value1.Nanosecond);
				}
				else
				{
					result = OracleTimeStampTZ.Null;
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

		// Token: 0x060017FB RID: 6139 RVA: 0x000FC728 File Offset: 0x000FA928
		public static explicit operator OracleTimeStampTZ(OracleTimeStampLTZ value1)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleTimeStampTZ result;
			try
			{
				if (!value1.IsNull)
				{
					result = new OracleTimeStampTZ(value1.Year, value1.Month, value1.Day, value1.Hour, value1.Minute, value1.Second, value1.Nanosecond);
				}
				else
				{
					result = OracleTimeStampTZ.Null;
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

		// Token: 0x060017FC RID: 6140 RVA: 0x000FC7E0 File Offset: 0x000FA9E0
		public static implicit operator OracleTimeStampTZ(DateTime value1)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleTimeStampTZ result;
			try
			{
				result = new OracleTimeStampTZ(value1, null);
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

		// Token: 0x060017FD RID: 6141 RVA: 0x000FC858 File Offset: 0x000FAA58
		internal static string ConstructTimeZone(int hoursTZ, int minutesTZ)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			string result;
			try
			{
				bool flag = true;
				if (hoursTZ < 0)
				{
					flag = false;
					hoursTZ = -hoursTZ;
				}
				if (minutesTZ < 0)
				{
					flag = false;
					minutesTZ = -minutesTZ;
				}
				StringBuilder stringBuilder = new StringBuilder(15);
				string text = hoursTZ.ToString();
				string text2 = minutesTZ.ToString();
				if (flag)
				{
					stringBuilder.Append("+");
				}
				else
				{
					stringBuilder.Append("-");
				}
				if (hoursTZ <= 9)
				{
					text = "0" + text;
				}
				if (minutesTZ <= 9)
				{
					text2 = "0" + text2;
				}
				stringBuilder.Append(text);
				stringBuilder.Append(":");
				stringBuilder.Append(text2);
				result = stringBuilder.ToString();
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

		// Token: 0x060017FE RID: 6142 RVA: 0x000FC95C File Offset: 0x000FAB5C
		internal static OracleTimeStampTZ AddDayTimeToTimeStampTZ(byte[] byteRepresentation, object value, byte tsComponent)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			OracleTimeStampTZ result;
			try
			{
				byte[] binData = TimeStamp.AddDayTimeToTimeStamp(byteRepresentation, value, tsComponent, OracleDbType.TimeStampTZ);
				result = new OracleTimeStampTZ(binData, false, 0, -1);
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

		// Token: 0x060017FF RID: 6143 RVA: 0x000FC9E0 File Offset: 0x000FABE0
		internal static OracleTimeStampTZ AddYearMonthToTimeStampTZ(byte[] byteRepresentation, int yearToBeAdded, long monthToBeAdded)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			OracleTimeStampTZ result;
			try
			{
				byte[] binData = TimeStamp.AddYearMonthToTimeStamp(byteRepresentation, yearToBeAdded, monthToBeAdded, OracleDbType.TimeStampTZ);
				result = new OracleTimeStampTZ(binData, false, 0, -1);
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

		// Token: 0x06001800 RID: 6144 RVA: 0x000FCA64 File Offset: 0x000FAC64
		private void FromString(string tsStr)
		{
			if (tsStr == null)
			{
				throw new ArgumentNullException();
			}
			string timeZone = null;
			DateTimeStringUtilities.FromString(tsStr, out this.m_year, out this.m_month, out this.m_day, out this.m_hour, out this.m_minute, out this.m_second, out this.m_nanosecond, out timeZone, false, false);
			DateTime? date = null;
			if (this.m_year >= 1800)
			{
				date = new DateTime?(new DateTime((this.m_year < 0) ? 1 : this.m_year, this.m_month, this.m_day, this.m_hour, this.m_minute, this.m_second));
			}
			TimeStamp.ProcessTimeZone(date, ref timeZone, out this.m_hourTZ, out this.m_minuteTZ, out this.m_zoneID);
			if (this.m_zoneID > 0)
			{
				this.m_timeZone = timeZone;
			}
			else
			{
				this.m_timeZone = OracleTimeStampTZ.ConstructTimeZone(this.m_hourTZ, this.m_minuteTZ);
			}
			this.m_byteRepresentation = TimeStamp.ConstructUTCByteArray(this.m_year, this.m_month, this.m_day, this.m_hour, this.m_minute, this.m_second, this.m_nanosecond, this.m_hourTZ, this.m_minuteTZ, this.m_zoneID);
		}

		// Token: 0x170003CD RID: 973
		// (get) Token: 0x06001801 RID: 6145 RVA: 0x000FCB8C File Offset: 0x000FAD8C
		internal byte[] InternalByteRepresentation
		{
			get
			{
				return this.m_byteRepresentation;
			}
		}

		// Token: 0x04001A4C RID: 6732
		internal const byte MaxArrSize = 13;

		// Token: 0x04001A4D RID: 6733
		internal byte[] m_byteRepresentation;

		// Token: 0x04001A4E RID: 6734
		private bool m_bNotNull;

		// Token: 0x04001A4F RID: 6735
		private int m_fSecondPrec;

		// Token: 0x04001A50 RID: 6736
		private string m_timeZone;

		// Token: 0x04001A51 RID: 6737
		private int m_year;

		// Token: 0x04001A52 RID: 6738
		private int m_month;

		// Token: 0x04001A53 RID: 6739
		private int m_day;

		// Token: 0x04001A54 RID: 6740
		private int m_hour;

		// Token: 0x04001A55 RID: 6741
		private int m_minute;

		// Token: 0x04001A56 RID: 6742
		private int m_second;

		// Token: 0x04001A57 RID: 6743
		private int m_nanosecond;

		// Token: 0x04001A58 RID: 6744
		private int m_hourTZ;

		// Token: 0x04001A59 RID: 6745
		private int m_minuteTZ;

		// Token: 0x04001A5A RID: 6746
		private int m_zoneID;

		// Token: 0x04001A5B RID: 6747
		public static readonly OracleTimeStampTZ MaxValue = new OracleTimeStampTZ(9999, 12, 31, 23, 59, 59, 999999999, "00:00");

		// Token: 0x04001A5C RID: 6748
		public static readonly OracleTimeStampTZ MinValue = new OracleTimeStampTZ(-4712, 1, 1, 0, 0, 0, 0, "00:00");

		// Token: 0x04001A5D RID: 6749
		public static readonly OracleTimeStampTZ Null;
	}
}
