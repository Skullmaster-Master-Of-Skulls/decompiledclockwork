using System;
using System.Web;
using System.Web.Caching;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Web.UI.Captcha;

namespace Telerik.Web.UI
{
	// Token: 0x020016D0 RID: 5840
	internal class CaptchaProtector : StateManager, ISpamProtector, IDisposable
	{
		// Token: 0x17004517 RID: 17687
		// (get) Token: 0x0600E157 RID: 57687 RVA: 0x00321629 File Offset: 0x0031F829
		public bool IsValid
		{
			get
			{
				return this.isValid;
			}
		}

		// Token: 0x17004518 RID: 17688
		// (get) Token: 0x0600E158 RID: 57688 RVA: 0x00321631 File Offset: 0x0031F831
		// (set) Token: 0x0600E159 RID: 57689 RVA: 0x00321639 File Offset: 0x0031F839
		public string PrevGuid
		{
			get
			{
				return this.prevGuid;
			}
			set
			{
				this.prevGuid = value;
			}
		}

		// Token: 0x17004519 RID: 17689
		// (get) Token: 0x0600E15A RID: 57690 RVA: 0x00321642 File Offset: 0x0031F842
		// (set) Token: 0x0600E15B RID: 57691 RVA: 0x00321662 File Offset: 0x0031F862
		private string CurrentGuid
		{
			get
			{
				return (string)(base.ViewState["CurrentGuid"] ?? string.Empty);
			}
			set
			{
				base.ViewState["CurrentGuid"] = value;
			}
		}

		// Token: 0x1700451A RID: 17690
		// (get) Token: 0x0600E15C RID: 57692 RVA: 0x00321675 File Offset: 0x0031F875
		// (set) Token: 0x0600E15D RID: 57693 RVA: 0x00321697 File Offset: 0x0031F897
		public int MaxTimeout
		{
			get
			{
				return (int)(base.ViewState["MaxTimeout"] ?? 20);
			}
			set
			{
				base.ViewState["MaxTimeout"] = value;
			}
		}

		// Token: 0x1700451B RID: 17691
		// (get) Token: 0x0600E15E RID: 57694 RVA: 0x003216AF File Offset: 0x0031F8AF
		// (set) Token: 0x0600E15F RID: 57695 RVA: 0x003216CF File Offset: 0x0031F8CF
		public string TextBoxCssClass
		{
			get
			{
				return (string)(base.ViewState["TextBoxCssClass"] ?? string.Empty);
			}
			set
			{
				base.ViewState["TextBoxCssClass"] = value;
			}
		}

		// Token: 0x1700451C RID: 17692
		// (get) Token: 0x0600E160 RID: 57696 RVA: 0x003216E2 File Offset: 0x0031F8E2
		// (set) Token: 0x0600E161 RID: 57697 RVA: 0x003216FD File Offset: 0x0031F8FD
		internal CaptchaImage CaptchaImage
		{
			get
			{
				if (this.captchaImage == null)
				{
					this.captchaImage = new CaptchaImage();
				}
				return this.captchaImage;
			}
			set
			{
				this.captchaImage = value;
			}
		}

		// Token: 0x1700451D RID: 17693
		// (get) Token: 0x0600E162 RID: 57698 RVA: 0x00321706 File Offset: 0x0031F906
		// (set) Token: 0x0600E163 RID: 57699 RVA: 0x00321727 File Offset: 0x0031F927
		public short TextBoxTabIndex
		{
			get
			{
				return (short)(base.ViewState["TextBoxTabIndex"] ?? 0);
			}
			set
			{
				base.ViewState["TextBoxTabIndex"] = value;
			}
		}

		// Token: 0x1700451E RID: 17694
		// (get) Token: 0x0600E164 RID: 57700 RVA: 0x0032173F File Offset: 0x0031F93F
		// (set) Token: 0x0600E165 RID: 57701 RVA: 0x0032175F File Offset: 0x0031F95F
		public string TextBoxAccessKey
		{
			get
			{
				return (string)(base.ViewState["TextBoxAccessKey"] ?? "");
			}
			set
			{
				base.ViewState["TextBoxAccessKey"] = value;
			}
		}

