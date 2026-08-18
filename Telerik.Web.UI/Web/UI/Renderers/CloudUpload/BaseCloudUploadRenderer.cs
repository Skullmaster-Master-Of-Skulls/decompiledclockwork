using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI.CloudUpload;

namespace Telerik.Web.UI.Renderers.CloudUpload
{
	// Token: 0x020000CA RID: 202
	public class BaseCloudUploadRenderer : IRenderer
	{
		// Token: 0x170002AD RID: 685
		// (get) Token: 0x060007D6 RID: 2006 RVA: 0x0001DA51 File Offset: 0x0001BC51
		internal FileListPanelSettings PanelSettings
		{
			get
			{
				if (this.cloudUpload._panelSettings != null)
				{
					return this.cloudUpload.FileListPanelSettings;
				}
				return this.cloudUpload._panelSettings;
			}
		}

		// Token: 0x060007D7 RID: 2007 RVA: 0x0001DA77 File Offset: 0x0001BC77
		public BaseCloudUploadRenderer(RadCloudUpload owner)
		{
			this.cloudUpload = owner;
		}

		// Token: 0x170002AE RID: 686
		// (get) Token: 0x060007D8 RID: 2008 RVA: 0x0001DA91 File Offset: 0x0001BC91
		public HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Div;
			}
		}

		// Token: 0x170002AF RID: 687
		// (get) Token: 0x060007D9 RID: 2009 RVA: 0x0001DA95 File Offset: 0x0001BC95
		public string CssClassFormatString
		{
			get
			{
				return "RadCloudUpload RadCloudUpload_{0}";
			}
		}

		// Token: 0x060007DA RID: 2010 RVA: 0x0001DA9C File Offset: 0x0001BC9C
		public void AddAttributesToRender(HtmlTextWriter writer)
		{
			bool isControlEnabled = this.cloudUpload.IsControlEnabled;
			this.cloudUpload.Enabled = true;
			this.cloudUpload.CallBaseAddAttributesToRender(writer);
			this.cloudUpload.Enabled = isControlEnabled;
		}

		// Token: 0x060007DB RID: 2011 RVA: 0x0001DAD9 File Offset: 0x0001BCD9
		public void RenderContents(HtmlTextWriter writer)
		{
			if (this.cloudUpload.IsDesignMode)
			{
				this.RenderDesignTimeHTML(writer);
				return;
			}
			this.RenderSelectButton(writer);
			this.RenderInfoPanel(writer);
		}

		// Token: 0x060007DC RID: 2012 RVA: 0x0001DB00 File Offset: 0x0001BD00
		protected virtual void RenderDesignTimeHTML(HtmlTextWriter writer)
		{
			string str = "200px";
			if (!this.cloudUpload.Width.IsEmpty)
			{
				UnitType type = this.cloudUpload.Width.Type;
				if (type != UnitType.Pixel)
				{
					if (type == UnitType.Percentage)
					{
						str = this.cloudUpload.Width.Value + "%";
					}
				}
				else
				{
					str = this.cloudUpload.Width.Value + "px";
				}
			}
			writer.Write("<style type=\"text/css\">");
			writer.Write(" \r\n                        .RadCloudUpload {\r\n                            display:block;\r\n                            width:" + str + ";}");
			writer.Write("</style>");
			string selectButtonText = this.cloudUpload.Localization.SelectButtonText;
			writer.Write("<input type='button' class='RadCloudUpload' value='" + selectButtonText + "' >");
		}

		// Token: 0x060007DD RID: 2013 RVA: 0x0001DBE4 File Offset: 0x0001BDE4
		protected virtual void RenderSelectButton(HtmlTextWriter writer)
		{
			string arg = this.cloudUpload.IsControlEnabled ? string.Empty : "rcuDisabled";
			string value = string.Format("{0} {1}", "rcuFileSelect", arg).TrimEnd(new char[0]);
			writer.AddAttribute(HtmlTextWriterAttribute.Class, value);
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			string value2 = string.Format("{0} {1}", "rcuFileWrap", "rcuButton");
			writer.AddAttribute(HtmlTextWriterAttribute.Class, value2);
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			writer.RenderBeginTag(HtmlTextWriterTag.Label);
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rcuFileInput");
			writer.AddAttribute(HtmlTextWriterAttribute.Type, "file");
			if (!this.cloudUpload.IsControlEnabled)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Disabled, "disabled");
			}
			writer.RenderBeginTag(HtmlTextWriterTag.Input);
			writer.RenderEndTag();
			writer.RenderEndTag();
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rcuButtonText");
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			writer.Write(this.cloudUpload.Localization.SelectButtonText);
			writer.RenderEndTag();
			writer.RenderEndTag();
			writer.RenderEndTag();
		}

		// Token: 0x060007DE RID: 2014 RVA: 0x0001DCEB File Offset: 0x0001BEEB
		protected virtual void RenderInfoPanel(HtmlTextWriter writer)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060007DF RID: 2015 RVA: 0x0001DCF2 File Offset: 0x0001BEF2
		protected virtual void RenderInfoPanelHeader(HtmlTextWriter writer)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060007E0 RID: 2016 RVA: 0x0001DCF9 File Offset: 0x0001BEF9
		protected virtual void RenderInfoPanelScrollContainer(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rcuBodyScroll");
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
		}

		// Token: 0x060007E1 RID: 2017 RVA: 0x0001DD10 File Offset: 0x0001BF10
		protected virtual void RenderInfoPanelBody(HtmlTextWriter writer)
		{
			string cssClass = string.Format("{0} {1}", "rcuFileList", "rcuFailed");
			string value = string.Format("{0}", "rcuBody");
			writer.AddAttribute(HtmlTextWriterAttribute.Class, value);
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			this.RenderInfoPanelScrollContainer(writer);
			this.RenderFileList(writer, cssClass);
			this.RenderFileList(writer, "rcuFileList");
			writer.RenderEndTag();
			writer.RenderEndTag();
		}

		// Token: 0x060007E2 RID: 2018 RVA: 0x0001DD7A File Offset: 0x0001BF7A
		protected virtual void RenderFileList(HtmlTextWriter writer, string cssClass)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060007E3 RID: 2019 RVA: 0x0001DD84 File Offset: 0x0001BF84
		protected virtual void RenderHeaderText(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rcuHeaderText");
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			this.RenderTextPlaceHolder(writer, "rcuTmplStatus");
			this.RenderTextPlaceHolder(writer, "rcuUploadedFiles");
			writer.Write("/");
			this.RenderTextPlaceHolder(writer, "rcuTotalFiles");
			writer.RenderEndTag();
		}

		// Token: 0x060007E4 RID: 2020 RVA: 0x0001DDDB File Offset: 0x0001BFDB
		protected virtual void RenderTextPlaceHolder(HtmlTextWriter writer, string cssClass)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, cssClass);
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			writer.Write(this.placeHolder);
			writer.RenderEndTag();
		}

		// Token: 0x060007E5 RID: 2021 RVA: 0x0001DE00 File Offset: 0x0001C000
		protected virtual void RenderIcon(HtmlTextWriter writer, string cssClass)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, cssClass);
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			writer.Write(this.placeHolder);
			writer.RenderEndTag();
		}

		// Token: 0x060007E6 RID: 2022 RVA: 0x0001DE25 File Offset: 0x0001C025
		protected virtual void RenderToggleButton(HtmlTextWriter writer)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060007E7 RID: 2023 RVA: 0x0001DE2C File Offset: 0x0001C02C
		protected virtual void RenderToolTip(HtmlTextWriter writer)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060007E8 RID: 2024 RVA: 0x0001DE34 File Offset: 0x0001C034
		protected virtual void RenderPanelStyleAttributes(HtmlTextWriter writer)
		{
			if (this.PanelSettings == null)
			{
				return;
			}
			if (this.PanelSettings.Width != Unit.Parse("420px"))
			{
				writer.AddStyleAttribute(HtmlTextWriterStyle.Width, this.PanelSettings.Width.ToString());
			}
		}

		// Token: 0x040001DD RID: 477
		internal RadCloudUpload cloudUpload;

		// Token: 0x040001DE RID: 478
		internal readonly string placeHolder = "<!-- &nbsp; -->";
	}
}
