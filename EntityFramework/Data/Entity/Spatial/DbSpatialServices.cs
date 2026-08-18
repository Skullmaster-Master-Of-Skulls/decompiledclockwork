using System;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;

namespace System.Data.Entity.Spatial
{
	// Token: 0x0200071E RID: 1822
	[Serializable]
	public abstract class DbSpatialServices
	{
		// Token: 0x17000B46 RID: 2886
		// (get) Token: 0x06004A12 RID: 18962 RVA: 0x001604B8 File Offset: 0x0015E6B8
		public static DbSpatialServices Default
		{
			get
			{
				return DbSpatialServices._defaultServices.Value;
			}
		}

		// Token: 0x17000B47 RID: 2887
		// (get) Token: 0x06004A13 RID: 18963 RVA: 0x001604C4 File Offset: 0x0015E6C4
		public virtual bool NativeTypesAvailable
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06004A14 RID: 18964 RVA: 0x001604C7 File Offset: 0x0015E6C7
		protected static DbGeography CreateGeography(DbSpatialServices spatialServices, object providerValue)
		{
			Check.NotNull<DbSpatialServices>(spatialServices, "spatialServices");
			Check.NotNull<object>(providerValue, "providerValue");
			return new DbGeography(spatialServices, providerValue);
		}

		// Token: 0x06004A15 RID: 18965
		public abstract DbGeography GeographyFromProviderValue(object providerValue);

		// Token: 0x06004A16 RID: 18966
		public abstract object CreateProviderValue(DbGeographyWellKnownValue wellKnownValue);

		// Token: 0x06004A17 RID: 18967
		public abstract DbGeographyWellKnownValue CreateWellKnownValue(DbGeography geographyValue);

		// Token: 0x06004A18 RID: 18968
		public abstract DbGeography GeographyFromBinary(byte[] wellKnownBinary);

		// Token: 0x06004A19 RID: 18969
		public abstract DbGeography GeographyFromBinary(byte[] wellKnownBinary, int coordinateSystemId);

		// Token: 0x06004A1A RID: 18970
		public abstract DbGeography GeographyLineFromBinary(byte[] lineWellKnownBinary, int coordinateSystemId);

		// Token: 0x06004A1B RID: 18971
		public abstract DbGeography GeographyPointFromBinary(byte[] pointWellKnownBinary, int coordinateSystemId);

		// Token: 0x06004A1C RID: 18972
		public abstract DbGeography GeographyPolygonFromBinary(byte[] polygonWellKnownBinary, int coordinateSystemId);

