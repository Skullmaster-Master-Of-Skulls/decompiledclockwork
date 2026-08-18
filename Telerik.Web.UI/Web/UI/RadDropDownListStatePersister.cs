using System;
using System.Collections.Generic;
using System.Web.UI;
using Telerik.Web.UI.PersistenceFramework;

namespace Telerik.Web.UI
{
	// Token: 0x0200045A RID: 1114
	internal class RadDropDownListStatePersister : RadStatePersister
	{
		// Token: 0x06002852 RID: 10322 RVA: 0x00082E64 File Offset: 0x00081064
		public override void ReadSettings(Control control)
		{
			List<ControlSetting> list = new List<ControlSetting>();
			RadControlState currentState = null;
			RadDropDownList radDropDownList = control as RadDropDownList;
			if (radDropDownList != null)
			{
				list.Add(new ControlSetting
				{
					Name = "SelectedIndex",
					Value = radDropDownList.SelectedIndex
				});
				currentState = new RadControlState(list, radDropDownList.UniqueID);
			}
			this.currentState = currentState;
		}

		// Token: 0x06002853 RID: 10323 RVA: 0x00082EC4 File Offset: 0x000810C4
		public override void ApplySettings(Control control)
		{
			RadDropDownList radDropDownList = control as RadDropDownList;
			if (radDropDownList != null)
			{
				foreach (ControlSetting controlSetting in this.currentState.ControlSettings)
				{
					string a;
					if ((a = controlSetting.Name.ToString()) != null && a == "SelectedIndex")
					{
						radDropDownList.SelectedIndex = (int)controlSetting.Value;
					}
				}
			}
		}
	}
}
