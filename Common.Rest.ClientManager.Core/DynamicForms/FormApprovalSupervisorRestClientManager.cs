using System;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms.AppointmentNotes.FormApproval;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.DynamicForms;
using TechnoPro.Common.Public;
using TechnoPro.Common.Unity.IoC;
using TechnoPro.Common.Web.Security.Proxy;

namespace TechnoPro.Common.Rest.ClientManager.Core.DynamicForms
{
	// Token: 0x02000058 RID: 88
	public class FormApprovalSupervisorRestClientManager : BearerTokenRestProxy<IFormApprovalSupervisorClientManager>, IFormApprovalSupervisorClientManager, IWebService
	{
		// Token: 0x06000368 RID: 872 RVA: 0x0000A87D File Offset: 0x00008A7D
		public FormApprovalSupervisorRestClientManager(string serviceAddress, string token = null) : base(serviceAddress, token)
		{
		}

		// Token: 0x06000369 RID: 873 RVA: 0x0000A887 File Offset: 0x00008A87
		public FormApprovalSupervisorRestClientManager(string serviceAddress, string serviceAddressSuffix, string token = null) : base(serviceAddress, serviceAddressSuffix, token)
		{
		}

		// Token: 0x0600036A RID: 874 RVA: 0x0000A892 File Offset: 0x00008A92
		public FormApprovalForAppointmentDTO LoadFormApprovalForSupervisor(int screenNum, int studentPersonId, int appId)
		{
			return base.Get<FormApprovalForAppointmentDTO>(string.Format("formapprovalsupervisor/screennum/{0}/studentpid/{1}/appid/{2}", screenNum, studentPersonId, appId), true);
		}

		// Token: 0x0600036B RID: 875 RVA: 0x0000A8B8 File Offset: 0x00008AB8
		public void AddFormApprovalCommentForSupervisor(Guid formApprovalId, FormApprovalCommentTextDTO commentText)
		{
			AddFormApprovalCommentForSupervisorReq addFormApprovalCommentForSupervisorReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<AddFormApprovalCommentForSupervisorReq>();
			addFormApprovalCommentForSupervisorReq.FormApprovalId = formApprovalId;
			addFormApprovalCommentForSupervisorReq.Comment = commentText;
			base.Post<AddFormApprovalCommentForSupervisorReq>(addFormApprovalCommentForSupervisorReq, "formapprovalsupervisor/addcomment");
		}

		// Token: 0x0600036C RID: 876 RVA: 0x0000A8EC File Offset: 0x00008AEC
		public void ApproveForm(Guid formApprovalId, FormApprovalCommentTextDTO commentText, FormApprovalSignatureDTO supervisorSignature)
		{
			ApproveFormReq approveFormReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<ApproveFormReq>();
			approveFormReq.FormApprovalId = formApprovalId;
			approveFormReq.Comment = commentText;
			approveFormReq.SupervisorSignature = supervisorSignature;
			base.Post<ApproveFormReq>(approveFormReq, "formapprovalsupervisor/approveform");
		}

		// Token: 0x0600036D RID: 877 RVA: 0x0000A928 File Offset: 0x00008B28
		public void SendFormBackToTrainee(Guid formApprovalId, FormApprovalCommentTextDTO commentText)
		{
			SendFormBackToTraineeReq sendFormBackToTraineeReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<SendFormBackToTraineeReq>();
			sendFormBackToTraineeReq.FormApprovalId = formApprovalId;
			sendFormBackToTraineeReq.Comment = commentText;
			base.Post<SendFormBackToTraineeReq>(sendFormBackToTraineeReq, "formapprovalsupervisor/sendformbacktotrainee");
		}

		// Token: 0x0600036E RID: 878 RVA: 0x0000A95C File Offset: 0x00008B5C
		public void UnApproveFormApprovalResp(Guid formApprovalId, FormApprovalCommentTextDTO commentText = null)
		{
			UnApproveFormApprovalReq unApproveFormApprovalReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<UnApproveFormApprovalReq>();
			unApproveFormApprovalReq.FormApprovalId = formApprovalId;
			unApproveFormApprovalReq.CommentText = commentText;
			base.Post<UnApproveFormApprovalReq>(unApproveFormApprovalReq, "formapprovalsupervisor/unapprove");
		}

		// Token: 0x0600036F RID: 879 RVA: 0x0000A98E File Offset: 0x00008B8E
		public FormApprovalSignatureDTO LoadSupervisorSignature(Guid formApprovalId)
		{
			return base.Get<FormApprovalSignatureDTO>(string.Format("formapprovalsupervisor/supervisorsignature/formapprovalid/{0}", formApprovalId), true);
		}
	}
}
