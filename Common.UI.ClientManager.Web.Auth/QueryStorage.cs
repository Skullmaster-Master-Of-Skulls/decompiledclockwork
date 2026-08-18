using System;

namespace TechnoPro.Common.UI.ClientManager.Web.Auth
{
	// Token: 0x02000009 RID: 9
	public static class QueryStorage
	{
		// Token: 0x04000014 RID: 20
		public static readonly string QS_Select_GroupMembership = "SELECT personid FROM peoplegroups WHERE personid=@pid AND groupid=@gid";
	}
}
