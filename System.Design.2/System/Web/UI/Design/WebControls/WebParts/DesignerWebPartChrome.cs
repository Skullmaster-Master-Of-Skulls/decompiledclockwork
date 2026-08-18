using System;
using System.Design;
using System.Globalization;
using System.IO;
using System.Security.Permissions;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace System.Web.UI.Design.WebControls.WebParts
{
	// Token: 0x02000149 RID: 329
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	internal class DesignerWebPartChrome : WebPartChrome
	{
		// Token: 0x06000BB8 RID: 3000 RVA: 0x0004AF28 File Offset: 0x00049128
		public DesignerWebPartChrome(WebPartZoneBase zone) : base(zone, null)
		{
		}

		// Token: 0x06000BB9 RID: 3001 RVA: 0x0004AF34 File Offset: 0x00049134
		public ViewRendering GetViewRendering(Control control)
		{
			DesignerRegionCollection regions;
			string value;
			try
			{
				this._partViewRendering = ControlDesigner.GetViewRendering(control);
				regions = this._partViewRendering.Regions;
				WebPart webPart = control as WebPart;
				if (webPart == null)
				{
					webPart = new DesignerGenericWebPart(PartDesigner.GetViewControl(control));
				}
				StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture);
				this.RenderWebPart(new DesignTimeHtmlTextWriter(stringWriter), (WebPart)PartDesigner.GetViewControl(webPart));
				value = stringWriter.ToString();
			}
			catch (Exception e)
			{
				value = ControlDesigner.CreateErrorDesignTimeHtml(SR.GetString("ControlDesigner_UnhandledException"), e, control);
				regions = new DesignerRegionCollection();
			}
			StringWriter stringWriter2 = new StringWriter(CultureInfo.InvariantCulture);
			DesignTimeHtmlTextWriter designTimeHtmlTextWriter = new DesignTimeHtmlTextWriter(stringWriter2);
			bool flag = base.Zone.LayoutOrientation == Orientation.Horizontal;
			if (flag)
			{
				designTimeHtmlTextWriter.AddStyleAttribute("display", "inline-block");
				designTimeHtmlTextWriter.AddStyleAttribute(HtmlTextWriterStyle.Height, "100%");
				designTimeHtmlTextWriter.RenderBeginTag(HtmlTextWriterTag.Span);
			}
			designTimeHtmlTextWriter.Write(value);
			if (flag)
			{
				designTimeHtmlTextWriter.RenderEndTag();
			}
			return new ViewRendering(stringWriter2.ToString(), regions);
		}

		// Token: 0x06000BBA RID: 3002 RVA: 0x0004B038 File Offset: 0x00049238
		protected override void RenderPartContents(HtmlTextWriter writer, WebPart webPart)
		{
			writer.Write(this._partViewRendering.Content);
		}

		// Token: 0x0400070E RID: 1806
		private ViewRendering _partViewRendering;
	}
}
