using System;
using System.Collections.Generic;
using System.Web.UI;
using Telerik.Web.UI.PersistenceFramework;

namespace Telerik.Web.UI
{
	// Token: 0x0200096E RID: 2414
	internal class RadTreeListStatePersister : RadStatePersister
	{
		// Token: 0x06005BCB RID: 23499 RVA: 0x00117EA8 File Offset: 0x001160A8
		public override void ReadSettings(Control control)
		{
			List<ControlSetting> list = new List<ControlSetting>();
			RadControlState currentState = null;
			RadTreeList radTreeList = control as RadTreeList;
			if (radTreeList != null)
			{
				list.Add(new ControlSetting
				{
					Name = "CurrentPageIndex",
					Value = radTreeList.CurrentPageIndex
				});
				list.Add(new ControlSetting
				{
					Name = "EditIndexes",
					Value = radTreeList.EditIndexes
				});
				list.Add(new ControlSetting
				{
					Name = "ExpandedIndexes",
					Value = radTreeList.ExpandedIndexes
				});
				list.Add(new ControlSetting
				{
					Name = "InsertIndexes",
					Value = radTreeList.InsertIndexes
				});
				list.Add(new ControlSetting
				{
					Name = "IsItemInserted",
					Value = radTreeList.IsItemInserted
				});
				list.Add(new ControlSetting
				{
					Name = "PageSize",
					Value = radTreeList.PageSize
				});
				list.Add(new ControlSetting
				{
					Name = "SelectedIndexes",
					Value = radTreeList.SelectedIndexes
				});
				list.Add(new ControlSetting
				{
					Name = "SortExpressions",
					Value = radTreeList.SortExpressions
				});
				currentState = new RadControlState(list, radTreeList.UniqueID);
			}
			this.currentState = currentState;
		}

		// Token: 0x06005BCC RID: 23500 RVA: 0x00118034 File Offset: 0x00116234
		public override void ApplySettings(Control control)
		{
			RadTreeList radTreeList = control as RadTreeList;
			if (radTreeList != null)
			{
				foreach (ControlSetting controlSetting in this.currentState.ControlSettings)
				{
					string key;
					switch (key = controlSetting.Name.ToString())
					{
					case "CurrentPageIndex":
						radTreeList.CurrentPageIndex = (int)controlSetting.Value;
						break;
					case "EditIndexes":
						radTreeList.EditIndexes = (TreeListEditIndexesCollection)controlSetting.Value;
						break;
					case "ExpandedIndexes":
						radTreeList.ExpandedIndexes = (TreeListExpandedIndexesCollection)controlSetting.Value;
						break;
					case "InsertIndexes":
						radTreeList.InsertIndexes = (TreeListEditIndexesCollection)controlSetting.Value;
						break;
					case "IsItemInserted":
						radTreeList.IsItemInserted = (bool)controlSetting.Value;
						break;
					case "PageSize":
						radTreeList.PageSize = (int)controlSetting.Value;
						break;
					case "SelectedIndexes":
						radTreeList.SelectedIndexes = (TreeListSelectedIndexesCollection)controlSetting.Value;
						break;
					case "SortExpressions":
						radTreeList.SortExpressions = (TreeListSortExpressionCollection)controlSetting.Value;
						break;
					}
				}
			}
		}
	}
}
