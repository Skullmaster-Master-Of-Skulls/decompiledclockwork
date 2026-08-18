using System;
using System.Collections.Generic;
using System.Runtime;
using System.Threading;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000916 RID: 2326
	internal abstract class ReliableChannelBinder<TChannel> : IReliableChannelBinder where TChannel : class, IChannel
	{
		// Token: 0x060058B4 RID: 22708 RVA: 0x00144F5C File Offset: 0x0014315C
		protected ReliableChannelBinder(TChannel channel, MaskingMode maskingMode, TolerateFaultsMode faultMode, TimeSpan defaultCloseTimeout, TimeSpan defaultSendTimeout)
		{
			if (maskingMode != MaskingMode.None && maskingMode != MaskingMode.All)
			{
				throw Fx.AssertAndThrow("ReliableChannelBinder was implemented with only 2 default masking modes, None and All.");
			}
			this.defaultMaskingMode = maskingMode;
			this.defaultCloseTimeout = defaultCloseTimeout;
			this.defaultSendTimeout = defaultSendTimeout;
			this.synchronizer = new ReliableChannelBinder<TChannel>.ChannelSynchronizer(this, channel, faultMode);
		}

		// Token: 0x170015A2 RID: 5538
		// (get) Token: 0x060058B5 RID: 22709
		protected abstract bool CanGetChannelForReceive { get; }

		// Token: 0x170015A3 RID: 5539
		// (get) Token: 0x060058B6 RID: 22710
		public abstract bool CanSendAsynchronously { get; }

		// Token: 0x170015A4 RID: 5540
		// (get) Token: 0x060058B7 RID: 22711 RVA: 0x00144FB1 File Offset: 0x001431B1
		public virtual ChannelParameterCollection ChannelParameters
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170015A5 RID: 5541
		// (get) Token: 0x060058B8 RID: 22712 RVA: 0x00144FB4 File Offset: 0x001431B4
		public IChannel Channel
		{
			get
			{
				return this.synchronizer.CurrentChannel;
			}
		}

		// Token: 0x170015A6 RID: 5542
		// (get) Token: 0x060058B9 RID: 22713 RVA: 0x00144FC6 File Offset: 0x001431C6
		public bool Connected
		{
			get
			{
				return this.synchronizer.Connected;
			}
		}

		// Token: 0x170015A7 RID: 5543
		// (get) Token: 0x060058BA RID: 22714 RVA: 0x00144FD3 File Offset: 0x001431D3
		public MaskingMode DefaultMaskingMode
		{
			get
			{
				return this.defaultMaskingMode;
			}
		}

		// Token: 0x170015A8 RID: 5544
		// (get) Token: 0x060058BB RID: 22715 RVA: 0x00144FDB File Offset: 0x001431DB
		public TimeSpan DefaultSendTimeout
		{
			get
			{
				return this.defaultSendTimeout;
			}
		}

		// Token: 0x170015A9 RID: 5545
		// (get) Token: 0x060058BC RID: 22716
		public abstract bool HasSession { get; }

		// Token: 0x170015AA RID: 5546
		// (get) Token: 0x060058BD RID: 22717
		public abstract EndpointAddress LocalAddress { get; }

		// Token: 0x170015AB RID: 5547
		// (get) Token: 0x060058BE RID: 22718
		protected abstract bool MustCloseChannel { get; }

		// Token: 0x170015AC RID: 5548
		// (get) Token: 0x060058BF RID: 22719
		protected abstract bool MustOpenChannel { get; }

		// Token: 0x170015AD RID: 5549
		// (get) Token: 0x060058C0 RID: 22720
		public abstract EndpointAddress RemoteAddress { get; }

		// Token: 0x170015AE RID: 5550
		// (get) Token: 0x060058C1 RID: 22721 RVA: 0x00144FE3 File Offset: 0x001431E3
		public CommunicationState State
		{
			get
			{
				return this.state;
			}
		}

		// Token: 0x170015AF RID: 5551
		// (get) Token: 0x060058C2 RID: 22722 RVA: 0x00144FEB File Offset: 0x001431EB
		protected ReliableChannelBinder<TChannel>.ChannelSynchronizer Synchronizer
		{
			get
			{
				return this.synchronizer;
			}
		}

		// Token: 0x170015B0 RID: 5552
		// (get) Token: 0x060058C3 RID: 22723 RVA: 0x00144FF3 File Offset: 0x001431F3
		protected object ThisLock
		{
			get
			{
				return this.thisLock;
			}
		}

		// Token: 0x170015B1 RID: 5553
		// (get) Token: 0x060058C4 RID: 22724 RVA: 0x00144FFB File Offset: 0x001431FB
		private bool TolerateFaults
		{
			get
			{
				return this.synchronizer.TolerateFaults;
			}
		}

		// Token: 0x14000038 RID: 56
		// (add) Token: 0x060058C5 RID: 22725 RVA: 0x00145008 File Offset: 0x00143208
		// (remove) Token: 0x060058C6 RID: 22726 RVA: 0x00145040 File Offset: 0x00143240
		public event EventHandler ConnectionLost;

		// Token: 0x14000039 RID: 57
		// (add) Token: 0x060058C7 RID: 22727 RVA: 0x00145078 File Offset: 0x00143278
		// (remove) Token: 0x060058C8 RID: 22728 RVA: 0x001450B0 File Offset: 0x001432B0
		public event BinderExceptionHandler Faulted;

		// Token: 0x1400003A RID: 58
		// (add) Token: 0x060058C9 RID: 22729 RVA: 0x001450E8 File Offset: 0x001432E8
		// (remove) Token: 0x060058CA RID: 22730 RVA: 0x00145120 File Offset: 0x00143320
		public event BinderExceptionHandler OnException;

		// Token: 0x060058CB RID: 22731 RVA: 0x00145158 File Offset: 0x00143358
		public void Abort()
		{
			object obj = this.ThisLock;
			TChannel tchannel;
			lock (obj)
			{
				this.aborted = true;
				if (this.state == CommunicationState.Closed)
				{
					return;
				}
				this.state = CommunicationState.Closing;
				tchannel = this.synchronizer.StopSynchronizing(true);
				if (!this.MustCloseChannel)
				{
					tchannel = default(TChannel);
				}
			}
			this.synchronizer.UnblockWaiters();
			this.OnShutdown();
			this.OnAbort();
			if (tchannel != null)
			{
				tchannel.Abort();
			}
			this.TransitionToClosed();
		}

		// Token: 0x060058CC RID: 22732 RVA: 0x001451F8 File Offset: 0x001433F8
		protected virtual void AddOutputHeaders(Message message)
		{
		}

		// Token: 0x060058CD RID: 22733 RVA: 0x001451FA File Offset: 0x001433FA
		public IAsyncResult BeginClose(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return this.BeginClose(timeout, this.defaultMaskingMode, callback, state);
		}

		// Token: 0x060058CE RID: 22734 RVA: 0x0014520C File Offset: 0x0014340C
		public IAsyncResult BeginClose(TimeSpan timeout, MaskingMode maskingMode, AsyncCallback callback, object state)
		{
			this.ThrowIfTimeoutNegative(timeout);
			TChannel channel;
			if (this.CloseCore(out channel))
			{
				return new CompletedAsyncResult(callback, state);
			}
			return new ReliableChannelBinder<TChannel>.CloseAsyncResult(this, channel, timeout, maskingMode, callback, state);
		}

		// Token: 0x060058CF RID: 22735 RVA: 0x0014523F File Offset: 0x0014343F
		protected virtual IAsyncResult BeginCloseChannel(TChannel channel, TimeSpan timeout, AsyncCallback callback, object state)
		{
			return channel.BeginClose(timeout, callback, state);
		}

		// Token: 0x060058D0 RID: 22736 RVA: 0x00145250 File Offset: 0x00143450
		public IAsyncResult BeginOpen(TimeSpan timeout, AsyncCallback callback, object state)
		{
			this.ThrowIfTimeoutNegative(timeout);
			if (this.OnOpening(this.defaultMaskingMode))
			{
				try
				{
					return this.OnBeginOpen(timeout, callback, state);
				}
				catch (Exception ex)
				{
					if (Fx.IsFatal(ex))
					{
						throw;
					}
					this.Fault(null);
					if (this.defaultMaskingMode == MaskingMode.None)
					{
						throw;
					}
					this.RaiseOnException(ex);
				}
			}
			return new ReliableChannelBinder<TChannel>.BinderCompletedAsyncResult(callback, state);
		}

		// Token: 0x060058D1 RID: 22737 RVA: 0x001452BC File Offset: 0x001434BC
		public IAsyncResult BeginSend(Message message, TimeSpan timeout, AsyncCallback callback, object state)
		{
			return this.BeginSend(message, timeout, this.defaultMaskingMode, callback, state);
		}

		// Token: 0x060058D2 RID: 22738 RVA: 0x001452D0 File Offset: 0x001434D0
		public IAsyncResult BeginSend(Message message, TimeSpan timeout, MaskingMode maskingMode, AsyncCallback callback, object state)
		{
			ReliableChannelBinder<TChannel>.SendAsyncResult sendAsyncResult = new ReliableChannelBinder<TChannel>.SendAsyncResult(this, callback, state);
			sendAsyncResult.Start(message, timeout, maskingMode);
			return sendAsyncResult;
		}

		// Token: 0x060058D3 RID: 22739
		protected abstract IAsyncResult BeginTryGetChannel(TimeSpan timeout, AsyncCallback callback, object state);

		// Token: 0x060058D4 RID: 22740 RVA: 0x001452F2 File Offset: 0x001434F2
		public virtual IAsyncResult BeginTryReceive(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return this.BeginTryReceive(timeout, this.defaultMaskingMode, callback, state);
		}

		// Token: 0x060058D5 RID: 22741 RVA: 0x00145303 File Offset: 0x00143503
		public virtual IAsyncResult BeginTryReceive(TimeSpan timeout, MaskingMode maskingMode, AsyncCallback callback, object state)
		{
			if (this.ValidateInputOperation(timeout))
			{
				return new ReliableChannelBinder<TChannel>.TryReceiveAsyncResult(this, timeout, maskingMode, callback, state);
			}
			return new CompletedAsyncResult(callback, state);
		}

		// Token: 0x060058D6 RID: 22742 RVA: 0x00145322 File Offset: 0x00143522
		internal IAsyncResult BeginWaitForPendingOperations(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return this.synchronizer.BeginWaitForPendingOperations(timeout, callback, state);
		}

		// Token: 0x060058D7 RID: 22743 RVA: 0x00145334 File Offset: 0x00143534
		private bool CloseCore(out TChannel channel)
		{
			channel = default(TChannel);
			bool flag = true;
			bool flag2 = false;
			object obj = this.ThisLock;
			lock (obj)
			{
				if (this.state == CommunicationState.Closing || this.state == CommunicationState.Closed)
				{
					return true;
				}
				if (this.state == CommunicationState.Opened)
				{
					this.state = CommunicationState.Closing;
					channel = this.synchronizer.StopSynchronizing(true);
					flag = false;
					if (!this.MustCloseChannel)
					{
						channel = default(TChannel);
					}
					if (channel != null)
					{
						CommunicationState communicationState = channel.State;
						if (communicationState == CommunicationState.Created || communicationState == CommunicationState.Opening || communicationState == CommunicationState.Faulted)
						{
							flag2 = true;
						}
						else if (communicationState == CommunicationState.Closing || communicationState == CommunicationState.Closed)
						{
							channel = default(TChannel);
						}
					}
				}
			}
			this.synchronizer.UnblockWaiters();
			if (flag)
			{
				this.Abort();
				return true;
			}
			if (flag2)
			{
				channel.Abort();
				channel = default(TChannel);
			}
			return false;
		}

		// Token: 0x060058D8 RID: 22744 RVA: 0x00145434 File Offset: 0x00143634
		public void Close(TimeSpan timeout)
		{
			this.Close(timeout, this.defaultMaskingMode);
		}

		// Token: 0x060058D9 RID: 22745 RVA: 0x00145444 File Offset: 0x00143644
		public void Close(TimeSpan timeout, MaskingMode maskingMode)
		{
			this.ThrowIfTimeoutNegative(timeout);
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			TChannel tchannel;
			if (this.CloseCore(out tchannel))
			{
				return;
			}
			try
			{
				this.OnShutdown();
				this.OnClose(timeoutHelper.RemainingTime());
				if (tchannel != null)
				{
					this.CloseChannel(tchannel, timeoutHelper.RemainingTime());
				}
				this.TransitionToClosed();
			}
			catch (Exception ex)
			{
				if (Fx.IsFatal(ex))
				{
					throw;
				}
				this.Abort();
				if (!this.HandleException(ex, maskingMode))
				{
					throw;
				}
			}
		}

		// Token: 0x060058DA RID: 22746 RVA: 0x001454CC File Offset: 0x001436CC
		private void CloseChannel(TChannel channel)
		{
			if (!this.MustCloseChannel)
			{
				throw Fx.AssertAndThrow("MustCloseChannel is false when there is no receive loop and this method is called when there is a receive loop.");
			}
			if (this.onCloseChannelComplete == null)
			{
				this.onCloseChannelComplete = Fx.ThunkCallback(new AsyncCallback(this.OnCloseChannelComplete));
			}
			try
			{
				IAsyncResult asyncResult = channel.BeginClose(this.onCloseChannelComplete, channel);
				if (asyncResult.CompletedSynchronously)
				{
					channel.EndClose(asyncResult);
				}
			}
			catch (Exception ex)
			{
				if (Fx.IsFatal(ex))
				{
					throw;
				}
				this.HandleException(ex, MaskingMode.All);
			}
		}

		// Token: 0x060058DB RID: 22747 RVA: 0x00145560 File Offset: 0x00143760
		protected virtual void CloseChannel(TChannel channel, TimeSpan timeout)
		{
			channel.Close(timeout);
		}

		// Token: 0x060058DC RID: 22748 RVA: 0x00145570 File Offset: 0x00143770
		public void EndClose(IAsyncResult result)
		{
			ReliableChannelBinder<TChannel>.CloseAsyncResult closeAsyncResult = result as ReliableChannelBinder<TChannel>.CloseAsyncResult;
			if (closeAsyncResult != null)
			{
				closeAsyncResult.End();
				return;
			}
			CompletedAsyncResult.End(result);
		}

		// Token: 0x060058DD RID: 22749 RVA: 0x00145594 File Offset: 0x00143794
		protected virtual void EndCloseChannel(TChannel channel, IAsyncResult result)
		{
			channel.EndClose(result);
		}

		// Token: 0x060058DE RID: 22750 RVA: 0x001455A4 File Offset: 0x001437A4
		public void EndOpen(IAsyncResult result)
		{
			ReliableChannelBinder<TChannel>.BinderCompletedAsyncResult binderCompletedAsyncResult = result as ReliableChannelBinder<TChannel>.BinderCompletedAsyncResult;
			if (binderCompletedAsyncResult != null)
			{
				binderCompletedAsyncResult.End();
				return;
			}
			try
			{
				this.OnEndOpen(result);
			}
			catch (Exception ex)
			{
				if (Fx.IsFatal(ex))
				{
					throw;
				}
				this.Fault(null);
				if (this.defaultMaskingMode == MaskingMode.None)
				{
					throw;
				}
				this.RaiseOnException(ex);
				return;
			}
			this.synchronizer.StartSynchronizing();
			this.OnOpened();
		}

		// Token: 0x060058DF RID: 22751 RVA: 0x00145614 File Offset: 0x00143814
		public void EndSend(IAsyncResult result)
		{
			ReliableChannelBinder<TChannel>.SendAsyncResult.End(result);
		}

		// Token: 0x060058E0 RID: 22752
		protected abstract bool EndTryGetChannel(IAsyncResult result);

		// Token: 0x060058E1 RID: 22753 RVA: 0x0014561C File Offset: 0x0014381C
		public virtual bool EndTryReceive(IAsyncResult result, out RequestContext requestContext)
		{
			ReliableChannelBinder<TChannel>.TryReceiveAsyncResult tryReceiveAsyncResult = result as ReliableChannelBinder<TChannel>.TryReceiveAsyncResult;
			if (tryReceiveAsyncResult != null)
			{
				return tryReceiveAsyncResult.End(out requestContext);
			}
			CompletedAsyncResult.End(result);
			requestContext = null;
			return true;
		}

		// Token: 0x060058E2 RID: 22754 RVA: 0x00145645 File Offset: 0x00143845
		public void EndWaitForPendingOperations(IAsyncResult result)
		{
			this.synchronizer.EndWaitForPendingOperations(result);
		}

		// Token: 0x060058E3 RID: 22755 RVA: 0x00145654 File Offset: 0x00143854
		protected void Fault(Exception e)
		{
			object obj = this.ThisLock;
			lock (obj)
			{
				if (this.state == CommunicationState.Created)
				{
					throw Fx.AssertAndThrow("The binder should not detect the inner channel's faults until after the binder is opened.");
				}
				if (this.state == CommunicationState.Faulted || this.state == CommunicationState.Closed)
				{
					return;
				}
				this.state = CommunicationState.Faulted;
				this.synchronizer.StopSynchronizing(false);
			}
			this.synchronizer.UnblockWaiters();
			BinderExceptionHandler faulted = this.Faulted;
			if (faulted != null)
			{
				faulted(this, e);
			}
		}

		// Token: 0x060058E4 RID: 22756 RVA: 0x001456E8 File Offset: 0x001438E8
		private Exception GetClosedException(MaskingMode maskingMode)
		{
			if (ReliableChannelBinderHelper.MaskHandled(maskingMode))
			{
				return null;
			}
			if (this.aborted)
			{
				return new CommunicationObjectAbortedException(SR.GetString("CommunicationObjectAborted1", new object[]
				{
					base.GetType().ToString()
				}));
			}
			return new ObjectDisposedException(base.GetType().ToString());
		}

		// Token: 0x060058E5 RID: 22757 RVA: 0x0014573B File Offset: 0x0014393B
		private Exception GetClosedOrFaultedException(MaskingMode maskingMode)
		{
			if (this.state == CommunicationState.Faulted)
			{
				return this.GetFaultedException(maskingMode);
			}
			if (this.state == CommunicationState.Closing || this.state == CommunicationState.Closed)
			{
				return this.GetClosedException(maskingMode);
			}
			throw Fx.AssertAndThrow("Caller is attempting to get a terminal exception in a non-terminal state.");
		}

		// Token: 0x060058E6 RID: 22758 RVA: 0x00145772 File Offset: 0x00143972
		private Exception GetFaultedException(MaskingMode maskingMode)
		{
			if (ReliableChannelBinderHelper.MaskHandled(maskingMode))
			{
				return null;
			}
			return new CommunicationObjectFaultedException(SR.GetString("CommunicationObjectFaulted1", new object[]
			{
				base.GetType().ToString()
			}));
		}

		// Token: 0x060058E7 RID: 22759
		public abstract ISession GetInnerSession();

		// Token: 0x060058E8 RID: 22760 RVA: 0x001457A1 File Offset: 0x001439A1
		public void HandleException(Exception e)
		{
			this.HandleException(e, MaskingMode.All);
		}

		// Token: 0x060058E9 RID: 22761 RVA: 0x001457AC File Offset: 0x001439AC
		protected bool HandleException(Exception e, MaskingMode maskingMode)
		{
			if (this.TolerateFaults && e is CommunicationObjectFaultedException)
			{
				return true;
			}
			if (this.IsHandleable(e))
			{
				return ReliableChannelBinderHelper.MaskHandled(maskingMode);
			}
			bool flag = ReliableChannelBinderHelper.MaskUnhandled(maskingMode);
			if (flag)
			{
				this.RaiseOnException(e);
			}
			return flag;
		}

		// Token: 0x060058EA RID: 22762 RVA: 0x001457ED File Offset: 0x001439ED
		protected bool HandleException(Exception e, MaskingMode maskingMode, bool autoAborted)
		{
			return (this.TolerateFaults && autoAborted && e is CommunicationObjectAbortedException) || this.HandleException(e, maskingMode);
		}

		// Token: 0x060058EB RID: 22763
		protected abstract bool HasSecuritySession(TChannel channel);

		// Token: 0x060058EC RID: 22764 RVA: 0x0014580B File Offset: 0x00143A0B
		public bool IsHandleable(Exception e)
		{
			return !(e is ProtocolException) && (e is CommunicationException || e is TimeoutException);
		}

		// Token: 0x060058ED RID: 22765
		protected abstract void OnAbort();

		// Token: 0x060058EE RID: 22766
		protected abstract IAsyncResult OnBeginClose(TimeSpan timeout, AsyncCallback callback, object state);

		// Token: 0x060058EF RID: 22767
		protected abstract IAsyncResult OnBeginOpen(TimeSpan timeout, AsyncCallback callback, object state);

		// Token: 0x060058F0 RID: 22768 RVA: 0x0014582A File Offset: 0x00143A2A
		protected virtual IAsyncResult OnBeginSend(TChannel channel, Message message, TimeSpan timeout, AsyncCallback callback, object state)
		{
			throw Fx.AssertAndThrow("The derived class does not support the BeginSend operation.");
		}

		// Token: 0x060058F1 RID: 22769 RVA: 0x00145836 File Offset: 0x00143A36
		protected virtual IAsyncResult OnBeginTryReceive(TChannel channel, TimeSpan timeout, AsyncCallback callback, object state)
		{
			throw Fx.AssertAndThrow("The derived class does not support the BeginTryReceive operation.");
		}

		// Token: 0x060058F2 RID: 22770
		protected abstract void OnClose(TimeSpan timeout);

		// Token: 0x060058F3 RID: 22771 RVA: 0x00145844 File Offset: 0x00143A44
		private void OnCloseChannelComplete(IAsyncResult result)
		{
			if (result.CompletedSynchronously)
			{
				return;
			}
			TChannel tchannel = (TChannel)((object)result.AsyncState);
			try
			{
				tchannel.EndClose(result);
			}
			catch (Exception ex)
			{
				if (Fx.IsFatal(ex))
				{
					throw;
				}
				this.HandleException(ex, MaskingMode.All);
			}
		}

		// Token: 0x060058F4 RID: 22772
		protected abstract void OnEndClose(IAsyncResult result);

		// Token: 0x060058F5 RID: 22773
		protected abstract void OnEndOpen(IAsyncResult result);

		// Token: 0x060058F6 RID: 22774 RVA: 0x0014589C File Offset: 0x00143A9C
		protected virtual void OnEndSend(TChannel channel, IAsyncResult result)
		{
			throw Fx.AssertAndThrow("The derived class does not support the EndSend operation.");
		}

		// Token: 0x060058F7 RID: 22775 RVA: 0x001458A8 File Offset: 0x00143AA8
		protected virtual bool OnEndTryReceive(TChannel channel, IAsyncResult result, out RequestContext requestContext)
		{
			throw Fx.AssertAndThrow("The derived class does not support the EndTryReceive operation.");
		}

		// Token: 0x060058F8 RID: 22776 RVA: 0x001458B4 File Offset: 0x00143AB4
		private void OnInnerChannelFaulted()
		{
			if (!this.TolerateFaults)
			{
				return;
			}
			EventHandler connectionLost = this.ConnectionLost;
			if (connectionLost != null)
			{
				connectionLost(this, EventArgs.Empty);
			}
		}

		// Token: 0x060058F9 RID: 22777
		protected abstract void OnOpen(TimeSpan timeout);

		// Token: 0x060058FA RID: 22778 RVA: 0x001458E0 File Offset: 0x00143AE0
		private void OnOpened()
		{
			object obj = this.ThisLock;
			lock (obj)
			{
				if (this.state == CommunicationState.Opening)
				{
					this.state = CommunicationState.Opened;
				}
			}
		}

		// Token: 0x060058FB RID: 22779 RVA: 0x0014592C File Offset: 0x00143B2C
		private bool OnOpening(MaskingMode maskingMode)
		{
			object obj = this.ThisLock;
			bool result;
			lock (obj)
			{
				if (this.state != CommunicationState.Created)
				{
					Exception ex = null;
					if (this.state == CommunicationState.Opening || this.state == CommunicationState.Opened)
					{
						if (!ReliableChannelBinderHelper.MaskUnhandled(maskingMode))
						{
							ex = new InvalidOperationException(SR.GetString("CommunicationObjectCannotBeModifiedInState", new object[]
							{
								base.GetType().ToString(),
								this.state.ToString()
							}));
						}
					}
					else
					{
						ex = this.GetClosedOrFaultedException(maskingMode);
					}
					if (ex != null)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(ex);
					}
					result = false;
				}
				else
				{
					this.state = CommunicationState.Opening;
					result = true;
				}
			}
			return result;
		}

		// Token: 0x060058FC RID: 22780 RVA: 0x001459EC File Offset: 0x00143BEC
		protected virtual void OnShutdown()
		{
		}

		// Token: 0x060058FD RID: 22781 RVA: 0x001459EE File Offset: 0x00143BEE
		protected virtual void OnSend(TChannel channel, Message message, TimeSpan timeout)
		{
			throw Fx.AssertAndThrow("The derived class does not support the Send operation.");
		}

		// Token: 0x060058FE RID: 22782 RVA: 0x001459FA File Offset: 0x00143BFA
		protected virtual bool OnTryReceive(TChannel channel, TimeSpan timeout, out RequestContext requestContext)
		{
			throw Fx.AssertAndThrow("The derived class does not support the TryReceive operation.");
		}

		// Token: 0x060058FF RID: 22783 RVA: 0x00145A08 File Offset: 0x00143C08
		public void Open(TimeSpan timeout)
		{
			this.ThrowIfTimeoutNegative(timeout);
			if (!this.OnOpening(this.defaultMaskingMode))
			{
				return;
			}
			try
			{
				this.OnOpen(timeout);
			}
			catch (Exception ex)
			{
				if (Fx.IsFatal(ex))
				{
					throw;
				}
				this.Fault(null);
				if (this.defaultMaskingMode == MaskingMode.None)
				{
					throw;
				}
				this.RaiseOnException(ex);
				return;
			}
			this.synchronizer.StartSynchronizing();
			this.OnOpened();
		}

		// Token: 0x06005900 RID: 22784 RVA: 0x00145A7C File Offset: 0x00143C7C
		private void RaiseOnException(Exception e)
		{
			BinderExceptionHandler onException = this.OnException;
			if (onException != null)
			{
				onException(this, e);
			}
		}

		// Token: 0x06005901 RID: 22785 RVA: 0x00145A9B File Offset: 0x00143C9B
		public void Send(Message message, TimeSpan timeout)
		{
			this.Send(message, timeout, this.defaultMaskingMode);
		}

		// Token: 0x06005902 RID: 22786 RVA: 0x00145AAC File Offset: 0x00143CAC
		public void Send(Message message, TimeSpan timeout, MaskingMode maskingMode)
		{
			if (!this.ValidateOutputOperation(message, timeout, maskingMode))
			{
				return;
			}
			bool autoAborted = false;
			try
			{
				TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
				TChannel tchannel;
				if (!this.synchronizer.TryGetChannelForOutput(timeoutHelper.RemainingTime(), maskingMode, out tchannel))
				{
					if (!ReliableChannelBinderHelper.MaskHandled(maskingMode))
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new TimeoutException(SR.GetString("TimeoutOnSend", new object[]
						{
							timeout
						})));
					}
				}
				else if (tchannel != null)
				{
					this.AddOutputHeaders(message);
					try
					{
						this.OnSend(tchannel, message, timeoutHelper.RemainingTime());
					}
					finally
					{
						autoAborted = this.Synchronizer.Aborting;
						this.synchronizer.ReturnChannel();
					}
				}
			}
			catch (Exception ex)
			{
				if (Fx.IsFatal(ex))
				{
					throw;
				}
				if (!this.HandleException(ex, maskingMode, autoAborted))
				{
					throw;
				}
			}
		}

		// Token: 0x06005903 RID: 22787 RVA: 0x00145B8C File Offset: 0x00143D8C
		public void SetMaskingMode(RequestContext context, MaskingMode maskingMode)
		{
			ReliableChannelBinder<TChannel>.BinderRequestContext binderRequestContext = (ReliableChannelBinder<TChannel>.BinderRequestContext)context;
			binderRequestContext.SetMaskingMode(maskingMode);
		}

		// Token: 0x06005904 RID: 22788 RVA: 0x00145BA8 File Offset: 0x00143DA8
		private bool ThrowIfNotOpenedAndNotMasking(MaskingMode maskingMode, bool throwDisposed)
		{
			object obj = this.ThisLock;
			bool result;
			lock (obj)
			{
				if (this.State == CommunicationState.Created)
				{
					throw Fx.AssertAndThrow("Messaging operations cannot be called when the binder is in the Created state.");
				}
				if (this.State == CommunicationState.Opening)
				{
					throw Fx.AssertAndThrow("Messaging operations cannot be called when the binder is in the Opening state.");
				}
				if (this.State == CommunicationState.Opened)
				{
					result = true;
				}
				else
				{
					if (throwDisposed)
					{
						Exception closedOrFaultedException = this.GetClosedOrFaultedException(maskingMode);
						if (closedOrFaultedException != null)
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(closedOrFaultedException);
						}
					}
					result = false;
				}
			}
			return result;
		}

		// Token: 0x06005905 RID: 22789 RVA: 0x00145C34 File Offset: 0x00143E34
		private void ThrowIfTimeoutNegative(TimeSpan timeout)
		{
			if (timeout < TimeSpan.Zero)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("timeout", timeout, "SFxTimeoutOutOfRange0"));
			}
		}

		// Token: 0x06005906 RID: 22790 RVA: 0x00145C64 File Offset: 0x00143E64
		private void TransitionToClosed()
		{
			object obj = this.ThisLock;
			lock (obj)
			{
				if (this.state != CommunicationState.Closing && this.state != CommunicationState.Closed && this.state != CommunicationState.Faulted)
				{
					throw Fx.AssertAndThrow("Caller cannot transition to the Closed state from a non-terminal state.");
				}
				this.state = CommunicationState.Closed;
			}
		}

		// Token: 0x06005907 RID: 22791
		protected abstract bool TryGetChannel(TimeSpan timeout);

		// Token: 0x06005908 RID: 22792 RVA: 0x00145CCC File Offset: 0x00143ECC
		public virtual bool TryReceive(TimeSpan timeout, out RequestContext requestContext)
		{
			return this.TryReceive(timeout, out requestContext, this.defaultMaskingMode);
		}

		// Token: 0x06005909 RID: 22793 RVA: 0x00145CDC File Offset: 0x00143EDC
		public virtual bool TryReceive(TimeSpan timeout, out RequestContext requestContext, MaskingMode maskingMode)
		{
			if (maskingMode != MaskingMode.None)
			{
				throw Fx.AssertAndThrow("This method was implemented only for the case where we do not mask exceptions.");
			}
			if (!this.ValidateInputOperation(timeout))
			{
				requestContext = null;
				return true;
			}
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			bool result;
			for (;;)
			{
				bool autoAborted = false;
				try
				{
					TChannel tchannel;
					bool flag = !this.synchronizer.TryGetChannelForInput(this.CanGetChannelForReceive, timeoutHelper.RemainingTime(), out tchannel);
					if (tchannel != null)
					{
						try
						{
							flag = this.OnTryReceive(tchannel, timeoutHelper.RemainingTime(), out requestContext);
							if (!flag || requestContext != null)
							{
								result = flag;
								break;
							}
							this.synchronizer.OnReadEof();
						}
						finally
						{
							autoAborted = this.Synchronizer.Aborting;
							this.synchronizer.ReturnChannel();
						}
						continue;
					}
					requestContext = null;
					result = flag;
				}
				catch (Exception ex)
				{
					if (Fx.IsFatal(ex))
					{
						throw;
					}
					if (!this.HandleException(ex, maskingMode, autoAborted))
					{
						throw;
					}
					continue;
				}
				break;
			}
			return result;
		}

		// Token: 0x0600590A RID: 22794 RVA: 0x00145DC0 File Offset: 0x00143FC0
		protected bool ValidateInputOperation(TimeSpan timeout)
		{
			if (timeout < TimeSpan.Zero)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("timeout", timeout, "SFxTimeoutOutOfRange0"));
			}
			return this.ThrowIfNotOpenedAndNotMasking(MaskingMode.All, false);
		}

		// Token: 0x0600590B RID: 22795 RVA: 0x00145DF8 File Offset: 0x00143FF8
		protected bool ValidateOutputOperation(Message message, TimeSpan timeout, MaskingMode maskingMode)
		{
			if (message == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("message");
			}
			if (timeout < TimeSpan.Zero)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("timeout", timeout, "SFxTimeoutOutOfRange0"));
			}
			return this.ThrowIfNotOpenedAndNotMasking(maskingMode, true);
		}

		// Token: 0x0600590C RID: 22796 RVA: 0x00145E4D File Offset: 0x0014404D
		internal void WaitForPendingOperations(TimeSpan timeout)
		{
			this.synchronizer.WaitForPendingOperations(timeout);
		}

		// Token: 0x0600590D RID: 22797 RVA: 0x00145E5B File Offset: 0x0014405B
		protected RequestContext WrapMessage(Message message)
		{
			if (message == null)
			{
				return null;
			}
			return new ReliableChannelBinder<TChannel>.MessageRequestContext(this, message);
		}

		// Token: 0x0600590E RID: 22798 RVA: 0x00145E69 File Offset: 0x00144069
		public RequestContext WrapRequestContext(RequestContext context)
		{
			if (context == null)
			{
				return null;
			}
			if (!this.TolerateFaults && this.defaultMaskingMode == MaskingMode.None)
			{
				return context;
			}
			return new ReliableChannelBinder<TChannel>.RequestRequestContext(this, context, context.RequestMessage);
		}

		// Token: 0x0400363E RID: 13886
		private bool aborted;

		// Token: 0x0400363F RID: 13887
		private TimeSpan defaultCloseTimeout;

		// Token: 0x04003640 RID: 13888
		private MaskingMode defaultMaskingMode;

		// Token: 0x04003641 RID: 13889
		private TimeSpan defaultSendTimeout;

		// Token: 0x04003642 RID: 13890
		private AsyncCallback onCloseChannelComplete;

		// Token: 0x04003643 RID: 13891
		private CommunicationState state;

		// Token: 0x04003644 RID: 13892
		private ReliableChannelBinder<TChannel>.ChannelSynchronizer synchronizer;

		// Token: 0x04003645 RID: 13893
		private object thisLock = new object();

		// Token: 0x02000DB7 RID: 3511
		private sealed class BinderCompletedAsyncResult : CompletedAsyncResult
		{
			// Token: 0x06007F61 RID: 32609 RVA: 0x001D9769 File Offset: 0x001D7969
			public BinderCompletedAsyncResult(AsyncCallback callback, object state) : base(callback, state)
			{
			}

			// Token: 0x06007F62 RID: 32610 RVA: 0x001D9773 File Offset: 0x001D7973
			public void End()
			{
				CompletedAsyncResult.End(this);
			}
		}

		// Token: 0x02000DB8 RID: 3512
		private abstract class BinderRequestContext : RequestContextBase
		{
			// Token: 0x06007F63 RID: 32611 RVA: 0x001D977B File Offset: 0x001D797B
			public BinderRequestContext(ReliableChannelBinder<TChannel> binder, Message message) : base(message, binder.defaultCloseTimeout, binder.defaultSendTimeout)
			{
				this.binder = binder;
				this.maskingMode = binder.defaultMaskingMode;
			}

			// Token: 0x17001C5F RID: 7263
			// (get) Token: 0x06007F64 RID: 32612 RVA: 0x001D97A5 File Offset: 0x001D79A5
			protected ReliableChannelBinder<TChannel> Binder
			{
				get
				{
					return this.binder;
				}
			}

			// Token: 0x17001C60 RID: 7264
			// (get) Token: 0x06007F65 RID: 32613 RVA: 0x001D97AD File Offset: 0x001D79AD
			protected MaskingMode MaskingMode
			{
				get
				{
					return this.maskingMode;
				}
			}

			// Token: 0x06007F66 RID: 32614 RVA: 0x001D97B5 File Offset: 0x001D79B5
			public void SetMaskingMode(MaskingMode maskingMode)
			{
				if (this.binder.defaultMaskingMode != MaskingMode.All)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException());
				}
				this.maskingMode = maskingMode;
			}

			// Token: 0x040048E4 RID: 18660
			private ReliableChannelBinder<TChannel> binder;

			// Token: 0x040048E5 RID: 18661
			private MaskingMode maskingMode;
		}

		// Token: 0x02000DB9 RID: 3513
		protected class ChannelSynchronizer
		{
			// Token: 0x06007F67 RID: 32615 RVA: 0x001D97DC File Offset: 0x001D79DC
			public ChannelSynchronizer(ReliableChannelBinder<TChannel> binder, TChannel channel, TolerateFaultsMode faultMode)
			{
				this.binder = binder;
				this.currentChannel = channel;
				this.faultMode = faultMode;
			}

			// Token: 0x17001C61 RID: 7265
			// (get) Token: 0x06007F68 RID: 32616 RVA: 0x001D980B File Offset: 0x001D7A0B
			public bool Aborting
			{
				get
				{
					return this.aborting;
				}
			}

			// Token: 0x17001C62 RID: 7266
			// (get) Token: 0x06007F69 RID: 32617 RVA: 0x001D9813 File Offset: 0x001D7A13
			public bool Connected
			{
				get
				{
					return this.state == ReliableChannelBinder<TChannel>.ChannelSynchronizer.State.ChannelOpened || this.state == ReliableChannelBinder<TChannel>.ChannelSynchronizer.State.ChannelOpening;
				}
			}

			// Token: 0x17001C63 RID: 7267
			// (get) Token: 0x06007F6A RID: 32618 RVA: 0x001D9829 File Offset: 0x001D7A29
			public TChannel CurrentChannel
			{
				get
				{
					return this.currentChannel;
				}
			}

			// Token: 0x17001C64 RID: 7268
			// (get) Token: 0x06007F6B RID: 32619 RVA: 0x001D9831 File Offset: 0x001D7A31
			private object ThisLock
			{
				get
				{
					return this.thisLock;
				}
			}

			// Token: 0x17001C65 RID: 7269
			// (get) Token: 0x06007F6C RID: 32620 RVA: 0x001D9839 File Offset: 0x001D7A39
			public bool TolerateFaults
			{
				get
				{
					return this.tolerateFaults;
				}
			}

			// Token: 0x06007F6D RID: 32621 RVA: 0x001D9844 File Offset: 0x001D7A44
			public TChannel AbortCurentChannel()
			{
				object obj = this.ThisLock;
				TChannel result;
				lock (obj)
				{
					if (!this.tolerateFaults)
					{
						throw Fx.AssertAndThrow("It is only valid to abort the current channel when masking faults");
					}
					if (this.state == ReliableChannelBinder<TChannel>.ChannelSynchronizer.State.ChannelOpening)
					{
						this.aborting = true;
					}
					else
					{
						if (this.state != ReliableChannelBinder<TChannel>.ChannelSynchronizer.State.ChannelOpened)
						{
							result = default(TChannel);
							return result;
						}
						if (this.count == 0)
						{
							this.state = ReliableChannelBinder<TChannel>.ChannelSynchronizer.State.NoChannel;
						}
						else
						{
							this.aborting = true;
							this.state = ReliableChannelBinder<TChannel>.ChannelSynchronizer.State.ChannelClosing;
						}
					}
					result = this.currentChannel;
				}
				return result;
			}

			// Token: 0x06007F6E RID: 32622 RVA: 0x001D98E4 File Offset: 0x001D7AE4
			private static void AsyncGetChannelCallback(object state)
			{
				ReliableChannelBinder<TChannel>.ChannelSynchronizer.AsyncWaiter asyncWaiter = (ReliableChannelBinder<TChannel>.ChannelSynchronizer.AsyncWaiter)state;
				asyncWaiter.GetChannel(false);
			}

			// Token: 0x06007F6F RID: 32623 RVA: 0x001D98FF File Offset: 0x001D7AFF
			public IAsyncResult BeginTryGetChannelForInput(bool canGetChannel, TimeSpan timeout, AsyncCallback callback, object state)
			{
				return this.BeginTryGetChannel(canGetChannel, false, timeout, MaskingMode.All, callback, state);
			}

			// Token: 0x06007F70 RID: 32624 RVA: 0x001D990E File Offset: 0x001D7B0E
			public IAsyncResult BeginTryGetChannelForOutput(TimeSpan timeout, MaskingMode maskingMode, AsyncCallback callback, object state)
			{
				return this.BeginTryGetChannel(true, true, timeout, maskingMode, callback, state);
			}

			// Token: 0x06007F71 RID: 32625 RVA: 0x001D9920 File Offset: 0x001D7B20
			private IAsyncResult BeginTryGetChannel(bool canGetChannel, bool canCauseFault, TimeSpan timeout, MaskingMode maskingMode, AsyncCallback callback, object state)
			{
				TChannel data = default(TChannel);
				ReliableChannelBinder<TChannel>.ChannelSynchronizer.AsyncWaiter asyncWaiter = null;
				bool flag = false;
				bool flag2 = false;
				object obj = this.ThisLock;
				lock (obj)
				{
					if (!this.ThrowIfNecessary(maskingMode))
					{
						data = default(TChannel);
					}
					else if (this.state == ReliableChannelBinder<TChannel>.ChannelSynchronizer.State.ChannelOpened)
					{
						if (this.currentChannel == null)
						{
							throw Fx.AssertAndThrow("Field currentChannel cannot be null in the ChannelOpened state.");
						}
						this.count++;
						data = this.currentChannel;
					}
					else if (!this.tolerateFaults && (this.state == ReliableChannelBinder<TChannel>.ChannelSynchronizer.State.NoChannel || this.state == ReliableChannelBinder<TChannel>.ChannelSynchronizer.State.ChannelClosing))
					{
						if (canCauseFault)
						{
							flag2 = true;
						}
						data = default(TChannel);
					}
					else if (!canGetChannel || this.state == ReliableChannelBinder<TChannel>.ChannelSynchronizer.State.ChannelOpening || this.state == ReliableChannelBinder<TChannel>.ChannelSynchronizer.State.ChannelClosing)
					{
						asyncWaiter = new ReliableChannelBinder<TChannel>.ChannelSynchronizer.AsyncWaiter(this, canGetChannel, default(TChannel), timeout, maskingMode, this.binder.ChannelParameters, callback, state);
						this.GetQueue(canGetChannel).Enqueue(asyncWaiter);
					}
					else
					{
						if (this.state != ReliableChannelBinder<TChannel>.ChannelSynchronizer.State.NoChannel)
						{
							throw Fx.AssertAndThrow("The state must be NoChannel.");
						}
						asyncWaiter = new ReliableChannelBinder<TChannel>.ChannelSynchronizer.AsyncWaiter(this, canGetChannel, this.GetCurrentChannelIfCreated(), timeout, maskingMode, this.binder.ChannelParameters, callback, state);
						this.state = ReliableChannelBinder<TChannel>.ChannelSynchronizer.State.ChannelOpening;
						flag = true;
					}
				}
				if (flag2)
				{
					this.binder.Fault(null);
				}
				if (asyncWaiter == null)
				{
					return new CompletedAsyncResult<TChannel>(data, callback, state);
				}
				if (flag)
				{
					asyncWaiter.GetChannel(true);
				}
				else
				{
					asyncWaiter.Wait();
				}
				return asyncWaiter;
			}

			// Token: 0x06007F72 RID: 32626 RVA: 0x001D9AAC File Offset: 0x001D7CAC
			public IAsyncResult BeginWaitForPendingOperations(TimeSpan timeout, AsyncCallback callback, object state)
			{
				object obj = this.ThisLock;
				lock (obj)
				{
					if (this.drainEvent != null)
					{
						throw Fx.AssertAndThrow("The WaitForPendingOperations operation may only be invoked once.");
					}
					if (this.count > 0)
					{
						this.drainEvent = new InterruptibleWaitObject(false, false);
					}
				}
				if (this.drainEvent != null)
				{
					return this.drainEvent.BeginWait(timeout, callback, state);
				}
				return new ReliableChannelBinder<TChannel>.ChannelSynchronizer.SynchronizerCompletedAsyncResult(callback, state);
			}

			// Token: 0x06007F73 RID: 32627 RVA: 0x001D9B30 File Offset: 0x001D7D30
			private bool CompleteSetChannel(ReliableChannelBinder<TChannel>.ChannelSynchronizer.IWaiter waiter, out TChannel channel)
			{
				if (waiter == null)
				{
					throw Fx.AssertAndThrow("Argument waiter cannot be null.");
				}
				bool flag = false;
				object obj = this.ThisLock;
				lock (obj)
				{
					if (this.ValidateOpened())
					{
						channel = this.currentChannel;
						return true;
					}
					channel = default(TChannel);
					flag = (this.state == ReliableChannelBinder<TChannel>.ChannelSynchronizer.State.Closed);
				}
				if (flag)
				{
					waiter.Close();
				}
				else
				{
					waiter.Fault();
				}
				return false;
			}

			// Token: 0x06007F74 RID: 32628 RVA: 0x001D9BB8 File Offset: 0x001D7DB8
			public bool EndTryGetChannel(IAsyncResult result, out TChannel channel)
			{
				ReliableChannelBinder<TChannel>.ChannelSynchronizer.AsyncWaiter asyncWaiter = result as ReliableChannelBinder<TChannel>.ChannelSynchronizer.AsyncWaiter;
				if (asyncWaiter != null)
				{
					return asyncWaiter.End(out channel);
				}
				channel = CompletedAsyncResult<TChannel>.End(result);
				return true;
			}

			// Token: 0x06007F75 RID: 32629 RVA: 0x001D9BE4 File Offset: 0x001D7DE4
			public void EndWaitForPendingOperations(IAsyncResult result)
			{
				ReliableChannelBinder<TChannel>.ChannelSynchronizer.SynchronizerCompletedAsyncResult synchronizerCompletedAsyncResult = result as ReliableChannelBinder<TChannel>.ChannelSynchronizer.SynchronizerCompletedAsyncResult;
				if (synchronizerCompletedAsyncResult != null)
				{
					synchronizerCompletedAsyncResult.End();
					return;
				}
				this.drainEvent.EndWait(result);
			}

			// Token: 0x06007F76 RID: 32630 RVA: 0x001D9C10 File Offset: 0x001D7E10
			public bool EnsureChannel()
			{
				bool flag = false;
				object obj = this.ThisLock;
				lock (obj)
				{
					if (this.ValidateOpened())
					{
						if (this.state == ReliableChannelBinder<TChannel>.ChannelSynchronizer.State.ChannelOpened)
						{
							return true;
						}
						if (this.state != ReliableChannelBinder<TChannel>.ChannelSynchronizer.State.NoChannel)
						{
							throw Fx.AssertAndThrow("The caller may only invoke this EnsureChannel during the CreateSequence negotiation. ChannelOpening and ChannelClosing are invalid states during this phase of the negotiation.");
						}
						if (!this.tolerateFaults)
						{
							flag = true;
						}
						else
						{
							if (this.GetCurrentChannelIfCreated() != null)
							{
								return true;
							}
							if (this.binder.TryGetChannel(TimeSpan.Zero))
							{
								if (this.currentChannel == null)
								{
									return false;
								}
								return true;
							}
						}
					}
				}
				if (flag)
				{
					this.binder.Fault(null);
				}
				return false;
			}

			// Token: 0x06007F77 RID: 32631 RVA: 0x001D9CCC File Offset: 0x001D7ECC
			private ReliableChannelBinder<TChannel>.ChannelSynchronizer.IWaiter GetChannelWaiter()
			{
				if (this.getChannelQueue == null || this.getChannelQueue.Count == 0)
				{
					return null;
				}
				return this.getChannelQueue.Dequeue();
			}

			// Token: 0x06007F78 RID: 32632 RVA: 0x001D9CF0 File Offset: 0x001D7EF0
			private TChannel GetCurrentChannelIfCreated()
			{
				if (this.state != ReliableChannelBinder<TChannel>.ChannelSynchronizer.State.NoChannel)
				{
					throw Fx.AssertAndThrow("This method may only be called in the NoChannel state.");
				}
				if (this.currentChannel != null && this.currentChannel.State == CommunicationState.Created)
				{
					return this.currentChannel;
				}
				return default(TChannel);
			}

			// Token: 0x06007F79 RID: 32633 RVA: 0x001D9D40 File Offset: 0x001D7F40
			private Queue<ReliableChannelBinder<TChannel>.ChannelSynchronizer.IWaiter> GetQueue(bool canGetChannel)
			{
				if (canGetChannel)
				{
					if (this.getChannelQueue == null)
					{
						this.getChannelQueue = new Queue<ReliableChannelBinder<TChannel>.ChannelSynchronizer.IWaiter>();
					}
					return this.getChannelQueue;
				}
				if (this.waitQueue == null)
				{
					this.waitQueue = new Queue<ReliableChannelBinder<TChannel>.ChannelSynchronizer.IWaiter>();
				}
				return this.waitQueue;
			}

			// Token: 0x06007F7A RID: 32634 RVA: 0x001D9D78 File Offset: 0x001D7F78
			private void OnChannelFaulted(object sender, EventArgs e)
			{
				TChannel tchannel = (TChannel)((object)sender);
				bool flag = false;
				bool flag2 = false;
				object obj = this.ThisLock;
				lock (obj)
				{
					if (this.currentChannel != tchannel)
					{
						return;
					}
					if (!this.ValidateOpened())
					{
						return;
					}
					if (this.state == ReliableChannelBinder<TChannel>.ChannelSynchronizer.State.ChannelOpened)
					{
						if (this.count == 0)
						{
							tchannel.Faulted -= this.onChannelFaulted;
						}
						flag = !this.tolerateFaults;
						this.state = ReliableChannelBinder<TChannel>.ChannelSynchronizer.State.ChannelClosing;
						this.innerChannelFaulted = true;
						if (!flag && this.count == 0)
						{
							this.state = ReliableChannelBinder<TChannel>.ChannelSynchronizer.State.NoChannel;
							this.aborting = false;
							flag2 = true;
							this.innerChannelFaulted = false;
						}
					}
				}
				if (flag)
				{
					this.binder.Fault(null);
				}
				tchannel.Abort();
				if (flag2)
				{
					this.binder.OnInnerChannelFaulted();
				}
			}

			// Token: 0x06007F7B RID: 32635 RVA: 0x001D9E68 File Offset: 0x001D8068
			private bool OnChannelOpened(ReliableChannelBinder<TChannel>.ChannelSynchronizer.IWaiter waiter)
			{
				if (waiter == null)
				{
					throw Fx.AssertAndThrow("Argument waiter cannot be null.");
				}
				bool flag = false;
				bool flag2 = false;
				Queue<ReliableChannelBinder<TChannel>.ChannelSynchronizer.IWaiter> waiters = null;
				Queue<ReliableChannelBinder<TChannel>.ChannelSynchronizer.IWaiter> waiters2 = null;
				TChannel channel = default(TChannel);
				object obj = this.ThisLock;
				lock (obj)
				{
					if (this.currentChannel == null)
					{
						throw Fx.AssertAndThrow("Caller must ensure that field currentChannel is set before opening the channel.");
					}
					if (this.ValidateOpened())
					{
						if (this.state != ReliableChannelBinder<TChannel>.ChannelSynchronizer.State.ChannelOpening)
						{
							throw Fx.AssertAndThrow("This method may only be called in the ChannelOpening state.");
						}
						this.state = ReliableChannelBinder<TChannel>.ChannelSynchronizer.State.ChannelOpened;
						this.SetTolerateFaults();
						this.count++;
						this.count += ((this.getChannelQueue == null) ? 0 : this.getChannelQueue.Count);
						this.count += ((this.waitQueue == null) ? 0 : this.waitQueue.Count);
						waiters = this.getChannelQueue;
						waiters2 = this.waitQueue;
						channel = this.currentChannel;
						this.getChannelQueue = null;
						this.waitQueue = null;
					}
					else
					{
						flag = (this.state == ReliableChannelBinder<TChannel>.ChannelSynchronizer.State.Closed);
						flag2 = (this.state == ReliableChannelBinder<TChannel>.ChannelSynchronizer.State.Faulted);
					}
				}
				if (flag)
				{
					waiter.Close();
					return false;
				}
				if (flag2)
				{
					waiter.Fault();
					return false;
				}
				this.SetWaiters(waiters, channel);
				this.SetWaiters(waiters2, channel);
				return true;
			}

			// Token: 0x06007F7C RID: 32636 RVA: 0x001D9FC0 File Offset: 0x001D81C0
			private void OnGetChannelFailed()
			{
				ReliableChannelBinder<TChannel>.ChannelSynchronizer.IWaiter waiter = null;
				object obj = this.ThisLock;
				lock (obj)
				{
					if (!this.ValidateOpened())
					{
						return;
					}
					if (this.state != ReliableChannelBinder<TChannel>.ChannelSynchronizer.State.ChannelOpening)
					{
						throw Fx.AssertAndThrow("The state must be set to ChannelOpening before the caller attempts to open the channel.");
					}
					waiter = this.GetChannelWaiter();
					if (waiter == null)
					{
						this.state = ReliableChannelBinder<TChannel>.ChannelSynchronizer.State.NoChannel;
						return;
					}
				}
				if (waiter is ReliableChannelBinder<TChannel>.ChannelSynchronizer.SyncWaiter)
				{
					waiter.GetChannel(false);
					return;
				}
				ActionItem.Schedule(ReliableChannelBinder<TChannel>.ChannelSynchronizer.asyncGetChannelCallback, waiter);
			}

			// Token: 0x06007F7D RID: 32637 RVA: 0x001DA048 File Offset: 0x001D8248
			public void OnReadEof()
			{
				object obj = this.ThisLock;
				lock (obj)
				{
					if (this.count <= 0)
					{
						throw Fx.AssertAndThrow("Caller must ensure that OnReadEof is called before ReturnChannel.");
					}
					if (this.ValidateOpened())
					{
						if (this.state != ReliableChannelBinder<TChannel>.ChannelSynchronizer.State.ChannelOpened && this.state != ReliableChannelBinder<TChannel>.ChannelSynchronizer.State.ChannelClosing)
						{
							throw Fx.AssertAndThrow("Since count is positive, the only valid states are ChannelOpened and ChannelClosing.");
						}
						if (this.currentChannel.State != CommunicationState.Faulted)
						{
							this.state = ReliableChannelBinder<TChannel>.ChannelSynchronizer.State.ChannelClosing;
						}
					}
				}
			}

			// Token: 0x06007F7E RID: 32638 RVA: 0x001DA0D8 File Offset: 0x001D82D8
			private bool RemoveWaiter(ReliableChannelBinder<TChannel>.ChannelSynchronizer.IWaiter waiter)
			{
				Queue<ReliableChannelBinder<TChannel>.ChannelSynchronizer.IWaiter> queue = waiter.CanGetChannel ? this.getChannelQueue : this.waitQueue;
				if (queue == null)
				{
					return false;
				}
				bool result = false;
				object obj = this.ThisLock;
				lock (obj)
				{
					if (!this.ValidateOpened())
					{
						return false;
					}
					for (int i = queue.Count; i > 0; i--)
					{
						ReliableChannelBinder<TChannel>.ChannelSynchronizer.IWaiter waiter2 = queue.Dequeue();
						if (waiter == waiter2)
						{
							result = true;
						}
						else
						{
							queue.Enqueue(waiter2);
						}
					}
				}
				return result;
			}

			// Token: 0x06007F7F RID: 32639 RVA: 0x001DA170 File Offset: 0x001D8370
			public void ReturnChannel()
			{
				TChannel tchannel = default(TChannel);
				ReliableChannelBinder<TChannel>.ChannelSynchronizer.IWaiter waiter = null;
				bool flag = false;
				bool flag2 = false;
				object obj = this.ThisLock;
				bool flag4;
				lock (obj)
				{
					if (this.count <= 0)
					{
						throw Fx.AssertAndThrow("Method ReturnChannel() can only be called after TryGetChannel or EndTryGetChannel returns a channel.");
					}
					this.count--;
					flag4 = (this.count == 0 && this.drainEvent != null);
					if (this.ValidateOpened())
					{
						if (this.state != ReliableChannelBinder<TChannel>.ChannelSynchronizer.State.ChannelOpened && this.state != ReliableChannelBinder<TChannel>.ChannelSynchronizer.State.ChannelClosing)
						{
							throw Fx.AssertAndThrow("ChannelOpened and ChannelClosing are the only 2 valid states when count is positive.");
						}
						if (this.currentChannel.State == CommunicationState.Faulted)
						{
							flag = !this.tolerateFaults;
							this.innerChannelFaulted = true;
							this.state = ReliableChannelBinder<TChannel>.ChannelSynchronizer.State.ChannelClosing;
						}
						if (!flag && this.state == ReliableChannelBinder<TChannel>.ChannelSynchronizer.State.ChannelClosing && this.count == 0)
						{
							tchannel = this.currentChannel;
							flag2 = this.innerChannelFaulted;
							this.innerChannelFaulted = false;
							this.state = ReliableChannelBinder<TChannel>.ChannelSynchronizer.State.NoChannel;
							this.aborting = false;
							waiter = this.GetChannelWaiter();
							if (waiter != null)
							{
								this.state = ReliableChannelBinder<TChannel>.ChannelSynchronizer.State.ChannelOpening;
							}
						}
					}
				}
				if (flag)
				{
					this.binder.Fault(null);
				}
				if (flag4)
				{
					this.drainEvent.Set();
				}
				if (tchannel != null)
				{
					tchannel.Faulted -= this.onChannelFaulted;
					if (tchannel.State == CommunicationState.Opened)
					{
						this.binder.CloseChannel(tchannel);
					}
					else
					{
						tchannel.Abort();
					}
					if (waiter != null)
					{
						waiter.GetChannel(false);
					}
				}
				if (flag2)
				{
					this.binder.OnInnerChannelFaulted();
				}
			}

			// Token: 0x06007F80 RID: 32640 RVA: 0x001DA308 File Offset: 0x001D8508
			public bool SetChannel(TChannel channel)
			{
				object obj = this.ThisLock;
				bool result;
				lock (obj)
				{
					if (this.state != ReliableChannelBinder<TChannel>.ChannelSynchronizer.State.ChannelOpening && this.state != ReliableChannelBinder<TChannel>.ChannelSynchronizer.State.NoChannel)
					{
						throw Fx.AssertAndThrow("SetChannel is only valid in the NoChannel and ChannelOpening states");
					}
					if (!this.tolerateFaults)
					{
						throw Fx.AssertAndThrow("SetChannel is only valid when masking faults");
					}
					if (this.ValidateOpened())
					{
						this.currentChannel = channel;
						result = true;
					}
					else
					{
						result = false;
					}
				}
				return result;
			}

			// Token: 0x06007F81 RID: 32641 RVA: 0x001DA388 File Offset: 0x001D8588
			private void SetTolerateFaults()
			{
				if (this.faultMode == TolerateFaultsMode.Never)
				{
					this.tolerateFaults = false;
				}
				else if (this.faultMode == TolerateFaultsMode.IfNotSecuritySession)
				{
					this.tolerateFaults = !this.binder.HasSecuritySession(this.currentChannel);
				}
				if (this.onChannelFaulted == null)
				{
					this.onChannelFaulted = new EventHandler(this.OnChannelFaulted);
				}
				this.currentChannel.Faulted += this.onChannelFaulted;
			}

			// Token: 0x06007F82 RID: 32642 RVA: 0x001DA3FC File Offset: 0x001D85FC
			private void SetWaiters(Queue<ReliableChannelBinder<TChannel>.ChannelSynchronizer.IWaiter> waiters, TChannel channel)
			{
				if (waiters != null && waiters.Count > 0)
				{
					foreach (ReliableChannelBinder<TChannel>.ChannelSynchronizer.IWaiter waiter in waiters)
					{
						waiter.Set(channel);
					}
				}
			}

			// Token: 0x06007F83 RID: 32643 RVA: 0x001DA458 File Offset: 0x001D8658
			public void StartSynchronizing()
			{
				object obj = this.ThisLock;
				lock (obj)
				{
					if (this.state == ReliableChannelBinder<TChannel>.ChannelSynchronizer.State.Created)
					{
						this.state = ReliableChannelBinder<TChannel>.ChannelSynchronizer.State.NoChannel;
						if (this.currentChannel != null || this.binder.TryGetChannel(TimeSpan.Zero))
						{
							if (this.currentChannel != null)
							{
								if (!this.binder.MustOpenChannel)
								{
									this.state = ReliableChannelBinder<TChannel>.ChannelSynchronizer.State.ChannelOpened;
									this.SetTolerateFaults();
								}
							}
						}
					}
					else if (this.state != ReliableChannelBinder<TChannel>.ChannelSynchronizer.State.Closed)
					{
						throw Fx.AssertAndThrow("Abort is the only operation that can race with Open.");
					}
				}
			}

			// Token: 0x06007F84 RID: 32644 RVA: 0x001DA504 File Offset: 0x001D8704
			public TChannel StopSynchronizing(bool close)
			{
				object obj = this.ThisLock;
				TChannel result;
				lock (obj)
				{
					if (this.state != ReliableChannelBinder<TChannel>.ChannelSynchronizer.State.Faulted && this.state != ReliableChannelBinder<TChannel>.ChannelSynchronizer.State.Closed)
					{
						this.state = (close ? ReliableChannelBinder<TChannel>.ChannelSynchronizer.State.Closed : ReliableChannelBinder<TChannel>.ChannelSynchronizer.State.Faulted);
						if (this.currentChannel != null && this.onChannelFaulted != null)
						{
							this.currentChannel.Faulted -= this.onChannelFaulted;
						}
					}
					result = this.currentChannel;
				}
				return result;
			}

			// Token: 0x06007F85 RID: 32645 RVA: 0x001DA590 File Offset: 0x001D8790
			private bool ThrowIfNecessary(MaskingMode maskingMode)
			{
				if (this.ValidateOpened())
				{
					return true;
				}
				Exception ex;
				if (this.state == ReliableChannelBinder<TChannel>.ChannelSynchronizer.State.Closed)
				{
					ex = this.binder.GetClosedException(maskingMode);
				}
				else
				{
					ex = this.binder.GetFaultedException(maskingMode);
				}
				if (ex != null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(ex);
				}
				return false;
			}

			// Token: 0x06007F86 RID: 32646 RVA: 0x001DA5DC File Offset: 0x001D87DC
			public bool TryGetChannelForInput(bool canGetChannel, TimeSpan timeout, out TChannel channel)
			{
				return this.TryGetChannel(canGetChannel, false, timeout, MaskingMode.All, out channel);
			}

			// Token: 0x06007F87 RID: 32647 RVA: 0x001DA5E9 File Offset: 0x001D87E9
			public bool TryGetChannelForOutput(TimeSpan timeout, MaskingMode maskingMode, out TChannel channel)
			{
				return this.TryGetChannel(true, true, timeout, maskingMode, out channel);
			}

			// Token: 0x06007F88 RID: 32648 RVA: 0x001DA5F8 File Offset: 0x001D87F8
			private bool TryGetChannel(bool canGetChannel, bool canCauseFault, TimeSpan timeout, MaskingMode maskingMode, out TChannel channel)
			{
				ReliableChannelBinder<TChannel>.ChannelSynchronizer.SyncWaiter syncWaiter = null;
				bool flag = false;
				bool flag2 = false;
				object obj = this.ThisLock;
				lock (obj)
				{
					if (!this.ThrowIfNecessary(maskingMode))
					{
						channel = default(TChannel);
						return true;
					}
					if (this.state == ReliableChannelBinder<TChannel>.ChannelSynchronizer.State.ChannelOpened)
					{
						if (this.currentChannel == null)
						{
							throw Fx.AssertAndThrow("Field currentChannel cannot be null in the ChannelOpened state.");
						}
						this.count++;
						channel = this.currentChannel;
						return true;
					}
					else if (!this.tolerateFaults && (this.state == ReliableChannelBinder<TChannel>.ChannelSynchronizer.State.ChannelClosing || this.state == ReliableChannelBinder<TChannel>.ChannelSynchronizer.State.NoChannel))
					{
						if (!canCauseFault)
						{
							channel = default(TChannel);
							return true;
						}
						flag = true;
					}
					else if (!canGetChannel || this.state == ReliableChannelBinder<TChannel>.ChannelSynchronizer.State.ChannelOpening || this.state == ReliableChannelBinder<TChannel>.ChannelSynchronizer.State.ChannelClosing)
					{
						syncWaiter = new ReliableChannelBinder<TChannel>.ChannelSynchronizer.SyncWaiter(this, canGetChannel, default(TChannel), timeout, maskingMode, this.binder.ChannelParameters);
						this.GetQueue(canGetChannel).Enqueue(syncWaiter);
					}
					else
					{
						if (this.state != ReliableChannelBinder<TChannel>.ChannelSynchronizer.State.NoChannel)
						{
							throw Fx.AssertAndThrow("The state must be NoChannel.");
						}
						syncWaiter = new ReliableChannelBinder<TChannel>.ChannelSynchronizer.SyncWaiter(this, canGetChannel, this.GetCurrentChannelIfCreated(), timeout, maskingMode, this.binder.ChannelParameters);
						this.state = ReliableChannelBinder<TChannel>.ChannelSynchronizer.State.ChannelOpening;
						flag2 = true;
					}
				}
				if (flag)
				{
					this.binder.Fault(null);
					channel = default(TChannel);
					return true;
				}
				if (flag2)
				{
					syncWaiter.GetChannel(true);
				}
				return syncWaiter.TryWait(out channel);
			}

			// Token: 0x06007F89 RID: 32649 RVA: 0x001DA784 File Offset: 0x001D8984
			public void UnblockWaiters()
			{
				object obj = this.ThisLock;
				Queue<ReliableChannelBinder<TChannel>.ChannelSynchronizer.IWaiter> waiters;
				Queue<ReliableChannelBinder<TChannel>.ChannelSynchronizer.IWaiter> waiters2;
				lock (obj)
				{
					waiters = this.getChannelQueue;
					waiters2 = this.waitQueue;
					this.getChannelQueue = null;
					this.waitQueue = null;
				}
				bool close = this.state == ReliableChannelBinder<TChannel>.ChannelSynchronizer.State.Closed;
				this.UnblockWaiters(waiters, close);
				this.UnblockWaiters(waiters2, close);
			}

			// Token: 0x06007F8A RID: 32650 RVA: 0x001DA7F8 File Offset: 0x001D89F8
			private void UnblockWaiters(Queue<ReliableChannelBinder<TChannel>.ChannelSynchronizer.IWaiter> waiters, bool close)
			{
				if (waiters != null && waiters.Count > 0)
				{
					foreach (ReliableChannelBinder<TChannel>.ChannelSynchronizer.IWaiter waiter in waiters)
					{
						if (close)
						{
							waiter.Close();
						}
						else
						{
							waiter.Fault();
						}
					}
				}
			}

			// Token: 0x06007F8B RID: 32651 RVA: 0x001DA85C File Offset: 0x001D8A5C
			private bool ValidateOpened()
			{
				if (this.state == ReliableChannelBinder<TChannel>.ChannelSynchronizer.State.Created)
				{
					throw Fx.AssertAndThrow("This operation expects that the synchronizer has been opened.");
				}
				return this.state != ReliableChannelBinder<TChannel>.ChannelSynchronizer.State.Closed && this.state != ReliableChannelBinder<TChannel>.ChannelSynchronizer.State.Faulted;
			}

			// Token: 0x06007F8C RID: 32652 RVA: 0x001DA888 File Offset: 0x001D8A88
			public void WaitForPendingOperations(TimeSpan timeout)
			{
				object obj = this.ThisLock;
				lock (obj)
				{
					if (this.drainEvent != null)
					{
						throw Fx.AssertAndThrow("The WaitForPendingOperations operation may only be invoked once.");
					}
					if (this.count > 0)
					{
						this.drainEvent = new InterruptibleWaitObject(false, false);
					}
				}
				if (this.drainEvent != null)
				{
					this.drainEvent.Wait(timeout);
				}
			}

			// Token: 0x040048E6 RID: 18662
			private bool aborting;

			// Token: 0x040048E7 RID: 18663
			private ReliableChannelBinder<TChannel> binder;

			// Token: 0x040048E8 RID: 18664
			private int count;

			// Token: 0x040048E9 RID: 18665
			private TChannel currentChannel;

			// Token: 0x040048EA RID: 18666
			private InterruptibleWaitObject drainEvent;

			// Token: 0x040048EB RID: 18667
			private static Action<object> asyncGetChannelCallback = new Action<object>(ReliableChannelBinder<TChannel>.ChannelSynchronizer.AsyncGetChannelCallback);

			// Token: 0x040048EC RID: 18668
			private TolerateFaultsMode faultMode;

			// Token: 0x040048ED RID: 18669
			private Queue<ReliableChannelBinder<TChannel>.ChannelSynchronizer.IWaiter> getChannelQueue;

			// Token: 0x040048EE RID: 18670
			private bool innerChannelFaulted;

			// Token: 0x040048EF RID: 18671
			private EventHandler onChannelFaulted;

			// Token: 0x040048F0 RID: 18672
			private ReliableChannelBinder<TChannel>.ChannelSynchronizer.State state;

			// Token: 0x040048F1 RID: 18673
			private bool tolerateFaults = true;

			// Token: 0x040048F2 RID: 18674
			private object thisLock = new object();

			// Token: 0x040048F3 RID: 18675
			private Queue<ReliableChannelBinder<TChannel>.ChannelSynchronizer.IWaiter> waitQueue;

			// Token: 0x02000F71 RID: 3953
			private enum State
			{
				// Token: 0x04004F28 RID: 20264
				Created,
				// Token: 0x04004F29 RID: 20265
				NoChannel,
				// Token: 0x04004F2A RID: 20266
				ChannelOpening,
				// Token: 0x04004F2B RID: 20267
				ChannelOpened,
				// Token: 0x04004F2C RID: 20268
				ChannelClosing,
				// Token: 0x04004F2D RID: 20269
				Faulted,
				// Token: 0x04004F2E RID: 20270
				Closed
			}

			// Token: 0x02000F72 RID: 3954
			public interface IWaiter
			{
				// Token: 0x17001D9A RID: 7578
				// (get) Token: 0x060087BE RID: 34750
				bool CanGetChannel { get; }

				// Token: 0x060087BF RID: 34751
				void Close();

				// Token: 0x060087C0 RID: 34752
				void Fault();

				// Token: 0x060087C1 RID: 34753
				void GetChannel(bool onUserThread);

				// Token: 0x060087C2 RID: 34754
				void Set(TChannel channel);
			}

			// Token: 0x02000F73 RID: 3955
			public sealed class AsyncWaiter : AsyncResult, ReliableChannelBinder<TChannel>.ChannelSynchronizer.IWaiter
			{
				// Token: 0x060087C3 RID: 34755 RVA: 0x001F898C File Offset: 0x001F6B8C
				public AsyncWaiter(ReliableChannelBinder<TChannel>.ChannelSynchronizer synchronizer, bool canGetChannel, TChannel channel, TimeSpan timeout, MaskingMode maskingMode, ChannelParameterCollection channelParameters, AsyncCallback callback, object state) : base(callback, state)
				{
					if (!canGetChannel && channel != null)
					{
						throw Fx.AssertAndThrow("This waiter must wait for a channel thus argument channel must be null.");
					}
					this.synchronizer = synchronizer;
					this.canGetChannel = canGetChannel;
					this.channel = channel;
					this.timeoutHelper = new TimeoutHelper(timeout);
					this.maskingMode = maskingMode;
					this.channelParameters = channelParameters;
				}

				// Token: 0x17001D9B RID: 7579
				// (get) Token: 0x060087C4 RID: 34756 RVA: 0x001F89F2 File Offset: 0x001F6BF2
				public bool CanGetChannel
				{
					get
					{
						return this.canGetChannel;
					}
				}

				// Token: 0x17001D9C RID: 7580
				// (get) Token: 0x060087C5 RID: 34757 RVA: 0x001F89FA File Offset: 0x001F6BFA
				private object ThisLock
				{
					get
					{
						return this;
					}
				}

				// Token: 0x060087C6 RID: 34758 RVA: 0x001F8A00 File Offset: 0x001F6C00
				private void CancelTimer()
				{
					object thisLock = this.ThisLock;
					lock (thisLock)
					{
						if (!this.timerCancelled)
						{
							if (this.timer != null)
							{
								this.timer.Cancel();
							}
							this.timerCancelled = true;
						}
					}
				}

				// Token: 0x060087C7 RID: 34759 RVA: 0x001F8A60 File Offset: 0x001F6C60
				public void Close()
				{
					this.CancelTimer();
					this.channel = default(TChannel);
					base.Complete(false, this.synchronizer.binder.GetClosedException(this.maskingMode));
				}

				// Token: 0x060087C8 RID: 34760 RVA: 0x001F8A91 File Offset: 0x001F6C91
				private bool CompleteOpen(IAsyncResult result)
				{
					this.channel.EndOpen(result);
					return this.OnChannelOpened();
				}

				// Token: 0x060087C9 RID: 34761 RVA: 0x001F8AAC File Offset: 0x001F6CAC
				private bool CompleteTryGetChannel(IAsyncResult result)
				{
					if (!this.synchronizer.binder.EndTryGetChannel(result))
					{
						this.timedOut = true;
						this.OnGetChannelFailed();
						return true;
					}
					if (this.synchronizer.CompleteSetChannel(this, out this.channel))
					{
						return this.OpenChannel();
					}
					if (!base.IsCompleted)
					{
						throw Fx.AssertAndThrow("CompleteSetChannel must complete the IWaiter if it returns false.");
					}
					return false;
				}

				// Token: 0x060087CA RID: 34762 RVA: 0x001F8B0A File Offset: 0x001F6D0A
				public bool End(out TChannel channel)
				{
					AsyncResult.End<ReliableChannelBinder<TChannel>.ChannelSynchronizer.AsyncWaiter>(this);
					channel = this.channel;
					return !this.timedOut;
				}

				// Token: 0x060087CB RID: 34763 RVA: 0x001F8B28 File Offset: 0x001F6D28
				public void Fault()
				{
					this.CancelTimer();
					this.channel = default(TChannel);
					base.Complete(false, this.synchronizer.binder.GetFaultedException(this.maskingMode));
				}

				// Token: 0x060087CC RID: 34764 RVA: 0x001F8B5C File Offset: 0x001F6D5C
				private bool GetChannel()
				{
					if (this.channel != null)
					{
						return this.OpenChannel();
					}
					IAsyncResult asyncResult = this.synchronizer.binder.BeginTryGetChannel(this.timeoutHelper.RemainingTime(), ReliableChannelBinder<TChannel>.ChannelSynchronizer.AsyncWaiter.onTryGetChannelComplete, this);
					return asyncResult.CompletedSynchronously && this.CompleteTryGetChannel(asyncResult);
				}

				// Token: 0x060087CD RID: 34765 RVA: 0x001F8BB0 File Offset: 0x001F6DB0
				public void GetChannel(bool onUserThread)
				{
					if (!this.CanGetChannel)
					{
						throw Fx.AssertAndThrow("This waiter must wait for a channel thus the caller cannot attempt to get a channel.");
					}
					this.isSynchronous = onUserThread;
					if (onUserThread)
					{
						bool flag = true;
						try
						{
							if (this.GetChannel())
							{
								base.Complete(true);
							}
							flag = false;
							return;
						}
						finally
						{
							if (flag)
							{
								this.OnGetChannelFailed();
							}
						}
					}
					bool flag2 = false;
					Exception ex = null;
					try
					{
						this.CancelTimer();
						flag2 = this.GetChannel();
					}
					catch (Exception ex2)
					{
						if (Fx.IsFatal(ex2))
						{
							throw;
						}
						this.OnGetChannelFailed();
						ex = ex2;
					}
					if (flag2 || ex != null)
					{
						base.Complete(false, ex);
					}
				}

				// Token: 0x060087CE RID: 34766 RVA: 0x001F8C4C File Offset: 0x001F6E4C
				private bool OnChannelOpened()
				{
					if (this.synchronizer.OnChannelOpened(this))
					{
						return true;
					}
					if (!base.IsCompleted)
					{
						throw Fx.AssertAndThrow("OnChannelOpened must complete the IWaiter if it returns false.");
					}
					return false;
				}

				// Token: 0x060087CF RID: 34767 RVA: 0x001F8C72 File Offset: 0x001F6E72
				private void OnGetChannelFailed()
				{
					if (this.channel != null)
					{
						this.channel.Abort();
					}
					this.synchronizer.OnGetChannelFailed();
				}

				// Token: 0x060087D0 RID: 34768 RVA: 0x001F8C9C File Offset: 0x001F6E9C
				private static void OnOpenComplete(IAsyncResult result)
				{
					if (!result.CompletedSynchronously)
					{
						ReliableChannelBinder<TChannel>.ChannelSynchronizer.AsyncWaiter asyncWaiter = (ReliableChannelBinder<TChannel>.ChannelSynchronizer.AsyncWaiter)result.AsyncState;
						bool flag = false;
						Exception ex = null;
						asyncWaiter.isSynchronous = false;
						try
						{
							flag = asyncWaiter.CompleteOpen(result);
						}
						catch (Exception ex2)
						{
							if (Fx.IsFatal(ex2))
							{
								throw;
							}
							ex = ex2;
						}
						if (flag)
						{
							asyncWaiter.Complete(false);
							return;
						}
						if (ex != null)
						{
							asyncWaiter.OnGetChannelFailed();
							asyncWaiter.Complete(false, ex);
						}
					}
				}

				// Token: 0x060087D1 RID: 34769 RVA: 0x001F8D10 File Offset: 0x001F6F10
				private void OnTimeoutElapsed()
				{
					if (this.synchronizer.RemoveWaiter(this))
					{
						this.timedOut = true;
						base.Complete(this.isSynchronous, null);
					}
				}

				// Token: 0x060087D2 RID: 34770 RVA: 0x001F8D34 File Offset: 0x001F6F34
				private static void OnTimeoutElapsed(object state)
				{
					ReliableChannelBinder<TChannel>.ChannelSynchronizer.AsyncWaiter asyncWaiter = (ReliableChannelBinder<TChannel>.ChannelSynchronizer.AsyncWaiter)state;
					asyncWaiter.isSynchronous = false;
					asyncWaiter.OnTimeoutElapsed();
				}

				// Token: 0x060087D3 RID: 34771 RVA: 0x001F8D58 File Offset: 0x001F6F58
				private static void OnTryGetChannelComplete(IAsyncResult result)
				{
					if (!result.CompletedSynchronously)
					{
						ReliableChannelBinder<TChannel>.ChannelSynchronizer.AsyncWaiter asyncWaiter = (ReliableChannelBinder<TChannel>.ChannelSynchronizer.AsyncWaiter)result.AsyncState;
						asyncWaiter.isSynchronous = false;
						bool flag = false;
						Exception ex = null;
						try
						{
							flag = asyncWaiter.CompleteTryGetChannel(result);
						}
						catch (Exception ex2)
						{
							if (Fx.IsFatal(ex2))
							{
								throw;
							}
							ex = ex2;
						}
						if (flag || ex != null)
						{
							if (ex != null)
							{
								asyncWaiter.OnGetChannelFailed();
							}
							asyncWaiter.Complete(asyncWaiter.isSynchronous, ex);
						}
					}
				}

				// Token: 0x060087D4 RID: 34772 RVA: 0x001F8DCC File Offset: 0x001F6FCC
				private bool OpenChannel()
				{
					if (this.synchronizer.binder.MustOpenChannel)
					{
						if (this.channelParameters != null)
						{
							this.channelParameters.PropagateChannelParameters(this.channel);
						}
						IAsyncResult asyncResult = this.channel.BeginOpen(this.timeoutHelper.RemainingTime(), ReliableChannelBinder<TChannel>.ChannelSynchronizer.AsyncWaiter.onOpenComplete, this);
						return asyncResult.CompletedSynchronously && this.CompleteOpen(asyncResult);
					}
					return this.OnChannelOpened();
				}

				// Token: 0x060087D5 RID: 34773 RVA: 0x001F8E43 File Offset: 0x001F7043
				public void Set(TChannel channel)
				{
					this.CancelTimer();
					this.channel = channel;
					base.Complete(false);
				}

				// Token: 0x060087D6 RID: 34774 RVA: 0x001F8E5C File Offset: 0x001F705C
				public void Wait()
				{
					object thisLock = this.ThisLock;
					lock (thisLock)
					{
						if (this.timerCancelled)
						{
							return;
						}
						TimeSpan t = this.timeoutHelper.RemainingTime();
						if (t > TimeSpan.Zero)
						{
							this.timer = new IOThreadTimer(ReliableChannelBinder<TChannel>.ChannelSynchronizer.AsyncWaiter.onTimeoutElapsed, this, true);
							this.timer.Set(this.timeoutHelper.RemainingTime());
							return;
						}
					}
					this.OnTimeoutElapsed();
				}

				// Token: 0x04004F2F RID: 20271
				private bool canGetChannel;

				// Token: 0x04004F30 RID: 20272
				private TChannel channel;

				// Token: 0x04004F31 RID: 20273
				private ChannelParameterCollection channelParameters;

				// Token: 0x04004F32 RID: 20274
				private bool isSynchronous = true;

				// Token: 0x04004F33 RID: 20275
				private MaskingMode maskingMode;

				// Token: 0x04004F34 RID: 20276
				private static AsyncCallback onOpenComplete = Fx.ThunkCallback(new AsyncCallback(ReliableChannelBinder<TChannel>.ChannelSynchronizer.AsyncWaiter.OnOpenComplete));

				// Token: 0x04004F35 RID: 20277
				private static Action<object> onTimeoutElapsed = new Action<object>(ReliableChannelBinder<TChannel>.ChannelSynchronizer.AsyncWaiter.OnTimeoutElapsed);

				// Token: 0x04004F36 RID: 20278
				private static AsyncCallback onTryGetChannelComplete = Fx.ThunkCallback(new AsyncCallback(ReliableChannelBinder<TChannel>.ChannelSynchronizer.AsyncWaiter.OnTryGetChannelComplete));

				// Token: 0x04004F37 RID: 20279
				private bool timedOut;

				// Token: 0x04004F38 RID: 20280
				private ReliableChannelBinder<TChannel>.ChannelSynchronizer synchronizer;

				// Token: 0x04004F39 RID: 20281
				private TimeoutHelper timeoutHelper;

				// Token: 0x04004F3A RID: 20282
				private IOThreadTimer timer;

				// Token: 0x04004F3B RID: 20283
				private bool timerCancelled;
			}

			// Token: 0x02000F74 RID: 3956
			private sealed class SynchronizerCompletedAsyncResult : CompletedAsyncResult
			{
				// Token: 0x060087D8 RID: 34776 RVA: 0x001F8F2B File Offset: 0x001F712B
				public SynchronizerCompletedAsyncResult(AsyncCallback callback, object state) : base(callback, state)
				{
				}

				// Token: 0x060087D9 RID: 34777 RVA: 0x001F8F35 File Offset: 0x001F7135
				public void End()
				{
					CompletedAsyncResult.End(this);
				}
			}

			// Token: 0x02000F75 RID: 3957
			private sealed class SyncWaiter : ReliableChannelBinder<TChannel>.ChannelSynchronizer.IWaiter
			{
				// Token: 0x060087DA RID: 34778 RVA: 0x001F8F40 File Offset: 0x001F7140
				public SyncWaiter(ReliableChannelBinder<TChannel>.ChannelSynchronizer synchronizer, bool canGetChannel, TChannel channel, TimeSpan timeout, MaskingMode maskingMode, ChannelParameterCollection channelParameters)
				{
					if (!canGetChannel && channel != null)
					{
						throw Fx.AssertAndThrow("This waiter must wait for a channel thus argument channel must be null.");
					}
					this.synchronizer = synchronizer;
					this.canGetChannel = canGetChannel;
					this.channel = channel;
					this.timeoutHelper = new TimeoutHelper(timeout);
					this.maskingMode = maskingMode;
					this.channelParameters = channelParameters;
				}

				// Token: 0x17001D9D RID: 7581
				// (get) Token: 0x060087DB RID: 34779 RVA: 0x001F8FA7 File Offset: 0x001F71A7
				public bool CanGetChannel
				{
					get
					{
						return this.canGetChannel;
					}
				}

				// Token: 0x060087DC RID: 34780 RVA: 0x001F8FAF File Offset: 0x001F71AF
				public void Close()
				{
					this.exception = this.synchronizer.binder.GetClosedException(this.maskingMode);
					this.completeEvent.Set();
				}

				// Token: 0x060087DD RID: 34781 RVA: 0x001F8FD9 File Offset: 0x001F71D9
				public void Fault()
				{
					this.exception = this.synchronizer.binder.GetFaultedException(this.maskingMode);
					this.completeEvent.Set();
				}

				// Token: 0x060087DE RID: 34782 RVA: 0x001F9003 File Offset: 0x001F7203
				public void GetChannel(bool onUserThread)
				{
					if (!this.CanGetChannel)
					{
						throw Fx.AssertAndThrow("This waiter must wait for a channel thus the caller cannot attempt to get a channel.");
					}
					this.getChannel = true;
					this.completeEvent.Set();
				}

				// Token: 0x060087DF RID: 34783 RVA: 0x001F902B File Offset: 0x001F722B
				public void Set(TChannel channel)
				{
					if (channel == null)
					{
						throw Fx.AssertAndThrow("Argument channel cannot be null. Caller must call Fault or Close instead.");
					}
					this.channel = channel;
					this.completeEvent.Set();
				}

				// Token: 0x060087E0 RID: 34784 RVA: 0x001F9054 File Offset: 0x001F7254
				private bool TryGetChannel()
				{
					TChannel tchannel;
					if (this.channel != null)
					{
						tchannel = this.channel;
					}
					else
					{
						if (!this.synchronizer.binder.TryGetChannel(this.timeoutHelper.RemainingTime()))
						{
							this.synchronizer.OnGetChannelFailed();
							return false;
						}
						if (!this.synchronizer.CompleteSetChannel(this, out tchannel))
						{
							return true;
						}
					}
					if (this.synchronizer.binder.MustOpenChannel)
					{
						bool flag = true;
						if (this.channelParameters != null)
						{
							this.channelParameters.PropagateChannelParameters(tchannel);
						}
						try
						{
							tchannel.Open(this.timeoutHelper.RemainingTime());
							flag = false;
						}
						finally
						{
							if (flag)
							{
								tchannel.Abort();
								this.synchronizer.OnGetChannelFailed();
							}
						}
					}
					if (this.synchronizer.OnChannelOpened(this))
					{
						this.Set(tchannel);
					}
					return true;
				}

				// Token: 0x060087E1 RID: 34785 RVA: 0x001F913C File Offset: 0x001F733C
				public bool TryWait(out TChannel channel)
				{
					if (!this.Wait())
					{
						channel = default(TChannel);
						return false;
					}
					if (this.getChannel && !this.TryGetChannel())
					{
						channel = default(TChannel);
						return false;
					}
					this.completeEvent.Close();
					if (this.exception == null)
					{
						channel = this.channel;
						return true;
					}
					if (this.channel != null)
					{
						throw Fx.AssertAndThrow("User of IWaiter called both Set and Fault or Close.");
					}
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(this.exception);
				}

				// Token: 0x060087E2 RID: 34786 RVA: 0x001F91BC File Offset: 0x001F73BC
				private bool Wait()
				{
					if (!TimeoutHelper.WaitOne(this.completeEvent, this.timeoutHelper.RemainingTime()))
					{
						if (this.synchronizer.RemoveWaiter(this))
						{
							return false;
						}
						TimeoutHelper.WaitOne(this.completeEvent, TimeSpan.MaxValue);
					}
					return true;
				}

				// Token: 0x04004F3C RID: 20284
				private bool canGetChannel;

				// Token: 0x04004F3D RID: 20285
				private TChannel channel;

				// Token: 0x04004F3E RID: 20286
				private ChannelParameterCollection channelParameters;

				// Token: 0x04004F3F RID: 20287
				private AutoResetEvent completeEvent = new AutoResetEvent(false);

				// Token: 0x04004F40 RID: 20288
				private Exception exception;

				// Token: 0x04004F41 RID: 20289
				private bool getChannel;

				// Token: 0x04004F42 RID: 20290
				private MaskingMode maskingMode;

				// Token: 0x04004F43 RID: 20291
				private ReliableChannelBinder<TChannel>.ChannelSynchronizer synchronizer;

				// Token: 0x04004F44 RID: 20292
				private TimeoutHelper timeoutHelper;
			}
		}

		// Token: 0x02000DBA RID: 3514
		private sealed class CloseAsyncResult : AsyncResult
		{
			// Token: 0x06007F8E RID: 32654 RVA: 0x001DA914 File Offset: 0x001D8B14
			public CloseAsyncResult(ReliableChannelBinder<TChannel> binder, TChannel channel, TimeSpan timeout, MaskingMode maskingMode, AsyncCallback callback, object state) : base(callback, state)
			{
				this.binder = binder;
				this.channel = channel;
				this.timeoutHelper = new TimeoutHelper(timeout);
				this.maskingMode = maskingMode;
				bool flag = false;
				try
				{
					this.binder.OnShutdown();
					IAsyncResult asyncResult = this.binder.OnBeginClose(timeout, ReliableChannelBinder<TChannel>.CloseAsyncResult.onBinderCloseComplete, this);
					if (asyncResult.CompletedSynchronously)
					{
						flag = this.CompleteBinderClose(true, asyncResult);
					}
				}
				catch (Exception ex)
				{
					if (Fx.IsFatal(ex))
					{
						throw;
					}
					this.binder.Abort();
					if (!this.binder.HandleException(ex, this.maskingMode))
					{
						throw;
					}
					flag = true;
				}
				if (flag)
				{
					base.Complete(true);
				}
			}

			// Token: 0x06007F8F RID: 32655 RVA: 0x001DA9CC File Offset: 0x001D8BCC
			private bool CompleteBinderClose(bool synchronous, IAsyncResult result)
			{
				this.binder.OnEndClose(result);
				if (this.channel != null)
				{
					result = this.binder.BeginCloseChannel(this.channel, this.timeoutHelper.RemainingTime(), ReliableChannelBinder<TChannel>.CloseAsyncResult.onChannelCloseComplete, this);
					return result.CompletedSynchronously && this.CompleteChannelClose(synchronous, result);
				}
				this.binder.TransitionToClosed();
				return true;
			}

			// Token: 0x06007F90 RID: 32656 RVA: 0x001DAA35 File Offset: 0x001D8C35
			private bool CompleteChannelClose(bool synchronous, IAsyncResult result)
			{
				this.binder.EndCloseChannel(this.channel, result);
				this.binder.TransitionToClosed();
				return true;
			}

			// Token: 0x06007F91 RID: 32657 RVA: 0x001DAA55 File Offset: 0x001D8C55
			public void End()
			{
				AsyncResult.End<ReliableChannelBinder<TChannel>.CloseAsyncResult>(this);
			}

			// Token: 0x06007F92 RID: 32658 RVA: 0x001DAA5E File Offset: 0x001D8C5E
			private Exception HandleAsyncException(Exception e)
			{
				this.binder.Abort();
				if (this.binder.HandleException(e, this.maskingMode))
				{
					return null;
				}
				return e;
			}

			// Token: 0x06007F93 RID: 32659 RVA: 0x001DAA84 File Offset: 0x001D8C84
			private static void OnBinderCloseComplete(IAsyncResult result)
			{
				if (!result.CompletedSynchronously)
				{
					ReliableChannelBinder<TChannel>.CloseAsyncResult closeAsyncResult = (ReliableChannelBinder<TChannel>.CloseAsyncResult)result.AsyncState;
					bool flag;
					Exception ex;
					try
					{
						flag = closeAsyncResult.CompleteBinderClose(false, result);
						ex = null;
					}
					catch (Exception ex2)
					{
						if (Fx.IsFatal(ex2))
						{
							throw;
						}
						flag = true;
						ex = ex2;
					}
					if (flag)
					{
						if (ex != null)
						{
							ex = closeAsyncResult.HandleAsyncException(ex);
						}
						closeAsyncResult.Complete(false, ex);
					}
				}
			}

			// Token: 0x06007F94 RID: 32660 RVA: 0x001DAAEC File Offset: 0x001D8CEC
			private static void OnChannelCloseComplete(IAsyncResult result)
			{
				if (!result.CompletedSynchronously)
				{
					ReliableChannelBinder<TChannel>.CloseAsyncResult closeAsyncResult = (ReliableChannelBinder<TChannel>.CloseAsyncResult)result.AsyncState;
					bool flag;
					Exception ex;
					try
					{
						flag = closeAsyncResult.CompleteChannelClose(false, result);
						ex = null;
					}
					catch (Exception ex2)
					{
						if (Fx.IsFatal(ex2))
						{
							throw;
						}
						flag = true;
						ex = ex2;
					}
					if (flag)
					{
						if (ex != null)
						{
							ex = closeAsyncResult.HandleAsyncException(ex);
						}
						closeAsyncResult.Complete(false, ex);
					}
				}
			}

			// Token: 0x040048F4 RID: 18676
			private ReliableChannelBinder<TChannel> binder;

			// Token: 0x040048F5 RID: 18677
			private TChannel channel;

			// Token: 0x040048F6 RID: 18678
			private MaskingMode maskingMode;

			// Token: 0x040048F7 RID: 18679
			private static AsyncCallback onBinderCloseComplete = Fx.ThunkCallback(new AsyncCallback(ReliableChannelBinder<TChannel>.CloseAsyncResult.OnBinderCloseComplete));

			// Token: 0x040048F8 RID: 18680
			private static AsyncCallback onChannelCloseComplete = Fx.ThunkCallback(new AsyncCallback(ReliableChannelBinder<TChannel>.CloseAsyncResult.OnChannelCloseComplete));

			// Token: 0x040048F9 RID: 18681
			private TimeoutHelper timeoutHelper;
		}

		// Token: 0x02000DBB RID: 3515
		protected abstract class InputAsyncResult<TBinder> : AsyncResult where TBinder : ReliableChannelBinder<TChannel>
		{
			// Token: 0x06007F96 RID: 32662 RVA: 0x001DAB82 File Offset: 0x001D8D82
			public InputAsyncResult(TBinder binder, bool canGetChannel, TimeSpan timeout, MaskingMode maskingMode, AsyncCallback callback, object state) : base(callback, state)
			{
				this.binder = binder;
				this.canGetChannel = canGetChannel;
				this.timeoutHelper = new TimeoutHelper(timeout);
				this.maskingMode = maskingMode;
			}

			// Token: 0x06007F97 RID: 32663
			protected abstract IAsyncResult BeginInput(TBinder binder, TChannel channel, TimeSpan timeout, AsyncCallback callback, object state);

			// Token: 0x06007F98 RID: 32664 RVA: 0x001DABB8 File Offset: 0x001D8DB8
			private bool CompleteInput(IAsyncResult result)
			{
				bool flag;
				try
				{
					this.success = this.EndInput(this.binder, this.channel, result, out flag);
				}
				finally
				{
					this.autoAborted = this.binder.Synchronizer.Aborting;
					this.binder.synchronizer.ReturnChannel();
				}
				return !flag;
			}

			// Token: 0x06007F99 RID: 32665 RVA: 0x001DAC28 File Offset: 0x001D8E28
			private bool CompleteTryGetChannel(IAsyncResult result, out bool complete)
			{
				complete = false;
				this.success = this.binder.synchronizer.EndTryGetChannel(result, out this.channel);
				if (this.channel == null)
				{
					complete = true;
					return false;
				}
				bool flag = true;
				IAsyncResult asyncResult = null;
				try
				{
					asyncResult = this.BeginInput(this.binder, this.channel, this.timeoutHelper.RemainingTime(), ReliableChannelBinder<TChannel>.InputAsyncResult<TBinder>.onInputComplete, this);
					flag = false;
				}
				finally
				{
					if (flag)
					{
						this.autoAborted = this.binder.Synchronizer.Aborting;
						this.binder.synchronizer.ReturnChannel();
					}
				}
				if (!asyncResult.CompletedSynchronously)
				{
					complete = false;
					return false;
				}
				if (this.CompleteInput(asyncResult))
				{
					complete = false;
					return true;
				}
				complete = true;
				return false;
			}

			// Token: 0x06007F9A RID: 32666 RVA: 0x001DACFC File Offset: 0x001D8EFC
			public bool End()
			{
				AsyncResult.End<ReliableChannelBinder<TChannel>.InputAsyncResult<TBinder>>(this);
				return this.success;
			}

			// Token: 0x06007F9B RID: 32667
			protected abstract bool EndInput(TBinder binder, TChannel channel, IAsyncResult result, out bool complete);

			// Token: 0x06007F9C RID: 32668 RVA: 0x001DAD0C File Offset: 0x001D8F0C
			private void OnInputComplete(IAsyncResult result)
			{
				this.isSynchronous = false;
				Exception exception = null;
				bool flag;
				try
				{
					flag = this.CompleteInput(result);
				}
				catch (Exception ex)
				{
					if (Fx.IsFatal(ex))
					{
						throw;
					}
					if (!this.binder.HandleException(ex, this.maskingMode, this.autoAborted))
					{
						exception = ex;
						flag = false;
					}
					else
					{
						flag = true;
					}
				}
				if (flag)
				{
					this.StartOnNonUserThread();
					return;
				}
				base.Complete(this.isSynchronous, exception);
			}

			// Token: 0x06007F9D RID: 32669 RVA: 0x001DAD88 File Offset: 0x001D8F88
			private static void OnInputCompleteStatic(IAsyncResult result)
			{
				if (!result.CompletedSynchronously)
				{
					ReliableChannelBinder<TChannel>.InputAsyncResult<TBinder> inputAsyncResult = (ReliableChannelBinder<TChannel>.InputAsyncResult<TBinder>)result.AsyncState;
					inputAsyncResult.OnInputComplete(result);
				}
			}

			// Token: 0x06007F9E RID: 32670 RVA: 0x001DADB0 File Offset: 0x001D8FB0
			private void OnTryGetChannelComplete(IAsyncResult result)
			{
				this.isSynchronous = false;
				bool flag = false;
				bool flag2 = false;
				Exception ex = null;
				try
				{
					flag = this.CompleteTryGetChannel(result, out flag2);
				}
				catch (Exception ex2)
				{
					if (Fx.IsFatal(ex2))
					{
						throw;
					}
					if (!this.binder.HandleException(ex2, this.maskingMode, this.autoAborted))
					{
						ex = ex2;
						flag = false;
					}
					else
					{
						flag = true;
					}
				}
				if (flag2 && flag)
				{
					throw Fx.AssertAndThrow("The derived class' implementation of CompleteTryGetChannel() cannot indicate that the asynchronous operation should complete and retry.");
				}
				if (flag)
				{
					this.StartOnNonUserThread();
					return;
				}
				if (flag2 || ex != null)
				{
					base.Complete(this.isSynchronous, ex);
				}
			}

			// Token: 0x06007F9F RID: 32671 RVA: 0x001DAE48 File Offset: 0x001D9048
			private static void OnTryGetChannelCompleteStatic(IAsyncResult result)
			{
				if (!result.CompletedSynchronously)
				{
					ReliableChannelBinder<TChannel>.InputAsyncResult<TBinder> inputAsyncResult = (ReliableChannelBinder<TChannel>.InputAsyncResult<TBinder>)result.AsyncState;
					inputAsyncResult.OnTryGetChannelComplete(result);
				}
			}

			// Token: 0x06007FA0 RID: 32672 RVA: 0x001DAE70 File Offset: 0x001D9070
			protected bool Start()
			{
				for (;;)
				{
					bool flag = false;
					bool flag2 = false;
					this.autoAborted = false;
					try
					{
						IAsyncResult asyncResult = this.binder.synchronizer.BeginTryGetChannelForInput(this.canGetChannel, this.timeoutHelper.RemainingTime(), ReliableChannelBinder<TChannel>.InputAsyncResult<TBinder>.onTryGetChannelComplete, this);
						if (asyncResult.CompletedSynchronously)
						{
							flag = this.CompleteTryGetChannel(asyncResult, out flag2);
						}
					}
					catch (Exception ex)
					{
						if (Fx.IsFatal(ex))
						{
							throw;
						}
						if (!this.binder.HandleException(ex, this.maskingMode, this.autoAborted))
						{
							throw;
						}
						flag = true;
					}
					if (flag2 && flag)
					{
						break;
					}
					if (!flag)
					{
						return flag2;
					}
				}
				throw Fx.AssertAndThrow("The derived class' implementation of CompleteTryGetChannel() cannot indicate that the asynchronous operation should complete and retry.");
			}

			// Token: 0x06007FA1 RID: 32673 RVA: 0x001DAF20 File Offset: 0x001D9120
			private void StartOnNonUserThread()
			{
				bool flag = false;
				Exception ex = null;
				try
				{
					flag = this.Start();
				}
				catch (Exception ex2)
				{
					if (Fx.IsFatal(ex2))
					{
						throw;
					}
					ex = ex2;
				}
				if (flag || ex != null)
				{
					base.Complete(false, ex);
				}
			}

			// Token: 0x040048FA RID: 18682
			private bool autoAborted;

			// Token: 0x040048FB RID: 18683
			private TBinder binder;

			// Token: 0x040048FC RID: 18684
			private bool canGetChannel;

			// Token: 0x040048FD RID: 18685
			private TChannel channel;

			// Token: 0x040048FE RID: 18686
			private bool isSynchronous = true;

			// Token: 0x040048FF RID: 18687
			private MaskingMode maskingMode;

			// Token: 0x04004900 RID: 18688
			private static AsyncCallback onInputComplete = Fx.ThunkCallback(new AsyncCallback(ReliableChannelBinder<TChannel>.InputAsyncResult<TBinder>.OnInputCompleteStatic));

			// Token: 0x04004901 RID: 18689
			private static AsyncCallback onTryGetChannelComplete = Fx.ThunkCallback(new AsyncCallback(ReliableChannelBinder<TChannel>.InputAsyncResult<TBinder>.OnTryGetChannelCompleteStatic));

			// Token: 0x04004902 RID: 18690
			private bool success;

			// Token: 0x04004903 RID: 18691
			private TimeoutHelper timeoutHelper;
		}

		// Token: 0x02000DBC RID: 3516
		private sealed class MessageRequestContext : ReliableChannelBinder<TChannel>.BinderRequestContext
		{
			// Token: 0x06007FA3 RID: 32675 RVA: 0x001DAF96 File Offset: 0x001D9196
			public MessageRequestContext(ReliableChannelBinder<TChannel> binder, Message message) : base(binder, message)
			{
			}

			// Token: 0x06007FA4 RID: 32676 RVA: 0x001DAFA0 File Offset: 0x001D91A0
			protected override void OnAbort()
			{
			}

			// Token: 0x06007FA5 RID: 32677 RVA: 0x001DAFA2 File Offset: 0x001D91A2
			protected override void OnClose(TimeSpan timeout)
			{
			}

			// Token: 0x06007FA6 RID: 32678 RVA: 0x001DAFA4 File Offset: 0x001D91A4
			protected override IAsyncResult OnBeginReply(Message message, TimeSpan timeout, AsyncCallback callback, object state)
			{
				return new ReliableChannelBinder<TChannel>.MessageRequestContext.ReplyAsyncResult(this, message, timeout, callback, state);
			}

			// Token: 0x06007FA7 RID: 32679 RVA: 0x001DAFB1 File Offset: 0x001D91B1
			protected override void OnEndReply(IAsyncResult result)
			{
				ReliableChannelBinder<TChannel>.MessageRequestContext.ReplyAsyncResult.End(result);
			}

			// Token: 0x06007FA8 RID: 32680 RVA: 0x001DAFB9 File Offset: 0x001D91B9
			protected override void OnReply(Message message, TimeSpan timeout)
			{
				if (message != null)
				{
					base.Binder.Send(message, timeout, base.MaskingMode);
				}
			}

			// Token: 0x02000F76 RID: 3958
			private class ReplyAsyncResult : AsyncResult
			{
				// Token: 0x060087E3 RID: 34787 RVA: 0x001F91F8 File Offset: 0x001F73F8
				public ReplyAsyncResult(ReliableChannelBinder<TChannel>.MessageRequestContext context, Message message, TimeSpan timeout, AsyncCallback callback, object state) : base(callback, state)
				{
					if (message != null)
					{
						if (ReliableChannelBinder<TChannel>.MessageRequestContext.ReplyAsyncResult.onSend == null)
						{
							ReliableChannelBinder<TChannel>.MessageRequestContext.ReplyAsyncResult.onSend = Fx.ThunkCallback(new AsyncCallback(ReliableChannelBinder<TChannel>.MessageRequestContext.ReplyAsyncResult.OnSend));
						}
						this.context = context;
						IAsyncResult asyncResult = context.Binder.BeginSend(message, timeout, context.MaskingMode, ReliableChannelBinder<TChannel>.MessageRequestContext.ReplyAsyncResult.onSend, this);
						if (!asyncResult.CompletedSynchronously)
						{
							return;
						}
						context.Binder.EndSend(asyncResult);
					}
					base.Complete(true);
				}

				// Token: 0x060087E4 RID: 34788 RVA: 0x001F926C File Offset: 0x001F746C
				public static void End(IAsyncResult result)
				{
					AsyncResult.End<ReliableChannelBinder<TChannel>.MessageRequestContext.ReplyAsyncResult>(result);
				}

				// Token: 0x060087E5 RID: 34789 RVA: 0x001F9278 File Offset: 0x001F7478
				private static void OnSend(IAsyncResult result)
				{
					if (result.CompletedSynchronously)
					{
						return;
					}
					Exception exception = null;
					ReliableChannelBinder<TChannel>.MessageRequestContext.ReplyAsyncResult replyAsyncResult = (ReliableChannelBinder<TChannel>.MessageRequestContext.ReplyAsyncResult)result.AsyncState;
					try
					{
						replyAsyncResult.context.Binder.EndSend(result);
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

				// Token: 0x04004F45 RID: 20293
				private static AsyncCallback onSend;

				// Token: 0x04004F46 RID: 20294
				private ReliableChannelBinder<TChannel>.MessageRequestContext context;
			}
		}

		// Token: 0x02000DBD RID: 3517
		protected abstract class OutputAsyncResult<TBinder> : AsyncResult where TBinder : ReliableChannelBinder<TChannel>
		{
			// Token: 0x06007FA9 RID: 32681 RVA: 0x001DAFD1 File Offset: 0x001D91D1
			public OutputAsyncResult(TBinder binder, AsyncCallback callback, object state) : base(callback, state)
			{
				this.binder = binder;
			}

			// Token: 0x17001C66 RID: 7270
			// (get) Token: 0x06007FAA RID: 32682 RVA: 0x001DAFE2 File Offset: 0x001D91E2
			public MaskingMode MaskingMode
			{
				get
				{
					return this.maskingMode;
				}
			}

			// Token: 0x06007FAB RID: 32683
			protected abstract IAsyncResult BeginOutput(TBinder binder, TChannel channel, Message message, TimeSpan timeout, MaskingMode maskingMode, AsyncCallback callback, object state);

			// Token: 0x06007FAC RID: 32684 RVA: 0x001DAFEA File Offset: 0x001D91EA
			private void Cleanup()
			{
				if (this.hasChannel)
				{
					this.autoAborted = this.binder.Synchronizer.Aborting;
					this.binder.synchronizer.ReturnChannel();
				}
			}

			// Token: 0x06007FAD RID: 32685 RVA: 0x001DB024 File Offset: 0x001D9224
			private bool CompleteTryGetChannel(IAsyncResult result)
			{
				bool flag = !this.binder.synchronizer.EndTryGetChannel(result, out this.channel);
				if (flag || this.channel == null)
				{
					this.Cleanup();
					if (flag && !ReliableChannelBinderHelper.MaskHandled(this.maskingMode))
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new TimeoutException(this.GetTimeoutString(this.timeout)));
					}
					return true;
				}
				else
				{
					this.hasChannel = true;
					result = this.BeginOutput(this.binder, this.channel, this.message, this.timeoutHelper.RemainingTime(), this.maskingMode, ReliableChannelBinder<TChannel>.OutputAsyncResult<TBinder>.onOutputComplete, this);
					if (result.CompletedSynchronously)
					{
						this.EndOutput(this.binder, this.channel, this.maskingMode, result);
						this.Cleanup();
						return true;
					}
					return false;
				}
			}

			// Token: 0x06007FAE RID: 32686
			protected abstract void EndOutput(TBinder binder, TChannel channel, MaskingMode maskingMode, IAsyncResult result);

			// Token: 0x06007FAF RID: 32687
			protected abstract string GetTimeoutString(TimeSpan timeout);

			// Token: 0x06007FB0 RID: 32688 RVA: 0x001DB0F8 File Offset: 0x001D92F8
			private void OnOutputComplete(IAsyncResult result)
			{
				if (!result.CompletedSynchronously)
				{
					bool flag = false;
					Exception exception = null;
					try
					{
						this.EndOutput(this.binder, this.channel, this.maskingMode, result);
						flag = true;
						this.Cleanup();
					}
					catch (Exception ex)
					{
						if (Fx.IsFatal(ex))
						{
							throw;
						}
						if (!flag)
						{
							this.Cleanup();
						}
						if (!this.binder.HandleException(ex, this.maskingMode, this.autoAborted))
						{
							exception = ex;
						}
					}
					base.Complete(false, exception);
				}
			}

			// Token: 0x06007FB1 RID: 32689 RVA: 0x001DB184 File Offset: 0x001D9384
			private static void OnOutputCompleteStatic(IAsyncResult result)
			{
				ReliableChannelBinder<TChannel>.OutputAsyncResult<TBinder> outputAsyncResult = (ReliableChannelBinder<TChannel>.OutputAsyncResult<TBinder>)result.AsyncState;
				outputAsyncResult.OnOutputComplete(result);
			}

			// Token: 0x06007FB2 RID: 32690 RVA: 0x001DB1A4 File Offset: 0x001D93A4
			private void OnTryGetChannelComplete(IAsyncResult result)
			{
				if (!result.CompletedSynchronously)
				{
					bool flag = false;
					Exception exception = null;
					try
					{
						flag = this.CompleteTryGetChannel(result);
					}
					catch (Exception ex)
					{
						if (Fx.IsFatal(ex))
						{
							throw;
						}
						this.Cleanup();
						flag = true;
						if (!this.binder.HandleException(ex, this.maskingMode, this.autoAborted))
						{
							exception = ex;
						}
					}
					if (flag)
					{
						base.Complete(false, exception);
					}
				}
			}

			// Token: 0x06007FB3 RID: 32691 RVA: 0x001DB218 File Offset: 0x001D9418
			private static void OnTryGetChannelCompleteStatic(IAsyncResult result)
			{
				ReliableChannelBinder<TChannel>.OutputAsyncResult<TBinder> outputAsyncResult = (ReliableChannelBinder<TChannel>.OutputAsyncResult<TBinder>)result.AsyncState;
				outputAsyncResult.OnTryGetChannelComplete(result);
			}

			// Token: 0x06007FB4 RID: 32692 RVA: 0x001DB238 File Offset: 0x001D9438
			public void Start(Message message, TimeSpan timeout, MaskingMode maskingMode)
			{
				if (!this.binder.ValidateOutputOperation(message, timeout, maskingMode))
				{
					base.Complete(true);
					return;
				}
				this.message = message;
				this.timeout = timeout;
				this.timeoutHelper = new TimeoutHelper(timeout);
				this.maskingMode = maskingMode;
				bool flag = false;
				try
				{
					IAsyncResult asyncResult = this.binder.synchronizer.BeginTryGetChannelForOutput(this.timeoutHelper.RemainingTime(), this.maskingMode, ReliableChannelBinder<TChannel>.OutputAsyncResult<TBinder>.onTryGetChannelComplete, this);
					if (asyncResult.CompletedSynchronously)
					{
						flag = this.CompleteTryGetChannel(asyncResult);
					}
				}
				catch (Exception ex)
				{
					if (Fx.IsFatal(ex))
					{
						throw;
					}
					this.Cleanup();
					if (!this.binder.HandleException(ex, this.maskingMode, this.autoAborted))
					{
						throw;
					}
					flag = true;
				}
				if (flag)
				{
					base.Complete(true);
				}
			}

			// Token: 0x04004904 RID: 18692
			private bool autoAborted;

			// Token: 0x04004905 RID: 18693
			private TBinder binder;

			// Token: 0x04004906 RID: 18694
			private TChannel channel;

			// Token: 0x04004907 RID: 18695
			private bool hasChannel;

			// Token: 0x04004908 RID: 18696
			private MaskingMode maskingMode;

			// Token: 0x04004909 RID: 18697
			private Message message;

			// Token: 0x0400490A RID: 18698
			private static AsyncCallback onTryGetChannelComplete = Fx.ThunkCallback(new AsyncCallback(ReliableChannelBinder<TChannel>.OutputAsyncResult<TBinder>.OnTryGetChannelCompleteStatic));

			// Token: 0x0400490B RID: 18699
			private static AsyncCallback onOutputComplete = Fx.ThunkCallback(new AsyncCallback(ReliableChannelBinder<TChannel>.OutputAsyncResult<TBinder>.OnOutputCompleteStatic));

			// Token: 0x0400490C RID: 18700
			private TimeSpan timeout;

			// Token: 0x0400490D RID: 18701
			private TimeoutHelper timeoutHelper;
		}

		// Token: 0x02000DBE RID: 3518
		private sealed class RequestRequestContext : ReliableChannelBinder<TChannel>.BinderRequestContext
		{
			// Token: 0x06007FB6 RID: 32694 RVA: 0x001DB346 File Offset: 0x001D9546
			public RequestRequestContext(ReliableChannelBinder<TChannel> binder, RequestContext innerContext, Message message) : base(binder, message)
			{
				if (binder.defaultMaskingMode != MaskingMode.All && !binder.TolerateFaults)
				{
					throw Fx.AssertAndThrow("This request context is designed to catch exceptions. Thus it cannot be used if the caller expects no exception handling.");
				}
				if (innerContext == null)
				{
					throw Fx.AssertAndThrow("Argument innerContext cannot be null.");
				}
				this.innerContext = innerContext;
			}

			// Token: 0x06007FB7 RID: 32695 RVA: 0x001DB381 File Offset: 0x001D9581
			protected override void OnAbort()
			{
				this.innerContext.Abort();
			}

			// Token: 0x06007FB8 RID: 32696 RVA: 0x001DB390 File Offset: 0x001D9590
			protected override IAsyncResult OnBeginReply(Message message, TimeSpan timeout, AsyncCallback callback, object state)
			{
				try
				{
					if (message != null)
					{
						base.Binder.AddOutputHeaders(message);
					}
					return this.innerContext.BeginReply(message, timeout, callback, state);
				}
				catch (ObjectDisposedException)
				{
				}
				catch (Exception ex)
				{
					if (Fx.IsFatal(ex))
					{
						throw;
					}
					if (!base.Binder.HandleException(ex, base.MaskingMode))
					{
						throw;
					}
					this.innerContext.Abort();
				}
				return new ReliableChannelBinder<TChannel>.BinderCompletedAsyncResult(callback, state);
			}

			// Token: 0x06007FB9 RID: 32697 RVA: 0x001DB418 File Offset: 0x001D9618
			protected override void OnClose(TimeSpan timeout)
			{
				try
				{
					this.innerContext.Close(timeout);
				}
				catch (ObjectDisposedException)
				{
				}
				catch (Exception ex)
				{
					if (Fx.IsFatal(ex))
					{
						throw;
					}
					if (!base.Binder.HandleException(ex, base.MaskingMode))
					{
						throw;
					}
					this.innerContext.Abort();
				}
			}

			// Token: 0x06007FBA RID: 32698 RVA: 0x001DB480 File Offset: 0x001D9680
			protected override void OnEndReply(IAsyncResult result)
			{
				ReliableChannelBinder<TChannel>.BinderCompletedAsyncResult binderCompletedAsyncResult = result as ReliableChannelBinder<TChannel>.BinderCompletedAsyncResult;
				if (binderCompletedAsyncResult != null)
				{
					binderCompletedAsyncResult.End();
					return;
				}
				try
				{
					this.innerContext.EndReply(result);
				}
				catch (ObjectDisposedException)
				{
				}
				catch (Exception ex)
				{
					if (Fx.IsFatal(ex))
					{
						throw;
					}
					if (!base.Binder.HandleException(ex, base.MaskingMode))
					{
						throw;
					}
					this.innerContext.Abort();
				}
			}

			// Token: 0x06007FBB RID: 32699 RVA: 0x001DB4FC File Offset: 0x001D96FC
			protected override void OnReply(Message message, TimeSpan timeout)
			{
				try
				{
					if (message != null)
					{
						base.Binder.AddOutputHeaders(message);
					}
					this.innerContext.Reply(message, timeout);
				}
				catch (ObjectDisposedException)
				{
				}
				catch (Exception ex)
				{
					if (Fx.IsFatal(ex))
					{
						throw;
					}
					if (!base.Binder.HandleException(ex, base.MaskingMode))
					{
						throw;
					}
					this.innerContext.Abort();
				}
			}

			// Token: 0x0400490E RID: 18702
			private RequestContext innerContext;
		}

		// Token: 0x02000DBF RID: 3519
		private sealed class SendAsyncResult : ReliableChannelBinder<TChannel>.OutputAsyncResult<ReliableChannelBinder<TChannel>>
		{
			// Token: 0x06007FBC RID: 32700 RVA: 0x001DB574 File Offset: 0x001D9774
			public SendAsyncResult(ReliableChannelBinder<TChannel> binder, AsyncCallback callback, object state) : base(binder, callback, state)
			{
			}

			// Token: 0x06007FBD RID: 32701 RVA: 0x001DB57F File Offset: 0x001D977F
			protected override IAsyncResult BeginOutput(ReliableChannelBinder<TChannel> binder, TChannel channel, Message message, TimeSpan timeout, MaskingMode maskingMode, AsyncCallback callback, object state)
			{
				binder.AddOutputHeaders(message);
				return binder.OnBeginSend(channel, message, timeout, callback, state);
			}

			// Token: 0x06007FBE RID: 32702 RVA: 0x001DB596 File Offset: 0x001D9796
			public static void End(IAsyncResult result)
			{
				AsyncResult.End<ReliableChannelBinder<TChannel>.SendAsyncResult>(result);
			}

			// Token: 0x06007FBF RID: 32703 RVA: 0x001DB59F File Offset: 0x001D979F
			protected override void EndOutput(ReliableChannelBinder<TChannel> binder, TChannel channel, MaskingMode maskingMode, IAsyncResult result)
			{
				binder.OnEndSend(channel, result);
			}

			// Token: 0x06007FC0 RID: 32704 RVA: 0x001DB5AA File Offset: 0x001D97AA
			protected override string GetTimeoutString(TimeSpan timeout)
			{
				return SR.GetString("TimeoutOnSend", new object[]
				{
					timeout
				});
			}
		}

		// Token: 0x02000DC0 RID: 3520
		private sealed class TryReceiveAsyncResult : ReliableChannelBinder<TChannel>.InputAsyncResult<ReliableChannelBinder<TChannel>>
		{
			// Token: 0x06007FC1 RID: 32705 RVA: 0x001DB5C5 File Offset: 0x001D97C5
			public TryReceiveAsyncResult(ReliableChannelBinder<TChannel> binder, TimeSpan timeout, MaskingMode maskingMode, AsyncCallback callback, object state) : base(binder, binder.CanGetChannelForReceive, timeout, maskingMode, callback, state)
			{
				if (base.Start())
				{
					base.Complete(true);
				}
			}

			// Token: 0x06007FC2 RID: 32706 RVA: 0x001DB5E9 File Offset: 0x001D97E9
			protected override IAsyncResult BeginInput(ReliableChannelBinder<TChannel> binder, TChannel channel, TimeSpan timeout, AsyncCallback callback, object state)
			{
				return binder.OnBeginTryReceive(channel, timeout, callback, state);
			}

			// Token: 0x06007FC3 RID: 32707 RVA: 0x001DB5F7 File Offset: 0x001D97F7
			public bool End(out RequestContext requestContext)
			{
				requestContext = this.requestContext;
				return base.End();
			}

			// Token: 0x06007FC4 RID: 32708 RVA: 0x001DB608 File Offset: 0x001D9808
			protected override bool EndInput(ReliableChannelBinder<TChannel> binder, TChannel channel, IAsyncResult result, out bool complete)
			{
				bool flag = binder.OnEndTryReceive(channel, result, out this.requestContext);
				complete = (!flag || this.requestContext != null);
				if (!complete)
				{
					binder.synchronizer.OnReadEof();
				}
				return flag;
			}

			// Token: 0x0400490F RID: 18703
			private RequestContext requestContext;
		}
	}
}
