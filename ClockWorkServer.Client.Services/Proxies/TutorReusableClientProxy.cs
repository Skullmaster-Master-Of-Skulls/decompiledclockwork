using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Tutoring;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x02000155 RID: 341
	public class TutorReusableClientProxy : WCFTokenBasedReusableClientProxy<ITutor>, ITutor, IService
	{
		// Token: 0x06000D14 RID: 3348 RVA: 0x000207AA File Offset: 0x0001E9AA
		public TutorReusableClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000D15 RID: 3349 RVA: 0x000207B5 File Offset: 0x0001E9B5
		public TutorReusableClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000D16 RID: 3350 RVA: 0x000207C4 File Offset: 0x0001E9C4
		public SearchForTutorsResp SearchForTutors(SearchForTutorsReq Request)
		{
			return this.WrapServiceMethod<SearchForTutorsResp>(() => this.Proxy.SearchForTutors(Request));
		}

		// Token: 0x06000D17 RID: 3351 RVA: 0x000207FC File Offset: 0x0001E9FC
		public LoadTutorByPersonIdResp LoadTutorByPersonId(LoadTutorByPersonIdReq Request)
		{
			return this.WrapServiceMethod<LoadTutorByPersonIdResp>(() => this.Proxy.LoadTutorByPersonId(Request));
		}

		// Token: 0x06000D18 RID: 3352 RVA: 0x00020834 File Offset: 0x0001EA34
		public TryToBookTutorAppointmentResp TryToBookTutorAppointment(TryToBookTutorAppointmentReq Request)
		{
			return this.WrapServiceMethod<TryToBookTutorAppointmentResp>(() => this.Proxy.TryToBookTutorAppointment(Request));
		}

		// Token: 0x06000D19 RID: 3353 RVA: 0x0002086C File Offset: 0x0001EA6C
		public IsConfidentialityAgreementSigningRequiredForTutorResp IsConfidentialityAgreementSigningRequiredForTutor(IsConfidentialityAgreementSigningRequiredForTutorReq Request)
		{
			return this.WrapServiceMethod<IsConfidentialityAgreementSigningRequiredForTutorResp>(() => this.Proxy.IsConfidentialityAgreementSigningRequiredForTutor(Request));
		}

		// Token: 0x06000D1A RID: 3354 RVA: 0x000208A4 File Offset: 0x0001EAA4
		public void RecordConfidentialityAgreementSignedByTutor(RecordConfidentialityAgreementSignedByTutorReq Request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.RecordConfidentialityAgreementSignedByTutor(Request);
			});
		}

		// Token: 0x06000D1B RID: 3355 RVA: 0x000208DC File Offset: 0x0001EADC
		public CreateTutorResp CreateTutor(CreateTutorReq Request)
		{
			return this.WrapServiceMethod<CreateTutorResp>(() => this.Proxy.CreateTutor(Request));
		}

		// Token: 0x06000D1C RID: 3356 RVA: 0x00020914 File Offset: 0x0001EB14
		public void RegisterTutorByExistingPersonId(RegisterTutorByExistingPersonIdReq Request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.RegisterTutorByExistingPersonId(Request);
			});
		}

		// Token: 0x06000D1D RID: 3357 RVA: 0x0002094C File Offset: 0x0001EB4C
		public GetTutorStatusResp GetTutorStatus(GetTutorStatusReq Request)
		{
			return this.WrapServiceMethod<GetTutorStatusResp>(() => this.Proxy.GetTutorStatus(Request));
		}

		// Token: 0x06000D1E RID: 3358 RVA: 0x00020984 File Offset: 0x0001EB84
		public LoadAllTutorsResp LoadAllTutors(LoadAllTutorsReq Request)
		{
			return this.WrapServiceMethod<LoadAllTutorsResp>(() => this.Proxy.LoadAllTutors(Request));
		}

		// Token: 0x06000D1F RID: 3359 RVA: 0x000209BC File Offset: 0x0001EBBC
		public void ActivateTutor(ActivateTutorReq Request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.ActivateTutor(Request);
			});
		}

		// Token: 0x06000D20 RID: 3360 RVA: 0x000209F4 File Offset: 0x0001EBF4
		public void DeActivateTutor(DeActivateTutorReq Request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.DeActivateTutor(Request);
			});
		}

		// Token: 0x06000D21 RID: 3361 RVA: 0x00020A2C File Offset: 0x0001EC2C
		public LoadTutorAppointmentResp LoadTutorAppointment(LoadTutorAppointmentReq Request)
		{
			return this.WrapServiceMethod<LoadTutorAppointmentResp>(() => this.Proxy.LoadTutorAppointment(Request));
		}

		// Token: 0x06000D22 RID: 3362 RVA: 0x00020A64 File Offset: 0x0001EC64
		public LoadTutorWithActiveStatusByIdResp LoadTutorWithActiveStatusById(LoadTutorWithActiveStatusByIdReq Request)
		{
			return this.WrapServiceMethod<LoadTutorWithActiveStatusByIdResp>(() => this.Proxy.LoadTutorWithActiveStatusById(Request));
		}

		// Token: 0x06000D23 RID: 3363 RVA: 0x00020A9C File Offset: 0x0001EC9C
		public GetTutorStatusesResp GetTutorStatuses(GetTutorStatusesReq Request)
		{
			return this.WrapServiceMethod<GetTutorStatusesResp>(() => this.Proxy.GetTutorStatuses(Request));
		}
	}
}
