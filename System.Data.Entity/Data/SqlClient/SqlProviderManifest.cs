using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Metadata.Edm;
using System.Linq;
using System.Text;
using System.Xml;

namespace System.Data.SqlClient
{
	// Token: 0x02000022 RID: 34
	internal class SqlProviderManifest : DbXmlEnabledProviderManifest
	{
		// Token: 0x06000223 RID: 547 RVA: 0x00006261 File Offset: 0x00004461
		public SqlProviderManifest(string manifestToken) : base(SqlProviderManifest.GetProviderManifest())
		{
			this._version = SqlVersionUtils.GetSqlVersion(manifestToken);
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000224 RID: 548 RVA: 0x00006282 File Offset: 0x00004482
		internal SqlVersion SqlVersion
		{
			get
			{
				return this._version;
			}
		}

		// Token: 0x06000225 RID: 549 RVA: 0x0000628A File Offset: 0x0000448A
		private static XmlReader GetProviderManifest()
		{
			return DbProviderServices.GetXmlResource("System.Data.Resources.SqlClient.SqlProviderServices.ProviderManifest.xml");
		}

		// Token: 0x06000226 RID: 550 RVA: 0x00006296 File Offset: 0x00004496
		private XmlReader GetStoreSchemaMapping(string mslName)
		{
			return DbProviderServices.GetXmlResource("System.Data.Resources.SqlClient.SqlProviderServices." + mslName + ".msl");
		}

		// Token: 0x06000227 RID: 551 RVA: 0x000062AD File Offset: 0x000044AD
		private XmlReader GetStoreSchemaDescription(string ssdlName)
		{
			if (this._version == SqlVersion.Sql8)
			{
				return DbProviderServices.GetXmlResource("System.Data.Resources.SqlClient.SqlProviderServices." + ssdlName + "_Sql8.ssdl");
			}
			return DbProviderServices.GetXmlResource("System.Data.Resources.SqlClient.SqlProviderServices." + ssdlName + ".ssdl");
		}

		// Token: 0x06000228 RID: 552 RVA: 0x000062E4 File Offset: 0x000044E4
		internal static string EscapeLikeText(string text, bool alwaysEscapeEscapeChar, out bool usedEscapeChar)
		{
			usedEscapeChar = false;
			if (!text.Contains("%") && !text.Contains("_") && !text.Contains("[") && !text.Contains("^") && (!alwaysEscapeEscapeChar || !text.Contains("~")))
			{
				return text;
			}
			StringBuilder stringBuilder = new StringBuilder(text.Length);
			foreach (char c in text)
			{
				if (c == '%' || c == '_' || c == '[' || c == '^' || c == '~')
				{
					stringBuilder.Append('~');
					usedEscapeChar = true;
				}
				stringBuilder.Append(c);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000229 RID: 553 RVA: 0x00006394 File Offset: 0x00004594
		protected override XmlReader GetDbInformation(string informationType)
		{
			if (informationType == DbProviderManifest.StoreSchemaDefinitionVersion3 || informationType == DbProviderManifest.StoreSchemaDefinition)
			{
				return this.GetStoreSchemaDescription(informationType);
			}
			if (informationType == DbProviderManifest.StoreSchemaMappingVersion3 || informationType == DbProviderManifest.StoreSchemaMapping)
			{
				return this.GetStoreSchemaMapping(informationType);
			}
			if (informationType == DbProviderManifest.ConceptualSchemaDefinitionVersion3 || informationType == DbProviderManifest.ConceptualSchemaDefinition)
			{
				return null;
			}
			throw EntityUtil.ProviderIncompatible(Strings.ProviderReturnedNullForGetDbInformation(informationType));
		}

		// Token: 0x0600022A RID: 554 RVA: 0x0000640C File Offset: 0x0000460C
		public override ReadOnlyCollection<PrimitiveType> GetStoreTypes()
		{
			if (this._primitiveTypes == null)
			{
				if (this._version == SqlVersion.Sql10)
				{
					this._primitiveTypes = base.GetStoreTypes();
				}
				else
				{
					List<PrimitiveType> list = new List<PrimitiveType>(base.GetStoreTypes());
					list.RemoveAll(delegate(PrimitiveType primitiveType)
					{
						string text = primitiveType.Name.ToLowerInvariant();
						return text.Equals("time", StringComparison.Ordinal) || text.Equals("date", StringComparison.Ordinal) || text.Equals("datetime2", StringComparison.Ordinal) || text.Equals("datetimeoffset", StringComparison.Ordinal) || text.Equals("geography", StringComparison.Ordinal) || text.Equals("geometry", StringComparison.Ordinal);
					});
					if (this._version == SqlVersion.Sql8)
					{
						list.RemoveAll(delegate(PrimitiveType primitiveType)
						{
							string text = primitiveType.Name.ToLowerInvariant();
							return text.Equals("xml", StringComparison.Ordinal) || text.EndsWith("(max)", StringComparison.Ordinal);
						});
					}
					this._primitiveTypes = list.AsReadOnly();
				}
			}
			return this._primitiveTypes;
		}

		// Token: 0x0600022B RID: 555 RVA: 0x000064B0 File Offset: 0x000046B0
		public override ReadOnlyCollection<EdmFunction> GetStoreFunctions()
		{
			if (this._functions == null)
			{
				if (this._version == SqlVersion.Sql10)
				{
					this._functions = base.GetStoreFunctions();
				}
				else
				{
					IEnumerable<EdmFunction> source = from f in base.GetStoreFunctions()
					where !SqlProviderManifest.IsKatmaiOrNewer(f)
					select f;
					if (this._version == SqlVersion.Sql8)
					{
						source = from f in source
						where !SqlProviderManifest.IsYukonOrNewer(f)
						select f;
					}
					this._functions = source.ToList<EdmFunction>().AsReadOnly();
				}
			}
			return this._functions;
		}

		// Token: 0x0600022C RID: 556 RVA: 0x00006554 File Offset: 0x00004754
		private static bool IsKatmaiOrNewer(EdmFunction edmFunction)
		{
			if (edmFunction.ReturnParameter == null || !Helper.IsSpatialType(edmFunction.ReturnParameter.TypeUsage))
			{
				if (!edmFunction.Parameters.Any((FunctionParameter p) => Helper.IsSpatialType(p.TypeUsage)))
				{
					ReadOnlyMetadataCollection<FunctionParameter> parameters = edmFunction.Parameters;
					string text = edmFunction.Name.ToUpperInvariant();
					uint num = <PrivateImplementationDetails>.ComputeStringHash(text);
					if (num > 1900062112U)
					{
						if (num <= 2897371749U)
						{
							if (num <= 2725144690U)
							{
								if (num != 2585027932U)
								{
									if (num != 2725144690U)
									{
										return false;
									}
									if (!(text == "DATENAME"))
									{
										return false;
									}
								}
								else
								{
									if (!(text == "YEAR"))
									{
										return false;
									}
									goto IL_2FF;
								}
							}
							else if (num != 2750919380U)
							{
								if (num != 2897371749U)
								{
									return false;
								}
								if (!(text == "COUNT_BIG"))
								{
									return false;
								}
								goto IL_2BC;
							}
							else
							{
								if (!(text == "COUNT"))
								{
									return false;
								}
								goto IL_2BC;
							}
						}
						else if (num <= 3457630659U)
						{
							if (num != 3224348880U)
							{
								if (num != 3457630659U)
								{
									return false;
								}
								if (!(text == "SYSDATETIME"))
								{
									return false;
								}
								return true;
							}
							else if (!(text == "DATEPART"))
							{
								return false;
							}
						}
						else if (num != 3865452901U)
						{
							if (num != 4246149173U)
							{
								return false;
							}
							if (!(text == "DATALENGTH"))
							{
								return false;
							}
							goto IL_2FF;
						}
						else
						{
							if (!(text == "MONTH"))
							{
								return false;
							}
							goto IL_2FF;
						}
						string name = parameters[1].TypeUsage.EdmType.Name;
						return name.Equals("DateTimeOffset", StringComparison.OrdinalIgnoreCase) || name.Equals("Time", StringComparison.OrdinalIgnoreCase);
					}
					if (num <= 688247133U)
					{
						if (num <= 437886384U)
						{
							if (num != 239465655U)
							{
								if (num != 437886384U)
								{
									return false;
								}
								if (!(text == "SYSDATETIMEOFFSET"))
								{
									return false;
								}
								return true;
							}
							else if (!(text == "MIN"))
							{
								return false;
							}
						}
						else if (num != 475632249U)
						{
							if (num != 688247133U)
							{
								return false;
							}
							if (!(text == "DAY"))
							{
								return false;
							}
							goto IL_2FF;
						}
						else if (!(text == "MAX"))
						{
							return false;
						}
					}
					else
					{
						if (num > 999103698U)
						{
							if (num != 1674423462U)
							{
								if (num != 1900062112U)
								{
									return false;
								}
								if (!(text == "DATEADD"))
								{
									return false;
								}
							}
							else if (!(text == "DATEDIFF"))
							{
								return false;
							}
							string name2 = parameters[1].TypeUsage.EdmType.Name;
							string name3 = parameters[2].TypeUsage.EdmType.Name;
							return name2.Equals("Time", StringComparison.OrdinalIgnoreCase) || name3.Equals("Time", StringComparison.OrdinalIgnoreCase) || name2.Equals("DateTimeOffset", StringComparison.OrdinalIgnoreCase) || name3.Equals("DateTimeOffset", StringComparison.OrdinalIgnoreCase);
						}
						if (num != 719394705U)
						{
							if (num != 999103698U)
							{
								return false;
							}
							if (!(text == "CHECKSUM"))
							{
								return false;
							}
							goto IL_2FF;
						}
						else
						{
							if (!(text == "SYSUTCDATETIME"))
							{
								return false;
							}
							return true;
						}
					}
					IL_2BC:
					string name4 = ((CollectionType)parameters[0].TypeUsage.EdmType).TypeUsage.EdmType.Name;
					return name4.Equals("DateTimeOffset", StringComparison.OrdinalIgnoreCase) || name4.Equals("Time", StringComparison.OrdinalIgnoreCase);
					IL_2FF:
					string name5 = parameters[0].TypeUsage.EdmType.Name;
					return name5.Equals("DateTimeOffset", StringComparison.OrdinalIgnoreCase) || name5.Equals("Time", StringComparison.OrdinalIgnoreCase);
				}
			}
			return true;
		}

		// Token: 0x0600022D RID: 557 RVA: 0x00006940 File Offset: 0x00004B40
		private static bool IsYukonOrNewer(EdmFunction edmFunction)
		{
			ReadOnlyMetadataCollection<FunctionParameter> parameters = edmFunction.Parameters;
			if (parameters == null || parameters.Count == 0)
			{
				return false;
			}
			string a = edmFunction.Name.ToUpperInvariant();
			if (!(a == "COUNT") && !(a == "COUNT_BIG"))
			{
				if (a == "CHARINDEX")
				{
					foreach (FunctionParameter functionParameter in parameters)
					{
						if (functionParameter.TypeUsage.EdmType.Name.Equals("Int64", StringComparison.OrdinalIgnoreCase))
						{
							return true;
						}
					}
					return false;
				}
				return false;
			}
			string name = ((CollectionType)parameters[0].TypeUsage.EdmType).TypeUsage.EdmType.Name;
			return name.Equals("Guid", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x0600022E RID: 558 RVA: 0x00006A30 File Offset: 0x00004C30
		public override TypeUsage GetEdmType(TypeUsage storeType)
		{
			EntityUtil.CheckArgumentNull<TypeUsage>(storeType, "storeType");
			string text = storeType.EdmType.Name.ToLowerInvariant();
			if (!base.StoreTypeNameToEdmPrimitiveType.ContainsKey(text))
			{
				throw EntityUtil.Argument(Strings.ProviderDoesNotSupportType(text));
			}
			PrimitiveType primitiveType = base.StoreTypeNameToEdmPrimitiveType[text];
			int maxLength = 0;
			bool isUnicode = true;
			uint num = <PrivateImplementationDetails>.ComputeStringHash(text);
			PrimitiveTypeKind primitiveTypeKind;
			bool flag;
			bool isFixedLength;
			if (num > 2603927413U)
			{
				if (num <= 3437915536U)
				{
					if (num <= 3008443898U)
					{
						if (num <= 2823553821U)
						{
							if (num != 2797886853U)
							{
								if (num != 2823553821U)
								{
									goto IL_710;
								}
								if (!(text == "char"))
								{
									goto IL_710;
								}
								primitiveTypeKind = PrimitiveTypeKind.String;
								flag = !TypeHelpers.TryGetMaxLength(storeType, out maxLength);
								isUnicode = false;
								isFixedLength = true;
								goto IL_71C;
							}
							else if (!(text == "float"))
							{
								goto IL_710;
							}
						}
						else if (num != 2994984227U)
						{
							if (num != 3008443898U)
							{
								goto IL_710;
							}
							if (!(text == "image"))
							{
								goto IL_710;
							}
							goto IL_67E;
						}
						else
						{
							if (!(text == "timestamp"))
							{
								goto IL_710;
							}
							goto IL_68C;
						}
					}
					else if (num <= 3286697625U)
					{
						if (num != 3185987134U)
						{
							if (num != 3286697625U)
							{
								goto IL_710;
							}
							if (!(text == "geography"))
							{
								goto IL_710;
							}
							goto IL_5B9;
						}
						else
						{
							if (!(text == "text"))
							{
								goto IL_710;
							}
							goto IL_62C;
						}
					}
					else if (num != 3347933383U)
					{
						if (num != 3431564149U)
						{
							if (num != 3437915536U)
							{
								goto IL_710;
							}
							if (!(text == "datetime"))
							{
								goto IL_710;
							}
							goto IL_6D6;
						}
						else
						{
							if (!(text == "uniqueidentifier"))
							{
								goto IL_710;
							}
							goto IL_5B9;
						}
					}
					else
					{
						if (!(text == "varbinary"))
						{
							goto IL_710;
						}
						primitiveTypeKind = PrimitiveTypeKind.Binary;
						flag = !TypeHelpers.TryGetMaxLength(storeType, out maxLength);
						isFixedLength = false;
						goto IL_71C;
					}
				}
				else if (num <= 3664801462U)
				{
					if (num <= 3604983901U)
					{
						if (num != 3564297305U)
						{
							if (num != 3604983901U)
							{
								goto IL_710;
							}
							if (!(text == "real"))
							{
								goto IL_710;
							}
						}
						else
						{
							if (!(text == "date"))
							{
								goto IL_710;
							}
							return TypeUsage.CreateDefaultTypeUsage(primitiveType);
						}
					}
					else if (num != 3659634113U)
					{
						if (num != 3664801462U)
						{
							goto IL_710;
						}
						if (!(text == "xml"))
						{
							goto IL_710;
						}
						goto IL_63D;
					}
					else
					{
						if (!(text == "smalldatetime"))
						{
							goto IL_710;
						}
						goto IL_6D6;
					}
				}
				else if (num <= 3761451113U)
				{
					if (num != 3716508924U)
					{
						if (num != 3761451113U)
						{
							goto IL_710;
						}
						if (!(text == "nchar"))
						{
							goto IL_710;
						}
						primitiveTypeKind = PrimitiveTypeKind.String;
						flag = !TypeHelpers.TryGetMaxLength(storeType, out maxLength);
						isUnicode = true;
						isFixedLength = true;
						goto IL_71C;
					}
					else
					{
						if (!(text == "binary"))
						{
							goto IL_710;
						}
						primitiveTypeKind = PrimitiveTypeKind.Binary;
						flag = !TypeHelpers.TryGetMaxLength(storeType, out maxLength);
						isFixedLength = true;
						goto IL_71C;
					}
				}
				else if (num != 3780168015U)
				{
					if (num != 3918255874U)
					{
						if (num != 4163743794U)
						{
							goto IL_710;
						}
						if (!(text == "varchar"))
						{
							goto IL_710;
						}
						primitiveTypeKind = PrimitiveTypeKind.String;
						flag = !TypeHelpers.TryGetMaxLength(storeType, out maxLength);
						isUnicode = false;
						isFixedLength = false;
						goto IL_71C;
					}
					else
					{
						if (!(text == "ntext"))
						{
							goto IL_710;
						}
						goto IL_63D;
					}
				}
				else
				{
					if (!(text == "money"))
					{
						goto IL_710;
					}
					return TypeUsage.CreateDecimalTypeUsage(primitiveType, 19, 4);
				}
				return TypeUsage.CreateDefaultTypeUsage(primitiveType);
			}
			if (num <= 1539863742U)
			{
				if (num <= 750634308U)
				{
					if (num <= 520654156U)
					{
						if (num != 132678327U)
						{
							if (num != 520654156U)
							{
								goto IL_710;
							}
							if (!(text == "decimal"))
							{
								goto IL_710;
							}
						}
						else
						{
							if (!(text == "nvarchar(max)"))
							{
								goto IL_710;
							}
							goto IL_63D;
						}
					}
					else if (num != 711820689U)
					{
						if (num != 750634308U)
						{
							goto IL_710;
						}
						if (!(text == "tinyint"))
						{
							goto IL_710;
						}
						goto IL_5B9;
					}
					else
					{
						if (!(text == "geometry"))
						{
							goto IL_710;
						}
						goto IL_5B9;
					}
				}
				else if (num <= 956906072U)
				{
					if (num != 923440646U)
					{
						if (num != 956906072U)
						{
							goto IL_710;
						}
						if (!(text == "smallmoney"))
						{
							goto IL_710;
						}
						return TypeUsage.CreateDecimalTypeUsage(primitiveType, 10, 4);
					}
					else
					{
						if (!(text == "datetime2"))
						{
							goto IL_710;
						}
						goto IL_6D6;
					}
				}
				else if (num != 1498571224U)
				{
					if (num != 1539863742U)
					{
						goto IL_710;
					}
					if (!(text == "nvarchar"))
					{
						goto IL_710;
					}
					primitiveTypeKind = PrimitiveTypeKind.String;
					flag = !TypeHelpers.TryGetMaxLength(storeType, out maxLength);
					isUnicode = true;
					isFixedLength = false;
					goto IL_71C;
				}
				else
				{
					if (!(text == "varbinary(max)"))
					{
						goto IL_710;
					}
					goto IL_67E;
				}
			}
			else if (num <= 1762504443U)
			{
				if (num <= 1623908912U)
				{
					if (num != 1564253156U)
					{
						if (num != 1623908912U)
						{
							goto IL_710;
						}
						if (!(text == "bit"))
						{
							goto IL_710;
						}
						goto IL_5B9;
					}
					else
					{
						if (!(text == "time"))
						{
							goto IL_710;
						}
						return TypeUsage.CreateTimeTypeUsage(primitiveType, null);
					}
				}
				else if (num != 1761125480U)
				{
					if (num != 1762504443U)
					{
						goto IL_710;
					}
					if (!(text == "varchar(max)"))
					{
						goto IL_710;
					}
					goto IL_62C;
				}
				else if (!(text == "numeric"))
				{
					goto IL_710;
				}
			}
			else if (num <= 2322048458U)
			{
				if (num != 2174562837U)
				{
					if (num != 2322048458U)
					{
						goto IL_710;
					}
					if (!(text == "bigint"))
					{
						goto IL_710;
					}
					goto IL_5B9;
				}
				else
				{
					if (!(text == "smallint"))
					{
						goto IL_710;
					}
					goto IL_5B9;
				}
			}
			else if (num != 2336348659U)
			{
				if (num != 2515107422U)
				{
					if (num != 2603927413U)
					{
						goto IL_710;
					}
					if (!(text == "rowversion"))
					{
						goto IL_710;
					}
					goto IL_68C;
				}
				else
				{
					if (!(text == "int"))
					{
						goto IL_710;
					}
					goto IL_5B9;
				}
			}
			else
			{
				if (!(text == "datetimeoffset"))
				{
					goto IL_710;
				}
				return TypeUsage.CreateDateTimeOffsetTypeUsage(primitiveType, null);
			}
			byte precision;
			byte scale;
			if (TypeHelpers.TryGetPrecision(storeType, out precision) && TypeHelpers.TryGetScale(storeType, out scale))
			{
				return TypeUsage.CreateDecimalTypeUsage(primitiveType, precision, scale);
			}
			return TypeUsage.CreateDecimalTypeUsage(primitiveType);
			IL_5B9:
			return TypeUsage.CreateDefaultTypeUsage(primitiveType);
			IL_62C:
			primitiveTypeKind = PrimitiveTypeKind.String;
			flag = true;
			isUnicode = false;
			isFixedLength = false;
			goto IL_71C;
			IL_63D:
			primitiveTypeKind = PrimitiveTypeKind.String;
			flag = true;
			isUnicode = true;
			isFixedLength = false;
			goto IL_71C;
			IL_67E:
			primitiveTypeKind = PrimitiveTypeKind.Binary;
			flag = true;
			isFixedLength = false;
			goto IL_71C;
			IL_68C:
			return TypeUsage.CreateBinaryTypeUsage(primitiveType, true, 8);
			IL_6D6:
			return TypeUsage.CreateDateTimeTypeUsage(primitiveType, null);
			IL_710:
			throw EntityUtil.NotSupported(Strings.ProviderDoesNotSupportType(text));
			IL_71C:
			if (primitiveTypeKind != PrimitiveTypeKind.Binary)
			{
				if (primitiveTypeKind != PrimitiveTypeKind.String)
				{
					throw EntityUtil.NotSupported(Strings.ProviderDoesNotSupportType(text));
				}
				if (!flag)
				{
					return TypeUsage.CreateStringTypeUsage(primitiveType, isUnicode, isFixedLength, maxLength);
				}
				return TypeUsage.CreateStringTypeUsage(primitiveType, isUnicode, isFixedLength);
			}
			else
			{
				if (!flag)
				{
					return TypeUsage.CreateBinaryTypeUsage(primitiveType, isFixedLength, maxLength);
				}
				return TypeUsage.CreateBinaryTypeUsage(primitiveType, isFixedLength);
			}
		}

		// Token: 0x0600022F RID: 559 RVA: 0x000071A0 File Offset: 0x000053A0
		public override TypeUsage GetStoreType(TypeUsage edmType)
		{
			EntityUtil.CheckArgumentNull<TypeUsage>(edmType, "edmType");
			PrimitiveType primitiveType = edmType.EdmType as PrimitiveType;
			if (primitiveType == null)
			{
				throw EntityUtil.Argument(Strings.ProviderDoesNotSupportType(edmType.Identity));
			}
			ReadOnlyMetadataCollection<Facet> facets = edmType.Facets;
			switch (primitiveType.PrimitiveTypeKind)
			{
			case PrimitiveTypeKind.Binary:
			{
				bool flag = facets["FixedLength"].Value != null && (bool)facets["FixedLength"].Value;
				Facet facet = facets["MaxLength"];
				bool flag2 = Helper.IsUnboundedFacetValue(facet) || facet.Value == null || (int)facet.Value > 8000;
				int num = (!flag2) ? ((int)facet.Value) : int.MinValue;
				TypeUsage result;
				if (flag)
				{
					result = TypeUsage.CreateBinaryTypeUsage(base.StoreTypeNameToStorePrimitiveType["binary"], true, flag2 ? 8000 : num);
				}
				else if (flag2)
				{
					if (this._version != SqlVersion.Sql8)
					{
						result = TypeUsage.CreateBinaryTypeUsage(base.StoreTypeNameToStorePrimitiveType["varbinary(max)"], false);
					}
					else
					{
						result = TypeUsage.CreateBinaryTypeUsage(base.StoreTypeNameToStorePrimitiveType["varbinary"], false, 8000);
					}
				}
				else
				{
					result = TypeUsage.CreateBinaryTypeUsage(base.StoreTypeNameToStorePrimitiveType["varbinary"], false, num);
				}
				return result;
			}
			case PrimitiveTypeKind.Boolean:
				return TypeUsage.CreateDefaultTypeUsage(base.StoreTypeNameToStorePrimitiveType["bit"]);
			case PrimitiveTypeKind.Byte:
				return TypeUsage.CreateDefaultTypeUsage(base.StoreTypeNameToStorePrimitiveType["tinyint"]);
			case PrimitiveTypeKind.DateTime:
				return TypeUsage.CreateDefaultTypeUsage(base.StoreTypeNameToStorePrimitiveType["datetime"]);
			case PrimitiveTypeKind.Decimal:
			{
				byte precision;
				if (!TypeHelpers.TryGetPrecision(edmType, out precision))
				{
					precision = 18;
				}
				byte scale;
				if (!TypeHelpers.TryGetScale(edmType, out scale))
				{
					scale = 0;
				}
				return TypeUsage.CreateDecimalTypeUsage(base.StoreTypeNameToStorePrimitiveType["decimal"], precision, scale);
			}
			case PrimitiveTypeKind.Double:
				return TypeUsage.CreateDefaultTypeUsage(base.StoreTypeNameToStorePrimitiveType["float"]);
			case PrimitiveTypeKind.Guid:
				return TypeUsage.CreateDefaultTypeUsage(base.StoreTypeNameToStorePrimitiveType["uniqueidentifier"]);
			case PrimitiveTypeKind.Single:
				return TypeUsage.CreateDefaultTypeUsage(base.StoreTypeNameToStorePrimitiveType["real"]);
			case PrimitiveTypeKind.Int16:
				return TypeUsage.CreateDefaultTypeUsage(base.StoreTypeNameToStorePrimitiveType["smallint"]);
			case PrimitiveTypeKind.Int32:
				return TypeUsage.CreateDefaultTypeUsage(base.StoreTypeNameToStorePrimitiveType["int"]);
			case PrimitiveTypeKind.Int64:
				return TypeUsage.CreateDefaultTypeUsage(base.StoreTypeNameToStorePrimitiveType["bigint"]);
			case PrimitiveTypeKind.String:
			{
				bool flag3 = facets["Unicode"].Value == null || (bool)facets["Unicode"].Value;
				bool flag4 = facets["FixedLength"].Value != null && (bool)facets["FixedLength"].Value;
				Facet facet2 = facets["MaxLength"];
				bool flag5 = Helper.IsUnboundedFacetValue(facet2) || facet2.Value == null || (int)facet2.Value > (flag3 ? 4000 : 8000);
				int num2 = (!flag5) ? ((int)facet2.Value) : int.MinValue;
				TypeUsage result2;
				if (flag3)
				{
					if (flag4)
					{
						result2 = TypeUsage.CreateStringTypeUsage(base.StoreTypeNameToStorePrimitiveType["nchar"], true, true, flag5 ? 4000 : num2);
					}
					else if (flag5)
					{
						if (this._version != SqlVersion.Sql8)
						{
							result2 = TypeUsage.CreateStringTypeUsage(base.StoreTypeNameToStorePrimitiveType["nvarchar(max)"], true, false);
						}
						else
						{
							result2 = TypeUsage.CreateStringTypeUsage(base.StoreTypeNameToStorePrimitiveType["nvarchar"], true, false, 4000);
						}
					}
					else
					{
						result2 = TypeUsage.CreateStringTypeUsage(base.StoreTypeNameToStorePrimitiveType["nvarchar"], true, false, num2);
					}
				}
				else if (flag4)
				{
					result2 = TypeUsage.CreateStringTypeUsage(base.StoreTypeNameToStorePrimitiveType["char"], false, true, flag5 ? 8000 : num2);
				}
				else if (flag5)
				{
					if (this._version != SqlVersion.Sql8)
					{
						result2 = TypeUsage.CreateStringTypeUsage(base.StoreTypeNameToStorePrimitiveType["varchar(max)"], false, false);
					}
					else
					{
						result2 = TypeUsage.CreateStringTypeUsage(base.StoreTypeNameToStorePrimitiveType["varchar"], false, false, 8000);
					}
				}
				else
				{
					result2 = TypeUsage.CreateStringTypeUsage(base.StoreTypeNameToStorePrimitiveType["varchar"], false, false, num2);
				}
				return result2;
			}
			case PrimitiveTypeKind.Time:
				return this.GetStorePrimitiveTypeIfPostSql9("time", edmType.Identity, primitiveType.PrimitiveTypeKind);
			case PrimitiveTypeKind.DateTimeOffset:
				return this.GetStorePrimitiveTypeIfPostSql9("datetimeoffset", edmType.Identity, primitiveType.PrimitiveTypeKind);
			case PrimitiveTypeKind.Geometry:
			case PrimitiveTypeKind.GeometryPoint:
			case PrimitiveTypeKind.GeometryLineString:
			case PrimitiveTypeKind.GeometryPolygon:
			case PrimitiveTypeKind.GeometryMultiPoint:
			case PrimitiveTypeKind.GeometryMultiLineString:
			case PrimitiveTypeKind.GeometryMultiPolygon:
			case PrimitiveTypeKind.GeometryCollection:
				return this.GetStorePrimitiveTypeIfPostSql9("geometry", edmType.Identity, primitiveType.PrimitiveTypeKind);
			case PrimitiveTypeKind.Geography:
			case PrimitiveTypeKind.GeographyPoint:
			case PrimitiveTypeKind.GeographyLineString:
			case PrimitiveTypeKind.GeographyPolygon:
			case PrimitiveTypeKind.GeographyMultiPoint:
			case PrimitiveTypeKind.GeographyMultiLineString:
			case PrimitiveTypeKind.GeographyMultiPolygon:
			case PrimitiveTypeKind.GeographyCollection:
				return this.GetStorePrimitiveTypeIfPostSql9("geography", edmType.Identity, primitiveType.PrimitiveTypeKind);
			}
			throw EntityUtil.NotSupported(Strings.NoStoreTypeForEdmType(edmType.Identity, primitiveType.PrimitiveTypeKind));
		}

		// Token: 0x06000230 RID: 560 RVA: 0x000076DC File Offset: 0x000058DC
		private TypeUsage GetStorePrimitiveTypeIfPostSql9(string storeTypeName, string edmTypeIdentity, PrimitiveTypeKind primitiveTypeKind)
		{
			if (this.SqlVersion != SqlVersion.Sql8 && this.SqlVersion != SqlVersion.Sql9)
			{
				return TypeUsage.CreateDefaultTypeUsage(base.StoreTypeNameToStorePrimitiveType[storeTypeName]);
			}
			throw EntityUtil.NotSupported(Strings.NoStoreTypeForEdmType(edmTypeIdentity, primitiveTypeKind));
		}

		// Token: 0x06000231 RID: 561 RVA: 0x00007715 File Offset: 0x00005915
		public override bool SupportsEscapingLikeArgument(out char escapeCharacter)
		{
			escapeCharacter = '~';
			return true;
		}

		// Token: 0x06000232 RID: 562 RVA: 0x0000771C File Offset: 0x0000591C
		public override string EscapeLikeArgument(string argument)
		{
			EntityUtil.CheckArgumentNull<string>(argument, "argument");
			bool flag;
			return SqlProviderManifest.EscapeLikeText(argument, true, out flag);
		}

		// Token: 0x04000647 RID: 1607
		internal const string TokenSql8 = "2000";

		// Token: 0x04000648 RID: 1608
		internal const string TokenSql9 = "2005";

		// Token: 0x04000649 RID: 1609
		internal const string TokenSql10 = "2008";

		// Token: 0x0400064A RID: 1610
		internal const char LikeEscapeChar = '~';

		// Token: 0x0400064B RID: 1611
		internal const string LikeEscapeCharToString = "~";

		// Token: 0x0400064C RID: 1612
		private SqlVersion _version = SqlVersion.Sql9;

		// Token: 0x0400064D RID: 1613
		private const int varcharMaxSize = 8000;

		// Token: 0x0400064E RID: 1614
		private const int nvarcharMaxSize = 4000;

		// Token: 0x0400064F RID: 1615
		private const int binaryMaxSize = 8000;

		// Token: 0x04000650 RID: 1616
		private ReadOnlyCollection<PrimitiveType> _primitiveTypes;

		// Token: 0x04000651 RID: 1617
		private ReadOnlyCollection<EdmFunction> _functions;
	}
}
