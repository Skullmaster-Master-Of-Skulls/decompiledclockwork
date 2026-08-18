using System;
using System.Data.Spatial.Internal;

namespace System.Data.Spatial
{
	// Token: 0x020002DB RID: 731
	[Serializable]
	internal sealed class DefaultSpatialServices : DbSpatialServices
	{
		// Token: 0x06002BC1 RID: 11201 RVA: 0x000A807D File Offset: 0x000A627D
		private DefaultSpatialServices()
		{
		}

		// Token: 0x06002BC2 RID: 11202 RVA: 0x000488CA File Offset: 0x00046ACA
		private static Exception SpatialServicesUnavailable()
		{
			return new NotImplementedException();
		}

		// Token: 0x06002BC3 RID: 11203 RVA: 0x000A8088 File Offset: 0x000A6288
		private static DefaultSpatialServices.ReadOnlySpatialValues CheckProviderValue(object providerValue)
		{
			DefaultSpatialServices.ReadOnlySpatialValues readOnlySpatialValues = providerValue as DefaultSpatialServices.ReadOnlySpatialValues;
			if (readOnlySpatialValues == null)
			{
				throw SpatialExceptions.ProviderValueNotCompatibleWithSpatialServices();
			}
			return readOnlySpatialValues;
		}

		// Token: 0x06002BC4 RID: 11204 RVA: 0x000A80A8 File Offset: 0x000A62A8
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
			throw SpatialExceptions.GeographyValueNotCompatibleWithSpatialServices("geographyValue");
		}

