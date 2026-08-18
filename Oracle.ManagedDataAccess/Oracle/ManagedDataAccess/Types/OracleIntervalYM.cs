using System;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Oracle.ManagedDataAccess.Client;
using OracleInternal.Common;

namespace Oracle.ManagedDataAccess.Types
{
	// Token: 0x0200024C RID: 588
	[XmlSchemaProvider("GetXsdType")]
	[Serializable]
	public struct OracleIntervalYM : IComparable, IXmlSerializable, INullable
	{
		// Token: 0x060016A4 RID: 5796 RVA: 0x000F2380 File Offset: 0x000F0580
		public OracleIntervalYM(int years, int months)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				if (!Interval.IsValidYM(years, months))
				{
					throw new ArgumentOutOfRangeException();
				}
				this.m_bNotNull = true;
				this.m_yearPrec = 9;
				this.m_byteRepresentation = new byte[5];
				years += (int)OracleIntervalYM.INTYMYEAROFFSET;
				this.m_byteRepresentation[0] = RepConversion.RightShiftFirstNibble(years);
				this.m_byteRepresentation[1] = RepConversion.RightShiftSecondNibble(years);
				this.m_byteRepresentation[2] = RepConversion.RightShiftThirdNibble(years);
				this.m_byteRepresentation[3] = RepConversion.RightShiftFourthNibble(years);
				this.m_byteRepresentation[4] = (byte)((long)months + (long)((ulong)OracleIntervalYM.INTYMMONTHOFFSET));
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

		// Token: 0x060016A5 RID: 5797 RVA: 0x000F2470 File Offset: 0x000F0670
		public OracleIntervalYM(long totalMonths)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				if (!Interval.IsValidYMMonths(totalMonths))
				{
					throw new ArgumentOutOfRangeException("totalMonths");
				}
				this.m_bNotNull = true;
				this.m_byteRepresentation = new byte[5];
				this.m_yearPrec = 9;
				int num = (int)(totalMonths / 12L);
				int num2 = (int)(totalMonths % 12L);
				num += (int)OracleIntervalYM.INTYMYEAROFFSET;
				this.m_byteRepresentation[0] = RepConversion.RightShiftFirstNibble(num);
				this.m_byteRepresentation[1] = RepConversion.RightShiftSecondNibble(num);
				this.m_byteRepresentation[2] = RepConversion.RightShiftThirdNibble(num);
				this.m_byteRepresentation[3] = RepConversion.RightShiftFourthNibble(num);
				this.m_byteRepresentation[4] = (byte)((long)num2 + (long)((ulong)OracleIntervalYM.INTYMMONTHOFFSET));
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

		// Token: 0x060016A6 RID: 5798 RVA: 0x000F2570 File Offset: 0x000F0770
		public OracleIntervalYM(double totalYears)
		{
			this = new OracleIntervalYM((int)totalYears, (int)Math.Ceiling((totalYears - (double)((int)totalYears)) * 12.0));
		}

		// Token: 0x060016A7 RID: 5799 RVA: 0x000F2590 File Offset: 0x000F0790
		public OracleIntervalYM(string intervalStr)
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
				this.m_yearPrec = 9;
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

		// Token: 0x060016A8 RID: 5800 RVA: 0x000F2644 File Offset: 0x000F0844
		public OracleIntervalYM(byte[] binData)
		{
			this = new OracleIntervalYM(binData, true);
		}

		// Token: 0x060016A9 RID: 5801 RVA: 0x000F2650 File Offset: 0x000F0850
		internal OracleIntervalYM(byte[] binData, int precision, bool bCopyData = true)
		{
			this = new OracleIntervalYM(binData, bCopyData);
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				int num = OracleIntervalYM.GetIYMData(this.m_byteRepresentation, 0, 0);
				if (num < 0)
				{
					num = -num;
				}
				if (num != 0 && num.ToString().Length > precision)
				{
					throw new OverflowException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.YEAR_OUT_OF_RANGE, new string[0]));
				}
				this.m_yearPrec = (byte)precision;
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