		// Token: 0x1700451F RID: 17695
		// (get) Token: 0x0600E166 RID: 57702 RVA: 0x00321772 File Offset: 0x0031F972
		// (set) Token: 0x0600E167 RID: 57703 RVA: 0x00321792 File Offset: 0x0031F992
		public string TextBoxTitle
		{
			get
			{
				return (string)(base.ViewState["TextBoxTitle"] ?? "");
			}
			set
			{
				base.ViewState["TextBoxTitle"] = value;
			}
		}

		// Token: 0x17004520 RID: 17696
		// (get) Token: 0x0600E168 RID: 57704 RVA: 0x003217A5 File Offset: 0x0031F9A5
		// (set) Token: 0x0600E169 RID: 57705 RVA: 0x003217C5 File Offset: 0x0031F9C5
		public string TextBoxLabel
		{
			get
			{
				return (string)(base.ViewState["TextBoxLabel"] ?? "Type the code from the image");
			}
			set
			{
				base.ViewState["TextBoxLabel"] = value;
			}
		}

		// Token: 0x17004521 RID: 17697
		// (get) Token: 0x0600E16A RID: 57706 RVA: 0x003217D8 File Offset: 0x0031F9D8
		// (set) Token: 0x0600E16B RID: 57707 RVA: 0x003217F8 File Offset: 0x0031F9F8
		public string TextBoxLabelCssClass
		{
			get
			{
				return (string)(base.ViewState["TextBoxLabelCssClass"] ?? "");
			}
			set
			{
				base.ViewState["TextBoxLabelCssClass"] = value;
			}
		}

		// Token: 0x17004522 RID: 17698
		// (get) Token: 0x0600E16C RID: 57708 RVA: 0x0032180B File Offset: 0x0031FA0B
		// (set) Token: 0x0600E16D RID: 57709 RVA: 0x0032182C File Offset: 0x0031FA2C
		public bool IsCaseIgnored
		{
			get
			{
				return (bool)(base.ViewState["IsCaseIgnored"] ?? true);
			}
			set
			{
				base.ViewState["IsCaseIgnored"] = value;
			}
		}

		// Token: 0x17004523 RID: 17699
		// (get) Token: 0x0600E16E RID: 57710 RVA: 0x00321844 File Offset: 0x0031FA44
		// (set) Token: 0x0600E16F RID: 57711 RVA: 0x00321865 File Offset: 0x0031FA65
		public CaptchaImageStorage CaptchaImageStoredIn
		{
			get
			{
				return (CaptchaImageStorage)(base.ViewState["CaptchaImageStoredIn"] ?? CaptchaImageStorage.Cache);
			}
			set
			{
				base.ViewState["CaptchaImageStoredIn"] = value;
			}
		}

		// Token: 0x17004524 RID: 17700
		// (get) Token: 0x0600E170 RID: 57712 RVA: 0x0032187D File Offset: 0x0031FA7D
		// (set) Token: 0x0600E171 RID: 57713 RVA: 0x0032189E File Offset: 0x0031FA9E
		public bool EnableRefreshImage
		{
			get
			{
				return (bool)(base.ViewState["EnableRefreshImage"] ?? false);
			}
			set
			{
				base.ViewState["EnableRefreshImage"] = value;
			}
		}

		// Token: 0x17004525 RID: 17701
		// (get) Token: 0x0600E172 RID: 57714 RVA: 0x003218B6 File Offset: 0x0031FAB6
		// (set) Token: 0x0600E173 RID: 57715 RVA: 0x003218CD File Offset: 0x0031FACD
		public string RefreshImageAccessKey
		{
			get
			{
				return (string)base.ViewState["RefreshImageAccessKey"];
			}
			set
			{
				base.ViewState["RefreshImageAccessKey"] = value;
			}
		}

		// Token: 0x17004526 RID: 17702
		// (get) Token: 0x0600E174 RID: 57716 RVA: 0x003218E0 File Offset: 0x0031FAE0
		// (set) Token: 0x0600E175 RID: 57717 RVA: 0x003218F7 File Offset: 0x0031FAF7
		public string AudioAccessKey
		{
			get
			{
				return (string)base.ViewState["AudioAccessKey"];
			}
			set
			{
				base.ViewState["AudioAccessKey"] = value;
			}
		}

		// Token: 0x17004527 RID: 17703
		// (get) Token: 0x0600E176 RID: 57718 RVA: 0x0032190A File Offset: 0x0031FB0A
		// (set) Token: 0x0600E177 RID: 57719 RVA: 0x0032192A File Offset: 0x0031FB2A
		public string LinkButtonText
		{
			get
			{
				return ((string)base.ViewState["LinkButtonText"]) ?? "Generate New Image";
			}
			set
			{
				base.ViewState["LinkButtonText"] = value;
			}
		}

