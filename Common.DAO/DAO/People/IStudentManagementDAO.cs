using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.DAO.People
{
	// Token: 0x02000043 RID: 67
	public interface IStudentManagementDAO : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000139 RID: 313
		IList<PersonBase> LoadActiveStudents(DateTime StartDate, DateTime EndDate);

		// Token: 0x0600013A RID: 314
		IList<PersonBase> PermanentlyDeleteStudents(IList<PersonBase> StudentsToDelete);

		// Token: 0x0600013B RID: 315
		string LoadStudentNumber(int PersonId);
	}
}
