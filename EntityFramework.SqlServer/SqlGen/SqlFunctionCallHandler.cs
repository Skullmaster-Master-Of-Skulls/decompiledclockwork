using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Spatial;
using System.Data.Entity.SqlServer.Resources;
using System.Data.Entity.SqlServer.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;

namespace System.Data.Entity.SqlServer.SqlGen
{
	// Token: 0x02000037 RID: 55
	internal static class SqlFunctionCallHandler
	{
		// Token: 0x06000310 RID: 784 RVA: 0x0000CA88 File Offset: 0x0000AC88
		private static Dictionary<string, SqlFunctionCallHandler.FunctionHandler> InitializeStoreFunctionHandlers()
		{
			Dictionary<string, SqlFunctionCallHandler.FunctionHandler> dictionary = new Dictionary<string, SqlFunctionCallHandler.FunctionHandler>(15, StringComparer.Ordinal);
			dictionary.Add("CONCAT", new SqlFunctionCallHandler.FunctionHandler(SqlFunctionCallHandler.HandleConcatFunction));
			dictionary.Add("DATEADD", new SqlFunctionCallHandler.FunctionHandler(SqlFunctionCallHandler.HandleDatepartDateFunction));
			dictionary.Add("DATEDIFF", new SqlFunctionCallHandler.FunctionHandler(SqlFunctionCallHandler.HandleDatepartDateFunction));
			dictionary.Add("DATENAME", new SqlFunctionCallHandler.FunctionHandler(SqlFunctionCallHandler.HandleDatepartDateFunction));
			dictionary.Add("DATEPART", new SqlFunctionCallHandler.FunctionHandler(SqlFunctionCallHandler.HandleDatepartDateFunction));
			dictionary.Add("POINTGEOGRAPHY", (SqlGenerator sqlgen, DbFunctionExpression functionExpression) => SqlFunctionCallHandler.HandleFunctionDefaultGivenName(sqlgen, functionExpression, "geography::Point"));
			dictionary.Add("POINTGEOMETRY", (SqlGenerator sqlgen, DbFunctionExpression functionExpression) => SqlFunctionCallHandler.HandleFunctionDefaultGivenName(sqlgen, functionExpression, "geometry::Point"));
			dictionary.Add("ASTEXTZM", (SqlGenerator sqlgen, DbFunctionExpression functionExpression) => SqlFunctionCallHandler.WriteInstanceFunctionCall(sqlgen, "AsTextZM", functionExpression, false));
			dictionary.Add("BUFFERWITHTOLERANCE", (SqlGenerator sqlgen, DbFunctionExpression functionExpression) => SqlFunctionCallHandler.WriteInstanceFunctionCall(sqlgen, "BufferWithTolerance", functionExpression, false));
			dictionary.Add("ENVELOPEANGLE", (SqlGenerator sqlgen, DbFunctionExpression functionExpression) => SqlFunctionCallHandler.WriteInstanceFunctionCall(sqlgen, "EnvelopeAngle", functionExpression, false));
			dictionary.Add("ENVELOPECENTER", (SqlGenerator sqlgen, DbFunctionExpression functionExpression) => SqlFunctionCallHandler.WriteInstanceFunctionCall(sqlgen, "EnvelopeCenter", functionExpression, false));
			dictionary.Add("INSTANCEOF", (SqlGenerator sqlgen, DbFunctionExpression functionExpression) => SqlFunctionCallHandler.WriteInstanceFunctionCall(sqlgen, "InstanceOf", functionExpression, false));
			dictionary.Add("FILTER", (SqlGenerator sqlgen, DbFunctionExpression functionExpression) => SqlFunctionCallHandler.WriteInstanceFunctionCall(sqlgen, "Filter", functionExpression, false));
			dictionary.Add("MAKEVALID", (SqlGenerator sqlgen, DbFunctionExpression functionExpression) => SqlFunctionCallHandler.WriteInstanceFunctionCall(sqlgen, "MakeValid", functionExpression, false));
			dictionary.Add("REDUCE", (SqlGenerator sqlgen, DbFunctionExpression functionExpression) => SqlFunctionCallHandler.WriteInstanceFunctionCall(sqlgen, "Reduce", functionExpression, false));
			dictionary.Add("NUMRINGS", (SqlGenerator sqlgen, DbFunctionExpression functionExpression) => SqlFunctionCallHandler.WriteInstanceFunctionCall(sqlgen, "NumRings", functionExpression, false));
			dictionary.Add("RINGN", (SqlGenerator sqlgen, DbFunctionExpression functionExpression) => SqlFunctionCallHandler.WriteInstanceFunctionCall(sqlgen, "RingN", functionExpression, false));
			return dictionary;
		}

		// Token: 0x06000311 RID: 785 RVA: 0x0000CCF8 File Offset: 0x0000AEF8
		private static Dictionary<string, SqlFunctionCallHandler.FunctionHandler> InitializeCanonicalFunctionHandlers()
		{
			return new Dictionary<string, SqlFunctionCallHandler.FunctionHandler>(16, StringComparer.Ordinal)
			{
				{
					"IndexOf",
					new SqlFunctionCallHandler.FunctionHandler(SqlFunctionCallHandler.HandleCanonicalFunctionIndexOf)
				},
				{
					"Length",
					new SqlFunctionCallHandler.FunctionHandler(SqlFunctionCallHandler.HandleCanonicalFunctionLength)
				},
				{
					"NewGuid",
					new SqlFunctionCallHandler.FunctionHandler(SqlFunctionCallHandler.HandleCanonicalFunctionNewGuid)
				},
				{
					"Round",
					new SqlFunctionCallHandler.FunctionHandler(SqlFunctionCallHandler.HandleCanonicalFunctionRound)
				},
				{
					"Truncate",
					new SqlFunctionCallHandler.FunctionHandler(SqlFunctionCallHandler.HandleCanonicalFunctionTruncate)
				},
				{
					"Abs",
					new SqlFunctionCallHandler.FunctionHandler(SqlFunctionCallHandler.HandleCanonicalFunctionAbs)
				},
				{
					"ToLower",
					new SqlFunctionCallHandler.FunctionHandler(SqlFunctionCallHandler.HandleCanonicalFunctionToLower)
				},
				{
					"ToUpper",
					new SqlFunctionCallHandler.FunctionHandler(SqlFunctionCallHandler.HandleCanonicalFunctionToUpper)
				},
				{
					"Trim",
					new SqlFunctionCallHandler.FunctionHandler(SqlFunctionCallHandler.HandleCanonicalFunctionTrim)
				},
				{
					"Contains",
					new SqlFunctionCallHandler.FunctionHandler(SqlFunctionCallHandler.HandleCanonicalFunctionContains)
				},
				{
					"StartsWith",
					new SqlFunctionCallHandler.FunctionHandler(SqlFunctionCallHandler.HandleCanonicalFunctionStartsWith)
				},
				{
					"EndsWith",
					new SqlFunctionCallHandler.FunctionHandler(SqlFunctionCallHandler.HandleCanonicalFunctionEndsWith)
				},
				{
					"Year",
					new SqlFunctionCallHandler.FunctionHandler(SqlFunctionCallHandler.HandleCanonicalFunctionDatepart)
				},
				{
					"Month",
					new SqlFunctionCallHandler.FunctionHandler(SqlFunctionCallHandler.HandleCanonicalFunctionDatepart)
				},
				{
					"Day",
					new SqlFunctionCallHandler.FunctionHandler(SqlFunctionCallHandler.HandleCanonicalFunctionDatepart)
				},
				{
					"Hour",
					new SqlFunctionCallHandler.FunctionHandler(SqlFunctionCallHandler.HandleCanonicalFunctionDatepart)
				},
				{
					"Minute",
					new SqlFunctionCallHandler.FunctionHandler(SqlFunctionCallHandler.HandleCanonicalFunctionDatepart)
				},
				{
					"Second",
					new SqlFunctionCallHandler.FunctionHandler(SqlFunctionCallHandler.HandleCanonicalFunctionDatepart)
				},
				{
					"Millisecond",
					new SqlFunctionCallHandler.FunctionHandler(SqlFunctionCallHandler.HandleCanonicalFunctionDatepart)
				},
				{
					"DayOfYear",
					new SqlFunctionCallHandler.FunctionHandler(SqlFunctionCallHandler.HandleCanonicalFunctionDatepart)
				},
				{
					"CurrentDateTime",
					new SqlFunctionCallHandler.FunctionHandler(SqlFunctionCallHandler.HandleCanonicalFunctionCurrentDateTime)
				},
				{
					"CurrentUtcDateTime",
					new SqlFunctionCallHandler.FunctionHandler(SqlFunctionCallHandler.HandleCanonicalFunctionCurrentUtcDateTime)
				},
				{
					"CurrentDateTimeOffset",
					new SqlFunctionCallHandler.FunctionHandler(SqlFunctionCallHandler.HandleCanonicalFunctionCurrentDateTimeOffset)
				},
				{
					"GetTotalOffsetMinutes",
					new SqlFunctionCallHandler.FunctionHandler(SqlFunctionCallHandler.HandleCanonicalFunctionGetTotalOffsetMinutes)
				},
				{
					"TruncateTime",
					new SqlFunctionCallHandler.FunctionHandler(SqlFunctionCallHandler.HandleCanonicalFunctionTruncateTime)
				},
				{
					"CreateDateTime",
					new SqlFunctionCallHandler.FunctionHandler(SqlFunctionCallHandler.HandleCanonicalFunctionCreateDateTime)
				},
				{
					"CreateDateTimeOffset",
					new SqlFunctionCallHandler.FunctionHandler(SqlFunctionCallHandler.HandleCanonicalFunctionCreateDateTimeOffset)
				},
				{
					"CreateTime",
					new SqlFunctionCallHandler.FunctionHandler(SqlFunctionCallHandler.HandleCanonicalFunctionCreateTime)
				},
				{
					"AddYears",
					new SqlFunctionCallHandler.FunctionHandler(SqlFunctionCallHandler.HandleCanonicalFunctionDateAdd)
				},
				{
					"AddMonths",
					new SqlFunctionCallHandler.FunctionHandler(SqlFunctionCallHandler.HandleCanonicalFunctionDateAdd)
				},
				{
					"AddDays",
					new SqlFunctionCallHandler.FunctionHandler(SqlFunctionCallHandler.HandleCanonicalFunctionDateAdd)
				},
				{
					"AddHours",
					new SqlFunctionCallHandler.FunctionHandler(SqlFunctionCallHandler.HandleCanonicalFunctionDateAdd)
				},
				{
					"AddMinutes",
					new SqlFunctionCallHandler.FunctionHandler(SqlFunctionCallHandler.HandleCanonicalFunctionDateAdd)
				},
				{
					"AddSeconds",
					new SqlFunctionCallHandler.FunctionHandler(SqlFunctionCallHandler.HandleCanonicalFunctionDateAdd)
				},
				{
					"AddMilliseconds",
					new SqlFunctionCallHandler.FunctionHandler(SqlFunctionCallHandler.HandleCanonicalFunctionDateAdd)
				},
				{
					"AddMicroseconds",
					new SqlFunctionCallHandler.FunctionHandler(SqlFunctionCallHandler.HandleCanonicalFunctionDateAddKatmaiOrNewer)
				},
				{
					"AddNanoseconds",
					new SqlFunctionCallHandler.FunctionHandler(SqlFunctionCallHandler.HandleCanonicalFunctionDateAddKatmaiOrNewer)
				},
				{
					"DiffYears",
					new SqlFunctionCallHandler.FunctionHandler(SqlFunctionCallHandler.HandleCanonicalFunctionDateDiff)
				},
				{
					"DiffMonths",
					new SqlFunctionCallHandler.FunctionHandler(SqlFunctionCallHandler.HandleCanonicalFunctionDateDiff)
				},
				{
					"DiffDays",
					new SqlFunctionCallHandler.FunctionHandler(SqlFunctionCallHandler.HandleCanonicalFunctionDateDiff)
				},
				{
					"DiffHours",
					new SqlFunctionCallHandler.FunctionHandler(SqlFunctionCallHandler.HandleCanonicalFunctionDateDiff)
				},
				{
					"DiffMinutes",
					new SqlFunctionCallHandler.FunctionHandler(SqlFunctionCallHandler.HandleCanonicalFunctionDateDiff)
				},
				{
					"DiffSeconds",
					new SqlFunctionCallHandler.FunctionHandler(SqlFunctionCallHandler.HandleCanonicalFunctionDateDiff)
				},
				{
					"DiffMilliseconds",
					new SqlFunctionCallHandler.FunctionHandler(SqlFunctionCallHandler.HandleCanonicalFunctionDateDiff)
				},
				{
					"DiffMicroseconds",
					new SqlFunctionCallHandler.FunctionHandler(SqlFunctionCallHandler.HandleCanonicalFunctionDateDiffKatmaiOrNewer)
				},
				{
					"DiffNanoseconds",
					new SqlFunctionCallHandler.FunctionHandler(SqlFunctionCallHandler.HandleCanonicalFunctionDateDiffKatmaiOrNewer)
				},
				{
					"Concat",
					new SqlFunctionCallHandler.FunctionHandler(SqlFunctionCallHandler.HandleConcatFunction)
				},
				{
					"BitwiseAnd",
					new SqlFunctionCallHandler.FunctionHandler(SqlFunctionCallHandler.HandleCanonicalFunctionBitwise)
				},
				{
					"BitwiseNot",
					new SqlFunctionCallHandler.FunctionHandler(SqlFunctionCallHandler.HandleCanonicalFunctionBitwise)
				},
				{
					"BitwiseOr",
					new SqlFunctionCallHandler.FunctionHandler(SqlFunctionCallHandler.HandleCanonicalFunctionBitwise)
				},
				{
					"BitwiseXor",
					new SqlFunctionCallHandler.FunctionHandler(SqlFunctionCallHandler.HandleCanonicalFunctionBitwise)
				}
			};
		}

