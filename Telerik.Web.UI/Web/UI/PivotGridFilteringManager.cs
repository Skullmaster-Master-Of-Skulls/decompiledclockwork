using System;
using System.ComponentModel;
using System.Linq;
using System.Web.Script.Serialization;
using Telerik.Web.UI.PivotGrid.Core;
using Telerik.Web.UI.PivotGrid.Core.Filtering;
using Telerik.Web.UI.PivotGrid.DataProviders.Adomd;
using Telerik.Web.UI.PivotGrid.Xmla;

namespace Telerik.Web.UI
{
	// Token: 0x02000DB1 RID: 3505
	internal class PivotGridFilteringManager
	{
		// Token: 0x060082CE RID: 33486 RVA: 0x001DCFFC File Offset: 0x001DB1FC
		public PivotGridFilteringManager(RadPivotGrid owner)
		{
			this.ownerPivotGrid = owner;
		}

		// Token: 0x17002959 RID: 10585
		// (get) Token: 0x060082CF RID: 33487 RVA: 0x001DD00B File Offset: 0x001DB20B
		public RadPivotGrid OwnerPivotGrid
		{
			get
			{
				return this.ownerPivotGrid;
			}
		}

		// Token: 0x1700295A RID: 10586
		// (get) Token: 0x060082D0 RID: 33488 RVA: 0x001DD013 File Offset: 0x001DB213
		public RadListBox SetBox
		{
			get
			{
				return this.OwnerPivotGrid.FilterWindow.SetBox;
			}
		}

		// Token: 0x1700295B RID: 10587
		// (get) Token: 0x060082D1 RID: 33489 RVA: 0x001DD025 File Offset: 0x001DB225
		public PivotGridFilterWindow FilterWindow
		{
			get
			{
				return this.OwnerPivotGrid.FilterWindow;
			}
		}

		// Token: 0x1700295C RID: 10588
		// (get) Token: 0x060082D2 RID: 33490 RVA: 0x001DD032 File Offset: 0x001DB232
		public PivotGridFilterDialog FilterDialog
		{
			get
			{
				return this.OwnerPivotGrid.FilterDialog;
			}
		}

		// Token: 0x1700295D RID: 10589
		// (get) Token: 0x060082D3 RID: 33491 RVA: 0x001DD03F File Offset: 0x001DB23F
		// (set) Token: 0x060082D4 RID: 33492 RVA: 0x001DD047 File Offset: 0x001DB247
		[DefaultValue("")]
		public string FieldUniqueName { get; set; }

		// Token: 0x1700295E RID: 10590
		// (get) Token: 0x060082D5 RID: 33493 RVA: 0x001DD050 File Offset: 0x001DB250
		// (set) Token: 0x060082D6 RID: 33494 RVA: 0x001DD058 File Offset: 0x001DB258
		internal bool IsInitFilterCommandInProgress { get; set; }

		// Token: 0x060082D7 RID: 33495 RVA: 0x001DD064 File Offset: 0x001DB264
		internal object GetUnboxedValue(string value)
		{
			string assemblyNameForFieldGroups = this.GetAssemblyNameForFieldGroups();
			string uniqueKeyValueByText = this.GetUniqueKeyValueByText(value);
			object result;
			double num;
			DateTime dateTime;
			if (assemblyNameForFieldGroups.Contains("Telerik.Web.UI.PivotGrid.Core.Groups") && uniqueKeyValueByText != null)
			{
				JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
				Type type = Type.GetType(assemblyNameForFieldGroups);
				object obj = javaScriptSerializer.Deserialize(uniqueKeyValueByText, type);
				result = obj;
			}
			else if (double.TryParse(value, out num))
			{
				result = num;
			}
			else if (DateTime.TryParse(value, out dateTime))
			{
				result = dateTime;
			}
			else
			{
				result = value;
			}
			return result;
		}

		// Token: 0x060082D8 RID: 33496 RVA: 0x001DD0DC File Offset: 0x001DB2DC
		private string GetAssemblyNameForFieldGroups()
		{
			int count = this.OwnerPivotGrid.FilterWindow.SetBox.Items.Count;
			string result = string.Empty;
			if (count > 1)
			{
				result = this.OwnerPivotGrid.FilterWindow.SetBox.Items[1].Attributes["AQN"];
			}
			return result;
		}

		// Token: 0x060082D9 RID: 33497 RVA: 0x001DD13C File Offset: 0x001DB33C
		private string GetUniqueKeyValueByText(string text)
		{
			string result = null;
			RadListBoxItem radListBoxItem = this.SetBox.FindItemByText(text);
			if (radListBoxItem != null)
			{
				result = radListBoxItem.Value;
			}
			return result;
		}

