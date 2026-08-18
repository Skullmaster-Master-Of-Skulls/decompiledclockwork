using System;
using System.Collections.Generic;
using System.Web.UI;
using Telerik.Web.UI.PersistenceFramework;

namespace Telerik.Web.UI
{
	// Token: 0x020005D7 RID: 1495
	internal class RadMenuStatePersister : RadStatePersister
	{
		// Token: 0x06003659 RID: 13913 RVA: 0x000B3998 File Offset: 0x000B1B98
		public override void ReadSettings(Control control)
		{
			List<ControlSetting> list = new List<ControlSetting>();
			RadControlState currentState = null;
			RadMenu radMenu = control as RadMenu;
			if (radMenu != null)
			{
				list.Add(new ControlSetting
				{
					Name = "SelectedIndex",
					Value = radMenu.SelectedIndex
				});
				currentState = new RadControlState(list, radMenu.UniqueID);
			}
			this.currentState = currentState;
		}

		// Token: 0x0600365A RID: 13914 RVA: 0x000B39F4 File Offset: 0x000B1BF4
		public override void ApplySettings(Control control)
		{
			RadMenu radMenu = control as RadMenu;
			if (radMenu != null)
			{
				foreach (ControlSetting controlSetting in this.currentState.ControlSettings)
				{
					string a;
					if ((a = controlSetting.Name.ToString()) != null && a == "SelectedIndex")
					{
						radMenu.SelectedIndex = (string)controlSetting.Value;
					}
				}
			}
		}
	}
}
