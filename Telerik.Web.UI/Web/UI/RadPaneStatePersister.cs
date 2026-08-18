using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI.PersistenceFramework;

namespace Telerik.Web.UI
{
	// Token: 0x02000892 RID: 2194
	internal class RadPaneStatePersister : RadStatePersister
	{
		// Token: 0x060051B8 RID: 20920 RVA: 0x000FEA60 File Offset: 0x000FCC60
		public override void ReadSettings(Control control)
		{
			List<ControlSetting> list = new List<ControlSetting>();
			RadControlState currentState = null;
			RadPane radPane = control as RadPane;
			if (radPane != null)
			{
				list.Add(new ControlSetting
				{
					Name = "Collapsed",
					Value = radPane.Collapsed
				});
				list.Add(new ControlSetting
				{
					Name = "ExpandedSize",
					Value = radPane.ExpandedSize
				});
				list.Add(new ControlSetting
				{
					Name = "Height",
					Value = radPane.Height
				});
				list.Add(new ControlSetting
				{
					Name = "Width",
					Value = radPane.Width
				});
				currentState = new RadControlState(list, radPane.UniqueID);
			}
			this.currentState = currentState;
		}

		// Token: 0x060051B9 RID: 20921 RVA: 0x000FEB50 File Offset: 0x000FCD50
		public override void ApplySettings(Control control)
		{
			RadPane radPane = control as RadPane;
			if (radPane != null)
			{
				foreach (ControlSetting controlSetting in this.currentState.ControlSettings)
				{
					string a;
					if ((a = controlSetting.Name.ToString()) != null)
					{
						if (!(a == "Collapsed"))
						{
							if (!(a == "ExpandedSize"))
							{
								if (!(a == "Height"))
								{
									if (a == "Width")
									{
										radPane.Width = (Unit)controlSetting.Value;
									}
								}
								else
								{
									radPane.Height = (Unit)controlSetting.Value;
								}
							}
							else
							{
								radPane.ExpandedSize = (Unit)controlSetting.Value;
							}
						}
						else
						{
							radPane.Collapsed = (bool)controlSetting.Value;
						}
					}
				}
			}
		}
	}
}
