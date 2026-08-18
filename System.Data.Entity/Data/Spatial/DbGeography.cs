using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Spatial.Internal;
using System.Globalization;
using System.Runtime.Serialization;

namespace System.Data.Spatial
{
	// Token: 0x020002D6 RID: 726
	[DataContract]
	[BindableType]
	[Serializable]
	public class DbGeography
	{
		// Token: 0x06002AB1 RID: 10929 RVA: 0x000A74EB File Offset: 0x000A56EB
		internal DbGeography(DbSpatialServices spatialServices, object spatialProviderValue)
		{
			this.spatialSvcs = spatialServices;
			this.providerValue = spatialProviderValue;
		}

		// Token: 0x1700084F RID: 2127
		// (get) Token: 0x06002AB2 RID: 10930 RVA: 0x000A7501 File Offset: 0x000A5701
		public static int DefaultCoordinateSystemId
		{
			get
			{
				return 4326;
			}
		}

		// Token: 0x17000850 RID: 2128
		// (get) Token: 0x06002AB3 RID: 10931 RVA: 0x000A7508 File Offset: 0x000A5708
		public object ProviderValue
		{
			get
			{
				return this.providerValue;
			}
		}

		// Token: 0x17000851 RID: 2129
		// (get) Token: 0x06002AB4 RID: 10932 RVA: 0x000A7510 File Offset: 0x000A5710
		// (set) Token: 0x06002AB5 RID: 10933 RVA: 0x000A7520 File Offset: 0x000A5720
		[DataMember(Name = "Geography")]
		public DbGeographyWellKnownValue WellKnownValue
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

		// Token: 0x06002AB6 RID: 10934 RVA: 0x000A7555 File Offset: 0x000A5755
		public static DbGeography FromBinary(byte[] wellKnownBinary)
		{
			wellKnownBinary.CheckNull("wellKnownBinary");
			return DbSpatialServices.Default.GeographyFromBinary(wellKnownBinary);
		}

		// Token: 0x06002AB7 RID: 10935 RVA: 0x000A756D File Offset: 0x000A576D
		public static DbGeography FromBinary(byte[] wellKnownBinary, int coordinateSystemId)
		{
			wellKnownBinary.CheckNull("wellKnownBinary");
			return DbSpatialServices.Default.GeographyFromBinary(wellKnownBinary, coordinateSystemId);
		}

		// Token: 0x06002AB8 RID: 10936 RVA: 0x000A7586 File Offset: 0x000A5786
		public static DbGeography LineFromBinary(byte[] lineWellKnownBinary, int coordinateSystemId)
		{
			lineWellKnownBinary.CheckNull("lineWellKnownBinary");
			return DbSpatialServices.Default.GeographyLineFromBinary(lineWellKnownBinary, coordinateSystemId);
		}

		// Token: 0x06002AB9 RID: 10937 RVA: 0x000A759F File Offset: 0x000A579F
		public static DbGeography PointFromBinary(byte[] pointWellKnownBinary, int coordinateSystemId)
		{
			pointWellKnownBinary.CheckNull("pointWellKnownBinary");
			return DbSpatialServices.Default.GeographyPointFromBinary(pointWellKnownBinary, coordinateSystemId);
		}

		// Token: 0x06002ABA RID: 10938 RVA: 0x000A75B8 File Offset: 0x000A57B8
		public static DbGeography PolygonFromBinary(byte[] polygonWellKnownBinary, int coordinateSystemId)
		{
			polygonWellKnownBinary.CheckNull("polygonWellKnownBinary");
			return DbSpatialServices.Default.GeographyPolygonFromBinary(polygonWellKnownBinary, coordinateSystemId);
		}

		// Token: 0x06002ABB RID: 10939 RVA: 0x000A75D1 File Offset: 0x000A57D1
		public static DbGeography MultiLineFromBinary(byte[] multiLineWellKnownBinary, int coordinateSystemId)
		{
			multiLineWellKnownBinary.CheckNull("multiLineWellKnownBinary");
			return DbSpatialServices.Default.GeographyMultiLineFromBinary(multiLineWellKnownBinary, coordinateSystemId);
		}

		// Token: 0x06002ABC RID: 10940 RVA: 0x000A75EA File Offset: 0x000A57EA
		public static DbGeography MultiPointFromBinary(byte[] multiPointWellKnownBinary, int coordinateSystemId)
		{
			multiPointWellKnownBinary.CheckNull("multiPointWellKnownBinary");
			return DbSpatialServices.Default.GeographyMultiPointFromBinary(multiPointWellKnownBinary, coordinateSystemId);
		}

