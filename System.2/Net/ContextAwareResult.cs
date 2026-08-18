using System;
using System.Security;
using System.Security.Permissions;
using System.Security.Principal;
using System.Threading;

namespace System.Net
{
	// Token: 0x020001A9 RID: 425
	internal class ContextAwareResult : LazyAsyncResult
	{
		// Token: 0x060010C0 RID: 4288 RVA: 0x00059CDE File Offset: 0x00057EDE
		internal ContextAwareResult(object myObject, object myState, AsyncCallback myCallBack) : this(false, false, myObject, myState, myCallBack)
		{
		}

		// Token: 0x060010C1 RID: 4289 RVA: 0x00059CEB File Offset: 0x00057EEB
		internal ContextAwareResult(bool captureIdentity, bool forceCaptureContext, object myObject, object myState, AsyncCallback myCallBack) : this(captureIdentity, forceCaptureContext, false, myObject, myState, myCallBack)
		{
		}

		// Token: 0x060010C2 RID: 4290 RVA: 0x00059CFB File Offset: 0x00057EFB
		internal ContextAwareResult(bool captureIdentity, bool forceCaptureContext, bool threadSafeContextCopy, object myObject, object myState, AsyncCallback myCallBack) : base(myObject, myState, myCallBack)
		{
			if (forceCaptureContext)
			{
				this._Flags = ContextAwareResult.StateFlags.CaptureContext;
			}
			if (captureIdentity)
			{
				this._Flags |= ContextAwareResult.StateFlags.CaptureIdentity;
			}
			if (threadSafeContextCopy)
			{
				this._Flags |= ContextAwareResult.StateFlags.ThreadSafeContextCopy;
			}
		}

		// Token: 0x060010C3 RID: 4291 RVA: 0x00059D35 File Offset: 0x00057F35
		[SecurityPermission(SecurityAction.Assert, Flags = SecurityPermissionFlag.ControlPrincipal)]
		private void SafeCaptureIdenity()
		{
			this._Wi = WindowsIdentity.GetCurrent();
		}

		// Token: 0x170003AA RID: 938
		// (get) Token: 0x060010C4 RID: 4292 RVA: 0x00059D44 File Offset: 0x00057F44
		internal ExecutionContext ContextCopy
		{
			get
			{
				if (base.InternalPeekCompleted)
				{
					throw new InvalidOperationException(SR.GetString("net_completed_result"));
				}
				ExecutionContext context = this._Context;
				if (context != null)
				{
					return context.CreateCopy();
				}
				if ((this._Flags & ContextAwareResult.StateFlags.PostBlockFinished) == ContextAwareResult.StateFlags.None)
				{
					object @lock = this._Lock;
					lock (@lock)
					{
					}
				}
				if (base.InternalPeekCompleted)
				{
					throw new InvalidOperationException(SR.GetString("net_completed_result"));
				}
				context = this._Context;
				if (context != null)
				{
					return context.CreateCopy();
				}
				return null;
			}
		}

		// Token: 0x170003AB RID: 939
		// (get) Token: 0x060010C5 RID: 4293 RVA: 0x00059DE0 File Offset: 0x00057FE0
		internal WindowsIdentity Identity
		{
			get
			{
				if (base.InternalPeekCompleted)
				{
					throw new InvalidOperationException(SR.GetString("net_completed_result"));
				}
				if (this._Wi != null)
				{
					return this._Wi;
				}
				if ((this._Flags & ContextAwareResult.StateFlags.PostBlockFinished) == ContextAwareResult.StateFlags.None)
				{
					object @lock = this._Lock;
					lock (@lock)
					{
					}
				}
				if (base.InternalPeekCompleted)
				{
					throw new InvalidOperationException(SR.GetString("net_completed_result"));
				}
				return this._Wi;
			}
		}

		// Token: 0x060010C6 RID: 4294 RVA: 0x00059E6C File Offset: 0x0005806C
		internal object StartPostingAsyncOp()
		{
			return this.StartPostingAsyncOp(true);
		}

		// Token: 0x060010C7 RID: 4295 RVA: 0x00059E75 File Offset: 0x00058075
		internal object StartPostingAsyncOp(bool lockCapture)
		{
			this._Lock = (lockCapture ? new object() : null);
			this._Flags |= ContextAwareResult.StateFlags.PostBlockStarted;
			return this._Lock;
		}

		// Token: 0x060010C8 RID: 4296 RVA: 0x00059E9C File Offset: 0x0005809C
		internal bool FinishPostingAsyncOp()
		{
			if ((this._Flags & (ContextAwareResult.StateFlags.PostBlockStarted | ContextAwareResult.StateFlags.PostBlockFinished)) != ContextAwareResult.StateFlags.PostBlockStarted)
			{
				return false;
			}
			this._Flags |= ContextAwareResult.StateFlags.PostBlockFinished;
			ExecutionContext executionContext = null;
			return this.CaptureOrComplete(ref executionContext, false);
		}

