using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace System.Web.UI
{
	// Token: 0x02000239 RID: 569
	internal sealed class PageAsyncTaskApm : IPageAsyncTask
	{
		// Token: 0x06001ABF RID: 6847 RVA: 0x00053EC9 File Offset: 0x000520C9
		public PageAsyncTaskApm(BeginEventHandler beginHandler, EndEventHandler endHandler, object state)
		{
			this._beginHandler = beginHandler;
			this._endHandler = endHandler;
			this._state = state;
		}

		// Token: 0x06001AC0 RID: 6848 RVA: 0x00053EE8 File Offset: 0x000520E8
		public Task ExecuteAsync(object sender, EventArgs e, CancellationToken cancellationToken)
		{
			PageAsyncTaskApm.<ExecuteAsync>d__4 <ExecuteAsync>d__;
			<ExecuteAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ExecuteAsync>d__.<>4__this = this;
			<ExecuteAsync>d__.sender = sender;
			<ExecuteAsync>d__.e = e;
			<ExecuteAsync>d__.<>1__state = -1;
			<ExecuteAsync>d__.<>t__builder.Start<PageAsyncTaskApm.<ExecuteAsync>d__4>(ref <ExecuteAsync>d__);
			return <ExecuteAsync>d__.<>t__builder.Task;
		}

		// Token: 0x04001855 RID: 6229
		private readonly BeginEventHandler _beginHandler;

		// Token: 0x04001856 RID: 6230
		private readonly EndEventHandler _endHandler;

		// Token: 0x04001857 RID: 6231
		private readonly object _state;
	}
}
