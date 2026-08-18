using System;
using System.Web;
using System.Web.UI;

namespace Telerik.Web.UI.TreeView.Renderers
{
	// Token: 0x0200097D RID: 2429
	internal class TreeNodeClassicRenderer : TreeNodeRenderBase
	{
		// Token: 0x06005C55 RID: 23637 RVA: 0x001199F8 File Offset: 0x00117BF8
		public TreeNodeClassicRenderer(RadTreeNode owner) : base(owner)
		{
		}

		// Token: 0x06005C56 RID: 23638 RVA: 0x00119A04 File Offset: 0x00117C04
		protected override void RenderWrap(HtmlTextWriter writer)
		{
			string text = "rt";
			if (base.IsLastVisibleChild && !base.FirstNodeInTreeView)
			{
				text += "Bot";
			}
			else if (base.IsFirstVisibleChild)
			{
				text += "Top";
			}
			else
			{
				text += "Mid";
			}
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
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rtSp");
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			writer.RenderEndTag();
			this.RenderWrapContents(writer);
			writer.RenderEndTag();
		}

		// Token: 0x06005C57 RID: 23639 RVA: 0x00119B4C File Offset: 0x00117D4C
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
			if (!string.IsNullOrEmpty(base.Owner.CurrentImageUrl))
			{
				base.RenderImage(writer);
			}
			string cssClass = base.Owner.CssClass;
			base.Owner.CssClass = ("rtIn " + base.Owner.CssClass).Trim();
			if (!base.Owner.Enabled || !base.TreeView.IsControlEnabled)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Disabled, "disabled");
			}
			base.Owner.AddAttributes(writer);
			base.Owner.CssClass = cssClass;
			if (!base.Owner.Templated)
			{
				if (!string.IsNullOrEmpty(base.Owner.NavigateUrl))
				{
					this.RenderLink(writer);
				}
				else
				{
					this.RenderText(writer);
				}
				if (flag && !base.TreeView.TriStateCheckBoxes)
				{
					writer.RenderEndTag();
				}
				return;
			}
			if (flag && !base.TreeView.TriStateCheckBoxes)
			{
				writer.RenderEndTag();
			}
			if (base.Owner.Controls.IsReadOnly)
			{
				base.Owner.CallBaseRenderChildren(writer);
				return;
			}
			base.RenderTemplate(writer);
		}

		// Token: 0x06005C58 RID: 23640 RVA: 0x00119CAB File Offset: 0x00117EAB
		protected void RenderText(HtmlTextWriter writer)
		{
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			writer.Write(base.TreeView.EnableNodeTextHtmlEncoding ? HttpUtility.HtmlEncode(base.Owner.Text) : base.Owner.Text);
			writer.RenderEndTag();
		}

		// Token: 0x06005C59 RID: 23641 RVA: 0x00119CEC File Offset: 0x00117EEC
		protected void RenderLink(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Href, base.Owner.ResolveClientUrl(base.Owner.NavigateUrl));
			if (!string.IsNullOrEmpty(base.Owner.Target))
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Target, base.Owner.Target);
			}
			writer.RenderBeginTag(HtmlTextWriterTag.A);
			writer.Write(base.Owner.Text);
			writer.RenderEndTag();
		}

		// Token: 0x06005C5A RID: 23642 RVA: 0x00119D5A File Offset: 0x00117F5A
		protected override void RenderDefaultCheckBox(HtmlTextWriter writer)
		{
			writer.RenderBeginTag(HtmlTextWriterTag.Label);
			base.RenderDefaultCheckBox(writer);
		}
	}
}
