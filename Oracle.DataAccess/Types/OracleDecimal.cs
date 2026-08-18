using System;
using System.Data;
using System.Runtime.InteropServices;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Oracle.DataAccess.Client;

namespace Oracle.DataAccess.Types
{
	// Token: 0x0200011E RID: 286
	[XmlSchemaProvider("GetXsdType")]
	public struct OracleDecimal : IComparable, INullable, IXmlSerializable
	{
		// Token: 0x06000B78 RID: 2936 RVA: 0x000742B8 File Offset: 0x000732B8
		static OracleDecimal()
		{
			if (!OracleInit.bSetDllDirectoryInvoked)
			{
				OracleInit.Initialize();
			}
		}

		// Token: 0x06000B79 RID: 2937 RVA: 0x00074355 File Offset: 0x00073355
		public static XmlQualifiedName GetXsdType(XmlSchemaSet schemaSet)
		{
			return new XmlQualifiedName("decimal", "http://www.w3.org/2001/XMLSchema");
		}

		// Token: 0x06000B7A RID: 2938 RVA: 0x00074366 File Offset: 0x00073366
		XmlSchema IXmlSerializable.GetSchema()
		{
			return null;
		}

		// Token: 0x06000B7B RID: 2939 RVA: 0x0007436C File Offset: 0x0007336C
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			string text = reader.GetAttribute("null", "http://www.w3.org/2001/XMLSchema-instance");
			if (text != null && XmlConvert.ToBoolean(text))
			{
				this.m_bNotNull = false;
				return;
			}
			text = reader.ReadElementString();
			this.m_format = null;
			this.m_opoDecCtx = new OpoDecCtx(text, this.m_format, ref this.m_numberType, ref this.m_bPositive, ref this.m_bZero);
			this.m_bGetInfo = true;
			if (this.m_opoDecCtx.m_error == 0)
			{
				this.m_bNotNull = true;
				return;
			}
			if (this.m_opoDecCtx.m_error == 22053)
			{
				throw new OverflowException(OracleTypeException.GetTypeMsg(this.m_opoDecCtx.m_error, new object[0]));
			}
			throw new ArgumentException(OracleTypeException.GetTypeMsg(this.m_opoDecCtx.m_error, new object[]
			{
				"numStr"
			}));
		}

