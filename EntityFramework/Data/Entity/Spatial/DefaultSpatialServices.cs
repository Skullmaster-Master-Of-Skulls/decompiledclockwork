using System;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;

namespace System.Data.Entity.Spatial
{
	// Token: 0x0200071F RID: 1823
	[Serializable]
	internal sealed class DefaultSpatialServices : DbSpatialServices
	{
		// Token: 0x06004A94 RID: 19092 RVA: 0x00160552 File Offset: 0x0015E752
		private DefaultSpatialServices()
		{
		}

		// Token: 0x06004A95 RID: 19093 RVA: 0x0016055A File Offset: 0x0015E75A
		private static Exception SpatialServicesUnavailable()
		{
			return new NotImplementedException(Strings.SpatialProviderNotUsable);
		}

		// Token: 0x06004A96 RID: 19094 RVA: 0x00160568 File Offset: 0x0015E768
		private static DefaultSpatialServices.ReadOnlySpatialValues CheckProviderValue(object providerValue)
		{
			DefaultSpatialServices.ReadOnlySpatialValues readOnlySpatialValues = providerValue as DefaultSpatialServices.ReadOnlySpatialValues;
			if (readOnlySpatialValues == null)
			{
				throw new ArgumentException(Strings.Spatial_ProviderValueNotCompatibleWithSpatialServices, "providerValue");
			}
			return readOnlySpatialValues;
		}

		// Token: 0x06004A97 RID: 19095 RVA: 0x00160590 File Offset: 0x0015E790
		private static DefaultSpatialServices.ReadOnlySpatialValues CheckCompatible(DbGeography geographyValue)
		{
			if (geographyValue != null)
			{
				DefaultSpatialServices.ReadOnlySpatialValues readOnlySpatialValues = geographyValue.ProviderValue as DefaultSpatialServices.ReadOnlySpatialValues;
				if (readOnlySpatialValues != null)
				{
					return readOnlySpatialValues;
				}
			}
			throw new ArgumentException(Strings.Spatial_GeographyValueNotCompatibleWithSpatialServices, "geographyValue");
		}

		// Token: 0x06004A98 RID: 19096 RVA: 0x001605C0 File Offset: 0x0015E7C0
		private static DefaultSpatialServices.ReadOnlySpatialValues CheckCompatible(DbGeometry geometryValue)
		{
			if (geometryValue != null)
			{
				DefaultSpatialServices.ReadOnlySpatialValues readOnlySpatialValues = geometryValue.ProviderValue as DefaultSpatialServices.ReadOnlySpatialValues;
				if (readOnlySpatialValues != null)
				{
					return readOnlySpatialValues;
				}
			}
			throw new ArgumentException(Strings.Spatial_GeometryValueNotCompatibleWithSpatialServices, "geometryValue");
		}

		// Token: 0x06004A99 RID: 19097 RVA: 0x001605F0 File Offset: 0x0015E7F0
		public override DbGeography GeographyFromProviderValue(object providerValue)
		{
			Check.NotNull<object>(providerValue, "providerValue");
			DefaultSpatialServices.ReadOnlySpatialValues providerValue2 = DefaultSpatialServices.CheckProviderValue(providerValue);
			return DbSpatialServices.CreateGeography(this, providerValue2);
		}

		// Token: 0x06004A9A RID: 19098 RVA: 0x00160617 File Offset: 0x0015E817
		public override object CreateProviderValue(DbGeographyWellKnownValue wellKnownValue)
		{
			Check.NotNull<DbGeographyWellKnownValue>(wellKnownValue, "wellKnownValue");
			return new DefaultSpatialServices.ReadOnlySpatialValues(wellKnownValue.CoordinateSystemId, wellKnownValue.WellKnownText, wellKnownValue.WellKnownBinary, null);
		}

		// Token: 0x06004A9B RID: 19099 RVA: 0x00160640 File Offset: 0x0015E840
		public override DbGeographyWellKnownValue CreateWellKnownValue(DbGeography geographyValue)
		{
			Check.NotNull<DbGeography>(geographyValue, "geographyValue");
			DefaultSpatialServices.ReadOnlySpatialValues readOnlySpatialValues = DefaultSpatialServices.CheckCompatible(geographyValue);
			return new DbGeographyWellKnownValue
			{
				CoordinateSystemId = readOnlySpatialValues.CoordinateSystemId,
				WellKnownBinary = readOnlySpatialValues.CloneBinary(),
				WellKnownText = readOnlySpatialValues.Text
			};
		}