		// Token: 0x06000312 RID: 786 RVA: 0x0000D1A8 File Offset: 0x0000B3A8
		private static Dictionary<string, string> InitializeFunctionNameToOperatorDictionary()
		{
			return new Dictionary<string, string>(5, StringComparer.Ordinal)
			{
				{
					"Concat",
					"+"
				},
				{
					"CONCAT",
					"+"
				},
				{
					"BitwiseAnd",
					"&"
				},
				{
					"BitwiseNot",
					"~"
				},
				{
					"BitwiseOr",
					"|"
				},
				{
					"BitwiseXor",
					"^"
				}
			};
		}

		// Token: 0x06000313 RID: 787 RVA: 0x0000D224 File Offset: 0x0000B424
		private static Dictionary<string, string> InitializeDateAddFunctionNameToDatepartDictionary()
		{
			return new Dictionary<string, string>(5, StringComparer.Ordinal)
			{
				{
					"AddYears",
					"year"
				},
				{
					"AddMonths",
					"month"
				},
				{
					"AddDays",
					"day"
				},
				{
					"AddHours",
					"hour"
				},
				{
					"AddMinutes",
					"minute"
				},
				{
					"AddSeconds",
					"second"
				},
				{
					"AddMilliseconds",
					"millisecond"
				},
				{
					"AddMicroseconds",
					"microsecond"
				},
				{
					"AddNanoseconds",
					"nanosecond"
				}
			};
		}

		// Token: 0x06000314 RID: 788 RVA: 0x0000D2D0 File Offset: 0x0000B4D0
		private static Dictionary<string, string> InitializeDateDiffFunctionNameToDatepartDictionary()
		{
			return new Dictionary<string, string>(5, StringComparer.Ordinal)
			{
				{
					"DiffYears",
					"year"
				},
				{
					"DiffMonths",
					"month"
				},
				{
					"DiffDays",
					"day"
				},
				{
					"DiffHours",
					"hour"
				},
				{
					"DiffMinutes",
					"minute"
				},
				{
					"DiffSeconds",
					"second"
				},
				{
					"DiffMilliseconds",
					"millisecond"
				},
				{
					"DiffMicroseconds",
					"microsecond"
				},
				{
					"DiffNanoseconds",
					"nanosecond"
				}
			};
		}

