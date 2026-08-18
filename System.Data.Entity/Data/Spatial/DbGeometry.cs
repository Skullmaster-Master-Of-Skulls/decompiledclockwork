using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Spatial.Internal;
using System.Globalization;
using System.Runtime.Serialization;

namespace System.Data.Spatial
{
	// Token: 0x020002D8 RID: 728
	[DataContract]
	[BindableType]
	[Serializable]
	public class DbGeometry
	{
		// Token: 0x06002AF0 RID: 10992 RVA: 0x000A79DC File Offset: 0x000A5BDC
		internal DbGeometry(DbSpatialServices spatialServices, object spatialProviderValue)
		{
			this.spatialSvcs = spatialServices;
			this.providerValue = spatialProviderValue;
		}

		// Token: 0x17000864 RID: 2148
		// (get) Token: 0x06002AF1 RID: 10993 RVA: 0x000173E2 File Offset: 0x000155E2
		public static int DefaultCoordinateSystemId
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000865 RID: 2149
		// (get) Token: 0x06002AF2 RID: 10994 RVA: 0x000A79F2 File Offset: 0x000A5BF2
		public object ProviderValue
		{
			get
			{
				return this.providerValue;
			}
		}

		// Token: 0x17000866 RID: 2150
		// (get) Token: 0x06002AF3 RID: 10995 RVA: 0x000A79FA File Offset: 0x000A5BFA
		// (set) Token: 0x06002AF4 RID: 10996 RVA: 0x000A7A08 File Offset: 0x000A5C08
		[DataMember(Name = "Geometry")]
		public DbGeometryWellKnownValue WellKnownValue
		{
			get
			{
				return this.spatialSvcs.CreateWellKnownValue(this);
			}
			set
			{
				if (this.spatialSvcs != null)
				{
					throw SpatialExceptions.WellKnownValueSerializationPropertyNotDirectlySettable();
				}
				DbSpatialServices @default = DbSpatialServices.Default;
				this.providerValue = @default.CreateProviderValue(value);
				this.spatialSvcs = @default;
			}
		}

		// Token: 0x06002AF5 RID: 10997 RVA: 0x000A7A3D File Offset: 0x000A5C3D
		public static DbGeometry FromBinary(byte[] wellKnownBinary)
		{
			wellKnownBinary.CheckNull("wellKnownBinary");
			return DbSpatialServices.Default.GeometryFromBinary(wellKnownBinary);
		}

		// Token: 0x06002AF6 RID: 10998 RVA: 0x000A7A55 File Offset: 0x000A5C55
		public static DbGeometry FromBinary(byte[] wellKnownBinary, int coordinateSystemId)
		{
			wellKnownBinary.CheckNull("wellKnownBinary");
			return DbSpatialServices.Default.GeometryFromBinary(wellKnownBinary, coordinateSystemId);
		}

		// Token: 0x06002AF7 RID: 10999 RVA: 0x000A7A6E File Offset: 0x000A5C6E
		public static DbGeometry LineFromBinary(byte[] lineWellKnownBinary, int coordinateSystemId)
		{
			lineWellKnownBinary.CheckNull("lineWellKnownBinary");
			return DbSpatialServices.Default.GeometryLineFromBinary(lineWellKnownBinary, coordinateSystemId);
		}

		// Token: 0x06002AF8 RID: 11000 RVA: 0x000A7A87 File Offset: 0x000A5C87
		public static DbGeometry PointFromBinary(byte[] pointWellKnownBinary, int coordinateSystemId)
		{
			pointWellKnownBinary.CheckNull("pointWellKnownBinary");
			return DbSpatialServices.Default.GeometryPointFromBinary(pointWellKnownBinary, coordinateSystemId);
		}

		// Token: 0x06002AF9 RID: 11001 RVA: 0x000A7AA0 File Offset: 0x000A5CA0
		public static DbGeometry PolygonFromBinary(byte[] polygonWellKnownBinary, int coordinateSystemId)
		{
			polygonWellKnownBinary.CheckNull("polygonWellKnownBinary");
			return DbSpatialServices.Default.GeometryPolygonFromBinary(polygonWellKnownBinary, coordinateSystemId);
		}

