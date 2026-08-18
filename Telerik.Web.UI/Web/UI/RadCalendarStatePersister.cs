using System;
using System.Collections.Generic;
using System.Web.UI;
using Telerik.Web.UI.PersistenceFramework;

namespace Telerik.Web.UI
{
	// Token: 0x02000196 RID: 406
	internal class RadCalendarStatePersister : RadStatePersister
	{
		// Token: 0x06000DCD RID: 3533 RVA: 0x00034460 File Offset: 0x00032660
		public override void ReadSettings(Control control)
		{
			List<ControlSetting> list = new List<ControlSetting>();
			RadControlState currentState = null;
			RadCalendar radCalendar = control as RadCalendar;
			if (radCalendar != null)
			{
				list.Add(new ControlSetting
				{
					Name = "FocusedDate",
					Value = radCalendar.FocusedDate
				});
				list.Add(new ControlSetting
				{
					Name = "SelectedDate",
					Value = radCalendar.SelectedDate
				});
				currentState = new RadControlState(list, radCalendar.UniqueID);
			}
			this.currentState = currentState;
		}

		// Token: 0x06000DCE RID: 3534 RVA: 0x000344F0 File Offset: 0x000326F0
		public override void ApplySettings(Control control)
		{
			RadCalendar radCalendar = control as RadCalendar;
			if (radCalendar != null)
			{
				foreach (ControlSetting controlSetting in this.currentState.ControlSettings)
				{
					string a;
					if ((a = controlSetting.Name.ToString()) != null)
					{
						if (!(a == "FocusedDate"))
						{
							if (a == "SelectedDate")
							{
								radCalendar.SelectedDate = (DateTime)controlSetting.Value;
							}
						}
						else
						{
							radCalendar.FocusedDate = (DateTime)controlSetting.Value;
						}
					}
				}
			}
		}
	}
}
