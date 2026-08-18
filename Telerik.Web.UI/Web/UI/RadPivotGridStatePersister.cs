using System;
using System.Collections.Generic;
using System.Web.UI;
using Telerik.Web.UI.PersistenceFramework;

namespace Telerik.Web.UI
{
	// Token: 0x02000771 RID: 1905
	internal class RadPivotGridStatePersister : RadStatePersister
	{
		// Token: 0x06004335 RID: 17205 RVA: 0x000D23B8 File Offset: 0x000D05B8
		public override void ReadSettings(Control control)
		{
			List<ControlSetting> list = new List<ControlSetting>();
			RadControlState currentState = null;
			RadPivotGrid radPivotGrid = control as RadPivotGrid;
			if (radPivotGrid != null)
			{
				list.Add(new ControlSetting
				{
					Name = "AggregatesPosition",
					Value = radPivotGrid.AggregatesPosition
				});
				list.Add(new ControlSetting
				{
					Name = "AggregatesLevel",
					Value = radPivotGrid.AggregatesLevel
				});
				list.Add(new ControlSetting
				{
					Name = "PageSize",
					Value = radPivotGrid.PageSize
				});
				list.Add(new ControlSetting
				{
					Name = "CurrentPageIndex",
					Value = radPivotGrid.CurrentPageIndex
				});
				list.Add(new ControlSetting
				{
					Name = "FiltersPersistence",
					Value = radPivotGrid.FiltersPersistence
				});
				list.Add(new ControlSetting
				{
					Name = "FieldSettings",
					Value = radPivotGrid.FieldSettings
				});
				list.Add(new ControlSetting
				{
					Name = "CollapsedRowIndexes",
					Value = radPivotGrid.CollapsedRowIndexes
				});
				list.Add(new ControlSetting
				{
					Name = "CollapsedColumnIndexes",
					Value = radPivotGrid.CollapsedColumnIndexes
				});
				list.Add(new ControlSetting
				{
					Name = "SortExpressions",
					Value = radPivotGrid.SortExpressions
				});
				list.Add(new ControlSetting
				{
					Name = "ConfigurationPanelSettings.LayoutType",
					Value = radPivotGrid.ConfigurationPanelSettings.LayoutType
				});
				currentState = new RadControlState(list, radPivotGrid.UniqueID);
			}
			this.currentState = currentState;
		}

		// Token: 0x06004336 RID: 17206 RVA: 0x000D25A8 File Offset: 0x000D07A8
		public override void ApplySettings(Control control)
		{
			RadPivotGrid radPivotGrid = control as RadPivotGrid;
			if (radPivotGrid != null)
			{
				foreach (ControlSetting controlSetting in this.currentState.ControlSettings)
				{
					string key;
					switch (key = controlSetting.Name.ToString())
					{
					case "AggregatesPosition":
						radPivotGrid.AggregatesPosition = (PivotGridAxis)controlSetting.Value;
						break;
					case "AggregatesLevel":
						radPivotGrid.AggregatesLevel = (int)controlSetting.Value;
						break;
					case "PageSize":
						radPivotGrid.PageSize = (int)controlSetting.Value;
						break;
					case "CurrentPageIndex":
						radPivotGrid.CurrentPageIndex = (int)controlSetting.Value;
						break;
					case "FiltersPersistence":
						radPivotGrid.FiltersPersistence = (string)controlSetting.Value;
						break;
					case "FieldSettings":
						radPivotGrid.FieldSettings = (List<RadPivotGrid.PersistableFieldSetting>)controlSetting.Value;
						break;
					case "CollapsedRowIndexes":
						radPivotGrid.CollapsedRowIndexes = (HashSet<Array>)controlSetting.Value;
						break;
					case "CollapsedColumnIndexes":
						radPivotGrid.CollapsedColumnIndexes = (HashSet<Array>)controlSetting.Value;
						break;
					case "SortExpressions":
						radPivotGrid.SortExpressions = (PivotGridSortExpressionCollection)controlSetting.Value;
						break;
					case "ConfigurationPanelSettings.LayoutType":
						radPivotGrid.ConfigurationPanelSettings.LayoutType = (PivotGridConfigurationPanelLayoutType)controlSetting.Value;
						break;
					}
				}
			}
		}
	}
}
