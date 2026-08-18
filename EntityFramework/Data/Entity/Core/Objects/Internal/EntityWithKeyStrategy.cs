using System;
using System.Data.Entity.Core.Objects.DataClasses;

namespace System.Data.Entity.Core.Objects.Internal
{
	// Token: 0x02000583 RID: 1411
	internal sealed class EntityWithKeyStrategy : IEntityKeyStrategy
	{
		// Token: 0x06003719 RID: 14105 RVA: 0x00105A6C File Offset: 0x00103C6C
		public EntityWithKeyStrategy(IEntityWithKey entity)
		{
			this._entity = entity;
		}

		// Token: 0x0600371A RID: 14106 RVA: 0x00105A7B File Offset: 0x00103C7B
		public EntityKey GetEntityKey()
		{
			return this._entity.EntityKey;
		}

		// Token: 0x0600371B RID: 14107 RVA: 0x00105A88 File Offset: 0x00103C88
		public void SetEntityKey(EntityKey key)
		{
			this._entity.EntityKey = key;
		}

		// Token: 0x0600371C RID: 14108 RVA: 0x00105A96 File Offset: 0x00103C96
		public EntityKey GetEntityKeyFromEntity()
		{
			return this._entity.EntityKey;
		}

		// Token: 0x04001530 RID: 5424
		private readonly IEntityWithKey _entity;
	}
}
