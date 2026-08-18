using System;
using System.Diagnostics;
using System.Runtime;
using System.ServiceModel.Diagnostics.Application;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000894 RID: 2196
	internal class DuplexSessionOneWayChannelFactory : LayeredChannelFactory<IOutputChannel>
	{
		// Token: 0x06005360 RID: 21344 RVA: 0x0013331C File Offset: 0x0013151C
		public DuplexSessionOneWayChannelFactory(OneWayBindingElement bindingElement, BindingContext context) : base(context.Binding, context.BuildInnerChannelFactory<IDuplexSessionChannel>())
		{
			this.packetRoutable = bindingElement.PacketRoutable;
			ISecurityCapabilities property = base.InnerChannelFactory.GetProperty<ISecurityCapabilities>();
			if (property != null && property.SupportsClientAuthentication)
			{
				this.channelPoolSettings = bindingElement.ChannelPoolSettings.Clone();
				return;
			}
			this.channelPool = new ChannelPool<IDuplexSessionChannel>(bindingElement.ChannelPoolSettings);
		}

		// Token: 0x06005361 RID: 21345 RVA: 0x00133381 File Offset: 0x00131581
		internal ChannelPool<IDuplexSessionChannel> GetChannelPool(out bool cleanupChannelPool)
		{
			if (this.channelPool != null)
			{
				cleanupChannelPool = false;
				return this.channelPool;
			}
			cleanupChannelPool = true;
			return new ChannelPool<IDuplexSessionChannel>(this.channelPoolSettings);
		}

		// Token: 0x06005362 RID: 21346 RVA: 0x001333A3 File Offset: 0x001315A3
		protected override void OnAbort()
		{
			if (this.channelPool != null)
			{
				this.channelPool.Close(TimeSpan.Zero);
			}
			base.OnAbort();
		}

		// Token: 0x06005363 RID: 21347 RVA: 0x001333C4 File Offset: 0x001315C4
		protected override void OnClose(TimeSpan timeout)
		{
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			if (this.channelPool != null)
			{
				this.channelPool.Close(timeoutHelper.RemainingTime());
			}
			base.OnClose(timeoutHelper.RemainingTime());
		}

		// Token: 0x06005364 RID: 21348 RVA: 0x00133404 File Offset: 0x00131604
		protected override IAsyncResult OnBeginClose(TimeSpan timeout, AsyncCallback callback, object state)
		{
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			if (this.channelPool != null)
			{
				this.channelPool.Close(timeoutHelper.RemainingTime());
			}
			return base.OnBeginClose(timeoutHelper.RemainingTime(), callback, state);
		}

		// Token: 0x06005365 RID: 21349 RVA: 0x00133443 File Offset: 0x00131643
		protected override IOutputChannel OnCreateChannel(EndpointAddress address, Uri via)
		{
			return new DuplexSessionOneWayChannelFactory.DuplexSessionOutputChannel(this, address, via);
		}

		// Token: 0x040032BE RID: 12990
		private ChannelPool<IDuplexSessionChannel> channelPool;

		// Token: 0x040032BF RID: 12991
		private ChannelPoolSettings channelPoolSettings;

		// Token: 0x040032C0 RID: 12992
		private bool packetRoutable;

		// Token: 0x02000D6E RID: 3438
		private class DuplexSessionOutputChannel : OutputChannel
		{
			// Token: 0x06007DF4 RID: 32244 RVA: 0x001D6904 File Offset: 0x001D4B04
			public DuplexSessionOutputChannel(DuplexSessionOneWayChannelFactory factory, EndpointAddress remoteAddress, Uri via) : base(factory)
			{
				this.channelPool = factory.GetChannelPool(out this.cleanupChannelPool);
				this.packetRoutable = factory.packetRoutable;
				this.innerFactory = (IChannelFactory<IDuplexSessionChannel>)factory.InnerChannelFactory;
				this.remoteAddress = remoteAddress;
				this.via = via;
			}

			// Token: 0x17001C1B RID: 7195
			// (get) Token: 0x06007DF5 RID: 32245 RVA: 0x001D6955 File Offset: 0x001D4B55
			public override EndpointAddress RemoteAddress
			{
				get
				{
					return this.remoteAddress;
				}
			}

			// Token: 0x17001C1C RID: 7196
			// (get) Token: 0x06007DF6 RID: 32246 RVA: 0x001D695D File Offset: 0x001D4B5D
			public override Uri Via
			{
				get
				{
					return this.via;
				}
			}

			// Token: 0x06007DF7 RID: 32247 RVA: 0x001D6965 File Offset: 0x001D4B65
			protected override void OnOpen(TimeSpan timeout)
			{
			}

			// Token: 0x06007DF8 RID: 32248 RVA: 0x001D6967 File Offset: 0x001D4B67
			protected override IAsyncResult OnBeginOpen(TimeSpan timeout, AsyncCallback callback, object state)
			{
				return new CompletedAsyncResult(callback, state);
			}

			// Token: 0x06007DF9 RID: 32249 RVA: 0x001D6970 File Offset: 0x001D4B70
			protected override void OnEndOpen(IAsyncResult result)
			{
				CompletedAsyncResult.End(result);
			}

			// Token: 0x06007DFA RID: 32250 RVA: 0x001D6978 File Offset: 0x001D4B78
			protected override void OnAbort()
			{
				if (this.cleanupChannelPool)
				{
					this.channelPool.Close(TimeSpan.Zero);
				}
			}

			// Token: 0x06007DFB RID: 32251 RVA: 0x001D6993 File Offset: 0x001D4B93
			protected override void OnClose(TimeSpan timeout)
			{
				if (this.cleanupChannelPool)
				{
					this.channelPool.Close(timeout);
				}
			}

			// Token: 0x06007DFC RID: 32252 RVA: 0x001D69AA File Offset: 0x001D4BAA
			protected override IAsyncResult OnBeginClose(TimeSpan timeout, AsyncCallback callback, object state)
			{
				if (this.cleanupChannelPool)
				{
					this.channelPool.Close(timeout);
				}
				return new CompletedAsyncResult(callback, state);
			}

			// Token: 0x06007DFD RID: 32253 RVA: 0x001D69C8 File Offset: 0x001D4BC8
			protected override void OnEndClose(IAsyncResult result)
			{
				CompletedAsyncResult.End(result);
			}

			// Token: 0x06007DFE RID: 32254 RVA: 0x001D69D0 File Offset: 0x001D4BD0
			protected override IAsyncResult OnBeginSend(Message message, TimeSpan timeout, AsyncCallback callback, object state)
			{
				return new DuplexSessionOneWayChannelFactory.DuplexSessionOutputChannel.SendAsyncResult(this, message, timeout, callback, state);
			}

			// Token: 0x06007DFF RID: 32255 RVA: 0x001D69DD File Offset: 0x001D4BDD
			protected override void OnEndSend(IAsyncResult result)
			{
				DuplexSessionOneWayChannelFactory.DuplexSessionOutputChannel.SendAsyncResult.End(result);
			}

			// Token: 0x06007E00 RID: 32256 RVA: 0x001D69E8 File Offset: 0x001D4BE8
			protected override void OnSend(Message message, TimeSpan timeout)
			{
				TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
				ChannelPoolKey key = null;
				bool flag = true;
				IDuplexSessionChannel channelFromPool = this.GetChannelFromPool(ref timeoutHelper, out key, out flag);
				bool flag2 = false;
				try
				{
					if (!flag)
					{
						this.StampInitialMessage(message);
						channelFromPool.Open(timeoutHelper.RemainingTime());
						this.StartBackgroundReceive(channelFromPool);
					}
					channelFromPool.Send(message, timeoutHelper.RemainingTime());
					flag2 = true;
				}
				finally
				{
					if (!flag2)
					{
						this.CleanupChannel(channelFromPool, false, key, flag, ref timeoutHelper);
					}
				}
				this.CleanupChannel(channelFromPool, true, key, flag, ref timeoutHelper);
			}

			// Token: 0x06007E01 RID: 32257 RVA: 0x001D6A70 File Offset: 0x001D4C70
			private void StartBackgroundReceive(IDuplexSessionChannel channel)
			{
				if (this.onReceive == null)
				{
					this.onReceive = Fx.ThunkCallback(new AsyncCallback(this.OnReceive));
				}
				channel.BeginReceive(TimeSpan.MaxValue, this.onReceive, channel);
			}

			// Token: 0x06007E02 RID: 32258 RVA: 0x001D6AA4 File Offset: 0x001D4CA4
			private void OnReceive(IAsyncResult result)
			{
				IDuplexSessionChannel duplexSessionChannel = (IDuplexSessionChannel)result.AsyncState;
				bool flag = false;
				try
				{
					Message message = duplexSessionChannel.EndReceive(result);
					if (message == null)
					{
						duplexSessionChannel.Close(this.channelPool.IdleTimeout);
						flag = true;
					}
					else
					{
						message.Close();
					}
				}
				catch (CommunicationException exception)
				{
					DiagnosticUtility.TraceHandledException(exception, TraceEventType.Information);
				}
				catch (TimeoutException ex)
				{
					if (TD.CloseTimeoutIsEnabled())
					{
						TD.CloseTimeout(ex.Message);
					}
					DiagnosticUtility.TraceHandledException(ex, TraceEventType.Information);
				}
				finally
				{
					if (!flag)
					{
						duplexSessionChannel.Abort();
					}
				}
			}

			// Token: 0x06007E03 RID: 32259 RVA: 0x001D6B44 File Offset: 0x001D4D44
			private void StampInitialMessage(Message message)
			{
				if (this.packetRoutable)
				{
					PacketRoutableHeader.AddHeadersTo(message, null);
				}
			}

			// Token: 0x06007E04 RID: 32260 RVA: 0x001D6B55 File Offset: 0x001D4D55
			private void CleanupChannel(IDuplexSessionChannel channel, bool connectionStillGood, ChannelPoolKey key, bool isConnectionFromPool, ref TimeoutHelper timeoutHelper)
			{
				if (isConnectionFromPool)
				{
					this.channelPool.ReturnConnection(key, channel, connectionStillGood, timeoutHelper.RemainingTime());
					return;
				}
				if (connectionStillGood)
				{
					this.channelPool.AddConnection(key, channel, timeoutHelper.RemainingTime());
					return;
				}
				channel.Abort();
			}

			// Token: 0x06007E05 RID: 32261 RVA: 0x001D6B90 File Offset: 0x001D4D90
			private IDuplexSessionChannel GetChannelFromPool(ref TimeoutHelper timeoutHelper, out ChannelPoolKey key, out bool isConnectionFromPool)
			{
				isConnectionFromPool = true;
				for (;;)
				{
					IDuplexSessionChannel duplexSessionChannel = this.channelPool.TakeConnection(this.RemoteAddress, this.Via, timeoutHelper.RemainingTime(), out key);
					if (duplexSessionChannel == null)
					{
						break;
					}
					if (duplexSessionChannel.State == CommunicationState.Opened)
					{
						return duplexSessionChannel;
					}
					this.channelPool.ReturnConnection(key, duplexSessionChannel, false, timeoutHelper.RemainingTime());
				}
				isConnectionFromPool = false;
				return this.innerFactory.CreateChannel(this.RemoteAddress, this.Via);
			}

			// Token: 0x04004855 RID: 18517
			private ChannelPool<IDuplexSessionChannel> channelPool;

			// Token: 0x04004856 RID: 18518
			private EndpointAddress remoteAddress;

			// Token: 0x04004857 RID: 18519
			private IChannelFactory<IDuplexSessionChannel> innerFactory;

			// Token: 0x04004858 RID: 18520
			private AsyncCallback onReceive;

			// Token: 0x04004859 RID: 18521
			private bool packetRoutable;

			// Token: 0x0400485A RID: 18522
			private bool cleanupChannelPool;

			// Token: 0x0400485B RID: 18523
			private Uri via;

			// Token: 0x02000F67 RID: 3943
			private class SendAsyncResult : AsyncResult
			{
				// Token: 0x0600878C RID: 34700 RVA: 0x001F7CB0 File Offset: 0x001F5EB0
				public SendAsyncResult(DuplexSessionOneWayChannelFactory.DuplexSessionOutputChannel parent, Message message, TimeSpan timeout, AsyncCallback callback, object state) : base(callback, state)
				{
					this.parent = parent;
					this.message = message;
					this.timeoutHelper = new TimeoutHelper(timeout);
					this.innerChannel = parent.GetChannelFromPool(ref this.timeoutHelper, out this.key, out this.isConnectionFromPool);
					bool flag = false;
					bool flag2 = true;
					try
					{
						if (!this.isConnectionFromPool)
						{
							flag2 = this.OpenNewChannel();
						}
						if (flag2)
						{
							flag2 = this.SendMessage();
						}
						flag = true;
					}
					finally
					{
						if (!flag)
						{
							this.Cleanup(false);
						}
					}
					if (flag2)
					{
						this.Cleanup(true);
						base.Complete(true);
					}
				}

				// Token: 0x0600878D RID: 34701 RVA: 0x001F7D4C File Offset: 0x001F5F4C
				public static void End(IAsyncResult result)
				{
					AsyncResult.End<DuplexSessionOneWayChannelFactory.DuplexSessionOutputChannel.SendAsyncResult>(result);
				}

				// Token: 0x0600878E RID: 34702 RVA: 0x001F7D55 File Offset: 0x001F5F55
				private void Cleanup(bool connectionStillGood)
				{
					this.parent.CleanupChannel(this.innerChannel, connectionStillGood, this.key, this.isConnectionFromPool, ref this.timeoutHelper);
				}

				// Token: 0x0600878F RID: 34703 RVA: 0x001F7D7C File Offset: 0x001F5F7C
				private bool OpenNewChannel()
				{
					if (DuplexSessionOneWayChannelFactory.DuplexSessionOutputChannel.SendAsyncResult.onOpen == null)
					{
						DuplexSessionOneWayChannelFactory.DuplexSessionOutputChannel.SendAsyncResult.onOpen = Fx.ThunkCallback(new AsyncCallback(DuplexSessionOneWayChannelFactory.DuplexSessionOutputChannel.SendAsyncResult.OnOpen));
					}
					this.parent.StampInitialMessage(this.message);
					IAsyncResult asyncResult = this.innerChannel.BeginOpen(this.timeoutHelper.RemainingTime(), DuplexSessionOneWayChannelFactory.DuplexSessionOutputChannel.SendAsyncResult.onOpen, this);
					if (!asyncResult.CompletedSynchronously)
					{
						return false;
					}
					this.CompleteOpen(asyncResult);
					return true;
				}

				// Token: 0x06008790 RID: 34704 RVA: 0x001F7DE6 File Offset: 0x001F5FE6
				private void CompleteOpen(IAsyncResult result)
				{
					this.innerChannel.EndOpen(result);
					this.parent.StartBackgroundReceive(this.innerChannel);
				}

				// Token: 0x06008791 RID: 34705 RVA: 0x001F7E08 File Offset: 0x001F6008
				private bool SendMessage()
				{
					IAsyncResult asyncResult = this.innerChannel.BeginSend(this.message, DuplexSessionOneWayChannelFactory.DuplexSessionOutputChannel.SendAsyncResult.onInnerSend, this);
					if (!asyncResult.CompletedSynchronously)
					{
						return false;
					}
					this.innerChannel.EndSend(asyncResult);
					return true;
				}

				// Token: 0x06008792 RID: 34706 RVA: 0x001F7E44 File Offset: 0x001F6044
				private static void OnOpen(IAsyncResult result)
				{
					if (result.CompletedSynchronously)
					{
						return;
					}
					DuplexSessionOneWayChannelFactory.DuplexSessionOutputChannel.SendAsyncResult sendAsyncResult = (DuplexSessionOneWayChannelFactory.DuplexSessionOutputChannel.SendAsyncResult)result.AsyncState;
					Exception ex = null;
					bool flag = false;
					try
					{
						sendAsyncResult.CompleteOpen(result);
						flag = sendAsyncResult.SendMessage();
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
						sendAsyncResult.Cleanup(ex == null);
						sendAsyncResult.Complete(false, ex);
					}
				}

				// Token: 0x06008793 RID: 34707 RVA: 0x001F7EB0 File Offset: 0x001F60B0
				private static void OnInnerSend(IAsyncResult result)
				{
					if (result.CompletedSynchronously)
					{
						return;
					}
					DuplexSessionOneWayChannelFactory.DuplexSessionOutputChannel.SendAsyncResult sendAsyncResult = (DuplexSessionOneWayChannelFactory.DuplexSessionOutputChannel.SendAsyncResult)result.AsyncState;
					Exception ex = null;
					try
					{
						sendAsyncResult.innerChannel.EndSend(result);
					}
					catch (Exception ex2)
					{
						if (Fx.IsFatal(ex2))
						{
							throw;
						}
						ex = ex2;
					}
					sendAsyncResult.Cleanup(ex == null);
					sendAsyncResult.Complete(false, ex);
				}

				// Token: 0x04004F04 RID: 20228
				private DuplexSessionOneWayChannelFactory.DuplexSessionOutputChannel parent;

				// Token: 0x04004F05 RID: 20229
				private IDuplexSessionChannel innerChannel;

				// Token: 0x04004F06 RID: 20230
				private Message message;

				// Token: 0x04004F07 RID: 20231
				private TimeoutHelper timeoutHelper;

				// Token: 0x04004F08 RID: 20232
				private static AsyncCallback onOpen;

				// Token: 0x04004F09 RID: 20233
				private static AsyncCallback onInnerSend = Fx.ThunkCallback(new AsyncCallback(DuplexSessionOneWayChannelFactory.DuplexSessionOutputChannel.SendAsyncResult.OnInnerSend));

				// Token: 0x04004F0A RID: 20234
				private ChannelPoolKey key;

				// Token: 0x04004F0B RID: 20235
				private bool isConnectionFromPool;
			}
		}
	}
}
