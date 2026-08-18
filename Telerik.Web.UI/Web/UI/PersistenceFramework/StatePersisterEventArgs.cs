using System;
using System.Web.UI;

namespace Telerik.Web.UI.PersistenceFramework
{
	// Token: 0x02000886 RID: 2182
	public class StatePersisterEventArgs : EventArgs
	{
		// Token: 0x060050BF RID: 20671 RVA: 0x000FBDDB File Offset: 0x000F9FDB
		internal StatePersisterEventArgs(Control c, RadControlState state)
		{
			this.State = state;
			this.Control = c;
		}

		// Token: 0x060050C0 RID: 20672 RVA: 0x000FBDF1 File Offset: 0x000F9FF1
		public void AddSetting(ControlSetting setting)
		{
			this.State.ControlSettings.Add(setting);
		}

		// Token: 0x040013EC RID: 5100
		public RadControlState State;

		// Token: 0x040013ED RID: 5101
		public readonly Control Control;
	}
}
