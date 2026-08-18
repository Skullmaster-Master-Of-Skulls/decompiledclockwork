using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;

namespace TechnoPro.Common.ICore.MarkedForDeletion
{
	// Token: 0x02000063 RID: 99
	public interface IMarkedForDeletionArchiveManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060002B6 RID: 694
		void MoveExamFilesToArchives(IList<int> examIds);
	}
}
