using System;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Oracle.ManagedDataAccess.Client;
using OracleInternal.Common;

namespace Oracle.ManagedDataAccess.Types
{
	// Token: 0x0200024B RID: 587
	[XmlSchemaProvider("GetXsdType")]
	[Serializable]
	public struct OracleIntervalDS : IComparable, IXmlSerializable, INullable
	{
		// Token: 0x0600166C RID: 5740 RVA: 0x000EF880 File Offset: 0x000EDA80
		public OracleIntervalDS(byte[] binData)
		{
			this = new OracleIntervalDS(binData, true);
		}

		// Token: 0x0600166D RID: 5741 RVA: 0x000EF88C File Offset: 0x000EDA8C
		public OracleIntervalDS(int days, int hours, int minutes, int seconds, int nanoseconds)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				if (!Interval.IsValidDS(days, hours, minutes, seconds, nanoseconds))
				{
					throw new ArgumentOutOfRangeException();
				}
				this.m_bNotNull = true;
				this.initialZoneId = 0;
				this.m_fSecondPrec = 9;
				this.m_dayPrec = 9;
				this.m_byteRepresentation = new byte[11];
				days += (int)OracleIntervalDS.INTERVALDAYOFFSET;
				this.m_byteRepresentation[0] = RepConversion.RightShiftFirstNibble(days);
				this.m_byteRepresentation[1] = RepConversion.RightShiftSecondNibble(days);
				this.m_byteRepresentation[2] = RepConversion.RightShiftThirdNibble(days);
				this.m_byteRepresentation[3] = RepConversion.RightShiftFourthNibble(days);
				this.m_byteRepresentation[4] = (byte)((long)hours + (long)((ulong)OracleIntervalDS.INTERVALDSOFFSET));
				this.m_byteRepresentation[5] = (byte)((long)minutes + (long)((ulong)OracleIntervalDS.INTERVALDSOFFSET));
				this.m_byteRepresentation[6] = (byte)((long)seconds + (long)((ulong)OracleIntervalDS.INTERVALDSOFFSET));
				int val = nanoseconds + (int)OracleIntervalDS.INTERVALDAYOFFSET;
				this.m_byteRepresentation[7] = RepConversion.RightShiftFirstNibble(val);
				this.m_byteRepresentation[8] = RepConversion.RightShiftSecondNibble(val);
				this.m_byteRepresentation[9] = RepConversion.RightShiftThirdNibble(val);
				this.m_byteRepresentation[10] = RepConversion.RightShiftFourthNibble(val);
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

		// Token: 0x0600166E RID: 5742 RVA: 0x000EFA14 File Offset: 0x000EDC14
		public OracleIntervalDS(int days, int hours, int minutes, int seconds, double milliseconds)
		{
			this = new OracleIntervalDS(days, hours, minutes, seconds, (int)(milliseconds * 1000000.0));
		}

		// Token: 0x0600166F RID: 5743 RVA: 0x000EFA30 File Offset: 0x000EDC30
		public OracleIntervalDS(TimeSpan data)
		{
			this = new OracleIntervalDS(data.Days, data.Hours, data.Minutes, data.Seconds, (int)(data.Ticks - (long)data.Days * 864000000000L - (long)data.Hours * 36000000000L - (long)data.Minutes * 600000000L - (long)data.Seconds * 10000000L) * 100);
		}

		// Token: 0x06001670 RID: 5744 RVA: 0x000EFAB0 File Offset: 0x000EDCB0
		public OracleIntervalDS(double totalDays)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				decimal num = (decimal)totalDays;
				int num2 = (int)num;
				decimal d = num - num2;
				num = d * 24m;
				int num3 = (int)num;
				d = num - num3;
				num = d * 60m;
				int num4 = (int)num;
				d = num - num4;
				num = d * 60m;
				int num5 = (int)num;
				d = num - num5;
				int num6 = (int)(d * 1000000000m);
				if (!Interval.IsValidDS(num2, num3, num4, num5, num6))
				{
					throw new OverflowException("totalDays");
				}
				this.m_bNotNull = true;
				this.initialZoneId = 0;
				this.m_fSecondPrec = 9;
				this.m_dayPrec = 9;
				this.m_byteRepresentation = new byte[11];
				num2 += (int)OracleIntervalDS.INTERVALDAYOFFSET;
				this.m_byteRepresentation[0] = RepConversion.RightShiftFirstNibble(num2);
				this.m_byteRepresentation[1] = RepConversion.RightShiftSecondNibble(num2);
				this.m_byteRepresentation[2] = RepConversion.RightShiftThirdNibble(num2);
				this.m_byteRepresentation[3] = RepConversion.RightShiftFourthNibble(num2);
				this.m_byteRepresentation[4] = (byte)((long)num3 + (long)((ulong)OracleIntervalDS.INTERVALDSOFFSET));
				this.m_byteRepresentation[5] = (byte)((long)num4 + (long)((ulong)OracleIntervalDS.INTERVALDSOFFSET));
				this.m_byteRepresentation[6] = (byte)((long)num5 + (long)((ulong)OracleIntervalDS.INTERVALDSOFFSET));
				int val = num6 + (int)OracleIntervalDS.INTERVALDAYOFFSET;
				this.m_byteRepresentation[7] = RepConversion.RightShiftFirstNibble(val);
				this.m_byteRepresentation[8] = RepConversion.RightShiftSecondNibble(val);
				this.m_byteRepresentation[9] = RepConversion.RightShiftThirdNibble(val);
				this.m_byteRepresentation[10] = RepConversion.RightShiftFourthNibble(val);
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

		// Token: 0x06001671 RID: 5745 RVA: 0x000EFCE8 File Offset: 0x000EDEE8
		public OracleIntervalDS(string intervalStr)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				if (intervalStr == null)
				{
					throw new ArgumentNullException("intervalStr");
				}
				if (intervalStr.Length == 0)
				{
					throw new FormatException("intervalStr");
				}
				this.m_bNotNull = true;
				this.initialZoneId = 0;
				this.m_fSecondPrec = 9;
				this.m_dayPrec = 9;
				this.m_byteRepresentation = null;
				this.m_byteRepresentation = this.ToBytes(intervalStr);
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

