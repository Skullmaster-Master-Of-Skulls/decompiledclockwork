using System;
using System.Diagnostics;
using System.Runtime;

namespace System.ServiceModel.Channels
{
	// Token: 0x0200093D RID: 2365
	internal class ReliableChannelOpenAsyncResult : AsyncResult
	{
		// Token: 0x06005ADD RID: 23261 RVA: 0x0014DAD4 File Offset: 0x0014BCD4
		public ReliableChannelOpenAsyncResult(IReliableChannelBinder binder, ChannelReliableSession session, TimeSpan timeout, AsyncCallback callback, object state) : base(callback, state)
		{
			this.binder = binder;
			this.session = session;
			this.timeoutHelper = new TimeoutHelper(timeout);
			bool flag = false;
			bool flag2 = true;
			Exception e = null;
			try
			{
				IAsyncResult asyncResult = this.binder.BeginOpen(this.timeoutHelper.RemainingTime(), ReliableChannelOpenAsyncResult.onBinderOpenComplete, this);
				flag2 = false;
				if (asyncResult.CompletedSynchronously)
				{
					flag = this.CompleteBinderOpen(true, asyncResult);
				}
			}
			catch (Exception exception)
			{
				if (Fx.IsFatal(exception))
				{
					throw;
				}
				if (flag2 || this.CloseBinder(e))
				{
					throw;
				}
			}
			finally
			{
				if (flag2)
				{
					this.binder.Abort();
				}
			}
			if (flag)
			{
				base.Complete(true);
			}
		}

		// Token: 0x06005ADE RID: 23262 RVA: 0x0014DB90 File Offset: 0x0014BD90
		private bool CloseBinder(Exception e)
		{
			IAsyncResult asyncResult = this.binder.BeginClose(this.timeoutHelper.RemainingTime(), Fx.ThunkCallback(new AsyncCallback(this.OnBinderCloseComplete)), e);
			if (asyncResult.CompletedSynchronously)
			{
				this.binder.EndClose(asyncResult);
				return true;
			}
			return false;
		}

		// Token: 0x06005ADF RID: 23263 RVA: 0x0014DBE0 File Offset: 0x0014BDE0
		private void CloseBinderAndComplete(Exception e)
		{
			bool flag = true;
			try
			{
				flag = this.CloseBinder(e);
			}
			catch (Exception exception)
			{
				if (Fx.IsFatal(exception))
				{
					throw;
				}
				if (DiagnosticUtility.ShouldTraceInformation)
				{
					DiagnosticUtility.TraceHandledException(exception, TraceEventType.Information);
				}
			}
			if (flag)
			{
				base.Complete(false, e);
			}
		}

		// Token: 0x06005AE0 RID: 23264 RVA: 0x0014DC30 File Offset: 0x0014BE30
		private bool CompleteBinderOpen(bool synchronous, IAsyncResult result)
		{
			this.binder.EndOpen(result);
			result = this.session.BeginOpen(this.timeoutHelper.RemainingTime(), ReliableChannelOpenAsyncResult.onSessionOpenComplete, this);
			if (result.CompletedSynchronously)
			{
				this.session.EndOpen(result);
				return true;
			}
			return false;
		}

		// Token: 0x06005AE1 RID: 23265 RVA: 0x0014DC7E File Offset: 0x0014BE7E
		public static void End(IAsyncResult result)
		{
			AsyncResult.End<ReliableChannelOpenAsyncResult>(result);
		}

		// Token: 0x06005AE2 RID: 23266 RVA: 0x0014DC88 File Offset: 0x0014BE88
		private void OnBinderCloseComplete(IAsyncResult result)
		{
			if (!result.CompletedSynchronously)
			{
				Exception exception = (Exception)result.AsyncState;
				try
				{
					this.binder.EndClose(result);
				}
				catch (Exception exception2)
				{
					if (Fx.IsFatal(exception2))
					{
						throw;
					}
					if (DiagnosticUtility.ShouldTraceInformation)
					{
						DiagnosticUtility.TraceHandledException(exception2, TraceEventType.Information);
					}
				}
				base.Complete(false, exception);
			}
		}

		// Token: 0x06005AE3 RID: 23267 RVA: 0x0014DCEC File Offset: 0x0014BEEC
		private static void OnBinderOpenComplete(IAsyncResult result)
		{
			if (!result.CompletedSynchronously)
			{
				ReliableChannelOpenAsyncResult reliableChannelOpenAsyncResult = (ReliableChannelOpenAsyncResult)result.AsyncState;
				bool flag = false;
				Exception ex = null;
				try
				{
					flag = reliableChannelOpenAsyncResult.CompleteBinderOpen(false, result);
				}
				catch (Exception ex2)
				{
					if (Fx.IsFatal(ex2))
					{
						throw;
					}
					ex = ex2;
				}
				if (flag)
				{
					reliableChannelOpenAsyncResult.Complete(false, ex);
					return;
				}
				if (ex != null)
				{
					reliableChannelOpenAsyncResult.CloseBinderAndComplete(ex);
				}
			}
		}

		// Token: 0x06005AE4 RID: 23268 RVA: 0x0014DD54 File Offset: 0x0014BF54
		private static void OnSessionOpenComplete(IAsyncResult result)
		{
			if (!result.CompletedSynchronously)
			{
				ReliableChannelOpenAsyncResult reliableChannelOpenAsyncResult = (ReliableChannelOpenAsyncResult)result.AsyncState;
				Exception ex = null;
				try
				{
					reliableChannelOpenAsyncResult.session.EndOpen(result);
				}
				catch (Exception ex2)
				{
					if (Fx.IsFatal(ex2))
					{
						throw;
					}
					ex = ex2;
				}
				if (ex != null)
				{
					reliableChannelOpenAsyncResult.CloseBinderAndComplete(ex);
					return;
				}
				reliableChannelOpenAsyncResult.Complete(false);
			}
		}

		// Token: 0x040036C0 RID: 14016
		private IReliableChannelBinder binder;

		// Token: 0x040036C1 RID: 14017
		private static AsyncCallback onBinderOpenComplete = Fx.ThunkCallback(new AsyncCallback(ReliableChannelOpenAsyncResult.OnBinderOpenComplete));

		// Token: 0x040036C2 RID: 14018
		private static AsyncCallback onSessionOpenComplete = Fx.ThunkCallback(new AsyncCallback(ReliableChannelOpenAsyncResult.OnSessionOpenComplete));

		// Token: 0x040036C3 RID: 14019
		private ChannelReliableSession session;

		// Token: 0x040036C4 RID: 14020
		private TimeoutHelper timeoutHelper;
	}
}
