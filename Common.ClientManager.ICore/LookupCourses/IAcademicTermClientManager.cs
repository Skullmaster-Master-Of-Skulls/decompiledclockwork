using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.LookupCourses;

namespace TechnoPro.Common.ClientManager.ICore.LookupCourses
{
	// Token: 0x02000040 RID: 64
	public interface IAcademicTermClientManager : IWebService
	{
		// Token: 0x060001D4 RID: 468
		AcademicTermDTO GetCurrentAcademicTerm();

		// Token: 0x060001D5 RID: 469
		IList<AcademicTermDTO> LoadAcademicTerms(bool ignoreCache = false);

		// Token: 0x060001D6 RID: 470
		AcademicTermDTO GetAcademicTerm(DateTime date);

		// Token: 0x060001D7 RID: 471
		void ChangeCurrentAcademicTerms(IList<AcademicTermDTO> newAcademicTermList);

		// Token: 0x060001D8 RID: 472
		eSessionListValidationResult ValidateAcademicTermList(IList<AcademicTermDTO> list);
	}
}
