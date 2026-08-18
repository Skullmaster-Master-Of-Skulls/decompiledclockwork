using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace Telerik.Web.UI
{
	// Token: 0x02000020 RID: 32
	[ToolboxItem(true)]
	public abstract class CardComponentBase : HtmlContainerControl
	{
		// Token: 0x060001C4 RID: 452 RVA: 0x00005232 File Offset: 0x00003432
		protected override void Render(HtmlTextWriter writer)
		{
			this.RenderComponentContent(writer);
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060001C5 RID: 453 RVA: 0x0000523B File Offset: 0x0000343B
		// (set) Token: 0x060001C6 RID: 454 RVA: 0x00005243 File Offset: 0x00003443
		[DefaultValue("")]
		[CssClassProperty]
		public virtual string CssClass { get; set; }

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060001C7 RID: 455
		public abstract string DefaultCssClass { get; }

		// Token: 0x060001C8 RID: 456 RVA: 0x0000524C File Offset: 0x0000344C
		public virtual bool ShouldRenderAttribute(string key)
		{
			List<string> list = new List<string>
			{
				"title",
				"tagkey",
				"tooltip",
				"content"
			};
			return !list.Contains(key.ToLower());
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x0000529C File Offset: 0x0000349C
		protected virtual void RenderComponentContent(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, this.DefaultCssClass + " " + this.CssClass);
			foreach (object obj in base.Attributes.Keys)
			{
				string text = (string)obj;
				if (this.ShouldRenderAttribute(text))
				{
					writer.AddAttribute(text, base.Attributes[text]);
				}
				if (text.ToLower() == "tooltip")
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Title, base.Attributes[text]);
				}
			}
			if (!string.IsNullOrEmpty(this.ID))
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID);
			}
			writer.RenderBeginTag(this.TagKey);
			if (!string.IsNullOrEmpty(this.Content))
			{
				writer.Write(this.Content);
			}
			base.RenderChildren(writer);
			writer.RenderEndTag();
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060001CA RID: 458 RVA: 0x000053A0 File Offset: 0x000035A0
		// (set) Token: 0x060001CB RID: 459 RVA: 0x000053CC File Offset: 0x000035CC
		[DefaultValue(HtmlTextWriterTag.Div)]
		public virtual HtmlTextWriterTag TagKey
		{
			get
			{
				if (this.ViewState["TagKey"] == null)
				{
					return HtmlTextWriterTag.Div;
				}
				return (HtmlTextWriterTag)this.ViewState["TagKey"];
			}
			set
			{
				this.ViewState["TagKey"] = value;
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060001CC RID: 460 RVA: 0x000053E4 File Offset: 0x000035E4
		// (set) Token: 0x060001CD RID: 461 RVA: 0x00005404 File Offset: 0x00003604
		[DefaultValue("")]
		public virtual string Content
		{
			get
			{
				return ((string)this.ViewState["Content"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["Content"] = value;
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060001CE RID: 462 RVA: 0x00005417 File Offset: 0x00003617
		// (set) Token: 0x060001CF RID: 463 RVA: 0x00005437 File Offset: 0x00003637
		[DefaultValue("")]
		public string ToolTip
		{
			get
			{
				return ((string)this.ViewState["ToolTip"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["ToolTip"] = value;
			}
		}
	}
}
