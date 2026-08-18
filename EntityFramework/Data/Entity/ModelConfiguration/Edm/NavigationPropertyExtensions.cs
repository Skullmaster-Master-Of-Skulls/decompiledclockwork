using System;
using System.Data.Entity.Core.Metadata.Edm;

namespace System.Data.Entity.ModelConfiguration.Edm
{
	// Token: 0x02000818 RID: 2072
	internal static class NavigationPropertyExtensions
	{
		// Token: 0x06005D3B RID: 23867 RVA: 0x0019278C File Offset: 0x0019098C
		public static object GetConfiguration(this NavigationProperty navigationProperty)
		{
			return navigationProperty.Annotations.GetConfiguration();
		}

		// Token: 0x06005D3C RID: 23868 RVA: 0x00192799 File Offset: 0x00190999
		public static void SetConfiguration(this NavigationProperty navigationProperty, object configuration)
		{
			navigationProperty.GetMetadataProperties().SetConfiguration(configuration);
		}

		// Token: 0x06005D3D RID: 23869 RVA: 0x001927A7 File Offset: 0x001909A7
		public static AssociationEndMember GetFromEnd(this NavigationProperty navProp)
		{
			if (navProp.Association.SourceEnd != navProp.ResultEnd)
			{
				return navProp.Association.SourceEnd;
			}
			return navProp.Association.TargetEnd;
		}
	}
}
