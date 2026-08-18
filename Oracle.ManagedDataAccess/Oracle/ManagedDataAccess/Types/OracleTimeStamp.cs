using System;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Oracle.ManagedDataAccess.Client;
using OracleInternal.Common;

namespace Oracle.ManagedDataAccess.Types
{
	// Token: 0x02000251 RID: 593
	[XmlSchemaProvider("GetXsdType")]
	[Serializable]
	public struct OracleTimeStamp : IComparable, IXmlSerializable, INullable
	{
		// Token: 0x06001719 RID: 5913 RVA: 0x000F5478 File Offset: 0x000F3678
		public OracleTimeStamp(int year, int month, int day, int hour, int minute, int second, int nanosecond)
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

		// Token: 0x0600171A RID: 5914 RVA: 0x000F5558 File Offset: 0x000F3758
		public OracleTimeStamp(int year, int month, int day, int hour, int minute, int second, double millisecond)
		{
			this = new OracleTimeStamp(year, month, day, hour, minute, second, (int)(millisecond * 1000000.0));
		}

		// Token: 0x0600171B RID: 5915 RVA: 0x000F5584 File Offset: 0x000F3784
		public OracleTimeStamp(int year, int month, int day)
		{
			this = new OracleTimeStamp(year, month, day, 0, 0, 0, 0);
		}

		// Token: 0x0600171C RID: 5916 RVA: 0x000F5594 File Offset: 0x000F3794
		public OracleTimeStamp(int year, int month, int day, int hour, int minute, int second)
		{
			this = new OracleTimeStamp(year, month, day, hour, minute, second, 0);
		}

		// Token: 0x0600171D RID: 5917 RVA: 0x000F55A8 File Offset: 0x000F37A8
		public OracleTimeStamp(DateTime date)
		{
			this = new OracleTimeStamp(date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second, TimeStamp.GetFsecond(date));
		}

		// Token: 0x0600171E RID: 5918 RVA: 0x000F55E0 File Offset: 0x000F37E0
		public OracleTimeStamp(byte[] binData)
		{
			this = new OracleTimeStamp(binData, true);
		}

		// Token: 0x0600171F RID: 5919 RVA: 0x000F55EC File Offset: 0x000F37EC
		public OracleTimeStamp(string tsStr)
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

