using System;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Util;

namespace System.Web.UI
{
	// Token: 0x02000237 RID: 567
	public sealed class PageAsyncTask
	{
		// Token: 0x06001AAA RID: 6826 RVA: 0x00053D40 File Offset: 0x00051F40
		public PageAsyncTask(BeginEventHandler beginHandler, EndEventHandler endHandler, EndEventHandler timeoutHandler, object state) : this(beginHandler, endHandler, timeoutHandler, state, false)
		{
		}

		// Token: 0x06001AAB RID: 6827 RVA: 0x00053D4E File Offset: 0x00051F4E
		public PageAsyncTask(BeginEventHandler beginHandler, EndEventHandler endHandler, EndEventHandler timeoutHandler, object state, bool executeInParallel) : this(beginHandler, endHandler, timeoutHandler, state, executeInParallel, SynchronizationContextUtil.CurrentMode)
		{
		}

		// Token: 0x06001AAC RID: 6828 RVA: 0x00053D64 File Offset: 0x00051F64
		internal PageAsyncTask(BeginEventHandler beginHandler, EndEventHandler endHandler, EndEventHandler timeoutHandler, object state, bool executeInParallel, SynchronizationContextMode currentMode)
		{
			if (beginHandler == null)
			{
				throw new ArgumentNullException("beginHandler");
			}
			if (endHandler == null)
			{
				throw new ArgumentNullException("endHandler");
			}
			if (timeoutHandler != null || executeInParallel)
			{
				SynchronizationContextUtil.ValidateMode(currentMode, SynchronizationContextMode.Legacy, "SynchronizationContextUtil_PageAsyncTaskTimeoutHandlerParallelNotCompatible");
			}
			this.BeginHandler = beginHandler;
			this.EndHandler = endHandler;
			this.TimeoutHandler = timeoutHandler;
			this.State = state;
			this.ExecuteInParallel = executeInParallel;
		}

		// Token: 0x06001AAD RID: 6829 RVA: 0x00053DCE File Offset: 0x00051FCE
		public PageAsyncTask(Func<Task> handler) : this(PageAsyncTask.WrapParameterlessTaskHandler(handler))
		{
		}

		// Token: 0x06001AAE RID: 6830 RVA: 0x00053DDC File Offset: 0x00051FDC
		public PageAsyncTask(Func<CancellationToken, Task> handler) : this(handler, SynchronizationContextUtil.CurrentMode)
		{
		}

		// Token: 0x06001AAF RID: 6831 RVA: 0x00053DEA File Offset: 0x00051FEA
		internal PageAsyncTask(Func<CancellationToken, Task> handler, SynchronizationContextMode currentMode)
		{
			if (handler == null)
			{
				throw new ArgumentNullException("handler");
			}
			SynchronizationContextUtil.ValidateMode(currentMode, SynchronizationContextMode.Normal, "SynchronizationContextUtil_TaskReturningPageAsyncMethodsNotCompatible");
			this.TaskHandler = handler;
		}

		// Token: 0x1700077E RID: 1918
		// (get) Token: 0x06001AB0 RID: 6832 RVA: 0x00053E13 File Offset: 0x00052013
		// (set) Token: 0x06001AB1 RID: 6833 RVA: 0x00053E1B File Offset: 0x0005201B
		public BeginEventHandler BeginHandler { get; private set; }

		// Token: 0x1700077F RID: 1919
		// (get) Token: 0x06001AB2 RID: 6834 RVA: 0x00053E24 File Offset: 0x00052024
		// (set) Token: 0x06001AB3 RID: 6835 RVA: 0x00053E2C File Offset: 0x0005202C
		public EndEventHandler EndHandler { get; private set; }

		// Token: 0x17000780 RID: 1920
		// (get) Token: 0x06001AB4 RID: 6836 RVA: 0x00053E35 File Offset: 0x00052035
		// (set) Token: 0x06001AB5 RID: 6837 RVA: 0x00053E3D File Offset: 0x0005203D
		public bool ExecuteInParallel { get; private set; }

		// Token: 0x17000781 RID: 1921
		// (get) Token: 0x06001AB6 RID: 6838 RVA: 0x00053E46 File Offset: 0x00052046
		// (set) Token: 0x06001AB7 RID: 6839 RVA: 0x00053E4E File Offset: 0x0005204E
		public object State { get; private set; }

		// Token: 0x17000782 RID: 1922
		// (get) Token: 0x06001AB8 RID: 6840 RVA: 0x00053E57 File Offset: 0x00052057
		// (set) Token: 0x06001AB9 RID: 6841 RVA: 0x00053E5F File Offset: 0x0005205F
		public EndEventHandler TimeoutHandler { get; private set; }

		// Token: 0x17000783 RID: 1923
		// (get) Token: 0x06001ABA RID: 6842 RVA: 0x00053E68 File Offset: 0x00052068
		// (set) Token: 0x06001ABB RID: 6843 RVA: 0x00053E70 File Offset: 0x00052070
		internal Func<CancellationToken, Task> TaskHandler { get; private set; }

		// Token: 0x06001ABC RID: 6844 RVA: 0x00053E7C File Offset: 0x0005207C
		private static Func<CancellationToken, Task> WrapParameterlessTaskHandler(Func<Task> handler)
		{
			if (handler == null)
			{
				return null;
			}
			return (CancellationToken _) => handler();
		}
	}
}
