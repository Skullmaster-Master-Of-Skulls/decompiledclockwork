using System;
using System.Runtime;
using System.Threading;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000936 RID: 2358
	internal class InterruptibleWaitObject
	{
		// Token: 0x06005A98 RID: 23192 RVA: 0x0014CC53 File Offset: 0x0014AE53
		public InterruptibleWaitObject(bool signaled) : this(signaled, true)
		{
		}

		// Token: 0x06005A99 RID: 23193 RVA: 0x0014CC5D File Offset: 0x0014AE5D
		public InterruptibleWaitObject(bool signaled, bool throwTimeoutByDefault)
		{
			this.set = signaled;
			this.throwTimeoutByDefault = throwTimeoutByDefault;
		}

		// Token: 0x1400003C RID: 60
		// (add) Token: 0x06005A9A RID: 23194 RVA: 0x0014CC88 File Offset: 0x0014AE88
		// (remove) Token: 0x06005A9B RID: 23195 RVA: 0x0014CCC0 File Offset: 0x0014AEC0
		private event WaitAsyncResult.AbortHandler Aborted;

		// Token: 0x1400003D RID: 61
		// (add) Token: 0x06005A9C RID: 23196 RVA: 0x0014CCF8 File Offset: 0x0014AEF8
		// (remove) Token: 0x06005A9D RID: 23197 RVA: 0x0014CD30 File Offset: 0x0014AF30
		private event WaitAsyncResult.AbortHandler Faulted;

		// Token: 0x1400003E RID: 62
		// (add) Token: 0x06005A9E RID: 23198 RVA: 0x0014CD68 File Offset: 0x0014AF68
		// (remove) Token: 0x06005A9F RID: 23199 RVA: 0x0014CDA0 File Offset: 0x0014AFA0
		private event WaitAsyncResult.SignaledHandler Signaled;

		// Token: 0x06005AA0 RID: 23200 RVA: 0x0014CDD8 File Offset: 0x0014AFD8
		public void Abort(CommunicationObject communicationObject)
		{
			if (communicationObject == null)
			{
				throw Fx.AssertAndThrow("Argument communicationObject cannot be null.");
			}
			object obj = this.thisLock;
			lock (obj)
			{
				if (this.aborted)
				{
					return;
				}
				this.communicationObject = communicationObject;
				this.aborted = true;
				this.InternalSet();
			}
			WaitAsyncResult.AbortHandler abortHandler = this.Aborted;
			if (abortHandler != null)
			{
				abortHandler(communicationObject);
			}
		}

		// Token: 0x06005AA1 RID: 23201 RVA: 0x0014CE50 File Offset: 0x0014B050
		public void Fault(CommunicationObject communicationObject)
		{
			if (communicationObject == null)
			{
				throw Fx.AssertAndThrow("Argument communicationObject cannot be null.");
			}
			object obj = this.thisLock;
			lock (obj)
			{
				if (this.aborted)
				{
					return;
				}
				this.communicationObject = communicationObject;
				this.aborted = false;
				this.InternalSet();
			}
			WaitAsyncResult.AbortHandler faulted = this.Faulted;
			if (faulted != null)
			{
				faulted(communicationObject);
			}
		}

		// Token: 0x06005AA2 RID: 23202 RVA: 0x0014CEC8 File Offset: 0x0014B0C8
		public IAsyncResult BeginWait(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return this.BeginWait(timeout, this.throwTimeoutByDefault, callback, state);
		}

		// Token: 0x06005AA3 RID: 23203 RVA: 0x0014CEDC File Offset: 0x0014B0DC
		public IAsyncResult BeginWait(TimeSpan timeout, bool throwTimeoutException, AsyncCallback callback, object state)
		{
			Exception ex = null;
			object obj = this.thisLock;
			lock (obj)
			{
				if (!this.set)
				{
					WaitAsyncResult waitAsyncResult = new WaitAsyncResult(timeout, throwTimeoutException, callback, state);
					this.Aborted += waitAsyncResult.OnAborted;
					this.Faulted += waitAsyncResult.OnFaulted;
					this.Signaled += waitAsyncResult.OnSignaled;
					waitAsyncResult.Begin();
					return waitAsyncResult;
				}
				if (this.communicationObject != null)
				{
					ex = this.GetException();
				}
			}
			if (ex != null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(ex);
			}
			return new CompletedAsyncResult(callback, state);
		}

		// Token: 0x06005AA4 RID: 23204 RVA: 0x0014CF98 File Offset: 0x0014B198
		public IAsyncResult BeginTryWait(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return this.BeginWait(timeout, false, callback, state);
		}

		// Token: 0x06005AA5 RID: 23205 RVA: 0x0014CFA4 File Offset: 0x0014B1A4
		public void EndWait(IAsyncResult result)
		{
			this.EndTryWait(result);
		}

		// Token: 0x06005AA6 RID: 23206 RVA: 0x0014CFAE File Offset: 0x0014B1AE
		public bool EndTryWait(IAsyncResult result)
		{
			if (result is CompletedAsyncResult)
			{
				CompletedAsyncResult.End(result);
				return true;
			}
			return WaitAsyncResult.End(result);
		}

		// Token: 0x06005AA7 RID: 23207 RVA: 0x0014CFC6 File Offset: 0x0014B1C6
		private Exception GetException()
		{
			CommunicationObject communicationObject = this.communicationObject;
			if (!this.aborted)
			{
				return this.communicationObject.GetTerminalException();
			}
			return this.communicationObject.CreateAbortedException();
		}

		// Token: 0x06005AA8 RID: 23208 RVA: 0x0014CFF0 File Offset: 0x0014B1F0
		private void InternalSet()
		{
			object obj = this.thisLock;
			lock (obj)
			{
				this.set = true;
				if (this.handle != null)
				{
					this.handle.Set();
				}
			}
		}

		// Token: 0x06005AA9 RID: 23209 RVA: 0x0014D048 File Offset: 0x0014B248
		public void Reset()
		{
			object obj = this.thisLock;
			lock (obj)
			{
				this.communicationObject = null;
				this.aborted = false;
				this.set = false;
				if (this.handle != null)
				{
					this.handle.Reset();
				}
			}
		}

		// Token: 0x06005AAA RID: 23210 RVA: 0x0014D0AC File Offset: 0x0014B2AC
		public void Set()
		{
			this.InternalSet();
			WaitAsyncResult.SignaledHandler signaled = this.Signaled;
			if (signaled != null)
			{
				signaled();
			}
		}

		// Token: 0x06005AAB RID: 23211 RVA: 0x0014D0CF File Offset: 0x0014B2CF
		public bool Wait(TimeSpan timeout)
		{
			return this.Wait(timeout, this.throwTimeoutByDefault);
		}

		// Token: 0x06005AAC RID: 23212 RVA: 0x0014D0E0 File Offset: 0x0014B2E0
		public bool Wait(TimeSpan timeout, bool throwTimeoutException)
		{
			object obj = this.thisLock;
			lock (obj)
			{
				if (this.set)
				{
					if (this.communicationObject != null)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(this.GetException());
					}
					return true;
				}
				else
				{
					if (this.handle == null)
					{
						this.handle = new ManualResetEvent(false);
					}
					this.syncWaiters++;
				}
			}
			try
			{
				if (!TimeoutHelper.WaitOne(this.handle, timeout))
				{
					if (throwTimeoutException)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new TimeoutException(SR.GetString("TimeoutOnOperation", new object[]
						{
							timeout
						})));
					}
					return false;
				}
			}
			finally
			{
				object obj2 = this.thisLock;
				lock (obj2)
				{
					this.syncWaiters--;
					if (this.syncWaiters == 0)
					{
						this.handle.Close();
						this.handle = null;
					}
				}
			}
			if (this.communicationObject != null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(this.GetException());
			}
			return true;
		}

		// Token: 0x040036A8 RID: 13992
		private bool aborted;

		// Token: 0x040036A9 RID: 13993
		private CommunicationObject communicationObject;

		// Token: 0x040036AA RID: 13994
		private ManualResetEvent handle;

		// Token: 0x040036AB RID: 13995
		private bool set;

		// Token: 0x040036AC RID: 13996
		private int syncWaiters;

		// Token: 0x040036AD RID: 13997
		private object thisLock = new object();

		// Token: 0x040036AE RID: 13998
		private bool throwTimeoutByDefault = true;
	}
}
