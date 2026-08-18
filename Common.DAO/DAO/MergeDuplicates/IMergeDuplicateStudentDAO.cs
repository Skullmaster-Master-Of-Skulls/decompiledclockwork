using System;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.MergeDuplicates.Students;

namespace TechnoPro.Common.DAO.MergeDuplicates
{
	// Token: 0x0200004E RID: 78
	public interface IMergeDuplicateStudentDAO : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060001B4 RID: 436
		void MergeDuplicateStudents(DuplicateStudentSet DuplicateStudentSet);
	}
}