		// Token: 0x06000315 RID: 789 RVA: 0x0000D440 File Offset: 0x0000B640
		[SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
		private static Dictionary<string, SqlFunctionCallHandler.FunctionHandler> InitializeGeographyStaticMethodFunctionsDictionary()
		{
			Dictionary<string, SqlFunctionCallHandler.FunctionHandler> dictionary = new Dictionary<string, SqlFunctionCallHandler.FunctionHandler>();
			dictionary.Add("GeographyFromText", new SqlFunctionCallHandler.FunctionHandler(SqlFunctionCallHandler.HandleSpatialFromTextFunction));
			dictionary.Add("GeographyPointFromText", (SqlGenerator sqlgen, DbFunctionExpression functionExpression) => SqlFunctionCallHandler.HandleFunctionDefaultGivenName(sqlgen, functionExpression, "geography::STPointFromText"));
			dictionary.Add("GeographyLineFromText", (SqlGenerator sqlgen, DbFunctionExpression functionExpression) => SqlFunctionCallHandler.HandleFunctionDefaultGivenName(sqlgen, functionExpression, "geography::STLineFromText"));
			dictionary.Add("GeographyPolygonFromText", (SqlGenerator sqlgen, DbFunctionExpression functionExpression) => SqlFunctionCallHandler.HandleFunctionDefaultGivenName(sqlgen, functionExpression, "geography::STPolyFromText"));
			dictionary.Add("GeographyMultiPointFromText", (SqlGenerator sqlgen, DbFunctionExpression functionExpression) => SqlFunctionCallHandler.HandleFunctionDefaultGivenName(sqlgen, functionExpression, "geography::STMPointFromText"));
			dictionary.Add("GeographyMultiLineFromText", (SqlGenerator sqlgen, DbFunctionExpression functionExpression) => SqlFunctionCallHandler.HandleFunctionDefaultGivenName(sqlgen, functionExpression, "geography::STMLineFromText"));
			dictionary.Add("GeographyMultiPolygonFromText", (SqlGenerator sqlgen, DbFunctionExpression functionExpression) => SqlFunctionCallHandler.HandleFunctionDefaultGivenName(sqlgen, functionExpression, "geography::STMPolyFromText"));
			dictionary.Add("GeographyCollectionFromText", (SqlGenerator sqlgen, DbFunctionExpression functionExpression) => SqlFunctionCallHandler.HandleFunctionDefaultGivenName(sqlgen, functionExpression, "geography::STGeomCollFromText"));
			dictionary.Add("GeographyFromBinary", new SqlFunctionCallHandler.FunctionHandler(SqlFunctionCallHandler.HandleSpatialFromBinaryFunction));
			dictionary.Add("GeographyPointFromBinary", (SqlGenerator sqlgen, DbFunctionExpression functionExpression) => SqlFunctionCallHandler.HandleFunctionDefaultGivenName(sqlgen, functionExpression, "geography::STPointFromWKB"));
			dictionary.Add("GeographyLineFromBinary", (SqlGenerator sqlgen, DbFunctionExpression functionExpression) => SqlFunctionCallHandler.HandleFunctionDefaultGivenName(sqlgen, functionExpression, "geography::STLineFromWKB"));
			dictionary.Add("GeographyPolygonFromBinary", (SqlGenerator sqlgen, DbFunctionExpression functionExpression) => SqlFunctionCallHandler.HandleFunctionDefaultGivenName(sqlgen, functionExpression, "geography::STPolyFromWKB"));
			dictionary.Add("GeographyMultiPointFromBinary", (SqlGenerator sqlgen, DbFunctionExpression functionExpression) => SqlFunctionCallHandler.HandleFunctionDefaultGivenName(sqlgen, functionExpression, "geography::STMPointFromWKB"));
			dictionary.Add("GeographyMultiLineFromBinary", (SqlGenerator sqlgen, DbFunctionExpression functionExpression) => SqlFunctionCallHandler.HandleFunctionDefaultGivenName(sqlgen, functionExpression, "geography::STMLineFromWKB"));
			dictionary.Add("GeographyMultiPolygonFromBinary", (SqlGenerator sqlgen, DbFunctionExpression functionExpression) => SqlFunctionCallHandler.HandleFunctionDefaultGivenName(sqlgen, functionExpression, "geography::STMPolyFromWKB"));
			dictionary.Add("GeographyCollectionFromBinary", (SqlGenerator sqlgen, DbFunctionExpression functionExpression) => SqlFunctionCallHandler.HandleFunctionDefaultGivenName(sqlgen, functionExpression, "geography::STGeomCollFromWKB"));
			dictionary.Add("GeographyFromGml", new SqlFunctionCallHandler.FunctionHandler(SqlFunctionCallHandler.HandleSpatialFromGmlFunction));
			return dictionary;
		}

		// Token: 0x06000316 RID: 790 RVA: 0x0000D6CC File Offset: 0x0000B8CC
		private static Dictionary<string, string> InitializeGeographyInstancePropertyFunctionsDictionary()
		{
			return new Dictionary<string, string>
			{
				{
					"CoordinateSystemId",
					"STSrid"
				},
				{
					"Latitude",
					"Lat"
				},
				{
					"Longitude",
					"Long"
				},
				{
					"Measure",
					"M"
				},
				{
					"Elevation",
					"Z"
				}
			};
		}

		// Token: 0x06000317 RID: 791 RVA: 0x0000D730 File Offset: 0x0000B930
		private static Dictionary<string, string> InitializeRenamedGeographyInstanceMethodFunctions()
		{
			return new Dictionary<string, string>
			{
				{
					"AsText",
					"STAsText"
				},
				{
					"AsBinary",
					"STAsBinary"
				},
				{
					"SpatialTypeName",
					"STGeometryType"
				},
				{
					"SpatialDimension",
					"STDimension"
				},
				{
					"IsEmptySpatial",
					"STIsEmpty"
				},
				{
					"SpatialEquals",
					"STEquals"
				},
				{
					"SpatialDisjoint",
					"STDisjoint"
				},
				{
					"SpatialIntersects",
					"STIntersects"
				},
				{
					"SpatialBuffer",
					"STBuffer"
				},
				{
					"Distance",
					"STDistance"
				},
				{
					"SpatialUnion",
					"STUnion"
				},
				{
					"SpatialIntersection",
					"STIntersection"
				},
				{
					"SpatialDifference",
					"STDifference"
				},
				{
					"SpatialSymmetricDifference",
					"STSymDifference"
				},
				{
					"SpatialElementCount",
					"STNumGeometries"
				},
				{
					"SpatialElementAt",
					"STGeometryN"
				},
				{
					"SpatialLength",
					"STLength"
				},
				{
					"StartPoint",
					"STStartPoint"
				},
				{
					"EndPoint",
					"STEndPoint"
				},
				{
					"IsClosedSpatial",
					"STIsClosed"
				},
				{
					"PointCount",
					"STNumPoints"
				},
				{
					"PointAt",
					"STPointN"
				},
				{
					"Area",
					"STArea"
				}
			};
		}

		// Token: 0x06000318 RID: 792 RVA: 0x0000D978 File Offset: 0x0000BB78
		[SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
		private static Dictionary<string, SqlFunctionCallHandler.FunctionHandler> InitializeGeometryStaticMethodFunctionsDictionary()
		{
			Dictionary<string, SqlFunctionCallHandler.FunctionHandler> dictionary = new Dictionary<string, SqlFunctionCallHandler.FunctionHandler>();
			dictionary.Add("GeometryFromText", new SqlFunctionCallHandler.FunctionHandler(SqlFunctionCallHandler.HandleSpatialFromTextFunction));
			dictionary.Add("GeometryPointFromText", (SqlGenerator sqlgen, DbFunctionExpression functionExpression) => SqlFunctionCallHandler.HandleFunctionDefaultGivenName(sqlgen, functionExpression, "geometry::STPointFromText"));
			dictionary.Add("GeometryLineFromText", (SqlGenerator sqlgen, DbFunctionExpression functionExpression) => SqlFunctionCallHandler.HandleFunctionDefaultGivenName(sqlgen, functionExpression, "geometry::STLineFromText"));
			dictionary.Add("GeometryPolygonFromText", (SqlGenerator sqlgen, DbFunctionExpression functionExpression) => SqlFunctionCallHandler.HandleFunctionDefaultGivenName(sqlgen, functionExpression, "geometry::STPolyFromText"));
			dictionary.Add("GeometryMultiPointFromText", (SqlGenerator sqlgen, DbFunctionExpression functionExpression) => SqlFunctionCallHandler.HandleFunctionDefaultGivenName(sqlgen, functionExpression, "geometry::STMPointFromText"));
			dictionary.Add("GeometryMultiLineFromText", (SqlGenerator sqlgen, DbFunctionExpression functionExpression) => SqlFunctionCallHandler.HandleFunctionDefaultGivenName(sqlgen, functionExpression, "geometry::STMLineFromText"));
			dictionary.Add("GeometryMultiPolygonFromText", (SqlGenerator sqlgen, DbFunctionExpression functionExpression) => SqlFunctionCallHandler.HandleFunctionDefaultGivenName(sqlgen, functionExpression, "geometry::STMPolyFromText"));
			dictionary.Add("GeometryCollectionFromText", (SqlGenerator sqlgen, DbFunctionExpression functionExpression) => SqlFunctionCallHandler.HandleFunctionDefaultGivenName(sqlgen, functionExpression, "geometry::STGeomCollFromText"));
			dictionary.Add("GeometryFromBinary", new SqlFunctionCallHandler.FunctionHandler(SqlFunctionCallHandler.HandleSpatialFromBinaryFunction));
			dictionary.Add("GeometryPointFromBinary", (SqlGenerator sqlgen, DbFunctionExpression functionExpression) => SqlFunctionCallHandler.HandleFunctionDefaultGivenName(sqlgen, functionExpression, "geometry::STPointFromWKB"));
			dictionary.Add("GeometryLineFromBinary", (SqlGenerator sqlgen, DbFunctionExpression functionExpression) => SqlFunctionCallHandler.HandleFunctionDefaultGivenName(sqlgen, functionExpression, "geometry::STLineFromWKB"));
			dictionary.Add("GeometryPolygonFromBinary", (SqlGenerator sqlgen, DbFunctionExpression functionExpression) => SqlFunctionCallHandler.HandleFunctionDefaultGivenName(sqlgen, functionExpression, "geometry::STPolyFromWKB"));
			dictionary.Add("GeometryMultiPointFromBinary", (SqlGenerator sqlgen, DbFunctionExpression functionExpression) => SqlFunctionCallHandler.HandleFunctionDefaultGivenName(sqlgen, functionExpression, "geometry::STMPointFromWKB"));
			dictionary.Add("GeometryMultiLineFromBinary", (SqlGenerator sqlgen, DbFunctionExpression functionExpression) => SqlFunctionCallHandler.HandleFunctionDefaultGivenName(sqlgen, functionExpression, "geometry::STMLineFromWKB"));
			dictionary.Add("GeometryMultiPolygonFromBinary", (SqlGenerator sqlgen, DbFunctionExpression functionExpression) => SqlFunctionCallHandler.HandleFunctionDefaultGivenName(sqlgen, functionExpression, "geometry::STMPolyFromWKB"));
			dictionary.Add("GeometryCollectionFromBinary", (SqlGenerator sqlgen, DbFunctionExpression functionExpression) => SqlFunctionCallHandler.HandleFunctionDefaultGivenName(sqlgen, functionExpression, "geometry::STGeomCollFromWKB"));
			dictionary.Add("GeometryFromGml", new SqlFunctionCallHandler.FunctionHandler(SqlFunctionCallHandler.HandleSpatialFromGmlFunction));
			return dictionary;
		}

		// Token: 0x06000319 RID: 793 RVA: 0x0000DC04 File Offset: 0x0000BE04
		private static Dictionary<string, string> InitializeGeometryInstancePropertyFunctionsDictionary()
		{
			return new Dictionary<string, string>
			{
				{
					"CoordinateSystemId",
					"STSrid"
				},
				{
					"Measure",
					"M"
				},
				{
					"XCoordinate",
					"STX"
				},
				{
					"YCoordinate",
					"STY"
				},
				{
					"Elevation",
					"Z"
				}
			};
		}

		// Token: 0x0600031A RID: 794 RVA: 0x0000DC68 File Offset: 0x0000BE68
		private static Dictionary<string, string> InitializeRenamedGeometryInstanceMethodFunctions()
		{
			return new Dictionary<string, string>
			{
				{
					"AsText",
					"STAsText"
				},
				{
					"AsBinary",
					"STAsBinary"
				},
				{
					"SpatialTypeName",
					"STGeometryType"
				},
				{
					"SpatialDimension",
					"STDimension"
				},
				{
					"IsEmptySpatial",
					"STIsEmpty"
				},
				{
					"IsSimpleGeometry",
					"STIsSimple"
				},
				{
					"IsValidGeometry",
					"STIsValid"
				},
				{
					"SpatialBoundary",
					"STBoundary"
				},
				{
					"SpatialEnvelope",
					"STEnvelope"
				},
				{
					"SpatialEquals",
					"STEquals"
				},
				{
					"SpatialDisjoint",
					"STDisjoint"
				},
				{
					"SpatialIntersects",
					"STIntersects"
				},
				{
					"SpatialTouches",
					"STTouches"
				},
				{
					"SpatialCrosses",
					"STCrosses"
				},
				{
					"SpatialWithin",
					"STWithin"
				},
				{
					"SpatialContains",
					"STContains"
				},
				{
					"SpatialOverlaps",
					"STOverlaps"
				},
				{
					"SpatialRelate",
					"STRelate"
				},
				{
					"SpatialBuffer",
					"STBuffer"
				},
				{
					"SpatialConvexHull",
					"STConvexHull"
				},
				{
					"Distance",
					"STDistance"
				},
				{
					"SpatialUnion",
					"STUnion"
				},
				{
					"SpatialIntersection",
					"STIntersection"
				},
				{
					"SpatialDifference",
					"STDifference"
				},
				{
					"SpatialSymmetricDifference",
					"STSymDifference"
				},
				{
					"SpatialElementCount",
					"STNumGeometries"
				},
				{
					"SpatialElementAt",
					"STGeometryN"
				},
				{
					"SpatialLength",
					"STLength"
				},
				{
					"StartPoint",
					"STStartPoint"
				},
				{
					"EndPoint",
					"STEndPoint"
				},
				{
					"IsClosedSpatial",
					"STIsClosed"
				},
				{
					"IsRing",
					"STIsRing"
				},
				{
					"PointCount",
					"STNumPoints"
				},
				{
					"PointAt",
					"STPointN"
				},
				{
					"Area",
					"STArea"
				},
				{
					"Centroid",
					"STCentroid"
				},
				{
					"PointOnSurface",
					"STPointOnSurface"
				},
				{
					"ExteriorRing",
					"STExteriorRing"
				},
				{
					"InteriorRingCount",
					"STNumInteriorRing"
				},
				{
					"InteriorRingAt",
					"STInteriorRingN"
				}
			};
		}

		// Token: 0x0600031B RID: 795 RVA: 0x0000DEFC File Offset: 0x0000C0FC
		private static ISqlFragment HandleSpatialFromTextFunction(SqlGenerator sqlgen, DbFunctionExpression functionExpression)
		{
			string functionName = functionExpression.ResultType.IsPrimitiveType(PrimitiveTypeKind.Geometry) ? "geometry::STGeomFromText" : "geography::STGeomFromText";
			string functionName2 = functionExpression.ResultType.IsPrimitiveType(PrimitiveTypeKind.Geometry) ? "geometry::Parse" : "geography::Parse";
			if (functionExpression.Arguments.Count == 2)
			{
				return SqlFunctionCallHandler.HandleFunctionDefaultGivenName(sqlgen, functionExpression, functionName);
			}
			return SqlFunctionCallHandler.HandleFunctionDefaultGivenName(sqlgen, functionExpression, functionName2);
		}

		// Token: 0x0600031C RID: 796 RVA: 0x0000DF60 File Offset: 0x0000C160
		private static ISqlFragment HandleSpatialFromGmlFunction(SqlGenerator sqlgen, DbFunctionExpression functionExpression)
		{
			return SqlFunctionCallHandler.HandleSpatialStaticMethodFunctionAppendSrid(sqlgen, functionExpression, functionExpression.ResultType.IsPrimitiveType(PrimitiveTypeKind.Geometry) ? "geometry::GeomFromGml" : "geography::GeomFromGml");
		}

		// Token: 0x0600031D RID: 797 RVA: 0x0000DF84 File Offset: 0x0000C184
		private static ISqlFragment HandleSpatialFromBinaryFunction(SqlGenerator sqlgen, DbFunctionExpression functionExpression)
		{
			return SqlFunctionCallHandler.HandleSpatialStaticMethodFunctionAppendSrid(sqlgen, functionExpression, functionExpression.ResultType.IsPrimitiveType(PrimitiveTypeKind.Geometry) ? "geometry::STGeomFromWKB" : "geography::STGeomFromWKB");
		}

		// Token: 0x0600031E RID: 798 RVA: 0x0000DFA8 File Offset: 0x0000C1A8
		private static ISqlFragment HandleSpatialStaticMethodFunctionAppendSrid(SqlGenerator sqlgen, DbFunctionExpression functionExpression, string functionName)
		{
			if (functionExpression.Arguments.Count == 2)
			{
				return SqlFunctionCallHandler.HandleFunctionDefaultGivenName(sqlgen, functionExpression, functionName);
			}
			DbExpression dbExpression = functionExpression.ResultType.IsPrimitiveType(PrimitiveTypeKind.Geometry) ? SqlFunctionCallHandler._defaultGeometrySridExpression : SqlFunctionCallHandler._defaultGeographySridExpression;
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.Append(functionName);
			SqlFunctionCallHandler.WriteFunctionArguments(sqlgen, functionExpression.Arguments.Concat(new DbExpression[]
			{
				dbExpression
			}), sqlBuilder);
			return sqlBuilder;
		}

		// Token: 0x0600031F RID: 799 RVA: 0x0000E014 File Offset: 0x0000C214
		internal static ISqlFragment GenerateFunctionCallSql(SqlGenerator sqlgen, DbFunctionExpression functionExpression)
		{
			if (SqlFunctionCallHandler.IsSpecialCanonicalFunction(functionExpression))
			{
				return SqlFunctionCallHandler.HandleSpecialCanonicalFunction(sqlgen, functionExpression);
			}
			if (SqlFunctionCallHandler.IsSpecialStoreFunction(functionExpression))
			{
				return SqlFunctionCallHandler.HandleSpecialStoreFunction(sqlgen, functionExpression);
			}
			PrimitiveTypeKind spatialTypeKind;
			if (SqlFunctionCallHandler.IsSpatialCanonicalFunction(functionExpression, out spatialTypeKind))
			{
				return SqlFunctionCallHandler.HandleSpatialCanonicalFunction(sqlgen, functionExpression, spatialTypeKind);
			}
			return SqlFunctionCallHandler.HandleFunctionDefault(sqlgen, functionExpression);
		}

		// Token: 0x06000320 RID: 800 RVA: 0x0000E05B File Offset: 0x0000C25B
		private static bool IsSpecialStoreFunction(DbFunctionExpression e)
		{
			return SqlFunctionCallHandler.IsStoreFunction(e.Function) && SqlFunctionCallHandler._storeFunctionHandlers.ContainsKey(e.Function.Name);
		}

		// Token: 0x06000321 RID: 801 RVA: 0x0000E081 File Offset: 0x0000C281
		private static bool IsSpecialCanonicalFunction(DbFunctionExpression e)
		{
			return e.Function.IsCanonicalFunction() && SqlFunctionCallHandler._canonicalFunctionHandlers.ContainsKey(e.Function.Name);
		}

		// Token: 0x06000322 RID: 802 RVA: 0x0000E0A8 File Offset: 0x0000C2A8
		private static bool IsSpatialCanonicalFunction(DbFunctionExpression e, out PrimitiveTypeKind spatialTypeKind)
		{
			if (e.Function.IsCanonicalFunction())
			{
				if (e.ResultType.IsSpatialType(out spatialTypeKind))
				{
					return true;
				}
				foreach (FunctionParameter functionParameter in e.Function.Parameters)
				{
					if (functionParameter.TypeUsage.IsSpatialType(out spatialTypeKind))
					{
						return true;
					}
				}
			}
			spatialTypeKind = PrimitiveTypeKind.Binary;
			return false;
		}

		// Token: 0x06000323 RID: 803 RVA: 0x0000E130 File Offset: 0x0000C330
		private static ISqlFragment HandleFunctionDefault(SqlGenerator sqlgen, DbFunctionExpression e)
		{
			return SqlFunctionCallHandler.HandleFunctionDefaultGivenName(sqlgen, e, null);
		}

		// Token: 0x06000324 RID: 804 RVA: 0x0000E13C File Offset: 0x0000C33C
		private static ISqlFragment HandleFunctionDefaultGivenName(SqlGenerator sqlgen, DbFunctionExpression e, string functionName)
		{
			if (SqlFunctionCallHandler.CastReturnTypeToInt64(e))
			{
				return SqlFunctionCallHandler.HandleFunctionDefaultCastReturnValue(sqlgen, e, functionName, "bigint");
			}
			if (SqlFunctionCallHandler.CastReturnTypeToInt32(sqlgen, e))
			{
				return SqlFunctionCallHandler.HandleFunctionDefaultCastReturnValue(sqlgen, e, functionName, "int");
			}
			if (SqlFunctionCallHandler.CastReturnTypeToInt16(e))
			{
				return SqlFunctionCallHandler.HandleFunctionDefaultCastReturnValue(sqlgen, e, functionName, "smallint");
			}
			if (SqlFunctionCallHandler.CastReturnTypeToSingle(e))
			{
				return SqlFunctionCallHandler.HandleFunctionDefaultCastReturnValue(sqlgen, e, functionName, "real");
			}
			return SqlFunctionCallHandler.HandleFunctionDefaultCastReturnValue(sqlgen, e, functionName, null);
		}

		// Token: 0x06000325 RID: 805 RVA: 0x0000E1F0 File Offset: 0x0000C3F0
		private static ISqlFragment HandleFunctionDefaultCastReturnValue(SqlGenerator sqlgen, DbFunctionExpression e, string functionName, string returnType)
		{
			return SqlFunctionCallHandler.WrapWithCast(returnType, delegate(SqlBuilder result)
			{
				if (functionName == null)
				{
					SqlFunctionCallHandler.WriteFunctionName(result, e.Function);
				}
				else
				{
					result.Append(functionName);
				}
				SqlFunctionCallHandler.HandleFunctionArgumentsDefault(sqlgen, e, result);
			});
		}

		// Token: 0x06000326 RID: 806 RVA: 0x0000E22C File Offset: 0x0000C42C
		private static ISqlFragment WrapWithCast(string returnType, Action<SqlBuilder> toWrap)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			if (returnType != null)
			{
				sqlBuilder.Append(" CAST(");
			}
			toWrap(sqlBuilder);
			if (returnType != null)
			{
				sqlBuilder.Append(" AS ");
				sqlBuilder.Append(returnType);
				sqlBuilder.Append(")");
			}
			return sqlBuilder;
		}

		// Token: 0x06000327 RID: 807 RVA: 0x0000E278 File Offset: 0x0000C478
		private static void HandleFunctionArgumentsDefault(SqlGenerator sqlgen, DbFunctionExpression e, SqlBuilder result)
		{
			bool niladicFunctionAttribute = e.Function.NiladicFunctionAttribute;
			if (niladicFunctionAttribute && e.Arguments.Count > 0)
			{
				throw new MetadataException(Strings.SqlGen_NiladicFunctionsCannotHaveParameters);
			}
			if (!niladicFunctionAttribute)
			{
				SqlFunctionCallHandler.WriteFunctionArguments(sqlgen, e.Arguments, result);
			}
		}

		// Token: 0x06000328 RID: 808 RVA: 0x0000E2C0 File Offset: 0x0000C4C0
		private static void WriteFunctionArguments(SqlGenerator sqlgen, IEnumerable<DbExpression> functionArguments, SqlBuilder result)
		{
			result.Append("(");
			string s = "";
			foreach (DbExpression dbExpression in functionArguments)
			{
				result.Append(s);
				result.Append(dbExpression.Accept<ISqlFragment>(sqlgen));
				s = ", ";
			}
			result.Append(")");
		}

		// Token: 0x06000329 RID: 809 RVA: 0x0000E338 File Offset: 0x0000C538
		private static ISqlFragment HandleFunctionGivenNameBasedOnVersion(SqlGenerator sqlgen, DbFunctionExpression e, string preKatmaiName, string katmaiName)
		{
			if (sqlgen.IsPreKatmai)
			{
				return SqlFunctionCallHandler.HandleFunctionDefaultGivenName(sqlgen, e, preKatmaiName);
			}
			return SqlFunctionCallHandler.HandleFunctionDefaultGivenName(sqlgen, e, katmaiName);
		}

		// Token: 0x0600032A RID: 810 RVA: 0x0000E353 File Offset: 0x0000C553
		private static ISqlFragment HandleSpecialStoreFunction(SqlGenerator sqlgen, DbFunctionExpression e)
		{
			return SqlFunctionCallHandler.HandleSpecialFunction(SqlFunctionCallHandler._storeFunctionHandlers, sqlgen, e);
		}

		// Token: 0x0600032B RID: 811 RVA: 0x0000E361 File Offset: 0x0000C561
		private static ISqlFragment HandleSpecialCanonicalFunction(SqlGenerator sqlgen, DbFunctionExpression e)
		{
			return SqlFunctionCallHandler.HandleSpecialFunction(SqlFunctionCallHandler._canonicalFunctionHandlers, sqlgen, e);
		}

		// Token: 0x0600032C RID: 812 RVA: 0x0000E36F File Offset: 0x0000C56F
		private static ISqlFragment HandleSpecialFunction(Dictionary<string, SqlFunctionCallHandler.FunctionHandler> handlers, SqlGenerator sqlgen, DbFunctionExpression e)
		{
			return handlers[e.Function.Name](sqlgen, e);
		}

		// Token: 0x0600032D RID: 813 RVA: 0x0000E389 File Offset: 0x0000C589
		private static ISqlFragment HandleSpatialCanonicalFunction(SqlGenerator sqlgen, DbFunctionExpression functionExpression, PrimitiveTypeKind spatialTypeKind)
		{
			if (spatialTypeKind == PrimitiveTypeKind.Geography)
			{
				return SqlFunctionCallHandler.HandleSpatialCanonicalFunction(sqlgen, functionExpression, SqlFunctionCallHandler._geographyFunctionNameToStaticMethodHandlerDictionary, SqlFunctionCallHandler._geographyFunctionNameToInstancePropertyNameDictionary, SqlFunctionCallHandler._geographyRenamedInstanceMethodFunctionDictionary);
			}
			return SqlFunctionCallHandler.HandleSpatialCanonicalFunction(sqlgen, functionExpression, SqlFunctionCallHandler._geometryFunctionNameToStaticMethodHandlerDictionary, SqlFunctionCallHandler._geometryFunctionNameToInstancePropertyNameDictionary, SqlFunctionCallHandler._geometryRenamedInstanceMethodFunctionDictionary);
		}

		// Token: 0x0600032E RID: 814 RVA: 0x0000E3C0 File Offset: 0x0000C5C0
		private static ISqlFragment HandleSpatialCanonicalFunction(SqlGenerator sqlgen, DbFunctionExpression functionExpression, Dictionary<string, SqlFunctionCallHandler.FunctionHandler> staticMethodsMap, Dictionary<string, string> instancePropertiesMap, Dictionary<string, string> renamedInstanceMethodsMap)
		{
			SqlFunctionCallHandler.FunctionHandler functionHandler;
			if (staticMethodsMap.TryGetValue(functionExpression.Function.Name, out functionHandler))
			{
				return functionHandler(sqlgen, functionExpression);
			}
			string functionName;
			if (instancePropertiesMap.TryGetValue(functionExpression.Function.Name, out functionName))
			{
				return SqlFunctionCallHandler.WriteInstanceFunctionCall(sqlgen, functionName, functionExpression, true, null);
			}
			string name;
			if (!renamedInstanceMethodsMap.TryGetValue(functionExpression.Function.Name, out name))
			{
				name = functionExpression.Function.Name;
			}
			string castReturnTypeTo = null;
			if (name == "AsGml")
			{
				castReturnTypeTo = "nvarchar(max)";
			}
			return SqlFunctionCallHandler.WriteInstanceFunctionCall(sqlgen, name, functionExpression, false, castReturnTypeTo);
		}

		// Token: 0x0600032F RID: 815 RVA: 0x0000E44C File Offset: 0x0000C64C
		private static ISqlFragment WriteInstanceFunctionCall(SqlGenerator sqlgen, string functionName, DbFunctionExpression functionExpression, bool isPropertyAccess)
		{
			return SqlFunctionCallHandler.WriteInstanceFunctionCall(sqlgen, functionName, functionExpression, isPropertyAccess, null);
		}

		// Token: 0x06000330 RID: 816 RVA: 0x0000E4E8 File Offset: 0x0000C6E8
		private static ISqlFragment WriteInstanceFunctionCall(SqlGenerator sqlgen, string functionName, DbFunctionExpression functionExpression, bool isPropertyAccess, string castReturnTypeTo)
		{
			return SqlFunctionCallHandler.WrapWithCast(castReturnTypeTo, delegate(SqlBuilder result)
			{
				DbExpression dbExpression = functionExpression.Arguments[0];
				if (dbExpression.ExpressionKind != DbExpressionKind.Function)
				{
					sqlgen.ParenthesizeExpressionIfNeeded(dbExpression, result);
				}
				else
				{
					result.Append(dbExpression.Accept<ISqlFragment>(sqlgen));
				}
				result.Append(".");
				result.Append(functionName);
				if (!isPropertyAccess)
				{
					SqlFunctionCallHandler.WriteFunctionArguments(sqlgen, functionExpression.Arguments.Skip(1), result);
				}
			});
		}

		// Token: 0x06000331 RID: 817 RVA: 0x0000E52C File Offset: 0x0000C72C
		private static ISqlFragment HandleSpecialFunctionToOperator(SqlGenerator sqlgen, DbFunctionExpression e, bool parenthesiseArguments)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			if (e.Arguments.Count > 1)
			{
				if (parenthesiseArguments)
				{
					sqlBuilder.Append("(");
				}
				sqlBuilder.Append(e.Arguments[0].Accept<ISqlFragment>(sqlgen));
				if (parenthesiseArguments)
				{
					sqlBuilder.Append(")");
				}
			}
			sqlBuilder.Append(" ");
			sqlBuilder.Append(SqlFunctionCallHandler._functionNameToOperatorDictionary[e.Function.Name]);
			sqlBuilder.Append(" ");
			if (parenthesiseArguments)
			{
				sqlBuilder.Append("(");
			}
			sqlBuilder.Append(e.Arguments[e.Arguments.Count - 1].Accept<ISqlFragment>(sqlgen));
			if (parenthesiseArguments)
			{
				sqlBuilder.Append(")");
			}
			return sqlBuilder;
		}

