using System;
using System.Collections.Generic;
using System.Web.UI;
using Telerik.Web.UI.PersistenceFramework;

namespace Telerik.Web.UI
{
	// Token: 0x020008E8 RID: 2280
	internal class RadTabStripStatePersister : RadStatePersister
	{
		// Token: 0x0600563F RID: 22079 RVA: 0x00107F58 File Offset: 0x00106158
		public override void ReadSettings(Control control)
		{
			List<ControlSetting> list = new List<ControlSetting>();
			RadControlState currentState = null;
			RadTabStrip radTabStrip = control as RadTabStrip;
			if (radTabStrip != null)
			{
				list.Add(new ControlSetting
				{
					Name = "SelectedIndices",
					Value = radTabStrip.SelectedIndices
				});
				currentState = new RadControlState(list, radTabStrip.UniqueID);
			}
			this.currentState = currentState;
		}

		// Token: 0x06005640 RID: 22080 RVA: 0x00107FB4 File Offset: 0x001061B4
		public override void ApplySettings(Control control)
		{
			RadTabStrip radTabStrip = control as RadTabStrip;
			if (radTabStrip != null)
			{
				foreach (ControlSetting controlSetting in this.currentState.ControlSettings)
				{
					string a;
					if ((a = controlSetting.Name.ToString()) != null && a == "SelectedIndices")
					{
						radTabStrip.SelectedIndices = (List<string>)controlSetting.Value;
					}
				}
			}
		}
	}
}
