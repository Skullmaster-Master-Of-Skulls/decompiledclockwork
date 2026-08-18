using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.DynamicForms;
using TechnoPro.Common.Public.Entities.Intake;

namespace TechnoPro.Common.ICore.Intake
{
	// Token: 0x02000089 RID: 137
	public interface IIntakeAccountManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060003D4 RID: 980
		int CreateNewIntakeAccount(IntakeUserAccount UserAccount);

		// Token: 0x060003D5 RID: 981
		IList<IntakeEntry> LoadPendingIntakeEntries();

		// Token: 0x060003D6 RID: 982
		void UpdateActiveIntakeStatusAndNote(int[] intakePersonIds, string newNote, Guid newIntakeStatusId);

		// Token: 0x060003D7 RID: 983
		void UpdateActiveIntakeStatus(int[] intakePersonIds, Guid newIntakeStatusId);

		// Token: 0x060003D8 RID: 984
		void UpdateActiveIntakeNote(int[] intakePersonIds, string newNote);

		// Token: 0x060003D9 RID: 985
		void RemoveIntake(string student_no);

		// Token: 0x060003DA RID: 986
		IList<IntakeStatus> LoadLookupStatuses();

		// Token: 0x060003DB RID: 987
		CreateRealStudentAccountFromIntakeResult CreateRealStudentAccountFromIntakeAndRemoveIntake(string snum, IList<int> gids);

		// Token: 0x060003DC RID: 988
		IList<DynamicData> LoadIntakeFormData(string snum);

		// Token: 0x060003DD RID: 989
		IDictionary<string, ePreIntakeStatus> GetIntakeStatuses(params string[] studentNumbers);

		// Token: 0x060003DE RID: 990
		void RemoveIntakes(int[] intakePersonIds);

		// Token: 0x060003DF RID: 991
		IList<IntakeEntryQueueItem> LoadPendingIntakeEntryQueueItems();

		// Token: 0x060003E0 RID: 992
		void SyncIntakeData(string snum, bool removeIntakesWhenDone);
	}
}
