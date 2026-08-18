using System;
using System.Collections.Generic;
using TechnoPro.Common.ICore.MarkedForDeletion;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;

namespace TechnoPro.Common.Core.MarkedForDeletion
{
	// Token: 0x020000B9 RID: 185
	public class MarkedForDeletionArchiveManager : IMarkedForDeletionArchiveManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060006FB RID: 1787 RVA: 0x0000672B File Offset: 0x0000492B
		public MarkedForDeletionArchiveManager()
		{
		}

		// Token: 0x060006FC RID: 1788 RVA: 0x000291EB File Offset: 0x000273EB
		public MarkedForDeletionArchiveManager(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x060006FD RID: 1789 RVA: 0x000291FD File Offset: 0x000273FD
		// (set) Token: 0x060006FE RID: 1790 RVA: 0x00029205 File Offset: 0x00027405
		public OperationContext OpContext { get; set; }

		// Token: 0x060006FF RID: 1791 RVA: 0x00003940 File Offset: 0x00001B40
		public void MoveExamFilesToArchives(IList<int> examIds)
		{
		}
	}
}
