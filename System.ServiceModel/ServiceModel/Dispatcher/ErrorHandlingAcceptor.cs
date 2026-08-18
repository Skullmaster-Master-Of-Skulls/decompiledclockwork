using System;
using System.Runtime;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x0200055A RID: 1370
	internal class ErrorHandlingAcceptor
	{
		// Token: 0x0600356A RID: 13674 RVA: 0x000CFCD0 File Offset: 0x000CDED0
		internal ErrorHandlingAcceptor(IListenerBinder binder, ChannelDispatcher dispatcher)
		{
			this.binder = binder;
			this.dispatcher = dispatcher;
		}

		// Token: 0x0600356B RID: 13675 RVA: 0x000CFCEC File Offset: 0x000CDEEC
		internal void Close()
		{
			try
			{
				this.binder.Listener.Close();
			}
			catch (Exception ex)
			{
				if (Fx.IsFatal(ex))
				{
					throw;
				}
				this.HandleError(ex);
			}
		}

		// Token: 0x0600356C RID: 13676 RVA: 0x000CFD30 File Offset: 0x000CDF30
		private void HandleError(Exception e)
		{
			if (this.dispatcher != null)
			{
				this.dispatcher.HandleError(e);
			}
		}

		// Token: 0x0600356D RID: 13677 RVA: 0x000CFD47 File Offset: 0x000CDF47
		private void HandleErrorOrAbort(Exception e)
		{
			if (this.dispatcher != null)
			{
				this.dispatcher.HandleError(e);
			}
		}

		// Token: 0x0600356E RID: 13678 RVA: 0x000CFD60 File Offset: 0x000CDF60
		internal bool TryAccept(TimeSpan timeout, out IChannelBinder channelBinder)
		{
			bool result;
			try
			{
				channelBinder = this.binder.Accept(timeout);
				if (channelBinder != null)
				{
					this.dispatcher.PendingChannels.Add(channelBinder.Channel);
				}
				result = true;
			}
			catch (CommunicationObjectAbortedException)
			{
				channelBinder = null;
				result = true;
			}
			catch (CommunicationObjectFaultedException)
			{
				channelBinder = null;
				result = true;
			}
			catch (TimeoutException)
			{
				channelBinder = null;
				result = false;
			}
			catch (CommunicationException e)
			{
				this.HandleError(e);
				channelBinder = null;
				result = false;
			}
			catch (Exception ex)
			{
				if (Fx.IsFatal(ex))
				{
					throw;
				}
				this.HandleErrorOrAbort(ex);
				channelBinder = null;
				result = false;
			}
			return result;
		}

		// Token: 0x0600356F RID: 13679 RVA: 0x000CFE1C File Offset: 0x000CE01C
		internal IAsyncResult BeginTryAccept(TimeSpan timeout, AsyncCallback callback, object state)
		{
			IAsyncResult result;
			try
			{
				result = this.binder.BeginAccept(timeout, callback, state);
			}
			catch (CommunicationObjectAbortedException)
			{
				result = new ErrorHandlingAcceptor.ErrorHandlingCompletedAsyncResult(true, callback, state);
			}
			catch (CommunicationObjectFaultedException)
			{
				result = new ErrorHandlingAcceptor.ErrorHandlingCompletedAsyncResult(true, callback, state);
			}
			catch (TimeoutException)
			{
				result = new ErrorHandlingAcceptor.ErrorHandlingCompletedAsyncResult(false, callback, state);
			}
			catch (CommunicationException e)
			{
				this.HandleError(e);
				result = new ErrorHandlingAcceptor.ErrorHandlingCompletedAsyncResult(false, callback, state);
			}
			catch (Exception ex)
			{
				if (Fx.IsFatal(ex))
				{
					throw;
				}
				this.HandleErrorOrAbort(ex);
				result = new ErrorHandlingAcceptor.ErrorHandlingCompletedAsyncResult(false, callback, state);
			}
			return result;
		}

		// Token: 0x06003570 RID: 13680 RVA: 0x000CFED0 File Offset: 0x000CE0D0
		internal bool EndTryAccept(IAsyncResult result, out IChannelBinder channelBinder)
		{
			ErrorHandlingAcceptor.ErrorHandlingCompletedAsyncResult errorHandlingCompletedAsyncResult = result as ErrorHandlingAcceptor.ErrorHandlingCompletedAsyncResult;
			if (errorHandlingCompletedAsyncResult != null)
			{
				channelBinder = null;
				return CompletedAsyncResult<bool>.End(errorHandlingCompletedAsyncResult);
			}
			bool result2;
			try
			{
				channelBinder = this.binder.EndAccept(result);
				if (channelBinder != null)
				{
					this.dispatcher.PendingChannels.Add(channelBinder.Channel);
				}
				result2 = true;
			}
			catch (CommunicationObjectAbortedException)
			{
				channelBinder = null;
				result2 = true;
			}
			catch (CommunicationObjectFaultedException)
			{
				channelBinder = null;
				result2 = true;
			}
			catch (TimeoutException)
			{
				channelBinder = null;
				result2 = false;
			}
			catch (CommunicationException e)
			{
				this.HandleError(e);
				channelBinder = null;
				result2 = false;
			}
			catch (Exception ex)
			{
				if (Fx.IsFatal(ex))
				{
					throw;
				}
				this.HandleErrorOrAbort(ex);
				channelBinder = null;
				result2 = false;
			}
			return result2;
		}

		// Token: 0x06003571 RID: 13681 RVA: 0x000CFFA0 File Offset: 0x000CE1A0
		internal void WaitForChannel()
		{
			try
			{
				this.binder.Listener.WaitForChannel(TimeSpan.MaxValue);
			}
			catch (CommunicationObjectAbortedException)
			{
			}
			catch (CommunicationObjectFaultedException)
			{
			}
			catch (CommunicationException e)
			{
				this.HandleError(e);
			}
			catch (Exception ex)
			{
				if (Fx.IsFatal(ex))
				{
					throw;
				}
				this.HandleErrorOrAbort(ex);
			}
		}

		// Token: 0x06003572 RID: 13682 RVA: 0x000D0020 File Offset: 0x000CE220
		internal IAsyncResult BeginWaitForChannel(AsyncCallback callback, object state)
		{
			IAsyncResult result;
			try
			{
				result = this.binder.Listener.BeginWaitForChannel(TimeSpan.MaxValue, callback, state);
			}
			catch (CommunicationObjectAbortedException)
			{
				result = new ErrorHandlingAcceptor.WaitCompletedAsyncResult(callback, state);
			}
			catch (CommunicationObjectFaultedException)
			{
				result = new ErrorHandlingAcceptor.WaitCompletedAsyncResult(callback, state);
			}
			catch (CommunicationException e)
			{
				this.HandleError(e);
				result = new ErrorHandlingAcceptor.WaitCompletedAsyncResult(callback, state);
			}
			catch (Exception ex)
			{
				if (Fx.IsFatal(ex))
				{
					throw;
				}
				this.HandleErrorOrAbort(ex);
				result = new ErrorHandlingAcceptor.WaitCompletedAsyncResult(callback, state);
			}
			return result;
		}

		// Token: 0x06003573 RID: 13683 RVA: 0x000D00C0 File Offset: 0x000CE2C0
		internal void EndWaitForChannel(IAsyncResult result)
		{
			ErrorHandlingAcceptor.WaitCompletedAsyncResult waitCompletedAsyncResult = result as ErrorHandlingAcceptor.WaitCompletedAsyncResult;
			if (waitCompletedAsyncResult != null)
			{
				CompletedAsyncResult.End(waitCompletedAsyncResult);
				return;
			}
			try
			{
				this.binder.Listener.EndWaitForChannel(result);
			}
			catch (CommunicationObjectAbortedException)
			{
			}
			catch (CommunicationObjectFaultedException)
			{
			}
			catch (CommunicationException e)
			{
				this.HandleError(e);
			}
			catch (Exception ex)
			{
				if (Fx.IsFatal(ex))
				{
					throw;
				}
				this.HandleErrorOrAbort(ex);
			}
		}

		// Token: 0x0400287C RID: 10364
		private readonly ChannelDispatcher dispatcher;

		// Token: 0x0400287D RID: 10365
		private readonly IListenerBinder binder;

		// Token: 0x02000C80 RID: 3200
		private class ErrorHandlingCompletedAsyncResult : CompletedAsyncResult<bool>
		{
			// Token: 0x06007885 RID: 30853 RVA: 0x001C240D File Offset: 0x001C060D
			internal ErrorHandlingCompletedAsyncResult(bool data, AsyncCallback callback, object state) : base(data, callback, state)
			{
			}
		}

		// Token: 0x02000C81 RID: 3201
		private class WaitCompletedAsyncResult : CompletedAsyncResult
		{
			// Token: 0x06007886 RID: 30854 RVA: 0x001C2418 File Offset: 0x001C0618
			internal WaitCompletedAsyncResult(AsyncCallback callback, object state) : base(callback, state)
			{
			}
		}
	}
}
