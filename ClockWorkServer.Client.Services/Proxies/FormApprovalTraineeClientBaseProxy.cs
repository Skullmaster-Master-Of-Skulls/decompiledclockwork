using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms.AppointmentNotes.FormApproval;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x02000093 RID: 147
	internal class FormApprovalTraineeClientBaseProxy : ClientBase<IFormApprovalTrainee>, IFormApprovalTrainee, IService
	{
		// Token: 0x06000630 RID: 1584 RVA: 0x00010EF4 File Offset: 0x0000F0F4
		public FormApprovalTraineeClientBaseProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000631 RID: 1585 RVA: 0x00010EFF File Offset: 0x0000F0FF
		public FormApprovalTraineeClientBaseProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000632 RID: 1586 RVA: 0x00010F0C File Offset: 0x0000F10C
		public LoadFormApprovalForTraineeResp LoadFormApprovalForTrainee(LoadFormApprovalForTraineeReq Request)
		{
			return base.Channel.LoadFormApprovalForTrainee(Request);
		}

		// Token: 0x06000633 RID: 1587 RVA: 0x00010F2C File Offset: 0x0000F12C
		public AddFormApprovalCommentForTraineeResp AddFormApprovalCommentForTrainee(AddFormApprovalCommentForTraineeReq Request)
		{
			return base.Channel.AddFormApprovalCommentForTrainee(Request);
		}

		// Token: 0x06000634 RID: 1588 RVA: 0x00010F4C File Offset: 0x0000F14C
		public ReSubmitFormApprovalFormResp ReSubmitFormApprovalForm(ReSubmitFormApprovalFormReq Request)
		{
			return base.Channel.ReSubmitFormApprovalForm(Request);
		}

		// Token: 0x06000635 RID: 1589 RVA: 0x00010F6C File Offset: 0x0000F16C
		public CreateFormApprovalFormResp CreateFormApprovalForm(CreateFormApprovalFormReq Request)
		{
			return base.Channel.CreateFormApprovalForm(Request);
		}

		// Token: 0x06000636 RID: 1590 RVA: 0x00010F8C File Offset: 0x0000F18C
		public LoadTraineeSignatureResp LoadTraineeSignature(LoadTraineeSignatureReq Request)
		{
			return base.Channel.LoadTraineeSignature(Request);
		}
	}
}
