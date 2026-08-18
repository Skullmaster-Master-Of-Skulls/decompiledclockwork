using System;
using TechnoPro.ClockWorkServer.Client.ServiceFactory;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms.AppointmentNotes.FormApproval;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.DynamicForms;
using TechnoPro.Common.Public;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.ClientManager.Core.DynamicForms
{
	// Token: 0x02000069 RID: 105
	public class FormApprovalSupervisorClientManager : IFormApprovalSupervisorClientManager, IWebService
	{
		// Token: 0x060003DB RID: 987 RVA: 0x000116CC File Offset: 0x0000F8CC
		public FormApprovalForAppointmentDTO LoadFormApprovalForSupervisor(int screenNum, int studentPersonId, int appId)
		{
			LoadFormApprovalForSupervisorReq loadFormApprovalForSupervisorReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadFormApprovalForSupervisorReq>();
			loadFormApprovalForSupervisorReq.ScreenNum = screenNum;
			loadFormApprovalForSupervisorReq.StudentPersonId = studentPersonId;
			loadFormApprovalForSupervisorReq.AppointmentId = appId;
			return ClientServiceFactory.GetClientInstance<IFormApprovalSupervisor>().LoadFormApprovalForSupervisor(loadFormApprovalForSupervisorReq).FormApproval;
		}

		// Token: 0x060003DC RID: 988 RVA: 0x00011714 File Offset: 0x0000F914
		public void AddFormApprovalCommentForSupervisor(Guid formApprovalId, FormApprovalCommentTextDTO commentText)
		{
			AddFormApprovalCommentForSupervisorReq addFormApprovalCommentForSupervisorReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<AddFormApprovalCommentForSupervisorReq>();
			addFormApprovalCommentForSupervisorReq.FormApprovalId = formApprovalId;
			addFormApprovalCommentForSupervisorReq.Comment = commentText;
			ClientServiceFactory.GetClientInstance<IFormApprovalSupervisor>().AddFormApprovalCommentForSupervisor(addFormApprovalCommentForSupervisorReq);
		}

		// Token: 0x060003DD RID: 989 RVA: 0x0001174C File Offset: 0x0000F94C
		public void ApproveForm(Guid formApprovalId, FormApprovalCommentTextDTO commentText, FormApprovalSignatureDTO supervisorSignature)
		{
			ApproveFormReq approveFormReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<ApproveFormReq>();
			approveFormReq.FormApprovalId = formApprovalId;
			approveFormReq.Comment = commentText;
			approveFormReq.SupervisorSignature = supervisorSignature;
			ClientServiceFactory.GetClientInstance<IFormApprovalSupervisor>().ApproveForm(approveFormReq);
		}

		// Token: 0x060003DE RID: 990 RVA: 0x0001178C File Offset: 0x0000F98C
		public void SendFormBackToTrainee(Guid formApprovalId, FormApprovalCommentTextDTO commentText)
		{
			SendFormBackToTraineeReq sendFormBackToTraineeReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<SendFormBackToTraineeReq>();
			sendFormBackToTraineeReq.FormApprovalId = formApprovalId;
			sendFormBackToTraineeReq.Comment = commentText;
			ClientServiceFactory.GetClientInstance<IFormApprovalSupervisor>().SendFormBackToTrainee(sendFormBackToTraineeReq);
		}

		// Token: 0x060003DF RID: 991 RVA: 0x000117C4 File Offset: 0x0000F9C4
		public void UnApproveFormApprovalResp(Guid formApprovalId, FormApprovalCommentTextDTO commentText = null)
		{
			UnApproveFormApprovalReq unApproveFormApprovalReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<UnApproveFormApprovalReq>();
			unApproveFormApprovalReq.FormApprovalId = formApprovalId;
			unApproveFormApprovalReq.CommentText = commentText;
			ClientServiceFactory.GetClientInstance<IFormApprovalSupervisor>().UnApproveFormApproval(unApproveFormApprovalReq);
		}

		// Token: 0x060003E0 RID: 992 RVA: 0x000117FC File Offset: 0x0000F9FC
		public FormApprovalSignatureDTO LoadSupervisorSignature(Guid formApprovalId)
		{
			LoadSupervisorSignatureReq loadSupervisorSignatureReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadSupervisorSignatureReq>();
			loadSupervisorSignatureReq.FormApprovalId = formApprovalId;
			return ClientServiceFactory.GetClientInstance<IFormApprovalSupervisor>().LoadSupervisorSignature(loadSupervisorSignatureReq).Signature;
		}
	}
}