		// Token: 0x060082DA RID: 33498 RVA: 0x001DD164 File Offset: 0x001DB364
		internal static PivotGridFilterFunction MapToFilterFunction(IFilterCondition condition)
		{
			PivotGridFilterFunction result;
			bool flag = PivotGridFilteringManager.MapToComparisonFilterFunction(condition as IPivotComparisonCondition, out result);
			if (!flag)
			{
				flag = PivotGridFilteringManager.MapToTextComparisonFilterFunction(condition as IPivotTextCondition, out result);
			}
			if (!flag)
			{
				PivotGridFilteringManager.MapToIntervalFilterFunction(condition as IPivotIntervalCondition, out result);
			}
			return result;
		}

		// Token: 0x060082DB RID: 33499 RVA: 0x001DD1A4 File Offset: 0x001DB3A4
		internal static bool MapToComparisonFilterFunction(IPivotComparisonCondition cond, out PivotGridFilterFunction filterFunction)
		{
			bool result = false;
			filterFunction = PivotGridFilterFunction.Contains;
			if (cond != null)
			{
				switch (cond.Condition)
				{
				case Comparison.Equals:
					filterFunction = PivotGridFilterFunction.Equals;
					result = true;
					break;
				case Comparison.DoesNotEqual:
					filterFunction = PivotGridFilterFunction.DoesNotEqual;
					result = true;
					break;
				case Comparison.IsGreaterThan:
					filterFunction = PivotGridFilterFunction.IsGreaterThan;
					result = true;
					break;
				case Comparison.IsGreaterThanOrEqualTo:
					filterFunction = PivotGridFilterFunction.IsGreaterThanOrEqualTo;
					result = true;
					break;
				case Comparison.IsLessThan:
					filterFunction = PivotGridFilterFunction.IsLessThan;
					result = true;
					break;
				case Comparison.IsLessThanOrEqualTo:
					filterFunction = PivotGridFilterFunction.IsLessThanOrEqualTo;
					result = true;
					break;
				}
			}
			return result;
		}

		// Token: 0x060082DC RID: 33500 RVA: 0x001DD210 File Offset: 0x001DB410
		internal static bool MapToTextComparisonFilterFunction(IPivotTextCondition cond, out PivotGridFilterFunction filterFunction)
		{
			bool result = false;
			filterFunction = PivotGridFilterFunction.Contains;
			if (cond != null)
			{
				switch (cond.Comparison)
				{
				case TextComparison.BeginsWith:
					filterFunction = PivotGridFilterFunction.BeginsWith;
					result = true;
					break;
				case TextComparison.DoesNotBeginWith:
					filterFunction = PivotGridFilterFunction.DoesNotBeginWith;
					result = true;
					break;
				case TextComparison.EndsWith:
					filterFunction = PivotGridFilterFunction.EndsWith;
					result = true;
					break;
				case TextComparison.DoesNotEndWith:
					filterFunction = PivotGridFilterFunction.DoesNotEndWith;
					result = true;
					break;
				case TextComparison.Contains:
					filterFunction = PivotGridFilterFunction.Contains;
					result = true;
					break;
				case TextComparison.DoesNotContain:
					filterFunction = PivotGridFilterFunction.DoesNotContain;
					result = true;
					break;
				}
			}
			return result;
		}

		// Token: 0x060082DD RID: 33501 RVA: 0x001DD278 File Offset: 0x001DB478
		internal static bool MapToIntervalFilterFunction(IPivotIntervalCondition cond, out PivotGridFilterFunction filterFunction)
		{
			bool result = false;
			filterFunction = PivotGridFilterFunction.Contains;
			if (cond != null)
			{
				switch (cond.Condition)
				{
				case IntervalComparison.IsBetween:
					filterFunction = PivotGridFilterFunction.IsBetween;
					result = true;
					break;
				case IntervalComparison.IsNotBetween:
					filterFunction = PivotGridFilterFunction.IsNotBetween;
					result = true;
					break;
				}
			}
			return result;
		}

		// Token: 0x060082DE RID: 33502 RVA: 0x001DD2B4 File Offset: 0x001DB4B4
		internal RadMenuItem FindMenuItemByFilterFunciton(string filtersGroups, PivotGridFilterFunction filterFunction)
		{
			RadMenuItem radMenuItem = this.FilterWindow.FilterMenu.FindItemByValue(filtersGroups);
			int num = (int)filterFunction;
			string value = num.ToString();
			return radMenuItem.Items.FindItemByValue(value);
		}

