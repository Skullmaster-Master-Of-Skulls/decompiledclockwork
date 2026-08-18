using System;
using System.Threading.Tasks;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Communications;

namespace TechnoPro.Common.ICore.Communications
{
	// Token: 0x020000D5 RID: 213
	public interface ICommunicationsSentManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060006A0 RID: 1696
		StudentCommunicationHistory LoadStudentCommunicationHistory(int studentPersonId);

		// Token: 0x060006A1 RID: 1697
		Task<StudentCommunicationHistory> LoadStudentCommunicationHistoryAsync(int studentPersonId);

		// Token: 0x060006A2 RID: 1698
		int AddCommicationSendAttempt(CommunicationBase sendAttempt);

		// Token: 0x060006A3 RID: 1699
		Task<int> AddCommicationSendAttemptAsync(CommunicationBase sendAttempt);
	}
}
