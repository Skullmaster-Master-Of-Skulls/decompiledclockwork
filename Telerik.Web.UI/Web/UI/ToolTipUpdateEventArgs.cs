using System;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02001340 RID: 4928
	public class ToolTipUpdateEventArgs : EventArgs
	{
		// Token: 0x0600CD79 RID: 52601 RVA: 0x002DBED1 File Offset: 0x002DA0D1
		internal ToolTipUpdateEventArgs(string targetControlID, string val, UpdatePanel panel)
		{
			this._targetControlID = targetControlID;
			this._panel = panel;
			this._val = val;
		}

		// Token: 0x17004204 RID: 16900
		// (get) Token: 0x0600CD7A RID: 52602 RVA: 0x002DBEEE File Offset: 0x002DA0EE
		public string TargetControlID
		{
			get
			{
				return this._targetControlID;
			}
		}

		// Token: 0x17004205 RID: 16901
		// (get) Token: 0x0600CD7B RID: 52603 RVA: 0x002DBEF6 File Offset: 0x002DA0F6
		public string Value
		{
			get
			{
				return this._val;
			}
		}

		// Token: 0x17004206 RID: 16902
		// (get) Token: 0x0600CD7C RID: 52604 RVA: 0x002DBEFE File Offset: 0x002DA0FE
		public UpdatePanel UpdatePanel
		{
			get
			{
				return this._panel;
			}
		}

		// Token: 0x040036E3 RID: 14051
		private readonly string _targetControlID;

		// Token: 0x040036E4 RID: 14052
		private readonly string _val;

		// Token: 0x040036E5 RID: 14053
		private readonly UpdatePanel _panel;
	}
}
