using System;
using System.Collections.ObjectModel;
using System.Xml;

namespace System.ServiceModel.Security.Tokens
{
	// Token: 0x02000393 RID: 915
	public interface ISecurityContextSecurityTokenCache
	{
		// Token: 0x060021DD RID: 8669
		void AddContext(SecurityContextSecurityToken token);

		// Token: 0x060021DE RID: 8670
		bool TryAddContext(SecurityContextSecurityToken token);

		// Token: 0x060021DF RID: 8671
		void ClearContexts();

		// Token: 0x060021E0 RID: 8672
		void RemoveContext(UniqueId contextId, UniqueId generation);

		// Token: 0x060021E1 RID: 8673
		void RemoveAllContexts(UniqueId contextId);

		// Token: 0x060021E2 RID: 8674
		SecurityContextSecurityToken GetContext(UniqueId contextId, UniqueId generation);

		// Token: 0x060021E3 RID: 8675
		Collection<SecurityContextSecurityToken> GetAllContexts(UniqueId contextId);

		// Token: 0x060021E4 RID: 8676
		void UpdateContextCachingTime(SecurityContextSecurityToken context, DateTime expirationTime);
	}
}
