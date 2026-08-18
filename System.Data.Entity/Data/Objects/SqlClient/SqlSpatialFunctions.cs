using System;
using System.Data.Entity;
using System.Data.Objects.DataClasses;
using System.Data.Spatial;

namespace System.Data.Objects.SqlClient
{
	// Token: 0x0200015D RID: 349
	public static class SqlSpatialFunctions
	{
		// Token: 0x06001A30 RID: 6704 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("SqlServer", "POINTGEOGRAPHY")]
		public static DbGeography PointGeography(double? latitude, double? longitude, int? spatialReferenceId)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x06001A31 RID: 6705 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("SqlServer", "ASTEXTZM")]
		public static string AsTextZM(DbGeography geographyValue)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x06001A32 RID: 6706 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("SqlServer", "BUFFERWITHTOLERANCE")]
		public static DbGeography BufferWithTolerance(DbGeography geographyValue, double? distance, double? tolerance, bool? relative)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x06001A33 RID: 6707 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("SqlServer", "ENVELOPEANGLE")]
		public static double? EnvelopeAngle(DbGeography geographyValue)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x06001A34 RID: 6708 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("SqlServer", "ENVELOPECENTER")]
		public static DbGeography EnvelopeCenter(DbGeography geographyValue)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x06001A35 RID: 6709 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("SqlServer", "FILTER")]
		public static bool? Filter(DbGeography geographyValue, DbGeography geographyOther)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x06001A36 RID: 6710 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("SqlServer", "INSTANCEOF")]
		public static bool? InstanceOf(DbGeography geographyValue, string geometryTypeName)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x06001A37 RID: 6711 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("SqlServer", "NUMRINGS")]
		public static int? NumRings(DbGeography geographyValue)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x06001A38 RID: 6712 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("SqlServer", "REDUCE")]
		public static DbGeography Reduce(DbGeography geographyValue, double? tolerance)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x06001A39 RID: 6713 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("SqlServer", "RINGN")]
		public static DbGeography RingN(DbGeography geographyValue, int? index)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x06001A3A RID: 6714 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("SqlServer", "POINTGEOMETRY")]
		public static DbGeometry PointGeometry(double? xCoordinate, double? yCoordinate, int? spatialReferenceId)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x06001A3B RID: 6715 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("SqlServer", "ASTEXTZM")]
		public static string AsTextZM(DbGeometry geometryValue)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x06001A3C RID: 6716 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("SqlServer", "BUFFERWITHTOLERANCE")]
		public static DbGeometry BufferWithTolerance(DbGeometry geometryValue, double? distance, double? tolerance, bool? relative)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x06001A3D RID: 6717 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("SqlServer", "INSTANCEOF")]
		public static bool? InstanceOf(DbGeometry geometryValue, string geometryTypeName)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x06001A3E RID: 6718 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("SqlServer", "FILTER")]
		public static bool? Filter(DbGeometry geometryValue, DbGeometry geometryOther)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x06001A3F RID: 6719 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("SqlServer", "MAKEVALID")]
		public static DbGeometry MakeValid(DbGeometry geometryValue)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}

		// Token: 0x06001A40 RID: 6720 RVA: 0x00049342 File Offset: 0x00047542
		[EdmFunction("SqlServer", "REDUCE")]
		public static DbGeometry Reduce(DbGeometry geometryValue, double? tolerance)
		{
			throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionDirectCall);
		}
	}
}
