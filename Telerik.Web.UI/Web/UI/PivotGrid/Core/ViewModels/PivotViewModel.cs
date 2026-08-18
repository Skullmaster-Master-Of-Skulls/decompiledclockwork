using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using Telerik.Web.UI.PivotGrid.Core.Aggregates;
using Telerik.Web.UI.PivotGrid.Core.Fields;

namespace Telerik.Web.UI.PivotGrid.Core.ViewModels
{
	// Token: 0x02000D4B RID: 3403
	internal class PivotViewModel : IPivotViewModel
	{
		// Token: 0x06007EA3 RID: 32419 RVA: 0x001CFC84 File Offset: 0x001CDE84
		public PivotViewModel()
		{
			this.GrandTotalText = PivotLocalizationManager.GrandTotal;
			this.ValuesGroupText = PivotLocalizationManager.Values;
			this.GrandTotalGroupNameFormat = PivotLocalizationManager.TotalP0;
			this.SubTotalGroupNameFormat = PivotLocalizationManager.P0Total;
			this.AggregateGroupNameFormat = PivotLocalizationManager.GroupP0AggregateP1;
			this.EmptyValue = string.Empty;
			this.ErrorValue = "Error";
			this.RowsSubTotalsPosition = TotalsPosition.Last;
			this.RowGrandTotalsPosition = TotalsPosition.Last;
			this.ColumnsSubTotalsPosition = TotalsPosition.Last;
			this.ColumnGrandTotalsPosition = TotalsPosition.Last;
		}

		// Token: 0x14000134 RID: 308
		// (add) Token: 0x06007EA4 RID: 32420 RVA: 0x001CFD00 File Offset: 0x001CDF00
		// (remove) Token: 0x06007EA5 RID: 32421 RVA: 0x001CFD38 File Offset: 0x001CDF38
		public event EventHandler<EventArgs> Completed;

		// Token: 0x17002860 RID: 10336
		// (get) Token: 0x06007EA6 RID: 32422 RVA: 0x001CFD6D File Offset: 0x001CDF6D
		// (set) Token: 0x06007EA7 RID: 32423 RVA: 0x001CFD75 File Offset: 0x001CDF75
		public bool ShowSubTotalAggregatesInline { get; set; }

		// Token: 0x17002861 RID: 10337
		// (get) Token: 0x06007EA8 RID: 32424 RVA: 0x001CFD7E File Offset: 0x001CDF7E
		// (set) Token: 0x06007EA9 RID: 32425 RVA: 0x001CFD88 File Offset: 0x001CDF88
		public IDataProvider DataProvider
		{
			get
			{
				return this.dataProvider;
			}
			set
			{
				IDataProvider oldDataProvider = this.dataProvider;
				this.dataProvider = value;
				this.OnDataProviderChanged(oldDataProvider, value);
			}
		}

		// Token: 0x17002862 RID: 10338
		// (get) Token: 0x06007EAA RID: 32426 RVA: 0x001CFDAB File Offset: 0x001CDFAB
		private IPivotResults Results
		{
			get
			{
				if (this.DataProvider == null)
				{
					return null;
				}
				return this.DataProvider.Results;
			}
		}

		// Token: 0x17002863 RID: 10339
		// (get) Token: 0x06007EAB RID: 32427 RVA: 0x001CFDC2 File Offset: 0x001CDFC2
		private IGroupDescription ValuesDescription
		{
			get
			{
				if (this.valuesDescription == null)
				{
					this.valuesDescription = new PivotViewModel.ValuesGroupDescription(this);
				}
				return this.valuesDescription;
			}
		}

		// Token: 0x17002864 RID: 10340
		// (get) Token: 0x06007EAC RID: 32428 RVA: 0x001CFDDE File Offset: 0x001CDFDE
		// (set) Token: 0x06007EAD RID: 32429 RVA: 0x001CFDE6 File Offset: 0x001CDFE6
		public TotalsPosition RowsSubTotalsPosition { get; set; }

		// Token: 0x17002865 RID: 10341
		// (get) Token: 0x06007EAE RID: 32430 RVA: 0x001CFDEF File Offset: 0x001CDFEF
		// (set) Token: 0x06007EAF RID: 32431 RVA: 0x001CFDF7 File Offset: 0x001CDFF7
		public TotalsPosition RowGrandTotalsPosition { get; set; }

		// Token: 0x17002866 RID: 10342
		// (get) Token: 0x06007EB0 RID: 32432 RVA: 0x001CFE00 File Offset: 0x001CE000
		// (set) Token: 0x06007EB1 RID: 32433 RVA: 0x001CFE08 File Offset: 0x001CE008
		public TotalsPosition ColumnsSubTotalsPosition { get; set; }

		// Token: 0x17002867 RID: 10343
		// (get) Token: 0x06007EB2 RID: 32434 RVA: 0x001CFE11 File Offset: 0x001CE011
		// (set) Token: 0x06007EB3 RID: 32435 RVA: 0x001CFE19 File Offset: 0x001CE019
		public TotalsPosition ColumnGrandTotalsPosition { get; set; }

		// Token: 0x17002868 RID: 10344
		// (get) Token: 0x06007EB4 RID: 32436 RVA: 0x001CFE22 File Offset: 0x001CE022
		// (set) Token: 0x06007EB5 RID: 32437 RVA: 0x001CFE2A File Offset: 0x001CE02A
		public string EmptyValue { get; set; }

