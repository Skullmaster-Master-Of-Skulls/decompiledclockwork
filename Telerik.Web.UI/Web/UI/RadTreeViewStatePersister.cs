using System;
using System.Collections.Generic;
using System.Web.UI;
using Telerik.Web.UI.PersistenceFramework;

namespace Telerik.Web.UI
{
	// Token: 0x02000979 RID: 2425
	internal class RadTreeViewStatePersister : RadStatePersister
	{
		// Token: 0x06005C37 RID: 23607 RVA: 0x001192C4 File Offset: 0x001174C4
		public override void ReadSettings(Control control)
		{
			List<ControlSetting> list = new List<ControlSetting>();
			RadControlState currentState = null;
			RadTreeView radTreeView = control as RadTreeView;
			if (radTreeView != null)
			{
				list.Add(new ControlSetting
				{
					Name = "CheckedIndices",
					Value = radTreeView.CheckedIndices
				});
				list.Add(new ControlSetting
				{
					Name = "ExpandedIndices",
					Value = radTreeView.ExpandedIndices
				});
				list.Add(new ControlSetting
				{
					Name = "SelectedIndices",
					Value = radTreeView.SelectedIndices
				});
				currentState = new RadControlState(list, radTreeView.UniqueID);
			}
			this.currentState = currentState;
		}

		// Token: 0x06005C38 RID: 23608 RVA: 0x00119374 File Offset: 0x00117574
		public override void ApplySettings(Control control)
		{
			RadTreeView radTreeView = control as RadTreeView;
			if (radTreeView != null)
			{
				foreach (ControlSetting controlSetting in this.currentState.ControlSettings)
				{
					string a;
					if ((a = controlSetting.Name.ToString()) != null)
					{
						if (!(a == "CheckedIndices"))
						{
							if (!(a == "ExpandedIndices"))
							{
								if (a == "SelectedIndices")
								{
									radTreeView.SelectedIndices = (List<string>)controlSetting.Value;
								}
							}
							else
							{
								radTreeView.ExpandedIndices = (List<string>)controlSetting.Value;
							}
						}
						else
						{
							radTreeView.CheckedIndices = (List<string>)controlSetting.Value;
						}
					}
				}
			}
		}
	}
}
