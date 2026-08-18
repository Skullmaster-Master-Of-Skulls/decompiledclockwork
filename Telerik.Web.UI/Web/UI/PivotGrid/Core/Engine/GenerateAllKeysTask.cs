using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

namespace Telerik.Web.UI.PivotGrid.Core.Engine
{
	// Token: 0x020006AF RID: 1711
	[SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable", Justification = "Won't fix. Microsoft suggests that we should not care to dispose tasks.")]
	internal class GenerateAllKeysTask : EngineTaskBase
	{
		// Token: 0x06003DA9 RID: 15785 RVA: 0x000C69F8 File Offset: 0x000C4BF8
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

		// Token: 0x06003DAA RID: 15786 RVA: 0x000C6A58 File Offset: 0x000C4C58
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

		// Token: 0x06003DAB RID: 15787 RVA: 0x000C6A9C File Offset: 0x000C4C9C
		private PivotResultsProcessingState Sort(object input)
		{
			PivotResultsProcessingState pivotResultsProcessingState = input as PivotResultsProcessingState;
			GenerateAllKeysTask.GenerateUniqueKeys(pivotResultsProcessingState);
			return pivotResultsProcessingState;
		}

		// Token: 0x06003DAC RID: 15788 RVA: 0x000C6AB8 File Offset: 0x000C4CB8
		private static void GenerateUniqueKeys(PivotResultsProcessingState state)
		{
			List<List<HashSet<object>>> list = new List<List<HashSet<object>>>();
			GenerateAllKeysTask.GenerateUniqueKeysForAxis(state, list, PivotAxis.Rows);
			GenerateAllKeysTask.GenerateUniqueKeysForAxis(state, list, PivotAxis.Columns);
			state.UniqueGroupKeys = list;
		}

		// Token: 0x06003DAD RID: 15789 RVA: 0x000C6AE4 File Offset: 0x000C4CE4
		private static void GenerateUniqueKeysForAxis(PivotResultsProcessingState state, List<List<HashSet<object>>> uniqueKeys, PivotAxis axis)
		{
			IAggregateResultProvider aggregatesProvider = state.AggregatesProvider;
			state.CancellationToken.ThrowIfCancellationRequested();
			uniqueKeys.Add(new List<HashSet<object>>());
			IReadOnlyList<GroupDescription> descriptions = (axis == PivotAxis.Rows) ? state.RowGroupDescriptions : state.ColumnGroupDescriptions;
			IList<GroupDescription> allDescriptions = GroupDescription.GetAllDescriptions<GroupDescription>(descriptions);
			for (int i = 0; i < allDescriptions.Count; i++)
			{
				uniqueKeys[(int)axis].Add(new HashSet<object>());
			}
			IGroup group = (axis == PivotAxis.Rows) ? aggregatesProvider.Root.RowGroup : aggregatesProvider.Root.ColumnGroup;
			GenerateAllKeysTask.GetUniqueKeys(state.CancellationToken, group, 0, uniqueKeys[(int)axis]);
		}

		// Token: 0x06003DAE RID: 15790 RVA: 0x000C6B88 File Offset: 0x000C4D88
		private static void GetUniqueKeys(CancellationToken token, IGroup group, int level, List<HashSet<object>> keys)
		{
			if (group.HasGroups && level < keys.Count)
			{
				int level2 = level + 1;
				HashSet<object> hashSet = keys[level];
				foreach (IGroup group2 in group.Groups)
				{
					token.ThrowIfCancellationRequested();
					hashSet.Add(group2.Name);
					GenerateAllKeysTask.GetUniqueKeys(token, group2, level2, keys);
				}
			}
		}

		// Token: 0x06003DAF RID: 15791 RVA: 0x000C6C08 File Offset: 0x000C4E08
		public override void Cancel()
		{
			if (this.task == null)
			{
				return;
			}
			PivotResultsProcessingState pivotResultsProcessingState = this.task.AsyncState as PivotResultsProcessingState;
			pivotResultsProcessingState.CancellationTokenSource.Cancel();
		}

		// Token: 0x04001089 RID: 4233
		private Task<PivotResultsProcessingState> task;
	}
}
