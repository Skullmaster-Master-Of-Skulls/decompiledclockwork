using System;
using System.Collections;
using System.Collections.Specialized;
using System.Configuration;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Xml;
using Oracle.DataAccess.Client;

namespace Oracle.DataAccess.Types
{
	// Token: 0x02000027 RID: 39
	[ReflectionPermission(SecurityAction.Assert, Unrestricted = true)]
	[SecurityPermission(SecurityAction.Assert, UnmanagedCode = true)]
	public class OracleUdt
	{
		// Token: 0x06000194 RID: 404 RVA: 0x000149CC File Offset: 0x000139CC
		static OracleUdt()
		{
			if (!OracleInit.bSetDllDirectoryInvoked)
			{
				OracleInit.Initialize();
			}
		}

		// Token: 0x06000195 RID: 405 RVA: 0x000149F8 File Offset: 0x000139F8
		public unsafe static bool IsDBNull(OracleConnection con, IntPtr pUdt, int attrIndex)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleUdt::IsDBNull(0)\n"
				});
			}
			if (con == null)
			{
				throw new ArgumentNullException("con");
			}
			if (pUdt == IntPtr.Zero)
			{
				throw new ArgumentException("pUdt");
			}
			OpoUdtValCtx* ptr = (OpoUdtValCtx*)((void*)pUdt);
			OpoUdtValCtx* ptr3;
			if (ptr->pOpoDscValCtx->TypeCode == 108)
			{
				if (attrIndex < 0 || (long)attrIndex >= (long)((ulong)ptr->pOpoDscValCtx->NumAttrs))
				{
					throw new ArgumentOutOfRangeException("attrIndex");
				}
				AttrMetaVal* ptr2 = ptr->pOpoDscValCtx->pAttrMetaVals + attrIndex;
				if (ptr2->CustTypeCode == (CustomTypeCode)0)
				{
					throw new ArgumentException(OpoErrResManager.GetErrorMesg(-2905, new string[0]));
				}
				ptr3 = ptr->pOpoUdtValCtx + attrIndex;
			}
			else
			{
				if (attrIndex != 0)
				{
					throw new ArgumentOutOfRangeException("attrIndex");
				}
				ptr3 = ptr;
			}
			bool result = ptr3->bIsNull == 1;
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleUdt::IsDBNull(0)\n"
				});
			}
			return result;
		}

		// Token: 0x06000196 RID: 406 RVA: 0x00014B0C File Offset: 0x00013B0C
		public unsafe static bool IsDBNull(OracleConnection con, IntPtr pUdt, string attrName)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleUdt::IsDBNull(1)\n"
				});
			}
			if (con == null)
			{
				throw new ArgumentNullException("con");
			}
			if (pUdt == IntPtr.Zero)
			{
				throw new ArgumentException("pUdt");
			}
			OpoUdtValCtx* ptr = (OpoUdtValCtx*)((void*)pUdt);
			int attrIndex;
			if (ptr->pOpoDscValCtx->TypeCode == 108)
			{
				if (attrName == null)
				{
					throw new ArgumentNullException("attrName");
				}
				if (attrName == "")
				{
					throw new ArgumentException("attrName");
				}
				bool flag;
				OracleUdtDescriptor oracleUdtDescriptor = OracleUdtDescriptor.GetOracleUdtDescriptor(con, (IntPtr)(*(IntPtr*)((void*)ptr->pTDO)), false, out flag);
				oracleUdtDescriptor.GetMetaDataTable();
				object obj = oracleUdtDescriptor.m_attrNameToIndex[attrName];
				if (obj == null)
				{
					throw new ArgumentException("attrName");
				}
				attrIndex = (int)obj;
			}
			else
			{
				if (attrName != null && !(attrName == ""))
				{
					throw new ArgumentException("attrName");
				}
				attrIndex = 0;
			}
			bool result = OracleUdt.IsDBNull(con, pUdt, attrIndex);
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleUdt::IsDBNull(1)\n"
				});
			}
			return result;
		}

		// Token: 0x06000197 RID: 407 RVA: 0x00014C30 File Offset: 0x00013C30
		public unsafe static object GetValue(OracleConnection con, IntPtr pUdt, int attrIndex)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleUdt::GetValue(0)\n"
				});
			}
			if (con == null)
			{
				throw new ArgumentNullException("con");
			}
			if (pUdt == IntPtr.Zero)
			{
				throw new ArgumentException("pUdt");
			}
			OpoUdtValCtx* ptr = (OpoUdtValCtx*)((void*)pUdt);
			ptr->bIgnoreElemStatus = 1;
			object value;
			try
			{
				object obj;
				value = OracleUdt.GetValue(con, pUdt, attrIndex, out obj);
			}
			finally
			{
				ptr->bIgnoreElemStatus = 0;
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleUdt::GetValue(0)\n"
				});
			}
			return value;
		}

		// Token: 0x06000198 RID: 408 RVA: 0x00014CD4 File Offset: 0x00013CD4
		public unsafe static object GetValue(OracleConnection con, IntPtr pUdt, int attrIndex, out object statusArray)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleUdt::GetValue(1)\n"
				});
			}
			if (con == null)
			{
				throw new ArgumentNullException("con");
			}
			if (pUdt == IntPtr.Zero)
			{
				throw new ArgumentException("pUdt");
			}
			OpoUdtValCtx* ptr = (OpoUdtValCtx*)((void*)pUdt);
			object result;
			if (ptr->pOpoDscValCtx->TypeCode == 108)
			{
				if (attrIndex < 0 || (long)attrIndex >= (long)((ulong)ptr->pOpoDscValCtx->NumAttrs))
				{
					throw new ArgumentOutOfRangeException("attrIndex");
				}
				OracleUdtStatus oracleUdtStatus;
				result = OracleUdt.GetData(con, pUdt, attrIndex, out oracleUdtStatus, out statusArray);
			}
			else
			{
				if (attrIndex != 0)
				{
					throw new ArgumentOutOfRangeException("attrIndex");
				}
				OracleUdtStatus oracleUdtStatus;
				result = OracleUdt.GetArrData(con, pUdt, out oracleUdtStatus, out statusArray);
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleUdt::GetValue(1)\n"
				});
			}
			return result;
		}

		// Token: 0x06000199 RID: 409 RVA: 0x00014DA4 File Offset: 0x00013DA4
		public unsafe static object GetValue(OracleConnection con, IntPtr pUdt, string attrName)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleUdt::GetValue(2)\n"
				});
			}
			if (con == null)
			{
				throw new ArgumentNullException("con");
			}
			if (pUdt == IntPtr.Zero)
			{
				throw new ArgumentException("pUdt");
			}
			OpoUdtValCtx* ptr = (OpoUdtValCtx*)((void*)pUdt);
			ptr->bIgnoreElemStatus = 1;
			object value;
			try
			{
				object obj;
				value = OracleUdt.GetValue(con, pUdt, attrName, out obj);
			}
			finally
			{
				ptr->bIgnoreElemStatus = 0;
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleUdt::GetValue(2)\n"
				});
			}
			return value;
		}

		// Token: 0x0600019A RID: 410 RVA: 0x00014E48 File Offset: 0x00013E48
		public unsafe static object GetValue(OracleConnection con, IntPtr pUdt, string attrName, out object statusArray)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleUdt::GetValue(3)\n"
				});
			}
			if (con == null)
			{
				throw new ArgumentNullException("con");
			}
			if (pUdt == IntPtr.Zero)
			{
				throw new ArgumentException("pUdt");
			}
			OpoUdtValCtx* ptr = (OpoUdtValCtx*)((void*)pUdt);
			int attrIndex;
			if (ptr->pOpoDscValCtx->TypeCode == 108)
			{
				if (attrName == null)
				{
					throw new ArgumentNullException("attrName");
				}
				if (attrName == "")
				{
					throw new ArgumentException("attrName");
				}
				bool flag;
				OracleUdtDescriptor oracleUdtDescriptor = OracleUdtDescriptor.GetOracleUdtDescriptor(con, (IntPtr)(*(IntPtr*)((void*)ptr->pTDO)), false, out flag);
				oracleUdtDescriptor.GetMetaDataTable();
				object obj = oracleUdtDescriptor.m_attrNameToIndex[attrName];
				if (obj == null)
				{
					throw new ArgumentException("attrName");
				}
				attrIndex = (int)obj;
			}
			else
			{
				if (attrName != null && !(attrName == ""))
				{
					throw new ArgumentException("attrName");
				}
				attrIndex = 0;
			}
			object value = OracleUdt.GetValue(con, pUdt, attrIndex, out statusArray);
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleUdt::GetValue(2)\n"
				});
			}
			return value;
		}

		// Token: 0x0600019B RID: 411 RVA: 0x00014F70 File Offset: 0x00013F70
		internal unsafe static object GetData(OracleConnection con, IntPtr pUdt, int index, out OracleUdtStatus status, out object statusArray)
		{
			int num = 0;
			statusArray = null;
			OpoUdtValCtx* ptr = (OpoUdtValCtx*)((void*)pUdt);
			AttrMetaVal* ptr2;
			if (ptr->pOpoDscValCtx->TypeCode == 108)
			{
				ptr2 = ptr->pOpoDscValCtx->pAttrMetaVals + index;
				if (ptr->bIsOdtConnection == 0 && ptr2->CustTypeCode == (CustomTypeCode)0)
				{
					throw new ArgumentException(OpoErrResManager.GetErrorMesg(-2905, new string[0]));
				}
			}
			else
			{
				ptr2 = ptr->pOpoDscValCtx->pAttrMetaVals;
			}
			SQLT oraType = (SQLT)ptr2->OraType;
			if (oraType <= SQLT.NCO)
			{
				switch (oraType)
				{
				case SQLT.CHR:
				case SQLT.STR:
				case SQLT.LNG:
				case SQLT.VCS:
					goto IL_1ED;
				case SQLT.NUM:
				case SQLT.INT:
				case SQLT.FLT:
				case SQLT.VNU:
					break;
				case SQLT.PDN:
				case SQLT.NON:
				case SQLT.RID:
					goto IL_4CF;
				case SQLT.DAT:
					goto IL_448;
				case (SQLT)13:
				case (SQLT)14:
				case (SQLT)16:
				case (SQLT)17:
				case (SQLT)18:
				case (SQLT)19:
				case (SQLT)20:
					goto IL_4D5;
				case SQLT.VBI:
				case SQLT.BIN:
				case SQLT.LBI:
					goto IL_4B7;
				case SQLT.BFLT:
					goto IL_47B;
				case SQLT.BDBL:
					goto IL_487;
				default:
					if (oraType != SQLT.UIN)
					{
						switch (oraType)
						{
						case SQLT.SLS:
						case SQLT.CUR:
						case SQLT.RDD:
						case SQLT.LAB:
						case SQLT.OSL:
						case SQLT.RSET:
							goto IL_4CF;
						case (SQLT)92:
						case (SQLT)93:
						case (SQLT)98:
						case (SQLT)99:
						case (SQLT)103:
						case (SQLT)107:
						case (SQLT)109:
						case (SQLT)111:
						case (SQLT)117:
						case (SQLT)118:
						case (SQLT)119:
						case (SQLT)120:
						case (SQLT)121:
							goto IL_4D5;
						case SQLT.LVC:
						case SQLT.AFC:
						case SQLT.AVC:
							goto IL_1ED;
						case SQLT.LVB:
							goto IL_4B7;
						case SQLT.IBFL:
							goto IL_47B;
						case SQLT.IBDL:
							goto IL_487;
						case SQLT.NTY:
						case SQLT.NCO:
							goto IL_1FC;
						case SQLT.REF:
							return OracleUdt.GetRefData(con, pUdt, index, out status);
						case SQLT.CLOB:
						case SQLT.CFILEE:
							return OracleUdt.GetClobData(con, pUdt, index, out status);
						case SQLT.BLOB:
							return OracleUdt.GetBlobData(con, pUdt, index, out status);
						case SQLT.BFILEE:
							return OracleUdt.GetBFileData(con, pUdt, index, out status);
						default:
							goto IL_4D5;
						}
					}
					break;
				}
				return OracleUdt.GetNumData(con, pUdt, index, out status);
				IL_47B:
				return OracleUdt.GetFltData(con, pUdt, index, out status);
				IL_487:
				return OracleUdt.GetDblData(con, pUdt, index, out status);
				IL_4B7:
				return OracleUdt.GetRawData(con, pUdt, index, out status);
				IL_4CF:
				throw new NotSupportedException();
			}
			if (oraType <= SQLT.INTERVAL_DS)
			{
				switch (oraType)
				{
				case SQLT.VST:
					goto IL_1ED;
				case SQLT.ODT:
					goto IL_448;
				default:
					switch (oraType)
					{
					case SQLT.DATE:
						goto IL_448;
					case SQLT.TIME:
					case SQLT.TIME_TZ:
					case SQLT.TIMESTAMP:
					case SQLT.TIMESTAMP_TZ:
						break;
					case SQLT.INTERVAL_YM:
						return OracleUdt.GetIYMData(con, pUdt, index, out status);
					case SQLT.INTERVAL_DS:
						return OracleUdt.GetIDSData(con, pUdt, index, out status);
					default:
						goto IL_4D5;
					}
					break;
				}
			}
			else if (oraType != SQLT.TIMESTAMP_LTZ)
			{
				if (oraType != SQLT.PNTY)
				{
					goto IL_4D5;
				}
				goto IL_1FC;
			}
			return OracleUdt.GetTSData(con, pUdt, index, out status);
			IL_1ED:
			return OracleUdt.GetStrData(con, pUdt, index, out status);
			IL_1FC:
			if (ptr2->TypeCode == 108)
			{
				return OracleUdt.GetObjData(con, pUdt, index, out status);
			}
			if (ptr2->TypeCode == 58)
			{
				return OracleUdt.GetXmlData(con, pUdt, index, out status);
			}
			OpoUdtValCtx* ptr3 = ptr->pOpoUdtValCtx + index;
			ptr3->pOpsErrCtx = ptr->pOpsErrCtx;
			ptr3->bIgnoreElemStatus = ptr->bIgnoreElemStatus;
			ptr3->pOpoDscValCtx = ptr2->pOpoDscValCtx;
			AttrMetaVal* ptr4;
			if (ptr->pOpoDscValCtx->TypeCode == 108)
			{
				ptr4 = ptr3->pOpoDscValCtx->pAttrMetaVals;
			}
			else
			{
				ptr4 = ptr2;
			}
			if (ptr3->pOpoDscValCtx->bIsArrayType == 1 && ptr4->OraType == 2 && (ptr4->CustTypeCode == CustomTypeCode.Int32 || ptr4->CustTypeCode == CustomTypeCode.Double))
			{
				return OracleUdt.GetArrData(con, (IntPtr)((void*)ptr3), out status, out statusArray);
			}
			bool flag;
			OracleUdtDescriptor oracleUdtDescriptor = OracleUdtDescriptor.GetOracleUdtDescriptor(con, (IntPtr)(*(IntPtr*)((void*)ptr->pTDO)), false, out flag);
			OracleUdtDescriptor oracleUdtDescriptor2;
			if (ptr->pOpoDscValCtx->TypeCode == 108)
			{
				oracleUdtDescriptor2 = oracleUdtDescriptor.GetObjAttrUdtDescriptor(index);
			}
			else
			{
				oracleUdtDescriptor2 = oracleUdtDescriptor.GetArrElemUdtDescriptor();
			}
			if (oracleUdtDescriptor2.m_customTypeFactory == null)
			{
				object factory = OracleUdt.GetFactory(oracleUdtDescriptor2);
				if (factory != null)
				{
					oracleUdtDescriptor2.DescribeCustomType(factory);
				}
			}
			ptr3->pTDO = oracleUdtDescriptor2.m_opsDscCtx;
			ptr3->ppRefTDO = ptr->ppRefTDO;
			ptr3->pOpoDscValCtx = oracleUdtDescriptor2.m_pOpoDscValCtx;
			if (ptr4->OraType == 108 && ptr3->bIsNull != 1)
			{
				try
				{
					num = OpsUdt.GetArr(con.m_opoConCtx.opsConCtx, ptr3);
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
					if (num != 0 && num != ErrRes.INT_ERR)
					{
						OracleException.HandleError(num, con, ptr3->pOpsErrCtx, null);
					}
				}
			}
			object result;
			if (oracleUdtDescriptor2.m_pOpoDscValCtx->bIsArrayType == 0)
			{
				IOracleCustomTypeFactory oracleCustomTypeFactory = (IOracleCustomTypeFactory)oracleUdtDescriptor2.m_customTypeFactory;
				IOracleCustomType oracleCustomType = oracleCustomTypeFactory.CreateObject();
				if (ptr3->bIsNull == 1)
				{
					status = OracleUdtStatus.Null;
					Type type = oracleCustomType.GetType();
					PropertyInfo property = type.GetProperty("Null");
					result = property.GetValue(null, null);
				}
				else
				{
					status = OracleUdtStatus.NotNull;
					ptr3->bIgnoreElemStatus = 0;
					oracleCustomType.ToCustomObject(con, (IntPtr)((void*)ptr3));
					result = oracleCustomType;
				}
			}
			else
			{
				result = OracleUdt.GetArrData(con, (IntPtr)((void*)ptr3), out status, out statusArray);
			}
			GC.KeepAlive(oracleUdtDescriptor2);
			return result;
			IL_448:
			return OracleUdt.GetDatData(con, pUdt, index, out status);
			IL_4D5:
			result = null;
			status = OracleUdtStatus.Null;
			return result;
		}

		// Token: 0x0600019C RID: 412 RVA: 0x00015474 File Offset: 0x00014474
		internal unsafe static object GetArrData(OracleConnection con, IntPtr pUdt, out OracleUdtStatus status, out object statusArray)
		{
			OpoUdtValCtx* ptr = (OpoUdtValCtx*)((void*)pUdt);
			AttrMetaVal* ptr2 = ptr->pOpoDscValCtx->pAttrMetaVals;
			if (ptr->bIsNull == 1)
			{
				status = OracleUdtStatus.Null;
				statusArray = null;
				return null;
			}
			object result;
			if (ptr2->OraType == 2 && (ptr2->CustTypeCode == CustomTypeCode.Int32 || ptr2->CustTypeCode == CustomTypeCode.Double))
			{
				SQLT oraType = (SQLT)ptr2->OraType;
				if (oraType == SQLT.NUM)
				{
					result = OracleUdt.GetNumArrData(con, pUdt, out status, out statusArray);
				}
				else
				{
					status = OracleUdtStatus.NotNull;
					statusArray = null;
					result = null;
				}
			}
			else
			{
				bool flag;
				OracleUdtDescriptor oracleUdtDescriptor = OracleUdtDescriptor.GetOracleUdtDescriptor(con, (IntPtr)(*(IntPtr*)((void*)ptr->pTDO)), false, out flag);
				if (oracleUdtDescriptor.m_customTypeFactory == null)
				{
					object factory = OracleUdt.GetFactory(oracleUdtDescriptor);
					if (factory != null)
					{
						oracleUdtDescriptor.DescribeCustomType(factory);
					}
				}
				IOracleArrayTypeFactory oracleArrayTypeFactory = (IOracleArrayTypeFactory)oracleUdtDescriptor.m_customTypeFactory;
				Array array = oracleArrayTypeFactory.CreateArray(ptr->NumOfArrElems);
				Array array2 = null;
				if (ptr->bIgnoreElemStatus == 0)
				{
					array2 = oracleArrayTypeFactory.CreateStatusArray(ptr->NumOfArrElems);
				}
				for (int i = 0; i < ptr->NumOfArrElems; i++)
				{
					OracleUdtStatus oracleUdtStatus;
					object obj;
					object data = OracleUdt.GetData(con, pUdt, i, out oracleUdtStatus, out obj);
					if (oracleUdtStatus == OracleUdtStatus.NotNull)
					{
						array.SetValue(data, i);
						if (ptr->bIgnoreElemStatus == 0)
						{
							if (obj != null)
							{
								array2.SetValue(obj, i);
							}
							else
							{
								array2.SetValue(OracleUdtStatus.NotNull, i);
							}
						}
					}
				}
				status = OracleUdtStatus.NotNull;
				statusArray = array2;
				result = array;
			}
			return result;
		}

		// Token: 0x0600019D RID: 413 RVA: 0x000155C8 File Offset: 0x000145C8
		internal unsafe static object GetNumData(OracleConnection con, IntPtr pUdt, int index, out OracleUdtStatus status)
		{
			status = OracleUdtStatus.Null;
			OpoUdtValCtx* ptr = (OpoUdtValCtx*)((void*)pUdt);
			OpoUdtValCtx* ptr2 = ptr->pOpoUdtValCtx + index;
			AttrMetaVal* ptr3;
			if (ptr->pOpoDscValCtx->TypeCode == 108)
			{
				ptr3 = ptr->pOpoDscValCtx->pAttrMetaVals + index;
			}
			else
			{
				ptr3 = ptr->pOpoDscValCtx->pAttrMetaVals;
			}
			CustomTypeCode customTypeCode = ptr3->CustTypeCode;
			if (ptr->bIsOdtConnection == 1)
			{
				customTypeCode = CustomTypeCode.OracleDecimal;
			}
			CustomTypeCode customTypeCode2 = customTypeCode;
			object result;
			switch (customTypeCode2)
			{
			case CustomTypeCode.Byte:
				if (ptr2->bIsNull != 1)
				{
					status = OracleUdtStatus.NotNull;
					return ptr2->opoUdtAttrValCtx.m_byte;
				}
				status = OracleUdtStatus.Null;
				if (ptr3->IsNullable == 1)
				{
					return null;
				}
				if (ptr->pOpoDscValCtx->TypeCode == 108)
				{
					return DBNull.Value;
				}
				return 0;
			case CustomTypeCode.Bytes:
			case CustomTypeCode.Chars:
			case CustomTypeCode.DateTime:
				break;
			case CustomTypeCode.Decimal:
				if (ptr2->bIsNull != 1)
				{
					status = OracleUdtStatus.NotNull;
					return DecimalConv.GetDecimal(ptr2->pUDT);
				}
				status = OracleUdtStatus.Null;
				if (ptr3->IsNullable == 1)
				{
					return null;
				}
				if (ptr->pOpoDscValCtx->TypeCode == 108)
				{
					return DBNull.Value;
				}
				return 0;
			case CustomTypeCode.Double:
				if (ptr2->bIsNull != 1)
				{
					status = OracleUdtStatus.NotNull;
					return ptr2->opoUdtAttrValCtx.m_double;
				}
				status = OracleUdtStatus.Null;
				if (ptr3->IsNullable == 1)
				{
					return null;
				}
				if (ptr->pOpoDscValCtx->TypeCode == 108)
				{
					return DBNull.Value;
				}
				return 0;
			case CustomTypeCode.Int16:
				if (ptr2->bIsNull != 1)
				{
					status = OracleUdtStatus.NotNull;
					return ptr2->opoUdtAttrValCtx.m_short;
				}
				status = OracleUdtStatus.Null;
				if (ptr3->IsNullable == 1)
				{
					return null;
				}
				if (ptr->pOpoDscValCtx->TypeCode == 108)
				{
					return DBNull.Value;
				}
				return 0;
			case CustomTypeCode.Int32:
				if (ptr2->bIsNull != 1)
				{
					status = OracleUdtStatus.NotNull;
					return ptr2->opoUdtAttrValCtx.m_int;
				}
				status = OracleUdtStatus.Null;
				if (ptr3->IsNullable == 1)
				{
					return null;
				}
				if (ptr->pOpoDscValCtx->TypeCode == 108)
				{
					return DBNull.Value;
				}
				return 0;
			case CustomTypeCode.Int64:
				if (ptr2->bIsNull != 1)
				{
					status = OracleUdtStatus.NotNull;
					return ptr2->opoUdtAttrValCtx.m_long;
				}
				status = OracleUdtStatus.Null;
				if (ptr3->IsNullable == 1)
				{
					return null;
				}
				if (ptr->pOpoDscValCtx->TypeCode == 108)
				{
					return DBNull.Value;
				}
				return 0L;
			case CustomTypeCode.Single:
				if (ptr2->bIsNull != 1)
				{
					status = OracleUdtStatus.NotNull;
					return ptr2->opoUdtAttrValCtx.m_float;
				}
				status = OracleUdtStatus.Null;
				if (ptr3->IsNullable == 1)
				{
					return null;
				}
				if (ptr->pOpoDscValCtx->TypeCode == 108)
				{
					return DBNull.Value;
				}
				return 0;
			default:
				if (customTypeCode2 == CustomTypeCode.OracleDecimal)
				{
					if (ptr2->bIsNull == 1)
					{
						status = OracleUdtStatus.Null;
						return OracleDecimal.Null;
					}
					status = OracleUdtStatus.NotNull;
					result = new OracleDecimal(ptr2->pDataTmpBuffer);
					ptr2->pDataTmpBuffer = IntPtr.Zero;
					return result;
				}
				break;
			}
			status = OracleUdtStatus.Null;
			result = null;
			return result;
		}

		// Token: 0x0600019E RID: 414 RVA: 0x0001593C File Offset: 0x0001493C
		internal unsafe static object GetStrData(OracleConnection con, IntPtr pUdt, int index, out OracleUdtStatus status)
		{
			OpoUdtValCtx* ptr = (OpoUdtValCtx*)((void*)pUdt);
			OpoUdtValCtx* ptr2 = ptr->pOpoUdtValCtx + index;
			AttrMetaVal* ptr3;
			if (ptr->pOpoDscValCtx->TypeCode == 108)
			{
				ptr3 = ptr->pOpoDscValCtx->pAttrMetaVals + index;
			}
			else
			{
				ptr3 = ptr->pOpoDscValCtx->pAttrMetaVals;
			}
			CustomTypeCode customTypeCode = ptr3->CustTypeCode;
			if (ptr->bIsOdtConnection == 1)
			{
				customTypeCode = CustomTypeCode.OracleString;
			}
			CustomTypeCode customTypeCode2 = customTypeCode;
			object result;
			if (customTypeCode2 != CustomTypeCode.Chars)
			{
				if (customTypeCode2 != CustomTypeCode.String)
				{
					if (customTypeCode2 != CustomTypeCode.OracleString)
					{
						status = OracleUdtStatus.Null;
						result = null;
					}
					else if (ptr2->bIsNull == 1)
					{
						status = OracleUdtStatus.Null;
						result = OracleString.Null;
					}
					else
					{
						status = OracleUdtStatus.NotNull;
						result = new OracleString(Marshal.PtrToStringUni(ptr2->pDataMarshalBuffer, ptr2->DataLen));
					}
				}
				else if (ptr2->bIsNull == 1)
				{
					status = OracleUdtStatus.Null;
					result = null;
				}
				else
				{
					status = OracleUdtStatus.NotNull;
					result = Marshal.PtrToStringUni(ptr2->pDataMarshalBuffer, ptr2->DataLen);
				}
			}
			else if (ptr2->bIsNull == 1)
			{
				status = OracleUdtStatus.Null;
				result = null;
			}
			else
			{
				status = OracleUdtStatus.NotNull;
				result = Marshal.PtrToStringUni(ptr2->pDataMarshalBuffer, ptr2->DataLen).ToCharArray();
			}
			return result;
		}

		// Token: 0x0600019F RID: 415 RVA: 0x00015A64 File Offset: 0x00014A64
		internal unsafe static object GetRawData(OracleConnection con, IntPtr pUdt, int index, out OracleUdtStatus status)
		{
			OpoUdtValCtx* ptr = (OpoUdtValCtx*)((void*)pUdt);
			OpoUdtValCtx* ptr2 = ptr->pOpoUdtValCtx + index;
			AttrMetaVal* ptr3;
			if (ptr->pOpoDscValCtx->TypeCode == 108)
			{
				ptr3 = ptr->pOpoDscValCtx->pAttrMetaVals + index;
			}
			else
			{
				ptr3 = ptr->pOpoDscValCtx->pAttrMetaVals;
			}
			CustomTypeCode customTypeCode = ptr3->CustTypeCode;
			if (ptr->bIsOdtConnection == 1)
			{
				customTypeCode = CustomTypeCode.OracleBinary;
			}
			CustomTypeCode customTypeCode2 = customTypeCode;
			object obj;
			if (customTypeCode2 != CustomTypeCode.Bytes)
			{
				if (customTypeCode2 != CustomTypeCode.OracleBinary)
				{
					status = OracleUdtStatus.Null;
					obj = null;
				}
				else if (ptr2->bIsNull == 1)
				{
					status = OracleUdtStatus.Null;
					obj = OracleBinary.Null;
				}
				else
				{
					status = OracleUdtStatus.NotNull;
					byte[] array = new byte[ptr2->DataLen];
					Marshal.Copy(ptr2->pUDT, array, 0, ptr2->DataLen);
					obj = new OracleBinary(array, false);
				}
			}
			else if (ptr2->bIsNull == 1)
			{
				status = OracleUdtStatus.Null;
				obj = null;
			}
			else
			{
				status = OracleUdtStatus.NotNull;
				obj = new byte[ptr2->DataLen];
				Marshal.Copy(ptr2->pUDT, (byte[])obj, 0, ptr2->DataLen);
			}
			return obj;
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x00015B7C File Offset: 0x00014B7C
		internal unsafe static object GetRefData(OracleConnection con, IntPtr pUdt, int index, out OracleUdtStatus status)
		{
			int num = 0;
			OpoUdtValCtx* ptr = (OpoUdtValCtx*)((void*)pUdt);
			OpoUdtValCtx* ptr2 = ptr->pOpoUdtValCtx + index;
			AttrMetaVal* ptr3;
			if (ptr->pOpoDscValCtx->TypeCode == 108)
			{
				ptr3 = ptr->pOpoDscValCtx->pAttrMetaVals + index;
			}
			else
			{
				ptr3 = ptr->pOpoDscValCtx->pAttrMetaVals;
			}
			CustomTypeCode customTypeCode = ptr3->CustTypeCode;
			if (ptr->bIsOdtConnection == 1)
			{
				customTypeCode = CustomTypeCode.OracleRef;
			}
			if (ptr2->bIsNull != 1)
			{
				try
				{
					num = OpsUdt.GetRef(con.m_opoConCtx.opsConCtx, ptr, index);
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
					if (num != 0)
					{
						OracleException.HandleError(num, con, ptr->pOpsErrCtx, null);
					}
				}
			}
			CustomTypeCode customTypeCode2 = customTypeCode;
			object result;
			if (customTypeCode2 != CustomTypeCode.String)
			{
				if (customTypeCode2 != CustomTypeCode.OracleRef)
				{
					status = OracleUdtStatus.Null;
					result = null;
				}
				else if (ptr2->bIsNull == 1)
				{
					status = OracleUdtStatus.Null;
					result = OracleRef.Null;
				}
				else
				{
					status = OracleUdtStatus.NotNull;
					result = new OracleRef(con, new OpoUdtCtx(con.m_opoConCtx.opsConCtx, IntPtr.Zero, ptr2->pDataTmpBuffer, IntPtr.Zero));
					ptr2->pDataTmpBuffer = IntPtr.Zero;
				}
			}
			else if (ptr2->bIsNull == 1)
			{
				status = OracleUdtStatus.Null;
				result = null;
			}
			else
			{
				status = OracleUdtStatus.NotNull;
				OracleRef oracleRef = new OracleRef(con, new OpoUdtCtx(con.m_opoConCtx.opsConCtx, IntPtr.Zero, ptr2->pDataTmpBuffer, IntPtr.Zero));
				ptr2->pDataTmpBuffer = IntPtr.Zero;
				result = oracleRef.Value;
				oracleRef.Dispose();
			}
			return result;
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x00015D1C File Offset: 0x00014D1C
		internal unsafe static object GetXmlData(OracleConnection con, IntPtr pUdt, int index, out OracleUdtStatus status)
		{
			int num = 0;
			OpoUdtValCtx* ptr = (OpoUdtValCtx*)((void*)pUdt);
			OpoUdtValCtx* ptr2 = ptr->pOpoUdtValCtx + index;
			AttrMetaVal* ptr3;
			if (ptr->pOpoDscValCtx->TypeCode == 108)
			{
				ptr3 = ptr->pOpoDscValCtx->pAttrMetaVals + index;
			}
			else
			{
				ptr3 = ptr->pOpoDscValCtx->pAttrMetaVals;
			}
			CustomTypeCode customTypeCode = ptr3->CustTypeCode;
			if (ptr->bIsOdtConnection == 1)
			{
				customTypeCode = CustomTypeCode.OracleXmlType;
			}
			if (ptr2->bIsNull != 1)
			{
				try
				{
					num = OpsUdt.GetXml(con.m_opoConCtx.opsConCtx, ptr, index);
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
					if (num != 0)
					{
						OracleException.HandleError(num, con, ptr->pOpsErrCtx, null);
					}
				}
			}
			CustomTypeCode customTypeCode2 = customTypeCode;
			object result;
			if (customTypeCode2 != CustomTypeCode.Chars)
			{
				if (customTypeCode2 != CustomTypeCode.String)
				{
					if (customTypeCode2 != CustomTypeCode.OracleXmlType)
					{
						status = OracleUdtStatus.Null;
						result = null;
					}
					else if (ptr2->bIsNull == 1)
					{
						status = OracleUdtStatus.Null;
						result = OracleXmlType.Null;
					}
					else
					{
						status = OracleUdtStatus.NotNull;
						result = new OracleXmlType(con, ptr2->pDataTmpBuffer, false);
						ptr2->pDataTmpBuffer = IntPtr.Zero;
					}
				}
				else if (ptr2->bIsNull == 1)
				{
					status = OracleUdtStatus.Null;
					result = null;
				}
				else
				{
					status = OracleUdtStatus.NotNull;
					OracleXmlType oracleXmlType = new OracleXmlType(con, ptr2->pDataTmpBuffer, false);
					ptr2->pDataTmpBuffer = IntPtr.Zero;
					result = oracleXmlType.Value;
					oracleXmlType.Dispose();
				}
			}
			else if (ptr2->bIsNull == 1)
			{
				status = OracleUdtStatus.Null;
				result = null;
			}
			else
			{
				status = OracleUdtStatus.NotNull;
				OracleXmlType oracleXmlType2 = new OracleXmlType(con, ptr2->pDataTmpBuffer, false);
				ptr2->pDataTmpBuffer = IntPtr.Zero;
				result = oracleXmlType2.Value.ToCharArray();
				oracleXmlType2.Dispose();
			}
			return result;
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x00015ED4 File Offset: 0x00014ED4
		internal unsafe static object GetDatData(OracleConnection con, IntPtr pUdt, int index, out OracleUdtStatus status)
		{
			OpoUdtValCtx* ptr = (OpoUdtValCtx*)((void*)pUdt);
			OpoUdtValCtx* ptr2 = ptr->pOpoUdtValCtx + index;
			AttrMetaVal* ptr3;
			if (ptr->pOpoDscValCtx->TypeCode == 108)
			{
				ptr3 = ptr->pOpoDscValCtx->pAttrMetaVals + index;
			}
			else
			{
				ptr3 = ptr->pOpoDscValCtx->pAttrMetaVals;
			}
			CustomTypeCode customTypeCode = ptr3->CustTypeCode;
			if (ptr->bIsOdtConnection == 1)
			{
				customTypeCode = CustomTypeCode.OracleDate;
			}
			CustomTypeCode customTypeCode2 = customTypeCode;
			object result;
			if (customTypeCode2 != CustomTypeCode.DateTime)
			{
				if (customTypeCode2 != CustomTypeCode.OracleDate)
				{
					status = OracleUdtStatus.Null;
					result = null;
				}
				else if (ptr2->bIsNull == 1)
				{
					status = OracleUdtStatus.Null;
					result = OracleDate.Null;
				}
				else
				{
					status = OracleUdtStatus.NotNull;
					result = new OracleDate(ptr2->pDataTmpBuffer);
					ptr2->pDataTmpBuffer = IntPtr.Zero;
				}
			}
			else if (ptr2->bIsNull == 1)
			{
				status = OracleUdtStatus.Null;
				if (ptr3->IsNullable == 1)
				{
					result = null;
				}
				else if (ptr->pOpoDscValCtx->TypeCode == 108)
				{
					result = DBNull.Value;
				}
				else
				{
					result = new DateTime(0L);
				}
			}
			else
			{
				status = OracleUdtStatus.NotNull;
				result = DateTimeConv.GetDateTime((OpoDatValCtx*)((void*)ptr2->pUDT), OracleDbType.Date, false);
			}
			return result;
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x00016004 File Offset: 0x00015004
		internal unsafe static object GetTSData(OracleConnection con, IntPtr pUdt, int index, out OracleUdtStatus status)
		{
			OpoUdtValCtx* ptr = (OpoUdtValCtx*)((void*)pUdt);
			OpoUdtValCtx* ptr2 = ptr->pOpoUdtValCtx + index;
			AttrMetaVal* ptr3;
			if (ptr->pOpoDscValCtx->TypeCode == 108)
			{
				ptr3 = ptr->pOpoDscValCtx->pAttrMetaVals + index;
			}
			else
			{
				ptr3 = ptr->pOpoDscValCtx->pAttrMetaVals;
			}
			CustomTypeCode customTypeCode = ptr3->CustTypeCode;
			if (ptr->bIsOdtConnection == 1)
			{
				if (ptr3->OraType == 232)
				{
					customTypeCode = CustomTypeCode.OracleTimeStampLTZ;
				}
				else if (ptr3->OraType == 188)
				{
					customTypeCode = CustomTypeCode.OracleTimeStampTZ;
				}
				else
				{
					customTypeCode = CustomTypeCode.OracleTimeStamp;
				}
			}
			CustomTypeCode customTypeCode2 = customTypeCode;
			object result;
			if (customTypeCode2 != CustomTypeCode.DateTime)
			{
				switch (customTypeCode2)
				{
				case CustomTypeCode.OracleTimeStamp:
					if (ptr2->bIsNull == 1)
					{
						status = OracleUdtStatus.Null;
						result = OracleTimeStamp.Null;
					}
					else
					{
						status = OracleUdtStatus.NotNull;
						result = new OracleTimeStamp(ptr2->pUDT);
					}
					break;
				case CustomTypeCode.OracleTimeStampLTZ:
					if (ptr2->bIsNull == 1)
					{
						status = OracleUdtStatus.Null;
						result = OracleTimeStampLTZ.Null;
					}
					else
					{
						status = OracleUdtStatus.NotNull;
						result = new OracleTimeStampLTZ(ptr2->pUDT);
					}
					break;
				case CustomTypeCode.OracleTimeStampTZ:
					if (ptr2->bIsNull == 1)
					{
						status = OracleUdtStatus.Null;
						result = OracleTimeStampTZ.Null;
					}
					else
					{
						status = OracleUdtStatus.NotNull;
						result = new OracleTimeStampTZ(ptr2->pUDT);
					}
					break;
				default:
					status = OracleUdtStatus.Null;
					result = null;
					break;
				}
			}
			else if (ptr2->bIsNull == 1)
			{
				status = OracleUdtStatus.Null;
				if (ptr3->IsNullable == 1)
				{
					result = null;
				}
				else if (ptr->pOpoDscValCtx->TypeCode == 108)
				{
					result = DBNull.Value;
				}
				else
				{
					result = new DateTime(0L);
				}
			}
			else
			{
				status = OracleUdtStatus.NotNull;
				OracleDbType oraType = (OracleDbType)0;
				if (ptr3->OraType == 187)
				{
					oraType = OracleDbType.TimeStamp;
				}
				else if (ptr3->OraType == 232)
				{
					oraType = OracleDbType.TimeStampLTZ;
				}
				else if (ptr3->OraType == 188)
				{
					oraType = OracleDbType.TimeStampTZ;
				}
				result = DateTimeConv.GetDateTime((OpoTSValCtx*)((void*)ptr2->pUDT), oraType, false);
			}
			return result;
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x00016204 File Offset: 0x00015204
		internal unsafe static object GetIDSData(OracleConnection con, IntPtr pUdt, int index, out OracleUdtStatus status)
		{
			OpoUdtValCtx* ptr = (OpoUdtValCtx*)((void*)pUdt);
			OpoUdtValCtx* ptr2 = ptr->pOpoUdtValCtx + index;
			AttrMetaVal* ptr3;
			if (ptr->pOpoDscValCtx->TypeCode == 108)
			{
				ptr3 = ptr->pOpoDscValCtx->pAttrMetaVals + index;
			}
			else
			{
				ptr3 = ptr->pOpoDscValCtx->pAttrMetaVals;
			}
			CustomTypeCode customTypeCode = ptr3->CustTypeCode;
			if (ptr->bIsOdtConnection == 1)
			{
				customTypeCode = CustomTypeCode.OracleIntervalDS;
			}
			CustomTypeCode customTypeCode2 = customTypeCode;
			object result;
			if (customTypeCode2 != CustomTypeCode.TimeSpan)
			{
				if (customTypeCode2 != CustomTypeCode.OracleIntervalDS)
				{
					status = OracleUdtStatus.Null;
					result = null;
				}
				else if (ptr2->bIsNull == 1)
				{
					status = OracleUdtStatus.Null;
					result = OracleIntervalDS.Null;
				}
				else
				{
					status = OracleUdtStatus.NotNull;
					result = new OracleIntervalDS((OpoITLValCtx*)((void*)ptr2->pDataTmpBuffer));
					ptr2->pDataTmpBuffer = IntPtr.Zero;
				}
			}
			else if (ptr2->bIsNull == 1)
			{
				status = OracleUdtStatus.Null;
				if (ptr3->IsNullable == 1)
				{
					result = null;
				}
				else if (ptr->pOpoDscValCtx->TypeCode == 108)
				{
					result = DBNull.Value;
				}
				else
				{
					result = new TimeSpan(0L);
				}
			}
			else
			{
				status = OracleUdtStatus.NotNull;
				result = TimeSpanConv.GetTimeSpan((OpoITLValCtx*)((void*)ptr2->pUDT), OracleDbType.IntervalDS);
			}
			return result;
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x00016338 File Offset: 0x00015338
		internal unsafe static object GetIYMData(OracleConnection con, IntPtr pUdt, int index, out OracleUdtStatus status)
		{
			OpoUdtValCtx* ptr = (OpoUdtValCtx*)((void*)pUdt);
			OpoUdtValCtx* ptr2 = ptr->pOpoUdtValCtx + index;
			AttrMetaVal* ptr3;
			if (ptr->pOpoDscValCtx->TypeCode == 108)
			{
				ptr3 = ptr->pOpoDscValCtx->pAttrMetaVals + index;
			}
			else
			{
				ptr3 = ptr->pOpoDscValCtx->pAttrMetaVals;
			}
			CustomTypeCode customTypeCode = ptr3->CustTypeCode;
			if (ptr->bIsOdtConnection == 1)
			{
				customTypeCode = CustomTypeCode.OracleIntervalYM;
			}
			CustomTypeCode customTypeCode2 = customTypeCode;
			object result;
			if (customTypeCode2 != CustomTypeCode.Int64)
			{
				if (customTypeCode2 != CustomTypeCode.OracleIntervalYM)
				{
					status = OracleUdtStatus.Null;
					result = null;
				}
				else if (ptr2->bIsNull == 1)
				{
					status = OracleUdtStatus.Null;
					result = OracleIntervalYM.Null;
				}
				else
				{
					status = OracleUdtStatus.NotNull;
					result = new OracleIntervalYM((OpoITLValCtx*)((void*)ptr2->pDataTmpBuffer));
					ptr2->pDataTmpBuffer = IntPtr.Zero;
				}
			}
			else if (ptr2->bIsNull == 1)
			{
				status = OracleUdtStatus.Null;
				if (ptr3->IsNullable == 1)
				{
					result = null;
				}
				else if (ptr->pOpoDscValCtx->TypeCode == 108)
				{
					result = DBNull.Value;
				}
				else
				{
					result = 0L;
				}
			}
			else
			{
				status = OracleUdtStatus.NotNull;
				result = LongConv.GetLong((OpoITLValCtx*)((void*)ptr2->pUDT), OracleDbType.IntervalYM);
			}
			return result;
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x00016464 File Offset: 0x00015464
		internal unsafe static object GetFltData(OracleConnection con, IntPtr pUdt, int index, out OracleUdtStatus status)
		{
			OpoUdtValCtx* ptr = (OpoUdtValCtx*)((void*)pUdt);
			OpoUdtValCtx* ptr2 = ptr->pOpoUdtValCtx + index;
			AttrMetaVal* ptr3;
			if (ptr->pOpoDscValCtx->TypeCode == 108)
			{
				ptr3 = ptr->pOpoDscValCtx->pAttrMetaVals + index;
			}
			else
			{
				ptr3 = ptr->pOpoDscValCtx->pAttrMetaVals;
			}
			CustomTypeCode customTypeCode = ptr3->CustTypeCode;
			if (ptr->bIsOdtConnection == 1)
			{
				customTypeCode = CustomTypeCode.OracleDecimal;
			}
			CustomTypeCode customTypeCode2 = customTypeCode;
			object result;
			switch (customTypeCode2)
			{
			case CustomTypeCode.Byte:
				if (ptr2->bIsNull != 1)
				{
					status = OracleUdtStatus.NotNull;
					return (byte)ptr2->opoUdtAttrValCtx.m_float;
				}
				status = OracleUdtStatus.Null;
				if (ptr3->IsNullable == 1)
				{
					return null;
				}
				if (ptr->pOpoDscValCtx->TypeCode == 108)
				{
					return DBNull.Value;
				}
				return 0;
			case CustomTypeCode.Bytes:
			case CustomTypeCode.Chars:
			case CustomTypeCode.DateTime:
				break;
			case CustomTypeCode.Decimal:
				if (ptr2->bIsNull != 1)
				{
					status = OracleUdtStatus.NotNull;
					return (decimal)ptr2->opoUdtAttrValCtx.m_float;
				}
				status = OracleUdtStatus.Null;
				if (ptr3->IsNullable == 1)
				{
					return null;
				}
				if (ptr->pOpoDscValCtx->TypeCode == 108)
				{
					return DBNull.Value;
				}
				return 0;
			case CustomTypeCode.Double:
				if (ptr2->bIsNull != 1)
				{
					status = OracleUdtStatus.NotNull;
					return (double)ptr2->opoUdtAttrValCtx.m_float;
				}
				status = OracleUdtStatus.Null;
				if (ptr3->IsNullable == 1)
				{
					return null;
				}
				if (ptr->pOpoDscValCtx->TypeCode == 108)
				{
					return DBNull.Value;
				}
				return 0;
			case CustomTypeCode.Int16:
				if (ptr2->bIsNull != 1)
				{
					status = OracleUdtStatus.NotNull;
					return (short)ptr2->opoUdtAttrValCtx.m_float;
				}
				status = OracleUdtStatus.Null;
				if (ptr3->IsNullable == 1)
				{
					return null;
				}
				if (ptr->pOpoDscValCtx->TypeCode == 108)
				{
					return DBNull.Value;
				}
				return 0;
			case CustomTypeCode.Int32:
				if (ptr2->bIsNull != 1)
				{
					status = OracleUdtStatus.NotNull;
					return (int)ptr2->opoUdtAttrValCtx.m_float;
				}
				status = OracleUdtStatus.Null;
				if (ptr3->IsNullable == 1)
				{
					return null;
				}
				if (ptr->pOpoDscValCtx->TypeCode == 108)
				{
					return DBNull.Value;
				}
				return 0;
			case CustomTypeCode.Int64:
				if (ptr2->bIsNull != 1)
				{
					status = OracleUdtStatus.NotNull;
					return (long)ptr2->opoUdtAttrValCtx.m_float;
				}
				status = OracleUdtStatus.Null;
				if (ptr3->IsNullable == 1)
				{
					return null;
				}
				if (ptr->pOpoDscValCtx->TypeCode == 108)
				{
					return DBNull.Value;
				}
				return 0L;
			case CustomTypeCode.Single:
				if (ptr2->bIsNull != 1)
				{
					status = OracleUdtStatus.NotNull;
					return ptr2->opoUdtAttrValCtx.m_float;
				}
				status = OracleUdtStatus.Null;
				if (ptr3->IsNullable == 1)
				{
					return null;
				}
				if (ptr->pOpoDscValCtx->TypeCode == 108)
				{
					return DBNull.Value;
				}
				return 0;
			default:
				if (customTypeCode2 == CustomTypeCode.OracleDecimal)
				{
					if (ptr2->bIsNull == 1)
					{
						status = OracleUdtStatus.Null;
						return OracleDecimal.Null;
					}
					status = OracleUdtStatus.NotNull;
					result = new OracleDecimal(ptr2->pDataTmpBuffer);
					ptr2->pDataTmpBuffer = IntPtr.Zero;
					return result;
				}
				break;
			}
			status = OracleUdtStatus.Null;
			result = null;
			return result;
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x000167DC File Offset: 0x000157DC
		internal unsafe static object GetDblData(OracleConnection con, IntPtr pUdt, int index, out OracleUdtStatus status)
		{
			OpoUdtValCtx* ptr = (OpoUdtValCtx*)((void*)pUdt);
			OpoUdtValCtx* ptr2 = ptr->pOpoUdtValCtx + index;
			AttrMetaVal* ptr3;
			if (ptr->pOpoDscValCtx->TypeCode == 108)
			{
				ptr3 = ptr->pOpoDscValCtx->pAttrMetaVals + index;
			}
			else
			{
				ptr3 = ptr->pOpoDscValCtx->pAttrMetaVals;
			}
			CustomTypeCode customTypeCode = ptr3->CustTypeCode;
			if (ptr->bIsOdtConnection == 1)
			{
				customTypeCode = CustomTypeCode.OracleDecimal;
			}
			CustomTypeCode customTypeCode2 = customTypeCode;
			object result;
			switch (customTypeCode2)
			{
			case CustomTypeCode.Byte:
				if (ptr2->bIsNull != 1)
				{
					status = OracleUdtStatus.NotNull;
					return (byte)ptr2->opoUdtAttrValCtx.m_double;
				}
				status = OracleUdtStatus.Null;
				if (ptr3->IsNullable == 1)
				{
					return null;
				}
				if (ptr->pOpoDscValCtx->TypeCode == 108)
				{
					return DBNull.Value;
				}
				return 0;
			case CustomTypeCode.Bytes:
			case CustomTypeCode.Chars:
			case CustomTypeCode.DateTime:
				break;
			case CustomTypeCode.Decimal:
				if (ptr2->bIsNull != 1)
				{
					status = OracleUdtStatus.NotNull;
					return (decimal)ptr2->opoUdtAttrValCtx.m_double;
				}
				status = OracleUdtStatus.Null;
				if (ptr3->IsNullable == 1)
				{
					return null;
				}
				if (ptr->pOpoDscValCtx->TypeCode == 108)
				{
					return DBNull.Value;
				}
				return 0;
			case CustomTypeCode.Double:
				if (ptr2->bIsNull != 1)
				{
					status = OracleUdtStatus.NotNull;
					return ptr2->opoUdtAttrValCtx.m_double;
				}
				status = OracleUdtStatus.Null;
				if (ptr3->IsNullable == 1)
				{
					return null;
				}
				if (ptr->pOpoDscValCtx->TypeCode == 108)
				{
					return DBNull.Value;
				}
				return 0;
			case CustomTypeCode.Int16:
				if (ptr2->bIsNull != 1)
				{
					status = OracleUdtStatus.NotNull;
					return (short)ptr2->opoUdtAttrValCtx.m_double;
				}
				status = OracleUdtStatus.Null;
				if (ptr3->IsNullable == 1)
				{
					return null;
				}
				if (ptr->pOpoDscValCtx->TypeCode == 108)
				{
					return DBNull.Value;
				}
				return 0;
			case CustomTypeCode.Int32:
				if (ptr2->bIsNull != 1)
				{
					status = OracleUdtStatus.NotNull;
					return (int)ptr2->opoUdtAttrValCtx.m_double;
				}
				status = OracleUdtStatus.Null;
				if (ptr3->IsNullable == 1)
				{
					return null;
				}
				if (ptr->pOpoDscValCtx->TypeCode == 108)
				{
					return DBNull.Value;
				}
				return 0;
			case CustomTypeCode.Int64:
				if (ptr2->bIsNull != 1)
				{
					status = OracleUdtStatus.NotNull;
					return (long)ptr2->opoUdtAttrValCtx.m_double;
				}
				status = OracleUdtStatus.Null;
				if (ptr3->IsNullable == 1)
				{
					return null;
				}
				if (ptr->pOpoDscValCtx->TypeCode == 108)
				{
					return DBNull.Value;
				}
				return 0L;
			case CustomTypeCode.Single:
				if (ptr2->bIsNull != 1)
				{
					status = OracleUdtStatus.NotNull;
					return (float)ptr2->opoUdtAttrValCtx.m_double;
				}
				status = OracleUdtStatus.Null;
				if (ptr3->IsNullable == 1)
				{
					return null;
				}
				if (ptr->pOpoDscValCtx->TypeCode == 108)
				{
					return DBNull.Value;
				}
				return 0;
			default:
				if (customTypeCode2 == CustomTypeCode.OracleDecimal)
				{
					if (ptr2->bIsNull == 1)
					{
						status = OracleUdtStatus.Null;
						return OracleDecimal.Null;
					}
					status = OracleUdtStatus.NotNull;
					result = new OracleDecimal(ptr2->pDataTmpBuffer);
					ptr2->pDataTmpBuffer = IntPtr.Zero;
					return result;
				}
				break;
			}
			status = OracleUdtStatus.Null;
			result = null;
			return result;
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x00016B54 File Offset: 0x00015B54
		internal unsafe static object GetBFileData(OracleConnection con, IntPtr pUdt, int index, out OracleUdtStatus status)
		{
			int num = 0;
			OpoUdtValCtx* ptr = (OpoUdtValCtx*)((void*)pUdt);
			OpoUdtValCtx* ptr2 = ptr->pOpoUdtValCtx + index;
			AttrMetaVal* ptr3;
			if (ptr->pOpoDscValCtx->TypeCode == 108)
			{
				ptr3 = ptr->pOpoDscValCtx->pAttrMetaVals + index;
			}
			else
			{
				ptr3 = ptr->pOpoDscValCtx->pAttrMetaVals;
			}
			CustomTypeCode customTypeCode = ptr3->CustTypeCode;
			if (ptr->bIsOdtConnection == 1)
			{
				customTypeCode = CustomTypeCode.OracleBFile;
			}
			if (ptr2->bIsNull != 1)
			{
				try
				{
					num = OpsUdt.GetBFile(con.m_opoConCtx.opsConCtx, ptr, index);
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
					if (num != 0)
					{
						OracleException.HandleError(num, con, ptr->pOpsErrCtx, null);
					}
				}
			}
			CustomTypeCode customTypeCode2 = customTypeCode;
			object obj;
			if (customTypeCode2 != CustomTypeCode.Bytes)
			{
				if (customTypeCode2 != CustomTypeCode.OracleBFile)
				{
					status = OracleUdtStatus.Null;
					obj = null;
				}
				else if (ptr2->bIsNull == 1)
				{
					status = OracleUdtStatus.Null;
					obj = OracleBFile.Null;
				}
				else
				{
					status = OracleUdtStatus.NotNull;
					obj = new OracleBFile(con, ptr2->pDataTmpBuffer, 0);
					((OracleBFile)obj).m_allocOciLobLoc = 1;
					ptr2->pDataTmpBuffer = IntPtr.Zero;
				}
			}
			else if (ptr2->bIsNull == 1)
			{
				status = OracleUdtStatus.Null;
				obj = null;
			}
			else
			{
				status = OracleUdtStatus.NotNull;
				OracleBFile oracleBFile = new OracleBFile(con, ptr2->pDataTmpBuffer, 0);
				oracleBFile.m_allocOciLobLoc = 1;
				ptr2->pDataTmpBuffer = IntPtr.Zero;
				obj = oracleBFile.Value;
				oracleBFile.Dispose();
			}
			return obj;
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x00016CD0 File Offset: 0x00015CD0
		internal unsafe static object GetBlobData(OracleConnection con, IntPtr pUdt, int index, out OracleUdtStatus status)
		{
			int num = 0;
			OpoUdtValCtx* ptr = (OpoUdtValCtx*)((void*)pUdt);
			OpoUdtValCtx* ptr2 = ptr->pOpoUdtValCtx + index;
			AttrMetaVal* ptr3;
			if (ptr->pOpoDscValCtx->TypeCode == 108)
			{
				ptr3 = ptr->pOpoDscValCtx->pAttrMetaVals + index;
			}
			else
			{
				ptr3 = ptr->pOpoDscValCtx->pAttrMetaVals;
			}
			CustomTypeCode customTypeCode = ptr3->CustTypeCode;
			if (ptr->bIsOdtConnection == 1)
			{
				customTypeCode = CustomTypeCode.OracleBlob;
			}
			if (ptr2->bIsNull != 1)
			{
				try
				{
					num = OpsUdt.GetLob(con.m_opoConCtx.opsConCtx, ptr, index);
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
					if (num != 0)
					{
						OracleException.HandleError(num, con, ptr->pOpsErrCtx, null);
					}
				}
			}
			CustomTypeCode customTypeCode2 = customTypeCode;
			object obj;
			if (customTypeCode2 != CustomTypeCode.Bytes)
			{
				if (customTypeCode2 != CustomTypeCode.OracleBlob)
				{
					status = OracleUdtStatus.Null;
					obj = null;
				}
				else if (ptr2->bIsNull == 1)
				{
					status = OracleUdtStatus.Null;
					obj = OracleBlob.Null;
				}
				else
				{
					status = OracleUdtStatus.NotNull;
					obj = new OracleBlob(con, ptr2->pDataTmpBuffer, false, false, 0);
					((OracleBlob)obj).m_allocOciLobLoc = 1;
					ptr2->pDataTmpBuffer = IntPtr.Zero;
				}
			}
			else if (ptr2->bIsNull == 1)
			{
				status = OracleUdtStatus.Null;
				obj = null;
			}
			else
			{
				status = OracleUdtStatus.NotNull;
				OracleBlob oracleBlob = new OracleBlob(con, ptr2->pDataTmpBuffer, false, false, 0);
				oracleBlob.m_allocOciLobLoc = 1;
				ptr2->pDataTmpBuffer = IntPtr.Zero;
				obj = oracleBlob.Value;
				oracleBlob.Dispose();
			}
			return obj;
		}

		// Token: 0x060001AA RID: 426 RVA: 0x00016E50 File Offset: 0x00015E50
		internal unsafe static object GetClobData(OracleConnection con, IntPtr pUdt, int index, out OracleUdtStatus status)
		{
			int num = 0;
			OpoUdtValCtx* ptr = (OpoUdtValCtx*)((void*)pUdt);
			OpoUdtValCtx* ptr2 = ptr->pOpoUdtValCtx + index;
			AttrMetaVal* ptr3;
			if (ptr->pOpoDscValCtx->TypeCode == 108)
			{
				ptr3 = ptr->pOpoDscValCtx->pAttrMetaVals + index;
			}
			else
			{
				ptr3 = ptr->pOpoDscValCtx->pAttrMetaVals;
			}
			bool bNClob = false;
			if (ptr3->CharsetForm == 2)
			{
				bNClob = true;
			}
			CustomTypeCode customTypeCode = ptr3->CustTypeCode;
			if (ptr->bIsOdtConnection == 1)
			{
				customTypeCode = CustomTypeCode.OracleClob;
			}
			if (ptr2->bIsNull != 1)
			{
				try
				{
					num = OpsUdt.GetLob(con.m_opoConCtx.opsConCtx, ptr, index);
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
					if (num != 0)
					{
						OracleException.HandleError(num, con, ptr->pOpsErrCtx, null);
					}
				}
			}
			CustomTypeCode customTypeCode2 = customTypeCode;
			object obj;
			if (customTypeCode2 != CustomTypeCode.Chars)
			{
				if (customTypeCode2 != CustomTypeCode.String)
				{
					if (customTypeCode2 != CustomTypeCode.OracleClob)
					{
						status = OracleUdtStatus.Null;
						obj = null;
					}
					else if (ptr2->bIsNull == 1)
					{
						status = OracleUdtStatus.Null;
						obj = OracleClob.Null;
					}
					else
					{
						status = OracleUdtStatus.NotNull;
						obj = new OracleClob(con, ptr2->pDataTmpBuffer, false, bNClob, false, 0);
						((OracleClob)obj).m_allocOciLobLoc = 1;
						ptr2->pDataTmpBuffer = IntPtr.Zero;
					}
				}
				else if (ptr2->bIsNull == 1)
				{
					status = OracleUdtStatus.Null;
					obj = null;
				}
				else
				{
					status = OracleUdtStatus.NotNull;
					OracleClob oracleClob = new OracleClob(con, ptr2->pDataTmpBuffer, false, bNClob, false, 0);
					oracleClob.m_allocOciLobLoc = 1;
					ptr2->pDataTmpBuffer = IntPtr.Zero;
					obj = oracleClob.Value;
					oracleClob.Dispose();
				}
			}
			else if (ptr2->bIsNull == 1)
			{
				status = OracleUdtStatus.Null;
				obj = null;
			}
			else
			{
				status = OracleUdtStatus.NotNull;
				OracleClob oracleClob2 = new OracleClob(con, ptr2->pDataTmpBuffer, false, bNClob, false, 0);
				oracleClob2.m_allocOciLobLoc = 1;
				ptr2->pDataTmpBuffer = IntPtr.Zero;
				obj = oracleClob2.Value.ToCharArray();
				oracleClob2.Dispose();
			}
			return obj;
		}

		// Token: 0x060001AB RID: 427 RVA: 0x00017048 File Offset: 0x00016048
		internal unsafe static object GetObjData(OracleConnection con, IntPtr pUdt, int attrIndex, out OracleUdtStatus status)
		{
			int num = 0;
			OpoUdtValCtx* ptr = (OpoUdtValCtx*)((void*)pUdt);
			OpoUdtValCtx* ptr2 = ptr->pOpoUdtValCtx + attrIndex;
			AttrMetaVal* ptr3;
			if (ptr->pOpoDscValCtx->TypeCode == 108)
			{
				ptr3 = ptr->pOpoDscValCtx->pAttrMetaVals + attrIndex;
			}
			else
			{
				ptr3 = ptr->pOpoDscValCtx->pAttrMetaVals;
			}
			if (ptr3->pOpoDscValCtx->bIsFinalType != 1 && ptr2->bIsNull != 1)
			{
				try
				{
					num = OpsUdt.GetUdt(con.m_opoConCtx.opsConCtx, ptr, attrIndex);
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
					if (num != 0)
					{
						OracleException.HandleError(num, con, ptr->pOpsErrCtx, null);
					}
				}
			}
			OracleUdtDescriptor oracleUdtDescriptor2;
			if (ptr3->pOpoDscValCtx->bIsFinalType == 1 || ptr2->bIsNull == 1)
			{
				bool flag;
				OracleUdtDescriptor oracleUdtDescriptor = OracleUdtDescriptor.GetOracleUdtDescriptor(con, (IntPtr)(*(IntPtr*)((void*)ptr->pTDO)), false, out flag);
				if (ptr->pOpoDscValCtx->TypeCode == 108)
				{
					oracleUdtDescriptor2 = oracleUdtDescriptor.GetObjAttrUdtDescriptor(attrIndex);
				}
				else
				{
					oracleUdtDescriptor2 = oracleUdtDescriptor.GetArrElemUdtDescriptor();
				}
			}
			else
			{
				bool flag2;
				oracleUdtDescriptor2 = OracleUdtDescriptor.GetOracleUdtDescriptor(con, ptr2->pTDO, false, out flag2);
				if (flag2)
				{
					try
					{
						OpsDsc.UnpinTDO(con.m_opoConCtx.opsConCtx, ptr2->pTDO);
					}
					catch (Exception ex2)
					{
						if (OraTrace.m_TraceLevel != 0U)
						{
							OraTrace.TraceExceptionInfo(ex2);
						}
					}
				}
			}
			if (oracleUdtDescriptor2.m_customTypeFactory == null)
			{
				object factory = OracleUdt.GetFactory(oracleUdtDescriptor2);
				if (factory != null)
				{
					oracleUdtDescriptor2.DescribeCustomType(factory);
				}
			}
			IOracleCustomTypeFactory oracleCustomTypeFactory = (IOracleCustomTypeFactory)oracleUdtDescriptor2.m_customTypeFactory;
			IOracleCustomType oracleCustomType = oracleCustomTypeFactory.CreateObject();
			if (ptr2->bIsNull != 1)
			{
				ptr2->pOpoDscValCtx = oracleUdtDescriptor2.m_pOpoDscValCtx;
				ptr2->pOpsErrCtx = ptr->pOpsErrCtx;
				ptr2->pTDO = oracleUdtDescriptor2.m_opsDscCtx;
				ptr2->ppRefTDO = ptr->ppRefTDO;
				try
				{
					num = OpsUdt.GetObj(con.m_opoConCtx.opsConCtx, ptr2);
				}
				catch (Exception ex3)
				{
					if (OraTrace.m_TraceLevel != 0U)
					{
						OraTrace.TraceExceptionInfo(ex3);
					}
					throw;
				}
				finally
				{
					if (num != 0)
					{
						OracleException.HandleError(num, con, ptr2->pOpsErrCtx, null);
					}
				}
				ptr2->bIgnoreElemStatus = 0;
				oracleCustomType.ToCustomObject(con, (IntPtr)((void*)ptr2));
				object result = oracleCustomType;
				status = OracleUdtStatus.NotNull;
				GC.KeepAlive(oracleUdtDescriptor2);
				return result;
			}
			status = OracleUdtStatus.Null;
			Type type = oracleCustomType.GetType();
			PropertyInfo property = type.GetProperty("Null");
			if (property != null)
			{
				return property.GetValue(null, null);
			}
			throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(-2902, new string[]
			{
				"'" + type.FullName + "'",
				"'Null'"
			}));
		}

		// Token: 0x060001AC RID: 428 RVA: 0x0001730C File Offset: 0x0001630C
		internal unsafe static object GetNumArrData(OracleConnection con, IntPtr pUdt, out OracleUdtStatus status, out object statusArray)
		{
			status = OracleUdtStatus.NotNull;
			OpoUdtValCtx* ptr = (OpoUdtValCtx*)((void*)pUdt);
			AttrMetaVal* ptr2 = ptr->pOpoDscValCtx->pAttrMetaVals;
			OracleUdtStatus[] array;
			if (ptr->bIgnoreElemStatus == 0)
			{
				array = new OracleUdtStatus[ptr->NumOfArrElems];
			}
			else
			{
				array = null;
			}
			CustomTypeCode customTypeCode;
			if (ptr->bIsOdtConnection == 1)
			{
				customTypeCode = CustomTypeCode.OracleDecimal;
			}
			else
			{
				customTypeCode = ptr2->CustTypeCode;
			}
			object result;
			switch (customTypeCode)
			{
			case CustomTypeCode.Double:
			{
				if (ptr2->IsNullable == 1)
				{
					double?[] array2 = new double?[ptr->NumOfArrElems];
					for (int i = 0; i < ptr->NumOfArrElems; i++)
					{
						if (*(int*)((byte*)((void*)ptr->pStatusMarshalBuffer) + (IntPtr)i * 4) == 0)
						{
							if (ptr->bIgnoreElemStatus == 0)
							{
								array[i] = OracleUdtStatus.NotNull;
							}
							array2[i] = new double?(*(double*)((byte*)((void*)ptr->pDataMarshalBuffer) + (IntPtr)i * 8));
						}
						else
						{
							array2[i] = null;
						}
					}
					result = array2;
					goto IL_26C;
				}
				double[] array3 = new double[ptr->NumOfArrElems];
				if (ptr->bIgnoreElemStatus == 0)
				{
					for (int j = 0; j < ptr->NumOfArrElems; j++)
					{
						if (*(int*)((byte*)((void*)ptr->pStatusMarshalBuffer) + (IntPtr)j * 4) == 0)
						{
							array[j] = OracleUdtStatus.NotNull;
							array3[j] = *(double*)((byte*)((void*)ptr->pDataMarshalBuffer) + (IntPtr)j * 8);
						}
					}
				}
				else
				{
					Marshal.Copy(ptr->pDataMarshalBuffer, array3, 0, ptr->NumOfArrElems);
				}
				result = array3;
				goto IL_26C;
			}
			case CustomTypeCode.Int32:
			{
				if (ptr2->IsNullable == 1)
				{
					int?[] array4 = new int?[ptr->NumOfArrElems];
					for (int k = 0; k < ptr->NumOfArrElems; k++)
					{
						if (*(int*)((byte*)((void*)ptr->pStatusMarshalBuffer) + (IntPtr)k * 4) == 0)
						{
							if (ptr->bIgnoreElemStatus == 0)
							{
								array[k] = OracleUdtStatus.NotNull;
							}
							array4[k] = new int?(*(int*)((byte*)((void*)ptr->pDataMarshalBuffer) + (IntPtr)k * 4));
						}
						else
						{
							array4[k] = null;
						}
					}
					result = array4;
					goto IL_26C;
				}
				int[] array5 = new int[ptr->NumOfArrElems];
				if (ptr->bIgnoreElemStatus == 0)
				{
					for (int l = 0; l < ptr->NumOfArrElems; l++)
					{
						if (*(int*)((byte*)((void*)ptr->pStatusMarshalBuffer) + (IntPtr)l * 4) == 0)
						{
							array[l] = OracleUdtStatus.NotNull;
							array5[l] = *(int*)((byte*)((void*)ptr->pDataMarshalBuffer) + (IntPtr)l * 4);
						}
					}
				}
				else
				{
					Marshal.Copy(ptr->pDataMarshalBuffer, array5, 0, ptr->NumOfArrElems);
				}
				result = array5;
				goto IL_26C;
			}
			}
			result = null;
			IL_26C:
			statusArray = array;
			return result;
		}

		// Token: 0x060001AD RID: 429 RVA: 0x0001758C File Offset: 0x0001658C
		[ConfigurationPermission(SecurityAction.Assert, Unrestricted = true)]
		internal static void SetCustomTypeMappings()
		{
			if (OracleUdt.s_mapUdtNameToMappingObj == null)
			{
				lock (OracleUdt.s_lockObj)
				{
					if (OracleUdt.s_mapUdtNameToMappingObj == null)
					{
						Hashtable hashtable = new Hashtable();
						if (RegAndConfigRdr.m_configSection != null)
						{
							foreach (object obj2 in RegAndConfigRdr.m_configSection)
							{
								string text = RegAndConfigRdr.m_configSection[(string)obj2].Trim();
								XmlDataDocument xmlDataDocument = new XmlDataDocument();
								int i;
								for (i = text.LastIndexOf("udtMapping", StringComparison.InvariantCultureIgnoreCase); i > -1; i = text.LastIndexOf("udtMapping", i, StringComparison.InvariantCultureIgnoreCase))
								{
									string text2 = null;
									if (i > 0 && text[i - 1] == ',')
									{
										text2 = text.Substring(i);
									}
									else if (i == 0)
									{
										text2 = text;
									}
									if (text2 != null)
									{
										try
										{
											xmlDataDocument.LoadXml("<" + text2 + "/>");
											break;
										}
										catch
										{
										}
									}
								}
								if (i == -1)
								{
									if (OraTrace.m_TraceLevel != 0U)
									{
										IEnumerator enumerator;
										OraTrace.Trace(1U, new string[]
										{
											" (UDT)   Skipped Config Entry: [{%s} => {%s}]\n",
											enumerator.Current.ToString(),
											text
										});
									}
								}
								else
								{
									IEnumerator enumerator;
									if (xmlDataDocument.ChildNodes.Count != 1 || xmlDataDocument.FirstChild.HasChildNodes)
									{
										if (OraTrace.m_TraceLevel != 0U)
										{
											OraTrace.Trace(1U, new string[]
											{
												" (UDT)   Processing Config Entry: [{%s} => {%s}]\n",
												enumerator.Current.ToString(),
												text
											});
										}
										throw new ConfigurationErrorsException(OpoErrResManager.GetErrorMesg(-2901, new string[]
										{
											"'" + enumerator.Current + "'"
										}), xmlDataDocument);
									}
									NameValueCollection nameValueCollection = new NameValueCollection(StringComparer.CurrentCulture);
									string text3 = string.Empty;
									string text4 = null;
									string text5 = null;
									string text6 = null;
									foreach (object obj3 in xmlDataDocument.FirstChild.Attributes)
									{
										XmlAttribute xmlAttribute = (XmlAttribute)obj3;
										nameValueCollection.Add(xmlAttribute.Name, xmlAttribute.Value);
										if (string.Compare(xmlAttribute.Name, "dataSource", true) == 0)
										{
											text3 = xmlAttribute.Value;
										}
										else if (string.Compare(xmlAttribute.Name, "schemaName", true) == 0)
										{
											text4 = xmlAttribute.Value;
										}
										else if (string.Compare(xmlAttribute.Name, "typeName", true) == 0)
										{
											text5 = xmlAttribute.Value;
										}
										else
										{
											if (string.Compare(xmlAttribute.Name, "factoryName", true) != 0)
											{
												if (OraTrace.m_TraceLevel != 0U)
												{
													OraTrace.Trace(1U, new string[]
													{
														" (UDT)   Processing Config Entry: [{%s} => {%s}]\n",
														enumerator.Current.ToString(),
														text
													});
												}
												throw new ConfigurationErrorsException(OpoErrResManager.GetErrorMesg(ErrRes.ODP_NOT_SUPPORTED, new string[]
												{
													"'udtMapping'",
													"'" + xmlAttribute.Name + "'"
												}), xmlDataDocument.FirstChild);
											}
											text6 = xmlAttribute.Value;
										}
									}
									if (text5 == null || text5 == "")
									{
										if (OraTrace.m_TraceLevel != 0U)
										{
											OraTrace.Trace(1U, new string[]
											{
												" (UDT)   Processing Config Entry: [{%s} => {%s}]\n",
												enumerator.Current.ToString(),
												text
											});
										}
										throw new ConfigurationErrorsException(OpoErrResManager.GetErrorMesg(ErrRes.CON_STR_INVALID_VALUE, new string[]
										{
											"typeName",
											text5
										}), xmlDataDocument.FirstChild);
									}
									if (text6 == null || text6 == "")
									{
										if (OraTrace.m_TraceLevel != 0U)
										{
											OraTrace.Trace(1U, new string[]
											{
												" (UDT)   Processing Config Entry: [{%s} => {%s}]\n",
												enumerator.Current.ToString(),
												text
											});
										}
										throw new ConfigurationErrorsException(OpoErrResManager.GetErrorMesg(ErrRes.CON_STR_INVALID_VALUE, new string[]
										{
											"factoryName",
											text6
										}), xmlDataDocument.FirstChild);
									}
									string text7 = "typeName='" + text5 + "'";
									if (text4 != null && text4.Length > 0)
									{
										text7 = "schemaName='" + text4 + "' " + text7;
									}
									if (text3 != null && text3.Length > 0)
									{
										text7 = "dataSource='" + text3.ToUpper() + "' " + text7;
									}
									hashtable[text7] = nameValueCollection;
									if (OraTrace.m_TraceLevel != 0U)
									{
										OraTrace.Trace(1U, new string[]
										{
											" (UDT)   UdtToFactoryMapping(0): [{%s} => {%s}]\n",
											text7,
											text6
										});
									}
								}
							}
							if (hashtable.Count > 0)
							{
								OracleUdt.s_mapUdtNameToMappingObj = hashtable;
								return;
							}
						}
						ArrayList allReferencedAssemblies = OracleUdt.GetAllReferencedAssemblies();
						foreach (object obj4 in allReferencedAssemblies)
						{
							Assembly assembly = (Assembly)obj4;
							Type[] array = null;
							try
							{
								array = assembly.GetTypes();
							}
							catch (ReflectionTypeLoadException ex)
							{
								if (OraTrace.m_TraceLevel != 0U)
								{
									OraTrace.TraceExceptionInfo(ex, false);
								}
								Exception[] loaderExceptions = ex.LoaderExceptions;
								foreach (Exception ex2 in loaderExceptions)
								{
									if (OraTrace.m_TraceLevel != 0U)
									{
										OraTrace.TraceExceptionInfo(ex2, false);
									}
								}
								array = ex.Types;
							}
							catch (Exception ex3)
							{
								if (OraTrace.m_TraceLevel != 0U)
								{
									OraTrace.TraceExceptionInfo(ex3, false);
								}
							}
							if (array != null)
							{
								foreach (Type type in array)
								{
									if (type != null && (type.GetInterface("IOracleCustomTypeFactory") != null || type.GetInterface("IOracleArrayTypeFactory") != null))
									{
										object[] customAttributes = type.GetCustomAttributes(typeof(OracleCustomTypeMappingAttribute), false);
										if (customAttributes.Length > 0)
										{
											OracleCustomTypeMappingAttribute oracleCustomTypeMappingAttribute = (OracleCustomTypeMappingAttribute)customAttributes[0];
											NameValueCollection nameValueCollection2 = new NameValueCollection();
											nameValueCollection2["factoryName"] = type.AssemblyQualifiedName;
											object value = nameValueCollection2;
											string text8 = oracleCustomTypeMappingAttribute.UdtTypeName;
											int num = text8.LastIndexOf('.');
											if (num != -1)
											{
												string text9 = oracleCustomTypeMappingAttribute.UdtTypeName.Substring(0, num);
												string text10 = oracleCustomTypeMappingAttribute.UdtTypeName.Substring(num + 1);
												nameValueCollection2["schemaName"] = text9;
												nameValueCollection2["typeName"] = text10;
												text8 = string.Concat(new string[]
												{
													"schemaName='",
													text9,
													"' typeName='",
													text10,
													"'"
												});
											}
											else
											{
												string udtTypeName = oracleCustomTypeMappingAttribute.UdtTypeName;
												nameValueCollection2["typeName"] = udtTypeName;
												text8 = "typeName='" + udtTypeName + "'";
											}
											hashtable[text8] = value;
											if (OraTrace.m_TraceLevel != 0U)
											{
												OraTrace.Trace(1U, new string[]
												{
													" (UDT)   UdtToFactoryMapping(1): [{%s} => {%s}]\n",
													text8,
													type.AssemblyQualifiedName
												});
											}
										}
									}
								}
							}
						}
						OracleUdt.s_mapUdtNameToMappingObj = hashtable;
					}
				}
			}
		}

		// Token: 0x060001AE RID: 430 RVA: 0x00017D48 File Offset: 0x00016D48
		private static ArrayList GetAllReferencedAssemblies()
		{
			ArrayList arrayList = new ArrayList();
			ArrayList arrayList2 = new ArrayList();
			Stack stack = new Stack();
			Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
			foreach (Assembly assembly in assemblies)
			{
				if (!assembly.FullName.StartsWith("System") && !assembly.FullName.StartsWith("Microsoft") && !assembly.FullName.StartsWith("mscorlib"))
				{
					arrayList2.Add(assembly.FullName);
					stack.Push(assembly.FullName);
				}
			}
			while (stack.Count > 0)
			{
				try
				{
					string assemblyString = (string)stack.Pop();
					Assembly assembly2 = Assembly.Load(assemblyString);
					if (OraTrace.m_TraceLevel != 0U)
					{
						OraTrace.Trace(1U, new string[]
						{
							" (UDT)   OracleUdt::GetAllReferencedAssemblies(): {%s}\n",
							assembly2.FullName
						});
					}
					arrayList.Add(assembly2);
					AssemblyName[] referencedAssemblies = assembly2.GetReferencedAssemblies();
					foreach (AssemblyName assemblyName in referencedAssemblies)
					{
						if (!arrayList2.Contains(assemblyName.ToString()) && !assemblyName.ToString().StartsWith("System") && !assemblyName.ToString().StartsWith("Microsoft") && !assemblyName.ToString().StartsWith("mscorlib"))
						{
							arrayList2.Add(assemblyName.ToString());
							stack.Push(assemblyName.ToString());
						}
					}
				}
				catch (Exception ex)
				{
					if (OraTrace.m_TraceLevel != 0U)
					{
						OraTrace.TraceExceptionInfo(ex, false);
					}
				}
			}
			return arrayList;
		}

		// Token: 0x060001AF RID: 431 RVA: 0x00017EF0 File Offset: 0x00016EF0
		internal unsafe static object GetFactory(OracleUdtDescriptor udtDesc)
		{
			if (udtDesc.m_udtTypeNameKey == null)
			{
				string udtTypeName = udtDesc.UdtTypeName;
			}
			string text = "typeName='" + udtDesc.m_opoDscRefCtx.TypeName + "'";
			string udtTypeNameKey = udtDesc.m_udtTypeNameKey;
			string str = udtDesc.m_connection.DataSource.ToUpper();
			string text2 = "dataSource='" + str + "' " + udtTypeNameKey;
			string key = "dataSource='" + str + "' " + text;
			object obj = OracleUdt.s_mapUdtNameToFactory[text2];
			if (obj == null)
			{
				lock (OracleUdt.s_lockObj)
				{
					obj = OracleUdt.s_mapUdtNameToFactory[text2];
					if (obj == null)
					{
						if (OracleUdt.s_mapUdtNameToMappingObj == null)
						{
							OracleUdt.SetCustomTypeMappings();
						}
						object obj3 = OracleUdt.s_mapUdtNameToMappingObj[text2];
						if (obj3 == null)
						{
							obj3 = OracleUdt.s_mapUdtNameToMappingObj[udtTypeNameKey];
						}
						if (obj3 == null)
						{
							obj3 = OracleUdt.s_mapUdtNameToMappingObj[key];
						}
						if (obj3 == null)
						{
							obj3 = OracleUdt.s_mapUdtNameToMappingObj[text];
						}
						if (obj3 != null)
						{
							string text3 = ((NameValueCollection)obj3)["factoryName"];
							obj = Activator.CreateInstance(Type.GetType(text3, true));
							OracleUdt.s_mapUdtNameToFactory[text2] = obj;
							if (OraTrace.m_TraceLevel != 0U)
							{
								OraTrace.Trace(1U, new string[]
								{
									" (UDT)   UdtToFactoryMapping(2): [{%s} => {%s}]\n",
									text2,
									text3
								});
							}
						}
						else
						{
							if (udtDesc.m_pOpoDscValCtx->bIsInstantiableType == 0)
							{
								return new object();
							}
							throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(-2901, new string[]
							{
								"'" + text2 + "'"
							}));
						}
					}
				}
				return obj;
			}
			return obj;
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x000180C0 File Offset: 0x000170C0
		internal static object GetUdtName(string customTypeName, string dataSource)
		{
			dataSource = dataSource.ToUpper();
			string text = string.Concat(new string[]
			{
				"dataSource='",
				dataSource,
				"' customTypeName='",
				customTypeName,
				"'"
			});
			object obj = OracleUdt.s_mapCustomTypeNameToUdtName[text];
			if (obj == null)
			{
				lock (OracleUdt.s_lockObj)
				{
					obj = OracleUdt.s_mapCustomTypeNameToUdtName[text];
					if (obj == null)
					{
						if (OracleUdt.s_mapUdtNameToMappingObj == null)
						{
							OracleUdt.SetCustomTypeMappings();
						}
						IDictionaryEnumerator enumerator = OracleUdt.s_mapUdtNameToMappingObj.GetEnumerator();
						bool flag2 = true;
						bool flag3 = false;
						bool flag4 = enumerator.MoveNext();
						while (flag4)
						{
							DictionaryEntry dictionaryEntry = (DictionaryEntry)enumerator.Current;
							object key = dictionaryEntry.Key;
							DictionaryEntry dictionaryEntry2 = (DictionaryEntry)enumerator.Current;
							object value = dictionaryEntry2.Value;
							NameValueCollection nameValueCollection = (NameValueCollection)value;
							string text2 = nameValueCollection["factoryName"];
							string text3 = nameValueCollection["dataSource"];
							if (text3 != null)
							{
								text3 = text3.ToUpper();
							}
							if ((flag2 && text3 == dataSource) || (!flag2 && (text3 == null || text3 == "")))
							{
								Type type = null;
								try
								{
									type = Type.GetType(text2);
								}
								catch (Exception ex)
								{
									if (OraTrace.m_TraceLevel != 0U)
									{
										OraTrace.Trace(1U, new string[]
										{
											" (UDT)   Exception loading Factory '%s': %s\n",
											text2,
											ex.Message
										});
									}
								}
								if (type != null)
								{
									object obj3 = Activator.CreateInstance(type);
									if (obj3 is IOracleCustomTypeFactory && ((IOracleCustomTypeFactory)obj3).CreateObject().GetType().FullName == customTypeName)
									{
										flag3 = true;
										OpoDscRefCtx opoDscRefCtx = new OpoDscRefCtx();
										opoDscRefCtx.SchemaName = nameValueCollection["schemaName"];
										opoDscRefCtx.TypeName = nameValueCollection["typeName"];
										obj = opoDscRefCtx;
										if (OraTrace.m_TraceLevel != 0U)
										{
											OraTrace.Trace(1U, new string[]
											{
												" (UDT)   CustomTypeToUdtMapping: [{%s} => {%s}]\n",
												text,
												string.Concat(new string[]
												{
													"schemaName='",
													opoDscRefCtx.SchemaName,
													"' typeName='",
													opoDscRefCtx.TypeName,
													"'"
												})
											});
										}
										try
										{
											OracleUdt.s_mapCustomTypeNameToUdtName.Add(text, obj);
										}
										catch
										{
											if (OracleUdt.s_mapCustomTypeNameToUdtName[text] != obj)
											{
												OracleUdt.s_mapCustomTypeNameToUdtName.Remove(text);
												throw;
											}
										}
									}
								}
							}
							if (!(flag4 = enumerator.MoveNext()) && flag2)
							{
								if (flag3)
								{
									return obj;
								}
								enumerator.Reset();
								flag2 = false;
								flag4 = enumerator.MoveNext();
							}
						}
						if (!flag3)
						{
							throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(-2901, new string[]
							{
								"'" + customTypeName + "'"
							}));
						}
					}
				}
				return obj;
			}
			return obj;
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x000183FC File Offset: 0x000173FC
		internal unsafe static void SetArrayValue(OracleConnection con, IntPtr pUdt, int attrIndex, object value, object statusArray)
		{
			OpoUdtValCtx* ptr = (OpoUdtValCtx*)((void*)pUdt);
			AttrMetaVal* ptr2 = ptr->pOpoDscValCtx->pAttrMetaVals + attrIndex;
			CustomTypeCode custTypeCode = ptr2->CustTypeCode;
			SQLT oraType = (SQLT)ptr2->OraType;
			if (oraType <= SQLT.IBDL)
			{
				if (oraType <= SQLT.BDBL)
				{
					switch (oraType)
					{
					case SQLT.CHR:
					case SQLT.STR:
					case SQLT.LNG:
					case SQLT.VCS:
						goto IL_F3;
					case SQLT.NUM:
					case SQLT.INT:
					case SQLT.FLT:
					case SQLT.VNU:
						break;
					case SQLT.PDN:
						goto IL_3A7;
					default:
						switch (oraType)
						{
						case SQLT.BFLT:
						case SQLT.BDBL:
							break;
						default:
							goto IL_3A7;
						}
						break;
					}
				}
				else if (oraType != SQLT.UIN)
				{
					switch (oraType)
					{
					case SQLT.LVC:
					case SQLT.AFC:
					case SQLT.AVC:
						goto IL_F3;
					case SQLT.LVB:
					case (SQLT)98:
					case (SQLT)99:
						goto IL_3A7;
					case SQLT.IBFL:
					case SQLT.IBDL:
						break;
					default:
						goto IL_3A7;
					}
				}
				OracleUdt.SetNumArrData(pUdt, attrIndex, value, (IntPtr)((void*)ptr2), statusArray);
				return;
			}
			if (oraType <= SQLT.NCO)
			{
				if (oraType != SQLT.NTY && oraType != SQLT.NCO)
				{
					goto IL_3A7;
				}
			}
			else
			{
				if (oraType == SQLT.VST)
				{
					goto IL_F3;
				}
				if (oraType != SQLT.PNTY)
				{
					goto IL_3A7;
				}
			}
			Array array = (Array)value;
			int num = 0;
			ptr->NumOfArrElems = array.Length;
			if ((IntPtr)((void*)ptr->pOpoUdtValCtx) == IntPtr.Zero)
			{
				try
				{
					try
					{
						num = OpsUdt.AllocValCtx(out ptr->pOpoUdtValCtx, ptr->NumOfArrElems);
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
					goto IL_206;
				}
				finally
				{
					if (num == 0)
					{
						ptr->NumOpoUdtValCtx = ptr->NumOfArrElems;
					}
					else if (num != 0 && num != ErrRes.INT_ERR)
					{
						OracleException.HandleError(num, con, ptr->pOpsErrCtx, null);
					}
				}
			}
			if (ptr->NumOpoUdtValCtx < ptr->NumOfArrElems)
			{
				try
				{
					num = OpsUdt.ReAllocValCtx(ref ptr->pOpoUdtValCtx, ptr->NumOpoUdtValCtx, ptr->NumOfArrElems);
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
					if (num == 0)
					{
						ptr->NumOpoUdtValCtx = ptr->NumOfArrElems;
					}
					else if (num != 0 && num != ErrRes.INT_ERR)
					{
						OracleException.HandleError(num, con, ptr->pOpsErrCtx, null);
					}
				}
			}
			IL_206:
			if (ptr2->TypeCode == 108)
			{
				for (int i = 0; i < ptr->NumOfArrElems; i++)
				{
					object value2 = array.GetValue(i);
					if (value2 == null || value2 == DBNull.Value || (value2 is INullable && ((INullable)value2).IsNull))
					{
						ptr->pOpoUdtValCtx[i].bIsNull = 1;
					}
					else
					{
						OracleUdt.SetObjData(con, (IntPtr)((void*)(ptr->pOpoUdtValCtx + i)), attrIndex, value2, statusArray);
					}
				}
				return;
			}
			if (ptr2->TypeCode == 58)
			{
				for (int j = 0; j < ptr->NumOfArrElems; j++)
				{
					object value2 = array.GetValue(j);
					if (value2 == null || value2 == DBNull.Value)
					{
						ptr->pOpoUdtValCtx[j].bIsNull = 1;
					}
					else
					{
						OracleUdt.SetXml(con, (IntPtr)((void*)(ptr->pOpoUdtValCtx + j)), attrIndex, value2, (IntPtr)((void*)ptr2), statusArray);
					}
				}
				return;
			}
			bool flag;
			OracleUdtDescriptor oracleUdtDescriptor = OracleUdtDescriptor.GetOracleUdtDescriptor(con, (IntPtr)(*(IntPtr*)((void*)ptr->pTDO)), false, out flag);
			oracleUdtDescriptor.GetMetaDataTable();
			OracleUdtDescriptor arrElemUdtDescriptor = oracleUdtDescriptor.GetArrElemUdtDescriptor();
			for (int k = 0; k < ptr->NumOfArrElems; k++)
			{
				object value2 = array.GetValue(k);
				if (value2 == null || value2 == DBNull.Value)
				{
					ptr->pOpoUdtValCtx[k].bIsNull = 1;
				}
				else
				{
					OpoUdtValCtx* ptr3 = ptr->pOpoUdtValCtx + k;
					ptr3->pTDO = arrElemUdtDescriptor.m_opsDscCtx;
					OracleUdt.SetArrayData(con, (IntPtr)((void*)ptr3), attrIndex, value2, statusArray);
				}
			}
			GC.KeepAlive(oracleUdtDescriptor);
			return;
			IL_F3:
			OracleUdt.SetStringArrData(pUdt, attrIndex, value, (IntPtr)((void*)ptr2), statusArray);
			return;
			IL_3A7:
			Array array2 = (Array)value;
			int num2 = 0;
			ptr->NumOfArrElems = array2.Length;
			if ((IntPtr)((void*)ptr->pOpoUdtValCtx) == IntPtr.Zero)
			{
				try
				{
					try
					{
						num2 = OpsUdt.AllocValCtx(out ptr->pOpoUdtValCtx, ptr->NumOfArrElems);
					}
					catch (Exception ex3)
					{
						if (OraTrace.m_TraceLevel != 0U)
						{
							OraTrace.TraceExceptionInfo(ex3);
						}
						num2 = ErrRes.INT_ERR;
						throw;
					}
					goto IL_4AB;
				}
				finally
				{
					if (num2 == 0)
					{
						ptr->NumOpoUdtValCtx = ptr->NumOfArrElems;
					}
					else if (num2 != 0 && num2 != ErrRes.INT_ERR)
					{
						OracleException.HandleError(num2, con, ptr->pOpsErrCtx, null);
					}
				}
			}
			if (ptr->NumOpoUdtValCtx < ptr->NumOfArrElems)
			{
				try
				{
					num2 = OpsUdt.ReAllocValCtx(ref ptr->pOpoUdtValCtx, ptr->NumOpoUdtValCtx, ptr->NumOfArrElems);
				}
				catch (Exception ex4)
				{
					if (OraTrace.m_TraceLevel != 0U)
					{
						OraTrace.TraceExceptionInfo(ex4);
					}
					num2 = ErrRes.INT_ERR;
					throw;
				}
				finally
				{
					if (num2 == 0)
					{
						ptr->NumOpoUdtValCtx = ptr->NumOfArrElems;
					}
					else if (num2 != 0 && num2 != ErrRes.INT_ERR)
					{
						OracleException.HandleError(num2, con, ptr->pOpsErrCtx, null);
					}
				}
			}
			IL_4AB:
			for (int l = 0; l < ptr->NumOfArrElems; l++)
			{
				object value2 = array2.GetValue(l);
				if (value2 == null || value2 == DBNull.Value)
				{
					ptr->pOpoUdtValCtx[l].bIsNull = 1;
				}
				else
				{
					OracleUdt.SetData(con, (IntPtr)((void*)(ptr->pOpoUdtValCtx + l)), attrIndex, (OraType)ptr2->OraType, (IntPtr)((void*)ptr2), value2, statusArray);
				}
			}
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x00018988 File Offset: 0x00017988
		internal unsafe static void SetNumArrData(IntPtr pUdt, int attrIndex, object value, IntPtr pAttrMeta, object statusArray)
		{
			OpoUdtValCtx* ptr = (OpoUdtValCtx*)((void*)pUdt);
			AttrMetaVal* ptr2 = (AttrMetaVal*)((void*)pAttrMeta);
			CustomTypeCode custTypeCode = ptr2->CustTypeCode;
			CustomTypeCode customTypeCode = custTypeCode;
			switch (customTypeCode)
			{
			case CustomTypeCode.Byte:
			{
				int num;
				if (ptr2->IsNullable == 1)
				{
					num = ((byte?[])value).Length;
				}
				else
				{
					num = ((byte[])value).Length;
				}
				ptr->pStatusMarshalBuffer = Marshal.AllocCoTaskMem(num * 4);
				if (ptr->pDataMarshalBuffer == IntPtr.Zero || ptr->DataBufferSize < (long)num)
				{
					ptr->pDataMarshalBuffer = Marshal.ReAllocCoTaskMem(ptr->pDataMarshalBuffer, num);
					ptr->DataBufferSize = (long)num;
				}
				if (statusArray != null)
				{
					for (int i = 0; i < num; i++)
					{
						if (((OracleUdtStatus[])statusArray)[i] == OracleUdtStatus.Null)
						{
							*(int*)((byte*)((void*)ptr->pStatusMarshalBuffer) + (IntPtr)i * 4) = 1;
						}
						else
						{
							*(int*)((byte*)((void*)ptr->pStatusMarshalBuffer) + (IntPtr)i * 4) = 0;
						}
					}
				}
				else
				{
					for (int j = 0; j < num; j++)
					{
						*(int*)((byte*)((void*)ptr->pStatusMarshalBuffer) + (IntPtr)j * 4) = 0;
					}
				}
				if (ptr2->IsNullable == 1)
				{
					for (int k = 0; k < num; k++)
					{
						byte? b = ((byte?[])value)[k];
						int? num2 = (b != null) ? new int?((int)b.GetValueOrDefault()) : null;
						if (num2 != null)
						{
							if (*(int*)((byte*)((void*)ptr->pStatusMarshalBuffer) + (IntPtr)k * 4) == 0)
							{
								((byte*)((void*)ptr->pDataMarshalBuffer))[k] = ((byte?[])value)[k].Value;
							}
						}
						else
						{
							*(int*)((byte*)((void*)ptr->pStatusMarshalBuffer) + (IntPtr)k * 4) = 1;
						}
					}
				}
				else
				{
					Marshal.Copy((byte[])value, 0, ptr->pDataMarshalBuffer, num);
				}
				ptr->NumOfArrElems = num;
				return;
			}
			case CustomTypeCode.Bytes:
			case CustomTypeCode.Chars:
			case CustomTypeCode.DateTime:
				break;
			case CustomTypeCode.Decimal:
			{
				int num;
				if (ptr2->IsNullable == 1)
				{
					num = ((decimal?[])value).Length;
				}
				else
				{
					num = ((decimal[])value).Length;
				}
				ptr->pStatusMarshalBuffer = Marshal.AllocCoTaskMem(num * 4);
				if (ptr->pDataMarshalBuffer == IntPtr.Zero || ptr->DataBufferSize < (long)(num * 22))
				{
					ptr->pDataMarshalBuffer = Marshal.ReAllocCoTaskMem(ptr->pDataMarshalBuffer, num * 22);
					ptr->DataBufferSize = (long)(num * 22);
				}
				if (statusArray != null)
				{
					for (int l = 0; l < num; l++)
					{
						if (((OracleUdtStatus[])statusArray)[l] == OracleUdtStatus.Null)
						{
							*(int*)((byte*)((void*)ptr->pStatusMarshalBuffer) + (IntPtr)l * 4) = 1;
						}
						else
						{
							*(int*)((byte*)((void*)ptr->pStatusMarshalBuffer) + (IntPtr)l * 4) = 0;
						}
					}
				}
				else
				{
					for (int m = 0; m < num; m++)
					{
						*(int*)((byte*)((void*)ptr->pStatusMarshalBuffer) + (IntPtr)m * 4) = 0;
					}
				}
				if (ptr2->IsNullable == 1)
				{
					for (int n = 0; n < num; n++)
					{
						if (((decimal?[])value)[n] != null)
						{
							if (*(int*)((byte*)((void*)ptr->pStatusMarshalBuffer) + (IntPtr)n * 4) == 0)
							{
								DecimalConv.GetBytes(((decimal?[])value)[n].Value, (IntPtr)((void*)((byte*)((void*)ptr->pDataMarshalBuffer) + (IntPtr)n * 22)));
							}
						}
						else
						{
							*(int*)((byte*)((void*)ptr->pStatusMarshalBuffer) + (IntPtr)n * 4) = 1;
						}
					}
				}
				else
				{
					for (int num3 = 0; num3 < num; num3++)
					{
						DecimalConv.GetBytes(((decimal[])value)[num3], (IntPtr)((void*)((byte*)((void*)ptr->pDataMarshalBuffer) + (IntPtr)num3 * 22)));
					}
				}
				ptr->NumOfArrElems = num;
				return;
			}
			case CustomTypeCode.Double:
			{
				int num;
				if (ptr2->IsNullable == 1)
				{
					num = ((double?[])value).Length;
				}
				else
				{
					num = ((double[])value).Length;
				}
				ptr->pStatusMarshalBuffer = Marshal.AllocCoTaskMem(num * 4);
				if (ptr->pDataMarshalBuffer == IntPtr.Zero || ptr->DataBufferSize < (long)(num * 8))
				{
					ptr->pDataMarshalBuffer = Marshal.ReAllocCoTaskMem(ptr->pDataMarshalBuffer, num * 8);
					ptr->DataBufferSize = (long)(num * 8);
				}
				if (statusArray != null)
				{
					for (int num4 = 0; num4 < num; num4++)
					{
						if (((OracleUdtStatus[])statusArray)[num4] == OracleUdtStatus.Null)
						{
							*(int*)((byte*)((void*)ptr->pStatusMarshalBuffer) + (IntPtr)num4 * 4) = 1;
						}
						else
						{
							*(int*)((byte*)((void*)ptr->pStatusMarshalBuffer) + (IntPtr)num4 * 4) = 0;
						}
					}
				}
				else
				{
					for (int num5 = 0; num5 < num; num5++)
					{
						*(int*)((byte*)((void*)ptr->pStatusMarshalBuffer) + (IntPtr)num5 * 4) = 0;
					}
				}
				if (ptr2->IsNullable == 1)
				{
					for (int num6 = 0; num6 < num; num6++)
					{
						if (((double?[])value)[num6] != null)
						{
							if (*(int*)((byte*)((void*)ptr->pStatusMarshalBuffer) + (IntPtr)num6 * 4) == 0)
							{
								*(double*)((byte*)((void*)ptr->pDataMarshalBuffer) + (IntPtr)num6 * 8) = ((double?[])value)[num6].Value;
							}
						}
						else
						{
							*(int*)((byte*)((void*)ptr->pStatusMarshalBuffer) + (IntPtr)num6 * 4) = 1;
						}
					}
				}
				else
				{
					Marshal.Copy((double[])value, 0, ptr->pDataMarshalBuffer, num);
				}
				ptr->NumOfArrElems = num;
				return;
			}
			case CustomTypeCode.Int16:
			{
				int num;
				if (ptr2->IsNullable == 1)
				{
					num = ((short?[])value).Length;
				}
				else
				{
					num = ((short[])value).Length;
				}
				ptr->pStatusMarshalBuffer = Marshal.AllocCoTaskMem(num * 4);
				if (statusArray != null)
				{
					for (int num7 = 0; num7 < num; num7++)
					{
						if (((OracleUdtStatus[])statusArray)[num7] == OracleUdtStatus.Null)
						{
							*(int*)((byte*)((void*)ptr->pStatusMarshalBuffer) + (IntPtr)num7 * 4) = 1;
						}
						else
						{
							*(int*)((byte*)((void*)ptr->pStatusMarshalBuffer) + (IntPtr)num7 * 4) = 0;
						}
					}
				}
				else
				{
					for (int num8 = 0; num8 < num; num8++)
					{
						*(int*)((byte*)((void*)ptr->pStatusMarshalBuffer) + (IntPtr)num8 * 4) = 0;
					}
				}
				if (ptr->pDataMarshalBuffer == IntPtr.Zero || ptr->DataBufferSize < (long)(num * 2))
				{
					ptr->pDataMarshalBuffer = Marshal.ReAllocCoTaskMem(ptr->pDataMarshalBuffer, num * 2);
					ptr->DataBufferSize = (long)(num * 2);
				}
				if (ptr2->IsNullable == 1)
				{
					for (int num9 = 0; num9 < num; num9++)
					{
						short? num10 = ((short?[])value)[num9];
						int? num11 = (num10 != null) ? new int?((int)num10.GetValueOrDefault()) : null;
						if (num11 != null)
						{
							if (*(int*)((byte*)((void*)ptr->pStatusMarshalBuffer) + (IntPtr)num9 * 4) == 0)
							{
								*(short*)((byte*)((void*)ptr->pDataMarshalBuffer) + (IntPtr)num9 * 2) = ((short?[])value)[num9].Value;
							}
						}
						else
						{
							*(int*)((byte*)((void*)ptr->pStatusMarshalBuffer) + (IntPtr)num9 * 4) = 1;
						}
					}
				}
				else
				{
					Marshal.Copy((short[])value, 0, ptr->pDataMarshalBuffer, num);
				}
				ptr->NumOfArrElems = num;
				return;
			}
			case CustomTypeCode.Int32:
			{
				int num;
				if (ptr2->IsNullable == 1)
				{
					num = ((int?[])value).Length;
				}
				else
				{
					num = ((int[])value).Length;
				}
				ptr->pStatusMarshalBuffer = Marshal.AllocCoTaskMem(num * 4);
				if (ptr->pDataMarshalBuffer == IntPtr.Zero || ptr->DataBufferSize < (long)(num * 4))
				{
					ptr->pDataMarshalBuffer = Marshal.ReAllocCoTaskMem(ptr->pDataMarshalBuffer, num * 4);
					ptr->DataBufferSize = (long)(num * 4);
				}
				if (statusArray != null)
				{
					for (int num12 = 0; num12 < num; num12++)
					{
						if (((OracleUdtStatus[])statusArray)[num12] == OracleUdtStatus.Null)
						{
							*(int*)((byte*)((void*)ptr->pStatusMarshalBuffer) + (IntPtr)num12 * 4) = 1;
						}
						else
						{
							*(int*)((byte*)((void*)ptr->pStatusMarshalBuffer) + (IntPtr)num12 * 4) = 0;
						}
					}
				}
				else
				{
					for (int num13 = 0; num13 < num; num13++)
					{
						*(int*)((byte*)((void*)ptr->pStatusMarshalBuffer) + (IntPtr)num13 * 4) = 0;
					}
				}
				if (ptr2->IsNullable == 1)
				{
					for (int num14 = 0; num14 < num; num14++)
					{
						if (((int?[])value)[num14] != null)
						{
							if (*(int*)((byte*)((void*)ptr->pStatusMarshalBuffer) + (IntPtr)num14 * 4) == 0)
							{
								*(int*)((byte*)((void*)ptr->pDataMarshalBuffer) + (IntPtr)num14 * 4) = ((int?[])value)[num14].Value;
							}
						}
						else
						{
							*(int*)((byte*)((void*)ptr->pStatusMarshalBuffer) + (IntPtr)num14 * 4) = 1;
						}
					}
				}
				else
				{
					Marshal.Copy((int[])value, 0, ptr->pDataMarshalBuffer, num);
				}
				ptr->NumOfArrElems = num;
				return;
			}
			case CustomTypeCode.Int64:
			{
				int num;
				if (ptr2->IsNullable == 1)
				{
					num = ((long?[])value).Length;
				}
				else
				{
					num = ((long[])value).Length;
				}
				ptr->pStatusMarshalBuffer = Marshal.AllocCoTaskMem(num * 4);
				if (ptr->pDataMarshalBuffer == IntPtr.Zero || ptr->DataBufferSize < (long)(num * 8))
				{
					ptr->pDataMarshalBuffer = Marshal.ReAllocCoTaskMem(ptr->pDataMarshalBuffer, num * 8);
					ptr->DataBufferSize = (long)(num * 8);
				}
				if (statusArray != null)
				{
					for (int num15 = 0; num15 < num; num15++)
					{
						if (((OracleUdtStatus[])statusArray)[num15] == OracleUdtStatus.Null)
						{
							*(int*)((byte*)((void*)ptr->pStatusMarshalBuffer) + (IntPtr)num15 * 4) = 1;
						}
						else
						{
							*(int*)((byte*)((void*)ptr->pStatusMarshalBuffer) + (IntPtr)num15 * 4) = 0;
						}
					}
				}
				else
				{
					for (int num16 = 0; num16 < num; num16++)
					{
						*(int*)((byte*)((void*)ptr->pStatusMarshalBuffer) + (IntPtr)num16 * 4) = 0;
					}
				}
				if (ptr2->IsNullable == 1)
				{
					for (int num17 = 0; num17 < num; num17++)
					{
						if (((long?[])value)[num17] != null)
						{
							if (*(int*)((byte*)((void*)ptr->pStatusMarshalBuffer) + (IntPtr)num17 * 4) == 0)
							{
								*(long*)((byte*)((void*)ptr->pDataMarshalBuffer) + (IntPtr)num17 * 8) = ((long?[])value)[num17].Value;
							}
						}
						else
						{
							*(int*)((byte*)((void*)ptr->pStatusMarshalBuffer) + (IntPtr)num17 * 4) = 1;
						}
					}
				}
				else
				{
					Marshal.Copy((long[])value, 0, ptr->pDataMarshalBuffer, num);
				}
				ptr->NumOfArrElems = num;
				return;
			}
			case CustomTypeCode.Single:
			{
				int num;
				if (ptr2->IsNullable == 1)
				{
					num = ((float?[])value).Length;
				}
				else
				{
					num = ((float[])value).Length;
				}
				ptr->pStatusMarshalBuffer = Marshal.AllocCoTaskMem(num * 4);
				if (ptr->pDataMarshalBuffer == IntPtr.Zero || ptr->DataBufferSize < (long)(num * 4))
				{
					ptr->pDataMarshalBuffer = Marshal.ReAllocCoTaskMem(ptr->pDataMarshalBuffer, num * 4);
					ptr->DataBufferSize = (long)(num * 4);
				}
				if (statusArray != null)
				{
					for (int num18 = 0; num18 < num; num18++)
					{
						if (((OracleUdtStatus[])statusArray)[num18] == OracleUdtStatus.Null)
						{
							*(int*)((byte*)((void*)ptr->pStatusMarshalBuffer) + (IntPtr)num18 * 4) = 1;
						}
						else
						{
							*(int*)((byte*)((void*)ptr->pStatusMarshalBuffer) + (IntPtr)num18 * 4) = 0;
						}
					}
				}
				else
				{
					for (int num19 = 0; num19 < num; num19++)
					{
						*(int*)((byte*)((void*)ptr->pStatusMarshalBuffer) + (IntPtr)num19 * 4) = 0;
					}
				}
				if (ptr2->IsNullable == 1)
				{
					for (int num20 = 0; num20 < num; num20++)
					{
						if (((float?[])value)[num20] != null)
						{
							if (*(int*)((byte*)((void*)ptr->pStatusMarshalBuffer) + (IntPtr)num20 * 4) == 0)
							{
								*(float*)((byte*)((void*)ptr->pDataMarshalBuffer) + (IntPtr)num20 * 4) = ((float?[])value)[num20].Value;
							}
						}
						else
						{
							*(int*)((byte*)((void*)ptr->pStatusMarshalBuffer) + (IntPtr)num20 * 4) = 1;
						}
					}
				}
				else
				{
					Marshal.Copy((float[])value, 0, ptr->pDataMarshalBuffer, num);
				}
				ptr->NumOfArrElems = num;
				return;
			}
			default:
			{
				if (customTypeCode != CustomTypeCode.OracleDecimal)
				{
					return;
				}
				int num = ((OracleDecimal[])value).Length;
				ptr->pStatusMarshalBuffer = Marshal.AllocCoTaskMem(num * 4);
				if (ptr->pDataMarshalBuffer == IntPtr.Zero || ptr->DataBufferSize < (long)(num * 22))
				{
					ptr->pDataMarshalBuffer = Marshal.ReAllocCoTaskMem(ptr->pDataMarshalBuffer, num * 22);
					ptr->DataBufferSize = (long)(num * 22);
				}
				if (statusArray != null)
				{
					for (int num21 = 0; num21 < num; num21++)
					{
						if (((OracleUdtStatus[])statusArray)[num21] == OracleUdtStatus.Null)
						{
							*(int*)((byte*)((void*)ptr->pStatusMarshalBuffer) + (IntPtr)num21 * 4) = 1;
						}
						else
						{
							*(int*)((byte*)((void*)ptr->pStatusMarshalBuffer) + (IntPtr)num21 * 4) = 0;
						}
					}
				}
				else
				{
					for (int num22 = 0; num22 < num; num22++)
					{
						*(int*)((byte*)((void*)ptr->pStatusMarshalBuffer) + (IntPtr)num22 * 4) = 0;
					}
				}
				for (int num23 = 0; num23 < num; num23++)
				{
					if (((OracleDecimal[])value)[num23].IsNull)
					{
						*(int*)((byte*)((void*)ptr->pStatusMarshalBuffer) + (IntPtr)num23 * 4) = 1;
					}
					else if (*(int*)((byte*)((void*)ptr->pStatusMarshalBuffer) + (IntPtr)num23 * 4) == 0)
					{
						DecimalConv.GetBytes(((OracleDecimal[])value)[num23].Value, (IntPtr)((void*)((byte*)((void*)ptr->pDataMarshalBuffer) + (IntPtr)num23 * 22)));
					}
				}
				ptr->NumOfArrElems = num;
				break;
			}
			}
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x000195BC File Offset: 0x000185BC
		internal unsafe static void SetStringArrData(IntPtr pUdt, int attrIndex, object value, IntPtr pAttrMeta, object statusArray)
		{
			OpoUdtValCtx* ptr = (OpoUdtValCtx*)((void*)pUdt);
			AttrMetaVal* ptr2 = (AttrMetaVal*)((void*)pAttrMeta);
			CustomTypeCode custTypeCode = ptr2->CustTypeCode;
			CustomTypeCode customTypeCode = custTypeCode;
			int num;
			if (customTypeCode == CustomTypeCode.Chars)
			{
				num = ((char[][])value).Length;
				int num2 = 0;
				if (ptr->pDataLen == null || ptr->NumOfArrElems < num)
				{
					ptr->pDataLen = (int*)((void*)Marshal.ReAllocCoTaskMem((IntPtr)((void*)ptr->pDataLen), num * 4));
				}
				ptr->pStatusMarshalBuffer = Marshal.AllocCoTaskMem(num * 4);
				for (int i = 0; i < num; i++)
				{
					if (((char[][])value)[i] == null || (statusArray != null && ((OracleUdtStatus[])statusArray)[i] == OracleUdtStatus.Null))
					{
						*(int*)((byte*)((void*)ptr->pStatusMarshalBuffer) + (IntPtr)i * 4) = 1;
					}
					else
					{
						*(int*)((byte*)((void*)ptr->pStatusMarshalBuffer) + (IntPtr)i * 4) = 0;
					}
				}
				for (int j = 0; j < num; j++)
				{
					if (*(int*)((byte*)((void*)ptr->pStatusMarshalBuffer) + (IntPtr)j * 4) == 0)
					{
						ptr->pDataLen[j] = ((char[][])value)[j].Length;
						num2 += ptr->pDataLen[j];
					}
				}
				num2 *= 2;
				if (ptr->pDataMarshalBuffer == IntPtr.Zero || ptr->DataBufferSize < (long)num2)
				{
					ptr->pDataMarshalBuffer = Marshal.ReAllocCoTaskMem(ptr->pDataMarshalBuffer, num2);
					ptr->DataBufferSize = (long)num2;
				}
				IntPtr intPtr = ptr->pDataMarshalBuffer;
				for (int k = 0; k < num; k++)
				{
					if (*(int*)((byte*)((void*)ptr->pStatusMarshalBuffer) + (IntPtr)k * 4) == 0)
					{
						Marshal.Copy(((char[][])value)[k], 0, intPtr, ptr->pDataLen[k]);
						intPtr = (IntPtr)((void*)((byte*)((void*)intPtr) + (IntPtr)ptr->pDataLen[k] * 2));
					}
				}
				ptr->NumOfArrElems = num;
				return;
			}
			if (customTypeCode == CustomTypeCode.String)
			{
				num = ((string[])value).Length;
				int num3 = 0;
				if (ptr->pDataLen == null || ptr->NumOfArrElems < num)
				{
					ptr->pDataLen = (int*)((void*)Marshal.ReAllocCoTaskMem((IntPtr)((void*)ptr->pDataLen), num * 4));
				}
				ptr->pStatusMarshalBuffer = Marshal.AllocCoTaskMem(num * 4);
				for (int l = 0; l < num; l++)
				{
					if (((string[])value)[l] == null || (statusArray != null && ((OracleUdtStatus[])statusArray)[l] == OracleUdtStatus.Null))
					{
						*(int*)((byte*)((void*)ptr->pStatusMarshalBuffer) + (IntPtr)l * 4) = 1;
					}
					else
					{
						*(int*)((byte*)((void*)ptr->pStatusMarshalBuffer) + (IntPtr)l * 4) = 0;
					}
				}
				for (int m = 0; m < num; m++)
				{
					if (*(int*)((byte*)((void*)ptr->pStatusMarshalBuffer) + (IntPtr)m * 4) == 0)
					{
						ptr->pDataLen[m] = ((string[])value)[m].Length;
						num3 += ptr->pDataLen[m];
					}
				}
				num3 *= 2;
				if (ptr->pDataMarshalBuffer == IntPtr.Zero || ptr->DataBufferSize < (long)num3)
				{
					ptr->pDataMarshalBuffer = Marshal.ReAllocCoTaskMem(ptr->pDataMarshalBuffer, num3);
					ptr->DataBufferSize = (long)num3;
				}
				IntPtr intPtr2 = ptr->pDataMarshalBuffer;
				for (int n = 0; n < num; n++)
				{
					if (*(int*)((byte*)((void*)ptr->pStatusMarshalBuffer) + (IntPtr)n * 4) == 0)
					{
						Marshal.Copy(((string[])value)[n].ToCharArray(), 0, intPtr2, ptr->pDataLen[n]);
						intPtr2 = (IntPtr)((void*)((byte*)((void*)intPtr2) + (IntPtr)ptr->pDataLen[n] * 2));
					}
				}
				ptr->NumOfArrElems = num;
				return;
			}
			if (customTypeCode != CustomTypeCode.OracleString)
			{
				return;
			}
			num = ((OracleString[])value).Length;
			int num4 = 0;
			if (ptr->pDataLen == null || ptr->NumOfArrElems < num)
			{
				ptr->pDataLen = (int*)((void*)Marshal.ReAllocCoTaskMem((IntPtr)((void*)ptr->pDataLen), num * 4));
			}
			ptr->pStatusMarshalBuffer = Marshal.AllocCoTaskMem(num * 4);
			for (int num5 = 0; num5 < num; num5++)
			{
				if (((OracleString[])value)[num5] == null || ((OracleString[])value)[num5].IsNull || (statusArray != null && ((OracleUdtStatus[])statusArray)[num5] == OracleUdtStatus.Null))
				{
					*(int*)((byte*)((void*)ptr->pStatusMarshalBuffer) + (IntPtr)num5 * 4) = 1;
				}
				else
				{
					*(int*)((byte*)((void*)ptr->pStatusMarshalBuffer) + (IntPtr)num5 * 4) = 0;
				}
			}
			for (int num6 = 0; num6 < num; num6++)
			{
				if (*(int*)((byte*)((void*)ptr->pStatusMarshalBuffer) + (IntPtr)num6 * 4) == 0)
				{
					ptr->pDataLen[num6] = ((OracleString[])value)[num6].Value.Length;
					num4 += ptr->pDataLen[num6];
				}
			}
			num4 *= 2;
			if (ptr->pDataMarshalBuffer == IntPtr.Zero || ptr->DataBufferSize < (long)num4)
			{
				ptr->pDataMarshalBuffer = Marshal.ReAllocCoTaskMem(ptr->pDataMarshalBuffer, num4);
				ptr->DataBufferSize = (long)num4;
			}
			IntPtr intPtr3 = ptr->pDataMarshalBuffer;
			for (int num7 = 0; num7 < num; num7++)
			{
				if (*(int*)((byte*)((void*)ptr->pStatusMarshalBuffer) + (IntPtr)num7 * 4) == 0)
				{
					Marshal.Copy(((OracleString[])value)[num7].Value.ToCharArray(), 0, intPtr3, ptr->pDataLen[num7]);
					intPtr3 = (IntPtr)((void*)((byte*)((void*)intPtr3) + (IntPtr)ptr->pDataLen[num7] * 2));
				}
			}
			ptr->NumOfArrElems = num;
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x00019B1C File Offset: 0x00018B1C
		internal unsafe static void SetNumData(IntPtr pUdt, int attrIndex, object value, IntPtr pAttrMeta, object statusArray)
		{
			OpoUdtValCtx* ptr = (OpoUdtValCtx*)((void*)pUdt);
			AttrMetaVal* ptr2 = (AttrMetaVal*)((void*)pAttrMeta);
			CustomTypeCode custTypeCode = ptr2->CustTypeCode;
			if (statusArray != null && ((OracleUdtStatus[])statusArray)[attrIndex] == OracleUdtStatus.Null)
			{
				ptr->bIsNull = 1;
				return;
			}
			CustomTypeCode customTypeCode = custTypeCode;
			switch (customTypeCode)
			{
			case CustomTypeCode.Byte:
				ptr->opoUdtAttrValCtx.m_byte = (byte)value;
				return;
			case CustomTypeCode.Bytes:
			case CustomTypeCode.Chars:
			case CustomTypeCode.DateTime:
				break;
			case CustomTypeCode.Decimal:
				if (ptr->pDataMarshalBuffer == IntPtr.Zero)
				{
					ptr->pDataMarshalBuffer = Marshal.AllocCoTaskMem(22);
					ptr->DataBufferSize = 22L;
				}
				else if (ptr->DataBufferSize < 22L)
				{
					ptr->pDataMarshalBuffer = Marshal.ReAllocCoTaskMem(ptr->pDataMarshalBuffer, 22);
					ptr->DataBufferSize = 22L;
				}
				DecimalConv.GetBytes((decimal)value, ptr->pDataMarshalBuffer);
				return;
			case CustomTypeCode.Double:
				ptr->opoUdtAttrValCtx.m_double = (double)value;
				return;
			case CustomTypeCode.Int16:
				ptr->opoUdtAttrValCtx.m_short = (short)value;
				return;
			case CustomTypeCode.Int32:
				ptr->opoUdtAttrValCtx.m_int = (int)value;
				return;
			case CustomTypeCode.Int64:
				ptr->opoUdtAttrValCtx.m_long = (long)value;
				return;
			case CustomTypeCode.Single:
				ptr->opoUdtAttrValCtx.m_float = (float)value;
				return;
			default:
				if (customTypeCode != CustomTypeCode.OracleDecimal)
				{
					return;
				}
				if (((OracleDecimal)value).IsNull)
				{
					ptr->bIsNull = 1;
					return;
				}
				if (ptr->pDataMarshalBuffer == IntPtr.Zero)
				{
					ptr->pDataMarshalBuffer = Marshal.AllocCoTaskMem(22);
					ptr->DataBufferSize = 22L;
				}
				else if (ptr->DataBufferSize < 22L)
				{
					ptr->pDataMarshalBuffer = Marshal.ReAllocCoTaskMem(ptr->pDataMarshalBuffer, 22);
					ptr->DataBufferSize = 22L;
				}
				DecimalConv.GetBytes(((OracleDecimal)value).Value, ptr->pDataMarshalBuffer);
				break;
			}
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x00019CE8 File Offset: 0x00018CE8
		internal unsafe static void SetArrayData(OracleConnection conn, IntPtr pUdt, int attrIndex, object value, object statusArray)
		{
			OpoUdtValCtx* ptr = (OpoUdtValCtx*)((void*)pUdt);
			int num = 0;
			bool flag;
			OracleUdtDescriptor oracleUdtDescriptor = OracleUdtDescriptor.GetOracleUdtDescriptor(conn, (IntPtr)(*(IntPtr*)((void*)ptr->pTDO)), false, out flag);
			if (oracleUdtDescriptor == null)
			{
				throw new InvalidOperationException();
			}
			if (oracleUdtDescriptor.m_customTypeFactory == null)
			{
				object factory = OracleUdt.GetFactory(oracleUdtDescriptor);
				oracleUdtDescriptor.DescribeCustomType(factory);
			}
			try
			{
				ptr->pOpsErrCtx = conn.m_opoConCtx.opsErrCtx;
				ptr->pTDO = oracleUdtDescriptor.m_opsDscCtx;
				ptr->pOpoDscValCtx = oracleUdtDescriptor.m_pOpoDscValCtx;
				if (oracleUdtDescriptor.m_pOpoDscValCtx->bIsArrayType == 0)
				{
					ptr->NumOfArrElems = 0;
					((IOracleCustomType)value).FromCustomObject(conn, (IntPtr)((void*)ptr));
				}
				else
				{
					OracleUdt.SetValue(conn, (IntPtr)((void*)ptr), 0, value, statusArray);
				}
				num = OpsUdt.SetArrayData(conn.m_opoConCtx.opsConCtx, ptr);
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
				if (num != 0)
				{
					OracleException.HandleError(num, conn, conn.m_opoConCtx.opsErrCtx, null);
				}
			}
			GC.KeepAlive(oracleUdtDescriptor);
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x00019DFC File Offset: 0x00018DFC
		internal unsafe static void SetObjData(OracleConnection conn, IntPtr pUdt, int attrIndex, object value, object statusArray)
		{
			OpoUdtValCtx* ptr = (OpoUdtValCtx*)((void*)pUdt);
			int num = 0;
			OracleUdtDescriptor oracleUdtDescriptor = OracleUdtDescriptor.GetOracleUdtDescriptor2(conn, (OpoDscRefCtx)OracleUdt.GetUdtName(value.GetType().FullName, conn.DataSource));
			if (oracleUdtDescriptor == null)
			{
				throw new InvalidOperationException();
			}
			if (oracleUdtDescriptor.m_customTypeFactory == null)
			{
				object factory = OracleUdt.GetFactory(oracleUdtDescriptor);
				oracleUdtDescriptor.DescribeCustomType(factory);
			}
			try
			{
				if ((IntPtr)((void*)ptr->pOpoUdtValCtx) == IntPtr.Zero)
				{
					try
					{
						try
						{
							num = OpsUdt.AllocValCtx(out ptr->pOpoUdtValCtx, oracleUdtDescriptor.AttributeCount);
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
						goto IL_12A;
					}
					finally
					{
						if (num == 0)
						{
							ptr->NumOpoUdtValCtx = oracleUdtDescriptor.AttributeCount;
						}
						else if (num != 0 && num != ErrRes.INT_ERR)
						{
							OracleException.HandleError(num, conn, ptr->pOpsErrCtx, null);
						}
					}
				}
				if (ptr->NumOpoUdtValCtx < oracleUdtDescriptor.AttributeCount)
				{
					try
					{
						num = OpsUdt.ReAllocValCtx(ref ptr->pOpoUdtValCtx, ptr->NumOpoUdtValCtx, oracleUdtDescriptor.AttributeCount);
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
						if (num == 0)
						{
							ptr->NumOpoUdtValCtx = oracleUdtDescriptor.AttributeCount;
						}
						else if (num != 0 && num != ErrRes.INT_ERR)
						{
							OracleException.HandleError(num, conn, ptr->pOpsErrCtx, null);
						}
					}
				}
				IL_12A:
				ptr->pOpsErrCtx = conn.m_opoConCtx.opsErrCtx;
				ptr->pTDO = oracleUdtDescriptor.m_opsDscCtx;
				ptr->pOpoDscValCtx = oracleUdtDescriptor.m_pOpoDscValCtx;
				for (int i = 0; i < oracleUdtDescriptor.AttributeCount; i++)
				{
					ptr->pOpoUdtValCtx[i].bIsNull = 1;
				}
				((IOracleCustomType)value).FromCustomObject(conn, (IntPtr)((void*)ptr));
				if (oracleUdtDescriptor.m_pOpoDscValCtx->TypeCode == 122)
				{
					num = OpsUdt.SetArrayData(conn.m_opoConCtx.opsConCtx, ptr);
				}
				else
				{
					num = OpsUdt.SetData(conn.m_opoConCtx.opsConCtx, ptr);
				}
			}
			catch (Exception ex3)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex3);
				}
				throw;
			}
			finally
			{
				if (num != 0)
				{
					OracleException.HandleError(num, conn, conn.m_opoConCtx.opsErrCtx, null);
				}
			}
			GC.KeepAlive(oracleUdtDescriptor);
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x0001A098 File Offset: 0x00019098
		internal unsafe static void SetDate(IntPtr pUdt, int attrIndex, object value, IntPtr pAttrMeta, object statusArray)
		{
			OpoUdtValCtx* ptr = (OpoUdtValCtx*)((void*)pUdt);
			AttrMetaVal* ptr2 = (AttrMetaVal*)((void*)pAttrMeta);
			CustomTypeCode custTypeCode = ptr2->CustTypeCode;
			if (statusArray != null && ((OracleUdtStatus[])statusArray)[attrIndex] == OracleUdtStatus.Null)
			{
				ptr->bIsNull = 1;
				return;
			}
			CustomTypeCode customTypeCode = custTypeCode;
			if (customTypeCode != CustomTypeCode.DateTime)
			{
				if (customTypeCode != CustomTypeCode.OracleDate)
				{
					return;
				}
				if (((OracleDate)value).IsNull)
				{
					ptr->bIsNull = 1;
					return;
				}
				ptr->bIsNull = 0;
				if (ptr->pDataMarshalBuffer != IntPtr.Zero)
				{
					Marshal.FreeCoTaskMem(ptr->pDataMarshalBuffer);
					ptr->pDataMarshalBuffer = IntPtr.Zero;
				}
				ptr->pDataMarshalBuffer = ((OracleDate)value).AllocValCtxFromCtx();
				return;
			}
			else
			{
				if (ptr2->IsNullable == 1 && value == null)
				{
					ptr->bIsNull = 1;
					return;
				}
				ptr->bIsNull = 0;
				if (ptr->pDataMarshalBuffer != IntPtr.Zero)
				{
					Marshal.FreeCoTaskMem(ptr->pDataMarshalBuffer);
					ptr->pDataMarshalBuffer = IntPtr.Zero;
				}
				OracleDate oracleDate = new OracleDate((DateTime)value);
				ptr->pDataMarshalBuffer = oracleDate.AllocValCtxFromCtx();
				return;
			}
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x0001A1A0 File Offset: 0x000191A0
		internal unsafe static void SetIntervalDS(IntPtr pUdt, int attrIndex, object value, IntPtr pAttrMeta, object statusArray)
		{
			OpoUdtValCtx* ptr = (OpoUdtValCtx*)((void*)pUdt);
			AttrMetaVal* ptr2 = (AttrMetaVal*)((void*)pAttrMeta);
			CustomTypeCode custTypeCode = ptr2->CustTypeCode;
			if (statusArray != null && ((OracleUdtStatus[])statusArray)[attrIndex] == OracleUdtStatus.Null)
			{
				ptr->bIsNull = 1;
				return;
			}
			CustomTypeCode customTypeCode = custTypeCode;
			if (customTypeCode != CustomTypeCode.TimeSpan)
			{
				if (customTypeCode != CustomTypeCode.OracleIntervalDS)
				{
					return;
				}
				if (((OracleIntervalDS)value).IsNull)
				{
					ptr->bIsNull = 1;
					return;
				}
				ptr->bIsNull = 0;
				if (ptr->pDataMarshalBuffer != IntPtr.Zero)
				{
					Marshal.FreeCoTaskMem(ptr->pDataMarshalBuffer);
					ptr->pDataMarshalBuffer = IntPtr.Zero;
				}
				ptr->pDataMarshalBuffer = ((OracleIntervalDS)value).DupValCtx();
				return;
			}
			else
			{
				if (ptr2->IsNullable == 1 && value == null)
				{
					ptr->bIsNull = 1;
					return;
				}
				ptr->bIsNull = 0;
				object obj = new OracleIntervalDS((TimeSpan)value);
				if (ptr->pDataMarshalBuffer != IntPtr.Zero)
				{
					Marshal.FreeCoTaskMem(ptr->pDataMarshalBuffer);
					ptr->pDataMarshalBuffer = IntPtr.Zero;
				}
				ptr->pDataMarshalBuffer = ((OracleIntervalDS)obj).DupValCtx();
				return;
			}
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x0001A2B4 File Offset: 0x000192B4
		internal unsafe static void SetIntervalYM(IntPtr pUdt, int attrIndex, object value, IntPtr pAttrMeta, object statusArray)
		{
			OpoUdtValCtx* ptr = (OpoUdtValCtx*)((void*)pUdt);
			AttrMetaVal* ptr2 = (AttrMetaVal*)((void*)pAttrMeta);
			CustomTypeCode custTypeCode = ptr2->CustTypeCode;
			if (statusArray != null && ((OracleUdtStatus[])statusArray)[attrIndex] == OracleUdtStatus.Null)
			{
				ptr->bIsNull = 1;
				return;
			}
			CustomTypeCode customTypeCode = custTypeCode;
			if (customTypeCode != CustomTypeCode.Int64)
			{
				if (customTypeCode != CustomTypeCode.OracleIntervalYM)
				{
					return;
				}
				if (((OracleIntervalYM)value).IsNull)
				{
					ptr->bIsNull = 1;
					return;
				}
				ptr->bIsNull = 0;
				if (ptr->pDataMarshalBuffer != IntPtr.Zero)
				{
					Marshal.FreeCoTaskMem(ptr->pDataMarshalBuffer);
					ptr->pDataMarshalBuffer = IntPtr.Zero;
				}
				ptr->pDataMarshalBuffer = ((OracleIntervalYM)value).DupValCtx();
				return;
			}
			else
			{
				if (ptr2->IsNullable == 1 && value == null)
				{
					ptr->bIsNull = 1;
					return;
				}
				ptr->bIsNull = 0;
				object obj = new OracleIntervalYM((long)value);
				if (ptr->pDataMarshalBuffer != IntPtr.Zero)
				{
					Marshal.FreeCoTaskMem(ptr->pDataMarshalBuffer);
					ptr->pDataMarshalBuffer = IntPtr.Zero;
				}
				ptr->pDataMarshalBuffer = ((OracleIntervalYM)obj).DupValCtx();
				return;
			}
		}

		// Token: 0x060001BA RID: 442 RVA: 0x0001A3C8 File Offset: 0x000193C8
		internal unsafe static void SetBinary(IntPtr pUdt, int attrIndex, object value, IntPtr pAttrMeta, object statusArray)
		{
			OpoUdtValCtx* ptr = (OpoUdtValCtx*)((void*)pUdt);
			AttrMetaVal* ptr2 = (AttrMetaVal*)((void*)pAttrMeta);
			CustomTypeCode custTypeCode = ptr2->CustTypeCode;
			if (statusArray != null && ((OracleUdtStatus[])statusArray)[attrIndex] == OracleUdtStatus.Null)
			{
				ptr->bIsNull = 1;
				return;
			}
			CustomTypeCode customTypeCode = custTypeCode;
			if (customTypeCode == CustomTypeCode.Bytes)
			{
				ptr->bIsNull = 0;
				int num = ((byte[])value).Length;
				if (ptr->pDataMarshalBuffer == IntPtr.Zero || ptr->DataBufferSize < (long)num)
				{
					ptr->pDataMarshalBuffer = Marshal.ReAllocCoTaskMem(ptr->pDataMarshalBuffer, num);
					ptr->DataBufferSize = (long)num;
				}
				Marshal.Copy((byte[])value, 0, ptr->pDataMarshalBuffer, num);
				ptr->DataLen = num;
				return;
			}
			if (customTypeCode != CustomTypeCode.OracleBinary)
			{
				return;
			}
			if (((OracleBinary)value).IsNull)
			{
				ptr->bIsNull = 1;
				return;
			}
			ptr->bIsNull = 0;
			int num2 = ((OracleBinary)value).Value.Length;
			if (ptr->pDataMarshalBuffer == IntPtr.Zero || ptr->DataBufferSize < (long)num2)
			{
				ptr->pDataMarshalBuffer = Marshal.ReAllocCoTaskMem(ptr->pDataMarshalBuffer, num2);
				ptr->DataBufferSize = (long)num2;
			}
			Marshal.Copy(((OracleBinary)value).Value, 0, ptr->pDataMarshalBuffer, num2);
			ptr->DataLen = num2;
		}

		// Token: 0x060001BB RID: 443 RVA: 0x0001A508 File Offset: 0x00019508
		internal unsafe static void SetClob(OracleConnection con, IntPtr pUdt, int attrIndex, object value, IntPtr pAttrMeta, object statusArray)
		{
			OpoUdtValCtx* ptr = (OpoUdtValCtx*)((void*)pUdt);
			AttrMetaVal* ptr2 = (AttrMetaVal*)((void*)pAttrMeta);
			CustomTypeCode custTypeCode = ptr2->CustTypeCode;
			bool bNClob = false;
			if (ptr2->CharsetForm == 2)
			{
				bNClob = true;
			}
			if (statusArray != null && ((OracleUdtStatus[])statusArray)[attrIndex] == OracleUdtStatus.Null)
			{
				ptr->bIsNull = 1;
				return;
			}
			CustomTypeCode customTypeCode = custTypeCode;
			if (customTypeCode != CustomTypeCode.Chars)
			{
				if (customTypeCode != CustomTypeCode.String)
				{
					if (customTypeCode != CustomTypeCode.OracleClob)
					{
						return;
					}
					if (((OracleClob)value).IsNull)
					{
						ptr->bIsNull = 1;
						return;
					}
					ptr->bIsNull = 0;
					OracleClob oracleClob = (OracleClob)((OracleClob)value).Clone();
					oracleClob.m_allocOciLobLoc = 0;
					if (ptr->pDataMarshalBuffer != IntPtr.Zero)
					{
						Marshal.FreeCoTaskMem(ptr->pDataMarshalBuffer);
						ptr->pDataMarshalBuffer = IntPtr.Zero;
					}
					int lobLocator = oracleClob.GetLobLocator(out ptr->pDataMarshalBuffer);
					oracleClob.Dispose();
					if (lobLocator != 0)
					{
						throw new OracleException(lobLocator);
					}
				}
				else
				{
					ptr->bIsNull = 0;
					OracleClob oracleClob2 = new OracleClob(con, false, bNClob);
					oracleClob2.m_allocOciLobLoc = 0;
					oracleClob2.Write(((string)value).ToCharArray(), 0, ((string)value).Length);
					if (ptr->pDataMarshalBuffer != IntPtr.Zero)
					{
						Marshal.FreeCoTaskMem(ptr->pDataMarshalBuffer);
						ptr->pDataMarshalBuffer = IntPtr.Zero;
					}
					int lobLocator2 = oracleClob2.GetLobLocator(out ptr->pDataMarshalBuffer);
					oracleClob2.Dispose();
					if (lobLocator2 != 0)
					{
						throw new OracleException(lobLocator2);
					}
				}
			}
			else
			{
				ptr->bIsNull = 0;
				OracleClob oracleClob3 = new OracleClob(con, false, bNClob);
				oracleClob3.m_allocOciLobLoc = 0;
				oracleClob3.Write((char[])value, 0, ((char[])value).Length);
				if (ptr->pDataMarshalBuffer != IntPtr.Zero)
				{
					Marshal.FreeCoTaskMem(ptr->pDataMarshalBuffer);
					ptr->pDataMarshalBuffer = IntPtr.Zero;
				}
				int lobLocator3 = oracleClob3.GetLobLocator(out ptr->pDataMarshalBuffer);
				oracleClob3.Dispose();
				if (lobLocator3 != 0)
				{
					throw new OracleException(lobLocator3);
				}
			}
		}

		// Token: 0x060001BC RID: 444 RVA: 0x0001A6F8 File Offset: 0x000196F8
		internal unsafe static void SetXml(OracleConnection con, IntPtr pUdt, int attrIndex, object value, IntPtr pAttrMeta, object statusArray)
		{
			OpoUdtValCtx* ptr = (OpoUdtValCtx*)((void*)pUdt);
			AttrMetaVal* ptr2 = (AttrMetaVal*)((void*)pAttrMeta);
			CustomTypeCode custTypeCode = ptr2->CustTypeCode;
			if (statusArray != null && ((OracleUdtStatus[])statusArray)[attrIndex] == OracleUdtStatus.Null)
			{
				ptr->bIsNull = 1;
				return;
			}
			CustomTypeCode customTypeCode = custTypeCode;
			if (customTypeCode != CustomTypeCode.Chars)
			{
				if (customTypeCode != CustomTypeCode.String)
				{
					if (customTypeCode != CustomTypeCode.OracleXmlType)
					{
						return;
					}
					if (((OracleXmlType)value).IsNull)
					{
						ptr->bIsNull = 1;
						return;
					}
					ptr->bIsNull = 0;
					OracleXmlType oracleXmlType = new OracleXmlType(con, ((OracleXmlType)value).Value);
					oracleXmlType.KeepOciXmlType();
					if (ptr->pDataMarshalBuffer != IntPtr.Zero)
					{
						Marshal.FreeCoTaskMem(ptr->pDataMarshalBuffer);
						ptr->pDataMarshalBuffer = IntPtr.Zero;
					}
					int ocixmltype = oracleXmlType.GetOCIXMLType(out ptr->pDataMarshalBuffer);
					oracleXmlType.Dispose();
					if (ocixmltype != 0)
					{
						throw new OracleException(ocixmltype);
					}
				}
				else
				{
					ptr->bIsNull = 0;
					OracleXmlType oracleXmlType2 = new OracleXmlType(con, (string)value);
					oracleXmlType2.KeepOciXmlType();
					if (ptr->pDataMarshalBuffer != IntPtr.Zero)
					{
						Marshal.FreeCoTaskMem(ptr->pDataMarshalBuffer);
						ptr->pDataMarshalBuffer = IntPtr.Zero;
					}
					int ocixmltype2 = oracleXmlType2.GetOCIXMLType(out ptr->pDataMarshalBuffer);
					oracleXmlType2.Dispose();
					if (ocixmltype2 != 0)
					{
						throw new OracleException(ocixmltype2);
					}
				}
			}
			else
			{
				ptr->bIsNull = 0;
				OracleXmlType oracleXmlType3 = new OracleXmlType(con, ((char[])value).ToString());
				oracleXmlType3.KeepOciXmlType();
				if (ptr->pDataMarshalBuffer != IntPtr.Zero)
				{
					Marshal.FreeCoTaskMem(ptr->pDataMarshalBuffer);
					ptr->pDataMarshalBuffer = IntPtr.Zero;
				}
				int ocixmltype3 = oracleXmlType3.GetOCIXMLType(out ptr->pDataMarshalBuffer);
				oracleXmlType3.Dispose();
				if (ocixmltype3 != 0)
				{
					throw new OracleException(ocixmltype3);
				}
			}
		}

		// Token: 0x060001BD RID: 445 RVA: 0x0001A8AC File Offset: 0x000198AC
		internal unsafe static void SetBlob(OracleConnection con, IntPtr pUdt, int attrIndex, object value, IntPtr pAttrMeta, object statusArray)
		{
			OpoUdtValCtx* ptr = (OpoUdtValCtx*)((void*)pUdt);
			AttrMetaVal* ptr2 = (AttrMetaVal*)((void*)pAttrMeta);
			CustomTypeCode custTypeCode = ptr2->CustTypeCode;
			if (statusArray != null && ((OracleUdtStatus[])statusArray)[attrIndex] == OracleUdtStatus.Null)
			{
				ptr->bIsNull = 1;
				return;
			}
			CustomTypeCode customTypeCode = custTypeCode;
			if (customTypeCode != CustomTypeCode.Bytes)
			{
				if (customTypeCode != CustomTypeCode.OracleBlob)
				{
					return;
				}
				if (((OracleBlob)value).IsNull)
				{
					ptr->bIsNull = 1;
					return;
				}
				ptr->bIsNull = 0;
				OracleBlob oracleBlob = (OracleBlob)((OracleBlob)value).Clone();
				oracleBlob.m_allocOciLobLoc = 0;
				if (ptr->pDataMarshalBuffer != IntPtr.Zero)
				{
					Marshal.FreeCoTaskMem(ptr->pDataMarshalBuffer);
					ptr->pDataMarshalBuffer = IntPtr.Zero;
				}
				int lobLocator = oracleBlob.GetLobLocator(out ptr->pDataMarshalBuffer);
				oracleBlob.Dispose();
				if (lobLocator != 0)
				{
					throw new OracleException(lobLocator);
				}
			}
			else
			{
				ptr->bIsNull = 0;
				OracleBlob oracleBlob2 = new OracleBlob(con);
				oracleBlob2.m_allocOciLobLoc = 0;
				oracleBlob2.Write((byte[])value, 0, ((byte[])value).Length);
				if (ptr->pDataMarshalBuffer != IntPtr.Zero)
				{
					Marshal.FreeCoTaskMem(ptr->pDataMarshalBuffer);
					ptr->pDataMarshalBuffer = IntPtr.Zero;
				}
				int lobLocator2 = oracleBlob2.GetLobLocator(out ptr->pDataMarshalBuffer);
				oracleBlob2.Dispose();
				if (lobLocator2 != 0)
				{
					throw new OracleException(lobLocator2);
				}
			}
		}

		// Token: 0x060001BE RID: 446 RVA: 0x0001A9F8 File Offset: 0x000199F8
		internal unsafe static void SetBFile(IntPtr pUdt, int attrIndex, object value, IntPtr pAttrMeta, object statusArray)
		{
			OpoUdtValCtx* ptr = (OpoUdtValCtx*)((void*)pUdt);
			AttrMetaVal* ptr2 = (AttrMetaVal*)((void*)pAttrMeta);
			CustomTypeCode custTypeCode = ptr2->CustTypeCode;
			if (statusArray != null && ((OracleUdtStatus[])statusArray)[attrIndex] == OracleUdtStatus.Null)
			{
				ptr->bIsNull = 1;
				return;
			}
			CustomTypeCode customTypeCode = custTypeCode;
			if (customTypeCode == CustomTypeCode.Bytes)
			{
				ptr->bIsNull = 0;
				throw new ArgumentException(OpoErrResManager.GetErrorMesg(-2903, new string[]
				{
					"'" + value.GetType().FullName + "'",
					"'BFILE'"
				}));
			}
			if (customTypeCode != CustomTypeCode.OracleBFile)
			{
				return;
			}
			if (((OracleBFile)value).IsNull)
			{
				ptr->bIsNull = 1;
				return;
			}
			ptr->bIsNull = 0;
			OracleBFile oracleBFile = (OracleBFile)((OracleBFile)value).Clone();
			oracleBFile.m_allocOciLobLoc = 0;
			if (ptr->pDataMarshalBuffer != IntPtr.Zero)
			{
				Marshal.FreeCoTaskMem(ptr->pDataMarshalBuffer);
				ptr->pDataMarshalBuffer = IntPtr.Zero;
			}
			int lobLocator = oracleBFile.GetLobLocator(out ptr->pDataMarshalBuffer);
			oracleBFile.Dispose();
			if (lobLocator != 0)
			{
				throw new OracleException(lobLocator);
			}
		}

		// Token: 0x060001BF RID: 447 RVA: 0x0001AB10 File Offset: 0x00019B10
		internal unsafe static void SetREF(OracleConnection con, IntPtr pUdt, int attrIndex, object value, IntPtr pAttrMeta, object statusArray)
		{
			OpoUdtValCtx* ptr = (OpoUdtValCtx*)((void*)pUdt);
			AttrMetaVal* ptr2 = (AttrMetaVal*)((void*)pAttrMeta);
			CustomTypeCode custTypeCode = ptr2->CustTypeCode;
			if (statusArray != null && ((OracleUdtStatus[])statusArray)[attrIndex] == OracleUdtStatus.Null)
			{
				ptr->bIsNull = 1;
				return;
			}
			CustomTypeCode customTypeCode = custTypeCode;
			if (customTypeCode == CustomTypeCode.String)
			{
				ptr->bIsNull = 0;
				if (ptr->pDataMarshalBuffer != IntPtr.Zero)
				{
					Marshal.FreeCoTaskMem(ptr->pDataMarshalBuffer);
					ptr->pDataMarshalBuffer = IntPtr.Zero;
				}
				ptr->pDataMarshalBuffer = Marshal.StringToCoTaskMemUni((string)value);
				ptr->DataLen = ((string)value).Length;
				ptr->DataBufferSize = (long)ptr->DataLen;
				return;
			}
			if (customTypeCode != CustomTypeCode.OracleRef)
			{
				return;
			}
			if (((OracleRef)value).IsNull)
			{
				ptr->bIsNull = 1;
				return;
			}
			ptr->bIsNull = 0;
			if (ptr->pDataMarshalBuffer != IntPtr.Zero)
			{
				Marshal.FreeCoTaskMem(ptr->pDataMarshalBuffer);
				ptr->pDataMarshalBuffer = IntPtr.Zero;
			}
			ptr->pDataMarshalBuffer = Marshal.StringToCoTaskMemUni(((OracleRef)value).Value);
			ptr->DataLen = ((OracleRef)value).Value.Length;
			ptr->DataBufferSize = (long)ptr->DataLen;
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x0001AC3C File Offset: 0x00019C3C
		internal unsafe static void SetTimeStamp(IntPtr pUdt, int attrIndex, object value, IntPtr pAttrMeta, object statusArray)
		{
			OpoUdtValCtx* ptr = (OpoUdtValCtx*)((void*)pUdt);
			AttrMetaVal* ptr2 = (AttrMetaVal*)((void*)pAttrMeta);
			CustomTypeCode custTypeCode = ptr2->CustTypeCode;
			if (statusArray != null && ((OracleUdtStatus[])statusArray)[attrIndex] == OracleUdtStatus.Null)
			{
				ptr->bIsNull = 1;
				return;
			}
			CustomTypeCode customTypeCode = custTypeCode;
			if (customTypeCode != CustomTypeCode.DateTime)
			{
				if (customTypeCode != CustomTypeCode.OracleTimeStamp)
				{
					return;
				}
				if (((OracleTimeStamp)value).IsNull)
				{
					ptr->bIsNull = 1;
					return;
				}
				ptr->bIsNull = 0;
				if (ptr->pDataMarshalBuffer != IntPtr.Zero)
				{
					Marshal.FreeCoTaskMem(ptr->pDataMarshalBuffer);
					ptr->pDataMarshalBuffer = IntPtr.Zero;
				}
				ptr->pDataMarshalBuffer = ((OracleTimeStamp)value).DupValCtx();
				return;
			}
			else
			{
				if (ptr2->IsNullable == 1 && value == null)
				{
					ptr->bIsNull = 1;
					return;
				}
				ptr->bIsNull = 0;
				if (ptr->pDataMarshalBuffer != IntPtr.Zero)
				{
					Marshal.FreeCoTaskMem(ptr->pDataMarshalBuffer);
					ptr->pDataMarshalBuffer = IntPtr.Zero;
				}
				object obj = new OracleTimeStamp((DateTime)value);
				ptr->pDataMarshalBuffer = ((OracleTimeStamp)obj).DupValCtx();
				return;
			}
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x0001AD50 File Offset: 0x00019D50
		internal unsafe static void SetTimeStampLTZ(IntPtr pUdt, int attrIndex, object value, IntPtr pAttrMeta, object statusArray)
		{
			OpoUdtValCtx* ptr = (OpoUdtValCtx*)((void*)pUdt);
			AttrMetaVal* ptr2 = (AttrMetaVal*)((void*)pAttrMeta);
			CustomTypeCode custTypeCode = ptr2->CustTypeCode;
			if (statusArray != null && ((OracleUdtStatus[])statusArray)[attrIndex] == OracleUdtStatus.Null)
			{
				ptr->bIsNull = 1;
				return;
			}
			CustomTypeCode customTypeCode = custTypeCode;
			if (customTypeCode != CustomTypeCode.DateTime)
			{
				if (customTypeCode != CustomTypeCode.OracleTimeStampLTZ)
				{
					return;
				}
				if (((OracleTimeStampLTZ)value).IsNull)
				{
					ptr->bIsNull = 1;
					return;
				}
				ptr->bIsNull = 0;
				if (ptr->pDataMarshalBuffer != IntPtr.Zero)
				{
					Marshal.FreeCoTaskMem(ptr->pDataMarshalBuffer);
					ptr->pDataMarshalBuffer = IntPtr.Zero;
				}
				ptr->pDataMarshalBuffer = ((OracleTimeStampLTZ)value).DupValCtx();
				return;
			}
			else
			{
				if (ptr2->IsNullable == 1 && value == null)
				{
					ptr->bIsNull = 1;
					return;
				}
				ptr->bIsNull = 0;
				if (ptr->pDataMarshalBuffer != IntPtr.Zero)
				{
					Marshal.FreeCoTaskMem(ptr->pDataMarshalBuffer);
					ptr->pDataMarshalBuffer = IntPtr.Zero;
				}
				object obj = new OracleTimeStampLTZ((DateTime)value);
				ptr->pDataMarshalBuffer = ((OracleTimeStampLTZ)obj).DupValCtx();
				return;
			}
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x0001AE64 File Offset: 0x00019E64
		internal unsafe static void SetTimeStampTZ(IntPtr pUdt, int attrIndex, object value, IntPtr pAttrMeta, object statusArray)
		{
			OpoUdtValCtx* ptr = (OpoUdtValCtx*)((void*)pUdt);
			AttrMetaVal* ptr2 = (AttrMetaVal*)((void*)pAttrMeta);
			CustomTypeCode custTypeCode = ptr2->CustTypeCode;
			if (statusArray != null && ((OracleUdtStatus[])statusArray)[attrIndex] == OracleUdtStatus.Null)
			{
				ptr->bIsNull = 1;
				return;
			}
			CustomTypeCode customTypeCode = custTypeCode;
			if (customTypeCode != CustomTypeCode.DateTime)
			{
				if (customTypeCode != CustomTypeCode.OracleTimeStampTZ)
				{
					return;
				}
				if (((OracleTimeStampTZ)value).IsNull)
				{
					ptr->bIsNull = 1;
					return;
				}
				ptr->bIsNull = 0;
				if (ptr->pDataMarshalBuffer != IntPtr.Zero)
				{
					Marshal.FreeCoTaskMem(ptr->pDataMarshalBuffer);
					ptr->pDataMarshalBuffer = IntPtr.Zero;
				}
				ptr->pDataMarshalBuffer = ((OracleTimeStampTZ)value).DupValCtx();
				return;
			}
			else
			{
				if (ptr2->IsNullable == 1 && value == null)
				{
					ptr->bIsNull = 1;
					return;
				}
				ptr->bIsNull = 0;
				if (ptr->pDataMarshalBuffer != IntPtr.Zero)
				{
					Marshal.FreeCoTaskMem(ptr->pDataMarshalBuffer);
					ptr->pDataMarshalBuffer = IntPtr.Zero;
				}
				object obj = new OracleTimeStampTZ((DateTime)value);
				ptr->pDataMarshalBuffer = ((OracleTimeStampTZ)obj).DupValCtx();
				return;
			}
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x0001AF78 File Offset: 0x00019F78
		internal unsafe static void SetStringData(IntPtr pUdt, int attrIndex, object value, IntPtr pAttrMeta, object statusArray)
		{
			OpoUdtValCtx* ptr = (OpoUdtValCtx*)((void*)pUdt);
			AttrMetaVal* ptr2 = (AttrMetaVal*)((void*)pAttrMeta);
			CustomTypeCode custTypeCode = ptr2->CustTypeCode;
			if (statusArray != null && ((OracleUdtStatus[])statusArray)[attrIndex] == OracleUdtStatus.Null)
			{
				ptr->bIsNull = 1;
				return;
			}
			CustomTypeCode customTypeCode = custTypeCode;
			if (customTypeCode == CustomTypeCode.Chars)
			{
				ptr->bIsNull = 0;
				int num = ((char[])value).Length;
				if (ptr->pDataMarshalBuffer != IntPtr.Zero)
				{
					Marshal.FreeCoTaskMem(ptr->pDataMarshalBuffer);
					ptr->pDataMarshalBuffer = IntPtr.Zero;
				}
				ptr->pDataMarshalBuffer = Marshal.AllocCoTaskMem(num * 2);
				ptr->DataBufferSize = (long)num;
				Marshal.Copy((char[])value, 0, ptr->pDataMarshalBuffer, num);
				ptr->DataLen = num;
				return;
			}
			if (customTypeCode == CustomTypeCode.String)
			{
				ptr->bIsNull = 0;
				int length = ((string)value).Length;
				if (ptr->pDataMarshalBuffer != IntPtr.Zero)
				{
					Marshal.FreeCoTaskMem(ptr->pDataMarshalBuffer);
					ptr->pDataMarshalBuffer = IntPtr.Zero;
				}
				ptr->pDataMarshalBuffer = Marshal.StringToCoTaskMemUni((string)value);
				ptr->DataBufferSize = (long)length;
				ptr->DataLen = length;
				return;
			}
			if (customTypeCode != CustomTypeCode.OracleString)
			{
				return;
			}
			if (((OracleString)value).IsNull)
			{
				ptr->bIsNull = 1;
				return;
			}
			ptr->bIsNull = 0;
			string value2 = ((OracleString)value).Value;
			int length2 = value2.Length;
			if (ptr->pDataMarshalBuffer != IntPtr.Zero)
			{
				Marshal.FreeCoTaskMem(ptr->pDataMarshalBuffer);
				ptr->pDataMarshalBuffer = IntPtr.Zero;
			}
			ptr->pDataMarshalBuffer = Marshal.StringToCoTaskMemUni(value2);
			ptr->DataBufferSize = (long)length2;
			ptr->DataLen = length2;
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x0001B118 File Offset: 0x0001A118
		internal unsafe static void SetData(OracleConnection con, IntPtr pUdt, int attrIndex, OraType oraType, IntPtr pAttrMetaVal, object value, object statusArray)
		{
			OpoUdtValCtx* value2 = (OpoUdtValCtx*)((void*)pUdt);
			if (oraType <= OraType.ORA_LONGVARRAW)
			{
				if (oraType <= OraType.ORA_VARRAW)
				{
					if (oraType == OraType.ORA_DATE)
					{
						goto IL_A1;
					}
					if (oraType != OraType.ORA_VARRAW)
					{
						return;
					}
				}
				else
				{
					switch (oraType)
					{
					case OraType.ORA_RAW:
					case OraType.ORA_LONGRAW:
						break;
					default:
						if (oraType != OraType.ORA_LONGVARRAW)
						{
							return;
						}
						break;
					}
				}
				OracleUdt.SetBinary((IntPtr)((void*)value2), attrIndex, value, pAttrMetaVal, statusArray);
				return;
			}
			if (oraType <= OraType.ORA_OCIDate)
			{
				switch (oraType)
				{
				case OraType.ORA_OCIRef:
					OracleUdt.SetREF(con, (IntPtr)((void*)value2), attrIndex, value, pAttrMetaVal, statusArray);
					break;
				case (OraType)111:
					break;
				case OraType.ORA_OCICLobLocator:
				case (OraType)115:
					OracleUdt.SetClob(con, (IntPtr)((void*)value2), attrIndex, value, pAttrMetaVal, statusArray);
					return;
				case OraType.ORA_OCIBLobLocator:
					OracleUdt.SetBlob(con, (IntPtr)((void*)value2), attrIndex, value, pAttrMetaVal, statusArray);
					return;
				case OraType.ORA_OCIBFileLocator:
					OracleUdt.SetBFile((IntPtr)((void*)value2), attrIndex, value, pAttrMetaVal, statusArray);
					return;
				default:
					if (oraType != OraType.ORA_OCIDate)
					{
						return;
					}
					goto IL_A1;
				}
			}
			else
			{
				switch (oraType)
				{
				case (OraType)184:
					goto IL_A1;
				case (OraType)185:
				case OraType.ORA_TIME_TZ:
					break;
				case OraType.ORA_TIMESTAMP:
					OracleUdt.SetTimeStamp((IntPtr)((void*)value2), attrIndex, value, pAttrMetaVal, statusArray);
					return;
				case OraType.ORA_TIMESTAMP_TZ:
					OracleUdt.SetTimeStampTZ((IntPtr)((void*)value2), attrIndex, value, pAttrMetaVal, statusArray);
					return;
				case OraType.ORA_INTERVAL_YM:
					OracleUdt.SetIntervalYM((IntPtr)((void*)value2), attrIndex, value, pAttrMetaVal, statusArray);
					return;
				case OraType.ORA_INTERVAL_DS:
					OracleUdt.SetIntervalDS((IntPtr)((void*)value2), attrIndex, value, pAttrMetaVal, statusArray);
					return;
				default:
					if (oraType != OraType.ORA_TIMESTAMP_LTZ)
					{
						return;
					}
					OracleUdt.SetTimeStampLTZ((IntPtr)((void*)value2), attrIndex, value, pAttrMetaVal, statusArray);
					return;
				}
			}
			return;
			IL_A1:
			OracleUdt.SetDate((IntPtr)((void*)value2), attrIndex, value, pAttrMetaVal, statusArray);
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x0001B29C File Offset: 0x0001A29C
		public unsafe static void SetValue(OracleConnection con, IntPtr pUdt, int attrIndex, object value, object statusArray)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleUdt::SetValue(2)\n"
				});
			}
			if (con == null)
			{
				throw new ArgumentNullException("con");
			}
			if (pUdt == IntPtr.Zero)
			{
				throw new ArgumentException("pUdt");
			}
			OpoUdtValCtx* ptr = (OpoUdtValCtx*)((void*)pUdt);
			if (ptr->pStatusMarshalBuffer != IntPtr.Zero)
			{
				Marshal.FreeCoTaskMem(ptr->pStatusMarshalBuffer);
				ptr->pStatusMarshalBuffer = IntPtr.Zero;
			}
			if (ptr->pOpoDscValCtx->TypeCode == 122)
			{
				if (attrIndex != 0)
				{
					throw new ArgumentOutOfRangeException("attrIndex");
				}
				if (value == null || value == DBNull.Value)
				{
					ptr->bIsNull = 1;
					return;
				}
				ptr->bIsNull = 0;
				OracleUdt.SetArrayValue(con, pUdt, attrIndex, value, statusArray);
				return;
			}
			else
			{
				if (attrIndex < 0 || (long)attrIndex >= (long)((ulong)ptr->pOpoDscValCtx->NumAttrs))
				{
					throw new ArgumentOutOfRangeException("attrIndex");
				}
				OpoUdtValCtx* ptr2 = ptr->pOpoUdtValCtx + attrIndex;
				AttrMetaVal* ptr3 = ptr->pOpoDscValCtx->pAttrMetaVals + attrIndex;
				if (ptr3->CustTypeCode == (CustomTypeCode)0)
				{
					throw new ArgumentException(OpoErrResManager.GetErrorMesg(-2905, new string[0]));
				}
				if (value == null || value == DBNull.Value)
				{
					ptr2->bIsNull = 1;
					return;
				}
				if (value is INullable && ((INullable)value).IsNull)
				{
					ptr2->bIsNull = 1;
					return;
				}
				ptr2->bIsNull = 0;
				CustomTypeCode custTypeCode = ptr3->CustTypeCode;
				SQLT oraType = (SQLT)ptr3->OraType;
				if (oraType <= SQLT.NCO)
				{
					switch (oraType)
					{
					case SQLT.CHR:
					case SQLT.STR:
					case SQLT.LNG:
					case SQLT.VCS:
						goto IL_2F1;
					case SQLT.NUM:
					case SQLT.INT:
					case SQLT.FLT:
					case SQLT.VNU:
					case SQLT.BFLT:
					case SQLT.BDBL:
						break;
					case SQLT.PDN:
					case SQLT.NON:
					case SQLT.RID:
						goto IL_4D3;
					case SQLT.DAT:
						goto IL_30B;
					case (SQLT)13:
					case (SQLT)14:
					case (SQLT)16:
					case (SQLT)17:
					case (SQLT)18:
					case (SQLT)19:
					case (SQLT)20:
						goto IL_4D9;
					case SQLT.VBI:
					case SQLT.BIN:
					case SQLT.LBI:
						goto IL_3A7;
					default:
						if (oraType != SQLT.UIN)
						{
							switch (oraType)
							{
							case SQLT.SLS:
							case SQLT.CUR:
							case SQLT.RDD:
							case SQLT.LAB:
							case SQLT.OSL:
							case SQLT.RSET:
								goto IL_4D3;
							case (SQLT)92:
							case (SQLT)93:
							case (SQLT)98:
							case (SQLT)99:
							case (SQLT)103:
							case (SQLT)107:
							case (SQLT)109:
							case (SQLT)111:
							case (SQLT)117:
							case (SQLT)118:
							case (SQLT)119:
							case (SQLT)120:
							case (SQLT)121:
								goto IL_4D9;
							case SQLT.LVC:
							case SQLT.AFC:
							case SQLT.AVC:
								goto IL_2F1;
							case SQLT.LVB:
								goto IL_3A7;
							case SQLT.IBFL:
							case SQLT.IBDL:
								break;
							case SQLT.NTY:
							case SQLT.NCO:
								goto IL_42C;
							case SQLT.REF:
								OracleUdt.SetREF(con, (IntPtr)((void*)ptr2), attrIndex, value, (IntPtr)((void*)ptr3), statusArray);
								goto IL_4D9;
							case SQLT.CLOB:
							case SQLT.CFILEE:
								OracleUdt.SetClob(con, (IntPtr)((void*)ptr2), attrIndex, value, (IntPtr)((void*)ptr3), statusArray);
								goto IL_4D9;
							case SQLT.BLOB:
								OracleUdt.SetBlob(con, (IntPtr)((void*)ptr2), attrIndex, value, (IntPtr)((void*)ptr3), statusArray);
								goto IL_4D9;
							case SQLT.BFILEE:
								OracleUdt.SetBFile((IntPtr)((void*)ptr2), attrIndex, value, (IntPtr)((void*)ptr3), statusArray);
								goto IL_4D9;
							default:
								goto IL_4D9;
							}
						}
						break;
					}
					OracleUdt.SetNumData((IntPtr)((void*)ptr2), attrIndex, value, (IntPtr)((void*)ptr3), statusArray);
					goto IL_4D9;
					IL_3A7:
					OracleUdt.SetBinary((IntPtr)((void*)ptr2), attrIndex, value, (IntPtr)((void*)ptr3), statusArray);
					goto IL_4D9;
					IL_4D3:
					throw new NotSupportedException();
				}
				if (oraType <= SQLT.INTERVAL_DS)
				{
					switch (oraType)
					{
					case SQLT.VST:
						break;
					case SQLT.ODT:
						goto IL_30B;
					default:
						switch (oraType)
						{
						case SQLT.DATE:
							goto IL_30B;
						case SQLT.TIME:
						case SQLT.TIME_TZ:
							goto IL_4D9;
						case SQLT.TIMESTAMP:
							OracleUdt.SetTimeStamp((IntPtr)((void*)ptr2), attrIndex, value, (IntPtr)((void*)ptr3), statusArray);
							goto IL_4D9;
						case SQLT.TIMESTAMP_TZ:
							OracleUdt.SetTimeStampTZ((IntPtr)((void*)ptr2), attrIndex, value, (IntPtr)((void*)ptr3), statusArray);
							goto IL_4D9;
						case SQLT.INTERVAL_YM:
							OracleUdt.SetIntervalYM((IntPtr)((void*)ptr2), attrIndex, value, (IntPtr)((void*)ptr3), statusArray);
							goto IL_4D9;
						case SQLT.INTERVAL_DS:
							OracleUdt.SetIntervalDS((IntPtr)((void*)ptr2), attrIndex, value, (IntPtr)((void*)ptr3), statusArray);
							goto IL_4D9;
						default:
							goto IL_4D9;
						}
						break;
					}
				}
				else
				{
					if (oraType == SQLT.TIMESTAMP_LTZ)
					{
						OracleUdt.SetTimeStampLTZ((IntPtr)((void*)ptr2), attrIndex, value, (IntPtr)((void*)ptr3), statusArray);
						goto IL_4D9;
					}
					if (oraType != SQLT.PNTY)
					{
						goto IL_4D9;
					}
					goto IL_42C;
				}
				IL_2F1:
				OracleUdt.SetStringData((IntPtr)((void*)ptr2), attrIndex, value, (IntPtr)((void*)ptr3), statusArray);
				goto IL_4D9;
				IL_30B:
				OracleUdt.SetDate((IntPtr)((void*)ptr2), attrIndex, value, (IntPtr)((void*)ptr3), statusArray);
				goto IL_4D9;
				IL_42C:
				if (ptr3->TypeCode == 108)
				{
					OracleUdt.SetObjData(con, (IntPtr)((void*)ptr2), attrIndex, value, statusArray);
				}
				else if (ptr3->TypeCode == 58)
				{
					OracleUdt.SetXml(con, (IntPtr)((void*)ptr2), attrIndex, value, (IntPtr)((void*)ptr3), statusArray);
				}
				else
				{
					bool flag;
					OracleUdtDescriptor oracleUdtDescriptor = OracleUdtDescriptor.GetOracleUdtDescriptor(con, (IntPtr)(*(IntPtr*)((void*)ptr->pTDO)), false, out flag);
					OracleUdtDescriptor oracleUdtDescriptor2;
					if (ptr->pOpoDscValCtx->TypeCode == 108)
					{
						oracleUdtDescriptor2 = oracleUdtDescriptor.GetObjAttrUdtDescriptor(attrIndex);
					}
					else
					{
						oracleUdtDescriptor2 = oracleUdtDescriptor.GetArrElemUdtDescriptor();
					}
					ptr2->pTDO = oracleUdtDescriptor2.m_opsDscCtx;
					OracleUdt.SetArrayData(con, (IntPtr)((void*)ptr2), 0, value, statusArray);
					GC.KeepAlive(oracleUdtDescriptor);
				}
				IL_4D9:
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleUdt::SetValue(2)\n"
					});
				}
				return;
			}
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x0001B7A4 File Offset: 0x0001A7A4
		public static void SetValue(OracleConnection con, IntPtr pUdt, string attrName, object value)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleUdt::SetValue(1)\n"
				});
			}
			OracleUdt.SetValue(con, pUdt, attrName, value, null);
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleUdt::SetValue(1)\n"
				});
			}
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x0001B7F8 File Offset: 0x0001A7F8
		public static void SetValue(OracleConnection con, IntPtr pUdt, int attrIndex, object value)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleUdt::SetValue(0)\n"
				});
			}
			OracleUdt.SetValue(con, pUdt, attrIndex, value, null);
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleUdt::SetValue(0)\n"
				});
			}
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x0001B84C File Offset: 0x0001A84C
		public unsafe static void SetValue(OracleConnection con, IntPtr pUdt, string attrName, object value, object statusArray)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleUdt::SetValue(3)\n"
				});
			}
			if (con == null)
			{
				throw new ArgumentNullException("con");
			}
			if (pUdt == IntPtr.Zero)
			{
				throw new ArgumentException("pUdt");
			}
			OpoUdtValCtx* ptr = (OpoUdtValCtx*)((void*)pUdt);
			int attrIndex;
			if (ptr->pOpoDscValCtx->TypeCode == 108)
			{
				if (attrName == null)
				{
					throw new ArgumentNullException("attrName");
				}
				if (attrName == "")
				{
					throw new ArgumentException("attrName");
				}
				bool flag;
				OracleUdtDescriptor oracleUdtDescriptor = OracleUdtDescriptor.GetOracleUdtDescriptor(con, (IntPtr)(*(IntPtr*)((void*)ptr->pTDO)), false, out flag);
				oracleUdtDescriptor.GetMetaDataTable();
				object obj = oracleUdtDescriptor.m_attrNameToIndex[attrName];
				if (obj == null)
				{
					throw new ArgumentException("attrName");
				}
				attrIndex = (int)obj;
			}
			else
			{
				if (attrName != null && !(attrName == ""))
				{
					throw new ArgumentException("attrName");
				}
				attrIndex = 0;
			}
			OracleUdt.SetValue(con, pUdt, attrIndex, value, statusArray);
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleUdt::SetValue(3)\n"
				});
			}
		}

		// Token: 0x0400010F RID: 271
		internal const string CustomTypeName = "customTypeName";

		// Token: 0x04000110 RID: 272
		private static object s_lockObj = new object();

		// Token: 0x04000111 RID: 273
		internal static Hashtable s_mapUdtNameToMappingObj;

		// Token: 0x04000112 RID: 274
		internal static Hashtable s_mapUdtNameToFactory = new Hashtable();

		// Token: 0x04000113 RID: 275
		internal static Hashtable s_mapCustomTypeNameToUdtName = new Hashtable();
	}
}
