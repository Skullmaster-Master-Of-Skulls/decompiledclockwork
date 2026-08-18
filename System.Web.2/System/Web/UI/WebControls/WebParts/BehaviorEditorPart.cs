using System;
using System.ComponentModel;

namespace System.Web.UI.WebControls.WebParts
{
	// Token: 0x02000529 RID: 1321
	public sealed class BehaviorEditorPart : EditorPart
	{
		// Token: 0x170013A1 RID: 5025
		// (get) Token: 0x060042E3 RID: 17123 RVA: 0x000D9E7A File Offset: 0x000D807A
		// (set) Token: 0x060042E4 RID: 17124 RVA: 0x000D9E82 File Offset: 0x000D8082
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Themeable(false)]
		public override string DefaultButton
		{
			get
			{
				return base.DefaultButton;
			}
			set
			{
				base.DefaultButton = value;
			}
		}

		// Token: 0x170013A2 RID: 5026
		// (get) Token: 0x060042E5 RID: 17125 RVA: 0x000DA737 File Offset: 0x000D8937
		public override bool Display
		{
			get
			{
				return (base.WebPartToEdit == null || !base.WebPartToEdit.IsShared || base.WebPartManager == null || base.WebPartManager.Personalization.Scope != PersonalizationScope.User) && base.Display;
			}
		}

		// Token: 0x170013A3 RID: 5027
		// (get) Token: 0x060042E6 RID: 17126 RVA: 0x000DA770 File Offset: 0x000D8970
		private bool HasError
		{
			get
			{
				return this._allowCloseErrorMessage != null || this._allowConnectErrorMessage != null || this._allowHideErrorMessage != null || this._allowMinimizeErrorMessage != null || this._allowZoneChangeErrorMessage != null || this._exportModeErrorMessage != null || this._helpModeErrorMessage != null || this._descriptionErrorMessage != null || this._titleUrlErrorMessage != null || this._titleIconImageUrlErrorMessage != null || this._catalogIconImageUrlErrorMessage != null || this._helpUrlErrorMessage != null || this._importErrorMessageErrorMessage != null || this._authorizationFilterErrorMessage != null || this._allowEditErrorMessage != null;
			}
		}

		// Token: 0x170013A4 RID: 5028
		// (get) Token: 0x060042E7 RID: 17127 RVA: 0x000DA7F8 File Offset: 0x000D89F8
		// (set) Token: 0x060042E8 RID: 17128 RVA: 0x000D9EF2 File Offset: 0x000D80F2
		[WebSysDefaultValue("BehaviorEditorPart_PartTitle")]
		public override string Title
		{
			get
			{
				string text = (string)this.ViewState["Title"];
				if (text == null)
				{
					return SR.GetString("BehaviorEditorPart_PartTitle");
				}
				return text;
			}
			set
			{
				this.ViewState["Title"] = value;
			}
		}

