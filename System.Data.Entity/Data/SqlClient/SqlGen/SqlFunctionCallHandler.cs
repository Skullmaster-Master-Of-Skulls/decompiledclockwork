using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Common.CommandTrees;
using System.Data.Common.CommandTrees.ExpressionBuilder;
using System.Data.Common.Utils;
using System.Data.Entity;
using System.Data.Metadata.Edm;
using System.Data.Spatial;
using System.Linq;
using System.Text;

namespace System.Data.SqlClient.SqlGen
{
	// Token: 0x0200002C RID: 44
	internal static class SqlFunctionCallHandler
	{
		// Token: 0x060003E2 RID: 994 RVA: 0x0000EED0 File Offset: 0x0000D0D0
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

		// Token: 0x060003E3 RID: 995 RVA: 0x0000F158 File Offset: 0x0000D358
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

		// Token: 0x060003E4 RID: 996 RVA: 0x0000F608 File Offset: 0x0000D808
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

		// Token: 0x060003E5 RID: 997 RVA: 0x0000F684 File Offset: 0x0000D884
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

		// Token: 0x060003E6 RID: 998 RVA: 0x0000F730 File Offset: 0x0000D930
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

		// Token: 0x060003E7 RID: 999 RVA: 0x0000F7DC File Offset: 0x0000D9DC
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

		// Token: 0x060003E8 RID: 1000 RVA: 0x0000FA84 File Offset: 0x0000DC84
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

		// Token: 0x060003E9 RID: 1001 RVA: 0x0000FAE8 File Offset: 0x0000DCE8
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

		// Token: 0x060003EA RID: 1002 RVA: 0x0000FC6C File Offset: 0x0000DE6C
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

		// Token: 0x060003EB RID: 1003 RVA: 0x0000FF14 File Offset: 0x0000E114
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

		// Token: 0x060003EC RID: 1004 RVA: 0x0000FF78 File Offset: 0x0000E178
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

