using System;
using System.Threading;
using System.Threading.Tasks;

namespace System.Web.UI
{
	// Token: 0x02000238 RID: 568
	internal sealed class PageAsyncTaskTap : IPageAsyncTask
	{
		// Token: 0x06001ABD RID: 6845 RVA: 0x00053EAC File Offset: 0x000520AC
		public PageAsyncTaskTap(Func<CancellationToken, Task> handler)
		{
			this._handler = handler;
		}

		// Token: 0x06001ABE RID: 6846 RVA: 0x00053EBB File Offset: 0x000520BB
		public Task ExecuteAsync(object sender, EventArgs e, CancellationToken cancellationToken)
		{
			return this._handler(cancellationToken);
		}

		// Token: 0x04001854 RID: 6228
		private readonly Func<CancellationToken, Task> _handler;
	}
}
