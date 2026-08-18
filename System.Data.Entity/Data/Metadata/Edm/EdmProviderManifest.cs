using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Data.Spatial;
using System.Threading;
using System.Xml;

namespace System.Data.Metadata.Edm
{
	// Token: 0x020001EE RID: 494
	internal class EdmProviderManifest : DbProviderManifest
	{
		// Token: 0x060020E6 RID: 8422 RVA: 0x000729E7 File Offset: 0x00070BE7
		private EdmProviderManifest()
		{
		}

		// Token: 0x170006B2 RID: 1714
		// (get) Token: 0x060020E7 RID: 8423 RVA: 0x00072E32 File Offset: 0x00071032
		internal static EdmProviderManifest Instance
		{
			get
			{
				return EdmProviderManifest._instance;
			}
		}

		// Token: 0x170006B3 RID: 1715
		// (get) Token: 0x060020E8 RID: 8424 RVA: 0x00072E39 File Offset: 0x00071039
		public override string NamespaceName
		{
			get
			{
				return "Edm";
			}
		}

		// Token: 0x170006B4 RID: 1716
		// (get) Token: 0x060020E9 RID: 8425 RVA: 0x000406A4 File Offset: 0x0003E8A4
		internal string Token
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x060020EA RID: 8426 RVA: 0x00072E40 File Offset: 0x00071040
		public override ReadOnlyCollection<EdmFunction> GetStoreFunctions()
		{
			this.InitializeCanonicalFunctions();
			return this._functions;
		}

		// Token: 0x060020EB RID: 8427 RVA: 0x00072E50 File Offset: 0x00071050
		public override ReadOnlyCollection<FacetDescription> GetFacetDescriptions(EdmType type)
		{
			this.InitializeFacetDescriptions();
			ReadOnlyCollection<FacetDescription> result = null;
			if (this._facetDescriptions.TryGetValue(type as PrimitiveType, out result))
			{
				return result;
			}
			return Helper.EmptyFacetDescriptionEnumerable;
		}

		// Token: 0x060020EC RID: 8428 RVA: 0x00072E81 File Offset: 0x00071081
		public PrimitiveType GetPrimitiveType(PrimitiveTypeKind primitiveTypeKind)
		{
			this.InitializePrimitiveTypes();
			return this._primitiveTypes[(int)primitiveTypeKind];
		}

		// Token: 0x060020ED RID: 8429 RVA: 0x00072E98 File Offset: 0x00071098
		private void InitializePrimitiveTypes()
		{
			if (this._primitiveTypes != null)
			{
				return;
			}
			PrimitiveType[] array = new PrimitiveType[31];
			array[0] = new PrimitiveType();
			array[1] = new PrimitiveType();
			array[2] = new PrimitiveType();
			array[3] = new PrimitiveType();
			array[4] = new PrimitiveType();
			array[5] = new PrimitiveType();
			array[7] = new PrimitiveType();
			array[6] = new PrimitiveType();
			array[9] = new PrimitiveType();
			array[10] = new PrimitiveType();
			array[11] = new PrimitiveType();
			array[8] = new PrimitiveType();
			array[12] = new PrimitiveType();
			array[13] = new PrimitiveType();
			array[14] = new PrimitiveType();
			array[15] = new PrimitiveType();
			array[17] = new PrimitiveType();
			array[18] = new PrimitiveType();
			array[19] = new PrimitiveType();
			array[20] = new PrimitiveType();
			array[21] = new PrimitiveType();
			array[22] = new PrimitiveType();
			array[23] = new PrimitiveType();
			array[16] = new PrimitiveType();
			array[24] = new PrimitiveType();
			array[25] = new PrimitiveType();
			array[26] = new PrimitiveType();
			array[27] = new PrimitiveType();
			array[28] = new PrimitiveType();
			array[29] = new PrimitiveType();
			array[30] = new PrimitiveType();
			this.InitializePrimitiveType(array[0], PrimitiveTypeKind.Binary, "Binary", typeof(byte[]));
			this.InitializePrimitiveType(array[1], PrimitiveTypeKind.Boolean, "Boolean", typeof(bool));
			this.InitializePrimitiveType(array[2], PrimitiveTypeKind.Byte, "Byte", typeof(byte));
			this.InitializePrimitiveType(array[3], PrimitiveTypeKind.DateTime, "DateTime", typeof(DateTime));
			this.InitializePrimitiveType(array[4], PrimitiveTypeKind.Decimal, "Decimal", typeof(decimal));
			this.InitializePrimitiveType(array[5], PrimitiveTypeKind.Double, "Double", typeof(double));
			this.InitializePrimitiveType(array[7], PrimitiveTypeKind.Single, "Single", typeof(float));
			this.InitializePrimitiveType(array[6], PrimitiveTypeKind.Guid, "Guid", typeof(Guid));
			this.InitializePrimitiveType(array[9], PrimitiveTypeKind.Int16, "Int16", typeof(short));
			this.InitializePrimitiveType(array[10], PrimitiveTypeKind.Int32, "Int32", typeof(int));
			this.InitializePrimitiveType(array[11], PrimitiveTypeKind.Int64, "Int64", typeof(long));
			this.InitializePrimitiveType(array[8], PrimitiveTypeKind.SByte, "SByte", typeof(sbyte));
			this.InitializePrimitiveType(array[12], PrimitiveTypeKind.String, "String", typeof(string));
			this.InitializePrimitiveType(array[13], PrimitiveTypeKind.Time, "Time", typeof(TimeSpan));
			this.InitializePrimitiveType(array[14], PrimitiveTypeKind.DateTimeOffset, "DateTimeOffset", typeof(DateTimeOffset));
			this.InitializePrimitiveType(array[16], PrimitiveTypeKind.Geography, "Geography", typeof(DbGeography));
			this.InitializePrimitiveType(array[24], PrimitiveTypeKind.GeographyPoint, "GeographyPoint", typeof(DbGeography));
			this.InitializePrimitiveType(array[25], PrimitiveTypeKind.GeographyLineString, "GeographyLineString", typeof(DbGeography));
			this.InitializePrimitiveType(array[26], PrimitiveTypeKind.GeographyPolygon, "GeographyPolygon", typeof(DbGeography));
			this.InitializePrimitiveType(array[27], PrimitiveTypeKind.GeographyMultiPoint, "GeographyMultiPoint", typeof(DbGeography));
			this.InitializePrimitiveType(array[28], PrimitiveTypeKind.GeographyMultiLineString, "GeographyMultiLineString", typeof(DbGeography));
			this.InitializePrimitiveType(array[29], PrimitiveTypeKind.GeographyMultiPolygon, "GeographyMultiPolygon", typeof(DbGeography));
			this.InitializePrimitiveType(array[30], PrimitiveTypeKind.GeographyCollection, "GeographyCollection", typeof(DbGeography));
			this.InitializePrimitiveType(array[15], PrimitiveTypeKind.Geometry, "Geometry", typeof(DbGeometry));
			this.InitializePrimitiveType(array[17], PrimitiveTypeKind.GeometryPoint, "GeometryPoint", typeof(DbGeometry));
			this.InitializePrimitiveType(array[18], PrimitiveTypeKind.GeometryLineString, "GeometryLineString", typeof(DbGeometry));
			this.InitializePrimitiveType(array[19], PrimitiveTypeKind.GeometryPolygon, "GeometryPolygon", typeof(DbGeometry));
			this.InitializePrimitiveType(array[20], PrimitiveTypeKind.GeometryMultiPoint, "GeometryMultiPoint", typeof(DbGeometry));
			this.InitializePrimitiveType(array[21], PrimitiveTypeKind.GeometryMultiLineString, "GeometryMultiLineString", typeof(DbGeometry));
			this.InitializePrimitiveType(array[22], PrimitiveTypeKind.GeometryMultiPolygon, "GeometryMultiPolygon", typeof(DbGeometry));
			this.InitializePrimitiveType(array[23], PrimitiveTypeKind.GeometryCollection, "GeometryCollection", typeof(DbGeometry));
			foreach (PrimitiveType primitiveType in array)
			{
				primitiveType.ProviderManifest = this;
				primitiveType.SetReadOnly();
			}
			ReadOnlyCollection<PrimitiveType> value = new ReadOnlyCollection<PrimitiveType>(array);
			Interlocked.CompareExchange<ReadOnlyCollection<PrimitiveType>>(ref this._primitiveTypes, value, null);
		}

