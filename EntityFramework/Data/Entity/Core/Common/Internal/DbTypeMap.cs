using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Metadata.Edm.Provider;

namespace System.Data.Entity.Core.Common.Internal
{
	// Token: 0x020002D5 RID: 725
	internal static class DbTypeMap
	{
		// Token: 0x06001968 RID: 6504 RVA: 0x0007EB60 File Offset: 0x0007CD60
		internal static bool TryGetModelTypeUsage(DbType dbType, out TypeUsage modelType)
		{
			switch (dbType)
			{
			case DbType.AnsiString:
				modelType = DbTypeMap.AnsiString;
				goto IL_163;
			case DbType.Binary:
				modelType = DbTypeMap.Binary;
				goto IL_163;
			case DbType.Byte:
				modelType = DbTypeMap.Byte;
				goto IL_163;
			case DbType.Boolean:
				modelType = DbTypeMap.Boolean;
				goto IL_163;
			case DbType.Currency:
				modelType = DbTypeMap.Currency;
				goto IL_163;
			case DbType.Date:
				modelType = DbTypeMap.Date;
				goto IL_163;
			case DbType.DateTime:
				modelType = DbTypeMap.DateTime;
				goto IL_163;
			case DbType.Decimal:
				modelType = DbTypeMap.Decimal;
				goto IL_163;
			case DbType.Double:
				modelType = DbTypeMap.Double;
				goto IL_163;
			case DbType.Guid:
				modelType = DbTypeMap.Guid;
				goto IL_163;
			case DbType.Int16:
				modelType = DbTypeMap.Int16;
				goto IL_163;
			case DbType.Int32:
				modelType = DbTypeMap.Int32;
				goto IL_163;
			case DbType.Int64:
				modelType = DbTypeMap.Int64;
				goto IL_163;
			case DbType.SByte:
				modelType = DbTypeMap.SByte;
				goto IL_163;
			case DbType.Single:
				modelType = DbTypeMap.Single;
				goto IL_163;
			case DbType.String:
				modelType = DbTypeMap.String;
				goto IL_163;
			case DbType.Time:
				modelType = DbTypeMap.Time;
				goto IL_163;
			case DbType.VarNumeric:
				modelType = null;
				goto IL_163;
			case DbType.AnsiStringFixedLength:
				modelType = DbTypeMap.AnsiStringFixedLength;
				goto IL_163;
			case DbType.StringFixedLength:
				modelType = DbTypeMap.StringFixedLength;
				goto IL_163;
			case DbType.Xml:
				modelType = DbTypeMap.Xml;
				goto IL_163;
			case DbType.DateTime2:
				modelType = DbTypeMap.DateTime2;
				goto IL_163;
			case DbType.DateTimeOffset:
				modelType = DbTypeMap.DateTimeOffset;
				goto IL_163;
			}
			modelType = null;
			IL_163:
			return modelType != null;
		}

		// Token: 0x06001969 RID: 6505 RVA: 0x0007ECD8 File Offset: 0x0007CED8
		private static TypeUsage CreateType(PrimitiveTypeKind type)
		{
			return DbTypeMap.CreateType(type, new FacetValues());
		}

		// Token: 0x0600196A RID: 6506 RVA: 0x0007ECE8 File Offset: 0x0007CEE8
		private static TypeUsage CreateType(PrimitiveTypeKind type, FacetValues facets)
		{
			PrimitiveType primitiveType = EdmProviderManifest.Instance.GetPrimitiveType(type);
			return TypeUsage.Create(primitiveType, facets);
		}

		// Token: 0x040008B4 RID: 2228
		internal static readonly TypeUsage AnsiString = DbTypeMap.CreateType(PrimitiveTypeKind.String, new FacetValues
		{
			Unicode = new bool?(false),
			FixedLength = new bool?(false),
			MaxLength = null
		});

		// Token: 0x040008B5 RID: 2229
		internal static readonly TypeUsage AnsiStringFixedLength = DbTypeMap.CreateType(PrimitiveTypeKind.String, new FacetValues
		{
			Unicode = new bool?(false),
			FixedLength = new bool?(true),
			MaxLength = null
		});

