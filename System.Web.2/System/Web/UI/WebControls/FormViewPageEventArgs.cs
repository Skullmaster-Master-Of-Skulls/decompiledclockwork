using System;
using System.ComponentModel;

namespace System.Web.UI.WebControls
{
	// Token: 0x0200040C RID: 1036
	public class FormViewPageEventArgs : CancelEventArgs
	{
		// Token: 0x06003230 RID: 12848 RVA: 0x000A38DB File Offset: 0x000A1ADB
		public FormViewPageEventArgs(int newPageIndex)
		{
			this._newPageIndex = newPageIndex;
		}

		// Token: 0x17000E7A RID: 3706
		// (get) Token: 0x06003231 RID: 12849 RVA: 0x000A38EA File Offset: 0x000A1AEA
		// (set) Token: 0x06003232 RID: 12850 RVA: 0x000A38F2 File Offset: 0x000A1AF2
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

		// Token: 0x04002107 RID: 8455
		private int _newPageIndex;
	}
}
