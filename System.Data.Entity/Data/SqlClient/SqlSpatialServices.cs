using System;
using System.Collections.Generic;
using System.Data.Common.Utils;
using System.Data.Spatial;
using System.Data.Spatial.Internal;
using System.Data.SqlClient.Internal;
using System.Reflection;
using System.Runtime.Serialization;

namespace System.Data.SqlClient
{
	// Token: 0x02000026 RID: 38
	[Serializable]
	internal sealed class SqlSpatialServices : DbSpatialServices, ISerializable
	{
		// Token: 0x06000258 RID: 600 RVA: 0x000085AF File Offset: 0x000067AF
		private SqlSpatialServices(Func<SqlTypesAssembly> getSqlTypes)
		{
			this._sqlTypesAssemblySingleton = new Singleton<SqlTypesAssembly>(getSqlTypes);
			this.InitializeMemberInfo();
		}

		// Token: 0x06000259 RID: 601 RVA: 0x000085CC File Offset: 0x000067CC
		private SqlSpatialServices(SerializationInfo info, StreamingContext context)
		{
			SqlSpatialServices instance = SqlSpatialServices.Instance;
			this._sqlTypesAssemblySingleton = instance._sqlTypesAssemblySingleton;
			this.InitializeMemberInfo(instance);
		}

		// Token: 0x0600025A RID: 602 RVA: 0x000085F8 File Offset: 0x000067F8
		private static bool TryGetSpatialServiceFromAssembly(Assembly assembly, out SqlSpatialServices services)
		{
			if (SqlSpatialServices.otherSpatialServices == null || !SqlSpatialServices.otherSpatialServices.TryGetValue(assembly.FullName, out services))
			{
				SqlSpatialServices instance = SqlSpatialServices.Instance;
				lock (instance)
				{
					if (SqlSpatialServices.otherSpatialServices == null || !SqlSpatialServices.otherSpatialServices.TryGetValue(assembly.FullName, out services))
					{
						SqlTypesAssembly sqlAssembly;
						if (SqlTypesAssembly.TryGetSqlTypesAssembly(assembly, out sqlAssembly))
						{
							if (SqlSpatialServices.otherSpatialServices == null)
							{
								SqlSpatialServices.otherSpatialServices = new Dictionary<string, SqlSpatialServices>(1);
							}
							services = new SqlSpatialServices(() => sqlAssembly);
							SqlSpatialServices.otherSpatialServices.Add(assembly.FullName, services);
						}
						else
						{
							services = null;
						}
					}
				}
			}
			return services != null;
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600025B RID: 603 RVA: 0x000086C0 File Offset: 0x000068C0
		private SqlTypesAssembly SqlTypes
		{
			get
			{
				return this._sqlTypesAssemblySingleton.Value;
			}
		}

		// Token: 0x0600025C RID: 604 RVA: 0x000086D0 File Offset: 0x000068D0
		public override object CreateProviderValue(DbGeographyWellKnownValue wellKnownValue)
		{
			wellKnownValue.CheckNull("wellKnownValue");
			object result;
			if (wellKnownValue.WellKnownText != null)
			{
				result = this.SqlTypes.SqlTypesGeographyFromText(wellKnownValue.WellKnownText, wellKnownValue.CoordinateSystemId);
			}
			else
			{
				if (wellKnownValue.WellKnownBinary == null)
				{
					throw SpatialExceptions.WellKnownGeographyValueNotValid("wellKnownValue");
				}
				result = this.SqlTypes.SqlTypesGeographyFromBinary(wellKnownValue.WellKnownBinary, wellKnownValue.CoordinateSystemId);
			}
			return result;
		}

		// Token: 0x0600025D RID: 605 RVA: 0x0000873C File Offset: 0x0000693C
		public override DbGeography GeographyFromProviderValue(object providerValue)
		{
			providerValue.CheckNull("providerValue");
			object obj = this.NormalizeProviderValue(providerValue, this.SqlTypes.SqlGeographyType);
			if (!this.SqlTypes.IsSqlGeographyNull(obj))
			{
				return DbSpatialServices.CreateGeography(this, obj);
			}
			return null;
		}

		// Token: 0x0600025E RID: 606 RVA: 0x00008780 File Offset: 0x00006980
		private object NormalizeProviderValue(object providerValue, Type expectedSpatialType)
		{
			Type type = providerValue.GetType();
			if (type != expectedSpatialType)
			{
				SqlSpatialServices sqlSpatialServices;
				if (SqlSpatialServices.TryGetSpatialServiceFromAssembly(providerValue.GetType().Assembly, out sqlSpatialServices))
				{
					if (expectedSpatialType == this.SqlTypes.SqlGeographyType)
					{
						if (type == sqlSpatialServices.SqlTypes.SqlGeographyType)
						{
							return this.ConvertToSqlValue(sqlSpatialServices.GeographyFromProviderValue(providerValue), "providerValue");
						}
					}
					else if (type == sqlSpatialServices.SqlTypes.SqlGeometryType)
					{
						return this.ConvertToSqlValue(sqlSpatialServices.GeometryFromProviderValue(providerValue), "providerValue");
					}
				}
				throw SpatialExceptions.SqlSpatialServices_ProviderValueNotSqlType(expectedSpatialType);
			}
			return providerValue;
		}

		// Token: 0x0600025F RID: 607 RVA: 0x00008818 File Offset: 0x00006A18
		public override DbGeographyWellKnownValue CreateWellKnownValue(DbGeography geographyValue)
		{
			geographyValue.CheckNull("geographyValue");
			IDbSpatialValue spatialValue = geographyValue.AsSpatialValue();
			return SqlSpatialServices.CreateWellKnownValue<DbGeographyWellKnownValue>(spatialValue, () => SpatialExceptions.CouldNotCreateWellKnownGeographyValueNoSrid("geographyValue"), () => SpatialExceptions.CouldNotCreateWellKnownGeographyValueNoWkbOrWkt("geographyValue"), (int srid, byte[] wkb, string wkt) => new DbGeographyWellKnownValue
			{
				CoordinateSystemId = srid,
				WellKnownBinary = wkb,
				WellKnownText = wkt
			});
		}

		// Token: 0x06000260 RID: 608 RVA: 0x0000889C File Offset: 0x00006A9C
		public override object CreateProviderValue(DbGeometryWellKnownValue wellKnownValue)
		{
			wellKnownValue.CheckNull("wellKnownValue");
			object result;
			if (wellKnownValue.WellKnownText != null)
			{
				result = this.SqlTypes.SqlTypesGeometryFromText(wellKnownValue.WellKnownText, wellKnownValue.CoordinateSystemId);
			}
			else
			{
				if (wellKnownValue.WellKnownBinary == null)
				{
					throw SpatialExceptions.WellKnownGeometryValueNotValid("wellKnownValue");
				}
				result = this.SqlTypes.SqlTypesGeometryFromBinary(wellKnownValue.WellKnownBinary, wellKnownValue.CoordinateSystemId);
			}
			return result;
		}

		// Token: 0x06000261 RID: 609 RVA: 0x00008908 File Offset: 0x00006B08
		public override DbGeometry GeometryFromProviderValue(object providerValue)
		{
			providerValue.CheckNull("providerValue");
			object obj = this.NormalizeProviderValue(providerValue, this.SqlTypes.SqlGeometryType);
			if (!this.SqlTypes.IsSqlGeometryNull(obj))
			{
				return DbSpatialServices.CreateGeometry(this, obj);
			}
			return null;
		}

		// Token: 0x06000262 RID: 610 RVA: 0x0000894C File Offset: 0x00006B4C
		public override DbGeometryWellKnownValue CreateWellKnownValue(DbGeometry geometryValue)
		{
			geometryValue.CheckNull("geometryValue");
			IDbSpatialValue spatialValue = geometryValue.AsSpatialValue();
			return SqlSpatialServices.CreateWellKnownValue<DbGeometryWellKnownValue>(spatialValue, () => SpatialExceptions.CouldNotCreateWellKnownGeometryValueNoSrid("geometryValue"), () => SpatialExceptions.CouldNotCreateWellKnownGeometryValueNoWkbOrWkt("geometryValue"), (int srid, byte[] wkb, string wkt) => new DbGeometryWellKnownValue
			{
				CoordinateSystemId = srid,
				WellKnownBinary = wkb,
				WellKnownText = wkt
			});
		}

		// Token: 0x06000263 RID: 611 RVA: 0x000089D0 File Offset: 0x00006BD0
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
		}

		// Token: 0x06000264 RID: 612 RVA: 0x000089D4 File Offset: 0x00006BD4
		private static TValue CreateWellKnownValue<TValue>(IDbSpatialValue spatialValue, Func<Exception> onMissingSrid, Func<Exception> onMissingWkbAndWkt, Func<int, byte[], string, TValue> onValidValue)
		{
			int? coordinateSystemId = spatialValue.CoordinateSystemId;
			if (coordinateSystemId == null)
			{
				throw onMissingSrid();
			}
			string wellKnownText = spatialValue.WellKnownText;
			if (wellKnownText != null)
			{
				return onValidValue(coordinateSystemId.Value, null, wellKnownText);
			}
			byte[] wellKnownBinary = spatialValue.WellKnownBinary;
			if (wellKnownBinary != null)
			{
				return onValidValue(coordinateSystemId.Value, wellKnownBinary, null);
			}
			throw onMissingWkbAndWkt();
		}

		// Token: 0x06000265 RID: 613 RVA: 0x00008A32 File Offset: 0x00006C32
		public override string AsTextIncludingElevationAndMeasure(DbGeography geographyValue)
		{
			return this.SqlTypes.GeographyAsTextZM(geographyValue);
		}

		// Token: 0x06000266 RID: 614 RVA: 0x00008A40 File Offset: 0x00006C40
		public override string AsTextIncludingElevationAndMeasure(DbGeometry geometryValue)
		{
			return this.SqlTypes.GeometryAsTextZM(geometryValue);
		}

		// Token: 0x06000267 RID: 615 RVA: 0x00008A4E File Offset: 0x00006C4E
		private MethodInfo FindSqlGeographyMethod(string methodName, params Type[] argTypes)
		{
			return this.SqlTypes.SqlGeographyType.GetMethod(methodName, BindingFlags.Instance | BindingFlags.Public, null, argTypes, null);
		}

		// Token: 0x06000268 RID: 616 RVA: 0x00008A66 File Offset: 0x00006C66
		private MethodInfo FindSqlGeographyStaticMethod(string methodName, params Type[] argTypes)
		{
			return this.SqlTypes.SqlGeographyType.GetMethod(methodName, BindingFlags.Static | BindingFlags.Public, null, argTypes, null);
		}

		// Token: 0x06000269 RID: 617 RVA: 0x00008A7E File Offset: 0x00006C7E
		private PropertyInfo FindSqlGeographyProperty(string propertyName)
		{
			return this.SqlTypes.SqlGeographyType.GetProperty(propertyName, BindingFlags.Instance | BindingFlags.Public);
		}

		// Token: 0x0600026A RID: 618 RVA: 0x00008A93 File Offset: 0x00006C93
		private MethodInfo FindSqlGeometryStaticMethod(string methodName, params Type[] argTypes)
		{
			return this.SqlTypes.SqlGeometryType.GetMethod(methodName, BindingFlags.Static | BindingFlags.Public, null, argTypes, null);
		}

		// Token: 0x0600026B RID: 619 RVA: 0x00008AAB File Offset: 0x00006CAB
		private MethodInfo FindSqlGeometryMethod(string methodName, params Type[] argTypes)
		{
			return this.SqlTypes.SqlGeometryType.GetMethod(methodName, BindingFlags.Instance | BindingFlags.Public, null, argTypes, null);
		}

		// Token: 0x0600026C RID: 620 RVA: 0x00008AC3 File Offset: 0x00006CC3
		private PropertyInfo FindSqlGeometryProperty(string propertyName)
		{
			return this.SqlTypes.SqlGeometryType.GetProperty(propertyName, BindingFlags.Instance | BindingFlags.Public);
		}

		// Token: 0x0600026D RID: 621 RVA: 0x00008AD8 File Offset: 0x00006CD8
		private object ConvertToSqlValue(DbGeography geographyValue, string argumentName)
		{
			if (geographyValue == null)
			{
				return null;
			}
			return this.SqlTypes.ConvertToSqlTypesGeography(geographyValue);
		}

		// Token: 0x0600026E RID: 622 RVA: 0x00008AEB File Offset: 0x00006CEB
		private object ConvertToSqlValue(DbGeometry geometryValue, string argumentName)
		{
			if (geometryValue == null)
			{
				return null;
			}
			return this.SqlTypes.ConvertToSqlTypesGeometry(geometryValue);
		}

		// Token: 0x0600026F RID: 623 RVA: 0x00008AFE File Offset: 0x00006CFE
		private object ConvertToSqlBytes(byte[] binaryValue, string argumentName)
		{
			if (binaryValue == null)
			{
				return null;
			}
			return this.SqlTypes.SqlBytesFromByteArray(binaryValue);
		}

		// Token: 0x06000270 RID: 624 RVA: 0x00008B11 File Offset: 0x00006D11
		private object ConvertToSqlChars(string stringValue, string argumentName)
		{
			if (stringValue == null)
			{
				return null;
			}
			return this.SqlTypes.SqlCharsFromString(stringValue);
		}

		// Token: 0x06000271 RID: 625 RVA: 0x00008B24 File Offset: 0x00006D24
		private object ConvertToSqlString(string stringValue, string argumentName)
		{
			if (stringValue == null)
			{
				return null;
			}
			return this.SqlTypes.SqlStringFromString(stringValue);
		}

		// Token: 0x06000272 RID: 626 RVA: 0x00008B37 File Offset: 0x00006D37
		private object ConvertToSqlXml(string stringValue, string argumentName)
		{
			if (stringValue == null)
			{
				return null;
			}
			return this.SqlTypes.SqlXmlFromString(stringValue);
		}

		// Token: 0x06000273 RID: 627 RVA: 0x00008B4A File Offset: 0x00006D4A
		private bool ConvertSqlBooleanToBoolean(object sqlBoolean)
		{
			return this.SqlTypes.SqlBooleanToBoolean(sqlBoolean);
		}

		// Token: 0x06000274 RID: 628 RVA: 0x00008B58 File Offset: 0x00006D58
		private bool? ConvertSqlBooleanToNullableBoolean(object sqlBoolean)
		{
			return this.SqlTypes.SqlBooleanToNullableBoolean(sqlBoolean);
		}

		// Token: 0x06000275 RID: 629 RVA: 0x00008B66 File Offset: 0x00006D66
		private byte[] ConvertSqlBytesToBinary(object sqlBytes)
		{
			return this.SqlTypes.SqlBytesToByteArray(sqlBytes);
		}

		// Token: 0x06000276 RID: 630 RVA: 0x00008B74 File Offset: 0x00006D74
		private string ConvertSqlCharsToString(object sqlCharsValue)
		{
			return this.SqlTypes.SqlCharsToString(sqlCharsValue);
		}

		// Token: 0x06000277 RID: 631 RVA: 0x00008B82 File Offset: 0x00006D82
		private string ConvertSqlStringToString(object sqlCharsValue)
		{
			return this.SqlTypes.SqlStringToString(sqlCharsValue);
		}

		// Token: 0x06000278 RID: 632 RVA: 0x00008B90 File Offset: 0x00006D90
		private double ConvertSqlDoubleToDouble(object sqlDoubleValue)
		{
			return this.SqlTypes.SqlDoubleToDouble(sqlDoubleValue);
		}