		// Token: 0x17004528 RID: 17704
		// (get) Token: 0x0600E178 RID: 57720 RVA: 0x0032193D File Offset: 0x0031FB3D
		// (set) Token: 0x0600E179 RID: 57721 RVA: 0x0032195D File Offset: 0x0031FB5D
		public string AudioButtonText
		{
			get
			{
				return ((string)base.ViewState["AudioButtonText"]) ?? "Get Audio Code";
			}
			set
			{
				base.ViewState["AudioButtonText"] = value;
			}
		}

		// Token: 0x17004529 RID: 17705
		// (get) Token: 0x0600E17A RID: 57722 RVA: 0x00321970 File Offset: 0x0031FB70
		// (set) Token: 0x0600E17B RID: 57723 RVA: 0x00321990 File Offset: 0x0031FB90
		internal string UserEntry
		{
			get
			{
				return ((string)base.ViewState["UserEntry"]) ?? string.Empty;
			}
			set
			{
				base.ViewState["UserEntry"] = value;
			}
		}

		// Token: 0x1700452A RID: 17706
		// (get) Token: 0x0600E17C RID: 57724 RVA: 0x003219A3 File Offset: 0x0031FBA3
		internal ICaptchaCachingProvider CachingProvider
		{
			get
			{
				if (object.Equals(null, this._cachingProvider))
				{
					this._cachingProvider = CachingProviderFactory.GetProviderByStorageType(this.CaptchaImageStoredIn).Provider;
				}
				return this._cachingProvider;
			}
		}

		// Token: 0x1700452B RID: 17707
		// (get) Token: 0x0600E17D RID: 57725 RVA: 0x003219CF File Offset: 0x0031FBCF
		// (set) Token: 0x0600E17E RID: 57726 RVA: 0x003219F0 File Offset: 0x0031FBF0
		public bool EnableMissingPluginNotification
		{
			get
			{
				return (bool)(base.ViewState["MissingPluginNotification"] ?? false);
			}
			set
			{
				base.ViewState["MissingPluginNotification"] = value;
			}
		}

		// Token: 0x1700452C RID: 17708
		// (get) Token: 0x0600E17F RID: 57727 RVA: 0x00321A08 File Offset: 0x0031FC08
		// (set) Token: 0x0600E180 RID: 57728 RVA: 0x00321A28 File Offset: 0x0031FC28
		public string MissingPluginText
		{
			get
			{
				return ((string)base.ViewState["MissingPluginText"]) ?? "Missing Browser Plug-In";
			}
			set
			{
				if (value != null && !string.IsNullOrEmpty(value.Trim()))
				{
					base.ViewState["MissingPluginText"] = value;
				}
			}
		}

		// Token: 0x1700452D RID: 17709
		// (get) Token: 0x0600E181 RID: 57729 RVA: 0x00321A4B File Offset: 0x0031FC4B
		// (set) Token: 0x0600E182 RID: 57730 RVA: 0x00321A6C File Offset: 0x0031FC6C
		public bool EnableDownloadAudio
		{
			get
			{
				return (bool)(base.ViewState["EnableDownloadAudio"] ?? false);
			}
			set
			{
				base.ViewState["EnableDownloadAudio"] = value;
			}
		}

		// Token: 0x1700452E RID: 17710
		// (get) Token: 0x0600E183 RID: 57731 RVA: 0x00321A84 File Offset: 0x0031FC84
		// (set) Token: 0x0600E184 RID: 57732 RVA: 0x00321AA4 File Offset: 0x0031FCA4
		public string DownloadAudioText
		{
			get
			{
				return ((string)base.ViewState["DownloadAudioText"]) ?? "Download Audio Code";
			}
			set
			{
				if (value != null && !string.IsNullOrEmpty(value.Trim()))
				{
					base.ViewState["DownloadAudioText"] = value;
				}
			}
		}

		// Token: 0x1700452F RID: 17711
		// (get) Token: 0x0600E185 RID: 57733 RVA: 0x00321AC7 File Offset: 0x0031FCC7
		public WebControlDecorator TextBoxDecoration
		{
			get
			{
				if (this._textBoxDecoration == null)
				{
					this._textBoxDecoration = new WebControlDecorator(this.captchaTextBox);
				}
				return this._textBoxDecoration;
			}
		}

