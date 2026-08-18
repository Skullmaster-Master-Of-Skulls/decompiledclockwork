using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;
using TechnoPro.ClockWorkServer.Contracts.DTO.Veteran;
using TechnoPro.Common.Public;

namespace TechnoPro.Common.ClientManager.ICore.Veteran
{
	// Token: 0x02000005 RID: 5
	public interface IVeteranClientManager : IWebService
	{
		// Token: 0x06000010 RID: 16
		bool HasUserCompletedBenefitRequestForm(int Pid, PerDateEntryDTO PerDateEntry, int ScreenNum);

		// Token: 0x06000011 RID: 17
		bool HasUserCompletedAgreementForm(int Pid, PerDateEntryDTO PerDateEntry, int ScreenNum);

		// Token: 0x06000012 RID: 18
		bool? CounselorResult(int Pid, SessionDTO Session, PerDateEntryDTO PerDateEntry, out string MessageToStudent);

		// Token: 0x06000013 RID: 19
		bool? AdministratorResult(int Pid, SessionDTO Session, PerDateEntryDTO PerDateEntry, out string MessageToStudent);

		// Token: 0x06000014 RID: 20
		IList<ChangeInBenefitRequestDTO> LoadChangeInBenefits(int StudentPersonId, DateTime StartDate, DateTime EndDate);
	}
}
