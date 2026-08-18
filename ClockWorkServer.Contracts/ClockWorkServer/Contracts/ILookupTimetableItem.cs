using System;
using System.ServiceModel;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;
using TechnoPro.ClockWorkServer.Contracts.Faults;
using TechnoPro.Common.Public;
using TechnoPro.Common.WCF.Faults;
using WCFExtrasPlus.Soap;
using WCFExtrasPlus.Wsdl.Documentation;

namespace TechnoPro.ClockWorkServer.Contracts
{
	// Token: 0x02000067 RID: 103
	[ServiceContract(Name = "LookupTimetableItemService", Namespace = "http://tpro.ca")]
	[SoapHeaders]
	[XmlComments]
	public interface ILookupTimetableItem : IService
	{
		// Token: 0x06000306 RID: 774
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadLookupTimetableItemResp LoadLookupTimetableItem(LoadLookupTimetableItemReq Request);

		// Token: 0x06000307 RID: 775
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		void SaveLookupTimetableItems(SaveLookupTimetableItemsReq Request);
	}
}
