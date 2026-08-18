using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms.AppointmentNotes.FormApproval;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x02000090 RID: 144
	public class FormApprovalSupervisorReusableClientProxy : WCFTokenBasedReusableClientProxy<IFormApprovalSupervisor>, IFormApprovalSupervisor, IService
	{
		// Token: 0x06000619 RID: 1561 RVA: 0x00010B82 File Offset: 0x0000ED82
		public FormApprovalSupervisorReusableClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x0600061A RID: 1562 RVA: 0x00010B8D File Offset: 0x0000ED8D
		public FormApprovalSupervisorReusableClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x0600061B RID: 1563 RVA: 0x00010B9C File Offset: 0x0000ED9C
		public LoadFormApprovalForSupervisorResp LoadFormApprovalForSupervisor(LoadFormApprovalForSupervisorReq Request)
		{
			return this.WrapServiceMethod<LoadFormApprovalForSupervisorResp>(() => this.Proxy.LoadFormApprovalForSupervisor(Request));
		}

		// Token: 0x0600061C RID: 1564 RVA: 0x00010BD4 File Offset: 0x0000EDD4
		public AddFormApprovalCommentForSupervisorResp AddFormApprovalCommentForSupervisor(AddFormApprovalCommentForSupervisorReq Request)
		{
			return this.WrapServiceMethod<AddFormApprovalCommentForSupervisorResp>(() => this.Proxy.AddFormApprovalCommentForSupervisor(Request));
		}

		// Token: 0x0600061D RID: 1565 RVA: 0x00010C0C File Offset: 0x0000EE0C
		public ApproveFormResp ApproveForm(ApproveFormReq Request)
		{
			return this.WrapServiceMethod<ApproveFormResp>(() => this.Proxy.ApproveForm(Request));
		}

		// Token: 0x0600061E RID: 1566 RVA: 0x00010C44 File Offset: 0x0000EE44
		public SendFormBackToTraineeResp SendFormBackToTrainee(SendFormBackToTraineeReq Request)
		{
			return this.WrapServiceMethod<SendFormBackToTraineeResp>(() => this.Proxy.SendFormBackToTrainee(Request));
		}

		// Token: 0x0600061F RID: 1567 RVA: 0x00010C7C File Offset: 0x0000EE7C
		public UnApproveFormApprovalResp UnApproveFormApproval(UnApproveFormApprovalReq Request)
		{
			return this.WrapServiceMethod<UnApproveFormApprovalResp>(() => this.Proxy.UnApproveFormApproval(Request));
		}

		// Token: 0x06000620 RID: 1568 RVA: 0x00010CB4 File Offset: 0x0000EEB4
		public LoadSupervisorSignatureResp LoadSupervisorSignature(LoadSupervisorSignatureReq Request)
		{
			return this.WrapServiceMethod<LoadSupervisorSignatureResp>(() => this.Proxy.LoadSupervisorSignature(Request));
		}
	}
}
