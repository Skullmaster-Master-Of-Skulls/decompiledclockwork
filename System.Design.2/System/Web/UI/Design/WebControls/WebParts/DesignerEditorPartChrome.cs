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
	// Token: 0x02000147 RID: 327
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	internal class DesignerEditorPartChrome : EditorPartChrome
	{
		// Token: 0x06000BB3 RID: 2995 RVA: 0x0004AE2F File Offset: 0x0004902F
		public DesignerEditorPartChrome(EditorZone zone) : base(zone)
		{
		}

		// Token: 0x06000BB4 RID: 2996 RVA: 0x0004AE38 File Offset: 0x00049038
		public ViewRendering GetViewRendering(Control control)
		{
			EditorPart editorPart = control as EditorPart;
			if (editorPart == null)
			{
				string content = ControlDesigner.CreateErrorDesignTimeHtml(SR.GetString("EditorZoneDesigner_OnlyEditorParts"), null, control);
				return new ViewRendering(content, new DesignerRegionCollection());
			}
			DesignerRegionCollection regions;
			string content2;
			try
			{
				IDictionary dictionary = new HybridDictionary(1);
				dictionary["Zone"] = base.Zone;
				((IControlDesignerAccessor)editorPart).SetDesignModeState(dictionary);
				this._partViewRendering = ControlDesigner.GetViewRendering(editorPart);
				regions = this._partViewRendering.Regions;
				StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture);
				this.RenderEditorPart(new DesignTimeHtmlTextWriter(stringWriter), (EditorPart)PartDesigner.GetViewControl(editorPart));
				content2 = stringWriter.ToString();
			}
			catch (Exception e)
			{
				content2 = ControlDesigner.CreateErrorDesignTimeHtml(SR.GetString("ControlDesigner_UnhandledException"), e, control);
				regions = new DesignerRegionCollection();
			}
			return new ViewRendering(content2, regions);
		}

		// Token: 0x06000BB5 RID: 2997 RVA: 0x0004AF0C File Offset: 0x0004910C
		protected override void RenderPartContents(HtmlTextWriter writer, EditorPart editorPart)
		{
			writer.Write(this._partViewRendering.Content);
		}

		// Token: 0x0400070D RID: 1805
		private ViewRendering _partViewRendering;
	}
}
