using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000A39 RID: 2617
	[ServiceContract(Name = "PeerService", Namespace = "http://schemas.microsoft.com/net/2006/05/peer", SessionMode = SessionMode.Required, CallbackContract = typeof(IPeerServiceContract))]
	internal interface IPeerServiceContract
	{
		// Token: 0x060067D6 RID: 26582
		[OperationContract(IsOneWay = true, Action = "http://schemas.microsoft.com/net/2006/05/peer/Connect")]
		void Connect(ConnectInfo connectInfo);

		// Token: 0x060067D7 RID: 26583
		[OperationContract(IsOneWay = true, Action = "http://schemas.microsoft.com/net/2006/05/peer/Disconnect")]
		void Disconnect(DisconnectInfo disconnectInfo);

		// Token: 0x060067D8 RID: 26584
		[OperationContract(IsOneWay = true, Action = "http://schemas.microsoft.com/net/2006/05/peer/Refuse")]
		void Refuse(RefuseInfo refuseInfo);

		// Token: 0x060067D9 RID: 26585
		[OperationContract(IsOneWay = true, Action = "http://schemas.microsoft.com/net/2006/05/peer/Welcome")]
		void Welcome(WelcomeInfo welcomeInfo);

		// Token: 0x060067DA RID: 26586
		[OperationContract(IsOneWay = true, Action = "http://schemas.microsoft.com/net/2006/05/peer/Flood", AsyncPattern = true)]
		IAsyncResult BeginFloodMessage(Message floodedInfo, AsyncCallback callback, object state);

		// Token: 0x060067DB RID: 26587
		void EndFloodMessage(IAsyncResult result);

		// Token: 0x060067DC RID: 26588
		[OperationContract(IsOneWay = true, Action = "http://schemas.microsoft.com/net/2006/05/peer/LinkUtility")]
		void LinkUtility(UtilityInfo utilityInfo);

		// Token: 0x060067DD RID: 26589
		[OperationContract(Action = "RequestSecurityToken", ReplyAction = "RequestSecurityTokenResponse")]
		Message ProcessRequestSecurityToken(Message message);

		// Token: 0x060067DE RID: 26590
		[OperationContract(IsOneWay = true, Action = "http://schemas.microsoft.com/net/2006/05/peer/Ping")]
		void Ping(Message message);

		// Token: 0x060067DF RID: 26591
		[OperationContract(IsOneWay = true, Action = "http://www.w3.org/2005/08/addressing/fault")]
		void Fault(Message message);
	}
}