		// Token: 0x060003ED RID: 1005 RVA: 0x0001020C File Offset: 0x0000E40C
		private static ISqlFragment HandleSpatialFromTextFunction(SqlGenerator sqlgen, DbFunctionExpression functionExpression)
		{
			string functionName = TypeSemantics.IsPrimitiveType(functionExpression.ResultType, PrimitiveTypeKind.Geometry) ? "geometry::STGeomFromText" : "geography::STGeomFromText";
			string functionName2 = TypeSemantics.IsPrimitiveType(functionExpression.ResultType, PrimitiveTypeKind.Geometry) ? "geometry::Parse" : "geography::Parse";
			if (functionExpression.Arguments.Count == 2)
			{
				return SqlFunctionCallHandler.HandleFunctionDefaultGivenName(sqlgen, functionExpression, functionName);
			}
			return SqlFunctionCallHandler.HandleFunctionDefaultGivenName(sqlgen, functionExpression, functionName2);
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x00010270 File Offset: 0x0000E470
		private static ISqlFragment HandleSpatialFromGmlFunction(SqlGenerator sqlgen, DbFunctionExpression functionExpression)
		{
			return SqlFunctionCallHandler.HandleSpatialStaticMethodFunctionAppendSrid(sqlgen, functionExpression, TypeSemantics.IsPrimitiveType(functionExpression.ResultType, PrimitiveTypeKind.Geometry) ? "geometry::GeomFromGml" : "geography::GeomFromGml");
		}

		// Token: 0x060003EF RID: 1007 RVA: 0x00010294 File Offset: 0x0000E494
		private static ISqlFragment HandleSpatialFromBinaryFunction(SqlGenerator sqlgen, DbFunctionExpression functionExpression)
		{
			return SqlFunctionCallHandler.HandleSpatialStaticMethodFunctionAppendSrid(sqlgen, functionExpression, TypeSemantics.IsPrimitiveType(functionExpression.ResultType, PrimitiveTypeKind.Geometry) ? "geometry::STGeomFromWKB" : "geography::STGeomFromWKB");
		}

		// Token: 0x060003F0 RID: 1008 RVA: 0x000102B8 File Offset: 0x0000E4B8
		private static ISqlFragment HandleSpatialStaticMethodFunctionAppendSrid(SqlGenerator sqlgen, DbFunctionExpression functionExpression, string functionName)
		{
			if (functionExpression.Arguments.Count == 2)
			{
				return SqlFunctionCallHandler.HandleFunctionDefaultGivenName(sqlgen, functionExpression, functionName);
			}
			DbExpression dbExpression = TypeSemantics.IsPrimitiveType(functionExpression.ResultType, PrimitiveTypeKind.Geometry) ? SqlFunctionCallHandler.defaultGeometrySridExpression : SqlFunctionCallHandler.defaultGeographySridExpression;
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.Append(functionName);
			SqlFunctionCallHandler.WriteFunctionArguments(sqlgen, functionExpression.Arguments.Concat(new DbExpression[]
			{
				dbExpression
			}), sqlBuilder);
			return sqlBuilder;
		}

		// Token: 0x060003F1 RID: 1009 RVA: 0x00010324 File Offset: 0x0000E524
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

		// Token: 0x060003F2 RID: 1010 RVA: 0x0001036B File Offset: 0x0000E56B
		private static bool IsSpecialStoreFunction(DbFunctionExpression e)
		{
			return SqlFunctionCallHandler.IsStoreFunction(e.Function) && SqlFunctionCallHandler._storeFunctionHandlers.ContainsKey(e.Function.Name);
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x00010391 File Offset: 0x0000E591
		private static bool IsSpecialCanonicalFunction(DbFunctionExpression e)
		{
			return TypeHelpers.IsCanonicalFunction(e.Function) && SqlFunctionCallHandler._canonicalFunctionHandlers.ContainsKey(e.Function.Name);
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x000103B8 File Offset: 0x0000E5B8
		private static bool IsSpatialCanonicalFunction(DbFunctionExpression e, out PrimitiveTypeKind spatialTypeKind)
		{
			if (TypeHelpers.IsCanonicalFunction(e.Function))
			{
				if (Helper.IsSpatialType(e.ResultType, out spatialTypeKind))
				{
					return true;
				}
				foreach (FunctionParameter functionParameter in e.Function.Parameters)
				{
					if (Helper.IsSpatialType(functionParameter.TypeUsage, out spatialTypeKind))
					{
						return true;
					}
				}
			}
			spatialTypeKind = PrimitiveTypeKind.Binary;
			return false;
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x00010440 File Offset: 0x0000E640
		private static ISqlFragment HandleFunctionDefault(SqlGenerator sqlgen, DbFunctionExpression e)
		{
			return SqlFunctionCallHandler.HandleFunctionDefaultGivenName(sqlgen, e, null);
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x0001044C File Offset: 0x0000E64C
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

		// Token: 0x060003F7 RID: 1015 RVA: 0x000104BC File Offset: 0x0000E6BC
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

		// Token: 0x060003F8 RID: 1016 RVA: 0x000104F8 File Offset: 0x0000E6F8
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

		// Token: 0x060003F9 RID: 1017 RVA: 0x00010544 File Offset: 0x0000E744
		private static void HandleFunctionArgumentsDefault(SqlGenerator sqlgen, DbFunctionExpression e, SqlBuilder result)
		{
			bool niladicFunctionAttribute = e.Function.NiladicFunctionAttribute;
			if (niladicFunctionAttribute && e.Arguments.Count > 0)
			{
				EntityUtil.Metadata(Strings.SqlGen_NiladicFunctionsCannotHaveParameters);
			}
			if (!niladicFunctionAttribute)
			{
				SqlFunctionCallHandler.WriteFunctionArguments(sqlgen, e.Arguments, result);
			}
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x0001058C File Offset: 0x0000E78C
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

		// Token: 0x060003FB RID: 1019 RVA: 0x00010604 File Offset: 0x0000E804
		private static ISqlFragment HandleFunctionGivenNameBasedOnVersion(SqlGenerator sqlgen, DbFunctionExpression e, string preKatmaiName, string katmaiName)
		{
			if (sqlgen.IsPreKatmai)
			{
				return SqlFunctionCallHandler.HandleFunctionDefaultGivenName(sqlgen, e, preKatmaiName);
			}
			return SqlFunctionCallHandler.HandleFunctionDefaultGivenName(sqlgen, e, katmaiName);
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x0001061F File Offset: 0x0000E81F
		private static ISqlFragment HandleSpecialStoreFunction(SqlGenerator sqlgen, DbFunctionExpression e)
		{
			return SqlFunctionCallHandler.HandleSpecialFunction(SqlFunctionCallHandler._storeFunctionHandlers, sqlgen, e);
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x0001062D File Offset: 0x0000E82D
		private static ISqlFragment HandleSpecialCanonicalFunction(SqlGenerator sqlgen, DbFunctionExpression e)
		{
			return SqlFunctionCallHandler.HandleSpecialFunction(SqlFunctionCallHandler._canonicalFunctionHandlers, sqlgen, e);
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x0001063B File Offset: 0x0000E83B
		private static ISqlFragment HandleSpecialFunction(Dictionary<string, SqlFunctionCallHandler.FunctionHandler> handlers, SqlGenerator sqlgen, DbFunctionExpression e)
		{
			return handlers[e.Function.Name](sqlgen, e);
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x00010655 File Offset: 0x0000E855
		private static ISqlFragment HandleSpatialCanonicalFunction(SqlGenerator sqlgen, DbFunctionExpression functionExpression, PrimitiveTypeKind spatialTypeKind)
		{
			if (spatialTypeKind == PrimitiveTypeKind.Geography)
			{
				return SqlFunctionCallHandler.HandleSpatialCanonicalFunction(sqlgen, functionExpression, SqlFunctionCallHandler._geographyFunctionNameToStaticMethodHandlerDictionary, SqlFunctionCallHandler._geographyFunctionNameToInstancePropertyNameDictionary, SqlFunctionCallHandler._geographyRenamedInstanceMethodFunctionDictionary);
			}
			return SqlFunctionCallHandler.HandleSpatialCanonicalFunction(sqlgen, functionExpression, SqlFunctionCallHandler._geometryFunctionNameToStaticMethodHandlerDictionary, SqlFunctionCallHandler._geometryFunctionNameToInstancePropertyNameDictionary, SqlFunctionCallHandler._geometryRenamedInstanceMethodFunctionDictionary);
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x0001068C File Offset: 0x0000E88C
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
				castReturnTypeTo = sqlgen.DefaultStringTypeName;
			}
			return SqlFunctionCallHandler.WriteInstanceFunctionCall(sqlgen, name, functionExpression, false, castReturnTypeTo);
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x00010719 File Offset: 0x0000E919
		private static ISqlFragment WriteInstanceFunctionCall(SqlGenerator sqlgen, string functionName, DbFunctionExpression functionExpression, bool isPropertyAccess)
		{
			return SqlFunctionCallHandler.WriteInstanceFunctionCall(sqlgen, functionName, functionExpression, isPropertyAccess, null);
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x00010728 File Offset: 0x0000E928
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

		// Token: 0x06000403 RID: 1027 RVA: 0x0001076C File Offset: 0x0000E96C
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

		// Token: 0x06000404 RID: 1028 RVA: 0x00010833 File Offset: 0x0000EA33
		private static ISqlFragment HandleConcatFunction(SqlGenerator sqlgen, DbFunctionExpression e)
		{
			return SqlFunctionCallHandler.HandleSpecialFunctionToOperator(sqlgen, e, false);
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x0001083D File Offset: 0x0000EA3D
		private static ISqlFragment HandleCanonicalFunctionBitwise(SqlGenerator sqlgen, DbFunctionExpression e)
		{
			return SqlFunctionCallHandler.HandleSpecialFunctionToOperator(sqlgen, e, true);
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x00010848 File Offset: 0x0000EA48
		private static ISqlFragment HandleDatepartDateFunction(SqlGenerator sqlgen, DbFunctionExpression e)
		{
			DbConstantExpression dbConstantExpression = e.Arguments[0] as DbConstantExpression;
			if (dbConstantExpression == null)
			{
				throw EntityUtil.InvalidOperation(Strings.SqlGen_InvalidDatePartArgumentExpression(e.Function.NamespaceName, e.Function.Name));
			}
			string text = dbConstantExpression.Value as string;
			if (text == null)
			{
				throw EntityUtil.InvalidOperation(Strings.SqlGen_InvalidDatePartArgumentExpression(e.Function.NamespaceName, e.Function.Name));
			}
			SqlBuilder sqlBuilder = new SqlBuilder();
			if (!SqlFunctionCallHandler._datepartKeywords.Contains(text))
			{
				throw EntityUtil.InvalidOperation(Strings.SqlGen_InvalidDatePartArgumentValue(text, e.Function.NamespaceName, e.Function.Name));
			}
			SqlFunctionCallHandler.WriteFunctionName(sqlBuilder, e.Function);
			sqlBuilder.Append("(");
			sqlBuilder.Append(text);
			string s = ", ";
			for (int i = 1; i < e.Arguments.Count; i++)
			{
				sqlBuilder.Append(s);
				sqlBuilder.Append(e.Arguments[i].Accept<ISqlFragment>(sqlgen));
			}
			sqlBuilder.Append(")");
			return sqlBuilder;
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x0001095A File Offset: 0x0000EB5A
		private static ISqlFragment HandleCanonicalFunctionDatepart(SqlGenerator sqlgen, DbFunctionExpression e)
		{
			return SqlFunctionCallHandler.HandleCanonicalFunctionDatepart(sqlgen, e.Function.Name.ToLowerInvariant(), e);
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x00010973 File Offset: 0x0000EB73
		private static ISqlFragment HandleCanonicalFunctionGetTotalOffsetMinutes(SqlGenerator sqlgen, DbFunctionExpression e)
		{
			return SqlFunctionCallHandler.HandleCanonicalFunctionDatepart(sqlgen, "tzoffset", e);
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x00010984 File Offset: 0x0000EB84
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

		// Token: 0x0600040A RID: 1034 RVA: 0x000109D8 File Offset: 0x0000EBD8
		private static ISqlFragment HandleCanonicalFunctionCurrentDateTime(SqlGenerator sqlgen, DbFunctionExpression e)
		{
			return SqlFunctionCallHandler.HandleFunctionGivenNameBasedOnVersion(sqlgen, e, "GetDate", "SysDateTime");
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x000109EB File Offset: 0x0000EBEB
		private static ISqlFragment HandleCanonicalFunctionCurrentUtcDateTime(SqlGenerator sqlgen, DbFunctionExpression e)
		{
			return SqlFunctionCallHandler.HandleFunctionGivenNameBasedOnVersion(sqlgen, e, "GetUtcDate", "SysUtcDateTime");
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x000109FE File Offset: 0x0000EBFE
		private static ISqlFragment HandleCanonicalFunctionCurrentDateTimeOffset(SqlGenerator sqlgen, DbFunctionExpression e)
		{
			sqlgen.AssertKatmaiOrNewer(e);
			return SqlFunctionCallHandler.HandleFunctionDefaultGivenName(sqlgen, e, "SysDateTimeOffset");
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x00010A14 File Offset: 0x0000EC14
		private static ISqlFragment HandleCanonicalFunctionCreateDateTime(SqlGenerator sqlgen, DbFunctionExpression e)
		{
			string typeName = sqlgen.IsPreKatmai ? "datetime" : "datetime2";
			return SqlFunctionCallHandler.HandleCanonicalFunctionDateTimeTypeCreation(sqlgen, typeName, e.Arguments, true, false);
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x00010A45 File Offset: 0x0000EC45
		private static ISqlFragment HandleCanonicalFunctionCreateDateTimeOffset(SqlGenerator sqlgen, DbFunctionExpression e)
		{
			sqlgen.AssertKatmaiOrNewer(e);
			return SqlFunctionCallHandler.HandleCanonicalFunctionDateTimeTypeCreation(sqlgen, "datetimeoffset", e.Arguments, true, true);
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x00010A61 File Offset: 0x0000EC61
		private static ISqlFragment HandleCanonicalFunctionCreateTime(SqlGenerator sqlgen, DbFunctionExpression e)
		{
			sqlgen.AssertKatmaiOrNewer(e);
			return SqlFunctionCallHandler.HandleCanonicalFunctionDateTimeTypeCreation(sqlgen, "time", e.Arguments, false, false);
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x00010A80 File Offset: 0x0000EC80
		private static ISqlFragment HandleCanonicalFunctionDateTimeTypeCreation(SqlGenerator sqlgen, string typeName, IList<DbExpression> args, bool hasDatePart, bool hasTimeZonePart)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			int index = 0;
			if (!sqlgen.IsPreKatmai && hasDatePart)
			{
				sqlBuilder.Append("DATEADD(year, ");
				sqlgen.ParenthesizeExpressionIfNeeded(args[index++], sqlBuilder);
				sqlBuilder.Append(" - 1, ");
			}
			sqlBuilder.Append("convert (");
			sqlBuilder.Append(typeName);
			sqlBuilder.Append(",");
			if (hasDatePart)
			{
				if (!sqlgen.IsPreKatmai)
				{
					sqlBuilder.Append("'0001'");
				}
				else
				{
					SqlFunctionCallHandler.AppendConvertToVarchar(sqlgen, sqlBuilder, args[index++]);
				}
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
			if (!sqlgen.IsPreKatmai && hasDatePart)
			{
				sqlBuilder.Append(")");
			}
			return sqlBuilder;
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x00010C45 File Offset: 0x0000EE45
		private static void AppendConvertToVarchar(SqlGenerator sqlgen, SqlBuilder result, DbExpression e)
		{
			result.Append("convert(varchar(255), ");
			result.Append(e.Accept<ISqlFragment>(sqlgen));
			result.Append(")");
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x00010C6C File Offset: 0x0000EE6C
		private static ISqlFragment HandleCanonicalFunctionTruncateTime(SqlGenerator sqlgen, DbFunctionExpression e)
		{
			string s = null;
			bool flag = false;
			PrimitiveTypeKind primitiveTypeKind;
			bool flag2 = TypeHelpers.TryGetPrimitiveTypeKind(e.Arguments[0].ResultType, out primitiveTypeKind);
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

		// Token: 0x06000413 RID: 1043 RVA: 0x00010D4C File Offset: 0x0000EF4C
		private static ISqlFragment HandleCanonicalFunctionDateAddKatmaiOrNewer(SqlGenerator sqlgen, DbFunctionExpression e)
		{
			sqlgen.AssertKatmaiOrNewer(e);
			return SqlFunctionCallHandler.HandleCanonicalFunctionDateAdd(sqlgen, e);
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x00010D5C File Offset: 0x0000EF5C
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

		// Token: 0x06000415 RID: 1045 RVA: 0x00010DE7 File Offset: 0x0000EFE7
		private static ISqlFragment HandleCanonicalFunctionDateDiffKatmaiOrNewer(SqlGenerator sqlgen, DbFunctionExpression e)
		{
			sqlgen.AssertKatmaiOrNewer(e);
			return SqlFunctionCallHandler.HandleCanonicalFunctionDateDiff(sqlgen, e);
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x00010DF8 File Offset: 0x0000EFF8
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

		// Token: 0x06000417 RID: 1047 RVA: 0x00010E83 File Offset: 0x0000F083
		private static ISqlFragment HandleCanonicalFunctionIndexOf(SqlGenerator sqlgen, DbFunctionExpression e)
		{
			return SqlFunctionCallHandler.HandleFunctionDefaultGivenName(sqlgen, e, "CHARINDEX");
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x00010E91 File Offset: 0x0000F091
		private static ISqlFragment HandleCanonicalFunctionNewGuid(SqlGenerator sqlgen, DbFunctionExpression e)
		{
			return SqlFunctionCallHandler.HandleFunctionDefaultGivenName(sqlgen, e, "NEWID");
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x00010E9F File Offset: 0x0000F09F
		private static ISqlFragment HandleCanonicalFunctionLength(SqlGenerator sqlgen, DbFunctionExpression e)
		{
			return SqlFunctionCallHandler.HandleFunctionDefaultGivenName(sqlgen, e, "LEN");
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x00010EAD File Offset: 0x0000F0AD
		private static ISqlFragment HandleCanonicalFunctionRound(SqlGenerator sqlgen, DbFunctionExpression e)
		{
			return SqlFunctionCallHandler.HandleCanonicalFunctionRoundOrTruncate(sqlgen, e, true);
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x00010EB7 File Offset: 0x0000F0B7
		private static ISqlFragment HandleCanonicalFunctionTruncate(SqlGenerator sqlgen, DbFunctionExpression e)
		{
			return SqlFunctionCallHandler.HandleCanonicalFunctionRoundOrTruncate(sqlgen, e, false);
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x00010EC4 File Offset: 0x0000F0C4
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

		// Token: 0x0600041D RID: 1053 RVA: 0x00010F88 File Offset: 0x0000F188
		private static ISqlFragment HandleCanonicalFunctionAbs(SqlGenerator sqlgen, DbFunctionExpression e)
		{
			if (TypeSemantics.IsPrimitiveType(e.Arguments[0].ResultType, PrimitiveTypeKind.Byte))
			{
				SqlBuilder sqlBuilder = new SqlBuilder();
				sqlBuilder.Append(e.Arguments[0].Accept<ISqlFragment>(sqlgen));
				return sqlBuilder;
			}
			return SqlFunctionCallHandler.HandleFunctionDefault(sqlgen, e);
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x00010FD8 File Offset: 0x0000F1D8
		private static ISqlFragment HandleCanonicalFunctionTrim(SqlGenerator sqlgen, DbFunctionExpression e)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.Append("LTRIM(RTRIM(");
			sqlBuilder.Append(e.Arguments[0].Accept<ISqlFragment>(sqlgen));
			sqlBuilder.Append("))");
			return sqlBuilder;
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x0001101A File Offset: 0x0000F21A
		private static ISqlFragment HandleCanonicalFunctionToLower(SqlGenerator sqlgen, DbFunctionExpression e)
		{
			return SqlFunctionCallHandler.HandleFunctionDefaultGivenName(sqlgen, e, "LOWER");
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x00011028 File Offset: 0x0000F228
		private static ISqlFragment HandleCanonicalFunctionToUpper(SqlGenerator sqlgen, DbFunctionExpression e)
		{
			return SqlFunctionCallHandler.HandleFunctionDefaultGivenName(sqlgen, e, "UPPER");
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x00011038 File Offset: 0x0000F238
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
			DbConstantExpression dbConstantExpression = new DbConstantExpression(constSearchParamExpression.ResultType, stringBuilder.ToString());
			result.Append(dbConstantExpression.Accept<ISqlFragment>(sqlgen));
			if (flag)
			{
				result.Append(" ESCAPE '~'");
			}
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x000110CA File Offset: 0x0000F2CA
		private static ISqlFragment HandleCanonicalFunctionContains(SqlGenerator sqlgen, DbFunctionExpression e)
		{
			return SqlFunctionCallHandler.WrapPredicate(new Func<SqlGenerator, IList<DbExpression>, SqlBuilder, SqlBuilder>(SqlFunctionCallHandler.HandleCanonicalFunctionContains), sqlgen, e);
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x000110E0 File Offset: 0x0000F2E0
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

		// Token: 0x06000424 RID: 1060 RVA: 0x0001116A File Offset: 0x0000F36A
		private static ISqlFragment HandleCanonicalFunctionStartsWith(SqlGenerator sqlgen, DbFunctionExpression e)
		{
			return SqlFunctionCallHandler.WrapPredicate(new Func<SqlGenerator, IList<DbExpression>, SqlBuilder, SqlBuilder>(SqlFunctionCallHandler.HandleCanonicalFunctionStartsWith), sqlgen, e);
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x00011180 File Offset: 0x0000F380
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

		// Token: 0x06000426 RID: 1062 RVA: 0x0001120A File Offset: 0x0000F40A
		private static ISqlFragment HandleCanonicalFunctionEndsWith(SqlGenerator sqlgen, DbFunctionExpression e)
		{
			return SqlFunctionCallHandler.WrapPredicate(new Func<SqlGenerator, IList<DbExpression>, SqlBuilder, SqlBuilder>(SqlFunctionCallHandler.HandleCanonicalFunctionEndsWith), sqlgen, e);
		}

		// Token: 0x06000427 RID: 1063 RVA: 0x00011220 File Offset: 0x0000F420
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

		// Token: 0x06000428 RID: 1064 RVA: 0x000112BC File Offset: 0x0000F4BC
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

		// Token: 0x06000429 RID: 1065 RVA: 0x00011310 File Offset: 0x0000F510
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
			if (TypeHelpers.IsCanonicalFunction(function))
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

		// Token: 0x0600042A RID: 1066 RVA: 0x000113A2 File Offset: 0x0000F5A2
		internal static bool IsStoreFunction(EdmFunction function)
		{
			return function.BuiltInAttribute && !TypeHelpers.IsCanonicalFunction(function);
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x000113B7 File Offset: 0x0000F5B7
		private static bool CastReturnTypeToInt64(DbFunctionExpression e)
		{
			return SqlFunctionCallHandler.CastReturnTypeToGivenType(e, SqlFunctionCallHandler._functionRequiresReturnTypeCastToInt64, PrimitiveTypeKind.Int64);
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x000113C8 File Offset: 0x0000F5C8
		private static bool CastReturnTypeToInt32(SqlGenerator sqlgen, DbFunctionExpression e)
		{
			if (!SqlFunctionCallHandler._functionRequiresReturnTypeCastToInt32.Contains(e.Function.FullName))
			{
				return false;
			}
			for (int i = 0; i < e.Arguments.Count; i++)
			{
				TypeUsage storeType = sqlgen.StoreItemCollection.StoreProviderManifest.GetStoreType(e.Arguments[i].ResultType);
				if (SqlFunctionCallHandler._maxTypeNames.Contains(storeType.EdmType.Name))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x00011440 File Offset: 0x0000F640
		private static bool CastReturnTypeToInt16(DbFunctionExpression e)
		{
			return SqlFunctionCallHandler.CastReturnTypeToGivenType(e, SqlFunctionCallHandler._functionRequiresReturnTypeCastToInt16, PrimitiveTypeKind.Int16);
		}

		// Token: 0x0600042E RID: 1070 RVA: 0x0001144F File Offset: 0x0000F64F
		private static bool CastReturnTypeToSingle(DbFunctionExpression e)
		{
			return SqlFunctionCallHandler.CastReturnTypeToGivenType(e, SqlFunctionCallHandler._functionRequiresReturnTypeCastToSingle, PrimitiveTypeKind.Single);
		}

		// Token: 0x0600042F RID: 1071 RVA: 0x00011460 File Offset: 0x0000F660
		private static bool CastReturnTypeToGivenType(DbFunctionExpression e, Set<string> functionsRequiringReturnTypeCast, PrimitiveTypeKind type)
		{
			if (!functionsRequiringReturnTypeCast.Contains(e.Function.FullName))
			{
				return false;
			}
			for (int i = 0; i < e.Arguments.Count; i++)
			{
				if (TypeSemantics.IsPrimitiveType(e.Arguments[i].ResultType, type))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x040006F3 RID: 1779
		private static readonly Dictionary<string, SqlFunctionCallHandler.FunctionHandler> _storeFunctionHandlers = SqlFunctionCallHandler.InitializeStoreFunctionHandlers();

		// Token: 0x040006F4 RID: 1780
		private static readonly Dictionary<string, SqlFunctionCallHandler.FunctionHandler> _canonicalFunctionHandlers = SqlFunctionCallHandler.InitializeCanonicalFunctionHandlers();

		// Token: 0x040006F5 RID: 1781
		private static readonly Dictionary<string, string> _functionNameToOperatorDictionary = SqlFunctionCallHandler.InitializeFunctionNameToOperatorDictionary();

		// Token: 0x040006F6 RID: 1782
		private static readonly Dictionary<string, string> _dateAddFunctionNameToDatepartDictionary = SqlFunctionCallHandler.InitializeDateAddFunctionNameToDatepartDictionary();

		// Token: 0x040006F7 RID: 1783
		private static readonly Dictionary<string, string> _dateDiffFunctionNameToDatepartDictionary = SqlFunctionCallHandler.InitializeDateDiffFunctionNameToDatepartDictionary();

		// Token: 0x040006F8 RID: 1784
		private static readonly Dictionary<string, SqlFunctionCallHandler.FunctionHandler> _geographyFunctionNameToStaticMethodHandlerDictionary = SqlFunctionCallHandler.InitializeGeographyStaticMethodFunctionsDictionary();

		// Token: 0x040006F9 RID: 1785
		private static readonly Dictionary<string, string> _geographyFunctionNameToInstancePropertyNameDictionary = SqlFunctionCallHandler.InitializeGeographyInstancePropertyFunctionsDictionary();

		// Token: 0x040006FA RID: 1786
		private static readonly Dictionary<string, string> _geographyRenamedInstanceMethodFunctionDictionary = SqlFunctionCallHandler.InitializeRenamedGeographyInstanceMethodFunctions();

		// Token: 0x040006FB RID: 1787
		private static readonly Dictionary<string, SqlFunctionCallHandler.FunctionHandler> _geometryFunctionNameToStaticMethodHandlerDictionary = SqlFunctionCallHandler.InitializeGeometryStaticMethodFunctionsDictionary();

		// Token: 0x040006FC RID: 1788
		private static readonly Dictionary<string, string> _geometryFunctionNameToInstancePropertyNameDictionary = SqlFunctionCallHandler.InitializeGeometryInstancePropertyFunctionsDictionary();

		// Token: 0x040006FD RID: 1789
		private static readonly Dictionary<string, string> _geometryRenamedInstanceMethodFunctionDictionary = SqlFunctionCallHandler.InitializeRenamedGeometryInstanceMethodFunctions();

		// Token: 0x040006FE RID: 1790
		private static readonly Set<string> _datepartKeywords = new Set<string>(new string[]
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
		}, StringComparer.OrdinalIgnoreCase).MakeReadOnly();

		// Token: 0x040006FF RID: 1791
		private static readonly Set<string> _functionRequiresReturnTypeCastToInt64 = new Set<string>(new string[]
		{
			"SqlServer.CHARINDEX"
		}, StringComparer.Ordinal).MakeReadOnly();

		// Token: 0x04000700 RID: 1792
		private static readonly Set<string> _functionRequiresReturnTypeCastToInt32 = new Set<string>(new string[]
		{
			"SqlServer.LEN",
			"SqlServer.PATINDEX",
			"SqlServer.DATALENGTH",
			"SqlServer.CHARINDEX",
			"Edm.IndexOf",
			"Edm.Length"
		}, StringComparer.Ordinal).MakeReadOnly();

		// Token: 0x04000701 RID: 1793
		private static readonly Set<string> _functionRequiresReturnTypeCastToInt16 = new Set<string>(new string[]
		{
			"Edm.Abs"
		}, StringComparer.Ordinal).MakeReadOnly();

		// Token: 0x04000702 RID: 1794
		private static readonly Set<string> _functionRequiresReturnTypeCastToSingle = new Set<string>(new string[]
		{
			"Edm.Abs",
			"Edm.Round",
			"Edm.Floor",
			"Edm.Ceiling"
		}, StringComparer.Ordinal).MakeReadOnly();

		// Token: 0x04000703 RID: 1795
		private static readonly Set<string> _maxTypeNames = new Set<string>(new string[]
		{
			"varchar(max)",
			"nvarchar(max)",
			"text",
			"ntext",
			"varbinary(max)",
			"image",
			"xml"
		}, StringComparer.Ordinal).MakeReadOnly();

		// Token: 0x04000704 RID: 1796
		private static readonly DbExpression defaultGeographySridExpression = DbExpressionBuilder.Constant(DbGeography.DefaultCoordinateSystemId);

		// Token: 0x04000705 RID: 1797
		private static readonly DbExpression defaultGeometrySridExpression = DbExpressionBuilder.Constant(DbGeometry.DefaultCoordinateSystemId);

		// Token: 0x02000450 RID: 1104
		// (Invoke) Token: 0x06003A68 RID: 14952
		private delegate ISqlFragment FunctionHandler(SqlGenerator sqlgen, DbFunctionExpression functionExpr);
	}
}
