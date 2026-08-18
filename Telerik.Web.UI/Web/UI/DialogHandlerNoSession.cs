using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using Telerik.Web.UI.Common;
using Telerik.Web.UI.Dialogs;
using Telerik.Web.UI.Editor.DialogControls;

namespace Telerik.Web.UI
{
	// Token: 0x02001033 RID: 4147
	public class DialogHandlerNoSession : RadAjaxPage
	{
		// Token: 0x17003395 RID: 13205
		// (get) Token: 0x0600A35C RID: 41820 RVA: 0x00245728 File Offset: 0x00243928
		protected bool UseRSM
		{
			get
			{
				return !string.IsNullOrEmpty(base.Request.QueryString["UseRSM"]);
			}
		}

		// Token: 0x17003396 RID: 13206
		// (get) Token: 0x0600A35D RID: 41821 RVA: 0x00245747 File Offset: 0x00243947
		protected string DialogName
		{
			get
			{
				return base.Request.QueryString["DialogName"];
			}
		}

		// Token: 0x17003397 RID: 13207
		// (get) Token: 0x0600A35E RID: 41822 RVA: 0x0024575E File Offset: 0x0024395E
		protected string DialogOpenerIdentifier
		{
			get
			{
				return base.Request.QueryString["doid"];
			}
		}

		// Token: 0x17003398 RID: 13208
		// (get) Token: 0x0600A35F RID: 41823 RVA: 0x00245775 File Offset: 0x00243975
		protected string PageTitle
		{
			get
			{
				return base.Request.QueryString["Title"];
			}
		}

		// Token: 0x17003399 RID: 13209
		// (get) Token: 0x0600A360 RID: 41824 RVA: 0x0024578C File Offset: 0x0024398C
		protected RenderMode RenderMode
		{
			get
			{
				string value = base.Request.QueryString["renderMode"];
				RenderMode result;
				try
				{
					result = (RenderMode)Enum.Parse(typeof(RenderMode), value);
				}
				catch
				{
					result = RenderMode.Classic;
				}
				return result;
			}
		}

		// Token: 0x1700339A RID: 13210
		// (get) Token: 0x0600A361 RID: 41825 RVA: 0x002457E0 File Offset: 0x002439E0
		protected DialogParametersProvider DialogParametersProvider
		{
			get
			{
				if (this._dialogParametersProvider == null)
				{
					this._dialogParametersProvider = (DialogParametersProvider)Activator.CreateInstance(this.DialogParametersProviderType, new object[]
					{
						this
					});
				}
				return this._dialogParametersProvider;
			}
		}

		// Token: 0x1700339B RID: 13211
		// (get) Token: 0x0600A362 RID: 41826 RVA: 0x00245820 File Offset: 0x00243A20
		protected Type DialogParametersProviderType
		{
			get
			{
				string dialogParametersProviderTypeName = this.GetDialogParametersProviderTypeName();
				if (string.IsNullOrEmpty(dialogParametersProviderTypeName))
				{
					return typeof(JavascriptDialogParametersProvider);
				}
				return Type.GetType(dialogParametersProviderTypeName);
			}
		}

		// Token: 0x0600A363 RID: 41827 RVA: 0x00245850 File Offset: 0x00243A50
		protected string GetDialogParametersProviderTypeName()
		{
			string text = base.Request.QueryString["dpptn"];
			if (string.IsNullOrEmpty(text))
			{
				return string.Empty;
			}
			HmacEnabledCryptoService service = DialogHashService.GetService();
			return service.Decrypt(text);
		}

		// Token: 0x1700339C RID: 13212
		// (get) Token: 0x0600A364 RID: 41828 RVA: 0x00245890 File Offset: 0x00243A90
		protected string Skin
		{
			get
			{
				string text = base.Request.QueryString["Skin"];
				if (text == null)
				{
					return string.Empty;
				}
				return text;
			}
		}

