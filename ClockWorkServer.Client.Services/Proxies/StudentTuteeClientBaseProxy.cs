using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Tutoring;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x02000154 RID: 340
	internal class StudentTuteeClientBaseProxy : ClientBase<IStudentTutee>, IStudentTutee, IService
	{
		// Token: 0x06000D0C RID: 3340 RVA: 0x00020705 File Offset: 0x0001E905
		public StudentTuteeClientBaseProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000D0D RID: 3341 RVA: 0x00020710 File Offset: 0x0001E910
		public StudentTuteeClientBaseProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000D0E RID: 3342 RVA: 0x0002071C File Offset: 0x0001E91C
		public GetStudentMyTutorsResp GetStudentMyTutors(GetStudentMyTutorsReq Request)
		{
			return base.Channel.GetStudentMyTutors(Request);
		}

		// Token: 0x06000D0F RID: 3343 RVA: 0x0002073A File Offset: 0x0001E93A
		public void MarkStudentCantFindAvailability(MarkStudentCantFindAvailabilityReq Request)
		{
			base.Channel.MarkStudentCantFindAvailability(Request);
		}

		// Token: 0x06000D10 RID: 3344 RVA: 0x0002074A File Offset: 0x0001E94A
		public void MarkStudentCantFindTutor(MarkStudentCantFindTutorReq Request)
		{
			base.Channel.MarkStudentCantFindTutor(Request);
		}

		// Token: 0x06000D11 RID: 3345 RVA: 0x0002075C File Offset: 0x0001E95C
		public GetTuteeStatusResp GetTuteeStatus(GetTuteeStatusReq Request)
		{
			return base.Channel.GetTuteeStatus(Request);
		}

		// Token: 0x06000D12 RID: 3346 RVA: 0x0002077C File Offset: 0x0001E97C
		public IsConfidentialityAgreementSigningRequiredForStudentResp IsConfidentialityAgreementSigningRequiredForStudent(IsConfidentialityAgreementSigningRequiredForStudentReq Request)
		{
			return base.Channel.IsConfidentialityAgreementSigningRequiredForStudent(Request);
		}

		// Token: 0x06000D13 RID: 3347 RVA: 0x0002079A File Offset: 0x0001E99A
		public void RecordConfidentialityAgreementSignedByStudent(RecordConfidentialityAgreementSignedByStudentReq Request)
		{
			base.Channel.RecordConfidentialityAgreementSignedByStudent(Request);
		}
	}
}
