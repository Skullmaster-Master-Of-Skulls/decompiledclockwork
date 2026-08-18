using System;
using System.Runtime;
using System.Threading;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000954 RID: 2388
	internal sealed class ReliableOutputSessionChannelOverDuplex : ReliableOutputSessionChannel
	{
		// Token: 0x06005C09 RID: 23561 RVA: 0x00151A9D File Offset: 0x0014FC9D
		public ReliableOutputSessionChannelOverDuplex(ChannelManagerBase factory, IReliableFactorySettings settings, IClientReliableChannelBinder binder, FaultHelper faultHelper, LateBoundChannelParameterCollection channelParameters) : base(factory, settings, binder, faultHelper, channelParameters)
		{
		}

		// Token: 0x1700161A RID: 5658
		// (get) Token: 0x06005C0A RID: 23562 RVA: 0x00151AAC File Offset: 0x0014FCAC
		protected override bool RequestAcks
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06005C0B RID: 23563 RVA: 0x00151AAF File Offset: 0x0014FCAF
		protected override ReliableRequestor CreateRequestor()
		{
			return new SendWaitReliableRequestor();
		}

		// Token: 0x06005C0C RID: 23564 RVA: 0x00151AB8 File Offset: 0x0014FCB8
		protected override void OnConnectionSend(Message message, TimeSpan timeout, bool saveHandledException, bool maskUnhandledException)
		{
			MaskingMode maskingMode = maskUnhandledException ? MaskingMode.Unhandled : MaskingMode.None;
			if (saveHandledException)
			{
				try
				{
					base.Binder.Send(message, timeout, maskingMode);
					return;
				}
				catch (Exception ex)
				{
					if (Fx.IsFatal(ex))
					{
						throw;
					}
					if (base.Binder.IsHandleable(ex))
					{
						base.MaxRetryCountException = ex;
						return;
					}
					throw;
				}
			}
			maskingMode |= MaskingMode.Handled;
			base.Binder.Send(message, timeout, maskingMode);
		}

		// Token: 0x06005C0D RID: 23565 RVA: 0x00151B2C File Offset: 0x0014FD2C
		protected override IAsyncResult OnConnectionBeginSend(MessageAttemptInfo attemptInfo, TimeSpan timeout, bool maskUnhandledException, AsyncCallback callback, object state)
		{
			ReliableBinderSendAsyncResult reliableBinderSendAsyncResult = new ReliableBinderSendAsyncResult(callback, state);
			reliableBinderSendAsyncResult.Binder = base.Binder;
			reliableBinderSendAsyncResult.MessageAttemptInfo = attemptInfo;
			reliableBinderSendAsyncResult.MaskingMode = (maskUnhandledException ? MaskingMode.Unhandled : MaskingMode.None);
			if (attemptInfo.RetryCount < base.Settings.MaxRetryCount)
			{
				reliableBinderSendAsyncResult.MaskingMode |= MaskingMode.Handled;
				reliableBinderSendAsyncResult.SaveHandledException = false;
			}
			else
			{
				reliableBinderSendAsyncResult.SaveHandledException = true;
			}
			reliableBinderSendAsyncResult.Begin(timeout);
			return reliableBinderSendAsyncResult;
		}

		// Token: 0x06005C0E RID: 23566 RVA: 0x00151BA0 File Offset: 0x0014FDA0
		protected override void OnConnectionEndSend(IAsyncResult result)
		{
			Exception maxRetryCountException;
			ReliableBinderSendAsyncResult.End(result, out maxRetryCountException);
			ReliableBinderSendAsyncResult reliableBinderSendAsyncResult = (ReliableBinderSendAsyncResult)result;
			if (reliableBinderSendAsyncResult.MessageAttemptInfo.RetryCount == base.Settings.MaxRetryCount)
			{
				base.MaxRetryCountException = maxRetryCountException;
			}
		}

		// Token: 0x06005C0F RID: 23567 RVA: 0x00151BDE File Offset: 0x0014FDDE
		protected override void OnConnectionSendMessage(Message message, TimeSpan timeout, MaskingMode maskingMode)
		{
			base.Binder.Send(message, timeout, maskingMode);
		}

		// Token: 0x06005C10 RID: 23568 RVA: 0x00151BF0 File Offset: 0x0014FDF0
		protected override IAsyncResult OnConnectionBeginSendMessage(Message message, TimeSpan timeout, AsyncCallback callback, object state)
		{
			ReliableBinderSendAsyncResult reliableBinderSendAsyncResult = new ReliableBinderSendAsyncResult(callback, state);
			reliableBinderSendAsyncResult.Binder = base.Binder;
			reliableBinderSendAsyncResult.MaskingMode = MaskingMode.Unhandled;
			reliableBinderSendAsyncResult.Message = message;
			reliableBinderSendAsyncResult.Begin(timeout);
			return reliableBinderSendAsyncResult;
		}

		// Token: 0x06005C11 RID: 23569 RVA: 0x00151C28 File Offset: 0x0014FE28
		protected override void OnConnectionEndSendMessage(IAsyncResult result)
		{
			ReliableBinderSendAsyncResult.End(result);
		}

		// Token: 0x06005C12 RID: 23570 RVA: 0x00151C30 File Offset: 0x0014FE30
		protected override void OnOpened()
		{
			base.OnOpened();
			if (Thread.CurrentThread.IsThreadPoolThread)
			{
				try
				{
					this.StartReceiving();
					return;
				}
				catch (Exception ex)
				{
					if (Fx.IsFatal(ex))
					{
						throw;
					}
					base.ReliableSession.OnUnknownException(ex);
					return;
				}
			}
			ActionItem.Schedule(new Action<object>(ReliableOutputSessionChannelOverDuplex.StartReceiving), this);
		}

		// Token: 0x06005C13 RID: 23571 RVA: 0x00151C94 File Offset: 0x0014FE94
		private static void OnReceiveCompletedStatic(IAsyncResult result)
		{
			ReliableOutputSessionChannelOverDuplex reliableOutputSessionChannelOverDuplex = (ReliableOutputSessionChannelOverDuplex)result.AsyncState;
			try
			{
				reliableOutputSessionChannelOverDuplex.OnReceiveCompleted(result);
			}
			catch (Exception ex)
			{
				if (Fx.IsFatal(ex))
				{
					throw;
				}
				reliableOutputSessionChannelOverDuplex.ReliableSession.OnUnknownException(ex);
			}
		}

		// Token: 0x06005C14 RID: 23572 RVA: 0x00151CE0 File Offset: 0x0014FEE0
		private void OnReceiveCompleted(IAsyncResult result)
		{
			RequestContext requestContext;
			if (base.Binder.EndTryReceive(result, out requestContext))
			{
				if (requestContext != null)
				{
					using (requestContext)
					{
						Message requestMessage = requestContext.RequestMessage;
						base.ProcessMessage(requestMessage);
						requestContext.Close(this.DefaultCloseTimeout);
					}
					base.Binder.BeginTryReceive(TimeSpan.MaxValue, ReliableOutputSessionChannelOverDuplex.onReceiveCompleted, this);
					return;
				}
				if (!base.Connection.Closed && base.Binder.State == CommunicationState.Opened)
				{
					Exception e = new CommunicationException(SR.GetString("EarlySecurityClose"));
					base.ReliableSession.OnLocalFault(e, null, null);
					return;
				}
			}
			else
			{
				base.Binder.BeginTryReceive(TimeSpan.MaxValue, ReliableOutputSessionChannelOverDuplex.onReceiveCompleted, this);
			}
		}

		// Token: 0x06005C15 RID: 23573 RVA: 0x00151DA4 File Offset: 0x0014FFA4
		protected override WsrmFault ProcessRequestorResponse(ReliableRequestor requestor, string requestName, WsrmMessageInfo info)
		{
			if (requestor != null)
			{
				requestor.SetInfo(info);
				return null;
			}
			string @string = SR.GetString("ReceivedResponseBeforeRequestFaultString", new object[]
			{
				requestName
			});
			string string2 = SR.GetString("ReceivedResponseBeforeRequestExceptionString", new object[]
			{
				requestName
			});
			return SequenceTerminatedFault.CreateProtocolFault(base.ReliableSession.OutputID, @string, string2);
		}

		// Token: 0x06005C16 RID: 23574 RVA: 0x00151DF9 File Offset: 0x0014FFF9
		private void StartReceiving()
		{
			base.Binder.BeginTryReceive(TimeSpan.MaxValue, ReliableOutputSessionChannelOverDuplex.onReceiveCompleted, this);
		}

		// Token: 0x06005C17 RID: 23575 RVA: 0x00151E14 File Offset: 0x00150014
		private static void StartReceiving(object state)
		{
			ReliableOutputSessionChannelOverDuplex reliableOutputSessionChannelOverDuplex = (ReliableOutputSessionChannelOverDuplex)state;
			try
			{
				reliableOutputSessionChannelOverDuplex.StartReceiving();
			}
			catch (Exception ex)
			{
				if (Fx.IsFatal(ex))
				{
					throw;
				}
				reliableOutputSessionChannelOverDuplex.ReliableSession.OnUnknownException(ex);
			}
		}

		// Token: 0x0400371A RID: 14106
		private static AsyncCallback onReceiveCompleted = Fx.ThunkCallback(new AsyncCallback(ReliableOutputSessionChannelOverDuplex.OnReceiveCompletedStatic));
	}
}
