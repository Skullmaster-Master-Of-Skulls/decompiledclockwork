using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Telerik.Web.UI.PivotGrid.Core.Filtering;
using Telerik.Web.UI.PivotGrid.Core.Olap;
using Telerik.Web.UI.PivotGrid.Core.Totals;

namespace Telerik.Web.UI.PivotGrid.Core.Engine
{
	// Token: 0x02000D3D RID: 3389
	[SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling", Justification = "Will resolve in future.")]
	internal class PivotEngine : IPivotEngine, IPivotResults, IAggregateResultProvider
	{
		// Token: 0x06007DFB RID: 32251 RVA: 0x001CC420 File Offset: 0x001CA620
		public PivotEngine()
		{
			this.aggregates = new Dictionary<Coordinate, AggregateValue[]>();
			this.summaries = new Dictionary<Coordinate, AggregateValue[]>();
			this.formattedTotals = new Dictionary<Coordinate, AggregateValue[]>();
			this.RowGroupDescriptions = new ReadOnlyList<GroupDescription, GroupDescription>(new List<GroupDescription>());
			this.ColumnGroupDescriptions = new ReadOnlyList<GroupDescription, GroupDescription>(new List<GroupDescription>());
			this.AggregateDescriptions = new ReadOnlyList<IAggregateDescription, IAggregateDescription>(new List<IAggregateDescription>());
			this.FilterDescriptions = new ReadOnlyList<FilterDescription, FilterDescription>(new List<FilterDescription>());
		}

		// Token: 0x14000133 RID: 307
		// (add) Token: 0x06007DFC RID: 32252 RVA: 0x001CC494 File Offset: 0x001CA694
		// (remove) Token: 0x06007DFD RID: 32253 RVA: 0x001CC4CC File Offset: 0x001CA6CC
		public event EventHandler<PivotEngineCompletedEventArgs> Completed;

		// Token: 0x17002838 RID: 10296
		// (get) Token: 0x06007DFE RID: 32254 RVA: 0x001CC501 File Offset: 0x001CA701
		// (set) Token: 0x06007DFF RID: 32255 RVA: 0x001CC509 File Offset: 0x001CA709
		public Coordinate Root { get; private set; }

		// Token: 0x17002839 RID: 10297
		// (get) Token: 0x06007E00 RID: 32256 RVA: 0x001CC512 File Offset: 0x001CA712
		// (set) Token: 0x06007E01 RID: 32257 RVA: 0x001CC51A File Offset: 0x001CA71A
		public IReadOnlyList<GroupDescription> RowGroupDescriptions { get; private set; }

		// Token: 0x1700283A RID: 10298
		// (get) Token: 0x06007E02 RID: 32258 RVA: 0x001CC523 File Offset: 0x001CA723
		// (set) Token: 0x06007E03 RID: 32259 RVA: 0x001CC52B File Offset: 0x001CA72B
		public IReadOnlyList<GroupDescription> ColumnGroupDescriptions { get; private set; }

		// Token: 0x1700283B RID: 10299
		// (get) Token: 0x06007E04 RID: 32260 RVA: 0x001CC534 File Offset: 0x001CA734
		// (set) Token: 0x06007E05 RID: 32261 RVA: 0x001CC53C File Offset: 0x001CA73C
		public IReadOnlyList<IAggregateDescription> AggregateDescriptions { get; private set; }

		// Token: 0x1700283C RID: 10300
		// (get) Token: 0x06007E06 RID: 32262 RVA: 0x001CC545 File Offset: 0x001CA745
		// (set) Token: 0x06007E07 RID: 32263 RVA: 0x001CC54D File Offset: 0x001CA74D
		public IReadOnlyList<FilterDescription> FilterDescriptions { get; private set; }

		// Token: 0x06007E08 RID: 32264 RVA: 0x001CC558 File Offset: 0x001CA758
		public IEnumerable<object> GetUniqueKeys(PivotAxis axis, int groupDescriptionIndex)
		{
			if (this.uniqueGroupKeys != null)
			{
				if (axis == PivotAxis.Rows && this.uniqueGroupKeys.Count >= 1)
				{
					List<HashSet<object>> list = this.uniqueGroupKeys[0];
					if (groupDescriptionIndex >= 0 && groupDescriptionIndex < list.Count)
					{
						return list[groupDescriptionIndex];
					}
				}
				else if (axis == PivotAxis.Columns && this.uniqueGroupKeys.Count >= 2)
				{
					List<HashSet<object>> list2 = this.uniqueGroupKeys[1];
					if (groupDescriptionIndex >= 0 && groupDescriptionIndex < list2.Count)
					{
						return list2[groupDescriptionIndex];
					}
				}
			}
			return null;
		}

		// Token: 0x06007E09 RID: 32265 RVA: 0x001CC5D5 File Offset: 0x001CA7D5
		public IEnumerable<object> GetUniqueFilterItems(int filterIndex)
		{
			if (this.uniqueFilterItems != null && filterIndex >= 0 && filterIndex < this.uniqueFilterItems.Length)
			{
				return this.uniqueFilterItems[filterIndex];
			}
			return null;
		}

		// Token: 0x06007E0A RID: 32266 RVA: 0x001CC5F8 File Offset: 0x001CA7F8
		public AggregateValue GetAggregateResult(int aggregateIndex, IGroup row, IGroup column)
		{
			return this.GetAggregateResult(aggregateIndex, new Coordinate(row, column));
		}

		// Token: 0x06007E0B RID: 32267 RVA: 0x001CC608 File Offset: 0x001CA808
		public AggregateValue GetAggregateResult(int aggregateIndex, Coordinate coordinate)
		{
			if (aggregateIndex >= 0 && aggregateIndex < this.AggregateDescriptions.Count)
			{
				IAggregateDescription aggregateDescription = this.AggregateDescriptions[aggregateIndex];
				AggregateValue[] array;
				if (aggregateDescription.TotalFormat != null)
				{
					if (this.formattedTotals.TryGetValue(coordinate, out array))
					{
						AggregateValue aggregateValue = array[aggregateIndex];
						if (aggregateValue != null)
						{
							return aggregateValue;
						}
					}
				}
				else if (this.aggregates.TryGetValue(coordinate, out array))
				{
					AggregateValue aggregateValue2 = array[aggregateIndex];
					if (aggregateValue2 != null)
					{
						return aggregateValue2;
					}
				}
				else if (this.summaries.TryGetValue(coordinate, out array))
				{
					AggregateValue aggregateValue3 = array[aggregateIndex];
					if (aggregateValue3 != null)
					{
						return aggregateValue3;
					}
				}
			}
			return null;
		}

		// Token: 0x06007E0C RID: 32268 RVA: 0x001CC68C File Offset: 0x001CA88C
		[SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "Design choice.")]
		public void RebuildCube(ParallelState state)
		{
			List<Exception> list = new List<Exception>();
			PivotEngineStatus status = PivotEngineStatus.Faulted;
			if (state.IsEmpty)
			{
				this.Clear();
				return;
			}
			try
			{
				this.CancelCurrentProcessing();
				this.RaiseInProgress();
				state.CancellationTokenSource = new CancellationTokenSource();
				PivotEngine.GroupingResults groupingResults = PivotEngine.ProcessItems(new PivotEngine.BottomLevelGroupingTaskState
				{
					ParallelState = state,
					Start = 0,
					End = state.ItemsSource.Count
				});
				PivotEngine.GroupingFinalizationTaskState finalizationTasksState = new PivotEngine.GroupingFinalizationTaskState
				{
					ParallelState = state,
					Results = groupingResults
				};
				this.FinalizeAggregations(groupingResults, finalizationTasksState);
				status = PivotEngineStatus.Completed;
			}
			catch (Exception item)
			{
				list.Add(item);
			}
			ReadOnlyCollection<Exception> innerExceptions = new ReadOnlyCollection<Exception>(list);
			this.RaiseCompleted(new PivotEngineCompletedEventArgs(innerExceptions, status));
		}

		// Token: 0x06007E0D RID: 32269 RVA: 0x001CC750 File Offset: 0x001CA950
		private void RaiseInProgress()
		{
			this.RaiseCompleted(new PivotEngineCompletedEventArgs(new ReadOnlyCollection<Exception>(new List<Exception>()), PivotEngineStatus.InProgress));
		}

		// Token: 0x06007E0E RID: 32270 RVA: 0x001CC768 File Offset: 0x001CA968
		private void RaiseCompleted(PivotEngineCompletedEventArgs args)
		{
			this.currentResultTask = null;
			if (this.Completed != null)
			{
				this.Completed(this, args);
			}
		}

		// Token: 0x06007E0F RID: 32271 RVA: 0x001CC788 File Offset: 0x001CA988
		private void FinalizeAggregations(PivotEngine.GroupingResults finalResult, PivotEngine.GroupingFinalizationTaskState finalizationTasksState)
		{
			List<List<HashSet<object>>> list = PivotEngine.GenerateUniqueKeys(finalizationTasksState);
			PivotEngine.GenerateEmptyGroups(finalizationTasksState, list);
			PivotEngine.GenerateAllKeys(finalizationTasksState, list);
			List<List<HashSet<object>>> allKeys = list;
			PivotEngine.ProcessCalculatedItems(finalResult, finalizationTasksState, allKeys);
			Dictionary<Coordinate, AggregateValue[]> dictionary = PivotEngine.GenerateSummaries(finalizationTasksState);
			CalculatedFieldsAggregateValues calculatedFieldsAggregateValues = new CalculatedFieldsAggregateValues
			{
				Infos = finalizationTasksState.ParallelState.AggregateDescriptionInfos,
				Aggregates = finalResult.Aggregates,
				Summaries = dictionary
			};
			PivotEngine.ProcessCalculatedFields(finalizationTasksState, calculatedFieldsAggregateValues);
			PivotEngine.AggregateResultProvider aggregateResultProvider = new PivotEngine.AggregateResultProvider
			{
				Infos = finalizationTasksState.ParallelState.AggregateDescriptionInfos,
				Aggregates = finalResult.Aggregates,
				Summaries = dictionary,
				Root = finalizationTasksState.Results.Root
			};
			PivotEngine.FilterGroups(finalizationTasksState, aggregateResultProvider, dictionary);
			PivotEngine.SortGroups(finalizationTasksState, aggregateResultProvider);
			Dictionary<Coordinate, AggregateValue[]> totalFormats = PivotEngine.GenerateFormattedTotals(finalizationTasksState, aggregateResultProvider);
			PivotEngine.ApplyStringFormats(finalizationTasksState, aggregateResultProvider, totalFormats);
			this.aggregates = finalResult.Aggregates;
			this.summaries = dictionary;
			this.formattedTotals = totalFormats;
			this.uniqueGroupKeys = allKeys;
			this.uniqueFilterItems = finalResult.UniqueFilterItems;
			this.Root = finalResult.Root;
			int aggregateDescriptionCount = finalizationTasksState.ParallelState.AggregateDescriptionCount;
			List<IAggregateDescription> list2 = new List<IAggregateDescription>(aggregateDescriptionCount);
			for (int i = 0; i < aggregateDescriptionCount; i++)
			{
				list2.Add(finalizationTasksState.ParallelState.AggregateDescriptions[i]);
			}
			this.AggregateDescriptions = new ReadOnlyList<IAggregateDescription, IAggregateDescription>(list2);
			this.RowGroupDescriptions = finalizationTasksState.ParallelState.RowGroupDescriptions;
			this.ColumnGroupDescriptions = finalizationTasksState.ParallelState.ColumnGroupDescriptions;
			this.FilterDescriptions = finalizationTasksState.ParallelState.FilterDescriptions;
		}

		// Token: 0x06007E10 RID: 32272 RVA: 0x001CC91F File Offset: 0x001CAB1F
		private static void ProcessCalculatedFields(PivotEngine.GroupingFinalizationTaskState finalizationTasksState, CalculatedFieldsAggregateValues calculatedFieldsAggregateValues)
		{
			if (finalizationTasksState.ParallelState.AggregateDescriptionInfos.Any((AggregateDescriptionInfo item) => item.IsCalculated))
			{
				PivotEngine.ProcessCalculatedFieldsAggregateValues(finalizationTasksState, calculatedFieldsAggregateValues);
			}
		}

		// Token: 0x06007E11 RID: 32273 RVA: 0x001CC958 File Offset: 0x001CAB58
		private static void ProcessCalculatedItems(PivotEngine.GroupingResults finalResult, PivotEngine.GroupingFinalizationTaskState finalizationTasksState, List<List<HashSet<object>>> allKeys)
		{
			ParallelState parallelState = finalizationTasksState.ParallelState;
			Coordinate root = finalizationTasksState.Results.Root;
			Group group = (Group)root.RowGroup;
			Group group2 = (Group)root.ColumnGroup;
			IValueProvider valueProvider = finalizationTasksState.ParallelState.ValueProvider;
			List<Group> list = new List<Group>();
			List<Group> list2 = new List<Group>();
			PivotEngine.GenerateCalculatedItemGroups(group, valueProvider, list, PivotAxis.Rows, allKeys, 0, parallelState.RowGroupDescriptions.Count);
			PivotEngine.GenerateCalculatedItemGroups(group2, valueProvider, list2, PivotAxis.Columns, allKeys, 0, parallelState.ColumnGroupDescriptions.Count);
			Dictionary<Coordinate, PivotEngine.CalculatedItemDetails> dictionary = new Dictionary<Coordinate, PivotEngine.CalculatedItemDetails>();
			List<Group> list3 = new List<Group>();
			PivotEngine.GetBottomLevelGroups(group2, list3);
			foreach (Group rowGroup in list)
			{
				foreach (Group columnGroup in list3)
				{
					Coordinate coordinate = new Coordinate(rowGroup, columnGroup);
					PivotAxis axis;
					int level;
					CalculatedItem calculatedItem = PivotEngine.GetCalculatedItem(coordinate, out axis, out level);
					if (!dictionary.ContainsKey(coordinate))
					{
						dictionary.Add(coordinate, new PivotEngine.CalculatedItemDetails(calculatedItem, axis, level));
					}
				}
			}
			List<Group> list4 = new List<Group>();
			PivotEngine.GetBottomLevelGroups(group, list4);
			foreach (Group columnGroup2 in list2)
			{
				foreach (Group rowGroup2 in list4)
				{
					Coordinate coordinate2 = new Coordinate(rowGroup2, columnGroup2);
					PivotAxis axis2;
					int level2;
					CalculatedItem calculatedItem2 = PivotEngine.GetCalculatedItem(coordinate2, out axis2, out level2);
					if (!dictionary.ContainsKey(coordinate2))
					{
						dictionary.Add(coordinate2, new PivotEngine.CalculatedItemDetails(calculatedItem2, axis2, level2));
					}
				}
			}
			PivotEngine.AggregateResultProvider resultsProvider = new PivotEngine.AggregateResultProvider
			{
				Infos = parallelState.AggregateDescriptionInfos,
				Aggregates = finalResult.Aggregates,
				Summaries = new Dictionary<Coordinate, AggregateValue[]>(),
				Root = finalizationTasksState.Results.Root
			};
			PivotEngine.ProcessCalculatedItemsAggregateValues(parallelState, dictionary, resultsProvider);
		}

		// Token: 0x06007E12 RID: 32274 RVA: 0x001CCBA8 File Offset: 0x001CADA8
		private static void GenerateCalculatedItemGroups(Group group, IValueProvider valueProvider, List<Group> newGroups, PivotAxis axis, List<List<HashSet<object>>> allKeys, int level, int levelsCount)
		{
			if (level < levelsCount)
			{
				IEnumerable<CalculatedItem> enumerable = (axis == PivotAxis.Rows) ? valueProvider.GetRowCalculatedItems(level) : valueProvider.GetColumnCalculatedItems(level);
				foreach (CalculatedItem groupName in enumerable)
				{
					Group group2 = group.CreateGroupByName(groupName);
					PivotEngine.AddUniqueKeysToCalculatedGroup(group2, valueProvider, newGroups, axis, allKeys, level + 1, levelsCount);
					if (level == levelsCount - 1)
					{
						newGroups.Add(group2);
					}
				}
				if (group.HasGroups)
				{
					foreach (Group group3 in group.InternalGroups)
					{
						PivotEngine.GenerateCalculatedItemGroups(group3, valueProvider, newGroups, axis, allKeys, level + 1, levelsCount);
					}
				}
			}
		}

		// Token: 0x06007E13 RID: 32275 RVA: 0x001CCC88 File Offset: 0x001CAE88
		private static void AddUniqueKeysToCalculatedGroup(Group calculatedGroup, IValueProvider valueProvider, List<Group> newGroups, PivotAxis axis, List<List<HashSet<object>>> allKeys, int level, int levelsCount)
		{
			if (level < levelsCount)
			{
				IEnumerable<object> enumerable = (axis == PivotAxis.Rows) ? allKeys[0][level] : allKeys[1][level];
				foreach (object groupName in enumerable)
				{
					Group item = calculatedGroup.CreateGroupByName(groupName);
					if (level == levelsCount - 1)
					{
						newGroups.Add(item);
					}
				}
				foreach (Group calculatedGroup2 in calculatedGroup.InternalGroups)
				{
					PivotEngine.AddUniqueKeysToCalculatedGroup(calculatedGroup2, valueProvider, newGroups, axis, allKeys, level + 1, levelsCount);
				}
			}
		}

		// Token: 0x06007E14 RID: 32276 RVA: 0x001CCD60 File Offset: 0x001CAF60
		private static void GetBottomLevelGroups(Group group, List<Group> bottomLevelColumnGroups)
		{
			if (group.HasGroups)
			{
				using (IEnumerator<Group> enumerator = group.InternalGroups.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						Group group2 = enumerator.Current;
						PivotEngine.GetBottomLevelGroups(group2, bottomLevelColumnGroups);
					}
					return;
				}
			}
			bottomLevelColumnGroups.Add(group);
		}

