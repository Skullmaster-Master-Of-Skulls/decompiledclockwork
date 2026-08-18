using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;

namespace System.Data.Entity.Internal.Validation
{
	// Token: 0x0200079C RID: 1948
	internal class ComplexPropertyValidator : PropertyValidator
	{
		// Token: 0x17000F6F RID: 3951
		// (get) Token: 0x06005829 RID: 22569 RVA: 0x0017B3C4 File Offset: 0x001795C4
		public ComplexTypeValidator ComplexTypeValidator
		{
			get
			{
				return this._complexTypeValidator;
			}
		}

		// Token: 0x0600582A RID: 22570 RVA: 0x0017B3CC File Offset: 0x001795CC
		public ComplexPropertyValidator(string propertyName, IEnumerable<IValidator> propertyValidators, ComplexTypeValidator complexTypeValidator) : base(propertyName, propertyValidators)
		{
			this._complexTypeValidator = complexTypeValidator;
		}

		// Token: 0x0600582B RID: 22571 RVA: 0x0017B3E0 File Offset: 0x001795E0
		public override IEnumerable<DbValidationError> Validate(EntityValidationContext entityValidationContext, InternalMemberEntry property)
		{
			List<DbValidationError> list = new List<DbValidationError>();
			list.AddRange(base.Validate(entityValidationContext, property));
			if (!list.Any<DbValidationError>() && property.CurrentValue != null && this._complexTypeValidator != null)
			{
				list.AddRange(this._complexTypeValidator.Validate(entityValidationContext, (InternalPropertyEntry)property));
			}
			return list;
		}

		// Token: 0x04002365 RID: 9061
		private readonly ComplexTypeValidator _complexTypeValidator;
	}
}
