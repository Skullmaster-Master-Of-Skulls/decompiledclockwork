using System;
using System.Collections;
using System.Collections.Generic;

namespace Telerik.Web.UI
{
	// Token: 0x02001150 RID: 4432
	public class PageSizeItemsComparer : IComparer<ControlItem>, IComparer
	{
		// Token: 0x0600B493 RID: 46227 RVA: 0x0027C494 File Offset: 0x0027A694
		public int Compare(ControlItem x, ControlItem y)
		{
			int num;
			int value;
			if (int.TryParse(x.Text, out num) && int.TryParse(y.Text, out value))
			{
				return num.CompareTo(value);
			}
			return x.Text.CompareTo(y.Text);
		}

		// Token: 0x0600B494 RID: 46228 RVA: 0x0027C4D9 File Offset: 0x0027A6D9
		int IComparer.Compare(object x, object y)
		{
			return this.Compare(x as ControlItem, y as ControlItem);
		}
	}
}
