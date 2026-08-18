using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Communications;

namespace TechnoPro.Common.DAO.Communications
{
	// Token: 0x02000098 RID: 152
	public interface ICommunicationsSentDAO : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060003EB RID: 1003
		Task<IList<Communication>> LoadCommunicationsForUserAsync(int personId);

		// Token: 0x060003EC RID: 1004
		IList<Communication> LoadCommunicationsForUser(int personId);

		// Token: 0x060003ED RID: 1005
		int AddCommicationSendAttempt(CommunicationBase sendAttempt);

		// Token: 0x060003EE RID: 1006
		Task<int> AddCommicationSendAttemptAsync(CommunicationBase sendAttempt);
	}
}