		// Token: 0x06000332 RID: 818 RVA: 0x0000E5F3 File Offset: 0x0000C7F3
		private static ISqlFragment HandleConcatFunction(SqlGenerator sqlgen, DbFunctionExpression e)
		{
			return SqlFunctionCallHandler.HandleSpecialFunctionToOperator(sqlgen, e, false);
		}

		// Token: 0x06000333 RID: 819 RVA: 0x0000E5FD File Offset: 0x0000C7FD
		private static ISqlFragment HandleCanonicalFunctionBitwise(SqlGenerator sqlgen, DbFunctionExpression e)
		{
			return SqlFunctionCallHandler.HandleSpecialFunctionToOperator(sqlgen, e, true);
		}

		// Token: 0x06000334 RID: 820 RVA: 0x0000E608 File Offset: 0x0000C808
		internal static ISqlFragment HandleDatepartDateFunction(SqlGenerator sqlgen, DbFunctionExpression e)
		{
			DbConstantExpression dbConstantExpression = e.Arguments[0] as DbConstantExpression;
			if (dbConstantExpression == null)
			{
				throw new InvalidOperationException(Strings.SqlGen_InvalidDatePartArgumentExpression(e.Function.NamespaceName, e.Function.Name));
			}
			string text = dbConstantExpression.Value as string;
			if (text == null)
			{
				throw new InvalidOperationException(Strings.SqlGen_InvalidDatePartArgumentExpression(e.Function.NamespaceName, e.Function.Name));
			}
			if (!SqlFunctionCallHandler._datepartKeywords.Contains(text))
			{
				throw new InvalidOperationException(Strings.SqlGen_InvalidDatePartArgumentValue(text, e.Function.NamespaceName, e.Function.Name));
			}
			SqlBuilder sqlBuilder = new SqlBuilder();
			SqlFunctionCallHandler.WriteFunctionName(sqlBuilder, e.Function);
			sqlBuilder.Append("(");
			sqlBuilder.Append(text);
			for (int i = 1; i < e.Arguments.Count; i++)
			{
				sqlBuilder.Append(", ");
				sqlBuilder.Append(e.Arguments[i].Accept<ISqlFragment>(sqlgen));
			}
			sqlBuilder.Append(")");
			return sqlBuilder;
		}

