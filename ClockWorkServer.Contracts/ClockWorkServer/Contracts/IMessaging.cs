using System;
using System.Collections.Generic;
using System.ServiceModel;
using TechnoPro.ClockWorkServer.Contracts.Faults;
using TechnoPro.Common.Public;
using TechnoPro.Common.WCF.Attributes;
using TechnoPro.Common.WCF.Faults;
using WCFExtrasPlus.Soap;
using WCFExtrasPlus.Wsdl.Documentation;

namespace TechnoPro.ClockWorkServer.Contracts
{
	// Token: 0x020000CE RID: 206
	[ServiceContract(Name = "MessagingService", Namespace = "http://tpro.ca", SessionMode = SessionMode.Required, CallbackContract = typeof(IMessagingCallback))]
	[SoapHeaders]
	[XmlComments]
	[DualChannelService]
	public interface IMessaging : IService
	{
		// Token: 0x060005A5 RID: 1445
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		int CheckConnectivity();

		// Token: 0x060005A6 RID: 1446
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		IM_User Login();

		// Token: 0x060005A7 RID: 1447
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		void SendMessage(InstantMessage msg);

		// Token: 0x060005A8 RID: 1448
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		void SendAttachment(AttachmentFile att);

		// Token: 0x060005A9 RID: 1449
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		List<IM_User> GetOnlineUsers();

		// Token: 0x060005AA RID: 1450
		[OperationContract(Name = "GetOnlineUsersByGroup")]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		List<IM_User> GetOnlineUsers(OnlineUsersRequest onlineUsersRequest);

		// Token: 0x060005AB RID: 1451
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		List<string> GetOnlineGroups();

		// Token: 0x060005AC RID: 1452
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		void Logout();
	}
}
