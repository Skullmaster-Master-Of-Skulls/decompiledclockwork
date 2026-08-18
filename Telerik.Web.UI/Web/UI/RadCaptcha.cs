using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Licensing;

namespace Telerik.Web.UI
{
	// Token: 0x02000A3F RID: 2623
	[ToolboxBitmap(typeof(RadCaptcha), "Telerik.Web.UI.Captcha.png")]
	[Designer("Telerik.Web.Design.RadCaptchaDesigner, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4")]
	[TelerikToolboxCategory("Miscellaneous")]
	[ClientScriptResource("Telerik.Web.UI.RadCaptcha", "Telerik.Web.UI.Captcha.RadCaptcha.js")]
	[LicenseProvider(typeof(TelerikLicenseProvider))]
	[ToolboxData("<{0}:RadCaptcha Runat=server></{0}:RadCaptcha>")]
	public class RadCaptcha : RadWebControl, IValidator, INamingContainer, IPostBackDataHandler
	{
		// Token: 0x060063F9 RID: 25593 RVA: 0x00177A3C File Offset: 0x00175C3C
		protected internal override void DescribeClientProperties(IScriptDescriptor descriptor)
		{
			base.DescribeProperty<bool>(descriptor, "_enableDownloadAudio", this.EnableDownloadAudio, false);
			base.DescribeProperty<bool>(descriptor, "_enableMissingPluginNotification", this.EnableMissingPluginNotification, false);
			base.DescribeClientProperties(descriptor);
		}

		// Token: 0x060063FA RID: 25594 RVA: 0x00177A6B File Offset: 0x00175C6B
		protected internal override void DescribeClientEvents(IScriptDescriptor descriptor)
		{
			RadWebControl.DescribeEvent(descriptor, "load", this.OnClientLoad);
			base.DescribeClientEvents(descriptor);
		}

		// Token: 0x170020D2 RID: 8402
		// (get) Token: 0x060063FB RID: 25595 RVA: 0x00177A85 File Offset: 0x00175C85
		internal static string HandlerRouterKey
		{
			get
			{
				return "rca";
			}
		}

		// Token: 0x170020D3 RID: 8403
		// (get) Token: 0x060063FC RID: 25596 RVA: 0x00177A8C File Offset: 0x00175C8C
		internal static string HandlerRouterKeyCaptchaAudio
		{
			get
			{
				return "cah";
			}
		}

		// Token: 0x170020D4 RID: 8404
		// (get) Token: 0x060063FD RID: 25597 RVA: 0x00177A93 File Offset: 0x00175C93
		[Description("This control features no skins, so this property must be set to false.")]
		[Browsable(false)]
		[DefaultValue(false)]
		public override bool EnableEmbeddedBaseStylesheet
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170020D5 RID: 8405
		// (get) Token: 0x060063FE RID: 25598 RVA: 0x00177A96 File Offset: 0x00175C96
		[DefaultValue(false)]
		[Description("This control features no skins, so this property must be set to false.")]
		[Browsable(false)]
		public override bool EnableEmbeddedSkins
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170020D6 RID: 8406
		// (get) Token: 0x060063FF RID: 25599 RVA: 0x00177A99 File Offset: 0x00175C99
		// (set) Token: 0x06006400 RID: 25600 RVA: 0x00177AA6 File Offset: 0x00175CA6
		[Category("Appearance")]
		[Localizable(true)]
		[Description("Message to display in a Validation Summary when the RadCaptcha fails to validate.")]
		[Bindable(true)]
		[DefaultValue("")]
		public string ErrorMessage
		{
			get
			{
				return this.captchaBaseValidator.ErrorMessage;
			}
			set
			{
				this.captchaBaseValidator.ErrorMessage = value;
			}
		}

		// Token: 0x170020D7 RID: 8407
		// (get) Token: 0x06006401 RID: 25601 RVA: 0x00177AB4 File Offset: 0x00175CB4
		// (set) Token: 0x06006402 RID: 25602 RVA: 0x00177AC1 File Offset: 0x00175CC1
		[DefaultValue(ValidatorDisplay.Static)]
		[Description("Gets or Sets the display behavior of the error message.")]
		[Category("Appearance")]
		public ValidatorDisplay Display
		{
			get
			{
				return this.captchaBaseValidator.Display;
			}
			set
			{
				this.captchaBaseValidator.Display = value;
			}
		}

		// Token: 0x170020D8 RID: 8408
		// (get) Token: 0x06006403 RID: 25603 RVA: 0x00177ACF File Offset: 0x00175CCF
		// (set) Token: 0x06006404 RID: 25604 RVA: 0x00177ADC File Offset: 0x00175CDC
		[Category("Appearance")]
		[Description("Gets or Sets the fore color of the error message.")]
		public override Color ForeColor
		{
			get
			{
				return this.captchaBaseValidator.ForeColor;
			}
			set
			{
				this.captchaBaseValidator.ForeColor = value;
			}
		}

