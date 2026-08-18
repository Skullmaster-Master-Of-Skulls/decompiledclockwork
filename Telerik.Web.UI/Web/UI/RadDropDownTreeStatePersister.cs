using System;
using System.Collections.Generic;
using System.Web.UI;
using Telerik.Web.UI.PersistenceFramework;

namespace Telerik.Web.UI
{
	// Token: 0x02000469 RID: 1129
	internal class RadDropDownTreeStatePersister : RadStatePersister
	{
		// Token: 0x06002881 RID: 10369 RVA: 0x00083234 File Offset: 0x00081434
		public override void ReadSettings(Control control)
		{
			List<ControlSetting> list = new List<ControlSetting>();
			RadControlState currentState = null;
			RadDropDownTree radDropDownTree = control as RadDropDownTree;
			if (radDropDownTree != null)
			{
				list.Add(new ControlSetting
				{
					Name = "CheckedIndices",
					Value = radDropDownTree.CheckedIndices
				});
				list.Add(new ControlSetting
				{
					Name = "ExpandedIndices",
					Value = radDropDownTree.ExpandedIndices
				});
				list.Add(new ControlSetting
				{
					Name = "SelectedIndices",
					Value = radDropDownTree.SelectedIndices
				});
				currentState = new RadControlState(list, radDropDownTree.UniqueID);
			}
			this.currentState = currentState;
		}

		// Token: 0x06002882 RID: 10370 RVA: 0x000832E4 File Offset: 0x000814E4
		public override void ApplySettings(Control control)
		{
			RadDropDownTree radDropDownTree = control as RadDropDownTree;
			if (radDropDownTree != null)
			{
				foreach (ControlSetting controlSetting in this.currentState.ControlSettings)
				{
					string a;
					if ((a = controlSetting.Name.ToString()) != null)
					{
						if (!(a == "CheckedIndices"))
						{
							if (!(a == "ExpandedIndices"))
							{
								if (a == "SelectedIndices")
								{
									radDropDownTree.SelectedIndices = (List<string>)controlSetting.Value;
								}
							}
							else
							{
								radDropDownTree.ExpandedIndices = (List<string>)controlSetting.Value;
							}
						}
						else
						{
							radDropDownTree.CheckedIndices = (List<string>)controlSetting.Value;
						}
					}
				}
			}
		}
	}
}
