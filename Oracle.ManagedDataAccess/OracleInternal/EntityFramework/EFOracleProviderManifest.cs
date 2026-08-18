using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Common;
using System.Data.Common.Utils;
using System.Data.Metadata.Edm;
using System.IO;
using System.Reflection;
using System.Security.Permissions;
using System.Text;
using System.Xml;
using Oracle.ManagedDataAccess.Client;
using OracleInternal.Common;

namespace OracleInternal.EntityFramework
{
	// Token: 0x020000E4 RID: 228
	internal class EFOracleProviderManifest : DbXmlEnabledProviderManifest
	{
		// Token: 0x06000901 RID: 2305 RVA: 0x0006965C File Offset: 0x0006785C
		public EFOracleProviderManifest(string manifestToken) : base(EFOracleProviderManifest.GetProviderManifest())
		{
			if (EFProviderSettings.s_tracingEnabled)
			{
				EFProviderSettings.Instance.Trace(EFProviderSettings.EFTraceLevel.Entry, " (ENTRY) EFOracleProviderManifest::EFOracleProviderManifest()\n");
			}
			this._version = EFOracleVersionUtils.GetStorageVersion(manifestToken);
			this._token = manifestToken;
			ODTSettings.FireEdmInUseEvent();
			EFOracleProviderManifest.m_bMapNumberToBoolean = false;
			int maxPrecision;
			if ((maxPrecision = EFProviderSettings.Instance.GetMaxPrecision("BOOL")) > 0)
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
			if ((maxPrecision2 = EFProviderSettings.Instance.GetMaxPrecision("BYTE")) > 0)
			{
				EFOracleProviderManifest.m_edmMappingMaxBYTE = maxPrecision2;
				EFOracleProviderManifest.m_bMapNumberToByte = true;
			}
			else
			{
				EFOracleProviderManifest.m_edmMappingMaxBYTE = 3;
			}
			int maxPrecision3;
			if ((maxPrecision3 = EFProviderSettings.Instance.GetMaxPrecision("INT16")) > 0)
			{
				EFOracleProviderManifest.m_edmMappingMaxINT16 = maxPrecision3;
			}
			else
			{
				EFOracleProviderManifest.m_edmMappingMaxINT16 = 5;
			}
			int maxPrecision4;
			if ((maxPrecision4 = EFProviderSettings.Instance.GetMaxPrecision("INT32")) > 0)
			{
				EFOracleProviderManifest.m_edmMappingMaxINT32 = maxPrecision4;
			}
			else
			{
				EFOracleProviderManifest.m_edmMappingMaxINT32 = 10;
			}
			int maxPrecision5;
			if ((maxPrecision5 = EFProviderSettings.Instance.GetMaxPrecision("INT64")) > 0)
			{
				EFOracleProviderManifest.m_edmMappingMaxINT64 = maxPrecision5;
			}
			else
			{
				EFOracleProviderManifest.m_edmMappingMaxINT64 = 19;
			}
			if (EFProviderSettings.s_tracingEnabled)
			{
				EFProviderSettings.Instance.Trace(EFProviderSettings.EFTraceLevel.Entry, " (EXIT)  EFOracleProviderManifest::EFOracleProviderManifest()\n");
			}
		}

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x06000902 RID: 2306 RVA: 0x000697C0 File Offset: 0x000679C0
		internal string Token
		{
			get
			{
				return this._token;
			}
		}

		// Token: 0x06000903 RID: 2307 RVA: 0x000697C8 File Offset: 0x000679C8
		private static XmlReader GetProviderManifest()
		{
			if (EFOracleVersionUtils.GetStorageVersion(EFOracleProviderServices.versionHint_static) >= EFOracleVersion.Oracle12cR1)
			{
				return EFOracleProviderManifest.GetXmlResource("Oracle.ManagedDataAccess.src.EntityFramework.Resources.EFOracleProviderManifest_12c_or_later.xml");
			}
			return EFOracleProviderManifest.GetXmlResource("Oracle.ManagedDataAccess.src.EntityFramework.Resources.EFOracleProviderManifest.xml");
		}

		// Token: 0x06000904 RID: 2308 RVA: 0x000697F0 File Offset: 0x000679F0
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

		// Token: 0x06000905 RID: 2309 RVA: 0x0006987C File Offset: 0x00067A7C
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
			throw new ProviderIncompatibleException(EFProviderSettings.Instance.GetErrorMessage(-1202, new string[]
			{
				informationType
			}));
		}

