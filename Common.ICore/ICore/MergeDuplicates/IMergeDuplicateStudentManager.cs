using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.MergeDuplicates.Students;

namespace TechnoPro.Common.ICore.MergeDuplicates
{
	// Token: 0x0200005F RID: 95
	public interface IMergeDuplicateStudentManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000296 RID: 662
		DuplicateStudentSet LoadDuplicateStudentPreviewInfo(DuplicateStudentSet DuplicateSet);

		// Token: 0x06000297 RID: 663
		IList<PotentialDuplicateStudentSet> FindPotentialDuplicateStudents(int GroupId);

		// Token: 0x06000298 RID: 664
		void MergeDuplicateStudents(DuplicateStudentSet DuplicateStudentSet);
	}
}
