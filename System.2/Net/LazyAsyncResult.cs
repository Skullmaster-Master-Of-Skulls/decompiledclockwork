using System;
using System.Diagnostics;
using System.Threading;

namespace System.Net
{
	// Token: 0x020001BC RID: 444
	internal class LazyAsyncResult : IAsyncResult
	{
		// Token: 0x170003C5 RID: 965
		// (get) Token: 0x06001158 RID: 4440 RVA: 0x0005E6EC File Offset: 0x0005C8EC
		private static LazyAsyncResult.ThreadContext CurrentThreadContext
		{
			get
			{
				LazyAsyncResult.ThreadContext threadContext = LazyAsyncResult.t_ThreadContext;
				if (threadContext == null)
				{
					threadContext = new LazyAsyncResult.ThreadContext();
					LazyAsyncResult.t_ThreadContext = threadContext;
				}
				return threadContext;
			}
		}

		// Token: 0x06001159 RID: 4441 RVA: 0x0005E70F File Offset: 0x0005C90F
		internal LazyAsyncResult(object myObject, object myState, AsyncCallback myCallBack)
		{
			this.m_AsyncObject = myObject;
			this.m_AsyncState = myState;
			this.m_AsyncCallback = myCallBack;
			this.m_Result = DBNull.Value;
		}

		// Token: 0x0600115A RID: 4442 RVA: 0x0005E737 File Offset: 0x0005C937
		internal LazyAsyncResult(object myObject, object myState, AsyncCallback myCallBack, object result)
		{
			this.m_AsyncObject = myObject;
			this.m_AsyncState = myState;
			this.m_AsyncCallback = myCallBack;
			this.m_Result = result;
			this.m_IntCompleted = 1;
			if (this.m_AsyncCallback != null)
			{
				this.m_AsyncCallback(this);
			}
		}

		// Token: 0x170003C6 RID: 966
		// (get) Token: 0x0600115B RID: 4443 RVA: 0x0005E777 File Offset: 0x0005C977
		internal object AsyncObject
		{
			get
			{
				return this.m_AsyncObject;
			}
		}

		// Token: 0x170003C7 RID: 967
		// (get) Token: 0x0600115C RID: 4444 RVA: 0x0005E77F File Offset: 0x0005C97F
		public object AsyncState
		{
			get
			{
				return this.m_AsyncState;
			}
		}

		// Token: 0x170003C8 RID: 968
		// (get) Token: 0x0600115D RID: 4445 RVA: 0x0005E787 File Offset: 0x0005C987
		// (set) Token: 0x0600115E RID: 4446 RVA: 0x0005E78F File Offset: 0x0005C98F
		protected AsyncCallback AsyncCallback
		{
			get
			{
				return this.m_AsyncCallback;
			}
			set
			{
				this.m_AsyncCallback = value;
			}
		}

		// Token: 0x170003C9 RID: 969
		// (get) Token: 0x0600115F RID: 4447 RVA: 0x0005E798 File Offset: 0x0005C998
		public WaitHandle AsyncWaitHandle
		{
			get
			{
				this.m_UserEvent = true;
				if (this.m_IntCompleted == 0)
				{
					Interlocked.CompareExchange(ref this.m_IntCompleted, int.MinValue, 0);
				}
				ManualResetEvent manualResetEvent = (ManualResetEvent)this.m_Event;
				while (manualResetEvent == null)
				{
					this.LazilyCreateEvent(out manualResetEvent);
				}
				return manualResetEvent;
			}
		}

		// Token: 0x06001160 RID: 4448 RVA: 0x0005E7E4 File Offset: 0x0005C9E4
		private bool LazilyCreateEvent(out ManualResetEvent waitHandle)
		{
			waitHandle = new ManualResetEvent(false);
			bool result;
			try
			{
				if (Interlocked.CompareExchange(ref this.m_Event, waitHandle, null) == null)
				{
					if (this.InternalPeekCompleted)
					{
						waitHandle.Set();
					}
					result = true;
				}
				else
				{
					waitHandle.Close();
					waitHandle = (ManualResetEvent)this.m_Event;
					result = false;
				}
			}
			catch
			{
				this.m_Event = null;
				if (waitHandle != null)
				{
					waitHandle.Close();
				}
				throw;
			}
			return result;
		}