		// Token: 0x170020D9 RID: 8409
		// (get) Token: 0x06006405 RID: 25605 RVA: 0x00177AEA File Offset: 0x00175CEA
		// (set) Token: 0x06006406 RID: 25606 RVA: 0x00177AF7 File Offset: 0x00175CF7
		[DefaultValue(true)]
		[Category("Behavior")]
		[Description("Gets or sets a value indicating whether the user-entered content in the RadCaptcha control passes validation.")]
		public bool IsValid
		{
			get
			{
				return this.captchaBaseValidator.IsValid;
			}
			set
			{
				this.captchaBaseValidator.IsValid = value;
			}
		}

		// Token: 0x170020DA RID: 8410
		// (get) Token: 0x06006407 RID: 25607 RVA: 0x00177B05 File Offset: 0x00175D05
		// (set) Token: 0x06006408 RID: 25608 RVA: 0x00177B25 File Offset: 0x00175D25
		[Description("Validation group name used to bind the Spam Protector to a button for validation.")]
		[Category("Behavior")]
		[DefaultValue("")]
		public string ValidationGroup
		{
			get
			{
				return ((string)this.ViewState["ValidationGroup"]) ?? "";
			}
			set
			{
				this.ViewState["ValidationGroup"] = value;
			}
		}

		// Token: 0x170020DB RID: 8411
		// (get) Token: 0x06006409 RID: 25609 RVA: 0x00177B38 File Offset: 0x00175D38
		// (set) Token: 0x0600640A RID: 25610 RVA: 0x00177B40 File Offset: 0x00175D40
		[Category("Behavior")]
		[Description("Gets or sets a value indicating whether the Web server control is enabled.")]
		[DefaultValue(true)]
		public new bool Enabled
		{
			get
			{
				return base.Enabled;
			}
			set
			{
				base.Enabled = value;
				if (!value)
				{
					this.IsValid = true;
				}
			}
		}

		// Token: 0x170020DC RID: 8412
		// (get) Token: 0x0600640B RID: 25611 RVA: 0x00177B53 File Offset: 0x00175D53
		// (set) Token: 0x0600640C RID: 25612 RVA: 0x00177B7E File Offset: 0x00175D7E
		[Category("Behavior")]
		[Description("Modes used for Spam Protection")]
		[DefaultValue(RadCaptcha.ProtectionStrategies.Captcha)]
		public RadCaptcha.ProtectionStrategies ProtectionMode
		{
			get
			{
				if (this.ViewState["ProtectionMode"] == null)
				{
					return RadCaptcha.ProtectionStrategies.Captcha;
				}
				return (RadCaptcha.ProtectionStrategies)this.ViewState["ProtectionMode"];
			}
			set
			{
				this.ViewState["ProtectionMode"] = value;
				this.ProcessSpamProtectors();
			}
		}

		// Token: 0x170020DD RID: 8413
		// (get) Token: 0x0600640D RID: 25613 RVA: 0x00177B9C File Offset: 0x00175D9C
		// (set) Token: 0x0600640E RID: 25614 RVA: 0x00177BBC File Offset: 0x00175DBC
		[Category("Behavior")]
		[Description("Specifies the URL of the HTTPHandler that serves the captcha image.")]
		[DefaultValue("~/Telerik.Web.UI.WebResource.axd")]
		public string HttpHandlerUrl
		{
			get
			{
				return ((string)this.ViewState["HandlerUrl"]) ?? "~/Telerik.Web.UI.WebResource.axd";
			}
			set
			{
				if (!VirtualPathUtility.IsAppRelative(value))
				{
					throw WebResource.GetHttpHandlerUrlNotAppRelative();
				}
				this.ViewState["HandlerUrl"] = value;
				this.captcha.imageHandlerUrl = value;
			}
		}

