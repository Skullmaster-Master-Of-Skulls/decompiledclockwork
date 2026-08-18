using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	// Token: 0x0200067E RID: 1662
	[Designer("System.Web.UI.Design.WebControls.PreviewControlDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class ValidationSummary : WebControl
	{
		// Token: 0x060051C0 RID: 20928 RVA: 0x0014A909 File Offset: 0x00149909
		public ValidationSummary() : base(HtmlTextWriterTag.Div)
		{
			this.renderUplevel = false;
			this.ForeColor = Color.Red;
		}

		// Token: 0x170014CD RID: 5325
		// (get) Token: 0x060051C1 RID: 20929 RVA: 0x0014A928 File Offset: 0x00149928
		// (set) Token: 0x060051C2 RID: 20930 RVA: 0x0014A951 File Offset: 0x00149951
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

		// Token: 0x170014CE RID: 5326
		// (get) Token: 0x060051C3 RID: 20931 RVA: 0x0014A97C File Offset: 0x0014997C
		// (set) Token: 0x060051C4 RID: 20932 RVA: 0x0014A9A5 File Offset: 0x001499A5
		[Themeable(false)]
		[WebCategory("Behavior")]
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

		// Token: 0x170014CF RID: 5327
		// (get) Token: 0x060051C5 RID: 20933 RVA: 0x0014A9BD File Offset: 0x001499BD
		// (set) Token: 0x060051C6 RID: 20934 RVA: 0x0014A9C5 File Offset: 0x001499C5
		[DefaultValue(typeof(Color), "Red")]
		public override Color ForeColor
		{
			get
			{
				return base.ForeColor;
			}
			set
			{
				base.ForeColor = value;
			}
		}

		// Token: 0x170014D0 RID: 5328
		// (get) Token: 0x060051C7 RID: 20935 RVA: 0x0014A9D0 File Offset: 0x001499D0
		// (set) Token: 0x060051C8 RID: 20936 RVA: 0x0014A9FD File Offset: 0x001499FD
		[DefaultValue("")]
		[WebCategory("Appearance")]
		[Localizable(true)]
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

		// Token: 0x170014D1 RID: 5329
		// (get) Token: 0x060051C9 RID: 20937 RVA: 0x0014AA10 File Offset: 0x00149A10
		// (set) Token: 0x060051CA RID: 20938 RVA: 0x0014AA39 File Offset: 0x00149A39
		[DefaultValue(false)]
		[WebCategory("Behavior")]
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

		// Token: 0x170014D2 RID: 5330
		// (get) Token: 0x060051CB RID: 20939 RVA: 0x0014AA54 File Offset: 0x00149A54
		// (set) Token: 0x060051CC RID: 20940 RVA: 0x0014AA7D File Offset: 0x00149A7D
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

		// Token: 0x170014D3 RID: 5331
		// (get) Token: 0x060051CD RID: 20941 RVA: 0x0014AA98 File Offset: 0x00149A98
		// (set) Token: 0x060051CE RID: 20942 RVA: 0x0014AAC5 File Offset: 0x00149AC5
		[WebSysDescription("ValidationSummary_ValidationGroup")]
		[WebCategory("Behavior")]
		[Themeable(false)]
		[DefaultValue("")]
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

		// Token: 0x060051CF RID: 20943 RVA: 0x0014AAD8 File Offset: 0x00149AD8
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			if (this.renderUplevel)
			{
				base.EnsureID();
				string clientID = this.ClientID;
				HtmlTextWriter writer2 = base.EnableLegacyRendering ? writer : null;
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

		// Token: 0x060051D0 RID: 20944 RVA: 0x0014ABBC File Offset: 0x00149BBC
		internal string[] GetErrorMessages(out bool inError)
		{
			string[] array = null;
			inError = false;
			int num = 0;
			ValidatorCollection validators = this.Page.GetValidators(this.ValidationGroup);
			for (int i = 0; i < validators.Count; i++)
			{
				IValidator validator = validators[i];
				if (!validator.IsValid)
				{
					inError = true;
					if (validator.ErrorMessage.Length != 0)
					{
						num++;
					}
				}
			}
			if (num != 0)
			{
				array = new string[num];
				int num2 = 0;
				for (int j = 0; j < validators.Count; j++)
				{
					IValidator validator2 = validators[j];
					if (!validator2.IsValid && validator2.ErrorMessage != null && validator2.ErrorMessage.Length != 0)
					{
						array[num2] = string.Copy(validator2.ErrorMessage);
						num2++;
					}
				}
			}
			return array;
		}

		// Token: 0x060051D1 RID: 20945 RVA: 0x0014AC80 File Offset: 0x00149C80
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
				this.renderUplevel = (this.EnableClientScript && page.Request.Browser.W3CDomVersion.Major >= 1 && page.Request.Browser.EcmaScriptVersion.CompareTo(new Version(1, 2)) >= 0);
			}
			if (this.renderUplevel)
			{
				string arrayValue = "document.getElementById(\"" + this.ClientID + "\")";
				if (!this.Page.IsPartialRenderingSupported)
				{
					this.Page.ClientScript.RegisterArrayDeclaration("Page_ValidationSummaries", arrayValue);
					return;
				}
				ValidatorCompatibilityHelper.RegisterArrayDeclaration(this, "Page_ValidationSummaries", arrayValue);
				ValidatorCompatibilityHelper.RegisterStartupScript(this, typeof(ValidationSummary), this.ClientID + "_DisposeScript", string.Format(CultureInfo.InvariantCulture, "\r\ndocument.getElementById('{0}').dispose = function() {{\r\n    Array.remove({1}, document.getElementById('{0}'));\r\n}}\r\n", new object[]
				{
					this.ClientID,
					"Page_ValidationSummaries"
				}), true);
			}
		}

		// Token: 0x060051D2 RID: 20946 RVA: 0x0014AD94 File Offset: 0x00149D94
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
					goto IL_134;
				case ValidationSummaryDisplayMode.SingleParagraph:
					text = " ";
					value = string.Empty;
					value2 = string.Empty;
					text2 = " ";
					text3 = "b";
					goto IL_134;
				}
				text = string.Empty;
				value = "<ul>";
				value2 = "<li>";
				text2 = "</li>";
				text3 = "</ul>";
				IL_134:
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

		// Token: 0x060051D3 RID: 20947 RVA: 0x0014AF43 File Offset: 0x00149F43
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

		// Token: 0x04002DC2 RID: 11714
		private const string breakTag = "b";

		// Token: 0x04002DC3 RID: 11715
		private bool renderUplevel;
	}
}
