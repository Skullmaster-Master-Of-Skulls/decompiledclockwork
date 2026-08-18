using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Web.Util;

namespace System.Web.UI.WebControls
{
	// Token: 0x0200037E RID: 894
	[DefaultProperty("ErrorMessage")]
	[Designer("System.Web.UI.Design.WebControls.BaseValidatorDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public abstract class BaseValidator : Label, IValidator
	{
		// Token: 0x0600296C RID: 10604 RVA: 0x00085E09 File Offset: 0x00084009
		protected BaseValidator()
		{
			this.isValid = true;
			this.propertiesChecked = false;
			this.propertiesValid = true;
			this.renderUplevel = false;
		}

		// Token: 0x17000B80 RID: 2944
		// (get) Token: 0x0600296D RID: 10605 RVA: 0x00085E2D File Offset: 0x0008402D
		protected bool IsUnobtrusive
		{
			get
			{
				return this.Page != null && this.Page.UnobtrusiveValidationMode > UnobtrusiveValidationMode.None;
			}
		}

		// Token: 0x17000B81 RID: 2945
		// (get) Token: 0x0600296E RID: 10606 RVA: 0x00082AB0 File Offset: 0x00080CB0
		// (set) Token: 0x0600296F RID: 10607 RVA: 0x00085E47 File Offset: 0x00084047
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string AssociatedControlID
		{
			get
			{
				return base.AssociatedControlID;
			}
			set
			{
				throw new NotSupportedException(SR.GetString("Property_Not_Supported", new object[]
				{
					"AssociatedControlID",
					base.GetType().ToString()
				}));
			}
		}

		// Token: 0x17000B82 RID: 2946
		// (get) Token: 0x06002970 RID: 10608 RVA: 0x00085E74 File Offset: 0x00084074
		// (set) Token: 0x06002971 RID: 10609 RVA: 0x00085E7C File Offset: 0x0008407C
		[DefaultValue(typeof(Color), "Red")]
		public override Color ForeColor
		{
			get
			{
				return base.ForeColor;
			}
			set
			{
				this.wasForeColorSet = true;
				base.ForeColor = value;
			}
		}

		// Token: 0x17000B83 RID: 2947
		// (get) Token: 0x06002972 RID: 10610 RVA: 0x00085E8C File Offset: 0x0008408C
		// (set) Token: 0x06002973 RID: 10611 RVA: 0x00085EB9 File Offset: 0x000840B9
		[WebCategory("Behavior")]
		[Themeable(false)]
		[DefaultValue("")]
		[IDReferenceProperty]
		[WebSysDescription("BaseValidator_ControlToValidate")]
		[TypeConverter(typeof(ValidatedControlConverter))]
		public string ControlToValidate
		{
			get
			{
				object obj = this.ViewState["ControlToValidate"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["ControlToValidate"] = value;
			}
		}

		// Token: 0x17000B84 RID: 2948
		// (get) Token: 0x06002974 RID: 10612 RVA: 0x00085ECC File Offset: 0x000840CC
		// (set) Token: 0x06002975 RID: 10613 RVA: 0x00085EF9 File Offset: 0x000840F9
		[Localizable(true)]
		[WebCategory("Appearance")]
		[DefaultValue("")]
		[WebSysDescription("BaseValidator_ErrorMessage")]
		public string ErrorMessage
		{
			get
			{
				object obj = this.ViewState["ErrorMessage"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["ErrorMessage"] = value;
			}
		}

		// Token: 0x17000B85 RID: 2949
		// (get) Token: 0x06002976 RID: 10614 RVA: 0x00085F0C File Offset: 0x0008410C
		// (set) Token: 0x06002977 RID: 10615 RVA: 0x00085F35 File Offset: 0x00084135
		[WebCategory("Behavior")]
		[Themeable(false)]
		[DefaultValue(true)]
		[WebSysDescription("BaseValidator_EnableClientScript")]
		public bool EnableClientScript
		{
			get
			{
				object obj = this.ViewState["EnableClientScript"];
				return obj == null || (bool)obj;
			}
			set
			{
				this.ViewState["EnableClientScript"] = value;
			}
		}

		// Token: 0x17000B86 RID: 2950
		// (get) Token: 0x06002978 RID: 10616 RVA: 0x00085F4D File Offset: 0x0008414D
		// (set) Token: 0x06002979 RID: 10617 RVA: 0x00085F55 File Offset: 0x00084155
		public override bool Enabled
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
					this.isValid = true;
				}
			}
		}

		// Token: 0x17000B87 RID: 2951
		// (get) Token: 0x0600297A RID: 10618 RVA: 0x000097B7 File Offset: 0x000079B7
		internal override bool IsReloadable
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000B88 RID: 2952
		// (get) Token: 0x0600297B RID: 10619 RVA: 0x00085F68 File Offset: 0x00084168
		// (set) Token: 0x0600297C RID: 10620 RVA: 0x00085F70 File Offset: 0x00084170
		[Browsable(false)]
		[WebCategory("Behavior")]
		[Themeable(false)]
		[DefaultValue(true)]
		[WebSysDescription("BaseValidator_IsValid")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool IsValid
		{
			get
			{
				return this.isValid;
			}
			set
			{
				this.isValid = value;
			}
		}

		// Token: 0x17000B89 RID: 2953
		// (get) Token: 0x0600297D RID: 10621 RVA: 0x00085F79 File Offset: 0x00084179
		protected bool PropertiesValid
		{
			get
			{
				if (!this.propertiesChecked)
				{
					this.propertiesValid = this.ControlPropertiesValid();
					this.propertiesChecked = true;
				}
				return this.propertiesValid;
			}
		}

		// Token: 0x17000B8A RID: 2954
		// (get) Token: 0x0600297E RID: 10622 RVA: 0x00085F9C File Offset: 0x0008419C
		protected bool RenderUplevel
		{
			get
			{
				return this.renderUplevel;
			}
		}

		// Token: 0x17000B8B RID: 2955
		// (get) Token: 0x0600297F RID: 10623 RVA: 0x00085FA4 File Offset: 0x000841A4
		// (set) Token: 0x06002980 RID: 10624 RVA: 0x00085FCD File Offset: 0x000841CD
		[WebCategory("Appearance")]
		[Themeable(true)]
		[DefaultValue(ValidatorDisplay.Static)]
		[WebSysDescription("BaseValidator_Display")]
		public ValidatorDisplay Display
		{
			get
			{
				object obj = this.ViewState["Display"];
				if (obj != null)
				{
					return (ValidatorDisplay)obj;
				}
				return ValidatorDisplay.Static;
			}
			set
			{
				if (value < ValidatorDisplay.None || value > ValidatorDisplay.Dynamic)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.ViewState["Display"] = value;
			}
		}

		// Token: 0x17000B8C RID: 2956
		// (get) Token: 0x06002981 RID: 10625 RVA: 0x00085FF8 File Offset: 0x000841F8
		// (set) Token: 0x06002982 RID: 10626 RVA: 0x00082AED File Offset: 0x00080CED
		[WebCategory("Behavior")]
		[Themeable(false)]
		[DefaultValue(false)]
		[WebSysDescription("BaseValidator_SetFocusOnError")]
		public bool SetFocusOnError
		{
			get
			{
				object obj = this.ViewState["SetFocusOnError"];
				return obj != null && (bool)obj;
			}
			set
			{
				this.ViewState["SetFocusOnError"] = value;
			}
		}

		// Token: 0x17000B8D RID: 2957
		// (get) Token: 0x06002983 RID: 10627 RVA: 0x00086021 File Offset: 0x00084221
		// (set) Token: 0x06002984 RID: 10628 RVA: 0x00086029 File Offset: 0x00084229
		[WebCategory("Appearance")]
		[DefaultValue("")]
		[WebSysDescription("BaseValidator_Text")]
		[PersistenceMode(PersistenceMode.InnerDefaultProperty)]
		public override string Text
		{
			get
			{
				return base.Text;
			}
			set
			{
				base.Text = value;
			}
		}

		// Token: 0x17000B8E RID: 2958
		// (get) Token: 0x06002985 RID: 10629 RVA: 0x00086034 File Offset: 0x00084234
		// (set) Token: 0x06002986 RID: 10630 RVA: 0x0007E369 File Offset: 0x0007C569
		[WebCategory("Behavior")]
		[Themeable(false)]
		[DefaultValue("")]
		[WebSysDescription("BaseValidator_ValidationGroup")]
		public virtual string ValidationGroup
		{
			get
			{
				object obj = this.ViewState["ValidationGroup"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["ValidationGroup"] = value;
			}
		}

		// Token: 0x06002987 RID: 10631 RVA: 0x00086064 File Offset: 0x00084264
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			bool flag = !this.Enabled;
			if (flag)
			{
				this.Enabled = true;
			}
			try
			{
				if (this.RenderUplevel)
				{
					base.EnsureID();
					string clientID = this.ClientID;
					HtmlTextWriter writer2 = (base.EnableLegacyRendering || this.IsUnobtrusive) ? writer : null;
					if (this.IsUnobtrusive)
					{
						base.Attributes["data-val"] = "true";
					}
					if (this.ControlToValidate.Length > 0)
					{
						this.AddExpandoAttribute(writer2, clientID, "controltovalidate", this.GetControlRenderID(this.ControlToValidate));
					}
					if (this.SetFocusOnError)
					{
						this.AddExpandoAttribute(writer2, clientID, "focusOnError", "t", false);
					}
					if (this.ErrorMessage.Length > 0)
					{
						this.AddExpandoAttribute(writer2, clientID, "errormessage", this.ErrorMessage);
					}
					ValidatorDisplay display = this.Display;
					if (display != ValidatorDisplay.Static)
					{
						this.AddExpandoAttribute(writer2, clientID, "display", PropertyConverter.EnumToString(typeof(ValidatorDisplay), display), false);
					}
					if (!this.IsValid)
					{
						this.AddExpandoAttribute(writer2, clientID, "isvalid", "False", false);
					}
					if (flag)
					{
						this.AddExpandoAttribute(writer2, clientID, "enabled", "False", false);
					}
					if (this.ValidationGroup.Length > 0)
					{
						this.AddExpandoAttribute(writer2, clientID, "validationGroup", this.ValidationGroup);
					}
				}
				base.AddAttributesToRender(writer);
			}
			finally
			{
				if (flag)
				{
					this.Enabled = false;
				}
			}
		}

		// Token: 0x06002988 RID: 10632 RVA: 0x000861E4 File Offset: 0x000843E4
		internal void AddExpandoAttribute(HtmlTextWriter writer, string controlId, string attributeName, string attributeValue)
		{
			this.AddExpandoAttribute(writer, controlId, attributeName, attributeValue, true);
		}

		// Token: 0x06002989 RID: 10633 RVA: 0x000861F2 File Offset: 0x000843F2
		internal void AddExpandoAttribute(HtmlTextWriter writer, string controlId, string attributeName, string attributeValue, bool encode)
		{
			BaseValidator.AddExpandoAttribute(this, writer, controlId, attributeName, attributeValue, encode);
		}

		// Token: 0x0600298A RID: 10634 RVA: 0x00086204 File Offset: 0x00084404
		internal static void AddExpandoAttribute(Control control, HtmlTextWriter writer, string controlId, string attributeName, string attributeValue, bool encode)
		{
			Page page = control.Page;
			if (writer != null)
			{
				if (page.UnobtrusiveValidationMode != UnobtrusiveValidationMode.None)
				{
					attributeName = "data-val-" + attributeName;
				}
				writer.AddAttribute(attributeName, attributeValue, encode);
				return;
			}
			if (!page.IsPartialRenderingSupported)
			{
				page.ClientScript.RegisterExpandoAttribute(controlId, attributeName, attributeValue, encode);
				return;
			}
			ValidatorCompatibilityHelper.RegisterExpandoAttribute(control, controlId, attributeName, attributeValue, encode);
		}

		// Token: 0x0600298B RID: 10635 RVA: 0x00086264 File Offset: 0x00084464
		protected void CheckControlValidationProperty(string name, string propertyName)
		{
			Control control = this.NamingContainer.FindControl(name);
			if (control == null)
			{
				throw new HttpException(SR.GetString("Validator_control_not_found", new object[]
				{
					name,
					propertyName,
					this.ID
				}));
			}
			if (BaseValidator.GetValidationProperty(control) == null)
			{
				throw new HttpException(SR.GetString("Validator_bad_control_type", new object[]
				{
					name,
					propertyName,
					this.ID
				}));
			}
		}

		// Token: 0x0600298C RID: 10636 RVA: 0x000862DC File Offset: 0x000844DC
		protected virtual bool ControlPropertiesValid()
		{
			string controlToValidate = this.ControlToValidate;
			if (controlToValidate.Length == 0)
			{
				throw new HttpException(SR.GetString("Validator_control_blank", new object[]
				{
					this.ID
				}));
			}
			this.CheckControlValidationProperty(controlToValidate, "ControlToValidate");
			return true;
		}

		// Token: 0x0600298D RID: 10637 RVA: 0x00086324 File Offset: 0x00084524
		protected virtual bool DetermineRenderUplevel()
		{
			Page page = this.Page;
			return page != null && page.RequestInternal != null && (this.EnableClientScript && page.Request.Browser.W3CDomVersion.Major >= 1) && page.Request.Browser.EcmaScriptVersion.CompareTo(new Version(1, 2)) >= 0;
		}

		// Token: 0x0600298E RID: 10638
		protected abstract bool EvaluateIsValid();

		// Token: 0x0600298F RID: 10639 RVA: 0x0008638C File Offset: 0x0008458C
		protected string GetControlRenderID(string name)
		{
			Control control = this.FindControl(name);
			if (control == null)
			{
				return string.Empty;
			}
			return control.ClientID;
		}

		// Token: 0x06002990 RID: 10640 RVA: 0x000863B0 File Offset: 0x000845B0
		protected string GetControlValidationValue(string name)
		{
			Control control = this.NamingContainer.FindControl(name);
			if (control == null)
			{
				return null;
			}
			PropertyDescriptor validationProperty = BaseValidator.GetValidationProperty(control);
			if (validationProperty == null)
			{
				return null;
			}
			object value = validationProperty.GetValue(control);
			if (value is ListItem)
			{
				return ((ListItem)value).Value;
			}
			if (value != null)
			{
				return value.ToString();
			}
			return string.Empty;
		}

		// Token: 0x06002991 RID: 10641 RVA: 0x00086408 File Offset: 0x00084608
		public static PropertyDescriptor GetValidationProperty(object component)
		{
			ValidationPropertyAttribute validationPropertyAttribute = (ValidationPropertyAttribute)TypeDescriptor.GetAttributes(component)[typeof(ValidationPropertyAttribute)];
			if (validationPropertyAttribute != null && validationPropertyAttribute.Name != null)
			{
				return TypeDescriptor.GetProperties(component, null)[validationPropertyAttribute.Name];
			}
			return null;
		}

		// Token: 0x06002992 RID: 10642 RVA: 0x0008644F File Offset: 0x0008464F
		protected internal override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			if (!this.wasForeColorSet && this.RenderingCompatibility < VersionUtil.Framework40)
			{
				this.ForeColor = Color.Red;
			}
			this.Page.Validators.Add(this);
		}

		// Token: 0x06002993 RID: 10643 RVA: 0x0008648E File Offset: 0x0008468E
		protected internal override void OnUnload(EventArgs e)
		{
			if (this.Page != null)
			{
				this.Page.Validators.Remove(this);
			}
			base.OnUnload(e);
		}

		// Token: 0x06002994 RID: 10644 RVA: 0x000864B0 File Offset: 0x000846B0
		protected internal override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			this.preRenderCalled = true;
			this.propertiesChecked = false;
			bool flag = this.PropertiesValid;
			this.renderUplevel = this.DetermineRenderUplevel();
			if (this.IsUnobtrusive && this.EnableClientScript)
			{
				this.RegisterUnobtrusiveScript();
			}
			if (this.renderUplevel)
			{
				this.RegisterValidatorCommonScript();
			}
		}

		// Token: 0x06002995 RID: 10645 RVA: 0x0008650C File Offset: 0x0008470C
		protected void RegisterValidatorCommonScript()
		{
			if (!this.Page.IsPartialRenderingSupported)
			{
				if (this.Page.ClientScript.IsClientScriptBlockRegistered(typeof(BaseValidator), "ValidatorIncludeScript"))
				{
					return;
				}
				this.Page.ClientScript.RegisterClientScriptResource(typeof(BaseValidator), "WebUIValidation.js");
				this.Page.ClientScript.RegisterOnSubmitStatement(typeof(BaseValidator), "ValidatorOnSubmit", "if (typeof(ValidatorOnSubmit) == \"function\" && ValidatorOnSubmit() == false) return false;");
				if (!this.IsUnobtrusive)
				{
					this.Page.ClientScript.RegisterStartupScript(typeof(BaseValidator), "ValidatorIncludeScript", "\r\nvar Page_ValidationActive = false;\r\nif (typeof(ValidatorOnLoad) == \"function\") {\r\n    ValidatorOnLoad();\r\n}\r\n\r\nfunction ValidatorOnSubmit() {\r\n    if (Page_ValidationActive) {\r\n        return ValidatorCommonOnSubmit();\r\n    }\r\n    else {\r\n        return true;\r\n    }\r\n}\r\n        ", true);
					return;
				}
			}
			else
			{
				ValidatorCompatibilityHelper.RegisterClientScriptResource(this, typeof(BaseValidator), "WebUIValidation.js");
				ValidatorCompatibilityHelper.RegisterOnSubmitStatement(this, typeof(BaseValidator), "ValidatorOnSubmit", "if (typeof(ValidatorOnSubmit) == \"function\" && ValidatorOnSubmit() == false) return false;");
				if (!this.IsUnobtrusive)
				{
					ValidatorCompatibilityHelper.RegisterStartupScript(this, typeof(BaseValidator), "ValidatorIncludeScript", "\r\nvar Page_ValidationActive = false;\r\nif (typeof(ValidatorOnLoad) == \"function\") {\r\n    ValidatorOnLoad();\r\n}\r\n\r\nfunction ValidatorOnSubmit() {\r\n    if (Page_ValidationActive) {\r\n        return ValidatorCommonOnSubmit();\r\n    }\r\n    else {\r\n        return true;\r\n    }\r\n}\r\n        ", true);
				}
			}
		}

		// Token: 0x06002996 RID: 10646 RVA: 0x0008660E File Offset: 0x0008480E
		internal void RegisterUnobtrusiveScript()
		{
			ClientScriptManager.EnsureJqueryRegistered();
			ValidatorCompatibilityHelper.RegisterClientScriptResource(this, "jquery");
		}

		// Token: 0x06002997 RID: 10647 RVA: 0x00086620 File Offset: 0x00084820
		protected virtual void RegisterValidatorDeclaration()
		{
			string arrayValue = "document.getElementById(\"" + this.ClientID + "\")";
			if (!this.Page.IsPartialRenderingSupported)
			{
				this.Page.ClientScript.RegisterArrayDeclaration("Page_Validators", arrayValue);
				return;
			}
			ValidatorCompatibilityHelper.RegisterArrayDeclaration(this, "Page_Validators", arrayValue);
			ValidatorCompatibilityHelper.RegisterStartupScript(this, typeof(BaseValidator), this.ClientID + "_DisposeScript", string.Format(CultureInfo.InvariantCulture, "\r\ndocument.getElementById('{0}').dispose = function() {{\r\n    Array.remove({1}, document.getElementById('{0}'));\r\n}}\r\n", new object[]
			{
				this.ClientID,
				"Page_Validators"
			}), true);
		}

		// Token: 0x06002998 RID: 10648 RVA: 0x000866BC File Offset: 0x000848BC
		protected internal override void Render(HtmlTextWriter writer)
		{
			bool flag;
			if (base.DesignMode || (!this.preRenderCalled && this.Page == null))
			{
				this.propertiesChecked = true;
				this.propertiesValid = true;
				this.renderUplevel = false;
				flag = true;
			}
			else
			{
				flag = (this.Enabled && !this.IsValid);
			}
			if (!this.PropertiesValid)
			{
				return;
			}
			if (this.Page != null)
			{
				this.Page.VerifyRenderingInServerForm(this);
			}
			ValidatorDisplay display = this.Display;
			bool flag2;
			bool flag3;
			if (this.RenderUplevel)
			{
				flag2 = true;
				flag3 = (display > ValidatorDisplay.None);
			}
			else
			{
				flag3 = (display > ValidatorDisplay.None && flag);
				flag2 = flag3;
			}
			if (flag2 && this.RenderUplevel)
			{
				if (!this.IsUnobtrusive)
				{
					this.RegisterValidatorDeclaration();
				}
				if (display == ValidatorDisplay.None || (!flag && display == ValidatorDisplay.Dynamic))
				{
					base.Style["display"] = "none";
				}
				else if (!flag)
				{
					base.Style["visibility"] = "hidden";
				}
			}
			if (flag2)
			{
				this.RenderBeginTag(writer);
			}
			if (flag3)
			{
				if (this.Text.Trim().Length > 0)
				{
					this.RenderContents(writer);
				}
				else if (base.HasRenderingData())
				{
					base.RenderContents(writer);
				}
				else
				{
					writer.Write(this.ErrorMessage);
				}
			}
			else if (!this.RenderUplevel && display == ValidatorDisplay.Static)
			{
				writer.Write("&nbsp;");
			}
			if (flag2)
			{
				this.RenderEndTag(writer);
			}
		}

		// Token: 0x06002999 RID: 10649 RVA: 0x00086808 File Offset: 0x00084A08
		internal bool ShouldSerializeForeColor()
		{
			Color left = (this.RenderingCompatibility < VersionUtil.Framework40) ? Color.Red : Color.Empty;
			return left != this.ForeColor;
		}

		// Token: 0x0600299A RID: 10650 RVA: 0x00086840 File Offset: 0x00084A40
		public void Validate()
		{
			this.IsValid = true;
			if (!this.Visible || !this.Enabled)
			{
				return;
			}
			this.propertiesChecked = false;
			if (!this.PropertiesValid)
			{
				return;
			}
			this.IsValid = this.EvaluateIsValid();
			if (!this.IsValid)
			{
				Page page = this.Page;
				if (page != null && this.SetFocusOnError)
				{
					string text = this.ControlToValidate;
					Control control = this.NamingContainer.FindControl(text);
					if (control != null)
					{
						text = control.ClientID;
					}
					this.Page.SetValidatorInvalidControlFocus(text);
				}
			}
		}

		// Token: 0x04001E5A RID: 7770
		private const string UnobtrusivePrefix = "data-val-";

		// Token: 0x04001E5B RID: 7771
		private const string jqueryScriptKey = "jquery";

		// Token: 0x04001E5C RID: 7772
		private const string ValidatorFileName = "WebUIValidation.js";

		// Token: 0x04001E5D RID: 7773
		private const string ValidatorIncludeScriptKey = "ValidatorIncludeScript";

		// Token: 0x04001E5E RID: 7774
		private const string ValidatorStartupScript = "\r\nvar Page_ValidationActive = false;\r\nif (typeof(ValidatorOnLoad) == \"function\") {\r\n    ValidatorOnLoad();\r\n}\r\n\r\nfunction ValidatorOnSubmit() {\r\n    if (Page_ValidationActive) {\r\n        return ValidatorCommonOnSubmit();\r\n    }\r\n    else {\r\n        return true;\r\n    }\r\n}\r\n        ";

		// Token: 0x04001E5F RID: 7775
		private bool preRenderCalled;

		// Token: 0x04001E60 RID: 7776
		private bool isValid;

		// Token: 0x04001E61 RID: 7777
		private bool propertiesChecked;

		// Token: 0x04001E62 RID: 7778
		private bool propertiesValid;

		// Token: 0x04001E63 RID: 7779
		private bool renderUplevel;

		// Token: 0x04001E64 RID: 7780
		private bool wasForeColorSet;
	}
}
