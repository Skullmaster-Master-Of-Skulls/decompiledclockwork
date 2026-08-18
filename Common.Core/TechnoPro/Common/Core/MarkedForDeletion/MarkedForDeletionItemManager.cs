using System;
using System.Collections.Generic;
using TechnoPro.Common.DAO.Impl.MarkedForDeletion;
using TechnoPro.Common.DAO.MarkedForDeletion;
using TechnoPro.Common.ICore.MarkedForDeletion;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.MarkedForDeletion;
using TechnoPro.Common.Public.Entities.MarkedForDeletion.JobResults;

namespace TechnoPro.Common.Core.MarkedForDeletion
{
	// Token: 0x020000BB RID: 187
	public class MarkedForDeletionItemManager : IMarkedForDeletionItemManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000701 RID: 1793 RVA: 0x0000672B File Offset: 0x0000492B
		public MarkedForDeletionItemManager()
		{
		}

		// Token: 0x06000702 RID: 1794 RVA: 0x00029274 File Offset: 0x00027474
		public MarkedForDeletionItemManager(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x06000703 RID: 1795 RVA: 0x00029286 File Offset: 0x00027486
		// (set) Token: 0x06000704 RID: 1796 RVA: 0x0002928E File Offset: 0x0002748E
		public OperationContext OpContext { get; set; }

		// Token: 0x06000705 RID: 1797 RVA: 0x00029298 File Offset: 0x00027498
		public IList<MarkItemForDeletionResult> MarkItemsForDeletion(bool inProductionMode, eMarkedForDeletionType markedForDeletionType, IList<string> ids)
		{
			IMarkedForDeletionItemDAO markedForDeletionItemDAO = new MarkedForDeletionItemDAO(this.OpContext);
			return markedForDeletionItemDAO.MarkItemsForDeletion(inProductionMode, markedForDeletionType, ids);
		}

		// Token: 0x06000706 RID: 1798 RVA: 0x000292C0 File Offset: 0x000274C0
		public void ExemptMarkedForDeletionItem(string markedForDeletionId)
		{
			IMarkedForDeletionItemDAO markedForDeletionItemDAO = new MarkedForDeletionItemDAO(this.OpContext);
			markedForDeletionItemDAO.ExemptMarkedForDeletionItem(markedForDeletionId);
		}

		// Token: 0x06000707 RID: 1799 RVA: 0x000292E4 File Offset: 0x000274E4
		public void UnExemptMarkedForDeletionItem(string markedForDeletionId)
		{
			IMarkedForDeletionItemDAO markedForDeletionItemDAO = new MarkedForDeletionItemDAO(this.OpContext);
			markedForDeletionItemDAO.UnExemptMarkedForDeletionItem(markedForDeletionId);
		}

		// Token: 0x06000708 RID: 1800 RVA: 0x00029308 File Offset: 0x00027508
		public MarkedForDeletionItem LoadMarkedForDeletionItemById(string markedForDeletionId)
		{
			IMarkedForDeletionItemDAO markedForDeletionItemDAO = new MarkedForDeletionItemDAO(this.OpContext);
			return markedForDeletionItemDAO.LoadMarkedForDeletionItemById(markedForDeletionId);
		}

		// Token: 0x06000709 RID: 1801 RVA: 0x00029330 File Offset: 0x00027530
		public void DeleteMarkedForDeletionItemById(string markedForDeletionId)
		{
			IMarkedForDeletionItemDAO markedForDeletionItemDAO = new MarkedForDeletionItemDAO(this.OpContext);
			markedForDeletionItemDAO.DeleteMarkedForDeletionItemById(markedForDeletionId);
		}

		// Token: 0x0600070A RID: 1802 RVA: 0x00029354 File Offset: 0x00027554
		public IList<MarkedForDeletionItem> LoadMarkedForDeletionItemsByType(eMarkedForDeletionType type, bool includeExempt)
		{
			IMarkedForDeletionItemDAO markedForDeletionItemDAO = new MarkedForDeletionItemDAO(this.OpContext);
			return markedForDeletionItemDAO.LoadMarkedForDeletionItemsByType(type, includeExempt);
		}
	}
}
