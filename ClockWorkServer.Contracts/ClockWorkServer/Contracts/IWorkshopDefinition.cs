using System;
using System.ServiceModel;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsWorkshops;
using TechnoPro.ClockWorkServer.Contracts.Faults;
using TechnoPro.Common.Public;
using TechnoPro.Common.WCF.Faults;
using WCFExtrasPlus.Soap;
using WCFExtrasPlus.Wsdl.Documentation;

namespace TechnoPro.ClockWorkServer.Contracts
{
	// Token: 0x02000022 RID: 34
	[ServiceContract(Name = "WorkshopDefinitionService", Namespace = "http://tpro.ca")]
	[SoapHeaders]
	[XmlComments]
	public interface IWorkshopDefinition : IService
	{
		// Token: 0x06000134 RID: 308
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadWorkshopDefinitionsResp LoadWorkshopDefinitions(LoadWorkshopDefinitionsReq Request);

		// Token: 0x06000135 RID: 309
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		DeleteWorkshopDefinitionResp DeleteWorkshopDefinition(DeleteWorkshopDefinitionReq Request);

		// Token: 0x06000136 RID: 310
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		CreateWorkshopDefinitionResp CreateWorkshopDefinition(CreateWorkshopDefinitionReq Request);

		// Token: 0x06000137 RID: 311
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		UpdateWorkshopDefinitionResp UpdateWorkshopDefinition(UpdateWorkshopDefinitionReq Request);

		// Token: 0x06000138 RID: 312
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadAllWorkshopAppTypesResp LoadAllWorkshopAppTypes(LoadAllWorkshopAppTypesReq Request);

		// Token: 0x06000139 RID: 313
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadWorkshopDefinitionByIdResp LoadWorkshopDefinitionById(LoadWorkshopDefinitionByIdReq Request);

		// Token: 0x0600013A RID: 314
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadWorkDefinitionsByAppTypeResp LoadWorkshopDefinitionsByAppType(LoadWorkDefinitionsByAppTypeReq request);

		// Token: 0x0600013B RID: 315
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadAppTypesWithWorkshopDefinitionsResp LoadAppTypesWithWorkshopDefinitions(LoadAppTypesWithWorkshopDefinitionsReq Request);
	}
}