		// Token: 0x060042E9 RID: 17129 RVA: 0x000DA82C File Offset: 0x000D8A2C
		public override bool ApplyChanges()
		{
			WebPart webPartToEdit = base.WebPartToEdit;
			if (webPartToEdit != null)
			{
				this.EnsureChildControls();
				bool allowLayoutChange = webPartToEdit.Zone.AllowLayoutChange;
				if (allowLayoutChange)
				{
					try
					{
						webPartToEdit.AllowClose = this._allowClose.Checked;
					}
					catch (Exception ex)
					{
						this._allowCloseErrorMessage = base.CreateErrorMessage(ex.Message);
					}
				}
				try
				{
					webPartToEdit.AllowConnect = this._allowConnect.Checked;
				}
				catch (Exception ex2)
				{
					this._allowConnectErrorMessage = base.CreateErrorMessage(ex2.Message);
				}
				if (allowLayoutChange)
				{
					try
					{
						webPartToEdit.AllowHide = this._allowHide.Checked;
					}
					catch (Exception ex3)
					{
						this._allowHideErrorMessage = base.CreateErrorMessage(ex3.Message);
					}
				}
				if (allowLayoutChange)
				{
					try
					{
						webPartToEdit.AllowMinimize = this._allowMinimize.Checked;
					}
					catch (Exception ex4)
					{
						this._allowMinimizeErrorMessage = base.CreateErrorMessage(ex4.Message);
					}
				}
				if (allowLayoutChange)
				{
					try
					{
						webPartToEdit.AllowZoneChange = this._allowZoneChange.Checked;
					}
					catch (Exception ex5)
					{
						this._allowZoneChangeErrorMessage = base.CreateErrorMessage(ex5.Message);
					}
				}
				try
				{
					TypeConverter converter = TypeDescriptor.GetConverter(typeof(WebPartExportMode));
					webPartToEdit.ExportMode = (WebPartExportMode)converter.ConvertFromString(this._exportMode.SelectedValue);
				}
				catch (Exception ex6)
				{
					this._exportModeErrorMessage = base.CreateErrorMessage(ex6.Message);
				}
				try
				{
					TypeConverter converter2 = TypeDescriptor.GetConverter(typeof(WebPartHelpMode));
					webPartToEdit.HelpMode = (WebPartHelpMode)converter2.ConvertFromString(this._helpMode.SelectedValue);
				}
				catch (Exception ex7)
				{
					this._helpModeErrorMessage = base.CreateErrorMessage(ex7.Message);
				}
				try
				{
					webPartToEdit.Description = this._description.Text;
				}
				catch (Exception ex8)
				{
					this._descriptionErrorMessage = base.CreateErrorMessage(ex8.Message);
				}
				string text = this._titleUrl.Text;
				if (CrossSiteScriptingValidation.IsDangerousUrl(text))
				{
					this._titleUrlErrorMessage = SR.GetString("EditorPart_ErrorBadUrl");
				}
				else
				{
					try
					{
						webPartToEdit.TitleUrl = text;
					}
					catch (Exception ex9)
					{
						this._titleUrlErrorMessage = base.CreateErrorMessage(ex9.Message);
					}
				}
				text = this._titleIconImageUrl.Text;
				if (CrossSiteScriptingValidation.IsDangerousUrl(text))
				{
					this._titleIconImageUrlErrorMessage = SR.GetString("EditorPart_ErrorBadUrl");
				}
				else
				{
					try
					{
						webPartToEdit.TitleIconImageUrl = text;
					}
					catch (Exception ex10)
					{
						this._titleIconImageUrlErrorMessage = base.CreateErrorMessage(ex10.Message);
					}
				}
				text = this._catalogIconImageUrl.Text;
				if (CrossSiteScriptingValidation.IsDangerousUrl(text))
				{
					this._catalogIconImageUrlErrorMessage = SR.GetString("EditorPart_ErrorBadUrl");
				}
				else
				{
					try
					{
						webPartToEdit.CatalogIconImageUrl = text;
					}
					catch (Exception ex11)
					{
						this._catalogIconImageUrlErrorMessage = base.CreateErrorMessage(ex11.Message);
					}
				}
				text = this._helpUrl.Text;
				if (CrossSiteScriptingValidation.IsDangerousUrl(text))
				{
					this._helpUrlErrorMessage = SR.GetString("EditorPart_ErrorBadUrl");
				}
				else
				{
					try
					{
						webPartToEdit.HelpUrl = text;
					}
					catch (Exception ex12)
					{
						this._helpUrlErrorMessage = base.CreateErrorMessage(ex12.Message);
					}
				}
				try
				{
					webPartToEdit.ImportErrorMessage = this._importErrorMessage.Text;
				}
				catch (Exception ex13)
				{
					this._importErrorMessageErrorMessage = base.CreateErrorMessage(ex13.Message);
				}
				try
				{
					webPartToEdit.AuthorizationFilter = this._authorizationFilter.Text;
				}
				catch (Exception ex14)
				{
					this._authorizationFilterErrorMessage = base.CreateErrorMessage(ex14.Message);
				}
				try
				{
					webPartToEdit.AllowEdit = this._allowEdit.Checked;
				}
				catch (Exception ex15)
				{
					this._allowEditErrorMessage = base.CreateErrorMessage(ex15.Message);
				}
			}
			return !this.HasError;
		}

