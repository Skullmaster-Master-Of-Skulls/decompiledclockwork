using System;
using System.Collections.Generic;
using TechnoPro.Common.DAO.Impl.MarkedForDeletion;
using TechnoPro.Common.DAO.MarkedForDeletion;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.MarkedForDeletion;

namespace TechnoPro.Common.Core.MarkedForDeletion
{
	// Token: 0x020000BC RID: 188
	public class MarkedForDeletionJobManager
	{
		// Token: 0x0600070B RID: 1803 RVA: 0x0002937A File Offset: 0x0002757A
		public MarkedForDeletionJobManager(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x0600070C RID: 1804 RVA: 0x0000672B File Offset: 0x0000492B
		public MarkedForDeletionJobManager()
		{
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x0600070D RID: 1805 RVA: 0x0002938C File Offset: 0x0002758C
		// (set) Token: 0x0600070E RID: 1806 RVA: 0x00029394 File Offset: 0x00027594
		public OperationContext OpContext { get; set; }

		// Token: 0x0600070F RID: 1807 RVA: 0x000293A0 File Offset: 0x000275A0
		public IList<MarkedForDeletionJob> LoadAllJobs()
		{
			IMarkedForDeletionJobDAO markedForDeletionJobDAO = new MarkedForDeletionJobDAO(this.OpContext);
			return markedForDeletionJobDAO.LoadAllJobs();
		}

		// Token: 0x06000710 RID: 1808 RVA: 0x000293C4 File Offset: 0x000275C4
		public IList<MarkedForDeletionJob> LoadJobsByType(params eMarkedForDeletionType[] types)
		{
			IMarkedForDeletionJobDAO markedForDeletionJobDAO = new MarkedForDeletionJobDAO(this.OpContext);
			return markedForDeletionJobDAO.LoadJobsByType(types);
		}

		// Token: 0x06000711 RID: 1809 RVA: 0x000293EC File Offset: 0x000275EC
		public void UpdateJob(MarkedForDeletionJob job)
		{
			IMarkedForDeletionJobDAO markedForDeletionJobDAO = new MarkedForDeletionJobDAO(this.OpContext);
			markedForDeletionJobDAO.UpdateJob(job);
		}

		// Token: 0x06000712 RID: 1810 RVA: 0x00029410 File Offset: 0x00027610
		public void EnableJob(string markedForDeletionJobId)
		{
			IMarkedForDeletionJobDAO markedForDeletionJobDAO = new MarkedForDeletionJobDAO(this.OpContext);
			markedForDeletionJobDAO.EnableJob(markedForDeletionJobId);
		}

		// Token: 0x06000713 RID: 1811 RVA: 0x00029434 File Offset: 0x00027634
		public void DisableJob(string markedForDeletionJobId)
		{
			IMarkedForDeletionJobDAO markedForDeletionJobDAO = new MarkedForDeletionJobDAO(this.OpContext);
			markedForDeletionJobDAO.DisableJob(markedForDeletionJobId);
		}

		// Token: 0x06000714 RID: 1812 RVA: 0x00029458 File Offset: 0x00027658
		public void ExecuteAllActiveMarkedForDeletionJobs()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000715 RID: 1813 RVA: 0x000072EA File Offset: 0x000054EA
		private IList<MarkedForDeletionItem> FigureOutNewItemsThatShouldBeMarkedForDeletionNow(MarkedForDeletionJob job)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000716 RID: 1814 RVA: 0x000072EA File Offset: 0x000054EA
		public void ExecuteMarkedForDeletionJob(string markedForDeletionId)
		{
			throw new NotImplementedException();
		}
	}
}
