using System;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Data.Entity.SqlServer.Resources;
using System.Data.Entity.SqlServer.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace System.Data.Entity.SqlServer
{
	// Token: 0x02000046 RID: 70
	[Serializable]
	public class SqlSpatialServices : DbSpatialServices
	{
		// Token: 0x0600049F RID: 1183 RVA: 0x000175D1 File Offset: 0x000157D1
		internal SqlSpatialServices()
		{
		}

		// Token: 0x060004A0 RID: 1184 RVA: 0x000175D9 File Offset: 0x000157D9
		internal SqlSpatialServices(SqlTypesAssemblyLoader loader)
		{
			this._loader = loader;
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060004A1 RID: 1185 RVA: 0x000175E8 File Offset: 0x000157E8
		public override bool NativeTypesAvailable
		{
			get
			{
				return (this._loader ?? SqlTypesAssemblyLoader.DefaultInstance).TryGetSqlTypesAssembly() != null;
			}
		}

		// Token: 0x060004A2 RID: 1186 RVA: 0x00017604 File Offset: 0x00015804
		private static bool TryGetSpatialServiceFromAssembly(Assembly assembly, out SqlSpatialServices services)
		{
			if (SqlSpatialServices._otherSpatialServices == null || !SqlSpatialServices._otherSpatialServices.TryGetValue(assembly.FullName, out services))
			{
				lock (SqlSpatialServices.Instance)
				{
					if (SqlSpatialServices._otherSpatialServices == null || !SqlSpatialServices._otherSpatialServices.TryGetValue(assembly.FullName, out services))
					{
						SqlTypesAssembly assembly2;
						if (SqlTypesAssemblyLoader.DefaultInstance.TryGetSqlTypesAssembly(assembly, out assembly2))
						{
							if (SqlSpatialServices._otherSpatialServices == null)
							{
								SqlSpatialServices._otherSpatialServices = new Dictionary<string, SqlSpatialServices>(1);
							}
							services = new SqlSpatialServices(new SqlTypesAssemblyLoader(assembly2));
							SqlSpatialServices._otherSpatialServices.Add(assembly.FullName, services);
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

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060004A3 RID: 1187 RVA: 0x000176C0 File Offset: 0x000158C0
		internal SqlTypesAssembly SqlTypes
		{
			get
			{
				return (this._loader ?? SqlTypesAssemblyLoader.DefaultInstance).GetSqlTypesAssembly();
			}
		}

		// Token: 0x060004A4 RID: 1188 RVA: 0x000176D8 File Offset: 0x000158D8
		public override object CreateProviderValue(DbGeographyWellKnownValue wellKnownValue)
		{
			Check.NotNull<DbGeographyWellKnownValue>(wellKnownValue, "wellKnownValue");
			object result;
			if (wellKnownValue.WellKnownText != null)
			{
				result = this.SqlTypes.SqlTypesGeographyFromText(wellKnownValue.WellKnownText, wellKnownValue.CoordinateSystemId);
			}
			else
			{
				if (wellKnownValue.WellKnownBinary == null)
				{
					throw new ArgumentException(Strings.Spatial_WellKnownGeographyValueNotValid, "wellKnownValue");
				}
				result = this.SqlTypes.SqlTypesGeographyFromBinary(wellKnownValue.WellKnownBinary, wellKnownValue.CoordinateSystemId);
			}
			return result;
		}

		// Token: 0x060004A5 RID: 1189 RVA: 0x00017748 File Offset: 0x00015948
		public override DbGeography GeographyFromProviderValue(object providerValue)
		{
			Check.NotNull<object>(providerValue, "providerValue");
			object obj = this.NormalizeProviderValue(providerValue, this.SqlTypes.SqlGeographyType);
			if (!this.SqlTypes.IsSqlGeographyNull(obj))
			{
				return DbSpatialServices.CreateGeography(this, obj);
			}
			return null;
		}

		// Token: 0x060004A6 RID: 1190 RVA: 0x0001778C File Offset: 0x0001598C
		private object NormalizeProviderValue(object providerValue, Type expectedSpatialType)
		{
			Type type = providerValue.GetType();
			if (type != expectedSpatialType)
			{
				SqlSpatialServices sqlSpatialServices;
				if (SqlSpatialServices.TryGetSpatialServiceFromAssembly(providerValue.GetType().Assembly(), out sqlSpatialServices))
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
				throw new ArgumentException(Strings.SqlSpatialServices_ProviderValueNotSqlType(expectedSpatialType.AssemblyQualifiedName), "providerValue");
			}
			return providerValue;
		}

		// Token: 0x060004A7 RID: 1191 RVA: 0x00017884 File Offset: 0x00015A84
		[SuppressMessage("Microsoft.Usage", "CA2208:InstantiateArgumentExceptionsCorrectly")]
		public override DbGeographyWellKnownValue CreateWellKnownValue(DbGeography geographyValue)
		{
			Check.NotNull<DbGeography>(geographyValue, "geographyValue");
			IDbSpatialValue spatialValue = geographyValue.AsSpatialValue();
			return SqlSpatialServices.CreateWellKnownValue<DbGeographyWellKnownValue>(spatialValue, () => new ArgumentException(Strings.SqlSpatialservices_CouldNotCreateWellKnownGeographyValueNoSrid, "geographyValue"), () => new ArgumentException(Strings.SqlSpatialservices_CouldNotCreateWellKnownGeographyValueNoWkbOrWkt, "geographyValue"), (int coordinateSystemId, byte[] wkb, string wkt) => new DbGeographyWellKnownValue
			{
				CoordinateSystemId = coordinateSystemId,
				WellKnownBinary = wkb,
				WellKnownText = wkt
			});
		}

		// Token: 0x060004A8 RID: 1192 RVA: 0x00017904 File Offset: 0x00015B04
		public override object CreateProviderValue(DbGeometryWellKnownValue wellKnownValue)
		{
			Check.NotNull<DbGeometryWellKnownValue>(wellKnownValue, "wellKnownValue");
			object result;
			if (wellKnownValue.WellKnownText != null)
			{
				result = this.SqlTypes.SqlTypesGeometryFromText(wellKnownValue.WellKnownText, wellKnownValue.CoordinateSystemId);
			}
			else
			{
				if (wellKnownValue.WellKnownBinary == null)
				{
					throw new ArgumentException(Strings.Spatial_WellKnownGeometryValueNotValid, "wellKnownValue");
				}
				result = this.SqlTypes.SqlTypesGeometryFromBinary(wellKnownValue.WellKnownBinary, wellKnownValue.CoordinateSystemId);
			}
			return result;
		}

		// Token: 0x060004A9 RID: 1193 RVA: 0x00017974 File Offset: 0x00015B74
		public override DbGeometry GeometryFromProviderValue(object providerValue)
		{
			Check.NotNull<object>(providerValue, "providerValue");
			object obj = this.NormalizeProviderValue(providerValue, this.SqlTypes.SqlGeometryType);
			if (!this.SqlTypes.IsSqlGeometryNull(obj))
			{
				return DbSpatialServices.CreateGeometry(this, obj);
			}
			return null;
		}

		// Token: 0x060004AA RID: 1194 RVA: 0x00017A08 File Offset: 0x00015C08
		[SuppressMessage("Microsoft.Usage", "CA2208:InstantiateArgumentExceptionsCorrectly")]
		public override DbGeometryWellKnownValue CreateWellKnownValue(DbGeometry geometryValue)
		{
			Check.NotNull<DbGeometry>(geometryValue, "geometryValue");
			IDbSpatialValue spatialValue = geometryValue.AsSpatialValue();
			return SqlSpatialServices.CreateWellKnownValue<DbGeometryWellKnownValue>(spatialValue, () => new ArgumentException(Strings.SqlSpatialservices_CouldNotCreateWellKnownGeometryValueNoSrid, "geometryValue"), () => new ArgumentException(Strings.SqlSpatialservices_CouldNotCreateWellKnownGeometryValueNoWkbOrWkt, "geometryValue"), (int coordinateSystemId, byte[] wkb, string wkt) => new DbGeometryWellKnownValue
			{
				CoordinateSystemId = coordinateSystemId,
				WellKnownBinary = wkb,
				WellKnownText = wkt
			});
		}

		// Token: 0x060004AB RID: 1195 RVA: 0x00017A88 File Offset: 0x00015C88
		private static TValue CreateWellKnownValue<TValue>(IDbSpatialValue spatialValue, Func<Exception> onMissingcoordinateSystemId, Func<Exception> onMissingWkbAndWkt, Func<int, byte[], string, TValue> onValidValue)
		{
			int? coordinateSystemId = spatialValue.CoordinateSystemId;
			if (coordinateSystemId == null)
			{
				throw onMissingcoordinateSystemId();
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

		// Token: 0x060004AC RID: 1196 RVA: 0x00017AE6 File Offset: 0x00015CE6
		public override string AsTextIncludingElevationAndMeasure(DbGeography geographyValue)
		{
			Check.NotNull<DbGeography>(geographyValue, "geographyValue");
			return this.SqlTypes.GeographyAsTextZM(geographyValue);
		}

		// Token: 0x060004AD RID: 1197 RVA: 0x00017B00 File Offset: 0x00015D00
		public override string AsTextIncludingElevationAndMeasure(DbGeometry geometryValue)
		{
			Check.NotNull<DbGeometry>(geometryValue, "geometryValue");
			return this.SqlTypes.GeometryAsTextZM(geometryValue);
		}

		// Token: 0x060004AE RID: 1198 RVA: 0x00017B1A File Offset: 0x00015D1A
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "argumentName")]
		private object ConvertToSqlValue(DbGeography geographyValue, string argumentName)
		{
			if (geographyValue == null)
			{
				return null;
			}
			return this.SqlTypes.ConvertToSqlTypesGeography(geographyValue);
		}

		// Token: 0x060004AF RID: 1199 RVA: 0x00017B2D File Offset: 0x00015D2D
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "argumentName")]
		private object ConvertToSqlValue(DbGeometry geometryValue, string argumentName)
		{
			if (geometryValue == null)
			{
				return null;
			}
			return this.SqlTypes.ConvertToSqlTypesGeometry(geometryValue);
		}

		// Token: 0x060004B0 RID: 1200 RVA: 0x00017B40 File Offset: 0x00015D40
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "argumentName")]
		private object ConvertToSqlBytes(byte[] binaryValue, string argumentName)
		{
			if (binaryValue == null)
			{
				return null;
			}
			return this.SqlTypes.SqlBytesFromByteArray(binaryValue);
		}

		// Token: 0x060004B1 RID: 1201 RVA: 0x00017B53 File Offset: 0x00015D53
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "argumentName")]
		private object ConvertToSqlChars(string stringValue, string argumentName)
		{
			if (stringValue == null)
			{
				return null;
			}
			return this.SqlTypes.SqlCharsFromString(stringValue);
		}

		// Token: 0x060004B2 RID: 1202 RVA: 0x00017B66 File Offset: 0x00015D66
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "argumentName")]
		private object ConvertToSqlString(string stringValue, string argumentName)
		{
			if (stringValue == null)
			{
				return null;
			}
			return this.SqlTypes.SqlStringFromString(stringValue);
		}

		// Token: 0x060004B3 RID: 1203 RVA: 0x00017B79 File Offset: 0x00015D79
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "argumentName")]
		private object ConvertToSqlXml(string stringValue, string argumentName)
		{
			if (stringValue == null)
			{
				return null;
			}
			return this.SqlTypes.SqlXmlFromString(stringValue);
		}

		// Token: 0x060004B4 RID: 1204 RVA: 0x00017B8C File Offset: 0x00015D8C
		private bool ConvertSqlBooleanToBoolean(object sqlBoolean)
		{
			return this.SqlTypes.SqlBooleanToBoolean(sqlBoolean);
		}

		// Token: 0x060004B5 RID: 1205 RVA: 0x00017B9A File Offset: 0x00015D9A
		private bool? ConvertSqlBooleanToNullableBoolean(object sqlBoolean)
		{
			return this.SqlTypes.SqlBooleanToNullableBoolean(sqlBoolean);
		}

		// Token: 0x060004B6 RID: 1206 RVA: 0x00017BA8 File Offset: 0x00015DA8
		private byte[] ConvertSqlBytesToBinary(object sqlBytes)
		{
			return this.SqlTypes.SqlBytesToByteArray(sqlBytes);
		}

		// Token: 0x060004B7 RID: 1207 RVA: 0x00017BB6 File Offset: 0x00015DB6
		private string ConvertSqlCharsToString(object sqlCharsValue)
		{
			return this.SqlTypes.SqlCharsToString(sqlCharsValue);
		}

		// Token: 0x060004B8 RID: 1208 RVA: 0x00017BC4 File Offset: 0x00015DC4
		private string ConvertSqlStringToString(object sqlCharsValue)
		{
			return this.SqlTypes.SqlStringToString(sqlCharsValue);
		}

		// Token: 0x060004B9 RID: 1209 RVA: 0x00017BD2 File Offset: 0x00015DD2
		private double ConvertSqlDoubleToDouble(object sqlDoubleValue)
		{
			return this.SqlTypes.SqlDoubleToDouble(sqlDoubleValue);
		}

		// Token: 0x060004BA RID: 1210 RVA: 0x00017BE0 File Offset: 0x00015DE0
		private double? ConvertSqlDoubleToNullableDouble(object sqlDoubleValue)
		{
			return this.SqlTypes.SqlDoubleToNullableDouble(sqlDoubleValue);
		}

		// Token: 0x060004BB RID: 1211 RVA: 0x00017BEE File Offset: 0x00015DEE
		private int ConvertSqlInt32ToInt(object sqlInt32Value)
		{
			return this.SqlTypes.SqlInt32ToInt(sqlInt32Value);
		}

		// Token: 0x060004BC RID: 1212 RVA: 0x00017BFC File Offset: 0x00015DFC
		private int? ConvertSqlInt32ToNullableInt(object sqlInt32Value)
		{
			return this.SqlTypes.SqlInt32ToNullableInt(sqlInt32Value);
		}

		// Token: 0x060004BD RID: 1213 RVA: 0x00017C0A File Offset: 0x00015E0A
		private string ConvertSqlXmlToString(object sqlXmlValue)
		{
			return this.SqlTypes.SqlXmlToString(sqlXmlValue);
		}

		// Token: 0x060004BE RID: 1214 RVA: 0x00017C18 File Offset: 0x00015E18
		public override DbGeography GeographyFromText(string wellKnownText)
		{
			object obj = this.ConvertToSqlString(wellKnownText, "wellKnownText");
			object providerValue = this.SqlTypes.SmiSqlGeographyParse.Value.Invoke(null, new object[]
			{
				obj
			});
			return this.GeographyFromProviderValue(providerValue);
		}

		// Token: 0x060004BF RID: 1215 RVA: 0x00017C5C File Offset: 0x00015E5C
		public override DbGeography GeographyFromText(string wellKnownText, int coordinateSystemId)
		{
			object obj = this.ConvertToSqlChars(wellKnownText, "wellKnownText");
			object providerValue = this.SqlTypes.SmiSqlGeographyStGeomFromText.Value.Invoke(null, new object[]
			{
				obj,
				coordinateSystemId
			});
			return this.GeographyFromProviderValue(providerValue);
		}

		// Token: 0x060004C0 RID: 1216 RVA: 0x00017CAC File Offset: 0x00015EAC
		public override DbGeography GeographyPointFromText(string pointWellKnownText, int coordinateSystemId)
		{
			object obj = this.ConvertToSqlChars(pointWellKnownText, "pointWellKnownText");
			object providerValue = this.SqlTypes.SmiSqlGeographyStPointFromText.Value.Invoke(null, new object[]
			{
				obj,
				coordinateSystemId
			});
			return this.GeographyFromProviderValue(providerValue);
		}

		// Token: 0x060004C1 RID: 1217 RVA: 0x00017CFC File Offset: 0x00015EFC
		public override DbGeography GeographyLineFromText(string lineWellKnownText, int coordinateSystemId)
		{
			object obj = this.ConvertToSqlChars(lineWellKnownText, "lineWellKnownText");
			object providerValue = this.SqlTypes.SmiSqlGeographyStLineFromText.Value.Invoke(null, new object[]
			{
				obj,
				coordinateSystemId
			});
			return this.GeographyFromProviderValue(providerValue);
		}

		// Token: 0x060004C2 RID: 1218 RVA: 0x00017D4C File Offset: 0x00015F4C
		public override DbGeography GeographyPolygonFromText(string polygonWellKnownText, int coordinateSystemId)
		{
			object obj = this.ConvertToSqlChars(polygonWellKnownText, "polygonWellKnownText");
			object providerValue = this.SqlTypes.SmiSqlGeographyStPolyFromText.Value.Invoke(null, new object[]
			{
				obj,
				coordinateSystemId
			});
			return this.GeographyFromProviderValue(providerValue);
		}

		// Token: 0x060004C3 RID: 1219 RVA: 0x00017D9C File Offset: 0x00015F9C
		public override DbGeography GeographyMultiPointFromText(string multiPointWellKnownText, int coordinateSystemId)
		{
			object obj = this.ConvertToSqlChars(multiPointWellKnownText, "multiPointWellKnownText");
			object providerValue = this.SqlTypes.SmiSqlGeographyStmPointFromText.Value.Invoke(null, new object[]
			{
				obj,
				coordinateSystemId
			});
			return this.GeographyFromProviderValue(providerValue);
		}

		// Token: 0x060004C4 RID: 1220 RVA: 0x00017DEC File Offset: 0x00015FEC
		public override DbGeography GeographyMultiLineFromText(string multiLineWellKnownText, int coordinateSystemId)
		{
			object obj = this.ConvertToSqlChars(multiLineWellKnownText, "multiLineWellKnownText");
			object providerValue = this.SqlTypes.SmiSqlGeographyStmLineFromText.Value.Invoke(null, new object[]
			{
				obj,
				coordinateSystemId
			});
			return this.GeographyFromProviderValue(providerValue);
		}

		// Token: 0x060004C5 RID: 1221 RVA: 0x00017E3C File Offset: 0x0001603C
		public override DbGeography GeographyMultiPolygonFromText(string multiPolygonKnownText, int coordinateSystemId)
		{
			object obj = this.ConvertToSqlChars(multiPolygonKnownText, "multiPolygonWellKnownText");
			object providerValue = this.SqlTypes.SmiSqlGeographyStmPolyFromText.Value.Invoke(null, new object[]
			{
				obj,
				coordinateSystemId
			});
			return this.GeographyFromProviderValue(providerValue);
		}

		// Token: 0x060004C6 RID: 1222 RVA: 0x00017E8C File Offset: 0x0001608C
		public override DbGeography GeographyCollectionFromText(string geographyCollectionWellKnownText, int coordinateSystemId)
		{
			object obj = this.ConvertToSqlChars(geographyCollectionWellKnownText, "geographyCollectionWellKnownText");
			object providerValue = this.SqlTypes.SmiSqlGeographyStGeomCollFromText.Value.Invoke(null, new object[]
			{
				obj,
				coordinateSystemId
			});
			return this.GeographyFromProviderValue(providerValue);
		}

		// Token: 0x060004C7 RID: 1223 RVA: 0x00017EDC File Offset: 0x000160DC
		public override DbGeography GeographyFromBinary(byte[] wellKnownBinary, int coordinateSystemId)
		{
			object obj = this.ConvertToSqlBytes(wellKnownBinary, "wellKnownBinary");
			object providerValue = this.SqlTypes.SmiSqlGeographyStGeomFromWkb.Value.Invoke(null, new object[]
			{
				obj,
				coordinateSystemId
			});
			return this.GeographyFromProviderValue(providerValue);
		}

		// Token: 0x060004C8 RID: 1224 RVA: 0x00017F2C File Offset: 0x0001612C
		public override DbGeography GeographyFromBinary(byte[] wellKnownBinary)
		{
			object obj = this.ConvertToSqlBytes(wellKnownBinary, "wellKnownBinary");
			object providerValue = this.SqlTypes.SmiSqlGeographyStGeomFromWkb.Value.Invoke(null, new object[]
			{
				obj,
				4326
			});
			return this.GeographyFromProviderValue(providerValue);
		}

		// Token: 0x060004C9 RID: 1225 RVA: 0x00017F80 File Offset: 0x00016180
		public override DbGeography GeographyPointFromBinary(byte[] pointWellKnownBinary, int coordinateSystemId)
		{
			object obj = this.ConvertToSqlBytes(pointWellKnownBinary, "pointWellKnownBinary");
			object providerValue = this.SqlTypes.SmiSqlGeographyStPointFromWkb.Value.Invoke(null, new object[]
			{
				obj,
				coordinateSystemId
			});
			return this.GeographyFromProviderValue(providerValue);
		}

		// Token: 0x060004CA RID: 1226 RVA: 0x00017FD0 File Offset: 0x000161D0
		public override DbGeography GeographyLineFromBinary(byte[] lineWellKnownBinary, int coordinateSystemId)
		{
			object obj = this.ConvertToSqlBytes(lineWellKnownBinary, "lineWellKnownBinary");
			object providerValue = this.SqlTypes.SmiSqlGeographyStLineFromWkb.Value.Invoke(null, new object[]
			{
				obj,
				coordinateSystemId
			});
			return this.GeographyFromProviderValue(providerValue);
		}

		// Token: 0x060004CB RID: 1227 RVA: 0x00018020 File Offset: 0x00016220
		public override DbGeography GeographyPolygonFromBinary(byte[] polygonWellKnownBinary, int coordinateSystemId)
		{
			object obj = this.ConvertToSqlBytes(polygonWellKnownBinary, "polygonWellKnownBinary");
			object providerValue = this.SqlTypes.SmiSqlGeographyStPolyFromWkb.Value.Invoke(null, new object[]
			{
				obj,
				coordinateSystemId
			});
			return this.GeographyFromProviderValue(providerValue);
		}

		// Token: 0x060004CC RID: 1228 RVA: 0x00018070 File Offset: 0x00016270
		public override DbGeography GeographyMultiPointFromBinary(byte[] multiPointWellKnownBinary, int coordinateSystemId)
		{
			object obj = this.ConvertToSqlBytes(multiPointWellKnownBinary, "multiPointWellKnownBinary");
			object providerValue = this.SqlTypes.SmiSqlGeographyStmPointFromWkb.Value.Invoke(null, new object[]
			{
				obj,
				coordinateSystemId
			});
			return this.GeographyFromProviderValue(providerValue);
		}

		// Token: 0x060004CD RID: 1229 RVA: 0x000180C0 File Offset: 0x000162C0
		public override DbGeography GeographyMultiLineFromBinary(byte[] multiLineWellKnownBinary, int coordinateSystemId)
		{
			object obj = this.ConvertToSqlBytes(multiLineWellKnownBinary, "multiLineWellKnownBinary");
			object providerValue = this.SqlTypes.SmiSqlGeographyStmLineFromWkb.Value.Invoke(null, new object[]
			{
				obj,
				coordinateSystemId
			});
			return this.GeographyFromProviderValue(providerValue);
		}

		// Token: 0x060004CE RID: 1230 RVA: 0x00018110 File Offset: 0x00016310
		public override DbGeography GeographyMultiPolygonFromBinary(byte[] multiPolygonWellKnownBinary, int coordinateSystemId)
		{
			object obj = this.ConvertToSqlBytes(multiPolygonWellKnownBinary, "multiPolygonWellKnownBinary");
			object providerValue = this.SqlTypes.SmiSqlGeographyStmPolyFromWkb.Value.Invoke(null, new object[]
			{
				obj,
				coordinateSystemId
			});
			return this.GeographyFromProviderValue(providerValue);
		}

		// Token: 0x060004CF RID: 1231 RVA: 0x00018160 File Offset: 0x00016360
		public override DbGeography GeographyCollectionFromBinary(byte[] geographyCollectionWellKnownBinary, int coordinateSystemId)
		{
			object obj = this.ConvertToSqlBytes(geographyCollectionWellKnownBinary, "geographyCollectionWellKnownBinary");
			object providerValue = this.SqlTypes.SmiSqlGeographyStGeomCollFromWkb.Value.Invoke(null, new object[]
			{
				obj,
				coordinateSystemId
			});
			return this.GeographyFromProviderValue(providerValue);
		}

		// Token: 0x060004D0 RID: 1232 RVA: 0x000181B0 File Offset: 0x000163B0
		public override DbGeography GeographyFromGml(string geographyMarkup)
		{
			object obj = this.ConvertToSqlXml(geographyMarkup, "geographyMarkup");
			object providerValue = this.SqlTypes.SmiSqlGeographyGeomFromGml.Value.Invoke(null, new object[]
			{
				obj,
				4326
			});
			return this.GeographyFromProviderValue(providerValue);
		}

		// Token: 0x060004D1 RID: 1233 RVA: 0x00018204 File Offset: 0x00016404
		public override DbGeography GeographyFromGml(string geographyMarkup, int coordinateSystemId)
		{
			object obj = this.ConvertToSqlXml(geographyMarkup, "geographyMarkup");
			object providerValue = this.SqlTypes.SmiSqlGeographyGeomFromGml.Value.Invoke(null, new object[]
			{
				obj,
				coordinateSystemId
			});
			return this.GeographyFromProviderValue(providerValue);
		}

		// Token: 0x060004D2 RID: 1234 RVA: 0x00018254 File Offset: 0x00016454
		public override int GetCoordinateSystemId(DbGeography geographyValue)
		{
			Check.NotNull<DbGeography>(geographyValue, "geographyValue");
			object obj = this.ConvertToSqlValue(geographyValue, "geographyValue");
			object value = this.SqlTypes.IpiSqlGeographyStSrid.Value.GetValue(obj, null);
			return this.ConvertSqlInt32ToInt(value);
		}

		// Token: 0x060004D3 RID: 1235 RVA: 0x0001829C File Offset: 0x0001649C
		public override string GetSpatialTypeName(DbGeography geographyValue)
		{
			Check.NotNull<DbGeography>(geographyValue, "geographyValue");
			object obj = this.ConvertToSqlValue(geographyValue, "geographyValue");
			object sqlCharsValue = this.SqlTypes.ImiSqlGeographyStGeometryType.Value.Invoke(obj, new object[0]);
			return this.ConvertSqlStringToString(sqlCharsValue);
		}

		// Token: 0x060004D4 RID: 1236 RVA: 0x000182E8 File Offset: 0x000164E8
		public override int GetDimension(DbGeography geographyValue)
		{
			Check.NotNull<DbGeography>(geographyValue, "geographyValue");
			object obj = this.ConvertToSqlValue(geographyValue, "geographyValue");
			object sqlInt32Value = this.SqlTypes.ImiSqlGeographyStDimension.Value.Invoke(obj, new object[0]);
			return this.ConvertSqlInt32ToInt(sqlInt32Value);
		}

		// Token: 0x060004D5 RID: 1237 RVA: 0x00018334 File Offset: 0x00016534
		public override byte[] AsBinary(DbGeography geographyValue)
		{
			Check.NotNull<DbGeography>(geographyValue, "geographyValue");
			object obj = this.ConvertToSqlValue(geographyValue, "geographyValue");
			object sqlBytes = this.SqlTypes.ImiSqlGeographyStAsBinary.Value.Invoke(obj, new object[0]);
			return this.ConvertSqlBytesToBinary(sqlBytes);
		}

		// Token: 0x060004D6 RID: 1238 RVA: 0x00018380 File Offset: 0x00016580
		public override string AsGml(DbGeography geographyValue)
		{
			Check.NotNull<DbGeography>(geographyValue, "geographyValue");
			object obj = this.ConvertToSqlValue(geographyValue, "geographyValue");
			object sqlXmlValue = this.SqlTypes.ImiSqlGeographyAsGml.Value.Invoke(obj, new object[0]);
			return this.ConvertSqlXmlToString(sqlXmlValue);
		}

		// Token: 0x060004D7 RID: 1239 RVA: 0x000183CC File Offset: 0x000165CC
		public override string AsText(DbGeography geographyValue)
		{
			Check.NotNull<DbGeography>(geographyValue, "geographyValue");
			object obj = this.ConvertToSqlValue(geographyValue, "geographyValue");
			object sqlCharsValue = this.SqlTypes.ImiSqlGeographyStAsText.Value.Invoke(obj, new object[0]);
			return this.ConvertSqlCharsToString(sqlCharsValue);
		}

		// Token: 0x060004D8 RID: 1240 RVA: 0x00018418 File Offset: 0x00016618
		public override bool GetIsEmpty(DbGeography geographyValue)
		{
			Check.NotNull<DbGeography>(geographyValue, "geographyValue");
			object obj = this.ConvertToSqlValue(geographyValue, "geographyValue");
			object sqlBoolean = this.SqlTypes.ImiSqlGeographyStIsEmpty.Value.Invoke(obj, new object[0]);
			return this.ConvertSqlBooleanToBoolean(sqlBoolean);
		}

		// Token: 0x060004D9 RID: 1241 RVA: 0x00018464 File Offset: 0x00016664
		public override bool SpatialEquals(DbGeography geographyValue, DbGeography otherGeography)
		{
			Check.NotNull<DbGeography>(geographyValue, "geographyValue");
			object obj = this.ConvertToSqlValue(geographyValue, "geographyValue");
			object obj2 = this.ConvertToSqlValue(otherGeography, "otherGeography");
			object sqlBoolean = this.SqlTypes.ImiSqlGeographyStEquals.Value.Invoke(obj, new object[]
			{
				obj2
			});
			return this.ConvertSqlBooleanToBoolean(sqlBoolean);
		}

		// Token: 0x060004DA RID: 1242 RVA: 0x000184C4 File Offset: 0x000166C4
		public override bool Disjoint(DbGeography geographyValue, DbGeography otherGeography)
		{
			Check.NotNull<DbGeography>(geographyValue, "geographyValue");
			object obj = this.ConvertToSqlValue(geographyValue, "geographyValue");
			object obj2 = this.ConvertToSqlValue(otherGeography, "otherGeography");
			object sqlBoolean = this.SqlTypes.ImiSqlGeographyStDisjoint.Value.Invoke(obj, new object[]
			{
				obj2
			});
			return this.ConvertSqlBooleanToBoolean(sqlBoolean);
		}

		// Token: 0x060004DB RID: 1243 RVA: 0x00018524 File Offset: 0x00016724
		public override bool Intersects(DbGeography geographyValue, DbGeography otherGeography)
		{
			Check.NotNull<DbGeography>(geographyValue, "geographyValue");
			object obj = this.ConvertToSqlValue(geographyValue, "geographyValue");
			object obj2 = this.ConvertToSqlValue(otherGeography, "otherGeography");
			object sqlBoolean = this.SqlTypes.ImiSqlGeographyStIntersects.Value.Invoke(obj, new object[]
			{
				obj2
			});
			return this.ConvertSqlBooleanToBoolean(sqlBoolean);
		}

		// Token: 0x060004DC RID: 1244 RVA: 0x00018584 File Offset: 0x00016784
		public override DbGeography Buffer(DbGeography geographyValue, double distance)
		{
			Check.NotNull<DbGeography>(geographyValue, "geographyValue");
			object obj = this.ConvertToSqlValue(geographyValue, "geographyValue");
			object providerValue = this.SqlTypes.ImiSqlGeographyStBuffer.Value.Invoke(obj, new object[]
			{
				distance
			});
			return this.GeographyFromProviderValue(providerValue);
		}

		// Token: 0x060004DD RID: 1245 RVA: 0x000185DC File Offset: 0x000167DC
		public override double Distance(DbGeography geographyValue, DbGeography otherGeography)
		{
			Check.NotNull<DbGeography>(geographyValue, "geographyValue");
			object obj = this.ConvertToSqlValue(geographyValue, "geographyValue");
			object obj2 = this.ConvertToSqlValue(otherGeography, "otherGeography");
			object sqlDoubleValue = this.SqlTypes.ImiSqlGeographyStDistance.Value.Invoke(obj, new object[]
			{
				obj2
			});
			return this.ConvertSqlDoubleToDouble(sqlDoubleValue);
		}

		// Token: 0x060004DE RID: 1246 RVA: 0x0001863C File Offset: 0x0001683C
		public override DbGeography Intersection(DbGeography geographyValue, DbGeography otherGeography)
		{
			Check.NotNull<DbGeography>(geographyValue, "geographyValue");
			object obj = this.ConvertToSqlValue(geographyValue, "geographyValue");
			object obj2 = this.ConvertToSqlValue(otherGeography, "otherGeography");
			object providerValue = this.SqlTypes.ImiSqlGeographyStIntersection.Value.Invoke(obj, new object[]
			{
				obj2
			});
			return this.GeographyFromProviderValue(providerValue);
		}

		// Token: 0x060004DF RID: 1247 RVA: 0x0001869C File Offset: 0x0001689C
		public override DbGeography Union(DbGeography geographyValue, DbGeography otherGeography)
		{
			Check.NotNull<DbGeography>(geographyValue, "geographyValue");
			object obj = this.ConvertToSqlValue(geographyValue, "geographyValue");
			object obj2 = this.ConvertToSqlValue(otherGeography, "otherGeography");
			object providerValue = this.SqlTypes.ImiSqlGeographyStUnion.Value.Invoke(obj, new object[]
			{
				obj2
			});
			return this.GeographyFromProviderValue(providerValue);
		}

		// Token: 0x060004E0 RID: 1248 RVA: 0x000186FC File Offset: 0x000168FC
		public override DbGeography Difference(DbGeography geographyValue, DbGeography otherGeography)
		{
			Check.NotNull<DbGeography>(geographyValue, "geographyValue");
			object obj = this.ConvertToSqlValue(geographyValue, "geographyValue");
			object obj2 = this.ConvertToSqlValue(otherGeography, "otherGeography");
			object providerValue = this.SqlTypes.ImiSqlGeographyStDifference.Value.Invoke(obj, new object[]
			{
				obj2
			});
			return this.GeographyFromProviderValue(providerValue);
		}

		// Token: 0x060004E1 RID: 1249 RVA: 0x0001875C File Offset: 0x0001695C
		public override DbGeography SymmetricDifference(DbGeography geographyValue, DbGeography otherGeography)
		{
			Check.NotNull<DbGeography>(geographyValue, "geographyValue");
			object obj = this.ConvertToSqlValue(geographyValue, "geographyValue");
			object obj2 = this.ConvertToSqlValue(otherGeography, "otherGeography");
			object providerValue = this.SqlTypes.ImiSqlGeographyStSymDifference.Value.Invoke(obj, new object[]
			{
				obj2
			});
			return this.GeographyFromProviderValue(providerValue);
		}

		// Token: 0x060004E2 RID: 1250 RVA: 0x000187BC File Offset: 0x000169BC
		public override int? GetElementCount(DbGeography geographyValue)
		{
			Check.NotNull<DbGeography>(geographyValue, "geographyValue");
			object obj = this.ConvertToSqlValue(geographyValue, "geographyValue");
			object sqlInt32Value = this.SqlTypes.ImiSqlGeographyStNumGeometries.Value.Invoke(obj, new object[0]);
			return this.ConvertSqlInt32ToNullableInt(sqlInt32Value);
		}

		// Token: 0x060004E3 RID: 1251 RVA: 0x00018808 File Offset: 0x00016A08
		public override DbGeography ElementAt(DbGeography geographyValue, int index)
		{
			Check.NotNull<DbGeography>(geographyValue, "geographyValue");
			object obj = this.ConvertToSqlValue(geographyValue, "geographyValue");
			object providerValue = this.SqlTypes.ImiSqlGeographyStGeometryN.Value.Invoke(obj, new object[]
			{
				index
			});
			return this.GeographyFromProviderValue(providerValue);
		}

		// Token: 0x060004E4 RID: 1252 RVA: 0x00018860 File Offset: 0x00016A60
		public override double? GetLatitude(DbGeography geographyValue)
		{
			Check.NotNull<DbGeography>(geographyValue, "geographyValue");
			object obj = this.ConvertToSqlValue(geographyValue, "geographyValue");
			object value = this.SqlTypes.IpiSqlGeographyLat.Value.GetValue(obj, null);
			return this.ConvertSqlDoubleToNullableDouble(value);
		}

		// Token: 0x060004E5 RID: 1253 RVA: 0x000188A8 File Offset: 0x00016AA8
		public override double? GetLongitude(DbGeography geographyValue)
		{
			Check.NotNull<DbGeography>(geographyValue, "geographyValue");
			object obj = this.ConvertToSqlValue(geographyValue, "geographyValue");
			object value = this.SqlTypes.IpiSqlGeographyLong.Value.GetValue(obj, null);
			return this.ConvertSqlDoubleToNullableDouble(value);
		}

		// Token: 0x060004E6 RID: 1254 RVA: 0x000188F0 File Offset: 0x00016AF0
		public override double? GetElevation(DbGeography geographyValue)
		{
			Check.NotNull<DbGeography>(geographyValue, "geographyValue");
			object obj = this.ConvertToSqlValue(geographyValue, "geographyValue");
			object value = this.SqlTypes.IpiSqlGeographyZ.Value.GetValue(obj, null);
			return this.ConvertSqlDoubleToNullableDouble(value);
		}

		// Token: 0x060004E7 RID: 1255 RVA: 0x00018938 File Offset: 0x00016B38
		public override double? GetMeasure(DbGeography geographyValue)
		{
			Check.NotNull<DbGeography>(geographyValue, "geographyValue");
			object obj = this.ConvertToSqlValue(geographyValue, "geographyValue");
			object value = this.SqlTypes.IpiSqlGeographyM.Value.GetValue(obj, null);
			return this.ConvertSqlDoubleToNullableDouble(value);
		}

		// Token: 0x060004E8 RID: 1256 RVA: 0x00018980 File Offset: 0x00016B80
		public override double? GetLength(DbGeography geographyValue)
		{
			Check.NotNull<DbGeography>(geographyValue, "geographyValue");
			object obj = this.ConvertToSqlValue(geographyValue, "geographyValue");
			object sqlDoubleValue = this.SqlTypes.ImiSqlGeographyStLength.Value.Invoke(obj, new object[0]);
			return this.ConvertSqlDoubleToNullableDouble(sqlDoubleValue);
		}

		// Token: 0x060004E9 RID: 1257 RVA: 0x000189CC File Offset: 0x00016BCC
		public override DbGeography GetStartPoint(DbGeography geographyValue)
		{
			Check.NotNull<DbGeography>(geographyValue, "geographyValue");
			object obj = this.ConvertToSqlValue(geographyValue, "geographyValue");
			object providerValue = this.SqlTypes.ImiSqlGeographyStStartPoint.Value.Invoke(obj, new object[0]);
			return this.GeographyFromProviderValue(providerValue);
		}

		// Token: 0x060004EA RID: 1258 RVA: 0x00018A18 File Offset: 0x00016C18
		public override DbGeography GetEndPoint(DbGeography geographyValue)
		{
			Check.NotNull<DbGeography>(geographyValue, "geographyValue");
			object obj = this.ConvertToSqlValue(geographyValue, "geographyValue");
			object providerValue = this.SqlTypes.ImiSqlGeographyStEndPoint.Value.Invoke(obj, new object[0]);
			return this.GeographyFromProviderValue(providerValue);
		}

		// Token: 0x060004EB RID: 1259 RVA: 0x00018A64 File Offset: 0x00016C64
		public override bool? GetIsClosed(DbGeography geographyValue)
		{
			Check.NotNull<DbGeography>(geographyValue, "geographyValue");
			object obj = this.ConvertToSqlValue(geographyValue, "geographyValue");
			object sqlBoolean = this.SqlTypes.ImiSqlGeographyStIsClosed.Value.Invoke(obj, new object[0]);
			return this.ConvertSqlBooleanToNullableBoolean(sqlBoolean);
		}

		// Token: 0x060004EC RID: 1260 RVA: 0x00018AB0 File Offset: 0x00016CB0
		public override int? GetPointCount(DbGeography geographyValue)
		{
			Check.NotNull<DbGeography>(geographyValue, "geographyValue");
			object obj = this.ConvertToSqlValue(geographyValue, "geographyValue");
			object sqlInt32Value = this.SqlTypes.ImiSqlGeographyStNumPoints.Value.Invoke(obj, new object[0]);
			return this.ConvertSqlInt32ToNullableInt(sqlInt32Value);
		}

		// Token: 0x060004ED RID: 1261 RVA: 0x00018AFC File Offset: 0x00016CFC
		public override DbGeography PointAt(DbGeography geographyValue, int index)
		{
			Check.NotNull<DbGeography>(geographyValue, "geographyValue");
			object obj = this.ConvertToSqlValue(geographyValue, "geographyValue");
			object providerValue = this.SqlTypes.ImiSqlGeographyStPointN.Value.Invoke(obj, new object[]
			{
				index
			});
			return this.GeographyFromProviderValue(providerValue);
		}

		// Token: 0x060004EE RID: 1262 RVA: 0x00018B54 File Offset: 0x00016D54
		public override double? GetArea(DbGeography geographyValue)
		{
			Check.NotNull<DbGeography>(geographyValue, "geographyValue");
			object obj = this.ConvertToSqlValue(geographyValue, "geographyValue");
			object sqlDoubleValue = this.SqlTypes.ImiSqlGeographyStArea.Value.Invoke(obj, new object[0]);
			return this.ConvertSqlDoubleToNullableDouble(sqlDoubleValue);
		}

		// Token: 0x060004EF RID: 1263 RVA: 0x00018BA0 File Offset: 0x00016DA0
		public override DbGeometry GeometryFromText(string wellKnownText)
		{
			object obj = this.ConvertToSqlString(wellKnownText, "wellKnownText");
			object providerValue = this.SqlTypes.SmiSqlGeometryParse.Value.Invoke(null, new object[]
			{
				obj
			});
			return this.GeometryFromProviderValue(providerValue);
		}

		// Token: 0x060004F0 RID: 1264 RVA: 0x00018BE4 File Offset: 0x00016DE4
		public override DbGeometry GeometryFromText(string wellKnownText, int coordinateSystemId)
		{
			object obj = this.ConvertToSqlChars(wellKnownText, "wellKnownText");
			object providerValue = this.SqlTypes.SmiSqlGeometryStGeomFromText.Value.Invoke(null, new object[]
			{
				obj,
				coordinateSystemId
			});
			return this.GeometryFromProviderValue(providerValue);
		}

		// Token: 0x060004F1 RID: 1265 RVA: 0x00018C34 File Offset: 0x00016E34
		public override DbGeometry GeometryPointFromText(string pointWellKnownText, int coordinateSystemId)
		{
			object obj = this.ConvertToSqlChars(pointWellKnownText, "pointWellKnownText");
			object providerValue = this.SqlTypes.SmiSqlGeometryStPointFromText.Value.Invoke(null, new object[]
			{
				obj,
				coordinateSystemId
			});
			return this.GeometryFromProviderValue(providerValue);
		}

		// Token: 0x060004F2 RID: 1266 RVA: 0x00018C84 File Offset: 0x00016E84
		public override DbGeometry GeometryLineFromText(string lineWellKnownText, int coordinateSystemId)
		{
			object obj = this.ConvertToSqlChars(lineWellKnownText, "lineWellKnownText");
			object providerValue = this.SqlTypes.SmiSqlGeometryStLineFromText.Value.Invoke(null, new object[]
			{
				obj,
				coordinateSystemId
			});
			return this.GeometryFromProviderValue(providerValue);
		}

		// Token: 0x060004F3 RID: 1267 RVA: 0x00018CD4 File Offset: 0x00016ED4
		public override DbGeometry GeometryPolygonFromText(string polygonWellKnownText, int coordinateSystemId)
		{
			object obj = this.ConvertToSqlChars(polygonWellKnownText, "polygonWellKnownText");
			object providerValue = this.SqlTypes.SmiSqlGeometryStPolyFromText.Value.Invoke(null, new object[]
			{
				obj,
				coordinateSystemId
			});
			return this.GeometryFromProviderValue(providerValue);
		}

		// Token: 0x060004F4 RID: 1268 RVA: 0x00018D24 File Offset: 0x00016F24
		public override DbGeometry GeometryMultiPointFromText(string multiPointWellKnownText, int coordinateSystemId)
		{
			object obj = this.ConvertToSqlChars(multiPointWellKnownText, "multiPointWellKnownText");
			object providerValue = this.SqlTypes.SmiSqlGeometryStmPointFromText.Value.Invoke(null, new object[]
			{
				obj,
				coordinateSystemId
			});
			return this.GeometryFromProviderValue(providerValue);
		}

		// Token: 0x060004F5 RID: 1269 RVA: 0x00018D74 File Offset: 0x00016F74
		public override DbGeometry GeometryMultiLineFromText(string multiLineWellKnownText, int coordinateSystemId)
		{
			object obj = this.ConvertToSqlChars(multiLineWellKnownText, "multiLineWellKnownText");
			object providerValue = this.SqlTypes.SmiSqlGeometryStmLineFromText.Value.Invoke(null, new object[]
			{
				obj,
				coordinateSystemId
			});
			return this.GeometryFromProviderValue(providerValue);
		}

		// Token: 0x060004F6 RID: 1270 RVA: 0x00018DC4 File Offset: 0x00016FC4
		public override DbGeometry GeometryMultiPolygonFromText(string multiPolygonKnownText, int coordinateSystemId)
		{
			object obj = this.ConvertToSqlChars(multiPolygonKnownText, "multiPolygonKnownText");
			object providerValue = this.SqlTypes.SmiSqlGeometryStmPolyFromText.Value.Invoke(null, new object[]
			{
				obj,
				coordinateSystemId
			});
			return this.GeometryFromProviderValue(providerValue);
		}

		// Token: 0x060004F7 RID: 1271 RVA: 0x00018E14 File Offset: 0x00017014
		public override DbGeometry GeometryCollectionFromText(string geometryCollectionWellKnownText, int coordinateSystemId)
		{
			object obj = this.ConvertToSqlChars(geometryCollectionWellKnownText, "geometryCollectionWellKnownText");
			object providerValue = this.SqlTypes.SmiSqlGeometryStGeomCollFromText.Value.Invoke(null, new object[]
			{
				obj,
				coordinateSystemId
			});
			return this.GeometryFromProviderValue(providerValue);
		}

		// Token: 0x060004F8 RID: 1272 RVA: 0x00018E64 File Offset: 0x00017064
		public override DbGeometry GeometryFromBinary(byte[] wellKnownBinary)
		{
			object obj = this.ConvertToSqlBytes(wellKnownBinary, "wellKnownBinary");
			object providerValue = this.SqlTypes.SmiSqlGeometryStGeomFromWkb.Value.Invoke(null, new object[]
			{
				obj,
				0
			});
			return this.GeometryFromProviderValue(providerValue);
		}

		// Token: 0x060004F9 RID: 1273 RVA: 0x00018EB4 File Offset: 0x000170B4
		public override DbGeometry GeometryFromBinary(byte[] wellKnownBinary, int coordinateSystemId)
		{
			object obj = this.ConvertToSqlBytes(wellKnownBinary, "wellKnownBinary");
			object providerValue = this.SqlTypes.SmiSqlGeometryStGeomFromWkb.Value.Invoke(null, new object[]
			{
				obj,
				coordinateSystemId
			});
			return this.GeometryFromProviderValue(providerValue);
		}

		// Token: 0x060004FA RID: 1274 RVA: 0x00018F04 File Offset: 0x00017104
		public override DbGeometry GeometryPointFromBinary(byte[] pointWellKnownBinary, int coordinateSystemId)
		{
			object obj = this.ConvertToSqlBytes(pointWellKnownBinary, "pointWellKnownBinary");
			object providerValue = this.SqlTypes.SmiSqlGeometryStPointFromWkb.Value.Invoke(null, new object[]
			{
				obj,
				coordinateSystemId
			});
			return this.GeometryFromProviderValue(providerValue);
		}

		// Token: 0x060004FB RID: 1275 RVA: 0x00018F54 File Offset: 0x00017154
		public override DbGeometry GeometryLineFromBinary(byte[] lineWellKnownBinary, int coordinateSystemId)
		{
			object obj = this.ConvertToSqlBytes(lineWellKnownBinary, "lineWellKnownBinary");
			object providerValue = this.SqlTypes.SmiSqlGeometryStLineFromWkb.Value.Invoke(null, new object[]
			{
				obj,
				coordinateSystemId
			});
			return this.GeometryFromProviderValue(providerValue);
		}

		// Token: 0x060004FC RID: 1276 RVA: 0x00018FA4 File Offset: 0x000171A4
		public override DbGeometry GeometryPolygonFromBinary(byte[] polygonWellKnownBinary, int coordinateSystemId)
		{
			object obj = this.ConvertToSqlBytes(polygonWellKnownBinary, "polygonWellKnownBinary");
			object providerValue = this.SqlTypes.SmiSqlGeometryStPolyFromWkb.Value.Invoke(null, new object[]
			{
				obj,
				coordinateSystemId
			});
			return this.GeometryFromProviderValue(providerValue);
		}

		// Token: 0x060004FD RID: 1277 RVA: 0x00018FF4 File Offset: 0x000171F4
		public override DbGeometry GeometryMultiPointFromBinary(byte[] multiPointWellKnownBinary, int coordinateSystemId)
		{
			object obj = this.ConvertToSqlBytes(multiPointWellKnownBinary, "multiPointWellKnownBinary");
			object providerValue = this.SqlTypes.SmiSqlGeometryStmPointFromWkb.Value.Invoke(null, new object[]
			{
				obj,
				coordinateSystemId
			});
			return this.GeometryFromProviderValue(providerValue);
		}

		// Token: 0x060004FE RID: 1278 RVA: 0x00019044 File Offset: 0x00017244
		public override DbGeometry GeometryMultiLineFromBinary(byte[] multiLineWellKnownBinary, int coordinateSystemId)
		{
			object obj = this.ConvertToSqlBytes(multiLineWellKnownBinary, "multiLineWellKnownBinary");
			object providerValue = this.SqlTypes.SmiSqlGeometryStmLineFromWkb.Value.Invoke(null, new object[]
			{
				obj,
				coordinateSystemId
			});
			return this.GeometryFromProviderValue(providerValue);
		}

		// Token: 0x060004FF RID: 1279 RVA: 0x00019094 File Offset: 0x00017294
		public override DbGeometry GeometryMultiPolygonFromBinary(byte[] multiPolygonWellKnownBinary, int coordinateSystemId)
		{
			object obj = this.ConvertToSqlBytes(multiPolygonWellKnownBinary, "multiPolygonWellKnownBinary");
			object providerValue = this.SqlTypes.SmiSqlGeometryStmPolyFromWkb.Value.Invoke(null, new object[]
			{
				obj,
				coordinateSystemId
			});
			return this.GeometryFromProviderValue(providerValue);
		}

		// Token: 0x06000500 RID: 1280 RVA: 0x000190E4 File Offset: 0x000172E4
		public override DbGeometry GeometryCollectionFromBinary(byte[] geometryCollectionWellKnownBinary, int coordinateSystemId)
		{
			object obj = this.ConvertToSqlBytes(geometryCollectionWellKnownBinary, "geometryCollectionWellKnownBinary");
			object providerValue = this.SqlTypes.SmiSqlGeometryStGeomCollFromWkb.Value.Invoke(null, new object[]
			{
				obj,
				coordinateSystemId
			});
			return this.GeometryFromProviderValue(providerValue);
		}

		// Token: 0x06000501 RID: 1281 RVA: 0x00019134 File Offset: 0x00017334
		public override DbGeometry GeometryFromGml(string geometryMarkup)
		{
			object obj = this.ConvertToSqlXml(geometryMarkup, "geometryMarkup");
			object providerValue = this.SqlTypes.SmiSqlGeometryGeomFromGml.Value.Invoke(null, new object[]
			{
				obj,
				0
			});
			return this.GeometryFromProviderValue(providerValue);
		}

		// Token: 0x06000502 RID: 1282 RVA: 0x00019184 File Offset: 0x00017384
		public override DbGeometry GeometryFromGml(string geometryMarkup, int coordinateSystemId)
		{
			object obj = this.ConvertToSqlXml(geometryMarkup, "geometryMarkup");
			object providerValue = this.SqlTypes.SmiSqlGeometryGeomFromGml.Value.Invoke(null, new object[]
			{
				obj,
				coordinateSystemId
			});
			return this.GeometryFromProviderValue(providerValue);
		}

		// Token: 0x06000503 RID: 1283 RVA: 0x000191D4 File Offset: 0x000173D4
		public override int GetCoordinateSystemId(DbGeometry geometryValue)
		{
			Check.NotNull<DbGeometry>(geometryValue, "geometryValue");
			object obj = this.ConvertToSqlValue(geometryValue, "geometryValue");
			object value = this.SqlTypes.IpiSqlGeometryStSrid.Value.GetValue(obj, null);
			return this.ConvertSqlInt32ToInt(value);
		}

		// Token: 0x06000504 RID: 1284 RVA: 0x0001921C File Offset: 0x0001741C
		public override string GetSpatialTypeName(DbGeometry geometryValue)
		{
			Check.NotNull<DbGeometry>(geometryValue, "geometryValue");
			object obj = this.ConvertToSqlValue(geometryValue, "geometryValue");
			object sqlCharsValue = this.SqlTypes.ImiSqlGeometryStGeometryType.Value.Invoke(obj, new object[0]);
			return this.ConvertSqlStringToString(sqlCharsValue);
		}

		// Token: 0x06000505 RID: 1285 RVA: 0x00019268 File Offset: 0x00017468
		public override int GetDimension(DbGeometry geometryValue)
		{
			Check.NotNull<DbGeometry>(geometryValue, "geometryValue");
			object obj = this.ConvertToSqlValue(geometryValue, "geometryValue");
			object sqlInt32Value = this.SqlTypes.ImiSqlGeometryStDimension.Value.Invoke(obj, new object[0]);
			return this.ConvertSqlInt32ToInt(sqlInt32Value);
		}

		// Token: 0x06000506 RID: 1286 RVA: 0x000192B4 File Offset: 0x000174B4
		public override DbGeometry GetEnvelope(DbGeometry geometryValue)
		{
			Check.NotNull<DbGeometry>(geometryValue, "geometryValue");
			object obj = this.ConvertToSqlValue(geometryValue, "geometryValue");
			object providerValue = this.SqlTypes.ImiSqlGeometryStEnvelope.Value.Invoke(obj, new object[0]);
			return this.GeometryFromProviderValue(providerValue);
		}

		// Token: 0x06000507 RID: 1287 RVA: 0x00019300 File Offset: 0x00017500
		public override byte[] AsBinary(DbGeometry geometryValue)
		{
			Check.NotNull<DbGeometry>(geometryValue, "geometryValue");
			object obj = this.ConvertToSqlValue(geometryValue, "geometryValue");
			object sqlBytes = this.SqlTypes.ImiSqlGeometryStAsBinary.Value.Invoke(obj, new object[0]);
			return this.ConvertSqlBytesToBinary(sqlBytes);
		}

		// Token: 0x06000508 RID: 1288 RVA: 0x0001934C File Offset: 0x0001754C
		public override string AsGml(DbGeometry geometryValue)
		{
			Check.NotNull<DbGeometry>(geometryValue, "geometryValue");
			object obj = this.ConvertToSqlValue(geometryValue, "geometryValue");
			object sqlXmlValue = this.SqlTypes.ImiSqlGeometryAsGml.Value.Invoke(obj, new object[0]);
			return this.ConvertSqlXmlToString(sqlXmlValue);
		}

		// Token: 0x06000509 RID: 1289 RVA: 0x00019398 File Offset: 0x00017598
		public override string AsText(DbGeometry geometryValue)
		{
			Check.NotNull<DbGeometry>(geometryValue, "geometryValue");
			object obj = this.ConvertToSqlValue(geometryValue, "geometryValue");
			object sqlCharsValue = this.SqlTypes.ImiSqlGeometryStAsText.Value.Invoke(obj, new object[0]);
			return this.ConvertSqlCharsToString(sqlCharsValue);
		}

		// Token: 0x0600050A RID: 1290 RVA: 0x000193E4 File Offset: 0x000175E4
		public override bool GetIsEmpty(DbGeometry geometryValue)
		{
			Check.NotNull<DbGeometry>(geometryValue, "geometryValue");
			object obj = this.ConvertToSqlValue(geometryValue, "geometryValue");
			object sqlBoolean = this.SqlTypes.ImiSqlGeometryStIsEmpty.Value.Invoke(obj, new object[0]);
			return this.ConvertSqlBooleanToBoolean(sqlBoolean);
		}

		// Token: 0x0600050B RID: 1291 RVA: 0x00019430 File Offset: 0x00017630
		public override bool GetIsSimple(DbGeometry geometryValue)
		{
			Check.NotNull<DbGeometry>(geometryValue, "geometryValue");
			object obj = this.ConvertToSqlValue(geometryValue, "geometryValue");
			object sqlBoolean = this.SqlTypes.ImiSqlGeometryStIsSimple.Value.Invoke(obj, new object[0]);
			return this.ConvertSqlBooleanToBoolean(sqlBoolean);
		}

		// Token: 0x0600050C RID: 1292 RVA: 0x0001947C File Offset: 0x0001767C
		public override DbGeometry GetBoundary(DbGeometry geometryValue)
		{
			Check.NotNull<DbGeometry>(geometryValue, "geometryValue");
			object obj = this.ConvertToSqlValue(geometryValue, "geometryValue");
			object providerValue = this.SqlTypes.ImiSqlGeometryStBoundary.Value.Invoke(obj, new object[0]);
			return this.GeometryFromProviderValue(providerValue);
		}

		// Token: 0x0600050D RID: 1293 RVA: 0x000194C8 File Offset: 0x000176C8
		public override bool GetIsValid(DbGeometry geometryValue)
		{
			Check.NotNull<DbGeometry>(geometryValue, "geometryValue");
			object obj = this.ConvertToSqlValue(geometryValue, "geometryValue");
			object sqlBoolean = this.SqlTypes.ImiSqlGeometryStIsValid.Value.Invoke(obj, new object[0]);
			return this.ConvertSqlBooleanToBoolean(sqlBoolean);
		}

		// Token: 0x0600050E RID: 1294 RVA: 0x00019514 File Offset: 0x00017714
		public override bool SpatialEquals(DbGeometry geometryValue, DbGeometry otherGeometry)
		{
			Check.NotNull<DbGeometry>(geometryValue, "geometryValue");
			object obj = this.ConvertToSqlValue(geometryValue, "geometryValue");
			object obj2 = this.ConvertToSqlValue(otherGeometry, "otherGeometry");
			object sqlBoolean = this.SqlTypes.ImiSqlGeometryStEquals.Value.Invoke(obj, new object[]
			{
				obj2
			});
			return this.ConvertSqlBooleanToBoolean(sqlBoolean);
		}

		// Token: 0x0600050F RID: 1295 RVA: 0x00019574 File Offset: 0x00017774
		public override bool Disjoint(DbGeometry geometryValue, DbGeometry otherGeometry)
		{
			Check.NotNull<DbGeometry>(geometryValue, "geometryValue");
			object obj = this.ConvertToSqlValue(geometryValue, "geometryValue");
			object obj2 = this.ConvertToSqlValue(otherGeometry, "otherGeometry");
			object sqlBoolean = this.SqlTypes.ImiSqlGeometryStDisjoint.Value.Invoke(obj, new object[]
			{
				obj2
			});
			return this.ConvertSqlBooleanToBoolean(sqlBoolean);
		}

		// Token: 0x06000510 RID: 1296 RVA: 0x000195D4 File Offset: 0x000177D4
		public override bool Intersects(DbGeometry geometryValue, DbGeometry otherGeometry)
		{
			Check.NotNull<DbGeometry>(geometryValue, "geometryValue");
			object obj = this.ConvertToSqlValue(geometryValue, "geometryValue");
			object obj2 = this.ConvertToSqlValue(otherGeometry, "otherGeometry");
			object sqlBoolean = this.SqlTypes.ImiSqlGeometryStIntersects.Value.Invoke(obj, new object[]
			{
				obj2
			});
			return this.ConvertSqlBooleanToBoolean(sqlBoolean);
		}

		// Token: 0x06000511 RID: 1297 RVA: 0x00019634 File Offset: 0x00017834
		public override bool Touches(DbGeometry geometryValue, DbGeometry otherGeometry)
		{
			Check.NotNull<DbGeometry>(geometryValue, "geometryValue");
			object obj = this.ConvertToSqlValue(geometryValue, "geometryValue");
			object obj2 = this.ConvertToSqlValue(otherGeometry, "otherGeometry");
			object sqlBoolean = this.SqlTypes.ImiSqlGeometryStTouches.Value.Invoke(obj, new object[]
			{
				obj2
			});
			return this.ConvertSqlBooleanToBoolean(sqlBoolean);
		}

		// Token: 0x06000512 RID: 1298 RVA: 0x00019694 File Offset: 0x00017894
		public override bool Crosses(DbGeometry geometryValue, DbGeometry otherGeometry)
		{
			Check.NotNull<DbGeometry>(geometryValue, "geometryValue");
			object obj = this.ConvertToSqlValue(geometryValue, "geometryValue");
			object obj2 = this.ConvertToSqlValue(otherGeometry, "otherGeometry");
			object sqlBoolean = this.SqlTypes.ImiSqlGeometryStCrosses.Value.Invoke(obj, new object[]
			{
				obj2
			});
			return this.ConvertSqlBooleanToBoolean(sqlBoolean);
		}

		// Token: 0x06000513 RID: 1299 RVA: 0x000196F4 File Offset: 0x000178F4
		public override bool Within(DbGeometry geometryValue, DbGeometry otherGeometry)
		{
			Check.NotNull<DbGeometry>(geometryValue, "geometryValue");
			object obj = this.ConvertToSqlValue(geometryValue, "geometryValue");
			object obj2 = this.ConvertToSqlValue(otherGeometry, "otherGeometry");
			object sqlBoolean = this.SqlTypes.ImiSqlGeometryStWithin.Value.Invoke(obj, new object[]
			{
				obj2
			});
			return this.ConvertSqlBooleanToBoolean(sqlBoolean);
		}

		// Token: 0x06000514 RID: 1300 RVA: 0x00019754 File Offset: 0x00017954
		public override bool Contains(DbGeometry geometryValue, DbGeometry otherGeometry)
		{
			Check.NotNull<DbGeometry>(geometryValue, "geometryValue");
			object obj = this.ConvertToSqlValue(geometryValue, "geometryValue");
			object obj2 = this.ConvertToSqlValue(otherGeometry, "otherGeometry");
			object sqlBoolean = this.SqlTypes.ImiSqlGeometryStContains.Value.Invoke(obj, new object[]
			{
				obj2
			});
			return this.ConvertSqlBooleanToBoolean(sqlBoolean);
		}

		// Token: 0x06000515 RID: 1301 RVA: 0x000197B4 File Offset: 0x000179B4
		public override bool Overlaps(DbGeometry geometryValue, DbGeometry otherGeometry)
		{
			Check.NotNull<DbGeometry>(geometryValue, "geometryValue");
			object obj = this.ConvertToSqlValue(geometryValue, "geometryValue");
			object obj2 = this.ConvertToSqlValue(otherGeometry, "otherGeometry");
			object sqlBoolean = this.SqlTypes.ImiSqlGeometryStOverlaps.Value.Invoke(obj, new object[]
			{
				obj2
			});
			return this.ConvertSqlBooleanToBoolean(sqlBoolean);
		}

		// Token: 0x06000516 RID: 1302 RVA: 0x00019814 File Offset: 0x00017A14
		public override bool Relate(DbGeometry geometryValue, DbGeometry otherGeometry, string matrix)
		{
			Check.NotNull<DbGeometry>(geometryValue, "geometryValue");
			object obj = this.ConvertToSqlValue(geometryValue, "geometryValue");
			object obj2 = this.ConvertToSqlValue(otherGeometry, "otherGeometry");
			object sqlBoolean = this.SqlTypes.ImiSqlGeometryStRelate.Value.Invoke(obj, new object[]
			{
				obj2,
				matrix
			});
			return this.ConvertSqlBooleanToBoolean(sqlBoolean);
		}

		// Token: 0x06000517 RID: 1303 RVA: 0x00019878 File Offset: 0x00017A78
		public override DbGeometry Buffer(DbGeometry geometryValue, double distance)
		{
			Check.NotNull<DbGeometry>(geometryValue, "geometryValue");
			object obj = this.ConvertToSqlValue(geometryValue, "geometryValue");
			object providerValue = this.SqlTypes.ImiSqlGeometryStBuffer.Value.Invoke(obj, new object[]
			{
				distance
			});
			return this.GeometryFromProviderValue(providerValue);
		}

		// Token: 0x06000518 RID: 1304 RVA: 0x000198D0 File Offset: 0x00017AD0
		public override double Distance(DbGeometry geometryValue, DbGeometry otherGeometry)
		{
			Check.NotNull<DbGeometry>(geometryValue, "geometryValue");
			object obj = this.ConvertToSqlValue(geometryValue, "geometryValue");
			object obj2 = this.ConvertToSqlValue(otherGeometry, "otherGeometry");
			object sqlDoubleValue = this.SqlTypes.ImiSqlGeometryStDistance.Value.Invoke(obj, new object[]
			{
				obj2
			});
			return this.ConvertSqlDoubleToDouble(sqlDoubleValue);
		}

		// Token: 0x06000519 RID: 1305 RVA: 0x00019930 File Offset: 0x00017B30
		public override DbGeometry GetConvexHull(DbGeometry geometryValue)
		{
			Check.NotNull<DbGeometry>(geometryValue, "geometryValue");
			object obj = this.ConvertToSqlValue(geometryValue, "geometryValue");
			object providerValue = this.SqlTypes.ImiSqlGeometryStConvexHull.Value.Invoke(obj, new object[0]);
			return this.GeometryFromProviderValue(providerValue);
		}

		// Token: 0x0600051A RID: 1306 RVA: 0x0001997C File Offset: 0x00017B7C
		public override DbGeometry Intersection(DbGeometry geometryValue, DbGeometry otherGeometry)
		{
			Check.NotNull<DbGeometry>(geometryValue, "geometryValue");
			object obj = this.ConvertToSqlValue(geometryValue, "geometryValue");
			object obj2 = this.ConvertToSqlValue(otherGeometry, "otherGeometry");
			object providerValue = this.SqlTypes.ImiSqlGeometryStIntersection.Value.Invoke(obj, new object[]
			{
				obj2
			});
			return this.GeometryFromProviderValue(providerValue);
		}

		// Token: 0x0600051B RID: 1307 RVA: 0x000199DC File Offset: 0x00017BDC
		public override DbGeometry Union(DbGeometry geometryValue, DbGeometry otherGeometry)
		{
			Check.NotNull<DbGeometry>(geometryValue, "geometryValue");
			object obj = this.ConvertToSqlValue(geometryValue, "geometryValue");
			object obj2 = this.ConvertToSqlValue(otherGeometry, "otherGeometry");
			object providerValue = this.SqlTypes.ImiSqlGeometryStUnion.Value.Invoke(obj, new object[]
			{
				obj2
			});
			return this.GeometryFromProviderValue(providerValue);
		}

		// Token: 0x0600051C RID: 1308 RVA: 0x00019A3C File Offset: 0x00017C3C
		public override DbGeometry Difference(DbGeometry geometryValue, DbGeometry otherGeometry)
		{
			Check.NotNull<DbGeometry>(geometryValue, "geometryValue");
			object obj = this.ConvertToSqlValue(geometryValue, "geometryValue");
			object obj2 = this.ConvertToSqlValue(otherGeometry, "otherGeometry");
			object providerValue = this.SqlTypes.ImiSqlGeometryStDifference.Value.Invoke(obj, new object[]
			{
				obj2
			});
			return this.GeometryFromProviderValue(providerValue);
		}

		// Token: 0x0600051D RID: 1309 RVA: 0x00019A9C File Offset: 0x00017C9C
		public override DbGeometry SymmetricDifference(DbGeometry geometryValue, DbGeometry otherGeometry)
		{
			Check.NotNull<DbGeometry>(geometryValue, "geometryValue");
			object obj = this.ConvertToSqlValue(geometryValue, "geometryValue");
			object obj2 = this.ConvertToSqlValue(otherGeometry, "otherGeometry");
			object providerValue = this.SqlTypes.ImiSqlGeometryStSymDifference.Value.Invoke(obj, new object[]
			{
				obj2
			});
			return this.GeometryFromProviderValue(providerValue);
		}

		// Token: 0x0600051E RID: 1310 RVA: 0x00019AFC File Offset: 0x00017CFC
		public override int? GetElementCount(DbGeometry geometryValue)
		{
			Check.NotNull<DbGeometry>(geometryValue, "geometryValue");
			object obj = this.ConvertToSqlValue(geometryValue, "geometryValue");
			object sqlInt32Value = this.SqlTypes.ImiSqlGeometryStNumGeometries.Value.Invoke(obj, new object[0]);
			return this.ConvertSqlInt32ToNullableInt(sqlInt32Value);
		}

		// Token: 0x0600051F RID: 1311 RVA: 0x00019B48 File Offset: 0x00017D48
		public override DbGeometry ElementAt(DbGeometry geometryValue, int index)
		{
			Check.NotNull<DbGeometry>(geometryValue, "geometryValue");
			object obj = this.ConvertToSqlValue(geometryValue, "geometryValue");
			object providerValue = this.SqlTypes.ImiSqlGeometryStGeometryN.Value.Invoke(obj, new object[]
			{
				index
			});
			return this.GeometryFromProviderValue(providerValue);
		}

		// Token: 0x06000520 RID: 1312 RVA: 0x00019BA0 File Offset: 0x00017DA0
		public override double? GetXCoordinate(DbGeometry geometryValue)
		{
			Check.NotNull<DbGeometry>(geometryValue, "geometryValue");
			object obj = this.ConvertToSqlValue(geometryValue, "geometryValue");
			object value = this.SqlTypes.IpiSqlGeometryStx.Value.GetValue(obj, null);
			return this.ConvertSqlDoubleToNullableDouble(value);
		}

		// Token: 0x06000521 RID: 1313 RVA: 0x00019BE8 File Offset: 0x00017DE8
		public override double? GetYCoordinate(DbGeometry geometryValue)
		{
			Check.NotNull<DbGeometry>(geometryValue, "geometryValue");
			object obj = this.ConvertToSqlValue(geometryValue, "geometryValue");
			object value = this.SqlTypes.IpiSqlGeometrySty.Value.GetValue(obj, null);
			return this.ConvertSqlDoubleToNullableDouble(value);
		}

		// Token: 0x06000522 RID: 1314 RVA: 0x00019C30 File Offset: 0x00017E30
		public override double? GetElevation(DbGeometry geometryValue)
		{
			Check.NotNull<DbGeometry>(geometryValue, "geometryValue");
			object obj = this.ConvertToSqlValue(geometryValue, "geometryValue");
			object value = this.SqlTypes.IpiSqlGeometryZ.Value.GetValue(obj, null);
			return this.ConvertSqlDoubleToNullableDouble(value);
		}

		// Token: 0x06000523 RID: 1315 RVA: 0x00019C78 File Offset: 0x00017E78
		public override double? GetMeasure(DbGeometry geometryValue)
		{
			Check.NotNull<DbGeometry>(geometryValue, "geometryValue");
			object obj = this.ConvertToSqlValue(geometryValue, "geometryValue");
			object value = this.SqlTypes.IpiSqlGeometryM.Value.GetValue(obj, null);
			return this.ConvertSqlDoubleToNullableDouble(value);
		}

		// Token: 0x06000524 RID: 1316 RVA: 0x00019CC0 File Offset: 0x00017EC0
		public override double? GetLength(DbGeometry geometryValue)
		{
			Check.NotNull<DbGeometry>(geometryValue, "geometryValue");
			object obj = this.ConvertToSqlValue(geometryValue, "geometryValue");
			object sqlDoubleValue = this.SqlTypes.ImiSqlGeometryStLength.Value.Invoke(obj, new object[0]);
			return this.ConvertSqlDoubleToNullableDouble(sqlDoubleValue);
		}

		// Token: 0x06000525 RID: 1317 RVA: 0x00019D0C File Offset: 0x00017F0C
		public override DbGeometry GetStartPoint(DbGeometry geometryValue)
		{
			Check.NotNull<DbGeometry>(geometryValue, "geometryValue");
			object obj = this.ConvertToSqlValue(geometryValue, "geometryValue");
			object providerValue = this.SqlTypes.ImiSqlGeometryStStartPoint.Value.Invoke(obj, new object[0]);
			return this.GeometryFromProviderValue(providerValue);
		}

		// Token: 0x06000526 RID: 1318 RVA: 0x00019D58 File Offset: 0x00017F58
		public override DbGeometry GetEndPoint(DbGeometry geometryValue)
		{
			Check.NotNull<DbGeometry>(geometryValue, "geometryValue");
			object obj = this.ConvertToSqlValue(geometryValue, "geometryValue");
			object providerValue = this.SqlTypes.ImiSqlGeometryStEndPoint.Value.Invoke(obj, new object[0]);
			return this.GeometryFromProviderValue(providerValue);
		}

		// Token: 0x06000527 RID: 1319 RVA: 0x00019DA4 File Offset: 0x00017FA4
		public override bool? GetIsClosed(DbGeometry geometryValue)
		{
			Check.NotNull<DbGeometry>(geometryValue, "geometryValue");
			object obj = this.ConvertToSqlValue(geometryValue, "geometryValue");
			object sqlBoolean = this.SqlTypes.ImiSqlGeometryStIsClosed.Value.Invoke(obj, new object[0]);
			return this.ConvertSqlBooleanToNullableBoolean(sqlBoolean);
		}

		// Token: 0x06000528 RID: 1320 RVA: 0x00019DF0 File Offset: 0x00017FF0
		public override bool? GetIsRing(DbGeometry geometryValue)
		{
			Check.NotNull<DbGeometry>(geometryValue, "geometryValue");
			object obj = this.ConvertToSqlValue(geometryValue, "geometryValue");
			object sqlBoolean = this.SqlTypes.ImiSqlGeometryStIsRing.Value.Invoke(obj, new object[0]);
			return this.ConvertSqlBooleanToNullableBoolean(sqlBoolean);
		}

		// Token: 0x06000529 RID: 1321 RVA: 0x00019E3C File Offset: 0x0001803C
		public override int? GetPointCount(DbGeometry geometryValue)
		{
			Check.NotNull<DbGeometry>(geometryValue, "geometryValue");
			object obj = this.ConvertToSqlValue(geometryValue, "geometryValue");
			object sqlInt32Value = this.SqlTypes.ImiSqlGeometryStNumPoints.Value.Invoke(obj, new object[0]);
			return this.ConvertSqlInt32ToNullableInt(sqlInt32Value);
		}

		// Token: 0x0600052A RID: 1322 RVA: 0x00019E88 File Offset: 0x00018088
		public override DbGeometry PointAt(DbGeometry geometryValue, int index)
		{
			Check.NotNull<DbGeometry>(geometryValue, "geometryValue");
			object obj = this.ConvertToSqlValue(geometryValue, "geometryValue");
			object providerValue = this.SqlTypes.ImiSqlGeometryStPointN.Value.Invoke(obj, new object[]
			{
				index
			});
			return this.GeometryFromProviderValue(providerValue);
		}

		// Token: 0x0600052B RID: 1323 RVA: 0x00019EE0 File Offset: 0x000180E0
		public override double? GetArea(DbGeometry geometryValue)
		{
			Check.NotNull<DbGeometry>(geometryValue, "geometryValue");
			object obj = this.ConvertToSqlValue(geometryValue, "geometryValue");
			object sqlDoubleValue = this.SqlTypes.ImiSqlGeometryStArea.Value.Invoke(obj, new object[0]);
			return this.ConvertSqlDoubleToNullableDouble(sqlDoubleValue);
		}

		// Token: 0x0600052C RID: 1324 RVA: 0x00019F2C File Offset: 0x0001812C
		public override DbGeometry GetCentroid(DbGeometry geometryValue)
		{
			Check.NotNull<DbGeometry>(geometryValue, "geometryValue");
			object obj = this.ConvertToSqlValue(geometryValue, "geometryValue");
			object providerValue = this.SqlTypes.ImiSqlGeometryStCentroid.Value.Invoke(obj, new object[0]);
			return this.GeometryFromProviderValue(providerValue);
		}

		// Token: 0x0600052D RID: 1325 RVA: 0x00019F78 File Offset: 0x00018178
		public override DbGeometry GetPointOnSurface(DbGeometry geometryValue)
		{
			Check.NotNull<DbGeometry>(geometryValue, "geometryValue");
			object obj = this.ConvertToSqlValue(geometryValue, "geometryValue");
			object providerValue = this.SqlTypes.ImiSqlGeometryStPointOnSurface.Value.Invoke(obj, new object[0]);
			return this.GeometryFromProviderValue(providerValue);
		}

		// Token: 0x0600052E RID: 1326 RVA: 0x00019FC4 File Offset: 0x000181C4
		public override DbGeometry GetExteriorRing(DbGeometry geometryValue)
		{
			Check.NotNull<DbGeometry>(geometryValue, "geometryValue");
			object obj = this.ConvertToSqlValue(geometryValue, "geometryValue");
			object providerValue = this.SqlTypes.ImiSqlGeometryStExteriorRing.Value.Invoke(obj, new object[0]);
			return this.GeometryFromProviderValue(providerValue);
		}

		// Token: 0x0600052F RID: 1327 RVA: 0x0001A010 File Offset: 0x00018210
		public override int? GetInteriorRingCount(DbGeometry geometryValue)
		{
			Check.NotNull<DbGeometry>(geometryValue, "geometryValue");
			object obj = this.ConvertToSqlValue(geometryValue, "geometryValue");
			object sqlInt32Value = this.SqlTypes.ImiSqlGeometryStNumInteriorRing.Value.Invoke(obj, new object[0]);
			return this.ConvertSqlInt32ToNullableInt(sqlInt32Value);
		}

		// Token: 0x06000530 RID: 1328 RVA: 0x0001A05C File Offset: 0x0001825C
		public override DbGeometry InteriorRingAt(DbGeometry geometryValue, int index)
		{
			Check.NotNull<DbGeometry>(geometryValue, "geometryValue");
			object obj = this.ConvertToSqlValue(geometryValue, "geometryValue");
			object providerValue = this.SqlTypes.ImiSqlGeometryStInteriorRingN.Value.Invoke(obj, new object[]
			{
				index
			});
			return this.GeometryFromProviderValue(providerValue);
		}

		// Token: 0x0400011F RID: 287
		internal static readonly SqlSpatialServices Instance = new SqlSpatialServices();

		// Token: 0x04000120 RID: 288
		private static Dictionary<string, SqlSpatialServices> _otherSpatialServices;

		// Token: 0x04000121 RID: 289
		[NonSerialized]
		private readonly SqlTypesAssemblyLoader _loader;
	}
}
