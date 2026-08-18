using System;
using System.Collections.Generic;

namespace Telerik.Web.UI
{
	// Token: 0x020019B9 RID: 6585
	[Serializable]
	public class RadListViewIndexesCollection : List<int>
	{
		// Token: 0x0600FE8C RID: 65164 RVA: 0x003927B0 File Offset: 0x003909B0
		public RadListViewIndexesCollection()
		{
		}

		// Token: 0x0600FE8D RID: 65165 RVA: 0x003927B8 File Offset: 0x003909B8
		public RadListViewIndexesCollection(IEnumerable<int> collection) : base(collection)
		{
		}

		// Token: 0x0600FE8E RID: 65166 RVA: 0x003927C1 File Offset: 0x003909C1
		public new void Add(int item)
		{
			if (!base.Contains(item))
			{
				base.Add(item);
			}
		}
	}
}
