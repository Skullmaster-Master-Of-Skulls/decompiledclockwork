using System;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Web.UI.WebControls;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x02000105 RID: 261
	internal sealed class SiteMapPathAutoFormat : BaseAutoFormat<SiteMapPath>
	{
		// Token: 0x06000937 RID: 2359 RVA: 0x000350A7 File Offset: 0x000332A7
		public SiteMapPathAutoFormat(string schemeName, string schemes) : base(schemeName, schemes)
		{
			base.Style.Width = 400;
			base.Style.Height = 100;
		}

		// Token: 0x06000938 RID: 2360 RVA: 0x000350D8 File Offset: 0x000332D8
		protected override void Apply(SiteMapPath siteMapPath)
		{
			siteMapPath.Font.Name = this._fontName;
			siteMapPath.Font.Size = this._fontSize;
			siteMapPath.Font.ClearDefaults();
			siteMapPath.NodeStyle.Font.Bold = this._nodeStyleFontBold;
			siteMapPath.NodeStyle.ForeColor = this._nodeStyleForeColor;
			siteMapPath.NodeStyle.Font.ClearDefaults();
			siteMapPath.RootNodeStyle.Font.Bold = this._rootNodeStyleFontBold;
			siteMapPath.RootNodeStyle.ForeColor = this._rootNodeStyleForeColor;
			siteMapPath.RootNodeStyle.Font.ClearDefaults();
			siteMapPath.CurrentNodeStyle.ForeColor = this._currentNodeStyleForeColor;
			siteMapPath.PathSeparatorStyle.Font.Bold = this._pathSeparatorStyleFontBold;
			siteMapPath.PathSeparatorStyle.ForeColor = this._pathSeparatorStyleForeColor;
			siteMapPath.PathSeparatorStyle.Font.ClearDefaults();
			if (this._pathSeparator != null && this._pathSeparator.Length == 0)
			{
				this._pathSeparator = null;
			}
			siteMapPath.PathSeparator = this._pathSeparator;
		}

		// Token: 0x06000939 RID: 2361 RVA: 0x000351F0 File Offset: 0x000333F0
		protected override void Initialize(DataRow schemeData)
		{
			if (schemeData == null)
			{
				return;
			}
			this._fontName = BaseAutoFormat<SiteMapPath>.GetStringProperty("FontName", schemeData);
			this._fontSize = new FontUnit(BaseAutoFormat<SiteMapPath>.GetStringProperty("FontSize", schemeData), CultureInfo.InvariantCulture);
			this._pathSeparator = BaseAutoFormat<SiteMapPath>.GetStringProperty("PathSeparator", schemeData);
			this._nodeStyleFontBold = BaseAutoFormat<SiteMapPath>.GetBooleanProperty("NodeStyleFontBold", schemeData);
			this._nodeStyleForeColor = ColorTranslator.FromHtml(BaseAutoFormat<SiteMapPath>.GetStringProperty("NodeStyleForeColor", schemeData));
			this._rootNodeStyleFontBold = BaseAutoFormat<SiteMapPath>.GetBooleanProperty("RootNodeStyleFontBold", schemeData);
			this._rootNodeStyleForeColor = ColorTranslator.FromHtml(BaseAutoFormat<SiteMapPath>.GetStringProperty("RootNodeStyleForeColor", schemeData));
			this._currentNodeStyleForeColor = ColorTranslator.FromHtml(BaseAutoFormat<SiteMapPath>.GetStringProperty("CurrentNodeStyleForeColor", schemeData));
			this._pathSeparatorStyleFontBold = BaseAutoFormat<SiteMapPath>.GetBooleanProperty("PathSeparatorStyleFontBold", schemeData);
			this._pathSeparatorStyleForeColor = ColorTranslator.FromHtml(BaseAutoFormat<SiteMapPath>.GetStringProperty("PathSeparatorStyleForeColor", schemeData));
		}

		// Token: 0x04000562 RID: 1378
		private string _fontName;

		// Token: 0x04000563 RID: 1379
		private FontUnit _fontSize;

		// Token: 0x04000564 RID: 1380
		private string _pathSeparator;

		// Token: 0x04000565 RID: 1381
		private bool _nodeStyleFontBold;

		// Token: 0x04000566 RID: 1382
		private Color _nodeStyleForeColor;

		// Token: 0x04000567 RID: 1383
		private bool _rootNodeStyleFontBold;

		// Token: 0x04000568 RID: 1384
		private Color _rootNodeStyleForeColor;

		// Token: 0x04000569 RID: 1385
		private Color _currentNodeStyleForeColor;

		// Token: 0x0400056A RID: 1386
		private bool _pathSeparatorStyleFontBold;

		// Token: 0x0400056B RID: 1387
		private Color _pathSeparatorStyleForeColor;
	}
}
