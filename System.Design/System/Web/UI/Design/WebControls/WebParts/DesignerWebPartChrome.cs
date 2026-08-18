using System;
using System.Design;
using System.Globalization;
using System.IO;
using System.Security.Permissions;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace System.Web.UI.Design.WebControls.WebParts
{
	// Token: 0x0200053B RID: 1339
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	internal class DesignerWebPartChrome : WebPartChrome
	{
		// Token: 0x06002F53 RID: 12115 RVA: 0x0010E182 File Offset: 0x0010D182
		public DesignerWebPartChrome(WebPartZoneBase zone) : base(zone, null)
		{
		}

		// Token: 0x06002F54 RID: 12116 RVA: 0x0010E18C File Offset: 0x0010D18C
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

		// Token: 0x06002F55 RID: 12117 RVA: 0x0010E290 File Offset: 0x0010D290
		protected override void RenderPartContents(HtmlTextWriter writer, WebPart webPart)
		{
			writer.Write(this._partViewRendering.Content);
		}

		// Token: 0x0400203E RID: 8254
		private ViewRendering _partViewRendering;
	}
}