		// Token: 0x06000279 RID: 633 RVA: 0x00008B9E File Offset: 0x00006D9E
		private double? ConvertSqlDoubleToNullableDouble(object sqlDoubleValue)
		{
			return this.SqlTypes.SqlDoubleToNullableDouble(sqlDoubleValue);
		}

		// Token: 0x0600027A RID: 634 RVA: 0x00008BAC File Offset: 0x00006DAC
		private int ConvertSqlInt32ToInt(object sqlInt32Value)
		{
			return this.SqlTypes.SqlInt32ToInt(sqlInt32Value);
		}

		// Token: 0x0600027B RID: 635 RVA: 0x00008BBA File Offset: 0x00006DBA
		private int? ConvertSqlInt32ToNullableInt(object sqlInt32Value)
		{
			return this.SqlTypes.SqlInt32ToNullableInt(sqlInt32Value);
		}

		// Token: 0x0600027C RID: 636 RVA: 0x00008BC8 File Offset: 0x00006DC8
		private string ConvertSqlXmlToString(object sqlXmlValue)
		{
			return this.SqlTypes.SqlXmlToString(sqlXmlValue);
		}

		// Token: 0x0600027D RID: 637 RVA: 0x00008BD8 File Offset: 0x00006DD8
		public override DbGeography GeographyFromText(string geographyText)
		{
			object obj = this.ConvertToSqlString(geographyText, "geographyText");
			object providerValue = this.smi_SqlGeography_Parse.Value.Invoke(null, new object[]
			{
				obj
			});
			return this.GeographyFromProviderValue(providerValue);
		}

		// Token: 0x0600027E RID: 638 RVA: 0x00008C18 File Offset: 0x00006E18
		public override DbGeography GeographyFromText(string geographyText, int srid)
		{
			object obj = this.ConvertToSqlChars(geographyText, "geographyText");
			object providerValue = this.smi_SqlGeography_STGeomFromText.Value.Invoke(null, new object[]
			{
				obj,
				srid
			});
			return this.GeographyFromProviderValue(providerValue);
		}

		// Token: 0x0600027F RID: 639 RVA: 0x00008C60 File Offset: 0x00006E60
		public override DbGeography GeographyPointFromText(string pointText, int srid)
		{
			object obj = this.ConvertToSqlChars(pointText, "pointText");
			object providerValue = this.smi_SqlGeography_STPointFromText.Value.Invoke(null, new object[]
			{
				obj,
				srid
			});
			return this.GeographyFromProviderValue(providerValue);
		}

		// Token: 0x06000280 RID: 640 RVA: 0x00008CA8 File Offset: 0x00006EA8
		public override DbGeography GeographyLineFromText(string lineText, int srid)
		{
			object obj = this.ConvertToSqlChars(lineText, "lineText");
			object providerValue = this.smi_SqlGeography_STLineFromText.Value.Invoke(null, new object[]
			{
				obj,
				srid
			});
			return this.GeographyFromProviderValue(providerValue);
		}

		// Token: 0x06000281 RID: 641 RVA: 0x00008CF0 File Offset: 0x00006EF0
		public override DbGeography GeographyPolygonFromText(string polygonText, int srid)
		{
			object obj = this.ConvertToSqlChars(polygonText, "polygonText");
			object providerValue = this.smi_SqlGeography_STPolyFromText.Value.Invoke(null, new object[]
			{
				obj,
				srid
			});
			return this.GeographyFromProviderValue(providerValue);
		}

		// Token: 0x06000282 RID: 642 RVA: 0x00008D38 File Offset: 0x00006F38
		public override DbGeography GeographyMultiPointFromText(string multiPointText, int srid)
		{
			object obj = this.ConvertToSqlChars(multiPointText, "multiPointText");
			object providerValue = this.smi_SqlGeography_STMPointFromText.Value.Invoke(null, new object[]
			{
				obj,
				srid
			});
			return this.GeographyFromProviderValue(providerValue);
		}

		// Token: 0x06000283 RID: 643 RVA: 0x00008D80 File Offset: 0x00006F80
		public override DbGeography GeographyMultiLineFromText(string multiLineText, int srid)
		{
			object obj = this.ConvertToSqlChars(multiLineText, "multiLineText");
			object providerValue = this.smi_SqlGeography_STMLineFromText.Value.Invoke(null, new object[]
			{
				obj,
				srid
			});
			return this.GeographyFromProviderValue(providerValue);
		}

		// Token: 0x06000284 RID: 644 RVA: 0x00008DC8 File Offset: 0x00006FC8
		public override DbGeography GeographyMultiPolygonFromText(string multiPolygonText, int srid)
		{
			object obj = this.ConvertToSqlChars(multiPolygonText, "multiPolygonText");
			object providerValue = this.smi_SqlGeography_STMPolyFromText.Value.Invoke(null, new object[]
			{
				obj,
				srid
			});
			return this.GeographyFromProviderValue(providerValue);
		}

		// Token: 0x06000285 RID: 645 RVA: 0x00008E10 File Offset: 0x00007010
		public override DbGeography GeographyCollectionFromText(string geographyCollectionText, int srid)
		{
			object obj = this.ConvertToSqlChars(geographyCollectionText, "geographyCollectionText");
			object providerValue = this.smi_SqlGeography_STGeomCollFromText.Value.Invoke(null, new object[]
			{
				obj,
				srid
			});
			return this.GeographyFromProviderValue(providerValue);
		}

		// Token: 0x06000286 RID: 646 RVA: 0x00008E58 File Offset: 0x00007058
		public override DbGeography GeographyFromBinary(byte[] geographyBytes, int srid)
		{
			object obj = this.ConvertToSqlBytes(geographyBytes, "geographyBytes");
			object providerValue = this.smi_SqlGeography_STGeomFromWKB.Value.Invoke(null, new object[]
			{
				obj,
				srid
			});
			return this.GeographyFromProviderValue(providerValue);
		}

		// Token: 0x06000287 RID: 647 RVA: 0x00008EA0 File Offset: 0x000070A0
		public override DbGeography GeographyFromBinary(byte[] geographyBytes)
		{
			object obj = this.ConvertToSqlBytes(geographyBytes, "geographyBytes");
			object providerValue = this.smi_SqlGeography_STGeomFromWKB.Value.Invoke(null, new object[]
			{
				obj,
				4326
			});
			return this.GeographyFromProviderValue(providerValue);
		}

		// Token: 0x06000288 RID: 648 RVA: 0x00008EEC File Offset: 0x000070EC
		public override DbGeography GeographyPointFromBinary(byte[] pointBytes, int srid)
		{
			object obj = this.ConvertToSqlBytes(pointBytes, "pointBytes");
			object providerValue = this.smi_SqlGeography_STPointFromWKB.Value.Invoke(null, new object[]
			{
				obj,
				srid
			});
			return this.GeographyFromProviderValue(providerValue);
		}

		// Token: 0x06000289 RID: 649 RVA: 0x00008F34 File Offset: 0x00007134
		public override DbGeography GeographyLineFromBinary(byte[] lineBytes, int srid)
		{
			object obj = this.ConvertToSqlBytes(lineBytes, "lineBytes");
			object providerValue = this.smi_SqlGeography_STLineFromWKB.Value.Invoke(null, new object[]
			{
				obj,
				srid
			});
			return this.GeographyFromProviderValue(providerValue);
		}

		// Token: 0x0600028A RID: 650 RVA: 0x00008F7C File Offset: 0x0000717C
		public override DbGeography GeographyPolygonFromBinary(byte[] polygonBytes, int srid)
		{
			object obj = this.ConvertToSqlBytes(polygonBytes, "polygonBytes");
			object providerValue = this.smi_SqlGeography_STPolyFromWKB.Value.Invoke(null, new object[]
			{
				obj,
				srid
			});
			return this.GeographyFromProviderValue(providerValue);
		}

		// Token: 0x0600028B RID: 651 RVA: 0x00008FC4 File Offset: 0x000071C4
		public override DbGeography GeographyMultiPointFromBinary(byte[] multiPointBytes, int srid)
		{
			object obj = this.ConvertToSqlBytes(multiPointBytes, "multiPointBytes");
			object providerValue = this.smi_SqlGeography_STMPointFromWKB.Value.Invoke(null, new object[]
			{
				obj,
				srid
			});
			return this.GeographyFromProviderValue(providerValue);
		}

		// Token: 0x0600028C RID: 652 RVA: 0x0000900C File Offset: 0x0000720C
		public override DbGeography GeographyMultiLineFromBinary(byte[] multiLineBytes, int srid)
		{
			object obj = this.ConvertToSqlBytes(multiLineBytes, "multiLineBytes");
			object providerValue = this.smi_SqlGeography_STMLineFromWKB.Value.Invoke(null, new object[]
			{
				obj,
				srid
			});
			return this.GeographyFromProviderValue(providerValue);
		}

		// Token: 0x0600028D RID: 653 RVA: 0x00009054 File Offset: 0x00007254
		public override DbGeography GeographyMultiPolygonFromBinary(byte[] multiPolygonBytes, int srid)
		{
			object obj = this.ConvertToSqlBytes(multiPolygonBytes, "multiPolygonBytes");
			object providerValue = this.smi_SqlGeography_STMPolyFromWKB.Value.Invoke(null, new object[]
			{
				obj,
				srid
			});
			return this.GeographyFromProviderValue(providerValue);
		}

		// Token: 0x0600028E RID: 654 RVA: 0x0000909C File Offset: 0x0000729C
		public override DbGeography GeographyCollectionFromBinary(byte[] geographyCollectionBytes, int srid)
		{
			object obj = this.ConvertToSqlBytes(geographyCollectionBytes, "geographyCollectionBytes");
			object providerValue = this.smi_SqlGeography_STGeomCollFromWKB.Value.Invoke(null, new object[]
			{
				obj,
				srid
			});
			return this.GeographyFromProviderValue(providerValue);
		}

		// Token: 0x0600028F RID: 655 RVA: 0x000090E4 File Offset: 0x000072E4
		public override DbGeography GeographyFromGml(string geographyGml)
		{
			object obj = this.ConvertToSqlXml(geographyGml, "geographyGml");
			object providerValue = this.smi_SqlGeography_GeomFromGml.Value.Invoke(null, new object[]
			{
				obj,
				4326
			});
			return this.GeographyFromProviderValue(providerValue);
		}

		// Token: 0x06000290 RID: 656 RVA: 0x00009130 File Offset: 0x00007330
		public override DbGeography GeographyFromGml(string geographyGml, int srid)
		{
			object obj = this.ConvertToSqlXml(geographyGml, "geographyGml");
			object providerValue = this.smi_SqlGeography_GeomFromGml.Value.Invoke(null, new object[]
			{
				obj,
				srid
			});
			return this.GeographyFromProviderValue(providerValue);
		}

		// Token: 0x06000291 RID: 657 RVA: 0x00009178 File Offset: 0x00007378
		public override int GetCoordinateSystemId(DbGeography geographyValue)
		{
			geographyValue.CheckNull("geographyValue");
			object obj = this.ConvertToSqlValue(geographyValue, "geographyValue");
			object value = this.ipi_SqlGeography_STSrid.Value.GetValue(obj, null);
			return this.ConvertSqlInt32ToInt(value);
		}

		// Token: 0x06000292 RID: 658 RVA: 0x000091B8 File Offset: 0x000073B8
		public override string GetSpatialTypeName(DbGeography geographyValue)
		{
			geographyValue.CheckNull("geographyValue");
			object obj = this.ConvertToSqlValue(geographyValue, "geographyValue");
			object sqlCharsValue = this.imi_SqlGeography_STGeometryType.Value.Invoke(obj, new object[0]);
			return this.ConvertSqlStringToString(sqlCharsValue);
		}

		// Token: 0x06000293 RID: 659 RVA: 0x000091FC File Offset: 0x000073FC
		public override int GetDimension(DbGeography geographyValue)
		{
			geographyValue.CheckNull("geographyValue");
			object obj = this.ConvertToSqlValue(geographyValue, "geographyValue");
			object sqlInt32Value = this.imi_SqlGeography_STDimension.Value.Invoke(obj, new object[0]);
			return this.ConvertSqlInt32ToInt(sqlInt32Value);
		}

		// Token: 0x06000294 RID: 660 RVA: 0x00009240 File Offset: 0x00007440
		public override byte[] AsBinary(DbGeography geographyValue)
		{
			geographyValue.CheckNull("geographyValue");
			object obj = this.ConvertToSqlValue(geographyValue, "geographyValue");
			object sqlBytes = this.imi_SqlGeography_STAsBinary.Value.Invoke(obj, new object[0]);
			return this.ConvertSqlBytesToBinary(sqlBytes);
		}

		// Token: 0x06000295 RID: 661 RVA: 0x00009284 File Offset: 0x00007484
		public override string AsGml(DbGeography geographyValue)
		{
			geographyValue.CheckNull("geographyValue");
			object obj = this.ConvertToSqlValue(geographyValue, "geographyValue");
			object sqlXmlValue = this.imi_SqlGeography_AsGml.Value.Invoke(obj, new object[0]);
			return this.ConvertSqlXmlToString(sqlXmlValue);
		}

		// Token: 0x06000296 RID: 662 RVA: 0x000092C8 File Offset: 0x000074C8
		public override string AsText(DbGeography geographyValue)
		{
			geographyValue.CheckNull("geographyValue");
			object obj = this.ConvertToSqlValue(geographyValue, "geographyValue");
			object sqlCharsValue = this.imi_SqlGeography_STAsText.Value.Invoke(obj, new object[0]);
			return this.ConvertSqlCharsToString(sqlCharsValue);
		}

		// Token: 0x06000297 RID: 663 RVA: 0x0000930C File Offset: 0x0000750C
		public override bool GetIsEmpty(DbGeography geographyValue)
		{
			geographyValue.CheckNull("geographyValue");
			object obj = this.ConvertToSqlValue(geographyValue, "geographyValue");
			object sqlBoolean = this.imi_SqlGeography_STIsEmpty.Value.Invoke(obj, new object[0]);
			return this.ConvertSqlBooleanToBoolean(sqlBoolean);
		}

		// Token: 0x06000298 RID: 664 RVA: 0x00009350 File Offset: 0x00007550
		public override bool SpatialEquals(DbGeography geographyValue1, DbGeography geographyValue2)
		{
			geographyValue1.CheckNull("geographyValue1");
			object obj = this.ConvertToSqlValue(geographyValue1, "geographyValue1");
			object obj2 = this.ConvertToSqlValue(geographyValue2, "geographyValue2");
			object sqlBoolean = this.imi_SqlGeography_STEquals.Value.Invoke(obj, new object[]
			{
				obj2
			});
			return this.ConvertSqlBooleanToBoolean(sqlBoolean);
		}

		// Token: 0x06000299 RID: 665 RVA: 0x000093A8 File Offset: 0x000075A8
		public override bool Disjoint(DbGeography geographyValue1, DbGeography geographyValue2)
		{
			geographyValue1.CheckNull("geographyValue1");
			object obj = this.ConvertToSqlValue(geographyValue1, "geographyValue1");
			object obj2 = this.ConvertToSqlValue(geographyValue2, "geographyValue2");
			object sqlBoolean = this.imi_SqlGeography_STDisjoint.Value.Invoke(obj, new object[]
			{
				obj2
			});
			return this.ConvertSqlBooleanToBoolean(sqlBoolean);
		}

