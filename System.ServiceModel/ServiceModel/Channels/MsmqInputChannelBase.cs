using System;
using System.Runtime;

namespace System.ServiceModel.Channels
{
	// Token: 0x020008E3 RID: 2275
	internal abstract class MsmqInputChannelBase : ChannelBase, IInputChannel, IChannel, ICommunicationObject
	{
		// Token: 0x060056A9 RID: 22185 RVA: 0x0013E2CC File Offset: 0x0013C4CC
		public MsmqInputChannelBase(MsmqInputChannelListenerBase listener, IMsmqMessagePool messagePool) : base(listener)
		{
			this.receiveParameters = listener.ReceiveParameters;
			this.receiver = new MsmqReceiveHelper(listener.ReceiveParameters, listener.Uri, messagePool, this, listener);
			this.localAddress = new EndpointAddress(listener.Uri, new AddressHeader[0]);
			this.listener = listener;
			if (this.receiveParameters.ReceiveContextSettings.Enabled)
			{
				this.receiveContextManager = new MsmqReceiveContextLockManager(this.receiveParameters.ReceiveContextSettings, this.receiver.Queue);
			}
		}

		// Token: 0x1700152B RID: 5419
		// (get) Token: 0x060056AA RID: 22186 RVA: 0x0013E357 File Offset: 0x0013C557
		public EndpointAddress LocalAddress
		{
			get
			{
				return this.localAddress;
			}
		}

		// Token: 0x1700152C RID: 5420
		// (get) Token: 0x060056AB RID: 22187 RVA: 0x0013E35F File Offset: 0x0013C55F
		protected MsmqReceiveHelper MsmqReceiveHelper
		{
			get
			{
				return this.receiver;
			}
		}

		// Token: 0x1700152D RID: 5421
		// (get) Token: 0x060056AC RID: 22188 RVA: 0x0013E367 File Offset: 0x0013C567
		protected MsmqReceiveParameters ReceiveParameters
		{
			get
			{
				return this.receiveParameters;
			}
		}

		// Token: 0x060056AD RID: 22189 RVA: 0x0013E36F File Offset: 0x0013C56F
		protected virtual void OnCloseCore(bool isAborting)
		{
			this.receiver.Close();
			if (this.receiveContextManager != null)
			{
				this.receiveContextManager.Dispose();
			}
		}

		// Token: 0x060056AE RID: 22190 RVA: 0x0013E390 File Offset: 0x0013C590
		protected virtual void OnOpenCore()
		{
			try
			{
				this.receiver.Open();
			}
			catch (MsmqException ex)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(ex.Normalized);
			}
		}

		// Token: 0x060056AF RID: 22191 RVA: 0x0013E3CC File Offset: 0x0013C5CC
		protected override IAsyncResult OnBeginOpen(TimeSpan timeout, AsyncCallback callback, object state)
		{
			this.OnOpenCore();
			return new CompletedAsyncResult(callback, state);
		}

		// Token: 0x060056B0 RID: 22192 RVA: 0x0013E3DB File Offset: 0x0013C5DB
		protected override void OnOpen(TimeSpan timeout)
		{
			this.OnOpenCore();
		}

		// Token: 0x060056B1 RID: 22193 RVA: 0x0013E3E3 File Offset: 0x0013C5E3
		protected override void OnEndOpen(IAsyncResult result)
		{
			CompletedAsyncResult.End(result);
		}

		// Token: 0x060056B2 RID: 22194 RVA: 0x0013E3EB File Offset: 0x0013C5EB
		protected override void OnAbort()
		{
			this.OnCloseCore(true);
		}

		// Token: 0x060056B3 RID: 22195 RVA: 0x0013E3F4 File Offset: 0x0013C5F4
		protected override IAsyncResult OnBeginClose(TimeSpan timeout, AsyncCallback callback, object state)
		{
			this.OnCloseCore(false);
			return new CompletedAsyncResult(callback, state);
		}

		// Token: 0x060056B4 RID: 22196 RVA: 0x0013E404 File Offset: 0x0013C604
		protected override void OnClose(TimeSpan timeout)
		{
			this.OnCloseCore(false);
		}

