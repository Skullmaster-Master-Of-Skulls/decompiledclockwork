using System;
using System.Runtime.CompilerServices;
using Validation;

namespace System.Collections.Immutable
{
	// Token: 0x0200003E RID: 62
	internal class SecurePooledObject<T>
	{
		// Token: 0x06000379 RID: 889 RVA: 0x0000955B File Offset: 0x0000775B
		internal SecurePooledObject(T newValue)
		{
			Requires.NotNullAllowStructs<T>(newValue, "newValue");
			this._value = newValue;
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x0600037A RID: 890 RVA: 0x00009575 File Offset: 0x00007775
		// (set) Token: 0x0600037B RID: 891 RVA: 0x0000957D File Offset: 0x0000777D
		internal int Owner
		{
			get
			{
				return this._owner;
			}
			set
			{
				this._owner = value;
			}
		}

		// Token: 0x0600037C RID: 892 RVA: 0x00009586 File Offset: 0x00007786
		internal T Use<TCaller>(ref TCaller caller) where TCaller : struct, ISecurePooledObjectUser
		{
			if (!this.IsOwned<TCaller>(ref caller))
			{
				Requires.FailObjectDisposed<TCaller>(caller);
			}
			return this._value;
		}

		// Token: 0x0600037D RID: 893 RVA: 0x000095A2 File Offset: 0x000077A2
		internal bool TryUse<TCaller>(ref TCaller caller, out T value) where TCaller : struct, ISecurePooledObjectUser
		{
			if (this.IsOwned<TCaller>(ref caller))
			{
				value = this._value;
				return true;
			}
			value = default(T);
			return false;
		}

		// Token: 0x0600037E RID: 894 RVA: 0x000095C3 File Offset: 0x000077C3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal bool IsOwned<TCaller>(ref TCaller caller) where TCaller : struct, ISecurePooledObjectUser
		{
			return caller.PoolUserId == this._owner;
		}

		// Token: 0x0400004A RID: 74
		private readonly T _value;

		// Token: 0x0400004B RID: 75
		private int _owner;
	}
}
