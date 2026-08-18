using System;
using System.Data;
using System.Windows.Forms;

namespace DynamicScreens
{
	// Token: 0x02000015 RID: 21
	public class PanelInfo : IDisposable
	{
		// Token: 0x06000179 RID: 377 RVA: 0x00015E13 File Offset: 0x00014E13
		public PanelInfo(int _screenNum, Panel _panel, DataTable _controlListTable)
		{
			this.screenNum = _screenNum;
			this.panel = _panel;
			this.controlListTable = _controlListTable;
		}

		// Token: 0x0600017A RID: 378 RVA: 0x00015E33 File Offset: 0x00014E33
		public void Dispose()
		{
			this.panel = null;
			this.controlListTable = null;
		}

		// Token: 0x04000125 RID: 293
		public int screenNum;

		// Token: 0x04000126 RID: 294
		public Panel panel;

		// Token: 0x04000127 RID: 295
		public DataTable controlListTable;
	}
}