		// Token: 0x060020EE RID: 8430 RVA: 0x00073330 File Offset: 0x00071530
		private void InitializePrimitiveType(PrimitiveType primitiveType, PrimitiveTypeKind primitiveTypeKind, string name, Type clrType)
		{
			EdmType.Initialize(primitiveType, name, "Edm", DataSpace.CSpace, true, null);
			PrimitiveType.Initialize(primitiveType, primitiveTypeKind, true, this);
		}

		// Token: 0x060020EF RID: 8431 RVA: 0x0007334C File Offset: 0x0007154C
		private void InitializeFacetDescriptions()
		{
			if (this._facetDescriptions != null)
			{
				return;
			}
			this.InitializePrimitiveTypes();
			Dictionary<PrimitiveType, ReadOnlyCollection<FacetDescription>> dictionary = new Dictionary<PrimitiveType, ReadOnlyCollection<FacetDescription>>();
			FacetDescription[] initialFacetDescriptions = EdmProviderManifest.GetInitialFacetDescriptions(PrimitiveTypeKind.String);
			PrimitiveType key = this._primitiveTypes[12];
			dictionary.Add(key, Array.AsReadOnly<FacetDescription>(initialFacetDescriptions));
			initialFacetDescriptions = EdmProviderManifest.GetInitialFacetDescriptions(PrimitiveTypeKind.Binary);
			key = this._primitiveTypes[0];
			dictionary.Add(key, Array.AsReadOnly<FacetDescription>(initialFacetDescriptions));
			initialFacetDescriptions = EdmProviderManifest.GetInitialFacetDescriptions(PrimitiveTypeKind.DateTime);
			key = this._primitiveTypes[3];
			dictionary.Add(key, Array.AsReadOnly<FacetDescription>(initialFacetDescriptions));
			initialFacetDescriptions = EdmProviderManifest.GetInitialFacetDescriptions(PrimitiveTypeKind.Time);
			key = this._primitiveTypes[13];
			dictionary.Add(key, Array.AsReadOnly<FacetDescription>(initialFacetDescriptions));
			initialFacetDescriptions = EdmProviderManifest.GetInitialFacetDescriptions(PrimitiveTypeKind.DateTimeOffset);
			key = this._primitiveTypes[14];
			dictionary.Add(key, Array.AsReadOnly<FacetDescription>(initialFacetDescriptions));
			initialFacetDescriptions = EdmProviderManifest.GetInitialFacetDescriptions(PrimitiveTypeKind.Decimal);
			key = this._primitiveTypes[4];
			dictionary.Add(key, Array.AsReadOnly<FacetDescription>(initialFacetDescriptions));
			initialFacetDescriptions = EdmProviderManifest.GetInitialFacetDescriptions(PrimitiveTypeKind.Geography);
			key = this._primitiveTypes[16];
			dictionary.Add(key, Array.AsReadOnly<FacetDescription>(initialFacetDescriptions));
			initialFacetDescriptions = EdmProviderManifest.GetInitialFacetDescriptions(PrimitiveTypeKind.GeographyPoint);
			key = this._primitiveTypes[24];
			dictionary.Add(key, Array.AsReadOnly<FacetDescription>(initialFacetDescriptions));
			initialFacetDescriptions = EdmProviderManifest.GetInitialFacetDescriptions(PrimitiveTypeKind.GeographyLineString);
			key = this._primitiveTypes[25];
			dictionary.Add(key, Array.AsReadOnly<FacetDescription>(initialFacetDescriptions));
			initialFacetDescriptions = EdmProviderManifest.GetInitialFacetDescriptions(PrimitiveTypeKind.GeographyPolygon);
			key = this._primitiveTypes[26];
			dictionary.Add(key, Array.AsReadOnly<FacetDescription>(initialFacetDescriptions));
			initialFacetDescriptions = EdmProviderManifest.GetInitialFacetDescriptions(PrimitiveTypeKind.GeographyMultiPoint);
			key = this._primitiveTypes[27];
			dictionary.Add(key, Array.AsReadOnly<FacetDescription>(initialFacetDescriptions));
			initialFacetDescriptions = EdmProviderManifest.GetInitialFacetDescriptions(PrimitiveTypeKind.GeographyMultiLineString);
			key = this._primitiveTypes[28];
			dictionary.Add(key, Array.AsReadOnly<FacetDescription>(initialFacetDescriptions));
			initialFacetDescriptions = EdmProviderManifest.GetInitialFacetDescriptions(PrimitiveTypeKind.GeographyMultiPolygon);
			key = this._primitiveTypes[29];
			dictionary.Add(key, Array.AsReadOnly<FacetDescription>(initialFacetDescriptions));
			initialFacetDescriptions = EdmProviderManifest.GetInitialFacetDescriptions(PrimitiveTypeKind.GeographyCollection);
			key = this._primitiveTypes[30];
			dictionary.Add(key, Array.AsReadOnly<FacetDescription>(initialFacetDescriptions));
			initialFacetDescriptions = EdmProviderManifest.GetInitialFacetDescriptions(PrimitiveTypeKind.Geometry);
			key = this._primitiveTypes[15];
			dictionary.Add(key, Array.AsReadOnly<FacetDescription>(initialFacetDescriptions));
			initialFacetDescriptions = EdmProviderManifest.GetInitialFacetDescriptions(PrimitiveTypeKind.GeometryPoint);
			key = this._primitiveTypes[17];
			dictionary.Add(key, Array.AsReadOnly<FacetDescription>(initialFacetDescriptions));
			initialFacetDescriptions = EdmProviderManifest.GetInitialFacetDescriptions(PrimitiveTypeKind.GeometryLineString);
			key = this._primitiveTypes[18];
			dictionary.Add(key, Array.AsReadOnly<FacetDescription>(initialFacetDescriptions));
			initialFacetDescriptions = EdmProviderManifest.GetInitialFacetDescriptions(PrimitiveTypeKind.GeometryPolygon);
			key = this._primitiveTypes[19];
			dictionary.Add(key, Array.AsReadOnly<FacetDescription>(initialFacetDescriptions));
			initialFacetDescriptions = EdmProviderManifest.GetInitialFacetDescriptions(PrimitiveTypeKind.GeometryMultiPoint);
			key = this._primitiveTypes[20];
			dictionary.Add(key, Array.AsReadOnly<FacetDescription>(initialFacetDescriptions));
			initialFacetDescriptions = EdmProviderManifest.GetInitialFacetDescriptions(PrimitiveTypeKind.GeometryMultiLineString);
			key = this._primitiveTypes[21];
			dictionary.Add(key, Array.AsReadOnly<FacetDescription>(initialFacetDescriptions));
			initialFacetDescriptions = EdmProviderManifest.GetInitialFacetDescriptions(PrimitiveTypeKind.GeometryMultiPolygon);
			key = this._primitiveTypes[22];
			dictionary.Add(key, Array.AsReadOnly<FacetDescription>(initialFacetDescriptions));
			initialFacetDescriptions = EdmProviderManifest.GetInitialFacetDescriptions(PrimitiveTypeKind.GeometryCollection);
			key = this._primitiveTypes[23];
			dictionary.Add(key, Array.AsReadOnly<FacetDescription>(initialFacetDescriptions));
			Interlocked.CompareExchange<Dictionary<PrimitiveType, ReadOnlyCollection<FacetDescription>>>(ref this._facetDescriptions, dictionary, null);
		}

