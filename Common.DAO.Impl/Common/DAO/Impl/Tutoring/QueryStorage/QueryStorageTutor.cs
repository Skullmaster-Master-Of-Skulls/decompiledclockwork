using System;

namespace TechnoPro.Common.DAO.Impl.Tutoring.QueryStorage
{
	// Token: 0x02000035 RID: 53
	internal static class QueryStorageTutor
	{
		// Token: 0x0400007C RID: 124
		internal const string QS_TUTOR_INFOS = "SELECT\tDISTINCT p.personid,pg.groupid,m.controlvalue AS isauthorized,d.controlvalue AS confsigndate\r\nFROM\tpeople p LEFT JOIN peoplegroups pg ON pg.personid=p.personid AND pg.groupid=5 --tutor group\r\n\t\tLEFT JOIN maininfops m ON m.personid=p.personid AND m.controlid=@authcid\r\n\t\tLEFT JOIN datetimeinfops d ON d.personid=p.personid AND d.controlid=@confCid\r\nWHERE\tp.isactive=1 AND NOT pg.groupid IS NULL\r\nORDER BY p.personid";

		// Token: 0x0400007D RID: 125
		internal const string QS_TUTOR_INFOS_BY_TUTOR_PIDS = "SELECT\tDISTINCT p.personid,pg.groupid,m.controlvalue AS isauthorized,d.controlvalue AS confsigndate\r\nFROM\tpeople p LEFT JOIN peoplegroups pg ON pg.personid=p.personid AND pg.groupid=5 --tutor group\r\n\t\tLEFT JOIN maininfops m ON m.personid=p.personid AND m.controlid=@authcid\r\n\t\tLEFT JOIN datetimeinfops d ON d.personid=p.personid AND d.controlid=@confCid\r\nWHERE\tp.isactive=1 AND NOT pg.groupid IS NULL\r\n        AND p.personid IN (SELECT orderid AS personid FROM splitorderids(@pids,','))\r\nORDER BY p.personid";
	}
}