		// Token: 0x060010C9 RID: 4297 RVA: 0x00059ED4 File Offset: 0x000580D4
		internal bool FinishPostingAsyncOp(ref CallbackClosure closure)
		{
			if ((this._Flags & (ContextAwareResult.StateFlags.PostBlockStarted | ContextAwareResult.StateFlags.PostBlockFinished)) != ContextAwareResult.StateFlags.PostBlockStarted)
			{
				return false;
			}
			this._Flags |= ContextAwareResult.StateFlags.PostBlockFinished;
			CallbackClosure callbackClosure = closure;
			ExecutionContext executionContext;
			if (callbackClosure == null)
			{
				executionContext = null;
			}
			else if (!callbackClosure.IsCompatible(base.AsyncCallback))
			{
				closure = null;
				executionContext = null;
			}
			else
			{
				base.AsyncCallback = callbackClosure.AsyncCallback;
				executionContext = callbackClosure.Context;
			}
			bool result = this.CaptureOrComplete(ref executionContext, true);
			if (closure == null && base.AsyncCallback != null && executionContext != null)
			{
				closure = new CallbackClosure(executionContext, base.AsyncCallback);
			}
			return result;
		}

		// Token: 0x060010CA RID: 4298 RVA: 0x00059F58 File Offset: 0x00058158
		protected override void Cleanup()
		{
			base.Cleanup();
			if (this._Wi != null)
			{
				this._Wi.Dispose();
				this._Wi = null;
			}
		}

		// Token: 0x060010CB RID: 4299 RVA: 0x00059F7C File Offset: 0x0005817C
		private bool CaptureOrComplete(ref ExecutionContext cachedContext, bool returnContext)
		{
			bool flag = base.AsyncCallback != null || (this._Flags & ContextAwareResult.StateFlags.CaptureContext) > ContextAwareResult.StateFlags.None;
			if ((this._Flags & ContextAwareResult.StateFlags.CaptureIdentity) != ContextAwareResult.StateFlags.None && !base.InternalPeekCompleted && (!flag || SecurityContext.IsWindowsIdentityFlowSuppressed()))
			{
				this.SafeCaptureIdenity();
			}
			if (flag && !base.InternalPeekCompleted)
			{
				if (cachedContext == null)
				{
					cachedContext = ExecutionContext.Capture();
				}
				if (cachedContext != null)
				{
					if (!returnContext)
					{
						this._Context = cachedContext;
						cachedContext = null;
					}
					else
					{
						this._Context = cachedContext.CreateCopy();
					}
				}
			}
			else
			{
				cachedContext = null;
			}
			if (base.CompletedSynchronously)
			{
				base.Complete(IntPtr.Zero);
				return true;
			}
			return false;
		}

		// Token: 0x060010CC RID: 4300 RVA: 0x0005A018 File Offset: 0x00058218
		protected override void Complete(IntPtr userToken)
		{
			if ((this._Flags & ContextAwareResult.StateFlags.PostBlockStarted) == ContextAwareResult.StateFlags.None)
			{
				base.Complete(userToken);
				return;
			}
			if (base.CompletedSynchronously)
			{
				return;
			}
			ExecutionContext context = this._Context;
			if (userToken != IntPtr.Zero || context == null)
			{
				base.Complete(userToken);
				return;
			}
			ExecutionContext.Run(((this._Flags & ContextAwareResult.StateFlags.ThreadSafeContextCopy) != ContextAwareResult.StateFlags.None) ? context.CreateCopy() : context, new ContextCallback(this.CompleteCallback), null);
		}

		// Token: 0x060010CD RID: 4301 RVA: 0x0005A086 File Offset: 0x00058286
		private void CompleteCallback(object state)
		{
			base.Complete(IntPtr.Zero);
		}

		// Token: 0x040013AE RID: 5038
		private volatile ExecutionContext _Context;

		// Token: 0x040013AF RID: 5039
		private object _Lock;

		// Token: 0x040013B0 RID: 5040
		private ContextAwareResult.StateFlags _Flags;

		// Token: 0x040013B1 RID: 5041
		private WindowsIdentity _Wi;

		// Token: 0x0200074D RID: 1869
		[Flags]
		private enum StateFlags
		{
			// Token: 0x040031FA RID: 12794
			None = 0,
			// Token: 0x040031FB RID: 12795
			CaptureIdentity = 1,
			// Token: 0x040031FC RID: 12796
			CaptureContext = 2,
			// Token: 0x040031FD RID: 12797
			ThreadSafeContextCopy = 4,
			// Token: 0x040031FE RID: 12798
			PostBlockStarted = 8,
			// Token: 0x040031FF RID: 12799
			PostBlockFinished = 16
		}
	}
}
