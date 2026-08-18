using System;
using System.Collections.Generic;
using System.Drawing;
using System.Web.UI;
using Telerik.Web.UI.PersistenceFramework;

namespace Telerik.Web.UI
{
	// Token: 0x020001BD RID: 445
	internal class RadColorPickerStatePersister : RadStatePersister
	{
		// Token: 0x0600106C RID: 4204 RVA: 0x0003C0B4 File Offset: 0x0003A2B4
		public override void ReadSettings(Control control)
		{
			List<ControlSetting> list = new List<ControlSetting>();
			RadControlState currentState = null;
			RadColorPicker radColorPicker = control as RadColorPicker;
			if (radColorPicker != null)
			{
				list.Add(new ControlSetting
				{
					Name = "SelectedColor",
					Value = radColorPicker.SelectedColor
				});
				currentState = new RadControlState(list, radColorPicker.UniqueID);
			}
			this.currentState = currentState;
		}

		// Token: 0x0600106D RID: 4205 RVA: 0x0003C114 File Offset: 0x0003A314
		public override void ApplySettings(Control control)
		{
			RadColorPicker radColorPicker = control as RadColorPicker;
			if (radColorPicker != null)
			{
				foreach (ControlSetting controlSetting in this.currentState.ControlSettings)
				{
					string a;
					if ((a = controlSetting.Name.ToString()) != null && a == "SelectedColor")
					{
						radColorPicker.SelectedColor = (Color)controlSetting.Value;
					}
				}
			}
		}
	}
}
