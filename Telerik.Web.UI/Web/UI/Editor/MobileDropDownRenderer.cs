using System;
using System.Web.UI;

namespace Telerik.Web.UI.Editor
{
	// Token: 0x020002CF RID: 719
	internal class MobileDropDownRenderer : ToolRendererBase
	{
		// Token: 0x0600190B RID: 6411 RVA: 0x00052B45 File Offset: 0x00050D45
		public MobileDropDownRenderer(EditorDropDown owner) : base(owner)
		{
			this.Owner = owner;
		}

		// Token: 0x17000877 RID: 2167
		// (get) Token: 0x0600190C RID: 6412 RVA: 0x00052B55 File Offset: 0x00050D55
		// (set) Token: 0x0600190D RID: 6413 RVA: 0x00052B5D File Offset: 0x00050D5D
		public new EditorDropDown Owner { get; private set; }

		// Token: 0x17000878 RID: 2168
		// (get) Token: 0x0600190E RID: 6414 RVA: 0x00052B66 File Offset: 0x00050D66
		public override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Span;
			}
		}

		// Token: 0x0600190F RID: 6415 RVA: 0x00052B6A File Offset: 0x00050D6A
		public override void RenderContents(HtmlTextWriter writer)
		{
			base.RenderContents(writer);
			this.RenderValue(writer);
			this.RenderIconForward(writer);
		}

		// Token: 0x06001910 RID: 6416 RVA: 0x00052B81 File Offset: 0x00050D81
		public void RenderValue(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "reToolValue");
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			writer.RenderEndTag();
		}

		// Token: 0x06001911 RID: 6417 RVA: 0x00052B9E File Offset: 0x00050D9E
		public override void AddAttributesToRender(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, this.CssClassString);
		}

		// Token: 0x06001912 RID: 6418 RVA: 0x00052BB0 File Offset: 0x00050DB0
		public override void RenderToolText(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "reToolText");
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			writer.Write((this.Owner.Text.Trim().Length == 0) ? "&nbsp;" : this.Owner.Text);
			writer.RenderEndTag();
		}

		// Token: 0x06001913 RID: 6419 RVA: 0x00052C07 File Offset: 0x00050E07
		public override void RenderToolIcon(HtmlTextWriter writer)
		{
		}

		// Token: 0x06001914 RID: 6420 RVA: 0x00052C09 File Offset: 0x00050E09
		public override void AddIconAttributesToRender(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, string.Format("reIcon reIcon{0}", this.Owner.Name));
		}

		// Token: 0x06001915 RID: 6421 RVA: 0x00052C28 File Offset: 0x00050E28
		public void RenderIconForward(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "reMore");
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "reIcon reIconForward");
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			writer.RenderEndTag();
			writer.RenderEndTag();
		}

		// Token: 0x17000879 RID: 2169
		// (get) Token: 0x06001916 RID: 6422 RVA: 0x00052C60 File Offset: 0x00050E60
		public override string CssClassString
		{
			get
			{
				return this.GetCssClassString();
			}
		}

		// Token: 0x1700087A RID: 2170
		// (get) Token: 0x06001917 RID: 6423 RVA: 0x00052C68 File Offset: 0x00050E68
		public override string CssClassFormatString
		{
			get
			{
				return "{0} re{1}";
			}
		}

		// Token: 0x06001918 RID: 6424 RVA: 0x00052C6F File Offset: 0x00050E6F
		public override string GetCssClassString()
		{
			return string.Format(this.CssClassFormatString, "reDropDown", this.Owner.Name);
		}
	}
}
