using System;
using System.Web;
using System.Web.UI;
using Telerik.Web.UI.Renderers;

namespace Telerik.Web.UI.PanelBar.Renderers
{
	// Token: 0x0200064E RID: 1614
	public abstract class PanelItemRenderBase : RendererBase
	{
		// Token: 0x06003B53 RID: 15187 RVA: 0x000C0B80 File Offset: 0x000BED80
		public PanelItemRenderBase(RadPanelItem owner)
		{
			this.Owner = owner;
		}

		// Token: 0x17001380 RID: 4992
		// (get) Token: 0x06003B54 RID: 15188 RVA: 0x000C0B8F File Offset: 0x000BED8F
		// (set) Token: 0x06003B55 RID: 15189 RVA: 0x000C0B97 File Offset: 0x000BED97
		protected RadPanelItem Owner { get; set; }

		// Token: 0x17001381 RID: 4993
		// (get) Token: 0x06003B56 RID: 15190 RVA: 0x000C0BA0 File Offset: 0x000BEDA0
		protected RadPanelBar PanelBar
		{
			get
			{
				return this.Owner.PanelBar;
			}
		}

		// Token: 0x17001382 RID: 4994
		// (get) Token: 0x06003B57 RID: 15191 RVA: 0x000C0BAD File Offset: 0x000BEDAD
		public override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Li;
			}
		}

		// Token: 0x06003B58 RID: 15192 RVA: 0x000C0BB4 File Offset: 0x000BEDB4
		protected void RenderTemplate(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rpTemplate");
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			if (this.Owner.Controls.IsReadOnly)
			{
				this.Owner.RenderChildrenBase(writer);
			}
			else
			{
				foreach (object obj in this.Owner.Controls)
				{
					Control control = (Control)obj;
					if (!(control is RadPanelItem) && !(control is RadPanelItemHeaderTemplateContainer))
					{
						control.RenderControl(writer);
					}
				}
			}
			writer.RenderEndTag();
		}

		// Token: 0x06003B59 RID: 15193 RVA: 0x000C0C60 File Offset: 0x000BEE60
		protected void RenderItemContent(HtmlTextWriter writer)
		{
			writer.Write(this.Owner.PanelBar.EnableItemTextHtmlEncoding ? HttpUtility.HtmlEncode(this.Owner.Text) : this.Owner.Text);
		}

		// Token: 0x06003B5A RID: 15194 RVA: 0x000C0C97 File Offset: 0x000BEE97
		protected virtual void RenderExpandHandle(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rpExpandHandle");
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			writer.RenderEndTag();
		}

		// Token: 0x06003B5B RID: 15195 RVA: 0x000C0CB4 File Offset: 0x000BEEB4
		protected void RenderTextPlaceholder(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rpText");
			if (string.IsNullOrEmpty(this.Owner.Text))
			{
				writer.AddStyleAttribute("display", "none");
			}
			if (this.Owner.InDesignMode && this.Owner.Items.Count > 0)
			{
				writer.AddAttribute("ItemRegion", null);
			}
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
		}

		// Token: 0x06003B5C RID: 15196 RVA: 0x000C0D24 File Offset: 0x000BEF24
		protected void RenderSlideBeginTag(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rpSlide");
			if (this.Owner.Expanded)
			{
				writer.AddStyleAttribute("display", "block");
				if (this.Owner.Level == 0 && this.Owner.PanelBar.ExpandMode == PanelBarExpandMode.FullExpandedItem && !this.Owner.PanelBar.Height.IsEmpty)
				{
					if (this.Owner.PanelBar.HasExpandedItems())
					{
						writer.AddStyleAttribute("height", this.Owner.PanelBar.Height.Value - (double)(this.Owner.PanelBar.Items.Count * 26) - 2.0 + "px");
					}
					else
					{
						writer.AddStyleAttribute("height", Math.Round(this.Owner.PanelBar.Height.Value / (double)this.Owner.PanelBar.Items.Count, 0) + "px");
					}
					writer.AddStyleAttribute("overflow", "auto");
				}
			}
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
		}

		// Token: 0x06003B5D RID: 15197 RVA: 0x000C0E74 File Offset: 0x000BF074
		protected void RenderChildItems(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, string.Format("rpGroup rpLevel{0} {1}", this.Owner.Level + 1, this.Owner.ChildGroupCssClass));
			if (this.Owner.Expanded)
			{
				writer.AddStyleAttribute("display", "block");
			}
			if (!this.Owner.ChildGroupHeight.IsEmpty)
			{
				writer.AddStyleAttribute(HtmlTextWriterStyle.Height, this.Owner.ChildGroupHeight.ToString());
			}
			writer.RenderBeginTag(HtmlTextWriterTag.Ul);
			foreach (object obj in this.Owner.Items)
			{
				RadPanelItem radPanelItem = (RadPanelItem)obj;
				radPanelItem.RenderControl(writer);
			}
			writer.RenderEndTag();
		}

		// Token: 0x06003B5E RID: 15198 RVA: 0x000C0F68 File Offset: 0x000BF168
		protected void RenderImage(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Alt, this.Owner.ToolTip);
			if (this.Owner.Page != null)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Src, this.Owner.ResolveClientUrl(this.Owner.CurrentImageUrl));
			}
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rpImage");
			if (this.Owner.ImagePosition == RadPanelItemImagePosition.Right)
			{
				writer.AddStyleAttribute("float", "right");
			}
			writer.RenderBeginTag(HtmlTextWriterTag.Img);
			writer.RenderEndTag();
		}

		// Token: 0x04001018 RID: 4120
		protected const int DefaultRootItemHeight = 26;
	}
}