		// Token: 0x060042EA RID: 17130 RVA: 0x000DAC50 File Offset: 0x000D8E50
		protected internal override void CreateChildControls()
		{
			ControlCollection controls = this.Controls;
			controls.Clear();
			this._allowClose = new CheckBox();
			controls.Add(this._allowClose);
			this._allowConnect = new CheckBox();
			controls.Add(this._allowConnect);
			this._allowHide = new CheckBox();
			controls.Add(this._allowHide);
			this._allowMinimize = new CheckBox();
			controls.Add(this._allowMinimize);
			this._allowZoneChange = new CheckBox();
			controls.Add(this._allowZoneChange);
			TypeConverter converter = TypeDescriptor.GetConverter(typeof(WebPartExportMode));
			this._exportMode = new DropDownList();
			this._exportMode.Items.AddRange(new ListItem[]
			{
				new ListItem(SR.GetString("BehaviorEditorPart_ExportModeNone"), converter.ConvertToString(WebPartExportMode.None)),
				new ListItem(SR.GetString("BehaviorEditorPart_ExportModeAll"), converter.ConvertToString(WebPartExportMode.All)),
				new ListItem(SR.GetString("BehaviorEditorPart_ExportModeNonSensitiveData"), converter.ConvertToString(WebPartExportMode.NonSensitiveData))
			});
			controls.Add(this._exportMode);
			TypeConverter converter2 = TypeDescriptor.GetConverter(typeof(WebPartHelpMode));
			this._helpMode = new DropDownList();
			this._helpMode.Items.AddRange(new ListItem[]
			{
				new ListItem(SR.GetString("BehaviorEditorPart_HelpModeModal"), converter2.ConvertToString(WebPartHelpMode.Modal)),
				new ListItem(SR.GetString("BehaviorEditorPart_HelpModeModeless"), converter2.ConvertToString(WebPartHelpMode.Modeless)),
				new ListItem(SR.GetString("BehaviorEditorPart_HelpModeNavigate"), converter2.ConvertToString(WebPartHelpMode.Navigate))
			});
			controls.Add(this._helpMode);
			this._description = new TextBox();
			this._description.Columns = 30;
			controls.Add(this._description);
			this._titleUrl = new TextBox();
			this._titleUrl.Columns = 30;
			controls.Add(this._titleUrl);
			this._titleIconImageUrl = new TextBox();
			this._titleIconImageUrl.Columns = 30;
			controls.Add(this._titleIconImageUrl);
			this._catalogIconImageUrl = new TextBox();
			this._catalogIconImageUrl.Columns = 30;
			controls.Add(this._catalogIconImageUrl);
			this._helpUrl = new TextBox();
			this._helpUrl.Columns = 30;
			controls.Add(this._helpUrl);
			this._importErrorMessage = new TextBox();
			this._importErrorMessage.Columns = 30;
			controls.Add(this._importErrorMessage);
			this._authorizationFilter = new TextBox();
			this._authorizationFilter.Columns = 30;
			controls.Add(this._authorizationFilter);
			this._allowEdit = new CheckBox();
			controls.Add(this._allowEdit);
			foreach (object obj in controls)
			{
				Control control = (Control)obj;
				control.EnableViewState = false;
			}
		}

