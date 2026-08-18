using System;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AjaxControlToolkit.MaskedEditValidatorCompatibility
{
	// Token: 0x02000136 RID: 310
	internal static class ValidatorHelper
	{
		// Token: 0x060007AF RID: 1967 RVA: 0x00014640 File Offset: 0x00012840
		public static void DoBaseValidatorAddAttributes(BaseValidator validator, IBaseValidatorAccessor validatorAccessor, HtmlTextWriter writer)
		{
			bool flag = !validator.Enabled;
			if (flag)
			{
				validator.Enabled = true;
			}
			try
			{
				if (validatorAccessor.RenderUpLevel)
				{
					validatorAccessor.EnsureID();
					string clientID = validator.ClientID;
					if (validator.ControlToValidate.Length > 0)
					{
						ValidatorHelper.AddExpandoAttribute(validator, clientID, "controltovalidate", validatorAccessor.GetControlRenderID(validator.ControlToValidate));
					}
					if (validator.SetFocusOnError)
					{
						ValidatorHelper.AddExpandoAttribute(validator, clientID, "focusOnError", "t", false);
					}
					if (validator.ErrorMessage.Length > 0)
					{
						ValidatorHelper.AddExpandoAttribute(validator, clientID, "errormessage", validator.ErrorMessage);
					}
					ValidatorDisplay display = validator.Display;
					if (display != ValidatorDisplay.Static)
					{
						ValidatorHelper.AddExpandoAttribute(validator, clientID, "display", PropertyConverter.EnumToString(typeof(ValidatorDisplay), display), false);
					}
					if (!validator.IsValid)
					{
						ValidatorHelper.AddExpandoAttribute(validator, clientID, "isvalid", "False", false);
					}
					if (flag)
					{
						ValidatorHelper.AddExpandoAttribute(validator, clientID, "enabled", "False", false);
					}
					if (validator.ValidationGroup.Length > 0)
					{
						ValidatorHelper.AddExpandoAttribute(validator, clientID, "validationGroup", validator.ValidationGroup);
					}
				}
				ValidatorHelper.DoWebControlAddAttributes(validator, validatorAccessor, writer);
			}
			finally
			{
				if (flag)
				{
					validator.Enabled = false;
				}
			}
		}

		// Token: 0x060007B0 RID: 1968 RVA: 0x0001477C File Offset: 0x0001297C
		public static void DoWebControlAddAttributes(WebControl webControl, IWebControlAccessor webControlAccessor, HtmlTextWriter writer)
		{
			if (webControl.ID != null)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Id, webControl.ClientID);
			}
			string value = webControl.AccessKey;
			if (!string.IsNullOrEmpty(value))
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Accesskey, value);
			}
			if (!webControl.Enabled)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Disabled, "disabled");
			}
			short tabIndex = webControl.TabIndex;
			if (tabIndex != 0)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Tabindex, tabIndex.ToString(NumberFormatInfo.InvariantInfo));
			}
			value = webControl.ToolTip;
			if (!string.IsNullOrEmpty(value))
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Title, value);
			}
			if ((webControlAccessor.TagKey == HtmlTextWriterTag.Span || webControlAccessor.TagKey == HtmlTextWriterTag.A) && (webControl.BorderStyle != BorderStyle.NotSet || !webControl.BorderWidth.IsEmpty || !webControl.Height.IsEmpty || !webControl.Width.IsEmpty))
			{
				writer.AddStyleAttribute(HtmlTextWriterStyle.Display, "inline-block");
			}
			if (webControl.ControlStyleCreated && !webControl.ControlStyle.IsEmpty)
			{
				webControl.ControlStyle.AddAttributesToRender(writer, webControl);
			}
			AttributeCollection attributes = webControl.Attributes;
			foreach (object obj in attributes.Keys)
			{
				string text = (string)obj;
				writer.AddAttribute(text, attributes[text]);
			}
		}

		// Token: 0x060007B1 RID: 1969 RVA: 0x000148B8 File Offset: 0x00012AB8
		public static void DoInitRegistration(Page page)
		{
			page.ClientScript.RegisterClientScriptBlock(typeof(BaseValidator), "ValidatorIncludeScript", string.Empty);
		}

		// Token: 0x060007B2 RID: 1970 RVA: 0x000148DC File Offset: 0x00012ADC
		public static void DoValidatorArrayDeclaration(BaseValidator validator, Type validatorType)
		{
			string arrayValue = "document.getElementById(\"" + validator.ClientID + "\")";
			ScriptManager.RegisterArrayDeclaration(validator, "Page_Validators", arrayValue);
			ScriptManager.RegisterStartupScript(validator, validatorType, validator.ClientID + "_DisposeScript", string.Format(CultureInfo.InvariantCulture, "\ndocument.getElementById('{0}').dispose = function() {{\n    Array.remove(Page_Validators, document.getElementById('{0}'));\n}}\n", new object[]
			{
				validator.ClientID
			}), true);
		}

		// Token: 0x060007B3 RID: 1971 RVA: 0x00014944 File Offset: 0x00012B44
		public static void DoPreRenderRegistration(BaseValidator validator, IBaseValidatorAccessor validatorAccessor)
		{
			if (validatorAccessor.RenderUpLevel)
			{
				ScriptManager.RegisterClientScriptResource(validator, typeof(BaseValidator), "WebUIValidation.js");
				ScriptManager.RegisterStartupScript(validator, typeof(BaseValidator), "ValidatorIncludeScript", "\nvar Page_ValidationActive = false;\nif (typeof(ValidatorOnLoad) == \"function\") {\n    ValidatorOnLoad();\n}\n\nfunction ValidatorOnSubmit() {\n    if (Page_ValidationActive) {\n        return ValidatorCommonOnSubmit();\n    }\n    else {\n        return true;\n    }\n}\n", true);
				ScriptManager.RegisterOnSubmitStatement(validator, typeof(BaseValidator), "ValidatorOnSubmit", "if (typeof(ValidatorOnSubmit) == \"function\" && ValidatorOnSubmit() == false) return false;");
			}
		}

		// Token: 0x060007B4 RID: 1972 RVA: 0x000149A3 File Offset: 0x00012BA3
		public static void AddExpandoAttribute(WebControl webControl, string controlId, string attributeName, string attributeValue)
		{
			ValidatorHelper.AddExpandoAttribute(webControl, controlId, attributeName, attributeValue, true);
		}

		// Token: 0x060007B5 RID: 1973 RVA: 0x000149AF File Offset: 0x00012BAF
		public static void AddExpandoAttribute(WebControl webControl, string controlId, string attributeName, string attributeValue, bool encode)
		{
			ScriptManager.RegisterExpandoAttribute(webControl, controlId, attributeName, attributeValue, encode);
		}

		// Token: 0x0400032B RID: 811
		private const string ValidatorFileName = "WebUIValidation.js";

		// Token: 0x0400032C RID: 812
		private const string ValidatorIncludeScriptKey = "ValidatorIncludeScript";

		// Token: 0x0400032D RID: 813
		private const string ValidatorStartupScript = "\nvar Page_ValidationActive = false;\nif (typeof(ValidatorOnLoad) == \"function\") {\n    ValidatorOnLoad();\n}\n\nfunction ValidatorOnSubmit() {\n    if (Page_ValidationActive) {\n        return ValidatorCommonOnSubmit();\n    }\n    else {\n        return true;\n    }\n}\n";
	}
}
