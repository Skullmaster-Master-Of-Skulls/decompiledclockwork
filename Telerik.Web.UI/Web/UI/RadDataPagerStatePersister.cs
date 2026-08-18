using System;
using System.Collections.Generic;
using System.Web.UI;
using Telerik.Web.UI.PersistenceFramework;

namespace Telerik.Web.UI
{
	// Token: 0x02000589 RID: 1417
	internal class RadDataPagerStatePersister : RadStatePersister
	{
		// Token: 0x0600331A RID: 13082 RVA: 0x000AA478 File Offset: 0x000A8678
		public override void ReadSettings(Control control)
		{
			List<ControlSetting> list = new List<ControlSetting>();
			RadControlState currentState = null;
			RadDataPager radDataPager = control as RadDataPager;
			if (radDataPager != null)
			{
				list.Add(new ControlSetting
				{
					Name = "PageSize",
					Value = radDataPager.PageSize
				});
				list.Add(new ControlSetting
				{
					Name = "StartRowIndex",
					Value = radDataPager.StartRowIndex
				});
				currentState = new RadControlState(list, radDataPager.UniqueID);
			}
			this.currentState = currentState;
		}

		// Token: 0x0600331B RID: 13083 RVA: 0x000AA508 File Offset: 0x000A8708
		public override void ApplySettings(Control control)
		{
			RadDataPager radDataPager = control as RadDataPager;
			if (radDataPager != null)
			{
				foreach (ControlSetting controlSetting in this.currentState.ControlSettings)
				{
					string a;
					if ((a = controlSetting.Name.ToString()) != null)
					{
						if (!(a == "PageSize"))
						{
							if (a == "StartRowIndex")
							{
								radDataPager.StartRowIndex = (int)controlSetting.Value;
							}
						}
						else
						{
							radDataPager.PageSize = (int)controlSetting.Value;
						}
					}
				}
			}
		}
	}
}
