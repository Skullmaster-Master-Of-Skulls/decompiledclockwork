using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Licensing;

namespace Telerik.Web.UI
{
	// Token: 0x02001AB3 RID: 6835
	[ToolboxData("<{0}:RadSiteMapDataSource Runat=\"server\" />")]
	[LicenseProvider(typeof(TelerikLicenseProvider))]
	[TelerikToolboxCategory("Navigation")]
	[ToolboxBitmap(typeof(RadSiteMapDataSource), "Telerik.Web.UI.SiteMapDataSource.png")]
	[Designer("Telerik.Web.Design.RadSiteMapDataSourceDesigner, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4")]
	public class RadSiteMapDataSource : SiteMapDataSource
	{
		// Token: 0x17005041 RID: 20545
		// (get) Token: 0x06010840 RID: 67648 RVA: 0x003B0431 File Offset: 0x003AE631
		// (set) Token: 0x06010841 RID: 67649 RVA: 0x003B043C File Offset: 0x003AE63C
		[Category("Data")]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.SiteMapFileUrlEditor", "System.Drawing.Design.UITypeEditor")]
		[UrlProperty("*.sitemap")]
		[Description("The relative path to the .sitemap file from which to load SiteMap data")]
		public string SiteMapFile
		{
			get
			{
				return this._siteMapFile;
			}
			set
			{
				if (!base.DesignMode)
				{
					if (string.IsNullOrEmpty(value) && !string.IsNullOrEmpty(this._siteMapFile))
					{
						base.Provider = null;
					}
					else if (value != this._siteMapFile)
					{
						XmlSiteMapProvider xmlSiteMapProvider = new XmlSiteMapProvider();
						xmlSiteMapProvider.Initialize("TemporaryRunTimeXmlSiteMapProvider", new NameValueCollection
						{
							{
								"siteMapFile",
								value
							}
						});
						base.Provider = xmlSiteMapProvider;
					}
				}
				this._siteMapFile = value;
			}
		}

		// Token: 0x040049F6 RID: 18934
		private string _siteMapFile;
	}
}
