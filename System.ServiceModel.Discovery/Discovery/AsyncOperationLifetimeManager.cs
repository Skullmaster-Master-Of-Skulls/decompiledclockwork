using System;
using System.Collections.Generic;
using System.Runtime;
using System.Threading;
using System.Xml;

namespace System.ServiceModel.Discovery
{
	// Token: 0x0200000A RID: 10
	internal class AsyncOperationLifetimeManager
	{
		// Token: 0x06000094 RID: 148 RVA: 0x000032E7 File Offset: 0x000014E7
		public AsyncOperationLifetimeManager()
		{
			this.thisLock = new object();
			this.activeOperations = new Dictionary<UniqueId, AsyncOperationContext>();
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000095 RID: 149 RVA: 0x00003305 File Offset: 0x00001505
		public bool IsAborted
		{
			get
			{
				return this.isAborted;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000096 RID: 150 RVA: 0x0000330D File Offset: 0x0000150D
		public bool IsClosed
		{
			get
			{
				return this.closeHandle != null;
			}
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00003318 File Offset: 0x00001518
		public bool TryAdd(AsyncOperationContext context)
		{
			object obj = this.thisLock;
			lock (obj)
			{
				if (this.IsAborted || this.IsClosed)
				{
					return false;
				}
				if (this.activeOperations.ContainsKey(context.OperationId))
				{
					return false;
				}
				this.activeOperations.Add(context.OperationId, context);
			}
			return true;
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00003394 File Offset: 0x00001594
		public AsyncOperationContext[] Abort()
		{
			AsyncOperationContext[] array = null;
			bool flag = false;
			object obj = this.thisLock;
			lock (obj)
			{
				if (this.IsAborted)
				{
					return new AsyncOperationContext[0];
				}
				this.isAborted = true;
				array = new AsyncOperationContext[this.activeOperations.Count];
				this.activeOperations.Values.CopyTo(array, 0);
				this.activeOperations.Clear();
				flag = (this.closeHandle != null);
			}
			if (flag)
			{
				this.closeHandle.Set();
			}
			return array;
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00003438 File Offset: 0x00001638
		public bool TryLookup(UniqueId operationId, out AsyncOperationContext context)
		{
			object obj = this.thisLock;
			bool result;
			lock (obj)
			{
				result = this.activeOperations.TryGetValue(operationId, out context);
			}
			return result;
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00003484 File Offset: 0x00001684
		public bool TryLookup<T>(UniqueId operationId, out T context) where T : AsyncOperationContext
		{
			AsyncOperationContext asyncOperationContext = null;
			if (this.TryLookup(operationId, out asyncOperationContext))
			{
				context = (asyncOperationContext as T);
				if (context != null)
				{
					return true;
				}
			}
			context = default(T);
			return false;
		}

		// Token: 0x0600009B RID: 155 RVA: 0x000034C8 File Offset: 0x000016C8
		public T Remove<T>(UniqueId operationId) where T : AsyncOperationContext
		{
			AsyncOperationContext asyncOperationContext = null;
			bool flag = false;
			object obj = this.thisLock;
			lock (obj)
			{
				if (this.activeOperations.TryGetValue(operationId, out asyncOperationContext) && asyncOperationContext is T)
				{
					this.activeOperations.Remove(operationId);
					flag = (this.closeHandle != null && this.activeOperations.Count == 0);
				}
				else
				{
					asyncOperationContext = null;
				}
			}
			if (flag)
			{
				this.closeHandle.Set();
			}
			return asyncOperationContext as T;
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00003564 File Offset: 0x00001764
		public bool TryRemoveUnique(object userState, out AsyncOperationContext context)
		{
			bool flag = false;
			bool flag2 = false;
			context = null;
			object obj = this.thisLock;
			lock (obj)
			{
				foreach (AsyncOperationContext asyncOperationContext in this.activeOperations.Values)
				{
					if (object.Equals(asyncOperationContext.UserState, userState))
					{
						if (flag)
						{
							flag = false;
							break;
						}
						context = asyncOperationContext;
						flag = true;
					}
				}
				if (flag)
				{
					this.activeOperations.Remove(context.OperationId);
					flag2 = (this.closeHandle != null && this.activeOperations.Count == 0);
				}
			}
			if (flag2)
			{
				this.closeHandle.Set();
			}
			return flag;
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00003644 File Offset: 0x00001844
		public void Close(TimeSpan timeout)
		{
			this.InitializeCloseHandle();
			if (!this.closeHandle.Wait(timeout))
			{
				throw FxTrace.Exception.AsError(new TimeoutException(SR.TimeoutOnOperation(timeout)));
			}
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00003675 File Offset: 0x00001875
		public IAsyncResult BeginClose(TimeSpan timeout, AsyncCallback callback, object state)
		{
			this.InitializeCloseHandle();
			return new AsyncOperationLifetimeManager.CloseAsyncResult(this.closeHandle, timeout, callback, state);
		}

		// Token: 0x0600009F RID: 159 RVA: 0x0000368B File Offset: 0x0000188B
		public void EndClose(IAsyncResult result)
		{
			AsyncOperationLifetimeManager.CloseAsyncResult.End(result);
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00003694 File Offset: 0x00001894
		private void InitializeCloseHandle()
		{
			bool flag = false;
			object obj = this.thisLock;
			lock (obj)
			{
				this.closeHandle = new AsyncWaitHandle(EventResetMode.ManualReset);
				flag = (this.activeOperations.Count == 0);
				if (this.IsAborted)
				{
					flag = true;
				}
			}
			if (flag)
			{
				this.closeHandle.Set();
			}
		}

		// Token: 0x04000029 RID: 41
		private object thisLock;

		// Token: 0x0400002A RID: 42
		private bool isAborted;

		// Token: 0x0400002B RID: 43
		private AsyncWaitHandle closeHandle;

		// Token: 0x0400002C RID: 44
		private Dictionary<UniqueId, AsyncOperationContext> activeOperations;

		// Token: 0x020000C5 RID: 197
		private class CloseAsyncResult : AsyncResult
		{
			// Token: 0x060007C3 RID: 1987 RVA: 0x0001432C File Offset: 0x0001252C
			internal CloseAsyncResult(AsyncWaitHandle asyncWaitHandle, TimeSpan timeout, AsyncCallback callback, object state) : base(callback, state)
			{
				this.asyncWaitHandle = asyncWaitHandle;
				if (this.asyncWaitHandle.WaitAsync(AsyncOperationLifetimeManager.CloseAsyncResult.onWaitCompleted, this, timeout))
				{
					base.Complete(true);
				}
			}

			// Token: 0x060007C4 RID: 1988 RVA: 0x0001435C File Offset: 0x0001255C
			private static void OnWaitCompleted(object state, TimeoutException asyncException)
			{
				AsyncOperationLifetimeManager.CloseAsyncResult closeAsyncResult = (AsyncOperationLifetimeManager.CloseAsyncResult)state;
				closeAsyncResult.Complete(false, asyncException);
			}

			// Token: 0x060007C5 RID: 1989 RVA: 0x00014378 File Offset: 0x00012578
			internal static void End(IAsyncResult result)
			{
				AsyncResult.End<AsyncOperationLifetimeManager.CloseAsyncResult>(result);
			}

			// Token: 0x040001E0 RID: 480
			private static Action<object, TimeoutException> onWaitCompleted = new Action<object, TimeoutException>(AsyncOperationLifetimeManager.CloseAsyncResult.OnWaitCompleted);

			// Token: 0x040001E1 RID: 481
			private AsyncWaitHandle asyncWaitHandle;
		}
	}
}
