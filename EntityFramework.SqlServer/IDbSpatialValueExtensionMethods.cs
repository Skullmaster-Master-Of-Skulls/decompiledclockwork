using System;
using System.Data.Entity.Spatial;

namespace System.Data.Entity.SqlServer
{
	// Token: 0x02000013 RID: 19
	internal static class IDbSpatialValueExtensionMethods
	{
		// Token: 0x060000BA RID: 186 RVA: 0x00004633 File Offset: 0x00002833
		internal static IDbSpatialValue AsSpatialValue(this DbGeography geographyValue)
		{
			return new DbGeographyAdapter(geographyValue);
		}

		// Token: 0x060000BB RID: 187 RVA: 0x0000463B File Offset: 0x0000283B
		internal static IDbSpatialValue AsSpatialValue(this DbGeometry geometryValue)
		{
			return new DbGeometryAdapter(geometryValue);
		}
	}
}
