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
	// Token: 0x02000025 RID: 37
	[ServiceContract(Name = "AppointmentIconService", Namespace = "http://tpro.ca")]
	[SoapHeaders]
	[XmlComments]
	public interface IAppointmentIcon : IService
	{
		// Token: 0x06000145 RID: 325
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadAppointmentIconsByAppointmentResp LoadAppointmentIconsByAppointment(LoadAppointmentIconsByAppointmentReq Request);

		// Token: 0x06000146 RID: 326
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadAppointmentIconResp LoadAppointmentIcon(LoadAppointmentIconReq Request);

		// Token: 0x06000147 RID: 327
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadAppointmentIconByIconNumResp LoadAppointmentIconByIconNum(LoadAppointmentIconByIconNumReq Request);

		// Token: 0x06000148 RID: 328
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadAppointmentIconByIconInfoIdResp LoadAppointmentIconByIconInfoId(LoadAppointmentIconByIconInfoIdReq Request);

		// Token: 0x06000149 RID: 329
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		void DeleteAppointmentIconsNotInList(DeleteAppointmentIconsNotInListReq Request);

		// Token: 0x0600014A RID: 330
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		InsertOrUpdateAppointmentIconResp InsertOrUpdateAppointmentIcon(InsertOrUpdateAppointmentIconReq Request);

		// Token: 0x0600014B RID: 331
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		void DeleteAppointmentIcon(DeleteAppointmentIconReq Request);

		// Token: 0x0600014C RID: 332
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadAllIconInfosResp LoadAllIconInfos(LoadAllIconInfosReq Request);
	}
}
