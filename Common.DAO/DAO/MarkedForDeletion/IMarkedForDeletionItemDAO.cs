using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.MarkedForDeletion;
using TechnoPro.Common.Public.Entities.MarkedForDeletion.JobResults;

namespace TechnoPro.Common.DAO.MarkedForDeletion
{
	// Token: 0x02000051 RID: 81
	public interface IMarkedForDeletionItemDAO : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060001C0 RID: 448
		IList<MarkItemForDeletionResult> MarkItemsForDeletion(bool inProductionMode, eMarkedForDeletionType markedForDeletionType, IList<string> ids);

		// Token: 0x060001C1 RID: 449
		void ExemptMarkedForDeletionItem(string markedForDeletionId);

		// Token: 0x060001C2 RID: 450
		void UnExemptMarkedForDeletionItem(string markedForDeletionId);

		// Token: 0x060001C3 RID: 451
		MarkedForDeletionItem LoadMarkedForDeletionItemById(string markedForDeletionId);

		// Token: 0x060001C4 RID: 452
		void DeleteMarkedForDeletionItemById(string markedForDeletionId);

		// Token: 0x060001C5 RID: 453
		IList<MarkedForDeletionItem> LoadMarkedForDeletionItemsByType(eMarkedForDeletionType type, bool includeExempt);
	}
}
