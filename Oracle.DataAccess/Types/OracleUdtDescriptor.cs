using System;
using System.Collections;
using System.Data;
using System.Reflection;
using System.Runtime.InteropServices;
using Oracle.DataAccess.Client;

namespace Oracle.DataAccess.Types
{
	// Token: 0x0200013E RID: 318
	internal sealed class OracleUdtDescriptor : MarshalByRefObject
	{
		// Token: 0x06000CAE RID: 3246 RVA: 0x00084334 File Offset: 0x00083334
		public static OracleUdtDescriptor GetOracleUdtDescriptor(OracleConnection con, string udtTypeName)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleUdtDescriptor::GetOracleUdtDescriptor(OracleConnection, string)\n"
				});
			}
			if (con == null)
			{
				throw new ArgumentNullException("con");
			}
			if (udtTypeName == null)
			{
				throw new ArgumentNullException("udtTypeName");
			}
			if (udtTypeName == string.Empty)
			{
				throw new ArgumentException("udtTypeName");
			}
			string schemaName = null;
			string typeName = udtTypeName;
			int num = udtTypeName.LastIndexOf('.');
			if (num != -1)
			{
				schemaName = udtTypeName.Substring(0, num);
				typeName = udtTypeName.Substring(num + 1);
			}
			OracleUdtDescriptor oracleUdtDescriptor = OracleUdtDescriptor.GetOracleUdtDescriptor(con, schemaName, typeName);
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleUdtDescriptor::GetOracleUdtDescriptor(OracleConnection, string)\n"
				});
			}
			return oracleUdtDescriptor;
		}

		// Token: 0x06000CAF RID: 3247 RVA: 0x000843E4 File Offset: 0x000833E4
		internal static OracleUdtDescriptor GetOracleUdtDescriptor2(OracleConnection con, OpoDscRefCtx opoDscRefCtx)
		{
			if (con == null)
			{
				throw new ArgumentNullException("con");
			}
			if (opoDscRefCtx == null)
			{
				throw new ArgumentNullException("opoDscRefCtx");
			}
			return OracleUdtDescriptor.GetOracleUdtDescriptor(con, opoDscRefCtx.SchemaName, opoDscRefCtx.TypeName);
		}

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x06000CB0 RID: 3248 RVA: 0x00084424 File Offset: 0x00083424
		public unsafe int AttributeCount
		{
			get
			{
				int num = 0;
				OciTypeCode ociTypeCode;
				if (this.m_pOpoDscValCtx->bDescribedUdt == 0)
				{
					ociTypeCode = this.GetUdtTypeCode();
				}
				else
				{
					ociTypeCode = (OciTypeCode)this.m_pOpoDscValCtx->TypeCode;
				}
				if (ociTypeCode == OciTypeCode.OBJECT || ociTypeCode == OciTypeCode.OPAQUE)
				{
					if (this.m_pOpoDscValCtx->bFetchedNumObjAttrs == 0)
					{
						if (this.m_connection.m_opoConCtx.opsConCtx == IntPtr.Zero)
						{
							throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_CLOSED, new string[0]));
						}
						if (this.m_connection.m_conSignature != this.m_conSignature)
						{
							throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_REOPENED, new string[0]));
						}
						try
						{
							num = OpsDsc.GetNumObjAttrs(this.m_opsConCtx, this.m_opsErrCtx, this.m_opsDscCtx, this.m_pOpoDscValCtx);
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
								OracleException.HandleError(num, this.m_connection, this.m_opsErrCtx, this);
							}
						}
						this.m_pOpoDscValCtx->bFetchedNumObjAttrs = 1;
					}
					return (int)this.m_pOpoDscValCtx->NumAttrs;
				}
				return 0;
			}
		}

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x06000CB1 RID: 3249 RVA: 0x0008454C File Offset: 0x0008354C
		public unsafe int MaxSize
		{
			get
			{
				int num = 0;
				OciTypeCode ociTypeCode;
				if (this.m_pOpoDscValCtx->bDescribedUdt == 0)
				{
					ociTypeCode = this.GetUdtTypeCode();
				}
				else
				{
					ociTypeCode = (OciTypeCode)this.m_pOpoDscValCtx->TypeCode;
				}
				if (ociTypeCode == OciTypeCode.OBJECT || ociTypeCode == OciTypeCode.OPAQUE || (this.m_pOpoDscValCtx->CollTypeCode != 0 && this.m_pOpoDscValCtx->CollTypeCode == 248))
				{
					return 0;
				}
				if (!this.m_bFetchedNumArrElems)
				{
					if (this.m_connection.m_opoConCtx.opsConCtx == IntPtr.Zero)
					{
						throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_CLOSED, new string[0]));
					}
					if (this.m_connection.m_conSignature != this.m_conSignature)
					{
						throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_REOPENED, new string[0]));
					}
					try
					{
						num = OpsDsc.GetNumArrElems(this.m_opsConCtx, this.m_opsErrCtx, this.m_opsDscCtx, this.m_pOpoDscValCtx);
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
							OracleException.HandleError(num, this.m_connection, this.m_opsErrCtx, this);
						}
					}
					this.m_bFetchedNumArrElems = true;
				}
				return (int)this.m_pOpoDscValCtx->NumAttrs;
			}
		}

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x06000CB2 RID: 3250 RVA: 0x00084688 File Offset: 0x00083688
		public unsafe OracleDbType OracleDbType
		{
			get
			{
				if (!this.m_bSetOracleDbType)
				{
					this.m_oraDbType = OracleDbType.Object;
					OciTypeCode ociTypeCode;
					if (this.m_pOpoDscValCtx->bDescribedUdt == 0)
					{
						ociTypeCode = this.GetUdtTypeCode();
					}
					else
					{
						ociTypeCode = (OciTypeCode)this.m_pOpoDscValCtx->TypeCode;
					}
					if (ociTypeCode == OciTypeCode.NAMEDCOLLECTION)
					{
						if (this.m_pOpoDscValCtx->CollTypeCode == 0)
						{
							this.GetArrTypeCode();
						}
						this.m_oraDbType = OracleDbType.Array;
					}
					else if (ociTypeCode == OciTypeCode.OPAQUE && this.UdtTypeName == OracleUdtDescriptor.s_xmlTypeName)
					{
						this.m_oraDbType = OracleDbType.XmlType;
					}
					this.m_bSetOracleDbType = true;
				}
				return this.m_oraDbType;
			}
		}

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x06000CB3 RID: 3251 RVA: 0x0008471B File Offset: 0x0008371B
		public string SchemaName
		{
			get
			{
				if (!this.m_bFetchedUdtTypeName)
				{
					this.GetUdtTypeName();
					this.m_bFetchedUdtTypeName = true;
				}
				return this.m_opoDscRefCtx.SchemaName;
			}
		}

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x06000CB4 RID: 3252 RVA: 0x00084740 File Offset: 0x00083740
		public string UdtTypeName
		{
			get
			{
				if (!this.m_bFetchedUdtTypeName)
				{
					this.GetUdtTypeName();
					this.m_bFetchedUdtTypeName = true;
				}
				this.m_udtTypeNameKey = string.Concat(new string[]
				{
					"schemaName='",
					this.m_opoDscRefCtx.SchemaName,
					"' typeName='",
					this.m_opoDscRefCtx.TypeName,
					"'"
				});
				return this.m_opoDscRefCtx.SchemaName + "." + this.m_opoDscRefCtx.TypeName;
			}
		}

		// Token: 0x06000CB5 RID: 3253 RVA: 0x000847CC File Offset: 0x000837CC
		public unsafe DataTable GetMetaDataTable()
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleUdtDescriptor::GetMetaDataTable()\n"
				});
			}
			if (this.m_metaDataTable == null)
			{
				lock (this)
				{
					if (this.m_metaDataTable == null)
					{
						OciTypeCode ociTypeCode;
						if (this.m_pOpoDscValCtx->bDescribedUdt == 0)
						{
							ociTypeCode = this.GetUdtTypeCode();
						}
						else
						{
							ociTypeCode = (OciTypeCode)this.m_pOpoDscValCtx->TypeCode;
						}
						int num2;
						if (ociTypeCode == OciTypeCode.OBJECT || ociTypeCode == OciTypeCode.OPAQUE)
						{
							if (!this.m_bAllObjAttrMetaRefsMarshalled)
							{
								if (!this.m_bAllObjAttrsDescribed)
								{
									this.DescribeAllObjAttrs();
									this.m_bAllObjAttrsDescribed = true;
								}
								if (this.m_attrMetaRefs == null)
								{
									this.m_attrMetaRefs = new AttrMetaRef[this.m_pOpoDscValCtx->NumAttrs];
								}
								int num = 0;
								while ((long)num < (long)((ulong)this.m_pOpoDscValCtx->NumAttrs))
								{
									if (this.m_attrMetaRefs[num] == null)
									{
										this.MarshalAttrMetaRef(num);
									}
									num++;
								}
								this.m_bAllObjAttrMetaRefsMarshalled = true;
							}
							num2 = (int)this.m_pOpoDscValCtx->NumAttrs;
						}
						else
						{
							if (this.m_attrMetaRefs == null)
							{
								this.m_attrMetaRefs = new AttrMetaRef[1];
							}
							if (this.m_attrMetaRefs[0] == null)
							{
								if (this.m_pOpoDscValCtx->pAttrMetaVals == null || this.m_pOpoDscValCtx->pAttrMetaVals->bDescribed == 0)
								{
									this.DescribeArrElem();
									this.m_pOpoDscValCtx->pAttrMetaVals->bDescribed = 1;
								}
								this.MarshalAttrMetaRef(0);
							}
							num2 = 1;
						}
						this.m_metaDataTable = new DataTable("MetaDataTable");
						this.m_metaDataTable.Columns.Add("Name", typeof(string));
						this.m_metaDataTable.Columns.Add("UdtDescriptor", typeof(OracleUdtDescriptor));
						this.m_metaDataTable.Columns.Add("Size", typeof(int));
						this.m_metaDataTable.Columns.Add("NumericPrecision", typeof(short));
						this.m_metaDataTable.Columns.Add("NumericScale", typeof(short));
						this.m_metaDataTable.Columns.Add("ProviderType", typeof(OracleDbType));
						this.m_metaDataTable.MinimumCapacity = num2;
						for (int i = 0; i < num2; i++)
						{
							DataRow dataRow = this.m_metaDataTable.NewRow();
							if (ociTypeCode == OciTypeCode.OBJECT)
							{
								dataRow[0] = this.m_attrMetaRefs[i].AttrName;
							}
							OciTypeCode typeCode = (OciTypeCode)this.m_pOpoDscValCtx->pAttrMetaVals[i].TypeCode;
							OraType oraType = (OraType)this.m_pOpoDscValCtx->pAttrMetaVals[i].OraType;
							OracleUdtDescriptor oracleUdtDescriptor = null;
							if (oraType == OraType.ORA_NDT || oraType == OraType.ORA_OCIRef)
							{
								oracleUdtDescriptor = OracleUdtDescriptor.GetOracleUdtDescriptor(this.m_connection, this.m_attrMetaRefs[i].AttrSchemaName, this.m_attrMetaRefs[i].AttrTypeName);
								dataRow[1] = oracleUdtDescriptor;
							}
							if (typeCode == OciTypeCode.NAMEDCOLLECTION)
							{
								dataRow[2] = oracleUdtDescriptor.MaxSize;
							}
							else if (oraType == OraType.ORA_CHAR || oraType == OraType.ORA_CHARN || oraType == OraType.ORA_RAW)
							{
								dataRow[2] = this.m_pOpoDscValCtx->pAttrMetaVals[i].Size;
							}
							if (oraType == OraType.ORA_NUMBER)
							{
								dataRow[3] = this.m_pOpoDscValCtx->pAttrMetaVals[i].Precision;
								dataRow[4] = this.m_pOpoDscValCtx->pAttrMetaVals[i].Scale;
							}
							OraType oraType2 = oraType;
							OracleDbType oracleDbType;
							if (oraType2 != OraType.ORA_NUMBER)
							{
								switch (oraType2)
								{
								case OraType.ORA_NDT:
									if (oracleUdtDescriptor.m_bSetOracleDbType)
									{
										oracleDbType = oracleUdtDescriptor.m_oraDbType;
										goto IL_46E;
									}
									oracleDbType = oracleUdtDescriptor.OracleDbType;
									goto IL_46E;
								case OraType.ORA_OCIRef:
									oracleDbType = OracleDbType.Ref;
									goto IL_46E;
								}
								oracleDbType = (OracleDbType)OraDb_DbTypeTable.oraTypeToOracleDbTypeMapping[(int)this.m_pOpoDscValCtx->pAttrMetaVals[i].OraType];
								if (this.m_pOpoDscValCtx->pAttrMetaVals[i].CharsetForm == 2)
								{
									OracleDbType oracleDbType2 = oracleDbType;
									switch (oracleDbType2)
									{
									case OracleDbType.Char:
										oracleDbType = OracleDbType.NChar;
										break;
									case OracleDbType.Clob:
										oracleDbType = OracleDbType.NClob;
										break;
									default:
										if (oracleDbType2 == OracleDbType.Varchar2)
										{
											oracleDbType = OracleDbType.NVarchar2;
										}
										break;
									}
								}
							}
							else
							{
								oracleDbType = OraDb_DbTypeTable.ConvertNumberToOraDbType((int)this.m_pOpoDscValCtx->pAttrMetaVals[i].Precision, (int)this.m_pOpoDscValCtx->pAttrMetaVals[i].Scale);
							}
							IL_46E:
							dataRow[5] = oracleDbType;
							this.m_metaDataTable.Rows.Add(dataRow);
						}
						this.m_metaDataTable.AcceptChanges();
						if (ociTypeCode == OciTypeCode.OBJECT)
						{
							this.CalcIndOffsets();
						}
					}
				}
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleUdtDescriptor::GetMetaDataTable()\n"
				});
			}
			return this.m_metaDataTable;
		}

		// Token: 0x06000CB6 RID: 3254 RVA: 0x00084CDC File Offset: 0x00083CDC
		internal static OracleUdtDescriptor GetOracleUdtDescriptor(OracleConnection con, string schemaName, string typeName)
		{
			if (schemaName == null)
			{
				schemaName = string.Empty;
			}
			string fqName = string.Concat(new string[]
			{
				"schemaName='",
				schemaName,
				"' typeName='",
				typeName,
				"'"
			});
			return OracleUdtDescriptor.GetOracleUdtDescriptor(con, fqName, schemaName, typeName, false);
		}

		// Token: 0x06000CB7 RID: 3255 RVA: 0x00084D2C File Offset: 0x00083D2C
		internal unsafe static OracleUdtDescriptor GetOracleUdtDescriptor(OracleConnection con, IntPtr pTDO, bool bRefresh, out bool bExists)
		{
			if (con.m_contextConnection)
			{
				throw new NotSupportedException(OpoErrResManager.GetErrorMesg(ErrRes.CLR_UDT_NOTSUPPORTED_CTX_CONN, new string[0]));
			}
			if (pTDO == IntPtr.Zero)
			{
				throw new ArgumentException("pTDO");
			}
			OracleUdtDescriptor oracleUdtDescriptor;
			if (bRefresh || (oracleUdtDescriptor = (OracleUdtDescriptor)con.m_opoConCtx.m_udtDescPoolerByTDO.Get(pTDO)) == null || oracleUdtDescriptor.m_pOpoDscValCtx->bInvalidTDO == 1)
			{
				lock (OracleUdtDescriptor.s_lockObj)
				{
					if (bRefresh || (oracleUdtDescriptor = (OracleUdtDescriptor)con.m_opoConCtx.m_udtDescPoolerByTDO.Get(pTDO)) == null || oracleUdtDescriptor.m_pOpoDscValCtx->bInvalidTDO == 1)
					{
						oracleUdtDescriptor = new OracleUdtDescriptor(con, pTDO);
						if (con.m_opoConCtx.metaPool == 1)
						{
							con.m_opoConCtx.m_udtDescPoolerByTDO.Put(pTDO, oracleUdtDescriptor);
						}
						bExists = true;
						return oracleUdtDescriptor;
					}
				}
			}
			bExists = true;
			if (oracleUdtDescriptor.m_connection != con)
			{
				oracleUdtDescriptor.m_connection = con;
			}
			if (oracleUdtDescriptor.m_conSignature != con.m_conSignature)
			{
				oracleUdtDescriptor.m_conSignature = con.m_conSignature;
			}
			return oracleUdtDescriptor;
		}

		// Token: 0x06000CB8 RID: 3256 RVA: 0x00084E64 File Offset: 0x00083E64
		internal unsafe void DescribeCustomType(object customTypeFactory)
		{
			if (customTypeFactory == null)
			{
				throw new InvalidOperationException();
			}
			this.GetMetaDataTable();
			if (!(customTypeFactory is IOracleCustomTypeFactory) && !(customTypeFactory is IOracleArrayTypeFactory))
			{
				return;
			}
			if (this.m_customTypeFactory == null)
			{
				lock (this)
				{
					if (this.m_customTypeFactory == null)
					{
						if (this.OracleDbType == OracleDbType.Object)
						{
							this.m_UdtType = ((IOracleCustomTypeFactory)customTypeFactory).CreateObject().GetType();
							MemberInfo[] members = this.m_UdtType.GetMembers(BindingFlags.Instance | BindingFlags.Public);
							for (int i = 0; i < members.Length; i++)
							{
								if (members[i].MemberType == MemberTypes.Field || members[i].MemberType == MemberTypes.Property)
								{
									object[] customAttributes = members[i].GetCustomAttributes(typeof(OracleObjectMappingAttribute), true);
									if (customAttributes.Length > 0)
									{
										int num = ((OracleObjectMappingAttribute)customAttributes[0]).m_attrIndex;
										if (num == -1)
										{
											string attrName = ((OracleObjectMappingAttribute)customAttributes[0]).m_attrName;
											if (attrName == null || attrName == "")
											{
												throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_STR_INVALID_VALUE, new string[]
												{
													this.m_UdtType.FullName + "::" + members[i].Name + "::OracleObjectMappingAttribute",
													attrName
												}));
											}
											object obj = this.m_attrNameToIndex[attrName];
											if (obj == null)
											{
												throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_STR_INVALID_VALUE, new string[]
												{
													this.m_UdtType.FullName + "::" + members[i].Name + "::OracleObjectMappingAttribute",
													attrName
												}));
											}
											num = (int)obj;
										}
										else if (num < 0 || (long)num >= (long)((ulong)this.m_pOpoDscValCtx->NumAttrs))
										{
											throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_STR_INVALID_VALUE, new string[]
											{
												this.m_UdtType.FullName + "::" + members[i].Name + "::OracleObjectMappingAttribute",
												num.ToString()
											}));
										}
										AttrMetaVal* ptr = this.m_pOpoDscValCtx->pAttrMetaVals + num;
										if (ptr->OraType == 108 && ptr->TypeCode != 58)
										{
											OracleUdtDescriptor objAttrUdtDescriptor = this.GetObjAttrUdtDescriptor(num);
											objAttrUdtDescriptor.GetMetaDataTable();
											if (ptr->TypeCode != 108 && (objAttrUdtDescriptor.m_pOpoDscValCtx->pAttrMetaVals->OraType != 108 || objAttrUdtDescriptor.m_pOpoDscValCtx->pAttrMetaVals->TypeCode != 122) && objAttrUdtDescriptor.m_customTypeFactory == null)
											{
												object factory = OracleUdt.GetFactory(objAttrUdtDescriptor);
												objAttrUdtDescriptor.DescribeCustomType(factory);
											}
											ptr->CustTypeCode = CustomTypeCode.Udt;
											ptr->pOpoDscValCtx = objAttrUdtDescriptor.m_pOpoDscValCtx;
										}
										else
										{
											Type type;
											if (members[i].MemberType == MemberTypes.Field)
											{
												type = ((FieldInfo)members[i]).FieldType;
											}
											else
											{
												type = ((PropertyInfo)members[i]).PropertyType;
											}
											if (type.IsGenericType && type.FullName.StartsWith("System.Nullable"))
											{
												ptr->IsNullable = 1;
												Type[] genericArguments = type.GetGenericArguments();
												type = genericArguments[0];
											}
											if (OracleUdtDescriptor.s_mapTypeNameToCustTypeCode.ContainsKey(type.FullName))
											{
												ptr->CustTypeCode = (CustomTypeCode)OracleUdtDescriptor.s_mapTypeNameToCustTypeCode[type.FullName];
											}
										}
									}
								}
							}
						}
						else
						{
							if (!(customTypeFactory is IOracleCustomTypeFactory))
							{
								this.m_pOpoDscValCtx->bIsArrayType = 1;
							}
							AttrMetaVal* ptr2 = this.m_pOpoDscValCtx->pAttrMetaVals;
							if (ptr2->OraType == 108 && ptr2->TypeCode != 58)
							{
								OracleUdtDescriptor arrElemUdtDescriptor = this.GetArrElemUdtDescriptor();
								arrElemUdtDescriptor.GetMetaDataTable();
								if (ptr2->TypeCode != 108 && arrElemUdtDescriptor.m_pOpoDscValCtx->pAttrMetaVals->OraType != 108 && arrElemUdtDescriptor.m_customTypeFactory == null)
								{
									object factory2 = OracleUdt.GetFactory(arrElemUdtDescriptor);
									arrElemUdtDescriptor.DescribeCustomType(factory2);
								}
								ptr2->CustTypeCode = CustomTypeCode.Udt;
								ptr2->pOpoDscValCtx = arrElemUdtDescriptor.m_pOpoDscValCtx;
							}
							else
							{
								this.m_UdtType = ((IOracleArrayTypeFactory)customTypeFactory).CreateArray(0).GetType();
								Type type2 = this.m_UdtType.GetElementType();
								if (type2.IsGenericType && type2.FullName.StartsWith("System.Nullable"))
								{
									this.m_pOpoDscValCtx->pAttrMetaVals->IsNullable = 1;
									Type[] genericArguments2 = type2.GetGenericArguments();
									type2 = genericArguments2[0];
								}
								if (OracleUdtDescriptor.s_mapTypeNameToCustTypeCode.ContainsKey(type2.FullName))
								{
									this.m_pOpoDscValCtx->pAttrMetaVals->CustTypeCode = (CustomTypeCode)OracleUdtDescriptor.s_mapTypeNameToCustTypeCode[type2.FullName];
								}
							}
						}
						this.m_customTypeFactory = customTypeFactory;
					}
				}
			}
		}

		// Token: 0x06000CB9 RID: 3257 RVA: 0x0008532C File Offset: 0x0008432C
		internal unsafe void CalcIndOffsets()
		{
			int num = 2;
			int num2 = 0;
			while ((long)num2 < (long)((ulong)this.m_pOpoDscValCtx->NumAttrs))
			{
				this.m_pOpoDscValCtx->pAttrMetaVals[num2].IndOffset = num;
				if (this.m_pOpoDscValCtx->pAttrMetaVals[num2].TypeCode == 108)
				{
					OracleUdtDescriptor objAttrUdtDescriptor = this.GetObjAttrUdtDescriptor(num2);
					objAttrUdtDescriptor.GetMetaDataTable();
					if (objAttrUdtDescriptor.m_pOpoDscValCtx->bIsFinalType == 1)
					{
						num += objAttrUdtDescriptor.m_pOpoDscValCtx->IndSize;
					}
					else
					{
						num += 2;
					}
				}
				else
				{
					num += 2;
				}
				num2++;
			}
			this.m_pOpoDscValCtx->IndSize = num;
		}

		// Token: 0x06000CBA RID: 3258 RVA: 0x000853D4 File Offset: 0x000843D4
		internal OracleUdtDescriptor GetArrElemUdtDescriptor()
		{
			this.GetMetaDataTable();
			OracleUdtDescriptor oracleUdtDescriptor = this.m_metaDataTable.Rows[0]["UdtDescriptor"] as OracleUdtDescriptor;
			if (oracleUdtDescriptor.m_connection != this.m_connection)
			{
				oracleUdtDescriptor.m_connection = this.m_connection;
			}
			if (oracleUdtDescriptor.m_conSignature != this.m_connection.m_conSignature)
			{
				oracleUdtDescriptor.m_conSignature = this.m_connection.m_conSignature;
			}
			return oracleUdtDescriptor;
		}

		// Token: 0x06000CBB RID: 3259 RVA: 0x00085448 File Offset: 0x00084448
		internal OracleUdtDescriptor GetObjAttrUdtDescriptor(int attrIndex)
		{
			this.GetMetaDataTable();
			OracleUdtDescriptor oracleUdtDescriptor = this.m_metaDataTable.Rows[attrIndex]["UdtDescriptor"] as OracleUdtDescriptor;
			if (oracleUdtDescriptor.m_connection != this.m_connection)
			{
				oracleUdtDescriptor.m_connection = this.m_connection;
			}
			if (oracleUdtDescriptor.m_conSignature != this.m_connection.m_conSignature)
			{
				oracleUdtDescriptor.m_conSignature = this.m_connection.m_conSignature;
			}
			return oracleUdtDescriptor;
		}

		// Token: 0x06000CBC RID: 3260 RVA: 0x000854BC File Offset: 0x000844BC
		internal unsafe OciTypeCode GetUdtTypeCode()
		{
			int num = 0;
			if (this.m_pOpoDscValCtx->bDescribedUdt == 0)
			{
				if (this.m_connection.m_opoConCtx.opsConCtx == IntPtr.Zero)
				{
					throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_CLOSED, new string[0]));
				}
				if (this.m_connection.m_conSignature != this.m_conSignature)
				{
					throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_REOPENED, new string[0]));
				}
				try
				{
					num = OpsDsc.DescribeUdt(this.m_opsConCtx, this.m_opsErrCtx, this.m_opsDscCtx, this.m_pOpoDscValCtx);
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
						OracleException.HandleError(num, this.m_connection, this.m_opsErrCtx, this);
					}
				}
				this.m_pOpoDscValCtx->bDescribedUdt = 1;
			}
			return (OciTypeCode)this.m_pOpoDscValCtx->TypeCode;
		}

		// Token: 0x06000CBD RID: 3261 RVA: 0x000855B4 File Offset: 0x000845B4
		private OracleUdtDescriptor(OracleConnection con, string schemaName, string typeName)
		{
			int num = 0;
			this.m_connection = con;
			this.m_opoDscRefCtx = new OpoDscRefCtx();
			this.m_opoDscRefCtx.SchemaName = schemaName;
			this.m_opoDscRefCtx.TypeName = typeName;
			this.m_attrNameToIndex = new Hashtable();
			this.m_opsConCtx = this.m_connection.m_opoConCtx.opsConCtx;
			this.m_conSignature = this.m_connection.m_conSignature;
			try
			{
				int num2 = OpsCon.AddRef(this.m_opsConCtx);
				if (num2 <= 1)
				{
					throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_CLOSED, new string[0]));
				}
			}
			catch (Exception ex)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex);
				}
				throw;
			}
			try
			{
				this.m_pOpoDscValCtx = null;
				num = OpsDsc.GetTDO(this.m_opsConCtx, out this.m_opsErrCtx, ref this.m_opsDscCtx, out this.m_pOpoDscValCtx, this.m_opoDscRefCtx);
			}
			catch (Exception ex2)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex2);
				}
				GC.SuppressFinalize(this);
				throw;
			}
			finally
			{
				if (num != 0)
				{
					try
					{
						OpsCon.RelRef(ref this.m_opsConCtx);
					}
					catch (Exception ex3)
					{
						if (OraTrace.m_TraceLevel != 0U)
						{
							OraTrace.TraceExceptionInfo(ex3);
						}
					}
					try
					{
						GC.SuppressFinalize(this);
						OracleException.HandleError(num, this.m_connection, this.m_opsErrCtx, this);
					}
					finally
					{
						if (this.m_opsErrCtx != IntPtr.Zero)
						{
							try
							{
								OpsErr.FreeCtx(ref this.m_opsErrCtx);
							}
							catch (Exception ex4)
							{
								if (OraTrace.m_TraceLevel != 0U)
								{
									OraTrace.TraceExceptionInfo(ex4);
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06000CBE RID: 3262 RVA: 0x00085764 File Offset: 0x00084764
		private OracleUdtDescriptor(OracleConnection con, IntPtr pTDO)
		{
			int num = 0;
			this.m_connection = con;
			this.m_opsDscCtx = pTDO;
			this.m_attrNameToIndex = new Hashtable();
			this.m_opsConCtx = this.m_connection.m_opoConCtx.opsConCtx;
			this.m_conSignature = this.m_connection.m_conSignature;
			try
			{
				int num2 = OpsCon.AddRef(this.m_opsConCtx);
				if (num2 <= 1)
				{
					throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_CLOSED, new string[0]));
				}
			}
			catch (Exception ex)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex);
				}
				GC.SuppressFinalize(this);
				throw;
			}
			try
			{
				this.m_pOpoDscValCtx = null;
				num = OpsDsc.GetTDO(this.m_opsConCtx, out this.m_opsErrCtx, ref this.m_opsDscCtx, out this.m_pOpoDscValCtx, this.m_opoDscRefCtx);
			}
			catch (Exception ex2)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex2);
				}
				GC.SuppressFinalize(this);
				throw;
			}
			finally
			{
				if (num != 0)
				{
					try
					{
						OpsCon.RelRef(ref this.m_opsConCtx);
					}
					catch (Exception ex3)
					{
						if (OraTrace.m_TraceLevel != 0U)
						{
							OraTrace.TraceExceptionInfo(ex3);
						}
					}
					try
					{
						GC.SuppressFinalize(this);
						OracleException.HandleError(num, this.m_connection, this.m_opsErrCtx, this);
					}
					finally
					{
						if (this.m_opsErrCtx != IntPtr.Zero)
						{
							try
							{
								OpsErr.FreeCtx(ref this.m_opsErrCtx);
							}
							catch (Exception ex4)
							{
								if (OraTrace.m_TraceLevel != 0U)
								{
									OraTrace.TraceExceptionInfo(ex4);
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06000CBF RID: 3263 RVA: 0x00085900 File Offset: 0x00084900
		private unsafe static OracleUdtDescriptor GetOracleUdtDescriptor(OracleConnection con, string fqName, string schemaName, string typeName, bool bRefresh)
		{
			if (con.m_opoConCtx.opsConCtx == IntPtr.Zero)
			{
				throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_CLOSED, new string[0]));
			}
			if (con.m_contextConnection)
			{
				throw new NotSupportedException(OpoErrResManager.GetErrorMesg(ErrRes.CLR_UDT_NOTSUPPORTED_CTX_CONN, new string[0]));
			}
			OracleUdtDescriptor oracleUdtDescriptor;
			if (con.m_opoConCtx.metaPool == 1)
			{
				if (!bRefresh && (oracleUdtDescriptor = (OracleUdtDescriptor)con.m_opoConCtx.m_udtDescPoolerByName.Get(fqName)) != null && oracleUdtDescriptor.m_pOpoDscValCtx->bInvalidTDO != 1)
				{
					goto IL_12E;
				}
				lock (OracleUdtDescriptor.s_lockObj)
				{
					if (bRefresh || (oracleUdtDescriptor = (OracleUdtDescriptor)con.m_opoConCtx.m_udtDescPoolerByName.Get(fqName)) == null || oracleUdtDescriptor.m_pOpoDscValCtx->bInvalidTDO == 1)
					{
						oracleUdtDescriptor = new OracleUdtDescriptor(con, schemaName, typeName);
						con.m_opoConCtx.m_udtDescPoolerByName.Put(fqName, oracleUdtDescriptor);
						if (oracleUdtDescriptor.m_udtTypeNameKey == null)
						{
							string udtTypeName = oracleUdtDescriptor.UdtTypeName;
						}
						string udtTypeNameKey = oracleUdtDescriptor.m_udtTypeNameKey;
						if (udtTypeNameKey != fqName)
						{
							con.m_opoConCtx.m_udtDescPoolerByName.Put(udtTypeNameKey, oracleUdtDescriptor);
						}
						return oracleUdtDescriptor;
					}
					goto IL_12E;
				}
			}
			oracleUdtDescriptor = new OracleUdtDescriptor(con, schemaName, typeName);
			IL_12E:
			if (oracleUdtDescriptor.m_connection != con)
			{
				oracleUdtDescriptor.m_connection = con;
			}
			if (oracleUdtDescriptor.m_conSignature != con.m_conSignature)
			{
				oracleUdtDescriptor.m_conSignature = con.m_conSignature;
			}
			return oracleUdtDescriptor;
		}

		// Token: 0x06000CC0 RID: 3264 RVA: 0x00085A78 File Offset: 0x00084A78
		private void DescribeAllObjAttrs()
		{
			int num = 0;
			if (this.m_connection.m_opoConCtx.opsConCtx == IntPtr.Zero)
			{
				throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_CLOSED, new string[0]));
			}
			if (this.m_connection.m_conSignature != this.m_conSignature)
			{
				throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_REOPENED, new string[0]));
			}
			try
			{
				num = OpsDsc.DescribeAllObjAttrs(this.m_opsConCtx, this.m_opsErrCtx, this.m_opsDscCtx, this.m_pOpoDscValCtx);
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
					OracleException.HandleError(num, this.m_connection, this.m_opsErrCtx, this);
				}
			}
		}

		// Token: 0x06000CC1 RID: 3265 RVA: 0x00085B48 File Offset: 0x00084B48
		private void DescribeArrElem()
		{
			int num = 0;
			if (this.m_connection.m_opoConCtx.opsConCtx == IntPtr.Zero)
			{
				throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_CLOSED, new string[0]));
			}
			if (this.m_connection.m_conSignature != this.m_conSignature)
			{
				throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_REOPENED, new string[0]));
			}
			try
			{
				num = OpsDsc.DescribeArrElem(this.m_opsConCtx, this.m_opsErrCtx, this.m_opsDscCtx, this.m_pOpoDscValCtx);
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
					OracleException.HandleError(num, this.m_connection, this.m_opsErrCtx, this);
				}
			}
		}

		// Token: 0x06000CC2 RID: 3266 RVA: 0x00085C18 File Offset: 0x00084C18
		private void DescribeObjAttr(int attrIndex)
		{
			int num = 0;
			if (this.m_connection.m_opoConCtx.opsConCtx == IntPtr.Zero)
			{
				throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_CLOSED, new string[0]));
			}
			if (this.m_connection.m_conSignature != this.m_conSignature)
			{
				throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_REOPENED, new string[0]));
			}
			try
			{
				num = OpsDsc.DescribeObjAttr(this.m_opsConCtx, this.m_opsErrCtx, this.m_opsDscCtx, this.m_pOpoDscValCtx, attrIndex);
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
					OracleException.HandleError(num, this.m_connection, this.m_opsErrCtx, this);
				}
			}
		}

		// Token: 0x06000CC3 RID: 3267 RVA: 0x00085CE8 File Offset: 0x00084CE8
		private void GetArrTypeCode()
		{
			int num = 0;
			if (this.m_connection.m_opoConCtx.opsConCtx == IntPtr.Zero)
			{
				throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_CLOSED, new string[0]));
			}
			if (this.m_connection.m_conSignature != this.m_conSignature)
			{
				throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_REOPENED, new string[0]));
			}
			try
			{
				num = OpsDsc.GetArrTypeCode(this.m_opsConCtx, this.m_opsErrCtx, this.m_opsDscCtx, this.m_pOpoDscValCtx);
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
					OracleException.HandleError(num, this.m_connection, this.m_opsErrCtx, this);
				}
			}
		}

		// Token: 0x06000CC4 RID: 3268 RVA: 0x00085DB8 File Offset: 0x00084DB8
		private void GetUdtTypeName()
		{
			int num = 0;
			if (this.m_connection.m_opoConCtx.opsConCtx == IntPtr.Zero)
			{
				throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_CLOSED, new string[0]));
			}
			if (this.m_connection.m_conSignature != this.m_conSignature)
			{
				throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_REOPENED, new string[0]));
			}
			if (this.m_opoDscRefCtx == null)
			{
				this.m_opoDscRefCtx = new OpoDscRefCtx();
			}
			try
			{
				num = OpsDsc.GetUdtTypeName(this.m_opsConCtx, this.m_opsErrCtx, this.m_opsDscCtx, this.m_pOpoDscValCtx, ref this.m_opoDscRefCtx);
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
					OracleException.HandleError(num, this.m_connection, this.m_opsErrCtx, this);
				}
			}
		}

		// Token: 0x06000CC5 RID: 3269 RVA: 0x00085EA0 File Offset: 0x00084EA0
		private unsafe void MarshalAttrMetaRef(int attrIndex)
		{
			this.m_attrMetaRefs[attrIndex] = new AttrMetaRef();
			try
			{
				IntPtr ptr = IntPtr.Zero;
				ptr = (IntPtr)((void*)((byte*)((void*)this.m_pOpoDscValCtx->pAttrMetaRefs) + (IntPtr)OracleUdtDescriptor.s_attrMetaRefMarshalSize * (IntPtr)attrIndex));
				Marshal.PtrToStructure(ptr, this.m_attrMetaRefs[attrIndex]);
				if (this.m_attrMetaRefs[attrIndex].AttrName != null)
				{
					this.m_attrNameToIndex[this.m_attrMetaRefs[attrIndex].AttrName] = attrIndex;
				}
			}
			catch
			{
				this.m_attrMetaRefs[attrIndex] = null;
				throw;
			}
		}

		// Token: 0x06000CC6 RID: 3270 RVA: 0x00085F38 File Offset: 0x00084F38
		protected override void Finalize()
		{
			try
			{
				try
				{
					OpsDsc.Dispose(this.m_opsConCtx, this.m_opsErrCtx, this.m_opsDscCtx, this.m_pOpoDscValCtx);
				}
				catch (Exception ex)
				{
					if (OraTrace.m_TraceLevel != 0U)
					{
						OraTrace.TraceExceptionInfo(ex);
					}
				}
				this.m_connection = null;
				this.m_opoDscRefCtx = null;
				this.m_metaDataTable = null;
				this.m_attrMetaRefs = null;
				this.m_attrNameToIndex = null;
			}
			finally
			{
				base.Finalize();
			}
		}

		// Token: 0x06000CC7 RID: 3271 RVA: 0x00085FC0 File Offset: 0x00084FC0
		static OracleUdtDescriptor()
		{
			if (!OracleInit.bSetDllDirectoryInvoked)
			{
				OracleInit.Initialize();
			}
			OracleUdtDescriptor.s_attrMetaRefMarshalSize = Marshal.SizeOf(typeof(AttrMetaRef));
			OracleUdtDescriptor.s_mapTypeNameToCustTypeCode = new Hashtable();
			OracleUdtDescriptor.s_mapTypeNameToCustTypeCode["System.Byte"] = CustomTypeCode.Byte;
			OracleUdtDescriptor.s_mapTypeNameToCustTypeCode["System.Byte[]"] = CustomTypeCode.Bytes;
			OracleUdtDescriptor.s_mapTypeNameToCustTypeCode["System.Char[]"] = CustomTypeCode.Chars;
			OracleUdtDescriptor.s_mapTypeNameToCustTypeCode["System.DateTime"] = CustomTypeCode.DateTime;
			OracleUdtDescriptor.s_mapTypeNameToCustTypeCode["System.Decimal"] = CustomTypeCode.Decimal;
			OracleUdtDescriptor.s_mapTypeNameToCustTypeCode["System.Double"] = CustomTypeCode.Double;
			OracleUdtDescriptor.s_mapTypeNameToCustTypeCode["System.Int16"] = CustomTypeCode.Int16;
			OracleUdtDescriptor.s_mapTypeNameToCustTypeCode["System.Int32"] = CustomTypeCode.Int32;
			OracleUdtDescriptor.s_mapTypeNameToCustTypeCode["System.Int64"] = CustomTypeCode.Int64;
			OracleUdtDescriptor.s_mapTypeNameToCustTypeCode["System.Single"] = CustomTypeCode.Single;
			OracleUdtDescriptor.s_mapTypeNameToCustTypeCode["System.String"] = CustomTypeCode.String;
			OracleUdtDescriptor.s_mapTypeNameToCustTypeCode["System.TimeSpan"] = CustomTypeCode.TimeSpan;
			OracleUdtDescriptor.s_mapTypeNameToCustTypeCode["Oracle.DataAccess.Types.OracleBFile"] = CustomTypeCode.OracleBFile;
			OracleUdtDescriptor.s_mapTypeNameToCustTypeCode["Oracle.DataAccess.Types.OracleBinary"] = CustomTypeCode.OracleBinary;
			OracleUdtDescriptor.s_mapTypeNameToCustTypeCode["Oracle.DataAccess.Types.OracleBlob"] = CustomTypeCode.OracleBlob;
			OracleUdtDescriptor.s_mapTypeNameToCustTypeCode["Oracle.DataAccess.Types.OracleClob"] = CustomTypeCode.OracleClob;
			OracleUdtDescriptor.s_mapTypeNameToCustTypeCode["Oracle.DataAccess.Types.OracleDate"] = CustomTypeCode.OracleDate;
			OracleUdtDescriptor.s_mapTypeNameToCustTypeCode["Oracle.DataAccess.Types.OracleDecimal"] = CustomTypeCode.OracleDecimal;
			OracleUdtDescriptor.s_mapTypeNameToCustTypeCode["Oracle.DataAccess.Types.OracleIntervalDS"] = CustomTypeCode.OracleIntervalDS;
			OracleUdtDescriptor.s_mapTypeNameToCustTypeCode["Oracle.DataAccess.Types.OracleIntervalYM"] = CustomTypeCode.OracleIntervalYM;
			OracleUdtDescriptor.s_mapTypeNameToCustTypeCode["Oracle.DataAccess.Types.OracleRef"] = CustomTypeCode.OracleRef;
			OracleUdtDescriptor.s_mapTypeNameToCustTypeCode["Oracle.DataAccess.Types.OracleString"] = CustomTypeCode.OracleString;
			OracleUdtDescriptor.s_mapTypeNameToCustTypeCode["Oracle.DataAccess.Types.OracleTimeStamp"] = CustomTypeCode.OracleTimeStamp;
			OracleUdtDescriptor.s_mapTypeNameToCustTypeCode["Oracle.DataAccess.Types.OracleTimeStampLTZ"] = CustomTypeCode.OracleTimeStampLTZ;
			OracleUdtDescriptor.s_mapTypeNameToCustTypeCode["Oracle.DataAccess.Types.OracleTimeStampTZ"] = CustomTypeCode.OracleTimeStampTZ;
			OracleUdtDescriptor.s_mapTypeNameToCustTypeCode["Oracle.DataAccess.Types.OracleXmlType"] = CustomTypeCode.OracleXmlType;
			OracleUdtDescriptor.s_mapCustTypeCodeToTypeName = new Hashtable();
			OracleUdtDescriptor.s_mapCustTypeCodeToTypeName[CustomTypeCode.Byte] = "System.Byte";
			OracleUdtDescriptor.s_mapCustTypeCodeToTypeName[CustomTypeCode.Bytes] = "System.Byte[]";
			OracleUdtDescriptor.s_mapCustTypeCodeToTypeName[CustomTypeCode.Chars] = "System.Char[]";
			OracleUdtDescriptor.s_mapCustTypeCodeToTypeName[CustomTypeCode.DateTime] = "System.DateTime";
			OracleUdtDescriptor.s_mapCustTypeCodeToTypeName[CustomTypeCode.Decimal] = "System.Decimal";
			OracleUdtDescriptor.s_mapCustTypeCodeToTypeName[CustomTypeCode.Double] = "System.Double";
			OracleUdtDescriptor.s_mapCustTypeCodeToTypeName[CustomTypeCode.Int16] = "System.Int16";
			OracleUdtDescriptor.s_mapCustTypeCodeToTypeName[CustomTypeCode.Int32] = "System.Int32";
			OracleUdtDescriptor.s_mapCustTypeCodeToTypeName[CustomTypeCode.Int64] = "System.Int64";
			OracleUdtDescriptor.s_mapCustTypeCodeToTypeName[CustomTypeCode.Single] = "System.Single";
			OracleUdtDescriptor.s_mapCustTypeCodeToTypeName[CustomTypeCode.String] = "System.String";
			OracleUdtDescriptor.s_mapCustTypeCodeToTypeName[CustomTypeCode.TimeSpan] = "System.TimeSpan";
			OracleUdtDescriptor.s_mapCustTypeCodeToTypeName[CustomTypeCode.OracleBFile] = "Oracle.DataAccess.Types.OracleBFile";
			OracleUdtDescriptor.s_mapCustTypeCodeToTypeName[CustomTypeCode.OracleBinary] = "Oracle.DataAccess.Types.OracleBinary";
			OracleUdtDescriptor.s_mapCustTypeCodeToTypeName[CustomTypeCode.OracleBlob] = "Oracle.DataAccess.Types.OracleBlob";
			OracleUdtDescriptor.s_mapCustTypeCodeToTypeName[CustomTypeCode.OracleClob] = "Oracle.DataAccess.Types.OracleClob";
			OracleUdtDescriptor.s_mapCustTypeCodeToTypeName[CustomTypeCode.OracleDate] = "Oracle.DataAccess.Types.OracleDate";
			OracleUdtDescriptor.s_mapCustTypeCodeToTypeName[CustomTypeCode.OracleDecimal] = "Oracle.DataAccess.Types.OracleDecimal";
			OracleUdtDescriptor.s_mapCustTypeCodeToTypeName[CustomTypeCode.OracleIntervalDS] = "Oracle.DataAccess.Types.OracleIntervalDS";
			OracleUdtDescriptor.s_mapCustTypeCodeToTypeName[CustomTypeCode.OracleIntervalYM] = "Oracle.DataAccess.Types.OracleIntervalYM";
			OracleUdtDescriptor.s_mapCustTypeCodeToTypeName[CustomTypeCode.OracleRef] = "Oracle.DataAccess.Types.OracleRef";
			OracleUdtDescriptor.s_mapCustTypeCodeToTypeName[CustomTypeCode.OracleString] = "Oracle.DataAccess.Types.OracleString";
			OracleUdtDescriptor.s_mapCustTypeCodeToTypeName[CustomTypeCode.OracleTimeStamp] = "Oracle.DataAccess.Types.OracleTimeStamp";
			OracleUdtDescriptor.s_mapCustTypeCodeToTypeName[CustomTypeCode.OracleTimeStampLTZ] = "Oracle.DataAccess.Types.OracleTimeStampLTZ";
			OracleUdtDescriptor.s_mapCustTypeCodeToTypeName[CustomTypeCode.OracleTimeStampTZ] = "Oracle.DataAccess.Types.OracleTimeStampTZ";
			OracleUdtDescriptor.s_mapCustTypeCodeToTypeName[CustomTypeCode.OracleXmlType] = "Oracle.DataAccess.Types.OracleXmlType";
		}

		// Token: 0x040009FD RID: 2557
		private static readonly string s_xmlTypeName = "SYS.XMLTYPE";

		// Token: 0x040009FE RID: 2558
		private static int s_attrMetaRefMarshalSize;

		// Token: 0x040009FF RID: 2559
		private static Hashtable s_mapTypeNameToCustTypeCode;

		// Token: 0x04000A00 RID: 2560
		internal static Hashtable s_mapCustTypeCodeToTypeName;

		// Token: 0x04000A01 RID: 2561
		private static object s_lockObj = new object();

		// Token: 0x04000A02 RID: 2562
		private IntPtr m_opsConCtx;

		// Token: 0x04000A03 RID: 2563
		private IntPtr m_opsErrCtx;

		// Token: 0x04000A04 RID: 2564
		internal OpoDscRefCtx m_opoDscRefCtx;

		// Token: 0x04000A05 RID: 2565
		private DataTable m_metaDataTable;

		// Token: 0x04000A06 RID: 2566
		private bool m_bAllObjAttrsDescribed;

		// Token: 0x04000A07 RID: 2567
		private bool m_bAllObjAttrMetaRefsMarshalled;

		// Token: 0x04000A08 RID: 2568
		private bool m_bFetchedNumArrElems;

		// Token: 0x04000A09 RID: 2569
		private bool m_bFetchedUdtTypeName;

		// Token: 0x04000A0A RID: 2570
		private AttrMetaRef[] m_attrMetaRefs;

		// Token: 0x04000A0B RID: 2571
		internal Hashtable m_attrNameToIndex;

		// Token: 0x04000A0C RID: 2572
		private int m_conSignature;

		// Token: 0x04000A0D RID: 2573
		internal IntPtr m_opsDscCtx;

		// Token: 0x04000A0E RID: 2574
		internal OracleConnection m_connection;

		// Token: 0x04000A0F RID: 2575
		internal OracleDbType m_oraDbType;

		// Token: 0x04000A10 RID: 2576
		internal bool m_bSetOracleDbType;

		// Token: 0x04000A11 RID: 2577
		internal unsafe OpoDscValCtx* m_pOpoDscValCtx;

		// Token: 0x04000A12 RID: 2578
		internal object m_customTypeFactory;

		// Token: 0x04000A13 RID: 2579
		internal Type m_UdtType;

		// Token: 0x04000A14 RID: 2580
		internal string m_udtTypeNameKey;
	}
}
