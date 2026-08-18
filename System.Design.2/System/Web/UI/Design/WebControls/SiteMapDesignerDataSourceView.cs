using System;
using System.Collections;
using System.Security.Permissions;
using System.Web.UI.WebControls;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x02000102 RID: 258
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public class SiteMapDesignerDataSourceView : DesignerDataSourceView
	{
		// Token: 0x06000926 RID: 2342 RVA: 0x00034CBA File Offset: 0x00032EBA
		public SiteMapDesignerDataSourceView(SiteMapDataSourceDesigner owner, string viewName) : base(owner, viewName)
		{
			this._owner = owner;
			this._siteMapDataSource = (SiteMapDataSource)this._owner.Component;
		}

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x06000927 RID: 2343 RVA: 0x00034CE1 File Offset: 0x00032EE1
		public override IDataSourceViewSchema Schema
		{
			get
			{
				return SiteMapDesignerDataSourceView._siteMapViewSchema;
			}
		}

		// Token: 0x06000928 RID: 2344 RVA: 0x00034CE8 File Offset: 0x00032EE8
		public override IEnumerable GetDesignTimeData(int minimumRows, out bool isSampleData)
		{
			string siteMapProvider = null;
			string startingNodeUrl = null;
			SiteMapNodeCollection siteMapNodeCollection = null;
			siteMapProvider = this._siteMapDataSource.SiteMapProvider;
			startingNodeUrl = this._siteMapDataSource.StartingNodeUrl;
			this._siteMapDataSource.Provider = this._owner.DesignTimeSiteMapProvider;
			try
			{
				this._siteMapDataSource.StartingNodeUrl = null;
				siteMapNodeCollection = (((SiteMapDataSourceView)((IDataSource)this._siteMapDataSource).GetView(base.Name)).Select(DataSourceSelectArguments.Empty) as SiteMapNodeCollection);
				isSampleData = false;
			}
			finally
			{
				this._siteMapDataSource.StartingNodeUrl = startingNodeUrl;
				this._siteMapDataSource.SiteMapProvider = siteMapProvider;
			}
			if (siteMapNodeCollection != null && siteMapNodeCollection.Count == 0)
			{
				isSampleData = true;
				return DesignTimeData.GetDesignTimeDataSource(DesignTimeData.CreateDummyDataBoundDataTable(), minimumRows);
			}
			return siteMapNodeCollection;
		}

		// Token: 0x04000559 RID: 1369
		private static readonly SiteMapDataSourceDesigner.SiteMapDataSourceViewSchema _siteMapViewSchema = new SiteMapDataSourceDesigner.SiteMapDataSourceViewSchema();

		// Token: 0x0400055A RID: 1370
		private SiteMapDataSourceDesigner _owner;

		// Token: 0x0400055B RID: 1371
		private SiteMapDataSource _siteMapDataSource;
	}
}