		// Token: 0x170020DE RID: 8414
		// (get) Token: 0x0600640F RID: 25615 RVA: 0x00177BE9 File Offset: 0x00175DE9
		// (set) Token: 0x06006410 RID: 25616 RVA: 0x00177C09 File Offset: 0x00175E09
		[Category("Client-side events")]
		[Description("Gets or sets the name of the JavaScript function that will be called when the RadCaptcha is loaded on the page.")]
		[ClientPropertyName("load")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[DefaultValue("")]
		public string OnClientLoad
		{
			get
			{
				return ((string)this.ViewState["OnClientLoad"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["OnClientLoad"] = value;
			}
		}

		// Token: 0x170020DF RID: 8415
		// (get) Token: 0x06006411 RID: 25617 RVA: 0x00177C1C File Offset: 0x00175E1C
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Category("Captcha")]
		[Description("Used to set properties for the captcha image object.")]
		public CaptchaImage CaptchaImage
		{
			get
			{
				return this.captcha.CaptchaImage;
			}
		}

		// Token: 0x170020E0 RID: 8416
		// (get) Token: 0x06006412 RID: 25618 RVA: 0x00177C29 File Offset: 0x00175E29
		// (set) Token: 0x06006413 RID: 25619 RVA: 0x00177C36 File Offset: 0x00175E36
		[DefaultValue(20)]
		[Description("Maximum number of minutes RadCaptcha will be cached and valid. If you're too slow, you may be a RadCaptcha hack attempt. Set to zero to disable.")]
		[Category("Captcha")]
		public int CaptchaMaxTimeout
		{
			get
			{
				return this.captcha.MaxTimeout;
			}
			set
			{
				this.captcha.MaxTimeout = value;
			}
		}

		// Token: 0x170020E1 RID: 8417
		// (get) Token: 0x06006414 RID: 25620 RVA: 0x00177C44 File Offset: 0x00175E44
		// (set) Token: 0x06006415 RID: 25621 RVA: 0x00177C51 File Offset: 0x00175E51
		[Description("Defines whether notification panel for missing audio plug-in should be displayed if one is not installed on the client's machine.")]
		[DefaultValue(false)]
		[Category("Captcha")]
		[ClientPropertyName("_enableMissingPluginNotification")]
		[ClientControlProperty]
		public bool EnableMissingPluginNotification
		{
			get
			{
				return this.captcha.EnableMissingPluginNotification;
			}
			set
			{
				this.captcha.EnableMissingPluginNotification = value;
			}
		}

		// Token: 0x170020E2 RID: 8418
		// (get) Token: 0x06006416 RID: 25622 RVA: 0x00177C5F File Offset: 0x00175E5F
		// (set) Token: 0x06006417 RID: 25623 RVA: 0x00177C6C File Offset: 0x00175E6C
		[Description("Gets or sets the text displayed in the MissingPlugin download link.")]
		[Category("Captcha")]
		[DefaultValue("Missing Browser Plug-In")]
		public string MissingPluginText
		{
			get
			{
				return this.captcha.MissingPluginText;
			}
			set
			{
				this.captcha.MissingPluginText = value;
			}
		}

		// Token: 0x170020E3 RID: 8419
		// (get) Token: 0x06006418 RID: 25624 RVA: 0x00177C7A File Offset: 0x00175E7A
		// (set) Token: 0x06006419 RID: 25625 RVA: 0x00177C87 File Offset: 0x00175E87
		[DefaultValue(false)]
		[ClientControlProperty]
		[ClientPropertyName("_enableDownloadAudio")]
		[Description("Defines whether Download Audio Code link should be rendered.")]
		[Category("Captcha")]
		public bool EnableDownloadAudio
		{
			get
			{
				return this.captcha.EnableDownloadAudio;
			}
			set
			{
				this.captcha.EnableDownloadAudio = value;
			}
		}

		// Token: 0x170020E4 RID: 8420
		// (get) Token: 0x0600641A RID: 25626 RVA: 0x00177C95 File Offset: 0x00175E95
		// (set) Token: 0x0600641B RID: 25627 RVA: 0x00177CA2 File Offset: 0x00175EA2
		[Category("Captcha")]
		[Description("Gets or sets the text displayed in the Download Audio Code link.")]
		[DefaultValue("Download Audio Code")]
		public string DownloadAudioText
		{
			get
			{
				return this.captcha.DownloadAudioText;
			}
			set
			{
				this.captcha.DownloadAudioText = value;
			}
		}

		// Token: 0x170020E5 RID: 8421
		// (get) Token: 0x0600641C RID: 25628 RVA: 0x00177CB0 File Offset: 0x00175EB0
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public WebControlDecorator TextBoxDecoration
		{
			get
			{
				return this.captcha.TextBoxDecoration;
			}
		}

		// Token: 0x170020E6 RID: 8422
		// (get) Token: 0x0600641D RID: 25629 RVA: 0x00177CBD File Offset: 0x00175EBD
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public WebControlDecorator TextBoxLabelDecoration
		{
			get
			{
				return this.captcha.TextBoxLabelDecoration;
			}
		}

		// Token: 0x170020E7 RID: 8423
		// (get) Token: 0x0600641E RID: 25630 RVA: 0x00177CCA File Offset: 0x00175ECA
		// (set) Token: 0x0600641F RID: 25631 RVA: 0x00177CD7 File Offset: 0x00175ED7
		[Obsolete("This property is obsolete. Please use the new composite TextBoxDecoration property.")]
		[Description("The CSS class applied to the RadCaptcha input textbox.")]
		[Category("Captcha")]
		[DefaultValue("")]
		public string CaptchaTextBoxCssClass
		{
			get
			{
				return this.captcha.TextBoxCssClass;
			}
			set
			{
				this.captcha.TextBoxCssClass = value;
			}
		}

		// Token: 0x170020E8 RID: 8424
		// (get) Token: 0x06006420 RID: 25632 RVA: 0x00177CE5 File Offset: 0x00175EE5
		// (set) Token: 0x06006421 RID: 25633 RVA: 0x00177CF2 File Offset: 0x00175EF2
		[Obsolete("This property is obsolete. Please use the new composite TextBoxDecoration property.")]
		[Category("Captcha")]
		[DefaultValue("")]
		[Description("The title for the RadCaptcha input textbox.")]
		public string CaptchaTextBoxTitle
		{
			get
			{
				return this.captcha.TextBoxTitle;
			}
			set
			{
				this.captcha.TextBoxTitle = value;
			}
		}

		// Token: 0x170020E9 RID: 8425
		// (get) Token: 0x06006422 RID: 25634 RVA: 0x00177D00 File Offset: 0x00175F00
		// (set) Token: 0x06006423 RID: 25635 RVA: 0x00177D0D File Offset: 0x00175F0D
		[Description("The tabindex of the RadCaptcha text box.")]
		public override short TabIndex
		{
			get
			{
				return this.captcha.TextBoxTabIndex;
			}
			set
			{
				this.captcha.TextBoxTabIndex = value;
			}
		}

		// Token: 0x170020EA RID: 8426
		// (get) Token: 0x06006424 RID: 25636 RVA: 0x00177D1B File Offset: 0x00175F1B
		// (set) Token: 0x06006425 RID: 25637 RVA: 0x00177D28 File Offset: 0x00175F28
		[Description("The RadCaptcha text box access key.")]
		[DefaultValue("")]
		public override string AccessKey
		{
			get
			{
				return this.captcha.TextBoxAccessKey;
			}
			set
			{
				this.captcha.TextBoxAccessKey = value;
			}
		}

		// Token: 0x170020EB RID: 8427
		// (get) Token: 0x06006426 RID: 25638 RVA: 0x00177D36 File Offset: 0x00175F36
		// (set) Token: 0x06006427 RID: 25639 RVA: 0x00177D43 File Offset: 0x00175F43
		[Description("The RadCaptcha Label which explains that the user needs to input the RadCaptcha text box.")]
		[DefaultValue("Type the code from the image")]
		[Category("Captcha")]
		[Localizable(true)]
		public string CaptchaTextBoxLabel
		{
			get
			{
				return this.captcha.TextBoxLabel;
			}
			set
			{
				this.captcha.TextBoxLabel = value;
			}
		}

		// Token: 0x170020EC RID: 8428
		// (get) Token: 0x06006428 RID: 25640 RVA: 0x00177D51 File Offset: 0x00175F51
		// (set) Token: 0x06006429 RID: 25641 RVA: 0x00177D5E File Offset: 0x00175F5E
		[DefaultValue("")]
		[Description("The CSS class to the label which explains that the user needs to input the RadCaptcha text box.")]
		[Category("Captcha")]
		[Obsolete("This property is obsolete. Please use the new composite TextBoxLabelDecoration property.")]
		public string CaptchaTextBoxLabelCssClass
		{
			get
			{
				return this.captcha.TextBoxLabelCssClass;
			}
			set
			{
				this.captcha.TextBoxLabelCssClass = value;
			}
		}

		// Token: 0x170020ED RID: 8429
		// (get) Token: 0x0600642A RID: 25642 RVA: 0x00177D6C File Offset: 0x00175F6C
		// (set) Token: 0x0600642B RID: 25643 RVA: 0x00177D8C File Offset: 0x00175F8C
		[DefaultValue("")]
		[Description("Gets or sets the ID of the textbox to be validated, when only the RadCaptcha image is rendered on the page.")]
		[Category("Captcha")]
		public string ValidatedTextBoxID
		{
			get
			{
				return ((string)this.ViewState["ValidatedTextBoxID"]) ?? "";
			}
			set
			{
				this.ViewState["ValidatedTextBoxID"] = value;
			}
		}

		// Token: 0x170020EE RID: 8430
		// (get) Token: 0x0600642C RID: 25644 RVA: 0x00177D9F File Offset: 0x00175F9F
		[Category("Captcha")]
		[Description("Gets the TextBox that is being validated by the RadCaptcha.")]
		[Browsable(false)]
		public TextBox ValidatedTextBox
		{
			get
			{
				return this.FindTextBox() as TextBox;
			}
		}

		// Token: 0x170020EF RID: 8431
		// (get) Token: 0x0600642D RID: 25645 RVA: 0x00177DAC File Offset: 0x00175FAC
		[Description("Gets the ITextControl that is being validated by the RadCaptcha.")]
		[Category("Captcha")]
		public ITextControl ValidatedTextControl
		{
			get
			{
				return this.FindTextBox() as ITextControl;
			}
		}

		// Token: 0x0600642E RID: 25646 RVA: 0x00177DB9 File Offset: 0x00175FB9
		private Control FindTextBox()
		{
			if (this.Page != null)
			{
				return this.NamingContainer.FindControl(this.ValidatedTextBoxID);
			}
			return null;
		}

		// Token: 0x170020F0 RID: 8432
		// (get) Token: 0x0600642F RID: 25647 RVA: 0x00177DD6 File Offset: 0x00175FD6
		// (set) Token: 0x06006430 RID: 25648 RVA: 0x00177DE3 File Offset: 0x00175FE3
		[Category("Captcha")]
		[DefaultValue(true)]
		[Description("Gets or sets a bool value indicating whether the RadCaptcha should ignore the case of the letters or not.")]
		public bool IgnoreCase
		{
			get
			{
				return this.captcha.IsCaseIgnored;
			}
			set
			{
				this.captcha.IsCaseIgnored = value;
			}
		}

		// Token: 0x170020F1 RID: 8433
		// (get) Token: 0x06006431 RID: 25649 RVA: 0x00177DF1 File Offset: 0x00175FF1
		// (set) Token: 0x06006432 RID: 25650 RVA: 0x00177DFE File Offset: 0x00175FFE
		[DefaultValue(CaptchaImageStorage.Cache)]
		[Description("Gets or sets a value indicating where the CaptchaImage is stored.")]
		public CaptchaImageStorage ImageStorageLocation
		{
			get
			{
				return this.captcha.CaptchaImageStoredIn;
			}
			set
			{
				this.captcha.CaptchaImageStoredIn = value;
			}
		}

		// Token: 0x170020F2 RID: 8434
		// (get) Token: 0x06006433 RID: 25651 RVA: 0x00177E0C File Offset: 0x0017600C
		// (set) Token: 0x06006434 RID: 25652 RVA: 0x00177E19 File Offset: 0x00176019
		[DefaultValue(false)]
		[Description("Gets or sets a bool value indicating whether or not the RadCaptchaImage can be refreshed. The 'rcRefreshImage' CSS class should be used for changing the skinning of the LinkButton, that generates the new image.")]
		[Category("Captcha")]
		public bool EnableRefreshImage
		{
			get
			{
				return this.captcha.EnableRefreshImage;
			}
			set
			{
				this.captcha.EnableRefreshImage = value;
			}
		}

		// Token: 0x170020F3 RID: 8435
		// (get) Token: 0x06006435 RID: 25653 RVA: 0x00177E27 File Offset: 0x00176027
		// (set) Token: 0x06006436 RID: 25654 RVA: 0x00177E34 File Offset: 0x00176034
		[Description("Gets or sets the access key for generating new captcha image.")]
		public string RefreshImageAccessKey
		{
			get
			{
				return this.captcha.RefreshImageAccessKey;
			}
			set
			{
				this.captcha.RefreshImageAccessKey = value;
			}
		}

		// Token: 0x170020F4 RID: 8436
		// (get) Token: 0x06006437 RID: 25655 RVA: 0x00177E42 File Offset: 0x00176042
		// (set) Token: 0x06006438 RID: 25656 RVA: 0x00177E4F File Offset: 0x0017604F
		[Description("Gets or sets the access key for the Get Audio Code link.")]
		public string AudioAccessKey
		{
			get
			{
				return this.captcha.AudioAccessKey;
			}
			set
			{
				this.captcha.AudioAccessKey = value;
			}
		}

		// Token: 0x170020F5 RID: 8437
		// (get) Token: 0x06006439 RID: 25657 RVA: 0x00177E5D File Offset: 0x0017605D
		// (set) Token: 0x0600643A RID: 25658 RVA: 0x00177E6A File Offset: 0x0017606A
		[Description("Gets or sets the text of the LinkButton that generates new CaptchaImage.")]
		[Category("Captcha")]
		[DefaultValue("Generate New Image")]
		[Localizable(true)]
		public string CaptchaLinkButtonText
		{
			get
			{
				return this.captcha.LinkButtonText;
			}
			set
			{
				this.captcha.LinkButtonText = value;
			}
		}

		// Token: 0x170020F6 RID: 8438
		// (get) Token: 0x0600643B RID: 25659 RVA: 0x00177E78 File Offset: 0x00176078
		// (set) Token: 0x0600643C RID: 25660 RVA: 0x00177E85 File Offset: 0x00176085
		[Localizable(true)]
		[Category("Captcha")]
		[Description("Gets or sets the text of the LinkButton that gets the Captcha Audio Code.")]
		[DefaultValue("Get Audio Code")]
		public string CaptchaAudioLinkButtonText
		{
			get
			{
				return this.captcha.AudioButtonText;
			}
			set
			{
				this.captcha.AudioButtonText = value;
			}
		}

		// Token: 0x170020F7 RID: 8439
		// (get) Token: 0x0600643D RID: 25661 RVA: 0x00177E93 File Offset: 0x00176093
		// (set) Token: 0x0600643E RID: 25662 RVA: 0x00177EA5 File Offset: 0x001760A5
		[Description("Invisible text box strategy label text.")]
		[Category("Auto Discovery - Invisible TextBox")]
		[DefaultValue("Do not fill this textbox.")]
		public string InvisibleTextBoxLabel
		{
			get
			{
				return this.autoBotFind.InvisibleTextBoxStrat.LabelText;
			}
			set
			{
				this.autoBotFind.InvisibleTextBoxStrat.LabelText = value;
			}
		}

		// Token: 0x170020F8 RID: 8440
		// (get) Token: 0x0600643F RID: 25663 RVA: 0x00177EB8 File Offset: 0x001760B8
		// (set) Token: 0x06006440 RID: 25664 RVA: 0x00177ECA File Offset: 0x001760CA
		[Category("Auto Discovery - Min Submission Time")]
		[DefaultValue(3)]
		[Description("Minimum number of seconds the form must be displayed before it is valid. If you're too fast, you must be a robot.")]
		public int MinTimeout
		{
			get
			{
				return this.autoBotFind.MinSubmTimeStrat.MinTimeout;
			}
			set
			{
				this.autoBotFind.MinSubmTimeStrat.MinTimeout = value;
			}
		}

		// Token: 0x170020F9 RID: 8441
		// (get) Token: 0x06006441 RID: 25665 RVA: 0x00177EDD File Offset: 0x001760DD
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Div;
			}
		}