		// Token: 0x06007E15 RID: 32277 RVA: 0x001CCDBC File Offset: 0x001CAFBC
		private static void ApplyStringFormats(PivotEngine.GroupingFinalizationTaskState finalizationTasksState, PivotEngine.AggregateResultProvider resultsProvider, IDictionary<Coordinate, AggregateValue[]> totalFormats)
		{
			IReadOnlyList<IAggregateDescription> aggregateDescriptions = finalizationTasksState.ParallelState.AggregateDescriptions;
			List<string> list = new List<string>(aggregateDescriptions.Count);
			for (int i = 0; i < aggregateDescriptions.Count; i++)
			{
				list.Add(finalizationTasksState.ParallelState.ValueProvider.GetAggregateStringFormat(i));
			}
			PivotEngine.ApplyStringFormatsToAggregateValues(resultsProvider.Aggregates, list, finalizationTasksState.ParallelState.Culture);
			PivotEngine.ApplyStringFormatsToAggregateValues(resultsProvider.Summaries, list, finalizationTasksState.ParallelState.Culture);
			PivotEngine.ApplyStringFormatsToAggregateValues(totalFormats, list, finalizationTasksState.ParallelState.Culture);
		}

		// Token: 0x06007E16 RID: 32278 RVA: 0x001CCE4C File Offset: 0x001CB04C
		[SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "Design choice.")]
		private static void ApplyStringFormatsToAggregateValues(IDictionary<Coordinate, AggregateValue[]> aggValues, List<string> stringFormats, CultureInfo culture)
		{
			if (culture == CultureInfo.InvariantCulture)
			{
				culture = CultureInfo.CurrentCulture;
			}
			foreach (KeyValuePair<Coordinate, AggregateValue[]> keyValuePair in aggValues)
			{
				AggregateValue[] value = keyValuePair.Value;
				for (int i = 0; i < value.Length; i++)
				{
					AggregateValue aggregateValue = value[i];
					string text = stringFormats[i];
					if (aggregateValue != null && text != null)
					{
						try
						{
							IFormattable formattable = aggregateValue.GetValue() as IFormattable;
							if (formattable != null)
							{
								aggregateValue.SetFormattedValue(formattable.ToString(text, culture));
							}
						}
						catch
						{
							aggregateValue.RaiseError();
						}
					}
				}
			}
		}

		// Token: 0x06007E17 RID: 32279 RVA: 0x001CCF04 File Offset: 0x001CB104
		private static Dictionary<Coordinate, AggregateValue[]> GenerateFormattedTotals(PivotEngine.GroupingFinalizationTaskState finalizationTasksState, PivotEngine.AggregateResultProvider summaryResults)
		{
			Dictionary<Coordinate, AggregateValue[]> dictionary = new Dictionary<Coordinate, AggregateValue[]>();
			IReadOnlyList<IAggregateDescription> aggregateDescriptions = finalizationTasksState.ParallelState.AggregateDescriptions;
			for (int i = 0; i < aggregateDescriptions.Count; i++)
			{
				IAggregateDescription aggregateDescription = aggregateDescriptions[i];
				TotalFormat totalFormat = aggregateDescription.TotalFormat;
				SingleTotalFormat singleTotalFormat = totalFormat as SingleTotalFormat;
				if (singleTotalFormat != null)
				{
					PivotEngine.GenerateSimpleFormat(finalizationTasksState, summaryResults, dictionary, i, aggregateDescriptions.Count, singleTotalFormat);
				}
				else
				{
					SiblingTotalsFormat siblingTotalsFormat = totalFormat as SiblingTotalsFormat;
					if (siblingTotalsFormat != null)
					{
						PivotEngine.GenerateRunningTotals(finalizationTasksState, summaryResults, dictionary, aggregateDescriptions, i, siblingTotalsFormat);
					}
				}
			}
			return dictionary;
		}

		// Token: 0x06007E18 RID: 32280 RVA: 0x001CCF80 File Offset: 0x001CB180
		[SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "Design choice.")]
		private static void GenerateSimpleFormat(PivotEngine.GroupingFinalizationTaskState finalizationTasksState, PivotEngine.AggregateResultProvider summaryResults, Dictionary<Coordinate, AggregateValue[]> formatTotals, int aggregateIndex, int aggregatesCount, SingleTotalFormat formattedTotals)
		{
			PivotEngine.GroupingResults results = finalizationTasksState.Results;
			IList<Group> list = PivotEngine.GetChildrenGroups(results.RowRootGroup).ToList<Group>();
			IList<Group> list2 = PivotEngine.GetChildrenGroups(results.ColumnRootGroup).ToList<Group>();
			foreach (Group rowGroup in list)
			{
				foreach (Group columnGroup in list2)
				{
					Coordinate coordinate = new Coordinate(rowGroup, columnGroup);
					AggregateValue aggregateValue;
					try
					{
						aggregateValue = formattedTotals.FormatValue(coordinate, summaryResults, aggregateIndex);
					}
					catch
					{
						aggregateValue = AggregateValue.ErrorAggregateValue;
					}
					PivotEngine.SetRunningTotalValue(formatTotals, aggregatesCount, aggregateIndex, coordinate, aggregateValue);
				}
			}
		}

		// Token: 0x06007E19 RID: 32281 RVA: 0x001CD064 File Offset: 0x001CB264
		[SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "Design choice.")]
		private static void GenerateRunningTotals(PivotEngine.GroupingFinalizationTaskState finalizationTasksState, PivotEngine.AggregateResultProvider summaryResults, Dictionary<Coordinate, AggregateValue[]> formatTotals, IReadOnlyList<IAggregateDescription> aggregates, int aggregateIndex, SiblingTotalsFormat showValueAs)
		{
			PivotEngine.GroupingResults results = finalizationTasksState.Results;
			IEqualityComparer<object[]> comparer = null;
			switch (showValueAs.SubVariation())
			{
			case RunningTotalSubGroupVariation.ParentAndSelfNames:
				comparer = new PivotEngine.ObjectArrayComparer();
				goto IL_31;
			}
			comparer = new PivotEngine.CountAndLastArrayComparer();
			IL_31:
			PivotAxis axis = showValueAs.Axis;
			IEnumerable<Group> enumerable = PivotEngine.GetChildrenGroups((axis == PivotAxis.Rows) ? results.ColumnRootGroup : results.RowRootGroup).ToList<Group>();
			IEnumerable<Group> enumerable2 = PivotEngine.ChildGroupsAtLevel((axis == PivotAxis.Rows) ? results.RowRootGroup : results.ColumnRootGroup, showValueAs.Level);
			foreach (Group group in enumerable2)
			{
				if (group.HasGroups)
				{
					IEnumerable<List<Group>> enumerable3 = PivotEngine.GetUniqueSubNameTrees(group.InternalGroups, comparer).ToList<List<Group>>();
					foreach (Group group2 in enumerable)
					{
						foreach (List<Group> list in enumerable3)
						{
							List<TotalValue> list2 = new List<TotalValue>();
							foreach (Group group3 in list)
							{
								Coordinate groups = (axis == PivotAxis.Rows) ? new Coordinate(group3, group2) : new Coordinate(group2, group3);
								list2.Add(new TotalValue(summaryResults, groups, aggregateIndex));
							}
							try
							{
								showValueAs.FormatTotals(new ReadOnlyList<TotalValue, TotalValue>(list2), summaryResults);
								foreach (TotalValue totalValue in list2)
								{
									PivotEngine.SetRunningTotalValue(formatTotals, aggregates.Count, aggregateIndex, totalValue.Groups, totalValue.FormattedValue);
								}
							}
							catch
							{
								foreach (TotalValue totalValue2 in list2)
								{
									PivotEngine.SetRunningTotalValue(formatTotals, aggregates.Count, aggregateIndex, totalValue2.Groups, AggregateValue.ErrorAggregateValue);
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06007E1A RID: 32282 RVA: 0x001CD348 File Offset: 0x001CB548
		private static void SetRunningTotalValue(Dictionary<Coordinate, AggregateValue[]> runningTotals, int aggregatesCount, int aggregateIndex, Coordinate coordinate, AggregateValue aggregateValue)
		{
			if (aggregateValue != null)
			{
				AggregateValue[] array = null;
				runningTotals.TryGetValue(coordinate, out array);
				if (array == null)
				{
					array = new AggregateValue[aggregatesCount];
					runningTotals[coordinate] = array;
				}
				array[aggregateIndex] = aggregateValue;
			}
		}

		// Token: 0x06007E1B RID: 32283 RVA: 0x001CD388 File Offset: 0x001CB588
		private static IEnumerable<List<Group>> GetUniqueSubNameTrees(IEnumerable<Group> groups, IEqualityComparer<object[]> comparer)
		{
			List<object> parentNames = new List<object>();
			Dictionary<object[], List<Group>> dictionary = new Dictionary<object[], List<Group>>(comparer);
			foreach (Group group in groups)
			{
				PivotEngine.AddGroupToNamesSubTree(parentNames, dictionary, group);
				if (group.HasGroups)
				{
					PivotEngine.GetUniqueSubNameTrees(group.InternalGroups, parentNames, dictionary);
				}
			}
			return from s in dictionary
			select s.Value;
		}

		// Token: 0x06007E1C RID: 32284 RVA: 0x001CD418 File Offset: 0x001CB618
		private static void GetUniqueSubNameTrees(IEnumerable<Group> groups, List<object> parentNames, Dictionary<object[], List<Group>> subTreeNames)
		{
			foreach (Group group in groups)
			{
				parentNames.Add(group.Name);
				PivotEngine.AddGroupToNamesSubTree(parentNames, subTreeNames, group);
				if (group.HasGroups)
				{
					PivotEngine.GetUniqueSubNameTrees(group.InternalGroups, parentNames, subTreeNames);
				}
				parentNames.RemoveAt(parentNames.Count - 1);
			}
		}

		// Token: 0x06007E1D RID: 32285 RVA: 0x001CD490 File Offset: 0x001CB690
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

		// Token: 0x06007E1E RID: 32286 RVA: 0x001CD754 File Offset: 0x001CB954
		private static IEnumerable<Group> ChildGroupsAtLevel(Group root, int depth)
		{
			if (depth > 0)
			{
				if (root.HasGroups)
				{
					foreach (Group group in root.InternalGroups)
					{
						foreach (Group childGroup in PivotEngine.ChildGroupsAtLevel(group, depth - 1))
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

		// Token: 0x06007E1F RID: 32287 RVA: 0x001CD778 File Offset: 0x001CB978
		public void WaitForParallel()
		{
			if (this.currentResultTask != null)
			{
				this.currentResultTask.Wait();
			}
		}

		// Token: 0x06007E20 RID: 32288 RVA: 0x001CD78D File Offset: 0x001CB98D
		public void Clear()
		{
			this.CancelCurrentProcessing();
			this.RaiseCompleted(new PivotEngineCompletedEventArgs(new ReadOnlyCollection<Exception>(new List<Exception>()), PivotEngineStatus.Completed));
		}

		// Token: 0x06007E21 RID: 32289 RVA: 0x001CD7AC File Offset: 0x001CB9AC
		private void CancelCurrentProcessing()
		{
			if (this.initalState != null && this.initalState.CancellationTokenSource != null)
			{
				this.initalState.CancellationTokenSource.Cancel();
			}
			this.initalState = null;
			this.formattedTotals.Clear();
			this.aggregates.Clear();
			this.summaries.Clear();
			this.Root = default(Coordinate);
			this.uniqueGroupKeys = null;
			this.RowGroupDescriptions = new ReadOnlyList<GroupDescription, GroupDescription>(new List<GroupDescription>());
			this.ColumnGroupDescriptions = new ReadOnlyList<GroupDescription, GroupDescription>(new List<GroupDescription>());
			this.AggregateDescriptions = new ReadOnlyList<IAggregateDescription, IAggregateDescription>(new List<IAggregateDescription>());
			this.FilterDescriptions = new ReadOnlyList<FilterDescription, FilterDescription>(new List<FilterDescription>());
		}

		// Token: 0x06007E22 RID: 32290 RVA: 0x001CD85C File Offset: 0x001CBA5C
		private static void GenerateAllKeys(PivotEngine.GroupingFinalizationTaskState finalizationState, List<List<HashSet<object>>> uniqueKeys)
		{
			ParallelState parallelState = finalizationState.ParallelState;
			PivotEngine.GenerateAllKeysAxis(uniqueKeys, parallelState, PivotAxis.Rows, parallelState.RowGroupDescriptions);
			PivotEngine.GenerateAllKeysAxis(uniqueKeys, parallelState, PivotAxis.Columns, parallelState.ColumnGroupDescriptions);
		}

		// Token: 0x06007E23 RID: 32291 RVA: 0x001CD88C File Offset: 0x001CBA8C
		private static void GenerateAllKeysAxis(List<List<HashSet<object>>> uniqueKeys, ParallelState state, PivotAxis axis, IReadOnlyList<GroupDescription> rd)
		{
			for (int i = 0; i < rd.Count; i++)
			{
				state.CancellationToken.ThrowIfCancellationRequested();
				GroupDescription groupDescription = rd[i];
				HashSet<object> uniqueNames = uniqueKeys[(int)axis][i];
				HashSet<object> hashSet = new HashSet<object>();
				uniqueKeys[(int)axis][i] = hashSet;
				IEnumerable<object> allNames = groupDescription.GetAllNames(uniqueNames, Enumerable.Empty<object>());
				if (allNames != null)
				{
					foreach (object item in allNames)
					{
						hashSet.Add(item);
					}
				}
			}
		}

		// Token: 0x06007E24 RID: 32292 RVA: 0x001CD940 File Offset: 0x001CBB40
		private static List<List<HashSet<object>>> GenerateUniqueKeys(PivotEngine.GroupingFinalizationTaskState finalizationState)
		{
			List<List<HashSet<object>>> list = new List<List<HashSet<object>>>();
			PivotEngine.GenerateUniqueKeysForAxis(finalizationState, list, PivotAxis.Rows);
			PivotEngine.GenerateUniqueKeysForAxis(finalizationState, list, PivotAxis.Columns);
			return list;
		}

		// Token: 0x06007E25 RID: 32293 RVA: 0x001CD964 File Offset: 0x001CBB64
		private static void GenerateUniqueKeysForAxis(PivotEngine.GroupingFinalizationTaskState finalizationState, List<List<HashSet<object>>> uniqueKeys, PivotAxis axis)
		{
			ParallelState parallelState = finalizationState.ParallelState;
			PivotEngine.GroupingResults results = finalizationState.Results;
			parallelState.CancellationToken.ThrowIfCancellationRequested();
			uniqueKeys.Add(new List<HashSet<object>>());
			IReadOnlyList<GroupDescription> readOnlyList = (axis == PivotAxis.Rows) ? parallelState.RowGroupDescriptions : parallelState.ColumnGroupDescriptions;
			for (int i = 0; i < readOnlyList.Count; i++)
			{
				uniqueKeys[(int)axis].Add(new HashSet<object>());
			}
			IGroup group = (axis == PivotAxis.Rows) ? results.Root.RowGroup : results.Root.ColumnGroup;
			PivotEngine.GetUniqueKeys(parallelState.CancellationToken, group, 0, uniqueKeys[(int)axis]);
		}

		// Token: 0x06007E26 RID: 32294 RVA: 0x001CDA08 File Offset: 0x001CBC08
		[SuppressMessage("Microsoft.Usage", "CA2233:OperationsShouldNotOverflow", Justification = "Not a real issue.")]
		private static void GetUniqueKeys(CancellationToken token, IGroup group, int level, List<HashSet<object>> keys)
		{
			if (group.HasGroups)
			{
				int level2 = level + 1;
				HashSet<object> hashSet = keys[level];
				foreach (IGroup group2 in group.Groups)
				{
					token.ThrowIfCancellationRequested();
					hashSet.Add(group2.Name);
					PivotEngine.GetUniqueKeys(token, group2, level2, keys);
				}
			}
		}

		// Token: 0x06007E27 RID: 32295 RVA: 0x001CDA80 File Offset: 0x001CBC80
		private static void GenerateEmptyGroups(PivotEngine.GroupingFinalizationTaskState finalizationState, List<List<HashSet<object>>> uniqueKeys)
		{
			PivotEngine.GroupingResults results = finalizationState.Results;
			PivotEngine.GenerateEmptyGroups(finalizationState, uniqueKeys, results.RowRootGroup, PivotAxis.Rows, 0);
			PivotEngine.GenerateEmptyGroups(finalizationState, uniqueKeys, results.ColumnRootGroup, PivotAxis.Columns, 0);
		}

		// Token: 0x06007E28 RID: 32296 RVA: 0x001CDAB4 File Offset: 0x001CBCB4
		private static void GenerateEmptyGroups(PivotEngine.GroupingFinalizationTaskState finalizationState, List<List<HashSet<object>>> allKeys, Group group, PivotAxis axis, int level)
		{
			ParallelState parallelState = finalizationState.ParallelState;
			parallelState.CancellationToken.ThrowIfCancellationRequested();
			IReadOnlyList<GroupDescription> readOnlyList = (axis == PivotAxis.Rows) ? parallelState.RowGroupDescriptions : parallelState.ColumnGroupDescriptions;
			if (level < readOnlyList.Count)
			{
				GroupDescription groupDescription = readOnlyList[level];
				if (groupDescription.ShowGroupsWithNoData)
				{
					HashSet<object> uniqueNames = allKeys[(int)axis][level];
					string uniqueName = groupDescription.GetUniqueName();
					IEnumerable<object> allNames = groupDescription.GetAllNames(uniqueNames, PivotEngine.EnumerateParentNames(group, uniqueName, readOnlyList, level - 1));
					if (allNames != null)
					{
						foreach (object groupName in allNames)
						{
							parallelState.CancellationToken.ThrowIfCancellationRequested();
							group.CreateGroupByName(groupName);
						}
					}
				}
				if (group.HasGroups)
				{
					parallelState.CancellationToken.ThrowIfCancellationRequested();
					int level2 = level + 1;
					foreach (Group group2 in group.InternalGroups)
					{
						PivotEngine.GenerateEmptyGroups(finalizationState, allKeys, group2, axis, level2);
					}
				}
			}
		}

		// Token: 0x06007E29 RID: 32297 RVA: 0x001CDBF0 File Offset: 0x001CBDF0
		private static void SortGroups(PivotEngine.GroupingFinalizationTaskState finalizationState, IAggregateResultProvider resultsProvider)
		{
			PivotEngine.GroupingResults results = finalizationState.Results;
			PivotEngine.SortGroups(finalizationState, resultsProvider, results.RowRootGroup, PivotAxis.Rows, 0);
			PivotEngine.SortGroups(finalizationState, resultsProvider, results.ColumnRootGroup, PivotAxis.Columns, 0);
		}

		// Token: 0x06007E2A RID: 32298 RVA: 0x001CDC24 File Offset: 0x001CBE24
		private static void SortGroups(PivotEngine.GroupingFinalizationTaskState finalizationState, IAggregateResultProvider resultsProvider, Group group, PivotAxis axis, int level)
		{
			ParallelState parallelState = finalizationState.ParallelState;
			parallelState.CancellationToken.ThrowIfCancellationRequested();
			IReadOnlyList<GroupDescription> readOnlyList = (axis == PivotAxis.Rows) ? parallelState.RowGroupDescriptions : parallelState.ColumnGroupDescriptions;
			if (level < readOnlyList.Count)
			{
				GroupDescription groupDescription = readOnlyList[level];
				GroupComparer groupComparer = groupDescription.GroupComparer;
				if (groupComparer != null && groupDescription.SortOrder != SortOrder.None)
				{
					SortOrder sortOrder = groupDescription.SortOrder;
					GroupComparerDecorator comparer = new GroupComparerDecorator(groupComparer, sortOrder, resultsProvider, axis);
					group.SortSubGroups(comparer);
				}
				level++;
				if (level < readOnlyList.Count && group.HasGroups)
				{
					for (int i = 0; i < group.InternalGroups.Count; i++)
					{
						parallelState.CancellationToken.ThrowIfCancellationRequested();
						PivotEngine.SortGroups(finalizationState, resultsProvider, group.InternalGroups[i], axis, level);
					}
				}
			}
		}

		// Token: 0x06007E2B RID: 32299 RVA: 0x001CDCF8 File Offset: 0x001CBEF8
		private static void FilterGroups(PivotEngine.GroupingFinalizationTaskState finalizationState, IAggregateResultProvider resultsProvider, Dictionary<Coordinate, AggregateValue[]> summaries)
		{
			ParallelState parallelState = finalizationState.ParallelState;
			PivotEngine.GroupingResults results = finalizationState.Results;
			parallelState.CancellationToken.ThrowIfCancellationRequested();
			HashSet<IGroup> hashSet = new HashSet<IGroup>();
			PivotEngine.FilterGroups(finalizationState, resultsProvider, results.RowRootGroup, hashSet, PivotAxis.Rows, 0);
			PivotEngine.FilterGroups(finalizationState, resultsProvider, results.ColumnRootGroup, hashSet, PivotAxis.Columns, 0);
			if (hashSet.Count > 0)
			{
				IDictionary<Coordinate, AggregateValue[]> dictionary = new Dictionary<Coordinate, AggregateValue[]>();
				foreach (KeyValuePair<Coordinate, AggregateValue[]> item in results.Aggregates)
				{
					Coordinate key = item.Key;
					if (!hashSet.Contains(key.RowGroup) && !hashSet.Contains(key.ColumnGroup))
					{
						dictionary.Add(item);
					}
				}
				results.Aggregates = dictionary;
				summaries.Clear();
				PivotEngine.Summarize(finalizationState, summaries, PivotAxis.Rows, PivotEngine.Append(summaries, results.Aggregates));
				PivotEngine.Summarize(finalizationState, summaries, PivotAxis.Columns, PivotEngine.Append(summaries, results.Aggregates));
				if (parallelState.AggregateDescriptionCount != 0)
				{
					PivotEngine.RemoveEmptyGroups(finalizationState, summaries, results, results.RowRootGroup, PivotAxis.Rows, 0);
					PivotEngine.RemoveEmptyGroups(finalizationState, summaries, results, results.ColumnRootGroup, PivotAxis.Columns, 0);
				}
				CalculatedFieldsAggregateValues calculatedFieldsAggregateValues = new CalculatedFieldsAggregateValues
				{
					Infos = finalizationState.ParallelState.AggregateDescriptionInfos,
					Aggregates = dictionary,
					Summaries = summaries
				};
				PivotEngine.ProcessCalculatedFields(finalizationState, calculatedFieldsAggregateValues);
			}
		}

		// Token: 0x06007E2C RID: 32300 RVA: 0x001CDE5C File Offset: 0x001CC05C
		[SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "By design the engine should not brake if an exception is thrown in user code.")]
		[SuppressMessage("Microsoft.Usage", "CA2233:OperationsShouldNotOverflow", Justification = "Small chance to happen.")]
		private static void FilterGroups(PivotEngine.GroupingFinalizationTaskState finalizationState, IAggregateResultProvider resultsProvider, Group group, ICollection<IGroup> filteredGroups, PivotAxis axis, int level)
		{
			ParallelState parallelState = finalizationState.ParallelState;
			if (group.HasGroups)
			{
				IList<Group> internalGroups = group.InternalGroups;
				int num = internalGroups.Count;
				int level2 = level + 1;
				IReadOnlyList<GroupDescription> readOnlyList = (axis == PivotAxis.Rows) ? parallelState.RowGroupDescriptions : parallelState.ColumnGroupDescriptions;
				GroupDescription groupDescription = readOnlyList[level];
				GroupFilter groupFilter = groupDescription.GroupFilter;
				SingleGroupFilter singleGroupFilter = groupFilter as SingleGroupFilter;
				ICollection<IGroup> collection = null;
				if (groupFilter != null && singleGroupFilter == null)
				{
					SiblingGroupsFilter siblingGroupsFilter = groupFilter as SiblingGroupsFilter;
					collection = siblingGroupsFilter.Filter(group.Groups, resultsProvider, axis, level);
				}
				for (int i = 0; i < num; i++)
				{
					Group group2 = internalGroups[i];
					bool flag = false;
					parallelState.CancellationToken.ThrowIfCancellationRequested();
					try
					{
						if (singleGroupFilter != null)
						{
							flag = !singleGroupFilter.Filter(group2, resultsProvider, axis);
						}
						else if (collection != null)
						{
							flag = !collection.Contains(group2);
						}
					}
					catch (Exception)
					{
					}
					if (!flag && group2.HasGroups)
					{
						PivotEngine.FilterGroups(finalizationState, resultsProvider, group2, filteredGroups, axis, level2);
						if (!group2.HasGroups)
						{
							flag = true;
						}
					}
					if (flag)
					{
						PivotEngine.AddChildGroupsToSet(filteredGroups, group2);
						group.RemoveGroupAt(i);
						i--;
						num--;
					}
				}
			}
		}

		// Token: 0x06007E2D RID: 32301 RVA: 0x001CDF9C File Offset: 0x001CC19C
		[SuppressMessage("Microsoft.Usage", "CA2233:OperationsShouldNotOverflow")]
		private static void RemoveEmptyGroups(PivotEngine.GroupingFinalizationTaskState finalizationState, Dictionary<Coordinate, AggregateValue[]> summaries, PivotEngine.GroupingResults results, Group group, PivotAxis axis, int level)
		{
			ParallelState parallelState = finalizationState.ParallelState;
			if (group.HasGroups)
			{
				IReadOnlyList<GroupDescription> readOnlyList = (axis == PivotAxis.Rows) ? parallelState.RowGroupDescriptions : parallelState.ColumnGroupDescriptions;
				GroupDescription groupDescription = readOnlyList[level];
				IList<Group> internalGroups = group.InternalGroups;
				int num = internalGroups.Count;
				int level2 = level + 1;
				for (int i = 0; i < num; i++)
				{
					Group group2 = internalGroups[i];
					Coordinate key = (axis == PivotAxis.Rows) ? new Coordinate(group2, results.ColumnRootGroup) : new Coordinate(results.RowRootGroup, group2);
					PivotEngine.RemoveEmptyGroups(finalizationState, summaries, results, group2, axis, level2);
					if (!groupDescription.ShowGroupsWithNoData)
					{
						bool flag = !summaries.ContainsKey(key) && !results.Aggregates.ContainsKey(key);
						if (flag)
						{
							group.RemoveGroupAt(i);
							i--;
							num--;
						}
					}
				}
			}
		}

		// Token: 0x06007E2E RID: 32302 RVA: 0x001CE07C File Offset: 0x001CC27C
		private static void AddChildGroupsToSet(ICollection<IGroup> filteredGroups, Group group)
		{
			foreach (Group item in PivotEngine.GetChildrenGroups(group))
			{
				filteredGroups.Add(item);
			}
		}

		// Token: 0x06007E2F RID: 32303 RVA: 0x001CE32C File Offset: 0x001CC52C
		private static IEnumerable<Group> GetChildrenGroups(Group group)
		{
			yield return group;
			if (group.HasGroups)
			{
				foreach (Group childGroup in group.InternalGroups)
				{
					foreach (Group grandChildGroup in PivotEngine.GetChildrenGroups(childGroup))
					{
						yield return grandChildGroup;
					}
				}
			}
			yield break;
		}

		// Token: 0x06007E30 RID: 32304 RVA: 0x001CE34C File Offset: 0x001CC54C
		private static Dictionary<Coordinate, AggregateValue[]> GenerateSummaries(PivotEngine.GroupingFinalizationTaskState finalizationState)
		{
			PivotEngine.GroupingResults results = finalizationState.Results;
			Dictionary<Coordinate, AggregateValue[]> dictionary = new Dictionary<Coordinate, AggregateValue[]>();
			PivotEngine.Summarize(finalizationState, dictionary, PivotAxis.Rows, PivotEngine.Append(dictionary, results.Aggregates));
			PivotEngine.Summarize(finalizationState, dictionary, PivotAxis.Columns, PivotEngine.Append(dictionary, results.Aggregates));
			return dictionary;
		}

		// Token: 0x06007E31 RID: 32305 RVA: 0x001CE390 File Offset: 0x001CC590
		private static void Summarize(PivotEngine.GroupingFinalizationTaskState finalizationState, Dictionary<Coordinate, AggregateValue[]> summaries, PivotAxis axis, IDictionary<Coordinate, AggregateValue[]> children)
		{
			ParallelState parallelState = finalizationState.ParallelState;
			int maxCalculatedItem = PivotEngine.GetMaxCalculatedItem(parallelState.RowGroupDescriptions, parallelState.ValueProvider, PivotAxis.Rows);
			int maxCalculatedItem2 = PivotEngine.GetMaxCalculatedItem(parallelState.ColumnGroupDescriptions, parallelState.ValueProvider, PivotAxis.Columns);
			parallelState.CancellationToken.ThrowIfCancellationRequested();
			IDictionary<Coordinate, AggregateValue[]> dictionary = new Dictionary<Coordinate, AggregateValue[]>();
			foreach (KeyValuePair<Coordinate, AggregateValue[]> keyValuePair in children)
			{
				Coordinate key = keyValuePair.Key;
				if ((axis == PivotAxis.Rows) ? (key.RowGroup.Parent != null) : (key.ColumnGroup.Parent != null))
				{
					Coordinate coordinate;
					if (axis == PivotAxis.Rows)
					{
						coordinate = new Coordinate(key.RowGroup.Parent, key.ColumnGroup);
					}
					else
					{
						coordinate = new Coordinate(key.RowGroup, key.ColumnGroup.Parent);
					}
					for (int i = 0; i < parallelState.AggregateDescriptionInfos.Length; i++)
					{
						AggregateDescriptionInfo aggregateDescriptionInfo = parallelState.AggregateDescriptionInfos[i];
						int originalIndex = aggregateDescriptionInfo.OriginalIndex;
						if (!aggregateDescriptionInfo.IsCalculated)
						{
							bool hasCalculatedGroup = (maxCalculatedItem >= 0 && coordinate.RowGroup.Level <= maxCalculatedItem + 1) || (maxCalculatedItem2 >= 0 && coordinate.ColumnGroup.Level <= maxCalculatedItem2 + 1);
							IList<AggregateValue> orCreateAggregates = PivotEngine.GetOrCreateAggregates(dictionary, coordinate, parallelState, hasCalculatedGroup);
							AggregateValue aggregateValue = orCreateAggregates[originalIndex];
							AggregateValue aggregateValue2 = keyValuePair.Value[originalIndex];
							if (aggregateValue2 != null)
							{
								aggregateValue.MergeCore(aggregateValue2);
							}
						}
					}
					parallelState.CancellationToken.ThrowIfCancellationRequested();
				}
			}
			foreach (KeyValuePair<Coordinate, AggregateValue[]> keyValuePair2 in dictionary)
			{
				summaries.Add(keyValuePair2.Key, keyValuePair2.Value);
			}
			if (dictionary.Any<KeyValuePair<Coordinate, AggregateValue[]>>())
			{
				PivotEngine.Summarize(finalizationState, summaries, axis, dictionary);
			}
		}

		// Token: 0x06007E32 RID: 32306 RVA: 0x001CE5C0 File Offset: 0x001CC7C0
		private static int GetMaxCalculatedItem(IReadOnlyList<GroupDescription> groupDescriptions, IValueProvider valueProvider, PivotAxis axis)
		{
			int result = -1;
			for (int i = 0; i < groupDescriptions.Count; i++)
			{
				IEnumerable<CalculatedItem> enumerable = (axis == PivotAxis.Rows) ? valueProvider.GetRowCalculatedItems(i) : valueProvider.GetColumnCalculatedItems(i);
				if (enumerable != null && enumerable.Any<CalculatedItem>())
				{
					result = i + 1;
				}
			}
			return result;
		}

		// Token: 0x06007E33 RID: 32307 RVA: 0x001CE604 File Offset: 0x001CC804
		private static AggregateValue[] GetOrCreateAggregates(IDictionary<Coordinate, AggregateValue[]> dictionary, Coordinate coordinate, ParallelState state, bool hasCalculatedGroup)
		{
			IReadOnlyList<IAggregateDescription> aggregateDescriptions = state.AggregateDescriptions;
			AggregateValue[] array;
			if (!dictionary.TryGetValue(coordinate, out array))
			{
				int count = aggregateDescriptions.Count;
				array = new AggregateValue[count];
				for (int i = 0; i < state.AggregateDescriptionInfos.Length; i++)
				{
					AggregateDescriptionInfo aggregateDescriptionInfo = state.AggregateDescriptionInfos[i];
					int originalIndex = aggregateDescriptionInfo.OriginalIndex;
					if (!aggregateDescriptionInfo.IsCalculated)
					{
						array[originalIndex] = state.ValueProvider.CreateAggregateValue(originalIndex, hasCalculatedGroup);
					}
				}
				dictionary.Add(coordinate, array);
			}
			return array;
		}

		// Token: 0x06007E34 RID: 32308 RVA: 0x001CE688 File Offset: 0x001CC888
		private static CalculatedItem GetCalculatedItem(Coordinate coordinate, out PivotAxis axis, out int level)
		{
			Group group = (Group)coordinate.RowGroup;
			Group group2 = (Group)coordinate.ColumnGroup;
			int num;
			CalculatedItem calculatedItem = PivotEngine.GetCalculatedItem(group, out num);
			int num2;
			CalculatedItem calculatedItem2 = PivotEngine.GetCalculatedItem(group2, out num2);
			CalculatedItem calculatedItemWithLargestSolveOrder = PivotEngine.GetCalculatedItemWithLargestSolveOrder(calculatedItem, calculatedItem2, out axis);
			level = ((axis == PivotAxis.Rows) ? num : num2);
			return calculatedItemWithLargestSolveOrder;
		}

		// Token: 0x06007E35 RID: 32309 RVA: 0x001CE6DB File Offset: 0x001CC8DB
		private static CalculatedItem GetCalculatedItemWithLargestSolveOrder(CalculatedItem rowCalculatedItem, CalculatedItem columnCalculatedItem, out PivotAxis axis)
		{
			if (rowCalculatedItem != null && columnCalculatedItem != null)
			{
				if (rowCalculatedItem.SolveOrder > columnCalculatedItem.SolveOrder)
				{
					axis = PivotAxis.Rows;
					return rowCalculatedItem;
				}
				axis = PivotAxis.Columns;
				return columnCalculatedItem;
			}
			else
			{
				if (rowCalculatedItem != null)
				{
					axis = PivotAxis.Rows;
					return rowCalculatedItem;
				}
				if (columnCalculatedItem != null)
				{
					axis = PivotAxis.Columns;
					return columnCalculatedItem;
				}
				axis = PivotAxis.Rows;
				return null;
			}
		}

		// Token: 0x06007E36 RID: 32310 RVA: 0x001CE710 File Offset: 0x001CC910
		internal static CalculatedItem GetCalculatedItem(Group group, out int level)
		{
			level = 0;
			CalculatedItem calculatedItem = null;
			while (group != null)
			{
				PivotAxis pivotAxis;
				calculatedItem = PivotEngine.GetCalculatedItemWithLargestSolveOrder(calculatedItem, group.CalculatedItem, out pivotAxis);
				if (calculatedItem == group.CalculatedItem)
				{
					level = group.Level;
				}
				group = group.InternalParent;
			}
			return calculatedItem;
		}

		// Token: 0x06007E37 RID: 32311 RVA: 0x001CE8B4 File Offset: 0x001CCAB4
		private static IEnumerable<object> EnumerateParentNames(IGroup group, string uniqueName, IReadOnlyList<GroupDescription> groupDescriptions, int level)
		{
			while (group != null && level >= 0)
			{
				GroupDescription groupDescription = groupDescriptions[level];
				string currentUniqueName = groupDescription.GetUniqueName();
				if (currentUniqueName == uniqueName)
				{
					yield return group.Name;
				}
				group = group.Parent;
				level--;
			}
			yield break;
		}

		// Token: 0x06007E38 RID: 32312 RVA: 0x001CE8E8 File Offset: 0x001CCAE8
		private static IDictionary<Coordinate, AggregateValue[]> Append(IDictionary<Coordinate, AggregateValue[]> dictionary, IDictionary<Coordinate, AggregateValue[]> elements)
		{
			Dictionary<Coordinate, AggregateValue[]> dictionary2 = new Dictionary<Coordinate, AggregateValue[]>(dictionary);
			foreach (KeyValuePair<Coordinate, AggregateValue[]> keyValuePair in elements)
			{
				dictionary2.Add(keyValuePair.Key, keyValuePair.Value);
			}
			return dictionary2;
		}

		// Token: 0x06007E39 RID: 32313 RVA: 0x001CE948 File Offset: 0x001CCB48
		internal static Group CreateGrandTotal()
		{
			return new Group("Grand Total");
		}

		// Token: 0x06007E3A RID: 32314 RVA: 0x001CE954 File Offset: 0x001CCB54
		internal static Group OlapGrandTotal()
		{
			return new Group(new OlapGroupName("Grand Total"));
		}

		// Token: 0x06007E3B RID: 32315 RVA: 0x001CE968 File Offset: 0x001CCB68
		[SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "Design choice.")]
		private static PivotEngine.GroupingResults ProcessItems(object state)
		{
			PivotEngine.BottomLevelGroupingTaskState bottomLevelGroupingTaskState = (PivotEngine.BottomLevelGroupingTaskState)state;
			ParallelState parallelState = bottomLevelGroupingTaskState.ParallelState;
			int start = bottomLevelGroupingTaskState.Start;
			int end = bottomLevelGroupingTaskState.End;
			Group group = PivotEngine.CreateGrandTotal();
			Group group2 = PivotEngine.CreateGrandTotal();
			Dictionary<Coordinate, AggregateValue[]> dictionary = new Dictionary<Coordinate, AggregateValue[]>();
			int filtersCount = parallelState.ValueProvider.GetFiltersCount();
			HashSet<object>[] array = new HashSet<object>[filtersCount];
			for (int i = 0; i < filtersCount; i++)
			{
				array[i] = new HashSet<object>();
			}
			IValueProvider valueProvider = parallelState.ValueProvider;
			CancellationToken cancellationToken = parallelState.CancellationToken;
			for (int j = start; j < end; j++)
			{
				cancellationToken.ThrowIfCancellationRequested();
				object item = parallelState.GetItem(j);
				object[] filterItems = valueProvider.GetFilterItems(item);
				for (int k = 0; k < array.Length; k++)
				{
					array[k].Add(filterItems[k]);
				}
				bool flag = valueProvider.PassesFilter(filterItems);
				if (flag)
				{
					Group group3 = group;
					IEnumerable rowGroupNames = parallelState.ValueProvider.GetRowGroupNames(item);
					foreach (object groupName in rowGroupNames)
					{
						group3 = group3.CreateGroupByName(groupName);
					}
					Group group4 = group2;
					IEnumerable columnGroupNames = parallelState.ValueProvider.GetColumnGroupNames(item);
					foreach (object groupName2 in columnGroupNames)
					{
						group4 = group4.CreateGroupByName(groupName2);
					}
					Coordinate coordinate = new Coordinate(group3, group4);
					PivotAxis pivotAxis;
					int num;
					if (PivotEngine.GetCalculatedItem(coordinate, out pivotAxis, out num) == null)
					{
						AggregateValue[] orCreateAggregates = PivotEngine.GetOrCreateAggregates(dictionary, coordinate, parallelState, false);
						for (int l = 0; l < parallelState.AggregateDescriptionInfos.Length; l++)
						{
							AggregateDescriptionInfo aggregateDescriptionInfo = parallelState.AggregateDescriptionInfos[l];
							int originalIndex = aggregateDescriptionInfo.OriginalIndex;
							bool isError = aggregateDescriptionInfo.IsError;
							if (!aggregateDescriptionInfo.IsCalculated)
							{
								object item2 = null;
								object obj = null;
								if (isError)
								{
									orCreateAggregates[originalIndex] = AggregateValue.ErrorAggregateValue;
								}
								else
								{
									try
									{
										item2 = valueProvider.GetAggregateValue(originalIndex, item);
									}
									catch (Exception ex)
									{
										obj = ex;
									}
									if (obj == null)
									{
										orCreateAggregates[originalIndex].AccumulateCore(item2);
									}
									else
									{
										orCreateAggregates[originalIndex].RaiseError();
									}
								}
							}
						}
					}
				}
			}
			return new PivotEngine.GroupingResults(group, group2, array)
			{
				Aggregates = dictionary
			};
		}

		// Token: 0x06007E3C RID: 32316 RVA: 0x001CED60 File Offset: 0x001CCF60
		private static IEnumerable<IGroup> FlattenGroups(IEnumerable<IGroup> groups)
		{
			Stack<IEnumerator<IGroup>> stack = new Stack<IEnumerator<IGroup>>();
			stack.Push(groups.GetEnumerator());
			while (stack.Count > 0)
			{
				IEnumerator<IGroup> top = stack.Peek();
				if (top.MoveNext())
				{
					IGroup group = top.Current;
					yield return group;
					if (group.HasGroups)
					{
						stack.Push(group.Groups.GetEnumerator());
					}
				}
				else
				{
					stack.Pop();
				}
			}
			yield break;
		}

		// Token: 0x06007E3D RID: 32317 RVA: 0x001CED80 File Offset: 0x001CCF80
		[SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "Design choice.")]
		private static void ProcessCalculatedFieldsAggregateValues(PivotEngine.GroupingFinalizationTaskState finalizationTasksState, CalculatedFieldsAggregateValues calculatedFieldsAggregateValues)
		{
			ParallelState parallelState = finalizationTasksState.ParallelState;
			IReadOnlyList<IAggregateDescription> aggregateDescriptions = parallelState.AggregateDescriptions;
			IEnumerable<IGroup> enumerable = PivotEngine.FlattenGroups(new IGroup[]
			{
				finalizationTasksState.Results.Root.RowGroup
			});
			IEnumerable<IGroup> enumerable2 = PivotEngine.FlattenGroups(new IGroup[]
			{
				finalizationTasksState.Results.Root.ColumnGroup
			});
			foreach (IGroup group in enumerable)
			{
				foreach (IGroup group2 in enumerable2)
				{
					Coordinate coordinate = new Coordinate(group, group2);
					for (int i = 0; i < parallelState.AggregateDescriptionInfos.Length; i++)
					{
						AggregateDescriptionInfo aggregateDescriptionInfo = parallelState.AggregateDescriptionInfos[i];
						int originalIndex = aggregateDescriptionInfo.OriginalIndex;
						bool isError = aggregateDescriptionInfo.IsError;
						bool isCalculated = aggregateDescriptionInfo.IsCalculated;
						calculatedFieldsAggregateValues.Coordinate = coordinate;
						AggregateValue[] aggregateResults = calculatedFieldsAggregateValues.GetAggregateResults();
						if (aggregateResults == null)
						{
							bool flag = !group.HasGroups && !group2.HasGroups;
							AggregateValue[] value = new AggregateValue[parallelState.AggregateDescriptionInfos.Length];
							if (flag)
							{
								calculatedFieldsAggregateValues.Aggregates[coordinate] = value;
							}
							else
							{
								calculatedFieldsAggregateValues.Summaries[coordinate] = value;
							}
						}
						if (isError)
						{
							aggregateResults[originalIndex] = AggregateValue.ErrorAggregateValue;
						}
						else if (isCalculated)
						{
							object obj = null;
							try
							{
								ICalculatedAggregateDescription calculatedAggregateDescription = (ICalculatedAggregateDescription)aggregateDescriptions[originalIndex];
								aggregateResults[originalIndex] = calculatedAggregateDescription.CalculatedField.CalculateValue(calculatedFieldsAggregateValues);
							}
							catch (Exception ex)
							{
								obj = ex;
							}
							if (obj != null)
							{
								aggregateResults[originalIndex].RaiseError();
							}
						}
					}
				}
			}
		}

		// Token: 0x06007E3E RID: 32318 RVA: 0x001CEFE0 File Offset: 0x001CD1E0
		[SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "Design choice.")]
		private static void ProcessCalculatedItemsAggregateValues(ParallelState state, Dictionary<Coordinate, PivotEngine.CalculatedItemDetails> calculatedItemCoordiantes, PivotEngine.AggregateResultProvider resultsProvider)
		{
			List<KeyValuePair<Coordinate, PivotEngine.CalculatedItemDetails>> list = calculatedItemCoordiantes.ToList<KeyValuePair<Coordinate, PivotEngine.CalculatedItemDetails>>();
			list.Sort((KeyValuePair<Coordinate, PivotEngine.CalculatedItemDetails> l, KeyValuePair<Coordinate, PivotEngine.CalculatedItemDetails> r) => l.Value.CalculatedItem.SolveOrder.CompareTo(r.Value.CalculatedItem.SolveOrder));
			AggregateSummaryValues aggregateSummaryValues = new AggregateSummaryValues();
			aggregateSummaryValues.Results = resultsProvider;
			int count = state.AggregateDescriptions.Count;
			IDictionary<Coordinate, AggregateValue[]> dictionary = resultsProvider.Aggregates;
			foreach (KeyValuePair<Coordinate, PivotEngine.CalculatedItemDetails> keyValuePair in list)
			{
				Coordinate key = keyValuePair.Key;
				PivotEngine.CalculatedItemDetails value = keyValuePair.Value;
				CalculatedItem calculatedItem = value.CalculatedItem;
				PivotAxis axis = value.Axis;
				int level = value.Level;
				AggregateValue[] array = new AggregateValue[count];
				dictionary[key] = array;
				for (int i = 0; i < state.AggregateDescriptionInfos.Length; i++)
				{
					AggregateDescriptionInfo aggregateDescriptionInfo = state.AggregateDescriptionInfos[i];
					int originalIndex = aggregateDescriptionInfo.OriginalIndex;
					if (!aggregateDescriptionInfo.IsCalculated)
					{
						try
						{
							aggregateSummaryValues.Axis = axis;
							aggregateSummaryValues.AggregateIndex = originalIndex;
							aggregateSummaryValues.Coordinate = key;
							aggregateSummaryValues.Level = level;
							array[originalIndex] = calculatedItem.GetValue(aggregateSummaryValues);
						}
						catch (Exception)
						{
							if (array[originalIndex] == null)
							{
								array[originalIndex] = AggregateValue.ErrorAggregateValue;
							}
							else
							{
								array[originalIndex].RaiseError();
							}
						}
					}
				}
			}
		}

		// Token: 0x06007E3F RID: 32319 RVA: 0x001CF184 File Offset: 0x001CD384
		private void BeginParallelProcessing(ParallelState parallelInitalState)
		{
			this.initalState = parallelInitalState;
			this.currentResultTask = Task.Factory.StartNew<PivotEngine.GroupingResults>(() => PivotEngine.GenerateBottomLevelsFromSourceParallel(parallelInitalState), parallelInitalState.CancellationToken, TaskCreationOptions.LongRunning, parallelInitalState.TaskScheduler).ContinueWith(delegate(Task<PivotEngine.GroupingResults> task)
			{
				this.ProcessBottomLevelsParallel(task, parallelInitalState);
			}, TaskContinuationOptions.AttachedToParent);
		}

		// Token: 0x06007E40 RID: 32320 RVA: 0x001CF1F8 File Offset: 0x001CD3F8
		private static PivotEngine.GroupingResults GenerateBottomLevelsFromSourceParallel(ParallelState state)
		{
			int num = Math.Max(1, state.MaxDegreeOfParallelism);
			int count = state.ItemsSource.Count;
			int num2 = count % num;
			int num3 = count / num + ((num2 > 0) ? 1 : 0);
			List<Task<PivotEngine.GroupingResults>> list = new List<Task<PivotEngine.GroupingResults>>(num);
			for (int i = 0; i < num; i++)
			{
				int start = i * num3;
				int end = Math.Min((i + 1) * num3, count);
				PivotEngine.BottomLevelGroupingTaskState state2 = new PivotEngine.BottomLevelGroupingTaskState
				{
					ParallelState = state,
					Start = start,
					End = end
				};
				Task<PivotEngine.GroupingResults> item = Task.Factory.StartNew<PivotEngine.GroupingResults>(new Func<object, PivotEngine.GroupingResults>(PivotEngine.ProcessItems), state2, state.CancellationToken, TaskCreationOptions.LongRunning | TaskCreationOptions.AttachedToParent, state.TaskScheduler);
				list.Add(item);
			}
			Task.WaitAll(list.ToArray());
			PivotEngine.GroupingResults result = list[0].Result;
			for (int j = 1; j < num; j++)
			{
				result.Merge(list[j].Result);
			}
			return result;
		}

		// Token: 0x06007E41 RID: 32321 RVA: 0x001CF2F4 File Offset: 0x001CD4F4
		[SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Scope = "member", Target = "Telerik.Web.UI.PivotGrid.Core.Engine.PivotEngine.#ProcessBottomLevelsParallel(System.Threading.Tasks.Task`1<Telerik.Web.UI.PivotGrid.Core.Engine.PivotEngine+GroupingResults>,Telerik.Web.UI.PivotGrid.Core.Engine.ParallelState)", Justification = "Catching all excpetions from different threads.")]
		private void ProcessBottomLevelsParallel(Task<PivotEngine.GroupingResults> bottomLevelResultsTask, ParallelState parallelState)
		{
			AggregateException ex = null;
			PivotEngineStatus status = PivotEngineStatus.Faulted;
			bool flag = this.initalState == parallelState;
			try
			{
				PivotEngine.GroupingResults result = bottomLevelResultsTask.Result;
				if (flag)
				{
					PivotEngine.GroupingFinalizationTaskState finalizationTasksState = new PivotEngine.GroupingFinalizationTaskState
					{
						ParallelState = parallelState,
						Results = result
					};
					this.FinalizeAggregations(result, finalizationTasksState);
					status = PivotEngineStatus.Completed;
				}
			}
			catch (OperationCanceledException)
			{
				status = PivotEngineStatus.Completed;
			}
			catch (AggregateException ex2)
			{
				AggregateException ex3 = ex2.Flatten();
				List<Exception> list = null;
				for (int i = 0; i < ex3.InnerExceptions.Count; i++)
				{
					Exception ex4 = ex3.InnerExceptions[i];
					if (!(ex4 is OperationCanceledException))
					{
						if (list == null)
						{
							list = new List<Exception>();
						}
						list.Add(ex4);
					}
				}
				if (flag)
				{
					if (list != null && list.Count > 0)
					{
						ex = new AggregateException(ex3.ToString(), list);
					}
					else
					{
						status = PivotEngineStatus.Completed;
					}
				}
			}
			catch (Exception ex5)
			{
				ex = new AggregateException(ex5.ToString(), new List<Exception>
				{
					ex5
				});
			}
			finally
			{
				flag = (this.initalState == parallelState);
				if (flag)
				{
					this.currentResultTask = null;
					if (parallelState != null && parallelState.CancellationTokenSource != null)
					{
						parallelState.CancellationTokenSource.Dispose();
						parallelState.CancellationTokenSource = null;
					}
					this.initalState = null;
					ReadOnlyCollection<Exception> innerExceptions;
					if (ex != null)
					{
						innerExceptions = ex.InnerExceptions;
					}
					else
					{
						innerExceptions = new ReadOnlyCollection<Exception>(new List<Exception>());
					}
					this.RaiseCompleted(new PivotEngineCompletedEventArgs(innerExceptions, status));
				}
			}
		}

		// Token: 0x06007E42 RID: 32322 RVA: 0x001CF484 File Offset: 0x001CD684
		public void RebuildCubeParallel(ParallelState state)
		{
			if (state.IsEmpty)
			{
				this.Clear();
				return;
			}
			this.CancelCurrentProcessing();
			state.CancellationTokenSource = new CancellationTokenSource();
			this.RaiseInProgress();
			this.BeginParallelProcessing(state);
		}

		// Token: 0x040022AA RID: 8874
		private IDictionary<Coordinate, AggregateValue[]> aggregates;

		// Token: 0x040022AB RID: 8875
		private IDictionary<Coordinate, AggregateValue[]> summaries;

		// Token: 0x040022AC RID: 8876
		private IDictionary<Coordinate, AggregateValue[]> formattedTotals;

		// Token: 0x040022AD RID: 8877
		private List<List<HashSet<object>>> uniqueGroupKeys;

		// Token: 0x040022AE RID: 8878
		private HashSet<object>[] uniqueFilterItems;

		// Token: 0x040022B0 RID: 8880
		private Task currentResultTask;

		// Token: 0x040022B1 RID: 8881
		private ParallelState initalState;

		// Token: 0x02000D3E RID: 3390
		private struct CalculatedItemDetails
		{
			// Token: 0x06007E46 RID: 32326 RVA: 0x001CF4B3 File Offset: 0x001CD6B3
			public CalculatedItemDetails(CalculatedItem calculatedItem, PivotAxis axis, int level)
			{
				this = default(PivotEngine.CalculatedItemDetails);
				this.CalculatedItem = calculatedItem;
				this.Axis = axis;
				this.Level = level;
			}

			// Token: 0x1700283D RID: 10301
			// (get) Token: 0x06007E47 RID: 32327 RVA: 0x001CF4D1 File Offset: 0x001CD6D1
			// (set) Token: 0x06007E48 RID: 32328 RVA: 0x001CF4D9 File Offset: 0x001CD6D9
			public PivotAxis Axis { get; set; }

			// Token: 0x1700283E RID: 10302
			// (get) Token: 0x06007E49 RID: 32329 RVA: 0x001CF4E2 File Offset: 0x001CD6E2
			// (set) Token: 0x06007E4A RID: 32330 RVA: 0x001CF4EA File Offset: 0x001CD6EA
			public int Level { get; set; }

			// Token: 0x1700283F RID: 10303
			// (get) Token: 0x06007E4B RID: 32331 RVA: 0x001CF4F3 File Offset: 0x001CD6F3
			// (set) Token: 0x06007E4C RID: 32332 RVA: 0x001CF4FB File Offset: 0x001CD6FB
			public CalculatedItem CalculatedItem { get; set; }
		}

		// Token: 0x02000D3F RID: 3391
		private class CountAndLastArrayComparer : IEqualityComparer<object[]>
		{
			// Token: 0x06007E4D RID: 32333 RVA: 0x001CF504 File Offset: 0x001CD704
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

			// Token: 0x06007E4E RID: 32334 RVA: 0x001CF538 File Offset: 0x001CD738
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

		// Token: 0x02000D40 RID: 3392
		private class ObjectArrayComparer : IEqualityComparer<object[]>
		{
			// Token: 0x06007E50 RID: 32336 RVA: 0x001CF570 File Offset: 0x001CD770
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

			// Token: 0x06007E51 RID: 32337 RVA: 0x001CF5A8 File Offset: 0x001CD7A8
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

		// Token: 0x02000D41 RID: 3393
		private class BottomLevelGroupingTaskState
		{
			// Token: 0x17002840 RID: 10304
			// (get) Token: 0x06007E53 RID: 32339 RVA: 0x001CF5E7 File Offset: 0x001CD7E7
			// (set) Token: 0x06007E54 RID: 32340 RVA: 0x001CF5EF File Offset: 0x001CD7EF
			internal int Start { get; set; }

			// Token: 0x17002841 RID: 10305
			// (get) Token: 0x06007E55 RID: 32341 RVA: 0x001CF5F8 File Offset: 0x001CD7F8
			// (set) Token: 0x06007E56 RID: 32342 RVA: 0x001CF600 File Offset: 0x001CD800
			internal int End { get; set; }

			// Token: 0x17002842 RID: 10306
			// (get) Token: 0x06007E57 RID: 32343 RVA: 0x001CF609 File Offset: 0x001CD809
			// (set) Token: 0x06007E58 RID: 32344 RVA: 0x001CF611 File Offset: 0x001CD811
			internal ParallelState ParallelState { get; set; }
		}

		// Token: 0x02000D42 RID: 3394
		private class GroupingFinalizationTaskState
		{
			// Token: 0x17002843 RID: 10307
			// (get) Token: 0x06007E5A RID: 32346 RVA: 0x001CF622 File Offset: 0x001CD822
			// (set) Token: 0x06007E5B RID: 32347 RVA: 0x001CF62A File Offset: 0x001CD82A
			internal PivotEngine.GroupingResults Results { get; set; }

			// Token: 0x17002844 RID: 10308
			// (get) Token: 0x06007E5C RID: 32348 RVA: 0x001CF633 File Offset: 0x001CD833
			// (set) Token: 0x06007E5D RID: 32349 RVA: 0x001CF63B File Offset: 0x001CD83B
			internal ParallelState ParallelState { get; set; }
		}

		// Token: 0x02000D43 RID: 3395
		private class AggregateResultProvider : IAggregateResultProvider
		{
			// Token: 0x17002845 RID: 10309
			// (get) Token: 0x06007E5F RID: 32351 RVA: 0x001CF64C File Offset: 0x001CD84C
			// (set) Token: 0x06007E60 RID: 32352 RVA: 0x001CF654 File Offset: 0x001CD854
			internal IDictionary<Coordinate, AggregateValue[]> Aggregates { get; set; }

			// Token: 0x17002846 RID: 10310
			// (get) Token: 0x06007E61 RID: 32353 RVA: 0x001CF65D File Offset: 0x001CD85D
			// (set) Token: 0x06007E62 RID: 32354 RVA: 0x001CF665 File Offset: 0x001CD865
			internal IDictionary<Coordinate, AggregateValue[]> Summaries { get; set; }

			// Token: 0x17002847 RID: 10311
			// (get) Token: 0x06007E63 RID: 32355 RVA: 0x001CF66E File Offset: 0x001CD86E
			// (set) Token: 0x06007E64 RID: 32356 RVA: 0x001CF676 File Offset: 0x001CD876
			internal AggregateDescriptionInfo[] Infos { get; set; }

			// Token: 0x17002848 RID: 10312
			// (get) Token: 0x06007E65 RID: 32357 RVA: 0x001CF67F File Offset: 0x001CD87F
			// (set) Token: 0x06007E66 RID: 32358 RVA: 0x001CF687 File Offset: 0x001CD887
			public Coordinate Root { get; internal set; }

			// Token: 0x06007E67 RID: 32359 RVA: 0x001CF690 File Offset: 0x001CD890
			public AggregateValue GetAggregateResult(int aggregate, Coordinate groups)
			{
				AggregateValue[] aggregateResults = this.GetAggregateResults(groups);
				int num = -1;
				AggregateDescriptionInfo[] infos = this.Infos;
				for (int i = 0; i < infos.Length; i++)
				{
					if (infos[i].OriginalIndex == aggregate)
					{
						num = i;
						break;
					}
				}
				if (aggregateResults == null || num < 0 || num >= aggregateResults.Length)
				{
					return null;
				}
				return aggregateResults[aggregate];
			}

			// Token: 0x06007E68 RID: 32360 RVA: 0x001CF6E4 File Offset: 0x001CD8E4
			private AggregateValue[] GetAggregateResults(Coordinate coordinate)
			{
				AggregateValue[] result;
				if (this.Aggregates.TryGetValue(coordinate, out result))
				{
					return result;
				}
				if (this.Summaries.TryGetValue(coordinate, out result))
				{
					return result;
				}
				return null;
			}
		}

		// Token: 0x02000D44 RID: 3396
		private class GroupingResults
		{
			// Token: 0x06007E6A RID: 32362 RVA: 0x001CF71E File Offset: 0x001CD91E
			internal GroupingResults(Group rowRoot, Group columnRoot, HashSet<object>[] uniqueFilterItems)
			{
				this.RowRootGroup = rowRoot;
				this.ColumnRootGroup = columnRoot;
				this.Root = new Coordinate(this.RowRootGroup, this.ColumnRootGroup);
				this.UniqueFilterItems = uniqueFilterItems;
			}

			// Token: 0x17002849 RID: 10313
			// (get) Token: 0x06007E6B RID: 32363 RVA: 0x001CF752 File Offset: 0x001CD952
			// (set) Token: 0x06007E6C RID: 32364 RVA: 0x001CF75A File Offset: 0x001CD95A
			public Coordinate Root { get; private set; }

			// Token: 0x1700284A RID: 10314
			// (get) Token: 0x06007E6D RID: 32365 RVA: 0x001CF763 File Offset: 0x001CD963
			// (set) Token: 0x06007E6E RID: 32366 RVA: 0x001CF76B File Offset: 0x001CD96B
			internal Group RowRootGroup { get; private set; }

			// Token: 0x1700284B RID: 10315
			// (get) Token: 0x06007E6F RID: 32367 RVA: 0x001CF774 File Offset: 0x001CD974
			// (set) Token: 0x06007E70 RID: 32368 RVA: 0x001CF77C File Offset: 0x001CD97C
			internal Group ColumnRootGroup { get; private set; }

			// Token: 0x1700284C RID: 10316
			// (get) Token: 0x06007E71 RID: 32369 RVA: 0x001CF785 File Offset: 0x001CD985
			// (set) Token: 0x06007E72 RID: 32370 RVA: 0x001CF78D File Offset: 0x001CD98D
			public IDictionary<Coordinate, AggregateValue[]> Aggregates { get; internal set; }

			// Token: 0x1700284D RID: 10317
			// (get) Token: 0x06007E73 RID: 32371 RVA: 0x001CF796 File Offset: 0x001CD996
			// (set) Token: 0x06007E74 RID: 32372 RVA: 0x001CF79E File Offset: 0x001CD99E
			public HashSet<object>[] UniqueFilterItems { get; private set; }

			// Token: 0x06007E75 RID: 32373 RVA: 0x001CF7A8 File Offset: 0x001CD9A8
			[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Not called for NET20 only.")]
			internal void Merge(PivotEngine.GroupingResults result)
			{
				foreach (KeyValuePair<Coordinate, AggregateValue[]> item in result.Aggregates)
				{
					AggregateValue[] array;
					if (this.Aggregates.TryGetValue(item.Key, out array))
					{
						for (int i = 0; i < array.Length; i++)
						{
							AggregateValue aggregateValue = array[i];
							AggregateValue aggregateValue2 = item.Value[i];
							if (aggregateValue != null && aggregateValue2 != null)
							{
								aggregateValue.MergeCore(aggregateValue2);
							}
							else if ((aggregateValue != null || aggregateValue2 != null) && aggregateValue == null)
							{
								array[i] = aggregateValue2;
							}
						}
					}
					else
					{
						this.Aggregates.Add(item);
					}
				}
				for (int j = 0; j < this.UniqueFilterItems.Length; j++)
				{
					HashSet<object> hashSet = this.UniqueFilterItems[j];
					HashSet<object> hashSet2 = result.UniqueFilterItems[j];
					foreach (object item2 in hashSet2)
					{
						hashSet.Add(item2);
					}
				}
				PivotEngine.GroupingResults.MergeChildGroups(this.RowRootGroup, result.RowRootGroup);
				PivotEngine.GroupingResults.MergeChildGroups(this.ColumnRootGroup, result.ColumnRootGroup);
			}

			// Token: 0x06007E76 RID: 32374 RVA: 0x001CF8E8 File Offset: 0x001CDAE8
			[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Not called for NET20 only.")]
			private static void MergeChildGroups(Group group, Group group2)
			{
				if (group2.HasGroups)
				{
					foreach (Group group3 in group2.InternalGroups)
					{
						Group group4 = group.CreateGroupByName(group3.Name);
						PivotEngine.GroupingResults.MergeChildGroups(group4, group3);
					}
				}
			}
		}
	}
}
