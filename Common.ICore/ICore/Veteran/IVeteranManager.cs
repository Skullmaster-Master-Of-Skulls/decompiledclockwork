using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Veteran;

namespace TechnoPro.Common.ICore.Veteran
{
	// Token: 0x02000013 RID: 19
	public interface IVeteranManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000074 RID: 116
		IList<ChangeInBenefitRequest> LoadChangeInBenefitRequests(int PersonId, DateTime StartDate, DateTime EndDate);

		// Token: 0x06000075 RID: 117
		BenefitApplication LoadBenefitApplicationByStudentAndSemester(int PersonId, int SemesterId);

		// Token: 0x06000076 RID: 118
		VeteranChapter LoadChapterByStudent(int PersonId);
	}
}
