using System;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Oracle.ManagedDataAccess.Client;
using OracleInternal.Common;

namespace Oracle.ManagedDataAccess.Types
{
	// Token: 0x02000252 RID: 594
	[XmlSchemaProvider("GetXsdType")]
	[Serializable]
	public struct OracleTimeStampLTZ : IComparable, IXmlSerializable, INullable
	{
		// Token: 0x06001763 RID: 5987 RVA: 0x000F78F4 File Offset: 0x000F5AF4
		public OracleTimeStampLTZ(int year, int month, int day, int hour, int minute, int second, int nanosecond)
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
				this.m_byteRepresentation = TimeStamp.ConstructByteArrayTS(year, month, day, hour, minute, second, nanosecond);
				this.m_year = year;
				this.m_month = month;
				this.m_day = day;
				this.m_hour = hour;
				this.m_minute = minute;
				this.m_second = second;
				this.m_nanosecond = nanosecond;
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

		// Token: 0x06001764 RID: 5988 RVA: 0x000F79D4 File Offset: 0x000F5BD4
		public OracleTimeStampLTZ(int year, int month, int day, int hour, int minute, int second, double millisecond)
		{
			this = new OracleTimeStampLTZ(year, month, day, hour, minute, second, (int)(millisecond * 1000000.0));
		}

		// Token: 0x06001765 RID: 5989 RVA: 0x000F7A00 File Offset: 0x000F5C00
		public OracleTimeStampLTZ(int year, int month, int day)
		{
			this = new OracleTimeStampLTZ(year, month, day, 0, 0, 0, 0);
		}

		// Token: 0x06001766 RID: 5990 RVA: 0x000F7A10 File Offset: 0x000F5C10
		public OracleTimeStampLTZ(int year, int month, int day, int hour, int minute, int second)
		{
			this = new OracleTimeStampLTZ(year, month, day, hour, minute, second, 0);
		}

		// Token: 0x06001767 RID: 5991 RVA: 0x000F7A24 File Offset: 0x000F5C24
		public OracleTimeStampLTZ(DateTime data)
		{
			this = new OracleTimeStampLTZ(data.Year, data.Month, data.Day, data.Hour, data.Minute, data.Second, TimeStamp.GetFsecond(data));
		}

		// Token: 0x06001768 RID: 5992 RVA: 0x000F7A5C File Offset: 0x000F5C5C
		public OracleTimeStampLTZ(byte[] binData)
		{
			this = new OracleTimeStampLTZ(binData, true);
		}

		// Token: 0x06001769 RID: 5993 RVA: 0x000F7A68 File Offset: 0x000F5C68
		public OracleTimeStampLTZ(string tsStr)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				this.m_year = (this.m_month = (this.m_day = (this.m_hour = (this.m_minute = (this.m_second = (this.m_nanosecond = (this.m_fSecondPrec = 0)))))));
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

