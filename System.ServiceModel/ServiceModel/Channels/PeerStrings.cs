using System;
using System.Collections.Generic;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000A01 RID: 2561
	internal static class PeerStrings
	{
		// Token: 0x060065AF RID: 26031 RVA: 0x0017B024 File Offset: 0x00179224
		static PeerStrings()
		{
			PeerStrings.PopulateProtocolActions();
		}

		// Token: 0x060065B0 RID: 26032 RVA: 0x0017B038 File Offset: 0x00179238
		private static void PopulateProtocolActions()
		{
			PeerStrings.protocolActions.Add("http://schemas.microsoft.com/net/2006/05/peer/Connect", "Connect");
			PeerStrings.protocolActions.Add("http://schemas.microsoft.com/net/2006/05/peer/Welcome", "Welcome");
			PeerStrings.protocolActions.Add("http://schemas.microsoft.com/net/2006/05/peer/Refuse", "Refuse");
			PeerStrings.protocolActions.Add("http://schemas.microsoft.com/net/2006/05/peer/Disconnect", "Disconnect");
			PeerStrings.protocolActions.Add("RequestSecurityToken", "ProcessRequestSecurityToken");
			PeerStrings.protocolActions.Add("RequestSecurityTokenResponse", "RequestSecurityTokenResponse");
			PeerStrings.protocolActions.Add("http://schemas.microsoft.com/net/2006/05/peer/LinkUtility", "LinkUtility");
			PeerStrings.protocolActions.Add("http://www.w3.org/2005/08/addressing/fault", "Fault");
			PeerStrings.protocolActions.Add("http://schemas.microsoft.com/net/2006/05/peer/Ping", "Ping");
		}

		// Token: 0x060065B1 RID: 26033 RVA: 0x0017B0FC File Offset: 0x001792FC
		public static string FindAction(string action)
		{
			string result = null;
			PeerStrings.protocolActions.TryGetValue(action, out result);
			return result;
		}

		// Token: 0x04003A50 RID: 14928
		public static Dictionary<string, string> protocolActions = new Dictionary<string, string>();

		// Token: 0x04003A51 RID: 14929
		public const string Namespace = "http://schemas.microsoft.com/net/2006/05/peer";

		// Token: 0x04003A52 RID: 14930
		public const string ServiceContractName = "PeerService";

		// Token: 0x04003A53 RID: 14931
		public const string ConnectAction = "http://schemas.microsoft.com/net/2006/05/peer/Connect";

		// Token: 0x04003A54 RID: 14932
		public const string WelcomeAction = "http://schemas.microsoft.com/net/2006/05/peer/Welcome";

		// Token: 0x04003A55 RID: 14933
		public const string RefuseAction = "http://schemas.microsoft.com/net/2006/05/peer/Refuse";

		// Token: 0x04003A56 RID: 14934
		public const string DisconnectAction = "http://schemas.microsoft.com/net/2006/05/peer/Disconnect";

		// Token: 0x04003A57 RID: 14935
		public const string FloodAction = "http://schemas.microsoft.com/net/2006/05/peer/Flood";

		// Token: 0x04003A58 RID: 14936
		public const string InternalFloodAction = "http://schemas.microsoft.com/net/2006/05/peer/IntFlood";

		// Token: 0x04003A59 RID: 14937
		public const string LinkUtilityAction = "http://schemas.microsoft.com/net/2006/05/peer/LinkUtility";

		// Token: 0x04003A5A RID: 14938
		public const string RequestSecurityTokenAction = "RequestSecurityToken";

		// Token: 0x04003A5B RID: 14939
		public const string RequestSecurityTokenResponseAction = "RequestSecurityTokenResponse";

		// Token: 0x04003A5C RID: 14940
		public const string HopCountElementName = "Hops";

		// Token: 0x04003A5D RID: 14941
		public const string HopCountElementNamespace = "http://schemas.microsoft.com/net/2006/05/peer/HopCount";

		// Token: 0x04003A5E RID: 14942
		public const string PingAction = "http://schemas.microsoft.com/net/2006/05/peer/Ping";

		// Token: 0x04003A5F RID: 14943
		public const string Scheme = "net.p2p";

		// Token: 0x04003A60 RID: 14944
		public const string KnownServiceUriPrefix = "PeerChannelEndpoints";

		// Token: 0x04003A61 RID: 14945
		public const string PeerCustomResolver = "PeerCustomResolver";

		// Token: 0x04003A62 RID: 14946
		public const string SkipLocalChannels = "SkipLocalChannels";

		// Token: 0x04003A63 RID: 14947
		public const string Via = "PeerVia";

		// Token: 0x04003A64 RID: 14948
		public const string MessageVerified = "MessageVerified";

		// Token: 0x04003A65 RID: 14949
		public const string CacheMiss = "CacheMiss";

		// Token: 0x04003A66 RID: 14950
		public const string PeerProperty = "PeerProperty";

		// Token: 0x04003A67 RID: 14951
		public const string MessageId = "MessageID";
	}
}
