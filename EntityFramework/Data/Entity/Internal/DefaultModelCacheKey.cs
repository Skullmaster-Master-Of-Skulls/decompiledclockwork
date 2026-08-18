using System;
using System.Data.Entity.Infrastructure;

namespace System.Data.Entity.Internal
{
	// Token: 0x020002A4 RID: 676
	internal sealed class DefaultModelCacheKey : IDbModelCacheKey
	{
		// Token: 0x060017F1 RID: 6129 RVA: 0x00078F3B File Offset: 0x0007713B
		public DefaultModelCacheKey(Type contextType, string providerName, Type providerType, string customKey)
		{
			this._contextType = contextType;
			this._providerName = providerName;
			this._providerType = providerType;
			this._customKey = customKey;
		}

		// Token: 0x060017F2 RID: 6130 RVA: 0x00078F60 File Offset: 0x00077160
		public override bool Equals(object obj)
		{
			if (object.ReferenceEquals(null, obj))
			{
				return false;
			}
			if (object.ReferenceEquals(this, obj))
			{
				return true;
			}
			DefaultModelCacheKey defaultModelCacheKey = obj as DefaultModelCacheKey;
			return defaultModelCacheKey != null && this.Equals(defaultModelCacheKey);
		}

		// Token: 0x060017F3 RID: 6131 RVA: 0x00078F98 File Offset: 0x00077198
		public override int GetHashCode()
		{
			return this._contextType.GetHashCode() * 397 ^ this._providerName.GetHashCode() ^ this._providerType.GetHashCode() ^ ((!string.IsNullOrWhiteSpace(this._customKey)) ? this._customKey.GetHashCode() : 0);
		}

		// Token: 0x060017F4 RID: 6132 RVA: 0x00078FEC File Offset: 0x000771EC
		private bool Equals(DefaultModelCacheKey other)
		{
			return this._contextType == other._contextType && string.Equals(this._providerName, other._providerName) && object.Equals(this._providerType, other._providerType) && string.Equals(this._customKey, other._customKey);
		}

		// Token: 0x0400085C RID: 2140
		private readonly Type _contextType;

		// Token: 0x0400085D RID: 2141
		private readonly string _providerName;

		// Token: 0x0400085E RID: 2142
		private readonly Type _providerType;

		// Token: 0x0400085F RID: 2143
		private readonly string _customKey;
	}
}
