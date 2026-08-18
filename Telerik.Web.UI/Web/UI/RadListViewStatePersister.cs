using System;
using System.Collections.Generic;
using System.Web.UI;
using Telerik.Web.UI.PersistenceFramework;

namespace Telerik.Web.UI
{
	// Token: 0x0200058A RID: 1418
	internal class RadListViewStatePersister : RadStatePersister
	{
		// Token: 0x0600331D RID: 13085 RVA: 0x000AA5C0 File Offset: 0x000A87C0
		public override void ReadSettings(Control control)
		{
			List<ControlSetting> list = new List<ControlSetting>();
			RadControlState currentState = null;
			RadListView radListView = control as RadListView;
			if (radListView != null)
			{
				list.Add(new ControlSetting
				{
					Name = "CurrentPageIndex",
					Value = radListView.CurrentPageIndex
				});
				list.Add(new ControlSetting
				{
					Name = "EditIndexes",
					Value = radListView.EditIndexes
				});
				list.Add(new ControlSetting
				{
					Name = "FilterExpressions",
					Value = radListView.FilterExpressions
				});
				list.Add(new ControlSetting
				{
					Name = "IsItemInserted",
					Value = radListView.IsItemInserted
				});
				list.Add(new ControlSetting
				{
					Name = "PageSize",
					Value = radListView.PageSize
				});
				list.Add(new ControlSetting
				{
					Name = "SelectedIndexes",
					Value = radListView.SelectedIndexes
				});
				list.Add(new ControlSetting
				{
					Name = "SortExpressions",
					Value = radListView.SortExpressions
				});
				currentState = new RadControlState(list, radListView.UniqueID);
			}
			this.currentState = currentState;
		}

		// Token: 0x0600331E RID: 13086 RVA: 0x000AA724 File Offset: 0x000A8924
		public override void ApplySettings(Control control)
		{
			RadListView radListView = control as RadListView;
			if (radListView != null)
			{
				foreach (ControlSetting controlSetting in this.currentState.ControlSettings)
				{
					string key;
					switch (key = controlSetting.Name.ToString())
					{
					case "CurrentPageIndex":
						radListView.CurrentPageIndex = (int)controlSetting.Value;
						break;
					case "EditIndexes":
						radListView.EditIndexes = (RadListViewIndexesCollection)controlSetting.Value;
						break;
					case "FilterExpressions":
						radListView.FilterExpressions = (RadListViewFilterExpressionCollection)controlSetting.Value;
						break;
					case "IsItemInserted":
						radListView.IsItemInserted = (bool)controlSetting.Value;
						break;
					case "PageSize":
						radListView.PageSize = (int)controlSetting.Value;
						break;
					case "SelectedIndexes":
						radListView.SelectedIndexes = (RadListViewIndexesCollection)controlSetting.Value;
						break;
					case "SortExpressions":
						radListView.SortExpressions = (RadListViewSortExpressionCollection)controlSetting.Value;
						break;
					}
				}
			}
		}
	}
}