		// Token: 0x17002869 RID: 10345
		// (get) Token: 0x06007EB6 RID: 32438 RVA: 0x001CFE33 File Offset: 0x001CE033
		// (set) Token: 0x06007EB7 RID: 32439 RVA: 0x001CFE3B File Offset: 0x001CE03B
		public string ErrorValue { get; set; }

		// Token: 0x1700286A RID: 10346
		// (get) Token: 0x06007EB8 RID: 32440 RVA: 0x001CFE44 File Offset: 0x001CE044
		// (set) Token: 0x06007EB9 RID: 32441 RVA: 0x001CFE4C File Offset: 0x001CE04C
		public string GrandTotalText { get; set; }

		// Token: 0x1700286B RID: 10347
		// (get) Token: 0x06007EBA RID: 32442 RVA: 0x001CFE55 File Offset: 0x001CE055
		// (set) Token: 0x06007EBB RID: 32443 RVA: 0x001CFE5D File Offset: 0x001CE05D
		public string ValuesGroupText { get; set; }

		// Token: 0x1700286C RID: 10348
		// (get) Token: 0x06007EBC RID: 32444 RVA: 0x001CFE66 File Offset: 0x001CE066
		// (set) Token: 0x06007EBD RID: 32445 RVA: 0x001CFE6E File Offset: 0x001CE06E
		public string GrandTotalGroupNameFormat { get; set; }

		// Token: 0x1700286D RID: 10349
		// (get) Token: 0x06007EBE RID: 32446 RVA: 0x001CFE77 File Offset: 0x001CE077
		// (set) Token: 0x06007EBF RID: 32447 RVA: 0x001CFE7F File Offset: 0x001CE07F
		public string SubTotalGroupNameFormat { get; set; }

		// Token: 0x1700286E RID: 10350
		// (get) Token: 0x06007EC0 RID: 32448 RVA: 0x001CFE88 File Offset: 0x001CE088
		// (set) Token: 0x06007EC1 RID: 32449 RVA: 0x001CFE90 File Offset: 0x001CE090
		public string AggregateGroupNameFormat { get; set; }

		// Token: 0x1700286F RID: 10351
		// (get) Token: 0x06007EC2 RID: 32450 RVA: 0x001CFE99 File Offset: 0x001CE099
		// (set) Token: 0x06007EC3 RID: 32451 RVA: 0x001CFEA1 File Offset: 0x001CE0A1
		public int RowGroupDescriptionCount { get; private set; }

		// Token: 0x17002870 RID: 10352
		// (get) Token: 0x06007EC4 RID: 32452 RVA: 0x001CFEAA File Offset: 0x001CE0AA
		// (set) Token: 0x06007EC5 RID: 32453 RVA: 0x001CFEB2 File Offset: 0x001CE0B2
		public int ColumnGroupDescriptionCount { get; private set; }

		// Token: 0x17002871 RID: 10353
		// (get) Token: 0x06007EC6 RID: 32454 RVA: 0x001CFEBB File Offset: 0x001CE0BB
		// (set) Token: 0x06007EC7 RID: 32455 RVA: 0x001CFEC3 File Offset: 0x001CE0C3
		public int AggregateDescriptionCount { get; private set; }

		// Token: 0x17002872 RID: 10354
		// (get) Token: 0x06007EC8 RID: 32456 RVA: 0x001CFECC File Offset: 0x001CE0CC
		// (set) Token: 0x06007EC9 RID: 32457 RVA: 0x001CFED4 File Offset: 0x001CE0D4
		public IReadOnlyList<IGroup> RowGroups { get; private set; }

		// Token: 0x17002873 RID: 10355
		// (get) Token: 0x06007ECA RID: 32458 RVA: 0x001CFEDD File Offset: 0x001CE0DD
		// (set) Token: 0x06007ECB RID: 32459 RVA: 0x001CFEE5 File Offset: 0x001CE0E5
		public IReadOnlyList<IGroup> ColumnGroups { get; private set; }

		// Token: 0x17002874 RID: 10356
		// (get) Token: 0x06007ECC RID: 32460 RVA: 0x001CFEEE File Offset: 0x001CE0EE
		IEnumerable<IGroup> IPivotViewModel.RowGroups
		{
			get
			{
				return this.RowGroups;
			}
		}

		// Token: 0x17002875 RID: 10357
		// (get) Token: 0x06007ECD RID: 32461 RVA: 0x001CFEF6 File Offset: 0x001CE0F6
		IEnumerable<IGroup> IPivotViewModel.ColumnGroups
		{
			get
			{
				return this.ColumnGroups;
			}
		}

		// Token: 0x17002876 RID: 10358
		// (get) Token: 0x06007ECE RID: 32462 RVA: 0x001CFEFE File Offset: 0x001CE0FE
		// (set) Token: 0x06007ECF RID: 32463 RVA: 0x001CFF06 File Offset: 0x001CE106
		public int RowLevels { get; private set; }

		// Token: 0x17002877 RID: 10359
		// (get) Token: 0x06007ED0 RID: 32464 RVA: 0x001CFF0F File Offset: 0x001CE10F
		// (set) Token: 0x06007ED1 RID: 32465 RVA: 0x001CFF17 File Offset: 0x001CE117
		public int ColumnLevels { get; private set; }

		// Token: 0x17002878 RID: 10360
		// (get) Token: 0x06007ED2 RID: 32466 RVA: 0x001CFF20 File Offset: 0x001CE120
		// (set) Token: 0x06007ED3 RID: 32467 RVA: 0x001CFF28 File Offset: 0x001CE128
		public bool IsReady { get; private set; }

