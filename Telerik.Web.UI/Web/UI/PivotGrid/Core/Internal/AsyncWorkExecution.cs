using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

namespace Telerik.Web.UI.PivotGrid.Core.Internal
{
	// Token: 0x020006E5 RID: 1765
	[SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable", Justification = "Design choice.")]
	internal class AsyncWorkExecution : WorkExecutionContext
	{
		// Token: 0x06003EF5 RID: 16117 RVA: 0x000C874C File Offset: 0x000C694C
		public AsyncWorkExecution()
		{
			this.worker = new BackgroundWorker();
			this.worker.DoWork += this.WorkerDoWork;
			this.worker.RunWorkerCompleted += this.RunWorkerCompleted;
			this.worker.WorkerReportsProgress = false;
			this.worker.WorkerSupportsCancellation = false;
		}

		// Token: 0x06003EF6 RID: 16118 RVA: 0x000C87B0 File Offset: 0x000C69B0
		public override void Execute()
		{
			this.worker.RunWorkerAsync();
		}

		// Token: 0x06003EF7 RID: 16119 RVA: 0x000C87BD File Offset: 0x000C69BD
		private void RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
		}

		// Token: 0x06003EF8 RID: 16120 RVA: 0x000C87BF File Offset: 0x000C69BF
		private void WorkerDoWork(object sender, DoWorkEventArgs e)
		{
			if (base.ActionToExecute == null)
			{
				return;
			}
			base.ActionToExecute();
		}

		// Token: 0x040010B5 RID: 4277
		private readonly BackgroundWorker worker;
	}
}
