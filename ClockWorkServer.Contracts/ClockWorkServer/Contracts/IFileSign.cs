using System;
using System.ServiceModel;
using TechnoPro.ClockWorkServer.Contracts.DTO.Storages;
using TechnoPro.ClockWorkServer.Contracts.Faults;
using TechnoPro.Common.Public;
using TechnoPro.Common.WCF.Faults;
using WCFExtrasPlus.Soap;
using WCFExtrasPlus.Wsdl.Documentation;

namespace TechnoPro.ClockWorkServer.Contracts
{
	// Token: 0x0200004D RID: 77
	[ServiceContract(Name = "FileSignService", Namespace = "http://tpro.ca")]
	[SoapHeaders]
	[XmlComments]
	public interface IFileSign : IService
	{
		// Token: 0x06000259 RID: 601
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		DecryptAndVerifyResp DecryptAndVerify(DecryptAndVerifyReq Request);

		// Token: 0x0600025A RID: 602
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		void DecryptAndVerifyUsingFileSystem(DecryptAndVerifyUsingFileSystemReq Request);
	}
}
