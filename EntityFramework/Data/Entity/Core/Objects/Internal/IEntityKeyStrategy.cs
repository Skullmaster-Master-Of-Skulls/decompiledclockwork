using System;

namespace System.Data.Entity.Core.Objects.Internal
{
	// Token: 0x02000582 RID: 1410
	internal interface IEntityKeyStrategy
	{
		// Token: 0x06003716 RID: 14102
		EntityKey GetEntityKey();

		// Token: 0x06003717 RID: 14103
		void SetEntityKey(EntityKey key);

		// Token: 0x06003718 RID: 14104
		EntityKey GetEntityKeyFromEntity();
	}
}