		// Token: 0x060016AA RID: 5802 RVA: 0x000F270C File Offset: 0x000F090C
		internal OracleIntervalYM(byte[] binData, bool bCopyData)
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
				this.m_yearPrec = 9;
				if (binData.Length != 5)
				{
					throw new ArgumentException("binData");
				}
				if (bCopyData)
				{
					this.m_byteRepresentation = new byte[5];
					Array.Copy(binData, this.m_byteRepresentation, 5);
				}
				else
				{
					this.m_byteRepresentation = binData;
				}
				int iymdata = OracleIntervalYM.GetIYMData(this.m_byteRepresentation, 0, 0);
				int iymdata2 = OracleIntervalYM.GetIYMData(this.m_byteRepresentation, 1, 0);
				if (!Interval.IsValidYM(iymdata, iymdata2))
				{
					throw new ArgumentOutOfRangeException();
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

		// Token: 0x17000396 RID: 918
		// (get) Token: 0x060016AB RID: 5803 RVA: 0x000F27F8 File Offset: 0x000F09F8
		public byte[] BinData
		{
			get
			{
				if (this.m_bNotNull)
				{
					byte[] array = new byte[5];
					if (this.m_byteRepresentation != null)
					{
						Array.Copy(this.m_byteRepresentation, array, 5);
					}
					return array;
				}
				throw new OracleNullValueException();
			}
		}

		// Token: 0x17000397 RID: 919
		// (get) Token: 0x060016AC RID: 5804 RVA: 0x000F2830 File Offset: 0x000F0A30
		public bool IsNull
		{
			get
			{
				return !this.m_bNotNull;
			}
		}

		// Token: 0x17000398 RID: 920
		// (get) Token: 0x060016AD RID: 5805 RVA: 0x000F283C File Offset: 0x000F0A3C
		public long Value
		{
			get
			{
				if (this.m_bNotNull)
				{
					return OracleIntervalYM.GetLong(this.m_byteRepresentation, OracleDbType.IntervalYM, 0, -1);
				}
				throw new OracleNullValueException();
			}
		}

		// Token: 0x17000399 RID: 921
		// (get) Token: 0x060016AE RID: 5806 RVA: 0x000F285C File Offset: 0x000F0A5C
		public int Years
		{
			get
			{
				if (this.m_bNotNull)
				{
					return OracleIntervalYM.GetIYMData(this.m_byteRepresentation, 0, 0);
				}
				throw new OracleNullValueException();
			}
		}

		// Token: 0x1700039A RID: 922
		// (get) Token: 0x060016AF RID: 5807 RVA: 0x000F287C File Offset: 0x000F0A7C
		public int Months
		{
			get
			{
				if (this.m_bNotNull)
				{
					return OracleIntervalYM.GetIYMData(this.m_byteRepresentation, 1, 0);
				}
				throw new OracleNullValueException();
			}
		}

		// Token: 0x1700039B RID: 923
		// (get) Token: 0x060016B0 RID: 5808 RVA: 0x000F289C File Offset: 0x000F0A9C
		public double TotalYears
		{
			get
			{
				if (this.m_bNotNull)
				{
					return (double)OracleIntervalYM.GetIYMData(this.m_byteRepresentation, 0, 0) + (double)OracleIntervalYM.GetIYMData(this.m_byteRepresentation, 1, 0) / 12.0;
				}
				throw new OracleNullValueException();
			}
		}

		// Token: 0x060016B1 RID: 5809 RVA: 0x000F28EC File Offset: 0x000F0AEC
		public static bool Equals(OracleIntervalYM value1, OracleIntervalYM value2)
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
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x060016B2 RID: 5810 RVA: 0x000F2978 File Offset: 0x000F0B78
		public static bool GreaterThan(OracleIntervalYM value1, OracleIntervalYM value2)
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
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x060016B3 RID: 5811 RVA: 0x000F29DC File Offset: 0x000F0BDC
		public static bool GreaterThanOrEqual(OracleIntervalYM value1, OracleIntervalYM value2)
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

		// Token: 0x060016B4 RID: 5812 RVA: 0x000F2A44 File Offset: 0x000F0C44
		public static bool LessThan(OracleIntervalYM value1, OracleIntervalYM value2)
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

		// Token: 0x060016B5 RID: 5813 RVA: 0x000F2AA8 File Offset: 0x000F0CA8
		public static bool LessThanOrEqual(OracleIntervalYM value1, OracleIntervalYM value2)
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

		// Token: 0x060016B6 RID: 5814 RVA: 0x000F2B10 File Offset: 0x000F0D10
		public static bool NotEquals(OracleIntervalYM value1, OracleIntervalYM value2)
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

		// Token: 0x060016B7 RID: 5815 RVA: 0x000F2B78 File Offset: 0x000F0D78
		public static OracleIntervalYM Parse(string intervalStr)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleIntervalYM result;
			try
			{
				result = new OracleIntervalYM(intervalStr);
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

		// Token: 0x060016B8 RID: 5816 RVA: 0x000F2BEC File Offset: 0x000F0DEC
		public static OracleIntervalYM SetPrecision(OracleIntervalYM value1, int yearPrecision)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleIntervalYM result;
			try
			{
				if (!value1.m_bNotNull)
				{
					throw new OracleNullValueException();
				}
				if (yearPrecision < 0 || yearPrecision > 9)
				{
					throw new ArgumentOutOfRangeException("yearPrecision");
				}
				result = new OracleIntervalYM(value1.InternalByteRepresentation, yearPrecision, true);
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

		// Token: 0x060016B9 RID: 5817 RVA: 0x000F2C8C File Offset: 0x000F0E8C
		public static bool operator ==(OracleIntervalYM value1, OracleIntervalYM value2)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			bool result;
			try
			{
				result = OracleIntervalYM.Equals(value1, value2);
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

		// Token: 0x060016BA RID: 5818 RVA: 0x000F2CE8 File Offset: 0x000F0EE8
		public static bool operator >(OracleIntervalYM value1, OracleIntervalYM value2)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			bool result;
			try
			{
				result = OracleIntervalYM.GreaterThan(value1, value2);
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

		// Token: 0x060016BB RID: 5819 RVA: 0x000F2D44 File Offset: 0x000F0F44
		public static bool operator >=(OracleIntervalYM value1, OracleIntervalYM value2)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			bool result;
			try
			{
				result = OracleIntervalYM.GreaterThanOrEqual(value1, value2);
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

		// Token: 0x060016BC RID: 5820 RVA: 0x000F2DA0 File Offset: 0x000F0FA0
		public static bool operator <(OracleIntervalYM value1, OracleIntervalYM value2)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			bool result;
			try
			{
				result = OracleIntervalYM.LessThan(value1, value2);
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

		// Token: 0x060016BD RID: 5821 RVA: 0x000F2DFC File Offset: 0x000F0FFC
		public static bool operator <=(OracleIntervalYM value1, OracleIntervalYM value2)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			bool result;
			try
			{
				result = OracleIntervalYM.LessThanOrEqual(value1, value2);
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

		// Token: 0x060016BE RID: 5822 RVA: 0x000F2E58 File Offset: 0x000F1058
		public static bool operator !=(OracleIntervalYM value1, OracleIntervalYM value2)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			bool result;
			try
			{
				result = OracleIntervalYM.NotEquals(value1, value2);
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

		// Token: 0x060016BF RID: 5823 RVA: 0x000F2EB4 File Offset: 0x000F10B4
		public static OracleIntervalYM operator +(OracleIntervalYM value1, OracleIntervalYM value2)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleIntervalYM result;
			try
			{
				if (!value1.m_bNotNull || !value2.m_bNotNull)
				{
					result = OracleIntervalYM.Null;
				}
				else
				{
					long totalMonth = OracleIntervalYM.GetTotalMonth(value1);
					long totalMonth2 = OracleIntervalYM.GetTotalMonth(value2);
					long totalMonths = totalMonth + totalMonth2;
					result = new OracleIntervalYM(totalMonths);
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

		// Token: 0x060016C0 RID: 5824 RVA: 0x000F2F58 File Offset: 0x000F1158
		public static OracleIntervalYM operator -(OracleIntervalYM value1, OracleIntervalYM value2)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleIntervalYM result;
			try
			{
				if (!value1.m_bNotNull || !value2.m_bNotNull)
				{
					result = OracleIntervalYM.Null;
				}
				else
				{
					long totalMonth = OracleIntervalYM.GetTotalMonth(value1);
					long totalMonth2 = OracleIntervalYM.GetTotalMonth(value2);
					long totalMonths = totalMonth - totalMonth2;
					result = new OracleIntervalYM(totalMonths);
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

		// Token: 0x060016C1 RID: 5825 RVA: 0x000F2FFC File Offset: 0x000F11FC
		public static OracleIntervalYM operator -(OracleIntervalYM value1)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleIntervalYM result;
			try
			{
				if (!value1.m_bNotNull)
				{
					result = OracleIntervalYM.Null;
				}
				else
				{
					int iymdata = OracleIntervalYM.GetIYMData(value1.InternalByteRepresentation, 0, 0);
					int iymdata2 = OracleIntervalYM.GetIYMData(value1.InternalByteRepresentation, 1, 0);
					result = new OracleIntervalYM(-iymdata, -iymdata2);
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

		// Token: 0x060016C2 RID: 5826 RVA: 0x000F30A4 File Offset: 0x000F12A4
		public static OracleIntervalYM operator *(OracleIntervalYM value1, int multiplier)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleIntervalYM result;
			try
			{
				if (!value1.m_bNotNull)
				{
					result = OracleIntervalYM.Null;
				}
				else
				{
					long totalMonth = OracleIntervalYM.GetTotalMonth(value1);
					long totalMonths = totalMonth * (long)multiplier;
					result = new OracleIntervalYM(totalMonths);
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

		// Token: 0x060016C3 RID: 5827 RVA: 0x000F3138 File Offset: 0x000F1338
		public static OracleIntervalYM operator /(OracleIntervalYM value1, int divisor)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleIntervalYM result;
			try
			{
				if (!value1.m_bNotNull)
				{
					result = OracleIntervalYM.Null;
				}
				else
				{
					if (divisor == 0)
					{
						throw new DivideByZeroException();
					}
					long totalMonth = OracleIntervalYM.GetTotalMonth(value1);
					long totalMonths = totalMonth / (long)divisor;
					result = new OracleIntervalYM(totalMonths);
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

		// Token: 0x060016C4 RID: 5828 RVA: 0x000F31D4 File Offset: 0x000F13D4
		public static explicit operator OracleIntervalYM(string intervalStr)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleIntervalYM result;
			try
			{
				result = new OracleIntervalYM(intervalStr);
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

		// Token: 0x060016C5 RID: 5829 RVA: 0x000F3248 File Offset: 0x000F1448
		public static explicit operator long(OracleIntervalYM value1)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			long totalMonth;
			try
			{
				totalMonth = OracleIntervalYM.GetTotalMonth(value1);
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
			return totalMonth;
		}

		// Token: 0x060016C6 RID: 5830 RVA: 0x000F32BC File Offset: 0x000F14BC
		public static implicit operator OracleIntervalYM(long months)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleIntervalYM result;
			try
			{
				result = new OracleIntervalYM(months);
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

		// Token: 0x060016C7 RID: 5831 RVA: 0x000F3330 File Offset: 0x000F1530
		public static XmlQualifiedName GetXsdType(XmlSchemaSet schemaSet)
		{
			return new XmlQualifiedName("string", "http://www.w3.org/2001/XMLSchema");
		}

		// Token: 0x060016C8 RID: 5832 RVA: 0x000F3344 File Offset: 0x000F1544
		XmlSchema IXmlSerializable.GetSchema()
		{
			return null;
		}

		// Token: 0x060016C9 RID: 5833 RVA: 0x000F3348 File Offset: 0x000F1548
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
			this.m_bNotNull = true;
			this.m_yearPrec = 9;
			this.m_byteRepresentation = this.ToBytes(text);
		}

		// Token: 0x060016CA RID: 5834 RVA: 0x000F33BC File Offset: 0x000F15BC
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			if (this.m_bNotNull)
			{
				writer.WriteString(this.ToString());
				return;
			}
			writer.WriteAttributeString("xsi", "null", "http://www.w3.org/2001/XMLSchema-instance", "true");
		}

		// Token: 0x060016CB RID: 5835 RVA: 0x000F33F4 File Offset: 0x000F15F4
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
				if (obj.GetType() != typeof(OracleIntervalYM))
				{
					throw new ArgumentException("obj");
				}
				OracleIntervalYM oracleIntervalYM = (OracleIntervalYM)obj;
				CompareNullEnum compareNullEnum = InternalTypes.CompareNull(!this.m_bNotNull, !oracleIntervalYM.m_bNotNull);
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
					byte[] internalByteRepresentation = oracleIntervalYM.InternalByteRepresentation;
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

		// Token: 0x060016CC RID: 5836 RVA: 0x000F3514 File Offset: 0x000F1714
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
					int num = OracleIntervalYM.GetIYMData(this.m_byteRepresentation, 0, 0);
					int num2 = OracleIntervalYM.GetIYMData(this.m_byteRepresentation, 1, 0);
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
					string text = num.ToString();
					string text2 = num2.ToString();
					int length = text.Length;
					if (length < (int)this.m_yearPrec)
					{
						text = HelperClass.GetZeros((int)this.m_yearPrec - length) + text;
					}
					else if (length > (int)this.m_yearPrec)
					{
						text = text.Substring(0, (int)this.m_yearPrec);
					}
					if (num2 < 10)
					{
						text2 = "0" + text2;
					}
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
					stringBuilder.Append("-");
					stringBuilder.Append(text2);
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

		// Token: 0x060016CD RID: 5837 RVA: 0x000F36B0 File Offset: 0x000F18B0
		public override bool Equals(object obj)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			bool result;
			try
			{
				if (obj == null || obj.GetType() != typeof(OracleIntervalYM))
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

		// Token: 0x060016CE RID: 5838 RVA: 0x000F3748 File Offset: 0x000F1948
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

		// Token: 0x060016CF RID: 5839 RVA: 0x000F37B4 File Offset: 0x000F19B4
		internal byte[] ToBytes(string intervalYM)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			byte[] result;
			try
			{
				byte[] array = new byte[5];
				string text = intervalYM.Trim();
				char c = text[0];
				int startIndex = 0;
				if (c == '-' || c == '+')
				{
					startIndex = 1;
				}
				text = text.Substring(startIndex);
				int num = text.IndexOf('-');
				if (num < 0)
				{
					throw new FormatException();
				}
				string text2 = text.Substring(0, num);
				if (text2.Length > (int)this.m_yearPrec)
				{
					throw new FormatException();
				}
				int num2 = int.Parse(text2);
				string s = text.Substring(num + 1);
				int num3 = int.Parse(s);
				if (num3 > 11)
				{
					throw new FormatException();
				}
				if (c == '-')
				{
					num2 = -1 * num2;
					num3 = -1 * num3;
				}
				num2 += (int)OracleIntervalYM.INTYMYEAROFFSET;
				array[0] = RepConversion.RightShiftFirstNibble(num2);
				array[1] = RepConversion.RightShiftSecondNibble(num2);
				array[2] = RepConversion.RightShiftThirdNibble(num2);
				array[3] = RepConversion.RightShiftFourthNibble(num2);
				array[4] = (byte)((long)num3 + (long)((ulong)OracleIntervalYM.INTYMMONTHOFFSET));
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

		// Token: 0x060016D0 RID: 5840 RVA: 0x000F3904 File Offset: 0x000F1B04
		internal static int GetIYMData(byte[] byteRepresentation, byte iymComponent, int dataOffset = 0)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			int result;
			try
			{
				int num = 0;
				switch (iymComponent)
				{
				case 0:
					num = RepConversion.LeftShiftFirstNibble(byteRepresentation[dataOffset]);
					num |= RepConversion.LeftShiftSecondNibble(byteRepresentation[1 + dataOffset]);
					num |= RepConversion.LeftShiftThirdNibble(byteRepresentation[2 + dataOffset]);
					num |= (int)(byteRepresentation[3 + dataOffset] & byte.MaxValue);
					num -= (int)OracleIntervalYM.INTYMYEAROFFSET;
					break;
				case 1:
					num = (int)((uint)byteRepresentation[4 + dataOffset] - OracleIntervalYM.INTYMMONTHOFFSET);
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

		// Token: 0x060016D1 RID: 5841 RVA: 0x000F39D0 File Offset: 0x000F1BD0
		internal static long GetLong(byte[] byteRepresentation, OracleDbType oraType, int dataOffset = 0, int dataLength = -1)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			long result;
			try
			{
				if (dataLength == -1)
				{
					dataLength = byteRepresentation.Length;
				}
				if (oraType != OracleDbType.IntervalYM || dataLength != 5)
				{
					throw new OracleTypeException(ResourceStringConstants.INT_ERR, new object[0]);
				}
				result = (long)OracleIntervalYM.GetIYMData(byteRepresentation, 0, dataOffset) * 12L + (long)OracleIntervalYM.GetIYMData(byteRepresentation, 1, dataOffset);
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

		// Token: 0x060016D2 RID: 5842 RVA: 0x000F3A78 File Offset: 0x000F1C78
		internal static long GetTotalMonth(OracleIntervalYM value)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			long result;
			try
			{
				int iymdata = OracleIntervalYM.GetIYMData(value.InternalByteRepresentation, 0, 0);
				int iymdata2 = OracleIntervalYM.GetIYMData(value.InternalByteRepresentation, 1, 0);
				long num = (long)iymdata * 12L + (long)iymdata2;
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

		// Token: 0x1700039C RID: 924
		// (get) Token: 0x060016D3 RID: 5843 RVA: 0x000F3B14 File Offset: 0x000F1D14
		internal byte[] InternalByteRepresentation
		{
			get
			{
				return this.m_byteRepresentation;
			}
		}

		// Token: 0x04001A0A RID: 6666
		internal const short MaxArrSize = 5;

		// Token: 0x04001A0B RID: 6667
		internal const byte IYMType = 7;

		// Token: 0x04001A0C RID: 6668
		internal const byte YEAR = 0;

		// Token: 0x04001A0D RID: 6669
		internal const byte MONTH = 1;

		// Token: 0x04001A0E RID: 6670
		private static uint INTYMYEAROFFSET = 2147483648U;

		// Token: 0x04001A0F RID: 6671
		private static uint INTYMMONTHOFFSET = 60U;

		// Token: 0x04001A10 RID: 6672
		private bool m_bNotNull;

		// Token: 0x04001A11 RID: 6673
		private byte m_yearPrec;

		// Token: 0x04001A12 RID: 6674
		private byte[] m_byteRepresentation;

		// Token: 0x04001A13 RID: 6675
		public static readonly OracleIntervalYM MaxValue = new OracleIntervalYM(999999999, 11);

		// Token: 0x04001A14 RID: 6676
		public static readonly OracleIntervalYM MinValue = new OracleIntervalYM(-999999999, -11);

		// Token: 0x04001A15 RID: 6677
		public static readonly OracleIntervalYM Zero = new OracleIntervalYM(0, 0);

		// Token: 0x04001A16 RID: 6678
		public static readonly OracleIntervalYM Null;
	}
}
