using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.LookupCourses;

namespace TechnoPro.Common.ICore.LookupCourses
{
	// Token: 0x02000071 RID: 113
	public interface IAcademicTermManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000339 RID: 825
		AcademicTerm GetCurrentAcademicTerm();

		// Token: 0x0600033A RID: 826
		IList<AcademicTerm> LoadAcademicTerms(bool ignoreCache = false);

		// Token: 0x0600033B RID: 827
		AcademicTerm GetAcademicTerm(DateTime date);

		// Token: 0x0600033C RID: 828
		void ChangeCurrentAcademicTerms(IList<AcademicTerm> newAcademicTermList);

		// Token: 0x0600033D RID: 829
		eSessionListValidationResult ValidateAcademicTermList(IList<AcademicTerm> list);
	}
}
