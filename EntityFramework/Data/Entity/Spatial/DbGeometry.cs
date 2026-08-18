using System;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.Serialization;

namespace System.Data.Entity.Spatial
{
	// Token: 0x0200071B RID: 1819
	[DataContract]
	[Serializable]
	public class DbGeometry
	{
		// Token: 0x060049B9 RID: 18873 RVA: 0x0015FDA9 File Offset: 0x0015DFA9
		internal DbGeometry()
		{
		}

		// Token: 0x060049BA RID: 18874 RVA: 0x0015FDB1 File Offset: 0x0015DFB1
		internal DbGeometry(DbSpatialServices spatialServices, object spatialProviderValue)
		{
			this._spatialProvider = spatialServices;
			this._providerValue = spatialProviderValue;
		}

		// Token: 0x17000B26 RID: 2854
		// (get) Token: 0x060049BB RID: 18875 RVA: 0x0015FDC7 File Offset: 0x0015DFC7
		public static int DefaultCoordinateSystemId
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000B27 RID: 2855
		// (get) Token: 0x060049BC RID: 18876 RVA: 0x0015FDCA File Offset: 0x0015DFCA
		public object ProviderValue
		{
			get
			{
				return this._providerValue;
			}
		}

		// Token: 0x17000B28 RID: 2856
		// (get) Token: 0x060049BD RID: 18877 RVA: 0x0015FDD2 File Offset: 0x0015DFD2
		public virtual DbSpatialServices Provider
		{
			get
			{
				return this._spatialProvider;
			}
		}

		// Token: 0x17000B29 RID: 2857
		// (get) Token: 0x060049BE RID: 18878 RVA: 0x0015FDDA File Offset: 0x0015DFDA
		// (set) Token: 0x060049BF RID: 18879 RVA: 0x0015FDE8 File Offset: 0x0015DFE8
		[DataMember(Name = "Geometry")]
		public DbGeometryWellKnownValue WellKnownValue
		{
			get
			{
				return this._spatialProvider.CreateWellKnownValue(this);
			}
			set
			{
				if (this._spatialProvider != null)
				{
					throw new InvalidOperationException(Strings.Spatial_WellKnownValueSerializationPropertyNotDirectlySettable);
				}
				DbSpatialServices @default = DbSpatialServices.Default;
				this._providerValue = @default.CreateProviderValue(value);
				this._spatialProvider = @default;
			}
		}

		// Token: 0x060049C0 RID: 18880 RVA: 0x0015FE22 File Offset: 0x0015E022
		public static DbGeometry FromBinary(byte[] wellKnownBinary)
		{
			Check.NotNull<byte[]>(wellKnownBinary, "wellKnownBinary");
			return DbSpatialServices.Default.GeometryFromBinary(wellKnownBinary);
		}

		// Token: 0x060049C1 RID: 18881 RVA: 0x0015FE3B File Offset: 0x0015E03B
		public static DbGeometry FromBinary(byte[] wellKnownBinary, int coordinateSystemId)
		{
			Check.NotNull<byte[]>(wellKnownBinary, "wellKnownBinary");
			return DbSpatialServices.Default.GeometryFromBinary(wellKnownBinary, coordinateSystemId);
		}

		// Token: 0x060049C2 RID: 18882 RVA: 0x0015FE55 File Offset: 0x0015E055
		public static DbGeometry LineFromBinary(byte[] lineWellKnownBinary, int coordinateSystemId)
		{
			Check.NotNull<byte[]>(lineWellKnownBinary, "lineWellKnownBinary");
			return DbSpatialServices.Default.GeometryLineFromBinary(lineWellKnownBinary, coordinateSystemId);
		}

		// Token: 0x060049C3 RID: 18883 RVA: 0x0015FE6F File Offset: 0x0015E06F
		public static DbGeometry PointFromBinary(byte[] pointWellKnownBinary, int coordinateSystemId)
		{
			Check.NotNull<byte[]>(pointWellKnownBinary, "pointWellKnownBinary");
			return DbSpatialServices.Default.GeometryPointFromBinary(pointWellKnownBinary, coordinateSystemId);
		}

		// Token: 0x060049C4 RID: 18884 RVA: 0x0015FE89 File Offset: 0x0015E089
		public static DbGeometry PolygonFromBinary(byte[] polygonWellKnownBinary, int coordinateSystemId)
		{
			Check.NotNull<byte[]>(polygonWellKnownBinary, "polygonWellKnownBinary");
			return DbSpatialServices.Default.GeometryPolygonFromBinary(polygonWellKnownBinary, coordinateSystemId);
		}

