using System;
using System.ServiceModel;
using System.Threading.Tasks;
using TechnoPro.ClockWorkServer.Contracts.DTO.CustomForms.Field;
using TechnoPro.ClockWorkServer.Contracts.Faults;
using TechnoPro.Common.Public;
using TechnoPro.Common.WCF.Faults;
using WCFExtrasPlus.Soap;
using WCFExtrasPlus.Wsdl.Documentation;

namespace TechnoPro.ClockWorkServer.Contracts
{
	// Token: 0x0200003A RID: 58
	[ServiceContract(Name = "CustomFieldService", Namespace = "http://tpro.ca")]
	[SoapHeaders]
	[XmlComments]
	public interface ICustomField : IService
	{
		// Token: 0x060001D6 RID: 470
		[OperationContract(Name = "CreateDataInstanceAsync")]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		Task<CreateDataInstanceResp> CreateDataInstanceAsync(CreateDataInstanceReq Request);

		// Token: 0x060001D7 RID: 471
		[OperationContract(Name = "DeleteDataInstanceAsync")]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		Task<DeleteDataInstanceResp> DeleteDataInstanceAsync(DeleteDataInstanceReq Request);

		// Token: 0x060001D8 RID: 472
		[OperationContract(Name = "UpdateDataInstanceAsync")]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		Task<UpdateDataInstanceResp> UpdateDataInstanceAsync(UpdateDataInstanceReq Request);
	}
}
