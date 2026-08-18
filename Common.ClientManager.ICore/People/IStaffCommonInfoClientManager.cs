using System;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.Public;

namespace TechnoPro.Common.ClientManager.ICore.People
{
	// Token: 0x0200002D RID: 45
	public interface IStaffCommonInfoClientManager : IWebService
	{
		// Token: 0x06000134 RID: 308
		byte[] LoadStaffStoredSignature(int StaffPersonId);

		// Token: 0x06000135 RID: 309
		void SaveStaffStoredSignature(int StaffPersonId, byte[] imageBytes);

		// Token: 0x06000136 RID: 310
		DynamicDataDTO LoadAssignedAdvisorSignatureData(int StudentPersonId);

		// Token: 0x06000137 RID: 311
		void SaveAssignedAdvisorStoredSignatureWithImageBytes(int StudentPersonId, byte[] imageBytes);

		// Token: 0x06000138 RID: 312
		void SaveAssignedAdvisorStoredSignature(int StudentPersonId, DynamicDataDTO dataItem);

		// Token: 0x06000139 RID: 313
		StaffWithCommonInfoDTO LoadStaffWithCommonInfoById(int PersonId);

		// Token: 0x0600013A RID: 314
		void UpdateCommonInfo(int PersonId, StaffCommonInfoDTO CommonInfo, bool JustUpdateEmailAndPhone);
	}
}
