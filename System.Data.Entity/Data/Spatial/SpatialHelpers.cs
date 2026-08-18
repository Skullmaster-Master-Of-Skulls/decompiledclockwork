using System;
using System.Data.Common;
using System.Data.Metadata.Edm;

namespace System.Data.Spatial
{
	// Token: 0x020002DD RID: 733
	internal static class SpatialHelpers
	{
		// Token: 0x06002C41 RID: 11329 RVA: 0x000A85B0 File Offset: 0x000A67B0
		internal static object GetSpatialValue(MetadataWorkspace workspace, DbDataReader reader, TypeUsage columnType, int columnOrdinal)
		{
			DbSpatialDataReader dbSpatialDataReader = SpatialHelpers.CreateSpatialDataReader(workspace, reader);
			if (Helper.IsGeographicType((PrimitiveType)columnType.EdmType))
			{
				return dbSpatialDataReader.GetGeography(columnOrdinal);
			}
			return dbSpatialDataReader.GetGeometry(columnOrdinal);
		}

		// Token: 0x06002C42 RID: 11330 RVA: 0x000A85E8 File Offset: 0x000A67E8
		internal static DbSpatialDataReader CreateSpatialDataReader(MetadataWorkspace workspace, DbDataReader reader)
		{
			StoreItemCollection storeItemCollection = (StoreItemCollection)workspace.GetItemCollection(DataSpace.SSpace);
			DbProviderFactory storeProviderFactory = storeItemCollection.StoreProviderFactory;
			DbProviderServices providerServices = DbProviderServices.GetProviderServices(storeProviderFactory);
			return providerServices.GetSpatialDataReader(reader, storeItemCollection.StoreProviderManifestToken);
		}
	}
}
