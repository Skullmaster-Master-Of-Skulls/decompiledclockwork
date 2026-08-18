using System;
using Oracle.ManagedDataAccess.Client;
using OracleInternal.Common;
using OracleInternal.TTC;

namespace Oracle.ManagedDataAccess.Types
{
	// Token: 0x0200025A RID: 602
	internal static class TimeStamp
	{
		// Token: 0x06001848 RID: 6216 RVA: 0x000FFBC0 File Offset: 0x000FDDC0
		private static int setHighOrderbits(int ID)
		{
			return (ID & 8128) >> 6;
		}

		// Token: 0x06001849 RID: 6217 RVA: 0x000FFBCC File Offset: 0x000FDDCC
		private static int setLowOrderbits(int ID)
		{
			return (ID & 63) << 2;
		}

		// Token: 0x0600184A RID: 6218 RVA: 0x000FFBD4 File Offset: 0x000FDDD4
		private static int getHighOrderbits(int hour)
		{
			return (hour & 127) << 6;
		}

		// Token: 0x0600184B RID: 6219 RVA: 0x000FFBDC File Offset: 0x000FDDDC
		private static int getLowOrderbits(int minute)
		{
			return (minute & 252) >> 2;
		}

		// Token: 0x0600184C RID: 6220 RVA: 0x000FFBE8 File Offset: 0x000FDDE8
		internal static bool IsValidDateTime(int year, int month, int day, int hour, int minute, int second, int nanosecond)
		{
			return year >= -4712 && year <= 9999 && month >= 1 && month <= 12 && day >= 1 && day <= 31 && hour >= 0 && hour <= 23 && minute >= 0 && minute <= 59 && second >= 0 && second <= 59 && nanosecond >= 0 && nanosecond <= 999999999 && TimeStamp.GetDays(month, year) >= day;
		}

		// Token: 0x0600184D RID: 6221 RVA: 0x000FFC60 File Offset: 0x000FDE60
		internal static bool IsValidTimeZone(int hourTZ, int minuteTZ)
		{
			return hourTZ >= -12 && hourTZ <= 14 && minuteTZ >= -59 && minuteTZ <= 59;
		}

		// Token: 0x0600184E RID: 6222 RVA: 0x000FFC7C File Offset: 0x000FDE7C
		internal static string GetZoneName(int zoneId)
		{
			return TimeStamp.s_timeZoneObj.GetZoneName(zoneId);
		}

		// Token: 0x0600184F RID: 6223 RVA: 0x000FFC8C File Offset: 0x000FDE8C
		internal static void InitializelatestTZversion()
		{
			TTCDataTypeNegotiation.s_latestTZVersion = TimeStamp.s_timeZoneObj.GetlatestTZversion();
		}

