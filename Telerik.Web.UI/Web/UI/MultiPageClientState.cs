using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x02001AD2 RID: 6866
	[EditorBrowsable(EditorBrowsableState.Never)]
	public class MultiPageClientState
	{
		// Token: 0x170050D2 RID: 20690
		// (get) Token: 0x060109FB RID: 68091 RVA: 0x003B58A9 File Offset: 0x003B3AA9
		// (set) Token: 0x060109FC RID: 68092 RVA: 0x003B58B1 File Offset: 0x003B3AB1
		public int SelectedIndex
		{
			get
			{
				return this._selectedIndex;
			}
			set
			{
				this._selectedIndex = value;
			}
		}

		// Token: 0x170050D3 RID: 20691
		// (get) Token: 0x060109FD RID: 68093 RVA: 0x003B58BA File Offset: 0x003B3ABA
		// (set) Token: 0x060109FE RID: 68094 RVA: 0x003B58C2 File Offset: 0x003B3AC2
		public IList<ClientStateLogEntry> ChangeLog
		{
			get
			{
				return this._changeLog;
			}
			set
			{
				this._changeLog = value;
			}
		}

		// Token: 0x04004A4D RID: 19021
		private int _selectedIndex;

		// Token: 0x04004A4E RID: 19022
		private IList<ClientStateLogEntry> _changeLog;
	}
}
