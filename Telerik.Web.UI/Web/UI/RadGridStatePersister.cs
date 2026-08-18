using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI.PersistenceFramework;

namespace Telerik.Web.UI
{
	// Token: 0x020004CA RID: 1226
	internal class RadGridStatePersister : RadStatePersister
	{
		// Token: 0x06002C76 RID: 11382 RVA: 0x00091D98 File Offset: 0x0008FF98
		public override void ReadSettings(Control control)
		{
			List<ControlSetting> list = new List<ControlSetting>();
			RadControlState currentState = null;
			RadGrid radGrid = control as RadGrid;
			if (radGrid != null)
			{
				list.Add(new ControlSetting
				{
					Name = "CurrentPageIndex",
					Value = radGrid.CurrentPageIndex
				});
				list.Add(new ControlSetting
				{
					Name = "EditIndexes",
					Value = radGrid.EditIndexes
				});
				list.Add(new ControlSetting
				{
					Name = "PageSize",
					Value = radGrid.PageSize
				});
				list.Add(new ControlSetting
				{
					Name = "SelectedCellIndexes",
					Value = radGrid.SelectedCellIndexes
				});
				list.Add(new ControlSetting
				{
					Name = "SelectedIndexes",
					Value = radGrid.SelectedIndexes
				});
				list.Add(new ControlSetting
				{
					Name = "Width",
					Value = radGrid.Width
				});
				list.Add(new ControlSetting
				{
					Name = "MasterTableView.Width",
					Value = radGrid.MasterTableView.Width
				});
				list.Add(new ControlSetting
				{
					Name = "MasterTableView.FilterExpression",
					Value = radGrid.MasterTableView.FilterExpression
				});
				list.Add(new ControlSetting
				{
					Name = "MasterTableView.PageSize",
					Value = radGrid.MasterTableView.PageSize
				});
				list.Add(new ControlSetting
				{
					Name = "MasterTableView.ColumnSettings",
					Value = radGrid.MasterTableView.ColumnSettings
				});
				list.Add(new ControlSetting
				{
					Name = "MasterTableView.SortExpressions",
					Value = radGrid.MasterTableView.SortExpressions
				});
				list.Add(new ControlSetting
				{
					Name = "MasterTableView.GroupByExpressions",
					Value = radGrid.MasterTableView.GroupByExpressions
				});
				list.Add(new ControlSetting
				{
					Name = "MasterTableView.CurrentPageIndex",
					Value = radGrid.MasterTableView.CurrentPageIndex
				});
				list.Add(new ControlSetting
				{
					Name = "MasterTableView.IsItemInserted",
					Value = radGrid.MasterTableView.IsItemInserted
				});
				currentState = new RadControlState(list, radGrid.UniqueID);
			}
			this.currentState = currentState;
		}

		// Token: 0x06002C77 RID: 11383 RVA: 0x00092058 File Offset: 0x00090258
		public override void ApplySettings(Control control)
		{
			RadGrid radGrid = control as RadGrid;
			if (radGrid != null)
			{
				foreach (ControlSetting controlSetting in this.currentState.ControlSettings)
				{
					string key;
					switch (key = controlSetting.Name.ToString())
					{
					case "CurrentPageIndex":
						radGrid.CurrentPageIndex = (int)controlSetting.Value;
						break;
					case "EditIndexes":
						radGrid.EditIndexes = (GridIndexCollection)controlSetting.Value;
						break;
					case "PageSize":
						radGrid.PageSize = (int)controlSetting.Value;
						break;
					case "SelectedCellIndexes":
						radGrid.SelectedCellIndexes = (GridIndexCollection)controlSetting.Value;
						break;
					case "SelectedIndexes":
						radGrid.SelectedIndexes = (GridIndexCollection)controlSetting.Value;
						break;
					case "Width":
						radGrid.Width = (Unit)controlSetting.Value;
						break;
					case "MasterTableView.Width":
						radGrid.MasterTableView.Width = (Unit)controlSetting.Value;
						break;
					case "MasterTableView.FilterExpression":
						radGrid.MasterTableView.FilterExpression = (string)controlSetting.Value;
						break;
					case "MasterTableView.PageSize":
						radGrid.MasterTableView.PageSize = (int)controlSetting.Value;
						break;
					case "MasterTableView.ColumnSettings":
						radGrid.MasterTableView.ColumnSettings = (List<GridTableView.PersistableColumnSetting>)controlSetting.Value;
						break;
					case "MasterTableView.SortExpressions":
						radGrid.MasterTableView.SortExpressions = (GridSortExpressionCollection)controlSetting.Value;
						break;
					case "MasterTableView.GroupByExpressions":
						radGrid.MasterTableView.GroupByExpressions = (GridGroupByExpressionCollection)controlSetting.Value;
						break;
					case "MasterTableView.CurrentPageIndex":
						radGrid.MasterTableView.CurrentPageIndex = (int)controlSetting.Value;
						break;
					case "MasterTableView.IsItemInserted":
						radGrid.MasterTableView.IsItemInserted = (bool)controlSetting.Value;
						break;
					}
				}
			}
		}
	}
}