		// Token: 0x06001850 RID: 6224 RVA: 0x000FFCA0 File Offset: 0x000FDEA0
		internal static void ProcessTimeZone(DateTime? date, ref string timeZone, out int hoursTZ, out int minutesTZ, out int zoneID)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				bool flag = true;
				zoneID = -1;
				if (timeZone == null)
				{
					TimeZoneInfo local = TimeZoneInfo.Local;
					if (ZoneIdMap.isValidRegion(local.Id))
					{
						zoneID = ZoneIdMap.GetRegionID(local.Id);
					}
					if (TimeStamp.s_timeZoneObj.IsValidZoneId(zoneID))
					{
						TimeSpan timeSpan;
						bool flag2;
						TimeStamp.s_timeZoneObj.GetOffsetOfLocalTime(date, zoneID, out timeSpan, out flag2);
						hoursTZ = timeSpan.Hours;
						minutesTZ = timeSpan.Minutes;
						timeZone = TimeStamp.s_timeZoneObj.GetZoneName(zoneID);
					}
					else
					{
						hoursTZ = TimeStamp.LocalTZOffset.m_tzHours;
						minutesTZ = TimeStamp.LocalTZOffset.m_tzMinutes;
					}
				}
				else if (timeZone.Contains(":"))
				{
					string text = timeZone.Trim();
					char c = text[0];
					if (c == '-')
					{
						flag = false;
					}
					string[] array = timeZone.Split(new char[]
					{
						':'
					});
					if (array == null || array.Length <= 0)
					{
						throw new InvalidTimeZoneException();
					}
					try
					{
						string s = array[0];
						string s2 = array[1];
						hoursTZ = int.Parse(s);
						minutesTZ = int.Parse(s2);
					}
					catch (Exception)
					{
						throw new InvalidTimeZoneException();
					}
					if (!flag)
					{
						minutesTZ = -minutesTZ;
					}
				}
				else
				{
					if (!TimeStamp.s_timeZoneObj.IsValidZoneName(timeZone))
					{
						throw new InvalidTimeZoneException();
					}
					zoneID = TimeStamp.s_timeZoneObj.GetZoneId(timeZone);
					TimeSpan timeSpan2;
					bool flag3;
					TimeStamp.s_timeZoneObj.GetOffsetOfLocalTime(date, zoneID, out timeSpan2, out flag3);
					hoursTZ = timeSpan2.Hours;
					minutesTZ = timeSpan2.Minutes;
				}
				if (!TimeStamp.IsValidTimeZone(hoursTZ, minutesTZ))
				{
					throw new InvalidTimeZoneException();
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

		// Token: 0x06001851 RID: 6225 RVA: 0x000FFEB0 File Offset: 0x000FE0B0
		internal static void GetTimeZoneOffsetInfo(DateTime? date, byte[] byteRep, out int hourTZ, out int minuteTZ, out int zoneID, out bool dst, bool isDataInLocalTime = false, int offset = 0, int length = -1)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				zoneID = 0;
				dst = false;
				if (length == -1 && byteRep != null)
				{
					length = byteRep.Length - offset;
				}
				if ((byteRep[11 + offset] & 128) != 0)
				{
					zoneID = TimeStamp.getHighOrderbits((int)byteRep[11 + offset]);
					zoneID += TimeStamp.getLowOrderbits((int)byteRep[12 + offset]);
					TimeSpan timeSpan;
					if (isDataInLocalTime)
					{
						TimeStamp.s_timeZoneObj.GetOffsetOfLocalTime(date, zoneID, out timeSpan, out dst);
					}
					else
					{
						TimeStamp.s_timeZoneObj.GetOffsetOfUTCTime(date, zoneID, out timeSpan, out dst);
					}
					hourTZ = timeSpan.Hours;
					minuteTZ = timeSpan.Minutes;
				}
				else
				{
					hourTZ = (int)((byteRep[11 + offset] & byte.MaxValue) - 20);
					minuteTZ = (int)((byteRep[12 + offset] & byte.MaxValue) - 60);
				}
				if (!TimeStamp.IsValidTimeZone(hourTZ, minuteTZ))
				{
					throw new InvalidTimeZoneException();
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

		// Token: 0x06001852 RID: 6226 RVA: 0x000FFFDC File Offset: 0x000FE1DC
		internal static int GetFsecond(DateTime date)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			int result;
			try
			{
				int year = date.Year;
				int month = date.Month;
				int day = date.Day;
				int hour = date.Hour;
				int minute = date.Minute;
				int second = date.Second;
				DateTime dateTime = new DateTime(year, month, day, hour, minute, second);
				long num = date.Ticks - dateTime.Ticks;
				int num2 = (int)(num * 100L);
				result = num2;
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

		// Token: 0x06001853 RID: 6227 RVA: 0x001000B0 File Offset: 0x000FE2B0
		internal static int GetDays(int month, int year)
		{
			int result = 0;
			switch (month)
			{
			case 0:
			case 12:
				result = 31;
				break;
			case 1:
				result = 31;
				break;
			case 2:
				if (CoreFunctions.IsLeapYear(year))
				{
					result = 29;
				}
				else
				{
					result = 28;
				}
				break;
			case 3:
				result = 31;
				break;
			case 4:
				result = 30;
				break;
			case 5:
				result = 31;
				break;
			case 6:
				result = 30;
				break;
			case 7:
				result = 31;
				break;
			case 8:
				result = 31;
				break;
			case 9:
				result = 30;
				break;
			case 10:
				result = 31;
				break;
			case 11:
				result = 30;
				break;
			}
			return result;
		}

		// Token: 0x06001854 RID: 6228 RVA: 0x00100148 File Offset: 0x000FE348
		internal static int GetDaysBetweenMonth(int lowMonth, int lowDays, int upMonth, int upDays, int year)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			int result;
			try
			{
				int num = 0;
				while (lowMonth != upMonth)
				{
					num += DateTime.DaysInMonth((year <= 0) ? ((year < 0) ? (-year) : 4) : year, lowMonth);
					lowMonth++;
				}
				num = num - lowDays + upDays;
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

		// Token: 0x06001855 RID: 6229 RVA: 0x001001E8 File Offset: 0x000FE3E8
		internal static OracleIntervalDS GetDaysBetweenTimeStamp(byte[] byteRepresentation, int lesserHour, int lesserMinute, int lesserSecond, int lesserNanosecond, int upYear, int upMonth, int upDay, int lowYear, int lowMonth, int lowDay, bool isPositive, OracleDbType oraType)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			int num = 0;
			OracleIntervalDS oracleIntervalDS;
			try
			{
				int num2;
				int num3;
				int num4;
				if (oraType == OracleDbType.Date)
				{
					num2 = OracleDate.GetDateData(byteRepresentation, 3) - 1;
					num3 = OracleDate.GetDateData(byteRepresentation, 4) - 1;
					num4 = OracleDate.GetDateData(byteRepresentation, 5) - 1;
				}
				else if (oraType != OracleDbType.TimeStampTZ)
				{
					num2 = TimeStamp.GetTSData(byteRepresentation, 3) - 1;
					num3 = TimeStamp.GetTSData(byteRepresentation, 4) - 1;
					num4 = TimeStamp.GetTSData(byteRepresentation, 5) - 1;
					num = TimeStamp.GetTSData(byteRepresentation, 7);
				}
				else
				{
					num2 = TimeStamp.GetTSTZData(byteRepresentation, 3);
					num3 = TimeStamp.GetTSTZData(byteRepresentation, 4);
					num4 = TimeStamp.GetTSTZData(byteRepresentation, 5);
					num = TimeStamp.GetTSTZData(byteRepresentation, 7);
				}
				int num5 = num - lesserNanosecond;
				int num6 = num4 - lesserSecond;
				int num7 = num3 - lesserMinute;
				int num8 = num2 - lesserHour;
				int num9 = CoreFunctions.DateToJulianDays(upYear, upMonth, upDay) - CoreFunctions.DateToJulianDays(lowYear, lowMonth, lowDay);
				if (isPositive)
				{
					oracleIntervalDS = OracleIntervalDS.GetOracleIntervalDS(num9, num8, num7, num6, num5);
				}
				else
				{
					oracleIntervalDS = OracleIntervalDS.GetOracleIntervalDS(-num9, -num8, -num7, -num6, -num5);
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
			return oracleIntervalDS;
		}

		// Token: 0x06001856 RID: 6230 RVA: 0x0010033C File Offset: 0x000FE53C
		internal static OracleIntervalYM GetYearsBetweenTimeStamp(int sumDay, int sumMonth, int sumYear, int passYear, bool isPositive)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			OracleIntervalYM result;
			try
			{
				int num = 0;
				if (sumDay < 0)
				{
					if (sumMonth <= 0)
					{
						sumMonth += 12;
						sumYear--;
					}
					sumDay += DateTime.DaysInMonth((passYear <= 0) ? ((passYear < 0) ? (-passYear) : 4) : passYear, (sumMonth - 1 == 0) ? 12 : (sumMonth - 1));
					sumMonth--;
				}
				if (sumDay >= 15)
				{
					num = 1;
				}
				sumMonth += num;
				if (sumMonth >= 12)
				{
					sumYear += sumMonth / 12;
					sumMonth %= 12;
				}
				else if (sumMonth < 0)
				{
					sumMonth += 12;
					sumYear--;
				}
				if (isPositive)
				{
					result = new OracleIntervalYM(sumYear, sumMonth);
				}
				else
				{
					result = new OracleIntervalYM(-sumYear, -sumMonth);
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
			return result;
		}

		// Token: 0x06001857 RID: 6231 RVA: 0x00100430 File Offset: 0x000FE630
		internal static byte[] ConstructTimeStampByteRep(int maxArrSize, int year, int month, int day, int hour, int minute, int second, int nanosecond, int hoursTZ = -1, int minutesTZ = -1, int zoneID = -1)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			byte[] result;
			try
			{
				byte[] array = new byte[maxArrSize];
				array[0] = (byte)(year / 100 + 100);
				array[1] = (byte)(year % 100 + 100);
				array[2] = (byte)month;
				array[3] = (byte)day;
				array[4] = (byte)(hour + 1);
				array[5] = (byte)(minute + 1);
				array[6] = (byte)(second + 1);
				if (nanosecond != 0)
				{
					array[7] = (byte)(nanosecond >> 24);
					array[8] = (byte)(nanosecond >> 16 & 255);
					array[9] = (byte)(nanosecond >> 8 & 255);
					array[10] = (byte)(nanosecond & 255);
				}
				if (maxArrSize > 11)
				{
					if (zoneID > 0)
					{
						array[11] = (byte)TimeStamp.setHighOrderbits(zoneID);
						byte[] array2 = array;
						int num = 11;
						array2[num] |= 128;
						array[12] = (byte)TimeStamp.setLowOrderbits(zoneID);
					}
					else
					{
						array[11] = (byte)(hoursTZ + 20);
						array[12] = (byte)(minutesTZ + 60);
					}
				}
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

		// Token: 0x06001858 RID: 6232 RVA: 0x00100568 File Offset: 0x000FE768
		internal static byte[] ConstructByteArrayTS(int year, int month, int day, int hour, int minute, int second, int nanosecond)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			byte[] result;
			try
			{
				result = TimeStamp.ConstructTimeStampByteRep(11, year, month, day, hour, minute, second, nanosecond, -1, -1, -1);
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

		// Token: 0x06001859 RID: 6233 RVA: 0x001005EC File Offset: 0x000FE7EC
		internal static byte[] ConstructUTCByteArray(int year, int month, int day, int hour, int minute, int second, int nanosecond, int hoursTZ, int minutesTZ, int zoneID)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			byte[] result;
			try
			{
				hour -= hoursTZ;
				minute -= minutesTZ;
				TimeStamp.AdjustTimeStampValues(ref year, ref month, ref day, ref hour, ref minute, ref second, ref nanosecond);
				result = TimeStamp.ConstructTimeStampByteRep(13, year, month, day, hour, minute, second, nanosecond, hoursTZ, minutesTZ, zoneID);
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

		// Token: 0x0600185A RID: 6234 RVA: 0x00100694 File Offset: 0x000FE894
		internal static byte[] AddYearMonthToTimeStamp(byte[] byteRepresentation, int yearToBeAdded, long monthToBeAdded, OracleDbType oraType)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			byte[] result;
			try
			{
				TSType tsType;
				if (oraType == OracleDbType.TimeStamp || oraType == OracleDbType.TimeStampLTZ)
				{
					tsType = TSType.TimeStamp;
				}
				else
				{
					tsType = TSType.UtcTimeTZ;
				}
				int num;
				int num2;
				int num3;
				int hour;
				int minute;
				int second;
				int nanosecond;
				int hoursTZ;
				int minutesTZ;
				int zoneID;
				TimeStamp.ExtractTimeStampValuesFromByteArray(byteRepresentation, out num, out num2, out num3, out hour, out minute, out second, out nanosecond, out hoursTZ, out minutesTZ, out zoneID, tsType);
				int num4 = num + yearToBeAdded;
				long num5 = (long)num2 + monthToBeAdded;
				if (num5 > 12L)
				{
					if ((num4 += (int)(num5 / 12L)) > 9999)
					{
						throw new OverflowException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.YEAR_OUT_OF_RANGE, new string[0]));
					}
					if ((num5 %= 12L) == 0L)
					{
						num4--;
						num5 = 12L;
					}
				}
				else if (num5 <= 0L)
				{
					if ((num4 += (int)(num5 / 12L - 1L)) < -4712)
					{
						throw new OverflowException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.YEAR_OUT_OF_RANGE, new string[0]));
					}
					num5 = num5 % 12L + 12L;
				}
				if (num4 < -4712 || num4 > 9999)
				{
					throw new OverflowException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.YEAR_OUT_OF_RANGE, new string[0]));
				}
				int num6 = DateTime.DaysInMonth((num <= 0) ? ((num < 0) ? (-num) : 4) : num, num2);
				int num7 = DateTime.DaysInMonth((num4 <= 0) ? ((num4 < 0) ? (-num4) : 4) : num4, (int)num5);
				num = num4;
				num2 = (int)num5;
				num3 = ((num3 == num6 || num3 > num7) ? num7 : num3);
				if (oraType == OracleDbType.TimeStamp || oraType == OracleDbType.TimeStampLTZ)
				{
					result = TimeStamp.ConstructTimeStampByteRep(11, num, num2, num3, hour, minute, second, nanosecond, -1, -1, -1);
				}
				else
				{
					result = TimeStamp.ConstructTimeStampByteRep(13, num, num2, num3, hour, minute, second, nanosecond, hoursTZ, minutesTZ, zoneID);
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
			return result;
		}

		// Token: 0x0600185B RID: 6235 RVA: 0x00100894 File Offset: 0x000FEA94
		internal static byte[] AddDayTimeToTimeStamp(byte[] byteRepresentation, object value, byte tsComponent, OracleDbType oraType)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			byte[] result;
			try
			{
				TSType tsType;
				if (oraType == OracleDbType.TimeStamp || oraType == OracleDbType.TimeStampLTZ)
				{
					tsType = TSType.TimeStamp;
				}
				else
				{
					tsType = TSType.UtcTimeTZ;
				}
				int num;
				int num2;
				int num3;
				int num4;
				int num5;
				int num6;
				int num7;
				int hoursTZ;
				int minutesTZ;
				int zoneID;
				TimeStamp.ExtractTimeStampValuesFromByteArray(byteRepresentation, out num, out num2, out num3, out num4, out num5, out num6, out num7, out hoursTZ, out minutesTZ, out zoneID, tsType);
				int num8 = num;
				long num9 = (long)num2;
				long num10;
				int num11;
				int num12;
				int num13;
				int num14;
				if (tsComponent == 7)
				{
					num10 = (long)num7 + (long)value;
					num11 = 0;
					num12 = 0;
					num13 = 0;
					num14 = 0;
					int num15 = 0;
				}
				else if (tsComponent == 10)
				{
					TimeSpan timeSpan = (TimeSpan)value;
					num11 = timeSpan.Days;
					num12 = timeSpan.Hours;
					num13 = timeSpan.Minutes;
					num14 = timeSpan.Seconds;
					int num15 = timeSpan.Milliseconds * 1000000;
					num10 = (long)(num7 + num15);
				}
				else if (tsComponent == 11)
				{
					OracleIntervalDS oracleIntervalDS = (OracleIntervalDS)value;
					num11 = oracleIntervalDS.Days;
					num12 = oracleIntervalDS.Hours;
					num13 = oracleIntervalDS.Minutes;
					num14 = oracleIntervalDS.Seconds;
					int num15 = oracleIntervalDS.Nanoseconds;
					num10 = (long)(num7 + num15);
				}
				else
				{
					int num15;
					TimeStamp.ExtractTimeStampValues((double)value, tsComponent, out num11, out num12, out num13, out num14, out num15);
					num10 = (long)(num7 + num15);
				}
				int num16 = 0;
				if (num10 > 999999999L || num10 < 0L)
				{
					num16 = (int)(num10 / 1000000000L);
					num10 %= 1000000000L;
				}
				long num17 = (long)(num6 + num14 + num16);
				num16 = 0;
				if (num17 > 59L || num17 < 0L)
				{
					num16 = (int)(num17 / 60L);
					num17 %= 60L;
				}
				long num18 = (long)(num5 + num13 + num16);
				num16 = 0;
				if (num18 > 59L || num18 < 0L)
				{
					num16 = (int)(num18 / 60L);
					num18 %= 60L;
				}
				long num19 = (long)(num4 + num12 + num16);
				num16 = 0;
				if (num19 > 23L || num19 < 0L)
				{
					num16 = (int)(num19 / 24L);
					num19 %= 24L;
				}
				long num20 = (long)(num3 + num11 + num16);
				if (num20 > 0L)
				{
					while (num20 > (long)DateTime.DaysInMonth((num8 <= 0) ? ((num8 < 0) ? (-num8) : 4) : num8, (int)num9))
					{
						num20 -= (long)DateTime.DaysInMonth((num8 <= 0) ? ((num8 < 0) ? (-num8) : 4) : num8, (int)num9);
						num9 += 1L;
						if (num9 > 12L)
						{
							if (++num8 > 9999)
							{
								throw new OverflowException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.YEAR_OUT_OF_RANGE, new string[0]));
							}
							num9 = 1L;
						}
					}
				}
				else
				{
					while (num20 <= 0L)
					{
						num9 -= 1L;
						if (num9 <= 0L)
						{
							if (--num8 < -4712)
							{
								throw new OverflowException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.YEAR_OUT_OF_RANGE, new string[0]));
							}
							num9 += 12L;
						}
						num20 += (long)DateTime.DaysInMonth((num8 <= 0) ? ((num8 < 0) ? (-num8) : 4) : num8, (int)num9);
					}
				}
				if (num9 <= 0L || num20 <= 0L || num19 < 0L || num18 < 0L || num17 < 0L || num10 < 0L)
				{
					TimeStamp.GetPositiveTimeStampValues(ref num8, ref num9, ref num20, ref num19, ref num18, ref num17, ref num10);
				}
				if (num8 < -4712 || num8 > 9999)
				{
					throw new OverflowException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.YEAR_OUT_OF_RANGE, new string[0]));
				}
				num = num8;
				num2 = (int)num9;
				num3 = (int)num20;
				num4 = (int)num19;
				num5 = (int)num18;
				num6 = (int)num17;
				num7 = (int)num10;
				if (oraType == OracleDbType.TimeStamp || oraType == OracleDbType.TimeStampLTZ)
				{
					result = TimeStamp.ConstructTimeStampByteRep(11, num, num2, num3, num4, num5, num6, num7, -1, -1, -1);
				}
				else
				{
					result = TimeStamp.ConstructTimeStampByteRep(13, num, num2, num3, num4, num5, num6, num7, hoursTZ, minutesTZ, zoneID);
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
			return result;
		}

