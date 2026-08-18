using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.ServiceProvider;

namespace TechnoPro.Common.DAO.ServiceProvider
{
	// Token: 0x02000034 RID: 52
	public interface IServiceProviderLookupDAO : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060000D0 RID: 208
		SPRequestStatusType LoadRequestStatusTypeById(int SPRequestStatusTypeId);

		// Token: 0x060000D1 RID: 209
		IList<SPRequestStatusType> LoadActiveRequestStatusTypes();

		// Token: 0x060000D2 RID: 210
		void DeleteRequestStatusType(int SPRequestStatusTypeId);

		// Token: 0x060000D3 RID: 211
		void UpdateRequestStatusType(SPRequestStatusType StatusType);

		// Token: 0x060000D4 RID: 212
		int CreateRequestStatusType(SPRequestStatusType StatusType);

		// Token: 0x060000D5 RID: 213
		SPRequestAssignmentStatusType LoadRequestAssignmentStatusTypeById(int SPRequestAssignmentStatusTypeId);

		// Token: 0x060000D6 RID: 214
		IList<SPRequestAssignmentStatusType> LoadActiveRequestAssignmentStatusTypes();

		// Token: 0x060000D7 RID: 215
		void DeleteRequestAssignmentStatusType(int SPRequestAssignmentStatusTypeId);

		// Token: 0x060000D8 RID: 216
		void UpdateRequestAssignmentStatusType(SPRequestAssignmentStatusType AssignmentStatusType);

		// Token: 0x060000D9 RID: 217
		int CreateRequestAssignmentStatusType(SPRequestAssignmentStatusType AssignmentStatusType);

		// Token: 0x060000DA RID: 218
		SPUrgencyLevelType LoadUrgencyLevelTypeById(int SPUrgencyLevelTypeId);

		// Token: 0x060000DB RID: 219
		IList<SPUrgencyLevelType> LoadActiveUrgencyLevelTypes();

		// Token: 0x060000DC RID: 220
		void DeleteUrgencyLevelStatusType(int SPUrgencyLevelTypeId);

		// Token: 0x060000DD RID: 221
		void UpdateUrgencyLevelStatusType(SPUrgencyLevelType UrgencyLevelType);

		// Token: 0x060000DE RID: 222
		int CreateUrgencyLevelStatusType(SPUrgencyLevelType UrgencyLevelType);
	}
}
