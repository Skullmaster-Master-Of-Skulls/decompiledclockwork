using System;
using System.Web.UI;

namespace Telerik.Web.UI.PanelBar.Renderers
{
	// Token: 0x02000650 RID: 1616
	internal class PanelItemClassicRenderer : PanelItemRenderBase
	{
		// Token: 0x06003B64 RID: 15204 RVA: 0x000C14A1 File Offset: 0x000BF6A1
		public PanelItemClassicRenderer(RadPanelItem owner) : base(owner)
		{
		}

		// Token: 0x06003B65 RID: 15205 RVA: 0x000C14AC File Offset: 0x000BF6AC
		public override void RenderContents(HtmlTextWriter writer)
		{
			if (base.Owner.ShouldRenderLink)
			{
				this.RenderLink(writer);
				string text = string.Empty;
				if (!string.IsNullOrEmpty(base.Owner.NavigateUrl))
				{
					text = RadPanelBar.Styles.Combine(new string[]
					{
						text,
						"rpNavigation"
					});
				}
				writer.AddAttribute(HtmlTextWriterAttribute.Class, RadPanelBar.Styles.Combine(new string[]
				{
					"rpOut",
					text
				}));
				writer.RenderBeginTag(HtmlTextWriterTag.Span);
				if (!string.IsNullOrEmpty(base.Owner.ImageUrl))
				{
					base.RenderImage(writer);
				}
				this.RenderExpandHandle(writer);
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
				writer.RenderEndTag();
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

		// Token: 0x06003B66 RID: 15206 RVA: 0x000C1710 File Offset: 0x000BF910
		private void RenderLink(HtmlTextWriter writer)
		{
			if (string.IsNullOrEmpty(base.Owner.NavigateUrl))
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Href, "#");
			}
			else
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Href, base.Owner.ResolveClientUrl(base.Owner.NavigateUrl));
			}
			string cssClass = base.Owner.CssClass;
			this.DetermineCssClassToHeader();
			base.Owner.AddAttributes(writer);
			base.Owner.CssClass = cssClass;
			if (base.Owner.Target.Length > 0)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Target, base.Owner.Target);
			}
			writer.RenderBeginTag(HtmlTextWriterTag.A);
		}

		// Token: 0x06003B67 RID: 15207 RVA: 0x000C17B4 File Offset: 0x000BF9B4
		private void DetermineCssClassToHeader()
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
	}
}
