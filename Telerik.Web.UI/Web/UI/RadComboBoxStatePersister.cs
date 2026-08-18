using System;
using System.Collections.Generic;
using System.Web.UI;
using Telerik.Web.UI.PersistenceFramework;

namespace Telerik.Web.UI
{
	// Token: 0x020001BE RID: 446
	internal class RadComboBoxStatePersister : RadStatePersister
	{
		// Token: 0x0600106F RID: 4207 RVA: 0x0003C1A8 File Offset: 0x0003A3A8
		public override void ReadSettings(Control control)
		{
			List<ControlSetting> list = new List<ControlSetting>();
			RadControlState currentState = null;
			RadComboBox radComboBox = control as RadComboBox;
			if (radComboBox != null)
			{
				list.Add(new ControlSetting
				{
					Name = "CheckedIndices",
					Value = radComboBox.CheckedIndices
				});
				list.Add(new ControlSetting
				{
					Name = "SelectedIndex",
					Value = radComboBox.SelectedIndex
				});
				currentState = new RadControlState(list, radComboBox.UniqueID);
			}
			this.currentState = currentState;
		}

		// Token: 0x06001070 RID: 4208 RVA: 0x0003C234 File Offset: 0x0003A434
		public override void ApplySettings(Control control)
		{
			RadComboBox radComboBox = control as RadComboBox;
			if (radComboBox != null)
			{
				foreach (ControlSetting controlSetting in this.currentState.ControlSettings)
				{
					string a;
					if ((a = controlSetting.Name.ToString()) != null)
					{
						if (!(a == "CheckedIndices"))
						{
							if (a == "SelectedIndex")
							{
								radComboBox.SelectedIndex = (int)controlSetting.Value;
							}
						}
						else
						{
							radComboBox.CheckedIndices = (int[])controlSetting.Value;
						}
					}
				}
			}
		}
	}
}
