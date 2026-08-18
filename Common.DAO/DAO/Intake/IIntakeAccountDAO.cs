using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.DynamicForms;
using TechnoPro.Common.Public.Entities.Intake;

namespace TechnoPro.Common.DAO.Intake
{
	// Token: 0x0200006E RID: 110
	public interface IIntakeAccountDAO : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060002A8 RID: 680
		int CreateNewIntakeAccount(IntakeUserAccount UserAccount);

		// Token: 0x060002A9 RID: 681
		IList<IntakeEntry> LoadPendingIntakeEntries();

		// Token: 0x060002AA RID: 682
		void UpdateActiveIntakeStatus(int[] intakePersonIds, Guid newIntakeStatusId);

		// Token: 0x060002AB RID: 683
		void UpdateActiveIntakeNote(int[] intakePersonIds, string newNote);

		// Token: 0x060002AC RID: 684
		void MarkIntakesInactiveByStudentNumber(string student_no);

		// Token: 0x060002AD RID: 685
		void MarkIntakesInactiveByPersonIds(int[] intakePersonIds);

		// Token: 0x060002AE RID: 686
		IList<IntakeStatus> LoadLookupStatuses();

		// Token: 0x060002AF RID: 687
		IList<DynamicData> LoadIntakeFormData(string snum, int intakeFormScreenNum);

		// Token: 0x060002B0 RID: 688
		int[] LoadIntakePersonIdsByStudentNumber(string snum);

		// Token: 0x060002B1 RID: 689
		IntakePerson LoadIntakePersonByStudentNumber(string snum);

		// Token: 0x060002B2 RID: 690
		IList<IntakeEntryQueueItem> LoadPendingIntakeEntryQueueItems(int departmentControlId);
	}
}