		// Token: 0x06000335 RID: 821 RVA: 0x0000E713 File Offset: 0x0000C913
		[SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase")]
		private static ISqlFragment HandleCanonicalFunctionDatepart(SqlGenerator sqlgen, DbFunctionExpression e)
		{
			return SqlFunctionCallHandler.HandleCanonicalFunctionDatepart(sqlgen, e.Function.Name.ToLowerInvariant(), e);
		}

		// Token: 0x06000336 RID: 822 RVA: 0x0000E72C File Offset: 0x0000C92C
		private static ISqlFragment HandleCanonicalFunctionGetTotalOffsetMinutes(SqlGenerator sqlgen, DbFunctionExpression e)
		{
			return SqlFunctionCallHandler.HandleCanonicalFunctionDatepart(sqlgen, "tzoffset", e);
		}

		// Token: 0x06000337 RID: 823 RVA: 0x0000E73C File Offset: 0x0000C93C
		private static ISqlFragment HandleCanonicalFunctionDatepart(SqlGenerator sqlgen, string datepart, DbFunctionExpression e)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.Append("DATEPART (");
			sqlBuilder.Append(datepart);
			sqlBuilder.Append(", ");
			sqlBuilder.Append(e.Arguments[0].Accept<ISqlFragment>(sqlgen));
			sqlBuilder.Append(")");
			return sqlBuilder;
		}

