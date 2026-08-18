using System;
using System.Collections;
using System.IO;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x020006D8 RID: 1752
	internal class DispatchChannelSink : IServerChannelSink, IChannelSinkBase
	{
		// Token: 0x06003F10 RID: 16144 RVA: 0x000D7FED File Offset: 0x000D6FED
		internal DispatchChannelSink()
		{
		}

		// Token: 0x06003F11 RID: 16145 RVA: 0x000D7FF5 File Offset: 0x000D6FF5
		public ServerProcessing ProcessMessage(IServerChannelSinkStack sinkStack, IMessage requestMsg, ITransportHeaders requestHeaders, Stream requestStream, out IMessage responseMsg, out ITransportHeaders responseHeaders, out Stream responseStream)
		{
			if (requestMsg == null)
			{
				throw new ArgumentNullException("requestMsg", Environment.GetResourceString("Remoting_Channel_DispatchSinkMessageMissing"));
			}
			if (requestStream != null)
			{
				throw new RemotingException(Environment.GetResourceString("Remoting_Channel_DispatchSinkWantsNullRequestStream"));
			}
			responseHeaders = null;
			responseStream = null;
			return ChannelServices.DispatchMessage(sinkStack, requestMsg, out responseMsg);
		}

		// Token: 0x06003F12 RID: 16146 RVA: 0x000D8034 File Offset: 0x000D7034
		public void AsyncProcessResponse(IServerResponseChannelSinkStack sinkStack, object state, IMessage msg, ITransportHeaders headers, Stream stream)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06003F13 RID: 16147 RVA: 0x000D803B File Offset: 0x000D703B
		public Stream GetResponseStream(IServerResponseChannelSinkStack sinkStack, object state, IMessage msg, ITransportHeaders headers)
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000A87 RID: 2695
		// (get) Token: 0x06003F14 RID: 16148 RVA: 0x000D8042 File Offset: 0x000D7042
		public IServerChannelSink NextChannelSink
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000A88 RID: 2696
		// (get) Token: 0x06003F15 RID: 16149 RVA: 0x000D8045 File Offset: 0x000D7045
		public IDictionary Properties
		{
			get
			{
				return null;
			}
		}
	}
}