		// Token: 0x17004530 RID: 17712
		// (get) Token: 0x0600E186 RID: 57734 RVA: 0x00321AE8 File Offset: 0x0031FCE8
		public WebControlDecorator TextBoxLabelDecoration
		{
			get
			{
				if (this._textBoxLabelDecoration == null)
				{
					this._textBoxLabelDecoration = new WebControlDecorator(this.captchaTextBoxLabel);
				}
				return this._textBoxLabelDecoration;
			}
		}

		// Token: 0x0600E187 RID: 57735 RVA: 0x00321B0C File Offset: 0x0031FD0C
		public CaptchaProtector()
		{
		}

		// Token: 0x0600E188 RID: 57736 RVA: 0x00321BC5 File Offset: 0x0031FDC5
		public CaptchaProtector(string handlerUrl) : this()
		{
			this.imageHandlerUrl = handlerUrl;
		}

		// Token: 0x0600E189 RID: 57737 RVA: 0x00321BD4 File Offset: 0x0031FDD4
		public void AddChildControls(Control container)
		{
			this.ProcessUpdatePanel(container);
			this.ProcessIndividualImage(container);
			this.ProcessAudioLink(container, this.captchaUpdatePanel);
			this.ProcessDownloadAudioLink(container, this.captchaUpdatePanel);
			this.ProcessPluginNotificationPanel(container);
			this.ProcessTextBox(container);
		}

		// Token: 0x0600E18A RID: 57738 RVA: 0x00321C0C File Offset: 0x0031FE0C
		private void ProcessPluginNotificationPanel(Control container)
		{
			this.pluginNotificationPanel.ID = "PluginNotificationPanel";
			this.pluginNotificationPanel.CssClass = "rcPluginNotification";
			this.ProcessPluginLink(this.pluginNotificationPanel);
			container.Controls.Add(this.pluginNotificationPanel);
		}

		// Token: 0x0600E18B RID: 57739 RVA: 0x00321C4B File Offset: 0x0031FE4B
		private void ProcessPluginLink(Control container)
		{
			this.pluginLink.ID = "MissingPluginLink";
			this.pluginLink.HRef = "http://www.apple.com/quicktime/download/";
			container.Controls.Add(this.pluginLink);
		}

		// Token: 0x0600E18C RID: 57740 RVA: 0x00321C80 File Offset: 0x0031FE80
		private void ProcessUpdatePanel(Control container)
		{
			this.imageUpdatePanel.ID = "CaptchaImageUP";
			if (this.CaptchaImage.ImageAlternativeText == null)
			{
				this.imageUpdatePanel.Attributes.Add("alt", "");
			}
			else
			{
				this.imageUpdatePanel.Attributes.Add("alt", this.CaptchaImage.ImageAlternativeText);
			}
			this.imageUpdatePanel.Style.Add(HtmlTextWriterStyle.Display, "block");
			this.btnNewImage.ID = "CaptchaLinkButton";
			this.btnNewImage.CssClass = "rcRefreshImage";
			this.btnNewImage.CausesValidation = false;
			this.btnNewImage.Style.Add(HtmlTextWriterStyle.Display, "block");
			this.btnNewImage.AccessKey = this.RefreshImageAccessKey;
			this.captchaUpdatePanel.UpdateMode = UpdatePanelUpdateMode.Conditional;
			AsyncPostBackTrigger asyncPostBackTrigger = new AsyncPostBackTrigger();
			asyncPostBackTrigger.ControlID = this.btnNewImage.ID;
			asyncPostBackTrigger.EventName = "Click";
			this.captchaUpdatePanel.Triggers.Add(asyncPostBackTrigger);
			this.captchaUpdatePanel.ContentTemplateContainer.Controls.Add(this.imageUpdatePanel);
			this.captchaUpdatePanel.ContentTemplateContainer.Controls.Add(this.btnNewImage);
			container.Controls.Add(this.captchaUpdatePanel);
		}

		// Token: 0x0600E18D RID: 57741 RVA: 0x00321DD8 File Offset: 0x0031FFD8
		private void ProcessIndividualImage(Control container)
		{
			this.image.ID = "CaptchaImage";
			if (this.CaptchaImage.ImageAlternativeText == null)
			{
				this.image.Attributes.Add("alt", "");
			}
			else
			{
				this.image.Attributes.Add("alt", this.CaptchaImage.ImageAlternativeText);
			}
			this.image.Style.Add(HtmlTextWriterStyle.Display, "block");
			container.Controls.Add(this.image);
		}

