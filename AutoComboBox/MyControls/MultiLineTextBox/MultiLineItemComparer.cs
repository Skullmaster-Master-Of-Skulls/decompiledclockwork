using System;
using System.Collections;

namespace AutoComboBox.MyControls.MultiLineTextBox
{
	// Token: 0x020000DC RID: 220
	public class MultiLineItemComparer : IComparer
	{
		// Token: 0x060008AE RID: 2222 RVA: 0x00043220 File Offset: 0x00042220
		public int Compare(object x, object y)
		{
			return ((MultiLineItem)x).DateEntered.CompareTo(((MultiLineItem)y).DateEntered);
		}
	}
}
