using System;
using System.Runtime.CompilerServices;
using System.Security.Permissions;
using System.Threading;
using System.Threading.Tasks;

namespace System.Web.Util
{
	// Token: 0x020001CF RID: 463
	internal struct WithinCancellableCallbackTaskAwaitable
	{
		// Token: 0x06001772 RID: 6002 RVA: 0x0004999B File Offset: 0x00047B9B
		public WithinCancellableCallbackTaskAwaitable(HttpContext context, TaskAwaiter innerAwaiter)
		{
			this._awaiter = new WithinCancellableCallbackTaskAwaitable.WithinCancellableCallbackTaskAwaiter(context, innerAwaiter);
		}

		// Token: 0x06001773 RID: 6003 RVA: 0x000499AA File Offset: 0x00047BAA
		public WithinCancellableCallbackTaskAwaitable.WithinCancellableCallbackTaskAwaiter GetAwaiter()
		{
			return this._awaiter;
		}

		// Token: 0x04001711 RID: 5905
		internal static readonly WithinCancellableCallbackTaskAwaitable Completed = new WithinCancellableCallbackTaskAwaitable(null, Task.FromResult<object>(null).GetAwaiter());

		// Token: 0x04001712 RID: 5906
		private readonly WithinCancellableCallbackTaskAwaitable.WithinCancellableCallbackTaskAwaiter _awaiter;

		// Token: 0x02000937 RID: 2359
		internal struct WithinCancellableCallbackTaskAwaiter : ICriticalNotifyCompletion, INotifyCompletion
		{
			// Token: 0x0600695B RID: 26971 RVA: 0x00177237 File Offset: 0x00175437
			internal WithinCancellableCallbackTaskAwaiter(HttpContext context, TaskAwaiter innerAwaiter)
			{
				this._context = context;
				this._innerAwaiter = innerAwaiter;
			}

			// Token: 0x17001D23 RID: 7459
			// (get) Token: 0x0600695C RID: 26972 RVA: 0x00177248 File Offset: 0x00175448
			public bool IsCompleted
			{
				get
				{
					return this._innerAwaiter.IsCompleted;
				}
			}

			// Token: 0x0600695D RID: 26973 RVA: 0x00177264 File Offset: 0x00175464
			public void GetResult()
			{
				this._innerAwaiter.GetResult();
				HttpContext context = this._context;
				if (context != null)
				{
					context.Response.ObserveResponseEndCalled();
				}
			}

			// Token: 0x0600695E RID: 26974 RVA: 0x00177294 File Offset: 0x00175494
			public void OnCompleted(Action continuation)
			{
				Action continuation2 = this.WrapContinuation(continuation);
				this._innerAwaiter.OnCompleted(continuation2);
			}

			// Token: 0x0600695F RID: 26975 RVA: 0x001772B8 File Offset: 0x001754B8
			[SecurityPermission(SecurityAction.LinkDemand, Unrestricted = true)]
			public void UnsafeOnCompleted(Action continuation)
			{
				Action continuation2 = this.WrapContinuation(continuation);
				this._innerAwaiter.UnsafeOnCompleted(continuation2);
			}

			// Token: 0x06006960 RID: 26976 RVA: 0x001772DC File Offset: 0x001754DC
			private Action WrapContinuation(Action continuation)
			{
				HttpContext context = this._context;
				if (context == null)
				{
					return continuation;
				}
				return delegate()
				{
					context.InvokeCancellableCallback(WithinCancellableCallbackTaskAwaitable.WithinCancellableCallbackTaskAwaiter._shunt, continuation);
				};
			}

			// Token: 0x04003797 RID: 14231
			private static readonly WaitCallback _shunt = delegate(object state)
			{
				((Action)state)();
			};

			// Token: 0x04003798 RID: 14232
			private readonly HttpContext _context;

			// Token: 0x04003799 RID: 14233
			private readonly TaskAwaiter _innerAwaiter;
		}
	}
}