		// Token: 0x06002ABD RID: 10941 RVA: 0x000A7603 File Offset: 0x000A5803
		public static DbGeography MultiPolygonFromBinary(byte[] multiPolygonWellKnownBinary, int coordinateSystemId)
		{
			multiPolygonWellKnownBinary.CheckNull("multiPolygonWellKnownBinary");
			return DbSpatialServices.Default.GeographyMultiPolygonFromBinary(multiPolygonWellKnownBinary, coordinateSystemId);
		}

		// Token: 0x06002ABE RID: 10942 RVA: 0x000A761C File Offset: 0x000A581C
		public static DbGeography GeographyCollectionFromBinary(byte[] geographyCollectionWellKnownBinary, int coordinateSystemId)
		{
			geographyCollectionWellKnownBinary.CheckNull("geographyCollectionWellKnownBinary");
			return DbSpatialServices.Default.GeographyCollectionFromBinary(geographyCollectionWellKnownBinary, coordinateSystemId);
		}

		// Token: 0x06002ABF RID: 10943 RVA: 0x000A7635 File Offset: 0x000A5835
		public static DbGeography FromGml(string geographyMarkup)
		{
			geographyMarkup.CheckNull("geographyMarkup");
			return DbSpatialServices.Default.GeographyFromGml(geographyMarkup);
		}

		// Token: 0x06002AC0 RID: 10944 RVA: 0x000A764D File Offset: 0x000A584D
		public static DbGeography FromGml(string geographyMarkup, int coordinateSystemId)
		{
			geographyMarkup.CheckNull("geographyMarkup");
			return DbSpatialServices.Default.GeographyFromGml(geographyMarkup, coordinateSystemId);
		}

		// Token: 0x06002AC1 RID: 10945 RVA: 0x000A7666 File Offset: 0x000A5866
		public static DbGeography FromText(string wellKnownText)
		{
			wellKnownText.CheckNull("wellKnownText");
			return DbSpatialServices.Default.GeographyFromText(wellKnownText);
		}

		// Token: 0x06002AC2 RID: 10946 RVA: 0x000A767E File Offset: 0x000A587E
		public static DbGeography FromText(string wellKnownText, int coordinateSystemId)
		{
			wellKnownText.CheckNull("wellKnownText");
			return DbSpatialServices.Default.GeographyFromText(wellKnownText, coordinateSystemId);
		}

		// Token: 0x06002AC3 RID: 10947 RVA: 0x000A7697 File Offset: 0x000A5897
		public static DbGeography LineFromText(string lineWellKnownText, int coordinateSystemId)
		{
			lineWellKnownText.CheckNull("lineWellKnownText");
			return DbSpatialServices.Default.GeographyLineFromText(lineWellKnownText, coordinateSystemId);
		}

		// Token: 0x06002AC4 RID: 10948 RVA: 0x000A76B0 File Offset: 0x000A58B0
		public static DbGeography PointFromText(string pointWellKnownText, int coordinateSystemId)
		{
			pointWellKnownText.CheckNull("pointWellKnownText");
			return DbSpatialServices.Default.GeographyPointFromText(pointWellKnownText, coordinateSystemId);
		}

		// Token: 0x06002AC5 RID: 10949 RVA: 0x000A76C9 File Offset: 0x000A58C9
		public static DbGeography PolygonFromText(string polygonWellKnownText, int coordinateSystemId)
		{
			polygonWellKnownText.CheckNull("polygonWellKnownText");
			return DbSpatialServices.Default.GeographyPolygonFromText(polygonWellKnownText, coordinateSystemId);
		}

		// Token: 0x06002AC6 RID: 10950 RVA: 0x000A76E2 File Offset: 0x000A58E2
		public static DbGeography MultiLineFromText(string multiLineWellKnownText, int coordinateSystemId)
		{
			multiLineWellKnownText.CheckNull("multiLineWellKnownText");
			return DbSpatialServices.Default.GeographyMultiLineFromText(multiLineWellKnownText, coordinateSystemId);
		}

		// Token: 0x06002AC7 RID: 10951 RVA: 0x000A76FB File Offset: 0x000A58FB
		public static DbGeography MultiPointFromText(string multiPointWellKnownText, int coordinateSystemId)
		{
			multiPointWellKnownText.CheckNull("multiPointWellKnownText");
			return DbSpatialServices.Default.GeographyMultiPointFromText(multiPointWellKnownText, coordinateSystemId);
		}

		// Token: 0x06002AC8 RID: 10952 RVA: 0x000A7714 File Offset: 0x000A5914
		public static DbGeography MultiPolygonFromText(string multiPolygonWellKnownText, int coordinateSystemId)
		{
			multiPolygonWellKnownText.CheckNull("multiPolygonWellKnownText");
			return DbSpatialServices.Default.GeographyMultiPolygonFromText(multiPolygonWellKnownText, coordinateSystemId);
		}

