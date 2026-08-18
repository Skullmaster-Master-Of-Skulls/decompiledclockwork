using System;
using System.ServiceModel.Channels;
using System.Xml;

namespace System.ServiceModel.Security
{
	// Token: 0x02000312 RID: 786
	public interface ISecureConversationSession : ISecuritySession, ISession
	{
		// Token: 0x06001B2B RID: 6955
		void WriteSessionTokenIdentifier(XmlDictionaryWriter writer);

		// Token: 0x06001B2C RID: 6956
		bool TryReadSessionTokenIdentifier(XmlReader reader);
	}
}
