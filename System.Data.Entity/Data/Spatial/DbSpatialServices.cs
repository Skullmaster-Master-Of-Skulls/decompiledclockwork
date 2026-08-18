using System;
using System.Data.Common.Utils;
using System.Data.SqlClient;

namespace System.Data.Spatial
{
	// Token: 0x020002DA RID: 730
	[Serializable]
	public abstract class DbSpatialServices
	{
		// Token: 0x17000883 RID: 2179
		// (get) Token: 0x06002B40 RID: 11072 RVA: 0x000A8007 File Offset: 0x000A6207
		public static DbSpatialServices Default
		{
			get
			{
				return DbSpatialServices.defaultServices.Value;
			}
		}

		// Token: 0x06002B42 RID: 11074 RVA: 0x000A8013 File Offset: 0x000A6213
		private static DbSpatialServices LoadDefaultServices()
		{
			if (SqlProviderServices.SqlTypesAssemblyIsAvailable)
			{
				return SqlSpatialServices.Instance;
			}
			return DefaultSpatialServices.Instance;
		}

		// Token: 0x06002B43 RID: 11075 RVA: 0x000A8027 File Offset: 0x000A6227
		protected static DbGeography CreateGeography(DbSpatialServices spatialServices, object providerValue)
		{
			spatialServices.CheckNull("spatialServices");
			providerValue.CheckNull("providerValue");
			return new DbGeography(spatialServices, providerValue);
		}

		// Token: 0x06002B44 RID: 11076
		public abstract DbGeography GeographyFromProviderValue(object providerValue);

		// Token: 0x06002B45 RID: 11077
		public abstract object CreateProviderValue(DbGeographyWellKnownValue wellKnownValue);

		// Token: 0x06002B46 RID: 11078
		public abstract DbGeographyWellKnownValue CreateWellKnownValue(DbGeography geographyValue);

		// Token: 0x06002B47 RID: 11079
		public abstract DbGeography GeographyFromBinary(byte[] wellKnownBinary);

		// Token: 0x06002B48 RID: 11080
		public abstract DbGeography GeographyFromBinary(byte[] wellKnownBinary, int coordinateSystemId);

		// Token: 0x06002B49 RID: 11081
		public abstract DbGeography GeographyLineFromBinary(byte[] lineWellKnownBinary, int coordinateSystemId);

		// Token: 0x06002B4A RID: 11082
		public abstract DbGeography GeographyPointFromBinary(byte[] pointWellKnownBinary, int coordinateSystemId);

		// Token: 0x06002B4B RID: 11083
		public abstract DbGeography GeographyPolygonFromBinary(byte[] polygonWellKnownBinary, int coordinateSystemId);

		// Token: 0x06002B4C RID: 11084
		public abstract DbGeography GeographyMultiLineFromBinary(byte[] multiLineWellKnownBinary, int coordinateSystemId);

		// Token: 0x06002B4D RID: 11085
		public abstract DbGeography GeographyMultiPointFromBinary(byte[] multiPointWellKnownBinary, int coordinateSystemId);

		// Token: 0x06002B4E RID: 11086
		public abstract DbGeography GeographyMultiPolygonFromBinary(byte[] multiPolygonWellKnownBinary, int coordinateSystemId);

		// Token: 0x06002B4F RID: 11087
		public abstract DbGeography GeographyCollectionFromBinary(byte[] geographyCollectionWellKnownBinary, int coordinateSystemId);

		// Token: 0x06002B50 RID: 11088
		public abstract DbGeography GeographyFromText(string wellKnownText);

		// Token: 0x06002B51 RID: 11089
		public abstract DbGeography GeographyFromText(string wellKnownText, int coordinateSystemId);

		// Token: 0x06002B52 RID: 11090
		public abstract DbGeography GeographyLineFromText(string lineWellKnownText, int coordinateSystemId);

		// Token: 0x06002B53 RID: 11091
		public abstract DbGeography GeographyPointFromText(string pointWellKnownText, int coordinateSystemId);

