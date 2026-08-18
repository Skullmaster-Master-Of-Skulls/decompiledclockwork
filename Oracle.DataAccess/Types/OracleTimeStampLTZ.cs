using System;
using System.Runtime.InteropServices;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Oracle.DataAccess.Client;

namespace Oracle.DataAccess.Types
{
	// Token: 0x02000118 RID: 280
	[XmlSchemaProvider("GetXsdType")]
	public struct OracleTimeStampLTZ : IComparable, INullable, IXmlSerializable
	{
		// Token: 0x06000AF8 RID: 2808 RVA: 0x000705CC File Offset: 0x0006F5CC
		static OracleTimeStampLTZ()
		{
			if (!OracleInit.bSetDllDirectoryInvoked)
			{
				OracleInit.Initialize();
			}
		}

		// Token: 0x06000AF9 RID: 2809 RVA: 0x00070618 File Offset: 0x0006F618
		public static XmlQualifiedName GetXsdType(XmlSchemaSet schemaSet)
		{
			return new XmlQualifiedName("dateTime", "http://www.w3.org/2001/XMLSchema");
		}

		// Token: 0x06000AFA RID: 2810 RVA: 0x00070629 File Offset: 0x0006F629
		XmlSchema IXmlSerializable.GetSchema()
		{
			return null;
		}

		// Token: 0x06000AFB RID: 2811 RVA: 0x0007062C File Offset: 0x0006F62C
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			string text = reader.GetAttribute("null", "http://www.w3.org/2001/XMLSchema-instance");
			if (text != null && XmlConvert.ToBoolean(text))
			{
				this.m_bNotNull = false;
				return;
			}
			text = reader.ReadElementString();
			this.m_opoTSCtx = new OpoTSCtx(text, TimeStampType.TSType_TSL);
			if (this.m_opoTSCtx.m_error != 0)
			{
				throw new ArgumentException(OracleTypeException.GetTypeMsg(this.m_opoTSCtx.m_error, new object[0]));
			}
			this.m_fSecondPrec = 9;
			this.m_bNotNull = true;
		}

