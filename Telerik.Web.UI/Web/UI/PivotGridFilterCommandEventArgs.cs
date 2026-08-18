using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using System.Web.UI;
using Telerik.Web.UI.PivotGrid.Core;
using Telerik.Web.UI.PivotGrid.Core.Filtering;

namespace Telerik.Web.UI
{
	// Token: 0x02000C38 RID: 3128
	public class PivotGridFilterCommandEventArgs : PivotGridCommandEventArgs
	{
		// Token: 0x06007672 RID: 30322 RVA: 0x001B7F23 File Offset: 0x001B6123
		public PivotGridFilterCommandEventArgs(PivotGridItem item, object commandSource, object argument) : base(item, commandSource, "Filter", argument)
		{
		}

		// Token: 0x17002681 RID: 9857
		// (get) Token: 0x06007673 RID: 30323 RVA: 0x001B7F33 File Offset: 0x001B6133
		public RadPivotGrid OwnerPivotGrid
		{
			get
			{
				return this.Item.OwnerPivotGrid;
			}
		}

		// Token: 0x17002682 RID: 9858
		// (get) Token: 0x06007674 RID: 30324 RVA: 0x001B7F40 File Offset: 0x001B6140
		public string FieldName
		{
			get
			{
				return this.fieldName;
			}
		}

		// Token: 0x17002683 RID: 9859
		// (get) Token: 0x06007675 RID: 30325 RVA: 0x001B7F48 File Offset: 0x001B6148
		// (set) Token: 0x06007676 RID: 30326 RVA: 0x001B7F50 File Offset: 0x001B6150
		public object[] FilterValues { get; set; }

		// Token: 0x06007677 RID: 30327 RVA: 0x001B7F6C File Offset: 0x001B616C
		public override void ExecuteCommand(object source)
		{
			this.fieldName = (base.CommandArgument as Pair).Second.ToString();
			this.OwnerPivotGrid.FireFilterCommand(this);
			if (this.Canceled)
			{
				return;
			}
			PivotGridFilterCommandType pivotGridFilterCommandType = (PivotGridFilterCommandType)(base.CommandArgument as Pair).First;
			this.fieldName = (base.CommandArgument as Pair).Second.ToString();
			PivotGridField pivotGridField = this.OwnerPivotGrid.Fields[this.fieldName];
			this.OwnerPivotGrid.Filters.RemoveAll((PivotGridFilter f) => f.FieldName == this.fieldName);
			if (pivotGridFilterCommandType == PivotGridFilterCommandType.Set)
			{
				SetComparison setComparison;
				if (this.OwnerPivotGrid.FilterWindow.SetOptions.SelectedIndex == 0)
				{
					setComparison = SetComparison.Includes;
				}
				else
				{
					setComparison = SetComparison.DoesNotInclude;
				}
				JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
				List<object> list = new List<object>();
				if (this.OwnerPivotGrid.FilterWindow.SetBox.CheckedItems.Count > 0 && !this.OwnerPivotGrid.FilterWindow.SetBox.Items[0].Checked)
				{
					string typeName = this.OwnerPivotGrid.FilterWindow.SetBox.Items[1].Attributes["AQN"];
					Type type = Type.GetType(typeName);
					foreach (RadListBoxItem radListBoxItem in this.OwnerPivotGrid.FilterWindow.SetBox.CheckedItems)
					{
						object obj;
						if (type.FullName == "Telerik.Web.UI.PivotGrid.Core.NullValue")
						{
							obj = NullValue.Instance;
						}
						else
						{
							obj = javaScriptSerializer.Deserialize(radListBoxItem.Value, type);
						}
						if (obj is DateTime)
						{
							obj = ((DateTime)obj).ToLocalTime();
						}
						list.Add(obj);
					}
					this.OwnerPivotGrid.SetFilterIncludesOrExcludes(pivotGridField.UniqueName, list, setComparison, true);
				}
			}
			else
			{
				int num = int.Parse(this.OwnerPivotGrid.FilterDialog.AggregateOperatorsCombo.SelectedValue);
				PivotGridFilterFunction filterFunction = (PivotGridFilterFunction)num;
				string text = this.OwnerPivotGrid.FilterDialog.FilterValue1Box.Text;
				string text2 = this.OwnerPivotGrid.FilterDialog.FilterValue2Box.Text;
				PivotGridAggregateField aggregateField = this.OwnerPivotGrid.Fields[this.OwnerPivotGrid.FilterDialog.AggregatesCombo.SelectedValue] as PivotGridAggregateField;
				bool @checked = this.OwnerPivotGrid.FilterDialog.IgnoreCaseCheckBox.Checked;
				if (pivotGridFilterCommandType == PivotGridFilterCommandType.Label)
				{
					this.OwnerPivotGrid.FilterByLabel(filterFunction, pivotGridField, text, text2, true, @checked);
				}
				else if (pivotGridFilterCommandType == PivotGridFilterCommandType.Value)
				{
					this.OwnerPivotGrid.FilterByValue(filterFunction, pivotGridField, aggregateField, text, text2, true, @checked);
				}
				else if (pivotGridFilterCommandType == PivotGridFilterCommandType.Top)
				{
					num = int.Parse(this.OwnerPivotGrid.FilterDialog.SortedListSelectionCombo.SelectedValue);
					filterFunction = (PivotGridFilterFunction)num;
					string selectedValue = this.OwnerPivotGrid.FilterDialog.SortedListAggregateOperatorCombo.SelectedValue;
					PivotGridAggregateType aggregateType = (PivotGridAggregateType)Enum.Parse(typeof(PivotGridAggregateType), selectedValue);
					double value = (this.OwnerPivotGrid.FilterDialog.SortedListFilterValueBox.Value != null) ? this.OwnerPivotGrid.FilterDialog.SortedListFilterValueBox.Value.Value : 0.0;
					this.OwnerPivotGrid.FilterByTopOrBottom(filterFunction, pivotGridField, aggregateField, aggregateType, value, true);
				}
			}
			this.OwnerPivotGrid.IsFilterCommandInProgress = true;
			this.OwnerPivotGrid.ResetPivotModel();
			this.OwnerPivotGrid.ObtainDataSource(PivotGridRebindReason.PostBackEvent);
			this.OwnerPivotGrid.DataBind();
		}

		// Token: 0x0400208C RID: 8332
		private string fieldName;
	}
}
