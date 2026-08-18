using System;
using System.Collections.Generic;
using System.Web.UI;
using Telerik.Web.UI.PersistenceFramework;

namespace Telerik.Web.UI
{
	// Token: 0x02000824 RID: 2084
	internal class RadSchedulerStatePersister : RadStatePersister
	{
		// Token: 0x06004D1A RID: 19738 RVA: 0x000F2930 File Offset: 0x000F0B30
		public override void ReadSettings(Control control)
		{
			List<ControlSetting> list = new List<ControlSetting>();
			RadControlState currentState = null;
			RadScheduler radScheduler = control as RadScheduler;
			if (radScheduler != null)
			{
				list.Add(new ControlSetting
				{
					Name = "SelectedDate",
					Value = radScheduler.SelectedDate
				});
				list.Add(new ControlSetting
				{
					Name = "SelectedView",
					Value = radScheduler.SelectedView
				});
				currentState = new RadControlState(list, radScheduler.UniqueID);
			}
			this.currentState = currentState;
		}

		// Token: 0x06004D1B RID: 19739 RVA: 0x000F29C0 File Offset: 0x000F0BC0
		public override void ApplySettings(Control control)
		{
			RadScheduler radScheduler = control as RadScheduler;
			if (radScheduler != null)
			{
				foreach (ControlSetting controlSetting in this.currentState.ControlSettings)
				{
					string a;
					if ((a = controlSetting.Name.ToString()) != null)
					{
						if (!(a == "SelectedDate"))
						{
							if (a == "SelectedView")
							{
								radScheduler.SelectedView = (SchedulerViewType)controlSetting.Value;
							}
						}
						else
						{
							radScheduler.SelectedDate = (DateTime)controlSetting.Value;
						}
					}
				}
			}
		}
	}
}
