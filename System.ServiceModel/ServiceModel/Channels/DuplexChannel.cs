using System;
using System.Runtime;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000743 RID: 1859
	internal abstract class DuplexChannel : InputQueueChannel<Message>, IDuplexChannel, IInputChannel, IChannel, ICommunicationObject, IOutputChannel
	{
		// Token: 0x060046F3 RID: 18163 RVA: 0x00108900 File Offset: 0x00106B00
		protected DuplexChannel(ChannelManagerBase channelManager, EndpointAddress localAddress) : base(channelManager)
		{
			this.localAddress = localAddress;
		}

		// Token: 0x1700120D RID: 4621
		// (get) Token: 0x060046F4 RID: 18164 RVA: 0x00108910 File Offset: 0x00106B10
		public virtual EndpointAddress LocalAddress
		{
			get
			{
				return this.localAddress;
			}
		}

		// Token: 0x1700120E RID: 4622
		// (get) Token: 0x060046F5 RID: 18165
		public abstract EndpointAddress RemoteAddress { get; }

		// Token: 0x1700120F RID: 4623
		// (get) Token: 0x060046F6 RID: 18166
		public abstract Uri Via { get; }

		// Token: 0x060046F7 RID: 18167 RVA: 0x00108918 File Offset: 0x00106B18
		public IAsyncResult BeginSend(Message message, AsyncCallback callback, object state)
		{
			return this.BeginSend(message, base.DefaultSendTimeout, callback, state);
		}

		// Token: 0x060046F8 RID: 18168 RVA: 0x0010892C File Offset: 0x00106B2C
		public IAsyncResult BeginSend(Message message, TimeSpan timeout, AsyncCallback callback, object state)
		{
			if (message == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("message");
			}
			if (timeout < TimeSpan.Zero)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("timeout", timeout, SR.GetString("SFxTimeoutOutOfRange0")));
			}
			base.ThrowIfDisposedOrNotOpen();
			this.AddHeadersTo(message);
			return this.OnBeginSend(message, timeout, callback, state);
		}

		// Token: 0x060046F9 RID: 18169 RVA: 0x00108996 File Offset: 0x00106B96
		public void EndSend(IAsyncResult result)
		{
			this.OnEndSend(result);
		}

		// Token: 0x060046FA RID: 18170 RVA: 0x001089A0 File Offset: 0x00106BA0
		public override T GetProperty<T>()
		{
			if (typeof(T) == typeof(IDuplexChannel))
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

		// Token: 0x060046FB RID: 18171
		protected abstract void OnSend(Message message, TimeSpan timeout);

		// Token: 0x060046FC RID: 18172 RVA: 0x001089E9 File Offset: 0x00106BE9
		protected virtual IAsyncResult OnBeginSend(Message message, TimeSpan timeout, AsyncCallback callback, object state)
		{
			this.OnSend(message, timeout);
			return new CompletedAsyncResult(callback, state);
		}

		// Token: 0x060046FD RID: 18173 RVA: 0x001089FB File Offset: 0x00106BFB
		protected virtual void OnEndSend(IAsyncResult result)
		{
			CompletedAsyncResult.End(result);
		}

		// Token: 0x060046FE RID: 18174 RVA: 0x00108A03 File Offset: 0x00106C03
		public void Send(Message message)
		{
			this.Send(message, base.DefaultSendTimeout);
		}

		// Token: 0x060046FF RID: 18175 RVA: 0x00108A14 File Offset: 0x00106C14
		public void Send(Message message, TimeSpan timeout)
		{
			if (message == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("message");
			}
			if (timeout < TimeSpan.Zero)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("timeout", timeout, SR.GetString("SFxTimeoutOutOfRange0")));
			}
			base.ThrowIfDisposedOrNotOpen();
			this.AddHeadersTo(message);
			this.OnSend(message, timeout);
		}

		// Token: 0x06004700 RID: 18176 RVA: 0x00108A7B File Offset: 0x00106C7B
		protected virtual void AddHeadersTo(Message message)
		{
		}

		// Token: 0x06004701 RID: 18177 RVA: 0x00108A7D File Offset: 0x00106C7D
		public Message Receive()
		{
			return this.Receive(base.DefaultReceiveTimeout);
		}

		// Token: 0x06004702 RID: 18178 RVA: 0x00108A8C File Offset: 0x00106C8C
		public Message Receive(TimeSpan timeout)
		{
			if (timeout < TimeSpan.Zero)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("timeout", timeout, SR.GetString("SFxTimeoutOutOfRange0")));
			}
			base.ThrowPending();
			return InputChannel.HelpReceive(this, timeout);
		}

		// Token: 0x06004703 RID: 18179 RVA: 0x00108AD8 File Offset: 0x00106CD8
		public IAsyncResult BeginReceive(AsyncCallback callback, object state)
		{
			return this.BeginReceive(base.DefaultReceiveTimeout, callback, state);
		}

		// Token: 0x06004704 RID: 18180 RVA: 0x00108AE8 File Offset: 0x00106CE8
		public IAsyncResult BeginReceive(TimeSpan timeout, AsyncCallback callback, object state)
		{
			if (timeout < TimeSpan.Zero)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("timeout", timeout, SR.GetString("SFxTimeoutOutOfRange0")));
			}
			base.ThrowPending();
			return InputChannel.HelpBeginReceive(this, timeout, callback, state);
		}

		// Token: 0x06004705 RID: 18181 RVA: 0x00108B36 File Offset: 0x00106D36
		public Message EndReceive(IAsyncResult result)
		{
			return InputChannel.HelpEndReceive(result);
		}

		// Token: 0x06004706 RID: 18182 RVA: 0x00108B40 File Offset: 0x00106D40
		public bool TryReceive(TimeSpan timeout, out Message message)
		{
			if (timeout < TimeSpan.Zero)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("timeout", timeout, SR.GetString("SFxTimeoutOutOfRange0")));
			}
			base.ThrowPending();
			return base.Dequeue(timeout, out message);
		}

		// Token: 0x06004707 RID: 18183 RVA: 0x00108B90 File Offset: 0x00106D90
		public IAsyncResult BeginTryReceive(TimeSpan timeout, AsyncCallback callback, object state)
		{
			if (timeout < TimeSpan.Zero)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("timeout", timeout, SR.GetString("SFxTimeoutOutOfRange0")));
			}
			base.ThrowPending();
			return base.BeginDequeue(timeout, callback, state);
		}

		// Token: 0x06004708 RID: 18184 RVA: 0x00108BDE File Offset: 0x00106DDE
		public bool EndTryReceive(IAsyncResult result, out Message message)
		{
			return base.EndDequeue(result, out message);
		}

		// Token: 0x06004709 RID: 18185 RVA: 0x00108BE8 File Offset: 0x00106DE8
		public bool WaitForMessage(TimeSpan timeout)
		{
			if (timeout < TimeSpan.Zero)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("timeout", timeout, SR.GetString("SFxTimeoutOutOfRange0")));
			}
			base.ThrowPending();
			return base.WaitForItem(timeout);
		}

		// Token: 0x0600470A RID: 18186 RVA: 0x00108C34 File Offset: 0x00106E34
		public IAsyncResult BeginWaitForMessage(TimeSpan timeout, AsyncCallback callback, object state)
		{
			if (timeout < TimeSpan.Zero)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("timeout", timeout, SR.GetString("SFxTimeoutOutOfRange0")));
			}
			base.ThrowPending();
			return base.BeginWaitForItem(timeout, callback, state);
		}

		// Token: 0x0600470B RID: 18187 RVA: 0x00108C82 File Offset: 0x00106E82
		public bool EndWaitForMessage(IAsyncResult result)
		{
			return base.EndWaitForItem(result);
		}

		// Token: 0x04002DA6 RID: 11686
		private EndpointAddress localAddress;
	}
}
