using System;

namespace Telerik.Web.UI.PivotGrid.Core.Engine
{
	// Token: 0x020006A7 RID: 1703
	internal abstract class EngineTaskBase : IEngineTask
	{
		// Token: 0x140000A9 RID: 169
		// (add) Token: 0x06003D65 RID: 15717 RVA: 0x000C593C File Offset: 0x000C3B3C
		// (remove) Token: 0x06003D66 RID: 15718 RVA: 0x000C5974 File Offset: 0x000C3B74
		public event EventHandler<EngineTaskCompletedEventArgs> Completed;

		// Token: 0x140000AA RID: 170
		// (add) Token: 0x06003D67 RID: 15719 RVA: 0x000C59A9 File Offset: 0x000C3BA9
		// (remove) Token: 0x06003D68 RID: 15720 RVA: 0x000C59B2 File Offset: 0x000C3BB2
		event EventHandler<EngineTaskCompletedEventArgs> IEngineTask.Completed
		{
			add
			{
				this.Completed += value;
			}
			remove
			{
				this.Completed -= value;
			}
		}

		// Token: 0x17001422 RID: 5154
		// (get) Token: 0x06003D69 RID: 15721 RVA: 0x000C59BB File Offset: 0x000C3BBB
		// (set) Token: 0x06003D6A RID: 15722 RVA: 0x000C59C3 File Offset: 0x000C3BC3
		public PivotResultsProcessingState Result { get; protected set; }

		// Token: 0x17001423 RID: 5155
		// (get) Token: 0x06003D6B RID: 15723 RVA: 0x000C59CC File Offset: 0x000C3BCC
		object IEngineTask.Result
		{
			get
			{
				return this.Result;
			}
		}

		// Token: 0x06003D6C RID: 15724
		protected abstract void RunCore(PivotResultsProcessingState input);

		// Token: 0x06003D6D RID: 15725 RVA: 0x000C59D4 File Offset: 0x000C3BD4
		protected void CompleteWithError(Exception error)
		{
			EngineTaskCompletedEventArgs args = new EngineTaskCompletedEventArgs(error);
			this.OnCompleted(args);
		}

		// Token: 0x06003D6E RID: 15726 RVA: 0x000C59EF File Offset: 0x000C3BEF
		protected void OnCompleted(EngineTaskCompletedEventArgs args)
		{
			if (this.Completed != null)
			{
				this.Completed(this, args);
			}
		}

		// Token: 0x06003D6F RID: 15727 RVA: 0x000C5A08 File Offset: 0x000C3C08
		void IEngineTask.Run(object input)
		{
			PivotResultsProcessingState pivotResultsProcessingState = input as PivotResultsProcessingState;
			if (pivotResultsProcessingState == null)
			{
				ArgumentException ex = new ArgumentException("Input is not valid.");
				throw ex;
			}
			this.RunCore(pivotResultsProcessingState);
		}

		// Token: 0x06003D70 RID: 15728
		public abstract void Cancel();
	}
}