		// Token: 0x06000AFC RID: 2812 RVA: 0x000706A9 File Offset: 0x0006F6A9
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			if (this.m_bNotNull)
			{
				writer.WriteString(TimeStamp.ToTSString(this.m_opoTSCtx, this.m_fSecondPrec, TimeStampType.TSType_TSL));
				return;
			}
			writer.WriteAttributeString("xsi", "null", "http://www.w3.org/2001/XMLSchema-instance", "true");
		}

		// Token: 0x06000AFD RID: 2813 RVA: 0x000706E8 File Offset: 0x0006F6E8
		public OracleTimeStampLTZ(int year, int month, int day, int hour, int minute, int second, int nanosecond)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleTimeStampLTZ::OracleTimeStampLTZ(1)\n"
				});
			}
			if (!TimeStamp.IsValidDateTime(year, month, day, hour, minute, second, nanosecond))
			{
				throw new ArgumentOutOfRangeException();
			}
			this.m_opoTSCtx = new OpoTSCtx(year, month, day, hour, minute, second, nanosecond, TimeStamp.LocalTZOffset.m_tzHours, TimeStamp.LocalTZOffset.m_tzMinutes, TimeStampType.TSType_TSL);
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
					" (EXIT)  OracleTimeStampLTZ::OracleTimeStampLTZ(1)\n"
				});
			}
		}

		// Token: 0x06000AFE RID: 2814 RVA: 0x000707AC File Offset: 0x0006F7AC
		public OracleTimeStampLTZ(int year, int month, int day, int hour, int minute, int second, double millisecond)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleTimeStampLTZ::OracleTimeStampLTZ(2)\n"
				});
			}
			if (!TimeStamp.IsValidDateTime(year, month, day, hour, minute, second, (int)(millisecond * 1000000.0)))
			{
				throw new ArgumentOutOfRangeException();
			}
			this.m_opoTSCtx = new OpoTSCtx(year, month, day, hour, minute, second, millisecond, TimeStamp.LocalTZOffset.m_tzHours, TimeStamp.LocalTZOffset.m_tzMinutes, TimeStampType.TSType_TSL);
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
					" (EXIT)  OracleTimeStampLTZ::OracleTimeStampLTZ(2)\n"
				});
			}
		}

		// Token: 0x06000AFF RID: 2815 RVA: 0x0007087C File Offset: 0x0006F87C
		internal unsafe OracleTimeStampLTZ(IntPtr ociDateTime)
		{
			OpoTSValCtx* pCtx = null;
			int num = OpsTSL.AllocValCtxFromOCI(ociDateTime, out pCtx);
			if (num != 0)
			{
				throw new OracleException(num, string.Empty, string.Empty, string.Empty);
			}
			this.m_opoTSCtx = new OpoTSCtx(pCtx);
			this.m_bNotNull = true;
			this.m_fSecondPrec = 9;
		}

		// Token: 0x06000B00 RID: 2816 RVA: 0x000708CB File Offset: 0x0006F8CB
		public OracleTimeStampLTZ(int year, int month, int day)
		{
			this = new OracleTimeStampLTZ(year, month, day, 0, 0, 0, 0);
		}

		// Token: 0x06000B01 RID: 2817 RVA: 0x000708DA File Offset: 0x0006F8DA
		public OracleTimeStampLTZ(int year, int month, int day, int hour, int minute, int second)
		{
			this = new OracleTimeStampLTZ(year, month, day, hour, minute, second, 0);
		}

		// Token: 0x06000B02 RID: 2818 RVA: 0x000708EC File Offset: 0x0006F8EC
		public OracleTimeStampLTZ(DateTime data)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleTimeStampLTZ::OracleTimeStampLTZ(3)\n"
				});
			}
			this.m_opoTSCtx = new OpoTSCtx(data, TimeStamp.LocalTZOffset.m_tzHours, TimeStamp.LocalTZOffset.m_tzMinutes, TimeStampType.TSType_TSL);
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
					" (EXIT)  OracleTimeStampLTZ::OracleTimeStampLTZ(3)\n"
				});
			}
		}

		// Token: 0x06000B03 RID: 2819 RVA: 0x0007098C File Offset: 0x0006F98C
		public OracleTimeStampLTZ(byte[] binData)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleTimeStampLTZ::OracleTimeStampLTZ(4)\n"
				});
			}
			if (binData == null)
			{
				throw new ArgumentNullException();
			}
			this.m_opoTSCtx = new OpoTSCtx(binData, TimeStampType.TSType_TSL);
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
					" (EXIT)  OracleTimeStampLTZ::OracleTimeStampLTZ(4)\n"
				});
			}
		}

		// Token: 0x06000B04 RID: 2820 RVA: 0x00070A24 File Offset: 0x0006FA24
		public OracleTimeStampLTZ(string tsStr)
		{
			if (tsStr == null)
			{
				throw new ArgumentNullException();
			}
			this.m_opoTSCtx = new OpoTSCtx(tsStr, TimeStampType.TSType_TSL);
			if (this.m_opoTSCtx.m_error != 0)
			{
				throw new ArgumentException(OracleTypeException.GetTypeMsg(this.m_opoTSCtx.m_error, new object[0]));
			}
			this.m_bNotNull = true;
			this.m_fSecondPrec = 9;
		}

		// Token: 0x06000B05 RID: 2821 RVA: 0x00070A80 File Offset: 0x0006FA80
		internal unsafe static IntPtr AllocValCtx(object methodParam)
		{
			IntPtr zero = IntPtr.Zero;
			if (methodParam is char[])
			{
				OracleTimeStampLTZ oracleTimeStampLTZ = new OracleTimeStampLTZ(new string((char[])methodParam));
				int num = OpsTSL.AllocOCIFromValCtx(OracleConnection.GetInternalConnection().m_opoConCtx.opsConCtx, oracleTimeStampLTZ.GetValCtx(), out zero);
				if (num != 0)
				{
					throw new OracleException(num, string.Empty, string.Empty, string.Empty);
				}
			}
			else if (methodParam is byte[])
			{
				OpoTSValCtx* ptr;
				int num = OpsTSL.AllocValCtxFromBytes((byte[])methodParam, out ptr, 9);
				if (num != 0)
				{
					throw new OracleException(num, string.Empty, string.Empty, string.Empty);
				}
				num = OpsTSL.AllocOCIFromValCtx(OracleConnection.GetInternalConnection().m_opoConCtx.opsConCtx, ptr, out zero);
				if (num != 0)
				{
					throw new OracleException(num, string.Empty, string.Empty, string.Empty);
				}
				num = OpsTSL.FreeValCtx(ptr);
				if (num != 0)
				{
					throw new OracleException(num, string.Empty, string.Empty, string.Empty);
				}
			}
			else if (methodParam is string)
			{
				OracleTimeStampLTZ oracleTimeStampLTZ2 = new OracleTimeStampLTZ((string)methodParam);
				int num = OpsTSL.AllocOCIFromValCtx(OracleConnection.GetInternalConnection().m_opoConCtx.opsConCtx, oracleTimeStampLTZ2.GetValCtx(), out zero);
				if (num != 0)
				{
					throw new OracleException(num, string.Empty, string.Empty, string.Empty);
				}
			}
			else if (methodParam is DateTime)
			{
				OracleTimeStampLTZ oracleTimeStampLTZ3 = new OracleTimeStampLTZ((DateTime)methodParam);
				int num = OpsTSL.AllocOCIFromValCtx(OracleConnection.GetInternalConnection().m_opoConCtx.opsConCtx, oracleTimeStampLTZ3.GetValCtx(), out zero);
				if (num != 0)
				{
					throw new OracleException(num, string.Empty, string.Empty, string.Empty);
				}
			}
			else if (methodParam is OracleString)
			{
				OracleTimeStampLTZ oracleTimeStampLTZ4 = new OracleTimeStampLTZ(((OracleString)methodParam).Value);
				int num = OpsTSL.AllocOCIFromValCtx(OracleConnection.GetInternalConnection().m_opoConCtx.opsConCtx, oracleTimeStampLTZ4.GetValCtx(), out zero);
				if (num != 0)
				{
					throw new OracleException(num, string.Empty, string.Empty, string.Empty);
				}
			}
			else if (methodParam is OracleDate)
			{
				OpoTSValCtx* ptr2;
				int num = OpsTSL.AllocValCtxForFromDate(((OracleDate)methodParam).GetValCtx(), out ptr2);
				if (num != 0)
				{
					throw new OracleException(num, string.Empty, string.Empty, string.Empty);
				}
				num = OpsTSL.AllocOCIFromValCtx(OracleConnection.GetInternalConnection().m_opoConCtx.opsConCtx, ptr2, out zero);
				if (num != 0)
				{
					throw new OracleException(num, string.Empty, string.Empty, string.Empty);
				}
				num = OpsTSL.FreeValCtx(ptr2);
				if (num != 0)
				{
					throw new OracleException(num, string.Empty, string.Empty, string.Empty);
				}
			}
			else if (methodParam is OracleTimeStamp)
			{
				int num = OpsTSL.AllocOCIFromValCtx(OracleConnection.GetInternalConnection().m_opoConCtx.opsConCtx, ((OracleTimeStamp)methodParam).ToOracleTimeStampLTZ().GetValCtx(), out zero);
				if (num != 0)
				{
					throw new OracleException(num, string.Empty, string.Empty, string.Empty);
				}
			}
			else if (methodParam is OracleTimeStampTZ)
			{
				int num = OpsTSL.AllocOCIFromValCtx(OracleConnection.GetInternalConnection().m_opoConCtx.opsConCtx, ((OracleTimeStampTZ)methodParam).ToOracleTimeStampLTZ().GetValCtx(), out zero);
				if (num != 0)
				{
					throw new OracleException(num, string.Empty, string.Empty, string.Empty);
				}
			}
			else if (methodParam is OracleTimeStampLTZ)
			{
				int num = OpsTSL.AllocOCIFromValCtx(OracleConnection.GetInternalConnection().m_opoConCtx.opsConCtx, ((OracleTimeStampLTZ)methodParam).GetValCtx(), out zero);
				if (num != 0)
				{
					throw new OracleException(num, string.Empty, string.Empty, string.Empty);
				}
			}
			return zero;
		}

		// Token: 0x06000B06 RID: 2822 RVA: 0x00070DE0 File Offset: 0x0006FDE0
		public static bool Equals(OracleTimeStampLTZ value1, OracleTimeStampLTZ value2)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleTimeStampLTZ::Equals(1)\n"
				});
			}
			CompareNullEnum compareNullEnum = InternalTypes.CompareNull(!value1.m_bNotNull, !value2.m_bNotNull);
			if (compareNullEnum == CompareNullEnum.BothNull)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleTimeStampLTZ::Equals(1)\n"
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
						" (EXIT)  OracleTimeStampLTZ::Equals(1)\n"
					});
				}
				return false;
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleTimeStampLTZ::Equals(1)\n"
				});
			}
			return TimeStamp.Compare(value1.m_opoTSCtx, value2.m_opoTSCtx) == 0;
		}

		// Token: 0x06000B07 RID: 2823 RVA: 0x00070EA4 File Offset: 0x0006FEA4
		public static bool GreaterThan(OracleTimeStampLTZ value1, OracleTimeStampLTZ value2)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleTimeStampLTZ::GreaterThan()\n"
				});
			}
			CompareNullEnum compareNullEnum = InternalTypes.CompareNull(!value1.m_bNotNull, !value2.m_bNotNull);
			if (compareNullEnum == CompareNullEnum.BothNull)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleTimeStampLTZ::GreaterThan()\n"
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
						" (EXIT)  OracleTimeStampLTZ::GreaterThan()\n"
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
						" (EXIT)  OracleTimeStampLTZ::GreaterThan()\n"
					});
				}
				return true;
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleTimeStampLTZ::GreaterThan()\n"
				});
			}
			return TimeStamp.Compare(value1.m_opoTSCtx, value2.m_opoTSCtx) > 0;
		}

		// Token: 0x06000B08 RID: 2824 RVA: 0x00070F90 File Offset: 0x0006FF90
		public static bool GreaterThanOrEqual(OracleTimeStampLTZ value1, OracleTimeStampLTZ value2)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleTimeStampLTZ::GreaterThanOrEqual()\n"
				});
			}
			CompareNullEnum compareNullEnum = InternalTypes.CompareNull(!value1.m_bNotNull, !value2.m_bNotNull);
			if (compareNullEnum == CompareNullEnum.BothNull)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleTimeStampLTZ::GreaterThanOrEqual()\n"
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
						" (EXIT)  OracleTimeStampLTZ::GreaterThanOrEqual()\n"
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
						" (EXIT)  OracleTimeStampLTZ::GreaterThanOrEqual()\n"
					});
				}
				return true;
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleTimeStampLTZ::GreaterThanOrEqual()\n"
				});
			}
			return TimeStamp.Compare(value1.m_opoTSCtx, value2.m_opoTSCtx) >= 0;
		}

		// Token: 0x06000B09 RID: 2825 RVA: 0x0007107C File Offset: 0x0007007C
		public static bool LessThan(OracleTimeStampLTZ value1, OracleTimeStampLTZ value2)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleTimeStampLTZ::LessThan()\n"
				});
			}
			CompareNullEnum compareNullEnum = InternalTypes.CompareNull(!value1.m_bNotNull, !value2.m_bNotNull);
			if (compareNullEnum == CompareNullEnum.BothNull)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleTimeStampLTZ::LessThan()\n"
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
						" (EXIT)  OracleTimeStampLTZ::LessThan()\n"
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
						" (EXIT)  OracleTimeStampLTZ::LessThan()\n"
					});
				}
				return false;
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleTimeStampLTZ::LessThan()\n"
				});
			}
			return TimeStamp.Compare(value1.m_opoTSCtx, value2.m_opoTSCtx) < 0;
		}

		// Token: 0x06000B0A RID: 2826 RVA: 0x00071168 File Offset: 0x00070168
		public static bool LessThanOrEqual(OracleTimeStampLTZ value1, OracleTimeStampLTZ value2)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleTimeStampLTZ::LessThanOrEqual()\n"
				});
			}
			CompareNullEnum compareNullEnum = InternalTypes.CompareNull(!value1.m_bNotNull, !value2.m_bNotNull);
			if (compareNullEnum == CompareNullEnum.BothNull)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleTimeStampLTZ::LessThanOrEqual()\n"
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
						" (EXIT)  OracleTimeStampLTZ::LessThanOrEqual()\n"
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
						" (EXIT)  OracleTimeStampLTZ::LessThanOrEqual()\n"
					});
				}
				return false;
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleTimeStampLTZ::LessThanOrEqual()\n"
				});
			}
			return TimeStamp.Compare(value1.m_opoTSCtx, value2.m_opoTSCtx) <= 0;
		}

		// Token: 0x06000B0B RID: 2827 RVA: 0x00071251 File Offset: 0x00070251
		public static bool NotEquals(OracleTimeStampLTZ value1, OracleTimeStampLTZ value2)
		{
			return !OracleTimeStampLTZ.Equals(value1, value2);
		}

		// Token: 0x06000B0C RID: 2828 RVA: 0x00071260 File Offset: 0x00070260
		public unsafe static OracleTimeStampLTZ GetSysDate()
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleTimeStampLTZ::GetSysDate()\n"
				});
			}
			int num = 0;
			OpoTSValCtx* ptr = null;
			try
			{
				num = OpsTSL.AllocValCtxForSysDate(out ptr);
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
							" (EXIT)  OracleTimeStampLTZ::GetSysDate()\n"
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
					" (EXIT)  OracleTimeStampLTZ::GetSysDate()\n"
				});
			}
			return new OracleTimeStampLTZ(ptr);
		}

		// Token: 0x06000B0D RID: 2829 RVA: 0x0007135C File Offset: 0x0007035C
		public static string GetLocalTimeZoneName()
		{
			return TimeStamp.LocalTZName;
		}

		// Token: 0x06000B0E RID: 2830 RVA: 0x00071363 File Offset: 0x00070363
		public static TimeSpan GetLocalTimeZoneOffset()
		{
			return new TimeSpan(TimeStamp.LocalTZOffset.m_tzHours, TimeStamp.LocalTZOffset.m_tzMinutes, 0);
		}

		// Token: 0x06000B0F RID: 2831 RVA: 0x0007137F File Offset: 0x0007037F
		public static OracleTimeStampLTZ Parse(string tsStr)
		{
			if (tsStr == null)
			{
				throw new ArgumentNullException();
			}
			return new OracleTimeStampLTZ(tsStr);
		}

		// Token: 0x06000B10 RID: 2832 RVA: 0x00071390 File Offset: 0x00070390
		public unsafe static OracleTimeStampLTZ SetPrecision(OracleTimeStampLTZ value1, int fracSecPrecision)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleTimeStampLTZ::SetPrecision()\n"
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
				num = OpsTSL.AllocValCtxFromBytes(value1.BinData, out ptr, fracSecPrecision);
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
							" (EXIT)  OracleTimeStampLTZ::SetPrecision()\n"
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
					" (EXIT)  OracleTimeStampLTZ::SetPrecision()\n"
				});
			}
			return new OracleTimeStampLTZ(ptr, fracSecPrecision);
		}

		// Token: 0x06000B11 RID: 2833 RVA: 0x000714B8 File Offset: 0x000704B8
		public static bool operator ==(OracleTimeStampLTZ value1, OracleTimeStampLTZ value2)
		{
			return OracleTimeStampLTZ.Equals(value1, value2);
		}

		// Token: 0x06000B12 RID: 2834 RVA: 0x000714C1 File Offset: 0x000704C1
		public static bool operator >(OracleTimeStampLTZ value1, OracleTimeStampLTZ value2)
		{
			return OracleTimeStampLTZ.GreaterThan(value1, value2);
		}

		// Token: 0x06000B13 RID: 2835 RVA: 0x000714CA File Offset: 0x000704CA
		public static bool operator >=(OracleTimeStampLTZ value1, OracleTimeStampLTZ value2)
		{
			return OracleTimeStampLTZ.GreaterThanOrEqual(value1, value2);
		}

		// Token: 0x06000B14 RID: 2836 RVA: 0x000714D3 File Offset: 0x000704D3
		public static bool operator <(OracleTimeStampLTZ value1, OracleTimeStampLTZ value2)
		{
			return OracleTimeStampLTZ.LessThan(value1, value2);
		}

		// Token: 0x06000B15 RID: 2837 RVA: 0x000714DC File Offset: 0x000704DC
		public static bool operator <=(OracleTimeStampLTZ value1, OracleTimeStampLTZ value2)
		{
			return OracleTimeStampLTZ.LessThanOrEqual(value1, value2);
		}

		// Token: 0x06000B16 RID: 2838 RVA: 0x000714E5 File Offset: 0x000704E5
		public static bool operator !=(OracleTimeStampLTZ value1, OracleTimeStampLTZ value2)
		{
			return OracleTimeStampLTZ.NotEquals(value1, value2);
		}

		// Token: 0x06000B17 RID: 2839 RVA: 0x000714F0 File Offset: 0x000704F0
		public unsafe static OracleTimeStampLTZ operator +(OracleTimeStampLTZ value1, OracleIntervalDS value2)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleTimeStampLTZ::operator +(1)\n"
				});
			}
			if (value1.IsNull || value2.IsNull)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleTimeStampLTZ::operator +(1)\n"
					});
				}
				return OracleTimeStampLTZ.Null;
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
					" (EXIT)  OracleTimeStampLTZ::operator +(1)\n"
				});
			}
			return new OracleTimeStampLTZ(ctx);
		}

		// Token: 0x06000B18 RID: 2840 RVA: 0x000715D4 File Offset: 0x000705D4
		public unsafe static OracleTimeStampLTZ operator +(OracleTimeStampLTZ value1, OracleIntervalYM value2)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleTimeStampLTZ::operator +(2)\n"
				});
			}
			if (value1.IsNull || value2.IsNull)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleTimeStampLTZ::operator +()\n"
					});
				}
				return OracleTimeStampLTZ.Null;
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
					" (EXIT)  OracleTimeStampLTZ::operator +()\n"
				});
			}
			return new OracleTimeStampLTZ(ctx);
		}

		// Token: 0x06000B19 RID: 2841 RVA: 0x000716B8 File Offset: 0x000706B8
		public unsafe static OracleTimeStampLTZ operator +(OracleTimeStampLTZ value1, TimeSpan value2)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleTimeStampLTZ::operator +(3)\n"
				});
			}
			if (value1.IsNull)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleTimeStampLTZ::operator +(3)\n"
					});
				}
				return OracleTimeStampLTZ.Null;
			}
			int mesgNum = 0;
			OpoITLValCtx opoITLValCtx = default(OpoITLValCtx);
			GCHandle gchandle = GCHandle.Alloc(opoITLValCtx, GCHandleType.Pinned);
			OracleTimeStampLTZ result;
			try
			{
				OracleIntervalDS.FillValCtxFromTimeSpan(&opoITLValCtx, value2);
				OpoTSValCtx* ptr;
				if ((mesgNum = OpsTSA.AllocValCtxForAddInterval(value1.m_opoTSCtx.m_pValCtx, &opoITLValCtx, out ptr)) != 0)
				{
					if (ptr != null)
					{
						try
						{
							OpsTS.FreeValCtx(ptr);
						}
						catch (Exception ex)
						{
							if (OraTrace.m_TraceLevel != 0U)
							{
								OraTrace.TraceExceptionInfo(ex);
							}
						}
						ptr = null;
					}
					if (OraTrace.m_TraceLevel != 0U)
					{
						OraTrace.Trace(1U, new string[]
						{
							" (EXIT)  OracleTimeStampLTZ::operator +(3)\n"
						});
					}
					throw new OracleTypeException(mesgNum, new object[0]);
				}
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleTimeStampLTZ::operator +(3)\n"
					});
				}
				result = new OracleTimeStampLTZ(ptr);
			}
			finally
			{
				if (gchandle.IsAllocated)
				{
					gchandle.Free();
				}
			}
			return result;
		}

		// Token: 0x06000B1A RID: 2842 RVA: 0x000717F8 File Offset: 0x000707F8
		public unsafe static OracleTimeStampLTZ operator -(OracleTimeStampLTZ value1, OracleIntervalDS value2)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleTimeStampLTZ::operator -(1)\n"
				});
			}
			if (value1.IsNull || value2.IsNull)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleTimeStampLTZ::operator -(1)\n"
					});
				}
				return OracleTimeStampLTZ.Null;
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
					" (EXIT)  OracleTimeStampLTZ::operator -(1)\n"
				});
			}
			return new OracleTimeStampLTZ(ctx);
		}

		// Token: 0x06000B1B RID: 2843 RVA: 0x000718DC File Offset: 0x000708DC
		public unsafe static OracleTimeStampLTZ operator -(OracleTimeStampLTZ value1, OracleIntervalYM value2)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleTimeStampLTZ::operator -(2)\n"
				});
			}
			if (value1.IsNull || value2.IsNull)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleTimeStampLTZ::operator -(2)\n"
					});
				}
				return OracleTimeStampLTZ.Null;
			}
			int num = 0;
			OpoTSCtx ctx = new OpoTSCtx(TimeStampType.TSType_TSL);
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
					" (EXIT)  OracleTimeStampLTZ::operator -(2)\n"
				});
			}
			return new OracleTimeStampLTZ(ctx);
		}

		// Token: 0x06000B1C RID: 2844 RVA: 0x000719C4 File Offset: 0x000709C4
		public unsafe static OracleTimeStampLTZ operator -(OracleTimeStampLTZ value1, TimeSpan value2)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleTimeStampLTZ::operator -(3)\n"
				});
			}
			if (value1.IsNull)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleTimeStampLTZ::operator -(3)\n"
					});
				}
				return OracleTimeStampLTZ.Null;
			}
			int num = 0;
			OpoTSCtx ctx = new OpoTSCtx(TimeStampType.TSType_TSL);
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
					" (EXIT)  OracleTimeStampLTZ::operator -(3)\n"
				});
			}
			return new OracleTimeStampLTZ(ctx);
		}

		// Token: 0x06000B1D RID: 2845 RVA: 0x00071AC0 File Offset: 0x00070AC0
		public static explicit operator OracleTimeStampLTZ(string tsStr)
		{
			return new OracleTimeStampLTZ(tsStr);
		}

		// Token: 0x06000B1E RID: 2846 RVA: 0x00071AC8 File Offset: 0x00070AC8
		public static explicit operator DateTime(OracleTimeStampLTZ value1)
		{
			if (value1.m_bNotNull)
			{
				return DateTimeConv.GetDateTime(value1.m_opoTSCtx.m_pValCtx, OracleDbType.TimeStampLTZ, false);
			}
			throw new OracleNullValueException();
		}

		// Token: 0x06000B1F RID: 2847 RVA: 0x00071AF0 File Offset: 0x00070AF0
		public unsafe static implicit operator OracleTimeStampLTZ(OracleDate value1)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleTimeStampLTZ::operator OracleTimeStampLTZ(1)\n"
				});
			}
			if (value1.IsNull)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleTimeStampLTZ::operator OracleTimeStampLTZ(1)\n"
					});
				}
				return OracleTimeStampLTZ.Null;
			}
			int num = 0;
			OpoTSValCtx* pCtx;
			try
			{
				num = OpsTSL.AllocValCtxForFromDate(value1.GetValCtx(), out pCtx);
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
					" (EXIT)  OracleTimeStampLTZ::operator OracleTimeStampLTZ(1)\n"
				});
			}
			return new OracleTimeStampLTZ(ctx);
		}

		// Token: 0x06000B20 RID: 2848 RVA: 0x00071BBC File Offset: 0x00070BBC
		public static explicit operator OracleTimeStampLTZ(OracleTimeStamp value1)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleTimeStampLTZ::operator OracleTimeStampLTZ(2)\n"
				});
			}
			if (!value1.IsNull)
			{
				return OracleTimeStamp.ToTSL(value1);
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleTimeStampLTZ::operator OracleTimeStampLTZ(2)\n"
				});
			}
			return OracleTimeStampLTZ.Null;
		}

		// Token: 0x06000B21 RID: 2849 RVA: 0x00071C18 File Offset: 0x00070C18
		public static explicit operator OracleTimeStampLTZ(OracleTimeStampTZ value1)
		{
			if (!value1.IsNull)
			{
				return OracleTimeStampTZ.ToTSL(value1);
			}
			return OracleTimeStampLTZ.Null;
		}

		// Token: 0x06000B22 RID: 2850 RVA: 0x00071C2F File Offset: 0x00070C2F
		public static implicit operator OracleTimeStampLTZ(DateTime value1)
		{
			return new OracleTimeStampLTZ(value1);
		}

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x06000B23 RID: 2851 RVA: 0x00071C38 File Offset: 0x00070C38
		public unsafe byte[] BinData
		{
			get
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (ENTRY) OracleTimeStampLTZ::BinData: get\n"
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
					num2 = OpsTSL.ToBytes(this.m_opoTSCtx.m_pValCtx, array, &num);
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
						" (EXIT)  OracleTimeStampLTZ::BinData: get\n"
					});
				}
				return array;
			}
		}

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x06000B24 RID: 2852 RVA: 0x00071CE8 File Offset: 0x00070CE8
		public bool IsNull
		{
			get
			{
				return !this.m_bNotNull;
			}
		}

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x06000B25 RID: 2853 RVA: 0x00071CF3 File Offset: 0x00070CF3
		public DateTime Value
		{
			get
			{
				if (this.m_bNotNull)
				{
					return DateTimeConv.GetDateTime(this.m_opoTSCtx.m_pValCtx, OracleDbType.TimeStampLTZ, false);
				}
				throw new OracleNullValueException();
			}
		}

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x06000B26 RID: 2854 RVA: 0x00071D16 File Offset: 0x00070D16
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

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x06000B27 RID: 2855 RVA: 0x00071D2D File Offset: 0x00070D2D
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

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x06000B28 RID: 2856 RVA: 0x00071D44 File Offset: 0x00070D44
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

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x06000B29 RID: 2857 RVA: 0x00071D5B File Offset: 0x00070D5B
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

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x06000B2A RID: 2858 RVA: 0x00071D72 File Offset: 0x00070D72
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

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x06000B2B RID: 2859 RVA: 0x00071D89 File Offset: 0x00070D89
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

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x06000B2C RID: 2860 RVA: 0x00071DA0 File Offset: 0x00070DA0
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

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x06000B2D RID: 2861 RVA: 0x00071DC3 File Offset: 0x00070DC3
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

		// Token: 0x06000B2E RID: 2862 RVA: 0x00071DDC File Offset: 0x00070DDC
		public unsafe OracleTimeStampLTZ AddYears(int years)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleTimeStampLTZ::AddYears()\n"
				});
			}
			int num = 0;
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
					" (EXIT)  OracleTimeStampLTZ::AddYears()\n"
				});
			}
			return new OracleTimeStampLTZ(ctx);
		}

		// Token: 0x06000B2F RID: 2863 RVA: 0x00071EA4 File Offset: 0x00070EA4
		public unsafe OracleTimeStampLTZ AddMonths(long months)
		{
			int num = 0;
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleTimeStampLTZ::AddMonths()\n"
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
					" (EXIT)  OracleTimeStampLTZ::AddMonths()\n"
				});
			}
			return new OracleTimeStampLTZ(ctx);
		}

		// Token: 0x06000B30 RID: 2864 RVA: 0x00071F74 File Offset: 0x00070F74
		public unsafe OracleTimeStampLTZ AddDays(double days)
		{
			int num = 0;
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleTimeStampLTZ::AddDays()\n"
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
					" (EXIT)  OracleTimeStampLTZ::AddDays()\n"
				});
			}
			return new OracleTimeStampLTZ(ctx);
		}

		// Token: 0x06000B31 RID: 2865 RVA: 0x00072044 File Offset: 0x00071044
		public unsafe OracleTimeStampLTZ AddHours(double hours)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleTimeStampLTZ::AddHours()\n"
				});
			}
			int num = 0;
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
					" (EXIT)  OracleTimeStampLTZ::AddHours()\n"
				});
			}
			return new OracleTimeStampLTZ(ctx);
		}

		// Token: 0x06000B32 RID: 2866 RVA: 0x00072114 File Offset: 0x00071114
		public unsafe OracleTimeStampLTZ AddMinutes(double minutes)
		{
			int num = 0;
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleTimeStampLTZ::AddMinutes()\n"
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
					" (EXIT)  OracleTimeStampLTZ::AddMinutes()\n"
				});
			}
			return new OracleTimeStampLTZ(ctx);
		}

		// Token: 0x06000B33 RID: 2867 RVA: 0x000721E4 File Offset: 0x000711E4
		public unsafe OracleTimeStampLTZ AddSeconds(double seconds)
		{
			int num = 0;
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleTimeStampLTZ::AddSeconds()\n"
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
					" (EXIT)  OracleTimeStampLTZ::AddSeconds()\n"
				});
			}
			return new OracleTimeStampLTZ(ctx);
		}

		// Token: 0x06000B34 RID: 2868 RVA: 0x000722B4 File Offset: 0x000712B4
		public unsafe OracleTimeStampLTZ AddMilliseconds(double milliseconds)
		{
			int num = 0;
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleTimeStampLTZ::AddMilliseconds()\n"
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
					" (EXIT)  OracleTimeStampLTZ::AddMilliseconds()\n"
				});
			}
			return new OracleTimeStampLTZ(ctx);
		}

		// Token: 0x06000B35 RID: 2869 RVA: 0x00072384 File Offset: 0x00071384
		public unsafe OracleTimeStampLTZ AddNanoseconds(long nanoseconds)
		{
			int num = 0;
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleTimeStampLTZ::AddNanoseconds()\n"
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
					" (EXIT)  OracleTimeStampLTZ::AddNanoseconds()\n"
				});
			}
			return new OracleTimeStampLTZ(ctx);
		}

		// Token: 0x06000B36 RID: 2870 RVA: 0x00072434 File Offset: 0x00071434
		public int CompareTo(object obj)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleTimeStampLTZ::CompareTo()\n"
				});
			}
			if (obj.GetType() != typeof(OracleTimeStampLTZ))
			{
				throw new ArgumentException();
			}
			OracleTimeStampLTZ oracleTimeStampLTZ = (OracleTimeStampLTZ)obj;
			CompareNullEnum compareNullEnum = InternalTypes.CompareNull(!this.m_bNotNull, !oracleTimeStampLTZ.m_bNotNull);
			if (compareNullEnum == CompareNullEnum.BothNull)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleTimeStampLTZ::CompareTo()\n"
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
						" (EXIT)  OracleTimeStampLTZ::CompareTo()\n"
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
						" (EXIT)  OracleTimeStampLTZ::CompareTo()\n"
					});
				}
				return 1;
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleTimeStampLTZ::CompareTo()\n"
				});
			}
			return TimeStamp.Compare(this.m_opoTSCtx, oracleTimeStampLTZ.m_opoTSCtx);
		}

		// Token: 0x06000B37 RID: 2871 RVA: 0x0007253C File Offset: 0x0007153C
		public override bool Equals(object obj)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleTimeStampLTZ::Equals(2)\n"
				});
			}
			if (obj == null)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleTimeStampLTZ::Equals(2)\n"
					});
				}
				return false;
			}
			if (obj.GetType() != typeof(OracleTimeStampLTZ))
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleTimeStampLTZ::Equals(2)\n"
					});
				}
				return false;
			}
			OracleTimeStampLTZ value = (OracleTimeStampLTZ)obj;
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleTimeStampLTZ::Equals(2)\n"
				});
			}
			return OracleTimeStampLTZ.Equals(this, value);
		}

		// Token: 0x06000B38 RID: 2872 RVA: 0x000725F1 File Offset: 0x000715F1
		public override int GetHashCode()
		{
			if (this.m_bNotNull)
			{
				return this.m_opoTSCtx.GetHashCode();
			}
			return 0;
		}

		// Token: 0x06000B39 RID: 2873 RVA: 0x00072608 File Offset: 0x00071608
		public unsafe OracleIntervalDS GetDaysBetween(OracleTimeStampLTZ value1)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleTimeStampLTZ::GetDaysBetween()\n"
				});
			}
			if (!this.m_bNotNull || !value1.m_bNotNull)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleTimeStampLTZ::GetDaysBetween()\n"
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
					" (EXIT)  OracleTimeStampLTZ::GetDaysBetween()\n"
				});
			}
			return new OracleIntervalDS(ctx2);
		}

		// Token: 0x06000B3A RID: 2874 RVA: 0x000726EC File Offset: 0x000716EC
		public unsafe OracleIntervalYM GetYearsBetween(OracleTimeStampLTZ value1)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleTimeStampLTZ::GetYearsBetween()\n"
				});
			}
			if (!this.m_bNotNull || !value1.m_bNotNull)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleTimeStampLTZ::GetYearsBetween()\n"
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
					" (EXIT)  OracleTimeStampLTZ::GetYearsBetween()\n"
				});
			}
			return new OracleIntervalYM(ctx2);
		}

		// Token: 0x06000B3B RID: 2875 RVA: 0x000727D0 File Offset: 0x000717D0
		public unsafe OracleDate ToOracleDate()
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleTimeStampLTZ::ToOracleDate()\n"
				});
			}
			if (!this.m_bNotNull)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleTimeStampLTZ::ToOracleDate()\n"
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
					" (EXIT)  OracleTimeStampLTZ::ToOracleDate()\n"
				});
			}
			return new OracleDate(ctx);
		}

		// Token: 0x06000B3C RID: 2876 RVA: 0x000728A0 File Offset: 0x000718A0
		public OracleTimeStamp ToOracleTimeStamp()
		{
			if (this.m_bNotNull)
			{
				return OracleTimeStampLTZ.ToTS(this);
			}
			return OracleTimeStamp.Null;
		}

		// Token: 0x06000B3D RID: 2877 RVA: 0x000728BB File Offset: 0x000718BB
		public OracleTimeStampTZ ToOracleTimeStampTZ()
		{
			if (this.m_bNotNull)
			{
				return OracleTimeStampLTZ.ToTSZ(this);
			}
			return OracleTimeStampTZ.Null;
		}

		// Token: 0x06000B3E RID: 2878 RVA: 0x000728D8 File Offset: 0x000718D8
		public override string ToString()
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleTimeStampLTZ::ToString()\n"
				});
			}
			if (this.m_bNotNull)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleTimeStampLTZ::ToString()\n"
					});
				}
				return TimeStamp.ToTSString(this.m_opoTSCtx, this.m_fSecondPrec, TimeStampType.TSType_TSL);
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleTimeStampLTZ::ToString()\n"
				});
			}
			return "null";
		}

		// Token: 0x06000B3F RID: 2879 RVA: 0x0007295C File Offset: 0x0007195C
		public OracleTimeStampTZ ToUniversalTime()
		{
			if (this.m_bNotNull)
			{
				return TimeStamp.ToUniversalTime(this.m_opoTSCtx);
			}
			return OracleTimeStampTZ.Null;
		}

		// Token: 0x06000B40 RID: 2880 RVA: 0x00072977 File Offset: 0x00071977
		internal unsafe OracleTimeStampLTZ(OpoTSValCtx* pCtx)
		{
			this = new OracleTimeStampLTZ(pCtx, 9);
		}

		// Token: 0x06000B41 RID: 2881 RVA: 0x00072982 File Offset: 0x00071982
		internal unsafe OracleTimeStampLTZ(OpoTSValCtx* pCtx, int fSecondPrec)
		{
			this.m_opoTSCtx = new OpoTSCtx(pCtx);
			this.m_bNotNull = true;
			this.m_fSecondPrec = fSecondPrec;
		}

		// Token: 0x06000B42 RID: 2882 RVA: 0x0007299E File Offset: 0x0007199E
		internal OracleTimeStampLTZ(OpoTSCtx ctx)
		{
			this.m_opoTSCtx = ctx;
			this.m_bNotNull = true;
			this.m_fSecondPrec = 9;
		}

		// Token: 0x06000B43 RID: 2883 RVA: 0x000729B8 File Offset: 0x000719B8
		internal unsafe static OracleTimeStamp ToTS(OracleTimeStampLTZ value1)
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

		// Token: 0x06000B44 RID: 2884 RVA: 0x00072A2C File Offset: 0x00071A2C
		internal unsafe static OracleTimeStampTZ ToTSZ(OracleTimeStampLTZ value1)
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

		// Token: 0x06000B45 RID: 2885 RVA: 0x00072AA0 File Offset: 0x00071AA0
		internal unsafe OpoTSValCtx* GetValCtx()
		{
			return this.m_opoTSCtx.m_pValCtx;
		}

		// Token: 0x06000B46 RID: 2886 RVA: 0x00072AB0 File Offset: 0x00071AB0
		internal IntPtr DupValCtx()
		{
			IntPtr zero = IntPtr.Zero;
			int num = OpsTSA.DupValCtx(this.GetValCtx(), out zero, TimeStampType.TSType_TSL);
			if (num != 0)
			{
				throw new OracleException(num, string.Empty, string.Empty, string.Empty);
			}
			return zero;
		}

		// Token: 0x06000B47 RID: 2887 RVA: 0x00072AEE File Offset: 0x00071AEE
		internal int GetTSData(byte tsComponent)
		{
			return TimeStamp.GetTSData(this.m_opoTSCtx.m_pValCtx, tsComponent);
		}

		// Token: 0x0400091C RID: 2332
		internal const byte MaxArrSize = 11;

		// Token: 0x0400091D RID: 2333
		public static readonly OracleTimeStampLTZ MaxValue = new OracleTimeStampLTZ(9999, 12, 31, 23, 59, 59, 999999999);

		// Token: 0x0400091E RID: 2334
		public static readonly OracleTimeStampLTZ MinValue = new OracleTimeStampLTZ(-4712, 1, 1, 0, 0, 0, 0);

		// Token: 0x0400091F RID: 2335
		public static readonly OracleTimeStampLTZ Null;

		// Token: 0x04000920 RID: 2336
		private OpoTSCtx m_opoTSCtx;

		// Token: 0x04000921 RID: 2337
		private bool m_bNotNull;

		// Token: 0x04000922 RID: 2338
		private int m_fSecondPrec;
	}
}