		// Token: 0x17002879 RID: 10361
		// (get) Token: 0x06007ED4 RID: 32468 RVA: 0x001CFF31 File Offset: 0x001CE131
		private PivotAxis AggregatesPosition
		{
			get
			{
				if (this.dataProvider != null)
				{
					return this.dataProvider.AggregatesPosition;
				}
				return PivotAxis.Columns;
			}
		}

		// Token: 0x1700287A RID: 10362
		// (get) Token: 0x06007ED5 RID: 32469 RVA: 0x001CFF48 File Offset: 0x001CE148
		private int AggregatesLevel
		{
			get
			{
				if (this.dataProvider != null)
				{
					return this.dataProvider.AggregatesLevel;
				}
				return -1;
			}
		}

		// Token: 0x06007ED6 RID: 32470 RVA: 0x001CFF5F File Offset: 0x001CE15F
		private void OnDataProviderChanged(IDataProvider oldDataProvider, IDataProvider newDataProvider)
		{
			if (oldDataProvider != null)
			{
				oldDataProvider.StatusChanged -= this.OnDataProviderStatusChanged;
			}
			if (newDataProvider != null)
			{
				newDataProvider.StatusChanged += this.OnDataProviderStatusChanged;
			}
			this.RebuildOrCleanViewModel();
		}

		// Token: 0x06007ED7 RID: 32471 RVA: 0x001CFF91 File Offset: 0x001CE191
		private void RebuildOrCleanViewModel()
		{
			if (this.DataProvider == null)
			{
				this.CleanUp();
				this.RaiseCompleted();
				return;
			}
			if (this.DataProvider.Status == DataProviderStatus.Ready)
			{
				this.RebuildViewModel();
				this.RaiseCompleted();
				return;
			}
			this.CleanUp();
			this.RaiseCompleted();
		}

		// Token: 0x06007ED8 RID: 32472 RVA: 0x001CFFCF File Offset: 0x001CE1CF
		private IAggregateDescription GetAggregateDescription(int index)
		{
			if (this.Results != null && index >= 0 && index < this.AggregateDescriptionCount)
			{
				return this.Results.AggregateDescriptions[index];
			}
			return null;
		}

		// Token: 0x06007ED9 RID: 32473 RVA: 0x001CFFFC File Offset: 0x001CE1FC
		public CellAggregateValue GetAggregateValue(IGroup row, IGroup column, bool collapsedRow, bool collapsedColumn)
		{
			GroupNode groupNode = row as GroupNode;
			GroupNode groupNode2 = column as GroupNode;
			IGroup group = groupNode.Group;
			IGroup group2 = groupNode2.Group;
			if (groupNode == null || groupNode2 == null || this.AggregateDescriptionCount < 1)
			{
				return null;
			}
			if (this.IsBlankCell(collapsedRow, collapsedColumn, groupNode, groupNode2))
			{
				return null;
			}
			CellAggregateValue cellAggregateValue = new CellAggregateValue();
			cellAggregateValue.RowGroup = row;
			cellAggregateValue.ColumnGroup = column;
			int num = this.AggregateIndexAtGroups(groupNode, groupNode2);
			if (num >= 0 && num < this.AggregateDescriptionCount)
			{
				cellAggregateValue.Description = this.Results.AggregateDescriptions[num];
				AggregateValue aggregateResult = this.Results.GetAggregateResult(num, group, group2);
				if (aggregateResult != null)
				{
					object value = aggregateResult.GetValue();
					cellAggregateValue.Value = value;
					if (value is AggregateError)
					{
						cellAggregateValue.FormattedValue = this.ErrorValue;
					}
					else
					{
						cellAggregateValue.FormattedValue = aggregateResult.ToString();
					}
				}
				else
				{
					cellAggregateValue.FormattedValue = this.EmptyValue;
				}
			}
			else
			{
				cellAggregateValue.FormattedValue = this.EmptyValue;
			}
			return cellAggregateValue;
		}

		// Token: 0x06007EDA RID: 32474 RVA: 0x001D00FE File Offset: 0x001CE2FE
		private int AggregateIndexAtGroups(GroupNode rowNodeGroup, GroupNode columnNodeGroup)
		{
			if (this.AggregateDescriptionCount <= 1)
			{
				return 0;
			}
			if (this.AggregatesPosition != PivotAxis.Rows)
			{
				return columnNodeGroup.AggregateIndex;
			}
			return rowNodeGroup.AggregateIndex;
		}

		// Token: 0x06007EDB RID: 32475 RVA: 0x001D0120 File Offset: 0x001CE320
		private bool IsBlankCell(bool collapsedRow, bool collapsedColumn, GroupNode rowNodeGroup, GroupNode columnNodeGroup)
		{
			bool result = false;
			if (this.AggregatesPosition == PivotAxis.Rows)
			{
				if (this.IsBlankRow(collapsedRow, collapsedColumn, rowNodeGroup, columnNodeGroup))
				{
					result = true;
				}
			}
			else if (this.IsBlankColumn(collapsedRow, collapsedColumn, rowNodeGroup, columnNodeGroup))
			{
				result = true;
			}
			return result;
		}

