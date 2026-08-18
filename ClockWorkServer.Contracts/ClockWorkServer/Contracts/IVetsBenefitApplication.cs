using System;
using System.ServiceModel;
using System.Threading.Tasks;
using TechnoPro.ClockWorkServer.Contracts.DTO.Vets;
using TechnoPro.ClockWorkServer.Contracts.Faults;
using TechnoPro.Common.Public;
using TechnoPro.Common.WCF.Faults;
using WCFExtrasPlus.Soap;
using WCFExtrasPlus.Wsdl.Documentation;

namespace TechnoPro.ClockWorkServer.Contracts
{
	// Token: 0x020000A6 RID: 166
	[ServiceContract(Name = "VetsBenefitApplicationService", Namespace = "http://tpro.ca")]
	[SoapHeaders]
	[XmlComments]
	public interface IVetsBenefitApplication : IService
	{
		// Token: 0x060004D9 RID: 1241
		[OperationContract(Name = "LoadBenefitApplicationByIdAsync")]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		Task<LoadBenefitApplicationByIdResp> LoadBenefitApplicationByIdAsync(LoadBenefitApplicationByIdReq Request);

		// Token: 0x060004DA RID: 1242
		[OperationContract(Name = "LoadBenefitApplicationByIdAsync")]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		Task<LoadBenefitApplicationBaseAndSingleStepDataResp> LoadBenefitApplicationBaseAndSingleStepData(LoadBenefitApplicationBaseAndSingleStepDataReq Request);

		// Token: 0x060004DB RID: 1243
		[OperationContract(Name = "SaveVetsChapterAsync")]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		Task<SaveVetsChapterResp> SaveVetsChapterAsync(SaveVetsChapterReq Request);

		// Token: 0x060004DC RID: 1244
		[OperationContract(Name = "SaveVetsRegistrationDataAsync")]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		Task<SaveVetsRegistrationDataResp> SaveVetsRegistrationDataAsync(SaveVetsRegistrationDataReq Request);

		// Token: 0x060004DD RID: 1245
		[OperationContract(Name = "SaveVetsBenAppDataAsync")]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		Task<SaveVetsBenAppDataResp> SaveVetsBenAppDataAsync(SaveVetsBenAppDataReq Request);

		// Token: 0x060004DE RID: 1246
		[OperationContract(Name = "SaveVetsStudentAgreeDataAsync")]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		Task<SaveVetsStudentAgreeDataResp> SaveVetsStudentAgreeDataAsync(SaveVetsStudentAgreeDataReq Request);

		// Token: 0x060004DF RID: 1247
		[OperationContract(Name = "CreateVetsBenefitApplicationAsync")]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		Task<CreateVetsBenefitApplicationResp> CreateVetsBenefitApplicationAsync(CreateVetsBenefitApplicationReq Request);

		// Token: 0x060004E0 RID: 1248
		[OperationContract(Name = "CreateVetsBenefitApplicationCurrentSemesterAsync")]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		Task<CreateVetsBenefitApplicationCurrentSemesterResp> CreateVetsBenefitApplicationCurrentSemesterAsync(CreateVetsBenefitApplicationCurrentSemesterReq Request);

		// Token: 0x060004E1 RID: 1249
		[OperationContract(Name = "CreateVetsBenefitApplicationNextSemesterAsync")]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		Task<CreateVetsBenefitApplicationNextSemesterResp> CreateVetsBenefitApplicationNextSemesterAsync(CreateVetsBenefitApplicationNextSemesterReq Request);
	}
}
