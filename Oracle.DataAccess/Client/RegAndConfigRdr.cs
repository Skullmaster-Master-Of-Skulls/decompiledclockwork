using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Xml;
using Microsoft.Win32;

namespace Oracle.DataAccess.Client
{
	// Token: 0x0200009B RID: 155
	internal class RegAndConfigRdr
	{
		// Token: 0x06000777 RID: 1911 RVA: 0x00048C64 File Offset: 0x00047C64
		[RegistryPermission(SecurityAction.Assert, Unrestricted = true)]
		[ConfigurationPermission(SecurityAction.Assert, Unrestricted = true)]
		static RegAndConfigRdr()
		{
			RegAndConfigRdr.m_configSection = (ConfigurationManager.GetSection("oracle.dataaccess.client") as NameValueCollection);
			RegistryKey localMachine = Registry.LocalMachine;
			RegistryKey registryKey = localMachine.OpenSubKey("SOFTWARE\\Oracle\\ODP.NET");
			if (registryKey == null)
			{
				return;
			}
			string[] subKeyNames = registryKey.GetSubKeyNames();
			string assemblyVersion = OracleInit.GetAssemblyVersion();
			for (int i = 0; i < subKeyNames.Length; i++)
			{
				if (assemblyVersion == subKeyNames[i])
				{
					RegAndConfigRdr.odpNetKey = registryKey.OpenSubKey(assemblyVersion);
				}
			}
			RegAndConfigRdr.RetrieveInfoFromConfig(RegAndConfigRdr.m_configSection, ref RegAndConfigRdr.s_storedProcInformation, false);
			RegAndConfigRdr.ValidateEdmMapping();
		}

		// Token: 0x06000778 RID: 1912 RVA: 0x00048E30 File Offset: 0x00047E30
		private static void ValidateEdmMapping()
		{
			int num = -1;
			for (int i = 0; i < RegAndConfigRdr.s_edmTypes.Length; i++)
			{
				int num2 = RegAndConfigRdr.GetMaxPrecision(RegAndConfigRdr.s_edmTypes[i]);
				if (num2 == -1)
				{
					num2 = RegAndConfigRdr.s_maxPrecision[i];
				}
				if (num2 > 0)
				{
					if (num > num2)
					{
						if (OraTrace.m_TraceLevel != 0U)
						{
							OraTrace.Trace(1U, new string[]
							{
								" (ERROR) (EDMMAPPING) " + RegAndConfigRdr.s_edmTypes[i] + " is invalid\n"
							});
							OraTrace.Trace(1U, new string[]
							{
								" (ERROR) (EDMMAPPING) " + RegAndConfigRdr.s_edmTypes[i] + " has a max precision that is lower than required\n"
							});
						}
						throw new ConfigurationErrorsException(OpoErrResManager.GetErrorMesg(ErrRes.ODP_INVALID_VALUE, new string[]
						{
							RegAndConfigRdr.s_edmTypes[i]
						}));
					}
					num = num2;
				}
			}
		}

		// Token: 0x06000779 RID: 1913 RVA: 0x00048EFC File Offset: 0x00047EFC
		internal static int GetMaxPrecision(string edmType)
		{
			object obj = RegAndConfigRdr.s_edmMapping[edmType.Trim().ToUpperInvariant()];
			if (obj == null)
			{
				return -1;
			}
			return (int)obj;
		}

