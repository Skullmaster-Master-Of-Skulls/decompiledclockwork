using System;
using System.ComponentModel;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit.Design;
using AjaxControlToolkit.ToolboxIcons;

namespace AjaxControlToolkit
{
	// Token: 0x02000079 RID: 121
	[TargetControlType(typeof(IButtonControl))]
	[ClientScriptResource("Sys.Extended.UI.ConfirmButtonBehavior", "ConfirmButton")]
	[ToolboxBitmap(typeof(Accessor), "ConfirmButton.bmp")]
	[Designer(typeof(ConfirmButtonExtenderDesigner))]
	public class ConfirmButtonExtender : ExtenderControlBase
	{
		// Token: 0x0600042D RID: 1069 RVA: 0x0000C338 File Offset: 0x0000A538
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			ScriptManager.RegisterOnSubmitStatement(this, typeof(ConfirmButtonExtender), "ConfirmButtonExtenderOnSubmit", "null;");
			this.RegisterDisplayModalPopup();
		}

		// Token: 0x0600042E RID: 1070 RVA: 0x0000C364 File Offset: 0x0000A564
		public void RegisterDisplayModalPopup()
		{
			if (!string.IsNullOrEmpty(this.DisplayModalPopupID))
			{
				ModalPopupExtender modalPopupExtender = base.FindControlHelper(this.DisplayModalPopupID) as ModalPopupExtender;
				if (modalPopupExtender == null)
				{
					throw new ArgumentException("Unable to find specified ModalPopupExtender.");
				}
				if (modalPopupExtender.TargetControlID != base.TargetControlID)
				{
					throw new ArgumentException("ConfirmButton and the ModalPopupExtender specified by its DisplayModalPopupID must specify the same TargetControlID.");
				}
				if (string.IsNullOrEmpty(modalPopupExtender.OkControlID) && string.IsNullOrEmpty(modalPopupExtender.CancelControlID))
				{
					throw new ArgumentException("Specified ModalPopupExtender must set at least OkControlID and/or CancelControlID.");
				}
				if (!string.IsNullOrEmpty(modalPopupExtender.OnOkScript) || !string.IsNullOrEmpty(modalPopupExtender.OnCancelScript))
				{
					throw new ArgumentException("Specified ModalPopupExtender may not set OnOkScript or OnCancelScript.");
				}
				Button button = new Button();
				button.ID = this.ID + "_CBE_MPE_Placeholder";
				button.Style[HtmlTextWriterStyle.Display] = "none";
				this.Controls.Add(button);
				modalPopupExtender.TargetControlID = button.ID;
				this.PostBackScript = this.Page.ClientScript.GetPostBackEventReference(base.TargetControl, string.Empty);
			}
		}

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x0600042F RID: 1071 RVA: 0x0000C470 File Offset: 0x0000A670
		// (set) Token: 0x06000430 RID: 1072 RVA: 0x0000C482 File Offset: 0x0000A682
		[RequiredProperty]
		[ClientPropertyName("confirmText")]
		[ExtenderControlProperty]
		public string ConfirmText
		{
			get
			{
				return base.GetPropertyValue<string>("ConfirmText", string.Empty);
			}
			set
			{
				base.SetPropertyValue<string>("ConfirmText", value);
			}
		}

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x06000431 RID: 1073 RVA: 0x0000C490 File Offset: 0x0000A690
		// (set) Token: 0x06000432 RID: 1074 RVA: 0x0000C4A2 File Offset: 0x0000A6A2
		[ClientPropertyName("onClientCancel")]
		[DefaultValue("")]
		[ExtenderControlProperty]
		public string OnClientCancel
		{
			get
			{
				return base.GetPropertyValue<string>("OnClientCancel", string.Empty);
			}
			set
			{
				base.SetPropertyValue<string>("OnClientCancel", value);
			}
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x06000433 RID: 1075 RVA: 0x0000C4B0 File Offset: 0x0000A6B0
		// (set) Token: 0x06000434 RID: 1076 RVA: 0x0000C4BE File Offset: 0x0000A6BE
		[ClientPropertyName("confirmOnFormSubmit")]
		[DefaultValue(false)]
		[ExtenderControlProperty]
		public bool ConfirmOnFormSubmit
		{
			get
			{
				return base.GetPropertyValue<bool>("ConfirmOnFormSubmit", false);
			}
			set
			{
				base.SetPropertyValue<bool>("ConfirmOnFormSubmit", value);
			}
		}

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x06000435 RID: 1077 RVA: 0x0000C4CC File Offset: 0x0000A6CC
		// (set) Token: 0x06000436 RID: 1078 RVA: 0x0000C4DE File Offset: 0x0000A6DE
		[ExtenderControlProperty]
		[IDReferenceProperty(typeof(ModalPopupExtender))]
		[ClientPropertyName("displayModalPopupID")]
		[DefaultValue("")]
		public string DisplayModalPopupID
		{
			get
			{
				return base.GetPropertyValue<string>("DisplayModalPopupID", string.Empty);
			}
			set
			{
				base.SetPropertyValue<string>("DisplayModalPopupID", value);
			}
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x06000437 RID: 1079 RVA: 0x0000C4EC File Offset: 0x0000A6EC
		// (set) Token: 0x06000438 RID: 1080 RVA: 0x0000C4FE File Offset: 0x0000A6FE
		[DefaultValue("")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[ExtenderControlProperty]
		[Browsable(false)]
		[ClientPropertyName("postBackScript")]
		public string PostBackScript
		{
			get
			{
				return base.GetPropertyValue<string>("PostBackScript", string.Empty);
			}
			set
			{
				base.SetPropertyValue<string>("PostBackScript", value);
			}
		}
	}
}