		// Token: 0x06007EDC RID: 32476 RVA: 0x001D0158 File Offset: 0x001CE358
		private bool IsBlankColumn(bool collapsedRow, bool collapsedColumn, GroupNode rowNodeGroup, GroupNode columnNodeGroup)
		{
			bool result = false;
			if (!collapsedColumn && this.ColumnsSubTotalsPosition != TotalsPosition.Inline && columnNodeGroup.Type == GroupType.Subheading && columnNodeGroup.HasGroups && (!this.ShowSubTotalAggregatesInline || this.AggregateDescriptionCount <= 1))
			{
				result = true;
			}
			if (!collapsedRow && this.RowsSubTotalsPosition != TotalsPosition.Inline && rowNodeGroup.Type == GroupType.Subheading && rowNodeGroup.HasGroups)
			{
				result = true;
			}
			return result;
		}

		// Token: 0x06007EDD RID: 32477 RVA: 0x001D01BC File Offset: 0x001CE3BC
		private bool IsBlankRow(bool collapsedRow, bool collapsedColumn, GroupNode rowNodeGroup, GroupNode columnNodeGroup)
		{
			bool result = false;
			if (!collapsedRow && this.RowsSubTotalsPosition != TotalsPosition.Inline && rowNodeGroup.Type == GroupType.Subheading && rowNodeGroup.HasGroups && (!this.ShowSubTotalAggregatesInline || this.AggregateDescriptionCount <= 1))
			{
				result = true;
			}
			if (!collapsedColumn && this.ColumnsSubTotalsPosition != TotalsPosition.Inline && columnNodeGroup.Type == GroupType.Subheading && columnNodeGroup.HasGroups)
			{
				result = true;
			}
			return result;
		}

		// Token: 0x06007EDE RID: 32478 RVA: 0x001D021D File Offset: 0x001CE41D
		public void Refresh()
		{
			this.RebuildOrCleanViewModel();
		}

		// Token: 0x06007EDF RID: 32479 RVA: 0x001D0225 File Offset: 0x001CE425
		private void OnDataProviderStatusChanged(object sender, DataProviderStatusChangedEventArgs e)
		{
			if (e.NewStatus == DataProviderStatus.Ready && e.ResultsChanged)
			{
				this.RebuildViewModel();
			}
			else
			{
				this.CleanUp();
			}
			this.RaiseCompleted();
		}

		// Token: 0x06007EE0 RID: 32480 RVA: 0x001D024C File Offset: 0x001CE44C
		private void UpdateLocalState()
		{
			this.UpdateAggregateCount();
			this.UpdateColumnGroupDescriptionsCount();
			this.UpdateRowGroupDescriptionsCount();
		}

		// Token: 0x06007EE1 RID: 32481 RVA: 0x001D0260 File Offset: 0x001CE460
		private void UpdateColumnGroupDescriptionsCount()
		{
			int num = 0;
			if (this.Results != null)
			{
				foreach (GroupDescription groupDescription in this.Results.ColumnGroupDescriptions)
				{
					IHierarchyGroupDescription hierarchyGroupDescription = groupDescription as IHierarchyGroupDescription;
					if (hierarchyGroupDescription != null)
					{
						num += hierarchyGroupDescription.LevelsCount;
					}
					else
					{
						num++;
					}
				}
			}
			this.ColumnGroupDescriptionCount = num;
		}

		// Token: 0x06007EE2 RID: 32482 RVA: 0x001D02D8 File Offset: 0x001CE4D8
		private void UpdateRowGroupDescriptionsCount()
		{
			int num = 0;
			if (this.Results != null)
			{
				foreach (GroupDescription groupDescription in this.Results.RowGroupDescriptions)
				{
					IHierarchyGroupDescription hierarchyGroupDescription = groupDescription as IHierarchyGroupDescription;
					if (hierarchyGroupDescription != null)
					{
						num += hierarchyGroupDescription.LevelsCount;
					}
					else
					{
						num++;
					}
				}
			}
			this.RowGroupDescriptionCount = num;
		}

		// Token: 0x06007EE3 RID: 32483 RVA: 0x001D0350 File Offset: 0x001CE550
		private void UpdateAggregateCount()
		{
			int aggregateDescriptionCount = 0;
			if (this.Results != null)
			{
				aggregateDescriptionCount = this.Results.AggregateDescriptions.Count;
			}
			this.AggregateDescriptionCount = aggregateDescriptionCount;
		}

		// Token: 0x06007EE4 RID: 32484 RVA: 0x001D037F File Offset: 0x001CE57F
		private void RaiseCompleted()
		{
			this.IsReady = true;
			if (this.Completed != null)
			{
				this.Completed(this, EventArgs.Empty);
			}
		}

		// Token: 0x06007EE5 RID: 32485 RVA: 0x001D03A1 File Offset: 0x001CE5A1
		private void CleanUp()
		{
			this.IsReady = false;
			this.RowGroups = new ReadOnlyList<GroupNode, IGroup>(new List<GroupNode>());
			this.ColumnGroups = new ReadOnlyList<GroupNode, IGroup>(new List<GroupNode>());
			this.RowLevels = 0;
			this.ColumnLevels = 0;
		}

		// Token: 0x06007EE6 RID: 32486 RVA: 0x001D03D8 File Offset: 0x001CE5D8
		private void RebuildViewModel()
		{
			this.IsReady = false;
			this.UpdateLocalState();
			this.CoerceAggregatesLevel();
			this.RebuildRowsAndColumns();
		}

		// Token: 0x06007EE7 RID: 32487 RVA: 0x001D03F4 File Offset: 0x001CE5F4
		private static bool ShouldRespectGrandTotalPositionProperty(IReadOnlyList<GroupDescription> descriptions)
		{
			bool flag = true;
			foreach (GroupDescription groupDescription in descriptions)
			{
				IGrandTotalSupport grandTotalSupport = groupDescription;
				if (grandTotalSupport != null)
				{
					flag = (flag && grandTotalSupport.SupportsGrandTotal);
				}
			}
			return flag;
		}