		// Token: 0x06000906 RID: 2310 RVA: 0x000698E0 File Offset: 0x00067AE0
		public override ReadOnlyCollection<PrimitiveType> GetStoreTypes()
		{
			if (EFProviderSettings.s_tracingEnabled)
			{
				EFProviderSettings.Instance.Trace(EFProviderSettings.EFTraceLevel.Entry, " (ENTRY) EFOracleProviderManifest::GetStoreTypes()\n");
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
			if (EFProviderSettings.s_tracingEnabled)
			{
				EFProviderSettings.Instance.Trace(EFProviderSettings.EFTraceLevel.Entry, " (EXIT)  EFOracleProviderManifest::GetStoreTypes()\n");
			}
			return this._primitiveTypes;
		}

		// Token: 0x06000907 RID: 2311 RVA: 0x0006998C File Offset: 0x00067B8C
		public override ReadOnlyCollection<EdmFunction> GetStoreFunctions()
		{
			if (EFProviderSettings.s_tracingEnabled)
			{
				EFProviderSettings.Instance.Trace(EFProviderSettings.EFTraceLevel.Entry, " (ENTRY) EFOracleProviderManifest::GetStoreFunctions()\n");
			}
			if (this._functions == null && EFOracleVersionUtils.IsVersionX(this._version))
			{
				this._functions = base.GetStoreFunctions();
			}
			if (EFProviderSettings.s_tracingEnabled)
			{
				EFProviderSettings.Instance.Trace(EFProviderSettings.EFTraceLevel.Entry, " (EXIT) EFOracleProviderManifest::GetStoreFunctions()\n");
			}
			return this._functions;
		}

		// Token: 0x06000908 RID: 2312 RVA: 0x000699F0 File Offset: 0x00067BF0
		public override TypeUsage GetEdmType(TypeUsage storeType)
		{
			if (EFProviderSettings.s_tracingEnabled)
			{
				EFProviderSettings.Instance.Trace(EFProviderSettings.EFTraceLevel.Entry, " (ENTRY) EFOracleProviderManifest::GetEdmType()\n");
			}
			EntityUtils.CheckArgumentNull<TypeUsage>(storeType, "storeType");
			string text = storeType.EdmType.Name.ToLowerInvariant();
			if (!base.StoreTypeNameToEdmPrimitiveType.ContainsKey(text))
			{
				throw new ArgumentException(EFProviderSettings.Instance.GetErrorMessage(-1703, new string[]
				{
					"Oracle Data Provider for .NET",
					text
				}));
			}
			PrimitiveType primitiveType = base.StoreTypeNameToEdmPrimitiveType[text];
			int num = 0;
			bool isUnicode = true;
			if (EFProviderSettings.s_tracingEnabled)
			{
				EFProviderSettings.Instance.Trace(EFProviderSettings.EFTraceLevel.Entry, " (EXIT) EFOracleProviderManifest::GetEdmType()\n");
			}
			string key;
			if ((key = text) != null)
			{
				if (<PrivateImplementationDetails>{28A9BD3B-E95E-447F-A7DB-0C43D6EA795F}.$$method0x6000853-1 == null)
				{
					<PrivateImplementationDetails>{28A9BD3B-E95E-447F-A7DB-0C43D6EA795F}.$$method0x6000853-1 = new Dictionary<string, int>(31)
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
				if (<PrivateImplementationDetails>{28A9BD3B-E95E-447F-A7DB-0C43D6EA795F}.$$method0x6000853-1.TryGetValue(key, out num2))
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
						goto IL_5D4;
					}
					PrimitiveTypeKind primitiveTypeKind2 = primitiveTypeKind;
					if (primitiveTypeKind2 != PrimitiveTypeKind.Binary)
					{
						if (primitiveTypeKind2 != PrimitiveTypeKind.String)
						{
							throw new NotSupportedException(EFProviderSettings.Instance.GetErrorMessage(-1703, new string[]
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
			IL_5D4:
			throw new NotSupportedException(EFProviderSettings.Instance.GetErrorMessage(-1703, new string[]
			{
				"Oracle Data Provider for .NET",
				text
			}));
		}

		// Token: 0x06000909 RID: 2313 RVA: 0x0006A06C File Offset: 0x0006826C
		public override TypeUsage GetStoreType(TypeUsage edmType)
		{
			return this.GetStoreType(edmType, false);
		}

		// Token: 0x0600090A RID: 2314 RVA: 0x0006A078 File Offset: 0x00068278
		internal TypeUsage GetStoreType(TypeUsage edmType, bool bEnable32kSupport)
		{
			if (EFProviderSettings.s_tracingEnabled)
			{
				EFProviderSettings.Instance.Trace(EFProviderSettings.EFTraceLevel.Entry, " (ENTRY) EFOracleProviderManifest::GetStoreType() \n");
			}
			EntityUtils.CheckArgumentNull<TypeUsage>(edmType, "edmType");
			PrimitiveType primitiveType = edmType.EdmType as PrimitiveType;
			if (primitiveType == null)
			{
				throw new ArgumentException(EFProviderSettings.Instance.GetErrorMessage(-1703, new string[]
				{
					"Oracle Data Provider for .NET",
					edmType.EdmType.FullName
				}));
			}
			ReadOnlyMetadataCollection<Facet> facets = edmType.Facets;
			if (EFProviderSettings.s_tracingEnabled)
			{
				EFProviderSettings.Instance.Trace(EFProviderSettings.EFTraceLevel.Entry, " (EXIT) EFOracleProviderManifest::GetStoreType() \n");
			}
			switch (primitiveType.PrimitiveTypeKind)
			{
			case PrimitiveTypeKind.Binary:
			{
				bool flag = facets["FixedLength"].Value != null && (bool)facets["FixedLength"].Value;
				Facet facet = facets["MaxLength"];
				bool flag2;
				if (this._version >= EFOracleVersion.Oracle12cR1 && bEnable32kSupport)
				{
					flag2 = (facet.IsUnbounded || facet.Value == null || (int)facet.Value > this.BinaryMaxSize_12c);
				}
				else
				{
					flag2 = (facet.IsUnbounded || facet.Value == null || (int)facet.Value > 2000);
				}
				int num = (!flag2) ? ((int)facet.Value) : int.MinValue;
				TypeUsage result;
				if (flag)
				{
					if (this._version >= EFOracleVersion.Oracle12cR1 && bEnable32kSupport)
					{
						result = TypeUsage.CreateBinaryTypeUsage(base.StoreTypeNameToStorePrimitiveType["raw"], true, flag2 ? this.BinaryMaxSize_12c : num);
					}
					else
					{
						result = TypeUsage.CreateBinaryTypeUsage(base.StoreTypeNameToStorePrimitiveType["raw"], true, flag2 ? 2000 : num);
					}
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
				bool flag5;
				if (this._version >= EFOracleVersion.Oracle12cR1 && bEnable32kSupport)
				{
					flag5 = (facet2.IsUnbounded || facet2.Value == null || (int)facet2.Value > (flag3 ? this.Nvarchar2MaxSize_12c : this.Varchar2MaxSize_12c));
				}
				else
				{
					flag5 = (facet2.IsUnbounded || facet2.Value == null || (int)facet2.Value > (flag3 ? 4000 : 4000));
				}
				int num2 = (!flag5) ? ((int)facet2.Value) : int.MinValue;
				TypeUsage result2;
				if (flag3)
				{
					if (flag4)
					{
						result2 = TypeUsage.CreateStringTypeUsage(base.StoreTypeNameToStorePrimitiveType["nchar"], true, true, flag5 ? 2000 : num2);
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
			throw new NotSupportedException(EFProviderSettings.Instance.GetErrorMessage(-1703, new string[]
			{
				"Oracle Data Provider for .NET",
				primitiveType.PrimitiveTypeKind.ToString()
			}));
		}

		// Token: 0x0600090B RID: 2315 RVA: 0x0006A734 File Offset: 0x00068934
		public override bool SupportsEscapingLikeArgument(out char escapeCharacter)
		{
			escapeCharacter = '\\';
			return true;
		}

		// Token: 0x0600090C RID: 2316 RVA: 0x0006A73C File Offset: 0x0006893C
		public override string EscapeLikeArgument(string argument)
		{
			bool flag;
			return EFOracleProviderManifest.EscapeLikeText(argument, true, out flag);
		}

		// Token: 0x0600090D RID: 2317 RVA: 0x0006A754 File Offset: 0x00068954
		private XmlReader GetStoreSchemaMapping()
		{
			return EFOracleProviderManifest.GetXmlResource("Oracle.ManagedDataAccess.src.EntityFramework.Resources.EFOracleStoreSchemaMapping.msl");
		}

		// Token: 0x0600090E RID: 2318 RVA: 0x0006A760 File Offset: 0x00068960
		private XmlReader GetStoreSchemaDescription()
		{
			if (EFOracleVersionUtils.IsVersionX(this._version))
			{
				return EFOracleProviderManifest.GetOMDPStoreSchemaDescription();
			}
			return null;
		}

		// Token: 0x0600090F RID: 2319 RVA: 0x0006A778 File Offset: 0x00068978
		[ReflectionPermission(SecurityAction.Assert, Unrestricted = true)]
		internal static XmlReader GetXmlResource(string resourceName)
		{
			Assembly executingAssembly = Assembly.GetExecutingAssembly();
			Stream manifestResourceStream = executingAssembly.GetManifestResourceStream(resourceName);
			return XmlReader.Create(manifestResourceStream, null, resourceName);
		}

		// Token: 0x06000910 RID: 2320 RVA: 0x0006A79C File Offset: 0x0006899C
		[ReflectionPermission(SecurityAction.Assert, Unrestricted = true)]
		internal static XmlReader GetOMDPStoreSchemaDescription()
		{
			string text = "Oracle.ManagedDataAccess.src.EntityFramework.Resources.EFOracleStoreSchemaDefinition.ssdl";
			Assembly executingAssembly = Assembly.GetExecutingAssembly();
			Stream manifestResourceStream = executingAssembly.GetManifestResourceStream(text);
			MemoryStream memoryStream = new MemoryStream();
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.Load(manifestResourceStream);
			xmlDocument.DocumentElement.Attributes["Provider"].Value = "Oracle.ManagedDataAccess.Client";
			xmlDocument.Save(memoryStream);
			memoryStream.Seek(0L, SeekOrigin.Begin);
			manifestResourceStream.Dispose();
			return XmlReader.Create(memoryStream, null, text);
		}

		// Token: 0x04000BFB RID: 3067
		internal const string TokenOracle9iR2 = "9.2";

		// Token: 0x04000BFC RID: 3068
		internal const string TokenOracle10gR1 = "10.1";

		// Token: 0x04000BFD RID: 3069
		internal const string TokenOracle10gR2 = "10.2";

		// Token: 0x04000BFE RID: 3070
		internal const string TokenOracle11gR1 = "11.1";

		// Token: 0x04000BFF RID: 3071
		internal const string TokenOracle11gR2 = "11.2";

		// Token: 0x04000C00 RID: 3072
		internal const string TokenOracle12cR1 = "12.1";

		// Token: 0x04000C01 RID: 3073
		internal const string TokenOracle12cR2 = "12.2";

		// Token: 0x04000C02 RID: 3074
		internal const char LikeEscapeChar = '\\';

		// Token: 0x04000C03 RID: 3075
		internal const string LikeEscapeCharToString = "\\";

		// Token: 0x04000C04 RID: 3076
		private const int BinaryMaxSize = 2000;

		// Token: 0x04000C05 RID: 3077
		private const int Nvarchar2MaxSize = 4000;

		// Token: 0x04000C06 RID: 3078
		private const int NcharMaxSize = 2000;

		// Token: 0x04000C07 RID: 3079
		private const int CharMaxSize = 2000;

		// Token: 0x04000C08 RID: 3080
		private const int Varchar2MaxSize = 4000;

		// Token: 0x04000C09 RID: 3081
		internal static bool m_bMapNumberToBoolean = false;

		// Token: 0x04000C0A RID: 3082
		internal static bool m_bMapNumberToByte = false;

		// Token: 0x04000C0B RID: 3083
		internal static int m_edmMappingMaxBOOL = 1;

		// Token: 0x04000C0C RID: 3084
		internal static int m_edmMappingMaxBYTE = 3;

		// Token: 0x04000C0D RID: 3085
		internal static int m_edmMappingMaxINT16 = 5;

		// Token: 0x04000C0E RID: 3086
		internal static int m_edmMappingMaxINT32 = 10;

		// Token: 0x04000C0F RID: 3087
		internal static int m_edmMappingMaxINT64 = 19;

		// Token: 0x04000C10 RID: 3088
		private EFOracleVersion _version = EFOracleVersion.Oracle11gR2;

		// Token: 0x04000C11 RID: 3089
		private string _token = "11.2";

		// Token: 0x04000C12 RID: 3090
		private ReadOnlyCollection<PrimitiveType> _primitiveTypes;

		// Token: 0x04000C13 RID: 3091
		private ReadOnlyCollection<EdmFunction> _functions;

		// Token: 0x04000C14 RID: 3092
		private int Nvarchar2MaxSize_12c = 32767;

		// Token: 0x04000C15 RID: 3093
		private int Varchar2MaxSize_12c = 32767;

		// Token: 0x04000C16 RID: 3094
		private int BinaryMaxSize_12c = 32767;
	}
}
