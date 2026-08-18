using System;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms.AppointmentNotes.FormApproval;
using TechnoPro.Common.Public;

namespace TechnoPro.Common.ClientManager.ICore.DynamicForms
{
	// Token: 0x02000063 RID: 99
	public interface IFormApprovalTraineeClientManager : IWebService
	{
		// Token: 0x06000300 RID: 768
		FormApprovalForAppointmentDTO LoadFormApprovalForTrainee(int screenNum, int studentPersonId, int appId);

		// Token: 0x06000301 RID: 769
		void AddFormApprovalCommentForTrainee(Guid formApprovalId, FormApprovalCommentTextDTO comment);

		// Token: 0x06000302 RID: 770
		void ReSubmitFormApprovalForm(Guid formApprovalId, FormApprovalCommentTextDTO comment);

		// Token: 0x06000303 RID: 771
		Guid CreateFormApprovalForm(int screenNum, int studentPersonId, int appId, FormApprovalCommentTextDTO comment, FormApprovalSignatureDTO traineeSignature);

		// Token: 0x06000304 RID: 772
		FormApprovalSignatureDTO LoadTraineeSignature(Guid formApprovalId);
	}
}
