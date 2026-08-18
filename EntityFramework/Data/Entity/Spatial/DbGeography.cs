using System;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.Serialization;

namespace System.Data.Entity.Spatial
{
	// Token: 0x02000719 RID: 1817
	[DataContract]
	[Serializable]
	public class DbGeography
	{
		// Token: 0x06004978 RID: 18808 RVA: 0x0015F878 File Offset: 0x0015DA78
		internal DbGeography()
		{
		}

		// Token: 0x06004979 RID: 18809 RVA: 0x0015F880 File Offset: 0x0015DA80
		internal DbGeography(DbSpatialServices spatialServices, object spatialProviderValue)
		{
			this._spatialProvider = spatialServices;
			this._providerValue = spatialProviderValue;
		}

		// Token: 0x17000B10 RID: 2832
		// (get) Token: 0x0600497A RID: 18810 RVA: 0x0015F896 File Offset: 0x0015DA96
		public static int DefaultCoordinateSystemId
		{
			get
			{
				return 4326;
			}
		}

		// Token: 0x17000B11 RID: 2833
		// (get) Token: 0x0600497B RID: 18811 RVA: 0x0015F89D File Offset: 0x0015DA9D
		public object ProviderValue
		{
			get
			{
				return this._providerValue;
			}
		}

		// Token: 0x17000B12 RID: 2834
		// (get) Token: 0x0600497C RID: 18812 RVA: 0x0015F8A5 File Offset: 0x0015DAA5
		public virtual DbSpatialServices Provider
		{
			get
			{
				return this._spatialProvider;
			}
		}

		// Token: 0x17000B13 RID: 2835
		// (get) Token: 0x0600497D RID: 18813 RVA: 0x0015F8AD File Offset: 0x0015DAAD
		// (set) Token: 0x0600497E RID: 18814 RVA: 0x0015F8BC File Offset: 0x0015DABC
		[DataMember(Name = "Geography")]
		public DbGeographyWellKnownValue WellKnownValue
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

		// Token: 0x0600497F RID: 18815 RVA: 0x0015F8F6 File Offset: 0x0015DAF6
		public static DbGeography FromBinary(byte[] wellKnownBinary)
		{
			Check.NotNull<byte[]>(wellKnownBinary, "wellKnownBinary");
			return DbSpatialServices.Default.GeographyFromBinary(wellKnownBinary);
		}

		// Token: 0x06004980 RID: 18816 RVA: 0x0015F90F File Offset: 0x0015DB0F
		public static DbGeography FromBinary(byte[] wellKnownBinary, int coordinateSystemId)
		{
			Check.NotNull<byte[]>(wellKnownBinary, "wellKnownBinary");
			return DbSpatialServices.Default.GeographyFromBinary(wellKnownBinary, coordinateSystemId);
		}

		// Token: 0x06004981 RID: 18817 RVA: 0x0015F929 File Offset: 0x0015DB29
		public static DbGeography LineFromBinary(byte[] lineWellKnownBinary, int coordinateSystemId)
		{
			Check.NotNull<byte[]>(lineWellKnownBinary, "lineWellKnownBinary");
			return DbSpatialServices.Default.GeographyLineFromBinary(lineWellKnownBinary, coordinateSystemId);
		}

		// Token: 0x06004982 RID: 18818 RVA: 0x0015F943 File Offset: 0x0015DB43
		public static DbGeography PointFromBinary(byte[] pointWellKnownBinary, int coordinateSystemId)
		{
			Check.NotNull<byte[]>(pointWellKnownBinary, "pointWellKnownBinary");
			return DbSpatialServices.Default.GeographyPointFromBinary(pointWellKnownBinary, coordinateSystemId);
		}

		// Token: 0x06004983 RID: 18819 RVA: 0x0015F95D File Offset: 0x0015DB5D
		public static DbGeography PolygonFromBinary(byte[] polygonWellKnownBinary, int coordinateSystemId)
		{
			Check.NotNull<byte[]>(polygonWellKnownBinary, "polygonWellKnownBinary");
			return DbSpatialServices.Default.GeographyPolygonFromBinary(polygonWellKnownBinary, coordinateSystemId);
		}

