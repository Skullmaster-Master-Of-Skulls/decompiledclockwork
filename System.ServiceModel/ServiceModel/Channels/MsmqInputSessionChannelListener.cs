using System;
using System.Runtime;
using System.Transactions;

namespace System.ServiceModel.Channels
{
	// Token: 0x020008EA RID: 2282
	internal sealed class MsmqInputSessionChannelListener : MsmqChannelListenerBase<IInputSessionChannel>
	{
		// Token: 0x06005705 RID: 22277 RVA: 0x0013F440 File Offset: 0x0013D640
		internal MsmqInputSessionChannelListener(MsmqBindingElementBase bindingElement, BindingContext context, MsmqReceiveParameters receiveParameters) : base(bindingElement, context, receiveParameters, TransportDefaults.GetDefaultMessageEncoderFactory())
		{
			base.SetSecurityTokenAuthenticator(MsmqUri.NetMsmqAddressTranslator.Scheme, context);
			this.receiver = new MsmqReceiveHelper(base.ReceiveParameters, this.Uri, new MsmqInputMessagePool((base.ReceiveParameters as MsmqTransportReceiveParameters).MaxPoolSize), null, this);
			if (base.ReceiveParameters.ReceiveContextSettings.Enabled)
			{
				this.receiveContextManager = new MsmqReceiveContextLockManager(base.ReceiveParameters.ReceiveContextSettings, this.receiver.Queue);
			}
		}

		// Token: 0x17001533 RID: 5427
		// (get) Token: 0x06005706 RID: 22278 RVA: 0x0013F4CD File Offset: 0x0013D6CD
		internal MsmqReceiveHelper MsmqReceiveHelper
		{
			get
			{
				return this.receiver;
			}
		}

		// Token: 0x06005707 RID: 22279 RVA: 0x0013F4D5 File Offset: 0x0013D6D5
		protected override void OnCloseCore(bool aborting)
		{
			if (this.receiver != null)
			{
				this.receiver.Close();
			}
			if (this.receiveContextManager != null)
			{
				this.receiveContextManager.Dispose();
			}
		}

		// Token: 0x06005708 RID: 22280 RVA: 0x0013F500 File Offset: 0x0013D700
		protected override void OnOpenCore(TimeSpan timeout)
		{
			base.OnOpenCore(timeout);
			try
			{
				this.receiver.Open();
			}
			catch (MsmqException ex)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(ex.Normalized);
			}
		}

		// Token: 0x06005709 RID: 22281 RVA: 0x0013F544 File Offset: 0x0013D744
		public override IInputSessionChannel AcceptChannel()
		{
			return this.AcceptChannel(this.DefaultReceiveTimeout);
		}