		// Token: 0x060049C5 RID: 18885 RVA: 0x0015FEA3 File Offset: 0x0015E0A3
		[SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "multiLine", Justification = "Match OGC, EDM")]
		[SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "MultiLine", Justification = "Match OGC, EDM")]
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Multi", Justification = "Match OGC, EDM")]
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "multi", Justification = "Match OGC, EDM")]
		public static DbGeometry MultiLineFromBinary(byte[] multiLineWellKnownBinary, int coordinateSystemId)
		{
			Check.NotNull<byte[]>(multiLineWellKnownBinary, "multiLineWellKnownBinary");
			return DbSpatialServices.Default.GeometryMultiLineFromBinary(multiLineWellKnownBinary, coordinateSystemId);
		}

		// Token: 0x060049C6 RID: 18886 RVA: 0x0015FEBD File Offset: 0x0015E0BD
		[SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "MultiPoint", Justification = "Match OGC, EDM")]
		[SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "multiPoint", Justification = "Match OGC, EDM")]
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Multi", Justification = "Match OGC, EDM")]
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "multi", Justification = "Match OGC, EDM")]
		public static DbGeometry MultiPointFromBinary(byte[] multiPointWellKnownBinary, int coordinateSystemId)
		{
			Check.NotNull<byte[]>(multiPointWellKnownBinary, "multiPointWellKnownBinary");
			return DbSpatialServices.Default.GeometryMultiPointFromBinary(multiPointWellKnownBinary, coordinateSystemId);
		}

		// Token: 0x060049C7 RID: 18887 RVA: 0x0015FED7 File Offset: 0x0015E0D7
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Multi", Justification = "Match OGC, EDM")]
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "multi", Justification = "Match OGC, EDM")]
		public static DbGeometry MultiPolygonFromBinary(byte[] multiPolygonWellKnownBinary, int coordinateSystemId)
		{
			Check.NotNull<byte[]>(multiPolygonWellKnownBinary, "multiPolygonWellKnownBinary");
			return DbSpatialServices.Default.GeometryMultiPolygonFromBinary(multiPolygonWellKnownBinary, coordinateSystemId);
		}

		// Token: 0x060049C8 RID: 18888 RVA: 0x0015FEF1 File Offset: 0x0015E0F1
		public static DbGeometry GeometryCollectionFromBinary(byte[] geometryCollectionWellKnownBinary, int coordinateSystemId)
		{
			Check.NotNull<byte[]>(geometryCollectionWellKnownBinary, "geometryCollectionWellKnownBinary");
			return DbSpatialServices.Default.GeometryCollectionFromBinary(geometryCollectionWellKnownBinary, coordinateSystemId);
		}

		// Token: 0x060049C9 RID: 18889 RVA: 0x0015FF0B File Offset: 0x0015E10B
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Gml")]
		public static DbGeometry FromGml(string geometryMarkup)
		{
			Check.NotNull<string>(geometryMarkup, "geometryMarkup");
			return DbSpatialServices.Default.GeometryFromGml(geometryMarkup);
		}

		// Token: 0x060049CA RID: 18890 RVA: 0x0015FF24 File Offset: 0x0015E124
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Gml")]
		public static DbGeometry FromGml(string geometryMarkup, int coordinateSystemId)
		{
			Check.NotNull<string>(geometryMarkup, "geometryMarkup");
			return DbSpatialServices.Default.GeometryFromGml(geometryMarkup, coordinateSystemId);
		}

		// Token: 0x060049CB RID: 18891 RVA: 0x0015FF3E File Offset: 0x0015E13E
		public static DbGeometry FromText(string wellKnownText)
		{
			Check.NotNull<string>(wellKnownText, "wellKnownText");
			return DbSpatialServices.Default.GeometryFromText(wellKnownText);
		}

		// Token: 0x060049CC RID: 18892 RVA: 0x0015FF57 File Offset: 0x0015E157
		public static DbGeometry FromText(string wellKnownText, int coordinateSystemId)
		{
			Check.NotNull<string>(wellKnownText, "wellKnownText");
			return DbSpatialServices.Default.GeometryFromText(wellKnownText, coordinateSystemId);
		}

		// Token: 0x060049CD RID: 18893 RVA: 0x0015FF71 File Offset: 0x0015E171
		public static DbGeometry LineFromText(string lineWellKnownText, int coordinateSystemId)
		{
			Check.NotNull<string>(lineWellKnownText, "lineWellKnownText");
			return DbSpatialServices.Default.GeometryLineFromText(lineWellKnownText, coordinateSystemId);
		}

		// Token: 0x060049CE RID: 18894 RVA: 0x0015FF8B File Offset: 0x0015E18B
		public static DbGeometry PointFromText(string pointWellKnownText, int coordinateSystemId)
		{
			Check.NotNull<string>(pointWellKnownText, "pointWellKnownText");
			return DbSpatialServices.Default.GeometryPointFromText(pointWellKnownText, coordinateSystemId);
		}

		// Token: 0x060049CF RID: 18895 RVA: 0x0015FFA5 File Offset: 0x0015E1A5
		public static DbGeometry PolygonFromText(string polygonWellKnownText, int coordinateSystemId)
		{
			Check.NotNull<string>(polygonWellKnownText, "polygonWellKnownText");
			return DbSpatialServices.Default.GeometryPolygonFromText(polygonWellKnownText, coordinateSystemId);
		}

		// Token: 0x060049D0 RID: 18896 RVA: 0x0015FFBF File Offset: 0x0015E1BF
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "multi", Justification = "Match OGC, EDM")]
		[SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "MultiLine", Justification = "Match OGC, EDM")]
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Multi", Justification = "Match OGC, EDM")]
		[SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "multiLine", Justification = "Match OGC, EDM")]
		public static DbGeometry MultiLineFromText(string multiLineWellKnownText, int coordinateSystemId)
		{
			Check.NotNull<string>(multiLineWellKnownText, "multiLineWellKnownText");
			return DbSpatialServices.Default.GeometryMultiLineFromText(multiLineWellKnownText, coordinateSystemId);
		}

		// Token: 0x060049D1 RID: 18897 RVA: 0x0015FFD9 File Offset: 0x0015E1D9
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "multi", Justification = "Match OGC, EDM")]
		[SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "MultiPoint", Justification = "Match OGC, EDM")]
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Multi", Justification = "Match OGC, EDM")]
		[SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "multiPoint", Justification = "Match OGC, EDM")]
		public static DbGeometry MultiPointFromText(string multiPointWellKnownText, int coordinateSystemId)
		{
			Check.NotNull<string>(multiPointWellKnownText, "multiPointWellKnownText");
			return DbSpatialServices.Default.GeometryMultiPointFromText(multiPointWellKnownText, coordinateSystemId);
		}

		// Token: 0x060049D2 RID: 18898 RVA: 0x0015FFF3 File Offset: 0x0015E1F3
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "multi", Justification = "Match OGC, EDM")]
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Multi", Justification = "Match OGC, EDM")]
		public static DbGeometry MultiPolygonFromText(string multiPolygonWellKnownText, int coordinateSystemId)
		{
			Check.NotNull<string>(multiPolygonWellKnownText, "multiPolygonWellKnownText");
			return DbSpatialServices.Default.GeometryMultiPolygonFromText(multiPolygonWellKnownText, coordinateSystemId);
		}

		// Token: 0x060049D3 RID: 18899 RVA: 0x0016000D File Offset: 0x0015E20D
		public static DbGeometry GeometryCollectionFromText(string geometryCollectionWellKnownText, int coordinateSystemId)
		{
			Check.NotNull<string>(geometryCollectionWellKnownText, "geometryCollectionWellKnownText");
			return DbSpatialServices.Default.GeometryCollectionFromText(geometryCollectionWellKnownText, coordinateSystemId);
		}

		// Token: 0x17000B2A RID: 2858
		// (get) Token: 0x060049D4 RID: 18900 RVA: 0x00160027 File Offset: 0x0015E227
		public int CoordinateSystemId
		{
			get
			{
				return this._spatialProvider.GetCoordinateSystemId(this);
			}
		}

		// Token: 0x17000B2B RID: 2859
		// (get) Token: 0x060049D5 RID: 18901 RVA: 0x00160035 File Offset: 0x0015E235
		public DbGeometry Boundary
		{
			get
			{
				return this._spatialProvider.GetBoundary(this);
			}
		}

		// Token: 0x17000B2C RID: 2860
		// (get) Token: 0x060049D6 RID: 18902 RVA: 0x00160043 File Offset: 0x0015E243
		public int Dimension
		{
			get
			{
				return this._spatialProvider.GetDimension(this);
			}
		}

		// Token: 0x17000B2D RID: 2861
		// (get) Token: 0x060049D7 RID: 18903 RVA: 0x00160051 File Offset: 0x0015E251
		public DbGeometry Envelope
		{
			get
			{
				return this._spatialProvider.GetEnvelope(this);
			}
		}

		// Token: 0x17000B2E RID: 2862
		// (get) Token: 0x060049D8 RID: 18904 RVA: 0x0016005F File Offset: 0x0015E25F
		public string SpatialTypeName
		{
			get
			{
				return this._spatialProvider.GetSpatialTypeName(this);
			}
		}

		// Token: 0x17000B2F RID: 2863
		// (get) Token: 0x060049D9 RID: 18905 RVA: 0x0016006D File Offset: 0x0015E26D
		public bool IsEmpty
		{
			get
			{
				return this._spatialProvider.GetIsEmpty(this);
			}
		}

		// Token: 0x17000B30 RID: 2864
		// (get) Token: 0x060049DA RID: 18906 RVA: 0x0016007B File Offset: 0x0015E27B
		public bool IsSimple
		{
			get
			{
				return this._spatialProvider.GetIsSimple(this);
			}
		}

		// Token: 0x17000B31 RID: 2865
		// (get) Token: 0x060049DB RID: 18907 RVA: 0x00160089 File Offset: 0x0015E289
		public bool IsValid
		{
			get
			{
				return this._spatialProvider.GetIsValid(this);
			}
		}

		// Token: 0x060049DC RID: 18908 RVA: 0x00160097 File Offset: 0x0015E297
		public virtual string AsText()
		{
			return this._spatialProvider.AsText(this);
		}

		// Token: 0x060049DD RID: 18909 RVA: 0x001600A5 File Offset: 0x0015E2A5
		internal string AsTextIncludingElevationAndMeasure()
		{
			return this._spatialProvider.AsTextIncludingElevationAndMeasure(this);
		}

		// Token: 0x060049DE RID: 18910 RVA: 0x001600B3 File Offset: 0x0015E2B3
		public byte[] AsBinary()
		{
			return this._spatialProvider.AsBinary(this);
		}

		// Token: 0x060049DF RID: 18911 RVA: 0x001600C1 File Offset: 0x0015E2C1
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Gml")]
		public string AsGml()
		{
			return this._spatialProvider.AsGml(this);
		}

		// Token: 0x060049E0 RID: 18912 RVA: 0x001600CF File Offset: 0x0015E2CF
		public bool SpatialEquals(DbGeometry other)
		{
			Check.NotNull<DbGeometry>(other, "other");
			return this._spatialProvider.SpatialEquals(this, other);
		}

		// Token: 0x060049E1 RID: 18913 RVA: 0x001600EA File Offset: 0x0015E2EA
		public bool Disjoint(DbGeometry other)
		{
			Check.NotNull<DbGeometry>(other, "other");
			return this._spatialProvider.Disjoint(this, other);
		}

		// Token: 0x060049E2 RID: 18914 RVA: 0x00160105 File Offset: 0x0015E305
		public bool Intersects(DbGeometry other)
		{
			Check.NotNull<DbGeometry>(other, "other");
			return this._spatialProvider.Intersects(this, other);
		}

		// Token: 0x060049E3 RID: 18915 RVA: 0x00160120 File Offset: 0x0015E320
		public bool Touches(DbGeometry other)
		{
			Check.NotNull<DbGeometry>(other, "other");
			return this._spatialProvider.Touches(this, other);
		}

		// Token: 0x060049E4 RID: 18916 RVA: 0x0016013B File Offset: 0x0015E33B
		public bool Crosses(DbGeometry other)
		{
			Check.NotNull<DbGeometry>(other, "other");
			return this._spatialProvider.Crosses(this, other);
		}

		// Token: 0x060049E5 RID: 18917 RVA: 0x00160156 File Offset: 0x0015E356
		public bool Within(DbGeometry other)
		{
			Check.NotNull<DbGeometry>(other, "other");
			return this._spatialProvider.Within(this, other);
		}

		// Token: 0x060049E6 RID: 18918 RVA: 0x00160171 File Offset: 0x0015E371
		public bool Contains(DbGeometry other)
		{
			Check.NotNull<DbGeometry>(other, "other");
			return this._spatialProvider.Contains(this, other);
		}

		// Token: 0x060049E7 RID: 18919 RVA: 0x0016018C File Offset: 0x0015E38C
		public bool Overlaps(DbGeometry other)
		{
			Check.NotNull<DbGeometry>(other, "other");
			return this._spatialProvider.Overlaps(this, other);
		}

		// Token: 0x060049E8 RID: 18920 RVA: 0x001601A7 File Offset: 0x0015E3A7
		public bool Relate(DbGeometry other, string matrix)
		{
			Check.NotNull<DbGeometry>(other, "other");
			Check.NotNull<string>(matrix, "matrix");
			return this._spatialProvider.Relate(this, other, matrix);
		}

		// Token: 0x060049E9 RID: 18921 RVA: 0x001601CF File Offset: 0x0015E3CF
		public DbGeometry Buffer(double? distance)
		{
			Check.NotNull<double>(distance, "distance");
			return this._spatialProvider.Buffer(this, distance.Value);
		}

		// Token: 0x060049EA RID: 18922 RVA: 0x001601F0 File Offset: 0x0015E3F0
		public double? Distance(DbGeometry other)
		{
			Check.NotNull<DbGeometry>(other, "other");
			return new double?(this._spatialProvider.Distance(this, other));
		}

		// Token: 0x17000B32 RID: 2866
		// (get) Token: 0x060049EB RID: 18923 RVA: 0x00160210 File Offset: 0x0015E410
		public DbGeometry ConvexHull
		{
			get
			{
				return this._spatialProvider.GetConvexHull(this);
			}
		}

		// Token: 0x060049EC RID: 18924 RVA: 0x0016021E File Offset: 0x0015E41E
		public DbGeometry Intersection(DbGeometry other)
		{
			Check.NotNull<DbGeometry>(other, "other");
			return this._spatialProvider.Intersection(this, other);
		}

		// Token: 0x060049ED RID: 18925 RVA: 0x00160239 File Offset: 0x0015E439
		public DbGeometry Union(DbGeometry other)
		{
			Check.NotNull<DbGeometry>(other, "other");
			return this._spatialProvider.Union(this, other);
		}

		// Token: 0x060049EE RID: 18926 RVA: 0x00160254 File Offset: 0x0015E454
		public DbGeometry Difference(DbGeometry other)
		{
			Check.NotNull<DbGeometry>(other, "other");
			return this._spatialProvider.Difference(this, other);
		}

		// Token: 0x060049EF RID: 18927 RVA: 0x0016026F File Offset: 0x0015E46F
		public DbGeometry SymmetricDifference(DbGeometry other)
		{
			Check.NotNull<DbGeometry>(other, "other");
			return this._spatialProvider.SymmetricDifference(this, other);
		}

		// Token: 0x17000B33 RID: 2867
		// (get) Token: 0x060049F0 RID: 18928 RVA: 0x0016028A File Offset: 0x0015E48A
		public int? ElementCount
		{
			get
			{
				return this._spatialProvider.GetElementCount(this);
			}
		}

		// Token: 0x060049F1 RID: 18929 RVA: 0x00160298 File Offset: 0x0015E498
		public DbGeometry ElementAt(int index)
		{
			return this._spatialProvider.ElementAt(this, index);
		}

		// Token: 0x17000B34 RID: 2868
		// (get) Token: 0x060049F2 RID: 18930 RVA: 0x001602A7 File Offset: 0x0015E4A7
		public double? XCoordinate
		{
			get
			{
				return this._spatialProvider.GetXCoordinate(this);
			}
		}

		// Token: 0x17000B35 RID: 2869
		// (get) Token: 0x060049F3 RID: 18931 RVA: 0x001602B5 File Offset: 0x0015E4B5
		public double? YCoordinate
		{
			get
			{
				return this._spatialProvider.GetYCoordinate(this);
			}
		}

		// Token: 0x17000B36 RID: 2870
		// (get) Token: 0x060049F4 RID: 18932 RVA: 0x001602C3 File Offset: 0x0015E4C3
		public double? Elevation
		{
			get
			{
				return this._spatialProvider.GetElevation(this);
			}
		}

		// Token: 0x17000B37 RID: 2871
		// (get) Token: 0x060049F5 RID: 18933 RVA: 0x001602D1 File Offset: 0x0015E4D1
		public double? Measure
		{
			get
			{
				return this._spatialProvider.GetMeasure(this);
			}
		}

		// Token: 0x17000B38 RID: 2872
		// (get) Token: 0x060049F6 RID: 18934 RVA: 0x001602DF File Offset: 0x0015E4DF
		public double? Length
		{
			get
			{
				return this._spatialProvider.GetLength(this);
			}
		}

		// Token: 0x17000B39 RID: 2873
		// (get) Token: 0x060049F7 RID: 18935 RVA: 0x001602ED File Offset: 0x0015E4ED
		public DbGeometry StartPoint
		{
			get
			{
				return this._spatialProvider.GetStartPoint(this);
			}
		}

		// Token: 0x17000B3A RID: 2874
		// (get) Token: 0x060049F8 RID: 18936 RVA: 0x001602FB File Offset: 0x0015E4FB
		public DbGeometry EndPoint
		{
			get
			{
				return this._spatialProvider.GetEndPoint(this);
			}
		}

		// Token: 0x17000B3B RID: 2875
		// (get) Token: 0x060049F9 RID: 18937 RVA: 0x00160309 File Offset: 0x0015E509
		public bool? IsClosed
		{
			get
			{
				return this._spatialProvider.GetIsClosed(this);
			}
		}

		// Token: 0x17000B3C RID: 2876
		// (get) Token: 0x060049FA RID: 18938 RVA: 0x00160317 File Offset: 0x0015E517
		public bool? IsRing
		{
			get
			{
				return this._spatialProvider.GetIsRing(this);
			}
		}

		// Token: 0x17000B3D RID: 2877
		// (get) Token: 0x060049FB RID: 18939 RVA: 0x00160325 File Offset: 0x0015E525
		public int? PointCount
		{
			get
			{
				return this._spatialProvider.GetPointCount(this);
			}
		}

		// Token: 0x060049FC RID: 18940 RVA: 0x00160333 File Offset: 0x0015E533
		public DbGeometry PointAt(int index)
		{
			return this._spatialProvider.PointAt(this, index);
		}

		// Token: 0x17000B3E RID: 2878
		// (get) Token: 0x060049FD RID: 18941 RVA: 0x00160342 File Offset: 0x0015E542
		public double? Area
		{
			get
			{
				return this._spatialProvider.GetArea(this);
			}
		}

		// Token: 0x17000B3F RID: 2879
		// (get) Token: 0x060049FE RID: 18942 RVA: 0x00160350 File Offset: 0x0015E550
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Centroid", Justification = "Naming convention prescribed by OGC specification")]
		public DbGeometry Centroid
		{
			get
			{
				return this._spatialProvider.GetCentroid(this);
			}
		}

		// Token: 0x17000B40 RID: 2880
		// (get) Token: 0x060049FF RID: 18943 RVA: 0x0016035E File Offset: 0x0015E55E
		public DbGeometry PointOnSurface
		{
			get
			{
				return this._spatialProvider.GetPointOnSurface(this);
			}
		}

		// Token: 0x17000B41 RID: 2881
		// (get) Token: 0x06004A00 RID: 18944 RVA: 0x0016036C File Offset: 0x0015E56C
		public DbGeometry ExteriorRing
		{
			get
			{
				return this._spatialProvider.GetExteriorRing(this);
			}
		}

		// Token: 0x17000B42 RID: 2882
		// (get) Token: 0x06004A01 RID: 18945 RVA: 0x0016037A File Offset: 0x0015E57A
		public int? InteriorRingCount
		{
			get
			{
				return this._spatialProvider.GetInteriorRingCount(this);
			}
		}

		// Token: 0x06004A02 RID: 18946 RVA: 0x00160388 File Offset: 0x0015E588
		public DbGeometry InteriorRingAt(int index)
		{
			return this._spatialProvider.InteriorRingAt(this, index);
		}

		// Token: 0x06004A03 RID: 18947 RVA: 0x00160398 File Offset: 0x0015E598
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "SRID={1};{0}", new object[]
			{
				this.WellKnownValue.WellKnownText ?? base.ToString(),
				this.CoordinateSystemId
			});
		}

		// Token: 0x04001B53 RID: 6995
		private DbSpatialServices _spatialProvider;

		// Token: 0x04001B54 RID: 6996
		private object _providerValue;
	}
}
