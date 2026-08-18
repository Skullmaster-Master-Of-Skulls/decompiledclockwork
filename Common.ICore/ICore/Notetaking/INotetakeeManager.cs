using System;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Notetaking.Notetakee;

namespace TechnoPro.Common.ICore.Notetaking
{
	// Token: 0x02000059 RID: 89
	public interface INotetakeeManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000266 RID: 614
		NotetakeeStudentCourseRegistrations LoadNotetakeeCourseRegistrations(int NotetakeePersonId, DateTime StartDate, DateTime EndDate);
	}
}
