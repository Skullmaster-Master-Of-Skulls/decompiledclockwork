using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Tutoring;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x02000156 RID: 342
	internal class TutorClientBaseProxy : ClientBase<ITutor>, ITutor, IService
	{
		// Token: 0x06000D24 RID: 3364 RVA: 0x00020AD4 File Offset: 0x0001ECD4
		public TutorClientBaseProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000D25 RID: 3365 RVA: 0x00020ADF File Offset: 0x0001ECDF
		public TutorClientBaseProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000D26 RID: 3366 RVA: 0x00020AEC File Offset: 0x0001ECEC
		public SearchForTutorsResp SearchForTutors(SearchForTutorsReq Request)
		{
			return base.Channel.SearchForTutors(Request);
		}

		// Token: 0x06000D27 RID: 3367 RVA: 0x00020B0C File Offset: 0x0001ED0C
		public LoadTutorByPersonIdResp LoadTutorByPersonId(LoadTutorByPersonIdReq Request)
		{
			return base.Channel.LoadTutorByPersonId(Request);
		}

		// Token: 0x06000D28 RID: 3368 RVA: 0x00020B2C File Offset: 0x0001ED2C
		public TryToBookTutorAppointmentResp TryToBookTutorAppointment(TryToBookTutorAppointmentReq Request)
		{
			return base.Channel.TryToBookTutorAppointment(Request);
		}

		// Token: 0x06000D29 RID: 3369 RVA: 0x00020B4C File Offset: 0x0001ED4C
		public IsConfidentialityAgreementSigningRequiredForTutorResp IsConfidentialityAgreementSigningRequiredForTutor(IsConfidentialityAgreementSigningRequiredForTutorReq Request)
		{
			return base.Channel.IsConfidentialityAgreementSigningRequiredForTutor(Request);
		}

		// Token: 0x06000D2A RID: 3370 RVA: 0x00020B6A File Offset: 0x0001ED6A
		public void RecordConfidentialityAgreementSignedByTutor(RecordConfidentialityAgreementSignedByTutorReq Request)
		{
			base.Channel.RecordConfidentialityAgreementSignedByTutor(Request);
		}

		// Token: 0x06000D2B RID: 3371 RVA: 0x00020B7C File Offset: 0x0001ED7C
		public CreateTutorResp CreateTutor(CreateTutorReq Request)
		{
			return base.Channel.CreateTutor(Request);
		}

		// Token: 0x06000D2C RID: 3372 RVA: 0x00020B9A File Offset: 0x0001ED9A
		public void RegisterTutorByExistingPersonId(RegisterTutorByExistingPersonIdReq Request)
		{
			base.Channel.RegisterTutorByExistingPersonId(Request);
		}

		// Token: 0x06000D2D RID: 3373 RVA: 0x00020BAC File Offset: 0x0001EDAC
		public GetTutorStatusResp GetTutorStatus(GetTutorStatusReq Request)
		{
			return base.Channel.GetTutorStatus(Request);
		}

		// Token: 0x06000D2E RID: 3374 RVA: 0x00020BCC File Offset: 0x0001EDCC
		public LoadAllTutorsResp LoadAllTutors(LoadAllTutorsReq Request)
		{
			return base.Channel.LoadAllTutors(Request);
		}

		// Token: 0x06000D2F RID: 3375 RVA: 0x00020BEA File Offset: 0x0001EDEA
		public void ActivateTutor(ActivateTutorReq Request)
		{
			base.Channel.ActivateTutor(Request);
		}

		// Token: 0x06000D30 RID: 3376 RVA: 0x00020BFA File Offset: 0x0001EDFA
		public void DeActivateTutor(DeActivateTutorReq Request)
		{
			base.Channel.DeActivateTutor(Request);
		}

		// Token: 0x06000D31 RID: 3377 RVA: 0x00020C0C File Offset: 0x0001EE0C
		public LoadTutorAppointmentResp LoadTutorAppointment(LoadTutorAppointmentReq Request)
		{
			return base.Channel.LoadTutorAppointment(Request);
		}

		// Token: 0x06000D32 RID: 3378 RVA: 0x00020C2C File Offset: 0x0001EE2C
		public LoadTutorWithActiveStatusByIdResp LoadTutorWithActiveStatusById(LoadTutorWithActiveStatusByIdReq Request)
		{
			return base.Channel.LoadTutorWithActiveStatusById(Request);
		}

		// Token: 0x06000D33 RID: 3379 RVA: 0x00020C4C File Offset: 0x0001EE4C
		public GetTutorStatusesResp GetTutorStatuses(GetTutorStatusesReq Request)
		{
			return base.Channel.GetTutorStatuses(Request);
		}
	}
}