		// Token: 0x06000B7C RID: 2940 RVA: 0x00074444 File Offset: 0x00073444
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
				num = OpsDec.ToString(this.m_opoDecCtx.m_pValCtx, this.m_format, out text);
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
			text = text.TrimEnd(null);
			text = text.TrimStart(null);
			int length = text.Length;
			string text2 = new string('#', length);
			if (text2.Equals(text))
			{
				throw new OracleTypeException(22065, new object[0]);
			}
			writer.WriteString(text);
		}

		// Token: 0x06000B7D RID: 2941 RVA: 0x00074508 File Offset: 0x00073508
		internal OracleDecimal(IntPtr numCtx)
		{
			this = new OracleDecimal(numCtx, true);
		}

		// Token: 0x06000B7E RID: 2942 RVA: 0x00074514 File Offset: 0x00073514
		public OracleDecimal(byte[] bytes)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleDecimal::OracleDecimal(1)\n"
				});
			}
			if (bytes == null)
			{
				throw new ArgumentNullException();
			}
			if (bytes.Length != 22)
			{
				throw new ArgumentException();
			}
			IntPtr intPtr = Marshal.AllocCoTaskMem(22);
			Marshal.Copy(bytes, 0, intPtr, 22);
			this.m_opoDecCtx = new OpoDecCtx(intPtr, ref this.m_numberType, ref this.m_bPositive, ref this.m_bZero);
			this.m_bGetInfo = true;
			if (this.m_opoDecCtx.m_error != 0)
			{
				throw new ArgumentException(OracleTypeException.GetTypeMsg(this.m_opoDecCtx.m_error, new object[]
				{
					"bytes"
				}));
			}
			this.m_bNotNull = true;
			this.m_format = null;
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleDecimal::OracleDecimal(1)\n"
				});
			}
		}

		// Token: 0x06000B7F RID: 2943 RVA: 0x000745EC File Offset: 0x000735EC
		public OracleDecimal(int intX)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleDecimal::OracleDecimal(2)\n"
				});
			}
			this.m_opoDecCtx = new OpoDecCtx(intX, ref this.m_numberType, ref this.m_bPositive, ref this.m_bZero);
			this.m_bGetInfo = true;
			if (this.m_opoDecCtx.m_error == 0)
			{
				this.m_bNotNull = true;
				this.m_format = null;
				this.m_numberType = 1;
				if (intX > 0)
				{
					this.m_bPositive = true;
				}
				else
				{
					this.m_bPositive = false;
				}
				if (intX == 0)
				{
					this.m_bZero = true;
				}
				else
				{
					this.m_bZero = false;
				}
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleDecimal::OracleDecimal(2)\n"
					});
				}
				return;
			}
			if (this.m_opoDecCtx.m_error == 22053)
			{
				throw new OverflowException(OracleTypeException.GetTypeMsg(this.m_opoDecCtx.m_error, new object[0]));
			}
			throw new OracleTypeException(this.m_opoDecCtx.m_error, new object[]
			{
				"intX"
			});
		}

		// Token: 0x06000B80 RID: 2944 RVA: 0x000746F0 File Offset: 0x000736F0
		public OracleDecimal(long longX)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleDecimal::OracleDecimal(3)\n"
				});
			}
			this.m_opoDecCtx = new OpoDecCtx(longX, ref this.m_numberType, ref this.m_bPositive, ref this.m_bZero);
			this.m_bGetInfo = true;
			if (this.m_opoDecCtx.m_error == 0)
			{
				this.m_bNotNull = true;
				this.m_format = null;
				this.m_numberType = 1;
				if (longX > 0L)
				{
					this.m_bPositive = true;
				}
				else
				{
					this.m_bPositive = false;
				}
				if (longX == 0L)
				{
					this.m_bZero = true;
				}
				else
				{
					this.m_bZero = false;
				}
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleDecimal::OracleDecimal(3)\n"
					});
				}
				return;
			}
			if (this.m_opoDecCtx.m_error == 22053)
			{
				throw new OverflowException(OracleTypeException.GetTypeMsg(this.m_opoDecCtx.m_error, new object[0]));
			}
			throw new OracleTypeException(this.m_opoDecCtx.m_error, new object[]
			{
				"longX"
			});
		}

		// Token: 0x06000B81 RID: 2945 RVA: 0x000747F8 File Offset: 0x000737F8
		public OracleDecimal(float floatX)
		{
			int num = 0;
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleDecimal::OracleDecimal(4)\n"
				});
			}
			if (float.IsNaN(floatX))
			{
				this.m_opoDecCtx = null;
				this.m_bGetInfo = true;
				this.m_bNotNull = false;
				this.m_numberType = 0;
				this.m_bPositive = false;
				this.m_bZero = false;
				this.m_format = null;
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleDecimal::OracleDecimal(4)\n"
					});
				}
				return;
			}
			if (float.IsPositiveInfinity(floatX))
			{
				IntPtr zero = IntPtr.Zero;
				try
				{
					num = OpsDec.AllocValCtxForPosInf(out zero);
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
						OpoDecCtx.FreeCtx(ref zero);
						if (num != ErrRes.INT_ERR)
						{
							throw new OracleTypeException(num, new object[0]);
						}
					}
				}
				this.m_opoDecCtx = new OpoDecCtx(zero);
				this.m_bGetInfo = true;
				this.m_bNotNull = true;
				this.m_numberType = 3;
				this.m_bPositive = true;
				this.m_bZero = false;
				this.m_format = null;
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleDecimal::OracleDecimal(4)\n"
					});
				}
				return;
			}
			if (float.IsNegativeInfinity(floatX))
			{
				IntPtr zero2 = IntPtr.Zero;
				try
				{
					num = OpsDec.AllocValCtxForNegInf(out zero2);
				}
				catch (Exception ex2)
				{
					if (OraTrace.m_TraceLevel != 0U)
					{
						OraTrace.TraceExceptionInfo(ex2);
					}
					num = ErrRes.INT_ERR;
					throw;
				}
				finally
				{
					if (num != 0)
					{
						OpoDecCtx.FreeCtx(ref zero2);
						if (num != ErrRes.INT_ERR)
						{
							throw new OracleTypeException(num, new object[0]);
						}
					}
				}
				this.m_opoDecCtx = new OpoDecCtx(zero2);
				this.m_bGetInfo = true;
				this.m_bNotNull = true;
				this.m_numberType = 4;
				this.m_bPositive = false;
				this.m_bZero = false;
				this.m_format = null;
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleDecimal::OracleDecimal(4)\n"
					});
				}
				return;
			}
			this.m_opoDecCtx = new OpoDecCtx(floatX, ref this.m_numberType, ref this.m_bPositive, ref this.m_bZero);
			this.m_bGetInfo = true;
			if (this.m_opoDecCtx.m_error == 0)
			{
				this.m_bNotNull = true;
				this.m_format = null;
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleDecimal::OracleDecimal(4)\n"
					});
				}
				return;
			}
			if (this.m_opoDecCtx.m_error == 22053)
			{
				throw new OverflowException(OracleTypeException.GetTypeMsg(this.m_opoDecCtx.m_error, new object[0]));
			}
			throw new OracleTypeException(this.m_opoDecCtx.m_error, new object[]
			{
				"doubleX"
			});
		}

		// Token: 0x06000B82 RID: 2946 RVA: 0x00074AC0 File Offset: 0x00073AC0
		public OracleDecimal(double doubleX)
		{
			int num = 0;
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleDecimal::OracleDecimal(4)\n"
				});
			}
			if (double.IsNaN(doubleX))
			{
				this.m_opoDecCtx = null;
				this.m_bGetInfo = true;
				this.m_bNotNull = false;
				this.m_numberType = 0;
				this.m_bPositive = false;
				this.m_bZero = false;
				this.m_format = null;
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleDecimal::OracleDecimal(4)\n"
					});
				}
				return;
			}
			if (double.IsPositiveInfinity(doubleX))
			{
				IntPtr zero = IntPtr.Zero;
				try
				{
					num = OpsDec.AllocValCtxForPosInf(out zero);
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
						OpoDecCtx.FreeCtx(ref zero);
						if (num != ErrRes.INT_ERR)
						{
							throw new OracleTypeException(num, new object[0]);
						}
					}
				}
				this.m_opoDecCtx = new OpoDecCtx(zero);
				this.m_bGetInfo = true;
				this.m_bNotNull = true;
				this.m_numberType = 3;
				this.m_bPositive = true;
				this.m_bZero = false;
				this.m_format = null;
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleDecimal::OracleDecimal(4)\n"
					});
				}
				return;
			}
			if (double.IsNegativeInfinity(doubleX))
			{
				IntPtr zero2 = IntPtr.Zero;
				try
				{
					num = OpsDec.AllocValCtxForNegInf(out zero2);
				}
				catch (Exception ex2)
				{
					if (OraTrace.m_TraceLevel != 0U)
					{
						OraTrace.TraceExceptionInfo(ex2);
					}
					num = ErrRes.INT_ERR;
					throw;
				}
				finally
				{
					if (num != 0)
					{
						OpoDecCtx.FreeCtx(ref zero2);
						if (num != ErrRes.INT_ERR)
						{
							throw new OracleTypeException(num, new object[0]);
						}
					}
				}
				this.m_opoDecCtx = new OpoDecCtx(zero2);
				this.m_bGetInfo = true;
				this.m_bNotNull = true;
				this.m_numberType = 4;
				this.m_bPositive = false;
				this.m_bZero = false;
				this.m_format = null;
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleDecimal::OracleDecimal(4)\n"
					});
				}
				return;
			}
			this.m_opoDecCtx = new OpoDecCtx(doubleX, ref this.m_numberType, ref this.m_bPositive, ref this.m_bZero);
			this.m_bGetInfo = true;
			if (this.m_opoDecCtx.m_error == 0)
			{
				this.m_bNotNull = true;
				this.m_format = null;
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleDecimal::OracleDecimal(4)\n"
					});
				}
				return;
			}
			if (this.m_opoDecCtx.m_error == 22053)
			{
				throw new OverflowException(OracleTypeException.GetTypeMsg(this.m_opoDecCtx.m_error, new object[0]));
			}
			throw new OracleTypeException(this.m_opoDecCtx.m_error, new object[]
			{
				"doubleX"
			});
		}

		// Token: 0x06000B83 RID: 2947 RVA: 0x00074D88 File Offset: 0x00073D88
		public OracleDecimal(decimal decimalX)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleDecimal::OracleDecimal(5)\n"
				});
			}
			this.m_opoDecCtx = new OpoDecCtx(decimalX, ref this.m_numberType, ref this.m_bPositive, ref this.m_bZero);
			this.m_bGetInfo = true;
			if (this.m_opoDecCtx.m_error == 0)
			{
				this.m_bNotNull = true;
				this.m_format = null;
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleDecimal::OracleDecimal(5)\n"
					});
				}
				return;
			}
			if (this.m_opoDecCtx.m_error == 22053)
			{
				throw new OverflowException(OracleTypeException.GetTypeMsg(this.m_opoDecCtx.m_error, new object[0]));
			}
			throw new OracleTypeException(this.m_opoDecCtx.m_error, new object[]
			{
				"decimalX"
			});
		}

		// Token: 0x06000B84 RID: 2948 RVA: 0x00074E5E File Offset: 0x00073E5E
		public OracleDecimal(string numStr)
		{
			this = new OracleDecimal(numStr, null);
		}

		// Token: 0x06000B85 RID: 2949 RVA: 0x00074E68 File Offset: 0x00073E68
		public OracleDecimal(string numStr, string format)
		{
			if (numStr == null)
			{
				throw new ArgumentNullException("numStr");
			}
			this.m_format = format;
			this.m_opoDecCtx = new OpoDecCtx(numStr, this.m_format, ref this.m_numberType, ref this.m_bPositive, ref this.m_bZero);
			this.m_bGetInfo = true;
			if (this.m_opoDecCtx.m_error == 0)
			{
				this.m_bNotNull = true;
				return;
			}
			if (this.m_opoDecCtx.m_error == 22053)
			{
				throw new OverflowException(OracleTypeException.GetTypeMsg(this.m_opoDecCtx.m_error, new object[0]));
			}
			throw new ArgumentException(OracleTypeException.GetTypeMsg(this.m_opoDecCtx.m_error, new object[]
			{
				numStr
			}));
		}

		// Token: 0x06000B86 RID: 2950 RVA: 0x00074F1C File Offset: 0x00073F1C
		public static bool Equals(OracleDecimal value1, OracleDecimal value2)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleDecimal::Equals(1)\n"
				});
			}
			CompareNullEnum compareNullEnum = InternalTypes.CompareNull(!value1.m_bNotNull, !value2.m_bNotNull);
			if (compareNullEnum == CompareNullEnum.BothNull)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleDecimal::Equals(1)\n"
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
						" (EXIT)  OracleDecimal::Equals(1)\n"
					});
				}
				return false;
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleDecimal::Equals(1)\n"
				});
			}
			return OracleDecimal.Compare(value1.m_opoDecCtx, value2.m_opoDecCtx) == 0;
		}

		// Token: 0x06000B87 RID: 2951 RVA: 0x00074FE0 File Offset: 0x00073FE0
		public static bool GreaterThan(OracleDecimal value1, OracleDecimal value2)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleDecimal::GreaterThan()\n"
				});
			}
			CompareNullEnum compareNullEnum = InternalTypes.CompareNull(!value1.m_bNotNull, !value2.m_bNotNull);
			if (compareNullEnum == CompareNullEnum.BothNull)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleDecimal::GreaterThan()\n"
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
						" (EXIT)  OracleDecimal::GreaterThan()\n"
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
						" (EXIT)  OracleDecimal::GreaterThan()\n"
					});
				}
				return true;
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleDecimal::GreaterThan()\n"
				});
			}
			return OracleDecimal.Compare(value1.m_opoDecCtx, value2.m_opoDecCtx) > 0;
		}

		// Token: 0x06000B88 RID: 2952 RVA: 0x000750CC File Offset: 0x000740CC
		public static bool GreaterThanOrEqual(OracleDecimal value1, OracleDecimal value2)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleDecimal::GreaterThanOrEqual()\n"
				});
			}
			CompareNullEnum compareNullEnum = InternalTypes.CompareNull(!value1.m_bNotNull, !value2.m_bNotNull);
			if (compareNullEnum == CompareNullEnum.BothNull)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleDecimal::GreaterThanOrEqual()\n"
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
						" (EXIT)  OracleDecimal::GreaterThanOrEqual()\n"
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
						" (EXIT)  OracleDecimal::GreaterThanOrEqual()\n"
					});
				}
				return true;
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleDecimal::GreaterThanOrEqual()\n"
				});
			}
			return OracleDecimal.Compare(value1.m_opoDecCtx, value2.m_opoDecCtx) >= 0;
		}

		// Token: 0x06000B89 RID: 2953 RVA: 0x000751B8 File Offset: 0x000741B8
		public static bool LessThan(OracleDecimal value1, OracleDecimal value2)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleDecimal::LessThan()\n"
				});
			}
			CompareNullEnum compareNullEnum = InternalTypes.CompareNull(!value1.m_bNotNull, !value2.m_bNotNull);
			if (compareNullEnum == CompareNullEnum.BothNull)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleDecimal::LessThan()\n"
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
						" (EXIT)  OracleDecimal::LessThan()\n"
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
						" (EXIT)  OracleDecimal::LessThan()\n"
					});
				}
				return false;
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleDecimal::LessThan()\n"
				});
			}
			return OracleDecimal.Compare(value1.m_opoDecCtx, value2.m_opoDecCtx) < 0;
		}

		// Token: 0x06000B8A RID: 2954 RVA: 0x000752A4 File Offset: 0x000742A4
		public static bool LessThanOrEqual(OracleDecimal value1, OracleDecimal value2)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleDecimal::LessThanOrEqual()\n"
				});
			}
			CompareNullEnum compareNullEnum = InternalTypes.CompareNull(!value1.m_bNotNull, !value2.m_bNotNull);
			if (compareNullEnum == CompareNullEnum.BothNull)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleDecimal::LessThanOrEqual()\n"
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
						" (EXIT)  OracleDecimal::LessThanOrEqual()\n"
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
						" (EXIT)  OracleDecimal::LessThanOrEqual()\n"
					});
				}
				return false;
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleDecimal::LessThanOrEqual()\n"
				});
			}
			return OracleDecimal.Compare(value1.m_opoDecCtx, value2.m_opoDecCtx) <= 0;
		}

		// Token: 0x06000B8B RID: 2955 RVA: 0x0007538D File Offset: 0x0007438D
		public static bool NotEquals(OracleDecimal value1, OracleDecimal value2)
		{
			return !OracleDecimal.Equals(value1, value2);
		}

		// Token: 0x06000B8C RID: 2956 RVA: 0x00075399 File Offset: 0x00074399
		public static OracleDecimal Max(OracleDecimal value1, OracleDecimal value2)
		{
			if (OracleDecimal.GreaterThanOrEqual(value1, value2))
			{
				return value1;
			}
			return value2;
		}

		// Token: 0x06000B8D RID: 2957 RVA: 0x000753A7 File Offset: 0x000743A7
		public static OracleDecimal Min(OracleDecimal value1, OracleDecimal value2)
		{
			if (OracleDecimal.LessThanOrEqual(value1, value2))
			{
				return value1;
			}
			return value2;
		}

		// Token: 0x06000B8E RID: 2958 RVA: 0x000753B8 File Offset: 0x000743B8
		public static OracleDecimal Abs(OracleDecimal value1)
		{
			int num = 0;
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleDecimal::Abs()\n"
				});
			}
			if (value1.m_bNotNull)
			{
				IntPtr zero = IntPtr.Zero;
				try
				{
					num = OpsDec.AllocValCtxForAbs(value1.m_opoDecCtx.m_pValCtx, out zero);
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
						OpoDecCtx.FreeCtx(ref zero);
						if (num != ErrRes.INT_ERR)
						{
							throw new OracleTypeException(num, new object[]
							{
								"value1"
							});
						}
					}
				}
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleDecimal::Abs()\n"
					});
				}
				return new OracleDecimal(zero, false);
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleDecimal::Abs()\n"
				});
			}
			return OracleDecimal.Null;
		}

		// Token: 0x06000B8F RID: 2959 RVA: 0x000754BC File Offset: 0x000744BC
		public static OracleDecimal Add(OracleDecimal value1, OracleDecimal value2)
		{
			int num = 0;
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleDecimal::Add()\n"
				});
			}
			if (value1.m_bNotNull && value2.m_bNotNull)
			{
				IntPtr zero = IntPtr.Zero;
				try
				{
					num = OpsDec.AllocValCtxForAdd(value1.m_opoDecCtx.m_pValCtx, value2.m_opoDecCtx.m_pValCtx, out zero);
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
						OpoDecCtx.FreeCtx(ref zero);
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
						" (EXIT)  OracleDecimal::Add()\n"
					});
				}
				return new OracleDecimal(zero, false);
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleDecimal::Add()\n"
				});
			}
			return OracleDecimal.Null;
		}

		// Token: 0x06000B90 RID: 2960 RVA: 0x000755CC File Offset: 0x000745CC
		public static OracleDecimal AdjustScale(OracleDecimal value1, int digits, bool fRound)
		{
			if (fRound)
			{
				return OracleDecimal.Round(value1, digits);
			}
			return OracleDecimal.Truncate(value1, digits);
		}

		// Token: 0x06000B91 RID: 2961 RVA: 0x000755E0 File Offset: 0x000745E0
		public static OracleDecimal Ceiling(OracleDecimal value1)
		{
			int num = 0;
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleDecimal::Ceiling()\n"
				});
			}
			if (value1.m_bNotNull)
			{
				IntPtr zero = IntPtr.Zero;
				try
				{
					num = OpsDec.AllocValCtxForCeiling(value1.m_opoDecCtx.m_pValCtx, out zero);
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
						OpoDecCtx.FreeCtx(ref zero);
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
						" (EXIT)  OracleDecimal::Ceiling()\n"
					});
				}
				return new OracleDecimal(zero, false);
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleDecimal::Ceiling()\n"
				});
			}
			return OracleDecimal.Null;
		}

		// Token: 0x06000B92 RID: 2962 RVA: 0x000756D4 File Offset: 0x000746D4
		public static OracleDecimal ConvertToPrecScale(OracleDecimal value1, int precision, int scale)
		{
			int num = 0;
			int num2 = 0;
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleDecimal::ConvertToPrecScale()\n"
				});
			}
			if (!value1.m_bNotNull)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleDecimal::ConvertToPrecScale()\n"
					});
				}
				return OracleDecimal.Null;
			}
			IntPtr zero = IntPtr.Zero;
			if (scale > (int)OracleDecimal.MaxScale || scale < OracleDecimal.MinScale)
			{
				throw new OracleTypeException(1728, new object[0]);
			}
			if (precision > (int)OracleDecimal.MaxPrecision || precision < 1)
			{
				throw new OracleTypeException(1727, new object[0]);
			}
			try
			{
				num2 = OpsDec.AllocValCtxForConvertToPrecScale(value1.m_opoDecCtx.m_pValCtx, precision, scale, out zero, ref num);
			}
			catch (Exception ex)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex);
				}
				num2 = ErrRes.INT_ERR;
				throw;
			}
			finally
			{
				if (num2 != 0)
				{
					OpoDecCtx.FreeCtx(ref zero);
					if (num2 != ErrRes.INT_ERR)
					{
						throw new OracleTypeException(num2, new object[0]);
					}
				}
			}
			if (num > 0)
			{
				OpoDecCtx.FreeCtx(ref zero);
				throw new OracleTruncateException();
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleDecimal::ConvertToPrecScale()\n"
				});
			}
			return new OracleDecimal(zero, false);
		}

		// Token: 0x06000B93 RID: 2963 RVA: 0x00075824 File Offset: 0x00074824
		public static OracleDecimal Divide(OracleDecimal value1, OracleDecimal value2)
		{
			int num = 0;
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleDecimal::Divide()\n"
				});
			}
			if (value1.m_bNotNull && value2.m_bNotNull)
			{
				IntPtr zero = IntPtr.Zero;
				try
				{
					num = OpsDec.AllocValCtxForDivide(value1.m_opoDecCtx.m_pValCtx, value2.m_opoDecCtx.m_pValCtx, out zero);
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
						OpoDecCtx.FreeCtx(ref zero);
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
						" (EXIT)  OracleDecimal::Divide()\n"
					});
				}
				return new OracleDecimal(zero, false);
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleDecimal::Divide()\n"
				});
			}
			return OracleDecimal.Null;
		}

		// Token: 0x06000B94 RID: 2964 RVA: 0x00075934 File Offset: 0x00074934
		public static OracleDecimal Floor(OracleDecimal value1)
		{
			int num = 0;
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleDecimal::Floor()\n"
				});
			}
			if (value1.m_bNotNull)
			{
				IntPtr zero = IntPtr.Zero;
				try
				{
					num = OpsDec.AllocValCtxForFloor(value1.m_opoDecCtx.m_pValCtx, out zero);
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
						OpoDecCtx.FreeCtx(ref zero);
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
						" (EXIT)  OracleDecimal::Floor()\n"
					});
				}
				return new OracleDecimal(zero, false);
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleDecimal::Floor()\n"
				});
			}
			return OracleDecimal.Null;
		}

		// Token: 0x06000B95 RID: 2965 RVA: 0x00075A28 File Offset: 0x00074A28
		public static OracleDecimal Mod(OracleDecimal value1, OracleDecimal value2)
		{
			int num = 0;
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleDecimal::Mod()\n"
				});
			}
			if (value1.m_bNotNull && value2.m_bNotNull)
			{
				IntPtr zero = IntPtr.Zero;
				try
				{
					num = OpsDec.AllocValCtxForModulus(value1.m_opoDecCtx.m_pValCtx, value2.m_opoDecCtx.m_pValCtx, out zero);
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
						OpoDecCtx.FreeCtx(ref zero);
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
						" (EXIT)  OracleDecimal::Mod()\n"
					});
				}
				return new OracleDecimal(zero, false);
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleDecimal::Mod()\n"
				});
			}
			return OracleDecimal.Null;
		}

		// Token: 0x06000B96 RID: 2966 RVA: 0x00075B38 File Offset: 0x00074B38
		public static OracleDecimal Multiply(OracleDecimal value1, OracleDecimal value2)
		{
			int num = 0;
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleDecimal::Multiply()\n"
				});
			}
			if (value1.m_bNotNull && value2.m_bNotNull)
			{
				IntPtr zero = IntPtr.Zero;
				try
				{
					num = OpsDec.AllocValCtxForMultiply(value1.m_opoDecCtx.m_pValCtx, value2.m_opoDecCtx.m_pValCtx, out zero);
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
						OpoDecCtx.FreeCtx(ref zero);
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
						" (EXIT)  OracleDecimal::Multiply()\n"
					});
				}
				return new OracleDecimal(zero, false);
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleDecimal::Multiply()\n"
				});
			}
			return OracleDecimal.Null;
		}

		// Token: 0x06000B97 RID: 2967 RVA: 0x00075C48 File Offset: 0x00074C48
		public static OracleDecimal Negate(OracleDecimal value1)
		{
			int num = 0;
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleDecimal::Negate()\n"
				});
			}
			if (value1.m_bNotNull)
			{
				IntPtr zero = IntPtr.Zero;
				try
				{
					num = OpsDec.AllocValCtxForNegate(value1.m_opoDecCtx.m_pValCtx, out zero);
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
						OpoDecCtx.FreeCtx(ref zero);
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
						" (EXIT)  OracleDecimal::Negate()\n"
					});
				}
				return new OracleDecimal(zero, false);
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleDecimal::Negate()\n"
				});
			}
			return OracleDecimal.Null;
		}

		// Token: 0x06000B98 RID: 2968 RVA: 0x00075D3C File Offset: 0x00074D3C
		public static OracleDecimal Parse(string numStr)
		{
			return new OracleDecimal(numStr);
		}

		// Token: 0x06000B99 RID: 2969 RVA: 0x00075D44 File Offset: 0x00074D44
		public static OracleDecimal SetPrecision(OracleDecimal value1, int precision)
		{
			int num = 0;
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleDecimal::SetPrecision()\n"
				});
			}
			if (!value1.m_bNotNull)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleDecimal::SetPrecision()\n"
					});
				}
				return OracleDecimal.Null;
			}
			if (precision > (int)OracleDecimal.MaxPrecision || precision < 1)
			{
				throw new OracleTypeException(1727, new object[0]);
			}
			IntPtr zero = IntPtr.Zero;
			try
			{
				num = OpsDec.AllocValCtxForSetPrecWRound(value1.m_opoDecCtx.m_pValCtx, precision, out zero);
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
					OpoDecCtx.FreeCtx(ref zero);
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
					" (EXIT)  OracleDecimal::SetPrecision()\n"
				});
			}
			return new OracleDecimal(zero, false);
		}

		// Token: 0x06000B9A RID: 2970 RVA: 0x00075E58 File Offset: 0x00074E58
		internal static OracleDecimal SetPrecisionNoRound(OracleDecimal value1, int precision)
		{
			int num = 0;
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleDecimal::SetPrecisionNoRound()\n"
				});
			}
			if (!value1.m_bNotNull)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleDecimal::SetPrecisionNoRound()\n"
					});
				}
				return OracleDecimal.Null;
			}
			if (precision > (int)OracleDecimal.MaxPrecision || precision < 1)
			{
				throw new OracleTypeException(1727, new object[0]);
			}
			IntPtr zero = IntPtr.Zero;
			try
			{
				num = OpsDec.AllocValCtxForSetPrecNoRound(value1.m_opoDecCtx.m_pValCtx, precision, out zero);
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
					OpoDecCtx.FreeCtx(ref zero);
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
					" (EXIT)  OracleDecimal::SetPrecisionNoRound()\n"
				});
			}
			return new OracleDecimal(zero, false);
		}

		// Token: 0x06000B9B RID: 2971 RVA: 0x00075F6C File Offset: 0x00074F6C
		public static OracleDecimal Round(OracleDecimal value1, int decplace)
		{
			int num = 0;
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleDecimal::Round()\n"
				});
			}
			if (value1.m_bNotNull)
			{
				IntPtr zero = IntPtr.Zero;
				try
				{
					num = OpsDec.AllocValCtxForRound(value1.m_opoDecCtx.m_pValCtx, decplace, out zero);
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
						OpoDecCtx.FreeCtx(ref zero);
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
						" (EXIT)  OracleDecimal::Round()\n"
					});
				}
				return new OracleDecimal(zero, false);
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleDecimal::Round()\n"
				});
			}
			return OracleDecimal.Null;
		}

		// Token: 0x06000B9C RID: 2972 RVA: 0x00076060 File Offset: 0x00075060
		public static OracleDecimal Shift(OracleDecimal value1, int decplace)
		{
			int num = 0;
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleDecimal::Shift()\n"
				});
			}
			if (value1.m_bNotNull)
			{
				IntPtr zero = IntPtr.Zero;
				try
				{
					num = OpsDec.AllocValCtxForShift(value1.m_opoDecCtx.m_pValCtx, decplace, out zero);
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
						OpoDecCtx.FreeCtx(ref zero);
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
						" (EXIT)  OracleDecimal::Shift()\n"
					});
				}
				return new OracleDecimal(zero, false);
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleDecimal::Shift()\n"
				});
			}
			return OracleDecimal.Null;
		}

		// Token: 0x06000B9D RID: 2973 RVA: 0x00076154 File Offset: 0x00075154
		public static int Sign(OracleDecimal value1)
		{
			int num = 0;
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleDecimal::Sign()\n"
				});
			}
			if (!value1.m_bNotNull)
			{
				throw new OracleNullValueException();
			}
			int result = 0;
			try
			{
				num = OpsDec.Sign(value1.m_opoDecCtx.m_pValCtx, ref result);
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
					" (EXIT)  OracleDecimal::Sign()\n"
				});
			}
			return result;
		}

		// Token: 0x06000B9E RID: 2974 RVA: 0x000761F8 File Offset: 0x000751F8
		public static OracleDecimal Sqrt(OracleDecimal value1)
		{
			int num = 0;
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleDecimal::Sqrt()\n"
				});
			}
			if (!value1.m_bNotNull)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleDecimal::Sqrt()\n"
					});
				}
				return OracleDecimal.Null;
			}
			IntPtr zero = IntPtr.Zero;
			if (!value1.m_bGetInfo)
			{
				int num2 = 0;
				int num3 = 0;
				try
				{
					value1.m_opoDecCtx.m_error = OpsDec.GetInfo(value1.m_opoDecCtx.m_pValCtx, out value1.m_numberType, out num2, out num3, 0);
				}
				catch (Exception ex)
				{
					if (OraTrace.m_TraceLevel != 0U)
					{
						OraTrace.TraceExceptionInfo(ex);
					}
					throw;
				}
				if (value1.m_opoDecCtx.m_error != 0)
				{
					throw new OracleTypeException(value1.m_opoDecCtx.m_error, new object[0]);
				}
				if (num2 == 1)
				{
					value1.m_bPositive = true;
				}
				else
				{
					value1.m_bPositive = false;
				}
				if (num3 == 1)
				{
					value1.m_bZero = true;
				}
				else
				{
					value1.m_bZero = false;
				}
				value1.m_bGetInfo = true;
			}
			if (!value1.m_bPositive && !value1.m_bZero)
			{
				throw new ArgumentOutOfRangeException("value1");
			}
			try
			{
				num = OpsDec.AllocValCtxForSqrt(value1.m_opoDecCtx.m_pValCtx, out zero);
			}
			catch (Exception ex2)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex2);
				}
				num = ErrRes.INT_ERR;
				throw;
			}
			finally
			{
				if (num != 0)
				{
					OpoDecCtx.FreeCtx(ref zero);
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
					" (EXIT)  OracleDecimal::Sqrt()\n"
				});
			}
			return new OracleDecimal(zero, false);
		}

		// Token: 0x06000B9F RID: 2975 RVA: 0x000763C4 File Offset: 0x000753C4
		public static OracleDecimal Subtract(OracleDecimal value1, OracleDecimal value2)
		{
			int num = 0;
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleDecimal::Subtract()\n"
				});
			}
			if (value1.m_bNotNull && value2.m_bNotNull)
			{
				IntPtr zero = IntPtr.Zero;
				try
				{
					num = OpsDec.AllocValCtxForSubtract(value1.m_opoDecCtx.m_pValCtx, value2.m_opoDecCtx.m_pValCtx, out zero);
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
						OpoDecCtx.FreeCtx(ref zero);
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
						" (EXIT)  OracleDecimal::Subtract()\n"
					});
				}
				return new OracleDecimal(zero, false);
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleDecimal::Subtract()\n"
				});
			}
			return OracleDecimal.Null;
		}

		// Token: 0x06000BA0 RID: 2976 RVA: 0x000764D4 File Offset: 0x000754D4
		public static OracleDecimal Truncate(OracleDecimal value1, int position)
		{
			int num = 0;
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleDecimal::Truncate()\n"
				});
			}
			if (value1.m_bNotNull)
			{
				IntPtr zero = IntPtr.Zero;
				try
				{
					num = OpsDec.AllocValCtxForTruncate(value1.m_opoDecCtx.m_pValCtx, position, out zero);
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
						OpoDecCtx.FreeCtx(ref zero);
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
						" (EXIT)  OracleDecimal::Truncate()\n"
					});
				}
				return new OracleDecimal(zero, false);
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleDecimal::Truncate()\n"
				});
			}
			return OracleDecimal.Null;
		}

		// Token: 0x06000BA1 RID: 2977 RVA: 0x000765C8 File Offset: 0x000755C8
		public static OracleDecimal Exp(OracleDecimal value1)
		{
			int num = 0;
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleDecimal::Exp()\n"
				});
			}
			if (value1.m_bNotNull)
			{
				IntPtr zero = IntPtr.Zero;
				try
				{
					num = OpsDec.AllocValCtxForExp(value1.m_opoDecCtx.m_pValCtx, out zero);
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
						OpoDecCtx.FreeCtx(ref zero);
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
						" (EXIT)  OracleDecimal::Exp()\n"
					});
				}
				return new OracleDecimal(zero, false);
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleDecimal::Exp()\n"
				});
			}
			return OracleDecimal.Null;
		}

		// Token: 0x06000BA2 RID: 2978 RVA: 0x000766BC File Offset: 0x000756BC
		public static OracleDecimal Pow(OracleDecimal value1, int power)
		{
			int num = 0;
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleDecimal::Pow(1)\n"
				});
			}
			if (value1.m_bNotNull)
			{
				IntPtr zero = IntPtr.Zero;
				try
				{
					num = OpsDec.AllocValCtxForIntPower(value1.m_opoDecCtx.m_pValCtx, power, out zero);
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
						OpoDecCtx.FreeCtx(ref zero);
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
						" (EXIT)  OracleDecimal::Pow(1)\n"
					});
				}
				return new OracleDecimal(zero, false);
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleDecimal::Pow(1)\n"
				});
			}
			return OracleDecimal.Null;
		}

		// Token: 0x06000BA3 RID: 2979 RVA: 0x000767B0 File Offset: 0x000757B0
		public static OracleDecimal Log(OracleDecimal value1)
		{
			int num = 0;
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleDecimal::Log(1)\n"
				});
			}
			if (!value1.m_bNotNull)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleDecimal::Log(1)\n"
					});
				}
				return OracleDecimal.Null;
			}
			if (!value1.IsPositive && !value1.IsZero)
			{
				throw new ArgumentOutOfRangeException();
			}
			IntPtr zero = IntPtr.Zero;
			try
			{
				num = OpsDec.AllocValCtxForLn(value1.m_opoDecCtx.m_pValCtx, out zero);
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
					OpoDecCtx.FreeCtx(ref zero);
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
					" (EXIT)  OracleDecimal::Log(1)\n"
				});
			}
			return new OracleDecimal(zero, false);
		}

		// Token: 0x06000BA4 RID: 2980 RVA: 0x000768C0 File Offset: 0x000758C0
		public static OracleDecimal Log(OracleDecimal value1, int logBase)
		{
			int num = 0;
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleDecimal::Log(2)\n"
				});
			}
			if (!value1.m_bNotNull)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleDecimal::Log(2)\n"
					});
				}
				return OracleDecimal.Null;
			}
			if ((!value1.IsPositive && !value1.IsZero) || logBase <= 0)
			{
				throw new ArgumentOutOfRangeException();
			}
			if (value1.IsZero && logBase == 0)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleDecimal::Log(2)\n"
					});
				}
				return OracleDecimal.Null;
			}
			if (value1.IsPositive && logBase == 0)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleDecimal::Log(2)\n"
					});
				}
				return new OracleDecimal(0);
			}
			IntPtr zero = IntPtr.Zero;
			try
			{
				num = OpsDec.AllocValCtxForIntLog(value1.m_opoDecCtx.m_pValCtx, logBase, out zero);
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
					OpoDecCtx.FreeCtx(ref zero);
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
					" (EXIT)  OracleDecimal::Log(2)\n"
				});
			}
			return new OracleDecimal(zero, false);
		}

		// Token: 0x06000BA5 RID: 2981 RVA: 0x00076A38 File Offset: 0x00075A38
		public static OracleDecimal Log(OracleDecimal value1, OracleDecimal logBase)
		{
			int num = 0;
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleDecimal::Log(3)\n"
				});
			}
			if (!value1.m_bNotNull || !logBase.m_bNotNull)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleDecimal::Log(3)\n"
					});
				}
				return OracleDecimal.Null;
			}
			if ((!value1.IsPositive && !value1.IsZero) || (!logBase.IsPositive && !logBase.IsZero))
			{
				throw new ArgumentOutOfRangeException();
			}
			if (value1.IsZero && logBase.IsZero)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleDecimal::Log(3)\n"
					});
				}
				return OracleDecimal.Null;
			}
			if (value1.IsPositive && logBase.IsZero)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleDecimal::Log(3)\n"
					});
				}
				return new OracleDecimal(0);
			}
			IntPtr zero = IntPtr.Zero;
			try
			{
				num = OpsDec.AllocValCtxForLog(value1.m_opoDecCtx.m_pValCtx, logBase.m_opoDecCtx.m_pValCtx, out zero);
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
					OpoDecCtx.FreeCtx(ref zero);
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
					" (EXIT)  OracleDecimal::Log(3)\n"
				});
			}
			return new OracleDecimal(zero, false);
		}

		// Token: 0x06000BA6 RID: 2982 RVA: 0x00076BE0 File Offset: 0x00075BE0
		public static OracleDecimal Pow(OracleDecimal value1, OracleDecimal power)
		{
			int num = 0;
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleDecimal::Pow(2)\n"
				});
			}
			if (!value1.m_bNotNull || !power.m_bNotNull)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleDecimal::Pow(2)\n"
					});
				}
				return OracleDecimal.Null;
			}
			if (value1.IsZero && !power.IsPositive && !power.IsZero)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleDecimal::Pow(2)\n"
					});
				}
				return OracleDecimal.PositiveInfinity;
			}
			IntPtr zero = IntPtr.Zero;
			try
			{
				num = OpsDec.AllocValCtxForPower(value1.m_opoDecCtx.m_pValCtx, power.m_opoDecCtx.m_pValCtx, out zero);
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
					OpoDecCtx.FreeCtx(ref zero);
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
					" (EXIT)  OracleDecimal::Pow(2)\n"
				});
			}
			return new OracleDecimal(zero, false);
		}

		// Token: 0x06000BA7 RID: 2983 RVA: 0x00076D30 File Offset: 0x00075D30
		public static OracleDecimal Acos(OracleDecimal value1)
		{
			int num = 0;
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleDecimal::Acos()\n"
				});
			}
			if (value1.m_bNotNull)
			{
				IntPtr zero = IntPtr.Zero;
				try
				{
					num = OpsDec.AllocValCtxForACos(value1.m_opoDecCtx.m_pValCtx, out zero);
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
						OpoDecCtx.FreeCtx(ref zero);
						if (num != ErrRes.INT_ERR)
						{
							throw new OracleTypeException(num, new object[]
							{
								"value1"
							});
						}
					}
				}
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleDecimal::Acos()\n"
					});
				}
				return new OracleDecimal(zero, false);
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleDecimal::Acos()\n"
				});
			}
			return OracleDecimal.Null;
		}

		// Token: 0x06000BA8 RID: 2984 RVA: 0x00076E34 File Offset: 0x00075E34
		public static OracleDecimal Asin(OracleDecimal value1)
		{
			int num = 0;
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleDecimal::Asin()\n"
				});
			}
			if (value1.m_bNotNull)
			{
				IntPtr zero = IntPtr.Zero;
				try
				{
					num = OpsDec.AllocValCtxForASin(value1.m_opoDecCtx.m_pValCtx, out zero);
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
						OpoDecCtx.FreeCtx(ref zero);
						if (num != ErrRes.INT_ERR)
						{
							throw new OracleTypeException(num, new object[]
							{
								"value1"
							});
						}
					}
				}
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleDecimal::Asin()\n"
					});
				}
				return new OracleDecimal(zero, false);
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleDecimal::Asin()\n"
				});
			}
			return OracleDecimal.Null;
		}

		// Token: 0x06000BA9 RID: 2985 RVA: 0x00076F38 File Offset: 0x00075F38
		public static OracleDecimal Atan(OracleDecimal value1)
		{
			int num = 0;
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleDecimal::Atan()\n"
				});
			}
			if (value1.m_bNotNull)
			{
				IntPtr zero = IntPtr.Zero;
				try
				{
					num = OpsDec.AllocValCtxForATan(value1.m_opoDecCtx.m_pValCtx, out zero);
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
						OpoDecCtx.FreeCtx(ref zero);
						if (num != ErrRes.INT_ERR)
						{
							throw new OracleTypeException(num, new object[]
							{
								"value1"
							});
						}
					}
				}
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleDecimal::Atan()\n"
					});
				}
				return new OracleDecimal(zero, false);
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleDecimal::Atan()\n"
				});
			}
			return OracleDecimal.Null;
		}

		// Token: 0x06000BAA RID: 2986 RVA: 0x0007703C File Offset: 0x0007603C
		public static OracleDecimal Atan2(OracleDecimal value1, OracleDecimal value2)
		{
			int num = 0;
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleDecimal::Atan2()\n"
				});
			}
			if (value1.m_bNotNull && value2.m_bNotNull)
			{
				IntPtr zero = IntPtr.Zero;
				try
				{
					num = OpsDec.AllocValCtxForATan2(value1.m_opoDecCtx.m_pValCtx, value2.m_opoDecCtx.m_pValCtx, out zero);
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
						OpoDecCtx.FreeCtx(ref zero);
						if (num != ErrRes.INT_ERR)
						{
							throw new OracleTypeException(num, new object[]
							{
								"value1/value2"
							});
						}
					}
				}
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleDecimal::Atan2()\n"
					});
				}
				return new OracleDecimal(zero, false);
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleDecimal::Atan2()\n"
				});
			}
			return OracleDecimal.Null;
		}

		// Token: 0x06000BAB RID: 2987 RVA: 0x00077158 File Offset: 0x00076158
		public static OracleDecimal Cos(OracleDecimal value1)
		{
			int num = 0;
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleDecimal::Cos()\n"
				});
			}
			if (!value1.m_bNotNull)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleDecimal::Cos()\n"
					});
				}
				return OracleDecimal.Null;
			}
			if (value1.IsInfinity)
			{
				throw new ArgumentOutOfRangeException("value1");
			}
			IntPtr zero = IntPtr.Zero;
			try
			{
				num = OpsDec.AllocValCtxForCos(value1.m_opoDecCtx.m_pValCtx, out zero);
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
					OpoDecCtx.FreeCtx(ref zero);
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
					" (EXIT)  OracleDecimal::Cos()\n"
				});
			}
			return new OracleDecimal(zero, false);
		}

		// Token: 0x06000BAC RID: 2988 RVA: 0x00077264 File Offset: 0x00076264
		public static OracleDecimal Sin(OracleDecimal value1)
		{
			int num = 0;
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleDecimal::Sin()\n"
				});
			}
			if (!value1.m_bNotNull)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleDecimal::Sin()\n"
					});
				}
				return OracleDecimal.Null;
			}
			if (value1.IsInfinity)
			{
				throw new ArgumentOutOfRangeException("value1");
			}
			IntPtr zero = IntPtr.Zero;
			try
			{
				num = OpsDec.AllocValCtxForSin(value1.m_opoDecCtx.m_pValCtx, out zero);
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
					OpoDecCtx.FreeCtx(ref zero);
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
					" (EXIT)  OracleDecimal::Sin()\n"
				});
			}
			return new OracleDecimal(zero, false);
		}

		// Token: 0x06000BAD RID: 2989 RVA: 0x00077370 File Offset: 0x00076370
		public static OracleDecimal Tan(OracleDecimal value1)
		{
			int num = 0;
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleDecimal::Tan()\n"
				});
			}
			if (!value1.m_bNotNull)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleDecimal::Tan()\n"
					});
				}
				return OracleDecimal.Null;
			}
			if (value1.IsInfinity)
			{
				throw new ArgumentOutOfRangeException("value1");
			}
			IntPtr zero = IntPtr.Zero;
			try
			{
				num = OpsDec.AllocValCtxForTan(value1.m_opoDecCtx.m_pValCtx, out zero);
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
					OpoDecCtx.FreeCtx(ref zero);
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
					" (EXIT)  OracleDecimal::Tan()\n"
				});
			}
			return new OracleDecimal(zero, false);
		}

		// Token: 0x06000BAE RID: 2990 RVA: 0x0007747C File Offset: 0x0007647C
		public static OracleDecimal Cosh(OracleDecimal value1)
		{
			int num = 0;
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleDecimal::Cosh()\n"
				});
			}
			if (value1.m_bNotNull)
			{
				IntPtr zero = IntPtr.Zero;
				try
				{
					num = OpsDec.AllocValCtxForCosh(value1.m_opoDecCtx.m_pValCtx, out zero);
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
						OpoDecCtx.FreeCtx(ref zero);
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
						" (EXIT)  OracleDecimal::Cosh()\n"
					});
				}
				return new OracleDecimal(zero, false);
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleDecimal::Cosh()\n"
				});
			}
			return OracleDecimal.Null;
		}

		// Token: 0x06000BAF RID: 2991 RVA: 0x00077570 File Offset: 0x00076570
		public static OracleDecimal Sinh(OracleDecimal value1)
		{
			int num = 0;
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleDecimal::Sinh()\n"
				});
			}
			if (value1.m_bNotNull)
			{
				IntPtr zero = IntPtr.Zero;
				try
				{
					num = OpsDec.AllocValCtxForSinh(value1.m_opoDecCtx.m_pValCtx, out zero);
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
						OpoDecCtx.FreeCtx(ref zero);
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
						" (EXIT)  OracleDecimal::Sinh()\n"
					});
				}
				return new OracleDecimal(zero, false);
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleDecimal::Sinh()\n"
				});
			}
			return OracleDecimal.Null;
		}

		// Token: 0x06000BB0 RID: 2992 RVA: 0x00077664 File Offset: 0x00076664
		public static OracleDecimal Tanh(OracleDecimal value1)
		{
			int num = 0;
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleDecimal::Tanh()\n"
				});
			}
			if (!value1.m_bNotNull)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleDecimal::Tanh()\n"
					});
				}
				return OracleDecimal.Null;
			}
			if (value1.IsPositiveInfinity)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleDecimal::Tanh()\n"
					});
				}
				return new OracleDecimal(1);
			}
			if (value1.IsNegativeInfinity)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleDecimal::Tanh()\n"
					});
				}
				return new OracleDecimal(-1);
			}
			IntPtr zero = IntPtr.Zero;
			try
			{
				num = OpsDec.AllocValCtxForTanh(value1.m_opoDecCtx.m_pValCtx, out zero);
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
					OpoDecCtx.FreeCtx(ref zero);
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
					" (EXIT)  OracleDecimal::Tanh()\n"
				});
			}
			return new OracleDecimal(zero, false);
		}

		// Token: 0x06000BB1 RID: 2993 RVA: 0x000777BC File Offset: 0x000767BC
		public static bool operator ==(OracleDecimal value1, OracleDecimal value2)
		{
			return OracleDecimal.Equals(value1, value2);
		}

		// Token: 0x06000BB2 RID: 2994 RVA: 0x000777C5 File Offset: 0x000767C5
		public static bool operator >(OracleDecimal value1, OracleDecimal value2)
		{
			return OracleDecimal.GreaterThan(value1, value2);
		}

		// Token: 0x06000BB3 RID: 2995 RVA: 0x000777CE File Offset: 0x000767CE
		public static bool operator >=(OracleDecimal value1, OracleDecimal value2)
		{
			return OracleDecimal.GreaterThanOrEqual(value1, value2);
		}

		// Token: 0x06000BB4 RID: 2996 RVA: 0x000777D7 File Offset: 0x000767D7
		public static bool operator <(OracleDecimal value1, OracleDecimal value2)
		{
			return OracleDecimal.LessThan(value1, value2);
		}

		// Token: 0x06000BB5 RID: 2997 RVA: 0x000777E0 File Offset: 0x000767E0
		public static bool operator <=(OracleDecimal value1, OracleDecimal value2)
		{
			return OracleDecimal.LessThanOrEqual(value1, value2);
		}

		// Token: 0x06000BB6 RID: 2998 RVA: 0x000777E9 File Offset: 0x000767E9
		public static bool operator !=(OracleDecimal value1, OracleDecimal value2)
		{
			return OracleDecimal.NotEquals(value1, value2);
		}

		// Token: 0x06000BB7 RID: 2999 RVA: 0x000777F2 File Offset: 0x000767F2
		public static OracleDecimal operator +(OracleDecimal value1, OracleDecimal value2)
		{
			return OracleDecimal.Add(value1, value2);
		}

		// Token: 0x06000BB8 RID: 3000 RVA: 0x000777FB File Offset: 0x000767FB
		public static OracleDecimal operator -(OracleDecimal value1, OracleDecimal value2)
		{
			return OracleDecimal.Subtract(value1, value2);
		}

		// Token: 0x06000BB9 RID: 3001 RVA: 0x00077804 File Offset: 0x00076804
		public static OracleDecimal operator -(OracleDecimal value1)
		{
			return OracleDecimal.Negate(value1);
		}

		// Token: 0x06000BBA RID: 3002 RVA: 0x0007780C File Offset: 0x0007680C
		public static OracleDecimal operator *(OracleDecimal value1, OracleDecimal value2)
		{
			return OracleDecimal.Multiply(value1, value2);
		}

		// Token: 0x06000BBB RID: 3003 RVA: 0x00077815 File Offset: 0x00076815
		public static OracleDecimal operator /(OracleDecimal value1, OracleDecimal value2)
		{
			return OracleDecimal.Divide(value1, value2);
		}

		// Token: 0x06000BBC RID: 3004 RVA: 0x0007781E File Offset: 0x0007681E
		public static OracleDecimal operator %(OracleDecimal value1, OracleDecimal value2)
		{
			return OracleDecimal.Mod(value1, value2);
		}

		// Token: 0x06000BBD RID: 3005 RVA: 0x00077827 File Offset: 0x00076827
		public static explicit operator OracleDecimal(string numStr)
		{
			return new OracleDecimal(numStr);
		}

		// Token: 0x06000BBE RID: 3006 RVA: 0x0007782F File Offset: 0x0007682F
		public static explicit operator byte(OracleDecimal value1)
		{
			if (value1.m_bNotNull)
			{
				return (byte)DecimalConv.GetNum(value1.m_opoDecCtx.m_pValCtx, DbType.Byte);
			}
			throw new OracleNullValueException();
		}

		// Token: 0x06000BBF RID: 3007 RVA: 0x00077857 File Offset: 0x00076857
		public static explicit operator short(OracleDecimal value1)
		{
			if (value1.m_bNotNull)
			{
				return (short)DecimalConv.GetNum(value1.m_opoDecCtx.m_pValCtx, DbType.Int16);
			}
			throw new OracleNullValueException();
		}

		// Token: 0x06000BC0 RID: 3008 RVA: 0x00077880 File Offset: 0x00076880
		public static explicit operator int(OracleDecimal value1)
		{
			if (value1.m_bNotNull)
			{
				return (int)DecimalConv.GetNum(value1.m_opoDecCtx.m_pValCtx, DbType.Int32);
			}
			throw new OracleNullValueException();
		}

		// Token: 0x06000BC1 RID: 3009 RVA: 0x000778A9 File Offset: 0x000768A9
		public static explicit operator long(OracleDecimal value1)
		{
			if (value1.m_bNotNull)
			{
				return (long)DecimalConv.GetNum(value1.m_opoDecCtx.m_pValCtx, DbType.Int64);
			}
			throw new OracleNullValueException();
		}

		// Token: 0x06000BC2 RID: 3010 RVA: 0x000778D2 File Offset: 0x000768D2
		public static explicit operator float(OracleDecimal value1)
		{
			if (value1.m_bNotNull)
			{
				return (float)DecimalConv.GetNum(value1.m_opoDecCtx.m_pValCtx, DbType.Single);
			}
			throw new OracleNullValueException();
		}

		// Token: 0x06000BC3 RID: 3011 RVA: 0x000778FB File Offset: 0x000768FB
		public static explicit operator double(OracleDecimal value1)
		{
			if (value1.m_bNotNull)
			{
				return (double)DecimalConv.GetNum(value1.m_opoDecCtx.m_pValCtx, DbType.Double);
			}
			throw new OracleNullValueException();
		}

		// Token: 0x06000BC4 RID: 3012 RVA: 0x00077923 File Offset: 0x00076923
		public static explicit operator decimal(OracleDecimal value1)
		{
			if (value1.m_bNotNull)
			{
				return DecimalConv.GetDecimal(value1.m_opoDecCtx.m_pValCtx);
			}
			throw new OracleNullValueException();
		}

		// Token: 0x06000BC5 RID: 3013 RVA: 0x00077945 File Offset: 0x00076945
		public static explicit operator OracleDecimal(double value1)
		{
			return new OracleDecimal(value1);
		}

		// Token: 0x06000BC6 RID: 3014 RVA: 0x0007794D File Offset: 0x0007694D
		public static implicit operator OracleDecimal(int value1)
		{
			return new OracleDecimal(value1);
		}

		// Token: 0x06000BC7 RID: 3015 RVA: 0x00077955 File Offset: 0x00076955
		public static implicit operator OracleDecimal(long value1)
		{
			return new OracleDecimal(value1);
		}

		// Token: 0x06000BC8 RID: 3016 RVA: 0x0007795D File Offset: 0x0007695D
		public static implicit operator OracleDecimal(decimal value1)
		{
			return new OracleDecimal(value1);
		}

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x06000BC9 RID: 3017 RVA: 0x00077968 File Offset: 0x00076968
		public byte[] BinData
		{
			get
			{
				byte[] array = new byte[22];
				Marshal.Copy(this.m_opoDecCtx.m_pValCtx, array, 0, 22);
				return array;
			}
		}

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x06000BCA RID: 3018 RVA: 0x00077992 File Offset: 0x00076992
		public bool IsNull
		{
			get
			{
				return !this.m_bNotNull;
			}
		}

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x06000BCB RID: 3019 RVA: 0x000779A0 File Offset: 0x000769A0
		public bool IsInt
		{
			get
			{
				if (this.m_bNotNull)
				{
					if (!this.m_bGetInfo)
					{
						int num = 0;
						int num2 = 0;
						this.m_opoDecCtx.m_error = 0;
						try
						{
							this.m_opoDecCtx.m_error = OpsDec.GetInfo(this.m_opoDecCtx.m_pValCtx, out this.m_numberType, out num, out num2, 0);
						}
						catch (Exception ex)
						{
							if (OraTrace.m_TraceLevel != 0U)
							{
								OraTrace.TraceExceptionInfo(ex);
							}
							throw;
						}
						if (this.m_opoDecCtx.m_error != 0)
						{
							throw new OracleTypeException(this.m_opoDecCtx.m_error, new object[0]);
						}
						if (num == 1)
						{
							this.m_bPositive = true;
						}
						else
						{
							this.m_bPositive = false;
						}
						if (num2 == 1)
						{
							this.m_bZero = true;
						}
						else
						{
							this.m_bZero = false;
						}
						this.m_bGetInfo = true;
					}
					return this.m_numberType == 1;
				}
				throw new OracleNullValueException();
			}
		}

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x06000BCC RID: 3020 RVA: 0x00077A80 File Offset: 0x00076A80
		public bool IsPositive
		{
			get
			{
				if (this.m_bNotNull)
				{
					if (!this.m_bGetInfo)
					{
						int num = 0;
						int num2 = 0;
						this.m_opoDecCtx.m_error = 0;
						try
						{
							this.m_opoDecCtx.m_error = OpsDec.GetInfo(this.m_opoDecCtx.m_pValCtx, out this.m_numberType, out num, out num2, 0);
						}
						catch (Exception ex)
						{
							if (OraTrace.m_TraceLevel != 0U)
							{
								OraTrace.TraceExceptionInfo(ex);
							}
							throw;
						}
						if (this.m_opoDecCtx.m_error != 0)
						{
							throw new OracleTypeException(this.m_opoDecCtx.m_error, new object[0]);
						}
						if (num == 1)
						{
							this.m_bPositive = true;
						}
						else
						{
							this.m_bPositive = false;
						}
						if (num2 == 1)
						{
							this.m_bZero = true;
						}
						else
						{
							this.m_bZero = false;
						}
						this.m_bGetInfo = true;
					}
					return this.m_bPositive;
				}
				throw new OracleNullValueException();
			}
		}

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x06000BCD RID: 3021 RVA: 0x00077B5C File Offset: 0x00076B5C
		public bool IsZero
		{
			get
			{
				if (this.m_bNotNull)
				{
					if (!this.m_bGetInfo)
					{
						int num = 0;
						int num2 = 0;
						this.m_opoDecCtx.m_error = 0;
						try
						{
							this.m_opoDecCtx.m_error = OpsDec.GetInfo(this.m_opoDecCtx.m_pValCtx, out this.m_numberType, out num, out num2, 0);
						}
						catch (Exception ex)
						{
							if (OraTrace.m_TraceLevel != 0U)
							{
								OraTrace.TraceExceptionInfo(ex);
							}
							throw;
						}
						if (this.m_opoDecCtx.m_error != 0)
						{
							throw new OracleTypeException(this.m_opoDecCtx.m_error, new object[0]);
						}
						if (num == 1)
						{
							this.m_bPositive = true;
						}
						else
						{
							this.m_bPositive = false;
						}
						if (num2 == 1)
						{
							this.m_bZero = true;
						}
						else
						{
							this.m_bZero = false;
						}
						this.m_bGetInfo = true;
					}
					return this.m_bZero;
				}
				throw new OracleNullValueException();
			}
		}

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x06000BCE RID: 3022 RVA: 0x00077C38 File Offset: 0x00076C38
		internal bool IsInfinity
		{
			get
			{
				if (this.m_bNotNull)
				{
					if (!this.m_bGetInfo)
					{
						int num = 0;
						int num2 = 0;
						this.m_opoDecCtx.m_error = 0;
						try
						{
							this.m_opoDecCtx.m_error = OpsDec.GetInfo(this.m_opoDecCtx.m_pValCtx, out this.m_numberType, out num, out num2, 0);
						}
						catch (Exception ex)
						{
							if (OraTrace.m_TraceLevel != 0U)
							{
								OraTrace.TraceExceptionInfo(ex);
							}
							throw;
						}
						if (this.m_opoDecCtx.m_error != 0)
						{
							throw new OracleTypeException(this.m_opoDecCtx.m_error, new object[0]);
						}
						if (num == 1)
						{
							this.m_bPositive = true;
						}
						else
						{
							this.m_bPositive = false;
						}
						if (num2 == 1)
						{
							this.m_bZero = true;
						}
						else
						{
							this.m_bZero = false;
						}
						this.m_bGetInfo = true;
					}
					return this.m_numberType == 3 || this.m_numberType == 4;
				}
				throw new OracleNullValueException();
			}
		}

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x06000BCF RID: 3023 RVA: 0x00077D24 File Offset: 0x00076D24
		internal bool IsPositiveInfinity
		{
			get
			{
				if (this.m_bNotNull)
				{
					if (!this.m_bGetInfo)
					{
						int num = 0;
						int num2 = 0;
						this.m_opoDecCtx.m_error = 0;
						try
						{
							this.m_opoDecCtx.m_error = OpsDec.GetInfo(this.m_opoDecCtx.m_pValCtx, out this.m_numberType, out num, out num2, 0);
						}
						catch (Exception ex)
						{
							if (OraTrace.m_TraceLevel != 0U)
							{
								OraTrace.TraceExceptionInfo(ex);
							}
							throw;
						}
						if (this.m_opoDecCtx.m_error != 0)
						{
							throw new OracleTypeException(this.m_opoDecCtx.m_error, new object[0]);
						}
						if (num == 1)
						{
							this.m_bPositive = true;
						}
						else
						{
							this.m_bPositive = false;
						}
						if (num2 == 1)
						{
							this.m_bZero = true;
						}
						else
						{
							this.m_bZero = false;
						}
						this.m_bGetInfo = true;
					}
					return this.m_numberType == 3;
				}
				throw new OracleNullValueException();
			}
		}

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x06000BD0 RID: 3024 RVA: 0x00077E04 File Offset: 0x00076E04
		internal bool IsNegativeInfinity
		{
			get
			{
				if (this.m_bNotNull)
				{
					if (!this.m_bGetInfo)
					{
						int num = 0;
						int num2 = 0;
						this.m_opoDecCtx.m_error = 0;
						try
						{
							this.m_opoDecCtx.m_error = OpsDec.GetInfo(this.m_opoDecCtx.m_pValCtx, out this.m_numberType, out num, out num2, 0);
						}
						catch (Exception ex)
						{
							if (OraTrace.m_TraceLevel != 0U)
							{
								OraTrace.TraceExceptionInfo(ex);
							}
							throw;
						}
						if (this.m_opoDecCtx.m_error != 0)
						{
							throw new OracleTypeException(this.m_opoDecCtx.m_error, new object[0]);
						}
						if (num == 1)
						{
							this.m_bPositive = true;
						}
						else
						{
							this.m_bPositive = false;
						}
						if (num2 == 1)
						{
							this.m_bZero = true;
						}
						else
						{
							this.m_bZero = false;
						}
						this.m_bGetInfo = true;
					}
					return this.m_numberType == 4;
				}
				throw new OracleNullValueException();
			}
		}

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x06000BD1 RID: 3025 RVA: 0x00077EE4 File Offset: 0x00076EE4
		// (set) Token: 0x06000BD2 RID: 3026 RVA: 0x00077EFA File Offset: 0x00076EFA
		public string Format
		{
			get
			{
				if (this.m_bNotNull)
				{
					return this.m_format;
				}
				throw new OracleNullValueException();
			}
			set
			{
				if (this.m_bNotNull)
				{
					this.m_format = value;
					return;
				}
				throw new OracleNullValueException();
			}
		}

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x06000BD3 RID: 3027 RVA: 0x00077F11 File Offset: 0x00076F11
		public decimal Value
		{
			get
			{
				if (this == OracleDecimal.Pi)
				{
					return OracleDecimal.Pivalue;
				}
				if (this.m_bNotNull)
				{
					return DecimalConv.GetDecimal(this.m_opoDecCtx.m_pValCtx);
				}
				throw new OracleNullValueException();
			}
		}

		// Token: 0x06000BD4 RID: 3028 RVA: 0x00077F49 File Offset: 0x00076F49
		public byte ToByte()
		{
			if (this.m_bNotNull)
			{
				return (byte)DecimalConv.GetNum(this.m_opoDecCtx.m_pValCtx, DbType.Byte);
			}
			throw new OracleNullValueException();
		}

		// Token: 0x06000BD5 RID: 3029 RVA: 0x00077F6F File Offset: 0x00076F6F
		public short ToInt16()
		{
			if (this.m_bNotNull)
			{
				return (short)DecimalConv.GetNum(this.m_opoDecCtx.m_pValCtx, DbType.Int16);
			}
			throw new OracleNullValueException();
		}

		// Token: 0x06000BD6 RID: 3030 RVA: 0x00077F96 File Offset: 0x00076F96
		public int ToInt32()
		{
			if (this.m_bNotNull)
			{
				return (int)DecimalConv.GetNum(this.m_opoDecCtx.m_pValCtx, DbType.Int32);
			}
			throw new OracleNullValueException();
		}

		// Token: 0x06000BD7 RID: 3031 RVA: 0x00077FBD File Offset: 0x00076FBD
		public long ToInt64()
		{
			if (this.m_bNotNull)
			{
				return (long)DecimalConv.GetNum(this.m_opoDecCtx.m_pValCtx, DbType.Int64);
			}
			throw new OracleNullValueException();
		}

		// Token: 0x06000BD8 RID: 3032 RVA: 0x00077FE4 File Offset: 0x00076FE4
		public float ToSingle()
		{
			if (this.m_bNotNull)
			{
				return (float)DecimalConv.GetNum(this.m_opoDecCtx.m_pValCtx, DbType.Single);
			}
			throw new OracleNullValueException();
		}

		// Token: 0x06000BD9 RID: 3033 RVA: 0x0007800B File Offset: 0x0007700B
		public double ToDouble()
		{
			if (this.m_bNotNull)
			{
				return (double)DecimalConv.GetNum(this.m_opoDecCtx.m_pValCtx, DbType.Double);
			}
			throw new OracleNullValueException();
		}

		// Token: 0x06000BDA RID: 3034 RVA: 0x00078034 File Offset: 0x00077034
		public int CompareTo(object obj)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleDecimal::CompareTo()\n"
				});
			}
			if (obj.GetType() != typeof(OracleDecimal))
			{
				throw new ArgumentException();
			}
			OracleDecimal oracleDecimal = (OracleDecimal)obj;
			CompareNullEnum compareNullEnum = InternalTypes.CompareNull(!this.m_bNotNull, !oracleDecimal.m_bNotNull);
			if (compareNullEnum == CompareNullEnum.BothNull)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleDecimal::CompareTo()\n"
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
						" (EXIT)  OracleDecimal::CompareTo()\n"
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
						" (EXIT)  OracleDecimal::CompareTo()\n"
					});
				}
				return 1;
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleDecimal::CompareTo()\n"
				});
			}
			return OracleDecimal.Compare(this.m_opoDecCtx, oracleDecimal.m_opoDecCtx);
		}

		// Token: 0x06000BDB RID: 3035 RVA: 0x0007813C File Offset: 0x0007713C
		public override bool Equals(object obj)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleDecimal::Equals(2)\n"
				});
			}
			if (obj == null)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleDecimal::Equals(2)\n"
					});
				}
				return false;
			}
			if (obj.GetType() != typeof(OracleDecimal))
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleDecimal::Equals(2)\n"
					});
				}
				return false;
			}
			OracleDecimal value = (OracleDecimal)obj;
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleDecimal::Equals(2)\n"
				});
			}
			return OracleDecimal.Equals(this, value);
		}

		// Token: 0x06000BDC RID: 3036 RVA: 0x000781F1 File Offset: 0x000771F1
		public override int GetHashCode()
		{
			if (this.m_bNotNull)
			{
				return this.m_opoDecCtx.GetHashCode();
			}
			return 0;
		}

		// Token: 0x06000BDD RID: 3037 RVA: 0x00078208 File Offset: 0x00077208
		public override string ToString()
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleDecimal::ToString()\n"
				});
			}
			if (!this.m_bNotNull)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleDecimal::ToString()\n"
					});
				}
				return "null";
			}
			int num = 0;
			string text;
			try
			{
				num = OpsDec.ToString(this.m_opoDecCtx.m_pValCtx, this.m_format, out text);
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
			text = text.TrimEnd(null);
			text = text.TrimStart(null);
			int length = text.Length;
			string text2 = new string('#', length);
			if (text2.Equals(text))
			{
				throw new OracleTypeException(22065, new object[0]);
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleDecimal::ToString()\n"
				});
			}
			return text;
		}

		// Token: 0x06000BDE RID: 3038 RVA: 0x00078310 File Offset: 0x00077310
		internal OracleDecimal(IntPtr numCtx, bool getInfo)
		{
			if (getInfo)
			{
				this.m_opoDecCtx = new OpoDecCtx(numCtx, ref this.m_numberType, ref this.m_bPositive, ref this.m_bZero);
				this.m_bGetInfo = true;
				if (this.m_opoDecCtx.m_error != 0)
				{
					throw new OracleTypeException(this.m_opoDecCtx.m_error, new object[0]);
				}
			}
			else
			{
				this.m_opoDecCtx = new OpoDecCtx(numCtx);
				this.m_numberType = 0;
				this.m_bZero = false;
				this.m_bPositive = false;
				this.m_bGetInfo = false;
			}
			this.m_bNotNull = true;
			this.m_format = null;
		}

		// Token: 0x06000BDF RID: 3039 RVA: 0x000783A0 File Offset: 0x000773A0
		internal static OracleDecimal SetPi()
		{
			int num = 0;
			IntPtr zero = IntPtr.Zero;
			try
			{
				num = OpsDec.AllocValCtxFromPi(out zero);
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
					OpoDecCtx.FreeCtx(ref zero);
					if (num != ErrRes.INT_ERR)
					{
						throw new OracleTypeException(num, new object[0]);
					}
				}
			}
			return new OracleDecimal(zero, false);
		}

		// Token: 0x06000BE0 RID: 3040 RVA: 0x0007841C File Offset: 0x0007741C
		internal unsafe static int Compare(OpoDecCtx value1, OpoDecCtx value2)
		{
			int num = 0;
			int result;
			try
			{
				num = OpsDec.Compare(value1.m_pValCtx, value2.m_pValCtx, &result);
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

		// Token: 0x06000BE1 RID: 3041 RVA: 0x00078474 File Offset: 0x00077474
		internal static OracleDecimal GetMaxValue()
		{
			string numStr = "9.9999999999999999999999999999999999999E+125";
			IntPtr zero = IntPtr.Zero;
			int num = 0;
			try
			{
				num = OpsDec.AllocValCtxFromNoFmtStr(numStr, out zero);
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
					OpoDecCtx.FreeCtx(ref zero);
					if (num != ErrRes.INT_ERR)
					{
						throw new OracleTypeException(num, new object[0]);
					}
				}
			}
			return new OracleDecimal(zero, false);
		}

		// Token: 0x06000BE2 RID: 3042 RVA: 0x000784F8 File Offset: 0x000774F8
		internal static OracleDecimal GetMinValue()
		{
			string numStr = "-9.9999999999999999999999999999999999999E+125";
			IntPtr zero = IntPtr.Zero;
			int num = 0;
			try
			{
				num = OpsDec.AllocValCtxFromNoFmtStr(numStr, out zero);
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
					OpoDecCtx.FreeCtx(ref zero);
					if (num != ErrRes.INT_ERR)
					{
						throw new OracleTypeException(num, new object[0]);
					}
				}
			}
			return new OracleDecimal(zero, false);
		}

		// Token: 0x06000BE3 RID: 3043 RVA: 0x0007857C File Offset: 0x0007757C
		internal static OracleDecimal GetPosInfinity()
		{
			IntPtr zero = IntPtr.Zero;
			int num = 0;
			try
			{
				num = OpsDec.AllocValCtxForPosInf(out zero);
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
					OpoDecCtx.FreeCtx(ref zero);
					if (num != ErrRes.INT_ERR)
					{
						throw new OracleTypeException(num, new object[0]);
					}
				}
			}
			return new OracleDecimal(zero, false);
		}

		// Token: 0x06000BE4 RID: 3044 RVA: 0x000785F8 File Offset: 0x000775F8
		internal static OracleDecimal GetNegInfinity()
		{
			IntPtr zero = IntPtr.Zero;
			int num = 0;
			try
			{
				num = OpsDec.AllocValCtxForNegInf(out zero);
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
					OpoDecCtx.FreeCtx(ref zero);
					if (num != ErrRes.INT_ERR)
					{
						throw new OracleTypeException(num, new object[0]);
					}
				}
			}
			return new OracleDecimal(zero, false);
		}

		// Token: 0x06000BE5 RID: 3045 RVA: 0x00078674 File Offset: 0x00077674
		internal unsafe static IntPtr AllocValCtx(object methodParam)
		{
			IntPtr zero = IntPtr.Zero;
			if (methodParam is char || methodParam is sbyte || methodParam is byte)
			{
				int num = Convert.ToInt32(methodParam);
				OpsDec.AllocValCtxFromInteger((void*)(&num), 4, ref zero);
			}
			else
			{
				if (methodParam is byte[])
				{
					if (((byte[])methodParam).Length != 22)
					{
						throw new ArgumentException(OpoErrResManager.GetErrorMesg(ErrRes.PRM_INVALID_BIND, new string[0]), methodParam.ToString());
					}
					int num2 = 0;
					GCHandle gchandle = GCHandle.Alloc((byte[])methodParam, GCHandleType.Pinned);
					try
					{
						num2 = OpsDec.AllocValCtxFromBytes(gchandle.AddrOfPinnedObject(), out zero);
						return zero;
					}
					finally
					{
						if (gchandle.IsAllocated)
						{
							gchandle.Free();
						}
						if (num2 != 0)
						{
							throw new OracleTypeException(num2, new object[0]);
						}
					}
				}
				if (methodParam is short || methodParam is int)
				{
					int num3 = Convert.ToInt32(methodParam);
					OpsDec.AllocValCtxFromInteger((void*)(&num3), 4, ref zero);
				}
				else if (methodParam is long)
				{
					long num4 = (long)methodParam;
					OpsDec.AllocValCtxFromInteger((void*)(&num4), 8, ref zero);
				}
				else if (methodParam is float)
				{
					OracleDecimal oracleDecimal = new OracleDecimal((double)((float)methodParam));
					OpsDec.AllocValCtxForSetPrecNoRound(oracleDecimal.m_opoDecCtx.m_pValCtx, 7, out zero);
				}
				else if (methodParam is double)
				{
					OracleDecimal oracleDecimal2 = new OracleDecimal((double)methodParam);
					OpsDec.AllocValCtxFromBytes(oracleDecimal2.m_opoDecCtx.m_pValCtx, out zero);
				}
				else if (methodParam is decimal)
				{
					OracleDecimal oracleDecimal3 = new OracleDecimal((decimal)methodParam);
					OpsDec.AllocValCtxFromBytes(oracleDecimal3.m_opoDecCtx.m_pValCtx, out zero);
				}
				else if (methodParam is string)
				{
					OracleDecimal oracleDecimal4 = new OracleDecimal((string)methodParam);
					OpsDec.AllocValCtxFromBytes(oracleDecimal4.m_opoDecCtx.m_pValCtx, out zero);
				}
				else if (methodParam is OracleDecimal)
				{
					OpsDec.AllocValCtxFromBytes(((OracleDecimal)methodParam).m_opoDecCtx.m_pValCtx, out zero);
				}
				else if (methodParam is OracleString)
				{
					OracleDecimal oracleDecimal5 = new OracleDecimal(((OracleString)methodParam).Value);
					OpsDec.AllocValCtxFromBytes(oracleDecimal5.m_opoDecCtx.m_pValCtx, out zero);
				}
				else
				{
					OracleDecimal oracleDecimal6 = new OracleDecimal(Convert.ToDecimal(methodParam));
					OpsDec.AllocValCtxFromBytes(oracleDecimal6.m_opoDecCtx.m_pValCtx, out zero);
				}
			}
			return zero;
		}

		// Token: 0x06000BE6 RID: 3046 RVA: 0x000788B4 File Offset: 0x000778B4
		internal static int ConvertToInt(object methodParam)
		{
			int result = 0;
			if (methodParam is byte[])
			{
				if (((byte[])methodParam).Length != 22)
				{
					throw new InvalidCastException();
				}
				GCHandle gchandle = GCHandle.Alloc((byte[])methodParam, GCHandleType.Pinned);
				try
				{
					return (int)DecimalConv.GetNum(gchandle.AddrOfPinnedObject(), DbType.Int32);
				}
				finally
				{
					if (gchandle.IsAllocated)
					{
						gchandle.Free();
					}
				}
			}
			if (methodParam is OracleDecimal)
			{
				result = ((OracleDecimal)methodParam).ToInt32();
			}
			else if (methodParam is OracleString)
			{
				result = Convert.ToInt32(((OracleString)methodParam).Value);
			}
			else
			{
				result = Convert.ToInt32(methodParam);
			}
			return result;
		}

		// Token: 0x06000BE7 RID: 3047 RVA: 0x00078960 File Offset: 0x00077960
		internal void KeepValCtx()
		{
			if (this.m_opoDecCtx != null)
			{
				this.m_opoDecCtx.m_DoNotFreeValCtx = true;
			}
		}

		// Token: 0x04000954 RID: 2388
		internal const int NUMSIZE = 22;

		// Token: 0x04000955 RID: 2389
		internal const int LNXSUCC = 0;

		// Token: 0x04000956 RID: 2390
		internal const int LNXIBIG = 1;

		// Token: 0x04000957 RID: 2391
		internal const int LNXISMALL = 2;

		// Token: 0x04000958 RID: 2392
		internal const int LNXIINVALIDNUM = 3;

		// Token: 0x04000959 RID: 2393
		internal const int LNXFAIL = -1;

		// Token: 0x0400095A RID: 2394
		internal const byte MinPrecision = 1;

		// Token: 0x0400095B RID: 2395
		public static readonly byte MaxPrecision = 38;

		// Token: 0x0400095C RID: 2396
		public static readonly byte MaxScale = 127;

		// Token: 0x0400095D RID: 2397
		public static readonly OracleDecimal MaxValue = OracleDecimal.GetMaxValue();

		// Token: 0x0400095E RID: 2398
		public static readonly int MinScale = -84;

		// Token: 0x0400095F RID: 2399
		public static readonly OracleDecimal MinValue = OracleDecimal.GetMinValue();

		// Token: 0x04000960 RID: 2400
		public static readonly OracleDecimal NegativeOne = new OracleDecimal(-1);

		// Token: 0x04000961 RID: 2401
		public static readonly OracleDecimal Null;

		// Token: 0x04000962 RID: 2402
		public static readonly OracleDecimal One = new OracleDecimal(1);

		// Token: 0x04000963 RID: 2403
		private static readonly decimal Pivalue = 3.1415926535897932384626433832m;

		// Token: 0x04000964 RID: 2404
		public static readonly OracleDecimal Pi = OracleDecimal.SetPi();

		// Token: 0x04000965 RID: 2405
		public static readonly OracleDecimal Zero = new OracleDecimal(0);

		// Token: 0x04000966 RID: 2406
		internal static readonly OracleDecimal PositiveInfinity = OracleDecimal.GetPosInfinity();

		// Token: 0x04000967 RID: 2407
		internal static readonly OracleDecimal NegativeInfinity = OracleDecimal.GetNegInfinity();

		// Token: 0x04000968 RID: 2408
		internal OpoDecCtx m_opoDecCtx;

		// Token: 0x04000969 RID: 2409
		private bool m_bPositive;

		// Token: 0x0400096A RID: 2410
		private int m_numberType;

		// Token: 0x0400096B RID: 2411
		private bool m_bZero;

		// Token: 0x0400096C RID: 2412
		private string m_format;

		// Token: 0x0400096D RID: 2413
		private bool m_bNotNull;

		// Token: 0x0400096E RID: 2414
		private bool m_bGetInfo;
	}
}
