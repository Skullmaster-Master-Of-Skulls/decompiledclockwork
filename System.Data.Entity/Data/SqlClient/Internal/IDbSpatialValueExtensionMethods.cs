using System;
using System.Data.Spatial;

namespace System.Data.SqlClient.Internal
{
	// Token: 0x0200003F RID: 63
	internal static class IDbSpatialValueExtensionMethods
	{
		// Token: 0x06000558 RID: 1368 RVA: 0x0001788F File Offset: 0x00015A8F
		internal static IDbSpatialValue AsSpatialValue(this DbGeography geographyValue)
		{
			if (geographyValue == null)
			{
				return null;
			}
			return new DbGeographyAdapter(geographyValue);
		}

		// Token: 0x06000559 RID: 1369 RVA: 0x000178A1 File Offset: 0x00015AA1
		internal static IDbSpatialValue AsSpatialValue(this DbGeometry geometryValue)
		{
			if (geometryValue == null)
			{
				return null;
			}
			return new DbGeometryAdapter(geometryValue);
		}
	}
}
