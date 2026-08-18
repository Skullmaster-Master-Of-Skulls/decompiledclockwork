using System;
using System.Data.Entity.Spatial;
using System.Data.Entity.SqlServer.Resources;
using System.Diagnostics.CodeAnalysis;

namespace System.Data.Entity.SqlServer
{
	// Token: 0x02000026 RID: 38
	public static class SqlSpatialFunctions
	{
		// Token: 0x06000234 RID: 564 RVA: 0x0000A943 File Offset: 0x00008B43
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "latitude")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "spatialReferenceId")]
		[DbFunction("SqlServer", "POINTGEOGRAPHY")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "longitude")]
		public static DbGeography PointGeography(double? latitude, double? longitude, int? spatialReferenceId)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06000235 RID: 565 RVA: 0x0000A94F File Offset: 0x00008B4F
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "geographyValue")]
		[DbFunction("SqlServer", "ASTEXTZM")]
		public static string AsTextZM(DbGeography geographyValue)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06000236 RID: 566 RVA: 0x0000A95B File Offset: 0x00008B5B
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "tolerance")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "distance")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "relative")]
		[DbFunction("SqlServer", "BUFFERWITHTOLERANCE")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "geographyValue")]
		public static DbGeography BufferWithTolerance(DbGeography geographyValue, double? distance, double? tolerance, bool? relative)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06000237 RID: 567 RVA: 0x0000A967 File Offset: 0x00008B67
		[DbFunction("SqlServer", "ENVELOPEANGLE")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "geographyValue")]
		public static double? EnvelopeAngle(DbGeography geographyValue)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06000238 RID: 568 RVA: 0x0000A973 File Offset: 0x00008B73
		[DbFunction("SqlServer", "ENVELOPECENTER")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "geographyValue")]
		public static DbGeography EnvelopeCenter(DbGeography geographyValue)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06000239 RID: 569 RVA: 0x0000A97F File Offset: 0x00008B7F
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "geographyOther")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "geographyValue")]
		[DbFunction("SqlServer", "FILTER")]
		public static bool? Filter(DbGeography geographyValue, DbGeography geographyOther)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x0600023A RID: 570 RVA: 0x0000A98B File Offset: 0x00008B8B
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "geometryTypeName")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "geographyValue")]
		[DbFunction("SqlServer", "INSTANCEOF")]
		public static bool? InstanceOf(DbGeography geographyValue, string geometryTypeName)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x0600023B RID: 571 RVA: 0x0000A997 File Offset: 0x00008B97
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Num", Justification = "Naming convention prescribed by OGC specification")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "geographyValue")]
		[DbFunction("SqlServer", "NUMRINGS")]
		public static int? NumRings(DbGeography geographyValue)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x0600023C RID: 572 RVA: 0x0000A9A3 File Offset: 0x00008BA3
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "tolerance")]
		[DbFunction("SqlServer", "REDUCE")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "geographyValue")]
		public static DbGeography Reduce(DbGeography geographyValue, double? tolerance)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x0600023D RID: 573 RVA: 0x0000A9AF File Offset: 0x00008BAF
		[DbFunction("SqlServer", "RINGN")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "index")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "geographyValue")]
		public static DbGeography RingN(DbGeography geographyValue, int? index)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x0600023E RID: 574 RVA: 0x0000A9BB File Offset: 0x00008BBB
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "xCoordinate")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "yCoordinate")]
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "y", Justification = "Naming convention prescribed by OGC specification")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "spatialReferenceId")]
		[DbFunction("SqlServer", "POINTGEOMETRY")]
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "x", Justification = "Naming convention prescribed by OGC specification")]
		public static DbGeometry PointGeometry(double? xCoordinate, double? yCoordinate, int? spatialReferenceId)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x0600023F RID: 575 RVA: 0x0000A9C7 File Offset: 0x00008BC7
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "geometryValue")]
		[DbFunction("SqlServer", "ASTEXTZM")]
		public static string AsTextZM(DbGeometry geometryValue)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06000240 RID: 576 RVA: 0x0000A9D3 File Offset: 0x00008BD3
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "distance")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "geometryValue")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "relative")]
		[DbFunction("SqlServer", "BUFFERWITHTOLERANCE")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "tolerance")]
		public static DbGeometry BufferWithTolerance(DbGeometry geometryValue, double? distance, double? tolerance, bool? relative)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06000241 RID: 577 RVA: 0x0000A9DF File Offset: 0x00008BDF
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "geometryValue")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "geometryTypeName")]
		[DbFunction("SqlServer", "INSTANCEOF")]
		public static bool? InstanceOf(DbGeometry geometryValue, string geometryTypeName)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06000242 RID: 578 RVA: 0x0000A9EB File Offset: 0x00008BEB
		[DbFunction("SqlServer", "FILTER")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "geometryValue")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "geometryOther")]
		public static bool? Filter(DbGeometry geometryValue, DbGeometry geometryOther)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06000243 RID: 579 RVA: 0x0000A9F7 File Offset: 0x00008BF7
		[DbFunction("SqlServer", "MAKEVALID")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "geometryValue")]
		public static DbGeometry MakeValid(DbGeometry geometryValue)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06000244 RID: 580 RVA: 0x0000AA03 File Offset: 0x00008C03
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "tolerance")]
		[DbFunction("SqlServer", "REDUCE")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "geometryValue")]
		public static DbGeometry Reduce(DbGeometry geometryValue, double? tolerance)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}
	}
}
