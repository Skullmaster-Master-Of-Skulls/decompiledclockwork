using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;
using TechnoPro.Common.Public;

namespace TechnoPro.Common.ClientManager.ICore.LookupCourses
{
	// Token: 0x0200003D RID: 61
	public interface ILookupSubjectClientManager : IWebService
	{
		// Token: 0x060001C4 RID: 452
		int SaveSubject(LookupSubjectDTO subject);

		// Token: 0x060001C5 RID: 453
		LookupSubjectDTO LoadLookupSubjectBySubjectCode(string SubjectCode);

		// Token: 0x060001C6 RID: 454
		LookupSubjectDTO LoadLookupSubjectBySubjectDescription(string SubjectDescription);

		// Token: 0x060001C7 RID: 455
		LookupSubjectDTO LoadLookupSubject(string SubjectCode, string SubjectDescription);

		// Token: 0x060001C8 RID: 456
		IList<LookupSubjectDTO> LoadLookupSubjectsBySession(SessionDTO Session);

		// Token: 0x060001C9 RID: 457
		IList<LookupSubjectDTO> LoadAllLookupSubjects();
	}
}
