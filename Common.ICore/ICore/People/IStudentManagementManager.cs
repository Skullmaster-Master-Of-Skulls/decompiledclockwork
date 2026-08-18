using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.ICore.People
{
	// Token: 0x02000053 RID: 83
	public interface IStudentManagementManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000206 RID: 518
		StudentSummary LoadStudentSummary(int PersonId);

		// Token: 0x06000207 RID: 519
		IList<PersonBase> LoadActiveStudents(DateTime StartDate, DateTime EndDate);

		// Token: 0x06000208 RID: 520
		IList<PersonBase> PermanentlyDeleteStudents(IList<int> StudentPersonIds);

		// Token: 0x06000209 RID: 521
		string LoadStudentNumber(int PersonId);
	}
}