		// Token: 0x06002AFA RID: 11002 RVA: 0x000A7AB9 File Offset: 0x000A5CB9
		public static DbGeometry MultiLineFromBinary(byte[] multiLineWellKnownBinary, int coordinateSystemId)
		{
			multiLineWellKnownBinary.CheckNull("multiLineWellKnownBinary");
			return DbSpatialServices.Default.GeometryMultiLineFromBinary(multiLineWellKnownBinary, coordinateSystemId);
		}

		// Token: 0x06002AFB RID: 11003 RVA: 0x000A7AD2 File Offset: 0x000A5CD2
		public static DbGeometry MultiPointFromBinary(byte[] multiPointWellKnownBinary, int coordinateSystemId)
		{
			multiPointWellKnownBinary.CheckNull("multiPointWellKnownBinary");
			return DbSpatialServices.Default.GeometryMultiPointFromBinary(multiPointWellKnownBinary, coordinateSystemId);
		}

		// Token: 0x06002AFC RID: 11004 RVA: 0x000A7AEB File Offset: 0x000A5CEB
		public static DbGeometry MultiPolygonFromBinary(byte[] multiPolygonWellKnownBinary, int coordinateSystemId)
		{
			multiPolygonWellKnownBinary.CheckNull("multiPolygonWellKnownBinary");
			return DbSpatialServices.Default.GeometryMultiPolygonFromBinary(multiPolygonWellKnownBinary, coordinateSystemId);
		}

		// Token: 0x06002AFD RID: 11005 RVA: 0x000A7B04 File Offset: 0x000A5D04
		public static DbGeometry GeometryCollectionFromBinary(byte[] geometryCollectionWellKnownBinary, int coordinateSystemId)
		{
			geometryCollectionWellKnownBinary.CheckNull("geometryCollectionWellKnownBinary");
			return DbSpatialServices.Default.GeometryCollectionFromBinary(geometryCollectionWellKnownBinary, coordinateSystemId);
		}

		// Token: 0x06002AFE RID: 11006 RVA: 0x000A7B1D File Offset: 0x000A5D1D
		public static DbGeometry FromGml(string geometryMarkup)
		{
			geometryMarkup.CheckNull("geometryMarkup");
			return DbSpatialServices.Default.GeometryFromGml(geometryMarkup);
		}

		// Token: 0x06002AFF RID: 11007 RVA: 0x000A7B35 File Offset: 0x000A5D35
		public static DbGeometry FromGml(string geometryMarkup, int coordinateSystemId)
		{
			geometryMarkup.CheckNull("geometryMarkup");
			return DbSpatialServices.Default.GeometryFromGml(geometryMarkup, coordinateSystemId);
		}

		// Token: 0x06002B00 RID: 11008 RVA: 0x000A7B4E File Offset: 0x000A5D4E
		public static DbGeometry FromText(string wellKnownText)
		{
			wellKnownText.CheckNull("wellKnownText");
			return DbSpatialServices.Default.GeometryFromText(wellKnownText);
		}

		// Token: 0x06002B01 RID: 11009 RVA: 0x000A7B66 File Offset: 0x000A5D66
		public static DbGeometry FromText(string wellKnownText, int coordinateSystemId)
		{
			wellKnownText.CheckNull("wellKnownText");
			return DbSpatialServices.Default.GeometryFromText(wellKnownText, coordinateSystemId);
		}

		// Token: 0x06002B02 RID: 11010 RVA: 0x000A7B7F File Offset: 0x000A5D7F
		public static DbGeometry LineFromText(string lineWellKnownText, int coordinateSystemId)
		{
			lineWellKnownText.CheckNull("lineWellKnownText");
			return DbSpatialServices.Default.GeometryLineFromText(lineWellKnownText, coordinateSystemId);
		}

