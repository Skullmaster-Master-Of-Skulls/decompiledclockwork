using System;
using System.Collections.Generic;
using System.Web.UI;

namespace TechnoPro.Common.UI.Web.ClockWork.Controls
{
	// Token: 0x0200000F RID: 15
	public class SubControlsRequiredArgs : EventArgs
	{
		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000D6 RID: 214 RVA: 0x00004621 File Offset: 0x00002821
		// (set) Token: 0x060000D7 RID: 215 RVA: 0x00004629 File Offset: 0x00002829
		public IList<Control> Controls { get; set; }
	}
}
