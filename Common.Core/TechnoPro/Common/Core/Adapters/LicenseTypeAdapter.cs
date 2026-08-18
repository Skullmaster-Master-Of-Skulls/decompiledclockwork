using System;
using TechnoPro.Common.Public.Entities;

namespace TechnoPro.Common.Core.Adapters
{
	// Token: 0x02000173 RID: 371
	public static class LicenseTypeAdapter
	{
		// Token: 0x06001042 RID: 4162 RVA: 0x00077D08 File Offset: 0x00075F08
		public static bool DoesExpire(this LicenseType licType)
		{
			bool result;
			switch (licType)
			{
			case LicenseType.Demo:
			case LicenseType.Production:
			case LicenseType.Development:
			case LicenseType.Beta:
				result = false;
				break;
			case LicenseType.Trial:
			case LicenseType.SupportPlan:
				result = true;
				break;
			default:
				result = true;
				break;
			}
			return result;
		}
	}
}
