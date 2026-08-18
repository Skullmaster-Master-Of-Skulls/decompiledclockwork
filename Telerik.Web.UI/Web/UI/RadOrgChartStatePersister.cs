using System;
using System.Collections.Generic;
using System.Web.UI;
using Telerik.Web.UI.PersistenceFramework;

namespace Telerik.Web.UI
{
	// Token: 0x0200062E RID: 1582
	internal class RadOrgChartStatePersister : RadStatePersister
	{
		// Token: 0x06003987 RID: 14727 RVA: 0x000BD070 File Offset: 0x000BB270
		public override void ReadSettings(Control control)
		{
			List<ControlSetting> list = new List<ControlSetting>();
			RadControlState currentState = null;
			RadOrgChart radOrgChart = control as RadOrgChart;
			if (radOrgChart != null)
			{
				list.Add(new ControlSetting
				{
					Name = "CollapsedIndices",
					Value = radOrgChart.CollapsedIndices
				});
				list.Add(new ControlSetting
				{
					Name = "GroupCollapsedIndices",
					Value = radOrgChart.GroupCollapsedIndices
				});
				currentState = new RadControlState(list, radOrgChart.UniqueID);
			}
			this.currentState = currentState;
		}

		// Token: 0x06003988 RID: 14728 RVA: 0x000BD0F4 File Offset: 0x000BB2F4
		public override void ApplySettings(Control control)
		{
			RadOrgChart radOrgChart = control as RadOrgChart;
			if (radOrgChart != null)
			{
				foreach (ControlSetting controlSetting in this.currentState.ControlSettings)
				{
					string a;
					if ((a = controlSetting.Name.ToString()) != null)
					{
						if (!(a == "CollapsedIndices"))
						{
							if (a == "GroupCollapsedIndices")
							{
								radOrgChart.GroupCollapsedIndices = (List<string>)controlSetting.Value;
							}
						}
						else
						{
							radOrgChart.CollapsedIndices = (List<string>)controlSetting.Value;
						}
					}
				}
			}
		}
	}
}
