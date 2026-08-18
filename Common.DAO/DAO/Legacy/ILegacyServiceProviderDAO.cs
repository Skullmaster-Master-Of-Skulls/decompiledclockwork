using System;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Legacy.ServiceProviders;
using TechnoPro.Common.Public.Entities.ServiceProvidersOriginal;

namespace TechnoPro.Common.DAO.Legacy
{
	// Token: 0x02000062 RID: 98
	public interface ILegacyServiceProviderDAO : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000239 RID: 569
		LegacyRequestDetailNotesAndSpecialInstructions LoadRequestDetailNotesAndSpecialInstructions(int RequestId);

		// Token: 0x0600023A RID: 570
		void UpdateRequest(LegacyServiceProviderRequestDetail RequestDetail);

		// Token: 0x0600023B RID: 571
		void UpdateRequestDetailNotesAndSpecialInstructions(LegacyRequestDetailNotesAndSpecialInstructions notesAndSpecialInstructions);

		// Token: 0x0600023C RID: 572
		void UpdateRequestNotes(int RequestId, string notes);

		// Token: 0x0600023D RID: 573
		void UpdateProvider(ServiceProvider provider);

		// Token: 0x0600023E RID: 574
		int CreateProvider(ServiceProvider provider);

		// Token: 0x0600023F RID: 575
		ServiceProvider LoadProvider(int serviceProviderId);

		// Token: 0x06000240 RID: 576
		int LoadProviderIdByStudentNumber(string snum);
	}
}
