using System;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Oracle.DataAccess.Client;

namespace Oracle.DataAccess.Types
{
	// Token: 0x0200008A RID: 138
	[XmlSchemaProvider("GetXsdType")]
	[Serializable]
	public struct OracleIntervalDS : IComparable, INullable, IXmlSerializable
	{
		// Token: 0x0600066A RID: 1642 RVA: 0x00042C54 File Offset: 0x00041C54
		static OracleIntervalDS()
		{
			if (!OracleInit.bSetDllDirectoryInvoked)
			{
				OracleInit.Initialize();
			}
		}

		// Token: 0x0600066B RID: 1643 RVA: 0x00042CB0 File Offset: 0x00041CB0
		public static XmlQualifiedName GetXsdType(XmlSchemaSet schemaSet)
		{
			return new XmlQualifiedName("string", "http://www.w3.org/2001/XMLSchema");
		}

		// Token: 0x0600066C RID: 1644 RVA: 0x00042CC1 File Offset: 0x00041CC1
		XmlSchema IXmlSerializable.GetSchema()
		{
			return null;
		}

		// Token: 0x0600066D RID: 1645 RVA: 0x00042CC4 File Offset: 0x00041CC4
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			string text = reader.GetAttribute("null", "http://www.w3.org/2001/XMLSchema-instance");
			if (text != null && XmlConvert.ToBoolean(text))
			{
				this.m_bNotNull = false;
				return;
			}
			text = reader.ReadElementString();
			this.m_opoIDSCtx = new OpoIDSCtx(text);
			if (this.m_opoIDSCtx.m_error != 0)
			{
				throw new ArgumentException(OracleTypeException.GetTypeMsg(this.m_opoIDSCtx.m_error, new object[0]));
			}
			this.m_fSecondPrec = 9;
			this.m_dayPrec = 9;
			this.m_bNotNull = true;
		}

