using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Appointments;
using TechnoPro.Common.Public.Entities.Cases;

namespace TechnoPro.Common.DAO.Cases
{
	// Token: 0x0200009E RID: 158
	public interface ICaseDAO : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000413 RID: 1043
		void MergeCasesForTwoStudents(int PersonIdNew, int PersonIdOld);

		// Token: 0x06000414 RID: 1044
		int CreateCase(Case Case);

		// Token: 0x06000415 RID: 1045
		void DeleteCase(int InfoPcId);

		// Token: 0x06000416 RID: 1046
		void UpdateCaseClientsAndRespondents(int InfoPcId, IList<CaseClient> FullClientListForCase);

		// Token: 0x06000417 RID: 1047
		void UpdateBasicCaseInfo(int InfoPcId, string NewTitle);

		// Token: 0x06000418 RID: 1048
		Case LoadCaseById(int InfoPcId, int ScreenNum);

		// Token: 0x06000419 RID: 1049
		IList<CaseForDisplay> LoadCasesForDisplayForStudent(int PersonId, int ScreenNum, params int[] controlIdsToAddToColumn);

		// Token: 0x0600041A RID: 1050
		IList<BaseBasicAppointment> LoadBasicAppointmentsByCase(int infoPcId);

		// Token: 0x0600041B RID: 1051
		Task<IList<BaseBasicAppointment>> LoadBasicAppointmentsByCaseAsync(int infoPcId);
	}
}
