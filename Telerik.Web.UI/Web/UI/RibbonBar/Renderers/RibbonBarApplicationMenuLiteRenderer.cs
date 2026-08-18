using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI.RibbonBar.Renderers
{
	// Token: 0x020007A9 RID: 1961
	internal class RibbonBarApplicationMenuLiteRenderer : RibbonBarApplicationMenuRenderBase
	{
		// Token: 0x06004491 RID: 17553 RVA: 0x000D7FAC File Offset: 0x000D61AC
		public RibbonBarApplicationMenuLiteRenderer(RibbonBarApplicationMenu owner) : base(owner)
		{
		}

		// Token: 0x06004492 RID: 17554 RVA: 0x000D7FB5 File Offset: 0x000D61B5
		public override string GetItemCssClassToRender()
		{
			return "rrbItem";
		}

		// Token: 0x06004493 RID: 17555 RVA: 0x000D7FBC File Offset: 0x000D61BC
		public override string GetFooterPaneCssClass()
		{
			return "rrbFooter";
		}

		// Token: 0x06004494 RID: 17556 RVA: 0x000D7FC3 File Offset: 0x000D61C3
		public override string GetAuxiliaryPaneContentCssClass()
		{
			return "rrbTemplate";
		}

		// Token: 0x06004495 RID: 17557 RVA: 0x000D7FCC File Offset: 0x000D61CC
		public override void AddAttributesToRender(HtmlTextWriter writer)
		{
			base.Owner.CssClass = "rrbApplicationItem";
			if (!base.Owner.Enabled)
			{
				RibbonBarApplicationMenu owner = base.Owner;
				owner.CssClass += " rrbDisabled";
			}
			Unit width = base.Owner.Width;
			Unit height = base.Owner.Height;
			base.Owner.Height = Unit.Empty;
			base.Owner.Width = Unit.Empty;
			base.Owner.BaseAddAttributesToRender(writer);
			base.Owner.Width = width;
			base.Owner.Height = height;
		}

		// Token: 0x06004496 RID: 17558 RVA: 0x000D8070 File Offset: 0x000D6270
		public override void RenderContents(HtmlTextWriter writer)
		{
			if (!string.IsNullOrEmpty(base.Owner.AccessKey))
			{
				this.RenderKeyboardBox(writer);
			}
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rrbLink");
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			writer.Write(base.Owner.Text);
			writer.RenderEndTag();
			this.RenderDropDown(writer);
		}

		// Token: 0x06004497 RID: 17559 RVA: 0x000D80C9 File Offset: 0x000D62C9
		protected void RenderKeyboardBox(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rrbKeyBox");
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			writer.Write(base.Owner.AccessKey);
			writer.RenderEndTag();
		}

		// Token: 0x06004498 RID: 17560 RVA: 0x000D80F8 File Offset: 0x000D62F8
		protected void RenderDropDown(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rrbSlide");
			writer.AddStyleAttribute(HtmlTextWriterStyle.Display, "none");
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			writer.AddAttribute(HtmlTextWriterAttribute.Class, string.Format("{0} {0}_{1} {2}", "rrbPopup", base.Owner.SkinToRender, "rrbApplicationMenuPopup"));
			if (!base.Owner.Width.IsEmpty)
			{
				writer.AddStyleAttribute(HtmlTextWriterStyle.Width, base.Owner.Width.ToString());
			}
			if (!base.Owner.Height.IsEmpty)
			{
				writer.AddStyleAttribute(HtmlTextWriterStyle.Height, base.Owner.Height.ToString());
			}
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			this.RenderHeaderBar(writer);
			this.RenderMenu(writer);
			this.RenderAuxiliaryPane(writer);
			this.RenderFooterPane(writer);
			writer.RenderEndTag();
			writer.RenderEndTag();
		}

		// Token: 0x06004499 RID: 17561 RVA: 0x000D81EA File Offset: 0x000D63EA
		protected void RenderHeaderBar(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rrbHeaderBar");
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			writer.RenderEndTag();
		}

		// Token: 0x0600449A RID: 17562 RVA: 0x000D8208 File Offset: 0x000D6408
		protected void RenderMenu(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, RibbonBarStyles.Combine(new string[]
			{
				"rrbMenu",
				"rrbApplicationMenu"
			}));
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rrbGroup");
			writer.RenderBeginTag(HtmlTextWriterTag.Ul);
			foreach (RibbonBarApplicationMenuItemBase ribbonBarApplicationMenuItemBase in base.Owner.Items)
			{
				ribbonBarApplicationMenuItemBase.RibbonBar = base.Owner.RibbonBar;
				ribbonBarApplicationMenuItemBase.RenderControl(writer);
			}
			writer.RenderEndTag();
			writer.RenderEndTag();
		}

		// Token: 0x0600449B RID: 17563 RVA: 0x000D82C0 File Offset: 0x000D64C0
		protected void RenderAuxiliaryPane(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rrbAuxiliaryPane");
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			if (!string.IsNullOrEmpty(base.Owner.AuxiliaryPane.Header))
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Class, "rrbHeader");
				writer.RenderBeginTag(HtmlTextWriterTag.Strong);
				writer.Write(base.Owner.AuxiliaryPane.Header);
				writer.RenderEndTag();
			}
			base.Owner.AuxiliaryPane.ContentWrapper.RenderControl(writer);
			writer.RenderEndTag();
		}

		// Token: 0x0600449C RID: 17564 RVA: 0x000D8348 File Offset: 0x000D6548
		protected void RenderFooterPane(HtmlTextWriter writer)
		{
			if (base.Owner.FooterPane.ContentTemplate != null || base.Owner.FooterPane.ContentWrapper.Controls.Count > 0)
			{
				base.Owner.FooterPane.ContentWrapper.RenderControl(writer);
			}
		}
	}
}