		// Token: 0x06004984 RID: 18820 RVA: 0x0015F977 File Offset: 0x0015DB77
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "multi", Justification = "Match OGC, EDM")]
		[SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "MultiLine", Justification = "Match OGC, EDM")]
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Multi", Justification = "Match OGC, EDM")]
		[SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "multiLine", Justification = "Match OGC, EDM")]
		public static DbGeography MultiLineFromBinary(byte[] multiLineWellKnownBinary, int coordinateSystemId)
		{
			Check.NotNull<byte[]>(multiLineWellKnownBinary, "multiLineWellKnownBinary");
			return DbSpatialServices.Default.GeographyMultiLineFromBinary(multiLineWellKnownBinary, coordinateSystemId);
		}

		// Token: 0x06004985 RID: 18821 RVA: 0x0015F991 File Offset: 0x0015DB91
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "multi", Justification = "Match OGC, EDM")]
		[SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "MultiPoint", Justification = "Match OGC, EDM")]
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Multi", Justification = "Match OGC, EDM")]
		[SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "multiPoint", Justification = "Match OGC, EDM")]
		public static DbGeography MultiPointFromBinary(byte[] multiPointWellKnownBinary, int coordinateSystemId)
		{
			Check.NotNull<byte[]>(multiPointWellKnownBinary, "multiPointWellKnownBinary");
			return DbSpatialServices.Default.GeographyMultiPointFromBinary(multiPointWellKnownBinary, coordinateSystemId);
		}

		// Token: 0x06004986 RID: 18822 RVA: 0x0015F9AB File Offset: 0x0015DBAB
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Multi", Justification = "Match OGC, EDM")]
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "multi", Justification = "Match OGC, EDM")]
		public static DbGeography MultiPolygonFromBinary(byte[] multiPolygonWellKnownBinary, int coordinateSystemId)
		{
			Check.NotNull<byte[]>(multiPolygonWellKnownBinary, "multiPolygonWellKnownBinary");
			return DbSpatialServices.Default.GeographyMultiPolygonFromBinary(multiPolygonWellKnownBinary, coordinateSystemId);
		}

		// Token: 0x06004987 RID: 18823 RVA: 0x0015F9C5 File Offset: 0x0015DBC5
		public static DbGeography GeographyCollectionFromBinary(byte[] geographyCollectionWellKnownBinary, int coordinateSystemId)
		{
			Check.NotNull<byte[]>(geographyCollectionWellKnownBinary, "geographyCollectionWellKnownBinary");
			return DbSpatialServices.Default.GeographyCollectionFromBinary(geographyCollectionWellKnownBinary, coordinateSystemId);
		}

		// Token: 0x06004988 RID: 18824 RVA: 0x0015F9DF File Offset: 0x0015DBDF
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Gml")]
		public static DbGeography FromGml(string geographyMarkup)
		{
			Check.NotNull<string>(geographyMarkup, "geographyMarkup");
			return DbSpatialServices.Default.GeographyFromGml(geographyMarkup);
		}

		// Token: 0x06004989 RID: 18825 RVA: 0x0015F9F8 File Offset: 0x0015DBF8
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Gml")]
		public static DbGeography FromGml(string geographyMarkup, int coordinateSystemId)
		{
			Check.NotNull<string>(geographyMarkup, "geographyMarkup");
			return DbSpatialServices.Default.GeographyFromGml(geographyMarkup, coordinateSystemId);
		}

		// Token: 0x0600498A RID: 18826 RVA: 0x0015FA12 File Offset: 0x0015DC12
		public static DbGeography FromText(string wellKnownText)
		{
			Check.NotNull<string>(wellKnownText, "wellKnownText");
			return DbSpatialServices.Default.GeographyFromText(wellKnownText);
		}

		// Token: 0x0600498B RID: 18827 RVA: 0x0015FA2B File Offset: 0x0015DC2B
		public static DbGeography FromText(string wellKnownText, int coordinateSystemId)
		{
			Check.NotNull<string>(wellKnownText, "wellKnownText");
			return DbSpatialServices.Default.GeographyFromText(wellKnownText, coordinateSystemId);
		}

		// Token: 0x0600498C RID: 18828 RVA: 0x0015FA45 File Offset: 0x0015DC45
		public static DbGeography LineFromText(string lineWellKnownText, int coordinateSystemId)
		{
			Check.NotNull<string>(lineWellKnownText, "lineWellKnownText");
			return DbSpatialServices.Default.GeographyLineFromText(lineWellKnownText, coordinateSystemId);
		}

		// Token: 0x0600498D RID: 18829 RVA: 0x0015FA5F File Offset: 0x0015DC5F
		public static DbGeography PointFromText(string pointWellKnownText, int coordinateSystemId)
		{
			Check.NotNull<string>(pointWellKnownText, "pointWellKnownText");
			return DbSpatialServices.Default.GeographyPointFromText(pointWellKnownText, coordinateSystemId);
		}

		// Token: 0x0600498E RID: 18830 RVA: 0x0015FA79 File Offset: 0x0015DC79
		public static DbGeography PolygonFromText(string polygonWellKnownText, int coordinateSystemId)
		{
			Check.NotNull<string>(polygonWellKnownText, "polygonWellKnownText");
			return DbSpatialServices.Default.GeographyPolygonFromText(polygonWellKnownText, coordinateSystemId);
		}

		// Token: 0x0600498F RID: 18831 RVA: 0x0015FA93 File Offset: 0x0015DC93
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Multi", Justification = "Match OGC, EDM")]
		[SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "MultiLine", Justification = "Match OGC, EDM")]
		[SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "multiLine", Justification = "Match OGC, EDM")]
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "multi", Justification = "Match OGC, EDM")]
		public static DbGeography MultiLineFromText(string multiLineWellKnownText, int coordinateSystemId)
		{
			Check.NotNull<string>(multiLineWellKnownText, "multiLineWellKnownText");
			return DbSpatialServices.Default.GeographyMultiLineFromText(multiLineWellKnownText, coordinateSystemId);
		}

		// Token: 0x06004990 RID: 18832 RVA: 0x0015FAAD File Offset: 0x0015DCAD
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Multi", Justification = "Match OGC, EDM")]
		[SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "MultiPoint", Justification = "Match OGC, EDM")]
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "multi", Justification = "Match OGC, EDM")]
		[SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "multiPoint", Justification = "Match OGC, EDM")]
		public static DbGeography MultiPointFromText(string multiPointWellKnownText, int coordinateSystemId)
		{
			Check.NotNull<string>(multiPointWellKnownText, "multiPointWellKnownText");
			return DbSpatialServices.Default.GeographyMultiPointFromText(multiPointWellKnownText, coordinateSystemId);
		}

		// Token: 0x06004991 RID: 18833 RVA: 0x0015FAC7 File Offset: 0x0015DCC7
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Multi", Justification = "Match OGC, EDM")]
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "multi", Justification = "Match OGC, EDM")]
		public static DbGeography MultiPolygonFromText(string multiPolygonWellKnownText, int coordinateSystemId)
		{
			Check.NotNull<string>(multiPolygonWellKnownText, "multiPolygonWellKnownText");
			return DbSpatialServices.Default.GeographyMultiPolygonFromText(multiPolygonWellKnownText, coordinateSystemId);
		}

		// Token: 0x06004992 RID: 18834 RVA: 0x0015FAE1 File Offset: 0x0015DCE1
		public static DbGeography GeographyCollectionFromText(string geographyCollectionWellKnownText, int coordinateSystemId)
		{
			Check.NotNull<string>(geographyCollectionWellKnownText, "geographyCollectionWellKnownText");
			return DbSpatialServices.Default.GeographyCollectionFromText(geographyCollectionWellKnownText, coordinateSystemId);
		}

		// Token: 0x17000B14 RID: 2836
		// (get) Token: 0x06004993 RID: 18835 RVA: 0x0015FAFB File Offset: 0x0015DCFB
		public int CoordinateSystemId
		{
			get
			{
				return this._spatialProvider.GetCoordinateSystemId(this);
			}
		}

		// Token: 0x17000B15 RID: 2837
		// (get) Token: 0x06004994 RID: 18836 RVA: 0x0015FB09 File Offset: 0x0015DD09
		public int Dimension
		{
			get
			{
				return this._spatialProvider.GetDimension(this);
			}
		}

		// Token: 0x17000B16 RID: 2838
		// (get) Token: 0x06004995 RID: 18837 RVA: 0x0015FB17 File Offset: 0x0015DD17
		public string SpatialTypeName
		{
			get
			{
				return this._spatialProvider.GetSpatialTypeName(this);
			}
		}

		// Token: 0x17000B17 RID: 2839
		// (get) Token: 0x06004996 RID: 18838 RVA: 0x0015FB25 File Offset: 0x0015DD25
		public bool IsEmpty
		{
			get
			{
				return this._spatialProvider.GetIsEmpty(this);
			}
		}

		// Token: 0x06004997 RID: 18839 RVA: 0x0015FB33 File Offset: 0x0015DD33
		public virtual string AsText()
		{
			return this._spatialProvider.AsText(this);
		}

		// Token: 0x06004998 RID: 18840 RVA: 0x0015FB41 File Offset: 0x0015DD41
		internal string AsTextIncludingElevationAndMeasure()
		{
			return this._spatialProvider.AsTextIncludingElevationAndMeasure(this);
		}

		// Token: 0x06004999 RID: 18841 RVA: 0x0015FB4F File Offset: 0x0015DD4F
		public byte[] AsBinary()
		{
			return this._spatialProvider.AsBinary(this);
		}

		// Token: 0x0600499A RID: 18842 RVA: 0x0015FB5D File Offset: 0x0015DD5D
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Gml")]
		public string AsGml()
		{
			return this._spatialProvider.AsGml(this);
		}

		// Token: 0x0600499B RID: 18843 RVA: 0x0015FB6B File Offset: 0x0015DD6B
		public bool SpatialEquals(DbGeography other)
		{
			Check.NotNull<DbGeography>(other, "other");
			return this._spatialProvider.SpatialEquals(this, other);
		}

		// Token: 0x0600499C RID: 18844 RVA: 0x0015FB86 File Offset: 0x0015DD86
		public bool Disjoint(DbGeography other)
		{
			Check.NotNull<DbGeography>(other, "other");
			return this._spatialProvider.Disjoint(this, other);
		}

		// Token: 0x0600499D RID: 18845 RVA: 0x0015FBA1 File Offset: 0x0015DDA1
		public bool Intersects(DbGeography other)
		{
			Check.NotNull<DbGeography>(other, "other");
			return this._spatialProvider.Intersects(this, other);
		}

		// Token: 0x0600499E RID: 18846 RVA: 0x0015FBBC File Offset: 0x0015DDBC
		public DbGeography Buffer(double? distance)
		{
			Check.NotNull<double>(distance, "distance");
			return this._spatialProvider.Buffer(this, distance.Value);
		}

		// Token: 0x0600499F RID: 18847 RVA: 0x0015FBDD File Offset: 0x0015DDDD
		public double? Distance(DbGeography other)
		{
			Check.NotNull<DbGeography>(other, "other");
			return new double?(this._spatialProvider.Distance(this, other));
		}

		// Token: 0x060049A0 RID: 18848 RVA: 0x0015FBFD File Offset: 0x0015DDFD
		public DbGeography Intersection(DbGeography other)
		{
			Check.NotNull<DbGeography>(other, "other");
			return this._spatialProvider.Intersection(this, other);
		}

		// Token: 0x060049A1 RID: 18849 RVA: 0x0015FC18 File Offset: 0x0015DE18
		public DbGeography Union(DbGeography other)
		{
			Check.NotNull<DbGeography>(other, "other");
			return this._spatialProvider.Union(this, other);
		}

		// Token: 0x060049A2 RID: 18850 RVA: 0x0015FC33 File Offset: 0x0015DE33
		public DbGeography Difference(DbGeography other)
		{
			Check.NotNull<DbGeography>(other, "other");
			return this._spatialProvider.Difference(this, other);
		}

		// Token: 0x060049A3 RID: 18851 RVA: 0x0015FC4E File Offset: 0x0015DE4E
		public DbGeography SymmetricDifference(DbGeography other)
		{
			Check.NotNull<DbGeography>(other, "other");
			return this._spatialProvider.SymmetricDifference(this, other);
		}

		// Token: 0x17000B18 RID: 2840
		// (get) Token: 0x060049A4 RID: 18852 RVA: 0x0015FC69 File Offset: 0x0015DE69
		public int? ElementCount
		{
			get
			{
				return this._spatialProvider.GetElementCount(this);
			}
		}

		// Token: 0x060049A5 RID: 18853 RVA: 0x0015FC77 File Offset: 0x0015DE77
		public DbGeography ElementAt(int index)
		{
			return this._spatialProvider.ElementAt(this, index);
		}

		// Token: 0x17000B19 RID: 2841
		// (get) Token: 0x060049A6 RID: 18854 RVA: 0x0015FC86 File Offset: 0x0015DE86
		public double? Latitude
		{
			get
			{
				return this._spatialProvider.GetLatitude(this);
			}
		}

		// Token: 0x17000B1A RID: 2842
		// (get) Token: 0x060049A7 RID: 18855 RVA: 0x0015FC94 File Offset: 0x0015DE94
		public double? Longitude
		{
			get
			{
				return this._spatialProvider.GetLongitude(this);
			}
		}

		// Token: 0x17000B1B RID: 2843
		// (get) Token: 0x060049A8 RID: 18856 RVA: 0x0015FCA2 File Offset: 0x0015DEA2
		public double? Elevation
		{
			get
			{
				return this._spatialProvider.GetElevation(this);
			}
		}

		// Token: 0x17000B1C RID: 2844
		// (get) Token: 0x060049A9 RID: 18857 RVA: 0x0015FCB0 File Offset: 0x0015DEB0
		public double? Measure
		{
			get
			{
				return this._spatialProvider.GetMeasure(this);
			}
		}

		// Token: 0x17000B1D RID: 2845
		// (get) Token: 0x060049AA RID: 18858 RVA: 0x0015FCBE File Offset: 0x0015DEBE
		public double? Length
		{
			get
			{
				return this._spatialProvider.GetLength(this);
			}
		}

		// Token: 0x17000B1E RID: 2846
		// (get) Token: 0x060049AB RID: 18859 RVA: 0x0015FCCC File Offset: 0x0015DECC
		public DbGeography StartPoint
		{
			get
			{
				return this._spatialProvider.GetStartPoint(this);
			}
		}

		// Token: 0x17000B1F RID: 2847
		// (get) Token: 0x060049AC RID: 18860 RVA: 0x0015FCDA File Offset: 0x0015DEDA
		public DbGeography EndPoint
		{
			get
			{
				return this._spatialProvider.GetEndPoint(this);
			}
		}

		// Token: 0x17000B20 RID: 2848
		// (get) Token: 0x060049AD RID: 18861 RVA: 0x0015FCE8 File Offset: 0x0015DEE8
		public bool? IsClosed
		{
			get
			{
				return this._spatialProvider.GetIsClosed(this);
			}
		}

		// Token: 0x17000B21 RID: 2849
		// (get) Token: 0x060049AE RID: 18862 RVA: 0x0015FCF6 File Offset: 0x0015DEF6
		public int? PointCount
		{
			get
			{
				return this._spatialProvider.GetPointCount(this);
			}
		}

		// Token: 0x060049AF RID: 18863 RVA: 0x0015FD04 File Offset: 0x0015DF04
		public DbGeography PointAt(int index)
		{
			return this._spatialProvider.PointAt(this, index);
		}

		// Token: 0x17000B22 RID: 2850
		// (get) Token: 0x060049B0 RID: 18864 RVA: 0x0015FD13 File Offset: 0x0015DF13
		public double? Area
		{
			get
			{
				return this._spatialProvider.GetArea(this);
			}
		}

		// Token: 0x060049B1 RID: 18865 RVA: 0x0015FD24 File Offset: 0x0015DF24
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "SRID={1};{0}", new object[]
			{
				this.WellKnownValue.WellKnownText ?? base.ToString(),
				this.CoordinateSystemId
			});
		}

		// Token: 0x04001B4E RID: 6990
		private DbSpatialServices _spatialProvider;

		// Token: 0x04001B4F RID: 6991
		private object _providerValue;
	}
}