		// Token: 0x06002B54 RID: 11092
		public abstract DbGeography GeographyPolygonFromText(string polygonWellKnownText, int coordinateSystemId);

		// Token: 0x06002B55 RID: 11093
		public abstract DbGeography GeographyMultiLineFromText(string multiLineWellKnownText, int coordinateSystemId);

		// Token: 0x06002B56 RID: 11094
		public abstract DbGeography GeographyMultiPointFromText(string multiPointWellKnownText, int coordinateSystemId);

		// Token: 0x06002B57 RID: 11095
		public abstract DbGeography GeographyMultiPolygonFromText(string multiPolygonWellKnownText, int coordinateSystemId);

		// Token: 0x06002B58 RID: 11096
		public abstract DbGeography GeographyCollectionFromText(string geographyCollectionWellKnownText, int coordinateSystemId);

		// Token: 0x06002B59 RID: 11097
		public abstract DbGeography GeographyFromGml(string geographyMarkup);

		// Token: 0x06002B5A RID: 11098
		public abstract DbGeography GeographyFromGml(string geographyMarkup, int coordinateSystemId);

		// Token: 0x06002B5B RID: 11099
		public abstract int GetCoordinateSystemId(DbGeography geographyValue);

		// Token: 0x06002B5C RID: 11100
		public abstract int GetDimension(DbGeography geographyValue);

		// Token: 0x06002B5D RID: 11101
		public abstract string GetSpatialTypeName(DbGeography geographyValue);

		// Token: 0x06002B5E RID: 11102
		public abstract bool GetIsEmpty(DbGeography geographyValue);

		// Token: 0x06002B5F RID: 11103
		public abstract string AsText(DbGeography geographyValue);

		// Token: 0x06002B60 RID: 11104 RVA: 0x00006174 File Offset: 0x00004374
		public virtual string AsTextIncludingElevationAndMeasure(DbGeography geographyValue)
		{
			return null;
		}

		// Token: 0x06002B61 RID: 11105
		public abstract byte[] AsBinary(DbGeography geographyValue);

		// Token: 0x06002B62 RID: 11106
		public abstract string AsGml(DbGeography geographyValue);

		// Token: 0x06002B63 RID: 11107
		public abstract bool SpatialEquals(DbGeography geographyValue, DbGeography otherGeography);

		// Token: 0x06002B64 RID: 11108
		public abstract bool Disjoint(DbGeography geographyValue, DbGeography otherGeography);

		// Token: 0x06002B65 RID: 11109
		public abstract bool Intersects(DbGeography geographyValue, DbGeography otherGeography);

		// Token: 0x06002B66 RID: 11110
		public abstract DbGeography Buffer(DbGeography geographyValue, double distance);

		// Token: 0x06002B67 RID: 11111
		public abstract double Distance(DbGeography geographyValue, DbGeography otherGeography);

		// Token: 0x06002B68 RID: 11112
		public abstract DbGeography Intersection(DbGeography geographyValue, DbGeography otherGeography);

		// Token: 0x06002B69 RID: 11113
		public abstract DbGeography Union(DbGeography geographyValue, DbGeography otherGeography);

		// Token: 0x06002B6A RID: 11114
		public abstract DbGeography Difference(DbGeography geographyValue, DbGeography otherGeography);

		// Token: 0x06002B6B RID: 11115
		public abstract DbGeography SymmetricDifference(DbGeography geographyValue, DbGeography otherGeography);

		// Token: 0x06002B6C RID: 11116
		public abstract int? GetElementCount(DbGeography geographyValue);

		// Token: 0x06002B6D RID: 11117
		public abstract DbGeography ElementAt(DbGeography geographyValue, int index);

		// Token: 0x06002B6E RID: 11118
		public abstract double? GetLatitude(DbGeography geographyValue);

		// Token: 0x06002B6F RID: 11119
		public abstract double? GetLongitude(DbGeography geographyValue);

