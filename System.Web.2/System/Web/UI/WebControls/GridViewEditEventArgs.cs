using System;
using System.ComponentModel;

namespace System.Web.UI.WebControls
{
	// Token: 0x0200041E RID: 1054
	public class GridViewEditEventArgs : CancelEventArgs
	{
		// Token: 0x0600338A RID: 13194 RVA: 0x000A900E File Offset: 0x000A720E
		public GridViewEditEventArgs(int newEditIndex)
		{
			this._newEditIndex = newEditIndex;
		}

		// Token: 0x17000EED RID: 3821
		// (get) Token: 0x0600338B RID: 13195 RVA: 0x000A901D File Offset: 0x000A721D
		// (set) Token: 0x0600338C RID: 13196 RVA: 0x000A9025 File Offset: 0x000A7225
		public int NewEditIndex
		{
			get
			{
				return this._newEditIndex;
			}
			set
			{
				this._newEditIndex = value;
			}
		}

		// Token: 0x0400216C RID: 8556
		private int _newEditIndex;
	}
}
