using System;
using System.ComponentModel;

namespace System.Web.UI.WebControls
{
	// Token: 0x020003AC RID: 940
	[DefaultEvent("ServerValidate")]
	[ToolboxData("<{0}:CustomValidator runat=\"server\" ErrorMessage=\"CustomValidator\"></{0}:CustomValidator>")]
	public class CustomValidator : BaseValidator
	{
		// Token: 0x17000CF3 RID: 3315
		// (get) Token: 0x06002D5E RID: 11614 RVA: 0x00094664 File Offset: 0x00092864
		// (set) Token: 0x06002D5F RID: 11615 RVA: 0x00094691 File Offset: 0x00092891
		[WebCategory("Behavior")]
		[Themeable(false)]
		[DefaultValue("")]
		[WebSysDescription("CustomValidator_ClientValidationFunction")]
		public string ClientValidationFunction
		{
			get
			{
				object obj = this.ViewState["ClientValidationFunction"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["ClientValidationFunction"] = value;
			}
		}

		// Token: 0x17000CF4 RID: 3316
		// (get) Token: 0x06002D60 RID: 11616 RVA: 0x000946A4 File Offset: 0x000928A4
		// (set) Token: 0x06002D61 RID: 11617 RVA: 0x000946CD File Offset: 0x000928CD
		[WebCategory("Behavior")]
		[Themeable(false)]
		[DefaultValue(false)]
		[WebSysDescription("CustomValidator_ValidateEmptyText")]
		public bool ValidateEmptyText
		{
			get
			{
				object obj = this.ViewState["ValidateEmptyText"];
				return obj != null && (bool)obj;
			}
			set
			{
				this.ViewState["ValidateEmptyText"] = value;
			}
		}

		// Token: 0x14000067 RID: 103
		// (add) Token: 0x06002D62 RID: 11618 RVA: 0x000946E5 File Offset: 0x000928E5
		// (remove) Token: 0x06002D63 RID: 11619 RVA: 0x000946F8 File Offset: 0x000928F8
		[WebSysDescription("CustomValidator_ServerValidate")]
		public event ServerValidateEventHandler ServerValidate
		{
			add
			{
				base.Events.AddHandler(CustomValidator.EventServerValidate, value);
			}
			remove
			{
				base.Events.RemoveHandler(CustomValidator.EventServerValidate, value);
			}
		}

		// Token: 0x06002D64 RID: 11620 RVA: 0x0009470C File Offset: 0x0009290C
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			base.AddAttributesToRender(writer);
			if (base.RenderUplevel)
			{
				string clientID = this.ClientID;
				HtmlTextWriter writer2 = (base.EnableLegacyRendering || base.IsUnobtrusive) ? writer : null;
				base.AddExpandoAttribute(writer2, clientID, "evaluationfunction", "CustomValidatorEvaluateIsValid", false);
				if (this.ClientValidationFunction.Length > 0)
				{
					base.AddExpandoAttribute(writer2, clientID, "clientvalidationfunction", this.ClientValidationFunction);
					if (this.ValidateEmptyText)
					{
						base.AddExpandoAttribute(writer2, clientID, "validateemptytext", "true", false);
					}
				}
			}
		}

		// Token: 0x06002D65 RID: 11621 RVA: 0x00094794 File Offset: 0x00092994
		protected override bool ControlPropertiesValid()
		{
			string controlToValidate = base.ControlToValidate;
			if (controlToValidate.Length > 0)
			{
				base.CheckControlValidationProperty(controlToValidate, "ControlToValidate");
			}
			return true;
		}

		// Token: 0x06002D66 RID: 11622 RVA: 0x000947C0 File Offset: 0x000929C0
		protected override bool EvaluateIsValid()
		{
			string text = string.Empty;
			string controlToValidate = base.ControlToValidate;
			if (controlToValidate.Length > 0)
			{
				text = base.GetControlValidationValue(controlToValidate);
				if ((text == null || text.Trim().Length == 0) && !this.ValidateEmptyText)
				{
					return true;
				}
			}
			return this.OnServerValidate(text);
		}

		// Token: 0x06002D67 RID: 11623 RVA: 0x0009480C File Offset: 0x00092A0C
		protected virtual bool OnServerValidate(string value)
		{
			ServerValidateEventHandler serverValidateEventHandler = (ServerValidateEventHandler)base.Events[CustomValidator.EventServerValidate];
			ServerValidateEventArgs serverValidateEventArgs = new ServerValidateEventArgs(value, true);
			if (serverValidateEventHandler != null)
			{
				serverValidateEventHandler(this, serverValidateEventArgs);
				return serverValidateEventArgs.IsValid;
			}
			return true;
		}

		// Token: 0x04001F82 RID: 8066
		private static readonly object EventServerValidate = new object();
	}
}
