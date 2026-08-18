using System;
using System.ServiceModel;
using System.Threading.Tasks;
using TechnoPro.ClockWorkServer.Contracts.DTO.CustomForms.Data;
using TechnoPro.ClockWorkServer.Contracts.Faults;
using TechnoPro.Common.Public;
using TechnoPro.Common.WCF.Faults;
using WCFExtrasPlus.Soap;
using WCFExtrasPlus.Wsdl.Documentation;

namespace TechnoPro.ClockWorkServer.Contracts
{
	// Token: 0x02000039 RID: 57
	[ServiceContract(Name = "CustomDataService", Namespace = "http://tpro.ca")]
	[SoapHeaders]
	[XmlComments]
	public interface ICustomData : IService
	{
		// Token: 0x060001D3 RID: 467
		[OperationContract(Name = "LoadCustomDataAsync")]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		Task<LoadCustomDataResp> LoadCustomDataAsync(LoadCustomDataReq Request);

		// Token: 0x060001D4 RID: 468
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadCustomDataResp LoadCustomData(LoadCustomDataReq Request);

		// Token: 0x060001D5 RID: 469
		[OperationContract(Name = "SaveCustomFormsDataAsync")]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		Task<SaveCustomFormsDataResp> SaveCustomFormsDataAsync(SaveCustomFormsDataReq Request);
	}
}
