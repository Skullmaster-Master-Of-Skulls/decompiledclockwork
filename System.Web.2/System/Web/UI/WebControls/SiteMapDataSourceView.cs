using System;
using System.Collections;

namespace System.Web.UI.WebControls
{
	// Token: 0x020004C8 RID: 1224
	public class SiteMapDataSourceView : DataSourceView
	{
		// Token: 0x06003CE2 RID: 15586 RVA: 0x000C4F32 File Offset: 0x000C3132
		public SiteMapDataSourceView(SiteMapDataSource owner, string name, SiteMapNode node) : base(owner, name)
		{
			this._owner = owner;
			this._collection = new SiteMapNodeCollection(node);
		}

		// Token: 0x06003CE3 RID: 15587 RVA: 0x000C4F4F File Offset: 0x000C314F
		public SiteMapDataSourceView(SiteMapDataSource owner, string name, SiteMapNodeCollection collection) : base(owner, name)
		{
			this._owner = owner;
			this._collection = collection;
		}

		// Token: 0x06003CE4 RID: 15588 RVA: 0x000C4F67 File Offset: 0x000C3167
		protected internal override IEnumerable ExecuteSelect(DataSourceSelectArguments arguments)
		{
			arguments.RaiseUnsupportedCapabilitiesError(this);
			return this._collection;
		}

		// Token: 0x06003CE5 RID: 15589 RVA: 0x000C4F76 File Offset: 0x000C3176
		protected override void OnDataSourceViewChanged(EventArgs e)
		{
			this._collection = this._owner.GetPathNodeCollection(base.Name);
			base.OnDataSourceViewChanged(e);
		}

		// Token: 0x06003CE6 RID: 15590 RVA: 0x000B940C File Offset: 0x000B760C
		public IEnumerable Select(DataSourceSelectArguments arguments)
		{
			return this.ExecuteSelect(arguments);
		}

		// Token: 0x0400239A RID: 9114
		private SiteMapNodeCollection _collection;

		// Token: 0x0400239B RID: 9115
		private SiteMapDataSource _owner;
	}
}
