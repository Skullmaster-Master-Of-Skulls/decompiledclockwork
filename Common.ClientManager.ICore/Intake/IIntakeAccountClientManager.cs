using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms;
using TechnoPro.ClockWorkServer.Contracts.DTO.Intake;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.Intake;

namespace TechnoPro.Common.ClientManager.ICore.Intake
{
	// Token: 0x02000053 RID: 83
	public interface IIntakeAccountClientManager : IWebService
	{
		// Token: 0x06000261 RID: 609
		int CreateNewIntakeAccount(IntakeUserAccountDTO UserAccount);

		// Token: 0x06000262 RID: 610
		IList<IntakeEntryDTO> LoadPendingIntakeEntries();

		// Token: 0x06000263 RID: 611
		void UpdateActiveIntakeStatusAndNote(int[] intakePersonIds, string newNote, Guid newIntakeStatusId);

		// Token: 0x06000264 RID: 612
		void UpdateActiveIntakeStatus(int[] intakePersonIds, Guid newIntakeStatusId);

		// Token: 0x06000265 RID: 613
		void UpdateActiveIntakeNote(int[] intakePersonIds, string newNote);

		// Token: 0x06000266 RID: 614
		void RemoveIntake(string student_no);

		// Token: 0x06000267 RID: 615
		IList<IntakeStatusDTO> LoadLookupStatuses();

		// Token: 0x06000268 RID: 616
		CreateRealStudentAccountFromIntakeResultDTO CreateRealStudentAccountFromIntakeAndRemoveIntake(string snum, int[] gids);

		// Token: 0x06000269 RID: 617
		IList<DynamicDataDTO> LoadIntakeFormData(string snum);

		// Token: 0x0600026A RID: 618
		IDictionary<string, ePreIntakeStatus> GetIntakeStatuses(params string[] studentNumbers);

		// Token: 0x0600026B RID: 619
		void RemoveIntakes(int[] intakePersonIds);

		// Token: 0x0600026C RID: 620
		IList<IntakeEntryQueueItemDTO> LoadPendingIntakeEntryQueueItems();

		// Token: 0x0600026D RID: 621
		void SyncIntakeData(string snum, bool removeIntakesWhenDone);
	}
}
