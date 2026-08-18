using System;
using System.Runtime.InteropServices;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Oracle.DataAccess.Client;

namespace Oracle.DataAccess.Types
{
	// Token: 0x02000042 RID: 66
	[XmlSchemaProvider("GetXsdType")]
	public struct OracleTimeStamp : IComparable, INullable, IXmlSerializable
	{
		// Token: 0x060002B9 RID: 697 RVA: 0x00022860 File Offset: 0x00021860
		static OracleTimeStamp()
		{
			if (!OracleInit.bSetDllDirectoryInvoked)
			{
				OracleInit.Initialize();
			}
		}

		// Token: 0x060002BA RID: 698 RVA: 0x000228AC File Offset: 0x000218AC
		public static XmlQualifiedName GetXsdType(XmlSchemaSet schemaSet)
		{
			return new XmlQualifiedName("dateTime", "http://www.w3.org/2001/XMLSchema");
		}

		// Token: 0x060002BB RID: 699 RVA: 0x000228BD File Offset: 0x000218BD
		XmlSchema IXmlSerializable.GetSchema()
		{
			return null;
		}

		// Token: 0x060002BC RID: 700 RVA: 0x000228C0 File Offset: 0x000218C0
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			string text = reader.GetAttribute("null", "http://www.w3.org/2001/XMLSchema-instance");
			if (text != null && XmlConvert.ToBoolean(text))
			{
				this.m_bNotNull = false;
				return;
			}
			text = reader.ReadElementString();
			this.m_opoTSCtx = new OpoTSCtx(text, TimeStampType.TSType_TS);
			if (this.m_opoTSCtx.m_error != 0)
			{
				throw new ArgumentException(OracleTypeException.GetTypeMsg(this.m_opoTSCtx.m_error, new object[0]));
			}
			this.m_fSecondPrec = 9;
			this.m_bNotNull = true;
		}

