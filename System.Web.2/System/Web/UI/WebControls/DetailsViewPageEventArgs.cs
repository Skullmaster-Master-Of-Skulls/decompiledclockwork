using System;
using System.ComponentModel;

namespace System.Web.UI.WebControls
{
	// Token: 0x020003E4 RID: 996
	public class DetailsViewPageEventArgs : CancelEventArgs
	{
		// Token: 0x06003056 RID: 12374 RVA: 0x0009E5AB File Offset: 0x0009C7AB
		public DetailsViewPageEventArgs(int newPageIndex)
		{
			this._newPageIndex = newPageIndex;
		}

		// Token: 0x17000DED RID: 3565
		// (get) Token: 0x06003057 RID: 12375 RVA: 0x0009E5BA File Offset: 0x0009C7BA
		// (set) Token: 0x06003058 RID: 12376 RVA: 0x0009E5C2 File Offset: 0x0009C7C2
		public int NewPageIndex
		{
			get
			{
				return this._newPageIndex;
			}
			set
			{
				this._newPageIndex = value;
			}
		}

		// Token: 0x04002085 RID: 8325
		private int _newPageIndex;
	}
}
