using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.MarkedForDeletion;

namespace TechnoPro.Common.DAO.MarkedForDeletion
{
	// Token: 0x02000052 RID: 82
	public interface IMarkedForDeletionJobDAO : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060001C6 RID: 454
		IList<MarkedForDeletionJob> LoadAllJobs();

		// Token: 0x060001C7 RID: 455
		IList<MarkedForDeletionJob> LoadJobsByType(params eMarkedForDeletionType[] types);

		// Token: 0x060001C8 RID: 456
		void UpdateJob(MarkedForDeletionJob job);

		// Token: 0x060001C9 RID: 457
		void EnableJob(string markedForDeletionJobId);

		// Token: 0x060001CA RID: 458
		void DisableJob(string markedForDeletionJobId);
	}
}
