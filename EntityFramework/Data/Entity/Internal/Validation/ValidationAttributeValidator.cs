using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Data.Entity.Validation;
using System.Linq;

namespace System.Data.Entity.Internal.Validation
{
	// Token: 0x020007A4 RID: 1956
	internal class ValidationAttributeValidator : IValidator
	{
		// Token: 0x06005855 RID: 22613 RVA: 0x0017C000 File Offset: 0x0017A200
		public ValidationAttributeValidator(ValidationAttribute validationAttribute, DisplayAttribute displayAttribute)
		{
			this._validationAttribute = validationAttribute;
			this._displayAttribute = displayAttribute;
		}

		// Token: 0x06005856 RID: 22614 RVA: 0x0017C018 File Offset: 0x0017A218
		public virtual IEnumerable<DbValidationError> Validate(EntityValidationContext entityValidationContext, InternalMemberEntry property)
		{
			ValidationContext externalValidationContext = entityValidationContext.ExternalValidationContext;
			externalValidationContext.SetDisplayName(property, this._displayAttribute);
			object value = (property == null) ? entityValidationContext.InternalEntity.Entity : property.CurrentValue;
			ValidationResult validationResult = null;
			try
			{
				validationResult = this._validationAttribute.GetValidationResult(value, externalValidationContext);
			}
			catch (Exception innerException)
			{
				throw new DbUnexpectedValidationException(Strings.DbUnexpectedValidationException_ValidationAttribute(externalValidationContext.DisplayName, this._validationAttribute.GetType()), innerException);
			}
			if (validationResult == ValidationResult.Success)
			{
				return Enumerable.Empty<DbValidationError>();
			}
			return DbHelpers.SplitValidationResults(externalValidationContext.MemberName, new ValidationResult[]
			{
				validationResult
			});
		}

		// Token: 0x04002378 RID: 9080
		private readonly DisplayAttribute _displayAttribute;

		// Token: 0x04002379 RID: 9081
		private readonly ValidationAttribute _validationAttribute;
	}
}