		// Token: 0x06002BC5 RID: 11205 RVA: 0x000A80D4 File Offset: 0x000A62D4
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
			throw SpatialExceptions.GeometryValueNotCompatibleWithSpatialServices("geometryValue");
		}

		// Token: 0x06002BC6 RID: 11206 RVA: 0x000A8100 File Offset: 0x000A6300
		public override DbGeography GeographyFromProviderValue(object providerValue)
		{
			providerValue.CheckNull("providerValue");
			DefaultSpatialServices.ReadOnlySpatialValues providerValue2 = DefaultSpatialServices.CheckProviderValue(providerValue);
			return DbSpatialServices.CreateGeography(this, providerValue2);
		}

		// Token: 0x06002BC7 RID: 11207 RVA: 0x000A8126 File Offset: 0x000A6326
		public override object CreateProviderValue(DbGeographyWellKnownValue wellKnownValue)
		{
			wellKnownValue.CheckNull("wellKnownValue");
			return new DefaultSpatialServices.ReadOnlySpatialValues(wellKnownValue.CoordinateSystemId, wellKnownValue.WellKnownText, wellKnownValue.WellKnownBinary, null);
		}

		// Token: 0x06002BC8 RID: 11208 RVA: 0x000A814C File Offset: 0x000A634C
		public override DbGeographyWellKnownValue CreateWellKnownValue(DbGeography geographyValue)
		{
			geographyValue.CheckNull("geographyValue");
			DefaultSpatialServices.ReadOnlySpatialValues readOnlySpatialValues = DefaultSpatialServices.CheckCompatible(geographyValue);
			return new DbGeographyWellKnownValue
			{
				CoordinateSystemId = readOnlySpatialValues.CoordinateSystemId,
				WellKnownBinary = readOnlySpatialValues.CloneBinary(),
				WellKnownText = readOnlySpatialValues.Text
			};
		}

		// Token: 0x06002BC9 RID: 11209 RVA: 0x000A8194 File Offset: 0x000A6394
		public override DbGeography GeographyFromBinary(byte[] geographyBinary)
		{
			geographyBinary.CheckNull("geographyBinary");
			DefaultSpatialServices.ReadOnlySpatialValues providerValue = new DefaultSpatialServices.ReadOnlySpatialValues(DbGeography.DefaultCoordinateSystemId, null, geographyBinary, null);
			return DbSpatialServices.CreateGeography(this, providerValue);
		}

		// Token: 0x06002BCA RID: 11210 RVA: 0x000A81C4 File Offset: 0x000A63C4
		public override DbGeography GeographyFromBinary(byte[] geographyBinary, int spatialReferenceSystemId)
		{
			geographyBinary.CheckNull("geographyBinary");
			DefaultSpatialServices.ReadOnlySpatialValues providerValue = new DefaultSpatialServices.ReadOnlySpatialValues(spatialReferenceSystemId, null, geographyBinary, null);
			return DbSpatialServices.CreateGeography(this, providerValue);
		}

		// Token: 0x06002BCB RID: 11211 RVA: 0x000A81ED File Offset: 0x000A63ED
		public override DbGeography GeographyLineFromBinary(byte[] geographyBinary, int spatialReferenceSystemId)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06002BCC RID: 11212 RVA: 0x000A81ED File Offset: 0x000A63ED
		public override DbGeography GeographyPointFromBinary(byte[] geographyBinary, int spatialReferenceSystemId)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06002BCD RID: 11213 RVA: 0x000A81ED File Offset: 0x000A63ED
		public override DbGeography GeographyPolygonFromBinary(byte[] geographyBinary, int spatialReferenceSystemId)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06002BCE RID: 11214 RVA: 0x000A81ED File Offset: 0x000A63ED
		public override DbGeography GeographyMultiLineFromBinary(byte[] geographyBinary, int spatialReferenceSystemId)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06002BCF RID: 11215 RVA: 0x000A81ED File Offset: 0x000A63ED
		public override DbGeography GeographyMultiPointFromBinary(byte[] geographyBinary, int spatialReferenceSystemId)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06002BD0 RID: 11216 RVA: 0x000A81ED File Offset: 0x000A63ED
		public override DbGeography GeographyMultiPolygonFromBinary(byte[] geographyBinary, int spatialReferenceSystemId)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06002BD1 RID: 11217 RVA: 0x000A81ED File Offset: 0x000A63ED
		public override DbGeography GeographyCollectionFromBinary(byte[] geographyBinary, int spatialReferenceSystemId)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06002BD2 RID: 11218 RVA: 0x000A81F4 File Offset: 0x000A63F4
		public override DbGeography GeographyFromText(string geographyText)
		{
			geographyText.CheckNull("geographyText");
			DefaultSpatialServices.ReadOnlySpatialValues providerValue = new DefaultSpatialServices.ReadOnlySpatialValues(DbGeography.DefaultCoordinateSystemId, geographyText, null, null);
			return DbSpatialServices.CreateGeography(this, providerValue);
		}

		// Token: 0x06002BD3 RID: 11219 RVA: 0x000A8224 File Offset: 0x000A6424
		public override DbGeography GeographyFromText(string geographyText, int spatialReferenceSystemId)
		{
			geographyText.CheckNull("geographyText");
			DefaultSpatialServices.ReadOnlySpatialValues providerValue = new DefaultSpatialServices.ReadOnlySpatialValues(spatialReferenceSystemId, geographyText, null, null);
			return DbSpatialServices.CreateGeography(this, providerValue);
		}

		// Token: 0x06002BD4 RID: 11220 RVA: 0x000A81ED File Offset: 0x000A63ED
		public override DbGeography GeographyLineFromText(string geographyText, int spatialReferenceSystemId)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06002BD5 RID: 11221 RVA: 0x000A81ED File Offset: 0x000A63ED
		public override DbGeography GeographyPointFromText(string geographyText, int spatialReferenceSystemId)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06002BD6 RID: 11222 RVA: 0x000A81ED File Offset: 0x000A63ED
		public override DbGeography GeographyPolygonFromText(string geographyText, int spatialReferenceSystemId)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06002BD7 RID: 11223 RVA: 0x000A81ED File Offset: 0x000A63ED
		public override DbGeography GeographyMultiLineFromText(string geographyText, int spatialReferenceSystemId)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06002BD8 RID: 11224 RVA: 0x000A81ED File Offset: 0x000A63ED
		public override DbGeography GeographyMultiPointFromText(string geographyText, int spatialReferenceSystemId)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06002BD9 RID: 11225 RVA: 0x000A81ED File Offset: 0x000A63ED
		public override DbGeography GeographyMultiPolygonFromText(string geographyText, int spatialReferenceSystemId)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06002BDA RID: 11226 RVA: 0x000A81ED File Offset: 0x000A63ED
		public override DbGeography GeographyCollectionFromText(string geographyText, int spatialReferenceSystemId)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06002BDB RID: 11227 RVA: 0x000A8250 File Offset: 0x000A6450
		public override DbGeography GeographyFromGml(string geographyMarkup)
		{
			geographyMarkup.CheckNull("geographyMarkup");
			DefaultSpatialServices.ReadOnlySpatialValues providerValue = new DefaultSpatialServices.ReadOnlySpatialValues(DbGeography.DefaultCoordinateSystemId, null, null, geographyMarkup);
			return DbSpatialServices.CreateGeography(this, providerValue);
		}

		// Token: 0x06002BDC RID: 11228 RVA: 0x000A8280 File Offset: 0x000A6480
		public override DbGeography GeographyFromGml(string geographyMarkup, int spatialReferenceSystemId)
		{
			geographyMarkup.CheckNull("geographyMarkup");
			DefaultSpatialServices.ReadOnlySpatialValues providerValue = new DefaultSpatialServices.ReadOnlySpatialValues(spatialReferenceSystemId, null, null, geographyMarkup);
			return DbSpatialServices.CreateGeography(this, providerValue);
		}

		// Token: 0x06002BDD RID: 11229 RVA: 0x000A82AC File Offset: 0x000A64AC
		public override int GetCoordinateSystemId(DbGeography geographyValue)
		{
			geographyValue.CheckNull("geographyValue");
			DefaultSpatialServices.ReadOnlySpatialValues readOnlySpatialValues = DefaultSpatialServices.CheckCompatible(geographyValue);
			return readOnlySpatialValues.CoordinateSystemId;
		}

		// Token: 0x06002BDE RID: 11230 RVA: 0x000A81ED File Offset: 0x000A63ED
		public override int GetDimension(DbGeography geographyValue)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06002BDF RID: 11231 RVA: 0x000A81ED File Offset: 0x000A63ED
		public override string GetSpatialTypeName(DbGeography geographyValue)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06002BE0 RID: 11232 RVA: 0x000A81ED File Offset: 0x000A63ED
		public override bool GetIsEmpty(DbGeography geographyValue)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06002BE1 RID: 11233 RVA: 0x000A82D4 File Offset: 0x000A64D4
		public override string AsText(DbGeography geographyValue)
		{
			geographyValue.CheckNull("geographyValue");
			DefaultSpatialServices.ReadOnlySpatialValues readOnlySpatialValues = DefaultSpatialServices.CheckCompatible(geographyValue);
			return readOnlySpatialValues.Text;
		}

		// Token: 0x06002BE2 RID: 11234 RVA: 0x000A82FC File Offset: 0x000A64FC
		public override byte[] AsBinary(DbGeography geographyValue)
		{
			geographyValue.CheckNull("geographyValue");
			DefaultSpatialServices.ReadOnlySpatialValues readOnlySpatialValues = DefaultSpatialServices.CheckCompatible(geographyValue);
			return readOnlySpatialValues.CloneBinary();
		}

		// Token: 0x06002BE3 RID: 11235 RVA: 0x000A8324 File Offset: 0x000A6524
		public override string AsGml(DbGeography geographyValue)
		{
			geographyValue.CheckNull("geographyValue");
			DefaultSpatialServices.ReadOnlySpatialValues readOnlySpatialValues = DefaultSpatialServices.CheckCompatible(geographyValue);
			return readOnlySpatialValues.GML;
		}

		// Token: 0x06002BE4 RID: 11236 RVA: 0x000A81ED File Offset: 0x000A63ED
		public override bool SpatialEquals(DbGeography geographyValue, DbGeography otherGeography)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06002BE5 RID: 11237 RVA: 0x000A81ED File Offset: 0x000A63ED
		public override bool Disjoint(DbGeography geographyValue, DbGeography otherGeography)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06002BE6 RID: 11238 RVA: 0x000A81ED File Offset: 0x000A63ED
		public override bool Intersects(DbGeography geographyValue, DbGeography otherGeography)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06002BE7 RID: 11239 RVA: 0x000A81ED File Offset: 0x000A63ED
		public override DbGeography Buffer(DbGeography geographyValue, double distance)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06002BE8 RID: 11240 RVA: 0x000A81ED File Offset: 0x000A63ED
		public override double Distance(DbGeography geographyValue, DbGeography otherGeography)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06002BE9 RID: 11241 RVA: 0x000A81ED File Offset: 0x000A63ED
		public override DbGeography Intersection(DbGeography geographyValue, DbGeography otherGeography)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06002BEA RID: 11242 RVA: 0x000A81ED File Offset: 0x000A63ED
		public override DbGeography Union(DbGeography geographyValue, DbGeography otherGeography)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06002BEB RID: 11243 RVA: 0x000A81ED File Offset: 0x000A63ED
		public override DbGeography Difference(DbGeography geographyValue, DbGeography otherGeography)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06002BEC RID: 11244 RVA: 0x000A81ED File Offset: 0x000A63ED
		public override DbGeography SymmetricDifference(DbGeography geographyValue, DbGeography otherGeography)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06002BED RID: 11245 RVA: 0x000A81ED File Offset: 0x000A63ED
		public override int? GetElementCount(DbGeography geographyValue)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06002BEE RID: 11246 RVA: 0x000A81ED File Offset: 0x000A63ED
		public override DbGeography ElementAt(DbGeography geographyValue, int index)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06002BEF RID: 11247 RVA: 0x000A81ED File Offset: 0x000A63ED
		public override double? GetLatitude(DbGeography geographyValue)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06002BF0 RID: 11248 RVA: 0x000A81ED File Offset: 0x000A63ED
		public override double? GetLongitude(DbGeography geographyValue)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06002BF1 RID: 11249 RVA: 0x000A81ED File Offset: 0x000A63ED
		public override double? GetElevation(DbGeography geographyValue)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06002BF2 RID: 11250 RVA: 0x000A81ED File Offset: 0x000A63ED
		public override double? GetMeasure(DbGeography geographyValue)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06002BF3 RID: 11251 RVA: 0x000A81ED File Offset: 0x000A63ED
		public override double? GetLength(DbGeography geographyValue)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06002BF4 RID: 11252 RVA: 0x000A81ED File Offset: 0x000A63ED
		public override DbGeography GetEndPoint(DbGeography geographyValue)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06002BF5 RID: 11253 RVA: 0x000A81ED File Offset: 0x000A63ED
		public override DbGeography GetStartPoint(DbGeography geographyValue)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06002BF6 RID: 11254 RVA: 0x000A81ED File Offset: 0x000A63ED
		public override bool? GetIsClosed(DbGeography geographyValue)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06002BF7 RID: 11255 RVA: 0x000A81ED File Offset: 0x000A63ED
		public override int? GetPointCount(DbGeography geographyValue)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06002BF8 RID: 11256 RVA: 0x000A81ED File Offset: 0x000A63ED
		public override DbGeography PointAt(DbGeography geographyValue, int index)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06002BF9 RID: 11257 RVA: 0x000A81ED File Offset: 0x000A63ED
		public override double? GetArea(DbGeography geographyValue)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06002BFA RID: 11258 RVA: 0x000A8349 File Offset: 0x000A6549
		public override object CreateProviderValue(DbGeometryWellKnownValue wellKnownValue)
		{
			wellKnownValue.CheckNull("wellKnownValue");
			return new DefaultSpatialServices.ReadOnlySpatialValues(wellKnownValue.CoordinateSystemId, wellKnownValue.WellKnownText, wellKnownValue.WellKnownBinary, null);
		}

		// Token: 0x06002BFB RID: 11259 RVA: 0x000A8370 File Offset: 0x000A6570
		public override DbGeometryWellKnownValue CreateWellKnownValue(DbGeometry geometryValue)
		{
			geometryValue.CheckNull("geometryValue");
			DefaultSpatialServices.ReadOnlySpatialValues readOnlySpatialValues = DefaultSpatialServices.CheckCompatible(geometryValue);
			return new DbGeometryWellKnownValue
			{
				CoordinateSystemId = readOnlySpatialValues.CoordinateSystemId,
				WellKnownBinary = readOnlySpatialValues.CloneBinary(),
				WellKnownText = readOnlySpatialValues.Text
			};
		}

		// Token: 0x06002BFC RID: 11260 RVA: 0x000A83B8 File Offset: 0x000A65B8
		public override DbGeometry GeometryFromProviderValue(object providerValue)
		{
			providerValue.CheckNull("providerValue");
			DefaultSpatialServices.ReadOnlySpatialValues providerValue2 = DefaultSpatialServices.CheckProviderValue(providerValue);
			return DbSpatialServices.CreateGeometry(this, providerValue2);
		}

		// Token: 0x06002BFD RID: 11261 RVA: 0x000A83E0 File Offset: 0x000A65E0
		public override DbGeometry GeometryFromBinary(byte[] geometryBinary)
		{
			geometryBinary.CheckNull("geometryBinary");
			DefaultSpatialServices.ReadOnlySpatialValues providerValue = new DefaultSpatialServices.ReadOnlySpatialValues(DbGeometry.DefaultCoordinateSystemId, null, geometryBinary, null);
			return DbSpatialServices.CreateGeometry(this, providerValue);
		}

		// Token: 0x06002BFE RID: 11262 RVA: 0x000A8410 File Offset: 0x000A6610
		public override DbGeometry GeometryFromBinary(byte[] geometryBinary, int spatialReferenceSystemId)
		{
			geometryBinary.CheckNull("geometryBinary");
			DefaultSpatialServices.ReadOnlySpatialValues providerValue = new DefaultSpatialServices.ReadOnlySpatialValues(spatialReferenceSystemId, null, geometryBinary, null);
			return DbSpatialServices.CreateGeometry(this, providerValue);
		}

		// Token: 0x06002BFF RID: 11263 RVA: 0x000A81ED File Offset: 0x000A63ED
		public override DbGeometry GeometryLineFromBinary(byte[] geometryBinary, int spatialReferenceSystemId)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06002C00 RID: 11264 RVA: 0x000A81ED File Offset: 0x000A63ED
		public override DbGeometry GeometryPointFromBinary(byte[] geometryBinary, int spatialReferenceSystemId)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06002C01 RID: 11265 RVA: 0x000A81ED File Offset: 0x000A63ED
		public override DbGeometry GeometryPolygonFromBinary(byte[] geometryBinary, int spatialReferenceSystemId)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06002C02 RID: 11266 RVA: 0x000A81ED File Offset: 0x000A63ED
		public override DbGeometry GeometryMultiLineFromBinary(byte[] geometryBinary, int spatialReferenceSystemId)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06002C03 RID: 11267 RVA: 0x000A81ED File Offset: 0x000A63ED
		public override DbGeometry GeometryMultiPointFromBinary(byte[] geometryBinary, int spatialReferenceSystemId)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06002C04 RID: 11268 RVA: 0x000A81ED File Offset: 0x000A63ED
		public override DbGeometry GeometryMultiPolygonFromBinary(byte[] geometryBinary, int spatialReferenceSystemId)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06002C05 RID: 11269 RVA: 0x000A81ED File Offset: 0x000A63ED
		public override DbGeometry GeometryCollectionFromBinary(byte[] geometryBinary, int spatialReferenceSystemId)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06002C06 RID: 11270 RVA: 0x000A843C File Offset: 0x000A663C
		public override DbGeometry GeometryFromText(string geometryText)
		{
			geometryText.CheckNull("geometryText");
			DefaultSpatialServices.ReadOnlySpatialValues providerValue = new DefaultSpatialServices.ReadOnlySpatialValues(DbGeometry.DefaultCoordinateSystemId, geometryText, null, null);
			return DbSpatialServices.CreateGeometry(this, providerValue);
		}

		// Token: 0x06002C07 RID: 11271 RVA: 0x000A846C File Offset: 0x000A666C
		public override DbGeometry GeometryFromText(string geometryText, int spatialReferenceSystemId)
		{
			geometryText.CheckNull("geometryText");
			DefaultSpatialServices.ReadOnlySpatialValues providerValue = new DefaultSpatialServices.ReadOnlySpatialValues(spatialReferenceSystemId, geometryText, null, null);
			return DbSpatialServices.CreateGeometry(this, providerValue);
		}

		// Token: 0x06002C08 RID: 11272 RVA: 0x000A81ED File Offset: 0x000A63ED
		public override DbGeometry GeometryLineFromText(string geometryText, int spatialReferenceSystemId)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06002C09 RID: 11273 RVA: 0x000A81ED File Offset: 0x000A63ED
		public override DbGeometry GeometryPointFromText(string geometryText, int spatialReferenceSystemId)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06002C0A RID: 11274 RVA: 0x000A81ED File Offset: 0x000A63ED
		public override DbGeometry GeometryPolygonFromText(string geometryText, int spatialReferenceSystemId)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06002C0B RID: 11275 RVA: 0x000A81ED File Offset: 0x000A63ED
		public override DbGeometry GeometryMultiLineFromText(string geometryText, int spatialReferenceSystemId)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06002C0C RID: 11276 RVA: 0x000A81ED File Offset: 0x000A63ED
		public override DbGeometry GeometryMultiPointFromText(string geometryText, int spatialReferenceSystemId)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06002C0D RID: 11277 RVA: 0x000A81ED File Offset: 0x000A63ED
		public override DbGeometry GeometryMultiPolygonFromText(string geometryText, int spatialReferenceSystemId)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06002C0E RID: 11278 RVA: 0x000A81ED File Offset: 0x000A63ED
		public override DbGeometry GeometryCollectionFromText(string geometryText, int spatialReferenceSystemId)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06002C0F RID: 11279 RVA: 0x000A8498 File Offset: 0x000A6698
		public override DbGeometry GeometryFromGml(string geometryMarkup)
		{
			geometryMarkup.CheckNull("geometryMarkup");
			DefaultSpatialServices.ReadOnlySpatialValues providerValue = new DefaultSpatialServices.ReadOnlySpatialValues(DbGeometry.DefaultCoordinateSystemId, null, null, geometryMarkup);
			return DbSpatialServices.CreateGeometry(this, providerValue);
		}

		// Token: 0x06002C10 RID: 11280 RVA: 0x000A84C8 File Offset: 0x000A66C8
		public override DbGeometry GeometryFromGml(string geometryMarkup, int spatialReferenceSystemId)
		{
			geometryMarkup.CheckNull("geometryMarkup");
			DefaultSpatialServices.ReadOnlySpatialValues providerValue = new DefaultSpatialServices.ReadOnlySpatialValues(spatialReferenceSystemId, null, null, geometryMarkup);
			return DbSpatialServices.CreateGeometry(this, providerValue);
		}

		// Token: 0x06002C11 RID: 11281 RVA: 0x000A84F4 File Offset: 0x000A66F4
		public override int GetCoordinateSystemId(DbGeometry geometryValue)
		{
			geometryValue.CheckNull("geometryValue");
			DefaultSpatialServices.ReadOnlySpatialValues readOnlySpatialValues = DefaultSpatialServices.CheckCompatible(geometryValue);
			return readOnlySpatialValues.CoordinateSystemId;
		}

		// Token: 0x06002C12 RID: 11282 RVA: 0x000A81ED File Offset: 0x000A63ED
		public override DbGeometry GetBoundary(DbGeometry geometryValue)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06002C13 RID: 11283 RVA: 0x000A81ED File Offset: 0x000A63ED
		public override int GetDimension(DbGeometry geometryValue)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06002C14 RID: 11284 RVA: 0x000A81ED File Offset: 0x000A63ED
		public override DbGeometry GetEnvelope(DbGeometry geometryValue)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06002C15 RID: 11285 RVA: 0x000A81ED File Offset: 0x000A63ED
		public override string GetSpatialTypeName(DbGeometry geometryValue)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06002C16 RID: 11286 RVA: 0x000A81ED File Offset: 0x000A63ED
		public override bool GetIsEmpty(DbGeometry geometryValue)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06002C17 RID: 11287 RVA: 0x000A81ED File Offset: 0x000A63ED
		public override bool GetIsSimple(DbGeometry geometryValue)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06002C18 RID: 11288 RVA: 0x000A81ED File Offset: 0x000A63ED
		public override bool GetIsValid(DbGeometry geometryValue)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06002C19 RID: 11289 RVA: 0x000A851C File Offset: 0x000A671C
		public override string AsText(DbGeometry geometryValue)
		{
			geometryValue.CheckNull("geometryValue");
			DefaultSpatialServices.ReadOnlySpatialValues readOnlySpatialValues = DefaultSpatialServices.CheckCompatible(geometryValue);
			return readOnlySpatialValues.Text;
		}

		// Token: 0x06002C1A RID: 11290 RVA: 0x000A8544 File Offset: 0x000A6744
		public override byte[] AsBinary(DbGeometry geometryValue)
		{
			geometryValue.CheckNull("geometryValue");
			DefaultSpatialServices.ReadOnlySpatialValues readOnlySpatialValues = DefaultSpatialServices.CheckCompatible(geometryValue);
			return readOnlySpatialValues.CloneBinary();
		}

		// Token: 0x06002C1B RID: 11291 RVA: 0x000A856C File Offset: 0x000A676C
		public override string AsGml(DbGeometry geometryValue)
		{
			geometryValue.CheckNull("geometryValue");
			DefaultSpatialServices.ReadOnlySpatialValues readOnlySpatialValues = DefaultSpatialServices.CheckCompatible(geometryValue);
			return readOnlySpatialValues.GML;
		}

		// Token: 0x06002C1C RID: 11292 RVA: 0x000A81ED File Offset: 0x000A63ED
		public override bool SpatialEquals(DbGeometry geometryValue, DbGeometry otherGeometry)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06002C1D RID: 11293 RVA: 0x000A81ED File Offset: 0x000A63ED
		public override bool Disjoint(DbGeometry geometryValue, DbGeometry otherGeometry)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06002C1E RID: 11294 RVA: 0x000A81ED File Offset: 0x000A63ED
		public override bool Intersects(DbGeometry geometryValue, DbGeometry otherGeometry)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06002C1F RID: 11295 RVA: 0x000A81ED File Offset: 0x000A63ED
		public override bool Touches(DbGeometry geometryValue, DbGeometry otherGeometry)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06002C20 RID: 11296 RVA: 0x000A81ED File Offset: 0x000A63ED
		public override bool Crosses(DbGeometry geometryValue, DbGeometry otherGeometry)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06002C21 RID: 11297 RVA: 0x000A81ED File Offset: 0x000A63ED
		public override bool Within(DbGeometry geometryValue, DbGeometry otherGeometry)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06002C22 RID: 11298 RVA: 0x000A81ED File Offset: 0x000A63ED
		public override bool Contains(DbGeometry geometryValue, DbGeometry otherGeometry)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06002C23 RID: 11299 RVA: 0x000A81ED File Offset: 0x000A63ED
		public override bool Overlaps(DbGeometry geometryValue, DbGeometry otherGeometry)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06002C24 RID: 11300 RVA: 0x000A81ED File Offset: 0x000A63ED
		public override bool Relate(DbGeometry geometryValue, DbGeometry otherGeometry, string matrix)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06002C25 RID: 11301 RVA: 0x000A81ED File Offset: 0x000A63ED
		public override DbGeometry Buffer(DbGeometry geometryValue, double distance)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06002C26 RID: 11302 RVA: 0x000A81ED File Offset: 0x000A63ED
		public override double Distance(DbGeometry geometryValue, DbGeometry otherGeometry)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06002C27 RID: 11303 RVA: 0x000A81ED File Offset: 0x000A63ED
		public override DbGeometry GetConvexHull(DbGeometry geometryValue)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06002C28 RID: 11304 RVA: 0x000A81ED File Offset: 0x000A63ED
		public override DbGeometry Intersection(DbGeometry geometryValue, DbGeometry otherGeometry)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06002C29 RID: 11305 RVA: 0x000A81ED File Offset: 0x000A63ED
		public override DbGeometry Union(DbGeometry geometryValue, DbGeometry otherGeometry)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06002C2A RID: 11306 RVA: 0x000A81ED File Offset: 0x000A63ED
		public override DbGeometry Difference(DbGeometry geometryValue, DbGeometry otherGeometry)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06002C2B RID: 11307 RVA: 0x000A81ED File Offset: 0x000A63ED
		public override DbGeometry SymmetricDifference(DbGeometry geometryValue, DbGeometry otherGeometry)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06002C2C RID: 11308 RVA: 0x000A81ED File Offset: 0x000A63ED
		public override int? GetElementCount(DbGeometry geometryValue)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06002C2D RID: 11309 RVA: 0x000A81ED File Offset: 0x000A63ED
		public override DbGeometry ElementAt(DbGeometry geometryValue, int index)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06002C2E RID: 11310 RVA: 0x000A81ED File Offset: 0x000A63ED
		public override double? GetXCoordinate(DbGeometry geometryValue)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06002C2F RID: 11311 RVA: 0x000A81ED File Offset: 0x000A63ED
		public override double? GetYCoordinate(DbGeometry geometryValue)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06002C30 RID: 11312 RVA: 0x000A81ED File Offset: 0x000A63ED
		public override double? GetElevation(DbGeometry geometryValue)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06002C31 RID: 11313 RVA: 0x000A81ED File Offset: 0x000A63ED
		public override double? GetMeasure(DbGeometry geometryValue)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06002C32 RID: 11314 RVA: 0x000A81ED File Offset: 0x000A63ED
		public override double? GetLength(DbGeometry geometryValue)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06002C33 RID: 11315 RVA: 0x000A81ED File Offset: 0x000A63ED
		public override DbGeometry GetEndPoint(DbGeometry geometryValue)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06002C34 RID: 11316 RVA: 0x000A81ED File Offset: 0x000A63ED
		public override DbGeometry GetStartPoint(DbGeometry geometryValue)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06002C35 RID: 11317 RVA: 0x000A81ED File Offset: 0x000A63ED
		public override bool? GetIsClosed(DbGeometry geometryValue)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06002C36 RID: 11318 RVA: 0x000A81ED File Offset: 0x000A63ED
		public override bool? GetIsRing(DbGeometry geometryValue)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06002C37 RID: 11319 RVA: 0x000A81ED File Offset: 0x000A63ED
		public override int? GetPointCount(DbGeometry geometryValue)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06002C38 RID: 11320 RVA: 0x000A81ED File Offset: 0x000A63ED
		public override DbGeometry PointAt(DbGeometry geometryValue, int index)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06002C39 RID: 11321 RVA: 0x000A81ED File Offset: 0x000A63ED
		public override double? GetArea(DbGeometry geometryValue)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06002C3A RID: 11322 RVA: 0x000A81ED File Offset: 0x000A63ED
		public override DbGeometry GetCentroid(DbGeometry geometryValue)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06002C3B RID: 11323 RVA: 0x000A81ED File Offset: 0x000A63ED
		public override DbGeometry GetPointOnSurface(DbGeometry geometryValue)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06002C3C RID: 11324 RVA: 0x000A81ED File Offset: 0x000A63ED
		public override DbGeometry GetExteriorRing(DbGeometry geometryValue)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06002C3D RID: 11325 RVA: 0x000A81ED File Offset: 0x000A63ED
		public override int? GetInteriorRingCount(DbGeometry geometryValue)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x06002C3E RID: 11326 RVA: 0x000A81ED File Offset: 0x000A63ED
		public override DbGeometry InteriorRingAt(DbGeometry geometryValue, int index)
		{
			throw DefaultSpatialServices.SpatialServicesUnavailable();
		}

		// Token: 0x040012FD RID: 4861
		internal static readonly DefaultSpatialServices Instance = new DefaultSpatialServices();

		// Token: 0x02000633 RID: 1587
		[Serializable]
		private sealed class ReadOnlySpatialValues
		{
			// Token: 0x0600437B RID: 17275 RVA: 0x000F5ACB File Offset: 0x000F3CCB
			internal ReadOnlySpatialValues(int spatialRefSysId, string textValue, byte[] binaryValue, string gmlValue)
			{
				this.srid = spatialRefSysId;
				this.wkb = ((binaryValue == null) ? null : ((byte[])binaryValue.Clone()));
				this.wkt = textValue;
				this.gml = gmlValue;
			}

			// Token: 0x17000B9A RID: 2970
			// (get) Token: 0x0600437C RID: 17276 RVA: 0x000F5B00 File Offset: 0x000F3D00
			internal int CoordinateSystemId
			{
				get
				{
					return this.srid;
				}
			}

			// Token: 0x0600437D RID: 17277 RVA: 0x000F5B08 File Offset: 0x000F3D08
			internal byte[] CloneBinary()
			{
				if (this.wkb != null)
				{
					return (byte[])this.wkb.Clone();
				}
				return null;
			}

			// Token: 0x17000B9B RID: 2971
			// (get) Token: 0x0600437E RID: 17278 RVA: 0x000F5B24 File Offset: 0x000F3D24
			internal string Text
			{
				get
				{
					return this.wkt;
				}
			}

			// Token: 0x17000B9C RID: 2972
			// (get) Token: 0x0600437F RID: 17279 RVA: 0x000F5B2C File Offset: 0x000F3D2C
			internal string GML
			{
				get
				{
					return this.gml;
				}
			}

			// Token: 0x04001EB2 RID: 7858
			private readonly int srid;

			// Token: 0x04001EB3 RID: 7859
			private readonly byte[] wkb;

			// Token: 0x04001EB4 RID: 7860
			private readonly string wkt;

			// Token: 0x04001EB5 RID: 7861
			private readonly string gml;
		}
	}
}
