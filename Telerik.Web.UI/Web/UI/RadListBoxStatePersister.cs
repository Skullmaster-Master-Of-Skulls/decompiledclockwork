using System;
using System.Collections.Generic;
using System.Web.UI;
using Telerik.Web.UI.PersistenceFramework;

namespace Telerik.Web.UI
{
	// Token: 0x02000576 RID: 1398
	internal class RadListBoxStatePersister : RadStatePersister
	{
		// Token: 0x060032A2 RID: 12962 RVA: 0x000A6210 File Offset: 0x000A4410
		public override void ReadSettings(Control control)
		{
			List<ControlSetting> list = new List<ControlSetting>();
			RadControlState currentState = null;
			RadListBox radListBox = control as RadListBox;
			if (radListBox != null)
			{
				list.Add(new ControlSetting
				{
					Name = "CheckedIndices",
					Value = radListBox.CheckedIndices
				});
				list.Add(new ControlSetting
				{
					Name = "SelectedIndices",
					Value = radListBox.SelectedIndices
				});
				currentState = new RadControlState(list, radListBox.UniqueID);
			}
			this.currentState = currentState;
		}

		// Token: 0x060032A3 RID: 12963 RVA: 0x000A6294 File Offset: 0x000A4494
		public override void ApplySettings(Control control)
		{
			RadListBox radListBox = control as RadListBox;
			if (radListBox != null)
			{
				foreach (ControlSetting controlSetting in this.currentState.ControlSettings)
				{
					string a;
					if ((a = controlSetting.Name.ToString()) != null)
					{
						if (!(a == "CheckedIndices"))
						{
							if (a == "SelectedIndices")
							{
								radListBox.SelectedIndices = (int[])controlSetting.Value;
							}
						}
						else
						{
							radListBox.CheckedIndices = (int[])controlSetting.Value;
						}
					}
				}
			}
		}
	}
}
