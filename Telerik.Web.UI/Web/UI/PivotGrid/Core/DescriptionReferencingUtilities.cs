using System;

namespace Telerik.Web.UI.PivotGrid.Core
{
	// Token: 0x020006A0 RID: 1696
	internal static class DescriptionReferencingUtilities
	{
		// Token: 0x06003D3C RID: 15676 RVA: 0x000C55A4 File Offset: 0x000C37A4
		public static T TrackReferencesOrNull<T>(T descriptionTracking, IDescriptionIndexMap map) where T : class
		{
			IDescriptionsReferencing descriptionsReferencing = descriptionTracking as IDescriptionsReferencing;
			if (descriptionsReferencing != null && !descriptionsReferencing.TrackDescriptions(map))
			{
				return default(T);
			}
			return descriptionTracking;
		}
	}
}