		// Token: 0x060002BD RID: 701 RVA: 0x0002293D File Offset: 0x0002193D
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			if (this.m_bNotNull)
			{
				writer.WriteString(TimeStamp.ToTSString(this.m_opoTSCtx, this.m_fSecondPrec, TimeStampType.TSType_TS));
				return;
			}
			writer.WriteAttributeString("xsi", "null", "http://www.w3.org/2001/XMLSchema-instance", "true");
		}

		// Token: 0x060002BE RID: 702 RVA: 0x0002297C File Offset: 0x0002197C
		public OracleTimeStamp(int year, int month, int day, int hour, int minute, int second, int nanosecond)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleTimeStamp::OracleTimeStamp(1)\n"
				});
			}
			if (!TimeStamp.IsValidDateTime(year, month, day, hour, minute, second, nanosecond))
			{
				throw new ArgumentOutOfRangeException();
			}
			this.m_opoTSCtx = new OpoTSCtx(year, month, day, hour, minute, second, nanosecond, 0, 0, TimeStampType.TSType_TS);
			if (this.m_opoTSCtx.m_error != 0)
			{
				throw new ArgumentException(OracleTypeException.GetTypeMsg(this.m_opoTSCtx.m_error, new object[0]));
			}
			this.m_bNotNull = true;
			this.m_fSecondPrec = 9;
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleTimeStamp::OracleTimeStamp(1)\n"
				});
			}
		}

		// Token: 0x060002BF RID: 703 RVA: 0x00022A2C File Offset: 0x00021A2C
		public OracleTimeStamp(int year, int month, int day, int hour, int minute, int second, double millisecond)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleTimeStamp::OracleTimeStamp(2)\n"
				});
			}
			if (!TimeStamp.IsValidDateTime(year, month, day, hour, minute, second, (int)(millisecond * 1000000.0)))
			{
				throw new ArgumentOutOfRangeException();
			}
			this.m_opoTSCtx = new OpoTSCtx(year, month, day, hour, minute, second, millisecond, 0, 0, TimeStampType.TSType_TS);
			if (this.m_opoTSCtx.m_error != 0)
			{
				throw new ArgumentException(OracleTypeException.GetTypeMsg(this.m_opoTSCtx.m_error, new object[0]));
			}
			this.m_bNotNull = true;
			this.m_fSecondPrec = 9;
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleTimeStamp::OracleTimeStamp(2)\n"
				});
			}
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x00022AE7 File Offset: 0x00021AE7
		public OracleTimeStamp(int year, int month, int day)
		{
			this = new OracleTimeStamp(year, month, day, 0, 0, 0, 0);
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x00022AF6 File Offset: 0x00021AF6
		public OracleTimeStamp(int year, int month, int day, int hour, int minute, int second)
		{
			this = new OracleTimeStamp(year, month, day, hour, minute, second, 0);
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x00022B08 File Offset: 0x00021B08
		internal unsafe OracleTimeStamp(IntPtr ociDateTime)
		{
			OpoTSValCtx* pCtx = null;
			int num = OpsTS.AllocValCtxFromOCI(ociDateTime, out pCtx);
			if (num != 0)
			{
				throw new OracleException(num, string.Empty, string.Empty, string.Empty);
			}
			this.m_opoTSCtx = new OpoTSCtx(pCtx);
			this.m_bNotNull = true;
			this.m_fSecondPrec = 9;
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x00022B58 File Offset: 0x00021B58
		public OracleTimeStamp(DateTime data)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleTimeStamp::OracleTimeStamp(3)\n"
				});
			}
			this.m_opoTSCtx = new OpoTSCtx(data, 0, 0, TimeStampType.TSType_TS);
			if (this.m_opoTSCtx.m_error != 0)
			{
				throw new ArgumentException(OracleTypeException.GetTypeMsg(this.m_opoTSCtx.m_error, new object[0]));
			}
			this.m_bNotNull = true;
			this.m_fSecondPrec = 9;
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleTimeStamp::OracleTimeStamp(3)\n"
				});
			}
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x00022BE8 File Offset: 0x00021BE8
		public OracleTimeStamp(byte[] binData)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleTimeStamp::OracleTimeStamp(4)\n"
				});
			}
			if (binData == null)
			{
				throw new ArgumentNullException();
			}
			this.m_opoTSCtx = new OpoTSCtx(binData, TimeStampType.TSType_TS);
			if (this.m_opoTSCtx.m_error != 0)
			{
				throw new ArgumentException(OracleTypeException.GetTypeMsg(this.m_opoTSCtx.m_error, new object[0]));
			}
			this.m_bNotNull = true;
			this.m_fSecondPrec = 9;
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleTimeStamp::OracleTimeStamp(4)\n"
				});
			}
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x00022C80 File Offset: 0x00021C80
		public OracleTimeStamp(string tsStr)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleTimeStamp::OracleTimeStamp(5)\n"
				});
			}
			if (tsStr == null)
			{
				throw new ArgumentNullException("tsStr");
			}
			this.m_opoTSCtx = new OpoTSCtx(tsStr, TimeStampType.TSType_TS);
			if (this.m_opoTSCtx.m_error != 0)
			{
				throw new ArgumentException(OracleTypeException.GetTypeMsg(this.m_opoTSCtx.m_error, new object[0]));
			}
			this.m_bNotNull = true;
			this.m_fSecondPrec = 9;
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleTimeStamp::OracleTimeStamp(5)\n"
				});
			}
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x00022D1C File Offset: 0x00021D1C
		internal unsafe static IntPtr AllocValCtx(object methodParam)
		{
			IntPtr zero = IntPtr.Zero;
			bool flag = false;
			int num = 0;
			OpoTSValCtx* ptr = null;
			if (methodParam is char[])
			{
				num = OpsTS.AllocValCtxFromStr(new string((char[])methodParam), out ptr);
			}
			else if (methodParam is byte[])
			{
				num = OpsTS.AllocValCtxFromBytes((byte[])methodParam, out ptr, 9);
			}
			else if (methodParam is string)
			{
				num = OpsTS.AllocValCtxFromStr((string)methodParam, out ptr);
			}
			else if (methodParam is DateTime)
			{
				OracleTimeStamp oracleTimeStamp = new OracleTimeStamp((DateTime)methodParam);
				num = OpsTS.AllocOCIFromValCtx(OracleConnection.GetInternalConnection().m_opoConCtx.opsConCtx, oracleTimeStamp.GetValCtx(), out zero);
				if (num != 0)
				{
					throw new OracleException(num, string.Empty, string.Empty, string.Empty);
				}
				flag = true;
			}
			else if (methodParam is OracleString)
			{
				num = OpsTS.AllocValCtxFromStr(((OracleString)methodParam).Value, out ptr);
			}
			else if (methodParam is OracleDate)
			{
				num = OpsTS.AllocValCtxForFromDate(((OracleDate)methodParam).GetValCtx(), out ptr);
			}
			else if (methodParam is OracleTimeStamp)
			{
				num = OpsTS.AllocOCIFromValCtx(OracleConnection.GetInternalConnection().m_opoConCtx.opsConCtx, ((OracleTimeStamp)methodParam).GetValCtx(), out zero);
				if (num != 0)
				{
					throw new OracleException(num, string.Empty, string.Empty, string.Empty);
				}
				flag = true;
			}
			else if (methodParam is OracleTimeStampTZ)
			{
				num = OpsTS.AllocOCIFromValCtx(OracleConnection.GetInternalConnection().m_opoConCtx.opsConCtx, ((OracleTimeStampTZ)methodParam).ToOracleTimeStamp().GetValCtx(), out zero);
				if (num != 0)
				{
					throw new OracleException(num, string.Empty, string.Empty, string.Empty);
				}
				flag = true;
			}
			else if (methodParam is OracleTimeStampLTZ)
			{
				num = OpsTS.AllocOCIFromValCtx(OracleConnection.GetInternalConnection().m_opoConCtx.opsConCtx, ((OracleTimeStampLTZ)methodParam).ToOracleTimeStamp().GetValCtx(), out zero);
				if (num != 0)
				{
					throw new OracleException(num, string.Empty, string.Empty, string.Empty);
				}
				flag = true;
			}
			if (!flag)
			{
				if (num != 0)
				{
					throw new OracleException(num, string.Empty, string.Empty, string.Empty);
				}
				num = OpsTS.AllocOCIFromValCtx(OracleConnection.GetInternalConnection().m_opoConCtx.opsConCtx, ptr, out zero);
				if (num != 0)
				{
					throw new OracleException(num, string.Empty, string.Empty, string.Empty);
				}
				num = OpsTS.FreeValCtx(ptr);
				if (num != 0)
				{
					throw new OracleException(num, string.Empty, string.Empty, string.Empty);
				}
			}
			return zero;
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x00022F8C File Offset: 0x00021F8C
		public static bool Equals(OracleTimeStamp value1, OracleTimeStamp value2)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleTimeStamp::Equals(1)\n"
				});
			}
			CompareNullEnum compareNullEnum = InternalTypes.CompareNull(!value1.m_bNotNull, !value2.m_bNotNull);
			if (compareNullEnum == CompareNullEnum.BothNull)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleTimeStamp::Equals(1)\n"
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
						" (EXIT)  OracleTimeStamp::Equals(1)\n"
					});
				}
				return false;
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleTimeStamp::Equals(1)\n"
				});
			}
			return TimeStamp.Compare(value1.m_opoTSCtx, value2.m_opoTSCtx) == 0;
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x00023050 File Offset: 0x00022050
		public static bool GreaterThan(OracleTimeStamp value1, OracleTimeStamp value2)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleTimeStamp::GreaterThan()\n"
				});
			}
			CompareNullEnum compareNullEnum = InternalTypes.CompareNull(!value1.m_bNotNull, !value2.m_bNotNull);
			if (compareNullEnum == CompareNullEnum.BothNull)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleTimeStamp::GreaterThan()\n"
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
						" (EXIT)  OracleTimeStamp::GreaterThan()\n"
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
						" (EXIT)  OracleTimeStamp::GreaterThan()\n"
					});
				}
				return true;
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleTimeStamp::GreaterThan()\n"
				});
			}
			return TimeStamp.Compare(value1.m_opoTSCtx, value2.m_opoTSCtx) > 0;
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x0002313C File Offset: 0x0002213C
		public static bool GreaterThanOrEqual(OracleTimeStamp value1, OracleTimeStamp value2)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleTimeStamp::GreaterThanOrEqual()\n"
				});
			}
			CompareNullEnum compareNullEnum = InternalTypes.CompareNull(!value1.m_bNotNull, !value2.m_bNotNull);
			if (compareNullEnum == CompareNullEnum.BothNull)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleTimeStamp::GreaterThanOrEqual()\n"
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
						" (EXIT)  OracleTimeStamp::GreaterThanOrEqual()\n"
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
						" (EXIT)  OracleTimeStamp::GreaterThanOrEqual()\n"
					});
				}
				return true;
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleTimeStamp::GreaterThanOrEqual()\n"
				});
			}
			return TimeStamp.Compare(value1.m_opoTSCtx, value2.m_opoTSCtx) >= 0;
		}

		// Token: 0x060002CA RID: 714 RVA: 0x00023228 File Offset: 0x00022228
		public static bool LessThan(OracleTimeStamp value1, OracleTimeStamp value2)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleTimeStamp::LessThan()\n"
				});
			}
			CompareNullEnum compareNullEnum = InternalTypes.CompareNull(!value1.m_bNotNull, !value2.m_bNotNull);
			if (compareNullEnum == CompareNullEnum.BothNull)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleTimeStamp::LessThan()\n"
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
						" (EXIT)  OracleTimeStamp::LessThan()\n"
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
						" (EXIT)  OracleTimeStamp::LessThan()\n"
					});
				}
				return false;
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleTimeStamp::LessThan()\n"
				});
			}
			return TimeStamp.Compare(value1.m_opoTSCtx, value2.m_opoTSCtx) < 0;
		}

		// Token: 0x060002CB RID: 715 RVA: 0x00023314 File Offset: 0x00022314
		public static bool LessThanOrEqual(OracleTimeStamp value1, OracleTimeStamp value2)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleTimeStamp::LessThanOrEqual()\n"
				});
			}
			CompareNullEnum compareNullEnum = InternalTypes.CompareNull(!value1.m_bNotNull, !value2.m_bNotNull);
			if (compareNullEnum == CompareNullEnum.BothNull)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleTimeStamp::LessThanOrEqual()\n"
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
						" (EXIT)  OracleTimeStamp::LessThanOrEqual()\n"
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
						" (EXIT)  OracleTimeStamp::LessThanOrEqual()\n"
					});
				}
				return false;
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleTimeStamp::LessThanOrEqual()\n"
				});
			}
			return TimeStamp.Compare(value1.m_opoTSCtx, value2.m_opoTSCtx) <= 0;
		}

		// Token: 0x060002CC RID: 716 RVA: 0x000233FD File Offset: 0x000223FD
		public static bool NotEquals(OracleTimeStamp value1, OracleTimeStamp value2)
		{
			return !OracleTimeStamp.Equals(value1, value2);
		}

		// Token: 0x060002CD RID: 717 RVA: 0x0002340C File Offset: 0x0002240C
		public unsafe static OracleTimeStamp GetSysDate()
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleTimeStamp::GetSysDate()\n"
				});
			}
			int num = 0;
			OpoTSValCtx* ptr = null;
			try
			{
				num = OpsTS.AllocValCtxForSysDate(out ptr);
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
							" (EXIT)  OracleTimeStamp::GetSysDate()\n"
						});
					}
					if (ptr != null)
					{
						try
						{
							OpsTS.FreeValCtx(ptr);
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
					" (EXIT)  OracleTimeStamp::GetSysDate()\n"
				});
			}
			return new OracleTimeStamp(ptr);
		}

		// Token: 0x060002CE RID: 718 RVA: 0x00023508 File Offset: 0x00022508
		public static OracleTimeStamp Parse(string tsStr)
		{
			if (tsStr == null)
			{
				throw new ArgumentNullException();
			}
			return new OracleTimeStamp(tsStr);
		}

		// Token: 0x060002CF RID: 719 RVA: 0x0002351C File Offset: 0x0002251C
		public unsafe static OracleTimeStamp SetPrecision(OracleTimeStamp value1, int fracSecPrecision)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleTimeStamp::SetPrecision()\n"
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
				num = OpsTS.AllocValCtxFromBytes(value1.BinData, out ptr, fracSecPrecision);
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
							" (EXIT)  OracleTimeStamp::SetPrecision()\n"
						});
					}
					if (ptr != null)
					{
						try
						{
							OpsTS.FreeValCtx(ptr);
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
					" (EXIT)  OracleTimeStamp::SetPrecision()\n"
				});
			}
			return new OracleTimeStamp(ptr, fracSecPrecision);
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x00023644 File Offset: 0x00022644
		public static bool operator ==(OracleTimeStamp value1, OracleTimeStamp value2)
		{
			return OracleTimeStamp.Equals(value1, value2);
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x0002364D File Offset: 0x0002264D
		public static bool operator >(OracleTimeStamp value1, OracleTimeStamp value2)
		{
			return OracleTimeStamp.GreaterThan(value1, value2);
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x00023656 File Offset: 0x00022656
		public static bool operator >=(OracleTimeStamp value1, OracleTimeStamp value2)
		{
			return OracleTimeStamp.GreaterThanOrEqual(value1, value2);
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x0002365F File Offset: 0x0002265F
		public static bool operator <(OracleTimeStamp value1, OracleTimeStamp value2)
		{
			return OracleTimeStamp.LessThan(value1, value2);
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x00023668 File Offset: 0x00022668
		public static bool operator <=(OracleTimeStamp value1, OracleTimeStamp value2)
		{
			return OracleTimeStamp.LessThanOrEqual(value1, value2);
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x00023671 File Offset: 0x00022671
		public static bool operator !=(OracleTimeStamp value1, OracleTimeStamp value2)
		{
			return OracleTimeStamp.NotEquals(value1, value2);
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x0002367C File Offset: 0x0002267C
		public unsafe static OracleTimeStamp operator +(OracleTimeStamp value1, OracleIntervalDS value2)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleTimeStamp::operator +(1)\n"
				});
			}
			if (value1.IsNull || value2.IsNull)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleTimeStamp::operator +(1)\n"
					});
				}
				return OracleTimeStamp.Null;
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
							" (EXIT)  OracleTimeStamp::operator +(1)\n"
						});
					}
					if (ptr != null)
					{
						try
						{
							OpsTS.FreeValCtx(ptr);
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
					" (EXIT)  OracleTimeStamp::operator +(1)\n"
				});
			}
			return new OracleTimeStamp(ptr);
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x000237C4 File Offset: 0x000227C4
		public unsafe static OracleTimeStamp operator +(OracleTimeStamp value1, OracleIntervalYM value2)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleTimeStamp::operator +(2)\n"
				});
			}
			if (value1.IsNull || value2.IsNull)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleTimeStamp::operator +(2)\n"
					});
				}
				return OracleTimeStamp.Null;
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
					" (EXIT)  OracleTimeStamp::operator +(2)\n"
				});
			}
			return new OracleTimeStamp(ctx);
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x000238A8 File Offset: 0x000228A8
		public unsafe static OracleTimeStamp operator +(OracleTimeStamp value1, TimeSpan value2)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleTimeStamp::operator +(3)\n"
				});
			}
			if (value1.IsNull)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleTimeStamp::operator +(3)\n"
					});
				}
				return OracleTimeStamp.Null;
			}
			int num = 0;
			OpoITLValCtx opoITLValCtx = default(OpoITLValCtx);
			GCHandle gchandle = GCHandle.Alloc(opoITLValCtx, GCHandleType.Pinned);
			OracleIntervalDS.FillValCtxFromTimeSpan(&opoITLValCtx, value2);
			OpoTSValCtx* pCtx;
			try
			{
				num = OpsTSA.AllocValCtxForAddInterval(value1.m_opoTSCtx.m_pValCtx, &opoITLValCtx, out pCtx);
			}
			catch (Exception ex)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex);
				}
				throw;
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
					" (EXIT)  OracleTimeStamp::operator +(3)\n"
				});
			}
			return new OracleTimeStamp(ctx);
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x000239BC File Offset: 0x000229BC
		public unsafe static OracleTimeStamp operator -(OracleTimeStamp value1, OracleIntervalDS value2)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleTimeStamp::operator -(1)\n"
				});
			}
			if (value1.IsNull || value2.IsNull)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleTimeStamp::operator -(1)\n"
					});
				}
				return OracleTimeStamp.Null;
			}
			int num = 0;
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
			OpoTSCtx ctx = new OpoTSCtx(pCtx);
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleTimeStamp::operator -(1)\n"
				});
			}
			return new OracleTimeStamp(ctx);
		}

		// Token: 0x060002DA RID: 730 RVA: 0x00023AA0 File Offset: 0x00022AA0
		public unsafe static OracleTimeStamp operator -(OracleTimeStamp value1, OracleIntervalYM value2)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleTimeStamp::operator -(2)\n"
				});
			}
			if (value1.IsNull || value2.IsNull)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleTimeStamp::operator -(2)\n"
					});
				}
				return OracleTimeStamp.Null;
			}
			int num = 0;
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
			OpoTSCtx ctx = new OpoTSCtx(pCtx);
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleTimeStamp::operator -(2)\n"
				});
			}
			return new OracleTimeStamp(ctx);
		}

		// Token: 0x060002DB RID: 731 RVA: 0x00023B84 File Offset: 0x00022B84
		public unsafe static OracleTimeStamp operator -(OracleTimeStamp value1, TimeSpan value2)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleTimeStamp::operator -(3)\n"
				});
			}
			if (value1.IsNull)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleTimeStamp::operator -(3)\n"
					});
				}
				return OracleTimeStamp.Null;
			}
			int num = 0;
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
			OpoTSCtx ctx = new OpoTSCtx(pCtx);
			if (num != 0)
			{
				throw new OracleTypeException(num, new object[0]);
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleTimeStamp::operator -(3)\n"
				});
			}
			return new OracleTimeStamp(ctx);
		}

		// Token: 0x060002DC RID: 732 RVA: 0x00023C78 File Offset: 0x00022C78
		public static explicit operator OracleTimeStamp(string tsStr)
		{
			return new OracleTimeStamp(tsStr);
		}

		// Token: 0x060002DD RID: 733 RVA: 0x00023C80 File Offset: 0x00022C80
		public static explicit operator DateTime(OracleTimeStamp value1)
		{
			if (value1.m_bNotNull)
			{
				return DateTimeConv.GetDateTime(value1.m_opoTSCtx.m_pValCtx, OracleDbType.TimeStamp, false);
			}
			throw new OracleNullValueException();
		}

		// Token: 0x060002DE RID: 734 RVA: 0x00023CA8 File Offset: 0x00022CA8
		public unsafe static implicit operator OracleTimeStamp(OracleDate value1)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleTimeStamp::operator OracleTimeStamp(1)\n"
				});
			}
			int num = 0;
			if (value1.IsNull)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleTimeStamp::operator OracleTimeStamp(1)\n"
					});
				}
				return OracleTimeStamp.Null;
			}
			OpoTSValCtx* pCtx;
			try
			{
				num = OpsTS.AllocValCtxForFromDate(value1.GetValCtx(), out pCtx);
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
					" (EXIT)  OracleTimeStamp::operator OracleTimeStamp(1)\n"
				});
			}
			return new OracleTimeStamp(ctx);
		}

		// Token: 0x060002DF RID: 735 RVA: 0x00023D74 File Offset: 0x00022D74
		public static explicit operator OracleTimeStamp(OracleTimeStampLTZ value1)
		{
			if (!value1.IsNull)
			{
				return OracleTimeStampLTZ.ToTS(value1);
			}
			return OracleTimeStamp.Null;
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x00023D8C File Offset: 0x00022D8C
		public static explicit operator OracleTimeStamp(OracleTimeStampTZ value1)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleTimeStamp::operator OracleTimeStamp(2)\n"
				});
			}
			if (!value1.IsNull)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleTimeStamp::operator OracleTimeStamp(2)\n"
					});
				}
				return OracleTimeStampTZ.ToTS(value1);
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleTimeStamp::operator OracleTimeStamp(2)\n"
				});
			}
			return OracleTimeStamp.Null;
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x00023E05 File Offset: 0x00022E05
		public static implicit operator OracleTimeStamp(DateTime value1)
		{
			return new OracleTimeStamp(value1);
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060002E2 RID: 738 RVA: 0x00023E10 File Offset: 0x00022E10
		public unsafe byte[] BinData
		{
			get
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (ENTRY) OracleTimeStamp::BinData: get\n"
					});
				}
				if (!this.m_bNotNull)
				{
					throw new OracleNullValueException();
				}
				byte[] array = new byte[11];
				int num = 0;
				int num2 = 0;
				try
				{
					num2 = OpsTS.ToBytes(this.m_opoTSCtx.m_pValCtx, array, &num);
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
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleTimeStamp::BinData: get\n"
					});
				}
				return array;
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060002E3 RID: 739 RVA: 0x00023EC0 File Offset: 0x00022EC0
		public bool IsNull
		{
			get
			{
				return !this.m_bNotNull;
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060002E4 RID: 740 RVA: 0x00023ECB File Offset: 0x00022ECB
		public DateTime Value
		{
			get
			{
				if (this.m_bNotNull)
				{
					return DateTimeConv.GetDateTime(this.m_opoTSCtx.m_pValCtx, OracleDbType.TimeStamp, false);
				}
				throw new OracleNullValueException();
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060002E5 RID: 741 RVA: 0x00023EEE File Offset: 0x00022EEE
		public int Year
		{
			get
			{
				if (this.m_bNotNull)
				{
					return this.GetTSData(0);
				}
				throw new OracleNullValueException();
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060002E6 RID: 742 RVA: 0x00023F05 File Offset: 0x00022F05
		public int Month
		{
			get
			{
				if (this.m_bNotNull)
				{
					return this.GetTSData(1);
				}
				throw new OracleNullValueException();
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060002E7 RID: 743 RVA: 0x00023F1C File Offset: 0x00022F1C
		public int Day
		{
			get
			{
				if (this.m_bNotNull)
				{
					return this.GetTSData(2);
				}
				throw new OracleNullValueException();
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060002E8 RID: 744 RVA: 0x00023F33 File Offset: 0x00022F33
		public int Hour
		{
			get
			{
				if (this.m_bNotNull)
				{
					return this.GetTSData(3);
				}
				throw new OracleNullValueException();
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060002E9 RID: 745 RVA: 0x00023F4A File Offset: 0x00022F4A
		public int Minute
		{
			get
			{
				if (this.m_bNotNull)
				{
					return this.GetTSData(4);
				}
				throw new OracleNullValueException();
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060002EA RID: 746 RVA: 0x00023F61 File Offset: 0x00022F61
		public int Second
		{
			get
			{
				if (this.m_bNotNull)
				{
					return this.GetTSData(5);
				}
				throw new OracleNullValueException();
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060002EB RID: 747 RVA: 0x00023F78 File Offset: 0x00022F78
		public double Millisecond
		{
			get
			{
				if (this.m_bNotNull)
				{
					return (double)this.GetTSData(7) / 1000000.0;
				}
				throw new OracleNullValueException();
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060002EC RID: 748 RVA: 0x00023F9B File Offset: 0x00022F9B
		public int Nanosecond
		{
			get
			{
				if (this.m_bNotNull)
				{
					return this.GetTSData(7);
				}
				throw new OracleNullValueException();
			}
		}

		// Token: 0x060002ED RID: 749 RVA: 0x00023FB4 File Offset: 0x00022FB4
		public unsafe OracleTimeStamp AddYears(int years)
		{
			int num = 0;
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleTimeStamp::AddYears()\n"
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
					" (EXIT)  OracleTimeStamp::AddYears()\n"
				});
			}
			return new OracleTimeStamp(ctx);
		}

		// Token: 0x060002EE RID: 750 RVA: 0x0002407C File Offset: 0x0002307C
		public unsafe OracleTimeStamp AddMonths(long months)
		{
			int num = 0;
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleTimeStamp::AddMonths()\n"
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
					" (EXIT)  OracleTimeStamp::AddMonths()\n"
				});
			}
			return new OracleTimeStamp(ctx);
		}

		// Token: 0x060002EF RID: 751 RVA: 0x0002414C File Offset: 0x0002314C
		public unsafe OracleTimeStamp AddDays(double days)
		{
			int num = 0;
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleTimeStamp::AddDays()\n"
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
					" (EXIT)  OracleTimeStamp::AddDays()\n"
				});
			}
			return new OracleTimeStamp(ctx);
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x0002421C File Offset: 0x0002321C
		public unsafe OracleTimeStamp AddHours(double hours)
		{
			int num = 0;
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleTimeStamp::AddHours()\n"
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
					" (EXIT)  OracleTimeStamp::AddHours()\n"
				});
			}
			return new OracleTimeStamp(ctx);
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x000242EC File Offset: 0x000232EC
		public unsafe OracleTimeStamp AddMinutes(double minutes)
		{
			int num = 0;
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleTimeStamp::AddMinutes()\n"
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
					" (EXIT)  OracleTimeStamp::AddMinutes()\n"
				});
			}
			return new OracleTimeStamp(ctx);
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x000243BC File Offset: 0x000233BC
		public unsafe OracleTimeStamp AddSeconds(double seconds)
		{
			int num = 0;
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleTimeStamp::AddSeconds()\n"
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
					" (EXIT)  OracleTimeStamp::AddSeconds()\n"
				});
			}
			return new OracleTimeStamp(ctx);
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x0002448C File Offset: 0x0002348C
		public unsafe OracleTimeStamp AddMilliseconds(double milliseconds)
		{
			int num = 0;
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleTimeStamp::AddMilliseconds()\n"
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
					" (EXIT)  OracleTimeStamp::AddMilliseconds()\n"
				});
			}
			return new OracleTimeStamp(ctx);
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x0002455C File Offset: 0x0002355C
		public unsafe OracleTimeStamp AddNanoseconds(long nanoseconds)
		{
			int num = 0;
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleTimeStamp::AddNanoseconds()\n"
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
					" (EXIT)  OracleTimeStamp::AddNanoseconds()\n"
				});
			}
			return new OracleTimeStamp(ctx);
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x0002460C File Offset: 0x0002360C
		public int CompareTo(object obj)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleTimeStamp::CompareTo()\n"
				});
			}
			if (obj.GetType() != typeof(OracleTimeStamp))
			{
				throw new ArgumentException();
			}
			OracleTimeStamp oracleTimeStamp = (OracleTimeStamp)obj;
			CompareNullEnum compareNullEnum = InternalTypes.CompareNull(!this.m_bNotNull, !oracleTimeStamp.m_bNotNull);
			if (compareNullEnum == CompareNullEnum.BothNull)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleTimeStamp::CompareTo()\n"
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
						" (EXIT)  OracleTimeStamp::CompareTo()\n"
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
						" (EXIT)  OracleTimeStamp::CompareTo()\n"
					});
				}
				return 1;
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleTimeStamp::CompareTo()\n"
				});
			}
			return TimeStamp.Compare(this.m_opoTSCtx, oracleTimeStamp.m_opoTSCtx);
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x00024714 File Offset: 0x00023714
		public override bool Equals(object obj)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleTimeStamp::Equals(2)\n"
				});
			}
			if (obj == null)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (ENTRY) OracleTimeStamp::Equals(2)\n"
					});
				}
				return false;
			}
			if (obj.GetType() != typeof(OracleTimeStamp))
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (ENTRY) OracleTimeStamp::Equals(2)\n"
					});
				}
				return false;
			}
			OracleTimeStamp value = (OracleTimeStamp)obj;
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleTimeStamp::Equals(2)\n"
				});
			}
			return OracleTimeStamp.Equals(this, value);
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x000247C9 File Offset: 0x000237C9
		public override int GetHashCode()
		{
			if (this.m_bNotNull)
			{
				return this.m_opoTSCtx.GetHashCode();
			}
			return 0;
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x000247E0 File Offset: 0x000237E0
		public unsafe OracleIntervalDS GetDaysBetween(OracleTimeStamp value1)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleTimeStamp::GetDaysBetween()\n"
				});
			}
			if (!this.m_bNotNull || !value1.m_bNotNull)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleTimeStamp::GetDaysBetween()\n"
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
					" (EXIT)  OracleTimeStamp::GetDaysBetween()\n"
				});
			}
			return new OracleIntervalDS(ctx2);
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x000248C4 File Offset: 0x000238C4
		public unsafe OracleIntervalYM GetYearsBetween(OracleTimeStamp value1)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleTimeStamp::GetYearsBetween()\n"
				});
			}
			if (!this.m_bNotNull || !value1.m_bNotNull)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleTimeStamp::GetYearsBetween()\n"
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
					" (EXIT)  OracleTimeStamp::GetYearsBetween()\n"
				});
			}
			return new OracleIntervalYM(ctx2);
		}

		// Token: 0x060002FA RID: 762 RVA: 0x000249A8 File Offset: 0x000239A8
		public unsafe OracleDate ToOracleDate()
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleTimeStamp::ToOracleDate()\n"
				});
			}
			if (!this.m_bNotNull)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleTimeStamp::ToOracleDate()\n"
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
					" (EXIT)  OracleTimeStamp::ToOracleDate()\n"
				});
			}
			return new OracleDate(ctx);
		}

		// Token: 0x060002FB RID: 763 RVA: 0x00024A78 File Offset: 0x00023A78
		public OracleTimeStampLTZ ToOracleTimeStampLTZ()
		{
			if (this.m_bNotNull)
			{
				return OracleTimeStamp.ToTSL(this);
			}
			return OracleTimeStampLTZ.Null;
		}

		// Token: 0x060002FC RID: 764 RVA: 0x00024A93 File Offset: 0x00023A93
		public OracleTimeStampTZ ToOracleTimeStampTZ()
		{
			if (this.m_bNotNull)
			{
				return OracleTimeStamp.ToTSZ(this);
			}
			return OracleTimeStampTZ.Null;
		}

		// Token: 0x060002FD RID: 765 RVA: 0x00024AB0 File Offset: 0x00023AB0
		public override string ToString()
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleTimeStamp::ToString()\n"
				});
			}
			if (this.m_bNotNull)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleTimeStamp::ToString()\n"
					});
				}
				return TimeStamp.ToTSString(this.m_opoTSCtx, this.m_fSecondPrec, TimeStampType.TSType_TS);
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleTimeStamp::ToString()\n"
				});
			}
			return "null";
		}

		// Token: 0x060002FE RID: 766 RVA: 0x00024B34 File Offset: 0x00023B34
		internal unsafe OracleTimeStamp(OpoTSValCtx* pCtx)
		{
			this = new OracleTimeStamp(pCtx, 9);
		}

		// Token: 0x060002FF RID: 767 RVA: 0x00024B3F File Offset: 0x00023B3F
		internal unsafe OracleTimeStamp(OpoTSValCtx* pCtx, int fSecondPrec)
		{
			this.m_opoTSCtx = new OpoTSCtx(pCtx);
			this.m_bNotNull = true;
			this.m_fSecondPrec = fSecondPrec;
		}

		// Token: 0x06000300 RID: 768 RVA: 0x00024B5B File Offset: 0x00023B5B
		internal OracleTimeStamp(OpoTSCtx ctx)
		{
			this.m_opoTSCtx = ctx;
			this.m_bNotNull = true;
			this.m_fSecondPrec = 9;
		}

		// Token: 0x06000301 RID: 769 RVA: 0x00024B74 File Offset: 0x00023B74
		internal unsafe static OracleTimeStampTZ ToTSZ(OracleTimeStamp value1)
		{
			if (!value1.m_bNotNull)
			{
				return OracleTimeStampTZ.Null;
			}
			int num = 0;
			OpoTSValCtx* pCtx;
			try
			{
				num = OpsTSA.AllocValCtxForToTSZ(value1.m_opoTSCtx.m_pValCtx, out pCtx);
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
			return new OracleTimeStampTZ(ctx);
		}

		// Token: 0x06000302 RID: 770 RVA: 0x00024BE8 File Offset: 0x00023BE8
		internal unsafe static OracleTimeStampLTZ ToTSL(OracleTimeStamp value1)
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

		// Token: 0x06000303 RID: 771 RVA: 0x00024C5C File Offset: 0x00023C5C
		internal unsafe OpoTSValCtx* GetValCtx()
		{
			return this.m_opoTSCtx.m_pValCtx;
		}

		// Token: 0x06000304 RID: 772 RVA: 0x00024C6C File Offset: 0x00023C6C
		internal IntPtr DupValCtx()
		{
			IntPtr zero = IntPtr.Zero;
			int num = OpsTSA.DupValCtx(this.GetValCtx(), out zero, TimeStampType.TSType_TS);
			if (num != 0)
			{
				throw new OracleException(num, string.Empty, string.Empty, string.Empty);
			}
			return zero;
		}

		// Token: 0x06000305 RID: 773 RVA: 0x00024CAA File Offset: 0x00023CAA
		internal int GetTSData(byte tsComponent)
		{
			return TimeStamp.GetTSData(this.m_opoTSCtx.m_pValCtx, tsComponent);
		}

		// Token: 0x04000228 RID: 552
		internal const byte MaxArrSize = 11;

		// Token: 0x04000229 RID: 553
		public static readonly OracleTimeStamp MaxValue = new OracleTimeStamp(9999, 12, 31, 23, 59, 59, 999999999);

		// Token: 0x0400022A RID: 554
		public static readonly OracleTimeStamp MinValue = new OracleTimeStamp(-4712, 1, 1, 0, 0, 0, 0);

		// Token: 0x0400022B RID: 555
		public static readonly OracleTimeStamp Null;

		// Token: 0x0400022C RID: 556
		private OpoTSCtx m_opoTSCtx;

		// Token: 0x0400022D RID: 557
		private bool m_bNotNull;

		// Token: 0x0400022E RID: 558
		private int m_fSecondPrec;
	}
}