		// Token: 0x06007EE8 RID: 32488 RVA: 0x001D044C File Offset: 0x001CE64C
		private TotalsPosition GetEffectiveRowGrandTotalsPosition()
		{
			if (this.Results == null)
			{
				return this.RowGrandTotalsPosition;
			}
			bool flag = PivotViewModel.ShouldRespectGrandTotalPositionProperty(this.Results.RowGroupDescriptions);
			if (flag)
			{
				return this.RowGrandTotalsPosition;
			}
			return TotalsPosition.None;
		}

		// Token: 0x06007EE9 RID: 32489 RVA: 0x001D0484 File Offset: 0x001CE684
		private TotalsPosition GetEffectiveColumnGrandTotalsPosition()
		{
			if (this.Results == null)
			{
				return this.RowGrandTotalsPosition;
			}
			bool flag = PivotViewModel.ShouldRespectGrandTotalPositionProperty(this.Results.ColumnGroupDescriptions);
			if (flag)
			{
				return this.ColumnGrandTotalsPosition;
			}
			return TotalsPosition.None;
		}

		// Token: 0x06007EEA RID: 32490 RVA: 0x001D04BC File Offset: 0x001CE6BC
		[SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = "Design choice.")]
		private void RebuildRowsAndColumns()
		{
			this.RowLevels = this.RowGroupDescriptionCount + ((this.AggregatesPosition == PivotAxis.Rows && this.AggregateDescriptionCount > 1) ? 1 : 0);
			this.ColumnLevels = this.ColumnGroupDescriptionCount + ((this.AggregatesPosition == PivotAxis.Columns && this.AggregateDescriptionCount > 1) ? 1 : 0);
			TotalsPosition effectiveRowGrandTotalsPosition = this.GetEffectiveRowGrandTotalsPosition();
			TotalsPosition effectiveColumnGrandTotalsPosition = this.GetEffectiveColumnGrandTotalsPosition();
			int aggregatesLevel = (this.AggregatesPosition == PivotAxis.Rows) ? this.CoerceAggregatesLevel() : -1;
			List<TotalsPosition> list = new List<TotalsPosition>(2);
			list.Add(effectiveRowGrandTotalsPosition);
			list.Add(this.RowsSubTotalsPosition);
			List<GroupNode> list2 = new List<GroupNode>();
			if (this.Results != null && this.Results.Root.RowGroup != null && (this.Results.Root.RowGroup.HasGroups || this.AggregateDescriptionCount > 0))
			{
				bool showSubTotalAggregatesInline = this.AggregatesPosition == PivotAxis.Rows && this.AggregateDescriptionCount > 1 && this.ShowSubTotalAggregatesInline;
				list2 = this.CreateGroupNodes(this.Results.Root.RowGroup, aggregatesLevel, this.RowLevels, this.RowsSubTotalsPosition, effectiveRowGrandTotalsPosition, showSubTotalAggregatesInline);
			}
			this.RowGroups = new ReadOnlyList<GroupNode, IGroup>(list2);
			if (this.RowGroups.Count > 0)
			{
				this.RowLevels = Math.Max(1, this.RowLevels);
			}
			list = new List<TotalsPosition>(2);
			list.Add(effectiveColumnGrandTotalsPosition);
			list.Add(this.ColumnsSubTotalsPosition);
			int aggregatesLevel2 = (this.AggregatesPosition == PivotAxis.Columns) ? this.CoerceAggregatesLevel() : -1;
			IList<GroupNode> list3 = new List<GroupNode>();
			if (this.Results != null && this.Results.Root.ColumnGroup != null && (this.Results.Root.ColumnGroup.HasGroups || this.AggregateDescriptionCount > 0))
			{
				bool showSubTotalAggregatesInline2 = this.AggregatesPosition == PivotAxis.Columns && this.AggregateDescriptionCount > 1 && this.ShowSubTotalAggregatesInline;
				list3 = this.CreateGroupNodes(this.Results.Root.ColumnGroup, aggregatesLevel2, this.ColumnLevels, this.ColumnsSubTotalsPosition, effectiveColumnGrandTotalsPosition, showSubTotalAggregatesInline2);
			}
			this.ColumnGroups = new ReadOnlyList<GroupNode, IGroup>(list3);
			if (this.ColumnGroups.Count > 0)
			{
				this.ColumnLevels = Math.Max(1, this.ColumnLevels);
			}
			if (this.Results != null && this.AggregateDescriptionCount > 0 && (this.RowLevels > 0 || this.ColumnLevels > 0))
			{
				if (this.Results.Root.RowGroup != null && list2.Count == 0)
				{
					list2.Add(new GroupNode(this.Results.Root.RowGroup, null, GroupType.GrandTotal, 0, this.GrandTotalText));
					this.RowLevels = 1;
				}
				if (this.Results.Root.ColumnGroup != null && list3.Count == 0)
				{
					list3.Add(new GroupNode(this.Results.Root.ColumnGroup, null, GroupType.GrandTotal, 0, this.GrandTotalText));
					this.ColumnLevels = 1;
				}
			}
		}

