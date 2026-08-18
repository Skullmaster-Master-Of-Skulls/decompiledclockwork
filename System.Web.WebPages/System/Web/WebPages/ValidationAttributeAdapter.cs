using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace System.Web.WebPages
{
	// Token: 0x0200005E RID: 94
	internal class ValidationAttributeAdapter : RequestFieldValidatorBase
	{
		// Token: 0x06000231 RID: 561 RVA: 0x00008E2F File Offset: 0x0000702F
		public ValidationAttributeAdapter(ValidationAttribute attribute, string errorMessage, ModelClientValidationRule clientValidationRule) : this(attribute, errorMessage, clientValidationRule, false)
		{
		}

		// Token: 0x06000232 RID: 562 RVA: 0x00008E3B File Offset: 0x0000703B
		public ValidationAttributeAdapter(ValidationAttribute attribute, string errorMessage, ModelClientValidationRule clientValidationRule, bool useUnvalidatedValues) : base(errorMessage, useUnvalidatedValues)
		{
			this._attribute = attribute;
			this._clientValidationRule = clientValidationRule;
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000233 RID: 563 RVA: 0x00008E54 File Offset: 0x00007054
		public ValidationAttribute Attribute
		{
			get
			{
				return this._attribute;
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000234 RID: 564 RVA: 0x00008E5C File Offset: 0x0000705C
		public override ModelClientValidationRule ClientValidationRule
		{
			get
			{
				return this._clientValidationRule;
			}
		}

		// Token: 0x06000235 RID: 565 RVA: 0x00008E64 File Offset: 0x00007064
		protected override bool IsValid(HttpContextBase httpContext, string value)
		{
			return this._attribute.IsValid(value);
		}

		// Token: 0x040000BF RID: 191
		private readonly ValidationAttribute _attribute;

		// Token: 0x040000C0 RID: 192
		private readonly ModelClientValidationRule _clientValidationRule;
	}
}
