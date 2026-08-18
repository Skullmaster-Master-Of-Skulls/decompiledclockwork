using System;
using System.Globalization;
using System.IO;
using System.Text;
using System.Web.UI;
using System.Web.UI.Design;
using AjaxControlToolkit.HtmlEditor;

namespace AjaxControlToolkit.Design
{
	// Token: 0x020000D4 RID: 212
	public class HtmlEditorDesigner : DesignerWithMapPath
	{
		// Token: 0x1700026F RID: 623
		// (get) Token: 0x06000608 RID: 1544 RVA: 0x000101D5 File Offset: 0x0000E3D5
		private Editor HtmlEditor
		{
			get
			{
				return (Editor)base.Component;
			}
		}

		// Token: 0x06000609 RID: 1545 RVA: 0x000101E4 File Offset: 0x0000E3E4
		public override string GetDesignTimeHtml(DesignerRegionCollection regions)
		{
			StringBuilder stringBuilder = new StringBuilder(1024);
			StringWriter writer = new StringWriter(stringBuilder, CultureInfo.InvariantCulture);
			HtmlTextWriter htmlTextWriter = new HtmlTextWriter(writer);
			htmlTextWriter.AddAttribute(HtmlTextWriterAttribute.Rel, "stylesheet");
			htmlTextWriter.AddAttribute(HtmlTextWriterAttribute.Href, this.HtmlEditor.Page.ClientScript.GetWebResourceUrl(typeof(Editor), "HtmlEditor.Editor.css"));
			htmlTextWriter.RenderBeginTag(HtmlTextWriterTag.Link);
			htmlTextWriter.RenderEndTag();
			this.HtmlEditor.CreateChilds(this);
			this.HtmlEditor.RenderControl(htmlTextWriter);
			return stringBuilder.ToString();
		}
	}
}