		// Token: 0x06002B70 RID: 11120
		public abstract double? GetElevation(DbGeography geographyValue);

		// Token: 0x06002B71 RID: 11121
		public abstract double? GetMeasure(DbGeography geographyValue);

		// Token: 0x06002B72 RID: 11122
		public abstract double? GetLength(DbGeography geographyValue);

		// Token: 0x06002B73 RID: 11123
		public abstract DbGeography GetStartPoint(DbGeography geographyValue);

		// Token: 0x06002B74 RID: 11124
		public abstract DbGeography GetEndPoint(DbGeography geographyValue);

		// Token: 0x06002B75 RID: 11125
		public abstract bool? GetIsClosed(DbGeography geographyValue);

		// Token: 0x06002B76 RID: 11126
		public abstract int? GetPointCount(DbGeography geographyValue);

		// Token: 0x06002B77 RID: 11127
		public abstract DbGeography PointAt(DbGeography geographyValue, int index);

		// Token: 0x06002B78 RID: 11128
		public abstract double? GetArea(DbGeography geographyValue);

		// Token: 0x06002B79 RID: 11129 RVA: 0x000A8046 File Offset: 0x000A6246
		protected static DbGeometry CreateGeometry(DbSpatialServices spatialServices, object providerValue)
		{
			spatialServices.CheckNull("spatialServices");
			providerValue.CheckNull("providerValue");
			return new DbGeometry(spatialServices, providerValue);
		}

		// Token: 0x06002B7A RID: 11130
		public abstract object CreateProviderValue(DbGeometryWellKnownValue wellKnownValue);

		// Token: 0x06002B7B RID: 11131
		public abstract DbGeometryWellKnownValue CreateWellKnownValue(DbGeometry geometryValue);

		// Token: 0x06002B7C RID: 11132
		public abstract DbGeometry GeometryFromProviderValue(object providerValue);

		// Token: 0x06002B7D RID: 11133
		public abstract DbGeometry GeometryFromBinary(byte[] wellKnownBinary);

		// Token: 0x06002B7E RID: 11134
		public abstract DbGeometry GeometryFromBinary(byte[] wellKnownBinary, int coordinateSystemId);

		// Token: 0x06002B7F RID: 11135
		public abstract DbGeometry GeometryLineFromBinary(byte[] lineWellKnownBinary, int coordinateSystemId);

		// Token: 0x06002B80 RID: 11136
		public abstract DbGeometry GeometryPointFromBinary(byte[] pointWellKnownBinary, int coordinateSystemId);

		// Token: 0x06002B81 RID: 11137
		public abstract DbGeometry GeometryPolygonFromBinary(byte[] polygonWellKnownBinary, int coordinateSystemId);

		// Token: 0x06002B82 RID: 11138
		public abstract DbGeometry GeometryMultiLineFromBinary(byte[] multiLineWellKnownBinary, int coordinateSystemId);

		// Token: 0x06002B83 RID: 11139
		public abstract DbGeometry GeometryMultiPointFromBinary(byte[] multiPointWellKnownBinary, int coordinateSystemId);

		// Token: 0x06002B84 RID: 11140
		public abstract DbGeometry GeometryMultiPolygonFromBinary(byte[] multiPolygonWellKnownBinary, int coordinateSystemId);

		// Token: 0x06002B85 RID: 11141
		public abstract DbGeometry GeometryCollectionFromBinary(byte[] geometryCollectionWellKnownBinary, int coordinateSystemId);

		// Token: 0x06002B86 RID: 11142
		public abstract DbGeometry GeometryFromText(string wellKnownText);

		// Token: 0x06002B87 RID: 11143
		public abstract DbGeometry GeometryFromText(string wellKnownText, int coordinateSystemId);

		// Token: 0x06002B88 RID: 11144
		public abstract DbGeometry GeometryLineFromText(string lineWellKnownText, int coordinateSystemId);

