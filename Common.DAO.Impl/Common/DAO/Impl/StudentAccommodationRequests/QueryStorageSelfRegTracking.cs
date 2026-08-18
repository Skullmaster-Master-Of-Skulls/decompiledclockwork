using System;

namespace TechnoPro.Common.DAO.Impl.StudentAccommodationRequests
{
	// Token: 0x02000041 RID: 65
	internal static class QueryStorageSelfRegTracking
	{
		// Token: 0x040000BA RID: 186
		internal const string QI_EXTERNAL_STAFF_LOA_ACCESS_LOG = "INSERT INTO LoaExternalLog (personid,studentpersonid,lucourseid) VALUES (@staffpid,@studentpid,@lucid)";
	}
}
