using System;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Oracle.DataAccess.Client;

namespace Oracle.DataAccess.Types
{
	// Token: 0x0200008D RID: 141
	[XmlSchemaProvider("GetXsdType")]
	[Serializable]
	public struct OracleDate : IComparable, INullable, IXmlSerializable
	{
		// Token: 0x060006AF RID: 1711 RVA: 0x00044834 File Offset: 0x00043834
		static OracleDate()
		{
			if (!OracleInit.bSetDllDirectoryInvoked)
			{
				OracleInit.Initialize();
			}
		}

		// Token: 0x060006B0 RID: 1712 RVA: 0x0004486F File Offset: 0x0004386F
		public static XmlQualifiedName GetXsdType(XmlSchemaSet schemaSet)
		{
			return new XmlQualifiedName("DateTime", "http://www.w3.org/2001/XMLSchema");
		}

		// Token: 0x060006B1 RID: 1713 RVA: 0x00044880 File Offset: 0x00043880
		XmlSchema IXmlSerializable.GetSchema()
		{
			return null;
		}

		// Token: 0x060006B2 RID: 1714 RVA: 0x00044884 File Offset: 0x00043884
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			string text = reader.GetAttribute("null", "http://www.w3.org/2001/XMLSchema-instance");
			if (text != null && XmlConvert.ToBoolean(text))
			{
				this.m_bNotNull = false;
				return;
			}
			text = reader.ReadElementString();
			this.m_opoDatCtx = new OpoDatCtx(text);
			if (this.m_opoDatCtx.m_error != 0)
			{
				throw new ArgumentException(OracleTypeException.GetTypeMsg(this.m_opoDatCtx.m_error, new object[0]));
			}
			this.m_bNotNull = true;
		}

		// Token: 0x060006B3 RID: 1715 RVA: 0x000448F8 File Offset: 0x000438F8
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			if (!this.m_bNotNull)
			{
				writer.WriteAttributeString("xsi", "null", "http://www.w3.org/2001/XMLSchema-instance", "true");
				return;
			}
			int num = 0;
			string text;
			try
			{
				num = OpsDat.ToString(this.m_opoDatCtx.m_pValCtx, out text);
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
			writer.WriteString(text);
		}

