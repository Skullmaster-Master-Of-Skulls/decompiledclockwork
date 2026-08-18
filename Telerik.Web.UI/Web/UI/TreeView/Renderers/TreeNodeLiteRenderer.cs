using System;
using System.Web;
using System.Web.UI;

namespace Telerik.Web.UI.TreeView.Renderers
{
	// Token: 0x0200097E RID: 2430
	internal class TreeNodeLiteRenderer : TreeNodeRenderBase
	{
		// Token: 0x06005C5B RID: 23643 RVA: 0x00119D6B File Offset: 0x00117F6B
		public TreeNodeLiteRenderer(RadTreeNode owner) : base(owner)
		{
		}

		// Token: 0x06005C5C RID: 23644 RVA: 0x00119D74 File Offset: 0x00117F74
		protected override void RenderWrap(HtmlTextWriter writer)
		{
			string text = "rtOut";
			if (!string.IsNullOrEmpty(base.Owner.ContentCssClass))
			{
				text = text + " " + base.Owner.ContentCssClass;
			}
			if (base.Owner.Selected)
			{
				text += " rtSelected";
				if (!string.IsNullOrEmpty(base.Owner.SelectedCssClass))
				{
					text = text + " " + base.Owner.SelectedCssClass;
				}
			}
			if (!base.Owner.Enabled || !base.TreeView.IsControlEnabled)
			{
				text += " rtDisabled";
				if (!string.IsNullOrEmpty(base.Owner.DisabledCssClass))
				{
					text = text + " " + base.Owner.DisabledCssClass;
				}
			}
			writer.AddAttribute(HtmlTextWriterAttribute.Class, text);
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			this.RenderWrapContents(writer);
			writer.RenderEndTag();
		}

		// Token: 0x06005C5D RID: 23645 RVA: 0x00119E60 File Offset: 0x00118060
		protected void RenderWrapContents(HtmlTextWriter writer)
		{
			bool flag = base.TreeView.CheckBoxes && base.Owner.Checkable;
			if (base.Owner.HasVisibleChildren || base.Owner.ExpandMode != TreeNodeExpandMode.ClientSide)
			{
				base.RenderExpand(writer);
			}
			if (flag)
			{
				base.RenderCheckBox(writer);
			}
			string cssClass = base.Owner.CssClass;
			base.Owner.CssClass = ("rtIn " + base.Owner.CssClass).Trim();
			if (!base.Owner.Enabled || !base.TreeView.IsControlEnabled)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Disabled, "disabled");
			}
			base.Owner.AddAttributes(writer);
			base.Owner.CssClass = cssClass;
			if (base.Owner.Templated)
			{
				if (base.Owner.Controls.IsReadOnly)
				{
					base.Owner.CallBaseRenderChildren(writer);
					return;
				}
				base.RenderTemplate(writer);
				return;
			}
			else
			{
				if (!string.IsNullOrEmpty(base.Owner.NavigateUrl))
				{
					this.RenderLink(writer);
					return;
				}
				this.RenderSimpleNode(writer);
				return;
			}
		}

		// Token: 0x06005C5E RID: 23646 RVA: 0x00119F79 File Offset: 0x00118179
		protected void RenderSimpleNode(HtmlTextWriter writer)
		{
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			this.RenderNodeContents(writer);
			writer.RenderEndTag();
		}

		// Token: 0x06005C5F RID: 23647 RVA: 0x00119F90 File Offset: 0x00118190
		protected void RenderLink(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Href, base.Owner.ResolveClientUrl(base.Owner.NavigateUrl));
			if (!string.IsNullOrEmpty(base.Owner.Target))
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Target, base.Owner.Target);
			}
			writer.RenderBeginTag(HtmlTextWriterTag.A);
			this.RenderNodeContents(writer);
			writer.RenderEndTag();
		}

		// Token: 0x06005C60 RID: 23648 RVA: 0x00119FF4 File Offset: 0x001181F4
		protected void RenderNodeContents(HtmlTextWriter writer)
		{
			if (!string.IsNullOrEmpty(base.Owner.CurrentImageUrl))
			{
				base.RenderImage(writer);
			}
			this.RenderText(writer);
		}

		// Token: 0x06005C61 RID: 23649 RVA: 0x0011A018 File Offset: 0x00118218
		protected void RenderText(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rtText");
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			writer.Write(base.TreeView.EnableNodeTextHtmlEncoding ? HttpUtility.HtmlEncode(base.Owner.Text) : base.Owner.Text);
			writer.RenderEndTag();
		}

		// Token: 0x06005C62 RID: 23650 RVA: 0x0011A070 File Offset: 0x00118270
		protected override void RenderThreeStateCheckBox(HtmlTextWriter writer)
		{
			string value = string.Format("{0} rt{1}", "rtChkTristate", base.Owner.CheckState);
			writer.AddAttribute(HtmlTextWriterAttribute.Class, value);
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			writer.RenderEndTag();
		}
	}
}
