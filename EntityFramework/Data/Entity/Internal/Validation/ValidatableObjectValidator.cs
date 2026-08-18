using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Data.Entity.Validation;
using System.Linq;

namespace System.Data.Entity.Internal.Validation
{
	// Token: 0x020007A3 RID: 1955
	internal class ValidatableObjectValidator : IValidator
	{
		// Token: 0x06005853 RID: 22611 RVA: 0x0017BF51 File Offset: 0x0017A151
		public ValidatableObjectValidator(DisplayAttribute displayAttribute)
		{
			this._displayAttribute = displayAttribute;
		}

		// Token: 0x06005854 RID: 22612 RVA: 0x0017BF60 File Offset: 0x0017A160
		public virtual IEnumerable<DbValidationError> Validate(EntityValidationContext entityValidationContext, InternalMemberEntry property)
		{
			if (property != null && property.CurrentValue == null)
			{
				return Enumerable.Empty<DbValidationError>();
			}
			ValidationContext externalValidationContext = entityValidationContext.ExternalValidationContext;
			externalValidationContext.SetDisplayName(property, this._displayAttribute);
			IValidatableObject validatableObject = (IValidatableObject)((property == null) ? entityValidationContext.InternalEntity.Entity : property.CurrentValue);
			IEnumerable<ValidationResult> enumerable = null;
			try
			{
				enumerable = validatableObject.Validate(externalValidationContext);
			}
			catch (Exception innerException)
			{
				throw new DbUnexpectedValidationException(Strings.DbUnexpectedValidationException_IValidatableObject(externalValidationContext.DisplayName, ObjectContextTypeCache.GetObjectType(validatableObject.GetType())), innerException);
			}
			return DbHelpers.SplitValidationResults(externalValidationContext.MemberName, enumerable ?? Enumerable.Empty<ValidationResult>());
		}

		// Token: 0x04002377 RID: 9079
		private readonly DisplayAttribute _displayAttribute;
	}
}
