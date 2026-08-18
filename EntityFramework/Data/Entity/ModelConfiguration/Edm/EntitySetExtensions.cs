using System;
using System.Data.Entity.Core.Metadata.Edm;

namespace System.Data.Entity.ModelConfiguration.Edm
{
	// Token: 0x02000815 RID: 2069
	internal static class EntitySetExtensions
	{
		// Token: 0x06005CFA RID: 23802 RVA: 0x001913F3 File Offset: 0x0018F5F3
		public static object GetConfiguration(this EntitySet entitySet)
		{
			return entitySet.Annotations.GetConfiguration();
		}

		// Token: 0x06005CFB RID: 23803 RVA: 0x00191400 File Offset: 0x0018F600
		public static void SetConfiguration(this EntitySet entitySet, object configuration)
		{
			entitySet.GetMetadataProperties().SetConfiguration(configuration);
		}
	}
}
