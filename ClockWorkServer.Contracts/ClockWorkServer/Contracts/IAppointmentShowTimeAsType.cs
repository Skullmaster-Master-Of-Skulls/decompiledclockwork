using System;
using System.ServiceModel;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments.BaseAppParameters;
using TechnoPro.ClockWorkServer.Contracts.Faults;
using TechnoPro.Common.Public;
using TechnoPro.Common.WCF.Faults;
using WCFExtrasPlus.Soap;
using WCFExtrasPlus.Wsdl.Documentation;

namespace TechnoPro.ClockWorkServer.Contracts
{
	// Token: 0x02000026 RID: 38
	[ServiceContract(Name = "AppointmentShowTimeAsTypeService", Namespace = "http://tpro.ca")]
	[SoapHeaders]
	[XmlComments]
	public interface IAppointmentShowTimeAsType : IService
	{
		// Token: 0x0600014D RID: 333
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadAllShowTimeAsTypesResp LoadAllShowTimeAsTypes(LoadAllShowTimeAsTypesReq Request);

		// Token: 0x0600014E RID: 334
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadShowTimeAsTypeByAppCodeResp LoadShowTimeAsTypeByAppCode(LoadShowTimeAsTypeByAppCodeReq Request);

		// Token: 0x0600014F RID: 335
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadShowTimeAsTypeByIdResp LoadShowTimeAsTypeById(LoadShowTimeAsTypeByIdReq Request);

		// Token: 0x06000150 RID: 336
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		void DeleteShowTimeAsTypeByAppCode(DeleteShowTimeAsTypeByAppCodeReq Request);

		// Token: 0x06000151 RID: 337
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		void DeleteShowTimeAsTypeById(DeleteShowTimeAsTypeByIdReq Request);

		// Token: 0x06000152 RID: 338
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		void UpdateShowTimeAsType(UpdateShowTimeAsTypeReq Request);

		// Token: 0x06000153 RID: 339
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		CreateShowTimeAsTypeResp CreateShowTimeAsType(CreateShowTimeAsTypeReq Request);
	}
}
