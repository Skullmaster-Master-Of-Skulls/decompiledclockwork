using System;
using System.Web.UI;

namespace Telerik.Web.UI.Renderers.CloudUpload
{
	// Token: 0x02000133 RID: 307
	public class CloudUploadRenderer : BaseCloudUploadRenderer
	{
		// Token: 0x06000CC5 RID: 3269 RVA: 0x0002DAAE File Offset: 0x0002BCAE
		public CloudUploadRenderer(RadCloudUpload owner) : base(owner)
		{
		}

		// Token: 0x06000CC6 RID: 3270 RVA: 0x0002DAB8 File Offset: 0x0002BCB8
		protected override void RenderInfoPanel(HtmlTextWriter writer)
		{
			string text = this.cloudUpload.IsControlEnabled ? string.Empty : "rcuDisabled";
			string value = string.Format("{0} {1} {2}", "rcuInfoPanel", "radHideButtonText", text).TrimEnd(new char[0]);
			if (base.PanelSettings != null && base.PanelSettings.RenderButtonText)
			{
				value = string.Format("{0} {1}", "rcuInfoPanel", text).TrimEnd(new char[0]);
			}
			writer.AddAttribute(HtmlTextWriterAttribute.Class, value);
			writer.AddAttribute(HtmlTextWriterAttribute.Style, "display:none");
			this.RenderPanelStyleAttributes(writer);
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			this.RenderInfoPanelHeader(writer);
			this.RenderInfoPanelBody(writer);
			this.RenderToolTip(writer);
			writer.RenderEndTag();
		}

		// Token: 0x06000CC7 RID: 3271 RVA: 0x0002DB74 File Offset: 0x0002BD74
		protected override void RenderInfoPanelHeader(HtmlTextWriter writer)
		{
			string cssClass = string.Format("{0} {1}", "p-icon", "p-i-loading");
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rcuHeader");
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			this.RenderIcon(writer, cssClass);
			this.RenderHeaderText(writer);
			this.RenderToggleButton(writer);
			writer.RenderEndTag();
		}

		// Token: 0x06000CC8 RID: 3272 RVA: 0x0002DBC8 File Offset: 0x0002BDC8
		protected override void RenderToggleButton(HtmlTextWriter writer)
		{
			string value = string.Format("{0} {1}", "rcuButton", "rcuToggleButton");
			string cssClass = string.Format("{0} {1}", "p-icon", "p-i-arrow-60-up");
			writer.AddAttribute(HtmlTextWriterAttribute.Class, value);
			writer.AddAttribute(HtmlTextWriterAttribute.Type, "button");
			writer.RenderBeginTag(HtmlTextWriterTag.Button);
			this.RenderIcon(writer, cssClass);
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rcuButtonText");
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			writer.Write(this.cloudUpload.Localization.CollapseButton);
			writer.RenderEndTag();
			writer.RenderEndTag();
		}

		// Token: 0x06000CC9 RID: 3273 RVA: 0x0002DC5C File Offset: 0x0002BE5C
		protected override void RenderFileList(HtmlTextWriter writer, string cssClass)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, cssClass);
			writer.AddAttribute(HtmlTextWriterAttribute.Style, "display:none");
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			writer.RenderEndTag();
		}

		// Token: 0x06000CCA RID: 3274 RVA: 0x0002DC84 File Offset: 0x0002BE84
		protected override void RenderToolTip(HtmlTextWriter writer)
		{
			string text = "rcuToolTip";
			string value = string.Format("{0} {1}_{2}", text, text, this.cloudUpload.RuntimeSkin);
			writer.AddAttribute(HtmlTextWriterAttribute.Class, value);
			writer.AddStyleAttribute(HtmlTextWriterStyle.Display, "none");
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			writer.RenderEndTag();
		}
	}
}
