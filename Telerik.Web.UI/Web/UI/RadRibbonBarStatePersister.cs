using System;
using System.Collections.Generic;
using System.Web.UI;
using Telerik.Web.UI.PersistenceFramework;

namespace Telerik.Web.UI
{
	// Token: 0x02000786 RID: 1926
	internal class RadRibbonBarStatePersister : RadStatePersister
	{
		// Token: 0x060043CB RID: 17355 RVA: 0x000D4320 File Offset: 0x000D2520
		public override void ReadSettings(Control control)
		{
			List<ControlSetting> list = new List<ControlSetting>();
			RadControlState currentState = null;
			RadRibbonBar radRibbonBar = control as RadRibbonBar;
			if (radRibbonBar != null)
			{
				list.Add(new ControlSetting
				{
					Name = "Minimized",
					Value = radRibbonBar.Minimized
				});
				list.Add(new ControlSetting
				{
					Name = "SelectedTabIndex",
					Value = radRibbonBar.SelectedTabIndex
				});
				currentState = new RadControlState(list, radRibbonBar.UniqueID);
			}
			this.currentState = currentState;
		}

		// Token: 0x060043CC RID: 17356 RVA: 0x000D43B0 File Offset: 0x000D25B0
		public override void ApplySettings(Control control)
		{
			RadRibbonBar radRibbonBar = control as RadRibbonBar;
			if (radRibbonBar != null)
			{
				foreach (ControlSetting controlSetting in this.currentState.ControlSettings)
				{
					string a;
					if ((a = controlSetting.Name.ToString()) != null)
					{
						if (!(a == "Minimized"))
						{
							if (a == "SelectedTabIndex")
							{
								radRibbonBar.SelectedTabIndex = (int)controlSetting.Value;
							}
						}
						else
						{
							radRibbonBar.Minimized = (bool)controlSetting.Value;
						}
					}
				}
			}
		}
	}
}
