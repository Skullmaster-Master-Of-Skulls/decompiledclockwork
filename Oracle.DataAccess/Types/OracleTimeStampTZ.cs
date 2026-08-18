using System;
using System.Runtime.InteropServices;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Oracle.DataAccess.Client;

namespace Oracle.DataAccess.Types
{
	// Token: 0x02000089 RID: 137
	[XmlSchemaProvider("GetXsdType")]
	public struct OracleTimeStampTZ : IComparable, INullable, IXmlSerializable
	{
		// Token: 0x06000612 RID: 1554 RVA: 0x000400E4 File Offset: 0x0003F0E4
		static OracleTimeStampTZ()
		{
			if (!OracleInit.bSetDllDirectoryInvoked)
			{
				OracleInit.Initialize();
			}
		}

		// Token: 0x06000613 RID: 1555 RVA: 0x00040134 File Offset: 0x0003F134
		public static XmlQualifiedName GetXsdType(XmlSchemaSet schemaSet)
		{
			return new XmlQualifiedName("DateTime", "http://www.w3.org/2001/XMLSchema");
		}

		// Token: 0x06000614 RID: 1556 RVA: 0x00040145 File Offset: 0x0003F145
		XmlSchema IXmlSerializable.GetSchema()
		{
			return null;
		}

