using System;
using System.Data.Common;
using System.Data.Entity.Core;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data.Entity.Spatial
{
	// Token: 0x02000721 RID: 1825
	internal static class SpatialHelpers
	{
		// Token: 0x06004B18 RID: 19224 RVA: 0x00160D94 File Offset: 0x0015EF94
		internal static object GetSpatialValue(MetadataWorkspace workspace, DbDataReader reader, TypeUsage columnType, int columnOrdinal)
		{
			DbSpatialDataReader dbSpatialDataReader = SpatialHelpers.CreateSpatialDataReader(workspace, reader);
			if (Helper.IsGeographicType((PrimitiveType)columnType.EdmType))
			{
				return dbSpatialDataReader.GetGeography(columnOrdinal);
			}
			return dbSpatialDataReader.GetGeometry(columnOrdinal);
		}

		// Token: 0x06004B19 RID: 19225 RVA: 0x00160F90 File Offset: 0x0015F190
		internal static async Task<object> GetSpatialValueAsync(MetadataWorkspace workspace, DbDataReader reader, TypeUsage columnType, int columnOrdinal, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			DbSpatialDataReader spatialReader = SpatialHelpers.CreateSpatialDataReader(workspace, reader);
			object result;
			if (Helper.IsGeographicType((PrimitiveType)columnType.EdmType))
			{
				result = await spatialReader.GetGeographyAsync(columnOrdinal, cancellationToken).WithCurrentCulture<DbGeography>();
			}
			else
			{
				result = await spatialReader.GetGeometryAsync(columnOrdinal, cancellationToken).WithCurrentCulture<DbGeometry>();
			}
			return result;
		}

		// Token: 0x06004B1A RID: 19226 RVA: 0x00160FF8 File Offset: 0x0015F1F8
		internal static DbSpatialDataReader CreateSpatialDataReader(MetadataWorkspace workspace, DbDataReader reader)
		{
			StoreItemCollection storeItemCollection = (StoreItemCollection)workspace.GetItemCollection(DataSpace.SSpace);
			DbProviderFactory providerFactory = storeItemCollection.ProviderFactory;
			DbProviderServices providerServices = providerFactory.GetProviderServices();
			DbSpatialDataReader spatialDataReader = providerServices.GetSpatialDataReader(reader, storeItemCollection.ProviderManifestToken);
			if (spatialDataReader == null)
			{
				throw new ProviderIncompatibleException(Strings.ProviderDidNotReturnSpatialServices);
			}
			return spatialDataReader;
		}
	}
}
