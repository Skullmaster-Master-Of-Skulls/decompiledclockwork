using System;
using System.Collections;

namespace AutoComboBox.MyControls.MultiLineTextBox
{
	// Token: 0x020000DD RID: 221
	public class ReverseComparer : IComparer
	{
		// Token: 0x060008B0 RID: 2224 RVA: 0x00043258 File Offset: 0x00042258
		public ReverseComparer(IComparer original)
		{
			this._original = original;
		}

		// Token: 0x060008B1 RID: 2225 RVA: 0x0004326C File Offset: 0x0004226C
		public int Compare(object x, object y)
		{
			return -this._original.Compare(x, y);
		}

		// Token: 0x04000644 RID: 1604
		private IComparer _original;
	}
}
