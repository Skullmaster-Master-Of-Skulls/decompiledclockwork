using System;
using System.Data.Entity.Spatial;
using System.Data.Entity.SqlServer.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq.Expressions;
using System.Reflection;
using System.Xml;

namespace System.Data.Entity.SqlServer
{
	// Token: 0x02000047 RID: 71
	internal class SqlTypesAssembly
	{
		// Token: 0x06000538 RID: 1336 RVA: 0x0001A0BD File Offset: 0x000182BD
		public SqlTypesAssembly()
		{
		}

		// Token: 0x06000539 RID: 1337 RVA: 0x0001B53C File Offset: 0x0001973C
		[SuppressMessage("Microsoft.Performance", "CA1809:AvoidExcessiveLocals")]
		[SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
		[SuppressMessage("Microsoft.Maintainability", "CA1505:AvoidUnmaintainableCode")]
		public SqlTypesAssembly(Assembly sqlSpatialAssembly)
		{
			Type type = sqlSpatialAssembly.GetType("Microsoft.SqlServer.Types.SqlGeography", true);
			Type type2 = sqlSpatialAssembly.GetType("Microsoft.SqlServer.Types.SqlGeometry", true);
			this.SqlGeographyType = type;
			this.sqlGeographyFromWKTString = SqlTypesAssembly.CreateStaticConstructorDelegate<string>(type, "STGeomFromText");
			this.sqlGeographyFromWKBByteArray = SqlTypesAssembly.CreateStaticConstructorDelegate<byte[]>(type, "STGeomFromWKB");
			this.sqlGeographyFromGMLReader = SqlTypesAssembly.CreateStaticConstructorDelegate<XmlReader>(type, "GeomFromGml");
			this.SqlGeometryType = type2;
			this.sqlGeometryFromWKTString = SqlTypesAssembly.CreateStaticConstructorDelegate<string>(type2, "STGeomFromText");
			this.sqlGeometryFromWKBByteArray = SqlTypesAssembly.CreateStaticConstructorDelegate<byte[]>(type2, "STGeomFromWKB");
			this.sqlGeometryFromGMLReader = SqlTypesAssembly.CreateStaticConstructorDelegate<XmlReader>(type2, "GeomFromGml");
			MethodInfo publicInstanceMethod = this.SqlGeometryType.GetPublicInstanceMethod("STAsText", new Type[0]);
			this.SqlCharsType = publicInstanceMethod.ReturnType;
			this.SqlStringType = this.SqlCharsType.Assembly().GetType("System.Data.SqlTypes.SqlString", true);
			this.SqlBooleanType = this.SqlCharsType.Assembly().GetType("System.Data.SqlTypes.SqlBoolean", true);
			this.SqlBytesType = this.SqlCharsType.Assembly().GetType("System.Data.SqlTypes.SqlBytes", true);
			this.SqlDoubleType = this.SqlCharsType.Assembly().GetType("System.Data.SqlTypes.SqlDouble", true);
			this.SqlInt32Type = this.SqlCharsType.Assembly().GetType("System.Data.SqlTypes.SqlInt32", true);
			this.SqlXmlType = this.SqlCharsType.Assembly().GetType("System.Data.SqlTypes.SqlXml", true);
			this.sqlBytesFromByteArray = Expressions.Lambda<byte[], object>("binaryValue", (ParameterExpression bytesVal) => SqlTypesAssembly.BuildConvertToSqlBytes(bytesVal, this.SqlBytesType)).Compile();
			this.sqlStringFromString = Expressions.Lambda<string, object>("stringValue", (ParameterExpression stringVal) => SqlTypesAssembly.BuildConvertToSqlString(stringVal, this.SqlStringType)).Compile();
			this.sqlCharsFromString = Expressions.Lambda<string, object>("stringValue", (ParameterExpression stringVal) => SqlTypesAssembly.BuildConvertToSqlChars(stringVal, this.SqlCharsType)).Compile();
			this.sqlXmlFromXmlReader = Expressions.Lambda<XmlReader, object>("readerVaue", (ParameterExpression readerVal) => SqlTypesAssembly.BuildConvertToSqlXml(readerVal, this.SqlXmlType)).Compile();
			this.sqlBooleanToBoolean = Expressions.Lambda<object, bool>("sqlBooleanValue", (ParameterExpression sqlBoolVal) => sqlBoolVal.ConvertTo(this.SqlBooleanType).ConvertTo<bool>()).Compile();
			this.sqlBooleanToNullableBoolean = Expressions.Lambda<object, bool?>("sqlBooleanValue", (ParameterExpression sqlBoolVal) => sqlBoolVal.ConvertTo(this.SqlBooleanType).Property("IsNull").IfTrueThen(Expressions.Null<bool?>()).Else(sqlBoolVal.ConvertTo(this.SqlBooleanType).ConvertTo<bool>().ConvertTo<bool?>())).Compile();
			this.sqlBytesToByteArray = Expressions.Lambda<object, byte[]>("sqlBytesValue", (ParameterExpression sqlBytesVal) => sqlBytesVal.ConvertTo(this.SqlBytesType).Property("Value")).Compile();
			this.sqlCharsToString = Expressions.Lambda<object, string>("sqlCharsValue", (ParameterExpression sqlCharsVal) => sqlCharsVal.ConvertTo(this.SqlCharsType).Call("ToSqlString").Property("Value")).Compile();
			this.sqlStringToString = Expressions.Lambda<object, string>("sqlStringValue", (ParameterExpression sqlStringVal) => sqlStringVal.ConvertTo(this.SqlStringType).Property("Value")).Compile();
			this.sqlDoubleToDouble = Expressions.Lambda<object, double>("sqlDoubleValue", (ParameterExpression sqlDoubleVal) => sqlDoubleVal.ConvertTo(this.SqlDoubleType).ConvertTo<double>()).Compile();
			this.sqlDoubleToNullableDouble = Expressions.Lambda<object, double?>("sqlDoubleValue", (ParameterExpression sqlDoubleVal) => sqlDoubleVal.ConvertTo(this.SqlDoubleType).Property("IsNull").IfTrueThen(Expressions.Null<double?>()).Else(sqlDoubleVal.ConvertTo(this.SqlDoubleType).ConvertTo<double>().ConvertTo<double?>())).Compile();
			this.sqlInt32ToInt = Expressions.Lambda<object, int>("sqlInt32Value", (ParameterExpression sqlInt32Val) => sqlInt32Val.ConvertTo(this.SqlInt32Type).ConvertTo<int>()).Compile();
			this.sqlInt32ToNullableInt = Expressions.Lambda<object, int?>("sqlInt32Value", (ParameterExpression sqlInt32Val) => sqlInt32Val.ConvertTo(this.SqlInt32Type).Property("IsNull").IfTrueThen(Expressions.Null<int?>()).Else(sqlInt32Val.ConvertTo(this.SqlInt32Type).ConvertTo<int>().ConvertTo<int?>())).Compile();
			this.sqlXmlToString = Expressions.Lambda<object, string>("sqlXmlValue", (ParameterExpression sqlXmlVal) => sqlXmlVal.ConvertTo(this.SqlXmlType).Property("Value")).Compile();
			this.isSqlGeographyNull = Expressions.Lambda<object, bool>("sqlGeographyValue", (ParameterExpression sqlGeographyValue) => sqlGeographyValue.ConvertTo(this.SqlGeographyType).Property("IsNull")).Compile();
			this.isSqlGeometryNull = Expressions.Lambda<object, bool>("sqlGeometryValue", (ParameterExpression sqlGeometryValue) => sqlGeometryValue.ConvertTo(this.SqlGeometryType).Property("IsNull")).Compile();
			this.geographyAsTextZMAsSqlChars = Expressions.Lambda<object, object>("sqlGeographyValue", (ParameterExpression sqlGeographyValue) => sqlGeographyValue.ConvertTo(this.SqlGeographyType).Call("AsTextZM")).Compile();
			this.geometryAsTextZMAsSqlChars = Expressions.Lambda<object, object>("sqlGeometryValue", (ParameterExpression sqlGeometryValue) => sqlGeometryValue.ConvertTo(this.SqlGeometryType).Call("AsTextZM")).Compile();
			this._smiSqlGeographyParse = new Lazy<MethodInfo>(() => this.FindSqlGeographyStaticMethod("Parse", new Type[]
			{
				this.SqlStringType
			}), true);
			this._smiSqlGeographyStGeomFromText = new Lazy<MethodInfo>(() => this.FindSqlGeographyStaticMethod("STGeomFromText", new Type[]
			{
				this.SqlCharsType,
				typeof(int)
			}), true);
			this._smiSqlGeographyStPointFromText = new Lazy<MethodInfo>(() => this.FindSqlGeographyStaticMethod("STPointFromText", new Type[]
			{
				this.SqlCharsType,
				typeof(int)
			}), true);
			this._smiSqlGeographyStLineFromText = new Lazy<MethodInfo>(() => this.FindSqlGeographyStaticMethod("STLineFromText", new Type[]
			{
				this.SqlCharsType,
				typeof(int)
			}), true);
			this._smiSqlGeographyStPolyFromText = new Lazy<MethodInfo>(() => this.FindSqlGeographyStaticMethod("STPolyFromText", new Type[]
			{
				this.SqlCharsType,
				typeof(int)
			}), true);
			this._smiSqlGeographyStmPointFromText = new Lazy<MethodInfo>(() => this.FindSqlGeographyStaticMethod("STMPointFromText", new Type[]
			{
				this.SqlCharsType,
				typeof(int)
			}), true);
			this._smiSqlGeographyStmLineFromText = new Lazy<MethodInfo>(() => this.FindSqlGeographyStaticMethod("STMLineFromText", new Type[]
			{
				this.SqlCharsType,
				typeof(int)
			}), true);
			this._smiSqlGeographyStmPolyFromText = new Lazy<MethodInfo>(() => this.FindSqlGeographyStaticMethod("STMPolyFromText", new Type[]
			{
				this.SqlCharsType,
				typeof(int)
			}), true);
			this._smiSqlGeographyStGeomCollFromText = new Lazy<MethodInfo>(() => this.FindSqlGeographyStaticMethod("STGeomCollFromText", new Type[]
			{
				this.SqlCharsType,
				typeof(int)
			}), true);
			this._smiSqlGeographyStGeomFromWkb = new Lazy<MethodInfo>(() => this.FindSqlGeographyStaticMethod("STGeomFromWKB", new Type[]
			{
				this.SqlBytesType,
				typeof(int)
			}), true);
			this._smiSqlGeographyStPointFromWkb = new Lazy<MethodInfo>(() => this.FindSqlGeographyStaticMethod("STPointFromWKB", new Type[]
			{
				this.SqlBytesType,
				typeof(int)
			}), true);
			this._smiSqlGeographyStLineFromWkb = new Lazy<MethodInfo>(() => this.FindSqlGeographyStaticMethod("STLineFromWKB", new Type[]
			{
				this.SqlBytesType,
				typeof(int)
			}), true);
			this._smiSqlGeographyStPolyFromWkb = new Lazy<MethodInfo>(() => this.FindSqlGeographyStaticMethod("STPolyFromWKB", new Type[]
			{
				this.SqlBytesType,
				typeof(int)
			}), true);
			this._smiSqlGeographyStmPointFromWkb = new Lazy<MethodInfo>(() => this.FindSqlGeographyStaticMethod("STMPointFromWKB", new Type[]
			{
				this.SqlBytesType,
				typeof(int)
			}), true);
			this._smiSqlGeographyStmLineFromWkb = new Lazy<MethodInfo>(() => this.FindSqlGeographyStaticMethod("STMLineFromWKB", new Type[]
			{
				this.SqlBytesType,
				typeof(int)
			}), true);
			this._smiSqlGeographyStmPolyFromWkb = new Lazy<MethodInfo>(() => this.FindSqlGeographyStaticMethod("STMPolyFromWKB", new Type[]
			{
				this.SqlBytesType,
				typeof(int)
			}), true);
			this._smiSqlGeographyStGeomCollFromWkb = new Lazy<MethodInfo>(() => this.FindSqlGeographyStaticMethod("STGeomCollFromWKB", new Type[]
			{
				this.SqlBytesType,
				typeof(int)
			}), true);
			this._smiSqlGeographyGeomFromGml = new Lazy<MethodInfo>(() => this.FindSqlGeographyStaticMethod("GeomFromGml", new Type[]
			{
				this.SqlXmlType,
				typeof(int)
			}), true);
			this._ipiSqlGeographyStSrid = new Lazy<PropertyInfo>(() => this.FindSqlGeographyProperty("STSrid"), true);
			this._imiSqlGeographyStGeometryType = new Lazy<MethodInfo>(() => this.FindSqlGeographyMethod("STGeometryType", new Type[0]), true);
			this._imiSqlGeographyStDimension = new Lazy<MethodInfo>(() => this.FindSqlGeographyMethod("STDimension", new Type[0]), true);
			this._imiSqlGeographyStAsBinary = new Lazy<MethodInfo>(() => this.FindSqlGeographyMethod("STAsBinary", new Type[0]), true);
			this._imiSqlGeographyAsGml = new Lazy<MethodInfo>(() => this.FindSqlGeographyMethod("AsGml", new Type[0]), true);
			this._imiSqlGeographyStAsText = new Lazy<MethodInfo>(() => this.FindSqlGeographyMethod("STAsText", new Type[0]), true);
			this._imiSqlGeographyStIsEmpty = new Lazy<MethodInfo>(() => this.FindSqlGeographyMethod("STIsEmpty", new Type[0]), true);
			this._imiSqlGeographyStEquals = new Lazy<MethodInfo>(() => this.FindSqlGeographyMethod("STEquals", new Type[]
			{
				this.SqlGeographyType
			}), true);
			this._imiSqlGeographyStDisjoint = new Lazy<MethodInfo>(() => this.FindSqlGeographyMethod("STDisjoint", new Type[]
			{
				this.SqlGeographyType
			}), true);
			this._imiSqlGeographyStIntersects = new Lazy<MethodInfo>(() => this.FindSqlGeographyMethod("STIntersects", new Type[]
			{
				this.SqlGeographyType
			}), true);
			this._imiSqlGeographyStBuffer = new Lazy<MethodInfo>(() => this.FindSqlGeographyMethod("STBuffer", new Type[]
			{
				typeof(double)
			}), true);
			this._imiSqlGeographyStDistance = new Lazy<MethodInfo>(() => this.FindSqlGeographyMethod("STDistance", new Type[]
			{
				this.SqlGeographyType
			}), true);
			this._imiSqlGeographyStIntersection = new Lazy<MethodInfo>(() => this.FindSqlGeographyMethod("STIntersection", new Type[]
			{
				this.SqlGeographyType
			}), true);
			this._imiSqlGeographyStUnion = new Lazy<MethodInfo>(() => this.FindSqlGeographyMethod("STUnion", new Type[]
			{
				this.SqlGeographyType
			}), true);
			this._imiSqlGeographyStDifference = new Lazy<MethodInfo>(() => this.FindSqlGeographyMethod("STDifference", new Type[]
			{
				this.SqlGeographyType
			}), true);
			this._imiSqlGeographyStSymDifference = new Lazy<MethodInfo>(() => this.FindSqlGeographyMethod("STSymDifference", new Type[]
			{
				this.SqlGeographyType
			}), true);
			this._imiSqlGeographyStNumGeometries = new Lazy<MethodInfo>(() => this.FindSqlGeographyMethod("STNumGeometries", new Type[0]), true);
			this._imiSqlGeographyStGeometryN = new Lazy<MethodInfo>(() => this.FindSqlGeographyMethod("STGeometryN", new Type[]
			{
				typeof(int)
			}), true);
			this._ipiSqlGeographyLat = new Lazy<PropertyInfo>(() => this.FindSqlGeographyProperty("Lat"), true);
			this._ipiSqlGeographyLong = new Lazy<PropertyInfo>(() => this.FindSqlGeographyProperty("Long"), true);
			this._ipiSqlGeographyZ = new Lazy<PropertyInfo>(() => this.FindSqlGeographyProperty("Z"), true);
			this._ipiSqlGeographyM = new Lazy<PropertyInfo>(() => this.FindSqlGeographyProperty("M"), true);
			this._imiSqlGeographyStLength = new Lazy<MethodInfo>(() => this.FindSqlGeographyMethod("STLength", new Type[0]), true);
			this._imiSqlGeographyStStartPoint = new Lazy<MethodInfo>(() => this.FindSqlGeographyMethod("STStartPoint", new Type[0]), true);
			this._imiSqlGeographyStEndPoint = new Lazy<MethodInfo>(() => this.FindSqlGeographyMethod("STEndPoint", new Type[0]), true);
			this._imiSqlGeographyStIsClosed = new Lazy<MethodInfo>(() => this.FindSqlGeographyMethod("STIsClosed", new Type[0]), true);
			this._imiSqlGeographyStNumPoints = new Lazy<MethodInfo>(() => this.FindSqlGeographyMethod("STNumPoints", new Type[0]), true);
			this._imiSqlGeographyStPointN = new Lazy<MethodInfo>(() => this.FindSqlGeographyMethod("STPointN", new Type[]
			{
				typeof(int)
			}), true);
			this._imiSqlGeographyStArea = new Lazy<MethodInfo>(() => this.FindSqlGeographyMethod("STArea", new Type[0]), true);
			this._smiSqlGeometryParse = new Lazy<MethodInfo>(() => this.FindSqlGeometryStaticMethod("Parse", new Type[]
			{
				this.SqlStringType
			}), true);
			this._smiSqlGeometryStGeomFromText = new Lazy<MethodInfo>(() => this.FindSqlGeometryStaticMethod("STGeomFromText", new Type[]
			{
				this.SqlCharsType,
				typeof(int)
			}), true);
			this._smiSqlGeometryStPointFromText = new Lazy<MethodInfo>(() => this.FindSqlGeometryStaticMethod("STPointFromText", new Type[]
			{
				this.SqlCharsType,
				typeof(int)
			}), true);
			this._smiSqlGeometryStLineFromText = new Lazy<MethodInfo>(() => this.FindSqlGeometryStaticMethod("STLineFromText", new Type[]
			{
				this.SqlCharsType,
				typeof(int)
			}), true);
			this._smiSqlGeometryStPolyFromText = new Lazy<MethodInfo>(() => this.FindSqlGeometryStaticMethod("STPolyFromText", new Type[]
			{
				this.SqlCharsType,
				typeof(int)
			}), true);
			this._smiSqlGeometryStmPointFromText = new Lazy<MethodInfo>(() => this.FindSqlGeometryStaticMethod("STMPointFromText", new Type[]
			{
				this.SqlCharsType,
				typeof(int)
			}), true);
			this._smiSqlGeometryStmLineFromText = new Lazy<MethodInfo>(() => this.FindSqlGeometryStaticMethod("STMLineFromText", new Type[]
			{
				this.SqlCharsType,
				typeof(int)
			}), true);
			this._smiSqlGeometryStmPolyFromText = new Lazy<MethodInfo>(() => this.FindSqlGeometryStaticMethod("STMPolyFromText", new Type[]
			{
				this.SqlCharsType,
				typeof(int)
			}), true);
			this._smiSqlGeometryStGeomCollFromText = new Lazy<MethodInfo>(() => this.FindSqlGeometryStaticMethod("STGeomCollFromText", new Type[]
			{
				this.SqlCharsType,
				typeof(int)
			}), true);
			this._smiSqlGeometryStGeomFromWkb = new Lazy<MethodInfo>(() => this.FindSqlGeometryStaticMethod("STGeomFromWKB", new Type[]
			{
				this.SqlBytesType,
				typeof(int)
			}), true);
			this._smiSqlGeometryStPointFromWkb = new Lazy<MethodInfo>(() => this.FindSqlGeometryStaticMethod("STPointFromWKB", new Type[]
			{
				this.SqlBytesType,
				typeof(int)
			}), true);
			this._smiSqlGeometryStLineFromWkb = new Lazy<MethodInfo>(() => this.FindSqlGeometryStaticMethod("STLineFromWKB", new Type[]
			{
				this.SqlBytesType,
				typeof(int)
			}), true);
			this._smiSqlGeometryStPolyFromWkb = new Lazy<MethodInfo>(() => this.FindSqlGeometryStaticMethod("STPolyFromWKB", new Type[]
			{
				this.SqlBytesType,
				typeof(int)
			}), true);
			this._smiSqlGeometryStmPointFromWkb = new Lazy<MethodInfo>(() => this.FindSqlGeometryStaticMethod("STMPointFromWKB", new Type[]
			{
				this.SqlBytesType,
				typeof(int)
			}), true);
			this._smiSqlGeometryStmLineFromWkb = new Lazy<MethodInfo>(() => this.FindSqlGeometryStaticMethod("STMLineFromWKB", new Type[]
			{
				this.SqlBytesType,
				typeof(int)
			}), true);
			this._smiSqlGeometryStmPolyFromWkb = new Lazy<MethodInfo>(() => this.FindSqlGeometryStaticMethod("STMPolyFromWKB", new Type[]
			{
				this.SqlBytesType,
				typeof(int)
			}), true);
			this._smiSqlGeometryStGeomCollFromWkb = new Lazy<MethodInfo>(() => this.FindSqlGeometryStaticMethod("STGeomCollFromWKB", new Type[]
			{
				this.SqlBytesType,
				typeof(int)
			}), true);
			this._smiSqlGeometryGeomFromGml = new Lazy<MethodInfo>(() => this.FindSqlGeometryStaticMethod("GeomFromGml", new Type[]
			{
				this.SqlXmlType,
				typeof(int)
			}), true);
			this._ipiSqlGeometryStSrid = new Lazy<PropertyInfo>(() => this.FindSqlGeometryProperty("STSrid"), true);
			this._imiSqlGeometryStGeometryType = new Lazy<MethodInfo>(() => this.FindSqlGeometryMethod("STGeometryType", new Type[0]), true);
			this._imiSqlGeometryStDimension = new Lazy<MethodInfo>(() => this.FindSqlGeometryMethod("STDimension", new Type[0]), true);
			this._imiSqlGeometryStEnvelope = new Lazy<MethodInfo>(() => this.FindSqlGeometryMethod("STEnvelope", new Type[0]), true);
			this._imiSqlGeometryStAsBinary = new Lazy<MethodInfo>(() => this.FindSqlGeometryMethod("STAsBinary", new Type[0]), true);
			this._imiSqlGeometryAsGml = new Lazy<MethodInfo>(() => this.FindSqlGeometryMethod("AsGml", new Type[0]), true);
			this._imiSqlGeometryStAsText = new Lazy<MethodInfo>(() => this.FindSqlGeometryMethod("STAsText", new Type[0]), true);
			this._imiSqlGeometryStIsEmpty = new Lazy<MethodInfo>(() => this.FindSqlGeometryMethod("STIsEmpty", new Type[0]), true);
			this._imiSqlGeometryStIsSimple = new Lazy<MethodInfo>(() => this.FindSqlGeometryMethod("STIsSimple", new Type[0]), true);
			this._imiSqlGeometryStBoundary = new Lazy<MethodInfo>(() => this.FindSqlGeometryMethod("STBoundary", new Type[0]), true);
			this._imiSqlGeometryStIsValid = new Lazy<MethodInfo>(() => this.FindSqlGeometryMethod("STIsValid", new Type[0]), true);
			this._imiSqlGeometryStEquals = new Lazy<MethodInfo>(() => this.FindSqlGeometryMethod("STEquals", new Type[]
			{
				this.SqlGeometryType
			}), true);
			this._imiSqlGeometryStDisjoint = new Lazy<MethodInfo>(() => this.FindSqlGeometryMethod("STDisjoint", new Type[]
			{
				this.SqlGeometryType
			}), true);
			this._imiSqlGeometryStIntersects = new Lazy<MethodInfo>(() => this.FindSqlGeometryMethod("STIntersects", new Type[]
			{
				this.SqlGeometryType
			}), true);
			this._imiSqlGeometryStTouches = new Lazy<MethodInfo>(() => this.FindSqlGeometryMethod("STTouches", new Type[]
			{
				this.SqlGeometryType
			}), true);
			this._imiSqlGeometryStCrosses = new Lazy<MethodInfo>(() => this.FindSqlGeometryMethod("STCrosses", new Type[]
			{
				this.SqlGeometryType
			}), true);
			this._imiSqlGeometryStWithin = new Lazy<MethodInfo>(() => this.FindSqlGeometryMethod("STWithin", new Type[]
			{
				this.SqlGeometryType
			}), true);
			this._imiSqlGeometryStContains = new Lazy<MethodInfo>(() => this.FindSqlGeometryMethod("STContains", new Type[]
			{
				this.SqlGeometryType
			}), true);
			this._imiSqlGeometryStOverlaps = new Lazy<MethodInfo>(() => this.FindSqlGeometryMethod("STOverlaps", new Type[]
			{
				this.SqlGeometryType
			}), true);
			this._imiSqlGeometryStRelate = new Lazy<MethodInfo>(() => this.FindSqlGeometryMethod("STRelate", new Type[]
			{
				this.SqlGeometryType,
				typeof(string)
			}), true);
			this._imiSqlGeometryStBuffer = new Lazy<MethodInfo>(() => this.FindSqlGeometryMethod("STBuffer", new Type[]
			{
				typeof(double)
			}), true);
			this._imiSqlGeometryStDistance = new Lazy<MethodInfo>(() => this.FindSqlGeometryMethod("STDistance", new Type[]
			{
				this.SqlGeometryType
			}), true);
			this._imiSqlGeometryStConvexHull = new Lazy<MethodInfo>(() => this.FindSqlGeometryMethod("STConvexHull", new Type[0]), true);
			this._imiSqlGeometryStIntersection = new Lazy<MethodInfo>(() => this.FindSqlGeometryMethod("STIntersection", new Type[]
			{
				this.SqlGeometryType
			}), true);
			this._imiSqlGeometryStUnion = new Lazy<MethodInfo>(() => this.FindSqlGeometryMethod("STUnion", new Type[]
			{
				this.SqlGeometryType
			}), true);
			this._imiSqlGeometryStDifference = new Lazy<MethodInfo>(() => this.FindSqlGeometryMethod("STDifference", new Type[]
			{
				this.SqlGeometryType
			}), true);
			this._imiSqlGeometryStSymDifference = new Lazy<MethodInfo>(() => this.FindSqlGeometryMethod("STSymDifference", new Type[]
			{
				this.SqlGeometryType
			}), true);
			this._imiSqlGeometryStNumGeometries = new Lazy<MethodInfo>(() => this.FindSqlGeometryMethod("STNumGeometries", new Type[0]), true);
			this._imiSqlGeometryStGeometryN = new Lazy<MethodInfo>(() => this.FindSqlGeometryMethod("STGeometryN", new Type[]
			{
				typeof(int)
			}), true);
			this._ipiSqlGeometryStx = new Lazy<PropertyInfo>(() => this.FindSqlGeometryProperty("STX"), true);
			this._ipiSqlGeometrySty = new Lazy<PropertyInfo>(() => this.FindSqlGeometryProperty("STY"), true);
			this._ipiSqlGeometryZ = new Lazy<PropertyInfo>(() => this.FindSqlGeometryProperty("Z"), true);
			this._ipiSqlGeometryM = new Lazy<PropertyInfo>(() => this.FindSqlGeometryProperty("M"), true);
			this._imiSqlGeometryStLength = new Lazy<MethodInfo>(() => this.FindSqlGeometryMethod("STLength", new Type[0]), true);
			this._imiSqlGeometryStStartPoint = new Lazy<MethodInfo>(() => this.FindSqlGeometryMethod("STStartPoint", new Type[0]), true);
			this._imiSqlGeometryStEndPoint = new Lazy<MethodInfo>(() => this.FindSqlGeometryMethod("STEndPoint", new Type[0]), true);
			this._imiSqlGeometryStIsClosed = new Lazy<MethodInfo>(() => this.FindSqlGeometryMethod("STIsClosed", new Type[0]), true);
			this._imiSqlGeometryStIsRing = new Lazy<MethodInfo>(() => this.FindSqlGeometryMethod("STIsRing", new Type[0]), true);
			this._imiSqlGeometryStNumPoints = new Lazy<MethodInfo>(() => this.FindSqlGeometryMethod("STNumPoints", new Type[0]), true);
			this._imiSqlGeometryStPointN = new Lazy<MethodInfo>(() => this.FindSqlGeometryMethod("STPointN", new Type[]
			{
				typeof(int)
			}), true);
			this._imiSqlGeometryStArea = new Lazy<MethodInfo>(() => this.FindSqlGeometryMethod("STArea", new Type[0]), true);
			this._imiSqlGeometryStCentroid = new Lazy<MethodInfo>(() => this.FindSqlGeometryMethod("STCentroid", new Type[0]), true);
			this._imiSqlGeometryStPointOnSurface = new Lazy<MethodInfo>(() => this.FindSqlGeometryMethod("STPointOnSurface", new Type[0]), true);
			this._imiSqlGeometryStExteriorRing = new Lazy<MethodInfo>(() => this.FindSqlGeometryMethod("STExteriorRing", new Type[0]), true);
			this._imiSqlGeometryStNumInteriorRing = new Lazy<MethodInfo>(() => this.FindSqlGeometryMethod("STNumInteriorRing", new Type[0]), true);
			this._imiSqlGeometryStInteriorRingN = new Lazy<MethodInfo>(() => this.FindSqlGeometryMethod("STInteriorRingN", new Type[]
			{
				typeof(int)
			}), true);
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x0600053A RID: 1338 RVA: 0x0001C8E9 File Offset: 0x0001AAE9
		// (set) Token: 0x0600053B RID: 1339 RVA: 0x0001C8F1 File Offset: 0x0001AAF1
		internal Type SqlBooleanType { get; private set; }

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x0600053C RID: 1340 RVA: 0x0001C8FA File Offset: 0x0001AAFA
		// (set) Token: 0x0600053D RID: 1341 RVA: 0x0001C902 File Offset: 0x0001AB02
		internal Type SqlBytesType { get; private set; }

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x0600053E RID: 1342 RVA: 0x0001C90B File Offset: 0x0001AB0B
		// (set) Token: 0x0600053F RID: 1343 RVA: 0x0001C913 File Offset: 0x0001AB13
		internal Type SqlCharsType { get; private set; }

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000540 RID: 1344 RVA: 0x0001C91C File Offset: 0x0001AB1C
		// (set) Token: 0x06000541 RID: 1345 RVA: 0x0001C924 File Offset: 0x0001AB24
		internal Type SqlStringType { get; private set; }

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000542 RID: 1346 RVA: 0x0001C92D File Offset: 0x0001AB2D
		// (set) Token: 0x06000543 RID: 1347 RVA: 0x0001C935 File Offset: 0x0001AB35
		internal Type SqlDoubleType { get; private set; }

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000544 RID: 1348 RVA: 0x0001C93E File Offset: 0x0001AB3E
		// (set) Token: 0x06000545 RID: 1349 RVA: 0x0001C946 File Offset: 0x0001AB46
		internal Type SqlInt32Type { get; private set; }

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000546 RID: 1350 RVA: 0x0001C94F File Offset: 0x0001AB4F
		// (set) Token: 0x06000547 RID: 1351 RVA: 0x0001C957 File Offset: 0x0001AB57
		internal Type SqlXmlType { get; private set; }

		// Token: 0x06000548 RID: 1352 RVA: 0x0001C960 File Offset: 0x0001AB60
		internal bool SqlBooleanToBoolean(object sqlBooleanValue)
		{
			return this.sqlBooleanToBoolean(sqlBooleanValue);
		}

		// Token: 0x06000549 RID: 1353 RVA: 0x0001C970 File Offset: 0x0001AB70
		internal bool? SqlBooleanToNullableBoolean(object sqlBooleanValue)
		{
			if (this.sqlBooleanToBoolean == null)
			{
				return null;
			}
			return this.sqlBooleanToNullableBoolean(sqlBooleanValue);
		}

		// Token: 0x0600054A RID: 1354 RVA: 0x0001C99B File Offset: 0x0001AB9B
		internal object SqlBytesFromByteArray(byte[] binaryValue)
		{
			return this.sqlBytesFromByteArray(binaryValue);
		}

		// Token: 0x0600054B RID: 1355 RVA: 0x0001C9A9 File Offset: 0x0001ABA9
		internal byte[] SqlBytesToByteArray(object sqlBytesValue)
		{
			if (sqlBytesValue == null)
			{
				return null;
			}
			return this.sqlBytesToByteArray(sqlBytesValue);
		}

		// Token: 0x0600054C RID: 1356 RVA: 0x0001C9BC File Offset: 0x0001ABBC
		internal object SqlStringFromString(string stringValue)
		{
			return this.sqlStringFromString(stringValue);
		}

		// Token: 0x0600054D RID: 1357 RVA: 0x0001C9CA File Offset: 0x0001ABCA
		internal object SqlCharsFromString(string stringValue)
		{
			return this.sqlCharsFromString(stringValue);
		}

		// Token: 0x0600054E RID: 1358 RVA: 0x0001C9D8 File Offset: 0x0001ABD8
		internal string SqlCharsToString(object sqlCharsValue)
		{
			if (sqlCharsValue == null)
			{
				return null;
			}
			return this.sqlCharsToString(sqlCharsValue);
		}

		// Token: 0x0600054F RID: 1359 RVA: 0x0001C9EB File Offset: 0x0001ABEB
		internal string SqlStringToString(object sqlStringValue)
		{
			if (sqlStringValue == null)
			{
				return null;
			}
			return this.sqlStringToString(sqlStringValue);
		}

		// Token: 0x06000550 RID: 1360 RVA: 0x0001C9FE File Offset: 0x0001ABFE
		internal double SqlDoubleToDouble(object sqlDoubleValue)
		{
			return this.sqlDoubleToDouble(sqlDoubleValue);
		}

		// Token: 0x06000551 RID: 1361 RVA: 0x0001CA0C File Offset: 0x0001AC0C
		internal double? SqlDoubleToNullableDouble(object sqlDoubleValue)
		{
			if (sqlDoubleValue == null)
			{
				return null;
			}
			return this.sqlDoubleToNullableDouble(sqlDoubleValue);
		}

		// Token: 0x06000552 RID: 1362 RVA: 0x0001CA32 File Offset: 0x0001AC32
		internal int SqlInt32ToInt(object sqlInt32Value)
		{
			return this.sqlInt32ToInt(sqlInt32Value);
		}

		// Token: 0x06000553 RID: 1363 RVA: 0x0001CA40 File Offset: 0x0001AC40
		internal int? SqlInt32ToNullableInt(object sqlInt32Value)
		{
			if (sqlInt32Value == null)
			{
				return null;
			}
			return this.sqlInt32ToNullableInt(sqlInt32Value);
		}

		// Token: 0x06000554 RID: 1364 RVA: 0x0001CA68 File Offset: 0x0001AC68
		internal object SqlXmlFromString(string stringValue)
		{
			XmlReader arg = SqlTypesAssembly.XmlReaderFromString(stringValue);
			return this.sqlXmlFromXmlReader(arg);
		}

		// Token: 0x06000555 RID: 1365 RVA: 0x0001CA88 File Offset: 0x0001AC88
		internal string SqlXmlToString(object sqlXmlValue)
		{
			if (sqlXmlValue == null)
			{
				return null;
			}
			return this.sqlXmlToString(sqlXmlValue);
		}

		// Token: 0x06000556 RID: 1366 RVA: 0x0001CA9B File Offset: 0x0001AC9B
		internal bool IsSqlGeographyNull(object sqlGeographyValue)
		{
			return sqlGeographyValue == null || this.isSqlGeographyNull(sqlGeographyValue);
		}

		// Token: 0x06000557 RID: 1367 RVA: 0x0001CAAE File Offset: 0x0001ACAE
		internal bool IsSqlGeometryNull(object sqlGeometryValue)
		{
			return sqlGeometryValue == null || this.isSqlGeometryNull(sqlGeometryValue);
		}

		// Token: 0x06000558 RID: 1368 RVA: 0x0001CAC4 File Offset: 0x0001ACC4
		internal string GeographyAsTextZM(DbGeography geographyValue)
		{
			if (geographyValue == null)
			{
				return null;
			}
			object arg = this.ConvertToSqlTypesGeography(geographyValue);
			object sqlCharsValue = this.geographyAsTextZMAsSqlChars(arg);
			return this.SqlCharsToString(sqlCharsValue);
		}

		// Token: 0x06000559 RID: 1369 RVA: 0x0001CAF4 File Offset: 0x0001ACF4
		internal string GeometryAsTextZM(DbGeometry geometryValue)
		{
			if (geometryValue == null)
			{
				return null;
			}
			object arg = this.ConvertToSqlTypesGeometry(geometryValue);
			object sqlCharsValue = this.geometryAsTextZMAsSqlChars(arg);
			return this.SqlCharsToString(sqlCharsValue);
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x0600055A RID: 1370 RVA: 0x0001CB22 File Offset: 0x0001AD22
		// (set) Token: 0x0600055B RID: 1371 RVA: 0x0001CB2A File Offset: 0x0001AD2A
		internal Type SqlGeographyType { get; private set; }

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x0600055C RID: 1372 RVA: 0x0001CB33 File Offset: 0x0001AD33
		// (set) Token: 0x0600055D RID: 1373 RVA: 0x0001CB3B File Offset: 0x0001AD3B
		internal Type SqlGeometryType { get; private set; }

		// Token: 0x0600055E RID: 1374 RVA: 0x0001CB44 File Offset: 0x0001AD44
		internal object ConvertToSqlTypesGeography(DbGeography geographyValue)
		{
			return this.GetSqlTypesSpatialValue(geographyValue.AsSpatialValue(), this.SqlGeographyType);
		}

		// Token: 0x0600055F RID: 1375 RVA: 0x0001CB58 File Offset: 0x0001AD58
		internal object SqlTypesGeographyFromBinary(byte[] wellKnownBinary, int srid)
		{
			return this.sqlGeographyFromWKBByteArray(wellKnownBinary, srid);
		}

		// Token: 0x06000560 RID: 1376 RVA: 0x0001CB67 File Offset: 0x0001AD67
		internal object SqlTypesGeographyFromText(string wellKnownText, int srid)
		{
			return this.sqlGeographyFromWKTString(wellKnownText, srid);
		}

		// Token: 0x06000561 RID: 1377 RVA: 0x0001CB76 File Offset: 0x0001AD76
		internal object ConvertToSqlTypesGeometry(DbGeometry geometryValue)
		{
			return this.GetSqlTypesSpatialValue(geometryValue.AsSpatialValue(), this.SqlGeometryType);
		}

		// Token: 0x06000562 RID: 1378 RVA: 0x0001CB8A File Offset: 0x0001AD8A
		internal object SqlTypesGeometryFromBinary(byte[] wellKnownBinary, int srid)
		{
			return this.sqlGeometryFromWKBByteArray(wellKnownBinary, srid);
		}

		// Token: 0x06000563 RID: 1379 RVA: 0x0001CB99 File Offset: 0x0001AD99
		internal object SqlTypesGeometryFromText(string wellKnownText, int srid)
		{
			return this.sqlGeometryFromWKTString(wellKnownText, srid);
		}

		// Token: 0x06000564 RID: 1380 RVA: 0x0001CBA8 File Offset: 0x0001ADA8
		private object GetSqlTypesSpatialValue(IDbSpatialValue spatialValue, Type requiredProviderValueType)
		{
			object providerValue = spatialValue.ProviderValue;
			if (providerValue != null && providerValue.GetType() == requiredProviderValueType)
			{
				return providerValue;
			}
			int? coordinateSystemId = spatialValue.CoordinateSystemId;
			if (coordinateSystemId != null)
			{
				byte[] wellKnownBinary = spatialValue.WellKnownBinary;
				if (wellKnownBinary != null)
				{
					if (!spatialValue.IsGeography)
					{
						return this.sqlGeometryFromWKBByteArray(wellKnownBinary, coordinateSystemId.Value);
					}
					return this.sqlGeographyFromWKBByteArray(wellKnownBinary, coordinateSystemId.Value);
				}
				else
				{
					string wellKnownText = spatialValue.WellKnownText;
					if (wellKnownText != null)
					{
						if (!spatialValue.IsGeography)
						{
							return this.sqlGeometryFromWKTString(wellKnownText, coordinateSystemId.Value);
						}
						return this.sqlGeographyFromWKTString(wellKnownText, coordinateSystemId.Value);
					}
					else
					{
						string gmlString = spatialValue.GmlString;
						if (gmlString != null)
						{
							XmlReader arg = SqlTypesAssembly.XmlReaderFromString(gmlString);
							if (!spatialValue.IsGeography)
							{
								return this.sqlGeometryFromGMLReader(arg, coordinateSystemId.Value);
							}
							return this.sqlGeographyFromGMLReader(arg, coordinateSystemId.Value);
						}
					}
				}
			}
			throw spatialValue.NotSqlCompatible();
		}

		// Token: 0x06000565 RID: 1381 RVA: 0x0001CCA3 File Offset: 0x0001AEA3
		[SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
		private static XmlReader XmlReaderFromString(string stringValue)
		{
			return XmlReader.Create(new StringReader(stringValue));
		}

		// Token: 0x06000566 RID: 1382 RVA: 0x0001CCB0 File Offset: 0x0001AEB0
		private static Func<TArg, int, object> CreateStaticConstructorDelegate<TArg>(Type spatialType, string methodName)
		{
			ParameterExpression parameterExpression = Expression.Parameter(typeof(TArg));
			ParameterExpression parameterExpression2 = Expression.Parameter(typeof(int));
			MethodInfo onlyDeclaredMethod = spatialType.GetOnlyDeclaredMethod(methodName);
			Expression arg = SqlTypesAssembly.BuildConvertToSqlType(parameterExpression, onlyDeclaredMethod.GetParameters()[0].ParameterType);
			Expression<Func<TArg, int, object>> expression = Expression.Lambda<Func<TArg, int, object>>(Expression.Call(null, onlyDeclaredMethod, arg, parameterExpression2), new ParameterExpression[]
			{
				parameterExpression,
				parameterExpression2
			});
			return expression.Compile();
		}

		// Token: 0x06000567 RID: 1383 RVA: 0x0001CD28 File Offset: 0x0001AF28
		private static Expression BuildConvertToSqlType(Expression toConvert, Type convertTo)
		{
			if (toConvert.Type == typeof(byte[]))
			{
				return SqlTypesAssembly.BuildConvertToSqlBytes(toConvert, convertTo);
			}
			if (toConvert.Type == typeof(string))
			{
				if (convertTo.Name == "SqlString")
				{
					return SqlTypesAssembly.BuildConvertToSqlString(toConvert, convertTo);
				}
				return SqlTypesAssembly.BuildConvertToSqlChars(toConvert, convertTo);
			}
			else
			{
				if (toConvert.Type == typeof(XmlReader))
				{
					return SqlTypesAssembly.BuildConvertToSqlXml(toConvert, convertTo);
				}
				return toConvert;
			}
		}

		// Token: 0x06000568 RID: 1384 RVA: 0x0001CDB0 File Offset: 0x0001AFB0
		private static Expression BuildConvertToSqlBytes(Expression toConvert, Type sqlBytesType)
		{
			ConstructorInfo declaredConstructor = sqlBytesType.GetDeclaredConstructor(new Type[]
			{
				toConvert.Type
			});
			return Expression.New(declaredConstructor, new Expression[]
			{
				toConvert
			});
		}

		// Token: 0x06000569 RID: 1385 RVA: 0x0001CDEC File Offset: 0x0001AFEC
		private static Expression BuildConvertToSqlChars(Expression toConvert, Type sqlCharsType)
		{
			Type type = sqlCharsType.Assembly().GetType("System.Data.SqlTypes.SqlString", true);
			ConstructorInfo declaredConstructor = sqlCharsType.GetDeclaredConstructor(new Type[]
			{
				type
			});
			ConstructorInfo declaredConstructor2 = type.GetDeclaredConstructor(new Type[]
			{
				typeof(string)
			});
			return Expression.New(declaredConstructor, new Expression[]
			{
				Expression.New(declaredConstructor2, new Expression[]
				{
					toConvert
				})
			});
		}

		// Token: 0x0600056A RID: 1386 RVA: 0x0001CE6C File Offset: 0x0001B06C
		private static Expression BuildConvertToSqlString(Expression toConvert, Type sqlStringType)
		{
			ConstructorInfo declaredConstructor = sqlStringType.GetDeclaredConstructor(new Type[]
			{
				typeof(string)
			});
			return Expression.Convert(Expression.New(declaredConstructor, new Expression[]
			{
				toConvert
			}), typeof(object));
		}

		// Token: 0x0600056B RID: 1387 RVA: 0x0001CEB8 File Offset: 0x0001B0B8
		private static Expression BuildConvertToSqlXml(Expression toConvert, Type sqlXmlType)
		{
			ConstructorInfo declaredConstructor = sqlXmlType.GetDeclaredConstructor(new Type[]
			{
				toConvert.Type
			});
			return Expression.New(declaredConstructor, new Expression[]
			{
				toConvert
			});
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x0600056C RID: 1388 RVA: 0x0001CEF1 File Offset: 0x0001B0F1
		public Lazy<MethodInfo> SmiSqlGeographyParse
		{
			get
			{
				return this._smiSqlGeographyParse;
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x0600056D RID: 1389 RVA: 0x0001CEF9 File Offset: 0x0001B0F9
		public Lazy<MethodInfo> SmiSqlGeographyStGeomFromText
		{
			get
			{
				return this._smiSqlGeographyStGeomFromText;
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x0600056E RID: 1390 RVA: 0x0001CF01 File Offset: 0x0001B101
		public Lazy<MethodInfo> SmiSqlGeographyStPointFromText
		{
			get
			{
				return this._smiSqlGeographyStPointFromText;
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x0600056F RID: 1391 RVA: 0x0001CF09 File Offset: 0x0001B109
		public Lazy<MethodInfo> SmiSqlGeographyStLineFromText
		{
			get
			{
				return this._smiSqlGeographyStLineFromText;
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000570 RID: 1392 RVA: 0x0001CF11 File Offset: 0x0001B111
		public Lazy<MethodInfo> SmiSqlGeographyStPolyFromText
		{
			get
			{
				return this._smiSqlGeographyStPolyFromText;
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000571 RID: 1393 RVA: 0x0001CF19 File Offset: 0x0001B119
		public Lazy<MethodInfo> SmiSqlGeographyStmPointFromText
		{
			get
			{
				return this._smiSqlGeographyStmPointFromText;
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000572 RID: 1394 RVA: 0x0001CF21 File Offset: 0x0001B121
		public Lazy<MethodInfo> SmiSqlGeographyStmLineFromText
		{
			get
			{
				return this._smiSqlGeographyStmLineFromText;
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000573 RID: 1395 RVA: 0x0001CF29 File Offset: 0x0001B129
		public Lazy<MethodInfo> SmiSqlGeographyStmPolyFromText
		{
			get
			{
				return this._smiSqlGeographyStmPolyFromText;
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000574 RID: 1396 RVA: 0x0001CF31 File Offset: 0x0001B131
		public Lazy<MethodInfo> SmiSqlGeographyStGeomCollFromText
		{
			get
			{
				return this._smiSqlGeographyStGeomCollFromText;
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000575 RID: 1397 RVA: 0x0001CF39 File Offset: 0x0001B139
		public Lazy<MethodInfo> SmiSqlGeographyStGeomFromWkb
		{
			get
			{
				return this._smiSqlGeographyStGeomFromWkb;
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000576 RID: 1398 RVA: 0x0001CF41 File Offset: 0x0001B141
		public Lazy<MethodInfo> SmiSqlGeographyStPointFromWkb
		{
			get
			{
				return this._smiSqlGeographyStPointFromWkb;
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000577 RID: 1399 RVA: 0x0001CF49 File Offset: 0x0001B149
		public Lazy<MethodInfo> SmiSqlGeographyStLineFromWkb
		{
			get
			{
				return this._smiSqlGeographyStLineFromWkb;
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000578 RID: 1400 RVA: 0x0001CF51 File Offset: 0x0001B151
		public Lazy<MethodInfo> SmiSqlGeographyStPolyFromWkb
		{
			get
			{
				return this._smiSqlGeographyStPolyFromWkb;
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000579 RID: 1401 RVA: 0x0001CF59 File Offset: 0x0001B159
		public Lazy<MethodInfo> SmiSqlGeographyStmPointFromWkb
		{
			get
			{
				return this._smiSqlGeographyStmPointFromWkb;
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x0600057A RID: 1402 RVA: 0x0001CF61 File Offset: 0x0001B161
		public Lazy<MethodInfo> SmiSqlGeographyStmLineFromWkb
		{
			get
			{
				return this._smiSqlGeographyStmLineFromWkb;
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x0600057B RID: 1403 RVA: 0x0001CF69 File Offset: 0x0001B169
		public Lazy<MethodInfo> SmiSqlGeographyStmPolyFromWkb
		{
			get
			{
				return this._smiSqlGeographyStmPolyFromWkb;
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x0600057C RID: 1404 RVA: 0x0001CF71 File Offset: 0x0001B171
		public Lazy<MethodInfo> SmiSqlGeographyStGeomCollFromWkb
		{
			get
			{
				return this._smiSqlGeographyStGeomCollFromWkb;
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x0600057D RID: 1405 RVA: 0x0001CF79 File Offset: 0x0001B179
		public Lazy<MethodInfo> SmiSqlGeographyGeomFromGml
		{
			get
			{
				return this._smiSqlGeographyGeomFromGml;
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x0600057E RID: 1406 RVA: 0x0001CF81 File Offset: 0x0001B181
		public Lazy<PropertyInfo> IpiSqlGeographyStSrid
		{
			get
			{
				return this._ipiSqlGeographyStSrid;
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x0600057F RID: 1407 RVA: 0x0001CF89 File Offset: 0x0001B189
		public Lazy<MethodInfo> ImiSqlGeographyStGeometryType
		{
			get
			{
				return this._imiSqlGeographyStGeometryType;
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000580 RID: 1408 RVA: 0x0001CF91 File Offset: 0x0001B191
		public Lazy<MethodInfo> ImiSqlGeographyStDimension
		{
			get
			{
				return this._imiSqlGeographyStDimension;
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x06000581 RID: 1409 RVA: 0x0001CF99 File Offset: 0x0001B199
		public Lazy<MethodInfo> ImiSqlGeographyStAsBinary
		{
			get
			{
				return this._imiSqlGeographyStAsBinary;
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000582 RID: 1410 RVA: 0x0001CFA1 File Offset: 0x0001B1A1
		public Lazy<MethodInfo> ImiSqlGeographyAsGml
		{
			get
			{
				return this._imiSqlGeographyAsGml;
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x06000583 RID: 1411 RVA: 0x0001CFA9 File Offset: 0x0001B1A9
		public Lazy<MethodInfo> ImiSqlGeographyStAsText
		{
			get
			{
				return this._imiSqlGeographyStAsText;
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000584 RID: 1412 RVA: 0x0001CFB1 File Offset: 0x0001B1B1
		public Lazy<MethodInfo> ImiSqlGeographyStIsEmpty
		{
			get
			{
				return this._imiSqlGeographyStIsEmpty;
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000585 RID: 1413 RVA: 0x0001CFB9 File Offset: 0x0001B1B9
		public Lazy<MethodInfo> ImiSqlGeographyStEquals
		{
			get
			{
				return this._imiSqlGeographyStEquals;
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000586 RID: 1414 RVA: 0x0001CFC1 File Offset: 0x0001B1C1
		public Lazy<MethodInfo> ImiSqlGeographyStDisjoint
		{
			get
			{
				return this._imiSqlGeographyStDisjoint;
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000587 RID: 1415 RVA: 0x0001CFC9 File Offset: 0x0001B1C9
		public Lazy<MethodInfo> ImiSqlGeographyStIntersects
		{
			get
			{
				return this._imiSqlGeographyStIntersects;
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000588 RID: 1416 RVA: 0x0001CFD1 File Offset: 0x0001B1D1
		public Lazy<MethodInfo> ImiSqlGeographyStBuffer
		{
			get
			{
				return this._imiSqlGeographyStBuffer;
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x06000589 RID: 1417 RVA: 0x0001CFD9 File Offset: 0x0001B1D9
		public Lazy<MethodInfo> ImiSqlGeographyStDistance
		{
			get
			{
				return this._imiSqlGeographyStDistance;
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x0600058A RID: 1418 RVA: 0x0001CFE1 File Offset: 0x0001B1E1
		public Lazy<MethodInfo> ImiSqlGeographyStIntersection
		{
			get
			{
				return this._imiSqlGeographyStIntersection;
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x0600058B RID: 1419 RVA: 0x0001CFE9 File Offset: 0x0001B1E9
		public Lazy<MethodInfo> ImiSqlGeographyStUnion
		{
			get
			{
				return this._imiSqlGeographyStUnion;
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x0600058C RID: 1420 RVA: 0x0001CFF1 File Offset: 0x0001B1F1
		public Lazy<MethodInfo> ImiSqlGeographyStDifference
		{
			get
			{
				return this._imiSqlGeographyStDifference;
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x0600058D RID: 1421 RVA: 0x0001CFF9 File Offset: 0x0001B1F9
		public Lazy<MethodInfo> ImiSqlGeographyStSymDifference
		{
			get
			{
				return this._imiSqlGeographyStSymDifference;
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x0600058E RID: 1422 RVA: 0x0001D001 File Offset: 0x0001B201
		public Lazy<MethodInfo> ImiSqlGeographyStNumGeometries
		{
			get
			{
				return this._imiSqlGeographyStNumGeometries;
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x0600058F RID: 1423 RVA: 0x0001D009 File Offset: 0x0001B209
		public Lazy<MethodInfo> ImiSqlGeographyStGeometryN
		{
			get
			{
				return this._imiSqlGeographyStGeometryN;
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x06000590 RID: 1424 RVA: 0x0001D011 File Offset: 0x0001B211
		public Lazy<PropertyInfo> IpiSqlGeographyLat
		{
			get
			{
				return this._ipiSqlGeographyLat;
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x06000591 RID: 1425 RVA: 0x0001D019 File Offset: 0x0001B219
		public Lazy<PropertyInfo> IpiSqlGeographyLong
		{
			get
			{
				return this._ipiSqlGeographyLong;
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000592 RID: 1426 RVA: 0x0001D021 File Offset: 0x0001B221
		public Lazy<PropertyInfo> IpiSqlGeographyZ
		{
			get
			{
				return this._ipiSqlGeographyZ;
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06000593 RID: 1427 RVA: 0x0001D029 File Offset: 0x0001B229
		public Lazy<PropertyInfo> IpiSqlGeographyM
		{
			get
			{
				return this._ipiSqlGeographyM;
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000594 RID: 1428 RVA: 0x0001D031 File Offset: 0x0001B231
		public Lazy<MethodInfo> ImiSqlGeographyStLength
		{
			get
			{
				return this._imiSqlGeographyStLength;
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x06000595 RID: 1429 RVA: 0x0001D039 File Offset: 0x0001B239
		public Lazy<MethodInfo> ImiSqlGeographyStStartPoint
		{
			get
			{
				return this._imiSqlGeographyStStartPoint;
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x06000596 RID: 1430 RVA: 0x0001D041 File Offset: 0x0001B241
		public Lazy<MethodInfo> ImiSqlGeographyStEndPoint
		{
			get
			{
				return this._imiSqlGeographyStEndPoint;
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x06000597 RID: 1431 RVA: 0x0001D049 File Offset: 0x0001B249
		public Lazy<MethodInfo> ImiSqlGeographyStIsClosed
		{
			get
			{
				return this._imiSqlGeographyStIsClosed;
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x06000598 RID: 1432 RVA: 0x0001D051 File Offset: 0x0001B251
		public Lazy<MethodInfo> ImiSqlGeographyStNumPoints
		{
			get
			{
				return this._imiSqlGeographyStNumPoints;
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x06000599 RID: 1433 RVA: 0x0001D059 File Offset: 0x0001B259
		public Lazy<MethodInfo> ImiSqlGeographyStPointN
		{
			get
			{
				return this._imiSqlGeographyStPointN;
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x0600059A RID: 1434 RVA: 0x0001D061 File Offset: 0x0001B261
		public Lazy<MethodInfo> ImiSqlGeographyStArea
		{
			get
			{
				return this._imiSqlGeographyStArea;
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x0600059B RID: 1435 RVA: 0x0001D069 File Offset: 0x0001B269
		public Lazy<MethodInfo> SmiSqlGeometryParse
		{
			get
			{
				return this._smiSqlGeometryParse;
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x0600059C RID: 1436 RVA: 0x0001D071 File Offset: 0x0001B271
		public Lazy<MethodInfo> SmiSqlGeometryStGeomFromText
		{
			get
			{
				return this._smiSqlGeometryStGeomFromText;
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x0600059D RID: 1437 RVA: 0x0001D079 File Offset: 0x0001B279
		public Lazy<MethodInfo> SmiSqlGeometryStPointFromText
		{
			get
			{
				return this._smiSqlGeometryStPointFromText;
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x0600059E RID: 1438 RVA: 0x0001D081 File Offset: 0x0001B281
		public Lazy<MethodInfo> SmiSqlGeometryStLineFromText
		{
			get
			{
				return this._smiSqlGeometryStLineFromText;
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x0600059F RID: 1439 RVA: 0x0001D089 File Offset: 0x0001B289
		public Lazy<MethodInfo> SmiSqlGeometryStPolyFromText
		{
			get
			{
				return this._smiSqlGeometryStPolyFromText;
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x060005A0 RID: 1440 RVA: 0x0001D091 File Offset: 0x0001B291
		public Lazy<MethodInfo> SmiSqlGeometryStmPointFromText
		{
			get
			{
				return this._smiSqlGeometryStmPointFromText;
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x060005A1 RID: 1441 RVA: 0x0001D099 File Offset: 0x0001B299
		public Lazy<MethodInfo> SmiSqlGeometryStmLineFromText
		{
			get
			{
				return this._smiSqlGeometryStmLineFromText;
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x060005A2 RID: 1442 RVA: 0x0001D0A1 File Offset: 0x0001B2A1
		public Lazy<MethodInfo> SmiSqlGeometryStmPolyFromText
		{
			get
			{
				return this._smiSqlGeometryStmPolyFromText;
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x060005A3 RID: 1443 RVA: 0x0001D0A9 File Offset: 0x0001B2A9
		public Lazy<MethodInfo> SmiSqlGeometryStGeomCollFromText
		{
			get
			{
				return this._smiSqlGeometryStGeomCollFromText;
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x060005A4 RID: 1444 RVA: 0x0001D0B1 File Offset: 0x0001B2B1
		public Lazy<MethodInfo> SmiSqlGeometryStGeomFromWkb
		{
			get
			{
				return this._smiSqlGeometryStGeomFromWkb;
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x060005A5 RID: 1445 RVA: 0x0001D0B9 File Offset: 0x0001B2B9
		public Lazy<MethodInfo> SmiSqlGeometryStPointFromWkb
		{
			get
			{
				return this._smiSqlGeometryStPointFromWkb;
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x060005A6 RID: 1446 RVA: 0x0001D0C1 File Offset: 0x0001B2C1
		public Lazy<MethodInfo> SmiSqlGeometryStLineFromWkb
		{
			get
			{
				return this._smiSqlGeometryStLineFromWkb;
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x060005A7 RID: 1447 RVA: 0x0001D0C9 File Offset: 0x0001B2C9
		public Lazy<MethodInfo> SmiSqlGeometryStPolyFromWkb
		{
			get
			{
				return this._smiSqlGeometryStPolyFromWkb;
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x060005A8 RID: 1448 RVA: 0x0001D0D1 File Offset: 0x0001B2D1
		public Lazy<MethodInfo> SmiSqlGeometryStmPointFromWkb
		{
			get
			{
				return this._smiSqlGeometryStmPointFromWkb;
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x060005A9 RID: 1449 RVA: 0x0001D0D9 File Offset: 0x0001B2D9
		public Lazy<MethodInfo> SmiSqlGeometryStmLineFromWkb
		{
			get
			{
				return this._smiSqlGeometryStmLineFromWkb;
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x060005AA RID: 1450 RVA: 0x0001D0E1 File Offset: 0x0001B2E1
		public Lazy<MethodInfo> SmiSqlGeometryStmPolyFromWkb
		{
			get
			{
				return this._smiSqlGeometryStmPolyFromWkb;
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x060005AB RID: 1451 RVA: 0x0001D0E9 File Offset: 0x0001B2E9
		public Lazy<MethodInfo> SmiSqlGeometryStGeomCollFromWkb
		{
			get
			{
				return this._smiSqlGeometryStGeomCollFromWkb;
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x060005AC RID: 1452 RVA: 0x0001D0F1 File Offset: 0x0001B2F1
		public Lazy<MethodInfo> SmiSqlGeometryGeomFromGml
		{
			get
			{
				return this._smiSqlGeometryGeomFromGml;
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x060005AD RID: 1453 RVA: 0x0001D0F9 File Offset: 0x0001B2F9
		public Lazy<PropertyInfo> IpiSqlGeometryStSrid
		{
			get
			{
				return this._ipiSqlGeometryStSrid;
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x060005AE RID: 1454 RVA: 0x0001D101 File Offset: 0x0001B301
		public Lazy<MethodInfo> ImiSqlGeometryStGeometryType
		{
			get
			{
				return this._imiSqlGeometryStGeometryType;
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x060005AF RID: 1455 RVA: 0x0001D109 File Offset: 0x0001B309
		public Lazy<MethodInfo> ImiSqlGeometryStDimension
		{
			get
			{
				return this._imiSqlGeometryStDimension;
			}
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x060005B0 RID: 1456 RVA: 0x0001D111 File Offset: 0x0001B311
		public Lazy<MethodInfo> ImiSqlGeometryStEnvelope
		{
			get
			{
				return this._imiSqlGeometryStEnvelope;
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x060005B1 RID: 1457 RVA: 0x0001D119 File Offset: 0x0001B319
		public Lazy<MethodInfo> ImiSqlGeometryStAsBinary
		{
			get
			{
				return this._imiSqlGeometryStAsBinary;
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x060005B2 RID: 1458 RVA: 0x0001D121 File Offset: 0x0001B321
		public Lazy<MethodInfo> ImiSqlGeometryAsGml
		{
			get
			{
				return this._imiSqlGeometryAsGml;
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x060005B3 RID: 1459 RVA: 0x0001D129 File Offset: 0x0001B329
		public Lazy<MethodInfo> ImiSqlGeometryStAsText
		{
			get
			{
				return this._imiSqlGeometryStAsText;
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x060005B4 RID: 1460 RVA: 0x0001D131 File Offset: 0x0001B331
		public Lazy<MethodInfo> ImiSqlGeometryStIsEmpty
		{
			get
			{
				return this._imiSqlGeometryStIsEmpty;
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x060005B5 RID: 1461 RVA: 0x0001D139 File Offset: 0x0001B339
		public Lazy<MethodInfo> ImiSqlGeometryStIsSimple
		{
			get
			{
				return this._imiSqlGeometryStIsSimple;
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x060005B6 RID: 1462 RVA: 0x0001D141 File Offset: 0x0001B341
		public Lazy<MethodInfo> ImiSqlGeometryStBoundary
		{
			get
			{
				return this._imiSqlGeometryStBoundary;
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x060005B7 RID: 1463 RVA: 0x0001D149 File Offset: 0x0001B349
		public Lazy<MethodInfo> ImiSqlGeometryStIsValid
		{
			get
			{
				return this._imiSqlGeometryStIsValid;
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x060005B8 RID: 1464 RVA: 0x0001D151 File Offset: 0x0001B351
		public Lazy<MethodInfo> ImiSqlGeometryStEquals
		{
			get
			{
				return this._imiSqlGeometryStEquals;
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x060005B9 RID: 1465 RVA: 0x0001D159 File Offset: 0x0001B359
		public Lazy<MethodInfo> ImiSqlGeometryStDisjoint
		{
			get
			{
				return this._imiSqlGeometryStDisjoint;
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x060005BA RID: 1466 RVA: 0x0001D161 File Offset: 0x0001B361
		public Lazy<MethodInfo> ImiSqlGeometryStIntersects
		{
			get
			{
				return this._imiSqlGeometryStIntersects;
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x060005BB RID: 1467 RVA: 0x0001D169 File Offset: 0x0001B369
		public Lazy<MethodInfo> ImiSqlGeometryStTouches
		{
			get
			{
				return this._imiSqlGeometryStTouches;
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x060005BC RID: 1468 RVA: 0x0001D171 File Offset: 0x0001B371
		public Lazy<MethodInfo> ImiSqlGeometryStCrosses
		{
			get
			{
				return this._imiSqlGeometryStCrosses;
			}
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x060005BD RID: 1469 RVA: 0x0001D179 File Offset: 0x0001B379
		public Lazy<MethodInfo> ImiSqlGeometryStWithin
		{
			get
			{
				return this._imiSqlGeometryStWithin;
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x060005BE RID: 1470 RVA: 0x0001D181 File Offset: 0x0001B381
		public Lazy<MethodInfo> ImiSqlGeometryStContains
		{
			get
			{
				return this._imiSqlGeometryStContains;
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x060005BF RID: 1471 RVA: 0x0001D189 File Offset: 0x0001B389
		public Lazy<MethodInfo> ImiSqlGeometryStOverlaps
		{
			get
			{
				return this._imiSqlGeometryStOverlaps;
			}
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x060005C0 RID: 1472 RVA: 0x0001D191 File Offset: 0x0001B391
		public Lazy<MethodInfo> ImiSqlGeometryStRelate
		{
			get
			{
				return this._imiSqlGeometryStRelate;
			}
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x060005C1 RID: 1473 RVA: 0x0001D199 File Offset: 0x0001B399
		public Lazy<MethodInfo> ImiSqlGeometryStBuffer
		{
			get
			{
				return this._imiSqlGeometryStBuffer;
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x060005C2 RID: 1474 RVA: 0x0001D1A1 File Offset: 0x0001B3A1
		public Lazy<MethodInfo> ImiSqlGeometryStDistance
		{
			get
			{
				return this._imiSqlGeometryStDistance;
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x060005C3 RID: 1475 RVA: 0x0001D1A9 File Offset: 0x0001B3A9
		public Lazy<MethodInfo> ImiSqlGeometryStConvexHull
		{
			get
			{
				return this._imiSqlGeometryStConvexHull;
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x060005C4 RID: 1476 RVA: 0x0001D1B1 File Offset: 0x0001B3B1
		public Lazy<MethodInfo> ImiSqlGeometryStIntersection
		{
			get
			{
				return this._imiSqlGeometryStIntersection;
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x060005C5 RID: 1477 RVA: 0x0001D1B9 File Offset: 0x0001B3B9
		public Lazy<MethodInfo> ImiSqlGeometryStUnion
		{
			get
			{
				return this._imiSqlGeometryStUnion;
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x060005C6 RID: 1478 RVA: 0x0001D1C1 File Offset: 0x0001B3C1
		public Lazy<MethodInfo> ImiSqlGeometryStDifference
		{
			get
			{
				return this._imiSqlGeometryStDifference;
			}
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x060005C7 RID: 1479 RVA: 0x0001D1C9 File Offset: 0x0001B3C9
		public Lazy<MethodInfo> ImiSqlGeometryStSymDifference
		{
			get
			{
				return this._imiSqlGeometryStSymDifference;
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x060005C8 RID: 1480 RVA: 0x0001D1D1 File Offset: 0x0001B3D1
		public Lazy<MethodInfo> ImiSqlGeometryStNumGeometries
		{
			get
			{
				return this._imiSqlGeometryStNumGeometries;
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x060005C9 RID: 1481 RVA: 0x0001D1D9 File Offset: 0x0001B3D9
		public Lazy<MethodInfo> ImiSqlGeometryStGeometryN
		{
			get
			{
				return this._imiSqlGeometryStGeometryN;
			}
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x060005CA RID: 1482 RVA: 0x0001D1E1 File Offset: 0x0001B3E1
		public Lazy<PropertyInfo> IpiSqlGeometryStx
		{
			get
			{
				return this._ipiSqlGeometryStx;
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x060005CB RID: 1483 RVA: 0x0001D1E9 File Offset: 0x0001B3E9
		public Lazy<PropertyInfo> IpiSqlGeometrySty
		{
			get
			{
				return this._ipiSqlGeometrySty;
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x060005CC RID: 1484 RVA: 0x0001D1F1 File Offset: 0x0001B3F1
		public Lazy<PropertyInfo> IpiSqlGeometryZ
		{
			get
			{
				return this._ipiSqlGeometryZ;
			}
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x060005CD RID: 1485 RVA: 0x0001D1F9 File Offset: 0x0001B3F9
		public Lazy<PropertyInfo> IpiSqlGeometryM
		{
			get
			{
				return this._ipiSqlGeometryM;
			}
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x060005CE RID: 1486 RVA: 0x0001D201 File Offset: 0x0001B401
		public Lazy<MethodInfo> ImiSqlGeometryStLength
		{
			get
			{
				return this._imiSqlGeometryStLength;
			}
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x060005CF RID: 1487 RVA: 0x0001D209 File Offset: 0x0001B409
		public Lazy<MethodInfo> ImiSqlGeometryStStartPoint
		{
			get
			{
				return this._imiSqlGeometryStStartPoint;
			}
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x060005D0 RID: 1488 RVA: 0x0001D211 File Offset: 0x0001B411
		public Lazy<MethodInfo> ImiSqlGeometryStEndPoint
		{
			get
			{
				return this._imiSqlGeometryStEndPoint;
			}
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x060005D1 RID: 1489 RVA: 0x0001D219 File Offset: 0x0001B419
		public Lazy<MethodInfo> ImiSqlGeometryStIsClosed
		{
			get
			{
				return this._imiSqlGeometryStIsClosed;
			}
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x060005D2 RID: 1490 RVA: 0x0001D221 File Offset: 0x0001B421
		public Lazy<MethodInfo> ImiSqlGeometryStIsRing
		{
			get
			{
				return this._imiSqlGeometryStIsRing;
			}
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x060005D3 RID: 1491 RVA: 0x0001D229 File Offset: 0x0001B429
		public Lazy<MethodInfo> ImiSqlGeometryStNumPoints
		{
			get
			{
				return this._imiSqlGeometryStNumPoints;
			}
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x060005D4 RID: 1492 RVA: 0x0001D231 File Offset: 0x0001B431
		public Lazy<MethodInfo> ImiSqlGeometryStPointN
		{
			get
			{
				return this._imiSqlGeometryStPointN;
			}
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x060005D5 RID: 1493 RVA: 0x0001D239 File Offset: 0x0001B439
		public Lazy<MethodInfo> ImiSqlGeometryStArea
		{
			get
			{
				return this._imiSqlGeometryStArea;
			}
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x060005D6 RID: 1494 RVA: 0x0001D241 File Offset: 0x0001B441
		public Lazy<MethodInfo> ImiSqlGeometryStCentroid
		{
			get
			{
				return this._imiSqlGeometryStCentroid;
			}
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x060005D7 RID: 1495 RVA: 0x0001D249 File Offset: 0x0001B449
		public Lazy<MethodInfo> ImiSqlGeometryStPointOnSurface
		{
			get
			{
				return this._imiSqlGeometryStPointOnSurface;
			}
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x060005D8 RID: 1496 RVA: 0x0001D251 File Offset: 0x0001B451
		public Lazy<MethodInfo> ImiSqlGeometryStExteriorRing
		{
			get
			{
				return this._imiSqlGeometryStExteriorRing;
			}
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x060005D9 RID: 1497 RVA: 0x0001D259 File Offset: 0x0001B459
		public Lazy<MethodInfo> ImiSqlGeometryStNumInteriorRing
		{
			get
			{
				return this._imiSqlGeometryStNumInteriorRing;
			}
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x060005DA RID: 1498 RVA: 0x0001D261 File Offset: 0x0001B461
		public Lazy<MethodInfo> ImiSqlGeometryStInteriorRingN
		{
			get
			{
				return this._imiSqlGeometryStInteriorRingN;
			}
		}

		// Token: 0x060005DB RID: 1499 RVA: 0x0001D269 File Offset: 0x0001B469
		private MethodInfo FindSqlGeographyMethod(string methodName, params Type[] argTypes)
		{
			return this.SqlGeographyType.GetDeclaredMethod(methodName, argTypes);
		}

		// Token: 0x060005DC RID: 1500 RVA: 0x0001D278 File Offset: 0x0001B478
		private MethodInfo FindSqlGeographyStaticMethod(string methodName, params Type[] argTypes)
		{
			return this.SqlGeographyType.GetDeclaredMethod(methodName, argTypes);
		}

		// Token: 0x060005DD RID: 1501 RVA: 0x0001D287 File Offset: 0x0001B487
		private PropertyInfo FindSqlGeographyProperty(string propertyName)
		{
			return this.SqlGeographyType.GetRuntimeProperty(propertyName);
		}

		// Token: 0x060005DE RID: 1502 RVA: 0x0001D295 File Offset: 0x0001B495
		private MethodInfo FindSqlGeometryStaticMethod(string methodName, params Type[] argTypes)
		{
			return this.SqlGeometryType.GetDeclaredMethod(methodName, argTypes);
		}

		// Token: 0x060005DF RID: 1503 RVA: 0x0001D2A4 File Offset: 0x0001B4A4
		private MethodInfo FindSqlGeometryMethod(string methodName, params Type[] argTypes)
		{
			return this.SqlGeometryType.GetDeclaredMethod(methodName, argTypes);
		}

		// Token: 0x060005E0 RID: 1504 RVA: 0x0001D2B3 File Offset: 0x0001B4B3
		private PropertyInfo FindSqlGeometryProperty(string propertyName)
		{
			return this.SqlGeometryType.GetRuntimeProperty(propertyName);
		}

		// Token: 0x04000128 RID: 296
		private readonly Func<object, bool> sqlBooleanToBoolean;

		// Token: 0x04000129 RID: 297
		private readonly Func<object, bool?> sqlBooleanToNullableBoolean;

		// Token: 0x0400012A RID: 298
		private readonly Func<byte[], object> sqlBytesFromByteArray;

		// Token: 0x0400012B RID: 299
		private readonly Func<object, byte[]> sqlBytesToByteArray;

		// Token: 0x0400012C RID: 300
		private readonly Func<string, object> sqlStringFromString;

		// Token: 0x0400012D RID: 301
		private readonly Func<string, object> sqlCharsFromString;

		// Token: 0x0400012E RID: 302
		private readonly Func<object, string> sqlCharsToString;

		// Token: 0x0400012F RID: 303
		private readonly Func<object, string> sqlStringToString;

		// Token: 0x04000130 RID: 304
		private readonly Func<object, double> sqlDoubleToDouble;

		// Token: 0x04000131 RID: 305
		private readonly Func<object, double?> sqlDoubleToNullableDouble;

		// Token: 0x04000132 RID: 306
		private readonly Func<object, int> sqlInt32ToInt;

		// Token: 0x04000133 RID: 307
		private readonly Func<object, int?> sqlInt32ToNullableInt;

		// Token: 0x04000134 RID: 308
		private readonly Func<XmlReader, object> sqlXmlFromXmlReader;

		// Token: 0x04000135 RID: 309
		private readonly Func<object, string> sqlXmlToString;

		// Token: 0x04000136 RID: 310
		private readonly Func<object, bool> isSqlGeographyNull;

		// Token: 0x04000137 RID: 311
		private readonly Func<object, bool> isSqlGeometryNull;

		// Token: 0x04000138 RID: 312
		private readonly Func<object, object> geographyAsTextZMAsSqlChars;

		// Token: 0x04000139 RID: 313
		private readonly Func<object, object> geometryAsTextZMAsSqlChars;

		// Token: 0x0400013A RID: 314
		private readonly Func<string, int, object> sqlGeographyFromWKTString;

		// Token: 0x0400013B RID: 315
		private readonly Func<byte[], int, object> sqlGeographyFromWKBByteArray;

		// Token: 0x0400013C RID: 316
		private readonly Func<XmlReader, int, object> sqlGeographyFromGMLReader;

		// Token: 0x0400013D RID: 317
		private readonly Func<string, int, object> sqlGeometryFromWKTString;

		// Token: 0x0400013E RID: 318
		private readonly Func<byte[], int, object> sqlGeometryFromWKBByteArray;

		// Token: 0x0400013F RID: 319
		private readonly Func<XmlReader, int, object> sqlGeometryFromGMLReader;

		// Token: 0x04000140 RID: 320
		private readonly Lazy<MethodInfo> _smiSqlGeographyParse;

		// Token: 0x04000141 RID: 321
		private readonly Lazy<MethodInfo> _smiSqlGeographyStGeomFromText;

		// Token: 0x04000142 RID: 322
		private readonly Lazy<MethodInfo> _smiSqlGeographyStPointFromText;

		// Token: 0x04000143 RID: 323
		private readonly Lazy<MethodInfo> _smiSqlGeographyStLineFromText;

		// Token: 0x04000144 RID: 324
		private readonly Lazy<MethodInfo> _smiSqlGeographyStPolyFromText;

		// Token: 0x04000145 RID: 325
		private readonly Lazy<MethodInfo> _smiSqlGeographyStmPointFromText;

		// Token: 0x04000146 RID: 326
		private readonly Lazy<MethodInfo> _smiSqlGeographyStmLineFromText;

		// Token: 0x04000147 RID: 327
		private readonly Lazy<MethodInfo> _smiSqlGeographyStmPolyFromText;

		// Token: 0x04000148 RID: 328
		private readonly Lazy<MethodInfo> _smiSqlGeographyStGeomCollFromText;

		// Token: 0x04000149 RID: 329
		private readonly Lazy<MethodInfo> _smiSqlGeographyStGeomFromWkb;

		// Token: 0x0400014A RID: 330
		private readonly Lazy<MethodInfo> _smiSqlGeographyStPointFromWkb;

		// Token: 0x0400014B RID: 331
		private readonly Lazy<MethodInfo> _smiSqlGeographyStLineFromWkb;

		// Token: 0x0400014C RID: 332
		private readonly Lazy<MethodInfo> _smiSqlGeographyStPolyFromWkb;

		// Token: 0x0400014D RID: 333
		private readonly Lazy<MethodInfo> _smiSqlGeographyStmPointFromWkb;

		// Token: 0x0400014E RID: 334
		private readonly Lazy<MethodInfo> _smiSqlGeographyStmLineFromWkb;

		// Token: 0x0400014F RID: 335
		private readonly Lazy<MethodInfo> _smiSqlGeographyStmPolyFromWkb;

		// Token: 0x04000150 RID: 336
		private readonly Lazy<MethodInfo> _smiSqlGeographyStGeomCollFromWkb;

		// Token: 0x04000151 RID: 337
		private readonly Lazy<MethodInfo> _smiSqlGeographyGeomFromGml;

		// Token: 0x04000152 RID: 338
		private readonly Lazy<PropertyInfo> _ipiSqlGeographyStSrid;

		// Token: 0x04000153 RID: 339
		private readonly Lazy<MethodInfo> _imiSqlGeographyStGeometryType;

		// Token: 0x04000154 RID: 340
		private readonly Lazy<MethodInfo> _imiSqlGeographyStDimension;

		// Token: 0x04000155 RID: 341
		private readonly Lazy<MethodInfo> _imiSqlGeographyStAsBinary;

		// Token: 0x04000156 RID: 342
		private readonly Lazy<MethodInfo> _imiSqlGeographyAsGml;

		// Token: 0x04000157 RID: 343
		private readonly Lazy<MethodInfo> _imiSqlGeographyStAsText;

		// Token: 0x04000158 RID: 344
		private readonly Lazy<MethodInfo> _imiSqlGeographyStIsEmpty;

		// Token: 0x04000159 RID: 345
		private readonly Lazy<MethodInfo> _imiSqlGeographyStEquals;

		// Token: 0x0400015A RID: 346
		private readonly Lazy<MethodInfo> _imiSqlGeographyStDisjoint;

		// Token: 0x0400015B RID: 347
		private readonly Lazy<MethodInfo> _imiSqlGeographyStIntersects;

		// Token: 0x0400015C RID: 348
		private readonly Lazy<MethodInfo> _imiSqlGeographyStBuffer;

		// Token: 0x0400015D RID: 349
		private readonly Lazy<MethodInfo> _imiSqlGeographyStDistance;

		// Token: 0x0400015E RID: 350
		private readonly Lazy<MethodInfo> _imiSqlGeographyStIntersection;

		// Token: 0x0400015F RID: 351
		private readonly Lazy<MethodInfo> _imiSqlGeographyStUnion;

		// Token: 0x04000160 RID: 352
		private readonly Lazy<MethodInfo> _imiSqlGeographyStDifference;

		// Token: 0x04000161 RID: 353
		private readonly Lazy<MethodInfo> _imiSqlGeographyStSymDifference;

		// Token: 0x04000162 RID: 354
		private readonly Lazy<MethodInfo> _imiSqlGeographyStNumGeometries;

		// Token: 0x04000163 RID: 355
		private readonly Lazy<MethodInfo> _imiSqlGeographyStGeometryN;

		// Token: 0x04000164 RID: 356
		private readonly Lazy<PropertyInfo> _ipiSqlGeographyLat;

		// Token: 0x04000165 RID: 357
		private readonly Lazy<PropertyInfo> _ipiSqlGeographyLong;

		// Token: 0x04000166 RID: 358
		private readonly Lazy<PropertyInfo> _ipiSqlGeographyZ;

		// Token: 0x04000167 RID: 359
		private readonly Lazy<PropertyInfo> _ipiSqlGeographyM;

		// Token: 0x04000168 RID: 360
		private readonly Lazy<MethodInfo> _imiSqlGeographyStLength;

		// Token: 0x04000169 RID: 361
		private readonly Lazy<MethodInfo> _imiSqlGeographyStStartPoint;

		// Token: 0x0400016A RID: 362
		private readonly Lazy<MethodInfo> _imiSqlGeographyStEndPoint;

		// Token: 0x0400016B RID: 363
		private readonly Lazy<MethodInfo> _imiSqlGeographyStIsClosed;

		// Token: 0x0400016C RID: 364
		private readonly Lazy<MethodInfo> _imiSqlGeographyStNumPoints;

		// Token: 0x0400016D RID: 365
		private readonly Lazy<MethodInfo> _imiSqlGeographyStPointN;

		// Token: 0x0400016E RID: 366
		private readonly Lazy<MethodInfo> _imiSqlGeographyStArea;

		// Token: 0x0400016F RID: 367
		private readonly Lazy<MethodInfo> _smiSqlGeometryParse;

		// Token: 0x04000170 RID: 368
		private readonly Lazy<MethodInfo> _smiSqlGeometryStGeomFromText;

		// Token: 0x04000171 RID: 369
		private readonly Lazy<MethodInfo> _smiSqlGeometryStPointFromText;

		// Token: 0x04000172 RID: 370
		private readonly Lazy<MethodInfo> _smiSqlGeometryStLineFromText;

		// Token: 0x04000173 RID: 371
		private readonly Lazy<MethodInfo> _smiSqlGeometryStPolyFromText;

		// Token: 0x04000174 RID: 372
		private readonly Lazy<MethodInfo> _smiSqlGeometryStmPointFromText;

		// Token: 0x04000175 RID: 373
		private readonly Lazy<MethodInfo> _smiSqlGeometryStmLineFromText;

		// Token: 0x04000176 RID: 374
		private readonly Lazy<MethodInfo> _smiSqlGeometryStmPolyFromText;

		// Token: 0x04000177 RID: 375
		private readonly Lazy<MethodInfo> _smiSqlGeometryStGeomCollFromText;

		// Token: 0x04000178 RID: 376
		private readonly Lazy<MethodInfo> _smiSqlGeometryStGeomFromWkb;

		// Token: 0x04000179 RID: 377
		private readonly Lazy<MethodInfo> _smiSqlGeometryStPointFromWkb;

		// Token: 0x0400017A RID: 378
		private readonly Lazy<MethodInfo> _smiSqlGeometryStLineFromWkb;

		// Token: 0x0400017B RID: 379
		private readonly Lazy<MethodInfo> _smiSqlGeometryStPolyFromWkb;

		// Token: 0x0400017C RID: 380
		private readonly Lazy<MethodInfo> _smiSqlGeometryStmPointFromWkb;

		// Token: 0x0400017D RID: 381
		private readonly Lazy<MethodInfo> _smiSqlGeometryStmLineFromWkb;

		// Token: 0x0400017E RID: 382
		private readonly Lazy<MethodInfo> _smiSqlGeometryStmPolyFromWkb;

		// Token: 0x0400017F RID: 383
		private readonly Lazy<MethodInfo> _smiSqlGeometryStGeomCollFromWkb;

		// Token: 0x04000180 RID: 384
		private readonly Lazy<MethodInfo> _smiSqlGeometryGeomFromGml;

		// Token: 0x04000181 RID: 385
		private readonly Lazy<PropertyInfo> _ipiSqlGeometryStSrid;

		// Token: 0x04000182 RID: 386
		private readonly Lazy<MethodInfo> _imiSqlGeometryStGeometryType;

		// Token: 0x04000183 RID: 387
		private readonly Lazy<MethodInfo> _imiSqlGeometryStDimension;

		// Token: 0x04000184 RID: 388
		private readonly Lazy<MethodInfo> _imiSqlGeometryStEnvelope;

		// Token: 0x04000185 RID: 389
		private readonly Lazy<MethodInfo> _imiSqlGeometryStAsBinary;

		// Token: 0x04000186 RID: 390
		private readonly Lazy<MethodInfo> _imiSqlGeometryAsGml;

		// Token: 0x04000187 RID: 391
		private readonly Lazy<MethodInfo> _imiSqlGeometryStAsText;

		// Token: 0x04000188 RID: 392
		private readonly Lazy<MethodInfo> _imiSqlGeometryStIsEmpty;

		// Token: 0x04000189 RID: 393
		private readonly Lazy<MethodInfo> _imiSqlGeometryStIsSimple;

		// Token: 0x0400018A RID: 394
		private readonly Lazy<MethodInfo> _imiSqlGeometryStBoundary;

		// Token: 0x0400018B RID: 395
		private readonly Lazy<MethodInfo> _imiSqlGeometryStIsValid;

		// Token: 0x0400018C RID: 396
		private readonly Lazy<MethodInfo> _imiSqlGeometryStEquals;

		// Token: 0x0400018D RID: 397
		private readonly Lazy<MethodInfo> _imiSqlGeometryStDisjoint;

		// Token: 0x0400018E RID: 398
		private readonly Lazy<MethodInfo> _imiSqlGeometryStIntersects;

		// Token: 0x0400018F RID: 399
		private readonly Lazy<MethodInfo> _imiSqlGeometryStTouches;

		// Token: 0x04000190 RID: 400
		private readonly Lazy<MethodInfo> _imiSqlGeometryStCrosses;

		// Token: 0x04000191 RID: 401
		private readonly Lazy<MethodInfo> _imiSqlGeometryStWithin;

		// Token: 0x04000192 RID: 402
		private readonly Lazy<MethodInfo> _imiSqlGeometryStContains;

		// Token: 0x04000193 RID: 403
		private readonly Lazy<MethodInfo> _imiSqlGeometryStOverlaps;

		// Token: 0x04000194 RID: 404
		private readonly Lazy<MethodInfo> _imiSqlGeometryStRelate;

		// Token: 0x04000195 RID: 405
		private readonly Lazy<MethodInfo> _imiSqlGeometryStBuffer;

		// Token: 0x04000196 RID: 406
		private readonly Lazy<MethodInfo> _imiSqlGeometryStDistance;

		// Token: 0x04000197 RID: 407
		private readonly Lazy<MethodInfo> _imiSqlGeometryStConvexHull;

		// Token: 0x04000198 RID: 408
		private readonly Lazy<MethodInfo> _imiSqlGeometryStIntersection;

		// Token: 0x04000199 RID: 409
		private readonly Lazy<MethodInfo> _imiSqlGeometryStUnion;

		// Token: 0x0400019A RID: 410
		private readonly Lazy<MethodInfo> _imiSqlGeometryStDifference;

		// Token: 0x0400019B RID: 411
		private readonly Lazy<MethodInfo> _imiSqlGeometryStSymDifference;

		// Token: 0x0400019C RID: 412
		private readonly Lazy<MethodInfo> _imiSqlGeometryStNumGeometries;

		// Token: 0x0400019D RID: 413
		private readonly Lazy<MethodInfo> _imiSqlGeometryStGeometryN;

		// Token: 0x0400019E RID: 414
		private readonly Lazy<PropertyInfo> _ipiSqlGeometryStx;

		// Token: 0x0400019F RID: 415
		private readonly Lazy<PropertyInfo> _ipiSqlGeometrySty;

		// Token: 0x040001A0 RID: 416
		private readonly Lazy<PropertyInfo> _ipiSqlGeometryZ;

		// Token: 0x040001A1 RID: 417
		private readonly Lazy<PropertyInfo> _ipiSqlGeometryM;

		// Token: 0x040001A2 RID: 418
		private readonly Lazy<MethodInfo> _imiSqlGeometryStLength;

		// Token: 0x040001A3 RID: 419
		private readonly Lazy<MethodInfo> _imiSqlGeometryStStartPoint;

		// Token: 0x040001A4 RID: 420
		private readonly Lazy<MethodInfo> _imiSqlGeometryStEndPoint;

		// Token: 0x040001A5 RID: 421
		private readonly Lazy<MethodInfo> _imiSqlGeometryStIsClosed;

		// Token: 0x040001A6 RID: 422
		private readonly Lazy<MethodInfo> _imiSqlGeometryStIsRing;

		// Token: 0x040001A7 RID: 423
		private readonly Lazy<MethodInfo> _imiSqlGeometryStNumPoints;

		// Token: 0x040001A8 RID: 424
		private readonly Lazy<MethodInfo> _imiSqlGeometryStPointN;

		// Token: 0x040001A9 RID: 425
		private readonly Lazy<MethodInfo> _imiSqlGeometryStArea;

		// Token: 0x040001AA RID: 426
		private readonly Lazy<MethodInfo> _imiSqlGeometryStCentroid;

		// Token: 0x040001AB RID: 427
		private readonly Lazy<MethodInfo> _imiSqlGeometryStPointOnSurface;

		// Token: 0x040001AC RID: 428
		private readonly Lazy<MethodInfo> _imiSqlGeometryStExteriorRing;

		// Token: 0x040001AD RID: 429
		private readonly Lazy<MethodInfo> _imiSqlGeometryStNumInteriorRing;

		// Token: 0x040001AE RID: 430
		private readonly Lazy<MethodInfo> _imiSqlGeometryStInteriorRingN;
	}
}
