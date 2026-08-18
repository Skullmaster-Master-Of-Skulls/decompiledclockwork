using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime;
using System.Security.Claims;
using System.Security.Principal;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Security;
using System.Threading;

namespace System.ServiceModel
{
	// Token: 0x0200010E RID: 270
	[__DynamicallyInvokable]
	public sealed class OperationContext : IExtensibleObject<OperationContext>
	{
		// Token: 0x14000009 RID: 9
		// (add) Token: 0x06000638 RID: 1592 RVA: 0x0001B5D4 File Offset: 0x000197D4
		// (remove) Token: 0x06000639 RID: 1593 RVA: 0x0001B60C File Offset: 0x0001980C
		[__DynamicallyInvokable]
		[method: __DynamicallyInvokable]
		public event EventHandler OperationCompleted;

		// Token: 0x0600063A RID: 1594 RVA: 0x0001B644 File Offset: 0x00019844
		[__DynamicallyInvokable]
		public OperationContext(IContextChannel channel)
		{
			if (channel == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("channel"));
			}
			ServiceChannel serviceChannel = channel as ServiceChannel;
			if (serviceChannel == null)
			{
				serviceChannel = ServiceChannelFactory.GetServiceChannel(channel);
			}
			if (serviceChannel != null)
			{
				this.outgoingMessageVersion = serviceChannel.MessageVersion;
				this.channel = serviceChannel;
				return;
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxInvalidChannelToOperationContext")));
		}

		// Token: 0x0600063B RID: 1595 RVA: 0x0001B6B0 File Offset: 0x000198B0
		internal OperationContext(ServiceHostBase host) : this(host, MessageVersion.Soap12WSAddressing10)
		{
		}

		// Token: 0x0600063C RID: 1596 RVA: 0x0001B6BE File Offset: 0x000198BE
		internal OperationContext(ServiceHostBase host, MessageVersion outgoingMessageVersion)
		{
			if (outgoingMessageVersion == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("outgoingMessageVersion"));
			}
			this.host = host;
			this.outgoingMessageVersion = outgoingMessageVersion;
		}

