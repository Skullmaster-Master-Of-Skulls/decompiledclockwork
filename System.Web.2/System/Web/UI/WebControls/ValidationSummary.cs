using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Web.ModelBinding;
using System.Web.Util;

namespace System.Web.UI.WebControls
{
	// Token: 0x0200050C RID: 1292
	[Designer("System.Web.UI.Design.WebControls.ValidationSummaryDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public class ValidationSummary : WebControl
	{
		// Token: 0x060040FE RID: 16638 RVA: 0x000D4926 File Offset: 0x000D2B26
		public ValidationSummary() : base(HtmlTextWriterTag.Div)
		{
			this.renderUplevel = false;
		}

		// Token: 0x17001304 RID: 4868
		// (get) Token: 0x060040FF RID: 16639 RVA: 0x00085E2D File Offset: 0x0008402D
		private bool IsUnobtrusive
		{
			get
			{
				return this.Page != null && this.Page.UnobtrusiveValidationMode > UnobtrusiveValidationMode.None;
			}
		}

		// Token: 0x17001305 RID: 4869
		// (get) Token: 0x06004100 RID: 16640 RVA: 0x000D4938 File Offset: 0x000D2B38
		// (set) Token: 0x06004101 RID: 16641 RVA: 0x000D4961 File Offset: 0x000D2B61
		[WebCategory("Appearance")]
		[DefaultValue(ValidationSummaryDisplayMode.BulletList)]
		[WebSysDescription("ValidationSummary_DisplayMode")]
		public ValidationSummaryDisplayMode DisplayMode
		{
			get
			{
				object obj = this.ViewState["DisplayMode"];
				if (obj != null)
				{
					return (ValidationSummaryDisplayMode)obj;
				}
				return ValidationSummaryDisplayMode.BulletList;
			}
			set
			{
				if (value < ValidationSummaryDisplayMode.List || value > ValidationSummaryDisplayMode.SingleParagraph)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.ViewState["DisplayMode"] = value;
			}
		}

		// Token: 0x17001306 RID: 4870
		// (get) Token: 0x06004102 RID: 16642 RVA: 0x000D498C File Offset: 0x000D2B8C
		// (set) Token: 0x06004103 RID: 16643 RVA: 0x00085F35 File Offset: 0x00084135
		[WebCategory("Behavior")]
		[Themeable(false)]
		[DefaultValue(true)]
		[WebSysDescription("ValidationSummary_EnableClientScript")]
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

		// Token: 0x17001307 RID: 4871
		// (get) Token: 0x06004104 RID: 16644 RVA: 0x000D49B8 File Offset: 0x000D2BB8
		// (set) Token: 0x06004105 RID: 16645 RVA: 0x000D49E1 File Offset: 0x000D2BE1
		[WebCategory("Behavior")]
		[Themeable(false)]
		[DefaultValue(true)]
		[WebSysDescription("ValidationSummary_ShowValidationErrors")]
		public bool ShowValidationErrors
		{
			get
			{
				object obj = this.ViewState["ShowValidationErrors"];
				return obj == null || (bool)obj;
			}
			set
			{
				this.ViewState["ShowValidationErrors"] = value;
			}
		}

		// Token: 0x17001308 RID: 4872
		// (get) Token: 0x06004106 RID: 16646 RVA: 0x000D49FC File Offset: 0x000D2BFC
		// (set) Token: 0x06004107 RID: 16647 RVA: 0x000D4A25 File Offset: 0x000D2C25
		[WebCategory("Behavior")]
		[Themeable(false)]
		[DefaultValue(true)]
		[WebSysDescription("ValidationSummary_ShowModelStateErrors")]
		public bool ShowModelStateErrors
		{
			get
			{
				object obj = this.ViewState["ShowModelStateErrors"];
				return obj == null || (bool)obj;
			}
			set
			{
				this.ViewState["ShowModelStateErrors"] = value;
			}
		}

		// Token: 0x17001309 RID: 4873
		// (get) Token: 0x06004108 RID: 16648 RVA: 0x00085E74 File Offset: 0x00084074
		// (set) Token: 0x06004109 RID: 16649 RVA: 0x000D4A3D File Offset: 0x000D2C3D
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

		// Token: 0x1700130A RID: 4874
		// (get) Token: 0x0600410A RID: 16650 RVA: 0x000D4A50 File Offset: 0x000D2C50
		// (set) Token: 0x0600410B RID: 16651 RVA: 0x000A0A1D File Offset: 0x0009EC1D
		[Localizable(true)]
		[WebCategory("Appearance")]
		[DefaultValue("")]
		[WebSysDescription("ValidationSummary_HeaderText")]
		public string HeaderText
		{
			get
			{
				object obj = this.ViewState["HeaderText"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["HeaderText"] = value;
			}
		}

		// Token: 0x1700130B RID: 4875
		// (get) Token: 0x0600410C RID: 16652 RVA: 0x000853AC File Offset: 0x000835AC
		public override bool SupportsDisabledAttribute
		{
			get
			{
				return this.RenderingCompatibility < VersionUtil.Framework40;
			}
		}

		// Token: 0x1700130C RID: 4876
		// (get) Token: 0x0600410D RID: 16653 RVA: 0x000D4A80 File Offset: 0x000D2C80
		// (set) Token: 0x0600410E RID: 16654 RVA: 0x000D4AA9 File Offset: 0x000D2CA9
		[WebCategory("Behavior")]
		[DefaultValue(false)]
		[WebSysDescription("ValidationSummary_ShowMessageBox")]
		public bool ShowMessageBox
		{
			get
			{
				object obj = this.ViewState["ShowMessageBox"];
				return obj != null && (bool)obj;
			}
			set
			{
				this.ViewState["ShowMessageBox"] = value;
			}
		}

		// Token: 0x1700130D RID: 4877
		// (get) Token: 0x0600410F RID: 16655 RVA: 0x000D4AC4 File Offset: 0x000D2CC4
		// (set) Token: 0x06004110 RID: 16656 RVA: 0x000D4AED File Offset: 0x000D2CED
		[WebCategory("Behavior")]
		[DefaultValue(true)]
		[WebSysDescription("ValidationSummary_ShowSummary")]
		public bool ShowSummary
		{
			get
			{
				object obj = this.ViewState["ShowSummary"];
				return obj == null || (bool)obj;
			}
			set
			{
				this.ViewState["ShowSummary"] = value;
			}
		}

		// Token: 0x1700130E RID: 4878
		// (get) Token: 0x06004111 RID: 16657 RVA: 0x000D4B08 File Offset: 0x000D2D08
		// (set) Token: 0x06004112 RID: 16658 RVA: 0x0007E369 File Offset: 0x0007C569
		[WebCategory("Behavior")]
		[Themeable(false)]
		[DefaultValue("")]
		[WebSysDescription("ValidationSummary_ValidationGroup")]
		public virtual string ValidationGroup
		{
			get
			{
				string text = (string)this.ViewState["ValidationGroup"];
				if (text != null)
				{
					return text;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["ValidationGroup"] = value;
			}
		}

		// Token: 0x06004113 RID: 16659 RVA: 0x000D4B38 File Offset: 0x000D2D38
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			if (this.renderUplevel)
			{
				base.EnsureID();
				string clientID = this.ClientID;
				HtmlTextWriter writer2 = (base.EnableLegacyRendering || this.IsUnobtrusive) ? writer : null;
				if (this.IsUnobtrusive)
				{
					base.Attributes["data-valsummary"] = "true";
				}
				if (this.HeaderText.Length > 0)
				{
					BaseValidator.AddExpandoAttribute(this, writer2, clientID, "headertext", this.HeaderText, true);
				}
				if (this.ShowMessageBox)
				{
					BaseValidator.AddExpandoAttribute(this, writer2, clientID, "showmessagebox", "True", false);
				}
				if (!this.ShowSummary)
				{
					BaseValidator.AddExpandoAttribute(this, writer2, clientID, "showsummary", "False", false);
				}
				if (this.DisplayMode != ValidationSummaryDisplayMode.BulletList)
				{
					BaseValidator.AddExpandoAttribute(this, writer2, clientID, "displaymode", PropertyConverter.EnumToString(typeof(ValidationSummaryDisplayMode), this.DisplayMode), false);
				}
				if (this.ValidationGroup.Length > 0)
				{
					BaseValidator.AddExpandoAttribute(this, writer2, clientID, "validationGroup", this.ValidationGroup, true);
				}
			}
			base.AddAttributesToRender(writer);
		}

		// Token: 0x06004114 RID: 16660 RVA: 0x000D4C44 File Offset: 0x000D2E44
		internal string[] GetErrorMessages(out bool inError)
		{
			List<string> list = new List<string>();
			inError = false;
			if (this.ShowValidationErrors)
			{
				ValidatorCollection validators = this.Page.GetValidators(this.ValidationGroup);
				for (int i = 0; i < validators.Count; i++)
				{
					IValidator validator = validators[i];
					if (!validator.IsValid)
					{
						inError = true;
						if (!string.IsNullOrEmpty(validator.ErrorMessage))
						{
							list.Add(string.Copy(validator.ErrorMessage));
						}
					}
				}
			}
			if (this.ShowModelStateErrors)
			{
				ModelStateDictionary modelState = this.Page.ModelState;
				if (!modelState.IsValid)
				{
					inError = true;
					foreach (KeyValuePair<string, ModelState> keyValuePair in modelState)
					{
						foreach (ModelError modelError in keyValuePair.Value.Errors)
						{
							if (!string.IsNullOrEmpty(modelError.ErrorMessage))
							{
								list.Add(modelError.ErrorMessage);
							}
						}
					}
				}
			}
			return list.ToArray();
		}

		// Token: 0x06004115 RID: 16661 RVA: 0x000D4D78 File Offset: 0x000D2F78
		protected internal override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			if (!this.wasForeColorSet && this.RenderingCompatibility < VersionUtil.Framework40)
			{
				this.ForeColor = Color.Red;
			}
		}

		// Token: 0x06004116 RID: 16662 RVA: 0x000D4DA8 File Offset: 0x000D2FA8
		protected internal override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			if (!this.Enabled)
			{
				return;
			}
			Page page = this.Page;
			if (page != null && page.RequestInternal != null)
			{
				this.renderUplevel = (this.EnableClientScript && this.ShowValidationErrors && page.Request.Browser.W3CDomVersion.Major >= 1 && page.Request.Browser.EcmaScriptVersion.CompareTo(new Version(1, 2)) >= 0);
			}
			if (this.renderUplevel && !this.IsUnobtrusive)
			{
				string arrayValue = "document.getElementById(\"" + this.ClientID + "\")";
				if (!this.Page.IsPartialRenderingSupported)
				{
					this.Page.ClientScript.RegisterArrayDeclaration("Page_ValidationSummaries", arrayValue);
					return;
				}
				ValidatorCompatibilityHelper.RegisterArrayDeclaration(this, "Page_ValidationSummaries", arrayValue);
				ValidatorCompatibilityHelper.RegisterStartupScript(this, typeof(ValidationSummary), this.ClientID + "_DisposeScript", string.Format(CultureInfo.InvariantCulture, "\r\n(function(id) {{\r\n    var e = document.getElementById(id);\r\n    if (e) {{\r\n        e.dispose = function() {{\r\n            Array.remove({1}, document.getElementById(id));\r\n        }}\r\n        e = null;\r\n    }}\r\n}})('{0}');\r\n", new object[]
				{
					this.ClientID,
					"Page_ValidationSummaries"
				}), true);
			}
		}

		// Token: 0x06004117 RID: 16663 RVA: 0x000D4ED0 File Offset: 0x000D30D0
		protected internal override void Render(HtmlTextWriter writer)
		{
			string[] array;
			bool flag;
			if (base.DesignMode)
			{
				array = new string[]
				{
					SR.GetString("ValSummary_error_message_1"),
					SR.GetString("ValSummary_error_message_2")
				};
				flag = true;
				this.renderUplevel = false;
			}
			else
			{
				if (!this.Enabled)
				{
					return;
				}
				bool flag2;
				array = this.GetErrorMessages(out flag2);
				flag = (this.ShowSummary && flag2);
				if (!flag && this.renderUplevel)
				{
					base.Style["display"] = "none";
				}
			}
			if (this.Page != null)
			{
				this.Page.VerifyRenderingInServerForm(this);
			}
			bool flag3 = this.renderUplevel || flag;
			if (flag3)
			{
				this.RenderBeginTag(writer);
			}
			if (flag)
			{
				string text;
				string value;
				string value2;
				string text2;
				string text3;
				switch (this.DisplayMode)
				{
				case ValidationSummaryDisplayMode.List:
					text = "b";
					value = string.Empty;
					value2 = string.Empty;
					text2 = "b";
					text3 = string.Empty;
					goto IL_12A;
				case ValidationSummaryDisplayMode.SingleParagraph:
					text = " ";
					value = string.Empty;
					value2 = string.Empty;
					text2 = " ";
					text3 = "b";
					goto IL_12A;
				}
				text = string.Empty;
				value = "<ul>";
				value2 = "<li>";
				text2 = "</li>";
				text3 = "</ul>";
				IL_12A:
				if (this.HeaderText.Length > 0)
				{
					writer.Write(this.HeaderText);
					this.WriteBreakIfPresent(writer, text);
				}
				if (array != null)
				{
					writer.Write(value);
					for (int i = 0; i < array.Length; i++)
					{
						writer.Write(value2);
						writer.Write(array[i]);
						this.WriteBreakIfPresent(writer, text2);
					}
					this.WriteBreakIfPresent(writer, text3);
				}
			}
			if (flag3)
			{
				this.RenderEndTag(writer);
			}
		}

		// Token: 0x06004118 RID: 16664 RVA: 0x000D5078 File Offset: 0x000D3278
		internal bool ShouldSerializeForeColor()
		{
			Color left = (this.RenderingCompatibility < VersionUtil.Framework40) ? Color.Red : Color.Empty;
			return left != this.ForeColor;
		}

		// Token: 0x06004119 RID: 16665 RVA: 0x000D50B0 File Offset: 0x000D32B0
		private void WriteBreakIfPresent(HtmlTextWriter writer, string text)
		{
			if (!(text == "b"))
			{
				writer.Write(text);
				return;
			}
			if (base.EnableLegacyRendering)
			{
				writer.WriteObsoleteBreak();
				return;
			}
			writer.WriteBreak();
		}

		// Token: 0x040024F5 RID: 9461
		private const string breakTag = "b";

		// Token: 0x040024F6 RID: 9462
		private bool renderUplevel;

		// Token: 0x040024F7 RID: 9463
		private bool wasForeColorSet;
	}
}
