using System;
using System.Web.Mvc;

namespace System.Web.WebPages
{
	// Token: 0x0200005B RID: 91
	internal class CompareValidator : RequestFieldValidatorBase
	{
		// Token: 0x0600022C RID: 556 RVA: 0x00008D70 File Offset: 0x00006F70
		public CompareValidator(string otherField, string errorMessage) : base(errorMessage)
		{
			this._otherField = otherField;
			this._clientValidationRule = new ModelClientValidationEqualToRule(errorMessage, otherField);
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x0600022D RID: 557 RVA: 0x00008D8D File Offset: 0x00006F8D
		public override ModelClientValidationRule ClientValidationRule
		{
			get
			{
				return this._clientValidationRule;
			}
		}

		// Token: 0x0600022E RID: 558 RVA: 0x00008D98 File Offset: 0x00006F98
		protected override bool IsValid(HttpContextBase httpContext, string value)
		{
			string requestValue = base.GetRequestValue(httpContext.Request, this._otherField);
			return string.Equals(value, requestValue, StringComparison.CurrentCulture);
		}

		// Token: 0x040000B6 RID: 182
		private readonly string _otherField;

		// Token: 0x040000B7 RID: 183
		private readonly ModelClientValidationEqualToRule _clientValidationRule;
	}
}