		// Token: 0x0600063D RID: 1597 RVA: 0x0001B6EC File Offset: 0x000198EC
		internal OperationContext(RequestContext requestContext, Message request, ServiceChannel channel, ServiceHostBase host)
		{
			this.channel = channel;
			this.host = host;
			this.requestContext = requestContext;
			this.request = request;
			this.outgoingMessageVersion = channel.MessageVersion;
		}

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x0600063E RID: 1598 RVA: 0x0001B71D File Offset: 0x0001991D
		public IContextChannel Channel
		{
			get
			{
				return this.GetCallbackChannel<IContextChannel>();
			}
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x0600063F RID: 1599 RVA: 0x0001B725 File Offset: 0x00019925
		// (set) Token: 0x06000640 RID: 1600 RVA: 0x0001B743 File Offset: 0x00019943
		[__DynamicallyInvokable]
		public static OperationContext Current
		{
			[__DynamicallyInvokable]
			get
			{
				if (!OperationContext.ShouldUseAsyncLocalContext)
				{
					return OperationContext.CurrentHolder.Context;
				}
				return OperationContext.currentAsyncLocalContext.Value;
			}
			[__DynamicallyInvokable]
			set
			{
				if (OperationContext.ShouldUseAsyncLocalContext && value != null && value.isAsyncFlowEnabled)
				{
					OperationContext.currentAsyncLocalContext.Value = value;
					return;
				}
				OperationContext.CurrentHolder.Context = value;
			}
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x06000641 RID: 1601 RVA: 0x0001B770 File Offset: 0x00019970
		internal static OperationContext.Holder CurrentHolder
		{
			get
			{
				OperationContext.Holder holder = OperationContext.currentContext;
				if (holder == null)
				{
					holder = new OperationContext.Holder();
					OperationContext.currentContext = holder;
				}
				return holder;
			}
		}

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x06000642 RID: 1602 RVA: 0x0001B793 File Offset: 0x00019993
		internal static bool ShouldUseAsyncLocalContext
		{
			get
			{
				return !ServiceModelAppSettings.DisableOperationContextAsyncFlow && OperationContext.CurrentHolder.Context == null && OperationContext.currentAsyncLocalContext.Value != null && OperationContext.currentAsyncLocalContext.Value.isAsyncFlowEnabled;
			}
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x06000643 RID: 1603 RVA: 0x0001B7C5 File Offset: 0x000199C5
		// (set) Token: 0x06000644 RID: 1604 RVA: 0x0001B7CD File Offset: 0x000199CD
		public EndpointDispatcher EndpointDispatcher
		{
			get
			{
				return this.endpointDispatcher;
			}
			set
			{
				this.endpointDispatcher = value;
			}
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x06000645 RID: 1605 RVA: 0x0001B7D6 File Offset: 0x000199D6
		[__DynamicallyInvokable]
		public bool IsUserContext
		{
			[__DynamicallyInvokable]
			get
			{
				return this.request == null;
			}
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x06000646 RID: 1606 RVA: 0x0001B7E1 File Offset: 0x000199E1
		[__DynamicallyInvokable]
		public IExtensionCollection<OperationContext> Extensions
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.extensions == null)
				{
					this.extensions = new ExtensionCollection<OperationContext>(this);
				}
				return this.extensions;
			}
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x06000647 RID: 1607 RVA: 0x0001B7FD File Offset: 0x000199FD
		// (set) Token: 0x06000648 RID: 1608 RVA: 0x0001B805 File Offset: 0x00019A05
		internal bool IsServiceReentrant
		{
			get
			{
				return this.isServiceReentrant;
			}
			set
			{
				this.isServiceReentrant = value;
			}
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x06000649 RID: 1609 RVA: 0x0001B810 File Offset: 0x00019A10
		public bool HasSupportingTokens
		{
			get
			{
				MessageProperties incomingMessageProperties = this.IncomingMessageProperties;
				return incomingMessageProperties != null && incomingMessageProperties.Security != null && incomingMessageProperties.Security.HasIncomingSupportingTokens;
			}
		}

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x0600064A RID: 1610 RVA: 0x0001B83C File Offset: 0x00019A3C
		public ServiceHostBase Host
		{
			get
			{
				return this.host;
			}
		}

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x0600064B RID: 1611 RVA: 0x0001B844 File Offset: 0x00019A44
		internal Message IncomingMessage
		{
			get
			{
				return this.clientReply ?? this.request;
			}
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x0600064C RID: 1612 RVA: 0x0001B856 File Offset: 0x00019A56
		// (set) Token: 0x0600064D RID: 1613 RVA: 0x0001B85E File Offset: 0x00019A5E
		internal ServiceChannel InternalServiceChannel
		{
			get
			{
				return this.channel;
			}
			set
			{
				this.channel = value;
			}
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x0600064E RID: 1614 RVA: 0x0001B867 File Offset: 0x00019A67
		internal bool HasOutgoingMessageHeaders
		{
			get
			{
				return this.outgoingMessageHeaders != null;
			}
		}

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x0600064F RID: 1615 RVA: 0x0001B872 File Offset: 0x00019A72
		[__DynamicallyInvokable]
		public MessageHeaders OutgoingMessageHeaders
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.outgoingMessageHeaders == null)
				{
					this.outgoingMessageHeaders = new MessageHeaders(this.OutgoingMessageVersion);
				}
				return this.outgoingMessageHeaders;
			}
		}

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x06000650 RID: 1616 RVA: 0x0001B893 File Offset: 0x00019A93
		internal bool HasOutgoingMessageProperties
		{
			get
			{
				return this.outgoingMessageProperties != null;
			}
		}

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x06000651 RID: 1617 RVA: 0x0001B89E File Offset: 0x00019A9E
		[__DynamicallyInvokable]
		public MessageProperties OutgoingMessageProperties
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.outgoingMessageProperties == null)
				{
					this.outgoingMessageProperties = new MessageProperties();
				}
				return this.outgoingMessageProperties;
			}
		}

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x06000652 RID: 1618 RVA: 0x0001B8B9 File Offset: 0x00019AB9
		internal MessageVersion OutgoingMessageVersion
		{
			get
			{
				return this.outgoingMessageVersion;
			}
		}

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x06000653 RID: 1619 RVA: 0x0001B8C4 File Offset: 0x00019AC4
		[__DynamicallyInvokable]
		public MessageHeaders IncomingMessageHeaders
		{
			[__DynamicallyInvokable]
			get
			{
				Message message = this.clientReply ?? this.request;
				if (message != null)
				{
					return message.Headers;
				}
				return null;
			}
		}

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x06000654 RID: 1620 RVA: 0x0001B8F0 File Offset: 0x00019AF0
		[__DynamicallyInvokable]
		public MessageProperties IncomingMessageProperties
		{
			[__DynamicallyInvokable]
			get
			{
				Message message = this.clientReply ?? this.request;
				if (message != null)
				{
					return message.Properties;
				}
				return null;
			}
		}

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x06000655 RID: 1621 RVA: 0x0001B91C File Offset: 0x00019B1C
		[__DynamicallyInvokable]
		public MessageVersion IncomingMessageVersion
		{
			[__DynamicallyInvokable]
			get
			{
				Message message = this.clientReply ?? this.request;
				if (message != null)
				{
					return message.Version;
				}
				return null;
			}
		}

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x06000656 RID: 1622 RVA: 0x0001B945 File Offset: 0x00019B45
		public InstanceContext InstanceContext
		{
			get
			{
				return this.instanceContext;
			}
		}

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x06000657 RID: 1623 RVA: 0x0001B94D File Offset: 0x00019B4D
		// (set) Token: 0x06000658 RID: 1624 RVA: 0x0001B955 File Offset: 0x00019B55
		[__DynamicallyInvokable]
		public RequestContext RequestContext
		{
			[__DynamicallyInvokable]
			get
			{
				return this.requestContext;
			}
			[__DynamicallyInvokable]
			set
			{
				this.requestContext = value;
			}
		}

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x06000659 RID: 1625 RVA: 0x0001B960 File Offset: 0x00019B60
		public ServiceSecurityContext ServiceSecurityContext
		{
			get
			{
				MessageProperties incomingMessageProperties = this.IncomingMessageProperties;
				if (incomingMessageProperties != null && incomingMessageProperties.Security != null)
				{
					return incomingMessageProperties.Security.ServiceSecurityContext;
				}
				return null;
			}
		}

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x0600065A RID: 1626 RVA: 0x0001B98C File Offset: 0x00019B8C
		public string SessionId
		{
			get
			{
				if (this.channel != null)
				{
					IChannel innerChannel = this.channel.InnerChannel;
					if (innerChannel != null)
					{
						ISessionChannel<IDuplexSession> sessionChannel = innerChannel as ISessionChannel<IDuplexSession>;
						if (sessionChannel != null && sessionChannel.Session != null)
						{
							return sessionChannel.Session.Id;
						}
						ISessionChannel<IInputSession> sessionChannel2 = innerChannel as ISessionChannel<IInputSession>;
						if (sessionChannel2 != null && sessionChannel2.Session != null)
						{
							return sessionChannel2.Session.Id;
						}
						ISessionChannel<IOutputSession> sessionChannel3 = innerChannel as ISessionChannel<IOutputSession>;
						if (sessionChannel3 != null && sessionChannel3.Session != null)
						{
							return sessionChannel3.Session.Id;
						}
					}
				}
				return null;
			}
		}

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x0600065B RID: 1627 RVA: 0x0001BA0C File Offset: 0x00019C0C
		public ICollection<SupportingTokenSpecification> SupportingTokens
		{
			get
			{
				MessageProperties incomingMessageProperties = this.IncomingMessageProperties;
				if (incomingMessageProperties != null && incomingMessageProperties.Security != null)
				{
					return new ReadOnlyCollection<SupportingTokenSpecification>(incomingMessageProperties.Security.IncomingSupportingTokens);
				}
				return null;
			}
		}

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x0600065C RID: 1628 RVA: 0x0001BA3D File Offset: 0x00019C3D
		// (set) Token: 0x0600065D RID: 1629 RVA: 0x0001BA45 File Offset: 0x00019C45
		internal IPrincipal ThreadPrincipal
		{
			get
			{
				return this.threadPrincipal;
			}
			set
			{
				this.threadPrincipal = value;
			}
		}

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x0600065E RID: 1630 RVA: 0x0001BA4E File Offset: 0x00019C4E
		// (set) Token: 0x0600065F RID: 1631 RVA: 0x0001BA56 File Offset: 0x00019C56
		public ClaimsPrincipal ClaimsPrincipal { get; internal set; }

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x06000660 RID: 1632 RVA: 0x0001BA5F File Offset: 0x00019C5F
		// (set) Token: 0x06000661 RID: 1633 RVA: 0x0001BA67 File Offset: 0x00019C67
		internal TransactionRpcFacet TransactionFacet
		{
			get
			{
				return this.txFacet;
			}
			set
			{
				this.txFacet = value;
			}
		}

