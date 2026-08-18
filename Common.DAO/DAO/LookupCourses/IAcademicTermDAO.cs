using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.LookupCourses;

namespace TechnoPro.Common.DAO.LookupCourses
{
	// Token: 0x0200005C RID: 92
	public interface IAcademicTermDAO : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x0600021D RID: 541
		IList<AcademicTerm> LoadAcademicTerms();

		// Token: 0x0600021E RID: 542
		void ChangeCurrentAcademicTerms(IList<AcademicTerm> newAcademicTermList);
	}
}