		// Token: 0x06000338 RID: 824 RVA: 0x0000E790 File Offset: 0x0000C990
		private static ISqlFragment HandleCanonicalFunctionCurrentDateTime(SqlGenerator sqlgen, DbFunctionExpression e)
		{
			return SqlFunctionCallHandler.HandleFunctionGivenNameBasedOnVersion(sqlgen, e, "GetDate", "SysDateTime");
		}

		// Token: 0x06000339 RID: 825 RVA: 0x0000E7A3 File Offset: 0x0000C9A3
		private static ISqlFragment HandleCanonicalFunctionCurrentUtcDateTime(SqlGenerator sqlgen, DbFunctionExpression e)
		{
			return SqlFunctionCallHandler.HandleFunctionGivenNameBasedOnVersion(sqlgen, e, "GetUtcDate", "SysUtcDateTime");
		}

		// Token: 0x0600033A RID: 826 RVA: 0x0000E7B6 File Offset: 0x0000C9B6
		private static ISqlFragment HandleCanonicalFunctionCurrentDateTimeOffset(SqlGenerator sqlgen, DbFunctionExpression e)
		{
			sqlgen.AssertKatmaiOrNewer(e);
			return SqlFunctionCallHandler.HandleFunctionDefaultGivenName(sqlgen, e, "SysDateTimeOffset");
		}

		// Token: 0x0600033B RID: 827 RVA: 0x0000E7CC File Offset: 0x0000C9CC
		private static ISqlFragment HandleCanonicalFunctionCreateDateTime(SqlGenerator sqlgen, DbFunctionExpression e)
		{
			string typeName = sqlgen.IsPreKatmai ? "datetime" : "datetime2";
			return SqlFunctionCallHandler.HandleCanonicalFunctionDateTimeTypeCreation(sqlgen, typeName, e.Arguments, true, false);
		}

		// Token: 0x0600033C RID: 828 RVA: 0x0000E7FD File Offset: 0x0000C9FD
		private static ISqlFragment HandleCanonicalFunctionCreateDateTimeOffset(SqlGenerator sqlgen, DbFunctionExpression e)
		{
			sqlgen.AssertKatmaiOrNewer(e);
			return SqlFunctionCallHandler.HandleCanonicalFunctionDateTimeTypeCreation(sqlgen, "datetimeoffset", e.Arguments, true, true);
		}

		// Token: 0x0600033D RID: 829 RVA: 0x0000E819 File Offset: 0x0000CA19
		private static ISqlFragment HandleCanonicalFunctionCreateTime(SqlGenerator sqlgen, DbFunctionExpression e)
		{
			sqlgen.AssertKatmaiOrNewer(e);
			return SqlFunctionCallHandler.HandleCanonicalFunctionDateTimeTypeCreation(sqlgen, "time", e.Arguments, false, false);
		}

		// Token: 0x0600033E RID: 830 RVA: 0x0000E838 File Offset: 0x0000CA38
		private static ISqlFragment HandleCanonicalFunctionDateTimeTypeCreation(SqlGenerator sqlgen, string typeName, IList<DbExpression> args, bool hasDatePart, bool hasTimeZonePart)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			int index = 0;
			sqlBuilder.Append("convert (");
			sqlBuilder.Append(typeName);
			sqlBuilder.Append(",");
			if (hasDatePart)
			{
				sqlBuilder.Append("right('000' + ");
				SqlFunctionCallHandler.AppendConvertToVarchar(sqlgen, sqlBuilder, args[index++]);
				sqlBuilder.Append(", 4)");
				sqlBuilder.Append(" + '-' + ");
				SqlFunctionCallHandler.AppendConvertToVarchar(sqlgen, sqlBuilder, args[index++]);
				sqlBuilder.Append(" + '-' + ");
				SqlFunctionCallHandler.AppendConvertToVarchar(sqlgen, sqlBuilder, args[index++]);
				sqlBuilder.Append(" + ' ' + ");
			}
			SqlFunctionCallHandler.AppendConvertToVarchar(sqlgen, sqlBuilder, args[index++]);
			sqlBuilder.Append(" + ':' + ");
			SqlFunctionCallHandler.AppendConvertToVarchar(sqlgen, sqlBuilder, args[index++]);
			sqlBuilder.Append(" + ':' + str(");
			sqlBuilder.Append(args[index++].Accept<ISqlFragment>(sqlgen));
			if (sqlgen.IsPreKatmai)
			{
				sqlBuilder.Append(", 6, 3)");
			}
			else
			{
				sqlBuilder.Append(", 10, 7)");
			}
			if (hasTimeZonePart)
			{
				sqlBuilder.Append(" + (CASE WHEN ");
				sqlgen.ParenthesizeExpressionIfNeeded(args[index], sqlBuilder);
				sqlBuilder.Append(" >= 0 THEN '+' ELSE '-' END) + convert(varchar(255), ABS(");
				sqlgen.ParenthesizeExpressionIfNeeded(args[index], sqlBuilder);
				sqlBuilder.Append("/60)) + ':' + convert(varchar(255), ABS(");
				sqlgen.ParenthesizeExpressionIfNeeded(args[index], sqlBuilder);
				sqlBuilder.Append("%60))");
			}
			sqlBuilder.Append(", 121)");
			return sqlBuilder;
		}

		// Token: 0x0600033F RID: 831 RVA: 0x0000E9B1 File Offset: 0x0000CBB1
		private static void AppendConvertToVarchar(SqlGenerator sqlgen, SqlBuilder result, DbExpression e)
		{
			result.Append("convert(varchar(255), ");
			result.Append(e.Accept<ISqlFragment>(sqlgen));
			result.Append(")");
		}

		// Token: 0x06000340 RID: 832 RVA: 0x0000E9D8 File Offset: 0x0000CBD8
		private static ISqlFragment HandleCanonicalFunctionTruncateTime(SqlGenerator sqlgen, DbFunctionExpression e)
		{
			string s = null;
			bool flag = false;
			PrimitiveTypeKind primitiveTypeKind = e.Arguments[0].ResultType.GetPrimitiveTypeKind();
			if (primitiveTypeKind == PrimitiveTypeKind.DateTime)
			{
				s = (sqlgen.IsPreKatmai ? "datetime" : "datetime2");
			}
			else if (primitiveTypeKind == PrimitiveTypeKind.DateTimeOffset)
			{
				s = "datetimeoffset";
				flag = true;
			}
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.Append("convert (");
			sqlBuilder.Append(s);
			sqlBuilder.Append(", convert(varchar(255), ");
			sqlBuilder.Append(e.Arguments[0].Accept<ISqlFragment>(sqlgen));
			sqlBuilder.Append(", 102) ");
			if (flag)
			{
				sqlBuilder.Append("+ ' 00:00:00 ' +  Right(convert(varchar(255), ");
				sqlBuilder.Append(e.Arguments[0].Accept<ISqlFragment>(sqlgen));
				sqlBuilder.Append(", 121), 6)  ");
			}
			sqlBuilder.Append(",  102)");
			return sqlBuilder;
		}

		// Token: 0x06000341 RID: 833 RVA: 0x0000EAAB File Offset: 0x0000CCAB
		private static ISqlFragment HandleCanonicalFunctionDateAddKatmaiOrNewer(SqlGenerator sqlgen, DbFunctionExpression e)
		{
			sqlgen.AssertKatmaiOrNewer(e);
			return SqlFunctionCallHandler.HandleCanonicalFunctionDateAdd(sqlgen, e);
		}

