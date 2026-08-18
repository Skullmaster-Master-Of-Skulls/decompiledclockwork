using System;
using System.Runtime;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000745 RID: 1861
	internal class InputChannel : InputQueueChannel<Message>, IInputChannel, IChannel, ICommunicationObject
	{
		// Token: 0x06004712 RID: 18194 RVA: 0x00108C8B File Offset: 0x00106E8B
		public InputChannel(ChannelManagerBase channelManager, EndpointAddress localAddress) : base(channelManager)
		{
			this.localAddress = localAddress;
		}

		// Token: 0x17001210 RID: 4624
		// (get) Token: 0x06004713 RID: 18195 RVA: 0x00108C9B File Offset: 0x00106E9B
		public EndpointAddress LocalAddress
		{
			get
			{
				return this.localAddress;
			}
		}

		// Token: 0x06004714 RID: 18196 RVA: 0x00108CA4 File Offset: 0x00106EA4
		public override T GetProperty<T>()
		{
			if (typeof(T) == typeof(IInputChannel))
			{
				return (T)((object)this);
			}
			T property = base.GetProperty<T>();
			if (property != null)
			{
				return property;
			}
			return default(T);
		}

		// Token: 0x06004715 RID: 18197 RVA: 0x00108CED File Offset: 0x00106EED
		protected override IAsyncResult OnBeginOpen(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return new CompletedAsyncResult(callback, state);
		}

		// Token: 0x06004716 RID: 18198 RVA: 0x00108CF6 File Offset: 0x00106EF6
		protected override void OnEndOpen(IAsyncResult result)
		{
			CompletedAsyncResult.End(result);
		}

		// Token: 0x06004717 RID: 18199 RVA: 0x00108CFE File Offset: 0x00106EFE
		protected override void OnOpen(TimeSpan timeout)
		{
		}

		// Token: 0x06004718 RID: 18200 RVA: 0x00108D00 File Offset: 0x00106F00
		public virtual Message Receive()
		{
			return this.Receive(base.DefaultReceiveTimeout);
		}

		// Token: 0x06004719 RID: 18201 RVA: 0x00108D10 File Offset: 0x00106F10
		public virtual Message Receive(TimeSpan timeout)
		{
			if (timeout < TimeSpan.Zero)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("timeout", timeout, SR.GetString("SFxTimeoutOutOfRange0")));
			}
			base.ThrowPending();
			return InputChannel.HelpReceive(this, timeout);
		}

		// Token: 0x0600471A RID: 18202 RVA: 0x00108D5C File Offset: 0x00106F5C
		public virtual IAsyncResult BeginReceive(AsyncCallback callback, object state)
		{
			return this.BeginReceive(base.DefaultReceiveTimeout, callback, state);
		}

		// Token: 0x0600471B RID: 18203 RVA: 0x00108D6C File Offset: 0x00106F6C
		public virtual IAsyncResult BeginReceive(TimeSpan timeout, AsyncCallback callback, object state)
		{
			if (timeout < TimeSpan.Zero)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("timeout", timeout, SR.GetString("SFxTimeoutOutOfRange0")));
			}
			base.ThrowPending();
			return InputChannel.HelpBeginReceive(this, timeout, callback, state);
		}

		// Token: 0x0600471C RID: 18204 RVA: 0x00108DBA File Offset: 0x00106FBA
		public Message EndReceive(IAsyncResult result)
		{
			return InputChannel.HelpEndReceive(result);
		}

		// Token: 0x0600471D RID: 18205 RVA: 0x00108DC4 File Offset: 0x00106FC4
		public virtual bool TryReceive(TimeSpan timeout, out Message message)
		{
			if (timeout < TimeSpan.Zero)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("timeout", timeout, SR.GetString("SFxTimeoutOutOfRange0")));
			}
			base.ThrowPending();
			return base.Dequeue(timeout, out message);
		}

		// Token: 0x0600471E RID: 18206 RVA: 0x00108E14 File Offset: 0x00107014
		public virtual IAsyncResult BeginTryReceive(TimeSpan timeout, AsyncCallback callback, object state)
		{
			if (timeout < TimeSpan.Zero)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("timeout", timeout, SR.GetString("SFxTimeoutOutOfRange0")));
			}
			base.ThrowPending();
			return base.BeginDequeue(timeout, callback, state);
		}

		// Token: 0x0600471F RID: 18207 RVA: 0x00108E62 File Offset: 0x00107062
		public virtual bool EndTryReceive(IAsyncResult result, out Message message)
		{
			return base.EndDequeue(result, out message);
		}

		// Token: 0x06004720 RID: 18208 RVA: 0x00108E6C File Offset: 0x0010706C
		public bool WaitForMessage(TimeSpan timeout)
		{
			if (timeout < TimeSpan.Zero)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("timeout", timeout, SR.GetString("SFxTimeoutOutOfRange0")));
			}
			base.ThrowPending();
			return base.WaitForItem(timeout);
		}

		// Token: 0x06004721 RID: 18209 RVA: 0x00108EB8 File Offset: 0x001070B8
		public IAsyncResult BeginWaitForMessage(TimeSpan timeout, AsyncCallback callback, object state)
		{
			if (timeout < TimeSpan.Zero)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("timeout", timeout, SR.GetString("SFxTimeoutOutOfRange0")));
			}
			base.ThrowPending();
			return base.BeginWaitForItem(timeout, callback, state);
		}

		// Token: 0x06004722 RID: 18210 RVA: 0x00108F06 File Offset: 0x00107106
		public bool EndWaitForMessage(IAsyncResult result)
		{
			return base.EndWaitForItem(result);
		}

		// Token: 0x06004723 RID: 18211 RVA: 0x00108F10 File Offset: 0x00107110
		internal static Message HelpReceive(IInputChannel channel, TimeSpan timeout)
		{
			Message result;
			if (channel.TryReceive(timeout, out result))
			{
				return result;
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(InputChannel.CreateReceiveTimedOutException(channel, timeout));
		}

		// Token: 0x06004724 RID: 18212 RVA: 0x00108F3B File Offset: 0x0010713B
		internal static IAsyncResult HelpBeginReceive(IInputChannel channel, TimeSpan timeout, AsyncCallback callback, object state)
		{
			return new InputChannel.HelpReceiveAsyncResult(channel, timeout, callback, state);
		}

		// Token: 0x06004725 RID: 18213 RVA: 0x00108F46 File Offset: 0x00107146
		internal static Message HelpEndReceive(IAsyncResult result)
		{
			return InputChannel.HelpReceiveAsyncResult.End(result);
		}

		// Token: 0x06004726 RID: 18214 RVA: 0x00108F50 File Offset: 0x00107150
		private static Exception CreateReceiveTimedOutException(IInputChannel channel, TimeSpan timeout)
		{
			if (channel.LocalAddress != null)
			{
				return new TimeoutException(SR.GetString("ReceiveTimedOut", new object[]
				{
					channel.LocalAddress.Uri.AbsoluteUri,
					timeout
				}));
			}
			return new TimeoutException(SR.GetString("ReceiveTimedOutNoLocalAddress", new object[]
			{
				timeout
			}));
		}

		// Token: 0x04002DA7 RID: 11687
		private EndpointAddress localAddress;

		// Token: 0x02000CD7 RID: 3287
		private class HelpReceiveAsyncResult : AsyncResult
		{
			// Token: 0x060079E5 RID: 31205 RVA: 0x001C69F4 File Offset: 0x001C4BF4
			public HelpReceiveAsyncResult(IInputChannel channel, TimeSpan timeout, AsyncCallback callback, object state) : base(callback, state)
			{
				this.channel = channel;
				this.timeout = timeout;
				IAsyncResult asyncResult = channel.BeginTryReceive(timeout, InputChannel.HelpReceiveAsyncResult.onReceive, this);
				if (!asyncResult.CompletedSynchronously)
				{
					return;
				}
				this.HandleReceiveComplete(asyncResult);
				base.Complete(true);
			}

			// Token: 0x060079E6 RID: 31206 RVA: 0x001C6A40 File Offset: 0x001C4C40
			public static Message End(IAsyncResult result)
			{
				InputChannel.HelpReceiveAsyncResult helpReceiveAsyncResult = AsyncResult.End<InputChannel.HelpReceiveAsyncResult>(result);
				return helpReceiveAsyncResult.message;
			}

			// Token: 0x060079E7 RID: 31207 RVA: 0x001C6A5A File Offset: 0x001C4C5A
			private void HandleReceiveComplete(IAsyncResult result)
			{
				if (!this.channel.EndTryReceive(result, out this.message))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(InputChannel.CreateReceiveTimedOutException(this.channel, this.timeout));
				}
			}

			// Token: 0x060079E8 RID: 31208 RVA: 0x001C6A8C File Offset: 0x001C4C8C
			private static void OnReceive(IAsyncResult result)
			{
				if (result.CompletedSynchronously)
				{
					return;
				}
				InputChannel.HelpReceiveAsyncResult helpReceiveAsyncResult = (InputChannel.HelpReceiveAsyncResult)result.AsyncState;
				Exception exception = null;
				try
				{
					helpReceiveAsyncResult.HandleReceiveComplete(result);
				}
				catch (Exception ex)
				{
					if (Fx.IsFatal(ex))
					{
						throw;
					}
					exception = ex;
				}
				helpReceiveAsyncResult.Complete(false, exception);
			}

			// Token: 0x040045BC RID: 17852
			private IInputChannel channel;

			// Token: 0x040045BD RID: 17853
			private TimeSpan timeout;

			// Token: 0x040045BE RID: 17854
			private static AsyncCallback onReceive = Fx.ThunkCallback(new AsyncCallback(InputChannel.HelpReceiveAsyncResult.OnReceive));

			// Token: 0x040045BF RID: 17855
			private Message message;
		}
	}
}
