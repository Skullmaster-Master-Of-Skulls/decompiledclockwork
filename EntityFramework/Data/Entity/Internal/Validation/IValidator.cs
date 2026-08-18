using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;

namespace System.Data.Entity.Internal.Validation
{
	// Token: 0x020007A2 RID: 1954
	internal interface IValidator
	{
		// Token: 0x06005852 RID: 22610
		IEnumerable<DbValidationError> Validate(EntityValidationContext entityValidationContext, InternalMemberEntry property);
	}
}
