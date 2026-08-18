using System;
using System.Runtime;
using System.ServiceModel.Channels;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x0200055B RID: 1371
	internal class ErrorHandlingReceiver
	{
		// Token: 0x06003574 RID: 13684 RVA: 0x000D014C File Offset: 0x000CE34C
		internal ErrorHandlingReceiver(IChannelBinder binder, ChannelDispatcher dispatcher)
		{
			this.binder = binder;
			this.dispatcher = dispatcher;
		}

		// Token: 0x06003575 RID: 13685 RVA: 0x000D0164 File Offset: 0x000CE364
		internal void Close()
		{
			try
			{
				this.binder.Channel.Close();
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

		// Token: 0x06003576 RID: 13686 RVA: 0x000D01A8 File Offset: 0x000CE3A8
		private void HandleError(Exception e)
		{
			if (this.dispatcher != null)
			{
				this.dispatcher.HandleError(e);
			}
		}

		// Token: 0x06003577 RID: 13687 RVA: 0x000D01BF File Offset: 0x000CE3BF
		private void HandleErrorOrAbort(Exception e)
		{
			if ((this.dispatcher == null || !this.dispatcher.HandleError(e)) && this.binder.HasSession)
			{
				this.binder.Abort();
			}
		}

		// Token: 0x06003578 RID: 13688 RVA: 0x000D01F0 File Offset: 0x000CE3F0
		internal bool TryReceive(TimeSpan timeout, out RequestContext requestContext)
		{
			bool result;
			try
			{
				result = this.binder.TryReceive(timeout, out requestContext);
			}
			catch (CommunicationObjectAbortedException)
			{
				requestContext = null;
				result = true;
			}
			catch (CommunicationObjectFaultedException)
			{
				requestContext = null;
				result = true;
			}
			catch (CommunicationException e)
			{
				this.HandleError(e);
				requestContext = null;
				result = false;
			}
			catch (TimeoutException e2)
			{
				this.HandleError(e2);
				requestContext = null;
				result = false;
			}
			catch (Exception ex)
			{
				if (Fx.IsFatal(ex))
				{
					throw;
				}
				this.HandleErrorOrAbort(ex);
				requestContext = null;
				result = false;
			}
			return result;
		}

		// Token: 0x06003579 RID: 13689 RVA: 0x000D0298 File Offset: 0x000CE498
		internal IAsyncResult BeginTryReceive(TimeSpan timeout, AsyncCallback callback, object state)
		{
			IAsyncResult result;
			try
			{
				result = this.binder.BeginTryReceive(timeout, callback, state);
			}
			catch (CommunicationObjectAbortedException)
			{
				result = new ErrorHandlingReceiver.ErrorHandlingCompletedAsyncResult(true, callback, state);
			}
			catch (CommunicationObjectFaultedException)
			{
				result = new ErrorHandlingReceiver.ErrorHandlingCompletedAsyncResult(true, callback, state);
			}
			catch (CommunicationException e)
			{
				this.HandleError(e);
				result = new ErrorHandlingReceiver.ErrorHandlingCompletedAsyncResult(false, callback, state);
			}
			catch (TimeoutException e2)
			{
				this.HandleError(e2);
				result = new ErrorHandlingReceiver.ErrorHandlingCompletedAsyncResult(false, callback, state);
			}
			catch (Exception ex)
			{
				if (Fx.IsFatal(ex))
				{
					throw;
				}
				this.HandleErrorOrAbort(ex);
				result = new ErrorHandlingReceiver.ErrorHandlingCompletedAsyncResult(false, callback, state);
			}
			return result;
		}

		// Token: 0x0600357A RID: 13690 RVA: 0x000D0354 File Offset: 0x000CE554
		internal bool EndTryReceive(IAsyncResult result, out RequestContext requestContext)
		{
			ErrorHandlingReceiver.ErrorHandlingCompletedAsyncResult errorHandlingCompletedAsyncResult = result as ErrorHandlingReceiver.ErrorHandlingCompletedAsyncResult;
			if (errorHandlingCompletedAsyncResult != null)
			{
				requestContext = null;
				return CompletedAsyncResult<bool>.End(errorHandlingCompletedAsyncResult);
			}
			bool result2;
			try
			{
				result2 = this.binder.EndTryReceive(result, out requestContext);
			}
			catch (CommunicationObjectAbortedException)
			{
				requestContext = null;
				result2 = true;
			}
			catch (CommunicationObjectFaultedException)
			{
				requestContext = null;
				result2 = true;
			}
			catch (CommunicationException e)
			{
				this.HandleError(e);
				requestContext = null;
				result2 = false;
			}
			catch (TimeoutException e2)
			{
				this.HandleError(e2);
				requestContext = null;
				result2 = false;
			}
			catch (Exception ex)
			{
				if (Fx.IsFatal(ex))
				{
					throw;
				}
				this.HandleErrorOrAbort(ex);
				requestContext = null;
				result2 = false;
			}
			return result2;
		}

		// Token: 0x0600357B RID: 13691 RVA: 0x000D0414 File Offset: 0x000CE614
		internal void WaitForMessage()
		{
			try
			{
				this.binder.WaitForMessage(TimeSpan.MaxValue);
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

		// Token: 0x0600357C RID: 13692 RVA: 0x000D048C File Offset: 0x000CE68C
		internal IAsyncResult BeginWaitForMessage(AsyncCallback callback, object state)
		{
			IAsyncResult result;
			try
			{
				result = this.binder.BeginWaitForMessage(TimeSpan.MaxValue, callback, state);
			}
			catch (CommunicationObjectAbortedException)
			{
				result = new ErrorHandlingReceiver.WaitCompletedAsyncResult(callback, state);
			}
			catch (CommunicationObjectFaultedException)
			{
				result = new ErrorHandlingReceiver.WaitCompletedAsyncResult(callback, state);
			}
			catch (CommunicationException e)
			{
				this.HandleError(e);
				result = new ErrorHandlingReceiver.WaitCompletedAsyncResult(callback, state);
			}
			catch (Exception ex)
			{
				if (Fx.IsFatal(ex))
				{
					throw;
				}
				this.HandleErrorOrAbort(ex);
				result = new ErrorHandlingReceiver.WaitCompletedAsyncResult(callback, state);
			}
			return result;
		}

		// Token: 0x0600357D RID: 13693 RVA: 0x000D0528 File Offset: 0x000CE728
		internal void EndWaitForMessage(IAsyncResult result)
		{
			ErrorHandlingReceiver.WaitCompletedAsyncResult waitCompletedAsyncResult = result as ErrorHandlingReceiver.WaitCompletedAsyncResult;
			if (waitCompletedAsyncResult != null)
			{
				CompletedAsyncResult.End(waitCompletedAsyncResult);
				return;
			}
			try
			{
				this.binder.EndWaitForMessage(result);
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

		// Token: 0x0400287E RID: 10366
		private ChannelDispatcher dispatcher;

		// Token: 0x0400287F RID: 10367
		private IChannelBinder binder;

		// Token: 0x02000C82 RID: 3202
		private class ErrorHandlingCompletedAsyncResult : CompletedAsyncResult<bool>
		{
			// Token: 0x06007887 RID: 30855 RVA: 0x001C2422 File Offset: 0x001C0622
			internal ErrorHandlingCompletedAsyncResult(bool data, AsyncCallback callback, object state) : base(data, callback, state)
			{
			}
		}

		// Token: 0x02000C83 RID: 3203
		private class WaitCompletedAsyncResult : CompletedAsyncResult
		{
			// Token: 0x06007888 RID: 30856 RVA: 0x001C242D File Offset: 0x001C062D
			internal WaitCompletedAsyncResult(AsyncCallback callback, object state) : base(callback, state)
			{
			}
		}
	}
}