		// Token: 0x170020FA RID: 8442
		// (get) Token: 0x06006442 RID: 25666 RVA: 0x00177EE1 File Offset: 0x001760E1
		protected override string CssClassFormatString
		{
			get
			{
				return "RadCaptcha RadCaptcha_{0} " + this._validationCssClass;
			}
		}

		// Token: 0x06006443 RID: 25667 RVA: 0x00177EF4 File Offset: 0x001760F4
		protected override void Render(HtmlTextWriter writer)
		{
			if (this.IsModeEnabled(RadCaptcha.ProtectionStrategies.Captcha) && this.CaptchaImage != null)
			{
				this.captcha.Visible = true;
				TextBox textBox;
				if (this.CaptchaImage.RenderImageOnly)
				{
					textBox = this.ValidatedTextBox;
				}
				else
				{
					textBox = (TextBox)this.FindControl("CaptchaTextBox");
				}
				if (textBox != null)
				{
					textBox.MaxLength = this.CaptchaImage.TextLength;
				}
			}
			else
			{
				this.captcha.Visible = false;
			}
			if (base.DesignMode)
			{
				if (this.HasControls())
				{
					for (int i = 0; i < this.Controls.Count; i++)
					{
						if (this.Controls[i] is Panel)
						{
							Control control = this.Controls[i];
							for (int j = 0; j < control.Controls.Count; j++)
							{
								Control control2 = control.Controls[j];
								System.Web.UI.WebControls.Image image = control2 as System.Web.UI.WebControls.Image;
								if (image != null)
								{
									image.Visible = true;
									image.ImageUrl = this.Page.ClientScript.GetWebResourceUrl(typeof(RadCaptcha), "Telerik.Web.UI.Skins.Common.Captcha.DesignTime.gif");
									control.Controls[j].RenderControl(writer);
									if (this.EnableRefreshImage)
									{
										writer.Write("<a href='#'>" + this.CaptchaLinkButtonText + "</a><br/>");
									}
									if (this.CaptchaImage.EnableCaptchaAudio)
									{
										writer.Write("<a href='#'>" + this.CaptchaAudioLinkButtonText + "</a><br/>");
									}
								}
								else if (!(control2 is UpdatePanel))
								{
									control.Controls[j].RenderControl(writer);
								}
							}
						}
						else
						{
							this.Controls[i].RenderControl(writer);
						}
					}
					return;
				}
			}
			else
			{
				base.Render(writer);
			}
		}