		// Token: 0x06002B03 RID: 11011 RVA: 0x000A7B98 File Offset: 0x000A5D98
		public static DbGeometry PointFromText(string pointWellKnownText, int coordinateSystemId)
		{
			pointWellKnownText.CheckNull("pointWellKnownText");
			return DbSpatialServices.Default.GeometryPointFromText(pointWellKnownText, coordinateSystemId);
		}

		// Token: 0x06002B04 RID: 11012 RVA: 0x000A7BB1 File Offset: 0x000A5DB1
		public static DbGeometry PolygonFromText(string polygonWellKnownText, int coordinateSystemId)
		{
			polygonWellKnownText.CheckNull("polygonWellKnownText");
			return DbSpatialServices.Default.GeometryPolygonFromText(polygonWellKnownText, coordinateSystemId);
		}

		// Token: 0x06002B05 RID: 11013 RVA: 0x000A7BCA File Offset: 0x000A5DCA
		public static DbGeometry MultiLineFromText(string multiLineWellKnownText, int coordinateSystemId)
		{
			multiLineWellKnownText.CheckNull("multiLineWellKnownText");
			return DbSpatialServices.Default.GeometryMultiLineFromText(multiLineWellKnownText, coordinateSystemId);
		}

		// Token: 0x06002B06 RID: 11014 RVA: 0x000A7BE3 File Offset: 0x000A5DE3
		public static DbGeometry MultiPointFromText(string multiPointWellKnownText, int coordinateSystemId)
		{
			multiPointWellKnownText.CheckNull("multiPointWellKnownText");
			return DbSpatialServices.Default.GeometryMultiPointFromText(multiPointWellKnownText, coordinateSystemId);
		}

		// Token: 0x06002B07 RID: 11015 RVA: 0x000A7BFC File Offset: 0x000A5DFC
		public static DbGeometry MultiPolygonFromText(string multiPolygonWellKnownText, int coordinateSystemId)
		{
			multiPolygonWellKnownText.CheckNull("multiPolygonWellKnownText");
			return DbSpatialServices.Default.GeometryMultiPolygonFromText(multiPolygonWellKnownText, coordinateSystemId);
		}

		// Token: 0x06002B08 RID: 11016 RVA: 0x000A7C15 File Offset: 0x000A5E15
		public static DbGeometry GeometryCollectionFromText(string geometryCollectionWellKnownText, int coordinateSystemId)
		{
			geometryCollectionWellKnownText.CheckNull("geometryCollectionWellKnownText");
			return DbSpatialServices.Default.GeometryCollectionFromText(geometryCollectionWellKnownText, coordinateSystemId);
		}

		// Token: 0x17000867 RID: 2151
		// (get) Token: 0x06002B09 RID: 11017 RVA: 0x000A7C2E File Offset: 0x000A5E2E
		public int CoordinateSystemId
		{
			get
			{
				return this.spatialSvcs.GetCoordinateSystemId(this);
			}
		}

		// Token: 0x17000868 RID: 2152
		// (get) Token: 0x06002B0A RID: 11018 RVA: 0x000A7C3C File Offset: 0x000A5E3C
		public DbGeometry Boundary
		{
			get
			{
				return this.spatialSvcs.GetBoundary(this);
			}
		}

		// Token: 0x17000869 RID: 2153
		// (get) Token: 0x06002B0B RID: 11019 RVA: 0x000A7C4A File Offset: 0x000A5E4A
		public int Dimension
		{
			get
			{
				return this.spatialSvcs.GetDimension(this);
			}
		}

		// Token: 0x1700086A RID: 2154
		// (get) Token: 0x06002B0C RID: 11020 RVA: 0x000A7C58 File Offset: 0x000A5E58
		public DbGeometry Envelope
		{
			get
			{
				return this.spatialSvcs.GetEnvelope(this);
			}
		}

		// Token: 0x1700086B RID: 2155
		// (get) Token: 0x06002B0D RID: 11021 RVA: 0x000A7C66 File Offset: 0x000A5E66
		public string SpatialTypeName
		{
			get
			{
				return this.spatialSvcs.GetSpatialTypeName(this);
			}
		}

