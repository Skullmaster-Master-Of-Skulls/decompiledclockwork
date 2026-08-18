using System;
using System.Collections.Generic;
using System.Runtime;
using System.Runtime.CompilerServices;
using System.ServiceModel.Channels;
using System.ServiceModel.Diagnostics;
using System.ServiceModel.Security;
using System.Threading;
using System.Xml;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x02000556 RID: 1366
	internal class DuplexChannelBinder : IChannelBinder
	{
		// Token: 0x0600350C RID: 13580 RVA: 0x000CE973 File Offset: 0x000CCB73
		internal DuplexChannelBinder(IDuplexChannel channel, IRequestReplyCorrelator correlator)
		{
			this.channel = channel;
			this.correlator = correlator;
			this.channel.Faulted += this.OnFaulted;
		}

		// Token: 0x0600350D RID: 13581 RVA: 0x000CE9A0 File Offset: 0x000CCBA0
		internal DuplexChannelBinder(IDuplexChannel channel, IRequestReplyCorrelator correlator, Uri listenUri) : this(channel, correlator)
		{
			this.listenUri = listenUri;
		}

		// Token: 0x0600350E RID: 13582 RVA: 0x000CE9B1 File Offset: 0x000CCBB1
		internal DuplexChannelBinder(IDuplexSessionChannel channel, IRequestReplyCorrelator correlator, Uri listenUri) : this(channel, correlator, listenUri)
		{
			this.isSession = true;
		}

		// Token: 0x0600350F RID: 13583 RVA: 0x000CE9C4 File Offset: 0x000CCBC4
		internal DuplexChannelBinder(IDuplexSessionChannel channel, IRequestReplyCorrelator correlator, bool useActiveAutoClose)
		{
			IDuplexSessionChannel duplexSessionChannel;
			if (!useActiveAutoClose)
			{
				duplexSessionChannel = channel;
			}
			else
			{
				IDuplexSessionChannel duplexSessionChannel2 = new DuplexChannelBinder.AutoCloseDuplexSessionChannel(channel);
				duplexSessionChannel = duplexSessionChannel2;
			}
			this..ctor(duplexSessionChannel, correlator, null);
		}

		// Token: 0x17000CA6 RID: 3238
		// (get) Token: 0x06003510 RID: 13584 RVA: 0x000CE9E7 File Offset: 0x000CCBE7
		public IChannel Channel
		{
			get
			{
				return this.channel;
			}
		}

		// Token: 0x17000CA7 RID: 3239
		// (get) Token: 0x06003511 RID: 13585 RVA: 0x000CE9EF File Offset: 0x000CCBEF
		// (set) Token: 0x06003512 RID: 13586 RVA: 0x000CE9F7 File Offset: 0x000CCBF7
		public TimeSpan DefaultCloseTimeout
		{
			get
			{
				return this.defaultCloseTimeout;
			}
			set
			{
				this.defaultCloseTimeout = value;
			}
		}

		// Token: 0x17000CA8 RID: 3240
		// (get) Token: 0x06003513 RID: 13587 RVA: 0x000CEA00 File Offset: 0x000CCC00
		// (set) Token: 0x06003514 RID: 13588 RVA: 0x000CEA0F File Offset: 0x000CCC0F
		internal ChannelHandler ChannelHandler
		{
			get
			{
				ChannelHandler channelHandler = this.channelHandler;
				return this.channelHandler;
			}
			set
			{
				ChannelHandler channelHandler = this.channelHandler;
				this.channelHandler = value;
			}
		}

		// Token: 0x17000CA9 RID: 3241
		// (get) Token: 0x06003515 RID: 13589 RVA: 0x000CEA1F File Offset: 0x000CCC1F
		// (set) Token: 0x06003516 RID: 13590 RVA: 0x000CEA27 File Offset: 0x000CCC27
		public TimeSpan DefaultSendTimeout
		{
			get
			{
				return this.defaultSendTimeout;
			}
			set
			{
				this.defaultSendTimeout = value;
			}
		}

		// Token: 0x17000CAA RID: 3242
		// (get) Token: 0x06003517 RID: 13591 RVA: 0x000CEA30 File Offset: 0x000CCC30
		public bool HasSession
		{
			get
			{
				return this.isSession;
			}
		}

		// Token: 0x17000CAB RID: 3243
		// (get) Token: 0x06003518 RID: 13592 RVA: 0x000CEA38 File Offset: 0x000CCC38
		// (set) Token: 0x06003519 RID: 13593 RVA: 0x000CEA53 File Offset: 0x000CCC53
		internal IdentityVerifier IdentityVerifier
		{
			get
			{
				if (this.identityVerifier == null)
				{
					this.identityVerifier = IdentityVerifier.CreateDefault();
				}
				return this.identityVerifier;
			}
			set
			{
				if (value == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("value");
				}
				this.identityVerifier = value;
			}
		}

		// Token: 0x17000CAC RID: 3244
		// (get) Token: 0x0600351A RID: 13594 RVA: 0x000CEA6F File Offset: 0x000CCC6F
		public Uri ListenUri
		{
			get
			{
				return this.listenUri;
			}
		}

		// Token: 0x17000CAD RID: 3245
		// (get) Token: 0x0600351B RID: 13595 RVA: 0x000CEA77 File Offset: 0x000CCC77
		public EndpointAddress LocalAddress
		{
			get
			{
				return this.channel.LocalAddress;
			}
		}

		// Token: 0x17000CAE RID: 3246
		// (get) Token: 0x0600351C RID: 13596 RVA: 0x000CEA84 File Offset: 0x000CCC84
		private bool Pumping
		{
			get
			{
				return this.syncPumpEnabled || (this.ChannelHandler != null && this.ChannelHandler.HasRegisterBeenCalled);
			}
		}

		// Token: 0x17000CAF RID: 3247
		// (get) Token: 0x0600351D RID: 13597 RVA: 0x000CEAA8 File Offset: 0x000CCCA8
		public EndpointAddress RemoteAddress
		{
			get
			{
				return this.channel.RemoteAddress;
			}
		}

		// Token: 0x17000CB0 RID: 3248
		// (get) Token: 0x0600351E RID: 13598 RVA: 0x000CEAB8 File Offset: 0x000CCCB8
		private List<DuplexChannelBinder.IDuplexRequest> Requests
		{
			get
			{
				object thisLock = this.ThisLock;
				List<DuplexChannelBinder.IDuplexRequest> result;
				lock (thisLock)
				{
					if (this.requests == null)
					{
						this.requests = new List<DuplexChannelBinder.IDuplexRequest>();
					}
					result = this.requests;
				}
				return result;
			}
		}

		// Token: 0x17000CB1 RID: 3249
		// (get) Token: 0x0600351F RID: 13599 RVA: 0x000CEB10 File Offset: 0x000CCD10
		private List<ICorrelatorKey> TimedOutRequests
		{
			get
			{
				object thisLock = this.ThisLock;
				List<ICorrelatorKey> result;
				lock (thisLock)
				{
					if (this.timedOutRequests == null)
					{
						this.timedOutRequests = new List<ICorrelatorKey>();
					}
					result = this.timedOutRequests;
				}
				return result;
			}
		}

		// Token: 0x17000CB2 RID: 3250
		// (get) Token: 0x06003520 RID: 13600 RVA: 0x000CEB68 File Offset: 0x000CCD68
		private object ThisLock
		{
			get
			{
				return this;
			}
		}

		// Token: 0x06003521 RID: 13601 RVA: 0x000CEB6B File Offset: 0x000CCD6B
		private void OnFaulted(object sender, EventArgs e)
		{
			this.AbortRequests();
		}

		// Token: 0x06003522 RID: 13602 RVA: 0x000CEB73 File Offset: 0x000CCD73
		public void Abort()
		{
			this.channel.Abort();
			this.AbortRequests();
		}

		// Token: 0x06003523 RID: 13603 RVA: 0x000CEB86 File Offset: 0x000CCD86
		public void CloseAfterFault(TimeSpan timeout)
		{
			this.channel.Close(timeout);
			this.AbortRequests();
		}

		// Token: 0x06003524 RID: 13604 RVA: 0x000CEB9C File Offset: 0x000CCD9C
		private void AbortRequests()
		{
			DuplexChannelBinder.IDuplexRequest[] array = null;
			object thisLock = this.ThisLock;
			lock (thisLock)
			{
				if (this.requests != null)
				{
					array = this.requests.ToArray();
					this.requests = null;
				}
				this.requestAborted = true;
			}
			bool flag2 = array != null && array.Length != 0;
			if (flag2)
			{
				foreach (DuplexChannelBinder.IDuplexRequest duplexRequest in array)
				{
					duplexRequest.Abort();
				}
			}
			if (flag2)
			{
				RequestReplyCorrelator requestReplyCorrelator = this.correlator as RequestReplyCorrelator;
				if (requestReplyCorrelator != null)
				{
					foreach (DuplexChannelBinder.IDuplexRequest duplexRequest2 in array)
					{
						ICorrelatorKey correlatorKey = duplexRequest2 as ICorrelatorKey;
						if (correlatorKey != null)
						{
							requestReplyCorrelator.RemoveRequest(correlatorKey);
						}
					}
				}
			}
			this.DeleteTimedoutRequestsFromCorrelator();
		}

		// Token: 0x06003525 RID: 13605 RVA: 0x000CEC7C File Offset: 0x000CCE7C
		private TimeoutException GetReceiveTimeoutException(TimeSpan timeout)
		{
			EndpointAddress endpointAddress = this.channel.RemoteAddress ?? this.channel.LocalAddress;
			if (endpointAddress != null)
			{
				return new TimeoutException(SR.GetString("SFxRequestTimedOut2", new object[]
				{
					endpointAddress,
					timeout
				}));
			}
			return new TimeoutException(SR.GetString("SFxRequestTimedOut1", new object[]
			{
				timeout
			}));
		}

		// Token: 0x06003526 RID: 13606 RVA: 0x000CECF0 File Offset: 0x000CCEF0
		internal bool HandleRequestAsReply(Message message)
		{
			UniqueId id = null;
			try
			{
				id = message.Headers.RelatesTo;
			}
			catch (MessageHeaderException)
			{
			}
			return !(id == null) && this.HandleRequestAsReplyCore(message);
		}

		// Token: 0x06003527 RID: 13607 RVA: 0x000CED34 File Offset: 0x000CCF34
		private bool HandleRequestAsReplyCore(Message message)
		{
			DuplexChannelBinder.IDuplexRequest duplexRequest = this.correlator.Find<DuplexChannelBinder.IDuplexRequest>(message, true);
			if (duplexRequest != null)
			{
				duplexRequest.GotReply(message);
				return true;
			}
			return false;
		}

		// Token: 0x06003528 RID: 13608 RVA: 0x000CED5C File Offset: 0x000CCF5C
		public void EnsurePumping()
		{
			object thisLock = this.ThisLock;
			lock (thisLock)
			{
				if (!this.syncPumpEnabled && !this.ChannelHandler.HasRegisterBeenCalled)
				{
					ChannelHandler.Register(this.ChannelHandler);
				}
			}
		}

		// Token: 0x06003529 RID: 13609 RVA: 0x000CEDB8 File Offset: 0x000CCFB8
		public IAsyncResult BeginTryReceive(TimeSpan timeout, AsyncCallback callback, object state)
		{
			if (this.channel.State == CommunicationState.Faulted)
			{
				return new DuplexChannelBinder.ChannelFaultedAsyncResult(callback, state);
			}
			return this.channel.BeginTryReceive(timeout, callback, state);
		}

		// Token: 0x0600352A RID: 13610 RVA: 0x000CEDE0 File Offset: 0x000CCFE0
		public bool EndTryReceive(IAsyncResult result, out RequestContext requestContext)
		{
			DuplexChannelBinder.ChannelFaultedAsyncResult channelFaultedAsyncResult = result as DuplexChannelBinder.ChannelFaultedAsyncResult;
			if (channelFaultedAsyncResult != null)
			{
				this.AbortRequests();
				requestContext = null;
				return true;
			}
			Message message;
			if (this.channel.EndTryReceive(result, out message))
			{
				if (message != null)
				{
					requestContext = new DuplexChannelBinder.DuplexRequestContext(this.channel, message, this);
				}
				else
				{
					this.AbortRequests();
					requestContext = null;
				}
				return true;
			}
			requestContext = null;
			return false;
		}

		// Token: 0x0600352B RID: 13611 RVA: 0x000CEE35 File Offset: 0x000CD035
		public RequestContext CreateRequestContext(Message message)
		{
			return new DuplexChannelBinder.DuplexRequestContext(this.channel, message, this);
		}

		// Token: 0x0600352C RID: 13612 RVA: 0x000CEE44 File Offset: 0x000CD044
		public IAsyncResult BeginSend(Message message, TimeSpan timeout, AsyncCallback callback, object state)
		{
			return this.channel.BeginSend(message, timeout, callback, state);
		}

		// Token: 0x0600352D RID: 13613 RVA: 0x000CEE56 File Offset: 0x000CD056
		public void EndSend(IAsyncResult result)
		{
			this.channel.EndSend(result);
		}

		// Token: 0x0600352E RID: 13614 RVA: 0x000CEE64 File Offset: 0x000CD064
		public void Send(Message message, TimeSpan timeout)
		{
			this.channel.Send(message, timeout);
		}

		// Token: 0x0600352F RID: 13615 RVA: 0x000CEE74 File Offset: 0x000CD074
		public IAsyncResult BeginRequest(Message message, TimeSpan timeout, AsyncCallback callback, object state)
		{
			bool flag = false;
			DuplexChannelBinder.AsyncDuplexRequest asyncDuplexRequest = null;
			IAsyncResult result;
			try
			{
				RequestReplyCorrelator.PrepareRequest(message);
				asyncDuplexRequest = new DuplexChannelBinder.AsyncDuplexRequest(message, this, timeout, callback, state);
				object thisLock = this.ThisLock;
				lock (thisLock)
				{
					this.RequestStarting(message, asyncDuplexRequest);
				}
				IAsyncResult asyncResult = this.channel.BeginSend(message, timeout, Fx.ThunkCallback(new AsyncCallback(this.SendCallback)), asyncDuplexRequest);
				if (asyncResult.CompletedSynchronously)
				{
					asyncDuplexRequest.FinishedSend(asyncResult, true);
				}
				this.EnsurePumping();
				flag = true;
				result = asyncDuplexRequest;
			}
			finally
			{
				object thisLock2 = this.ThisLock;
				lock (thisLock2)
				{
					if (flag)
					{
						asyncDuplexRequest.EnableCompletion();
					}
					else
					{
						this.RequestCompleting(asyncDuplexRequest);
					}
				}
			}
			return result;
		}

		// Token: 0x06003530 RID: 13616 RVA: 0x000CEF5C File Offset: 0x000CD15C
		public Message EndRequest(IAsyncResult result)
		{
			DuplexChannelBinder.AsyncDuplexRequest asyncDuplexRequest = result as DuplexChannelBinder.AsyncDuplexRequest;
			if (asyncDuplexRequest == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(SR.GetString("InvalidAsyncResult")));
			}
			return asyncDuplexRequest.End();
		}

		// Token: 0x06003531 RID: 13617 RVA: 0x000CEF94 File Offset: 0x000CD194
		public bool TryReceive(TimeSpan timeout, out RequestContext requestContext)
		{
			if (this.channel.State == CommunicationState.Faulted)
			{
				this.AbortRequests();
				requestContext = null;
				return true;
			}
			Message message;
			if (this.channel.TryReceive(timeout, out message))
			{
				if (message != null)
				{
					requestContext = new DuplexChannelBinder.DuplexRequestContext(this.channel, message, this);
				}
				else
				{
					this.AbortRequests();
					requestContext = null;
				}
				return true;
			}
			requestContext = null;
			return false;
		}

		// Token: 0x06003532 RID: 13618 RVA: 0x000CEFF0 File Offset: 0x000CD1F0
		public Message Request(Message message, TimeSpan timeout)
		{
			DuplexChannelBinder.SyncDuplexRequest syncDuplexRequest = null;
			bool flag = false;
			RequestReplyCorrelator.PrepareRequest(message);
			object thisLock = this.ThisLock;
			lock (thisLock)
			{
				if (!this.Pumping)
				{
					flag = true;
					this.syncPumpEnabled = true;
				}
				if (!flag)
				{
					syncDuplexRequest = new DuplexChannelBinder.SyncDuplexRequest(this);
				}
				this.RequestStarting(message, syncDuplexRequest);
			}
			if (flag)
			{
				TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
				UniqueId messageId = message.Headers.MessageId;
				try
				{
					this.channel.Send(message, timeoutHelper.RemainingTime());
					if (DiagnosticUtility.ShouldUseActivity && ServiceModelActivity.Current != null && ServiceModelActivity.Current.ActivityType == ActivityType.ProcessAction)
					{
						ServiceModelActivity.Current.Suspend();
					}
					Message message2;
					for (;;)
					{
						TimeSpan timeout2 = timeoutHelper.RemainingTime();
						if (!this.channel.TryReceive(timeout2, out message2))
						{
							break;
						}
						if (message2 == null)
						{
							goto Block_14;
						}
						if (message2.Headers.RelatesTo == messageId)
						{
							goto Block_15;
						}
						if (!this.HandleRequestAsReply(message2))
						{
							if (DiagnosticUtility.ShouldTraceInformation)
							{
								EndpointDispatcher dispatcher = null;
								if (this.ChannelHandler != null && this.ChannelHandler.Channel != null)
								{
									dispatcher = this.ChannelHandler.Channel.EndpointDispatcher;
								}
								TraceUtility.TraceDroppedMessage(message2, dispatcher);
							}
							message2.Close();
						}
					}
					throw TraceUtility.ThrowHelperError(this.GetReceiveTimeoutException(timeout), message);
					Block_14:
					this.AbortRequests();
					return null;
					Block_15:
					this.ThrowIfInvalidReplyIdentity(message2);
					return message2;
				}
				finally
				{
					object thisLock2 = this.ThisLock;
					lock (thisLock2)
					{
						this.RequestCompleting(null);
						this.syncPumpEnabled = false;
						if (this.pending > 0)
						{
							this.EnsurePumping();
						}
					}
				}
			}
			TimeoutHelper timeoutHelper2 = new TimeoutHelper(timeout);
			this.channel.Send(message, timeoutHelper2.RemainingTime());
			this.EnsurePumping();
			return syncDuplexRequest.WaitForReply(timeoutHelper2.RemainingTime());
		}

		// Token: 0x06003533 RID: 13619 RVA: 0x000CF1E4 File Offset: 0x000CD3E4
		private void RequestStarting(Message message, DuplexChannelBinder.IDuplexRequest request)
		{
			if (request != null)
			{
				this.Requests.Add(request);
				if (!this.requestAborted)
				{
					this.correlator.Add<DuplexChannelBinder.IDuplexRequest>(message, request);
				}
			}
			this.pending++;
		}

		// Token: 0x06003534 RID: 13620 RVA: 0x000CF218 File Offset: 0x000CD418
		private void RequestCompleting(DuplexChannelBinder.IDuplexRequest request)
		{
			this.pending--;
			if (this.pending == 0)
			{
				this.requests = null;
				return;
			}
			if (request != null && this.requests != null)
			{
				this.requests.Remove(request);
			}
		}

		// Token: 0x06003535 RID: 13621 RVA: 0x000CF250 File Offset: 0x000CD450
		private void AddToTimedOutRequestList(ICorrelatorKey request)
		{
			this.TimedOutRequests.Add(request);
		}

		// Token: 0x06003536 RID: 13622 RVA: 0x000CF25E File Offset: 0x000CD45E
		private void RemoveFromTimedOutRequestList(ICorrelatorKey request)
		{
			if (this.timedOutRequests != null)
			{
				this.timedOutRequests.Remove(request);
			}
		}

		// Token: 0x06003537 RID: 13623 RVA: 0x000CF278 File Offset: 0x000CD478
		private void DeleteTimedoutRequestsFromCorrelator()
		{
			ICorrelatorKey[] array = null;
			if (this.timedOutRequests != null && this.timedOutRequests.Count > 0)
			{
				object thisLock = this.ThisLock;
				lock (thisLock)
				{
					if (this.timedOutRequests != null && this.timedOutRequests.Count > 0)
					{
						array = this.timedOutRequests.ToArray();
						this.timedOutRequests = null;
					}
				}
			}
			if (array != null && array.Length != 0)
			{
				RequestReplyCorrelator requestReplyCorrelator = this.correlator as RequestReplyCorrelator;
				if (requestReplyCorrelator != null)
				{
					foreach (ICorrelatorKey request in array)
					{
						requestReplyCorrelator.RemoveRequest(request);
					}
				}
			}
		}

		// Token: 0x06003538 RID: 13624 RVA: 0x000CF330 File Offset: 0x000CD530
		private void SendCallback(IAsyncResult result)
		{
			DuplexChannelBinder.AsyncDuplexRequest asyncDuplexRequest = result.AsyncState as DuplexChannelBinder.AsyncDuplexRequest;
			if (!result.CompletedSynchronously)
			{
				asyncDuplexRequest.FinishedSend(result, false);
			}
		}

		// Token: 0x06003539 RID: 13625 RVA: 0x000CF35B File Offset: 0x000CD55B
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void EnsureIncomingIdentity(SecurityMessageProperty property, EndpointAddress address, Message reply)
		{
			this.IdentityVerifier.EnsureIncomingIdentity(address, property.ServiceSecurityContext.AuthorizationContext);
		}

		// Token: 0x0600353A RID: 13626 RVA: 0x000CF374 File Offset: 0x000CD574
		private void ThrowIfInvalidReplyIdentity(Message reply)
		{
			if (!this.isSession)
			{
				SecurityMessageProperty security = reply.Properties.Security;
				EndpointAddress remoteAddress = this.channel.RemoteAddress;
				if (security != null && remoteAddress != null)
				{
					this.EnsureIncomingIdentity(security, remoteAddress, reply);
				}
			}
		}

		// Token: 0x0600353B RID: 13627 RVA: 0x000CF3B6 File Offset: 0x000CD5B6
		public bool WaitForMessage(TimeSpan timeout)
		{
			return this.channel.WaitForMessage(timeout);
		}

		// Token: 0x0600353C RID: 13628 RVA: 0x000CF3C4 File Offset: 0x000CD5C4
		public IAsyncResult BeginWaitForMessage(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return this.channel.BeginWaitForMessage(timeout, callback, state);
		}

		// Token: 0x0600353D RID: 13629 RVA: 0x000CF3D4 File Offset: 0x000CD5D4
		public bool EndWaitForMessage(IAsyncResult result)
		{
			return this.channel.EndWaitForMessage(result);
		}

		// Token: 0x04002858 RID: 10328
		private IDuplexChannel channel;

		// Token: 0x04002859 RID: 10329
		private IRequestReplyCorrelator correlator;

		// Token: 0x0400285A RID: 10330
		private TimeSpan defaultCloseTimeout;

		// Token: 0x0400285B RID: 10331
		private TimeSpan defaultSendTimeout;

		// Token: 0x0400285C RID: 10332
		private IdentityVerifier identityVerifier;

		// Token: 0x0400285D RID: 10333
		private bool isSession;

		// Token: 0x0400285E RID: 10334
		private Uri listenUri;

		// Token: 0x0400285F RID: 10335
		private int pending;

		// Token: 0x04002860 RID: 10336
		private bool syncPumpEnabled;

		// Token: 0x04002861 RID: 10337
		private List<DuplexChannelBinder.IDuplexRequest> requests;

		// Token: 0x04002862 RID: 10338
		private List<ICorrelatorKey> timedOutRequests;

		// Token: 0x04002863 RID: 10339
		private ChannelHandler channelHandler;

		// Token: 0x04002864 RID: 10340
		private bool requestAborted;

		// Token: 0x02000C7A RID: 3194
		private class DuplexRequestContext : RequestContextBase
		{
			// Token: 0x06007830 RID: 30768 RVA: 0x001C169F File Offset: 0x001BF89F
			internal DuplexRequestContext(IDuplexChannel channel, Message request, DuplexChannelBinder binder) : base(request, binder.DefaultCloseTimeout, binder.DefaultSendTimeout)
			{
				this.channel = channel;
				this.binder = binder;
			}

			// Token: 0x06007831 RID: 30769 RVA: 0x001C16C2 File Offset: 0x001BF8C2
			protected override void OnAbort()
			{
			}

			// Token: 0x06007832 RID: 30770 RVA: 0x001C16C4 File Offset: 0x001BF8C4
			protected override void OnClose(TimeSpan timeout)
			{
			}

			// Token: 0x06007833 RID: 30771 RVA: 0x001C16C6 File Offset: 0x001BF8C6
			protected override void OnReply(Message message, TimeSpan timeout)
			{
				if (message != null)
				{
					this.channel.Send(message, timeout);
				}
			}

			// Token: 0x06007834 RID: 30772 RVA: 0x001C16D8 File Offset: 0x001BF8D8
			protected override IAsyncResult OnBeginReply(Message message, TimeSpan timeout, AsyncCallback callback, object state)
			{
				return new DuplexChannelBinder.DuplexRequestContext.ReplyAsyncResult(this, message, timeout, callback, state);
			}

			// Token: 0x06007835 RID: 30773 RVA: 0x001C16E5 File Offset: 0x001BF8E5
			protected override void OnEndReply(IAsyncResult result)
			{
				DuplexChannelBinder.DuplexRequestContext.ReplyAsyncResult.End(result);
			}

			// Token: 0x0400449B RID: 17563
			private DuplexChannelBinder binder;

			// Token: 0x0400449C RID: 17564
			private IDuplexChannel channel;

			// Token: 0x02000F3B RID: 3899
			private class ReplyAsyncResult : AsyncResult
			{
				// Token: 0x06008694 RID: 34452 RVA: 0x001F2C4C File Offset: 0x001F0E4C
				public ReplyAsyncResult(DuplexChannelBinder.DuplexRequestContext context, Message message, TimeSpan timeout, AsyncCallback callback, object state) : base(callback, state)
				{
					if (message != null)
					{
						if (DuplexChannelBinder.DuplexRequestContext.ReplyAsyncResult.onSend == null)
						{
							DuplexChannelBinder.DuplexRequestContext.ReplyAsyncResult.onSend = Fx.ThunkCallback(new AsyncCallback(DuplexChannelBinder.DuplexRequestContext.ReplyAsyncResult.OnSend));
						}
						this.context = context;
						IAsyncResult asyncResult = context.channel.BeginSend(message, timeout, DuplexChannelBinder.DuplexRequestContext.ReplyAsyncResult.onSend, this);
						if (!asyncResult.CompletedSynchronously)
						{
							return;
						}
						context.channel.EndSend(asyncResult);
					}
					base.Complete(true);
				}

				// Token: 0x06008695 RID: 34453 RVA: 0x001F2CBA File Offset: 0x001F0EBA
				public static void End(IAsyncResult result)
				{
					AsyncResult.End<DuplexChannelBinder.DuplexRequestContext.ReplyAsyncResult>(result);
				}

				// Token: 0x06008696 RID: 34454 RVA: 0x001F2CC4 File Offset: 0x001F0EC4
				private static void OnSend(IAsyncResult result)
				{
					if (result.CompletedSynchronously)
					{
						return;
					}
					Exception exception = null;
					DuplexChannelBinder.DuplexRequestContext.ReplyAsyncResult replyAsyncResult = (DuplexChannelBinder.DuplexRequestContext.ReplyAsyncResult)result.AsyncState;
					try
					{
						replyAsyncResult.context.channel.EndSend(result);
					}
					catch (Exception ex)
					{
						if (Fx.IsFatal(ex))
						{
							throw;
						}
						exception = ex;
					}
					replyAsyncResult.Complete(false, exception);
				}

				// Token: 0x04004E38 RID: 20024
				private static AsyncCallback onSend;

				// Token: 0x04004E39 RID: 20025
				private DuplexChannelBinder.DuplexRequestContext context;
			}
		}

		// Token: 0x02000C7B RID: 3195
		private interface IDuplexRequest
		{
			// Token: 0x06007836 RID: 30774
			void Abort();

			// Token: 0x06007837 RID: 30775
			void GotReply(Message reply);
		}

		// Token: 0x02000C7C RID: 3196
		private class SyncDuplexRequest : DuplexChannelBinder.IDuplexRequest, ICorrelatorKey
		{
			// Token: 0x06007838 RID: 30776 RVA: 0x001C16ED File Offset: 0x001BF8ED
			internal SyncDuplexRequest(DuplexChannelBinder parent)
			{
				this.parent = parent;
			}

			// Token: 0x17001B59 RID: 7001
			// (get) Token: 0x06007839 RID: 30777 RVA: 0x001C1713 File Offset: 0x001BF913
			// (set) Token: 0x0600783A RID: 30778 RVA: 0x001C171B File Offset: 0x001BF91B
			RequestReplyCorrelator.Key ICorrelatorKey.RequestCorrelatorKey
			{
				get
				{
					return this.requestCorrelatorKey;
				}
				set
				{
					this.requestCorrelatorKey = value;
				}
			}

			// Token: 0x0600783B RID: 30779 RVA: 0x001C1724 File Offset: 0x001BF924
			public void Abort()
			{
				this.SetWaitHandle();
			}

			// Token: 0x0600783C RID: 30780 RVA: 0x001C172C File Offset: 0x001BF92C
			internal Message WaitForReply(TimeSpan timeout)
			{
				try
				{
					if (!TimeoutHelper.WaitOne(this.wait, timeout))
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(this.parent.GetReceiveTimeoutException(timeout));
					}
				}
				finally
				{
					this.CloseWaitHandle();
				}
				this.parent.ThrowIfInvalidReplyIdentity(this.reply);
				return this.reply;
			}

			// Token: 0x0600783D RID: 30781 RVA: 0x001C1790 File Offset: 0x001BF990
			public void GotReply(Message reply)
			{
				object obj = this.parent.ThisLock;
				lock (obj)
				{
					this.parent.RequestCompleting(this);
				}
				this.reply = reply;
				this.SetWaitHandle();
				this.CloseWaitHandle();
			}

			// Token: 0x0600783E RID: 30782 RVA: 0x001C17F0 File Offset: 0x001BF9F0
			private void SetWaitHandle()
			{
				object obj = this.thisLock;
				lock (obj)
				{
					if (this.waitCount < 2)
					{
						this.wait.Set();
					}
				}
			}

			// Token: 0x0600783F RID: 30783 RVA: 0x001C1840 File Offset: 0x001BFA40
			private void CloseWaitHandle()
			{
				object obj = this.thisLock;
				lock (obj)
				{
					this.waitCount++;
					if (this.waitCount == 2)
					{
						this.wait.Close();
					}
				}
			}

			// Token: 0x0400449D RID: 17565
			private Message reply;

			// Token: 0x0400449E RID: 17566
			private DuplexChannelBinder parent;

			// Token: 0x0400449F RID: 17567
			private ManualResetEvent wait = new ManualResetEvent(false);

			// Token: 0x040044A0 RID: 17568
			private int waitCount;

			// Token: 0x040044A1 RID: 17569
			private RequestReplyCorrelator.Key requestCorrelatorKey;

			// Token: 0x040044A2 RID: 17570
			private readonly object thisLock = new object();
		}

		// Token: 0x02000C7D RID: 3197
		private class AsyncDuplexRequest : AsyncResult, DuplexChannelBinder.IDuplexRequest, ICorrelatorKey
		{
			// Token: 0x06007840 RID: 30784 RVA: 0x001C189C File Offset: 0x001BFA9C
			internal AsyncDuplexRequest(Message message, DuplexChannelBinder parent, TimeSpan timeout, AsyncCallback callback, object state) : base(callback, state)
			{
				this.parent = parent;
				this.timeout = timeout;
				if (timeout != TimeSpan.MaxValue)
				{
					this.timer = new IOThreadTimer(DuplexChannelBinder.AsyncDuplexRequest.timerCallback, this, true);
					this.timer.Set(timeout);
				}
				if (DiagnosticUtility.ShouldUseActivity)
				{
					this.activity = TraceUtility.ExtractActivity(message);
				}
			}

			// Token: 0x17001B5A RID: 7002
			// (get) Token: 0x06007841 RID: 30785 RVA: 0x001C18FF File Offset: 0x001BFAFF
			private bool IsDone
			{
				get
				{
					return this.enableComplete && ((this.sendResult != null && this.gotReply) || this.sendException != null || this.timedOut || this.aborted);
				}
			}

			// Token: 0x17001B5B RID: 7003
			// (get) Token: 0x06007842 RID: 30786 RVA: 0x001C1933 File Offset: 0x001BFB33
			// (set) Token: 0x06007843 RID: 30787 RVA: 0x001C193B File Offset: 0x001BFB3B
			RequestReplyCorrelator.Key ICorrelatorKey.RequestCorrelatorKey
			{
				get
				{
					return this.requestCorrelatorKey;
				}
				set
				{
					this.requestCorrelatorKey = value;
				}
			}

			// Token: 0x06007844 RID: 30788 RVA: 0x001C1944 File Offset: 0x001BFB44
			public void Abort()
			{
				object thisLock = this.parent.ThisLock;
				bool flag2;
				lock (thisLock)
				{
					bool isDone = this.IsDone;
					this.aborted = true;
					flag2 = (!isDone && this.IsDone);
				}
				if (flag2)
				{
					this.Done(false);
				}
			}

			// Token: 0x06007845 RID: 30789 RVA: 0x001C19A8 File Offset: 0x001BFBA8
			private void Done(bool completedSynchronously)
			{
				ServiceModelActivity serviceModelActivity = DiagnosticUtility.ShouldUseActivity ? TraceUtility.ExtractActivity(this.reply) : null;
				using (ServiceModelActivity.BoundOperation(serviceModelActivity))
				{
					if (this.timer != null)
					{
						this.timer.Cancel();
						this.timer = null;
					}
					object thisLock = this.parent.ThisLock;
					lock (thisLock)
					{
						if (this.timedOut)
						{
							this.parent.AddToTimedOutRequestList(this);
						}
						this.parent.RequestCompleting(this);
					}
					if (this.sendException != null)
					{
						base.Complete(completedSynchronously, this.sendException);
					}
					else if (this.timedOut)
					{
						base.Complete(completedSynchronously, this.parent.GetReceiveTimeoutException(this.timeout));
					}
					else
					{
						base.Complete(completedSynchronously);
					}
				}
			}

			// Token: 0x06007846 RID: 30790 RVA: 0x001C1A98 File Offset: 0x001BFC98
			public void EnableCompletion()
			{
				object thisLock = this.parent.ThisLock;
				bool flag2;
				lock (thisLock)
				{
					bool isDone = this.IsDone;
					this.enableComplete = true;
					flag2 = (!isDone && this.IsDone);
				}
				if (flag2)
				{
					this.Done(true);
				}
			}

			// Token: 0x06007847 RID: 30791 RVA: 0x001C1AFC File Offset: 0x001BFCFC
			public void FinishedSend(IAsyncResult sendResult, bool completedSynchronously)
			{
				Exception ex = null;
				try
				{
					this.parent.channel.EndSend(sendResult);
				}
				catch (Exception ex2)
				{
					if (Fx.IsFatal(ex2))
					{
						throw;
					}
					ex = ex2;
				}
				object thisLock = this.parent.ThisLock;
				bool flag2;
				lock (thisLock)
				{
					bool isDone = this.IsDone;
					this.sendResult = sendResult;
					this.sendException = ex;
					flag2 = (!isDone && this.IsDone);
				}
				if (flag2)
				{
					this.Done(completedSynchronously);
				}
			}

			// Token: 0x06007848 RID: 30792 RVA: 0x001C1B9C File Offset: 0x001BFD9C
			internal Message End()
			{
				AsyncResult.End<DuplexChannelBinder.AsyncDuplexRequest>(this);
				this.parent.ThrowIfInvalidReplyIdentity(this.reply);
				return this.reply;
			}

			// Token: 0x06007849 RID: 30793 RVA: 0x001C1BBC File Offset: 0x001BFDBC
			public void GotReply(Message reply)
			{
				ServiceModelActivity serviceModelActivity = DiagnosticUtility.ShouldUseActivity ? TraceUtility.ExtractActivity(reply) : null;
				bool flag2;
				using (ServiceModelActivity.BoundOperation(serviceModelActivity))
				{
					object thisLock = this.parent.ThisLock;
					lock (thisLock)
					{
						bool isDone = this.IsDone;
						this.reply = reply;
						this.gotReply = true;
						flag2 = (!isDone && this.IsDone);
						if (isDone && this.timedOut)
						{
							this.parent.RemoveFromTimedOutRequestList(this);
						}
					}
					if (serviceModelActivity != null && DiagnosticUtility.ShouldUseActivity)
					{
						TraceUtility.SetActivity(reply, this.activity);
						if (DiagnosticUtility.ShouldUseActivity && this.activity != null && FxTrace.Trace != null)
						{
							FxTrace.Trace.TraceTransfer(this.activity.Id);
						}
					}
				}
				if (DiagnosticUtility.ShouldUseActivity && serviceModelActivity != null)
				{
					serviceModelActivity.Stop();
				}
				if (flag2)
				{
					this.Done(false);
				}
			}

			// Token: 0x0600784A RID: 30794 RVA: 0x001C1CC4 File Offset: 0x001BFEC4
			private void TimedOut()
			{
				object thisLock = this.parent.ThisLock;
				bool flag2;
				lock (thisLock)
				{
					bool isDone = this.IsDone;
					this.timedOut = true;
					flag2 = (!isDone && this.IsDone);
				}
				if (flag2)
				{
					this.Done(false);
				}
			}

			// Token: 0x0600784B RID: 30795 RVA: 0x001C1D28 File Offset: 0x001BFF28
			private static void TimerCallback(object state)
			{
				((DuplexChannelBinder.AsyncDuplexRequest)state).TimedOut();
			}

			// Token: 0x040044A3 RID: 17571
			private static Action<object> timerCallback = new Action<object>(DuplexChannelBinder.AsyncDuplexRequest.TimerCallback);

			// Token: 0x040044A4 RID: 17572
			private bool aborted;

			// Token: 0x040044A5 RID: 17573
			private bool enableComplete;

			// Token: 0x040044A6 RID: 17574
			private bool gotReply;

			// Token: 0x040044A7 RID: 17575
			private Exception sendException;

			// Token: 0x040044A8 RID: 17576
			private IAsyncResult sendResult;

			// Token: 0x040044A9 RID: 17577
			private DuplexChannelBinder parent;

			// Token: 0x040044AA RID: 17578
			private Message reply;

			// Token: 0x040044AB RID: 17579
			private bool timedOut;

			// Token: 0x040044AC RID: 17580
			private TimeSpan timeout;

			// Token: 0x040044AD RID: 17581
			private IOThreadTimer timer;

			// Token: 0x040044AE RID: 17582
			private ServiceModelActivity activity;

			// Token: 0x040044AF RID: 17583
			private RequestReplyCorrelator.Key requestCorrelatorKey;
		}

		// Token: 0x02000C7E RID: 3198
		private class ChannelFaultedAsyncResult : CompletedAsyncResult
		{
			// Token: 0x0600784D RID: 30797 RVA: 0x001C1D48 File Offset: 0x001BFF48
			public ChannelFaultedAsyncResult(AsyncCallback callback, object state) : base(callback, state)
			{
			}
		}

		// Token: 0x02000C7F RID: 3199
		private class AutoCloseDuplexSessionChannel : IDuplexSessionChannel, IDuplexChannel, IInputChannel, IChannel, ICommunicationObject, IOutputChannel, ISessionChannel<IDuplexSession>
		{
			// Token: 0x0600784E RID: 30798 RVA: 0x001C1D52 File Offset: 0x001BFF52
			public AutoCloseDuplexSessionChannel(IDuplexSessionChannel innerChannel)
			{
				this.innerChannel = innerChannel;
				this.pendingMessages = new InputQueue<Message>();
				this.messageDequeuedCallback = new Action(this.StartBackgroundReceive);
				this.closeState = new DuplexChannelBinder.AutoCloseDuplexSessionChannel.CloseState();
			}

			// Token: 0x17001B5C RID: 7004
			// (get) Token: 0x0600784F RID: 30799 RVA: 0x001C1D89 File Offset: 0x001BFF89
			private object ThisLock
			{
				get
				{
					return this;
				}
			}

			// Token: 0x17001B5D RID: 7005
			// (get) Token: 0x06007850 RID: 30800 RVA: 0x001C1D8C File Offset: 0x001BFF8C
			public EndpointAddress LocalAddress
			{
				get
				{
					return this.innerChannel.LocalAddress;
				}
			}

			// Token: 0x17001B5E RID: 7006
			// (get) Token: 0x06007851 RID: 30801 RVA: 0x001C1D99 File Offset: 0x001BFF99
			public EndpointAddress RemoteAddress
			{
				get
				{
					return this.innerChannel.RemoteAddress;
				}
			}

			// Token: 0x17001B5F RID: 7007
			// (get) Token: 0x06007852 RID: 30802 RVA: 0x001C1DA6 File Offset: 0x001BFFA6
			public Uri Via
			{
				get
				{
					return this.innerChannel.Via;
				}
			}

			// Token: 0x17001B60 RID: 7008
			// (get) Token: 0x06007853 RID: 30803 RVA: 0x001C1DB3 File Offset: 0x001BFFB3
			public IDuplexSession Session
			{
				get
				{
					return this.innerChannel.Session;
				}
			}

			// Token: 0x17001B61 RID: 7009
			// (get) Token: 0x06007854 RID: 30804 RVA: 0x001C1DC0 File Offset: 0x001BFFC0
			public CommunicationState State
			{
				get
				{
					return this.innerChannel.State;
				}
			}

			// Token: 0x14000062 RID: 98
			// (add) Token: 0x06007855 RID: 30805 RVA: 0x001C1DCD File Offset: 0x001BFFCD
			// (remove) Token: 0x06007856 RID: 30806 RVA: 0x001C1DDB File Offset: 0x001BFFDB
			public event EventHandler Closing
			{
				add
				{
					this.innerChannel.Closing += value;
				}
				remove
				{
					this.innerChannel.Closing -= value;
				}
			}

			// Token: 0x14000063 RID: 99
			// (add) Token: 0x06007857 RID: 30807 RVA: 0x001C1DE9 File Offset: 0x001BFFE9
			// (remove) Token: 0x06007858 RID: 30808 RVA: 0x001C1DF7 File Offset: 0x001BFFF7
			public event EventHandler Closed
			{
				add
				{
					this.innerChannel.Closed += value;
				}
				remove
				{
					this.innerChannel.Closed -= value;
				}
			}

			// Token: 0x14000064 RID: 100
			// (add) Token: 0x06007859 RID: 30809 RVA: 0x001C1E05 File Offset: 0x001C0005
			// (remove) Token: 0x0600785A RID: 30810 RVA: 0x001C1E13 File Offset: 0x001C0013
			public event EventHandler Faulted
			{
				add
				{
					this.innerChannel.Faulted += value;
				}
				remove
				{
					this.innerChannel.Faulted -= value;
				}
			}

			// Token: 0x14000065 RID: 101
			// (add) Token: 0x0600785B RID: 30811 RVA: 0x001C1E21 File Offset: 0x001C0021
			// (remove) Token: 0x0600785C RID: 30812 RVA: 0x001C1E2F File Offset: 0x001C002F
			public event EventHandler Opened
			{
				add
				{
					this.innerChannel.Opened += value;
				}
				remove
				{
					this.innerChannel.Opened -= value;
				}
			}

			// Token: 0x14000066 RID: 102
			// (add) Token: 0x0600785D RID: 30813 RVA: 0x001C1E3D File Offset: 0x001C003D
			// (remove) Token: 0x0600785E RID: 30814 RVA: 0x001C1E4B File Offset: 0x001C004B
			public event EventHandler Opening
			{
				add
				{
					this.innerChannel.Opening += value;
				}
				remove
				{
					this.innerChannel.Opening -= value;
				}
			}

			// Token: 0x17001B62 RID: 7010
			// (get) Token: 0x0600785F RID: 30815 RVA: 0x001C1E5C File Offset: 0x001C005C
			private TimeSpan DefaultCloseTimeout
			{
				get
				{
					IDefaultCommunicationTimeouts defaultCommunicationTimeouts = this.innerChannel as IDefaultCommunicationTimeouts;
					if (defaultCommunicationTimeouts != null)
					{
						return defaultCommunicationTimeouts.CloseTimeout;
					}
					return ServiceDefaults.CloseTimeout;
				}
			}

			// Token: 0x17001B63 RID: 7011
			// (get) Token: 0x06007860 RID: 30816 RVA: 0x001C1E84 File Offset: 0x001C0084
			private TimeSpan DefaultReceiveTimeout
			{
				get
				{
					IDefaultCommunicationTimeouts defaultCommunicationTimeouts = this.innerChannel as IDefaultCommunicationTimeouts;
					if (defaultCommunicationTimeouts != null)
					{
						return defaultCommunicationTimeouts.ReceiveTimeout;
					}
					return ServiceDefaults.ReceiveTimeout;
				}
			}

			// Token: 0x06007861 RID: 30817 RVA: 0x001C1EAC File Offset: 0x001C00AC
			private void StartBackgroundReceive()
			{
				if (DuplexChannelBinder.AutoCloseDuplexSessionChannel.receiveAsyncCallback == null)
				{
					DuplexChannelBinder.AutoCloseDuplexSessionChannel.receiveAsyncCallback = Fx.ThunkCallback(new AsyncCallback(DuplexChannelBinder.AutoCloseDuplexSessionChannel.ReceiveAsyncCallback));
				}
				IAsyncResult asyncResult = null;
				Exception ex = null;
				try
				{
					asyncResult = this.innerChannel.BeginReceive(TimeSpan.MaxValue, DuplexChannelBinder.AutoCloseDuplexSessionChannel.receiveAsyncCallback, this);
				}
				catch (Exception ex2)
				{
					if (Fx.IsFatal(ex2))
					{
						throw;
					}
					ex = ex2;
				}
				if (ex != null)
				{
					this.pendingMessages.EnqueueAndDispatch(ex, this.messageDequeuedCallback, false);
					return;
				}
				if (asyncResult.CompletedSynchronously)
				{
					if (DuplexChannelBinder.AutoCloseDuplexSessionChannel.receiveThreadSchedulerCallback == null)
					{
						DuplexChannelBinder.AutoCloseDuplexSessionChannel.receiveThreadSchedulerCallback = new Action<object>(DuplexChannelBinder.AutoCloseDuplexSessionChannel.ReceiveThreadSchedulerCallback);
					}
					IOThreadScheduler.ScheduleCallbackLowPriNoFlow(DuplexChannelBinder.AutoCloseDuplexSessionChannel.receiveThreadSchedulerCallback, asyncResult);
				}
			}

			// Token: 0x06007862 RID: 30818 RVA: 0x001C1F54 File Offset: 0x001C0154
			private static void ReceiveThreadSchedulerCallback(object state)
			{
				IAsyncResult asyncResult = (IAsyncResult)state;
				DuplexChannelBinder.AutoCloseDuplexSessionChannel autoCloseDuplexSessionChannel = (DuplexChannelBinder.AutoCloseDuplexSessionChannel)asyncResult.AsyncState;
				autoCloseDuplexSessionChannel.OnReceive(asyncResult);
			}

			// Token: 0x06007863 RID: 30819 RVA: 0x001C1F7C File Offset: 0x001C017C
			private static void ReceiveAsyncCallback(IAsyncResult result)
			{
				if (result.CompletedSynchronously)
				{
					return;
				}
				DuplexChannelBinder.AutoCloseDuplexSessionChannel autoCloseDuplexSessionChannel = (DuplexChannelBinder.AutoCloseDuplexSessionChannel)result.AsyncState;
				autoCloseDuplexSessionChannel.OnReceive(result);
			}

			// Token: 0x06007864 RID: 30820 RVA: 0x001C1FA8 File Offset: 0x001C01A8
			private void OnReceive(IAsyncResult result)
			{
				Message message = null;
				Exception ex = null;
				try
				{
					message = this.innerChannel.EndReceive(result);
				}
				catch (Exception ex2)
				{
					if (Fx.IsFatal(ex2))
					{
						throw;
					}
					ex = ex2;
				}
				if (ex != null)
				{
					this.pendingMessages.EnqueueAndDispatch(ex, this.messageDequeuedCallback, true);
					return;
				}
				if (message == null)
				{
					this.pendingMessages.Shutdown();
					this.CloseInnerChannel();
					return;
				}
				this.pendingMessages.EnqueueAndDispatch(message, this.messageDequeuedCallback, true);
			}

			// Token: 0x06007865 RID: 30821 RVA: 0x001C2028 File Offset: 0x001C0228
			private void CloseInnerChannel()
			{
				object thisLock = this.ThisLock;
				lock (thisLock)
				{
					if (!this.closeState.TryBackgroundClose() || this.State != CommunicationState.Opened)
					{
						return;
					}
				}
				IAsyncResult asyncResult = null;
				Exception ex = null;
				try
				{
					if (DuplexChannelBinder.AutoCloseDuplexSessionChannel.closeInnerChannelCallback == null)
					{
						DuplexChannelBinder.AutoCloseDuplexSessionChannel.closeInnerChannelCallback = Fx.ThunkCallback(new AsyncCallback(DuplexChannelBinder.AutoCloseDuplexSessionChannel.CloseInnerChannelCallback));
					}
					asyncResult = this.innerChannel.BeginClose(DuplexChannelBinder.AutoCloseDuplexSessionChannel.closeInnerChannelCallback, this);
				}
				catch (Exception ex2)
				{
					if (Fx.IsFatal(ex2))
					{
						throw;
					}
					this.innerChannel.Abort();
					ex = ex2;
				}
				if (ex != null)
				{
					this.closeState.CaptureBackgroundException(ex);
					return;
				}
				if (asyncResult.CompletedSynchronously)
				{
					this.OnCloseInnerChannel(asyncResult);
				}
			}

			// Token: 0x06007866 RID: 30822 RVA: 0x001C20F8 File Offset: 0x001C02F8
			private static void CloseInnerChannelCallback(IAsyncResult result)
			{
				if (result.CompletedSynchronously)
				{
					return;
				}
				((DuplexChannelBinder.AutoCloseDuplexSessionChannel)result.AsyncState).OnCloseInnerChannel(result);
			}

			// Token: 0x06007867 RID: 30823 RVA: 0x001C2114 File Offset: 0x001C0314
			private void OnCloseInnerChannel(IAsyncResult result)
			{
				Exception ex = null;
				try
				{
					this.innerChannel.EndClose(result);
				}
				catch (Exception ex2)
				{
					if (Fx.IsFatal(ex2))
					{
						throw;
					}
					this.innerChannel.Abort();
					ex = ex2;
				}
				if (ex != null)
				{
					this.closeState.CaptureBackgroundException(ex);
					return;
				}
				this.closeState.FinishBackgroundClose();
			}

			// Token: 0x06007868 RID: 30824 RVA: 0x001C2178 File Offset: 0x001C0378
			public Message Receive()
			{
				return this.Receive(this.DefaultReceiveTimeout);
			}

			// Token: 0x06007869 RID: 30825 RVA: 0x001C2186 File Offset: 0x001C0386
			public Message Receive(TimeSpan timeout)
			{
				return this.pendingMessages.Dequeue(timeout);
			}

			// Token: 0x0600786A RID: 30826 RVA: 0x001C2194 File Offset: 0x001C0394
			public IAsyncResult BeginReceive(AsyncCallback callback, object state)
			{
				return this.BeginReceive(this.DefaultReceiveTimeout, callback, state);
			}

			// Token: 0x0600786B RID: 30827 RVA: 0x001C21A4 File Offset: 0x001C03A4
			public IAsyncResult BeginReceive(TimeSpan timeout, AsyncCallback callback, object state)
			{
				return this.pendingMessages.BeginDequeue(timeout, callback, state);
			}

			// Token: 0x0600786C RID: 30828 RVA: 0x001C21B4 File Offset: 0x001C03B4
			public Message EndReceive(IAsyncResult result)
			{
				throw FxTrace.Exception.AsError(new NotImplementedException());
			}

			// Token: 0x0600786D RID: 30829 RVA: 0x001C21C5 File Offset: 0x001C03C5
			public bool TryReceive(TimeSpan timeout, out Message message)
			{
				return this.pendingMessages.Dequeue(timeout, out message);
			}

			// Token: 0x0600786E RID: 30830 RVA: 0x001C21D4 File Offset: 0x001C03D4
			public IAsyncResult BeginTryReceive(TimeSpan timeout, AsyncCallback callback, object state)
			{
				return this.pendingMessages.BeginDequeue(timeout, callback, state);
			}

			// Token: 0x0600786F RID: 30831 RVA: 0x001C21E4 File Offset: 0x001C03E4
			public bool EndTryReceive(IAsyncResult result, out Message message)
			{
				return this.pendingMessages.EndDequeue(result, out message);
			}

			// Token: 0x06007870 RID: 30832 RVA: 0x001C21F3 File Offset: 0x001C03F3
			public bool WaitForMessage(TimeSpan timeout)
			{
				return this.pendingMessages.WaitForItem(timeout);
			}

			// Token: 0x06007871 RID: 30833 RVA: 0x001C2201 File Offset: 0x001C0401
			public IAsyncResult BeginWaitForMessage(TimeSpan timeout, AsyncCallback callback, object state)
			{
				return this.pendingMessages.BeginWaitForItem(timeout, callback, state);
			}

			// Token: 0x06007872 RID: 30834 RVA: 0x001C2211 File Offset: 0x001C0411
			public bool EndWaitForMessage(IAsyncResult result)
			{
				return this.pendingMessages.EndWaitForItem(result);
			}

			// Token: 0x06007873 RID: 30835 RVA: 0x001C221F File Offset: 0x001C041F
			public T GetProperty<T>() where T : class
			{
				return this.innerChannel.GetProperty<T>();
			}

			// Token: 0x06007874 RID: 30836 RVA: 0x001C222C File Offset: 0x001C042C
			public void Abort()
			{
				this.innerChannel.Abort();
				this.Cleanup();
			}

			// Token: 0x06007875 RID: 30837 RVA: 0x001C223F File Offset: 0x001C043F
			public void Close()
			{
				this.Close(this.DefaultCloseTimeout);
			}

			// Token: 0x06007876 RID: 30838 RVA: 0x001C2250 File Offset: 0x001C0450
			public void Close(TimeSpan timeout)
			{
				object thisLock = this.ThisLock;
				bool flag2;
				lock (thisLock)
				{
					flag2 = this.closeState.TryUserClose();
				}
				if (flag2)
				{
					this.innerChannel.Close(timeout);
				}
				else
				{
					this.closeState.WaitForBackgroundClose(timeout);
				}
				this.Cleanup();
			}

			// Token: 0x06007877 RID: 30839 RVA: 0x001C22BC File Offset: 0x001C04BC
			public IAsyncResult BeginClose(AsyncCallback callback, object state)
			{
				return this.BeginClose(this.DefaultCloseTimeout, callback, state);
			}

			// Token: 0x06007878 RID: 30840 RVA: 0x001C22CC File Offset: 0x001C04CC
			public IAsyncResult BeginClose(TimeSpan timeout, AsyncCallback callback, object state)
			{
				object thisLock = this.ThisLock;
				bool flag2;
				lock (thisLock)
				{
					flag2 = this.closeState.TryUserClose();
				}
				if (flag2)
				{
					return this.innerChannel.BeginClose(timeout, callback, state);
				}
				return this.closeState.BeginWaitForBackgroundClose(timeout, callback, state);
			}

			// Token: 0x06007879 RID: 30841 RVA: 0x001C2334 File Offset: 0x001C0534
			public void EndClose(IAsyncResult result)
			{
				if (this.closeState.TryUserClose())
				{
					this.innerChannel.EndClose(result);
				}
				else
				{
					this.closeState.EndWaitForBackgroundClose(result);
				}
				this.Cleanup();
			}

			// Token: 0x0600787A RID: 30842 RVA: 0x001C2363 File Offset: 0x001C0563
			private void Cleanup()
			{
				this.pendingMessages.Dispose();
			}

			// Token: 0x0600787B RID: 30843 RVA: 0x001C2370 File Offset: 0x001C0570
			public void Open()
			{
				this.innerChannel.Open();
				this.StartBackgroundReceive();
			}

			// Token: 0x0600787C RID: 30844 RVA: 0x001C2383 File Offset: 0x001C0583
			public void Open(TimeSpan timeout)
			{
				this.innerChannel.Open(timeout);
				this.StartBackgroundReceive();
			}

			// Token: 0x0600787D RID: 30845 RVA: 0x001C2397 File Offset: 0x001C0597
			public IAsyncResult BeginOpen(AsyncCallback callback, object state)
			{
				return this.innerChannel.BeginOpen(callback, state);
			}

			// Token: 0x0600787E RID: 30846 RVA: 0x001C23A6 File Offset: 0x001C05A6
			public IAsyncResult BeginOpen(TimeSpan timeout, AsyncCallback callback, object state)
			{
				return this.innerChannel.BeginOpen(timeout, callback, state);
			}

			// Token: 0x0600787F RID: 30847 RVA: 0x001C23B6 File Offset: 0x001C05B6
			public void EndOpen(IAsyncResult result)
			{
				this.innerChannel.EndOpen(result);
				this.StartBackgroundReceive();
			}

			// Token: 0x06007880 RID: 30848 RVA: 0x001C23CA File Offset: 0x001C05CA
			public void Send(Message message)
			{
				this.Send(message);
			}

			// Token: 0x06007881 RID: 30849 RVA: 0x001C23D3 File Offset: 0x001C05D3
			public void Send(Message message, TimeSpan timeout)
			{
				this.Send(message, timeout);
			}

			// Token: 0x06007882 RID: 30850 RVA: 0x001C23DD File Offset: 0x001C05DD
			public IAsyncResult BeginSend(Message message, AsyncCallback callback, object state)
			{
				return this.innerChannel.BeginSend(message, callback, state);
			}

			// Token: 0x06007883 RID: 30851 RVA: 0x001C23ED File Offset: 0x001C05ED
			public IAsyncResult BeginSend(Message message, TimeSpan timeout, AsyncCallback callback, object state)
			{
				return this.innerChannel.BeginSend(message, timeout, callback, state);
			}

			// Token: 0x06007884 RID: 30852 RVA: 0x001C23FF File Offset: 0x001C05FF
			public void EndSend(IAsyncResult result)
			{
				this.innerChannel.EndSend(result);
			}

			// Token: 0x040044B0 RID: 17584
			private static AsyncCallback receiveAsyncCallback;

			// Token: 0x040044B1 RID: 17585
			private static Action<object> receiveThreadSchedulerCallback;

			// Token: 0x040044B2 RID: 17586
			private static AsyncCallback closeInnerChannelCallback;

			// Token: 0x040044B3 RID: 17587
			private IDuplexSessionChannel innerChannel;

			// Token: 0x040044B4 RID: 17588
			private InputQueue<Message> pendingMessages;

			// Token: 0x040044B5 RID: 17589
			private Action messageDequeuedCallback;

			// Token: 0x040044B6 RID: 17590
			private DuplexChannelBinder.AutoCloseDuplexSessionChannel.CloseState closeState;

			// Token: 0x02000F3C RID: 3900
			private class CloseState
			{
				// Token: 0x06008698 RID: 34456 RVA: 0x001F2D2C File Offset: 0x001F0F2C
				public bool TryBackgroundClose()
				{
					if (!this.userClose)
					{
						this.backgroundCloseData = new InputQueue<object>();
						return true;
					}
					return false;
				}

				// Token: 0x06008699 RID: 34457 RVA: 0x001F2D44 File Offset: 0x001F0F44
				public void FinishBackgroundClose()
				{
					this.backgroundCloseData.Close();
				}

				// Token: 0x0600869A RID: 34458 RVA: 0x001F2D51 File Offset: 0x001F0F51
				public bool TryUserClose()
				{
					if (this.backgroundCloseData == null)
					{
						this.userClose = true;
						return true;
					}
					return false;
				}

				// Token: 0x0600869B RID: 34459 RVA: 0x001F2D68 File Offset: 0x001F0F68
				public void WaitForBackgroundClose(TimeSpan timeout)
				{
					object obj = this.backgroundCloseData.Dequeue(timeout);
				}

				// Token: 0x0600869C RID: 34460 RVA: 0x001F2D82 File Offset: 0x001F0F82
				public IAsyncResult BeginWaitForBackgroundClose(TimeSpan timeout, AsyncCallback callback, object state)
				{
					return this.backgroundCloseData.BeginDequeue(timeout, callback, state);
				}

				// Token: 0x0600869D RID: 34461 RVA: 0x001F2D94 File Offset: 0x001F0F94
				public void EndWaitForBackgroundClose(IAsyncResult result)
				{
					object obj = this.backgroundCloseData.EndDequeue(result);
				}

				// Token: 0x0600869E RID: 34462 RVA: 0x001F2DAE File Offset: 0x001F0FAE
				public void CaptureBackgroundException(Exception exception)
				{
					this.backgroundCloseData.EnqueueAndDispatch(exception, null, true);
				}

				// Token: 0x04004E3A RID: 20026
				private bool userClose;

				// Token: 0x04004E3B RID: 20027
				private InputQueue<object> backgroundCloseData;
			}
		}
	}
}