		// Token: 0x0600A365 RID: 41829 RVA: 0x002458BD File Offset: 0x00243ABD
		protected override void OnInit(EventArgs e)
		{
			base.AppRelativeVirtualPath = base.Request.AppRelativeCurrentExecutionFilePath;
			base.OnInit(e);
		}

		// Token: 0x0600A366 RID: 41830 RVA: 0x002458D7 File Offset: 0x00243AD7
		protected override void OnPreLoad(EventArgs e)
		{
			this.EnsureChildControls();
			base.OnPreLoad(e);
		}

		// Token: 0x0600A367 RID: 41831 RVA: 0x002458E8 File Offset: 0x00243AE8
		protected override void OnPreRenderComplete(EventArgs e)
		{
			base.OnPreRenderComplete(e);
			if (!string.IsNullOrEmpty(this._cssFile))
			{
				SkinRegistrar.RegisterCssReference(this, this, this._cssFile);
			}
			if (!string.IsNullOrEmpty(this._scriptFile))
			{
				ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "DialogsScriptFile", string.Format("\n<script src='{0}' type='text/javascript'></script>\n", base.ResolveClientUrl(this._scriptFile)), false);
			}
			string text = this.Page.Request.QueryString["isRtl"];
			if (!string.IsNullOrEmpty(text) && text == "true")
			{
				this.Page.Form.Attributes["class"] = "redRtl";
			}
		}

		// Token: 0x0600A368 RID: 41832 RVA: 0x002459A4 File Offset: 0x00243BA4
		public static DialogParameters GetDialogParameters(Control control)
		{
			return ((DialogHandler)control.Page).GetDialogParameters();
		}

		// Token: 0x1700339D RID: 13213
		// (get) Token: 0x0600A369 RID: 41833 RVA: 0x002459B6 File Offset: 0x00243BB6
		// (set) Token: 0x0600A36A RID: 41834 RVA: 0x002459CD File Offset: 0x00243BCD
		private DialogParameters storedDialogParameters
		{
			get
			{
				return (DialogParameters)this.ViewState["_dialogParameters"];
			}
			set
			{
				this.ViewState["_dialogParameters"] = value;
			}
		}

		// Token: 0x0600A36B RID: 41835 RVA: 0x002459E0 File Offset: 0x00243BE0
		private DialogParameters GetDialogParameters()
		{
			if (this.storedDialogParameters != null)
			{
				return this.storedDialogParameters;
			}
			if (this.DialogParametersProviderType == typeof(JavascriptDialogParametersProvider))
			{
				this.EnsureChildControls();
				if (this._parameterObtainer != null)
				{
					if (this._parameterObtainer.ParametersAvailable)
					{
						this.storedDialogParameters = this._parameterObtainer.GetDialogParameters();
						if (this.storedDialogParameters == null)
						{
							this.storedDialogParameters = new DialogParameters();
						}
					}
					else
					{
						base.Form.Controls.Add(new LiteralControl("<div style='text-align:center;'>Loading the dialog...</div>"));
					}
				}
			}
			else
			{
				this.storedDialogParameters = this.GetDialogParametersByName();
				if (this.storedDialogParameters == null)
				{
					this.storedDialogParameters = new DialogParameters();
				}
			}
			return this.storedDialogParameters;
		}

		// Token: 0x0600A36C RID: 41836 RVA: 0x00245A98 File Offset: 0x00243C98
		private DialogParameters GetDialogParametersByName()
		{
			IEnumerable<string> possibleDialogNames = this.GetPossibleDialogNames();
			foreach (string dialogName in possibleDialogNames)
			{
				if (this.DialogParametersProvider.GetDialogParameters(this.DialogOpenerIdentifier, dialogName) == null)
				{
					return new DialogParameters();
				}
			}
			return null;
		}