		// Token: 0x0600176A RID: 5994 RVA: 0x000F7B48 File Offset: 0x000F5D48
		internal OracleTimeStampLTZ(byte[] binData, int fSecondPrec)
		{
			this = new OracleTimeStampLTZ(binData);
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
					this.m_byteRepresentation = TimeStamp.ConstructByteArrayTS(num3, num4, num5, num6, num7, num8, num);
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

		// Token: 0x0600176B RID: 5995 RVA: 0x000F7D78 File Offset: 0x000F5F78
		internal OracleTimeStampLTZ(byte[] binData, bool bCopyData)
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
				if (binData.Length != 11)
				{
					throw new ArgumentException();
				}
				this.m_bNotNull = true;
				this.m_fSecondPrec = 9;
				if (bCopyData)
				{
					int num = binData.Length;
					this.m_byteRepresentation = new byte[num];
					Array.Copy(binData, this.m_byteRepresentation, num);
				}
				else
				{
					this.m_byteRepresentation = binData;
				}
				int num2;
				int num3;
				int num4;
				TimeStamp.ExtractTimeStampValuesFromByteArray(this.m_byteRepresentation, out this.m_year, out this.m_month, out this.m_day, out this.m_hour, out this.m_minute, out this.m_second, out this.m_nanosecond, out num2, out num3, out num4, TSType.TimeStampLTZ);
				if (!TimeStamp.IsValidDateTime(this.m_year, this.m_month, this.m_day, this.m_hour, this.m_minute, this.m_second, this.m_nanosecond))
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

		// Token: 0x0600176C RID: 5996 RVA: 0x000F7EAC File Offset: 0x000F60AC
		internal OracleTimeStampLTZ(byte[] binData, int dataOffset, int dataLength, TimeSpan localTimeAdjustment, bool bCopyData)
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
				if (dataLength == 0)
				{
					dataLength = binData.Length;
				}
				this.m_bNotNull = true;
				this.m_fSecondPrec = 9;
				if (bCopyData)
				{
					this.m_byteRepresentation = new byte[11];
					Array.Copy(binData, dataOffset, this.m_byteRepresentation, 0, dataLength);
				}
				else
				{
					this.m_byteRepresentation = binData;
				}
				this.m_byteRepresentation = TimeStamp.GetAdjustedTimeStampLTZ(this.m_byteRepresentation, localTimeAdjustment, false);
				int num;
				int num2;
				int num3;
				TimeStamp.ExtractTimeStampValuesFromByteArray(this.m_byteRepresentation, out this.m_year, out this.m_month, out this.m_day, out this.m_hour, out this.m_minute, out this.m_second, out this.m_nanosecond, out num, out num2, out num3, TSType.TimeStamp);
				if (!TimeStamp.IsValidDateTime(this.m_year, this.m_month, this.m_day, this.m_hour, this.m_minute, this.m_second, this.m_nanosecond))
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

		// Token: 0x0600176D RID: 5997 RVA: 0x000F7FEC File Offset: 0x000F61EC
		public static XmlQualifiedName GetXsdType(XmlSchemaSet schemaSet)
		{
			return new XmlQualifiedName("dateTime", "http://www.w3.org/2001/XMLSchema");
		}

		// Token: 0x0600176E RID: 5998 RVA: 0x000F8000 File Offset: 0x000F6200
		XmlSchema IXmlSerializable.GetSchema()
		{
			return null;
		}

		// Token: 0x0600176F RID: 5999 RVA: 0x000F8004 File Offset: 0x000F6204
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

		// Token: 0x06001770 RID: 6000 RVA: 0x000F8048 File Offset: 0x000F6248
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			if (this.m_bNotNull)
			{
				writer.WriteString(this.ToString());
				return;
			}
			writer.WriteAttributeString("xsi", "null", "http://www.w3.org/2001/XMLSchema-instance", "true");
		}

		// Token: 0x170003B5 RID: 949
		// (get) Token: 0x06001771 RID: 6001 RVA: 0x000F8080 File Offset: 0x000F6280
		public bool IsNull
		{
			get
			{
				return !this.m_bNotNull;
			}
		}

		// Token: 0x170003B6 RID: 950
		// (get) Token: 0x06001772 RID: 6002 RVA: 0x000F808C File Offset: 0x000F628C
		public byte[] BinData
		{
			get
			{
				if (this.m_bNotNull)
				{
					byte[] array = new byte[11];
					Array.Copy(this.m_byteRepresentation, array, 11);
					return array;
				}
				throw new OracleNullValueException();
			}
		}

		// Token: 0x170003B7 RID: 951
		// (get) Token: 0x06001773 RID: 6003 RVA: 0x000F80C0 File Offset: 0x000F62C0
		public DateTime Value
		{
			get
			{
				if (this.m_bNotNull)
				{
					return DateTimeConv.GetDateTime(this.m_byteRepresentation, OracleDbType.TimeStampLTZ, 0, -1);
				}
				throw new OracleNullValueException();
			}
		}

		// Token: 0x170003B8 RID: 952
		// (get) Token: 0x06001774 RID: 6004 RVA: 0x000F80E0 File Offset: 0x000F62E0
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

		// Token: 0x170003B9 RID: 953
		// (get) Token: 0x06001775 RID: 6005 RVA: 0x000F80F8 File Offset: 0x000F62F8
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

