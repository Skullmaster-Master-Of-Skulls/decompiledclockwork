using System;
using System.Data.Metadata.Edm;

namespace System.Data.Common.Internal
{
	// Token: 0x020003C5 RID: 965
	internal static class DbTypeMap
	{
		// Token: 0x0600342D RID: 13357 RVA: 0x000C97C0 File Offset: 0x000C79C0
		internal static bool TryGetModelTypeUsage(DbType dbType, out TypeUsage modelType)
		{
			switch (dbType)
			{
			case DbType.AnsiString:
				modelType = DbTypeMap.AnsiString;
				goto IL_161;
			case DbType.Binary:
				modelType = DbTypeMap.Binary;
				goto IL_161;
			case DbType.Byte:
				modelType = DbTypeMap.Byte;
				goto IL_161;
			case DbType.Boolean:
				modelType = DbTypeMap.Boolean;
				goto IL_161;
			case DbType.Currency:
				modelType = DbTypeMap.Currency;
				goto IL_161;
			case DbType.Date:
				modelType = DbTypeMap.Date;
				goto IL_161;
			case DbType.DateTime:
				modelType = DbTypeMap.DateTime;
				goto IL_161;
			case DbType.Decimal:
				modelType = DbTypeMap.Decimal;
				goto IL_161;
			case DbType.Double:
				modelType = DbTypeMap.Double;
				goto IL_161;
			case DbType.Guid:
				modelType = DbTypeMap.Guid;
				goto IL_161;
			case DbType.Int16:
				modelType = DbTypeMap.Int16;
				goto IL_161;
			case DbType.Int32:
				modelType = DbTypeMap.Int32;
				goto IL_161;
			case DbType.Int64:
				modelType = DbTypeMap.Int64;
				goto IL_161;
			case DbType.SByte:
				modelType = DbTypeMap.SByte;
				goto IL_161;
			case DbType.Single:
				modelType = DbTypeMap.Single;
				goto IL_161;
			case DbType.String:
				modelType = DbTypeMap.String;
				goto IL_161;
			case DbType.Time:
				modelType = DbTypeMap.Time;
				goto IL_161;
			case DbType.VarNumeric:
				modelType = null;
				goto IL_161;
			case DbType.AnsiStringFixedLength:
				modelType = DbTypeMap.AnsiStringFixedLength;
				goto IL_161;
			case DbType.StringFixedLength:
				modelType = DbTypeMap.StringFixedLength;
				goto IL_161;
			case DbType.Xml:
				modelType = DbTypeMap.Xml;
				goto IL_161;
			case DbType.DateTime2:
				modelType = DbTypeMap.DateTime2;
				goto IL_161;
			case DbType.DateTimeOffset:
				modelType = DbTypeMap.DateTimeOffset;
				goto IL_161;
			}
			modelType = null;
			IL_161:
			return modelType != null;
		}

		// Token: 0x0600342E RID: 13358 RVA: 0x000C9933 File Offset: 0x000C7B33
		private static TypeUsage CreateType(PrimitiveTypeKind type)
		{
			return DbTypeMap.CreateType(type, new FacetValues());
		}

		// Token: 0x0600342F RID: 13359 RVA: 0x000C9940 File Offset: 0x000C7B40
		private static TypeUsage CreateType(PrimitiveTypeKind type, FacetValues facets)
		{
			PrimitiveType primitiveType = EdmProviderManifest.Instance.GetPrimitiveType(type);
			return TypeUsage.Create(primitiveType, facets);
		}

		// Token: 0x040016B4 RID: 5812
		internal static readonly TypeUsage AnsiString = DbTypeMap.CreateType(PrimitiveTypeKind.String, new FacetValues
		{
			Unicode = new bool?(false),
			FixedLength = new bool?(false),
			MaxLength = null
		});

		// Token: 0x040016B5 RID: 5813
		internal static readonly TypeUsage AnsiStringFixedLength = DbTypeMap.CreateType(PrimitiveTypeKind.String, new FacetValues
		{
			Unicode = new bool?(false),
			FixedLength = new bool?(true),
			MaxLength = null
		});