		// Token: 0x0600066E RID: 1646 RVA: 0x00042D48 File Offset: 0x00041D48
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			if (this.m_bNotNull)
			{
				writer.WriteString(ITLMethods.ToString(this.m_opoIDSCtx.m_pValCtx, this.m_dayPrec, this.m_fSecondPrec));
				return;
			}
			writer.WriteAttributeString("xsi", "null", "http://www.w3.org/2001/XMLSchema-instance", "true");
		}

		// Token: 0x0600066F RID: 1647 RVA: 0x00042D9C File Offset: 0x00041D9C
		public OracleIntervalDS(int days, int hours, int minutes, int seconds, int nanoseconds)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleIntervalDS::OracleIntervalDS(1)\n"
				});
			}
			if (!Interval.IsValidDS(days, hours, minutes, seconds, nanoseconds))
			{
				throw new ArgumentOutOfRangeException();
			}
			this.m_opoIDSCtx = new OpoIDSCtx(days, hours, minutes, seconds, nanoseconds);
			if (this.m_opoIDSCtx.m_error != 0)
			{
				throw new ArgumentException(OracleTypeException.GetTypeMsg(this.m_opoIDSCtx.m_error, new object[0]));
			}
			this.m_bNotNull = true;
			this.m_fSecondPrec = 9;
			this.m_dayPrec = 9;
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleIntervalDS::OracleIntervalDS(1)\n"
				});
			}
		}

		// Token: 0x06000670 RID: 1648 RVA: 0x00042E4C File Offset: 0x00041E4C
		public OracleIntervalDS(int days, int hours, int minutes, int seconds, double milliseconds)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleIntervalDS::OracleIntervalDS(2)\n"
				});
			}
			int num = (int)(milliseconds * 1000000.0);
			if (!Interval.IsValidDS(days, hours, minutes, seconds, num))
			{
				throw new ArgumentOutOfRangeException();
			}
			this.m_opoIDSCtx = new OpoIDSCtx(days, hours, minutes, seconds, num);
			if (this.m_opoIDSCtx.m_error != 0)
			{
				throw new ArgumentException(OracleTypeException.GetTypeMsg(this.m_opoIDSCtx.m_error, new object[0]));
			}
			this.m_bNotNull = true;
			this.m_fSecondPrec = 9;
			this.m_dayPrec = 9;
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleIntervalDS::OracleIntervalDS(2)\n"
				});
			}
		}

		// Token: 0x06000671 RID: 1649 RVA: 0x00042F08 File Offset: 0x00041F08
		public OracleIntervalDS(TimeSpan data)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleIntervalDS::OracleIntervalDS(3)\n"
				});
			}
			this.m_opoIDSCtx = new OpoIDSCtx(data);
			this.m_bNotNull = true;
			this.m_fSecondPrec = 9;
			this.m_dayPrec = 9;
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleIntervalDS::OracleIntervalDS(3)\n"
				});
			}
		}

		// Token: 0x06000672 RID: 1650 RVA: 0x00042F74 File Offset: 0x00041F74
		public OracleIntervalDS(double totalDays)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleIntervalDS::OracleIntervalDS(4)\n"
				});
			}
			if (!Interval.IsValidDSDays(totalDays))
			{
				throw new ArgumentOutOfRangeException();
			}
			this.m_opoIDSCtx = new OpoIDSCtx(totalDays);
			if (this.m_opoIDSCtx.m_error != 0)
			{
				throw new ArgumentException(OracleTypeException.GetTypeMsg(this.m_opoIDSCtx.m_error, new object[0]));
			}
			this.m_bNotNull = true;
			this.m_fSecondPrec = 9;
			this.m_dayPrec = 9;
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleIntervalDS::OracleIntervalDS(4)\n"
				});
			}
		}

		// Token: 0x06000673 RID: 1651 RVA: 0x00043018 File Offset: 0x00042018
		public OracleIntervalDS(string intervalStr)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleIntervalDS::OracleIntervalDS(5)\n"
				});
			}
			if (intervalStr == null)
			{
				throw new ArgumentNullException();
			}
			this.m_opoIDSCtx = new OpoIDSCtx(intervalStr);
			if (this.m_opoIDSCtx.m_error != 0)
			{
				throw new ArgumentException(OracleTypeException.GetTypeMsg(this.m_opoIDSCtx.m_error, new object[0]));
			}
			this.m_bNotNull = true;
			this.m_fSecondPrec = 9;
			this.m_dayPrec = 9;
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleIntervalDS::OracleIntervalDS(5)\n"
				});
			}
		}

		// Token: 0x06000674 RID: 1652 RVA: 0x000430B4 File Offset: 0x000420B4
		internal unsafe OracleIntervalDS(IntPtr ociInterval)
		{
			OracleConnection internalConnection = OracleConnection.GetInternalConnection();
			OpoITLValCtx* ctx = null;
			int num = OpsIDS.AllocValCtxFromOCI(internalConnection.m_opoConCtx.opsConCtx, internalConnection.m_opoConCtx.opsErrCtx, ociInterval, out ctx);
			if (num != 0)
			{
				throw new OracleException(num, string.Empty, string.Empty, string.Empty);
			}
			this.m_opoIDSCtx = new OpoIDSCtx(ctx);
			this.m_bNotNull = true;
			this.m_fSecondPrec = 9;
			this.m_dayPrec = 9;
		}

		// Token: 0x06000675 RID: 1653 RVA: 0x00043128 File Offset: 0x00042128
		public OracleIntervalDS(byte[] binData)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleIntervalDS::OracleIntervalDS(6)\n"
				});
			}
			if (binData == null)
			{
				throw new ArgumentNullException();
			}
			this.m_opoIDSCtx = new OpoIDSCtx(binData);
			if (this.m_opoIDSCtx.m_error != 0)
			{
				throw new ArgumentException(OracleTypeException.GetTypeMsg(this.m_opoIDSCtx.m_error, new object[0]));
			}
			this.m_bNotNull = true;
			this.m_fSecondPrec = 9;
			this.m_dayPrec = 9;
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleIntervalDS::OracleIntervalDS(6)\n"
				});
			}
		}

		// Token: 0x06000676 RID: 1654 RVA: 0x000431C4 File Offset: 0x000421C4
		public static bool Equals(OracleIntervalDS value1, OracleIntervalDS value2)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleIntervalDS::Equals(1)\n"
				});
			}
			CompareNullEnum compareNullEnum = InternalTypes.CompareNull(!value1.m_bNotNull, !value2.m_bNotNull);
			if (compareNullEnum == CompareNullEnum.BothNull)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleIntervalDS::Equals(1)\n"
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
						" (EXIT)  OracleIntervalDS::Equals(1)\n"
					});
				}
				return false;
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleIntervalDS::Equals(1)\n"
				});
			}
			return ITLMethods.Compare(value1.m_opoIDSCtx.m_pValCtx, value2.m_opoIDSCtx.m_pValCtx) == 0;
		}

		// Token: 0x06000677 RID: 1655 RVA: 0x00043290 File Offset: 0x00042290
		public static bool GreaterThan(OracleIntervalDS value1, OracleIntervalDS value2)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleIntervalDS::GreaterThan()\n"
				});
			}
			CompareNullEnum compareNullEnum = InternalTypes.CompareNull(!value1.m_bNotNull, !value2.m_bNotNull);
			if (compareNullEnum == CompareNullEnum.BothNull)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleIntervalDS::GreaterThan()\n"
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
						" (EXIT)  OracleIntervalDS::GreaterThan()\n"
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
						" (EXIT)  OracleIntervalDS::GreaterThan()\n"
					});
				}
				return true;
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleIntervalDS::GreaterThan()\n"
				});
			}
			return ITLMethods.Compare(value1.m_opoIDSCtx.m_pValCtx, value2.m_opoIDSCtx.m_pValCtx) > 0;
		}

		// Token: 0x06000678 RID: 1656 RVA: 0x00043384 File Offset: 0x00042384
		public static bool GreaterThanOrEqual(OracleIntervalDS value1, OracleIntervalDS value2)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleIntervalDS::GreaterThanOrEqual()\n"
				});
			}
			CompareNullEnum compareNullEnum = InternalTypes.CompareNull(!value1.m_bNotNull, !value2.m_bNotNull);
			if (compareNullEnum == CompareNullEnum.BothNull)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleIntervalDS::GreaterThanOrEqual()\n"
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
						" (EXIT)  OracleIntervalDS::GreaterThanOrEqual()\n"
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
						" (EXIT)  OracleIntervalDS::GreaterThanOrEqual()\n"
					});
				}
				return true;
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleIntervalDS::GreaterThanOrEqual()\n"
				});
			}
			return ITLMethods.Compare(value1.m_opoIDSCtx.m_pValCtx, value2.m_opoIDSCtx.m_pValCtx) >= 0;
		}

		// Token: 0x06000679 RID: 1657 RVA: 0x00043478 File Offset: 0x00042478
		public static bool LessThan(OracleIntervalDS value1, OracleIntervalDS value2)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleIntervalDS::LessThan()\n"
				});
			}
			CompareNullEnum compareNullEnum = InternalTypes.CompareNull(!value1.m_bNotNull, !value2.m_bNotNull);
			if (compareNullEnum == CompareNullEnum.BothNull)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleIntervalDS::LessThan()\n"
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
						" (EXIT)  OracleIntervalDS::LessThan()\n"
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
						" (EXIT)  OracleIntervalDS::LessThan()\n"
					});
				}
				return false;
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleIntervalDS::LessThan()\n"
				});
			}
			return ITLMethods.Compare(value1.m_opoIDSCtx.m_pValCtx, value2.m_opoIDSCtx.m_pValCtx) < 0;
		}

		// Token: 0x0600067A RID: 1658 RVA: 0x0004356C File Offset: 0x0004256C
		public static bool LessThanOrEqual(OracleIntervalDS value1, OracleIntervalDS value2)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleIntervalDS::LessThanOrEqual()\n"
				});
			}
			CompareNullEnum compareNullEnum = InternalTypes.CompareNull(!value1.m_bNotNull, !value2.m_bNotNull);
			if (compareNullEnum == CompareNullEnum.BothNull)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleIntervalDS::LessThanOrEqual()\n"
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
						" (EXIT)  OracleIntervalDS::LessThanOrEqual()\n"
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
						" (EXIT)  OracleIntervalDS::LessThanOrEqual()\n"
					});
				}
				return false;
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleIntervalDS::LessThanOrEqual()\n"
				});
			}
			return ITLMethods.Compare(value1.m_opoIDSCtx.m_pValCtx, value2.m_opoIDSCtx.m_pValCtx) <= 0;
		}

		// Token: 0x0600067B RID: 1659 RVA: 0x0004365F File Offset: 0x0004265F
		public static bool NotEquals(OracleIntervalDS value1, OracleIntervalDS value2)
		{
			return !OracleIntervalDS.Equals(value1, value2);
		}

		// Token: 0x0600067C RID: 1660 RVA: 0x0004366B File Offset: 0x0004266B
		public static OracleIntervalDS Parse(string intervalStr)
		{
			return new OracleIntervalDS(intervalStr);
		}

		// Token: 0x0600067D RID: 1661 RVA: 0x00043674 File Offset: 0x00042674
		public unsafe static OracleIntervalDS SetPrecision(OracleIntervalDS value1, int dayPrecision, int fracSecPrecision)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleIntervalDS::SetPrecision()\n"
				});
			}
			if (!value1.m_bNotNull)
			{
				throw new OracleNullValueException();
			}
			int num = 0;
			if (dayPrecision < 0 || dayPrecision > 9)
			{
				throw new ArgumentOutOfRangeException("dayPrecision");
			}
			if (fracSecPrecision < 0 || fracSecPrecision > 9)
			{
				throw new ArgumentOutOfRangeException("fracSecPrecision");
			}
			OpoITLValCtx* ctx = null;
			try
			{
				num = OpsIDS.AllocValCtxFromBytes(value1.BinData, out ctx, dayPrecision, fracSecPrecision);
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
							" (EXIT)  OracleIntervalDS::SetPrecision()\n"
						});
					}
					ITLMethods.FreeCtx(ref ctx);
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
					" (EXIT)  OracleIntervalDS::SetPrecision()\n"
				});
			}
			return new OracleIntervalDS(ctx, dayPrecision, fracSecPrecision);
		}

		// Token: 0x0600067E RID: 1662 RVA: 0x0004378C File Offset: 0x0004278C
		public static bool operator ==(OracleIntervalDS value1, OracleIntervalDS value2)
		{
			return OracleIntervalDS.Equals(value1, value2);
		}

		// Token: 0x0600067F RID: 1663 RVA: 0x00043795 File Offset: 0x00042795
		public static bool operator >(OracleIntervalDS value1, OracleIntervalDS value2)
		{
			return OracleIntervalDS.GreaterThan(value1, value2);
		}

		// Token: 0x06000680 RID: 1664 RVA: 0x0004379E File Offset: 0x0004279E
		public static bool operator >=(OracleIntervalDS value1, OracleIntervalDS value2)
		{
			return OracleIntervalDS.GreaterThanOrEqual(value1, value2);
		}

		// Token: 0x06000681 RID: 1665 RVA: 0x000437A7 File Offset: 0x000427A7
		public static bool operator <(OracleIntervalDS value1, OracleIntervalDS value2)
		{
			return OracleIntervalDS.LessThan(value1, value2);
		}

		// Token: 0x06000682 RID: 1666 RVA: 0x000437B0 File Offset: 0x000427B0
		public static bool operator <=(OracleIntervalDS value1, OracleIntervalDS value2)
		{
			return OracleIntervalDS.LessThanOrEqual(value1, value2);
		}

		// Token: 0x06000683 RID: 1667 RVA: 0x000437B9 File Offset: 0x000427B9
		public static bool operator !=(OracleIntervalDS value1, OracleIntervalDS value2)
		{
			return OracleIntervalDS.NotEquals(value1, value2);
		}

		// Token: 0x06000684 RID: 1668 RVA: 0x000437C4 File Offset: 0x000427C4
		public unsafe static OracleIntervalDS operator +(OracleIntervalDS value1, OracleIntervalDS value2)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleIntervalDS::Operator +\n"
				});
			}
			if (!value1.m_bNotNull || !value2.m_bNotNull)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleIntervalDS::Operator +\n"
					});
				}
				return OracleIntervalDS.Null;
			}
			int num = 0;
			OpoITLValCtx* ctx = null;
			try
			{
				num = OpsITL.Add(value1.m_opoIDSCtx.m_pValCtx, value2.m_opoIDSCtx.m_pValCtx, out ctx);
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
							" (EXIT)  OracleIntervalDS::Operator +()\n"
						});
					}
					ITLMethods.FreeCtx(ref ctx);
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
					" (EXIT)  OracleIntervalDS::Operator +()\n"
				});
			}
			return new OracleIntervalDS(ctx, 9, 9);
		}

		// Token: 0x06000685 RID: 1669 RVA: 0x000438EC File Offset: 0x000428EC
		public unsafe static OracleIntervalDS operator -(OracleIntervalDS value1, OracleIntervalDS value2)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleIntervalDS::Operator -\n"
				});
			}
			if (!value1.m_bNotNull || !value2.m_bNotNull)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleIntervalDS::Operator -\n"
					});
				}
				return OracleIntervalDS.Null;
			}
			int num = 0;
			OpoITLValCtx* ctx = null;
			try
			{
				num = OpsITL.Subtract(value1.m_opoIDSCtx.m_pValCtx, value2.m_opoIDSCtx.m_pValCtx, out ctx);
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
							" (EXIT)  OracleIntervalDS::Operator -()\n"
						});
					}
					ITLMethods.FreeCtx(ref ctx);
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
					" (EXIT)  OracleIntervalDS::Operator -()\n"
				});
			}
			return new OracleIntervalDS(ctx, 9, 9);
		}

		// Token: 0x06000686 RID: 1670 RVA: 0x00043A14 File Offset: 0x00042A14
		public unsafe static OracleIntervalDS operator -(OracleIntervalDS value1)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleIntervalDS::Operator Negate\n"
				});
			}
			if (!value1.m_bNotNull)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleIntervalDS::Operator Negate\n"
					});
				}
				return OracleIntervalDS.Null;
			}
			int num = 0;
			OpoITLValCtx* ctx = null;
			try
			{
				num = OpsIDS.Negate(value1.m_opoIDSCtx.m_pValCtx, out ctx);
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
							" (EXIT)  OracleIntervalDS::Operator Negate\n"
						});
					}
					ITLMethods.FreeCtx(ref ctx);
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
					" (EXIT)  OracleIntervalDS::Operator Negate\n"
				});
			}
			return new OracleIntervalDS(ctx, 9, 9);
		}

		// Token: 0x06000687 RID: 1671 RVA: 0x00043B28 File Offset: 0x00042B28
		public unsafe static OracleIntervalDS operator *(OracleIntervalDS value1, int multiplier)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleIntervalDS::Operator *\n"
				});
			}
			if (!value1.m_bNotNull)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleIntervalDS::Operator *\n"
					});
				}
				return OracleIntervalDS.Null;
			}
			int num = 0;
			OpoITLValCtx* ctx = null;
			try
			{
				num = OpsITL.Multiply(value1.m_opoIDSCtx.m_pValCtx, multiplier, out ctx);
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
							" (EXIT)  OracleIntervalDS::Operator *\n"
						});
					}
					ITLMethods.FreeCtx(ref ctx);
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
					" (EXIT)  OracleIntervalDS::Operator *\n"
				});
			}
			return new OracleIntervalDS(ctx, 9, 9);
		}

		// Token: 0x06000688 RID: 1672 RVA: 0x00043C3C File Offset: 0x00042C3C
		public unsafe static OracleIntervalDS operator /(OracleIntervalDS value1, int divisor)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleIntervalDS::Operator /\n"
				});
			}
			if (!value1.m_bNotNull)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleIntervalDS::Operator /\n"
					});
				}
				return OracleIntervalDS.Null;
			}
			if (divisor == 0)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleIntervalDS::Operator /\n"
					});
				}
				throw new DivideByZeroException();
			}
			int num = 0;
			OpoITLValCtx* ctx = null;
			try
			{
				num = OpsITL.Divide(value1.m_opoIDSCtx.m_pValCtx, divisor, out ctx);
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
							" (EXIT)  OracleIntervalDS::Operator /\n"
						});
					}
					ITLMethods.FreeCtx(ref ctx);
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
					" (EXIT)  OracleIntervalDS::Operator /\n"
				});
			}
			return new OracleIntervalDS(ctx, 9, 9);
		}

		// Token: 0x06000689 RID: 1673 RVA: 0x00043D78 File Offset: 0x00042D78
		public static explicit operator OracleIntervalDS(string intervalStr)
		{
			return new OracleIntervalDS(intervalStr);
		}

		// Token: 0x0600068A RID: 1674 RVA: 0x00043D80 File Offset: 0x00042D80
		public static explicit operator TimeSpan(OracleIntervalDS value1)
		{
			if (!value1.m_bNotNull)
			{
				throw new OracleNullValueException();
			}
			decimal num = TimeSpanConv.ValCtxToTicks(value1.m_opoIDSCtx.m_pValCtx);
			if (num < -9223372036854775808m || num > 9223372036854775807m)
			{
				throw new OracleTypeException(ErrRes.TYP_GETDOTNETTYPE_FAIL, new object[0]);
			}
			return new TimeSpan((long)num);
		}

		// Token: 0x0600068B RID: 1675 RVA: 0x00043DF6 File Offset: 0x00042DF6
		public static implicit operator OracleIntervalDS(TimeSpan value1)
		{
			return new OracleIntervalDS(value1);
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x0600068C RID: 1676 RVA: 0x00043E00 File Offset: 0x00042E00
		public byte[] BinData
		{
			get
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (ENTRY) OracleIntervalDS::BinData: get\n"
					});
				}
				int num = 0;
				if (!this.m_bNotNull)
				{
					throw new OracleNullValueException();
				}
				byte[] array = new byte[11];
				try
				{
					num = OpsIDS.ToBytes(this.m_opoIDSCtx.m_pValCtx, array);
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
						" (EXIT)  OracleIntervalDS::BinData: get\n"
					});
				}
				return array;
			}
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x0600068D RID: 1677 RVA: 0x00043EA8 File Offset: 0x00042EA8
		public bool IsNull
		{
			get
			{
				return !this.m_bNotNull;
			}
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x0600068E RID: 1678 RVA: 0x00043EB3 File Offset: 0x00042EB3
		public TimeSpan Value
		{
			get
			{
				if (this.m_bNotNull)
				{
					return TimeSpanConv.GetTimeSpan(this.m_opoIDSCtx.m_pValCtx, OracleDbType.IntervalDS);
				}
				throw new OracleNullValueException();
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x0600068F RID: 1679 RVA: 0x00043ED5 File Offset: 0x00042ED5
		public int Days
		{
			get
			{
				if (this.m_bNotNull)
				{
					return this.GetIDSData(2);
				}
				throw new OracleNullValueException();
			}
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x06000690 RID: 1680 RVA: 0x00043EEC File Offset: 0x00042EEC
		public int Hours
		{
			get
			{
				if (this.m_bNotNull)
				{
					return this.GetIDSData(3);
				}
				throw new OracleNullValueException();
			}
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x06000691 RID: 1681 RVA: 0x00043F03 File Offset: 0x00042F03
		public int Minutes
		{
			get
			{
				if (this.m_bNotNull)
				{
					return this.GetIDSData(4);
				}
				throw new OracleNullValueException();
			}
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x06000692 RID: 1682 RVA: 0x00043F1A File Offset: 0x00042F1A
		public int Seconds
		{
			get
			{
				if (this.m_bNotNull)
				{
					return this.GetIDSData(5);
				}
				throw new OracleNullValueException();
			}
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x06000693 RID: 1683 RVA: 0x00043F31 File Offset: 0x00042F31
		public double Milliseconds
		{
			get
			{
				if (this.m_bNotNull)
				{
					return (double)(this.GetIDSData(7) / 1000000);
				}
				throw new OracleNullValueException();
			}
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x06000694 RID: 1684 RVA: 0x00043F4F File Offset: 0x00042F4F
		public int Nanoseconds
		{
			get
			{
				if (this.m_bNotNull)
				{
					return this.GetIDSData(7);
				}
				throw new OracleNullValueException();
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x06000695 RID: 1685 RVA: 0x00043F68 File Offset: 0x00042F68
		public unsafe double TotalDays
		{
			get
			{
				if (this.m_bNotNull)
				{
					double result = 0.0;
					try
					{
						OpsIDS.ToDays(this.m_opoIDSCtx.m_pValCtx, &result);
					}
					catch (Exception ex)
					{
						if (OraTrace.m_TraceLevel != 0U)
						{
							OraTrace.TraceExceptionInfo(ex);
						}
						throw;
					}
					return result;
				}
				throw new OracleNullValueException();
			}
		}

		// Token: 0x06000696 RID: 1686 RVA: 0x00043FC4 File Offset: 0x00042FC4
		internal IntPtr GetOCIInterval()
		{
			IntPtr zero = IntPtr.Zero;
			OracleConnection internalConnection = OracleConnection.GetInternalConnection();
			int num = OpsIDS.AllocOCIFromValCtx(internalConnection.m_opoConCtx.opsConCtx, internalConnection.m_opoConCtx.opsErrCtx, this.GetValCtx(), out zero);
			if (num != 0)
			{
				throw new OracleException(num, string.Empty, string.Empty, string.Empty);
			}
			return zero;
		}

		// Token: 0x06000697 RID: 1687 RVA: 0x00044020 File Offset: 0x00043020
		public int CompareTo(object obj)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleIntervalDS::CompareTo()\n"
				});
			}
			if (obj.GetType() != typeof(OracleIntervalDS))
			{
				throw new ArgumentException();
			}
			OracleIntervalDS oracleIntervalDS = (OracleIntervalDS)obj;
			CompareNullEnum compareNullEnum = InternalTypes.CompareNull(!this.m_bNotNull, !oracleIntervalDS.m_bNotNull);
			if (compareNullEnum == CompareNullEnum.BothNull)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleIntervalDS::CompareTo()\n"
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
						" (EXIT)  OracleIntervalDS::CompareTo()\n"
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
						" (EXIT)  OracleIntervalDS::CompareTo()\n"
					});
				}
				return 1;
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleIntervalDS::CompareTo()\n"
				});
			}
			return ITLMethods.Compare(this.m_opoIDSCtx.m_pValCtx, oracleIntervalDS.m_opoIDSCtx.m_pValCtx);
		}

		// Token: 0x06000698 RID: 1688 RVA: 0x00044134 File Offset: 0x00043134
		public override bool Equals(object obj)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleIntervalDS::Equals(2)\n"
				});
			}
			if (obj == null)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleIntervalDS::Equals()\n"
					});
				}
				return false;
			}
			if (obj.GetType() != typeof(OracleIntervalDS))
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleIntervalDS::Equals()\n"
					});
				}
				return false;
			}
			OracleIntervalDS value = (OracleIntervalDS)obj;
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleIntervalDS::Equals()\n"
				});
			}
			return OracleIntervalDS.Equals(this, value);
		}

		// Token: 0x06000699 RID: 1689 RVA: 0x000441E9 File Offset: 0x000431E9
		public override int GetHashCode()
		{
			if (this.m_bNotNull)
			{
				return this.m_opoIDSCtx.GetHashCode();
			}
			return 0;
		}

		// Token: 0x0600069A RID: 1690 RVA: 0x00044200 File Offset: 0x00043200
		public override string ToString()
		{
			if (this.m_bNotNull)
			{
				return ITLMethods.ToString(this.m_opoIDSCtx.m_pValCtx, this.m_dayPrec, this.m_fSecondPrec);
			}
			return "null";
		}

		// Token: 0x0600069B RID: 1691 RVA: 0x0004422C File Offset: 0x0004322C
		internal OracleIntervalDS(OpoIDSCtx ctx)
		{
			this.m_opoIDSCtx = ctx;
			this.m_bNotNull = true;
			this.m_fSecondPrec = 9;
			this.m_dayPrec = 9;
		}

		// Token: 0x0600069C RID: 1692 RVA: 0x0004424C File Offset: 0x0004324C
		internal unsafe OracleIntervalDS(OpoITLValCtx* ctx)
		{
			this = new OracleIntervalDS(ctx, 9, 9);
		}

		// Token: 0x0600069D RID: 1693 RVA: 0x00044259 File Offset: 0x00043259
		internal unsafe OracleIntervalDS(OpoITLValCtx* ctx, int dayPrec, int fSecondPrec)
		{
			this.m_opoIDSCtx = new OpoIDSCtx(ctx);
			this.m_bNotNull = true;
			this.m_fSecondPrec = fSecondPrec;
			this.m_dayPrec = dayPrec;
		}

		// Token: 0x0600069E RID: 1694 RVA: 0x0004427C File Offset: 0x0004327C
		internal unsafe static OpoITLValCtx* AllocValCtxFromData(TimeSpan data)
		{
			int num = 0;
			OpoITLValCtx* ptr = null;
			try
			{
				num = OpsIDS.AllocValCtx(ref ptr);
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
			OracleIntervalDS.FillValCtxFromTimeSpan(ptr, data);
			return ptr;
		}

		// Token: 0x0600069F RID: 1695 RVA: 0x000442D0 File Offset: 0x000432D0
		internal unsafe static void FillValCtxFromTimeSpan(OpoITLValCtx* pValCtx, TimeSpan ts)
		{
			long num = ts.Ticks - (long)ts.Days * 864000000000L - (long)ts.Hours * 36000000000L - (long)ts.Minutes * 600000000L - (long)ts.Seconds * 10000000L;
			pValCtx->m_type = 10;
			pValCtx->m_ds.m_days = ts.Days;
			pValCtx->m_ds.m_hours = ts.Hours;
			pValCtx->m_ds.m_minutes = ts.Minutes;
			pValCtx->m_ds.m_seconds = ts.Seconds;
			pValCtx->m_ds.m_fSeconds = (int)(num * 100L);
		}

		// Token: 0x060006A0 RID: 1696 RVA: 0x0004438C File Offset: 0x0004338C
		internal unsafe OpoITLValCtx* GetValCtx()
		{
			return this.m_opoIDSCtx.m_pValCtx;
		}

		// Token: 0x060006A1 RID: 1697 RVA: 0x0004439C File Offset: 0x0004339C
		internal IntPtr DupValCtx()
		{
			IntPtr zero = IntPtr.Zero;
			int num = OpsIDS.DupValCtx(this.GetValCtx(), out zero);
			if (num != 0)
			{
				throw new OracleException(num, string.Empty, string.Empty, string.Empty);
			}
			return zero;
		}

		// Token: 0x060006A2 RID: 1698 RVA: 0x000443DC File Offset: 0x000433DC
		internal unsafe int GetIDSData(byte idsComponent)
		{
			int result = 0;
			switch (idsComponent)
			{
			case 2:
				result = this.m_opoIDSCtx.m_pValCtx->m_ds.m_days;
				break;
			case 3:
				result = this.m_opoIDSCtx.m_pValCtx->m_ds.m_hours;
				break;
			case 4:
				result = this.m_opoIDSCtx.m_pValCtx->m_ds.m_minutes;
				break;
			case 5:
				result = this.m_opoIDSCtx.m_pValCtx->m_ds.m_seconds;
				break;
			case 7:
				result = this.m_opoIDSCtx.m_pValCtx->m_ds.m_fSeconds;
				break;
			}
			return result;
		}

		// Token: 0x040003F1 RID: 1009
		internal const short MaxArrSize = 11;

		// Token: 0x040003F2 RID: 1010
		internal const byte IDSType = 10;

		// Token: 0x040003F3 RID: 1011
		internal const byte DAY = 2;

		// Token: 0x040003F4 RID: 1012
		internal const byte HOUR = 3;

		// Token: 0x040003F5 RID: 1013
		internal const byte MINUTE = 4;

		// Token: 0x040003F6 RID: 1014
		internal const byte SECOND = 5;

		// Token: 0x040003F7 RID: 1015
		internal const byte MILLISECOND = 6;

		// Token: 0x040003F8 RID: 1016
		internal const byte FSECOND = 7;

		// Token: 0x040003F9 RID: 1017
		internal const byte MaxStrLen = 255;

		// Token: 0x040003FA RID: 1018
		public static readonly OracleIntervalDS MaxValue = new OracleIntervalDS(999999999, 23, 59, 59, 999999999);

		// Token: 0x040003FB RID: 1019
		public static readonly OracleIntervalDS MinValue = new OracleIntervalDS(-999999999, -23, -59, -59, -999999999);

		// Token: 0x040003FC RID: 1020
		public static readonly OracleIntervalDS Null;

		// Token: 0x040003FD RID: 1021
		public static readonly OracleIntervalDS Zero = new OracleIntervalDS(0, 0, 0, 0, 0);

		// Token: 0x040003FE RID: 1022
		private OpoIDSCtx m_opoIDSCtx;

		// Token: 0x040003FF RID: 1023
		private bool m_bNotNull;

		// Token: 0x04000400 RID: 1024
		private int m_dayPrec;

		// Token: 0x04000401 RID: 1025
		private int m_fSecondPrec;
	}
}
