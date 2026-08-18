using System;

namespace TechnoPro.Common.DAO.Impl.Data
{
	// Token: 0x020000F5 RID: 245
	public static class QueryStorageDataMaintenance
	{
		// Token: 0x04000413 RID: 1043
		internal const string QS_STAFF_DROPLIST_ASSIGNMENTS = "SELECT\tm.dataid,m.personid,m.controlvalue AS staffpersonid,\r\n\t\t    p.lastName,p.firstName,p.middleName,p.student_no\r\nFROM\t    maininfops m LEFT JOIN people p ON p.PersonID=m.PersonID\r\nWHERE\t    m.controlid=@cid AND m.controlvalue=@staffpid";

		// Token: 0x04000414 RID: 1044
		internal const string QU_REASSIGN_STAFF_DROP_LIST = "UPDATE maininfops SET controlvalue=@staffpidnew WHERE controlid=@cid AND controlvalue=@staffpidold";
	}
}