		// Token: 0x06006444 RID: 25668 RVA: 0x001780BC File Offset: 0x001762BC
		protected override void CreateChildControls()
		{
			this.spamProtectorPanel = new Panel();
			this.spamProtectorPanel.ID = "SpamProtectorPanel";
			this.spamProtectorPanel.CssClass = this.CssClass;
			this.spamProtectors.Add(this.autoBotFind);
			if (!this.spamProtectors.Contains(this.captcha))
			{
				this.spamProtectors.Add(this.captcha);
				this.captcha.Visible = false;
			}
			if (!this.autoBotFind.AutoBotFindStrats.Contains(this.autoBotFind.InvisibleTextBoxStrat))
			{
				this.autoBotFind.AutoBotFindStrats.Add(this.autoBotFind.InvisibleTextBoxStrat);
				this.autoBotFind.InvisibleTextBoxStrat.Visible = false;
			}
			foreach (ISpamProtector spamProtector in this.spamProtectors)
			{
				spamProtector.AddChildControls(this.spamProtectorPanel);
			}
			this.captchaBaseValidator.ValidationGroup = this.ValidationGroup;
			this.captchaBaseValidator.ParentCaptcha = this;
			this.Controls.Add(this.captchaBaseValidator);
			this.Controls.Add(this.spamProtectorPanel);
		}