		// Token: 0x060020F0 RID: 8432 RVA: 0x00073678 File Offset: 0x00071878
		internal static FacetDescription[] GetInitialFacetDescriptions(PrimitiveTypeKind primitiveTypeKind)
		{
			switch (primitiveTypeKind)
			{
			case PrimitiveTypeKind.Binary:
				return new FacetDescription[]
				{
					new FacetDescription("MaxLength", MetadataItem.EdmProviderManifest.GetPrimitiveType(PrimitiveTypeKind.Int32), new int?(0), new int?(int.MaxValue), null),
					new FacetDescription("FixedLength", MetadataItem.EdmProviderManifest.GetPrimitiveType(PrimitiveTypeKind.Boolean), null, null, null)
				};
			case PrimitiveTypeKind.DateTime:
				return new FacetDescription[]
				{
					new FacetDescription("Precision", MetadataItem.EdmProviderManifest.GetPrimitiveType(PrimitiveTypeKind.Byte), new int?(0), new int?(255), null)
				};
			case PrimitiveTypeKind.Decimal:
				return new FacetDescription[]
				{
					new FacetDescription("Precision", MetadataItem.EdmProviderManifest.GetPrimitiveType(PrimitiveTypeKind.Byte), new int?(1), new int?(255), null),
					new FacetDescription("Scale", MetadataItem.EdmProviderManifest.GetPrimitiveType(PrimitiveTypeKind.Byte), new int?(0), new int?(255), null)
				};
			case PrimitiveTypeKind.String:
				return new FacetDescription[]
				{
					new FacetDescription("MaxLength", MetadataItem.EdmProviderManifest.GetPrimitiveType(PrimitiveTypeKind.Int32), new int?(0), new int?(int.MaxValue), null),
					new FacetDescription("Unicode", MetadataItem.EdmProviderManifest.GetPrimitiveType(PrimitiveTypeKind.Boolean), null, null, null),
					new FacetDescription("FixedLength", MetadataItem.EdmProviderManifest.GetPrimitiveType(PrimitiveTypeKind.Boolean), null, null, null)
				};
			case PrimitiveTypeKind.Time:
				return new FacetDescription[]
				{
					new FacetDescription("Precision", MetadataItem.EdmProviderManifest.GetPrimitiveType(PrimitiveTypeKind.Byte), new int?(0), new int?(255), TypeUsage.DefaultDateTimePrecisionFacetValue)
				};
			case PrimitiveTypeKind.DateTimeOffset:
				return new FacetDescription[]
				{
					new FacetDescription("Precision", MetadataItem.EdmProviderManifest.GetPrimitiveType(PrimitiveTypeKind.Byte), new int?(0), new int?(255), TypeUsage.DefaultDateTimePrecisionFacetValue)
				};
			case PrimitiveTypeKind.Geometry:
			case PrimitiveTypeKind.GeometryPoint:
			case PrimitiveTypeKind.GeometryLineString:
			case PrimitiveTypeKind.GeometryPolygon:
			case PrimitiveTypeKind.GeometryMultiPoint:
			case PrimitiveTypeKind.GeometryMultiLineString:
			case PrimitiveTypeKind.GeometryMultiPolygon:
			case PrimitiveTypeKind.GeometryCollection:
				return new FacetDescription[]
				{
					new FacetDescription("SRID", MetadataItem.EdmProviderManifest.GetPrimitiveType(PrimitiveTypeKind.Int32), new int?(0), new int?(int.MaxValue), DbGeometry.DefaultCoordinateSystemId),
					new FacetDescription("IsStrict", MetadataItem.EdmProviderManifest.GetPrimitiveType(PrimitiveTypeKind.Boolean), null, null, true)
				};
			case PrimitiveTypeKind.Geography:
			case PrimitiveTypeKind.GeographyPoint:
			case PrimitiveTypeKind.GeographyLineString:
			case PrimitiveTypeKind.GeographyPolygon:
			case PrimitiveTypeKind.GeographyMultiPoint:
			case PrimitiveTypeKind.GeographyMultiLineString:
			case PrimitiveTypeKind.GeographyMultiPolygon:
			case PrimitiveTypeKind.GeographyCollection:
				return new FacetDescription[]
				{
					new FacetDescription("SRID", MetadataItem.EdmProviderManifest.GetPrimitiveType(PrimitiveTypeKind.Int32), new int?(0), new int?(int.MaxValue), DbGeography.DefaultCoordinateSystemId),
					new FacetDescription("IsStrict", MetadataItem.EdmProviderManifest.GetPrimitiveType(PrimitiveTypeKind.Boolean), null, null, true)
				};
			}
			return null;
		}

