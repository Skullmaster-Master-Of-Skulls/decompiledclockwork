using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments;
using TechnoPro.ClockWorkServer.Contracts.DTO.Cases;
using TechnoPro.Common.Public;

namespace TechnoPro.Common.ClientManager.ICore.Cases
{
	// Token: 0x02000072 RID: 114
	public interface ICasesClientManager : IWebService
	{
		// Token: 0x06000356 RID: 854
		IList<CaseForDisplayDTO> LoadCasesForDisplayForStudent(int PersonId, int ScreenNum, params int[] controlIdsToAddToColumn);

		// Token: 0x06000357 RID: 855
		CaseDTO LoadCaseById(int InfoPcId, int ScreenNum);

		// Token: 0x06000358 RID: 856
		int CreateCase(CaseDTO Case);

		// Token: 0x06000359 RID: 857
		void DeleteCase(int InfoPcId);

		// Token: 0x0600035A RID: 858
		void UpdateCase(CaseDTO Case);

		// Token: 0x0600035B RID: 859
		IList<BaseBasicAppointmentDTO> LoadBasicAppointmentsByCase(int caseId);
	}
}
