using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;

namespace System.Data.Entity.Internal.Validation
{
	// Token: 0x020007A0 RID: 1952
	internal class EntityValidator : TypeValidator
	{
		// Token: 0x06005839 RID: 22585 RVA: 0x0017B5BB File Offset: 0x001797BB
		public EntityValidator(IEnumerable<PropertyValidator> propertyValidators, IEnumerable<IValidator> typeLevelValidators) : base(propertyValidators, typeLevelValidators)
		{
		}

		// Token: 0x0600583A RID: 22586 RVA: 0x0017B5C8 File Offset: 0x001797C8
		public DbEntityValidationResult Validate(EntityValidationContext entityValidationContext)
		{
			IEnumerable<DbValidationError> validationErrors = base.Validate(entityValidationContext, null);
			return new DbEntityValidationResult(entityValidationContext.InternalEntity, validationErrors);
		}

		// Token: 0x0600583B RID: 22587 RVA: 0x0017B5EC File Offset: 0x001797EC
		protected override void ValidateProperties(EntityValidationContext entityValidationContext, InternalPropertyEntry parentProperty, List<DbValidationError> validationErrors)
		{
			InternalEntityEntry internalEntity = entityValidationContext.InternalEntity;
			foreach (PropertyValidator propertyValidator in base.PropertyValidators)
			{
				validationErrors.AddRange(propertyValidator.Validate(entityValidationContext, internalEntity.Member(propertyValidator.PropertyName, null)));
			}
		}
	}
}
