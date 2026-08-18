using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Spatial;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Xml;

namespace System.Data.Entity.Core.Metadata.Edm.Provider
{
	// Token: 0x020004F8 RID: 1272
	internal class EdmProviderManifest : DbProviderManifest
	{
		// Token: 0x06002F46 RID: 12102 RVA: 0x000E1564 File Offset: 0x000DF764
		private EdmProviderManifest()
		{
		}

		// Token: 0x1700071F RID: 1823
		// (get) Token: 0x06002F47 RID: 12103 RVA: 0x000E156C File Offset: 0x000DF76C
		internal static EdmProviderManifest Instance
		{
			get
			{
				return EdmProviderManifest._instance;
			}
		}

		// Token: 0x17000720 RID: 1824
		// (get) Token: 0x06002F48 RID: 12104 RVA: 0x000E1573 File Offset: 0x000DF773
		public override string NamespaceName
		{
			get
			{
				return "Edm";
			}
		}

		// Token: 0x17000721 RID: 1825
		// (get) Token: 0x06002F49 RID: 12105 RVA: 0x000E157A File Offset: 0x000DF77A
		internal virtual string Token
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x06002F4A RID: 12106 RVA: 0x000E1581 File Offset: 0x000DF781
		public override ReadOnlyCollection<EdmFunction> GetStoreFunctions()
		{
			this.InitializeCanonicalFunctions();
			return this._functions;
		}

		// Token: 0x06002F4B RID: 12107 RVA: 0x000E1590 File Offset: 0x000DF790
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

		// Token: 0x06002F4C RID: 12108 RVA: 0x000E15C1 File Offset: 0x000DF7C1
		public PrimitiveType GetPrimitiveType(PrimitiveTypeKind primitiveTypeKind)
		{
			this.InitializePrimitiveTypes();
			return this._primitiveTypes[(int)primitiveTypeKind];
		}

		// Token: 0x06002F4D RID: 12109 RVA: 0x000E15D8 File Offset: 0x000DF7D8
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

