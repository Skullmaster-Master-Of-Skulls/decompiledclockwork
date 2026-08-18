using System;
using TechnoPro.ClockWorkServer.Common.Services.Impl.Adapters;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.ConfidentialityAgreement;
using TechnoPro.Common.Core.ConfidentialityAgreement;
using TechnoPro.Common.Core.Mappers.ConfidentialityAgreement;
using TechnoPro.Common.ICore.ConfidentialityAgreement;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.OperationContexts;

namespace TechnoPro.ClockWorkServer.Common.Services.Impl
{
	// Token: 0x0200002F RID: 47
	public class StudentConfidentialityAgreementServiceManager : IStudentConfidentialityAgreement, IService
	{
		// Token: 0x060001E5 RID: 485 RVA: 0x000098EC File Offset: 0x00007AEC
		public SignedConfidentialityAgreementResp RecordSignedConfidentialityAgreement(SignedConfidentialityAgreementReq request)
		{
			ConfidentialityAgreementOperationContext operationContext = request.GetOperationContext<ConfidentialityAgreementOperationContext>();
			operationContext.Module = request.Module;
			IStudentConfidentilityAgreementManager studentConfidentilityAgreementManager = new StudentConfidentialityAgreementManager(operationContext);
			studentConfidentilityAgreementManager.RecordSignedConfidentialityAgreement(request.PersonId);
			return new SignedConfidentialityAgreementResp();
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x0000992C File Offset: 0x00007B2C
		public LastStudentConfidentialityAgreementResp LastSignedStudentConfidentialityAgreement(LastStudentConfidentialityAgreementReq request)
		{
			ConfidentialityAgreementOperationContext operationContext = request.GetOperationContext<ConfidentialityAgreementOperationContext>();
			operationContext.Module = request.Module;
			IStudentConfidentilityAgreementManager studentConfidentilityAgreementManager = new StudentConfidentialityAgreementManager(operationContext);
			return new LastStudentConfidentialityAgreementResp
			{
				ConfidentialityAgreement = studentConfidentilityAgreementManager.LastSignedStudentConfidentialityAgreement(request.PersonId).ToDTO()
			};
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x00009978 File Offset: 0x00007B78
		public IsConfidentialityAgreementSigningRequiredResp IsConfidentialityAgreementSigningRequired(IsConfidentialityAgreementSigningRequiredReq request)
		{
			ConfidentialityAgreementOperationContext operationContext = request.GetOperationContext<ConfidentialityAgreementOperationContext>();
			operationContext.Module = request.Module;
			IStudentConfidentilityAgreementManager studentConfidentilityAgreementManager = new StudentConfidentialityAgreementManager(operationContext);
			return new IsConfidentialityAgreementSigningRequiredResp
			{
				IsSigningRequired = studentConfidentilityAgreementManager.IsConfidentialityAgreementSigningRequired(request.PersonId)
			};
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x000099C0 File Offset: 0x00007BC0
		public GetStudentConfidentialityAgreementTextResp GetStudentConfidentialityAgreementText(GetStudentConfidentialityAgreementTextReq request)
		{
			ConfidentialityAgreementOperationContext operationContext = request.GetOperationContext<ConfidentialityAgreementOperationContext>();
			operationContext.Module = request.Module;
			IStudentConfidentilityAgreementManager studentConfidentilityAgreementManager = new StudentConfidentialityAgreementManager(operationContext);
			return new GetStudentConfidentialityAgreementTextResp
			{
				ConfidentialityAgreementText = studentConfidentilityAgreementManager.GetStudentConfidentialityAgreementText(request.PersonId)
			};
		}
	}
}
