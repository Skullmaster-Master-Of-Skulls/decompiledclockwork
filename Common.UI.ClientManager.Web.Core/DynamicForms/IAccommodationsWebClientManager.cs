using System;
using TechnoPro.Common.UI.Web.Entity.DynamicForms.Accommodations;

namespace TechnoPro.Common.UI.ClientManager.Web.Core.DynamicForms
{
	// Token: 0x02000013 RID: 19
	public interface IAccommodationsWebClientManager
	{
		// Token: 0x06000040 RID: 64
		AccommodationsExpiryDate GetStudentAccommodationsExpiryDate(int PersonId, bool useCache = false);

		// Token: 0x06000041 RID: 65
		bool AreAccommodationsCurrentlyExpired(int PersonId, bool useCache = false);

		// Token: 0x06000042 RID: 66
		bool AreAccommodationsCurrentlyExpired(int PersonId, DateTime DateToUseInsteadOfToday, bool useCache = false);

		// Token: 0x06000043 RID: 67
		DateTime? GetEffectiveExpiryDate(int PersonId, bool useCache = false);

		// Token: 0x06000044 RID: 68
		DateTime? GetEffectiveExpiryDate(AccommodationsExpiryDate ExpiryDate);
	}
}
