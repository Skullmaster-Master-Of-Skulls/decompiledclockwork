using System;
using System.Collections;
using System.Collections.Specialized;
using System.Design;
using System.Globalization;
using System.IO;
using System.Security.Permissions;
using System.Web.UI.WebControls.WebParts;

namespace System.Web.UI.Design.WebControls.WebParts
{
	// Token: 0x02000146 RID: 326
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	internal class DesignerCatalogPartChrome : CatalogPartChrome
	{
		// Token: 0x06000BB0 RID: 2992 RVA: 0x0004AD3C File Offset: 0x00048F3C
		public DesignerCatalogPartChrome(CatalogZone zone) : base(zone)
		{
		}

		// Token: 0x06000BB1 RID: 2993 RVA: 0x0004AD48 File Offset: 0x00048F48
		public ViewRendering GetViewRendering(Control control)
		{
			CatalogPart catalogPart = control as CatalogPart;
			if (catalogPart == null)
			{
				string content = ControlDesigner.CreateErrorDesignTimeHtml(SR.GetString("CatalogZoneDesigner_OnlyCatalogParts"), null, control);
				return new ViewRendering(content, new DesignerRegionCollection());
			}
			DesignerRegionCollection regions;
			string content2;
			try
			{
				IDictionary dictionary = new HybridDictionary(1);
				dictionary["Zone"] = base.Zone;
				((IControlDesignerAccessor)catalogPart).SetDesignModeState(dictionary);
				this._partViewRendering = ControlDesigner.GetViewRendering(catalogPart);
				regions = this._partViewRendering.Regions;
				StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture);
				this.RenderCatalogPart(new DesignTimeHtmlTextWriter(stringWriter), (CatalogPart)PartDesigner.GetViewControl(catalogPart));
				content2 = stringWriter.ToString();
			}
			catch (Exception e)
			{
				content2 = ControlDesigner.CreateErrorDesignTimeHtml(SR.GetString("ControlDesigner_UnhandledException"), e, control);
				regions = new DesignerRegionCollection();
			}
			return new ViewRendering(content2, regions);
		}

		// Token: 0x06000BB2 RID: 2994 RVA: 0x0004AE1C File Offset: 0x0004901C
		protected override void RenderPartContents(HtmlTextWriter writer, CatalogPart catalogPart)
		{
			writer.Write(this._partViewRendering.Content);
		}

		// Token: 0x0400070C RID: 1804
		private ViewRendering _partViewRendering;
	}
}
