using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.ConfidentialityAgreement;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x0200006A RID: 106
	internal class StudentConfidentialityAgreementClientBaseProxy : ClientBase<IStudentConfidentialityAgreement>, IStudentConfidentialityAgreement, IService
	{
		// Token: 0x0600048A RID: 1162 RVA: 0x0000CD50 File Offset: 0x0000AF50
		public StudentConfidentialityAgreementClientBaseProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x0600048B RID: 1163 RVA: 0x0000CD5B File Offset: 0x0000AF5B
		public StudentConfidentialityAgreementClientBaseProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x0600048C RID: 1164 RVA: 0x0000CD68 File Offset: 0x0000AF68
		public SignedConfidentialityAgreementResp RecordSignedConfidentialityAgreement(SignedConfidentialityAgreementReq request)
		{
			return base.Channel.RecordSignedConfidentialityAgreement(request);
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x0000CD88 File Offset: 0x0000AF88
		public LastStudentConfidentialityAgreementResp LastSignedStudentConfidentialityAgreement(LastStudentConfidentialityAgreementReq request)
		{
			return base.Channel.LastSignedStudentConfidentialityAgreement(request);
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x0000CDA8 File Offset: 0x0000AFA8
		public IsConfidentialityAgreementSigningRequiredResp IsConfidentialityAgreementSigningRequired(IsConfidentialityAgreementSigningRequiredReq request)
		{
			return base.Channel.IsConfidentialityAgreementSigningRequired(request);
		}

		// Token: 0x0600048F RID: 1167 RVA: 0x0000CDC8 File Offset: 0x0000AFC8
		public GetStudentConfidentialityAgreementTextResp GetStudentConfidentialityAgreementText(GetStudentConfidentialityAgreementTextReq request)
		{
			return base.Channel.GetStudentConfidentialityAgreementText(request);
		}
	}
}