		// Token: 0x06000662 RID: 1634 RVA: 0x0001BA70 File Offset: 0x00019C70
		internal void ClearClientReplyNoThrow()
		{
			this.clientReply = null;
		}

		// Token: 0x06000663 RID: 1635 RVA: 0x0001BA79 File Offset: 0x00019C79
		internal static void EnableAsyncFlow()
		{
			OperationContext.EnableAsyncFlow(OperationContext.CurrentHolder.Context);
		}

		// Token: 0x06000664 RID: 1636 RVA: 0x0001BA8A File Offset: 0x00019C8A
		internal static void EnableAsyncFlow(OperationContext oc)
		{
			if (oc != null)
			{
				oc.isAsyncFlowEnabled = true;
				OperationContext.currentAsyncLocalContext.Value = oc;
			}
		}

		// Token: 0x06000665 RID: 1637 RVA: 0x0001BAA1 File Offset: 0x00019CA1
		internal static void DisableAsyncFlow()
		{
			if (OperationContext.Current != null && OperationContext.Current.isAsyncFlowEnabled)
			{
				OperationContext.Current.isAsyncFlowEnabled = false;
				OperationContext.currentAsyncLocalContext.Value = null;
			}
		}

		// Token: 0x06000666 RID: 1638 RVA: 0x0001BACC File Offset: 0x00019CCC
		internal void FireOperationCompleted()
		{
			try
			{
				EventHandler operationCompleted = this.OperationCompleted;
				if (operationCompleted != null)
				{
					operationCompleted(this, EventArgs.Empty);
				}
			}
			catch (Exception ex)
			{
				if (Fx.IsFatal(ex))
				{
					throw;
				}
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperCallback(ex);
			}
		}

