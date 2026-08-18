using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Tutoring;

namespace TechnoPro.Common.DAO.Tutoring
{
	// Token: 0x0200001F RID: 31
	public interface IStudentTuteeDAO : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x0600006A RID: 106
		IList<MyTutor> GetStudentMyTutors(int StudentPersonId, DateTime? StartDate, DateTime? EndDate);

		// Token: 0x0600006B RID: 107
		bool GetIsStudentAuthorizedToUseTutoring(int studentPersonId, int studentIsAuthorizedCid);
	}
}
