using System;
using System.Collections.Generic;
using System.Web.UI;
using Telerik.Web.UI.PersistenceFramework;

namespace Telerik.Web.UI
{
	// Token: 0x0200064D RID: 1613
	internal class RadPanelBarStatePersister : RadStatePersister
	{
		// Token: 0x06003B50 RID: 15184 RVA: 0x000C0A44 File Offset: 0x000BEC44
		public override void ReadSettings(Control control)
		{
			List<ControlSetting> list = new List<ControlSetting>();
			RadControlState currentState = null;
			RadPanelBar radPanelBar = control as RadPanelBar;
			if (radPanelBar != null)
			{
				list.Add(new ControlSetting
				{
					Name = "ExpandedIndices",
					Value = radPanelBar.ExpandedIndices
				});
				list.Add(new ControlSetting
				{
					Name = "SelectedIndex",
					Value = radPanelBar.SelectedIndex
				});
				currentState = new RadControlState(list, radPanelBar.UniqueID);
			}
			this.currentState = currentState;
		}

		// Token: 0x06003B51 RID: 15185 RVA: 0x000C0AC8 File Offset: 0x000BECC8
		public override void ApplySettings(Control control)
		{
			RadPanelBar radPanelBar = control as RadPanelBar;
			if (radPanelBar != null)
			{
				foreach (ControlSetting controlSetting in this.currentState.ControlSettings)
				{
					string a;
					if ((a = controlSetting.Name.ToString()) != null)
					{
						if (!(a == "ExpandedIndices"))
						{
							if (a == "SelectedIndex")
							{
								radPanelBar.SelectedIndex = (string)controlSetting.Value;
							}
						}
						else
						{
							radPanelBar.ExpandedIndices = (List<string>)controlSetting.Value;
						}
					}
				}
			}
		}
	}
}
