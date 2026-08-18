using System;
using System.ServiceModel;
using TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat;
using TechnoPro.ClockWorkServer.Contracts.Faults;
using TechnoPro.Common.Public;
using TechnoPro.Common.WCF.Faults;
using WCFExtrasPlus.Soap;
using WCFExtrasPlus.Wsdl.Documentation;

namespace TechnoPro.ClockWorkServer.Contracts
{
	// Token: 0x0200000B RID: 11
	[ServiceContract(Name = "MediaJobVolunteerService", Namespace = "http://tpro.ca")]
	[SoapHeaders]
	[XmlComments]
	public interface IMediaJobVolunteer : IService
	{
		// Token: 0x06000050 RID: 80
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		GetAllMediaJobVolunteersResp GetAllMediaJobVolunteers(GetAllMediaJobVolunteersReq request);

		// Token: 0x06000051 RID: 81
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		AddMediaJobVolunteerResp AddMediaJobVolunteer(AddMediaJobVolunteerReq request);

		// Token: 0x06000052 RID: 82
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		UpdateMediaJobVolunteerResp UpdateMediaJobVolunteer(UpdateMediaJobVolunteerReq request);

		// Token: 0x06000053 RID: 83
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		DeleteMediaJobVolunteerResp DeleteMediaJobVolunteer(DeleteMediaJobVolunteerReq request);

		// Token: 0x06000054 RID: 84
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		GetMediaVolunteerByIdResp GetMediaVolunteerById(GetMediaVolunteerByIdReq request);

		// Token: 0x06000055 RID: 85
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		GetMediaVolunteerByPersonIdResp GetMediaVolunteerByPersonId(GetMediaVolunteerByPersonIdReq request);

		// Token: 0x06000056 RID: 86
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		GetMediaVolunteerByVolunteerAndJobResp GetMediaVolunteerByVolunteerAndJob(GetMediaVolunteerByVolunteerAndJobReq request);

		// Token: 0x06000057 RID: 87
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		GetMediaVolunteersAssignedToMediaJobResp GetMediaVolunteersAssignedToMediaJob(GetMediaVolunteersAssignedToMediaJobReq request);

		// Token: 0x06000058 RID: 88
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		GetMediaJobVolunteerInfoByVolunteerResp GetMediaJobVolunteerInfoByVolunteer(GetMediaJobVolunteerInfoByVolunteerReq request);

		// Token: 0x06000059 RID: 89
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		CreateMediaJobVolunteerResp CreateMediaJobVolunteer(CreateMediaJobVolunteerReq request);

		// Token: 0x0600005A RID: 90
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		ChangeMediaJobVolunteerNotesResp ChangeMediaJobVolunteerNotes(ChangeMediaJobVolunteerNotesReq request);

		// Token: 0x0600005B RID: 91
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		ChangeMediaJobVolunteerActiveStatusResp ChangeMediaJobVolunteerActiveStatus(ChangeMediaJobVolunteerActiveStatusReq request);

		// Token: 0x0600005C RID: 92
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		ChangeMediaJobVolunteerListActiveStatusResp ChangeMediaJobVolunteerListActiveStatus(ChangeMediaJobVolunteerListActiveStatusReq request);

		// Token: 0x0600005D RID: 93
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		GetMediaJobVolunteerWorkingHoursByVolunteerAndJobResp GetMediaJobVolunteerWorkingHoursByVolunteerAndJob(GetMediaJobVolunteerWorkingHoursByVolunteerAndJobReq request);

		// Token: 0x0600005E RID: 94
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		GetAllMediaJobVolunteerWorkingHoursByVolunteerIdResp GetAllMediaJobVolunteerWorkingHoursByVolunteerId(GetAllMediaJobVolunteerWorkingHoursByVolunteerIdReq request);

		// Token: 0x0600005F RID: 95
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		AddMediaJobVolunteerWorkingHoursResp AddMediaJobVolunteerWorkingHours(AddMediaJobVolunteerWorkingHoursReq request);

		// Token: 0x06000060 RID: 96
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		UpdateMediaJobVolunteerWorkingHoursResp UpdateMediaJobVolunteerWorkingHours(UpdateMediaJobVolunteerWorkingHoursReq request);

		// Token: 0x06000061 RID: 97
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		DeleteMediaJobVolunteerWorkingHoursResp DeleteMediaJobVolunteerWorkingHours(DeleteMediaJobVolunteerWorkingHoursReq request);
	}
}
