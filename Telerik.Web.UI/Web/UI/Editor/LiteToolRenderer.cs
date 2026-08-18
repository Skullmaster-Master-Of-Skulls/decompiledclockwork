using System;
using System.Web.UI;

namespace Telerik.Web.UI.Editor
{
	// Token: 0x020002D1 RID: 721
	internal class LiteToolRenderer : ToolRendererBase
	{
		// Token: 0x0600191B RID: 6427 RVA: 0x00052CB9 File Offset: 0x00050EB9
		public LiteToolRenderer(EditorTool owner) : base(owner)
		{
		}

		// Token: 0x0600191C RID: 6428 RVA: 0x00052CC2 File Offset: 0x00050EC2
		public override void RenderToolIcon(HtmlTextWriter writer)
		{
		}

		// Token: 0x0600191D RID: 6429 RVA: 0x00052CC4 File Offset: 0x00050EC4
		public override void RenderToolText(HtmlTextWriter writer)
		{
		}

		// Token: 0x0600191E RID: 6430 RVA: 0x00052CC6 File Offset: 0x00050EC6
		public override void RenderBeginTag(HtmlTextWriter writer)
		{
			base.RenderBeginTag(writer);
			this.RenderText(writer);
		}

		// Token: 0x0600191F RID: 6431 RVA: 0x00052CD6 File Offset: 0x00050ED6
		public virtual void RenderText(HtmlTextWriter writer)
		{
			if (base.Owner.ShowText)
			{
				writer.Write(base.Owner.Text);
			}
		}

		// Token: 0x1700087B RID: 2171
		// (get) Token: 0x06001920 RID: 6432 RVA: 0x00052CF6 File Offset: 0x00050EF6
		public override string CssClassString
		{
			get
			{
				return this.GetCssClassString();
			}
		}

		// Token: 0x1700087C RID: 2172
		// (get) Token: 0x06001921 RID: 6433 RVA: 0x00052CFE File Offset: 0x00050EFE
		public override string CssClassFormatString
		{
			get
			{
				return "{0} re{1}{2}{3}";
			}
		}

		// Token: 0x06001922 RID: 6434 RVA: 0x00052D08 File Offset: 0x00050F08
		public override string GetCssClassString()
		{
			return string.Format(this.CssClassFormatString, new object[]
			{
				"reTool",
				base.Owner.Name,
				base.Owner.ShowText ? " reToolText" : "",
				base.Owner.ShowIcon ? " reToolIcon" : ""
			});
		}
	}
}
