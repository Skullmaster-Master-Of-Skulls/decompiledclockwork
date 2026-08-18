using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI.PersistenceFramework;

namespace Telerik.Web.UI
{
	// Token: 0x02000457 RID: 1111
	internal class RadDockStatePersister : RadStatePersister
	{
		// Token: 0x06002829 RID: 10281 RVA: 0x00082364 File Offset: 0x00080564
		public override void ReadSettings(Control control)
		{
			List<ControlSetting> list = new List<ControlSetting>();
			RadControlState currentState = null;
			RadDock radDock = control as RadDock;
			if (radDock != null)
			{
				list.Add(new ControlSetting
				{
					Name = "Closed",
					Value = radDock.Closed
				});
				list.Add(new ControlSetting
				{
					Name = "Collapsed",
					Value = radDock.Collapsed
				});
				list.Add(new ControlSetting
				{
					Name = "ExpandedHeight",
					Value = radDock.ExpandedHeight
				});
				list.Add(new ControlSetting
				{
					Name = "Height",
					Value = radDock.Height
				});
				list.Add(new ControlSetting
				{
					Name = "Index",
					Value = radDock.Index
				});
				list.Add(new ControlSetting
				{
					Name = "Left",
					Value = radDock.Left
				});
				list.Add(new ControlSetting
				{
					Name = "PersistedDockZoneID",
					Value = radDock.PersistedDockZoneID
				});
				list.Add(new ControlSetting
				{
					Name = "Pinned",
					Value = radDock.Pinned
				});
				list.Add(new ControlSetting
				{
					Name = "Top",
					Value = radDock.Top
				});
				list.Add(new ControlSetting
				{
					Name = "Width",
					Value = radDock.Width
				});
				currentState = new RadControlState(list, radDock.UniqueID);
			}
			this.currentState = currentState;
		}

		// Token: 0x0600282A RID: 10282 RVA: 0x00082560 File Offset: 0x00080760
		public override void ApplySettings(Control control)
		{
			RadDock radDock = control as RadDock;
			if (radDock != null)
			{
				foreach (ControlSetting controlSetting in this.currentState.ControlSettings)
				{
					string key;
					switch (key = controlSetting.Name.ToString())
					{
					case "Closed":
						radDock.Closed = (bool)controlSetting.Value;
						break;
					case "Collapsed":
						radDock.Collapsed = (bool)controlSetting.Value;
						break;
					case "ExpandedHeight":
						radDock.ExpandedHeight = (int)controlSetting.Value;
						break;
					case "Height":
						radDock.Height = (Unit)controlSetting.Value;
						break;
					case "Index":
						radDock.Index = (int)controlSetting.Value;
						break;
					case "Left":
						radDock.Left = (Unit)controlSetting.Value;
						break;
					case "PersistedDockZoneID":
						radDock.PersistedDockZoneID = (string)controlSetting.Value;
						break;
					case "Pinned":
						radDock.Pinned = (bool)controlSetting.Value;
						break;
					case "Top":
						radDock.Top = (Unit)controlSetting.Value;
						break;
					case "Width":
						radDock.Width = (Unit)controlSetting.Value;
						break;
					}
				}
			}
		}
	}
}
