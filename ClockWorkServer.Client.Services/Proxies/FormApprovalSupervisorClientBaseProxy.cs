using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms.AppointmentNotes.FormApproval;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x02000091 RID: 145
	internal class FormApprovalSupervisorClientBaseProxy : ClientBase<IFormApprovalSupervisor>, IFormApprovalSupervisor, IService
	{
		// Token: 0x06000621 RID: 1569 RVA: 0x00010CEC File Offset: 0x0000EEEC
		public FormApprovalSupervisorClientBaseProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000622 RID: 1570 RVA: 0x00010CF7 File Offset: 0x0000EEF7
		public FormApprovalSupervisorClientBaseProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000623 RID: 1571 RVA: 0x00010D04 File Offset: 0x0000EF04
		public LoadFormApprovalForSupervisorResp LoadFormApprovalForSupervisor(LoadFormApprovalForSupervisorReq Request)
		{
			return base.Channel.LoadFormApprovalForSupervisor(Request);
		}

		// Token: 0x06000624 RID: 1572 RVA: 0x00010D24 File Offset: 0x0000EF24
		public AddFormApprovalCommentForSupervisorResp AddFormApprovalCommentForSupervisor(AddFormApprovalCommentForSupervisorReq Request)
		{
			return base.Channel.AddFormApprovalCommentForSupervisor(Request);
		}

		// Token: 0x06000625 RID: 1573 RVA: 0x00010D44 File Offset: 0x0000EF44
		public ApproveFormResp ApproveForm(ApproveFormReq Request)
		{
			return base.Channel.ApproveForm(Request);
		}

		// Token: 0x06000626 RID: 1574 RVA: 0x00010D64 File Offset: 0x0000EF64
		public SendFormBackToTraineeResp SendFormBackToTrainee(SendFormBackToTraineeReq Request)
		{
			return base.Channel.SendFormBackToTrainee(Request);
		}

		// Token: 0x06000627 RID: 1575 RVA: 0x00010D84 File Offset: 0x0000EF84
		public UnApproveFormApprovalResp UnApproveFormApproval(UnApproveFormApprovalReq Request)
		{
			return base.Channel.UnApproveFormApproval(Request);
		}

		// Token: 0x06000628 RID: 1576 RVA: 0x00010DA4 File Offset: 0x0000EFA4
		public LoadSupervisorSignatureResp LoadSupervisorSignature(LoadSupervisorSignatureReq Request)
		{
			return base.Channel.LoadSupervisorSignature(Request);
		}
	}
}
