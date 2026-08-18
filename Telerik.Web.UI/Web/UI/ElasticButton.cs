using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02000151 RID: 337
	[ToolboxItem(false)]
	public class ElasticButton : Button
	{
		// Token: 0x1700048B RID: 1163
		// (get) Token: 0x06000D60 RID: 3424 RVA: 0x00031600 File Offset: 0x0002F800
		protected override string TagName
		{
			get
			{
				return "button";
			}
		}

		// Token: 0x1700048C RID: 1164
		// (get) Token: 0x06000D61 RID: 3425 RVA: 0x00031607 File Offset: 0x0002F807
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Button;
			}
		}

		// Token: 0x1700048D RID: 1165
		// (get) Token: 0x06000D62 RID: 3426 RVA: 0x0003160B File Offset: 0x0002F80B
		// (set) Token: 0x06000D63 RID: 3427 RVA: 0x0003162B File Offset: 0x0002F82B
		[DefaultValue("")]
		[Description("Gets or sets the class for the first inner Span element")]
		[NotifyParentProperty(true)]
		public virtual string FirstSpanClass
		{
			get
			{
				return (this.ViewState["FirstSpanClass"] as string) ?? string.Empty;
			}
			set
			{
				this.ViewState["FirstSpanClass"] = value;
			}
		}

		// Token: 0x1700048E RID: 1166
		// (get) Token: 0x06000D64 RID: 3428 RVA: 0x0003163E File Offset: 0x0002F83E
		// (set) Token: 0x06000D65 RID: 3429 RVA: 0x0003165E File Offset: 0x0002F85E
		[DefaultValue("")]
		[Description("Gets or sets the class for the second inner Span element")]
		[NotifyParentProperty(true)]
		public virtual string SecondSpanClass
		{
			get
			{
				return (this.ViewState["SecondSpanClass"] as string) ?? string.Empty;
			}
			set
			{
				this.ViewState["SecondSpanClass"] = value;
			}
		}

		// Token: 0x1700048F RID: 1167
		// (get) Token: 0x06000D66 RID: 3430 RVA: 0x00031671 File Offset: 0x0002F871
		// (set) Token: 0x06000D67 RID: 3431 RVA: 0x00031691 File Offset: 0x0002F891
		[DefaultValue("")]
		[Description("Gets or sets the inner text of the first Span element")]
		[NotifyParentProperty(true)]
		public virtual string FirstSpanInnerText
		{
			get
			{
				return (this.ViewState["FirstSpanInnerText"] as string) ?? string.Empty;
			}
			set
			{
				this.ViewState["FirstSpanInnerText"] = value;
			}
		}

		// Token: 0x17000490 RID: 1168
		// (get) Token: 0x06000D68 RID: 3432 RVA: 0x000316A4 File Offset: 0x0002F8A4
		// (set) Token: 0x06000D69 RID: 3433 RVA: 0x000316C4 File Offset: 0x0002F8C4
		[NotifyParentProperty(true)]
		[Description("Gets or sets the inner text of the second Span element")]
		[DefaultValue("")]
		public virtual string SecondSpanInnerText
		{
			get
			{
				return (this.ViewState["SecondSpanInnerText"] as string) ?? string.Empty;
			}
			set
			{
				this.ViewState["SecondSpanInnerText"] = value;
			}
		}

		// Token: 0x06000D6A RID: 3434 RVA: 0x000316D7 File Offset: 0x0002F8D7
		public ElasticButton()
		{
		}

		// Token: 0x06000D6B RID: 3435 RVA: 0x000316DF File Offset: 0x0002F8DF
		public ElasticButton(string firstSpanClass)
		{
			this.FirstSpanClass = firstSpanClass;
			this.SecondSpanClass = string.Empty;
		}

		// Token: 0x06000D6C RID: 3436 RVA: 0x000316F9 File Offset: 0x0002F8F9
		public ElasticButton(string firstSpanClass, string secondSpanClass)
		{
			this.FirstSpanClass = firstSpanClass;
			this.SecondSpanClass = secondSpanClass;
		}

		// Token: 0x06000D6D RID: 3437 RVA: 0x00031710 File Offset: 0x0002F910
		protected override void RenderContents(HtmlTextWriter writer)
		{
			if (!string.IsNullOrEmpty(this.FirstSpanClass))
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Class, this.FirstSpanClass);
				writer.RenderBeginTag(HtmlTextWriterTag.Span);
				if (!string.IsNullOrEmpty(this.FirstSpanInnerText))
				{
					writer.WriteEncodedText(this.FirstSpanInnerText);
				}
				writer.RenderEndTag();
			}
			if (!string.IsNullOrEmpty(this.SecondSpanClass))
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Class, this.SecondSpanClass);
				writer.RenderBeginTag(HtmlTextWriterTag.Span);
				if (!string.IsNullOrEmpty(this.SecondSpanInnerText))
				{
					writer.WriteEncodedText(this.SecondSpanInnerText);
				}
				else if (!string.IsNullOrEmpty(base.Text))
				{
					writer.WriteEncodedText(base.Text);
				}
				else if (!string.IsNullOrEmpty(this.ToolTip))
				{
					writer.WriteEncodedText(this.ToolTip);
				}
				writer.RenderEndTag();
			}
		}
	}
}
