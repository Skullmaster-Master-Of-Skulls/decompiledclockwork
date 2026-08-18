using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.ConfidentialityAgreement;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x02000069 RID: 105
	public class StudentConfidentialityAgreementReusableClientProxy : WCFTokenBasedReusableClientProxy<IStudentConfidentialityAgreement>, IStudentConfidentialityAgreement, IService
	{
		// Token: 0x06000484 RID: 1156 RVA: 0x0000CC56 File Offset: 0x0000AE56
		public StudentConfidentialityAgreementReusableClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000485 RID: 1157 RVA: 0x0000CC61 File Offset: 0x0000AE61
		public StudentConfidentialityAgreementReusableClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000486 RID: 1158 RVA: 0x0000CC70 File Offset: 0x0000AE70
		public SignedConfidentialityAgreementResp RecordSignedConfidentialityAgreement(SignedConfidentialityAgreementReq request)
		{
			return this.WrapServiceMethod<SignedConfidentialityAgreementResp>(() => this.Proxy.RecordSignedConfidentialityAgreement(request));
		}

		// Token: 0x06000487 RID: 1159 RVA: 0x0000CCA8 File Offset: 0x0000AEA8
		public LastStudentConfidentialityAgreementResp LastSignedStudentConfidentialityAgreement(LastStudentConfidentialityAgreementReq request)
		{
			return this.WrapServiceMethod<LastStudentConfidentialityAgreementResp>(() => this.Proxy.LastSignedStudentConfidentialityAgreement(request));
		}

		// Token: 0x06000488 RID: 1160 RVA: 0x0000CCE0 File Offset: 0x0000AEE0
		public IsConfidentialityAgreementSigningRequiredResp IsConfidentialityAgreementSigningRequired(IsConfidentialityAgreementSigningRequiredReq request)
		{
			return this.WrapServiceMethod<IsConfidentialityAgreementSigningRequiredResp>(() => this.Proxy.IsConfidentialityAgreementSigningRequired(request));
		}

		// Token: 0x06000489 RID: 1161 RVA: 0x0000CD18 File Offset: 0x0000AF18
		public GetStudentConfidentialityAgreementTextResp GetStudentConfidentialityAgreementText(GetStudentConfidentialityAgreementTextReq request)
		{
			return this.WrapServiceMethod<GetStudentConfidentialityAgreementTextResp>(() => this.Proxy.GetStudentConfidentialityAgreementText(request));
		}
	}
}
