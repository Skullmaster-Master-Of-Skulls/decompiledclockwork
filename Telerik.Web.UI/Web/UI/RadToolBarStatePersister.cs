using System;
using System.Collections.Generic;
using System.Web.UI;
using Telerik.Web.UI.PersistenceFramework;

namespace Telerik.Web.UI
{
	// Token: 0x02000952 RID: 2386
	internal class RadToolBarStatePersister : RadStatePersister
	{
		// Token: 0x06005B11 RID: 23313 RVA: 0x00115190 File Offset: 0x00113390
		public override void ReadSettings(Control control)
		{
			List<ControlSetting> list = new List<ControlSetting>();
			RadControlState currentState = null;
			RadToolBar radToolBar = control as RadToolBar;
			if (radToolBar != null)
			{
				list.Add(new ControlSetting
				{
					Name = "CheckedIndices",
					Value = radToolBar.CheckedIndices
				});
				currentState = new RadControlState(list, radToolBar.UniqueID);
			}
			this.currentState = currentState;
		}

		// Token: 0x06005B12 RID: 23314 RVA: 0x001151EC File Offset: 0x001133EC
		public override void ApplySettings(Control control)
		{
			RadToolBar radToolBar = control as RadToolBar;
			if (radToolBar != null)
			{
				foreach (ControlSetting controlSetting in this.currentState.ControlSettings)
				{
					string a;
					if ((a = controlSetting.Name.ToString()) != null && a == "CheckedIndices")
					{
						radToolBar.CheckedIndices = (List<int>)controlSetting.Value;
					}
				}
			}
		}
	}
}
