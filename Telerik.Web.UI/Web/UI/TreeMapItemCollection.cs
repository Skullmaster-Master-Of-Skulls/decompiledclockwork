using System;

namespace Telerik.Web.UI
{
	// Token: 0x02000971 RID: 2417
	public class TreeMapItemCollection : GenericStateManagedCollection<TreeMapItem>
	{
		// Token: 0x06005BE0 RID: 23520 RVA: 0x00118410 File Offset: 0x00116610
		public TreeMapItemCollection()
		{
		}

		// Token: 0x06005BE1 RID: 23521 RVA: 0x00118418 File Offset: 0x00116618
		public TreeMapItemCollection(RadTreeMap control)
		{
			this._treeMap = control;
		}

		// Token: 0x04001615 RID: 5653
		private RadTreeMap _treeMap;
	}
}