		// Token: 0x06000342 RID: 834 RVA: 0x0000EABC File Offset: 0x0000CCBC
		private static ISqlFragment HandleCanonicalFunctionDateAdd(SqlGenerator sqlgen, DbFunctionExpression e)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.Append("DATEADD (");
			sqlBuilder.Append(SqlFunctionCallHandler._dateAddFunctionNameToDatepartDictionary[e.Function.Name]);
			sqlBuilder.Append(", ");
			sqlBuilder.Append(e.Arguments[1].Accept<ISqlFragment>(sqlgen));
			sqlBuilder.Append(", ");
			sqlBuilder.Append(e.Arguments[0].Accept<ISqlFragment>(sqlgen));
			sqlBuilder.Append(")");
			return sqlBuilder;
		}

		// Token: 0x06000343 RID: 835 RVA: 0x0000EB47 File Offset: 0x0000CD47
		private static ISqlFragment HandleCanonicalFunctionDateDiffKatmaiOrNewer(SqlGenerator sqlgen, DbFunctionExpression e)
		{
			sqlgen.AssertKatmaiOrNewer(e);
			return SqlFunctionCallHandler.HandleCanonicalFunctionDateDiff(sqlgen, e);
		}

		// Token: 0x06000344 RID: 836 RVA: 0x0000EB58 File Offset: 0x0000CD58
		private static ISqlFragment HandleCanonicalFunctionDateDiff(SqlGenerator sqlgen, DbFunctionExpression e)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.Append("DATEDIFF (");
			sqlBuilder.Append(SqlFunctionCallHandler._dateDiffFunctionNameToDatepartDictionary[e.Function.Name]);
			sqlBuilder.Append(", ");
			sqlBuilder.Append(e.Arguments[0].Accept<ISqlFragment>(sqlgen));
			sqlBuilder.Append(", ");
			sqlBuilder.Append(e.Arguments[1].Accept<ISqlFragment>(sqlgen));
			sqlBuilder.Append(")");
			return sqlBuilder;
		}

		// Token: 0x06000345 RID: 837 RVA: 0x0000EBE3 File Offset: 0x0000CDE3
		private static ISqlFragment HandleCanonicalFunctionIndexOf(SqlGenerator sqlgen, DbFunctionExpression e)
		{
			return SqlFunctionCallHandler.HandleFunctionDefaultGivenName(sqlgen, e, "CHARINDEX");
		}

		// Token: 0x06000346 RID: 838 RVA: 0x0000EBF1 File Offset: 0x0000CDF1
		private static ISqlFragment HandleCanonicalFunctionNewGuid(SqlGenerator sqlgen, DbFunctionExpression e)
		{
			return SqlFunctionCallHandler.HandleFunctionDefaultGivenName(sqlgen, e, "NEWID");
		}

		// Token: 0x06000347 RID: 839 RVA: 0x0000EBFF File Offset: 0x0000CDFF
		private static ISqlFragment HandleCanonicalFunctionLength(SqlGenerator sqlgen, DbFunctionExpression e)
		{
			return SqlFunctionCallHandler.HandleFunctionDefaultGivenName(sqlgen, e, "LEN");
		}

		// Token: 0x06000348 RID: 840 RVA: 0x0000EC0D File Offset: 0x0000CE0D
		private static ISqlFragment HandleCanonicalFunctionRound(SqlGenerator sqlgen, DbFunctionExpression e)
		{
			return SqlFunctionCallHandler.HandleCanonicalFunctionRoundOrTruncate(sqlgen, e, true);
		}

		// Token: 0x06000349 RID: 841 RVA: 0x0000EC17 File Offset: 0x0000CE17
		private static ISqlFragment HandleCanonicalFunctionTruncate(SqlGenerator sqlgen, DbFunctionExpression e)
		{
			return SqlFunctionCallHandler.HandleCanonicalFunctionRoundOrTruncate(sqlgen, e, false);
		}

		// Token: 0x0600034A RID: 842 RVA: 0x0000EC24 File Offset: 0x0000CE24
		private static ISqlFragment HandleCanonicalFunctionRoundOrTruncate(SqlGenerator sqlgen, DbFunctionExpression e, bool round)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			bool flag = false;
			if (e.Arguments.Count == 1)
			{
				flag = SqlFunctionCallHandler.CastReturnTypeToSingle(e);
				if (flag)
				{
					sqlBuilder.Append(" CAST(");
				}
			}
			sqlBuilder.Append("ROUND(");
			sqlBuilder.Append(e.Arguments[0].Accept<ISqlFragment>(sqlgen));
			sqlBuilder.Append(", ");
			if (e.Arguments.Count > 1)
			{
				sqlBuilder.Append(e.Arguments[1].Accept<ISqlFragment>(sqlgen));
			}
			else
			{
				sqlBuilder.Append("0");
			}
			if (!round)
			{
				sqlBuilder.Append(", 1");
			}
			sqlBuilder.Append(")");
			if (flag)
			{
				sqlBuilder.Append(" AS real)");
			}
			return sqlBuilder;
		}

		// Token: 0x0600034B RID: 843 RVA: 0x0000ECE8 File Offset: 0x0000CEE8
		private static ISqlFragment HandleCanonicalFunctionAbs(SqlGenerator sqlgen, DbFunctionExpression e)
		{
			if (e.Arguments[0].ResultType.IsPrimitiveType(PrimitiveTypeKind.Byte))
			{
				SqlBuilder sqlBuilder = new SqlBuilder();
				sqlBuilder.Append(e.Arguments[0].Accept<ISqlFragment>(sqlgen));
				return sqlBuilder;
			}
			return SqlFunctionCallHandler.HandleFunctionDefault(sqlgen, e);
		}

		// Token: 0x0600034C RID: 844 RVA: 0x0000ED38 File Offset: 0x0000CF38
		private static ISqlFragment HandleCanonicalFunctionTrim(SqlGenerator sqlgen, DbFunctionExpression e)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.Append("LTRIM(RTRIM(");
			sqlBuilder.Append(e.Arguments[0].Accept<ISqlFragment>(sqlgen));
			sqlBuilder.Append("))");
			return sqlBuilder;
		}

		// Token: 0x0600034D RID: 845 RVA: 0x0000ED7A File Offset: 0x0000CF7A
		private static ISqlFragment HandleCanonicalFunctionToLower(SqlGenerator sqlgen, DbFunctionExpression e)
		{
			return SqlFunctionCallHandler.HandleFunctionDefaultGivenName(sqlgen, e, "LOWER");
		}

		// Token: 0x0600034E RID: 846 RVA: 0x0000ED88 File Offset: 0x0000CF88
		private static ISqlFragment HandleCanonicalFunctionToUpper(SqlGenerator sqlgen, DbFunctionExpression e)
		{
			return SqlFunctionCallHandler.HandleFunctionDefaultGivenName(sqlgen, e, "UPPER");
		}

		// Token: 0x0600034F RID: 847 RVA: 0x0000ED98 File Offset: 0x0000CF98
		private static void TranslateConstantParameterForLike(SqlGenerator sqlgen, DbExpression targetExpression, DbConstantExpression constSearchParamExpression, SqlBuilder result, bool insertPercentStart, bool insertPercentEnd)
		{
			result.Append(targetExpression.Accept<ISqlFragment>(sqlgen));
			result.Append(" LIKE ");
			StringBuilder stringBuilder = new StringBuilder();
			if (insertPercentStart)
			{
				stringBuilder.Append("%");
			}
			bool flag;
			stringBuilder.Append(SqlProviderManifest.EscapeLikeText(constSearchParamExpression.Value as string, false, out flag));
			if (insertPercentEnd)
			{
				stringBuilder.Append("%");
			}
			DbConstantExpression dbConstantExpression = constSearchParamExpression.ResultType.Constant(stringBuilder.ToString());
			result.Append(dbConstantExpression.Accept<ISqlFragment>(sqlgen));
			if (flag)
			{
				result.Append(" ESCAPE '" + '~' + "'");
			}
		}

		// Token: 0x06000350 RID: 848 RVA: 0x0000EE3B File Offset: 0x0000D03B
		private static ISqlFragment HandleCanonicalFunctionContains(SqlGenerator sqlgen, DbFunctionExpression e)
		{
			return SqlFunctionCallHandler.WrapPredicate(new Func<SqlGenerator, IList<DbExpression>, SqlBuilder, SqlBuilder>(SqlFunctionCallHandler.HandleCanonicalFunctionContains), sqlgen, e);
		}

		// Token: 0x06000351 RID: 849 RVA: 0x0000EE50 File Offset: 0x0000D050
		private static SqlBuilder HandleCanonicalFunctionContains(SqlGenerator sqlgen, IList<DbExpression> args, SqlBuilder result)
		{
			DbConstantExpression dbConstantExpression = args[1] as DbConstantExpression;
			if (dbConstantExpression != null && !string.IsNullOrEmpty(dbConstantExpression.Value as string))
			{
				SqlFunctionCallHandler.TranslateConstantParameterForLike(sqlgen, args[0], dbConstantExpression, result, true, true);
			}
			else
			{
				result.Append("CHARINDEX( ");
				result.Append(args[1].Accept<ISqlFragment>(sqlgen));
				result.Append(", ");
				result.Append(args[0].Accept<ISqlFragment>(sqlgen));
				result.Append(") > 0");
			}
			return result;
		}

		// Token: 0x06000352 RID: 850 RVA: 0x0000EEDA File Offset: 0x0000D0DA
		private static ISqlFragment HandleCanonicalFunctionStartsWith(SqlGenerator sqlgen, DbFunctionExpression e)
		{
			return SqlFunctionCallHandler.WrapPredicate(new Func<SqlGenerator, IList<DbExpression>, SqlBuilder, SqlBuilder>(SqlFunctionCallHandler.HandleCanonicalFunctionStartsWith), sqlgen, e);
		}

		// Token: 0x06000353 RID: 851 RVA: 0x0000EEF0 File Offset: 0x0000D0F0
		private static SqlBuilder HandleCanonicalFunctionStartsWith(SqlGenerator sqlgen, IList<DbExpression> args, SqlBuilder result)
		{
			DbConstantExpression dbConstantExpression = args[1] as DbConstantExpression;
			if (dbConstantExpression != null && !string.IsNullOrEmpty(dbConstantExpression.Value as string))
			{
				SqlFunctionCallHandler.TranslateConstantParameterForLike(sqlgen, args[0], dbConstantExpression, result, false, true);
			}
			else
			{
				result.Append("CHARINDEX( ");
				result.Append(args[1].Accept<ISqlFragment>(sqlgen));
				result.Append(", ");
				result.Append(args[0].Accept<ISqlFragment>(sqlgen));
				result.Append(") = 1");
			}
			return result;
		}

		// Token: 0x06000354 RID: 852 RVA: 0x0000EF7A File Offset: 0x0000D17A
		private static ISqlFragment HandleCanonicalFunctionEndsWith(SqlGenerator sqlgen, DbFunctionExpression e)
		{
			return SqlFunctionCallHandler.WrapPredicate(new Func<SqlGenerator, IList<DbExpression>, SqlBuilder, SqlBuilder>(SqlFunctionCallHandler.HandleCanonicalFunctionEndsWith), sqlgen, e);
		}

		// Token: 0x06000355 RID: 853 RVA: 0x0000EF90 File Offset: 0x0000D190
		private static SqlBuilder HandleCanonicalFunctionEndsWith(SqlGenerator sqlgen, IList<DbExpression> args, SqlBuilder result)
		{
			DbConstantExpression dbConstantExpression = args[1] as DbConstantExpression;
			DbPropertyExpression dbPropertyExpression = args[0] as DbPropertyExpression;
			if (dbConstantExpression != null && dbPropertyExpression != null && !string.IsNullOrEmpty(dbConstantExpression.Value as string))
			{
				SqlFunctionCallHandler.TranslateConstantParameterForLike(sqlgen, args[0], dbConstantExpression, result, true, false);
			}
			else
			{
				result.Append("CHARINDEX( REVERSE(");
				result.Append(args[1].Accept<ISqlFragment>(sqlgen));
				result.Append("), REVERSE(");
				result.Append(args[0].Accept<ISqlFragment>(sqlgen));
				result.Append(")) = 1");
			}
			return result;
		}

		// Token: 0x06000356 RID: 854 RVA: 0x0000F02C File Offset: 0x0000D22C
		private static ISqlFragment WrapPredicate(Func<SqlGenerator, IList<DbExpression>, SqlBuilder, SqlBuilder> predicateTranslator, SqlGenerator sqlgen, DbFunctionExpression e)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.Append("CASE WHEN (");
			predicateTranslator(sqlgen, e.Arguments, sqlBuilder);
			sqlBuilder.Append(") THEN cast(1 as bit) WHEN ( NOT (");
			predicateTranslator(sqlgen, e.Arguments, sqlBuilder);
			sqlBuilder.Append(")) THEN cast(0 as bit) END");
			return sqlBuilder;
		}

		// Token: 0x06000357 RID: 855 RVA: 0x0000F080 File Offset: 0x0000D280
		internal static void WriteFunctionName(SqlBuilder result, EdmFunction function)
		{
			string text;
			if (function.StoreFunctionNameAttribute != null)
			{
				text = function.StoreFunctionNameAttribute;
			}
			else
			{
				text = function.Name;
			}
			if (function.IsCanonicalFunction())
			{
				result.Append(text.ToUpperInvariant());
				return;
			}
			if (SqlFunctionCallHandler.IsStoreFunction(function))
			{
				result.Append(text);
				return;
			}
			if (string.IsNullOrEmpty(function.Schema))
			{
				result.Append(SqlGenerator.QuoteIdentifier(function.NamespaceName));
			}
			else
			{
				result.Append(SqlGenerator.QuoteIdentifier(function.Schema));
			}
			result.Append(".");
			result.Append(SqlGenerator.QuoteIdentifier(text));
		}

		// Token: 0x06000358 RID: 856 RVA: 0x0000F112 File Offset: 0x0000D312
		internal static bool IsStoreFunction(EdmFunction function)
		{
			return function.BuiltInAttribute && !function.IsCanonicalFunction();
		}

		// Token: 0x06000359 RID: 857 RVA: 0x0000F127 File Offset: 0x0000D327
		internal static bool CastReturnTypeToInt64(DbFunctionExpression e)
		{
			return SqlFunctionCallHandler.CastReturnTypeToGivenType(e, SqlFunctionCallHandler._functionRequiresReturnTypeCastToInt64, PrimitiveTypeKind.Int64);
		}

		// Token: 0x0600035A RID: 858 RVA: 0x0000F174 File Offset: 0x0000D374
		internal static bool CastReturnTypeToInt32(SqlGenerator sqlgen, DbFunctionExpression e)
		{
			if (!SqlFunctionCallHandler._functionRequiresReturnTypeCastToInt32.Contains(e.Function.FullName))
			{
				return false;
			}
			return (from t in e.Arguments
			select sqlgen.StoreItemCollection.ProviderManifest.GetStoreType(t.ResultType)).Any((TypeUsage storeType) => SqlFunctionCallHandler._maxTypeNames.Contains(storeType.EdmType.Name));
		}

		// Token: 0x0600035B RID: 859 RVA: 0x0000F1E0 File Offset: 0x0000D3E0
		internal static bool CastReturnTypeToInt16(DbFunctionExpression e)
		{
			return SqlFunctionCallHandler.CastReturnTypeToGivenType(e, SqlFunctionCallHandler._functionRequiresReturnTypeCastToInt16, PrimitiveTypeKind.Int16);
		}

		// Token: 0x0600035C RID: 860 RVA: 0x0000F1EF File Offset: 0x0000D3EF
		internal static bool CastReturnTypeToSingle(DbFunctionExpression e)
		{
			return SqlFunctionCallHandler.CastReturnTypeToGivenType(e, SqlFunctionCallHandler._functionRequiresReturnTypeCastToSingle, PrimitiveTypeKind.Single);
		}

		// Token: 0x0600035D RID: 861 RVA: 0x0000F218 File Offset: 0x0000D418
		private static bool CastReturnTypeToGivenType(DbFunctionExpression e, ISet<string> functionsRequiringReturnTypeCast, PrimitiveTypeKind type)
		{
			return functionsRequiringReturnTypeCast.Contains(e.Function.FullName) && e.Arguments.Any((DbExpression t) => t.ResultType.IsPrimitiveType(type));
		}

		// Token: 0x04000090 RID: 144
		private static readonly Dictionary<string, SqlFunctionCallHandler.FunctionHandler> _storeFunctionHandlers = SqlFunctionCallHandler.InitializeStoreFunctionHandlers();

		// Token: 0x04000091 RID: 145
		private static readonly Dictionary<string, SqlFunctionCallHandler.FunctionHandler> _canonicalFunctionHandlers = SqlFunctionCallHandler.InitializeCanonicalFunctionHandlers();

		// Token: 0x04000092 RID: 146
		private static readonly Dictionary<string, string> _functionNameToOperatorDictionary = SqlFunctionCallHandler.InitializeFunctionNameToOperatorDictionary();

		// Token: 0x04000093 RID: 147
		private static readonly Dictionary<string, string> _dateAddFunctionNameToDatepartDictionary = SqlFunctionCallHandler.InitializeDateAddFunctionNameToDatepartDictionary();

		// Token: 0x04000094 RID: 148
		private static readonly Dictionary<string, string> _dateDiffFunctionNameToDatepartDictionary = SqlFunctionCallHandler.InitializeDateDiffFunctionNameToDatepartDictionary();

		// Token: 0x04000095 RID: 149
		private static readonly Dictionary<string, SqlFunctionCallHandler.FunctionHandler> _geographyFunctionNameToStaticMethodHandlerDictionary = SqlFunctionCallHandler.InitializeGeographyStaticMethodFunctionsDictionary();

		// Token: 0x04000096 RID: 150
		private static readonly Dictionary<string, string> _geographyFunctionNameToInstancePropertyNameDictionary = SqlFunctionCallHandler.InitializeGeographyInstancePropertyFunctionsDictionary();

		// Token: 0x04000097 RID: 151
		private static readonly Dictionary<string, string> _geographyRenamedInstanceMethodFunctionDictionary = SqlFunctionCallHandler.InitializeRenamedGeographyInstanceMethodFunctions();

		// Token: 0x04000098 RID: 152
		private static readonly Dictionary<string, SqlFunctionCallHandler.FunctionHandler> _geometryFunctionNameToStaticMethodHandlerDictionary = SqlFunctionCallHandler.InitializeGeometryStaticMethodFunctionsDictionary();

		// Token: 0x04000099 RID: 153
		private static readonly Dictionary<string, string> _geometryFunctionNameToInstancePropertyNameDictionary = SqlFunctionCallHandler.InitializeGeometryInstancePropertyFunctionsDictionary();

		// Token: 0x0400009A RID: 154
		private static readonly Dictionary<string, string> _geometryRenamedInstanceMethodFunctionDictionary = SqlFunctionCallHandler.InitializeRenamedGeometryInstanceMethodFunctions();

		// Token: 0x0400009B RID: 155
		private static readonly ISet<string> _datepartKeywords = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
		{
			"year",
			"yy",
			"yyyy",
			"quarter",
			"qq",
			"q",
			"month",
			"mm",
			"m",
			"dayofyear",
			"dy",
			"y",
			"day",
			"dd",
			"d",
			"week",
			"wk",
			"ww",
			"weekday",
			"dw",
			"w",
			"hour",
			"hh",
			"minute",
			"mi",
			"n",
			"second",
			"ss",
			"s",
			"millisecond",
			"ms",
			"microsecond",
			"mcs",
			"nanosecond",
			"ns",
			"tzoffset",
			"tz",
			"iso_week",
			"isoww",
			"isowk"
		};

		// Token: 0x0400009C RID: 156
		private static readonly ISet<string> _functionRequiresReturnTypeCastToInt64 = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
		{
			"SqlServer.CHARINDEX"
		};

		// Token: 0x0400009D RID: 157
		private static readonly ISet<string> _functionRequiresReturnTypeCastToInt32 = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
		{
			"SqlServer.LEN",
			"SqlServer.PATINDEX",
			"SqlServer.DATALENGTH",
			"SqlServer.CHARINDEX",
			"Edm.IndexOf",
			"Edm.Length"
		};

		// Token: 0x0400009E RID: 158
		private static readonly ISet<string> _functionRequiresReturnTypeCastToInt16 = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
		{
			"Edm.Abs"
		};

		// Token: 0x0400009F RID: 159
		private static readonly ISet<string> _functionRequiresReturnTypeCastToSingle = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
		{
			"Edm.Abs",
			"Edm.Round",
			"Edm.Floor",
			"Edm.Ceiling"
		};

		// Token: 0x040000A0 RID: 160
		private static readonly ISet<string> _maxTypeNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
		{
			"varchar(max)",
			"nvarchar(max)",
			"text",
			"ntext",
			"varbinary(max)",
			"image",
			"xml"
		};

		// Token: 0x040000A1 RID: 161
		private static readonly DbExpression _defaultGeographySridExpression = DbExpressionBuilder.Constant(DbGeography.DefaultCoordinateSystemId);

		// Token: 0x040000A2 RID: 162
		private static readonly DbExpression _defaultGeometrySridExpression = DbExpressionBuilder.Constant(DbGeometry.DefaultCoordinateSystemId);

		// Token: 0x02000038 RID: 56
		// (Invoke) Token: 0x06000389 RID: 905
		private delegate ISqlFragment FunctionHandler(SqlGenerator sqlgen, DbFunctionExpression functionExpr);
	}
}
