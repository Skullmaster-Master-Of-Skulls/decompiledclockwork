using System;
using System.ServiceModel.Security;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000A64 RID: 2660
	internal static class SecurityStandardsHelper
	{
		// Token: 0x0600690D RID: 26893 RVA: 0x00188983 File Offset: 0x00186B83
		private static SecurityStandardsManager CreateStandardsManager(MessageSecurityVersion securityVersion)
		{
			return new SecurityStandardsManager(securityVersion, new WSSecurityTokenSerializer(securityVersion.SecurityVersion, securityVersion.TrustVersion, securityVersion.SecureConversationVersion, false, null, null, null));
		}

		// Token: 0x0600690E RID: 26894 RVA: 0x001889A6 File Offset: 0x00186BA6
		public static SecurityStandardsManager CreateStandardsManager(TransactionProtocol transactionProtocol)
		{
			if (transactionProtocol == TransactionProtocol.WSAtomicTransactionOctober2004 || transactionProtocol == TransactionProtocol.OleTransactions)
			{
				return SecurityStandardsManager.DefaultInstance;
			}
			return SecurityStandardsHelper.SecurityStandardsManager2007;
		}

		// Token: 0x04003C28 RID: 15400
		private static SecurityStandardsManager SecurityStandardsManager2007 = SecurityStandardsHelper.CreateStandardsManager(MessageSecurityVersion.WSSecurity11WSTrust13WSSecureConversation13WSSecurityPolicy12);
	}
}
