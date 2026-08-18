using System;
using System.Collections.Generic;

namespace Telerik.Web.UI
{
	// Token: 0x0200048E RID: 1166
	public class PersistenceManagerSaveStateEventArgs : EventArgs
	{
		// Token: 0x17000D61 RID: 3425
		// (get) Token: 0x06002968 RID: 10600 RVA: 0x000857EE File Offset: 0x000839EE
		// (set) Token: 0x06002969 RID: 10601 RVA: 0x000857F6 File Offset: 0x000839F6
		public List<ControlSetting> CustomSettings { get; set; }
	}
}
