using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.DynamicForms;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.ICore.People
{
	// Token: 0x02000054 RID: 84
	public interface IStaffCommonInfoManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x0600020A RID: 522
		DynamicData LoadStaffStoredSignatureData(int StaffPersonId);

		// Token: 0x0600020B RID: 523
		byte[] LoadStaffStoredSignature(int StaffPersonId);

		// Token: 0x0600020C RID: 524
		void SaveStaffStoredSignature(int StaffPersonId, byte[] imageBytes);

		// Token: 0x0600020D RID: 525
		DynamicData LoadAssignedAdvisorSignatureData(int StudentPersonId);

		// Token: 0x0600020E RID: 526
		void SaveAssignedAdvisorStoredSignatureWithImageBytes(int StudentPersonId, byte[] imageBytes);

		// Token: 0x0600020F RID: 527
		void SaveAssignedAdvisorStoredSignature(int StudentPersonId, DynamicData dataItem);

		// Token: 0x06000210 RID: 528
		string LoadStaffEmail(int StaffPersonId);

		// Token: 0x06000211 RID: 529
		StaffWithCommonInfo LoadStaffWithCommonInfoById(int PersonId);

		// Token: 0x06000212 RID: 530
		T LoadStaffWithCommonInfoById<T>(int PersonId) where T : StaffWithCommonInfo;

		// Token: 0x06000213 RID: 531
		PersonBase LoadStaffByEmail(string Email);

		// Token: 0x06000214 RID: 532
		void UpdateCommonInfo(int PersonId, StaffCommonInfo CommonInfo, bool JustUpdateEmailAndPhone);

		// Token: 0x06000215 RID: 533
		IList<T> LoadStaffWithCommonInfoByGroupTitle<T>(params string[] GroupTitles) where T : StaffWithCommonInfo;

		// Token: 0x06000216 RID: 534
		int CreateStaffWithCommonInfo(StaffWithCommonInfo staffWithCommonInfo, params string[] addToFirstGroupTitleInThisList);

		// Token: 0x06000217 RID: 535
		void UpdateStaffWithCommonInfo(StaffWithCommonInfo staffWithCommonInfo, bool justUpdateEmailAndPhone);
	}
}