		// Token: 0x170003BA RID: 954
		// (get) Token: 0x06001776 RID: 6006 RVA: 0x000F8110 File Offset: 0x000F6310
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

		// Token: 0x170003BB RID: 955
		// (get) Token: 0x06001777 RID: 6007 RVA: 0x000F8128 File Offset: 0x000F6328
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

		// Token: 0x170003BC RID: 956
		// (get) Token: 0x06001778 RID: 6008 RVA: 0x000F8140 File Offset: 0x000F6340
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

		// Token: 0x170003BD RID: 957
		// (get) Token: 0x06001779 RID: 6009 RVA: 0x000F8158 File Offset: 0x000F6358
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

		// Token: 0x170003BE RID: 958
		// (get) Token: 0x0600177A RID: 6010 RVA: 0x000F8170 File Offset: 0x000F6370
		public double Millisecond
		{
			get
			{
				if (this.m_bNotNull)
				{
					return (double)TimeStamp.GetTSData(this.m_byteRepresentation, 7) / 1000000.0;
				}
				throw new OracleNullValueException();
			}
		}

		// Token: 0x170003BF RID: 959
		// (get) Token: 0x0600177B RID: 6011 RVA: 0x000F8198 File Offset: 0x000F6398
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

		// Token: 0x0600177C RID: 6012 RVA: 0x000F81B0 File Offset: 0x000F63B0
		public OracleTimeStampLTZ AddYears(int years)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleTimeStampLTZ result;
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
				result = OracleTimeStampLTZ.AddYearMonthToTimeStampLTZ(this.m_byteRepresentation, years, 0L);
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

		// Token: 0x0600177D RID: 6013 RVA: 0x000F8258 File Offset: 0x000F6458
		public OracleTimeStampLTZ AddMonths(long months)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleTimeStampLTZ result;
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
				result = OracleTimeStampLTZ.AddYearMonthToTimeStampLTZ(this.m_byteRepresentation, 0, months);
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

		// Token: 0x0600177E RID: 6014 RVA: 0x000F8304 File Offset: 0x000F6504
		public OracleTimeStampLTZ AddDays(double days)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleTimeStampLTZ result;
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
				result = OracleTimeStampLTZ.AddDayTimeToTimeStampLTZ(this.m_byteRepresentation, days, 2);
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

		// Token: 0x0600177F RID: 6015 RVA: 0x000F83B8 File Offset: 0x000F65B8
		public OracleTimeStampLTZ AddHours(double hours)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleTimeStampLTZ result;
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
				result = OracleTimeStampLTZ.AddDayTimeToTimeStampLTZ(this.m_byteRepresentation, hours, 3);
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

		// Token: 0x06001780 RID: 6016 RVA: 0x000F846C File Offset: 0x000F666C
		public OracleTimeStampLTZ AddMinutes(double minutes)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleTimeStampLTZ result;
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
				result = OracleTimeStampLTZ.AddDayTimeToTimeStampLTZ(this.m_byteRepresentation, minutes, 4);
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

		// Token: 0x06001781 RID: 6017 RVA: 0x000F8520 File Offset: 0x000F6720
		public OracleTimeStampLTZ AddSeconds(double seconds)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleTimeStampLTZ result;
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
				result = OracleTimeStampLTZ.AddDayTimeToTimeStampLTZ(this.m_byteRepresentation, seconds, 5);
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

		// Token: 0x06001782 RID: 6018 RVA: 0x000F85D4 File Offset: 0x000F67D4
		public OracleTimeStampLTZ AddMilliseconds(double milliseconds)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleTimeStampLTZ result;
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
				result = OracleTimeStampLTZ.AddDayTimeToTimeStampLTZ(this.m_byteRepresentation, num, 7);
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

