using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Common;
using System.Data.Common.Utils;
using System.Data.Metadata.Edm;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml;

namespace Oracle.DataAccess.Client
{
	// Token: 0x0200011B RID: 283
	internal class EFOracleProviderManifest : DbXmlEnabledProviderManifest
	{
		// Token: 0x06000B52 RID: 2898 RVA: 0x00072B0C File Offset: 0x00071B0C
		public EFOracleProviderManifest(string manifestToken) : base(EFOracleProviderManifest.GetProviderManifest())
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) EFOracleProviderManifest::EFOracleProviderManifest()\n"
				});
			}
			this._version = EFOracleVersionUtils.GetStorageVersion(manifestToken);
			this._token = manifestToken;
			EFOracleProviderServices.FireEdmInUseEvent();
			EFOracleProviderManifest.m_bMapNumberToBoolean = false;
			int maxPrecision;
			if ((maxPrecision = RegAndConfigRdr.GetMaxPrecision("BOOL")) > 0)
			{
				EFOracleProviderManifest.m_edmMappingMaxBOOL = maxPrecision;
				EFOracleProviderManifest.m_bMapNumberToBoolean = true;
			}
			else
			{
				EFOracleProviderManifest.m_edmMappingMaxBOOL = 1;
			}
			EFOracleProviderManifest.m_bMapNumberToByte = false;
			int maxPrecision2;
			if ((maxPrecision2 = RegAndConfigRdr.GetMaxPrecision("BYTE")) > 0)
			{
				EFOracleProviderManifest.m_edmMappingMaxBYTE = maxPrecision2;
				EFOracleProviderManifest.m_bMapNumberToByte = true;
			}
			else
			{
				EFOracleProviderManifest.m_edmMappingMaxBYTE = 3;
			}
			int maxPrecision3;
			if ((maxPrecision3 = RegAndConfigRdr.GetMaxPrecision("INT16")) > 0)
			{
				EFOracleProviderManifest.m_edmMappingMaxINT16 = maxPrecision3;
			}
			else
			{
				EFOracleProviderManifest.m_edmMappingMaxINT16 = 5;
			}
			int maxPrecision4;
			if ((maxPrecision4 = RegAndConfigRdr.GetMaxPrecision("INT32")) > 0)
			{
				EFOracleProviderManifest.m_edmMappingMaxINT32 = maxPrecision4;
			}
			else
			{
				EFOracleProviderManifest.m_edmMappingMaxINT32 = 10;
			}
			int maxPrecision5;
			if ((maxPrecision5 = RegAndConfigRdr.GetMaxPrecision("INT64")) > 0)
			{
				EFOracleProviderManifest.m_edmMappingMaxINT64 = maxPrecision5;
			}
			else
			{
				EFOracleProviderManifest.m_edmMappingMaxINT64 = 19;
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  EFOracleProviderManifest::EFOracleProviderManifest()\n"
				});
			}
		}

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x06000B53 RID: 2899 RVA: 0x00072C47 File Offset: 0x00071C47
		internal string Token
		{
			get
			{
				return this._token;
			}
		}

		// Token: 0x06000B54 RID: 2900 RVA: 0x00072C4F File Offset: 0x00071C4F
		private static XmlReader GetProviderManifest()
		{
			return EFOracleProviderManifest.GetXmlResource("Oracle.DataAccess.src.EntityFramework.Resources.EFOracleProviderManifest.xml");
		}

		// Token: 0x06000B55 RID: 2901 RVA: 0x00072C5C File Offset: 0x00071C5C
		internal static string EscapeLikeText(string text, bool alwaysEscapeEscapeChar, out bool usedEscapeChar)
		{
			usedEscapeChar = false;
			if (!text.Contains("%") && !text.Contains("_") && (!alwaysEscapeEscapeChar || !text.Contains("\\")))
			{
				return text;
			}
			StringBuilder stringBuilder = new StringBuilder(text.Length);
			foreach (char c in text)
			{
				if (c == '%' || c == '_' || c == '\\')
				{
					stringBuilder.Append('\\');
					usedEscapeChar = true;
				}
				stringBuilder.Append(c);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000B56 RID: 2902 RVA: 0x00072CE8 File Offset: 0x00071CE8
		protected override XmlReader GetDbInformation(string informationType)
		{
			if (informationType == DbProviderManifest.StoreSchemaDefinition)
			{
				return this.GetStoreSchemaDescription();
			}
			if (informationType == DbProviderManifest.StoreSchemaMapping)
			{
				return this.GetStoreSchemaMapping();
			}
			if (informationType == DbProviderManifest.ConceptualSchemaDefinition)
			{
				return null;
			}
			throw new ProviderIncompatibleException(OpoErrResManager.GetErrorMesg(ErrRes.ODP_INVALID_VALUE, new string[]
			{
				informationType
			}));
		}

		// Token: 0x06000B57 RID: 2903 RVA: 0x00072D80 File Offset: 0x00071D80
		public override ReadOnlyCollection<PrimitiveType> GetStoreTypes()
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) EFOracleProviderManifest::GetStoreTypes()\n"
				});
			}
			if (this._primitiveTypes == null && EFOracleVersionUtils.IsVersionX(this._version))
			{
				if (this._version < EFOracleVersion.Oracle10gR1)
				{
					List<PrimitiveType> list = new List<PrimitiveType>(base.GetStoreTypes());
					list.RemoveAll(delegate(PrimitiveType primitiveType)
					{
						string text = primitiveType.Name.ToLowerInvariant();
						return text.Equals("binary_float", StringComparison.Ordinal) || text.Equals("binary_double", StringComparison.Ordinal);
					});
					this._primitiveTypes = list.AsReadOnly();
				}
				else
				{
					this._primitiveTypes = base.GetStoreTypes();
				}
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  EFOracleProviderManifest::GetStoreTypes()\n"
				});
			}
			return this._primitiveTypes;
		}

		// Token: 0x06000B58 RID: 2904 RVA: 0x00072E38 File Offset: 0x00071E38
		public override ReadOnlyCollection<EdmFunction> GetStoreFunctions()
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) EFOracleProviderManifest::GetStoreFunctions()\n"
				});
			}
			if (this._functions == null && EFOracleVersionUtils.IsVersionX(this._version))
			{
				this._functions = base.GetStoreFunctions();
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT) EFOracleProviderManifest::GetStoreFunctions()\n"
				});
			}
			return this._functions;
		}

		// Token: 0x06000B59 RID: 2905 RVA: 0x00072EA8 File Offset: 0x00071EA8
		public override TypeUsage GetEdmType(TypeUsage storeType)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) EFOracleProviderManifest::GetEdmType()\n"
				});
			}
			EntityUtils.CheckArgumentNull<TypeUsage>(storeType, "storeType");
			string text = storeType.EdmType.Name.ToLowerInvariant();
			if (!base.StoreTypeNameToEdmPrimitiveType.ContainsKey(text))
			{
				throw new ArgumentException(OpoErrResManager.GetErrorMesg(ErrRes.ODP_NOT_SUPPORTED, new string[]
				{
					"Oracle Data Provider for .NET",
					text
				}));
			}
			PrimitiveType primitiveType = base.StoreTypeNameToEdmPrimitiveType[text];
			int num = 0;
			bool isUnicode = true;
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT) EFOracleProviderManifest::GetEdmType()\n"
				});
			}
			string key;
			if ((key = text) != null)
			{
				if (<PrivateImplementationDetails>{644819B4-6087-43CF-9BCE-A751E23BE24E}.$$method0x6000b4b-1 == null)
				{
					<PrivateImplementationDetails>{644819B4-6087-43CF-9BCE-A751E23BE24E}.$$method0x6000b4b-1 = new Dictionary<string, int>(31)
					{
						{
							"int",
							0
						},
						{
							"smallint",
							1
						},
						{
							"binary_integer",
							2
						},
						{
							"pl/sql boolean",
							3
						},
						{
							"mlslabel",
							4
						},
						{
							"varchar2",
							5
						},
						{
							"char",
							6
						},
						{
							"nvarchar2",
							7
						},
						{
							"nchar",
							8
						},
						{
							"clob",
							9
						},
						{
							"long",
							10
						},
						{
							"xmltype",
							11
						},
						{
							"nclob",
							12
						},
						{
							"blob",
							13
						},
						{
							"bfile",
							14
						},
						{
							"raw",
							15
						},
						{
							"long raw",
							16
						},
						{
							"guid",
							17
						},
						{
							"binary_float",
							18
						},
						{
							"binary_double",
							19
						},
						{
							"rowid",
							20
						},
						{
							"urowid",
							21
						},
						{
							"float",
							22
						},
						{
							"odp_internal_use_type",
							23
						},
						{
							"number",
							24
						},
						{
							"date",
							25
						},
						{
							"timestamp",
							26
						},
						{
							"timestamp with time zone",
							27
						},
						{
							"timestamp with local time zone",
							28
						},
						{
							"interval year to month",
							29
						},
						{
							"interval day to second",
							30
						}
					};
				}
				int num2;
				if (<PrivateImplementationDetails>{644819B4-6087-43CF-9BCE-A751E23BE24E}.$$method0x6000b4b-1.TryGetValue(key, out num2))
				{
					PrimitiveTypeKind primitiveTypeKind;
					bool flag;
					bool isFixedLength;
					switch (num2)
					{
					case 0:
					case 1:
					case 2:
					case 3:
						return TypeUsage.CreateDefaultTypeUsage(primitiveType);
					case 4:
						return TypeUsage.CreateBinaryTypeUsage(primitiveType, true, 12345);
					case 5:
						primitiveTypeKind = PrimitiveTypeKind.String;
						flag = !MetadataHelpers.TryGetMaxLength(storeType, out num);
						isUnicode = false;
						isFixedLength = false;
						break;
					case 6:
						primitiveTypeKind = PrimitiveTypeKind.String;
						flag = !MetadataHelpers.TryGetMaxLength(storeType, out num);
						isUnicode = false;
						isFixedLength = true;
						break;
					case 7:
						primitiveTypeKind = PrimitiveTypeKind.String;
						flag = !MetadataHelpers.TryGetMaxLength(storeType, out num);
						isUnicode = true;
						isFixedLength = false;
						break;
					case 8:
						primitiveTypeKind = PrimitiveTypeKind.String;
						flag = !MetadataHelpers.TryGetMaxLength(storeType, out num);
						isUnicode = true;
						isFixedLength = true;
						break;
					case 9:
					case 10:
						primitiveTypeKind = PrimitiveTypeKind.String;
						flag = true;
						isUnicode = false;
						isFixedLength = false;
						break;
					case 11:
					case 12:
						primitiveTypeKind = PrimitiveTypeKind.String;
						flag = true;
						isUnicode = true;
						isFixedLength = false;
						break;
					case 13:
					case 14:
						primitiveTypeKind = PrimitiveTypeKind.Binary;
						flag = true;
						isFixedLength = false;
						break;
					case 15:
						primitiveTypeKind = PrimitiveTypeKind.Binary;
						flag = !MetadataHelpers.TryGetMaxLength(storeType, out num);
						isFixedLength = false;
						if (num == 16)
						{
							return TypeUsage.CreateDefaultTypeUsage(PrimitiveType.GetEdmPrimitiveType(PrimitiveTypeKind.Guid));
						}
						break;
					case 16:
						primitiveTypeKind = PrimitiveTypeKind.Binary;
						flag = !MetadataHelpers.TryGetMaxLength(storeType, out num);
						isFixedLength = false;
						break;
					case 17:
					case 18:
					case 19:
						return TypeUsage.CreateDefaultTypeUsage(primitiveType);
					case 20:
					case 21:
						primitiveTypeKind = PrimitiveTypeKind.String;
						flag = !MetadataHelpers.TryGetMaxLength(storeType, out num);
						isUnicode = false;
						isFixedLength = false;
						break;
					case 22:
					{
						byte value;
						byte scale;
						if (MetadataHelpers.TryGetPrecision(storeType, out value) && MetadataHelpers.TryGetScale(storeType, out scale))
						{
							byte precision = byte.Parse(((int)((double)Convert.ToInt32(value) * 0.30103 + 1.0)).ToString());
							return TypeUsage.CreateDecimalTypeUsage(primitiveType, precision, scale);
						}
						return TypeUsage.CreateDecimalTypeUsage(primitiveType);
					}
					case 23:
						return TypeUsage.CreateDefaultTypeUsage(PrimitiveType.GetEdmPrimitiveType(PrimitiveTypeKind.Boolean));
					case 24:
					{
						byte b;
						byte b2;
						if (!MetadataHelpers.TryGetPrecision(storeType, out b) || !MetadataHelpers.TryGetScale(storeType, out b2))
						{
							return TypeUsage.CreateDecimalTypeUsage(primitiveType);
						}
						if (b == 1 && b2 == 0)
						{
							if (EFOracleProviderManifest.m_bMapNumberToBoolean && b <= (byte)EFOracleProviderManifest.m_edmMappingMaxBOOL)
							{
								return TypeUsage.CreateDefaultTypeUsage(PrimitiveType.GetEdmPrimitiveType(PrimitiveTypeKind.Boolean));
							}
							if (EFOracleProviderManifest.m_bMapNumberToByte && b <= (byte)EFOracleProviderManifest.m_edmMappingMaxBYTE)
							{
								return TypeUsage.CreateDefaultTypeUsage(PrimitiveType.GetEdmPrimitiveType(PrimitiveTypeKind.Byte));
							}
							return TypeUsage.CreateDefaultTypeUsage(PrimitiveType.GetEdmPrimitiveType(PrimitiveTypeKind.Int16));
						}
						else
						{
							if (EFOracleProviderManifest.m_bMapNumberToByte && b2 == 0 && b <= (byte)EFOracleProviderManifest.m_edmMappingMaxBYTE)
							{
								return TypeUsage.CreateDefaultTypeUsage(PrimitiveType.GetEdmPrimitiveType(PrimitiveTypeKind.Byte));
							}
							if (b2 == 0 && b <= (byte)EFOracleProviderManifest.m_edmMappingMaxINT16)
							{
								return TypeUsage.CreateDefaultTypeUsage(PrimitiveType.GetEdmPrimitiveType(PrimitiveTypeKind.Int16));
							}
							if (b2 == 0 && b <= (byte)EFOracleProviderManifest.m_edmMappingMaxINT32)
							{
								return TypeUsage.CreateDefaultTypeUsage(PrimitiveType.GetEdmPrimitiveType(PrimitiveTypeKind.Int32));
							}
							if (b2 == 0 && b <= (byte)EFOracleProviderManifest.m_edmMappingMaxINT64)
							{
								return TypeUsage.CreateDefaultTypeUsage(PrimitiveType.GetEdmPrimitiveType(PrimitiveTypeKind.Int64));
							}
							return TypeUsage.CreateDecimalTypeUsage(primitiveType, b, b2);
						}
						break;
					}
					case 25:
						return TypeUsage.CreateDateTimeTypeUsage(PrimitiveType.GetEdmPrimitiveType(PrimitiveTypeKind.DateTime), null);
					case 26:
					{
						byte value2;
						if (MetadataHelpers.TryGetByteFacetValue(storeType, "Precision", out value2))
						{
							return TypeUsage.CreateDateTimeTypeUsage(PrimitiveType.GetEdmPrimitiveType(PrimitiveTypeKind.DateTime), new byte?(value2));
						}
						return TypeUsage.CreateDateTimeTypeUsage(PrimitiveType.GetEdmPrimitiveType(PrimitiveTypeKind.DateTime), new byte?(9));
					}
					case 27:
						return TypeUsage.CreateDateTimeOffsetTypeUsage(PrimitiveType.GetEdmPrimitiveType(PrimitiveTypeKind.DateTimeOffset), new byte?(9));
					case 28:
						return TypeUsage.CreateDateTimeTypeUsage(PrimitiveType.GetEdmPrimitiveType(PrimitiveTypeKind.DateTime), new byte?(byte.MaxValue));
					case 29:
						return TypeUsage.CreateDecimalTypeUsage(primitiveType, 250, 0);
					case 30:
						return TypeUsage.CreateDecimalTypeUsage(primitiveType, 251, 0);
					default:
						goto IL_5E1;
					}
					PrimitiveTypeKind primitiveTypeKind2 = primitiveTypeKind;
					if (primitiveTypeKind2 != PrimitiveTypeKind.Binary)
					{
						if (primitiveTypeKind2 != PrimitiveTypeKind.String)
						{
							throw new NotSupportedException(OpoErrResManager.GetErrorMesg(ErrRes.ODP_NOT_SUPPORTED, new string[]
							{
								"Oracle Data Provider for .NET",
								text
							}));
						}
						if (!flag)
						{
							return TypeUsage.CreateStringTypeUsage(primitiveType, isUnicode, isFixedLength, num);
						}
						return TypeUsage.CreateStringTypeUsage(primitiveType, isUnicode, isFixedLength);
					}
					else
					{
						if (!flag)
						{
							return TypeUsage.CreateBinaryTypeUsage(primitiveType, isFixedLength, num);
						}
						return TypeUsage.CreateBinaryTypeUsage(primitiveType, isFixedLength);
					}
				}
			}
			IL_5E1:
			throw new NotSupportedException(OpoErrResManager.GetErrorMesg(ErrRes.ODP_NOT_SUPPORTED, new string[]
			{
				"Oracle Data Provider for .NET",
				text
			}));
		}

		// Token: 0x06000B5A RID: 2906 RVA: 0x00073524 File Offset: 0x00072524
		public override TypeUsage GetStoreType(TypeUsage edmType)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) EFOracleProviderManifest::GetStoreType()\n"
				});
			}
			EntityUtils.CheckArgumentNull<TypeUsage>(edmType, "edmType");
			PrimitiveType primitiveType = edmType.EdmType as PrimitiveType;
			if (primitiveType == null)
			{
				throw new ArgumentException(OpoErrResManager.GetErrorMesg(ErrRes.ODP_NOT_SUPPORTED, new string[]
				{
					"Oracle Data Provider for .NET",
					edmType.EdmType.FullName
				}));
			}
			ReadOnlyMetadataCollection<Facet> facets = edmType.Facets;
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT) EFOracleProviderManifest::GetStoreType()\n"
				});
			}
			switch (primitiveType.PrimitiveTypeKind)
			{
			case PrimitiveTypeKind.Binary:
			{
				bool flag = facets["FixedLength"].Value != null && (bool)facets["FixedLength"].Value;
				Facet facet = facets["MaxLength"];
				bool flag2 = facet.IsUnbounded || facet.Value == null || (int)facet.Value > 2000;
				int num = (!flag2) ? ((int)facet.Value) : int.MinValue;
				TypeUsage result;
				if (flag)
				{
					result = TypeUsage.CreateBinaryTypeUsage(base.StoreTypeNameToStorePrimitiveType["raw"], true, flag2 ? 2000 : num);
				}
				else if (flag2)
				{
					result = TypeUsage.CreateBinaryTypeUsage(base.StoreTypeNameToStorePrimitiveType["blob"], false);
				}
				else
				{
					result = TypeUsage.CreateBinaryTypeUsage(base.StoreTypeNameToStorePrimitiveType["raw"], false, num);
				}
				return result;
			}
			case PrimitiveTypeKind.Boolean:
				return TypeUsage.CreateDecimalTypeUsage(base.StoreTypeNameToStorePrimitiveType["number"], (byte)EFOracleProviderManifest.m_edmMappingMaxBOOL, 0);
			case PrimitiveTypeKind.Byte:
				return TypeUsage.CreateDecimalTypeUsage(base.StoreTypeNameToStorePrimitiveType["number"], (byte)EFOracleProviderManifest.m_edmMappingMaxBYTE, 0);
			case PrimitiveTypeKind.DateTime:
			{
				if (facets == null || facets["Precision"].Value == null)
				{
					return TypeUsage.CreateDefaultTypeUsage(base.StoreTypeNameToStorePrimitiveType["date"]);
				}
				byte b = (byte)facets["Precision"].Value;
				if (b > 9)
				{
					return TypeUsage.CreateDefaultTypeUsage(base.StoreTypeNameToStorePrimitiveType["timestamp with local time zone"]);
				}
				return TypeUsage.CreateDefaultTypeUsage(base.StoreTypeNameToStorePrimitiveType["timestamp"]);
			}
			case PrimitiveTypeKind.Decimal:
			{
				byte b2;
				byte b3;
				if (MetadataHelpers.TryGetPrecision(edmType, out b2) && MetadataHelpers.TryGetScale(edmType, out b3))
				{
					if (b2 == 250 && b3 == 0)
					{
						return TypeUsage.CreateDecimalTypeUsage(base.StoreTypeNameToStorePrimitiveType["interval year to month"], 9, 0);
					}
					if (b2 == 251 && b3 == 0)
					{
						return TypeUsage.CreateDecimalTypeUsage(base.StoreTypeNameToStorePrimitiveType["interval day to second"], 9, 0);
					}
					return TypeUsage.CreateDecimalTypeUsage(base.StoreTypeNameToStorePrimitiveType["number"], b2, b3);
				}
				else
				{
					if (MetadataHelpers.TryGetPrecision(edmType, out b2))
					{
						return TypeUsage.CreateDecimalTypeUsage(base.StoreTypeNameToStorePrimitiveType["number"], b2, 0);
					}
					if (MetadataHelpers.TryGetScale(edmType, out b3))
					{
						return TypeUsage.CreateDecimalTypeUsage(base.StoreTypeNameToStorePrimitiveType["number"], 38, b3);
					}
					return TypeUsage.CreateDefaultTypeUsage(base.StoreTypeNameToStorePrimitiveType["number"]);
				}
				break;
			}
			case PrimitiveTypeKind.Double:
				if (this._version < EFOracleVersion.Oracle10gR1)
				{
					return TypeUsage.CreateDefaultTypeUsage(base.StoreTypeNameToStorePrimitiveType["number"]);
				}
				return TypeUsage.CreateDefaultTypeUsage(base.StoreTypeNameToStorePrimitiveType["binary_double"]);
			case PrimitiveTypeKind.Guid:
				return TypeUsage.CreateBinaryTypeUsage(base.StoreTypeNameToStorePrimitiveType["raw"], true, 16);
			case PrimitiveTypeKind.Single:
				if (this._version < EFOracleVersion.Oracle10gR1)
				{
					return TypeUsage.CreateDefaultTypeUsage(base.StoreTypeNameToStorePrimitiveType["number"]);
				}
				return TypeUsage.CreateDefaultTypeUsage(base.StoreTypeNameToStorePrimitiveType["binary_float"]);
			case PrimitiveTypeKind.Int16:
				return TypeUsage.CreateDecimalTypeUsage(base.StoreTypeNameToStorePrimitiveType["number"], (byte)EFOracleProviderManifest.m_edmMappingMaxINT16, 0);
			case PrimitiveTypeKind.Int32:
				return TypeUsage.CreateDecimalTypeUsage(base.StoreTypeNameToStorePrimitiveType["number"], (byte)EFOracleProviderManifest.m_edmMappingMaxINT32, 0);
			case PrimitiveTypeKind.Int64:
				return TypeUsage.CreateDecimalTypeUsage(base.StoreTypeNameToStorePrimitiveType["number"], (byte)EFOracleProviderManifest.m_edmMappingMaxINT64, 0);
			case PrimitiveTypeKind.String:
			{
				bool flag3 = facets["Unicode"].Value == null || (bool)facets["Unicode"].Value;
				bool flag4 = facets["FixedLength"].Value != null && (bool)facets["FixedLength"].Value;
				Facet facet2 = facets["MaxLength"];
				bool flag5 = facet2.IsUnbounded || facet2.Value == null || (int)facet2.Value > (flag3 ? 2000 : 4000);
				int num2 = (!flag5) ? ((int)facet2.Value) : int.MinValue;
				TypeUsage result2;
				if (flag3)
				{
					if (flag4)
					{
						result2 = TypeUsage.CreateStringTypeUsage(base.StoreTypeNameToStorePrimitiveType["nchar"], true, true, flag5 ? 1000 : num2);
					}
					else if (flag5)
					{
						result2 = TypeUsage.CreateStringTypeUsage(base.StoreTypeNameToStorePrimitiveType["nclob"], true, false);
					}
					else
					{
						result2 = TypeUsage.CreateStringTypeUsage(base.StoreTypeNameToStorePrimitiveType["nvarchar2"], true, false, num2);
					}
				}
				else if (flag4)
				{
					result2 = TypeUsage.CreateStringTypeUsage(base.StoreTypeNameToStorePrimitiveType["char"], false, true, flag5 ? 2000 : num2);
				}
				else if (flag5)
				{
					result2 = TypeUsage.CreateStringTypeUsage(base.StoreTypeNameToStorePrimitiveType["clob"], false, false);
				}
				else
				{
					result2 = TypeUsage.CreateStringTypeUsage(base.StoreTypeNameToStorePrimitiveType["varchar2"], false, false, num2);
				}
				return result2;
			}
			case PrimitiveTypeKind.DateTimeOffset:
				return TypeUsage.CreateDefaultTypeUsage(base.StoreTypeNameToStorePrimitiveType["timestamp with time zone"]);
			}
			throw new NotSupportedException(OpoErrResManager.GetErrorMesg(ErrRes.ODP_NOT_SUPPORTED, new string[]
			{
				"Oracle Data Provider for .NET",
				primitiveType.PrimitiveTypeKind.ToString()
			}));
		}

		// Token: 0x06000B5B RID: 2907 RVA: 0x00073B2A File Offset: 0x00072B2A
		public override bool SupportsEscapingLikeArgument(out char escapeCharacter)
		{
			escapeCharacter = '\\';
			return true;
		}

		// Token: 0x06000B5C RID: 2908 RVA: 0x00073B34 File Offset: 0x00072B34
		public override string EscapeLikeArgument(string argument)
		{
			bool flag;
			return EFOracleProviderManifest.EscapeLikeText(argument, true, out flag);
		}

		// Token: 0x06000B5D RID: 2909 RVA: 0x00073B4A File Offset: 0x00072B4A
		private XmlReader GetStoreSchemaMapping()
		{
			return EFOracleProviderManifest.GetXmlResource("Oracle.DataAccess.src.EntityFramework.Resources.EFOracleStoreSchemaMapping.msl");
		}

		// Token: 0x06000B5E RID: 2910 RVA: 0x00073B56 File Offset: 0x00072B56
		private XmlReader GetStoreSchemaDescription()
		{
			if (EFOracleVersionUtils.IsVersionX(this._version))
			{
				return EFOracleProviderManifest.GetXmlResource("Oracle.DataAccess.src.EntityFramework.Resources.EFOracleStoreSchemaDefinition.ssdl");
			}
			return null;
		}

		// Token: 0x06000B5F RID: 2911 RVA: 0x00073B74 File Offset: 0x00072B74
		internal static XmlReader GetXmlResource(string resourceName)
		{
			Assembly executingAssembly = Assembly.GetExecutingAssembly();
			Stream manifestResourceStream = executingAssembly.GetManifestResourceStream(resourceName);
			return XmlReader.Create(manifestResourceStream, null, resourceName);
		}

		// Token: 0x04000929 RID: 2345
		internal const string ProviderInvariantName = "Oracle.DataAccess.Client";

		// Token: 0x0400092A RID: 2346
		internal const string TokenOracle9iR2 = "9.2";

		// Token: 0x0400092B RID: 2347
		internal const string TokenOracle10gR1 = "10.1";

		// Token: 0x0400092C RID: 2348
		internal const string TokenOracle10gR2 = "10.2";

		// Token: 0x0400092D RID: 2349
		internal const string TokenOracle11gR1 = "11.1";

		// Token: 0x0400092E RID: 2350
		internal const string TokenOracle11gR2 = "11.2";

		// Token: 0x0400092F RID: 2351
		internal const string TokenOracle12gR1 = "12.1";

		// Token: 0x04000930 RID: 2352
		internal const string TokenOracle12gR2 = "12.2";

		// Token: 0x04000931 RID: 2353
		internal const char LikeEscapeChar = '\\';

		// Token: 0x04000932 RID: 2354
		internal const string LikeEscapeCharToString = "\\";

		// Token: 0x04000933 RID: 2355
		private const int BinaryMaxSize = 2000;

		// Token: 0x04000934 RID: 2356
		private const int Nvarchar2MaxSize = 2000;

		// Token: 0x04000935 RID: 2357
		private const int NcharMaxSize = 1000;

		// Token: 0x04000936 RID: 2358
		private const int CharMaxSize = 2000;

		// Token: 0x04000937 RID: 2359
		private const int Varchar2MaxSize = 4000;

		// Token: 0x04000938 RID: 2360
		internal static bool m_bMapNumberToBoolean = false;

		// Token: 0x04000939 RID: 2361
		internal static bool m_bMapNumberToByte = false;

		// Token: 0x0400093A RID: 2362
		internal static int m_edmMappingMaxBOOL = 1;

		// Token: 0x0400093B RID: 2363
		internal static int m_edmMappingMaxBYTE = 3;

		// Token: 0x0400093C RID: 2364
		internal static int m_edmMappingMaxINT16 = 5;

		// Token: 0x0400093D RID: 2365
		internal static int m_edmMappingMaxINT32 = 10;

		// Token: 0x0400093E RID: 2366
		internal static int m_edmMappingMaxINT64 = 19;

		// Token: 0x0400093F RID: 2367
		private EFOracleVersion _version = EFOracleVersion.Oracle11gR2;

		// Token: 0x04000940 RID: 2368
		private string _token = "11.2";

		// Token: 0x04000941 RID: 2369
		private ReadOnlyCollection<PrimitiveType> _primitiveTypes;

		// Token: 0x04000942 RID: 2370
		private ReadOnlyCollection<EdmFunction> _functions;
	}
}
