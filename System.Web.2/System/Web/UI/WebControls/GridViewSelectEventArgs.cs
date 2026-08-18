using System;
using System.ComponentModel;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000426 RID: 1062
	public class GridViewSelectEventArgs : CancelEventArgs
	{
		// Token: 0x060033B4 RID: 13236 RVA: 0x000A9194 File Offset: 0x000A7394
		public GridViewSelectEventArgs(int newSelectedIndex)
		{
			this._newSelectedIndex = newSelectedIndex;
		}

		// Token: 0x17000EFD RID: 3837
		// (get) Token: 0x060033B5 RID: 13237 RVA: 0x000A91A3 File Offset: 0x000A73A3
		// (set) Token: 0x060033B6 RID: 13238 RVA: 0x000A91AB File Offset: 0x000A73AB
		public int NewSelectedIndex
		{
			get
			{
				return this._newSelectedIndex;
			}
			set
			{
				this._newSelectedIndex = value;
			}
		}

		// Token: 0x04002175 RID: 8565
		private int _newSelectedIndex;
	}
}
