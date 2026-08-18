using System;
using System.Collections.Generic;
using System.IdentityModel.Configuration;
using System.Xml;

namespace System.IdentityModel.Tokens
{
	// Token: 0x02000180 RID: 384
	public abstract class SessionSecurityTokenCache : ICustomIdentityConfiguration
	{
		// Token: 0x06000C72 RID: 3186 RVA: 0x0000443A File Offset: 0x0000263A
		public virtual void LoadCustomConfiguration(XmlNodeList nodelist)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotImplementedException(SR.GetString("ID0023", new object[]
			{
				base.GetType().AssemblyQualifiedName
			})));
		}

		// Token: 0x06000C73 RID: 3187
		public abstract void AddOrUpdate(SessionSecurityTokenCacheKey key, SessionSecurityToken value, DateTime expiryTime);

		// Token: 0x06000C74 RID: 3188
		public abstract IEnumerable<SessionSecurityToken> GetAll(string endpointId, UniqueId contextId);

		// Token: 0x06000C75 RID: 3189
		public abstract SessionSecurityToken Get(SessionSecurityTokenCacheKey key);

		// Token: 0x06000C76 RID: 3190
		public abstract void RemoveAll(string endpointId, UniqueId contextId);

		// Token: 0x06000C77 RID: 3191
		public abstract void RemoveAll(string endpointId);

		// Token: 0x06000C78 RID: 3192
		public abstract void Remove(SessionSecurityTokenCacheKey key);
	}
}