		// Token: 0x1700086C RID: 2156
		// (get) Token: 0x06002B0E RID: 11022 RVA: 0x000A7C74 File Offset: 0x000A5E74
		public bool IsEmpty
		{
			get
			{
				return this.spatialSvcs.GetIsEmpty(this);
			}
		}

		// Token: 0x1700086D RID: 2157
		// (get) Token: 0x06002B0F RID: 11023 RVA: 0x000A7C82 File Offset: 0x000A5E82
		public bool IsSimple
		{
			get
			{
				return this.spatialSvcs.GetIsSimple(this);
			}
		}

		// Token: 0x1700086E RID: 2158
		// (get) Token: 0x06002B10 RID: 11024 RVA: 0x000A7C90 File Offset: 0x000A5E90
		public bool IsValid
		{
			get
			{
				return this.spatialSvcs.GetIsValid(this);
			}
		}

		// Token: 0x06002B11 RID: 11025 RVA: 0x000A7C9E File Offset: 0x000A5E9E
		public string AsText()
		{
			return this.spatialSvcs.AsText(this);
		}

		// Token: 0x06002B12 RID: 11026 RVA: 0x000A7CAC File Offset: 0x000A5EAC
		internal string AsTextIncludingElevationAndMeasure()
		{
			return this.spatialSvcs.AsTextIncludingElevationAndMeasure(this);
		}

		// Token: 0x06002B13 RID: 11027 RVA: 0x000A7CBA File Offset: 0x000A5EBA
		public byte[] AsBinary()
		{
			return this.spatialSvcs.AsBinary(this);
		}

		// Token: 0x06002B14 RID: 11028 RVA: 0x000A7CC8 File Offset: 0x000A5EC8
		public string AsGml()
		{
			return this.spatialSvcs.AsGml(this);
		}

		// Token: 0x06002B15 RID: 11029 RVA: 0x000A7CD6 File Offset: 0x000A5ED6
		public bool SpatialEquals(DbGeometry other)
		{
			other.CheckNull("other");
			return this.spatialSvcs.SpatialEquals(this, other);
		}

		// Token: 0x06002B16 RID: 11030 RVA: 0x000A7CF0 File Offset: 0x000A5EF0
		public bool Disjoint(DbGeometry other)
		{
			other.CheckNull("other");
			return this.spatialSvcs.Disjoint(this, other);
		}

		// Token: 0x06002B17 RID: 11031 RVA: 0x000A7D0A File Offset: 0x000A5F0A
		public bool Intersects(DbGeometry other)
		{
			other.CheckNull("other");
			return this.spatialSvcs.Intersects(this, other);
		}

		// Token: 0x06002B18 RID: 11032 RVA: 0x000A7D24 File Offset: 0x000A5F24
		public bool Touches(DbGeometry other)
		{
			other.CheckNull("other");
			return this.spatialSvcs.Touches(this, other);
		}

		// Token: 0x06002B19 RID: 11033 RVA: 0x000A7D3E File Offset: 0x000A5F3E
		public bool Crosses(DbGeometry other)
		{
			other.CheckNull("other");
			return this.spatialSvcs.Crosses(this, other);
		}

		// Token: 0x06002B1A RID: 11034 RVA: 0x000A7D58 File Offset: 0x000A5F58
		public bool Within(DbGeometry other)
		{
			other.CheckNull("other");
			return this.spatialSvcs.Within(this, other);
		}

		// Token: 0x06002B1B RID: 11035 RVA: 0x000A7D72 File Offset: 0x000A5F72
		public bool Contains(DbGeometry other)
		{
			other.CheckNull("other");
			return this.spatialSvcs.Contains(this, other);
		}

		// Token: 0x06002B1C RID: 11036 RVA: 0x000A7D8C File Offset: 0x000A5F8C
		public bool Overlaps(DbGeometry other)
		{
			other.CheckNull("other");
			return this.spatialSvcs.Overlaps(this, other);
		}