		// Token: 0x06007EEB RID: 32491 RVA: 0x001D07C4 File Offset: 0x001CE9C4
		private List<GroupNode> CreateGroupNodes(IGroup rootGroup, int aggregatesLevel, int groupLevels, TotalsPosition totalsPosition, TotalsPosition grandTotalsPosition, bool showSubTotalAggregatesInline)
		{
			int num = 0;
			List<GroupNode> list = new List<GroupNode>();
			if (aggregatesLevel == 0)
			{
				GroupNode parent = null;
				GroupType type = (num + 1 < groupLevels) ? GroupType.Subheading : GroupType.BottomLevel;
				if (showSubTotalAggregatesInline && num >= aggregatesLevel)
				{
					totalsPosition = TotalsPosition.Inline;
				}
				bool flag = showSubTotalAggregatesInline || num + 1 == groupLevels;
				for (int i = 0; i < this.AggregateDescriptionCount; i++)
				{
					IAggregateDescription aggregateDescription = this.GetAggregateDescription(i);
					GroupNode groupNode = new GroupNode(rootGroup, parent, type, flag ? i : -1, aggregateDescription.DisplayName);
					list.Add(groupNode);
					this.Process(groupNode, totalsPosition, 1, groupLevels, i, aggregatesLevel, showSubTotalAggregatesInline);
				}
			}
			else if (rootGroup.HasGroups)
			{
				IReadOnlyList<IGroup> groups = rootGroup.Groups;
				for (int j = 0; j < groups.Count; j++)
				{
					IGroup group = groups[j];
					GroupType type2 = (num + 1 < groupLevels) ? GroupType.Subheading : GroupType.BottomLevel;
					GroupNode groupNode2 = new GroupNode(group, null, type2, 0);
					list.Add(groupNode2);
					this.Process(groupNode2, totalsPosition, 1, groupLevels, -1, aggregatesLevel, showSubTotalAggregatesInline);
				}
			}
			if (groupLevels == 1 && aggregatesLevel == 0)
			{
				return list;
			}
			switch (grandTotalsPosition)
			{
			case TotalsPosition.Last:
				if (aggregatesLevel == -1 || this.AggregateDescriptionCount < 2)
				{
					GroupNode item = new GroupNode(rootGroup, null, GroupType.GrandTotal, 0, this.GrandTotalText);
					list.Add(item);
				}
				else
				{
					for (int k = 0; k < this.AggregateDescriptionCount; k++)
					{
						IAggregateDescription aggregateDescription2 = this.GetAggregateDescription(k);
						string customName = string.Format(CultureInfo.InvariantCulture, this.GrandTotalGroupNameFormat, new object[]
						{
							aggregateDescription2.DisplayName
						});
						GroupNode item2 = new GroupNode(rootGroup, null, GroupType.GrandTotal, k, customName);
						list.Add(item2);
					}
				}
				break;
			case TotalsPosition.First:
			case TotalsPosition.Inline:
				if (aggregatesLevel == -1 || this.AggregateDescriptionCount < 2)
				{
					GroupNode item3 = new GroupNode(rootGroup, null, GroupType.GrandTotal, 0, this.GrandTotalText);
					list.Insert(0, item3);
				}
				else
				{
					for (int l = 0; l < this.AggregateDescriptionCount; l++)
					{
						IAggregateDescription aggregateDescription3 = this.GetAggregateDescription(l);
						string customName2 = string.Format(CultureInfo.InvariantCulture, this.GrandTotalGroupNameFormat, new object[]
						{
							aggregateDescription3.DisplayName
						});
						GroupNode item4 = new GroupNode(rootGroup, null, GroupType.GrandTotal, l, customName2);
						list.Insert(l, item4);
					}
				}
				break;
			}
			return list;
		}

		// Token: 0x06007EEC RID: 32492 RVA: 0x001D09FC File Offset: 0x001CEBFC
		private void CreateAggregateGroups(TotalsPosition subTotalsPosition, int level, int aggregatesLevel, int groupLevels, IGroup group, GroupNode groupNode, GroupType totalType, bool showSubTotalAggregatesInline)
		{
			TotalsPosition subTotalsPosition2 = subTotalsPosition;
			if (showSubTotalAggregatesInline && level >= aggregatesLevel)
			{
				subTotalsPosition2 = TotalsPosition.Inline;
			}
			bool flag = showSubTotalAggregatesInline || level == groupLevels;
			for (int i = 0; i < this.AggregateDescriptionCount; i++)
			{
				IAggregateDescription aggregateDescription = this.GetAggregateDescription(i);
				GroupNode groupNode2 = new GroupNode(group, groupNode, totalType, i, (level == 0) ? string.Format(CultureInfo.InvariantCulture, this.GrandTotalGroupNameFormat, new object[]
				{
					aggregateDescription.DisplayName
				}) : aggregateDescription.DisplayName);
				groupNode.InternalGroups.Add(groupNode2);
				if (!flag && level == aggregatesLevel && level != groupLevels - 1)
				{
					groupNode2.AggregateIndex = -1;
				}
				this.Process(groupNode2, subTotalsPosition2, level + 1, groupLevels, i, aggregatesLevel, showSubTotalAggregatesInline);
			}
		}