		// Token: 0x06004A9C RID: 19100 RVA: 0x0016068C File Offset: 0x0015E88C
		public override DbGeography GeographyFromBinary(byte[] geographyBinary)
		{
			Check.NotNull<byte[]>(geographyBinary, "geographyBinary");
			DefaultSpatialServices.ReadOnlySpatialValues providerValue = new DefaultSpatialServices.ReadOnlySpatialValues(DbGeography.DefaultCoordinateSystemId, null, geographyBinary, null);
			return DbSpatialServices.CreateGeography(this, providerValue);
		}

		// Token: 0x06004A9D RID: 19101 RVA: 0x001606BC File Offset: 0x0015E8BC
		public override DbGeography GeographyFromBinary(byte[] geographyBinary, int spatialReferenceSystemId)
		{
			Check.NotNull<byte[]>(geographyBinary, "geographyBinary");
			DefaultSpatialServices.ReadOnlySpatialValues providerValue = new DefaultSpatialServices.ReadOnlySpatialValues(spatialReferenceSystemId, null, geographyBinary, null);
			return DbSpatialServices.CreateGeography(this, providerValue);
		}

		// Token: 0x06004A9E RID: 19102 RVA: 0x001606E6 File Offset: 0x0015E8E6
		public override DbGeography GeographyLineFromBinary(byte[] geographyBinary, int spatialReferenceSystemId)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06004A9F RID: 19103 RVA: 0x001606ED File Offset: 0x0015E8ED
		public override DbGeography GeographyPointFromBinary(byte[] geographyBinary, int spatialReferenceSystemId)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06004AA0 RID: 19104 RVA: 0x001606F4 File Offset: 0x0015E8F4
		public override DbGeography GeographyPolygonFromBinary(byte[] geographyBinary, int spatialReferenceSystemId)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06004AA1 RID: 19105 RVA: 0x001606FB File Offset: 0x0015E8FB
		public override DbGeography GeographyMultiLineFromBinary(byte[] geographyBinary, int spatialReferenceSystemId)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06004AA2 RID: 19106 RVA: 0x00160702 File Offset: 0x0015E902
		public override DbGeography GeographyMultiPointFromBinary(byte[] geographyBinary, int spatialReferenceSystemId)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06004AA3 RID: 19107 RVA: 0x00160709 File Offset: 0x0015E909
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "MultiPolygon", Justification = "Match MultiPoint, MultiLine")]
		public override DbGeography GeographyMultiPolygonFromBinary(byte[] geographyBinary, int spatialReferenceSystemId)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06004AA4 RID: 19108 RVA: 0x00160710 File Offset: 0x0015E910
		public override DbGeography GeographyCollectionFromBinary(byte[] geographyBinary, int spatialReferenceSystemId)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06004AA5 RID: 19109 RVA: 0x00160718 File Offset: 0x0015E918
		public override DbGeography GeographyFromText(string geographyText)
		{
			Check.NotNull<string>(geographyText, "geographyText");
			DefaultSpatialServices.ReadOnlySpatialValues providerValue = new DefaultSpatialServices.ReadOnlySpatialValues(DbGeography.DefaultCoordinateSystemId, geographyText, null, null);
			return DbSpatialServices.CreateGeography(this, providerValue);
		}

		// Token: 0x06004AA6 RID: 19110 RVA: 0x00160748 File Offset: 0x0015E948
		public override DbGeography GeographyFromText(string geographyText, int spatialReferenceSystemId)
		{
			Check.NotNull<string>(geographyText, "geographyText");
			DefaultSpatialServices.ReadOnlySpatialValues providerValue = new DefaultSpatialServices.ReadOnlySpatialValues(spatialReferenceSystemId, geographyText, null, null);
			return DbSpatialServices.CreateGeography(this, providerValue);
		}

		// Token: 0x06004AA7 RID: 19111 RVA: 0x00160772 File Offset: 0x0015E972
		public override DbGeography GeographyLineFromText(string geographyText, int spatialReferenceSystemId)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06004AA8 RID: 19112 RVA: 0x00160779 File Offset: 0x0015E979
		public override DbGeography GeographyPointFromText(string geographyText, int spatialReferenceSystemId)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06004AA9 RID: 19113 RVA: 0x00160780 File Offset: 0x0015E980
		public override DbGeography GeographyPolygonFromText(string geographyText, int spatialReferenceSystemId)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06004AAA RID: 19114 RVA: 0x00160787 File Offset: 0x0015E987
		public override DbGeography GeographyMultiLineFromText(string geographyText, int spatialReferenceSystemId)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06004AAB RID: 19115 RVA: 0x0016078E File Offset: 0x0015E98E
		public override DbGeography GeographyMultiPointFromText(string geographyText, int spatialReferenceSystemId)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06004AAC RID: 19116 RVA: 0x00160795 File Offset: 0x0015E995
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "MultiPolygon", Justification = "Match MultiPoint, MultiLine")]
		public override DbGeography GeographyMultiPolygonFromText(string multiPolygonKnownText, int spatialReferenceSystemId)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06004AAD RID: 19117 RVA: 0x0016079C File Offset: 0x0015E99C
		public override DbGeography GeographyCollectionFromText(string geographyText, int spatialReferenceSystemId)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06004AAE RID: 19118 RVA: 0x001607A4 File Offset: 0x0015E9A4
		public override DbGeography GeographyFromGml(string geographyMarkup)
		{
			Check.NotNull<string>(geographyMarkup, "geographyMarkup");
			DefaultSpatialServices.ReadOnlySpatialValues providerValue = new DefaultSpatialServices.ReadOnlySpatialValues(DbGeography.DefaultCoordinateSystemId, null, null, geographyMarkup);
			return DbSpatialServices.CreateGeography(this, providerValue);
		}

		// Token: 0x06004AAF RID: 19119 RVA: 0x001607D4 File Offset: 0x0015E9D4
		public override DbGeography GeographyFromGml(string geographyMarkup, int spatialReferenceSystemId)
		{
			Check.NotNull<string>(geographyMarkup, "geographyMarkup");
			DefaultSpatialServices.ReadOnlySpatialValues providerValue = new DefaultSpatialServices.ReadOnlySpatialValues(spatialReferenceSystemId, null, null, geographyMarkup);
			return DbSpatialServices.CreateGeography(this, providerValue);
		}

		// Token: 0x06004AB0 RID: 19120 RVA: 0x00160800 File Offset: 0x0015EA00
		public override int GetCoordinateSystemId(DbGeography geographyValue)
		{
			Check.NotNull<DbGeography>(geographyValue, "geographyValue");
			DefaultSpatialServices.ReadOnlySpatialValues readOnlySpatialValues = DefaultSpatialServices.CheckCompatible(geographyValue);
			return readOnlySpatialValues.CoordinateSystemId;
		}

		// Token: 0x06004AB1 RID: 19121 RVA: 0x00160826 File Offset: 0x0015EA26
		public override int GetDimension(DbGeography geographyValue)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06004AB2 RID: 19122 RVA: 0x0016082D File Offset: 0x0015EA2D
		public override string GetSpatialTypeName(DbGeography geographyValue)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06004AB3 RID: 19123 RVA: 0x00160834 File Offset: 0x0015EA34
		public override bool GetIsEmpty(DbGeography geographyValue)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06004AB4 RID: 19124 RVA: 0x0016083C File Offset: 0x0015EA3C
		public override string AsText(DbGeography geographyValue)
		{
			Check.NotNull<DbGeography>(geographyValue, "geographyValue");
			DefaultSpatialServices.ReadOnlySpatialValues readOnlySpatialValues = DefaultSpatialServices.CheckCompatible(geographyValue);
			return readOnlySpatialValues.Text;
		}

		// Token: 0x06004AB5 RID: 19125 RVA: 0x00160864 File Offset: 0x0015EA64
		public override byte[] AsBinary(DbGeography geographyValue)
		{
			Check.NotNull<DbGeography>(geographyValue, "geographyValue");
			DefaultSpatialServices.ReadOnlySpatialValues readOnlySpatialValues = DefaultSpatialServices.CheckCompatible(geographyValue);
			return readOnlySpatialValues.CloneBinary();
		}

		// Token: 0x06004AB6 RID: 19126 RVA: 0x0016088C File Offset: 0x0015EA8C
		public override string AsGml(DbGeography geographyValue)
		{
			Check.NotNull<DbGeography>(geographyValue, "geographyValue");
			DefaultSpatialServices.ReadOnlySpatialValues readOnlySpatialValues = DefaultSpatialServices.CheckCompatible(geographyValue);
			return readOnlySpatialValues.GML;
		}

		// Token: 0x06004AB7 RID: 19127 RVA: 0x001608B2 File Offset: 0x0015EAB2
		public override bool SpatialEquals(DbGeography geographyValue, DbGeography otherGeography)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06004AB8 RID: 19128 RVA: 0x001608B9 File Offset: 0x0015EAB9
		public override bool Disjoint(DbGeography geographyValue, DbGeography otherGeography)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06004AB9 RID: 19129 RVA: 0x001608C0 File Offset: 0x0015EAC0
		public override bool Intersects(DbGeography geographyValue, DbGeography otherGeography)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06004ABA RID: 19130 RVA: 0x001608C7 File Offset: 0x0015EAC7
		public override DbGeography Buffer(DbGeography geographyValue, double distance)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06004ABB RID: 19131 RVA: 0x001608CE File Offset: 0x0015EACE
		public override double Distance(DbGeography geographyValue, DbGeography otherGeography)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06004ABC RID: 19132 RVA: 0x001608D5 File Offset: 0x0015EAD5
		public override DbGeography Intersection(DbGeography geographyValue, DbGeography otherGeography)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06004ABD RID: 19133 RVA: 0x001608DC File Offset: 0x0015EADC
		public override DbGeography Union(DbGeography geographyValue, DbGeography otherGeography)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06004ABE RID: 19134 RVA: 0x001608E3 File Offset: 0x0015EAE3
		public override DbGeography Difference(DbGeography geographyValue, DbGeography otherGeography)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06004ABF RID: 19135 RVA: 0x001608EA File Offset: 0x0015EAEA
		public override DbGeography SymmetricDifference(DbGeography geographyValue, DbGeography otherGeography)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06004AC0 RID: 19136 RVA: 0x001608F1 File Offset: 0x0015EAF1
		public override int? GetElementCount(DbGeography geographyValue)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06004AC1 RID: 19137 RVA: 0x001608F8 File Offset: 0x0015EAF8
		public override DbGeography ElementAt(DbGeography geographyValue, int index)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06004AC2 RID: 19138 RVA: 0x001608FF File Offset: 0x0015EAFF
		public override double? GetLatitude(DbGeography geographyValue)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06004AC3 RID: 19139 RVA: 0x00160906 File Offset: 0x0015EB06
		public override double? GetLongitude(DbGeography geographyValue)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06004AC4 RID: 19140 RVA: 0x0016090D File Offset: 0x0015EB0D
		public override double? GetElevation(DbGeography geographyValue)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06004AC5 RID: 19141 RVA: 0x00160914 File Offset: 0x0015EB14
		public override double? GetMeasure(DbGeography geographyValue)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06004AC6 RID: 19142 RVA: 0x0016091B File Offset: 0x0015EB1B
		public override double? GetLength(DbGeography geographyValue)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06004AC7 RID: 19143 RVA: 0x00160922 File Offset: 0x0015EB22
		public override DbGeography GetEndPoint(DbGeography geographyValue)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06004AC8 RID: 19144 RVA: 0x00160929 File Offset: 0x0015EB29
		public override DbGeography GetStartPoint(DbGeography geographyValue)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06004AC9 RID: 19145 RVA: 0x00160930 File Offset: 0x0015EB30
		public override bool? GetIsClosed(DbGeography geographyValue)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06004ACA RID: 19146 RVA: 0x00160937 File Offset: 0x0015EB37
		public override int? GetPointCount(DbGeography geographyValue)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06004ACB RID: 19147 RVA: 0x0016093E File Offset: 0x0015EB3E
		public override DbGeography PointAt(DbGeography geographyValue, int index)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06004ACC RID: 19148 RVA: 0x00160945 File Offset: 0x0015EB45
		public override double? GetArea(DbGeography geographyValue)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06004ACD RID: 19149 RVA: 0x0016094C File Offset: 0x0015EB4C
		public override object CreateProviderValue(DbGeometryWellKnownValue wellKnownValue)
		{
			Check.NotNull<DbGeometryWellKnownValue>(wellKnownValue, "wellKnownValue");
			return new DefaultSpatialServices.ReadOnlySpatialValues(wellKnownValue.CoordinateSystemId, wellKnownValue.WellKnownText, wellKnownValue.WellKnownBinary, null);
		}

		// Token: 0x06004ACE RID: 19150 RVA: 0x00160974 File Offset: 0x0015EB74
		public override DbGeometryWellKnownValue CreateWellKnownValue(DbGeometry geometryValue)
		{
			Check.NotNull<DbGeometry>(geometryValue, "geometryValue");
			DefaultSpatialServices.ReadOnlySpatialValues readOnlySpatialValues = DefaultSpatialServices.CheckCompatible(geometryValue);
			return new DbGeometryWellKnownValue
			{
				CoordinateSystemId = readOnlySpatialValues.CoordinateSystemId,
				WellKnownBinary = readOnlySpatialValues.CloneBinary(),
				WellKnownText = readOnlySpatialValues.Text
			};
		}

		// Token: 0x06004ACF RID: 19151 RVA: 0x001609C0 File Offset: 0x0015EBC0
		public override DbGeometry GeometryFromProviderValue(object providerValue)
		{
			Check.NotNull<object>(providerValue, "providerValue");
			DefaultSpatialServices.ReadOnlySpatialValues providerValue2 = DefaultSpatialServices.CheckProviderValue(providerValue);
			return DbSpatialServices.CreateGeometry(this, providerValue2);
		}

		// Token: 0x06004AD0 RID: 19152 RVA: 0x001609E8 File Offset: 0x0015EBE8
		public override DbGeometry GeometryFromBinary(byte[] geometryBinary)
		{
			Check.NotNull<byte[]>(geometryBinary, "geometryBinary");
			DefaultSpatialServices.ReadOnlySpatialValues providerValue = new DefaultSpatialServices.ReadOnlySpatialValues(DbGeometry.DefaultCoordinateSystemId, null, geometryBinary, null);
			return DbSpatialServices.CreateGeometry(this, providerValue);
		}

		// Token: 0x06004AD1 RID: 19153 RVA: 0x00160A18 File Offset: 0x0015EC18
		public override DbGeometry GeometryFromBinary(byte[] geometryBinary, int spatialReferenceSystemId)
		{
			Check.NotNull<byte[]>(geometryBinary, "geometryBinary");
			DefaultSpatialServices.ReadOnlySpatialValues providerValue = new DefaultSpatialServices.ReadOnlySpatialValues(spatialReferenceSystemId, null, geometryBinary, null);
			return DbSpatialServices.CreateGeometry(this, providerValue);
		}

		// Token: 0x06004AD2 RID: 19154 RVA: 0x00160A42 File Offset: 0x0015EC42
		public override DbGeometry GeometryLineFromBinary(byte[] geometryBinary, int spatialReferenceSystemId)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06004AD3 RID: 19155 RVA: 0x00160A49 File Offset: 0x0015EC49
		public override DbGeometry GeometryPointFromBinary(byte[] geometryBinary, int spatialReferenceSystemId)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06004AD4 RID: 19156 RVA: 0x00160A50 File Offset: 0x0015EC50
		public override DbGeometry GeometryPolygonFromBinary(byte[] geometryBinary, int spatialReferenceSystemId)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06004AD5 RID: 19157 RVA: 0x00160A57 File Offset: 0x0015EC57
		public override DbGeometry GeometryMultiLineFromBinary(byte[] geometryBinary, int spatialReferenceSystemId)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06004AD6 RID: 19158 RVA: 0x00160A5E File Offset: 0x0015EC5E
		public override DbGeometry GeometryMultiPointFromBinary(byte[] geometryBinary, int spatialReferenceSystemId)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06004AD7 RID: 19159 RVA: 0x00160A65 File Offset: 0x0015EC65
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "MultiPolygon", Justification = "Match MultiPoint, MultiLine")]
		public override DbGeometry GeometryMultiPolygonFromBinary(byte[] geometryBinary, int spatialReferenceSystemId)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06004AD8 RID: 19160 RVA: 0x00160A6C File Offset: 0x0015EC6C
		public override DbGeometry GeometryCollectionFromBinary(byte[] geometryBinary, int spatialReferenceSystemId)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06004AD9 RID: 19161 RVA: 0x00160A74 File Offset: 0x0015EC74
		public override DbGeometry GeometryFromText(string geometryText)
		{
			Check.NotNull<string>(geometryText, "geometryText");
			DefaultSpatialServices.ReadOnlySpatialValues providerValue = new DefaultSpatialServices.ReadOnlySpatialValues(DbGeometry.DefaultCoordinateSystemId, geometryText, null, null);
			return DbSpatialServices.CreateGeometry(this, providerValue);
		}

		// Token: 0x06004ADA RID: 19162 RVA: 0x00160AA4 File Offset: 0x0015ECA4
		public override DbGeometry GeometryFromText(string geometryText, int spatialReferenceSystemId)
		{
			Check.NotNull<string>(geometryText, "geometryText");
			DefaultSpatialServices.ReadOnlySpatialValues providerValue = new DefaultSpatialServices.ReadOnlySpatialValues(spatialReferenceSystemId, geometryText, null, null);
			return DbSpatialServices.CreateGeometry(this, providerValue);
		}

		// Token: 0x06004ADB RID: 19163 RVA: 0x00160ACE File Offset: 0x0015ECCE
		public override DbGeometry GeometryLineFromText(string geometryText, int spatialReferenceSystemId)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06004ADC RID: 19164 RVA: 0x00160AD5 File Offset: 0x0015ECD5
		public override DbGeometry GeometryPointFromText(string geometryText, int spatialReferenceSystemId)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06004ADD RID: 19165 RVA: 0x00160ADC File Offset: 0x0015ECDC
		public override DbGeometry GeometryPolygonFromText(string geometryText, int spatialReferenceSystemId)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06004ADE RID: 19166 RVA: 0x00160AE3 File Offset: 0x0015ECE3
		public override DbGeometry GeometryMultiLineFromText(string geometryText, int spatialReferenceSystemId)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06004ADF RID: 19167 RVA: 0x00160AEA File Offset: 0x0015ECEA
		public override DbGeometry GeometryMultiPointFromText(string geometryText, int spatialReferenceSystemId)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06004AE0 RID: 19168 RVA: 0x00160AF1 File Offset: 0x0015ECF1
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "MultiPolygon", Justification = "Match MultiPoint, MultiLine")]
		public override DbGeometry GeometryMultiPolygonFromText(string geometryText, int spatialReferenceSystemId)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06004AE1 RID: 19169 RVA: 0x00160AF8 File Offset: 0x0015ECF8
		public override DbGeometry GeometryCollectionFromText(string geometryText, int spatialReferenceSystemId)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06004AE2 RID: 19170 RVA: 0x00160B00 File Offset: 0x0015ED00
		public override DbGeometry GeometryFromGml(string geometryMarkup)
		{
			Check.NotNull<string>(geometryMarkup, "geometryMarkup");
			DefaultSpatialServices.ReadOnlySpatialValues providerValue = new DefaultSpatialServices.ReadOnlySpatialValues(DbGeometry.DefaultCoordinateSystemId, null, null, geometryMarkup);
			return DbSpatialServices.CreateGeometry(this, providerValue);
		}

		// Token: 0x06004AE3 RID: 19171 RVA: 0x00160B30 File Offset: 0x0015ED30
		public override DbGeometry GeometryFromGml(string geometryMarkup, int spatialReferenceSystemId)
		{
			Check.NotNull<string>(geometryMarkup, "geometryMarkup");
			DefaultSpatialServices.ReadOnlySpatialValues providerValue = new DefaultSpatialServices.ReadOnlySpatialValues(spatialReferenceSystemId, null, null, geometryMarkup);
			return DbSpatialServices.CreateGeometry(this, providerValue);
		}

		// Token: 0x06004AE4 RID: 19172 RVA: 0x00160B5C File Offset: 0x0015ED5C
		public override int GetCoordinateSystemId(DbGeometry geometryValue)
		{
			Check.NotNull<DbGeometry>(geometryValue, "geometryValue");
			DefaultSpatialServices.ReadOnlySpatialValues readOnlySpatialValues = DefaultSpatialServices.CheckCompatible(geometryValue);
			return readOnlySpatialValues.CoordinateSystemId;
		}

		// Token: 0x06004AE5 RID: 19173 RVA: 0x00160B82 File Offset: 0x0015ED82
		public override DbGeometry GetBoundary(DbGeometry geometryValue)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06004AE6 RID: 19174 RVA: 0x00160B89 File Offset: 0x0015ED89
		public override int GetDimension(DbGeometry geometryValue)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06004AE7 RID: 19175 RVA: 0x00160B90 File Offset: 0x0015ED90
		public override DbGeometry GetEnvelope(DbGeometry geometryValue)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06004AE8 RID: 19176 RVA: 0x00160B97 File Offset: 0x0015ED97
		public override string GetSpatialTypeName(DbGeometry geometryValue)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06004AE9 RID: 19177 RVA: 0x00160B9E File Offset: 0x0015ED9E
		public override bool GetIsEmpty(DbGeometry geometryValue)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06004AEA RID: 19178 RVA: 0x00160BA5 File Offset: 0x0015EDA5
		public override bool GetIsSimple(DbGeometry geometryValue)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06004AEB RID: 19179 RVA: 0x00160BAC File Offset: 0x0015EDAC
		public override bool GetIsValid(DbGeometry geometryValue)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06004AEC RID: 19180 RVA: 0x00160BB4 File Offset: 0x0015EDB4
		public override string AsText(DbGeometry geometryValue)
		{
			Check.NotNull<DbGeometry>(geometryValue, "geometryValue");
			DefaultSpatialServices.ReadOnlySpatialValues readOnlySpatialValues = DefaultSpatialServices.CheckCompatible(geometryValue);
			return readOnlySpatialValues.Text;
		}

		// Token: 0x06004AED RID: 19181 RVA: 0x00160BDC File Offset: 0x0015EDDC
		public override byte[] AsBinary(DbGeometry geometryValue)
		{
			Check.NotNull<DbGeometry>(geometryValue, "geometryValue");
			DefaultSpatialServices.ReadOnlySpatialValues readOnlySpatialValues = DefaultSpatialServices.CheckCompatible(geometryValue);
			return readOnlySpatialValues.CloneBinary();
		}

		// Token: 0x06004AEE RID: 19182 RVA: 0x00160C04 File Offset: 0x0015EE04
		public override string AsGml(DbGeometry geometryValue)
		{
			Check.NotNull<DbGeometry>(geometryValue, "geometryValue");
			DefaultSpatialServices.ReadOnlySpatialValues readOnlySpatialValues = DefaultSpatialServices.CheckCompatible(geometryValue);
			return readOnlySpatialValues.GML;
		}

		// Token: 0x06004AEF RID: 19183 RVA: 0x00160C2A File Offset: 0x0015EE2A
		public override bool SpatialEquals(DbGeometry geometryValue, DbGeometry otherGeometry)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06004AF0 RID: 19184 RVA: 0x00160C31 File Offset: 0x0015EE31
		public override bool Disjoint(DbGeometry geometryValue, DbGeometry otherGeometry)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06004AF1 RID: 19185 RVA: 0x00160C38 File Offset: 0x0015EE38
		public override bool Intersects(DbGeometry geometryValue, DbGeometry otherGeometry)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06004AF2 RID: 19186 RVA: 0x00160C3F File Offset: 0x0015EE3F
		public override bool Touches(DbGeometry geometryValue, DbGeometry otherGeometry)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06004AF3 RID: 19187 RVA: 0x00160C46 File Offset: 0x0015EE46
		public override bool Crosses(DbGeometry geometryValue, DbGeometry otherGeometry)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06004AF4 RID: 19188 RVA: 0x00160C4D File Offset: 0x0015EE4D
		public override bool Within(DbGeometry geometryValue, DbGeometry otherGeometry)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06004AF5 RID: 19189 RVA: 0x00160C54 File Offset: 0x0015EE54
		public override bool Contains(DbGeometry geometryValue, DbGeometry otherGeometry)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06004AF6 RID: 19190 RVA: 0x00160C5B File Offset: 0x0015EE5B
		public override bool Overlaps(DbGeometry geometryValue, DbGeometry otherGeometry)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06004AF7 RID: 19191 RVA: 0x00160C62 File Offset: 0x0015EE62
		public override bool Relate(DbGeometry geometryValue, DbGeometry otherGeometry, string matrix)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06004AF8 RID: 19192 RVA: 0x00160C69 File Offset: 0x0015EE69
		public override DbGeometry Buffer(DbGeometry geometryValue, double distance)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06004AF9 RID: 19193 RVA: 0x00160C70 File Offset: 0x0015EE70
		public override double Distance(DbGeometry geometryValue, DbGeometry otherGeometry)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06004AFA RID: 19194 RVA: 0x00160C77 File Offset: 0x0015EE77
		public override DbGeometry GetConvexHull(DbGeometry geometryValue)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06004AFB RID: 19195 RVA: 0x00160C7E File Offset: 0x0015EE7E
		public override DbGeometry Intersection(DbGeometry geometryValue, DbGeometry otherGeometry)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06004AFC RID: 19196 RVA: 0x00160C85 File Offset: 0x0015EE85
		public override DbGeometry Union(DbGeometry geometryValue, DbGeometry otherGeometry)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06004AFD RID: 19197 RVA: 0x00160C8C File Offset: 0x0015EE8C
		public override DbGeometry Difference(DbGeometry geometryValue, DbGeometry otherGeometry)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06004AFE RID: 19198 RVA: 0x00160C93 File Offset: 0x0015EE93
		public override DbGeometry SymmetricDifference(DbGeometry geometryValue, DbGeometry otherGeometry)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06004AFF RID: 19199 RVA: 0x00160C9A File Offset: 0x0015EE9A
		public override int? GetElementCount(DbGeometry geometryValue)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06004B00 RID: 19200 RVA: 0x00160CA1 File Offset: 0x0015EEA1
		public override DbGeometry ElementAt(DbGeometry geometryValue, int index)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06004B01 RID: 19201 RVA: 0x00160CA8 File Offset: 0x0015EEA8
		public override double? GetXCoordinate(DbGeometry geometryValue)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06004B02 RID: 19202 RVA: 0x00160CAF File Offset: 0x0015EEAF
		public override double? GetYCoordinate(DbGeometry geometryValue)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06004B03 RID: 19203 RVA: 0x00160CB6 File Offset: 0x0015EEB6
		public override double? GetElevation(DbGeometry geometryValue)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06004B04 RID: 19204 RVA: 0x00160CBD File Offset: 0x0015EEBD
		public override double? GetMeasure(DbGeometry geometryValue)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06004B05 RID: 19205 RVA: 0x00160CC4 File Offset: 0x0015EEC4
		public override double? GetLength(DbGeometry geometryValue)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06004B06 RID: 19206 RVA: 0x00160CCB File Offset: 0x0015EECB
		public override DbGeometry GetEndPoint(DbGeometry geometryValue)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06004B07 RID: 19207 RVA: 0x00160CD2 File Offset: 0x0015EED2
		public override DbGeometry GetStartPoint(DbGeometry geometryValue)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06004B08 RID: 19208 RVA: 0x00160CD9 File Offset: 0x0015EED9
		public override bool? GetIsClosed(DbGeometry geometryValue)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06004B09 RID: 19209 RVA: 0x00160CE0 File Offset: 0x0015EEE0
		public override bool? GetIsRing(DbGeometry geometryValue)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06004B0A RID: 19210 RVA: 0x00160CE7 File Offset: 0x0015EEE7
		public override int? GetPointCount(DbGeometry geometryValue)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06004B0B RID: 19211 RVA: 0x00160CEE File Offset: 0x0015EEEE
		public override DbGeometry PointAt(DbGeometry geometryValue, int index)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06004B0C RID: 19212 RVA: 0x00160CF5 File Offset: 0x0015EEF5
		public override double? GetArea(DbGeometry geometryValue)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06004B0D RID: 19213 RVA: 0x00160CFC File Offset: 0x0015EEFC
		public override DbGeometry GetCentroid(DbGeometry geometryValue)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06004B0E RID: 19214 RVA: 0x00160D03 File Offset: 0x0015EF03
		public override DbGeometry GetPointOnSurface(DbGeometry geometryValue)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06004B0F RID: 19215 RVA: 0x00160D0A File Offset: 0x0015EF0A
		public override DbGeometry GetExteriorRing(DbGeometry geometryValue)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06004B10 RID: 19216 RVA: 0x00160D11 File Offset: 0x0015EF11
		public override int? GetInteriorRingCount(DbGeometry geometryValue)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06004B11 RID: 19217 RVA: 0x00160D18 File Offset: 0x0015EF18
		public override DbGeometry InteriorRingAt(DbGeometry geometryValue, int index)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x04001B5A RID: 7002
		internal static readonly DefaultSpatialServices Instance = new DefaultSpatialServices();

		// Token: 0x02000720 RID: 1824
		[Serializable]
		private sealed class ReadOnlySpatialValues
		{
			// Token: 0x06004B13 RID: 19219 RVA: 0x00160D2B File Offset: 0x0015EF2B
			internal ReadOnlySpatialValues(int spatialRefSysId, string textValue, byte[] binaryValue, string gmlValue)
			{
				this.srid = spatialRefSysId;
				this.wkb = ((binaryValue == null) ? null : ((byte[])binaryValue.Clone()));
				this.wkt = textValue;
				this.gml = gmlValue;
			}

			// Token: 0x17000B48 RID: 2888
			// (get) Token: 0x06004B14 RID: 19220 RVA: 0x00160D60 File Offset: 0x0015EF60
			internal int CoordinateSystemId
			{
				get
				{
					return this.srid;
				}
			}

			// Token: 0x06004B15 RID: 19221 RVA: 0x00160D68 File Offset: 0x0015EF68
			internal byte[] CloneBinary()
			{
				if (this.wkb != null)
				{
					return (byte[])this.wkb.Clone();
				}
				return null;
			}

			// Token: 0x17000B49 RID: 2889
			// (get) Token: 0x06004B16 RID: 19222 RVA: 0x00160D84 File Offset: 0x0015EF84
			internal string Text
			{
				get
				{
					return this.wkt;
				}
			}

			// Token: 0x17000B4A RID: 2890
			// (get) Token: 0x06004B17 RID: 19223 RVA: 0x00160D8C File Offset: 0x0015EF8C
			internal string GML
			{
				get
				{
					return this.gml;
				}
			}

			// Token: 0x04001B5B RID: 7003
			private readonly int srid;

			// Token: 0x04001B5C RID: 7004
			private readonly byte[] wkb;

			// Token: 0x04001B5D RID: 7005
			private readonly string wkt;

			// Token: 0x04001B5E RID: 7006
			private readonly string gml;
		}
	}
}
