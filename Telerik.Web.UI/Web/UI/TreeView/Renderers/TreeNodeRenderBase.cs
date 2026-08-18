using System;
using System.Web.UI;
using Telerik.Web.UI.Renderers;

namespace Telerik.Web.UI.TreeView.Renderers
{
	// Token: 0x0200097C RID: 2428
	public abstract class TreeNodeRenderBase : RendererBase
	{
		// Token: 0x06005C42 RID: 23618 RVA: 0x001195FD File Offset: 0x001177FD
		public TreeNodeRenderBase(RadTreeNode owner)
		{
			this.Owner = owner;
		}

		// Token: 0x17001E66 RID: 7782
		// (get) Token: 0x06005C43 RID: 23619 RVA: 0x00119613 File Offset: 0x00117813
		// (set) Token: 0x06005C44 RID: 23620 RVA: 0x0011961B File Offset: 0x0011781B
		protected RadTreeNode Owner { get; set; }

		// Token: 0x17001E67 RID: 7783
		// (get) Token: 0x06005C45 RID: 23621 RVA: 0x00119624 File Offset: 0x00117824
		protected RadTreeView TreeView
		{
			get
			{
				return this.Owner.TreeView;
			}
		}

		// Token: 0x17001E68 RID: 7784
		// (get) Token: 0x06005C46 RID: 23622 RVA: 0x00119631 File Offset: 0x00117831
		public override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Li;
			}
		}

		// Token: 0x06005C47 RID: 23623 RVA: 0x00119635 File Offset: 0x00117835
		public virtual void Render(int index, HtmlTextWriter writer)
		{
			this._cachedIndex = index;
			this.Owner.RenderControl(writer);
		}

		// Token: 0x06005C48 RID: 23624 RVA: 0x0011964C File Offset: 0x0011784C
		public override void AddAttributesToRender(HtmlTextWriter writer)
		{
			string text = "rtLI";
			if (this.FirstNodeInTreeView)
			{
				text += " rtFirst";
			}
			if (this.IsLastVisibleChild)
			{
				text += " rtLast";
			}
			writer.AddAttribute(HtmlTextWriterAttribute.Class, text);
		}

		// Token: 0x06005C49 RID: 23625 RVA: 0x00119690 File Offset: 0x00117890
		public override void RenderContents(HtmlTextWriter writer)
		{
			this.RenderWrap(writer);
			this.RenderChildList(writer);
		}

		// Token: 0x17001E69 RID: 7785
		// (get) Token: 0x06005C4A RID: 23626 RVA: 0x001196A0 File Offset: 0x001178A0
		protected bool FirstNodeInTreeView
		{
			get
			{
				return this.IsFirstVisibleChild && this.Owner.Owner is RadTreeView;
			}
		}

		// Token: 0x17001E6A RID: 7786
		// (get) Token: 0x06005C4B RID: 23627 RVA: 0x001196C0 File Offset: 0x001178C0
		protected bool IsLastVisibleChild
		{
			get
			{
				RadTreeNodeCollection nodes = this.Owner.Owner.Nodes;
				for (int i = this._cachedIndex + 1; i < nodes.Count; i++)
				{
					if (nodes[i].Visible)
					{
						return false;
					}
				}
				return true;
			}
		}

		// Token: 0x17001E6B RID: 7787
		// (get) Token: 0x06005C4C RID: 23628 RVA: 0x00119708 File Offset: 0x00117908
		protected bool IsFirstVisibleChild
		{
			get
			{
				for (int i = 0; i < this._cachedIndex; i++)
				{
					if (this.Owner.Owner.Nodes[i].Visible)
					{
						return false;
					}
				}
				return true;
			}
		}

		// Token: 0x06005C4D RID: 23629
		protected abstract void RenderWrap(HtmlTextWriter writer);

		// Token: 0x06005C4E RID: 23630 RVA: 0x00119748 File Offset: 0x00117948
		protected void RenderChildList(HtmlTextWriter writer)
		{
			if (this.TreeView.InDesignMode && !this.Owner.Expanded)
			{
				return;
			}
			if (!this.Owner.ShouldRenderChildren)
			{
				return;
			}
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rtUL");
			if (!this.Owner.Expanded)
			{
				writer.AddStyleAttribute(HtmlTextWriterStyle.Display, "none");
			}
			writer.RenderBeginTag(HtmlTextWriterTag.Ul);
			for (int i = 0; i < this.Owner.Nodes.Count; i++)
			{
				this.Owner.Nodes[i].Render(i, writer);
			}
			writer.RenderEndTag();
		}

		// Token: 0x06005C4F RID: 23631 RVA: 0x001197E6 File Offset: 0x001179E6
		protected void RenderExpand(HtmlTextWriter writer)
		{
			if (this.Owner.Expanded)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Class, "rtMinus");
			}
			else
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Class, "rtPlus");
			}
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			writer.RenderEndTag();
		}

		// Token: 0x06005C50 RID: 23632 RVA: 0x0011981F File Offset: 0x00117A1F
		protected void RenderCheckBox(HtmlTextWriter writer)
		{
			if (this.TreeView.TriStateCheckBoxes)
			{
				this.RenderThreeStateCheckBox(writer);
				return;
			}
			this.RenderDefaultCheckBox(writer);
		}

		// Token: 0x06005C51 RID: 23633 RVA: 0x0011983D File Offset: 0x00117A3D
		protected virtual void RenderThreeStateCheckBox(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rt" + this.Owner.CheckState);
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			writer.RenderEndTag();
		}

		// Token: 0x06005C52 RID: 23634 RVA: 0x00119870 File Offset: 0x00117A70
		protected virtual void RenderDefaultCheckBox(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Type, "checkbox");
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rtChk");
			if (this.Owner.Checked)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Checked, "checked");
			}
			if (!this.Owner.Enabled || !this.TreeView.IsControlEnabled)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Disabled, "disabled");
			}
			writer.RenderBeginTag(HtmlTextWriterTag.Input);
			writer.RenderEndTag();
		}

		// Token: 0x06005C53 RID: 23635 RVA: 0x001198E8 File Offset: 0x00117AE8
		protected void RenderImage(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Src, this.Owner.ResolveUrl(this.Owner.CurrentImageUrl));
			if (!string.IsNullOrEmpty(this.Owner.LongDesc))
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Longdesc, this.Owner.LongDesc);
			}
			writer.AddAttribute(HtmlTextWriterAttribute.Alt, this.Owner.ToolTip);
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rtImg");
			writer.RenderBeginTag(HtmlTextWriterTag.Img);
			writer.RenderEndTag();
		}

		// Token: 0x06005C54 RID: 23636 RVA: 0x00119968 File Offset: 0x00117B68
		protected void RenderTemplate(HtmlTextWriter writer)
		{
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rtTemplate");
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			foreach (object obj in this.Owner.Controls)
			{
				Control control = (Control)obj;
				if (!(control is RadTreeNode))
				{
					control.RenderControl(writer);
				}
			}
			writer.RenderEndTag();
			writer.RenderEndTag();
		}

		// Token: 0x04001626 RID: 5670
		internal const string NodeCssClass = "rtLI";

		// Token: 0x04001627 RID: 5671
		internal const string FirstNodeCssClass = "rtFirst";

		// Token: 0x04001628 RID: 5672
		internal const string LastChildCssClass = "rtLast";

		// Token: 0x04001629 RID: 5673
		internal const string InnerCssClass = "rtIn";

		// Token: 0x0400162A RID: 5674
		internal const string NodeListCssClass = "rtUL";

		// Token: 0x0400162B RID: 5675
		internal const string CheckBoxCssClass = "rtChk";

		// Token: 0x0400162C RID: 5676
		internal const string TriStateCheckBoxCssClass = "rtChkTristate";

		// Token: 0x0400162D RID: 5677
		internal const string ImageCssClass = "rtImg";

		// Token: 0x0400162E RID: 5678
		private int _cachedIndex = -1;
	}
}
