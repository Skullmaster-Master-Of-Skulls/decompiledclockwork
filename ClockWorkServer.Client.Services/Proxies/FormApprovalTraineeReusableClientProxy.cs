using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms.AppointmentNotes.FormApproval;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x02000092 RID: 146
	public class FormApprovalTraineeReusableClientProxy : WCFTokenBasedReusableClientProxy<IFormApprovalTrainee>, IFormApprovalTrainee, IService
	{
		// Token: 0x06000629 RID: 1577 RVA: 0x00010DC2 File Offset: 0x0000EFC2
		public FormApprovalTraineeReusableClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x0600062A RID: 1578 RVA: 0x00010DCD File Offset: 0x0000EFCD
		public FormApprovalTraineeReusableClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x0600062B RID: 1579 RVA: 0x00010DDC File Offset: 0x0000EFDC
		public LoadFormApprovalForTraineeResp LoadFormApprovalForTrainee(LoadFormApprovalForTraineeReq Request)
		{
			return this.WrapServiceMethod<LoadFormApprovalForTraineeResp>(() => this.Proxy.LoadFormApprovalForTrainee(Request));
		}

		// Token: 0x0600062C RID: 1580 RVA: 0x00010E14 File Offset: 0x0000F014
		public AddFormApprovalCommentForTraineeResp AddFormApprovalCommentForTrainee(AddFormApprovalCommentForTraineeReq Request)
		{
			return this.WrapServiceMethod<AddFormApprovalCommentForTraineeResp>(() => this.Proxy.AddFormApprovalCommentForTrainee(Request));
		}

		// Token: 0x0600062D RID: 1581 RVA: 0x00010E4C File Offset: 0x0000F04C
		public ReSubmitFormApprovalFormResp ReSubmitFormApprovalForm(ReSubmitFormApprovalFormReq Request)
		{
			return this.WrapServiceMethod<ReSubmitFormApprovalFormResp>(() => this.Proxy.ReSubmitFormApprovalForm(Request));
		}

		// Token: 0x0600062E RID: 1582 RVA: 0x00010E84 File Offset: 0x0000F084
		public CreateFormApprovalFormResp CreateFormApprovalForm(CreateFormApprovalFormReq Request)
		{
			return this.WrapServiceMethod<CreateFormApprovalFormResp>(() => this.Proxy.CreateFormApprovalForm(Request));
		}

		// Token: 0x0600062F RID: 1583 RVA: 0x00010EBC File Offset: 0x0000F0BC
		public LoadTraineeSignatureResp LoadTraineeSignature(LoadTraineeSignatureReq Request)
		{
			return this.WrapServiceMethod<LoadTraineeSignatureResp>(() => this.Proxy.LoadTraineeSignature(Request));
		}
	}
}