		// Token: 0x0600077A RID: 1914 RVA: 0x00048F2C File Offset: 0x00047F2C
		private static void RetrieveEdmMappingInfoFromConfig(string key, string value)
		{
			int num = -1;
			string text = key.Trim().ToUpperInvariant();
			string text2 = value.Trim().ToUpperInvariant();
			bool flag = false;
			int num2 = -1;
			if (RegAndConfigRdr.s_edmTypes.Contains(text))
			{
				num2 = text2.IndexOf("NUMBER");
				if (num2 != -1)
				{
					int num3 = text2.IndexOf('(', num2);
					if (num3 != -1)
					{
						int num4 = text2.IndexOf(',', num3);
						if (num4 == -1)
						{
							num4 = text2.IndexOf(')', num3);
						}
						if (num4 != -1)
						{
							flag = int.TryParse(text2.Substring(num3 + 1, num4 - (num3 + 1)).Trim(), out num);
							if (num <= 0)
							{
								flag = false;
							}
						}
					}
				}
			}
			if (flag)
			{
				RegAndConfigRdr.s_edmMapping[text] = num;
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						string.Concat(new object[]
						{
							" (EDMMAPPING) Adding [",
							text,
							",",
							num,
							"]\n"
						})
					});
				}
				return;
			}
			Console.WriteLine("Error : [{0},{1}]", key, num);
			if (num2 == -1)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (ERROR) (EDMMAPPING) " + key + " is invalid\n"
					});
				}
				throw new ConfigurationErrorsException(OpoErrResManager.GetErrorMesg(ErrRes.ODP_INVALID_VALUE, new string[]
				{
					key
				}));
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ERROR) (EDMMAPPING) " + value + " is invalid\n"
				});
			}
			throw new ConfigurationErrorsException(OpoErrResManager.GetErrorMesg(ErrRes.ODP_INVALID_VALUE, new string[]
			{
				value
			}));
		}

		// Token: 0x0600077B RID: 1915 RVA: 0x000490EC File Offset: 0x000480EC
		private static void RetrieveInfoFromConfig(NameValueCollection nvc, ref Hashtable schemaTable, bool bIsCallFromODT)
		{
			if (nvc != null)
			{
				foreach (object obj in nvc)
				{
					string text = nvc[(string)obj].Trim();
					if (text.ToUpperInvariant().StartsWith("IMPLICITREFCURSOR"))
					{
						string text2 = text.Split(new char[]
						{
							' '
						})[1].Trim();
						int startIndex = text.IndexOf(text2);
						string text3 = text.Substring(startIndex);
						IEnumerator enumerator;
						string refCursorKey = (string)enumerator.Current;
						if (text2.ToLower().StartsWith("bindinfo"))
						{
							RegAndConfigRdr.AddBindInfoForRefCursor(refCursorKey, text3, ref schemaTable);
						}
						else
						{
							if (!text2.ToLower().StartsWith("metadata"))
							{
								if (OraTrace.m_TraceLevel != 0U)
								{
									OraTrace.Trace(1U, new string[]
									{
										" (ERROR) (REFCURSOR) " + text + " is invalid\n"
									});
								}
								throw new ConfigurationErrorsException(OpoErrResManager.GetErrorMesg(ErrRes.ODP_INVALID_VALUE, new string[]
								{
									text
								}));
							}
							RegAndConfigRdr.AddMetadataForRefCursor(refCursorKey, text3, ref schemaTable);
						}
					}
					else if (text.ToUpperInvariant().StartsWith("EDMMAPPING"))
					{
						IEnumerator enumerator;
						string text4 = enumerator.Current.ToString();
						RegAndConfigRdr.RetrieveEdmMappingInfoFromConfig(text4, nvc[text4]);
					}
				}
			}
		}

		// Token: 0x0600077C RID: 1916 RVA: 0x00049238 File Offset: 0x00048238
		private static void AddBindInfoForRefCursor(string refCursorKey, string infoForBind, ref Hashtable schematable)
		{
			bool flag = false;
			int num = infoForBind.IndexOf("'");
			int num2 = infoForBind.LastIndexOf("'");
			if (num == -1 || num2 == -1)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (ERROR) (REFCURSOR) " + refCursorKey + " : BindInfo is invalid\n"
					});
				}
				throw new ConfigurationErrorsException(OpoErrResManager.GetErrorMesg(ErrRes.ODP_INVALID_VALUE, new string[]
				{
					refCursorKey + " BindInfo"
				}));
			}
			string text = infoForBind.Substring(num + 1, num2 - (num + 1)).Trim();
			RefCursorInfo refCursorInfo = new RefCursorInfo();
			string text2 = refCursorKey.Substring(refCursorKey.LastIndexOf(".") + 1).Trim();
			if (!int.TryParse(text2, out refCursorInfo.position))
			{
				refCursorInfo.name = text2;
				refCursorInfo.position = -1;
				flag = true;
			}
			string[] array = text.Split(new char[]
			{
				'='
			});
			if (array.Length != 2)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (ERROR) (REFCURSOR) " + refCursorKey + " : BindInfo is invalid\n"
					});
				}
				throw new ConfigurationErrorsException(OpoErrResManager.GetErrorMesg(ErrRes.ODP_INVALID_VALUE, new string[]
				{
					refCursorKey + " BindInfo"
				}));
			}
			string a;
			if ((a = array[0].Trim().ToLower()) == null || !(a == "mode"))
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (ERROR) (REFCURSOR) " + refCursorKey + " : BindInfo is invalid\n"
					});
				}
				throw new ConfigurationErrorsException(OpoErrResManager.GetErrorMesg(ErrRes.ODP_INVALID_VALUE, new string[]
				{
					refCursorKey + " BindInfo"
				}));
			}
			string value = array[1].Trim();
			if (!Enum.IsDefined(typeof(ParameterDirection), value))
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (ERROR) (REFCURSOR) " + refCursorKey + " : Mode is invalid\n"
					});
				}
				throw new ConfigurationErrorsException(OpoErrResManager.GetErrorMesg(ErrRes.ODP_INVALID_VALUE, new string[]
				{
					refCursorKey + " Mode"
				}));
			}
			refCursorInfo.mode = (ParameterDirection)Enum.Parse(typeof(ParameterDirection), value, true);
			if (refCursorInfo.mode == ParameterDirection.Input)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (ERROR) (REFCURSOR) " + refCursorKey + " : ParameterDirection is invalid\n"
					});
				}
				throw new ConfigurationErrorsException(OpoErrResManager.GetErrorMesg(ErrRes.ODP_INVALID_VALUE, new string[]
				{
					"ParameterDirection"
				}));
			}
			string[] array2 = refCursorKey.Split(new char[]
			{
				'.'
			});
			if (array2[array2.Length - 2].Trim() != "RefCursor")
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (ERROR) (REFCURSOR) " + refCursorKey + " : BindInfo is invalid\n"
					});
				}
				throw new ConfigurationErrorsException(OpoErrResManager.GetErrorMesg(ErrRes.ODP_INVALID_VALUE, new string[]
				{
					refCursorKey + " BindInfo"
				}));
			}
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < array2.Length - 2; i++)
			{
				stringBuilder.Append(array2[i] + ".");
			}
			stringBuilder.Remove(stringBuilder.Length - 1, 1);
			string key = stringBuilder.ToString().Trim();
			RegAndConfigRdr.GetKeyInProperCase(ref key);
			if (!schematable.Contains(key))
			{
				StoredProcedureInfo storedProcedureInfo = new StoredProcedureInfo();
				storedProcedureInfo.refCursors.Add(refCursorInfo);
				schematable.Add(key, storedProcedureInfo);
				return;
			}
			StoredProcedureInfo storedProcedureInfo2 = (StoredProcedureInfo)schematable[key];
			int num3 = 0;
			foreach (object obj in storedProcedureInfo2.refCursors)
			{
				RefCursorInfo refCursorInfo2 = (RefCursorInfo)obj;
				if (flag)
				{
					if (refCursorInfo2.name.Length > 0 && refCursorInfo2.name.Equals(refCursorInfo.name))
					{
						storedProcedureInfo2.refCursors.RemoveAt(num3);
						break;
					}
				}
				else if (refCursorInfo2.position >= refCursorInfo.position)
				{
					if (refCursorInfo2.position == refCursorInfo.position)
					{
						storedProcedureInfo2.refCursors.RemoveAt(num3);
					}
					storedProcedureInfo2.refCursors.Insert(num3, refCursorInfo);
					return;
				}
				num3++;
			}
			storedProcedureInfo2.refCursors.Add(refCursorInfo);
		}

		// Token: 0x0600077D RID: 1917 RVA: 0x000496F8 File Offset: 0x000486F8
		private static string GetAttrValueInProperCase(string attributeValue)
		{
			int length = attributeValue.Length;
			if (length > 0)
			{
				if (attributeValue[0] == '"' && attributeValue[length - 1] == '"')
				{
					attributeValue = attributeValue.Trim(new char[]
					{
						'"'
					});
				}
				else
				{
					attributeValue = attributeValue.ToUpperInvariant();
				}
			}
			return attributeValue;
		}

		// Token: 0x0600077E RID: 1918 RVA: 0x00049748 File Offset: 0x00048748
		private static void GetKeyInProperCase(ref string storedProcKey)
		{
			string[] array = storedProcKey.Split(new char[]
			{
				'.'
			});
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < array.Length; i++)
			{
				if (i != 0)
				{
					stringBuilder.Append(".");
				}
				string attrValueInProperCase = RegAndConfigRdr.GetAttrValueInProperCase(array[i].Trim());
				stringBuilder.Append(attrValueInProperCase);
			}
			storedProcKey = stringBuilder.ToString();
		}

		// Token: 0x0600077F RID: 1919 RVA: 0x000497B0 File Offset: 0x000487B0
		private static void AddMetadataForRefCursor(string refCursorKey, string metadataInfo, ref Hashtable schemaTable)
		{
			int num = metadataInfo.IndexOf("'");
			int num2 = metadataInfo.LastIndexOf("'");
			if (num != -1 && num2 != -1)
			{
				string text = metadataInfo.Substring(num + 1, num2 - (num + 1)).Trim();
				string[] array = text.Split(new char[]
				{
					';'
				});
				string[] array2 = refCursorKey.Split(new char[]
				{
					'.'
				});
				StringBuilder stringBuilder = new StringBuilder();
				int num3 = 0;
				foreach (string text2 in array2)
				{
					if (text2.Trim().Equals("RefCursorMetaData"))
					{
						break;
					}
					num3++;
					stringBuilder.Append(text2 + ".");
				}
				if (num3 == array2.Length)
				{
					if (OraTrace.m_TraceLevel != 0U)
					{
						OraTrace.Trace(1U, new string[]
						{
							" (ERROR) (REFCURSOR) " + refCursorKey + " : Metadata is invalid\n"
						});
					}
					throw new ConfigurationErrorsException(OpoErrResManager.GetErrorMesg(ErrRes.ODP_INVALID_VALUE, new string[]
					{
						refCursorKey + " Metadata"
					}));
				}
				stringBuilder.Remove(stringBuilder.Length - 1, 1);
				string text3 = array2[num3 + 1].Trim();
				int num4 = -1;
				try
				{
					num4 = int.Parse(array2[array2.Length - 1]);
				}
				catch (FormatException ex)
				{
					if (OraTrace.m_TraceLevel != 0U)
					{
						OraTrace.Trace(1U, new string[]
						{
							" (ERROR) (REFCURSOR) " + refCursorKey + " : Column Ordinal is invalid\n"
						});
					}
					throw new ConfigurationErrorsException(ex.Message);
				}
				bool flag = false;
				int num5 = -1;
				if (int.TryParse(text3, out num5))
				{
					flag = true;
				}
				string key = stringBuilder.ToString().Trim();
				RegAndConfigRdr.GetKeyInProperCase(ref key);
				RefCursorInfo refCursorInfo = null;
				StoredProcedureInfo storedProcedureInfo = (StoredProcedureInfo)schemaTable[key];
				if (storedProcedureInfo != null)
				{
					foreach (object obj in storedProcedureInfo.refCursors)
					{
						RefCursorInfo refCursorInfo2 = (RefCursorInfo)obj;
						if (flag)
						{
							if (refCursorInfo2.position == num5)
							{
								refCursorInfo = refCursorInfo2;
							}
						}
						else if (refCursorInfo2.name.ToLower().Equals(text3.ToLower()))
						{
							refCursorInfo = refCursorInfo2;
						}
					}
				}
				if (refCursorInfo == null)
				{
					if (OraTrace.m_TraceLevel != 0U)
					{
						OraTrace.Trace(1U, new string[]
						{
							" (ERROR) (REFCURSOR) " + refCursorKey + " : Metadata is invalid\n"
						});
					}
					throw new ConfigurationErrorsException(OpoErrResManager.GetErrorMesg(ErrRes.ODP_INVALID_VALUE, new string[]
					{
						refCursorKey + " Metadata"
					}));
				}
				DataRow dataRow = refCursorInfo.columnInfo.NewRow();
				dataRow["ColumnOrdinal"] = num4;
				string[] array4 = null;
				foreach (string text4 in array)
				{
					if (!(text4 == string.Empty))
					{
						try
						{
							array4 = text4.Split(new char[]
							{
								'='
							});
							string key2;
							if ((key2 = array4[0].Trim().ToUpperInvariant()) != null)
							{
								if (<PrivateImplementationDetails>{644819B4-6087-43CF-9BCE-A751E23BE24E}.$$method0x6000776-1 == null)
								{
									<PrivateImplementationDetails>{644819B4-6087-43CF-9BCE-A751E23BE24E}.$$method0x6000776-1 = new Dictionary<string, int>(23)
									{
										{
											"COLUMNNAME",
											0
										},
										{
											"COLUMNSIZE",
											1
										},
										{
											"NUMERICPRECISION",
											2
										},
										{
											"NUMERICSCALE",
											3
										},
										{
											"ISUNIQUE",
											4
										},
										{
											"ISKEY",
											5
										},
										{
											"ISROWID",
											6
										},
										{
											"BASECOLUMNNAME",
											7
										},
										{
											"BASESCHEMANAME",
											8
										},
										{
											"BASETABLENAME",
											9
										},
										{
											"DATATYPE",
											10
										},
										{
											"PROVIDERTYPE",
											11
										},
										{
											"ALLOWDBNULL",
											12
										},
										{
											"ISALIASED",
											13
										},
										{
											"ISBYTESEMANTIC",
											14
										},
										{
											"ISEXPRESSION",
											15
										},
										{
											"ISHIDDEN",
											16
										},
										{
											"ISREADONLY",
											17
										},
										{
											"ISLONG",
											18
										},
										{
											"UDTTYPENAME",
											19
										},
										{
											"NATIVEDATATYPE",
											20
										},
										{
											"PROVIDERDBTYPE",
											21
										},
										{
											"OBJECTNAME",
											22
										}
									};
								}
								int num6;
								if (<PrivateImplementationDetails>{644819B4-6087-43CF-9BCE-A751E23BE24E}.$$method0x6000776-1.TryGetValue(key2, out num6))
								{
									switch (num6)
									{
									case 0:
										dataRow["ColumnName"] = RegAndConfigRdr.GetAttrValueInProperCase(array4[1].Trim());
										break;
									case 1:
										dataRow["ColumnSize"] = int.Parse(array4[1].Trim());
										break;
									case 2:
										dataRow["NumericPrecision"] = int.Parse(array4[1].Trim());
										break;
									case 3:
										dataRow["NumericScale"] = int.Parse(array4[1].Trim());
										break;
									case 4:
										dataRow["IsUnique"] = bool.Parse(array4[1].Trim());
										break;
									case 5:
										dataRow["IsKey"] = bool.Parse(array4[1].Trim());
										if ((bool)dataRow["IsKey"])
										{
											refCursorInfo.isPrimaryKeyPresent = true;
										}
										break;
									case 6:
										dataRow["IsRowID"] = bool.Parse(array4[1].Trim());
										break;
									case 7:
										dataRow["BaseColumnName"] = RegAndConfigRdr.GetAttrValueInProperCase(array4[1].Trim());
										break;
									case 8:
										dataRow["BaseSchemaName"] = RegAndConfigRdr.GetAttrValueInProperCase(array4[1].Trim());
										break;
									case 9:
										dataRow["BaseTableName"] = RegAndConfigRdr.GetAttrValueInProperCase(array4[1].Trim());
										break;
									case 10:
										dataRow["DataType"] = Type.GetType(array4[1].Trim());
										break;
									case 11:
										dataRow["ProviderType"] = (OracleDbType)Enum.Parse(typeof(OracleDbType), array4[1].Trim().Split(new char[]
										{
											'.'
										})[array4[1].Trim().Split(new char[]
										{
											'.'
										}).Length - 1], true);
										break;
									case 12:
										dataRow["AllowDBNull"] = bool.Parse(array4[1].Trim());
										break;
									case 13:
										dataRow["IsAliased"] = bool.Parse(array4[1].Trim());
										break;
									case 14:
										dataRow["IsByteSemantic"] = bool.Parse(array4[1].Trim());
										break;
									case 15:
										dataRow["IsExpression"] = bool.Parse(array4[1].Trim());
										break;
									case 16:
										dataRow["IsHidden"] = bool.Parse(array4[1].Trim());
										break;
									case 17:
										dataRow["IsReadOnly"] = bool.Parse(array4[1].Trim());
										break;
									case 18:
										dataRow["IsLong"] = bool.Parse(array4[1].Trim());
										break;
									case 19:
										dataRow["UdtTypeName"] = RegAndConfigRdr.GetAttrValueInProperCase(array4[1].Trim());
										break;
									case 20:
										dataRow["NativeDataType"] = RegAndConfigRdr.GetAttrValueInProperCase(array4[1].Trim());
										break;
									case 21:
										dataRow["ProviderDBType"] = (DbType)Enum.Parse(typeof(DbType), array4[1].Trim().Split(new char[]
										{
											'.'
										})[array4[1].Trim().Split(new char[]
										{
											'.'
										}).Length - 1], true);
										break;
									case 22:
										dataRow["ObjectName"] = RegAndConfigRdr.GetAttrValueInProperCase(array4[1].Trim());
										break;
									default:
										goto IL_880;
									}
									goto IL_9D3;
								}
							}
							IL_880:
							if (OraTrace.m_TraceLevel != 0U)
							{
								OraTrace.Trace(1U, new string[]
								{
									string.Concat(new string[]
									{
										" (ERROR) (REFCURSOR) ",
										refCursorKey,
										" : ",
										array4[0],
										" is invalid\n"
									})
								});
							}
							throw new ConfigurationErrorsException(OpoErrResManager.GetErrorMesg(ErrRes.ODP_INVALID_VALUE, new string[]
							{
								refCursorKey + " " + array4[0]
							}));
						}
						catch (ConfigurationErrorsException)
						{
							throw;
						}
						catch (Exception)
						{
							if (OraTrace.m_TraceLevel != 0U)
							{
								OraTrace.Trace(1U, new string[]
								{
									string.Concat(new string[]
									{
										" (ERROR) (REFCURSOR) ",
										refCursorKey,
										" : ",
										array4[0].Trim(),
										"=",
										array4[1].Trim(),
										" is invalid\n"
									})
								});
							}
							throw new ConfigurationErrorsException(OpoErrResManager.GetErrorMesg(ErrRes.ODP_INVALID_VALUE, new string[]
							{
								string.Concat(new string[]
								{
									refCursorKey,
									" ",
									array4[0].Trim(),
									"=",
									array4[1].Trim()
								})
							}));
						}
					}
					IL_9D3:;
				}
				if (refCursorInfo.columnInfo.Rows.Count > num4)
				{
					refCursorInfo.columnInfo.Rows.InsertAt(dataRow, num4);
				}
				else
				{
					refCursorInfo.columnInfo.Rows.Add(dataRow);
				}
				refCursorInfo.columnInfo.AcceptChanges();
			}
		}

		// Token: 0x06000780 RID: 1920 RVA: 0x0004A250 File Offset: 0x00049250
		internal static StoredProcedureInfo GetStoredProcInfo(string commandText)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) (REFCURSOR) GetRefCursorInfo(" + commandText + ")\n"
				});
			}
			if (commandText == null || commandText.Length == 0)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  (REFCURSOR) GetRefCursorInfo(" + commandText + ") : no match\n"
					});
				}
				return null;
			}
			commandText = commandText.Trim();
			StoredProcedureInfo storedProcedureInfo = null;
			string text = null;
			if (RegAndConfigRdr.s_storedProcInformation.Count > 0)
			{
				storedProcedureInfo = (StoredProcedureInfo)RegAndConfigRdr.s_storedProcInformation[commandText];
				if (storedProcedureInfo == null)
				{
					text = commandText;
					RegAndConfigRdr.GetKeyInProperCase(ref text);
					storedProcedureInfo = (StoredProcedureInfo)RegAndConfigRdr.s_storedProcInformation[text];
				}
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				if (storedProcedureInfo == null)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  (REFCURSOR) GetRefCursorInfo(" + commandText + ") : no match\n"
					});
				}
				else if (text == null)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  (REFCURSOR) GetRefCursorInfo(" + commandText + ") : match found!\n"
					});
				}
				else
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  (REFCURSOR) GetRefCursorInfo(" + text + ") : match found!\n"
					});
				}
			}
			return storedProcedureInfo;
		}

		// Token: 0x06000781 RID: 1921 RVA: 0x0004A380 File Offset: 0x00049380
		internal static StoredProcedureInfo GetStoredProcInfo(Hashtable storedProceduresList, string schemaName, string storedProcName)
		{
			string key = string.Empty;
			StoredProcedureInfo storedProcedureInfo = null;
			if (storedProcName == null || storedProcName.Length == 0)
			{
				return null;
			}
			storedProcName = RegAndConfigRdr.GetAttrValueInProperCase(storedProcName.Trim());
			if (schemaName != null && schemaName.Length > 0)
			{
				schemaName = RegAndConfigRdr.GetAttrValueInProperCase(schemaName.Trim());
				key = schemaName + "." + storedProcName;
				storedProcedureInfo = (StoredProcedureInfo)storedProceduresList[key];
			}
			if (storedProcedureInfo == null)
			{
				storedProcedureInfo = (StoredProcedureInfo)storedProceduresList[storedProcName];
			}
			return storedProcedureInfo;
		}

		// Token: 0x06000782 RID: 1922 RVA: 0x0004A3F4 File Offset: 0x000493F4
		public static DataTable GetRefCursorInfoForSP(string configFileAlongWithFullPath, string schemaName, string storedProcName)
		{
			if (storedProcName == null || configFileAlongWithFullPath == null)
			{
				RegAndConfigRdr.s_edmMapping.Clear();
				return null;
			}
			if (OraTrace.m_TraceLevel != 0U && storedProcName.Length != 0)
			{
				OraTrace.Trace(1U, new string[]
				{
					string.Concat(new string[]
					{
						" (ENTRY) (REFCURSOR) GetRefCursorInfoForSP(",
						schemaName,
						".",
						storedProcName,
						")\n"
					})
				});
			}
			if (storedProcName.Length != 0 && OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					string.Concat(new string[]
					{
						" (EXIT)  (REFCURSOR) GetRefCursorInfoForSP(",
						schemaName,
						".",
						storedProcName,
						") : no match\n"
					})
				});
			}
			string key = configFileAlongWithFullPath.Trim();
			Hashtable hashtable = new Hashtable();
			bool flag = RegAndConfigRdr.s_odtConfigNamesToRefCursorInfo.Contains(key);
			DateTime lastWriteTimeUtc = File.GetLastWriteTimeUtc(configFileAlongWithFullPath);
			if (flag)
			{
				ODTConfigFileInfoForRefCursors odtconfigFileInfoForRefCursors = (ODTConfigFileInfoForRefCursors)RegAndConfigRdr.s_odtConfigNamesToRefCursorInfo[key];
				if (odtconfigFileInfoForRefCursors.lastModifiedTime != lastWriteTimeUtc)
				{
					flag = false;
				}
			}
			if (!flag)
			{
				RegAndConfigRdr.s_edmMapping.Clear();
				XmlDocument xmlDocument = new XmlDocument();
				XmlTextReader xmlTextReader = new XmlTextReader(configFileAlongWithFullPath);
				xmlDocument.Load(xmlTextReader);
				XmlNodeList elementsByTagName = xmlDocument.GetElementsByTagName("oracle.dataaccess.client");
				if (elementsByTagName.Count == 0)
				{
					xmlTextReader.Close();
					if (OraTrace.m_TraceLevel != 0U && storedProcName.Length != 0)
					{
						OraTrace.Trace(1U, new string[]
						{
							string.Concat(new string[]
							{
								" (EXIT)  (REFCURSOR) GetRefCursorInfoForSP(",
								schemaName,
								".",
								storedProcName,
								") : no match\n"
							})
						});
					}
					return null;
				}
				NameValueCollection nameValueCollectionFromNodeList = RegAndConfigRdr.GetNameValueCollectionFromNodeList(elementsByTagName[0]);
				RegAndConfigRdr.RetrieveInfoFromConfig(nameValueCollectionFromNodeList, ref hashtable, true);
				ODTConfigFileInfoForRefCursors odtconfigFileInfoForRefCursors2 = new ODTConfigFileInfoForRefCursors();
				odtconfigFileInfoForRefCursors2.lastModifiedTime = lastWriteTimeUtc;
				odtconfigFileInfoForRefCursors2.storedProcList = hashtable;
				RegAndConfigRdr.s_odtConfigNamesToRefCursorInfo[key] = odtconfigFileInfoForRefCursors2;
				xmlTextReader.Close();
				RegAndConfigRdr.TraceRefCursorInfoInAppConfig(configFileAlongWithFullPath);
			}
			else
			{
				ODTConfigFileInfoForRefCursors odtconfigFileInfoForRefCursors3 = (ODTConfigFileInfoForRefCursors)RegAndConfigRdr.s_odtConfigNamesToRefCursorInfo[key];
				hashtable = odtconfigFileInfoForRefCursors3.storedProcList;
			}
			StoredProcedureInfo storedProcedureInfo = (StoredProcedureInfo)hashtable[schemaName + "." + storedProcName];
			if (storedProcedureInfo != null && storedProcedureInfo.refCursors.Count != 0 && OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					string.Concat(new string[]
					{
						" (EXIT)  (REFCURSOR) GetRefCursorInfoForSP(",
						schemaName,
						".",
						storedProcName,
						") : no match\n"
					})
				});
			}
			if (OraTrace.m_TraceLevel != 0U && storedProcName.Length != 0)
			{
				OraTrace.Trace(1U, new string[]
				{
					string.Concat(new string[]
					{
						" (EXIT)  (REFCURSOR) GetRefCursorInfoForSP(",
						schemaName,
						".",
						storedProcName,
						") : match found!\n"
					})
				});
			}
			if (storedProcedureInfo != null && storedProcedureInfo.refCursors.Count != 0)
			{
				return ((RefCursorInfo)storedProcedureInfo.refCursors[0]).columnInfo.Copy();
			}
			return null;
		}

		// Token: 0x06000783 RID: 1923 RVA: 0x0004A710 File Offset: 0x00049710
		private static NameValueCollection GetNameValueCollectionFromNodeList(XmlNode node)
		{
			NameValueCollection nameValueCollection = new NameValueCollection();
			ArrayList arrayList = new ArrayList();
			if (node.FirstChild == null)
			{
				return null;
			}
			XmlNode xmlNode = node;
			if (xmlNode.FirstChild.Name != "add")
			{
				xmlNode = xmlNode.FirstChild;
				if (xmlNode.FirstChild == null)
				{
					return null;
				}
			}
			foreach (object obj in xmlNode.ChildNodes)
			{
				XmlNode xmlNode2 = (XmlNode)obj;
				if (xmlNode2.Name.ToLower() == "add")
				{
					string requiredAttribute = RegAndConfigRdr.GetRequiredAttribute(xmlNode2, "name");
					string requiredAttribute2 = RegAndConfigRdr.GetRequiredAttribute(xmlNode2, "value");
					if (!arrayList.Contains(requiredAttribute))
					{
						nameValueCollection[requiredAttribute] = requiredAttribute2;
						arrayList.Add(requiredAttribute);
					}
				}
			}
			return nameValueCollection;
		}

		// Token: 0x06000784 RID: 1924 RVA: 0x0004A7F8 File Offset: 0x000497F8
		internal static string GetRequiredAttribute(XmlNode node, string name)
		{
			XmlAttribute xmlAttribute = node.Attributes.RemoveNamedItem(name) as XmlAttribute;
			if (xmlAttribute == null || xmlAttribute.Value == string.Empty)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (ERROR) (REFCURSOR) " + name + " is invalid\n"
					});
				}
				throw new ConfigurationErrorsException(OpoErrResManager.GetErrorMesg(ErrRes.ODP_INVALID_VALUE, new string[]
				{
					name
				}));
			}
			return xmlAttribute.Value;
		}

		// Token: 0x06000785 RID: 1925 RVA: 0x0004A878 File Offset: 0x00049878
		[RegistryPermission(SecurityAction.Assert, Unrestricted = true)]
		[ConfigurationPermission(SecurityAction.Assert, Unrestricted = true)]
		public static string RetrieveStringValue(string entryToBeSearched, object defaultValue, ref bool bFromConfigFile)
		{
			string text = null;
			try
			{
				if (RegAndConfigRdr.m_configSection != null && RegAndConfigRdr.m_configSection[entryToBeSearched] != null)
				{
					string text2 = RegAndConfigRdr.m_configSection[entryToBeSearched];
					if (text2 != null)
					{
						string[] array = text2.Split(new char[]
						{
							','
						});
						if (array.Length > 1)
						{
							text = array[array.Length - 1];
						}
						else
						{
							text = array[0];
						}
					}
					bFromConfigFile = true;
				}
				else if (RegAndConfigRdr.odpNetKey != null)
				{
					text = (RegAndConfigRdr.odpNetKey.GetValue(entryToBeSearched) as string);
				}
			}
			catch
			{
			}
			if ((text == null || text == string.Empty) && defaultValue != null)
			{
				text = defaultValue.ToString();
				bFromConfigFile = false;
			}
			return text;
		}

		// Token: 0x06000786 RID: 1926 RVA: 0x0004A924 File Offset: 0x00049924
		public static int RetrieveIntValue(string entryToBeSearched, object defaultValue, bool bAcceptNegativeValues, ref bool bFromConfigFile)
		{
			int num = 0;
			try
			{
				num = int.Parse(RegAndConfigRdr.RetrieveStringValue(entryToBeSearched, defaultValue, ref bFromConfigFile));
				if (!bAcceptNegativeValues && num < 0)
				{
					num = (int)defaultValue;
				}
			}
			catch
			{
				num = (int)defaultValue;
			}
			return num;
		}

		// Token: 0x06000787 RID: 1927 RVA: 0x0004A96C File Offset: 0x0004996C
		internal static void ReadEntriesForRegistryAndConfig()
		{
			OraTrace.m_oraOpsDllPath = RegAndConfigRdr.RetrieveStringValue("DllPath", "", ref RegAndConfigRdr.s_bFromConfigOraOpsDllPath);
			OraTrace.m_traceFileName = RegAndConfigRdr.RetrieveStringValue("TraceFileName", "", ref RegAndConfigRdr.s_bFromConfigTraceFileName);
			OraTrace.m_TraceOption = (uint)RegAndConfigRdr.RetrieveIntValue("TraceOption", 0, false, ref RegAndConfigRdr.s_bFromConfigTraceOption);
			OraTrace.m_TraceLevel = (uint)RegAndConfigRdr.RetrieveIntValue("TraceLevel", 0, false, ref RegAndConfigRdr.s_bFromConfigTraceLevel);
			OraTrace.m_udtCacheSize = (uint)RegAndConfigRdr.RetrieveIntValue("UdtCacheSize", 4096, false, ref RegAndConfigRdr.s_bFromConfigUdtCacheSize);
			OraTrace.m_StmtCacheSize = RegAndConfigRdr.RetrieveIntValue("StatementCacheSize", 0, false, ref RegAndConfigRdr.s_bFromConfigStmtCacheSize);
			OraTrace.m_checkConStatus = (uint)RegAndConfigRdr.RetrieveIntValue("CheckConStatus", 1, false, ref RegAndConfigRdr.s_bFromConfigCheckConStatus);
			OraTrace.m_dynamicEnlist = (uint)RegAndConfigRdr.RetrieveIntValue("DynamicEnlistment", 0, false, ref RegAndConfigRdr.s_bFromConfigDynamicEnlist);
			OraTrace.m_FetchSize = RegAndConfigRdr.RetrieveIntValue("FetchSize", 131072, false, ref RegAndConfigRdr.s_bFromConfigFetchSize);
			OraTrace.m_ociEvents = RegAndConfigRdr.RetrieveIntValue("OCI_EVENTS", 0, true, ref RegAndConfigRdr.s_bFromConfigOciEvents);
			OraTrace.m_stmtCacheWithUdts = RegAndConfigRdr.RetrieveIntValue("StatementCacheWithUdts", 1, false, ref RegAndConfigRdr.s_bFromConfigStmtCacheWithUdts);
			OraTrace.m_MetadataPooling = RegAndConfigRdr.RetrieveIntValue("MetadataPooling", 1, false, ref RegAndConfigRdr.s_bFromConfigMetadataPooling);
			OraTrace.m_DBNotificationPort = RegAndConfigRdr.RetrieveIntValue("DbNotificationPort", -1, true, ref RegAndConfigRdr.s_bFromConfigDBNotificationPort);
			OraTrace.m_DBNotificationRegInterval = RegAndConfigRdr.RetrieveIntValue("DbNotificationRegInterval", 0, false, ref RegAndConfigRdr.s_bFromConfigDbNtfnRegInterval);
			OraTrace.m_demandOrclPermission = RegAndConfigRdr.RetrieveIntValue("DemandOraclePermission", 0, false, ref RegAndConfigRdr.s_bFromConfigDemandOrclPermission);
			OraTrace.m_selfTuning = Convert.ToBoolean(RegAndConfigRdr.RetrieveIntValue("SelfTuning", 1, false, ref RegAndConfigRdr.s_bFromConfigSelfTuning));
			OraTrace.m_maxStatementCacheSize = RegAndConfigRdr.RetrieveIntValue("MaxStatementCacheSize", 100, false, ref RegAndConfigRdr.s_bFromConfigMaxStatementCacheSize);
			OraTrace.m_InitialLOBFetchSize = RegAndConfigRdr.RetrieveIntValue("InitialLOBFetchSize", -1, true, ref RegAndConfigRdr.s_bFromConfigInitialLOBFetchSize);
			OraTrace.m_InitialLONGFetchSize = RegAndConfigRdr.RetrieveIntValue("InitialLONGFetchSize", -1, true, ref RegAndConfigRdr.s_bFromConfigInitialLONGFetchSize);
			OraTrace.m_appEdition = RegAndConfigRdr.RetrieveStringValue("Edition", "", ref RegAndConfigRdr.s_bFromConfigAppEdition);
			OraTrace.m_MetaDataXml = RegAndConfigRdr.RetrieveStringValue("MetaDataXml", null, ref RegAndConfigRdr.s_bFromConfigMetaDataXml);
			OraTrace.m_RevertBUErrHandling = RegAndConfigRdr.RetrieveIntValue("RevertBatchUpdateErrorHandling", 0, false, ref RegAndConfigRdr.s_bFromConfigRevertBUErrHandling);
			OraTrace.m_fetchArrayPooling = RegAndConfigRdr.RetrieveIntValue("FetchArrayPooling", 1, false, ref RegAndConfigRdr.s_bFromConfigFetchArrayPooling);
			int performanceCounters = RegAndConfigRdr.RetrieveIntValue("PerformanceCounters", 0, false, ref RegAndConfigRdr.s_bFromConfigPerfCounters);
			OraTrace.m_PerformanceCounters = (PerfCounterLevel)performanceCounters;
			string strA = RegAndConfigRdr.RetrieveStringValue("PromotableTransaction", string.Empty, ref RegAndConfigRdr.s_bFromConfigPSPE);
			if (string.Compare(strA, "local", true) == 0)
			{
				OraTrace.m_PSPE = 0;
			}
			OraTrace.m_CPThreadPrioritization = RegAndConfigRdr.RetrieveIntValue("CPThreadPrioritization", 1, false, ref RegAndConfigRdr.s_bFromConfigCPThreadPrioritization);
			OraTrace.m_NoPSPESupport = Convert.ToBoolean(RegAndConfigRdr.RetrieveIntValue("DoNotUsePSPE", 0, false, ref RegAndConfigRdr.s_bFromConfigNoPSPESupport));
		}

		// Token: 0x06000788 RID: 1928 RVA: 0x0004AC6C File Offset: 0x00049C6C
		public static void TraceRefCursorInfoInAppConfig(string configFileName)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				Hashtable hashtable = null;
				if (configFileName != null && configFileName.Length > 0)
				{
					if (!RegAndConfigRdr.s_odtConfigNamesToRefCursorInfo.Contains(configFileName))
					{
						return;
					}
					hashtable = ((ODTConfigFileInfoForRefCursors)RegAndConfigRdr.s_odtConfigNamesToRefCursorInfo[configFileName]).storedProcList;
				}
				else
				{
					hashtable = RegAndConfigRdr.s_storedProcInformation;
				}
				if (hashtable.Keys.Count > 0)
				{
					foreach (object obj in hashtable.Keys)
					{
						string text = (string)obj;
						StringBuilder stringBuilder = new StringBuilder();
						ArrayList refCursors = ((StoredProcedureInfo)hashtable[text]).refCursors;
						foreach (object obj2 in refCursors)
						{
							RefCursorInfo refCursorInfo = (RefCursorInfo)obj2;
							stringBuilder.Append(RegAndConfigRdr.s_strCfg);
							if (configFileName != null && configFileName.Length > 0)
							{
								stringBuilder.Append(" (REFCURSOR) (Design-time Implicit Binding Info : [" + text + "]");
							}
							else
							{
								stringBuilder.Append(" (REFCURSOR) (Run-time Implicit Binding Info : [" + text + "]");
							}
							stringBuilder.Append("[param name/pos=" + ((refCursorInfo.name == string.Empty) ? refCursorInfo.position.ToString() : refCursorInfo.name) + ";");
							stringBuilder.Append("mode=" + refCursorInfo.mode + "] Metadata : ");
							string value = stringBuilder.ToString();
							if (refCursorInfo.columnInfo == null)
							{
								stringBuilder.Append("[<none>])");
								OraTrace.Trace(1U, new string[]
								{
									stringBuilder.ToString()
								});
							}
							else
							{
								DataTable columnInfo = refCursorInfo.columnInfo;
								for (int i = 0; i < columnInfo.Rows.Count; i++)
								{
									stringBuilder.Append("[");
									for (int j = 0; j < columnInfo.Columns.Count; j++)
									{
										stringBuilder.Append(string.Concat(new object[]
										{
											(j != 0) ? ";" : string.Empty,
											columnInfo.Columns[j].ToString(),
											"=",
											columnInfo.Rows[i][j]
										}));
									}
									stringBuilder.Append("])");
									OraTrace.Trace(1U, new string[]
									{
										stringBuilder.ToString()
									});
									stringBuilder.Length = 0;
									stringBuilder.Append(value);
								}
								stringBuilder.Length = 0;
							}
						}
					}
				}
			}
		}

		// Token: 0x06000789 RID: 1929 RVA: 0x0004AF74 File Offset: 0x00049F74
		public static void TraceRegistryAndConfigValues()
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					RegAndConfigRdr.s_strVer + " (" + OracleInit.m_version + RegAndConfigRdr.s_strTrm
				});
				OraTrace.Trace(1U, new string[]
				{
					(RegAndConfigRdr.s_bFromConfigOraOpsDllPath ? RegAndConfigRdr.s_strCfg : RegAndConfigRdr.s_strReg) + " (DllPath : " + OraTrace.m_oraOpsDllPath + RegAndConfigRdr.s_strTrm
				});
				OraTrace.Trace(1U, new string[]
				{
					(RegAndConfigRdr.s_bFromConfigTraceFileName ? RegAndConfigRdr.s_strCfg : RegAndConfigRdr.s_strReg) + " (TraceFileName : " + OraTrace.m_traceFileName + RegAndConfigRdr.s_strTrm
				});
				OraTrace.Trace(1U, new string[]
				{
					string.Concat(new object[]
					{
						RegAndConfigRdr.s_bFromConfigTraceLevel ? RegAndConfigRdr.s_strCfg : RegAndConfigRdr.s_strReg,
						" (TraceLevel : ",
						OraTrace.m_TraceLevel,
						RegAndConfigRdr.s_strTrm
					})
				});
				OraTrace.Trace(1U, new string[]
				{
					string.Concat(new object[]
					{
						RegAndConfigRdr.s_bFromConfigTraceOption ? RegAndConfigRdr.s_strCfg : RegAndConfigRdr.s_strReg,
						" (TraceOption : ",
						OraTrace.m_TraceOption,
						RegAndConfigRdr.s_strTrm
					})
				});
				OraTrace.Trace(1U, new string[]
				{
					string.Concat(new object[]
					{
						RegAndConfigRdr.s_bFromConfigCheckConStatus ? RegAndConfigRdr.s_strCfg : RegAndConfigRdr.s_strReg,
						" (CheckConStatus : ",
						OraTrace.m_checkConStatus,
						RegAndConfigRdr.s_strTrm
					})
				});
				OraTrace.Trace(1U, new string[]
				{
					string.Concat(new object[]
					{
						RegAndConfigRdr.s_bFromConfigUdtCacheSize ? RegAndConfigRdr.s_strCfg : RegAndConfigRdr.s_strReg,
						" (UdtCacheSize : ",
						OraTrace.m_udtCacheSize,
						RegAndConfigRdr.s_strTrm
					})
				});
				OraTrace.Trace(1U, new string[]
				{
					string.Concat(new object[]
					{
						RegAndConfigRdr.s_bFromConfigStmtCacheSize ? RegAndConfigRdr.s_strCfg : RegAndConfigRdr.s_strReg,
						" (StatementCacheSize : ",
						OraTrace.m_StmtCacheSize,
						RegAndConfigRdr.s_strTrm
					})
				});
				OraTrace.Trace(1U, new string[]
				{
					string.Concat(new object[]
					{
						RegAndConfigRdr.s_bFromConfigDynamicEnlist ? RegAndConfigRdr.s_strCfg : RegAndConfigRdr.s_strReg,
						" (DynamicEnlist : ",
						OraTrace.m_dynamicEnlist,
						RegAndConfigRdr.s_strTrm
					})
				});
				OraTrace.Trace(1U, new string[]
				{
					string.Concat(new object[]
					{
						RegAndConfigRdr.s_bFromConfigFetchSize ? RegAndConfigRdr.s_strCfg : RegAndConfigRdr.s_strReg,
						" (FetchSize : ",
						OraTrace.m_FetchSize,
						RegAndConfigRdr.s_strTrm
					})
				});
				OraTrace.Trace(1U, new string[]
				{
					string.Concat(new object[]
					{
						RegAndConfigRdr.s_bFromConfigOciEvents ? RegAndConfigRdr.s_strCfg : RegAndConfigRdr.s_strReg,
						" (OCI_EVENTS : ",
						OraTrace.m_ociEvents,
						RegAndConfigRdr.s_strTrm
					})
				});
				OraTrace.Trace(1U, new string[]
				{
					string.Concat(new object[]
					{
						RegAndConfigRdr.s_bFromConfigStmtCacheWithUdts ? RegAndConfigRdr.s_strCfg : RegAndConfigRdr.s_strReg,
						" (StatementCacheWithUdts : ",
						OraTrace.m_stmtCacheWithUdts,
						RegAndConfigRdr.s_strTrm
					})
				});
				OraTrace.Trace(1U, new string[]
				{
					string.Concat(new object[]
					{
						RegAndConfigRdr.s_bFromConfigMetadataPooling ? RegAndConfigRdr.s_strCfg : RegAndConfigRdr.s_strReg,
						" (MetadataPooling : ",
						OraTrace.m_MetadataPooling,
						RegAndConfigRdr.s_strTrm
					})
				});
				OraTrace.Trace(1U, new string[]
				{
					string.Concat(new object[]
					{
						RegAndConfigRdr.s_bFromConfigDBNotificationPort ? RegAndConfigRdr.s_strCfg : RegAndConfigRdr.s_strReg,
						" (DBNotificationPort : ",
						OraTrace.m_DBNotificationPort,
						RegAndConfigRdr.s_strTrm
					})
				});
				OraTrace.Trace(1U, new string[]
				{
					string.Concat(new object[]
					{
						RegAndConfigRdr.s_bFromConfigThreadPoolMaxSize ? RegAndConfigRdr.s_strCfg : RegAndConfigRdr.s_strReg,
						" (ThreadPoolMaxSize : ",
						OraTrace.m_threadPoolMaxSize,
						RegAndConfigRdr.s_strTrm
					})
				});
				OraTrace.Trace(1U, new string[]
				{
					string.Concat(new object[]
					{
						RegAndConfigRdr.s_bFromConfigDbNtfnRegInterval ? RegAndConfigRdr.s_strCfg : RegAndConfigRdr.s_strReg,
						" (DBNotificationRegInterval : ",
						OraTrace.m_DBNotificationRegInterval,
						RegAndConfigRdr.s_strTrm
					})
				});
				OraTrace.Trace(1U, new string[]
				{
					string.Concat(new object[]
					{
						RegAndConfigRdr.s_bFromConfigDemandOrclPermission ? RegAndConfigRdr.s_strCfg : RegAndConfigRdr.s_strReg,
						" (DemandOraclePermission : ",
						OraTrace.m_demandOrclPermission,
						RegAndConfigRdr.s_strTrm
					})
				});
				OraTrace.Trace(1U, new string[]
				{
					string.Concat(new object[]
					{
						RegAndConfigRdr.s_bFromConfigSelfTuning ? RegAndConfigRdr.s_strCfg : RegAndConfigRdr.s_strReg,
						" (SelfTuning : ",
						OraTrace.m_selfTuning,
						RegAndConfigRdr.s_strTrm
					})
				});
				OraTrace.Trace(1U, new string[]
				{
					string.Concat(new object[]
					{
						RegAndConfigRdr.s_bFromConfigMaxStatementCacheSize ? RegAndConfigRdr.s_strCfg : RegAndConfigRdr.s_strReg,
						" (MaxStatementCacheSize : ",
						OraTrace.m_maxStatementCacheSize,
						RegAndConfigRdr.s_strTrm
					})
				});
				OraTrace.Trace(1U, new string[]
				{
					(RegAndConfigRdr.s_bFromConfigAppEdition ? RegAndConfigRdr.s_strCfg : RegAndConfigRdr.s_strReg) + " (AppEdition : " + OraTrace.m_appEdition + RegAndConfigRdr.s_strTrm
				});
				OraTrace.Trace(1U, new string[]
				{
					(RegAndConfigRdr.s_bFromConfigMetaDataXml ? RegAndConfigRdr.s_strCfg : RegAndConfigRdr.s_strReg) + " (MetaDataXml : " + OraTrace.m_MetaDataXml + RegAndConfigRdr.s_strTrm
				});
				OraTrace.Trace(1U, new string[]
				{
					string.Concat(new object[]
					{
						RegAndConfigRdr.s_bFromConfigRevertBUErrHandling ? RegAndConfigRdr.s_strCfg : RegAndConfigRdr.s_strReg,
						" (RevertBatchUpdateErrorHandling : ",
						OraTrace.m_RevertBUErrHandling,
						RegAndConfigRdr.s_strTrm
					})
				});
				OraTrace.Trace(1U, new string[]
				{
					string.Concat(new object[]
					{
						RegAndConfigRdr.s_bFromConfigFetchArrayPooling ? RegAndConfigRdr.s_strCfg : RegAndConfigRdr.s_strReg,
						" (FetchArrayPooling : ",
						OraTrace.m_fetchArrayPooling,
						RegAndConfigRdr.s_strTrm
					})
				});
				OraTrace.Trace(1U, new string[]
				{
					string.Concat(new object[]
					{
						RegAndConfigRdr.s_bFromConfigPerfCounters ? RegAndConfigRdr.s_strCfg : RegAndConfigRdr.s_strReg,
						" (PerformanceCounters : ",
						OraTrace.m_PerformanceCounters,
						RegAndConfigRdr.s_strTrm
					})
				});
				OraTrace.Trace(1U, new string[]
				{
					string.Concat(new object[]
					{
						RegAndConfigRdr.s_bFromConfigPSPE ? RegAndConfigRdr.s_strCfg : RegAndConfigRdr.s_strReg,
						" (PSPE : ",
						OraTrace.m_PSPE,
						RegAndConfigRdr.s_strTrm
					})
				});
				OraTrace.Trace(1U, new string[]
				{
					string.Concat(new object[]
					{
						RegAndConfigRdr.s_bFromConfigNoPSPESupport ? RegAndConfigRdr.s_strCfg : RegAndConfigRdr.s_strReg,
						" (PSPESupport : ",
						OraTrace.m_NoPSPESupport,
						RegAndConfigRdr.s_strTrm
					})
				});
				RegAndConfigRdr.TraceRefCursorInfoInAppConfig(string.Empty);
			}
		}

		// Token: 0x0400043B RID: 1083
		internal static NameValueCollection m_configSection;

		// Token: 0x0400043C RID: 1084
		internal static RegistryKey odpNetKey = null;

		// Token: 0x0400043D RID: 1085
		internal static bool s_bFromConfigOraOpsDllPath = false;

		// Token: 0x0400043E RID: 1086
		internal static bool s_bFromConfigTraceLevel = false;

		// Token: 0x0400043F RID: 1087
		internal static bool s_bFromConfigTraceOption = false;

		// Token: 0x04000440 RID: 1088
		internal static bool s_bFromConfigUdtCacheSize = false;

		// Token: 0x04000441 RID: 1089
		internal static bool s_bFromConfigStmtCacheSize = false;

		// Token: 0x04000442 RID: 1090
		internal static bool s_bFromConfigCheckConStatus = false;

		// Token: 0x04000443 RID: 1091
		internal static bool s_bFromConfigDynamicEnlist = false;

		// Token: 0x04000444 RID: 1092
		internal static bool s_bFromConfigFetchSize = false;

		// Token: 0x04000445 RID: 1093
		internal static bool s_bFromConfigOciEvents = false;

		// Token: 0x04000446 RID: 1094
		internal static bool s_bFromConfigStmtCacheWithUdts = false;

		// Token: 0x04000447 RID: 1095
		internal static bool s_bFromConfigPSPE = false;

		// Token: 0x04000448 RID: 1096
		internal static bool s_bFromConfigMetadataPooling = false;

		// Token: 0x04000449 RID: 1097
		internal static bool s_bFromConfigDBNotificationPort = false;

		// Token: 0x0400044A RID: 1098
		internal static bool s_bFromConfigPerfCounters = false;

		// Token: 0x0400044B RID: 1099
		internal static bool s_bFromConfigThreadPoolMaxSize = false;

		// Token: 0x0400044C RID: 1100
		internal static bool s_bFromConfigDbNtfnRegInterval = false;

		// Token: 0x0400044D RID: 1101
		internal static bool s_bFromConfigDemandOrclPermission = false;

		// Token: 0x0400044E RID: 1102
		internal static bool s_bFromConfigTraceFileName = false;

		// Token: 0x0400044F RID: 1103
		internal static bool s_bFromConfigSelfTuning = false;

		// Token: 0x04000450 RID: 1104
		internal static bool s_bFromConfigMaxStatementCacheSize = false;

		// Token: 0x04000451 RID: 1105
		internal static bool s_bFromConfigAppEdition = false;

		// Token: 0x04000452 RID: 1106
		internal static bool s_bFromConfigMetaDataXml = false;

		// Token: 0x04000453 RID: 1107
		internal static bool s_bFromConfigFetchArrayPooling = false;

		// Token: 0x04000454 RID: 1108
		internal static bool s_bFromConfigRevertBUErrHandling = false;

		// Token: 0x04000455 RID: 1109
		internal static bool s_bFromConfigCPThreadPrioritization = false;

		// Token: 0x04000456 RID: 1110
		internal static bool s_bFromConfigNoPSPESupport = false;

		// Token: 0x04000457 RID: 1111
		internal static bool s_bFromConfigInitialLOBFetchSize = false;

		// Token: 0x04000458 RID: 1112
		internal static bool s_bFromConfigInitialLONGFetchSize = false;

		// Token: 0x04000459 RID: 1113
		internal static Hashtable s_edmMapping = new Hashtable();

		// Token: 0x0400045A RID: 1114
		internal static string[] s_edmTypes = new string[]
		{
			"BOOL",
			"BYTE",
			"INT16",
			"INT32",
			"INT64"
		};

		// Token: 0x0400045B RID: 1115
		internal static int[] s_maxPrecision = new int[]
		{
			-1,
			-1,
			5,
			10,
			19
		};

		// Token: 0x0400045C RID: 1116
		internal static string s_strReg = " (REGISTRY)";

		// Token: 0x0400045D RID: 1117
		internal static string s_strCfg = " (CONFIG)  ";

		// Token: 0x0400045E RID: 1118
		internal static string s_strVer = " (VERSION) ";

		// Token: 0x0400045F RID: 1119
		internal static string s_strTrm = ")\n";

		// Token: 0x04000460 RID: 1120
		internal static Hashtable s_storedProcInformation = new Hashtable();

		// Token: 0x04000461 RID: 1121
		internal static Hashtable s_odtConfigNamesToRefCursorInfo = new Hashtable();
	}
}
