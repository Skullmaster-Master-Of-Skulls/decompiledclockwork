using System;
using System.Collections.Generic;
using System.Web.UI;
using Telerik.Web.UI.PersistenceFramework;

namespace Telerik.Web.UI
{
	// Token: 0x0200087A RID: 2170
	internal class RadSkinManagerStatePersister : RadStatePersister
	{
		// Token: 0x0600505D RID: 20573 RVA: 0x000FB280 File Offset: 0x000F9480
		public override void ReadSettings(Control control)
		{
			List<ControlSetting> list = new List<ControlSetting>();
			RadControlState currentState = null;
			RadSkinManager radSkinManager = control as RadSkinManager;
			if (radSkinManager != null)
			{
				list.Add(new ControlSetting
				{
					Name = "Skin",
					Value = radSkinManager.Skin
				});
				currentState = new RadControlState(list, radSkinManager.UniqueID);
			}
			this.currentState = currentState;
		}

		// Token: 0x0600505E RID: 20574 RVA: 0x000FB2DC File Offset: 0x000F94DC
		public override void ApplySettings(Control control)
		{
			RadSkinManager radSkinManager = control as RadSkinManager;
			if (radSkinManager != null)
			{
				foreach (ControlSetting controlSetting in this.currentState.ControlSettings)
				{
					string a;
					if ((a = controlSetting.Name.ToString()) != null && a == "Skin")
					{
						radSkinManager.Skin = (string)controlSetting.Value;
					}
				}
			}
		}
	}
}