		// Token: 0x06006445 RID: 25669 RVA: 0x0017820C File Offset: 0x0017640C
		protected override void ControlPreRender()
		{
			foreach (ISpamProtector spamProtector in this.spamProtectors)
			{
				spamProtector.PreRenderHandler();
			}
			if (this.ImageStorageLocation == CaptchaImageStorage.Session)
			{
				DateTime dateTime = DateTime.Now.AddMinutes((double)((this.CaptchaMaxTimeout == 0) ? 120 : this.CaptchaMaxTimeout));
				HttpContext.Current.Session.Add("RadCaptcha_TimeOut", dateTime);
			}
			if (this.Visible)
			{
				this.captcha.GenerateNewCaptcha();
			}
			base.ControlPreRender();
		}

		// Token: 0x06006446 RID: 25670 RVA: 0x001782BC File Offset: 0x001764BC
		protected override bool LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			ITextControl validatedTextControl = this.ValidatedTextControl;
			if (this.IsModeEnabled(RadCaptcha.ProtectionStrategies.Captcha) && this.captcha.CaptchaImage.RenderImageOnly && validatedTextControl != null)
			{
				this.captcha.UserEntry = validatedTextControl.Text;
				validatedTextControl.Text = string.Empty;
			}
			foreach (ISpamProtector spamProtector in this.spamProtectors)
			{
				spamProtector.LoadPostBackData(this.spamProtectorPanel);
			}
			return false;
		}