		// Token: 0x06001783 RID: 6019 RVA: 0x000F8694 File Offset: 0x000F6894
		public OracleTimeStampLTZ AddNanoseconds(long nanoseconds)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleTimeStampLTZ result;
			try
			{
				if (!this.m_bNotNull)
				{
					throw new OracleNullValueException();
				}
				result = OracleTimeStampLTZ.AddDayTimeToTimeStampLTZ(this.m_byteRepresentation, nanoseconds, 7);
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

		// Token: 0x06001784 RID: 6020 RVA: 0x000F8724 File Offset: 0x000F6924
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
				if (obj.GetType() != typeof(OracleTimeStampLTZ))
				{
					throw new ArgumentException("obj");
				}
				result = HelperClass.CompareBytes(this.m_byteRepresentation, ((OracleTimeStampLTZ)obj).m_byteRepresentation);
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

		// Token: 0x06001785 RID: 6021 RVA: 0x000F87D8 File Offset: 0x000F69D8
		public override bool Equals(object obj)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			bool result;
			try
			{
				if (obj == null || obj.GetType() != typeof(OracleTimeStampLTZ))
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

		// Token: 0x06001786 RID: 6022 RVA: 0x000F8870 File Offset: 0x000F6A70
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

		// Token: 0x06001787 RID: 6023 RVA: 0x000F88DC File Offset: 0x000F6ADC
		public OracleIntervalDS GetDaysBetween(OracleTimeStampLTZ value1)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleIntervalDS result;
			try
			{
				if (this.m_bNotNull && value1.m_bNotNull)
				{
					if (this > value1)
					{
						result = TimeStamp.GetDaysBetweenTimeStamp(this.m_byteRepresentation, value1.m_hour, value1.m_minute, value1.m_second, value1.m_nanosecond, this.m_year, this.m_month, this.m_day, value1.m_year, value1.m_month, value1.m_day, true, OracleDbType.TimeStampLTZ);
					}
					else if (this < value1)
					{
						result = TimeStamp.GetDaysBetweenTimeStamp(value1.InternalByteRepresentation, this.m_hour, this.m_minute, this.m_second, this.m_nanosecond, value1.m_year, value1.m_month, value1.m_day, this.m_year, this.m_month, this.m_day, false, OracleDbType.TimeStampLTZ);
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

		// Token: 0x06001788 RID: 6024 RVA: 0x000F8A50 File Offset: 0x000F6C50
		public OracleIntervalYM GetYearsBetween(OracleTimeStampLTZ value1)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleIntervalYM result;
			try
			{
				if (this.m_bNotNull && value1.m_bNotNull)
				{
					if (this < value1)
					{
						int sumDay = value1.Day - this.m_day;
						int sumMonth = value1.Month - this.m_month;
						int sumYear = value1.Year - this.m_year;
						result = TimeStamp.GetYearsBetweenTimeStamp(sumDay, sumMonth, sumYear, value1.m_year, false);
					}
					else if (this > value1)
					{
						int sumDay = this.m_day - value1.Day;
						int sumMonth = this.m_month - value1.Month;
						int sumYear = this.m_year - value1.Year;
						result = TimeStamp.GetYearsBetweenTimeStamp(sumDay, sumMonth, sumYear, this.m_year, true);
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

		// Token: 0x06001789 RID: 6025 RVA: 0x000F8B90 File Offset: 0x000F6D90
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
					result = new OracleDate(this.Year, this.Month, this.Day, this.Hour, this.Minute, this.Second);
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

		// Token: 0x0600178A RID: 6026 RVA: 0x000F8C38 File Offset: 0x000F6E38
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
					result = new OracleTimeStamp(this.Year, this.Month, this.Day, this.Hour, this.Minute, this.Second, this.Millisecond);
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

		// Token: 0x0600178B RID: 6027 RVA: 0x000F8CE8 File Offset: 0x000F6EE8
		public OracleTimeStampTZ ToOracleTimeStampTZ()
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			if (this.m_bNotNull)
			{
				return new OracleTimeStampTZ(this.Year, this.Month, this.Day, this.Hour, this.Minute, this.Second, this.Millisecond);
			}
			return OracleTimeStampTZ.Null;
		}

		// Token: 0x0600178C RID: 6028 RVA: 0x000F8D4C File Offset: 0x000F6F4C
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
					result = DateTimeStringUtilities.ToString(this.m_year, this.m_month, this.m_day, this.m_hour, this.m_minute, this.m_second, this.m_nanosecond, null);
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

		// Token: 0x0600178D RID: 6029 RVA: 0x000F8DFC File Offset: 0x000F6FFC
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
					int num = 0;
					int num2 = 0;
					TimeZoneInfo local = TimeZoneInfo.Local;
					if (ZoneIdMap.isValidRegion(local.Id))
					{
						int regionID = ZoneIdMap.GetRegionID(local.Id);
						if (regionID > 0)
						{
							DateTime? localTime = null;
							if (year >= 1800)
							{
								localTime = new DateTime?(new DateTime(year, month, day, hour, minute, second));
							}
							TimeSpan timeSpan;
							bool flag;
							TimeStamp.s_timeZoneObj.GetOffsetOfLocalTime(localTime, regionID, out timeSpan, out flag);
							num = timeSpan.Hours;
							num2 = timeSpan.Minutes;
						}
					}
					else
					{
						num = TimeStamp.LocalTZOffset.m_tzHours;
						num2 = TimeStamp.LocalTZOffset.m_tzMinutes;
					}
					TimeStamp.ToUniversalTime(ref year, ref month, ref day, ref hour, ref minute, ref second, ref nanosecond, ref num, ref num2);
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

		// Token: 0x0600178E RID: 6030 RVA: 0x000F8F8C File Offset: 0x000F718C
		public static bool Equals(OracleTimeStampLTZ value1, OracleTimeStampLTZ value2)
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

		// Token: 0x0600178F RID: 6031 RVA: 0x000F9034 File Offset: 0x000F7234
		public static bool GreaterThan(OracleTimeStampLTZ value1, OracleTimeStampLTZ value2)
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

		// Token: 0x06001790 RID: 6032 RVA: 0x000F9098 File Offset: 0x000F7298
		public static bool GreaterThanOrEqual(OracleTimeStampLTZ value1, OracleTimeStampLTZ value2)
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

		// Token: 0x06001791 RID: 6033 RVA: 0x000F9100 File Offset: 0x000F7300
		public static bool LessThan(OracleTimeStampLTZ value1, OracleTimeStampLTZ value2)
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

		// Token: 0x06001792 RID: 6034 RVA: 0x000F9164 File Offset: 0x000F7364
		public static bool LessThanOrEqual(OracleTimeStampLTZ value1, OracleTimeStampLTZ value2)
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

		// Token: 0x06001793 RID: 6035 RVA: 0x000F91CC File Offset: 0x000F73CC
		public static bool NotEquals(OracleTimeStampLTZ value1, OracleTimeStampLTZ value2)
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

		// Token: 0x06001794 RID: 6036 RVA: 0x000F9234 File Offset: 0x000F7434
		public static OracleTimeStampLTZ GetSysDate()
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleTimeStampLTZ result;
			try
			{
				DateTime now = DateTime.Now;
				result = new OracleTimeStampLTZ(now);
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

		// Token: 0x06001795 RID: 6037 RVA: 0x000F9294 File Offset: 0x000F7494
		public static string GetLocalTimeZoneName()
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			string result;
			try
			{
				TimeZone currentTimeZone = TimeZone.CurrentTimeZone;
				DateTime now = DateTime.Now;
				TimeSpan utcOffset = currentTimeZone.GetUtcOffset(now);
				int num = utcOffset.Hours;
				int num2 = utcOffset.Minutes;
				bool flag = true;
				if (num < 0)
				{
					flag = false;
					num = -num;
				}
				if (num2 < 0)
				{
					flag = false;
					num2 = -num2;
				}
				StringBuilder stringBuilder = new StringBuilder(15);
				string value = num.ToString();
				string value2 = num2.ToString();
				if (flag)
				{
					stringBuilder.Append("+");
				}
				else
				{
					stringBuilder.Append("-");
				}
				stringBuilder.Append(value);
				stringBuilder.Append(":");
				stringBuilder.Append(value2);
				result = stringBuilder.ToString();
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

		// Token: 0x06001796 RID: 6038 RVA: 0x000F938C File Offset: 0x000F758C
		public static TimeSpan GetLocalTimeZoneOffset()
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			TimeSpan result;
			try
			{
				TimeZone currentTimeZone = TimeZone.CurrentTimeZone;
				DateTime now = DateTime.Now;
				TimeSpan utcOffset = currentTimeZone.GetUtcOffset(now);
				result = utcOffset;
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

		// Token: 0x06001797 RID: 6039 RVA: 0x000F93F4 File Offset: 0x000F75F4
		public static OracleTimeStampLTZ Parse(string tsStr)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleTimeStampLTZ result;
			try
			{
				result = new OracleTimeStampLTZ(tsStr);
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

		// Token: 0x06001798 RID: 6040 RVA: 0x000F9468 File Offset: 0x000F7668
		public static OracleTimeStampLTZ SetPrecision(OracleTimeStampLTZ value1, int fracSecPrecision)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleTimeStampLTZ result;
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
				result = new OracleTimeStampLTZ(value1.InternalByteRepresentation, fracSecPrecision);
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

		// Token: 0x06001799 RID: 6041 RVA: 0x000F9508 File Offset: 0x000F7708
		public static bool operator ==(OracleTimeStampLTZ value1, OracleTimeStampLTZ value2)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			bool result;
			try
			{
				result = OracleTimeStampLTZ.Equals(value1, value2);
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

		// Token: 0x0600179A RID: 6042 RVA: 0x000F9564 File Offset: 0x000F7764
		public static bool operator >(OracleTimeStampLTZ value1, OracleTimeStampLTZ value2)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			bool result;
			try
			{
				result = OracleTimeStampLTZ.GreaterThan(value1, value2);
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

		// Token: 0x0600179B RID: 6043 RVA: 0x000F95C0 File Offset: 0x000F77C0
		public static bool operator >=(OracleTimeStampLTZ value1, OracleTimeStampLTZ value2)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			bool result;
			try
			{
				result = OracleTimeStampLTZ.GreaterThanOrEqual(value1, value2);
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

		// Token: 0x0600179C RID: 6044 RVA: 0x000F961C File Offset: 0x000F781C
		public static bool operator <(OracleTimeStampLTZ value1, OracleTimeStampLTZ value2)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			bool result;
			try
			{
				result = OracleTimeStampLTZ.LessThan(value1, value2);
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

		// Token: 0x0600179D RID: 6045 RVA: 0x000F9678 File Offset: 0x000F7878
		public static bool operator <=(OracleTimeStampLTZ value1, OracleTimeStampLTZ value2)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			bool result;
			try
			{
				result = OracleTimeStampLTZ.LessThanOrEqual(value1, value2);
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

		// Token: 0x0600179E RID: 6046 RVA: 0x000F96D4 File Offset: 0x000F78D4
		public static bool operator !=(OracleTimeStampLTZ value1, OracleTimeStampLTZ value2)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			bool result;
			try
			{
				result = OracleTimeStampLTZ.NotEquals(value1, value2);
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

		// Token: 0x0600179F RID: 6047 RVA: 0x000F9730 File Offset: 0x000F7930
		public static OracleTimeStampLTZ operator +(OracleTimeStampLTZ value1, OracleIntervalDS value2)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleTimeStampLTZ result;
			try
			{
				if (value1.IsNull || value2.IsNull)
				{
					result = OracleTimeStampLTZ.Null;
				}
				else
				{
					result = OracleTimeStampLTZ.AddDayTimeToTimeStampLTZ(value1.m_byteRepresentation, value2, 11);
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

		// Token: 0x060017A0 RID: 6048 RVA: 0x000F97CC File Offset: 0x000F79CC
		public static OracleTimeStampLTZ operator +(OracleTimeStampLTZ value1, OracleIntervalYM value2)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleTimeStampLTZ result;
			try
			{
				if (value1.IsNull || value2.IsNull)
				{
					result = OracleTimeStampLTZ.Null;
				}
				else
				{
					result = OracleTimeStampLTZ.AddYearMonthToTimeStampLTZ(value1.InternalByteRepresentation, value2.Years, (long)value2.Months);
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

		// Token: 0x060017A1 RID: 6049 RVA: 0x000F9870 File Offset: 0x000F7A70
		public static OracleTimeStampLTZ operator +(OracleTimeStampLTZ value1, TimeSpan value2)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleTimeStampLTZ result;
			try
			{
				if (value1.IsNull)
				{
					result = OracleTimeStampLTZ.Null;
				}
				else
				{
					result = OracleTimeStampLTZ.AddDayTimeToTimeStampLTZ(value1.m_byteRepresentation, value2, 10);
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

		// Token: 0x060017A2 RID: 6050 RVA: 0x000F9904 File Offset: 0x000F7B04
		public static OracleTimeStampLTZ operator -(OracleTimeStampLTZ value1, OracleIntervalDS value2)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleTimeStampLTZ result;
			try
			{
				if (value1.IsNull || value2.IsNull)
				{
					result = OracleTimeStampLTZ.Null;
				}
				else
				{
					result = OracleTimeStampLTZ.AddDayTimeToTimeStampLTZ(value1.m_byteRepresentation, -value2, 11);
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

		// Token: 0x060017A3 RID: 6051 RVA: 0x000F99A8 File Offset: 0x000F7BA8
		public static OracleTimeStampLTZ operator -(OracleTimeStampLTZ value1, OracleIntervalYM value2)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleTimeStampLTZ result;
			try
			{
				if (value1.IsNull || value2.IsNull)
				{
					result = OracleTimeStampLTZ.Null;
				}
				else
				{
					result = OracleTimeStampLTZ.AddYearMonthToTimeStampLTZ(value1.InternalByteRepresentation, -value2.Years, -(long)value2.Months);
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

		// Token: 0x060017A4 RID: 6052 RVA: 0x000F9A50 File Offset: 0x000F7C50
		public static OracleTimeStampLTZ operator -(OracleTimeStampLTZ value1, TimeSpan value2)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleTimeStampLTZ result;
			try
			{
				if (value1.IsNull)
				{
					result = OracleTimeStampLTZ.Null;
				}
				else
				{
					result = OracleTimeStampLTZ.AddDayTimeToTimeStampLTZ(value1.m_byteRepresentation, -value2, 10);
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

		// Token: 0x060017A5 RID: 6053 RVA: 0x000F9AE8 File Offset: 0x000F7CE8
		public static explicit operator OracleTimeStampLTZ(string tsStr)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleTimeStampLTZ result;
			try
			{
				result = new OracleTimeStampLTZ(tsStr);
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

		// Token: 0x060017A6 RID: 6054 RVA: 0x000F9B5C File Offset: 0x000F7D5C
		public static explicit operator DateTime(OracleTimeStampLTZ value1)
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
				dateTime = DateTimeConv.GetDateTime(value1.m_byteRepresentation, OracleDbType.TimeStampLTZ, 0, -1);
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

		// Token: 0x060017A7 RID: 6055 RVA: 0x000F9BEC File Offset: 0x000F7DEC
		public static implicit operator OracleTimeStampLTZ(OracleDate value1)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleTimeStampLTZ result;
			try
			{
				if (!value1.IsNull)
				{
					byte[] byteRepresentation = value1.m_byteRepresentation;
					byte[] array = new byte[11];
					Array.Copy(byteRepresentation, array, byteRepresentation.Length);
					result = new OracleTimeStampLTZ(array, false);
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

		// Token: 0x060017A8 RID: 6056 RVA: 0x000F9C8C File Offset: 0x000F7E8C
		public static explicit operator OracleTimeStampLTZ(OracleTimeStamp value1)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleTimeStampLTZ result;
			try
			{
				if (!value1.IsNull)
				{
					byte[] byteRepresentation = value1.m_byteRepresentation;
					byte[] array = new byte[11];
					Array.Copy(byteRepresentation, array, byteRepresentation.Length);
					result = new OracleTimeStampLTZ(array, false);
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

		// Token: 0x060017A9 RID: 6057 RVA: 0x000F9D2C File Offset: 0x000F7F2C
		public static explicit operator OracleTimeStampLTZ(OracleTimeStampTZ value1)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleTimeStampLTZ result;
			try
			{
				if (!value1.IsNull)
				{
					result = new OracleTimeStampLTZ(value1.Year, value1.Month, value1.Day, value1.Hour, value1.Minute, value1.Second, value1.Nanosecond);
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

		// Token: 0x060017AA RID: 6058 RVA: 0x000F9DE4 File Offset: 0x000F7FE4
		public static implicit operator OracleTimeStampLTZ(DateTime value1)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleTimeStampLTZ result;
			try
			{
				result = new OracleTimeStampLTZ(value1);
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

		// Token: 0x060017AB RID: 6059 RVA: 0x000F9E58 File Offset: 0x000F8058
		internal static OracleTimeStampLTZ AddDayTimeToTimeStampLTZ(byte[] byteRepresentation, object value, byte tsComponent)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			OracleTimeStampLTZ result;
			try
			{
				byte[] binData = TimeStamp.AddDayTimeToTimeStamp(byteRepresentation, value, tsComponent, OracleDbType.TimeStampLTZ);
				result = new OracleTimeStampLTZ(binData, false);
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

		// Token: 0x060017AC RID: 6060 RVA: 0x000F9ED8 File Offset: 0x000F80D8
		internal static OracleTimeStampLTZ AddYearMonthToTimeStampLTZ(byte[] byteRepresentation, int yearToBeAdded, long monthToBeAdded)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			OracleTimeStampLTZ result;
			try
			{
				byte[] binData = TimeStamp.AddYearMonthToTimeStamp(byteRepresentation, yearToBeAdded, monthToBeAdded, OracleDbType.TimeStampLTZ);
				result = new OracleTimeStampLTZ(binData, false);
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

		// Token: 0x060017AD RID: 6061 RVA: 0x000F9F58 File Offset: 0x000F8158
		private void FromString(string tsStr)
		{
			if (tsStr == null)
			{
				throw new ArgumentNullException();
			}
			string text = null;
			DateTimeStringUtilities.FromString(tsStr, out this.m_year, out this.m_month, out this.m_day, out this.m_hour, out this.m_minute, out this.m_second, out this.m_nanosecond, out text, true, false);
			this.m_byteRepresentation = TimeStamp.ConstructByteArrayTS(this.m_year, this.m_month, this.m_day, this.m_hour, this.m_minute, this.m_second, this.m_nanosecond);
		}

		// Token: 0x170003C0 RID: 960
		// (get) Token: 0x060017AE RID: 6062 RVA: 0x000F9FDC File Offset: 0x000F81DC
		internal byte[] InternalByteRepresentation
		{
			get
			{
				return this.m_byteRepresentation;
			}
		}

		// Token: 0x04001A3E RID: 6718
		internal const byte MaxArrSize = 11;

		// Token: 0x04001A3F RID: 6719
		internal byte[] m_byteRepresentation;

		// Token: 0x04001A40 RID: 6720
		private int m_fSecondPrec;

		// Token: 0x04001A41 RID: 6721
		private bool m_bNotNull;

		// Token: 0x04001A42 RID: 6722
		private int m_year;

		// Token: 0x04001A43 RID: 6723
		private int m_month;

		// Token: 0x04001A44 RID: 6724
		private int m_day;

		// Token: 0x04001A45 RID: 6725
		private int m_hour;

		// Token: 0x04001A46 RID: 6726
		private int m_minute;

		// Token: 0x04001A47 RID: 6727
		private int m_second;

		// Token: 0x04001A48 RID: 6728
		private int m_nanosecond;

		// Token: 0x04001A49 RID: 6729
		public static readonly OracleTimeStampLTZ MaxValue = new OracleTimeStampLTZ(9999, 12, 31, 23, 59, 59, 999999999);

		// Token: 0x04001A4A RID: 6730
		public static readonly OracleTimeStampLTZ MinValue = new OracleTimeStampLTZ(-4712, 1, 1, 0, 0, 0, 0);

		// Token: 0x04001A4B RID: 6731
		public static readonly OracleTimeStampLTZ Null;
	}
}
