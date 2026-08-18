using System;
using System.Runtime;
using System.ServiceModel.Channels;

namespace System.ServiceModel.Security
{
	// Token: 0x020002EE RID: 750
	internal abstract class SecurityChannel<TChannel> : LayeredChannel<TChannel> where TChannel : class, IChannel
	{
		// Token: 0x060018B5 RID: 6325 RVA: 0x0005BFF1 File Offset: 0x0005A1F1
		protected SecurityChannel(ChannelManagerBase channelManager, TChannel innerChannel) : this(channelManager, innerChannel, null)
		{
		}

		// Token: 0x060018B6 RID: 6326 RVA: 0x0005BFFC File Offset: 0x0005A1FC
		protected SecurityChannel(ChannelManagerBase channelManager, TChannel innerChannel, SecurityProtocol securityProtocol) : base(channelManager, innerChannel)
		{
			this.securityProtocol = securityProtocol;
		}

		// Token: 0x060018B7 RID: 6327 RVA: 0x0005C00D File Offset: 0x0005A20D
		public override T GetProperty<T>()
		{
			if (typeof(T) == typeof(FaultConverter))
			{
				return new SecurityChannelFaultConverter(base.InnerChannel) as T;
			}
			return base.GetProperty<T>();
		}

		// Token: 0x170005F8 RID: 1528
		// (get) Token: 0x060018B8 RID: 6328 RVA: 0x0005C04B File Offset: 0x0005A24B
		// (set) Token: 0x060018B9 RID: 6329 RVA: 0x0005C053 File Offset: 0x0005A253
		public SecurityProtocol SecurityProtocol
		{
			get
			{
				return this.securityProtocol;
			}
			protected set
			{
				if (value == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("value"));
				}
				this.securityProtocol = value;
			}
		}

		// Token: 0x060018BA RID: 6330 RVA: 0x0005C074 File Offset: 0x0005A274
		protected override void OnAbort()
		{
			if (this.securityProtocol != null)
			{
				this.securityProtocol.Close(true, TimeSpan.Zero);
			}
			base.OnAbort();
		}

		// Token: 0x060018BB RID: 6331 RVA: 0x0005C095 File Offset: 0x0005A295
		protected override IAsyncResult OnBeginClose(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return new ChainedAsyncResult(timeout, callback, state, new ChainedBeginHandler(this.BeginCloseSecurityProtocol), new ChainedEndHandler(this.EndCloseSecurityProtocol), new ChainedBeginHandler(base.OnBeginClose), new ChainedEndHandler(base.OnEndClose));
		}

		// Token: 0x060018BC RID: 6332 RVA: 0x0005C0CF File Offset: 0x0005A2CF
		protected override void OnEndClose(IAsyncResult result)
		{
			ChainedAsyncResult.End(result);
		}

		// Token: 0x060018BD RID: 6333 RVA: 0x0005C0D7 File Offset: 0x0005A2D7
		private IAsyncResult BeginCloseSecurityProtocol(TimeSpan timeout, AsyncCallback callback, object state)
		{
			if (this.securityProtocol != null)
			{
				return this.securityProtocol.BeginClose(timeout, callback, state);
			}
			return new SecurityChannel<TChannel>.NullSecurityProtocolCloseAsyncResult(callback, state);
		}

		// Token: 0x060018BE RID: 6334 RVA: 0x0005C0F7 File Offset: 0x0005A2F7
		private void EndCloseSecurityProtocol(IAsyncResult result)
		{
			if (result is SecurityChannel<TChannel>.NullSecurityProtocolCloseAsyncResult)
			{
				SecurityChannel<TChannel>.NullSecurityProtocolCloseAsyncResult.End(result);
				return;
			}
			this.securityProtocol.EndClose(result);
		}

		// Token: 0x060018BF RID: 6335 RVA: 0x0005C114 File Offset: 0x0005A314
		protected override void OnClose(TimeSpan timeout)
		{
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			if (this.securityProtocol != null)
			{
				this.securityProtocol.Close(false, timeoutHelper.RemainingTime());
			}
			base.OnClose(timeoutHelper.RemainingTime());
		}

		// Token: 0x060018C0 RID: 6336 RVA: 0x0005C151 File Offset: 0x0005A351
		protected void ThrowIfDisposedOrNotOpen(Message message)
		{
			base.ThrowIfDisposedOrNotOpen();
			if (message == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("message");
			}
		}

		// Token: 0x04001C54 RID: 7252
		private SecurityProtocol securityProtocol;

		// Token: 0x02000B53 RID: 2899
		private class NullSecurityProtocolCloseAsyncResult : CompletedAsyncResult
		{
			// Token: 0x06007133 RID: 28979 RVA: 0x001A5A5F File Offset: 0x001A3C5F
			public NullSecurityProtocolCloseAsyncResult(AsyncCallback callback, object state) : base(callback, state)
			{
			}

			// Token: 0x06007134 RID: 28980 RVA: 0x001A5A69 File Offset: 0x001A3C69
			public new static void End(IAsyncResult result)
			{
				AsyncResult.End<SecurityChannel<TChannel>.NullSecurityProtocolCloseAsyncResult>(result);
			}
		}

		// Token: 0x02000B54 RID: 2900
		protected sealed class OutputChannelSendAsyncResult : ApplySecurityAndSendAsyncResult<IOutputChannel>
		{
			// Token: 0x06007135 RID: 28981 RVA: 0x001A5A72 File Offset: 0x001A3C72
			public OutputChannelSendAsyncResult(Message message, SecurityProtocol binding, IOutputChannel channel, TimeSpan timeout, AsyncCallback callback, object state) : base(binding, channel, timeout, callback, state)
			{
				base.Begin(message, null);
			}

			// Token: 0x06007136 RID: 28982 RVA: 0x001A5A8A File Offset: 0x001A3C8A
			protected override IAsyncResult BeginSendCore(IOutputChannel channel, Message message, TimeSpan timeout, AsyncCallback callback, object state)
			{
				return channel.BeginSend(message, timeout, callback, state);
			}

			// Token: 0x06007137 RID: 28983 RVA: 0x001A5A98 File Offset: 0x001A3C98
			internal static void End(IAsyncResult result)
			{
				SecurityChannel<TChannel>.OutputChannelSendAsyncResult self = result as SecurityChannel<TChannel>.OutputChannelSendAsyncResult;
				ApplySecurityAndSendAsyncResult<IOutputChannel>.OnEnd(self);
			}

			// Token: 0x06007138 RID: 28984 RVA: 0x001A5AB2 File Offset: 0x001A3CB2
			protected override void EndSendCore(IOutputChannel channel, IAsyncResult result)
			{
				channel.EndSend(result);
			}

			// Token: 0x06007139 RID: 28985 RVA: 0x001A5ABB File Offset: 0x001A3CBB
			protected override void OnSendCompleteCore(TimeSpan timeout)
			{
			}
		}
	}
}