		// Token: 0x06002AC9 RID: 10953 RVA: 0x000A772D File Offset: 0x000A592D
		public static DbGeography GeographyCollectionFromText(string geographyCollectionWellKnownText, int coordinateSystemId)
		{
			geographyCollectionWellKnownText.CheckNull("geographyCollectionWellKnownText");
			return DbSpatialServices.Default.GeographyCollectionFromText(geographyCollectionWellKnownText, coordinateSystemId);
		}

		// Token: 0x17000852 RID: 2130
		// (get) Token: 0x06002ACA RID: 10954 RVA: 0x000A7746 File Offset: 0x000A5946
		public int CoordinateSystemId
		{
			get
			{
				return this.spatialSvcs.GetCoordinateSystemId(this);
			}
		}

		// Token: 0x17000853 RID: 2131
		// (get) Token: 0x06002ACB RID: 10955 RVA: 0x000A7754 File Offset: 0x000A5954
		public int Dimension
		{
			get
			{
				return this.spatialSvcs.GetDimension(this);
			}
		}

		// Token: 0x17000854 RID: 2132
		// (get) Token: 0x06002ACC RID: 10956 RVA: 0x000A7762 File Offset: 0x000A5962
		public string SpatialTypeName
		{
			get
			{
				return this.spatialSvcs.GetSpatialTypeName(this);
			}
		}

		// Token: 0x17000855 RID: 2133
		// (get) Token: 0x06002ACD RID: 10957 RVA: 0x000A7770 File Offset: 0x000A5970
		public bool IsEmpty
		{
			get
			{
				return this.spatialSvcs.GetIsEmpty(this);
			}
		}

		// Token: 0x06002ACE RID: 10958 RVA: 0x000A777E File Offset: 0x000A597E
		public string AsText()
		{
			return this.spatialSvcs.AsText(this);
		}

		// Token: 0x06002ACF RID: 10959 RVA: 0x000A778C File Offset: 0x000A598C
		internal string AsTextIncludingElevationAndMeasure()
		{
			return this.spatialSvcs.AsTextIncludingElevationAndMeasure(this);
		}

		// Token: 0x06002AD0 RID: 10960 RVA: 0x000A779A File Offset: 0x000A599A
		public byte[] AsBinary()
		{
			return this.spatialSvcs.AsBinary(this);
		}

		// Token: 0x06002AD1 RID: 10961 RVA: 0x000A77A8 File Offset: 0x000A59A8
		public string AsGml()
		{
			return this.spatialSvcs.AsGml(this);
		}

		// Token: 0x06002AD2 RID: 10962 RVA: 0x000A77B6 File Offset: 0x000A59B6
		public bool SpatialEquals(DbGeography other)
		{
			other.CheckNull("other");
			return this.spatialSvcs.SpatialEquals(this, other);
		}

		// Token: 0x06002AD3 RID: 10963 RVA: 0x000A77D0 File Offset: 0x000A59D0
		public bool Disjoint(DbGeography other)
		{
			other.CheckNull("other");
			return this.spatialSvcs.Disjoint(this, other);
		}

		// Token: 0x06002AD4 RID: 10964 RVA: 0x000A77EA File Offset: 0x000A59EA
		public bool Intersects(DbGeography other)
		{
			other.CheckNull("other");
			return this.spatialSvcs.Intersects(this, other);
		}

		// Token: 0x06002AD5 RID: 10965 RVA: 0x000A7804 File Offset: 0x000A5A04
		public DbGeography Buffer(double? distance)
		{
			if (distance == null)
			{
				throw EntityUtil.ArgumentNull("distance");
			}
			return this.spatialSvcs.Buffer(this, distance.Value);
		}

		// Token: 0x06002AD6 RID: 10966 RVA: 0x000A782D File Offset: 0x000A5A2D
		public double? Distance(DbGeography other)
		{
			other.CheckNull("other");
			return new double?(this.spatialSvcs.Distance(this, other));
		}

		// Token: 0x06002AD7 RID: 10967 RVA: 0x000A784C File Offset: 0x000A5A4C
		public DbGeography Intersection(DbGeography other)
		{
			other.CheckNull("other");
			return this.spatialSvcs.Intersection(this, other);
		}

		// Token: 0x06002AD8 RID: 10968 RVA: 0x000A7866 File Offset: 0x000A5A66
		public DbGeography Union(DbGeography other)
		{
			other.CheckNull("other");
			return this.spatialSvcs.Union(this, other);
		}