		// Token: 0x060082DF RID: 33503 RVA: 0x001DD2E9 File Offset: 0x001DB4E9
		internal void SetUpFilterWindowControls()
		{
			this.CreateFilterWindow();
			this.CreateFilterDialog();
			if (this.OwnerPivotGrid.FilteringManager.IsInitFilterCommandInProgress)
			{
				this.ToggleClearFilterItem();
				this.IntializeFilterExpressionIntoControls();
			}
		}

		// Token: 0x060082E0 RID: 33504 RVA: 0x001DD315 File Offset: 0x001DB515
		protected void CreateFilterWindow()
		{
			if (this.OwnerPivotGrid.ShouldCreateFilterWindow)
			{
				this.OwnerPivotGrid.Controls.Add(this.FilterWindow);
				if (this.OwnerPivotGrid.IsDataBinding)
				{
					this.FilterWindow.InitializeControls();
				}
			}
		}

		// Token: 0x060082E1 RID: 33505 RVA: 0x001DD352 File Offset: 0x001DB552
		protected void CreateFilterDialog()
		{
			if (this.OwnerPivotGrid.ShouldCreateFilterDialog)
			{
				this.OwnerPivotGrid.Controls.Add(this.FilterDialog);
				if (this.OwnerPivotGrid.IsDataBinding)
				{
					this.FilterDialog.InitializeControls();
				}
			}
		}

		// Token: 0x060082E2 RID: 33506 RVA: 0x001DD3A4 File Offset: 0x001DB5A4
		internal void IntializeFilterExpressionIntoControls()
		{
			PivotGridFilter pivotGridFilter = (from expr in this.OwnerPivotGrid.Filters
			where expr.FieldName == this.FieldUniqueName
			select expr).FirstOrDefault<PivotGridFilter>();
			if (pivotGridFilter != null)
			{
				if (!this.InitializeSingleGroupFilterValues(pivotGridFilter as IPivotConditionFilter))
				{
					this.InitializeSortedGroupsFilterValues(pivotGridFilter as PivotGridSortedGroupsFilter);
					return;
				}
				if (!this.InitializeLabelFiltersMenuItem(pivotGridFilter as IPivotLabelGroupFilter))
				{
					IPivotValueGroupFilter valueGroupFilter = pivotGridFilter as IPivotValueGroupFilter;
					this.InitializeValueFiltersMenuItem(valueGroupFilter);
					this.InitializeValueFilterAggregate(valueGroupFilter);
					return;
				}
			}
			else
			{
				this.ClearFilterControls();
			}
		}

		// Token: 0x060082E3 RID: 33507 RVA: 0x001DD420 File Offset: 0x001DB620
		internal bool InitializeLabelFiltersMenuItem(IPivotLabelGroupFilter labelGroupFilter)
		{
			bool result = false;
			if (labelGroupFilter != null)
			{
				PivotGridFilterFunction filterFunction = PivotGridFilteringManager.MapToFilterFunction(labelGroupFilter.Condition);
				RadMenuItem radMenuItem = this.FindMenuItemByFilterFunciton("LabelFilters", filterFunction);
				if (radMenuItem != null)
				{
					radMenuItem.Selected = true;
				}
				result = true;
			}
			return result;
		}

		// Token: 0x060082E4 RID: 33508 RVA: 0x001DD458 File Offset: 0x001DB658
		internal bool InitializeValueFiltersMenuItem(IPivotValueGroupFilter valueGroupFilter)
		{
			bool result = false;
			if (valueGroupFilter != null)
			{
				PivotGridFilterFunction filterFunction = PivotGridFilteringManager.MapToFilterFunction(valueGroupFilter.Condition);
				RadMenuItem radMenuItem = this.FindMenuItemByFilterFunciton("ValueFilters", filterFunction);
				if (radMenuItem != null)
				{
					radMenuItem.Selected = true;
				}
				result = true;
			}
			return result;
		}

		// Token: 0x060082E5 RID: 33509 RVA: 0x001DD490 File Offset: 0x001DB690
		internal void InitializeValueFilterAggregate(IPivotValueGroupFilter valueGroupFilter)
		{
			if (valueGroupFilter != null)
			{
				this.FilterDialog.AggregatesCombo.SelectedIndex = valueGroupFilter.AggregateIndex;
			}
		}

