using System;
using System.Runtime;
using System.ServiceModel.Diagnostics;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000899 RID: 2201
	internal sealed class InternalDuplexChannelListener : DelegatingChannelListener<IDuplexChannel>
	{
		// Token: 0x06005395 RID: 21397 RVA: 0x0013402A File Offset: 0x0013222A
		internal InternalDuplexChannelListener(InternalDuplexBindingElement bindingElement, BindingContext context) : base(context.Binding, context.Clone().BuildInnerChannelListener<IInputChannel>())
		{
			this.innerChannelFactory = context.BuildInnerChannelFactory<IOutputChannel>();
			this.providesCorrelation = bindingElement.ProvidesCorrelation;
		}

		// Token: 0x06005396 RID: 21398 RVA: 0x0013405C File Offset: 0x0013225C
		private IOutputChannel GetOutputChannel(Uri to, TimeoutHelper timeoutHelper)
		{
			IOutputChannel outputChannel = this.innerChannelFactory.CreateChannel(new EndpointAddress(to, new AddressHeader[0]));
			outputChannel.Open(timeoutHelper.RemainingTime());
			return outputChannel;
		}

		// Token: 0x06005397 RID: 21399 RVA: 0x00134090 File Offset: 0x00132290
		protected override void OnAbort()
		{
			try
			{
				this.innerChannelFactory.Abort();
			}
			finally
			{
				base.OnAbort();
			}
		}

		// Token: 0x06005398 RID: 21400 RVA: 0x001340C4 File Offset: 0x001322C4
		protected override IAsyncResult OnBeginClose(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return new ChainedCloseAsyncResult(timeout, callback, state, new ChainedBeginHandler(base.OnBeginClose), new ChainedEndHandler(base.OnEndClose), new ICommunicationObject[]
			{
				this.innerChannelFactory
			});
		}

		// Token: 0x06005399 RID: 21401 RVA: 0x00134100 File Offset: 0x00132300
		protected override void OnEndClose(IAsyncResult result)
		{
			ChainedAsyncResult.End(result);
		}

		// Token: 0x0600539A RID: 21402 RVA: 0x00134108 File Offset: 0x00132308
		protected override void OnClose(TimeSpan timeout)
		{
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			base.OnClose(timeoutHelper.RemainingTime());
			this.innerChannelFactory.Close(timeoutHelper.RemainingTime());
		}

		// Token: 0x0600539B RID: 21403 RVA: 0x0013413C File Offset: 0x0013233C
		protected override void OnOpening()
		{
			base.OnOpening();
			base.Acceptor = (IChannelAcceptor<IDuplexChannel>)new InternalDuplexChannelListener.CompositeDuplexChannelAcceptor(this, (IChannelListener<IInputChannel>)this.InnerChannelListener);
		}

		// Token: 0x0600539C RID: 21404 RVA: 0x00134160 File Offset: 0x00132360
		protected override IAsyncResult OnBeginOpen(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return new ChainedOpenAsyncResult(timeout, callback, state, new ChainedBeginHandler(base.OnBeginOpen), new ChainedEndHandler(base.OnEndOpen), new ICommunicationObject[]
			{
				this.innerChannelFactory
			});
		}

		// Token: 0x0600539D RID: 21405 RVA: 0x0013419C File Offset: 0x0013239C
		protected override void OnEndOpen(IAsyncResult result)
		{
			ChainedAsyncResult.End(result);
		}

		// Token: 0x0600539E RID: 21406 RVA: 0x001341A4 File Offset: 0x001323A4
		protected override void OnOpen(TimeSpan timeout)
		{
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			base.OnOpen(timeoutHelper.RemainingTime());
			this.innerChannelFactory.Open(timeoutHelper.RemainingTime());
		}

		// Token: 0x0600539F RID: 21407 RVA: 0x001341D8 File Offset: 0x001323D8
		public override T GetProperty<T>()
		{
			if (typeof(T) == typeof(IChannelFactory))
			{
				return (T)((object)this.innerChannelFactory);
			}
			if (typeof(T) == typeof(ISecurityCapabilities) && !this.providesCorrelation)
			{
				return InternalDuplexBindingElement.GetSecurityCapabilities<T>(base.GetProperty<ISecurityCapabilities>());
			}
			T property = base.GetProperty<T>();
			if (property != null)
			{
				return property;
			}
			return this.innerChannelFactory.GetProperty<T>();
		}

		// Token: 0x040032D9 RID: 13017
		private IChannelFactory<IOutputChannel> innerChannelFactory;

		// Token: 0x040032DA RID: 13018
		private bool providesCorrelation;

		// Token: 0x02000D74 RID: 3444
		private sealed class CompositeDuplexChannelAcceptor : LayeredChannelAcceptor<IDuplexChannel, IInputChannel>
		{
			// Token: 0x06007E40 RID: 32320 RVA: 0x001D7681 File Offset: 0x001D5881
			public CompositeDuplexChannelAcceptor(InternalDuplexChannelListener listener, IChannelListener<IInputChannel> innerListener) : base(listener, innerListener)
			{
			}

			// Token: 0x06007E41 RID: 32321 RVA: 0x001D768B File Offset: 0x001D588B
			protected override IDuplexChannel OnAcceptChannel(IInputChannel innerChannel)
			{
				return new InternalDuplexChannelListener.ServerCompositeDuplexChannel((InternalDuplexChannelListener)base.ChannelManager, innerChannel);
			}
		}

		// Token: 0x02000D75 RID: 3445
		private sealed class ServerCompositeDuplexChannel : ChannelBase, IDuplexChannel, IInputChannel, IChannel, ICommunicationObject, IOutputChannel
		{
			// Token: 0x06007E42 RID: 32322 RVA: 0x001D769E File Offset: 0x001D589E
			public ServerCompositeDuplexChannel(InternalDuplexChannelListener listener, IInputChannel innerInputChannel) : base(listener)
			{
				this.innerInputChannel = innerInputChannel;
				this.sendTimeout = listener.DefaultSendTimeout;
			}

			// Token: 0x17001C1F RID: 7199
			// (get) Token: 0x06007E43 RID: 32323 RVA: 0x001D76BA File Offset: 0x001D58BA
			private InternalDuplexChannelListener Listener
			{
				get
				{
					return (InternalDuplexChannelListener)base.Manager;
				}
			}

			// Token: 0x17001C20 RID: 7200
			// (get) Token: 0x06007E44 RID: 32324 RVA: 0x001D76C7 File Offset: 0x001D58C7
			public EndpointAddress LocalAddress
			{
				get
				{
					return this.innerInputChannel.LocalAddress;
				}
			}

			// Token: 0x17001C21 RID: 7201
			// (get) Token: 0x06007E45 RID: 32325 RVA: 0x001D76D4 File Offset: 0x001D58D4
			public EndpointAddress RemoteAddress
			{
				get
				{
					return null;
				}
			}

			// Token: 0x17001C22 RID: 7202
			// (get) Token: 0x06007E46 RID: 32326 RVA: 0x001D76D7 File Offset: 0x001D58D7
			public Uri Via
			{
				get
				{
					return null;
				}
			}

			// Token: 0x06007E47 RID: 32327 RVA: 0x001D76DA File Offset: 0x001D58DA
			public Message Receive()
			{
				return this.Receive(base.DefaultReceiveTimeout);
			}

			// Token: 0x06007E48 RID: 32328 RVA: 0x001D76E8 File Offset: 0x001D58E8
			public Message Receive(TimeSpan timeout)
			{
				return InputChannel.HelpReceive(this, timeout);
			}

			// Token: 0x06007E49 RID: 32329 RVA: 0x001D76F1 File Offset: 0x001D58F1
			public IAsyncResult BeginReceive(AsyncCallback callback, object state)
			{
				return this.BeginReceive(base.DefaultReceiveTimeout, callback, state);
			}

			// Token: 0x06007E4A RID: 32330 RVA: 0x001D7701 File Offset: 0x001D5901
			public IAsyncResult BeginReceive(TimeSpan timeout, AsyncCallback callback, object state)
			{
				return InputChannel.HelpBeginReceive(this, timeout, callback, state);
			}

			// Token: 0x06007E4B RID: 32331 RVA: 0x001D770C File Offset: 0x001D590C
			public Message EndReceive(IAsyncResult result)
			{
				return InputChannel.HelpEndReceive(result);
			}

			// Token: 0x06007E4C RID: 32332 RVA: 0x001D7714 File Offset: 0x001D5914
			public IAsyncResult BeginTryReceive(TimeSpan timeout, AsyncCallback callback, object state)
			{
				return this.innerInputChannel.BeginTryReceive(timeout, callback, state);
			}

			// Token: 0x06007E4D RID: 32333 RVA: 0x001D7724 File Offset: 0x001D5924
			public IAsyncResult BeginSend(Message message, AsyncCallback callback, object state)
			{
				return this.BeginSend(message, base.DefaultSendTimeout, callback, state);
			}

			// Token: 0x06007E4E RID: 32334 RVA: 0x001D7735 File Offset: 0x001D5935
			public IAsyncResult BeginSend(Message message, TimeSpan timeout, AsyncCallback callback, object state)
			{
				return new InternalDuplexChannelListener.ServerCompositeDuplexChannel.SendAsyncResult(this, message, timeout, callback, state);
			}

			// Token: 0x06007E4F RID: 32335 RVA: 0x001D7742 File Offset: 0x001D5942
			public bool EndTryReceive(IAsyncResult result, out Message message)
			{
				return this.innerInputChannel.EndTryReceive(result, out message);
			}

			// Token: 0x06007E50 RID: 32336 RVA: 0x001D7751 File Offset: 0x001D5951
			public void EndSend(IAsyncResult result)
			{
				InternalDuplexChannelListener.ServerCompositeDuplexChannel.SendAsyncResult.End(result);
			}

			// Token: 0x06007E51 RID: 32337 RVA: 0x001D7759 File Offset: 0x001D5959
			protected override void OnAbort()
			{
				this.innerInputChannel.Abort();
			}

			// Token: 0x06007E52 RID: 32338 RVA: 0x001D7766 File Offset: 0x001D5966
			protected override IAsyncResult OnBeginClose(TimeSpan timeout, AsyncCallback callback, object state)
			{
				return this.innerInputChannel.BeginClose(timeout, callback, state);
			}

			// Token: 0x06007E53 RID: 32339 RVA: 0x001D7776 File Offset: 0x001D5976
			protected override void OnEndClose(IAsyncResult result)
			{
				this.innerInputChannel.EndClose(result);
			}

			// Token: 0x06007E54 RID: 32340 RVA: 0x001D7784 File Offset: 0x001D5984
			protected override void OnClose(TimeSpan timeout)
			{
				if (this.innerInputChannel.State == CommunicationState.Opened)
				{
					this.innerInputChannel.Close(timeout);
				}
			}

			// Token: 0x06007E55 RID: 32341 RVA: 0x001D77A0 File Offset: 0x001D59A0
			protected override IAsyncResult OnBeginOpen(TimeSpan timeout, AsyncCallback callback, object state)
			{
				return this.innerInputChannel.BeginOpen(callback, state);
			}

			// Token: 0x06007E56 RID: 32342 RVA: 0x001D77AF File Offset: 0x001D59AF
			protected override void OnEndOpen(IAsyncResult result)
			{
				this.innerInputChannel.EndOpen(result);
			}

			// Token: 0x06007E57 RID: 32343 RVA: 0x001D77BD File Offset: 0x001D59BD
			protected override void OnOpen(TimeSpan timeout)
			{
				this.innerInputChannel.Open(timeout);
			}

			// Token: 0x06007E58 RID: 32344 RVA: 0x001D77CB File Offset: 0x001D59CB
			public bool TryReceive(TimeSpan timeout, out Message message)
			{
				return this.innerInputChannel.TryReceive(timeout, out message);
			}

			// Token: 0x06007E59 RID: 32345 RVA: 0x001D77DA File Offset: 0x001D59DA
			public void Send(Message message)
			{
				this.Send(message, base.DefaultSendTimeout);
			}

			// Token: 0x06007E5A RID: 32346 RVA: 0x001D77EC File Offset: 0x001D59EC
			public void Send(Message message, TimeSpan timeout)
			{
				TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
				IOutputChannel outputChannel = this.ValidateStateAndGetOutputChannel(message, timeoutHelper);
				try
				{
					outputChannel.Send(message, timeoutHelper.RemainingTime());
					outputChannel.Close(timeoutHelper.RemainingTime());
				}
				finally
				{
					outputChannel.Abort();
				}
			}

			// Token: 0x06007E5B RID: 32347 RVA: 0x001D7840 File Offset: 0x001D5A40
			private IOutputChannel ValidateStateAndGetOutputChannel(Message message, TimeoutHelper timeoutHelper)
			{
				base.ThrowIfDisposedOrNotOpen();
				if (message == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("message");
				}
				Uri uri = message.Properties.Via;
				if (uri == null)
				{
					uri = message.Headers.To;
					if (uri == null)
					{
						throw TraceUtility.ThrowHelperError(new CommunicationException(SR.GetString("MessageMustHaveViaOrToSetForSendingOnServerSideCompositeDuplexChannels")), message);
					}
					if (uri.Equals(EndpointAddress.AnonymousUri) || uri.Equals(message.Version.Addressing.AnonymousUri))
					{
						throw TraceUtility.ThrowHelperError(new CommunicationException(SR.GetString("MessageToCannotBeAddressedToAnonymousOnServerSideCompositeDuplexChannels", new object[]
						{
							uri
						})), message);
					}
				}
				else if (uri.Equals(EndpointAddress.AnonymousUri) || uri.Equals(message.Version.Addressing.AnonymousUri))
				{
					throw TraceUtility.ThrowHelperError(new CommunicationException(SR.GetString("MessageViaCannotBeAddressedToAnonymousOnServerSideCompositeDuplexChannels", new object[]
					{
						uri
					})), message);
				}
				return this.Listener.GetOutputChannel(uri, timeoutHelper);
			}

			// Token: 0x06007E5C RID: 32348 RVA: 0x001D793D File Offset: 0x001D5B3D
			public bool WaitForMessage(TimeSpan timeout)
			{
				return this.innerInputChannel.WaitForMessage(timeout);
			}

			// Token: 0x06007E5D RID: 32349 RVA: 0x001D794B File Offset: 0x001D5B4B
			public IAsyncResult BeginWaitForMessage(TimeSpan timeout, AsyncCallback callback, object state)
			{
				return this.innerInputChannel.BeginWaitForMessage(timeout, callback, state);
			}

			// Token: 0x06007E5E RID: 32350 RVA: 0x001D795B File Offset: 0x001D5B5B
			public bool EndWaitForMessage(IAsyncResult result)
			{
				return this.innerInputChannel.EndWaitForMessage(result);
			}

			// Token: 0x0400486B RID: 18539
			private IInputChannel innerInputChannel;

			// Token: 0x0400486C RID: 18540
			private TimeSpan sendTimeout;

			// Token: 0x02000F6C RID: 3948
			private class SendAsyncResult : AsyncResult
			{
				// Token: 0x060087AD RID: 34733 RVA: 0x001F8408 File Offset: 0x001F6608
				public SendAsyncResult(InternalDuplexChannelListener.ServerCompositeDuplexChannel outer, Message message, TimeSpan timeout, AsyncCallback callback, object state) : base(callback, state)
				{
					this.timeoutHelper = new TimeoutHelper(timeout);
					this.outputChannel = outer.ValidateStateAndGetOutputChannel(message, this.timeoutHelper);
					bool flag = false;
					try
					{
						IAsyncResult asyncResult = this.outputChannel.BeginSend(message, this.timeoutHelper.RemainingTime(), InternalDuplexChannelListener.ServerCompositeDuplexChannel.SendAsyncResult.sendCompleteCallback, this);
						if (asyncResult.CompletedSynchronously)
						{
							this.CompleteSend(asyncResult);
							base.Complete(true);
						}
						flag = true;
					}
					finally
					{
						if (!flag)
						{
							this.outputChannel.Abort();
						}
					}
				}

				// Token: 0x060087AE RID: 34734 RVA: 0x001F8498 File Offset: 0x001F6698
				private void CompleteSend(IAsyncResult result)
				{
					try
					{
						this.outputChannel.EndSend(result);
						this.outputChannel.Close();
					}
					finally
					{
						this.outputChannel.Abort();
					}
				}

				// Token: 0x060087AF RID: 34735 RVA: 0x001F84DC File Offset: 0x001F66DC
				internal static void End(IAsyncResult result)
				{
					AsyncResult.End<InternalDuplexChannelListener.ServerCompositeDuplexChannel.SendAsyncResult>(result);
				}

				// Token: 0x060087B0 RID: 34736 RVA: 0x001F84E8 File Offset: 0x001F66E8
				private static void SendCompleteCallback(IAsyncResult result)
				{
					if (result.CompletedSynchronously)
					{
						return;
					}
					InternalDuplexChannelListener.ServerCompositeDuplexChannel.SendAsyncResult sendAsyncResult = (InternalDuplexChannelListener.ServerCompositeDuplexChannel.SendAsyncResult)result.AsyncState;
					Exception exception = null;
					try
					{
						sendAsyncResult.CompleteSend(result);
					}
					catch (Exception ex)
					{
						if (Fx.IsFatal(ex))
						{
							throw;
						}
						exception = ex;
					}
					sendAsyncResult.Complete(false, exception);
				}

				// Token: 0x04004F16 RID: 20246
				private IOutputChannel outputChannel;

				// Token: 0x04004F17 RID: 20247
				private static AsyncCallback sendCompleteCallback = Fx.ThunkCallback(new AsyncCallback(InternalDuplexChannelListener.ServerCompositeDuplexChannel.SendAsyncResult.SendCompleteCallback));

				// Token: 0x04004F18 RID: 20248
				private TimeoutHelper timeoutHelper;
			}
		}
	}
}
