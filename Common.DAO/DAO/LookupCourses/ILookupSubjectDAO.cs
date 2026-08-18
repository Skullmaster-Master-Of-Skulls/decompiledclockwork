using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.LookupCourses;

namespace TechnoPro.Common.DAO.LookupCourses
{
	// Token: 0x02000059 RID: 89
	public interface ILookupSubjectDAO : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000210 RID: 528
		LookupSubject LoadLookupSubject(int SubjectId);

		// Token: 0x06000211 RID: 529
		void SaveSubject(LookupSubject subject);

		// Token: 0x06000212 RID: 530
		LookupSubject LoadLookupSubjectBySubjectCode(string SubjectCode);

		// Token: 0x06000213 RID: 531
		LookupSubject LoadLookupSubjectBySubjectDescription(string SubjectDescription);

		// Token: 0x06000214 RID: 532
		LookupSubject LoadLookupSubject(string SubjectCode, string SubjectDescription);

		// Token: 0x06000215 RID: 533
		List<LookupSubject> LoadLookupSubjectsBySession(Session Session);

		// Token: 0x06000216 RID: 534
		List<LookupSubject> LoadAllLookupSubjects();
	}
}
