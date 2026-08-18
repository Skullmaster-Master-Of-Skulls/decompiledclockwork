using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace System.Web.WebPages
{
	// Token: 0x02000059 RID: 89
	public interface IValidator
	{
		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000221 RID: 545
		ModelClientValidationRule ClientValidationRule { get; }

		// Token: 0x06000222 RID: 546
		ValidationResult Validate(ValidationContext validationContext);
	}
}
