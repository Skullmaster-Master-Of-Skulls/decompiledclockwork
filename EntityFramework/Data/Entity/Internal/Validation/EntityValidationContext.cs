using System;
using System.ComponentModel.DataAnnotations;

namespace System.Data.Entity.Internal.Validation
{
	// Token: 0x0200079F RID: 1951
	internal class EntityValidationContext
	{
		// Token: 0x06005835 RID: 22581 RVA: 0x0017B58C File Offset: 0x0017978C
		public EntityValidationContext(InternalEntityEntry entityEntry, ValidationContext externalValidationContext)
		{
			this._entityEntry = entityEntry;
			this.ExternalValidationContext = externalValidationContext;
		}

		// Token: 0x17000F72 RID: 3954
		// (get) Token: 0x06005836 RID: 22582 RVA: 0x0017B5A2 File Offset: 0x001797A2
		// (set) Token: 0x06005837 RID: 22583 RVA: 0x0017B5AA File Offset: 0x001797AA
		public ValidationContext ExternalValidationContext { get; private set; }

		// Token: 0x17000F73 RID: 3955
		// (get) Token: 0x06005838 RID: 22584 RVA: 0x0017B5B3 File Offset: 0x001797B3
		public InternalEntityEntry InternalEntity
		{
			get
			{
				return this._entityEntry;
			}
		}

		// Token: 0x04002368 RID: 9064
		private readonly InternalEntityEntry _entityEntry;
	}
}
