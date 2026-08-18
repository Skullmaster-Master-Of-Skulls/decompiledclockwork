using System;
using System.Web.UI;
using Telerik.Web.UI.Renderers;

namespace Telerik.Web.UI.Menu.Renderers
{
	// Token: 0x020005DC RID: 1500
	public class MenuRendererBase : RendererBase
	{
		// Token: 0x06003692 RID: 13970 RVA: 0x000B497F File Offset: 0x000B2B7F
		public MenuRendererBase(RadMenu owner)
		{
			this.Owner = owner;
		}

		// Token: 0x170011E9 RID: 4585
		// (get) Token: 0x06003693 RID: 13971 RVA: 0x000B498E File Offset: 0x000B2B8E
		// (set) Token: 0x06003694 RID: 13972 RVA: 0x000B4996 File Offset: 0x000B2B96
		protected RadMenu Owner { get; set; }

		// Token: 0x06003695 RID: 13973 RVA: 0x000B499F File Offset: 0x000B2B9F
		public override void AddAttributesToRender(HtmlTextWriter writer)
		{
			this.Owner.CallBaseAddAttributesToRender(writer);
		}

		// Token: 0x06003696 RID: 13974 RVA: 0x000B49B0 File Offset: 0x000B2BB0
		protected void RenderDesignTimeHtml(HtmlTextWriter writer)
		{
			writer.Write(SkinRegistrar.GetDesignTimeStyleSheet(this.Owner));
			if (this.Owner.GetType() == typeof(RadMenu))
			{
				writer.Write("<style type=\"text/css\">");
				writer.Write(" .rmLink { width: auto !important; }\r\n\t\t\t\t\t\t\t\t.rmItem { float: left !important; }");
				if (!string.IsNullOrEmpty(this.Owner.Width.ToString()))
				{
					string text = string.IsNullOrEmpty(this.Owner.Height.ToString()) ? "24px" : this.Owner.Height.ToString();
					writer.Write(string.Concat(new string[]
					{
						" .RadMenu { width: ",
						this.Owner.Width.ToString(),
						" !important; }.RadMenu { height: ",
						text,
						" !important; }"
					}));
				}
				writer.Write("</style>");
			}
		}

		// Token: 0x06003697 RID: 13975 RVA: 0x000B4AC8 File Offset: 0x000B2CC8
		protected internal void RenderRootGroup(HtmlTextWriter writer, Action<RadMenuItemCollection> action = null)
		{
			writer.RenderBeginTag(HtmlTextWriterTag.Ul);
			if (action != null)
			{
				action(this.Owner.Items);
			}
			foreach (object obj in this.Owner.Items)
			{
				RadMenuItem radMenuItem = (RadMenuItem)obj;
				radMenuItem.RenderControl(writer);
			}
			writer.RenderEndTag();
		}
	}
}
