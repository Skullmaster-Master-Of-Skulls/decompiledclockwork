using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02000632 RID: 1586
	[ToolboxItem(false)]
	public class OrgChartGroupItemRendererBase : WebControl, IOrgChartFieldsRenderer, INamingContainer
	{
		// Token: 0x170012EF RID: 4847
		// (get) Token: 0x060039A9 RID: 14761 RVA: 0x000BD758 File Offset: 0x000BB958
		// (set) Token: 0x060039AA RID: 14762 RVA: 0x000BD760 File Offset: 0x000BB960
		public string ImageUrl { get; set; }

		// Token: 0x170012F0 RID: 4848
		// (get) Token: 0x060039AB RID: 14763 RVA: 0x000BD769 File Offset: 0x000BB969
		// (set) Token: 0x060039AC RID: 14764 RVA: 0x000BD771 File Offset: 0x000BB971
		public string ImageAltText { get; set; }

		// Token: 0x170012F1 RID: 4849
		// (get) Token: 0x060039AD RID: 14765 RVA: 0x000BD77A File Offset: 0x000BB97A
		// (set) Token: 0x060039AE RID: 14766 RVA: 0x000BD782 File Offset: 0x000BB982
		public string Text { get; set; }

		// Token: 0x170012F2 RID: 4850
		// (get) Token: 0x060039AF RID: 14767 RVA: 0x000BD78B File Offset: 0x000BB98B
		// (set) Token: 0x060039B0 RID: 14768 RVA: 0x000BD793 File Offset: 0x000BB993
		public bool IsInGroup { get; set; }

		// Token: 0x170012F3 RID: 4851
		// (get) Token: 0x060039B1 RID: 14769 RVA: 0x000BD79C File Offset: 0x000BB99C
		// (set) Token: 0x060039B2 RID: 14770 RVA: 0x000BD7A4 File Offset: 0x000BB9A4
		internal bool IsSimpleBinding { get; set; }

		// Token: 0x170012F4 RID: 4852
		// (get) Token: 0x060039B3 RID: 14771 RVA: 0x000BD7AD File Offset: 0x000BB9AD
		// (set) Token: 0x060039B4 RID: 14772 RVA: 0x000BD7B5 File Offset: 0x000BB9B5
		public bool IsFirst { get; set; }

		// Token: 0x170012F5 RID: 4853
		// (get) Token: 0x060039B5 RID: 14773 RVA: 0x000BD7BE File Offset: 0x000BB9BE
		// (set) Token: 0x060039B6 RID: 14774 RVA: 0x000BD7C6 File Offset: 0x000BB9C6
		public bool IsLast { get; set; }

		// Token: 0x170012F6 RID: 4854
		// (get) Token: 0x060039B7 RID: 14775 RVA: 0x000BD7CF File Offset: 0x000BB9CF
		// (set) Token: 0x060039B8 RID: 14776 RVA: 0x000BD7D7 File Offset: 0x000BB9D7
		public bool IsTemplated { get; set; }

		// Token: 0x170012F7 RID: 4855
		// (get) Token: 0x060039B9 RID: 14777 RVA: 0x000BD7E0 File Offset: 0x000BB9E0
		// (set) Token: 0x060039BA RID: 14778 RVA: 0x000BD7E8 File Offset: 0x000BB9E8
		public bool IsFirstInRow { get; set; }

		// Token: 0x170012F8 RID: 4856
		// (get) Token: 0x060039BB RID: 14779 RVA: 0x000BD7F1 File Offset: 0x000BB9F1
		// (set) Token: 0x060039BC RID: 14780 RVA: 0x000BD7F9 File Offset: 0x000BB9F9
		public bool IsLastInRow { get; set; }

		// Token: 0x170012F9 RID: 4857
		// (get) Token: 0x060039BD RID: 14781 RVA: 0x000BD802 File Offset: 0x000BBA02
		// (set) Token: 0x060039BE RID: 14782 RVA: 0x000BD80A File Offset: 0x000BBA0A
		public bool ShouldRenderImage { get; set; }

		// Token: 0x170012FA RID: 4858
		// (get) Token: 0x060039BF RID: 14783 RVA: 0x000BD813 File Offset: 0x000BBA13
		// (set) Token: 0x060039C0 RID: 14784 RVA: 0x000BD81B File Offset: 0x000BBA1B
		public string DefaultImageUrl { get; set; }

		// Token: 0x170012FB RID: 4859
		// (get) Token: 0x060039C1 RID: 14785 RVA: 0x000BD824 File Offset: 0x000BBA24
		// (set) Token: 0x060039C2 RID: 14786 RVA: 0x000BD82C File Offset: 0x000BBA2C
		public object DataItem { get; set; }

		// Token: 0x170012FC RID: 4860
		// (get) Token: 0x060039C3 RID: 14787 RVA: 0x000BD835 File Offset: 0x000BBA35
		// (set) Token: 0x060039C4 RID: 14788 RVA: 0x000BD83D File Offset: 0x000BBA3D
		public new string CssClass { get; set; }

		// Token: 0x170012FD RID: 4861
		// (get) Token: 0x060039C5 RID: 14789 RVA: 0x000BD846 File Offset: 0x000BBA46
		// (set) Token: 0x060039C6 RID: 14790 RVA: 0x000BD84E File Offset: 0x000BBA4E
		internal bool EnableCollapsing { get; set; }

		// Token: 0x170012FE RID: 4862
		// (get) Token: 0x060039C7 RID: 14791 RVA: 0x000BD857 File Offset: 0x000BBA57
		// (set) Token: 0x060039C8 RID: 14792 RVA: 0x000BD85F File Offset: 0x000BBA5F
		internal bool Collapsed { get; set; }

		// Token: 0x170012FF RID: 4863
		// (get) Token: 0x060039C9 RID: 14793 RVA: 0x000BD868 File Offset: 0x000BBA68
		// (set) Token: 0x060039CA RID: 14794 RVA: 0x000BD870 File Offset: 0x000BBA70
		internal bool HasNodes { get; set; }

		// Token: 0x17001300 RID: 4864
		// (get) Token: 0x060039CB RID: 14795 RVA: 0x000BD879 File Offset: 0x000BBA79
		// (set) Token: 0x060039CC RID: 14796 RVA: 0x000BD881 File Offset: 0x000BBA81
		internal bool HasNodesForLoad { get; set; }

		// Token: 0x060039CD RID: 14797 RVA: 0x000BD88A File Offset: 0x000BBA8A
		protected virtual void RenderInnerContainer(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rocItemContent");
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			this.RenderImage(writer);
			if (!string.IsNullOrEmpty(this.Text))
			{
				this.RenderText(writer);
			}
			this.RenderFields(writer);
			writer.RenderEndTag();
		}

		// Token: 0x17001301 RID: 4865
		// (get) Token: 0x060039CE RID: 14798 RVA: 0x000BD8C9 File Offset: 0x000BBAC9
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				if (this.IsSimpleBinding)
				{
					return HtmlTextWriterTag.Div;
				}
				return HtmlTextWriterTag.Li;
			}
		}

		// Token: 0x060039CF RID: 14799 RVA: 0x000BD8D8 File Offset: 0x000BBAD8
		protected string GetListItemWrapperCssClass()
		{
			string arg = string.Format("{0} {1}", "rocItemWrap", this.IsFirst ? "rocFirst" : "").Trim();
			arg = string.Format("{0} {1}", arg, this.IsLast ? "rocLast" : "").Trim();
			arg = string.Format("{0} {1}", arg, (this.IsFirst && this.IsLast) ? "rocOnly" : "").Trim();
			arg = string.Format("{0} {1}", arg, this.IsFirstInRow ? "rocFirstInRow" : "").Trim();
			return string.Format("{0} {1}", arg, this.IsLastInRow ? "rocLastInRow" : "").Trim();
		}

		// Token: 0x060039D0 RID: 14800 RVA: 0x000BD9AC File Offset: 0x000BBBAC
		protected virtual void RenderImage(HtmlTextWriter writer)
		{
			if (this.ShouldRenderImage)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Class, "rocImageWrap");
				writer.RenderBeginTag(HtmlTextWriterTag.Div);
				string text = string.IsNullOrEmpty(this.DefaultImageUrl) ? this.Page.ClientScript.GetWebResourceUrl(typeof(RadOrgChart), "Telerik.Web.UI.Skins.Common.OrgChart.rocItemDefaultPicture.png") : base.ResolveUrl(this.DefaultImageUrl);
				writer.AddAttribute(HtmlTextWriterAttribute.Src, string.IsNullOrEmpty(this.ImageUrl) ? text : base.ResolveUrl(this.ImageUrl));
				writer.AddAttribute(HtmlTextWriterAttribute.Alt, (this.ImageAltText == null) ? "" : this.ImageAltText);
				writer.RenderBeginTag(HtmlTextWriterTag.Img);
				writer.RenderEndTag();
				writer.RenderEndTag();
			}
		}

		// Token: 0x060039D1 RID: 14801 RVA: 0x000BDA68 File Offset: 0x000BBC68
		protected void RenderText(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rocItemText");
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			writer.Write(this.Text);
			writer.RenderEndTag();
		}

		// Token: 0x060039D2 RID: 14802 RVA: 0x000BDA94 File Offset: 0x000BBC94
		protected void RenderFields(HtmlTextWriter writer)
		{
			foreach (OrgChartRenderedField orgChartRenderedField in this.RenderedFields)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Class, "rocItemField");
				writer.RenderBeginTag(HtmlTextWriterTag.Div);
				writer.Write(orgChartRenderedField.TextToRender);
				writer.RenderEndTag();
			}
		}

		// Token: 0x060039D3 RID: 14803 RVA: 0x000BDB08 File Offset: 0x000BBD08
		protected virtual void RenderExpandCollapseArrow(string nodeCollapsedState, bool collapsed, HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, nodeCollapsedState);
			writer.RenderBeginTag(HtmlTextWriterTag.A);
			string value = collapsed ? "+" : "-";
			writer.Write(value);
			writer.RenderEndTag();
		}

		// Token: 0x17001302 RID: 4866
		// (get) Token: 0x060039D4 RID: 14804 RVA: 0x000BDB42 File Offset: 0x000BBD42
		public OrgChartRenderedFieldCollection RenderedFields
		{
			get
			{
				if (this._renderedFields == null)
				{
					this._renderedFields = new OrgChartRenderedFieldCollection();
				}
				return this._renderedFields;
			}
		}

		// Token: 0x04000F5C RID: 3932
		private OrgChartRenderedFieldCollection _renderedFields;
	}
}