		// Token: 0x060082E6 RID: 33510 RVA: 0x001DD4AB File Offset: 0x001DB6AB
		private bool IntializeFilterConditionValues<T>(T condition, PivotGridFilteringManager.FilterValuesInitializer<T> filterValueInitializer)
		{
			return filterValueInitializer(condition);
		}

		// Token: 0x060082E7 RID: 33511 RVA: 0x001DD65C File Offset: 0x001DB85C
		private bool InitializeSingleGroupFilterValues(IPivotConditionFilter singleGroupFilter)
		{
			bool flag = false;
			if (singleGroupFilter != null)
			{
				IFilterCondition condition2 = singleGroupFilter.Condition;
				flag = this.IntializeFilterConditionValues<IPivotSetCondition>(condition2 as IPivotSetCondition, delegate(IPivotSetCondition setCondition)
				{
					bool result = false;
					if (setCondition != null)
					{
						JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
						this.FilterWindow.SetBox.ClearChecked();
						this.ClearFilterControls();
						foreach (object obj in setCondition.Items)
						{
							RadListBoxItem radListBoxItem = this.FilterWindow.SetBox.FindItemByValue(javaScriptSerializer.Serialize(obj));
							if (radListBoxItem != null)
							{
								radListBoxItem.Checked = true;
							}
						}
						if (setCondition.Comparison == SetComparison.Includes)
						{
							this.FilterWindow.SetOptions.SelectedIndex = 0;
						}
						else
						{
							this.FilterWindow.SetOptions.SelectedIndex = 1;
						}
						result = true;
					}
					return result;
				});
				if (!flag)
				{
					flag = this.IntializeFilterConditionValues<IPivotComparisonCondition>(condition2 as IPivotComparisonCondition, delegate(IPivotComparisonCondition condition)
					{
						bool result = false;
						if (condition != null)
						{
							this.FilterDialog.FilterValue1Box.Text = condition.Than.ToString();
							this.FilterDialog.IgnoreCaseCheckBox.Checked = condition.IgnoreCase;
							result = true;
						}
						return result;
					});
				}
				if (!flag)
				{
					flag = this.IntializeFilterConditionValues<IPivotTextCondition>(condition2 as IPivotTextCondition, delegate(IPivotTextCondition condition)
					{
						if (condition != null)
						{
							this.FilterDialog.FilterValue1Box.Text = condition.Pattern;
							this.FilterDialog.IgnoreCaseCheckBox.Checked = condition.IgnoreCase;
							return true;
						}
						return false;
					});
				}
				if (!flag)
				{
					flag = this.IntializeFilterConditionValues<IPivotIntervalCondition>(condition2 as IPivotIntervalCondition, delegate(IPivotIntervalCondition condition)
					{
						if (condition != null)
						{
							this.FilterDialog.FilterValue1Box.Text = condition.From.ToString();
							this.FilterDialog.FilterValue2Box.Text = condition.To.ToString();
							this.FilterDialog.IgnoreCaseCheckBox.Checked = condition.IgnoreCase;
							return true;
						}
						return false;
					});
				}
			}
			return flag;
		}

		// Token: 0x060082E8 RID: 33512 RVA: 0x001DD70C File Offset: 0x001DB90C
		private bool InitializeSortedGroupsFilterValues(PivotGridSortedGroupsFilter sortedGroupsFilter)
		{
			bool result = false;
			if (sortedGroupsFilter != null)
			{
				if (sortedGroupsFilter.Selection == SortedListSelection.Top)
				{
					this.FilterDialog.SortedListSelectionCombo.SelectedIndex = 0;
				}
				else
				{
					this.FilterDialog.SortedListSelectionCombo.SelectedIndex = 1;
				}
				this.FilterDialog.AggregatesCombo.SelectedIndex = sortedGroupsFilter.AggregateIndex;
				PivotGridGroupsSumFilter pivotGridGroupsSumFilter = sortedGroupsFilter as PivotGridGroupsSumFilter;
				if (pivotGridGroupsSumFilter != null)
				{
					this.FilterDialog.SortedListAggregateOperatorCombo.SelectedIndex = 2;
					this.FilterDialog.SortedListFilterValueBox.Value = new double?(pivotGridGroupsSumFilter.Sum);
				}
				else
				{
					PivotGridGroupsCountFilter pivotGridGroupsCountFilter = sortedGroupsFilter as PivotGridGroupsCountFilter;
					if (pivotGridGroupsCountFilter != null)
					{
						this.FilterDialog.SortedListAggregateOperatorCombo.SelectedIndex = 0;
						this.FilterDialog.SortedListFilterValueBox.Value = new double?((double)pivotGridGroupsCountFilter.Count);
					}
					else
					{
						PivotGridGroupsPercentFilter pivotGridGroupsPercentFilter = sortedGroupsFilter as PivotGridGroupsPercentFilter;
						if (pivotGridGroupsPercentFilter != null)
						{
							this.FilterDialog.SortedListAggregateOperatorCombo.SelectedIndex = 1;
							this.FilterDialog.SortedListFilterValueBox.Value = new double?(pivotGridGroupsPercentFilter.Percent * 100.0);
						}
					}
				}
			}
			return result;
		}

