using System;
using System.ComponentModel;

namespace System.Web.UI.WebControls
{
	// Token: 0x020000B0 RID: 176
	public class ListViewEditEventArgs : CancelEventArgs
	{
		// Token: 0x060008C1 RID: 2241 RVA: 0x0002234E File Offset: 0x0002054E
		public ListViewEditEventArgs(int newEditIndex) : base(false)
		{
			this._newEditIndex = newEditIndex;
		}

		// Token: 0x17000275 RID: 629
		// (get) Token: 0x060008C2 RID: 2242 RVA: 0x0002235E File Offset: 0x0002055E
		public int NewEditIndex
		{
			get
			{
				return this._newEditIndex;
			}
		}

		// Token: 0x040002E3 RID: 739
		private int _newEditIndex;
	}
}