		// Token: 0x06007EED RID: 32493 RVA: 0x001D0AB4 File Offset: 0x001CECB4
		private void Process(GroupNode groupNode, TotalsPosition subTotalsPosition, int level, int groupLevels, int aggregateIndex, int aggregatesLevel, bool showSubTotalAggregatesInline)
		{
			if (level < groupLevels)
			{
				GroupType groupType = (level < groupLevels) ? GroupType.Subtotal : GroupType.BottomLevel;
				switch (subTotalsPosition)
				{
				case TotalsPosition.Last:
					this.CreateSubGroups(groupNode, subTotalsPosition, level, groupLevels, aggregateIndex, aggregatesLevel, showSubTotalAggregatesInline);
					this.CreateSubTotals(groupNode, groupType, level, aggregatesLevel, aggregateIndex, groupLevels, showSubTotalAggregatesInline);
					groupNode.AggregateIndex = aggregateIndex;
					return;
				case TotalsPosition.First:
					this.CreateSubTotals(groupNode, groupType, level, aggregatesLevel, aggregateIndex, groupLevels, showSubTotalAggregatesInline);
					this.CreateSubGroups(groupNode, subTotalsPosition, level, groupLevels, aggregateIndex, aggregatesLevel, showSubTotalAggregatesInline);
					groupNode.AggregateIndex = aggregateIndex;
					return;
				case TotalsPosition.Inline:
					if (aggregatesLevel > 0 && this.AggregateDescriptionCount > 1 && level <= aggregatesLevel)
					{
						this.CreateSubTotals(groupNode, groupType, level, aggregatesLevel, aggregateIndex, groupLevels, showSubTotalAggregatesInline);
						groupNode.AggregateIndex = -1;
					}
					this.CreateSubGroups(groupNode, subTotalsPosition, level, groupLevels, aggregateIndex, aggregatesLevel, showSubTotalAggregatesInline);
					return;
				case TotalsPosition.None:
					groupNode.AggregateIndex = aggregateIndex;
					this.CreateSubGroups(groupNode, subTotalsPosition, level, groupLevels, aggregateIndex, aggregatesLevel, showSubTotalAggregatesInline);
					break;
				default:
					return;
				}
			}
		}

		// Token: 0x06007EEE RID: 32494 RVA: 0x001D0B98 File Offset: 0x001CED98
		[SuppressMessage("Microsoft.Usage", "CA2233:OperationsShouldNotOverflow", Justification = "Not a real issue.")]
		private void CreateSubGroups(GroupNode groupNode, TotalsPosition subTotalsPosition, int level, int groupLevels, int aggregateIndex, int aggregatesLevel, bool showSubTotalAggregatesInline)
		{
			if (level == aggregatesLevel)
			{
				IGroup group = groupNode.Group;
				GroupType totalType = (level + 1 < groupLevels) ? GroupType.Subheading : GroupType.BottomLevel;
				this.CreateAggregateGroups(subTotalsPosition, level, aggregatesLevel, groupLevels, group, groupNode, totalType, showSubTotalAggregatesInline);
				return;
			}
			this.CreateSubHeadings(groupNode, subTotalsPosition, level, groupLevels, aggregateIndex, aggregatesLevel, showSubTotalAggregatesInline);
		}

		// Token: 0x06007EEF RID: 32495 RVA: 0x001D0BE0 File Offset: 0x001CEDE0
		[SuppressMessage("Microsoft.Usage", "CA2233:OperationsShouldNotOverflow", Justification = "Not a real issue.")]
		private void CreateSubTotals(GroupNode groupNode, GroupType groupType, int level, int aggregatesLevel, int aggregateIndex, int groupLevels, bool showSubTotalAggregatesInline)
		{
			if ((level >= aggregatesLevel && showSubTotalAggregatesInline) || level == groupLevels)
			{
				return;
			}
			if (!groupNode.Group.HasGroups)
			{
				return;
			}
			if (aggregatesLevel == -1)
			{
				IGroup group = groupNode.Group;
				GroupNode item = new GroupNode(group, groupNode, groupType, 0, string.Format(CultureInfo.InvariantCulture, this.SubTotalGroupNameFormat, new object[]
				{
					group.Name
				}));
				groupNode.InternalGroups.Add(item);
				return;
			}
			if (aggregateIndex == -1 && level < groupLevels - 1)
			{
				for (int i = 0; i < this.AggregateDescriptionCount; i++)
				{
					this.CreateSubTotal(groupNode, groupType, i, level, aggregatesLevel);
				}
				return;
			}
			if (level > aggregatesLevel + 1)
			{
				this.CreateSubTotal(groupNode, groupType, aggregateIndex, level, aggregatesLevel);
			}
		}

		// Token: 0x06007EF0 RID: 32496 RVA: 0x001D0C8C File Offset: 0x001CEE8C
		private void CreateSubTotal(GroupNode groupNode, GroupType groupType, int aggregateIndex, int level, int aggregatesLevel)
		{
			IGroup group = groupNode.Group;
			IAggregateDescription aggregateDescription = this.GetAggregateDescription(aggregateIndex);
			string customName;
			if (level <= aggregatesLevel)
			{
				customName = string.Format(CultureInfo.InvariantCulture, this.AggregateGroupNameFormat, new object[]
				{
					group.Name,
					aggregateDescription.DisplayName
				});
			}
			else
			{
				customName = string.Format(CultureInfo.InvariantCulture, this.SubTotalGroupNameFormat, new object[]
				{
					group.Name
				});
			}
			GroupNode item = new GroupNode(group, groupNode, groupType, aggregateIndex, customName);
			groupNode.InternalGroups.Add(item);
		}