		// Token: 0x06001161 RID: 4449 RVA: 0x0005E85C File Offset: 0x0005CA5C
		[Conditional("DEBUG")]
		protected void DebugProtectState(bool protect)
		{
		}

		// Token: 0x170003CA RID: 970
		// (get) Token: 0x06001162 RID: 4450 RVA: 0x0005E860 File Offset: 0x0005CA60
		public bool CompletedSynchronously
		{
			get
			{
				int num = this.m_IntCompleted;
				if (num == 0)
				{
					num = Interlocked.CompareExchange(ref this.m_IntCompleted, int.MinValue, 0);
				}
				return num > 0;
			}
		}

		// Token: 0x170003CB RID: 971
		// (get) Token: 0x06001163 RID: 4451 RVA: 0x0005E890 File Offset: 0x0005CA90
		public bool IsCompleted
		{
			get
			{
				int num = this.m_IntCompleted;
				if (num == 0)
				{
					num = Interlocked.CompareExchange(ref this.m_IntCompleted, int.MinValue, 0);
				}
				return (num & int.MaxValue) != 0;
			}
		}

		// Token: 0x170003CC RID: 972
		// (get) Token: 0x06001164 RID: 4452 RVA: 0x0005E8C3 File Offset: 0x0005CAC3
		internal bool InternalPeekCompleted
		{
			get
			{
				return (this.m_IntCompleted & int.MaxValue) != 0;
			}
		}

		// Token: 0x170003CD RID: 973
		// (get) Token: 0x06001165 RID: 4453 RVA: 0x0005E8D4 File Offset: 0x0005CAD4
		// (set) Token: 0x06001166 RID: 4454 RVA: 0x0005E8EB File Offset: 0x0005CAEB
		internal object Result
		{
			get
			{
				if (this.m_Result != DBNull.Value)
				{
					return this.m_Result;
				}
				return null;
			}
			set
			{
				this.m_Result = value;
			}
		}

		// Token: 0x170003CE RID: 974
		// (get) Token: 0x06001167 RID: 4455 RVA: 0x0005E8F4 File Offset: 0x0005CAF4
		// (set) Token: 0x06001168 RID: 4456 RVA: 0x0005E8FC File Offset: 0x0005CAFC
		internal bool EndCalled
		{
			get
			{
				return this.m_EndCalled;
			}
			set
			{
				this.m_EndCalled = value;
			}
		}

		// Token: 0x170003CF RID: 975
		// (get) Token: 0x06001169 RID: 4457 RVA: 0x0005E905 File Offset: 0x0005CB05
		// (set) Token: 0x0600116A RID: 4458 RVA: 0x0005E90D File Offset: 0x0005CB0D
		internal int ErrorCode
		{
			get
			{
				return this.m_ErrorCode;
			}
			set
			{
				this.m_ErrorCode = value;
			}
		}

		// Token: 0x0600116B RID: 4459 RVA: 0x0005E918 File Offset: 0x0005CB18
		protected void ProtectedInvokeCallback(object result, IntPtr userToken)
		{
			if (result == DBNull.Value)
			{
				throw new ArgumentNullException("result");
			}
			if ((this.m_IntCompleted & 2147483647) == 0 && (Interlocked.Increment(ref this.m_IntCompleted) & 2147483647) == 1)
			{
				if (this.m_Result == DBNull.Value)
				{
					this.m_Result = result;
				}
				ManualResetEvent manualResetEvent = (ManualResetEvent)this.m_Event;
				if (manualResetEvent != null)
				{
					try
					{
						manualResetEvent.Set();
					}
					catch (ObjectDisposedException)
					{
					}
				}
				this.Complete(userToken);
			}
		}

		// Token: 0x0600116C RID: 4460 RVA: 0x0005E9A0 File Offset: 0x0005CBA0
		internal void InvokeCallback(object result)
		{
			this.ProtectedInvokeCallback(result, IntPtr.Zero);
		}

		// Token: 0x0600116D RID: 4461 RVA: 0x0005E9AE File Offset: 0x0005CBAE
		internal void InvokeCallback()
		{
			this.ProtectedInvokeCallback(null, IntPtr.Zero);
		}