		// Token: 0x0600A36D RID: 41837 RVA: 0x00245B08 File Offset: 0x00243D08
		private IEnumerable<string> GetPossibleDialogNames()
		{
			if (this.RenderMode == RenderMode.Mobile)
			{
				return new string[]
				{
					this.PrefixDialogName(),
					this.DialogName
				};
			}
			return new string[]
			{
				this.DialogName
			};
		}

		// Token: 0x0600A36E RID: 41838 RVA: 0x00245B4C File Offset: 0x00243D4C
		private string PrefixDialogName()
		{
			string text = (this.RenderMode == RenderMode.Mobile) ? "Mobile" : string.Empty;
			if (!this.DialogName.StartsWith(text))
			{
				return text + this.DialogName;
			}
			return string.Empty;
		}

		// Token: 0x0600A36F RID: 41839 RVA: 0x00245B90 File Offset: 0x00243D90
		protected override void CreateChildControls()
		{
			base.CreateChildControls();
			this.CreatePageControls();
			if (!string.IsNullOrEmpty(base.Request.QueryString["checkHandler"]))
			{
				base.Title = "HandlerCheckOK";
				base.Form.Controls.Add(new LiteralControl("HandlerCheckOK"));
				return;
			}
			this.CreateTelerikManagers();
			if (this.DialogParametersProviderType == typeof(JavascriptDialogParametersProvider))
			{
				this._parameterObtainer = new JsParameterObtainer();
				base.Form.Controls.Add(this._parameterObtainer);
			}
			this.LoadDialogControl();
			this.ConfigureTelerikManagers();
		}

		// Token: 0x0600A370 RID: 41840 RVA: 0x00245C38 File Offset: 0x00243E38
		private void ConfigureTelerikManagers()
		{
			if (this._scriptManagerProperties != null && this.UseRSM)
			{
				this._scriptManager.DeserializeScriptManagerProperties(this._scriptManagerProperties);
			}
			else
			{
				this._scriptManager.EnableScriptCombine = false;
			}
			if (this._styleManagerProperties != null && this.UseRSM)
			{
				this._styleManager.DeserializeStyleSheetManagerProperties(this._styleManagerProperties);
			}
		}

		// Token: 0x0600A371 RID: 41841 RVA: 0x00245C98 File Offset: 0x00243E98
		private void CreateTelerikManagers()
		{
			this._scriptManager = new RadScriptManager();
			this._scriptManager.ID = "RadScriptManager1";
			this._scriptManager.EnableHandlerDetection = false;
			this._styleManager = new RadStyleSheetManager();
			this._styleManager.ID = "RadStyleSheetManager1";
			this._styleManager.EnableHandlerDetection = false;
			base.Form.Controls.Add(this._scriptManager);
			base.Form.Controls.Add(this._styleManager);
			this._scriptManager.EnableScriptCombine = true;
			this._styleManager.EnableStyleSheetCombine = true;
		}

		// Token: 0x0600A372 RID: 41842 RVA: 0x00245D38 File Offset: 0x00243F38
		private void CreatePageControls()
		{
			this.Controls.Add(DialogHandlerNoSession.GetDocType());
			HtmlGenericControl html = DialogHandlerNoSession.GetHtml();
			html.Attributes.Add("class", this.GetHtmlCssClasses());
			this.Controls.Add(html);
			html.Controls.Add(this.GetHead());
			HtmlGenericControl body = DialogHandlerNoSession.GetBody();
			html.Controls.Add(body);
			body.Controls.Add(DialogHandlerNoSession.GetForm());
		}

		// Token: 0x0600A373 RID: 41843 RVA: 0x00245DB0 File Offset: 0x00243FB0
		private string GetHtmlCssClasses()
		{
			string item = "red" + this.DialogName;
			string item2 = string.Format("re{0}Dialog", this.RenderMode.ToString());
			List<string> list = new List<string>
			{
				item,
				item2
			};
			return string.Join(" ", list.ToArray());
		}

