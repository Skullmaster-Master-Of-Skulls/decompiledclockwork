using System;

namespace System.Data.Objects.Internal
{
	// Token: 0x02000169 RID: 361
	internal interface IEntityKeyStrategy
	{
		// Token: 0x06001ACD RID: 6861
		EntityKey GetEntityKey();

		// Token: 0x06001ACE RID: 6862
		void SetEntityKey(EntityKey key);

		// Token: 0x06001ACF RID: 6863
		EntityKey GetEntityKeyFromEntity();
	}
}
