using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI.PersistenceFramework;

namespace Telerik.Web.UI
{
	// Token: 0x02000893 RID: 2195
	internal class RadSlidingPaneStatePersister : RadStatePersister
	{
		// Token: 0x060051BB RID: 20923 RVA: 0x000FEC54 File Offset: 0x000FCE54
		public override void ReadSettings(Control control)
		{
			List<ControlSetting> list = new List<ControlSetting>();
			RadControlState currentState = null;
			RadSlidingPane radSlidingPane = control as RadSlidingPane;
			if (radSlidingPane != null)
			{
				list.Add(new ControlSetting
				{
					Name = "Height",
					Value = radSlidingPane.Height
				});
				list.Add(new ControlSetting
				{
					Name = "Width",
					Value = radSlidingPane.Width
				});
				currentState = new RadControlState(list, radSlidingPane.UniqueID);
			}
			this.currentState = currentState;
		}

		// Token: 0x060051BC RID: 20924 RVA: 0x000FECE4 File Offset: 0x000FCEE4
		public override void ApplySettings(Control control)
		{
			RadSlidingPane radSlidingPane = control as RadSlidingPane;
			if (radSlidingPane != null)
			{
				foreach (ControlSetting controlSetting in this.currentState.ControlSettings)
				{
					string a;
					if ((a = controlSetting.Name.ToString()) != null)
					{
						if (!(a == "Height"))
						{
							if (a == "Width")
							{
								radSlidingPane.Width = (Unit)controlSetting.Value;
							}
						}
						else
						{
							radSlidingPane.Height = (Unit)controlSetting.Value;
						}
					}
				}
			}
		}
	}
}
