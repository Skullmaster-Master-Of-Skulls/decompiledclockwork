using System;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;

namespace System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder.Spatial
{
	// Token: 0x02000121 RID: 289
	public static class SpatialEdmFunctions
	{
		// Token: 0x060008CB RID: 2251 RVA: 0x0002D780 File Offset: 0x0002B980
		public static DbFunctionExpression GeometryFromText(DbExpression wellKnownText)
		{
			Check.NotNull<DbExpression>(wellKnownText, "wellKnownText");
			return EdmFunctions.InvokeCanonicalFunction("GeometryFromText", new DbExpression[]
			{
				wellKnownText
			});
		}

		// Token: 0x060008CC RID: 2252 RVA: 0x0002D7B0 File Offset: 0x0002B9B0
		public static DbFunctionExpression GeometryFromText(DbExpression wellKnownText, DbExpression coordinateSystemId)
		{
			Check.NotNull<DbExpression>(wellKnownText, "wellKnownText");
			Check.NotNull<DbExpression>(coordinateSystemId, "coordinateSystemId");
			return EdmFunctions.InvokeCanonicalFunction("GeometryFromText", new DbExpression[]
			{
				wellKnownText,
				coordinateSystemId
			});
		}

		// Token: 0x060008CD RID: 2253 RVA: 0x0002D7F0 File Offset: 0x0002B9F0
		public static DbFunctionExpression GeometryPointFromText(DbExpression pointWellKnownText, DbExpression coordinateSystemId)
		{
			Check.NotNull<DbExpression>(pointWellKnownText, "pointWellKnownText");
			Check.NotNull<DbExpression>(coordinateSystemId, "coordinateSystemId");
			return EdmFunctions.InvokeCanonicalFunction("GeometryPointFromText", new DbExpression[]
			{
				pointWellKnownText,
				coordinateSystemId
			});
		}

		// Token: 0x060008CE RID: 2254 RVA: 0x0002D830 File Offset: 0x0002BA30
		public static DbFunctionExpression GeometryLineFromText(DbExpression lineWellKnownText, DbExpression coordinateSystemId)
		{
			Check.NotNull<DbExpression>(lineWellKnownText, "lineWellKnownText");
			Check.NotNull<DbExpression>(coordinateSystemId, "coordinateSystemId");
			return EdmFunctions.InvokeCanonicalFunction("GeometryLineFromText", new DbExpression[]
			{
				lineWellKnownText,
				coordinateSystemId
			});
		}

		// Token: 0x060008CF RID: 2255 RVA: 0x0002D870 File Offset: 0x0002BA70
		public static DbFunctionExpression GeometryPolygonFromText(DbExpression polygonWellKnownText, DbExpression coordinateSystemId)
		{
			Check.NotNull<DbExpression>(polygonWellKnownText, "polygonWellKnownText");
			Check.NotNull<DbExpression>(coordinateSystemId, "coordinateSystemId");
			return EdmFunctions.InvokeCanonicalFunction("GeometryPolygonFromText", new DbExpression[]
			{
				polygonWellKnownText,
				coordinateSystemId
			});
		}

