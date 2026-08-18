using System;
using TechnoPro.ClockWorkServer.Contracts.DTO.Legacy.ServiceProvider;
using TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProviderOriginal;
using TechnoPro.Common.Public;

namespace TechnoPro.Common.ClientManager.ICore.Legacy
{
	// Token: 0x02000046 RID: 70
	public interface ILegacyServiceProviderClientManager : IWebService
	{
		// Token: 0x060001EF RID: 495
		LegacyRequestDetailNotesAndSpecialInstructionsDTO LoadRequestDetailNotesAndSpecialInstructions(int RequestId);

		// Token: 0x060001F0 RID: 496
		void UpdateRequestDetailNotesAndSpecialInstructions(LegacyRequestDetailNotesAndSpecialInstructionsDTO notesAndSpecialInstructions);

		// Token: 0x060001F1 RID: 497
		void UpdateRequest(LegacyServiceProviderRequestDetailDTO RequestDetail);

		// Token: 0x060001F2 RID: 498
		void UpdateRequestNotes(int RequestId, string notes);

		// Token: 0x060001F3 RID: 499
		void UpdateProvider(ServiceProviderDTO provider);

		// Token: 0x060001F4 RID: 500
		int CreateProvider(ServiceProviderDTO provider);

		// Token: 0x060001F5 RID: 501
		ServiceProviderDTO LoadProvider(int serviceProviderId);

		// Token: 0x060001F6 RID: 502
		int LoadProviderIdByStudentNumber(string snum);
	}
}
