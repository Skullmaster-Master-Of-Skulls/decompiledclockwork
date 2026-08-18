using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using Telerik.Web.UI.Dialogs;

namespace Telerik.Web.UI.Editor.DialogControls
{
	// Token: 0x02000B3A RID: 2874
	[ToolboxItem(false)]
	[ClientScriptResource("Telerik.Web.UI.Widgets.InsertExternalVideo", "Telerik.Web.UI.Common.Core.js")]
	[RequiredScript(typeof(jQuery))]
	public class InsertExternalVideo : UserControlBase, IClientParameterConsumer
	{
		// Token: 0x1700239D RID: 9117
		// (get) Token: 0x06006C87 RID: 27783 RVA: 0x001933EA File Offset: 0x001915EA
		public override string DialogName
		{
			get
			{
				return "InsertExternalVideo";
			}
		}

		// Token: 0x06006C88 RID: 27784 RVA: 0x001933F4 File Offset: 0x001915F4
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			this.FindTextControl("PasteVideoURLText").Text = this.Localization.GetString("InsertExternalVideo_PasteVideoURL");
			this.FindTextControl("EmbedVideoSettingsText").Text = this.Localization.GetString("InsertExternalVideo_EmbedVideoSettings");
			this.FindTextControl("AspectRatioText").Text = this.Localization.GetString("InsertExternalVideo_AspectRatio");
			this.FindTextControl("VideoWidthText").Text = this.Localization.GetString("Common_Width");
			this.FindTextControl("VideoHeightText").Text = this.Localization.GetString("Common_Height");
			this.FindTextControl("VideoAutoplayText").Text = this.Localization.GetString("InsertExternalVideo_AutoplaySetting");
			this.FindTextControl("ShowTitleText").Text = this.Localization.GetString("InsertExternalVideo_ShowTitleSetting");
			this.FindTextControl("EnableFullscreenText").Text = this.Localization.GetString("InsertExternalVideo_FullscreenSetting");
			this.FindTextControl("EnablePrivacyEnhancedText").Text = this.Localization.GetString("InsertExternalVideo_PrivacySetting");
			this.FindTextControl("AdvancedModeText").Text = this.Localization.GetString("InsertExternalVideo_AdvancedMode");
			this.FindTextControl("EmbedCodeText").Text = this.Localization.GetString("InsertExternalVideo_EmbedCode");
			HtmlAnchor htmlAnchor = (HtmlAnchor)base.FindControlRecursive("toggleEmbedCode");
			htmlAnchor.Title = this.Localization.GetString("InsertExternalVideo_ToggleAdvancedModeEmbedCode");
		}

		// Token: 0x06006C89 RID: 27785 RVA: 0x0019358F File Offset: 0x0019178F
		private ITextControl FindTextControl(string id)
		{
			return (ITextControl)base.FindControlRecursive(id);
		}
	}
}
