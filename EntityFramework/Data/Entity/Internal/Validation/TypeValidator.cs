using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace System.Data.Entity.Internal.Validation
{
	// Token: 0x0200079D RID: 1949
	internal abstract class TypeValidator
	{
		// Token: 0x0600582C RID: 22572 RVA: 0x0017B432 File Offset: 0x00179632
		public TypeValidator(IEnumerable<PropertyValidator> propertyValidators, IEnumerable<IValidator> typeLevelValidators)
		{
			this._typeLevelValidators = typeLevelValidators;
			this._propertyValidators = propertyValidators;
		}

		// Token: 0x17000F70 RID: 3952
		// (get) Token: 0x0600582D RID: 22573 RVA: 0x0017B448 File Offset: 0x00179648
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Used by test code.")]
		public IEnumerable<IValidator> TypeLevelValidators
		{
			get
			{
				return this._typeLevelValidators;
			}
		}

		// Token: 0x17000F71 RID: 3953
		// (get) Token: 0x0600582E RID: 22574 RVA: 0x0017B450 File Offset: 0x00179650
		public IEnumerable<PropertyValidator> PropertyValidators
		{
			get
			{
				return this._propertyValidators;
			}
		}

		// Token: 0x0600582F RID: 22575 RVA: 0x0017B458 File Offset: 0x00179658
		protected IEnumerable<DbValidationError> Validate(EntityValidationContext entityValidationContext, InternalPropertyEntry property)
		{
			List<DbValidationError> list = new List<DbValidationError>();
			this.ValidateProperties(entityValidationContext, property, list);
			if (!list.Any<DbValidationError>())
			{
				foreach (IValidator validator in this._typeLevelValidators)
				{
					list.AddRange(validator.Validate(entityValidationContext, property));
				}
			}
			return list;
		}

		// Token: 0x06005830 RID: 22576
		protected abstract void ValidateProperties(EntityValidationContext entityValidationContext, InternalPropertyEntry parentProperty, List<DbValidationError> validationErrors);

		// Token: 0x06005831 RID: 22577 RVA: 0x0017B4E0 File Offset: 0x001796E0
		public PropertyValidator GetPropertyValidator(string name)
		{
			return this._propertyValidators.SingleOrDefault((PropertyValidator v) => v.PropertyName == name);
		}

		// Token: 0x04002366 RID: 9062
		private readonly IEnumerable<IValidator> _typeLevelValidators;

		// Token: 0x04002367 RID: 9063
		private readonly IEnumerable<PropertyValidator> _propertyValidators;
	}
}