		// Token: 0x06001720 RID: 5920 RVA: 0x000F56CC File Offset: 0x000F38CC
		internal OracleTimeStamp(byte[] binData, int fSecondPrec)
		{
			this = new OracleTimeStamp(binData);
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

		// Token: 0x06001721 RID: 5921 RVA: 0x000F58FC File Offset: 0x000F3AFC
		internal OracleTimeStamp(byte[] binData, bool bCopyData)
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
				TimeStamp.ExtractTimeStampValuesFromByteArray(this.m_byteRepresentation, out this.m_year, out this.m_month, out this.m_day, out this.m_hour, out this.m_minute, out this.m_second, out this.m_nanosecond, out num2, out num3, out num4, TSType.TimeStamp);
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

		// Token: 0x06001722 RID: 5922 RVA: 0x000F5A30 File Offset: 0x000F3C30
		internal OracleTimeStamp(byte[] binData, int dataOffset, int dataLength, bool bCopyData)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				if (dataLength == 0)
				{
					dataLength = binData.Length;
				}
				if (binData == null)
				{
					throw new ArgumentNullException();
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

		// Token: 0x06001723 RID: 5923 RVA: 0x000F5B5C File Offset: 0x000F3D5C
		public static XmlQualifiedName GetXsdType(XmlSchemaSet schemaSet)
		{
			return new XmlQualifiedName("dateTime", "http://www.w3.org/2001/XMLSchema");
		}

		// Token: 0x06001724 RID: 5924 RVA: 0x000F5B70 File Offset: 0x000F3D70
		XmlSchema IXmlSerializable.GetSchema()
		{
			return null;
		}

		// Token: 0x06001725 RID: 5925 RVA: 0x000F5B74 File Offset: 0x000F3D74
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

		// Token: 0x06001726 RID: 5926 RVA: 0x000F5BB8 File Offset: 0x000F3DB8
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			if (this.m_bNotNull)
			{
				writer.WriteString(this.ToString());
				return;
			}
			writer.WriteAttributeString("xsi", "null", "http://www.w3.org/2001/XMLSchema-instance", "true");
		}

		// Token: 0x170003A9 RID: 937
		// (get) Token: 0x06001727 RID: 5927 RVA: 0x000F5BF0 File Offset: 0x000F3DF0
		public byte[] BinData
		{
			get
			{
				if (this.m_bNotNull)
				{
					byte[] array = null;
					if (this.m_byteRepresentation != null)
					{
						array = new byte[11];
						Array.Copy(this.m_byteRepresentation, array, this.m_byteRepresentation.Length);
					}
					return array;
				}
				throw new OracleNullValueException();
			}
		}

		// Token: 0x170003AA RID: 938
		// (get) Token: 0x06001728 RID: 5928 RVA: 0x000F5C34 File Offset: 0x000F3E34
		public bool IsNull
		{
			get
			{
				return !this.m_bNotNull;
			}
		}

		// Token: 0x170003AB RID: 939
		// (get) Token: 0x06001729 RID: 5929 RVA: 0x000F5C40 File Offset: 0x000F3E40
		public DateTime Value
		{
			get
			{
				if (this.m_bNotNull)
				{
					return DateTimeConv.GetDateTime(this.m_byteRepresentation, OracleDbType.TimeStamp, 0, -1);
				}
				throw new OracleNullValueException();
			}
		}

		// Token: 0x170003AC RID: 940
		// (get) Token: 0x0600172A RID: 5930 RVA: 0x000F5C60 File Offset: 0x000F3E60
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

		// Token: 0x170003AD RID: 941
		// (get) Token: 0x0600172B RID: 5931 RVA: 0x000F5C78 File Offset: 0x000F3E78
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

		// Token: 0x170003AE RID: 942
		// (get) Token: 0x0600172C RID: 5932 RVA: 0x000F5C90 File Offset: 0x000F3E90
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

		// Token: 0x170003AF RID: 943
		// (get) Token: 0x0600172D RID: 5933 RVA: 0x000F5CA8 File Offset: 0x000F3EA8
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

		// Token: 0x170003B0 RID: 944
		// (get) Token: 0x0600172E RID: 5934 RVA: 0x000F5CC0 File Offset: 0x000F3EC0
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

		// Token: 0x170003B1 RID: 945
		// (get) Token: 0x0600172F RID: 5935 RVA: 0x000F5CD8 File Offset: 0x000F3ED8
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

		// Token: 0x170003B2 RID: 946
		// (get) Token: 0x06001730 RID: 5936 RVA: 0x000F5CF0 File Offset: 0x000F3EF0
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

		// Token: 0x170003B3 RID: 947
		// (get) Token: 0x06001731 RID: 5937 RVA: 0x000F5D18 File Offset: 0x000F3F18
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

		// Token: 0x06001732 RID: 5938 RVA: 0x000F5D30 File Offset: 0x000F3F30
		public OracleTimeStamp AddYears(int years)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleTimeStamp result;
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
				result = OracleTimeStamp.AddYearMonthToTimeStamp(this.m_byteRepresentation, years, 0L);
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

		// Token: 0x06001733 RID: 5939 RVA: 0x000F5DD8 File Offset: 0x000F3FD8
		public OracleTimeStamp AddMonths(long months)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleTimeStamp result;
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
				result = OracleTimeStamp.AddYearMonthToTimeStamp(this.m_byteRepresentation, 0, months);
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

		// Token: 0x06001734 RID: 5940 RVA: 0x000F5E84 File Offset: 0x000F4084
		public OracleTimeStamp AddDays(double days)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleTimeStamp result;
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
				result = OracleTimeStamp.AddDayTimeToTimeStamp(this.m_byteRepresentation, days, 2);
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

		// Token: 0x06001735 RID: 5941 RVA: 0x000F5F38 File Offset: 0x000F4138
		public OracleTimeStamp AddHours(double hours)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleTimeStamp result;
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
				result = OracleTimeStamp.AddDayTimeToTimeStamp(this.m_byteRepresentation, hours, 3);
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

		// Token: 0x06001736 RID: 5942 RVA: 0x000F5FEC File Offset: 0x000F41EC
		public OracleTimeStamp AddMinutes(double minutes)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleTimeStamp result;
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
				result = OracleTimeStamp.AddDayTimeToTimeStamp(this.m_byteRepresentation, minutes, 4);
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

		// Token: 0x06001737 RID: 5943 RVA: 0x000F60A0 File Offset: 0x000F42A0
		public OracleTimeStamp AddSeconds(double seconds)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleTimeStamp result;
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
				result = OracleTimeStamp.AddDayTimeToTimeStamp(this.m_byteRepresentation, seconds, 5);
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

		// Token: 0x06001738 RID: 5944 RVA: 0x000F6154 File Offset: 0x000F4354
		public OracleTimeStamp AddMilliseconds(double milliseconds)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleTimeStamp result;
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
				result = OracleTimeStamp.AddDayTimeToTimeStamp(this.m_byteRepresentation, num, 7);
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

		// Token: 0x06001739 RID: 5945 RVA: 0x000F6214 File Offset: 0x000F4414
		public OracleTimeStamp AddNanoseconds(long nanoseconds)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleTimeStamp result;
			try
			{
				if (!this.m_bNotNull)
				{
					throw new OracleNullValueException();
				}
				result = OracleTimeStamp.AddDayTimeToTimeStamp(this.m_byteRepresentation, nanoseconds, 7);
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

		// Token: 0x0600173A RID: 5946 RVA: 0x000F62A4 File Offset: 0x000F44A4
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

		// Token: 0x0600173B RID: 5947 RVA: 0x000F6354 File Offset: 0x000F4554
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
				if (obj.GetType() != typeof(OracleTimeStamp))
				{
					throw new ArgumentException("obj");
				}
				result = HelperClass.CompareBytes(this.m_byteRepresentation, ((OracleTimeStamp)obj).m_byteRepresentation);
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

		// Token: 0x0600173C RID: 5948 RVA: 0x000F6408 File Offset: 0x000F4608
		public override bool Equals(object obj)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			bool result;
			try
			{
				if (obj == null || obj.GetType() != typeof(OracleTimeStamp))
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

		// Token: 0x0600173D RID: 5949 RVA: 0x000F64A0 File Offset: 0x000F46A0
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

		// Token: 0x0600173E RID: 5950 RVA: 0x000F650C File Offset: 0x000F470C
		public OracleIntervalDS GetDaysBetween(OracleTimeStamp value1)
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
						result = TimeStamp.GetDaysBetweenTimeStamp(this.m_byteRepresentation, value1.m_hour, value1.m_minute, value1.m_second, value1.m_nanosecond, this.m_year, this.m_month, this.m_day, value1.m_year, value1.m_month, value1.m_day, true, OracleDbType.TimeStamp);
					}
					else if (this < value1)
					{
						result = TimeStamp.GetDaysBetweenTimeStamp(value1.InternalByteRepresentation, this.m_hour, this.m_minute, this.m_second, this.m_nanosecond, value1.m_year, value1.m_month, value1.m_day, this.m_year, this.m_month, this.m_day, false, OracleDbType.TimeStamp);
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

		// Token: 0x0600173F RID: 5951 RVA: 0x000F6680 File Offset: 0x000F4880
		public OracleIntervalYM GetYearsBetween(OracleTimeStamp value1)
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

		// Token: 0x06001740 RID: 5952 RVA: 0x000F67C0 File Offset: 0x000F49C0
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

		// Token: 0x06001741 RID: 5953 RVA: 0x000F6868 File Offset: 0x000F4A68
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
					result = new OracleTimeStampLTZ(this.m_year, this.m_month, this.m_day, this.m_hour, this.m_minute, this.m_second, this.m_nanosecond);
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

		// Token: 0x06001742 RID: 5954 RVA: 0x000F6918 File Offset: 0x000F4B18
		public OracleTimeStampTZ ToOracleTimeStampTZ()
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
					result = new OracleTimeStampTZ(this.m_year, this.m_month, this.m_day, this.m_hour, this.m_minute, this.m_second, this.m_nanosecond, null);
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

		// Token: 0x06001743 RID: 5955 RVA: 0x000F69C8 File Offset: 0x000F4BC8
		public static bool Equals(OracleTimeStamp value1, OracleTimeStamp value2)
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

		// Token: 0x06001744 RID: 5956 RVA: 0x000F6A70 File Offset: 0x000F4C70
		public static bool GreaterThan(OracleTimeStamp value1, OracleTimeStamp value2)
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

		// Token: 0x06001745 RID: 5957 RVA: 0x000F6AD4 File Offset: 0x000F4CD4
		public static bool GreaterThanOrEqual(OracleTimeStamp value1, OracleTimeStamp value2)
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

		// Token: 0x06001746 RID: 5958 RVA: 0x000F6B3C File Offset: 0x000F4D3C
		public static bool LessThan(OracleTimeStamp value1, OracleTimeStamp value2)
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

		// Token: 0x06001747 RID: 5959 RVA: 0x000F6BA0 File Offset: 0x000F4DA0
		public static bool LessThanOrEqual(OracleTimeStamp value1, OracleTimeStamp value2)
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

		// Token: 0x06001748 RID: 5960 RVA: 0x000F6C08 File Offset: 0x000F4E08
		public static bool NotEquals(OracleTimeStamp value1, OracleTimeStamp value2)
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

		// Token: 0x06001749 RID: 5961 RVA: 0x000F6C70 File Offset: 0x000F4E70
		public static OracleTimeStamp GetSysDate()
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleTimeStamp result;
			try
			{
				result = new OracleTimeStamp(DateTime.Now);
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

		// Token: 0x0600174A RID: 5962 RVA: 0x000F6CCC File Offset: 0x000F4ECC
		public static OracleTimeStamp Parse(string tsStr)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleTimeStamp result;
			try
			{
				result = new OracleTimeStamp(tsStr);
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

		// Token: 0x0600174B RID: 5963 RVA: 0x000F6D40 File Offset: 0x000F4F40
		public static OracleTimeStamp SetPrecision(OracleTimeStamp value1, int fracSecPrecision)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleTimeStamp result;
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
				result = new OracleTimeStamp(value1.InternalByteRepresentation, fracSecPrecision);
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

		// Token: 0x0600174C RID: 5964 RVA: 0x000F6DE0 File Offset: 0x000F4FE0
		public static bool operator ==(OracleTimeStamp value1, OracleTimeStamp value2)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			bool result;
			try
			{
				result = OracleTimeStamp.Equals(value1, value2);
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

		// Token: 0x0600174D RID: 5965 RVA: 0x000F6E3C File Offset: 0x000F503C
		public static bool operator >(OracleTimeStamp value1, OracleTimeStamp value2)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			bool result;
			try
			{
				result = OracleTimeStamp.GreaterThan(value1, value2);
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

		// Token: 0x0600174E RID: 5966 RVA: 0x000F6E98 File Offset: 0x000F5098
		public static bool operator >=(OracleTimeStamp value1, OracleTimeStamp value2)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			bool result;
			try
			{
				result = OracleTimeStamp.GreaterThanOrEqual(value1, value2);
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

		// Token: 0x0600174F RID: 5967 RVA: 0x000F6EF4 File Offset: 0x000F50F4
		public static bool operator <(OracleTimeStamp value1, OracleTimeStamp value2)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			bool result;
			try
			{
				result = OracleTimeStamp.LessThan(value1, value2);
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

		// Token: 0x06001750 RID: 5968 RVA: 0x000F6F50 File Offset: 0x000F5150
		public static bool operator <=(OracleTimeStamp value1, OracleTimeStamp value2)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			bool result;
			try
			{
				result = OracleTimeStamp.LessThanOrEqual(value1, value2);
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

		// Token: 0x06001751 RID: 5969 RVA: 0x000F6FAC File Offset: 0x000F51AC
		public static bool operator !=(OracleTimeStamp value1, OracleTimeStamp value2)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			bool result;
			try
			{
				result = OracleTimeStamp.NotEquals(value1, value2);
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

		// Token: 0x06001752 RID: 5970 RVA: 0x000F7008 File Offset: 0x000F5208
		public static OracleTimeStamp operator +(OracleTimeStamp value1, OracleIntervalDS value2)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleTimeStamp result;
			try
			{
				if (value1.IsNull || value2.IsNull)
				{
					result = OracleTimeStamp.Null;
				}
				else
				{
					result = OracleTimeStamp.AddDayTimeToTimeStamp(value1.m_byteRepresentation, value2, 11);
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

		// Token: 0x06001753 RID: 5971 RVA: 0x000F70A4 File Offset: 0x000F52A4
		public static OracleTimeStamp operator +(OracleTimeStamp value1, OracleIntervalYM value2)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleTimeStamp result;
			try
			{
				if (value1.IsNull || value2.IsNull)
				{
					result = OracleTimeStamp.Null;
				}
				else
				{
					result = OracleTimeStamp.AddYearMonthToTimeStamp(value1.InternalByteRepresentation, value2.Years, (long)value2.Months);
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

		// Token: 0x06001754 RID: 5972 RVA: 0x000F7148 File Offset: 0x000F5348
		public static OracleTimeStamp operator +(OracleTimeStamp value1, TimeSpan value2)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleTimeStamp result;
			try
			{
				if (value1.IsNull)
				{
					result = OracleTimeStamp.Null;
				}
				else
				{
					result = OracleTimeStamp.AddDayTimeToTimeStamp(value1.m_byteRepresentation, value2, 10);
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

		// Token: 0x06001755 RID: 5973 RVA: 0x000F71DC File Offset: 0x000F53DC
		public static OracleTimeStamp operator -(OracleTimeStamp value1, OracleIntervalDS value2)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleTimeStamp result;
			try
			{
				if (value1.IsNull || value2.IsNull)
				{
					result = OracleTimeStamp.Null;
				}
				else
				{
					result = OracleTimeStamp.AddDayTimeToTimeStamp(value1.m_byteRepresentation, -value2, 11);
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

		// Token: 0x06001756 RID: 5974 RVA: 0x000F7280 File Offset: 0x000F5480
		public static OracleTimeStamp operator -(OracleTimeStamp value1, OracleIntervalYM value2)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleTimeStamp result;
			try
			{
				if (value1.IsNull || value2.IsNull)
				{
					result = OracleTimeStamp.Null;
				}
				else
				{
					result = OracleTimeStamp.AddYearMonthToTimeStamp(value1.InternalByteRepresentation, -value2.Years, -(long)value2.Months);
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

		// Token: 0x06001757 RID: 5975 RVA: 0x000F7328 File Offset: 0x000F5528
		public static OracleTimeStamp operator -(OracleTimeStamp value1, TimeSpan value2)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleTimeStamp result;
			try
			{
				if (value1.IsNull)
				{
					result = OracleTimeStamp.Null;
				}
				else
				{
					result = OracleTimeStamp.AddDayTimeToTimeStamp(value1.m_byteRepresentation, -value2, 10);
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

		// Token: 0x06001758 RID: 5976 RVA: 0x000F73C0 File Offset: 0x000F55C0
		public static explicit operator OracleTimeStamp(string tsStr)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleTimeStamp result;
			try
			{
				result = new OracleTimeStamp(tsStr);
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

		// Token: 0x06001759 RID: 5977 RVA: 0x000F7434 File Offset: 0x000F5634
		public static explicit operator DateTime(OracleTimeStamp value1)
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
				dateTime = DateTimeConv.GetDateTime(value1.m_byteRepresentation, OracleDbType.TimeStamp, 0, -1);
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

		// Token: 0x0600175A RID: 5978 RVA: 0x000F74C4 File Offset: 0x000F56C4
		public static implicit operator OracleTimeStamp(OracleDate value1)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleTimeStamp result;
			try
			{
				if (!value1.IsNull)
				{
					byte[] byteRepresentation = value1.m_byteRepresentation;
					byte[] array = new byte[11];
					Array.Copy(byteRepresentation, array, byteRepresentation.Length);
					result = new OracleTimeStamp(array, false);
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

		// Token: 0x0600175B RID: 5979 RVA: 0x000F7564 File Offset: 0x000F5764
		public static explicit operator OracleTimeStamp(OracleTimeStampLTZ value1)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleTimeStamp result;
			try
			{
				if (!value1.IsNull)
				{
					byte[] internalByteRepresentation = value1.InternalByteRepresentation;
					byte[] array = new byte[11];
					Array.Copy(internalByteRepresentation, array, internalByteRepresentation.Length);
					result = new OracleTimeStamp(array, false);
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

		// Token: 0x0600175C RID: 5980 RVA: 0x000F7604 File Offset: 0x000F5804
		public static explicit operator OracleTimeStamp(OracleTimeStampTZ value1)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleTimeStamp result;
			try
			{
				if (!value1.IsNull)
				{
					result = new OracleTimeStamp(value1.Year, value1.Month, value1.Day, value1.Hour, value1.Minute, value1.Second, value1.Nanosecond);
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

		// Token: 0x0600175D RID: 5981 RVA: 0x000F76BC File Offset: 0x000F58BC
		public static implicit operator OracleTimeStamp(DateTime value1)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleTimeStamp result;
			try
			{
				result = new OracleTimeStamp(value1);
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

		// Token: 0x0600175E RID: 5982 RVA: 0x000F7730 File Offset: 0x000F5930
		internal static OracleTimeStamp AddDayTimeToTimeStamp(byte[] byteRepresentation, object value, byte tsComponent)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			OracleTimeStamp result;
			try
			{
				byte[] binData = TimeStamp.AddDayTimeToTimeStamp(byteRepresentation, value, tsComponent, OracleDbType.TimeStamp);
				result = new OracleTimeStamp(binData, false);
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

		// Token: 0x0600175F RID: 5983 RVA: 0x000F77B0 File Offset: 0x000F59B0
		internal static OracleTimeStamp AddYearMonthToTimeStamp(byte[] byteRepresentation, int yearToBeAdded, long monthToBeAdded)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			OracleTimeStamp result;
			try
			{
				byte[] binData = TimeStamp.AddYearMonthToTimeStamp(byteRepresentation, yearToBeAdded, monthToBeAdded, OracleDbType.TimeStamp);
				result = new OracleTimeStamp(binData, false);
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

		// Token: 0x06001760 RID: 5984 RVA: 0x000F7830 File Offset: 0x000F5A30
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

		// Token: 0x170003B4 RID: 948
		// (get) Token: 0x06001761 RID: 5985 RVA: 0x000F78B4 File Offset: 0x000F5AB4
		internal byte[] InternalByteRepresentation
		{
			get
			{
				return this.m_byteRepresentation;
			}
		}

		// Token: 0x04001A30 RID: 6704
		internal const byte MaxArrSize = 11;

		// Token: 0x04001A31 RID: 6705
		private bool m_bNotNull;

		// Token: 0x04001A32 RID: 6706
		private int m_fSecondPrec;

		// Token: 0x04001A33 RID: 6707
		internal byte[] m_byteRepresentation;

		// Token: 0x04001A34 RID: 6708
		private int m_year;

		// Token: 0x04001A35 RID: 6709
		private int m_month;

		// Token: 0x04001A36 RID: 6710
		private int m_day;

		// Token: 0x04001A37 RID: 6711
		private int m_hour;

		// Token: 0x04001A38 RID: 6712
		private int m_minute;

		// Token: 0x04001A39 RID: 6713
		private int m_second;

		// Token: 0x04001A3A RID: 6714
		private int m_nanosecond;

		// Token: 0x04001A3B RID: 6715
		public static readonly OracleTimeStamp MaxValue = new OracleTimeStamp(9999, 12, 31, 23, 59, 59, 999999999);

		// Token: 0x04001A3C RID: 6716
		public static readonly OracleTimeStamp MinValue = new OracleTimeStamp(-4712, 1, 1, 0, 0, 0, 0);

		// Token: 0x04001A3D RID: 6717
		public static readonly OracleTimeStamp Null;
	}
}