		// Token: 0x06000667 RID: 1639 RVA: 0x0001BB18 File Offset: 0x00019D18
		public T GetCallbackChannel<T>()
		{
			if (this.channel == null || this.IsUserContext)
			{
				return default(T);
			}
			return (T)((object)this.channel.Proxy);
		}

		// Token: 0x06000668 RID: 1640 RVA: 0x0001BB4F File Offset: 0x00019D4F
		internal void ReInit(RequestContext requestContext, Message request, ServiceChannel channel)
		{
			this.requestContext = requestContext;
			this.request = request;
			this.channel = channel;
		}

		// Token: 0x06000669 RID: 1641 RVA: 0x0001BB66 File Offset: 0x00019D66
		internal void Recycle()
		{
			this.requestContext = null;
			this.request = null;
			this.extensions = null;
			this.instanceContext = null;
			this.threadPrincipal = null;
			this.txFacet = null;
			this.SetClientReply(null, false);
		}

		// Token: 0x0600066A RID: 1642 RVA: 0x0001BB9C File Offset: 0x00019D9C
		internal void SetClientReply(Message message, bool closeMessage)
		{
			Message message2 = null;
			if (!object.Equals(message, this.clientReply))
			{
				if (this.closeClientReply && this.clientReply != null)
				{
					message2 = this.clientReply;
				}
				this.clientReply = message;
			}
			this.closeClientReply = closeMessage;
			if (message2 != null)
			{
				message2.Close();
			}
		}

		// Token: 0x0600066B RID: 1643 RVA: 0x0001BBE7 File Offset: 0x00019DE7
		public void SetTransactionComplete()
		{
			if (this.txFacet == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("NoTransactionInContext")));
			}
			this.txFacet.Completed();
		}

		// Token: 0x0600066C RID: 1644 RVA: 0x0001BC16 File Offset: 0x00019E16
		internal void SetInstanceContext(InstanceContext instanceContext)
		{
			this.instanceContext = instanceContext;
		}

		// Token: 0x04000A69 RID: 2665
		[ThreadStatic]
		private static OperationContext.Holder currentContext;

		// Token: 0x04000A6A RID: 2666
		private static AsyncLocal<OperationContext> currentAsyncLocalContext = new AsyncLocal<OperationContext>();

		// Token: 0x04000A6B RID: 2667
		private ServiceChannel channel;

		// Token: 0x04000A6C RID: 2668
		private Message clientReply;

		// Token: 0x04000A6D RID: 2669
		private bool closeClientReply;

		// Token: 0x04000A6E RID: 2670
		private ExtensionCollection<OperationContext> extensions;

		// Token: 0x04000A6F RID: 2671
		private ServiceHostBase host;

		// Token: 0x04000A70 RID: 2672
		private RequestContext requestContext;

		// Token: 0x04000A71 RID: 2673
		private Message request;

		// Token: 0x04000A72 RID: 2674
		private InstanceContext instanceContext;

		// Token: 0x04000A73 RID: 2675
		private bool isServiceReentrant;

		// Token: 0x04000A74 RID: 2676
		internal IPrincipal threadPrincipal;

		// Token: 0x04000A75 RID: 2677
		private TransactionRpcFacet txFacet;

		// Token: 0x04000A76 RID: 2678
		private MessageProperties outgoingMessageProperties;

		// Token: 0x04000A77 RID: 2679
		private MessageHeaders outgoingMessageHeaders;

		// Token: 0x04000A78 RID: 2680
		private MessageVersion outgoingMessageVersion;

		// Token: 0x04000A79 RID: 2681
		private EndpointDispatcher endpointDispatcher;

		// Token: 0x04000A7A RID: 2682
		private bool isAsyncFlowEnabled;

		// Token: 0x02000AE4 RID: 2788
		internal class Holder
		{
			// Token: 0x170019D4 RID: 6612
			// (get) Token: 0x06006EB9 RID: 28345 RVA: 0x0019C8D1 File Offset: 0x0019AAD1
			// (set) Token: 0x06006EBA RID: 28346 RVA: 0x0019C8D9 File Offset: 0x0019AAD9
			public OperationContext Context
			{
				get
				{
					return this.context;
				}
				set
				{
					this.context = value;
				}
			}

			// Token: 0x04003F2B RID: 16171
			private OperationContext context;
		}
	}
}
