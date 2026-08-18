using System;
using System.Data.Entity;

namespace System.Data.Spatial.Internal
{
	// Token: 0x020002DF RID: 735
	internal static class SpatialExceptions
	{
		// Token: 0x06002C46 RID: 11334 RVA: 0x000A861F File Offset: 0x000A681F
		internal static ArgumentNullException ArgumentNull(string argumentName)
		{
			return EntityUtil.ArgumentNull(argumentName);
		}

		// Token: 0x06002C47 RID: 11335 RVA: 0x000A8627 File Offset: 0x000A6827
		internal static Exception ProviderValueNotCompatibleWithSpatialServices()
		{
			return EntityUtil.Argument(Strings.Spatial_ProviderValueNotCompatibleWithSpatialServices, "providerValue");
		}

		// Token: 0x06002C48 RID: 11336 RVA: 0x000A8638 File Offset: 0x000A6838
		internal static InvalidOperationException WellKnownValueSerializationPropertyNotDirectlySettable()
		{
			return EntityUtil.InvalidOperation(Strings.Spatial_WellKnownValueSerializationPropertyNotDirectlySettable);
		}

		// Token: 0x06002C49 RID: 11337 RVA: 0x000A8644 File Offset: 0x000A6844
		internal static Exception GeographyValueNotCompatibleWithSpatialServices(string argumentName)
		{
			return EntityUtil.Argument(Strings.Spatial_GeographyValueNotCompatibleWithSpatialServices, argumentName);
		}

		// Token: 0x06002C4A RID: 11338 RVA: 0x000A8651 File Offset: 0x000A6851
		internal static Exception WellKnownGeographyValueNotValid(string argumentName)
		{
			return EntityUtil.Argument(Strings.Spatial_WellKnownGeographyValueNotValid, argumentName);
		}

		// Token: 0x06002C4B RID: 11339 RVA: 0x000A865E File Offset: 0x000A685E
		internal static Exception CouldNotCreateWellKnownGeographyValueNoSrid(string argumentName)
		{
			return EntityUtil.Argument(Strings.SqlSpatialservices_CouldNotCreateWellKnownGeographyValueNoSrid, argumentName);
		}

		// Token: 0x06002C4C RID: 11340 RVA: 0x000A866B File Offset: 0x000A686B
		internal static Exception CouldNotCreateWellKnownGeographyValueNoWkbOrWkt(string argumentName)
		{
			return EntityUtil.Argument(Strings.SqlSpatialservices_CouldNotCreateWellKnownGeographyValueNoWkbOrWkt, argumentName);
		}

		// Token: 0x06002C4D RID: 11341 RVA: 0x000A8678 File Offset: 0x000A6878
		internal static Exception GeometryValueNotCompatibleWithSpatialServices(string argumentName)
		{
			return EntityUtil.Argument(Strings.Spatial_GeometryValueNotCompatibleWithSpatialServices, argumentName);
		}

		// Token: 0x06002C4E RID: 11342 RVA: 0x000A8685 File Offset: 0x000A6885
		internal static Exception WellKnownGeometryValueNotValid(string argumentName)
		{
			throw EntityUtil.Argument(Strings.Spatial_WellKnownGeometryValueNotValid, argumentName);
		}

		// Token: 0x06002C4F RID: 11343 RVA: 0x000A8692 File Offset: 0x000A6892
		internal static Exception CouldNotCreateWellKnownGeometryValueNoSrid(string argumentName)
		{
			return EntityUtil.Argument(Strings.SqlSpatialservices_CouldNotCreateWellKnownGeometryValueNoSrid, argumentName);
		}

		// Token: 0x06002C50 RID: 11344 RVA: 0x000A869F File Offset: 0x000A689F
		internal static Exception CouldNotCreateWellKnownGeometryValueNoWkbOrWkt(string argumentName)
		{
			return EntityUtil.Argument(Strings.SqlSpatialservices_CouldNotCreateWellKnownGeometryValueNoWkbOrWkt, argumentName);
		}

		// Token: 0x06002C51 RID: 11345 RVA: 0x000A86AC File Offset: 0x000A68AC
		internal static Exception SqlSpatialServices_ProviderValueNotSqlType(Type requiredType)
		{
			return EntityUtil.Argument(Strings.SqlSpatialServices_ProviderValueNotSqlType(requiredType.AssemblyQualifiedName), "providerValue");
		}
	}
}