		// Token: 0x06002F4E RID: 12110 RVA: 0x000E1A72 File Offset: 0x000DFC72
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "clrType")]
		private void InitializePrimitiveType(PrimitiveType primitiveType, PrimitiveTypeKind primitiveTypeKind, string name, Type clrType)
		{
			EdmType.Initialize(primitiveType, name, "Edm", DataSpace.CSpace, true, null);
			PrimitiveType.Initialize(primitiveType, primitiveTypeKind, this);
		}

		// Token: 0x06002F4F RID: 12111 RVA: 0x000E1A8C File Offset: 0x000DFC8C
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
			dictionary.Add(key, new ReadOnlyCollection<FacetDescription>(initialFacetDescriptions));
			initialFacetDescriptions = EdmProviderManifest.GetInitialFacetDescriptions(PrimitiveTypeKind.Binary);
			key = this._primitiveTypes[0];
			dictionary.Add(key, new ReadOnlyCollection<FacetDescription>(initialFacetDescriptions));
			initialFacetDescriptions = EdmProviderManifest.GetInitialFacetDescriptions(PrimitiveTypeKind.DateTime);
			key = this._primitiveTypes[3];
			dictionary.Add(key, new ReadOnlyCollection<FacetDescription>(initialFacetDescriptions));
			initialFacetDescriptions = EdmProviderManifest.GetInitialFacetDescriptions(PrimitiveTypeKind.Time);
			key = this._primitiveTypes[13];
			dictionary.Add(key, new ReadOnlyCollection<FacetDescription>(initialFacetDescriptions));
			initialFacetDescriptions = EdmProviderManifest.GetInitialFacetDescriptions(PrimitiveTypeKind.DateTimeOffset);
			key = this._primitiveTypes[14];
			dictionary.Add(key, new ReadOnlyCollection<FacetDescription>(initialFacetDescriptions));
			initialFacetDescriptions = EdmProviderManifest.GetInitialFacetDescriptions(PrimitiveTypeKind.Decimal);
			key = this._primitiveTypes[4];
			dictionary.Add(key, new ReadOnlyCollection<FacetDescription>(initialFacetDescriptions));
			initialFacetDescriptions = EdmProviderManifest.GetInitialFacetDescriptions(PrimitiveTypeKind.Geography);
			key = this._primitiveTypes[16];
			dictionary.Add(key, new ReadOnlyCollection<FacetDescription>(initialFacetDescriptions));
			initialFacetDescriptions = EdmProviderManifest.GetInitialFacetDescriptions(PrimitiveTypeKind.GeographyPoint);
			key = this._primitiveTypes[24];
			dictionary.Add(key, new ReadOnlyCollection<FacetDescription>(initialFacetDescriptions));
			initialFacetDescriptions = EdmProviderManifest.GetInitialFacetDescriptions(PrimitiveTypeKind.GeographyLineString);
			key = this._primitiveTypes[25];
			dictionary.Add(key, new ReadOnlyCollection<FacetDescription>(initialFacetDescriptions));
			initialFacetDescriptions = EdmProviderManifest.GetInitialFacetDescriptions(PrimitiveTypeKind.GeographyPolygon);
			key = this._primitiveTypes[26];
			dictionary.Add(key, new ReadOnlyCollection<FacetDescription>(initialFacetDescriptions));
			initialFacetDescriptions = EdmProviderManifest.GetInitialFacetDescriptions(PrimitiveTypeKind.GeographyMultiPoint);
			key = this._primitiveTypes[27];
			dictionary.Add(key, new ReadOnlyCollection<FacetDescription>(initialFacetDescriptions));
			initialFacetDescriptions = EdmProviderManifest.GetInitialFacetDescriptions(PrimitiveTypeKind.GeographyMultiLineString);
			key = this._primitiveTypes[28];
			dictionary.Add(key, new ReadOnlyCollection<FacetDescription>(initialFacetDescriptions));
			initialFacetDescriptions = EdmProviderManifest.GetInitialFacetDescriptions(PrimitiveTypeKind.GeographyMultiPolygon);
			key = this._primitiveTypes[29];
			dictionary.Add(key, new ReadOnlyCollection<FacetDescription>(initialFacetDescriptions));
			initialFacetDescriptions = EdmProviderManifest.GetInitialFacetDescriptions(PrimitiveTypeKind.GeographyCollection);
			key = this._primitiveTypes[30];
			dictionary.Add(key, new ReadOnlyCollection<FacetDescription>(initialFacetDescriptions));
			initialFacetDescriptions = EdmProviderManifest.GetInitialFacetDescriptions(PrimitiveTypeKind.Geometry);
			key = this._primitiveTypes[15];
			dictionary.Add(key, new ReadOnlyCollection<FacetDescription>(initialFacetDescriptions));
			initialFacetDescriptions = EdmProviderManifest.GetInitialFacetDescriptions(PrimitiveTypeKind.GeometryPoint);
			key = this._primitiveTypes[17];
			dictionary.Add(key, new ReadOnlyCollection<FacetDescription>(initialFacetDescriptions));
			initialFacetDescriptions = EdmProviderManifest.GetInitialFacetDescriptions(PrimitiveTypeKind.GeometryLineString);
			key = this._primitiveTypes[18];
			dictionary.Add(key, new ReadOnlyCollection<FacetDescription>(initialFacetDescriptions));
			initialFacetDescriptions = EdmProviderManifest.GetInitialFacetDescriptions(PrimitiveTypeKind.GeometryPolygon);
			key = this._primitiveTypes[19];
			dictionary.Add(key, new ReadOnlyCollection<FacetDescription>(initialFacetDescriptions));
			initialFacetDescriptions = EdmProviderManifest.GetInitialFacetDescriptions(PrimitiveTypeKind.GeometryMultiPoint);
			key = this._primitiveTypes[20];
			dictionary.Add(key, new ReadOnlyCollection<FacetDescription>(initialFacetDescriptions));
			initialFacetDescriptions = EdmProviderManifest.GetInitialFacetDescriptions(PrimitiveTypeKind.GeometryMultiLineString);
			key = this._primitiveTypes[21];
			dictionary.Add(key, new ReadOnlyCollection<FacetDescription>(initialFacetDescriptions));
			initialFacetDescriptions = EdmProviderManifest.GetInitialFacetDescriptions(PrimitiveTypeKind.GeometryMultiPolygon);
			key = this._primitiveTypes[22];
			dictionary.Add(key, new ReadOnlyCollection<FacetDescription>(initialFacetDescriptions));
			initialFacetDescriptions = EdmProviderManifest.GetInitialFacetDescriptions(PrimitiveTypeKind.GeometryCollection);
			key = this._primitiveTypes[23];
			dictionary.Add(key, new ReadOnlyCollection<FacetDescription>(initialFacetDescriptions));
			Interlocked.CompareExchange<Dictionary<PrimitiveType, ReadOnlyCollection<FacetDescription>>>(ref this._facetDescriptions, dictionary, null);
		}

		// Token: 0x06002F50 RID: 12112 RVA: 0x000E1DB8 File Offset: 0x000DFFB8
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

		// Token: 0x06002F51 RID: 12113 RVA: 0x000E26A0 File Offset: 0x000E08A0
		[SuppressMessage("Microsoft.Maintainability", "CA1505:AvoidUnmaintainableCode")]
		[SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
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
			EdmProviderManifestFunctionBuilder.ForTypes(typeKinds, delegate(PrimitiveTypeKind type)
			{
				functions.AddAggregate("Max", type);
			});
			EdmProviderManifestFunctionBuilder.ForTypes(typeKinds, delegate(PrimitiveTypeKind type)
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
			EdmProviderManifestFunctionBuilder.ForTypes(typeKinds, delegate(PrimitiveTypeKind type)
			{
				functions.AddAggregate("Avg", type);
			});
			EdmProviderManifestFunctionBuilder.ForTypes(typeKinds, delegate(PrimitiveTypeKind type)
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
			EdmProviderManifestFunctionBuilder.ForTypes(typeKinds, delegate(PrimitiveTypeKind type)
			{
				functions.AddAggregate(PrimitiveTypeKind.Double, "StDev", type);
			});
			EdmProviderManifestFunctionBuilder.ForTypes(typeKinds, delegate(PrimitiveTypeKind type)
			{
				functions.AddAggregate(PrimitiveTypeKind.Double, "StDevP", type);
			});
			EdmProviderManifestFunctionBuilder.ForTypes(typeKinds, delegate(PrimitiveTypeKind type)
			{
				functions.AddAggregate(PrimitiveTypeKind.Double, "Var", type);
			});
			EdmProviderManifestFunctionBuilder.ForTypes(typeKinds, delegate(PrimitiveTypeKind type)
			{
				functions.AddAggregate(PrimitiveTypeKind.Double, "VarP", type);
			});
			EdmProviderManifestFunctionBuilder.ForAllBasePrimitiveTypes(delegate(PrimitiveTypeKind type)
			{
				functions.AddAggregate(PrimitiveTypeKind.Int32, "Count", type);
			});
			EdmProviderManifestFunctionBuilder.ForAllBasePrimitiveTypes(delegate(PrimitiveTypeKind type)
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
			EdmProviderManifestFunctionBuilder.ForTypes(typeKinds, delegate(PrimitiveTypeKind type)
			{
				functions.AddFunction(PrimitiveTypeKind.String, "Substring", PrimitiveTypeKind.String, "stringArgument", type, "start", type, "length");
			});
			EdmProviderManifestFunctionBuilder.ForTypes(typeKinds, delegate(PrimitiveTypeKind type)
			{
				functions.AddFunction(PrimitiveTypeKind.String, "Left", PrimitiveTypeKind.String, "stringArgument", type, "length");
			});
			EdmProviderManifestFunctionBuilder.ForTypes(typeKinds, delegate(PrimitiveTypeKind type)
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
			EdmProviderManifestFunctionBuilder.ForTypes(typeKinds2, delegate(PrimitiveTypeKind type)
			{
				functions.AddFunction(PrimitiveTypeKind.Int32, "Year", type, "dateValue");
			});
			EdmProviderManifestFunctionBuilder.ForTypes(typeKinds2, delegate(PrimitiveTypeKind type)
			{
				functions.AddFunction(PrimitiveTypeKind.Int32, "Month", type, "dateValue");
			});
			EdmProviderManifestFunctionBuilder.ForTypes(typeKinds2, delegate(PrimitiveTypeKind type)
			{
				functions.AddFunction(PrimitiveTypeKind.Int32, "Day", type, "dateValue");
			});
			EdmProviderManifestFunctionBuilder.ForTypes(typeKinds2, delegate(PrimitiveTypeKind type)
			{
				functions.AddFunction(PrimitiveTypeKind.Int32, "DayOfYear", type, "dateValue");
			});
			PrimitiveTypeKind[] typeKinds3 = new PrimitiveTypeKind[]
			{
				PrimitiveTypeKind.DateTimeOffset,
				PrimitiveTypeKind.DateTime,
				PrimitiveTypeKind.Time
			};
			EdmProviderManifestFunctionBuilder.ForTypes(typeKinds3, delegate(PrimitiveTypeKind type)
			{
				functions.AddFunction(PrimitiveTypeKind.Int32, "Hour", type, "timeValue");
			});
			EdmProviderManifestFunctionBuilder.ForTypes(typeKinds3, delegate(PrimitiveTypeKind type)
			{
				functions.AddFunction(PrimitiveTypeKind.Int32, "Minute", type, "timeValue");
			});
			EdmProviderManifestFunctionBuilder.ForTypes(typeKinds3, delegate(PrimitiveTypeKind type)
			{
				functions.AddFunction(PrimitiveTypeKind.Int32, "Second", type, "timeValue");
			});
			EdmProviderManifestFunctionBuilder.ForTypes(typeKinds3, delegate(PrimitiveTypeKind type)
			{
				functions.AddFunction(PrimitiveTypeKind.Int32, "Millisecond", type, "timeValue");
			});
			functions.AddFunction(PrimitiveTypeKind.DateTime, "CurrentDateTime");
			functions.AddFunction(PrimitiveTypeKind.DateTimeOffset, "CurrentDateTimeOffset");
			functions.AddFunction(PrimitiveTypeKind.Int32, "GetTotalOffsetMinutes", PrimitiveTypeKind.DateTimeOffset, "dateTimeOffsetArgument");
			functions.AddFunction(PrimitiveTypeKind.DateTime, "CurrentUtcDateTime");
			EdmProviderManifestFunctionBuilder.ForTypes(typeKinds2, delegate(PrimitiveTypeKind type)
			{
				functions.AddFunction(type, "TruncateTime", type, "dateValue");
			});
			functions.AddFunction(PrimitiveTypeKind.DateTime, "CreateDateTime", PrimitiveTypeKind.Int32, "year", PrimitiveTypeKind.Int32, "month", PrimitiveTypeKind.Int32, "day", PrimitiveTypeKind.Int32, "hour", PrimitiveTypeKind.Int32, "minute", PrimitiveTypeKind.Double, "second");
			functions.AddFunction(PrimitiveTypeKind.DateTimeOffset, "CreateDateTimeOffset", PrimitiveTypeKind.Int32, "year", PrimitiveTypeKind.Int32, "month", PrimitiveTypeKind.Int32, "day", PrimitiveTypeKind.Int32, "hour", PrimitiveTypeKind.Int32, "minute", PrimitiveTypeKind.Double, "second", PrimitiveTypeKind.Int32, "timeZoneOffset");
			functions.AddFunction(PrimitiveTypeKind.Time, "CreateTime", PrimitiveTypeKind.Int32, "hour", PrimitiveTypeKind.Int32, "minute", PrimitiveTypeKind.Double, "second");
			EdmProviderManifestFunctionBuilder.ForTypes(typeKinds2, delegate(PrimitiveTypeKind type)
			{
				functions.AddFunction(type, "AddYears", type, "dateValue", PrimitiveTypeKind.Int32, "addValue");
			});
			EdmProviderManifestFunctionBuilder.ForTypes(typeKinds2, delegate(PrimitiveTypeKind type)
			{
				functions.AddFunction(type, "AddMonths", type, "dateValue", PrimitiveTypeKind.Int32, "addValue");
			});
			EdmProviderManifestFunctionBuilder.ForTypes(typeKinds2, delegate(PrimitiveTypeKind type)
			{
				functions.AddFunction(type, "AddDays", type, "dateValue", PrimitiveTypeKind.Int32, "addValue");
			});
			EdmProviderManifestFunctionBuilder.ForTypes(typeKinds3, delegate(PrimitiveTypeKind type)
			{
				functions.AddFunction(type, "AddHours", type, "timeValue", PrimitiveTypeKind.Int32, "addValue");
			});
			EdmProviderManifestFunctionBuilder.ForTypes(typeKinds3, delegate(PrimitiveTypeKind type)
			{
				functions.AddFunction(type, "AddMinutes", type, "timeValue", PrimitiveTypeKind.Int32, "addValue");
			});
			EdmProviderManifestFunctionBuilder.ForTypes(typeKinds3, delegate(PrimitiveTypeKind type)
			{
				functions.AddFunction(type, "AddSeconds", type, "timeValue", PrimitiveTypeKind.Int32, "addValue");
			});
			EdmProviderManifestFunctionBuilder.ForTypes(typeKinds3, delegate(PrimitiveTypeKind type)
			{
				functions.AddFunction(type, "AddMilliseconds", type, "timeValue", PrimitiveTypeKind.Int32, "addValue");
			});
			EdmProviderManifestFunctionBuilder.ForTypes(typeKinds3, delegate(PrimitiveTypeKind type)
			{
				functions.AddFunction(type, "AddMicroseconds", type, "timeValue", PrimitiveTypeKind.Int32, "addValue");
			});
			EdmProviderManifestFunctionBuilder.ForTypes(typeKinds3, delegate(PrimitiveTypeKind type)
			{
				functions.AddFunction(type, "AddNanoseconds", type, "timeValue", PrimitiveTypeKind.Int32, "addValue");
			});
			EdmProviderManifestFunctionBuilder.ForTypes(typeKinds2, delegate(PrimitiveTypeKind type)
			{
				functions.AddFunction(PrimitiveTypeKind.Int32, "DiffYears", type, "dateValue1", type, "dateValue2");
			});
			EdmProviderManifestFunctionBuilder.ForTypes(typeKinds2, delegate(PrimitiveTypeKind type)
			{
				functions.AddFunction(PrimitiveTypeKind.Int32, "DiffMonths", type, "dateValue1", type, "dateValue2");
			});
			EdmProviderManifestFunctionBuilder.ForTypes(typeKinds2, delegate(PrimitiveTypeKind type)
			{
				functions.AddFunction(PrimitiveTypeKind.Int32, "DiffDays", type, "dateValue1", type, "dateValue2");
			});
			EdmProviderManifestFunctionBuilder.ForTypes(typeKinds3, delegate(PrimitiveTypeKind type)
			{
				functions.AddFunction(PrimitiveTypeKind.Int32, "DiffHours", type, "timeValue1", type, "timeValue2");
			});
			EdmProviderManifestFunctionBuilder.ForTypes(typeKinds3, delegate(PrimitiveTypeKind type)
			{
				functions.AddFunction(PrimitiveTypeKind.Int32, "DiffMinutes", type, "timeValue1", type, "timeValue2");
			});
			EdmProviderManifestFunctionBuilder.ForTypes(typeKinds3, delegate(PrimitiveTypeKind type)
			{
				functions.AddFunction(PrimitiveTypeKind.Int32, "DiffSeconds", type, "timeValue1", type, "timeValue2");
			});
			EdmProviderManifestFunctionBuilder.ForTypes(typeKinds3, delegate(PrimitiveTypeKind type)
			{
				functions.AddFunction(PrimitiveTypeKind.Int32, "DiffMilliseconds", type, "timeValue1", type, "timeValue2");
			});
			EdmProviderManifestFunctionBuilder.ForTypes(typeKinds3, delegate(PrimitiveTypeKind type)
			{
				functions.AddFunction(PrimitiveTypeKind.Int32, "DiffMicroseconds", type, "timeValue1", type, "timeValue2");
			});
			EdmProviderManifestFunctionBuilder.ForTypes(typeKinds3, delegate(PrimitiveTypeKind type)
			{
				functions.AddFunction(PrimitiveTypeKind.Int32, "DiffNanoseconds", type, "timeValue1", type, "timeValue2");
			});
			typeKinds = new PrimitiveTypeKind[]
			{
				PrimitiveTypeKind.Single,
				PrimitiveTypeKind.Double,
				PrimitiveTypeKind.Decimal
			};
			EdmProviderManifestFunctionBuilder.ForTypes(typeKinds, delegate(PrimitiveTypeKind type)
			{
				functions.AddFunction(type, "Round", type, "value");
			});
			EdmProviderManifestFunctionBuilder.ForTypes(typeKinds, delegate(PrimitiveTypeKind type)
			{
				functions.AddFunction(type, "Floor", type, "value");
			});
			EdmProviderManifestFunctionBuilder.ForTypes(typeKinds, delegate(PrimitiveTypeKind type)
			{
				functions.AddFunction(type, "Ceiling", type, "value");
			});
			typeKinds = new PrimitiveTypeKind[]
			{
				PrimitiveTypeKind.Double,
				PrimitiveTypeKind.Decimal
			};
			EdmProviderManifestFunctionBuilder.ForTypes(typeKinds, delegate(PrimitiveTypeKind type)
			{
				functions.AddFunction(type, "Round", type, "value", PrimitiveTypeKind.Int32, "digits");
			});
			EdmProviderManifestFunctionBuilder.ForTypes(typeKinds, delegate(PrimitiveTypeKind type)
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
			EdmProviderManifestFunctionBuilder.ForTypes(typeKinds, delegate(PrimitiveTypeKind type)
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
			EdmProviderManifestFunctionBuilder.ForTypes(typeKinds, delegate(PrimitiveTypeKind type)
			{
				functions.AddFunction(type, "BitwiseAnd", type, "value1", type, "value2");
			});
			EdmProviderManifestFunctionBuilder.ForTypes(typeKinds, delegate(PrimitiveTypeKind type)
			{
				functions.AddFunction(type, "BitwiseOr", type, "value1", type, "value2");
			});
			EdmProviderManifestFunctionBuilder.ForTypes(typeKinds, delegate(PrimitiveTypeKind type)
			{
				functions.AddFunction(type, "BitwiseXor", type, "value1", type, "value2");
			});
			EdmProviderManifestFunctionBuilder.ForTypes(typeKinds, delegate(PrimitiveTypeKind type)
			{
				functions.AddFunction(type, "BitwiseNot", type, "value");
			});
			functions.AddFunction(PrimitiveTypeKind.Guid, "NewGuid");
			EdmProviderManifestSpatialFunctions.AddFunctions(functions);
			ReadOnlyCollection<EdmFunction> value = functions.ToFunctionCollection();
			Interlocked.CompareExchange<ReadOnlyCollection<EdmFunction>>(ref this._functions, value, null);
		}

		// Token: 0x06002F52 RID: 12114 RVA: 0x000E2F41 File Offset: 0x000E1141
		internal ReadOnlyCollection<PrimitiveType> GetPromotionTypes(PrimitiveType primitiveType)
		{
			this.InitializePromotableTypes();
			return this._promotionTypes[(int)primitiveType.PrimitiveTypeKind];
		}

		// Token: 0x06002F53 RID: 12115 RVA: 0x000E2F58 File Offset: 0x000E1158
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

		// Token: 0x06002F54 RID: 12116 RVA: 0x000E3208 File Offset: 0x000E1408
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

		// Token: 0x06002F55 RID: 12117 RVA: 0x000E3257 File Offset: 0x000E1457
		internal TypeUsage GetCanonicalModelTypeUsage(PrimitiveTypeKind primitiveTypeKind)
		{
			if (EdmProviderManifest._canonicalModelTypes == null)
			{
				this.InitializeCanonicalModelTypes();
			}
			return EdmProviderManifest._canonicalModelTypes[(int)primitiveTypeKind];
		}

		// Token: 0x06002F56 RID: 12118 RVA: 0x000E3270 File Offset: 0x000E1470
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

		// Token: 0x06002F57 RID: 12119 RVA: 0x000E32BD File Offset: 0x000E14BD
		public override ReadOnlyCollection<PrimitiveType> GetStoreTypes()
		{
			this.InitializePrimitiveTypes();
			return this._primitiveTypes;
		}

		// Token: 0x06002F58 RID: 12120 RVA: 0x000E32CB File Offset: 0x000E14CB
		public override TypeUsage GetEdmType(TypeUsage storeType)
		{
			Check.NotNull<TypeUsage>(storeType, "storeType");
			throw new NotImplementedException();
		}

		// Token: 0x06002F59 RID: 12121 RVA: 0x000E32DE File Offset: 0x000E14DE
		public override TypeUsage GetStoreType(TypeUsage edmType)
		{
			Check.NotNull<TypeUsage>(edmType, "edmType");
			throw new NotImplementedException();
		}

		// Token: 0x06002F5A RID: 12122 RVA: 0x000E32F4 File Offset: 0x000E14F4
		internal TypeUsage ForgetScalarConstraints(TypeUsage type)
		{
			PrimitiveType primitiveType = type.EdmType as PrimitiveType;
			if (primitiveType != null)
			{
				return this.GetCanonicalModelTypeUsage(primitiveType.PrimitiveTypeKind);
			}
			return type;
		}

		// Token: 0x06002F5B RID: 12123 RVA: 0x000E331E File Offset: 0x000E151E
		protected override XmlReader GetDbInformation(string informationType)
		{
			throw new NotImplementedException();
		}

		// Token: 0x04001215 RID: 4629
		internal const string ConcurrencyModeFacetName = "ConcurrencyMode";

		// Token: 0x04001216 RID: 4630
		internal const string StoreGeneratedPatternFacetName = "StoreGeneratedPattern";

		// Token: 0x04001217 RID: 4631
		internal const byte MaximumDecimalPrecision = 255;

		// Token: 0x04001218 RID: 4632
		internal const byte MaximumDateTimePrecision = 255;

		// Token: 0x04001219 RID: 4633
		private Dictionary<PrimitiveType, ReadOnlyCollection<FacetDescription>> _facetDescriptions;

		// Token: 0x0400121A RID: 4634
		private ReadOnlyCollection<PrimitiveType> _primitiveTypes;

		// Token: 0x0400121B RID: 4635
		private ReadOnlyCollection<EdmFunction> _functions;

		// Token: 0x0400121C RID: 4636
		private static readonly EdmProviderManifest _instance = new EdmProviderManifest();

		// Token: 0x0400121D RID: 4637
		private ReadOnlyCollection<PrimitiveType>[] _promotionTypes;

		// Token: 0x0400121E RID: 4638
		private static TypeUsage[] _canonicalModelTypes;
	}
}