		// Token: 0x060020F1 RID: 8433 RVA: 0x000739D0 File Offset: 0x00071BD0
		private void InitializeCanonicalFunctions()
		{
			if (this._functions != null)
			{
				return;
			}
			this.InitializePrimitiveTypes();
			EdmProviderManifestFunctionBuilder functions = new EdmProviderManifestFunctionBuilder(this._primitiveTypes);
			PrimitiveTypeKind[] typeKinds = new PrimitiveTypeKind[]
			{
				PrimitiveTypeKind.Byte,
				PrimitiveTypeKind.DateTime,
				PrimitiveTypeKind.Decimal,
				PrimitiveTypeKind.Double,
				PrimitiveTypeKind.Int16,
				PrimitiveTypeKind.Int32,
				PrimitiveTypeKind.Int64,
				PrimitiveTypeKind.SByte,
				PrimitiveTypeKind.Single,
				PrimitiveTypeKind.String,
				PrimitiveTypeKind.Binary,
				PrimitiveTypeKind.Time,
				PrimitiveTypeKind.DateTimeOffset
			};
			functions.ForTypes(typeKinds, delegate(PrimitiveTypeKind type)
			{
				functions.AddAggregate("Max", type);
			});
			functions.ForTypes(typeKinds, delegate(PrimitiveTypeKind type)
			{
				functions.AddAggregate("Min", type);
			});
			typeKinds = new PrimitiveTypeKind[]
			{
				PrimitiveTypeKind.Decimal,
				PrimitiveTypeKind.Double,
				PrimitiveTypeKind.Int32,
				PrimitiveTypeKind.Int64
			};
			functions.ForTypes(typeKinds, delegate(PrimitiveTypeKind type)
			{
				functions.AddAggregate("Avg", type);
			});
			functions.ForTypes(typeKinds, delegate(PrimitiveTypeKind type)
			{
				functions.AddAggregate("Sum", type);
			});
			typeKinds = new PrimitiveTypeKind[]
			{
				PrimitiveTypeKind.Decimal,
				PrimitiveTypeKind.Double,
				PrimitiveTypeKind.Int32,
				PrimitiveTypeKind.Int64
			};
			functions.ForTypes(typeKinds, delegate(PrimitiveTypeKind type)
			{
				functions.AddAggregate(PrimitiveTypeKind.Double, "StDev", type);
			});
			functions.ForTypes(typeKinds, delegate(PrimitiveTypeKind type)
			{
				functions.AddAggregate(PrimitiveTypeKind.Double, "StDevP", type);
			});
			functions.ForTypes(typeKinds, delegate(PrimitiveTypeKind type)
			{
				functions.AddAggregate(PrimitiveTypeKind.Double, "Var", type);
			});
			functions.ForTypes(typeKinds, delegate(PrimitiveTypeKind type)
			{
				functions.AddAggregate(PrimitiveTypeKind.Double, "VarP", type);
			});
			functions.ForAllBasePrimitiveTypes(delegate(PrimitiveTypeKind type)
			{
				functions.AddAggregate(PrimitiveTypeKind.Int32, "Count", type);
			});
			functions.ForAllBasePrimitiveTypes(delegate(PrimitiveTypeKind type)
			{
				functions.AddAggregate(PrimitiveTypeKind.Int64, "BigCount", type);
			});
			functions.AddFunction(PrimitiveTypeKind.String, "Trim", PrimitiveTypeKind.String, "stringArgument");
			functions.AddFunction(PrimitiveTypeKind.String, "RTrim", PrimitiveTypeKind.String, "stringArgument");
			functions.AddFunction(PrimitiveTypeKind.String, "LTrim", PrimitiveTypeKind.String, "stringArgument");
			functions.AddFunction(PrimitiveTypeKind.String, "Concat", PrimitiveTypeKind.String, "string1", PrimitiveTypeKind.String, "string2");
			functions.AddFunction(PrimitiveTypeKind.Int32, "Length", PrimitiveTypeKind.String, "stringArgument");
			typeKinds = new PrimitiveTypeKind[]
			{
				PrimitiveTypeKind.Byte,
				PrimitiveTypeKind.Int16,
				PrimitiveTypeKind.Int32,
				PrimitiveTypeKind.Int64,
				PrimitiveTypeKind.SByte
			};
			functions.ForTypes(typeKinds, delegate(PrimitiveTypeKind type)
			{
				functions.AddFunction(PrimitiveTypeKind.String, "Substring", PrimitiveTypeKind.String, "stringArgument", type, "start", type, "length");
			});
			functions.ForTypes(typeKinds, delegate(PrimitiveTypeKind type)
			{
				functions.AddFunction(PrimitiveTypeKind.String, "Left", PrimitiveTypeKind.String, "stringArgument", type, "length");
			});
			functions.ForTypes(typeKinds, delegate(PrimitiveTypeKind type)
			{
				functions.AddFunction(PrimitiveTypeKind.String, "Right", PrimitiveTypeKind.String, "stringArgument", type, "length");
			});
			functions.AddFunction(PrimitiveTypeKind.String, "Replace", PrimitiveTypeKind.String, "stringArgument", PrimitiveTypeKind.String, "toReplace", PrimitiveTypeKind.String, "replacement");
			functions.AddFunction(PrimitiveTypeKind.Int32, "IndexOf", PrimitiveTypeKind.String, "searchString", PrimitiveTypeKind.String, "stringToFind");
			functions.AddFunction(PrimitiveTypeKind.String, "ToUpper", PrimitiveTypeKind.String, "stringArgument");
			functions.AddFunction(PrimitiveTypeKind.String, "ToLower", PrimitiveTypeKind.String, "stringArgument");
			functions.AddFunction(PrimitiveTypeKind.String, "Reverse", PrimitiveTypeKind.String, "stringArgument");
			functions.AddFunction(PrimitiveTypeKind.Boolean, "Contains", PrimitiveTypeKind.String, "searchedString", PrimitiveTypeKind.String, "searchedForString");
			functions.AddFunction(PrimitiveTypeKind.Boolean, "StartsWith", PrimitiveTypeKind.String, "stringArgument", PrimitiveTypeKind.String, "prefix");
			functions.AddFunction(PrimitiveTypeKind.Boolean, "EndsWith", PrimitiveTypeKind.String, "stringArgument", PrimitiveTypeKind.String, "suffix");
			PrimitiveTypeKind[] typeKinds2 = new PrimitiveTypeKind[]
			{
				PrimitiveTypeKind.DateTimeOffset,
				PrimitiveTypeKind.DateTime
			};
			functions.ForTypes(typeKinds2, delegate(PrimitiveTypeKind type)
			{
				functions.AddFunction(PrimitiveTypeKind.Int32, "Year", type, "dateValue");
			});
			functions.ForTypes(typeKinds2, delegate(PrimitiveTypeKind type)
			{
				functions.AddFunction(PrimitiveTypeKind.Int32, "Month", type, "dateValue");
			});
			functions.ForTypes(typeKinds2, delegate(PrimitiveTypeKind type)
			{
				functions.AddFunction(PrimitiveTypeKind.Int32, "Day", type, "dateValue");
			});
			functions.ForTypes(typeKinds2, delegate(PrimitiveTypeKind type)
			{
				functions.AddFunction(PrimitiveTypeKind.Int32, "DayOfYear", type, "dateValue");
			});
			PrimitiveTypeKind[] typeKinds3 = new PrimitiveTypeKind[]
			{
				PrimitiveTypeKind.DateTimeOffset,
				PrimitiveTypeKind.DateTime,
				PrimitiveTypeKind.Time
			};
			functions.ForTypes(typeKinds3, delegate(PrimitiveTypeKind type)
			{
				functions.AddFunction(PrimitiveTypeKind.Int32, "Hour", type, "timeValue");
			});
			functions.ForTypes(typeKinds3, delegate(PrimitiveTypeKind type)
			{
				functions.AddFunction(PrimitiveTypeKind.Int32, "Minute", type, "timeValue");
			});
			functions.ForTypes(typeKinds3, delegate(PrimitiveTypeKind type)
			{
				functions.AddFunction(PrimitiveTypeKind.Int32, "Second", type, "timeValue");
			});
			functions.ForTypes(typeKinds3, delegate(PrimitiveTypeKind type)
			{
				functions.AddFunction(PrimitiveTypeKind.Int32, "Millisecond", type, "timeValue");
			});
			functions.AddFunction(PrimitiveTypeKind.DateTime, "CurrentDateTime");
			functions.AddFunction(PrimitiveTypeKind.DateTimeOffset, "CurrentDateTimeOffset");
			functions.AddFunction(PrimitiveTypeKind.Int32, "GetTotalOffsetMinutes", PrimitiveTypeKind.DateTimeOffset, "dateTimeOffsetArgument");
			functions.AddFunction(PrimitiveTypeKind.DateTime, "CurrentUtcDateTime");
			functions.ForTypes(typeKinds2, delegate(PrimitiveTypeKind type)
			{
				functions.AddFunction(type, "TruncateTime", type, "dateValue");
			});
			functions.AddFunction(PrimitiveTypeKind.DateTime, "CreateDateTime", PrimitiveTypeKind.Int32, "year", PrimitiveTypeKind.Int32, "month", PrimitiveTypeKind.Int32, "day", PrimitiveTypeKind.Int32, "hour", PrimitiveTypeKind.Int32, "minute", PrimitiveTypeKind.Double, "second");
			functions.AddFunction(PrimitiveTypeKind.DateTimeOffset, "CreateDateTimeOffset", PrimitiveTypeKind.Int32, "year", PrimitiveTypeKind.Int32, "month", PrimitiveTypeKind.Int32, "day", PrimitiveTypeKind.Int32, "hour", PrimitiveTypeKind.Int32, "minute", PrimitiveTypeKind.Double, "second", PrimitiveTypeKind.Int32, "timeZoneOffset");
			functions.AddFunction(PrimitiveTypeKind.Time, "CreateTime", PrimitiveTypeKind.Int32, "hour", PrimitiveTypeKind.Int32, "minute", PrimitiveTypeKind.Double, "second");
			functions.ForTypes(typeKinds2, delegate(PrimitiveTypeKind type)
			{
				functions.AddFunction(type, "AddYears", type, "dateValue", PrimitiveTypeKind.Int32, "addValue");
			});
			functions.ForTypes(typeKinds2, delegate(PrimitiveTypeKind type)
			{
				functions.AddFunction(type, "AddMonths", type, "dateValue", PrimitiveTypeKind.Int32, "addValue");
			});
			functions.ForTypes(typeKinds2, delegate(PrimitiveTypeKind type)
			{
				functions.AddFunction(type, "AddDays", type, "dateValue", PrimitiveTypeKind.Int32, "addValue");
			});
			functions.ForTypes(typeKinds3, delegate(PrimitiveTypeKind type)
			{
				functions.AddFunction(type, "AddHours", type, "timeValue", PrimitiveTypeKind.Int32, "addValue");
			});
			functions.ForTypes(typeKinds3, delegate(PrimitiveTypeKind type)
			{
				functions.AddFunction(type, "AddMinutes", type, "timeValue", PrimitiveTypeKind.Int32, "addValue");
			});
			functions.ForTypes(typeKinds3, delegate(PrimitiveTypeKind type)
			{
				functions.AddFunction(type, "AddSeconds", type, "timeValue", PrimitiveTypeKind.Int32, "addValue");
			});
			functions.ForTypes(typeKinds3, delegate(PrimitiveTypeKind type)
			{
				functions.AddFunction(type, "AddMilliseconds", type, "timeValue", PrimitiveTypeKind.Int32, "addValue");
			});
			functions.ForTypes(typeKinds3, delegate(PrimitiveTypeKind type)
			{
				functions.AddFunction(type, "AddMicroseconds", type, "timeValue", PrimitiveTypeKind.Int32, "addValue");
			});
			functions.ForTypes(typeKinds3, delegate(PrimitiveTypeKind type)
			{
				functions.AddFunction(type, "AddNanoseconds", type, "timeValue", PrimitiveTypeKind.Int32, "addValue");
			});
			functions.ForTypes(typeKinds2, delegate(PrimitiveTypeKind type)
			{
				functions.AddFunction(PrimitiveTypeKind.Int32, "DiffYears", type, "dateValue1", type, "dateValue2");
			});
			functions.ForTypes(typeKinds2, delegate(PrimitiveTypeKind type)
			{
				functions.AddFunction(PrimitiveTypeKind.Int32, "DiffMonths", type, "dateValue1", type, "dateValue2");
			});
			functions.ForTypes(typeKinds2, delegate(PrimitiveTypeKind type)
			{
				functions.AddFunction(PrimitiveTypeKind.Int32, "DiffDays", type, "dateValue1", type, "dateValue2");
			});
			functions.ForTypes(typeKinds3, delegate(PrimitiveTypeKind type)
			{
				functions.AddFunction(PrimitiveTypeKind.Int32, "DiffHours", type, "timeValue1", type, "timeValue2");
			});
			functions.ForTypes(typeKinds3, delegate(PrimitiveTypeKind type)
			{
				functions.AddFunction(PrimitiveTypeKind.Int32, "DiffMinutes", type, "timeValue1", type, "timeValue2");
			});
			functions.ForTypes(typeKinds3, delegate(PrimitiveTypeKind type)
			{
				functions.AddFunction(PrimitiveTypeKind.Int32, "DiffSeconds", type, "timeValue1", type, "timeValue2");
			});
			functions.ForTypes(typeKinds3, delegate(PrimitiveTypeKind type)
			{
				functions.AddFunction(PrimitiveTypeKind.Int32, "DiffMilliseconds", type, "timeValue1", type, "timeValue2");
			});
			functions.ForTypes(typeKinds3, delegate(PrimitiveTypeKind type)
			{
				functions.AddFunction(PrimitiveTypeKind.Int32, "DiffMicroseconds", type, "timeValue1", type, "timeValue2");
			});
			functions.ForTypes(typeKinds3, delegate(PrimitiveTypeKind type)
			{
				functions.AddFunction(PrimitiveTypeKind.Int32, "DiffNanoseconds", type, "timeValue1", type, "timeValue2");
			});
			typeKinds = new PrimitiveTypeKind[]
			{
				PrimitiveTypeKind.Single,
				PrimitiveTypeKind.Double,
				PrimitiveTypeKind.Decimal
			};
			functions.ForTypes(typeKinds, delegate(PrimitiveTypeKind type)
			{
				functions.AddFunction(type, "Round", type, "value");
			});
			functions.ForTypes(typeKinds, delegate(PrimitiveTypeKind type)
			{
				functions.AddFunction(type, "Floor", type, "value");
			});
			functions.ForTypes(typeKinds, delegate(PrimitiveTypeKind type)
			{
				functions.AddFunction(type, "Ceiling", type, "value");
			});
			typeKinds = new PrimitiveTypeKind[]
			{
				PrimitiveTypeKind.Double,
				PrimitiveTypeKind.Decimal
			};
			functions.ForTypes(typeKinds, delegate(PrimitiveTypeKind type)
			{
				functions.AddFunction(type, "Round", type, "value", PrimitiveTypeKind.Int32, "digits");
			});
			functions.ForTypes(typeKinds, delegate(PrimitiveTypeKind type)
			{
				functions.AddFunction(type, "Truncate", type, "value", PrimitiveTypeKind.Int32, "digits");
			});
			typeKinds = new PrimitiveTypeKind[]
			{
				PrimitiveTypeKind.Decimal,
				PrimitiveTypeKind.Double,
				PrimitiveTypeKind.Int16,
				PrimitiveTypeKind.Int32,
				PrimitiveTypeKind.Int64,
				PrimitiveTypeKind.Byte,
				PrimitiveTypeKind.Single
			};
			functions.ForTypes(typeKinds, delegate(PrimitiveTypeKind type)
			{
				functions.AddFunction(type, "Abs", type, "value");
			});
			PrimitiveTypeKind[] array = new PrimitiveTypeKind[]
			{
				PrimitiveTypeKind.Decimal,
				PrimitiveTypeKind.Double,
				PrimitiveTypeKind.Int32,
				PrimitiveTypeKind.Int64
			};
			PrimitiveTypeKind[] array2 = new PrimitiveTypeKind[]
			{
				PrimitiveTypeKind.Decimal,
				PrimitiveTypeKind.Double,
				PrimitiveTypeKind.Int64
			};
			foreach (PrimitiveTypeKind primitiveTypeKind in array)
			{
				foreach (PrimitiveTypeKind argument2TypeKind in array2)
				{
					functions.AddFunction(primitiveTypeKind, "Power", primitiveTypeKind, "baseArgument", argument2TypeKind, "exponent");
				}
			}
			typeKinds = new PrimitiveTypeKind[]
			{
				PrimitiveTypeKind.Int16,
				PrimitiveTypeKind.Int32,
				PrimitiveTypeKind.Int64,
				PrimitiveTypeKind.Byte
			};
			functions.ForTypes(typeKinds, delegate(PrimitiveTypeKind type)
			{
				functions.AddFunction(type, "BitwiseAnd", type, "value1", type, "value2");
			});
			functions.ForTypes(typeKinds, delegate(PrimitiveTypeKind type)
			{
				functions.AddFunction(type, "BitwiseOr", type, "value1", type, "value2");
			});
			functions.ForTypes(typeKinds, delegate(PrimitiveTypeKind type)
			{
				functions.AddFunction(type, "BitwiseXor", type, "value1", type, "value2");
			});
			functions.ForTypes(typeKinds, delegate(PrimitiveTypeKind type)
			{
				functions.AddFunction(type, "BitwiseNot", type, "value");
			});
			functions.AddFunction(PrimitiveTypeKind.Guid, "NewGuid");
			EdmProviderManifestSpatialFunctions.AddFunctions(functions);
			ReadOnlyCollection<EdmFunction> value = functions.ToFunctionCollection();
			Interlocked.CompareExchange<ReadOnlyCollection<EdmFunction>>(ref this._functions, value, null);
		}

