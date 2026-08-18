using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.SqlServer.Resources;
using System.Data.Entity.SqlServer.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Xml;

namespace System.Data.Entity.SqlServer
{
	// Token: 0x02000042 RID: 66
	internal class SqlProviderManifest : DbXmlEnabledProviderManifest
	{
		// Token: 0x06000445 RID: 1093 RVA: 0x00014741 File Offset: 0x00012941
		public SqlProviderManifest(string manifestToken) : base(SqlProviderManifest.GetProviderManifest())
		{
			this._version = SqlVersionUtils.GetSqlVersion(manifestToken);
			this.Initialize();
		}

		// Token: 0x06000446 RID: 1094 RVA: 0x00014828 File Offset: 0x00012A28
		private void Initialize()
		{
			if (this._version == SqlVersion.Sql10 || this._version == SqlVersion.Sql11)
			{
				this._primitiveTypes = base.GetStoreTypes();
				this._functions = base.GetStoreFunctions();
				return;
			}
			List<PrimitiveType> list = new List<PrimitiveType>(base.GetStoreTypes());
			list.RemoveAll((PrimitiveType primitiveType) => primitiveType.Name.Equals("time", StringComparison.OrdinalIgnoreCase) || primitiveType.Name.Equals("date", StringComparison.OrdinalIgnoreCase) || primitiveType.Name.Equals("datetime2", StringComparison.OrdinalIgnoreCase) || primitiveType.Name.Equals("datetimeoffset", StringComparison.OrdinalIgnoreCase) || primitiveType.Name.Equals("geography", StringComparison.OrdinalIgnoreCase) || primitiveType.Name.Equals("geometry", StringComparison.OrdinalIgnoreCase));
			if (this._version == SqlVersion.Sql8)
			{
				list.RemoveAll((PrimitiveType primitiveType) => primitiveType.Name.Equals("xml", StringComparison.OrdinalIgnoreCase) || primitiveType.Name.EndsWith("(max)", StringComparison.OrdinalIgnoreCase));
			}
			this._primitiveTypes = new ReadOnlyCollection<PrimitiveType>(list);
			IEnumerable<EdmFunction> source = from f in base.GetStoreFunctions()
			where !SqlProviderManifest.IsKatmaiOrNewer(f)
			select f;
			if (this._version == SqlVersion.Sql8)
			{
				source = from f in source
				where !SqlProviderManifest.IsYukonOrNewer(f)
				select f;
			}
			this._functions = new ReadOnlyCollection<EdmFunction>(source.ToList<EdmFunction>());
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000447 RID: 1095 RVA: 0x00014934 File Offset: 0x00012B34
		internal SqlVersion SqlVersion
		{
			get
			{
				return this._version;
			}
		}

		// Token: 0x06000448 RID: 1096 RVA: 0x0001493C File Offset: 0x00012B3C
		private static XmlReader GetXmlResource(string resourceName)
		{
			return XmlReader.Create(typeof(SqlProviderManifest).Assembly().GetManifestResourceStream(resourceName));
		}

		// Token: 0x06000449 RID: 1097 RVA: 0x00014958 File Offset: 0x00012B58
		internal static XmlReader GetProviderManifest()
		{
			return SqlProviderManifest.GetXmlResource("System.Data.Resources.SqlClient.SqlProviderServices.ProviderManifest.xml");
		}

		// Token: 0x0600044A RID: 1098 RVA: 0x00014964 File Offset: 0x00012B64
		internal static XmlReader GetStoreSchemaMapping(string mslName)
		{
			return SqlProviderManifest.GetXmlResource("System.Data.Resources.SqlClient.SqlProviderServices." + mslName + ".msl");
		}

		// Token: 0x0600044B RID: 1099 RVA: 0x0001497B File Offset: 0x00012B7B
		internal XmlReader GetStoreSchemaDescription(string ssdlName)
		{
			if (this._version == SqlVersion.Sql8)
			{
				return SqlProviderManifest.GetXmlResource("System.Data.Resources.SqlClient.SqlProviderServices." + ssdlName + "_Sql8.ssdl");
			}
			return SqlProviderManifest.GetXmlResource("System.Data.Resources.SqlClient.SqlProviderServices." + ssdlName + ".ssdl");
		}

		// Token: 0x0600044C RID: 1100 RVA: 0x000149B4 File Offset: 0x00012BB4
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

		// Token: 0x0600044D RID: 1101 RVA: 0x00014A64 File Offset: 0x00012C64
		protected override XmlReader GetDbInformation(string informationType)
		{
			if (informationType == "StoreSchemaDefinitionVersion3" || informationType == "StoreSchemaDefinition")
			{
				return this.GetStoreSchemaDescription(informationType);
			}
			if (informationType == "StoreSchemaMappingVersion3" || informationType == "StoreSchemaMapping")
			{
				return SqlProviderManifest.GetStoreSchemaMapping(informationType);
			}
			if (informationType == "ConceptualSchemaDefinitionVersion3" || informationType == "ConceptualSchemaDefinition")
			{
				return null;
			}
			throw new ProviderIncompatibleException(Strings.ProviderReturnedNullForGetDbInformation(informationType));
		}

		// Token: 0x0600044E RID: 1102 RVA: 0x00014ADB File Offset: 0x00012CDB
		public override ReadOnlyCollection<PrimitiveType> GetStoreTypes()
		{
			return this._primitiveTypes;
		}

		// Token: 0x0600044F RID: 1103 RVA: 0x00014AE3 File Offset: 0x00012CE3
		public override ReadOnlyCollection<EdmFunction> GetStoreFunctions()
		{
			return this._functions;
		}

		// Token: 0x06000450 RID: 1104 RVA: 0x00014AF8 File Offset: 0x00012CF8
		[SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
		private static bool IsKatmaiOrNewer(EdmFunction edmFunction)
		{
			if (edmFunction.ReturnParameter == null || !edmFunction.ReturnParameter.TypeUsage.IsSpatialType())
			{
				if (!edmFunction.Parameters.Any((FunctionParameter p) => p.TypeUsage.IsSpatialType()))
				{
					ReadOnlyMetadataCollection<FunctionParameter> parameters = edmFunction.Parameters;
					string key;
					switch (key = edmFunction.Name.ToUpperInvariant())
					{
					case "COUNT":
					case "COUNT_BIG":
					case "MAX":
					case "MIN":
					{
						string name = ((CollectionType)parameters[0].TypeUsage.EdmType).TypeUsage.EdmType.Name;
						return name.Equals("DateTimeOffset", StringComparison.OrdinalIgnoreCase) || name.Equals("Time", StringComparison.OrdinalIgnoreCase);
					}
					case "DAY":
					case "MONTH":
					case "YEAR":
					case "DATALENGTH":
					case "CHECKSUM":
					{
						string name2 = parameters[0].TypeUsage.EdmType.Name;
						return name2.Equals("DateTimeOffset", StringComparison.OrdinalIgnoreCase) || name2.Equals("Time", StringComparison.OrdinalIgnoreCase);
					}
					case "DATEADD":
					case "DATEDIFF":
					{
						string name3 = parameters[1].TypeUsage.EdmType.Name;
						string name4 = parameters[2].TypeUsage.EdmType.Name;
						return name3.Equals("Time", StringComparison.OrdinalIgnoreCase) || name4.Equals("Time", StringComparison.OrdinalIgnoreCase) || name3.Equals("DateTimeOffset", StringComparison.OrdinalIgnoreCase) || name4.Equals("DateTimeOffset", StringComparison.OrdinalIgnoreCase);
					}
					case "DATENAME":
					case "DATEPART":
					{
						string name5 = parameters[1].TypeUsage.EdmType.Name;
						return name5.Equals("DateTimeOffset", StringComparison.OrdinalIgnoreCase) || name5.Equals("Time", StringComparison.OrdinalIgnoreCase);
					}
					case "SYSUTCDATETIME":
					case "SYSDATETIME":
					case "SYSDATETIMEOFFSET":
						return true;
					}
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000451 RID: 1105 RVA: 0x00014DC4 File Offset: 0x00012FC4
		private static bool IsYukonOrNewer(EdmFunction edmFunction)
		{
			ReadOnlyMetadataCollection<FunctionParameter> parameters = edmFunction.Parameters;
			if (parameters == null || parameters.Count == 0)
			{
				return false;
			}
			string a;
			if ((a = edmFunction.Name.ToUpperInvariant()) != null)
			{
				if (a == "COUNT" || a == "COUNT_BIG")
				{
					string name = ((CollectionType)parameters[0].TypeUsage.EdmType).TypeUsage.EdmType.Name;
					return name.Equals("Guid", StringComparison.OrdinalIgnoreCase);
				}
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
			}
			return false;
		}

		// Token: 0x06000452 RID: 1106 RVA: 0x00014EB8 File Offset: 0x000130B8
		[SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
		[SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase")]
		public override TypeUsage GetEdmType(TypeUsage storeType)
		{
			Check.NotNull<TypeUsage>(storeType, "storeType");
			string text = storeType.EdmType.Name.ToLowerInvariant();
			if (!base.StoreTypeNameToEdmPrimitiveType.ContainsKey(text))
			{
				throw new ArgumentException(Strings.ProviderDoesNotSupportType(text));
			}
			PrimitiveType primitiveType = base.StoreTypeNameToEdmPrimitiveType[text];
			int maxLength = 0;
			bool isUnicode = true;
			string key;
			if ((key = text) != null)
			{
				if (<PrivateImplementationDetails>{0025BC3E-2252-4BA9-A352-D7F62FAA5B3F}.$$method0x60003f9-1 == null)
				{
					<PrivateImplementationDetails>{0025BC3E-2252-4BA9-A352-D7F62FAA5B3F}.$$method0x60003f9-1 = new Dictionary<string, int>(35)
					{
						{
							"tinyint",
							0
						},
						{
							"smallint",
							1
						},
						{
							"bigint",
							2
						},
						{
							"bit",
							3
						},
						{
							"uniqueidentifier",
							4
						},
						{
							"int",
							5
						},
						{
							"geography",
							6
						},
						{
							"geometry",
							7
						},
						{
							"varchar",
							8
						},
						{
							"char",
							9
						},
						{
							"nvarchar",
							10
						},
						{
							"nchar",
							11
						},
						{
							"varchar(max)",
							12
						},
						{
							"text",
							13
						},
						{
							"nvarchar(max)",
							14
						},
						{
							"ntext",
							15
						},
						{
							"xml",
							16
						},
						{
							"binary",
							17
						},
						{
							"varbinary",
							18
						},
						{
							"varbinary(max)",
							19
						},
						{
							"image",
							20
						},
						{
							"timestamp",
							21
						},
						{
							"rowversion",
							22
						},
						{
							"float",
							23
						},
						{
							"real",
							24
						},
						{
							"decimal",
							25
						},
						{
							"numeric",
							26
						},
						{
							"money",
							27
						},
						{
							"smallmoney",
							28
						},
						{
							"datetime",
							29
						},
						{
							"datetime2",
							30
						},
						{
							"smalldatetime",
							31
						},
						{
							"date",
							32
						},
						{
							"time",
							33
						},
						{
							"datetimeoffset",
							34
						}
					};
				}
				int num;
				if (<PrivateImplementationDetails>{0025BC3E-2252-4BA9-A352-D7F62FAA5B3F}.$$method0x60003f9-1.TryGetValue(key, out num))
				{
					PrimitiveTypeKind primitiveTypeKind;
					bool flag;
					bool isFixedLength;
					switch (num)
					{
					case 0:
					case 1:
					case 2:
					case 3:
					case 4:
					case 5:
					case 6:
					case 7:
						return TypeUsage.CreateDefaultTypeUsage(primitiveType);
					case 8:
						primitiveTypeKind = PrimitiveTypeKind.String;
						flag = !storeType.TryGetMaxLength(out maxLength);
						isUnicode = false;
						isFixedLength = false;
						break;
					case 9:
						primitiveTypeKind = PrimitiveTypeKind.String;
						flag = !storeType.TryGetMaxLength(out maxLength);
						isUnicode = false;
						isFixedLength = true;
						break;
					case 10:
						primitiveTypeKind = PrimitiveTypeKind.String;
						flag = !storeType.TryGetMaxLength(out maxLength);
						isUnicode = true;
						isFixedLength = false;
						break;
					case 11:
						primitiveTypeKind = PrimitiveTypeKind.String;
						flag = !storeType.TryGetMaxLength(out maxLength);
						isUnicode = true;
						isFixedLength = true;
						break;
					case 12:
					case 13:
						primitiveTypeKind = PrimitiveTypeKind.String;
						flag = true;
						isUnicode = false;
						isFixedLength = false;
						break;
					case 14:
					case 15:
					case 16:
						primitiveTypeKind = PrimitiveTypeKind.String;
						flag = true;
						isUnicode = true;
						isFixedLength = false;
						break;
					case 17:
						primitiveTypeKind = PrimitiveTypeKind.Binary;
						flag = !storeType.TryGetMaxLength(out maxLength);
						isFixedLength = true;
						break;
					case 18:
						primitiveTypeKind = PrimitiveTypeKind.Binary;
						flag = !storeType.TryGetMaxLength(out maxLength);
						isFixedLength = false;
						break;
					case 19:
					case 20:
						primitiveTypeKind = PrimitiveTypeKind.Binary;
						flag = true;
						isFixedLength = false;
						break;
					case 21:
					case 22:
						return TypeUsage.CreateBinaryTypeUsage(primitiveType, true, 8);
					case 23:
					case 24:
						return TypeUsage.CreateDefaultTypeUsage(primitiveType);
					case 25:
					case 26:
					{
						byte precision;
						byte scale;
						if (storeType.TryGetPrecision(out precision) && storeType.TryGetScale(out scale))
						{
							return TypeUsage.CreateDecimalTypeUsage(primitiveType, precision, scale);
						}
						return TypeUsage.CreateDecimalTypeUsage(primitiveType);
					}
					case 27:
						return TypeUsage.CreateDecimalTypeUsage(primitiveType, 19, 4);
					case 28:
						return TypeUsage.CreateDecimalTypeUsage(primitiveType, 10, 4);
					case 29:
					case 30:
					case 31:
						return TypeUsage.CreateDateTimeTypeUsage(primitiveType, null);
					case 32:
						return TypeUsage.CreateDefaultTypeUsage(primitiveType);
					case 33:
						return TypeUsage.CreateTimeTypeUsage(primitiveType, null);
					case 34:
						return TypeUsage.CreateDateTimeOffsetTypeUsage(primitiveType, null);
					default:
						goto IL_433;
					}
					PrimitiveTypeKind primitiveTypeKind2 = primitiveTypeKind;
					if (primitiveTypeKind2 != PrimitiveTypeKind.Binary)
					{
						if (primitiveTypeKind2 != PrimitiveTypeKind.String)
						{
							throw new NotSupportedException(Strings.ProviderDoesNotSupportType(text));
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
			}
			IL_433:
			throw new NotSupportedException(Strings.ProviderDoesNotSupportType(text));
		}

		// Token: 0x06000453 RID: 1107 RVA: 0x00015350 File Offset: 0x00013550
		[SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
		public override TypeUsage GetStoreType(TypeUsage edmType)
		{
			Check.NotNull<TypeUsage>(edmType, "edmType");
			PrimitiveType primitiveType = edmType.EdmType as PrimitiveType;
			if (primitiveType == null)
			{
				throw new ArgumentException(Strings.ProviderDoesNotSupportType(edmType.EdmType.Name));
			}
			ReadOnlyMetadataCollection<Facet> facets = edmType.Facets;
			switch (primitiveType.PrimitiveTypeKind)
			{
			case PrimitiveTypeKind.Binary:
			{
				bool flag = facets["FixedLength"].Value != null && (bool)facets["FixedLength"].Value;
				Facet facet = facets["MaxLength"];
				bool flag2 = facet.IsUnbounded || facet.Value == null || (int)facet.Value > 8000;
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
				if (!edmType.TryGetPrecision(out precision))
				{
					precision = 18;
				}
				byte scale;
				if (!edmType.TryGetScale(out scale))
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
				bool flag5 = facet2.IsUnbounded || facet2.Value == null || (int)facet2.Value > (flag3 ? 4000 : 8000);
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
				return this.GetStorePrimitiveTypeIfPostSql9("time", edmType.EdmType.Name, primitiveType.PrimitiveTypeKind);
			case PrimitiveTypeKind.DateTimeOffset:
				return this.GetStorePrimitiveTypeIfPostSql9("datetimeoffset", edmType.EdmType.Name, primitiveType.PrimitiveTypeKind);
			case PrimitiveTypeKind.Geometry:
			case PrimitiveTypeKind.GeometryPoint:
			case PrimitiveTypeKind.GeometryLineString:
			case PrimitiveTypeKind.GeometryPolygon:
			case PrimitiveTypeKind.GeometryMultiPoint:
			case PrimitiveTypeKind.GeometryMultiLineString:
			case PrimitiveTypeKind.GeometryMultiPolygon:
			case PrimitiveTypeKind.GeometryCollection:
				return this.GetStorePrimitiveTypeIfPostSql9("geometry", edmType.EdmType.Name, primitiveType.PrimitiveTypeKind);
			case PrimitiveTypeKind.Geography:
			case PrimitiveTypeKind.GeographyPoint:
			case PrimitiveTypeKind.GeographyLineString:
			case PrimitiveTypeKind.GeographyPolygon:
			case PrimitiveTypeKind.GeographyMultiPoint:
			case PrimitiveTypeKind.GeographyMultiLineString:
			case PrimitiveTypeKind.GeographyMultiPolygon:
			case PrimitiveTypeKind.GeographyCollection:
				return this.GetStorePrimitiveTypeIfPostSql9("geography", edmType.EdmType.Name, primitiveType.PrimitiveTypeKind);
			}
			throw new NotSupportedException(Strings.NoStoreTypeForEdmType(edmType.EdmType.Name, primitiveType.PrimitiveTypeKind));
		}

		// Token: 0x06000454 RID: 1108 RVA: 0x000158AA File Offset: 0x00013AAA
		private TypeUsage GetStorePrimitiveTypeIfPostSql9(string storeTypeName, string nameForException, PrimitiveTypeKind primitiveTypeKind)
		{
			if (this.SqlVersion != SqlVersion.Sql8 && this.SqlVersion != SqlVersion.Sql9)
			{
				return TypeUsage.CreateDefaultTypeUsage(base.StoreTypeNameToStorePrimitiveType[storeTypeName]);
			}
			throw new NotSupportedException(Strings.NoStoreTypeForEdmType(nameForException, primitiveTypeKind));
		}

		// Token: 0x06000455 RID: 1109 RVA: 0x000158E3 File Offset: 0x00013AE3
		public override bool SupportsEscapingLikeArgument(out char escapeCharacter)
		{
			escapeCharacter = '~';
			return true;
		}

		// Token: 0x06000456 RID: 1110 RVA: 0x000158EC File Offset: 0x00013AEC
		public override string EscapeLikeArgument(string argument)
		{
			Check.NotNull<string>(argument, "argument");
			bool flag;
			return SqlProviderManifest.EscapeLikeText(argument, true, out flag);
		}

		// Token: 0x06000457 RID: 1111 RVA: 0x0001590E File Offset: 0x00013B0E
		public override bool SupportsInExpression()
		{
			return true;
		}

		// Token: 0x06000458 RID: 1112 RVA: 0x00015911 File Offset: 0x00013B11
		public override bool SupportsIntersectAndUnionAllFlattening()
		{
			return true;
		}

		// Token: 0x040000F9 RID: 249
		internal const string TokenSql8 = "2000";

		// Token: 0x040000FA RID: 250
		internal const string TokenSql9 = "2005";

		// Token: 0x040000FB RID: 251
		internal const string TokenSql10 = "2008";

		// Token: 0x040000FC RID: 252
		internal const string TokenSql11 = "2012";

		// Token: 0x040000FD RID: 253
		internal const string TokenAzure11 = "2012.Azure";

		// Token: 0x040000FE RID: 254
		internal const char LikeEscapeChar = '~';

		// Token: 0x040000FF RID: 255
		internal const string LikeEscapeCharToString = "~";

		// Token: 0x04000100 RID: 256
		private const int varcharMaxSize = 8000;

		// Token: 0x04000101 RID: 257
		private const int nvarcharMaxSize = 4000;

		// Token: 0x04000102 RID: 258
		private const int binaryMaxSize = 8000;

		// Token: 0x04000103 RID: 259
		private readonly SqlVersion _version = SqlVersion.Sql9;

		// Token: 0x04000104 RID: 260
		private ReadOnlyCollection<PrimitiveType> _primitiveTypes;

		// Token: 0x04000105 RID: 261
		private ReadOnlyCollection<EdmFunction> _functions;
	}
}