		// Token: 0x06002B89 RID: 11145
		public abstract DbGeometry GeometryPointFromText(string pointWellKnownText, int coordinateSystemId);

		// Token: 0x06002B8A RID: 11146
		public abstract DbGeometry GeometryPolygonFromText(string polygonWellKnownText, int coordinateSystemId);

		// Token: 0x06002B8B RID: 11147
		public abstract DbGeometry GeometryMultiLineFromText(string multiLineWellKnownText, int coordinateSystemId);

		// Token: 0x06002B8C RID: 11148
		public abstract DbGeometry GeometryMultiPointFromText(string multiPointWellKnownText, int coordinateSystemId);

		// Token: 0x06002B8D RID: 11149
		public abstract DbGeometry GeometryMultiPolygonFromText(string multiPolygonKnownText, int coordinateSystemId);

		// Token: 0x06002B8E RID: 11150
		public abstract DbGeometry GeometryCollectionFromText(string geometryCollectionWellKnownText, int coordinateSystemId);

		// Token: 0x06002B8F RID: 11151
		public abstract DbGeometry GeometryFromGml(string geometryMarkup);

		// Token: 0x06002B90 RID: 11152
		public abstract DbGeometry GeometryFromGml(string geometryMarkup, int coordinateSystemId);

		// Token: 0x06002B91 RID: 11153
		public abstract int GetCoordinateSystemId(DbGeometry geometryValue);

		// Token: 0x06002B92 RID: 11154
		public abstract DbGeometry GetBoundary(DbGeometry geometryValue);

		// Token: 0x06002B93 RID: 11155
		public abstract int GetDimension(DbGeometry geometryValue);

		// Token: 0x06002B94 RID: 11156
		public abstract DbGeometry GetEnvelope(DbGeometry geometryValue);

		// Token: 0x06002B95 RID: 11157
		public abstract string GetSpatialTypeName(DbGeometry geometryValue);

		// Token: 0x06002B96 RID: 11158
		public abstract bool GetIsEmpty(DbGeometry geometryValue);

		// Token: 0x06002B97 RID: 11159
		public abstract bool GetIsSimple(DbGeometry geometryValue);

		// Token: 0x06002B98 RID: 11160
		public abstract bool GetIsValid(DbGeometry geometryValue);

		// Token: 0x06002B99 RID: 11161
		public abstract string AsText(DbGeometry geometryValue);

		// Token: 0x06002B9A RID: 11162 RVA: 0x00006174 File Offset: 0x00004374
		public virtual string AsTextIncludingElevationAndMeasure(DbGeometry geometryValue)
		{
			return null;
		}

		// Token: 0x06002B9B RID: 11163
		public abstract byte[] AsBinary(DbGeometry geometryValue);

		// Token: 0x06002B9C RID: 11164
		public abstract string AsGml(DbGeometry geometryValue);

		// Token: 0x06002B9D RID: 11165
		public abstract bool SpatialEquals(DbGeometry geometryValue, DbGeometry otherGeometry);

		// Token: 0x06002B9E RID: 11166
		public abstract bool Disjoint(DbGeometry geometryValue, DbGeometry otherGeometry);

		// Token: 0x06002B9F RID: 11167
		public abstract bool Intersects(DbGeometry geometryValue, DbGeometry otherGeometry);

		// Token: 0x06002BA0 RID: 11168
		public abstract bool Touches(DbGeometry geometryValue, DbGeometry otherGeometry);

		// Token: 0x06002BA1 RID: 11169
		public abstract bool Crosses(DbGeometry geometryValue, DbGeometry otherGeometry);

		// Token: 0x06002BA2 RID: 11170
		public abstract bool Within(DbGeometry geometryValue, DbGeometry otherGeometry);

		// Token: 0x06002BA3 RID: 11171
		public abstract bool Contains(DbGeometry geometryValue, DbGeometry otherGeometry);

		// Token: 0x06002BA4 RID: 11172
		public abstract bool Overlaps(DbGeometry geometryValue, DbGeometry otherGeometry);

