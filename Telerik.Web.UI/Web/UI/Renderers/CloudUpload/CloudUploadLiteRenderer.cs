using System;
using System.Web.UI;

namespace Telerik.Web.UI.Renderers.CloudUpload
{
	// Token: 0x020000CB RID: 203
	public class CloudUploadLiteRenderer : BaseCloudUploadRenderer
	{
		// Token: 0x060007E9 RID: 2025 RVA: 0x0001DE87 File Offset: 0x0001C087
		public CloudUploadLiteRenderer(RadCloudUpload owner) : base(owner)
		{
		}

		// Token: 0x060007EA RID: 2026 RVA: 0x0001DE90 File Offset: 0x0001C090
		protected override void RenderInfoPanel(HtmlTextWriter writer)
		{
			string text = this.cloudUpload.IsControlEnabled ? string.Empty : "rcuDisabled";
			string value = string.Format("{0} {1} {2} {3}", new object[]
			{
				"rcuInfoPanel",
				"radHideButtonText",
				"rcuHidden",
				text
			}).TrimEnd(new char[0]);
			if (base.PanelSettings != null && base.PanelSettings.RenderButtonText)
			{
				value = string.Format("{0} {1} {2}", "rcuInfoPanel", "rcuHidden", text).TrimEnd(new char[0]);
			}
			writer.AddAttribute(HtmlTextWriterAttribute.Class, value);
			this.RenderPanelStyleAttributes(writer);
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			this.RenderInfoPanelHeader(writer);
			this.RenderInfoPanelBody(writer);
			this.RenderToolTip(writer);
			writer.RenderEndTag();
		}

		// Token: 0x060007EB RID: 2027 RVA: 0x0001DF5C File Offset: 0x0001C15C
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

		// Token: 0x060007EC RID: 2028 RVA: 0x0001DFB0 File Offset: 0x0001C1B0
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

		// Token: 0x060007ED RID: 2029 RVA: 0x0001E044 File Offset: 0x0001C244
		protected override void RenderFileList(HtmlTextWriter writer, string cssClass)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, string.Format("{0} {1}", cssClass, "rcuHidden"));
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			writer.RenderEndTag();
		}

		// Token: 0x060007EE RID: 2030 RVA: 0x0001E06C File Offset: 0x0001C26C
		protected override void RenderToolTip(HtmlTextWriter writer)
		{
			string text = "rcuToolTip";
			string value = string.Format("{0} {1}_{2} {3}", new object[]
			{
				text,
				text,
				this.cloudUpload.RuntimeSkin,
				"rcuHidden"
			});
			writer.AddAttribute(HtmlTextWriterAttribute.Class, value);
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			writer.RenderEndTag();
		}
	}
}
