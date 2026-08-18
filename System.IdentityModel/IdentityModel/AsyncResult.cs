using System;
using System.Threading;

namespace System.IdentityModel
{
	// Token: 0x02000024 RID: 36
	public abstract class AsyncResult : IAsyncResult, IDisposable
	{
		// Token: 0x06000104 RID: 260 RVA: 0x00005480 File Offset: 0x00003680
		public static void End(IAsyncResult result)
		{
			if (result == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("result");
			}
			AsyncResult asyncResult = result as AsyncResult;
			if (asyncResult == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(SR.GetString("ID4001"), "result"));
			}
			if (asyncResult.endCalled)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ID4002")));
			}
			asyncResult.endCalled = true;
			if (!asyncResult.completed)
			{
				asyncResult.AsyncWaitHandle.WaitOne();
			}
			if (asyncResult.resetEvent != null)
			{
				((IDisposable)asyncResult.resetEvent).Dispose();
			}
			if (asyncResult.exception != null)
			{
				throw asyncResult.exception;
			}
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00005528 File Offset: 0x00003728
		protected AsyncResult() : this(null, null)
		{
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00005532 File Offset: 0x00003732
		protected AsyncResult(object state) : this(null, state)
		{
		}

		// Token: 0x06000107 RID: 263 RVA: 0x0000553C File Offset: 0x0000373C
		protected AsyncResult(AsyncCallback callback, object state)
		{
			this.thisLock = new object();
			this.callback = callback;
			this.state = state;
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00005560 File Offset: 0x00003760
		~AsyncResult()
		{
			this.Dispose(false);
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00005590 File Offset: 0x00003790
		protected void Complete(bool completedSynchronously)
		{
			this.Complete(completedSynchronously, null);
		}

		// Token: 0x0600010A RID: 266 RVA: 0x0000559C File Offset: 0x0000379C
		protected void Complete(bool completedSynchronously, Exception exception)
		{
			if (this.completed)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new AsynchronousOperationException(SR.GetString("ID4005")));
			}
			this.completedSync = completedSynchronously;
			this.exception = exception;
			if (completedSynchronously)
			{
				this.completed = true;
			}
			else
			{
				object obj = this.thisLock;
				lock (obj)
				{
					this.completed = true;
					if (this.resetEvent != null)
					{
						this.resetEvent.Set();
					}
				}
			}
			try
			{
				if (this.callback != null)
				{
					this.callback(this);
				}
			}
			catch (ThreadAbortException)
			{
			}
			catch (AsynchronousOperationException)
			{
				throw;
			}
			catch (Exception innerException)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new AsynchronousOperationException(SR.GetString("ID4003"), innerException));
			}
		}

		// Token: 0x0600010B RID: 267 RVA: 0x0000568C File Offset: 0x0000388C
		protected virtual void Dispose(bool isExplicitDispose)
		{
			if (!this.disposed && isExplicitDispose)
			{
				object obj = this.thisLock;
				lock (obj)
				{
					if (!this.disposed)
					{
						this.disposed = true;
						if (this.resetEvent != null)
						{
							this.resetEvent.Close();
						}
					}
				}
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x0600010C RID: 268 RVA: 0x000056F4 File Offset: 0x000038F4
		public object AsyncState
		{
			get
			{
				return this.state;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x0600010D RID: 269 RVA: 0x000056FC File Offset: 0x000038FC
		public virtual WaitHandle AsyncWaitHandle
		{
			get
			{
				if (this.resetEvent == null)
				{
					bool flag = this.completed;
					object obj = this.thisLock;
					lock (obj)
					{
						if (this.resetEvent == null)
						{
							this.resetEvent = new ManualResetEvent(this.completed);
						}
					}
					if (!flag && this.completed)
					{
						this.resetEvent.Set();
					}
				}
				return this.resetEvent;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x0600010E RID: 270 RVA: 0x0000577C File Offset: 0x0000397C
		public bool CompletedSynchronously
		{
			get
			{
				return this.completedSync;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x0600010F RID: 271 RVA: 0x00005784 File Offset: 0x00003984
		public bool IsCompleted
		{
			get
			{
				return this.completed;
			}
		}

		// Token: 0x06000110 RID: 272 RVA: 0x0000578C File Offset: 0x0000398C
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x040000D7 RID: 215
		private AsyncCallback callback;

		// Token: 0x040000D8 RID: 216
		private bool completed;

		// Token: 0x040000D9 RID: 217
		private bool completedSync;

		// Token: 0x040000DA RID: 218
		private bool disposed;

		// Token: 0x040000DB RID: 219
		private bool endCalled;

		// Token: 0x040000DC RID: 220
		private Exception exception;

		// Token: 0x040000DD RID: 221
		private ManualResetEvent resetEvent;

		// Token: 0x040000DE RID: 222
		private object state;

		// Token: 0x040000DF RID: 223
		private object thisLock;
	}
}