		// Token: 0x060042EB RID: 17131 RVA: 0x000DAF6C File Offset: 0x000D916C
		protected internal override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			if (this.Display && this.Visible && !this.HasError)
			{
				this.SyncChanges();
			}
		}

		// Token: 0x060042EC RID: 17132 RVA: 0x000DAF94 File Offset: 0x000D9194
		protected internal override void RenderContents(HtmlTextWriter writer)
		{
			if (this.Page != null)
			{
				this.Page.VerifyRenderingInServerForm(this);
			}
			this.EnsureChildControls();
			string[] propertyDisplayNames = new string[]
			{
				SR.GetString("BehaviorEditorPart_Description"),
				SR.GetString("BehaviorEditorPart_TitleLink"),
				SR.GetString("BehaviorEditorPart_TitleIconImageLink"),
				SR.GetString("BehaviorEditorPart_CatalogIconImageLink"),
				SR.GetString("BehaviorEditorPart_HelpLink"),
				SR.GetString("BehaviorEditorPart_HelpMode"),
				SR.GetString("BehaviorEditorPart_ImportErrorMessage"),
				SR.GetString("BehaviorEditorPart_ExportMode"),
				SR.GetString("BehaviorEditorPart_AuthorizationFilter"),
				SR.GetString("BehaviorEditorPart_AllowClose"),
				SR.GetString("BehaviorEditorPart_AllowConnect"),
				SR.GetString("BehaviorEditorPart_AllowEdit"),
				SR.GetString("BehaviorEditorPart_AllowHide"),
				SR.GetString("BehaviorEditorPart_AllowMinimize"),
				SR.GetString("BehaviorEditorPart_AllowZoneChange")
			};
			WebControl[] propertyEditors = new WebControl[]
			{
				this._description,
				this._titleUrl,
				this._titleIconImageUrl,
				this._catalogIconImageUrl,
				this._helpUrl,
				this._helpMode,
				this._importErrorMessage,
				this._exportMode,
				this._authorizationFilter,
				this._allowClose,
				this._allowConnect,
				this._allowEdit,
				this._allowHide,
				this._allowMinimize,
				this._allowZoneChange
			};
			string[] errorMessages = new string[]
			{
				this._descriptionErrorMessage,
				this._titleUrlErrorMessage,
				this._titleIconImageUrlErrorMessage,
				this._catalogIconImageUrlErrorMessage,
				this._helpUrlErrorMessage,
				this._helpModeErrorMessage,
				this._importErrorMessageErrorMessage,
				this._exportModeErrorMessage,
				this._authorizationFilterErrorMessage,
				this._allowCloseErrorMessage,
				this._allowConnectErrorMessage,
				this._allowEditErrorMessage,
				this._allowHideErrorMessage,
				this._allowMinimizeErrorMessage,
				this._allowZoneChangeErrorMessage
			};
			base.RenderPropertyEditors(writer, propertyDisplayNames, null, propertyEditors, errorMessages);
		}

		// Token: 0x060042ED RID: 17133 RVA: 0x000DB1C4 File Offset: 0x000D93C4
		public override void SyncChanges()
		{
			WebPart webPartToEdit = base.WebPartToEdit;
			if (webPartToEdit != null)
			{
				bool allowLayoutChange = webPartToEdit.Zone.AllowLayoutChange;
				this.EnsureChildControls();
				this._allowClose.Checked = webPartToEdit.AllowClose;
				this._allowClose.Enabled = allowLayoutChange;
				this._allowConnect.Checked = webPartToEdit.AllowConnect;
				this._allowHide.Checked = webPartToEdit.AllowHide;
				this._allowHide.Enabled = allowLayoutChange;
				this._allowMinimize.Checked = webPartToEdit.AllowMinimize;
				this._allowMinimize.Enabled = allowLayoutChange;
				this._allowZoneChange.Checked = webPartToEdit.AllowZoneChange;
				this._allowZoneChange.Enabled = allowLayoutChange;
				TypeConverter converter = TypeDescriptor.GetConverter(typeof(WebPartExportMode));
				this._exportMode.SelectedValue = converter.ConvertToString(webPartToEdit.ExportMode);
				TypeConverter converter2 = TypeDescriptor.GetConverter(typeof(WebPartHelpMode));
				this._helpMode.SelectedValue = converter2.ConvertToString(webPartToEdit.HelpMode);
				this._description.Text = webPartToEdit.Description;
				this._titleUrl.Text = webPartToEdit.TitleUrl;
				this._titleIconImageUrl.Text = webPartToEdit.TitleIconImageUrl;
				this._catalogIconImageUrl.Text = webPartToEdit.CatalogIconImageUrl;
				this._helpUrl.Text = webPartToEdit.HelpUrl;
				this._importErrorMessage.Text = webPartToEdit.ImportErrorMessage;
				this._authorizationFilter.Text = webPartToEdit.AuthorizationFilter;
				this._allowEdit.Checked = webPartToEdit.AllowEdit;
			}
		}

		// Token: 0x04002598 RID: 9624
		private CheckBox _allowClose;

		// Token: 0x04002599 RID: 9625
		private CheckBox _allowConnect;

		// Token: 0x0400259A RID: 9626
		private CheckBox _allowHide;

		// Token: 0x0400259B RID: 9627
		private CheckBox _allowMinimize;

		// Token: 0x0400259C RID: 9628
		private CheckBox _allowZoneChange;

		// Token: 0x0400259D RID: 9629
		private DropDownList _exportMode;

		// Token: 0x0400259E RID: 9630
		private DropDownList _helpMode;

		// Token: 0x0400259F RID: 9631
		private TextBox _description;

		// Token: 0x040025A0 RID: 9632
		private TextBox _titleUrl;

		// Token: 0x040025A1 RID: 9633
		private TextBox _titleIconImageUrl;

		// Token: 0x040025A2 RID: 9634
		private TextBox _catalogIconImageUrl;

		// Token: 0x040025A3 RID: 9635
		private TextBox _helpUrl;

		// Token: 0x040025A4 RID: 9636
		private TextBox _importErrorMessage;

		// Token: 0x040025A5 RID: 9637
		private TextBox _authorizationFilter;

		// Token: 0x040025A6 RID: 9638
		private CheckBox _allowEdit;

		// Token: 0x040025A7 RID: 9639
		private string _allowCloseErrorMessage;

		// Token: 0x040025A8 RID: 9640
		private string _allowConnectErrorMessage;

		// Token: 0x040025A9 RID: 9641
		private string _allowHideErrorMessage;

		// Token: 0x040025AA RID: 9642
		private string _allowMinimizeErrorMessage;

		// Token: 0x040025AB RID: 9643
		private string _allowZoneChangeErrorMessage;

		// Token: 0x040025AC RID: 9644
		private string _exportModeErrorMessage;

		// Token: 0x040025AD RID: 9645
		private string _helpModeErrorMessage;

		// Token: 0x040025AE RID: 9646
		private string _descriptionErrorMessage;

		// Token: 0x040025AF RID: 9647
		private string _titleUrlErrorMessage;

		// Token: 0x040025B0 RID: 9648
		private string _titleIconImageUrlErrorMessage;

		// Token: 0x040025B1 RID: 9649
		private string _catalogIconImageUrlErrorMessage;

		// Token: 0x040025B2 RID: 9650
		private string _helpUrlErrorMessage;

		// Token: 0x040025B3 RID: 9651
		private string _importErrorMessageErrorMessage;

		// Token: 0x040025B4 RID: 9652
		private string _authorizationFilterErrorMessage;

		// Token: 0x040025B5 RID: 9653
		private string _allowEditErrorMessage;

		// Token: 0x040025B6 RID: 9654
		private const int TextBoxColumns = 30;
	}
}
