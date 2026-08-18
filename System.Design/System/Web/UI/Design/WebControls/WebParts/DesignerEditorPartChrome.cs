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
	// Token: 0x02000539 RID: 1337
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	internal class DesignerEditorPartChrome : EditorPartChrome
	{
		// Token: 0x06002F4E RID: 12110 RVA: 0x0010E087 File Offset: 0x0010D087
		public DesignerEditorPartChrome(EditorZone zone) : base(zone)
		{
		}

		// Token: 0x06002F4F RID: 12111 RVA: 0x0010E090 File Offset: 0x0010D090
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

		// Token: 0x06002F50 RID: 12112 RVA: 0x0010E164 File Offset: 0x0010D164
		protected override void RenderPartContents(HtmlTextWriter writer, EditorPart editorPart)
		{
			writer.Write(this._partViewRendering.Content);
		}

		// Token: 0x0400203D RID: 8253
		private ViewRendering _partViewRendering;
	}
}
