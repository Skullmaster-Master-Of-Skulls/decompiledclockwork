using System;
using System.Runtime;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000917 RID: 2327
	internal static class ReliableChannelBinderHelper
	{
		// Token: 0x0600590F RID: 22799 RVA: 0x00145E8F File Offset: 0x0014408F
		internal static IAsyncResult BeginCloseDuplexSessionChannel(ReliableChannelBinder<IDuplexSessionChannel> binder, IDuplexSessionChannel channel, TimeSpan timeout, AsyncCallback callback, object state)
		{
			return new ReliableChannelBinderHelper.CloseDuplexSessionChannelAsyncResult(binder, channel, timeout, callback, state);
		}

		// Token: 0x06005910 RID: 22800 RVA: 0x00145E9C File Offset: 0x0014409C
		internal static IAsyncResult BeginCloseReplySessionChannel(ReliableChannelBinder<IReplySessionChannel> binder, IReplySessionChannel channel, TimeSpan timeout, AsyncCallback callback, object state)
		{
			return new ReliableChannelBinderHelper.CloseReplySessionChannelAsyncResult(binder, channel, timeout, callback, state);
		}

		// Token: 0x06005911 RID: 22801 RVA: 0x00145EAC File Offset: 0x001440AC
		internal static void CloseDuplexSessionChannel(ReliableChannelBinder<IDuplexSessionChannel> binder, IDuplexSessionChannel channel, TimeSpan timeout)
		{
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			channel.Session.CloseOutputSession(timeoutHelper.RemainingTime());
			binder.WaitForPendingOperations(timeoutHelper.RemainingTime());
			TimeSpan timeSpan = timeoutHelper.RemainingTime();
			bool flag = timeSpan == TimeSpan.Zero;
			for (;;)
			{
				Message message = null;
				bool flag2 = true;
				try
				{
					bool flag3 = channel.TryReceive(timeSpan, out message);
					flag2 = false;
					if (flag3 && message == null)
					{
						channel.Close(timeoutHelper.RemainingTime());
						return;
					}
				}
				catch (Exception ex)
				{
					if (Fx.IsFatal(ex))
					{
						throw;
					}
					if (!flag2)
					{
						throw;
					}
					if (!ReliableChannelBinderHelper.MaskHandled(binder.DefaultMaskingMode) || !binder.IsHandleable(ex))
					{
						throw;
					}
					flag2 = false;
				}
				finally
				{
					if (message != null)
					{
						message.Close();
					}
					if (flag2)
					{
						channel.Abort();
					}
				}
				if (flag || channel.State != CommunicationState.Opened)
				{
					break;
				}
				timeSpan = timeoutHelper.RemainingTime();
				flag = (timeSpan == TimeSpan.Zero);
			}
			channel.Abort();
		}

		// Token: 0x06005912 RID: 22802 RVA: 0x00145FB0 File Offset: 0x001441B0
		internal static void CloseReplySessionChannel(ReliableChannelBinder<IReplySessionChannel> binder, IReplySessionChannel channel, TimeSpan timeout)
		{
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			binder.WaitForPendingOperations(timeoutHelper.RemainingTime());
			TimeSpan timeSpan = timeoutHelper.RemainingTime();
			bool flag = timeSpan == TimeSpan.Zero;
			for (;;)
			{
				RequestContext requestContext = null;
				bool flag2 = true;
				try
				{
					bool flag3 = channel.TryReceiveRequest(timeSpan, out requestContext);
					flag2 = false;
					if (flag3 && requestContext == null)
					{
						channel.Close(timeoutHelper.RemainingTime());
						return;
					}
				}
				catch (Exception ex)
				{
					if (Fx.IsFatal(ex))
					{
						throw;
					}
					if (!flag2)
					{
						throw;
					}
					if (!ReliableChannelBinderHelper.MaskHandled(binder.DefaultMaskingMode) || !binder.IsHandleable(ex))
					{
						throw;
					}
					flag2 = false;
				}
				finally
				{
					if (requestContext != null)
					{
						requestContext.RequestMessage.Close();
						requestContext.Close();
					}
					if (flag2)
					{
						channel.Abort();
					}
				}
				if (flag || channel.State != CommunicationState.Opened)
				{
					break;
				}
				timeSpan = timeoutHelper.RemainingTime();
				flag = (timeSpan == TimeSpan.Zero);
			}
			channel.Abort();
		}

		// Token: 0x06005913 RID: 22803 RVA: 0x001460AC File Offset: 0x001442AC
		internal static void EndCloseDuplexSessionChannel(IDuplexSessionChannel channel, IAsyncResult result)
		{
			ReliableChannelBinderHelper.CloseDuplexSessionChannelAsyncResult.End(result);
		}

		// Token: 0x06005914 RID: 22804 RVA: 0x001460B4 File Offset: 0x001442B4
		internal static void EndCloseReplySessionChannel(IReplySessionChannel channel, IAsyncResult result)
		{
			ReliableChannelBinderHelper.CloseReplySessionChannelAsyncResult.End(result);
		}

		// Token: 0x06005915 RID: 22805 RVA: 0x001460BC File Offset: 0x001442BC
		internal static bool MaskHandled(MaskingMode maskingMode)
		{
			return (maskingMode & MaskingMode.Handled) == MaskingMode.Handled;
		}

		// Token: 0x06005916 RID: 22806 RVA: 0x001460C4 File Offset: 0x001442C4
		internal static bool MaskUnhandled(MaskingMode maskingMode)
		{
			return (maskingMode & MaskingMode.Unhandled) == MaskingMode.Unhandled;
		}

		// Token: 0x02000DC1 RID: 3521
		private abstract class CloseInputSessionChannelAsyncResult<TChannel, TItem> : AsyncResult where TChannel : class, IChannel where TItem : class
		{
			// Token: 0x06007FC5 RID: 32709 RVA: 0x001DB647 File Offset: 0x001D9847
			protected CloseInputSessionChannelAsyncResult(ReliableChannelBinder<TChannel> binder, TChannel channel, TimeSpan timeout, AsyncCallback callback, object state) : base(callback, state)
			{
				this.binder = binder;
				this.channel = channel;
				this.timeoutHelper = new TimeoutHelper(timeout);
			}

			// Token: 0x17001C67 RID: 7271
			// (get) Token: 0x06007FC6 RID: 32710 RVA: 0x001DB66D File Offset: 0x001D986D
			protected TChannel Channel
			{
				get
				{
					return this.channel;
				}
			}

			// Token: 0x17001C68 RID: 7272
			// (get) Token: 0x06007FC7 RID: 32711 RVA: 0x001DB675 File Offset: 0x001D9875
			protected TimeSpan RemainingTime
			{
				get
				{
					return this.timeoutHelper.RemainingTime();
				}
			}

			// Token: 0x06007FC8 RID: 32712 RVA: 0x001DB684 File Offset: 0x001D9884
			protected bool Begin()
			{
				bool result = false;
				IAsyncResult asyncResult = this.binder.BeginWaitForPendingOperations(this.RemainingTime, ReliableChannelBinderHelper.CloseInputSessionChannelAsyncResult<TChannel, TItem>.onWaitForPendingOperationsCompleteStatic, this);
				if (asyncResult.CompletedSynchronously)
				{
					result = this.HandleWaitForPendingOperationsComplete(asyncResult);
				}
				return result;
			}

			// Token: 0x06007FC9 RID: 32713
			protected abstract IAsyncResult BeginTryInput(TimeSpan timeout, AsyncCallback callback, object state);

			// Token: 0x06007FCA RID: 32714
			protected abstract void DisposeItem(TItem item);

			// Token: 0x06007FCB RID: 32715
			protected abstract bool EndTryInput(IAsyncResult result, out TItem item);

			// Token: 0x06007FCC RID: 32716 RVA: 0x001DB6BC File Offset: 0x001D98BC
			private void HandleChannelCloseComplete(IAsyncResult result)
			{
				this.channel.EndClose(result);
			}

			// Token: 0x06007FCD RID: 32717 RVA: 0x001DB6D0 File Offset: 0x001D98D0
			private bool HandleInputComplete(IAsyncResult result, out bool gotEof)
			{
				TItem titem = default(TItem);
				bool flag = true;
				gotEof = false;
				bool result2;
				try
				{
					bool flag2 = this.EndTryInput(result, out titem);
					flag = false;
					if (!flag2 || titem != null)
					{
						if (this.lastReceive || this.channel.State != CommunicationState.Opened)
						{
							this.channel.Abort();
							result2 = true;
						}
						else
						{
							result2 = false;
						}
					}
					else
					{
						gotEof = true;
						result = this.channel.BeginClose(this.RemainingTime, ReliableChannelBinderHelper.CloseInputSessionChannelAsyncResult<TChannel, TItem>.onChannelCloseCompleteStatic, this);
						if (result.CompletedSynchronously)
						{
							this.HandleChannelCloseComplete(result);
							result2 = true;
						}
						else
						{
							result2 = false;
						}
					}
				}
				catch (Exception ex)
				{
					if (Fx.IsFatal(ex))
					{
						throw;
					}
					if (!flag)
					{
						throw;
					}
					if (!ReliableChannelBinderHelper.MaskHandled(this.binder.DefaultMaskingMode) || !this.binder.IsHandleable(ex))
					{
						throw;
					}
					if (this.lastReceive || this.channel.State != CommunicationState.Opened)
					{
						this.channel.Abort();
						result2 = true;
					}
					else
					{
						result2 = false;
					}
				}
				finally
				{
					if (titem != null)
					{
						this.DisposeItem(titem);
					}
					if (flag)
					{
						this.channel.Abort();
					}
				}
				return result2;
			}

			// Token: 0x06007FCE RID: 32718 RVA: 0x001DB824 File Offset: 0x001D9A24
			private bool HandleWaitForPendingOperationsComplete(IAsyncResult result)
			{
				this.binder.EndWaitForPendingOperations(result);
				return this.WaitForEof();
			}

			// Token: 0x06007FCF RID: 32719 RVA: 0x001DB838 File Offset: 0x001D9A38
			private static void OnChannelCloseCompleteStatic(IAsyncResult result)
			{
				if (result.CompletedSynchronously)
				{
					return;
				}
				ReliableChannelBinderHelper.CloseInputSessionChannelAsyncResult<TChannel, TItem> closeInputSessionChannelAsyncResult = (ReliableChannelBinderHelper.CloseInputSessionChannelAsyncResult<TChannel, TItem>)result.AsyncState;
				Exception exception = null;
				try
				{
					closeInputSessionChannelAsyncResult.HandleChannelCloseComplete(result);
				}
				catch (Exception ex)
				{
					if (Fx.IsFatal(ex))
					{
						throw;
					}
					exception = ex;
				}
				closeInputSessionChannelAsyncResult.Complete(false, exception);
			}

			// Token: 0x06007FD0 RID: 32720 RVA: 0x001DB88C File Offset: 0x001D9A8C
			private static void OnInputCompleteStatic(IAsyncResult result)
			{
				if (result.CompletedSynchronously)
				{
					return;
				}
				ReliableChannelBinderHelper.CloseInputSessionChannelAsyncResult<TChannel, TItem> closeInputSessionChannelAsyncResult = (ReliableChannelBinderHelper.CloseInputSessionChannelAsyncResult<TChannel, TItem>)result.AsyncState;
				bool flag = false;
				Exception ex = null;
				try
				{
					bool flag2;
					flag = closeInputSessionChannelAsyncResult.HandleInputComplete(result, out flag2);
					if (!flag && !flag2)
					{
						flag = closeInputSessionChannelAsyncResult.WaitForEof();
					}
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
					closeInputSessionChannelAsyncResult.Complete(false, ex);
				}
			}

			// Token: 0x06007FD1 RID: 32721 RVA: 0x001DB8FC File Offset: 0x001D9AFC
			private static void OnWaitForPendingOperationsCompleteStatic(IAsyncResult result)
			{
				if (result.CompletedSynchronously)
				{
					return;
				}
				ReliableChannelBinderHelper.CloseInputSessionChannelAsyncResult<TChannel, TItem> closeInputSessionChannelAsyncResult = (ReliableChannelBinderHelper.CloseInputSessionChannelAsyncResult<TChannel, TItem>)result.AsyncState;
				bool flag = false;
				Exception ex = null;
				try
				{
					flag = closeInputSessionChannelAsyncResult.HandleWaitForPendingOperationsComplete(result);
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
					closeInputSessionChannelAsyncResult.Complete(false, ex);
				}
			}

			// Token: 0x06007FD2 RID: 32722 RVA: 0x001DB95C File Offset: 0x001D9B5C
			private bool WaitForEof()
			{
				TimeSpan remainingTime = this.RemainingTime;
				this.lastReceive = (remainingTime == TimeSpan.Zero);
				bool flag;
				for (;;)
				{
					IAsyncResult asyncResult = null;
					try
					{
						asyncResult = this.BeginTryInput(remainingTime, ReliableChannelBinderHelper.CloseInputSessionChannelAsyncResult<TChannel, TItem>.onInputCompleteStatic, this);
					}
					catch (Exception ex)
					{
						if (Fx.IsFatal(ex))
						{
							throw;
						}
						if (!ReliableChannelBinderHelper.MaskHandled(this.binder.DefaultMaskingMode) || !this.binder.IsHandleable(ex))
						{
							throw;
						}
					}
					if (asyncResult != null)
					{
						if (!asyncResult.CompletedSynchronously)
						{
							return false;
						}
						bool flag2;
						flag = this.HandleInputComplete(asyncResult, out flag2);
						if (flag || flag2)
						{
							break;
						}
					}
					if (this.lastReceive || this.channel.State != CommunicationState.Opened)
					{
						goto IL_95;
					}
					remainingTime = this.RemainingTime;
					this.lastReceive = (remainingTime == TimeSpan.Zero);
				}
				return flag;
				IL_95:
				this.channel.Abort();
				return true;
			}

			// Token: 0x04004910 RID: 18704
			private static AsyncCallback onChannelCloseCompleteStatic = Fx.ThunkCallback(new AsyncCallback(ReliableChannelBinderHelper.CloseInputSessionChannelAsyncResult<TChannel, TItem>.OnChannelCloseCompleteStatic));

			// Token: 0x04004911 RID: 18705
			private static AsyncCallback onInputCompleteStatic = Fx.ThunkCallback(new AsyncCallback(ReliableChannelBinderHelper.CloseInputSessionChannelAsyncResult<TChannel, TItem>.OnInputCompleteStatic));

			// Token: 0x04004912 RID: 18706
			private static AsyncCallback onWaitForPendingOperationsCompleteStatic = Fx.ThunkCallback(new AsyncCallback(ReliableChannelBinderHelper.CloseInputSessionChannelAsyncResult<TChannel, TItem>.OnWaitForPendingOperationsCompleteStatic));

			// Token: 0x04004913 RID: 18707
			private ReliableChannelBinder<TChannel> binder;

			// Token: 0x04004914 RID: 18708
			private TChannel channel;

			// Token: 0x04004915 RID: 18709
			private bool lastReceive;

			// Token: 0x04004916 RID: 18710
			private TimeoutHelper timeoutHelper;
		}

		// Token: 0x02000DC2 RID: 3522
		private sealed class CloseDuplexSessionChannelAsyncResult : ReliableChannelBinderHelper.CloseInputSessionChannelAsyncResult<IDuplexSessionChannel, Message>
		{
			// Token: 0x06007FD4 RID: 32724 RVA: 0x001DBA90 File Offset: 0x001D9C90
			public CloseDuplexSessionChannelAsyncResult(ReliableChannelBinder<IDuplexSessionChannel> binder, IDuplexSessionChannel channel, TimeSpan timeout, AsyncCallback callback, object state) : base(binder, channel, timeout, callback, state)
			{
				bool flag = false;
				IAsyncResult asyncResult = base.Channel.Session.BeginCloseOutputSession(base.RemainingTime, ReliableChannelBinderHelper.CloseDuplexSessionChannelAsyncResult.onCloseOutputSessionCompleteStatic, this);
				if (asyncResult.CompletedSynchronously)
				{
					flag = this.HandleCloseOutputSessionComplete(asyncResult);
				}
				if (flag)
				{
					base.Complete(true);
				}
			}

			// Token: 0x06007FD5 RID: 32725 RVA: 0x001DBAE3 File Offset: 0x001D9CE3
			protected override IAsyncResult BeginTryInput(TimeSpan timeout, AsyncCallback callback, object state)
			{
				return base.Channel.BeginTryReceive(timeout, callback, state);
			}

			// Token: 0x06007FD6 RID: 32726 RVA: 0x001DBAF3 File Offset: 0x001D9CF3
			protected override void DisposeItem(Message item)
			{
				item.Close();
			}

			// Token: 0x06007FD7 RID: 32727 RVA: 0x001DBAFB File Offset: 0x001D9CFB
			public static void End(IAsyncResult result)
			{
				AsyncResult.End<ReliableChannelBinderHelper.CloseDuplexSessionChannelAsyncResult>(result);
			}

			// Token: 0x06007FD8 RID: 32728 RVA: 0x001DBB04 File Offset: 0x001D9D04
			protected override bool EndTryInput(IAsyncResult result, out Message item)
			{
				return base.Channel.EndTryReceive(result, out item);
			}

			// Token: 0x06007FD9 RID: 32729 RVA: 0x001DBB13 File Offset: 0x001D9D13
			private bool HandleCloseOutputSessionComplete(IAsyncResult result)
			{
				base.Channel.Session.EndCloseOutputSession(result);
				return base.Begin();
			}

			// Token: 0x06007FDA RID: 32730 RVA: 0x001DBB2C File Offset: 0x001D9D2C
			private static void OnCloseOutputSessionCompleteStatic(IAsyncResult result)
			{
				if (result.CompletedSynchronously)
				{
					return;
				}
				ReliableChannelBinderHelper.CloseDuplexSessionChannelAsyncResult closeDuplexSessionChannelAsyncResult = (ReliableChannelBinderHelper.CloseDuplexSessionChannelAsyncResult)result.AsyncState;
				bool flag = false;
				Exception ex = null;
				try
				{
					flag = closeDuplexSessionChannelAsyncResult.HandleCloseOutputSessionComplete(result);
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
					closeDuplexSessionChannelAsyncResult.Complete(false, ex);
				}
			}

			// Token: 0x04004917 RID: 18711
			private static AsyncCallback onCloseOutputSessionCompleteStatic = Fx.ThunkCallback(new AsyncCallback(ReliableChannelBinderHelper.CloseDuplexSessionChannelAsyncResult.OnCloseOutputSessionCompleteStatic));
		}

		// Token: 0x02000DC3 RID: 3523
		private sealed class CloseReplySessionChannelAsyncResult : ReliableChannelBinderHelper.CloseInputSessionChannelAsyncResult<IReplySessionChannel, RequestContext>
		{
			// Token: 0x06007FDC RID: 32732 RVA: 0x001DBBA4 File Offset: 0x001D9DA4
			public CloseReplySessionChannelAsyncResult(ReliableChannelBinder<IReplySessionChannel> binder, IReplySessionChannel channel, TimeSpan timeout, AsyncCallback callback, object state) : base(binder, channel, timeout, callback, state)
			{
				if (base.Begin())
				{
					base.Complete(true);
				}
			}

			// Token: 0x06007FDD RID: 32733 RVA: 0x001DBBC2 File Offset: 0x001D9DC2
			protected override IAsyncResult BeginTryInput(TimeSpan timeout, AsyncCallback callback, object state)
			{
				return base.Channel.BeginTryReceiveRequest(timeout, callback, state);
			}

			// Token: 0x06007FDE RID: 32734 RVA: 0x001DBBD2 File Offset: 0x001D9DD2
			protected override void DisposeItem(RequestContext item)
			{
				item.RequestMessage.Close();
				item.Close();
			}

			// Token: 0x06007FDF RID: 32735 RVA: 0x001DBBE5 File Offset: 0x001D9DE5
			public static void End(IAsyncResult result)
			{
				AsyncResult.End<ReliableChannelBinderHelper.CloseReplySessionChannelAsyncResult>(result);
			}

			// Token: 0x06007FE0 RID: 32736 RVA: 0x001DBBEE File Offset: 0x001D9DEE
			protected override bool EndTryInput(IAsyncResult result, out RequestContext item)
			{
				return base.Channel.EndTryReceiveRequest(result, out item);
			}
		}
	}
}