		// Token: 0x06006447 RID: 25671 RVA: 0x00178358 File Offset: 0x00176558
		protected override void RaisePostDataChangedEvent()
		{
		}

		// Token: 0x06006448 RID: 25672 RVA: 0x0017835C File Offset: 0x0017655C
		protected override object SaveControlState()
		{
			return new object[]
			{
				this.captcha.CaptchaImage.UniqueId,
				this.autoBotFind.MinSubmTimeStrat.RenderedAt
			};
		}

		// Token: 0x06006449 RID: 25673 RVA: 0x001783A0 File Offset: 0x001765A0
		protected override void LoadControlState(object state)
		{
			if (state != null)
			{
				object[] array = (object[])state;
				this.captcha.PrevGuid = (string)array[0];
				this.autoBotFind.MinSubmTimeStrat.RenderedAt = (DateTime)array[1];
			}
		}

		// Token: 0x0600644A RID: 25674 RVA: 0x001783E4 File Offset: 0x001765E4
		protected override void LoadViewState(object state)
		{
			object[] array = (object[])state;
			base.LoadViewState(array[0]);
			((IStateManager)this.captcha).LoadViewState(array[1]);
			((IStateManager)this.autoBotFind).LoadViewState(array[2]);
			this.captchaBaseValidator.ValidationGroup = this.ValidationGroup;
			this.ProcessSpamProtectors();
		}

		// Token: 0x0600644B RID: 25675 RVA: 0x00178434 File Offset: 0x00176634
		protected override object SaveViewState()
		{
			return new object[]
			{
				base.SaveViewState(),
				((IStateManager)this.captcha).SaveViewState(),
				((IStateManager)this.autoBotFind).SaveViewState()
			};
		}

		// Token: 0x0600644C RID: 25676 RVA: 0x00178470 File Offset: 0x00176670
		protected override void TrackViewState()
		{
			base.TrackViewState();
			((IStateManager)this.captcha).TrackViewState();
			((IStateManager)this.autoBotFind).TrackViewState();
		}