		// Token: 0x060006B4 RID: 1716 RVA: 0x00044978 File Offset: 0x00043978
		public OracleDate(int year, int month, int day, int hour, int minute, int second)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleDate::OracleDate(1)\n"
				});
			}
			if (!TimeStamp.IsValidDateTime(year, month, day, hour, minute, second, 0))
			{
				throw new ArgumentOutOfRangeException();
			}
			this.m_opoDatCtx = new OpoDatCtx(year, month, day, hour, minute, second);
			if (this.m_opoDatCtx.m_error != 0)
			{
				throw new ArgumentException(OracleTypeException.GetTypeMsg(this.m_opoDatCtx.m_error, new object[0]));
			}
			this.m_bNotNull = true;
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleDate::OracleDate(1)\n"
				});
			}
		}

		// Token: 0x060006B5 RID: 1717 RVA: 0x00044A1A File Offset: 0x00043A1A
		public OracleDate(int year, int month, int day)
		{
			this = new OracleDate(year, month, day, 0, 0, 0);
		}

		// Token: 0x060006B6 RID: 1718 RVA: 0x00044A28 File Offset: 0x00043A28
		public OracleDate(DateTime data)
		{
			this = new OracleDate(data.Year, data.Month, data.Day, data.Hour, data.Minute, data.Second);
		}

		// Token: 0x060006B7 RID: 1719 RVA: 0x00044A5C File Offset: 0x00043A5C
		public OracleDate(byte[] binData)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleDate::OracleDate()\n"
				});
			}
			if (binData == null)
			{
				throw new ArgumentNullException();
			}
			this.m_opoDatCtx = new OpoDatCtx(binData);
			if (this.m_opoDatCtx.m_error != 0)
			{
				throw new ArgumentException(OracleTypeException.GetTypeMsg(this.m_opoDatCtx.m_error, new object[0]));
			}
			this.m_bNotNull = true;
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleDate::OracleDate(2)\n"
				});
			}
		}

		// Token: 0x060006B8 RID: 1720 RVA: 0x00044AE8 File Offset: 0x00043AE8
		public OracleDate(string datStr)
		{
			if (datStr == null)
			{
				throw new ArgumentNullException();
			}
			this.m_opoDatCtx = new OpoDatCtx(datStr);
			if (this.m_opoDatCtx.m_error != 0)
			{
				throw new ArgumentException(OracleTypeException.GetTypeMsg(this.m_opoDatCtx.m_error, new object[0]));
			}
			this.m_bNotNull = true;
		}

		// Token: 0x060006B9 RID: 1721 RVA: 0x00044B3C File Offset: 0x00043B3C
		public static bool Equals(OracleDate value1, OracleDate value2)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleDate::Equals(1)\n"
				});
			}
			CompareNullEnum compareNullEnum = InternalTypes.CompareNull(!value1.m_bNotNull, !value2.m_bNotNull);
			if (compareNullEnum == CompareNullEnum.BothNull)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleDate::Equals(1)\n"
					});
				}
				return true;
			}
			if (compareNullEnum != CompareNullEnum.BothNotNull)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleDate::Equals(1)\n"
					});
				}
				return false;
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleDate::Equals(1)\n"
				});
			}
			return OracleDate.Compare(value1.m_opoDatCtx, value2.m_opoDatCtx) == 0;
		}

		// Token: 0x060006BA RID: 1722 RVA: 0x00044C00 File Offset: 0x00043C00
		public static bool GreaterThan(OracleDate value1, OracleDate value2)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleDate::GreaterThan()\n"
				});
			}
			CompareNullEnum compareNullEnum = InternalTypes.CompareNull(!value1.m_bNotNull, !value2.m_bNotNull);
			if (compareNullEnum == CompareNullEnum.BothNull)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleDate::GreaterThan()\n"
					});
				}
				return false;
			}
			if (compareNullEnum == CompareNullEnum.FirstNullOnly)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleDate::GreaterThan()\n"
					});
				}
				return false;
			}
			if (compareNullEnum == CompareNullEnum.SecondNullOnly)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleDate::GreaterThan()\n"
					});
				}
				return true;
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleDate::GreaterThan()\n"
				});
			}
			return OracleDate.Compare(value1.m_opoDatCtx, value2.m_opoDatCtx) > 0;
		}

		// Token: 0x060006BB RID: 1723 RVA: 0x00044CEC File Offset: 0x00043CEC
		public static bool GreaterThanOrEqual(OracleDate value1, OracleDate value2)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleDate::GreaterThanOrEqual()\n"
				});
			}
			CompareNullEnum compareNullEnum = InternalTypes.CompareNull(!value1.m_bNotNull, !value2.m_bNotNull);
			if (compareNullEnum == CompareNullEnum.BothNull)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleDate::GreaterThanOrEqual()\n"
					});
				}
				return true;
			}
			if (compareNullEnum == CompareNullEnum.FirstNullOnly)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleDate::GreaterThanOrEqual()\n"
					});
				}
				return false;
			}
			if (compareNullEnum == CompareNullEnum.SecondNullOnly)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleDate::GreaterThanOrEqual()\n"
					});
				}
				return true;
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleDate::GreaterThanOrEqual()\n"
				});
			}
			return OracleDate.Compare(value1.m_opoDatCtx, value2.m_opoDatCtx) >= 0;
		}

		// Token: 0x060006BC RID: 1724 RVA: 0x00044DD8 File Offset: 0x00043DD8
		public static bool LessThan(OracleDate value1, OracleDate value2)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleDate::LessThan()\n"
				});
			}
			CompareNullEnum compareNullEnum = InternalTypes.CompareNull(!value1.m_bNotNull, !value2.m_bNotNull);
			if (compareNullEnum == CompareNullEnum.BothNull)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleDate::LessThan()\n"
					});
				}
				return false;
			}
			if (compareNullEnum == CompareNullEnum.FirstNullOnly)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleDate::LessThan()\n"
					});
				}
				return true;
			}
			if (compareNullEnum == CompareNullEnum.SecondNullOnly)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleDate::LessThan()\n"
					});
				}
				return false;
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleDate::LessThan()\n"
				});
			}
			return OracleDate.Compare(value1.m_opoDatCtx, value2.m_opoDatCtx) < 0;
		}

		// Token: 0x060006BD RID: 1725 RVA: 0x00044EC4 File Offset: 0x00043EC4
		public static bool LessThanOrEqual(OracleDate value1, OracleDate value2)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleDate::LessThanOrEqual()\n"
				});
			}
			CompareNullEnum compareNullEnum = InternalTypes.CompareNull(!value1.m_bNotNull, !value2.m_bNotNull);
			if (compareNullEnum == CompareNullEnum.BothNull)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleDate::LessThanOrEqual()\n"
					});
				}
				return true;
			}
			if (compareNullEnum == CompareNullEnum.FirstNullOnly)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleDate::LessThanOrEqual()\n"
					});
				}
				return true;
			}
			if (compareNullEnum == CompareNullEnum.SecondNullOnly)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleDate::LessThanOrEqual()\n"
					});
				}
				return false;
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleDate::LessThanOrEqual()\n"
				});
			}
			return OracleDate.Compare(value1.m_opoDatCtx, value2.m_opoDatCtx) <= 0;
		}

		// Token: 0x060006BE RID: 1726 RVA: 0x00044FAD File Offset: 0x00043FAD
		public static bool NotEquals(OracleDate value1, OracleDate value2)
		{
			return !OracleDate.Equals(value1, value2);
		}

		// Token: 0x060006BF RID: 1727 RVA: 0x00044FBC File Offset: 0x00043FBC
		public unsafe static OracleDate GetSysDate()
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleDate::GetSysDate()\n"
				});
			}
			int num = 0;
			OpoDatCtx ctx;
			try
			{
				OpoDatValCtx* pCtx;
				num = OpsDat.AllocValCtxForSysDate(out pCtx);
				ctx = new OpoDatCtx(pCtx);
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
					" (EXIT)  OracleDate::GetSysDate()\n"
				});
			}
			return new OracleDate(ctx);
		}

		// Token: 0x060006C0 RID: 1728 RVA: 0x00045054 File Offset: 0x00044054
		public static OracleDate Parse(string datStr)
		{
			if (datStr == null)
			{
				throw new ArgumentNullException();
			}
			return new OracleDate(datStr);
		}

		// Token: 0x060006C1 RID: 1729 RVA: 0x00045065 File Offset: 0x00044065
		public static bool operator ==(OracleDate value1, OracleDate value2)
		{
			return OracleDate.Equals(value1, value2);
		}

		// Token: 0x060006C2 RID: 1730 RVA: 0x0004506E File Offset: 0x0004406E
		public static bool operator >(OracleDate value1, OracleDate value2)
		{
			return OracleDate.GreaterThan(value1, value2);
		}

		// Token: 0x060006C3 RID: 1731 RVA: 0x00045077 File Offset: 0x00044077
		public static bool operator >=(OracleDate value1, OracleDate value2)
		{
			return OracleDate.GreaterThanOrEqual(value1, value2);
		}

		// Token: 0x060006C4 RID: 1732 RVA: 0x00045080 File Offset: 0x00044080
		public static bool operator <(OracleDate value1, OracleDate value2)
		{
			return OracleDate.LessThan(value1, value2);
		}

		// Token: 0x060006C5 RID: 1733 RVA: 0x00045089 File Offset: 0x00044089
		public static bool operator <=(OracleDate value1, OracleDate value2)
		{
			return OracleDate.LessThanOrEqual(value1, value2);
		}

		// Token: 0x060006C6 RID: 1734 RVA: 0x00045092 File Offset: 0x00044092
		public static bool operator !=(OracleDate value1, OracleDate value2)
		{
			return OracleDate.NotEquals(value1, value2);
		}

		// Token: 0x060006C7 RID: 1735 RVA: 0x0004509C File Offset: 0x0004409C
		public unsafe static explicit operator OracleDate(OracleTimeStamp value1)
		{
			if (value1.IsNull)
			{
				return OracleDate.Null;
			}
			int num = 0;
			OpoDatValCtx* pCtx;
			try
			{
				num = OpsTSA.AllocValCtxForToDate(value1.GetValCtx(), out pCtx);
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
			return new OracleDate(ctx);
		}

		// Token: 0x060006C8 RID: 1736 RVA: 0x00045108 File Offset: 0x00044108
		public static explicit operator OracleDate(string dateStr)
		{
			return new OracleDate(dateStr);
		}

		// Token: 0x060006C9 RID: 1737 RVA: 0x00045110 File Offset: 0x00044110
		public static explicit operator DateTime(OracleDate value1)
		{
			if (value1.m_bNotNull)
			{
				return DateTimeConv.GetDateTime(value1.m_opoDatCtx.m_pValCtx, OracleDbType.Date, false);
			}
			throw new OracleNullValueException();
		}

		// Token: 0x060006CA RID: 1738 RVA: 0x00045135 File Offset: 0x00044135
		public static explicit operator OracleDate(DateTime value1)
		{
			return new OracleDate(value1);
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x060006CB RID: 1739 RVA: 0x00045140 File Offset: 0x00044140
		public byte[] BinData
		{
			get
			{
				if (this.m_bNotNull)
				{
					byte[] array = new byte[7];
					OracleDate.ToBytes(this.m_opoDatCtx.m_pValCtx, array);
					return array;
				}
				throw new OracleNullValueException();
			}
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x060006CC RID: 1740 RVA: 0x00045175 File Offset: 0x00044175
		public bool IsNull
		{
			get
			{
				return !this.m_bNotNull;
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x060006CD RID: 1741 RVA: 0x00045180 File Offset: 0x00044180
		public DateTime Value
		{
			get
			{
				if (this.m_bNotNull)
				{
					return DateTimeConv.GetDateTime(this.m_opoDatCtx.m_pValCtx, OracleDbType.Date, false);
				}
				throw new OracleNullValueException();
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x060006CE RID: 1742 RVA: 0x000451A3 File Offset: 0x000441A3
		public int Year
		{
			get
			{
				if (this.m_bNotNull)
				{
					return this.GetDatData(0);
				}
				throw new OracleNullValueException();
			}
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x060006CF RID: 1743 RVA: 0x000451BA File Offset: 0x000441BA
		public int Month
		{
			get
			{
				if (this.m_bNotNull)
				{
					return this.GetDatData(1);
				}
				throw new OracleNullValueException();
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x060006D0 RID: 1744 RVA: 0x000451D1 File Offset: 0x000441D1
		public int Day
		{
			get
			{
				if (this.m_bNotNull)
				{
					return this.GetDatData(2);
				}
				throw new OracleNullValueException();
			}
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x060006D1 RID: 1745 RVA: 0x000451E8 File Offset: 0x000441E8
		public int Hour
		{
			get
			{
				if (this.m_bNotNull)
				{
					return this.GetDatData(3);
				}
				throw new OracleNullValueException();
			}
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x060006D2 RID: 1746 RVA: 0x000451FF File Offset: 0x000441FF
		public int Minute
		{
			get
			{
				if (this.m_bNotNull)
				{
					return this.GetDatData(4);
				}
				throw new OracleNullValueException();
			}
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x060006D3 RID: 1747 RVA: 0x00045216 File Offset: 0x00044216
		public int Second
		{
			get
			{
				if (this.m_bNotNull)
				{
					return this.GetDatData(5);
				}
				throw new OracleNullValueException();
			}
		}

		// Token: 0x060006D4 RID: 1748 RVA: 0x00045230 File Offset: 0x00044230
		internal IntPtr AllocValCtxFromCtx()
		{
			IntPtr zero = IntPtr.Zero;
			int num = OpsDat.AllocValCtxFromCtx(this.GetValCtx(), out zero);
			if (num != 0)
			{
				throw new OracleException(num, string.Empty, string.Empty, string.Empty);
			}
			return zero;
		}

		// Token: 0x060006D5 RID: 1749 RVA: 0x00045270 File Offset: 0x00044270
		public int CompareTo(object obj)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleDate::CompareTo()\n"
				});
			}
			if (obj.GetType() != typeof(OracleDate))
			{
				throw new ArgumentException();
			}
			OracleDate oracleDate = (OracleDate)obj;
			CompareNullEnum compareNullEnum = InternalTypes.CompareNull(!this.m_bNotNull, !oracleDate.m_bNotNull);
			if (compareNullEnum == CompareNullEnum.BothNull)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleDate::CompareTo()\n"
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
						" (EXIT)  OracleDate::CompareTo()\n"
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
						" (EXIT)  OracleDate::CompareTo()\n"
					});
				}
				return 1;
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleDate::CompareTo()\n"
				});
			}
			return OracleDate.Compare(this.m_opoDatCtx, oracleDate.m_opoDatCtx);
		}

		// Token: 0x060006D6 RID: 1750 RVA: 0x00045378 File Offset: 0x00044378
		public override bool Equals(object obj)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleDate::Equals()\n"
				});
			}
			if (obj == null)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleDate::Equals()\n"
					});
				}
				return false;
			}
			if (obj.GetType() != typeof(OracleDate))
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleDate::Equals()\n"
					});
				}
				return false;
			}
			OracleDate value = (OracleDate)obj;
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleDate::Equals()\n"
				});
			}
			return OracleDate.Equals(this, value);
		}

		// Token: 0x060006D7 RID: 1751 RVA: 0x00045430 File Offset: 0x00044430
		public unsafe int GetDaysBetween(OracleDate value1)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleDate::GetDaysBetween()\n"
				});
			}
			if (!this.m_bNotNull || !value1.m_bNotNull)
			{
				throw new OracleNullValueException();
			}
			int result = 0;
			int num = 0;
			try
			{
				num = OpsDat.GetDaysBetween(this.m_opoDatCtx.m_pValCtx, value1.m_opoDatCtx.m_pValCtx, &result);
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
					" (EXIT)  OracleDate::GetDaysBetween()\n"
				});
			}
			return result;
		}

		// Token: 0x060006D8 RID: 1752 RVA: 0x000454E8 File Offset: 0x000444E8
		public override int GetHashCode()
		{
			if (this.m_bNotNull)
			{
				return this.m_opoDatCtx.GetHashCode();
			}
			return 0;
		}

		// Token: 0x060006D9 RID: 1753 RVA: 0x00045500 File Offset: 0x00044500
		public unsafe OracleTimeStamp ToOracleTimeStamp()
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleDate::ToOracleTimeStamp()\n"
				});
			}
			if (!this.m_bNotNull)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleDate::ToOracleTimeStamp()\n"
					});
				}
				return OracleTimeStamp.Null;
			}
			int num = 0;
			OpoTSValCtx* pCtx;
			try
			{
				num = OpsTS.AllocValCtxForFromDate(this.m_opoDatCtx.m_pValCtx, out pCtx);
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
					" (EXIT)  OracleDate::ToOracleTimeStamp()\n"
				});
			}
			return new OracleTimeStamp(ctx);
		}

		// Token: 0x060006DA RID: 1754 RVA: 0x000455D0 File Offset: 0x000445D0
		public override string ToString()
		{
			if (!this.m_bNotNull)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleDate::ToString()\n"
					});
				}
				return "null";
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleDate::ToString()\n"
				});
			}
			int num = 0;
			string result;
			try
			{
				num = OpsDat.ToString(this.m_opoDatCtx.m_pValCtx, out result);
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
					" (EXIT)  OracleDate::ToString()\n"
				});
			}
			return result;
		}

		// Token: 0x060006DB RID: 1755 RVA: 0x00045690 File Offset: 0x00044690
		internal OracleDate(OpoDatCtx ctx)
		{
			this.m_opoDatCtx = ctx;
			this.m_bNotNull = true;
		}

		// Token: 0x060006DC RID: 1756 RVA: 0x000456A0 File Offset: 0x000446A0
		internal unsafe OracleDate(OpoDatValCtx* pCtx)
		{
			this.m_opoDatCtx = new OpoDatCtx(pCtx);
			this.m_bNotNull = true;
		}

		// Token: 0x060006DD RID: 1757 RVA: 0x000456B5 File Offset: 0x000446B5
		internal unsafe OracleDate(IntPtr pCtx)
		{
			this.m_opoDatCtx = new OpoDatCtx((OpoDatValCtx*)pCtx.ToPointer());
			this.m_bNotNull = true;
		}

		// Token: 0x060006DE RID: 1758 RVA: 0x000456D0 File Offset: 0x000446D0
		internal unsafe static bool ToBytes(OpoDatValCtx* pValCtx, byte[] bytes)
		{
			if (!TimeStamp.IsValidDateTime((int)pValCtx->m_year, (int)pValCtx->m_month, (int)pValCtx->m_day, (int)pValCtx->m_hour, (int)pValCtx->m_minute, (int)pValCtx->m_second, 0))
			{
				throw new OracleTypeException(1866, new object[0]);
			}
			bytes[0] = (byte)(pValCtx->m_year / 100 + 100);
			bytes[1] = (byte)(pValCtx->m_year % 100 + 100);
			bytes[2] = pValCtx->m_month;
			bytes[3] = pValCtx->m_day;
			bytes[4] = pValCtx->m_hour + 1;
			bytes[5] = pValCtx->m_minute + 1;
			bytes[6] = pValCtx->m_second + 1;
			return true;
		}

		// Token: 0x060006DF RID: 1759 RVA: 0x00045774 File Offset: 0x00044774
		internal unsafe static void ToBytes(OpoDatValCtx* pValCtx, byte* bytes)
		{
			*bytes = (byte)(pValCtx->m_year / 100 + 100);
			bytes[1] = (byte)(pValCtx->m_year % 100 + 100);
			bytes[2] = pValCtx->m_month;
			bytes[3] = pValCtx->m_day;
			bytes[4] = pValCtx->m_hour + 1;
			bytes[5] = pValCtx->m_minute + 1;
			bytes[6] = pValCtx->m_second + 1;
		}

		// Token: 0x060006E0 RID: 1760 RVA: 0x000457E4 File Offset: 0x000447E4
		internal unsafe static void ToBytes(OpoTSValCtx* pValCtx, byte* bytes)
		{
			*bytes = (byte)(pValCtx->m_year / 100 + 100);
			bytes[1] = (byte)(pValCtx->m_year % 100 + 100);
			bytes[2] = pValCtx->m_month;
			bytes[3] = pValCtx->m_day;
			bytes[4] = pValCtx->m_hour + 1;
			bytes[5] = pValCtx->m_minute + 1;
			bytes[6] = pValCtx->m_second + 1;
		}

		// Token: 0x060006E1 RID: 1761 RVA: 0x00045852 File Offset: 0x00044852
		internal unsafe OpoDatValCtx* GetValCtx()
		{
			return this.m_opoDatCtx.m_pValCtx;
		}

		// Token: 0x060006E2 RID: 1762 RVA: 0x00045860 File Offset: 0x00044860
		internal unsafe int GetDatData(byte datComponent)
		{
			int result = 0;
			switch (datComponent)
			{
			case 0:
				result = (int)this.m_opoDatCtx.m_pValCtx->m_year;
				break;
			case 1:
				result = (int)this.m_opoDatCtx.m_pValCtx->m_month;
				break;
			case 2:
				result = (int)this.m_opoDatCtx.m_pValCtx->m_day;
				break;
			case 3:
				result = (int)this.m_opoDatCtx.m_pValCtx->m_hour;
				break;
			case 4:
				result = (int)this.m_opoDatCtx.m_pValCtx->m_minute;
				break;
			case 5:
				result = (int)this.m_opoDatCtx.m_pValCtx->m_second;
				break;
			}
			return result;
		}

		// Token: 0x060006E3 RID: 1763 RVA: 0x00045904 File Offset: 0x00044904
		internal static int Compare(OpoDatCtx datCtx1, OpoDatCtx datCtx2)
		{
			int result = 0;
			int num = 0;
			try
			{
				num = OpsDat.Compare(datCtx1.m_pValCtx, datCtx2.m_pValCtx, ref result);
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
			return result;
		}

		// Token: 0x04000404 RID: 1028
		internal const byte MaxArrSize = 7;

		// Token: 0x04000405 RID: 1029
		internal const byte YEAR = 0;

		// Token: 0x04000406 RID: 1030
		internal const byte MONTH = 1;

		// Token: 0x04000407 RID: 1031
		internal const byte DAY = 2;

		// Token: 0x04000408 RID: 1032
		internal const byte HOUR = 3;

		// Token: 0x04000409 RID: 1033
		internal const byte MINUTE = 4;

		// Token: 0x0400040A RID: 1034
		internal const byte SECOND = 5;

		// Token: 0x0400040B RID: 1035
		public static readonly OracleDate MaxValue = new OracleDate(9999, 12, 31, 23, 59, 59);

		// Token: 0x0400040C RID: 1036
		public static readonly OracleDate MinValue = new OracleDate(-4712, 1, 1, 0, 0, 0);

		// Token: 0x0400040D RID: 1037
		public static readonly OracleDate Null;

		// Token: 0x0400040E RID: 1038
		private OpoDatCtx m_opoDatCtx;

		// Token: 0x0400040F RID: 1039
		private bool m_bNotNull;
	}
}
