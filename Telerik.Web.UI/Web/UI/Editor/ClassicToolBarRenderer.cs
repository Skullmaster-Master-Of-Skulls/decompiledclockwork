using System;
using System.Web.UI;

namespace Telerik.Web.UI.Editor
{
	// Token: 0x020002C6 RID: 710
	internal class ClassicToolBarRenderer : EditorToolBarRendererBase
	{
		// Token: 0x060018B6 RID: 6326 RVA: 0x0005232A File Offset: 0x0005052A
		public ClassicToolBarRenderer(EditorToolBar owner) : base(owner)
		{
		}

		// Token: 0x17000864 RID: 2148
		// (get) Token: 0x060018B7 RID: 6327 RVA: 0x00052333 File Offset: 0x00050533
		public override string CssClassFormatString
		{
			get
			{
				return "reToolbar {0}";
			}
		}

		// Token: 0x060018B8 RID: 6328 RVA: 0x0005233A File Offset: 0x0005053A
		public override void RenderBeginTag(HtmlTextWriter writer)
		{
			base.RenderBeginTag(writer);
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "reGrip grip_first");
			writer.RenderBeginTag(HtmlTextWriterTag.Li);
			writer.Write("&nbsp;");
			writer.RenderEndTag();
		}

		// Token: 0x060018B9 RID: 6329 RVA: 0x00052369 File Offset: 0x00050569
		public override void RenderEndTag(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "reGrip grip_last");
			writer.RenderBeginTag(HtmlTextWriterTag.Li);
			writer.Write("&nbsp;");
			writer.RenderEndTag();
			base.RenderEndTag(writer);
		}
	}
}
