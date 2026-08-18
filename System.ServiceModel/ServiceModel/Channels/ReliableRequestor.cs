using System;
using System.Diagnostics;
using System.Runtime;
using System.ServiceModel.Security;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x0200093F RID: 2367
	internal abstract class ReliableRequestor
	{
		// Token: 0x170015EF RID: 5615
		// (get) Token: 0x06005AE7 RID: 23271 RVA: 0x0014DE38 File Offset: 0x0014C038
		// (set) Token: 0x06005AE8 RID: 23272 RVA: 0x0014DE40 File Offset: 0x0014C040
		public IReliableChannelBinder Binder
		{
			protected get
			{
				return this.binder;
			}
			set
			{
				this.binder = value;
			}
		}

		// Token: 0x170015F0 RID: 5616
		// (get) Token: 0x06005AE9 RID: 23273 RVA: 0x0014DE49 File Offset: 0x0014C049
		// (set) Token: 0x06005AEA RID: 23274 RVA: 0x0014DE51 File Offset: 0x0014C051
		public bool IsCreateSequence
		{
			protected get
			{
				return this.isCreateSequence;
			}
			set
			{
				this.isCreateSequence = value;
			}
		}

		// Token: 0x170015F1 RID: 5617
		// (set) Token: 0x06005AEB RID: 23275 RVA: 0x0014DE5A File Offset: 0x0014C05A
		public ActionHeader MessageAction
		{
			set
			{
				this.messageAction = value;
			}
		}

		// Token: 0x170015F2 RID: 5618
		// (set) Token: 0x06005AEC RID: 23276 RVA: 0x0014DE63 File Offset: 0x0014C063
		public BodyWriter MessageBody
		{
			set
			{
				this.messageBody = value;
			}
		}

		// Token: 0x170015F3 RID: 5619
		// (get) Token: 0x06005AED RID: 23277 RVA: 0x0014DE6C File Offset: 0x0014C06C
		public UniqueId MessageId
		{
			get
			{
				return this.messageId;
			}
		}

		// Token: 0x170015F4 RID: 5620
		// (get) Token: 0x06005AEE RID: 23278 RVA: 0x0014DE74 File Offset: 0x0014C074
		// (set) Token: 0x06005AEF RID: 23279 RVA: 0x0014DE7C File Offset: 0x0014C07C
		public WsrmMessageHeader MessageHeader
		{
			get
			{
				return this.messageHeader;
			}
			set
			{
				this.messageHeader = value;
			}
		}

		// Token: 0x170015F5 RID: 5621
		// (set) Token: 0x06005AF0 RID: 23280 RVA: 0x0014DE85 File Offset: 0x0014C085
		public MessageVersion MessageVersion
		{
			set
			{
				this.messageVersion = value;
			}
		}

		// Token: 0x170015F6 RID: 5622
		// (set) Token: 0x06005AF1 RID: 23281 RVA: 0x0014DE8E File Offset: 0x0014C08E
		public string TimeoutString1Index
		{
			set
			{
				this.timeoutString1Index = value;
			}
		}

		// Token: 0x06005AF2 RID: 23282 RVA: 0x0014DE97 File Offset: 0x0014C097
		public void Abort(CommunicationObject communicationObject)
		{
			this.abortHandle.Abort(communicationObject);
		}

		// Token: 0x06005AF3 RID: 23283 RVA: 0x0014DEA8 File Offset: 0x0014C0A8
		private Message CreateRequestMessage()
		{
			Message message = Message.CreateMessage(this.messageVersion, this.messageAction, this.messageBody);
			message.Properties.AllowOutputBatching = false;
			if (this.messageHeader != null)
			{
				message.Headers.Insert(0, this.messageHeader);
			}
			if (this.messageId != null)
			{
				message.Headers.MessageId = this.messageId;
				RequestReplyCorrelator.PrepareRequest(message);
				EndpointAddress localAddress = this.binder.LocalAddress;
				if (localAddress == null)
				{
					message.Headers.ReplyTo = null;
				}
				else if (this.messageVersion.Addressing == AddressingVersion.WSAddressingAugust2004)
				{
					message.Headers.ReplyTo = localAddress;
				}
				else
				{
					if (this.messageVersion.Addressing != AddressingVersion.WSAddressing10)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ProtocolException(SR.GetString("AddressingVersionNotSupported", new object[]
						{
							this.messageVersion.Addressing
						})));
					}
					message.Headers.ReplyTo = (localAddress.IsAnonymous ? null : localAddress);
				}
			}
			return message;
		}

		// Token: 0x06005AF4 RID: 23284 RVA: 0x0014DFB8 File Offset: 0x0014C1B8
		private bool EnsureChannel()
		{
			if (this.IsCreateSequence)
			{
				IClientReliableChannelBinder clientReliableChannelBinder = (IClientReliableChannelBinder)this.binder;
				return clientReliableChannelBinder.EnsureChannelForRequest();
			}
			return true;
		}

		// Token: 0x06005AF5 RID: 23285 RVA: 0x0014DFE1 File Offset: 0x0014C1E1
		public virtual void Fault(CommunicationObject communicationObject)
		{
			this.abortHandle.Fault(communicationObject);
		}

		// Token: 0x06005AF6 RID: 23286
		public abstract WsrmMessageInfo GetInfo();

		// Token: 0x06005AF7 RID: 23287 RVA: 0x0014DFEF File Offset: 0x0014C1EF
		private TimeSpan GetNextRequestTimeout(TimeSpan remainingTimeout, out TimeoutHelper iterationTimeout, out bool lastIteration)
		{
			iterationTimeout = new TimeoutHelper(ReliableMessagingConstants.RequestorIterationTime);
			lastIteration = (remainingTimeout <= iterationTimeout.RemainingTime());
			return remainingTimeout;
		}

		// Token: 0x06005AF8 RID: 23288 RVA: 0x0014E010 File Offset: 0x0014C210
		private bool HandleException(Exception exception, bool lastIteration)
		{
			if (!this.IsCreateSequence)
			{
				return this.binder.IsHandleable(exception);
			}
			if (exception is QuotaExceededException)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new CommunicationException(exception.Message, exception));
			}
			return this.binder.IsHandleable(exception) && !(exception is MessageSecurityException) && !(exception is SecurityNegotiationException) && !(exception is SecurityAccessDeniedException) && this.binder.State == CommunicationState.Opened && !lastIteration;
		}

		// Token: 0x06005AF9 RID: 23289 RVA: 0x0014E092 File Offset: 0x0014C292
		private void ThrowTimeoutException()
		{
			if (this.timeoutString1Index != null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new TimeoutException(SR.GetString(this.timeoutString1Index, new object[]
				{
					this.originalTimeout
				})));
			}
		}

		// Token: 0x06005AFA RID: 23290
		protected abstract Message OnRequest(Message request, TimeSpan timeout, bool last);

		// Token: 0x06005AFB RID: 23291
		protected abstract IAsyncResult OnBeginRequest(Message request, TimeSpan timeout, AsyncCallback callback, object state);

		// Token: 0x06005AFC RID: 23292
		protected abstract Message OnEndRequest(bool last, IAsyncResult result);

		// Token: 0x06005AFD RID: 23293 RVA: 0x0014E0CC File Offset: 0x0014C2CC
		public Message Request(TimeSpan timeout)
		{
			this.originalTimeout = timeout;
			TimeoutHelper timeoutHelper = new TimeoutHelper(this.originalTimeout);
			Message message2;
			for (;;)
			{
				Message message = null;
				message2 = null;
				bool flag = false;
				TimeoutHelper timeoutHelper2;
				bool flag2;
				TimeSpan nextRequestTimeout = this.GetNextRequestTimeout(timeoutHelper.RemainingTime(), out timeoutHelper2, out flag2);
				try
				{
					if (this.EnsureChannel())
					{
						message = this.CreateRequestMessage();
						message2 = this.OnRequest(message, nextRequestTimeout, flag2);
						flag = true;
					}
				}
				catch (Exception exception)
				{
					if (Fx.IsFatal(exception) || !this.HandleException(exception, flag2))
					{
						throw;
					}
					DiagnosticUtility.TraceHandledException(exception, TraceEventType.Information);
				}
				finally
				{
					if (message != null)
					{
						message.Close();
					}
				}
				if (flag && this.ValidateReply(message2))
				{
					break;
				}
				if (flag2)
				{
					goto IL_A7;
				}
				this.abortHandle.Wait(timeoutHelper2.RemainingTime());
			}
			return message2;
			IL_A7:
			this.ThrowTimeoutException();
			return null;
		}

		// Token: 0x06005AFE RID: 23294 RVA: 0x0014E1A4 File Offset: 0x0014C3A4
		public IAsyncResult BeginRequest(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return new ReliableRequestor.RequestAsyncResult(this, timeout, callback, state);
		}

		// Token: 0x06005AFF RID: 23295 RVA: 0x0014E1AF File Offset: 0x0014C3AF
		public Message EndRequest(IAsyncResult result)
		{
			return ReliableRequestor.RequestAsyncResult.End(result);
		}

		// Token: 0x06005B00 RID: 23296
		public abstract void SetInfo(WsrmMessageInfo info);

		// Token: 0x06005B01 RID: 23297 RVA: 0x0014E1B7 File Offset: 0x0014C3B7
		public void SetRequestResponsePattern()
		{
			if (this.messageId != null)
			{
				throw Fx.AssertAndThrow("Initialize messageId only once.");
			}
			this.messageId = new UniqueId();
		}

		// Token: 0x06005B02 RID: 23298 RVA: 0x0014E1DD File Offset: 0x0014C3DD
		private bool ValidateReply(Message response)
		{
			return !(this.messageId != null) || response != null;
		}

		// Token: 0x040036C9 RID: 14025
		private InterruptibleWaitObject abortHandle = new InterruptibleWaitObject(false, false);

		// Token: 0x040036CA RID: 14026
		private IReliableChannelBinder binder;

		// Token: 0x040036CB RID: 14027
		private bool isCreateSequence;

		// Token: 0x040036CC RID: 14028
		private ActionHeader messageAction;

		// Token: 0x040036CD RID: 14029
		private BodyWriter messageBody;

		// Token: 0x040036CE RID: 14030
		private WsrmMessageHeader messageHeader;

		// Token: 0x040036CF RID: 14031
		private UniqueId messageId;

		// Token: 0x040036D0 RID: 14032
		private MessageVersion messageVersion;

		// Token: 0x040036D1 RID: 14033
		private TimeSpan originalTimeout;

		// Token: 0x040036D2 RID: 14034
		private string timeoutString1Index;

		// Token: 0x02000DCA RID: 3530
		private class RequestAsyncResult : AsyncResult
		{
			// Token: 0x06008004 RID: 32772 RVA: 0x001DC234 File Offset: 0x001DA434
			public RequestAsyncResult(ReliableRequestor requestor, TimeSpan timeout, AsyncCallback callback, object state) : base(callback, state)
			{
				this.requestor = requestor;
				this.requestor.originalTimeout = timeout;
				this.timeoutHelper = new TimeoutHelper(this.requestor.originalTimeout);
				if (this.Request(null))
				{
					base.Complete(true);
				}
			}

			// Token: 0x06008005 RID: 32773 RVA: 0x001DC284 File Offset: 0x001DA484
			public static Message End(IAsyncResult result)
			{
				ReliableRequestor.RequestAsyncResult requestAsyncResult = AsyncResult.End<ReliableRequestor.RequestAsyncResult>(result);
				return requestAsyncResult.response;
			}

			// Token: 0x06008006 RID: 32774 RVA: 0x001DC2A0 File Offset: 0x001DA4A0
			private bool Request(IAsyncResult requestResult)
			{
				for (;;)
				{
					bool flag = false;
					bool flag2 = true;
					TimeSpan timeout = (requestResult == null) ? this.requestor.GetNextRequestTimeout(this.timeoutHelper.RemainingTime(), out this.iterationTimeoutHelper, out this.lastIteration) : TimeSpan.Zero;
					try
					{
						if (requestResult == null && this.requestor.EnsureChannel())
						{
							this.request = this.requestor.CreateRequestMessage();
							requestResult = this.requestor.OnBeginRequest(this.request, timeout, ReliableRequestor.RequestAsyncResult.requestCallback, this);
							if (!requestResult.CompletedSynchronously)
							{
								flag2 = false;
								return false;
							}
						}
						if (requestResult != null)
						{
							this.response = this.requestor.OnEndRequest(this.lastIteration, requestResult);
							flag = true;
						}
					}
					catch (Exception exception)
					{
						if (Fx.IsFatal(exception) || !this.requestor.HandleException(exception, this.lastIteration))
						{
							throw;
						}
						DiagnosticUtility.TraceHandledException(exception, TraceEventType.Information);
					}
					finally
					{
						if (flag2 && this.request != null)
						{
							this.request.Close();
							this.request = null;
						}
						requestResult = null;
					}
					if (flag && this.requestor.ValidateReply(this.response))
					{
						break;
					}
					if (this.lastIteration)
					{
						goto IL_14C;
					}
					IAsyncResult asyncResult = this.requestor.abortHandle.BeginWait(this.iterationTimeoutHelper.RemainingTime(), ReliableRequestor.RequestAsyncResult.waitCallback, this);
					if (!asyncResult.CompletedSynchronously)
					{
						return false;
					}
					this.requestor.abortHandle.EndWait(asyncResult);
				}
				return true;
				IL_14C:
				this.requestor.ThrowTimeoutException();
				return true;
			}

			// Token: 0x06008007 RID: 32775 RVA: 0x001DC424 File Offset: 0x001DA624
			private static void RequestCallback(IAsyncResult result)
			{
				if (!result.CompletedSynchronously)
				{
					ReliableRequestor.RequestAsyncResult requestAsyncResult = (ReliableRequestor.RequestAsyncResult)result.AsyncState;
					bool flag;
					Exception exception;
					try
					{
						flag = requestAsyncResult.Request(result);
						exception = null;
					}
					catch (Exception ex)
					{
						if (Fx.IsFatal(ex))
						{
							throw;
						}
						flag = true;
						exception = ex;
					}
					if (flag)
					{
						requestAsyncResult.Complete(false, exception);
					}
				}
			}

			// Token: 0x06008008 RID: 32776 RVA: 0x001DC480 File Offset: 0x001DA680
			private bool EndWait(IAsyncResult result)
			{
				this.requestor.abortHandle.EndWait(result);
				return this.Request(null);
			}

			// Token: 0x06008009 RID: 32777 RVA: 0x001DC49C File Offset: 0x001DA69C
			private static void WaitCallback(IAsyncResult result)
			{
				if (!result.CompletedSynchronously)
				{
					ReliableRequestor.RequestAsyncResult requestAsyncResult = (ReliableRequestor.RequestAsyncResult)result.AsyncState;
					bool flag;
					Exception exception;
					try
					{
						flag = requestAsyncResult.EndWait(result);
						exception = null;
					}
					catch (Exception ex)
					{
						if (Fx.IsFatal(ex))
						{
							throw;
						}
						flag = true;
						exception = ex;
					}
					if (flag)
					{
						requestAsyncResult.Complete(false, exception);
					}
				}
			}

			// Token: 0x0400492A RID: 18730
			private static AsyncCallback requestCallback = Fx.ThunkCallback(new AsyncCallback(ReliableRequestor.RequestAsyncResult.RequestCallback));

			// Token: 0x0400492B RID: 18731
			private static AsyncCallback waitCallback = Fx.ThunkCallback(new AsyncCallback(ReliableRequestor.RequestAsyncResult.WaitCallback));

			// Token: 0x0400492C RID: 18732
			private TimeoutHelper iterationTimeoutHelper;

			// Token: 0x0400492D RID: 18733
			private bool lastIteration;

			// Token: 0x0400492E RID: 18734
			private Message request;

			// Token: 0x0400492F RID: 18735
			private ReliableRequestor requestor;

			// Token: 0x04004930 RID: 18736
			private Message response;

			// Token: 0x04004931 RID: 18737
			private TimeoutHelper timeoutHelper;
		}
	}
}