		// Token: 0x0600029A RID: 666 RVA: 0x00009400 File Offset: 0x00007600
		public override bool Intersects(DbGeography geographyValue1, DbGeography geographyValue2)
		{
			geographyValue1.CheckNull("geographyValue1");
			object obj = this.ConvertToSqlValue(geographyValue1, "geographyValue1");
			object obj2 = this.ConvertToSqlValue(geographyValue2, "geographyValue2");
			object sqlBoolean = this.imi_SqlGeography_STIntersects.Value.Invoke(obj, new object[]
			{
				obj2
			});
			return this.ConvertSqlBooleanToBoolean(sqlBoolean);
		}

		// Token: 0x0600029B RID: 667 RVA: 0x00009458 File Offset: 0x00007658
		public override DbGeography Buffer(DbGeography geographyValue, double distance)
		{
			geographyValue.CheckNull("geographyValue");
			object obj = this.ConvertToSqlValue(geographyValue, "geographyValue");
			object providerValue = this.imi_SqlGeography_STBuffer.Value.Invoke(obj, new object[]
			{
				distance
			});
			return this.GeographyFromProviderValue(providerValue);
		}

		// Token: 0x0600029C RID: 668 RVA: 0x000094A8 File Offset: 0x000076A8
		public override double Distance(DbGeography geographyValue1, DbGeography geographyValue2)
		{
			geographyValue1.CheckNull("geographyValue1");
			object obj = this.ConvertToSqlValue(geographyValue1, "geographyValue1");
			object obj2 = this.ConvertToSqlValue(geographyValue2, "geographyValue2");
			object sqlDoubleValue = this.imi_SqlGeography_STDistance.Value.Invoke(obj, new object[]
			{
				obj2
			});
			return this.ConvertSqlDoubleToDouble(sqlDoubleValue);
		}

		// Token: 0x0600029D RID: 669 RVA: 0x00009500 File Offset: 0x00007700
		public override DbGeography Intersection(DbGeography geographyValue1, DbGeography geographyValue2)
		{
			geographyValue1.CheckNull("geographyValue1");
			object obj = this.ConvertToSqlValue(geographyValue1, "geographyValue1");
			object obj2 = this.ConvertToSqlValue(geographyValue2, "geographyValue2");
			object providerValue = this.imi_SqlGeography_STIntersection.Value.Invoke(obj, new object[]
			{
				obj2
			});
			return this.GeographyFromProviderValue(providerValue);
		}

		// Token: 0x0600029E RID: 670 RVA: 0x00009558 File Offset: 0x00007758
		public override DbGeography Union(DbGeography geographyValue1, DbGeography geographyValue2)
		{
			geographyValue1.CheckNull("geographyValue1");
			object obj = this.ConvertToSqlValue(geographyValue1, "geographyValue1");
			object obj2 = this.ConvertToSqlValue(geographyValue2, "geographyValue2");
			object providerValue = this.imi_SqlGeography_STUnion.Value.Invoke(obj, new object[]
			{
				obj2
			});
			return this.GeographyFromProviderValue(providerValue);
		}

