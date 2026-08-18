using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;

namespace System.Data.Entity.Internal.Validation
{
	// Token: 0x0200079E RID: 1950
	internal class ComplexTypeValidator : TypeValidator
	{
		// Token: 0x06005832 RID: 22578 RVA: 0x0017B511 File Offset: 0x00179711
		public ComplexTypeValidator(IEnumerable<PropertyValidator> propertyValidators, IEnumerable<IValidator> typeLevelValidators) : base(propertyValidators, typeLevelValidators)
		{
		}

		// Token: 0x06005833 RID: 22579 RVA: 0x0017B51B File Offset: 0x0017971B
		public new IEnumerable<DbValidationError> Validate(EntityValidationContext entityValidationContext, InternalPropertyEntry property)
		{
			return base.Validate(entityValidationContext, property);
		}

		// Token: 0x06005834 RID: 22580 RVA: 0x0017B528 File Offset: 0x00179728
		protected override void ValidateProperties(EntityValidationContext entityValidationContext, InternalPropertyEntry parentProperty, List<DbValidationError> validationErrors)
		{
			foreach (PropertyValidator propertyValidator in base.PropertyValidators)
			{
				InternalPropertyEntry property = parentProperty.Property(propertyValidator.PropertyName, null, false);
				validationErrors.AddRange(propertyValidator.Validate(entityValidationContext, property));
			}
		}
	}
}
