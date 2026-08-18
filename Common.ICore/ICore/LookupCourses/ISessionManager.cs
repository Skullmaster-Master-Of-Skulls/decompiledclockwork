using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.LookupCourses;

namespace TechnoPro.Common.ICore.LookupCourses
{
	// Token: 0x02000070 RID: 112
	public interface ISessionManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x0600032D RID: 813
		Session AddSession(Session session, int count);

		// Token: 0x0600032E RID: 814
		Session SubtractSession(Session session, int count);

		// Token: 0x0600032F RID: 815
		Session GoToTodaysSession(Session session);

		// Token: 0x06000330 RID: 816
		Session GotoSession(Session session, AcademicTerm term, int year);

		// Token: 0x06000331 RID: 817
		[Obsolete("Use IAcademicTermManager instead")]
		AcademicTerm GetCurrentAcademicTerm();

		// Token: 0x06000332 RID: 818
		Session CopySession(Session session);

		// Token: 0x06000333 RID: 819
		[Obsolete("Use IAcademicTermManager instead")]
		IList<AcademicTerm> LoadAcademicTerms();

		// Token: 0x06000334 RID: 820
		Session GetCurrentSession();

		// Token: 0x06000335 RID: 821
		Session GetSession(DateTime Date);

		// Token: 0x06000336 RID: 822
		[Obsolete("Use IAcademicTermManager instead")]
		AcademicTerm GetAcademicTerm(DateTime date);

		// Token: 0x06000337 RID: 823
		void SetSessionChooserDefaultValue(DateTime DtpNow);

		// Token: 0x06000338 RID: 824
		DateTime? GetSessionChooserDefaultValue();
	}
}
