using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.LookupCourses;

namespace TechnoPro.Common.ICore.LookupCourses
{
	// Token: 0x0200006E RID: 110
	public interface ILookupSubjectManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000322 RID: 802
		LookupSubject LoadLookupSubject(int SubjectId);

		// Token: 0x06000323 RID: 803
		int SaveSubject(LookupSubject subject);

		// Token: 0x06000324 RID: 804
		LookupSubject LoadLookupSubjectBySubjectCode(string SubjectCode);

		// Token: 0x06000325 RID: 805
		LookupSubject LoadLookupSubjectBySubjectDescription(string SubjectDescription);

		// Token: 0x06000326 RID: 806
		LookupSubject LoadLookupSubject(string SubjectCode, string SubjectDescription);

		// Token: 0x06000327 RID: 807
		List<LookupSubject> LoadLookupSubjectsBySession(Session Session);

		// Token: 0x06000328 RID: 808
		List<LookupSubject> LoadAllLookupSubjects();
	}
}
