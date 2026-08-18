using System;
using TechnoPro.Common.Public.Entities.MarkedForDeletion;
using TechnoPro.Common.Public.Entities.MarkedForDeletion.JobResults;

namespace TechnoPro.Common.Core.MarkedForDeletion.MarkedForDeletionImplementations
{
	// Token: 0x020000BD RID: 189
	public interface IMarkedForDeletion
	{
		// Token: 0x06000717 RID: 1815
		MarkItemsForDeletionResult FigureOutNewItemsToBeMarkedForDeletion(MarkedForDeletionJob job, bool runInProductionMode);

		// Token: 0x06000718 RID: 1816
		MoveItemsToTempResult MoveMarkedItemsToTemp(MarkedForDeletionJob job, bool runInProductionMode);

		// Token: 0x06000719 RID: 1817
		DeleteItemsFromTempResult DeleteTempItems(MarkedForDeletionJob job, bool runInProductionMode);
	}
}
