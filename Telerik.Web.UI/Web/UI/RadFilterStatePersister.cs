using System;
using System.Collections.Generic;
using System.Web.UI;
using Telerik.Web.UI.PersistenceFramework;

namespace Telerik.Web.UI
{
	// Token: 0x02000471 RID: 1137
	internal class RadFilterStatePersister : RadStatePersister
	{
		// Token: 0x060028DB RID: 10459 RVA: 0x00084330 File Offset: 0x00082530
		public override void ReadSettings(Control control)
		{
			List<ControlSetting> list = new List<ControlSetting>();
			RadControlState currentState = null;
			RadFilter radFilter = control as RadFilter;
			if (radFilter != null)
			{
				list.Add(new ControlSetting
				{
					Name = "FilterExpressions",
					Value = radFilter.FilterExpressions
				});
				currentState = new RadControlState(list, radFilter.UniqueID);
			}
			this.currentState = currentState;
		}

		// Token: 0x060028DC RID: 10460 RVA: 0x0008438C File Offset: 0x0008258C
		public override void ApplySettings(Control control)
		{
			RadFilter radFilter = control as RadFilter;
			if (radFilter != null)
			{
				foreach (ControlSetting controlSetting in this.currentState.ControlSettings)
				{
					string a;
					if ((a = controlSetting.Name.ToString()) != null && a == "FilterExpressions")
					{
						radFilter.FilterExpressions = (string)controlSetting.Value;
					}
				}
			}
		}
	}
}