		// Token: 0x0600E18E RID: 57742 RVA: 0x00321E68 File Offset: 0x00320068
		private void ProcessTextBox(Control container)
		{
			this.captchaTextBox.ID = "CaptchaTextBox";
			this.captchaTextBox.EnableViewState = true;
			this.captchaTextBoxLabel.ID = "CaptchaTextBoxLabel";
			if (CaptchaProtector.IsDesignMode)
			{
				this.captchaTextBoxLabel.Text = this.TextBoxLabel;
			}
			this.captchaTextBoxLabel.AssociatedControlID = this.captchaTextBox.ID;
			this.pInput.Controls.Add(this.captchaTextBox);
			this.pInput.Controls.Add(this.captchaTextBoxLabel);
			container.Controls.Add(this.pInput);
		}

		// Token: 0x0600E18F RID: 57743 RVA: 0x00321F0C File Offset: 0x0032010C
		private void ProcessAudioLink(Control container, UpdatePanel updatePanel)
		{
			this.SetAudioLinkAttributes(this.audioLink, "CaptchaAudioCode");
			container.Controls.Add(this.audioLink);
			this.SetAudioLinkAttributes(this.audioLinkUP, "CaptchaAudioCodeUP");
			updatePanel.ContentTemplateContainer.Controls.Add(this.audioLinkUP);
		}

		// Token: 0x0600E190 RID: 57744 RVA: 0x00321F64 File Offset: 0x00320164
		private void ProcessDownloadAudioLink(Control container, UpdatePanel updatePanel)
		{
			this.SetDownloadAudioLinkAttributes(this.downloadAudioLink, "CaptchaDownloadAudioCode");
			container.Controls.Add(this.downloadAudioLink);
			this.SetDownloadAudioLinkAttributes(this.downloadAudioLinkUP, "CaptchaDownloadAudioCodeUP");
			updatePanel.ContentTemplateContainer.Controls.Add(this.downloadAudioLinkUP);
		}

		// Token: 0x0600E191 RID: 57745 RVA: 0x00321FBC File Offset: 0x003201BC
		private void SetDownloadAudioLinkAttributes(HtmlAnchor link, string id)
		{
			link.ID = id;
			link.Title = this.DownloadAudioText;
			link.Attributes.Add("class", "rcCaptchaDownloadAudioLink");
			link.Attributes.CssStyle.Add(HtmlTextWriterStyle.Display, "block");
		}

		// Token: 0x0600E192 RID: 57746 RVA: 0x00322008 File Offset: 0x00320208
		private void SetAudioLinkAttributes(HtmlAnchor link, string id)
		{
			link.ID = id;
			link.Title = this.AudioButtonText;
			link.Attributes.Add("accesskey", this.AudioAccessKey);
			link.Attributes.Add("class", "rcCaptchaAudioLink");
			link.Attributes.CssStyle.Add(HtmlTextWriterStyle.Display, "block");
		}

		// Token: 0x17004531 RID: 17713
		// (get) Token: 0x0600E193 RID: 57747 RVA: 0x0032206C File Offset: 0x0032026C
		// (set) Token: 0x0600E194 RID: 57748 RVA: 0x00322095 File Offset: 0x00320295
		public bool Visible
		{
			get
			{
				object obj = base.ViewState["Visible"];
				return obj == null || (bool)obj;
			}
			set
			{
				base.ViewState["Visible"] = value;
				this.ProcessVisibleControls(value);
			}
		}

		// Token: 0x0600E195 RID: 57749 RVA: 0x003220B4 File Offset: 0x003202B4
		private void ProcessVisibleControls(bool value)
		{
			if (this.CaptchaImage.RenderImageOnly)
			{
				this.image.Visible = (value && !this.EnableRefreshImage);
				this.captchaUpdatePanel.Visible = (value && this.EnableRefreshImage);
				this.pInput.Visible = false;
			}
			else
			{
				this.image.Visible = (value && !this.EnableRefreshImage);
				this.captchaUpdatePanel.Visible = (value && this.EnableRefreshImage);
				this.pInput.Visible = value;
			}
			this.pluginNotificationPanel.Visible = (this.CaptchaImage.EnableCaptchaAudio && this.EnableMissingPluginNotification && value);
			this.audioLink.Visible = (this.CaptchaImage.EnableCaptchaAudio && value && !this.EnableRefreshImage);
			this.audioLinkUP.Visible = (this.CaptchaImage.EnableCaptchaAudio && value && this.EnableRefreshImage);
			this.downloadAudioLink.Visible = (this.CaptchaImage.EnableCaptchaAudio && this.EnableDownloadAudio && value && !this.EnableRefreshImage);
			this.downloadAudioLinkUP.Visible = (this.CaptchaImage.EnableCaptchaAudio && this.EnableDownloadAudio && value && this.EnableRefreshImage);
		}

