using System;
using System.Runtime;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000953 RID: 2387
	internal sealed class ReliableOutputSessionChannelOverRequest : ReliableOutputSessionChannel
	{
		// Token: 0x06005BFF RID: 23551 RVA: 0x00151873 File Offset: 0x0014FA73
		public ReliableOutputSessionChannelOverRequest(ChannelManagerBase factory, IReliableFactorySettings settings, IClientReliableChannelBinder binder, FaultHelper faultHelper, LateBoundChannelParameterCollection channelParameters) : base(factory, settings, binder, faultHelper, channelParameters)
		{
			this.binder = binder;
		}

		// Token: 0x17001619 RID: 5657
		// (get) Token: 0x06005C00 RID: 23552 RVA: 0x00151889 File Offset: 0x0014FA89
		protected override bool RequestAcks
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06005C01 RID: 23553 RVA: 0x0015188C File Offset: 0x0014FA8C
		protected override ReliableRequestor CreateRequestor()
		{
			return new RequestReliableRequestor();
		}

		// Token: 0x06005C02 RID: 23554 RVA: 0x00151894 File Offset: 0x0014FA94
		protected override void OnConnectionSend(Message message, TimeSpan timeout, bool saveHandledException, bool maskUnhandledException)
		{
			MaskingMode maskingMode = maskUnhandledException ? MaskingMode.Unhandled : MaskingMode.None;
			Message message2;
			if (saveHandledException)
			{
				try
				{
					message2 = this.binder.Request(message, timeout, maskingMode);
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
			message2 = this.binder.Request(message, timeout, maskingMode);
			if (message2 != null)
			{
				base.ProcessMessage(message2);
			}
		}

		// Token: 0x06005C03 RID: 23555 RVA: 0x00151914 File Offset: 0x0014FB14
		protected override IAsyncResult OnConnectionBeginSend(MessageAttemptInfo attemptInfo, TimeSpan timeout, bool maskUnhandledException, AsyncCallback callback, object state)
		{
			ReliableBinderRequestAsyncResult reliableBinderRequestAsyncResult = new ReliableBinderRequestAsyncResult(callback, state);
			reliableBinderRequestAsyncResult.Binder = this.binder;
			reliableBinderRequestAsyncResult.MessageAttemptInfo = attemptInfo;
			reliableBinderRequestAsyncResult.MaskingMode = (maskUnhandledException ? MaskingMode.Unhandled : MaskingMode.None);
			if (attemptInfo.RetryCount < base.Settings.MaxRetryCount)
			{
				reliableBinderRequestAsyncResult.MaskingMode |= MaskingMode.Handled;
				reliableBinderRequestAsyncResult.SaveHandledException = false;
			}
			else
			{
				reliableBinderRequestAsyncResult.SaveHandledException = true;
			}
			reliableBinderRequestAsyncResult.Begin(timeout);
			return reliableBinderRequestAsyncResult;
		}

		// Token: 0x06005C04 RID: 23556 RVA: 0x00151988 File Offset: 0x0014FB88
		protected override void OnConnectionEndSend(IAsyncResult result)
		{
			Exception maxRetryCountException;
			Message message = ReliableBinderRequestAsyncResult.End(result, out maxRetryCountException);
			ReliableBinderRequestAsyncResult reliableBinderRequestAsyncResult = (ReliableBinderRequestAsyncResult)result;
			if (reliableBinderRequestAsyncResult.MessageAttemptInfo.RetryCount == base.Settings.MaxRetryCount)
			{
				base.MaxRetryCountException = maxRetryCountException;
			}
			if (message != null)
			{
				base.ProcessMessage(message);
			}
		}

		// Token: 0x06005C05 RID: 23557 RVA: 0x001519D4 File Offset: 0x0014FBD4
		protected override void OnConnectionSendMessage(Message message, TimeSpan timeout, MaskingMode maskingMode)
		{
			Message message2 = this.binder.Request(message, timeout, maskingMode);
			if (message2 != null)
			{
				base.ProcessMessage(message2);
			}
		}

		// Token: 0x06005C06 RID: 23558 RVA: 0x001519FC File Offset: 0x0014FBFC
		protected override IAsyncResult OnConnectionBeginSendMessage(Message message, TimeSpan timeout, AsyncCallback callback, object state)
		{
			ReliableBinderRequestAsyncResult reliableBinderRequestAsyncResult = new ReliableBinderRequestAsyncResult(callback, state);
			reliableBinderRequestAsyncResult.Binder = this.binder;
			reliableBinderRequestAsyncResult.MaskingMode = MaskingMode.Handled;
			reliableBinderRequestAsyncResult.Message = message;
			reliableBinderRequestAsyncResult.Begin(timeout);
			return reliableBinderRequestAsyncResult;
		}

		// Token: 0x06005C07 RID: 23559 RVA: 0x00151A34 File Offset: 0x0014FC34
		protected override void OnConnectionEndSendMessage(IAsyncResult result)
		{
			Message message = ReliableBinderRequestAsyncResult.End(result);
			if (message != null)
			{
				base.ProcessMessage(message);
			}
		}

		// Token: 0x06005C08 RID: 23560 RVA: 0x00151A54 File Offset: 0x0014FC54
		protected override WsrmFault ProcessRequestorResponse(ReliableRequestor requestor, string requestName, WsrmMessageInfo info)
		{
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

		// Token: 0x04003719 RID: 14105
		private IClientReliableChannelBinder binder;
	}
}