		// Token: 0x06001672 RID: 5746 RVA: 0x000EFDAC File Offset: 0x000EDFAC
		internal OracleIntervalDS(byte[] binData, int dayPrecision, int fsecondPrecision, bool bCopyData = true)
		{
			this = new OracleIntervalDS(binData, bCopyData);
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				int num;
				int num2;
				int num3;
				int num4;
				int num5;
				OracleIntervalDS.GetIDSData(this.m_byteRepresentation, out num, out num2, out num3, out num4, out num5, 0);
				int num6;
				if (num < 0)
				{
					num6 = -num;
				}
				else
				{
					num6 = num;
				}
				if (num6 != 0 && num6.ToString().Length > dayPrecision)
				{
					throw new OverflowException("binData");
				}
				this.m_dayPrec = (byte)dayPrecision;
				this.m_fSecondPrec = (byte)fsecondPrecision;
				double num7 = (double)num5 / 1000000000.0;
				num7 = Math.Round(num7, fsecondPrecision);
				num7 *= 1000000000.0;
				num5 = Convert.ToInt32(num7);
				if (num5 < 1000000000 && num5 > -999999998)
				{
					num5 += (int)OracleIntervalDS.INTERVALDAYOFFSET;
					this.m_byteRepresentation[7] = RepConversion.RightShiftFirstNibble(num5);
					this.m_byteRepresentation[8] = RepConversion.RightShiftSecondNibble(num5);
					this.m_byteRepresentation[9] = RepConversion.RightShiftThirdNibble(num5);
					this.m_byteRepresentation[10] = RepConversion.RightShiftFourthNibble(num5);
				}
				else
				{
					if (num5 > 999999999)
					{
						num5 = 0;
						num4++;
					}
					else if (num5 < -999999999)
					{
						num5 = 0;
						num4--;
					}
					if (num4 > 59)
					{
						num4 = 0;
						num3++;
					}
					else if (num4 < -59)
					{
						num4 = 0;
						num3--;
					}
					if (num3 > 59)
					{
						num3 = 0;
						num2++;
					}
					else if (num3 < -59)
					{
						num3 = 0;
						num2--;
					}
					if (num2 > 23)
					{
						num2 = 0;
						num++;
					}
					else if (num2 < -23)
					{
						num2 = 0;
						num--;
					}
					if (num > 999999999 || num < -999999999)
					{
						throw new OverflowException("binData");
					}
					num += (int)OracleIntervalDS.INTERVALDAYOFFSET;
					this.m_byteRepresentation[0] = RepConversion.RightShiftFirstNibble(num);
					this.m_byteRepresentation[1] = RepConversion.RightShiftSecondNibble(num);
					this.m_byteRepresentation[2] = RepConversion.RightShiftThirdNibble(num);
					this.m_byteRepresentation[3] = RepConversion.RightShiftFourthNibble(num);
					this.m_byteRepresentation[4] = (byte)((long)num2 + (long)((ulong)OracleIntervalDS.INTERVALDSOFFSET));
					this.m_byteRepresentation[5] = (byte)((long)num3 + (long)((ulong)OracleIntervalDS.INTERVALDSOFFSET));
					this.m_byteRepresentation[6] = (byte)((long)num4 + (long)((ulong)OracleIntervalDS.INTERVALDSOFFSET));
					num5 += (int)OracleIntervalDS.INTERVALDAYOFFSET;
					this.m_byteRepresentation[7] = RepConversion.RightShiftFirstNibble(num5);
					this.m_byteRepresentation[8] = RepConversion.RightShiftSecondNibble(num5);
					this.m_byteRepresentation[9] = RepConversion.RightShiftThirdNibble(num5);
					this.m_byteRepresentation[10] = RepConversion.RightShiftFourthNibble(num5);
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

		// Token: 0x06001673 RID: 5747 RVA: 0x000F0064 File Offset: 0x000EE264
		internal OracleIntervalDS(byte[] binData, bool bCopyData)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				if (binData == null)
				{
					throw new ArgumentNullException("binData");
				}
				this.m_bNotNull = true;
				this.initialZoneId = 0;
				this.m_fSecondPrec = 9;
				this.m_dayPrec = 9;
				if (binData.Length != 11)
				{
					throw new ArgumentException("binData");
				}
				if (bCopyData)
				{
					this.m_byteRepresentation = new byte[11];
					Array.Copy(binData, this.m_byteRepresentation, 11);
				}
				else
				{
					this.m_byteRepresentation = binData;
				}
				int days;
				int hours;
				int minutes;
				int seconds;
				int nanoseconds;
				OracleIntervalDS.GetIDSData(this.m_byteRepresentation, out days, out hours, out minutes, out seconds, out nanoseconds, 0);
				if (!Interval.IsValidDS(days, hours, minutes, seconds, nanoseconds))
				{
					throw new OverflowException("binData");
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

		// Token: 0x1700038B RID: 907
		// (get) Token: 0x06001674 RID: 5748 RVA: 0x000F0168 File Offset: 0x000EE368
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

		// Token: 0x1700038C RID: 908
		// (get) Token: 0x06001675 RID: 5749 RVA: 0x000F019C File Offset: 0x000EE39C
		public bool IsNull
		{
			get
			{
				return !this.m_bNotNull;
			}
		}

		// Token: 0x1700038D RID: 909
		// (get) Token: 0x06001676 RID: 5750 RVA: 0x000F01A8 File Offset: 0x000EE3A8
		public TimeSpan Value
		{
			get
			{
				if (this.m_bNotNull)
				{
					return OracleIntervalDS.GetTimeSpan(this.m_byteRepresentation, OracleDbType.IntervalDS, 0, -1);
				}
				throw new OracleNullValueException();
			}
		}

		// Token: 0x1700038E RID: 910
		// (get) Token: 0x06001677 RID: 5751 RVA: 0x000F01C8 File Offset: 0x000EE3C8
		public int Days
		{
			get
			{
				if (this.m_bNotNull)
				{
					return OracleIntervalDS.GetIDSData(this.m_byteRepresentation, 2);
				}
				throw new OracleNullValueException();
			}
		}

		// Token: 0x1700038F RID: 911
		// (get) Token: 0x06001678 RID: 5752 RVA: 0x000F01E4 File Offset: 0x000EE3E4
		public int Hours
		{
			get
			{
				if (this.m_bNotNull)
				{
					return OracleIntervalDS.GetIDSData(this.m_byteRepresentation, 3);
				}
				throw new OracleNullValueException();
			}
		}

		// Token: 0x17000390 RID: 912
		// (get) Token: 0x06001679 RID: 5753 RVA: 0x000F0200 File Offset: 0x000EE400
		public int Minutes
		{
			get
			{
				if (this.m_bNotNull)
				{
					return OracleIntervalDS.GetIDSData(this.m_byteRepresentation, 4);
				}
				throw new OracleNullValueException();
			}
		}

		// Token: 0x17000391 RID: 913
		// (get) Token: 0x0600167A RID: 5754 RVA: 0x000F021C File Offset: 0x000EE41C
		public int Seconds
		{
			get
			{
				if (this.m_bNotNull)
				{
					return OracleIntervalDS.GetIDSData(this.m_byteRepresentation, 5);
				}
				throw new OracleNullValueException();
			}
		}

		// Token: 0x17000392 RID: 914
		// (get) Token: 0x0600167B RID: 5755 RVA: 0x000F0238 File Offset: 0x000EE438
		public double Milliseconds
		{
			get
			{
				if (this.m_bNotNull)
				{
					return (double)(OracleIntervalDS.GetIDSData(this.m_byteRepresentation, 7) / 1000000);
				}
				throw new OracleNullValueException();
			}
		}

		// Token: 0x17000393 RID: 915
		// (get) Token: 0x0600167C RID: 5756 RVA: 0x000F025C File Offset: 0x000EE45C
		public int Nanoseconds
		{
			get
			{
				if (this.m_bNotNull)
				{
					return OracleIntervalDS.GetIDSData(this.m_byteRepresentation, 7);
				}
				throw new OracleNullValueException();
			}
		}

		// Token: 0x17000394 RID: 916
		// (get) Token: 0x0600167D RID: 5757 RVA: 0x000F0278 File Offset: 0x000EE478
		public double TotalDays
		{
			get
			{
				if (this.m_bNotNull)
				{
					int num;
					int num2;
					int num3;
					int num4;
					int num5;
					OracleIntervalDS.GetIDSData(this.m_byteRepresentation, out num, out num2, out num3, out num4, out num5, 0);
					return (double)num + (double)num2 / 24.0 + (double)num3 / 1440.0 + (double)num4 / 86400.0 + (double)num5 / 86400000000000.0;
				}
				throw new OracleNullValueException();
			}
		}

		// Token: 0x0600167E RID: 5758 RVA: 0x000F02F0 File Offset: 0x000EE4F0
		public static bool Equals(OracleIntervalDS value1, OracleIntervalDS value2)
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

		// Token: 0x0600167F RID: 5759 RVA: 0x000F0398 File Offset: 0x000EE598
		public static bool GreaterThan(OracleIntervalDS value1, OracleIntervalDS value2)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			bool result;
			try
			{
				result = (value1.CompareTo(value2) > 0);
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

		// Token: 0x06001680 RID: 5760 RVA: 0x000F0418 File Offset: 0x000EE618
		public static bool GreaterThanOrEqual(OracleIntervalDS value1, OracleIntervalDS value2)
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

		// Token: 0x06001681 RID: 5761 RVA: 0x000F049C File Offset: 0x000EE69C
		public static bool LessThan(OracleIntervalDS value1, OracleIntervalDS value2)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			bool result;
			try
			{
				result = (value1.CompareTo(value2) < 0);
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

		// Token: 0x06001682 RID: 5762 RVA: 0x000F0500 File Offset: 0x000EE700
		public static bool LessThanOrEqual(OracleIntervalDS value1, OracleIntervalDS value2)
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

		// Token: 0x06001683 RID: 5763 RVA: 0x000F0568 File Offset: 0x000EE768
		public static bool NotEquals(OracleIntervalDS value1, OracleIntervalDS value2)
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

		// Token: 0x06001684 RID: 5764 RVA: 0x000F05D0 File Offset: 0x000EE7D0
		public static OracleIntervalDS Parse(string intervalStr)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleIntervalDS result;
			try
			{
				result = new OracleIntervalDS(intervalStr);
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

		// Token: 0x06001685 RID: 5765 RVA: 0x000F0644 File Offset: 0x000EE844
		public static OracleIntervalDS SetPrecision(OracleIntervalDS value1, int dayPrecision, int fracSecPrecision)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleIntervalDS result;
			try
			{
				if (!value1.m_bNotNull)
				{
					throw new OracleNullValueException();
				}
				if (dayPrecision < 0 || dayPrecision > 9)
				{
					throw new ArgumentOutOfRangeException("dayPrecision");
				}
				if (fracSecPrecision < 0 || fracSecPrecision > 9)
				{
					throw new ArgumentOutOfRangeException("fracSecPrecision");
				}
				result = new OracleIntervalDS(value1.InternalByteRepresentation, dayPrecision, fracSecPrecision, true);
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

		// Token: 0x06001686 RID: 5766 RVA: 0x000F06F8 File Offset: 0x000EE8F8
		public static bool operator ==(OracleIntervalDS value1, OracleIntervalDS value2)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			bool result;
			try
			{
				result = OracleIntervalDS.Equals(value1, value2);
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

		// Token: 0x06001687 RID: 5767 RVA: 0x000F0754 File Offset: 0x000EE954
		public static bool operator >(OracleIntervalDS value1, OracleIntervalDS value2)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			bool result;
			try
			{
				result = OracleIntervalDS.GreaterThan(value1, value2);
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

		// Token: 0x06001688 RID: 5768 RVA: 0x000F07B0 File Offset: 0x000EE9B0
		public static bool operator >=(OracleIntervalDS value1, OracleIntervalDS value2)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			bool result;
			try
			{
				result = OracleIntervalDS.GreaterThanOrEqual(value1, value2);
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

		// Token: 0x06001689 RID: 5769 RVA: 0x000F080C File Offset: 0x000EEA0C
		public static bool operator <(OracleIntervalDS value1, OracleIntervalDS value2)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			bool result;
			try
			{
				result = OracleIntervalDS.LessThan(value1, value2);
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

		// Token: 0x0600168A RID: 5770 RVA: 0x000F0868 File Offset: 0x000EEA68
		public static bool operator <=(OracleIntervalDS value1, OracleIntervalDS value2)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			bool result;
			try
			{
				result = OracleIntervalDS.LessThanOrEqual(value1, value2);
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

		// Token: 0x0600168B RID: 5771 RVA: 0x000F08C4 File Offset: 0x000EEAC4
		public static bool operator !=(OracleIntervalDS value1, OracleIntervalDS value2)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			bool result;
			try
			{
				result = OracleIntervalDS.NotEquals(value1, value2);
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

		// Token: 0x0600168C RID: 5772 RVA: 0x000F0920 File Offset: 0x000EEB20
		public static OracleIntervalDS operator +(OracleIntervalDS value1, OracleIntervalDS value2)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleIntervalDS result;
			try
			{
				if (!value1.m_bNotNull || !value2.m_bNotNull)
				{
					result = OracleIntervalDS.Null;
				}
				else
				{
					int num = 0;
					int num2;
					int num3;
					int num4;
					int num5;
					int num6;
					OracleIntervalDS.GetIDSData(value1.InternalByteRepresentation, out num2, out num3, out num4, out num5, out num6, 0);
					int num7;
					int num8;
					int num9;
					int num10;
					int num11;
					OracleIntervalDS.GetIDSData(value2.InternalByteRepresentation, out num7, out num8, out num9, out num10, out num11, 0);
					int num12 = num6 + num11;
					if (num12 > 999999999)
					{
						num = 1;
						num12 -= 1000000000;
					}
					else if (num12 < -999999999)
					{
						num = -1;
						num12 -= -1000000000;
					}
					int num13 = num5 + num10 + num;
					num = 0;
					if (num13 > 59)
					{
						num = 1;
						num13 -= 60;
					}
					else if (num13 < -59)
					{
						num = -1;
						num13 -= -60;
					}
					int num14 = num4 + num9 + num;
					num = 0;
					if (num14 > 59)
					{
						num = 1;
						num14 -= 60;
					}
					else if (num14 < -59)
					{
						num = -1;
						num14 -= -60;
					}
					int num15 = num3 + num8 + num;
					num = 0;
					if (num15 > 23)
					{
						num = 1;
						num15 -= 24;
					}
					else if (num15 < -23)
					{
						num = -1;
						num15 -= -24;
					}
					int num16 = num2 + num7 + num;
					if (num16 > 999999999 || num16 < -999999999)
					{
						throw new OverflowException();
					}
					if ((num16 >= 0 && num15 >= 0 && num14 >= 0 && num13 >= 0 && num12 >= 0) || (num16 <= 0 && num15 <= 0 && num14 <= 0 && num13 <= 0 && num12 <= 0))
					{
						result = new OracleIntervalDS(num16, num15, num14, num13, num12);
					}
					else
					{
						result = OracleIntervalDS.GetOracleIntervalDS(num16, num15, num14, num13, num12);
					}
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

		// Token: 0x0600168D RID: 5773 RVA: 0x000F0B20 File Offset: 0x000EED20
		public static OracleIntervalDS operator -(OracleIntervalDS value1, OracleIntervalDS value2)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleIntervalDS result;
			try
			{
				if (!value1.m_bNotNull || !value2.m_bNotNull)
				{
					result = OracleIntervalDS.Null;
				}
				else
				{
					int num = 0;
					int num2;
					int num3;
					int num4;
					int num5;
					int num6;
					OracleIntervalDS.GetIDSData(value1.InternalByteRepresentation, out num2, out num3, out num4, out num5, out num6, 0);
					int num7;
					int num8;
					int num9;
					int num10;
					int num11;
					OracleIntervalDS.GetIDSData(value2.InternalByteRepresentation, out num7, out num8, out num9, out num10, out num11, 0);
					int num12 = num6 - num11;
					if (num12 > 999999999)
					{
						num = 1;
						num12 -= 1000000000;
					}
					else if (num12 < -999999999)
					{
						num = -1;
						num12 -= -1000000000;
					}
					int num13 = num5 - num10 + num;
					num = 0;
					if (num13 > 59)
					{
						num = 1;
						num13 -= 60;
					}
					else if (num13 < -59)
					{
						num = -1;
						num13 -= -60;
					}
					int num14 = num4 - num9 + num;
					num = 0;
					if (num14 > 59)
					{
						num = 1;
						num14 -= 60;
					}
					else if (num14 < -59)
					{
						num = -1;
						num14 -= -60;
					}
					int num15 = num3 - num8 + num;
					num = 0;
					if (num15 > 23)
					{
						num = 1;
						num15 -= 24;
					}
					else if (num15 < -23)
					{
						num = -1;
						num15 -= -24;
					}
					int num16 = num2 - num7 + num;
					if (num16 > 999999999 || num16 < -999999999)
					{
						throw new OverflowException();
					}
					if ((num16 >= 0 && num15 >= 0 && num14 >= 0 && num13 >= 0 && num12 >= 0) || (num16 <= 0 && num15 <= 0 && num14 <= 0 && num13 <= 0 && num12 <= 0))
					{
						result = new OracleIntervalDS(num16, num15, num14, num13, num12);
					}
					else
					{
						result = OracleIntervalDS.GetOracleIntervalDS(num16, num15, num14, num13, num12);
					}
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

		// Token: 0x0600168E RID: 5774 RVA: 0x000F0D20 File Offset: 0x000EEF20
		public static OracleIntervalDS operator -(OracleIntervalDS value1)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleIntervalDS result;
			try
			{
				if (!value1.m_bNotNull)
				{
					result = OracleIntervalDS.Null;
				}
				else
				{
					int num;
					int num2;
					int num3;
					int num4;
					int num5;
					OracleIntervalDS.GetIDSData(value1.InternalByteRepresentation, out num, out num2, out num3, out num4, out num5, 0);
					if ((num >= 0 && num2 >= 0 && num3 >= 0 && num4 >= 0 && num5 >= 0) || (num <= 0 && num2 <= 0 && num3 <= 0 && num4 <= 0 && num5 <= 0))
					{
						result = new OracleIntervalDS(-num, -num2, -num3, -num4, -num5);
					}
					else
					{
						result = OracleIntervalDS.GetOracleIntervalDS(-num, -num2, -num3, -num4, -num5);
					}
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

		// Token: 0x0600168F RID: 5775 RVA: 0x000F0E0C File Offset: 0x000EF00C
		public static OracleIntervalDS operator *(OracleIntervalDS value1, int multiplier)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleIntervalDS result;
			try
			{
				if (!value1.m_bNotNull)
				{
					result = OracleIntervalDS.Null;
				}
				else
				{
					long num = 0L;
					int num2;
					int num3;
					int num4;
					int num5;
					int num6;
					OracleIntervalDS.GetIDSData(value1.InternalByteRepresentation, out num2, out num3, out num4, out num5, out num6, 0);
					long num7 = (long)num6 * (long)multiplier;
					if (num7 > 999999999L || num7 < -999999999L)
					{
						num = num7 / 1000000000L;
						num6 = (int)(num7 % 1000000000L);
					}
					else
					{
						num6 = (int)num7;
					}
					num7 = (long)(num5 * multiplier) + num;
					num = 0L;
					if (num7 > 59L || num7 < -59L)
					{
						num = num7 / 60L;
						num5 = (int)num7 % 60;
					}
					else
					{
						num5 = (int)num7;
					}
					num7 = (long)(num4 * multiplier) + num;
					num = 0L;
					if (num7 > 59L || num7 < -59L)
					{
						num = num7 / 60L;
						num4 = (int)num7 % 60;
					}
					else
					{
						num4 = (int)num7;
					}
					num7 = (long)(num3 * multiplier) + num;
					num = 0L;
					if (num7 > 23L || num7 < -23L)
					{
						num = num7 / 24L;
						num3 = (int)num7 % 24;
					}
					else
					{
						num3 = (int)num7;
					}
					num7 = (long)num2 * (long)multiplier + num;
					if (num7 > 999999999L || num7 < -999999999L)
					{
						throw new OverflowException();
					}
					num2 = (int)num7;
					if ((num2 >= 0 && num3 >= 0 && num4 >= 0 && num5 >= 0 && num6 >= 0) || (num2 <= 0 && num3 <= 0 && num4 <= 0 && num5 <= 0 && num6 <= 0))
					{
						result = new OracleIntervalDS(num2, num3, num4, num5, num6);
					}
					else
					{
						result = OracleIntervalDS.GetOracleIntervalDS(num2, num3, num4, num5, num6);
					}
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

		// Token: 0x06001690 RID: 5776 RVA: 0x000F0FE8 File Offset: 0x000EF1E8
		public static OracleIntervalDS operator /(OracleIntervalDS value1, int divisor)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleIntervalDS result;
			try
			{
				if (!value1.m_bNotNull)
				{
					result = OracleIntervalDS.Null;
				}
				else
				{
					if (divisor == 0)
					{
						throw new DivideByZeroException();
					}
					int num;
					int num2;
					int num3;
					int num4;
					int num5;
					OracleIntervalDS.GetIDSData(value1.InternalByteRepresentation, out num, out num2, out num3, out num4, out num5, 0);
					long num6 = (long)num / (long)divisor;
					long num7 = (long)(num % divisor);
					num = (int)num6;
					long num8 = (long)num2 + num7 * 24L;
					num6 = num8 / (long)divisor;
					num7 = num8 % (long)divisor;
					num2 = (int)num6;
					long num9 = (long)num3 + num7 * 60L;
					num6 = num9 / (long)divisor;
					num7 = num9 % (long)divisor;
					num3 = (int)num6;
					long num10 = (long)num4 + num7 * 60L;
					num6 = num10 / (long)divisor;
					num7 = num10 % (long)divisor;
					num4 = (int)num6;
					num6 = ((long)num5 + num7 * 1000000000L) / (long)divisor;
					num5 = (int)num6;
					if ((num >= 0 && num2 >= 0 && num3 >= 0 && num4 >= 0 && num5 >= 0) || (num <= 0 && num2 <= 0 && num3 <= 0 && num4 <= 0 && num5 <= 0))
					{
						result = new OracleIntervalDS(num, num2, num3, num4, num5);
					}
					else
					{
						result = OracleIntervalDS.GetOracleIntervalDS(num, num2, num3, num4, num5);
					}
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

		// Token: 0x06001691 RID: 5777 RVA: 0x000F116C File Offset: 0x000EF36C
		public static explicit operator OracleIntervalDS(string intervalStr)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleIntervalDS result;
			try
			{
				result = new OracleIntervalDS(intervalStr);
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

		// Token: 0x06001692 RID: 5778 RVA: 0x000F11E0 File Offset: 0x000EF3E0
		public static explicit operator TimeSpan(OracleIntervalDS value1)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			TimeSpan result;
			try
			{
				if (!value1.m_bNotNull)
				{
					throw new OracleNullValueException();
				}
				decimal num = OracleIntervalDS.IntervalDSToTicks(value1.InternalByteRepresentation);
				if (num < -9223372036854775808m || num > 9223372036854775807m)
				{
					throw new OverflowException();
				}
				result = new TimeSpan((long)num);
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

		// Token: 0x06001693 RID: 5779 RVA: 0x000F12A8 File Offset: 0x000EF4A8
		public static implicit operator OracleIntervalDS(TimeSpan value1)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleIntervalDS result;
			try
			{
				result = new OracleIntervalDS(value1);
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

		// Token: 0x06001694 RID: 5780 RVA: 0x000F131C File Offset: 0x000EF51C
		public static XmlQualifiedName GetXsdType(XmlSchemaSet schemaSet)
		{
			return new XmlQualifiedName("string", "http://www.w3.org/2001/XMLSchema");
		}

		// Token: 0x06001695 RID: 5781 RVA: 0x000F1330 File Offset: 0x000EF530
		XmlSchema IXmlSerializable.GetSchema()
		{
			return null;
		}

		// Token: 0x06001696 RID: 5782 RVA: 0x000F1334 File Offset: 0x000EF534
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			string text = reader.GetAttribute("null", "http://www.w3.org/2001/XMLSchema-instance");
			if (text != null && XmlConvert.ToBoolean(text))
			{
				this.m_bNotNull = false;
				return;
			}
			text = reader.ReadElementString();
			if (text == null || text.Length == 0)
			{
				throw new FormatException();
			}
			this.m_fSecondPrec = 9;
			this.m_dayPrec = 9;
			this.m_byteRepresentation = this.ToBytes(text);
			this.m_bNotNull = true;
		}

		// Token: 0x06001697 RID: 5783 RVA: 0x000F13B0 File Offset: 0x000EF5B0
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			if (this.m_bNotNull)
			{
				writer.WriteString(this.ToString());
				return;
			}
			writer.WriteAttributeString("xsi", "null", "http://www.w3.org/2001/XMLSchema-instance", "true");
		}

		// Token: 0x06001698 RID: 5784 RVA: 0x000F13E8 File Offset: 0x000EF5E8
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
				if (obj.GetType() != typeof(OracleIntervalDS))
				{
					throw new ArgumentException("obj");
				}
				OracleIntervalDS oracleIntervalDS = (OracleIntervalDS)obj;
				CompareNullEnum compareNullEnum = InternalTypes.CompareNull(!this.m_bNotNull, !oracleIntervalDS.m_bNotNull);
				if (compareNullEnum == CompareNullEnum.BothNull)
				{
					result = 0;
				}
				else if (compareNullEnum == CompareNullEnum.FirstNullOnly)
				{
					result = -1;
				}
				else if (compareNullEnum == CompareNullEnum.SecondNullOnly)
				{
					result = 1;
				}
				else
				{
					byte[] internalByteRepresentation = oracleIntervalDS.InternalByteRepresentation;
					int i = 0;
					while (i < this.m_byteRepresentation.Length)
					{
						if (this.m_byteRepresentation[i] != internalByteRepresentation[i])
						{
							if (this.m_byteRepresentation[i] < internalByteRepresentation[i])
							{
								return -1;
							}
							return 1;
						}
						else
						{
							i++;
						}
					}
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

		// Token: 0x06001699 RID: 5785 RVA: 0x000F1508 File Offset: 0x000EF708
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
					bool flag = true;
					int num;
					int num2;
					int num3;
					int num4;
					int num5;
					OracleIntervalDS.GetIDSData(this.m_byteRepresentation, out num, out num2, out num3, out num4, out num5, 0);
					if (num < 0)
					{
						flag = false;
						num = -num;
						num2 = -num2;
						num3 = -num3;
						num4 = -num4;
						num5 = -num5;
					}
					else if (num2 < 0)
					{
						flag = false;
						num2 = -num2;
						num3 = -num3;
						num4 = -num4;
						num5 = -num5;
					}
					else if (num3 < 0)
					{
						flag = false;
						num3 = -num3;
						num4 = -num4;
						num5 = -num5;
					}
					else if (num4 < 0)
					{
						flag = false;
						num4 = -num4;
						num5 = -num5;
					}
					else if (num5 < 0)
					{
						flag = false;
						num5 = -num5;
					}
					string text = num.ToString();
					string text2 = num5.ToString();
					if (text.Length < (int)this.m_dayPrec)
					{
						text = HelperClass.GetZeros((int)this.m_dayPrec - text.Length) + text;
					}
					int length = text2.Length;
					if (length < (int)this.m_fSecondPrec)
					{
						text2 = HelperClass.GetZeros((int)this.m_fSecondPrec - length) + text2;
					}
					else if (length > (int)this.m_fSecondPrec)
					{
						text2 = text2.Substring(0, (int)this.m_fSecondPrec);
					}
					int length2 = text.Length;
					if (length2 < (int)this.m_dayPrec)
					{
						text = HelperClass.GetZeros((int)this.m_dayPrec - length2) + text;
					}
					else if (length2 > (int)this.m_dayPrec)
					{
						text = text2.Substring(0, (int)this.m_dayPrec);
					}
					StringBuilder stringBuilder = new StringBuilder(30);
					if (flag)
					{
						stringBuilder.Append("+");
					}
					else
					{
						stringBuilder.Append("-");
					}
					if (text.Length == 0)
					{
						stringBuilder.Append("0");
					}
					else
					{
						stringBuilder.Append(text);
					}
					stringBuilder.Append(" ");
					if (num2 <= 9)
					{
						stringBuilder.Append("0");
					}
					stringBuilder.Append(num2);
					stringBuilder.Append(":");
					if (num3 <= 9)
					{
						stringBuilder.Append("0");
					}
					stringBuilder.Append(num3);
					stringBuilder.Append(":");
					if (num4 <= 9)
					{
						stringBuilder.Append("0");
					}
					stringBuilder.Append(num4);
					if (text2 != "")
					{
						stringBuilder.Append(".");
						stringBuilder.Append(text2);
					}
					result = stringBuilder.ToString();
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

		// Token: 0x0600169A RID: 5786 RVA: 0x000F17E0 File Offset: 0x000EF9E0
		public override bool Equals(object obj)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			bool result;
			try
			{
				if (obj == null || obj.GetType() != typeof(OracleIntervalDS))
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

		// Token: 0x0600169B RID: 5787 RVA: 0x000F1878 File Offset: 0x000EFA78
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

		// Token: 0x0600169C RID: 5788 RVA: 0x000F18E4 File Offset: 0x000EFAE4
		internal byte[] ToBytes(string intervalStr)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			byte[] result;
			try
			{
				byte[] array = new byte[11];
				string text = intervalStr.Trim();
				char c = text[0];
				int num;
				if (c != '-' && c != '+')
				{
					num = 0;
				}
				else
				{
					num = 1;
				}
				if (1 == num)
				{
					text = text.Substring(num);
				}
				int num2 = text.IndexOf(' ');
				if (num2 < 0)
				{
					throw new FormatException();
				}
				string text2 = text.Substring(0, num2);
				if (text2.Length > (int)this.m_dayPrec)
				{
					throw new FormatException();
				}
				int num3 = int.Parse(text2);
				string text3 = text.Substring(num2 + 1);
				string[] array2 = text3.Split(new char[]
				{
					':'
				});
				if (array2 == null || array2.Length != 3)
				{
					throw new FormatException();
				}
				string s;
				string s2;
				string s3;
				string text5;
				try
				{
					s = array2[0];
					s2 = array2[1];
					string text4 = array2[2];
					string[] array3 = text4.Split(new char[]
					{
						'.'
					});
					if (array3 == null || array3.Length <= 0 || array3.Length > 2)
					{
						throw new FormatException();
					}
					s3 = array3[0];
					if (array3.Length == 2)
					{
						text5 = array3[1];
					}
					else
					{
						text5 = "0";
					}
				}
				catch (Exception)
				{
					throw new FormatException();
				}
				int num4 = int.Parse(s);
				int num5 = int.Parse(s2);
				int num6 = int.Parse(s3);
				if (num3 > 999999999 || num4 > 23 || num5 > 59 || num6 > 59)
				{
					throw new FormatException();
				}
				if (text5.Length > OracleIntervalDS.MAXLEADPREC)
				{
					throw new FormatException();
				}
				if (text5.Length != OracleIntervalDS.MAXLEADPREC)
				{
					text5 = text5.PadRight(OracleIntervalDS.MAXLEADPREC, '0');
				}
				int num7 = int.Parse(text5);
				if (c == '-')
				{
					num3 = -num3;
					num4 = -num4;
					num5 = -num5;
					num6 = -num6;
					num7 = -num7;
				}
				num3 += (int)OracleIntervalDS.INTERVALDAYOFFSET;
				array[0] = RepConversion.RightShiftFirstNibble(num3);
				array[1] = RepConversion.RightShiftSecondNibble(num3);
				array[2] = RepConversion.RightShiftThirdNibble(num3);
				array[3] = RepConversion.RightShiftFourthNibble(num3);
				array[4] = (byte)((long)num4 + (long)((ulong)OracleIntervalDS.INTERVALDSOFFSET));
				array[5] = (byte)((long)num5 + (long)((ulong)OracleIntervalDS.INTERVALDSOFFSET));
				array[6] = (byte)((long)num6 + (long)((ulong)OracleIntervalDS.INTERVALDSOFFSET));
				num7 += (int)OracleIntervalDS.INTERVALDAYOFFSET;
				array[7] = RepConversion.RightShiftFirstNibble(num7);
				array[8] = RepConversion.RightShiftSecondNibble(num7);
				array[9] = RepConversion.RightShiftThirdNibble(num7);
				array[10] = RepConversion.RightShiftFourthNibble(num7);
				result = array;
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

		// Token: 0x0600169D RID: 5789 RVA: 0x000F1BD4 File Offset: 0x000EFDD4
		internal static int GetIDSData(byte[] byteRepresentation, byte idsComponent)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			int result;
			try
			{
				int num = 0;
				switch (idsComponent)
				{
				case 2:
					if ((uint)byteRepresentation[4] > OracleIntervalDS.LDIREGIDFLAG)
					{
						num = RepConversion.LeftShiftThirdNibble(byteRepresentation[0]);
						num |= RepConversion.LeftShiftFourthNibble(byteRepresentation[1]);
						num -= (int)OracleIntervalDS.INTERVALDAYOFFSET_REGIDFLAG;
						int num2 = (int)RepConversion.GetRegHour(byteRepresentation[2]);
						num2 += (int)RepConversion.GetRegMin(byteRepresentation[3]);
					}
					else
					{
						num = RepConversion.LeftShiftFirstNibble(byteRepresentation[0]);
						num |= RepConversion.LeftShiftSecondNibble(byteRepresentation[1]);
						num |= RepConversion.LeftShiftThirdNibble(byteRepresentation[2]);
						num |= RepConversion.LeftShiftFourthNibble(byteRepresentation[3]);
						num -= (int)OracleIntervalDS.INTERVALDAYOFFSET;
					}
					break;
				case 3:
					if ((uint)byteRepresentation[4] > OracleIntervalDS.LDIREGIDFLAG)
					{
						num = (int)((uint)byteRepresentation[4] - OracleIntervalDS.LDIREGIDSET);
					}
					else
					{
						num = (int)((uint)byteRepresentation[4] - OracleIntervalDS.INTERVALDSOFFSET);
					}
					break;
				case 4:
					num = (int)((uint)byteRepresentation[5] - OracleIntervalDS.INTERVALDSOFFSET);
					break;
				case 5:
					num = (int)((uint)byteRepresentation[6] - OracleIntervalDS.INTERVALDSOFFSET);
					break;
				case 7:
					num = RepConversion.LeftShiftFirstNibble(byteRepresentation[7]);
					num |= RepConversion.LeftShiftSecondNibble(byteRepresentation[8]);
					num |= RepConversion.LeftShiftThirdNibble(byteRepresentation[9]);
					num |= RepConversion.LeftShiftFourthNibble(byteRepresentation[10]);
					num -= (int)OracleIntervalDS.INTERVALDAYOFFSET;
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

		// Token: 0x0600169E RID: 5790 RVA: 0x000F1D6C File Offset: 0x000EFF6C
		internal static void GetIDSData(byte[] byteRepresentation, out int days, out int hours, out int minutes, out int seconds, out int fseconds, int offset = 0)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				if ((uint)byteRepresentation[4 + offset] > OracleIntervalDS.LDIREGIDFLAG)
				{
					days = RepConversion.LeftShiftThirdNibble(byteRepresentation[offset]);
					days |= RepConversion.LeftShiftFourthNibble(byteRepresentation[1 + offset]);
					days -= (int)OracleIntervalDS.INTERVALDAYOFFSET_REGIDFLAG;
					int num = (int)RepConversion.GetRegHour(byteRepresentation[2 + offset]);
					num += (int)RepConversion.GetRegMin(byteRepresentation[3 + offset]);
				}
				else
				{
					days = RepConversion.LeftShiftFirstNibble(byteRepresentation[offset]);
					days |= RepConversion.LeftShiftSecondNibble(byteRepresentation[1 + offset]);
					days |= RepConversion.LeftShiftThirdNibble(byteRepresentation[2 + offset]);
					days |= RepConversion.LeftShiftFourthNibble(byteRepresentation[3 + offset]);
					days -= (int)OracleIntervalDS.INTERVALDAYOFFSET;
				}
				if ((uint)byteRepresentation[4 + offset] > OracleIntervalDS.LDIREGIDFLAG)
				{
					hours = (int)((uint)byteRepresentation[4 + offset] - OracleIntervalDS.LDIREGIDSET);
				}
				else
				{
					hours = (int)((uint)byteRepresentation[4 + offset] - OracleIntervalDS.INTERVALDSOFFSET);
				}
				minutes = (int)((uint)byteRepresentation[5 + offset] - OracleIntervalDS.INTERVALDSOFFSET);
				seconds = (int)((uint)byteRepresentation[6 + offset] - OracleIntervalDS.INTERVALDSOFFSET);
				fseconds = RepConversion.LeftShiftFirstNibble(byteRepresentation[7 + offset]);
				fseconds |= RepConversion.LeftShiftSecondNibble(byteRepresentation[8 + offset]);
				fseconds |= RepConversion.LeftShiftThirdNibble(byteRepresentation[9 + offset]);
				fseconds |= RepConversion.LeftShiftFourthNibble(byteRepresentation[10 + offset]);
				fseconds -= (int)OracleIntervalDS.INTERVALDAYOFFSET;
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

		// Token: 0x0600169F RID: 5791 RVA: 0x000F1F20 File Offset: 0x000F0120
		internal static decimal IntervalDSToTicks(byte[] byteRepresentation)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			decimal result;
			try
			{
				int value;
				int num;
				int num2;
				int num3;
				int num4;
				OracleIntervalDS.GetIDSData(byteRepresentation, out value, out num, out num2, out num3, out num4, 0);
				decimal num5 = value * 864000000000m + (long)num * 36000000000L + (long)num2 * 600000000L + (long)num3 * 10000000L + (decimal)((double)num4 * 0.01);
				result = num5;
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

		// Token: 0x060016A0 RID: 5792 RVA: 0x000F201C File Offset: 0x000F021C
		internal static TimeSpan GetTimeSpan(byte[] byteRepresentation, OracleDbType oraType, int dataOffset = 0, int dataLength = -1)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			TimeSpan result;
			try
			{
				if (oraType != OracleDbType.IntervalDS)
				{
					throw new OracleTypeException(ResourceStringConstants.INT_ERR, new object[0]);
				}
				if (dataLength == -1)
				{
					dataLength = byteRepresentation.Length;
				}
				if (dataLength != 11)
				{
					throw new ArgumentException("byteRepresentation");
				}
				int value;
				int num;
				int num2;
				int num3;
				int num4;
				OracleIntervalDS.GetIDSData(byteRepresentation, out value, out num, out num2, out num3, out num4, dataOffset);
				if (Math.Abs(num4) % 100 > 0)
				{
					throw new OverflowException();
				}
				decimal num5 = value * 864000000000m + (long)num * 36000000000L + (long)num2 * 600000000L + (long)num3 * 10000000L + (decimal)((double)num4 * 0.01);
				if (num5 < -9223372036854775808m || num5 > 9223372036854775807m)
				{
					throw new OverflowException();
				}
				result = new TimeSpan((long)num5);
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

		// Token: 0x060016A1 RID: 5793 RVA: 0x000F21AC File Offset: 0x000F03AC
		internal static OracleIntervalDS GetOracleIntervalDS(int days, int hours, int minutes, int seconds, int nanoseconds)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			OracleIntervalDS result;
			try
			{
				bool flag;
				if (days != 0)
				{
					flag = (days >= 0);
				}
				else if (hours != 0)
				{
					flag = (hours >= 0);
				}
				else if (minutes != 0)
				{
					flag = (minutes >= 0);
				}
				else if (seconds != 0)
				{
					flag = (seconds >= 0);
				}
				else
				{
					flag = (nanoseconds >= 0);
				}
				if (flag)
				{
					if (nanoseconds < 0)
					{
						nanoseconds += 1000000000;
						seconds--;
					}
					if (seconds < 0)
					{
						seconds += 60;
						minutes--;
					}
					if (minutes < 0)
					{
						minutes += 60;
						hours--;
					}
					if (hours < 0)
					{
						hours += 24;
						days--;
					}
				}
				else
				{
					if (nanoseconds > 0)
					{
						nanoseconds -= 1000000000;
						seconds++;
					}
					if (seconds > 0)
					{
						seconds -= 60;
						minutes++;
					}
					if (minutes > 0)
					{
						minutes -= 60;
						hours++;
					}
					if (hours > 0)
					{
						hours -= 24;
						days++;
					}
				}
				result = new OracleIntervalDS(days, hours, minutes, seconds, nanoseconds);
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

		// Token: 0x17000395 RID: 917
		// (get) Token: 0x060016A2 RID: 5794 RVA: 0x000F22F4 File Offset: 0x000F04F4
		internal byte[] InternalByteRepresentation
		{
			get
			{
				return this.m_byteRepresentation;
			}
		}

		// Token: 0x040019F2 RID: 6642
		internal const short MaxArrSize = 11;

		// Token: 0x040019F3 RID: 6643
		internal const byte IDSType = 10;

		// Token: 0x040019F4 RID: 6644
		internal const byte DAY = 2;

		// Token: 0x040019F5 RID: 6645
		internal const byte HOUR = 3;

		// Token: 0x040019F6 RID: 6646
		internal const byte MINUTE = 4;

		// Token: 0x040019F7 RID: 6647
		internal const byte SECOND = 5;

		// Token: 0x040019F8 RID: 6648
		internal const byte MILLISECOND = 6;

		// Token: 0x040019F9 RID: 6649
		internal const byte FSECOND = 7;

		// Token: 0x040019FA RID: 6650
		internal const byte MaxStrLen = 255;

		// Token: 0x040019FB RID: 6651
		private static uint INTERVALDSOFFSET = 60U;

		// Token: 0x040019FC RID: 6652
		private static uint INTERVALDAYOFFSET = 2147483648U;

		// Token: 0x040019FD RID: 6653
		private static int MAXLEADPREC = 9;

		// Token: 0x040019FE RID: 6654
		private static uint INTERVALDAYOFFSET_REGIDFLAG = 32768U;

		// Token: 0x040019FF RID: 6655
		private static uint LDIREGIDSET = 181U;

		// Token: 0x04001A00 RID: 6656
		internal static uint LDIREGIDFLAG = 120U;

		// Token: 0x04001A01 RID: 6657
		private bool m_bNotNull;

		// Token: 0x04001A02 RID: 6658
		private byte m_dayPrec;

		// Token: 0x04001A03 RID: 6659
		private byte m_fSecondPrec;

		// Token: 0x04001A04 RID: 6660
		private byte[] m_byteRepresentation;

		// Token: 0x04001A05 RID: 6661
		public static readonly OracleIntervalDS Null;

		// Token: 0x04001A06 RID: 6662
		internal int initialZoneId;

		// Token: 0x04001A07 RID: 6663
		public static readonly OracleIntervalDS MaxValue = new OracleIntervalDS(999999999, 23, 59, 59, 999999999);

		// Token: 0x04001A08 RID: 6664
		public static readonly OracleIntervalDS MinValue = new OracleIntervalDS(-999999999, -23, -59, -59, -999999999);

		// Token: 0x04001A09 RID: 6665
		public static readonly OracleIntervalDS Zero = new OracleIntervalDS(0, 0, 0, 0, 0);
	}
}
