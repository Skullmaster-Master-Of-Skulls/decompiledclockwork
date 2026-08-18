using System;
using System.Data.Entity.Core.Mapping;
using System.Diagnostics.CodeAnalysis;

namespace System.Data.Entity.ModelConfiguration.Edm
{
	// Token: 0x0200080C RID: 2060
	internal static class StorageAssociationSetMappingExtensions
	{
		// Token: 0x06005CAD RID: 23725 RVA: 0x001901C7 File Offset: 0x0018E3C7
		public static AssociationSetMapping Initialize(this AssociationSetMapping associationSetMapping)
		{
			associationSetMapping.SourceEndMapping = new EndPropertyMapping();
			associationSetMapping.TargetEndMapping = new EndPropertyMapping();
			return associationSetMapping;
		}

		// Token: 0x06005CAE RID: 23726 RVA: 0x001901E0 File Offset: 0x0018E3E0
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		public static object GetConfiguration(this AssociationSetMapping associationSetMapping)
		{
			return associationSetMapping.Annotations.GetConfiguration();
		}

		// Token: 0x06005CAF RID: 23727 RVA: 0x001901ED File Offset: 0x0018E3ED
		public static void SetConfiguration(this AssociationSetMapping associationSetMapping, object configuration)
		{
			associationSetMapping.Annotations.SetConfiguration(configuration);
		}
	}
}
