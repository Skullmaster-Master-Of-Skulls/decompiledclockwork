using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Telerik.Web.UI.PivotGrid.Core.Aggregates;
using Telerik.Web.UI.PivotGrid.Core.Totals;

namespace Telerik.Web.UI.PivotGrid.Core.Engine
{
	// Token: 0x020006AB RID: 1707
	[SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable", Justification = "Won't fix. Microsoft suggests that we should not care to dispose tasks.")]
	internal class FormatTotalsTask : EngineTaskBase
	{
		// Token: 0x06003D8C RID: 15756 RVA: 0x000C5BE0 File Offset: 0x000C3DE0
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

		// Token: 0x06003D8D RID: 15757 RVA: 0x000C5C40 File Offset: 0x000C3E40
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

		// Token: 0x06003D8E RID: 15758 RVA: 0x000C5C84 File Offset: 0x000C3E84
		private PivotResultsProcessingState Sort(object input)
		{
			PivotResultsProcessingState pivotResultsProcessingState = input as PivotResultsProcessingState;
			Dictionary<Coordinate, AggregateValue[]> formatTotals = FormatTotalsTask.GenerateFormattedTotals(pivotResultsProcessingState);
			pivotResultsProcessingState.FormatTotals = formatTotals;
			return pivotResultsProcessingState;
		}

		// Token: 0x06003D8F RID: 15759 RVA: 0x000C5CA8 File Offset: 0x000C3EA8
		private static Dictionary<Coordinate, AggregateValue[]> GenerateFormattedTotals(PivotResultsProcessingState state)
		{
			Dictionary<Coordinate, AggregateValue[]> dictionary = new Dictionary<Coordinate, AggregateValue[]>();
			IReadOnlyList<IAggregateDescription> aggregateDescriptions = state.AggregateDescriptions;
			for (int i = 0; i < aggregateDescriptions.Count; i++)
			{
				IAggregateDescription aggregateDescription = aggregateDescriptions[i];
				FormatTotalsTask.AggregateProcessingData aggregateProcessingData = new FormatTotalsTask.AggregateProcessingData();
				aggregateProcessingData.AggregateDescription = aggregateDescription;
				aggregateProcessingData.AggregateIndex = i;
				aggregateProcessingData.AggregatesCount = aggregateDescriptions.Count;
				TotalFormat totalFormat = aggregateDescription.TotalFormat;
				SingleTotalFormat singleTotalFormat = totalFormat as SingleTotalFormat;
				if (singleTotalFormat != null)
				{
					FormatTotalsTask.GenerateSimpleFormat(state, dictionary, aggregateProcessingData, singleTotalFormat);
				}
				SiblingTotalsFormat siblingTotalsFormat = totalFormat as SiblingTotalsFormat;
				if (siblingTotalsFormat != null)
				{
					FormatTotalsTask.GenerateRunningTotals(state, dictionary, aggregateProcessingData, siblingTotalsFormat);
				}
			}
			return dictionary;
		}

		// Token: 0x06003D90 RID: 15760 RVA: 0x000C5D38 File Offset: 0x000C3F38
		[SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "Will fix.")]
		private static void GenerateSimpleFormat(PivotResultsProcessingState state, Dictionary<Coordinate, AggregateValue[]> formatTotals, FormatTotalsTask.AggregateProcessingData aggregateData, SingleTotalFormat formattedTotals)
		{
			List<Group> list = FormatTotalsTask.GetChildrenGroups(state.AggregatesProvider.Root.RowGroup as Group).ToList<Group>();
			List<Group> list2 = FormatTotalsTask.GetChildrenGroups(state.AggregatesProvider.Root.ColumnGroup as Group).ToList<Group>();
			foreach (Group rowGroup in list)
			{
				foreach (Group columnGroup in list2)
				{
					state.CancellationTokenSource.Token.ThrowIfCancellationRequested();
					Coordinate coordinate = new Coordinate(rowGroup, columnGroup);
					AggregateValue aggregateValue;
					try
					{
						aggregateValue = formattedTotals.FormatValue(coordinate, state.AggregatesProvider, aggregateData.AggregateIndex);
					}
					catch
					{
						aggregateValue = new ConstantValueAggregate(AggregateValue.AggregateError);
					}
					FormatTotalsTask.SetRunningTotalValue(formatTotals, aggregateData, coordinate, aggregateValue);
				}
			}
		}

		// Token: 0x06003D91 RID: 15761 RVA: 0x000C60C0 File Offset: 0x000C42C0
		private static IEnumerable<Group> GetChildrenGroups(Group group)
		{
			yield return group;
			if (group.HasGroups)
			{
				foreach (Group childGroup in group.InternalGroups)
				{
					foreach (Group grandChildGroup in FormatTotalsTask.GetChildrenGroups(childGroup))
					{
						yield return grandChildGroup;
					}
				}
			}
			yield break;
		}

		// Token: 0x06003D92 RID: 15762 RVA: 0x000C6364 File Offset: 0x000C4564
		private static IEnumerable<Group> ChildGroupsAtLevel(Group root, int depth)
		{
			if (depth > 0)
			{
				if (root.HasGroups)
				{
					foreach (Group group in root.InternalGroups)
					{
						foreach (Group childGroup in FormatTotalsTask.ChildGroupsAtLevel(group, depth - 1))
						{
							yield return childGroup;
						}
					}
				}
			}
			else
			{
				yield return root;
			}
			yield break;
		}

		// Token: 0x06003D93 RID: 15763 RVA: 0x000C6394 File Offset: 0x000C4594
		private static IEnumerable<List<Group>> GetUniqueSubNameTrees(IEnumerable<Group> groups, IEqualityComparer<object[]> comparer)
		{
			List<object> parentNames = new List<object>();
			Dictionary<object[], List<Group>> dictionary = new Dictionary<object[], List<Group>>(comparer);
			foreach (Group group in groups)
			{
				FormatTotalsTask.AddGroupToNamesSubTree(parentNames, dictionary, group);
				if (group.HasGroups)
				{
					FormatTotalsTask.GetUniqueSubNameTrees(group.InternalGroups, parentNames, dictionary);
				}
			}
			return from s in dictionary
			select s.Value;
		}

		// Token: 0x06003D94 RID: 15764 RVA: 0x000C6424 File Offset: 0x000C4624
		private static void AddGroupToNamesSubTree(List<object> parentNames, Dictionary<object[], List<Group>> subTreeNames, Group group)
		{
			List<Group> list = null;
			object[] key = parentNames.ToArray();
			subTreeNames.TryGetValue(key, out list);
			if (list == null)
			{
				subTreeNames.Add(key, new List<Group>
				{
					group
				});
				return;
			}
			list.Add(group);
		}

		// Token: 0x06003D95 RID: 15765 RVA: 0x000C6464 File Offset: 0x000C4664
		private static void GetUniqueSubNameTrees(IEnumerable<Group> groups, List<object> parentNames, Dictionary<object[], List<Group>> subTreeNames)
		{
			foreach (Group group in groups)
			{
				parentNames.Add(group.Name);
				FormatTotalsTask.AddGroupToNamesSubTree(parentNames, subTreeNames, group);
				if (group.HasGroups)
				{
					FormatTotalsTask.GetUniqueSubNameTrees(group.InternalGroups, parentNames, subTreeNames);
				}
				parentNames.RemoveAt(parentNames.Count - 1);
			}
		}

		// Token: 0x06003D96 RID: 15766 RVA: 0x000C64DC File Offset: 0x000C46DC
		[SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "Design choice.")]
		private static void GenerateRunningTotals(PivotResultsProcessingState state, Dictionary<Coordinate, AggregateValue[]> formatTotals, FormatTotalsTask.AggregateProcessingData aggregateData, SiblingTotalsFormat showValueAs)
		{
			Group group = state.AggregatesProvider.Root.RowGroup as Group;
			Group group2 = state.AggregatesProvider.Root.ColumnGroup as Group;
			IEqualityComparer<object[]> comparer = null;
			switch (showValueAs.SubVariation())
			{
			case RunningTotalSubGroupVariation.ParentAndSelfNames:
				comparer = new FormatTotalsTask.ObjectArrayComparer();
				goto IL_5D;
			}
			comparer = new FormatTotalsTask.CountAndLastArrayComparer();
			IL_5D:
			PivotAxis axis = showValueAs.Axis;
			IEnumerable<Group> enumerable = FormatTotalsTask.GetChildrenGroups((axis == PivotAxis.Rows) ? group2 : group).ToList<Group>();
			IEnumerable<Group> enumerable2 = FormatTotalsTask.ChildGroupsAtLevel((axis == PivotAxis.Rows) ? group : group2, showValueAs.Level);
			foreach (Group group3 in enumerable2)
			{
				state.CancellationTokenSource.Token.ThrowIfCancellationRequested();
				if (group3.HasGroups)
				{
					IEnumerable<List<Group>> enumerable3 = FormatTotalsTask.GetUniqueSubNameTrees(group3.InternalGroups, comparer).ToList<List<Group>>();
					foreach (Group group4 in enumerable)
					{
						foreach (List<Group> list in enumerable3)
						{
							List<TotalValue> list2 = new List<TotalValue>();
							foreach (Group group5 in list)
							{
								Coordinate groups = (axis == PivotAxis.Rows) ? new Coordinate(group5, group4) : new Coordinate(group4, group5);
								list2.Add(new TotalValue(state.AggregatesProvider, groups, aggregateData.AggregateIndex));
							}
							try
							{
								showValueAs.FormatTotals(new ReadOnlyList<TotalValue, TotalValue>(list2), state.AggregatesProvider);
								foreach (TotalValue totalValue in list2)
								{
									FormatTotalsTask.SetRunningTotalValue(formatTotals, aggregateData, totalValue.Groups, totalValue.FormattedValue);
								}
							}
							catch
							{
								ConstantValueAggregate aggregateValue = new ConstantValueAggregate(AggregateValue.AggregateError);
								foreach (TotalValue totalValue2 in list2)
								{
									FormatTotalsTask.SetRunningTotalValue(formatTotals, aggregateData, totalValue2.Groups, aggregateValue);
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06003D97 RID: 15767 RVA: 0x000C67F4 File Offset: 0x000C49F4
		private static string GetEffectiveFormat(IAggregateDescription description)
		{
			IDataFieldDescription dataFieldDescription = description as IDataFieldDescription;
			if (dataFieldDescription == null)
			{
				return null;
			}
			Type dataType = dataFieldDescription.GetDataType();
			string text = null;
			if (description.TotalFormat != null)
			{
				text = description.TotalFormat.GetStringFormat(dataType, text);
			}
			return text;
		}

		// Token: 0x06003D98 RID: 15768 RVA: 0x000C6830 File Offset: 0x000C4A30
		private static void SetRunningTotalValue(Dictionary<Coordinate, AggregateValue[]> runningTotals, FormatTotalsTask.AggregateProcessingData aggregateData, Coordinate coordinate, AggregateValue aggregateValue)
		{
			if (aggregateValue == null)
			{
				return;
			}
			AggregateValue[] array = null;
			runningTotals.TryGetValue(coordinate, out array);
			if (array == null)
			{
				array = new AggregateValue[aggregateData.AggregatesCount];
			}
			string effectiveFormat = FormatTotalsTask.GetEffectiveFormat(aggregateData.AggregateDescription);
			IFormattable formattable = aggregateValue.GetValue() as IFormattable;
			if (formattable != null && effectiveFormat != null)
			{
				aggregateValue.SetFormattedValue(formattable.ToString(effectiveFormat, CultureInfo.CurrentCulture));
			}
			runningTotals[coordinate] = array;
			array[aggregateData.AggregateIndex] = aggregateValue;
		}

		// Token: 0x06003D99 RID: 15769 RVA: 0x000C68A0 File Offset: 0x000C4AA0
		public override void Cancel()
		{
			if (this.task == null)
			{
				return;
			}
			PivotResultsProcessingState pivotResultsProcessingState = this.task.AsyncState as PivotResultsProcessingState;
			pivotResultsProcessingState.CancellationTokenSource.Cancel();
		}

		// Token: 0x04001084 RID: 4228
		private Task<PivotResultsProcessingState> task;

		// Token: 0x020006AC RID: 1708
		private class ObjectArrayComparer : IEqualityComparer<object[]>
		{
			// Token: 0x06003D9C RID: 15772 RVA: 0x000C68DC File Offset: 0x000C4ADC
			public bool Equals(object[] x, object[] y)
			{
				if (x.Length != y.Length)
				{
					return false;
				}
				for (int i = 0; i < x.Length; i++)
				{
					if (!object.Equals(x[0], y[0]))
					{
						return false;
					}
				}
				return true;
			}

			// Token: 0x06003D9D RID: 15773 RVA: 0x000C6914 File Offset: 0x000C4B14
			public int GetHashCode(object[] obj)
			{
				int num = obj.Length * 8831;
				for (int i = 0; i < obj.Length; i++)
				{
					num = num * 8831 + obj[i].GetHashCode();
				}
				return num;
			}
		}

		// Token: 0x020006AD RID: 1709
		private class CountAndLastArrayComparer : IEqualityComparer<object[]>
		{
			// Token: 0x06003D9F RID: 15775 RVA: 0x000C6954 File Offset: 0x000C4B54
			public bool Equals(object[] x, object[] y)
			{
				if (x.Length != y.Length)
				{
					return false;
				}
				if (x.Length > 0)
				{
					int num = x.Length - 1;
					if (!object.Equals(x[num], y[num]))
					{
						return false;
					}
				}
				return true;
			}

			// Token: 0x06003DA0 RID: 15776 RVA: 0x000C6988 File Offset: 0x000C4B88
			public int GetHashCode(object[] obj)
			{
				int num = obj.Length;
				if (obj.Length > 0)
				{
					num = num * 8831 + obj[obj.Length - 1].GetHashCode();
				}
				return num;
			}
		}

		// Token: 0x020006AE RID: 1710
		private class AggregateProcessingData
		{
			// Token: 0x1700142F RID: 5167
			// (get) Token: 0x06003DA2 RID: 15778 RVA: 0x000C69BD File Offset: 0x000C4BBD
			// (set) Token: 0x06003DA3 RID: 15779 RVA: 0x000C69C5 File Offset: 0x000C4BC5
			public int AggregatesCount { get; set; }

			// Token: 0x17001430 RID: 5168
			// (get) Token: 0x06003DA4 RID: 15780 RVA: 0x000C69CE File Offset: 0x000C4BCE
			// (set) Token: 0x06003DA5 RID: 15781 RVA: 0x000C69D6 File Offset: 0x000C4BD6
			public int AggregateIndex { get; set; }

			// Token: 0x17001431 RID: 5169
			// (get) Token: 0x06003DA6 RID: 15782 RVA: 0x000C69DF File Offset: 0x000C4BDF
			// (set) Token: 0x06003DA7 RID: 15783 RVA: 0x000C69E7 File Offset: 0x000C4BE7
			public IAggregateDescription AggregateDescription { get; set; }
		}
	}
}