		// Token: 0x06004A1D RID: 18973
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "multi", Justification = "Match OGC, EDM")]
		[SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "MultiLine", Justification = "Match OGC, EDM")]
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Multi", Justification = "Match OGC, EDM")]
		[SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "multiLine", Justification = "Match OGC, EDM")]
		public abstract DbGeography GeographyMultiLineFromBinary(byte[] multiLineWellKnownBinary, int coordinateSystemId);

		// Token: 0x06004A1E RID: 18974
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Multi", Justification = "Match OGC, EDM")]
		[SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "MultiPoint", Justification = "Match OGC, EDM")]
		[SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "multiPoint", Justification = "Match OGC, EDM")]
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "multi", Justification = "Match OGC, EDM")]
		public abstract DbGeography GeographyMultiPointFromBinary(byte[] multiPointWellKnownBinary, int coordinateSystemId);

		// Token: 0x06004A1F RID: 18975
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Multi", Justification = "Match OGC, EDM")]
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "multi", Justification = "Match OGC, EDM")]
		public abstract DbGeography GeographyMultiPolygonFromBinary(byte[] multiPolygonWellKnownBinary, int coordinateSystemId);

		// Token: 0x06004A20 RID: 18976
		public abstract DbGeography GeographyCollectionFromBinary(byte[] geographyCollectionWellKnownBinary, int coordinateSystemId);

		// Token: 0x06004A21 RID: 18977
		public abstract DbGeography GeographyFromText(string wellKnownText);

		// Token: 0x06004A22 RID: 18978
		public abstract DbGeography GeographyFromText(string wellKnownText, int coordinateSystemId);

		// Token: 0x06004A23 RID: 18979
		public abstract DbGeography GeographyLineFromText(string lineWellKnownText, int coordinateSystemId);

		// Token: 0x06004A24 RID: 18980
		public abstract DbGeography GeographyPointFromText(string pointWellKnownText, int coordinateSystemId);

		// Token: 0x06004A25 RID: 18981
		public abstract DbGeography GeographyPolygonFromText(string polygonWellKnownText, int coordinateSystemId);

		// Token: 0x06004A26 RID: 18982
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "multi", Justification = "Match OGC, EDM")]
		[SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "MultiLine", Justification = "Match OGC, EDM")]
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Multi", Justification = "Match OGC, EDM")]
		[SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "multiLine", Justification = "Match OGC, EDM")]
		public abstract DbGeography GeographyMultiLineFromText(string multiLineWellKnownText, int coordinateSystemId);

		// Token: 0x06004A27 RID: 18983
		[SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "MultiPoint", Justification = "Match OGC, EDM")]
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "multi", Justification = "Match OGC, EDM")]
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Multi", Justification = "Match OGC, EDM")]
		[SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "multiPoint", Justification = "Match OGC, EDM")]
		public abstract DbGeography GeographyMultiPointFromText(string multiPointWellKnownText, int coordinateSystemId);

		// Token: 0x06004A28 RID: 18984
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "multi", Justification = "Match OGC, EDM")]
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Multi", Justification = "Match OGC, EDM")]
		public abstract DbGeography GeographyMultiPolygonFromText(string multiPolygonKnownText, int coordinateSystemId);

		// Token: 0x06004A29 RID: 18985
		public abstract DbGeography GeographyCollectionFromText(string geographyCollectionWellKnownText, int coordinateSystemId);

		// Token: 0x06004A2A RID: 18986
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Gml")]
		public abstract DbGeography GeographyFromGml(string geographyMarkup);

		// Token: 0x06004A2B RID: 18987
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Gml")]
		public abstract DbGeography GeographyFromGml(string geographyMarkup, int coordinateSystemId);

		// Token: 0x06004A2C RID: 18988
		public abstract int GetCoordinateSystemId(DbGeography geographyValue);

		// Token: 0x06004A2D RID: 18989
		public abstract int GetDimension(DbGeography geographyValue);

		// Token: 0x06004A2E RID: 18990
		public abstract string GetSpatialTypeName(DbGeography geographyValue);

		// Token: 0x06004A2F RID: 18991
		public abstract bool GetIsEmpty(DbGeography geographyValue);

		// Token: 0x06004A30 RID: 18992
		public abstract string AsText(DbGeography geographyValue);

		// Token: 0x06004A31 RID: 18993 RVA: 0x001604E8 File Offset: 0x0015E6E8
		public virtual string AsTextIncludingElevationAndMeasure(DbGeography geographyValue)
		{
			return null;
		}

		// Token: 0x06004A32 RID: 18994
		public abstract byte[] AsBinary(DbGeography geographyValue);

		// Token: 0x06004A33 RID: 18995
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Gml")]
		public abstract string AsGml(DbGeography geographyValue);

		// Token: 0x06004A34 RID: 18996
		public abstract bool SpatialEquals(DbGeography geographyValue, DbGeography otherGeography);

		// Token: 0x06004A35 RID: 18997
		public abstract bool Disjoint(DbGeography geographyValue, DbGeography otherGeography);

		// Token: 0x06004A36 RID: 18998
		public abstract bool Intersects(DbGeography geographyValue, DbGeography otherGeography);

		// Token: 0x06004A37 RID: 18999
		public abstract DbGeography Buffer(DbGeography geographyValue, double distance);

		// Token: 0x06004A38 RID: 19000
		public abstract double Distance(DbGeography geographyValue, DbGeography otherGeography);

		// Token: 0x06004A39 RID: 19001
		public abstract DbGeography Intersection(DbGeography geographyValue, DbGeography otherGeography);

		// Token: 0x06004A3A RID: 19002
		public abstract DbGeography Union(DbGeography geographyValue, DbGeography otherGeography);

		// Token: 0x06004A3B RID: 19003
		public abstract DbGeography Difference(DbGeography geographyValue, DbGeography otherGeography);

		// Token: 0x06004A3C RID: 19004
		public abstract DbGeography SymmetricDifference(DbGeography geographyValue, DbGeography otherGeography);

		// Token: 0x06004A3D RID: 19005
		public abstract int? GetElementCount(DbGeography geographyValue);

		// Token: 0x06004A3E RID: 19006
		public abstract DbGeography ElementAt(DbGeography geographyValue, int index);

		// Token: 0x06004A3F RID: 19007
		public abstract double? GetLatitude(DbGeography geographyValue);

		// Token: 0x06004A40 RID: 19008
		public abstract double? GetLongitude(DbGeography geographyValue);

		// Token: 0x06004A41 RID: 19009
		public abstract double? GetElevation(DbGeography geographyValue);

		// Token: 0x06004A42 RID: 19010
		public abstract double? GetMeasure(DbGeography geographyValue);

		// Token: 0x06004A43 RID: 19011
		public abstract double? GetLength(DbGeography geographyValue);

		// Token: 0x06004A44 RID: 19012
		public abstract DbGeography GetStartPoint(DbGeography geographyValue);

		// Token: 0x06004A45 RID: 19013
		public abstract DbGeography GetEndPoint(DbGeography geographyValue);

		// Token: 0x06004A46 RID: 19014
		public abstract bool? GetIsClosed(DbGeography geographyValue);

		// Token: 0x06004A47 RID: 19015
		public abstract int? GetPointCount(DbGeography geographyValue);

		// Token: 0x06004A48 RID: 19016
		public abstract DbGeography PointAt(DbGeography geographyValue, int index);

		// Token: 0x06004A49 RID: 19017
		public abstract double? GetArea(DbGeography geographyValue);

		// Token: 0x06004A4A RID: 19018 RVA: 0x001604EB File Offset: 0x0015E6EB
		protected static DbGeometry CreateGeometry(DbSpatialServices spatialServices, object providerValue)
		{
			Check.NotNull<DbSpatialServices>(spatialServices, "spatialServices");
			Check.NotNull<object>(providerValue, "providerValue");
			return new DbGeometry(spatialServices, providerValue);
		}

		// Token: 0x06004A4B RID: 19019
		public abstract object CreateProviderValue(DbGeometryWellKnownValue wellKnownValue);

		// Token: 0x06004A4C RID: 19020
		public abstract DbGeometryWellKnownValue CreateWellKnownValue(DbGeometry geometryValue);

		// Token: 0x06004A4D RID: 19021
		public abstract DbGeometry GeometryFromProviderValue(object providerValue);

		// Token: 0x06004A4E RID: 19022
		public abstract DbGeometry GeometryFromBinary(byte[] wellKnownBinary);

		// Token: 0x06004A4F RID: 19023
		public abstract DbGeometry GeometryFromBinary(byte[] wellKnownBinary, int coordinateSystemId);

		// Token: 0x06004A50 RID: 19024
		public abstract DbGeometry GeometryLineFromBinary(byte[] lineWellKnownBinary, int coordinateSystemId);

		// Token: 0x06004A51 RID: 19025
		public abstract DbGeometry GeometryPointFromBinary(byte[] pointWellKnownBinary, int coordinateSystemId);

		// Token: 0x06004A52 RID: 19026
		public abstract DbGeometry GeometryPolygonFromBinary(byte[] polygonWellKnownBinary, int coordinateSystemId);

		// Token: 0x06004A53 RID: 19027
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "multi", Justification = "Match OGC, EDM")]
		[SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "MultiLine", Justification = "Match OGC, EDM")]
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Multi", Justification = "Match OGC, EDM")]
		[SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "multiLine", Justification = "Match OGC, EDM")]
		public abstract DbGeometry GeometryMultiLineFromBinary(byte[] multiLineWellKnownBinary, int coordinateSystemId);

		// Token: 0x06004A54 RID: 19028
		[SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "MultiPoint", Justification = "Match OGC, EDM")]
		[SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "multiPoint", Justification = "Match OGC, EDM")]
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "multi", Justification = "Match OGC, EDM")]
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Multi", Justification = "Match OGC, EDM")]
		public abstract DbGeometry GeometryMultiPointFromBinary(byte[] multiPointWellKnownBinary, int coordinateSystemId);

		// Token: 0x06004A55 RID: 19029
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "multi", Justification = "Match OGC, EDM")]
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Multi", Justification = "Match OGC, EDM")]
		public abstract DbGeometry GeometryMultiPolygonFromBinary(byte[] multiPolygonWellKnownBinary, int coordinateSystemId);

		// Token: 0x06004A56 RID: 19030
		public abstract DbGeometry GeometryCollectionFromBinary(byte[] geometryCollectionWellKnownBinary, int coordinateSystemId);

		// Token: 0x06004A57 RID: 19031
		public abstract DbGeometry GeometryFromText(string wellKnownText);

		// Token: 0x06004A58 RID: 19032
		public abstract DbGeometry GeometryFromText(string wellKnownText, int coordinateSystemId);

		// Token: 0x06004A59 RID: 19033
		public abstract DbGeometry GeometryLineFromText(string lineWellKnownText, int coordinateSystemId);

		// Token: 0x06004A5A RID: 19034
		public abstract DbGeometry GeometryPointFromText(string pointWellKnownText, int coordinateSystemId);

		// Token: 0x06004A5B RID: 19035
		public abstract DbGeometry GeometryPolygonFromText(string polygonWellKnownText, int coordinateSystemId);

		// Token: 0x06004A5C RID: 19036
		[SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "multiLine", Justification = "Match OGC, EDM")]
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "multi", Justification = "Match OGC, EDM")]
		[SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "MultiLine", Justification = "Match OGC, EDM")]
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Multi", Justification = "Match OGC, EDM")]
		public abstract DbGeometry GeometryMultiLineFromText(string multiLineWellKnownText, int coordinateSystemId);

		// Token: 0x06004A5D RID: 19037
		[SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "multiPoint", Justification = "Match OGC, EDM")]
		[SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "MultiPoint", Justification = "Match OGC, EDM")]
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Multi", Justification = "Match OGC, EDM")]
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "multi", Justification = "Match OGC, EDM")]
		public abstract DbGeometry GeometryMultiPointFromText(string multiPointWellKnownText, int coordinateSystemId);

		// Token: 0x06004A5E RID: 19038
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Multi", Justification = "Match OGC, EDM")]
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "multi", Justification = "Match OGC, EDM")]
		public abstract DbGeometry GeometryMultiPolygonFromText(string multiPolygonKnownText, int coordinateSystemId);

		// Token: 0x06004A5F RID: 19039
		public abstract DbGeometry GeometryCollectionFromText(string geometryCollectionWellKnownText, int coordinateSystemId);

		// Token: 0x06004A60 RID: 19040
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Gml")]
		public abstract DbGeometry GeometryFromGml(string geometryMarkup);

		// Token: 0x06004A61 RID: 19041
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Gml")]
		public abstract DbGeometry GeometryFromGml(string geometryMarkup, int coordinateSystemId);

		// Token: 0x06004A62 RID: 19042
		public abstract int GetCoordinateSystemId(DbGeometry geometryValue);

		// Token: 0x06004A63 RID: 19043
		public abstract DbGeometry GetBoundary(DbGeometry geometryValue);

		// Token: 0x06004A64 RID: 19044
		public abstract int GetDimension(DbGeometry geometryValue);

		// Token: 0x06004A65 RID: 19045
		public abstract DbGeometry GetEnvelope(DbGeometry geometryValue);

		// Token: 0x06004A66 RID: 19046
		public abstract string GetSpatialTypeName(DbGeometry geometryValue);

		// Token: 0x06004A67 RID: 19047
		public abstract bool GetIsEmpty(DbGeometry geometryValue);

		// Token: 0x06004A68 RID: 19048
		public abstract bool GetIsSimple(DbGeometry geometryValue);

		// Token: 0x06004A69 RID: 19049
		public abstract bool GetIsValid(DbGeometry geometryValue);

		// Token: 0x06004A6A RID: 19050
		public abstract string AsText(DbGeometry geometryValue);

		// Token: 0x06004A6B RID: 19051 RVA: 0x0016050C File Offset: 0x0015E70C
		public virtual string AsTextIncludingElevationAndMeasure(DbGeometry geometryValue)
		{
			return null;
		}

		// Token: 0x06004A6C RID: 19052
		public abstract byte[] AsBinary(DbGeometry geometryValue);

		// Token: 0x06004A6D RID: 19053
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Gml")]
		public abstract string AsGml(DbGeometry geometryValue);

		// Token: 0x06004A6E RID: 19054
		public abstract bool SpatialEquals(DbGeometry geometryValue, DbGeometry otherGeometry);

		// Token: 0x06004A6F RID: 19055
		public abstract bool Disjoint(DbGeometry geometryValue, DbGeometry otherGeometry);

		// Token: 0x06004A70 RID: 19056
		public abstract bool Intersects(DbGeometry geometryValue, DbGeometry otherGeometry);

		// Token: 0x06004A71 RID: 19057
		public abstract bool Touches(DbGeometry geometryValue, DbGeometry otherGeometry);

		// Token: 0x06004A72 RID: 19058
		public abstract bool Crosses(DbGeometry geometryValue, DbGeometry otherGeometry);

		// Token: 0x06004A73 RID: 19059
		public abstract bool Within(DbGeometry geometryValue, DbGeometry otherGeometry);

		// Token: 0x06004A74 RID: 19060
		public abstract bool Contains(DbGeometry geometryValue, DbGeometry otherGeometry);

		// Token: 0x06004A75 RID: 19061
		public abstract bool Overlaps(DbGeometry geometryValue, DbGeometry otherGeometry);

		// Token: 0x06004A76 RID: 19062
		public abstract bool Relate(DbGeometry geometryValue, DbGeometry otherGeometry, string matrix);

		// Token: 0x06004A77 RID: 19063
		public abstract DbGeometry Buffer(DbGeometry geometryValue, double distance);

		// Token: 0x06004A78 RID: 19064
		public abstract double Distance(DbGeometry geometryValue, DbGeometry otherGeometry);

		// Token: 0x06004A79 RID: 19065
		public abstract DbGeometry GetConvexHull(DbGeometry geometryValue);

		// Token: 0x06004A7A RID: 19066
		public abstract DbGeometry Intersection(DbGeometry geometryValue, DbGeometry otherGeometry);

		// Token: 0x06004A7B RID: 19067
		public abstract DbGeometry Union(DbGeometry geometryValue, DbGeometry otherGeometry);

		// Token: 0x06004A7C RID: 19068
		public abstract DbGeometry Difference(DbGeometry geometryValue, DbGeometry otherGeometry);

		// Token: 0x06004A7D RID: 19069
		public abstract DbGeometry SymmetricDifference(DbGeometry geometryValue, DbGeometry otherGeometry);

		// Token: 0x06004A7E RID: 19070
		public abstract int? GetElementCount(DbGeometry geometryValue);

		// Token: 0x06004A7F RID: 19071
		public abstract DbGeometry ElementAt(DbGeometry geometryValue, int index);

		// Token: 0x06004A80 RID: 19072
		public abstract double? GetXCoordinate(DbGeometry geometryValue);

		// Token: 0x06004A81 RID: 19073
		public abstract double? GetYCoordinate(DbGeometry geometryValue);

		// Token: 0x06004A82 RID: 19074
		public abstract double? GetElevation(DbGeometry geometryValue);

		// Token: 0x06004A83 RID: 19075
		public abstract double? GetMeasure(DbGeometry geometryValue);

		// Token: 0x06004A84 RID: 19076
		public abstract double? GetLength(DbGeometry geometryValue);

		// Token: 0x06004A85 RID: 19077
		public abstract DbGeometry GetStartPoint(DbGeometry geometryValue);

		// Token: 0x06004A86 RID: 19078
		public abstract DbGeometry GetEndPoint(DbGeometry geometryValue);

		// Token: 0x06004A87 RID: 19079
		public abstract bool? GetIsClosed(DbGeometry geometryValue);

		// Token: 0x06004A88 RID: 19080
		public abstract bool? GetIsRing(DbGeometry geometryValue);

		// Token: 0x06004A89 RID: 19081
		public abstract int? GetPointCount(DbGeometry geometryValue);

		// Token: 0x06004A8A RID: 19082
		public abstract DbGeometry PointAt(DbGeometry geometryValue, int index);

		// Token: 0x06004A8B RID: 19083
		public abstract double? GetArea(DbGeometry geometryValue);

		// Token: 0x06004A8C RID: 19084
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Centroid", Justification = "Naming convention prescribed by OGC specification")]
		public abstract DbGeometry GetCentroid(DbGeometry geometryValue);

		// Token: 0x06004A8D RID: 19085
		public abstract DbGeometry GetPointOnSurface(DbGeometry geometryValue);

		// Token: 0x06004A8E RID: 19086
		public abstract DbGeometry GetExteriorRing(DbGeometry geometryValue);

		// Token: 0x06004A8F RID: 19087
		public abstract int? GetInteriorRingCount(DbGeometry geometryValue);

		// Token: 0x06004A90 RID: 19088
		public abstract DbGeometry InteriorRingAt(DbGeometry geometryValue, int index);

		// Token: 0x04001B58 RID: 7000
		private static readonly Lazy<DbSpatialServices> _defaultServices = new Lazy<DbSpatialServices>(() => new SpatialServicesLoader(DbConfiguration.DependencyResolver).LoadDefaultServices(), true);
	}
}
