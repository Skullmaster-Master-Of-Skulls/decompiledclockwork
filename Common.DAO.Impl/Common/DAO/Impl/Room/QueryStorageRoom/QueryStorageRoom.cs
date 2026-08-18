using System;

namespace TechnoPro.Common.DAO.Impl.Room.QueryStorageRoom
{
	// Token: 0x0200006B RID: 107
	internal static class QueryStorageRoom
	{
		// Token: 0x04000116 RID: 278
		internal const string QS_ALL_SEATS = "SELECT\tDISTINCT p.personid,p.firstname,p.middlename,p.lastname,p.student_no,\r\n\t\ts.ParentSeatGroupId,s.Campus,s.OrderNum\r\nFROM\tpeoplegroups pg LEFT JOIN people p ON p.PersonID=pg.PersonID\r\n\t\tLEFT JOIN Seat s ON s.PersonId=pg.PersonID\r\nWHERE\tpg.GroupID=3\r\nORDER BY s.Campus,s.ParentSeatGroupId,s.OrderNum";
	}
}