		// Token: 0x0600E196 RID: 57750 RVA: 0x00322214 File Offset: 0x00320414
		private string GetImageHandlerUrl()
		{
			string text = this.imageHandlerUrl;
			string text2 = (text.IndexOf('?') != -1) ? "&" : "?";
			string text3 = text;
			text = string.Concat(new string[]
			{
				text3,
				text2,
				HandlerRouter.HandlerUrlKey,
				"=",
				RadCaptcha.HandlerRouterKey
			});
			if (!CaptchaProtector.IsDesignMode)
			{
				string str = "true";
				if (this.CaptchaImageStoredIn == CaptchaImageStorage.Session)
				{
					str = "false";
				}
				else if (this.CaptchaImageStoredIn == CaptchaImageStorage.Custom)
				{
					str = "cust";
				}
				text = text + "&isc=" + str;
				text = text + "&guid=" + Convert.ToString(this.CaptchaImage.UniqueId);
			}
			return text;
		}

		// Token: 0x0600E197 RID: 57751 RVA: 0x003222D0 File Offset: 0x003204D0
		internal string GetAudioHandlerUrl()
		{
			string text = this.imageHandlerUrl;
			string text2 = (text.IndexOf('?') != -1) ? "&" : "?";
			string text3 = text;
			text = string.Concat(new string[]
			{
				text3,
				text2,
				HandlerRouter.HandlerUrlKey,
				"=",
				RadCaptcha.HandlerRouterKeyCaptchaAudio
			});
			if (!CaptchaProtector.IsDesignMode)
			{
				string str = "true";
				if (this.CaptchaImageStoredIn == CaptchaImageStorage.Session)
				{
					str = "false";
				}
				else if (this.CaptchaImageStoredIn == CaptchaImageStorage.Custom)
				{
					str = "cust";
				}
				text = text + "&isc=" + str;
				text = text + "&guid=" + Convert.ToString(this.CaptchaImage.UniqueId);
			}
			return text;
		}

		// Token: 0x0600E198 RID: 57752 RVA: 0x0032238C File Offset: 0x0032058C
		public void LoadPostBackData(Control container)
		{
			string text = string.Empty;
			if (this.captchaImage.RenderImageOnly)
			{
				text = this.UserEntry;
			}
			else
			{
				TextBox textBox = (TextBox)container.FindControl("CaptchaTextBox");
				if (textBox != null)
				{
					text = textBox.Text;
					textBox.Text = string.Empty;
				}
			}
			this.postData = text;
		}

		// Token: 0x0600E199 RID: 57753 RVA: 0x003223E2 File Offset: 0x003205E2
		public void ValidatePostBackData()
		{
			this.ValidateCaptcha(this.postData);
		}

