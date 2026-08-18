using System;
using Validation;

namespace System.Collections.Immutable
{
	// Token: 0x0200003C RID: 60
	internal class SecureObjectPool<T, TCaller> where TCaller : ISecurePooledObjectUser
	{
		// Token: 0x06000374 RID: 884 RVA: 0x000094D5 File Offset: 0x000076D5
		public void TryAdd(TCaller caller, SecurePooledObject<T> item)
		{
			if (caller.PoolUserId == item.Owner)
			{
				item.Owner = -1;
				AllocFreeConcurrentStack<SecurePooledObject<T>>.TryAdd(item);
			}
		}

		// Token: 0x06000375 RID: 885 RVA: 0x000094F9 File Offset: 0x000076F9
		public bool TryTake(TCaller caller, out SecurePooledObject<T> item)
		{
			if (caller.PoolUserId != -1 && AllocFreeConcurrentStack<SecurePooledObject<T>>.TryTake(out item))
			{
				item.Owner = caller.PoolUserId;
				return true;
			}
			item = null;
			return false;
		}

		// Token: 0x06000376 RID: 886 RVA: 0x0000952D File Offset: 0x0000772D
		public SecurePooledObject<T> PrepNew(TCaller caller, T newValue)
		{
			Requires.NotNullAllowStructs<T>(newValue, "newValue");
			return new SecurePooledObject<T>(newValue)
			{
				Owner = caller.PoolUserId
			};
		}
	}
}