		// Token: 0x0600570A RID: 22282 RVA: 0x0013F554 File Offset: 0x0013D754
		public override IInputSessionChannel AcceptChannel(TimeSpan timeout)
		{
			if (base.DoneReceivingInCurrentState())
			{
				return null;
			}
			if (!base.ReceiveParameters.ReceiveContextSettings.Enabled && Transaction.Current == null)
			{
				base.Fault();
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("MsmqTransactionRequired")));
			}
			MsmqInputMessage msmqInputMessage = this.receiver.TakeMessage();
			IInputSessionChannel result;
			try
			{
				MsmqMessageProperty msmqMessageProperty;
				bool flag = this.receiver.TryReceive(msmqInputMessage, timeout, MsmqTransactionMode.CurrentOrThrow, out msmqMessageProperty);
				if (!flag)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new TimeoutException());
				}
				if (msmqMessageProperty != null)
				{
					result = MsmqDecodeHelper.DecodeTransportSessiongram(this, msmqInputMessage, msmqMessageProperty, this.receiveContextManager);
				}
				else
				{
					if (CommunicationState.Opened == base.State)
					{
						base.Fault();
					}
					result = null;
				}
			}
			catch (MsmqException ex)
			{
				if (ex.FaultReceiver)
				{
					base.Fault();
				}
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(ex.Normalized);
			}
			finally
			{
				this.receiver.ReturnMessage(msmqInputMessage);
			}
			return result;
		}

		// Token: 0x0600570B RID: 22283 RVA: 0x0013F650 File Offset: 0x0013D850
		public override IAsyncResult BeginAcceptChannel(AsyncCallback callback, object state)
		{
			return this.BeginAcceptChannel(this.DefaultReceiveTimeout, callback, state);
		}

		// Token: 0x0600570C RID: 22284 RVA: 0x0013F660 File Offset: 0x0013D860
		public override IAsyncResult BeginAcceptChannel(TimeSpan timeout, AsyncCallback callback, object state)
		{
			if (base.DoneReceivingInCurrentState())
			{
				return new DoneReceivingAsyncResult(callback, state);
			}
			if (!base.ReceiveParameters.ReceiveContextSettings.Enabled && Transaction.Current == null)
			{
				base.Fault();
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("MsmqTransactionRequired")));
			}
			MsmqInputMessage msmqMessage = this.receiver.TakeMessage();
			return this.receiver.BeginTryReceive(msmqMessage, timeout, MsmqTransactionMode.CurrentOrThrow, callback, state);
		}

		// Token: 0x0600570D RID: 22285 RVA: 0x0013F6D8 File Offset: 0x0013D8D8
		public override IInputSessionChannel EndAcceptChannel(IAsyncResult result)
		{
			if (result == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("result");
			}
			DoneReceivingAsyncResult doneReceivingAsyncResult = result as DoneReceivingAsyncResult;
			if (doneReceivingAsyncResult != null)
			{
				DoneReceivingAsyncResult.End(doneReceivingAsyncResult);
				return null;
			}
			MsmqInputMessage msmqInputMessage = null;
			MsmqMessageProperty msmqMessageProperty = null;
			IInputSessionChannel result2;
			try
			{
				bool flag = this.receiver.EndTryReceive(result, out msmqInputMessage, out msmqMessageProperty);
				if (!flag)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new TimeoutException());
				}
				if (msmqMessageProperty != null)
				{
					result2 = MsmqDecodeHelper.DecodeTransportSessiongram(this, msmqInputMessage, msmqMessageProperty, this.receiveContextManager);
				}
				else
				{
					if (CommunicationState.Opened == base.State)
					{
						base.Fault();
					}
					result2 = null;
				}
			}
			catch (MsmqException ex)
			{
				if (ex.FaultReceiver)
				{
					base.Fault();
				}
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(ex.Normalized);
			}
			finally
			{
				if (msmqInputMessage != null)
				{
					this.receiver.ReturnMessage(msmqInputMessage);
				}
			}
			return result2;
		}

		// Token: 0x0600570E RID: 22286 RVA: 0x0013F7B0 File Offset: 0x0013D9B0
		protected override bool OnWaitForChannel(TimeSpan timeout)
		{
			if (base.DoneReceivingInCurrentState())
			{
				return true;
			}
			bool result;
			try
			{
				result = this.receiver.WaitForMessage(timeout);
			}
			catch (MsmqException ex)
			{
				if (ex.FaultReceiver)
				{
					base.Fault();
				}
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(ex.Normalized);
			}
			return result;
		}

		// Token: 0x0600570F RID: 22287 RVA: 0x0013F808 File Offset: 0x0013DA08
		protected override IAsyncResult OnBeginWaitForChannel(TimeSpan timeout, AsyncCallback callback, object state)
		{
			if (base.DoneReceivingInCurrentState())
			{
				return new MsmqInputSessionChannelListener.DoneAsyncResult(true, callback, state);
			}
			return this.receiver.BeginWaitForMessage(timeout, callback, state);
		}

		// Token: 0x06005710 RID: 22288 RVA: 0x0013F82C File Offset: 0x0013DA2C
		protected override bool OnEndWaitForChannel(IAsyncResult result)
		{
			if (result == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("result");
			}
			MsmqInputSessionChannelListener.DoneAsyncResult doneAsyncResult = result as MsmqInputSessionChannelListener.DoneAsyncResult;
			if (doneAsyncResult != null)
			{
				return CompletedAsyncResult<bool>.End(result);
			}
			bool result2;
			try
			{
				result2 = this.receiver.EndWaitForMessage(result);
			}
			catch (MsmqException ex)
			{
				if (ex.FaultReceiver)
				{
					base.Fault();
				}
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(ex.Normalized);
			}
			return result2;
		}

		// Token: 0x06005711 RID: 22289 RVA: 0x0013F8A0 File Offset: 0x0013DAA0
		protected override void OnFaulted()
		{
			this.OnCloseCore(true);
			base.OnFaulted();
		}

		// Token: 0x04003592 RID: 13714
		private MsmqReceiveHelper receiver;

		// Token: 0x04003593 RID: 13715
		private MsmqReceiveContextLockManager receiveContextManager;

		// Token: 0x02000D91 RID: 3473
		private class DoneAsyncResult : CompletedAsyncResult<bool>
		{
			// Token: 0x06007EAB RID: 32427 RVA: 0x001D7EB3 File Offset: 0x001D60B3
			internal DoneAsyncResult(bool data, AsyncCallback callback, object state) : base(data, callback, state)
			{
			}
		}
	}
}