		// Token: 0x06002BA5 RID: 11173
		public abstract bool Relate(DbGeometry geometryValue, DbGeometry otherGeometry, string matrix);

		// Token: 0x06002BA6 RID: 11174
		public abstract DbGeometry Buffer(DbGeometry geometryValue, double distance);

		// Token: 0x06002BA7 RID: 11175
		public abstract double Distance(DbGeometry geometryValue, DbGeometry otherGeometry);

		// Token: 0x06002BA8 RID: 11176
		public abstract DbGeometry GetConvexHull(DbGeometry geometryValue);

		// Token: 0x06002BA9 RID: 11177
		public abstract DbGeometry Intersection(DbGeometry geometryValue, DbGeometry otherGeometry);

		// Token: 0x06002BAA RID: 11178
		public abstract DbGeometry Union(DbGeometry geometryValue, DbGeometry otherGeometry);

		// Token: 0x06002BAB RID: 11179
		public abstract DbGeometry Difference(DbGeometry geometryValue, DbGeometry otherGeometry);

		// Token: 0x06002BAC RID: 11180
		public abstract DbGeometry SymmetricDifference(DbGeometry geometryValue, DbGeometry otherGeometry);

		// Token: 0x06002BAD RID: 11181
		public abstract int? GetElementCount(DbGeometry geometryValue);

		// Token: 0x06002BAE RID: 11182
		public abstract DbGeometry ElementAt(DbGeometry geometryValue, int index);

		// Token: 0x06002BAF RID: 11183
		public abstract double? GetXCoordinate(DbGeometry geometryValue);

		// Token: 0x06002BB0 RID: 11184
		public abstract double? GetYCoordinate(DbGeometry geometryValue);

		// Token: 0x06002BB1 RID: 11185
		public abstract double? GetElevation(DbGeometry geometryValue);

		// Token: 0x06002BB2 RID: 11186
		public abstract double? GetMeasure(DbGeometry geometryValue);

		// Token: 0x06002BB3 RID: 11187
		public abstract double? GetLength(DbGeometry geometryValue);

		// Token: 0x06002BB4 RID: 11188
		public abstract DbGeometry GetStartPoint(DbGeometry geometryValue);

		// Token: 0x06002BB5 RID: 11189
		public abstract DbGeometry GetEndPoint(DbGeometry geometryValue);

		// Token: 0x06002BB6 RID: 11190
		public abstract bool? GetIsClosed(DbGeometry geometryValue);

		// Token: 0x06002BB7 RID: 11191
		public abstract bool? GetIsRing(DbGeometry geometryValue);

		// Token: 0x06002BB8 RID: 11192
		public abstract int? GetPointCount(DbGeometry geometryValue);

		// Token: 0x06002BB9 RID: 11193
		public abstract DbGeometry PointAt(DbGeometry geometryValue, int index);

		// Token: 0x06002BBA RID: 11194
		public abstract double? GetArea(DbGeometry geometryValue);

		// Token: 0x06002BBB RID: 11195
		public abstract DbGeometry GetCentroid(DbGeometry geometryValue);

		// Token: 0x06002BBC RID: 11196
		public abstract DbGeometry GetPointOnSurface(DbGeometry geometryValue);

		// Token: 0x06002BBD RID: 11197
		public abstract DbGeometry GetExteriorRing(DbGeometry geometryValue);

		// Token: 0x06002BBE RID: 11198
		public abstract int? GetInteriorRingCount(DbGeometry geometryValue);

		// Token: 0x06002BBF RID: 11199
		public abstract DbGeometry InteriorRingAt(DbGeometry geometryValue, int index);

		// Token: 0x040012FC RID: 4860
		private static readonly Singleton<DbSpatialServices> defaultServices = new Singleton<DbSpatialServices>(new Func<DbSpatialServices>(DbSpatialServices.LoadDefaultServices));
	}
}
