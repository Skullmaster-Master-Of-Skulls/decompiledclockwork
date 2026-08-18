using System;
using System.Collections.Generic;
using System.Web.UI;
using Telerik.Web.UI.PersistenceFramework;

namespace Telerik.Web.UI
{
	// Token: 0x0200090F RID: 2319
	internal class RadTileListStatePersister : RadStatePersister
	{
		// Token: 0x0600579D RID: 22429 RVA: 0x0010BAA4 File Offset: 0x00109CA4
		public override void ReadSettings(Control control)
		{
			List<ControlSetting> list = new List<ControlSetting>();
			RadControlState currentState = null;
			RadTileList radTileList = control as RadTileList;
			if (radTileList != null)
			{
				list.Add(new ControlSetting
				{
					Name = "SelectedTilesUniqueIds",
					Value = radTileList.SelectedTilesUniqueIds
				});
				list.Add(new ControlSetting
				{
					Name = "TileGroupIndices",
					Value = radTileList.TileGroupIndices
				});
				currentState = new RadControlState(list, radTileList.UniqueID);
			}
			this.currentState = currentState;
		}

		// Token: 0x0600579E RID: 22430 RVA: 0x0010BB28 File Offset: 0x00109D28
		public override void ApplySettings(Control control)
		{
			RadTileList radTileList = control as RadTileList;
			if (radTileList != null)
			{
				foreach (ControlSetting controlSetting in this.currentState.ControlSettings)
				{
					string a;
					if ((a = controlSetting.Name.ToString()) != null)
					{
						if (!(a == "SelectedTilesUniqueIds"))
						{
							if (a == "TileGroupIndices")
							{
								radTileList.TileGroupIndices = (List<int[]>)controlSetting.Value;
							}
						}
						else
						{
							radTileList.SelectedTilesUniqueIds = (List<string>)controlSetting.Value;
						}
					}
				}
			}
		}
	}
}