		// Token: 0x0600E19A RID: 57754 RVA: 0x003223F0 File Offset: 0x003205F0
		public void PreRenderHandler()
		{
			if (this.EnableRefreshImage)
			{
				this.captchaUpdatePanel.Visible = true;
				this.image.Visible = false;
			}
			else
			{
				this.captchaUpdatePanel.Visible = false;
				this.image.Visible = true;
			}
			this.captchaImage.OnPreRender(true);
			this.image.Height = (this.imageUpdatePanel.Height = this.CaptchaImage.Height);
			this.image.Width = (this.imageUpdatePanel.Width = this.CaptchaImage.Width);
			this.image.CssClass = (this.imageUpdatePanel.CssClass = this.CaptchaImage.ImageCssClass);
			this.image.ImageUrl = (this.imageUpdatePanel.ImageUrl = this.GetImageHandlerUrl());
			this.captchaTextBox.CssClass = ((!string.IsNullOrEmpty(this.TextBoxDecoration.CssClass)) ? this.TextBoxDecoration.CssClass : this.TextBoxCssClass);
			this.captchaTextBox.AccessKey = ((!string.IsNullOrEmpty(this.TextBoxAccessKey)) ? this.TextBoxAccessKey : this.TextBoxDecoration.AccessKey);
			this.captchaTextBox.TabIndex = ((this.TextBoxTabIndex != 0) ? this.TextBoxTabIndex : this.TextBoxDecoration.TabIndex);
			this.captchaTextBox.ToolTip = ((!string.IsNullOrEmpty(this.TextBoxDecoration.ToolTip)) ? this.TextBoxDecoration.ToolTip : this.TextBoxTitle);
			this.captchaTextBoxLabel.CssClass = ((!string.IsNullOrEmpty(this.TextBoxLabelDecoration.CssClass)) ? this.TextBoxLabelDecoration.CssClass : this.TextBoxLabelCssClass);
			this.captchaTextBoxLabel.Text = this.TextBoxLabel;
			this.btnNewImage.Text = this.LinkButtonText;
			this.btnNewImage.Attributes.Add("title", this.LinkButtonText);
			this.pluginLink.InnerText = (this.pluginLink.Title = this.MissingPluginText);
			this.audioLink.InnerText = (this.audioLinkUP.InnerText = this.AudioButtonText);
			this.audioLink.HRef = (this.audioLinkUP.HRef = HttpUtility.HtmlEncode(this.GetAudioHandlerUrl()));
			this.downloadAudioLink.InnerText = (this.downloadAudioLinkUP.InnerText = this.DownloadAudioText);
			this.downloadAudioLink.HRef = (this.downloadAudioLinkUP.HRef = HttpUtility.HtmlEncode(this.GetAudioHandlerUrl()));
			this.ProcessVisibleControls(this.Visible);
			if (this.prevGuid != null)
			{
				CaptchaImage cachedCaptcha = this.GetCachedCaptcha(this.prevGuid);
				if (cachedCaptcha != null)
				{
					this.RemoveCachedCaptcha(this.prevGuid);
				}
			}
		}

		// Token: 0x0600E19B RID: 57755 RVA: 0x003226DC File Offset: 0x003208DC
		private CaptchaImage GetCachedCaptcha(string guid)
		{
			if (guid != null)
			{
				return this.CachingProvider.Load(guid);
			}
			return null;
		}

		// Token: 0x0600E19C RID: 57756 RVA: 0x003226F0 File Offset: 0x003208F0
		private CaptchaImage RetrieveCaptchaOnce(string guid)
		{
			if (string.IsNullOrEmpty(guid))
			{
				return null;
			}
			object once = Lockables.GetOnce(guid);
			if (once == null)
			{
				return null;
			}
			CaptchaImage cachedCaptcha;
			lock (once)
			{
				cachedCaptcha = this.GetCachedCaptcha(guid);
				this.RemoveCachedCaptcha(guid);
			}
			return cachedCaptcha;
		}

		// Token: 0x0600E19D RID: 57757 RVA: 0x0032274C File Offset: 0x0032094C
		private void RemoveCachedCaptcha(string guid)
		{
			this.CachingProvider.Clear(guid);
		}

		// Token: 0x17004532 RID: 17714
		// (get) Token: 0x0600E19E RID: 57758 RVA: 0x0032275A File Offset: 0x0032095A
		public static bool IsDesignMode
		{
			get
			{
				return HttpContext.Current == null;
			}
		}

		// Token: 0x0600E19F RID: 57759 RVA: 0x00322764 File Offset: 0x00320964
		private void ValidateCaptcha(string userEntry)
		{
			CaptchaImage captchaImage = this.RetrieveCaptchaOnce(this.prevGuid);
			this.isValid = (captchaImage != null && string.Compare(userEntry, captchaImage.Text, this.IsCaseIgnored) == 0);
		}

		// Token: 0x0600E1A0 RID: 57760 RVA: 0x003227A0 File Offset: 0x003209A0
		public void GenerateNewCaptcha()
		{
			if (!CaptchaProtector.IsDesignMode)
			{
				int num = (this.MaxTimeout == 0) ? 120 : this.MaxTimeout;
				DateTime absoluteExpiration = DateTime.Now.AddSeconds(Convert.ToDouble(num * 60));
				this.CachingProvider.Save(this.CaptchaImage.UniqueId, this.CaptchaImage);
				Lockables.Create(this.CaptchaImage.UniqueId);
				if (this.CachingProvider.ShouldAddCacheDependecy)
				{
					HttpContext.Current.Cache.Insert("CaptchaRemoval_" + this.CaptchaImage.UniqueId, this.CaptchaImage.UniqueId, null, absoluteExpiration, Cache.NoSlidingExpiration, CacheItemPriority.NotRemovable, new CacheItemRemovedCallback(this.CachingProvider.CacheExpirationCallback));
				}
			}
		}

