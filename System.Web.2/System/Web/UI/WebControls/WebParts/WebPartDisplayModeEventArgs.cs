using System;

namespace System.Web.UI.WebControls.WebParts
{
	// Token: 0x02000597 RID: 1431
	public class WebPartDisplayModeEventArgs : EventArgs
	{
		// Token: 0x06004822 RID: 18466 RVA: 0x000ECF07 File Offset: 0x000EB107
		public WebPartDisplayModeEventArgs(WebPartDisplayMode oldDisplayMode)
		{
			this._oldDisplayMode = oldDisplayMode;
		}

		// Token: 0x17001554 RID: 5460
		// (get) Token: 0x06004823 RID: 18467 RVA: 0x000ECF16 File Offset: 0x000EB116
		// (set) Token: 0x06004824 RID: 18468 RVA: 0x000ECF1E File Offset: 0x000EB11E
		public WebPartDisplayMode OldDisplayMode
		{
			get
			{
				return this._oldDisplayMode;
			}
			set
			{
				this._oldDisplayMode = value;
			}
		}

		// Token: 0x04002722 RID: 10018
		private WebPartDisplayMode _oldDisplayMode;
	}
}
