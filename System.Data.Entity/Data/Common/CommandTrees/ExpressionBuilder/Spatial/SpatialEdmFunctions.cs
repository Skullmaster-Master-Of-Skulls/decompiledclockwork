using System;

namespace System.Data.Common.CommandTrees.ExpressionBuilder.Spatial
{
	// Token: 0x0200042C RID: 1068
	public static class SpatialEdmFunctions
	{
		// Token: 0x060038EB RID: 14571 RVA: 0x000D85B0 File Offset: 0x000D67B0
		public static DbFunctionExpression GeometryFromText(DbExpression wellKnownText)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(wellKnownText, "wellKnownText");
			return EdmFunctions.InvokeCanonicalFunction("GeometryFromText", new DbExpression[]
			{
				wellKnownText
			});
		}

		// Token: 0x060038EC RID: 14572 RVA: 0x000D85D2 File Offset: 0x000D67D2
		public static DbFunctionExpression GeometryFromText(DbExpression wellKnownText, DbExpression coordinateSystemId)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(wellKnownText, "wellKnownText");
			EntityUtil.CheckArgumentNull<DbExpression>(coordinateSystemId, "coordinateSystemId");
			return EdmFunctions.InvokeCanonicalFunction("GeometryFromText", new DbExpression[]
			{
				wellKnownText,
				coordinateSystemId
			});
		}

		// Token: 0x060038ED RID: 14573 RVA: 0x000D8604 File Offset: 0x000D6804
		public static DbFunctionExpression GeometryPointFromText(DbExpression pointWellKnownText, DbExpression coordinateSystemId)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(pointWellKnownText, "pointWellKnownText");
			EntityUtil.CheckArgumentNull<DbExpression>(coordinateSystemId, "coordinateSystemId");
			return EdmFunctions.InvokeCanonicalFunction("GeometryPointFromText", new DbExpression[]
			{
				pointWellKnownText,
				coordinateSystemId
			});
		}

		// Token: 0x060038EE RID: 14574 RVA: 0x000D8636 File Offset: 0x000D6836
		public static DbFunctionExpression GeometryLineFromText(DbExpression lineWellKnownText, DbExpression coordinateSystemId)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(lineWellKnownText, "lineWellKnownText");
			EntityUtil.CheckArgumentNull<DbExpression>(coordinateSystemId, "coordinateSystemId");
			return EdmFunctions.InvokeCanonicalFunction("GeometryLineFromText", new DbExpression[]
			{
				lineWellKnownText,
				coordinateSystemId
			});
		}

		// Token: 0x060038EF RID: 14575 RVA: 0x000D8668 File Offset: 0x000D6868
		public static DbFunctionExpression GeometryPolygonFromText(DbExpression polygonWellKnownText, DbExpression coordinateSystemId)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(polygonWellKnownText, "polygonWellKnownText");
			EntityUtil.CheckArgumentNull<DbExpression>(coordinateSystemId, "coordinateSystemId");
			return EdmFunctions.InvokeCanonicalFunction("GeometryPolygonFromText", new DbExpression[]
			{
				polygonWellKnownText,
				coordinateSystemId
			});
		}

		// Token: 0x060038F0 RID: 14576 RVA: 0x000D869A File Offset: 0x000D689A
		public static DbFunctionExpression GeometryMultiPointFromText(DbExpression multiPointWellKnownText, DbExpression coordinateSystemId)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(multiPointWellKnownText, "multiPointWellKnownText");
			EntityUtil.CheckArgumentNull<DbExpression>(coordinateSystemId, "coordinateSystemId");
			return EdmFunctions.InvokeCanonicalFunction("GeometryMultiPointFromText", new DbExpression[]
			{
				multiPointWellKnownText,
				coordinateSystemId
			});
		}

		// Token: 0x060038F1 RID: 14577 RVA: 0x000D86CC File Offset: 0x000D68CC
		public static DbFunctionExpression GeometryMultiLineFromText(DbExpression multiLineWellKnownText, DbExpression coordinateSystemId)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(multiLineWellKnownText, "multiLineWellKnownText");
			EntityUtil.CheckArgumentNull<DbExpression>(coordinateSystemId, "coordinateSystemId");
			return EdmFunctions.InvokeCanonicalFunction("GeometryMultiLineFromText", new DbExpression[]
			{
				multiLineWellKnownText,
				coordinateSystemId
			});
		}

		// Token: 0x060038F2 RID: 14578 RVA: 0x000D86FE File Offset: 0x000D68FE
		public static DbFunctionExpression GeometryMultiPolygonFromText(DbExpression multiPolygonWellKnownText, DbExpression coordinateSystemId)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(multiPolygonWellKnownText, "multiPolygonWellKnownText");
			EntityUtil.CheckArgumentNull<DbExpression>(coordinateSystemId, "coordinateSystemId");
			return EdmFunctions.InvokeCanonicalFunction("GeometryMultiPolygonFromText", new DbExpression[]
			{
				multiPolygonWellKnownText,
				coordinateSystemId
			});
		}

		// Token: 0x060038F3 RID: 14579 RVA: 0x000D8730 File Offset: 0x000D6930
		public static DbFunctionExpression GeometryCollectionFromText(DbExpression geometryCollectionWellKnownText, DbExpression coordinateSystemId)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(geometryCollectionWellKnownText, "geometryCollectionWellKnownText");
			EntityUtil.CheckArgumentNull<DbExpression>(coordinateSystemId, "coordinateSystemId");
			return EdmFunctions.InvokeCanonicalFunction("GeometryCollectionFromText", new DbExpression[]
			{
				geometryCollectionWellKnownText,
				coordinateSystemId
			});
		}

		// Token: 0x060038F4 RID: 14580 RVA: 0x000D8762 File Offset: 0x000D6962
		public static DbFunctionExpression GeometryFromBinary(DbExpression wellKnownBinaryValue)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(wellKnownBinaryValue, "wellKnownBinaryValue");
			return EdmFunctions.InvokeCanonicalFunction("GeometryFromBinary", new DbExpression[]
			{
				wellKnownBinaryValue
			});
		}

		// Token: 0x060038F5 RID: 14581 RVA: 0x000D8784 File Offset: 0x000D6984
		public static DbFunctionExpression GeometryFromBinary(DbExpression wellKnownBinaryValue, DbExpression coordinateSystemId)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(wellKnownBinaryValue, "wellKnownBinaryValue");
			EntityUtil.CheckArgumentNull<DbExpression>(coordinateSystemId, "coordinateSystemId");
			return EdmFunctions.InvokeCanonicalFunction("GeometryFromBinary", new DbExpression[]
			{
				wellKnownBinaryValue,
				coordinateSystemId
			});
		}

		// Token: 0x060038F6 RID: 14582 RVA: 0x000D87B6 File Offset: 0x000D69B6
		public static DbFunctionExpression GeometryPointFromBinary(DbExpression pointWellKnownBinaryValue, DbExpression coordinateSystemId)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(pointWellKnownBinaryValue, "pointWellKnownBinaryValue");
			EntityUtil.CheckArgumentNull<DbExpression>(coordinateSystemId, "coordinateSystemId");
			return EdmFunctions.InvokeCanonicalFunction("GeometryPointFromBinary", new DbExpression[]
			{
				pointWellKnownBinaryValue,
				coordinateSystemId
			});
		}

		// Token: 0x060038F7 RID: 14583 RVA: 0x000D87E8 File Offset: 0x000D69E8
		public static DbFunctionExpression GeometryLineFromBinary(DbExpression lineWellKnownBinaryValue, DbExpression coordinateSystemId)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(lineWellKnownBinaryValue, "lineWellKnownBinaryValue");
			EntityUtil.CheckArgumentNull<DbExpression>(coordinateSystemId, "coordinateSystemId");
			return EdmFunctions.InvokeCanonicalFunction("GeometryLineFromBinary", new DbExpression[]
			{
				lineWellKnownBinaryValue,
				coordinateSystemId
			});
		}

		// Token: 0x060038F8 RID: 14584 RVA: 0x000D881A File Offset: 0x000D6A1A
		public static DbFunctionExpression GeometryPolygonFromBinary(DbExpression polygonWellKnownBinaryValue, DbExpression coordinateSystemId)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(polygonWellKnownBinaryValue, "polygonWellKnownBinaryValue");
			EntityUtil.CheckArgumentNull<DbExpression>(coordinateSystemId, "coordinateSystemId");
			return EdmFunctions.InvokeCanonicalFunction("GeometryPolygonFromBinary", new DbExpression[]
			{
				polygonWellKnownBinaryValue,
				coordinateSystemId
			});
		}

		// Token: 0x060038F9 RID: 14585 RVA: 0x000D884C File Offset: 0x000D6A4C
		public static DbFunctionExpression GeometryMultiPointFromBinary(DbExpression multiPointWellKnownBinaryValue, DbExpression coordinateSystemId)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(multiPointWellKnownBinaryValue, "multiPointWellKnownBinaryValue");
			EntityUtil.CheckArgumentNull<DbExpression>(coordinateSystemId, "coordinateSystemId");
			return EdmFunctions.InvokeCanonicalFunction("GeometryMultiPointFromBinary", new DbExpression[]
			{
				multiPointWellKnownBinaryValue,
				coordinateSystemId
			});
		}

		// Token: 0x060038FA RID: 14586 RVA: 0x000D887E File Offset: 0x000D6A7E
		public static DbFunctionExpression GeometryMultiLineFromBinary(DbExpression multiLineWellKnownBinaryValue, DbExpression coordinateSystemId)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(multiLineWellKnownBinaryValue, "multiLineWellKnownBinaryValue");
			EntityUtil.CheckArgumentNull<DbExpression>(coordinateSystemId, "coordinateSystemId");
			return EdmFunctions.InvokeCanonicalFunction("GeometryMultiLineFromBinary", new DbExpression[]
			{
				multiLineWellKnownBinaryValue,
				coordinateSystemId
			});
		}

		// Token: 0x060038FB RID: 14587 RVA: 0x000D88B0 File Offset: 0x000D6AB0
		public static DbFunctionExpression GeometryMultiPolygonFromBinary(DbExpression multiPolygonWellKnownBinaryValue, DbExpression coordinateSystemId)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(multiPolygonWellKnownBinaryValue, "multiPolygonWellKnownBinaryValue");
			EntityUtil.CheckArgumentNull<DbExpression>(coordinateSystemId, "coordinateSystemId");
			return EdmFunctions.InvokeCanonicalFunction("GeometryMultiPolygonFromBinary", new DbExpression[]
			{
				multiPolygonWellKnownBinaryValue,
				coordinateSystemId
			});
		}

		// Token: 0x060038FC RID: 14588 RVA: 0x000D88E2 File Offset: 0x000D6AE2
		public static DbFunctionExpression GeometryCollectionFromBinary(DbExpression geometryCollectionWellKnownBinaryValue, DbExpression coordinateSystemId)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(geometryCollectionWellKnownBinaryValue, "geometryCollectionWellKnownBinaryValue");
			EntityUtil.CheckArgumentNull<DbExpression>(coordinateSystemId, "coordinateSystemId");
			return EdmFunctions.InvokeCanonicalFunction("GeometryCollectionFromBinary", new DbExpression[]
			{
				geometryCollectionWellKnownBinaryValue,
				coordinateSystemId
			});
		}

		// Token: 0x060038FD RID: 14589 RVA: 0x000D8914 File Offset: 0x000D6B14
		public static DbFunctionExpression GeometryFromGml(DbExpression geometryMarkup)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(geometryMarkup, "geometryMarkup");
			return EdmFunctions.InvokeCanonicalFunction("GeometryFromGml", new DbExpression[]
			{
				geometryMarkup
			});
		}

		// Token: 0x060038FE RID: 14590 RVA: 0x000D8936 File Offset: 0x000D6B36
		public static DbFunctionExpression GeometryFromGml(DbExpression geometryMarkup, DbExpression coordinateSystemId)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(geometryMarkup, "geometryMarkup");
			EntityUtil.CheckArgumentNull<DbExpression>(coordinateSystemId, "coordinateSystemId");
			return EdmFunctions.InvokeCanonicalFunction("GeometryFromGml", new DbExpression[]
			{
				geometryMarkup,
				coordinateSystemId
			});
		}

		// Token: 0x060038FF RID: 14591 RVA: 0x000D8968 File Offset: 0x000D6B68
		public static DbFunctionExpression GeographyFromText(DbExpression wellKnownText)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(wellKnownText, "wellKnownText");
			return EdmFunctions.InvokeCanonicalFunction("GeographyFromText", new DbExpression[]
			{
				wellKnownText
			});
		}

		// Token: 0x06003900 RID: 14592 RVA: 0x000D898A File Offset: 0x000D6B8A
		public static DbFunctionExpression GeographyFromText(DbExpression wellKnownText, DbExpression coordinateSystemId)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(wellKnownText, "wellKnownText");
			EntityUtil.CheckArgumentNull<DbExpression>(coordinateSystemId, "coordinateSystemId");
			return EdmFunctions.InvokeCanonicalFunction("GeographyFromText", new DbExpression[]
			{
				wellKnownText,
				coordinateSystemId
			});
		}

		// Token: 0x06003901 RID: 14593 RVA: 0x000D89BC File Offset: 0x000D6BBC
		public static DbFunctionExpression GeographyPointFromText(DbExpression pointWellKnownText, DbExpression coordinateSystemId)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(pointWellKnownText, "pointWellKnownText");
			EntityUtil.CheckArgumentNull<DbExpression>(coordinateSystemId, "coordinateSystemId");
			return EdmFunctions.InvokeCanonicalFunction("GeographyPointFromText", new DbExpression[]
			{
				pointWellKnownText,
				coordinateSystemId
			});
		}

		// Token: 0x06003902 RID: 14594 RVA: 0x000D89EE File Offset: 0x000D6BEE
		public static DbFunctionExpression GeographyLineFromText(DbExpression lineWellKnownText, DbExpression coordinateSystemId)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(lineWellKnownText, "lineWellKnownText");
			EntityUtil.CheckArgumentNull<DbExpression>(coordinateSystemId, "coordinateSystemId");
			return EdmFunctions.InvokeCanonicalFunction("GeographyLineFromText", new DbExpression[]
			{
				lineWellKnownText,
				coordinateSystemId
			});
		}

		// Token: 0x06003903 RID: 14595 RVA: 0x000D8A20 File Offset: 0x000D6C20
		public static DbFunctionExpression GeographyPolygonFromText(DbExpression polygonWellKnownText, DbExpression coordinateSystemId)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(polygonWellKnownText, "polygonWellKnownText");
			EntityUtil.CheckArgumentNull<DbExpression>(coordinateSystemId, "coordinateSystemId");
			return EdmFunctions.InvokeCanonicalFunction("GeographyPolygonFromText", new DbExpression[]
			{
				polygonWellKnownText,
				coordinateSystemId
			});
		}

		// Token: 0x06003904 RID: 14596 RVA: 0x000D8A52 File Offset: 0x000D6C52
		public static DbFunctionExpression GeographyMultiPointFromText(DbExpression multiPointWellKnownText, DbExpression coordinateSystemId)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(multiPointWellKnownText, "multiPointWellKnownText");
			EntityUtil.CheckArgumentNull<DbExpression>(coordinateSystemId, "coordinateSystemId");
			return EdmFunctions.InvokeCanonicalFunction("GeographyMultiPointFromText", new DbExpression[]
			{
				multiPointWellKnownText,
				coordinateSystemId
			});
		}

		// Token: 0x06003905 RID: 14597 RVA: 0x000D8A84 File Offset: 0x000D6C84
		public static DbFunctionExpression GeographyMultiLineFromText(DbExpression multiLineWellKnownText, DbExpression coordinateSystemId)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(multiLineWellKnownText, "multiLineWellKnownText");
			EntityUtil.CheckArgumentNull<DbExpression>(coordinateSystemId, "coordinateSystemId");
			return EdmFunctions.InvokeCanonicalFunction("GeographyMultiLineFromText", new DbExpression[]
			{
				multiLineWellKnownText,
				coordinateSystemId
			});
		}

		// Token: 0x06003906 RID: 14598 RVA: 0x000D8AB6 File Offset: 0x000D6CB6
		public static DbFunctionExpression GeographyMultiPolygonFromText(DbExpression multiPolygonWellKnownText, DbExpression coordinateSystemId)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(multiPolygonWellKnownText, "multiPolygonWellKnownText");
			EntityUtil.CheckArgumentNull<DbExpression>(coordinateSystemId, "coordinateSystemId");
			return EdmFunctions.InvokeCanonicalFunction("GeographyMultiPolygonFromText", new DbExpression[]
			{
				multiPolygonWellKnownText,
				coordinateSystemId
			});
		}

		// Token: 0x06003907 RID: 14599 RVA: 0x000D8AE8 File Offset: 0x000D6CE8
		public static DbFunctionExpression GeographyCollectionFromText(DbExpression geographyCollectionWellKnownText, DbExpression coordinateSystemId)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(geographyCollectionWellKnownText, "geographyCollectionWellKnownText");
			EntityUtil.CheckArgumentNull<DbExpression>(coordinateSystemId, "coordinateSystemId");
			return EdmFunctions.InvokeCanonicalFunction("GeographyCollectionFromText", new DbExpression[]
			{
				geographyCollectionWellKnownText,
				coordinateSystemId
			});
		}

		// Token: 0x06003908 RID: 14600 RVA: 0x000D8B1A File Offset: 0x000D6D1A
		public static DbFunctionExpression GeographyFromBinary(DbExpression wellKnownBinaryValue)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(wellKnownBinaryValue, "wellKnownBinaryValue");
			return EdmFunctions.InvokeCanonicalFunction("GeographyFromBinary", new DbExpression[]
			{
				wellKnownBinaryValue
			});
		}

		// Token: 0x06003909 RID: 14601 RVA: 0x000D8B3C File Offset: 0x000D6D3C
		public static DbFunctionExpression GeographyFromBinary(DbExpression wellKnownBinaryValue, DbExpression coordinateSystemId)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(wellKnownBinaryValue, "wellKnownBinaryValue");
			EntityUtil.CheckArgumentNull<DbExpression>(coordinateSystemId, "coordinateSystemId");
			return EdmFunctions.InvokeCanonicalFunction("GeographyFromBinary", new DbExpression[]
			{
				wellKnownBinaryValue,
				coordinateSystemId
			});
		}

		// Token: 0x0600390A RID: 14602 RVA: 0x000D8B6E File Offset: 0x000D6D6E
		public static DbFunctionExpression GeographyPointFromBinary(DbExpression pointWellKnownBinaryValue, DbExpression coordinateSystemId)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(pointWellKnownBinaryValue, "pointWellKnownBinaryValue");
			EntityUtil.CheckArgumentNull<DbExpression>(coordinateSystemId, "coordinateSystemId");
			return EdmFunctions.InvokeCanonicalFunction("GeographyPointFromBinary", new DbExpression[]
			{
				pointWellKnownBinaryValue,
				coordinateSystemId
			});
		}

		// Token: 0x0600390B RID: 14603 RVA: 0x000D8BA0 File Offset: 0x000D6DA0
		public static DbFunctionExpression GeographyLineFromBinary(DbExpression lineWellKnownBinaryValue, DbExpression coordinateSystemId)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(lineWellKnownBinaryValue, "lineWellKnownBinaryValue");
			EntityUtil.CheckArgumentNull<DbExpression>(coordinateSystemId, "coordinateSystemId");
			return EdmFunctions.InvokeCanonicalFunction("GeographyLineFromBinary", new DbExpression[]
			{
				lineWellKnownBinaryValue,
				coordinateSystemId
			});
		}

		// Token: 0x0600390C RID: 14604 RVA: 0x000D8BD2 File Offset: 0x000D6DD2
		public static DbFunctionExpression GeographyPolygonFromBinary(DbExpression polygonWellKnownBinaryValue, DbExpression coordinateSystemId)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(polygonWellKnownBinaryValue, "polygonWellKnownBinaryValue");
			EntityUtil.CheckArgumentNull<DbExpression>(coordinateSystemId, "coordinateSystemId");
			return EdmFunctions.InvokeCanonicalFunction("GeographyPolygonFromBinary", new DbExpression[]
			{
				polygonWellKnownBinaryValue,
				coordinateSystemId
			});
		}

		// Token: 0x0600390D RID: 14605 RVA: 0x000D8C04 File Offset: 0x000D6E04
		public static DbFunctionExpression GeographyMultiPointFromBinary(DbExpression multiPointWellKnownBinaryValue, DbExpression coordinateSystemId)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(multiPointWellKnownBinaryValue, "multiPointWellKnownBinaryValue");
			EntityUtil.CheckArgumentNull<DbExpression>(coordinateSystemId, "coordinateSystemId");
			return EdmFunctions.InvokeCanonicalFunction("GeographyMultiPointFromBinary", new DbExpression[]
			{
				multiPointWellKnownBinaryValue,
				coordinateSystemId
			});
		}

		// Token: 0x0600390E RID: 14606 RVA: 0x000D8C36 File Offset: 0x000D6E36
		public static DbFunctionExpression GeographyMultiLineFromBinary(DbExpression multiLineWellKnownBinaryValue, DbExpression coordinateSystemId)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(multiLineWellKnownBinaryValue, "multiLineWellKnownBinaryValue");
			EntityUtil.CheckArgumentNull<DbExpression>(coordinateSystemId, "coordinateSystemId");
			return EdmFunctions.InvokeCanonicalFunction("GeographyMultiLineFromBinary", new DbExpression[]
			{
				multiLineWellKnownBinaryValue,
				coordinateSystemId
			});
		}

		// Token: 0x0600390F RID: 14607 RVA: 0x000D8C68 File Offset: 0x000D6E68
		public static DbFunctionExpression GeographyMultiPolygonFromBinary(DbExpression multiPolygonWellKnownBinaryValue, DbExpression coordinateSystemId)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(multiPolygonWellKnownBinaryValue, "multiPolygonWellKnownBinaryValue");
			EntityUtil.CheckArgumentNull<DbExpression>(coordinateSystemId, "coordinateSystemId");
			return EdmFunctions.InvokeCanonicalFunction("GeographyMultiPolygonFromBinary", new DbExpression[]
			{
				multiPolygonWellKnownBinaryValue,
				coordinateSystemId
			});
		}

		// Token: 0x06003910 RID: 14608 RVA: 0x000D8C9A File Offset: 0x000D6E9A
		public static DbFunctionExpression GeographyCollectionFromBinary(DbExpression geographyCollectionWellKnownBinaryValue, DbExpression coordinateSystemId)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(geographyCollectionWellKnownBinaryValue, "geographyCollectionWellKnownBinaryValue");
			EntityUtil.CheckArgumentNull<DbExpression>(coordinateSystemId, "coordinateSystemId");
			return EdmFunctions.InvokeCanonicalFunction("GeographyCollectionFromBinary", new DbExpression[]
			{
				geographyCollectionWellKnownBinaryValue,
				coordinateSystemId
			});
		}

		// Token: 0x06003911 RID: 14609 RVA: 0x000D8CCC File Offset: 0x000D6ECC
		public static DbFunctionExpression GeographyFromGml(DbExpression geographyMarkup)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(geographyMarkup, "geographyMarkup");
			return EdmFunctions.InvokeCanonicalFunction("GeographyFromGml", new DbExpression[]
			{
				geographyMarkup
			});
		}

		// Token: 0x06003912 RID: 14610 RVA: 0x000D8CEE File Offset: 0x000D6EEE
		public static DbFunctionExpression GeographyFromGml(DbExpression geographyMarkup, DbExpression coordinateSystemId)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(geographyMarkup, "geographyMarkup");
			EntityUtil.CheckArgumentNull<DbExpression>(coordinateSystemId, "coordinateSystemId");
			return EdmFunctions.InvokeCanonicalFunction("GeographyFromGml", new DbExpression[]
			{
				geographyMarkup,
				coordinateSystemId
			});
		}

		// Token: 0x06003913 RID: 14611 RVA: 0x000D8D20 File Offset: 0x000D6F20
		public static DbFunctionExpression CoordinateSystemId(this DbExpression spatialValue)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(spatialValue, "spatialValue");
			return EdmFunctions.InvokeCanonicalFunction("CoordinateSystemId", new DbExpression[]
			{
				spatialValue
			});
		}

		// Token: 0x06003914 RID: 14612 RVA: 0x000D8D42 File Offset: 0x000D6F42
		public static DbFunctionExpression SpatialTypeName(this DbExpression spatialValue)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(spatialValue, "spatialValue");
			return EdmFunctions.InvokeCanonicalFunction("SpatialTypeName", new DbExpression[]
			{
				spatialValue
			});
		}

		// Token: 0x06003915 RID: 14613 RVA: 0x000D8D64 File Offset: 0x000D6F64
		public static DbFunctionExpression SpatialDimension(this DbExpression spatialValue)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(spatialValue, "spatialValue");
			return EdmFunctions.InvokeCanonicalFunction("SpatialDimension", new DbExpression[]
			{
				spatialValue
			});
		}

		// Token: 0x06003916 RID: 14614 RVA: 0x000D8D86 File Offset: 0x000D6F86
		public static DbFunctionExpression SpatialEnvelope(this DbExpression geometryValue)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(geometryValue, "geometryValue");
			return EdmFunctions.InvokeCanonicalFunction("SpatialEnvelope", new DbExpression[]
			{
				geometryValue
			});
		}

		// Token: 0x06003917 RID: 14615 RVA: 0x000D8DA8 File Offset: 0x000D6FA8
		public static DbFunctionExpression AsBinary(this DbExpression spatialValue)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(spatialValue, "spatialValue");
			return EdmFunctions.InvokeCanonicalFunction("AsBinary", new DbExpression[]
			{
				spatialValue
			});
		}

		// Token: 0x06003918 RID: 14616 RVA: 0x000D8DCA File Offset: 0x000D6FCA
		public static DbFunctionExpression AsGml(this DbExpression spatialValue)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(spatialValue, "spatialValue");
			return EdmFunctions.InvokeCanonicalFunction("AsGml", new DbExpression[]
			{
				spatialValue
			});
		}

		// Token: 0x06003919 RID: 14617 RVA: 0x000D8DEC File Offset: 0x000D6FEC
		public static DbFunctionExpression AsText(this DbExpression spatialValue)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(spatialValue, "spatialValue");
			return EdmFunctions.InvokeCanonicalFunction("AsText", new DbExpression[]
			{
				spatialValue
			});
		}

		// Token: 0x0600391A RID: 14618 RVA: 0x000D8E0E File Offset: 0x000D700E
		public static DbFunctionExpression IsEmptySpatial(this DbExpression spatialValue)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(spatialValue, "spatialValue");
			return EdmFunctions.InvokeCanonicalFunction("IsEmptySpatial", new DbExpression[]
			{
				spatialValue
			});
		}

		// Token: 0x0600391B RID: 14619 RVA: 0x000D8E30 File Offset: 0x000D7030
		public static DbFunctionExpression IsSimpleGeometry(this DbExpression geometryValue)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(geometryValue, "geometryValue");
			return EdmFunctions.InvokeCanonicalFunction("IsSimpleGeometry", new DbExpression[]
			{
				geometryValue
			});
		}

		// Token: 0x0600391C RID: 14620 RVA: 0x000D8E52 File Offset: 0x000D7052
		public static DbFunctionExpression SpatialBoundary(this DbExpression geometryValue)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(geometryValue, "geometryValue");
			return EdmFunctions.InvokeCanonicalFunction("SpatialBoundary", new DbExpression[]
			{
				geometryValue
			});
		}

		// Token: 0x0600391D RID: 14621 RVA: 0x000D8E74 File Offset: 0x000D7074
		public static DbFunctionExpression IsValidGeometry(this DbExpression geometryValue)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(geometryValue, "geometryValue");
			return EdmFunctions.InvokeCanonicalFunction("IsValidGeometry", new DbExpression[]
			{
				geometryValue
			});
		}

		// Token: 0x0600391E RID: 14622 RVA: 0x000D8E96 File Offset: 0x000D7096
		public static DbFunctionExpression SpatialEquals(this DbExpression spatialValue1, DbExpression spatialValue2)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(spatialValue1, "spatialValue1");
			EntityUtil.CheckArgumentNull<DbExpression>(spatialValue2, "spatialValue2");
			return EdmFunctions.InvokeCanonicalFunction("SpatialEquals", new DbExpression[]
			{
				spatialValue1,
				spatialValue2
			});
		}

		// Token: 0x0600391F RID: 14623 RVA: 0x000D8EC8 File Offset: 0x000D70C8
		public static DbFunctionExpression SpatialDisjoint(this DbExpression spatialValue1, DbExpression spatialValue2)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(spatialValue1, "spatialValue1");
			EntityUtil.CheckArgumentNull<DbExpression>(spatialValue2, "spatialValue2");
			return EdmFunctions.InvokeCanonicalFunction("SpatialDisjoint", new DbExpression[]
			{
				spatialValue1,
				spatialValue2
			});
		}

		// Token: 0x06003920 RID: 14624 RVA: 0x000D8EFA File Offset: 0x000D70FA
		public static DbFunctionExpression SpatialIntersects(this DbExpression spatialValue1, DbExpression spatialValue2)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(spatialValue1, "spatialValue1");
			EntityUtil.CheckArgumentNull<DbExpression>(spatialValue2, "spatialValue2");
			return EdmFunctions.InvokeCanonicalFunction("SpatialIntersects", new DbExpression[]
			{
				spatialValue1,
				spatialValue2
			});
		}

		// Token: 0x06003921 RID: 14625 RVA: 0x000D8F2C File Offset: 0x000D712C
		public static DbFunctionExpression SpatialTouches(this DbExpression geometryValue1, DbExpression geometryValue2)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(geometryValue1, "geometryValue1");
			EntityUtil.CheckArgumentNull<DbExpression>(geometryValue2, "geometryValue2");
			return EdmFunctions.InvokeCanonicalFunction("SpatialTouches", new DbExpression[]
			{
				geometryValue1,
				geometryValue2
			});
		}

		// Token: 0x06003922 RID: 14626 RVA: 0x000D8F5E File Offset: 0x000D715E
		public static DbFunctionExpression SpatialCrosses(this DbExpression geometryValue1, DbExpression geometryValue2)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(geometryValue1, "geometryValue1");
			EntityUtil.CheckArgumentNull<DbExpression>(geometryValue2, "geometryValue2");
			return EdmFunctions.InvokeCanonicalFunction("SpatialCrosses", new DbExpression[]
			{
				geometryValue1,
				geometryValue2
			});
		}

		// Token: 0x06003923 RID: 14627 RVA: 0x000D8F90 File Offset: 0x000D7190
		public static DbFunctionExpression SpatialWithin(this DbExpression geometryValue1, DbExpression geometryValue2)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(geometryValue1, "geometryValue1");
			EntityUtil.CheckArgumentNull<DbExpression>(geometryValue2, "geometryValue2");
			return EdmFunctions.InvokeCanonicalFunction("SpatialWithin", new DbExpression[]
			{
				geometryValue1,
				geometryValue2
			});
		}

		// Token: 0x06003924 RID: 14628 RVA: 0x000D8FC2 File Offset: 0x000D71C2
		public static DbFunctionExpression SpatialContains(this DbExpression geometryValue1, DbExpression geometryValue2)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(geometryValue1, "geometryValue1");
			EntityUtil.CheckArgumentNull<DbExpression>(geometryValue2, "geometryValue2");
			return EdmFunctions.InvokeCanonicalFunction("SpatialContains", new DbExpression[]
			{
				geometryValue1,
				geometryValue2
			});
		}

		// Token: 0x06003925 RID: 14629 RVA: 0x000D8FF4 File Offset: 0x000D71F4
		public static DbFunctionExpression SpatialOverlaps(this DbExpression geometryValue1, DbExpression geometryValue2)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(geometryValue1, "geometryValue1");
			EntityUtil.CheckArgumentNull<DbExpression>(geometryValue2, "geometryValue2");
			return EdmFunctions.InvokeCanonicalFunction("SpatialOverlaps", new DbExpression[]
			{
				geometryValue1,
				geometryValue2
			});
		}

		// Token: 0x06003926 RID: 14630 RVA: 0x000D9028 File Offset: 0x000D7228
		public static DbFunctionExpression SpatialRelate(this DbExpression geometryValue1, DbExpression geometryValue2, DbExpression intersectionPatternMatrix)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(geometryValue1, "geometryValue1");
			EntityUtil.CheckArgumentNull<DbExpression>(geometryValue2, "geometryValue2");
			EntityUtil.CheckArgumentNull<DbExpression>(intersectionPatternMatrix, "intersectionPatternMatrix");
			return EdmFunctions.InvokeCanonicalFunction("SpatialRelate", new DbExpression[]
			{
				geometryValue1,
				geometryValue2,
				intersectionPatternMatrix
			});
		}

		// Token: 0x06003927 RID: 14631 RVA: 0x000D9075 File Offset: 0x000D7275
		public static DbFunctionExpression SpatialBuffer(this DbExpression spatialValue, DbExpression distance)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(spatialValue, "spatialValue");
			EntityUtil.CheckArgumentNull<DbExpression>(distance, "distance");
			return EdmFunctions.InvokeCanonicalFunction("SpatialBuffer", new DbExpression[]
			{
				spatialValue,
				distance
			});
		}

		// Token: 0x06003928 RID: 14632 RVA: 0x000D90A7 File Offset: 0x000D72A7
		public static DbFunctionExpression Distance(this DbExpression spatialValue1, DbExpression spatialValue2)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(spatialValue1, "spatialValue1");
			EntityUtil.CheckArgumentNull<DbExpression>(spatialValue2, "spatialValue2");
			return EdmFunctions.InvokeCanonicalFunction("Distance", new DbExpression[]
			{
				spatialValue1,
				spatialValue2
			});
		}

		// Token: 0x06003929 RID: 14633 RVA: 0x000D90D9 File Offset: 0x000D72D9
		public static DbFunctionExpression SpatialConvexHull(this DbExpression geometryValue)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(geometryValue, "geometryValue");
			return EdmFunctions.InvokeCanonicalFunction("SpatialConvexHull", new DbExpression[]
			{
				geometryValue
			});
		}

		// Token: 0x0600392A RID: 14634 RVA: 0x000D90FB File Offset: 0x000D72FB
		public static DbFunctionExpression SpatialIntersection(this DbExpression spatialValue1, DbExpression spatialValue2)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(spatialValue1, "spatialValue1");
			EntityUtil.CheckArgumentNull<DbExpression>(spatialValue2, "spatialValue2");
			return EdmFunctions.InvokeCanonicalFunction("SpatialIntersection", new DbExpression[]
			{
				spatialValue1,
				spatialValue2
			});
		}

		// Token: 0x0600392B RID: 14635 RVA: 0x000D912D File Offset: 0x000D732D
		public static DbFunctionExpression SpatialUnion(this DbExpression spatialValue1, DbExpression spatialValue2)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(spatialValue1, "spatialValue1");
			EntityUtil.CheckArgumentNull<DbExpression>(spatialValue2, "spatialValue2");
			return EdmFunctions.InvokeCanonicalFunction("SpatialUnion", new DbExpression[]
			{
				spatialValue1,
				spatialValue2
			});
		}

		// Token: 0x0600392C RID: 14636 RVA: 0x000D915F File Offset: 0x000D735F
		public static DbFunctionExpression SpatialDifference(this DbExpression spatialValue1, DbExpression spatialValue2)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(spatialValue1, "spatialValue1");
			EntityUtil.CheckArgumentNull<DbExpression>(spatialValue2, "spatialValue2");
			return EdmFunctions.InvokeCanonicalFunction("SpatialDifference", new DbExpression[]
			{
				spatialValue1,
				spatialValue2
			});
		}

		// Token: 0x0600392D RID: 14637 RVA: 0x000D9191 File Offset: 0x000D7391
		public static DbFunctionExpression SpatialSymmetricDifference(this DbExpression spatialValue1, DbExpression spatialValue2)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(spatialValue1, "spatialValue1");
			EntityUtil.CheckArgumentNull<DbExpression>(spatialValue2, "spatialValue2");
			return EdmFunctions.InvokeCanonicalFunction("SpatialSymmetricDifference", new DbExpression[]
			{
				spatialValue1,
				spatialValue2
			});
		}

		// Token: 0x0600392E RID: 14638 RVA: 0x000D91C3 File Offset: 0x000D73C3
		public static DbFunctionExpression SpatialElementCount(this DbExpression spatialValue)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(spatialValue, "spatialValue");
			return EdmFunctions.InvokeCanonicalFunction("SpatialElementCount", new DbExpression[]
			{
				spatialValue
			});
		}

		// Token: 0x0600392F RID: 14639 RVA: 0x000D91E5 File Offset: 0x000D73E5
		public static DbFunctionExpression SpatialElementAt(this DbExpression spatialValue, DbExpression indexValue)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(spatialValue, "spatialValue");
			EntityUtil.CheckArgumentNull<DbExpression>(indexValue, "indexValue");
			return EdmFunctions.InvokeCanonicalFunction("SpatialElementAt", new DbExpression[]
			{
				spatialValue,
				indexValue
			});
		}

		// Token: 0x06003930 RID: 14640 RVA: 0x000D9217 File Offset: 0x000D7417
		public static DbFunctionExpression XCoordinate(this DbExpression geometryValue)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(geometryValue, "geometryValue");
			return EdmFunctions.InvokeCanonicalFunction("XCoordinate", new DbExpression[]
			{
				geometryValue
			});
		}

		// Token: 0x06003931 RID: 14641 RVA: 0x000D9239 File Offset: 0x000D7439
		public static DbFunctionExpression YCoordinate(this DbExpression geometryValue)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(geometryValue, "geometryValue");
			return EdmFunctions.InvokeCanonicalFunction("YCoordinate", new DbExpression[]
			{
				geometryValue
			});
		}

		// Token: 0x06003932 RID: 14642 RVA: 0x000D925B File Offset: 0x000D745B
		public static DbFunctionExpression Elevation(this DbExpression spatialValue)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(spatialValue, "spatialValue");
			return EdmFunctions.InvokeCanonicalFunction("Elevation", new DbExpression[]
			{
				spatialValue
			});
		}

		// Token: 0x06003933 RID: 14643 RVA: 0x000D927D File Offset: 0x000D747D
		public static DbFunctionExpression Measure(this DbExpression spatialValue)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(spatialValue, "spatialValue");
			return EdmFunctions.InvokeCanonicalFunction("Measure", new DbExpression[]
			{
				spatialValue
			});
		}

		// Token: 0x06003934 RID: 14644 RVA: 0x000D929F File Offset: 0x000D749F
		public static DbFunctionExpression Latitude(this DbExpression geographyValue)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(geographyValue, "geographyValue");
			return EdmFunctions.InvokeCanonicalFunction("Latitude", new DbExpression[]
			{
				geographyValue
			});
		}

		// Token: 0x06003935 RID: 14645 RVA: 0x000D92C1 File Offset: 0x000D74C1
		public static DbFunctionExpression Longitude(this DbExpression geographyValue)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(geographyValue, "geographyValue");
			return EdmFunctions.InvokeCanonicalFunction("Longitude", new DbExpression[]
			{
				geographyValue
			});
		}

		// Token: 0x06003936 RID: 14646 RVA: 0x000D92E3 File Offset: 0x000D74E3
		public static DbFunctionExpression SpatialLength(this DbExpression spatialValue)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(spatialValue, "spatialValue");
			return EdmFunctions.InvokeCanonicalFunction("SpatialLength", new DbExpression[]
			{
				spatialValue
			});
		}

		// Token: 0x06003937 RID: 14647 RVA: 0x000D9305 File Offset: 0x000D7505
		public static DbFunctionExpression StartPoint(this DbExpression spatialValue)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(spatialValue, "spatialValue");
			return EdmFunctions.InvokeCanonicalFunction("StartPoint", new DbExpression[]
			{
				spatialValue
			});
		}

		// Token: 0x06003938 RID: 14648 RVA: 0x000D9327 File Offset: 0x000D7527
		public static DbFunctionExpression EndPoint(this DbExpression spatialValue)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(spatialValue, "spatialValue");
			return EdmFunctions.InvokeCanonicalFunction("EndPoint", new DbExpression[]
			{
				spatialValue
			});
		}

		// Token: 0x06003939 RID: 14649 RVA: 0x000D9349 File Offset: 0x000D7549
		public static DbFunctionExpression IsClosedSpatial(this DbExpression spatialValue)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(spatialValue, "spatialValue");
			return EdmFunctions.InvokeCanonicalFunction("IsClosedSpatial", new DbExpression[]
			{
				spatialValue
			});
		}

		// Token: 0x0600393A RID: 14650 RVA: 0x000D936B File Offset: 0x000D756B
		public static DbFunctionExpression IsRing(this DbExpression geometryValue)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(geometryValue, "geometryValue");
			return EdmFunctions.InvokeCanonicalFunction("IsRing", new DbExpression[]
			{
				geometryValue
			});
		}

		// Token: 0x0600393B RID: 14651 RVA: 0x000D938D File Offset: 0x000D758D
		public static DbFunctionExpression PointCount(this DbExpression spatialValue)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(spatialValue, "spatialValue");
			return EdmFunctions.InvokeCanonicalFunction("PointCount", new DbExpression[]
			{
				spatialValue
			});
		}

		// Token: 0x0600393C RID: 14652 RVA: 0x000D93AF File Offset: 0x000D75AF
		public static DbFunctionExpression PointAt(this DbExpression spatialValue, DbExpression indexValue)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(spatialValue, "spatialValue");
			EntityUtil.CheckArgumentNull<DbExpression>(indexValue, "indexValue");
			return EdmFunctions.InvokeCanonicalFunction("PointAt", new DbExpression[]
			{
				spatialValue,
				indexValue
			});
		}

		// Token: 0x0600393D RID: 14653 RVA: 0x000D93E1 File Offset: 0x000D75E1
		public static DbFunctionExpression Area(this DbExpression spatialValue)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(spatialValue, "spatialValue");
			return EdmFunctions.InvokeCanonicalFunction("Area", new DbExpression[]
			{
				spatialValue
			});
		}

		// Token: 0x0600393E RID: 14654 RVA: 0x000D9403 File Offset: 0x000D7603
		public static DbFunctionExpression Centroid(this DbExpression geometryValue)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(geometryValue, "geometryValue");
			return EdmFunctions.InvokeCanonicalFunction("Centroid", new DbExpression[]
			{
				geometryValue
			});
		}

		// Token: 0x0600393F RID: 14655 RVA: 0x000D9425 File Offset: 0x000D7625
		public static DbFunctionExpression PointOnSurface(this DbExpression geometryValue)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(geometryValue, "geometryValue");
			return EdmFunctions.InvokeCanonicalFunction("PointOnSurface", new DbExpression[]
			{
				geometryValue
			});
		}

		// Token: 0x06003940 RID: 14656 RVA: 0x000D9447 File Offset: 0x000D7647
		public static DbFunctionExpression ExteriorRing(this DbExpression geometryValue)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(geometryValue, "geometryValue");
			return EdmFunctions.InvokeCanonicalFunction("ExteriorRing", new DbExpression[]
			{
				geometryValue
			});
		}

		// Token: 0x06003941 RID: 14657 RVA: 0x000D9469 File Offset: 0x000D7669
		public static DbFunctionExpression InteriorRingCount(this DbExpression geometryValue)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(geometryValue, "geometryValue");
			return EdmFunctions.InvokeCanonicalFunction("InteriorRingCount", new DbExpression[]
			{
				geometryValue
			});
		}

		// Token: 0x06003942 RID: 14658 RVA: 0x000D948B File Offset: 0x000D768B
		public static DbFunctionExpression InteriorRingAt(this DbExpression geometryValue, DbExpression indexValue)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(geometryValue, "geometryValue");
			EntityUtil.CheckArgumentNull<DbExpression>(indexValue, "indexValue");
			return EdmFunctions.InvokeCanonicalFunction("InteriorRingAt", new DbExpression[]
			{
				geometryValue,
				indexValue
			});
		}
	}
}
