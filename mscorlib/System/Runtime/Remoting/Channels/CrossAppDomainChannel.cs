using System;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Messaging;
using System.Security;
using System.Security.Permissions;
using System.Threading;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x020006CF RID: 1743
	[Serializable]
	internal class CrossAppDomainChannel : IChannelSender, IChannelReceiver, IChannel
	{
		// Token: 0x17000A79 RID: 2681
		// (get) Token: 0x06003ED8 RID: 16088 RVA: 0x000D7737 File Offset: 0x000D6737
		// (set) Token: 0x06003ED9 RID: 16089 RVA: 0x000D774D File Offset: 0x000D674D
		private static CrossAppDomainChannel gAppDomainChannel
		{
			get
			{
				return Thread.GetDomain().RemotingData.ChannelServicesData.xadmessageSink;
			}
			set
			{
				Thread.GetDomain().RemotingData.ChannelServicesData.xadmessageSink = value;
			}
		}

		// Token: 0x17000A7A RID: 2682
		// (get) Token: 0x06003EDA RID: 16090 RVA: 0x000D7764 File Offset: 0x000D6764
		internal static CrossAppDomainChannel AppDomainChannel
		{
			get
			{
				if (CrossAppDomainChannel.gAppDomainChannel == null)
				{
					CrossAppDomainChannel gAppDomainChannel = new CrossAppDomainChannel();
					lock (CrossAppDomainChannel.staticSyncObject)
					{
						if (CrossAppDomainChannel.gAppDomainChannel == null)
						{
							CrossAppDomainChannel.gAppDomainChannel = gAppDomainChannel;
						}
					}
				}
				return CrossAppDomainChannel.gAppDomainChannel;
			}
		}

		// Token: 0x06003EDB RID: 16091 RVA: 0x000D77B8 File Offset: 0x000D67B8
		internal static void RegisterChannel()
		{
			CrossAppDomainChannel appDomainChannel = CrossAppDomainChannel.AppDomainChannel;
			ChannelServices.RegisterChannelInternal(appDomainChannel, false);
		}

		// Token: 0x17000A7B RID: 2683
		// (get) Token: 0x06003EDC RID: 16092 RVA: 0x000D77D2 File Offset: 0x000D67D2
		public virtual string ChannelName
		{
			get
			{
				return "XAPPDMN";
			}
		}

		// Token: 0x17000A7C RID: 2684
		// (get) Token: 0x06003EDD RID: 16093 RVA: 0x000D77D9 File Offset: 0x000D67D9
		public virtual string ChannelURI
		{
			get
			{
				return "XAPPDMN_URI";
			}
		}

		// Token: 0x17000A7D RID: 2685
		// (get) Token: 0x06003EDE RID: 16094 RVA: 0x000D77E0 File Offset: 0x000D67E0
		public virtual int ChannelPriority
		{
			get
			{
				return 100;
			}
		}

		// Token: 0x06003EDF RID: 16095 RVA: 0x000D77E4 File Offset: 0x000D67E4
		public string Parse(string url, out string objectURI)
		{
			objectURI = url;
			return null;
		}

		// Token: 0x17000A7E RID: 2686
		// (get) Token: 0x06003EE0 RID: 16096 RVA: 0x000D77EA File Offset: 0x000D67EA
		public virtual object ChannelData
		{
			get
			{
				return new CrossAppDomainData(Context.DefaultContext.InternalContextID, Thread.GetDomain().GetId(), Identity.ProcessGuid);
			}
		}

		// Token: 0x06003EE1 RID: 16097 RVA: 0x000D780C File Offset: 0x000D680C
		public virtual IMessageSink CreateMessageSink(string url, object data, out string objectURI)
		{
			objectURI = null;
			IMessageSink result = null;
			if (url != null && data == null)
			{
				if (url.StartsWith("XAPPDMN", StringComparison.Ordinal))
				{
					throw new RemotingException(Environment.GetResourceString("Remoting_AppDomains_NYI"));
				}
			}
			else
			{
				CrossAppDomainData crossAppDomainData = data as CrossAppDomainData;
				if (crossAppDomainData != null && crossAppDomainData.ProcessGuid.Equals(Identity.ProcessGuid))
				{
					result = CrossAppDomainSink.FindOrCreateSink(crossAppDomainData);
				}
			}
			return result;
		}

		// Token: 0x06003EE2 RID: 16098 RVA: 0x000D7866 File Offset: 0x000D6866
		public virtual string[] GetUrlsForUri(string objectURI)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_Method"));
		}

		// Token: 0x06003EE3 RID: 16099 RVA: 0x000D7877 File Offset: 0x000D6877
		public virtual void StartListening(object data)
		{
		}

		// Token: 0x06003EE4 RID: 16100 RVA: 0x000D7879 File Offset: 0x000D6879
		public virtual void StopListening(object data)
		{
		}

		// Token: 0x04001FF3 RID: 8179
		private const string _channelName = "XAPPDMN";

		// Token: 0x04001FF4 RID: 8180
		private const string _channelURI = "XAPPDMN_URI";

		// Token: 0x04001FF5 RID: 8181
		private static object staticSyncObject = new object();

		// Token: 0x04001FF6 RID: 8182
		private static PermissionSet s_fullTrust = new PermissionSet(PermissionState.Unrestricted);
	}
}
