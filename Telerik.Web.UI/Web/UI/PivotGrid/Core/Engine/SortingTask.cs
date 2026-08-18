using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Telerik.Web.UI.PivotGrid.Core.Engine
{
	// Token: 0x020006B0 RID: 1712
	[SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable", Justification = "Won't fix. Microsoft suggests that we should not care to dispose tasks.")]
	internal class SortingTask : EngineTaskBase
	{
		// Token: 0x06003DB1 RID: 15793 RVA: 0x000C6C44 File Offset: 0x000C4E44
		protected override void RunCore(PivotResultsProcessingState input)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			if (this.task != null)
			{
				return;
			}
			this.task = new Task<PivotResultsProcessingState>(new Func<object, PivotResultsProcessingState>(this.Sort), input);
			this.task.ContinueWith(new Action<Task<PivotResultsProcessingState>>(this.FinishSorting));
			this.task.Start();
		}

		// Token: 0x06003DB2 RID: 15794 RVA: 0x000C6CA4 File Offset: 0x000C4EA4
		[SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "Will fix.")]
		private void FinishSorting(Task<PivotResultsProcessingState> finishedTask)
		{
			try
			{
				base.Result = finishedTask.Result;
				base.OnCompleted(new EngineTaskCompletedEventArgs(null));
			}
			catch (Exception error)
			{
				base.CompleteWithError(error);
			}
		}

		// Token: 0x06003DB3 RID: 15795 RVA: 0x000C6CE8 File Offset: 0x000C4EE8
		private PivotResultsProcessingState Sort(object input)
		{
			PivotResultsProcessingState pivotResultsProcessingState = input as PivotResultsProcessingState;
			SortingTask.SortGroups(pivotResultsProcessingState);
			return pivotResultsProcessingState;
		}

		// Token: 0x06003DB4 RID: 15796 RVA: 0x000C6D04 File Offset: 0x000C4F04
		private static void SortGroups(PivotResultsProcessingState state)
		{
			SortingTask.SortGroups(state, state.AggregatesProvider.Root.RowGroup as Group, PivotAxis.Rows, 0);
			SortingTask.SortGroups(state, state.AggregatesProvider.Root.ColumnGroup as Group, PivotAxis.Columns, 0);
		}

		// Token: 0x06003DB5 RID: 15797 RVA: 0x000C6D54 File Offset: 0x000C4F54
		private static void SortGroups(PivotResultsProcessingState state, Group group, PivotAxis axis, int level)
		{
			IAggregateResultProvider aggregatesProvider = state.AggregatesProvider;
			state.CancellationToken.ThrowIfCancellationRequested();
			IReadOnlyList<GroupDescription> descriptions = (axis == PivotAxis.Rows) ? state.RowGroupDescriptions : state.ColumnGroupDescriptions;
			IList<GroupDescription> allDescriptions = GroupDescription.GetAllDescriptions<GroupDescription>(descriptions);
			if (level >= allDescriptions.Count)
			{
				return;
			}
			GroupDescription groupDescription = allDescriptions[level];
			GroupComparer groupComparer = groupDescription.GroupComparer;
			if (groupComparer != null && groupDescription.SortOrder != SortOrder.None)
			{
				SortOrder sortOrder = groupDescription.SortOrder;
				GroupComparerDecorator comparer = new GroupComparerDecorator(groupComparer, sortOrder, aggregatesProvider, axis);
				group.SortSubGroups(comparer);
			}
			level++;
			if (level < allDescriptions.Count && group.HasGroups)
			{
				for (int i = 0; i < group.InternalGroups.Count; i++)
				{
					state.CancellationToken.ThrowIfCancellationRequested();
					SortingTask.SortGroups(state, group.InternalGroups[i], axis, level);
				}
			}
		}

		// Token: 0x06003DB6 RID: 15798 RVA: 0x000C6E28 File Offset: 0x000C5028
		public override void Cancel()
		{
			if (this.task == null)
			{
				return;
			}
			PivotResultsProcessingState pivotResultsProcessingState = this.task.AsyncState as PivotResultsProcessingState;
			pivotResultsProcessingState.CancellationTokenSource.Cancel();
		}

		// Token: 0x0400108A RID: 4234
		private Task<PivotResultsProcessingState> task;
	}
}
