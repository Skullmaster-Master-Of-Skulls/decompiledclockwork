using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration.Utilities;

namespace System.Data.Entity.Internal.Validation
{
	// Token: 0x020007A5 RID: 1957
	internal class ValidationProvider
	{
		// Token: 0x06005857 RID: 22615 RVA: 0x0017C0B8 File Offset: 0x0017A2B8
		public ValidationProvider(EntityValidatorBuilder builder = null, AttributeProvider attributeProvider = null)
		{
			this._entityValidators = new Dictionary<Type, EntityValidator>();
			this._entityValidatorBuilder = (builder ?? new EntityValidatorBuilder(attributeProvider ?? new AttributeProvider()));
		}

		// Token: 0x06005858 RID: 22616 RVA: 0x0017C0E8 File Offset: 0x0017A2E8
		public virtual EntityValidator GetEntityValidator(InternalEntityEntry entityEntry)
		{
			Type entityType = entityEntry.EntityType;
			EntityValidator entityValidator = null;
			if (this._entityValidators.TryGetValue(entityType, out entityValidator))
			{
				return entityValidator;
			}
			entityValidator = this._entityValidatorBuilder.BuildEntityValidator(entityEntry);
			this._entityValidators[entityType] = entityValidator;
			return entityValidator;
		}

		// Token: 0x06005859 RID: 22617 RVA: 0x0017C12C File Offset: 0x0017A32C
		public virtual PropertyValidator GetPropertyValidator(InternalEntityEntry owningEntity, InternalMemberEntry property)
		{
			EntityValidator entityValidator = this.GetEntityValidator(owningEntity);
			if (entityValidator == null)
			{
				return null;
			}
			return this.GetValidatorForProperty(entityValidator, property);
		}

		// Token: 0x0600585A RID: 22618 RVA: 0x0017C150 File Offset: 0x0017A350
		protected virtual PropertyValidator GetValidatorForProperty(EntityValidator entityValidator, InternalMemberEntry memberEntry)
		{
			InternalNestedPropertyEntry internalNestedPropertyEntry = memberEntry as InternalNestedPropertyEntry;
			if (internalNestedPropertyEntry == null)
			{
				return entityValidator.GetPropertyValidator(memberEntry.Name);
			}
			ComplexPropertyValidator complexPropertyValidator = this.GetValidatorForProperty(entityValidator, internalNestedPropertyEntry.ParentPropertyEntry) as ComplexPropertyValidator;
			if (complexPropertyValidator == null || complexPropertyValidator.ComplexTypeValidator == null)
			{
				return null;
			}
			return complexPropertyValidator.ComplexTypeValidator.GetPropertyValidator(memberEntry.Name);
		}

		// Token: 0x0600585B RID: 22619 RVA: 0x0017C1A5 File Offset: 0x0017A3A5
		public virtual EntityValidationContext GetEntityValidationContext(InternalEntityEntry entityEntry, IDictionary<object, object> items)
		{
			return new EntityValidationContext(entityEntry, new ValidationContext(entityEntry.Entity, null, items));
		}

		// Token: 0x0400237A RID: 9082
		private readonly Dictionary<Type, EntityValidator> _entityValidators;

		// Token: 0x0400237B RID: 9083
		private readonly EntityValidatorBuilder _entityValidatorBuilder;
	}
}
