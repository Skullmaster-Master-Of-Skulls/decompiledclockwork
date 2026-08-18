using System;
using System.Collections.Generic;
using System.Web.UI;
using Telerik.Web.UI.PersistenceFramework;

namespace Telerik.Web.UI
{
	// Token: 0x02000195 RID: 405
	internal class RadButtonStatePersister : RadStatePersister
	{
		// Token: 0x06000DCA RID: 3530 RVA: 0x00034318 File Offset: 0x00032518
		public override void ReadSettings(Control control)
		{
			List<ControlSetting> list = new List<ControlSetting>();
			RadControlState currentState = null;
			RadButton radButton = control as RadButton;
			if (radButton != null)
			{
				list.Add(new ControlSetting
				{
					Name = "Checked",
					Value = radButton.Checked
				});
				list.Add(new ControlSetting
				{
					Name = "SelectedToggleStateIndex",
					Value = radButton.SelectedToggleStateIndex
				});
				currentState = new RadControlState(list, radButton.UniqueID);
			}
			this.currentState = currentState;
		}

		// Token: 0x06000DCB RID: 3531 RVA: 0x000343A8 File Offset: 0x000325A8
		public override void ApplySettings(Control control)
		{
			RadButton radButton = control as RadButton;
			if (radButton != null)
			{
				foreach (ControlSetting controlSetting in this.currentState.ControlSettings)
				{
					string a;
					if ((a = controlSetting.Name.ToString()) != null)
					{
						if (!(a == "Checked"))
						{
							if (a == "SelectedToggleStateIndex")
							{
								radButton.SelectedToggleStateIndex = (int)controlSetting.Value;
							}
						}
						else
						{
							radButton.Checked = (bool)controlSetting.Value;
						}
					}
				}
			}
		}
	}
}
