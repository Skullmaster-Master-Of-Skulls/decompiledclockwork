using System;
using System.ServiceModel;
using TechnoPro.ClockWorkServer.Contracts.DTO.Settings;
using TechnoPro.ClockWorkServer.Contracts.Faults;
using TechnoPro.Common.Public;
using TechnoPro.Common.WCF.Faults;
using WCFExtrasPlus.Soap;
using WCFExtrasPlus.Wsdl.Documentation;

namespace TechnoPro.ClockWorkServer.Contracts
{
	// Token: 0x02000084 RID: 132
	[ServiceContract(Name = "ReferenceTableSettingService", Namespace = "http://tpro.ca")]
	[SoapHeaders]
	[XmlComments]
	public interface IReferenceTableSetting : IService
	{
		// Token: 0x060003B1 RID: 945
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		GetValuesFromColumnResp GetValuesFromColumn(GetValuesFromColumnReq request);

		// Token: 0x060003B2 RID: 946
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		GetValuesFromColumnsResp GetValuesFromColumns(GetValuesFromColumnsReq request);
	}
}
