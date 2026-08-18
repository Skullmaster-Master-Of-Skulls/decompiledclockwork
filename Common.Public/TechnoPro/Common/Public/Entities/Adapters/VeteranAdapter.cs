using System;
using TechnoPro.Common.Public.Adapters;
using TechnoPro.Common.Public.Entities.Veteran;

namespace TechnoPro.Common.Public.Entities.Adapters
{
	// Token: 0x020005D0 RID: 1488
	public static class VeteranAdapter
	{
		// Token: 0x06002FDC RID: 12252 RVA: 0x0003AED8 File Offset: 0x000390D8
		public static string GetTitleForDisplay(this eVeteranRequestStatus status)
		{
			VeteranRequestStatusAttribute attribute = status.GetAttribute<VeteranRequestStatusAttribute>();
			return string.IsNullOrEmpty((attribute != null) ? attribute.DisplayTitle : null) ? status.ToString() : attribute.DisplayTitle;
		}
	}
}