		// Token: 0x060008D0 RID: 2256 RVA: 0x0002D8B0 File Offset: 0x0002BAB0
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Multi", Justification = "Match OGC, EDM")]
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "multi", Justification = "Match OGC, EDM")]
		[SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "MultiPoint", Justification = "Match OGC, EDM")]
		[SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "multiPoint", Justification = "Match OGC, EDM")]
		public static DbFunctionExpression GeometryMultiPointFromText(DbExpression multiPointWellKnownText, DbExpression coordinateSystemId)
		{
			Check.NotNull<DbExpression>(multiPointWellKnownText, "multiPointWellKnownText");
			Check.NotNull<DbExpression>(coordinateSystemId, "coordinateSystemId");
			return EdmFunctions.InvokeCanonicalFunction("GeometryMultiPointFromText", new DbExpression[]
			{
				multiPointWellKnownText,
				coordinateSystemId
			});
		}

		// Token: 0x060008D1 RID: 2257 RVA: 0x0002D8F0 File Offset: 0x0002BAF0
		[SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "MultiLine", Justification = "Match OGC, EDM")]
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Multi", Justification = "Match OGC, EDM")]
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "multi", Justification = "Match OGC, EDM")]
		[SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "multiLine", Justification = "Match OGC, EDM")]
		public static DbFunctionExpression GeometryMultiLineFromText(DbExpression multiLineWellKnownText, DbExpression coordinateSystemId)
		{
			Check.NotNull<DbExpression>(multiLineWellKnownText, "multiLineWellKnownText");
			Check.NotNull<DbExpression>(coordinateSystemId, "coordinateSystemId");
			return EdmFunctions.InvokeCanonicalFunction("GeometryMultiLineFromText", new DbExpression[]
			{
				multiLineWellKnownText,
				coordinateSystemId
			});
		}

		// Token: 0x060008D2 RID: 2258 RVA: 0x0002D930 File Offset: 0x0002BB30
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "multi", Justification = "Match OGC, EDM")]
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Multi", Justification = "Match OGC, EDM")]
		public static DbFunctionExpression GeometryMultiPolygonFromText(DbExpression multiPolygonWellKnownText, DbExpression coordinateSystemId)
		{
			Check.NotNull<DbExpression>(multiPolygonWellKnownText, "multiPolygonWellKnownText");
			Check.NotNull<DbExpression>(coordinateSystemId, "coordinateSystemId");
			return EdmFunctions.InvokeCanonicalFunction("GeometryMultiPolygonFromText", new DbExpression[]
			{
				multiPolygonWellKnownText,
				coordinateSystemId
			});
		}

		// Token: 0x060008D3 RID: 2259 RVA: 0x0002D970 File Offset: 0x0002BB70
		public static DbFunctionExpression GeometryCollectionFromText(DbExpression geometryCollectionWellKnownText, DbExpression coordinateSystemId)
		{
			Check.NotNull<DbExpression>(geometryCollectionWellKnownText, "geometryCollectionWellKnownText");
			Check.NotNull<DbExpression>(coordinateSystemId, "coordinateSystemId");
			return EdmFunctions.InvokeCanonicalFunction("GeometryCollectionFromText", new DbExpression[]
			{
				geometryCollectionWellKnownText,
				coordinateSystemId
			});
		}

		// Token: 0x060008D4 RID: 2260 RVA: 0x0002D9B0 File Offset: 0x0002BBB0
		public static DbFunctionExpression GeometryFromBinary(DbExpression wellKnownBinaryValue)
		{
			Check.NotNull<DbExpression>(wellKnownBinaryValue, "wellKnownBinaryValue");
			return EdmFunctions.InvokeCanonicalFunction("GeometryFromBinary", new DbExpression[]
			{
				wellKnownBinaryValue
			});
		}

		// Token: 0x060008D5 RID: 2261 RVA: 0x0002D9E0 File Offset: 0x0002BBE0
		public static DbFunctionExpression GeometryFromBinary(DbExpression wellKnownBinaryValue, DbExpression coordinateSystemId)
		{
			Check.NotNull<DbExpression>(wellKnownBinaryValue, "wellKnownBinaryValue");
			Check.NotNull<DbExpression>(coordinateSystemId, "coordinateSystemId");
			return EdmFunctions.InvokeCanonicalFunction("GeometryFromBinary", new DbExpression[]
			{
				wellKnownBinaryValue,
				coordinateSystemId
			});
		}

		// Token: 0x060008D6 RID: 2262 RVA: 0x0002DA20 File Offset: 0x0002BC20
		public static DbFunctionExpression GeometryPointFromBinary(DbExpression pointWellKnownBinaryValue, DbExpression coordinateSystemId)
		{
			Check.NotNull<DbExpression>(pointWellKnownBinaryValue, "pointWellKnownBinaryValue");
			Check.NotNull<DbExpression>(coordinateSystemId, "coordinateSystemId");
			return EdmFunctions.InvokeCanonicalFunction("GeometryPointFromBinary", new DbExpression[]
			{
				pointWellKnownBinaryValue,
				coordinateSystemId
			});
		}

		// Token: 0x060008D7 RID: 2263 RVA: 0x0002DA60 File Offset: 0x0002BC60
		public static DbFunctionExpression GeometryLineFromBinary(DbExpression lineWellKnownBinaryValue, DbExpression coordinateSystemId)
		{
			Check.NotNull<DbExpression>(lineWellKnownBinaryValue, "lineWellKnownBinaryValue");
			Check.NotNull<DbExpression>(coordinateSystemId, "coordinateSystemId");
			return EdmFunctions.InvokeCanonicalFunction("GeometryLineFromBinary", new DbExpression[]
			{
				lineWellKnownBinaryValue,
				coordinateSystemId
			});
		}

		// Token: 0x060008D8 RID: 2264 RVA: 0x0002DAA0 File Offset: 0x0002BCA0
		public static DbFunctionExpression GeometryPolygonFromBinary(DbExpression polygonWellKnownBinaryValue, DbExpression coordinateSystemId)
		{
			Check.NotNull<DbExpression>(polygonWellKnownBinaryValue, "polygonWellKnownBinaryValue");
			Check.NotNull<DbExpression>(coordinateSystemId, "coordinateSystemId");
			return EdmFunctions.InvokeCanonicalFunction("GeometryPolygonFromBinary", new DbExpression[]
			{
				polygonWellKnownBinaryValue,
				coordinateSystemId
			});
		}

		// Token: 0x060008D9 RID: 2265 RVA: 0x0002DAE0 File Offset: 0x0002BCE0
		[SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "MultiPoint", Justification = "Match OGC, EDM")]
		[SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "multiPoint", Justification = "Match OGC, EDM")]
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Multi", Justification = "Match OGC, EDM")]
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "multi", Justification = "Match OGC, EDM")]
		public static DbFunctionExpression GeometryMultiPointFromBinary(DbExpression multiPointWellKnownBinaryValue, DbExpression coordinateSystemId)
		{
			Check.NotNull<DbExpression>(multiPointWellKnownBinaryValue, "multiPointWellKnownBinaryValue");
			Check.NotNull<DbExpression>(coordinateSystemId, "coordinateSystemId");
			return EdmFunctions.InvokeCanonicalFunction("GeometryMultiPointFromBinary", new DbExpression[]
			{
				multiPointWellKnownBinaryValue,
				coordinateSystemId
			});
		}

		// Token: 0x060008DA RID: 2266 RVA: 0x0002DB20 File Offset: 0x0002BD20
		[SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "MultiLine", Justification = "Match OGC, EDM")]
		[SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "multiLine", Justification = "Match OGC, EDM")]
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "multi", Justification = "Match OGC, EDM")]
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Multi", Justification = "Match OGC, EDM")]
		public static DbFunctionExpression GeometryMultiLineFromBinary(DbExpression multiLineWellKnownBinaryValue, DbExpression coordinateSystemId)
		{
			Check.NotNull<DbExpression>(multiLineWellKnownBinaryValue, "multiLineWellKnownBinaryValue");
			Check.NotNull<DbExpression>(coordinateSystemId, "coordinateSystemId");
			return EdmFunctions.InvokeCanonicalFunction("GeometryMultiLineFromBinary", new DbExpression[]
			{
				multiLineWellKnownBinaryValue,
				coordinateSystemId
			});
		}

		// Token: 0x060008DB RID: 2267 RVA: 0x0002DB60 File Offset: 0x0002BD60
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "multi", Justification = "Match OGC, EDM")]
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Multi", Justification = "Match OGC, EDM")]
		public static DbFunctionExpression GeometryMultiPolygonFromBinary(DbExpression multiPolygonWellKnownBinaryValue, DbExpression coordinateSystemId)
		{
			Check.NotNull<DbExpression>(multiPolygonWellKnownBinaryValue, "multiPolygonWellKnownBinaryValue");
			Check.NotNull<DbExpression>(coordinateSystemId, "coordinateSystemId");
			return EdmFunctions.InvokeCanonicalFunction("GeometryMultiPolygonFromBinary", new DbExpression[]
			{
				multiPolygonWellKnownBinaryValue,
				coordinateSystemId
			});
		}

		// Token: 0x060008DC RID: 2268 RVA: 0x0002DBA0 File Offset: 0x0002BDA0
		public static DbFunctionExpression GeometryCollectionFromBinary(DbExpression geometryCollectionWellKnownBinaryValue, DbExpression coordinateSystemId)
		{
			Check.NotNull<DbExpression>(geometryCollectionWellKnownBinaryValue, "geometryCollectionWellKnownBinaryValue");
			Check.NotNull<DbExpression>(coordinateSystemId, "coordinateSystemId");
			return EdmFunctions.InvokeCanonicalFunction("GeometryCollectionFromBinary", new DbExpression[]
			{
				geometryCollectionWellKnownBinaryValue,
				coordinateSystemId
			});
		}

		// Token: 0x060008DD RID: 2269 RVA: 0x0002DBE0 File Offset: 0x0002BDE0
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Gml", Justification = "Abbreviation more meaningful than what it stands for")]
		public static DbFunctionExpression GeometryFromGml(DbExpression geometryMarkup)
		{
			Check.NotNull<DbExpression>(geometryMarkup, "geometryMarkup");
			return EdmFunctions.InvokeCanonicalFunction("GeometryFromGml", new DbExpression[]
			{
				geometryMarkup
			});
		}

		// Token: 0x060008DE RID: 2270 RVA: 0x0002DC10 File Offset: 0x0002BE10
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Gml", Justification = "Abbreviation more meaningful than what it stands for")]
		public static DbFunctionExpression GeometryFromGml(DbExpression geometryMarkup, DbExpression coordinateSystemId)
		{
			Check.NotNull<DbExpression>(geometryMarkup, "geometryMarkup");
			Check.NotNull<DbExpression>(coordinateSystemId, "coordinateSystemId");
			return EdmFunctions.InvokeCanonicalFunction("GeometryFromGml", new DbExpression[]
			{
				geometryMarkup,
				coordinateSystemId
			});
		}

		// Token: 0x060008DF RID: 2271 RVA: 0x0002DC50 File Offset: 0x0002BE50
		public static DbFunctionExpression GeographyFromText(DbExpression wellKnownText)
		{
			Check.NotNull<DbExpression>(wellKnownText, "wellKnownText");
			return EdmFunctions.InvokeCanonicalFunction("GeographyFromText", new DbExpression[]
			{
				wellKnownText
			});
		}

		// Token: 0x060008E0 RID: 2272 RVA: 0x0002DC80 File Offset: 0x0002BE80
		public static DbFunctionExpression GeographyFromText(DbExpression wellKnownText, DbExpression coordinateSystemId)
		{
			Check.NotNull<DbExpression>(wellKnownText, "wellKnownText");
			Check.NotNull<DbExpression>(coordinateSystemId, "coordinateSystemId");
			return EdmFunctions.InvokeCanonicalFunction("GeographyFromText", new DbExpression[]
			{
				wellKnownText,
				coordinateSystemId
			});
		}

		// Token: 0x060008E1 RID: 2273 RVA: 0x0002DCC0 File Offset: 0x0002BEC0
		public static DbFunctionExpression GeographyPointFromText(DbExpression pointWellKnownText, DbExpression coordinateSystemId)
		{
			Check.NotNull<DbExpression>(pointWellKnownText, "pointWellKnownText");
			Check.NotNull<DbExpression>(coordinateSystemId, "coordinateSystemId");
			return EdmFunctions.InvokeCanonicalFunction("GeographyPointFromText", new DbExpression[]
			{
				pointWellKnownText,
				coordinateSystemId
			});
		}

		// Token: 0x060008E2 RID: 2274 RVA: 0x0002DD00 File Offset: 0x0002BF00
		public static DbFunctionExpression GeographyLineFromText(DbExpression lineWellKnownText, DbExpression coordinateSystemId)
		{
			Check.NotNull<DbExpression>(lineWellKnownText, "lineWellKnownText");
			Check.NotNull<DbExpression>(coordinateSystemId, "coordinateSystemId");
			return EdmFunctions.InvokeCanonicalFunction("GeographyLineFromText", new DbExpression[]
			{
				lineWellKnownText,
				coordinateSystemId
			});
		}

		// Token: 0x060008E3 RID: 2275 RVA: 0x0002DD40 File Offset: 0x0002BF40
		public static DbFunctionExpression GeographyPolygonFromText(DbExpression polygonWellKnownText, DbExpression coordinateSystemId)
		{
			Check.NotNull<DbExpression>(polygonWellKnownText, "polygonWellKnownText");
			Check.NotNull<DbExpression>(coordinateSystemId, "coordinateSystemId");
			return EdmFunctions.InvokeCanonicalFunction("GeographyPolygonFromText", new DbExpression[]
			{
				polygonWellKnownText,
				coordinateSystemId
			});
		}

		// Token: 0x060008E4 RID: 2276 RVA: 0x0002DD80 File Offset: 0x0002BF80
		[SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "MultiPoint", Justification = "Match OGC, EDM")]
		[SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "multiPoint", Justification = "Match OGC, EDM")]
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Multi", Justification = "Match OGC, EDM")]
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "multi", Justification = "Match OGC, EDM")]
		public static DbFunctionExpression GeographyMultiPointFromText(DbExpression multiPointWellKnownText, DbExpression coordinateSystemId)
		{
			Check.NotNull<DbExpression>(multiPointWellKnownText, "multiPointWellKnownText");
			Check.NotNull<DbExpression>(coordinateSystemId, "coordinateSystemId");
			return EdmFunctions.InvokeCanonicalFunction("GeographyMultiPointFromText", new DbExpression[]
			{
				multiPointWellKnownText,
				coordinateSystemId
			});
		}

		// Token: 0x060008E5 RID: 2277 RVA: 0x0002DDC0 File Offset: 0x0002BFC0
		[SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "MultiLine", Justification = "Match OGC, EDM")]
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Multi", Justification = "Match OGC, EDM")]
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "multi", Justification = "Match OGC, EDM")]
		[SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "multiLine", Justification = "Match OGC, EDM")]
		public static DbFunctionExpression GeographyMultiLineFromText(DbExpression multiLineWellKnownText, DbExpression coordinateSystemId)
		{
			Check.NotNull<DbExpression>(multiLineWellKnownText, "multiLineWellKnownText");
			Check.NotNull<DbExpression>(coordinateSystemId, "coordinateSystemId");
			return EdmFunctions.InvokeCanonicalFunction("GeographyMultiLineFromText", new DbExpression[]
			{
				multiLineWellKnownText,
				coordinateSystemId
			});
		}

		// Token: 0x060008E6 RID: 2278 RVA: 0x0002DE00 File Offset: 0x0002C000
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Multi", Justification = "Match OGC, EDM")]
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "multi", Justification = "Match OGC, EDM")]
		public static DbFunctionExpression GeographyMultiPolygonFromText(DbExpression multiPolygonWellKnownText, DbExpression coordinateSystemId)
		{
			Check.NotNull<DbExpression>(multiPolygonWellKnownText, "multiPolygonWellKnownText");
			Check.NotNull<DbExpression>(coordinateSystemId, "coordinateSystemId");
			return EdmFunctions.InvokeCanonicalFunction("GeographyMultiPolygonFromText", new DbExpression[]
			{
				multiPolygonWellKnownText,
				coordinateSystemId
			});
		}

		// Token: 0x060008E7 RID: 2279 RVA: 0x0002DE40 File Offset: 0x0002C040
		public static DbFunctionExpression GeographyCollectionFromText(DbExpression geographyCollectionWellKnownText, DbExpression coordinateSystemId)
		{
			Check.NotNull<DbExpression>(geographyCollectionWellKnownText, "geographyCollectionWellKnownText");
			Check.NotNull<DbExpression>(coordinateSystemId, "coordinateSystemId");
			return EdmFunctions.InvokeCanonicalFunction("GeographyCollectionFromText", new DbExpression[]
			{
				geographyCollectionWellKnownText,
				coordinateSystemId
			});
		}

		// Token: 0x060008E8 RID: 2280 RVA: 0x0002DE80 File Offset: 0x0002C080
		public static DbFunctionExpression GeographyFromBinary(DbExpression wellKnownBinaryValue)
		{
			Check.NotNull<DbExpression>(wellKnownBinaryValue, "wellKnownBinaryValue");
			return EdmFunctions.InvokeCanonicalFunction("GeographyFromBinary", new DbExpression[]
			{
				wellKnownBinaryValue
			});
		}

		// Token: 0x060008E9 RID: 2281 RVA: 0x0002DEB0 File Offset: 0x0002C0B0
		public static DbFunctionExpression GeographyFromBinary(DbExpression wellKnownBinaryValue, DbExpression coordinateSystemId)
		{
			Check.NotNull<DbExpression>(wellKnownBinaryValue, "wellKnownBinaryValue");
			Check.NotNull<DbExpression>(coordinateSystemId, "coordinateSystemId");
			return EdmFunctions.InvokeCanonicalFunction("GeographyFromBinary", new DbExpression[]
			{
				wellKnownBinaryValue,
				coordinateSystemId
			});
		}

		// Token: 0x060008EA RID: 2282 RVA: 0x0002DEF0 File Offset: 0x0002C0F0
		public static DbFunctionExpression GeographyPointFromBinary(DbExpression pointWellKnownBinaryValue, DbExpression coordinateSystemId)
		{
			Check.NotNull<DbExpression>(pointWellKnownBinaryValue, "pointWellKnownBinaryValue");
			Check.NotNull<DbExpression>(coordinateSystemId, "coordinateSystemId");
			return EdmFunctions.InvokeCanonicalFunction("GeographyPointFromBinary", new DbExpression[]
			{
				pointWellKnownBinaryValue,
				coordinateSystemId
			});
		}

		// Token: 0x060008EB RID: 2283 RVA: 0x0002DF30 File Offset: 0x0002C130
		public static DbFunctionExpression GeographyLineFromBinary(DbExpression lineWellKnownBinaryValue, DbExpression coordinateSystemId)
		{
			Check.NotNull<DbExpression>(lineWellKnownBinaryValue, "lineWellKnownBinaryValue");
			Check.NotNull<DbExpression>(coordinateSystemId, "coordinateSystemId");
			return EdmFunctions.InvokeCanonicalFunction("GeographyLineFromBinary", new DbExpression[]
			{
				lineWellKnownBinaryValue,
				coordinateSystemId
			});
		}

		// Token: 0x060008EC RID: 2284 RVA: 0x0002DF70 File Offset: 0x0002C170
		public static DbFunctionExpression GeographyPolygonFromBinary(DbExpression polygonWellKnownBinaryValue, DbExpression coordinateSystemId)
		{
			Check.NotNull<DbExpression>(polygonWellKnownBinaryValue, "polygonWellKnownBinaryValue");
			Check.NotNull<DbExpression>(coordinateSystemId, "coordinateSystemId");
			return EdmFunctions.InvokeCanonicalFunction("GeographyPolygonFromBinary", new DbExpression[]
			{
				polygonWellKnownBinaryValue,
				coordinateSystemId
			});
		}

		// Token: 0x060008ED RID: 2285 RVA: 0x0002DFB0 File Offset: 0x0002C1B0
		[SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "MultiPoint", Justification = "Match OGC, EDM")]
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "multi", Justification = "Match OGC, EDM")]
		[SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "multiPoint", Justification = "Match OGC, EDM")]
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Multi", Justification = "Match OGC, EDM")]
		public static DbFunctionExpression GeographyMultiPointFromBinary(DbExpression multiPointWellKnownBinaryValue, DbExpression coordinateSystemId)
		{
			Check.NotNull<DbExpression>(multiPointWellKnownBinaryValue, "multiPointWellKnownBinaryValue");
			Check.NotNull<DbExpression>(coordinateSystemId, "coordinateSystemId");
			return EdmFunctions.InvokeCanonicalFunction("GeographyMultiPointFromBinary", new DbExpression[]
			{
				multiPointWellKnownBinaryValue,
				coordinateSystemId
			});
		}

		// Token: 0x060008EE RID: 2286 RVA: 0x0002DFF0 File Offset: 0x0002C1F0
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "multi", Justification = "Match OGC, EDM")]
		[SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "multiLine", Justification = "Match OGC, EDM")]
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Multi", Justification = "Match OGC, EDM")]
		[SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "MultiLine", Justification = "Match OGC, EDM")]
		public static DbFunctionExpression GeographyMultiLineFromBinary(DbExpression multiLineWellKnownBinaryValue, DbExpression coordinateSystemId)
		{
			Check.NotNull<DbExpression>(multiLineWellKnownBinaryValue, "multiLineWellKnownBinaryValue");
			Check.NotNull<DbExpression>(coordinateSystemId, "coordinateSystemId");
			return EdmFunctions.InvokeCanonicalFunction("GeographyMultiLineFromBinary", new DbExpression[]
			{
				multiLineWellKnownBinaryValue,
				coordinateSystemId
			});
		}

		// Token: 0x060008EF RID: 2287 RVA: 0x0002E030 File Offset: 0x0002C230
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "multi", Justification = "Match OGC, EDM")]
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Multi", Justification = "Match OGC, EDM")]
		public static DbFunctionExpression GeographyMultiPolygonFromBinary(DbExpression multiPolygonWellKnownBinaryValue, DbExpression coordinateSystemId)
		{
			Check.NotNull<DbExpression>(multiPolygonWellKnownBinaryValue, "multiPolygonWellKnownBinaryValue");
			Check.NotNull<DbExpression>(coordinateSystemId, "coordinateSystemId");
			return EdmFunctions.InvokeCanonicalFunction("GeographyMultiPolygonFromBinary", new DbExpression[]
			{
				multiPolygonWellKnownBinaryValue,
				coordinateSystemId
			});
		}

		// Token: 0x060008F0 RID: 2288 RVA: 0x0002E070 File Offset: 0x0002C270
		public static DbFunctionExpression GeographyCollectionFromBinary(DbExpression geographyCollectionWellKnownBinaryValue, DbExpression coordinateSystemId)
		{
			Check.NotNull<DbExpression>(geographyCollectionWellKnownBinaryValue, "geographyCollectionWellKnownBinaryValue");
			Check.NotNull<DbExpression>(coordinateSystemId, "coordinateSystemId");
			return EdmFunctions.InvokeCanonicalFunction("GeographyCollectionFromBinary", new DbExpression[]
			{
				geographyCollectionWellKnownBinaryValue,
				coordinateSystemId
			});
		}

		// Token: 0x060008F1 RID: 2289 RVA: 0x0002E0B0 File Offset: 0x0002C2B0
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Gml")]
		public static DbFunctionExpression GeographyFromGml(DbExpression geographyMarkup)
		{
			Check.NotNull<DbExpression>(geographyMarkup, "geographyMarkup");
			return EdmFunctions.InvokeCanonicalFunction("GeographyFromGml", new DbExpression[]
			{
				geographyMarkup
			});
		}

		// Token: 0x060008F2 RID: 2290 RVA: 0x0002E0E0 File Offset: 0x0002C2E0
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Gml")]
		public static DbFunctionExpression GeographyFromGml(DbExpression geographyMarkup, DbExpression coordinateSystemId)
		{
			Check.NotNull<DbExpression>(geographyMarkup, "geographyMarkup");
			Check.NotNull<DbExpression>(coordinateSystemId, "coordinateSystemId");
			return EdmFunctions.InvokeCanonicalFunction("GeographyFromGml", new DbExpression[]
			{
				geographyMarkup,
				coordinateSystemId
			});
		}

		// Token: 0x060008F3 RID: 2291 RVA: 0x0002E120 File Offset: 0x0002C320
		public static DbFunctionExpression CoordinateSystemId(this DbExpression spatialValue)
		{
			Check.NotNull<DbExpression>(spatialValue, "spatialValue");
			return EdmFunctions.InvokeCanonicalFunction("CoordinateSystemId", new DbExpression[]
			{
				spatialValue
			});
		}

		// Token: 0x060008F4 RID: 2292 RVA: 0x0002E150 File Offset: 0x0002C350
		public static DbFunctionExpression SpatialTypeName(this DbExpression spatialValue)
		{
			Check.NotNull<DbExpression>(spatialValue, "spatialValue");
			return EdmFunctions.InvokeCanonicalFunction("SpatialTypeName", new DbExpression[]
			{
				spatialValue
			});
		}

		// Token: 0x060008F5 RID: 2293 RVA: 0x0002E180 File Offset: 0x0002C380
		public static DbFunctionExpression SpatialDimension(this DbExpression spatialValue)
		{
			Check.NotNull<DbExpression>(spatialValue, "spatialValue");
			return EdmFunctions.InvokeCanonicalFunction("SpatialDimension", new DbExpression[]
			{
				spatialValue
			});
		}

		// Token: 0x060008F6 RID: 2294 RVA: 0x0002E1B0 File Offset: 0x0002C3B0
		public static DbFunctionExpression SpatialEnvelope(this DbExpression geometryValue)
		{
			Check.NotNull<DbExpression>(geometryValue, "geometryValue");
			return EdmFunctions.InvokeCanonicalFunction("SpatialEnvelope", new DbExpression[]
			{
				geometryValue
			});
		}

		// Token: 0x060008F7 RID: 2295 RVA: 0x0002E1E0 File Offset: 0x0002C3E0
		public static DbFunctionExpression AsBinary(this DbExpression spatialValue)
		{
			Check.NotNull<DbExpression>(spatialValue, "spatialValue");
			return EdmFunctions.InvokeCanonicalFunction("AsBinary", new DbExpression[]
			{
				spatialValue
			});
		}

		// Token: 0x060008F8 RID: 2296 RVA: 0x0002E210 File Offset: 0x0002C410
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Gml")]
		public static DbFunctionExpression AsGml(this DbExpression spatialValue)
		{
			Check.NotNull<DbExpression>(spatialValue, "spatialValue");
			return EdmFunctions.InvokeCanonicalFunction("AsGml", new DbExpression[]
			{
				spatialValue
			});
		}

		// Token: 0x060008F9 RID: 2297 RVA: 0x0002E240 File Offset: 0x0002C440
		public static DbFunctionExpression AsText(this DbExpression spatialValue)
		{
			Check.NotNull<DbExpression>(spatialValue, "spatialValue");
			return EdmFunctions.InvokeCanonicalFunction("AsText", new DbExpression[]
			{
				spatialValue
			});
		}

		// Token: 0x060008FA RID: 2298 RVA: 0x0002E270 File Offset: 0x0002C470
		public static DbFunctionExpression IsEmptySpatial(this DbExpression spatialValue)
		{
			Check.NotNull<DbExpression>(spatialValue, "spatialValue");
			return EdmFunctions.InvokeCanonicalFunction("IsEmptySpatial", new DbExpression[]
			{
				spatialValue
			});
		}

		// Token: 0x060008FB RID: 2299 RVA: 0x0002E2A0 File Offset: 0x0002C4A0
		public static DbFunctionExpression IsSimpleGeometry(this DbExpression geometryValue)
		{
			Check.NotNull<DbExpression>(geometryValue, "geometryValue");
			return EdmFunctions.InvokeCanonicalFunction("IsSimpleGeometry", new DbExpression[]
			{
				geometryValue
			});
		}

		// Token: 0x060008FC RID: 2300 RVA: 0x0002E2D0 File Offset: 0x0002C4D0
		public static DbFunctionExpression SpatialBoundary(this DbExpression geometryValue)
		{
			Check.NotNull<DbExpression>(geometryValue, "geometryValue");
			return EdmFunctions.InvokeCanonicalFunction("SpatialBoundary", new DbExpression[]
			{
				geometryValue
			});
		}

		// Token: 0x060008FD RID: 2301 RVA: 0x0002E300 File Offset: 0x0002C500
		public static DbFunctionExpression IsValidGeometry(this DbExpression geometryValue)
		{
			Check.NotNull<DbExpression>(geometryValue, "geometryValue");
			return EdmFunctions.InvokeCanonicalFunction("IsValidGeometry", new DbExpression[]
			{
				geometryValue
			});
		}

		// Token: 0x060008FE RID: 2302 RVA: 0x0002E330 File Offset: 0x0002C530
		public static DbFunctionExpression SpatialEquals(this DbExpression spatialValue1, DbExpression spatialValue2)
		{
			Check.NotNull<DbExpression>(spatialValue1, "spatialValue1");
			Check.NotNull<DbExpression>(spatialValue2, "spatialValue2");
			return EdmFunctions.InvokeCanonicalFunction("SpatialEquals", new DbExpression[]
			{
				spatialValue1,
				spatialValue2
			});
		}

		// Token: 0x060008FF RID: 2303 RVA: 0x0002E370 File Offset: 0x0002C570
		public static DbFunctionExpression SpatialDisjoint(this DbExpression spatialValue1, DbExpression spatialValue2)
		{
			Check.NotNull<DbExpression>(spatialValue1, "spatialValue1");
			Check.NotNull<DbExpression>(spatialValue2, "spatialValue2");
			return EdmFunctions.InvokeCanonicalFunction("SpatialDisjoint", new DbExpression[]
			{
				spatialValue1,
				spatialValue2
			});
		}

		// Token: 0x06000900 RID: 2304 RVA: 0x0002E3B0 File Offset: 0x0002C5B0
		public static DbFunctionExpression SpatialIntersects(this DbExpression spatialValue1, DbExpression spatialValue2)
		{
			Check.NotNull<DbExpression>(spatialValue1, "spatialValue1");
			Check.NotNull<DbExpression>(spatialValue2, "spatialValue2");
			return EdmFunctions.InvokeCanonicalFunction("SpatialIntersects", new DbExpression[]
			{
				spatialValue1,
				spatialValue2
			});
		}

		// Token: 0x06000901 RID: 2305 RVA: 0x0002E3F0 File Offset: 0x0002C5F0
		public static DbFunctionExpression SpatialTouches(this DbExpression geometryValue1, DbExpression geometryValue2)
		{
			Check.NotNull<DbExpression>(geometryValue1, "geometryValue1");
			Check.NotNull<DbExpression>(geometryValue2, "geometryValue2");
			return EdmFunctions.InvokeCanonicalFunction("SpatialTouches", new DbExpression[]
			{
				geometryValue1,
				geometryValue2
			});
		}

		// Token: 0x06000902 RID: 2306 RVA: 0x0002E430 File Offset: 0x0002C630
		public static DbFunctionExpression SpatialCrosses(this DbExpression geometryValue1, DbExpression geometryValue2)
		{
			Check.NotNull<DbExpression>(geometryValue1, "geometryValue1");
			Check.NotNull<DbExpression>(geometryValue2, "geometryValue2");
			return EdmFunctions.InvokeCanonicalFunction("SpatialCrosses", new DbExpression[]
			{
				geometryValue1,
				geometryValue2
			});
		}

		// Token: 0x06000903 RID: 2307 RVA: 0x0002E470 File Offset: 0x0002C670
		public static DbFunctionExpression SpatialWithin(this DbExpression geometryValue1, DbExpression geometryValue2)
		{
			Check.NotNull<DbExpression>(geometryValue1, "geometryValue1");
			Check.NotNull<DbExpression>(geometryValue2, "geometryValue2");
			return EdmFunctions.InvokeCanonicalFunction("SpatialWithin", new DbExpression[]
			{
				geometryValue1,
				geometryValue2
			});
		}

		// Token: 0x06000904 RID: 2308 RVA: 0x0002E4B0 File Offset: 0x0002C6B0
		public static DbFunctionExpression SpatialContains(this DbExpression geometryValue1, DbExpression geometryValue2)
		{
			Check.NotNull<DbExpression>(geometryValue1, "geometryValue1");
			Check.NotNull<DbExpression>(geometryValue2, "geometryValue2");
			return EdmFunctions.InvokeCanonicalFunction("SpatialContains", new DbExpression[]
			{
				geometryValue1,
				geometryValue2
			});
		}

		// Token: 0x06000905 RID: 2309 RVA: 0x0002E4F0 File Offset: 0x0002C6F0
		public static DbFunctionExpression SpatialOverlaps(this DbExpression geometryValue1, DbExpression geometryValue2)
		{
			Check.NotNull<DbExpression>(geometryValue1, "geometryValue1");
			Check.NotNull<DbExpression>(geometryValue2, "geometryValue2");
			return EdmFunctions.InvokeCanonicalFunction("SpatialOverlaps", new DbExpression[]
			{
				geometryValue1,
				geometryValue2
			});
		}

		// Token: 0x06000906 RID: 2310 RVA: 0x0002E530 File Offset: 0x0002C730
		public static DbFunctionExpression SpatialRelate(this DbExpression geometryValue1, DbExpression geometryValue2, DbExpression intersectionPatternMatrix)
		{
			Check.NotNull<DbExpression>(geometryValue1, "geometryValue1");
			Check.NotNull<DbExpression>(geometryValue2, "geometryValue2");
			Check.NotNull<DbExpression>(intersectionPatternMatrix, "intersectionPatternMatrix");
			return EdmFunctions.InvokeCanonicalFunction("SpatialRelate", new DbExpression[]
			{
				geometryValue1,
				geometryValue2,
				intersectionPatternMatrix
			});
		}

		// Token: 0x06000907 RID: 2311 RVA: 0x0002E580 File Offset: 0x0002C780
		public static DbFunctionExpression SpatialBuffer(this DbExpression spatialValue, DbExpression distance)
		{
			Check.NotNull<DbExpression>(spatialValue, "spatialValue");
			Check.NotNull<DbExpression>(distance, "distance");
			return EdmFunctions.InvokeCanonicalFunction("SpatialBuffer", new DbExpression[]
			{
				spatialValue,
				distance
			});
		}

		// Token: 0x06000908 RID: 2312 RVA: 0x0002E5C0 File Offset: 0x0002C7C0
		public static DbFunctionExpression Distance(this DbExpression spatialValue1, DbExpression spatialValue2)
		{
			Check.NotNull<DbExpression>(spatialValue1, "spatialValue1");
			Check.NotNull<DbExpression>(spatialValue2, "spatialValue2");
			return EdmFunctions.InvokeCanonicalFunction("Distance", new DbExpression[]
			{
				spatialValue1,
				spatialValue2
			});
		}

		// Token: 0x06000909 RID: 2313 RVA: 0x0002E600 File Offset: 0x0002C800
		public static DbFunctionExpression SpatialConvexHull(this DbExpression geometryValue)
		{
			Check.NotNull<DbExpression>(geometryValue, "geometryValue");
			return EdmFunctions.InvokeCanonicalFunction("SpatialConvexHull", new DbExpression[]
			{
				geometryValue
			});
		}

		// Token: 0x0600090A RID: 2314 RVA: 0x0002E630 File Offset: 0x0002C830
		public static DbFunctionExpression SpatialIntersection(this DbExpression spatialValue1, DbExpression spatialValue2)
		{
			Check.NotNull<DbExpression>(spatialValue1, "spatialValue1");
			Check.NotNull<DbExpression>(spatialValue2, "spatialValue2");
			return EdmFunctions.InvokeCanonicalFunction("SpatialIntersection", new DbExpression[]
			{
				spatialValue1,
				spatialValue2
			});
		}

		// Token: 0x0600090B RID: 2315 RVA: 0x0002E670 File Offset: 0x0002C870
		public static DbFunctionExpression SpatialUnion(this DbExpression spatialValue1, DbExpression spatialValue2)
		{
			Check.NotNull<DbExpression>(spatialValue1, "spatialValue1");
			Check.NotNull<DbExpression>(spatialValue2, "spatialValue2");
			return EdmFunctions.InvokeCanonicalFunction("SpatialUnion", new DbExpression[]
			{
				spatialValue1,
				spatialValue2
			});
		}

		// Token: 0x0600090C RID: 2316 RVA: 0x0002E6B0 File Offset: 0x0002C8B0
		public static DbFunctionExpression SpatialDifference(this DbExpression spatialValue1, DbExpression spatialValue2)
		{
			Check.NotNull<DbExpression>(spatialValue1, "spatialValue1");
			Check.NotNull<DbExpression>(spatialValue2, "spatialValue2");
			return EdmFunctions.InvokeCanonicalFunction("SpatialDifference", new DbExpression[]
			{
				spatialValue1,
				spatialValue2
			});
		}

		// Token: 0x0600090D RID: 2317 RVA: 0x0002E6F0 File Offset: 0x0002C8F0
		public static DbFunctionExpression SpatialSymmetricDifference(this DbExpression spatialValue1, DbExpression spatialValue2)
		{
			Check.NotNull<DbExpression>(spatialValue1, "spatialValue1");
			Check.NotNull<DbExpression>(spatialValue2, "spatialValue2");
			return EdmFunctions.InvokeCanonicalFunction("SpatialSymmetricDifference", new DbExpression[]
			{
				spatialValue1,
				spatialValue2
			});
		}

		// Token: 0x0600090E RID: 2318 RVA: 0x0002E730 File Offset: 0x0002C930
		public static DbFunctionExpression SpatialElementCount(this DbExpression spatialValue)
		{
			Check.NotNull<DbExpression>(spatialValue, "spatialValue");
			return EdmFunctions.InvokeCanonicalFunction("SpatialElementCount", new DbExpression[]
			{
				spatialValue
			});
		}

		// Token: 0x0600090F RID: 2319 RVA: 0x0002E760 File Offset: 0x0002C960
		public static DbFunctionExpression SpatialElementAt(this DbExpression spatialValue, DbExpression indexValue)
		{
			Check.NotNull<DbExpression>(spatialValue, "spatialValue");
			Check.NotNull<DbExpression>(indexValue, "indexValue");
			return EdmFunctions.InvokeCanonicalFunction("SpatialElementAt", new DbExpression[]
			{
				spatialValue,
				indexValue
			});
		}

		// Token: 0x06000910 RID: 2320 RVA: 0x0002E7A0 File Offset: 0x0002C9A0
		public static DbFunctionExpression XCoordinate(this DbExpression geometryValue)
		{
			Check.NotNull<DbExpression>(geometryValue, "geometryValue");
			return EdmFunctions.InvokeCanonicalFunction("XCoordinate", new DbExpression[]
			{
				geometryValue
			});
		}

		// Token: 0x06000911 RID: 2321 RVA: 0x0002E7D0 File Offset: 0x0002C9D0
		public static DbFunctionExpression YCoordinate(this DbExpression geometryValue)
		{
			Check.NotNull<DbExpression>(geometryValue, "geometryValue");
			return EdmFunctions.InvokeCanonicalFunction("YCoordinate", new DbExpression[]
			{
				geometryValue
			});
		}

		// Token: 0x06000912 RID: 2322 RVA: 0x0002E800 File Offset: 0x0002CA00
		public static DbFunctionExpression Elevation(this DbExpression spatialValue)
		{
			Check.NotNull<DbExpression>(spatialValue, "spatialValue");
			return EdmFunctions.InvokeCanonicalFunction("Elevation", new DbExpression[]
			{
				spatialValue
			});
		}

		// Token: 0x06000913 RID: 2323 RVA: 0x0002E830 File Offset: 0x0002CA30
		public static DbFunctionExpression Measure(this DbExpression spatialValue)
		{
			Check.NotNull<DbExpression>(spatialValue, "spatialValue");
			return EdmFunctions.InvokeCanonicalFunction("Measure", new DbExpression[]
			{
				spatialValue
			});
		}

		// Token: 0x06000914 RID: 2324 RVA: 0x0002E860 File Offset: 0x0002CA60
		public static DbFunctionExpression Latitude(this DbExpression geographyValue)
		{
			Check.NotNull<DbExpression>(geographyValue, "geographyValue");
			return EdmFunctions.InvokeCanonicalFunction("Latitude", new DbExpression[]
			{
				geographyValue
			});
		}

		// Token: 0x06000915 RID: 2325 RVA: 0x0002E890 File Offset: 0x0002CA90
		public static DbFunctionExpression Longitude(this DbExpression geographyValue)
		{
			Check.NotNull<DbExpression>(geographyValue, "geographyValue");
			return EdmFunctions.InvokeCanonicalFunction("Longitude", new DbExpression[]
			{
				geographyValue
			});
		}

		// Token: 0x06000916 RID: 2326 RVA: 0x0002E8C0 File Offset: 0x0002CAC0
		public static DbFunctionExpression SpatialLength(this DbExpression spatialValue)
		{
			Check.NotNull<DbExpression>(spatialValue, "spatialValue");
			return EdmFunctions.InvokeCanonicalFunction("SpatialLength", new DbExpression[]
			{
				spatialValue
			});
		}

		// Token: 0x06000917 RID: 2327 RVA: 0x0002E8F0 File Offset: 0x0002CAF0
		public static DbFunctionExpression StartPoint(this DbExpression spatialValue)
		{
			Check.NotNull<DbExpression>(spatialValue, "spatialValue");
			return EdmFunctions.InvokeCanonicalFunction("StartPoint", new DbExpression[]
			{
				spatialValue
			});
		}

		// Token: 0x06000918 RID: 2328 RVA: 0x0002E920 File Offset: 0x0002CB20
		public static DbFunctionExpression EndPoint(this DbExpression spatialValue)
		{
			Check.NotNull<DbExpression>(spatialValue, "spatialValue");
			return EdmFunctions.InvokeCanonicalFunction("EndPoint", new DbExpression[]
			{
				spatialValue
			});
		}

		// Token: 0x06000919 RID: 2329 RVA: 0x0002E950 File Offset: 0x0002CB50
		public static DbFunctionExpression IsClosedSpatial(this DbExpression spatialValue)
		{
			Check.NotNull<DbExpression>(spatialValue, "spatialValue");
			return EdmFunctions.InvokeCanonicalFunction("IsClosedSpatial", new DbExpression[]
			{
				spatialValue
			});
		}

		// Token: 0x0600091A RID: 2330 RVA: 0x0002E980 File Offset: 0x0002CB80
		public static DbFunctionExpression IsRing(this DbExpression geometryValue)
		{
			Check.NotNull<DbExpression>(geometryValue, "geometryValue");
			return EdmFunctions.InvokeCanonicalFunction("IsRing", new DbExpression[]
			{
				geometryValue
			});
		}

		// Token: 0x0600091B RID: 2331 RVA: 0x0002E9B0 File Offset: 0x0002CBB0
		public static DbFunctionExpression PointCount(this DbExpression spatialValue)
		{
			Check.NotNull<DbExpression>(spatialValue, "spatialValue");
			return EdmFunctions.InvokeCanonicalFunction("PointCount", new DbExpression[]
			{
				spatialValue
			});
		}

		// Token: 0x0600091C RID: 2332 RVA: 0x0002E9E0 File Offset: 0x0002CBE0
		public static DbFunctionExpression PointAt(this DbExpression spatialValue, DbExpression indexValue)
		{
			Check.NotNull<DbExpression>(spatialValue, "spatialValue");
			Check.NotNull<DbExpression>(indexValue, "indexValue");
			return EdmFunctions.InvokeCanonicalFunction("PointAt", new DbExpression[]
			{
				spatialValue,
				indexValue
			});
		}

		// Token: 0x0600091D RID: 2333 RVA: 0x0002EA20 File Offset: 0x0002CC20
		public static DbFunctionExpression Area(this DbExpression spatialValue)
		{
			Check.NotNull<DbExpression>(spatialValue, "spatialValue");
			return EdmFunctions.InvokeCanonicalFunction("Area", new DbExpression[]
			{
				spatialValue
			});
		}

		// Token: 0x0600091E RID: 2334 RVA: 0x0002EA50 File Offset: 0x0002CC50
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Centroid", Justification = "Standard bame")]
		public static DbFunctionExpression Centroid(this DbExpression geometryValue)
		{
			Check.NotNull<DbExpression>(geometryValue, "geometryValue");
			return EdmFunctions.InvokeCanonicalFunction("Centroid", new DbExpression[]
			{
				geometryValue
			});
		}

		// Token: 0x0600091F RID: 2335 RVA: 0x0002EA80 File Offset: 0x0002CC80
		public static DbFunctionExpression PointOnSurface(this DbExpression geometryValue)
		{
			Check.NotNull<DbExpression>(geometryValue, "geometryValue");
			return EdmFunctions.InvokeCanonicalFunction("PointOnSurface", new DbExpression[]
			{
				geometryValue
			});
		}

		// Token: 0x06000920 RID: 2336 RVA: 0x0002EAB0 File Offset: 0x0002CCB0
		public static DbFunctionExpression ExteriorRing(this DbExpression geometryValue)
		{
			Check.NotNull<DbExpression>(geometryValue, "geometryValue");
			return EdmFunctions.InvokeCanonicalFunction("ExteriorRing", new DbExpression[]
			{
				geometryValue
			});
		}

		// Token: 0x06000921 RID: 2337 RVA: 0x0002EAE0 File Offset: 0x0002CCE0
		public static DbFunctionExpression InteriorRingCount(this DbExpression geometryValue)
		{
			Check.NotNull<DbExpression>(geometryValue, "geometryValue");
			return EdmFunctions.InvokeCanonicalFunction("InteriorRingCount", new DbExpression[]
			{
				geometryValue
			});
		}

		// Token: 0x06000922 RID: 2338 RVA: 0x0002EB10 File Offset: 0x0002CD10
		public static DbFunctionExpression InteriorRingAt(this DbExpression geometryValue, DbExpression indexValue)
		{
			Check.NotNull<DbExpression>(geometryValue, "geometryValue");
			Check.NotNull<DbExpression>(indexValue, "indexValue");
			return EdmFunctions.InvokeCanonicalFunction("InteriorRingAt", new DbExpression[]
			{
				geometryValue,
				indexValue
			});
		}
	}
}
