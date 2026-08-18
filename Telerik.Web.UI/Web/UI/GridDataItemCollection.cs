using System;
using System.Collections;

namespace Telerik.Web.UI
{
	// Token: 0x02001148 RID: 4424
	public class GridDataItemCollection : GridItemCollection
	{
		// Token: 0x0600B443 RID: 46147 RVA: 0x002771D8 File Offset: 0x002753D8
		public GridDataItemCollection()
		{
		}

		// Token: 0x0600B444 RID: 46148 RVA: 0x002771E0 File Offset: 0x002753E0
		public GridDataItemCollection(ArrayList items) : base(items)
		{
		}

		// Token: 0x17003A44 RID: 14916
		public GridDataItem this[int index]
		{
			get
			{
				return (GridDataItem)base[index];
			}
		}

		// Token: 0x17003A45 RID: 14917
		public GridDataItem this[string hierarchicalIndex]
		{
			get
			{
				return (GridDataItem)base[hierarchicalIndex];
			}
		}
	}
}