		// Token: 0x06002B1D RID: 11037 RVA: 0x000A7DA6 File Offset: 0x000A5FA6
		public bool Relate(DbGeometry other, string matrix)
		{
			other.CheckNull("other");
			matrix.CheckNull("matrix");
			return this.spatialSvcs.Relate(this, other, matrix);
		}

		// Token: 0x06002B1E RID: 11038 RVA: 0x000A7DCC File Offset: 0x000A5FCC
		public DbGeometry Buffer(double? distance)
		{
			if (distance == null)
			{
				throw EntityUtil.ArgumentNull("distance");
			}
			return this.spatialSvcs.Buffer(this, distance.Value);
		}

		// Token: 0x06002B1F RID: 11039 RVA: 0x000A7DF5 File Offset: 0x000A5FF5
		public double? Distance(DbGeometry other)
		{
			other.CheckNull("other");
			return new double?(this.spatialSvcs.Distance(this, other));
		}

		// Token: 0x1700086F RID: 2159
		// (get) Token: 0x06002B20 RID: 11040 RVA: 0x000A7E14 File Offset: 0x000A6014
		public DbGeometry ConvexHull
		{
			get
			{
				return this.spatialSvcs.GetConvexHull(this);
			}
		}

		// Token: 0x06002B21 RID: 11041 RVA: 0x000A7E22 File Offset: 0x000A6022
		public DbGeometry Intersection(DbGeometry other)
		{
			other.CheckNull("other");
			return this.spatialSvcs.Intersection(this, other);
		}

		// Token: 0x06002B22 RID: 11042 RVA: 0x000A7E3C File Offset: 0x000A603C
		public DbGeometry Union(DbGeometry other)
		{
			other.CheckNull("other");
			return this.spatialSvcs.Union(this, other);
		}

		// Token: 0x06002B23 RID: 11043 RVA: 0x000A7E56 File Offset: 0x000A6056
		public DbGeometry Difference(DbGeometry other)
		{
			other.CheckNull("other");
			return this.spatialSvcs.Difference(this, other);
		}

		// Token: 0x06002B24 RID: 11044 RVA: 0x000A7E70 File Offset: 0x000A6070
		public DbGeometry SymmetricDifference(DbGeometry other)
		{
			other.CheckNull("other");
			return this.spatialSvcs.SymmetricDifference(this, other);
		}

		// Token: 0x17000870 RID: 2160
		// (get) Token: 0x06002B25 RID: 11045 RVA: 0x000A7E8A File Offset: 0x000A608A
		public int? ElementCount
		{
			get
			{
				return this.spatialSvcs.GetElementCount(this);
			}
		}

		// Token: 0x06002B26 RID: 11046 RVA: 0x000A7E98 File Offset: 0x000A6098
		public DbGeometry ElementAt(int index)
		{
			return this.spatialSvcs.ElementAt(this, index);
		}

		// Token: 0x17000871 RID: 2161
		// (get) Token: 0x06002B27 RID: 11047 RVA: 0x000A7EA7 File Offset: 0x000A60A7
		public double? XCoordinate
		{
			get
			{
				return this.spatialSvcs.GetXCoordinate(this);
			}
		}

		// Token: 0x17000872 RID: 2162
		// (get) Token: 0x06002B28 RID: 11048 RVA: 0x000A7EB5 File Offset: 0x000A60B5
		public double? YCoordinate
		{
			get
			{
				return this.spatialSvcs.GetYCoordinate(this);
			}
		}

		// Token: 0x17000873 RID: 2163
		// (get) Token: 0x06002B29 RID: 11049 RVA: 0x000A7EC3 File Offset: 0x000A60C3
		public double? Elevation
		{
			get
			{
				return this.spatialSvcs.GetElevation(this);
			}
		}

		// Token: 0x17000874 RID: 2164
		// (get) Token: 0x06002B2A RID: 11050 RVA: 0x000A7ED1 File Offset: 0x000A60D1
		public double? Measure
		{
			get
			{
				return this.spatialSvcs.GetMeasure(this);
			}
		}

		// Token: 0x17000875 RID: 2165
		// (get) Token: 0x06002B2B RID: 11051 RVA: 0x000A7EDF File Offset: 0x000A60DF
		public double? Length
		{
			get
			{
				return this.spatialSvcs.GetLength(this);
			}
		}