		// Token: 0x060056B5 RID: 22197 RVA: 0x0013E40D File Offset: 0x0013C60D
		protected override void OnEndClose(IAsyncResult result)
		{
			CompletedAsyncResult.End(result);
		}

		// Token: 0x060056B6 RID: 22198 RVA: 0x0013E415 File Offset: 0x0013C615
		protected override void OnFaulted()
		{
			this.OnCloseCore(true);
			base.OnFaulted();
		}

		// Token: 0x060056B7 RID: 22199
		protected abstract Message DecodeMsmqMessage(MsmqInputMessage msmqMessage, MsmqMessageProperty property);

		// Token: 0x060056B8 RID: 22200 RVA: 0x0013E424 File Offset: 0x0013C624
		internal void FaultChannel()
		{
			base.Fault();
		}

		// Token: 0x060056B9 RID: 22201 RVA: 0x0013E42C File Offset: 0x0013C62C
		public Message Receive()
		{
			return this.Receive(base.DefaultReceiveTimeout);
		}

		// Token: 0x060056BA RID: 22202 RVA: 0x0013E43A File Offset: 0x0013C63A
		public Message Receive(TimeSpan timeout)
		{
			return InputChannel.HelpReceive(this, timeout);
		}

		// Token: 0x060056BB RID: 22203 RVA: 0x0013E443 File Offset: 0x0013C643
		public IAsyncResult BeginReceive(AsyncCallback callback, object state)
		{
			return this.BeginReceive(base.DefaultReceiveTimeout, callback, state);
		}

		// Token: 0x060056BC RID: 22204 RVA: 0x0013E454 File Offset: 0x0013C654
		public IAsyncResult BeginReceive(TimeSpan timeout, AsyncCallback callback, object state)
		{
			IAsyncResult result;
			using (MsmqDiagnostics.BoundReceiveOperation(this.receiver))
			{
				result = InputChannel.HelpBeginReceive(this, timeout, callback, state);
			}
			return result;
		}

		// Token: 0x060056BD RID: 22205 RVA: 0x0013E494 File Offset: 0x0013C694
		public Message EndReceive(IAsyncResult result)
		{
			return InputChannel.HelpEndReceive(result);
		}

		// Token: 0x060056BE RID: 22206 RVA: 0x0013E49C File Offset: 0x0013C69C
		public bool TryReceive(TimeSpan timeout, out Message message)
		{
			message = null;
			if (base.DoneReceivingInCurrentState())
			{
				return true;
			}
			bool result;
			using (MsmqDiagnostics.BoundReceiveOperation(this.receiver))
			{
				MsmqInputMessage msmqInputMessage = this.receiver.TakeMessage();
				try
				{
					MsmqMessageProperty msmqMessageProperty;
					bool flag = this.receiver.TryReceive(msmqInputMessage, timeout, this.ReceiveParameters.ExactlyOnce ? MsmqTransactionMode.CurrentOrNone : MsmqTransactionMode.None, out msmqMessageProperty);
					if (flag)
					{
						if (msmqMessageProperty != null)
						{
							message = this.DecodeMsmqMessage(msmqInputMessage, msmqMessageProperty);
							message.Properties["MsmqMessageProperty"] = msmqMessageProperty;
							if (this.receiveParameters.ReceiveContextSettings.Enabled)
							{
								message.Properties[ReceiveContext.Name] = this.receiveContextManager.CreateMsmqReceiveContext(msmqInputMessage.LookupId.Value);
							}
							MsmqDiagnostics.DatagramReceived(msmqInputMessage.MessageId, message);
							this.listener.RaiseMessageReceived();
						}
						else if (CommunicationState.Opened == base.State)
						{
							this.listener.FaultListener();
							base.Fault();
						}
					}
					result = flag;
				}
				catch (MsmqException ex)
				{
					if (ex.FaultReceiver)
					{
						this.listener.FaultListener();
						base.Fault();
					}
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(ex.Normalized);
				}
				finally
				{
					this.receiver.ReturnMessage(msmqInputMessage);
				}
			}
			return result;
		}