		// Token: 0x0600116E RID: 4462 RVA: 0x0005E9BC File Offset: 0x0005CBBC
		protected virtual void Complete(IntPtr userToken)
		{
			bool flag = false;
			LazyAsyncResult.ThreadContext currentThreadContext = LazyAsyncResult.CurrentThreadContext;
			try
			{
				currentThreadContext.m_NestedIOCount++;
				if (this.m_AsyncCallback != null)
				{
					if (currentThreadContext.m_NestedIOCount >= 50)
					{
						ThreadPool.QueueUserWorkItem(new WaitCallback(this.WorkerThreadComplete));
						flag = true;
					}
					else
					{
						this.m_AsyncCallback(this);
					}
				}
			}
			finally
			{
				currentThreadContext.m_NestedIOCount--;
				if (!flag)
				{
					this.Cleanup();
				}
			}
		}

		// Token: 0x0600116F RID: 4463 RVA: 0x0005EA40 File Offset: 0x0005CC40
		private void WorkerThreadComplete(object state)
		{
			try
			{
				this.m_AsyncCallback(this);
			}
			finally
			{
				this.Cleanup();
			}
		}

		// Token: 0x06001170 RID: 4464 RVA: 0x0005EA74 File Offset: 0x0005CC74
		protected virtual void Cleanup()
		{
		}

		// Token: 0x06001171 RID: 4465 RVA: 0x0005EA76 File Offset: 0x0005CC76
		internal object InternalWaitForCompletion()
		{
			return this.WaitForCompletion(true);
		}

		// Token: 0x06001172 RID: 4466 RVA: 0x0005EA80 File Offset: 0x0005CC80
		private object WaitForCompletion(bool snap)
		{
			ManualResetEvent manualResetEvent = null;
			bool flag = false;
			if (!(snap ? this.IsCompleted : this.InternalPeekCompleted))
			{
				manualResetEvent = (ManualResetEvent)this.m_Event;
				if (manualResetEvent == null)
				{
					flag = this.LazilyCreateEvent(out manualResetEvent);
				}
			}
			if (manualResetEvent == null)
			{
				goto IL_75;
			}
			try
			{
				manualResetEvent.WaitOne(-1, false);
				goto IL_75;
			}
			catch (ObjectDisposedException)
			{
				goto IL_75;
			}
			finally
			{
				if (flag && !this.m_UserEvent)
				{
					ManualResetEvent manualResetEvent2 = (ManualResetEvent)this.m_Event;
					this.m_Event = null;
					if (!this.m_UserEvent)
					{
						manualResetEvent2.Close();
					}
				}
			}
			IL_6F:
			Thread.SpinWait(1);
			IL_75:
			if (this.m_Result != DBNull.Value)
			{
				return this.m_Result;
			}
			goto IL_6F;
		}

		// Token: 0x06001173 RID: 4467 RVA: 0x0005EB34 File Offset: 0x0005CD34
		internal void InternalCleanup()
		{
			if ((this.m_IntCompleted & 2147483647) == 0 && (Interlocked.Increment(ref this.m_IntCompleted) & 2147483647) == 1)
			{
				this.m_Result = null;
				this.Cleanup();
			}
		}

		// Token: 0x04001450 RID: 5200
		private const int c_HighBit = -2147483648;

		// Token: 0x04001451 RID: 5201
		private const int c_ForceAsyncCount = 50;

		// Token: 0x04001452 RID: 5202
		[ThreadStatic]
		private static LazyAsyncResult.ThreadContext t_ThreadContext;

		// Token: 0x04001453 RID: 5203
		private object m_AsyncObject;

		// Token: 0x04001454 RID: 5204
		private object m_AsyncState;

		// Token: 0x04001455 RID: 5205
		private AsyncCallback m_AsyncCallback;

		// Token: 0x04001456 RID: 5206
		private object m_Result;

		// Token: 0x04001457 RID: 5207
		private int m_ErrorCode;

		// Token: 0x04001458 RID: 5208
		private int m_IntCompleted;

		// Token: 0x04001459 RID: 5209
		private bool m_EndCalled;

		// Token: 0x0400145A RID: 5210
		private bool m_UserEvent;

		// Token: 0x0400145B RID: 5211
		private object m_Event;

		// Token: 0x02000750 RID: 1872
		private class ThreadContext
		{
			// Token: 0x04003208 RID: 12808
			internal int m_NestedIOCount;
		}
	}
}