		// Token: 0x040016B6 RID: 5814
		internal static readonly TypeUsage String = DbTypeMap.CreateType(PrimitiveTypeKind.String, new FacetValues
		{
			Unicode = new bool?(true),
			FixedLength = new bool?(false),
			MaxLength = null
		});

		// Token: 0x040016B7 RID: 5815
		internal static readonly TypeUsage StringFixedLength = DbTypeMap.CreateType(PrimitiveTypeKind.String, new FacetValues
		{
			Unicode = new bool?(true),
			FixedLength = new bool?(true),
			MaxLength = null
		});

		// Token: 0x040016B8 RID: 5816
		internal static readonly TypeUsage Xml = DbTypeMap.CreateType(PrimitiveTypeKind.String, new FacetValues
		{
			Unicode = new bool?(true),
			FixedLength = new bool?(false),
			MaxLength = null
		});

		// Token: 0x040016B9 RID: 5817
		internal static readonly TypeUsage Binary = DbTypeMap.CreateType(PrimitiveTypeKind.Binary, new FacetValues
		{
			MaxLength = null
		});

		// Token: 0x040016BA RID: 5818
		internal static readonly TypeUsage Boolean = DbTypeMap.CreateType(PrimitiveTypeKind.Boolean);

		// Token: 0x040016BB RID: 5819
		internal static readonly TypeUsage Byte = DbTypeMap.CreateType(PrimitiveTypeKind.Byte);

		// Token: 0x040016BC RID: 5820
		internal static readonly TypeUsage DateTime = DbTypeMap.CreateType(PrimitiveTypeKind.DateTime);

		// Token: 0x040016BD RID: 5821
		internal static readonly TypeUsage Date = DbTypeMap.CreateType(PrimitiveTypeKind.DateTime);

		// Token: 0x040016BE RID: 5822
		internal static readonly TypeUsage DateTime2 = DbTypeMap.CreateType(PrimitiveTypeKind.DateTime, new FacetValues
		{
			Precision = null
		});

		// Token: 0x040016BF RID: 5823
		internal static readonly TypeUsage Time = DbTypeMap.CreateType(PrimitiveTypeKind.Time, new FacetValues
		{
			Precision = null
		});

		// Token: 0x040016C0 RID: 5824
		internal static readonly TypeUsage DateTimeOffset = DbTypeMap.CreateType(PrimitiveTypeKind.DateTimeOffset, new FacetValues
		{
			Precision = null
		});

		// Token: 0x040016C1 RID: 5825
		internal static readonly TypeUsage Decimal = DbTypeMap.CreateType(PrimitiveTypeKind.Decimal, new FacetValues
		{
			Precision = null,
			Scale = null
		});

		// Token: 0x040016C2 RID: 5826
		internal static readonly TypeUsage Currency = DbTypeMap.CreateType(PrimitiveTypeKind.Decimal, new FacetValues
		{
			Precision = null,
			Scale = null
		});

		// Token: 0x040016C3 RID: 5827
		internal static readonly TypeUsage Double = DbTypeMap.CreateType(PrimitiveTypeKind.Double);

		// Token: 0x040016C4 RID: 5828
		internal static readonly TypeUsage Guid = DbTypeMap.CreateType(PrimitiveTypeKind.Guid);

		// Token: 0x040016C5 RID: 5829
		internal static readonly TypeUsage Int16 = DbTypeMap.CreateType(PrimitiveTypeKind.Int16);

		// Token: 0x040016C6 RID: 5830
		internal static readonly TypeUsage Int32 = DbTypeMap.CreateType(PrimitiveTypeKind.Int32);

		// Token: 0x040016C7 RID: 5831
		internal static readonly TypeUsage Int64 = DbTypeMap.CreateType(PrimitiveTypeKind.Int64);

		// Token: 0x040016C8 RID: 5832
		internal static readonly TypeUsage Single = DbTypeMap.CreateType(PrimitiveTypeKind.Single);

		// Token: 0x040016C9 RID: 5833
		internal static readonly TypeUsage SByte = DbTypeMap.CreateType(PrimitiveTypeKind.SByte);
	}
}
