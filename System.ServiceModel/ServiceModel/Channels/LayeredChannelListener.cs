using System;
using System.Runtime;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000751 RID: 1873
	internal abstract class LayeredChannelListener<TChannel> : ChannelListenerBase<TChannel> where TChannel : class, IChannel
	{
		// Token: 0x06004798 RID: 18328 RVA: 0x001098C7 File Offset: 0x00107AC7
		protected LayeredChannelListener(IDefaultCommunicationTimeouts timeouts, IChannelListener innerChannelListener) : this(false, timeouts, innerChannelListener)
		{
		}

		// Token: 0x06004799 RID: 18329 RVA: 0x001098D2 File Offset: 0x00107AD2
		protected LayeredChannelListener(bool sharedInnerListener) : this(sharedInnerListener, null, null)
		{
		}

		// Token: 0x0600479A RID: 18330 RVA: 0x001098DD File Offset: 0x00107ADD
		protected LayeredChannelListener(bool sharedInnerListener, IDefaultCommunicationTimeouts timeouts) : this(sharedInnerListener, timeouts, null)
		{
		}

		// Token: 0x0600479B RID: 18331 RVA: 0x001098E8 File Offset: 0x00107AE8
		protected LayeredChannelListener(bool sharedInnerListener, IDefaultCommunicationTimeouts timeouts, IChannelListener innerChannelListener) : base(timeouts)
		{
			this.sharedInnerListener = sharedInnerListener;
			this.innerChannelListener = innerChannelListener;
			this.onInnerListenerFaulted = new EventHandler(this.OnInnerListenerFaulted);
			if (this.innerChannelListener != null)
			{
				this.innerChannelListener.Faulted += this.onInnerListenerFaulted;
			}
		}

		// Token: 0x1700121E RID: 4638
		// (get) Token: 0x0600479C RID: 18332 RVA: 0x00109935 File Offset: 0x00107B35
		// (set) Token: 0x0600479D RID: 18333 RVA: 0x00109940 File Offset: 0x00107B40
		internal virtual IChannelListener InnerChannelListener
		{
			get
			{
				return this.innerChannelListener;
			}
			set
			{
				object thisLock = base.ThisLock;
				lock (thisLock)
				{
					base.ThrowIfDisposedOrImmutable();
					if (this.innerChannelListener != null)
					{
						this.innerChannelListener.Faulted -= this.onInnerListenerFaulted;
					}
					this.innerChannelListener = value;
					if (this.innerChannelListener != null)
					{
						this.innerChannelListener.Faulted += this.onInnerListenerFaulted;
					}
				}
			}
		}

		// Token: 0x1700121F RID: 4639
		// (get) Token: 0x0600479E RID: 18334 RVA: 0x001099BC File Offset: 0x00107BBC
		internal bool SharedInnerListener
		{
			get
			{
				return this.sharedInnerListener;
			}
		}

		// Token: 0x17001220 RID: 4640
		// (get) Token: 0x0600479F RID: 18335 RVA: 0x001099C4 File Offset: 0x00107BC4
		public override Uri Uri
		{
			get
			{
				return this.GetInnerListenerSnapshot().Uri;
			}
		}

		// Token: 0x060047A0 RID: 18336 RVA: 0x001099D4 File Offset: 0x00107BD4
		public override T GetProperty<T>()
		{
			T property = base.GetProperty<T>();
			if (property != null)
			{
				return property;
			}
			IChannelListener channelListener = this.InnerChannelListener;
			if (channelListener != null)
			{
				return channelListener.GetProperty<T>();
			}
			return default(T);
		}

		// Token: 0x060047A1 RID: 18337 RVA: 0x00109A0C File Offset: 0x00107C0C
		protected override void OnAbort()
		{
			object thisLock = base.ThisLock;
			lock (thisLock)
			{
				this.OnCloseOrAbort();
			}
			IChannelListener channelListener = this.InnerChannelListener;
			if (channelListener != null && !this.sharedInnerListener)
			{
				channelListener.Abort();
			}
		}

		// Token: 0x060047A2 RID: 18338 RVA: 0x00109A64 File Offset: 0x00107C64
		protected override IAsyncResult OnBeginClose(TimeSpan timeout, AsyncCallback callback, object state)
		{
			this.OnCloseOrAbort();
			return new LayeredChannelListener<TChannel>.CloseAsyncResult(this.InnerChannelListener, this.sharedInnerListener, timeout, callback, state);
		}

		// Token: 0x060047A3 RID: 18339 RVA: 0x00109A80 File Offset: 0x00107C80
		protected override void OnEndClose(IAsyncResult result)
		{
			LayeredChannelListener<TChannel>.CloseAsyncResult.End(result);
		}

		// Token: 0x060047A4 RID: 18340 RVA: 0x00109A88 File Offset: 0x00107C88
		protected override void OnClose(TimeSpan timeout)
		{
			this.OnCloseOrAbort();
			if (this.InnerChannelListener != null && !this.sharedInnerListener)
			{
				this.InnerChannelListener.Close(timeout);
			}
		}

		// Token: 0x060047A5 RID: 18341 RVA: 0x00109AAC File Offset: 0x00107CAC
		private void OnCloseOrAbort()
		{
			IChannelListener channelListener = this.InnerChannelListener;
			if (channelListener != null)
			{
				channelListener.Faulted -= this.onInnerListenerFaulted;
			}
		}

		// Token: 0x060047A6 RID: 18342 RVA: 0x00109ACF File Offset: 0x00107CCF
		protected override IAsyncResult OnBeginOpen(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return new LayeredChannelListener<TChannel>.OpenAsyncResult(this.InnerChannelListener, this.sharedInnerListener, timeout, callback, state);
		}

		// Token: 0x060047A7 RID: 18343 RVA: 0x00109AE5 File Offset: 0x00107CE5
		protected override void OnEndOpen(IAsyncResult result)
		{
			LayeredChannelListener<TChannel>.OpenAsyncResult.End(result);
		}

		// Token: 0x060047A8 RID: 18344 RVA: 0x00109AED File Offset: 0x00107CED
		protected override void OnOpen(TimeSpan timeout)
		{
			if (this.InnerChannelListener != null && !this.sharedInnerListener)
			{
				this.InnerChannelListener.Open(timeout);
			}
		}

		// Token: 0x060047A9 RID: 18345 RVA: 0x00109B0B File Offset: 0x00107D0B
		protected override void OnOpening()
		{
			base.OnOpening();
			this.ThrowIfInnerListenerNotSet();
		}

		// Token: 0x060047AA RID: 18346 RVA: 0x00109B19 File Offset: 0x00107D19
		private void OnInnerListenerFaulted(object sender, EventArgs e)
		{
			base.Fault();
		}

		// Token: 0x060047AB RID: 18347 RVA: 0x00109B21 File Offset: 0x00107D21
		internal void ThrowIfInnerListenerNotSet()
		{
			if (this.InnerChannelListener == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("InnerListenerFactoryNotSet", new object[]
				{
					base.GetType().ToString()
				})));
			}
		}

		// Token: 0x060047AC RID: 18348 RVA: 0x00109B5C File Offset: 0x00107D5C
		internal IChannelListener GetInnerListenerSnapshot()
		{
			IChannelListener channelListener = this.InnerChannelListener;
			if (channelListener == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("InnerListenerFactoryNotSet", new object[]
				{
					base.GetType().ToString()
				})));
			}
			return channelListener;
		}

		// Token: 0x04002DB1 RID: 11697
		private IChannelListener innerChannelListener;

		// Token: 0x04002DB2 RID: 11698
		private bool sharedInnerListener;

		// Token: 0x04002DB3 RID: 11699
		private EventHandler onInnerListenerFaulted;

		// Token: 0x02000CD8 RID: 3288
		private class OpenAsyncResult : AsyncResult
		{
			// Token: 0x060079EA RID: 31210 RVA: 0x001C6AF8 File Offset: 0x001C4CF8
			public OpenAsyncResult(ICommunicationObject communicationObject, bool sharedInnerListener, TimeSpan timeout, AsyncCallback callback, object state) : base(callback, state)
			{
				this.communicationObject = communicationObject;
				if (this.communicationObject == null || sharedInnerListener)
				{
					base.Complete(true);
					return;
				}
				IAsyncResult asyncResult = this.communicationObject.BeginOpen(timeout, LayeredChannelListener<TChannel>.OpenAsyncResult.onOpenComplete, this);
				if (asyncResult.CompletedSynchronously)
				{
					this.communicationObject.EndOpen(asyncResult);
					base.Complete(true);
				}
			}

			// Token: 0x060079EB RID: 31211 RVA: 0x001C6B5C File Offset: 0x001C4D5C
			private static void OnOpenComplete(IAsyncResult result)
			{
				if (result.CompletedSynchronously)
				{
					return;
				}
				LayeredChannelListener<TChannel>.OpenAsyncResult openAsyncResult = (LayeredChannelListener<TChannel>.OpenAsyncResult)result.AsyncState;
				Exception exception = null;
				try
				{
					openAsyncResult.communicationObject.EndOpen(result);
				}
				catch (Exception ex)
				{
					if (Fx.IsFatal(ex))
					{
						throw;
					}
					exception = ex;
				}
				openAsyncResult.Complete(false, exception);
			}

			// Token: 0x060079EC RID: 31212 RVA: 0x001C6BB8 File Offset: 0x001C4DB8
			public static void End(IAsyncResult result)
			{
				AsyncResult.End<LayeredChannelListener<TChannel>.OpenAsyncResult>(result);
			}

			// Token: 0x040045C0 RID: 17856
			private ICommunicationObject communicationObject;

			// Token: 0x040045C1 RID: 17857
			private static AsyncCallback onOpenComplete = Fx.ThunkCallback(new AsyncCallback(LayeredChannelListener<TChannel>.OpenAsyncResult.OnOpenComplete));
		}

		// Token: 0x02000CD9 RID: 3289
		private class CloseAsyncResult : AsyncResult
		{
			// Token: 0x060079EE RID: 31214 RVA: 0x001C6BDC File Offset: 0x001C4DDC
			public CloseAsyncResult(ICommunicationObject communicationObject, bool sharedInnerListener, TimeSpan timeout, AsyncCallback callback, object state) : base(callback, state)
			{
				this.communicationObject = communicationObject;
				if (this.communicationObject == null || sharedInnerListener)
				{
					base.Complete(true);
					return;
				}
				IAsyncResult asyncResult = this.communicationObject.BeginClose(timeout, LayeredChannelListener<TChannel>.CloseAsyncResult.onCloseComplete, this);
				if (asyncResult.CompletedSynchronously)
				{
					this.communicationObject.EndClose(asyncResult);
					base.Complete(true);
				}
			}

			// Token: 0x060079EF RID: 31215 RVA: 0x001C6C40 File Offset: 0x001C4E40
			private static void OnCloseComplete(IAsyncResult result)
			{
				if (result.CompletedSynchronously)
				{
					return;
				}
				LayeredChannelListener<TChannel>.CloseAsyncResult closeAsyncResult = (LayeredChannelListener<TChannel>.CloseAsyncResult)result.AsyncState;
				Exception exception = null;
				try
				{
					closeAsyncResult.communicationObject.EndClose(result);
				}
				catch (Exception ex)
				{
					if (Fx.IsFatal(ex))
					{
						throw;
					}
					exception = ex;
				}
				closeAsyncResult.Complete(false, exception);
			}

			// Token: 0x060079F0 RID: 31216 RVA: 0x001C6C9C File Offset: 0x001C4E9C
			public static void End(IAsyncResult result)
			{
				AsyncResult.End<LayeredChannelListener<TChannel>.CloseAsyncResult>(result);
			}

			// Token: 0x040045C2 RID: 17858
			private ICommunicationObject communicationObject;

			// Token: 0x040045C3 RID: 17859
			private static AsyncCallback onCloseComplete = Fx.ThunkCallback(new AsyncCallback(LayeredChannelListener<TChannel>.CloseAsyncResult.OnCloseComplete));
		}
	}
}
