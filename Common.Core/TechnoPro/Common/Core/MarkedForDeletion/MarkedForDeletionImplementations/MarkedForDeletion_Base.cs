using System;
using System.Collections.Generic;
using TechnoPro.Common.ICore.MarkedForDeletion;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.MarkedForDeletion;
using TechnoPro.Common.Public.Entities.MarkedForDeletion.JobResults;

namespace TechnoPro.Common.Core.MarkedForDeletion.MarkedForDeletionImplementations
{
	// Token: 0x020000C8 RID: 200
	public class MarkedForDeletion_Base
	{
		// Token: 0x06000727 RID: 1831 RVA: 0x0000672B File Offset: 0x0000492B
		public MarkedForDeletion_Base()
		{
		}

		// Token: 0x06000728 RID: 1832 RVA: 0x000295B3 File Offset: 0x000277B3
		public MarkedForDeletion_Base(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x06000729 RID: 1833 RVA: 0x000295C5 File Offset: 0x000277C5
		// (set) Token: 0x0600072A RID: 1834 RVA: 0x000295CD File Offset: 0x000277CD
		public OperationContext OpContext { get; set; }

		// Token: 0x0600072B RID: 1835 RVA: 0x000295D8 File Offset: 0x000277D8
		public MarkItemsForDeletionResult MarkItemsForDeletion(MarkedForDeletionJob job, IList<string> ids, bool runInProductionMode)
		{
			bool flag = ids == null;
			MarkItemsForDeletionResult result;
			if (flag)
			{
				result = new MarkItemsForDeletionResult
				{
					ErrorMessage = "Ids list was empty"
				};
			}
			else
			{
				IMarkedForDeletionItemManager markedForDeletionItemManager = new MarkedForDeletionItemManager(this.OpContext);
				IList<MarkItemForDeletionResult> list = markedForDeletionItemManager.MarkItemsForDeletion(runInProductionMode, job.MarkedForDeletionType, ids);
				bool flag2 = list == null;
				if (flag2)
				{
					result = new MarkItemsForDeletionResult
					{
						ErrorMessage = "Ids list was not empty but results are empty"
					};
				}
				else
				{
					result = new MarkItemsForDeletionResult
					{
						WasSuccessful = true,
						Items = list
					};
				}
			}
			return result;
		}
	}
}
