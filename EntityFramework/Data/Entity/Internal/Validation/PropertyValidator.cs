using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics.CodeAnalysis;

namespace System.Data.Entity.Internal.Validation
{
	// Token: 0x0200079B RID: 1947
	internal class PropertyValidator
	{
		// Token: 0x06005825 RID: 22565 RVA: 0x0017B340 File Offset: 0x00179540
		public PropertyValidator(string propertyName, IEnumerable<IValidator> propertyValidators)
		{
			this._propertyValidators = propertyValidators;
			this._propertyName = propertyName;
		}

		// Token: 0x17000F6D RID: 3949
		// (get) Token: 0x06005826 RID: 22566 RVA: 0x0017B356 File Offset: 0x00179556
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Used by test code.")]
		public IEnumerable<IValidator> PropertyAttributeValidators
		{
			get
			{
				return this._propertyValidators;
			}
		}

		// Token: 0x17000F6E RID: 3950
		// (get) Token: 0x06005827 RID: 22567 RVA: 0x0017B35E File Offset: 0x0017955E
		public string PropertyName
		{
			get
			{
				return this._propertyName;
			}
		}

		// Token: 0x06005828 RID: 22568 RVA: 0x0017B368 File Offset: 0x00179568
		public virtual IEnumerable<DbValidationError> Validate(EntityValidationContext entityValidationContext, InternalMemberEntry property)
		{
			List<DbValidationError> list = new List<DbValidationError>();
			foreach (IValidator validator in this._propertyValidators)
			{
				list.AddRange(validator.Validate(entityValidationContext, property));
			}
			return list;
		}

		// Token: 0x04002363 RID: 9059
		private readonly IEnumerable<IValidator> _propertyValidators;

		// Token: 0x04002364 RID: 9060
		private readonly string _propertyName;
	}
}