		// Token: 0x060020F2 RID: 8434 RVA: 0x0007427D File Offset: 0x0007247D
		internal ReadOnlyCollection<PrimitiveType> GetPromotionTypes(PrimitiveType primitiveType)
		{
			this.InitializePromotableTypes();
			return this._promotionTypes[(int)primitiveType.PrimitiveTypeKind];
		}

		// Token: 0x060020F3 RID: 8435 RVA: 0x00074294 File Offset: 0x00072494
		private void InitializePromotableTypes()
		{
			if (this._promotionTypes != null)
			{
				return;
			}
			ReadOnlyCollection<PrimitiveType>[] array = new ReadOnlyCollection<PrimitiveType>[31];
			for (int i = 0; i < 31; i++)
			{
				array[i] = new ReadOnlyCollection<PrimitiveType>(new PrimitiveType[]
				{
					this._primitiveTypes[i]
				});
			}
			array[2] = new ReadOnlyCollection<PrimitiveType>(new PrimitiveType[]
			{
				this._primitiveTypes[2],
				this._primitiveTypes[9],
				this._primitiveTypes[10],
				this._primitiveTypes[11],
				this._primitiveTypes[4],
				this._primitiveTypes[7],
				this._primitiveTypes[5]
			});
			array[9] = new ReadOnlyCollection<PrimitiveType>(new PrimitiveType[]
			{
				this._primitiveTypes[9],
				this._primitiveTypes[10],
				this._primitiveTypes[11],
				this._primitiveTypes[4],
				this._primitiveTypes[7],
				this._primitiveTypes[5]
			});
			array[10] = new ReadOnlyCollection<PrimitiveType>(new PrimitiveType[]
			{
				this._primitiveTypes[10],
				this._primitiveTypes[11],
				this._primitiveTypes[4],
				this._primitiveTypes[7],
				this._primitiveTypes[5]
			});
			array[11] = new ReadOnlyCollection<PrimitiveType>(new PrimitiveType[]
			{
				this._primitiveTypes[11],
				this._primitiveTypes[4],
				this._primitiveTypes[7],
				this._primitiveTypes[5]
			});
			array[7] = new ReadOnlyCollection<PrimitiveType>(new PrimitiveType[]
			{
				this._primitiveTypes[7],
				this._primitiveTypes[5]
			});
			this.InitializeSpatialPromotionGroup(array, new PrimitiveTypeKind[]
			{
				PrimitiveTypeKind.GeographyPoint,
				PrimitiveTypeKind.GeographyLineString,
				PrimitiveTypeKind.GeographyPolygon,
				PrimitiveTypeKind.GeographyMultiPoint,
				PrimitiveTypeKind.GeographyMultiLineString,
				PrimitiveTypeKind.GeographyMultiPolygon,
				PrimitiveTypeKind.GeographyCollection
			}, PrimitiveTypeKind.Geography);
			this.InitializeSpatialPromotionGroup(array, new PrimitiveTypeKind[]
			{
				PrimitiveTypeKind.GeometryPoint,
				PrimitiveTypeKind.GeometryLineString,
				PrimitiveTypeKind.GeometryPolygon,
				PrimitiveTypeKind.GeometryMultiPoint,
				PrimitiveTypeKind.GeometryMultiLineString,
				PrimitiveTypeKind.GeometryMultiPolygon,
				PrimitiveTypeKind.GeometryCollection
			}, PrimitiveTypeKind.Geometry);
			Interlocked.CompareExchange<ReadOnlyCollection<PrimitiveType>[]>(ref this._promotionTypes, array, null);
		}

