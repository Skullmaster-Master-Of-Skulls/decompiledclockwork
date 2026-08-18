using System;
using System.Web.UI;
using Telerik.Web.UI.Renderers;

namespace Telerik.Web.UI.TreeView.Renderers
{
	// Token: 0x0200097A RID: 2426
	internal class TreeViewRenderBase : RendererBase
	{
		// Token: 0x06005C3A RID: 23610 RVA: 0x00119450 File Offset: 0x00117650
		public TreeViewRenderBase(RadTreeView owner)
		{
			this.Owner = owner;
		}

		// Token: 0x17001E65 RID: 7781
		// (get) Token: 0x06005C3B RID: 23611 RVA: 0x0011945F File Offset: 0x0011765F
		// (set) Token: 0x06005C3C RID: 23612 RVA: 0x00119467 File Offset: 0x00117667
		protected RadTreeView Owner { get; set; }

		// Token: 0x06005C3D RID: 23613 RVA: 0x00119470 File Offset: 0x00117670
		public override void AddAttributesToRender(HtmlTextWriter writer)
		{
			if (!this.Owner.Width.IsEmpty || !this.Owner.Height.IsEmpty)
			{
				writer.AddStyleAttribute(HtmlTextWriterStyle.Overflow, "auto");
			}
			this.Owner.CallBaseAddAttributesToRender(writer);
		}

		// Token: 0x06005C3E RID: 23614 RVA: 0x001194C0 File Offset: 0x001176C0
		public override void RenderContents(HtmlTextWriter writer)
		{
			this.RenderTrialMessage(writer);
			if (this.Owner.InDesignMode)
			{
				writer.Write(SkinRegistrar.GetDesignTimeStyleSheet(this.Owner));
			}
			string value = this.Owner.ShowLineImages ? "rtUL rtLines" : "rtUL";
			if (this.Owner.Nodes.Count > 0)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Class, value);
				writer.RenderBeginTag(HtmlTextWriterTag.Ul);
				this.RenderNodes(writer);
				writer.RenderEndTag();
			}
			if (!this.Owner.InDesignMode)
			{
				this.RenderContextMenus(writer);
			}
		}

		// Token: 0x06005C3F RID: 23615 RVA: 0x00119554 File Offset: 0x00117754
		protected void RenderNodes(HtmlTextWriter writer)
		{
			for (int i = 0; i < this.Owner.Nodes.Count; i++)
			{
				this.Owner.Nodes[i].Render(i, writer);
			}
		}

		// Token: 0x06005C40 RID: 23616 RVA: 0x00119594 File Offset: 0x00117794
		protected void RenderContextMenus(HtmlTextWriter writer)
		{
			foreach (object obj in this.Owner.ContextMenus)
			{
				RadTreeViewContextMenu radTreeViewContextMenu = (RadTreeViewContextMenu)obj;
				radTreeViewContextMenu.RenderControl(writer);
			}
		}

		// Token: 0x04001623 RID: 5667
		internal const string NodeListCssClass = "rtUL";

		// Token: 0x04001624 RID: 5668
		internal const string RootListCssClass = "rtUL rtLines";
	}
}
