using System;
using System.Collections;
using System.Globalization;
using System.Runtime.Remoting.Activation;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x020006C9 RID: 1737
	[Serializable]
	internal class InternalSink
	{
		// Token: 0x06003EB6 RID: 16054 RVA: 0x000D6FD8 File Offset: 0x000D5FD8
		internal static IMessage ValidateMessage(IMessage reqMsg)
		{
			IMessage result = null;
			if (reqMsg == null)
			{
				result = new ReturnMessage(new ArgumentNullException("reqMsg"), null);
			}
			return result;
		}

		// Token: 0x06003EB7 RID: 16055 RVA: 0x000D6FFC File Offset: 0x000D5FFC
		internal static IMessage DisallowAsyncActivation(IMessage reqMsg)
		{
			if (reqMsg is IConstructionCallMessage)
			{
				return new ReturnMessage(new RemotingException(Environment.GetResourceString("Remoting_Activation_AsyncUnsupported")), null);
			}
			return null;
		}

		// Token: 0x06003EB8 RID: 16056 RVA: 0x000D7020 File Offset: 0x000D6020
		internal static Identity GetIdentity(IMessage reqMsg)
		{
			Identity identity = null;
			if (reqMsg is IInternalMessage)
			{
				identity = ((IInternalMessage)reqMsg).IdentityObject;
			}
			else if (reqMsg is InternalMessageWrapper)
			{
				identity = (Identity)((InternalMessageWrapper)reqMsg).GetIdentityObject();
			}
			if (identity == null)
			{
				string uri = InternalSink.GetURI(reqMsg);
				identity = IdentityHolder.ResolveIdentity(uri);
				if (identity == null)
				{
					throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_ServerObjectNotFound"), new object[]
					{
						uri
					}));
				}
			}
			return identity;
		}

		// Token: 0x06003EB9 RID: 16057 RVA: 0x000D709C File Offset: 0x000D609C
		internal static ServerIdentity GetServerIdentity(IMessage reqMsg)
		{
			ServerIdentity serverIdentity = null;
			bool flag = false;
			IInternalMessage internalMessage = reqMsg as IInternalMessage;
			if (internalMessage != null)
			{
				serverIdentity = ((IInternalMessage)reqMsg).ServerIdentityObject;
				flag = true;
			}
			else if (reqMsg is InternalMessageWrapper)
			{
				serverIdentity = (ServerIdentity)((InternalMessageWrapper)reqMsg).GetServerIdentityObject();
			}
			if (serverIdentity == null)
			{
				string uri = InternalSink.GetURI(reqMsg);
				Identity identity = IdentityHolder.ResolveIdentity(uri);
				if (identity is ServerIdentity)
				{
					serverIdentity = (ServerIdentity)identity;
					if (flag)
					{
						internalMessage.ServerIdentityObject = serverIdentity;
					}
				}
			}
			return serverIdentity;
		}

		// Token: 0x06003EBA RID: 16058 RVA: 0x000D7110 File Offset: 0x000D6110
		internal static string GetURI(IMessage msg)
		{
			string result = null;
			IMethodMessage methodMessage = msg as IMethodMessage;
			if (methodMessage != null)
			{
				result = methodMessage.Uri;
			}
			else
			{
				IDictionary properties = msg.Properties;
				if (properties != null)
				{
					result = (string)properties["__Uri"];
				}
			}
			return result;
		}
	}
}
