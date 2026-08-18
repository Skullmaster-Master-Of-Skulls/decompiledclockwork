using System;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Oracle.DataAccess.Client;

namespace Oracle.DataAccess.Types
{
	// Token: 0x0200001E RID: 30
	[XmlSchemaProvider("GetXsdType")]
	[Serializable]
	public struct OracleIntervalYM : IComparable, INullable, IXmlSerializable
	{
		// Token: 0x060000FA RID: 250 RVA: 0x000102AF File Offset: 0x0000F2AF
		static OracleIntervalYM()
		{
			if (!OracleInit.bSetDllDirectoryInvoked)
			{
				OracleInit.Initialize();
			}
		}

		// Token: 0x060000FB RID: 251 RVA: 0x000102EB File Offset: 0x0000F2EB
		public static XmlQualifiedName GetXsdType(XmlSchemaSet schemaSet)
		{
			return new XmlQualifiedName("string", "http://www.w3.org/2001/XMLSchema");
		}

		// Token: 0x060000FC RID: 252 RVA: 0x000102FC File Offset: 0x0000F2FC
		XmlSchema IXmlSerializable.GetSchema()
		{
			return null;
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00010300 File Offset: 0x0000F300
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			string text = reader.GetAttribute("null", "http://www.w3.org/2001/XMLSchema-instance");
			if (text != null && XmlConvert.ToBoolean(text))
			{
				this.m_bNotNull = false;
				return;
			}
			text = reader.ReadElementString();
			this.m_opoIYMCtx = new OpoIYMCtx(text);
			if (this.m_opoIYMCtx.m_error != 0)
			{
				throw new ArgumentException(OracleTypeException.GetTypeMsg(this.m_opoIYMCtx.m_error, new object[0]));
			}
			this.m_yearPrec = 9;
			this.m_bNotNull = true;
		}

		// Token: 0x060000FE RID: 254 RVA: 0x0001037C File Offset: 0x0000F37C
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			if (this.m_bNotNull)
			{
				writer.WriteString(ITLMethods.ToString(this.m_opoIYMCtx.m_pValCtx, this.m_yearPrec, 0));
				return;
			}
			writer.WriteAttributeString("xsi", "null", "http://www.w3.org/2001/XMLSchema-instance", "true");
		}

