using System;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000640 RID: 1600
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class SiteMapHierarchicalDataSourceView : HierarchicalDataSourceView
	{
		// Token: 0x06004EEE RID: 20206 RVA: 0x0013E96F File Offset: 0x0013D96F
		public SiteMapHierarchicalDataSourceView(SiteMapNode node)
		{
			this._collection = new SiteMapNodeCollection(node);
		}

		// Token: 0x06004EEF RID: 20207 RVA: 0x0013E983 File Offset: 0x0013D983
		public SiteMapHierarchicalDataSourceView(SiteMapNodeCollection collection)
		{
			this._collection = collection;
		}

		// Token: 0x06004EF0 RID: 20208 RVA: 0x0013E992 File Offset: 0x0013D992
		public override IHierarchicalEnumerable Select()
		{
			return this._collection;
		}

		// Token: 0x04002CB8 RID: 11448
		private SiteMapNodeCollection _collection;
	}
}