		// Token: 0x0600E1A1 RID: 57761 RVA: 0x00322868 File Offset: 0x00320A68
		protected override void LoadViewState(object state)
		{
			base.LoadViewState(state);
			CaptchaImage cachedCaptcha = this.GetCachedCaptcha(this.CurrentGuid);
			if (cachedCaptcha != null)
			{
				this.captchaImage = new CaptchaImage(cachedCaptcha, cachedCaptcha.RandomGenerator, this.captchaImage.RenderedAt, this.captchaImage.UniqueId);
			}
		}

		// Token: 0x0600E1A2 RID: 57762 RVA: 0x003228B4 File Offset: 0x00320AB4
		protected override object SaveViewState()
		{
			if (this.captchaImage != null)
			{
				this.CurrentGuid = this.captchaImage.UniqueId;
			}
			return base.SaveViewState();
		}

		// Token: 0x0600E1A3 RID: 57763 RVA: 0x003228D5 File Offset: 0x00320AD5
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600E1A4 RID: 57764 RVA: 0x003228E4 File Offset: 0x00320AE4
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.captchaUpdatePanel.Dispose();
				this.image.Dispose();
				this.imageUpdatePanel.Dispose();
				this.pInput.Dispose();
				this.captchaTextBox.Dispose();
				this.captchaTextBoxLabel.Dispose();
				this.btnNewImage.Dispose();
				this.audioLink.Dispose();
				this.audioLinkUP.Dispose();
				this.downloadAudioLink.Dispose();
				this.downloadAudioLinkUP.Dispose();
				this.pluginLink.Dispose();
				this.pluginNotificationPanel.Dispose();
			}
		}

		// Token: 0x0400414F RID: 16719
		internal ICaptchaCachingProvider _cachingProvider;

		// Token: 0x04004150 RID: 16720
		private WebControlDecorator _textBoxDecoration;

		// Token: 0x04004151 RID: 16721
		private WebControlDecorator _textBoxLabelDecoration;

		// Token: 0x04004152 RID: 16722
		public string imageHandlerUrl = "~/Telerik.Web.UI.WebResource.axd";

		// Token: 0x04004153 RID: 16723
		private readonly UpdatePanel captchaUpdatePanel = new UpdatePanel();

		// Token: 0x04004154 RID: 16724
		private readonly Image image = new Image();

		// Token: 0x04004155 RID: 16725
		private readonly Image imageUpdatePanel = new Image();

		// Token: 0x04004156 RID: 16726
		private readonly HtmlGenericControl pInput = new HtmlGenericControl("p");

		// Token: 0x04004157 RID: 16727
		private readonly TextBox captchaTextBox = new TextBox();

		// Token: 0x04004158 RID: 16728
		private readonly Label captchaTextBoxLabel = new Label();

		// Token: 0x04004159 RID: 16729
		private readonly LinkButton btnNewImage = new LinkButton();

		// Token: 0x0400415A RID: 16730
		private readonly HtmlAnchor audioLink = new HtmlAnchor();

		// Token: 0x0400415B RID: 16731
		private readonly HtmlAnchor audioLinkUP = new HtmlAnchor();

		// Token: 0x0400415C RID: 16732
		private readonly HtmlAnchor downloadAudioLink = new HtmlAnchor();

		// Token: 0x0400415D RID: 16733
		private readonly HtmlAnchor downloadAudioLinkUP = new HtmlAnchor();

		// Token: 0x0400415E RID: 16734
		private readonly HtmlAnchor pluginLink = new HtmlAnchor();

		// Token: 0x0400415F RID: 16735
		private readonly Panel pluginNotificationPanel = new Panel();

		// Token: 0x04004160 RID: 16736
		private bool isValid = true;

		// Token: 0x04004161 RID: 16737
		private string postData;

		// Token: 0x04004162 RID: 16738
		private string prevGuid;

		// Token: 0x04004163 RID: 16739
		private CaptchaImage captchaImage;
	}
}