		// Token: 0x060020F4 RID: 8436 RVA: 0x000744D8 File Offset: 0x000726D8
		private void InitializeSpatialPromotionGroup(ReadOnlyCollection<PrimitiveType>[] promotionTypes, PrimitiveTypeKind[] promotableKinds, PrimitiveTypeKind baseKind)
		{
			foreach (PrimitiveTypeKind primitiveTypeKind in promotableKinds)
			{
				promotionTypes[(int)primitiveTypeKind] = new ReadOnlyCollection<PrimitiveType>(new PrimitiveType[]
				{
					this._primitiveTypes[(int)primitiveTypeKind],
					this._primitiveTypes[(int)baseKind]
				});
			}
		}

		// Token: 0x060020F5 RID: 8437 RVA: 0x00074525 File Offset: 0x00072725
		internal TypeUsage GetCanonicalModelTypeUsage(PrimitiveTypeKind primitiveTypeKind)
		{
			if (EdmProviderManifest._canonicalModelTypes == null)
			{
				this.InitializeCanonicalModelTypes();
			}
			return EdmProviderManifest._canonicalModelTypes[(int)primitiveTypeKind];
		}

		// Token: 0x060020F6 RID: 8438 RVA: 0x0007453C File Offset: 0x0007273C
		private void InitializeCanonicalModelTypes()
		{
			this.InitializePrimitiveTypes();
			TypeUsage[] array = new TypeUsage[31];
			for (int i = 0; i < 31; i++)
			{
				PrimitiveType edmType = this._primitiveTypes[i];
				TypeUsage typeUsage = TypeUsage.CreateDefaultTypeUsage(edmType);
				array[i] = typeUsage;
			}
			Interlocked.CompareExchange<TypeUsage[]>(ref EdmProviderManifest._canonicalModelTypes, array, null);
		}

