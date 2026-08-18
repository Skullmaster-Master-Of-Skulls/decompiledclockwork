using System;
using System.Data.Objects.DataClasses;

namespace System.Data.Objects.Internal
{
	// Token: 0x02000167 RID: 359
	internal sealed class EntityWithKeyStrategy : IEntityKeyStrategy
	{
		// Token: 0x06001AC5 RID: 6853 RVA: 0x0005BBC0 File Offset: 0x00059DC0
		public EntityWithKeyStrategy(IEntityWithKey entity)
		{
			this._entity = entity;
		}

		// Token: 0x06001AC6 RID: 6854 RVA: 0x0005BBCF File Offset: 0x00059DCF
		public EntityKey GetEntityKey()
		{
			return this._entity.EntityKey;
		}

		// Token: 0x06001AC7 RID: 6855 RVA: 0x0005BBDC File Offset: 0x00059DDC
		public void SetEntityKey(EntityKey key)
		{
			this._entity.EntityKey = key;
		}

		// Token: 0x06001AC8 RID: 6856 RVA: 0x0005BBCF File Offset: 0x00059DCF
		public EntityKey GetEntityKeyFromEntity()
		{
			return this._entity.EntityKey;
		}

		// Token: 0x04000B2F RID: 2863
		private IEntityWithKey _entity;
	}
}
