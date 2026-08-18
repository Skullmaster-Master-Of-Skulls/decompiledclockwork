using System;
using System.Runtime;
using System.Runtime.Diagnostics;
using System.ServiceModel.Channels;
using System.ServiceModel.Diagnostics;
using System.Xml;

namespace System.ServiceModel.Discovery
{
	// Token: 0x02000023 RID: 35
	internal class DiscoveryOperationContext
	{
		// Token: 0x0600019B RID: 411 RVA: 0x000067CC File Offset: 0x000049CC
		public DiscoveryOperationContext(OperationContext operationContext)
		{
			if (Fx.Trace.IsEtwProviderEnabled)
			{
				this.eventTraceActivity = EventTraceActivityHelper.TryExtractActivity(operationContext.IncomingMessage);
			}
			this.operationContext = operationContext;
			this.operationContextExtension = DiscoveryOperationContext.GetDiscoveryOperationContextExtension(this.operationContext);
			this.messageProperty = DiscoveryOperationContext.GetDiscoveryMessageProperty(this.operationContext);
			this.thisLock = new object();
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x0600019C RID: 412 RVA: 0x00006830 File Offset: 0x00004A30
		public ServiceDiscoveryMode DiscoveryMode
		{
			get
			{
				return this.operationContextExtension.DiscoveryMode;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x0600019D RID: 413 RVA: 0x0000683D File Offset: 0x00004A3D
		public EventTraceActivity EventTraceActivity
		{
			get
			{
				return this.eventTraceActivity;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x0600019E RID: 414 RVA: 0x00006845 File Offset: 0x00004A45
		public TimeSpan MaxResponseDelay
		{
			get
			{
				return this.operationContextExtension.MaxResponseDelay;
			}
		}

		// Token: 0x0600019F RID: 415 RVA: 0x00006852 File Offset: 0x00004A52
		public TResponseChannel GetCallbackChannel<TResponseChannel>()
		{
			return this.operationContext.GetCallbackChannel<TResponseChannel>();
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x0000685F File Offset: 0x00004A5F
		public void AddressDuplexResponseMessage(OperationContext responseOperationContext)
		{
			this.EnsureOutgoingMessageHeaders();
			responseOperationContext.OutgoingMessageHeaders.CopyHeadersFrom(this.outgoingMessageHeaders);
			responseOperationContext.OutgoingMessageHeaders.MessageId = new UniqueId();
			this.AddDiscoveryMessageProperty(responseOperationContext);
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x0000688F File Offset: 0x00004A8F
		public void AddressRequestResponseMessage(OperationContext responseOperationContext)
		{
			responseOperationContext.OutgoingMessageHeaders.MessageId = new UniqueId();
			this.AddDiscoveryMessageProperty(responseOperationContext);
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x000068A8 File Offset: 0x00004AA8
		private static DiscoveryOperationContextExtension GetDiscoveryOperationContextExtension(OperationContext operationContext)
		{
			DiscoveryOperationContextExtension discoveryOperationContextExtension = operationContext.Extensions.Find<DiscoveryOperationContextExtension>();
			if (discoveryOperationContextExtension == null)
			{
				discoveryOperationContextExtension = new DiscoveryOperationContextExtension();
			}
			return discoveryOperationContextExtension;
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x000068CC File Offset: 0x00004ACC
		private static DiscoveryMessageProperty GetDiscoveryMessageProperty(OperationContext operationContext)
		{
			object obj;
			if (operationContext.IncomingMessageProperties.TryGetValue("System.ServiceModel.Discovery.DiscoveryMessageProperty", out obj))
			{
				return obj as DiscoveryMessageProperty;
			}
			return null;
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x000068F8 File Offset: 0x00004AF8
		private static MessageHeaders GetOutgoingMessageHeaders(OperationContext operationContext)
		{
			MessageHeaders messageHeaders = new MessageHeaders(operationContext.IncomingMessageVersion);
			EndpointAddress replyTo = operationContext.IncomingMessageHeaders.ReplyTo;
			if (replyTo != null)
			{
				messageHeaders.To = replyTo.Uri;
				foreach (AddressHeader addressHeader in replyTo.Headers)
				{
					messageHeaders.Add(addressHeader.ToMessageHeader());
				}
			}
			messageHeaders.RelatesTo = operationContext.IncomingMessageHeaders.MessageId;
			return messageHeaders;
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x0000698C File Offset: 0x00004B8C
		private void AddDiscoveryMessageProperty(OperationContext responseOperationContext)
		{
			if (this.messageProperty != null)
			{
				responseOperationContext.OutgoingMessageProperties.Add("System.ServiceModel.Discovery.DiscoveryMessageProperty", this.messageProperty);
			}
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x000069AC File Offset: 0x00004BAC
		private void EnsureOutgoingMessageHeaders()
		{
			if (this.outgoingMessageHeaders == null)
			{
				object obj = this.thisLock;
				lock (obj)
				{
					if (this.outgoingMessageHeaders == null)
					{
						this.outgoingMessageHeaders = DiscoveryOperationContext.GetOutgoingMessageHeaders(this.operationContext);
					}
				}
			}
		}

		// Token: 0x0400006C RID: 108
		private readonly object thisLock;

		// Token: 0x0400006D RID: 109
		private readonly OperationContext operationContext;

		// Token: 0x0400006E RID: 110
		private readonly DiscoveryOperationContextExtension operationContextExtension;

		// Token: 0x0400006F RID: 111
		private readonly DiscoveryMessageProperty messageProperty;

		// Token: 0x04000070 RID: 112
		private MessageHeaders outgoingMessageHeaders;

		// Token: 0x04000071 RID: 113
		private EventTraceActivity eventTraceActivity;
	}
}