		// Token: 0x060020F7 RID: 8439 RVA: 0x00074589 File Offset: 0x00072789
		public override ReadOnlyCollection<PrimitiveType> GetStoreTypes()
		{
			this.InitializePrimitiveTypes();
			return this._primitiveTypes;
		}

		// Token: 0x060020F8 RID: 8440 RVA: 0x00072E1F File Offset: 0x0007101F
		public override TypeUsage GetEdmType(TypeUsage storeType)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060020F9 RID: 8441 RVA: 0x00072E1F File Offset: 0x0007101F
		public override TypeUsage GetStoreType(TypeUsage edmType)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060020FA RID: 8442 RVA: 0x00074598 File Offset: 0x00072798
		internal TypeUsage ForgetScalarConstraints(TypeUsage type)
		{
			PrimitiveType primitiveType = type.EdmType as PrimitiveType;
			if (primitiveType != null)
			{
				return this.GetCanonicalModelTypeUsage(primitiveType.PrimitiveTypeKind);
			}
			return type;
		}

		// Token: 0x060020FB RID: 8443 RVA: 0x00072E1F File Offset: 0x0007101F
		protected override XmlReader GetDbInformation(string informationType)
		{
			throw new NotImplementedException();
		}

		// Token: 0x04000E93 RID: 3731
		internal const string ConcurrencyModeFacetName = "ConcurrencyMode";

		// Token: 0x04000E94 RID: 3732
		internal const string StoreGeneratedPatternFacetName = "StoreGeneratedPattern";

		// Token: 0x04000E95 RID: 3733
		private Dictionary<PrimitiveType, ReadOnlyCollection<FacetDescription>> _facetDescriptions;

		// Token: 0x04000E96 RID: 3734
		private ReadOnlyCollection<PrimitiveType> _primitiveTypes;

		// Token: 0x04000E97 RID: 3735
		private ReadOnlyCollection<EdmFunction> _functions;

		// Token: 0x04000E98 RID: 3736
		private static EdmProviderManifest _instance = new EdmProviderManifest();

		// Token: 0x04000E99 RID: 3737
		private ReadOnlyCollection<PrimitiveType>[] _promotionTypes;

		// Token: 0x04000E9A RID: 3738
		private static TypeUsage[] _canonicalModelTypes;

		// Token: 0x04000E9B RID: 3739
		internal const byte MaximumDecimalPrecision = 255;

		// Token: 0x04000E9C RID: 3740
		internal const byte MaximumDateTimePrecision = 255;
	}
}
