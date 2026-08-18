using System;

namespace System.Web.UI.WebControls
{
	// Token: 0x020004C9 RID: 1225
	public class SiteMapHierarchicalDataSourceView : HierarchicalDataSourceView
	{
		// Token: 0x06003CE7 RID: 15591 RVA: 0x000C4F96 File Offset: 0x000C3196
		public SiteMapHierarchicalDataSourceView(SiteMapNode node)
		{
			this._collection = new SiteMapNodeCollection(node);
		}

		// Token: 0x06003CE8 RID: 15592 RVA: 0x000C4FAA File Offset: 0x000C31AA
		public SiteMapHierarchicalDataSourceView(SiteMapNodeCollection collection)
		{
			this._collection = collection;
		}

		// Token: 0x06003CE9 RID: 15593 RVA: 0x000C4FB9 File Offset: 0x000C31B9
		public override IHierarchicalEnumerable Select()
		{
			return this._collection;
		}

		// Token: 0x0400239C RID: 9116
		private SiteMapNodeCollection _collection;
	}
}
