using System;

namespace TechnoPro.Common.DAO.Impl.ServiceProvider.QueryStorage
{
	// Token: 0x02000061 RID: 97
	public class QueryStorageServiceProviderType
	{
		// Token: 0x040000F4 RID: 244
		internal const string QS_ACTIVE_PROVIDERTYPES = "SELECT sp.spprovidertypeid,sp.providertypetitle,sp.providertypedescription,sp.spprovidertypebehaviourcode,sp.providertypeisactive FROM spprovidertype sp WHERE sp.providertypeisactive=1 ORDER BY providertypetitle";
	}
}
