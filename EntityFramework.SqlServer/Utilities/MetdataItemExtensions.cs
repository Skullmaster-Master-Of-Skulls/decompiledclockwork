using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;

namespace System.Data.Entity.SqlServer.Utilities
{
	// Token: 0x0200002D RID: 45
	internal static class MetdataItemExtensions
	{
		// Token: 0x060002A3 RID: 675 RVA: 0x0000B848 File Offset: 0x00009A48
		public static T GetMetadataPropertyValue<T>(this MetadataItem item, string propertyName)
		{
			MetadataProperty metadataProperty = item.MetadataProperties.FirstOrDefault((MetadataProperty p) => p.Name == propertyName);
			if (metadataProperty != null)
			{
				return (T)((object)metadataProperty.Value);
			}
			return default(T);
		}
	}
}
