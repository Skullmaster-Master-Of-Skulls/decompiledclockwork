using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Tutoring;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x02000153 RID: 339
	public class StudentTuteeReusableClientProxy : WCFTokenBasedReusableClientProxy<IStudentTutee>, IStudentTutee, IService
	{
		// Token: 0x06000D04 RID: 3332 RVA: 0x0002059E File Offset: 0x0001E79E
		public StudentTuteeReusableClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000D05 RID: 3333 RVA: 0x000205A9 File Offset: 0x0001E7A9
		public StudentTuteeReusableClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000D06 RID: 3334 RVA: 0x000205B8 File Offset: 0x0001E7B8
		public GetStudentMyTutorsResp GetStudentMyTutors(GetStudentMyTutorsReq Request)
		{
			return this.WrapServiceMethod<GetStudentMyTutorsResp>(() => this.Proxy.GetStudentMyTutors(Request));
		}

		// Token: 0x06000D07 RID: 3335 RVA: 0x000205F0 File Offset: 0x0001E7F0
		public void MarkStudentCantFindAvailability(MarkStudentCantFindAvailabilityReq Request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.MarkStudentCantFindAvailability(Request);
			});
		}

		// Token: 0x06000D08 RID: 3336 RVA: 0x00020628 File Offset: 0x0001E828
		public void MarkStudentCantFindTutor(MarkStudentCantFindTutorReq Request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.MarkStudentCantFindTutor(Request);
			});
		}

		// Token: 0x06000D09 RID: 3337 RVA: 0x00020660 File Offset: 0x0001E860
		public GetTuteeStatusResp GetTuteeStatus(GetTuteeStatusReq Request)
		{
			return this.WrapServiceMethod<GetTuteeStatusResp>(() => this.Proxy.GetTuteeStatus(Request));
		}

		// Token: 0x06000D0A RID: 3338 RVA: 0x00020698 File Offset: 0x0001E898
		public IsConfidentialityAgreementSigningRequiredForStudentResp IsConfidentialityAgreementSigningRequiredForStudent(IsConfidentialityAgreementSigningRequiredForStudentReq Request)
		{
			return this.WrapServiceMethod<IsConfidentialityAgreementSigningRequiredForStudentResp>(() => this.Proxy.IsConfidentialityAgreementSigningRequiredForStudent(Request));
		}

		// Token: 0x06000D0B RID: 3339 RVA: 0x000206D0 File Offset: 0x0001E8D0
		public void RecordConfidentialityAgreementSignedByStudent(RecordConfidentialityAgreementSignedByStudentReq Request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.RecordConfidentialityAgreementSignedByStudent(Request);
			});
		}
	}
}
