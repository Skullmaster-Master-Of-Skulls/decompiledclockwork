using System;
using System.Collections;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	// Token: 0x0200063F RID: 1599
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class SiteMapDataSourceView : DataSourceView
	{
		// Token: 0x06004EE9 RID: 20201 RVA: 0x0013E902 File Offset: 0x0013D902
		public SiteMapDataSourceView(SiteMapDataSource owner, string name, SiteMapNode node) : base(owner, name)
		{
			this._owner = owner;
			this._collection = new SiteMapNodeCollection(node);
		}

		// Token: 0x06004EEA RID: 20202 RVA: 0x0013E91F File Offset: 0x0013D91F
		public SiteMapDataSourceView(SiteMapDataSource owner, string name, SiteMapNodeCollection collection) : base(owner, name)
		{
			this._owner = owner;
			this._collection = collection;
		}

		// Token: 0x06004EEB RID: 20203 RVA: 0x0013E937 File Offset: 0x0013D937
		protected internal override IEnumerable ExecuteSelect(DataSourceSelectArguments arguments)
		{
			arguments.RaiseUnsupportedCapabilitiesError(this);
			return this._collection;
		}

		// Token: 0x06004EEC RID: 20204 RVA: 0x0013E946 File Offset: 0x0013D946
		protected override void OnDataSourceViewChanged(EventArgs e)
		{
			this._collection = this._owner.GetPathNodeCollection(base.Name);
			base.OnDataSourceViewChanged(e);
		}

		// Token: 0x06004EED RID: 20205 RVA: 0x0013E966 File Offset: 0x0013D966
		public IEnumerable Select(DataSourceSelectArguments arguments)
		{
			return this.ExecuteSelect(arguments);
		}

		// Token: 0x04002CB6 RID: 11446
		private SiteMapNodeCollection _collection;

		// Token: 0x04002CB7 RID: 11447
		private SiteMapDataSource _owner;
	}
}
