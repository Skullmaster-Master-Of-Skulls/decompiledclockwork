using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Appointments;
using TechnoPro.Common.Public.Entities.Cases;

namespace TechnoPro.Common.ICore.Cases
{
	// Token: 0x020000B9 RID: 185
	public interface ICaseManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000578 RID: 1400
		void MergeCasesForTwoStudents(int PersonIdNew, int PersonIdOld);

		// Token: 0x06000579 RID: 1401
		IList<CaseForDisplay> LoadCasesForDisplayForStudent(int PersonId, int ScreenNum, params int[] controlIdsToAddToColumn);

		// Token: 0x0600057A RID: 1402
		Case LoadCaseById(int InfoPcId, int ScreenNum);

		// Token: 0x0600057B RID: 1403
		int CreateCase(Case Case);

		// Token: 0x0600057C RID: 1404
		void DeleteCase(int InfoPcId);

		// Token: 0x0600057D RID: 1405
		void UpdateCaseClientsAndRespondents(int InfoPcId, IList<CaseClient> FullClientListForCase);

		// Token: 0x0600057E RID: 1406
		void UpdateCase(Case Case);

		// Token: 0x0600057F RID: 1407
		IList<BaseBasicAppointment> LoadBasicAppointmentsByCase(int infoPcId);

		// Token: 0x06000580 RID: 1408
		Task<IList<BaseBasicAppointment>> LoadBasicAppointmentsByCaseAsync(int infoPcId);
	}
}