		// Token: 0x0600029F RID: 671 RVA: 0x000095B0 File Offset: 0x000077B0
		public override DbGeography Difference(DbGeography geometryValue1, DbGeography geometryValue2)
		{
			geometryValue1.CheckNull("geometryValue1");
			object obj = this.ConvertToSqlValue(geometryValue1, "geometryValue1");
			object obj2 = this.ConvertToSqlValue(geometryValue2, "geometryValue2");
			object providerValue = this.imi_SqlGeography_STDifference.Value.Invoke(obj, new object[]
			{
				obj2
			});
			return this.GeographyFromProviderValue(providerValue);
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x00009608 File Offset: 0x00007808
		public override DbGeography SymmetricDifference(DbGeography geometryValue1, DbGeography geometryValue2)
		{
			geometryValue1.CheckNull("geometryValue1");
			object obj = this.ConvertToSqlValue(geometryValue1, "geometryValue1");
			object obj2 = this.ConvertToSqlValue(geometryValue2, "geometryValue2");
			object providerValue = this.imi_SqlGeography_STSymDifference.Value.Invoke(obj, new object[]
			{
				obj2
			});
			return this.GeographyFromProviderValue(providerValue);
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x00009660 File Offset: 0x00007860
		public override int? GetElementCount(DbGeography geographyValue)
		{
			geographyValue.CheckNull("geographyValue");
			object obj = this.ConvertToSqlValue(geographyValue, "geographyValue");
			object sqlInt32Value = this.imi_SqlGeography_STNumGeometries.Value.Invoke(obj, new object[0]);
			return this.ConvertSqlInt32ToNullableInt(sqlInt32Value);
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x000096A4 File Offset: 0x000078A4
		public override DbGeography ElementAt(DbGeography geographyValue, int nValue)
		{
			geographyValue.CheckNull("geographyValue");
			object obj = this.ConvertToSqlValue(geographyValue, "geographyValue");
			object providerValue = this.imi_SqlGeography_STGeometryN.Value.Invoke(obj, new object[]
			{
				nValue
			});
			return this.GeographyFromProviderValue(providerValue);
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x000096F4 File Offset: 0x000078F4
		public override double? GetLatitude(DbGeography geographyValue)
		{
			geographyValue.CheckNull("geographyValue");
			object obj = this.ConvertToSqlValue(geographyValue, "geographyValue");
			object value = this.ipi_SqlGeography_Lat.Value.GetValue(obj, null);
			return this.ConvertSqlDoubleToNullableDouble(value);
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x00009734 File Offset: 0x00007934
		public override double? GetLongitude(DbGeography geographyValue)
		{
			geographyValue.CheckNull("geographyValue");
			object obj = this.ConvertToSqlValue(geographyValue, "geographyValue");
			object value = this.ipi_SqlGeography_Long.Value.GetValue(obj, null);
			return this.ConvertSqlDoubleToNullableDouble(value);
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x00009774 File Offset: 0x00007974
		public override double? GetElevation(DbGeography geographyValue)
		{
			geographyValue.CheckNull("geographyValue");
			object obj = this.ConvertToSqlValue(geographyValue, "geographyValue");
			object value = this.ipi_SqlGeography_Z.Value.GetValue(obj, null);
			return this.ConvertSqlDoubleToNullableDouble(value);
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x000097B4 File Offset: 0x000079B4
		public override double? GetMeasure(DbGeography geographyValue)
		{
			geographyValue.CheckNull("geographyValue");
			object obj = this.ConvertToSqlValue(geographyValue, "geographyValue");
			object value = this.ipi_SqlGeography_M.Value.GetValue(obj, null);
			return this.ConvertSqlDoubleToNullableDouble(value);
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x000097F4 File Offset: 0x000079F4
		public override double? GetLength(DbGeography geographyValue)
		{
			geographyValue.CheckNull("geographyValue");
			object obj = this.ConvertToSqlValue(geographyValue, "geographyValue");
			object sqlDoubleValue = this.imi_SqlGeography_STLength.Value.Invoke(obj, new object[0]);
			return this.ConvertSqlDoubleToNullableDouble(sqlDoubleValue);
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x00009838 File Offset: 0x00007A38
		public override DbGeography GetStartPoint(DbGeography geographyValue)
		{
			geographyValue.CheckNull("geographyValue");
			object obj = this.ConvertToSqlValue(geographyValue, "geographyValue");
			object providerValue = this.imi_SqlGeography_STStartPoint.Value.Invoke(obj, new object[0]);
			return this.GeographyFromProviderValue(providerValue);
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x0000987C File Offset: 0x00007A7C
		public override DbGeography GetEndPoint(DbGeography geographyValue)
		{
			geographyValue.CheckNull("geographyValue");
			object obj = this.ConvertToSqlValue(geographyValue, "geographyValue");
			object providerValue = this.imi_SqlGeography_STEndPoint.Value.Invoke(obj, new object[0]);
			return this.GeographyFromProviderValue(providerValue);
		}

		// Token: 0x060002AA RID: 682 RVA: 0x000098C0 File Offset: 0x00007AC0
		public override bool? GetIsClosed(DbGeography geographyValue)
		{
			geographyValue.CheckNull("geographyValue");
			object obj = this.ConvertToSqlValue(geographyValue, "geographyValue");
			object sqlBoolean = this.imi_SqlGeography_STIsClosed.Value.Invoke(obj, new object[0]);
			return this.ConvertSqlBooleanToNullableBoolean(sqlBoolean);
		}

		// Token: 0x060002AB RID: 683 RVA: 0x00009904 File Offset: 0x00007B04
		public override int? GetPointCount(DbGeography geographyValue)
		{
			geographyValue.CheckNull("geographyValue");
			object obj = this.ConvertToSqlValue(geographyValue, "geographyValue");
			object sqlInt32Value = this.imi_SqlGeography_STNumPoints.Value.Invoke(obj, new object[0]);
			return this.ConvertSqlInt32ToNullableInt(sqlInt32Value);
		}

		// Token: 0x060002AC RID: 684 RVA: 0x00009948 File Offset: 0x00007B48
		public override DbGeography PointAt(DbGeography geographyValue, int nValue)
		{
			geographyValue.CheckNull("geographyValue");
			object obj = this.ConvertToSqlValue(geographyValue, "geographyValue");
			object providerValue = this.imi_SqlGeography_STPointN.Value.Invoke(obj, new object[]
			{
				nValue
			});
			return this.GeographyFromProviderValue(providerValue);
		}

		// Token: 0x060002AD RID: 685 RVA: 0x00009998 File Offset: 0x00007B98
		public override double? GetArea(DbGeography geographyValue)
		{
			geographyValue.CheckNull("geographyValue");
			object obj = this.ConvertToSqlValue(geographyValue, "geographyValue");
			object sqlDoubleValue = this.imi_SqlGeography_STArea.Value.Invoke(obj, new object[0]);
			return this.ConvertSqlDoubleToNullableDouble(sqlDoubleValue);
		}

		// Token: 0x060002AE RID: 686 RVA: 0x000099DC File Offset: 0x00007BDC
		public override DbGeometry GeometryFromText(string geometryText)
		{
			object obj = this.ConvertToSqlString(geometryText, "geometryText");
			object providerValue = this.smi_SqlGeometry_Parse.Value.Invoke(null, new object[]
			{
				obj
			});
			return this.GeometryFromProviderValue(providerValue);
		}

		// Token: 0x060002AF RID: 687 RVA: 0x00009A1C File Offset: 0x00007C1C
		public override DbGeometry GeometryFromText(string geometryText, int srid)
		{
			object obj = this.ConvertToSqlChars(geometryText, "geometryText");
			object providerValue = this.smi_SqlGeometry_STGeomFromText.Value.Invoke(null, new object[]
			{
				obj,
				srid
			});
			return this.GeometryFromProviderValue(providerValue);
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x00009A64 File Offset: 0x00007C64
		public override DbGeometry GeometryPointFromText(string pointText, int srid)
		{
			object obj = this.ConvertToSqlChars(pointText, "pointText");
			object providerValue = this.smi_SqlGeometry_STPointFromText.Value.Invoke(null, new object[]
			{
				obj,
				srid
			});
			return this.GeometryFromProviderValue(providerValue);
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x00009AAC File Offset: 0x00007CAC
		public override DbGeometry GeometryLineFromText(string lineText, int srid)
		{
			object obj = this.ConvertToSqlChars(lineText, "lineText");
			object providerValue = this.smi_SqlGeometry_STLineFromText.Value.Invoke(null, new object[]
			{
				obj,
				srid
			});
			return this.GeometryFromProviderValue(providerValue);
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x00009AF4 File Offset: 0x00007CF4
		public override DbGeometry GeometryPolygonFromText(string polygonText, int srid)
		{
			object obj = this.ConvertToSqlChars(polygonText, "polygonText");
			object providerValue = this.smi_SqlGeometry_STPolyFromText.Value.Invoke(null, new object[]
			{
				obj,
				srid
			});
			return this.GeometryFromProviderValue(providerValue);
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x00009B3C File Offset: 0x00007D3C
		public override DbGeometry GeometryMultiPointFromText(string multiPointText, int srid)
		{
			object obj = this.ConvertToSqlChars(multiPointText, "multiPointText");
			object providerValue = this.smi_SqlGeometry_STMPointFromText.Value.Invoke(null, new object[]
			{
				obj,
				srid
			});
			return this.GeometryFromProviderValue(providerValue);
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x00009B84 File Offset: 0x00007D84
		public override DbGeometry GeometryMultiLineFromText(string multiLineText, int srid)
		{
			object obj = this.ConvertToSqlChars(multiLineText, "multiLineText");
			object providerValue = this.smi_SqlGeometry_STMLineFromText.Value.Invoke(null, new object[]
			{
				obj,
				srid
			});
			return this.GeometryFromProviderValue(providerValue);
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x00009BCC File Offset: 0x00007DCC
		public override DbGeometry GeometryMultiPolygonFromText(string multiPolygonText, int srid)
		{
			object obj = this.ConvertToSqlChars(multiPolygonText, "multiPolygonText");
			object providerValue = this.smi_SqlGeometry_STMPolyFromText.Value.Invoke(null, new object[]
			{
				obj,
				srid
			});
			return this.GeometryFromProviderValue(providerValue);
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x00009C14 File Offset: 0x00007E14
		public override DbGeometry GeometryCollectionFromText(string geometryCollectionText, int srid)
		{
			object obj = this.ConvertToSqlChars(geometryCollectionText, "geometryCollectionText");
			object providerValue = this.smi_SqlGeometry_STGeomCollFromText.Value.Invoke(null, new object[]
			{
				obj,
				srid
			});
			return this.GeometryFromProviderValue(providerValue);
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x00009C5C File Offset: 0x00007E5C
		public override DbGeometry GeometryFromBinary(byte[] geometryBytes)
		{
			object obj = this.ConvertToSqlBytes(geometryBytes, "geometryBytes");
			object providerValue = this.smi_SqlGeometry_STGeomFromWKB.Value.Invoke(null, new object[]
			{
				obj,
				0
			});
			return this.GeometryFromProviderValue(providerValue);
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x00009CA4 File Offset: 0x00007EA4
		public override DbGeometry GeometryFromBinary(byte[] geometryBytes, int srid)
		{
			object obj = this.ConvertToSqlBytes(geometryBytes, "geometryBytes");
			object providerValue = this.smi_SqlGeometry_STGeomFromWKB.Value.Invoke(null, new object[]
			{
				obj,
				srid
			});
			return this.GeometryFromProviderValue(providerValue);
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x00009CEC File Offset: 0x00007EEC
		public override DbGeometry GeometryPointFromBinary(byte[] pointBytes, int srid)
		{
			object obj = this.ConvertToSqlBytes(pointBytes, "pointBytes");
			object providerValue = this.smi_SqlGeometry_STPointFromWKB.Value.Invoke(null, new object[]
			{
				obj,
				srid
			});
			return this.GeometryFromProviderValue(providerValue);
		}

		// Token: 0x060002BA RID: 698 RVA: 0x00009D34 File Offset: 0x00007F34
		public override DbGeometry GeometryLineFromBinary(byte[] lineBytes, int srid)
		{
			object obj = this.ConvertToSqlBytes(lineBytes, "lineBytes");
			object providerValue = this.smi_SqlGeometry_STLineFromWKB.Value.Invoke(null, new object[]
			{
				obj,
				srid
			});
			return this.GeometryFromProviderValue(providerValue);
		}

		// Token: 0x060002BB RID: 699 RVA: 0x00009D7C File Offset: 0x00007F7C
		public override DbGeometry GeometryPolygonFromBinary(byte[] polygonBytes, int srid)
		{
			object obj = this.ConvertToSqlBytes(polygonBytes, "polygonBytes");
			object providerValue = this.smi_SqlGeometry_STPolyFromWKB.Value.Invoke(null, new object[]
			{
				obj,
				srid
			});
			return this.GeometryFromProviderValue(providerValue);
		}

		// Token: 0x060002BC RID: 700 RVA: 0x00009DC4 File Offset: 0x00007FC4
		public override DbGeometry GeometryMultiPointFromBinary(byte[] multiPointBytes, int srid)
		{
			object obj = this.ConvertToSqlBytes(multiPointBytes, "multiPointBytes");
			object providerValue = this.smi_SqlGeometry_STMPointFromWKB.Value.Invoke(null, new object[]
			{
				obj,
				srid
			});
			return this.GeometryFromProviderValue(providerValue);
		}

		// Token: 0x060002BD RID: 701 RVA: 0x00009E0C File Offset: 0x0000800C
		public override DbGeometry GeometryMultiLineFromBinary(byte[] multiLineBytes, int srid)
		{
			object obj = this.ConvertToSqlBytes(multiLineBytes, "multiLineBytes");
			object providerValue = this.smi_SqlGeometry_STMLineFromWKB.Value.Invoke(null, new object[]
			{
				obj,
				srid
			});
			return this.GeometryFromProviderValue(providerValue);
		}

		// Token: 0x060002BE RID: 702 RVA: 0x00009E54 File Offset: 0x00008054
		public override DbGeometry GeometryMultiPolygonFromBinary(byte[] multiPolygonBytes, int srid)
		{
			object obj = this.ConvertToSqlBytes(multiPolygonBytes, "multiPolygonBytes");
			object providerValue = this.smi_SqlGeometry_STMPolyFromWKB.Value.Invoke(null, new object[]
			{
				obj,
				srid
			});
			return this.GeometryFromProviderValue(providerValue);
		}

		// Token: 0x060002BF RID: 703 RVA: 0x00009E9C File Offset: 0x0000809C
		public override DbGeometry GeometryCollectionFromBinary(byte[] geometryCollectionBytes, int srid)
		{
			object obj = this.ConvertToSqlBytes(geometryCollectionBytes, "geometryCollectionBytes");
			object providerValue = this.smi_SqlGeometry_STGeomCollFromWKB.Value.Invoke(null, new object[]
			{
				obj,
				srid
			});
			return this.GeometryFromProviderValue(providerValue);
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x00009EE4 File Offset: 0x000080E4
		public override DbGeometry GeometryFromGml(string geometryGml)
		{
			object obj = this.ConvertToSqlXml(geometryGml, "geometryGml");
			object providerValue = this.smi_SqlGeometry_GeomFromGml.Value.Invoke(null, new object[]
			{
				obj,
				0
			});
			return this.GeometryFromProviderValue(providerValue);
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x00009F2C File Offset: 0x0000812C
		public override DbGeometry GeometryFromGml(string geometryGml, int srid)
		{
			object obj = this.ConvertToSqlXml(geometryGml, "geometryGml");
			object providerValue = this.smi_SqlGeometry_GeomFromGml.Value.Invoke(null, new object[]
			{
				obj,
				srid
			});
			return this.GeometryFromProviderValue(providerValue);
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x00009F74 File Offset: 0x00008174
		public override int GetCoordinateSystemId(DbGeometry geometryValue)
		{
			geometryValue.CheckNull("geometryValue");
			object obj = this.ConvertToSqlValue(geometryValue, "geometryValue");
			object value = this.ipi_SqlGeometry_STSrid.Value.GetValue(obj, null);
			return this.ConvertSqlInt32ToInt(value);
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x00009FB4 File Offset: 0x000081B4
		public override string GetSpatialTypeName(DbGeometry geometryValue)
		{
			geometryValue.CheckNull("geometryValue");
			object obj = this.ConvertToSqlValue(geometryValue, "geometryValue");
			object sqlCharsValue = this.imi_SqlGeometry_STGeometryType.Value.Invoke(obj, new object[0]);
			return this.ConvertSqlStringToString(sqlCharsValue);
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x00009FF8 File Offset: 0x000081F8
		public override int GetDimension(DbGeometry geometryValue)
		{
			geometryValue.CheckNull("geometryValue");
			object obj = this.ConvertToSqlValue(geometryValue, "geometryValue");
			object sqlInt32Value = this.imi_SqlGeometry_STDimension.Value.Invoke(obj, new object[0]);
			return this.ConvertSqlInt32ToInt(sqlInt32Value);
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x0000A03C File Offset: 0x0000823C
		public override DbGeometry GetEnvelope(DbGeometry geometryValue)
		{
			geometryValue.CheckNull("geometryValue");
			object obj = this.ConvertToSqlValue(geometryValue, "geometryValue");
			object providerValue = this.imi_SqlGeometry_STEnvelope.Value.Invoke(obj, new object[0]);
			return this.GeometryFromProviderValue(providerValue);
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x0000A080 File Offset: 0x00008280
		public override byte[] AsBinary(DbGeometry geometryValue)
		{
			geometryValue.CheckNull("geometryValue");
			object obj = this.ConvertToSqlValue(geometryValue, "geometryValue");
			object sqlBytes = this.imi_SqlGeometry_STAsBinary.Value.Invoke(obj, new object[0]);
			return this.ConvertSqlBytesToBinary(sqlBytes);
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x0000A0C4 File Offset: 0x000082C4
		public override string AsGml(DbGeometry geometryValue)
		{
			geometryValue.CheckNull("geometryValue");
			object obj = this.ConvertToSqlValue(geometryValue, "geometryValue");
			object sqlXmlValue = this.imi_SqlGeometry_AsGml.Value.Invoke(obj, new object[0]);
			return this.ConvertSqlXmlToString(sqlXmlValue);
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x0000A108 File Offset: 0x00008308
		public override string AsText(DbGeometry geometryValue)
		{
			geometryValue.CheckNull("geometryValue");
			object obj = this.ConvertToSqlValue(geometryValue, "geometryValue");
			object sqlCharsValue = this.imi_SqlGeometry_STAsText.Value.Invoke(obj, new object[0]);
			return this.ConvertSqlCharsToString(sqlCharsValue);
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x0000A14C File Offset: 0x0000834C
		public override bool GetIsEmpty(DbGeometry geometryValue)
		{
			geometryValue.CheckNull("geometryValue");
			object obj = this.ConvertToSqlValue(geometryValue, "geometryValue");
			object sqlBoolean = this.imi_SqlGeometry_STIsEmpty.Value.Invoke(obj, new object[0]);
			return this.ConvertSqlBooleanToBoolean(sqlBoolean);
		}

		// Token: 0x060002CA RID: 714 RVA: 0x0000A190 File Offset: 0x00008390
		public override bool GetIsSimple(DbGeometry geometryValue)
		{
			geometryValue.CheckNull("geometryValue");
			object obj = this.ConvertToSqlValue(geometryValue, "geometryValue");
			object sqlBoolean = this.imi_SqlGeometry_STIsSimple.Value.Invoke(obj, new object[0]);
			return this.ConvertSqlBooleanToBoolean(sqlBoolean);
		}

		// Token: 0x060002CB RID: 715 RVA: 0x0000A1D4 File Offset: 0x000083D4
		public override DbGeometry GetBoundary(DbGeometry geometryValue)
		{
			geometryValue.CheckNull("geometryValue");
			object obj = this.ConvertToSqlValue(geometryValue, "geometryValue");
			object providerValue = this.imi_SqlGeometry_STBoundary.Value.Invoke(obj, new object[0]);
			return this.GeometryFromProviderValue(providerValue);
		}

		// Token: 0x060002CC RID: 716 RVA: 0x0000A218 File Offset: 0x00008418
		public override bool GetIsValid(DbGeometry geometryValue)
		{
			geometryValue.CheckNull("geometryValue");
			object obj = this.ConvertToSqlValue(geometryValue, "geometryValue");
			object sqlBoolean = this.imi_SqlGeometry_STIsValid.Value.Invoke(obj, new object[0]);
			return this.ConvertSqlBooleanToBoolean(sqlBoolean);
		}

		// Token: 0x060002CD RID: 717 RVA: 0x0000A25C File Offset: 0x0000845C
		public override bool SpatialEquals(DbGeometry geometryValue1, DbGeometry geometryValue2)
		{
			geometryValue1.CheckNull("geometryValue1");
			object obj = this.ConvertToSqlValue(geometryValue1, "geometryValue1");
			object obj2 = this.ConvertToSqlValue(geometryValue2, "geometryValue2");
			object sqlBoolean = this.imi_SqlGeometry_STEquals.Value.Invoke(obj, new object[]
			{
				obj2
			});
			return this.ConvertSqlBooleanToBoolean(sqlBoolean);
		}

		// Token: 0x060002CE RID: 718 RVA: 0x0000A2B4 File Offset: 0x000084B4
		public override bool Disjoint(DbGeometry geometryValue1, DbGeometry geometryValue2)
		{
			geometryValue1.CheckNull("geometryValue1");
			object obj = this.ConvertToSqlValue(geometryValue1, "geometryValue1");
			object obj2 = this.ConvertToSqlValue(geometryValue2, "geometryValue2");
			object sqlBoolean = this.imi_SqlGeometry_STDisjoint.Value.Invoke(obj, new object[]
			{
				obj2
			});
			return this.ConvertSqlBooleanToBoolean(sqlBoolean);
		}

		// Token: 0x060002CF RID: 719 RVA: 0x0000A30C File Offset: 0x0000850C
		public override bool Intersects(DbGeometry geometryValue1, DbGeometry geometryValue2)
		{
			geometryValue1.CheckNull("geometryValue1");
			object obj = this.ConvertToSqlValue(geometryValue1, "geometryValue1");
			object obj2 = this.ConvertToSqlValue(geometryValue2, "geometryValue2");
			object sqlBoolean = this.imi_SqlGeometry_STIntersects.Value.Invoke(obj, new object[]
			{
				obj2
			});
			return this.ConvertSqlBooleanToBoolean(sqlBoolean);
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x0000A364 File Offset: 0x00008564
		public override bool Touches(DbGeometry geometryValue1, DbGeometry geometryValue2)
		{
			geometryValue1.CheckNull("geometryValue1");
			object obj = this.ConvertToSqlValue(geometryValue1, "geometryValue1");
			object obj2 = this.ConvertToSqlValue(geometryValue2, "geometryValue2");
			object sqlBoolean = this.imi_SqlGeometry_STTouches.Value.Invoke(obj, new object[]
			{
				obj2
			});
			return this.ConvertSqlBooleanToBoolean(sqlBoolean);
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x0000A3BC File Offset: 0x000085BC
		public override bool Crosses(DbGeometry geometryValue1, DbGeometry geometryValue2)
		{
			geometryValue1.CheckNull("geometryValue1");
			object obj = this.ConvertToSqlValue(geometryValue1, "geometryValue1");
			object obj2 = this.ConvertToSqlValue(geometryValue2, "geometryValue2");
			object sqlBoolean = this.imi_SqlGeometry_STCrosses.Value.Invoke(obj, new object[]
			{
				obj2
			});
			return this.ConvertSqlBooleanToBoolean(sqlBoolean);
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x0000A414 File Offset: 0x00008614
		public override bool Within(DbGeometry geometryValue1, DbGeometry geometryValue2)
		{
			geometryValue1.CheckNull("geometryValue1");
			object obj = this.ConvertToSqlValue(geometryValue1, "geometryValue1");
			object obj2 = this.ConvertToSqlValue(geometryValue2, "geometryValue2");
			object sqlBoolean = this.imi_SqlGeometry_STWithin.Value.Invoke(obj, new object[]
			{
				obj2
			});
			return this.ConvertSqlBooleanToBoolean(sqlBoolean);
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x0000A46C File Offset: 0x0000866C
		public override bool Contains(DbGeometry geometryValue1, DbGeometry geometryValue2)
		{
			geometryValue1.CheckNull("geometryValue1");
			object obj = this.ConvertToSqlValue(geometryValue1, "geometryValue1");
			object obj2 = this.ConvertToSqlValue(geometryValue2, "geometryValue2");
			object sqlBoolean = this.imi_SqlGeometry_STContains.Value.Invoke(obj, new object[]
			{
				obj2
			});
			return this.ConvertSqlBooleanToBoolean(sqlBoolean);
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x0000A4C4 File Offset: 0x000086C4
		public override bool Overlaps(DbGeometry geometryValue1, DbGeometry geometryValue2)
		{
			geometryValue1.CheckNull("geometryValue1");
			object obj = this.ConvertToSqlValue(geometryValue1, "geometryValue1");
			object obj2 = this.ConvertToSqlValue(geometryValue2, "geometryValue2");
			object sqlBoolean = this.imi_SqlGeometry_STOverlaps.Value.Invoke(obj, new object[]
			{
				obj2
			});
			return this.ConvertSqlBooleanToBoolean(sqlBoolean);
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x0000A51C File Offset: 0x0000871C
		public override bool Relate(DbGeometry geometryValue1, DbGeometry geometryValue2, string matrix)
		{
			geometryValue1.CheckNull("geometryValue1");
			object obj = this.ConvertToSqlValue(geometryValue1, "geometryValue1");
			object obj2 = this.ConvertToSqlValue(geometryValue2, "geometryValue2");
			object sqlBoolean = this.imi_SqlGeometry_STRelate.Value.Invoke(obj, new object[]
			{
				obj2,
				matrix
			});
			return this.ConvertSqlBooleanToBoolean(sqlBoolean);
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x0000A578 File Offset: 0x00008778
		public override DbGeometry Buffer(DbGeometry geometryValue, double distance)
		{
			geometryValue.CheckNull("geometryValue");
			object obj = this.ConvertToSqlValue(geometryValue, "geometryValue");
			object providerValue = this.imi_SqlGeometry_STBuffer.Value.Invoke(obj, new object[]
			{
				distance
			});
			return this.GeometryFromProviderValue(providerValue);
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x0000A5C8 File Offset: 0x000087C8
		public override double Distance(DbGeometry geometryValue1, DbGeometry geometryValue2)
		{
			geometryValue1.CheckNull("geometryValue1");
			object obj = this.ConvertToSqlValue(geometryValue1, "geometryValue1");
			object obj2 = this.ConvertToSqlValue(geometryValue2, "geometryValue2");
			object sqlDoubleValue = this.imi_SqlGeometry_STDistance.Value.Invoke(obj, new object[]
			{
				obj2
			});
			return this.ConvertSqlDoubleToDouble(sqlDoubleValue);
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x0000A620 File Offset: 0x00008820
		public override DbGeometry GetConvexHull(DbGeometry geometryValue)
		{
			geometryValue.CheckNull("geometryValue");
			object obj = this.ConvertToSqlValue(geometryValue, "geometryValue");
			object providerValue = this.imi_SqlGeometry_STConvexHull.Value.Invoke(obj, new object[0]);
			return this.GeometryFromProviderValue(providerValue);
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x0000A664 File Offset: 0x00008864
		public override DbGeometry Intersection(DbGeometry geometryValue1, DbGeometry geometryValue2)
		{
			geometryValue1.CheckNull("geometryValue1");
			object obj = this.ConvertToSqlValue(geometryValue1, "geometryValue1");
			object obj2 = this.ConvertToSqlValue(geometryValue2, "geometryValue2");
			object providerValue = this.imi_SqlGeometry_STIntersection.Value.Invoke(obj, new object[]
			{
				obj2
			});
			return this.GeometryFromProviderValue(providerValue);
		}

		// Token: 0x060002DA RID: 730 RVA: 0x0000A6BC File Offset: 0x000088BC
		public override DbGeometry Union(DbGeometry geometryValue1, DbGeometry geometryValue2)
		{
			geometryValue1.CheckNull("geometryValue1");
			object obj = this.ConvertToSqlValue(geometryValue1, "geometryValue1");
			object obj2 = this.ConvertToSqlValue(geometryValue2, "geometryValue2");
			object providerValue = this.imi_SqlGeometry_STUnion.Value.Invoke(obj, new object[]
			{
				obj2
			});
			return this.GeometryFromProviderValue(providerValue);
		}

		// Token: 0x060002DB RID: 731 RVA: 0x0000A714 File Offset: 0x00008914
		public override DbGeometry Difference(DbGeometry geometryValue1, DbGeometry geometryValue2)
		{
			geometryValue1.CheckNull("geometryValue1");
			object obj = this.ConvertToSqlValue(geometryValue1, "geometryValue1");
			object obj2 = this.ConvertToSqlValue(geometryValue2, "geometryValue2");
			object providerValue = this.imi_SqlGeometry_STDifference.Value.Invoke(obj, new object[]
			{
				obj2
			});
			return this.GeometryFromProviderValue(providerValue);
		}

		// Token: 0x060002DC RID: 732 RVA: 0x0000A76C File Offset: 0x0000896C
		public override DbGeometry SymmetricDifference(DbGeometry geometryValue1, DbGeometry geometryValue2)
		{
			geometryValue1.CheckNull("geometryValue1");
			object obj = this.ConvertToSqlValue(geometryValue1, "geometryValue1");
			object obj2 = this.ConvertToSqlValue(geometryValue2, "geometryValue2");
			object providerValue = this.imi_SqlGeometry_STSymDifference.Value.Invoke(obj, new object[]
			{
				obj2
			});
			return this.GeometryFromProviderValue(providerValue);
		}

		// Token: 0x060002DD RID: 733 RVA: 0x0000A7C4 File Offset: 0x000089C4
		public override int? GetElementCount(DbGeometry geometryValue)
		{
			geometryValue.CheckNull("geometryValue");
			object obj = this.ConvertToSqlValue(geometryValue, "geometryValue");
			object sqlInt32Value = this.imi_SqlGeometry_STNumGeometries.Value.Invoke(obj, new object[0]);
			return this.ConvertSqlInt32ToNullableInt(sqlInt32Value);
		}

		// Token: 0x060002DE RID: 734 RVA: 0x0000A808 File Offset: 0x00008A08
		public override DbGeometry ElementAt(DbGeometry geometryValue, int nValue)
		{
			geometryValue.CheckNull("geometryValue");
			object obj = this.ConvertToSqlValue(geometryValue, "geometryValue");
			object providerValue = this.imi_SqlGeometry_STGeometryN.Value.Invoke(obj, new object[]
			{
				nValue
			});
			return this.GeometryFromProviderValue(providerValue);
		}

		// Token: 0x060002DF RID: 735 RVA: 0x0000A858 File Offset: 0x00008A58
		public override double? GetXCoordinate(DbGeometry geometryValue)
		{
			geometryValue.CheckNull("geometryValue");
			object obj = this.ConvertToSqlValue(geometryValue, "geometryValue");
			object value = this.ipi_SqlGeometry_STX.Value.GetValue(obj, null);
			return this.ConvertSqlDoubleToNullableDouble(value);
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x0000A898 File Offset: 0x00008A98
		public override double? GetYCoordinate(DbGeometry geometryValue)
		{
			geometryValue.CheckNull("geometryValue");
			object obj = this.ConvertToSqlValue(geometryValue, "geometryValue");
			object value = this.ipi_SqlGeometry_STY.Value.GetValue(obj, null);
			return this.ConvertSqlDoubleToNullableDouble(value);
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x0000A8D8 File Offset: 0x00008AD8
		public override double? GetElevation(DbGeometry geometryValue)
		{
			geometryValue.CheckNull("geometryValue");
			object obj = this.ConvertToSqlValue(geometryValue, "geometryValue");
			object value = this.ipi_SqlGeometry_Z.Value.GetValue(obj, null);
			return this.ConvertSqlDoubleToNullableDouble(value);
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x0000A918 File Offset: 0x00008B18
		public override double? GetMeasure(DbGeometry geometryValue)
		{
			geometryValue.CheckNull("geometryValue");
			object obj = this.ConvertToSqlValue(geometryValue, "geometryValue");
			object value = this.ipi_SqlGeometry_M.Value.GetValue(obj, null);
			return this.ConvertSqlDoubleToNullableDouble(value);
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x0000A958 File Offset: 0x00008B58
		public override double? GetLength(DbGeometry geometryValue)
		{
			geometryValue.CheckNull("geometryValue");
			object obj = this.ConvertToSqlValue(geometryValue, "geometryValue");
			object sqlDoubleValue = this.imi_SqlGeometry_STLength.Value.Invoke(obj, new object[0]);
			return this.ConvertSqlDoubleToNullableDouble(sqlDoubleValue);
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x0000A99C File Offset: 0x00008B9C
		public override DbGeometry GetStartPoint(DbGeometry geometryValue)
		{
			geometryValue.CheckNull("geometryValue");
			object obj = this.ConvertToSqlValue(geometryValue, "geometryValue");
			object providerValue = this.imi_SqlGeometry_STStartPoint.Value.Invoke(obj, new object[0]);
			return this.GeometryFromProviderValue(providerValue);
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x0000A9E0 File Offset: 0x00008BE0
		public override DbGeometry GetEndPoint(DbGeometry geometryValue)
		{
			geometryValue.CheckNull("geometryValue");
			object obj = this.ConvertToSqlValue(geometryValue, "geometryValue");
			object providerValue = this.imi_SqlGeometry_STEndPoint.Value.Invoke(obj, new object[0]);
			return this.GeometryFromProviderValue(providerValue);
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x0000AA24 File Offset: 0x00008C24
		public override bool? GetIsClosed(DbGeometry geometryValue)
		{
			geometryValue.CheckNull("geometryValue");
			object obj = this.ConvertToSqlValue(geometryValue, "geometryValue");
			object sqlBoolean = this.imi_SqlGeometry_STIsClosed.Value.Invoke(obj, new object[0]);
			return this.ConvertSqlBooleanToNullableBoolean(sqlBoolean);
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x0000AA68 File Offset: 0x00008C68
		public override bool? GetIsRing(DbGeometry geometryValue)
		{
			geometryValue.CheckNull("geometryValue");
			object obj = this.ConvertToSqlValue(geometryValue, "geometryValue");
			object sqlBoolean = this.imi_SqlGeometry_STIsRing.Value.Invoke(obj, new object[0]);
			return this.ConvertSqlBooleanToNullableBoolean(sqlBoolean);
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x0000AAAC File Offset: 0x00008CAC
		public override int? GetPointCount(DbGeometry geometryValue)
		{
			geometryValue.CheckNull("geometryValue");
			object obj = this.ConvertToSqlValue(geometryValue, "geometryValue");
			object sqlInt32Value = this.imi_SqlGeometry_STNumPoints.Value.Invoke(obj, new object[0]);
			return this.ConvertSqlInt32ToNullableInt(sqlInt32Value);
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x0000AAF0 File Offset: 0x00008CF0
		public override DbGeometry PointAt(DbGeometry geometryValue, int nValue)
		{
			geometryValue.CheckNull("geometryValue");
			object obj = this.ConvertToSqlValue(geometryValue, "geometryValue");
			object providerValue = this.imi_SqlGeometry_STPointN.Value.Invoke(obj, new object[]
			{
				nValue
			});
			return this.GeometryFromProviderValue(providerValue);
		}

		// Token: 0x060002EA RID: 746 RVA: 0x0000AB40 File Offset: 0x00008D40
		public override double? GetArea(DbGeometry geometryValue)
		{
			geometryValue.CheckNull("geometryValue");
			object obj = this.ConvertToSqlValue(geometryValue, "geometryValue");
			object sqlDoubleValue = this.imi_SqlGeometry_STArea.Value.Invoke(obj, new object[0]);
			return this.ConvertSqlDoubleToNullableDouble(sqlDoubleValue);
		}

		// Token: 0x060002EB RID: 747 RVA: 0x0000AB84 File Offset: 0x00008D84
		public override DbGeometry GetCentroid(DbGeometry geometryValue)
		{
			geometryValue.CheckNull("geometryValue");
			object obj = this.ConvertToSqlValue(geometryValue, "geometryValue");
			object providerValue = this.imi_SqlGeometry_STCentroid.Value.Invoke(obj, new object[0]);
			return this.GeometryFromProviderValue(providerValue);
		}

		// Token: 0x060002EC RID: 748 RVA: 0x0000ABC8 File Offset: 0x00008DC8
		public override DbGeometry GetPointOnSurface(DbGeometry geometryValue)
		{
			geometryValue.CheckNull("geometryValue");
			object obj = this.ConvertToSqlValue(geometryValue, "geometryValue");
			object providerValue = this.imi_SqlGeometry_STPointOnSurface.Value.Invoke(obj, new object[0]);
			return this.GeometryFromProviderValue(providerValue);
		}

		// Token: 0x060002ED RID: 749 RVA: 0x0000AC0C File Offset: 0x00008E0C
		public override DbGeometry GetExteriorRing(DbGeometry geometryValue)
		{
			geometryValue.CheckNull("geometryValue");
			object obj = this.ConvertToSqlValue(geometryValue, "geometryValue");
			object providerValue = this.imi_SqlGeometry_STExteriorRing.Value.Invoke(obj, new object[0]);
			return this.GeometryFromProviderValue(providerValue);
		}

		// Token: 0x060002EE RID: 750 RVA: 0x0000AC50 File Offset: 0x00008E50
		public override int? GetInteriorRingCount(DbGeometry geometryValue)
		{
			geometryValue.CheckNull("geometryValue");
			object obj = this.ConvertToSqlValue(geometryValue, "geometryValue");
			object sqlInt32Value = this.imi_SqlGeometry_STNumInteriorRing.Value.Invoke(obj, new object[0]);
			return this.ConvertSqlInt32ToNullableInt(sqlInt32Value);
		}

		// Token: 0x060002EF RID: 751 RVA: 0x0000AC94 File Offset: 0x00008E94
		public override DbGeometry InteriorRingAt(DbGeometry geometryValue, int nValue)
		{
			geometryValue.CheckNull("geometryValue");
			object obj = this.ConvertToSqlValue(geometryValue, "geometryValue");
			object providerValue = this.imi_SqlGeometry_STInteriorRingN.Value.Invoke(obj, new object[]
			{
				nValue
			});
			return this.GeometryFromProviderValue(providerValue);
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x0000ACE4 File Offset: 0x00008EE4
		private void InitializeMemberInfo()
		{
			this.smi_SqlGeography_Parse = new Singleton<MethodInfo>(() => this.FindSqlGeographyStaticMethod("Parse", new Type[]
			{
				this.SqlTypes.SqlStringType
			}));
			this.smi_SqlGeography_STGeomFromText = new Singleton<MethodInfo>(() => this.FindSqlGeographyStaticMethod("STGeomFromText", new Type[]
			{
				this.SqlTypes.SqlCharsType,
				typeof(int)
			}));
			this.smi_SqlGeography_STPointFromText = new Singleton<MethodInfo>(() => this.FindSqlGeographyStaticMethod("STPointFromText", new Type[]
			{
				this.SqlTypes.SqlCharsType,
				typeof(int)
			}));
			this.smi_SqlGeography_STLineFromText = new Singleton<MethodInfo>(() => this.FindSqlGeographyStaticMethod("STLineFromText", new Type[]
			{
				this.SqlTypes.SqlCharsType,
				typeof(int)
			}));
			this.smi_SqlGeography_STPolyFromText = new Singleton<MethodInfo>(() => this.FindSqlGeographyStaticMethod("STPolyFromText", new Type[]
			{
				this.SqlTypes.SqlCharsType,
				typeof(int)
			}));
			this.smi_SqlGeography_STMPointFromText = new Singleton<MethodInfo>(() => this.FindSqlGeographyStaticMethod("STMPointFromText", new Type[]
			{
				this.SqlTypes.SqlCharsType,
				typeof(int)
			}));
			this.smi_SqlGeography_STMLineFromText = new Singleton<MethodInfo>(() => this.FindSqlGeographyStaticMethod("STMLineFromText", new Type[]
			{
				this.SqlTypes.SqlCharsType,
				typeof(int)
			}));
			this.smi_SqlGeography_STMPolyFromText = new Singleton<MethodInfo>(() => this.FindSqlGeographyStaticMethod("STMPolyFromText", new Type[]
			{
				this.SqlTypes.SqlCharsType,
				typeof(int)
			}));
			this.smi_SqlGeography_STGeomCollFromText = new Singleton<MethodInfo>(() => this.FindSqlGeographyStaticMethod("STGeomCollFromText", new Type[]
			{
				this.SqlTypes.SqlCharsType,
				typeof(int)
			}));
			this.smi_SqlGeography_STGeomFromWKB = new Singleton<MethodInfo>(() => this.FindSqlGeographyStaticMethod("STGeomFromWKB", new Type[]
			{
				this.SqlTypes.SqlBytesType,
				typeof(int)
			}));
			this.smi_SqlGeography_STPointFromWKB = new Singleton<MethodInfo>(() => this.FindSqlGeographyStaticMethod("STPointFromWKB", new Type[]
			{
				this.SqlTypes.SqlBytesType,
				typeof(int)
			}));
			this.smi_SqlGeography_STLineFromWKB = new Singleton<MethodInfo>(() => this.FindSqlGeographyStaticMethod("STLineFromWKB", new Type[]
			{
				this.SqlTypes.SqlBytesType,
				typeof(int)
			}));
			this.smi_SqlGeography_STPolyFromWKB = new Singleton<MethodInfo>(() => this.FindSqlGeographyStaticMethod("STPolyFromWKB", new Type[]
			{
				this.SqlTypes.SqlBytesType,
				typeof(int)
			}));
			this.smi_SqlGeography_STMPointFromWKB = new Singleton<MethodInfo>(() => this.FindSqlGeographyStaticMethod("STMPointFromWKB", new Type[]
			{
				this.SqlTypes.SqlBytesType,
				typeof(int)
			}));
			this.smi_SqlGeography_STMLineFromWKB = new Singleton<MethodInfo>(() => this.FindSqlGeographyStaticMethod("STMLineFromWKB", new Type[]
			{
				this.SqlTypes.SqlBytesType,
				typeof(int)
			}));
			this.smi_SqlGeography_STMPolyFromWKB = new Singleton<MethodInfo>(() => this.FindSqlGeographyStaticMethod("STMPolyFromWKB", new Type[]
			{
				this.SqlTypes.SqlBytesType,
				typeof(int)
			}));
			this.smi_SqlGeography_STGeomCollFromWKB = new Singleton<MethodInfo>(() => this.FindSqlGeographyStaticMethod("STGeomCollFromWKB", new Type[]
			{
				this.SqlTypes.SqlBytesType,
				typeof(int)
			}));
			this.smi_SqlGeography_GeomFromGml = new Singleton<MethodInfo>(() => this.FindSqlGeographyStaticMethod("GeomFromGml", new Type[]
			{
				this.SqlTypes.SqlXmlType,
				typeof(int)
			}));
			this.ipi_SqlGeography_STSrid = new Singleton<PropertyInfo>(() => this.FindSqlGeographyProperty("STSrid"));
			this.imi_SqlGeography_STGeometryType = new Singleton<MethodInfo>(() => this.FindSqlGeographyMethod("STGeometryType", new Type[0]));
			this.imi_SqlGeography_STDimension = new Singleton<MethodInfo>(() => this.FindSqlGeographyMethod("STDimension", new Type[0]));
			this.imi_SqlGeography_STAsBinary = new Singleton<MethodInfo>(() => this.FindSqlGeographyMethod("STAsBinary", new Type[0]));
			this.imi_SqlGeography_AsGml = new Singleton<MethodInfo>(() => this.FindSqlGeographyMethod("AsGml", new Type[0]));
			this.imi_SqlGeography_STAsText = new Singleton<MethodInfo>(() => this.FindSqlGeographyMethod("STAsText", new Type[0]));
			this.imi_SqlGeography_STIsEmpty = new Singleton<MethodInfo>(() => this.FindSqlGeographyMethod("STIsEmpty", new Type[0]));
			this.imi_SqlGeography_STEquals = new Singleton<MethodInfo>(() => this.FindSqlGeographyMethod("STEquals", new Type[]
			{
				this.SqlTypes.SqlGeographyType
			}));
			this.imi_SqlGeography_STDisjoint = new Singleton<MethodInfo>(() => this.FindSqlGeographyMethod("STDisjoint", new Type[]
			{
				this.SqlTypes.SqlGeographyType
			}));
			this.imi_SqlGeography_STIntersects = new Singleton<MethodInfo>(() => this.FindSqlGeographyMethod("STIntersects", new Type[]
			{
				this.SqlTypes.SqlGeographyType
			}));
			this.imi_SqlGeography_STBuffer = new Singleton<MethodInfo>(() => this.FindSqlGeographyMethod("STBuffer", new Type[]
			{
				typeof(double)
			}));
			this.imi_SqlGeography_STDistance = new Singleton<MethodInfo>(() => this.FindSqlGeographyMethod("STDistance", new Type[]
			{
				this.SqlTypes.SqlGeographyType
			}));
			this.imi_SqlGeography_STIntersection = new Singleton<MethodInfo>(() => this.FindSqlGeographyMethod("STIntersection", new Type[]
			{
				this.SqlTypes.SqlGeographyType
			}));
			this.imi_SqlGeography_STUnion = new Singleton<MethodInfo>(() => this.FindSqlGeographyMethod("STUnion", new Type[]
			{
				this.SqlTypes.SqlGeographyType
			}));
			this.imi_SqlGeography_STDifference = new Singleton<MethodInfo>(() => this.FindSqlGeographyMethod("STDifference", new Type[]
			{
				this.SqlTypes.SqlGeographyType
			}));
			this.imi_SqlGeography_STSymDifference = new Singleton<MethodInfo>(() => this.FindSqlGeographyMethod("STSymDifference", new Type[]
			{
				this.SqlTypes.SqlGeographyType
			}));
			this.imi_SqlGeography_STNumGeometries = new Singleton<MethodInfo>(() => this.FindSqlGeographyMethod("STNumGeometries", new Type[0]));
			this.imi_SqlGeography_STGeometryN = new Singleton<MethodInfo>(() => this.FindSqlGeographyMethod("STGeometryN", new Type[]
			{
				typeof(int)
			}));
			this.ipi_SqlGeography_Lat = new Singleton<PropertyInfo>(() => this.FindSqlGeographyProperty("Lat"));
			this.ipi_SqlGeography_Long = new Singleton<PropertyInfo>(() => this.FindSqlGeographyProperty("Long"));
			this.ipi_SqlGeography_Z = new Singleton<PropertyInfo>(() => this.FindSqlGeographyProperty("Z"));
			this.ipi_SqlGeography_M = new Singleton<PropertyInfo>(() => this.FindSqlGeographyProperty("M"));
			this.imi_SqlGeography_STLength = new Singleton<MethodInfo>(() => this.FindSqlGeographyMethod("STLength", new Type[0]));
			this.imi_SqlGeography_STStartPoint = new Singleton<MethodInfo>(() => this.FindSqlGeographyMethod("STStartPoint", new Type[0]));
			this.imi_SqlGeography_STEndPoint = new Singleton<MethodInfo>(() => this.FindSqlGeographyMethod("STEndPoint", new Type[0]));
			this.imi_SqlGeography_STIsClosed = new Singleton<MethodInfo>(() => this.FindSqlGeographyMethod("STIsClosed", new Type[0]));
			this.imi_SqlGeography_STNumPoints = new Singleton<MethodInfo>(() => this.FindSqlGeographyMethod("STNumPoints", new Type[0]));
			this.imi_SqlGeography_STPointN = new Singleton<MethodInfo>(() => this.FindSqlGeographyMethod("STPointN", new Type[]
			{
				typeof(int)
			}));
			this.imi_SqlGeography_STArea = new Singleton<MethodInfo>(() => this.FindSqlGeographyMethod("STArea", new Type[0]));
			this.smi_SqlGeometry_Parse = new Singleton<MethodInfo>(() => this.FindSqlGeometryStaticMethod("Parse", new Type[]
			{
				this.SqlTypes.SqlStringType
			}));
			this.smi_SqlGeometry_STGeomFromText = new Singleton<MethodInfo>(() => this.FindSqlGeometryStaticMethod("STGeomFromText", new Type[]
			{
				this.SqlTypes.SqlCharsType,
				typeof(int)
			}));
			this.smi_SqlGeometry_STPointFromText = new Singleton<MethodInfo>(() => this.FindSqlGeometryStaticMethod("STPointFromText", new Type[]
			{
				this.SqlTypes.SqlCharsType,
				typeof(int)
			}));
			this.smi_SqlGeometry_STLineFromText = new Singleton<MethodInfo>(() => this.FindSqlGeometryStaticMethod("STLineFromText", new Type[]
			{
				this.SqlTypes.SqlCharsType,
				typeof(int)
			}));
			this.smi_SqlGeometry_STPolyFromText = new Singleton<MethodInfo>(() => this.FindSqlGeometryStaticMethod("STPolyFromText", new Type[]
			{
				this.SqlTypes.SqlCharsType,
				typeof(int)
			}));
			this.smi_SqlGeometry_STMPointFromText = new Singleton<MethodInfo>(() => this.FindSqlGeometryStaticMethod("STMPointFromText", new Type[]
			{
				this.SqlTypes.SqlCharsType,
				typeof(int)
			}));
			this.smi_SqlGeometry_STMLineFromText = new Singleton<MethodInfo>(() => this.FindSqlGeometryStaticMethod("STMLineFromText", new Type[]
			{
				this.SqlTypes.SqlCharsType,
				typeof(int)
			}));
			this.smi_SqlGeometry_STMPolyFromText = new Singleton<MethodInfo>(() => this.FindSqlGeometryStaticMethod("STMPolyFromText", new Type[]
			{
				this.SqlTypes.SqlCharsType,
				typeof(int)
			}));
			this.smi_SqlGeometry_STGeomCollFromText = new Singleton<MethodInfo>(() => this.FindSqlGeometryStaticMethod("STGeomCollFromText", new Type[]
			{
				this.SqlTypes.SqlCharsType,
				typeof(int)
			}));
			this.smi_SqlGeometry_STGeomFromWKB = new Singleton<MethodInfo>(() => this.FindSqlGeometryStaticMethod("STGeomFromWKB", new Type[]
			{
				this.SqlTypes.SqlBytesType,
				typeof(int)
			}));
			this.smi_SqlGeometry_STPointFromWKB = new Singleton<MethodInfo>(() => this.FindSqlGeometryStaticMethod("STPointFromWKB", new Type[]
			{
				this.SqlTypes.SqlBytesType,
				typeof(int)
			}));
			this.smi_SqlGeometry_STLineFromWKB = new Singleton<MethodInfo>(() => this.FindSqlGeometryStaticMethod("STLineFromWKB", new Type[]
			{
				this.SqlTypes.SqlBytesType,
				typeof(int)
			}));
			this.smi_SqlGeometry_STPolyFromWKB = new Singleton<MethodInfo>(() => this.FindSqlGeometryStaticMethod("STPolyFromWKB", new Type[]
			{
				this.SqlTypes.SqlBytesType,
				typeof(int)
			}));
			this.smi_SqlGeometry_STMPointFromWKB = new Singleton<MethodInfo>(() => this.FindSqlGeometryStaticMethod("STMPointFromWKB", new Type[]
			{
				this.SqlTypes.SqlBytesType,
				typeof(int)
			}));
			this.smi_SqlGeometry_STMLineFromWKB = new Singleton<MethodInfo>(() => this.FindSqlGeometryStaticMethod("STMLineFromWKB", new Type[]
			{
				this.SqlTypes.SqlBytesType,
				typeof(int)
			}));
			this.smi_SqlGeometry_STMPolyFromWKB = new Singleton<MethodInfo>(() => this.FindSqlGeometryStaticMethod("STMPolyFromWKB", new Type[]
			{
				this.SqlTypes.SqlBytesType,
				typeof(int)
			}));
			this.smi_SqlGeometry_STGeomCollFromWKB = new Singleton<MethodInfo>(() => this.FindSqlGeometryStaticMethod("STGeomCollFromWKB", new Type[]
			{
				this.SqlTypes.SqlBytesType,
				typeof(int)
			}));
			this.smi_SqlGeometry_GeomFromGml = new Singleton<MethodInfo>(() => this.FindSqlGeometryStaticMethod("GeomFromGml", new Type[]
			{
				this.SqlTypes.SqlXmlType,
				typeof(int)
			}));
			this.ipi_SqlGeometry_STSrid = new Singleton<PropertyInfo>(() => this.FindSqlGeometryProperty("STSrid"));
			this.imi_SqlGeometry_STGeometryType = new Singleton<MethodInfo>(() => this.FindSqlGeometryMethod("STGeometryType", new Type[0]));
			this.imi_SqlGeometry_STDimension = new Singleton<MethodInfo>(() => this.FindSqlGeometryMethod("STDimension", new Type[0]));
			this.imi_SqlGeometry_STEnvelope = new Singleton<MethodInfo>(() => this.FindSqlGeometryMethod("STEnvelope", new Type[0]));
			this.imi_SqlGeometry_STAsBinary = new Singleton<MethodInfo>(() => this.FindSqlGeometryMethod("STAsBinary", new Type[0]));
			this.imi_SqlGeometry_AsGml = new Singleton<MethodInfo>(() => this.FindSqlGeometryMethod("AsGml", new Type[0]));
			this.imi_SqlGeometry_STAsText = new Singleton<MethodInfo>(() => this.FindSqlGeometryMethod("STAsText", new Type[0]));
			this.imi_SqlGeometry_STIsEmpty = new Singleton<MethodInfo>(() => this.FindSqlGeometryMethod("STIsEmpty", new Type[0]));
			this.imi_SqlGeometry_STIsSimple = new Singleton<MethodInfo>(() => this.FindSqlGeometryMethod("STIsSimple", new Type[0]));
			this.imi_SqlGeometry_STBoundary = new Singleton<MethodInfo>(() => this.FindSqlGeometryMethod("STBoundary", new Type[0]));
			this.imi_SqlGeometry_STIsValid = new Singleton<MethodInfo>(() => this.FindSqlGeometryMethod("STIsValid", new Type[0]));
			this.imi_SqlGeometry_STEquals = new Singleton<MethodInfo>(() => this.FindSqlGeometryMethod("STEquals", new Type[]
			{
				this.SqlTypes.SqlGeometryType
			}));
			this.imi_SqlGeometry_STDisjoint = new Singleton<MethodInfo>(() => this.FindSqlGeometryMethod("STDisjoint", new Type[]
			{
				this.SqlTypes.SqlGeometryType
			}));
			this.imi_SqlGeometry_STIntersects = new Singleton<MethodInfo>(() => this.FindSqlGeometryMethod("STIntersects", new Type[]
			{
				this.SqlTypes.SqlGeometryType
			}));
			this.imi_SqlGeometry_STTouches = new Singleton<MethodInfo>(() => this.FindSqlGeometryMethod("STTouches", new Type[]
			{
				this.SqlTypes.SqlGeometryType
			}));
			this.imi_SqlGeometry_STCrosses = new Singleton<MethodInfo>(() => this.FindSqlGeometryMethod("STCrosses", new Type[]
			{
				this.SqlTypes.SqlGeometryType
			}));
			this.imi_SqlGeometry_STWithin = new Singleton<MethodInfo>(() => this.FindSqlGeometryMethod("STWithin", new Type[]
			{
				this.SqlTypes.SqlGeometryType
			}));
			this.imi_SqlGeometry_STContains = new Singleton<MethodInfo>(() => this.FindSqlGeometryMethod("STContains", new Type[]
			{
				this.SqlTypes.SqlGeometryType
			}));
			this.imi_SqlGeometry_STOverlaps = new Singleton<MethodInfo>(() => this.FindSqlGeometryMethod("STOverlaps", new Type[]
			{
				this.SqlTypes.SqlGeometryType
			}));
			this.imi_SqlGeometry_STRelate = new Singleton<MethodInfo>(() => this.FindSqlGeometryMethod("STRelate", new Type[]
			{
				this.SqlTypes.SqlGeometryType,
				typeof(string)
			}));
			this.imi_SqlGeometry_STBuffer = new Singleton<MethodInfo>(() => this.FindSqlGeometryMethod("STBuffer", new Type[]
			{
				typeof(double)
			}));
			this.imi_SqlGeometry_STDistance = new Singleton<MethodInfo>(() => this.FindSqlGeometryMethod("STDistance", new Type[]
			{
				this.SqlTypes.SqlGeometryType
			}));
			this.imi_SqlGeometry_STConvexHull = new Singleton<MethodInfo>(() => this.FindSqlGeometryMethod("STConvexHull", new Type[0]));
			this.imi_SqlGeometry_STIntersection = new Singleton<MethodInfo>(() => this.FindSqlGeometryMethod("STIntersection", new Type[]
			{
				this.SqlTypes.SqlGeometryType
			}));
			this.imi_SqlGeometry_STUnion = new Singleton<MethodInfo>(() => this.FindSqlGeometryMethod("STUnion", new Type[]
			{
				this.SqlTypes.SqlGeometryType
			}));
			this.imi_SqlGeometry_STDifference = new Singleton<MethodInfo>(() => this.FindSqlGeometryMethod("STDifference", new Type[]
			{
				this.SqlTypes.SqlGeometryType
			}));
			this.imi_SqlGeometry_STSymDifference = new Singleton<MethodInfo>(() => this.FindSqlGeometryMethod("STSymDifference", new Type[]
			{
				this.SqlTypes.SqlGeometryType
			}));
			this.imi_SqlGeometry_STNumGeometries = new Singleton<MethodInfo>(() => this.FindSqlGeometryMethod("STNumGeometries", new Type[0]));
			this.imi_SqlGeometry_STGeometryN = new Singleton<MethodInfo>(() => this.FindSqlGeometryMethod("STGeometryN", new Type[]
			{
				typeof(int)
			}));
			this.ipi_SqlGeometry_STX = new Singleton<PropertyInfo>(() => this.FindSqlGeometryProperty("STX"));
			this.ipi_SqlGeometry_STY = new Singleton<PropertyInfo>(() => this.FindSqlGeometryProperty("STY"));
			this.ipi_SqlGeometry_Z = new Singleton<PropertyInfo>(() => this.FindSqlGeometryProperty("Z"));
			this.ipi_SqlGeometry_M = new Singleton<PropertyInfo>(() => this.FindSqlGeometryProperty("M"));
			this.imi_SqlGeometry_STLength = new Singleton<MethodInfo>(() => this.FindSqlGeometryMethod("STLength", new Type[0]));
			this.imi_SqlGeometry_STStartPoint = new Singleton<MethodInfo>(() => this.FindSqlGeometryMethod("STStartPoint", new Type[0]));
			this.imi_SqlGeometry_STEndPoint = new Singleton<MethodInfo>(() => this.FindSqlGeometryMethod("STEndPoint", new Type[0]));
			this.imi_SqlGeometry_STIsClosed = new Singleton<MethodInfo>(() => this.FindSqlGeometryMethod("STIsClosed", new Type[0]));
			this.imi_SqlGeometry_STIsRing = new Singleton<MethodInfo>(() => this.FindSqlGeometryMethod("STIsRing", new Type[0]));
			this.imi_SqlGeometry_STNumPoints = new Singleton<MethodInfo>(() => this.FindSqlGeometryMethod("STNumPoints", new Type[0]));
			this.imi_SqlGeometry_STPointN = new Singleton<MethodInfo>(() => this.FindSqlGeometryMethod("STPointN", new Type[]
			{
				typeof(int)
			}));
			this.imi_SqlGeometry_STArea = new Singleton<MethodInfo>(() => this.FindSqlGeometryMethod("STArea", new Type[0]));
			this.imi_SqlGeometry_STCentroid = new Singleton<MethodInfo>(() => this.FindSqlGeometryMethod("STCentroid", new Type[0]));
			this.imi_SqlGeometry_STPointOnSurface = new Singleton<MethodInfo>(() => this.FindSqlGeometryMethod("STPointOnSurface", new Type[0]));
			this.imi_SqlGeometry_STExteriorRing = new Singleton<MethodInfo>(() => this.FindSqlGeometryMethod("STExteriorRing", new Type[0]));
			this.imi_SqlGeometry_STNumInteriorRing = new Singleton<MethodInfo>(() => this.FindSqlGeometryMethod("STNumInteriorRing", new Type[0]));
			this.imi_SqlGeometry_STInteriorRingN = new Singleton<MethodInfo>(() => this.FindSqlGeometryMethod("STInteriorRingN", new Type[]
			{
				typeof(int)
			}));
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x0000B6EC File Offset: 0x000098EC
		private void InitializeMemberInfo(SqlSpatialServices from)
		{
			this.smi_SqlGeography_Parse = from.smi_SqlGeography_Parse;
			this.smi_SqlGeography_STGeomFromText = from.smi_SqlGeography_STGeomFromText;
			this.smi_SqlGeography_STPointFromText = from.smi_SqlGeography_STPointFromText;
			this.smi_SqlGeography_STLineFromText = from.smi_SqlGeography_STLineFromText;
			this.smi_SqlGeography_STPolyFromText = from.smi_SqlGeography_STPolyFromText;
			this.smi_SqlGeography_STMPointFromText = from.smi_SqlGeography_STMPointFromText;
			this.smi_SqlGeography_STMLineFromText = from.smi_SqlGeography_STMLineFromText;
			this.smi_SqlGeography_STMPolyFromText = from.smi_SqlGeography_STMPolyFromText;
			this.smi_SqlGeography_STGeomCollFromText = from.smi_SqlGeography_STGeomCollFromText;
			this.smi_SqlGeography_STGeomFromWKB = from.smi_SqlGeography_STGeomFromWKB;
			this.smi_SqlGeography_STPointFromWKB = from.smi_SqlGeography_STPointFromWKB;
			this.smi_SqlGeography_STLineFromWKB = from.smi_SqlGeography_STLineFromWKB;
			this.smi_SqlGeography_STPolyFromWKB = from.smi_SqlGeography_STPolyFromWKB;
			this.smi_SqlGeography_STMPointFromWKB = from.smi_SqlGeography_STMPointFromWKB;
			this.smi_SqlGeography_STMLineFromWKB = from.smi_SqlGeography_STMLineFromWKB;
			this.smi_SqlGeography_STMPolyFromWKB = from.smi_SqlGeography_STMPolyFromWKB;
			this.smi_SqlGeography_STGeomCollFromWKB = from.smi_SqlGeography_STGeomCollFromWKB;
			this.smi_SqlGeography_GeomFromGml = from.smi_SqlGeography_GeomFromGml;
			this.ipi_SqlGeography_STSrid = from.ipi_SqlGeography_STSrid;
			this.imi_SqlGeography_STGeometryType = from.imi_SqlGeography_STGeometryType;
			this.imi_SqlGeography_STDimension = from.imi_SqlGeography_STDimension;
			this.imi_SqlGeography_STAsBinary = from.imi_SqlGeography_STAsBinary;
			this.imi_SqlGeography_AsGml = from.imi_SqlGeography_AsGml;
			this.imi_SqlGeography_STAsText = from.imi_SqlGeography_STAsText;
			this.imi_SqlGeography_STIsEmpty = from.imi_SqlGeography_STIsEmpty;
			this.imi_SqlGeography_STEquals = from.imi_SqlGeography_STEquals;
			this.imi_SqlGeography_STDisjoint = from.imi_SqlGeography_STDisjoint;
			this.imi_SqlGeography_STIntersects = from.imi_SqlGeography_STIntersects;
			this.imi_SqlGeography_STBuffer = from.imi_SqlGeography_STBuffer;
			this.imi_SqlGeography_STDistance = from.imi_SqlGeography_STDistance;
			this.imi_SqlGeography_STIntersection = from.imi_SqlGeography_STIntersection;
			this.imi_SqlGeography_STUnion = from.imi_SqlGeography_STUnion;
			this.imi_SqlGeography_STDifference = from.imi_SqlGeography_STDifference;
			this.imi_SqlGeography_STSymDifference = from.imi_SqlGeography_STSymDifference;
			this.imi_SqlGeography_STNumGeometries = from.imi_SqlGeography_STNumGeometries;
			this.imi_SqlGeography_STGeometryN = from.imi_SqlGeography_STGeometryN;
			this.ipi_SqlGeography_Lat = from.ipi_SqlGeography_Lat;
			this.ipi_SqlGeography_Long = from.ipi_SqlGeography_Long;
			this.ipi_SqlGeography_Z = from.ipi_SqlGeography_Z;
			this.ipi_SqlGeography_M = from.ipi_SqlGeography_M;
			this.imi_SqlGeography_STLength = from.imi_SqlGeography_STLength;
			this.imi_SqlGeography_STStartPoint = from.imi_SqlGeography_STStartPoint;
			this.imi_SqlGeography_STEndPoint = from.imi_SqlGeography_STEndPoint;
			this.imi_SqlGeography_STIsClosed = from.imi_SqlGeography_STIsClosed;
			this.imi_SqlGeography_STNumPoints = from.imi_SqlGeography_STNumPoints;
			this.imi_SqlGeography_STPointN = from.imi_SqlGeography_STPointN;
			this.imi_SqlGeography_STArea = from.imi_SqlGeography_STArea;
			this.smi_SqlGeometry_Parse = from.smi_SqlGeometry_Parse;
			this.smi_SqlGeometry_STGeomFromText = from.smi_SqlGeometry_STGeomFromText;
			this.smi_SqlGeometry_STPointFromText = from.smi_SqlGeometry_STPointFromText;
			this.smi_SqlGeometry_STLineFromText = from.smi_SqlGeometry_STLineFromText;
			this.smi_SqlGeometry_STPolyFromText = from.smi_SqlGeometry_STPolyFromText;
			this.smi_SqlGeometry_STMPointFromText = from.smi_SqlGeometry_STMPointFromText;
			this.smi_SqlGeometry_STMLineFromText = from.smi_SqlGeometry_STMLineFromText;
			this.smi_SqlGeometry_STMPolyFromText = from.smi_SqlGeometry_STMPolyFromText;
			this.smi_SqlGeometry_STGeomCollFromText = from.smi_SqlGeometry_STGeomCollFromText;
			this.smi_SqlGeometry_STGeomFromWKB = from.smi_SqlGeometry_STGeomFromWKB;
			this.smi_SqlGeometry_STPointFromWKB = from.smi_SqlGeometry_STPointFromWKB;
			this.smi_SqlGeometry_STLineFromWKB = from.smi_SqlGeometry_STLineFromWKB;
			this.smi_SqlGeometry_STPolyFromWKB = from.smi_SqlGeometry_STPolyFromWKB;
			this.smi_SqlGeometry_STMPointFromWKB = from.smi_SqlGeometry_STMPointFromWKB;
			this.smi_SqlGeometry_STMLineFromWKB = from.smi_SqlGeometry_STMLineFromWKB;
			this.smi_SqlGeometry_STMPolyFromWKB = from.smi_SqlGeometry_STMPolyFromWKB;
			this.smi_SqlGeometry_STGeomCollFromWKB = from.smi_SqlGeometry_STGeomCollFromWKB;
			this.smi_SqlGeometry_GeomFromGml = from.smi_SqlGeometry_GeomFromGml;
			this.ipi_SqlGeometry_STSrid = from.ipi_SqlGeometry_STSrid;
			this.imi_SqlGeometry_STGeometryType = from.imi_SqlGeometry_STGeometryType;
			this.imi_SqlGeometry_STDimension = from.imi_SqlGeometry_STDimension;
			this.imi_SqlGeometry_STEnvelope = from.imi_SqlGeometry_STEnvelope;
			this.imi_SqlGeometry_STAsBinary = from.imi_SqlGeometry_STAsBinary;
			this.imi_SqlGeometry_AsGml = from.imi_SqlGeometry_AsGml;
			this.imi_SqlGeometry_STAsText = from.imi_SqlGeometry_STAsText;
			this.imi_SqlGeometry_STIsEmpty = from.imi_SqlGeometry_STIsEmpty;
			this.imi_SqlGeometry_STIsSimple = from.imi_SqlGeometry_STIsSimple;
			this.imi_SqlGeometry_STBoundary = from.imi_SqlGeometry_STBoundary;
			this.imi_SqlGeometry_STIsValid = from.imi_SqlGeometry_STIsValid;
			this.imi_SqlGeometry_STEquals = from.imi_SqlGeometry_STEquals;
			this.imi_SqlGeometry_STDisjoint = from.imi_SqlGeometry_STDisjoint;
			this.imi_SqlGeometry_STIntersects = from.imi_SqlGeometry_STIntersects;
			this.imi_SqlGeometry_STTouches = from.imi_SqlGeometry_STTouches;
			this.imi_SqlGeometry_STCrosses = from.imi_SqlGeometry_STCrosses;
			this.imi_SqlGeometry_STWithin = from.imi_SqlGeometry_STWithin;
			this.imi_SqlGeometry_STContains = from.imi_SqlGeometry_STContains;
			this.imi_SqlGeometry_STOverlaps = from.imi_SqlGeometry_STOverlaps;
			this.imi_SqlGeometry_STRelate = from.imi_SqlGeometry_STRelate;
			this.imi_SqlGeometry_STBuffer = from.imi_SqlGeometry_STBuffer;
			this.imi_SqlGeometry_STDistance = from.imi_SqlGeometry_STDistance;
			this.imi_SqlGeometry_STConvexHull = from.imi_SqlGeometry_STConvexHull;
			this.imi_SqlGeometry_STIntersection = from.imi_SqlGeometry_STIntersection;
			this.imi_SqlGeometry_STUnion = from.imi_SqlGeometry_STUnion;
			this.imi_SqlGeometry_STDifference = from.imi_SqlGeometry_STDifference;
			this.imi_SqlGeometry_STSymDifference = from.imi_SqlGeometry_STSymDifference;
			this.imi_SqlGeometry_STNumGeometries = from.imi_SqlGeometry_STNumGeometries;
			this.imi_SqlGeometry_STGeometryN = from.imi_SqlGeometry_STGeometryN;
			this.ipi_SqlGeometry_STX = from.ipi_SqlGeometry_STX;
			this.ipi_SqlGeometry_STY = from.ipi_SqlGeometry_STY;
			this.ipi_SqlGeometry_Z = from.ipi_SqlGeometry_Z;
			this.ipi_SqlGeometry_M = from.ipi_SqlGeometry_M;
			this.imi_SqlGeometry_STLength = from.imi_SqlGeometry_STLength;
			this.imi_SqlGeometry_STStartPoint = from.imi_SqlGeometry_STStartPoint;
			this.imi_SqlGeometry_STEndPoint = from.imi_SqlGeometry_STEndPoint;
			this.imi_SqlGeometry_STIsClosed = from.imi_SqlGeometry_STIsClosed;
			this.imi_SqlGeometry_STIsRing = from.imi_SqlGeometry_STIsRing;
			this.imi_SqlGeometry_STNumPoints = from.imi_SqlGeometry_STNumPoints;
			this.imi_SqlGeometry_STPointN = from.imi_SqlGeometry_STPointN;
			this.imi_SqlGeometry_STArea = from.imi_SqlGeometry_STArea;
			this.imi_SqlGeometry_STCentroid = from.imi_SqlGeometry_STCentroid;
			this.imi_SqlGeometry_STPointOnSurface = from.imi_SqlGeometry_STPointOnSurface;
			this.imi_SqlGeometry_STExteriorRing = from.imi_SqlGeometry_STExteriorRing;
			this.imi_SqlGeometry_STNumInteriorRing = from.imi_SqlGeometry_STNumInteriorRing;
			this.imi_SqlGeometry_STInteriorRingN = from.imi_SqlGeometry_STInteriorRingN;
		}

		// Token: 0x04000659 RID: 1625
		internal static readonly SqlSpatialServices Instance = new SqlSpatialServices(new Func<SqlTypesAssembly>(SqlProviderServices.GetSqlTypesAssembly));

		// Token: 0x0400065A RID: 1626
		private static Dictionary<string, SqlSpatialServices> otherSpatialServices;

		// Token: 0x0400065B RID: 1627
		[NonSerialized]
		private readonly Singleton<SqlTypesAssembly> _sqlTypesAssemblySingleton;

		// Token: 0x0400065C RID: 1628
		[NonSerialized]
		private Singleton<MethodInfo> smi_SqlGeography_Parse;

		// Token: 0x0400065D RID: 1629
		[NonSerialized]
		private Singleton<MethodInfo> smi_SqlGeography_STGeomFromText;

		// Token: 0x0400065E RID: 1630
		[NonSerialized]
		private Singleton<MethodInfo> smi_SqlGeography_STPointFromText;

		// Token: 0x0400065F RID: 1631
		[NonSerialized]
		private Singleton<MethodInfo> smi_SqlGeography_STLineFromText;

		// Token: 0x04000660 RID: 1632
		[NonSerialized]
		private Singleton<MethodInfo> smi_SqlGeography_STPolyFromText;

		// Token: 0x04000661 RID: 1633
		[NonSerialized]
		private Singleton<MethodInfo> smi_SqlGeography_STMPointFromText;

		// Token: 0x04000662 RID: 1634
		[NonSerialized]
		private Singleton<MethodInfo> smi_SqlGeography_STMLineFromText;

		// Token: 0x04000663 RID: 1635
		[NonSerialized]
		private Singleton<MethodInfo> smi_SqlGeography_STMPolyFromText;

		// Token: 0x04000664 RID: 1636
		[NonSerialized]
		private Singleton<MethodInfo> smi_SqlGeography_STGeomCollFromText;

		// Token: 0x04000665 RID: 1637
		[NonSerialized]
		private Singleton<MethodInfo> smi_SqlGeography_STGeomFromWKB;

		// Token: 0x04000666 RID: 1638
		[NonSerialized]
		private Singleton<MethodInfo> smi_SqlGeography_STPointFromWKB;

		// Token: 0x04000667 RID: 1639
		[NonSerialized]
		private Singleton<MethodInfo> smi_SqlGeography_STLineFromWKB;

		// Token: 0x04000668 RID: 1640
		[NonSerialized]
		private Singleton<MethodInfo> smi_SqlGeography_STPolyFromWKB;

		// Token: 0x04000669 RID: 1641
		[NonSerialized]
		private Singleton<MethodInfo> smi_SqlGeography_STMPointFromWKB;

		// Token: 0x0400066A RID: 1642
		[NonSerialized]
		private Singleton<MethodInfo> smi_SqlGeography_STMLineFromWKB;

		// Token: 0x0400066B RID: 1643
		[NonSerialized]
		private Singleton<MethodInfo> smi_SqlGeography_STMPolyFromWKB;

		// Token: 0x0400066C RID: 1644
		[NonSerialized]
		private Singleton<MethodInfo> smi_SqlGeography_STGeomCollFromWKB;

		// Token: 0x0400066D RID: 1645
		[NonSerialized]
		private Singleton<MethodInfo> smi_SqlGeography_GeomFromGml;

		// Token: 0x0400066E RID: 1646
		[NonSerialized]
		private Singleton<PropertyInfo> ipi_SqlGeography_STSrid;

		// Token: 0x0400066F RID: 1647
		[NonSerialized]
		private Singleton<MethodInfo> imi_SqlGeography_STGeometryType;

		// Token: 0x04000670 RID: 1648
		[NonSerialized]
		private Singleton<MethodInfo> imi_SqlGeography_STDimension;

		// Token: 0x04000671 RID: 1649
		[NonSerialized]
		private Singleton<MethodInfo> imi_SqlGeography_STAsBinary;

		// Token: 0x04000672 RID: 1650
		[NonSerialized]
		private Singleton<MethodInfo> imi_SqlGeography_AsGml;

		// Token: 0x04000673 RID: 1651
		[NonSerialized]
		private Singleton<MethodInfo> imi_SqlGeography_STAsText;

		// Token: 0x04000674 RID: 1652
		[NonSerialized]
		private Singleton<MethodInfo> imi_SqlGeography_STIsEmpty;

		// Token: 0x04000675 RID: 1653
		[NonSerialized]
		private Singleton<MethodInfo> imi_SqlGeography_STEquals;

		// Token: 0x04000676 RID: 1654
		[NonSerialized]
		private Singleton<MethodInfo> imi_SqlGeography_STDisjoint;

		// Token: 0x04000677 RID: 1655
		[NonSerialized]
		private Singleton<MethodInfo> imi_SqlGeography_STIntersects;

		// Token: 0x04000678 RID: 1656
		[NonSerialized]
		private Singleton<MethodInfo> imi_SqlGeography_STBuffer;

		// Token: 0x04000679 RID: 1657
		[NonSerialized]
		private Singleton<MethodInfo> imi_SqlGeography_STDistance;

		// Token: 0x0400067A RID: 1658
		[NonSerialized]
		private Singleton<MethodInfo> imi_SqlGeography_STIntersection;

		// Token: 0x0400067B RID: 1659
		[NonSerialized]
		private Singleton<MethodInfo> imi_SqlGeography_STUnion;

		// Token: 0x0400067C RID: 1660
		[NonSerialized]
		private Singleton<MethodInfo> imi_SqlGeography_STDifference;

		// Token: 0x0400067D RID: 1661
		[NonSerialized]
		private Singleton<MethodInfo> imi_SqlGeography_STSymDifference;

		// Token: 0x0400067E RID: 1662
		[NonSerialized]
		private Singleton<MethodInfo> imi_SqlGeography_STNumGeometries;

		// Token: 0x0400067F RID: 1663
		[NonSerialized]
		private Singleton<MethodInfo> imi_SqlGeography_STGeometryN;

		// Token: 0x04000680 RID: 1664
		[NonSerialized]
		private Singleton<PropertyInfo> ipi_SqlGeography_Lat;

		// Token: 0x04000681 RID: 1665
		[NonSerialized]
		private Singleton<PropertyInfo> ipi_SqlGeography_Long;

		// Token: 0x04000682 RID: 1666
		[NonSerialized]
		private Singleton<PropertyInfo> ipi_SqlGeography_Z;

		// Token: 0x04000683 RID: 1667
		[NonSerialized]
		private Singleton<PropertyInfo> ipi_SqlGeography_M;

		// Token: 0x04000684 RID: 1668
		[NonSerialized]
		private Singleton<MethodInfo> imi_SqlGeography_STLength;

		// Token: 0x04000685 RID: 1669
		[NonSerialized]
		private Singleton<MethodInfo> imi_SqlGeography_STStartPoint;

		// Token: 0x04000686 RID: 1670
		[NonSerialized]
		private Singleton<MethodInfo> imi_SqlGeography_STEndPoint;

		// Token: 0x04000687 RID: 1671
		[NonSerialized]
		private Singleton<MethodInfo> imi_SqlGeography_STIsClosed;

		// Token: 0x04000688 RID: 1672
		[NonSerialized]
		private Singleton<MethodInfo> imi_SqlGeography_STNumPoints;

		// Token: 0x04000689 RID: 1673
		[NonSerialized]
		private Singleton<MethodInfo> imi_SqlGeography_STPointN;

		// Token: 0x0400068A RID: 1674
		[NonSerialized]
		private Singleton<MethodInfo> imi_SqlGeography_STArea;

		// Token: 0x0400068B RID: 1675
		[NonSerialized]
		private Singleton<MethodInfo> smi_SqlGeometry_Parse;

		// Token: 0x0400068C RID: 1676
		[NonSerialized]
		private Singleton<MethodInfo> smi_SqlGeometry_STGeomFromText;

		// Token: 0x0400068D RID: 1677
		[NonSerialized]
		private Singleton<MethodInfo> smi_SqlGeometry_STPointFromText;

		// Token: 0x0400068E RID: 1678
		[NonSerialized]
		private Singleton<MethodInfo> smi_SqlGeometry_STLineFromText;

		// Token: 0x0400068F RID: 1679
		[NonSerialized]
		private Singleton<MethodInfo> smi_SqlGeometry_STPolyFromText;

		// Token: 0x04000690 RID: 1680
		[NonSerialized]
		private Singleton<MethodInfo> smi_SqlGeometry_STMPointFromText;

		// Token: 0x04000691 RID: 1681
		[NonSerialized]
		private Singleton<MethodInfo> smi_SqlGeometry_STMLineFromText;

		// Token: 0x04000692 RID: 1682
		[NonSerialized]
		private Singleton<MethodInfo> smi_SqlGeometry_STMPolyFromText;

		// Token: 0x04000693 RID: 1683
		[NonSerialized]
		private Singleton<MethodInfo> smi_SqlGeometry_STGeomCollFromText;

		// Token: 0x04000694 RID: 1684
		[NonSerialized]
		private Singleton<MethodInfo> smi_SqlGeometry_STGeomFromWKB;

		// Token: 0x04000695 RID: 1685
		[NonSerialized]
		private Singleton<MethodInfo> smi_SqlGeometry_STPointFromWKB;

		// Token: 0x04000696 RID: 1686
		[NonSerialized]
		private Singleton<MethodInfo> smi_SqlGeometry_STLineFromWKB;

		// Token: 0x04000697 RID: 1687
		[NonSerialized]
		private Singleton<MethodInfo> smi_SqlGeometry_STPolyFromWKB;

		// Token: 0x04000698 RID: 1688
		[NonSerialized]
		private Singleton<MethodInfo> smi_SqlGeometry_STMPointFromWKB;

		// Token: 0x04000699 RID: 1689
		[NonSerialized]
		private Singleton<MethodInfo> smi_SqlGeometry_STMLineFromWKB;

		// Token: 0x0400069A RID: 1690
		[NonSerialized]
		private Singleton<MethodInfo> smi_SqlGeometry_STMPolyFromWKB;

		// Token: 0x0400069B RID: 1691
		[NonSerialized]
		private Singleton<MethodInfo> smi_SqlGeometry_STGeomCollFromWKB;

		// Token: 0x0400069C RID: 1692
		[NonSerialized]
		private Singleton<MethodInfo> smi_SqlGeometry_GeomFromGml;

		// Token: 0x0400069D RID: 1693
		[NonSerialized]
		private Singleton<PropertyInfo> ipi_SqlGeometry_STSrid;

		// Token: 0x0400069E RID: 1694
		[NonSerialized]
		private Singleton<MethodInfo> imi_SqlGeometry_STGeometryType;

		// Token: 0x0400069F RID: 1695
		[NonSerialized]
		private Singleton<MethodInfo> imi_SqlGeometry_STDimension;

		// Token: 0x040006A0 RID: 1696
		[NonSerialized]
		private Singleton<MethodInfo> imi_SqlGeometry_STEnvelope;

		// Token: 0x040006A1 RID: 1697
		[NonSerialized]
		private Singleton<MethodInfo> imi_SqlGeometry_STAsBinary;

		// Token: 0x040006A2 RID: 1698
		[NonSerialized]
		private Singleton<MethodInfo> imi_SqlGeometry_AsGml;

		// Token: 0x040006A3 RID: 1699
		[NonSerialized]
		private Singleton<MethodInfo> imi_SqlGeometry_STAsText;

		// Token: 0x040006A4 RID: 1700
		[NonSerialized]
		private Singleton<MethodInfo> imi_SqlGeometry_STIsEmpty;

		// Token: 0x040006A5 RID: 1701
		[NonSerialized]
		private Singleton<MethodInfo> imi_SqlGeometry_STIsSimple;

		// Token: 0x040006A6 RID: 1702
		[NonSerialized]
		private Singleton<MethodInfo> imi_SqlGeometry_STBoundary;

		// Token: 0x040006A7 RID: 1703
		[NonSerialized]
		private Singleton<MethodInfo> imi_SqlGeometry_STIsValid;

		// Token: 0x040006A8 RID: 1704
		[NonSerialized]
		private Singleton<MethodInfo> imi_SqlGeometry_STEquals;

		// Token: 0x040006A9 RID: 1705
		[NonSerialized]
		private Singleton<MethodInfo> imi_SqlGeometry_STDisjoint;

		// Token: 0x040006AA RID: 1706
		[NonSerialized]
		private Singleton<MethodInfo> imi_SqlGeometry_STIntersects;

		// Token: 0x040006AB RID: 1707
		[NonSerialized]
		private Singleton<MethodInfo> imi_SqlGeometry_STTouches;

		// Token: 0x040006AC RID: 1708
		[NonSerialized]
		private Singleton<MethodInfo> imi_SqlGeometry_STCrosses;

		// Token: 0x040006AD RID: 1709
		[NonSerialized]
		private Singleton<MethodInfo> imi_SqlGeometry_STWithin;

		// Token: 0x040006AE RID: 1710
		[NonSerialized]
		private Singleton<MethodInfo> imi_SqlGeometry_STContains;

		// Token: 0x040006AF RID: 1711
		[NonSerialized]
		private Singleton<MethodInfo> imi_SqlGeometry_STOverlaps;

		// Token: 0x040006B0 RID: 1712
		[NonSerialized]
		private Singleton<MethodInfo> imi_SqlGeometry_STRelate;

		// Token: 0x040006B1 RID: 1713
		[NonSerialized]
		private Singleton<MethodInfo> imi_SqlGeometry_STBuffer;

		// Token: 0x040006B2 RID: 1714
		[NonSerialized]
		private Singleton<MethodInfo> imi_SqlGeometry_STDistance;

		// Token: 0x040006B3 RID: 1715
		[NonSerialized]
		private Singleton<MethodInfo> imi_SqlGeometry_STConvexHull;

		// Token: 0x040006B4 RID: 1716
		[NonSerialized]
		private Singleton<MethodInfo> imi_SqlGeometry_STIntersection;

		// Token: 0x040006B5 RID: 1717
		[NonSerialized]
		private Singleton<MethodInfo> imi_SqlGeometry_STUnion;

		// Token: 0x040006B6 RID: 1718
		[NonSerialized]
		private Singleton<MethodInfo> imi_SqlGeometry_STDifference;

		// Token: 0x040006B7 RID: 1719
		[NonSerialized]
		private Singleton<MethodInfo> imi_SqlGeometry_STSymDifference;

		// Token: 0x040006B8 RID: 1720
		[NonSerialized]
		private Singleton<MethodInfo> imi_SqlGeometry_STNumGeometries;

		// Token: 0x040006B9 RID: 1721
		[NonSerialized]
		private Singleton<MethodInfo> imi_SqlGeometry_STGeometryN;

		// Token: 0x040006BA RID: 1722
		[NonSerialized]
		private Singleton<PropertyInfo> ipi_SqlGeometry_STX;

		// Token: 0x040006BB RID: 1723
		[NonSerialized]
		private Singleton<PropertyInfo> ipi_SqlGeometry_STY;

		// Token: 0x040006BC RID: 1724
		[NonSerialized]
		private Singleton<PropertyInfo> ipi_SqlGeometry_Z;

		// Token: 0x040006BD RID: 1725
		[NonSerialized]
		private Singleton<PropertyInfo> ipi_SqlGeometry_M;

		// Token: 0x040006BE RID: 1726
		[NonSerialized]
		private Singleton<MethodInfo> imi_SqlGeometry_STLength;

		// Token: 0x040006BF RID: 1727
		[NonSerialized]
		private Singleton<MethodInfo> imi_SqlGeometry_STStartPoint;

		// Token: 0x040006C0 RID: 1728
		[NonSerialized]
		private Singleton<MethodInfo> imi_SqlGeometry_STEndPoint;

		// Token: 0x040006C1 RID: 1729
		[NonSerialized]
		private Singleton<MethodInfo> imi_SqlGeometry_STIsClosed;

		// Token: 0x040006C2 RID: 1730
		[NonSerialized]
		private Singleton<MethodInfo> imi_SqlGeometry_STIsRing;

		// Token: 0x040006C3 RID: 1731
		[NonSerialized]
		private Singleton<MethodInfo> imi_SqlGeometry_STNumPoints;

		// Token: 0x040006C4 RID: 1732
		[NonSerialized]
		private Singleton<MethodInfo> imi_SqlGeometry_STPointN;

		// Token: 0x040006C5 RID: 1733
		[NonSerialized]
		private Singleton<MethodInfo> imi_SqlGeometry_STArea;

		// Token: 0x040006C6 RID: 1734
		[NonSerialized]
		private Singleton<MethodInfo> imi_SqlGeometry_STCentroid;

		// Token: 0x040006C7 RID: 1735
		[NonSerialized]
		private Singleton<MethodInfo> imi_SqlGeometry_STPointOnSurface;

		// Token: 0x040006C8 RID: 1736
		[NonSerialized]
		private Singleton<MethodInfo> imi_SqlGeometry_STExteriorRing;

		// Token: 0x040006C9 RID: 1737
		[NonSerialized]
		private Singleton<MethodInfo> imi_SqlGeometry_STNumInteriorRing;

		// Token: 0x040006CA RID: 1738
		[NonSerialized]
		private Singleton<MethodInfo> imi_SqlGeometry_STInteriorRingN;
	}
}
