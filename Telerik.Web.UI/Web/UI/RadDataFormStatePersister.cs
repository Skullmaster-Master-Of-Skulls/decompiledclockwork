using System;
using System.Collections.Generic;
using System.Web.UI;
using Telerik.Web.UI.PersistenceFramework;

namespace Telerik.Web.UI
{
	// Token: 0x02000205 RID: 517
	internal class RadDataFormStatePersister : RadStatePersister
	{
		// Token: 0x0600133A RID: 4922 RVA: 0x00044120 File Offset: 0x00042320
		public override void ReadSettings(Control control)
		{
			List<ControlSetting> list = new List<ControlSetting>();
			RadControlState currentState = null;
			RadDataForm radDataForm = control as RadDataForm;
			if (radDataForm != null)
			{
				list.Add(new ControlSetting
				{
					Name = "CurrentPageIndex",
					Value = radDataForm.CurrentPageIndex
				});
				list.Add(new ControlSetting
				{
					Name = "EditIndex",
					Value = radDataForm.EditIndex
				});
				list.Add(new ControlSetting
				{
					Name = "IsItemInserted",
					Value = radDataForm.IsItemInserted
				});
				currentState = new RadControlState(list, radDataForm.UniqueID);
			}
			this.currentState = currentState;
		}

		// Token: 0x0600133B RID: 4923 RVA: 0x000441E0 File Offset: 0x000423E0
		public override void ApplySettings(Control control)
		{
			RadDataForm radDataForm = control as RadDataForm;
			if (radDataForm != null)
			{
				foreach (ControlSetting controlSetting in this.currentState.ControlSettings)
				{
					string a;
					if ((a = controlSetting.Name.ToString()) != null)
					{
						if (!(a == "CurrentPageIndex"))
						{
							if (!(a == "EditIndex"))
							{
								if (a == "IsItemInserted")
								{
									radDataForm.IsItemInserted = (bool)controlSetting.Value;
								}
							}
							else
							{
								radDataForm.EditIndex = (int)controlSetting.Value;
							}
						}
						else
						{
							radDataForm.CurrentPageIndex = (int)controlSetting.Value;
						}
					}
				}
			}
		}
	}
}