		// Token: 0x0600A374 RID: 41844 RVA: 0x00245E0F File Offset: 0x0024400F
		private static LiteralControl GetDocType()
		{
			return new LiteralControl("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.1//EN\" \"http://www.w3.org/tr/xhtml11/dtd/xhtml11.dtd\">" + Environment.NewLine + Environment.NewLine);
		}

		// Token: 0x0600A375 RID: 41845 RVA: 0x00245E2C File Offset: 0x0024402C
		private static HtmlGenericControl GetHtml()
		{
			HtmlGenericControl htmlGenericControl = new HtmlGenericControl("html");
			htmlGenericControl.Attributes.Add("xmlns", "http://www.w3.org/1999/xhtml");
			return htmlGenericControl;
		}

		// Token: 0x0600A376 RID: 41846 RVA: 0x00245E5C File Offset: 0x0024405C
		private HtmlHead GetHead()
		{
			HtmlHead htmlHead = new HtmlHead();
			htmlHead.Controls.Add(new LiteralControl("\r\n"));
			htmlHead.Controls.Add(new LiteralControl("<base target=\"_self\" />\r\n"));
			HtmlTitle htmlTitle = new HtmlTitle();
			htmlTitle.Text = base.Server.HtmlEncode(this.PageTitle);
			htmlHead.Controls.Add(htmlTitle);
			htmlHead.Controls.Add(new LiteralControl("\r\n"));
			return htmlHead;
		}

		// Token: 0x0600A377 RID: 41847 RVA: 0x00245ED8 File Offset: 0x002440D8
		private static HtmlGenericControl GetBody()
		{
			return new HtmlGenericControl("body");
		}

		// Token: 0x0600A378 RID: 41848 RVA: 0x00245EF4 File Offset: 0x002440F4
		private static HtmlForm GetForm()
		{
			return new HtmlForm
			{
				ID = "Form1"
			};
		}

		// Token: 0x0600A379 RID: 41849 RVA: 0x00245F14 File Offset: 0x00244114
		private void LoadDialogControl()
		{
			DialogParameters dialogParameters = null;
			try
			{
				dialogParameters = this.GetDialogParameters();
			}
			catch (Exception ex)
			{
				string text = "<div style='color:red'>Cannot deserialize dialog parameters. Please refresh the editor page.</div>";
				string message;
				if (ex is CryptographicException)
				{
					Exception ex2 = new Exception(null);
					message = ex2.Message;
				}
				else
				{
					message = ex.Message;
				}
				text = text + "<div>Error Message:" + message + "</div>";
				base.Form.Controls.Add(new LiteralControl(text));
			}
			if (dialogParameters == null || dialogParameters.Count == 0)
			{
				return;
			}
			this._cssFile = (string)dialogParameters["DialogsCssFile"];
			this._scriptFile = (string)dialogParameters["DialogsScriptFile"];
			this._scriptManagerProperties = (string)dialogParameters["ScriptManagerProperties"];
			this._styleManagerProperties = (string)dialogParameters["StyleManagerProperties"];
			DialogDefinition dialogDefinition = new DialogDefinition(dialogParameters);
			Control control;
			if (dialogDefinition.VirtualPath == null)
			{
				control = (Control)Activator.CreateInstance(dialogDefinition.DialogType, new object[0]);
			}
			else
			{
				control = (UserControl)this.Page.LoadControl(dialogDefinition.VirtualPath);
			}
			this.SetupControl(control, dialogParameters);
			control.ID = "dialogControl";
			this.AddDialogControl(control, dialogParameters);
		}

