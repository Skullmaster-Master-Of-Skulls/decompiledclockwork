using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI.RibbonBar.Renderers
{
	// Token: 0x02000788 RID: 1928
	internal class RibbonBarApplicationMenuClassicRenderer : RibbonBarApplicationMenuRenderBase
	{
		// Token: 0x060043D5 RID: 17365 RVA: 0x000D44A1 File Offset: 0x000D26A1
		public RibbonBarApplicationMenuClassicRenderer(RibbonBarApplicationMenu owner) : base(owner)
		{
		}

		// Token: 0x060043D6 RID: 17366 RVA: 0x000D44AA File Offset: 0x000D26AA
		public override string GetItemCssClassToRender()
		{
			return "rrbMenuItem";
		}

		// Token: 0x060043D7 RID: 17367 RVA: 0x000D44B1 File Offset: 0x000D26B1
		public override string GetFooterPaneCssClass()
		{
			return "rrbFooterPane";
		}

		// Token: 0x060043D8 RID: 17368 RVA: 0x000D44B8 File Offset: 0x000D26B8
		public override string GetAuxiliaryPaneContentCssClass()
		{
			return "rrbAPTemplate";
		}

		// Token: 0x060043D9 RID: 17369 RVA: 0x000D44C0 File Offset: 0x000D26C0
		public override void AddAttributesToRender(HtmlTextWriter writer)
		{
			base.Owner.CssClass = "rrbApplicationTab";
			if (!base.Owner.Enabled)
			{
				RibbonBarApplicationMenu owner = base.Owner;
				owner.CssClass += " rrbDisabled";
			}
			Unit width = base.Owner.Width;
			Unit height = base.Owner.Height;
			base.Owner.Height = Unit.Empty;
			base.Owner.Width = Unit.Empty;
			string accessKey = base.Owner.AccessKey;
			base.Owner.AccessKey = "";
			base.Owner.BaseAddAttributesToRender(writer);
			base.Owner.AccessKey = accessKey;
			base.Owner.Width = width;
			base.Owner.Height = height;
		}

		// Token: 0x060043DA RID: 17370 RVA: 0x000D458C File Offset: 0x000D278C
		public override void RenderContents(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Href, "#");
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rrbTabLabel");
			writer.AddAttribute(HtmlTextWriterAttribute.Accesskey, base.Owner.AccessKey);
			writer.RenderBeginTag(HtmlTextWriterTag.A);
			if (!string.IsNullOrEmpty(base.Owner.AccessKey))
			{
				this.RenderKeyboardBox(writer);
			}
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rrbTabText");
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			writer.Write(base.Owner.Text);
			writer.RenderEndTag();
			writer.RenderEndTag();
			this.RenderDropDown(writer);
		}

		// Token: 0x060043DB RID: 17371 RVA: 0x000D461E File Offset: 0x000D281E
		protected void RenderKeyboardBox(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rrbKeyBox");
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			writer.Write(base.Owner.AccessKey);
			writer.RenderEndTag();
		}

		// Token: 0x060043DC RID: 17372 RVA: 0x000D464C File Offset: 0x000D284C
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

		// Token: 0x060043DD RID: 17373 RVA: 0x000D473E File Offset: 0x000D293E
		protected void RenderHeaderBar(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rrbHeaderBar");
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			writer.RenderEndTag();
		}

		// Token: 0x060043DE RID: 17374 RVA: 0x000D475C File Offset: 0x000D295C
		protected void RenderMenu(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, RibbonBarStyles.Combine(new string[]
			{
				"rrbMenu",
				"rrbApplicationMenu"
			}));
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rrbMenuGroup");
			writer.RenderBeginTag(HtmlTextWriterTag.Ul);
			foreach (RibbonBarApplicationMenuItemBase ribbonBarApplicationMenuItemBase in base.Owner.Items)
			{
				ribbonBarApplicationMenuItemBase.RibbonBar = base.Owner.RibbonBar;
				ribbonBarApplicationMenuItemBase.RenderControl(writer);
			}
			writer.RenderEndTag();
			writer.RenderEndTag();
		}

		// Token: 0x060043DF RID: 17375 RVA: 0x000D4814 File Offset: 0x000D2A14
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

		// Token: 0x060043E0 RID: 17376 RVA: 0x000D489C File Offset: 0x000D2A9C
		protected void RenderFooterPane(HtmlTextWriter writer)
		{
			if (base.Owner.FooterPane.ContentTemplate != null || base.Owner.FooterPane.ContentWrapper.Controls.Count > 0)
			{
				base.Owner.FooterPane.ContentWrapper.RenderControl(writer);
			}
		}
	}
}
