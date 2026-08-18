using System;

namespace Telerik.Web.UI.PivotGrid.Core.Internal
{
	// Token: 0x020006E4 RID: 1764
	internal abstract class WorkExecutionContext
	{
		// Token: 0x140000AC RID: 172
		// (add) Token: 0x06003EED RID: 16109 RVA: 0x000C8694 File Offset: 0x000C6894
		// (remove) Token: 0x06003EEE RID: 16110 RVA: 0x000C86CC File Offset: 0x000C68CC
		public event EventHandler<EventArgs> Completed;

		// Token: 0x1700148A RID: 5258
		// (get) Token: 0x06003EEF RID: 16111 RVA: 0x000C8701 File Offset: 0x000C6901
		// (set) Token: 0x06003EF0 RID: 16112 RVA: 0x000C8709 File Offset: 0x000C6909
		public Action ActionToExecute { get; set; }

		// Token: 0x06003EF1 RID: 16113 RVA: 0x000C8712 File Offset: 0x000C6912
		public static WorkExecutionContext GetContextForCurrentExecutionStrategy()
		{
			if (GlobalOptions.PreferredExecutionStrategy == OperationExecutionStrategy.Asynchronous)
			{
				return new AsyncWorkExecution();
			}
			return new BlockingWorkExecution();
		}

		// Token: 0x06003EF2 RID: 16114
		public abstract void Execute();

		// Token: 0x06003EF3 RID: 16115 RVA: 0x000C8726 File Offset: 0x000C6926
		protected virtual void OnCompleted()
		{
			if (this.Completed != null)
			{
				this.Completed(this, EventArgs.Empty);
			}
		}
	}
}
