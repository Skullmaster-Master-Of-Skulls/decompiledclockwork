using System;

namespace TechnoPro.Common.DAO.Impl.ServiceProvider.QueryStorage
{
	// Token: 0x02000060 RID: 96
	public class QueryStorageServiceProviderLookup
	{
		// Token: 0x040000EE RID: 238
		internal const string QS_REQUESTSTATUSTYPE_BY_ID = "SELECT r.sprequeststatustypeid,r.rstitle,r.rsdescription,r.rsassignmentisrequired,r.rsspurgencyleveltypeid,u.urgencytitle AS rsurgencytitle,u.urgencydescription AS rsurgencydescription,u.urgencylevel AS rsurgencylevel FROM sprequeststatustype r LEFT JOIN spurgencyleveltype u ON u.spurgencyleveltypeid=r.rsspurgencyleveltypeid WHERE sprequeststatustypeid=@sprequeststatustypeid";

		// Token: 0x040000EF RID: 239
		internal const string QS_ACTIVE_REQUESTSTATUSTYPES = "SELECT r.sprequeststatustypeid,r.rstitle,r.rsdescription,r.rsassignmentisrequired,r.rsspurgencyleveltypeid,u.urgencytitle AS rsurgencytitle,u.urgencydescription AS rsurgencydescription,u.urgencylevel AS rsurgencylevel FROM sprequeststatustype r LEFT JOIN spurgencyleveltype u ON u.spurgencyleveltypeid=r.rsspurgencyleveltypeid ORDER BY r.rstitle";

		// Token: 0x040000F0 RID: 240
		internal const string QS_REQUESTASSIGNMENTSTATUSTYPE_BY_ID = "SELECT a.SPRequestAssignmentStatusTypeId,a.astitle,a.asdescription,a.asassignmentiscompleted,a.asspurgencyleveltypeid,u.urgencytitle AS asurgencytitle,u.urgencydescription AS asurgencydescription,u.urgencylevel AS asurgencylevel FROM sprequestassignmentstatustype a LEFT JOIN spurgencyleveltype u ON u.spurgencyleveltypeid=a.asspurgencyleveltypeid WHERE a.sprequeststatustypeid=@SPRequestAssignmentStatusTypeId";

		// Token: 0x040000F1 RID: 241
		internal const string QS_ACTIVE_REQUESTASSIGNMENTSTATUSTYPES = "SELECT a.SPRequestAssignmentStatusTypeId,a.astitle,a.asdescription,a.asassignmentiscompleted,a.asspurgencyleveltypeid,u.urgencytitle AS asurgencytitle,u.urgencydescription AS asurgencydescription,u.urgencylevel AS asurgencylevel FROM sprequestassignmentstatustype a LEFT JOIN spurgencyleveltype u ON u.spurgencyleveltypeid=a.asspurgencyleveltypeid ORDER BY a.astitle";

		// Token: 0x040000F2 RID: 242
		internal const string QS_URGENCYLEVELTYPE_BY_ID = "SELECT spurgencyleveltypeid,urgencytitle,urgencydescription,urgencylevel FROM spurgencyleveltype WHERE spurgencyleveltypeid=@spurgencyleveltypeid";

		// Token: 0x040000F3 RID: 243
		internal const string QS_ACTIVE_URGENCYLEVELTYPES = "SELECT spurgencyleveltypeid,urgencytitle,urgencydescription,urgencylevel FROM spurgencyleveltype ORDER BY urgencytitle";
	}
}
