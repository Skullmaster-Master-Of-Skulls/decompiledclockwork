using System;
using System.ServiceModel;
using System.Threading.Tasks;
using TechnoPro.ClockWorkServer.Contracts.DTO.CustomForms.Form;
using TechnoPro.ClockWorkServer.Contracts.Faults;
using TechnoPro.Common.Public;
using TechnoPro.Common.WCF.Faults;
using WCFExtrasPlus.Soap;
using WCFExtrasPlus.Wsdl.Documentation;

namespace TechnoPro.ClockWorkServer.Contracts
{
	// Token: 0x0200003B RID: 59
	[ServiceContract(Name = "CustomFormService", Namespace = "http://tpro.ca")]
	[SoapHeaders]
	[XmlComments]
	public interface ICustomForm : IService
	{
		// Token: 0x060001D9 RID: 473
		[OperationContract(Name = "LoadFormByIdAsync")]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		Task<LoadFormByIdResp> LoadFormByIdAsync(LoadFormByIdReq Request);

		// Token: 0x060001DA RID: 474
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadFormByIdResp LoadFormById(LoadFormByIdReq Request);

		// Token: 0x060001DB RID: 475
		[OperationContract(Name = "CreateCustomFormAsync")]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		Task<CreateCustomFormResp> CreateCustomFormAsync(CreateCustomFormReq Request);

		// Token: 0x060001DC RID: 476
		[OperationContract(Name = "DeleteCustomFormAsync")]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		Task<DeleteCustomFormResp> DeleteCustomFormAsync(DeleteCustomFormReq Request);

		// Token: 0x060001DD RID: 477
		[OperationContract(Name = "UpdateCustomFormAsync")]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		Task<UpdateCustomFormResp> UpdateCustomFormAsync(UpdateCustomFormReq Request);

		// Token: 0x060001DE RID: 478
		[OperationContract(Name = "LoadAllCustomFormsAsync")]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		Task<LoadAllCustomFormsResp> LoadAllCustomFormsAsync(LoadAllCustomFormsReq Request);
	}
}
