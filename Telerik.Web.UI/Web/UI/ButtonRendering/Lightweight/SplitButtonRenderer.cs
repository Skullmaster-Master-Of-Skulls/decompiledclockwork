using System;
using System.Web.UI;

namespace Telerik.Web.UI.ButtonRendering.Lightweight
{
	// Token: 0x020000EA RID: 234
	public class SplitButtonRenderer : StandardButtonRenderer
	{
		// Token: 0x060009AA RID: 2474 RVA: 0x00023149 File Offset: 0x00021349
		public SplitButtonRenderer(ButtonRenderingOptions options, IconRenderingOptions iconOptions) : base(options)
		{
			this.iconRenderer = new IconRenderer(options, iconOptions);
		}

		// Token: 0x1700035A RID: 858
		// (get) Token: 0x060009AB RID: 2475 RVA: 0x0002315F File Offset: 0x0002135F
		public override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Span;
			}
		}

		// Token: 0x060009AC RID: 2476 RVA: 0x00023163 File Offset: 0x00021363
		public override void AddAttributesToRender(HtmlTextWriter writer)
		{
		}

		// Token: 0x1700035B RID: 859
		// (get) Token: 0x060009AD RID: 2477 RVA: 0x00023168 File Offset: 0x00021368
		public override string CssClassFormatString
		{
			get
			{
				string splitButtonCssClass = this.options.SplitButtonCssClass;
				if (!string.IsNullOrEmpty(splitButtonCssClass))
				{
					return base.CssClassFormatString + " " + splitButtonCssClass;
				}
				return base.CssClassFormatString;
			}
		}

		// Token: 0x060009AE RID: 2478 RVA: 0x000231A1 File Offset: 0x000213A1
		protected override void RenderButtonChildNodes(HtmlTextWriter writer)
		{
			this.RenderPrimaryIcon(writer);
			if (this.ShouldRenderLeftSplitElement())
			{
				this.RenderSplitElement(writer, "rbSplitPartLeft");
			}
			if (this.ShouldRenderRightSplitElement())
			{
				this.RenderSplitElement(writer, "rbSplitPartRight");
			}
			base.RenderTextHolder(writer);
			this.RenderSecondaryIcon(writer);
		}

		// Token: 0x060009AF RID: 2479 RVA: 0x000231E0 File Offset: 0x000213E0
		public virtual void RenderPrimaryIcon(HtmlTextWriter writer)
		{
			this.iconRenderer.RenderPrimaryIcon(writer);
		}

		// Token: 0x060009B0 RID: 2480 RVA: 0x000231EE File Offset: 0x000213EE
		private bool ShouldRenderLeftSplitElement()
		{
			return this.options.SplitButtonPosition == ButtonPosition.Left && !this.iconRenderer.HasPrimaryIcon;
		}

		// Token: 0x060009B1 RID: 2481 RVA: 0x0002320E File Offset: 0x0002140E
		public virtual void RenderSecondaryIcon(HtmlTextWriter writer)
		{
			this.iconRenderer.RenderSecondaryIcon(writer);
		}

		// Token: 0x060009B2 RID: 2482 RVA: 0x0002321C File Offset: 0x0002141C
		private bool ShouldRenderRightSplitElement()
		{
			return this.options.SplitButtonPosition == ButtonPosition.Right && !this.iconRenderer.HasSecondaryIcon;
		}

		// Token: 0x060009B3 RID: 2483 RVA: 0x0002323C File Offset: 0x0002143C
		public virtual void RenderSplitElement(HtmlTextWriter writer, string splitPositionCssClass)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, string.Concat(new string[]
			{
				"rbSplitPart " + splitPositionCssClass
			}));
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			writer.AddAttribute(HtmlTextWriterAttribute.Class, string.Concat(new string[]
			{
				"rbIcon p-icon rbSplitIcon p-i-arrow-down"
			}));
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			writer.RenderEndTag();
			writer.RenderEndTag();
		}

		// Token: 0x0400026E RID: 622
		private readonly IconRenderer iconRenderer;
	}
}
