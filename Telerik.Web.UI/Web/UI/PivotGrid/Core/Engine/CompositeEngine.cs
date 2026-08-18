using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.PivotGrid.Core.Engine
{
	// Token: 0x020006A4 RID: 1700
	internal abstract class CompositeEngine<TResult> where TResult : class
	{
		// Token: 0x06003D4A RID: 15690 RVA: 0x000C563C File Offset: 0x000C383C
		public CompositeEngine()
		{
			this.taskQueue = new Queue<IEngineTask>();
		}

		// Token: 0x140000A7 RID: 167
		// (add) Token: 0x06003D4B RID: 15691 RVA: 0x000C565C File Offset: 0x000C385C
		// (remove) Token: 0x06003D4C RID: 15692 RVA: 0x000C5694 File Offset: 0x000C3894
		public event EventHandler<CompositeEngineCompletedEventArgs> Completed;

		// Token: 0x1700141E RID: 5150
		// (get) Token: 0x06003D4D RID: 15693 RVA: 0x000C56C9 File Offset: 0x000C38C9
		// (set) Token: 0x06003D4E RID: 15694 RVA: 0x000C56D1 File Offset: 0x000C38D1
		public TResult Result { get; private set; }

		// Token: 0x1700141F RID: 5151
		// (get) Token: 0x06003D4F RID: 15695 RVA: 0x000C56DA File Offset: 0x000C38DA
		private bool WorkDone
		{
			get
			{
				return this.taskQueue.Count == 0;
			}
		}

		// Token: 0x06003D50 RID: 15696 RVA: 0x000C56EA File Offset: 0x000C38EA
		public void Run(object initialState)
		{
			if (initialState == null)
			{
				throw new ArgumentNullException("initialState");
			}
			this.TryRun(initialState);
		}

		// Token: 0x06003D51 RID: 15697
		protected abstract TResult PrepareResult(object finalState);

		// Token: 0x06003D52 RID: 15698
		protected abstract Queue<IEngineTask> PrepareTasks(object initialState);

		// Token: 0x06003D53 RID: 15699 RVA: 0x000C5704 File Offset: 0x000C3904
		private void TryRun(object initialState)
		{
			if (this.state == initialState)
			{
				return;
			}
			if (this.state != null)
			{
				this.StopCurrentProcessing();
			}
			lock (this.locker)
			{
				this.state = initialState;
				this.taskQueue = this.PrepareTasks(initialState);
			}
			IEngineTask task = this.taskQueue.Dequeue();
			this.StartTaskRun(task, initialState);
		}

		// Token: 0x06003D54 RID: 15700 RVA: 0x000C5780 File Offset: 0x000C3980
		private void StopCurrentProcessing()
		{
			if (this.currentTask != null)
			{
				this.currentTask.Completed += this.TaskCompleted;
				this.currentTask.Cancel();
			}
			this.ClearStateVariables();
		}

		// Token: 0x06003D55 RID: 15701 RVA: 0x000C57B2 File Offset: 0x000C39B2
		private void ClearStateVariables()
		{
			this.currentTask = null;
			this.state = null;
			this.taskQueue = null;
		}

		// Token: 0x06003D56 RID: 15702 RVA: 0x000C57CC File Offset: 0x000C39CC
		private void RunNextTask(object input)
		{
			if (this.WorkDone)
			{
				return;
			}
			IEngineTask task = this.taskQueue.Dequeue();
			this.StartTaskRun(task, input);
		}

		// Token: 0x06003D57 RID: 15703 RVA: 0x000C57F8 File Offset: 0x000C39F8
		private void TaskCompleted(object sender, EngineTaskCompletedEventArgs e)
		{
			lock (this.locker)
			{
				IEngineTask engineTask = sender as IEngineTask;
				if (this.currentTask == engineTask)
				{
					this.EndTaskRun(engineTask, e);
				}
			}
		}

		// Token: 0x06003D58 RID: 15704 RVA: 0x000C584C File Offset: 0x000C3A4C
		private void StartTaskRun(IEngineTask task, object input)
		{
			this.currentTask = task;
			task.Completed += this.TaskCompleted;
			task.Run(input);
		}

		// Token: 0x06003D59 RID: 15705 RVA: 0x000C586E File Offset: 0x000C3A6E
		private void EndTaskRun(IEngineTask task, EngineTaskCompletedEventArgs e)
		{
			task.Completed -= this.TaskCompleted;
			if (e.Error != null)
			{
				this.FinishEngineRunWithError(e);
			}
			if (this.WorkDone)
			{
				this.FinishEngineRunWithResults(task, e);
				return;
			}
			this.RunNextTask(task.Result);
		}

		// Token: 0x06003D5A RID: 15706 RVA: 0x000C58B0 File Offset: 0x000C3AB0
		private void FinishEngineRunWithError(EngineTaskCompletedEventArgs e)
		{
			CompositeEngineCompletedEventArgs e2 = new CompositeEngineCompletedEventArgs(e.Error);
			this.OnCompleted(e2);
		}

		// Token: 0x06003D5B RID: 15707 RVA: 0x000C58D0 File Offset: 0x000C3AD0
		private void FinishEngineRunWithResults(IEngineTask task, EngineTaskCompletedEventArgs e)
		{
			TResult result = this.PrepareResult(task.Result);
			this.Result = result;
			CompositeEngineCompletedEventArgs e2 = new CompositeEngineCompletedEventArgs(e.Error);
			this.OnCompleted(e2);
		}

		// Token: 0x06003D5C RID: 15708 RVA: 0x000C5904 File Offset: 0x000C3B04
		private void OnCompleted(CompositeEngineCompletedEventArgs e)
		{
			if (this.Completed != null)
			{
				this.Completed(this, e);
			}
		}

		// Token: 0x04001071 RID: 4209
		private Queue<IEngineTask> taskQueue;

		// Token: 0x04001072 RID: 4210
		private IEngineTask currentTask;

		// Token: 0x04001073 RID: 4211
		private object locker = new object();

		// Token: 0x04001074 RID: 4212
		private object state;
	}
}
