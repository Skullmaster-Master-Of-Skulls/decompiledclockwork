using System;
using System.Data;
using System.ServiceModel;
using TechnoPro.ClockWorkServer.Contracts.DataContracts;
using TechnoPro.ClockWorkServer.Contracts.Faults;
using TechnoPro.Common.Public;
using TechnoPro.Common.WCF.Faults;
using WCFExtrasPlus.Soap;
using WCFExtrasPlus.Wsdl.Documentation;

namespace TechnoPro.ClockWorkServer.Contracts
{
	// Token: 0x020000D1 RID: 209
	[ServiceContract(Name = "QueryingService", Namespace = "http://tpro.ca")]
	[SoapHeaders]
	[XmlComments]
	public interface IQuerying : IService
	{
		// Token: 0x060005B5 RID: 1461
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		DataTable ExecuteQuery(string query);

		// Token: 0x060005B6 RID: 1462
		[OperationContract(Name = "ExecuteQueryWithParameters")]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		DataTable ExecuteQuery(string query, CWDbParameter[] parameters);

		// Token: 0x060005B7 RID: 1463
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		int ExecuteNonQuery(string query);

		// Token: 0x060005B8 RID: 1464
		[OperationContract(Name = "ExecuteNonQueryWithParameters")]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		int ExecuteNonQuery(string query, CWDbParameter[] parameters);
	}
}