		// Token: 0x06002AD9 RID: 10969 RVA: 0x000A7880 File Offset: 0x000A5A80
		public DbGeography Difference(DbGeography other)
		{
			other.CheckNull("other");
			return this.spatialSvcs.Difference(this, other);
		}

		// Token: 0x06002ADA RID: 10970 RVA: 0x000A789A File Offset: 0x000A5A9A
		public DbGeography SymmetricDifference(DbGeography other)
		{
			other.CheckNull("other");
			return this.spatialSvcs.SymmetricDifference(this, other);
		}

		// Token: 0x17000856 RID: 2134
		// (get) Token: 0x06002ADB RID: 10971 RVA: 0x000A78B4 File Offset: 0x000A5AB4
		public int? ElementCount
		{
			get
			{
				return this.spatialSvcs.GetElementCount(this);
			}
		}

		// Token: 0x06002ADC RID: 10972 RVA: 0x000A78C2 File Offset: 0x000A5AC2
		public DbGeography ElementAt(int index)
		{
			return this.spatialSvcs.ElementAt(this, index);
		}

		// Token: 0x17000857 RID: 2135
		// (get) Token: 0x06002ADD RID: 10973 RVA: 0x000A78D1 File Offset: 0x000A5AD1
		public double? Latitude
		{
			get
			{
				return this.spatialSvcs.GetLatitude(this);
			}
		}

		// Token: 0x17000858 RID: 2136
		// (get) Token: 0x06002ADE RID: 10974 RVA: 0x000A78DF File Offset: 0x000A5ADF
		public double? Longitude
		{
			get
			{
				return this.spatialSvcs.GetLongitude(this);
			}
		}

		// Token: 0x17000859 RID: 2137
		// (get) Token: 0x06002ADF RID: 10975 RVA: 0x000A78ED File Offset: 0x000A5AED
		public double? Elevation
		{
			get
			{
				return this.spatialSvcs.GetElevation(this);
			}
		}

		// Token: 0x1700085A RID: 2138
		// (get) Token: 0x06002AE0 RID: 10976 RVA: 0x000A78FB File Offset: 0x000A5AFB
		public double? Measure
		{
			get
			{
				return this.spatialSvcs.GetMeasure(this);
			}
		}

		// Token: 0x1700085B RID: 2139
		// (get) Token: 0x06002AE1 RID: 10977 RVA: 0x000A7909 File Offset: 0x000A5B09
		public double? Length
		{
			get
			{
				return this.spatialSvcs.GetLength(this);
			}
		}

		// Token: 0x1700085C RID: 2140
		// (get) Token: 0x06002AE2 RID: 10978 RVA: 0x000A7917 File Offset: 0x000A5B17
		public DbGeography StartPoint
		{
			get
			{
				return this.spatialSvcs.GetStartPoint(this);
			}
		}

		// Token: 0x1700085D RID: 2141
		// (get) Token: 0x06002AE3 RID: 10979 RVA: 0x000A7925 File Offset: 0x000A5B25
		public DbGeography EndPoint
		{
			get
			{
				return this.spatialSvcs.GetEndPoint(this);
			}
		}

		// Token: 0x1700085E RID: 2142
		// (get) Token: 0x06002AE4 RID: 10980 RVA: 0x000A7933 File Offset: 0x000A5B33
		public bool? IsClosed
		{
			get
			{
				return this.spatialSvcs.GetIsClosed(this);
			}
		}

		// Token: 0x1700085F RID: 2143
		// (get) Token: 0x06002AE5 RID: 10981 RVA: 0x000A7941 File Offset: 0x000A5B41
		public int? PointCount
		{
			get
			{
				return this.spatialSvcs.GetPointCount(this);
			}
		}

		// Token: 0x06002AE6 RID: 10982 RVA: 0x000A794F File Offset: 0x000A5B4F
		public DbGeography PointAt(int index)
		{
			return this.spatialSvcs.PointAt(this, index);
		}

		// Token: 0x17000860 RID: 2144
		// (get) Token: 0x06002AE7 RID: 10983 RVA: 0x000A795E File Offset: 0x000A5B5E
		public double? Area
		{
			get
			{
				return this.spatialSvcs.GetArea(this);
			}
		}

		// Token: 0x06002AE8 RID: 10984 RVA: 0x000A796C File Offset: 0x000A5B6C
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "SRID={1};{0}", new object[]
			{
				this.WellKnownValue.WellKnownText ?? base.ToString(),
				this.CoordinateSystemId
			});
		}

		// Token: 0x040012F2 RID: 4850
		private DbSpatialServices spatialSvcs;

		// Token: 0x040012F3 RID: 4851
		private object providerValue;
	}
}