		// Token: 0x06007EF1 RID: 32497 RVA: 0x001D0D1C File Offset: 0x001CEF1C
		[SuppressMessage("Microsoft.Usage", "CA2233:OperationsShouldNotOverflow", Justification = "Not a real issue.")]
		private void CreateSubHeadings(GroupNode groupNode, TotalsPosition subTotalsPosition, int level, int groupLevels, int aggregateIndex, int aggregatesLevel, bool showSubTotalAggregatesInline)
		{
			if (groupNode.Group.HasGroups)
			{
				IReadOnlyList<IGroup> groups = groupNode.Group.Groups;
				int aggregateIndex2 = (aggregatesLevel == -1) ? 0 : aggregateIndex;
				for (int i = 0; i < groups.Count; i++)
				{
					IGroup group = groups[i];
					GroupType type = (level + 1 < groupLevels) ? GroupType.Subheading : GroupType.BottomLevel;
					GroupNode groupNode2 = new GroupNode(group, groupNode, type, aggregateIndex2);
					groupNode.InternalGroups.Add(groupNode2);
					this.Process(groupNode2, subTotalsPosition, level + 1, groupLevels, aggregateIndex, aggregatesLevel, showSubTotalAggregatesInline);
				}
			}
		}

		// Token: 0x06007EF2 RID: 32498 RVA: 0x001D0DA0 File Offset: 0x001CEFA0
		private int CoerceAggregatesLevel()
		{
			if (this.AggregateDescriptionCount < 2)
			{
				return -1;
			}
			switch (this.AggregatesPosition)
			{
			case PivotAxis.Rows:
			{
				int num = Math.Max(0, this.RowGroupDescriptionCount);
				if (this.AggregatesLevel < 0)
				{
					return num;
				}
				return Math.Min(num, this.AggregatesLevel);
			}
			case PivotAxis.Columns:
			{
				int num2 = Math.Max(0, this.ColumnGroupDescriptionCount);
				if (this.AggregatesLevel < 0)
				{
					return num2;
				}
				return Math.Min(num2, this.AggregatesLevel);
			}
			default:
				return -1;
			}
		}

		// Token: 0x06007EF3 RID: 32499 RVA: 0x001D0E1A File Offset: 0x001CF01A
		public IGroupDescription GetRowGroupDescription(IGroup group)
		{
			return this.GetGroupDescription(group, PivotAxis.Rows);
		}

		// Token: 0x06007EF4 RID: 32500 RVA: 0x001D0E24 File Offset: 0x001CF024
		public IGroupDescription GetColumnGroupDescription(IGroup group)
		{
			return this.GetGroupDescription(group, PivotAxis.Columns);
		}

		// Token: 0x06007EF5 RID: 32501 RVA: 0x001D0E30 File Offset: 0x001CF030
		private IGroupDescription GetGroupDescription(IGroup group, PivotAxis position)
		{
			GroupNode groupNode = (GroupNode)group;
			if (groupNode.Type == GroupType.Subtotal || groupNode.Type == GroupType.GrandTotal)
			{
				return this.ValuesDescription;
			}
			int num = group.Level;
			int num2 = this.CoerceAggregatesLevel();
			if (this.AggregatesPosition == position)
			{
				if (num > num2 && num2 != -1)
				{
					num--;
				}
				else if (num == num2)
				{
					return this.ValuesDescription;
				}
			}
			IReadOnlyList<GroupDescription> readOnlyList = (this.Results == null) ? null : ((position == PivotAxis.Rows) ? this.Results.RowGroupDescriptions : this.Results.ColumnGroupDescriptions);
			if (readOnlyList != null && num >= 0 && num < readOnlyList.Count)
			{
				return readOnlyList[num];
			}
			return null;
		}

		// Token: 0x040022DE RID: 8926
		private IDataProvider dataProvider;

		// Token: 0x040022DF RID: 8927
		private IGroupDescription valuesDescription;

		// Token: 0x02000D4C RID: 3404
		private class ValuesGroupDescription : DescriptionBase, IGroupDescription, IDescriptionBase, INamed
		{
			// Token: 0x06007EF6 RID: 32502 RVA: 0x001D0ECD File Offset: 0x001CF0CD
			public ValuesGroupDescription(PivotViewModel pivotViewModel)
			{
				this.parent = pivotViewModel;
			}

			// Token: 0x1700287B RID: 10363
			// (get) Token: 0x06007EF7 RID: 32503 RVA: 0x001D0EDC File Offset: 0x001CF0DC
			public SortOrder SortOrder
			{
				get
				{
					return SortOrder.Ascending;
				}
			}

			// Token: 0x1700287C RID: 10364
			// (get) Token: 0x06007EF8 RID: 32504 RVA: 0x001D0EDF File Offset: 0x001CF0DF
			public GroupComparer GroupComparer
			{
				get
				{
					return null;
				}
			}

			// Token: 0x06007EF9 RID: 32505 RVA: 0x001D0EE2 File Offset: 0x001CF0E2
			protected override string GetDisplayName()
			{
				return this.parent.ValuesGroupText;
			}

			// Token: 0x06007EFA RID: 32506 RVA: 0x001D0EEF File Offset: 0x001CF0EF
			internal override IPivotFieldInfo GetFieldInfo()
			{
				return null;
			}

			// Token: 0x06007EFB RID: 32507 RVA: 0x001D0EF2 File Offset: 0x001CF0F2
			public override string GetUniqueName()
			{
				return string.Empty;
			}

			// Token: 0x06007EFC RID: 32508 RVA: 0x001D0EF9 File Offset: 0x001CF0F9
			protected override Cloneable CreateInstanceCore()
			{
				throw new NotImplementedException();
			}

			// Token: 0x040022F5 RID: 8949
			private PivotViewModel parent;
		}
	}
}
