using System;
using System.Collections.Generic;
using System.Web.UI;
using Telerik.Web.UI.PersistenceFramework;

namespace Telerik.Web.UI
{
	// Token: 0x02000891 RID: 2193
	internal class RadSlidingZoneStatePersister : RadStatePersister
	{
		// Token: 0x060051B5 RID: 20917 RVA: 0x000FE924 File Offset: 0x000FCB24
		public override void ReadSettings(Control control)
		{
			List<ControlSetting> list = new List<ControlSetting>();
			RadControlState currentState = null;
			RadSlidingZone radSlidingZone = control as RadSlidingZone;
			if (radSlidingZone != null)
			{
				list.Add(new ControlSetting
				{
					Name = "DockedPaneId",
					Value = radSlidingZone.DockedPaneId
				});
				list.Add(new ControlSetting
				{
					Name = "ExpandedPaneId",
					Value = radSlidingZone.ExpandedPaneId
				});
				currentState = new RadControlState(list, radSlidingZone.UniqueID);
			}
			this.currentState = currentState;
		}

		// Token: 0x060051B6 RID: 20918 RVA: 0x000FE9A8 File Offset: 0x000FCBA8
		public override void ApplySettings(Control control)
		{
			RadSlidingZone radSlidingZone = control as RadSlidingZone;
			if (radSlidingZone != null)
			{
				foreach (ControlSetting controlSetting in this.currentState.ControlSettings)
				{
					string a;
					if ((a = controlSetting.Name.ToString()) != null)
					{
						if (!(a == "DockedPaneId"))
						{
							if (a == "ExpandedPaneId")
							{
								radSlidingZone.ExpandedPaneId = (string)controlSetting.Value;
							}
						}
						else
						{
							radSlidingZone.DockedPaneId = (string)controlSetting.Value;
						}
					}
				}
			}
		}
	}
}
