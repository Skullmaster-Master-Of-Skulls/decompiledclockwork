using System;
using System.Web.UI;
using Telerik.Web.UI.Common.Helpers;

namespace Telerik.Web.UI.PanelBar.Renderers
{
	// Token: 0x0200064F RID: 1615
	internal class PanelItemLiteRenderer : PanelItemRenderBase
	{
		// Token: 0x06003B5F RID: 15199 RVA: 0x000C0FEB File Offset: 0x000BF1EB
		public PanelItemLiteRenderer(RadPanelItem owner) : base(owner)
		{
		}

		// Token: 0x06003B60 RID: 15200 RVA: 0x000C0FF4 File Offset: 0x000BF1F4
		public override void RenderContents(HtmlTextWriter writer)
		{
			if (base.Owner.ShouldRenderLink)
			{
				this.RenderLink(writer);
				if (!string.IsNullOrEmpty(base.Owner.ImageUrl))
				{
					base.RenderImage(writer);
				}
			}
			if (base.Owner.ShouldRenderHeaderTemplate)
			{
				string cssClass = base.Owner.CssClass;
				this.DetermineCssClassToHeader();
				base.Owner.AddAttributes(writer);
				base.Owner.CssClass = cssClass;
				base.Owner.Header.RenderControl(writer);
			}
			else if (!string.IsNullOrEmpty(base.Owner.Text) || !string.IsNullOrEmpty(base.Owner.ImageUrl))
			{
				base.RenderTextPlaceholder(writer);
				base.RenderItemContent(writer);
				writer.RenderEndTag();
			}
			if (base.Owner.ShouldRenderLink)
			{
				this.RenderExpandHandle(writer);
				writer.RenderEndTag();
			}
			if ((!base.Owner.InDesignMode || !base.Owner.PanelBar.RenderEditableRegions) && base.Owner.Templated)
			{
				if (base.Owner._contentTemplateIsSet && (base.Owner.ShouldRenderLink || base.Owner.ShouldRenderHeaderTemplate))
				{
					base.RenderSlideBeginTag(writer);
				}
				base.RenderTemplate(writer);
				if (base.Owner._contentTemplateIsSet && (base.Owner.ShouldRenderLink || base.Owner.ShouldRenderHeaderTemplate))
				{
					writer.RenderEndTag();
				}
			}
			if (base.Owner.InDesignMode && base.Owner.PanelBar.RenderEditableRegions)
			{
				writer.AddAttribute("TemplateRegion", null);
				writer.AddAttribute(HtmlTextWriterAttribute.Class, "rpTemplate");
				writer.AddStyleAttribute("border", "2px dotted buttonface");
				writer.RenderBeginTag("div");
				writer.RenderEndTag();
			}
			if (base.Owner.Items.Count > 0)
			{
				if (!base.Owner.Expanded && base.Owner.InDesignMode)
				{
					return;
				}
				base.RenderSlideBeginTag(writer);
				base.RenderChildItems(writer);
				writer.RenderEndTag();
			}
		}

		// Token: 0x06003B61 RID: 15201 RVA: 0x000C11F4 File Offset: 0x000BF3F4
		private void RenderLink(HtmlTextWriter writer)
		{
			string cssClass = base.Owner.CssClass;
			this.DetermineCssClassToHeader();
			base.Owner.AddAttributes(writer);
			base.Owner.CssClass = cssClass;
			if (base.Owner.Target.Length > 0)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Target, base.Owner.Target);
			}
			if (string.IsNullOrEmpty(base.Owner.NavigateUrl))
			{
				writer.RenderBeginTag(HtmlTextWriterTag.Span);
				return;
			}
			writer.AddAttribute(HtmlTextWriterAttribute.Href, base.Owner.ResolveClientUrl(base.Owner.NavigateUrl));
			writer.RenderBeginTag(HtmlTextWriterTag.A);
		}

		// Token: 0x06003B62 RID: 15202 RVA: 0x000C1294 File Offset: 0x000BF494
		protected void DetermineCssClassToHeader()
		{
			if (base.Owner.IsSeparator)
			{
				return;
			}
			string cssClass = base.Owner.CssClass;
			string text = base.Owner.ShouldRenderLink ? "rpLink" : "rpHeaderTemplate";
			base.Owner.CssClass = RadPanelBar.Styles.Combine(new string[]
			{
				text,
				cssClass
			});
			if (base.Owner.Selected)
			{
				base.Owner.CssClass = RadPanelBar.Styles.Combine(new string[]
				{
					base.Owner.CssClass,
					base.Owner.SelectedCssClass
				});
			}
			if (base.Owner.Parent is RadPanelBar)
			{
				base.Owner.CssClass = RadPanelBar.Styles.Combine(new string[]
				{
					base.Owner.CssClass,
					"rpRootLink"
				});
			}
			if (base.Owner.Items.Count > 0 || base.Owner._contentTemplateIsSet)
			{
				base.Owner.CssClass = RadPanelBar.Styles.Combine(new string[]
				{
					base.Owner.CssClass,
					"rpExpandable"
				});
				if (base.Owner.Expanded)
				{
					base.Owner.CssClass = RadPanelBar.Styles.Combine(new string[]
					{
						base.Owner.CssClass,
						base.Owner.ExpandedCssClass
					});
				}
			}
			if (!base.Owner.Enabled)
			{
				base.Owner.CssClass = RadPanelBar.Styles.Combine(new string[]
				{
					base.Owner.CssClass,
					base.Owner.DisabledCssClass
				});
			}
		}

		// Token: 0x06003B63 RID: 15203 RVA: 0x000C1458 File Offset: 0x000BF658
		protected override void RenderExpandHandle(HtmlTextWriter writer)
		{
			string iconName = base.Owner.Expanded ? "arrow-chevron-up" : "arrow-chevron-down";
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rpExpandHandle");
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			IconHelper.RenderIcon(writer, iconName);
			writer.RenderEndTag();
		}
	}
}
