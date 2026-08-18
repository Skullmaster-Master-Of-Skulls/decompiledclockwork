using System;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms.AppointmentNotes.FormApproval;
using TechnoPro.Common.Public;

namespace TechnoPro.Common.ClientManager.ICore.DynamicForms
{
	// Token: 0x02000062 RID: 98
	public interface IFormApprovalSupervisorClientManager : IWebService
	{
		// Token: 0x060002FA RID: 762
		FormApprovalForAppointmentDTO LoadFormApprovalForSupervisor(int screenNum, int studentPersonId, int appId);

		// Token: 0x060002FB RID: 763
		void AddFormApprovalCommentForSupervisor(Guid formApprovalId, FormApprovalCommentTextDTO commentText);

		// Token: 0x060002FC RID: 764
		void ApproveForm(Guid formApprovalId, FormApprovalCommentTextDTO commentText, FormApprovalSignatureDTO supervisorSignature);

		// Token: 0x060002FD RID: 765
		void SendFormBackToTrainee(Guid formApprovalId, FormApprovalCommentTextDTO commentText);

		// Token: 0x060002FE RID: 766
		void UnApproveFormApprovalResp(Guid formApprovalId, FormApprovalCommentTextDTO commentText = null);

		// Token: 0x060002FF RID: 767
		FormApprovalSignatureDTO LoadSupervisorSignature(Guid formApprovalId);
	}
}
