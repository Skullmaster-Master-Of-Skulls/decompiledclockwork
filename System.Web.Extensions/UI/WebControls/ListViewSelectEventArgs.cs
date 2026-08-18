using System;
using System.ComponentModel;

namespace System.Web.UI.WebControls
{
	// Token: 0x020000B7 RID: 183
	public class ListViewSelectEventArgs : CancelEventArgs
	{
		// Token: 0x060008EE RID: 2286 RVA: 0x000226BD File Offset: 0x000208BD
		public ListViewSelectEventArgs(int newSelectedIndex) : base(false)
		{
			this._newSelectedIndex = newSelectedIndex;
		}

		// Token: 0x1700028E RID: 654
		// (get) Token: 0x060008EF RID: 2287 RVA: 0x000226CD File Offset: 0x000208CD
		// (set) Token: 0x060008F0 RID: 2288 RVA: 0x000226D5 File Offset: 0x000208D5
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

		// Token: 0x040002F7 RID: 759
		private int _newSelectedIndex;
	}
}