		// Token: 0x06000615 RID: 1557 RVA: 0x00040148 File Offset: 0x0003F148
		unsafe void IXmlSerializable.ReadXml(XmlReader reader)
		{
			string text = reader.GetAttribute("null", "http://www.w3.org/2001/XMLSchema-instance");
			if (text != null && XmlConvert.ToBoolean(text))
			{
				this.m_bNotNull = false;
				return;
			}
			text = reader.ReadElementString();
			this.m_opoTSCtx = new OpoTSCtx(text, TimeStampType.TSType_TSZ);
			if (this.m_opoTSCtx.m_error != 0)
			{
				throw new ArgumentException(OracleTypeException.GetTypeMsg(this.m_opoTSCtx.m_error, new object[0]));
			}
			int num = 0;
			OpoTSValCtx opoTSValCtx;
			try
			{
				num = OpsTSZ.ConvertToTSL(this.m_opoTSCtx.m_pValCtx, &opoTSValCtx);
			}
			catch (Exception ex)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex);
				}
				throw;
			}
			if (num != 0)
			{
				throw new OracleTypeException(num, new object[0]);
			}
			this.m_year = (int)opoTSValCtx.m_year;
			this.m_month = (int)opoTSValCtx.m_month;
			this.m_day = (int)opoTSValCtx.m_day;
			this.m_hour = (int)opoTSValCtx.m_hour;
			this.m_minute = (int)opoTSValCtx.m_minute;
			this.m_second = (int)opoTSValCtx.m_second;
			this.m_fSecond = opoTSValCtx.m_fSecond;
			this.m_fSecondPrec = 9;
			this.m_timeZone = null;
			this.m_bNotNull = true;
		}

		// Token: 0x06000616 RID: 1558 RVA: 0x00040274 File Offset: 0x0003F274
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			if (this.m_bNotNull)
			{
				writer.WriteString(TimeStamp.ToTSString(this.m_opoTSCtx, this.m_fSecondPrec, TimeStampType.TSType_TSZ));
				return;
			}
			writer.WriteAttributeString("xsi", "null", "http://www.w3.org/2001/XMLSchema-instance", "true");
		}

		// Token: 0x06000617 RID: 1559 RVA: 0x000402B4 File Offset: 0x0003F2B4
		public OracleTimeStampTZ(int year, int month, int day, int hour, int minute, int second, int nanosecond, string timeZone)
		{
			if (!TimeStamp.IsValidDateTime(year, month, day, hour, minute, second, nanosecond))
			{
				throw new ArgumentOutOfRangeException();
			}
			this.m_opoTSCtx = new OpoTSCtx(year, month, day, hour, minute, second, nanosecond, timeZone);
			if (this.m_opoTSCtx.m_error != 0)
			{
				throw new ArgumentException(OracleTypeException.GetTypeMsg(this.m_opoTSCtx.m_error, new object[0]));
			}
			this.m_year = year;
			this.m_day = day;
			this.m_month = month;
			this.m_hour = hour;
			this.m_minute = minute;
			this.m_second = second;
			this.m_fSecond = nanosecond;
			this.m_bNotNull = true;
			this.m_fSecondPrec = 9;
			this.m_timeZone = timeZone;
		}

		// Token: 0x06000618 RID: 1560 RVA: 0x00040368 File Offset: 0x0003F368
		public OracleTimeStampTZ(int year, int month, int day, int hour, int minute, int second, int nanosecond)
		{
			this = new OracleTimeStampTZ(year, month, day, hour, minute, second, nanosecond, null);
		}

		// Token: 0x06000619 RID: 1561 RVA: 0x00040388 File Offset: 0x0003F388
		public OracleTimeStampTZ(int year, int month, int day, int hour, int minute, int second, double millisecond, string timeZone)
		{
			if (!TimeStamp.IsValidDateTime(year, month, day, hour, minute, second, (int)(millisecond * 1000000.0)))
			{
				throw new ArgumentOutOfRangeException();
			}
			int fSecond = (int)(millisecond * 1000000.0);
			this.m_opoTSCtx = new OpoTSCtx(year, month, day, hour, minute, second, fSecond, timeZone);
			if (this.m_opoTSCtx.m_error != 0)
			{
				throw new ArgumentException(OracleTypeException.GetTypeMsg(this.m_opoTSCtx.m_error, new object[0]));
			}
			this.m_year = year;
			this.m_day = day;
			this.m_month = month;
			this.m_hour = hour;
			this.m_minute = minute;
			this.m_second = second;
			this.m_fSecond = (int)(millisecond * 1000000.0);
			this.m_bNotNull = true;
			this.m_fSecondPrec = 9;
			this.m_timeZone = timeZone;
		}

		// Token: 0x0600061A RID: 1562 RVA: 0x00040460 File Offset: 0x0003F460
		public OracleTimeStampTZ(int year, int month, int day, int hour, int minute, int second, double millisecond)
		{
			this = new OracleTimeStampTZ(year, month, day, hour, minute, second, millisecond, null);
		}

		// Token: 0x0600061B RID: 1563 RVA: 0x00040480 File Offset: 0x0003F480
		public OracleTimeStampTZ(int year, int month, int day, string timeZone)
		{
			this = new OracleTimeStampTZ(year, month, day, 0, 0, 0, 0, timeZone);
		}

		// Token: 0x0600061C RID: 1564 RVA: 0x0004049C File Offset: 0x0003F49C
		public OracleTimeStampTZ(int year, int month, int day)
		{
			this = new OracleTimeStampTZ(year, month, day, 0, 0, 0, 0);
		}

		// Token: 0x0600061D RID: 1565 RVA: 0x000404AC File Offset: 0x0003F4AC
		internal unsafe OracleTimeStampTZ(IntPtr ociDateTime)
		{
			int num = 0;
			OpoTSValCtx* pCtx = null;
			num = OpsTSZ.AllocValCtxFromOCI(ociDateTime, out pCtx);
			if (num != 0)
			{
				throw new OracleException(num, string.Empty, string.Empty, string.Empty);
			}
			this.m_opoTSCtx = new OpoTSCtx(pCtx);
			OpoTSValCtx opoTSValCtx;
			try
			{
				num = OpsTSZ.ConvertToTSL(this.m_opoTSCtx.m_pValCtx, &opoTSValCtx);
			}
			catch (Exception ex)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex);
				}
				throw;
			}
			if (num != 0)
			{
				throw new OracleTypeException(num, new object[0]);
			}
			this.m_year = (int)opoTSValCtx.m_year;
			this.m_month = (int)opoTSValCtx.m_month;
			this.m_day = (int)opoTSValCtx.m_day;
			this.m_hour = (int)opoTSValCtx.m_hour;
			this.m_minute = (int)opoTSValCtx.m_minute;
			this.m_second = (int)opoTSValCtx.m_second;
			this.m_fSecond = opoTSValCtx.m_fSecond;
			this.m_bNotNull = true;
			this.m_fSecondPrec = 9;
			this.m_timeZone = null;
		}

		// Token: 0x0600061E RID: 1566 RVA: 0x000405A4 File Offset: 0x0003F5A4
		public OracleTimeStampTZ(int year, int month, int day, int hour, int minute, int second, string timeZone)
		{
			this = new OracleTimeStampTZ(year, month, day, hour, minute, second, 0, timeZone);
		}

		// Token: 0x0600061F RID: 1567 RVA: 0x000405C3 File Offset: 0x0003F5C3
		public OracleTimeStampTZ(int year, int month, int day, int hour, int minute, int second)
		{
			this = new OracleTimeStampTZ(year, month, day, hour, minute, second, 0);
		}

		// Token: 0x06000620 RID: 1568 RVA: 0x000405D8 File Offset: 0x0003F5D8
		public unsafe OracleTimeStampTZ(DateTime data, string timeZone)
		{
			this.m_opoTSCtx = new OpoTSCtx(data, timeZone);
			if (this.m_opoTSCtx.m_error != 0)
			{
				throw new ArgumentException(OracleTypeException.GetTypeMsg(this.m_opoTSCtx.m_error, new object[0]));
			}
			OpoTSValCtx opoTSValCtx;
			TimeStamp.FillValCtxFromDateTime(&opoTSValCtx, data);
			this.m_year = (int)opoTSValCtx.m_year;
			this.m_month = (int)opoTSValCtx.m_month;
			this.m_day = (int)opoTSValCtx.m_day;
			this.m_hour = (int)opoTSValCtx.m_hour;
			this.m_minute = (int)opoTSValCtx.m_minute;
			this.m_second = (int)opoTSValCtx.m_second;
			this.m_fSecond = opoTSValCtx.m_fSecond;
			this.m_bNotNull = true;
			this.m_fSecondPrec = 9;
			this.m_timeZone = timeZone;
		}

		// Token: 0x06000621 RID: 1569 RVA: 0x00040695 File Offset: 0x0003F695
		public OracleTimeStampTZ(DateTime data)
		{
			this = new OracleTimeStampTZ(data, null);
		}

		// Token: 0x06000622 RID: 1570 RVA: 0x000406A0 File Offset: 0x0003F6A0
		public unsafe OracleTimeStampTZ(byte[] binData)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleTimeStampTZ::OracleTimeStampTZ(4)\n"
				});
			}
			if (binData == null)
			{
				throw new ArgumentNullException();
			}
			this.m_opoTSCtx = new OpoTSCtx(binData, TimeStampType.TSType_TSZ);
			if (this.m_opoTSCtx.m_error != 0)
			{
				throw new ArgumentException(OracleTypeException.GetTypeMsg(this.m_opoTSCtx.m_error, new object[0]));
			}
			OpoTSValCtx opoTSValCtx;
			try
			{
				OpsTSZ.ConvertToTSL(this.m_opoTSCtx.m_pValCtx, &opoTSValCtx);
			}
			catch (Exception ex)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex);
				}
				throw;
			}
			this.m_year = (int)opoTSValCtx.m_year;
			this.m_month = (int)opoTSValCtx.m_month;
			this.m_day = (int)opoTSValCtx.m_day;
			this.m_hour = (int)opoTSValCtx.m_hour;
			this.m_minute = (int)opoTSValCtx.m_minute;
			this.m_second = (int)opoTSValCtx.m_second;
			this.m_fSecond = opoTSValCtx.m_fSecond;
			this.m_bNotNull = true;
			this.m_fSecondPrec = 9;
			this.m_timeZone = null;
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleTimeStampTZ::OracleTimeStampTZ(4)\n"
				});
			}
		}

		// Token: 0x06000623 RID: 1571 RVA: 0x000407D0 File Offset: 0x0003F7D0
		public unsafe OracleTimeStampTZ(string tsStr)
		{
			if (tsStr == null)
			{
				throw new ArgumentNullException();
			}
			this.m_opoTSCtx = new OpoTSCtx(tsStr, TimeStampType.TSType_TSZ);
			if (this.m_opoTSCtx.m_error != 0)
			{
				throw new ArgumentException(OracleTypeException.GetTypeMsg(this.m_opoTSCtx.m_error, new object[0]));
			}
			int num = 0;
			OpoTSValCtx opoTSValCtx;
			try
			{
				num = OpsTSZ.ConvertToTSL(this.m_opoTSCtx.m_pValCtx, &opoTSValCtx);
			}
			catch (Exception ex)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex);
				}
				throw;
			}
			if (num != 0)
			{
				throw new OracleTypeException(num, new object[0]);
			}
			this.m_year = (int)opoTSValCtx.m_year;
			this.m_month = (int)opoTSValCtx.m_month;
			this.m_day = (int)opoTSValCtx.m_day;
			this.m_hour = (int)opoTSValCtx.m_hour;
			this.m_minute = (int)opoTSValCtx.m_minute;
			this.m_second = (int)opoTSValCtx.m_second;
			this.m_fSecond = opoTSValCtx.m_fSecond;
			this.m_bNotNull = true;
			this.m_fSecondPrec = 9;
			this.m_timeZone = null;
		}

		// Token: 0x06000624 RID: 1572 RVA: 0x000408D8 File Offset: 0x0003F8D8
		internal unsafe static IntPtr AllocValCtx(object methodParam)
		{
			IntPtr zero = IntPtr.Zero;
			if (methodParam is char[])
			{
				OracleTimeStampTZ oracleTimeStampTZ = new OracleTimeStampTZ(new string((char[])methodParam));
				int num = OpsTSZ.AllocOCIFromValCtx(OracleConnection.GetInternalConnection().m_opoConCtx.opsConCtx, oracleTimeStampTZ.GetValCtx(), out zero);
				if (num != 0)
				{
					throw new OracleException(num, string.Empty, string.Empty, string.Empty);
				}
			}
			else if (methodParam is byte[])
			{
				OpoTSValCtx* ptr;
				int num = OpsTSZ.AllocValCtxFromBytes((byte[])methodParam, out ptr, 9);
				if (num != 0)
				{
					throw new OracleException(num, string.Empty, string.Empty, string.Empty);
				}
				num = OpsTSZ.AllocOCIFromValCtx(OracleConnection.GetInternalConnection().m_opoConCtx.opsConCtx, ptr, out zero);
				if (num != 0)
				{
					throw new OracleException(num, string.Empty, string.Empty, string.Empty);
				}
				num = OpsTSZ.FreeValCtx(ptr);
				if (num != 0)
				{
					throw new OracleException(num, string.Empty, string.Empty, string.Empty);
				}
			}
			else if (methodParam is string)
			{
				OracleTimeStampTZ oracleTimeStampTZ2 = new OracleTimeStampTZ((string)methodParam);
				int num = OpsTSZ.AllocOCIFromValCtx(OracleConnection.GetInternalConnection().m_opoConCtx.opsConCtx, oracleTimeStampTZ2.GetValCtx(), out zero);
				if (num != 0)
				{
					throw new OracleException(num, string.Empty, string.Empty, string.Empty);
				}
			}
			else if (methodParam is DateTime)
			{
				OracleTimeStampTZ oracleTimeStampTZ3 = new OracleTimeStampTZ((DateTime)methodParam);
				int num = OpsTSZ.AllocOCIFromValCtx(OracleConnection.GetInternalConnection().m_opoConCtx.opsConCtx, oracleTimeStampTZ3.GetValCtx(), out zero);
				if (num != 0)
				{
					throw new OracleException(num, string.Empty, string.Empty, string.Empty);
				}
			}
			else if (methodParam is OracleString)
			{
				OracleTimeStampTZ oracleTimeStampTZ4 = new OracleTimeStampTZ(((OracleString)methodParam).Value);
				int num = OpsTSZ.AllocOCIFromValCtx(OracleConnection.GetInternalConnection().m_opoConCtx.opsConCtx, oracleTimeStampTZ4.GetValCtx(), out zero);
				if (num != 0)
				{
					throw new OracleException(num, string.Empty, string.Empty, string.Empty);
				}
			}
			else if (methodParam is OracleDate)
			{
				OpoTSValCtx* ptr2;
				int num = OpsTSZ.AllocValCtxForFromDate(((OracleDate)methodParam).GetValCtx(), out ptr2);
				if (num != 0)
				{
					throw new OracleException(num, string.Empty, string.Empty, string.Empty);
				}
				num = OpsTSZ.AllocOCIFromValCtx(OracleConnection.GetInternalConnection().m_opoConCtx.opsConCtx, ptr2, out zero);
				if (num != 0)
				{
					throw new OracleException(num, string.Empty, string.Empty, string.Empty);
				}
				num = OpsTSZ.FreeValCtx(ptr2);
				if (num != 0)
				{
					throw new OracleException(num, string.Empty, string.Empty, string.Empty);
				}
			}
			else if (methodParam is OracleTimeStamp)
			{
				int num = OpsTSZ.AllocOCIFromValCtx(OracleConnection.GetInternalConnection().m_opoConCtx.opsConCtx, ((OracleTimeStamp)methodParam).ToOracleTimeStampTZ().GetValCtx(), out zero);
				if (num != 0)
				{
					throw new OracleException(num, string.Empty, string.Empty, string.Empty);
				}
			}
			else if (methodParam is OracleTimeStampTZ)
			{
				int num = OpsTSZ.AllocOCIFromValCtx(OracleConnection.GetInternalConnection().m_opoConCtx.opsConCtx, ((OracleTimeStampTZ)methodParam).GetValCtx(), out zero);
				if (num != 0)
				{
					throw new OracleException(num, string.Empty, string.Empty, string.Empty);
				}
			}
			else if (methodParam is OracleTimeStampLTZ)
			{
				int num = OpsTSZ.AllocOCIFromValCtx(OracleConnection.GetInternalConnection().m_opoConCtx.opsConCtx, ((OracleTimeStampLTZ)methodParam).ToOracleTimeStampTZ().GetValCtx(), out zero);
				if (num != 0)
				{
					throw new OracleException(num, string.Empty, string.Empty, string.Empty);
				}
			}
			return zero;
		}

		// Token: 0x06000625 RID: 1573 RVA: 0x00040C38 File Offset: 0x0003FC38
		public static bool Equals(OracleTimeStampTZ value1, OracleTimeStampTZ value2)
		{
			CompareNullEnum compareNullEnum = InternalTypes.CompareNull(!value1.m_bNotNull, !value2.m_bNotNull);
			return compareNullEnum == CompareNullEnum.BothNull || (compareNullEnum == CompareNullEnum.BothNotNull && TimeStamp.Compare(value1.m_opoTSCtx, value2.m_opoTSCtx) == 0);
		}

		// Token: 0x06000626 RID: 1574 RVA: 0x00040C84 File Offset: 0x0003FC84
		public static bool GreaterThan(OracleTimeStampTZ value1, OracleTimeStampTZ value2)
		{
			CompareNullEnum compareNullEnum = InternalTypes.CompareNull(!value1.m_bNotNull, !value2.m_bNotNull);
			return compareNullEnum != CompareNullEnum.BothNull && compareNullEnum != CompareNullEnum.FirstNullOnly && (compareNullEnum == CompareNullEnum.SecondNullOnly || TimeStamp.Compare(value1.m_opoTSCtx, value2.m_opoTSCtx) > 0);
		}

		// Token: 0x06000627 RID: 1575 RVA: 0x00040CD8 File Offset: 0x0003FCD8
		public static bool GreaterThanOrEqual(OracleTimeStampTZ value1, OracleTimeStampTZ value2)
		{
			CompareNullEnum compareNullEnum = InternalTypes.CompareNull(!value1.m_bNotNull, !value2.m_bNotNull);
			return compareNullEnum == CompareNullEnum.BothNull || (compareNullEnum != CompareNullEnum.FirstNullOnly && (compareNullEnum == CompareNullEnum.SecondNullOnly || TimeStamp.Compare(value1.m_opoTSCtx, value2.m_opoTSCtx) >= 0));
		}

		// Token: 0x06000628 RID: 1576 RVA: 0x00040D2C File Offset: 0x0003FD2C
		public static bool LessThan(OracleTimeStampTZ value1, OracleTimeStampTZ value2)
		{
			CompareNullEnum compareNullEnum = InternalTypes.CompareNull(!value1.m_bNotNull, !value2.m_bNotNull);
			return compareNullEnum != CompareNullEnum.BothNull && (compareNullEnum == CompareNullEnum.FirstNullOnly || (compareNullEnum != CompareNullEnum.SecondNullOnly && TimeStamp.Compare(value1.m_opoTSCtx, value2.m_opoTSCtx) < 0));
		}

		// Token: 0x06000629 RID: 1577 RVA: 0x00040D80 File Offset: 0x0003FD80
		public static bool LessThanOrEqual(OracleTimeStampTZ value1, OracleTimeStampTZ value2)
		{
			CompareNullEnum compareNullEnum = InternalTypes.CompareNull(!value1.m_bNotNull, !value2.m_bNotNull);
			return compareNullEnum == CompareNullEnum.BothNull || compareNullEnum == CompareNullEnum.FirstNullOnly || (compareNullEnum != CompareNullEnum.SecondNullOnly && TimeStamp.Compare(value1.m_opoTSCtx, value2.m_opoTSCtx) <= 0);
		}

		// Token: 0x0600062A RID: 1578 RVA: 0x00040DD2 File Offset: 0x0003FDD2
		public static bool NotEquals(OracleTimeStampTZ value1, OracleTimeStampTZ value2)
		{
			return !OracleTimeStampTZ.Equals(value1, value2);
		}

		// Token: 0x0600062B RID: 1579 RVA: 0x00040DE0 File Offset: 0x0003FDE0
		public unsafe static OracleTimeStampTZ GetSysDate()
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleTimeStampTZ::GetSysDate()\n"
				});
			}
			int num = 0;
			OpoTSValCtx* ptr = null;
			try
			{
				num = OpsTSZ.AllocValCtxForSysDate(out ptr);
			}
			catch (Exception ex)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex);
				}
				num = ErrRes.INT_ERR;
				throw;
			}
			finally
			{
				if (num != 0)
				{
					if (OraTrace.m_TraceLevel != 0U)
					{
						OraTrace.Trace(1U, new string[]
						{
							" (EXIT)  OracleTimeStampTZ::GetSysDate()\n"
						});
					}
					if (ptr != null)
					{
						try
						{
							OpsTSZ.FreeValCtx(ptr);
						}
						catch (Exception ex2)
						{
							if (OraTrace.m_TraceLevel != 0U)
							{
								OraTrace.TraceExceptionInfo(ex2);
							}
						}
						ptr = null;
					}
					if (num != ErrRes.INT_ERR)
					{
						throw new OracleTypeException(num, new object[0]);
					}
				}
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleTimeStampTZ::GetSysDate()\n"
				});
			}
			return new OracleTimeStampTZ(ptr);
		}

		// Token: 0x0600062C RID: 1580 RVA: 0x00040EDC File Offset: 0x0003FEDC
		public static OracleTimeStampTZ Parse(string tsStr)
		{
			if (tsStr == null)
			{
				throw new ArgumentNullException();
			}
			return new OracleTimeStampTZ(tsStr);
		}

		// Token: 0x0600062D RID: 1581 RVA: 0x00040EF0 File Offset: 0x0003FEF0
		public unsafe static OracleTimeStampTZ SetPrecision(OracleTimeStampTZ value1, int fracSecPrecision)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleTimeStampTZ::SetPrecision()\n"
				});
			}
			if (!value1.m_bNotNull)
			{
				throw new OracleNullValueException();
			}
			int num = 0;
			if (fracSecPrecision < 0 || fracSecPrecision > 9)
			{
				throw new ArgumentOutOfRangeException("fracSecPrecision");
			}
			OpoTSValCtx* ptr = null;
			try
			{
				num = OpsTSZ.AllocValCtxFromBytes(value1.BinData, out ptr, fracSecPrecision);
			}
			catch (Exception ex)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex);
				}
				num = ErrRes.INT_ERR;
				throw;
			}
			finally
			{
				if (num != 0)
				{
					if (OraTrace.m_TraceLevel != 0U)
					{
						OraTrace.Trace(1U, new string[]
						{
							" (EXIT)  OracleTimeStampTZ::SetPrecision()\n"
						});
					}
					if (ptr != null)
					{
						try
						{
							OpsTSZ.FreeValCtx(ptr);
						}
						catch (Exception ex2)
						{
							if (OraTrace.m_TraceLevel != 0U)
							{
								OraTrace.TraceExceptionInfo(ex2);
							}
						}
						ptr = null;
					}
					if (num != ErrRes.INT_ERR)
					{
						throw new OracleTypeException(num, new object[0]);
					}
				}
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleTimeStampTZ::SetPrecision()\n"
				});
			}
			return new OracleTimeStampTZ(ptr, fracSecPrecision);
		}

		// Token: 0x0600062E RID: 1582 RVA: 0x00041018 File Offset: 0x00040018
		public static bool operator ==(OracleTimeStampTZ value1, OracleTimeStampTZ value2)
		{
			return OracleTimeStampTZ.Equals(value1, value2);
		}

		// Token: 0x0600062F RID: 1583 RVA: 0x00041021 File Offset: 0x00040021
		public static bool operator >(OracleTimeStampTZ value1, OracleTimeStampTZ value2)
		{
			return OracleTimeStampTZ.GreaterThan(value1, value2);
		}

		// Token: 0x06000630 RID: 1584 RVA: 0x0004102A File Offset: 0x0004002A
		public static bool operator >=(OracleTimeStampTZ value1, OracleTimeStampTZ value2)
		{
			return OracleTimeStampTZ.GreaterThanOrEqual(value1, value2);
		}

		// Token: 0x06000631 RID: 1585 RVA: 0x00041033 File Offset: 0x00040033
		public static bool operator <(OracleTimeStampTZ value1, OracleTimeStampTZ value2)
		{
			return OracleTimeStampTZ.LessThan(value1, value2);
		}

		// Token: 0x06000632 RID: 1586 RVA: 0x0004103C File Offset: 0x0004003C
		public static bool operator <=(OracleTimeStampTZ value1, OracleTimeStampTZ value2)
		{
			return OracleTimeStampTZ.LessThanOrEqual(value1, value2);
		}

		// Token: 0x06000633 RID: 1587 RVA: 0x00041045 File Offset: 0x00040045
		public static bool operator !=(OracleTimeStampTZ value1, OracleTimeStampTZ value2)
		{
			return OracleTimeStampTZ.NotEquals(value1, value2);
		}

		// Token: 0x06000634 RID: 1588 RVA: 0x00041050 File Offset: 0x00040050
		public unsafe static OracleTimeStampTZ operator +(OracleTimeStampTZ value1, OracleIntervalDS value2)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleTimeStampTZ::operator +(1)\n"
				});
			}
			if (value1.IsNull || value2.IsNull)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleTimeStampTZ::operator +(1)\n"
					});
				}
				return OracleTimeStampTZ.Null;
			}
			int num = 0;
			OpoTSValCtx* ptr = null;
			try
			{
				num = OpsTSA.AllocValCtxForAddInterval(value1.m_opoTSCtx.m_pValCtx, value2.GetValCtx(), out ptr);
			}
			catch (Exception ex)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex);
				}
				num = ErrRes.INT_ERR;
				throw;
			}
			finally
			{
				if (num != 0)
				{
					if (OraTrace.m_TraceLevel != 0U)
					{
						OraTrace.Trace(1U, new string[]
						{
							" (EXIT)  OracleTimeStampTZ::operator +(1)\n"
						});
					}
					if (ptr != null)
					{
						try
						{
							OpsTSZ.FreeValCtx(ptr);
						}
						catch (Exception ex2)
						{
							if (OraTrace.m_TraceLevel != 0U)
							{
								OraTrace.TraceExceptionInfo(ex2);
							}
						}
						ptr = null;
					}
					if (num != ErrRes.INT_ERR)
					{
						throw new OracleTypeException(num, new object[0]);
					}
				}
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleTimeStampTZ::operator +(1)\n"
				});
			}
			return new OracleTimeStampTZ(ptr);
		}

		// Token: 0x06000635 RID: 1589 RVA: 0x00041198 File Offset: 0x00040198
		public unsafe static OracleTimeStampTZ operator +(OracleTimeStampTZ value1, OracleIntervalYM value2)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleTimeStampTZ::operator +(2)\n"
				});
			}
			if (value1.IsNull || value2.IsNull)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleTimeStampTZ::operator +(2)\n"
					});
				}
				return OracleTimeStampTZ.Null;
			}
			int num = 0;
			OpoTSValCtx* pCtx;
			try
			{
				num = OpsTSA.AllocValCtxForAddInterval(value1.m_opoTSCtx.m_pValCtx, value2.GetValCtx(), out pCtx);
			}
			catch (Exception ex)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex);
				}
				throw;
			}
			if (num != 0)
			{
				throw new OracleTypeException(num, new object[0]);
			}
			OpoTSCtx ctx = new OpoTSCtx(pCtx);
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleTimeStampTZ::operator +(2)\n"
				});
			}
			return new OracleTimeStampTZ(ctx);
		}

		// Token: 0x06000636 RID: 1590 RVA: 0x0004127C File Offset: 0x0004027C
		public unsafe static OracleTimeStampTZ operator +(OracleTimeStampTZ value1, TimeSpan value2)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleTimeStampTZ::operator +(3)\n"
				});
			}
			if (value1.IsNull)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleTimeStampTZ::operator +(3)\n"
					});
				}
				return OracleTimeStampTZ.Null;
			}
			int num = 0;
			OpoITLValCtx opoITLValCtx = default(OpoITLValCtx);
			GCHandle gchandle = GCHandle.Alloc(opoITLValCtx, GCHandleType.Pinned);
			OpoTSValCtx* pCtx;
			try
			{
				OracleIntervalDS.FillValCtxFromTimeSpan(&opoITLValCtx, value2);
				num = OpsTSA.AllocValCtxForAddInterval(value1.m_opoTSCtx.m_pValCtx, &opoITLValCtx, out pCtx);
			}
			finally
			{
				if (gchandle.IsAllocated)
				{
					gchandle.Free();
				}
			}
			OpoTSCtx ctx = new OpoTSCtx(pCtx);
			if (num != 0)
			{
				throw new OracleTypeException(num, new object[0]);
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleTimeStampTZ::operator +(3)\n"
				});
			}
			return new OracleTimeStampTZ(ctx);
		}

		// Token: 0x06000637 RID: 1591 RVA: 0x00041370 File Offset: 0x00040370
		public unsafe static OracleTimeStampTZ operator -(OracleTimeStampTZ value1, OracleIntervalDS value2)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleTimeStampTZ::operator -(1)\n"
				});
			}
			if (value1.IsNull || value2.IsNull)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleTimeStampTZ::operator -(1)\n"
					});
				}
				return OracleTimeStampTZ.Null;
			}
			int num = 0;
			OpoTSCtx ctx = new OpoTSCtx(TimeStampType.TSType_TSZ);
			OpoTSValCtx* pCtx;
			try
			{
				num = OpsTSA.AllocValCtxForSubInterval(value1.m_opoTSCtx.m_pValCtx, value2.GetValCtx(), out pCtx);
			}
			catch (Exception ex)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex);
				}
				throw;
			}
			if (num != 0)
			{
				throw new OracleTypeException(num, new object[0]);
			}
			ctx = new OpoTSCtx(pCtx);
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleTimeStampTZ::operator -(1)\n"
				});
			}
			return new OracleTimeStampTZ(ctx);
		}

		// Token: 0x06000638 RID: 1592 RVA: 0x00041458 File Offset: 0x00040458
		public unsafe static OracleTimeStampTZ operator -(OracleTimeStampTZ value1, OracleIntervalYM value2)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleTimeStampTZ::operator -(2)\n"
				});
			}
			if (value1.IsNull || value2.IsNull)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleTimeStampTZ::operator -(2)\n"
					});
				}
				return OracleTimeStampTZ.Null;
			}
			int num = 0;
			OpoTSCtx ctx = new OpoTSCtx(TimeStampType.TSType_TSZ);
			OpoTSValCtx* pCtx;
			try
			{
				num = OpsTSA.AllocValCtxForSubInterval(value1.m_opoTSCtx.m_pValCtx, value2.GetValCtx(), out pCtx);
			}
			catch (Exception ex)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex);
				}
				throw;
			}
			if (num != 0)
			{
				throw new OracleTypeException(num, new object[0]);
			}
			ctx = new OpoTSCtx(pCtx);
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleTimeStampTZ::operator -(2)\n"
				});
			}
			return new OracleTimeStampTZ(ctx);
		}

		// Token: 0x06000639 RID: 1593 RVA: 0x00041540 File Offset: 0x00040540
		public unsafe static OracleTimeStampTZ operator -(OracleTimeStampTZ value1, TimeSpan value2)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleTimeStampTZ::operator -(3)\n"
				});
			}
			if (value1.IsNull)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleTimeStampTZ::operator -(3)\n"
					});
				}
				return OracleTimeStampTZ.Null;
			}
			int num = 0;
			OpoTSCtx ctx = new OpoTSCtx(TimeStampType.TSType_TSZ);
			OpoITLValCtx opoITLValCtx = default(OpoITLValCtx);
			GCHandle gchandle = GCHandle.Alloc(opoITLValCtx, GCHandleType.Pinned);
			OpoTSValCtx* pCtx;
			try
			{
				OracleIntervalDS.FillValCtxFromTimeSpan(&opoITLValCtx, value2);
				num = OpsTSA.AllocValCtxForSubInterval(value1.m_opoTSCtx.m_pValCtx, &opoITLValCtx, out pCtx);
			}
			finally
			{
				if (gchandle.IsAllocated)
				{
					gchandle.Free();
				}
			}
			ctx = new OpoTSCtx(pCtx);
			if (num != 0)
			{
				throw new OracleTypeException(num, new object[0]);
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleTimeStampTZ::operator -()\n"
				});
			}
			return new OracleTimeStampTZ(ctx);
		}

		// Token: 0x0600063A RID: 1594 RVA: 0x0004163C File Offset: 0x0004063C
		public static explicit operator OracleTimeStampTZ(string tsStr)
		{
			return new OracleTimeStampTZ(tsStr);
		}

		// Token: 0x0600063B RID: 1595 RVA: 0x00041644 File Offset: 0x00040644
		public static explicit operator DateTime(OracleTimeStampTZ value1)
		{
			if (value1.m_bNotNull)
			{
				return value1.Value;
			}
			throw new OracleNullValueException();
		}

		// Token: 0x0600063C RID: 1596 RVA: 0x0004165C File Offset: 0x0004065C
		public unsafe static implicit operator OracleTimeStampTZ(OracleDate value1)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleTimeStampTZ::operator OracleTimeStampTZ(1)\n"
				});
			}
			if (value1.IsNull)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleTimeStampTZ::operator OracleTimeStampTZ(1)\n"
					});
				}
				return OracleTimeStampTZ.Null;
			}
			int num = 0;
			OpoTSValCtx* pCtx;
			try
			{
				num = OpsTSZ.AllocValCtxForFromDate(value1.GetValCtx(), out pCtx);
			}
			catch (Exception ex)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex);
				}
				throw;
			}
			if (num != 0)
			{
				throw new OracleTypeException(num, new object[0]);
			}
			OpoTSCtx ctx = new OpoTSCtx(pCtx);
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleTimeStampTZ::operator OracleTimeStampTZ(1)\n"
				});
			}
			return new OracleTimeStampTZ(ctx);
		}

		// Token: 0x0600063D RID: 1597 RVA: 0x00041728 File Offset: 0x00040728
		public static explicit operator OracleTimeStampTZ(OracleTimeStamp value1)
		{
			if (!value1.IsNull)
			{
				return OracleTimeStamp.ToTSZ(value1);
			}
			return OracleTimeStampTZ.Null;
		}

		// Token: 0x0600063E RID: 1598 RVA: 0x0004173F File Offset: 0x0004073F
		public static explicit operator OracleTimeStampTZ(OracleTimeStampLTZ value1)
		{
			if (!value1.IsNull)
			{
				return OracleTimeStampLTZ.ToTSZ(value1);
			}
			return OracleTimeStampTZ.Null;
		}

		// Token: 0x0600063F RID: 1599 RVA: 0x00041756 File Offset: 0x00040756
		public static implicit operator OracleTimeStampTZ(DateTime value1)
		{
			return new OracleTimeStampTZ(value1, null);
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x06000640 RID: 1600 RVA: 0x00041760 File Offset: 0x00040760
		public unsafe byte[] BinData
		{
			get
			{
				if (!this.m_bNotNull)
				{
					throw new OracleNullValueException();
				}
				byte[] array = new byte[13];
				int num = 0;
				int num2 = 0;
				try
				{
					num2 = OpsTSZ.ToBytes(this.m_opoTSCtx.m_pValCtx, array, &num);
				}
				catch (Exception ex)
				{
					if (OraTrace.m_TraceLevel != 0U)
					{
						OraTrace.TraceExceptionInfo(ex);
					}
					throw;
				}
				if (num2 != 0)
				{
					throw new OracleTypeException(num2, new object[0]);
				}
				return array;
			}
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x06000641 RID: 1601 RVA: 0x000417D0 File Offset: 0x000407D0
		public bool IsNull
		{
			get
			{
				return !this.m_bNotNull;
			}
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x06000642 RID: 1602 RVA: 0x000417DC File Offset: 0x000407DC
		public unsafe DateTime Value
		{
			get
			{
				if (this.m_bNotNull)
				{
					OpoTSValCtx opoTSValCtx;
					opoTSValCtx.m_year = (short)this.m_year;
					opoTSValCtx.m_month = (byte)this.m_month;
					opoTSValCtx.m_day = (byte)this.m_day;
					opoTSValCtx.m_hour = (byte)this.m_hour;
					opoTSValCtx.m_minute = (byte)this.m_minute;
					opoTSValCtx.m_second = (byte)this.m_second;
					opoTSValCtx.m_fSecond = this.m_fSecond;
					return DateTimeConv.GetDateTime(&opoTSValCtx, OracleDbType.TimeStamp, false);
				}
				throw new OracleNullValueException();
			}
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x06000643 RID: 1603 RVA: 0x00041863 File Offset: 0x00040863
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

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x06000644 RID: 1604 RVA: 0x00041879 File Offset: 0x00040879
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

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x06000645 RID: 1605 RVA: 0x0004188F File Offset: 0x0004088F
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

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x06000646 RID: 1606 RVA: 0x000418A5 File Offset: 0x000408A5
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

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x06000647 RID: 1607 RVA: 0x000418BB File Offset: 0x000408BB
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

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x06000648 RID: 1608 RVA: 0x000418D1 File Offset: 0x000408D1
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

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x06000649 RID: 1609 RVA: 0x000418E7 File Offset: 0x000408E7
		public double Millisecond
		{
			get
			{
				if (this.m_bNotNull)
				{
					return (double)this.m_fSecond / 1000000.0;
				}
				throw new OracleNullValueException();
			}
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x0600064A RID: 1610 RVA: 0x00041909 File Offset: 0x00040909
		public int Nanosecond
		{
			get
			{
				if (this.m_bNotNull)
				{
					return this.m_fSecond;
				}
				throw new OracleNullValueException();
			}
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x0600064B RID: 1611 RVA: 0x0004191F File Offset: 0x0004091F
		public string TimeZone
		{
			get
			{
				if (this.m_bNotNull)
				{
					if (this.m_timeZone == null)
					{
						this.m_timeZone = this.GetTimeZoneName();
					}
					return this.m_timeZone;
				}
				throw new OracleNullValueException();
			}
		}

		// Token: 0x0600064C RID: 1612 RVA: 0x0004194C File Offset: 0x0004094C
		public unsafe OracleTimeStampTZ AddYears(int years)
		{
			int num = 0;
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleTimeStampTZ::AddYears()\n"
				});
			}
			if (years < -999999999 || years > 999999999)
			{
				throw new ArgumentOutOfRangeException();
			}
			if (!this.m_bNotNull)
			{
				throw new OracleNullValueException();
			}
			OpoTSValCtx* pCtx;
			try
			{
				num = OpsTSA.AllocValCtxForAddYears(this.m_opoTSCtx.m_pValCtx, years, out pCtx);
			}
			catch (Exception ex)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex);
				}
				throw;
			}
			if (num != 0)
			{
				throw new OracleTypeException(num, new object[0]);
			}
			OpoTSCtx ctx = new OpoTSCtx(pCtx);
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleTimeStampTZ::AddYears()\n"
				});
			}
			return new OracleTimeStampTZ(ctx);
		}

		// Token: 0x0600064D RID: 1613 RVA: 0x00041A14 File Offset: 0x00040A14
		public unsafe OracleTimeStampTZ AddMonths(long months)
		{
			int num = 0;
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleTimeStampTZ::AddMonths()\n"
				});
			}
			if (months <= -12000000000L || months >= 12000000000L)
			{
				throw new ArgumentOutOfRangeException();
			}
			if (!this.m_bNotNull)
			{
				throw new OracleNullValueException();
			}
			OpoTSValCtx* pCtx;
			try
			{
				num = OpsTSA.AllocValCtxForAddMonths(this.m_opoTSCtx.m_pValCtx, months, out pCtx);
			}
			catch (Exception ex)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex);
				}
				throw;
			}
			if (num != 0)
			{
				throw new OracleTypeException(num, new object[0]);
			}
			OpoTSCtx ctx = new OpoTSCtx(pCtx);
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleTimeStampTZ::AddMonths()\n"
				});
			}
			return new OracleTimeStampTZ(ctx);
		}

		// Token: 0x0600064E RID: 1614 RVA: 0x00041AE4 File Offset: 0x00040AE4
		public unsafe OracleTimeStampTZ AddDays(double days)
		{
			int num = 0;
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleTimeStampTZ::AddDays()\n"
				});
			}
			if (days <= -1000000000.0 || days >= 1000000000.0)
			{
				throw new ArgumentOutOfRangeException();
			}
			if (!this.m_bNotNull)
			{
				throw new OracleNullValueException();
			}
			OpoTSValCtx* pCtx;
			try
			{
				num = OpsTSA.AllocValCtxForAddDays(this.m_opoTSCtx.m_pValCtx, days, out pCtx);
			}
			catch (Exception ex)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex);
				}
				throw;
			}
			if (num != 0)
			{
				throw new OracleTypeException(num, new object[0]);
			}
			OpoTSCtx ctx = new OpoTSCtx(pCtx);
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleTimeStampTZ::AddDays()\n"
				});
			}
			return new OracleTimeStampTZ(ctx);
		}

		// Token: 0x0600064F RID: 1615 RVA: 0x00041BB4 File Offset: 0x00040BB4
		public unsafe OracleTimeStampTZ AddHours(double hours)
		{
			int num = 0;
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleTimeStampTZ::AddDays()\n"
				});
			}
			if (hours <= -24000000000.0 || hours >= 24000000000.0)
			{
				throw new ArgumentOutOfRangeException();
			}
			if (!this.m_bNotNull)
			{
				throw new OracleNullValueException();
			}
			OpoTSValCtx* pCtx;
			try
			{
				num = OpsTSA.AllocValCtxForAddHours(this.m_opoTSCtx.m_pValCtx, hours, out pCtx);
			}
			catch (Exception ex)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex);
				}
				throw;
			}
			if (num != 0)
			{
				throw new OracleTypeException(num, new object[0]);
			}
			OpoTSCtx ctx = new OpoTSCtx(pCtx);
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleTimeStampTZ::AddDays()\n"
				});
			}
			return new OracleTimeStampTZ(ctx);
		}

		// Token: 0x06000650 RID: 1616 RVA: 0x00041C84 File Offset: 0x00040C84
		public unsafe OracleTimeStampTZ AddMinutes(double minutes)
		{
			int num = 0;
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleTimeStampTZ::AddMinutes()\n"
				});
			}
			if (minutes <= -1440000000000.0 || minutes >= 1440000000000.0)
			{
				throw new ArgumentOutOfRangeException();
			}
			if (!this.m_bNotNull)
			{
				throw new OracleNullValueException();
			}
			OpoTSValCtx* pCtx;
			try
			{
				num = OpsTSA.AllocValCtxForAddMinutes(this.m_opoTSCtx.m_pValCtx, minutes, out pCtx);
			}
			catch (Exception ex)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex);
				}
				throw;
			}
			if (num != 0)
			{
				throw new OracleTypeException(num, new object[0]);
			}
			OpoTSCtx ctx = new OpoTSCtx(pCtx);
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleTimeStampTZ::AddMinutes()\n"
				});
			}
			return new OracleTimeStampTZ(ctx);
		}

		// Token: 0x06000651 RID: 1617 RVA: 0x00041D54 File Offset: 0x00040D54
		public unsafe OracleTimeStampTZ AddSeconds(double seconds)
		{
			int num = 0;
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleTimeStampTZ::AddSeconds()\n"
				});
			}
			if (seconds <= -86400000000000.0 || seconds >= 86400000000000.0)
			{
				throw new ArgumentOutOfRangeException();
			}
			if (!this.m_bNotNull)
			{
				throw new OracleNullValueException();
			}
			OpoTSValCtx* pCtx;
			try
			{
				num = OpsTSA.AllocValCtxForAddSeconds(this.m_opoTSCtx.m_pValCtx, seconds, out pCtx);
			}
			catch (Exception ex)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex);
				}
				throw;
			}
			if (num != 0)
			{
				throw new OracleTypeException(num, new object[0]);
			}
			OpoTSCtx ctx = new OpoTSCtx(pCtx);
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleTimeStampTZ::AddSeconds()\n"
				});
			}
			return new OracleTimeStampTZ(ctx);
		}

		// Token: 0x06000652 RID: 1618 RVA: 0x00041E24 File Offset: 0x00040E24
		public unsafe OracleTimeStampTZ AddMilliseconds(double milliseconds)
		{
			int num = 0;
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleTimeStampTZ::AddMilliseconds()\n"
				});
			}
			if (milliseconds <= -86400000000000000.0 || milliseconds >= 86400000000000000.0)
			{
				throw new ArgumentOutOfRangeException();
			}
			if (!this.m_bNotNull)
			{
				throw new OracleNullValueException();
			}
			OpoTSValCtx* pCtx;
			try
			{
				num = OpsTSA.AllocValCtxForAddMilliseconds(this.m_opoTSCtx.m_pValCtx, milliseconds, out pCtx);
			}
			catch (Exception ex)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex);
				}
				throw;
			}
			if (num != 0)
			{
				throw new OracleTypeException(num, new object[0]);
			}
			OpoTSCtx ctx = new OpoTSCtx(pCtx);
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleTimeStampTZ::AddMilliseconds()\n"
				});
			}
			return new OracleTimeStampTZ(ctx);
		}

		// Token: 0x06000653 RID: 1619 RVA: 0x00041EF4 File Offset: 0x00040EF4
		public unsafe OracleTimeStampTZ AddNanoseconds(long nanoseconds)
		{
			int num = 0;
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleTimeStampTZ::AddNanoseconds()\n"
				});
			}
			if (!this.m_bNotNull)
			{
				throw new OracleNullValueException();
			}
			OpoTSValCtx* pCtx;
			try
			{
				num = OpsTSA.AllocValCtxForAddNanoseconds(this.m_opoTSCtx.m_pValCtx, nanoseconds, out pCtx);
			}
			catch (Exception ex)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex);
				}
				throw;
			}
			if (num != 0)
			{
				throw new OracleTypeException(num, new object[0]);
			}
			OpoTSCtx ctx = new OpoTSCtx(pCtx);
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleTimeStampTZ::AddNanoseconds()\n"
				});
			}
			return new OracleTimeStampTZ(ctx);
		}

		// Token: 0x06000654 RID: 1620 RVA: 0x00041FA4 File Offset: 0x00040FA4
		public int CompareTo(object obj)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleTimeStampTZ::CompareTo()\n"
				});
			}
			if (obj.GetType() != typeof(OracleTimeStampTZ))
			{
				throw new ArgumentException();
			}
			OracleTimeStampTZ oracleTimeStampTZ = (OracleTimeStampTZ)obj;
			CompareNullEnum compareNullEnum = InternalTypes.CompareNull(!this.m_bNotNull, !oracleTimeStampTZ.m_bNotNull);
			if (compareNullEnum == CompareNullEnum.BothNull)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleTimeStampTZ::CompareTo()\n"
					});
				}
				return 0;
			}
			if (compareNullEnum == CompareNullEnum.FirstNullOnly)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleTimeStampTZ::CompareTo()\n"
					});
				}
				return -1;
			}
			if (compareNullEnum == CompareNullEnum.SecondNullOnly)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleTimeStampTZ::CompareTo()\n"
					});
				}
				return 1;
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleTimeStampTZ::CompareTo()\n"
				});
			}
			return TimeStamp.Compare(this.m_opoTSCtx, oracleTimeStampTZ.m_opoTSCtx);
		}

		// Token: 0x06000655 RID: 1621 RVA: 0x000420AC File Offset: 0x000410AC
		public override bool Equals(object obj)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleTimeStampTZ::Equals()\n"
				});
			}
			if (obj == null)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleTimeStampTZ::Equals()\n"
					});
				}
				return false;
			}
			if (obj.GetType() != typeof(OracleTimeStampTZ))
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleTimeStampTZ::Equals()\n"
					});
				}
				return false;
			}
			OracleTimeStampTZ value = (OracleTimeStampTZ)obj;
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleTimeStampTZ::Equals()\n"
				});
			}
			return OracleTimeStampTZ.Equals(this, value);
		}

		// Token: 0x06000656 RID: 1622 RVA: 0x00042161 File Offset: 0x00041161
		public override int GetHashCode()
		{
			if (this.m_bNotNull)
			{
				return this.m_opoTSCtx.GetHashCode();
			}
			return 0;
		}

		// Token: 0x06000657 RID: 1623 RVA: 0x00042178 File Offset: 0x00041178
		public unsafe OracleIntervalDS GetDaysBetween(OracleTimeStampTZ value1)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleTimeStampTZ::GetDaysBetween()\n"
				});
			}
			if (!this.m_bNotNull || !value1.m_bNotNull)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleTimeStampTZ::GetDaysBetween()\n"
					});
				}
				return OracleIntervalDS.Null;
			}
			int num = 0;
			OpoITLValCtx* ctx;
			try
			{
				num = OpsTSA.AllocValCtxForSubTSToIDS(this.m_opoTSCtx.m_pValCtx, value1.m_opoTSCtx.m_pValCtx, out ctx);
			}
			catch (Exception ex)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex);
				}
				throw;
			}
			if (num != 0)
			{
				throw new OracleTypeException(num, new object[0]);
			}
			OpoIDSCtx ctx2 = new OpoIDSCtx(ctx);
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleTimeStampTZ::GetDaysBetween()\n"
				});
			}
			return new OracleIntervalDS(ctx2);
		}

		// Token: 0x06000658 RID: 1624 RVA: 0x0004225C File Offset: 0x0004125C
		public unsafe OracleIntervalYM GetYearsBetween(OracleTimeStampTZ value1)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleTimeStampTZ::GetYearsBetween()\n"
				});
			}
			if (!this.m_bNotNull || !value1.m_bNotNull)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleTimeStampTZ::GetYearsBetween()\n"
					});
				}
				return OracleIntervalYM.Null;
			}
			int num = 0;
			OpoITLValCtx* ctx;
			try
			{
				num = OpsTSA.AllocValCtxForSubTSToIYM(this.m_opoTSCtx.m_pValCtx, value1.m_opoTSCtx.m_pValCtx, out ctx);
			}
			catch (Exception ex)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex);
				}
				throw;
			}
			if (num != 0)
			{
				throw new OracleTypeException(num, new object[0]);
			}
			OpoIYMCtx ctx2 = new OpoIYMCtx(ctx);
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleTimeStampTZ::GetYearsBetween()\n"
				});
			}
			return new OracleIntervalYM(ctx2);
		}

		// Token: 0x06000659 RID: 1625 RVA: 0x00042340 File Offset: 0x00041340
		private unsafe string GetTimeZoneName()
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleTimeStampTZ::GetTimeZoneName()\n"
				});
			}
			if (!this.m_bNotNull)
			{
				throw new OracleNullValueException();
			}
			int num = 0;
			OpoTSValCtx* pValCtx = this.m_opoTSCtx.m_pValCtx;
			string result;
			try
			{
				num = OpsTSZ.GetTimeZoneName((int)pValCtx->m_tzHour, (int)pValCtx->m_tzMinute, (int)pValCtx->m_regid, out result);
			}
			catch (Exception ex)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex);
				}
				throw;
			}
			if (num != 0)
			{
				throw new OracleTypeException(num, new object[0]);
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleTimeStampTZ::GetTimeZoneName()\n"
				});
			}
			return result;
		}

		// Token: 0x0600065A RID: 1626 RVA: 0x000423F8 File Offset: 0x000413F8
		public unsafe TimeSpan GetTimeZoneOffset()
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleTimeStampTZ::GetTimeZoneOffset()\n"
				});
			}
			if (this.m_bNotNull)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleTimeStampTZ::GetTimeZoneOffset()\n"
					});
				}
				return new TimeSpan((int)this.m_opoTSCtx.m_pValCtx->m_tzHour, (int)this.m_opoTSCtx.m_pValCtx->m_tzMinute, 0);
			}
			throw new OracleNullValueException();
		}

		// Token: 0x0600065B RID: 1627 RVA: 0x00042474 File Offset: 0x00041474
		public unsafe OracleTimeStampLTZ ToLocalTime()
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleTimeStampTZ::ToLocalTime()\n"
				});
			}
			if (!this.m_bNotNull)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleTimeStampTZ::ToLocalTime()\n"
					});
				}
				return OracleTimeStampLTZ.Null;
			}
			int num = 0;
			OpoTSValCtx* pCtx;
			try
			{
				num = OpsTSA.AllocValCtxForToTSL(this.m_opoTSCtx.m_pValCtx, out pCtx);
			}
			catch (Exception ex)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex);
				}
				throw;
			}
			if (num != 0)
			{
				throw new OracleTypeException(num, new object[0]);
			}
			OpoTSCtx ctx = new OpoTSCtx(pCtx);
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleTimeStampTZ::ToLocalTime()\n"
				});
			}
			return new OracleTimeStampLTZ(ctx);
		}

		// Token: 0x0600065C RID: 1628 RVA: 0x00042544 File Offset: 0x00041544
		public unsafe OracleDate ToOracleDate()
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleTimeStampTZ::ToOracleDate()\n"
				});
			}
			if (!this.m_bNotNull)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleTimeStampTZ::ToOracleDate()\n"
					});
				}
				return OracleDate.Null;
			}
			int num = 0;
			OpoDatValCtx* pCtx;
			try
			{
				num = OpsTSA.AllocValCtxForToDate(this.m_opoTSCtx.m_pValCtx, out pCtx);
			}
			catch (Exception ex)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex);
				}
				throw;
			}
			if (num != 0)
			{
				throw new OracleTypeException(num, new object[0]);
			}
			OpoDatCtx ctx = new OpoDatCtx(pCtx);
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleTimeStampTZ::ToOracleDate()\n"
				});
			}
			return new OracleDate(ctx);
		}

		// Token: 0x0600065D RID: 1629 RVA: 0x00042614 File Offset: 0x00041614
		public OracleTimeStamp ToOracleTimeStamp()
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleTimeStampTZ::ToOracleTimeStamp()\n"
				});
			}
			if (this.m_bNotNull)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleTimeStampTZ::ToOracleTimeStamp()\n"
					});
				}
				return OracleTimeStampTZ.ToTS(this);
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleTimeStampTZ::ToOracleTimeStamp()\n"
				});
			}
			return OracleTimeStamp.Null;
		}

		// Token: 0x0600065E RID: 1630 RVA: 0x00042694 File Offset: 0x00041694
		public OracleTimeStampLTZ ToOracleTimeStampLTZ()
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleTimeStampTZ::ToOracleTimeStampLTZ()\n"
				});
			}
			if (this.m_bNotNull)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleTimeStampTZ::ToOracleTimeStampLTZ()\n"
					});
				}
				return OracleTimeStampTZ.ToTSL(this);
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleTimeStampTZ::ToOracleTimeStampLTZ()\n"
				});
			}
			return OracleTimeStampLTZ.Null;
		}

		// Token: 0x0600065F RID: 1631 RVA: 0x00042714 File Offset: 0x00041714
		public override string ToString()
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleTimeStampTZ::ToString()\n"
				});
			}
			if (this.m_bNotNull)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleTimeStampTZ::ToString()\n"
					});
				}
				return TimeStamp.ToTSString(this.m_opoTSCtx, this.m_fSecondPrec, TimeStampType.TSType_TSZ);
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleTimeStampTZ::ToString()\n"
				});
			}
			return "null";
		}

		// Token: 0x06000660 RID: 1632 RVA: 0x00042798 File Offset: 0x00041798
		public OracleTimeStampTZ ToUniversalTime()
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleTimeStampTZ::ToUniversalTime()\n"
				});
			}
			if (this.m_bNotNull)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleTimeStampTZ::ToUniversalTime()\n"
					});
				}
				return TimeStamp.ToUniversalTime(this.m_opoTSCtx);
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleTimeStampTZ::ToUniversalTime()\n"
				});
			}
			return OracleTimeStampTZ.Null;
		}

		// Token: 0x06000661 RID: 1633 RVA: 0x00042815 File Offset: 0x00041815
		internal unsafe OracleTimeStampTZ(OpoTSValCtx* pCtx)
		{
			this = new OracleTimeStampTZ(pCtx, 9);
		}

		// Token: 0x06000662 RID: 1634 RVA: 0x00042820 File Offset: 0x00041820
		internal unsafe OracleTimeStampTZ(OpoTSValCtx* pCtx, int fSecondPrec)
		{
			this.m_opoTSCtx = new OpoTSCtx(pCtx);
			int num = 0;
			OpoTSValCtx opoTSValCtx;
			try
			{
				num = OpsTSZ.ConvertToTSL(this.m_opoTSCtx.m_pValCtx, &opoTSValCtx);
			}
			catch (Exception ex)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex);
				}
				throw;
			}
			if (num != 0)
			{
				throw new OracleTypeException(num, new object[0]);
			}
			this.m_year = (int)opoTSValCtx.m_year;
			this.m_month = (int)opoTSValCtx.m_month;
			this.m_day = (int)opoTSValCtx.m_day;
			this.m_hour = (int)opoTSValCtx.m_hour;
			this.m_minute = (int)opoTSValCtx.m_minute;
			this.m_second = (int)opoTSValCtx.m_second;
			this.m_fSecond = opoTSValCtx.m_fSecond;
			this.m_bNotNull = true;
			this.m_fSecondPrec = fSecondPrec;
			this.m_timeZone = null;
		}

		// Token: 0x06000663 RID: 1635 RVA: 0x000428F4 File Offset: 0x000418F4
		internal unsafe OracleTimeStampTZ(OpoTSCtx ctx)
		{
			this.m_opoTSCtx = ctx;
			int num = 0;
			OpoTSValCtx opoTSValCtx;
			try
			{
				num = OpsTSZ.ConvertToTSL(this.m_opoTSCtx.m_pValCtx, &opoTSValCtx);
			}
			catch (Exception ex)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex);
				}
				throw;
			}
			if (num != 0)
			{
				throw new OracleTypeException(num, new object[0]);
			}
			this.m_year = (int)opoTSValCtx.m_year;
			this.m_month = (int)opoTSValCtx.m_month;
			this.m_day = (int)opoTSValCtx.m_day;
			this.m_hour = (int)opoTSValCtx.m_hour;
			this.m_minute = (int)opoTSValCtx.m_minute;
			this.m_second = (int)opoTSValCtx.m_second;
			this.m_fSecond = opoTSValCtx.m_fSecond;
			this.m_bNotNull = true;
			this.m_fSecondPrec = 9;
			this.m_timeZone = null;
		}

		// Token: 0x06000664 RID: 1636 RVA: 0x000429C4 File Offset: 0x000419C4
		internal unsafe static OracleTimeStampTZ GetMaxValue(int year, int month, int day, int hour, int minute, int second, int nanosecond, int tzHours, int tzMinutes)
		{
			OpoTSValCtx* ptr = null;
			int num = 0;
			try
			{
				num = OpsTSZ.AllocMaxValue(year, month, day, hour, minute, second, nanosecond, tzHours, tzMinutes, out ptr);
			}
			catch (Exception ex)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex);
				}
				num = ErrRes.INT_ERR;
				throw;
			}
			finally
			{
				if (num != 0)
				{
					if (ptr != null)
					{
						try
						{
							OpsTSZ.FreeValCtx(ptr);
						}
						catch (Exception ex2)
						{
							if (OraTrace.m_TraceLevel != 0U)
							{
								OraTrace.TraceExceptionInfo(ex2);
							}
						}
						ptr = null;
					}
					if (num != ErrRes.INT_ERR)
					{
						throw new OracleTypeException(num, new object[0]);
					}
				}
			}
			return new OracleTimeStampTZ(ptr);
		}

		// Token: 0x06000665 RID: 1637 RVA: 0x00042A70 File Offset: 0x00041A70
		internal unsafe static OracleTimeStampTZ GetMinValue(int year, int month, int day, int hour, int minute, int second, int nanosecond, int tzHours, int tzMinutes)
		{
			OpoTSValCtx* ptr = null;
			int num = 0;
			try
			{
				num = OpsTSZ.AllocMinValue(year, month, day, hour, minute, second, nanosecond, tzHours, tzMinutes, out ptr);
			}
			catch (Exception ex)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex);
				}
				num = ErrRes.INT_ERR;
				throw;
			}
			finally
			{
				if (num != 0)
				{
					if (ptr != null)
					{
						try
						{
							OpsTSZ.FreeValCtx(ptr);
						}
						catch (Exception ex2)
						{
							if (OraTrace.m_TraceLevel != 0U)
							{
								OraTrace.TraceExceptionInfo(ex2);
							}
						}
						ptr = null;
					}
					if (num != ErrRes.INT_ERR)
					{
						throw new OracleTypeException(num, new object[0]);
					}
				}
			}
			return new OracleTimeStampTZ(ptr);
		}

		// Token: 0x06000666 RID: 1638 RVA: 0x00042B1C File Offset: 0x00041B1C
		internal unsafe static OracleTimeStamp ToTS(OracleTimeStampTZ value1)
		{
			if (!value1.m_bNotNull)
			{
				return OracleTimeStamp.Null;
			}
			int num = 0;
			OpoTSValCtx* pCtx;
			try
			{
				num = OpsTSA.AllocValCtxForToTS(value1.m_opoTSCtx.m_pValCtx, out pCtx);
			}
			catch (Exception ex)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex);
				}
				throw;
			}
			if (num != 0)
			{
				throw new OracleTypeException(num, new object[0]);
			}
			OpoTSCtx ctx = new OpoTSCtx(pCtx);
			return new OracleTimeStamp(ctx);
		}

		// Token: 0x06000667 RID: 1639 RVA: 0x00042B90 File Offset: 0x00041B90
		internal unsafe static OracleTimeStampLTZ ToTSL(OracleTimeStampTZ value1)
		{
			if (!value1.m_bNotNull)
			{
				return OracleTimeStampLTZ.Null;
			}
			int num = 0;
			OpoTSValCtx* pCtx;
			try
			{
				num = OpsTSA.AllocValCtxForToTSL(value1.m_opoTSCtx.m_pValCtx, out pCtx);
			}
			catch (Exception ex)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex);
				}
				throw;
			}
			if (num != 0)
			{
				throw new OracleTypeException(num, new object[0]);
			}
			OpoTSCtx ctx = new OpoTSCtx(pCtx);
			return new OracleTimeStampLTZ(ctx);
		}

		// Token: 0x06000668 RID: 1640 RVA: 0x00042C04 File Offset: 0x00041C04
		internal unsafe OpoTSValCtx* GetValCtx()
		{
			return this.m_opoTSCtx.m_pValCtx;
		}

		// Token: 0x06000669 RID: 1641 RVA: 0x00042C14 File Offset: 0x00041C14
		internal IntPtr DupValCtx()
		{
			IntPtr zero = IntPtr.Zero;
			int num = OpsTSA.DupValCtx(this.GetValCtx(), out zero, TimeStampType.TSType_TSZ);
			if (num != 0)
			{
				throw new OracleException(num, string.Empty, string.Empty, string.Empty);
			}
			return zero;
		}

		// Token: 0x040003E2 RID: 994
		internal const byte MaxArrSize = 13;

		// Token: 0x040003E3 RID: 995
		public static readonly OracleTimeStampTZ MaxValue = OracleTimeStampTZ.GetMaxValue(9999, 12, 31, 23, 59, 59, 999999999, 0, 0);

		// Token: 0x040003E4 RID: 996
		public static readonly OracleTimeStampTZ MinValue = OracleTimeStampTZ.GetMinValue(-4712, 1, 1, 0, 0, 0, 0, 0, 0);

		// Token: 0x040003E5 RID: 997
		public static readonly OracleTimeStampTZ Null;

		// Token: 0x040003E6 RID: 998
		private OpoTSCtx m_opoTSCtx;

		// Token: 0x040003E7 RID: 999
		private bool m_bNotNull;

		// Token: 0x040003E8 RID: 1000
		private int m_fSecondPrec;

		// Token: 0x040003E9 RID: 1001
		private int m_year;

		// Token: 0x040003EA RID: 1002
		private int m_month;

		// Token: 0x040003EB RID: 1003
		private int m_day;

		// Token: 0x040003EC RID: 1004
		private int m_hour;

		// Token: 0x040003ED RID: 1005
		private int m_minute;

		// Token: 0x040003EE RID: 1006
		private int m_second;

		// Token: 0x040003EF RID: 1007
		private int m_fSecond;

		// Token: 0x040003F0 RID: 1008
		private string m_timeZone;
	}
}