		// Token: 0x0600A37A RID: 41850 RVA: 0x00246058 File Offset: 0x00244258
		private void SetupControl(Control dialogControl, DialogParameters dialogParameters)
		{
			ISkinnableControl skinnableControl = dialogControl as ISkinnableControl;
			if (skinnableControl != null)
			{
				skinnableControl.Skin = this.Skin;
				if (dialogParameters["EnableEmbeddedSkins"] != null)
				{
					skinnableControl.EnableEmbeddedSkins = (bool)dialogParameters["EnableEmbeddedSkins"];
				}
				if (dialogParameters["EnableEmbeddedBaseStylesheet"] != null)
				{
					skinnableControl.EnableEmbeddedBaseStylesheet = (bool)dialogParameters["EnableEmbeddedBaseStylesheet"];
				}
			}
			UserControlBase userControlBase = dialogControl as UserControlBase;
			if (userControlBase != null)
			{
				userControlBase.RenderMode = this.RenderMode;
				userControlBase.Title = this.PageTitle;
				userControlBase.Language = (string)dialogParameters["Language"];
				userControlBase.ExternalDialogsPath = (string)dialogParameters["ExternalDialogsPath"];
				userControlBase.LocalizationPath = (string)dialogParameters["LocalizationPath"];
				userControlBase.IsInAccessibleMode = (dialogParameters.Contains("IsInAccessibleMode") && (bool)dialogParameters["IsInAccessibleMode"]);
			}
			ImageEditorDialog imageEditorDialog = dialogControl as ImageEditorDialog;
			if (imageEditorDialog != null)
			{
				imageEditorDialog.FileBrowserContentProviderTypeName = (string)dialogParameters["FileBrowserContentProviderTypeName"];
			}
		}

		// Token: 0x0600A37B RID: 41851 RVA: 0x00246170 File Offset: 0x00244370
		private void AddDialogControl(Control dialogControl, DialogParameters dialogParameters)
		{
			UserControlBase userControlBase = dialogControl as UserControlBase;
			if (userControlBase != null)
			{
				RadFormDecorator radFormDecorator = new RadFormDecorator();
				radFormDecorator.ID = "dialogsDecorator";
				radFormDecorator.EnableRoundedCorners = (this.RenderMode == RenderMode.Lightweight);
				radFormDecorator.DecoratedControls = FormDecoratorDecoratedControls.All;
				if (this.RenderMode == RenderMode.Mobile)
				{
					radFormDecorator.ControlsToSkip = (FormDecoratorDecoratedControls.Scrollbars | FormDecoratorDecoratedControls.Select);
				}
				else
				{
					radFormDecorator.ControlsToSkip = FormDecoratorDecoratedControls.Scrollbars;
				}
				radFormDecorator.RenderMode = this.RenderMode;
				this.SetupControl(radFormDecorator, dialogParameters);
				base.Form.Controls.Add(radFormDecorator);
			}
			if (dialogControl is IClientParameterConsumer)
			{
				DialogControlInitializer dialogControlInitializer = new DialogControlInitializer();
				dialogControlInitializer.ID = "initializer";
				base.Form.Controls.Add(dialogControlInitializer);
				dialogControlInitializer.Controls.Add(dialogControl);
				return;
			}
			base.Form.Controls.Add(dialogControl);
		}

		// Token: 0x04002D6C RID: 11628
		public const string DefaultUrl = "Telerik.Web.UI.DialogHandler.aspx";

		// Token: 0x04002D6D RID: 11629
		private DialogParametersProvider _dialogParametersProvider;

		// Token: 0x04002D6E RID: 11630
		private JsParameterObtainer _parameterObtainer;

		// Token: 0x04002D6F RID: 11631
		private string _cssFile;

		// Token: 0x04002D70 RID: 11632
		private string _scriptFile;

		// Token: 0x04002D71 RID: 11633
		private string _scriptManagerProperties;

		// Token: 0x04002D72 RID: 11634
		private string _styleManagerProperties;

		// Token: 0x04002D73 RID: 11635
		private RadScriptManager _scriptManager;

		// Token: 0x04002D74 RID: 11636
		private RadStyleSheetManager _styleManager;
	}
}
