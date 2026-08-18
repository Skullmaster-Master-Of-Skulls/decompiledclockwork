using System;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Legacy.ServiceProviders;
using TechnoPro.Common.Public.Entities.ServiceProvidersOriginal;

namespace TechnoPro.Common.ICore.Legacy
{
	// Token: 0x02000077 RID: 119
	public interface ILegacyServiceProviderManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000352 RID: 850
		LegacyRequestDetailNotesAndSpecialInstructions LoadRequestDetailNotesAndSpecialInstructions(int RequestId);

		// Token: 0x06000353 RID: 851
		void UpdateRequest(LegacyServiceProviderRequestDetail RequestDetail);

		// Token: 0x06000354 RID: 852
		void UpdateRequestDetailNotesAndSpecialInstructions(LegacyRequestDetailNotesAndSpecialInstructions notesAndSpecialInstructions);

		// Token: 0x06000355 RID: 853
		void UpdateRequestNotes(int RequestId, string notes);

		// Token: 0x06000356 RID: 854
		void UpdateProvider(ServiceProvider provider);

		// Token: 0x06000357 RID: 855
		int CreateProvider(ServiceProvider provider);

		// Token: 0x06000358 RID: 856
		ServiceProvider LoadProvider(int serviceProviderId);

		// Token: 0x06000359 RID: 857
		int LoadProviderIdByStudentNumber(string snum);
	}
}
