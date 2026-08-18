using System;

namespace System.Data.Objects.Internal
{
	// Token: 0x0200016D RID: 365
	internal sealed class PocoEntityKeyStrategy : IEntityKeyStrategy
	{
		// Token: 0x06001AF0 RID: 6896 RVA: 0x0005BE72 File Offset: 0x0005A072
		public EntityKey GetEntityKey()
		{
			return this._key;
		}

		// Token: 0x06001AF1 RID: 6897 RVA: 0x0005BE7A File Offset: 0x0005A07A
		public void SetEntityKey(EntityKey key)
		{
			this._key = key;
		}

		// Token: 0x06001AF2 RID: 6898 RVA: 0x00006174 File Offset: 0x00004374
		public EntityKey GetEntityKeyFromEntity()
		{
			return null;
		}

		// Token: 0x04000B33 RID: 2867
		private EntityKey _key;
	}
}