		// Token: 0x040008B6 RID: 2230
		internal static readonly TypeUsage String = DbTypeMap.CreateType(PrimitiveTypeKind.String, new FacetValues
		{
			Unicode = new bool?(true),
			FixedLength = new bool?(false),
			MaxLength = null
		});

		// Token: 0x040008B7 RID: 2231
		internal static readonly TypeUsage StringFixedLength = DbTypeMap.CreateType(PrimitiveTypeKind.String, new FacetValues
		{
			Unicode = new bool?(true),
			FixedLength = new bool?(true),
			MaxLength = null
		});

		// Token: 0x040008B8 RID: 2232
		internal static readonly TypeUsage Xml = DbTypeMap.CreateType(PrimitiveTypeKind.String, new FacetValues
		{
			Unicode = new bool?(true),
			FixedLength = new bool?(false),
			MaxLength = null
		});

		// Token: 0x040008B9 RID: 2233
		internal static readonly TypeUsage Binary = DbTypeMap.CreateType(PrimitiveTypeKind.Binary, new FacetValues
		{
			MaxLength = null
		});

		// Token: 0x040008BA RID: 2234
		internal static readonly TypeUsage Boolean = DbTypeMap.CreateType(PrimitiveTypeKind.Boolean);

		// Token: 0x040008BB RID: 2235
		internal static readonly TypeUsage Byte = DbTypeMap.CreateType(PrimitiveTypeKind.Byte);

		// Token: 0x040008BC RID: 2236
		internal static readonly TypeUsage DateTime = DbTypeMap.CreateType(PrimitiveTypeKind.DateTime);

		// Token: 0x040008BD RID: 2237
		internal static readonly TypeUsage Date = DbTypeMap.CreateType(PrimitiveTypeKind.DateTime);

		// Token: 0x040008BE RID: 2238
		internal static readonly TypeUsage DateTime2 = DbTypeMap.CreateType(PrimitiveTypeKind.DateTime, new FacetValues
		{
			Precision = null
		});

		// Token: 0x040008BF RID: 2239
		internal static readonly TypeUsage Time = DbTypeMap.CreateType(PrimitiveTypeKind.Time, new FacetValues
		{
			Precision = null
		});

		// Token: 0x040008C0 RID: 2240
		internal static readonly TypeUsage DateTimeOffset = DbTypeMap.CreateType(PrimitiveTypeKind.DateTimeOffset, new FacetValues
		{
			Precision = null
		});

		// Token: 0x040008C1 RID: 2241
		internal static readonly TypeUsage Decimal = DbTypeMap.CreateType(PrimitiveTypeKind.Decimal, new FacetValues
		{
			Precision = null,
			Scale = null
		});

		// Token: 0x040008C2 RID: 2242
		internal static readonly TypeUsage Currency = DbTypeMap.CreateType(PrimitiveTypeKind.Decimal, new FacetValues
		{
			Precision = null,
			Scale = null
		});

		// Token: 0x040008C3 RID: 2243
		internal static readonly TypeUsage Double = DbTypeMap.CreateType(PrimitiveTypeKind.Double);

		// Token: 0x040008C4 RID: 2244
		internal static readonly TypeUsage Guid = DbTypeMap.CreateType(PrimitiveTypeKind.Guid);

		// Token: 0x040008C5 RID: 2245
		internal static readonly TypeUsage Int16 = DbTypeMap.CreateType(PrimitiveTypeKind.Int16);

		// Token: 0x040008C6 RID: 2246
		internal static readonly TypeUsage Int32 = DbTypeMap.CreateType(PrimitiveTypeKind.Int32);

		// Token: 0x040008C7 RID: 2247
		internal static readonly TypeUsage Int64 = DbTypeMap.CreateType(PrimitiveTypeKind.Int64);

		// Token: 0x040008C8 RID: 2248
		internal static readonly TypeUsage Single = DbTypeMap.CreateType(PrimitiveTypeKind.Single);

		// Token: 0x040008C9 RID: 2249
		internal static readonly TypeUsage SByte = DbTypeMap.CreateType(PrimitiveTypeKind.SByte);
	}
}
