using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.MarkedForDeletion;
using TechnoPro.Common.Public.Entities.MarkedForDeletion.JobResults;

namespace TechnoPro.Common.ICore.MarkedForDeletion
{
	// Token: 0x02000064 RID: 100
	public interface IMarkedForDeletionItemManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060002B7 RID: 695
		IList<MarkItemForDeletionResult> MarkItemsForDeletion(bool inProductionMode, eMarkedForDeletionType markedForDeletionType, IList<string> ids);

		// Token: 0x060002B8 RID: 696
		void ExemptMarkedForDeletionItem(string markedForDeletionId);

		// Token: 0x060002B9 RID: 697
		void UnExemptMarkedForDeletionItem(string markedForDeletionId);

		// Token: 0x060002BA RID: 698
		MarkedForDeletionItem LoadMarkedForDeletionItemById(string markedForDeletionId);

		// Token: 0x060002BB RID: 699
		void DeleteMarkedForDeletionItemById(string markedForDeletionId);

		// Token: 0x060002BC RID: 700
		IList<MarkedForDeletionItem> LoadMarkedForDeletionItemsByType(eMarkedForDeletionType type, bool includeExempt);
	}
}