		// Token: 0x060000FF RID: 255 RVA: 0x000103CC File Offset: 0x0000F3CC
		public OracleIntervalYM(int years, int months)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleIntervalYM::OracleIntervalYM(1)\n"
				});
			}
			if (!Interval.IsValidYM(years, months))
			{
				throw new ArgumentOutOfRangeException();
			}
			this.m_opoIYMCtx = new OpoIYMCtx(years, months);
			if (this.m_opoIYMCtx.m_error != 0)
			{
				throw new ArgumentException(OracleTypeException.GetTypeMsg(this.m_opoIYMCtx.m_error, new object[0]));
			}
			this.m_bNotNull = true;
			this.m_yearPrec = 9;
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleIntervalYM::OracleIntervalYM(1)\n"
				});
			}
		}

		// Token: 0x06000100 RID: 256 RVA: 0x0001046C File Offset: 0x0000F46C
		public OracleIntervalYM(long totalMonths)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleIntervalYM::OracleIntervalYM(2)\n"
				});
			}
			if (Interval.IsValidYMMonths(totalMonths))
			{
				int years = (int)(totalMonths / 12L);
				int months = (int)(totalMonths % 12L);
				this.m_opoIYMCtx = new OpoIYMCtx(years, months);
				this.m_bNotNull = true;
				this.m_yearPrec = 9;
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleIntervalYM::OracleIntervalYM(2)\n"
					});
				}
				return;
			}
			throw new ArgumentOutOfRangeException();
		}

		// Token: 0x06000101 RID: 257 RVA: 0x000104F0 File Offset: 0x0000F4F0
		public OracleIntervalYM(double totalYears)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleIntervalYM::OracleIntervalYM(3)\n"
				});
			}
			if (!Interval.IsValidYMYears(totalYears))
			{
				throw new ArgumentOutOfRangeException();
			}
			this.m_opoIYMCtx = new OpoIYMCtx(totalYears);
			if (this.m_opoIYMCtx.m_error != 0)
			{
				throw new ArgumentException(OracleTypeException.GetTypeMsg(this.m_opoIYMCtx.m_error, new object[0]));
			}
			this.m_bNotNull = true;
			this.m_yearPrec = 9;
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleIntervalYM::OracleIntervalYM(3)\n"
				});
			}
		}

		// Token: 0x06000102 RID: 258 RVA: 0x0001058C File Offset: 0x0000F58C
		public OracleIntervalYM(string intervalStr)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleIntervalYM::OracleIntervalYM(4)\n"
				});
			}
			if (intervalStr == null)
			{
				throw new ArgumentNullException();
			}
			this.m_opoIYMCtx = new OpoIYMCtx(intervalStr);
			if (this.m_opoIYMCtx.m_error != 0)
			{
				throw new ArgumentException(OracleTypeException.GetTypeMsg(this.m_opoIYMCtx.m_error, new object[0]));
			}
			this.m_bNotNull = true;
			this.m_yearPrec = 9;
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleIntervalYM::OracleIntervalYM(4)\n"
				});
			}
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00010620 File Offset: 0x0000F620
		public OracleIntervalYM(byte[] binData)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleIntervalYM::OracleIntervalYM(5)\n"
				});
			}
			if (binData == null)
			{
				throw new ArgumentNullException();
			}
			this.m_opoIYMCtx = new OpoIYMCtx(binData);
			if (this.m_opoIYMCtx.m_error != 0)
			{
				throw new ArgumentException(OracleTypeException.GetTypeMsg(this.m_opoIYMCtx.m_error, new object[0]));
			}
			this.m_bNotNull = true;
			this.m_yearPrec = 9;
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleIntervalYM::OracleIntervalYM()\n"
				});
			}
		}

		// Token: 0x06000104 RID: 260 RVA: 0x000106B4 File Offset: 0x0000F6B4
		internal unsafe OracleIntervalYM(IntPtr ociInterval)
		{
			OracleConnection internalConnection = OracleConnection.GetInternalConnection();
			OpoITLValCtx* ctx = null;
			int num = OpsIYM.AllocValCtxFromOCI(internalConnection.m_opoConCtx.opsConCtx, internalConnection.m_opoConCtx.opsErrCtx, ociInterval, out ctx);
			if (num != 0)
			{
				throw new OracleException(num, string.Empty, string.Empty, string.Empty);
			}
			this.m_opoIYMCtx = new OpoIYMCtx(ctx);
			this.m_bNotNull = true;
			this.m_yearPrec = 9;
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00010720 File Offset: 0x0000F720
		public static bool Equals(OracleIntervalYM value1, OracleIntervalYM value2)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleIntervalYM::Equals(1)\n"
				});
			}
			CompareNullEnum compareNullEnum = InternalTypes.CompareNull(!value1.m_bNotNull, !value2.m_bNotNull);
			if (compareNullEnum == CompareNullEnum.BothNull)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleIntervalYM::Equals(1)\n"
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
						" (EXIT)  OracleIntervalYM::Equals(1)\n"
					});
				}
				return false;
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleIntervalYM::Equals(1)\n"
				});
			}
			return ITLMethods.Compare(value1.m_opoIYMCtx.m_pValCtx, value2.m_opoIYMCtx.m_pValCtx) == 0;
		}

		// Token: 0x06000106 RID: 262 RVA: 0x000107EC File Offset: 0x0000F7EC
		public static bool GreaterThan(OracleIntervalYM value1, OracleIntervalYM value2)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleIntervalYM::GreaterThan()\n"
				});
			}
			CompareNullEnum compareNullEnum = InternalTypes.CompareNull(!value1.m_bNotNull, !value2.m_bNotNull);
			if (compareNullEnum == CompareNullEnum.BothNull)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleIntervalYM::GreaterThan()\n"
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
						" (EXIT)  OracleIntervalYM::GreaterThan()\n"
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
						" (EXIT)  OracleIntervalYM::GreaterThan()\n"
					});
				}
				return true;
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleIntervalYM::GreaterThan()\n"
				});
			}
			return ITLMethods.Compare(value1.m_opoIYMCtx.m_pValCtx, value2.m_opoIYMCtx.m_pValCtx) > 0;
		}

		// Token: 0x06000107 RID: 263 RVA: 0x000108E0 File Offset: 0x0000F8E0
		public static bool GreaterThanOrEqual(OracleIntervalYM value1, OracleIntervalYM value2)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleIntervalYM::GreaterThanOrEqual()\n"
				});
			}
			CompareNullEnum compareNullEnum = InternalTypes.CompareNull(!value1.m_bNotNull, !value2.m_bNotNull);
			if (compareNullEnum == CompareNullEnum.BothNull)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleIntervalYM::GreaterThanOrEqual()\n"
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
						" (EXIT)  OracleIntervalYM::GreaterThanOrEqual()\n"
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
						" (EXIT)  OracleIntervalYM::GreaterThanOrEqual()\n"
					});
				}
				return true;
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleIntervalYM::GreaterThanOrEqual()\n"
				});
			}
			return ITLMethods.Compare(value1.m_opoIYMCtx.m_pValCtx, value2.m_opoIYMCtx.m_pValCtx) >= 0;
		}

		// Token: 0x06000108 RID: 264 RVA: 0x000109D4 File Offset: 0x0000F9D4
		public static bool LessThan(OracleIntervalYM value1, OracleIntervalYM value2)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleIntervalYM::LessThan()\n"
				});
			}
			CompareNullEnum compareNullEnum = InternalTypes.CompareNull(!value1.m_bNotNull, !value2.m_bNotNull);
			if (compareNullEnum == CompareNullEnum.BothNull)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleIntervalYM::LessThan()\n"
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
						" (EXIT)  OracleIntervalYM::LessThan()\n"
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
						" (EXIT)  OracleIntervalYM::LessThan()\n"
					});
				}
				return false;
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleIntervalYM::LessThan()\n"
				});
			}
			return ITLMethods.Compare(value1.m_opoIYMCtx.m_pValCtx, value2.m_opoIYMCtx.m_pValCtx) < 0;
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00010AC8 File Offset: 0x0000FAC8
		public static bool LessThanOrEqual(OracleIntervalYM value1, OracleIntervalYM value2)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleIntervalYM::LessThanOrEqual()\n"
				});
			}
			CompareNullEnum compareNullEnum = InternalTypes.CompareNull(!value1.m_bNotNull, !value2.m_bNotNull);
			if (compareNullEnum == CompareNullEnum.BothNull)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleIntervalYM::LessThanOrEqual()\n"
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
						" (EXIT)  OracleIntervalYM::LessThanOrEqual()\n"
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
						" (EXIT)  OracleIntervalYM::LessThanOrEqual()\n"
					});
				}
				return false;
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleIntervalYM::LessThanOrEqual()\n"
				});
			}
			return ITLMethods.Compare(value1.m_opoIYMCtx.m_pValCtx, value2.m_opoIYMCtx.m_pValCtx) <= 0;
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00010BBB File Offset: 0x0000FBBB
		public static bool NotEquals(OracleIntervalYM value1, OracleIntervalYM value2)
		{
			return !OracleIntervalYM.Equals(value1, value2);
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00010BC7 File Offset: 0x0000FBC7
		public static OracleIntervalYM Parse(string intervalStr)
		{
			return new OracleIntervalYM(intervalStr);
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00010BD0 File Offset: 0x0000FBD0
		public unsafe static OracleIntervalYM SetPrecision(OracleIntervalYM value1, int yearPrecision)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleIntervalYM::SetPrecision()\n"
				});
			}
			if (!value1.m_bNotNull)
			{
				throw new OracleNullValueException();
			}
			int num = 0;
			if (yearPrecision < 0 || yearPrecision > 9)
			{
				throw new ArgumentOutOfRangeException("yearPrecision");
			}
			OpoITLValCtx* ctx = null;
			try
			{
				num = OpsIYM.AllocValCtxFromBytes(value1.BinData, out ctx, yearPrecision);
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
							" (EXIT)  OracleIntervalYM::SetPrecision()\n"
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
					" (EXIT)  OracleIntervalYM::SetPrecision()\n"
				});
			}
			return new OracleIntervalYM(ctx, yearPrecision);
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00010CD0 File Offset: 0x0000FCD0
		public static bool operator ==(OracleIntervalYM value1, OracleIntervalYM value2)
		{
			return OracleIntervalYM.Equals(value1, value2);
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00010CD9 File Offset: 0x0000FCD9
		public static bool operator >(OracleIntervalYM value1, OracleIntervalYM value2)
		{
			return OracleIntervalYM.GreaterThan(value1, value2);
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00010CE2 File Offset: 0x0000FCE2
		public static bool operator >=(OracleIntervalYM value1, OracleIntervalYM value2)
		{
			return OracleIntervalYM.GreaterThanOrEqual(value1, value2);
		}

		// Token: 0x06000110 RID: 272 RVA: 0x00010CEB File Offset: 0x0000FCEB
		public static bool operator <(OracleIntervalYM value1, OracleIntervalYM value2)
		{
			return OracleIntervalYM.LessThan(value1, value2);
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00010CF4 File Offset: 0x0000FCF4
		public static bool operator <=(OracleIntervalYM value1, OracleIntervalYM value2)
		{
			return OracleIntervalYM.LessThanOrEqual(value1, value2);
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00010CFD File Offset: 0x0000FCFD
		public static bool operator !=(OracleIntervalYM value1, OracleIntervalYM value2)
		{
			return OracleIntervalYM.NotEquals(value1, value2);
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00010D08 File Offset: 0x0000FD08
		public unsafe static OracleIntervalYM operator +(OracleIntervalYM value1, OracleIntervalYM value2)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleIntervalYM::Operator +\n"
				});
			}
			if (!value1.m_bNotNull || !value2.m_bNotNull)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleIntervalYM::Operator +\n"
					});
				}
				return OracleIntervalYM.Null;
			}
			int num = 0;
			OpoITLValCtx* ctx = null;
			try
			{
				num = OpsITL.Add(value1.m_opoIYMCtx.m_pValCtx, value2.m_opoIYMCtx.m_pValCtx, out ctx);
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
							" (EXIT)  OracleIntervalYM::Operator +\n"
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
					" (EXIT)  OracleIntervalYM::Operator +\n"
				});
			}
			return new OracleIntervalYM(ctx, 9);
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00010E30 File Offset: 0x0000FE30
		public unsafe static OracleIntervalYM operator -(OracleIntervalYM value1, OracleIntervalYM value2)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleIntervalYM::Operator -\n"
				});
			}
			if (!value1.m_bNotNull || !value2.m_bNotNull)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleIntervalYM::Operator -\n"
					});
				}
				return OracleIntervalYM.Null;
			}
			int num = 0;
			OpoITLValCtx* ctx = null;
			try
			{
				num = OpsITL.Subtract(value1.m_opoIYMCtx.m_pValCtx, value2.m_opoIYMCtx.m_pValCtx, out ctx);
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
							" (EXIT)  OracleIntervalYM::Operator -\n"
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
					" (EXIT)  OracleIntervalYM::Operator -\n"
				});
			}
			return new OracleIntervalYM(ctx, 9);
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00010F58 File Offset: 0x0000FF58
		public unsafe static OracleIntervalYM operator -(OracleIntervalYM value1)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleIntervalYM::Operator Negate\n"
				});
			}
			if (!value1.m_bNotNull)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleIntervalYM::Operator Negate\n"
					});
				}
				return OracleIntervalYM.Null;
			}
			int num = 0;
			OpoITLValCtx* ctx = null;
			try
			{
				num = OpsIYM.Negate(value1.m_opoIYMCtx.m_pValCtx, out ctx);
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
							" (EXIT)  OracleIntervalYM::Operator Negate\n"
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
					" (EXIT)  OracleIntervalYM::Operator Negate\n"
				});
			}
			return new OracleIntervalYM(ctx, 9);
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00011068 File Offset: 0x00010068
		public unsafe static OracleIntervalYM operator *(OracleIntervalYM value1, int multiplier)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleIntervalYM::Operator *\n"
				});
			}
			if (!value1.m_bNotNull)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleIntervalYM::Operator *\n"
					});
				}
				return OracleIntervalYM.Null;
			}
			int num = 0;
			OpoITLValCtx* ctx = null;
			try
			{
				num = OpsITL.Multiply(value1.m_opoIYMCtx.m_pValCtx, multiplier, out ctx);
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
							" (EXIT)  OracleIntervalYM::Operator *\n"
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
					" (EXIT)  OracleIntervalYM::Operator *\n"
				});
			}
			return new OracleIntervalYM(ctx, 9);
		}

		// Token: 0x06000117 RID: 279 RVA: 0x0001117C File Offset: 0x0001017C
		public unsafe static OracleIntervalYM operator /(OracleIntervalYM value1, int divisor)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleIntervalYM::Operator /\n"
				});
			}
			if (!value1.m_bNotNull)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleIntervalYM::Operator /\n"
					});
				}
				return OracleIntervalYM.Null;
			}
			if (divisor == 0)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleIntervalYM::Operator /\n"
					});
				}
				throw new DivideByZeroException();
			}
			int num = 0;
			OpoITLValCtx* ctx = null;
			try
			{
				num = OpsITL.Divide(value1.m_opoIYMCtx.m_pValCtx, divisor, out ctx);
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
							" (EXIT)  OracleIntervalYM::Operator /\n"
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
					" (EXIT)  OracleIntervalYM::Operator /\n"
				});
			}
			return new OracleIntervalYM(ctx, 9);
		}

		// Token: 0x06000118 RID: 280 RVA: 0x000112B8 File Offset: 0x000102B8
		public static explicit operator OracleIntervalYM(string intervalStr)
		{
			return new OracleIntervalYM(intervalStr);
		}

		// Token: 0x06000119 RID: 281 RVA: 0x000112C0 File Offset: 0x000102C0
		public unsafe static explicit operator long(OracleIntervalYM value1)
		{
			if (value1.m_bNotNull)
			{
				OpoITLValCtx* pValCtx = value1.m_opoIYMCtx.m_pValCtx;
				return (long)pValCtx->m_ym.m_years * 12L + (long)pValCtx->m_ym.m_months;
			}
			throw new OracleNullValueException();
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00011308 File Offset: 0x00010308
		public static implicit operator OracleIntervalYM(long months)
		{
			return new OracleIntervalYM(months);
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600011B RID: 283 RVA: 0x00011310 File Offset: 0x00010310
		public byte[] BinData
		{
			get
			{
				int num = 0;
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (ENTRY) OracleIntervalYM::BinData: get\n"
					});
				}
				if (!this.m_bNotNull)
				{
					throw new OracleNullValueException();
				}
				byte[] array = new byte[5];
				try
				{
					num = OpsIYM.ToBytes(this.m_opoIYMCtx.m_pValCtx, array);
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
						" (EXIT)  OracleIntervalYM::BinData: get\n"
					});
				}
				return array;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600011C RID: 284 RVA: 0x000113B8 File Offset: 0x000103B8
		public bool IsNull
		{
			get
			{
				return !this.m_bNotNull;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x0600011D RID: 285 RVA: 0x000113C4 File Offset: 0x000103C4
		public long Value
		{
			get
			{
				if (this.m_bNotNull)
				{
					return LongConv.GetLong(this.m_opoIYMCtx.m_pValCtx, OracleDbType.IntervalYM);
				}
				OracleNullValueException ex = new OracleNullValueException();
				throw ex;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x0600011E RID: 286 RVA: 0x000113F3 File Offset: 0x000103F3
		public int Years
		{
			get
			{
				if (this.m_bNotNull)
				{
					return this.GetIYMData(0);
				}
				throw new OracleNullValueException();
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x0600011F RID: 287 RVA: 0x0001140A File Offset: 0x0001040A
		public int Months
		{
			get
			{
				if (this.m_bNotNull)
				{
					return this.GetIYMData(1);
				}
				throw new OracleNullValueException();
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000120 RID: 288 RVA: 0x00011424 File Offset: 0x00010424
		public unsafe double TotalYears
		{
			get
			{
				int num = 0;
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (ENTRY) OracleIntervalYM::TotalYears: get\n"
					});
				}
				if (!this.m_bNotNull)
				{
					throw new OracleNullValueException();
				}
				double result = 0.0;
				try
				{
					num = OpsIYM.ToYears(this.m_opoIYMCtx.m_pValCtx, &result);
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
						" (EXIT)  OracleIntervalYM::TotalYears: get\n"
					});
				}
				return result;
			}
		}

		// Token: 0x06000121 RID: 289 RVA: 0x000114D0 File Offset: 0x000104D0
		internal IntPtr GetOCIInterval()
		{
			IntPtr zero = IntPtr.Zero;
			OracleConnection internalConnection = OracleConnection.GetInternalConnection();
			int num = OpsIYM.AllocOCIFromValCtx(internalConnection.m_opoConCtx.opsConCtx, internalConnection.m_opoConCtx.opsErrCtx, this.GetValCtx(), out zero);
			if (num != 0)
			{
				throw new OracleException(num, string.Empty, string.Empty, string.Empty);
			}
			return zero;
		}

		// Token: 0x06000122 RID: 290 RVA: 0x0001152C File Offset: 0x0001052C
		public int CompareTo(object obj)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleIntervalYM::CompareTo()\n"
				});
			}
			if (obj.GetType() != typeof(OracleIntervalYM))
			{
				throw new ArgumentException();
			}
			OracleIntervalYM oracleIntervalYM = (OracleIntervalYM)obj;
			CompareNullEnum compareNullEnum = InternalTypes.CompareNull(!this.m_bNotNull, !oracleIntervalYM.m_bNotNull);
			if (compareNullEnum == CompareNullEnum.BothNull)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleIntervalYM::CompareTo()\n"
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
						" (EXIT)  OracleIntervalYM::CompareTo()\n"
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
						" (EXIT)  OracleIntervalYM::CompareTo()\n"
					});
				}
				return 1;
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleIntervalYM::CompareTo()\n"
				});
			}
			return ITLMethods.Compare(this.m_opoIYMCtx.m_pValCtx, oracleIntervalYM.m_opoIYMCtx.m_pValCtx);
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00011640 File Offset: 0x00010640
		public override bool Equals(object obj)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleIntervalYM::Equals(2)\n"
				});
			}
			if (obj == null)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleIntervalYM::Equals(2)\n"
					});
				}
				return false;
			}
			if (obj.GetType() != typeof(OracleIntervalYM))
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleIntervalYM::Equals(2)\n"
					});
				}
				return false;
			}
			OracleIntervalYM value = (OracleIntervalYM)obj;
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleIntervalYM::Equals(2)\n"
				});
			}
			return OracleIntervalYM.Equals(this, value);
		}

		// Token: 0x06000124 RID: 292 RVA: 0x000116F5 File Offset: 0x000106F5
		public override int GetHashCode()
		{
			if (this.m_bNotNull)
			{
				return this.m_opoIYMCtx.GetHashCode();
			}
			return 0;
		}

		// Token: 0x06000125 RID: 293 RVA: 0x0001170C File Offset: 0x0001070C
		public override string ToString()
		{
			if (this.m_bNotNull)
			{
				return ITLMethods.ToString(this.m_opoIYMCtx.m_pValCtx, this.m_yearPrec, 0);
			}
			return "null";
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00011733 File Offset: 0x00010733
		internal OracleIntervalYM(OpoIYMCtx ctx)
		{
			this.m_opoIYMCtx = ctx;
			this.m_bNotNull = true;
			this.m_yearPrec = 9;
		}

		// Token: 0x06000127 RID: 295 RVA: 0x0001174B File Offset: 0x0001074B
		internal unsafe OracleIntervalYM(OpoITLValCtx* ctx)
		{
			this = new OracleIntervalYM(ctx, 9);
		}

		// Token: 0x06000128 RID: 296 RVA: 0x00011756 File Offset: 0x00010756
		internal unsafe OracleIntervalYM(OpoITLValCtx* ctx, int yearPrec)
		{
			this.m_opoIYMCtx = new OpoIYMCtx(ctx);
			this.m_bNotNull = true;
			this.m_yearPrec = yearPrec;
		}

		// Token: 0x06000129 RID: 297 RVA: 0x00011774 File Offset: 0x00010774
		internal unsafe static OpoITLValCtx* AllocValCtxFromData(long ymMonths)
		{
			int num = 0;
			if (!Interval.IsValidYMMonths(ymMonths))
			{
				throw new ArgumentException();
			}
			OpoITLValCtx* ptr = null;
			try
			{
				num = OpsIYM.AllocValCtx(ref ptr);
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
					ITLMethods.FreeCtx(ref ptr);
					if (num != ErrRes.INT_ERR)
					{
						throw new OracleTypeException(num, new object[0]);
					}
				}
			}
			ptr->m_type = 7;
			ptr->m_ym.m_years = (int)(ymMonths / 12L);
			ptr->m_ym.m_months = (int)(ymMonths % 12L);
			return ptr;
		}

		// Token: 0x0600012A RID: 298 RVA: 0x0001181C File Offset: 0x0001081C
		internal unsafe OpoITLValCtx* GetValCtx()
		{
			return this.m_opoIYMCtx.m_pValCtx;
		}

		// Token: 0x0600012B RID: 299 RVA: 0x0001182C File Offset: 0x0001082C
		internal IntPtr DupValCtx()
		{
			IntPtr zero = IntPtr.Zero;
			int num = OpsIYM.DupValCtx(this.GetValCtx(), out zero);
			if (num != 0)
			{
				throw new OracleException(num, string.Empty, string.Empty, string.Empty);
			}
			return zero;
		}

		// Token: 0x0600012C RID: 300 RVA: 0x0001186C File Offset: 0x0001086C
		internal unsafe int GetIYMData(byte iymComponent)
		{
			int result = 0;
			switch (iymComponent)
			{
			case 0:
				result = this.m_opoIYMCtx.m_pValCtx->m_ym.m_years;
				break;
			case 1:
				result = this.m_opoIYMCtx.m_pValCtx->m_ym.m_months;
				break;
			}
			return result;
		}

		// Token: 0x040000B3 RID: 179
		internal const short MaxArrSize = 5;

		// Token: 0x040000B4 RID: 180
		internal const byte IYMType = 7;

		// Token: 0x040000B5 RID: 181
		internal const byte YEAR = 0;

		// Token: 0x040000B6 RID: 182
		internal const byte MONTH = 1;

		// Token: 0x040000B7 RID: 183
		public static readonly OracleIntervalYM MaxValue = new OracleIntervalYM(999999999, 11);

		// Token: 0x040000B8 RID: 184
		public static readonly OracleIntervalYM MinValue = new OracleIntervalYM(-999999999, -11);

		// Token: 0x040000B9 RID: 185
		public static readonly OracleIntervalYM Zero = new OracleIntervalYM(0, 0);

		// Token: 0x040000BA RID: 186
		public static readonly OracleIntervalYM Null;

		// Token: 0x040000BB RID: 187
		private OpoIYMCtx m_opoIYMCtx;

		// Token: 0x040000BC RID: 188
		private bool m_bNotNull;

		// Token: 0x040000BD RID: 189
		private int m_yearPrec;
	}
}