		// Token: 0x17000876 RID: 2166
		// (get) Token: 0x06002B2C RID: 11052 RVA: 0x000A7EED File Offset: 0x000A60ED
		public DbGeometry StartPoint
		{
			get
			{
				return this.spatialSvcs.GetStartPoint(this);
			}
		}

		// Token: 0x17000877 RID: 2167
		// (get) Token: 0x06002B2D RID: 11053 RVA: 0x000A7EFB File Offset: 0x000A60FB
		public DbGeometry EndPoint
		{
			get
			{
				return this.spatialSvcs.GetEndPoint(this);
			}
		}

		// Token: 0x17000878 RID: 2168
		// (get) Token: 0x06002B2E RID: 11054 RVA: 0x000A7F09 File Offset: 0x000A6109
		public bool? IsClosed
		{
			get
			{
				return this.spatialSvcs.GetIsClosed(this);
			}
		}

		// Token: 0x17000879 RID: 2169
		// (get) Token: 0x06002B2F RID: 11055 RVA: 0x000A7F17 File Offset: 0x000A6117
		public bool? IsRing
		{
			get
			{
				return this.spatialSvcs.GetIsRing(this);
			}
		}

		// Token: 0x1700087A RID: 2170
		// (get) Token: 0x06002B30 RID: 11056 RVA: 0x000A7F25 File Offset: 0x000A6125
		public int? PointCount
		{
			get
			{
				return this.spatialSvcs.GetPointCount(this);
			}
		}

		// Token: 0x06002B31 RID: 11057 RVA: 0x000A7F33 File Offset: 0x000A6133
		public DbGeometry PointAt(int index)
		{
			return this.spatialSvcs.PointAt(this, index);
		}

		// Token: 0x1700087B RID: 2171
		// (get) Token: 0x06002B32 RID: 11058 RVA: 0x000A7F42 File Offset: 0x000A6142
		public double? Area
		{
			get
			{
				return this.spatialSvcs.GetArea(this);
			}
		}

		// Token: 0x1700087C RID: 2172
		// (get) Token: 0x06002B33 RID: 11059 RVA: 0x000A7F50 File Offset: 0x000A6150
		public DbGeometry Centroid
		{
			get
			{
				return this.spatialSvcs.GetCentroid(this);
			}
		}

		// Token: 0x1700087D RID: 2173
		// (get) Token: 0x06002B34 RID: 11060 RVA: 0x000A7F5E File Offset: 0x000A615E
		public DbGeometry PointOnSurface
		{
			get
			{
				return this.spatialSvcs.GetPointOnSurface(this);
			}
		}

		// Token: 0x1700087E RID: 2174
		// (get) Token: 0x06002B35 RID: 11061 RVA: 0x000A7F6C File Offset: 0x000A616C
		public DbGeometry ExteriorRing
		{
			get
			{
				return this.spatialSvcs.GetExteriorRing(this);
			}
		}

		// Token: 0x1700087F RID: 2175
		// (get) Token: 0x06002B36 RID: 11062 RVA: 0x000A7F7A File Offset: 0x000A617A
		public int? InteriorRingCount
		{
			get
			{
				return this.spatialSvcs.GetInteriorRingCount(this);
			}
		}

		// Token: 0x06002B37 RID: 11063 RVA: 0x000A7F88 File Offset: 0x000A6188
		public DbGeometry InteriorRingAt(int index)
		{
			return this.spatialSvcs.InteriorRingAt(this, index);
		}

		// Token: 0x06002B38 RID: 11064 RVA: 0x000A7F97 File Offset: 0x000A6197
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "SRID={1};{0}", new object[]
			{
				this.WellKnownValue.WellKnownText ?? base.ToString(),
				this.CoordinateSystemId
			});
		}

		// Token: 0x040012F7 RID: 4855
		private DbSpatialServices spatialSvcs;

		// Token: 0x040012F8 RID: 4856
		private object providerValue;
	}
}