		// Token: 0x0600185C RID: 6236 RVA: 0x00100C7C File Offset: 0x000FEE7C
		internal static void GetPositiveTimeStampValues(ref int year, ref long month, ref long days, ref long hours, ref long minutes, ref long seconds, ref long nanoseconds)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				if (nanoseconds < 0L)
				{
					nanoseconds += 1000000000L;
					seconds -= 1L;
				}
				if (seconds < 0L)
				{
					seconds += 60L;
					minutes -= 1L;
				}
				if (minutes < 0L)
				{
					minutes += 60L;
					hours -= 1L;
				}
				if (hours < 0L)
				{
					hours += 24L;
					days -= 1L;
				}
				if (month <= 0L)
				{
					month += 12L;
					year--;
				}
				while (days <= 0L)
				{
					days += (long)DateTime.DaysInMonth((year <= 0) ? ((year < 0) ? (-year) : 4) : year, (int)((month - 1L == 0L) ? 12L : (month - 1L)));
					month -= 1L;
					if (month <= 0L)
					{
						month += 12L;
						year--;
					}
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

		// Token: 0x0600185D RID: 6237 RVA: 0x00100DB8 File Offset: 0x000FEFB8
		internal static void AdjustTimeStampValues(ref int year, ref int month, ref int days, ref int hours, ref int minutes, ref int seconds, ref int nanoseconds)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				if (nanoseconds < 0)
				{
					nanoseconds += 1000000000;
					seconds--;
				}
				if (nanoseconds > 999999999)
				{
					nanoseconds -= 1000000000;
					seconds++;
				}
				if (seconds < 0)
				{
					seconds += 60;
					minutes--;
				}
				if (seconds > 59)
				{
					seconds -= 60;
					minutes++;
				}
				if (minutes < 0)
				{
					minutes += 60;
					hours--;
				}
				if (minutes > 59)
				{
					minutes -= 60;
					hours++;
				}
				if (hours < 0)
				{
					hours += 24;
					days--;
				}
				if (hours > 23)
				{
					hours -= 24;
					days++;
				}
				if (month <= 0)
				{
					month += 12;
					year--;
				}
				if (month > 12)
				{
					month -= 12;
					year++;
				}
				while (days <= 0)
				{
					days += DateTime.DaysInMonth((year <= 0) ? ((year < 0) ? (-year) : 4) : year, (month - 1 == 0) ? 12 : (month - 1));
					month--;
					if (month <= 0)
					{
						month += 12;
						year--;
					}
				}
				while (days > DateTime.DaysInMonth((year <= 0) ? ((year < 0) ? (-year) : 4) : year, month))
				{
					days -= DateTime.DaysInMonth((year <= 0) ? ((year < 0) ? (-year) : 4) : year, month);
					month++;
					if (month > 12)
					{
						month -= 12;
						year++;
					}
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

		// Token: 0x0600185E RID: 6238 RVA: 0x00100FC0 File Offset: 0x000FF1C0
		internal static int GetTSData(byte[] byteRep, byte tsComponent)
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
				case 6:
				case 7:
					num = (int)byteRep[7] << 24;
					num |= (int)byteRep[8] << 16;
					num |= (int)byteRep[9] << 8;
					num |= (int)(byteRep[10] & byte.MaxValue);
					if (6 == tsComponent)
					{
						num /= 1000000;
					}
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

		// Token: 0x0600185F RID: 6239 RVA: 0x001010E8 File Offset: 0x000FF2E8
		internal static int GetTSTZData(byte[] byteRep, byte tsComponent)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			int result;
			try
			{
				int num = 0;
				int num2 = (int)((byteRep[0] - 100) * 100 + (byteRep[1] - 100));
				int num3 = (int)(byteRep[2] & byte.MaxValue);
				int num4 = (int)(byteRep[3] & byte.MaxValue);
				int num5 = (int)((byteRep[4] & byte.MaxValue) - 1);
				int num6 = (int)((byteRep[5] & byte.MaxValue) - 1);
				int second = (int)((byteRep[6] & byte.MaxValue) - 1);
				DateTime? date = null;
				if (num2 >= 1800)
				{
					date = new DateTime?(new DateTime(num2, num3, num4, num5, num6, second));
				}
				int num7;
				int num8;
				int num9;
				bool flag;
				TimeStamp.GetTimeZoneOffsetInfo(date, byteRep, out num7, out num8, out num9, out flag, false, 0, -1);
				num6 += num8;
				num5 += num7;
				int num10 = 0;
				TimeStamp.AdjustTimeStampValues(ref num2, ref num3, ref num4, ref num5, ref num6, ref second, ref num10);
				switch (tsComponent)
				{
				case 0:
					num = num2;
					break;
				case 1:
					num = num3;
					break;
				case 2:
					num = num4;
					break;
				case 3:
					num = num5;
					break;
				case 4:
					num = num6;
					break;
				case 5:
					num = (int)((byteRep[6] & byte.MaxValue) - 1);
					break;
				case 6:
				case 7:
					num = (int)byteRep[7] << 24;
					num |= (int)byteRep[8] << 16;
					num |= (int)byteRep[9] << 8;
					num |= (int)(byteRep[10] & byte.MaxValue);
					if (6 == tsComponent)
					{
						num /= 1000000;
					}
					break;
				case 8:
					num = num7;
					break;
				case 9:
					num = num8;
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

		// Token: 0x06001860 RID: 6240 RVA: 0x001012B4 File Offset: 0x000FF4B4
		internal static void ExtractTimeStampValues(double data, byte tsComponent, out int day, out int hour, out int minute, out int second, out int nanosecond)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				decimal num = (decimal)data;
				day = 0;
				hour = 0;
				minute = 0;
				second = 0;
				nanosecond = 0;
				switch (tsComponent)
				{
				case 2:
				{
					day = (int)num;
					decimal d = num - day;
					num = d * 24m;
					hour = (int)num;
					d = num - hour;
					num = d * 60m;
					minute = (int)num;
					d = num - minute;
					num = d * 60m;
					second = (int)num;
					d = num - second;
					nanosecond = (int)(d * 1000000000m);
					break;
				}
				case 3:
				{
					hour = (int)num;
					decimal d = num - hour;
					num = d * 60m;
					minute = (int)num;
					d = num - minute;
					num = d * 60m;
					second = (int)num;
					d = num - second;
					nanosecond = (int)(d * 1000000000m);
					break;
				}
				case 4:
				{
					minute = (int)num;
					decimal d = num - minute;
					num = d * 60m;
					second = (int)num;
					d = num - second;
					nanosecond = (int)(d * 1000000000m);
					break;
				}
				case 5:
				{
					second = (int)num;
					decimal d = num - second;
					nanosecond = (int)(d * 1000000000m);
					break;
				}
				case 7:
					nanosecond = (int)data;
					break;
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

		// Token: 0x06001861 RID: 6241 RVA: 0x00101534 File Offset: 0x000FF734
		internal static void ExtractTimeStampValuesFromByteArray(byte[] binData, out int year, out int month, out int day, out int hour, out int minute, out int second, out int nanosecond, out int hourTZ, out int minuteTZ, out int zoneID, TSType tsType)
		{
			year = (int)((binData[0] - 100) * 100 + (binData[1] - 100));
			month = (int)(binData[2] & byte.MaxValue);
			day = (int)(binData[3] & byte.MaxValue);
			hour = (int)((binData[4] & byte.MaxValue) - 1);
			minute = (int)((binData[5] & byte.MaxValue) - 1);
			second = (int)((binData[6] & byte.MaxValue) - 1);
			int num = (int)binData[7] << 24;
			num |= (int)binData[8] << 16;
			num |= (int)binData[9] << 8;
			num |= (int)(binData[10] & byte.MaxValue);
			nanosecond = num;
			minuteTZ = -1;
			hourTZ = -1;
			zoneID = -1;
			if (tsType == TSType.LocalTimeTZ || tsType == TSType.UtcTimeTZ)
			{
				DateTime? date = null;
				if (year >= 1800)
				{
					date = new DateTime?(new DateTime(year, month, day, hour, minute, second));
				}
				bool flag;
				TimeStamp.GetTimeZoneOffsetInfo(date, binData, out hourTZ, out minuteTZ, out zoneID, out flag, false, 0, -1);
				if (tsType == TSType.LocalTimeTZ)
				{
					minute += minuteTZ;
					hour += hourTZ;
					TimeStamp.AdjustTimeStampValues(ref year, ref month, ref day, ref hour, ref minute, ref second, ref nanosecond);
				}
			}
		}

		// Token: 0x06001862 RID: 6242 RVA: 0x0010163C File Offset: 0x000FF83C
		internal static TimeSpan GetLTZOffset(byte[] dbTimeZoneBytes, OracleIntervalDS sessionTimeZone)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			TimeSpan result;
			try
			{
				TimeSpan t = new TimeSpan(sessionTimeZone.Hours, sessionTimeZone.Minutes, 0);
				int idsdata = OracleIntervalDS.GetIDSData(dbTimeZoneBytes, 3);
				int idsdata2 = OracleIntervalDS.GetIDSData(dbTimeZoneBytes, 4);
				TimeSpan t2 = new TimeSpan(idsdata, idsdata2, 0);
				TimeSpan timeSpan = t2 - t;
				result = timeSpan;
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

		// Token: 0x06001863 RID: 6243 RVA: 0x001016F0 File Offset: 0x000FF8F0
		internal static byte[] GetAdjustedTimeStampLTZ(byte[] bindata, TimeSpan adjTimeZone, bool isSend)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			byte[] result;
			try
			{
				int year;
				int month;
				int day;
				int num;
				int num2;
				int second;
				int nanosecond;
				int num3;
				int num4;
				int num5;
				TimeStamp.ExtractTimeStampValuesFromByteArray(bindata, out year, out month, out day, out num, out num2, out second, out nanosecond, out num3, out num4, out num5, TSType.TimeStampLTZ);
				if (isSend)
				{
					num += adjTimeZone.Hours;
					num2 += adjTimeZone.Minutes;
				}
				else
				{
					num -= adjTimeZone.Hours;
					num2 -= adjTimeZone.Minutes;
				}
				TimeStamp.AdjustTimeStampValues(ref year, ref month, ref day, ref num, ref num2, ref second, ref nanosecond);
				result = TimeStamp.ConstructByteArrayTS(year, month, day, num, num2, second, nanosecond);
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

		// Token: 0x06001864 RID: 6244 RVA: 0x001017D0 File Offset: 0x000FF9D0
		internal static byte[] GetLocalTimeFromUTCByteRep(byte[] bindata)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			byte[] result;
			try
			{
				int num = (int)((bindata[0] - 100) * 100 + (bindata[1] - 100));
				int month = (int)(bindata[2] & byte.MaxValue);
				int day = (int)(bindata[3] & byte.MaxValue);
				int num2 = (int)((bindata[4] & byte.MaxValue) - 1);
				int num3 = (int)((bindata[5] & byte.MaxValue) - 1);
				int second = (int)((bindata[6] & byte.MaxValue) - 1);
				int num4 = (int)bindata[7] << 24;
				num4 |= (int)bindata[8] << 16;
				num4 |= (int)bindata[9] << 8;
				num4 |= (int)(bindata[10] & byte.MaxValue);
				DateTime? date = null;
				if (num >= 1800)
				{
					date = new DateTime?(new DateTime(num, month, day, num2, num3, second));
				}
				int num5;
				int num6;
				int zoneID;
				bool flag;
				TimeStamp.GetTimeZoneOffsetInfo(date, bindata, out num5, out num6, out zoneID, out flag, false, 0, -1);
				num2 += num5;
				num3 += num6;
				TimeStamp.AdjustTimeStampValues(ref num, ref month, ref day, ref num2, ref num3, ref second, ref num4);
				byte[] array = TimeStamp.ConstructTimeStampByteRep(13, num, month, day, num2, num3, second, num4, num5, num6, zoneID);
				if ((array[11] & 128) != 0)
				{
					byte[] array2 = array;
					int num7 = 12;
					array2[num7] |= 1;
					if (flag)
					{
						byte[] array3 = array;
						int num8 = 12;
						array3[num8] |= 2;
					}
				}
				else
				{
					byte[] array4 = array;
					int num9 = 11;
					array4[num9] |= 64;
				}
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

		// Token: 0x06001865 RID: 6245 RVA: 0x001019A4 File Offset: 0x000FFBA4
		internal static void GetUTCByteRepFromLocalArray(byte[] bindata, out byte[] utcArray, out DateTime? dateObj, bool returnAsDateTime = false)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				int num = (int)((bindata[0] - 100) * 100 + (bindata[1] - 100));
				int month = (int)(bindata[2] & byte.MaxValue);
				int day = (int)(bindata[3] & byte.MaxValue);
				int num2 = (int)((bindata[4] & byte.MaxValue) - 1);
				int num3 = (int)((bindata[5] & byte.MaxValue) - 1);
				int second = (int)((bindata[6] & byte.MaxValue) - 1);
				int num4 = (int)bindata[7] << 24;
				num4 |= (int)bindata[8] << 16;
				num4 |= (int)bindata[9] << 8;
				num4 |= (int)(bindata[10] & byte.MaxValue);
				utcArray = null;
				dateObj = null;
				if (!returnAsDateTime)
				{
					if ((128 & bindata[11]) != 0)
					{
						int num5 = 12;
						bindata[num5] &= 254;
						if ((bindata[12] & 2) != 0)
						{
						}
					}
					else
					{
						int num6 = 11;
						bindata[num6] &= 191;
					}
					dateObj = null;
					if (num >= 1800)
					{
						dateObj = new DateTime?(new DateTime(num, month, day, num2, num3, second));
					}
					int num7;
					int num8;
					int zoneID;
					bool flag;
					TimeStamp.GetTimeZoneOffsetInfo(dateObj, bindata, out num7, out num8, out zoneID, out flag, true, 0, -1);
					num2 -= num7;
					num3 -= num8;
					TimeStamp.AdjustTimeStampValues(ref num, ref month, ref day, ref num2, ref num3, ref second, ref num4);
					utcArray = TimeStamp.ConstructTimeStampByteRep(13, num, month, day, num2, num3, second, num4, num7, num8, zoneID);
				}
				else
				{
					int millisecond = num4 / 1000000;
					int num9 = num4 % 1000000;
					dateObj = new DateTime?(new DateTime(num, month, day, num2, num3, second, millisecond));
					if (num9 > 0)
					{
						dateObj = new DateTime?(dateObj.Value.AddTicks((long)(num9 / 100)));
					}
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

		// Token: 0x06001866 RID: 6246 RVA: 0x00101BD8 File Offset: 0x000FFDD8
		internal static void ToUniversalTime(ref int year, ref int month, ref int day, ref int hour, ref int minute, ref int second, ref int nanosecond, ref int hoursTZ, ref int minutesTZ)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				hour -= hoursTZ;
				minute -= minutesTZ;
				TimeStamp.AdjustTimeStampValues(ref year, ref month, ref day, ref hour, ref minute, ref second, ref nanosecond);
				hoursTZ = 0;
				minutesTZ = 0;
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

		// Token: 0x06001867 RID: 6247 RVA: 0x00101C70 File Offset: 0x000FFE70
		internal static OracleTimeStampLTZ ToLocalTime(int year, int month, int day, int hour, int minute, int second, int nanosecond, int hoursTZ, int minutesTZ)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			OracleTimeStampLTZ result;
			try
			{
				TimeStamp.ToUniversalTime(ref year, ref month, ref day, ref hour, ref minute, ref second, ref nanosecond, ref hoursTZ, ref minutesTZ);
				TimeZoneInfo local = TimeZoneInfo.Local;
				if (ZoneIdMap.isValidRegion(local.Id))
				{
					int regionID = ZoneIdMap.GetRegionID(local.Id);
					if (regionID > 0)
					{
						DateTime? utcTime = null;
						if (year >= 1800)
						{
							utcTime = new DateTime?(new DateTime(year, month, day, hour, minute, second));
						}
						TimeSpan timeSpan;
						bool flag;
						TimeStamp.s_timeZoneObj.GetOffsetOfUTCTime(utcTime, regionID, out timeSpan, out flag);
						minute += timeSpan.Minutes;
						hour += timeSpan.Hours;
					}
				}
				else
				{
					minute += TimeStamp.LocalTZOffset.m_tzMinutes;
					hour += TimeStamp.LocalTZOffset.m_tzHours;
				}
				TimeStamp.AdjustTimeStampValues(ref year, ref month, ref day, ref hour, ref minute, ref second, ref nanosecond);
				result = new OracleTimeStampLTZ(year, month, day, hour, minute, second, nanosecond);
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

		// Token: 0x06001868 RID: 6248 RVA: 0x00101DAC File Offset: 0x000FFFAC
		internal static byte[] ConvertLTZDataToDBTime(byte[] ltzByteArray, OracleTimeZoneInfo? dbTimeZoneBytes, OracleIntervalDS sessionTimeZone)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			byte[] result;
			try
			{
				int num;
				int month;
				int day;
				int num2;
				int num3;
				int second;
				int nanosecond;
				int num4;
				int num5;
				int zoneId;
				TimeStamp.ExtractTimeStampValuesFromByteArray(ltzByteArray, out num, out month, out day, out num2, out num3, out second, out nanosecond, out num4, out num5, out zoneId, TSType.TimeStampLTZ);
				int num6 = 0;
				OracleTimeZoneInfo timeZoneInfo;
				if (sessionTimeZone.initialZoneId != 0)
				{
					timeZoneInfo = new OracleTimeZoneInfo(sessionTimeZone.initialZoneId, sessionTimeZone.Hours, sessionTimeZone.Minutes);
				}
				else
				{
					timeZoneInfo = TimeStamp.GetTimeZoneInfo(sessionTimeZone.BinData);
				}
				int zoneId2 = timeZoneInfo.m_zoneId;
				int num7;
				if (zoneId2 > 0)
				{
					DateTime? localTime = null;
					if (num >= 1800)
					{
						localTime = new DateTime?(new DateTime(num, month, day, num2, num3, second));
					}
					TimeSpan timeSpan;
					bool flag;
					TimeStamp.s_timeZoneObj.GetOffsetOfLocalTime(localTime, zoneId2, out timeSpan, out flag);
					num7 = timeSpan.Hours;
					num6 = timeSpan.Minutes;
				}
				else
				{
					num7 = timeZoneInfo.m_tzHours;
					num6 = timeZoneInfo.m_tzMinutes;
				}
				TimeStamp.ToUniversalTime(ref num, ref month, ref day, ref num2, ref num3, ref second, ref nanosecond, ref num7, ref num6);
				zoneId = dbTimeZoneBytes.Value.m_zoneId;
				if (zoneId > 0)
				{
					DateTime? utcTime = null;
					if (num >= 1800)
					{
						utcTime = new DateTime?(new DateTime(num, month, day, num2, num3, second));
					}
					TimeSpan timeSpan2;
					bool flag2;
					TimeStamp.s_timeZoneObj.GetOffsetOfUTCTime(utcTime, zoneId, out timeSpan2, out flag2);
					num4 = timeSpan2.Hours;
					num5 = timeSpan2.Minutes;
				}
				else
				{
					num4 = dbTimeZoneBytes.Value.m_tzHours;
					num5 = dbTimeZoneBytes.Value.m_tzMinutes;
				}
				num2 += num4;
				num3 += num5;
				TimeStamp.AdjustTimeStampValues(ref num, ref month, ref day, ref num2, ref num3, ref second, ref nanosecond);
				result = TimeStamp.ConstructByteArrayTS(num, month, day, num2, num3, second, nanosecond);
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

		// Token: 0x06001869 RID: 6249 RVA: 0x00101FB4 File Offset: 0x001001B4
		internal static void ConvertDBTimeToLTZData(byte[] dbByteArray, OracleTimeZoneInfo? dbTimeZoneBytes, OracleIntervalDS sessionTimeZone, out byte[] byteRep, out DateTime? dateObj, bool returnAsDateTimeObj = false)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				dateObj = null;
				int num;
				int month;
				int day;
				int num2;
				int num3;
				int second;
				int num4;
				int num5;
				int num6;
				int zoneId;
				TimeStamp.ExtractTimeStampValuesFromByteArray(dbByteArray, out num, out month, out day, out num2, out num3, out second, out num4, out num5, out num6, out zoneId, TSType.TimeStampLTZ);
				zoneId = dbTimeZoneBytes.Value.m_zoneId;
				if (zoneId > 0)
				{
					dateObj = null;
					if (num >= 1800)
					{
						dateObj = new DateTime?(new DateTime(num, month, day, num2, num3, second));
					}
					TimeSpan timeSpan;
					bool flag;
					TimeStamp.s_timeZoneObj.GetOffsetOfLocalTime(dateObj, zoneId, out timeSpan, out flag);
					num5 = timeSpan.Hours;
					num6 = timeSpan.Minutes;
				}
				else
				{
					num5 = dbTimeZoneBytes.Value.m_tzHours;
					num6 = dbTimeZoneBytes.Value.m_tzMinutes;
				}
				TimeStamp.ToUniversalTime(ref num, ref month, ref day, ref num2, ref num3, ref second, ref num4, ref num5, ref num6);
				OracleTimeZoneInfo timeZoneInfo;
				if (sessionTimeZone.initialZoneId != 0)
				{
					timeZoneInfo = new OracleTimeZoneInfo(sessionTimeZone.initialZoneId, sessionTimeZone.Hours, sessionTimeZone.Minutes);
				}
				else
				{
					timeZoneInfo = TimeStamp.GetTimeZoneInfo(sessionTimeZone.BinData);
				}
				int zoneId2 = timeZoneInfo.m_zoneId;
				int num7;
				int num8;
				if (zoneId2 > 0)
				{
					dateObj = null;
					if (num >= 1800)
					{
						dateObj = new DateTime?(new DateTime(num, month, day, num2, num3, second));
					}
					TimeSpan timeSpan2;
					bool flag2;
					TimeStamp.s_timeZoneObj.GetOffsetOfUTCTime(dateObj, zoneId2, out timeSpan2, out flag2);
					num7 = timeSpan2.Hours;
					num8 = timeSpan2.Minutes;
				}
				else
				{
					num7 = timeZoneInfo.m_tzHours;
					num8 = timeZoneInfo.m_tzMinutes;
				}
				num2 += num7;
				num3 += num8;
				TimeStamp.AdjustTimeStampValues(ref num, ref month, ref day, ref num2, ref num3, ref second, ref num4);
				byteRep = null;
				if (!returnAsDateTimeObj)
				{
					byteRep = TimeStamp.ConstructByteArrayTS(num, month, day, num2, num3, second, num4);
				}
				else
				{
					int millisecond = num4 / 1000000;
					int num9 = num4 % 1000000;
					dateObj = new DateTime?(new DateTime(num, month, day, num2, num3, second, millisecond));
					if (num9 > 0)
					{
						dateObj = new DateTime?(dateObj.Value.AddTicks((long)(num9 / 100)));
					}
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

		// Token: 0x0600186A RID: 6250 RVA: 0x00102238 File Offset: 0x00100438
		internal static TZInfo GetLocalTZOffset()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			TZInfo result;
			try
			{
				TimeZone currentTimeZone = TimeZone.CurrentTimeZone;
				DateTime now = DateTime.Now;
				TimeSpan utcOffset = currentTimeZone.GetUtcOffset(now);
				result = new TZInfo(utcOffset.Hours, utcOffset.Minutes);
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

		// Token: 0x0600186B RID: 6251 RVA: 0x001022D0 File Offset: 0x001004D0
		internal static OracleTimeZoneInfo GetTimeZoneInfo(byte[] dbTimeZoneBytes)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			OracleTimeZoneInfo result;
			try
			{
				int num = -1;
				int tzHours = -1;
				int tzMinutes = -1;
				if ((uint)dbTimeZoneBytes[4] > OracleIntervalDS.LDIREGIDFLAG)
				{
					num = (int)RepConversion.GetRegHour(dbTimeZoneBytes[2]);
					num += (int)RepConversion.GetRegMin(dbTimeZoneBytes[3]);
				}
				else
				{
					tzHours = OracleIntervalDS.GetIDSData(dbTimeZoneBytes, 3);
					tzMinutes = OracleIntervalDS.GetIDSData(dbTimeZoneBytes, 4);
				}
				result = new OracleTimeZoneInfo(num, tzHours, tzMinutes);
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

		// Token: 0x04001A7F RID: 6783
		internal const short DaysPerYear = 365;

		// Token: 0x04001A80 RID: 6784
		internal const short MaxYear = 9999;

		// Token: 0x04001A81 RID: 6785
		internal const byte MaxMonth = 12;

		// Token: 0x04001A82 RID: 6786
		internal const byte MaxDay = 31;

		// Token: 0x04001A83 RID: 6787
		internal const byte MaxHour = 23;

		// Token: 0x04001A84 RID: 6788
		internal const byte MaxMinute = 59;

		// Token: 0x04001A85 RID: 6789
		internal const byte MaxSecond = 59;

		// Token: 0x04001A86 RID: 6790
		internal const double MaxMillisecond = 999.999999;

		// Token: 0x04001A87 RID: 6791
		internal const int MaxFSecond = 999999999;

		// Token: 0x04001A88 RID: 6792
		internal const int MaxTZHours = 14;

		// Token: 0x04001A89 RID: 6793
		internal const int MaxTZMinutes = 59;

		// Token: 0x04001A8A RID: 6794
		internal const short MinYear = -4712;

		// Token: 0x04001A8B RID: 6795
		internal const byte MinMonth = 1;

		// Token: 0x04001A8C RID: 6796
		internal const byte MinDay = 1;

		// Token: 0x04001A8D RID: 6797
		internal const byte MinHour = 0;

		// Token: 0x04001A8E RID: 6798
		internal const byte MinMinute = 0;

		// Token: 0x04001A8F RID: 6799
		internal const byte MinSecond = 0;

		// Token: 0x04001A90 RID: 6800
		internal const double MinMillisecond = 0.0;

		// Token: 0x04001A91 RID: 6801
		internal const byte MinFSecond = 0;

		// Token: 0x04001A92 RID: 6802
		internal const int MinTZHours = -12;

		// Token: 0x04001A93 RID: 6803
		internal const int MinTZMinutes = -59;

		// Token: 0x04001A94 RID: 6804
		internal const byte MaxFSecondPrec = 9;

		// Token: 0x04001A95 RID: 6805
		internal const byte MinFSecondPrec = 0;

		// Token: 0x04001A96 RID: 6806
		private const int MaxTZHFormatLength = 6;

		// Token: 0x04001A97 RID: 6807
		internal const int MinTransYear = 1800;

		// Token: 0x04001A98 RID: 6808
		internal const byte YEAR = 0;

		// Token: 0x04001A99 RID: 6809
		internal const byte MONTH = 1;

		// Token: 0x04001A9A RID: 6810
		internal const byte DAY = 2;

		// Token: 0x04001A9B RID: 6811
		internal const byte HOUR = 3;

		// Token: 0x04001A9C RID: 6812
		internal const byte MINUTE = 4;

		// Token: 0x04001A9D RID: 6813
		internal const byte SECOND = 5;

		// Token: 0x04001A9E RID: 6814
		internal const byte MILLISECOND = 6;

		// Token: 0x04001A9F RID: 6815
		internal const byte FSECOND = 7;

		// Token: 0x04001AA0 RID: 6816
		internal const byte TZHOURS = 8;

		// Token: 0x04001AA1 RID: 6817
		internal const byte TZMINUTES = 9;

		// Token: 0x04001AA2 RID: 6818
		internal const byte TIMESPAN = 10;

		// Token: 0x04001AA3 RID: 6819
		internal const byte DAYTOSECOND = 11;

		// Token: 0x04001AA4 RID: 6820
		private const byte DLSBIT = 2;

		// Token: 0x04001AA5 RID: 6821
		private const byte REGLOCBIT = 1;

		// Token: 0x04001AA6 RID: 6822
		private const byte NOTREGLOCBIT = 254;

		// Token: 0x04001AA7 RID: 6823
		private const byte MINLOCBIT = 64;

		// Token: 0x04001AA8 RID: 6824
		private const byte NOTMINLOCBIT = 191;

		// Token: 0x04001AA9 RID: 6825
		private const byte REGIDBIT = 128;

		// Token: 0x04001AAA RID: 6826
		private const int OFFSET_HOUR = 20;

		// Token: 0x04001AAB RID: 6827
		private const int OFFSET_MINUTE = 60;

		// Token: 0x04001AAC RID: 6828
		internal static readonly OracleTimeZone s_timeZoneObj = OracleTimeZone.GetInstance();

		// Token: 0x04001AAD RID: 6829
		internal static readonly TZInfo LocalTZOffset = TimeStamp.GetLocalTZOffset();
	}
}
