using System;
using System.Diagnostics;
using System.Runtime;
using System.ServiceModel.Diagnostics.Application;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000895 RID: 2197
	internal class ReplyOneWayChannelListener : LayeredChannelListener<IInputChannel>
	{
		// Token: 0x06005366 RID: 21350 RVA: 0x0013344D File Offset: 0x0013164D
		public ReplyOneWayChannelListener(OneWayBindingElement bindingElement, BindingContext context) : base(context.Binding, context.BuildInnerChannelListener<IReplyChannel>())
		{
			this.packetRoutable = bindingElement.PacketRoutable;
		}

		// Token: 0x06005367 RID: 21351 RVA: 0x0013346D File Offset: 0x0013166D
		protected override void OnOpening()
		{
			this.innerChannelListener = (IChannelListener<IReplyChannel>)this.InnerChannelListener;
			base.OnOpening();
		}

		// Token: 0x06005368 RID: 21352 RVA: 0x00133488 File Offset: 0x00131688
		protected override IInputChannel OnAcceptChannel(TimeSpan timeout)
		{
			IReplyChannel innerChannel = this.innerChannelListener.AcceptChannel(timeout);
			return this.WrapInnerChannel(innerChannel);
		}

		// Token: 0x06005369 RID: 21353 RVA: 0x001334A9 File Offset: 0x001316A9
		protected override IAsyncResult OnBeginAcceptChannel(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return this.innerChannelListener.BeginAcceptChannel(timeout, callback, state);
		}

		// Token: 0x0600536A RID: 21354 RVA: 0x001334BC File Offset: 0x001316BC
		protected override IInputChannel OnEndAcceptChannel(IAsyncResult result)
		{
			IReplyChannel innerChannel = this.innerChannelListener.EndAcceptChannel(result);
			return this.WrapInnerChannel(innerChannel);
		}

		// Token: 0x0600536B RID: 21355 RVA: 0x001334DD File Offset: 0x001316DD
		protected override bool OnWaitForChannel(TimeSpan timeout)
		{
			return this.innerChannelListener.WaitForChannel(timeout);
		}

		// Token: 0x0600536C RID: 21356 RVA: 0x001334EB File Offset: 0x001316EB
		protected override IAsyncResult OnBeginWaitForChannel(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return this.innerChannelListener.BeginWaitForChannel(timeout, callback, state);
		}

		// Token: 0x0600536D RID: 21357 RVA: 0x001334FB File Offset: 0x001316FB
		protected override bool OnEndWaitForChannel(IAsyncResult result)
		{
			return this.innerChannelListener.EndWaitForChannel(result);
		}

		// Token: 0x0600536E RID: 21358 RVA: 0x00133509 File Offset: 0x00131709
		private IInputChannel WrapInnerChannel(IReplyChannel innerChannel)
		{
			if (innerChannel == null)
			{
				return null;
			}
			return new ReplyOneWayChannelListener.ReplyOneWayInputChannel(this, innerChannel);
		}

		// Token: 0x040032C1 RID: 12993
		private IChannelListener<IReplyChannel> innerChannelListener;

		// Token: 0x040032C2 RID: 12994
		private bool packetRoutable;

		// Token: 0x02000D6F RID: 3439
		private class ReplyOneWayInputChannel : LayeredChannel<IReplyChannel>, IInputChannel, IChannel, ICommunicationObject
		{
			// Token: 0x06007E06 RID: 32262 RVA: 0x001D6BFE File Offset: 0x001D4DFE
			public ReplyOneWayInputChannel(ReplyOneWayChannelListener listener, IReplyChannel innerChannel) : base(listener, innerChannel)
			{
				this.validateHeader = listener.packetRoutable;
			}

			// Token: 0x17001C1D RID: 7197
			// (get) Token: 0x06007E07 RID: 32263 RVA: 0x001D6C14 File Offset: 0x001D4E14
			public EndpointAddress LocalAddress
			{
				get
				{
					return base.InnerChannel.LocalAddress;
				}
			}

			// Token: 0x06007E08 RID: 32264 RVA: 0x001D6C24 File Offset: 0x001D4E24
			private Message ProcessContext(RequestContext context, TimeSpan timeout)
			{
				if (context == null)
				{
					return null;
				}
				bool flag = false;
				Message message = null;
				try
				{
					message = context.RequestMessage;
					message.Properties.Add(RequestContextMessageProperty.Name, new RequestContextMessageProperty(context));
					if (this.validateHeader)
					{
						PacketRoutableHeader.ValidateMessage(message);
					}
					try
					{
						TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
						context.Reply(null, timeoutHelper.RemainingTime());
						flag = true;
					}
					catch (CommunicationException exception)
					{
						DiagnosticUtility.TraceHandledException(exception, TraceEventType.Information);
					}
					catch (TimeoutException ex)
					{
						if (TD.SendTimeoutIsEnabled())
						{
							TD.SendTimeout(ex.Message);
						}
						DiagnosticUtility.TraceHandledException(ex, TraceEventType.Information);
					}
				}
				finally
				{
					if (!flag)
					{
						context.Abort();
						if (message != null)
						{
							message.Close();
							message = null;
						}
					}
				}
				return message;
			}

			// Token: 0x06007E09 RID: 32265 RVA: 0x001D6CE8 File Offset: 0x001D4EE8
			public Message Receive()
			{
				return this.Receive(base.DefaultReceiveTimeout);
			}

			// Token: 0x06007E0A RID: 32266 RVA: 0x001D6CF8 File Offset: 0x001D4EF8
			public Message Receive(TimeSpan timeout)
			{
				TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
				RequestContext context = base.InnerChannel.ReceiveRequest(timeoutHelper.RemainingTime());
				return this.ProcessContext(context, timeoutHelper.RemainingTime());
			}

			// Token: 0x06007E0B RID: 32267 RVA: 0x001D6D2E File Offset: 0x001D4F2E
			public IAsyncResult BeginReceive(AsyncCallback callback, object state)
			{
				return this.BeginReceive(base.DefaultReceiveTimeout, callback, state);
			}

			// Token: 0x06007E0C RID: 32268 RVA: 0x001D6D3E File Offset: 0x001D4F3E
			public IAsyncResult BeginReceive(TimeSpan timeout, AsyncCallback callback, object state)
			{
				return new ReplyOneWayChannelListener.ReplyOneWayInputChannel.ReceiveAsyncResult(base.InnerChannel, timeout, this.validateHeader, callback, state);
			}

			// Token: 0x06007E0D RID: 32269 RVA: 0x001D6D54 File Offset: 0x001D4F54
			public Message EndReceive(IAsyncResult result)
			{
				return ReplyOneWayChannelListener.ReplyOneWayInputChannel.ReceiveAsyncResult.End(result);
			}

			// Token: 0x06007E0E RID: 32270 RVA: 0x001D6D5C File Offset: 0x001D4F5C
			public bool TryReceive(TimeSpan timeout, out Message message)
			{
				TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
				RequestContext context;
				if (base.InnerChannel.TryReceiveRequest(timeoutHelper.RemainingTime(), out context))
				{
					message = this.ProcessContext(context, timeoutHelper.RemainingTime());
					return true;
				}
				message = null;
				return false;
			}

			// Token: 0x06007E0F RID: 32271 RVA: 0x001D6D9D File Offset: 0x001D4F9D
			public IAsyncResult BeginTryReceive(TimeSpan timeout, AsyncCallback callback, object state)
			{
				return new ReplyOneWayChannelListener.ReplyOneWayInputChannel.TryReceiveAsyncResult(base.InnerChannel, timeout, this.validateHeader, callback, state);
			}

			// Token: 0x06007E10 RID: 32272 RVA: 0x001D6DB3 File Offset: 0x001D4FB3
			public bool EndTryReceive(IAsyncResult result, out Message message)
			{
				return ReplyOneWayChannelListener.ReplyOneWayInputChannel.TryReceiveAsyncResult.End(result, out message);
			}

			// Token: 0x06007E11 RID: 32273 RVA: 0x001D6DBC File Offset: 0x001D4FBC
			public bool WaitForMessage(TimeSpan timeout)
			{
				return base.InnerChannel.WaitForRequest(timeout);
			}

			// Token: 0x06007E12 RID: 32274 RVA: 0x001D6DCA File Offset: 0x001D4FCA
			public IAsyncResult BeginWaitForMessage(TimeSpan timeout, AsyncCallback callback, object state)
			{
				return base.InnerChannel.BeginWaitForRequest(timeout, callback, state);
			}

			// Token: 0x06007E13 RID: 32275 RVA: 0x001D6DDA File Offset: 0x001D4FDA
			public bool EndWaitForMessage(IAsyncResult result)
			{
				return base.InnerChannel.EndWaitForRequest(result);
			}

			// Token: 0x0400485C RID: 18524
			private bool validateHeader;

			// Token: 0x02000F68 RID: 3944
			private class TryReceiveAsyncResult : ReplyOneWayChannelListener.ReplyOneWayInputChannel.ReceiveAsyncResultBase
			{
				// Token: 0x06008795 RID: 34709 RVA: 0x001F7F2C File Offset: 0x001F612C
				public TryReceiveAsyncResult(IReplyChannel innerChannel, TimeSpan timeout, bool validateHeader, AsyncCallback callback, object state) : base(innerChannel, timeout, validateHeader, callback, state)
				{
				}

				// Token: 0x06008796 RID: 34710 RVA: 0x001F7F3C File Offset: 0x001F613C
				public static bool End(IAsyncResult result, out Message message)
				{
					ReplyOneWayChannelListener.ReplyOneWayInputChannel.TryReceiveAsyncResult tryReceiveAsyncResult = AsyncResult.End<ReplyOneWayChannelListener.ReplyOneWayInputChannel.TryReceiveAsyncResult>(result);
					message = tryReceiveAsyncResult.Message;
					return tryReceiveAsyncResult.tryResult;
				}

				// Token: 0x06008797 RID: 34711 RVA: 0x001F7F5E File Offset: 0x001F615E
				protected override IAsyncResult OnBeginReceiveRequest(TimeSpan timeout, AsyncCallback callback, object state)
				{
					return base.InnerChannel.BeginTryReceiveRequest(timeout, callback, state);
				}

				// Token: 0x06008798 RID: 34712 RVA: 0x001F7F70 File Offset: 0x001F6170
				protected override RequestContext OnEndReceiveRequest(IAsyncResult result)
				{
					RequestContext result2;
					this.tryResult = base.InnerChannel.EndTryReceiveRequest(result, out result2);
					return result2;
				}

				// Token: 0x04004F0C RID: 20236
				private bool tryResult;
			}

			// Token: 0x02000F69 RID: 3945
			private class ReceiveAsyncResult : ReplyOneWayChannelListener.ReplyOneWayInputChannel.ReceiveAsyncResultBase
			{
				// Token: 0x06008799 RID: 34713 RVA: 0x001F7F92 File Offset: 0x001F6192
				public ReceiveAsyncResult(IReplyChannel innerChannel, TimeSpan timeout, bool validateHeader, AsyncCallback callback, object state) : base(innerChannel, timeout, validateHeader, callback, state)
				{
				}

				// Token: 0x0600879A RID: 34714 RVA: 0x001F7FA4 File Offset: 0x001F61A4
				public static Message End(IAsyncResult result)
				{
					ReplyOneWayChannelListener.ReplyOneWayInputChannel.ReceiveAsyncResult receiveAsyncResult = AsyncResult.End<ReplyOneWayChannelListener.ReplyOneWayInputChannel.ReceiveAsyncResult>(result);
					return receiveAsyncResult.Message;
				}

				// Token: 0x0600879B RID: 34715 RVA: 0x001F7FBE File Offset: 0x001F61BE
				protected override IAsyncResult OnBeginReceiveRequest(TimeSpan timeout, AsyncCallback callback, object state)
				{
					return base.InnerChannel.BeginReceiveRequest(timeout, callback, state);
				}

				// Token: 0x0600879C RID: 34716 RVA: 0x001F7FCE File Offset: 0x001F61CE
				protected override RequestContext OnEndReceiveRequest(IAsyncResult result)
				{
					return base.InnerChannel.EndReceiveRequest(result);
				}
			}

			// Token: 0x02000F6A RID: 3946
			private abstract class ReceiveAsyncResultBase : AsyncResult
			{
				// Token: 0x0600879D RID: 34717 RVA: 0x001F7FDC File Offset: 0x001F61DC
				protected ReceiveAsyncResultBase(IReplyChannel innerChannel, TimeSpan timeout, bool validateHeader, AsyncCallback callback, object state) : base(callback, state)
				{
					this.innerChannel = innerChannel;
					this.timeoutHelper = new TimeoutHelper(timeout);
					this.validateHeader = validateHeader;
					IAsyncResult asyncResult = this.OnBeginReceiveRequest(this.timeoutHelper.RemainingTime(), ReplyOneWayChannelListener.ReplyOneWayInputChannel.ReceiveAsyncResultBase.onReceiveRequest, this);
					if (!asyncResult.CompletedSynchronously)
					{
						return;
					}
					if (this.HandleReceiveRequestComplete(asyncResult))
					{
						base.Complete(true);
					}
				}

				// Token: 0x17001D98 RID: 7576
				// (get) Token: 0x0600879E RID: 34718 RVA: 0x001F803E File Offset: 0x001F623E
				protected IReplyChannel InnerChannel
				{
					get
					{
						return this.innerChannel;
					}
				}

				// Token: 0x17001D99 RID: 7577
				// (get) Token: 0x0600879F RID: 34719 RVA: 0x001F8046 File Offset: 0x001F6246
				protected Message Message
				{
					get
					{
						return this.message;
					}
				}

				// Token: 0x060087A0 RID: 34720
				protected abstract IAsyncResult OnBeginReceiveRequest(TimeSpan timeout, AsyncCallback callback, object state);

				// Token: 0x060087A1 RID: 34721
				protected abstract RequestContext OnEndReceiveRequest(IAsyncResult result);

				// Token: 0x060087A2 RID: 34722 RVA: 0x001F8050 File Offset: 0x001F6250
				private bool HandleReplyComplete(IAsyncResult result)
				{
					bool flag = true;
					try
					{
						this.context.EndReply(result);
						flag = false;
					}
					catch (CommunicationException exception)
					{
						DiagnosticUtility.TraceHandledException(exception, TraceEventType.Information);
					}
					catch (TimeoutException ex)
					{
						if (TD.SendTimeoutIsEnabled())
						{
							TD.SendTimeout(ex.Message);
						}
						DiagnosticUtility.TraceHandledException(ex, TraceEventType.Information);
					}
					finally
					{
						if (flag)
						{
							this.context.Abort();
						}
					}
					return true;
				}

				// Token: 0x060087A3 RID: 34723 RVA: 0x001F80D0 File Offset: 0x001F62D0
				private bool HandleReceiveRequestComplete(IAsyncResult result)
				{
					this.context = this.OnEndReceiveRequest(result);
					if (this.context == null)
					{
						return true;
					}
					bool flag = false;
					IAsyncResult asyncResult = null;
					try
					{
						this.message = this.context.RequestMessage;
						this.message.Properties.Add(RequestContextMessageProperty.Name, new RequestContextMessageProperty(this.context));
						if (this.validateHeader)
						{
							PacketRoutableHeader.ValidateMessage(this.message);
						}
						try
						{
							asyncResult = this.context.BeginReply(null, this.timeoutHelper.RemainingTime(), ReplyOneWayChannelListener.ReplyOneWayInputChannel.ReceiveAsyncResultBase.onReply, this);
							flag = true;
						}
						catch (CommunicationException exception)
						{
							DiagnosticUtility.TraceHandledException(exception, TraceEventType.Information);
						}
						catch (TimeoutException ex)
						{
							if (TD.SendTimeoutIsEnabled())
							{
								TD.SendTimeout(ex.Message);
							}
							DiagnosticUtility.TraceHandledException(ex, TraceEventType.Information);
						}
					}
					finally
					{
						if (!flag)
						{
							this.context.Abort();
							if (this.message != null)
							{
								this.message.Close();
								this.message = null;
							}
						}
					}
					return asyncResult == null || (asyncResult.CompletedSynchronously && this.HandleReplyComplete(asyncResult));
				}

				// Token: 0x060087A4 RID: 34724 RVA: 0x001F81EC File Offset: 0x001F63EC
				private static void OnReceiveRequest(IAsyncResult result)
				{
					if (result.CompletedSynchronously)
					{
						return;
					}
					ReplyOneWayChannelListener.ReplyOneWayInputChannel.ReceiveAsyncResultBase receiveAsyncResultBase = (ReplyOneWayChannelListener.ReplyOneWayInputChannel.ReceiveAsyncResultBase)result.AsyncState;
					Exception exception = null;
					bool flag;
					try
					{
						flag = receiveAsyncResultBase.HandleReceiveRequestComplete(result);
					}
					catch (Exception ex)
					{
						if (Fx.IsFatal(ex))
						{
							throw;
						}
						flag = true;
						exception = ex;
					}
					if (flag)
					{
						receiveAsyncResultBase.Complete(false, exception);
					}
				}

				// Token: 0x060087A5 RID: 34725 RVA: 0x001F8248 File Offset: 0x001F6448
				private static void OnReply(IAsyncResult result)
				{
					if (result.CompletedSynchronously)
					{
						return;
					}
					ReplyOneWayChannelListener.ReplyOneWayInputChannel.ReceiveAsyncResultBase receiveAsyncResultBase = (ReplyOneWayChannelListener.ReplyOneWayInputChannel.ReceiveAsyncResultBase)result.AsyncState;
					Exception exception = null;
					bool flag;
					try
					{
						flag = receiveAsyncResultBase.HandleReplyComplete(result);
					}
					catch (Exception ex)
					{
						if (Fx.IsFatal(ex))
						{
							throw;
						}
						flag = true;
						exception = ex;
					}
					if (flag)
					{
						receiveAsyncResultBase.Complete(false, exception);
					}
				}

				// Token: 0x04004F0D RID: 20237
				private IReplyChannel innerChannel;

				// Token: 0x04004F0E RID: 20238
				private RequestContext context;

				// Token: 0x04004F0F RID: 20239
				private Message message;

				// Token: 0x04004F10 RID: 20240
				private TimeoutHelper timeoutHelper;

				// Token: 0x04004F11 RID: 20241
				private bool validateHeader;

				// Token: 0x04004F12 RID: 20242
				private static AsyncCallback onReceiveRequest = Fx.ThunkCallback(new AsyncCallback(ReplyOneWayChannelListener.ReplyOneWayInputChannel.ReceiveAsyncResultBase.OnReceiveRequest));

				// Token: 0x04004F13 RID: 20243
				private static AsyncCallback onReply = Fx.ThunkCallback(new AsyncCallback(ReplyOneWayChannelListener.ReplyOneWayInputChannel.ReceiveAsyncResultBase.OnReply));
			}
		}
	}
}
