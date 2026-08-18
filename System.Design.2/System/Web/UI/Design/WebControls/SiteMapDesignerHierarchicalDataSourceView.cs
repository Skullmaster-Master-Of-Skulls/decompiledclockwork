using System;
using System.Security.Permissions;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x02000103 RID: 259
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public class SiteMapDesignerHierarchicalDataSourceView : DesignerHierarchicalDataSourceView
	{
		// Token: 0x0600092A RID: 2346 RVA: 0x00034DB0 File Offset: 0x00032FB0
		public SiteMapDesignerHierarchicalDataSourceView(SiteMapDataSourceDesigner owner, string viewPath) : base(owner, viewPath)
		{
			this._owner = owner;
		}

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x0600092B RID: 2347 RVA: 0x00034DC1 File Offset: 0x00032FC1
		public override IDataSourceSchema Schema
		{
			get
			{
				return SiteMapDataSourceDesigner.SiteMapHierarchicalSchema;
			}
		}

		// Token: 0x0600092C RID: 2348 RVA: 0x00034DC8 File Offset: 0x00032FC8
		public override IHierarchicalEnumerable GetDesignTimeData(out bool isSampleData)
		{
			string siteMapProvider = null;
			string startingNodeUrl = null;
			IHierarchicalEnumerable result = null;
			isSampleData = true;
			siteMapProvider = this._owner.SiteMapDataSource.SiteMapProvider;
			startingNodeUrl = this._owner.SiteMapDataSource.StartingNodeUrl;
			this._owner.SiteMapDataSource.Provider = this._owner.DesignTimeSiteMapProvider;
			try
			{
				this._owner.SiteMapDataSource.StartingNodeUrl = null;
				result = ((IHierarchicalDataSource)this._owner.SiteMapDataSource).GetHierarchicalView(base.Path).Select();
				isSampleData = false;
			}
			finally
			{
				this._owner.SiteMapDataSource.StartingNodeUrl = startingNodeUrl;
				this._owner.SiteMapDataSource.SiteMapProvider = siteMapProvider;
			}
			return result;
		}

		// Token: 0x0400055C RID: 1372
		private SiteMapDataSourceDesigner _owner;
	}
}
