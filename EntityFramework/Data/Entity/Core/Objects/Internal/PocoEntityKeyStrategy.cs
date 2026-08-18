using System;

namespace System.Data.Entity.Core.Objects.Internal
{
	// Token: 0x02000596 RID: 1430
	internal sealed class PocoEntityKeyStrategy : IEntityKeyStrategy
	{
		// Token: 0x060037DE RID: 14302 RVA: 0x001092EC File Offset: 0x001074EC
		public EntityKey GetEntityKey()
		{
			return this._key;
		}

		// Token: 0x060037DF RID: 14303 RVA: 0x001092F4 File Offset: 0x001074F4
		public void SetEntityKey(EntityKey key)
		{
			this._key = key;
		}

		// Token: 0x060037E0 RID: 14304 RVA: 0x001092FD File Offset: 0x001074FD
		public EntityKey GetEntityKeyFromEntity()
		{
			return null;
		}

		// Token: 0x0400157B RID: 5499
		private EntityKey _key;
	}
}
