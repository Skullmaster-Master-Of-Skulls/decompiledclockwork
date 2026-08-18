using System;
using System.Collections.Generic;
using System.Web.UI;
using Telerik.Web.UI.PersistenceFramework;

namespace Telerik.Web.UI
{
	// Token: 0x0200087B RID: 2171
	internal class RadSliderStatePersister : RadStatePersister
	{
		// Token: 0x06005060 RID: 20576 RVA: 0x000FB370 File Offset: 0x000F9570
		public override void ReadSettings(Control control)
		{
			List<ControlSetting> list = new List<ControlSetting>();
			RadControlState currentState = null;
			RadSlider radSlider = control as RadSlider;
			if (radSlider != null)
			{
				list.Add(new ControlSetting
				{
					Name = "SelectionEnd",
					Value = radSlider.SelectionEnd
				});
				list.Add(new ControlSetting
				{
					Name = "SelectionStart",
					Value = radSlider.SelectionStart
				});
				list.Add(new ControlSetting
				{
					Name = "Value",
					Value = radSlider.Value
				});
				currentState = new RadControlState(list, radSlider.UniqueID);
			}
			this.currentState = currentState;
		}

		// Token: 0x06005061 RID: 20577 RVA: 0x000FB430 File Offset: 0x000F9630
		public override void ApplySettings(Control control)
		{
			RadSlider radSlider = control as RadSlider;
			if (radSlider != null)
			{
				foreach (ControlSetting controlSetting in this.currentState.ControlSettings)
				{
					string a;
					if ((a = controlSetting.Name.ToString()) != null)
					{
						if (!(a == "SelectionEnd"))
						{
							if (!(a == "SelectionStart"))
							{
								if (a == "Value")
								{
									radSlider.Value = (decimal)controlSetting.Value;
								}
							}
							else
							{
								radSlider.SelectionStart = (decimal)controlSetting.Value;
							}
						}
						else
						{
							radSlider.SelectionEnd = (decimal)controlSetting.Value;
						}
					}
				}
			}
		}
	}
}