		// Token: 0x060056BF RID: 22207 RVA: 0x0013E5F8 File Offset: 0x0013C7F8
		public IAsyncResult BeginTryReceive(TimeSpan timeout, AsyncCallback callback, object state)
		{
			if (base.DoneReceivingInCurrentState())
			{
				return new DoneReceivingAsyncResult(callback, state);
			}
			MsmqInputMessage msmqMessage = this.receiver.TakeMessage();
			return this.receiver.BeginTryReceive(msmqMessage, timeout, this.ReceiveParameters.ExactlyOnce ? MsmqTransactionMode.CurrentOrNone : MsmqTransactionMode.None, callback, state);
		}

		// Token: 0x060056C0 RID: 22208 RVA: 0x0013E644 File Offset: 0x0013C844
		public bool EndTryReceive(IAsyncResult result, out Message message)
		{
			message = null;
			if (result == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("result");
			}
			DoneReceivingAsyncResult doneReceivingAsyncResult = result as DoneReceivingAsyncResult;
			if (doneReceivingAsyncResult != null)
			{
				return DoneReceivingAsyncResult.End(doneReceivingAsyncResult);
			}
			MsmqInputMessage msmqInputMessage = null;
			MsmqMessageProperty msmqMessageProperty = null;
			bool result2;
			try
			{
				bool flag = this.receiver.EndTryReceive(result, out msmqInputMessage, out msmqMessageProperty);
				if (flag)
				{
					if (msmqMessageProperty != null)
					{
						message = this.DecodeMsmqMessage(msmqInputMessage, msmqMessageProperty);
						message.Properties["MsmqMessageProperty"] = msmqMessageProperty;
						if (this.receiveParameters.ReceiveContextSettings.Enabled)
						{
							message.Properties[ReceiveContext.Name] = this.receiveContextManager.CreateMsmqReceiveContext(msmqInputMessage.LookupId.Value);
						}
						MsmqDiagnostics.DatagramReceived(msmqInputMessage.MessageId, message);
						this.listener.RaiseMessageReceived();
					}
					else if (CommunicationState.Opened == base.State)
					{
						this.listener.FaultListener();
						base.Fault();
					}
				}
				result2 = flag;
			}
			catch (MsmqException ex)
			{
				if (ex.FaultReceiver)
				{
					this.listener.FaultListener();
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

		// Token: 0x060056C1 RID: 22209 RVA: 0x0013E780 File Offset: 0x0013C980
		public bool WaitForMessage(TimeSpan timeout)
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
					this.listener.FaultListener();
					base.Fault();
				}
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(ex.Normalized);
			}
			return result;
		}

		// Token: 0x060056C2 RID: 22210 RVA: 0x0013E7E4 File Offset: 0x0013C9E4
		public IAsyncResult BeginWaitForMessage(TimeSpan timeout, AsyncCallback callback, object state)
		{
			if (base.DoneReceivingInCurrentState())
			{
				return new DoneReceivingAsyncResult(callback, state);
			}
			return this.receiver.BeginWaitForMessage(timeout, callback, state);
		}

		// Token: 0x060056C3 RID: 22211 RVA: 0x0013E804 File Offset: 0x0013CA04
		public bool EndWaitForMessage(IAsyncResult result)
		{
			if (result == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("result");
			}
			DoneReceivingAsyncResult doneReceivingAsyncResult = result as DoneReceivingAsyncResult;
			if (doneReceivingAsyncResult != null)
			{
				return DoneReceivingAsyncResult.End(doneReceivingAsyncResult);
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
					this.listener.FaultListener();
					base.Fault();
				}
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(ex.Normalized);
			}
			return result2;
		}

		// Token: 0x04003583 RID: 13699
		private EndpointAddress localAddress;

		// Token: 0x04003584 RID: 13700
		private MsmqReceiveHelper receiver;

		// Token: 0x04003585 RID: 13701
		private MsmqReceiveParameters receiveParameters;

		// Token: 0x04003586 RID: 13702
		private MsmqInputChannelListenerBase listener;

		// Token: 0x04003587 RID: 13703
		private MsmqReceiveContextLockManager receiveContextManager;
	}
}