		// Token: 0x060082E9 RID: 33513 RVA: 0x001DD87C File Offset: 0x001DBA7C
		internal void ToggleClearFilterItem()
		{
			string fieldName = this.FieldUniqueName;
			int num = this.OwnerPivotGrid.Filters.Count((PivotGridFilter f) => f.FieldName == fieldName && (f is PivotGridValueGroupFilter || f is PivotGridSortedGroupsFilter || f is PivotGridOlapValueGroupFilter));
			int num2 = this.OwnerPivotGrid.Filters.Count((PivotGridFilter f) => f.FieldName == fieldName && (f is PivotGridLabelGroupFilter || f is PivotGridOlapLabelGroupFilter));
			RadMenuItem radMenuItem = this.FilterWindow.FilterMenu.FindItemByValue("ValueFilters").Items.FindItemByValue("0");
			RadMenuItem radMenuItem2 = this.FilterWindow.FilterMenu.FindItemByValue("LabelFilters").Items.FindItemByValue("0");
			RadMenuItem radMenuItem3 = this.FilterWindow.FilterMenu.Items.FindItemByValue("ClearFiltersFrom");
			radMenuItem.Enabled = (num > 0);
			radMenuItem2.Enabled = (num2 > 0);
			radMenuItem3.Enabled = (num2 > 0 || num > 0);
			radMenuItem3.Text = this.FilterWindow.GetFilterLocalizedValue("ClearFilterFrom") + " " + fieldName;
		}

		// Token: 0x060082EA RID: 33514 RVA: 0x001DD990 File Offset: 0x001DBB90
		internal void ClearFilterControls()
		{
			this.FilterWindow.FilterMenu.ClearSelectedItem();
			this.FilterDialog.SortedListFilterValueBox.Value = null;
			this.FilterDialog.SortedListAggregateOperatorCombo.SelectedIndex = 0;
			this.FilterDialog.SortedListSelectionCombo.SelectedIndex = 0;
			this.FilterDialog.AggregatesCombo.SelectedIndex = 0;
			this.FilterDialog.FilterValue1Box.Text = string.Empty;
			this.FilterDialog.FilterValue2Box.Text = string.Empty;
			this.FilterDialog.IgnoreCaseCheckBox.Checked = false;
		}

		// Token: 0x060082EB RID: 33515 RVA: 0x001DDA34 File Offset: 0x001DBC34
		internal FilterDescription GetFilteDescriptionOnField()
		{
			FilterDescription result;
			if (this.OwnerPivotGrid.IsBoundToAdomd)
			{
				result = new AdomdFilterDescription();
			}
			else if (this.OwnerPivotGrid.IsBoundToXmla)
			{
				result = new XmlaFilterDescription();
			}
			else
			{
				result = new PropertyFilterDescription();
			}
			return result;
		}

		// Token: 0x04002425 RID: 9253
		private const string GroupsNameSpace = "Telerik.Web.UI.PivotGrid.Core.Groups";

		// Token: 0x04002426 RID: 9254
		private const string MonthGroupClassName = "Telerik.Web.UI.PivotGrid.Core.Groups.MonthGroup";

		// Token: 0x04002427 RID: 9255
		private const string DayGroupClassName = "Telerik.Web.UI.PivotGrid.Core.Groups.DayGroup";

		// Token: 0x04002428 RID: 9256
		private const string YearGroupClassName = "Telerik.Web.UI.PivotGrid.Core.Groups.YearGroup";

		// Token: 0x04002429 RID: 9257
		private const string QuarterGroupClassName = "Telerik.Web.UI.PivotGrid.Core.Groups.QuarterGroup";

		// Token: 0x0400242A RID: 9258
		private RadPivotGrid ownerPivotGrid;

		// Token: 0x02000DB2 RID: 3506
		// (Invoke) Token: 0x060082F2 RID: 33522
		private delegate bool FilterValuesInitializer<T>(T condition);
	}
}
