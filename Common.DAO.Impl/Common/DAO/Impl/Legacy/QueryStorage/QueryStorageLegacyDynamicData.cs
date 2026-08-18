using System;

namespace TechnoPro.Common.DAO.Impl.Legacy.QueryStorage
{
	// Token: 0x020000AD RID: 173
	public static class QueryStorageLegacyDynamicData
	{
		// Token: 0x04000237 RID: 567
		internal const string QS_Decrypted_Preview_Items_By_ControlId_PerStudent = "SELECT dataid,controlvalue FROM otherinfops WHERE controlid=@cid";

		// Token: 0x04000238 RID: 568
		internal const string QS_Decrypted_Preview_Items_By_ControlId_PerAppointment = "SELECT dataid,controlvalue FROM otherinfopa WHERE controlid=@cid";

		// Token: 0x04000239 RID: 569
		internal const string QS_Decrypted_Preview_Items_By_ControlId_Accommodation = "SELECT dataid,controlvalue FROM otherinfoaccommodationps WHERE controlid=@cid";

		// Token: 0x0400023A RID: 570
		internal const string QU_Update_Data_PerStudent = "UPDATE otherinfops SET controlvalue=@val WHERE dataid=@dataid\r\nSELECT COUNT(dataid) FROM otherinfops WHERE dataid=@dataid AND controlvalue=@val";

		// Token: 0x0400023B RID: 571
		internal const string QU_Update_Data_PerAppointment = "UPDATE otherinfopa SET controlvalue=@val WHERE dataid=@dataid\r\nSELECT COUNT(dataid) FROM otherinfopa WHERE dataid=@dataid AND controlvalue=@val";

		// Token: 0x0400023C RID: 572
		internal const string QU_Update_Data_Accommodation = "UPDATE otherinfoaccommodationps SET controlvalue=@val WHERE dataid=@dataid\r\nSELECT COUNT(dataid) FROM otherinfoaccommodationps WHERE dataid=@dataid AND controlvalue=@val";

		// Token: 0x0400023D RID: 573
		internal const string QS_STAFF_SIGNATURE = "DECLARE @sigcid INT\r\nSET @sigcid=(SELECT settingvalue AS titlecid FROM settingsgroups WHERE groupid=-1 AND settingcode=99719)\r\nSELECT TOP 1 controlvalue FROM imageinfops WHERE controlid=@sigcid AND personid=@pid";

		// Token: 0x0400023E RID: 574
		internal const string QI_STUDENT_NOTE = "IF NOT EXISTS(SELECT dataid FROM otherinfops WHERE controlid=@cid AND personid=@pid)\r\n    INSERT INTO otherinfops(screennum,controlid,personid,controlvalue) VALUES (1,@cid,@pid,@val)\r\nELSE\r\n    UPDATE otherinfops SET controlvalue=@val WHERE controlid=@cid AND personid=@pid";
	}
}
