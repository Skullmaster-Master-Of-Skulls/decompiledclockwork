using System;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Runtime.Remoting.Channels.Http;
using System.Runtime.Remoting.Messaging;
using System.Security.Permissions;
using System.Text;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x02000067 RID: 103
	public class SoapClientFormatterSink : IClientFormatterSink, IMessageSink, IClientChannelSink, IChannelSinkBase
	{
		// Token: 0x0600033C RID: 828 RVA: 0x0000F492 File Offset: 0x0000E492
		public SoapClientFormatterSink(IClientChannelSink nextSink)
		{
			this._nextSink = nextSink;
		}

		// Token: 0x170000C0 RID: 192
		// (set) Token: 0x0600033D RID: 829 RVA: 0x0000F4AF File Offset: 0x0000E4AF
		internal bool IncludeVersioning
		{
			set
			{
				this._includeVersioning = value;
			}
		}

		// Token: 0x170000C1 RID: 193
		// (set) Token: 0x0600033E RID: 830 RVA: 0x0000F4B8 File Offset: 0x0000E4B8
		internal bool StrictBinding
		{
			set
			{
				this._strictBinding = value;
			}
		}

		// Token: 0x170000C2 RID: 194
		// (set) Token: 0x0600033F RID: 831 RVA: 0x0000F4C1 File Offset: 0x0000E4C1
		internal SinkChannelProtocol ChannelProtocol
		{
			set
			{
				this._channelProtocol = value;
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x06000340 RID: 832 RVA: 0x0000F4CA File Offset: 0x0000E4CA
		public IMessageSink NextSink
		{
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure, Infrastructure = true)]
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06000341 RID: 833 RVA: 0x0000F4D4 File Offset: 0x0000E4D4
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure, Infrastructure = true)]
		public IMessage SyncProcessMessage(IMessage msg)
		{
			IMethodCallMessage mcm = (IMethodCallMessage)msg;
			IMessage result;
			try
			{
				ITransportHeaders requestHeaders;
				Stream requestStream;
				this.SerializeMessage(mcm, out requestHeaders, out requestStream);
				ITransportHeaders transportHeaders;
				Stream stream;
				this._nextSink.ProcessMessage(msg, requestHeaders, requestStream, out transportHeaders, out stream);
				if (transportHeaders == null)
				{
					throw new ArgumentNullException("returnHeaders");
				}
				result = this.DeserializeMessage(mcm, transportHeaders, stream);
			}
			catch (Exception e)
			{
				result = new ReturnMessage(e, mcm);
			}
			catch
			{
				result = new ReturnMessage(new Exception(CoreChannel.GetResourceString("Remoting_nonClsCompliantException")), mcm);
			}
			return result;
		}

		// Token: 0x06000342 RID: 834 RVA: 0x0000F568 File Offset: 0x0000E568
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure, Infrastructure = true)]
		public IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
		{
			IMethodCallMessage methodCallMessage = (IMethodCallMessage)msg;
			try
			{
				ITransportHeaders headers;
				Stream stream;
				this.SerializeMessage(methodCallMessage, out headers, out stream);
				ClientChannelSinkStack clientChannelSinkStack = new ClientChannelSinkStack(replySink);
				clientChannelSinkStack.Push(this, methodCallMessage);
				this._nextSink.AsyncProcessRequest(clientChannelSinkStack, msg, headers, stream);
			}
			catch (Exception e)
			{
				IMessage msg2 = new ReturnMessage(e, methodCallMessage);
				if (replySink != null)
				{
					replySink.SyncProcessMessage(msg2);
				}
			}
			catch
			{
				IMessage msg2 = new ReturnMessage(new Exception(CoreChannel.GetResourceString("Remoting_nonClsCompliantException")), methodCallMessage);
				if (replySink != null)
				{
					replySink.SyncProcessMessage(msg2);
				}
			}
			return null;
		}

		// Token: 0x06000343 RID: 835 RVA: 0x0000F604 File Offset: 0x0000E604
		private void SerializeMessage(IMethodCallMessage mcm, out ITransportHeaders headers, out Stream stream)
		{
			BaseTransportHeaders baseTransportHeaders = new BaseTransportHeaders();
			headers = baseTransportHeaders;
			MethodBase methodBase = mcm.MethodBase;
			headers["SOAPAction"] = '"' + HttpEncodingHelper.EncodeUriAsXLinkHref(SoapServices.GetSoapActionFromMethodBase(methodBase)) + '"';
			baseTransportHeaders.ContentType = "text/xml; charset=\"utf-8\"";
			if (this._channelProtocol == SinkChannelProtocol.Http)
			{
				headers["__RequestVerb"] = "POST";
			}
			bool flag = false;
			stream = this._nextSink.GetRequestStream(mcm, headers);
			if (stream == null)
			{
				stream = new ChunkedMemoryStream(CoreChannel.BufferPool);
				flag = true;
			}
			CoreChannel.SerializeSoapMessage(mcm, stream, this._includeVersioning);
			if (flag)
			{
				stream.Position = 0L;
			}
		}

		// Token: 0x06000344 RID: 836 RVA: 0x0000F6B0 File Offset: 0x0000E6B0
		private IMessage DeserializeMessage(IMethodCallMessage mcm, ITransportHeaders headers, Stream stream)
		{
			Header[] h = new Header[]
			{
				new Header("__TypeName", mcm.TypeName),
				new Header("__MethodName", mcm.MethodName),
				new Header("__MethodSignature", mcm.MethodSignature)
			};
			string contentType = headers["Content-Type"] as string;
			string strA;
			string text;
			HttpChannelHelper.ParseContentType(contentType, out strA, out text);
			IMessage result;
			if (string.Compare(strA, "text/xml", StringComparison.Ordinal) == 0)
			{
				result = CoreChannel.DeserializeSoapResponseMessage(stream, mcm, h, this._strictBinding);
			}
			else
			{
				int num = 1024;
				byte[] array = new byte[num];
				StringBuilder stringBuilder = new StringBuilder();
				for (int i = stream.Read(array, 0, num); i > 0; i = stream.Read(array, 0, num))
				{
					stringBuilder.Append(Encoding.ASCII.GetString(array, 0, i));
				}
				result = new ReturnMessage(new RemotingException(stringBuilder.ToString()), mcm);
			}
			stream.Close();
			return result;
		}

		// Token: 0x06000345 RID: 837 RVA: 0x0000F7A0 File Offset: 0x0000E7A0
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure, Infrastructure = true)]
		public void ProcessMessage(IMessage msg, ITransportHeaders requestHeaders, Stream requestStream, out ITransportHeaders responseHeaders, out Stream responseStream)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000346 RID: 838 RVA: 0x0000F7A7 File Offset: 0x0000E7A7
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure, Infrastructure = true)]
		public void AsyncProcessRequest(IClientChannelSinkStack sinkStack, IMessage msg, ITransportHeaders headers, Stream stream)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000347 RID: 839 RVA: 0x0000F7B0 File Offset: 0x0000E7B0
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure, Infrastructure = true)]
		public void AsyncProcessResponse(IClientResponseChannelSinkStack sinkStack, object state, ITransportHeaders headers, Stream stream)
		{
			IMethodCallMessage mcm = (IMethodCallMessage)state;
			IMessage msg = this.DeserializeMessage(mcm, headers, stream);
			sinkStack.DispatchReplyMessage(msg);
		}

		// Token: 0x06000348 RID: 840 RVA: 0x0000F7D6 File Offset: 0x0000E7D6
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure, Infrastructure = true)]
		public Stream GetRequestStream(IMessage msg, ITransportHeaders headers)
		{
			throw new NotSupportedException();
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x06000349 RID: 841 RVA: 0x0000F7DD File Offset: 0x0000E7DD
		public IClientChannelSink NextChannelSink
		{
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure, Infrastructure = true)]
			get
			{
				return this._nextSink;
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x0600034A RID: 842 RVA: 0x0000F7E5 File Offset: 0x0000E7E5
		public IDictionary Properties
		{
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure, Infrastructure = true)]
			get
			{
				return null;
			}
		}

		// Token: 0x04000262 RID: 610
		private IClientChannelSink _nextSink;

		// Token: 0x04000263 RID: 611
		private bool _includeVersioning = true;

		// Token: 0x04000264 RID: 612
		private bool _strictBinding;

		// Token: 0x04000265 RID: 613
		private SinkChannelProtocol _channelProtocol = SinkChannelProtocol.Other;
	}
}