		// Token: 0x0600644D RID: 25677 RVA: 0x0017848E File Offset: 0x0017668E
		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			this.ProcessSpamProtectors();
			this.EnsureChildControls();
			this.Page.RegisterRequiresControlState(this);
		}

		// Token: 0x0600644E RID: 25678 RVA: 0x001784B0 File Offset: 0x001766B0
		protected override void DescribeComponent(IScriptDescriptor descriptor)
		{
			base.DescribeComponent(descriptor);
			string text = this.captcha.GetAudioHandlerUrl();
			if (text.Length > 0)
			{
				text = this.Page.ResolveUrl(text);
			}
			descriptor.AddProperty("_audioUrl", text);
			descriptor.AddProperty("_enableAudio", this.CaptchaImage.EnableCaptchaAudio);
			descriptor.AddProperty("_persistCode", this.CaptchaImage.PersistCodeDuringAjax);
		}

		// Token: 0x0600644F RID: 25679 RVA: 0x00178528 File Offset: 0x00176728
		public void Validate()
		{
			this.captchaBaseValidator.Validate();
		}

		// Token: 0x06006450 RID: 25680 RVA: 0x00178538 File Offset: 0x00176738
		internal virtual bool EvaluateIsValid()
		{
			CaptchaValidateEventArgs captchaValidateEventArgs = new CaptchaValidateEventArgs();
			this.OnCaptchaValidate(captchaValidateEventArgs);
			if (captchaValidateEventArgs.CancelDefaultValidation)
			{
				return captchaValidateEventArgs.IsValid;
			}
			if (this.Enabled)
			{
				foreach (ISpamProtector spamProtector in this.spamProtectors)
				{
					spamProtector.ValidatePostBackData();
					if (!spamProtector.IsValid)
					{
						this._validationCssClass = this.GetValidationCssClass(false);
						return false;
					}
				}
			}
			this._validationCssClass = this.GetValidationCssClass(true);
			return true;
		}

		// Token: 0x06006451 RID: 25681 RVA: 0x001785D8 File Offset: 0x001767D8
		private void ProcessSpamProtectors()
		{
			if (this.IsModeEnabled(RadCaptcha.ProtectionStrategies.Captcha))
			{
				if (!this.spamProtectors.Contains(this.captcha))
				{
					this.captcha.Visible = true;
					this.spamProtectors.Add(this.captcha);
				}
			}
			else if (this.spamProtectors.Contains(this.captcha))
			{
				this.captcha.Visible = false;
				this.spamProtectors.Remove(this.captcha);
			}
			if (this.IsModeEnabled(RadCaptcha.ProtectionStrategies.InvisibleTextBox))
			{
				if (!this.autoBotFind.AutoBotFindStrats.Contains(this.autoBotFind.InvisibleTextBoxStrat))
				{
					this.autoBotFind.Visible = true;
					this.autoBotFind.InvisibleTextBoxStrat.Visible = true;
					this.autoBotFind.AutoBotFindStrats.Add(this.autoBotFind.InvisibleTextBoxStrat);
				}
			}
			else if (this.autoBotFind.AutoBotFindStrats.Contains(this.autoBotFind.InvisibleTextBoxStrat))
			{
				this.autoBotFind.Visible = false;
				this.autoBotFind.InvisibleTextBoxStrat.Visible = false;
				this.autoBotFind.AutoBotFindStrats.Remove(this.autoBotFind.InvisibleTextBoxStrat);
			}
			if (this.IsModeEnabled(RadCaptcha.ProtectionStrategies.MinimumTimeout))
			{
				if (!this.autoBotFind.AutoBotFindStrats.Contains(this.autoBotFind.MinSubmTimeStrat))
				{
					this.autoBotFind.Visible = true;
					this.autoBotFind.MinSubmTimeStrat.Visible = true;
					this.autoBotFind.AutoBotFindStrats.Add(this.autoBotFind.MinSubmTimeStrat);
					return;
				}
			}
			else if (this.autoBotFind.AutoBotFindStrats.Contains(this.autoBotFind.MinSubmTimeStrat))
			{
				this.autoBotFind.Visible = false;
				this.autoBotFind.MinSubmTimeStrat.Visible = false;
				this.autoBotFind.AutoBotFindStrats.Remove(this.autoBotFind.MinSubmTimeStrat);
			}
		}

		// Token: 0x06006452 RID: 25682 RVA: 0x001787C2 File Offset: 0x001769C2
		private bool IsModeEnabled(RadCaptcha.ProtectionStrategies protectionMode)
		{
			return this.ProtectionMode == protectionMode;
		}

		// Token: 0x06006453 RID: 25683 RVA: 0x001787CD File Offset: 0x001769CD
		protected virtual string GetValidationCssClass(bool isValid = true)
		{
			if (!isValid)
			{
				return "rcInvalid";
			}
			return "rcValid";
		}

		// Token: 0x140000E8 RID: 232
		// (add) Token: 0x06006454 RID: 25684 RVA: 0x001787DD File Offset: 0x001769DD
		// (remove) Token: 0x06006455 RID: 25685 RVA: 0x001787F0 File Offset: 0x001769F0
		[Category("Action")]
		[Description("Fired berfore the RadCaptcha is validated.")]
		public event RadCaptchaEventHandler CaptchaValidate
		{
			add
			{
				base.Events.AddHandler(RadCaptcha.captchaValidateEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadCaptcha.captchaValidateEvent, value);
			}
		}

		// Token: 0x06006456 RID: 25686 RVA: 0x00178804 File Offset: 0x00176A04
		protected virtual void OnCaptchaValidate(CaptchaValidateEventArgs e)
		{
			RadCaptchaEventHandler radCaptchaEventHandler = (RadCaptchaEventHandler)base.Events[RadCaptcha.captchaValidateEvent];
			if (radCaptchaEventHandler != null)
			{
				radCaptchaEventHandler(this, e);
			}
		}

		// Token: 0x0400184E RID: 6222
		internal const string _handlerUrl = "~/Telerik.Web.UI.WebResource.axd";

		// Token: 0x0400184F RID: 6223
		protected Panel spamProtectorPanel;

		// Token: 0x04001850 RID: 6224
		private string _validationCssClass = string.Empty;

		// Token: 0x04001851 RID: 6225
		private readonly List<ISpamProtector> spamProtectors = new List<ISpamProtector>();

		// Token: 0x04001852 RID: 6226
		private readonly CaptchaProtector captcha = new CaptchaProtector();

		// Token: 0x04001853 RID: 6227
		private readonly AutoBotDiscoveryProtector autoBotFind = new AutoBotDiscoveryProtector();

		// Token: 0x04001854 RID: 6228
		private readonly CaptchaBaseValidator captchaBaseValidator = new CaptchaBaseValidator();

		// Token: 0x04001855 RID: 6229
		private static readonly object captchaValidateEvent = new object();

		// Token: 0x02000A40 RID: 2624
		public enum ProtectionStrategies
		{
			// Token: 0x04001857 RID: 6231
			Captcha,
			// Token: 0x04001858 RID: 6232
			InvisibleTextBox,
			// Token: 0x04001859 RID: 6233
			MinimumTimeout
		}
	}
}
