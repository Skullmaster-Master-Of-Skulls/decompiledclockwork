using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace System.Web.Http.Validation.Providers
{
	// Token: 0x0200018C RID: 396
	// (Invoke) Token: 0x06000A33 RID: 2611
	public delegate ModelValidator DataAnnotationsModelValidationFactory(IEnumerable<ModelValidatorProvider> validatorProviders, ValidationAttribute attribute);
}
